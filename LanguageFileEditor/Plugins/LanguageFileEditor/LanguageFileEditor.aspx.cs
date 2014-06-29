using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml;
using EPiServer.Core;
using EPiServer.Data.Dynamic;
using EPiServer.PlugIn;
using EPiServer.UI;

namespace EPiServer.Plugins.LanguageFileEditor
{
    [GuiPlugIn(
        DisplayName = "Language File Editor v1.1",
        Description = "Tool for allowing the EPiServer web administrator create, update and remove language files.",
        Area = PlugInArea.AdminMenu,
        RequiredAccess = Security.AccessLevel.Administer,
        Url = "~/Plugins/LanguageFileEditor/LanguageFileEditor.aspx"
    )]
    // TODO - Remember to secure your Plugins directory to prevent unauthorized access to the editor.
    public partial class LanguageFileEditor : SystemPageBase
    {
        protected bool ShowMessage { get; set; }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            UserValidator.EnsureValidRoles();

            Page.MasterPageFile = Configuration.Settings.Instance.UIUrl + "MasterPages/EPiServerUI.Master";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (IsPostBack) return;

            RefreshCreateNewArea();
            RefreshReapplyArea();
            RefreshBackupArea();
            RefreshLanguageFileList();

            mvLanguageEditor.SetActiveView(vManageLanguageFiles);
        }
        
        private static IEnumerable<ListItem> ConvertToListItems(IEnumerable<string> filenames)
        {
            return filenames
                .Select(filename => new ListItem
                                        {
                                            Text = filename,
                                            Value = filename
                                        }).ToList();
        }

        private static IEnumerable<string> GetAllFilenamesFrom(string directory)
        {
            PathValidator.EnsureValid(directory);

            var xmlFilePaths = Directory.GetFiles(directory, "*.xml");
            return xmlFilePaths.Select(path => path.Split(System.IO.Path.DirectorySeparatorChar).Last());
        }

        #region Edit view region

        private void InitEditFor(string filename)
        {
            hfPatternFilename.Value = filename;
            var filePath = Path.To(filename);
            PathValidator.EnsureValid(filePath);

            var xmlDocument = new XmlDocument();
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                xmlDocument.Load(fileStream);
            }

            csEditableTree.XmlDeclaration = xmlDocument.FirstChild as XmlDeclaration;
            xmlDocument.RemoveChild(xmlDocument.FirstChild);
            csEditableTree.DataSource = xmlDocument.ChildNodes;
            csEditableTree.DataBind();
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            RefreshCreateNewArea();
            RefreshReapplyArea();
            RefreshLanguageFileList();
            mvLanguageEditor.SetActiveView(vManageLanguageFiles);
        }

        #endregion

        #region Create new files region

        private void RefreshCreateNewArea()
        {
            ddlCopyFrom.DataSource = ConvertToListItems(GetAllFilenamesFrom(LanguageManager.Instance.Directory));
            ddlCopyFrom.DataBind();
        }

        protected void btnNewFile_OnClick(object sender, EventArgs e)
        {
            litEditLegend.Text = string.Concat("Creating new language file based on: ", ddlCopyFrom.SelectedValue);
            tbFileName.Enabled = true;
            tbFileName.Text = string.Empty;
            InitEditFor(ddlCopyFrom.SelectedValue);
            mvLanguageEditor.SetActiveView(vEditLanguageFile);
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            tbFileName.Text = string.Empty;
            mvLanguageEditor.SetActiveView(vManageLanguageFiles);
            litMessage.Text = "Action canceled.";
            ShowMessage = true;
        }

        #endregion

        #region Reapply changes region

        private void RefreshReapplyArea()
        {
            var store = typeof (ChangeTrackingContainer).GetStore();
            var items = store.LoadAllAsPropertyBag()
                .Select(bag => new ListItem
                        {
                            Text = bag["Filename"] as string,
                            Value = bag["Filename"] as string
                        }).ToList();
            phReapplyChanges.Visible = items.Count > 0;
            ddlReapplyChanges.DataSource = items;
            ddlReapplyChanges.DataBind();
        }

        protected void btnReapplyChanges_OnClick(object sender, EventArgs e)
        {
            var filename = ddlReapplyChanges.SelectedValue;
            new LanguageFileUpdater().ExecuteReapplyFor(filename);
            litMessage.Text = string.Format("Changes reapplied for file {0}.", filename);
            ShowMessage = true;
        }

        protected void btnDeleteChanges_OnClick(object sender, EventArgs e)
        {
            var filename = ddlReapplyChanges.SelectedValue;
            var store = typeof(ChangeTrackingContainer).GetStore();
            var existingBags = store.FindAsPropertyBag("Filename", filename);
            foreach (var existingBag in existingBags)
            {
                store.Delete(existingBag.Id);
            }
            RefreshReapplyArea();
            litMessage.Text = string.Format("Reapply possibilities removed for file {0}.", filename);
            ShowMessage = true;
        }

        protected void btnReapplyAll_OnClick(object sender, EventArgs e)
        {
            new LanguageFileUpdater().ExecuteReapplyForAll();
            litMessage.Text = "All files had their changes were successfully reapplied.";
            ShowMessage = true;
        }

        #endregion

        #region Backup controls region
        
        private void RefreshBackupArea()
        {
            var store = typeof(LangFileBackupContainer).GetStore();
            var items = store.LoadAllAsPropertyBag()
                .Select(bag => new ListItem
                {
                    Text = string.Format("{0} {1}", bag["Filename"] as string, ((DateTime)bag["Created"]).ToString("yyyy-MM-dd HH:mm:ss")),
                    Value = bag["BackupId"] as string
                }).ToList();
            phRestoreBackups.Visible = items.Count > 0;
            ddlRestoreBackup.DataSource = items;
            ddlRestoreBackup.DataBind();
        }

        protected void btnRestoreBackup_OnClick(object sender, EventArgs e)
        {
            var backupId = ddlRestoreBackup.SelectedValue;
            var store = typeof(LangFileBackupContainer).GetStore();
            var backup = store
                            .ItemsAsPropertyBag()
                            .First(item => backupId.Equals(item["BackupId"] as string));
            var fileContent = backup["Content"] as string;
            var path = Path.To(backup["Filename"] as string);
            PathValidator.EnsureValid(path);
            using (var streamWriter = new StreamWriter(path))
            {
                streamWriter.Write(fileContent);
            }
            RefreshLanguageFileList();
            litMessage.Text = string.Format("File {0} was successfully restored.", backup["Filename"] as string);
            ShowMessage = true;
        }

        protected void btnDeleteAllBackups_OnClick(object sender, EventArgs e)
        {
            var store = typeof(LangFileBackupContainer).GetStore();
            foreach (var existingBag in store.LoadAllAsPropertyBag())
            {
                store.Delete(existingBag.Id);
            }
            litMessage.Text = "All backups were successfully deleted.";
            ShowMessage = true;
            RefreshBackupArea();
        }

        #endregion

        #region Language file list region

        private int _counter;
        protected string AlternatingRowClass
        {
            get { return _counter++ % 2 == 0 ? string.Empty : " altRow"; }
        }
        
        private void RefreshLanguageFileList()
        {
            rptLanguageFiles.DataSource = GetAllFilenamesFrom(LanguageManager.Instance.Directory);
            rptLanguageFiles.DataBind();
        }

        protected void lbEditFile_OnCommand(object sender, CommandEventArgs e)
        {
            var filename = e.CommandArgument.ToString();
            litEditLegend.Text = string.Concat("Editing existing file: ", filename);
            tbFileName.Enabled = false;
            tbFileName.Text = filename;
            InitEditFor(filename);
            mvLanguageEditor.SetActiveView(vEditLanguageFile);
        }

        protected void lbBackUpFile_OnCommand(object sender, CommandEventArgs e)
        {
            var filename = e.CommandArgument.ToString();
            var filePath = Path.To(filename);
            PathValidator.EnsureValid(filePath);

            var store = typeof(LangFileBackupContainer).GetStore();
            store.Save(new LangFileBackupContainer
                           {
                               BackupId = Guid.NewGuid().ToString(),
                               Filename = filename,
                               Content = File.ReadAllText(filePath),
                               Created = DateTime.Now
                           });
            RefreshBackupArea();
            litMessage.Text = string.Format("File {0} was successfully backed up.", filename);
            ShowMessage = true;
        }

        protected void lbDeleteFile_OnCommand(object sender, CommandEventArgs e)
        {
            var filename = e.CommandArgument.ToString();
            var filePath = Path.To(filename);
            PathValidator.EnsureValid(filePath);
            File.Delete(filePath);
            RefreshCreateNewArea();
            RefreshLanguageFileList();
            litMessage.Text = string.Format("File {0} was successfully deleted.", filename);
            ShowMessage = true;
        }

        #endregion
    }
}
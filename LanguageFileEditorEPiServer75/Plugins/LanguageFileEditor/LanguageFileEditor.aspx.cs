using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml;
using EPiServer.Data.Dynamic;
using EPiServer.PlugIn;
using EPiServer.ServiceLocation;
using EPiServer.Shell.WebForms;

namespace EPiServer.Templates.Alloy.Plugins.LanguageFileEditor
{
	[GuiPlugIn(
		DisplayName = "Language File Editor v.1.1 (EPi7)",
		Description = "Tool for editing EPiServer language files",
		Area = PlugInArea.AdminMenu,
		RequiredAccess = EPiServer.Security.AccessLevel.Administer,
		Url = "~/Plugins/LanguageFileEditor/LanguageFileEditor.aspx"
	)]
    // TODO - Remember to secure your plugins directory!
	public partial class LanguageFileEditor : WebFormsBase
	{
		private readonly ILanguageLocationService _languageLocationService;
		private readonly ISecurityValidator _securityValidator;
		private readonly ILanguageFileUpdater _languageFileUpdater;
		private string _langPath;

		protected bool ShowMessage { get; set; }

		public LanguageFileEditor()
		{
			_languageLocationService = ServiceLocator.Current.GetInstance<ILanguageLocationService>();
			_securityValidator = ServiceLocator.Current.GetInstance<ISecurityValidator>();
			_languageFileUpdater = ServiceLocator.Current.GetInstance<ILanguageFileUpdater>();
		}

		protected override void OnPreInit(EventArgs e)
		{
			base.OnPreInit(e);
			_securityValidator.EnsureValidUser();

			Page.MasterPageFile = EPiServer.Configuration.Settings.Instance.UIUrl + "MasterPages/EPiServerUI.Master";
            SystemMessageContainer.Heading = "Language File Editor v.1.1 (EPi7)";
			SystemMessageContainer.Description = "This tool allows for updating texts on the website that are retrieved from language files; a normal administrator should not need to use the backup and restore features, they exist to prevent data loss during deploys.";

			_langPath = _languageLocationService.LanguagePath;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (IsPostBack) return;

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

		private IEnumerable<string> GetAllFilenamesFrom(string directory)
		{
			_securityValidator.EnsureValid(directory);

			var xmlFilePaths = Directory.GetFiles(directory, "*.xml");
			return xmlFilePaths.Select(path => path.Split(Path.DirectorySeparatorChar).Last());
		}

		#region Edit view region

		private void InitEditFor(string filename)
		{
			hfPatternFilename.Value = filename;
			var filePath = _languageLocationService.PathTo(filename);
			_securityValidator.EnsureValid(filePath);

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
			RefreshReapplyArea();
			RefreshLanguageFileList();
			mvLanguageEditor.SetActiveView(vManageLanguageFiles);
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
			var store = typeof(ChangeTrackingContainer).GetStore();
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
			_languageFileUpdater.ExecuteReapplyFor(filename);
			litMessage.Text = string.Format("Changes made in the tool are reapplied to the file {0}.", filename);
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
			litMessage.Text = string.Format("Restore possibilities removed for {0}.", filename);
			ShowMessage = true;
		}

		protected void btnReapplyAll_OnClick(object sender, EventArgs e)
		{
			_languageFileUpdater.ExecuteReapplyForAll();
			litMessage.Text = "Changes made in the tool are reapplied for all files.";
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
			var backup = store.ItemsAsPropertyBag().First(item => backupId.Equals(item["BackupId"] as string));
			var fileContent = backup["Content"] as string;
			var path = _languageLocationService.PathTo(backup["Filename"] as string);
			_securityValidator.EnsureValid(path);
			
			using (var streamWriter = new StreamWriter(path))
			{
				streamWriter.Write(fileContent);
			}
			RefreshLanguageFileList();
			litMessage.Text = string.Format("The file {0} was restored.", backup["Filename"] as string);
			ShowMessage = true;
		}

		protected void btnDeleteAllBackups_OnClick(object sender, EventArgs e)
		{
			var store = typeof(LangFileBackupContainer).GetStore();
			foreach (var existingBag in store.LoadAllAsPropertyBag())
			{
				store.Delete(existingBag.Id);
			}
			litMessage.Text = "All backups are now deleted.";
			ShowMessage = true;
			RefreshBackupArea();
		}

		#endregion

		#region Language file list region

		private int counter;

		protected string AlternatingRowClass
		{
			get { return counter++ % 2 == 0 ? string.Empty : " altRow"; }
		}

		private void RefreshLanguageFileList()
		{
			rptLanguageFiles.DataSource = GetAllFilenamesFrom(_langPath);
			rptLanguageFiles.DataBind();
		}

		protected void lbEditFile_OnCommand(object sender, CommandEventArgs e)
		{
			var filename = e.CommandArgument.ToString();
			litEditLegend.Text = string.Concat("Editing file: ", filename);
			tbFileName.Enabled = false;
			tbFileName.Text = filename;
			InitEditFor(filename);
			mvLanguageEditor.SetActiveView(vEditLanguageFile);
		}

		protected void lbBackUpFile_OnCommand(object sender, CommandEventArgs e)
		{
			var filename = e.CommandArgument.ToString();
			var filePath = _languageLocationService.PathTo(filename);
			_securityValidator.EnsureValid(filePath);

			var store = typeof(LangFileBackupContainer).GetStore();
			store.Save(new LangFileBackupContainer
						   {
							   BackupId = Guid.NewGuid().ToString(),
							   Filename = filename,
							   Content = File.ReadAllText(filePath),
							   Created = DateTime.Now
						   });
			RefreshBackupArea();
			litMessage.Text = string.Format("The file {0} is now backed up.", filename);
			ShowMessage = true;
		}

		#endregion
	}
}

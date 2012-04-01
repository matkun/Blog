using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.Adapters;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EPiServer.ClientScript;
using EPiServer.UI.Edit;
using EPiServer.UI.WebControls;
using XFlow.Core.Extensions;
using XFlow.Core.Framework.Services;

namespace XFlow.Core.Framework.ControlAdapters
{
    public class XFormEditAdapter : PageAdapter
    {
        private XFormEdit _xFormEdit;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _xFormEdit = (XFormEdit) Control;
            if(_xFormEdit == null)
            {
                return;
            }
            AddXFlowPagesToSystemMessage();
        }

        private void AddXFlowPagesToSystemMessage()
        {
            var request = IoC.IoC.Get<HttpRequestBase>();
            var id = request["formid"];
            Guid formId;
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out formId))
            {
                return;
            }
            var pageLocatorService = IoC.IoC.Get<IPageLocatorService>();
            var pages = pageLocatorService.PagesUsingXForm(formId.ToString());
            if(pages == null || !pages.Any())
            {
                return;
            }
            var messagePanel = _xFormEdit.FindControlRecursive("MessagePanel") as Panel;
            if(messagePanel == null)
            {
                return;
            }
            var notificationPanel = messagePanel.Controls.Count > 0 ? messagePanel.Controls[0] as Panel : new Panel { CssClass = "EP-systemMessage" };
            if(messagePanel.Controls.Count <= 0)
            {
                messagePanel.Controls.Add(notificationPanel);
            }
            const string format = "{0} [{1}]";
            var xFlowPages = string.Join(", ", pages.Select(page => string.Format(format, page.PageName, page.PageLink)).ToArray());
            var message = string.Format( "Note that this form is used in XFlow properties on {0} page/pages. Changes to the form will also affect:<br />{1}", pages.Count, xFlowPages);
            
            if(notificationPanel.Controls.Count > 0)
            {
                notificationPanel.Controls.Add(new HtmlGenericControl("br"));
            }
            notificationPanel.Controls.Add(new Literal { Text = message });


            //string str = (index > 10) ? (string.Join(", ", strArray, 0, 10) + "&hellip;") : string.Join(", ", strArray, 0, index);
            //string toolTip = (index > 10) ? string.Join(", ", strArray, 0, index) : null;
            //this.ShowMessage(string.Format(base.Translate("/edit/editxform/existonpages"), index) + "<br/>" + str, toolTip, false);
            //_xFormEdit.DeleteButton.Enabled = false;

            ScriptManager.Current.AddSetting("window", "formIsUsedOnOtherPages", "true");


            var deleteButton = _xFormEdit.FindControlRecursive("DeleteButton") as ToolButton;
            if (deleteButton == null) //todo - duplicerad kod, finns även i xformselectadapter - fixa
            {
                return;
            }
            deleteButton.Enabled = false;
            var span = deleteButton.Parent as HtmlGenericControl;
            if (span == null)
            {
                return;
            }
            const string disableClass = "epi-cmsButtondisabled";
            var spanClasses = span.Attributes["class"];
            if (!string.IsNullOrEmpty(spanClasses) && spanClasses.Contains(disableClass))
            {
                return;
            }
            span.Attributes["class"] = string.Join(" ", new[] { spanClasses, disableClass });
        }
    }
}

//protected override void OnLoad(EventArgs e)
//{
//    base.OnLoad(e);
//    base.Title = LanguageManager.Instance.Translate("/edit/editxform/heading");
//    this.IsPageLeaveCheckEnabled = true;
//    base.RegisterClientScriptFile(base.ResolveUrlFromUI("javascript/xformedit.js"));
//    if (!string.IsNullOrEmpty(base.Request["formid"]))
//    {
//        this.XForm = XForm.CreateInstance(new Guid(base.Request["formid"].Replace("'", string.Empty)));
//        this.WritePagesWithThisForm(this.XForm);
//        this.NewFormContent.Visible = false;
//    }
//    else
//    {
//        this.DeleteButton.Enabled = false;
//        this.SaveAsButton.Enabled = false;
//        this.NewFormContent.Visible = true;
//    }
//    this.FormControl.FormDefinition = this.XForm;
//    this.FormControl.DataBind();
//    this.WriteEditFormScript();
//    if (!base.IsPostBack)
//    {
//        this.SetPageAfterPostForEdit();
//    }
//    ThemeUtility.RegisterThemeCss(this.Page, "xformedit.css");
//    if (!base.IsPostBack)
//    {
//        this.InitializeFields();
//    }
//    if (this.SaveAsAction.Value == "1")
//    {
//        this.XForm = XForm.CreateInstance();
//        this.SaveForm(true);
//    }
//}


using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer;
using EPiServer.ClientScript;
using EPiServer.Core;
using EPiServer.Util;
using EPiServer.Web;
using EPiServer.XForms;
using EPiServer.XForms.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using XFlow.Core.Extensions;
using XFlow.Core.Framework.CustomProperties.XFlow;
using XFlow.Core.Framework.EmbeddedResources;
using XFlow.Core.Framework.IoC;

namespace XFlow.Core.Presentation.XFlowCollectionControl
{
    public partial class PropertyXFlowViewModeControl : UserControlBase, IPropertyXFlowViewModeControlView
    {
        //protected string XFormEditorUrl
        //{
        //    get
        //    {
        //        return string.Format("XFormSelect.aspx?pageId={0}&parentId={1}", CurrentPage.PageLink, CurrentPage.ParentLink);
        //        //return string.Concat(
        //        //    EPiServer.Configuration.Settings.Instance.UIUrl.ToString(),
        //        //    "edit/XFormSelect.aspx");
        //    }
        //}// = "/secure/UI/CMS/edit/XFormSelect.aspx";
        
        //protected override void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);
        //    var resourceHandler = IoC.Get<IResourceHandler>();
        //    resourceHandler.AddJQueryJson(Page);
        //    resourceHandler.AddEditCollectionJavaScript(Page);
        //    DataFactory.Instance.SavingPage += new PageEventHandler(Update_XFormUsedOnPage);
        //    DataFactory.Instance.PublishingPage += new PageEventHandler(Update_XFormUsedOnPage);
        //}

        //private void Update_XFormUsedOnPage(object sender, PageEventArgs e)
        //{
        //    var allForms = XForm.GetForms();
        //    XFormHelper.ListFormReferences("formid");
        //}

        //private string[] GetxFormUsedonPages(string formId)
        //{
        //    PageDataCollection datas = XFormHelper.ListFormReferences(formId);
        //    string[] strArray = new string[datas.Count];
        //    for (int i = 0; i < datas.Count; i++)
        //    {
        //        strArray[i] = string.Format("{0}[{1}]", datas[i].PageName, datas[i].PageLink);
        //    }
        //    return strArray;
        //}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AddViews();
            XFlowMultiView.ActiveViewIndex = 0;
            XFlowMultiView.DataBind();
        }

        private void AddViews()
        {
            var i = 0;
            foreach (var item in XFormItems)
            {
                i++;
                var view = new View();
                var form = new XFormControl();
                form.FormDefinition = XForm.CreateInstance(item.FormId.Value);
                form.ValidationGroup = "ValidationGroupView" + i;
                view.Controls.Add(form);
                XFlowMultiView.Views.Add(view);
            }
            
        }

        public XFormItem[] XFormItems { get; set; }
        //{
        //    get { return (XFormItem[])rptXFlow.DataSource; }
        //    set 
        //    {
        //        rptXFlow.DataSource = value;
        //        rptXFlow.DataBind();
        //    }
        //}

        protected string UniqueIdentifier
        {
            get { return ClientScriptUtility.ToScriptSafeIdentifier(ClientID); }
        }
    }
}

using System;
using System.Web.UI;
using EPiServer;
using EPiServer.ClientScript;
using EPiServer.Core;
using EPiServer.Util;
using EPiServer.Web;
using EPiServer.XForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using XFlow.Core.Extensions;
using XFlow.Core.Framework.CustomProperties.XFlow;
using XFlow.Core.Framework.EmbeddedResources;
using XFlow.Core.Framework.IoC;

namespace XFlow.Core.Presentation.XFlowCollectionControl
{
    public partial class PropertyXFormItemCollectionControl : UserControlBase, IPropertyXFormItemCollectionView
    {
        protected string XFormEditorUrl
        {
            get
            {
                return string.Format("XFormSelect.aspx?pageId={0}&parentId={1}", CurrentPage.PageLink, CurrentPage.ParentLink);
                //return string.Concat(
                //    EPiServer.Configuration.Settings.Instance.UIUrl.ToString(),
                //    "edit/XFormSelect.aspx");
            }
        }// = "/secure/UI/CMS/edit/XFormSelect.aspx";
        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var resourceHandler = IoC.Get<IResourceHandler>();
            resourceHandler.AddJQueryJson(Page);
            resourceHandler.AddEditCollectionJavaScript(Page);
            DataFactory.Instance.SavingPage += new PageEventHandler(Update_XFormUsedOnPage);
            DataFactory.Instance.PublishingPage += new PageEventHandler(Update_XFormUsedOnPage);
        }

        private void Update_XFormUsedOnPage(object sender, PageEventArgs e)
        {
            var allForms = XForm.GetForms();
            XFormHelper.ListFormReferences("formid");
        }

        private string[] GetxFormUsedonPages(string formId)
        {
            PageDataCollection datas = XFormHelper.ListFormReferences(formId);
            string[] strArray = new string[datas.Count];
            for (int i = 0; i < datas.Count; i++)
            {
                strArray[i] = string.Format("{0}[{1}]", datas[i].PageName, datas[i].PageLink);
            }
            return strArray;
        }



        protected string UniqueIdentifier
        {
            get { return ClientScriptUtility.ToScriptSafeIdentifier(ClientID); }
        }

        private string HiddenFieldId
        {
            get { return string.Concat("hidden-updated-items-", UniqueIdentifier); }
        }

        //todo måste sätta used-on-page för alla nya
        //todo måste rensa bort used-on-page för alla borttagna

        private string _initialXFormItems;
        public string InitialXFormItems
        {
            get { return _initialXFormItems.EscapedJson(); }
            set
            {

                _initialXFormItems = Request.Form[HiddenFieldId] ?? value;
            }
        }

        public string UpdatedItems
        {
            get { return Request.Form[HiddenFieldId]; }
        }

        protected string EmptyXFormItem
        {
            get { return JsonConvert.SerializeObject(new[] {XFormItem.Empty}, new IsoDateTimeConverter()); }
        }
    }
}

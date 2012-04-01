using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Web.PropertyControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using XFlow.Core.AdapterPattern;
using XFlow.Core.Extensions;

namespace XFlow.Core.Framework.CustomProperties.XFlow
{
    public class PropertyXFormItemCollectionControl : PropertyDataControl
    {
        //private readonly IJsonConvertWrapper _jsonConvert;

        //public PropertyXFormItemCollectionControl(IJsonConvertWrapper jsonConvert)
        //{
        //    if (jsonConvert == null) throw new ArgumentNullException("jsonConvert");
        //    _jsonConvert = jsonConvert;
        //}

        private IPropertyXFormItemCollectionView _editPropertyXFormItemCollectionControl;
        public override void CreateEditControls()
        {
            var xFormItemCollectionControl = Page.LoadControl("~/EmbeddedXFlowResources/XFlow.Core.dll/XFlow.Core.Presentation.XFlowCollectionControl.PropertyXFormItemCollectionControl.ascx");
            _editPropertyXFormItemCollectionControl = (IPropertyXFormItemCollectionView)xFormItemCollectionControl;
            _editPropertyXFormItemCollectionControl.InitialXFormItems = JsonConvert.SerializeObject(XFormItems, new IsoDateTimeConverter());
            Controls.Add(xFormItemCollectionControl);
            _editPropertyXFormItemCollectionControl.InitialXFormItems = JsonConvert.SerializeObject(XFormItems, new IsoDateTimeConverter());
        }

        protected XFormItem[] XFormItems
        {
            get { return ((PropertyXFormItemCollection)PropertyData).XFormItems ?? new XFormItem[0]; }
            set { ((PropertyXFormItemCollection)PropertyData).XFormItems = value; }
        }

        public override void ApplyEditChanges()
        {
            try
            {
                if (_editPropertyXFormItemCollectionControl.UpdatedItems.TrimmedNullOrEmpty())
                {
                    XFormItems = new XFormItem[0];
                    return;
                }

                var updatedXFormItems = JsonConvert.DeserializeObject<IEnumerable<XFormItem>>(_editPropertyXFormItemCollectionControl.UpdatedItems, new IsoDateTimeConverter());
                var coll = updatedXFormItems ?? Enumerable.Empty<XFormItem>();
                XFormItems = coll.ToArray();
            }
            catch (Exception ex)
            {
                AddErrorValidator(ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using EPiServer.Core;
using EPiServer.PlugIn;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace XFlow.Core.Framework.CustomProperties.XFlow
{
    [Serializable]
    [PageDefinitionTypePlugIn(DisplayName = "XFlow")]
    public class PropertyXFormItemCollection : PropertyData
    {
        [NonSerialized] private XFormItem[] _xformItems;
        private string _jSon;

        protected override void SetDefaultValue()
        {
            _jSon = JsonConvert.SerializeObject(new XFormItem[0], new IsoDateTimeConverter());
        }

        public override PropertyData ParseToObject(string value)
        {
            return Parse(value);
        }

        public override void ParseToSelf(string value)
        {
            XFormItems = Parse(value).XFormItems;
        }

        public override object Value
        {
            get { return _jSon; }
            set
            {
                if (value is IEnumerable<XFormItem>)
                {
                    XFormItems = ((IEnumerable<XFormItem>)value).ToArray();
                }
                else if (value is string)
                {
                    XFormItems =  JsonConvert.DeserializeObject<XFormItem[]>((string)value, new IsoDateTimeConverter());
                }
                else
                {
                    throw new InvalidCastException(string.Format("An invalid object type \"{0}\" was passed into the property value. Expected type: \"{1}\".",
                                                 value.GetType().Name,
                                                 typeof(IEnumerable<XFormItem>).Name));
                }

            }
        }

        public override PropertyDataType Type
        {
            get { return PropertyDataType.LongString; }
        }

        public override Type PropertyValueType
        {
            get { return typeof(XFormItem[]); }
        }

        public override IPropertyControl CreatePropertyControl()
        {
            return IoC.IoC.Get<PropertyXFormItemCollectionControl>();
        }

        [XmlIgnore]
        public XFormItem[] XFormItems
        {
            get { return _xformItems ?? (_xformItems = JsonConvert.DeserializeObject<XFormItem[]>(_jSon, new IsoDateTimeConverter())); }
            set
            {
                ThrowIfReadOnly();
                if (value == null || !value.Any())
                {
                    Clear();
                    return;
                }

                Modified();
                _xformItems = value;
                _jSon = JsonConvert.SerializeObject(value, new IsoDateTimeConverter());
            }
        }

        private PropertyXFormItemCollection Parse(string value)
        {
            var property = new PropertyXFormItemCollection
            {
                XFormItems = JsonConvert.DeserializeObject<XFormItem[]>(value, new IsoDateTimeConverter()),
                IsModified = false
            };
            return property;
        }
    }
}

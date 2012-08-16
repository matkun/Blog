using System;
using System.Xml.Serialization;
using EPiServer.Core;
using EPiServer.PlugIn;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EPiServer.ImageMap.Core
{
    [Serializable]
    [PageDefinitionTypePlugIn(
        DisplayName = "Interactive Image Map",
        Description = "Property for creating an interactive image map where the web editor can add linked hot spots to an image."
    )]
    public class PropertyImageMap : PropertyData
    {
        [NonSerialized] private Domain.ImageMap _imageMap;
        private string _jSon;

        protected override void SetDefaultValue()
        {
            _jSon = JsonConvert.SerializeObject(Domain.ImageMap.Empty, new IsoDateTimeConverter());
        }

        public override PropertyData ParseToObject(string value)
        {
            return Parse(value);
        }

        public override void ParseToSelf(string value)
        {
            ImageMap = Parse(value).ImageMap;
        }

        public override object Value
        {
            get { return _jSon; }
            set
            {
                if (value is Domain.ImageMap)
                {
                    ImageMap = (Domain.ImageMap)value;
                }
                else if (value is string)
                {
                    ImageMap = JsonConvert.DeserializeObject<Domain.ImageMap>((string)value, new IsoDateTimeConverter());
                }
                else
                {
                    var message = string.Format("An invalid object type \"{0}\" was passed into the property value. Expected type: \"{1}\".", value.GetType().Name, typeof(Domain.ImageMap).Name);
                    throw new InvalidCastException(message);
                }
            }
        }

        public override PropertyDataType Type
        {
            get { return PropertyDataType.LongString; }
        }

        public override Type PropertyValueType
        {
            get { return typeof(Domain.ImageMap); }
        }

        public override IPropertyControl CreatePropertyControl()
        {
            return new PropertyImageMapControl();
        }

        [XmlIgnore]
        public Domain.ImageMap ImageMap
        {
            get { return _imageMap ?? (_imageMap = JsonConvert.DeserializeObject<Domain.ImageMap>(_jSon, new IsoDateTimeConverter())); }
            set
            {
                ThrowIfReadOnly();
                if (value == null)
                {
                    Clear();
                    return;
                }

                Modified();
                _imageMap = value;
                _jSon = JsonConvert.SerializeObject(value, new IsoDateTimeConverter());
            }
        }

        private static PropertyImageMap Parse(string value)
        {
            var property = new PropertyImageMap
            {
                ImageMap = JsonConvert.DeserializeObject<Domain.ImageMap>(value, new IsoDateTimeConverter()),
                IsModified = false
            };
            return property;
        }
    }
}

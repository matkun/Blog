using System;
using System.Xml.Serialization;
using EPiServer.Core;
using EPiServer.PlugIn;
using ImageSlideShow.Core.Framework.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using log4net;

namespace ImageSlideShow.Core.Framework.CustomProperties.ImageSlideShow
{
    [Serializable]
    [PageDefinitionTypePlugIn(
        DisplayName = "ImageSlideShow",
        Description = "Property for creating image slide shows with Nivo Slider."
    )]
    public class PropertyImageSlideCollection : PropertyData
    {
        [NonSerialized] private readonly ILog _logger;
        [NonSerialized] private SlideShow _imageSlideShow;
        private string _jSon;

        public PropertyImageSlideCollection()
        {
            _logger = Log.For(this);
        }

        protected override void SetDefaultValue()
        {
            _jSon = JsonConvert.SerializeObject(SlideShow.Empty, new IsoDateTimeConverter());
        }

        public override PropertyData ParseToObject(string value)
        {
            return Parse(value);
        }

        public override void ParseToSelf(string value)
        {
            SlideShow = Parse(value).SlideShow;
        }

        public override object Value
        {
            get { return _jSon; }
            set
            {
                if (value is SlideShow)
                {
                    SlideShow = (SlideShow) value;
                }
                else if (value is string)
                {
                    SlideShow = JsonConvert.DeserializeObject<SlideShow>((string)value, new IsoDateTimeConverter());
                }
                else
                {
                    var message = string.Format("An invalid object type \"{0}\" was passed into the property value. Expected type: \"{1}\".", value.GetType().Name, typeof(SlideShow).Name);
                    _logger.Error(message);
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
            get { return typeof(SlideShow); }
        }

        public override IPropertyControl CreatePropertyControl()
        {
            return IoC.IoC.Get<PropertyImageSlideCollectionControl>();
        }

        [XmlIgnore]
        public SlideShow SlideShow
        {
            get { return _imageSlideShow ?? (_imageSlideShow = JsonConvert.DeserializeObject<SlideShow>(_jSon, new IsoDateTimeConverter())); }
            set
            {
                ThrowIfReadOnly();
                if (value == null)
                {
                    Clear();
                    return;
                }

                Modified();
                _imageSlideShow = value;
                _jSon = JsonConvert.SerializeObject(value, new IsoDateTimeConverter());
            }
        }

        private static PropertyImageSlideCollection Parse(string value)
        {
            var property = new PropertyImageSlideCollection
            {
                SlideShow = JsonConvert.DeserializeObject<SlideShow>(value, new IsoDateTimeConverter()),
                IsModified = false
            };
            return property;
        }
    }
}

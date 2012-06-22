using System;
using System.Linq;
using EPiServer.Web.PropertyControls;
using ImageSlideShow.Core.Extensions;
using ImageSlideShow.Core.Framework.EmbeddedResources;
using ImageSlideShow.Core.Framework.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using log4net;

namespace ImageSlideShow.Core.Framework.CustomProperties.ImageSlideShow
{
    public partial class PropertyImageSlideCollectionControl : PropertyDataControl
    {
        private readonly ILog _logger;
        private IPropertyImageSlideShowViewModeControlView _viewPropertyImageSlideShowViewModeControlView;

        public PropertyImageSlideCollectionControl()
        {
            _logger = Log.For(this);
        }

        public override void CreateDefaultControls()
        {
            if(!ImageSlideShow.Slides.Any())
            {
                return;
            }
            var viewControl = Page.LoadControl(VirtualPaths.SlideShowViewModeControl);
            _viewPropertyImageSlideShowViewModeControlView = (IPropertyImageSlideShowViewModeControlView) viewControl;
            _viewPropertyImageSlideShowViewModeControlView.SlideShow = ImageSlideShow;
            Controls.Add(viewControl);
        }

        private IPropertyImageSlideCollectionView _editPropertyImageSlideCollectionControl;
        public override void CreateEditControls()
        {
            var imageSlideCollectionControl = Page.LoadControl(VirtualPaths.SlideShowEditModeControl);
            _editPropertyImageSlideCollectionControl = (IPropertyImageSlideCollectionView)imageSlideCollectionControl;
            _editPropertyImageSlideCollectionControl.InitialImageSlideShow = JsonConvert.SerializeObject(ImageSlideShow, new IsoDateTimeConverter());
            Controls.Add(imageSlideCollectionControl);
            _editPropertyImageSlideCollectionControl.InitialImageSlideShow = JsonConvert.SerializeObject(ImageSlideShow, new IsoDateTimeConverter());
        }

        protected SlideShow ImageSlideShow
        {
            get { return ((PropertyImageSlideCollection)PropertyData).SlideShow ?? SlideShow.Empty; }
            set { ((PropertyImageSlideCollection)PropertyData).SlideShow = value; }
        }

        public override void ApplyEditChanges()
        {
            try
            {
                if (_editPropertyImageSlideCollectionControl.UpdatedImageSlideShow.TrimmedNullOrEmpty())
                {
                    ImageSlideShow = SlideShow.Empty;
                    return;
                }

                var updatedImageSlideShow = JsonConvert.DeserializeObject<SlideShow>(_editPropertyImageSlideCollectionControl.UpdatedImageSlideShow, new IsoDateTimeConverter()) ?? SlideShow.Empty;
                updatedImageSlideShow.EnsureUniqueIds();
                ImageSlideShow = updatedImageSlideShow;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                AddErrorValidator(ex.Message);
            }
        }
    }
}

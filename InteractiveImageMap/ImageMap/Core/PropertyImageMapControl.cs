using System;
using EPiServer.Core;
using EPiServer.ImageMap.Core.Extensions;
using EPiServer.Web.PropertyControls;
using ImageSlideShow.Core.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EPiServer.ImageMap.Core
{
    public class PropertyImageMapControl : PropertyDataControl
    {
        private IPropertyImageMapViewModeControlView _propertyImageMapViewModeControlView;
        private IPropertyImageMapEditModeView _propertyImageMapEditModeView;

        public override void CreateDefaultControls()
        {
            if (string.IsNullOrEmpty(ImageMap.ImageUrl))
            {
                return;
            }
            var viewControl = Page.LoadControl("~/ImageMap/Web/ImageMapProperty/ImageMapViewModeControl.ascx");
            _propertyImageMapViewModeControlView = (IPropertyImageMapViewModeControlView)viewControl;
            _propertyImageMapViewModeControlView.ImageMap = ImageMap;
            Controls.Add(viewControl);
        }

        public override void CreateEditControls()
        {
            var imageSlideCollectionControl = Page.LoadControl("~/ImageMap/Web/ImageMapProperty/ImageMapEditModeControl.ascx");
            _propertyImageMapEditModeView = (IPropertyImageMapEditModeView)imageSlideCollectionControl;
            _propertyImageMapEditModeView.InitialImageMap = JsonConvert.SerializeObject(ImageMap, new IsoDateTimeConverter());
            Controls.Add(imageSlideCollectionControl);
            _propertyImageMapEditModeView.InitialImageMap = JsonConvert.SerializeObject(ImageMap, new IsoDateTimeConverter());
        }

        public override TableRowLayout RowLayout
        {
            get { return TableRowLayout.Wide; } // This tells EPiServer to render a wide colspan=2 layout for the control in Edit Mode.
        }

        protected Domain.ImageMap ImageMap
        {
            get { return ((PropertyImageMap)PropertyData).ImageMap ?? Domain.ImageMap.Empty; }
            set { ((PropertyImageMap)PropertyData).ImageMap = value; }
        }

        public override void ApplyEditChanges()
        {
            try
            {
                if (_propertyImageMapEditModeView.UpdatedImageMap.TrimmedNullOrEmpty())
                {
                    ImageMap = Domain.ImageMap.Empty;
                    return;
                }

                var updatedImageSlideShow = JsonConvert.DeserializeObject<Domain.ImageMap>(_propertyImageMapEditModeView.UpdatedImageMap, new IsoDateTimeConverter()) ?? Domain.ImageMap.Empty;
                updatedImageSlideShow.EnsureUniqueIds();
                ImageMap = updatedImageSlideShow;
            }
            catch (Exception ex)
            {
                AddErrorValidator(ex.Message);
            }
        }
    }
}

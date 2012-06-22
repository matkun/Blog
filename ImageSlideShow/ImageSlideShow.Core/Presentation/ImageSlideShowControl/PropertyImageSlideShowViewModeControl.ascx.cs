using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer;
using EPiServer.ClientScript;
using ImageSlideShow.Core.Framework.CustomProperties.ImageSlideShow;
using ImageSlideShow.Core.Framework.EmbeddedResources;
using ImageSlideShow.Core.Framework.IoC;

namespace ImageSlideShow.Core.Presentation.ImageSlideShowControl
{
    public partial class PropertyImageSlideShowViewModeControl : UserControlBase, IPropertyImageSlideShowViewModeControlView
    {
        private IResourceHandler _resourceHandler;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _resourceHandler = IoC.Get<IResourceHandler>();
            //_resourceHandler.AddjQuery(Page);
            _resourceHandler.AddjQueryNivoSlider(Page);
            _resourceHandler.AddNivoSliderStyleSheet(Page);

            AddThemeStylesheet();
        }

        private void AddThemeStylesheet()
        {
            if(SlideShow.SelectedTheme == "theme-default")
            {
                _resourceHandler.AddNivoSliderDefaultThemeStyleSheet(Page);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            rptImageSlides.DataSource = SlideShow.Slides;
            rptImageSlides.DataBind();

            rptHtmlCaptions.DataSource = CaptionsWithHtml();
            rptHtmlCaptions.DataBind();
        }

        private IEnumerable<ImageSlide> CaptionsWithHtml()
        {
            return from slide in SlideShow.Slides
                   where IsHtmlCaption(slide.ImageTooltip)
                   select slide;
        }

        public SlideShow SlideShow { get; set; }

        protected string UniqueIdentifier
        {
            get { return ClientScriptUtility.ToScriptSafeIdentifier(ClientID); }
        }

        protected string CaptionOrDefault(ImageSlide slide)
        {
            return IsHtmlCaption(slide.ImageTooltip) ?
                string.Concat("#", slide.SlideId.ToString()) :
                slide.ImageTooltip;
        }

        private static bool IsHtmlCaption(string caption)
        {
            if(string.IsNullOrEmpty(caption))
            {
                return false;
            }
            return caption != HttpUtility.HtmlEncode(caption);
        }

        protected bool IsLinked(ImageSlide slide)
        {
            return !string.IsNullOrEmpty(slide.ImageLinkUrl);
        }
    }
}

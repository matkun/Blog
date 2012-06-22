using System;
using EPiServer;
using ImageSlideShow.Core.Framework.EmbeddedResources;
using ImageSlideShow.Core.Framework.IoC;
using ImageSlideShow.Core.Framework.Language;

namespace ImageSlideShow.Core.Presentation.ImageSlideShowControl
{
    public partial class ImageSlideEditor : TemplatePage
    {
        public ITranslator Translator { get; private set; }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            Translator = IoC.Get<ITranslator>();

            Page.Title = Translator.Translate("/ImageSlideShow/EditModeProperty/SlideEditor/Title");
            MasterPageFile = VirtualPaths.PlugInMasterPage;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var resourceHandler = IoC.Get<IResourceHandler>();
            resourceHandler.AddjQuery(Page);
        }
    }
}

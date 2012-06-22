using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using EPiServer;
using EPiServer.ClientScript;
using ImageSlideShow.Core.Extensions;
using ImageSlideShow.Core.Framework.CustomProperties.ImageSlideShow;
using ImageSlideShow.Core.Framework.EmbeddedResources;
using ImageSlideShow.Core.Framework.IoC;
using ImageSlideShow.Core.Framework.Language;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ImageSlideShow.Core.Presentation.ImageSlideShowControl
{
    public partial class PropertyImageSlideCollectionControl : UserControlBase, IPropertyImageSlideCollectionView
    {
        protected string ImageSlideEditorUrl { get { return VirtualPaths.SlideEditor; } }

        private readonly ITranslator _translator;
        public ITranslator Translator { get { return _translator; } }

        public PropertyImageSlideCollectionControl()
        {
            _translator = IoC.Get<ITranslator>();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var resourceHandler = IoC.Get<IResourceHandler>();
            resourceHandler.AddjQueryJson(Page);
            resourceHandler.AddEditCollectionJavaScript(Page);

            rptTransitionEffects.DataSource = TransitionEffectsAsListItems();
            rptTransitionEffects.DataBind();

            rptThemes.DataSource = ThemesAsListItems();
            rptThemes.DataBind();
        }

        private static IEnumerable<ListItem> TransitionEffectsAsListItems()
        {
            var effects = Enum.GetValues(typeof (TransitionEffect)).Cast<TransitionEffect>().ToArray();
            return effects
                .Select(effect => new ListItem
                                    {
                                        Text = effect.DisplayNameOrDefault(),
                                        Value = effect.SystemIdOrDefault()
                                    });
        }

        private static IEnumerable<ListItem> ThemesAsListItems()
        {
            // Only the default theme is available for the time being. Possibility to add themes through admin mode will be added later.
            return new List<ListItem>
                       {
                           new ListItem
                               {
                                   Text = "Default",
                                   Value = "theme-default",
                                   Selected = true
                               }
                       };
        }

        protected string UniqueIdentifier
        {
            get { return ClientScriptUtility.ToScriptSafeIdentifier(ClientID); }
        }

        private string HiddenFieldId
        {
            get { return string.Concat("hidden-updated-show-", UniqueIdentifier); }
        }

        private string _initialImageSlideShow;
        public string InitialImageSlideShow
        {
            get { return _initialImageSlideShow.EscapedJson(); }
            set { _initialImageSlideShow =  Request.Form[HiddenFieldId] ?? value; }
        }

        public string UpdatedImageSlideShow
        {
            get { return Request.Form[HiddenFieldId]; }
        }
        
        protected string EmptyImageSlideShow
        {
            get { return JsonConvert.SerializeObject(SlideShow.Empty, new IsoDateTimeConverter()); }
        }

        protected string AddImageButtonText
        {
            get { return _translator.Translate("/ImageSlideShow/EditModeProperty/AddButtonText"); }
        }
    }
}

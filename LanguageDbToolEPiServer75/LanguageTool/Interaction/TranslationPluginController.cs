using System;
using System.Web.Mvc;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Shell;
using EPiServer.Shell.ViewComposition;
using EPiServer.Templates.Alloy.LanguageTool.Framework;
using EPiServer.Templates.Alloy.LanguageTool.Infrastructure;
using EPiServer.Templates.Alloy.LanguageTool.Interaction.Models;

namespace EPiServer.Templates.Alloy.LanguageTool.Interaction
{
    [IFrameComponent(
        PlugInAreas = PlugInArea.AssetsDefaultGroup,
        Url = "/customtool/translationsplugin",
        Title = "Custom translations")]
    [Authorize(Roles = "Administrators, WebAdmins, WebEditors")]
    public class TranslationsPluginController : TranslationsController<PageData>
    {
        private readonly IContentTypeRepository _contentTypeRepository;
        private readonly ITranslationService _translationService;

        public TranslationsPluginController(
            IContentTypeRepository contentTypeRepository,
            ITranslationService translationService)
        {
            if (contentTypeRepository == null) throw new ArgumentNullException("contentTypeRepository");
            if (translationService == null) throw new ArgumentNullException("translationService");
            _contentTypeRepository = contentTypeRepository;
            _translationService = translationService;
        }

        [Route("customTool/translationsplugin")]
        public ActionResult Index()
        {
            var contentType = _contentTypeRepository.Load(CurrentPage.ContentTypeID);
            var model = new SingleTranslationsViewModel
                {
                    ContentTypeName = contentType.Name,
                    Translations = _translationService.TranslationsFor(contentType.Name)
                };
            return View("~/LanguageTool/Interaction/Views/Plugin.cshtml", model);
        }

        [HttpPost]
        [Route("customtool/translationsplugin/update")]
        public ActionResult Update()
        {
            return _translationService.UpdateTranslation();
        }
    }
}

using System;
using System.Web.Mvc;
using EPiServer.Templates.Alloy.LanguageTool.Framework;
using EPiServer.Templates.Alloy.LanguageTool.Interaction.Models;

namespace EPiServer.Templates.Alloy.LanguageTool.Interaction
{
    [RoutePrefix("toolbox")]
    [Authorize(Roles = "Administrators, WebAdmins, WebEditors")]
    public class TranslationToolController : Controller
    {
        private readonly ITranslationService _translationService;

        public TranslationToolController(ITranslationService translationService)
        {
            if (translationService == null) throw new ArgumentNullException("translationService");
            _translationService = translationService;
        }

        [Route("translations")]
        public ActionResult Index()
        {
            var model = new MultipleTranslationsViewModel
                {
                    ContentTypeSpecificModels = _translationService.ModelsForAllTranslations()
                };
            return View("~/LanguageTool/Interaction/Views/Tool.cshtml", model);
        }

        [HttpPost]
        [Route("translations/update")]
        public ActionResult Update()
        {
            return _translationService.UpdateTranslation();
        }
    }
}

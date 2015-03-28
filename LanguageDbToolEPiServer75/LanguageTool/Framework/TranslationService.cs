using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPiServer.Core;
using EPiServer.Data;
using EPiServer.DataAbstraction;
using EPiServer.Templates.Alloy.LanguageTool.Domain;
using EPiServer.Templates.Alloy.LanguageTool.Infrastructure.AntiForgery;
using EPiServer.Templates.Alloy.LanguageTool.Interaction.Models;
using EPiServer.Templates.Alloy.LanguageTool.Wrappers;

namespace EPiServer.Templates.Alloy.LanguageTool.Framework
{
    public class TranslationService : ITranslationService
    {
        private readonly IAntiForgeryValidationService _antiForgeryValidationService;
        private readonly ILocalizationServiceWrapper _localizationService;
        private readonly ITranslationRepository _translationRepository;
        private readonly IContentTypeRepository _contentTypeRepository;
        private readonly HttpRequestBase _request;

        public TranslationService(
            IAntiForgeryValidationService antiForgeryValidationService,
            ILocalizationServiceWrapper localizationService,
            ITranslationRepository translationRepository,
            IContentTypeRepository contentTypeRepository,
            HttpRequestBase request)
        {
            if (antiForgeryValidationService == null) throw new ArgumentNullException("antiForgeryValidationService");
            if (localizationService == null) throw new ArgumentNullException("localizationService");
            if (translationRepository == null) throw new ArgumentNullException("translationRepository");
            if (request == null) throw new ArgumentNullException("request");
            _antiForgeryValidationService = antiForgeryValidationService;
            _localizationService = localizationService;
            _translationRepository = translationRepository;
            _contentTypeRepository = contentTypeRepository;
            _request = request;
        }

        public JsonResult UpdateTranslation()
        {
            if (!_antiForgeryValidationService.IsValidToken())
            {
                return WithErrorMessage(_localizationService.GetString("/plugins/languagetool/errors/antiforgery"));
            }

            Identity id;
            if (!Identity.TryParse(_request.Form["id"] ?? string.Empty, out id))
            {
                return WithErrorMessage(_localizationService.GetString("/plugins/languagetool/errors/notguid"));
            }

            var text = _request.Form["text"] ?? string.Empty;
            Translation updatedTranslation;
            if (!_translationRepository.UpdateTranslation(id.ExternalId, text, out updatedTranslation))
            {
                return WithErrorMessage(_localizationService.GetString("/plugins/languagetool/errors/notranslation"));
            }

            return Json(
                new
                {
                    Success = true,
                    Message = _localizationService.GetString("/plugins/languagetool/success"),
                    Updated = updatedTranslation.Updated.ToString("yyyy-MM-dd HH:mm:ss"),
                    UpdatedBy = updatedTranslation.UpdatedBy
                }
            );
        }

        public SingleTranslationsViewModel[] ModelsForAllTranslations()
        {
            var models = new List<SingleTranslationsViewModel>();
            var typeNames = _contentTypeRepository.List().Select(t => t.Name).ToArray();

            foreach (var name in typeNames)
            {
                var translations = _translationRepository.TranslationsFor(name, ignoreLanguage: false).ToArray();
                if (!translations.Any())
                {
                    continue;
                }
                var innerModel = new SingleTranslationsViewModel
                {
                    ContentTypeName = name,
                    Translations = translations
                };
                models.Add(innerModel);
            }
            models.Add(GlobalTranslationsModel());
            return models.ToArray();
        }

        public Translation[] TranslationsFor(string contentTypeName)
        {
            return _translationRepository.TranslationsFor(contentTypeName, ignoreLanguage: false);
        }

        private SingleTranslationsViewModel GlobalTranslationsModel()
        {
            return new SingleTranslationsViewModel
            {
                // Translations not belonging to a specific content type are persisted as type ContentData.
                ContentTypeName = _localizationService.GetString("/plugins/languagetool/globaltranslations"),
                Translations = _translationRepository.TranslationsFor(typeof(ContentData).Name, ignoreLanguage: false)
            };
        }

        private static JsonResult WithErrorMessage(string message)
        {
            return Json(new { Success = false, Message = message });
        }

        private static JsonResult Json(object data)
        {
            // System.Web.Mvc.Controller implementation of 'JsonResult Json(object data)'.
            return new JsonResult
            {
                Data = data, ContentType = null, ContentEncoding = null, JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }
    }
}

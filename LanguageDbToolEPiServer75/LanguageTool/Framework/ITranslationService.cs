using System.Web.Mvc;
using EPiServer.Templates.Alloy.LanguageTool.Domain;
using EPiServer.Templates.Alloy.LanguageTool.Interaction.Models;

namespace EPiServer.Templates.Alloy.LanguageTool.Framework
{
    public interface ITranslationService
    {
        JsonResult UpdateTranslation();
        SingleTranslationsViewModel[] ModelsForAllTranslations();
        Translation[] TranslationsFor(string contentTypeName);
    }
}

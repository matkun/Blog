using EPiServer.Templates.Alloy.LanguageTool.Domain;

namespace EPiServer.Templates.Alloy.LanguageTool.Interaction.Models
{
    public class SingleTranslationsViewModel
    {
        public string ContentTypeName { get; set; }
        public Translation[] Translations { get; set; }
    }
}

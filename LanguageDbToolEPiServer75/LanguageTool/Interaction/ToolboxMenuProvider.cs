using System;
using System.Collections.Generic;
using EPiServer.Security;
using EPiServer.Shell.Navigation;
using EPiServer.Templates.Alloy.LanguageTool.Wrappers;

namespace EPiServer.Templates.Alloy.LanguageTool.Interaction
{
    [MenuProvider]
    public class ToolboxMenuProvider : IMenuProvider
    {
        private readonly ILocalizationServiceWrapper _localizationService;
        public ToolboxMenuProvider(ILocalizationServiceWrapper localizationService)
        {
            if (localizationService == null) throw new ArgumentNullException("localizationService");
            _localizationService = localizationService;
        }

        public IEnumerable<MenuItem> GetMenuItems()
        {
            var label = _localizationService.GetString("/globalmenu/toolbox/label");
            var toolboxLabel = string.IsNullOrWhiteSpace(label) ? "Toolbox" : label;
            var toolbox = new SectionMenuItem(toolboxLabel, "/global/toolbox")
            {
                IsAvailable = (request) => PrincipalInfo.HasEditAccess
            };

            label = _localizationService.GetString("/globalmenu/toolbox/translations/label");
            var translationsLabel = string.IsNullOrWhiteSpace(label) ? "Translations" : label;
            var translationTool = new UrlMenuItem(translationsLabel, "/global/toolbox/translations", "/toolbox/translations")
            {
                IsAvailable = (request) => PrincipalInfo.HasEditAccess
            };
            return new MenuItem[] { toolbox, translationTool };
        }
    }
}

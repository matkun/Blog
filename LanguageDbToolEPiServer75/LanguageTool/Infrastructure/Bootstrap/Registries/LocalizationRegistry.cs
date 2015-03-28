using EPiServer.Templates.Alloy.LanguageTool.Framework;
using EPiServer.Templates.Alloy.LanguageTool.Wrappers;
using StructureMap.Configuration.DSL;

namespace EPiServer.Templates.Alloy.LanguageTool.Infrastructure.Bootstrap.Registries
{
    public class LocalizationRegistry : Registry
    {
        public LocalizationRegistry()
        {
            For<ILocalizationServiceWrapper>()
                .Use<LocalizationServiceDecorator>()
                .Ctor<ILocalizationServiceWrapper>().Is<LocalizationServiceWrapper>();
        }
    }
}

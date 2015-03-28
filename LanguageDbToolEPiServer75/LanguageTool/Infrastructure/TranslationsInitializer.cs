using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Templates.Alloy.LanguageTool.Framework;
using EPiServer.Templates.Alloy.LanguageTool.Infrastructure.Bootstrap;
using EPiServer.Templates.Alloy.LanguageTool.Infrastructure.Configuration;
using EPiServer.Templates.Alloy.LanguageTool.Resources;

namespace EPiServer.Templates.Alloy.LanguageTool.Infrastructure
{
    //  TODO - You may change the behaviour of this initializer by adding the following to your web.config transforms.
    //  TODO - It is only necessary to run the initializer the first time after a new deploy with added/removed entries.
    //  <configuration>
    //    <configSections>
    //      <section name="LanguageToolConfiguration"
    //               type="{your project}.LanguageTool.Infrastructure.Configuration.Configuration, {your assembly}"/>
    //  </configSections>
    //  <LanguageToolConfiguration InitializeNewTranslations="True"
    //                             RemoveOrphanTranslations="False" />

    [ModuleDependency(typeof(StructureMapInitializer))]
    [InitializableModule]
    public class TranslationsInitializer : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var translationRepository = ServiceLocator.Current.GetInstance<ITranslationRepository>();
            var translationResources = ServiceLocator.Current.GetAllInstances<IInitialTranslations>();
            var configuration = ServiceLocator.Current.GetInstance<ILanguageToolConfigurations>();
            
            foreach (var resource in translationResources)
            {
                if (configuration.InitializeNewTranslations)
                {
                    translationRepository.InitializeTranslationsFrom(resource);
                }
                if(configuration.RemoveOrphanTranslations)
                {
                    translationRepository.RemoveOrphansFrom(resource);
                }
            }
        }
        public void Uninitialize(InitializationEngine context) { }
        public void Preload(string[] parameters) { }
    }
}

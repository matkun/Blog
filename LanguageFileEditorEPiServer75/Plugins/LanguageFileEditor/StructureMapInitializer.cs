using System.Web.Mvc;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace EPiServer.Templates.Alloy.Plugins.LanguageFileEditor
{
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    [InitializableModule]
    public class StructureMapInitializer : IConfigurableModule
    {
        void IConfigurableModule.ConfigureContainer(ServiceConfigurationContext context)
        {
            var container = context.Container;
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(context.Container));

            container.Configure(x => x.Scan(s =>
                {
                    s.AssemblyContainingType<StructureMapInitializer>();
                    s.WithDefaultConventions();
                    s.LookForRegistries();
                }));
        }

        void IInitializableModule.Initialize(InitializationEngine context) { }
        void IInitializableModule.Preload(string[] parameters) { }
        void IInitializableModule.Uninitialize(InitializationEngine context) { }
    }
}

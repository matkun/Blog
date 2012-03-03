using System.Web;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace EPiServerBuiltInProperties.CustomizeAppearances.UsingInitializationModule
{
    [InitializableModule]
    public class AppearanceInitializationModule : IInitializableHttpModule
    {
        public void Initialize(InitializationEngine context) {}
        public void Uninitialize(InitializationEngine context) {}
        public void Preload(string[] parameters) {}

        public void InitializeHttpEvents(HttpApplication application)
        {
            PropertyControlClassFactory
                .Instance
                .RegisterClass(typeof(PropertyString),
                               typeof(RenderingChangedAppearance));
        }
    }
}

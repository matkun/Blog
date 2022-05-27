using EPiServer.Framework.TypeScanner;
using EPiServer.ServiceLocation;
using StructureMap;

namespace RemoteEventsTester
{
    internal class Initializer
    {
        public static void InitializeContainer()
        {
            var container = new Container();
            container.Configure(c => _ = c.For<ITypeScannerLookup>().Use<TypeScannerLookup>());

            var locator = new StructureMapServiceLocator(container);
            ServiceLocator.SetLocator(locator);
        }
    }
}

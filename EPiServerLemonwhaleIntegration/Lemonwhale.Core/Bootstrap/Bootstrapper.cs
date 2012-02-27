using StructureMap;
using StructureMap.Configuration.DSL;

namespace Lemonwhale.Core.Bootstrap
{
    public class Bootstrapper : IBootstrapper
    {
        public IContainer Container;

        public void BootstrapStructureMap()
        {
            Container = new Container(AddRegistryInfo);
        }

        private static void AddRegistryInfo(IRegistry registry)
        {
            registry.Scan(scanner =>
            {
                scanner.AssemblyContainingType<Bootstrapper>();
                scanner.LookForRegistries();
            });
        }
    }
}

using Lemonwhale.Core.Presentation;
using StructureMap.Configuration.DSL;

namespace Lemonwhale.Core.Bootstrap.Registries
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            Scan(scanner =>
            {
                scanner.RegisterConcreteTypesAgainstTheFirstInterface();
                scanner.AssemblyContainingType<PresenterBase>();
                scanner.WithDefaultConventions();
                scanner.SingleImplementationsOfInterface();
            });
        }
    }
}

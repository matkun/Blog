using PageTypeTreeFilter.Presentation;
using StructureMap.Configuration.DSL;

namespace PageTypeTreeFilter.Bootstrap.Registries
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

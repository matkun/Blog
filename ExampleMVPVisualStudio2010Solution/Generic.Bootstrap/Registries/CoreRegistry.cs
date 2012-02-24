using Generic.Core.Presentation;
using StructureMap.Configuration.DSL;

namespace Generic.Bootstrap.Registries
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

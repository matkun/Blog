//using Generic.Core.Presentation;

using StructureMap.Configuration.DSL;

namespace XFlow.Core.Bootstrap.Registries
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            Scan(scanner =>
            {
                scanner.RegisterConcreteTypesAgainstTheFirstInterface();
                //scanner.AssemblyContainingType<PresenterBase>();
                scanner.AssemblyContainingType<Bootstrapper>();
                scanner.WithDefaultConventions();
                scanner.SingleImplementationsOfInterface();
            });
        }
    }
}

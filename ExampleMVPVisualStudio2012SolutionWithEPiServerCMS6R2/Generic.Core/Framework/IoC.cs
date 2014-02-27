using System;
using StructureMap;
using StructureMap.Pipeline;

namespace Generic.Core.Framework
{
    public class IoC
    {
        public static IContainer Container;

        public static void Reset()
        {
            if (Container == null)
            {
                return;
            }
            Container.Dispose();
            Container = null;
        }
        
        public static TPresenter GetPresenter<TPresenter>(object view)
        {
            var explicitArguments = new ExplicitArguments();
            foreach (var implementedInterface in view.GetType().GetInterfaces())
            {
                explicitArguments.Set(implementedInterface, view);
            }
            return Container.GetInstance<TPresenter>(explicitArguments);
        }

        public static T Get<T>(params object[] extraParameters)
        {
            if(Container == null) throw new InvalidOperationException("The container was not initialized before it was accessed.");
            return Container.GetInstance<T>(CreateExplicitArguments(extraParameters));
        }

        private static ExplicitArguments CreateExplicitArguments(params object[] extraParameters)
        {
            var args = new ExplicitArguments();
            foreach(var parameter in extraParameters)
            {
                args.Set(parameter.GetType(), parameter);
            }
            return args;
        }
    }
}

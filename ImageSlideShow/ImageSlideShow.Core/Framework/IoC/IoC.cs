using System;
using StructureMap;
using StructureMap.Pipeline;

namespace ImageSlideShow.Core.Framework.IoC
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

        public static T Get<T>()
        {
            if (Container == null)
            {
                throw new InvalidOperationException("You must initialize the Container before accessing it.");
            }
            return Container.GetInstance<T>();
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
    }
}

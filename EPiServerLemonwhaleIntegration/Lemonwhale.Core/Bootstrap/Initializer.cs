using System.Web;
using Lemonwhale.Core.Framework.IoC;

namespace Lemonwhale.Core.Bootstrap
{
    public class Initializer : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.BootstrapStructureMap();
            IoC.Container = bootstrapper.Container;
        }

        public void Dispose()
        {
        }
    }
}

using System.Web;
using PageTypeTreeFilter.Framework.IoC;

namespace PageTypeTreeFilter.Bootstrap
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

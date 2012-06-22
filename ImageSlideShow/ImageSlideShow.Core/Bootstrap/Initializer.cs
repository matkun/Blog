using System.Web;
using ImageSlideShow.Core.Framework.IoC;

namespace ImageSlideShow.Core.Bootstrap
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

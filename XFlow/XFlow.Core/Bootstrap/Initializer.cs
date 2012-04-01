using System.Web;
using XFlow.Core.Framework.IoC;

namespace XFlow.Core.Bootstrap
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
//<add name="XFlowInitializer" type="XFlow.Core.Bootstrap.Initializer" />
using System.Web;
using StructureMap.Configuration.DSL;

namespace XFlow.Core.Bootstrap.Registries
{
    public class SystemRegistry : Registry
    {
        public SystemRegistry()
        {
            For<HttpContextBase>().Use(() => (HttpContextBase)new HttpContextWrapper(HttpContext.Current));
            For<HttpRequestBase>().Use(c => c.GetInstance<HttpContextBase>().Request);
            For<HttpResponseBase>().Use(c => c.GetInstance<HttpContextBase>().Response);
        }
    }
}

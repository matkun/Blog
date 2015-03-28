using System.Web;
using StructureMap.Configuration.DSL;

namespace EPiServer.Templates.Alloy.LanguageTool.Infrastructure.Bootstrap.Registries
{
    public class SystemWebRegistry : Registry
    {
        public SystemWebRegistry()
        {
            For<HttpContextBase>().Use(() => HttpContext.Current != null ? (HttpContextBase)new HttpContextWrapper(HttpContext.Current) : new NullHttpContext());
            For<HttpRequestBase>().Use(c => c.GetInstance<HttpContextBase>().Request);
            For<HttpResponseBase>().Use(c => c.GetInstance<HttpContextBase>().Response);
        }
    }
}

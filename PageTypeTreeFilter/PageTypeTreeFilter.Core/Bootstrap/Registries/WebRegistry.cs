using System.Web;
using StructureMap.Configuration.DSL;

namespace PageTypeTreeFilter.Bootstrap.Registries
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            For<HttpContextBase>()
                .Use(() => (HttpContextBase)new HttpContextWrapper(HttpContext.Current));
        }
    }
}

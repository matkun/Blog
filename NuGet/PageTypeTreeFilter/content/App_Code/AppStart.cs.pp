using System.Web.Hosting;
using PageTypeTreeFilter.Framework.EmbeddedResources;

namespace $rootnamespace$
{
    public static class AppStart
    {
        public static void AppInitialize()
        {
            HostingEnvironment.RegisterVirtualPathProvider(new EmbeddedResourceProvider());
        }
    }
}

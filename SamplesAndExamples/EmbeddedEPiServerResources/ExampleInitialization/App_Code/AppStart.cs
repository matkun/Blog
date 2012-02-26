using System.Web.Hosting;
using EmbeddedEPiServerResources.Core;

namespace MyProject
{
  public static class AppStart
  {
    public static void AppInitialize()
    {
	  HostingEnvironment.RegisterVirtualPathProvider(new EmbeddedResourceProvider());
    } 
  }
}

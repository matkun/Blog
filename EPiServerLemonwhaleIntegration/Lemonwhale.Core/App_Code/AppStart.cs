using System.Web.Hosting;
using Lemonwhale.Core.Framework.EmbeddedResources;

// This is an example file, real one in web project.
namespace Lemonwhale.Core
{
  public static class AppStart
  {
    public static void AppInitialize()
    {
	  HostingEnvironment.RegisterVirtualPathProvider(new EmbeddedResourceProvider());
    } 
  }
}

// Add to web.config
// <profile enabled="true" defaultProvider="SqlProfile" automaticSaveEnabled="true">
//    <properties>
//       <add name="LemonwhalePersonalApiKey" type="System.String" />
//    </properties>
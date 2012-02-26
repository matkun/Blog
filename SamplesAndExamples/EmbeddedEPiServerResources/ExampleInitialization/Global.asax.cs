using System;
using System.Web.Hosting;
using EmbeddedEPiServerResources.Core;

namespace MyProject.Web
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
			HostingEnvironment.RegisterVirtualPathProvider(new EmbeddedResourceProvider());
        }

        void Application_End(object sender, EventArgs e)
        {
        }

        void Application_Error(object sender, EventArgs e)
        {
        }

        void Session_Start(object sender, EventArgs e)
        {
        }

        void Session_End(object sender, EventArgs e)
        {
        }
    }
}

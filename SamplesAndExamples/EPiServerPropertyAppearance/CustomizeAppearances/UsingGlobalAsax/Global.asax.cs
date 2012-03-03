using System;
using EPiServer.Core;

namespace EPiServerBuiltInProperties.CustomizeAppearances.UsingGlobalAsax
{
    public class Global : EPiServer.Global
    {
        public override void Init()
        {
            PropertyControlClassFactory
                .Instance
                .RegisterClass(typeof(PropertyString),
                               typeof(RenderingChangedAppearance));
            base.Init();
        }

        void Application_Start(object sender, EventArgs e)
        {
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

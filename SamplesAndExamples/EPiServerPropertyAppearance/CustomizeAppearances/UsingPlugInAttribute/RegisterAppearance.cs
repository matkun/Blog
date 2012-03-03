using EPiServer.Core;
using EPiServer.PlugIn;

namespace EPiServerBuiltInProperties.CustomizeAppearances.UsingPlugInAttribute
{
    public class RegisterAppearance : PlugInAttribute
    {
        public static void Start()
        {
            PropertyControlClassFactory
                .Instance
                .RegisterClass(typeof(PropertyString),
                               typeof(RenderingChangedAppearance));
        }
    }
}

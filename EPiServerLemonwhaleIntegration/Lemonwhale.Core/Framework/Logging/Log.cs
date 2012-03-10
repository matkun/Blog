using log4net;

namespace Lemonwhale.Core.Framework.Logging
{
    public static class Log
    {
        public static ILog For<T>()
        {
            return LogManager.GetLogger(typeof (T).FullName);
        }

        public static ILog For<T>(T o)
        {
            return For<T>();
        }
        //private static void SetupHttpContextProperties()
        //{
        //    if (HttpContext.Current == null) return;
        //    ThreadContext.Properties["UA"] = HttpContext.Current.Request.UserAgent;
        //    ThreadContext.Properties["Referrer"] = HttpContext.Current.Request.UrlReferrer;
        //    ThreadContext.Properties["Url"] = HttpContext.Current.Request.Url;
        //    ThreadContext.Properties["IP"] = HttpContext.Current.Request.UserHostAddress;
        //    ThreadContext.Properties["Method"] = HttpContext.Current.Request.HttpMethod;
        //    ThreadContext.Properties["Path"] = HttpContext.Current.Request.Path;
        //}
    }
}

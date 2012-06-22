using log4net;

namespace ImageSlideShow.Core.Framework.Logging
{
    public static class Log
    {
        public static ILog For<T>()
        {
            return LogManager.GetLogger(typeof(T).FullName);
        }

        public static ILog For<T>(T o)
        {
            return For<T>();
        }
    }
}

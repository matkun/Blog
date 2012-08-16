using System;

namespace EPiServer.ImageMap.Core.Extensions
{
    public static class GuidExtensions
    {
        public static bool IsNullOrEmpty(this Guid? guid)
        {
            return guid == null || guid == Guid.Empty;
        }
    }
}

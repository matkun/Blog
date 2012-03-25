using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace PageTypeTreeFilter.Framework.EmbeddedResources
{
    public class EmbeddedResourceProvider : VirtualPathProvider
    {
        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            return IsEmbeddedResourcePath(virtualPath) ?
                null :
                base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        public override bool FileExists(string virtualPath)
        {
            return IsEmbeddedResourcePath(virtualPath) || base.FileExists(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            return IsEmbeddedResourcePath(virtualPath) ?
                new VirtualResourceFile(virtualPath) :
                base.GetFile(virtualPath);
        }

        private static bool IsEmbeddedResourcePath(string virtualPath)
        {
            var path = VirtualPathUtility.ToAppRelative(virtualPath);
            return path.StartsWith("~/PageTypeTreeFilterResource/", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
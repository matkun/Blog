using System;
using System.IO;
using System.Web;
using System.Web.Hosting;
using ImageSlideShow.Core.Framework.Logging;
using log4net;

namespace ImageSlideShow.Core.Framework.EmbeddedResources
{
    public class VirtualResourceFile : VirtualFile
    {
        readonly string _path;
        private readonly ILog _log;

        public VirtualResourceFile(string virtualPath)
            : base(virtualPath)
        {
            _path = VirtualPathUtility.ToAppRelative(virtualPath);
            _log = Log.For(this);
        }

        public override Stream Open()
        {
            var pathArray = _path.Split('/');
            if (pathArray.Length < 3)
            {
                var message = string.Format("Unable to split virtual path '{0}' retrieving resource and assembly information.", _path);
                _log.Error(message);
                throw new Exception(message);
            }
            var resource = pathArray[3];
            var assemblyFile = Path.Combine(HttpRuntime.BinDirectory, pathArray[2]);

            var assembly = System.Reflection.Assembly.LoadFile(assemblyFile);
            var stream = assembly.GetManifestResourceStream(resource);
            if (stream == null)
            {
                var message = string.Format("Unable to get resource '{0}' manifest resource stream from assembly '{1}'.", resource, assemblyFile);
                _log.Error(message);
                throw new Exception(message);
            }
            return stream;
        }
    }
}

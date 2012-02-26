using System;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace EmbeddedEPiServerResources.Core
{
    public class VirtualResourceFile : VirtualFile
    {
        readonly string _path;

        public VirtualResourceFile(string virtualPath) : base(virtualPath)
        {
            _path = VirtualPathUtility.ToAppRelative(virtualPath);
        }

        public override Stream Open()
        {
            var pathArray = _path.Split('/');
            var resource = pathArray[3];
            var assemblyFile = Path.Combine(HttpRuntime.BinDirectory, pathArray[2]);

            var assembly = System.Reflection.Assembly.LoadFile(assemblyFile);
            var stream = assembly.GetManifestResourceStream(resource);
            if(stream == null)
            {
                throw new Exception(string.Format("Unable to get resource '{0}' manifest resource stream from assembly '{1}'.", resource, assemblyFile));
            }
            return stream;
        }
    }
}

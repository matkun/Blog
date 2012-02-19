using System;
using System.IO;
using System.Drawing;
using System.Web.Hosting;
using EPiServer.Web.Hosting;
using EPiServer.Web;

namespace Base64EmbeddedImage
{
    public class ImageEmbeddedBase64 : System.Web.UI.WebControls.Image
    {
        public override string ImageUrl
        {
            get { return base.ImageUrl; }
            set { base.ImageUrl = GetBase64EncodedImageFromVpp(value); }
        }

        private static string GetBase64EncodedImageFromVpp(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return string.Empty;

            var unifiedFile = HostingEnvironment.VirtualPathProvider.GetFile(imageUrl) as UnifiedFile;
            if (unifiedFile == null) return string.Empty;
            var mimeMapping = MimeMapping.GetMimeMapping(unifiedFile.Name);

            var memoryStream = new MemoryStream();
            using (var stream = unifiedFile.Open())
            {
                var image = Image.FromStream(stream);
                image.Save(memoryStream, image.RawFormat);
            }
            var base64String = Convert.ToBase64String(memoryStream.ToArray());

            return string.Format("data:{0};base64,{1}", mimeMapping, base64String);
        }
    }
}
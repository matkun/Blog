using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Lemonwhale.Core.Framework.Lemonwhale
{
    public class ListService : IListService
    {
        public IEnumerable<LemonwhaleMedia> GetMedia()
        {
            var request = WebRequest.Create(string.Format(Configuration.Lemonwhale.ListBaseUrl, Configuration.Lemonwhale.SiteId));
            request.ContentType = "application/json; charset=utf-8";
            string text;
            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }


            return Enumerable.Empty<LemonwhaleMedia>();
        }
    }

    public interface IListService
    {
        IEnumerable<LemonwhaleMedia> GetMedia();
    }
}


//public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
//        {
//            if (!controllerContext.HttpContext.Request.ContentType.StartsWith("application/json"))
//            {
//                throw new InvalidOperationException("Can only handle Content-Type: application/json");
//            }

//            using (var reader = new StreamReader(controllerContext.HttpContext.Request.InputStream))
//            {
//                return new JavaScriptSerializer().Deserialize<T>(reader.ReadToEnd());
//            }
//        }
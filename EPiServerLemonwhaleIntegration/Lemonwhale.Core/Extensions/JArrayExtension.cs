using Newtonsoft.Json.Linq;

namespace Lemonwhale.Core.Extensions
{
    public static class JArrayExtension
    {
        public static bool IsNullOrEmpty(this JArray jArray)
        {
            return jArray == null || jArray.Count <= 0;
        }
    }
}

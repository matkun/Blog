using System.Collections.Generic;
using System.Linq;

namespace Lemonwhale.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static string ToQueryString(this Dictionary<string, string> queryCollection)
        {
            if(queryCollection.IsNullOrEmpty())
            {
                return string.Empty;
            }
            var query = queryCollection
                .Aggregate(string.Empty,
                    (current, parameterPair) => string.Format("{0}&{1}={2}", current, parameterPair.Key, parameterPair.Value));
            return query.TrimStart('&');
        }

        public static bool IsNullOrEmpty(this Dictionary<string, string> dictionary)
        {
            return dictionary == null || dictionary.Count <= 0;
        }
    }
}

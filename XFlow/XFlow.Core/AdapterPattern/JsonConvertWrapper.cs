using Newtonsoft.Json;

namespace XFlow.Core.AdapterPattern
{
    public class JsonConvertWrapper : IJsonConvertWrapper
    {
        public string SerializeObject(object value, params JsonConverter[] converters)
        {
            return JsonConvert.SerializeObject(value, converters);
        }

        public T DeserializeObject<T>(string value, params JsonConverter[] converters)
        {
            return JsonConvert.DeserializeObject<T>(value, converters);
        }
    }
}

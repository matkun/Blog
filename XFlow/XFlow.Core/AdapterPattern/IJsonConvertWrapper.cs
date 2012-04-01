using Newtonsoft.Json;

namespace XFlow.Core.AdapterPattern
{
    public interface IJsonConvertWrapper
    {
        string SerializeObject(object value, params JsonConverter[] converters);
        T DeserializeObject<T>(string value, params JsonConverter[] converters);
    }
}

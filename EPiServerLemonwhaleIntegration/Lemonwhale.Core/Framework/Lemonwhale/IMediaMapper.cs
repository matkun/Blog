using Newtonsoft.Json.Linq;

namespace Lemonwhale.Core.Framework.Lemonwhale
{
    public interface IMediaMapper
    {
        LemonwhaleMedia Map(JObject media);
    }
}

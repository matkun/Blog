using System.Collections.Generic;
using System.Linq;

namespace Lemonwhale.Core.Framework.Lemonwhale
{
    public class ListService : IListService
    {
        public IEnumerable<LemonwhaleMedia> GetMedia()
        {
            return Enumerable.Empty<LemonwhaleMedia>();
        }
    }

    public interface IListService
    {
        IEnumerable<LemonwhaleMedia> GetMedia();
    }
}

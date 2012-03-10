using Lemonwhale.Core.Framework.CustomAttributes;

namespace Lemonwhale.Core.DataAccess.Lemonwhale
{
    public enum SortOrder
    {
        [StringValue("asc")]
        Ascending,
        [StringValue("desc")]
        Descending
    }
}

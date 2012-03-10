using Lemonwhale.Core.Framework.CustomAttributes;

namespace Lemonwhale.Core.DataAccess.Lemonwhale
{
    public enum SortBy
    {
        [StringValue("created_at")]
        Created,
        [StringValue("duration")]
        Duration,
        [StringValue("name")]
        Name,
        [StringValue("views")]
        NumberOfViews,
        [StringValue("published_at")]
        Published
    }
}

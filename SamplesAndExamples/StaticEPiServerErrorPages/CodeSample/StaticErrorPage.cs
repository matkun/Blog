using EPiServer.SpecializedProperties;
using PageTypeBuilder;

namespace EPiServer.CodeSample
{
    [PageType("7C6EA22F-3F3A-4986-8B0B-4A352F27B3B1",
        Filename = "~/CodeSample/StaticError.aspx",
        Name = "Error page")]
    public class StaticErrorPage : TypedPageData
    {
        [PageTypeProperty(
            Type = typeof (PropertyXhtmlString),
            EditCaption = "Error message")]
        public virtual string ErrorMessage { get; set; }
    }
}

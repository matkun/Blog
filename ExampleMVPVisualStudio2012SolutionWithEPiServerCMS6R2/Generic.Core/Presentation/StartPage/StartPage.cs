using EPiServer.Core;
using PageTypeBuilder;

namespace Generic.Core.Presentation.StartPage
{
    [PageType("A6CB556A-E5C1-4C3D-97B8-F682F9C7D558",
        Name = "Start page",
        Description = "This is a start page",
        Filename = "/Default.aspx",
        SortOrder = 5
    )]
    public class StartPage : PageTypeBuilder.TypedPageData
    {
        [PageTypeProperty(
            Type = typeof(PropertyString),
            EditCaption = "Text",
            HelpText = "Some text",
            SortOrder = 10)]
        public virtual string Text { get; set; }
    }
}

using EPiServer.SpecializedProperties;
using PageTypeBuilder;

namespace EPiServer.CodeSample
{
    [PageType("213F3885-B484-404B-8171-58C4DF91DBA0",
        Filename = "~/CodeSample/EmbeddedImage.aspx",
        Name = "Embedded image page")]
    public class EmbeddedImagePage : TypedPageData
    {
        [PageTypeProperty(
            Type = typeof (PropertyImageUrl),
            EditCaption = "Image to embed")]
        public virtual string ImageToEmbed { get; set; }
    }
}

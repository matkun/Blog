using System.ComponentModel;

namespace MyProject.Core.Domain
{
    public enum TextPosition
    {
        [Description("Display text <strong>above</strong> the image.")]
        Above = 0,

        [Description("Display text <strong>below</strong> the image.")]
        Below = 1,

        [Description("Display text to the <strong>right</strong> of the image.")]
        Right = 2,

        [Description("Display text to the <strong>left</strong> of the image.")]
        Left = 3,
    }
}

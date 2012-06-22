using ImageSlideShow.Core.Framework.CustomAttributes;

namespace ImageSlideShow.Core.Framework.CustomProperties.ImageSlideShow
{
    public enum TransitionEffect
    {
        [EnumExtension(DisplayName = "Slice downwards", SystemId = "sliceDown")]
        SliceDown,

        [EnumExtension(DisplayName = "Slice downwards and left", SystemId = "sliceDownLeft")]
        SliceDownLeft,

        [EnumExtension(DisplayName = "Slice upwards", SystemId = "sliceUp")]
        SliceUp,

        [EnumExtension(DisplayName = "Slice upwards and left", SystemId = "sliceUpLeft")]
        SliceUpLeft,

        [EnumExtension(DisplayName = "Slice upwards and downwards", SystemId = "sliceUpDown")]
        SliceUpDown,

        [EnumExtension(DisplayName = "Slice upwards, downwards and left", SystemId = "sliceUpDownLeft")]
        SliceUpDownLeft,

        [EnumExtension(DisplayName = "Fold", SystemId = "fold")]
        Fold,

        [EnumExtension(DisplayName = "Fade", SystemId = "fade")]
        Fade,

        [EnumExtension(DisplayName = "Random effect", SystemId = "random")]
        Random,

        [EnumExtension(DisplayName = "Slide to the right", SystemId = "slideInRight")]
        SlideInRight,

        [EnumExtension(DisplayName = "Slide to the left", SystemId = "slideInLeft")]
        SlideInLeft,

        [EnumExtension(DisplayName = "Random box effect", SystemId = "boxRandom")]
        BoxRandom,

        [EnumExtension(DisplayName = "Rain box effect", SystemId = "slicboxRaineDown")]
        BoxRain,

        [EnumExtension(DisplayName = "Reversed rain box effect", SystemId = "boxRainReverse")]
        BoxRainReverse,

        [EnumExtension(DisplayName = "Growing rain box effect", SystemId = "boxRainGrow")]
        BoxRainGrow,

        [EnumExtension(DisplayName = "Reverse growing rain box effect", SystemId = "boxRainGrowReverse")]
        BoxRainGrowReverse,
    }
}

using System;
using System.Collections.Generic;
using ImageSlideShow.Core.Extensions;

namespace ImageSlideShow.Core.Framework.CustomProperties.ImageSlideShow
{
    [Serializable]
    public class SlideShow
    {
        public SlideShow()
        {
            Slides = new List<ImageSlide>();
        }

        public Guid? SlideShowId { get; set; }
        public List<ImageSlide> Slides { get; set; }
        
        public string SelectedTheme { get; set; }
        public string SelectedTransitionEffect { get; set; }
        public string PreviousText { get; set; }
        public string NextText { get; set; }

        public string TransitionSpeed { get; set; }
        public string SlidePause { get; set; }
        public string StartSlideIndex { get; set; }
        public string NumberOfSlices { get; set; }
        public string NumberOfBoxColumns { get; set; }
        public string NumberOfBoxRows { get; set; }

        public bool UseDirectionNavigation { get; set; }
        public bool HideDirectionNavigation { get; set; }
        public bool UseIndexedNavigation { get; set; }
        public bool UseThumbnailsForIndexedNavigation { get; set; }
        public bool PauseOnHover { get; set; }
        public bool AllowManualTransition { get; set; }
        public bool RandomStart { get; set; }

        public static SlideShow Empty
        {
            get
            {
                return new SlideShow
                           {
                               SelectedTheme = "theme-default",
                               SelectedTransitionEffect = TransitionEffect.Random.SystemIdOrDefault(),
                               PreviousText = "Prev",
                               NextText = "Next",

                               TransitionSpeed = "500",
                               SlidePause = "3000",
                               StartSlideIndex = "0",
                               NumberOfSlices = "15",
                               NumberOfBoxColumns = "8",
                               NumberOfBoxRows = "4",

                               UseDirectionNavigation = true,
                               HideDirectionNavigation = true,
                               UseIndexedNavigation = true,
                               UseThumbnailsForIndexedNavigation = false,
                               PauseOnHover = true,
                               AllowManualTransition = false,
                               RandomStart = false
                           };
            }
        }
    }
}

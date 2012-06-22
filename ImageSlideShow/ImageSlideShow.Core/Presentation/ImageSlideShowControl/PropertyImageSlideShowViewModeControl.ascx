<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PropertyImageSlideShowViewModeControl.ascx.cs" Inherits="ImageSlideShow.Core.Presentation.ImageSlideShowControl.PropertyImageSlideShowViewModeControl" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="ImageSlideShow.Core.Framework.CustomProperties.ImageSlideShow" %>

<script type="text/javascript">
    $(window).load(function () {
        $('#slider-<%= UniqueIdentifier %>').nivoSlider({
            effect: '<%= SlideShow.SelectedTransitionEffect %>',
            slices: <%= SlideShow.NumberOfSlices %>,
            boxCols: <%= SlideShow.NumberOfBoxColumns %>,
            boxRows: <%= SlideShow.NumberOfBoxRows %>,
            animSpeed: <%= SlideShow.TransitionSpeed %>,
            pauseTime: <%= SlideShow.SlidePause %>,
            startSlide: <%= SlideShow.StartSlideIndex %>,
            directionNav: <%= SlideShow.UseDirectionNavigation.ToString(CultureInfo.InvariantCulture).ToLower() %>,
            directionNavHide: <%= SlideShow.HideDirectionNavigation.ToString(CultureInfo.InvariantCulture).ToLower() %>,
            controlNav: <%= SlideShow.UseIndexedNavigation.ToString(CultureInfo.InvariantCulture).ToLower() %>,
            controlNavThumbs: <%= SlideShow.UseThumbnailsForIndexedNavigation.ToString(CultureInfo.InvariantCulture).ToLower() %>,
            pauseOnHover: <%= SlideShow.PauseOnHover.ToString(CultureInfo.InvariantCulture).ToLower() %>,
            manualAdvance: <%= SlideShow.AllowManualTransition.ToString(CultureInfo.InvariantCulture).ToLower() %>,
            prevText: '<%= SlideShow.PreviousText %>',
            nextText: '<%= SlideShow.NextText %>',
            randomStart: <%= SlideShow.RandomStart.ToString(CultureInfo.InvariantCulture).ToLower() %>
        });
    });
</script>

<%--
    There are also triggers that you can use:
    beforeChange: function(){}, // Triggers before a slide transition
    afterChange: function(){}, // Triggers after a slide transition
    slideshowEnd: function(){}, // Triggers after all slides have been shown
    lastSlide: function(){}, // Triggers when last slide is shown
    afterLoad: function(){} // Triggers when slider has loaded
--%>

<div id="image-slide-show-<%= UniqueIdentifier %>">
    <asp:Repeater ID="rptImageSlides" runat="server">
        <HeaderTemplate>
            <div class="slider-wrapper <%# SlideShow.SelectedTheme %>">
                <div id="slider-<%= UniqueIdentifier %>" class="nivoSlider">
        </HeaderTemplate>
            <ItemTemplate>
                <asp:PlaceHolder Visible="<%# IsLinked(((ImageSlide)Container.DataItem)) %>" runat="server"><a href="<%#((ImageSlide)Container.DataItem).ImageLinkUrl %>" rel="nofollow"></asp:PlaceHolder>
                <img src="<%# ((ImageSlide)Container.DataItem).ImageUrl %>" alt="" title="<%# CaptionOrDefault((ImageSlide)Container.DataItem) %>" data-thumb="<%# ((ImageSlide)Container.DataItem).ThumbnailUrl %>" />
                <asp:PlaceHolder Visible="<%# IsLinked(((ImageSlide)Container.DataItem)) %>" runat="server"></a></asp:PlaceHolder>
            </ItemTemplate>
        <FooterTemplate>
                </div>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    
    <asp:Repeater ID="rptHtmlCaptions" runat="server">
        <ItemTemplate>
            <div id="<%# ((ImageSlide)Container.DataItem).SlideId.ToString() %>"class="nivo-html-caption"><%# ((ImageSlide)Container.DataItem).ImageTooltip %></div>
        </ItemTemplate>
    </asp:Repeater>
</div>

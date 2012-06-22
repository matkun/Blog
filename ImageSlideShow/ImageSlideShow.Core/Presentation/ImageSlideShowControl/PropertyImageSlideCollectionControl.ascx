<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PropertyImageSlideCollectionControl.ascx.cs" Inherits="ImageSlideShow.Core.Presentation.ImageSlideShowControl.PropertyImageSlideCollectionControl" %>

<style type="text/css">
    div.image-slide-show-settings {
        background-color: #EAEAEA;
        border: 1px solid #808080;
        float: left;
        margin-left: -130px;
        padding-left: 10px;
        width: 546px;
    }
    
    div.global-slide-show-settings select {
        height: 22px;
        padding: 0;
    }
    
    div.global-slide-show-settings div {
        float: left;
        margin: 5px 0 0 0;
        width: 268px;
    }
    
    div.global-slide-show-settings div input[type=text]{
        width: 260px;
    }
    
    div.global-slide-show-settings div select {
        width: 262px;
    }
    
    div.global-slide-show-settings div.other {
        padding-left: 5px;
    }
    
    div.global-slide-show-settings ul {
        float: left;
        width: 262px;
    }
    
    div.global-slide-show-settings ul.other {
        padding-left: 13px;
    }
    
    div.global-slide-show-settings ul li {
        margin-bottom: 6px;
    }
    
    div.global-slide-show-settings ul li input {
        margin-right: 5px;
    }
    
    div.global-slide-show-settings div.slide-show-checkboxes {
        clear: both;
        margin: 7px 0;
        width: 100%;
    }
    
    div.image-slide-show-settings table.image-slide-table {
        width: 536px;
    }
    
    div.image-slide-show-settings table.image-slide-table td {
        padding: 2px;
    }
    
    div.image-slide-show-settings .image-slide-table tr.slide-row:hover {
        background-color: #CCCCCC;
    }
</style>

<div class="image-slide-show-settings">
    <div id="image-slide-collection-<%= UniqueIdentifier %>">
        <div class="epi-buttonDefault">
            <span class="epi-cmsButton">
                <input type="button" 
                       id="new-image-slide-button-<%= UniqueIdentifier %>"
                       onmouseout="EPi.ToolButton.ResetMouseDownHandler(this)" 
                       onmouseover="EPi.ToolButton.MouseDownHandler(this)" 
                       value="<%= AddImageButtonText %>"
                       name="addImageSlide"
                       class="epi-cmsButton-text epi-cmsButton-tools epi-cmsButton-Add">
            </span>
        </div>

        <table class="image-slide-table epi-default">
            <tr>
                <th colspan="2"><asp:Literal runat="server" Text="<%$ Resources: EPiServer, edit.linkcollection.sortheading %>" /></th>
                <th><asp:Literal runat="server" Text="<%$ Resources: EPiServer, ImageSlideShow.EditModeProperty.ColumnNameSlide %>" /></th>
                <th><asp:Literal runat="server" Text="<%$ Resources: EPiServer, ImageSlideShow.EditModeProperty.ColumnNameHasLink %>" /></th>
                <th><asp:Literal runat="server" Text="<%$ Resources: EPiServer, ImageSlideShow.EditModeProperty.ColumnNameHasCapition %>" /></th>
                <th><asp:Literal runat="server" Text="<%$ Resources: EPiServer, ImageSlideShow.EditModeProperty.ColumnNameHasThumbnail %>" /></th>
                <th style="text-align: center;"><asp:Literal runat="server" Text="<%$ Resources: EPiServer, button.edit %>" /></th>
                <th style="text-align: center;"><asp:Literal runat="server" Text="<%$ Resources: EPiServer, button.delete %>" /></th>
            </tr>
        </table>
    </div>
    
    <div id="global-slide-show-settings-<%= UniqueIdentifier %>" class="global-slide-show-settings">
        <div>
            <label for="te-<%= UniqueIdentifier %>" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/TransitionEffectTooltip") %>">
                <%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/TransitionEffectText")%>
            </label>
            <select id="te-<%= UniqueIdentifier %>" dto-id="SelectedTransitionEffect" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/TransitionEffectTooltip") %>">
                <asp:Repeater ID="rptTransitionEffects" runat="server">
                    <ItemTemplate>
                        <option value="<%# ((ListItem)Container.DataItem).Value %>"><%# ((ListItem)Container.DataItem).Text %></option>
                    </ItemTemplate>
                </asp:Repeater>
            </select>
        </div>
        <div class="other">
            <label for="ts-<%= UniqueIdentifier %>" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/TransitionSpeedTooltip") %>">
                <%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/TransitionSpeedText")%>
            </label>
            <input id="ts-<%= UniqueIdentifier %>" dto-id="TransitionSpeed" type="text" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/TransitionSpeedTooltip") %>" class="only-digits" />
        </div>
        <div>
            <label for="sp-<%= UniqueIdentifier %>" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/SlidePauseTooltip") %>">
                <%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/SlidePauseText")%>
            </label>
            <input id="sp-<%= UniqueIdentifier %>" dto-id="SlidePause" type="text" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/SlidePauseTooltip") %>" class="only-digits" />
        </div>
        <div class="other">
            <label for="ssi-<%= UniqueIdentifier %>" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/StartSlideIndexTooltip") %>">
                <%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/StartSlideIndexText")%>
            </label>
            <input id="ssi-<%= UniqueIdentifier %>" dto-id="StartSlideIndex" type="text" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/StartSlideIndexTooltip") %>" class="only-digits" />
        </div>
        <div>
            <label for="pt-<%= UniqueIdentifier %>" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/NavigationPreviousTooltip") %>">
                <%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/NavigationPreviousText")%>
            </label>
            <input id="pt-<%= UniqueIdentifier %>" dto-id="PreviousText" type="text" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/NavigationPreviousTooltip") %>" />
        </div>
        <div class="other">
            <label for="nt-<%= UniqueIdentifier %>" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/NavigationNextTooltip") %>">
                <%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/NavigationNextText")%>
            </label>
            <input id="nt-<%= UniqueIdentifier %>" dto-id="NextText" type="text" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/NavigationNextTooltip") %>" />
        </div>
        <div>
            <label for="nos-<%= UniqueIdentifier %>" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/NumberOfSlicesTooltip") %>">
                <%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/NumberOfSlicesText")%>
            </label>
            <input id="nos-<%= UniqueIdentifier %>" dto-id="NumberOfSlices" type="text" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/NumberOfSlicesTooltip") %>" class="only-digits" />
        </div>
        <div class="other">
            <label for="nobc-<%= UniqueIdentifier %>" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/NumberOfBoxColumnsTooltip") %>">
                <%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/NumberOfBoxColumnsText")%>
            </label>
            <input id="nobc-<%= UniqueIdentifier %>" dto-id="NumberOfBoxColumns" type="text" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/NumberOfBoxColumnsTooltip") %>" class="only-digits" />
        </div>
        <div>
            <label for="nobr-<%= UniqueIdentifier %>" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/NumberOfBoxRowsTooltip") %>">
                <%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/NumberOfBoxRowsText")%>
            </label>
            <input id="nobr-<%= UniqueIdentifier %>" dto-id="NumberOfBoxRows" type="text" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/NumberOfBoxRowsTooltip") %>" class="only-digits" />
        </div>
        <div class="other">
            <label for="the-<%= UniqueIdentifier %>" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/ThemeTooltip") %>">
                <%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/ThemeText")%>
            </label>
            <select id="t-<%= UniqueIdentifier %>" dto-id="SelectedTheme" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/ThemeTooltip") %>">
                <asp:Repeater ID="rptThemes" runat="server">
                    <ItemTemplate>
                        <option value="<%# ((ListItem)Container.DataItem).Value %>"><%# ((ListItem)Container.DataItem).Text %></option>
                    </ItemTemplate>
                </asp:Repeater>
            </select>
        </div>
        <div class="slide-show-checkboxes">
            <ul>
                <li>
                    <input id="udn-<%= UniqueIdentifier %>" dto-id="UseDirectionNavigation" type="checkbox" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/UseDirectionNavigationTooltip") %>" />
                    <label for="udn-<%= UniqueIdentifier %>" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/UseDirectionNavigationTooltip") %>">
                        <%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/UseDirectionNavigationText")%>
                    </label>
                </li>
                <li>
                    <input id="hdn-<%= UniqueIdentifier %>" dto-id="HideDirectionNavigation" type="checkbox" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/HideDirectionNavigationTooltip") %>" />
                    <label for="hdn-<%= UniqueIdentifier %>" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/HideDirectionNavigationTooltip") %>">
                        <%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/HideDirectionNavigationText")%>
                    </label>
                </li>
                <li>
                    <input id="uin-<%= UniqueIdentifier %>" dto-id="UseIndexedNavigation" type="checkbox" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/UseIndexedNavigationTooltip") %>" />
                    <label for="uin-<%= UniqueIdentifier %>" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/UseIndexedNavigationTooltip") %>">
                        <%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/UseIndexedNavigationText")%>
                    </label>
                </li>
                <li>
                    <input id="utfin-<%= UniqueIdentifier %>" dto-id="UseThumbnailsForIndexedNavigation" type="checkbox" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/UseThumbnailsForIndexedNavigationTooltip") %>" />
                    <label for="utfin-<%= UniqueIdentifier %>" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/UseThumbnailsForIndexedNavigationTooltip") %>">
                        <%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/UseThumbnailsForIndexedNavigationText")%>
                    </label>
                </li>
            </ul>
            <ul class="other">
                <li>
                    <input id="poh-<%= UniqueIdentifier %>" dto-id="PauseOnHover" type="checkbox" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/PauseOnHoverTooltip") %>" />
                    <label for="poh-<%= UniqueIdentifier %>" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/PauseOnHoverTooltip") %>">
                        <%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/PauseOnHoverText")%>
                    </label>
                </li>
                <li>
                    <input id="amt-<%= UniqueIdentifier %>" dto-id="AllowManualTransition" type="checkbox" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/ForceManualTransitionTooltip") %>" />
                    <label for="amt-<%= UniqueIdentifier %>" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/ForceManualTransitionTooltip") %>">
                        <%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/ForceManualTransitionText")%>
                    </label>
                </li>
                <li>
                    <input id="rs-<%= UniqueIdentifier %>" dto-id="RandomStart" type="checkbox" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/RandomStartTooltip") %>" />
                    <label for="rs-<%= UniqueIdentifier %>" title="<%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/RandomStartTooltip") %>">
                        <%= Translator.Translate("/ImageSlideShow/EditModeProperty/GlobalSettings/RandomStartText")%>
                    </label>
                </li>
            </ul>
        </div>
    </div>

    <script type="text/javascript" language="javascript">
    // <![CDATA[
        $(document).ready(function () {
            $('document').ready(function () {
                var cleaner = ImageSlideShow.FieldCleaning('<%= UniqueIdentifier %>');
                cleaner.init();
                
                var imageSlideEditor = ImageSlideShow.EditModeImageSlideCollection('<%= UniqueIdentifier %>', '<%= ImageSlideEditorUrl %>', '<%= InitialImageSlideShow %>', '<%= EmptyImageSlideShow %>');
                imageSlideEditor.init();
                $("#new-image-slide-button-<%= UniqueIdentifier %>").click(imageSlideEditor.newItem);
                var settings = $("#global-slide-show-settings-<%= UniqueIdentifier %>");
                settings.find("input").change(imageSlideEditor.updateGlobalSettings);
                settings.find("select").change(imageSlideEditor.updateGlobalSettings);
            });
        });
    // ]]>
    </script>
</div>

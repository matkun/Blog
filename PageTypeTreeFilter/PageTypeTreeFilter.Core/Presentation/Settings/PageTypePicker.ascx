<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PageTypePicker.ascx.cs" Inherits="PageTypeTreeFilter.Presentation.Settings.PageTypePicker" %>

<div id="page-type-picker-container" class="epi-paddingVertical">
    <asp:HiddenField ID="hfSelectedPageTypes" runat="server" />
    <div id="page-type-picker-description"><%= PageTypePickerDescription %></div>

    <div id="available-page-types" class="epi-floatLeft">
        <label for="available" title="<%= AvailablePageTypesDescription %>"><%= AvailablePageTypesLabel %></label>
        <select id="available" multiple="multiple"<%# !Enabled ? " disabled=\"disabled\"" : string.Empty %>>
            <asp:Repeater ID="rptAvailablePageTypes" runat="server">
                <ItemTemplate>
                    <option value="<%# ((ListItem)Container.DataItem).Value %>"><%# ((ListItem)Container.DataItem).Text %></option>
                </ItemTemplate>
            </asp:Repeater>
        </select>
    </div>
        
    <div id="arrow-container" class="epi-floatLeft epi-arrowButtonContainer">
        <span class="epi-cmsButton upper-button">
            <input<%# !Enabled ? " disabled=\"disabled\"" : string.Empty %> id="add-page-type-button" class="epi-cmsButton-tools epi-cmsButton-ArrowRight" type="submit" onclick="return false;" onmouseout="EPi.ToolButton.ResetMouseDownHandler(this)" onmouseover="EPi.ToolButton.MouseDownHandler(this)" title="<%= AddPageTypeToolTip %>" value=" " />
        </span>
        <span class="epi-cmsButton">
            <input<%# !Enabled ? " disabled=\"disabled\"" : string.Empty %> id="remove-page-type-button" class="epi-cmsButton-tools epi-cmsButton-ArrowLeft" type="submit" onclick="return false;" onmouseout="EPi.ToolButton.ResetMouseDownHandler(this)" onmouseover="EPi.ToolButton.MouseDownHandler(this)" title="<%= RemovePageTypeToolTip %>" value=" " />
        </span>
    </div>
        
    <div id="selected-page-types" class="epi-floatLeft">
        <label for="selected" title="<%= SelectedPageTypesDescription %>"><%= SelectedPageTypesLabel %></label>
        <select id="selected" multiple="multiple"<%# !Enabled ? " disabled=\"disabled\"" : string.Empty %>>
            <asp:Repeater ID="rptSelectedPageTypes" runat="server">
                <ItemTemplate>
                    <option value="<%# ((ListItem)Container.DataItem).Value %>"><%# ((ListItem)Container.DataItem).Text %></option>
                </ItemTemplate>
            </asp:Repeater>
        </select>
    </div>

    <div class="clear-left"></div>
</div>

<script type="text/javascript" language="javascript">
//<![CDATA[
    var PageTypePicker = {
        initialize: function () {
            $('#add-page-type-button').bind('click', PageTypePicker.AddPageTypes);
            $('#remove-page-type-button').bind('click', PageTypePicker.RemovePageTypes);
            $('#available').bind('dblclick', PageTypePicker.AddPageTypes);
            $('#selected').bind('dblclick', PageTypePicker.RemovePageTypes);
        },

        AddPageTypes: function () {
            $('#available option:selected').remove().appendTo('#selected');
            PageTypePicker.StoreSelectedPageTypes();
        },

        RemovePageTypes: function () {
            $('#selected option:selected').remove().appendTo('#available');
            PageTypePicker.StoreSelectedPageTypes();
        },

        StoreSelectedPageTypes: function () {
            var pageTypeIds = [];
            $('#selected option').each(function (i, option) {
                pageTypeIds[i] = $(option).val();
            });
            $('#<%= hfSelectedPageTypes.ClientID %>').val(pageTypeIds.join(";"));
        }
    };
    
    $(document).ready(function () {
        PageTypePicker.initialize();
    });
//]]>
</script>

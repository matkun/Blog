<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ImageMapViewModeControl.ascx.cs" Inherits="EPiServer.ImageMap.Web.ImageMapProperty.ImageMapViewModeControl" %>

<style type="text/css" media="screen">
    #image-map-<%# ImageMap.ImageMapId %> div.view-mode-image-map-container {
        display: inline-block;
        position: relative;
    }
    #image-map-<%# ImageMap.ImageMapId %> div.view-mode-image-map-container img {
        max-width: 607px;
    }
    #image-map-<%# ImageMap.ImageMapId %> div.view-mode-image-map-container a.hot-spot {
        background: url("/imagemap/web/style/img/dutt-blue.png") no-repeat center transparent;
        display: block;
        position: absolute;
        z-index: 100;
    }
    #image-map-<%# ImageMap.ImageMapId %> div.view-mode-image-map-container a.hot-spot:hover {
        background: url("/imagemap/web/style/img/dutt-red.png") no-repeat center transparent;
    }
</style>

<div id="image-map-<%# ImageMap.ImageMapId %>">
    <div class="view-mode-image-map-container">
        <img alt='' src="<%# ImageMap.ImageUrl %>" />
        <asp:PlaceHolder ID="phHotSpots" runat="server"></asp:PlaceHolder>
    </div>
</div>

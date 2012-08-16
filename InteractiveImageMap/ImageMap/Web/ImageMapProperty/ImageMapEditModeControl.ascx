<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ImageMapEditModeControl.ascx.cs" Inherits="EPiServer.ImageMap.Web.ImageMapProperty.ImageMapEditModeControl" %>

<div id="image-map-<%= UniqueIdentifier %>">
    <style type="text/css">
        table.hot-spot-table tr td {
            padding: 2px;
        }
        div.image-map-preview-container {
            display: inline-block;
            position: relative;
        }
        div.image-map-preview-container div.preview-hot-spot {
            background: none;
            z-index: 100;
            position: absolute;
        }
        div.image-map-preview-container div.preview-hot-spot div.dutt {
            background: url("/imagemap/web/style/img/dutt-blue.png") no-repeat center transparent;
            width: 100%;
            height: 100%;
        }
        table.hot-spot-table tr.hot-spot-row.highlighted-hot-spot {
            background-color: #f099ae;
        }
        div.image-map-preview-container div.preview-hot-spot.highlighted-hot-spot {
            background: url("/imagemap/web/style/img/background-grey.png") repeat scroll 0 0 transparent;
        }
        div.image-map-preview-container div.preview-hot-spot.highlighted-hot-spot div.dutt {
            background: url("/imagemap/web/style/img/dutt-red.png") no-repeat center transparent;
        }
        div.image-controls {
            margin-top: 9px;
        }
    </style>
    <div class="image-map-url-container"></div>
    
    <div class="image-controls">
        <div class="image-map-preview-container"></div>
    
        <div class="hot-spot-controls">
            <div class="epi-buttonDefault">
                <span class="epi-cmsButton">
                    <input type="button"
                            id="new-hot-spot-button-<%= UniqueIdentifier %>"
                            onmouseout="EPi.ToolButton.ResetMouseDownHandler(this)"
                            onmouseover="EPi.ToolButton.MouseDownHandler(this)"
                            value="New hot spot"
                            name="addHotSpot"
                            class="epi-cmsButton-text epi-cmsButton-tools epi-cmsButton-Add">
                </span>
            </div>
            <table class="hot-spot-table epi-default">
                <tr>
                    <th colspan="2">Over/Under</th>
                    <th>Hot spot target</th>
                    <th style="text-align: center;"><asp:Literal runat="server" Text="<%$ Resources: EPiServer, button.edit %>" /></th>
                    <th style="text-align: center;"><asp:Literal runat="server" Text="<%$ Resources: EPiServer, button.delete %>" /></th>
                </tr>
            </table>
        </div>
    </div>

    <script type="text/javascript" language="javascript">
    // <![CDATA[
        $(document).ready(function () {
            $('document').ready(function () {
                var imageMapEditor = ImageMap.EditModeImageMap('<%= UniqueIdentifier %>', '<%= ImageMapEditorUrl %>', '<%= InitialImageMap %>', '<%= EmptyImageMap %>');
                imageMapEditor.init();
                $("#new-hot-spot-button-<%= UniqueIdentifier %>").click(imageMapEditor.newItem);
            });
        });
    // ]]>
    </script>
</div>

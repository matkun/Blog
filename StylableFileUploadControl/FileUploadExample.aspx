<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUploadExample.aspx.cs" Inherits="StylableFileUploadControl.FileUploadExample" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>FileUploadAdapter sample code</title>
        <script type="text/javascript" language="javascript" src="http://code.jquery.com/jquery-1.7.1.min.js"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <label for="<%= FileUpload1.ClientID %>">FileUpload 1:</label><asp:FileUpload ID="FileUpload1" runat="server" />
                <label for="<%= FileUpload2.ClientID %>">FileUpload 2:</label><asp:FileUpload ID="FileUpload2" runat="server" />
                <label for="<%= FileUpload3.ClientID %>">FileUpload 3:</label><asp:FileUpload ID="FileUpload3" runat="server" />
            </div>
        </form>
    </body>
</html>

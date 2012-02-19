<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="EmbeddedImage.aspx.cs" Inherits="EPiServer.CodeSample.EmbeddedImage" %>
<%@ Register TagPrefix="CodeSample" Namespace="Base64EmbeddedImage" Assembly="Base64EmbeddedImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Embedded Image Sample</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <CodeSample:ImageEmbeddedBase64
            ID="embeddedBase64Image"
            ImageUrl="<%# CurrentPage.ImageToEmbed %>"
            runat="server" />
    </div>
    </form>
</body>
</html>

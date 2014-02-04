<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sample.aspx.cs" Inherits="SmallSample.Sample" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Sample code</title>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                
                
                

                
                <div>
                    <label for="<%# tbName.ClientID %>">Name:</label>
                    <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ControlToValidate="tbName"
                        ErrorMessage="You must supply a name"
                        EnableClientScript="True"
                        Display="Dynamic"
                        runat="server"></asp:RequiredFieldValidator>
                </div>

                <div>
                    <label for="<%# tbEmail.ClientID %>">E-mail:</label>
                    <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                        ControlToValidate="tbEmail"
                        ErrorMessage="You must supply an e-mail address"
                        EnableClientScript="True"
                        Display="Dynamic"
                        runat="server"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator
                        ControlToValidate="tbEmail"
                        ValidationExpression="[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}"
                        ErrorMessage="The e-mail address is invalid."
                        EnableClientScript="True"
                        Display="Dynamic"
                        Enabled="True"
                        runat="server" />
                </div>

                <div>
                    <label for="<%# tbPhone.ClientID %>">Phone:</label>
                    <asp:TextBox ID="tbPhone" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                        ControlToValidate="tbPhone" 
                        ErrorMessage="You must supply a phone number" 
                        EnableClientScript="True"
                        Display="Dynamic"
                        runat="server"></asp:RequiredFieldValidator>
                </div>

                <div>
                    <asp:Button OnClick="SubmitButton_Click" Text="Get Download Link" runat="server" />
                </div>

                <asp:PlaceHolder ID="phPDFLink" Visible="False" runat="server"></asp:PlaceHolder>
                
                
                

            </div>
        </form>
    </body>
</html>

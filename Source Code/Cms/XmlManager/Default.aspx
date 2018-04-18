<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2
        {
            font-size: small;
            height: 24px;
        }
        .style3
        {
            width: 81px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <div style="padding: 100px;">
            <table style="border: 1px solid #333333; width: 431px; height: 157px;">
                <tr>
                    <td colspan="2" bgcolor="#41526D" class="style2" style="border: 1px solid #808080;
                        color: White;">
                        <strong>Select Xml File</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                    <td class="style3">
                        <asp:Button ID="btnLoad" runat="server" Text="Load Xml" OnClick="btnLoad_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label1" runat="server" Style="font-family: 'Segoe UI'; font-weight: 700;
                            font-size: large; color: #666666"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </center>
</asp:Content>

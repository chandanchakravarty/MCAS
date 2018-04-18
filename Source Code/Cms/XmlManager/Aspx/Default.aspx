<%@ Page Title="" Language="C#" MasterPageFile="~/XmlManager/Aspx/XmlMaster.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Cms.XmlManager.Aspx.Default" %>

<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <webcontrol:Menu id="bottomMenu" runat="server">
                </webcontrol:Menu>
            </td>
        </tr>
    </table>
    <center>
        <div style="padding: 100px;">
            <table style="border: 1px solid #333333; width: 431px; height: 157px;">
                <tr>
                    <td colspan="2" class="DefaultPageHeading">
                        <strong>Select Xml File</strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                    <td class="DefaultPageButton">
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
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <webcontrol:Footer id="pageFooter" runat="server">
                </webcontrol:Footer>
            </td>
        </tr>
    </table>
</asp:Content>

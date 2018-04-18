<%@ Page Title="" Language="C#" MasterPageFile="~/XmlManager/Aspx/XmlMaster.Master"
    AutoEventWireup="true" CodeBehind="GridXML.aspx.cs" Inherits="Cms.XmlManager.Aspx.GridXML" %>

<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register Src="../webcontrols/TreeControlForGridXml.ascx" TagName="TreeControlForGridXml"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style3
        {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2">
                <webcontrol:Menu id="bottomMenu" runat="server">
                </webcontrol:Menu>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="right">
                <asp:Button ID="btnHome" runat="server" Text="Home" OnClick="btnHome_Click" />
                <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
            </td>
        </tr>
    </table>
    <div style="float: left; width: 35%">
        <uc1:TreeControlForGridXml ID="trControlForGridXml" runat="server" />
    </div>
    <div style="float: right; width: 64%">
        <table width="100%" cellspacing="10" style="border: 1px solid #C0C0C0">
            <tr>
                <td style="background-color: #B9C3D5; font-weight: bold; padding: 5px;">
                    <table>
                        <tr>
                            <td>
                                Title -
                            </td>
                            <td>
                                <asp:Label ID="lblName" runat="server" Width="100%"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
                        <ItemTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="right" class="style3">
                                        Value
                                    </td>
                                    <td style="width: 75%;" align="left">
                                        <asp:TextBox ID="txtName" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <webcontrol:Footer id="pageFooter" runat="server">
                </webcontrol:Footer>
            </td>
        </tr>
    </table>
</asp:Content>

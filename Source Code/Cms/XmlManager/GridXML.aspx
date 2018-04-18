<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="GridXML.aspx.cs" Inherits="GridXML" %>

<%@ Register Src="UserControl/TreeControlForGrid.ascx" TagName="TreeControlForGrid"
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
    <div style="float: left; width: 35%">
        <uc1:TreeControlForGrid ID="TreeControlForGrid1" runat="server" />
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
</asp:Content>

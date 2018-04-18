<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="MessageXML.aspx.cs" Inherits="MessageXML" %>

<%@ Register Src="UserControl/TreeControlCustomized.ascx" TagName="TreeControlCustomized"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2
        {
            width: 67px;
        }
        .style3
        {
            width: 89px;
        }
        .style4
        {
            width: 131px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="float: left; width: 35%">
        <uc1:TreeControlCustomized ID="TreeControlCustomized1" runat="server" />
    </div>
    <div style="float: right; width: 64%">
        <table width="100%" cellspacing="10" style="border: 1px solid #C0C0C0">
            <tr>
                <td style="background-color: #B9C3D5; font-weight: bold; padding: 5px;">
                    <table>
                        <tr>
                            <td>
                                Screen -
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
                                    <td align="right" class="style2">
                                        Message Id
                                    </td>
                                    <td align="left" class="style4">
                                        <asp:TextBox ID="txtName" runat="server" Width="90%" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td align="right" class="style3">
                                        Message
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtValue" runat="server" Width="100%"></asp:TextBox>
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

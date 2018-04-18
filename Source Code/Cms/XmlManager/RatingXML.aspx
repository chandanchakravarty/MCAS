<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="RatingXML.aspx.cs" Inherits="RatingXML" %>

<%@ Register Src="UserControl/TreeControlRating.ascx" TagName="TreeControlRating"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style3
        {
            text-align: left;
            color: #999999;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="float: left; width: 35%">
        <uc1:TreeControlRating ID="TreeControlRating1" runat="server" />
    </div>
    <div style="float: right; width: 64%">
        <table width="100%" cellspacing="10" style="border: 1px solid #C0C0C0">
            <tr>
                <td style="background-color: #B9C3D5; font-weight: bold; padding: 5px;">
                    <table>
                        <tr>
                            <td>
                                Node -
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
                                    <td align="right" class="style3" valign="top" bgcolor="#CCCCCC" style="border: 1px solid #808080">
                                        Attributes
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 1px solid #808080; width: 75%;" align="left">
                                        <asp:Repeater ID="rpt" runat="server">
                                            <ItemTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 25%;">
                                                            <asp:Label ID="lblName" runat="server" Width="99%" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 75%;">
                                                            <asp:TextBox ID="txtName" runat="server" Width="99%" Text='<%# DataBinder.Eval(Container.DataItem, "Value") %>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <br />
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

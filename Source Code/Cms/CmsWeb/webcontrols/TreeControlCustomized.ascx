<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TreeControlCustomized.ascx.cs"
    Inherits="TreeControlCustomized" %>
<table width="100%">
    <tr style="background-color: #9CAAC1">
        <td>
            Culture Code -
        </td>
        <td>
            <asp:DropDownList ID="ddlCulture" runat="server">
                <asp:ListItem Text="All" Selected="True"></asp:ListItem>
                <asp:ListItem Text="en-US"></asp:ListItem>
                <asp:ListItem Text="pt-BR"></asp:ListItem>
                <asp:ListItem Text="zh-SG"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
        </td>
    </tr>
    <tr style="background-color: #9CAAC1">
        <td>
            Screen Id -
        </td>
        <td>
            <asp:DropDownList ID="ddlScreen" runat="server">
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnApply" runat="server" Text="Apply Filter" OnClick="btnApply_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <ul id="browser" class="filetree treeview-famfamfam">
                <li><span class="folder">Customized Messages</span>
                    <ul>
                        <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
                            <ItemTemplate>
                                <li><span class="folder">Culture -
                                    <%# DataBinder.Eval(Container.DataItem, "Value") %></span>
                                    <ul>
                                        <asp:Repeater ID="rpt1" runat="server" OnItemDataBound="rpt1_ItemDataBound">
                                            <ItemTemplate>
                                                <li class="closed"><span class="folder">Screen -
                                                    <asp:LinkButton ID="lnkValue" runat="server" CssClass="file" OnClick="lnkValue_Click"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "Value") %>'></asp:LinkButton></span>
                                                    <ul id="folder21">
                                                        <asp:Repeater ID="rpt2" runat="server" OnItemDataBound="rpt2_ItemDataBound">
                                                            <ItemTemplate>
                                                                <li class="closed"><span class="file">Message -
                                                                    <%# DataBinder.Eval(Container.DataItem, "Value") %>
                                                                    <asp:LinkButton ID="lnkIndex" runat="server" Visible="false"></asp:LinkButton></span></li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ul>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </li>
            </ul>
        </td>
    </tr>
</table>

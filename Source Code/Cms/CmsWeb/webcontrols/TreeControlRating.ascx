<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TreeControlRating.ascx.cs"
    Inherits="TreeControlRating" %>
<table>
    <tr style="background-color: #9CAAC1">
        <td>
            Factor Id -
        </td>
        <td>
            <asp:DropDownList ID="ddlFactor" runat="server">
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
            Node Id -
        </td>
        <td>
            <asp:DropDownList ID="ddlNode" runat="server">
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnApply" runat="server" Text="Apply Filter" OnClick="btnApply_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <ul id="browser" class="filetree treeview-famfamfam">
                <li><span class="folder">PRODUCT MASTER</span>
                    <ul>
                        <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
                            <ItemTemplate>
                                <li><span class="folder">Product -
                                    <%# DataBinder.Eval(Container.DataItem, "Value") %></span>
                                    <ul>
                                        <asp:Repeater ID="rpt1" runat="server" OnItemDataBound="rpt1_ItemDataBound">
                                            <ItemTemplate>
                                                <li class="closed"><span class="folder">Factor -
                                                    <%# DataBinder.Eval(Container.DataItem, "Value")%></span>
                                                    <ul id="folder21">
                                                        <asp:Repeater ID="rpt2" runat="server" OnItemDataBound="rpt2_ItemDataBound">
                                                            <ItemTemplate>
                                                                <li class="closed"><span class="folder">Node -
                                                                    <asp:LinkButton ID="lnkValue" runat="server" CssClass="file" OnClick="lnkValue_Click"
                                                                        Text='<%# DataBinder.Eval(Container.DataItem, "Value") %>'></asp:LinkButton></span>
                                                                    <ul id="folder21">
                                                                        <asp:Repeater ID="rpt3" runat="server" OnItemDataBound="rpt3_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <li class="closed"><span class="file">Attribute -
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
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </li>
            </ul>
        </td>
    </tr>
</table>

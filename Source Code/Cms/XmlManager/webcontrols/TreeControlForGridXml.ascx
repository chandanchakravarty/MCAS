<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TreeControlForGridXml.ascx.cs" Inherits="Cms.webcontrols.TreeControlForGridXml" %>
<table>
    <tr style="background-color: #9CAAC1">
        <td>
            Element Name -
        </td>
        <td>
            <asp:DropDownList ID="ddlElement" runat="server">
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnApply" runat="server" Text="Apply Filter" OnClick="btnApply_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <ul id="browser" class="filetree treeview-famfamfam">
                <li><span class="folder">FUND TYPE</span>
                    <ul>
                        <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
                            <ItemTemplate>
                                <li class="closed"><span class="folder"> Node - 
                                    <asp:LinkButton ID="lnkValue" runat="server" CssClass="file" OnClick="lnkValue_Click"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:LinkButton>
                                </span>
                                    <ul id="folder21">
                                        <asp:Repeater ID="rpt1" runat="server">
                                            <ItemTemplate>
                                                <li class="closed"><span class="file">Value -
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
            </ul>
        </td>
    </tr>
</table>
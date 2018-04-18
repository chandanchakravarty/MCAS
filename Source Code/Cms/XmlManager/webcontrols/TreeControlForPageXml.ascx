<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TreeControlForPageXml.ascx.cs" Inherits="Cms.webcontrols.TreeControlForPageXml" %>
<table>
    <tr style="background-color: #9CAAC1">
        <td>
            Page Element Name -
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
                <li class="closed"><span class="folder">Page Title</span>
                    <ul>
                        <li class="closed"><span class="folder">Culture</span>
                            <ul id="folder21">
                                <li class="closed"><span class="file">Code - en-US</span></li>
                            </ul>
                        </li>
                        <li class="closed"><span class="folder">Culture</span>
                            <ul id="Ul6">
                                <li class="closed"><span class="file">Code - zh-SG</span></li>
                            </ul>
                        </li>
                        <li class="closed"><span class="folder">Culture</span>
                            <ul id="Ul7">
                                <li class="closed"><span class="file">Code - pt-BR</span></li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li><span class="folder">Page Element</span>
                    <ul>
                        <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
                            <ItemTemplate>
                                <li class="closed"><span class="folder">PageElement -
                                    <asp:LinkButton ID="lbl" runat="server" CssClass="file" OnClick="lnkValue_Click"></asp:LinkButton>
                                </span>
                                    <ul>
                                        <li class="closed"><span class="folder">Binding</span>
                                            <ul id="folder21">
                                                <li class="closed"><span class="file">Value </span></li>
                                            </ul>
                                        </li>
                                        <li class="closed"><span class="folder">Style </span>
                                            <ul id="Ul1">
                                                <li class="closed"><span class="file">Value </span></li>
                                            </ul>
                                        </li>
                                        <li class="closed"><span class="folder">rev </span>
                                            <ul id="Ul2">
                                                <li class="closed"><span class="file">Value </span></li>
                                            </ul>
                                        </li>
                                        <li class="closed"><span class="folder">csv </span>
                                            <ul id="Ul3">
                                                <li class="closed"><span class="file">Value </span></li>
                                            </ul>
                                        </li>
                                        <li class="closed"><span class="folder">Culture </span>
                                            <ul id="Ul4">
                                                <li class="closed"><span class="file">Caption </span></li>
                                                <li class="closed"><span class="file">rfvMessage </span></li>
                                                <li class="closed"><span class="file">DefaultValue </span></li>
                                                <li class="closed"><span class="file">revExpMessage </span></li>
                                            </ul>
                                        </li>
                                        <li class="closed"><span class="folder">Culture </span>
                                            <ul id="Ul5">
                                                <li class="closed"><span class="file">Caption </span></li>
                                                <li class="closed"><span class="file">rfvMessage </span></li>
                                                <li class="closed"><span class="file">DefaultValue </span></li>
                                                <li class="closed"><span class="file">revExpMessage </span></li>
                                            </ul>
                                        </li>
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
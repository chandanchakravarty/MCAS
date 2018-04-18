<%@ Page Title="" Language="C#" MasterPageFile="~/XmlManager/Aspx/XmlMaster.Master"
    AutoEventWireup="true" CodeBehind="PageXML.aspx.cs" Inherits="Cms.XmlManager.Aspx.PageXML" %>

<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register Src="../webcontrols/TreeControlForPageXml.ascx" TagName="TreeControlForPageXml"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2
        {
            width: 5%;
            text-align: left;
        }
        .style3
        {
            width: 8%;
            text-align: right;
        }
        .style4
        {
            width: 2%;
            text-align: left;
        }
        .style5
        {
            width: 4%;
            text-align: right;
        }
        .style6
        {
            text-align: right;
        }
        .style7
        {
            width: 7%;
        }
        .style8
        {
            width: 11%;
        }
        .style9
        {
            width: 6%;
            text-align: right;
        }
        .style10
        {
            width: 7%;
            text-align: right;
        }
        .style11
        {
            text-align: left;
            width: 13%;
        }
        .style12
        {
            text-align: left;
        }
        .style13
        {
            width: 13%;
        }
        .style14
        {
            width: 10%;
            text-align: right;
        }
        .style15
        {
            width: 4%;
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
        <uc1:TreeControlForPageXml ID="trControlForPageXml" runat="server" />
    </div>
    <div style="float: right; width: 64%">
        <asp:Repeater ID="rpt" runat="server" OnItemDataBound="rpt_ItemDataBound">
            <ItemTemplate>
                <table width="100%" cellspacing="2" style="border: 1px solid #C0C0C0">
                    <tr>
                        <td style="background-color: #B9C3D5; font-weight: bold; padding: 5px; border: 1px solid #C0C0C0">
                            <table>
                                <tr>
                                    <td>
                                        Page Element -
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
                            <table width="99%" style="border: 1px solid #C0C0C0">
                                <tr>
                                    <td>
                                        <table width="100%" cellspacing="1">
                                            <tr>
                                                <td align="right" class="style4">
                                                    Name
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:TextBox ID="txtName" runat="server" Width="99%" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td align="right" class="style5">
                                                    Type
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:DropDownList ID="txtType" runat="server" Width="99%">
                                                        <asp:ListItem Text="TextBox" Value="TextBox"></asp:ListItem>
                                                        <asp:ListItem Text="RadioButton" Value="RadioButton"></asp:ListItem>
                                                        <asp:ListItem Text="ComboBox" Value="ComboBox"></asp:ListItem>
                                                        <asp:ListItem Text="CheckBox" Value="CheckBox"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 15%;" class="style6">
                                                    <asp:CheckBox ID="chkIsMandatory" runat="server" Width="99%" Text="IsMandatory" Style="text-align: left">
                                                    </asp:CheckBox>
                                                </td>
                                                <td style="width: 15%;" class="style12">
                                                    <asp:CheckBox ID="chkIsReadOnly" runat="server" Width="99%" Text="IsReadOnly"></asp:CheckBox>
                                                </td>
                                                <td style="width: 15%;" class="style12">
                                                    <asp:CheckBox ID="chkIsDisabled" runat="server" Width="99%" Text="IsDisabled"></asp:CheckBox>
                                                </td>
                                                <td style="width: 15%;" class="style12">
                                                    <asp:CheckBox ID="chkIsDisplay" runat="server" Width="99%" Text="IsDisplay"></asp:CheckBox>
                                                </td>
                                                <td style="width: 15%;" class="style12">
                                                    <asp:CheckBox ID="chkIsFormatingRequired" runat="server" Width="99%" Text="IsFormatingRequired">
                                                    </asp:CheckBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td class="style11">
                                                    JsFunctionToFormat
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:TextBox ID="txtJsFunctionToFormat" runat="server" Width="99%"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%;" class="style6">
                                                    OnClickJSFunction
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:TextBox ID="txtOnClickJSFunction" runat="server" Width="99%"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%;" class="style6">
                                                    OnChangeJSFunction
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:TextBox ID="txtOnChangeJSFunction" runat="server" Width="98%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #B9C3D5; font-weight: bold; padding: 5px; border: 1px solid #C0C0C0">
                            Binding
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="99%" style="border: 1px solid #C0C0C0">
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" class="style4">
                                                    Binding
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:TextBox ID="txtBinding" runat="server" Width="98%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #B9C3D5; font-weight: bold; padding: 5px; border: 1px solid #C0C0C0">
                            Style
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="99%" style="border: 1px solid #C0C0C0">
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td class="style2">
                                                    Position
                                                </td>
                                                <td align="left" class="style13">
                                                    <asp:DropDownList ID="txtPosition" runat="server" Width="97%">
                                                        <asp:ListItem Text="absolute" Value="absolute"></asp:ListItem>
                                                        <asp:ListItem Text="reletive" Value="reletive"></asp:ListItem>
                                                        <asp:ListItem Text="fixed" Value="fixed"></asp:ListItem>
                                                        <asp:ListItem Text="inherit" Value="inherit"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="style10">
                                                    Top
                                                </td>
                                                <td align="left" class="style7">
                                                    <asp:TextBox ID="txtTop" runat="server" Width="84%"></asp:TextBox>
                                                </td>
                                                <td class="style9">
                                                    Left
                                                </td>
                                                <td align="left" class="style7">
                                                    <asp:TextBox ID="txtLeft" runat="server" Width="96%"></asp:TextBox>
                                                </td>
                                                <td class="style3">
                                                    CssClass
                                                </td>
                                                <td align="left" class="style8">
                                                    <asp:TextBox ID="txtCssClass" runat="server" Width="73%"></asp:TextBox>
                                                </td>
                                                <td class="style14">
                                                    ParentTableCell
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:TextBox ID="txtParentTableCell" runat="server" Width="98%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #B9C3D5; font-weight: bold; padding: 5px; border: 1px solid #C0C0C0">
                            Rev
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="99%" style="border: 1px solid #C0C0C0">
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" class="style4">
                                                    <asp:CheckBox ID="chkEnabledR" runat="server" Width="133%" Text="Enabled"></asp:CheckBox>
                                                </td>
                                                <td align="right" class="style5">
                                                    ExpKey
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:TextBox ID="txtExpKey" runat="server" Width="99%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #B9C3D5; font-weight: bold; padding: 5px; border: 1px solid #C0C0C0">
                            Csv
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="99%" style="border: 1px solid #C0C0C0">
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" class="style4">
                                                    <asp:CheckBox ID="chkEnabledC" runat="server" Width="129%" Text="Enabled"></asp:CheckBox>
                                                </td>
                                                <td align="right" class="style3">
                                                    csvValidationFunction
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:TextBox ID="txtcsvValidationFunction" runat="server" Width="99%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #B9C3D5; font-weight: bold; padding: 5px; border: 1px solid #C0C0C0">
                            Culture 1
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="99%" style="border: 1px solid #C0C0C0">
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" class="style4">
                                                    Code
                                                </td>
                                                <td align="left" class="style15">
                                                    <asp:DropDownList ID="txtCode0" runat="server" Width="97%">
                                                        <asp:ListItem Text="en-US" Value="en-US"></asp:ListItem>
                                                        <asp:ListItem Text="zh-SG" Value="zh-SG"></asp:ListItem>
                                                        <asp:ListItem Text="pt-BR" Value="pt-BR"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right" class="style5">
                                                    Desc
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:TextBox ID="txtDesc0" runat="server" Width="99%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" class="style4">
                                                    Caption
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:TextBox ID="txtCaption0" runat="server" Width="79%"></asp:TextBox>
                                                </td>
                                                <td align="right" class="style5">
                                                    rfvMessage
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:TextBox ID="txtrfvMessage0" runat="server" Width="85%"></asp:TextBox>
                                                </td>
                                                <td align="right" class="style4">
                                                    DefaultValue
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:TextBox ID="txtDefaultValue0" runat="server" Width="82%"></asp:TextBox>
                                                </td>
                                                <td align="right" class="style5">
                                                    revExpMessage
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:TextBox ID="txtrevExpMessage0" runat="server" Width="99%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #B9C3D5; font-weight: bold; padding: 5px; border: 1px solid #C0C0C0">
                            Culture 2
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="99%" style="border: 1px solid #C0C0C0">
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" class="style4">
                                                    Code
                                                </td>
                                                <td align="left" class="style15">
                                                    <asp:DropDownList ID="txtCode" runat="server" Width="97%">
                                                        <asp:ListItem Text="en-US" Value="en-US"></asp:ListItem>
                                                        <asp:ListItem Text="zh-SG" Value="zh-SG"></asp:ListItem>
                                                        <asp:ListItem Text="pt-BR" Value="pt-BR"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right" class="style5">
                                                    Desc
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:TextBox ID="txtDesc" runat="server" Width="98%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" class="style4">
                                                    Caption
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:TextBox ID="txtCaption" runat="server" Width="79%"></asp:TextBox>
                                                </td>
                                                <td align="right" class="style5">
                                                    rfvMessage
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:TextBox ID="txtrfvMessage" runat="server" Width="85%"></asp:TextBox>
                                                </td>
                                                <td align="right" class="style4">
                                                    DefaultValue
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:TextBox ID="txtDefaultValue" runat="server" Width="82%"></asp:TextBox>
                                                </td>
                                                <td align="right" class="style5">
                                                    revExpMessage
                                                </td>
                                                <td style="width: 15%;" align="left">
                                                    <asp:TextBox ID="txtrevExpMessage" runat="server" Width="98%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
        <table>
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

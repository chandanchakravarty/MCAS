<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InterfaceFile.aspx.cs" Inherits="Cms.Account.Aspx.InterfaceFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Interface File</title>
    <meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">   
    
<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>  
    	
		   

		  
</head>
<body oncontextmenu = "return false;" class="bodyBackGround" topMargin="0" onload="ApplyColor();ChangeColor(); top.topframe.main1.mousein = false;findMouseIn();showScroll();"
		MS_POSITIONING="GridLayout">
<webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
    <form id="formInterfaceFile"  method="post" runat="server">
    <div>
        <table class="tableWidth" cellspacing="1" cellpadding="0" align="center" border="0"
            width="50%">
            <tr>
                <td class="headereffectcenter" colspan="3">
                    <asp:Label ID="capHeader" runat="server"></asp:Label>
                </td>
                <%--Download Interface File--%>
            </tr>
            <tr>
                <td class="midcolora" width="50%">
                    <asp:Label ID="capDATE" runat="server" Text="Select File Date"></asp:Label><span
                        class="mandatory">*</span>
                </td>
                <td class="midcolora" width="50%" colspan="2">
                    <asp:TextBox ID="txtDATE" runat="server" ReadOnly="false"></asp:TextBox>
                    <asp:HyperLink ID="hlkDATE" runat="server" CssClass="HotSpot">
                        <asp:Image ID="imgDATE" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif">
                        </asp:Image>
                    </asp:HyperLink>
                    <asp:RegularExpressionValidator ID="revDATE" runat="server" Display="Dynamic" ControlToValidate="txtDATE"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvDATE" runat="server" ControlToValidate="txtDATE"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                 <td>
                 </td>
            </tr>
            <tr>
                <td class="midcolora" width="30%">
                    <asp:Button ID="btnPagnet" runat="server" CssClass="clsButton" Text="Pagnet File"
                        Width="127px" Enabled="true" OnClick="btnPagnet_Click" />
                </td>
                <td class="midcolora" width="35%">
                    <asp:DropDownList ID="ddl" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddl_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td width="35%">
                    <asp:DropDownList ID="ddlFilelist" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="midcolora" colspan="3">
                    <asp:Button ID="btnMelEvent" CssClass="clsButton" runat="server" Text="MelEvent File"
                        Width="127px" Enabled="true" OnClick="btnMelEvent_Click" />
                </td>
            </tr>
            <tr>
                <td class="midcolora" colspan="3" align="center">
                    <asp:Label ID="lblErr" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
        </table> 
   </div>
    </form>
</body>
</html>

<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Page language="c#" Codebehind="QuestionTab.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.UserDefined.QuestionTab" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>User Questions</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
	</HEAD>
	<body oncontextmenu = "return false;" MS_POSITIONING="GridLayout" onload="top.topframe.main1.mousein = false;findMouseIn();">
		<!-- To add bottom menu -->
		<webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<form id="QuestionTab" method="post" runat="server">
			<table cellpadding="0" cellspacing="0" border="0" class="tablewidth" align="center">
				<tr>
					<td colspan="2" class="pageHeader"><webcontrol:ClientTop id="cltClientTop" runat="server" width="100%"></webcontrol:ClientTop></td>
				</tr>
				<tr>
					<td colspan="2" height="22px"></td>
				</tr>
				<tr height="25px">
					<td colspan="2" class="headereffectcenter">Choose the User Defined Screens</td>
				</tr>
				<tr class="midcolora" id="udScreen" runat="server" height="25px">
					<td align="center" valign="middle">User Defined Screens</td>
					<td valign="middle"><asp:DropDownList ID="ddlScreens" Runat="server"></asp:DropDownList></td>
				</tr>
				<tr id="noudScreen" runat="server">
					<td colspan="2" class="midcolorc">No User Defined Screen exists for the Policy.</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

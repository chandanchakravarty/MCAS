<%@ Page language="c#" Codebehind="AddClaims.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddClaims" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS - Add Claims</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script>
		function OpenDummyPolicyWindow()
		{
			ShowPopup('/cms/claims/Aspx/Policy/DummyPolicyPopUp.aspx','VehicleInformation', 600, 250);
			return false;
		}
	  
  
		</script>
	</HEAD>
	<body onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();" MS_POSITIONING="GridLayout">
	<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<form id="Form1" method="post" runat="server">			
			<table cellSpacing='1' cellPadding='1' width='100%' border='0'>
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<tr>
					<td align="left" colspan="4">
						<cmsb:cmsbutton class="clsButton" id="btnDummyPolicyPopUp" runat="server" Text="Add Unmatched Policy"></cmsb:cmsbutton>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

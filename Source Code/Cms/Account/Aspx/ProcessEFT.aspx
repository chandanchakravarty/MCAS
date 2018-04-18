<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="ProcessEFT.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.ProcessEFT" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
    <title>Process EFT</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script src="/cms/cmsweb/scripts/common.js"></script>
	<script src="/cms/cmsweb/scripts/form.js"></script>
</HEAD>
  <body oncontextmenu = "return false;" onload="setfirstTime();top.topframe.main1.mousein = false;showScroll();findMouseIn();">
	<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
	<!-- To add bottom menu ends here -->
	<div class="pageContent" id="bodyHeight">
    <form id="ProcessEFT" method="post" runat="server">
		<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
			<tr>
				<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
			</tr>
			<tr>
				<td class="headereffectcenter" colSpan="3">EFT Process</td>
			</tr>
			<tr>
				<td class="pageheader" align="center" colSpan="3">Please click the Button to start the EFT process.</td>
			</tr>
			<tr>
				<td class="midcolorc" align="center"><asp:Label ID="lblMessage" Runat="server" CssClass="mandatory" ></asp:Label></td>
			</tr>
			<tr>
				<td class="midcolorr" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnLaunchEFT" runat="server" Text="Start EFT Process"></cmsb:cmsbutton></td>
			</tr>
		</table>
     </form>
	</div>
  </body>
</HTML>

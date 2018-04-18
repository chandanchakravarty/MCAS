<%@ Page language="c#" Codebehind="Process1099.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.Process1099" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Process 1099</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta http-equiv="Page-Enter" content="revealtrans(duration=0.0)">
		<meta http-equiv="Page-Exit" content="revealtrans(duration=0.0)">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
	</HEAD>
	<body oncontextmenu = "return false;" class="bodyBackGround" topMargin="0" onload="ApplyColor();ChangeColor();top.topframe.main1.mousein = false;findMouseIn();showScroll();">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here START-->
		<form id="PROCESS_1099" method="post" runat="server">
			<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
				<TBODY>
					<TR>
						<TD class="headereffectcenter" colSpan="4">Process 1099</TD>
					</TR>
					<TR>
						<TD class="midcolorc" align="center" colSpan="4">
							<asp:label id="lblMessage" runat="server" EnableViewState="False" Visible="False" CssClass="errmsg"></asp:label></TD>
					</TR>
					<TR>
						<td class="midcolora" align="center"><asp:label id="lblProcess" runat="server" EnableViewState="False"></asp:label></td>
						<TD class="midcolorr" colSpan="2"><CMSB:CMSBUTTON class="clsButton" id="btnProcess" runat="server" Text="Process 1099"></CMSB:CMSBUTTON></TD>
					</TR>
				</TBODY>
			</table>
			<INPUT id="hidYear" type="hidden" value="0" name="hidYear" runat="server">
			<INPUT id="hidFiscalID" type="hidden" value="0" name="hidFiscalID" runat="server">
		</form>
	</body>
</HTML>

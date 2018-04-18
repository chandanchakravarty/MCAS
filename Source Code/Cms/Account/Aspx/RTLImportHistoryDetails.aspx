<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="RTLImportHistoryDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.RTLImportHistoryDetails" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title><%=Strtitle%></title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		function showPrint()
		{
			spn_Button.style.display = "none"
			bgcolor = buttons.style.background
			buttons.style.background = "white"
			window.print()
			spn_Button.style.display = "inline"
		}
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" class="bodyBackGround" topmargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing='1' cellPadding='0' border='0' class='tableWidth' align="center">
				<tr>
					<td>
						<webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer>
					</td>
				</tr>
				<tr>
					<td class="headereffectCenter" colspan="2"><asp:Label ID="capHeader" runat="server"></asp:Label> </td><%--RTL Import History--%>
				</tr>
				<tr>
					<td class="midcolorr" width="50%"><asp:Label ID="capDate" runat ="server"></asp:Label>&nbsp;</td><%--Today's Date :--%>
					<td class="midcolora" width="50%"><%=DateTime.Now.ToShortDateString()%></td>
				</tr>
				<tr>
					<td class="midcolorr" width="50%"><asp:Label ID="capTime" runat="server"></asp:Label>  &nbsp;</td><%--Time : --%>
					<td class="midcolora" width="50%"><%=DateTime.Now.ToString("hh:mm:ss tt")%></td>
				</tr>
				<tr>
					<td class="midcolorc" colspan="2"><b><asp:Label id="capRTLImportHistory" runat="server"></asp:Label></b></td><%--RTL Import History--%>
				</tr>
				<tr>
					<td class="midcolora" colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td class="errmsg"><asp:Label ID="lblMessage" Runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td id="tdRTLImportHistoryDetails" runat="server" colspan="2"></td>
				</tr>
				<tr>
				<td><input type="hidden" id="hidDeposit" runat="server" /></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

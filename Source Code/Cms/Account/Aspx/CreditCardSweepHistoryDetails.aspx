<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Page language="c#" Codebehind="CreditCardSweepHistoryDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.CreditCardSweepHistoryDetails" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Credit Card Sweep History Details</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
					<td class="headereffectCenter" colspan="2">
						Credit Card Sweep History
					</td>
				</tr>
				<tr>
					<td class="midcolorr" width="50%">
						Today's Date : &nbsp;
					</td>
					<td class="midcolora" width="50%">
						<%=DateTime.Now.ToString("MM/dd/yyyy")%>
					</td>
				</tr>
				<tr>
					<td class="midcolorr" width="50%">
						Time : &nbsp;
					</td>
					<td class="midcolora" width="50%">
						<%=DateTime.Now.ToString("hh:mm:ss tt")%>
					</td>
				</tr>
				<tr>
					<td class="midcolorc" colspan="2"><b>Credit Card Sweep History</b>
					</td>
				</tr>
				<tr>
					<td class="midcolora" colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td class="errmsg">
						<asp:Label ID="lblMessage" Runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<td id="tdCreditCardSweepHistoryDetails" runat="server" colspan="2">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

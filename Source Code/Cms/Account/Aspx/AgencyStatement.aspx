<%@ Page language="c#" Codebehind="AgencyStatement.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AgencyStatement" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AgencyStatement</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
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
	<body class="bodyBackGround" topmargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing='1' cellPadding='0' border='0' class='tableWidth' align="center">
				<tr>
					<td>
						<webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer>
					</td>
				</tr>
				<tr>
					<td class="headereffectCenter" colspan="2">
						Agency Statement
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
					<td class="midcolorc" colspan="2"><b>
						Agency statement for the month of 
						<%=MonthName%>  <%=MonthYear%></b>
					</td>
				</tr>
				<tr>
					<td class="midcolora" colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td class="midcolora">
						<asp:Label CssClass="LabelFont" ID="lblAgencyName" Runat="server"></asp:Label>
					</td>
					<td class="midcolorr">
						<SPAN>Tran Type -> NBS: New Business, EDS: Endorsement, REN: Renewal</SPAN> 
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
					<td id="tdAgencyStatementTD" runat="server" colspan="2">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

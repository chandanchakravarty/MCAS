<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="JournalEntryPreview.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.JournalEntryPreview" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Journal Entry Preview</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
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
	</head>
	<body class="bodyBackGround" leftMargin="0" topMargin="0" rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing='1' cellPadding='0' border='0' class='tableWidth' align="center">
				<tr>
					<td colspan="2">
						<webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer>
					</td>
				</tr>
				<tr>
					<td class="headereffectCenter" colspan="2">
						Journal Entry Items List
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
					<td class="midcolorr" width="50%">
						Journal Entry Number : &nbsp;
					</td>
					<td class="midcolora" width="50%">
						<%=journal_num%>
					</td>
				</tr>
				<tr>
					<td class="midcolorr" width="50%">
						Journal Entry Type : &nbsp;
					</td>
					<td class="midcolora" width="50%">
						<%=journal_type%>
					</td>
				</tr>
				<tr>
					<td class="errmsg" colspan="2">
						<asp:Label ID="lblMessage" Runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<td id="myTd" runat="server" colspan="2"></td>
				</tr>
			</table>
		</form>
	</body>
</html>

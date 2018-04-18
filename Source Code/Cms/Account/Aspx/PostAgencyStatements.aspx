<%@ Page language="c#" Codebehind="PostAgencyStatements.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.PostAgencyStatements" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>PostAgencyStatements</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		function HideShowTransactionInProgress()
		{
			DisableButton(document.getElementById('btnPost'));
		}
		</script>
</HEAD>
	<body oncontextmenu = "return false;" class="bodyBackGround" topmargin="0" MS_POSITIONING="GridLayout" onload="top.topframe.main1.mousein = false;findMouseIn();showScroll();">
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<form id="Form1" method="post" runat="server">
			<table cellSpacing='1' cellPadding='0' border='0' class='tableWidth' align="center">
				<tr>
					<td class="headereffectcenter" colspan="2">
						Post Agency Statement
					</td>
				</tr>
				<tr>
					<td class="midcolorc" colspan="2">
						<asp:Label ID="lblMessage" Runat="server" CssClass="mandatory"></asp:Label>
					</td>
				</tr>
				<tr>
					<td class="midcolora">Post agency statement for the month of
					</td>
					<td class="midcolora">
						<asp:DropDownList ID="cmbMonth" Runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="cmbYEAR" Autopostback="true" Runat="server"></asp:DropDownList>
						<br>
						<asp:RequiredFieldValidator ID="rfvMonth" ControlToValidate="cmbMonth" Runat="server" Display="Dynamic" ErrorMessage="Please select month."></asp:RequiredFieldValidator>
						
					</td>					
				</tr>
				<TR>
					<TD class="midcolora" style="HEIGHT: 15px">Select Commision Type</TD>
					<TD class="midcolora" style="HEIGHT: 15px">
						<asp:DropDownList id="CmbCommType" Runat="server" Width="150px" Autopostback="true">
							<asp:ListItem Value="REG">Regular Commission</asp:ListItem>
							<asp:ListItem Value="ADC">Additional Commission</asp:ListItem>
							<asp:ListItem Value="CAC">Complete App Commission</asp:ListItem>
						</asp:DropDownList></TD>
				</TR>
				<tr>
					<td class="midcolora">
						<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" Visible="false"></cmsb:cmsbutton>
					</td>
					<td class="midcolorr">
						<cmsb:cmsbutton class="clsButton" id="btnPost" runat="server" Text="Post"></cmsb:cmsbutton>
					</td>
				</tr>
			</table>
			<input id="hidAGENCY_ID" type="hidden" name="hidAGENCY_ID" runat="server">
		</form>
	</body>
</HTML>

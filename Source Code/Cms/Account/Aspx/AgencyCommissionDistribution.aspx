<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AgencyCommissionDistribution.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AgencyCommissionDistribution" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Agency Commission Distribution</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script language="javascript">
		function refreshParent()
		{
			window.opener.__doPostBack('btnFind', null);
		}
		</script>
</HEAD>
	<body class="bodyBackGround" leftMargin="20" onload="ApplyColor();ChangeColor();" rightMargin="20"
		MS_POSITIONING="GridLayout" onunload="refreshParent();">
		<form id="AGEN_COMM_DISTRIBUTION" method="post" runat="server">
			<table cellSpacing="1" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="headereffectcenter" colSpan="2">Select Account against which difference 
						amount will be adjusted.
					</td>
				</tr>
				<tr>
					<td class="midcolorc" colSpan="2" align="center"><asp:label id="lblMessage" CssClass="errmsg" Runat="server"></asp:label></td>
				</tr>
				<TR>
					<TD class="midcolora" style="HEIGHT: 15px">Balance Amount</TD>
					<TD class="midcolora" style="HEIGHT: 15px"><asp:label id="lblBalAmt" Runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="midcolora" style="HEIGHT: 15px">Difference Amount</TD>
					<TD class="midcolora" style="HEIGHT: 15px"><asp:label id="lblDiffAmt" Runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="midcolora" style="HEIGHT: 15px">Uncollected Premium Amount AB</TD>
					<TD class="midcolora" style="HEIGHT: 15px">
						<asp:label id="lblUncolPremAB" Runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="midcolora" style="HEIGHT: 15px">Commission Payable Amount AB</TD>
					<TD class="midcolora" style="HEIGHT: 15px">
						<asp:label id="lblCommPayAB" Runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD class="midcolora" style="HEIGHT: 15px">Commission Payable Amount DB</TD>
					<TD class="midcolora" style="HEIGHT: 15px">
						<asp:label id="lblCommPayDB" Runat="server"></asp:label></TD>
				</TR>
				<TR id="trAdjAcc" runat="server">
					<TD class="midcolora" style="HEIGHT: 15px">Select Adjustment Account</TD>
					<TD class="midcolora" style="HEIGHT: 15px"><asp:dropdownlist id="cmbACCOUNT_ID" runat="server">
							<asp:ListItem Value="0">0</asp:ListItem>
						</asp:dropdownlist><br>
						<asp:requiredfieldvalidator id="rfvACCOUNT_ID" runat="server" ControlToValidate="cmbACCOUNT_ID" Display="Dynamic"
							ErrorMessage="ACCOUNT_ID can't be blank."></asp:requiredfieldvalidator></TD>
				</TR>
				<tr id="trDESC" runat="server">
					<TD class="midcolora" style="HEIGHT: 15px">Description</TD>
					<TD class="midcolora" style="HEIGHT: 15px">
						<asp:TextBox ID="txtDESCRIPTION" Runat="server" Width="337px"></asp:TextBox></TD>
				</tr>
				<tr>
					<td class="midcolora"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Visible="false" Text="Reset"></cmsb:cmsbutton></td>
					&nbsp;
					<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnApplyAmount" runat="server" Text="Apply Amount"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnClose" runat="server" Text="Close"></cmsb:cmsbutton></td>
				</tr>
			</table>
		</form>
		<input id="hidCLIENT_ID" type="hidden" runat="server" NAME="hidCLIENT_ID"> <input id="hidAccountID" type="hidden" runat="server" NAME="hidAccountID">
	</body>
</HTML>

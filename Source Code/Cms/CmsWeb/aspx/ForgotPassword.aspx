<%@ Page language="c#" Codebehind="ForgotPassword.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.aspx.ForgotPassword" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/Cms/CmsWeb/Css/Login.css" type="text/css" rel="stylesheet">
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<SCRIPT src="/cms/cmsweb/scripts/common.js"></SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="150" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table border="0" cellpadding="0" cellspacing="0" align="center">
				<tr>
					<td class="pageHeader" width="100%" colSpan="4">Please note that all fields marked 
						with * are mandatory</td>
				</tr>
				<tr>
					<td class="midcolorc" align="center" width="100%" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<tr>
					<td class="wmic" align="center" width="50%">Email Id<span class="mandatory">*</span></td>
					<td class="wmic" align="center" width="50%"><asp:TextBox ID="txtEmailId" Runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="wmic" align="center" width="50%">User Id<span class="mandatory">*</span></td>
					<td class="wmic" align="center" width="50%"><asp:TextBox ID="txtUserId" Runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="wmic" align="center" width="50%"></td>
					<td class="wmic" align="center" width="50%"><asp:Button ID="btnGetPassword" Runat="server" Text="Get Password"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

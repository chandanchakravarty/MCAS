<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" Codebehind="PolicyShowDiscountAndSurcharges.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.PolicyShowDiscountAndSurcharges" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
    <title>PolicyShowDiscountAndSurcharges</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
</HEAD>
  <BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
						<TD colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow></TD>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<td><asp:label id="FinalLAbel" Runat="server"></asp:label></td>
							</tr>
							<tr id="errorLabel" runat="server">
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg"></asp:label></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</BODY>
</HTML>

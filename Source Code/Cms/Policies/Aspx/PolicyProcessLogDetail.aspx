<%@ Page language="c#" Codebehind="PolicyProcessLogDetail.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.PolicyProcessLogDetail" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>TransactionLogDetail</TITLE>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
	</HEAD>
	<BODY class="bodyBackGround" leftMargin="0" topMargin="0">
		<FORM id="Transaction_Log" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg"></asp:label></td>
				</tr>
				<tr>
					<td class="midcolora" width="2%">&nbsp;</td>
					<td width="88%">
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td class="midcolora">&nbsp;</td>
							</tr>
							<tr>
								<TD class="midcolora"><asp:label id="capDescription" runat="server" Font-Bold="True">Description: </asp:label><asp:label id="lblDescription" runat="server"></asp:label></TD>
							</tr>
							<tr>
								<TD class="midcolora"><asp:label id="capAppNumber" runat="server" Font-Bold="True">Application Number: </asp:label><asp:label id="lblAppNumber" runat="server"></asp:label></TD>
							</tr>
							<tr>
								<TD class="midcolora"><asp:label id="capVersionNnmber" runat="server" Font-Bold="True">Version Number: </asp:label><asp:label id="lblVersionNumber" runat="server"></asp:label></TD>
							</tr>
							<tr>
								<td class="midcolora">
									<table id="tblHeadings" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0"
										runat="server">
									</table>
								</td>
							</tr>
							<tr>
								<td class="midcolora">&nbsp;
								</td>
							</tr>
							<TR>
								<TD class="midcolora"><asp:literal id="ltCoverage" Runat="server"></asp:literal><asp:datagrid id="dgTrans" runat="server" HeaderStyle-CssClass="HeadRow" AutoGenerateColumns="False">
										<Columns>
											<asp:BoundColumn HeaderText="Label Name" DataField="LableName" ItemStyle-CssClass="DataRow" ItemStyle-Font-Bold="True"></asp:BoundColumn>
											<asp:BoundColumn HeaderText="Modified From" DataField="OldValue" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
											<asp:BoundColumn HeaderText="Modified To" DataField="NewValue" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
										</Columns>
									</asp:datagrid></TD>
							</TR>
						</table>
					</td>
					<td class="midcolora" width="10%">&nbsp;</td>
				</tr>
				<tr>
					<td class="midcolora" colSpan="4">&nbsp;</td>
				</tr>
			</TABLE>
		</FORM>
	</BODY>
</HTML>


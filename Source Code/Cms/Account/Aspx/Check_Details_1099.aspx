<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page  validateRequest=false language="c#" Codebehind="Check_Details_1099.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.Check_Details_1099" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>BRICS-1099 Check Details</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=base.GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		
				
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE cellPadding="0" cellspacing ="0" width="90%" align="center" border="0">
				<tr>
					<TD class="headereffectCenter" colSpan="5">1099 Check Details</TD>
				</tr>
				<tr>
						<td class="midcolorc" align="right" colSpan="5"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="5" id="Grid">
						<asp:datagrid id="grd1099CheckDetails" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headereffectCenter"
							ItemStyle-CssClass="datarow" AlternatingItemStyle-CssClass="alternatedatarow" Width="100%">
							<Columns>
								<ASP:BOUNDCOLUMN HeaderText="Ref No" DataField="REF_NUMBER" ></ASP:BOUNDCOLUMN>
								<ASP:BOUNDCOLUMN HeaderText="Date" DataField="DATE"></ASP:BOUNDCOLUMN>
								<ASP:BOUNDCOLUMN HeaderText="Claim Number" DataField="CLAIM_NUMBER"></ASP:BOUNDCOLUMN>
								<ASP:BOUNDCOLUMN HeaderText="Amount" DataField="AMOUNT"></ASP:BOUNDCOLUMN>
								<ASP:BOUNDCOLUMN HeaderText="Type" DataField="TYPE"></ASP:BOUNDCOLUMN>	
								<ASP:BOUNDCOLUMN HeaderText="Payee Name" DataField="PAYEE_ENTITY_NAME"></ASP:BOUNDCOLUMN>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td class="midcolora" colSpan="5">&nbsp;</td>
				</tr>
				<tr>
						<td class="midcolorc" align="right" colSpan="5"><asp:label id="lblTransMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR>
						<TD class="midcolora"><asp:literal id="ltCoverage" Runat="server"></asp:literal></TD>
				</TR>
				
			</TABLE>
		</form>
		
	</body>
</HTML>

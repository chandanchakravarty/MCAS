<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Page language="c#" Codebehind="ReinsuranceBreakdown.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Reinsurance.ReinsuranceBreakdown" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>BRICS Reinsurance Breakdown</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
  </head>
  <body MS_POSITIONING="GridLayout">
	<form id="REINSURANCE_BREAKDOWN" method="post" runat="server">
			<div id="bodyHeight" class="pageContent">
				<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td align="center">
							<asp:Label ID="capMessage" Runat="server" Visible="True" CssClass="errmsg"></asp:Label>
						</td>
					</tr>
					<tr id="formTable" runat="server">
						<td>
							<table class="tableWidth" cellSpacing="0" cellPadding="0" border="0" align="center">
								<asp:Label ID="lblTemplate" Runat="server" Visible="True"></asp:Label>
							</table>
						</td>
					</tr>
				</table>
				<DIV>
					<table class="tableWidth" cellSpacing="0" cellPadding="0" border="0" align="center">
						<asp:Label ID="lblDetailTemplate" Runat="server" Visible="True"></asp:Label>
					</table>
				</DIV>
				<DIV>
					<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
						</TBODY>
						<tbody id="tbDataGrid" runat="server">
							<tr>
								<td class="midcolora" colSpan="4"><asp:datagrid id="dgReinBreakdown" Runat="server" Width="100%" ItemStyle-CssClass="datarow" HeaderStyle-CssClass="headereffectCenter"
										AllowPaging="True" PageSize="100" PagerStyle-Mode="NextPrev" PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="FALSE"
										PagerStyle-CssClass="datarow" PagerStyle-NextPageText="Next >>" PagerStyle-PrevPageText="<< Prev" AlternatingItemStyle-CssClass="alternatedatarow" OnPageIndexChanged="dgReinBreakdown_Paging">
										<COLUMNS>
											<asp:TemplateColumn ItemStyle-Width="36%" HeaderText="Complete">
												<ItemTemplate>
													<asp:Label runat="server" ID="lblCOMPLETE" Text='<%# DataBinder.Eval(Container, "DataItem.COMPLETED") %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<ASP:BOUNDCOLUMN HeaderText="Statement Date" DataField="TRAN_DATE"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Contract #" DataField="CONTRACT_NUMBER"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Premium Basis"  DataField="PREMIUM_BASE"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Contract Year"   DataField="CONTRACT_YEAR"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Total Insurance Value" DataField="TOTAL_INS_VALUE"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="TIV Reinsurance Group" DataField="TIV_GROUP"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Layer" DataField="LAYER"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Layer Amount" DataField="LAYER_AMOUNT"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Reinsurance Limit /FAB" DataField="REIN_LIMIT_FAB"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Coverage Category" DataField="COV_CATEGORY"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Transaction Premium" DataField="TRAN_PREMIUM"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Earned" DataField="EARNED"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Written" DataField="WRITTEN"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Construction" DataField="CONSTRACTION"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Protection" DataField="PROTECTION"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Central Alarm" DataField="CENTRAL_ALARM"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="New Home" DataField="NEW_HOME"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Age Of Home" DataField="HOME_AGE"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Deduction Layer 1" DataField="DEDUCT_LAYER_1"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Deduction Layer 2" DataField="DEDUCT_LAYER_2"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Rate" DataField="RATE"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Reinsurance Premium" DataField="REIN_PREMIUM"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Commission %" DataField="COMM_PERCENTAGE"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Comm. Amount" DataField="COMM_AMOUNT"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Net Due" DataField="NET_DUE"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Major Participant" DataField="MAJOR_PARTICIPANT"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Retention %" DataField="RETENTION_PER"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Reinsurance Ceded $" DataField="REIN_CEDED"></ASP:BOUNDCOLUMN>
										</COLUMNS>
									</asp:datagrid></td>
							</tr>
						</tbody>
				
					<TBODY >
					</table>
				</DIV>
				<DIV>
					<table class="tableWidth" cellSpacing="0" cellPadding="0" border="0" align="center">
					<tr>
						<td class="midcolora" align="center">
							<cmsb:cmsbutton class="clsButton" id="btnExportExcel" runat="server" Text='Export to Excel'></cmsb:cmsbutton>
						</td>
					</tr>						
					</table>
				</DIV>
				<input type="hidden" name="hidTabNumber" runat="server" id="hidTabNumber">
			</div>
    </form>
  </body>
</html>

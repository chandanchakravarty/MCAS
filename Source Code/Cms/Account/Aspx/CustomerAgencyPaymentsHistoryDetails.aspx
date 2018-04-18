<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="CustomerAgencyPaymentsHistoryDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.CustomerAgencyPaymentsHistoryDetails" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CustomerAgencyPaymentsHistoryDetails</title>
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
		//Grid Added FGor Itrack Issue #6610.
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
					<td class="headereffectCenter" colspan="3">
						Customer Agency Payments History
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
					<td class="midcolorc" colspan="2"><b>Customer Agency Payments History</b>
					</td>
				</tr>	
				<tr>
					<td class="midcolora" colspan="4">&nbsp;</td>
				</tr>			
				</table>
				<table cellSpacing='1' cellPadding='0' border='0' class='tableWidth' align="center">
				<tbody id="tbDataGrid" runat="server">
							<tr>
								<td class="midcolora" colSpan="4">
									<div id="divPrint">
										<asp:datagrid id="dgVenPendInv" Runat="server" Width="100%" ItemStyle-CssClass="DataRow2" HeaderStyle-CssClass="headereffectCenter"
											PageSize="15" PagerStyle-Mode="NumericPages" PagerStyle-HorizontalAlign="Center"
											AutoGenerateColumns="FALSE" PagerStyle-CssClass="datarow" 
											 AlternatingItemStyle-CssClass="alternatedatarow" AllowSorting="true" OnSortCommand="Sort_Grid">
											<Columns>
												<asp:BoundColumn DataField="CUSTOMER_NAME" HeaderText="Customer Name" HeaderStyle-ForeColor="white"></asp:BoundColumn>
												<asp:BoundColumn DataField="USER_NAME" HeaderText="Received By" HeaderStyle-ForeColor="white"></asp:BoundColumn>
												<asp:BoundColumn DataField="POLICY_NUMBER" HeaderText="Policy Number"  HeaderStyle-ForeColor="white"></asp:BoundColumn>
												<asp:BoundColumn DataField="AGENCY_NAME" HeaderText="Agency" SortExpression="AGENCY_NAME" HeaderStyle-ForeColor="white"></asp:BoundColumn>												
												<ASP:TEMPLATECOLUMN HeaderText="AMOUNT" ItemStyle-HorizontalAlign="Right"  HeaderStyle-ForeColor="white">
													<ITEMTEMPLATE>
														<ASP:LABEL runat ="server" id="lbl_BalAmount" Text = '<%# FormatMoney(DataBinder.Eval(Container,"DataItem.AMOUNT"))%>'></ASP:LABEL>
													</ITEMTEMPLATE>
												</ASP:TEMPLATECOLUMN>
												<asp:BoundColumn DataField="DATE_COMMITTED" HeaderText="Date Of Payment" HeaderStyle-ForeColor="white"></asp:BoundColumn>
												<asp:BoundColumn DataField="MODE" HeaderText="Mode Of Payment"  HeaderStyle-ForeColor="white"></asp:BoundColumn>												
											</Columns>
										</asp:datagrid>
									</div>
								</td>
							</tr>
							</tbody>							
							<tr id = "trPrint" Runat="server">
								<td class="midcolora" colspan ="2"><input type ="button" id="btnPrint" class="clsButton" value = "Print" onClick = "window.print()"></td>
								<td class="midcolorr"><input type ="button" id="btnClose" class="clsButton" value = "Close" onClick = "window.close()"></td>																
							</tr>							
				
				<tr>
					<td class="errmsg">
						<asp:Label ID="lblMessage" Runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<td id="tdCustomerAgencyPaymentsHistory" runat="server" colspan="2">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

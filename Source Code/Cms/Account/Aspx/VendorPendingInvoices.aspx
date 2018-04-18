<%@ Page language="c#" Codebehind="VendorPendingInvoices.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.VendorPendingInvoices" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>VendorPendingInvoices</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Page-Enter" content="revealtrans(duration=0.0)">
		<meta http-equiv="Page-Exit" content="revealtrans(duration=0.0)">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		//Formats the amount and convert 111 into 1.11
		function FormatAmount(txtAmount)
		{
						
			if (txtAmount.value != "")
			{
				amt = txtAmount.value;
				
				amt = ReplaceAll(amt,".","");
				
				if (amt.length == 1)
					amt = amt + "0";
				
				if ( ! isNaN(amt))
				{
					
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					txtAmount.value = InsertDecimal(amt);
				}
			}
		}
		
		function callPrint(strid)
		{
			//var prtContent = document.getElementById(strid);
			//var WinPrint = window.open('','','left=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
			//WinPrint.document.write(prtContent.innerHTML);
			//WinPrint.document.close();
			//WinPrint.focus();
			//WinPrint.print();
			//WinPrint.close();
		}
		</script>
	</HEAD>
	<body oncontextmenu="return false;" onload="setfirstTime();ApplyColor();ChangeColor();top.topframe.main1.mousein = false;showScroll();findMouseIn();">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<div class="pageContent" id="bodyHeight">
			<form id="VendorPendingInvoices" method="post" runat="server">
				<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
					<TBODY>
						<tr>
							<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
						</tr>
						<tr>
							<td class="pageheader" align="center" colSpan="3">Please select Run Report Button 
								to view Vendor Pending Invoices</td>
						</tr>
						<tr>
							<td class="headereffectcenter" colSpan="3">Vendor Pending Invoices</td>
						</tr>
						<tr> <!-- Transaction Date -->
							<td class="midcolora" width="18%">Transaction Date</td>
							<td class="midcolora" width="32%">From<asp:textbox id="txtFROM_TRAN_DATE" size="12" Runat="server"></asp:textbox>
								<asp:hyperlink id="hlkFROM_TRAN_DATE" runat="server" CssClass="HotSpot">
									<asp:image id="imgFROM_TRAN_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
								</asp:hyperlink><br>
								<asp:regularexpressionvalidator id="revFROM_TRAN_DATE" Runat="server" ControlToValidate="txtFROM_TRAN_DATE" Display="Dynamic"></asp:regularexpressionvalidator></td>
							<td class="midcolora" width="18%">To<asp:textbox id="txtTO_TRAN_DATE" size="12" Runat="server"></asp:textbox>
								<asp:hyperlink id="hlkTO_TRAN_DATE" runat="server" CssClass="HotSpot">
									<asp:image id="imgTO_TRAN_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
								</asp:hyperlink><br>
								<asp:comparevalidator id="cmpTO_TRAN_DATE" Runat="server" Display="Dynamic" ControlToValidate="txtTO_TRAN_DATE"
									ControlToCompare="txtFROM_TRAN_DATE" Type="Date" Operator="GreaterThanEqual" ErrorMessage=" To Date should be greater than From Date"></asp:comparevalidator>
								<asp:regularexpressionvalidator id="revTO_TRAN_DATE" Runat="server" ControlToValidate="txtTO_TRAN_DATE" Display="Dynamic"></asp:regularexpressionvalidator></td>
						</tr>
						<tr> <!-- Due Date -->
							<td class="midcolora" width="18%">Due Date</td>
							<td class="midcolora" width="32%">From<asp:textbox id="txtFROM_DUE_DATE" size="12" Runat="server"></asp:textbox>
								<asp:hyperlink id="hlkFROM_DUE_DATE" runat="server" CssClass="HotSpot">
									<asp:image id="imgFROM_DUE_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
								</asp:hyperlink><br>
								<asp:regularexpressionvalidator id="revFROM_DUE_DATE" Runat="server" ControlToValidate="txtFROM_DUE_DATE" Display="Dynamic"></asp:regularexpressionvalidator></td>
							<td class="midcolora" width="18%">To<asp:textbox id="txtTO_DUE_DATE" size="12" Runat="server"></asp:textbox>
								<asp:hyperlink id="hlkTO_DUE_DATE" runat="server" CssClass="HotSpot">
									<asp:image id="imgTO_DUE_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
								</asp:hyperlink><br>
								<asp:comparevalidator id="cmpTO_DUE_DATE" Runat="server" Display="Dynamic" ControlToValidate="txtTO_DUE_DATE"
									ControlToCompare="txtFROM_DUE_DATE" Type="Date" Operator="GreaterThanEqual" ErrorMessage="To Date should be greater than From Date"></asp:comparevalidator>
								<asp:regularexpressionvalidator id="revTO_DUE_DATE" Runat="server" ControlToValidate="txtTO_TRAN_DATE" Display="Dynamic"></asp:regularexpressionvalidator></td>
						</tr>
						<tr> <!-- Amount -->
							<TD class="midcolora" width="18%"><asp:label id="capFROM_AMOUNT" runat="server">Amount</asp:label></TD>
							<TD class="midcolora" width="32%">From<asp:textbox class="INPUTCURRENCY" id="txtFROM_AMOUNT" style="TEXT-ALIGN: right" runat="server"
									maxlength="9"></asp:textbox><BR>
								<asp:regularexpressionvalidator id="revFROM_AMOUNT" runat="server" ControlToValidate="txtFROM_AMOUNT" Display="Dynamic"
									ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator></TD>
							<TD class="midcolora" width="18%">To<asp:textbox class="INPUTCURRENCY" id="txtTO_AMOUNT" style="TEXT-ALIGN: right" runat="server"
									maxlength="9"></asp:textbox><BR>
								<asp:regularexpressionvalidator id="revTO_AMOUNT" runat="server" ControlToValidate="txtTO_AMOUNT" Display="Dynamic"
									ErrorMessage="revTO_AMOUNT"></asp:regularexpressionvalidator></TD>
						</tr>
						<tr>
							<TD class="midcolora" width="18%"><asp:label id="capVENDOR_ID" runat="server">Vendor Name</asp:label></TD>
							<TD class="midcolora" width="32%" colSpan="2"><asp:dropdownlist id="cmbVENDOR_ID" onfocus="SelectComboIndex('cmbVENDOR_ID')" runat="server">
									<asp:ListItem Value='0'></asp:ListItem>
								</asp:dropdownlist></TD>
						</tr>
						<tr>
							<td class="midcolora" width="18%" colSpan="4"><cmsb:cmsbutton class="clsButton" id="btnReport" runat="server" Text="Run Report"></cmsb:cmsbutton></td>
						</tr>
						<tr>
							<td><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></td>
						</tr>
						<tr>
							<td align="center" colspan="4"><asp:Label ID="lblDatagrid" Runat="server" Visible="False" CssClass="errmsg"></asp:Label></td>
						</tr>
						<tbody id="tbDataGrid" runat="server">
							<tr>
								<td class="midcolora" colSpan="4">
									<div id="divPrint">
										<asp:datagrid id="dgVenPendInv" Runat="server" Width="100%" ItemStyle-CssClass="datarow" HeaderStyle-CssClass="headereffectCenter"
											AllowPaging="True" PageSize="15" PagerStyle-Mode="NumericPages" PagerStyle-HorizontalAlign="Center"
											AutoGenerateColumns="FALSE" PagerStyle-CssClass="datarow" PagerStyle-NextPageText="Next >>"
											PagerStyle-PrevPageText="<< Prev" AlternatingItemStyle-CssClass="alternatedatarow" OnPageIndexChanged="dgVenPendInv_Paging" AllowSorting="true" OnSortCommand="Sort_Grid">
											<Columns>
												<asp:BoundColumn DataField="COMPANY_NAME" HeaderText="Company Name" SortExpression="COMPANY_NAME" HeaderStyle-ForeColor="white"></asp:BoundColumn>
												<asp:BoundColumn DataField="COMPANY_CODE" HeaderText="Company Code" SortExpression="COMPANY_CODE" HeaderStyle-ForeColor="white"></asp:BoundColumn>
												<asp:BoundColumn DataField="COMPANY_ADDRESS" HeaderText="Company Address" SortExpression="COMPANY_ADDRESS" HeaderStyle-ForeColor="white"></asp:BoundColumn>
												<asp:BoundColumn DataField="TRANSACTION_DATE" HeaderText="Transaction Date" SortExpression="TRANSACTION_DATE" HeaderStyle-ForeColor="white"></asp:BoundColumn>
												<asp:BoundColumn DataField="DUE_DATE" HeaderText="Due Date" SortExpression="DUE_DATE" HeaderStyle-ForeColor="white"></asp:BoundColumn>
												<ASP:TEMPLATECOLUMN HeaderText="Balance Amount" ItemStyle-HorizontalAlign="Right" SortExpression="BALANCE_AMOUNT" HeaderStyle-ForeColor="white">
													<ITEMTEMPLATE>
														<ASP:LABEL runat ="server" id="lbl_BalAmount" Text = '<%# FormatMoney(DataBinder.Eval(Container,"DataItem.BALANCE_AMOUNT"))%>'></ASP:LABEL>
													</ITEMTEMPLATE>
												</ASP:TEMPLATECOLUMN>
											</Columns>
										</asp:datagrid>
									</div>
								</td>
							</tr>
							<tr>
								<td class="midcolora" width="18%" colSpan="4"><cmsb:cmsbutton class="clsButton" id="btnPrint" runat="server" Text="Print Report"></cmsb:cmsbutton></td>
							</tr>
							</TR>
						</tbody>
				</table>
			</form>
		</div>
	</body>
</HTML>

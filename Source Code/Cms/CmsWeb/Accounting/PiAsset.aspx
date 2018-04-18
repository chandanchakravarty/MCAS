<%@ Page validateRequest=false language="c#" Codebehind="PiAsset.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Accounting.PiAsset" %>
<%@ Register  TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Posting interface Asset</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>

		<script language='javascript'>
function AddData()
{

	DisableValidators();
	document.getElementById('hidGL_ID').value	=	'New';
	document.getElementById('cmbAST_UNCOLL_PRM_CUSTOMER').focus();
	document.getElementById('cmbAST_UNCOLL_PRM_CUSTOMER').options.selectedIndex = -1;
	document.getElementById('cmbAST_UNCOLL_PRM_AGENCY').options.selectedIndex = -1;
	document.getElementById('cmbAST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL').options.selectedIndex = -1;
	document.getElementById('cmbAST_PRM_WRIT_SUSPENSE_DIRECT_BILL').options.selectedIndex = -1;
	document.getElementById('cmbAST_PRM_WRIT_SUSPENSE_AGENCY_BILL').options.selectedIndex = -1;
	document.getElementById('cmbAST_MCCA_FEE_SUSPENSE_DIRECT_BILL').options.selectedIndex = -1;
	document.getElementById('cmbAST_MCCA_FEE_SUSPENSE_AGENCY_BILL').options.selectedIndex = -1;
	document.getElementById('cmbAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL').options.selectedIndex = -1;
	document.getElementById('cmbAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL').options.selectedIndex = -1;
	document.getElementById('cmbAST_COMM_RECV_REINS_EXCESS_CONTRACT').options.selectedIndex = -1;
	document.getElementById('cmbAST_COMM_RECV_REINS_UMBRELLA_CONTRACT').options.selectedIndex = -1;
	document.getElementById('cmbAST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL').options.selectedIndex = -1;
	document.getElementById('cmbAST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL').options.selectedIndex = -1;
	document.getElementById('cmbAST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL').options.selectedIndex = -1;
	document.getElementById('cmbAST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL').options.selectedIndex = -1;
	document.getElementById('cmbAST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL').options.selectedIndex = -1;
	ChangeColor();
}
function populateXML()
{
	HiddenFields();
var tempXML = document.getElementById('hidOldData').value;
//alert(tempXML);

if(document.getElementById('hidFormSaved').value == '0')
{
	AddData();
	if(tempXML!="")
	{
		populateFormData(tempXML,ACT_GENERAL_LEDGER);
	}
	//else
	//{
	//	AddData();
	//}
	ChangeColor();
}

return false;
}


function HiddenFields()
{
	document.getElementById('tbHiddenFields').style.display='none';
	document.getElementById('rfvAST_COMM_RECV_REINS_UMBRELLA_CONTRACT').enabled = false;
	document.getElementById('rfvAST_COMM_RECV_REINS_EXCESS_CONTRACT').enabled = false;
	document.getElementById('rfvAST_MCCA_FEE_SUSPENSE_DIRECT_BILL').enabled = false;
	document.getElementById('rfvAST_MCCA_FEE_SUSPENSE_AGENCY_BILL').enabled = false;
}
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();'>
		<div style="OVERFLOW: auto" style="height : 450px">
			<FORM id='ACT_GENERAL_LEDGER' method='post' runat='server'>
				<TABLE width='100%' border='0' align='center'>
					<tr>
						<TD class="pageHeader" colSpan="4"><asp:Label ID="capMAN" runat="server"></asp:Label></TD> <%--Please note that all fields marked with * are mandatory--%>
					</tr>
					<tr>
						<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
					</tr>
					<tr>
						<TD class='midcolora' width='18%'>
							<asp:Label id="capAST_UNCOLL_PRM_CUSTOMER" runat="server">Uncollected Premiums from Customer</asp:Label><span class="mandatory">*</span></TD>
						<TD class='midcolora' width='32%'>
							<asp:DropDownList id='cmbAST_UNCOLL_PRM_CUSTOMER' OnFocus="SelectComboIndex('cmbAST_UNCOLL_PRM_CUSTOMER')"
								runat='server'>
								<asp:ListItem Value='0'></asp:ListItem>
							</asp:DropDownList><BR>
							<asp:requiredfieldvalidator id="rfvAST_UNCOLL_PRM_CUSTOMER" runat="server" ControlToValidate="cmbAST_UNCOLL_PRM_CUSTOMER"
								ErrorMessage="AST_UNCOLL_PRM_CUSTOMER can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
						<TD class='midcolora' width='18%'>
							<asp:Label id="capAST_UNCOLL_PRM_AGENCY" runat="server">Uncollected Premiums from Agency
</asp:Label><span class="mandatory">*</span></TD>
						<TD class='midcolora' width='32%'>
							<asp:DropDownList id='cmbAST_UNCOLL_PRM_AGENCY' OnFocus="SelectComboIndex('cmbAST_UNCOLL_PRM_AGENCY')"
								runat='server'>
								<asp:ListItem Value='0'></asp:ListItem>
							</asp:DropDownList><BR>
							<asp:requiredfieldvalidator id="rfvAST_UNCOLL_PRM_AGENCY" runat="server" ControlToValidate="cmbAST_UNCOLL_PRM_AGENCY"
								ErrorMessage="AST_UNCOLL_PRM_AGENCY can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
					</tr>
					<tr>
						<td class="headrow" colspan="4"><asp:Label ID="capDIRBIL" runat="server"></asp:Label></td><%--Direct Bill--%>
					</tr>
					<tr>
						<TD class='midcolora' width='18%'>
							<asp:Label id="capAST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL" runat="server" DESIGNTIMEDRAGDROP="1550">Commission Expense in Suspense-Direct Bill
</asp:Label><SPAN class="mandatory">*</SPAN></TD>
						<TD class="midcolora" width="32%">
							<asp:DropDownList id="cmbAST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL" onfocus="SelectComboIndex('cmbAST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL')"
								runat="server">
								<asp:ListItem Value='0'></asp:ListItem>
							</asp:DropDownList>
							<asp:requiredfieldvalidator id="rfvAST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL" runat="server" Display="Dynamic"
								ErrorMessage="AST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL can't be blank." ControlToValidate="cmbAST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL"></asp:requiredfieldvalidator><BR>
						</TD>
						<TD class="midcolora" width="18%">
							<asp:Label id="capAST_PRM_WRIT_SUSPENSE_DIRECT_BILL" runat="server">Premiums Written in Suspense - Direct  Bill</asp:Label><SPAN class="mandatory">*</SPAN></TD>
						<TD class="midcolora" width="32%">
							<asp:DropDownList id="cmbAST_PRM_WRIT_SUSPENSE_DIRECT_BILL" onfocus="SelectComboIndex('cmbAST_PRM_WRIT_SUSPENSE_DIRECT_BILL')"
								runat="server">
								<asp:ListItem Value='0'></asp:ListItem>
							</asp:DropDownList><BR>
							<asp:requiredfieldvalidator id="rfvAST_PRM_WRIT_SUSPENSE_DIRECT_BILL" runat="server" Display="Dynamic" ErrorMessage="AST_PRM_WRIT_SUSPENSE_DIRECT_BILL can't be blank."
								ControlToValidate="cmbAST_PRM_WRIT_SUSPENSE_DIRECT_BILL"></asp:requiredfieldvalidator></TD>
					</tr>
					<TR>
						<TD class="midcolora" width="18%">
							<asp:Label id="capAST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL" runat="server">Commission Payable in Suspense-Direct Bill
</asp:Label><SPAN class="mandatory">*</SPAN></TD>
						<TD class="midcolora" width="32%">
							<asp:DropDownList id="cmbAST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL" onfocus="SelectComboIndex('cmbAST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL')"
								runat="server">
								<asp:ListItem Value='0'></asp:ListItem>
							</asp:DropDownList><BR>
							<asp:requiredfieldvalidator id="rfvAST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL" runat="server" Display="Dynamic"
								ErrorMessage="AST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL can't be blank." ControlToValidate="cmbAST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL"></asp:requiredfieldvalidator></TD>
						<TD class="midcolora" width="18%">
							<asp:Label id="capAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL" runat="server">Other State Assessment Fees in Suspense - Direct Bill</asp:Label><SPAN class="mandatory">*</SPAN></TD>
						<TD class="midcolora" width="32%">
							<asp:DropDownList id="cmbAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL" onfocus="SelectComboIndex('cmbAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL')"
								runat="server">
								<asp:ListItem Value='0'></asp:ListItem>
							</asp:DropDownList><BR>
							<asp:requiredfieldvalidator id="rfvAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL" runat="server" Display="Dynamic"
								ErrorMessage="AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL can't be blank." ControlToValidate="cmbAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR>
						<TD class="midcolora" width="18%">
							<asp:Label id="capAST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL" runat="server">Uncollected Premium in Suspense-Direct Bill</asp:Label><SPAN class="mandatory">*</SPAN></TD>
						<TD class="midcolora" width="32%" colspan="3">
							<asp:DropDownList id="cmbAST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL" onfocus="SelectComboIndex('cmbAST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL')"
								runat="server">
								<asp:ListItem Value='0'></asp:ListItem>
							</asp:DropDownList><BR>
							<asp:requiredfieldvalidator id="rfvAST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL" runat="server" Display="Dynamic"
								ErrorMessage="AST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL can't be blank." ControlToValidate="cmbAST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR>
						<TD class="headrow" colSpan="4"><asp:Label ID="capAGENGYBIL" runat="server"></asp:Label></TD><%--Agency Bill--%>
					</TR>
					<TR>
						<TD class='midcolora' width='18%'>
							<asp:Label id="capAST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL" runat="server">Commission Expense in Suspense-Agency Bill</asp:Label><SPAN class="mandatory">*</SPAN></TD>
						<TD class='midcolora' width='32%'>
							<asp:DropDownList id="cmbAST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL" onfocus="SelectComboIndex('cmbAST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL')"
								runat="server">
								<asp:ListItem Value='0'></asp:ListItem>
							</asp:DropDownList>
							<asp:requiredfieldvalidator id="rfvAST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL" runat="server" Display="Dynamic"
								ErrorMessage="AST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL can't be blank." ControlToValidate="cmbAST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL"></asp:requiredfieldvalidator><BR>
						</TD>
						<TD class="midcolora" width="18%">
							<asp:Label id="capAST_PRM_WRIT_SUSPENSE_AGENCY_BILL" runat="server">Premiums Written in Suspense - Agency  Bill</asp:Label><SPAN class="mandatory">*</SPAN></TD>
						<TD class='midcolora'>
							<asp:DropDownList id='cmbAST_PRM_WRIT_SUSPENSE_AGENCY_BILL' OnFocus="SelectComboIndex('cmbAST_PRM_WRIT_SUSPENSE_AGENCY_BILL')"
								runat='server'>
								<asp:ListItem Value='0'></asp:ListItem>
							</asp:DropDownList>
							<asp:requiredfieldvalidator id="rfvAST_PRM_WRIT_SUSPENSE_AGENCY_BILL" runat="server" ControlToValidate="cmbAST_PRM_WRIT_SUSPENSE_AGENCY_BILL"
								ErrorMessage="AST_PRM_WRIT_SUSPENSE_AGENCY_BILL can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR>
						<TD class="midcolora" width="18%">
							<asp:Label id="capAST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL" runat="server">Commission Payable in Suspense-Agency Bill
						</asp:Label><SPAN class="mandatory">*</SPAN></TD>
						<TD class="midcolora" width="32%">
							<asp:DropDownList id="cmbAST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL" onfocus="SelectComboIndex('cmbAST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL')"
								runat="server">
								<asp:ListItem Value='0'></asp:ListItem>
							</asp:DropDownList><BR>
							<asp:requiredfieldvalidator id="rfvAST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL" runat="server" Display="Dynamic"
								ErrorMessage="AST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL can't be blank." ControlToValidate="cmbAST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL"></asp:requiredfieldvalidator></TD>
						<TD class="midcolora" width="18%">
							<asp:Label id="capAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL" runat="server">Other State Assessment Fees in Suspense - Agency Bill</asp:Label><SPAN class="mandatory">*</SPAN></TD>
						<TD class="midcolora" width="32%">
							<asp:DropDownList id="cmbAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL" onfocus="SelectComboIndex('cmbAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL')"
								runat="server">
								<asp:ListItem Value='0'></asp:ListItem>
							</asp:DropDownList><BR>
							<asp:requiredfieldvalidator id="rfvAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL" runat="server" Display="Dynamic"
								ErrorMessage="AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL can't be blank." ControlToValidate="cmbAST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL"></asp:requiredfieldvalidator></TD>
					</TR>
					<tr>
						<TD class='midcolora' width='18%'>
							<asp:Label id="capAST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL" runat="server">Uncollected Premiums in Suspense - Agency Bill</asp:Label><SPAN class="mandatory">*</SPAN></TD>
						<TD class='midcolora' width='32%' colspan="3">
							<asp:DropDownList id="cmbAST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL" onfocus="SelectComboIndex('cmbAST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL')"
								runat="server">
								<asp:ListItem Value='0'></asp:ListItem>
							</asp:DropDownList><BR>
							<asp:requiredfieldvalidator id="rfvAST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL" runat="server" Display="Dynamic" ErrorMessage="AST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL can't be blank."
								ControlToValidate="cmbAST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL"></asp:requiredfieldvalidator>
						</TD>
					</tr>
				<!-- HIDDEN FIELDS -->
					<tbody id="tbHiddenFields">
						<!--0-->
						<tr>
							<td class="headrow" colspan="4">Other</td>
						</tr>
						<!--1-->
						<tr>
							<TD class='midcolora' width='18%'>
								<asp:Label id="capAST_COMM_RECV_REINS_UMBRELLA_CONTRACT" runat="server">Commission Receivables Reinsurance - Umbrella Contract</asp:Label><SPAN class="mandatory">*</SPAN></TD>
							<TD class="midcolora" width="32%">
								<asp:DropDownList class="GrandFatheredRange" id="cmbAST_COMM_RECV_REINS_UMBRELLA_CONTRACT" onfocus="SelectComboIndex('cmbAST_COMM_RECV_REINS_UMBRELLA_CONTRACT')"
									runat="server">
									<asp:ListItem Value='0'></asp:ListItem>
								</asp:DropDownList><BR>
								<asp:requiredfieldvalidator id="rfvAST_COMM_RECV_REINS_UMBRELLA_CONTRACT" runat="server" Display="Dynamic" ErrorMessage="AST_COMM_RECV_REINS_UMBRELLA_CONTRACT can't be blank."
									ControlToValidate="cmbAST_COMM_RECV_REINS_UMBRELLA_CONTRACT"></asp:requiredfieldvalidator></TD>
							<TD class="midcolora" width="18%">
								<asp:Label id="capAST_COMM_RECV_REINS_EXCESS_CONTRACT" runat="server">Commission Receivables Reinsurance - Excess Contract</asp:Label><SPAN class="mandatory">*</SPAN></TD>
							<TD class="midcolora" colSpan="1">
								<asp:DropDownList class="GrandFatheredRange" id="cmbAST_COMM_RECV_REINS_EXCESS_CONTRACT" onfocus="SelectComboIndex('cmbAST_COMM_RECV_REINS_EXCESS_CONTRACT')"
									runat="server">
									<asp:ListItem Value='0'></asp:ListItem>
								</asp:DropDownList>
								<asp:requiredfieldvalidator id="rfvAST_COMM_RECV_REINS_EXCESS_CONTRACT" runat="server" Display="Dynamic" ErrorMessage="AST_COMM_RECV_REINS_EXCESS_CONTRACT can't be blank."
									ControlToValidate="cmbAST_COMM_RECV_REINS_EXCESS_CONTRACT"></asp:requiredfieldvalidator></TD>
						</tr>
						<!--2-->
						<TD class="midcolora" width="18%">
							<asp:Label id="capAST_MCCA_FEE_SUSPENSE_DIRECT_BILL" runat="server">MCCA Fees in Suspense - Direct Bill</asp:Label><SPAN class="mandatory">*</SPAN></TD>
						<TD class="midcolora" width="32%">
							<asp:DropDownList id="cmbAST_MCCA_FEE_SUSPENSE_DIRECT_BILL" onfocus="SelectComboIndex('cmbAST_MCCA_FEE_SUSPENSE_DIRECT_BILL')"
								runat="server">
								<asp:ListItem Value='0'></asp:ListItem>
							</asp:DropDownList><BR>
							<asp:requiredfieldvalidator id="rfvAST_MCCA_FEE_SUSPENSE_DIRECT_BILL" runat="server" Display="Dynamic" ErrorMessage="AST_MCCA_FEE_SUSPENSE_DIRECT_BILL can't be blank."
								ControlToValidate="cmbAST_MCCA_FEE_SUSPENSE_DIRECT_BILL"></asp:requiredfieldvalidator></TD>
						<!--3-->
						<TD class="midcolora" width="18%">
							<asp:Label id="capAST_MCCA_FEE_SUSPENSE_AGENCY_BILL" runat="server">MCCA Fees in Suspense - Agency Bill</asp:Label><span class="mandatory">*</span></TD>
						<TD class='midcolora' width='32%'>
							<asp:requiredfieldvalidator id="rfvAST_MCCA_FEE_SUSPENSE_AGENCY_BILL" runat="server" Display="Dynamic" ErrorMessage="AST_MCCA_FEE_SUSPENSE_AGENCY_BILL can't be blank."
								ControlToValidate="cmbAST_MCCA_FEE_SUSPENSE_AGENCY_BILL"></asp:requiredfieldvalidator>
							<asp:DropDownList id="cmbAST_MCCA_FEE_SUSPENSE_AGENCY_BILL" onfocus="SelectComboIndex('cmbAST_MCCA_FEE_SUSPENSE_AGENCY_BILL')"
								runat="server">
								<asp:ListItem Value='0'></asp:ListItem>
							</asp:DropDownList><BR>
						</TD>
					</tbody>
					<TR>
						<TD class="midcolora" colSpan="1">
							<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></TD>
						<TD class="midcolora" colSpan="2"></TD>
						<TD class="midcolorr" colSpan="1">
							<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></TD>
					</TR>
				</TABLE>
				<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
				<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
				<INPUT id="hidGL_ID" type="hidden" value="0" name="hidGL_ID" runat="server">
			</FORM>
		</div>
	</BODY>
</HTML>

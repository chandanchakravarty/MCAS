<%@ Page validateRequest=false language="c#" Codebehind="PiIncome.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Accounting.PiIncome" %>
<%@ Register  TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Posting interface Asset</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
function AddData()
{
	DisableValidators();
	document.getElementById('hidGL_ID').value	=	'New';
	document.getElementById('cmbINC_PRM_WRTN').focus();
	document.getElementById('cmbINC_PRM_WRTN').options.selectedIndex = -1;
	document.getElementById('cmbINC_PRM_WRTN_MCCA').options.selectedIndex = -1;
	document.getElementById('cmbINC_PRM_WRTN_OTH_STATE_ASSESS_FEE').options.selectedIndex = -1;
	document.getElementById('cmbINC_REINS_CEDED_EXCESS_CON').options.selectedIndex = -1;
	document.getElementById('cmbINC_REINS_CEDED_CAT_CON').options.selectedIndex = -1;
	document.getElementById('cmbINC_REINS_CEDED_UMBRELLA_CON').options.selectedIndex = -1;
	document.getElementById('cmbINC_REINS_CEDED_FACUL_CON').options.selectedIndex = -1;
	document.getElementById('cmbINC_REINS_CEDED_MCCA_CON').options.selectedIndex = -1;
	document.getElementById('cmbINC_CHG_UNEARN_PRM').options.selectedIndex = -1;
	document.getElementById('cmbINC_CHG_UNEARN_PRM_MCCA').options.selectedIndex = -1;
	document.getElementById('cmbINC_CHG_UNEARN_PRM_OTH_STATE_FEE').options.selectedIndex = -1;
	document.getElementById('cmbINC_CHG_CEDED_UNEARN_MCCA').options.selectedIndex = -1;
	document.getElementById('cmbINC_CHG_CEDED_UNEARN_UMBRELLA_REINS').options.selectedIndex = -1;
	document.getElementById('cmbINC_INSTALLMENT_FEES').options.selectedIndex = -1;
	document.getElementById('cmbINC_RE_INSTATEMENT_FEES').options.selectedIndex = -1;
	document.getElementById('cmbINC_NON_SUFFICIENT_FUND_FEES').options.selectedIndex = -1;
	document.getElementById('cmbINC_LATE_FEES').options.selectedIndex = -1;
	document.getElementById('cmbINC_SERVICE_CHARGE').options.selectedIndex = -1;
	document.getElementById('cmbINC_CONVENIENCE_FEE').options.selectedIndex = -1;

	document.getElementById('cmbINC_INTEREST_AMOUNT').options.selectedIndex = -1;
	document.getElementById('cmbINC_POLICY_TAXES').options.selectedIndex = -1;
	document.getElementById('cmbINC_POLICY_FEES').options.selectedIndex = -1;
	
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
    document.getElementById('tbHiddenFields').style.display = 'none';
    document.getElementById('rfvINC_REINS_CEDED_EXCESS_CON').enabled = false;
    document.getElementById('rfvINC_REINS_CEDED_MCCA_CON').enabled = false;
    document.getElementById('rfvINC_REINS_CEDED_CAT_CON').enabled = false;
    document.getElementById('rfvINC_REINS_CEDED_UMBRELLA_CON').enabled = false;
    document.getElementById('rfvINC_REINS_CEDED_FACUL_CON').enabled = false;
    document.getElementById('rfvINC_CHG_CEDED_UNEARN_UMBRELLA_REINS').enabled = false;
    document.getElementById('rfvINC_CHG_UNEARN_PRM').enabled = false;
    document.getElementById('rfvINC_CHG_UNEARN_PRM_MCCA').enabled = false;
    document.getElementById('rfvINC_CHG_CEDED_UNEARN_MCCA').enabled = false;
    document.getElementById('rfvINC_CHG_UNEARN_PRM_OTH_STATE_FEE').enabled = false;
    document.getElementById('rfvINC_PRM_WRTN_MCCA').enabled = false;
    document.getElementById('rfvINC_CONVENIENCE_FEE').enabled = false;
    document.getElementById('rfvINC_SERVICE_CHARGE').enabled = false;    
}
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();">
		<div style="OVERFLOW: auto">
			<FORM id="ACT_GENERAL_LEDGER" method="post" runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD>
							<TABLE width="100%" align="center" border="0">
								<tr>
									<TD class="pageHeader" colSpan="4"><asp:Label ID="capMANDATORY" runat="server"></asp:Label></TD><%--Please note that all fields marked with * are 
										mandatory--%>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colspan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="25%"><asp:label id="capINC_PRM_WRTN" runat="server">Premiums Written</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbINC_PRM_WRTN" onfocus="SelectComboIndex('cmbINC_PRM_WRTN')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_PRM_WRTN" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbINC_PRM_WRTN"></asp:requiredfieldvalidator></TD><%--INC_PRM_WRTN can't be blank.--%>
											
									<TD class="midcolora" width="25%"><asp:label id="capINC_PRM_WRTN_OTH_STATE_ASSESS_FEE" runat="server">Premiums Written - Other State Assesed Fees</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbINC_PRM_WRTN_OTH_STATE_ASSESS_FEE" onfocus="SelectComboIndex('cmbINC_PRM_WRTN_OTH_STATE_ASSESS_FEE')"
											runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_PRM_WRTN_OTH_STATE_ASSESS_FEE" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbINC_PRM_WRTN_OTH_STATE_ASSESS_FEE"></asp:requiredfieldvalidator></TD><%--INC_PRM_WRTN_OTH_STATE_ASSESS_FEE can't be blank.--%>
						
								</tr>
								
								<tr>
									<TD class="pageHeader" colSpan="4"><asp:Label ID="capFEEDETL" runat="server"></asp:Label></TD><%--Fee Details--%>
								</tr>
								<tr>
									<TD class="midcolora" width="25%">
									    <asp:label id="capINC_RE_INSTATEMENT_FEES" runat="server">INC_RE_INSTATEMENT_FEES</asp:label><span class="mandatory">*</span>
									</TD>
									<TD class="midcolora" width="25%">
									    <asp:dropdownlist id="cmbINC_RE_INSTATEMENT_FEES" onfocus="SelectComboIndex('cmbINC_RE_INSTATEMENT_FEES')" runat="server">
											    <asp:ListItem Value='0'></asp:ListItem>
										    </asp:dropdownlist><BR>
										    <asp:requiredfieldvalidator id="rfvINC_RE_INSTATEMENT_FEES" runat="server" Display="Dynamic" ErrorMessage=""
											    ControlToValidate="cmbINC_RE_INSTATEMENT_FEES"></asp:requiredfieldvalidator><%--INC_RE_INSTATEMENT_FEES can't be blank.--%>
									</TD>
									<TD class="midcolora" width="25%"><asp:label id="capINC_NON_SUFFICIENT_FUND_FEES" runat="server">INC_NON_SUFFICIENT_FUND_FEES</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbINC_NON_SUFFICIENT_FUND_FEES" onfocus="SelectComboIndex('cmbINC_NON_SUFFICIENT_FUND_FEES')"
											runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_NON_SUFFICIENT_FUND_FEES" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbINC_NON_SUFFICIENT_FUND_FEES"></asp:requiredfieldvalidator></TD><%--INC_NON_SUFFICIENT_FUND_FEES can't be blank.--%>
								</tr>
								<tr>
									<TD class="midcolora" width="25%"><asp:label id="capINC_LATE_FEES" runat="server">INC_LATE_FEES</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbINC_LATE_FEES" 
                                            onfocus="SelectComboIndex('cmbINC_LATE_FEES')" runat="server" Height="16px">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_LATE_FEES" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbINC_LATE_FEES"></asp:requiredfieldvalidator></TD><%--INC_LATE_FEES can't be blank.--%>
																				<td class="midcolora" width="25%"><asp:Label ID="capINC_INSTALLMENT_FEES" Runat="server">INC_INSTALLMENT_FEES</asp:Label><span class="mandatory">*</span></td>
									<td class="midcolora" width="25%"><asp:DropDownList ID="cmbINC_INSTALLMENT_FEES" OnFocus="SelectComboIndex('cmbINC_INSTALLMENT_FEES')"
											Runat="server">
											<asp:ListItem Value="0"></asp:ListItem>
										</asp:DropDownList><br>
										<asp:RequiredFieldValidator ID="rfvINC_INSTALLMENT_FEES" Display="Dynamic" Runat="server" ErrorMessage=""
											ControlToValidate="cmbINC_INSTALLMENT_FEES"></asp:RequiredFieldValidator><%--INC_INSTALLMENTS_FEES can't be blank.--%>
									</td>
								</tr>
								<!--Added by Pradeep Kushwaha on 30-August-2010-->
						        <tr>
									<TD class="midcolora" width="25%">
									<asp:label id="capINC_INTEREST_AMOUNT" runat="server"></asp:label><span class="mandatory">*</span>
									</TD>
									<TD class="midcolora" width="25%">
									<asp:dropdownlist id="cmbINC_INTEREST_AMOUNT" onfocus="SelectComboIndex('cmbINC_INTEREST_AMOUNT')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_INTEREST_AMOUNT" runat="server" Display="Dynamic" ControlToValidate="cmbINC_INTEREST_AMOUNT"></asp:requiredfieldvalidator>
									</TD>
									<td class="midcolora" width="25%">
									<asp:label id="capINC_POLICY_TAXES" runat="server"></asp:label><span class="mandatory">*</span>
									</td>
									<td class="midcolora" width="25%">
									<asp:dropdownlist id="cmbINC_POLICY_TAXES" onfocus="SelectComboIndex('cmbINC_POLICY_TAXES')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_POLICY_TAXES" runat="server" Display="Dynamic" ControlToValidate="cmbINC_POLICY_TAXES"></asp:requiredfieldvalidator>
									</td>
								</tr>
								 <tr>
									<TD class="midcolora" width="25%">
									<asp:label id="capINC_POLICY_FEES" runat="server"></asp:label><span class="mandatory">*</span>
									</TD>
									<TD class="midcolora" width="25%">
									<asp:dropdownlist id="cmbINC_POLICY_FEES" onfocus="SelectComboIndex('cmbINC_POLICY_FEES')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_POLICY_FEES" runat="server" Display="Dynamic" ControlToValidate="cmbINC_POLICY_FEES"></asp:requiredfieldvalidator>
									</TD>
									<td class="midcolora" width="25%">&nbsp;</td>
									<td class="midcolora" width="25%">&nbsp;</td>
								</tr>
								<!--Added till here -->
								
								<!--Hidden Fields-->
								<tbody id="tbHiddenFields">
							 
						
									<tr>
									<TD class="midcolora" width="25%"><asp:label id="capINC_REINS_CEDED_EXCESS_CON" runat="server">Reinsurance Ceded Excess Contract</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbINC_REINS_CEDED_EXCESS_CON" class="GrandFatheredRange" onfocus="SelectComboIndex('cmbINC_REINS_CEDED_EXCESS_CON')"
											runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_REINS_CEDED_EXCESS_CON" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbINC_REINS_CEDED_EXCESS_CON"></asp:requiredfieldvalidator></TD><%--INC_REINS_CEDED_EXCESS_CON can't be blank.--%>
									
									<TD class="midcolora" width="25%"><asp:label id="capINC_REINS_CEDED_MCCA_CON" runat="server">Reinsurance Ceded MCCA Contract</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="25%"><asp:dropdownlist class="GrandFatheredRange" id="cmbINC_REINS_CEDED_MCCA_CON" onfocus="SelectComboIndex('cmbINC_REINS_CEDED_MCCA_CON')"
											runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_REINS_CEDED_MCCA_CON" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbINC_REINS_CEDED_MCCA_CON"></asp:requiredfieldvalidator></TD><%--INC_REINS_CEDED_MCCA_CON can't be blank.--%>
							
								</tr>
								<tr>
									<TD class="midcolora" width="25%"><asp:label id="capINC_REINS_CEDED_CAT_CON" runat="server">Reinsurance Ceded CAT Contract</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbINC_REINS_CEDED_CAT_CON" class="GrandFatheredRange" onfocus="SelectComboIndex('cmbINC_REINS_CEDED_CAT_CON')"
											runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_REINS_CEDED_CAT_CON" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbINC_REINS_CEDED_CAT_CON"></asp:requiredfieldvalidator></TD><%--INC_REINS_CEDED_CAT_CON can't be blank.--%>
									<TD class="midcolora" width="25%"><asp:label id="capINC_REINS_CEDED_UMBRELLA_CON" runat="server">Reinsurance Ceded Umbrella Contract</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbINC_REINS_CEDED_UMBRELLA_CON" class="GrandFatheredRange" onfocus="SelectComboIndex('cmbINC_REINS_CEDED_UMBRELLA_CON')"
											runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_REINS_CEDED_UMBRELLA_CON" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbINC_REINS_CEDED_UMBRELLA_CON"></asp:requiredfieldvalidator></TD><%--INC_REINS_CEDED_UMBRELLA_CON can't be blank.--%>
								</tr>
								<tr>
									<TD class="midcolora" width="25%"><asp:label id="capINC_REINS_CEDED_FACUL_CON" runat="server">Reinsurance Ceded Facultative Contract</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbINC_REINS_CEDED_FACUL_CON" class="GrandFatheredRange" onfocus="SelectComboIndex('cmbINC_REINS_CEDED_FACUL_CON')"
											runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_REINS_CEDED_FACUL_CON" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbINC_REINS_CEDED_FACUL_CON"></asp:requiredfieldvalidator></TD><%--INC_REINS_CEDED_FACUL_CON can't be blank.--%>
									
									<TD class="midcolora" width="25%"><asp:label id="capINC_CHG_CEDED_UNEARN_UMBRELLA_REINS" runat="server">Change in Ceded Unearned Umbrella Reinsurance</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbINC_CHG_CEDED_UNEARN_UMBRELLA_REINS" class="GrandFatheredRange" onfocus="SelectComboIndex('cmbINC_CHG_CEDED_UNEARN_UMBRELLA_REINS')"
											runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_CHG_CEDED_UNEARN_UMBRELLA_REINS" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbINC_CHG_CEDED_UNEARN_UMBRELLA_REINS"></asp:requiredfieldvalidator></TD><%--INC_CHG_CEDED_UNEARN_UMBRELLA_REINS can't be blank.--%>
							
								</tr>
								<tr>
									<TD class="midcolora" width="25%"><asp:label id="capINC_CHG_UNEARN_PRM" runat="server">Change in Unearned Premium</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbINC_CHG_UNEARN_PRM" onfocus="SelectComboIndex('cmbINC_CHG_UNEARN_PRM')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_CHG_UNEARN_PRM" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbINC_CHG_UNEARN_PRM"></asp:requiredfieldvalidator></TD><%--INC_CHG_UNEARN_PRM can't be blank.--%>
									<TD class="midcolora" width="25%"><asp:label id="capINC_CHG_UNEARN_PRM_MCCA" runat="server">Change in Unearned Premium MCCA</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbINC_CHG_UNEARN_PRM_MCCA" onfocus="SelectComboIndex('cmbINC_CHG_UNEARN_PRM_MCCA')"
											runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_CHG_UNEARN_PRM_MCCA" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbINC_CHG_UNEARN_PRM_MCCA"></asp:requiredfieldvalidator></TD><%--INC_CHG_UNEARN_PRM_MCCA can't be blank.--%>
								</tr>
								<tr>
									<TD class="midcolora" width="25%"><asp:label id="capINC_CHG_CEDED_UNEARN_MCCA" runat="server">Change in Ceded Unearned MCCA</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="25%" colspan="3"><asp:dropdownlist id="cmbINC_CHG_CEDED_UNEARN_MCCA" onfocus="SelectComboIndex('cmbINC_CHG_CEDED_UNEARN_MCCA')"
											runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_CHG_CEDED_UNEARN_MCCA" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbINC_CHG_CEDED_UNEARN_MCCA"></asp:requiredfieldvalidator></TD><%--INC_CHG_CEDED_UNEARN_MCCA can't be blank.--%>
								</tr>
								<tr>
								<TD class="midcolora" width="25%"><asp:label id="capINC_CHG_UNEARN_PRM_OTH_STATE_FEE" runat="server">Change in Unearned Premium Other State Assesed Fees</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbINC_CHG_UNEARN_PRM_OTH_STATE_FEE" onfocus="SelectComboIndex('cmbINC_CHG_UNEARN_PRM_OTH_STATE_FEE')"
											runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_CHG_UNEARN_PRM_OTH_STATE_FEE" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbINC_CHG_UNEARN_PRM_OTH_STATE_FEE"></asp:requiredfieldvalidator></TD><%--INC_CHG_UNEARN_PRM_OTH_STATE_FEE can't be blank.--%>
							
							
								<TD class="midcolora" width="25%"><asp:label id="capINC_PRM_WRTN_MCCA" runat="server">Premiums Written MCCA</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbINC_PRM_WRTN_MCCA" onfocus="SelectComboIndex('cmbINC_PRM_WRTN_MCCA')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_PRM_WRTN_MCCA" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbINC_PRM_WRTN_MCCA"></asp:requiredfieldvalidator></TD><%--INC_PRM_WRTN_MCCA can't be blank.--%>
							
								<TD class="midcolora" width="25%"><asp:label id="capINC_CONVENIENCE_FEE" runat="server">INC_CONVENIENCE_FEE</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbINC_CONVENIENCE_FEE" onfocus="SelectComboIndex('cmbINC_CONVENIENCE_FEE')"
											runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvINC_CONVENIENCE_FEE" runat="server" Display="Dynamic" ErrorMessage="INC_CONVENIENCE_FEE can't be blank."
											ControlToValidate="cmbINC_CONVENIENCE_FEE"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="25%"><asp:label id="capINC_SERVICE_CHARGE" runat="server">INC_SERVICE_CHARGE</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbINC_SERVICE_CHARGE" class="GrandFatheredRange" onfocus="SelectComboIndex('cmbINC_SERVICE_CHARGE')"
											runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:RequiredFieldValidator ID="rfvINC_SERVICE_CHARGE" Runat="server" Display="Dynamic" ErrorMessage="INC_SERVICE_CHARGE can't be blank."
											ControlToValidate="cmbINC_SERVICE_CHARGE"></asp:RequiredFieldValidator></TD>
							</tr>
								</tbody>
								<tr>
									<td class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
									<td class="midcolora" colSpan="2"></td>
									<td class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
								<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
								<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
								<INPUT id="hidGL_ID" type="hidden" value="0" name="hidGL_ID" runat="server">
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</FORM>
		</div>
	</BODY>
</HTML>

<%@ Register  TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page validateRequest=false language="c#" Codebehind="PiLiabilities.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Accounting.PiLiabilities" %>
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
	document.getElementById('cmbLIB_COMM_PAYB_AGENCY_BILL').focus();
	document.getElementById('cmbLIB_COMM_PAYB_AGENCY_BILL').options.selectedIndex = -1;
	document.getElementById('cmbLIB_COMM_PAYB_DIRECT_BILL').options.selectedIndex = -1;
	document.getElementById('cmbLIB_REINS_PAYB_EXCESS_CONTRACT').options.selectedIndex = -1;
	document.getElementById('cmbLIB_REINS_PAYB_CAT_CONTRACT').options.selectedIndex = -1;
	document.getElementById('cmbLIB_REINS_PAYB_MCCA').options.selectedIndex = -1;
	document.getElementById('cmbLIB_REINS_PAYB_UMBRELLA').options.selectedIndex = -1;
	document.getElementById('cmbLIB_REINS_PAYB_FACULTATIVE').options.selectedIndex = -1;
	document.getElementById('cmbLIB_OUT_DRAFTS').options.selectedIndex = -1;
	document.getElementById('cmbLIB_ADVCE_PRM_DEPOSIT').options.selectedIndex = -1;
	document.getElementById('cmbLIB_ADVCE_PRM_DEPOSIT_2M').options.selectedIndex = -1;
	document.getElementById('cmbLIB_UNEARN_PRM').value  = '';
	document.getElementById('cmbLIB_UNEARN_PRM_MCCA').options.selectedIndex = -1;
	document.getElementById('cmbLIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS').options.selectedIndex = -1;
	document.getElementById('cmbLIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS').options.selectedIndex = -1;
	document.getElementById('cmbLIB_UNEARN_PRM_OTH_STATE_ASSES_FEE').options.selectedIndex = -1;
	//document.getElementById('cmbLIB_TAX_PAYB').options.selectedIndex = -1;
	document.getElementById('cmbLIB_VENDOR_PAYB').options.selectedIndex = -1;
	document.getElementById('cmbLIB_COLL_ON_NONISSUED_POLICY').options.selectedIndex = -1;
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
	ChangeColor();
}

return false;
}

function HiddenFields()
{
	document.getElementById('tbHiddenFields').style.display='none';
	document.getElementById('rfvLIB_REINS_PAYB_EXCESS_CONTRACT').enabled = false;
	document.getElementById('rfvLIB_REINS_PAYB_CAT_CONTRACT').enabled = false;
	document.getElementById('rfvLIB_REINS_PAYB_MCCA').enabled = false;
	document.getElementById('rfvLIB_REINS_PAYB_UMBRELLA').enabled = false;
	document.getElementById('rfvLIB_REINS_PAYB_FACULTATIVE').enabled = false;
	document.getElementById('rfvLIB_OUT_DRAFTS').enabled = false;
	document.getElementById('rfvLIB_ADVCE_PRM_DEPOSIT').enabled = false;
	document.getElementById('rfvLIB_ADVCE_PRM_DEPOSIT_2M').enabled = false;
	document.getElementById('rfvLIB_UNEARN_PRM').enabled = false;
	document.getElementById('rfvLIB_UNEARN_PRM_MCCA').enabled = false;
	document.getElementById('rfvLIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS').enabled = false;
	document.getElementById('rfvLIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS').enabled = false;
	document.getElementById('rfvLIB_UNEARN_PRM_OTH_STATE_ASSES_FEE').enabled = false;
	document.getElementById('rfvLIB_COLL_ON_NONISSUED_POLICY').enabled = false;
}

		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();'>
		<FORM id='ACT_GENERAL_LEDGER' method='post' runat='server'>
			<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
				<TR>
					<TD>
						<TABLE width='100%' border='0' align='center'>
							<tr>
								<TD class="pageHeader" colSpan="4"><asp:Label ID="capMANDATORY" runat="server"></asp:Label></TD><%--Please note that all fields marked with * are mandatory--%>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capLIB_COMM_PAYB_AGENCY_BILL" runat="server">Commission Payable Agency Bill</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList id='cmbLIB_COMM_PAYB_AGENCY_BILL' OnFocus="SelectComboIndex('cmbLIB_COMM_PAYB_AGENCY_BILL')"
										runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvLIB_COMM_PAYB_AGENCY_BILL" runat="server" ControlToValidate="cmbLIB_COMM_PAYB_AGENCY_BILL"
										ErrorMessage="LIB_COMM_PAYB_AGENCY_BILL can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capLIB_COMM_PAYB_DIRECT_BILL" runat="server">Commission Payable Direct Bill</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList  id='cmbLIB_COMM_PAYB_DIRECT_BILL' OnFocus="SelectComboIndex('cmbLIB_COMM_PAYB_DIRECT_BILL')"
										runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvLIB_COMM_PAYB_DIRECT_BILL" runat="server" ControlToValidate="cmbLIB_COMM_PAYB_DIRECT_BILL"
										ErrorMessage="LIB_COMM_PAYB_DIRECT_BILL can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
							</tr>
							
							<tr>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capLIB_VENDOR_PAYB" runat="server">Vendor Payable</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList id='cmbLIB_VENDOR_PAYB' OnFocus="SelectComboIndex('cmbLIB_VENDOR_PAYB')" runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvLIB_VENDOR_PAYB" runat="server" ControlToValidate="cmbLIB_VENDOR_PAYB" ErrorMessage="LIB_VENDOR_PAYB can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='25%'>
									
								<TD class='midcolora' width='25%'>
									
								</TD>
							</tr>
							<tbody id="tbHiddenFields">
								<tr>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capLIB_REINS_PAYB_EXCESS_CONTRACT" runat="server">Reinsurance Payable Excess Contract</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList class="GrandFatheredRange" id='cmbLIB_REINS_PAYB_EXCESS_CONTRACT' OnFocus="SelectComboIndex('cmbLIB_REINS_PAYB_EXCESS_CONTRACT')"
										runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvLIB_REINS_PAYB_EXCESS_CONTRACT" runat="server" ControlToValidate="cmbLIB_REINS_PAYB_EXCESS_CONTRACT"
										ErrorMessage="LIB_REINS_PAYB_EXCESS_CONTRACT can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capLIB_REINS_PAYB_CAT_CONTRACT" runat="server">Reinsurance Payable CAT Contract</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList class="GrandFatheredRange" id='cmbLIB_REINS_PAYB_CAT_CONTRACT' OnFocus="SelectComboIndex('cmbLIB_REINS_PAYB_CAT_CONTRACT')"
										runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvLIB_REINS_PAYB_CAT_CONTRACT" runat="server" ControlToValidate="cmbLIB_REINS_PAYB_CAT_CONTRACT"
										ErrorMessage="LIB_REINS_PAYB_CAT_CONTRACT can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capLIB_REINS_PAYB_MCCA" runat="server">Reinsurance Payable MCCA</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList class="GrandFatheredRange" id='cmbLIB_REINS_PAYB_MCCA' OnFocus="SelectComboIndex('cmbLIB_REINS_PAYB_MCCA')"
										runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvLIB_REINS_PAYB_MCCA" runat="server" ControlToValidate="cmbLIB_REINS_PAYB_MCCA"
										ErrorMessage="LIB_REINS_PAYB_MCCA can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capLIB_REINS_PAYB_UMBRELLA" runat="server">Reinsurance Payable Umbrella</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList class="GrandFatheredRange" id='cmbLIB_REINS_PAYB_UMBRELLA' OnFocus="SelectComboIndex('cmbLIB_REINS_PAYB_UMBRELLA')"
										runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvLIB_REINS_PAYB_UMBRELLA" runat="server" ControlToValidate="cmbLIB_REINS_PAYB_UMBRELLA"
										ErrorMessage="LIB_REINS_PAYB_UMBRELLA can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capLIB_REINS_PAYB_FACULTATIVE" runat="server">Reinsurance Payable Facultative</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList class="GrandFatheredRange" id='cmbLIB_REINS_PAYB_FACULTATIVE' OnFocus="SelectComboIndex('cmbLIB_REINS_PAYB_FACULTATIVE')"
										runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvLIB_REINS_PAYB_FACULTATIVE" runat="server" ControlToValidate="cmbLIB_REINS_PAYB_FACULTATIVE"
										ErrorMessage="LIB_REINS_PAYB_FACULTATIVE can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capLIB_OUT_DRAFTS" runat="server">Outstanding Drafts</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList class="GrandFatheredRange" id='cmbLIB_OUT_DRAFTS' OnFocus="SelectComboIndex('cmbLIB_OUT_DRAFTS')" runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvLIB_OUT_DRAFTS" runat="server" ControlToValidate="cmbLIB_OUT_DRAFTS" ErrorMessage="LIB_OUT_DRAFTS can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capLIB_ADVCE_PRM_DEPOSIT" runat="server">Advance Premium Deposits</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList class="GrandFatheredRange" id='cmbLIB_ADVCE_PRM_DEPOSIT' OnFocus="SelectComboIndex('cmbLIB_ADVCE_PRM_DEPOSIT')"
										runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvLIB_ADVCE_PRM_DEPOSIT" runat="server" ControlToValidate="cmbLIB_ADVCE_PRM_DEPOSIT"
										ErrorMessage="LIB_ADVCE_PRM_DEPOSIT can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capLIB_ADVCE_PRM_DEPOSIT_2M" runat="server">Advance Premium Deposits 2 Months</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList class="GrandFatheredRange" id='cmbLIB_ADVCE_PRM_DEPOSIT_2M' OnFocus="SelectComboIndex('cmbLIB_ADVCE_PRM_DEPOSIT_2M')"
										runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvLIB_ADVCE_PRM_DEPOSIT_2M" runat="server" ControlToValidate="cmbLIB_ADVCE_PRM_DEPOSIT_2M"
										ErrorMessage="LIB_ADVCE_PRM_DEPOSIT_2M can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capLIB_UNEARN_PRM" runat="server">Unearned Premium</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList class="GrandFatheredRange" id="cmbLIB_UNEARN_PRM" OnFocus="SelectComboIndex('cmbLIB_UNEARN_PRM')" runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList>
									<asp:requiredfieldvalidator id="rfvLIB_UNEARN_PRM" runat="server" ControlToValidate="cmbLIB_UNEARN_PRM" ErrorMessage="LIB_UNEARN_PRM can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capLIB_UNEARN_PRM_MCCA" runat="server">Unearned Premium MCCA</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList class="GrandFatheredRange" id='cmbLIB_UNEARN_PRM_MCCA' OnFocus="SelectComboIndex('cmbLIB_UNEARN_PRM_MCCA')"
										runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvLIB_UNEARN_PRM_MCCA" runat="server" ControlToValidate="cmbLIB_UNEARN_PRM_MCCA"
										ErrorMessage="LIB_UNEARN_PRM_MCCA can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capLIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS" runat="server">Unearned Premium Ceded - Unearned MCCA Reinsurance</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList class="GrandFatheredRange" id='cmbLIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS' OnFocus="SelectComboIndex('cmbLIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS')"
										runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvLIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS" runat="server" ControlToValidate="cmbLIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS"
										ErrorMessage="LIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capLIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS" runat="server">Unearned Premium Ceded - Unearned Umbrella Reinsurance</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList class="GrandFatheredRange" id='cmbLIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS' OnFocus="SelectComboIndex('cmbLIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS')"
										runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvLIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS" runat="server" ControlToValidate="cmbLIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS"
										ErrorMessage="LIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capLIB_UNEARN_PRM_OTH_STATE_ASSES_FEE" runat="server">Unearned Premium Other State Earned Fees</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList class="GrandFatheredRange" id='cmbLIB_UNEARN_PRM_OTH_STATE_ASSES_FEE' OnFocus="SelectComboIndex('cmbLIB_UNEARN_PRM_OTH_STATE_ASSES_FEE')"
										runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvLIB_UNEARN_PRM_OTH_STATE_ASSES_FEE" runat="server" ControlToValidate="cmbLIB_UNEARN_PRM_OTH_STATE_ASSES_FEE"
										ErrorMessage="LIB_UNEARN_PRM_OTH_STATE_ASSES_FEE can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capLIB_COLL_ON_NONISSUED_POLICY" runat="server">Collection on Non Issued Policy</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList class="GrandFatheredRange" id='cmbLIB_COLL_ON_NONISSUED_POLICY' OnFocus="SelectComboIndex('cmbLIB_COLL_ON_NONISSUED_POLICY')"
										runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvLIB_COLL_ON_NONISSUED_POLICY" runat="server" ControlToValidate="cmbLIB_COLL_ON_NONISSUED_POLICY"
										ErrorMessage="LIB_COLL_ON_NONISSUED_POLICY can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
							</tr>
							</tbody>
							<tr>
								<td class='midcolora' colspan='1'>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
								</td>
								<td class='midcolora' colspan='2'></td>
								<td class='midcolorr' colspan='1'>
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
								</td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
							<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> 
							<INPUT id="hidGL_ID" type="hidden" name="hidGL_ID" runat="server">
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</BODY>
</HTML>

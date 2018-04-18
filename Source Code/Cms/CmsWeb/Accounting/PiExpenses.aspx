<%@ Register  TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page validateRequest=false language="c#" Codebehind="PiExpenses.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Accounting.PiExpenses" %>
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
	document.getElementById('cmbEXP_COMM_INCURRED').focus();
	document.getElementById('cmbEXP_COMM_INCURRED').options.selectedIndex = -1;
	document.getElementById('cmbEXP_REINS_COMM_EXCESS_CON').options.selectedIndex = -1;
	document.getElementById('cmbEXP_REINS_COMM_UMBRELLA_CON').options.selectedIndex = -1;
	document.getElementById('cmbEXP_ASSIGNED_CLAIMS').options.selectedIndex = -1;
	document.getElementById('cmbEXP_REINS_PAID_LOSSES').options.selectedIndex = -1;
	document.getElementById('cmbEXP_REINS_PAID_LOSSES_CAT').options.selectedIndex = -1;
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
    document.getElementById('rfvEXP_REINS_COMM_EXCESS_CON').enabled = false;
    document.getElementById('rfvEXP_REINS_COMM_UMBRELLA_CON').enabled = false;
    document.getElementById('rfvEXP_ASSIGNED_CLAIMS').enabled = false;
    document.getElementById('rfvEXP_REINS_PAID_LOSSES').enabled = false;
    document.getElementById('rfvEXP_REINS_PAID_LOSSES_CAT').enabled = false;
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
									<asp:Label id="capEXP_COMM_INCURRED" runat="server">Commission Incurred</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList id='cmbEXP_COMM_INCURRED' OnFocus="SelectComboIndex('cmbEXP_COMM_INCURRED')" runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvEXP_COMM_INCURRED" runat="server" ControlToValidate="cmbEXP_COMM_INCURRED"
										ErrorMessage="EXP_COMM_INCURRED can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capEXP_SMALL_BALANCE_WRITE_OFF" runat="server">SMALL BALANCE WRITE OFF</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList id="cmbEXP_SMALL_BALANCE_WRITE_OFF" OnFocus="SelectComboIndex('cmbEXP_SMALL_BALANCE_WRITE_OFF')"
										runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvEXP_SMALL_BALANCE_WRITE_OFF" runat="server" ControlToValidate="cmbEXP_SMALL_BALANCE_WRITE_OFF"
										ErrorMessage="EXP_SMALL_BALANCE_WRITE_OFF can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
							</tr>
							<tbody id="tbHiddenFields">
								<tr>
									<TD class='midcolora' width='25%'>
										<asp:Label id="capEXP_REINS_COMM_EXCESS_CON" runat="server">Reinsurance Commissions Excess Contract</asp:Label><span class="mandatory">*</span></TD>
									<TD class='midcolora' width='25%'>
										<asp:DropDownList class="GrandFatheredRange" id='cmbEXP_REINS_COMM_EXCESS_CON' OnFocus="SelectComboIndex('cmbEXP_REINS_COMM_EXCESS_CON')"
											runat='server'>
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:DropDownList><BR>
										<asp:requiredfieldvalidator id="rfvEXP_REINS_COMM_EXCESS_CON" runat="server" ControlToValidate="cmbEXP_REINS_COMM_EXCESS_CON"
											ErrorMessage="EXP_REINS_COMM_EXCESS_CON can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
									</TD>
								</tr>
								<tr>
									<TD class='midcolora' width='25%'>
										<asp:Label id="capEXP_REINS_COMM_UMBRELLA_CON" runat="server">Reinsurance Commissions Umbrella Contract</asp:Label><span class="mandatory">*</span></TD>
									<TD class='midcolora' width='25%'>
										<asp:DropDownList class="GrandFatheredRange" id='cmbEXP_REINS_COMM_UMBRELLA_CON' OnFocus="SelectComboIndex('cmbEXP_REINS_COMM_UMBRELLA_CON')"
											runat='server'>
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:DropDownList><BR>
										<asp:requiredfieldvalidator id="rfvEXP_REINS_COMM_UMBRELLA_CON" runat="server" ControlToValidate="cmbEXP_REINS_COMM_UMBRELLA_CON"
											ErrorMessage="EXP_REINS_COMM_UMBRELLA_CON can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
									</TD>
									<TD class='midcolora' width='25%'>
										<asp:Label id="capEXP_ASSIGNED_CLAIMS" runat="server">Assigned Claims</asp:Label><span class="mandatory">*</span></TD>
									<TD class='midcolora' width='25%'>
										<asp:DropDownList class="GrandFatheredRange" id='cmbEXP_ASSIGNED_CLAIMS' OnFocus="SelectComboIndex('cmbEXP_ASSIGNED_CLAIMS')"
											runat='server'>
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:DropDownList><BR>
										<asp:requiredfieldvalidator id="rfvEXP_ASSIGNED_CLAIMS" runat="server" ControlToValidate="cmbEXP_ASSIGNED_CLAIMS"
											ErrorMessage="EXP_ASSIGNED_CLAIMS can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
									</TD>
								</tr>
								<tr>
									<TD class='midcolora' width='25%'>
										<asp:Label id="capEXP_REINS_PAID_LOSSES" runat="server">Reinsurance Paid Losses</asp:Label><span class="mandatory">*</span></TD>
									<TD class='midcolora' width='25%'>
										<asp:DropDownList class="GrandFatheredRange" id='cmbEXP_REINS_PAID_LOSSES' OnFocus="SelectComboIndex('cmbEXP_REINS_PAID_LOSSES')"
											runat='server'>
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:DropDownList><BR>
										<asp:requiredfieldvalidator id="rfvEXP_REINS_PAID_LOSSES" runat="server" ControlToValidate="cmbEXP_REINS_PAID_LOSSES"
											ErrorMessage="EXP_REINS_PAID_LOSSES can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
									</TD>
									<TD class='midcolora' width='25%'>
										<asp:Label id="capEXP_REINS_PAID_LOSSES_CAT" runat="server">Reinsurance Paid Losses CAT</asp:Label><span class="mandatory">*</span></TD>
									<TD class='midcolora' width='25%'>
										<asp:DropDownList class="GrandFatheredRange" id='cmbEXP_REINS_PAID_LOSSES_CAT' OnFocus="SelectComboIndex('cmbEXP_REINS_PAID_LOSSES_CAT')"
											runat='server'>
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:DropDownList><BR>
										<asp:requiredfieldvalidator id="rfvEXP_REINS_PAID_LOSSES_CAT" runat="server" ControlToValidate="cmbEXP_REINS_PAID_LOSSES_CAT"
											ErrorMessage="EXP_REINS_PAID_LOSSES_CAT can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
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
							<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
							<INPUT id="hidGL_ID" type="hidden" value="0" name="hidGL_ID" runat="server">
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</BODY>
</HTML>

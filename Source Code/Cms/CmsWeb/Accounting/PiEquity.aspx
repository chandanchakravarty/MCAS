<%@ Register  TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page validateRequest=false language="c#" Codebehind="PiEquity.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Accounting.PiEquity" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Posting interface Asset</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js">7</script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language='javascript'>
function AddData()
{
DisableValidators();
document.getElementById('hidGL_ID').value	=	'New';
document.getElementById('cmbEQU_TRANSFER').focus();
document.getElementById('cmbEQU_TRANSFER').options.selectedIndex = -1;
document.getElementById('cmbEQU_UNASSIGNED_SURPLUS').options.selectedIndex = 0;
ChangeColor();
}

function populateXML()
{
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
									<asp:Label id="capEQU_TRANSFER" runat="server">Transfer Account</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList class="GrandFatheredRange" id='cmbEQU_TRANSFER' OnFocus="SelectComboIndex('cmbEQU_TRANSFER')"
										runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvEQU_TRANSFER" runat="server" ControlToValidate="cmbEQU_TRANSFER" ErrorMessage="EQU_TRANSFER can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capEQU_UNASSIGNED_SURPLUS" runat="server">Unassigned Surplus</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='25%'>
									<asp:DropDownList id="cmbEQU_UNASSIGNED_SURPLUS" class="GrandFatheredRange" OnFocus="SelectComboIndex('cmbEQU_UNASSIGNED_SURPLUS')" runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvEQU_UNASSIGNED_SURPLUS" runat="server" ControlToValidate="cmbEQU_UNASSIGNED_SURPLUS"
										ErrorMessage="EQU_UNASSIGNED_SURPLUS can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
							</tr>
							<tr>
								<td class='midcolora' colspan='1'>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
								</td>
								<td class='midcolora' colspan='1'></td>
								<td class='midcolorr' colspan='2'>
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

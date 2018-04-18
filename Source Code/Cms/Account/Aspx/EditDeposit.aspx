<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" validaterequest=false Codebehind="EditDeposit.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.EditDeposit" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>BRICS - Edit Current Deposit Line Items</title>
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
document.getElementById('hidCD_LINE_ITEM_ID').value	=	'New';
document.getElementById('txtLINE_ITEM_INTERNAL_NUMBER').focus();
document.getElementById('txtLINE_ITEM_INTERNAL_NUMBER').value  = '';
document.getElementById('cmbAccountingEntity').options.selectedIndex = -1;
document.getElementById('txtACCOUNT_ID').value  = '';
document.getElementById('cmbDEPOSIT_TYPE').options.selectedIndex = -1;
document.getElementById('txtBANK_NAME').value  = '';
document.getElementById('txtCHECK_NUM').value  = '';
document.getElementById('txtRECEIPT_AMOUNT').value  = '';
document.getElementById('cmbPAYOR_TYPE').options.selectedIndex = -1;
document.getElementById('txtRECEIPT_FROM_ID').value  = '';
document.getElementById('txtLINE_ITEM_DESCRIPTION').value  = '';
document.getElementById('cmbPOLICY_ID').options.selectedIndex = -1;
document.getElementById('txtREF_CUSTOMER_ID').value  = '';
document.getElementById('txtAppliedStatus').value  = '';
ChangeColor();
}
function populateXML()
{
DisableValidators();
var tempXML = document.getElementById('hidOldData').value;
//alert(tempXML);
//AddData();
if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
{
	if(tempXML!="")
	{
		populateFormData(tempXML,ACT_CURRENT_DEPOSIT_LINE_ITEMS);
		var payerType = document.getElementById('cmbPAYOR_TYPE').options[document.getElementById('cmbPAYOR_TYPE').selectedIndex].value;
		CheckIfLookupIsRequired(payerType);
	}
	else
	{
		AddData();
	}
	
}

return false;
}
function OpenCustNewLookup()
{
	var url='<%=URL%>';
	//signature:Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction
	OpenLookupWithFunction( url,'CUSTOMER_ID','Name','hidREF_CUSTOMER_ID_HID','txtREF_CUSTOMER_ID','CustLookupForm','Customer Names','','SetLookupValues()');
}
function OpenNewLookup()
{
	var url='<%=URL%>';
		var idField,valueField,lookUpTagName,lookUpTitle;
	var payerType = document.getElementById('cmbPAYOR_TYPE').options[document.getElementById('cmbPAYOR_TYPE').selectedIndex].value;
	switch(payerType)
	{
	case 'AGN':
		idField			=	'AGENCY_ID';
		valueField		=	'Name';
		lookUpTagName	=	'Agency';
		lookUpTitle		=	"Agency Names";
		break;
	case 'CUS':
		idField			=	'CUSTOMER_ID';
		valueField		=	'Name';
		lookUpTagName	=	'CustLookupForm';
		lookUpTitle		=	'Customer Names';
		break;
	case 'TAX':
		idField			=	'TAX_ID';
		valueField		=	'Name';
		lookUpTagName	=	'Tax';
		lookUpTitle		=	'Tax Names';
		break;
	case 'VEN':
		idField			=	'VENDOR_ID';
		valueField		=	'Name';
		lookUpTagName	=	'Vendor';
		lookUpTitle		=	'Vendor Names';
		break;
	case 'MOR':
		idField			=	'HOLDER_ID';
		valueField		=	'Name';
		lookUpTagName	=	'Holder';
		lookUpTitle		=	'Mortgage Names';
		break;
	}
	//signature:Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction
	OpenLookupWithFunction( url,idField,valueField,'hidRECEIPT_FROM_ID_HID','txtRECEIPT_FROM_ID',lookUpTagName,lookUpTitle,'','SetLookupValues()');
}
function SetLookupValues()
{
}
function SetPayerType()
{
	document.getElementById('txtRECEIPT_FROM_ID').value = "";
	document.getElementById('hidRECEIPT_FROM_ID_HID').value = "";
	var payerType = document.getElementById('cmbPAYOR_TYPE').options[document.getElementById('cmbPAYOR_TYPE').selectedIndex].value;
	CheckIfLookupIsRequired(payerType);	
}
function CheckIfLookupIsRequired(payerType)
{
	if(payerType=="OTH")
	{
		document.getElementById('imgSelect').style.display = "None";	
		document.getElementById('txtRECEIPT_FROM_ID').disabled = false;
	}
	else
	{
		document.getElementById('imgSelect').style.display = "Inline";	
		document.getElementById('txtRECEIPT_FROM_ID').disabled = true;
	}
}
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();">
		<FORM id="ACT_CURRENT_DEPOSIT_LINE_ITEMS" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
									mandatory</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capAccountingEntity" runat="server">Accounting Entity</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbAccountingEntity" onfocus="SelectComboIndex('cmbAccountingEntity')" runat="server">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvAccountingEntity" runat="server" Display="Dynamic" ErrorMessage="AccountingEntity can't be blank."
										ControlToValidate="cmbAccountingEntity"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capLINE_ITEM_INTERNAL_NUMBER" runat="server">Internal Number</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtLINE_ITEM_INTERNAL_NUMBER" runat="server" maxlength="4" size="30" class="midcolora"
										style="BORDER-RIGHT:medium none;BORDER-TOP:medium none;FONT-WEIGHT:bold;BORDER-LEFT:medium none;BORDER-BOTTOM:medium none"
										ReadOnly="True"></asp:textbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capDEPOSIT_TYPE" runat="server">Deposit Type</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDEPOSIT_TYPE" onfocus="SelectComboIndex('cmbDEPOSIT_TYPE')" runat="server">
										<asp:ListItem value=''></asp:ListItem>
										<asp:ListItem Value="CH">Cash</asp:ListItem>
										<asp:ListItem Value="CK">Check</asp:ListItem>
										<asp:ListItem Value="CC">Credit Card Payment</asp:ListItem>
										<asp:ListItem Value="MO">Money Order</asp:ListItem>
										<asp:ListItem Value="WT">Wire Transfer</asp:ListItem>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvDEPOSIT_TYPE" runat="server" Display="Dynamic" ErrorMessage="DEPOSIT_TYPE can't be blank."
										ControlToValidate="cmbDEPOSIT_TYPE"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capBANK_NAME" runat="server">Bank Name</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtBANK_NAME" runat="server" maxlength="510" size="30"></asp:textbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCHECK_NUM" runat="server">Check Number</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCHECK_NUM" runat="server" maxlength="50" size="30"></asp:textbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capRECEIPT_AMOUNT" runat="server">Receipt Amount</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtRECEIPT_AMOUNT" runat="server" maxlength="9" size="30"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvRECEIPT_AMOUNT" runat="server" Display="Dynamic" ErrorMessage="RECEIPT_AMOUNT can't be blank."
										ControlToValidate="txtRECEIPT_AMOUNT"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revRECEIPT_AMOUNT" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtRECEIPT_AMOUNT"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capPAYOR_TYPE" runat="server">Payer Type</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist onchange="SetPayerType();" id="cmbPAYOR_TYPE" onfocus="SelectComboIndex('cmbPAYOR_TYPE')"
										runat="server">
										<asp:ListItem value=''></asp:ListItem>
										<asp:ListItem Value='AGN'>Agency</asp:ListItem>
										<asp:ListItem Value='CUS'>Customer</asp:ListItem>
										<asp:ListItem Value='TAX'>Tax Entity</asp:ListItem>
										<asp:ListItem Value='VEN'>Vendor</asp:ListItem>
										<asp:ListItem Value='OTH'>Other</asp:ListItem>
										<asp:ListItem Value='MOR'>Mortgage</asp:ListItem>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvPAYOR_TYPE" runat="server" Display="Dynamic" ErrorMessage="PAYOR_TYPE can't be blank."
										ControlToValidate="cmbPAYOR_TYPE"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capRECEIPT_FROM_ID" runat="server">Receipt From</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtRECEIPT_FROM_ID" runat="server" maxlength="255" size="30"></asp:textbox>
									<!--Lookup code--><IMG id="imgSelect" style="CURSOR: hand" onclick="OpenNewLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
										runat="server">
									<BR>
									<asp:requiredfieldvalidator id="rfvRECEIPT_FROM_ID" runat="server" Display="Dynamic" ErrorMessage="RECEIPT_FROM_ID can't be blank."
										ControlToValidate="txtRECEIPT_FROM_ID"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capPOLICY_ID" runat="server">Policy Number(LOB) Effective-Expiration Date</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPOLICY_ID" onfocus="SelectComboIndex('cmbPOLICY_ID')" runat="server">
										<asp:ListItem Value=''></asp:ListItem>
										<asp:ListItem Value="1">Policy 1</asp:ListItem>
										<asp:ListItem Value="2">Policy 2</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"><asp:label id="capACCOUNT_ID" runat="server">General Ledger Account</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtACCOUNT_ID" runat="server" maxlength="4" size="30" class="midcolora" style="BORDER-RIGHT:medium none;BORDER-TOP:medium none;FONT-WEIGHT:bold;BORDER-LEFT:medium none;BORDER-BOTTOM:medium none"
										ReadOnly="True"></asp:textbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capLINE_ITEM_DESCRIPTION" runat="server">Description</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtLINE_ITEM_DESCRIPTION" runat="server" maxlength="510" size="30"></asp:textbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capAppliedStatus" runat="server">Applied Status</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtAppliedStatus" class="midcolora" style="BORDER-RIGHT:medium none;BORDER-TOP:medium none;FONT-WEIGHT:bold;BORDER-LEFT:medium none;BORDER-BOTTOM:medium none"
										ReadOnly="True" runat="server" maxlength="8" size="30"></asp:textbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capREF_CUSTOMER_ID" runat="server">Reference Customer Name</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox ReadOnly="True" id="txtREF_CUSTOMER_ID" runat="server" maxlength="4" size="30"></asp:textbox>
									<!--Lookup code--><IMG id="Img1" style="CURSOR: hand" onclick="OpenCustNewLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
										runat="server">
								</TD>
								<TD class="midcolora" colSpan="2"></TD>
							</tr>
							<tr>
								<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnApplyOpenItems" runat="server" Text="Apply Open Items"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnDeleteLineItem" runat="server" Text="Delete Line Item"></cmsb:cmsbutton></td>
								<td class="midcolorr" colspan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
							<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCD_LINE_ITEM_ID" type="hidden" name="hidCD_LINE_ITEM_ID" runat="server">
							<INPUT id="hidRECEIPT_FROM_ID_HID" type="hidden" name="hidRECEIPT_FROM_ID_HID" runat="server">
							<INPUT id="hidREF_CUSTOMER_ID_HID" type="hidden" name="hidREF_CUSTOMER_ID_HID" runat="server">
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</BODY>
</HTML>

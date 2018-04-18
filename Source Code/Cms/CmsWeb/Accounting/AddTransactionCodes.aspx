<%@ Page  validateRequest=false  language="c#" Codebehind="AddTransactionCodes.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Accounting.AddTransactionCodes" %>
<%@Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>ACT_TRANSACTION_CODES</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
TranTypeACT  = new Array();
TranTypeACTCodes = new Array();
TranTypeNonACT = new Array();
TranTypeNonACTCodes = new Array();

TranTypeACT[0]		= "Premium Transactions";
TranTypeACTCodes[0]  = "pre";
TranTypeACT[1]		= "Fees Transactions";
TranTypeACTCodes[1]  = "fee";
TranTypeACT[2]		= "Reciepts";
TranTypeACTCodes[2]  = "rec";
TranTypeACT[3]		= "Payments";
TranTypeACTCodes[3]  = "pay";
TranTypeACT[4]		= "Discounts";
TranTypeACTCodes[4]  = "dis";

TranTypeNonACT[0]		= "Premium Notice Codes";
TranTypeNonACTCodes[0]   = "pnc";
TranTypeNonACT[1]		= "Past Due Codes";
TranTypeNonACTCodes[1]   = "pas";
TranTypeNonACT[2]		= "Print Codes";
TranTypeNonACTCodes[2]   = "pri";
TranTypeNonACT[3]		= "Cancellation Codes";
TranTypeNonACTCodes[3]   = "can";


function AddData()
{


document.getElementById('hidTRAN_ID').value	=	'New';
document.getElementById('cmbCATEGOTY_CODE').focus();
document.getElementById('cmbCATEGOTY_CODE').options.selectedIndex = 0;
document.getElementById('cmbTRAN_TYPE').options.selectedIndex = 0;
document.getElementById('txtTRAN_CODE').value  = '';
document.getElementById('txtDISPLAY_DESCRIPTION').value  = '';
document.getElementById('txtPRINT_DESCRIPTION').value  = '';
document.getElementById('cmbDEF_AMT_CALC_TYPE').options.selectedIndex = -1;
document.getElementById('txtDEF_AMT').value  = '';
document.getElementById('cmbGL_INCOME_ACC').options.selectedIndex = -1;
if(document.getElementById('btnActivateDeactivate'))
document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
DisableValidators();
ChangeColor();
}
function ResetPage()
{
	ResetForm('ACT_TRANSACTION_CODES');
	//1st populate xml selects account type then on bases of a/c type tran typ is filled and selected and then page display is customised on the bases of tran type.
	populateXML();FillTransactionTypes();populateXML();TranTypeChanged();
	ChangeColor();
	return false;
}
function populateXML()
{
	//alert('dd');
	var tempXML = document.getElementById('hidOldData').value;
	//alert(tempXML);
	DisableValidators();
	if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
	{

		if(tempXML!="")
		{
			populateFormData(tempXML,ACT_TRANSACTION_CODES);
			document.getElementById("txtDEF_AMT").value  = formatAmount(document.getElementById("txtDEF_AMT").value);
		}
		else
		{
			AddData();
		}
			
		
	}
	//document.getElementById('cmbDEF_AMT').selectedIndex = document.getElementById('txtDEF_AMT').value;	
	//ShowPercentDD();
	return false;
}
function FillTransactionTypes()
{	
	document.getElementById('cmbTRAN_TYPE').innerHTML = '';
	if(document.getElementById('cmbCATEGOTY_CODE').options.selectedIndex==1)
	{		
		oOption = document.createElement("option");
		oOption.value = "";
		oOption.text = "";
		document.getElementById("cmbTRAN_TYPE").add(oOption);
		for(var i=0;i<TranTypeACT.length;i++)
		{
			oOption = document.createElement("option");
			oOption.value = TranTypeACTCodes[i];
			oOption.text = TranTypeACT[i];
			document.getElementById("cmbTRAN_TYPE").add(oOption);
		}
	}
	else
	{
		for(var i=0;i<TranTypeNonACT.length;i++)
		{
			oOption = document.createElement("option");
			oOption.value = TranTypeNonACTCodes[i];
			oOption.text = TranTypeNonACT[i];
			document.getElementById("cmbTRAN_TYPE").add(oOption);
		}
	}
	document.getElementById('cmbTRAN_TYPE').options.selectedIndex = -1;
}
function TranTypeChanged()
{
	if(document.getElementById('cmbTRAN_TYPE').selectedIndex>-1)
	{
		var tranType = document.getElementById('cmbTRAN_TYPE').options[document.getElementById('cmbTRAN_TYPE').selectedIndex].value;
		if(tranType=='fee' || tranType=='dis')
		{
			DefAmt.style.display="inline";
			document.getElementById('rfvDEF_AMT_CALC_TYPE').setAttribute("enabled",true);
			document.getElementById('rfvDEF_AMT').setAttribute("enabled",true);
			SetAmountCaption();
		}
		else
		{
			DefAmt.style.display="none";	
			document.getElementById('rfvDEF_AMT_CALC_TYPE').setAttribute("enabled",false);		
			document.getElementById('rfvDEF_AMT').setAttribute("enabled",false);	
		}
		
		if(tranType=='dis')
		{
			document.getElementById('chkIS_DEF_NEGATIVE').checked = true;
		}	
		else
		{
			document.getElementById('chkIS_DEF_NEGATIVE').checked = false;
		}	
		
		if(tranType=='pre')
		{
			agencyCommission.style.display="block";
			agencyCommission2.style.display="block";
		}
		else
		{
			agencyCommission.style.display="none";
			agencyCommission2.style.display="none";
		}
		if(tranType=='fee')
		{
			glIncAmount.style.display="block";
			glIncAmount2.style.display="block";
			document.getElementById('rfvGL_INCOME_ACC').setAttribute("enabled",true);
		}
		else
		{
			glIncAmount.style.display="none";
			glIncAmount2.style.display="none";
			document.getElementById('rfvGL_INCOME_ACC').setAttribute("enabled",false);
		}
	}
}
function Validate()
{
	Page_ClientValidate();
	//alert(document.getElementById('cmbDEF_AMT_CALC_TYPE').selectedIndex);
	if(parseInt(document.getElementById('cmbDEF_AMT_CALC_TYPE').selectedIndex)>-1)
	   
	{
	var DefAmountType = document.getElementById('cmbDEF_AMT_CALC_TYPE').options[document.getElementById('cmbDEF_AMT_CALC_TYPE').selectedIndex].text;
	var amount = document.getElementById('txtDEF_AMT').value
	while(amount.indexOf(",")>-1)
			amount = amount.replace(",","");
	if(DefAmountType=="Percent")
	{
		if(parseFloat(amount)>100.00)
		{
			document.getElementById('revDEF_AMT').style.display = "inline";
			return false;
		}
	}
	else
	{
		//********
		amount= document.getElementById('txtDEF_AMT').value;
		whole=amount.substring(0,amount.indexOf('.'));
		while(whole.indexOf(",")>-1)
				whole = whole.replace(",","");
		if(whole.length>6)
		{
			document.getElementById('revDEF_AMT').style.display = "inline";
			return false;
		}
		//
	}
	}
}
function SetAmountCaption()
{
	if(document.getElementById('cmbDEF_AMT_CALC_TYPE').selectedIndex == 1) {
	    var strAmount = document.getElementById('hidAmount').value;
	    var strError=document.getElementById('hidError').value;
	    var strError2=document.getElementById('hidError2').value;
	    document.getElementById('capDEF_AMT').innerHTML = strAmount;   //"Default Percent";
	    document.getElementById('rfvDEF_AMT').innerHTML = strError;  //"Please enter default percent.";
	    document.getElementById('revDEF_AMT').innerHTML = strError2;  //"Please enter valid two digit percentage.";
		
	}
	else {
	    var strAmount = document.getElementById('hidAmount').value;
	    var strError = document.getElementById('hidError').value;
	    var strError2 = document.getElementById('hidError2').value;
	    document.getElementById('capDEF_AMT').innerHTML = strAmount; //"Default Amount";
	    document.getElementById('rfvDEF_AMT').innerHTML = strError;//  "Please enter default amount.";
	    document.getElementById('revDEF_AMT').innerHTML = strError2;  //"Please enter valid six digit amount.";
		
	}
	
}
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();FillTransactionTypes();populateXML();TranTypeChanged();SetAmountCaption();ApplyColor();">
		<FORM id="ACT_TRANSACTION_CODES" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCATEGOTY_CODE" runat="server">Category</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCATEGOTY_CODE" onchange="FillTransactionTypes();" onfocus="SelectComboIndex('cmbCATEGOTY_CODE')"
										runat="server">
										<asp:ListItem Value=""></asp:ListItem>
										<%--<asp:ListItem Value="ACT">Accounting</asp:ListItem>
										<asp:ListItem Value="Other">Non-Accounting</asp:ListItem>--%>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvCATEGOTY_CODE" runat="server" Display="Dynamic" ErrorMessage="CATEGOTY_CODE can't be blank."
										ControlToValidate="cmbCATEGOTY_CODE"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capTRAN_TYPE" runat="server">Transaction Type</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbTRAN_TYPE" onchange="TranTypeChanged();" onfocus="SelectComboIndex('cmbTRAN_TYPE')"
										runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvTRAN_TYPE" runat="server" Display="Dynamic" ErrorMessage="TRAN_TYPE can't be blank."
										ControlToValidate="cmbTRAN_TYPE"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capTRAN_CODE" runat="server">Transaction Code</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtTRAN_CODE" runat="server" maxlength="5" size="10"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvTRAN_CODE" runat="server" Display="Dynamic" ErrorMessage="TRAN_CODE can't be blank."
										ControlToValidate="txtTRAN_CODE"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTRAN_CODE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtTRAN_CODE"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" colSpan="2"></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capDISPLAY_DESCRIPTION" runat="server">Display Description</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" colSpan="3"><asp:textbox id="txtDISPLAY_DESCRIPTION" runat="server" maxlength="150" size="80"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvDISPLAY_DESCRIPTION" runat="server" Display="Dynamic" ErrorMessage="DISPLAY_DESCRIPTION can't be blank."
										ControlToValidate="txtDISPLAY_DESCRIPTION"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capPRINT_DESCRIPTION" runat="server">Print Description</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" colSpan="3"><asp:textbox id="txtPRINT_DESCRIPTION" runat="server" maxlength="150" size="80"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvPRINT_DESCRIPTION" runat="server" Display="Dynamic" ErrorMessage="PRINT_DESCRIPTION can't be blank."
										ControlToValidate="txtPRINT_DESCRIPTION"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr id="DefAmt">
								<TD class="midcolora" width="18%"><asp:label id="capDEF_AMT_CALC_TYPE" runat="server">Default amount Calculation Type</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist onchange="SetAmountCaption();" id="cmbDEF_AMT_CALC_TYPE" onfocus="SelectComboIndex('cmbDEF_AMT_CALC_TYPE')"
										runat="server">
										<%--<asp:ListItem Value='F'>Flat</asp:ListItem>
										<asp:ListItem Value='P'>Percent</asp:ListItem>--%>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvDEF_AMT_CALC_TYPE" runat="server" Display="Dynamic" ErrorMessage="DEF_AMT_CALC_TYPE can't be blank."
										ControlToValidate="cmbDEF_AMT_CALC_TYPE"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capDEF_AMT" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox CssClass="INPUTCURRENCY" id="txtDEF_AMT" onblur="this.value=formatAmount(this.value);"
										runat="server" maxlength="11" size="16"></asp:textbox>
									<BR>
									<asp:requiredfieldvalidator id="rfvDEF_AMT" runat="server" Display="Dynamic" ErrorMessage="DEF_AMT can't be blank."
										ControlToValidate="txtDEF_AMT"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revDEF_AMT" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtDEF_AMT"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD id="agencyCommission" class="midcolora" width="18%"><asp:label id="capAGENCY_COMM_APPLIES" runat="server">Agency commission applies</asp:label></TD>
								<TD id="agencyCommission2" class="midcolora" width="18%"><asp:checkbox id="chkAGENCY_COMM_APPLIES" runat="server"></asp:checkbox></TD>
								<TD id="glIncAmount" class="midcolora" width="18%"><asp:label id="capGL_INCOME_ACC" runat="server">GL Income Account</asp:label><span class="mandatory">*</span></TD>
								<TD id="glIncAmount2" class="midcolora" colspan="3" width="32%"><asp:dropdownlist id="cmbGL_INCOME_ACC" onfocus="SelectComboIndex('cmbGL_INCOME_ACC')" runat="server">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvGL_INCOME_ACC" runat="server" Display="Dynamic" ErrorMessage="GL_INCOME_ACC can't be blank."
										ControlToValidate="cmbGL_INCOME_ACC"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capIS_DEF_NEGATIVE" runat="server">Is Negative</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:checkbox id="chkIS_DEF_NEGATIVE" runat="server"></asp:checkbox></TD>
								<TD class="midcolora" colSpan="2"></TD>
							</tr>
							<tr>
								<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" style="Display:none" runat="server" Text="Reset"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server"  style="Display:none" Text="Activate/Deactivate"></cmsb:cmsbutton></td>
								<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave"  style="Display:none" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
							<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
							<INPUT id="hidTRAN_ID" type="hidden" name="hidTRAN_ID" runat="server">
							<input type="hidden"  id="hidAmount" runat="server" />
							<input type="hidden" id="hidError" runat="server" />
							<input type="hidden" id="hidError2" runat="server" />
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
		<script>
				RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidTRAN_ID').value);
		</script>
	</BODY>
</HTML>

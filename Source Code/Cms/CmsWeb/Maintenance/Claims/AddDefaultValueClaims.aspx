<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddDefaultValueClaims.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Claims.AddDefaultValueClaims" ValidateRequest="false" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>CLM_TYPE_DETAIL</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>
		<script language='javascript'>
function AddData()
{
	ChangeColor();
	DisableValidators();
	document.getElementById('hidDETAIL_TYPE_ID').value	=	'New';
	//document.getElementById('txtDETAIL_TYPE_DESCRIPTION').focus();
	document.getElementById('chkIS_SYSTEM_GENERATED').checked = false;
	if (document.getElementById('btnActivateDeactivate'))
	    document.getElementById('btnActivateDeactivate').setAttribute('disabled', false);
	//document.getElementById('txtDETAIL_TYPE_DESCRIPTION').value  = '';
	$("#txtDETAIL_TYPE_DESCRIPTION").focus();
}

function ResetForm() {
    document.CLM_TYPE_DETAIL.reset();
    DisableValidators();
    populateXML();
    ChangeColor();
    return false;
}




function populateXML()
{
	if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
		{

		   
			var tempXML;	 
			if(document.getElementById('hidOldData')!=null)
			{
				tempXML=document.getElementById('hidOldData').value;
				//alert(tempXML);									 							
				if(tempXML!="" && tempXML!=0)
				{	
					populateFormData(tempXML,CLM_TYPE_DETAIL);																
					if(document.getElementById('chkIS_SYSTEM_GENERATED').checked==false)
					{ 
					  //Added by Sibin on 21 Oct 08 to eliminate null object
					if(document.getElementById('btnActivateDeactivate')!=null)	
						document.getElementById('btnActivateDeactivate').style.display = "inline";
						
					//Added by Sibin on 21 Oct 08 to eliminate null object
					if(document.getElementById('btnSave')!=null)		
						document.getElementById('btnSave').style.display = "inline";
					}
					else
					{
					  //Added by Sibin on 21 Oct 08 to eliminate null object
					if(document.getElementById('btnActivateDeactivate')!=null)		
						document.getElementById('btnActivateDeactivate').style.display = "none";
						
					//Added by Sibin on 21 Oct 08 to eliminate null object
					if(document.getElementById('btnSave')!=null)		
						document.getElementById('btnSave').style.display = "none";
					}
				}
				else
				{     
				//Added by Sibin on 21 Oct 08 to eliminate null object
					if(document.getElementById('btnActivateDeactivate')!=null)	
						if(document.getElementById("btnActivateDeactivate"))
						   document.getElementById('btnActivateDeactivate').style.display = "none";
						AddData();
				}
			}
		}
		if(document.getElementById('hidFormSaved').value == '3')
		{
			if(document.getElementById('cmbTRANSACTION_CODE').options.selectedIndex!=-1)
			{
				document.getElementById('cmbTRANSACTION_CODE').value = document.getElementById('hidTRAN_CODE').value;
			}


}


		 
}

function SelectItem()
{
	if (document.getElementById("lstAssignedCrAcct").options[0])
		document.getElementById("lstAssignedCrAcct").options[0].selected=true;
	
	if (document.getElementById("lstAssignedDrAcct").options[0])
		document.getElementById("lstAssignedDrAcct").options[0].selected=true;
		
	return false;
}




function ShowTcode() {
    
	if (document.getElementById('hidTYPE_ID').value == 8)  //for Claim Transaction Code
	{
	    document.getElementById('trTcode').style.display = "inline";
	    document.getElementById('rfvTRANSACTION_CODE').style.display = "none";
	    document.getElementById('rfvTRANSACTION_CODE').setAttribute("enabled", false);
	    document.getElementById('rfvTRANSACTION_CODE').setAttribute("isValid", true);
	    document.getElementById('spnTRANSACTION_CODE').style.display = "none";
	}
	else
	{
		document.getElementById('trTcode').style.display = "none";
		document.getElementById('rfvTRANSACTION_CODE').style.display = "none";
		document.getElementById('rfvTRANSACTION_CODE').setAttribute("enabled",false);
		document.getElementById('rfvTRANSACTION_CODE').setAttribute("isValid",false);
	}

}


function GetValues() {
    
    var result = AddDefaultValueClaims.AjaxFetchExtraCoverages(document.getElementById('cmbLOSS_DEPARTMENT').value);

        fillDTCombo(result.value, document.getElementById('cmbLOSS_EXTRA_COVER'), 'COV_ID', 'COV_DES', 0);
        
}

function fillDTCombo(objDT, combo, valID, txtDesc, tabIndex) {
    
    combo.innerHTML = "";
    if (objDT != null) {

        for (i = 0; i < objDT.Tables[tabIndex].Rows.length; i++) {

            if (i == 0) {
                oOption = document.createElement("option");
                oOption.value = "";
                oOption.text = "";
                combo.add(oOption);
            }
            oOption = document.createElement("option");
            oOption.value = objDT.Tables[tabIndex].Rows[i][valID];
            oOption.text = objDT.Tables[tabIndex].Rows[i][txtDesc];
            combo.add(oOption);
        }
    }
}

function SetExtraCover() {
    if (document.getElementById('cmbLOSS_EXTRA_COVER').value != "") {

        //var e = document.getElementById("cmbLOSS_EXTRA_COVER"); // select element
        //var strValue = e.options[e.selectedIndex].text;
        //alert(strValue);

        document.getElementById('hidExtraCover').value = document.getElementById("cmbLOSS_EXTRA_COVER").value;

    }
}
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();ShowTcode();'>
		<FORM id='CLM_TYPE_DETAIL' method='post' runat='server'>
			<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
				<TR>
					<TD>
						<TABLE width='100%' border='0' align='center'>
							<tr>
								<TD class="pageHeader" colSpan="4"><asp:label ID="capMessages" runat="server" Text= "Please note that all fields marked with * are mandatory"></asp:label></TD>
							</tr>
							<tr id="trbody" runat="server">
								<td colspan="4">
									<table width="100%">
										<tr>
											<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
										</tr>

                                        <!-- Added by Agniswar for Singapore Implementation -->
                                        <tr id="trLossCode" runat="server" visible="true">
											<TD class='midcolora' width='24%'>
												<asp:Label id="capLOSS_TYPE_CODE" runat="server">Loss Type Code</asp:Label><span id="spnLOSS_TYPE_CODE" runat="server" class="mandatory">*</span></TD>
											<TD class='midcolora' ColSpan='3'>
												<asp:textbox id='txtLOSS_TYPE_CODE' runat='server' size='20' maxlength='0'></asp:textbox><br>
												<asp:requiredfieldvalidator id="rfvLOSS_TYPE_CODE" runat="server" ControlToValidate="txtLOSS_TYPE_CODE"
													Display="Dynamic"></asp:requiredfieldvalidator>
											</TD>
										</tr>

                                        <!-- Till Here -->

										<tr>
											<TD class='midcolora' width='24%'>
												<asp:Label id="capDETAIL_TYPE_DESCRIPTION" runat="server">Description</asp:Label><span class="mandatory">*</span></TD>
											<TD class='midcolora' ColSpan='3'>
												<asp:textbox id='txtDETAIL_TYPE_DESCRIPTION' runat='server' size='50' maxlength='0'></asp:textbox><br>
												<asp:requiredfieldvalidator id="rfvDETAIL_TYPE_DESCRIPTION" runat="server" ControlToValidate="txtDETAIL_TYPE_DESCRIPTION"
													Display="Dynamic"></asp:requiredfieldvalidator>
											</TD>
										</tr>

                                        <!-- Added by Agniswar for Singapore Implementation -->
                                        <tr id="trLOSS_DEPARTMENT" runat="server" visible="true">
											<TD class='midcolora' width='24%'>
												<asp:Label id="capLOSS_DEPARTMENT" runat="server">Department</asp:Label><span id="spnLOSS_DEPARTMENT" runat="server" class="mandatory">*</span></TD>
											<TD class='midcolora' ColSpan='3'>
												<asp:dropdownlist id="cmbLOSS_DEPARTMENT" onfocus="SelectComboIndex('cmbLOSS_DEPARTMENT')" onchange="GetValues();" runat="server" width="105" Enabled="true"></asp:dropdownlist><br>
												<%--<asp:requiredfieldvalidator id="rfvLOSS_DEPARTMENT" runat="server" ControlToValidate="cmbLOSS_DEPARTMENT"
													Display="Dynamic"></asp:requiredfieldvalidator>--%>
											</TD>
										</tr>

                                        <tr id="trExtraCover" runat="server" visible="true">
											<TD class='midcolora' width='24%'>
												<asp:Label id="capLOSS_EXTRA_COVER" runat="server">Extra Cover</asp:Label><span id="spnLOSS_EXTRA_COVER" runat="server" class="mandatory">*</span></TD>
											<TD class='midcolora' ColSpan='3'>
												<asp:dropdownlist id="cmbLOSS_EXTRA_COVER" onfocus="SelectComboIndex('cmbLOSS_EXTRA_COVER')" onchange="javascript:SetExtraCover();" runat="server" width="105" Enabled="true"></asp:dropdownlist><br>
												<%--<asp:requiredfieldvalidator id="rfvLOSS_EXTRA_COVER" runat="server" ControlToValidate="cmbLOSS_EXTRA_COVER"
													Display="Dynamic"></asp:requiredfieldvalidator>--%>
											</TD>
										</tr>

                                        <!-- Till Here -->


										<tr id="trTcode" runat="server">
											<TD class='midcolora' width='24%'>
												<asp:Label id="lblTRANSACTION_CODE1" runat="server">Transaction Code</asp:Label><span id="spnTRANSACTION_CODE" class="mandatory">*</span></TD>
											<TD class='midcolora' width='26%'>
												<asp:dropdownlist id="cmbTRANSACTION_CODE" onfocus="SelectComboIndex('cmbTRANSACTION_CODE')" runat="server" Enabled="False"></asp:dropdownlist><br>
												<asp:requiredfieldvalidator id="rfvTRANSACTION_CODE" runat="server" ControlToValidate="cmbTRANSACTION_CODE"
													Display="Dynamic"></asp:requiredfieldvalidator>
											</TD>
											<TD class='midcolora' width='24%'>
												<asp:Label id="capTRANSACTION_CATEGORY" runat="server"></asp:Label><%--Transaction Category--%>
											</TD>
											<TD class='midcolora' width='26%'>
												<asp:dropdownlist id="cmbTRANSACTION_CATEGORY" onfocus="SelectComboIndex('cmbTRANSACTION_CATEGORY')" runat="server"></asp:dropdownlist><br>
											</TD>
										</tr>
										<tr id="trSystemGenerated" runat="server" visible="true">
											<TD class='midcolora' width='24%'>
												<asp:Label id="capIS_SYSTEM_GENERATED" runat="server">System Generated</asp:Label></TD>
											<TD class='midcolora' ColSpan='3'>
												<asp:CheckBox id="chkIS_SYSTEM_GENERATED" runat="server" disabled="true"></asp:CheckBox><br>
											</TD>
										</tr>
										<tr id="trAccountingPosting" runat="server">
											<TD class="headerEffectSystemParams" colSpan="4">
												<span id="spnAccountingPosting" runat="server"><asp:Label ID="capACCOUNTING_POSTING" runat="server"></asp:Label></span><%--Accounting Posting --%>
											</TD>
										</tr>
										<tr runat="server" id="trDebit">
											<TD class='midcolora' colspan="4">
												<span id="spnDebitAccount" runat="server"><asp:label ID="capDebitAccount" runat="server"></asp:label><span class="mandatory">*</span>
												</span>
											</TD>
										</tr>
										<tr id="trDrActList" runat="server">
											<TD class='midcolora' colspan="4">
												<table width="100%">
													<tr>
														<td width="25%" class='midcolora'>
															<asp:ListBox ID="lstUnassignedDrAcct" SelectionMode='1' Runat="server" Width="412" Height="100"></asp:ListBox>
														</td>
														<td width="10%" align="center" class='midcolora'>
															<asp:Button CssClass="clsbutton" CausesValidation="False" ID="btnSelectAllDrAcct" Runat="server"
																Text=">>" Width="30"></asp:Button><br>
															<asp:Button CssClass="clsbutton" CausesValidation="False" ID="btnSelectDrAcct" Runat="server"
																Text=">" Width="30"></asp:Button><br>
															<asp:Button CssClass="clsbutton" CausesValidation="False" ID="btnDeSelectDrAcct" Runat="server"
																Text="<" Width="30"></asp:Button><br>
															<asp:Button CssClass="clsbutton" CausesValidation="False" ID="btnDeSelectAllDrAcct" Runat="server"
																Text="<<" Width="30"></asp:Button>
														</td>
														<td width="65%" class='midcolora'>
															<asp:ListBox ID="lstAssignedDrAcct" Runat="server" Width="412" Height="100"></asp:ListBox>
															<br>
															<asp:RequiredFieldValidator ID="rfvAssignedDrAcct" ControlToValidate="lstAssignedDrAcct" Display="Dynamic" ErrorMessage=""
																Runat="server"></asp:RequiredFieldValidator><%--Please select at least one account for debit--%>
														</td>
													</tr>
												</table>
											</TD>
										</tr>
										<tr runat="server" id="trCredit">
											<TD class='midcolora' colspan="4">
												<span id="spnCreditAccount" runat="server"><asp:label ID="capCreditAccount" runat="server"></asp:label><span class="mandatory">*</span>
												</span>
											</TD>
										</tr>
										<tr id="trCrActList" runat="server">
											<td colspan="4">
												<table width="100%">
													<tr>
														<td width="25%" class='midcolora'>
															<asp:ListBox ID="lstUnassignedCrAcct" SelectionMode='1' Runat="server" Width="412" Height="100"></asp:ListBox>
														</td>
														<td width="10%" align="center" class='midcolora'>
															<asp:Button CssClass="clsbutton" CausesValidation="False" ID="btnSelectAllCrAcct" Runat="server"
																Text=">>" Width="30"></asp:Button><br>
															<asp:Button CssClass="clsbutton" CausesValidation="False" ID="btnSelectCrAcct" Runat="server"
																Text=">" Width="30"></asp:Button><br>
															<asp:Button CssClass="clsbutton" CausesValidation="False" ID="btnDeSelectCrAcct" Runat="server"
																Text="<" Width="30"></asp:Button><br>
															<asp:Button CssClass="clsbutton" CausesValidation="False" ID="btnDeSelectAllCrAcct" Runat="server"
																Text="<<" Width="30"></asp:Button>
														</td>
														<td width="65%" class='midcolora'>
															<asp:ListBox ID="lstAssignedCrAcct" Runat="server" Width="412" Height="100"></asp:ListBox>
															<br>
															<asp:RequiredFieldValidator ID="rfvAssignedCrAcct" ControlToValidate="lstAssignedCrAcct" Display="Dynamic" ErrorMessage=""
																Runat="server"></asp:RequiredFieldValidator><%--Please select at least one account for credit--%>
														</td>
													</tr>
												</table>
											</td>
										</tr>
                                        
										<tr>
											<td class='midcolora' width='24%'>
												<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
												<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server"
												Text="Deactivate" CausesValidation="False"   ></cmsb:cmsbutton>
											</td>
											<td class='midcolorr' colspan="3">
												<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
							<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
							<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> 
                            <INPUT id="hidDETAIL_TYPE_ID" type="hidden" value="0" name="hidDETAIL_TYPE_ID" runat="server">
							<INPUT id="hidTYPE_ID" type="hidden" value="0" name="hidTYPE_ID" runat="server">
							<INPUT id="hidTRAN_CODE" type="hidden" value="0" name="hidTRAN_CODE" runat="server">
                            <INPUT id="hidExtraCover" type="hidden" name="hidExtraCover" runat="server">
							
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidDETAIL_TYPE_ID').value);	
		</script>
	</BODY>
</HTML>

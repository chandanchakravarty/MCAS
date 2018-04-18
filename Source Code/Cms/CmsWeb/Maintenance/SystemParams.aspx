<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Page language="c#" Codebehind="SystemParams.aspx.cs" AutoEventWireup="false" ValidateRequest="false" Inherits="Cms.CmsWeb.Maintenance.SystemParams" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MNT_SYSTEM_PARAMS</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href='/cms/cmsweb/css/css<%=GetColorScheme()%>.css' type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<SCRIPT src="/cms/cmsweb/scripts/common.js"></SCRIPT>
		<SCRIPT src="/cms/cmsweb/scripts/form.js"></SCRIPT>
		<script language='javascript'>

		    function AddData()
		{
		/*document.getElementById('txtSYS_BAD_LOGON_ATTMPT').focus();
			document.getElementById('txtSYS_BAD_LOGON_ATTMPT').value = '';
			document.getElementById('txtSYS_RENEWAL_FOR_ALERT').value = '';
			document.getElementById('txtSYS_PENDING_QUOTE_FOR_ALERT').value = '';
			document.getElementById('txtSYS_QUOTED_QUOTE_FOR_ALERT').value = '';
			document.getElementById('txtSYS_NUM_DAYS_EXPIRE').value = '';
			document.getElementById('txtSYS_NUM_DAYS_PEN_TO_NTU').value = '';
			document.getElementById('txtSYS_NUM_DAYS_EXPIRE_QUOTE').value = '';
			document.getElementById('cmbSYS_DEFAULT_POL_TERM').options.selectedIndex = 1;
			
			document.getElementById('txtSYS_NON_SUFFICIENT_FUND_FEES').value	= '';
			document.getElementById('txtSYS_REINSTATEMENT_FEES').value ='';
			document.getElementById('txtSYS_EMPLOYEE_DISCOUNT').value ='';
			document.getElementById('cmbSYS_PRINT_FOLLOWING').options.SelectedIndex =0;
			document.getElementById('txtSYS_CLAIME_NUMBER').value ='';
			document.getElementById('txtSYS_INSTALLMENT_FEES').value ='';
			document.getElementById('cmbSYS_STATEMENT_NAME_LOGO').options.SelectedIndex =0;
			document.getElementById('cmbSYS_INDICATE_POL').options.SelectedIndex =0;
			
			*/
			//document.getElementById('btnReset').reset();
			document.MNT_SYSTEM_PARAMS.reset();
			DisableValidators();
	
			EnableValidator('rfvInstallmentFees',false);
			EnableValidator('revInstallmentFees',false);

			EnableValidator('rfvNonSufficientFundFees',false);
			EnableValidator('revNonSufficientFundFees',false);

			EnableValidator('rfvReinstatementFees',false);
			EnableValidator('revReinstatementFees',false);

			EnableValidator('rfvEmployeeDiscount',false);
			EnableValidator('rngEmployeeDiscount',false);

			EnableValidator('rfvMinInstallPlan',false);
			EnableValidator('revMinInstallPlan',false);

			EnableValidator('rfvAmtUnderPayment',false);
			EnableValidator('revAmtUnderPayment',false);

			EnableValidator('rfvMinDays_Premium',false);
			EnableValidator('revMinDays_Premium',false);

			EnableValidator('rfvMinDays_Cancel',false);
			EnableValidator('revMinDays_Cancel',false);

			EnableValidator('rfvPostPhone',false);
			EnableValidator('revPostPhone',false);

			EnableValidator('rfvPostCancel',false);
			EnableValidator('revPostCancel',false);
			
			ChangeColor();
			return false;
}
function validate1() {
    
    if (document.getElementById('revBadLogin').isvalid == false) {
        document.getElementById('rngSYS_BAD_LOGON_ATTMPT').setAttribute('enabled', false);
        document.getElementById('rngSYS_BAD_LOGON_ATTMPT').style.display = 'none';

    }
    else
        document.getElementById('rngSYS_BAD_LOGON_ATTMPT').setAttribute('enabled', true);
}
		
		</script>
		<!--onkeypress="if(event.keyCode==13){ document.getElementById('btnSave').click();return false;}"-->
	</HEAD>
	<BODY  class="bodyBackGround" leftMargin="0" topMargin="0" onresize="SmallScroll();" onload="top.topframe.main1.mousein = false;findMouseIn();showScroll();ApplyColor();AddData();">
		<!--Start: to add bottom menu-->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!--End: bottom menu-->
		<!--Start: to add band space -->
		<!--End: band space -->
		<div id="bodyHeight" class="pageContent">
			<FORM id='MNT_SYSTEM_PARAMS' method='post' runat='server'>
				<TABLE cellSpacing='0' cellPadding='0' class="tableWidth" border='0'>
					<tr>
						<td>
							<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
						</td>
					</tr>
					<tr>
						<td class="headereffectCenter" colSpan="3"><asp:label id="capGeneralSetup" runat="server" ></asp:label></td>
					</tr>
					<tr>
						<TD class="pageHeader" width="100%" colSpan="4"><asp:label id="capManHeader" runat="server" ></asp:label></TD>
					</tr>
					<tr>
						<td class="midcolorc" width="100%"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
					</tr>
					<TR>
						<TD>
							<TABLE width='100%' border='0' align='center'>
								<TBODY>
									<tr>
										<TD class='headereffectSystemParams' colspan='4'><asp:label id="capSystemParameters" runat="server" ></asp:label></TD>
									</tr>
									<tr>
										<TD class='midcolora' width="35%"><asp:label id="capSYS_BAD_LOGON_ATTMPT" Runat="server"></asp:label><span class="mandatory">*</span></TD>
										<TD class='midcolora' width="15%">
											<asp:textbox id='txtSYS_BAD_LOGON_ATTMPT' runat='server' size='4' maxlength='2' onblur="validate1();"></asp:textbox><BR>
											<asp:RequiredFieldValidator id="rfvBadLogin" runat="server" Display="Dynamic" ControlToValidate="txtSYS_BAD_LOGON_ATTMPT"></asp:RequiredFieldValidator>
											<asp:RegularExpressionValidator id="revBadLogin" runat="server" ControlToValidate="txtSYS_BAD_LOGON_ATTMPT"  Display="Dynamic"></asp:RegularExpressionValidator>
											<%--Added by Sibin on 5 Dec 09--%>
											<asp:rangevalidator id="rngSYS_BAD_LOGON_ATTMPT" Runat="server" ControlToValidate="txtSYS_BAD_LOGON_ATTMPT" Display="Dynamic" MinimumValue="0" MaximumValue="10" Type="Integer" ErrorMessage="No. of Login attempts cannot be greater than 10"></asp:rangevalidator>
										</TD>
										<TD class='midcolora' width="35%"><asp:label id="capINSURANCE_SCORE_VALIDITY" Runat="server"></asp:label><span class="mandatory">*</span></TD>
										<TD class='midcolora' width="15%">
											<asp:TextBox ID="txtINSURANCE_SCORE_VALIDITY" Runat="server" MaxLength="4" size="8"></asp:TextBox><BR>
											<asp:RequiredFieldValidator id="rfvINSURANCE_SCORE_VALIDITY" runat="server" ControlToValidate="txtINSURANCE_SCORE_VALIDITY"
												Display="Dynamic"></asp:RequiredFieldValidator></TD>
									</tr>
									<tr>
									<td class='midcolora' width='35%'><asp:Label runat="server" ID="capBASE_CURRENCY" ></asp:Label></td>
									<td class='midcolora' width='15%'><asp:DropDownList runat="server" ID="cmbBASE_CURRENCY" ></asp:DropDownList></br>
									<asp:RequiredFieldValidator ID=rfvBASE_CURRENCY runat="server" ControlToValidate="cmbBASE_CURRENCY" Display="Dynamic"></asp:RequiredFieldValidator></td>
									<td class='midcolora' width="35%">
									</td>
									<td class='midcolora' width="15%">
									</td>
									</tr>
									<tbody style="DISPLAY:none">
									<tr id="tr1" >
										<TD class='headereffectSystemParams' colspan='4'>Diary Parameters</TD>
									</tr>
									<tr id="tr2">
										<TD class='midcolora'><asp:label id="capSYS_RENEWAL_FOR_ALERT" Runat="server"></asp:label><span class="mandatory">*</span></TD>
										<TD class='midcolora'>
											<asp:textbox id='txtSYS_RENEWAL_FOR_ALERT' runat='server' size='6' maxlength='3'></asp:textbox><BR>
											<asp:RequiredFieldValidator id="rfvRenewalAlert" runat="server" Display="Dynamic" ControlToValidate="txtSYS_RENEWAL_FOR_ALERT"></asp:RequiredFieldValidator>
											<asp:RegularExpressionValidator id="revRenewalAlert" runat="server" ControlToValidate="txtSYS_RENEWAL_FOR_ALERT"
												Display="Dynamic"></asp:RegularExpressionValidator>
										</TD>
										<TD class='midcolora'><asp:label id="capSYS_PENDING_QUOTE_FOR_ALERT" Runat="server"></asp:label><span class="mandatory">*</span></TD>
										<TD class='midcolora'>
											<asp:textbox id='txtSYS_PENDING_QUOTE_FOR_ALERT' runat='server' size='6' maxlength='3'></asp:textbox><BR>
											<asp:RequiredFieldValidator id="rfvPendingQuoteAlert" runat="server" Display="Dynamic" ControlToValidate="txtSYS_PENDING_QUOTE_FOR_ALERT"></asp:RequiredFieldValidator>
											<asp:RegularExpressionValidator id="revPendingQuoteAlert" runat="server" ControlToValidate="txtSYS_PENDING_QUOTE_FOR_ALERT"
												Display="Dynamic"></asp:RegularExpressionValidator>
										</TD>
									</tr>
									<tr id="tr3"> 
										<TD class='midcolora'><asp:label id="capSYS_QUOTED_QUOTE_FOR_ALERT" Runat="server"></asp:label><span class="mandatory">*</span></TD>
										<TD class='midcolora'>
											<asp:textbox id='txtSYS_QUOTED_QUOTE_FOR_ALERT' runat='server' size='6' maxlength='3'></asp:textbox><BR>
											<asp:RequiredFieldValidator id="rfvQuotedQuoteAlert" runat="server" Display="Dynamic" ControlToValidate="txtSYS_QUOTED_QUOTE_FOR_ALERT"></asp:RequiredFieldValidator>
											<asp:RegularExpressionValidator id="revQuotedQuoteAlert" runat="server" ControlToValidate="txtSYS_QUOTED_QUOTE_FOR_ALERT"
												Display="Dynamic"></asp:RegularExpressionValidator>
										</TD>
										<TD class='midcolora' ColSpan='2'></TD>
									</tr>
									</tbody>
									<tr>
										<TD class='headereffectSystemParams' colspan='4'><asp:label id="capPolicyActions" runat="server" ></asp:label></TD>
									</tr>
									<tr>
										<TD class='midcolora'><asp:label id="capSYS_NUM_DAYS_EXPIRE" Runat="server"></asp:label><span class="mandatory">*</span></TD>
										<TD class='midcolora'>
											<asp:textbox id='txtSYS_NUM_DAYS_EXPIRE' runat='server' size='6' maxlength='3'></asp:textbox><BR>
											<asp:RequiredFieldValidator id="rfvNumberDaysExpire" runat="server" Display="Dynamic" ControlToValidate="txtSYS_NUM_DAYS_EXPIRE"></asp:RequiredFieldValidator>
											<asp:RegularExpressionValidator id="revNumberDaysExpire" runat="server" ControlToValidate="txtSYS_NUM_DAYS_EXPIRE"
												Display="Dynamic"></asp:RegularExpressionValidator>
										</TD>
										<TD class='midcolora'><asp:label id="capSYS_NUM_DAYS_PEN_TO_NTU" Runat="server"></asp:label><span class="mandatory">*</span></TD>
										<TD class='midcolora'>
											<asp:textbox id='txtSYS_NUM_DAYS_PEN_TO_NTU' runat='server' size='6' maxlength='3'></asp:textbox><BR>
											<asp:RequiredFieldValidator id="rfvNumberDaysPendingNTU" runat="server" Display="Dynamic" ControlToValidate="txtSYS_NUM_DAYS_PEN_TO_NTU"></asp:RequiredFieldValidator>
											<asp:RegularExpressionValidator id="revNumberDaysPendingNTU" runat="server" ControlToValidate="txtSYS_NUM_DAYS_PEN_TO_NTU"
												Display="Dynamic"></asp:RegularExpressionValidator>
										</TD>
									</tr>
									<tr>
										<TD class='midcolora'><asp:label id="capSYS_NUM_DAYS_EXPIRE_QUOTE" Runat="server"></asp:label><span class="mandatory">*</span></TD>
										<TD class='midcolora'>
											<asp:textbox id='txtSYS_NUM_DAYS_EXPIRE_QUOTE' runat='server' size='6' maxlength='3'></asp:textbox><BR>
											<asp:RequiredFieldValidator id="rfvNumberDaysExpireQuote" runat="server" Display="Dynamic" ControlToValidate="txtSYS_NUM_DAYS_EXPIRE_QUOTE"></asp:RequiredFieldValidator>
											<asp:RegularExpressionValidator id="revNumberDaysExpireQuote" runat="server" ControlToValidate="txtSYS_NUM_DAYS_EXPIRE_QUOTE"
												Display="Dynamic"></asp:RegularExpressionValidator>
										</TD>
										<TD class='midcolora'><asp:label id="capSYS_DEFAULT_POL_TERM" Runat="server"></asp:label><span class="mandatory">*</span></TD>
										<TD class='midcolora'>
											<asp:DropDownList id='cmbSYS_DEFAULT_POL_TERM' runat='server'>
												<%--<asp:ListItem Value='0'>6 Months</asp:ListItem>
												<asp:ListItem Value='1'>1 Year</asp:ListItem>
												<asp:ListItem Value='2'>2 Year</asp:ListItem>--%>
											</asp:DropDownList>
										</TD>
									</tr>
									<tr>
										<TD class='midcolora'><asp:label id="capSYS_INDICATE_POL" Runat="server"></asp:label></TD>
										<TD class='midcolora' colspan='3'>
											<asp:DropDownList id="cmbSYS_INDICATE_POL" runat='server'>
												<asp:ListItem Value=''></asp:ListItem>
												<asp:ListItem Value='Lapsed'></asp:ListItem><%--Lapsed--%>
												<asp:ListItem Value='Expired'></asp:ListItem><%--Expired--%>
											</asp:DropDownList>
										</TD>
									</tr>
									<tr><td class='midcolora'>
                                        <asp:Label ID="capDAYS_FOR_BOLETO_EXPIRATION" runat="server" ></asp:Label> </td>
                                        <td class='midcolora' colspan='2'>
                                            <asp:TextBox ID="txtDAYS_FOR_BOLETO_EXPIRATION" runat="server" size='6' maxlength='3'></asp:TextBox> 
                                            <br /><asp:RegularExpressionValidator id="revDAYS_FOR_BOLETO_EXPIRATION" runat="server" ControlToValidate="txtDAYS_FOR_BOLETO_EXPIRATION"
												Display="Dynamic"></asp:RegularExpressionValidator></td>
                                        </tr>
									<tr>
										<TD class='headereffectSystemParams' colspan='4'><asp:label id="capStatementParameters" runat="server" ></asp:label></TD>
									</tr>
									<tr>
										<TD class='midcolora'><asp:label id="capSYS_STATEMENT_NAME_LOGO" Runat="server"></asp:label></TD>
										<TD class='midcolora' colspan='3'>
											<asp:DropDownList id="cmbSYS_STATEMENT_NAME_LOGO" runat='server'>
												<asp:ListItem Value=''></asp:ListItem>
												<asp:ListItem Value='Yes'></asp:ListItem><%--Yes--%>
												<asp:ListItem Value='No'></asp:ListItem><%--No--%>
											</asp:DropDownList>
										</TD>
									</tr>
									<tr>
										<TD class='midcolora'><asp:label id="capSYS_PRINT_FOLLOWING" Runat="server"></asp:label></TD>
										<TD class='midcolora' colspan='3'>
											<asp:DropDownList id="cmbSYS_PRINT_FOLLOWING" runat='server'>
												<asp:ListItem Value=''></asp:ListItem>
												<asp:ListItem Value='Office Name and Address'>Division Name and Address</asp:ListItem>
												<asp:ListItem Value='Department Name and Address'>Department Name and Address</asp:ListItem>
												<asp:ListItem Value='Profit Center Address'>Profit Center Address</asp:ListItem>
											</asp:DropDownList>
										</TD>
									</tr>
									<tr>
										<TD class='headereffectSystemParams' colspan='4'><asp:label id="capClaimsParameters" runat="server" ></asp:label></TD>
									</tr>
									<tr>
										<TD class='midcolora'><asp:label id="capSYS_CLAIME_NUMBER" Runat="server"></asp:label></TD>
										<TD class='midcolora' colspan='3'>
											<asp:TextBox ID="txtSYS_CLAIME_NUMBER" Runat="server" MaxLength="10" size='13'></asp:TextBox><BR>
											<asp:RegularExpressionValidator id="revClaimsNumber" runat="server" ControlToValidate="txtSYS_CLAIME_NUMBER" Display="Dynamic"></asp:RegularExpressionValidator>
										</TD>
									</tr>
									<tr>
										<TD class='headereffectSystemParams' colspan='4'><asp:label id="capCertifiedMailParameters" runat="server" ></asp:label></TD>
									</tr>
									<tr>
										<TD class='midcolora'><asp:label id="capPOSTAGE_FEE" Runat="server"></asp:label></TD>
										<TD class='midcolora'>
											<asp:TextBox ID="txtPOSTAGE_FEE" Runat="server" MaxLength="10" size='13'></asp:TextBox><BR>
											<asp:RegularExpressionValidator id="revPOSTAGE_FEE" ErrorMessage="Please provide only integer." runat="server" ControlToValidate="txtPOSTAGE_FEE" Display="Dynamic"></asp:RegularExpressionValidator>
										</TD>
										<TD class='midcolora'><asp:label id="capRESTR_DELIV_FEE" Runat="server"></asp:label></TD>
										<TD class='midcolora'>
											<asp:TextBox ID="txtRESTR_DELIV_FEE" Runat="server" MaxLength="10" size='13'></asp:TextBox><BR>
											<asp:RegularExpressionValidator id="revRESTR_DELIV_FEE" ErrorMessage="Please provide only integer." runat="server" ControlToValidate="txtRESTR_DELIV_FEE" Display="Dynamic"></asp:RegularExpressionValidator>
										</TD>
									</tr>
									<tr>
										<TD class='midcolora'><asp:label id="capCERTIFIED_FEE" Runat="server"></asp:label></TD>
										<TD class='midcolora'>
											<asp:TextBox ID="txtCERTIFIED_FEE" Runat="server" MaxLength="10" size='13'></asp:TextBox><BR>
											<asp:RegularExpressionValidator id="revCERTIFIED_FEE" ErrorMessage="Please provide only integer." runat="server" ControlToValidate="txtCERTIFIED_FEE" Display="Dynamic"></asp:RegularExpressionValidator>
										</TD>
										<TD class='midcolora'><asp:label id="capRET_RECEIPT_FEE" Runat="server"></asp:label></TD>
										<TD class='midcolora'>
											<asp:TextBox ID="txtRET_RECEIPT_FEE" Runat="server" MaxLength="10" size='13'></asp:TextBox><BR>
											<asp:RegularExpressionValidator id="revRET_RECEIPT_FEE" ErrorMessage="Please provide only integer." runat="server" ControlToValidate="txtRET_RECEIPT_FEE" Display="Dynamic"></asp:RegularExpressionValidator>
										</TD>
									</tr>
									<tbody style="DISPLAY:none">
										<tr>
											<TD class='headereffectSystemParams' colspan='4'>Accounting Parameters</TD>
										</tr>
										<tr>
											<TD class='midcolora'><asp:label id="capSYS_INSTALLMENT_FEES" Runat="server"></asp:label><span class="mandatory">*</span></TD>
											<TD class='midcolora'><asp:TextBox ID="txtSYS_INSTALLMENT_FEES" Runat="server" MaxLength="6" size="8" CssClass="INPUTCURRENCY"></asp:TextBox><BR>
												<asp:RequiredFieldValidator id="rfvInstallmentFees" runat="server" ControlToValidate="txtSYS_INSTALLMENT_FEES"
													Display="Dynamic"></asp:RequiredFieldValidator>
												<asp:RegularExpressionValidator id="revInstallmentFees" runat="server" ControlToValidate="txtSYS_INSTALLMENT_FEES"
													Display="Dynamic"></asp:RegularExpressionValidator>
											</TD>
											<TD class='midcolora'><asp:label id="capSYS_NON_SUFFICIENT_FUND_FEES" Runat="server"></asp:label><span class="mandatory">*</span></TD>
											<TD class='midcolora'>
												<asp:TextBox ID="txtSYS_NON_SUFFICIENT_FUND_FEES" Runat="server" MaxLength="6" size="8" CssClass="INPUTCURRENCY"></asp:TextBox><BR>
												<asp:RequiredFieldValidator id="rfvNonSufficientFundFees" runat="server" ControlToValidate="txtSYS_NON_SUFFICIENT_FUND_FEES"
													Display="Dynamic"></asp:RequiredFieldValidator>
												<asp:RegularExpressionValidator id="revNonSufficientFundFees" runat="server" ControlToValidate="txtSYS_NON_SUFFICIENT_FUND_FEES"
													Display="Dynamic"></asp:RegularExpressionValidator>
											</TD>
										</tr>
										<tr>
											<TD class='midcolora'><asp:label id="capSYS_REINSTATEMENT_FEES" Runat="server"></asp:label><span class="mandatory">*</span></TD>
											<TD class='midcolora'>
												<asp:TextBox ID="txtSYS_REINSTATEMENT_FEES" Runat="server" MaxLength="6" size="8" CssClass="INPUTCURRENCY"></asp:TextBox><BR>
												<asp:RequiredFieldValidator id="rfvReinstatementFees" runat="server" ControlToValidate="txtSYS_REINSTATEMENT_FEES"
													Display="Dynamic"></asp:RequiredFieldValidator>
												<asp:RegularExpressionValidator id="revReinstatementFees" runat="server" ControlToValidate="txtSYS_REINSTATEMENT_FEES"
													Display="Dynamic"></asp:RegularExpressionValidator>
											</TD>
											<TD class='midcolora'><asp:label id="capSYS_EMPLOYEE_DISCOUNT" Runat="server"></asp:label><span class="mandatory">*</span></TD>
											<TD class='midcolora'>
												<asp:TextBox ID="txtSYS_EMPLOYEE_DISCOUNT" Runat="server" MaxLength="5" size="8" onblur="this.value = formatAmount(this.value);"
													CssClass="INPUTCURRENCY"></asp:TextBox><BR>
												<asp:RequiredFieldValidator id="rfvEmployeeDiscount" runat="server" ControlToValidate="txtSYS_EMPLOYEE_DISCOUNT"
													Display="Dynamic"></asp:RequiredFieldValidator>
												<asp:rangevalidator id="rngEmployeeDiscount" Runat="server" ControlToValidate="txtSYS_EMPLOYEE_DISCOUNT"
													Display="Dynamic" ErrorMessage="Please enter employee discount between  0 and 100" MinimumValue="0"
													MaximumValue="100" Type="Double"></asp:rangevalidator><%--<asp:RegularExpressionValidator
                                                        ID="RegularExpressionValidator1" runat="server" ErrorMessage="RegularExpressionValidator"></asp:RegularExpressionValidator>--%>
											</TD>
										</tr>
										<tr>
											<TD class='midcolora'><asp:label id="capMinInstallPlan" Runat="server"></asp:label><span class="mandatory">*</span></TD>
											<TD class='midcolora'><asp:TextBox ID="txtMinInstallPlan" Runat="server" MaxLength="4" size="8" CssClass="INPUTCURRENCY"></asp:TextBox><BR>
												<asp:RequiredFieldValidator id="rfvMinInstallPlan" runat="server" ControlToValidate="txtMinInstallPlan" Display="Dynamic"></asp:RequiredFieldValidator>
												<asp:RegularExpressionValidator id="revMinInstallPlan" runat="server" ControlToValidate="txtMinInstallPlan" Display="Dynamic"></asp:RegularExpressionValidator>
											</TD>
											<TD class='midcolora'><asp:label id="capAmtUnderPayment" Runat="server"></asp:label><span class="mandatory">*</span></TD>
											<TD class='midcolora'><asp:TextBox ID="txtAmtUnderPayment" Runat="server" MaxLength="4" size="8" CssClass="INPUTCURRENCY"></asp:TextBox><BR>
												<asp:RequiredFieldValidator id="rfvAmtUnderPayment" runat="server" ControlToValidate="txtAmtUnderPayment" Display="Dynamic"></asp:RequiredFieldValidator>
												<asp:RegularExpressionValidator id="revAmtUnderPayment" runat="server" ControlToValidate="txtAmtUnderPayment" Display="Dynamic"></asp:RegularExpressionValidator>
											</TD>
										</tr>
										<tr>
											<td class='midcolora'><asp:label id="capMinDays_Premium" Runat="server"></asp:label><span class="mandatory">*</span></td>
											<td class='midcolora'>
												<asp:TextBox ID="txtMinDays_Premium" Runat="server" MaxLength="2" size="8"></asp:TextBox><br>
												<asp:RequiredFieldValidator ID="rfvMinDays_Premium" Runat="server" ControlToValidate="txtMinDays_Premium" Display="Dynamic"
													ErrorMessage="Please enter premiuim days"></asp:RequiredFieldValidator>
												<asp:RegularExpressionValidator id="revMinDays_Premium" runat="server" ControlToValidate="txtMinDays_Premium" Display="Dynamic"></asp:RegularExpressionValidator>
											</td>
											<td class='midcolora'><asp:label id="capMinDays_Cancel" Runat="server"></asp:label><span class="mandatory">*</span></td>
											<td class='midcolora'><asp:TextBox ID="txtMinDays_Cancel" Runat="server" MaxLength="2" size="8"></asp:TextBox><br>
												<asp:RequiredFieldValidator ID="rfvMinDays_Cancel" Runat="server" ControlToValidate="txtMinDays_Cancel" Display="Dynamic"
													ErrorMessage="Please enter premiuim days"></asp:RequiredFieldValidator>
												<asp:RegularExpressionValidator id="revMinDays_Cancel" runat="server" ControlToValidate="txtMinDays_Cancel" Display="Dynamic"
													ErrorMessage="Please enter numeric data"></asp:RegularExpressionValidator>
											</td>
										</tr>
										<tr>
										</tr>
										<tr>
											<td class='midcolora'><asp:label id="capPostPhone" Runat="server"></asp:label><span class="mandatory">*</span></td>
											<td class='midcolora'><asp:TextBox ID="txtPostPhone" Runat="server" MaxLength="2" size="8"></asp:TextBox><br>
												<asp:RequiredFieldValidator ID="rfvPostPhone" Runat="server" ControlToValidate="txtPostPhone" Display="Dynamic"></asp:RequiredFieldValidator>
												<asp:RegularExpressionValidator id="revPostPhone" runat="server" ControlToValidate="txtPostPhone" Display="Dynamic"></asp:RegularExpressionValidator>
											</td>
											<td class='midcolora'><asp:label id="capPostCancel" Runat="server"></asp:label><span class="mandatory">*</span></td>
											<td class='midcolora'><asp:TextBox ID="txtPostCancel" Runat="server" MaxLength="2" size="8"></asp:TextBox><br>
												<asp:RequiredFieldValidator ID="rfvPostCancel" Runat="server" ControlToValidate="txtPostCancel" Display="Dynamic"></asp:RequiredFieldValidator>
												<asp:RegularExpressionValidator id="revPostCancel" runat="server" ControlToValidate="txtPostCancel" Display="Dynamic"></asp:RegularExpressionValidator>
											</td>
										</tr>
										
									</tbody>
									<tr >
										    <td class='headereffectSystemParams' colspan='4'><asp:label id="capPrintParam" Text="Printing Parameters" Runat="server"></asp:label></td>
										    
									    </tr>
									    <tr>
									         <td class='midcolora' colspan='4'>
										        <asp:label id="capNOTIFY_RECVE_INSURED" Runat="server"></asp:label></br>
										        <asp:DropDownList ID="cmbNOTIFY_RECVE_INSURED" runat="server" ></asp:DropDownList>
										    </td>
										      
										    
									    </tr>
									<TR>
										<TD class='midcolora' ColSpan='4'></TD>
									</TR>
									<tr>
										<td class='midcolora' colspan='1'>
											<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" causesValidation="false"></cmsb:cmsbutton>
										</td>
										<td class='midcolorr' colspan='3' align="right">
											<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
										</td>
									<tr>
										<td class="iframsHeightMedium"></td>
									</tr>
					</TR>
				</TABLE>
				</TD> </TR> </TABLE> <INPUT id="hidOldData" runat="server" type="hidden" NAME="hidOldData">
			</FORM>
		</div>
	</BODY>
</HTML>

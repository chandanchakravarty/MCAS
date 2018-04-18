<%@ Page language="c#" Codebehind="AddClaimsAdjusterAuthority.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Claims.AddClaimsAdjusterAuthority" validateRequest=false  %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>MNT_AGENCY_LIST</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>		
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript">
		
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";

			function AddData()
			{
				
				DisableValidators();		
				var CurrentDate = new Date();	
				document.getElementById('hidADJUSTER_AUTHORITY_ID').value = 'NEW';
				document.getElementById('cmbLOB_ID').selectedIndex = -1;
				document.getElementById('cmbLIMIT_ID').selectedIndex = -1;
				document.getElementById('txtPAYMENT_LIMIT').value = '';
				document.getElementById('txtNOTIFY_AMOUNT').value = '';
				document.getElementById('txtRESERVE_LIMIT').value = '';
				//document.getElementById('txtEFFECTIVE_DATE').value = CurrentDate.getMonth()+1 + '/' + CurrentDate.getDate() + '/' + CurrentDate.getFullYear();
				SetFocus();
				ChangeColor();
			}
			function SetFocus()
			{
					//document.getElementById("cmbLIMIT_ID").style.display="inline";
					//document.getElementById("cmbLIMIT_ID").focus();					
					document.getElementById("cmbLOB_ID").style.display= "inline";
					//document.getElementById("cmbLOB_ID").focus();					
				
			}
			
			function populateXML()
			{
				if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value=="1")
				{
					var tempXML;
					if(document.getElementById("hidOldData").value!="")
					{
						tempXML=document.getElementById("hidOldData").value;						 						
						populateFormData(tempXML,CLM_ADJUSTER_AUTHORITY);
						document.getElementById('txtPAYMENT_LIMIT').value = formatBaseCurrencyAmount(document.getElementById('txtPAYMENT_LIMIT').value);
						document.getElementById('txtRESERVE_LIMIT').value = formatBaseCurrencyAmount(document.getElementById('txtRESERVE_LIMIT').value);
						
						document.getElementById('cmbLOB_ID').disabled = true;	
						document.getElementById("cmbLIMIT_ID").disabled = true;
						
						document.getElementById('trButtonRow').style.display="none";						
						document.getElementById('trActivateDeactivate').style.display="inline";						
						SetComboValueForConcatenatedString("cmbLIMIT_ID",document.getElementById("hidLIMIT_ID").value,'^',0)
						//Set the value of hidFormSaved as in the case of delete to prevent a pop-up message asking 
						//for save to come. When no control is being displayed on the page, there is no use of showing
						//save message pop-up either.
						document.getElementById("txtEFFECTIVE_DATE").value = document.getElementById("hidEffectiveDate").value;

						document.getElementById('txtNOTIFY_AMOUNT').value = formatBaseCurrencyAmount(document.getElementById('hidNotifyAmount').value);
						
						document.getElementById("txtEFFECTIVE_DATE").readOnly = true;
						document.getElementById("hidFormSaved").value = "5";
					}	
					else
					{
						AddData();						
					}
					
				}				
				return false;
			}
			function GetLimitData(combo)
			{
				//Structure of the encoded string is as follows : 
				//LIMIT_ID^PAYMENT_LIMIT^RESERVE_LIMIT AS LIMIT_ID,
				//We will split the limit_id field to obtain the relavent information
				if(combo.selectedIndex==-1) return;
				encoded_string = new String(combo.options[combo.selectedIndex].value);
				if(encoded_string.length<1) return;
				array = encoded_string.split('^');
				//Traverse through the array and put the values in relavent fields
				document.getElementById("hidLIMIT_ID").value = array[0];
				document.getElementById("txtPAYMENT_LIMIT").value = formatBaseCurrencyAmount(array[1]);
				document.getElementById("txtRESERVE_LIMIT").value = formatBaseCurrencyAmount(array[2]);
				//document.getElementById("txtPAYMENT_LIMIT").value = strPaymentLimit.substr(0,strPaymentLimit.length-3);
				//document.getElementById("txtRESERVE_LIMIT").value = strReserveLimit.substr(0,strReserveLimit.length-3);
			}
			function ResetTheForm()
			{
				DisableValidators();								
				document.CLM_ADJUSTER_AUTHORITY.reset();				
				populateXML();
				SetFocus();				
				return false;
			}
			function SelectLOB_ID()//onfocus="SelectComboIndex('cmbLOB_ID')"
			{
				for(i=0;i<document.getElementById('cmbLOB_ID').options.length-1;i++)
				{
					if(document.getElementById('cmbLOB_ID').options[i].value == document.getElementById('hidLOB_ID').value)
					{
						document.getElementById('cmbLOB_ID').options[i].selected = true;
						return;
					}
				}
            }

            function FormatAmountForSum(num) {

                num = ReplaceAll(num, sBaseDecimalSep, '.');
                return num;
            }


            function validateAmount(objSource, objArgs) {

                var Limt = document.getElementById(objSource.controltovalidate).value;

                Limt = FormatAmountForSum(Limt);
                if (parseFloat(Limt) > 0) {
                    objArgs.IsValid = true;
                }
                else
                    objArgs.IsValid = false;
            }

            function validateLengthOfAmount(objSource, objArgs) {
                var amtlength = document.getElementById(objSource.controltovalidate).value;
                    if (parseFloat(amtlength) > parseFloat('9999999999999.99')) {
                    objArgs.IsValid = false;
                }
                else {
                    objArgs.IsValid = true;
                }
            }
 </script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();">
		<FORM id="CLM_ADJUSTER_AUTHORITY" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capLOB_ID" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOB_ID"  runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvLOB_ID" runat="server" 
                                            ControlToValidate="cmbLOB_ID" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora"><asp:label id="capADJUSTER_NAME" runat="server"></asp:label></TD>
									<TD class="midcolora">
										<asp:Label ID="lblADJUSTER_NAME" Runat="server" CssClass="LabelFont"></asp:Label>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capLIMIT_ID" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLIMIT_ID" onfocus="SelectComboIndex('cmbLIMIT_ID')" runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvLIMIT_ID" runat="server" 
                                            ControlToValidate="cmbLIMIT_ID" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" colspan='2'></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPAYMENT_LIMIT" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPAYMENT_LIMIT" ReadOnly="True" runat="server" size="40" maxlength="50" BackColor="LightGray"></asp:textbox></TD>
									<TD class="midcolora" width="18%"><asp:label id="capRESERVE_LIMIT" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtRESERVE_LIMIT" ReadOnly="True" runat="server" size="40" maxlength="50" BackColor="LightGray"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capEFFECTIVE_DATE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_DATE" runat="server" size="12" maxlength="10"></asp:textbox>
										<asp:hyperlink id="hlkEFFECTIVE_DATE" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgEFFECTIVE_DATE" runat="server" ImageUrl="../../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
										</asp:hyperlink><br>
										<asp:requiredfieldvalidator id="rfvEFFECTIVE_DATE" runat="server" ControlToValidate="txtEFFECTIVE_DATE" Display="Dynamic"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revEFFECTIVE_DATE" runat="server" Display="Dynamic"
											ControlToValidate="txtEFFECTIVE_DATE"></asp:regularexpressionvalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capNOTIFY_AMOUNT" runat="server">Notification</asp:label><span class="mandatory" id="spnamount">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtNOTIFY_AMOUNT" runat="server" CssClass="INPUTCURRENCY" maxlength="16" size="20"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvNOTIFY_AMOUNT" runat="server" Display="Dynamic"
											ControlToValidate="txtNOTIFY_AMOUNT"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revNOTIFY_AMOUNT" Display="Dynamic" ControlToValidate="txtNOTIFY_AMOUNT" Runat="server">
											</asp:regularexpressionvalidator>
											<asp:CustomValidator ID="csvNOTIFY_AMOUNT" Display="Dynamic" runat="server" ControlToValidate="txtNOTIFY_AMOUNT" ErrorMessage="" ClientValidationFunction ="validateAmount"></asp:CustomValidator>
                                            <asp:CustomValidator ID="csvNOTIFY_AMOUNT_MaxAmt" Display="Dynamic" runat="server" ControlToValidate="txtNOTIFY_AMOUNT" ErrorMessage="" ClientValidationFunction ="validateLengthOfAmount"></asp:CustomValidator>
											</TD>
								</tr>
								<tr id="trActivateDeactivate" style="DISPLAY:none">
									<td colspan="2" class="midcolora">
										<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate" CausesValidation="False"></cmsb:cmsbutton>
									</td>
									<td colspan="2" class="midcolora">
									</td>
								</tr>
								<tr id="trButtonRow">
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
									</td>
									<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidLOB_ID" type="hidden" name="hidLOB_ID" runat="server">
			<INPUT id="hidADJUSTER_AUTHORITY_ID" type="hidden" value="0" name="hidADJUSTER_AUTHORITY_ID"
				runat="server"> <INPUT id="hidADJUSTER_ID" type="hidden" value="0" name="hidADJUSTER_ID" runat="server">
			<INPUT id="hidLIMIT_ID" type="hidden" value="0" name="hidLIMIT_ID" runat="server">
			<INPUT id="hidEffectiveDate" type="hidden" value="" name="hidEffectiveDate" runat="server">
			<INPUT id="hidNotifyAmount" type="hidden" value="" name="hidNotifyAmount" runat="server">
			
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidADJUSTER_AUTHORITY_ID').value,true);			
		</script>
	</BODY>
</HTML>

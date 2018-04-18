<%@ Page language="c#" Codebehind="PolicyAddOtherStructures.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.aspx.Homeowners.PolicyAddOtherStructures" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>AddOtherStructures</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/DwellingInfo.js"></script>
		<script language="javascript">
		function ChkTextAreaLength(source, arguments)
		{
			var txtArea = arguments.Value;
			if(txtArea.length > 250 ) 
			{
				arguments.IsValid = false;
				return;   
			}
		}
		function AddData()
		{
			
			document.getElementById('hidOTHER_STRUCTURE_ID').value	=	'New';
			//document.getElementById('cmbPREMISES_LOCATION').focus(); //Commented by Charles on 7-Oct-09 for Itrack 6525
			
			//In rental page On pre will be selected by default
			if (document.getElementById('CalledFROM') && document.getElementById('CalledFROM').value=="Rental")
			{	
				document.getElementById('cmbPREMISES_LOCATION').options.selectedIndex = 2;			
			}
			else
			{
				document.getElementById('cmbPREMISES_LOCATION').options.selectedIndex = -1;			
			}
			
			document.getElementById('txtPREMISES_DESCRIPTION').value  = '';
			document.getElementById('txtPREMISES_USE').value  = '';
			document.getElementById('cmbPREMISES_CONDITION').options.selectedIndex = -1;
			document.getElementById('cmbPICTURE_ATTACHED').options.selectedIndex = -1;
			document.getElementById('cmbCOVERAGE_BASIS').options.selectedIndex = -1;
			document.getElementById('cmbSATELLITE_EQUIPMENT').options.selectedIndex = -1;
			document.getElementById('txtLOCATION_ADDRESS').value  = '';
			document.getElementById('txtLOCATION_CITY').value  = '';
			document.getElementById('cmbLOCATION_STATE').options.selectedIndex = -1;
			document.getElementById('txtLOCATION_ZIP').value  = '';
			document.getElementById('txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED').value  = '';
			document.getElementById('txtINSURING_VALUE').value  = '';
			document.getElementById('txtINSURING_VALUE_OFF_PREMISES').value  = '';
			
			 //Added by Charles on 27-Nov-09 for Itrack 6681
			if(document.getElementById('cmbSOLID_FUEL_DEVICE'))
				document.getElementById('cmbSOLID_FUEL_DEVICE').options.selectedIndex = -1;
			
			if (document.getElementById('btnActivateDeactivate'))
				document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
				
			if (document.getElementById('btnDelete'))
				document.getElementById('btnDelete').setAttribute('disabled',true);
					
			ChangeColor();
			DisableValidators();
		}
		function populateXML() {
			if(document.getElementById('hidFormSaved').value == '0')
			{
				var tempXML = document.getElementById("hidOldData").value;
				if(tempXML != "")
				{
					
					//Enabling the activate deactivate button
					if (document.getElementById('btnActivateDeactivate'))
						document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
					//Enabling the delete button
					if (document.getElementById('btnDelete'))
						document.getElementById('btnDelete').setAttribute('disabled',false); 
						
					populateFormData(tempXML,APP_OTHER_STRUCTURE_DWELLING);
					
					document.getElementById('txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED').value=formatCurrency(document.getElementById('txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED').value);
					document.getElementById('txtINSURING_VALUE').value=formatCurrency(document.getElementById('txtINSURING_VALUE').value);
					document.getElementById('txtINSURING_VALUE_OFF_PREMISES').value=formatCurrency(document.getElementById('txtINSURING_VALUE_OFF_PREMISES').value);
					document.getElementById('txtCOVERAGE_AMOUNT').value=formatCurrency(document.getElementById('txtCOVERAGE_AMOUNT').value);					
				}
				else
				{
					AddData();
				}
			}			
			ShowPremises(document.getElementById("cmbPREMISES_LOCATION"));
			return false;
		}
		</script>
</HEAD>
	<BODY oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();">
		<FORM id="APP_OTHER_STRUCTURE_DWELLING" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr id="trError" runat="server">
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblError" runat="server" CssClass="errmsg"></asp:label></td>
				</tr>
				<tr>
					<td class="pageHeader" id="tdWorkflow" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server" ISTOP="FALSE"></webcontrol:workflow></td>
				</tr>
				<TR id="trBody" runat="server">
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
								<TD class="midcolora" width="18%"><asp:label id="capPREMISES_LOCATION" runat="server">Location</asp:label><span class="mandatory">*</span></TD>
								<td class="midcolora" width="32%"><asp:dropdownlist id="cmbPREMISES_LOCATION" onfocus="SelectComboIndex('cmbPREMISES_LOCATION')" runat="server"
										onchange="ShowPremises(this);"></asp:dropdownlist><br><asp:label id="lblRentedDwellingPolicies" runat="server" cssclass="mandatory" style="FONT-WEIGHT: normal"></asp:label>
									<asp:requiredfieldvalidator id="rfvPREMISES_LOCATION" runat="server" ErrorMessage="Please select Location of Premises"
										ControlToValidate="cmbPREMISES_LOCATION"></asp:requiredfieldvalidator></td>
								<TD class="midcolora" width="18%"><asp:label id="capPREMISES_DESCRIPTION" runat="server">Description</asp:label><span class="mandatory" id="spnPREMISES_DESCRIPTION">*</span></TD><!--Span added by Charles on 15-Sep-09 for Itrack 6405 -->
								<td class="midcolora" width="32%"><asp:textbox id="txtPREMISES_DESCRIPTION" MaxLength="250" Columns="35" Rows="4" Runat="server" onKeyPress="MaxLength(this,250)"
										TextMode="MultiLine"></asp:textbox><br>
									<asp:RequiredFieldValidator id="rfvPREMISES_DESCRIPTION" ControlToValidate="txtPREMISES_DESCRIPTION" runat="server" Display="Dynamic" ErrorMessage="Please enter Description of Premises"></asp:RequiredFieldValidator> <!--RequiredFieldValidator added by Charles on 15-Sep-09 for Itrack 6405 -->
									<asp:customvalidator id="csvPREMISES_DESCRIPTION" ErrorMessage="The number of characters in Description of Premises field should not exceed 250."
										ControlToValidate="txtPREMISES_DESCRIPTION" Runat="server" ClientValidationFunction="ChkTextAreaLength"
										Display="Dynamic"></asp:customvalidator></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capPREMISES_USE" runat="server">Use</asp:label></TD>
								<td class="midcolora" width="32%"><asp:textbox id="txtPREMISES_USE" MaxLength="30" Runat="server"></asp:textbox></td>
								<TD class="midcolora" width="18%"><asp:label id="capPREMISES_CONDITION" runat="server">Condition</asp:label></TD>
								<td class="midcolora" width="32%"><asp:dropdownlist id="cmbPREMISES_CONDITION" onfocus="SelectComboIndex('cmbPREMISES_CONDITION')" runat="server"></asp:dropdownlist></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capPICTURE_ATTACHED" runat="server">Attached</asp:label><span class="mandatory" id="spnPICTURE_ATTACHED" >*</span></TD><!--Span added by Charles on 15-Sep-09 for Itrack 6405 -->
								<td class="midcolora" width="32%"><asp:dropdownlist id="cmbPICTURE_ATTACHED" onfocus="SelectComboIndex('cmbPICTURE_ATTACHED')" runat="server"></asp:dropdownlist>
								<br><asp:RequiredFieldValidator id="rfvPICTURE_ATTACHED" ControlToValidate="cmbPICTURE_ATTACHED" runat="server" Display="Dynamic" ErrorMessage="Please select Picture Attached"></asp:RequiredFieldValidator><!--RequiredFieldValidator added by Charles on 15-Sep-09 for Itrack 6405 -->
								</td>
								<TD class='midcolora' width='18%'><asp:Label id="capCOVERAGE_AMOUNT" runat="server"></asp:Label></TD>
								<td class="midcolora" width="32%"><asp:TextBox ID="txtCOVERAGE_AMOUNT"  cssclass="INPUTCURRENCY" onblur="this.value=formatCurrency(this.value);" MaxLength="6" Runat="server"></asp:TextBox>
									<br>
									<asp:regularexpressionvalidator id="revCOVERAGE_AMOUNT" Display="Dynamic" ControlToValidate="txtCOVERAGE_AMOUNT" Runat="server"></asp:regularexpressionvalidator>
									<asp:RangeValidator Enabled="False" ID="rngCOVERAGE_AMOUNT" ControlToValidate="txtCOVERAGE_AMOUNT" Runat="server" Display="Dynamic" Type="Currency" MinimumValue="1" MaximumValue="999999"></asp:RangeValidator>
								</td>
							</tr>
							<tr id="tdCoverageBasis">
								<TD class="midcolora" width="18%"><asp:label id="capCOVERAGE_BASIS" runat="server">Basis</asp:label><span class="mandatory" id="spnCOVERAGE_BASIS">*</span></TD><!--Span added by Charles on 15-Sep-09 for Itrack 6405 -->
								<td class="midcolora" width="32%"><asp:dropdownlist id="cmbCOVERAGE_BASIS" onfocus="SelectComboIndex('cmbCOVERAGE_BASIS')" runat="server"
										onchange="ShowInsuringValue(this);"></asp:dropdownlist>
								<br>
								<asp:label id="lblOFF_EXCL_COV_BASIS" runat="server" cssclass="mandatory" style="FONT-WEIGHT: normal; display:none">The option Off Premises / Excluded is not available</asp:label><!-- Added by Charles on 3-Dec-09 for Itrack 6405 -->
								<asp:RequiredFieldValidator id="rfvCOVERAGE_BASIS" ControlToValidate="cmbCOVERAGE_BASIS" runat="server" Display="Dynamic" ErrorMessage="Please select Coverage Basis"></asp:RequiredFieldValidator><!--RequiredFieldValidator added by Charles on 15-Sep-09 for Itrack 6405 -->
								</td>
								<td class="midcolora" colSpan="2"></td>
							</tr>
							<tr id="trInsuringValue">
								<TD class="midcolora" width="18%"><asp:label id="capINSURING_VALUE" runat="server">Value</asp:label><span class="mandatory" id="spnINSURING_VALUE" style="display:none">*</span></TD><!--Span added by Charles on 15-Sep-09 for Itrack 6405 -->
								<td class="midcolora" width="32%"><asp:textbox id="txtINSURING_VALUE" cssclass="INPUTCURRENCY" onblur="this.value=formatCurrency(this.value);"
										MaxLength="9" Runat="server"></asp:textbox><br>
									<asp:RequiredFieldValidator id="rfvINSURING_VALUE" ControlToValidate="txtINSURING_VALUE" runat="server" Display="Dynamic" ErrorMessage="Please enter Insuring Value (On Premises)"></asp:RequiredFieldValidator><!--RequiredFieldValidator added by Charles on 15-Sep-09 for Itrack 6405 -->
									<asp:regularexpressionvalidator id="revINSURING_VALUE" Display="Dynamic" ControlToValidate="txtINSURING_VALUE" Runat="server"></asp:regularexpressionvalidator>
									<asp:comparevalidator Enabled="False" id="cmpINSURING_VALUE" ErrorMessage="Please enter a number greater than 0" ControlToValidate="txtINSURING_VALUE"
										Runat="server" Display="Dynamic" ValueToCompare="0" Operator="GreaterThan" Type="Currency"></asp:comparevalidator><!-- Modified by Charles on 12-Oct-09 for Itrack 6405 --></td>
								<td class="midcolora" colSpan="2"></td>
							</tr>
							<tr id="trAdditionalAmountofInsuranceDesired">
								<TD class="midcolora" width="18%"><asp:label id="capADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED" runat="server">Amount</asp:label><span class="mandatory" id="spnADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED" style="display:none">*</span></TD><!--Span added by Charles on 12-Oct-09 for Itrack 6405 -->
								<td class="midcolora" width="32%"><asp:textbox id="txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED" cssclass="INPUTCURRENCY" onblur="this.value=formatCurrency(this.value);" MaxLength="9"
										Runat="server"></asp:textbox><br>
									<asp:RequiredFieldValidator id="rfvADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED" Enabled="False" ControlToValidate="txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED" runat="server" Display="Dynamic" ErrorMessage="Please enter Additional Amount of Insurance Desired (Coverage B)"></asp:RequiredFieldValidator><!--RequiredFieldValidator added by Charles on 12-Oct-09 for Itrack 6405 -->
									<asp:regularexpressionvalidator id="revADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED" Display="Dynamic" ControlToValidate="txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED" Runat="server"></asp:regularexpressionvalidator>
									<asp:comparevalidator Enabled="False" id="cmpADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED" ErrorMessage="Please enter numeric value"
										ControlToValidate="txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED" Runat="server" Display="Dynamic" ValueToCompare="0"
										Operator="GreaterThanEqual" Type="Currency"></asp:comparevalidator></td>
								<TD class="midcolora" width="18%"><asp:label id="capSATELLITE_EQUIPMENT" runat="server">Equipment</asp:label><span class="mandatory" id="spnSATELLITE_EQUIPMENT" style="display:none">*</span></TD> <!--Span added by Charles on 12-Oct-09 for Itrack 6405 -->
								<td class="midcolora" width="32%"><asp:dropdownlist id="cmbSATELLITE_EQUIPMENT" onfocus="SelectComboIndex('cmbSATELLITE_EQUIPMENT')" runat="server" onchange="EnableDisableMandPic();"></asp:dropdownlist><!-- onchange event added by Charles on 14-Oct-09 for Itrack 6405-->
								<br><asp:RequiredFieldValidator id="rfvSATELLITE_EQUIPMENT" Enabled="False" ControlToValidate="cmbSATELLITE_EQUIPMENT" runat="server" Display="Dynamic" ErrorMessage="Please select Is this Satellite Equipment"></asp:RequiredFieldValidator><!--RequiredFieldValidator added by Charles on 12-Oct-09 for Itrack 6405 -->
								</td>
							</tr>
							<!-- Added by Charles on 27-Nov-09 for Itrack 6681 -->
							<tr id="trSOLID_FUEL_DEVICE" runat="server" visible ="false">
								<td class='midcolora' width='18%'>
								<asp:Label id="capSOLID_FUEL_DEVICE" runat="server">Does this Structure have a Solid Fuel Device?</asp:Label><span class="mandatory" id="spnSOLID_FUEL_DEVICE">*</span>
								</td>
								<td class='midcolora' width='32%'>
								<asp:dropdownlist id="cmbSOLID_FUEL_DEVICE" onfocus="SelectComboIndex('cmbSOLID_FUEL_DEVICE')" runat="server"></asp:dropdownlist>
								<br><asp:RequiredFieldValidator id="rfvSOLID_FUEL_DEVICE" Enabled="True" ControlToValidate="cmbSOLID_FUEL_DEVICE" runat="server" Display="Dynamic" ErrorMessage="Please select Does this Structure have a Solid Fuel Device"></asp:RequiredFieldValidator>
								</td>
								<td class='midcolora' colspan="2"></td>								
							</tr>
							<!-- Added till here -->
							<!-- Added by Charles on 3-Dec-09 for Itrack 6405 -->
							<tr id="trAPPLY_ENDS" style="display:none">
								<td class='midcolora' width='18%'>
								<asp:Label id="capAPPLY_ENDS" runat="server">Apply Endorsement</asp:Label>
								</td>
								<td class='midcolora' width='32%'>
								<asp:dropdownlist id="cmbAPPLY_ENDS" onfocus="SelectComboIndex('cmbAPPLY_ENDS')" runat="server">
									<asp:ListItem Value=''></asp:ListItem>									
									<asp:ListItem Value='HO237'>HO-237 Exclusion of Specific Other Structures & Personal Property</asp:ListItem>
									<asp:ListItem Value='HO238'>HO-238 Exclusion of Other Structures & Personal Property</asp:ListItem>
								</asp:dropdownlist>
								</td>
								<td class='midcolora' colspan="2"></td>
							</tr>
							<!-- Added till here -->
							<tr id="trAddressHeader">
								<td class="midcolora" width="18%">Would you like to pull customer address?
								</td>
								<td class="midcolora" width="32%"><cmsb:cmsbutton class="clsButton" id="btnPullCustomerAddress" runat="server" Text="Pull Customer Address"></cmsb:cmsbutton></td>
								<TD class="midcolora" colSpan="2">&nbsp;</TD>
							</tr>
							<tr id="trAddressCity">
								<TD class="midcolora" width="18%"><asp:label id="capLOCATION_ADDRESS" runat="server">Address</asp:label><span class="mandatory" id="spnLOCATION_ADDRESS" style="display:none">*</span></TD><!--Span added by Charles on 15-Sep-09 for Itrack 6405 -->
								<TD class="midcolora" width="32%"><asp:textbox id="txtLOCATION_ADDRESS" runat="server" Width="225px" maxlength="100"></asp:textbox>
								<br><asp:RequiredFieldValidator id="rfvLOCATION_ADDRESS" ControlToValidate="txtLOCATION_ADDRESS" runat="server" Display="Dynamic" ErrorMessage="Please enter Address"></asp:RequiredFieldValidator><!--RequiredFieldValidator added by Charles on 15-Sep-09 for Itrack 6405 -->
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="capLOCATION_CITY" runat="server">City</asp:label><span class="mandatory" id="spnLOCATION_CITY" style="display:none">*</span></TD><!--Span added by Charles on 15-Sep-09 for Itrack 6405 -->
								<TD class="midcolora" width="32%"><asp:textbox id="txtLOCATION_CITY" runat="server" maxlength="30"></asp:textbox>
								<br><asp:RequiredFieldValidator id="rfvLOCATION_CITY" ControlToValidate="txtLOCATION_CITY" runat="server" Display="Dynamic" ErrorMessage="Please enter City"></asp:RequiredFieldValidator><!--RequiredFieldValidator added by Charles on 15-Sep-09 for Itrack 6405 -->
								</TD>
							</tr>
							<tr id="trStateZip">
								<TD class="midcolora" width="18%"><asp:label id="capLOCATION_STATE" runat="server">State</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOCATION_STATE" runat="server"></asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"><asp:label id="capLOCATION_ZIP" runat="server">Zip</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtLOCATION_ZIP" runat="server" maxlength="10" size="12"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revLOCATION_ZIP" ControlToValidate="txtLOCATION_ZIP" Runat="server" Display="Dynamic"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr id="trInsuringValueOffPremises">
								<td class="midcolora"><asp:label id="capINSURING_VALUE_OFF_PREMISES" runat="server">Insuring Value (Off Premises)</asp:label><span class="mandatory" id="spnINSURING_VALUE_OFF_PREMISES" style="display:none">*</span><!--Span added by Charles on 15-Sep-09 for Itrack 6405 --></td>
								<td class="midcolora" width="32%"><asp:textbox id="txtINSURING_VALUE_OFF_PREMISES" cssclass="INPUTCURRENCY" onblur="this.value=formatCurrency(this.value);" MaxLength="6" Runat="server"></asp:textbox><br>
									<asp:RequiredFieldValidator id="rfvINSURING_VALUE_OFF_PREMISES" ControlToValidate="txtINSURING_VALUE_OFF_PREMISES" runat="server" Display="Dynamic" ErrorMessage="Please enter Insuring Value (Off Premises)"></asp:RequiredFieldValidator><!--RequiredFieldValidator added by Charles on 15-Sep-09 for Itrack 6405 -->
									<asp:regularexpressionvalidator id="revINSURING_VALUE_OFF_PREMISES" Display="Dynamic" ControlToValidate="txtINSURING_VALUE_OFF_PREMISES" Runat="server"></asp:regularexpressionvalidator>
									<asp:comparevalidator Enabled="False" id="cmpINSURING_VALUE_OFF_PREMISES" ErrorMessage="Please enter numeric value" ControlToValidate="txtINSURING_VALUE_OFF_PREMISES"
										Runat="server" Display="Dynamic" ValueToCompare="0" Operator="GreaterThanEqual" Type="Currency"></asp:comparevalidator></td>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capLIABILITY_EXTENDED" runat="server"></asp:Label><span class="mandatory" id="spnLIABILITY_EXTENDED" style="display:none">*</span></TD><!--Span added by Charles on 15-Sep-09 for Itrack 6405 -->
								<td class="midcolora" width="32%"><asp:dropdownlist id="cmbLIABILITY_EXTENDED" onfocus="SelectComboIndex('cmbLIABILITY_EXTENDED')" runat="server"></asp:dropdownlist>
								<br><asp:RequiredFieldValidator id="rfvLIABILITY_EXTENDED" ControlToValidate="cmbLIABILITY_EXTENDED" runat="server" Display="Dynamic" ErrorMessage="Please select If off premises is liability extended?"></asp:RequiredFieldValidator><!--RequiredFieldValidator added by Charles on 15-Sep-09 for Itrack 6405 -->
								</td>
							</tr>
						
								<tr>
								<td class='midcolora' colspan='2'>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset' CausesValidation=false></cmsb:cmsbutton>
									<cmsb:cmsbutton  class="clsButton" id='btnDelete' runat="server" Text='Delete'></cmsb:cmsbutton>
									<cmsb:cmsbutton  class="clsButton" id="btnActivateDeactivate" runat="server" text="Activate/Deactivate"></cmsb:cmsbutton>
									
								</td>
								<td class='midcolorr' colspan="2">
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
								</td>
							</tr>
							
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved"			type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE"			type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData"				type="hidden" name="hidOldData" runat="server"> 
			<INPUT id="hidOTHER_STRUCTURE_ID"	type="hidden" value="0" name="hidOTHER_STRUCTURE_ID" runat="server"> 
			<INPUT id="hidCustomerAddress"		type="hidden" name="hidCustomerAddress" runat="server">
			<INPUT id="hidCustomerCity"			type="hidden" name="hidCustomerCity" runat="server">
			<INPUT id="hidCustomerState"		type="hidden" name="hidCustomerState" runat="server">
			<INPUT id="hidCustomerZip"			type="hidden" name="hidCustomerZip" runat="server">
			<INPUT id="CalledFROM"				type="hidden" name="CalledFROM" runat="server">			
		</FORM>
		<script>
		RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidOTHER_STRUCTURE_ID').value, false);
		</script>
	</BODY>
</HTML>

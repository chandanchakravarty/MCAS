<%@ Page language="c#" Codebehind="PolicyAddWatercraftGenInfo.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Watercrafts.PolicyAddWatercraftGenInfo" ValidateRequest="false"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="~/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Policy WaterCraft UnderWriting Questions</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		var ShowSaveMsgAlways=true;
		var BoatListWindow;
		
		function setBoatList(strBoatList)
		{
			document.getElementById('txtMULTI_POLICY_DISC_APPLIED_PP_DESC').value=strBoatList;
			ChangeColor();
			DisableValidators();
		}
		
		function OpenLookupCheck()
		{
			var ExistingPolNumList = document.getElementById('txtMULTI_POLICY_DISC_APPLIED_PP_DESC').value;
			
			if (BoatListWindow != null)
				BoatListWindow.close();
				
			BoatListWindow = window.open ("../../../application/Aspx/LookUPCheck.aspx?ExistingPolNumList=" + ExistingPolNumList,"BoatListWindow","resizable=no,scrollbars=yes,width=620,height=400,left=150,top=150");
		}
				
		function HandlePopupImage()
		{
			if (document.getElementById("cmbMULTI_POLICY_DISC_APPLIED") && document.getElementById("cmbMULTI_POLICY_DISC_APPLIED").value == "1")	
				document.getElementById("spnImgSelect").style.display="inline";
			else
				document.getElementById("spnImgSelect").style.display="none";
		}
				
		function EnableDisableDesc(cmbCombo,txtDesc,lblNA)
		{	
			
			if (cmbCombo.selectedIndex > -1)
			{	
				
				//Checking value only if item is selected
				if (cmbCombo.options[cmbCombo.selectedIndex].text == "Yes")
				{
					//Disabling the description field, if No is selected
					txtDesc.style.display = "inline";
					lblNA.style.display = "none";
					
					//Enabling the validators
					if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null)
					{
						document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled",true);
						
						if (document.getElementById("rfv" + txtDesc.id.substring(3)).isvalid == false)
							document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "inline";
					}
					
					//making the * sign visible					
					if (document.getElementById("spn" + txtDesc.id.substring(3)) != null)
					{
						document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "inline";
					}
										
				}
				else
				{
				
					//Enabling the description field, if yes is selected
					txtDesc.style.display = "none";
				
					lblNA.style.display = "inline";
					lblNA.innerHTML="NA";
					
					//Disabling the validators					
					if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null)
					{
						document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled",false);
						document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "none";
					}
					
					//making the * sign invisible					
					if (document.getElementById("spn" + txtDesc.id.substring(3)) != null)
					{
						document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "none";
					}
				}
			}
			else
			{
				//Disabling the description field, if No is selected
				txtDesc.style.display = "none";
				lblNA.style.display = "inline";
				lblNA.innerHTML="NA";
				
				//Disabling the validators					
				if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null)
				{
					document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled",false);
					document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "none";
				}
				
				//making the * sign invisible					
				if (document.getElementById("spn" + txtDesc.id.substring(3)) != null)
				{
					document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "none";
				}
			}
			ChangeColor();
		}
			function AddData()
			{
				ChangeColor();
				DisableValidators();
				document.getElementById('hidRowId').value	=	'New';
				//document.getElementById('cmbHAS_CURR_ADD_THREE_YEARS').focus();
				//document.getElementById('txtHAS_CURR_ADD_THREE_YEARS_DESC').value  = '';
				document.getElementById('cmbPHY_MENTL_CHALLENGED').focus();
				document.getElementById('txtPHY_MENTL_CHALLENGED_DESC').value  = '';
				document.getElementById('txtDRIVER_SUS_REVOKED_DESC').value  = '';
				document.getElementById('txtIS_CONVICTED_ACCIDENT_DESC').value  = '';
				//document.getElementById('txtOTHER_POLICY_NUMBER_LIST').value  = '';
				document.getElementById('txtANY_LOSS_THREE_YEARS_DESC').value  = '';
				document.getElementById('txtCOVERAGE_DECLINED_DESC').value  = '';
				//document.getElementById('txtCREDIT_DETAILS').value  = '';
				document.getElementById('txtIS_RENTED_OTHERS_DESC').value  = '';
				document.getElementById('txtIS_REGISTERED_OTHERS_DESC').value  = '';
				document.getElementById('txtPARTICIPATE_RACE_DESC').value  = '';
				document.getElementById('txtPARTICIPATE_RACE_DESC').value  = '';
				document.getElementById('txtCARRY_PASSENGER_FOR_CHARGE_DESC').value  = '';
				document.getElementById('txtPRIOR_INSURANCE_CARRIER_DESC').value ='';
				document.getElementById('txtIS_BOAT_COOWNED_DESC').value  = '';
				
				
				document.getElementById('cmbPHY_MENTL_CHALLENGED').options.selectedIndex = 1;
				document.getElementById('cmbDRIVER_SUS_REVOKED').options.selectedIndex = 1;
				document.getElementById('cmbIS_CONVICTED_ACCIDENT').options.selectedIndex = 1;
				//document.getElementById('cmbANY_OTH_INSU_COMP').options.selectedIndex = 1;
				document.getElementById('cmbANY_LOSS_THREE_YEARS').options.selectedIndex = 1;
				document.getElementById('cmbCOVERAGE_DECLINED').options.selectedIndex = 1;
				document.getElementById('cmbIS_RENTED_OTHERS').options.selectedIndex = 1;
				document.getElementById('cmbIS_REGISTERED_OTHERS').options.selectedIndex = 1;
				
				document.getElementById('cmbPARTICIPATE_RACE').options.selectedIndex = 1;
				document.getElementById('cmbCARRY_PASSENGER_FOR_CHARGE').options.selectedIndex = 1;
				document.getElementById('cmbIS_PRIOR_INSURANCE_CARRIER').options.selectedIndex = 1;
				document.getElementById('cmbIS_BOAT_COOWNED').options.selectedIndex = 1;
				/* if the customer there is an existing policy of Auto or Home (Based on LOB)
			    Multi Policy discount Field Will be By Default Yes
				*/
				document.getElementById('txtMULTI_POLICY_DISC_APPLIED_PP_DESC').value='';
				
				if(document.getElementById('hidPolicyCount').value == "MULTI")
				{
				 document.getElementById('cmbMULTI_POLICY_DISC_APPLIED').options.selectedIndex = 2;
				}
				else
				{
				 document.getElementById('cmbMULTI_POLICY_DISC_APPLIED').options.selectedIndex = 1;
				} 
				document.getElementById('cmbANY_BOAT_AMPHIBIOUS').options.selectedIndex = 1;
				document.getElementById('cmbANY_BOAT_RESIDENCE').options.selectedIndex = 1;
				document.getElementById('cmbIS_BOAT_USED_IN_ANY_WATER').options.selectedIndex = 1;
				document.getElementById('txtMULTI_POLICY_DISC_APPLIED_PP_DESC').value = document.getElementById('hidActivePolicyList').value;				
				EnableDisablePolicyNumberDesc();
				
			}
			
			//If HM policy are existing for customer than text box will be NON-editable(read-only). 
			//In case no policies are existing then text box will be editable.
			function EnableDisablePolicyNumberDesc()
			{
				if(document.getElementById('hidPolicyCount').value == "MULTI")
				{
					document.getElementById('txtMULTI_POLICY_DISC_APPLIED_PP_DESC').readOnly=true;
				}
				else
				{
					if(document.getElementById('txtMULTI_POLICY_DISC_APPLIED_PP_DESC').value=='')
						document.getElementById('txtMULTI_POLICY_DISC_APPLIED_PP_DESC').value = 'No Policy existing';
					document.getElementById('txtMULTI_POLICY_DISC_APPLIED_PP_DESC').readOnly=false;
				}
			}
			
			function populateXML()
			{
				document.getElementById('cmbPHY_MENTL_CHALLENGED').focus();
				var tempXML;
				tempXML=document.getElementById('hidOldData').value;
		
				if(document.getElementById('hidFormSaved').value == '0')
				{
				
					if(tempXML!="" && tempXML!="0")	
					{		
						populateFormData(tempXML,APP_WATERCRAFT_GEN_INFO);		
					}
					else															
					{
						AddData();
					}
				}
				
					//EnableDisableDesc(document.getElementById('cmbHAS_CURR_ADD_THREE_YEARS'),document.getElementById('txtHAS_CURR_ADD_THREE_YEARS_DESC'),document.getElementById('lblHAS_CURR_ADD_THREE_YEARS_DESC'));
					EnableDisableDesc(document.getElementById('cmbPHY_MENTL_CHALLENGED'),document.getElementById('txtPHY_MENTL_CHALLENGED_DESC'),document.getElementById('lblPHY_MENTL_CHALLENGED_DESC'));
					EnableDisableDesc(document.getElementById('cmbDRIVER_SUS_REVOKED'),document.getElementById('txtDRIVER_SUS_REVOKED_DESC'),document.getElementById('lblDRIVER_SUS_REVOKED_DESC'));
					EnableDisableDesc(document.getElementById('cmbIS_CONVICTED_ACCIDENT'),document.getElementById('txtIS_CONVICTED_ACCIDENT_DESC'),document.getElementById('lblIS_CONVICTED_ACCIDENT_DESC'));
					//EnableDisableDesc(document.getElementById('cmbANY_OTH_INSU_COMP'),document.getElementById('txtOTHER_POLICY_NUMBER_LIST'),document.getElementById('lblOTHER_POLICY_NUMBER_LIST'));
					EnableDisableDesc(document.getElementById('cmbANY_LOSS_THREE_YEARS'),document.getElementById('txtANY_LOSS_THREE_YEARS_DESC'),document.getElementById('lblANY_LOSS_THREE_YEARS_DESC'));
					EnableDisableDesc(document.getElementById('cmbCOVERAGE_DECLINED'),document.getElementById('txtCOVERAGE_DECLINED_DESC'),document.getElementById('lblCOVERAGE_DECLINED_DESC'));			
					//EnableDisableDesc(document.getElementById('cmbIS_CREDIT'),document.getElementById('txtCREDIT_DETAILS'),document.getElementById('lblCREDIT_DETAILS'));
					EnableDisableDesc(document.getElementById('cmbIS_RENTED_OTHERS'),document.getElementById('txtIS_RENTED_OTHERS_DESC'),document.getElementById('lblIS_RENTED_OTHERS_DESC'));
					EnableDisableDesc(document.getElementById('cmbIS_REGISTERED_OTHERS'),document.getElementById('txtIS_REGISTERED_OTHERS_DESC'),document.getElementById('lblIS_REGISTERED_OTHERS_DESC'));
					EnableDisableDesc(document.getElementById('cmbPARTICIPATE_RACE'),document.getElementById('txtPARTICIPATE_RACE_DESC'),document.getElementById('lblPARTICIPATE_RACE_DESC'));				
					EnableDisableDesc(document.getElementById('cmbCARRY_PASSENGER_FOR_CHARGE'),document.getElementById('txtCARRY_PASSENGER_FOR_CHARGE_DESC'),document.getElementById('lblCARRY_PASSENGER_FOR_CHARGE_DESC'));	
					EnableDisableDesc(document.getElementById('cmbIS_PRIOR_INSURANCE_CARRIER'),document.getElementById('txtPRIOR_INSURANCE_CARRIER_DESC'),document.getElementById('lblPRIOR_INSURANCE_CARRIER_DESC'));	
					EnableDisableDesc(document.getElementById('cmbIS_BOAT_COOWNED'),document.getElementById('txtIS_BOAT_COOWNED_DESC'),document.getElementById('lblIS_BOAT_COOWNED_DESC'));	
					EnableDisableDesc(document.getElementById('cmbMULTI_POLICY_DISC_APPLIED'),document.getElementById('txtMULTI_POLICY_DISC_APPLIED_PP_DESC'),document.getElementById('lblMULTIPOLICY_DISC_DESC'));
					EnableDisableDesc(document.getElementById('cmbANY_BOAT_AMPHIBIOUS'),document.getElementById('txtANY_BOAT_AMPHIBIOUS_DESC'),document.getElementById('lblANY_BOAT_AMPHIBIOUS_DESC'));
					EnableDisableDesc(document.getElementById('cmbANY_BOAT_RESIDENCE'),document.getElementById('txtANY_BOAT_RESIDENCE_DESC'),document.getElementById('lblANY_BOAT_RESIDENCE_DESC'));
					EnableDisableDesc(document.getElementById('cmbIS_BOAT_USED_IN_ANY_WATER'),document.getElementById('txtIS_BOAT_USED_IN_ANY_WATER_DESC'),document.getElementById('lblIS_BOAT_USED_IN_ANY_WATER_DESC'));
					HandlePopupImage();					
					EnableDisablePolicyNumberDesc()
					return false;
			}					   
			
			function ResetForm1()
			{
				ResetForm('APP_WATERCRAFT_GEN_INFO');
				//EnableDisableDesc(document.getElementById('cmbHAS_CURR_ADD_THREE_YEARS'),document.getElementById('txtHAS_CURR_ADD_THREE_YEARS_DESC'),document.getElementById('lblHAS_CURR_ADD_THREE_YEARS_DESC'));
				EnableDisableDesc(document.getElementById('cmbPHY_MENTL_CHALLENGED'),document.getElementById('txtPHY_MENTL_CHALLENGED_DESC'),document.getElementById('lblPHY_MENTL_CHALLENGED_DESC'));
				EnableDisableDesc(document.getElementById('cmbDRIVER_SUS_REVOKED'),document.getElementById('txtDRIVER_SUS_REVOKED_DESC'),document.getElementById('lblDRIVER_SUS_REVOKED_DESC'));
				EnableDisableDesc(document.getElementById('cmbIS_CONVICTED_ACCIDENT'),document.getElementById('txtIS_CONVICTED_ACCIDENT_DESC'),document.getElementById('lblIS_CONVICTED_ACCIDENT_DESC'));
				//EnableDisableDesc(document.getElementById('cmbANY_OTH_INSU_COMP'),document.getElementById('txtOTHER_POLICY_NUMBER_LIST'),document.getElementById('lblOTHER_POLICY_NUMBER_LIST'));
				EnableDisableDesc(document.getElementById('cmbANY_LOSS_THREE_YEARS'),document.getElementById('txtANY_LOSS_THREE_YEARS_DESC'),document.getElementById('lblANY_LOSS_THREE_YEARS_DESC'));
				EnableDisableDesc(document.getElementById('cmbCOVERAGE_DECLINED'),document.getElementById('txtCOVERAGE_DECLINED_DESC'),document.getElementById('lblCOVERAGE_DECLINED_DESC'));			
				//EnableDisableDesc(document.getElementById('cmbIS_CREDIT'),document.getElementById('txtCREDIT_DETAILS'),document.getElementById('lblCREDIT_DETAILS'));
				EnableDisableDesc(document.getElementById('cmbIS_RENTED_OTHERS'),document.getElementById('txtIS_RENTED_OTHERS_DESC'),document.getElementById('lblIS_RENTED_OTHERS_DESC'));
				EnableDisableDesc(document.getElementById('cmbIS_REGISTERED_OTHERS'),document.getElementById('txtIS_REGISTERED_OTHERS_DESC'),document.getElementById('lblIS_REGISTERED_OTHERS_DESC'));				
				EnableDisableDesc(document.getElementById('cmbPARTICIPATE_RACE'),document.getElementById('txtPARTICIPATE_RACE_DESC'),document.getElementById('lblPARTICIPATE_RACE_DESC'));				
				EnableDisableDesc(document.getElementById('cmbCARRY_PASSENGER_FOR_CHARGE'),document.getElementById('txtCARRY_PASSENGER_FOR_CHARGE_DESC'),document.getElementById('lblCARRY_PASSENGER_FOR_CHARGE_DESC'));				
				EnableDisableDesc(document.getElementById('cmbIS_PRIOR_INSURANCE_CARRIER'),document.getElementById('txtPRIOR_INSURANCE_CARRIER_DESC'),document.getElementById('lblPRIOR_INSURANCE_CARRIER_DESC'));				
				EnableDisableDesc(document.getElementById('cmbIS_BOAT_COOWNED'),document.getElementById('txtIS_BOAT_COOWNED_DESC'),document.getElementById('lblIS_BOAT_COOWNED_DESC'));				
				EnableDisableDesc(document.getElementById('cmbMULTI_POLICY_DISC_APPLIED'),document.getElementById('txtMULTI_POLICY_DISC_APPLIED_PP_DESC'),document.getElementById('lblMULTIPOLICY_DISC_DESC'));
				EnableDisableDesc(document.getElementById('cmbANY_BOAT_AMPHIBIOUS'),document.getElementById('txtANY_BOAT_AMPHIBIOUS_DESC'),document.getElementById('lblANY_BOAT_AMPHIBIOUS_DESC'));
				EnableDisableDesc(document.getElementById('cmbANY_BOAT_RESIDENCE'),document.getElementById('txtANY_BOAT_RESIDENCE_DESC'),document.getElementById('lblANY_BOAT_RESIDENCE_DESC'));
				EnableDisableDesc(document.getElementById('cmbIS_BOAT_USED_IN_ANY_WATER'),document.getElementById('txtIS_BOAT_USED_IN_ANY_WATER_DESC'),document.getElementById('lblIS_BOAT_USED_IN_ANY_WATER_DESC'));
				return false;
			}
			
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="ApplyColor();populateXML();ChangeColor();">
		<div class="pageContent" id="bodyHeight">
			<FORM id="APP_WATERCRAFT_GEN_INFO" method="post" runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR id="trMessage" runat="server">
						<TD>
							<TABLE width="100%" align="center" border="0">
								<TBODY>
									<tr>
										<TD colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow></TD>
									</tr>
									<tr>
										<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
											mandatory</TD>
									</tr>
									<tr>
										<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
									</tr>
									<tr>
										<TD class="midcolora" width="25%"><asp:label id="capPHY_MENTL_CHALLENGED" runat="server">Mentl</asp:label><span class="mandatory" id="spnPHY_MENTL_CHALLENGED">*</span></TD>
										<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbPHY_MENTL_CHALLENGED" onfocus="SelectComboIndex('cmbPHY_MENTL_CHALLENGED')"
												runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtPHY_MENTL_CHALLENGED_DESC'),document.getElementById('lblPHY_MENTL_CHALLENGED_DESC'));"></asp:dropdownlist><BR>
											<asp:requiredfieldvalidator id="rfvPHY_MENTL_CHALLENGED" runat="server" ControlToValidate="cmbPHY_MENTL_CHALLENGED"
												Display="Dynamic"></asp:requiredfieldvalidator></TD>
										<td class="midcolora" width="25%"><asp:label id="capPHY_MENTL_CHALLENGED_DESC" runat="server"> Driver Impairment Description</asp:label><span class="mandatory" id="spnPHY_MENTL_CHALLENGED_DESC">*</span></td>
										<td class="midcolora" width="25%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtPHY_MENTL_CHALLENGED_DESC" runat="server"
												MaxLength="50" size="28"></asp:textbox><br>
											<asp:label id="lblPHY_MENTL_CHALLENGED_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><asp:requiredfieldvalidator id="rfvPHY_MENTL_CHALLENGED_DESC" runat="server" ControlToValidate="txtPHY_MENTL_CHALLENGED_DESC"
												Display="Dynamic"></asp:requiredfieldvalidator></td>
									</tr>
									<tr>
										<TD class="midcolora"><asp:label id="capDRIVER_SUS_REVOKED" runat="server">Sus</asp:label><span class="mandatory" id="spnDRIVER_SUS_REVOKED">*</span></TD>
										<TD class="midcolora"><asp:dropdownlist id="cmbDRIVER_SUS_REVOKED" onfocus="SelectComboIndex('cmbDRIVER_SUS_REVOKED')" runat="server"
												onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDRIVER_SUS_REVOKED_DESC'),document.getElementById('lblDRIVER_SUS_REVOKED_DESC'));"></asp:dropdownlist><BR>
											<asp:requiredfieldvalidator id="rfvDRIVER_SUS_REVOKED" runat="server" ControlToValidate="cmbDRIVER_SUS_REVOKED"
												Display="Dynamic"></asp:requiredfieldvalidator></TD>
										<td class="midcolora"><asp:label id="capDRIVER_SUS_REVOKED_DESC" runat="server"> Driver License Description</asp:label><span class="mandatory" id="spnDRIVER_SUS_REVOKED_DESC">*</span></td>
										<td class="midcolora"><asp:textbox onkeypress="MaxLength(this,50);" id="txtDRIVER_SUS_REVOKED_DESC" runat="server"
												MaxLength="50" size="28"></asp:textbox><br>
											<asp:label id="lblDRIVER_SUS_REVOKED_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><asp:requiredfieldvalidator id="rfvDRIVER_SUS_REVOKED_DESC" runat="server" ControlToValidate="txtDRIVER_SUS_REVOKED_DESC"
												Display="Dynamic"></asp:requiredfieldvalidator></td>
									</tr>
									<tr>
										<TD class="midcolora"><asp:label id="capIS_CONVICTED_ACCIDENT" runat="server">Convicted</asp:label><span class="mandatory" id="spnIS_CONVICTED_ACCIDENT">*</span></TD>
										<TD class="midcolora"><asp:dropdownlist id="cmbIS_CONVICTED_ACCIDENT" onfocus="SelectComboIndex('cmbIS_CONVICTED_ACCIDENT')"
												runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtIS_CONVICTED_ACCIDENT_DESC'),document.getElementById('lblIS_CONVICTED_ACCIDENT_DESC'));"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvIS_CONVICTED_ACCIDENT" runat="server" ControlToValidate="cmbIS_CONVICTED_ACCIDENT"
												Display="Dynamic"></asp:requiredfieldvalidator></TD>
										<td class="midcolora"><asp:label id="capIS_CONVICTED_ACCIDENT_DESC" runat="server"> Accident Description</asp:label><span class="mandatory" id="spnIS_CONVICTED_ACCIDENT_DESC">*</span></td>
										<td class="midcolora"><asp:textbox onkeypress="MaxLength(this,50);" id="txtIS_CONVICTED_ACCIDENT_DESC" runat="server"
												MaxLength="50" size="28"></asp:textbox><br>
											<asp:label id="lblIS_CONVICTED_ACCIDENT_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><asp:requiredfieldvalidator id="rfvIS_CONVICTED_ACCIDENT_DESC" runat="server" ControlToValidate="txtIS_CONVICTED_ACCIDENT_DESC"
												Display="Dynamic"></asp:requiredfieldvalidator></td>
									</tr>
									<tr>
										<TD class="midcolora"><asp:label id="capDRINK_DRUG_VOILATION" runat="server">Convicted</asp:label><span class="mandatory" id="spnIS_CONVICTED_ACCIDENT">*</span></TD>
										<TD class="midcolora"><asp:dropdownlist id="cmbDRINK_DRUG_VOILATION" onfocus="SelectComboIndex('cmbDRINK_DRUG_VOILATION')"
												runat="server"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvDRINK_DRUG_VOILATION" runat="server" ControlToValidate="cmbDRINK_DRUG_VOILATION"
												Display="Dynamic"></asp:requiredfieldvalidator></TD>
										<TD class="midcolora"><asp:label id="capMINOR_VIOLATION" runat="server">MINOR_VIOLATION</asp:label><span class="mandatory" id="spnIS_CONVICTED_ACCIDENT">*</span></TD>
										<TD class="midcolora"><asp:dropdownlist id="cmbMINOR_VIOLATION" onfocus="SelectComboIndex('cmbMINOR_VIOLATION')" runat="server"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvMINOR_VIOLATION" runat="server" ControlToValidate="cmbMINOR_VIOLATION" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									</tr>
									<tr>
										<TD class="midcolora"><asp:label id="capANY_LOSS_THREE_YEARS" runat="server">Loss</asp:label><span class="mandatory" id="spnANY_LOSS_THREE_YEARS">*</span></TD>
										<TD class="midcolora"><asp:dropdownlist id="cmbANY_LOSS_THREE_YEARS" onfocus="SelectComboIndex('cmbANY_LOSS_THREE_YEARS')"
												runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_LOSS_THREE_YEARS_DESC'),document.getElementById('lblANY_LOSS_THREE_YEARS_DESC'));"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvANY_LOSS_THREE_YEARS" runat="server" ControlToValidate="cmbANY_LOSS_THREE_YEARS"
												Display="Dynamic"></asp:requiredfieldvalidator></TD>
										<td class="midcolora"><asp:label id="capANY_LOSS_THREE_YEARS_DESC" runat="server"> Loss Description</asp:label><span class="mandatory" id="spnANY_LOSS_THREE_YEARS_DESC">*</span></td>
										<td class="midcolora"><asp:textbox onkeypress="MaxLength(this,50);" id="txtANY_LOSS_THREE_YEARS_DESC" runat="server"
												MaxLength="50" size="28"></asp:textbox><br>
											<asp:label id="lblANY_LOSS_THREE_YEARS_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><asp:requiredfieldvalidator id="rfvANY_LOSS_THREE_YEARS_DESC" runat="server" ControlToValidate="txtANY_LOSS_THREE_YEARS_DESC"
												Display="Dynamic"></asp:requiredfieldvalidator></td>
									</tr>
									<tr>
										<TD class="midcolora"><asp:label id="capCOVERAGE_DECLINED" runat="server">Declined</asp:label><span class="mandatory" id="spnIS_CONVICTED_ACCIDENT">*</span></TD>
										<TD class="midcolora"><asp:dropdownlist id="cmbCOVERAGE_DECLINED" onfocus="SelectComboIndex('cmbCOVERAGE_DECLINED')" runat="server"
												onchange="javascript:EnableDisableDesc(this,document.getElementById('txtCOVERAGE_DECLINED_DESC'),document.getElementById('lblCOVERAGE_DECLINED_DESC'));"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvCOVERAGE_DECLINED" runat="server" ControlToValidate="cmbCOVERAGE_DECLINED"
												Display="Dynamic"></asp:requiredfieldvalidator></TD>
										<td class="midcolora"><asp:label id="capCOVERAGE_DECLINED_DESC" runat="server"> Coverage Declined Description</asp:label><span class="mandatory" id="spnCOVERAGE_DECLINED_DESC">*</span></td>
										<td class="midcolora"><asp:textbox onkeypress="MaxLength(this,50);" id="txtCOVERAGE_DECLINED_DESC" runat="server" MaxLength="50"
												size="28"></asp:textbox><br>
											<asp:label id="lblCOVERAGE_DECLINED_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><asp:requiredfieldvalidator id="rfvCOVERAGE_DECLINED_DESC" runat="server" ControlToValidate="txtCOVERAGE_DECLINED_DESC"
												Display="Dynamic"></asp:requiredfieldvalidator></td>
									</tr>
									<!--<tr>
										<TD class="midcolora">--><%--<asp:label id="capDEGREE_CONVICTION" runat="server">Conviction</asp:label>--%><!--<span class="mandatory" id="spnDEGREE_CONVICTION">*</span></TD>
										<TD class="midcolora">--><%--<asp:dropdownlist id="cmbDEGREE_CONVICTION" onfocus="SelectComboIndex('cmbDEGREE_CONVICTION')" runat="server"
												onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDEGREE_CONVICTION_DESC'),document.getElementById('lblDEGREE_CONVICTION_DESC'));"></asp:dropdownlist><asp:requiredfieldvalidator id="rfvDEGREE_CONVICTION" runat="server" ControlToValidate="cmbDEGREE_CONVICTION"
												Display="Dynamic"></asp:requiredfieldvalidator>--%><!--</TD>
										<td class="midcolora">--><%--<asp:label id="capDEGREE_CONVICTION_DESC" runat="server"> Conviction Description</asp:label>--%><!--<span class="mandatory" id="spnDEGREE_CONVICTION_DESC">*</span></td>
										<td class="midcolora">--><%--<asp:textbox onkeypress="MaxLength(this,50);" id="txtDEGREE_CONVICTION_DESC" runat="server" MaxLength="50"
												size="28"></asp:textbox><asp:label id="lblDEGREE_CONVICTION_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><asp:requiredfieldvalidator id="rfvDEGREE_CONVICTION_DESC" runat="server" ControlToValidate="txtDEGREE_CONVICTION_DESC"
												Display="Dynamic"></asp:requiredfieldvalidator>--%><!--</td>
									</tr>-->
									<tr>
										<TD class="midcolora"><asp:label id="capIS_RENTED_OTHERS" runat="server">Rented</asp:label><span class="mandatory" id="spnIS_RENTED_OTHERS">*</span></TD>
										<TD class="midcolora"><asp:dropdownlist id="cmbIS_RENTED_OTHERS" onfocus="SelectComboIndex('cmbIS_RENTED_OTHERS')" runat="server"
												onchange="javascript:EnableDisableDesc(this,document.getElementById('txtIS_RENTED_OTHERS_DESC'),document.getElementById('lblIS_RENTED_OTHERS_DESC'));"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvIS_RENTED_OTHERS" runat="server" ControlToValidate="cmbIS_RENTED_OTHERS"
												Display="Dynamic"></asp:requiredfieldvalidator></TD>
										<td class="midcolora"><asp:label id="capIS_RENTED_OTHERS_DESC" runat="server"> Rented Description</asp:label><span class="mandatory" id="spnIS_RENTED_OTHERS_DESC">*</span></td>
										<td class="midcolora"><asp:textbox onkeypress="MaxLength(this,50);" id="txtIS_RENTED_OTHERS_DESC" runat="server" MaxLength="50"
												size="28"></asp:textbox><br>
											<asp:label id="lblIS_RENTED_OTHERS_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><asp:requiredfieldvalidator id="rfvIS_RENTED_OTHERS_DESC" runat="server" ControlToValidate="txtIS_RENTED_OTHERS_DESC"
												Display="Dynamic"></asp:requiredfieldvalidator></td>
						</TD>
					</TR>
					<tr>
						<TD class="midcolora" width="25%"><asp:label id="capIS_BOAT_COOWNED" runat="server">Is Boat Co-owned</asp:label><span class="mandatory" id="spnIS_BOAT_COOWNED">*</span></TD>
						<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbIS_BOAT_COOWNED" onfocus="SelectComboIndex('cmbIS_BOAT_COOWNED')" runat="server"
								onchange="javascript:EnableDisableDesc(this,document.getElementById('txtIS_BOAT_COOWNED_DESC'),document.getElementById('lblIS_BOAT_COOWNED_DESC'));"></asp:dropdownlist><br>
							<asp:requiredfieldvalidator id="rfvIS_BOAT_COOWNED" runat="server" ControlToValidate="cmbIS_BOAT_COOWNED" Display="Dynamic"></asp:requiredfieldvalidator></TD>
						<td class="midcolora" width="25%"><asp:label id="capIS_BOAT_COOWNED_DESC" runat="server">Description</asp:label><span class="mandatory" id="spnIS_BOAT_COOWNED_DESC">*</span></td>
						<td class="midcolora" width="25%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtIS_BOAT_COOWNED_DESC" runat="server" MaxLength="50"
								size="28"></asp:textbox><br>
							<asp:label id="lblIS_BOAT_COOWNED_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><asp:requiredfieldvalidator id="rfvIS_BOAT_COOWNED_DESC" runat="server" ControlToValidate="txtIS_BOAT_COOWNED_DESC"
								Display="Dynamic"></asp:requiredfieldvalidator></td>
						</TD>
					</tr>
					<tr>
						<TD class="midcolora"><asp:label id="capIS_REGISTERED_OTHERS" runat="server">Registered</asp:label><span class="mandatory" id="spnIS_REGISTERED_OTHER">*</span>
						</TD>
						<TD class="midcolora"><asp:dropdownlist id="cmbIS_REGISTERED_OTHERS" onfocus="SelectComboIndex('cmbIS_REGISTERED_OTHERS')"
								runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtIS_REGISTERED_OTHERS_DESC'),document.getElementById('lblIS_REGISTERED_OTHERS_DESC'));"></asp:dropdownlist><br>
							<asp:requiredfieldvalidator id="rfvIS_REGISTERED_OTHERS" runat="server" ControlToValidate="cmbIS_REGISTERED_OTHERS"
								Display="Dynamic"></asp:requiredfieldvalidator>
						<td class="midcolora"><asp:label id="capIS_REGISTERED_OTHERS_DESC" runat="server"> Registration Description</asp:label><span class="mandatory" id="spnIS_REGISTERED_OTHERS_DESC">*</span>
						</td>
						<td class="midcolora"><asp:textbox onkeypress="MaxLength(this,50);" id="txtIS_REGISTERED_OTHERS_DESC" runat="server"
								MaxLength="50" size="28"></asp:textbox><br>
							<asp:label id="lblIS_REGISTERED_OTHERS_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><asp:requiredfieldvalidator id="rfvIS_REGISTERED_OTHERS_DESC" runat="server" ControlToValidate="txtIS_REGISTERED_OTHERS_DESC"
								Display="Dynamic"></asp:requiredfieldvalidator></td>
					</tr>
					<tr>
						<TD class="midcolora"><asp:label id="capPARTICIPATE_RACE" runat="server"></asp:label><span class="mandatory" id="spnPARTICIPATE_RACES">*</span></TD>
						<TD class="midcolora"><asp:dropdownlist id="cmbPARTICIPATE_RACE" onfocus="SelectComboIndex('cmbPARTICIPATE_RACE')" runat="server"
								onchange="javascript:EnableDisableDesc(this,document.getElementById('txtPARTICIPATE_RACE_DESC'),document.getElementById('lblPARTICIPATE_RACE_DESC'));"></asp:dropdownlist><br>
							<asp:requiredfieldvalidator id="rfvPARTICIPATE_RACE" runat="server" ControlToValidate="cmbPARTICIPATE_RACE"
								Display="Dynamic"></asp:requiredfieldvalidator></TD>
						<td class="midcolora"><asp:label id="capPARTICIPATE_RACE_DESC" runat="server"></asp:label><span class="mandatory" id="spnPARTICIPATE_RACE_DESC">*</span></td>
						<td class="midcolora"><asp:textbox onkeypress="MaxLength(this,50);" id="txtPARTICIPATE_RACE_DESC" runat="server" MaxLength="50"
								size="28"></asp:textbox><br>
							<asp:label id="lblPARTICIPATE_RACE_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><asp:requiredfieldvalidator id="rfvPARTICIPATE_RACE_DESC" runat="server" ControlToValidate="txtPARTICIPATE_RACE_DESC"
								Display="Dynamic"></asp:requiredfieldvalidator></td>
					</tr>
					<tr>
						<TD class="midcolora"><asp:label id="capCARRY_PASSENGER_FOR_CHARGE" runat="server"></asp:label><span class="mandatory" id="spnCARRY_PASSENGER_FOR_CHARGE">*</span>
						</TD>
						<TD class="midcolora"><asp:dropdownlist id="cmbCARRY_PASSENGER_FOR_CHARGE" onfocus="SelectComboIndex('cmbCARRY_PASSENGER_FOR_CHARGE')"
								runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtCARRY_PASSENGER_FOR_CHARGE_DESC'),document.getElementById('lblCARRY_PASSENGER_FOR_CHARGE_DESC'));"></asp:dropdownlist><br>
							<asp:requiredfieldvalidator id="rfvCARRY_PASSENGER_FOR_CHARGE" runat="server" ControlToValidate="cmbCARRY_PASSENGER_FOR_CHARGE"
								Display="Dynamic"></asp:requiredfieldvalidator></TD>
						<td class="midcolora"><asp:label id="capCARRY_PASSENGER_FOR_CHARGE_DESC" runat="server"></asp:label><span class="mandatory" id="spnCARRY_PASSENGER_FOR_CHARGE_DESC">*</span>
						</td>
						<td class="midcolora"><asp:textbox onkeypress="MaxLength(this,50);" id="txtCARRY_PASSENGER_FOR_CHARGE_DESC" runat="server"
								MaxLength="50" size="28"></asp:textbox><br>
							<asp:label id="lblCARRY_PASSENGER_FOR_CHARGE_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><asp:requiredfieldvalidator id="rfvCARRY_PASSENGER_FOR_CHARGE_DESC" runat="server" ControlToValidate="txtCARRY_PASSENGER_FOR_CHARGE_DESC"
								Display="Dynamic"></asp:requiredfieldvalidator></td>
					</tr>
					<tr>
						<TD class="midcolora"><asp:label id="capPRIOR_INSURANCE_CARRIER" runat="server"></asp:label><span class="mandatory" id="spnPRIOR_INSURANCE_CARRIER">*</span>
						</TD>
						<TD class="midcolora"><asp:dropdownlist id="cmbIS_PRIOR_INSURANCE_CARRIER" onfocus="SelectComboIndex('cmbIS_PRIOR_INSURANCE_CARRIER')"
								runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtPRIOR_INSURANCE_CARRIER_DESC'),document.getElementById('lblPRIOR_INSURANCE_CARRIER_DESC'));"></asp:dropdownlist><br>
							<asp:requiredfieldvalidator id="rfvPRIOR_INSURANCE_CARRIER" runat="server" ControlToValidate="cmbIS_PRIOR_INSURANCE_CARRIER"
								Display="Dynamic"></asp:requiredfieldvalidator></TD>
						<td class="midcolora"><asp:label id="capPRIOR_INSURANCE_CARRIER_DESC" runat="server"></asp:label><span class="mandatory" id="spnPRIOR_INSURANCE_CARRIER_DESC">*</span>
						</td>
						<td class="midcolora"><asp:textbox onkeypress="MaxLength(this,50);" id="txtPRIOR_INSURANCE_CARRIER_DESC" runat="server"
								MaxLength="50" size="28"></asp:textbox><br>
							<asp:label id="lblPRIOR_INSURANCE_CARRIER_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><asp:requiredfieldvalidator id="rfvPRIOR_INSURANCE_CARRIER_DESC" runat="server" ControlToValidate="txtPRIOR_INSURANCE_CARRIER_DESC"
								Display="Dynamic"></asp:requiredfieldvalidator></td>
					</tr>
					<tr>
						<TD class="midcolora"><asp:label id="capMULTI_POLICY_DISC_APPLIED_PP_DESC" runat="server"></asp:label><span class="mandatory">*</span>
						</TD>
						<TD class="midcolora"><asp:dropdownlist id="cmbMULTI_POLICY_DISC_APPLIED" onfocus="SelectComboIndex('cmbMULTI_POLICY_DISC_APPLIED')"
								runat="server" onchange="javascript:HandlePopupImage();javascript:EnableDisableDesc(this,document.getElementById('txtMULTI_POLICY_DISC_APPLIED_PP_DESC'),document.getElementById('lblMULTIPOLICY_DISC_DESC'));"></asp:dropdownlist><br>
							<asp:requiredfieldvalidator id="rfvMULTI_POLICY_DISC_APPLIED" Runat="server" ControlToValidate="cmbMULTI_POLICY_DISC_APPLIED"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
						<td class="midcolora"><asp:label id="capMULTI_POLICY_DISC_APPLIED_DESC" runat="server">Multipolicy Discount Description</asp:label><span class="mandatory" id="spnMULTI_POLICY_DISC_APPLIED_PP_DESC">*</span></td>
						<td class="midcolora"><asp:textbox id="txtMULTI_POLICY_DISC_APPLIED_PP_DESC" runat="server" size="28" MaxLength="50"
								ReadOnly="True"></asp:textbox><asp:label id="lblMULTIPOLICY_DISC_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
							<span id="spnImgSelect"><IMG id="imgSelect" style="CURSOR: hand" alt="" src="../../../cmsweb/images/selecticon.gif"
									runat="server"></span>
							<br>
							<asp:requiredfieldvalidator id="rfvMULTI_POLICY_DISC_APPLIED_PP_DESC" runat="server" ControlToValidate="txtMULTI_POLICY_DISC_APPLIED_PP_DESC"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</td>
					</tr>
					<!--start-->
					<tr>
						<TD class="midcolora">
							<asp:label id="capANY_BOAT_AMPHIBIOUS" runat="server"></asp:label><span class="mandatory">*</span>
						</TD>
						<TD class="midcolora">
							<asp:dropdownlist id="cmbANY_BOAT_AMPHIBIOUS" onfocus="SelectComboIndex('cmbANY_BOAT_AMPHIBIOUS')"
								runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_BOAT_AMPHIBIOUS_DESC'),document.getElementById('lblANY_BOAT_AMPHIBIOUS_DESC'));"></asp:dropdownlist><br>
							<asp:requiredfieldvalidator id="rfvANY_BOAT_AMPHIBIOUS" Runat="server" ControlToValidate="cmbANY_BOAT_AMPHIBIOUS"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
						<td class="midcolora">
							<asp:label id="capANY_BOAT_AMPHIBIOUS_DESC" runat="server">Any of the boats Hydrofoils or Amphibious Desc</asp:label><span class="mandatory" id="spnANY_BOAT_AMPHIBIOUS_DESC">*</span>
						</td>
						<td class="midcolora">
							<asp:textbox id="txtANY_BOAT_AMPHIBIOUS_DESC" runat="server" size="28" MaxLength="50"></asp:textbox><asp:label id="lblANY_BOAT_AMPHIBIOUS_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
							<asp:requiredfieldvalidator id="rfvANY_BOAT_AMPHIBIOUS_DESC" runat="server" ControlToValidate="txtANY_BOAT_AMPHIBIOUS_DESC"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</td>
					</tr>
					<!--MID-->
					<tr>
						<TD class="midcolora">
							<asp:label id="capANY_BOAT_RESIDENCE" runat="server"></asp:label><span class="mandatory">*</span>
						</TD>
						<TD class="midcolora">
							<asp:dropdownlist id="cmbANY_BOAT_RESIDENCE" onfocus="SelectComboIndex('cmbANY_BOAT_RESIDENCE')" runat="server"
								onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_BOAT_RESIDENCE_DESC'),document.getElementById('lblANY_BOAT_RESIDENCE_DESC'));"></asp:dropdownlist><br>
							<asp:requiredfieldvalidator id="rfvANY_BOAT_RESIDENCE" Runat="server" ControlToValidate="cmbANY_BOAT_RESIDENCE"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
						<td class="midcolora">
							<asp:label id="capANY_BOAT_RESIDENCE_DESC" runat="server">Any of the boats Hydrofoils or Amphibious Desc</asp:label><span class="mandatory" id="spnANY_BOAT_RESIDENCE_DESC">*</span>
						</td>
						<td class="midcolora">
							<asp:textbox id="txtANY_BOAT_RESIDENCE_DESC" runat="server" size="28" MaxLength="50"></asp:textbox><asp:label id="lblANY_BOAT_RESIDENCE_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
							<asp:requiredfieldvalidator id="rfvANY_BOAT_RESIDENCE_DESC" runat="server" ControlToValidate="txtANY_BOAT_RESIDENCE_DESC"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</td>
					</tr>
					
					<!--Added 19 sep 2007-->
					<tr>
						<TD class="midcolora">
							<asp:label id="capIS_BOAT_USED_IN_ANY_WATER" runat="server"></asp:label><span class="mandatory">*</span>
						</TD>
						<TD class="midcolora">
							<asp:dropdownlist id="cmbIS_BOAT_USED_IN_ANY_WATER" onfocus="SelectComboIndex('cmbIS_BOAT_USED_IN_ANY_WATER')" runat="server"
								onchange="javascript:EnableDisableDesc(this,document.getElementById('txtIS_BOAT_USED_IN_ANY_WATER_DESC'),document.getElementById('lblIS_BOAT_USED_IN_ANY_WATER_DESC'));"></asp:dropdownlist><br>
							<asp:requiredfieldvalidator id="rfvIS_BOAT_USED_IN_ANY_WATER" Runat="server" ControlToValidate="cmbIS_BOAT_USED_IN_ANY_WATER"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</TD>
						<td class="midcolora">
							<asp:label id="capIS_BOAT_USED_IN_ANY_WATER_DESC" runat="server"></asp:label><span class="mandatory" id="spnIS_BOAT_USED_IN_ANY_WATER_DESC">*</span>
						</td>
						<td class="midcolora">
							<asp:textbox id="txtIS_BOAT_USED_IN_ANY_WATER_DESC" runat="server" size="28" MaxLength="50"></asp:textbox><asp:label id="lblIS_BOAT_USED_IN_ANY_WATER_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
							<asp:requiredfieldvalidator id="rfvIS_BOAT_USED_IN_ANY_WATER_DESC" runat="server" ControlToValidate="txtIS_BOAT_USED_IN_ANY_WATER_DESC"
								Display="Dynamic"></asp:requiredfieldvalidator>
						</td>
					</tr>
					<!--end-->
					<tr>
						<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
						<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
					</tr>
				</TABLE>
				</TD></TR></TBODY></TABLE> <INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
				<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
				<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
				<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
				<INPUT id="hidAPP_ID" type="hidden" value="0" name="hidAPP_ID" runat="server"> <INPUT id="hidAPP_VERSION_ID" type="hidden" value="0" name="hidAPP_VERSION_ID" runat="server">
				<input id="hidRowId" type="hidden" value="0" name="hidRowId" runat="server"> <input id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
				<input id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
				<INPUT id="hidPolicyCount" type="hidden" value="0" name="hidPolicyCount" runat="server">
				<INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
				<INPUT id="hidActivePolicyList" type="hidden" value="0" name="hidActivePolicyList" runat="server">
			</FORM>
		</div>
	</BODY>
</HTML>

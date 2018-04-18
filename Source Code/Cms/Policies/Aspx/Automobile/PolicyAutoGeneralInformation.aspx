<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="PolicyAutoGeneralInformation.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.PolicyAutoGeneralInformation" ValidateRequest = "false"%>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Policy Automobile General Information</title>
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
		
		function ResetClick()
		{
			ResetForm('APP_AUTO_GEN_INFO');
			Check();
			Check1();
			ResetControls();
					
			return false;
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
		}
			function AddData()
			{
			
			document.getElementById('hidCUSTOMER_ID').value	=	'New';
			document.getElementById('cmbANY_NON_OWNED_VEH').focus();
			document.getElementById('cmbANY_NON_OWNED_VEH').options.selectedIndex = 1;
			document.getElementById('txtANY_NON_OWNED_VEH_PP_DESC').value= ''; 
			document.getElementById('cmbCAR_MODIFIED').options.selectedIndex = 1;
			document.getElementById('txtCAR_MODIFIED_DESC').value= ''; 
			document.getElementById('cmbEXISTING_DMG').options.selectedIndex = 1;
			document.getElementById('txtEXISTING_DMG_PP_DESC').value= ''; 
			document.getElementById('cmbANY_CAR_AT_SCH').options.selectedIndex = 1;
			document.getElementById('txtANY_CAR_AT_SCH_DESC').value= ''; 
			document.getElementById('cmbANY_OTH_AUTO_INSU').options.selectedIndex = 1;
			document.getElementById('txtANY_OTH_AUTO_INSU_DESC').value= ''; 
			//document.getElementById('cmbANY_OTH_INSU_COMP').options.selectedIndex = 1;
			//document.getElementById('txtANY_OTH_INSU_COMP_PP_DESC').value= ''; 
			document.getElementById('cmbH_MEM_IN_MILITARY').options.selectedIndex = 1;
			document.getElementById('cmbH_MEM_IN_MILITARY_DESC').options.selectedIndex = 1; 

			document.getElementById('cmbDRIVER_SUS_REVOKED').options.selectedIndex = 1;
			document.getElementById('txtDRIVER_SUS_REVOKED_PP_DESC').value= ''; 
			document.getElementById('cmbPHY_MENTL_CHALLENGED').options.selectedIndex = 1;
			document.getElementById('txtPHY_MENTL_CHALLENGED_PP_DESC').value= ''; 
			document.getElementById('cmbANY_FINANCIAL_RESPONSIBILITY').options.selectedIndex = 1;
			document.getElementById('txtANY_FINANCIAL_RESPONSIBILITY_PP_DESC').value= ''; 
			document.getElementById('cmbINS_AGENCY_TRANSFER').options.selectedIndex = 1;
			document.getElementById('txtINS_AGENCY_TRANSFER_PP_DESC').value= ''; 
			document.getElementById('cmbCOVERAGE_DECLINED').options.selectedIndex = 1;
			document.getElementById('txtCOVERAGE_DECLINED_PP_DESC').value= ''; 
			document.getElementById('cmbAGENCY_VEH_INSPECTED').options.selectedIndex = 1;
			document.getElementById('txtAGENCY_VEH_INSPECTED_PP_DESC').value= ''; 
			document.getElementById('cmbUSE_AS_TRANSPORT_FEE').options.selectedIndex = 1;
			document.getElementById('txtUSE_AS_TRANSPORT_FEE_DESC').value= ''; 
			document.getElementById('cmbSALVAGE_TITLE').options.selectedIndex = 1;
			document.getElementById('txtSALVAGE_TITLE_PP_DESC').value= ''; 
			document.getElementById('cmbANY_ANTIQUE_AUTO').options.selectedIndex = 1;
			document.getElementById('txtANY_ANTIQUE_AUTO_DESC').value= ''; 
			document.getElementById('cmbMULTI_POLICY_DISC_APPLIED').options.selectedIndex = 1;
			document.getElementById('txtMULTI_POLICY_DISC_APPLIED_PP_DESC').value= ''; 
			document.getElementById('txtFullName').value= ''; 
			document.getElementById('txtDATE_OF_BIRTH').value= ''; 
			document.getElementById('txtDrivingLisence').value= ''; 
			document.getElementById('cmbInsuredElseWhere').options.selectedIndex = 1;
			document.getElementById('txtCompanyName').value= ''; 
			document.getElementById('txtPolicyNumber').value= ''; 
			document.getElementById('cmbCURR_RES_TYPE').options.selectedIndex = 1;
			document.getElementById('cmbIS_OTHER_THAN_INSURED').options.selectedIndex = 1;
			document.getElementById('txtCOST_EQUIPMENT_DESC').value='';
			document.getElementById('cmbANY_PRIOR_LOSSES').options.selectedIndex = -1;
			document.getElementById('txtANY_PRIOR_LOSSES_DESC').value= ''; 


			document.getElementById('txtREMARKS').value= ''; 
			ChangeColor();
			DisableValidators();
										
						
			//document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			return false;
			}

			function populateXML()
			{
				if(document.getElementById('hidFormSaved')!=null && document.getElementById('hidFormSaved').value == '0' ||document.getElementById('hidFormSaved').value == '1')
				{
				
				var tempXML;
				
					if(document.getElementById('hidOldData').value!="")
					{
					
				
					tempXML=document.getElementById('hidOldData').value;
					
					//Enabling the activate deactivate button
					document.getElementById('txtFullName').value= ''; 
					document.getElementById('txtDATE_OF_BIRTH').value= ''; 
					document.getElementById('txtDrivingLisence').value= ''; 
					document.getElementById('cmbInsuredElseWhere').options.selectedIndex = 1;
					document.getElementById('txtCompanyName').value= ''; 
					document.getElementById('txtPolicyNumber').value= ''; 
					document.getElementById('hidOldData').value		=	 tempXML;
					populateFormData(tempXML,APP_AUTO_GEN_INFO);
					}
					else
					{
					AddData();
					}
				}	
				ResetControls();
				Check();
				Check1();
				ShowCarModifyRow();
				return false;
			}
			
			// Commented by Swastika as Compare Validator is being used instead of Custom Validator.
			// Dated : 14th Mar'06 for Pol Iss# 96		
/*		function CompareAllWolYears(source, arguments)
		 {
			//Check For No of years
			if(parseInt(document.APP_AUTO_GEN_INFO.txtYEARS_INSU_WOL.value)>parseInt(document.APP_AUTO_GEN_INFO.txtYEARS_INSU.value))
			{
				arguments.IsValid = false;
				return;   
				
			}
			else
			arguments.IsValid = true;
		  } */
			//Check For Insure with Wolurine and total Insure Years
		 function CompareAllWolYears(source, arguments)
		   {
		    var yearWol  = document.APP_AUTO_GEN_INFO.txtYEARS_INSU_WOL.value;
		    var yearAll  = document.APP_AUTO_GEN_INFO.txtYEARS_INSU.value;
			//Check For No of years
			if(parseInt(yearWol)>parseInt(yearAll))
		    	{
		 	    	arguments.IsValid = false;
				    return;   
		    	}
		  }		
		
		function ResetControls()
		{
			EnableDisableDesc(document.getElementById('cmbANY_NON_OWNED_VEH'),document.getElementById('txtANY_NON_OWNED_VEH_PP_DESC'),document.getElementById('lblVEHICLE_DESC'));
			EnableDisableDesc(document.getElementById('cmbEXISTING_DMG'),document.getElementById('txtEXISTING_DMG_PP_DESC'),document.getElementById('lblDAMAGED_DESC'));
			EnableDisableDesc(document.getElementById('cmbANY_OTH_AUTO_INSU'),document.getElementById('txtANY_OTH_AUTO_INSU_DESC'),document.getElementById('lblAUTO_INSU_DESC'));
			EnableDisableDesc(document.getElementById('cmbANY_CAR_AT_SCH'),document.getElementById('txtANY_CAR_AT_SCH_DESC'),document.getElementById('lblCAR_DESC'));
			//EnableDisableDesc(document.getElementById('cmbANY_OTH_INSU_COMP'),document.getElementById('txtANY_OTH_INSU_COMP_PP_DESC'),document.getElementById('lblOTHER_INSU_DESC'));
			EnableDisableDesc(document.getElementById('cmbH_MEM_IN_MILITARY'),document.getElementById('cmbH_MEM_IN_MILITARY_DESC'),document.getElementById('lblMEMBER_DESC'));
			EnableDisableDesc(document.getElementById('cmbDRIVER_SUS_REVOKED'),document.getElementById('txtDRIVER_SUS_REVOKED_PP_DESC'),document.getElementById('lblDRIVER_LIC_DESC'));
			EnableDisableDesc(document.getElementById('cmbPHY_MENTL_CHALLENGED'),document.getElementById('txtPHY_MENTL_CHALLENGED_PP_DESC'),document.getElementById('lbldRIVER_IMP_DESC'));
			EnableDisableDesc(document.getElementById('cmbANY_FINANCIAL_RESPONSIBILITY'),document.getElementById('txtANY_FINANCIAL_RESPONSIBILITY_PP_DESC'),document.getElementById('lblDRIVER_NO_DESC'));
			EnableDisableDesc(document.getElementById('cmbINS_AGENCY_TRANSFER'),document.getElementById('txtINS_AGENCY_TRANSFER_PP_DESC'),document.getElementById('lblINSU_TRANS_DESC'));
			EnableDisableDesc(document.getElementById('cmbCOVERAGE_DECLINED'),document.getElementById('txtCOVERAGE_DECLINED_PP_DESC'),document.getElementById('lblCOVERAGE_DESC'));
			EnableDisableDesc(document.getElementById('cmbAGENCY_VEH_INSPECTED'),document.getElementById('txtAGENCY_VEH_INSPECTED_PP_DESC'),document.getElementById('lblAGENT_INS_DESC'));
			EnableDisableDesc(document.getElementById('cmbUSE_AS_TRANSPORT_FEE'),document.getElementById('txtUSE_AS_TRANSPORT_FEE_DESC'),document.getElementById('lblTRANSPORT_DESC'));
			EnableDisableDesc(document.getElementById('cmbSALVAGE_TITLE'),document.getElementById('txtSALVAGE_TITLE_PP_DESC'),document.getElementById('lblSALVAGE_DESC'));
			EnableDisableDesc(document.getElementById('cmbANY_PRIOR_LOSSES'),document.getElementById('txtANY_PRIOR_LOSSES_DESC'),document.getElementById('lblPRIOR_LOSSES'));
			EnableDisableDesc(document.getElementById('cmbANY_ANTIQUE_AUTO'),document.getElementById('txtANY_ANTIQUE_AUTO_DESC'),document.getElementById('lblANY_ANTIQUE_AUTO_DESC'));
			EnableDisableDesc(document.getElementById('cmbMULTI_POLICY_DISC_APPLIED'),document.getElementById('txtMULTI_POLICY_DISC_APPLIED_PP_DESC'),document.getElementById('lblMULTIPOLICY_DISC_DESC'));
		    
			ShowCarModifyRow();

		}	
		
				
		function ChkTextAreaLength(source, arguments)
		{
			var txtArea = arguments.Value;
			if(txtArea.length > 255 ) 
			{
				arguments.IsValid = false;
				return;   // invalid userName
			}
		}
		
		function Check1()
		{
			if(document.getElementById('cmbInsuredElseWhere').options[document.getElementById('cmbInsuredElseWhere').selectedIndex].value=='1')
			{
			
				
				if(document.getElementById('cmbIS_OTHER_THAN_INSURED').options[document.getElementById('cmbIS_OTHER_THAN_INSURED').selectedIndex].value=='1')
				{
					document.getElementById('trCompanyName').style.display ='inline';
					document.getElementById("rfvCompnayName").setAttribute('enabled',true);
					document.getElementById("rfvPolicyNumber").setAttribute('enabled',true);
				}
				else
				{
					document.getElementById('trCompanyName').style.display ='none';
					document.getElementById("rfvCompnayName").setAttribute('enabled',true);
					document.getElementById("rfvCompnayName").setAttribute('isValid',true);
					document.getElementById("rfvPolicyNumber").setAttribute('enabled',true);
					document.getElementById("rfvPolicyNumber").setAttribute('isValid',true);
				}
		}
		else
		{
			document.getElementById('trCompanyName').style.display ='none';
			document.getElementById("rfvCompnayName").setAttribute('isValid',false);
			document.getElementById("rfvCompnayName").style.display='none';
			document.getElementById("rfvCompnayName").setAttribute('enabled',false);
			document.getElementById("rfvPolicyNumber").setAttribute('isValid',false);
			document.getElementById("rfvPolicyNumber").style.display='none';
			document.getElementById("rfvPolicyNumber").setAttribute('enabled',false);
		
		}
	}
	function ShowCarModifyRow()
	{	
		
		if ( document.getElementById('cmbCAR_MODIFIED').options[document.getElementById('cmbCAR_MODIFIED').selectedIndex].value == '1')
		{
		
			document.getElementById('trCarModify').style.display ='inline';
			document.getElementById("rfvCAR_MODIFIED_DESC").setAttribute('enabled',true);
			document.getElementById("revCOST_EQUIPMENT_DESC").setAttribute('enabled',true);
		}
		else
		{
			
			document.getElementById('trCarModify').style.display ='none';
			document.getElementById("rfvCAR_MODIFIED_DESC").setAttribute('enabled',false);
			document.getElementById("rfvCAR_MODIFIED_DESC").setAttribute('isValid',false);
			document.getElementById("rfvCAR_MODIFIED_DESC").style.display = "none";
			document.getElementById("revCOST_EQUIPMENT_DESC").setAttribute('enabled',false);
			document.getElementById("revCOST_EQUIPMENT_DESC").setAttribute('isValid',false);
			document.getElementById("revCOST_EQUIPMENT_DESC").style.display = "none";
		}
		
	}
	function Check()
	{ 
		
		
		
		if(document.getElementById('cmbIS_OTHER_THAN_INSURED').options[document.getElementById('cmbIS_OTHER_THAN_INSURED').selectedIndex].value=='1')
		{
			
			document.getElementById('trHeaderFullName').style.display ='inline';
			document.getElementById('trHeaderDriving').style.display ='inline';
			document.getElementById('trInsuredElse').style.display ='inline';
			document.getElementById("rfvDATE_OF_BIRTH").setAttribute('enabled',true);
			document.getElementById("rfvDATE_OF_BIRTH").setAttribute('isValid',true);
			document.getElementById("rfvFullName").setAttribute('enabled',true);
			document.getElementById("rfvFullName").setAttribute('isValid',true);
	
		}
		else
		{
			
			document.getElementById('trHeaderFullName').style.display ='none';
			document.getElementById('trHeaderDriving').style.display ='none';
			document.getElementById('trInsuredElse').style.display ='none';
			document.getElementById('trCompanyName').style.display ='none';
			document.getElementById("rfvDATE_OF_BIRTH").setAttribute('isValid',false);
			document.getElementById("rfvDATE_OF_BIRTH").style.display='none';
			document.getElementById("rfvDATE_OF_BIRTH").setAttribute('enabled',false);
			document.getElementById("rfvFullName").setAttribute('isValid',false);
			document.getElementById("rfvFullName").style.display='none';
			document.getElementById("rfvFullName").setAttribute('enabled',false);
			document.getElementById("rfvCompnayName").setAttribute('isValid',false);
			document.getElementById("rfvCompnayName").style.display='none';
			document.getElementById("rfvCompnayName").setAttribute('enabled',false);
			document.getElementById("rfvPolicyNumber").setAttribute('isValid',false);
			document.getElementById("rfvPolicyNumber").style.display='none';
			document.getElementById("rfvPolicyNumber").setAttribute('enabled',false);
		
		}		
		
	}
	//Added By Raghav on 24-07-2008 Itrack Issue #4506		
		function CursorFocus()
		{			
				if(document.getElementById('cmbANY_NON_OWNED_VEH')!=null)	 
				document.getElementById('cmbANY_NON_OWNED_VEH').focus();
		}
	
	
	function ChkDateOfBirth(objSource , objArgs)
	{
		if(document.getElementById("revDATE_OF_BIRTH").isvalid==true)
		{
			var effdate=document.APP_AUTO_GEN_INFO.txtDATE_OF_BIRTH.value;
			objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
		}
		else
		     	objArgs.IsValid = true;	
	}	
	
	function SaveUnderw()
	{
		document.getElementById('txtCOST_EQUIPMENT_DESC').value=formatCurrency(document.getElementById('txtCOST_EQUIPMENT_DESC').value);
	}

		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" topMargin="0" leftmargin="0" rightmargin="0" onload="populateXML();ApplyColor();ChangeColor();showScroll();CursorFocus();">
		<FORM id="APP_AUTO_GEN_INFO" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
				<tr id="messageID">
					<td align="center" colSpan="3"><asp:label id="capMessage" Visible="False" CssClass="errmsg" Runat="server"></asp:label></td>
				</tr>
				<TR id="trMessage" runat="server">
					<TD>
						<TABLE class="tableWidthHeader" id="mainTable" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4"><webcontrol:workflow isTop="false" id="myWorkFlow" runat="server"></webcontrol:workflow></TD>
							</tr>
							<!--<tr>
									<TD class="pageHeader" id="tdClientTop" colSpan="4"><webcontrol:clienttop id="cltClientTop" runat="server" width="100%"></webcontrol:clienttop><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></TD>
								</tr>
								<tr>
									<td class="headereffectCenter" colSpan="4">Underwriting Questions</td>
								</tr>-->
							<tr>
								<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
									mandatory</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="32%"><asp:label id="capANY_NON_OWNED_VEH" runat="server">Non</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbANY_NON_OWNED_VEH" onfocus="SelectComboIndex('cmbANY_NON_OWNED_VEH')" runat="server"
										onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_NON_OWNED_VEH_PP_DESC'),document.getElementById('lblVEHICLE_DESC'));"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvANY_NON_OWNED_VEH" runat="server" ControlToValidate="cmbANY_NON_OWNED_VEH"
										ErrorMessage="ANY_NON_OWNED_VEH can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<td class="midcolora" width="32%"><asp:label id="capANY_NON_OWNED_VEH_PP_DESC" runat="server">Vehicle Description</asp:label><span class="mandatory" id="spnANY_NON_OWNED_VEH_PP_DESC">*</span></td>
								<td class="midcolora" width="18%"><asp:textbox id="txtANY_NON_OWNED_VEH_PP_DESC" runat="server" size="28" MaxLength="50"></asp:textbox><asp:label id="lblVEHICLE_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									<asp:requiredfieldvalidator id="rfvANY_NON_OWNED_VEH_PP_DESC" runat="server" ControlToValidate="txtANY_NON_OWNED_VEH_PP_DESC"
										Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<TD class="midcolora"><asp:label id="capCAR_MODIFIED" runat="server">Modified</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora"><asp:dropdownlist id="cmbCAR_MODIFIED" onfocus="SelectComboIndex('cmbCAR_MODIFIED')" runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvCAR_MODIFIED" runat="server" ControlToValidate="cmbCAR_MODIFIED" ErrorMessage="CAR_MODIFIED can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<td colspan="2" class="midcolora">&nbsp;</td>
							</tr>
							<tr runat="server" id="trCarModify">
								<td class="midcolora"><asp:label id="capCAR_MODIFIED_DESC" runat="server">Car Modified Description</asp:label><span class="mandatory" id="spnCAR_MODIFIED_DESC">*</span></td>
								<td class="midcolora"><asp:textbox id="txtCAR_MODIFIED_DESC" runat="server" size="28" MaxLength="50"></asp:textbox>
									<asp:requiredfieldvalidator id="rfvCAR_MODIFIED_DESC" runat="server" ControlToValidate="txtCAR_MODIFIED_DESC"
										Display="Dynamic"></asp:requiredfieldvalidator></td>
								<td class="midcolora">
									<asp:label id="capCOST_EQUIPMENT_DESC" runat="server">Cost of the customized equipment</asp:label><span class="mandatory">*</span>
								</td>
								<td class="midcolora">
									<asp:textbox id="txtCOST_EQUIPMENT_DESC" CssClass="INPUTCURRENCY" runat="server" size='18' maxlength='10'></asp:textbox><br>
									<asp:regularexpressionvalidator id="revCOST_EQUIPMENT_DESC" Display="Dynamic" ControlToValidate="txtCOST_EQUIPMENT_DESC"
										Runat="server"></asp:regularexpressionvalidator>
								</td>
							</tr>
							<tr>
								<TD class="midcolora"><asp:label id="capEXISTING_DMG" runat="server">Dmg</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora"><asp:dropdownlist id="cmbEXISTING_DMG" onfocus="SelectComboIndex('cmbEXISTING_DMG')" runat="server"
										onchange="javascript:EnableDisableDesc(this,document.getElementById('txtEXISTING_DMG_PP_DESC'),document.getElementById('lblDAMAGED_DESC'));"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvEXISTING_DMG" runat="server" ControlToValidate="cmbEXISTING_DMG" ErrorMessage="EXISTING_DMG can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<td class="midcolora"><asp:label id="capEXISTING_DMG_PP_DESC" runat="server">Damaged Description</asp:label><span class="mandatory" id="spnEXISTING_DMG_PP_DESC">*</span></td>
								<td class="midcolora"><asp:textbox id="txtEXISTING_DMG_PP_DESC" runat="server" size="28" MaxLength="50"></asp:textbox><asp:label id="lblDAMAGED_DESC" runat="server" CssClass="LabelFont" DESIGNTIMEDRAGDROP="7437">-N.A.-</asp:label><br>
									<asp:requiredfieldvalidator id="rfvEXISTING_DMG_PP_DESC" runat="server" ControlToValidate="txtEXISTING_DMG_PP_DESC"
										Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<TD class="midcolora"><asp:label id="capANY_CAR_AT_SCH" runat="server">Car</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora"><asp:dropdownlist id="cmbANY_CAR_AT_SCH" onfocus="SelectComboIndex('cmbANY_CAR_AT_SCH')" runat="server"
										onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_CAR_AT_SCH_DESC'),document.getElementById('lblCAR_DESC'));"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvANY_CAR_AT_SCH" runat="server" ControlToValidate="cmbANY_CAR_AT_SCH" ErrorMessage="ANY_CAR_AT_SCH can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<td class="midcolora"><asp:label id="capANY_CAR_AT_SCH_DESC" runat="server">Car Description</asp:label><span class="mandatory" id="spnANY_CAR_AT_SCH_DESC">*</span></td>
								<td class="midcolora"><asp:textbox id="txtANY_CAR_AT_SCH_DESC" runat="server" size="28" MaxLength="50"></asp:textbox><asp:label id="lblCAR_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									<asp:requiredfieldvalidator id="rfvANY_CAR_AT_SCH_DESC" runat="server" ControlToValidate="txtANY_CAR_AT_SCH_DESC"
										Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<TD class="midcolora"><asp:label id="capANY_OTH_AUTO_INSU" runat="server">Oth</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora"><asp:dropdownlist id="cmbANY_OTH_AUTO_INSU" onfocus="SelectComboIndex('cmbANY_OTH_AUTO_INSU')" runat="server"
										onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_OTH_AUTO_INSU_DESC'),document.getElementById('lblAUTO_INSU_DESC'));"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvANY_OTH_AUTO_INSU" runat="server" ControlToValidate="cmbANY_OTH_AUTO_INSU"
										ErrorMessage="ANY_OTH_AUTO_INSU can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<td class="midcolora"><asp:label id="capANY_OTH_AUTO_INSU_DESC" runat="server">Auto Insurance Description</asp:label><span class="mandatory" id="spnANY_OTH_AUTO_INSU_DESC">*</span></td>
								<td class="midcolora"><asp:textbox id="txtANY_OTH_AUTO_INSU_DESC" runat="server" size="28" MaxLength="50"></asp:textbox><asp:label id="lblAUTO_INSU_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									<asp:requiredfieldvalidator id="rfvANY_OTH_AUTO_INSU_DESC" runat="server" ControlToValidate="txtANY_OTH_AUTO_INSU_DESC"
										Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<!--<tr>
									<TD class="midcolora"><%--<asp:label id="capANY_OTH_INSU_COMP" runat="server">Oth</asp:label>--%><span class="mandatory">*</span></TD>
									<TD class="midcolora"><%--<asp:dropdownlist id="cmbANY_OTH_INSU_COMP" onfocus="SelectComboIndex('cmbANY_OTH_INSU_COMP')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_OTH_INSU_COMP_PP_DESC'),document.getElementById('lblOTHER_INSU_DESC'));"></asp:dropdownlist>--%><BR>
										<%--<asp:requiredfieldvalidator id="rfvANY_OTH_INSU_COMP" runat="server" ControlToValidate="cmbANY_OTH_INSU_COMP"
											ErrorMessage="ANY_OTH_INSU_COMP can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>--%></TD>
									<td class="midcolora"><%--<asp:label id="capANY_OTH_INSU_COMP_PP_DESC" runat="server">Other Insurance Description</asp:label>--%><span class="mandatory" id="spnANY_OTH_INSU_COMP_PP_DESC">*</span></td>
									<td class="midcolora">--%><%--<asp:textbox id="txtANY_OTH_INSU_COMP_PP_DESC" runat="server" size="28" MaxLength="50"></asp:textbox>--%> <%--<asp:label id="lblOTHER_INSU_DESC" runat="server" CssClass="LabelFont" DESIGNTIMEDRAGDROP="7434">-N.A.-</asp:label>--%><br>
										<%--<asp:requiredfieldvalidator id="rfvANY_OTH_INSU_COMP_PP_DESC" runat="server" ControlToValidate="txtANY_OTH_INSU_COMP_PP_DESC"
											Display="Dynamic"></asp:requiredfieldvalidator>--%></td>
								</tr>-->
							
							<tr>
								<TD class="midcolora"><asp:label id="capH_MEM_IN_MILITARY" runat="server">Mem</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora"><asp:dropdownlist id="cmbH_MEM_IN_MILITARY" onfocus="SelectComboIndex('cmbH_MEM_IN_MILITARY')" runat="server"
										onchange="javascript:EnableDisableDesc(this,document.getElementById('cmbH_MEM_IN_MILITARY_DESC'),document.getElementById('lblMEMBER_DESC'));"></asp:dropdownlist><BR>
								</TD>
								<td class="midcolora"><asp:label id="capH_MEM_IN_MILITARY_DESC" runat="server">Member Description</asp:label><span class="mandatory" id="spnH_MEM_IN_MILITARY_DESC">*</span></td>
								<td class="midcolora">
									<asp:dropdownlist id="cmbH_MEM_IN_MILITARY_DESC" runat="server"></asp:dropdownlist><asp:label id="lblMEMBER_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									<asp:requiredfieldvalidator id="rfvH_MEM_IN_MILITARY_DESC" runat="server" ControlToValidate="cmbH_MEM_IN_MILITARY_DESC"
										Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<TD class="midcolora"><asp:label id="capDRIVER_SUS_REVOKED" runat="server">Sus</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora"><asp:dropdownlist id="cmbDRIVER_SUS_REVOKED" onfocus="SelectComboIndex('cmbDRIVER_SUS_REVOKED')" runat="server"
										onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDRIVER_SUS_REVOKED_PP_DESC'),document.getElementById('lblDRIVER_LIC_DESC'));"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvDRIVER_SUS_REVOKED" runat="server" ControlToValidate="cmbDRIVER_SUS_REVOKED"
										ErrorMessage="DRIVER_SUS_REVOKED can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<td class="midcolora"><asp:label id="capDRIVER_SUS_REVOKED_PP_DESC" runat="server">Driver License Description</asp:label><span class="mandatory" id="spnDRIVER_SUS_REVOKED_PP_DESC">*</span></td>
								<td class="midcolora"><asp:textbox id="txtDRIVER_SUS_REVOKED_PP_DESC" runat="server" size="28" MaxLength="50"></asp:textbox><asp:label id="lblDRIVER_LIC_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									<asp:requiredfieldvalidator id="rfvDRIVER_SUS_REVOKED_PP_DESC" runat="server" ControlToValidate="txtDRIVER_SUS_REVOKED_PP_DESC"
										Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<TD class="midcolora"><asp:label id="capPHY_MENTL_CHALLENGED" runat="server">Mentl</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora"><asp:dropdownlist id="cmbPHY_MENTL_CHALLENGED" onfocus="SelectComboIndex('cmbPHY_MENTL_CHALLENGED')"
										runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtPHY_MENTL_CHALLENGED_PP_DESC'),document.getElementById('lbldRIVER_IMP_DESC'));"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvPHY_MENTL_CHALLENGED" runat="server" ControlToValidate="cmbPHY_MENTL_CHALLENGED"
										ErrorMessage="PHY_MENTL_CHALLENGED can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<td class="midcolora"><asp:label id="capPHY_MENTL_CHALLENGED_PP_DESC" runat="server">Driver Impairment Description</asp:label><span class="mandatory" id="spnPHY_MENTL_CHALLENGED_PP_DESC">*</span></td>
								<td class="midcolora"><asp:textbox id="txtPHY_MENTL_CHALLENGED_PP_DESC" runat="server" size="28" MaxLength="50"></asp:textbox><asp:label id="lbldRIVER_IMP_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									<asp:requiredfieldvalidator id="rfvPHY_MENTL_CHALLENGED_PP_DESC" runat="server" ControlToValidate="txtPHY_MENTL_CHALLENGED_PP_DESC"
										Display="Dynamic" DESIGNTIMEDRAGDROP="602"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<TD class="midcolora"><asp:label id="capANY_FINANCIAL_RESPONSIBILITY" runat="server">Financial</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora"><asp:dropdownlist id="cmbANY_FINANCIAL_RESPONSIBILITY" onfocus="SelectComboIndex('cmbANY_FINANCIAL_RESPONSIBILITY')"
										runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_FINANCIAL_RESPONSIBILITY_PP_DESC'),document.getElementById('lblDRIVER_NO_DESC'));"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvANY_FINANCIAL_RESPONSIBILITY" runat="server" ControlToValidate="cmbANY_FINANCIAL_RESPONSIBILITY"
										ErrorMessage="ANY_FINANCIAL_RESPONSIBILITY can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<td class="midcolora"><asp:label id="capANY_FINANCIAL_RESPONSIBILITY_PP_DESC" runat="server">Driver Number And Date Of Filing</asp:label><span class="mandatory" id="spnANY_FINANCIAL_RESPONSIBILITY_PP_DESC">*</span></td>
								<td class="midcolora"><asp:textbox id="txtANY_FINANCIAL_RESPONSIBILITY_PP_DESC" runat="server" size="28" MaxLength="50"></asp:textbox><asp:label id="lblDRIVER_NO_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									<asp:requiredfieldvalidator id="rfvANY_FINANCIAL_RESPONSIBILITY_PP_DESC" runat="server" ControlToValidate="txtANY_FINANCIAL_RESPONSIBILITY_PP_DESC"
										Display="Dynamic" DESIGNTIMEDRAGDROP="596"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<TD class="midcolora"><asp:label id="capINS_AGENCY_TRANSFER" runat="server">Agency</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora"><asp:dropdownlist id="cmbINS_AGENCY_TRANSFER" onfocus="SelectComboIndex('cmbINS_AGENCY_TRANSFER')"
										runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtINS_AGENCY_TRANSFER_PP_DESC'),document.getElementById('lblINSU_TRANS_DESC'));"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvINS_AGENCY_TRANSFER" runat="server" ControlToValidate="cmbINS_AGENCY_TRANSFER"
										ErrorMessage="INS_AGENCY_TRANSFER can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<td class="midcolora"><asp:label id="capINS_AGENCY_TRANSFER_PP_DESC" runat="server">Insurance Transfer Description</asp:label><span class="mandatory" id="spnINS_AGENCY_TRANSFER_PP_DESC">*</span></td>
								<td class="midcolora"><asp:textbox id="txtINS_AGENCY_TRANSFER_PP_DESC" runat="server" size="28" MaxLength="50"></asp:textbox><asp:label id="lblINSU_TRANS_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									<asp:requiredfieldvalidator id="rfvINS_AGENCY_TRANSFER_PP_DESC" runat="server" ControlToValidate="txtINS_AGENCY_TRANSFER_PP_DESC"
										Display="Dynamic" DESIGNTIMEDRAGDROP="603"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<TD class="midcolora"><asp:label id="capCOVERAGE_DECLINED" runat="server">Declined</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora"><asp:dropdownlist id="cmbCOVERAGE_DECLINED" onfocus="SelectComboIndex('cmbCOVERAGE_DECLINED')" runat="server"
										onchange="javascript:EnableDisableDesc(this,document.getElementById('txtCOVERAGE_DECLINED_PP_DESC'),document.getElementById('lblCOVERAGE_DESC'));"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvCOVERAGE_DECLINED" runat="server" ControlToValidate="cmbCOVERAGE_DECLINED"
										ErrorMessage="COVERAGE_DECLINED can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<td class="midcolora"><asp:label id="capCOVERAGE_DECLINED_PP_DESC" runat="server">Coverage Description</asp:label><span class="mandatory" id="spnCOVERAGE_DECLINED_PP_DESC">*</span></td>
								<td class="midcolora"><asp:textbox id="txtCOVERAGE_DECLINED_PP_DESC" runat="server" size="28" MaxLength="50"></asp:textbox><asp:label id="lblCOVERAGE_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									<asp:requiredfieldvalidator id="rfvCOVERAGE_DECLINED_PP_DESC" runat="server" ControlToValidate="txtCOVERAGE_DECLINED_PP_DESC"
										Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<TD class="midcolora"><asp:label id="capAGENCY_VEH_INSPECTED" runat="server">Veh</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora"><asp:dropdownlist id="cmbAGENCY_VEH_INSPECTED" onfocus="SelectComboIndex('cmbAGENCY_VEH_INSPECTED')"
										runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtAGENCY_VEH_INSPECTED_PP_DESC'),document.getElementById('lblAGENT_INS_DESC'));"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvAGENCY_VEH_INSPECTED" runat="server" ControlToValidate="cmbAGENCY_VEH_INSPECTED"
										ErrorMessage="AGENCY_VEH_INSPECTED can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<td class="midcolora"><asp:label id="capAGENCY_VEH_INSPECTED_PP_DESC" runat="server">Agent Inspection Description</asp:label><span class="mandatory" id="spnAGENCY_VEH_INSPECTED_PP_DESC">*</span></td>
								<td class="midcolora"><asp:textbox id="txtAGENCY_VEH_INSPECTED_PP_DESC" runat="server" size="28" MaxLength="50"></asp:textbox><asp:label id="lblAGENT_INS_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									<asp:requiredfieldvalidator id="rfvAGENCY_VEH_INSPECTED_PP_DESC" runat="server" ControlToValidate="txtAGENCY_VEH_INSPECTED_PP_DESC"
										Display="Dynamic" DESIGNTIMEDRAGDROP="600"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<TD class="midcolora"><asp:label id="capUSE_AS_TRANSPORT_FEE" runat="server">As</asp:label><!--<span class="mandatory">*</span>--></TD>
								<TD class="midcolora"><asp:dropdownlist id="cmbUSE_AS_TRANSPORT_FEE" onfocus="SelectComboIndex('cmbUSE_AS_TRANSPORT_FEE')"
										runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtUSE_AS_TRANSPORT_FEE_DESC'),document.getElementById('lblTRANSPORT_DESC'));"></asp:dropdownlist><BR>
									<%--<asp:requiredfieldvalidator id="rfvUSE_AS_TRANSPORT_FEE" runat="server" ControlToValidate="cmbUSE_AS_TRANSPORT_FEE"
											ErrorMessage="USE_AS_TRANSPORT_FEE can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>--%>
								</TD>
								<td class="midcolora"><asp:label id="capUSE_AS_TRANSPORT_FEE_DESC" runat="server">Transport Description</asp:label><span class="mandatory" id="spnUSE_AS_TRANSPORT_FEE_DESC">*</span></td>
								<td class="midcolora"><asp:textbox id="txtUSE_AS_TRANSPORT_FEE_DESC" runat="server" size="28" MaxLength="50"></asp:textbox><asp:label id="lblTRANSPORT_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									<asp:requiredfieldvalidator id="rfvUSE_AS_TRANSPORT_FEE_DESC" runat="server" ControlToValidate="txtUSE_AS_TRANSPORT_FEE_DESC"
										Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<TD class="midcolora"><asp:label id="capSALVAGE_TITLE" runat="server">Title</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora"><asp:dropdownlist id="cmbSALVAGE_TITLE" onfocus="SelectComboIndex('cmbSALVAGE_TITLE')" runat="server"
										onchange="javascript:EnableDisableDesc(this,document.getElementById('txtSALVAGE_TITLE_PP_DESC'),document.getElementById('lblSALVAGE_DESC'));"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvSALVAGE_TITLE" runat="server" ControlToValidate="cmbSALVAGE_TITLE" ErrorMessage="SALVAGE_TITLE can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<td class="midcolora"><asp:label id="capSALVAGE_TITLE_PP_DESC" runat="server">Salvage Title Description</asp:label><span class="mandatory" id="spnSALVAGE_TITLE_PP_DESC">*</span></td>
								<td class="midcolora"><asp:textbox id="txtSALVAGE_TITLE_PP_DESC" runat="server" size="28" MaxLength="50"></asp:textbox><asp:label id="lblSALVAGE_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									<asp:requiredfieldvalidator id="rfvSALVAGE_TITLE_PP_DESC" runat="server" ControlToValidate="txtSALVAGE_TITLE_PP_DESC"
										Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<TD class="midcolora"><asp:label id="capANY_ANTIQUE_AUTO" runat="server">Antique</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora"><asp:dropdownlist id="cmbANY_ANTIQUE_AUTO" onfocus="SelectComboIndex('cmbANY_ANTIQUE_AUTO')" runat="server"
										onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_ANTIQUE_AUTO_DESC'),document.getElementById('lblANY_ANTIQUE_AUTO_DESC'));"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvANY_ANTIQUE_AUTO" runat="server" ControlToValidate="cmbANY_ANTIQUE_AUTO"
										ErrorMessage="ANY_ANTIQUE_AUTO can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<td class="midcolora"><asp:label id="capANY_ANTIQUE_AUTO_DESC" runat="server">Vehicle Consideration Description</asp:label><span class="mandatory" id="spnANY_ANTIQUE_AUTO_DESC">*</span></td>
								<td class="midcolora"><asp:textbox id="txtANY_ANTIQUE_AUTO_DESC" runat="server" size="28" MaxLength="50" DESIGNTIMEDRAGDROP="7386"></asp:textbox><asp:label id="lblANY_ANTIQUE_AUTO_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									<asp:requiredfieldvalidator id="rfvANY_ANTIQUE_AUTO_DESC" runat="server" ControlToValidate="txtANY_ANTIQUE_AUTO_DESC"
										Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<TD class="midcolora"><asp:label id="capREMARKS" runat="server">Remarks</asp:label><span>(Max 
										255 characters)</span></TD>
								<TD class="midcolora" colSpan="3"><asp:textbox onkeypress="MaxLength(this,255);" id="txtREMARKS" runat="server" size="30" maxlength="255"
										TextMode="MultiLine"></asp:textbox><BR>
									<asp:customvalidator id="csvREMARKS" Runat="server" ControlToValidate="txtREMARKS" Display="Dynamic"
										ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator></TD>
							</tr>
							<!--<tr>
								<td class="midcolora"><%--<asp:label id="capYEARS_INSU" runat="server">Years Continuously Insured</asp:label>--%></td>
								<td class="midcolora"><%--<asp:textbox id="txtYEARS_INSU" runat="server" size="3" maxlength="3"></asp:textbox>--%><br>
									<%--<asp:regularexpressionvalidator id="revYEARS_INSU" runat="server" ControlToValidate="txtYEARS_INSU" Display="Dynamic"></asp:regularexpressionvalidator>--%></td>
								<td class="midcolora"><%--<asp:label id="capYEARS_INSU_WOL" runat="server"></asp:label>--%></td>
								<td class="midcolora"><%--<asp:textbox id="txtYEARS_INSU_WOL" runat="server" size="3" maxlength="3"></asp:textbox>--%><br>
									<%--<asp:regularexpressionvalidator id="revYEARS_INSU_WOL" runat="server" ControlToValidate="txtYEARS_INSU_WOL" Display="Dynamic"></asp:regularexpressionvalidator>--%>
									<%--<asp:CompareValidator ID="cmpYEARS_INSU_WOL" ControlToCompare="txtYEARS_INSU" ControlToValidate="txtYEARS_INSU_WOL"
										Runat="server" Display="Dynamic" Operator="LessThanEqual"></asp:CompareValidator>--%>
								</td>
							</tr>-->
							<tr>
								<TD class="midcolora"><asp:label id="capMULTI_POLICY_DISC_APPLIED_PP_DESC" runat="server"></asp:label>
								</TD>
								<TD class="midcolora"><asp:dropdownlist id="cmbMULTI_POLICY_DISC_APPLIED" onfocus="SelectComboIndex('cmbMULTI_POLICY_DISC_APPLIED')"
										runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtMULTI_POLICY_DISC_APPLIED_PP_DESC'),document.getElementById('lblMULTIPOLICY_DISC_DESC'));"></asp:dropdownlist><br>
									</TD>
								<td class="midcolora"><asp:label id="capMULTI_POLICY_DISC_APPLIED_DESC" runat="server">Multipolicy Discount Description</asp:label><span class="mandatory" id="spnMULTI_POLICY_DISC_APPLIED_PP_DESC">*</span></td>
								<td class="midcolora"><asp:textbox id="txtMULTI_POLICY_DISC_APPLIED_PP_DESC" runat="server" size="28" MaxLength="50"></asp:textbox><asp:label id="lblMULTIPOLICY_DISC_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									<asp:requiredfieldvalidator id="rfvMULTI_POLICY_DISC_APPLIED_PP_DESC" runat="server" ControlToValidate="txtMULTI_POLICY_DISC_APPLIED_PP_DESC"
										Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<td class="midcolora"><asp:label id="capCURR_RES_TYPE" runat="server">Current Residence is Owned or Rented?</asp:label></td>
								<td class="midcolora" colSpan="3"><asp:dropdownlist id="cmbCURR_RES_TYPE" runat="server">
										<asp:ListItem Value=''></asp:ListItem>
										<asp:ListItem Value="Owned">Owned</asp:ListItem>
										<asp:ListItem Value="Rented">Rented</asp:ListItem>
									</asp:dropdownlist></td>
							</tr>
							<tr>
								<td class="midcolora"><asp:label id="capIS_OTHER_THAN_INSURED" runat="server">Any other licensed drivers in the household that are not listed or rated on this policy</asp:label></td>
								<td class="midcolora" colSpan="3"><asp:dropdownlist id="cmbIS_OTHER_THAN_INSURED" runat="server"></asp:dropdownlist></td>
							</tr>
							<tr id="trHeaderFullName">
								<td class="midcolora"><asp:label id="capFull_Name" runat="server">Full Name</asp:label><span class="mandatory" id="spnFullName">*</span></td>
								<td class="midcolora"><asp:textbox id="txtFullName" Runat="server"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvFullName" Runat="server" ControlToValidate="txtFullName" Display="Dynamic"></asp:requiredfieldvalidator></td>
								<td class="midcolora"><asp:label id="capDOB" runat="server">Date of Birth</asp:label><span class="mandatory" id="spnDATE_OF_BIRTH">*</span></td>
								<TD class="midcolora"><asp:textbox id="txtDATE_OF_BIRTH" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkDATE_OF_BIRTH" runat="server" CssClass="HotSpot">
										<asp:image id="imgDATE_OF_BIRTH" runat="server" ImageUrl="../../../cmsweb/Images/CalendarPicker.gif"></asp:image>
									</asp:hyperlink><BR>
									<asp:requiredfieldvalidator id="rfvDATE_OF_BIRTH" runat="server" ControlToValidate="txtDATE_OF_BIRTH" ErrorMessage="Date of Birth can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revDATE_OF_BIRTH" runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvDATE_OF_BIRTH" Runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic"
										ClientValidationFunction="ChkDateOfBirth"></asp:customvalidator></TD>
							</tr>
							<tr id="trHeaderDriving">
								<td class="midcolora"><asp:label id="capDrivingLisence" runat="server">Driving License #</asp:label></td>
								<td class="midcolora"><asp:textbox id="txtDrivingLisence" Runat="server"></asp:textbox></td>
								<td class="midcolora" colSpan="2"></td>
							</tr>
							<tr id="trInsuredElse">
								<td class="midcolora"><asp:label id="capInsuredElsewhere" runat="server">Insured elsewhere</asp:label></td>
								<TD class="midcolora"><asp:dropdownlist id="cmbInsuredElseWhere" Runat="server"></asp:dropdownlist></TD>
								<td class="midcolora" colSpan="2"></td>
							</tr>
							<tr id="trCompanyName">
								<td class="midcolora"><asp:label id="capCompanyName" runat="server">Company Name</asp:label><span class="mandatory" id="spnCompanyName">*</span></td>
								<td class="midcolora"><asp:textbox id="txtCompanyName" Runat="server"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvCompnayName" Runat="server" ControlToValidate="txtCompanyName" Display="Dynamic"></asp:requiredfieldvalidator></td>
								<td class="midcolora"><asp:label id="capPolicyNumber" runat="server">Policy Number</asp:label><span class="mandatory" id="spnPolicyNumber">*</span></td>
								<TD class="midcolora"><asp:textbox id="txtPolicyNumber" Runat="server"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvPolicyNumber" Runat="server" ControlToValidate="txtPolicyNumber" Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
									<TD class="midcolora"><asp:label id="capANY_PRIOR_LOSSES" runat="server">ANY_PRIOR_LOSSES</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora"><asp:dropdownlist id="cmbANY_PRIOR_LOSSES" onfocus="SelectComboIndex('cmbANY_PRIOR_LOSSES')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_PRIOR_LOSSES_DESC'),document.getElementById('lblPRIOR_LOSSES'));"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvANY_PRIOR_LOSSES" runat="server" ControlToValidate="cmbANY_PRIOR_LOSSES" ErrorMessage="ANY_PRIOR_LOSSES can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								
									<td class="midcolora"><asp:label id="capANY_PRIOR_LOSSES_DESC" runat="server">ANY_PRIOR_LOSSES_DESC</asp:label><span class="mandatory" id="spnANY_PRIOR_LOSSES_DESC">*</span></td>
									<td class="midcolora"><asp:textbox id="txtANY_PRIOR_LOSSES_DESC" runat="server" size="28" MaxLength="50"></asp:textbox>
									<asp:label id="lblPRIOR_LOSSES" runat="server" CssClass="LabelFont">N.A.</asp:label>
										<asp:requiredfieldvalidator id="rfvANY_PRIOR_LOSSES_DESC" runat="server" ControlToValidate="txtANY_PRIOR_LOSSES_DESC"
											Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
									<td class="midcolora" width="32%"><asp:label id="capYEARS_INSU" runat="server">Years Continuously Insured</asp:label></td>
									<td class="midcolora" width="18%"><asp:textbox id="txtYEARS_INSU" runat="server" size="3" maxlength="3"></asp:textbox><br>
										<asp:RangeValidator ID="rngYEARS_INSU" Type="Integer" Runat="server" ControlToValidate="txtYEARS_INSU"
										Display="Dynamic" MinimumValue="0" MaximumValue="999"></asp:RangeValidator>
										<asp:regularexpressionvalidator id="revYEARS_INSU" runat="server" Enabled="False" ControlToValidate="txtYEARS_INSU" Display="Dynamic"></asp:regularexpressionvalidator></td>
									<td class="midcolora" width="32%"><asp:label id="capYEARS_INSU_WOL" runat="server"></asp:label></td>
									<td class="midcolora" width="18%"><asp:textbox id="txtYEARS_INSU_WOL" runat="server" size="3" maxlength="3"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revYEARS_INSU_WOL" Enabled="False" runat="server" ControlToValidate="txtYEARS_INSU_WOL" Display="Dynamic"></asp:regularexpressionvalidator>
										<asp:RangeValidator ID="rngYEARS_INSU_WOL" Type="Integer" Runat="server" ControlToValidate="txtYEARS_INSU_WOL"
										Display="Dynamic" MinimumValue="0" MaximumValue="999"></asp:RangeValidator>
										<asp:customvalidator id="csvYEARS_INSU_WOL" ControlToValidate="txtYEARS_INSU_WOL" Display="Dynamic" ClientValidationFunction="CompareAllWolYears"
											Runat="server"></asp:customvalidator></td>
							</tr>
							<tr>
								<td class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" CausesValidation="False"></cmsb:cmsbutton></td>
								<td class="midcolora" colSpan="2"></td>
								<td class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
							<tr>
								<td><INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
									<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
									<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
									<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</BODY>
</HTML>

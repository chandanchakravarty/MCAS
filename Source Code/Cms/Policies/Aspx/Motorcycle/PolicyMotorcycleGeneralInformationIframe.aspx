<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PolicyMotorcycleGeneralInformationIframe.aspx.cs" AutoEventWireup="false"  Inherits="Cms.Policies.Aspx.Motorcycle.PolicyMotorcycleGeneralInformationIframe" ValidateRequest="false"%>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="~/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>APP_AUTO_GEN_INFO</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
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
						
						//if (document.getElementById("rfv" + txtDesc.id.substring(3)).isvalid == false)
							//document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "inline";
					}
					
					//making the * sign visible					
					if (document.getElementById("spn" + txtDesc.id.substring(3)) != null)
					{
						document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "inline";
					}
					//ChangeColor();
										
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
			//document.getElementById('hidRowId').value	=	'New';
			//if(document.getElementById('trMessage')..display=="inline")
			document.getElementById('cmbANY_NON_OWNED_VEH').focus();
			document.getElementById('cmbANY_NON_OWNED_VEH').options.selectedIndex = 1;
			document.getElementById('txtANY_NON_OWNED_VEH_MC_DESC').value= ''; 
			document.getElementById('cmbEXISTING_DMG').options.selectedIndex = 1;
			document.getElementById('txtEXISTING_DMG_MC_DESC').value= ''; 
			document.getElementById('cmbANY_OTH_INSU_COMP').options.selectedIndex = 1;
			document.getElementById('txtANY_OTH_INSU_COMP_MC_DESC').value= ''; 
			document.getElementById('cmbDRIVER_SUS_REVOKED').options.selectedIndex = 1;
			document.getElementById('txtDRIVER_SUS_REVOKED_MC_DESC').value= ''; 
			document.getElementById('cmbPHY_MENTL_CHALLENGED').options.selectedIndex = 1;
			document.getElementById('txtPHY_MENTL_CHALLENGED_MC_DESC').value= ''; 
			document.getElementById('cmbANY_FINANCIAL_RESPONSIBILITY').options.selectedIndex = 1;
			document.getElementById('txtANY_FINANCIAL_RESPONSIBILITY_MC_DESC').value= ''; 
			document.getElementById('cmbINS_AGENCY_TRANSFER').options.selectedIndex = 1;
			document.getElementById('txtINS_AGENCY_TRANSFER_MC_DESC').value= ''; 
			document.getElementById('cmbCOVERAGE_DECLINED').options.selectedIndex = 1;
			document.getElementById('txtCOVERAGE_DECLINED_MC_DESC').value= ''; 
			document.getElementById('cmbAGENCY_VEH_INSPECTED').options.selectedIndex = 1;
			document.getElementById('txtAGENCY_VEH_INSPECTED_MC_DESC').value= ''; 
			document.getElementById('cmbSALVAGE_TITLE').options.selectedIndex = 1;
			document.getElementById('txtSALVAGE_TITLE_MC_DESC').value= ''; 
			document.getElementById('cmbIS_COMMERCIAL_USE').options.selectedIndex = 1;
			document.getElementById('txtIS_COMMERCIAL_USE_DESC').value= ''; 
			document.getElementById('cmbIS_USEDFOR_RACING').options.selectedIndex = 1;
			document.getElementById('txtIS_USEDFOR_RACING_DESC').value= ''; 
			//document.getElementById('cmbIS_COST_OVER_DEFINED_LIMIT').options.selectedIndex = 0;
			document.getElementById('cmbCURR_RES_TYPE').options.selectedIndex = 1;
			document.getElementById('txtIS_COST_OVER_DEFINED_LIMIT_DESC').value= ''; 
			document.getElementById('cmbIS_MORE_WHEELS').options.selectedIndex = 1;
			document.getElementById('txtIS_MORE_WHEELS_DESC').value= ''; 
			document.getElementById('cmbIS_EXTENDED_FORKS').options.selectedIndex = 1;
			document.getElementById('txtIS_EXTENDED_FORKS_DESC').value= ''; 
			document.getElementById('cmbIS_LICENSED_FOR_ROAD').options.selectedIndex = 1;
			document.getElementById('txtIS_LICENSED_FOR_ROAD_DESC').value= ''; 
			document.getElementById('cmbIS_MODIFIED_INCREASE_SPEED').options.selectedIndex = 1;
			document.getElementById('txtIS_MODIFIED_INCREASE_SPEED_DESC').value= ''; 
			document.getElementById('cmbIS_MODIFIED_KIT').options.selectedIndex = 1;
			document.getElementById('txtIS_MODIFIED_KIT_DESC').value= '';
			if(document.getElementById('cmbIS_TAKEN_OUT')!= null) 
			document.getElementById('cmbIS_TAKEN_OUT').options.selectedIndex = 1;
			if(document.getElementById('txtIS_TAKEN_OUT_DESC')!= null)
			document.getElementById('txtIS_TAKEN_OUT_DESC').value= ''; 
			document.getElementById('cmbIS_CONVICTED_CARELESS_DRIVE').options.selectedIndex = 1;
			document.getElementById('txtIS_CONVICTED_CARELESS_DRIVE_DESC').value= ''; 
			document.getElementById('cmbIS_CONVICTED_ACCIDENT').options.selectedIndex = 1;
			document.getElementById('txtIS_CONVICTED_ACCIDENT_DESC').value= ''; 
			document.getElementById('cmbMULTI_POLICY_DISC_APPLIED').options.selectedIndex = 1;
			document.getElementById('txtMULTI_POLICY_DISC_APPLIED_MC_DESC').value= ''; 
			document.getElementById('txtREMARKS').value= ''; 
			document.getElementById('cmbIS_OTHER_THAN_INSURED').options.selectedIndex = 0;
			document.getElementById('cmbCURR_RES_TYPE').options.selectedIndex = -1;
			document.getElementById('cmbANY_PRIOR_LOSSES').options.selectedIndex = 0;
			document.getElementById('cmbAPPLY_PERS_UMB_POL').options.selectedIndex = 0;			
			document.getElementById('txtANY_PRIOR_LOSSES_DESC').value= ''; 
			document.getElementById('txtAPPLY_PERS_UMB_POL_DESC').value= ''; 
			document.getElementById('txtIS_CONVICTED_ACCIDENT_DESC').value= ''; 
			

		}
		function populateXML()
		{
			
			var tempXML;
			tempXML=document.getElementById('hidOldData').value;
			
			//if(document.getElementById('hidFormSaved').value == '0')
			if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
			{	
				
				if(tempXML!="")
				{	
		          document.getElementById('txtFullName').value='';
		          document.getElementById('txtDATE_OF_BIRTH').value='';
		          document.getElementById('txtDrivingLisence').value='';
		          document.getElementById('cmbWhichCycle').options.selectedIndex = -1;
									
					populateFormData(tempXML,APP_AUTO_GEN_INFO);
					/*if(document.getElementById('hidAmount').value=="1") //Yes case					
						document.getElementById('cmbIS_COST_OVER_DEFINED_LIMIT').options.selectedIndex=1;
					else
						document.getElementById('cmbIS_COST_OVER_DEFINED_LIMIT').options.selectedIndex=0;
					*/
					
				}
				else
				{
					AddData();
				}
			}
			ResetControls();
			ChangeInsuredListed();
			return false;
		}																								 
		
		function ResetControls()
		{
			EnableDisableDesc(document.getElementById('cmbANY_NON_OWNED_VEH'),document.getElementById('txtANY_NON_OWNED_VEH_MC_DESC'),document.getElementById('lblANY_NON_OWNED_VEH_MC_DESC'));
			EnableDisableDesc(document.getElementById('cmbEXISTING_DMG'),document.getElementById('txtEXISTING_DMG_MC_DESC'),document.getElementById('lblEXISTING_DMG_MC_DESC'));
			EnableDisableDesc(document.getElementById('cmbANY_OTH_INSU_COMP'),document.getElementById('txtANY_OTH_INSU_COMP_MC_DESC'),document.getElementById('lblANY_OTH_INSU_COMP_MC_DESC'));
			EnableDisableDesc(document.getElementById('cmbDRIVER_SUS_REVOKED'),document.getElementById('txtDRIVER_SUS_REVOKED_MC_DESC'),document.getElementById('lblDRIVER_SUS_REVOKED_MC_DESC'));
			EnableDisableDesc(document.getElementById('cmbPHY_MENTL_CHALLENGED'),document.getElementById('txtPHY_MENTL_CHALLENGED_MC_DESC'),document.getElementById('lblPHY_MENTL_CHALLENGED_MC_DESC'));
			EnableDisableDesc(document.getElementById('cmbANY_FINANCIAL_RESPONSIBILITY'),document.getElementById('txtANY_FINANCIAL_RESPONSIBILITY_MC_DESC'),document.getElementById('lblANY_FINANCIAL_RESPONSIBILITY_MC_DESC'));
			EnableDisableDesc(document.getElementById('cmbINS_AGENCY_TRANSFER'),document.getElementById('txtINS_AGENCY_TRANSFER_MC_DESC'),document.getElementById('lblINS_AGENCY_TRANSFER_MC_DESC'));			
			EnableDisableDesc(document.getElementById('cmbCOVERAGE_DECLINED'),document.getElementById('txtCOVERAGE_DECLINED_MC_DESC'),document.getElementById('lblCOVERAGE_DECLINED_MC_DESC'));
			EnableDisableDesc(document.getElementById('cmbAGENCY_VEH_INSPECTED'),document.getElementById('txtAGENCY_VEH_INSPECTED_MC_DESC'),document.getElementById('lblAGENCY_VEH_INSPECTED_MC_DESC'));
			EnableDisableDesc(document.getElementById('cmbSALVAGE_TITLE'),document.getElementById('txtSALVAGE_TITLE_MC_DESC'),document.getElementById('lblSALVAGE_TITLE_MC_DESC'));
			EnableDisableDesc(document.getElementById('cmbIS_COMMERCIAL_USE'),document.getElementById('txtIS_COMMERCIAL_USE_DESC'),document.getElementById('lblIS_COMMERCIAL_USE_DESC'));
			EnableDisableDesc(document.getElementById('cmbIS_USEDFOR_RACING'),document.getElementById('txtIS_USEDFOR_RACING_DESC'),document.getElementById('lblIS_USEDFOR_RACING_DESC'));
			EnableDisableDesc(document.getElementById('cmbIS_COST_OVER_DEFINED_LIMIT'),document.getElementById('txtIS_COST_OVER_DEFINED_LIMIT_DESC'),document.getElementById('lblIS_COST_OVER_DEFINED_LIMIT_DESC'));
			EnableDisableDesc(document.getElementById('cmbIS_MORE_WHEELS'),document.getElementById('txtIS_MORE_WHEELS_DESC'),document.getElementById('lblIS_MORE_WHEELS_DESC'));
			EnableDisableDesc(document.getElementById('cmbIS_EXTENDED_FORKS'),document.getElementById('txtIS_EXTENDED_FORKS_DESC'),document.getElementById('lblIS_EXTENDED_FORKS_DESC'));
			EnableDisableDesc(document.getElementById('cmbIS_LICENSED_FOR_ROAD'),document.getElementById('txtIS_LICENSED_FOR_ROAD_DESC'),document.getElementById('lblIS_LICENSED_FOR_ROAD_DESC'));
			
			EnableDisableDesc(document.getElementById('cmbIS_MODIFIED_INCREASE_SPEED'),document.getElementById('txtIS_MODIFIED_INCREASE_SPEED_DESC'),document.getElementById('lblIS_MODIFIED_INCREASE_SPEED_DESC'));
			EnableDisableDesc(document.getElementById('cmbIS_MODIFIED_KIT'),document.getElementById('txtIS_MODIFIED_KIT_DESC'),document.getElementById('lblIS_MODIFIED_KIT_DESC'));
			if(document.getElementById('cmbIS_TAKEN_OUT')!= null)
			EnableDisableDesc(document.getElementById('cmbIS_TAKEN_OUT'),document.getElementById('txtIS_TAKEN_OUT_DESC'),document.getElementById('lblIS_TAKEN_OUT_DESC'));
			
			EnableDisableDesc(document.getElementById('cmbIS_CONVICTED_CARELESS_DRIVE'),document.getElementById('txtIS_CONVICTED_CARELESS_DRIVE_DESC'),document.getElementById('lblIS_CONVICTED_CARELESS_DRIVE_DESC'));
			EnableDisableDesc(document.getElementById('cmbIS_CONVICTED_ACCIDENT'),document.getElementById('txtIS_CONVICTED_ACCIDENT_DESC'),document.getElementById('lblIS_CONVICTED_ACCIDENT_DESC'));
			EnableDisableDesc(document.getElementById('cmbMULTI_POLICY_DISC_APPLIED'),document.getElementById('txtMULTI_POLICY_DISC_APPLIED_MC_DESC'),document.getElementById('lblMULTI_POLICY_DISC_APPLIED_MC_DESC'));
			EnableDisableDesc(document.getElementById('cmbANY_PRIOR_LOSSES'),document.getElementById('txtANY_PRIOR_LOSSES_DESC'),document.getElementById('lblANY_PRIOR_LOSSES_DESC'));
			EnableDisableDesc(document.getElementById('cmbAPPLY_PERS_UMB_POL'),document.getElementById('txtAPPLY_PERS_UMB_POL_DESC'),document.getElementById('lblAPPLY_PERS_UMB_POL_DESC'));			
			ChangeInsuredListed1();
		}
		
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
		function ChkTextAreaLength(source, arguments)
		{
			var txtArea = arguments.Value;
			if(txtArea.length > 255 ) 
			{
				arguments.IsValid = false;
				return;   // invalid userName
			}
		}
		function ChkDOB(objSource , objArgs)
		{
			if(document.getElementById("revDOB").isvalid==true)
			{
				var effdate=document.APP_AUTO_GEN_INFO.txtDATE_OF_BIRTH.value;
				objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
			}
			else
		     	objArgs.IsValid = true;	
		}		
		function ChangeInsuredListed1()
		{
			if (document.getElementById('cmbIS_OTHER_THAN_INSURED').options.selectedIndex ==1 || document.getElementById('cmbIS_OTHER_THAN_INSURED').options.selectedIndex ==0 )
			{				
				document.getElementById("trHeader1").style.display = "none";
				document.getElementById("trHeader2").style.display = "none";
				document.getElementById("rfvDOB").setAttribute('enabled',true); 
				document.getElementById("rfvDOB").setAttribute('isValid',true); 
				document.getElementById("rfvFULL_NAME").setAttribute('enabled',true); 
				document.getElementById("rfvFULL_NAME").setAttribute('isValid',true); 

				
			
			}
			else if (document.getElementById('cmbIS_OTHER_THAN_INSURED').options.selectedIndex ==2)
			{
				
				document.getElementById("trHeader1").style.display = "inline";
				document.getElementById("trHeader2").style.display = "inline";
				document.getElementById("rfvDOB").setAttribute('isValid',false); 
				document.getElementById("rfvDOB").style.display='none'; 
				document.getElementById("rfvDOB").setAttribute('enabled',false); 
document.getElementById("rfvFULL_NAME").setAttribute('isValid',false); 
document.getElementById("rfvFULL_NAME").style.display='none'; 
document.getElementById("rfvFULL_NAME").setAttribute('enabled',false); 

			}
			
		}	
	function ChangeInsuredListed()
	 { 
//alert(document.getElementById('cmbIS_OTHER_THAN_INSURED').options[document.getElementById('cmbIS_OTHER_THAN_INSURED').selectedIndex].value); 
if(document.getElementById('cmbIS_OTHER_THAN_INSURED').options[document.getElementById('cmbIS_OTHER_THAN_INSURED').selectedIndex].value=='1') 
{	
		//document.getElementById('txtFullName').value='';
		//document.getElementById('txtDATE_OF_BIRTH').value='';
		//document.getElementById('txtDrivingLisence').value='';
		//document.getElementById('cmbWhichCycle').options.selectedIndex = 1;
		document.getElementById('trHeader1').style.display ='inline'; 
		document.getElementById('trHeader2').style.display ='inline'; 
	//document.getElementById('trCompanyName').style.display ='inline'; 
		document.getElementById("rfvDOB").setAttribute('enabled',true); 
		document.getElementById("rfvDOB").setAttribute('isValid',true); 
		document.getElementById("rfvFULL_NAME").setAttribute('enabled',true); 
		document.getElementById("rfvFULL_NAME").setAttribute('isValid',true); } 
else { 
		document.getElementById('trHeader1').style.display ='none'; 
		document.getElementById('trHeader2').style.display ='none'; 
		//document.getElementById('trCompanyName').style.display ='none'; 
		document.getElementById("rfvDOB").setAttribute('isValid',false); 
		document.getElementById("rfvDOB").style.display='none'; 
		document.getElementById("rfvDOB").setAttribute('enabled',false); 
		document.getElementById("rfvFULL_NAME").setAttribute('isValid',false); 
		document.getElementById("rfvFULL_NAME").style.display='none'; 
		document.getElementById("rfvFULL_NAME").setAttribute('enabled',false); 
 } 
}
		</script>
		<script id="clientEventHandlersJS" language="javascript">
<!--

function hidPolicyID_onbeforeeditfocus() {

}

//-->
		</script>
</HEAD>
	<BODY class="bodyBackGround" leftMargin="0" topMargin="0" onload="ApplyColor();populateXML();ChangeColor();">
		<div class="pageContent" id="bodyHeight">
			<FORM id="APP_AUTO_GEN_INFO" method="post" runat="server">
				<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
					<tr id="messageID" runat="server">
						<td align="center" colSpan="3"><asp:label id="capMessage" Visible="False" CssClass="errmsg" Runat="server"></asp:label></td>
					</tr>
					<tr id="trMessage" runat="server">
						<TD>
							<TABLE class="tableWidthHeader" id="mainTable" align="center" border="0">
								<tr>
									<TD class="pageHeader" colSpan="4"><webcontrol:workflow id="myWorkFlow"  istop="false" runat="server"></webcontrol:workflow></TD>
								</tr>
								<!-- <tr>
									<TD class="pageHeader" id="tdClientTop" colSpan="4"><webcontrol:clienttop id="cltClientTop" runat="server" width="100%"></webcontrol:clienttop><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></TD>
								</tr>
								<tr>
									<td class="headereffectCenter" colSpan="4">Underwriting Questions</td>
								</tr> -->
								<tr>
									<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
										mandatory</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capANY_NON_OWNED_VEH" runat="server">Non</asp:label><span id="spnANY_NON_OWNED_VEH" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbANY_NON_OWNED_VEH" onfocus="SelectComboIndex('cmbANY_NON_OWNED_VEH')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_NON_OWNED_VEH_MC_DESC'),document.getElementById('lblANY_NON_OWNED_VEH_MC_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvANY_NON_OWNED_VEH" runat="server" ControlToValidate="cmbANY_NON_OWNED_VEH"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capANY_NON_OWNED_VEH_MC_DESC" runat="server">Vehicle Description</asp:label><span id="spnANY_NON_OWNED_VEH_MC_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtANY_NON_OWNED_VEH_MC_DESC" runat="server"
											MaxLength="50" size="28"></asp:textbox><br>
										<asp:label id="lblANY_NON_OWNED_VEH_MC_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvANY_NON_OWNED_VEH_MC_DESC" runat="server" ControlToValidate="txtANY_NON_OWNED_VEH_MC_DESC"
											Display="Dynamic"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capEXISTING_DMG" runat="server">Dmg</asp:label><span id="spnEXISTING_DMG" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbEXISTING_DMG" onfocus="SelectComboIndex('cmbEXISTING_DMG')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtEXISTING_DMG_MC_DESC'),document.getElementById('lblEXISTING_DMG_MC_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvEXISTING_DMG" runat="server" ControlToValidate="cmbEXISTING_DMG" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capEXISTING_DMG_MC_DESC" runat="server">Damaged Description</asp:label><span id="spnEXISTING_DMG_MC_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtEXISTING_DMG_MC_DESC" runat="server" MaxLength="50"
											size="28"></asp:textbox><br>
										<asp:RequiredFieldValidator id="rfvEXISTING_DMG_MC_DESC" runat="server" ControlToValidate="txtEXISTING_DMG_MC_DESC"
											Display="Dynamic"></asp:RequiredFieldValidator>
										<asp:label id="lblEXISTING_DMG_MC_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
									</td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capANY_OTH_INSU_COMP" runat="server">Oth</asp:label><span id="spnANY_OTH_INSU_COMP" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbANY_OTH_INSU_COMP" onfocus="SelectComboIndex('cmbANY_OTH_INSU_COMP')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_OTH_INSU_COMP_MC_DESC'),document.getElementById('lblANY_OTH_INSU_COMP_MC_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvANY_OTH_INSU_COMP" runat="server" ControlToValidate="cmbANY_OTH_INSU_COMP"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capANY_OTH_INSU_COMP_MC_DESC" runat="server">Auto Insurance Description</asp:label><span id="spnANY_OTH_INSU_COMP_MC_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtANY_OTH_INSU_COMP_MC_DESC" runat="server"
											MaxLength="50" size="28"></asp:textbox><br>
										<asp:label id="lblANY_OTH_INSU_COMP_MC_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvANY_OTH_INSU_COMP_MC_DESC" runat="server" ControlToValidate="txtANY_OTH_INSU_COMP_MC_DESC"
											Display="Dynamic"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capDRIVER_SUS_REVOKED" runat="server">Sus</asp:label><span id="spnDRIVER_SUS_REVOKED" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbDRIVER_SUS_REVOKED" onfocus="SelectComboIndex('cmbDRIVER_SUS_REVOKED')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDRIVER_SUS_REVOKED_MC_DESC'),document.getElementById('lblDRIVER_SUS_REVOKED_MC_DESC'));"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_SUS_REVOKED" runat="server" ControlToValidate="cmbDRIVER_SUS_REVOKED"
											ErrorMessage="DRIVER_SUS_REVOKED can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capDRIVER_SUS_REVOKED_MC_DESC" runat="server">Driver License Description</asp:label><span id="spnDRIVER_SUS_REVOKED_MC_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtDRIVER_SUS_REVOKED_MC_DESC" runat="server"
											MaxLength="50" size="28"></asp:textbox><br>
										<asp:label id="lblDRIVER_SUS_REVOKED_MC_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvDRIVER_SUS_REVOKED_MC_DESC" runat="server" ControlToValidate="txtDRIVER_SUS_REVOKED_MC_DESC"
											Display="Dynamic"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capPHY_MENTL_CHALLENGED" runat="server">Mentl</asp:label><span id="spnPHY_MENTL_CHALLENGED" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbPHY_MENTL_CHALLENGED" onfocus="SelectComboIndex('cmbPHY_MENTL_CHALLENGED')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtPHY_MENTL_CHALLENGED_MC_DESC'),document.getElementById('lblPHY_MENTL_CHALLENGED_MC_DESC'));"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvPHY_MENTL_CHALLENGED" runat="server" ControlToValidate="cmbPHY_MENTL_CHALLENGED"
											ErrorMessage="PHY_MENTL_CHALLENGED can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capPHY_MENTL_CHALLENGED_MC_DESC" runat="server">Driver Impairment Description</asp:label><span id="spnPHY_MENTL_CHALLENGED_MC_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtPHY_MENTL_CHALLENGED_MC_DESC" runat="server"
											MaxLength="50" size="28"></asp:textbox><br>
										<asp:label id="lblPHY_MENTL_CHALLENGED_MC_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvPHY_MENTL_CHALLENGED_MC_DESC" runat="server" ControlToValidate="txtPHY_MENTL_CHALLENGED_MC_DESC"
											Display="Dynamic"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capANY_FINANCIAL_RESPONSIBILITY" runat="server">Financial</asp:label><span id="spnANY_FINANCIAL_RESPONSIBILITY" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbANY_FINANCIAL_RESPONSIBILITY" onfocus="SelectComboIndex('cmbANY_FINANCIAL_RESPONSIBILITY')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_FINANCIAL_RESPONSIBILITY_MC_DESC'),document.getElementById('lblANY_FINANCIAL_RESPONSIBILITY_MC_DESC'));"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvANY_FINANCIAL_RESPONSIBILITY" runat="server" ControlToValidate="cmbANY_FINANCIAL_RESPONSIBILITY"
											ErrorMessage="ANY_FINANCIAL_RESPONSIBILITY can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capANY_FINANCIAL_RESPONSIBILITY_MC_DESC" runat="server">Driver Number And Date Of Filing</asp:label><span id="spnANY_FINANCIAL_RESPONSIBILITY_MC_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtANY_FINANCIAL_RESPONSIBILITY_MC_DESC" runat="server"
											MaxLength="50"></asp:textbox><br>
										<asp:label id="lblANY_FINANCIAL_RESPONSIBILITY_MC_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvANY_FINANCIAL_RESPONSIBILITY_MC_DESC" runat="server" ControlToValidate="txtANY_FINANCIAL_RESPONSIBILITY_MC_DESC"
											Display="Dynamic"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capINS_AGENCY_TRANSFER" runat="server">Agency</asp:label><span id="spnINS_AGENCY_TRANSFER" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbINS_AGENCY_TRANSFER" onfocus="SelectComboIndex('cmbINS_AGENCY_TRANSFER')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtINS_AGENCY_TRANSFER_MC_DESC'),document.getElementById('lblINS_AGENCY_TRANSFER_MC_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvINS_AGENCY_TRANSFER" runat="server" ControlToValidate="cmbINS_AGENCY_TRANSFER"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capINS_AGENCY_TRANSFER_MC_DESC" runat="server">Insurance Transfer Description</asp:label><span id="spnINS_AGENCY_TRANSFER_MC_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtINS_AGENCY_TRANSFER_MC_DESC" runat="server"
											MaxLength="50" size="28"></asp:textbox><br>
										<asp:label id="lblINS_AGENCY_TRANSFER_MC_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvINS_AGENCY_TRANSFER_MC_DESC" runat="server" Display="Dynamic" ControlToValidate="txtINS_AGENCY_TRANSFER_MC_DESC"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capCOVERAGE_DECLINED" runat="server">Declined</asp:label><span id="spnCOVERAGE_DECLINED" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbCOVERAGE_DECLINED" onfocus="SelectComboIndex('cmbCOVERAGE_DECLINED')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtCOVERAGE_DECLINED_MC_DESC'),document.getElementById('lblCOVERAGE_DECLINED_MC_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvCOVERAGE_DECLINED" runat="server" ControlToValidate="cmbCOVERAGE_DECLINED"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capCOVERAGE_DECLINED_MC_DESC" runat="server">Coverage Description</asp:label><span id="spnCOVERAGE_DECLINED_MC_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtCOVERAGE_DECLINED_MC_DESC" runat="server"
											MaxLength="50" size="28"></asp:textbox><br>
										<asp:label id="lblCOVERAGE_DECLINED_MC_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvCOVERAGE_DECLINED_MC_DESC" runat="server" Display="Dynamic" ControlToValidate="txtCOVERAGE_DECLINED_MC_DESC"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capAGENCY_VEH_INSPECTED" runat="server">Veh</asp:label><span id="spnAGENCY_VEH_INSPECTED" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbAGENCY_VEH_INSPECTED" onfocus="SelectComboIndex('cmbAGENCY_VEH_INSPECTED')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtAGENCY_VEH_INSPECTED_MC_DESC'),document.getElementById('lblAGENCY_VEH_INSPECTED_MC_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvAGENCY_VEH_INSPECTED" runat="server" ControlToValidate="cmbAGENCY_VEH_INSPECTED"
											Display="Dynamic" Width="152px"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capAGENCY_VEH_INSPECTED_MC_DESC" runat="server">Agent Inspection Description</asp:label><span id="spnAGENCY_VEH_INSPECTED_MC_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtAGENCY_VEH_INSPECTED_MC_DESC" runat="server"
											MaxLength="50" size="28"></asp:textbox><br>
										<asp:label id="lblAGENCY_VEH_INSPECTED_MC_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvAGENCY_VEH_INSPECTED_MC_DESC" runat="server" ControlToValidate="txtAGENCY_VEH_INSPECTED_MC_DESC"
											Display="Dynamic"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capSALVAGE_TITLE" runat="server">Title</asp:label><span id="spnSALVAGE_TITLE" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbSALVAGE_TITLE" onfocus="SelectComboIndex('cmbSALVAGE_TITLE')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtSALVAGE_TITLE_MC_DESC'),document.getElementById('lblSALVAGE_TITLE_MC_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvSALVAGE_TITLE" runat="server" ControlToValidate="cmbSALVAGE_TITLE" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capSALVAGE_TITLE_MC_DESC" runat="server">Salvage Title Description</asp:label><span id="spnSALVAGE_TITLE_MC_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtSALVAGE_TITLE_MC_DESC" runat="server" MaxLength="50"
											size="28"></asp:textbox><br>
										<asp:label id="lblSALVAGE_TITLE_MC_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvSALVAGE_TITLE_MC_DESC" runat="server" Display="Dynamic" ControlToValidate="txtSALVAGE_TITLE_MC_DESC"
											DESIGNTIMEDRAGDROP="634"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capIS_COMMERCIAL_USE" runat="server">Commercial</asp:label><span id="spnIS_COMMERCIAL_USE" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbIS_COMMERCIAL_USE" onfocus="SelectComboIndex('cmbIS_COMMERCIAL_USE')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtIS_COMMERCIAL_USE_DESC'),document.getElementById('lblIS_COMMERCIAL_USE_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvIS_COMMERCIAL_USE" runat="server" ControlToValidate="cmbIS_COMMERCIAL_USE"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capIS_COMMERCIAL_USE_DESC" runat="server">Comercial Use Description</asp:label><span id="spnIS_COMMERCIAL_USE_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtIS_COMMERCIAL_USE_DESC" runat="server" MaxLength="50"
											size="28"></asp:textbox><br>
										<asp:label id="lblIS_COMMERCIAL_USE_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvIS_COMMERCIAL_USE_DESC" runat="server" Display="Dynamic" ControlToValidate="txtIS_COMMERCIAL_USE_DESC"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capIS_USEDFOR_RACING" runat="server">Usedfor</asp:label><span id="spnIS_USEDFOR_RACING" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbIS_USEDFOR_RACING" onfocus="SelectComboIndex('cmbIS_USEDFOR_RACING')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtIS_USEDFOR_RACING_DESC'),document.getElementById('lblIS_USEDFOR_RACING_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvIS_USEDFOR_RACING" runat="server" ControlToValidate="cmbIS_USEDFOR_RACING"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capIS_USEDFOR_RACING_DESC" runat="server">Racing Description</asp:label><span id="spnIS_USEDFOR_RACING_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtIS_USEDFOR_RACING_DESC" runat="server" MaxLength="50"
											size="28"></asp:textbox><br>
										<asp:label id="lblIS_USEDFOR_RACING_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvIS_USEDFOR_RACING_DESC" runat="server" Display="Dynamic" ControlToValidate="txtIS_USEDFOR_RACING_DESC"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capIS_COST_OVER_DEFINED_LIMIT" runat="server">Cost</asp:label><span id="spnIS_COST_OVER_DEFINED_LIMIT" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbIS_COST_OVER_DEFINED_LIMIT" onfocus="SelectComboIndex('cmbIS_COST_OVER_DEFINED_LIMIT')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtIS_COST_OVER_DEFINED_LIMIT_DESC'),document.getElementById('lblIS_COST_OVER_DEFINED_LIMIT_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvIS_COST_OVER_DEFINED_LIMIT" runat="server" ControlToValidate="cmbIS_COST_OVER_DEFINED_LIMIT"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capIS_COST_OVER_DEFINED_LIMIT_DESC" runat="server">Limit Description</asp:label><span id="spnIS_COST_OVER_DEFINED_LIMIT_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtIS_COST_OVER_DEFINED_LIMIT_DESC" runat="server"
											MaxLength="50" size="28"></asp:textbox><br>
										<asp:label id="lblIS_COST_OVER_DEFINED_LIMIT_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvIS_COST_OVER_DEFINED_LIMIT_DESC" runat="server" Display="Dynamic" ControlToValidate="txtIS_COST_OVER_DEFINED_LIMIT_DESC"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capIS_MORE_WHEELS" runat="server">More</asp:label><span id="spnIS_MORE_WHEELS" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbIS_MORE_WHEELS" onfocus="SelectComboIndex('cmbIS_MORE_WHEELS')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtIS_MORE_WHEELS_DESC'),document.getElementById('lblIS_MORE_WHEELS_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvIS_MORE_WHEELS" runat="server" ControlToValidate="cmbIS_MORE_WHEELS" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capIS_MORE_WHEELS_DESC" runat="server">Wheel Description</asp:label><span id="spnIS_MORE_WHEELS_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtIS_MORE_WHEELS_DESC" runat="server" MaxLength="50"
											size="28"></asp:textbox><br>
										<asp:label id="lblIS_MORE_WHEELS_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvIS_MORE_WHEELS_DESC" runat="server" Display="Dynamic" ControlToValidate="txtIS_MORE_WHEELS_DESC"
											DESIGNTIMEDRAGDROP="631"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capIS_EXTENDED_FORKS" runat="server">Extended</asp:label><span id="spnIS_EXTENDED_FORKS" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbIS_EXTENDED_FORKS" onfocus="SelectComboIndex('cmbIS_EXTENDED_FORKS')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtIS_EXTENDED_FORKS_DESC'),document.getElementById('lblIS_EXTENDED_FORKS_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvIS_EXTENDED_FORKS" runat="server" ControlToValidate="cmbIS_EXTENDED_FORKS"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capIS_EXTENDED_FORKS_DESC" runat="server">Forks Description</asp:label><span id="spnIS_EXTENDED_FORKS_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtIS_EXTENDED_FORKS_DESC" runat="server" MaxLength="50"
											size="28"></asp:textbox><br>
										<asp:label id="lblIS_EXTENDED_FORKS_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvIS_EXTENDED_FORKS_DESC" runat="server" Display="Dynamic" ControlToValidate="txtIS_EXTENDED_FORKS_DESC"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capIS_LICENSED_FOR_ROAD" runat="server">Licensed</asp:label><span id="spnIS_LICENSED_FOR_ROAD" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbIS_LICENSED_FOR_ROAD" onfocus="SelectComboIndex('cmbIS_LICENSED_FOR_ROAD')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtIS_LICENSED_FOR_ROAD_DESC'),document.getElementById('lblIS_LICENSED_FOR_ROAD_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvIS_LICENSED_FOR_ROAD" runat="server" ControlToValidate="cmbIS_LICENSED_FOR_ROAD"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capIS_LICENSED_FOR_ROAD_DESC" runat="server">Road License Description</asp:label><span id="spnIS_LICENSED_FOR_ROAD_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtIS_LICENSED_FOR_ROAD_DESC" runat="server"
											MaxLength="50" size="28"></asp:textbox><br>
										<asp:label id="lblIS_LICENSED_FOR_ROAD_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvIS_LICENSED_FOR_ROAD_DESC" runat="server" Display="Dynamic" ControlToValidate="txtIS_LICENSED_FOR_ROAD_DESC"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capIS_MODIFIED_INCREASE_SPEED" runat="server">Modified</asp:label><span id="spnIS_MODIFIED_INCREASE_SPEED" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbIS_MODIFIED_INCREASE_SPEED" onfocus="SelectComboIndex('cmbIS_MODIFIED_INCREASE_SPEED')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtIS_MODIFIED_INCREASE_SPEED_DESC'),document.getElementById('lblIS_MODIFIED_INCREASE_SPEED_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvIS_MODIFIED_INCREASE_SPEED" runat="server" ControlToValidate="cmbIS_MODIFIED_INCREASE_SPEED"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capIS_MODIFIED_INCREASE_SPEED_DESC" runat="server">Speed Description</asp:label><span id="spnIS_MODIFIED_INCREASE_SPEED_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtIS_MODIFIED_INCREASE_SPEED_DESC" runat="server"
											MaxLength="50" size="28"></asp:textbox><br>
										<asp:label id="lblIS_MODIFIED_INCREASE_SPEED_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvIS_MODIFIED_INCREASE_SPEED_DESC" runat="server" Display="Dynamic" ControlToValidate="txtIS_MODIFIED_INCREASE_SPEED_DESC"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capIS_MODIFIED_KIT" runat="server">Modified</asp:label><span id="spnIS_MODIFIED_KIT" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbIS_MODIFIED_KIT" onfocus="SelectComboIndex('cmbIS_MODIFIED_KIT')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtIS_MODIFIED_KIT_DESC'),document.getElementById('lblIS_MODIFIED_KIT_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvIS_MODIFIED_KIT" runat="server" ControlToValidate="cmbIS_MODIFIED_KIT" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capIS_MODIFIED_KIT_DESC" runat="server"></asp:label><span id="spnIS_MODIFIED_KIT_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtIS_MODIFIED_KIT_DESC" runat="server" MaxLength="50"
											size="28"></asp:textbox><br>
										<asp:label id="lblIS_MODIFIED_KIT_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvIS_MODIFIED_KIT_DESC" ControlToValidate="txtIS_MODIFIED_KIT_DESC" runat="server"
											Display="Dynamic"></asp:RequiredFieldValidator></td>
								</tr>
								<tr id="trIS_TAKEN_OUT" runat="server">
									<TD class="midcolora" width="32%"><asp:label id="capIS_TAKEN_OUT" runat="server">Taken</asp:label><span id="spnIS_TAKEN_OUT" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbIS_TAKEN_OUT" onfocus="SelectComboIndex('cmbIS_TAKEN_OUT')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtIS_TAKEN_OUT_DESC'),document.getElementById('lblIS_TAKEN_OUT_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvIS_TAKEN_OUT" runat="server" ControlToValidate="cmbIS_TAKEN_OUT" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capIS_TAKEN_OUT_DESC" runat="server">Taken Out Description</asp:label><span id="spnIS_TAKEN_OUT_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtIS_TAKEN_OUT_DESC" runat="server" MaxLength="50"
											size="28"></asp:textbox><br>
										<asp:label id="lblIS_TAKEN_OUT_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvIS_TAKEN_OUT_DESC" runat="server" Display="Dynamic" ControlToValidate="txtIS_TAKEN_OUT_DESC"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capIS_CONVICTED_CARELESS_DRIVE" runat="server">Convicted</asp:label><span id="spnIS_CONVICTED_CARELESS_DRIVE" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbIS_CONVICTED_CARELESS_DRIVE" onfocus="SelectComboIndex('cmbIS_CONVICTED_CARELESS_DRIVE')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtIS_CONVICTED_CARELESS_DRIVE_DESC'),document.getElementById('lblIS_CONVICTED_CARELESS_DRIVE_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvIS_CONVICTED_CARELESS_DRIVE" runat="server" ControlToValidate="cmbIS_CONVICTED_CARELESS_DRIVE"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capIS_CONVICTED_CARELESS_DRIVE_DESC" runat="server">Drive Description</asp:label><span id="spnIS_CONVICTED_CARELESS_DRIVE_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtIS_CONVICTED_CARELESS_DRIVE_DESC" runat="server"
											MaxLength="50" size="28"></asp:textbox><br>
										<asp:label id="lblIS_CONVICTED_CARELESS_DRIVE_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvIS_CONVICTED_CARELESS_DRIVE_DESC" runat="server" Display="Dynamic" ControlToValidate="txtIS_CONVICTED_CARELESS_DRIVE_DESC"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capIS_CONVICTED_ACCIDENT" runat="server">Convicted</asp:label><span id="spnIS_CONVICTED_ACCIDENT" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbIS_CONVICTED_ACCIDENT" onfocus="SelectComboIndex('cmbIS_CONVICTED_ACCIDENT')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtIS_CONVICTED_ACCIDENT_DESC'),document.getElementById('lblIS_CONVICTED_ACCIDENT_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvIS_CONVICTED_ACCIDENT" runat="server" ControlToValidate="cmbIS_CONVICTED_ACCIDENT"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capIS_CONVICTED_ACCIDENT_DESC" runat="server">Accident Description</asp:label><span id="spnIS_CONVICTED_ACCIDENT_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtIS_CONVICTED_ACCIDENT_DESC" runat="server"
											MaxLength="50" size="28"></asp:textbox><br>
										<asp:label id="lblIS_CONVICTED_ACCIDENT_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvIS_CONVICTED_ACCIDENT_DESC" runat="server" ControlToValidate="txtIS_CONVICTED_ACCIDENT_DESC"
											Display="Dynamic" DESIGNTIMEDRAGDROP="1127"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMULTI_POLICY_DISC_APPLIED" runat="server"></asp:label><span id="spnMULTI_POLICY_DISC_APPLIED" class="mandatory">*</span>
									</TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbMULTI_POLICY_DISC_APPLIED" onfocus="SelectComboIndex('cmbMULTI_POLICY_DISC_APPLIED')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtMULTI_POLICY_DISC_APPLIED_MC_DESC'),document.getElementById('lblMULTI_POLICY_DISC_APPLIED_MC_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvMULTI_POLICY_DISC_APPLIED" Runat="server" ControlToValidate="cmbMULTI_POLICY_DISC_APPLIED"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capMULTI_POLICY_DISC_APPLIED_MC_DESC" runat="server">Multipolicy Discount Description</asp:label><span id="spnMULTI_POLICY_DISC_APPLIED_MC_DESC" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtMULTI_POLICY_DISC_APPLIED_MC_DESC" runat="server"
											MaxLength="50" size="28"></asp:textbox><br>
										<asp:label id="lblMULTI_POLICY_DISC_APPLIED_MC_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator id="rfvMULTI_POLICY_DISC_APPLIED_MC_DESC" runat="server" Display="Dynamic" ControlToValidate="txtMULTI_POLICY_DISC_APPLIED_MC_DESC"></asp:RequiredFieldValidator></td>
								</tr>
								<tr>
									<td class="midcolora" width="32%"><asp:label id="capCURR_RES_TYPE" runat="server">Current Residence is Owned or Rented?</asp:label><span id="spnCURR_RES_TYPE" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:dropdownlist id="cmbCURR_RES_TYPE" runat="server">
											<ASP:LISTITEM Value=""></ASP:LISTITEM>
											<ASP:LISTITEM Value="Owned">Owned</ASP:LISTITEM>
											<ASP:LISTITEM Value="Rented">Rented</ASP:LISTITEM>
										</asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvCURR_RES_TYPE" runat="server" ControlToValidate="cmbCURR_RES_TYPE" Display="Dynamic"></asp:requiredfieldvalidator>
									</td>
									<td colspan="2" class="midcolora"></td>
								</tr>
								<tr>
									<td class="midcolora" width="32%"><asp:label id="capIS_OTHER_THAN_INSURED" runat="server">Anyone other than insured listed, living in household?</asp:label><span id="spnIS_OTHER_THAN_INSURED" class="mandatory">*</span></td>
									<td class="midcolora" width="18%"><asp:dropdownlist id="cmbIS_OTHER_THAN_INSURED" runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtIS_OTHER_THAN_INSURED_YES'),document.getElementById('lblOTHER_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvIS_OTHER_THAN_INSURED" runat="server" ControlToValidate="cmbIS_OTHER_THAN_INSURED"
											Display="Dynamic"></asp:requiredfieldvalidator></td>
									<td colspan="2" class="midcolora"></td>
								</tr>
								<tr id="trHeader1">
									<TD class="midcolora" width="32%">
										<asp:label id="capFULL_NAME" runat="server">Full Name</asp:label><span class="mandatory">*</span>
									</TD>
									<TD class="midcolora" width="18%">
										<asp:textbox id="txtFullName" runat="server" maxlength="50" size="30" EnableViewState="False"></asp:textbox>										
										<asp:requiredfieldvalidator id="rfvFULL_NAME" runat="server" Display="Dynamic" ControlToValidate="txtFullName"></asp:requiredfieldvalidator>
									</TD>
									<TD class="midcolora" width="32%">
										<asp:label id="capDOB" runat="server">Date of Birth</asp:label><span class="mandatory">*</span>
									</TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDATE_OF_BIRTH" runat="server" size="12" maxlength="10"></asp:textbox>
										<asp:hyperlink id="hlkDRIVER_DOB" runat="server" CssClass="HotSpot">
											<asp:image id="imgDRIVER_DOB" runat="server" ImageUrl="/Cms/cmsweb/Images/CalendarPicker.gif"></asp:image>
										</asp:hyperlink><BR>
										<asp:requiredfieldvalidator id="rfvDOB" runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revDOB" runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic"></asp:regularexpressionvalidator>
										<asp:customvalidator id="csvDOB" Runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic"
											ClientValidationFunction="ChkDOB"></asp:customvalidator></TD>
								</tr>
								<tr id="trHeader2">
									<TD class="midcolora" width="32%" style="HEIGHT: 23px">
										<asp:label id="capDRIV_LIC" runat="server">License Number</asp:label></TD>
									<TD class="midcolora" width="18%" style="HEIGHT: 23px">
										<asp:textbox id="txtDrivingLisence" runat="server" maxlength="30" size="30"></asp:textbox></TD>
									<TD class="midcolora" width="32%" style="HEIGHT: 23px">
										<asp:label id="capMOTORCYCLE" runat="server">Indicate which cycle</asp:label></TD>
									<TD class="midcolora" width="18%" style="HEIGHT: 23px"><asp:dropdownlist id="cmbWhichCycle" onfocus="SelectComboIndex('cmbWhichCycle')" runat="server"></asp:dropdownlist>
									</TD>
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
									<TD class="midcolora" colSpan="1"><asp:label id="capREMARKS" runat="server">Remarks</asp:label></TD>
									<TD class="midcolora" colSpan="3"><asp:textbox onkeypress="MaxLength(this,255);" id="txtREMARKS" runat="server" size="30" Columns="70"
											Rows="7" maxlength="255" TextMode="MultiLine"></asp:textbox><br>
										<asp:customvalidator id="csvREMARKS" Runat="server" ControlToValidate="txtREMARKS" Display="Dynamic"
											ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capANY_PRIOR_LOSSES" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbANY_PRIOR_LOSSES" onfocus="SelectComboIndex('cmbANY_PRIOR_LOSSES')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_PRIOR_LOSSES_DESC'),document.getElementById('lblANY_PRIOR_LOSSES_DESC'));"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvANY_PRIOR_LOSSES" runat="server" ControlToValidate="cmbANY_PRIOR_LOSSES" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="32%"><asp:label id="capANY_PRIOR_LOSSES_DESC" runat="server"></asp:label><span class="mandatory" id="spnANY_PRIOR_LOSSES_DESC">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtANY_PRIOR_LOSSES_DESC" runat="server" MaxLength="50"
											size="28"></asp:textbox><asp:label id="lblANY_PRIOR_LOSSES_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvANY_PRIOR_LOSSES_DESC" runat="server" ControlToValidate="txtANY_PRIOR_LOSSES_DESC"
											Display="Dynamic"></asp:requiredfieldvalidator></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capAPPLY_PERS_UMB_POL" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbAPPLY_PERS_UMB_POL" onfocus="SelectComboIndex('cmbAPPLY_PERS_UMB_POL')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtAPPLY_PERS_UMB_POL_DESC'),document.getElementById('lblAPPLY_PERS_UMB_POL_DESC'));"></asp:dropdownlist>
									</TD>
									<td class="midcolora" width="32%"><asp:label id="capAPPLY_PERS_UMB_POL_DESC" runat="server"></asp:label><span class="mandatory" id="spnAPPLY_PERS_UMB_POL_DESC">*</span></td>
									<td class="midcolora" width="18%"><asp:textbox onkeypress="MaxLength(this,50);" id="txtAPPLY_PERS_UMB_POL_DESC" runat="server" MaxLength="50"
											size="28"></asp:textbox><asp:label id="lblAPPLY_PERS_UMB_POL_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvAPPLY_PERS_UMB_POL_DESC" runat="server" ControlToValidate="txtAPPLY_PERS_UMB_POL_DESC"
											Display="Dynamic"></asp:requiredfieldvalidator></td>
								</tr>
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
							</TABLE>
						</TD>
					</tr>
				</TABLE>
				<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">				
				<INPUT id="hidIS_COST_OVER_DEFINED_LIMIT" type="hidden" value="0" name="hidIS_COST_OVER_DEFINED_LIMIT" runat="server">
				<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
				<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
				<INPUT id="hidAPP_ID" type="hidden" value="0" name="hidAPP_ID" runat="server"><INPUT id="hidAPP_VERSION_ID" type="hidden" value="0" name="hidAPP_VERSION_ID" runat="server">
				<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
				<input id="hidRowId" type="hidden" value="0" name="hidRowId" runat="server"> <INPUT id="hidPolicyID" type="hidden" value="0" name="hidPolicyID" runat="server" language="javascript"
					onbeforeeditfocus="return hidPolicyID_onbeforeeditfocus()"> <INPUT id="hidPolicyVersionID" type="hidden" value="0" name="hidPolicyVersionID" runat="server">
			</FORM>
		</div>
	</BODY>
</HTML>





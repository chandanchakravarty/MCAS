<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" Codebehind="PolicyGeneralInformation.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.GeneralLiability.PolicyGeneralInformation" validateRequest="false" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>POL_GENERAL_UNDERWRITING_INFO</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>		
		<script language="javascript">
function AddData()
{	
	
	document.getElementById('hidCUSTOMER_ID').value	=	'New';
	
	document.getElementById('cmbINSURANCE_DECLINED_FIVE_YEARS').focus();
	document.getElementById('cmbINSURANCE_DECLINED_FIVE_YEARS').options.selectedIndex = 1;
	document.getElementById('cmbMEDICAL_PROFESSIONAL_EMPLOYEED').options.selectedIndex = 1;
	document.getElementById('cmbEXPOSURE_RATIOACTIVE_NUCLEAR').options.selectedIndex = 1;
	document.getElementById('cmbHAVE_PAST_PRESENT_OPERATIONS').options.selectedIndex = 1;
	document.getElementById('cmbANY_OPERATIONS_SOLD').options.selectedIndex = 1;
	document.getElementById('cmbMACHINERY_LOANED').options.selectedIndex = 1;
	document.getElementById('cmbANY_WATERCRAFT_LEASED').options.selectedIndex = 1;
	document.getElementById('cmbANY_PARKING_OWNED').options.selectedIndex = 1;
	document.getElementById('cmbFEE_CHARGED_PARKING').options.selectedIndex = 1;
	document.getElementById('cmbRECREATION_PROVIDED').options.selectedIndex = 1;
	document.getElementById('cmbSWIMMING_POOL_PREMISES').options.selectedIndex = 1;
	document.getElementById('cmbSPORTING_EVENT_SPONSORED').options.selectedIndex = 1;
	document.getElementById('cmbSTRUCTURAL_ALTERATION_CONTEMPATED').options.selectedIndex = 1;
	document.getElementById('cmbDEMOLITION_EXPOSURE_CONTEMPLATED').options.selectedIndex = 1;
	document.getElementById('cmbCUSTOMER_ACTIVE_JOINT_VENTURES').options.selectedIndex = 1;
	document.getElementById('cmbLEASE_EMPLOYEE').options.selectedIndex = 1;
	document.getElementById('cmbLABOR_INTERCHANGE_OTH_BUSINESS').options.selectedIndex = 1;
	document.getElementById('cmbDAY_CARE_FACILITIES').options.selectedIndex = 1;
	document.getElementById('txtADDITIONAL_COMMENTS').value='';
	
	//-----------------Added by Mohit on 11/10/2005------------------.
		
		document.getElementById('txtDESC_INSURANCE_DECLINED').value='';
		document.getElementById('txtDESC_MEDICAL_PROFESSIONAL').value='';
		document.getElementById('txtDESC_EXPOSURE_RATIOACTIVE').value='';
		document.getElementById('txtDESC_HAVE_PAST_PRESENT').value='';
		document.getElementById('txtDESC_ANY_OPERATIONS').value='';
		document.getElementById('txtDESC_MACHINERY_LOANED').value='';
		document.getElementById('txtDESC_ANY_WATERCRAFT').value='';
		document.getElementById('txtDESC_ANY_PARKING').value='';
		document.getElementById('txtDESC_FEE_CHARGED').value='';
		document.getElementById('txtDESC_RECREATION_PROVIDED').value='';
		document.getElementById('txtDESC_SWIMMING_POOL').value='';
		document.getElementById('txtDESC_SPORTING_EVENT').value='';
		document.getElementById('txtDESC_STRUCTURAL_ALTERATION').value='';
		document.getElementById('txtDESC_DEMOLITION_EXPOSURE').value='';
		document.getElementById('txtDESC_CUSTOMER_ACTIVE').value='';
		document.getElementById('txtDESC_LEASE_EMPLOYEE').value='';
		document.getElementById('txtDESC_LABOR_INTERCHANGE').value='';
		document.getElementById('txtDESC_DAY_CARE').value='';
	
	
	//------------------------End------------------------------------.
	
	
	//document.POL_GENERAL_UNDERWRITING_INFO.reset();
	ChangeColor();
	DisableValidators();

}
function populateXML()
{
	
			if(document.getElementById('hidFormSaved').value == '0')
			{
				//alert(document.getElementById('hidOldData').value);
				if(document.getElementById('hidOldData').value!="")
				{
				
					//alert(document.getElementById('hidOldData').value);
					populateFormData(document.getElementById('hidOldData').value,POL_GENERAL_UNDERWRITING_INFO);
				}    
				else
				{
					AddData();
				}
			}
			//-----------------------Added by Mohit on 13/10/2005------------------.
			EnableDisableDesc(document.getElementById('cmbINSURANCE_DECLINED_FIVE_YEARS'),document.getElementById('txtDESC_INSURANCE_DECLINED'),document.getElementById('lblDESC_INSURANCE_DECLINED'));
			EnableDisableDesc(document.getElementById('cmbMEDICAL_PROFESSIONAL_EMPLOYEED'),document.getElementById('txtDESC_MEDICAL_PROFESSIONAL'),document.getElementById('lblDESC_MEDICAL_PROFESSIONAL'));
			EnableDisableDesc(document.getElementById('cmbEXPOSURE_RATIOACTIVE_NUCLEAR'),document.getElementById('txtDESC_EXPOSURE_RATIOACTIVE'),document.getElementById('lblDESC_EXPOSURE_RATIOACTIVE'));
			EnableDisableDesc(document.getElementById('cmbHAVE_PAST_PRESENT_OPERATIONS'),document.getElementById('txtDESC_HAVE_PAST_PRESENT'),document.getElementById('lblDESC_HAVE_PAST_PRESENT'));
			EnableDisableDesc(document.getElementById('cmbANY_OPERATIONS_SOLD'),document.getElementById('txtDESC_ANY_OPERATIONS'),document.getElementById('lblDESC_ANY_OPERATIONS'));
			EnableDisableDesc(document.getElementById('cmbMACHINERY_LOANED'),document.getElementById('txtDESC_MACHINERY_LOANED'),document.getElementById('lblDESC_MACHINERY_LOANED'));
			EnableDisableDesc(document.getElementById('cmbANY_WATERCRAFT_LEASED'),document.getElementById('txtDESC_ANY_WATERCRAFT'),document.getElementById('lblDESC_ANY_WATERCRAFT'));
			EnableDisableDesc(document.getElementById('cmbANY_PARKING_OWNED'),document.getElementById('txtDESC_ANY_PARKING'),document.getElementById('lblDESC_ANY_PARKING'));
			EnableDisableDesc(document.getElementById('cmbFEE_CHARGED_PARKING'),document.getElementById('txtDESC_FEE_CHARGED'),document.getElementById('lblDESC_FEE_CHARGED'));
			EnableDisableDesc(document.getElementById('cmbRECREATION_PROVIDED'),document.getElementById('txtDESC_RECREATION_PROVIDED'),document.getElementById('lblDESC_RECREATION_PROVIDED'));
			EnableDisableDesc(document.getElementById('cmbSWIMMING_POOL_PREMISES'),document.getElementById('txtDESC_SWIMMING_POOL'),document.getElementById('lblDESC_SWIMMING_POOL'));
			EnableDisableDesc(document.getElementById('cmbSPORTING_EVENT_SPONSORED'),document.getElementById('txtDESC_SPORTING_EVENT'),document.getElementById('lblDESC_SPORTING_EVENT'));
			EnableDisableDesc(document.getElementById('cmbSTRUCTURAL_ALTERATION_CONTEMPATED'),document.getElementById('txtDESC_STRUCTURAL_ALTERATION'),document.getElementById('lblDESC_STRUCTURAL_ALTERATION'));
			EnableDisableDesc(document.getElementById('cmbDEMOLITION_EXPOSURE_CONTEMPLATED'),document.getElementById('txtDESC_DEMOLITION_EXPOSURE'),document.getElementById('lblDESC_DEMOLITION_EXPOSURE'));
			EnableDisableDesc(document.getElementById('cmbCUSTOMER_ACTIVE_JOINT_VENTURES'),document.getElementById('txtDESC_CUSTOMER_ACTIVE'),document.getElementById('lblDESC_CUSTOMER_ACTIVE'));
			EnableDisableDesc(document.getElementById('cmbLEASE_EMPLOYEE'),document.getElementById('txtDESC_LEASE_EMPLOYEE'),document.getElementById('lblDESC_LEASE_EMPLOYEE'));
			EnableDisableDesc(document.getElementById('cmbLABOR_INTERCHANGE_OTH_BUSINESS'),document.getElementById('txtDESC_LABOR_INTERCHANGE'),document.getElementById('lblDESC_LABOR_INTERCHANGE'));
			EnableDisableDesc(document.getElementById('cmbDAY_CARE_FACILITIES'),document.getElementById('txtDESC_DAY_CARE'),document.getElementById('lblDESC_DAY_CARE'));
			
			
			//----------------------------End--------------------------------------.
			ChangeColor();
			return false;
			}

			function ChkTextAreaLength(source, arguments)
			{
				var txtArea = arguments.Value;
				if(txtArea.length > 200 ) 
				{
					arguments.IsValid = false;
					return;   // invalid userName
				}
			}
		// Added by mohit on 11/10/2005---------.	
		/*function EnableDisableDesc(cmbCombo, txtDesc,lblNA)
		{	
			
			if (cmbCombo.selectedIndex > -1)
			{	
				
				//Checking value only if item is selected
				if (cmbCombo.options[cmbCombo.selectedIndex].text == "Yes")
				{
					//Disabling the description field, if No is selected
					txtDesc.style.display = "inline";
					
					lblNA.style.display = "none";
				}
				else
				{
					//Enabling the description field, if yes is selected
					txtDesc.style.display = "none";
				
					lblNA.style.display = "inline";
					lblNA.innerHTML="NA";
				}
			}
			else
			{
				//Disabling the description field, if No is selected
				txtDesc.style.display = "none";
				lblNA.style.display = "inline";
				lblNA.innerHTML="NA";
			}
		}*/
		
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
			
		function ResetControls()
		{			
			EnableDisableDesc(document.getElementById('cmbINSURANCE_DECLINED_FIVE_YEARS'),document.getElementById('txtDESC_INSURANCE_DECLINED'),document.getElementById('lblDESC_INSURANCE_DECLINED'));
			EnableDisableDesc(document.getElementById('cmbMEDICAL_PROFESSIONAL_EMPLOYEED'),document.getElementById('txtDESC_MEDICAL_PROFESSIONAL'),document.getElementById('lblDESC_MEDICAL_PROFESSIONAL'));
			EnableDisableDesc(document.getElementById('cmbEXPOSURE_RATIOACTIVE_NUCLEAR'),document.getElementById('txtDESC_EXPOSURE_RATIOACTIVE'),document.getElementById('lblDESC_EXPOSURE_RATIOACTIVE'));
			EnableDisableDesc(document.getElementById('cmbHAVE_PAST_PRESENT_OPERATIONS'),document.getElementById('txtDESC_HAVE_PAST_PRESENT'),document.getElementById('lblDESC_HAVE_PAST_PRESENT'));
			EnableDisableDesc(document.getElementById('cmbANY_OPERATIONS_SOLD'),document.getElementById('txtDESC_ANY_OPERATIONS'),document.getElementById('lblDESC_ANY_OPERATIONS'));
			EnableDisableDesc(document.getElementById('cmbMACHINERY_LOANED'),document.getElementById('txtDESC_MACHINERY_LOANED'),document.getElementById('lblDESC_MACHINERY_LOANED'));
			EnableDisableDesc(document.getElementById('cmbANY_WATERCRAFT_LEASED'),document.getElementById('txtDESC_ANY_WATERCRAFT'),document.getElementById('lblDESC_ANY_WATERCRAFT'));
			EnableDisableDesc(document.getElementById('cmbANY_PARKING_OWNED'),document.getElementById('txtDESC_ANY_PARKING'),document.getElementById('lblDESC_ANY_PARKING'));
			EnableDisableDesc(document.getElementById('cmbFEE_CHARGED_PARKING'),document.getElementById('txtDESC_FEE_CHARGED'),document.getElementById('lblDESC_FEE_CHARGED'));
			EnableDisableDesc(document.getElementById('cmbRECREATION_PROVIDED'),document.getElementById('txtDESC_RECREATION_PROVIDED'),document.getElementById('lblDESC_RECREATION_PROVIDED'));
			EnableDisableDesc(document.getElementById('cmbSWIMMING_POOL_PREMISES'),document.getElementById('txtDESC_SWIMMING_POOL'),document.getElementById('lblDESC_SWIMMING_POOL'));
			EnableDisableDesc(document.getElementById('cmbSPORTING_EVENT_SPONSORED'),document.getElementById('txtDESC_SPORTING_EVENT'),document.getElementById('lblDESC_SPORTING_EVENT'));
			EnableDisableDesc(document.getElementById('cmbSTRUCTURAL_ALTERATION_CONTEMPATED'),document.getElementById('txtDESC_STRUCTURAL_ALTERATION'),document.getElementById('lblDESC_STRUCTURAL_ALTERATION'));
			EnableDisableDesc(document.getElementById('cmbDEMOLITION_EXPOSURE_CONTEMPLATED'),document.getElementById('txtDESC_DEMOLITION_EXPOSURE'),document.getElementById('lblDESC_DEMOLITION_EXPOSURE'));
			EnableDisableDesc(document.getElementById('cmbCUSTOMER_ACTIVE_JOINT_VENTURES'),document.getElementById('txtDESC_CUSTOMER_ACTIVE'),document.getElementById('lblDESC_CUSTOMER_ACTIVE'));
			EnableDisableDesc(document.getElementById('cmbLEASE_EMPLOYEE'),document.getElementById('txtDESC_LEASE_EMPLOYEE'),document.getElementById('lblDESC_LEASE_EMPLOYEE'));
			EnableDisableDesc(document.getElementById('cmbLABOR_INTERCHANGE_OTH_BUSINESS'),document.getElementById('txtDESC_LABOR_INTERCHANGE'),document.getElementById('lblDESC_LABOR_INTERCHANGE'));
			EnableDisableDesc(document.getElementById('cmbDAY_CARE_FACILITIES'),document.getElementById('txtDESC_DAY_CARE'),document.getElementById('lblDESC_DAY_CARE'));
			
		}				
		</script>
</HEAD>
	<BODY class="bodyBackGround" leftMargin="0" topMargin="0" onload="populateXML();top.topframe.main1.mousein = false;findMouseIn();showScroll();ApplyColor();">
		<!--Start: to add bottom menu--><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!--End: bottom menu-->
		<!--Start: to add band space -->
		<!--End: band space -->
		<div class="pageContent" id="bodyHeight">
			<FORM id="POL_GENERAL_UNDERWRITING_INFO" method="post" runat="server">
				<TABLE class="tableWidth" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD>
							<TABLE width="100%" align="center" border="0">
								<tr>
									<td class="pageHeader" id="tdWorkflow" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow></td>
								</tr>
								<tr>
									<TD class="pageHeader" id="tdClientTop" colSpan="4"><webcontrol:clienttop id="cltClientTop" runat="server" width="100%"></webcontrol:clienttop><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></TD>
								</tr>
								<tr>
									<td class="headereffectCenter" colSpan="4">Underwriting Questions</td>
								</tr>
								<tr>
									<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
										mandatory</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capINSURANCE_DECLINED_FIVE_YEARS" runat="server">Has insurance been declined, cancelled, non-pay cancelled or non-renewed in the last 5 years?</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbINSURANCE_DECLINED_FIVE_YEARS" onfocus="SelectComboIndex('cmbINSURANCE_DECLINED_FIVE_YEARS')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_INSURANCE_DECLINED'),document.getElementById('lblDESC_INSURANCE_DECLINED'));">
											<ASP:LISTITEM Value="Y">Yes</ASP:LISTITEM>
											<ASP:LISTITEM Value="N">No</ASP:LISTITEM>
										</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDESC_INSURANCE_DECLINED" runat="server"></asp:label><span id="spnDESC_INSURANCE_DECLINED" class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESC_INSURANCE_DECLINED" runat="server" maxlength="300" size="30"></asp:textbox><asp:label id="lblDESC_INSURANCE_DECLINED" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvDESC_INSURANCE_DECLINED" runat="server" ControlToValidate="txtDESC_INSURANCE_DECLINED"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capMEDICAL_PROFESSIONAL_EMPLOYEED" runat="server">Any medical facilities provided or medical professional employed or contracted?</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbMEDICAL_PROFESSIONAL_EMPLOYEED" onfocus="SelectComboIndex('cmbMEDICAL_PROFESSIONAL_EMPLOYEED')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_MEDICAL_PROFESSIONAL'),document.getElementById('lblDESC_MEDICAL_PROFESSIONAL'));">
											<ASP:LISTITEM Value="Y">Yes</ASP:LISTITEM>
											<ASP:LISTITEM Value="N">No</ASP:LISTITEM>
										</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDESC_MEDICAL_PROFESSIONAL" runat="server"></asp:label><span class="mandatory" id="spnDESC_MEDICAL_PROFESSIONAL">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESC_MEDICAL_PROFESSIONAL" runat="server" maxlength="300" size="30"></asp:textbox><asp:label id="lblDESC_MEDICAL_PROFESSIONAL" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvDESC_MEDICAL_PROFESSIONAL" runat="server" ControlToValidate="txtDESC_MEDICAL_PROFESSIONAL"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capEXPOSURE_RATIOACTIVE_NUCLEAR" runat="server">Any exposure to radioactive/nuclear materials?</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbEXPOSURE_RATIOACTIVE_NUCLEAR" onfocus="SelectComboIndex('cmbEXPOSURE_RATIOACTIVE_NUCLEAR')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_EXPOSURE_RATIOACTIVE'),document.getElementById('lblDESC_EXPOSURE_RATIOACTIVE'));">
											<ASP:LISTITEM Value="Y">Yes</ASP:LISTITEM>
											<ASP:LISTITEM Value="N">No</ASP:LISTITEM>
										</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDESC_EXPOSURE_RATIOACTIVE" runat="server"></asp:label><span class="mandatory" id="spnDESC_EXPOSURE_RATIOACTIVE">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESC_EXPOSURE_RATIOACTIVE" runat="server" maxlength="300" size="30"></asp:textbox><asp:label id="lblDESC_EXPOSURE_RATIOACTIVE" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvDESC_EXPOSURE_RATIOACTIVE" runat="server" ControlToValidate="txtDESC_EXPOSURE_RATIOACTIVE"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capHAVE_PAST_PRESENT_OPERATIONS" runat="server">Do/have past, present or discontinued operations involved storing, treating, discharging, applying, disposition, or transporting go hazardous material?</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbHAVE_PAST_PRESENT_OPERATIONS" onfocus="SelectComboIndex('cmbHAVE_PAST_PRESENT_OPERATIONS')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_HAVE_PAST_PRESENT'),document.getElementById('lblDESC_HAVE_PAST_PRESENT'));">
											<ASP:LISTITEM Value="Y">Yes</ASP:LISTITEM>
											<ASP:LISTITEM Value="N">No</ASP:LISTITEM>
										</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDESC_HAVE_PAST_PRESENT" runat="server"></asp:label><span class="mandatory" id="spnDESC_HAVE_PAST_PRESENT">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESC_HAVE_PAST_PRESENT" runat="server" maxlength="300" size="30"></asp:textbox><asp:label id="lblDESC_HAVE_PAST_PRESENT" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvDESC_HAVE_PAST_PRESENT" runat="server" ControlToValidate="txtDESC_HAVE_PAST_PRESENT"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capANY_OPERATIONS_SOLD" runat="server">Any operations sold, acquired, or discontinued in last 5 years? </asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbANY_OPERATIONS_SOLD" onfocus="SelectComboIndex('cmbANY_OPERATIONS_SOLD')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_ANY_OPERATIONS'),document.getElementById('lblDESC_ANY_OPERATIONS'));">
											<ASP:LISTITEM Value="Y">Yes</ASP:LISTITEM>
											<ASP:LISTITEM Value="N">No</ASP:LISTITEM>
										</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDESC_ANY_OPERATIONS" runat="server"></asp:label><span class="mandatory" id="spnDESC_ANY_OPERATIONS">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESC_ANY_OPERATIONS" runat="server" maxlength="300" size="30"></asp:textbox><asp:label id="lblDESC_ANY_OPERATIONS" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvDESC_ANY_OPERATIONS" runat="server" ControlToValidate="txtDESC_ANY_OPERATIONS"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capMACHINERY_LOANED" runat="server">Machinery or equipment loaned or rented to others?</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbMACHINERY_LOANED" onfocus="SelectComboIndex('cmbMACHINERY_LOANED')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_MACHINERY_LOANED'),document.getElementById('lblDESC_MACHINERY_LOANED'));">
											<ASP:LISTITEM Value="Y">Yes</ASP:LISTITEM>
											<ASP:LISTITEM Value="N">No</ASP:LISTITEM>
										</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDESC_MACHINERY_LOANED" runat="server"></asp:label><span class="mandatory" id="spnDESC_MACHINERY_LOANED">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESC_MACHINERY_LOANED" runat="server" maxlength="300" size="30"></asp:textbox><asp:label id="lblDESC_MACHINERY_LOANED" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvDESC_MACHINERY_LOANED" runat="server" ControlToValidate="txtDESC_MACHINERY_LOANED"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capANY_WATERCRAFT_LEASED" runat="server">Any watercraft, docks, floats owned, hired or leased? </asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbANY_WATERCRAFT_LEASED" onfocus="SelectComboIndex('cmbANY_WATERCRAFT_LEASED')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_ANY_WATERCRAFT'),document.getElementById('lblDESC_ANY_WATERCRAFT'));">
											<ASP:LISTITEM Value="Y">Yes</ASP:LISTITEM>
											<ASP:LISTITEM Value="N">No</ASP:LISTITEM>
										</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDESC_ANY_WATERCRAFT" runat="server"></asp:label><span class="mandatory" id="spnDESC_ANY_WATERCRAFT">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESC_ANY_WATERCRAFT" runat="server" maxlength="300" size="30"></asp:textbox><asp:label id="lblDESC_ANY_WATERCRAFT" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvDESC_ANY_WATERCRAFT" runat="server" ControlToValidate="txtDESC_ANY_WATERCRAFT"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capANY_PARKING_OWNED" runat="server">Any parking facilities owned/Rented?</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbANY_PARKING_OWNED" onfocus="SelectComboIndex('cmbANY_PARKING_OWNED')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_ANY_PARKING'),document.getElementById('lblDESC_ANY_PARKING'));">
											<ASP:LISTITEM Value="Y">Yes</ASP:LISTITEM>
											<ASP:LISTITEM Value="N">No</ASP:LISTITEM>
										</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDESC_ANY_PARKING" runat="server"></asp:label><span class="mandatory" id="spnDESC_ANY_PARKING">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESC_ANY_PARKING" runat="server" maxlength="300" size="30"></asp:textbox><asp:label id="lblDESC_ANY_PARKING" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvDESC_ANY_PARKING" runat="server" ControlToValidate="txtDESC_ANY_PARKING"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capFEE_CHARGED_PARKING" runat="server">Is a fee charged for parking?</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbFEE_CHARGED_PARKING" onfocus="SelectComboIndex('cmbFEE_CHARGED_PARKING')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_FEE_CHARGED'),document.getElementById('lblDESC_FEE_CHARGED'));">
											<ASP:LISTITEM Value="Y">Yes</ASP:LISTITEM>
											<ASP:LISTITEM Value="N">No</ASP:LISTITEM>
										</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDESC_FEE_CHARGED" runat="server"></asp:label><span class="mandatory" id="spnDESC_FEE_CHARGED">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESC_FEE_CHARGED" runat="server" maxlength="300" size="30"></asp:textbox><asp:label id="lblDESC_FEE_CHARGED" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvDESC_FEE_CHARGED" runat="server" ControlToValidate="txtDESC_FEE_CHARGED"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capRECREATION_PROVIDED" runat="server">Recreation facilities provided?</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbRECREATION_PROVIDED" onfocus="SelectComboIndex('cmbRECREATION_PROVIDED')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_RECREATION_PROVIDED'),document.getElementById('lblDESC_RECREATION_PROVIDED'));">
											<ASP:LISTITEM Value="Y">Yes</ASP:LISTITEM>
											<ASP:LISTITEM Value="N">No</ASP:LISTITEM>
										</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDESC_RECREATION_PROVIDED" runat="server"></asp:label><span class="mandatory" id="spnDESC_RECREATION_PROVIDED">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESC_RECREATION_PROVIDED" runat="server" maxlength="300" size="30"></asp:textbox><asp:label id="lblDESC_RECREATION_PROVIDED" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvDESC_RECREATION_PROVIDED" runat="server" ControlToValidate="txtDESC_RECREATION_PROVIDED"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capSWIMMING_POOL_PREMISES" runat="server">Is there a swimming pool on the premises?</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbSWIMMING_POOL_PREMISES" onfocus="SelectComboIndex('cmbSWIMMING_POOL_PREMISES')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_SWIMMING_POOL'),document.getElementById('lblDESC_SWIMMING_POOL'));">
											<ASP:LISTITEM Value="Y">Yes</ASP:LISTITEM>
											<ASP:LISTITEM Value="N">No</ASP:LISTITEM>
										</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDESC_SWIMMING_POOL" runat="server"></asp:label><span class="mandatory" id="spnDESC_SWIMMING_POOL">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESC_SWIMMING_POOL" runat="server" maxlength="300" size="30"></asp:textbox><asp:label id="lblDESC_SWIMMING_POOL" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvDESC_SWIMMING_POOL" runat="server" ControlToValidate="txtDESC_SWIMMING_POOL"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capSPORTING_EVENT_SPONSORED" runat="server">Sporting or social events sponsored?</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbSPORTING_EVENT_SPONSORED" onfocus="SelectComboIndex('cmbSPORTING_EVENT_SPONSORED')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_SPORTING_EVENT'),document.getElementById('lblDESC_SPORTING_EVENT'));">
											<ASP:LISTITEM Value="Y">Yes</ASP:LISTITEM>
											<ASP:LISTITEM Value="N">No</ASP:LISTITEM>
										</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDESC_SPORTING_EVENT" runat="server"></asp:label><span class="mandatory" id="spnDESC_SPORTING_EVENT">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESC_SPORTING_EVENT" runat="server" maxlength="300" size="30"></asp:textbox><asp:label id="lblDESC_SPORTING_EVENT" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvDESC_SPORTING_EVENT" runat="server" ControlToValidate="txtDESC_SPORTING_EVENT"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capSTRUCTURAL_ALTERATION_CONTEMPATED" runat="server">Any structural alteration contemplated?</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbSTRUCTURAL_ALTERATION_CONTEMPATED" onfocus="SelectComboIndex('cmbSTRUCTURAL_ALTERATION_CONTEMPATED')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_STRUCTURAL_ALTERATION'),document.getElementById('lblDESC_STRUCTURAL_ALTERATION'));">
											<ASP:LISTITEM Value="Y">Yes</ASP:LISTITEM>
											<ASP:LISTITEM Value="N">No</ASP:LISTITEM>
										</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDESC_STRUCTURAL_ALTERATION" runat="server"></asp:label><span class="mandatory" id="spnDESC_STRUCTURAL_ALTERATION">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESC_STRUCTURAL_ALTERATION" runat="server" maxlength="300" size="30"></asp:textbox><asp:label id="lblDESC_STRUCTURAL_ALTERATION" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvDESC_STRUCTURAL_ALTERATION" runat="server" ControlToValidate="txtDESC_STRUCTURAL_ALTERATION"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capDEMOLITION_EXPOSURE_CONTEMPLATED" runat="server">Any demolition exposure contemplated?</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbDEMOLITION_EXPOSURE_CONTEMPLATED" onfocus="SelectComboIndex('cmbDEMOLITION_EXPOSURE_CONTEMPLATED')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_DEMOLITION_EXPOSURE'),document.getElementById('lblDESC_DEMOLITION_EXPOSURE'));">
											<ASP:LISTITEM Value="Y">Yes</ASP:LISTITEM>
											<ASP:LISTITEM Value="N">No</ASP:LISTITEM>
										</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDESC_DEMOLITION_EXPOSURE" runat="server"></asp:label><span class="mandatory" id="spnDESC_DEMOLITION_EXPOSURE">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESC_DEMOLITION_EXPOSURE" runat="server" maxlength="300" size="30"></asp:textbox><asp:label id="lblDESC_DEMOLITION_EXPOSURE" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvDESC_DEMOLITION_EXPOSURE" runat="server" ControlToValidate="txtDESC_DEMOLITION_EXPOSURE"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capCUSTOMER_ACTIVE_JOINT_VENTURES" runat="server">Has customer been active in or is currently active in joint ventures?</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbCUSTOMER_ACTIVE_JOINT_VENTURES" onfocus="SelectComboIndex('cmbCUSTOMER_ACTIVE_JOINT_VENTURES')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_CUSTOMER_ACTIVE'),document.getElementById('lblDESC_CUSTOMER_ACTIVE'));">
											<ASP:LISTITEM Value="Y">Yes</ASP:LISTITEM>
											<ASP:LISTITEM Value="N">No</ASP:LISTITEM>
										</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDESC_CUSTOMER_ACTIVE" runat="server"></asp:label><span class="mandatory" id="spnDESC_CUSTOMER_ACTIVE">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESC_CUSTOMER_ACTIVE" runat="server" maxlength="300" size="30"></asp:textbox><asp:label id="lblDESC_CUSTOMER_ACTIVE" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvDESC_CUSTOMER_ACTIVE" runat="server" ControlToValidate="txtDESC_CUSTOMER_ACTIVE"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capLEASE_EMPLOYEE" runat="server">Do you lease employees to or from other employers?</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbLEASE_EMPLOYEE" onfocus="SelectComboIndex('cmbLEASE_EMPLOYEE')" runat="server"
											onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_LEASE_EMPLOYEE'),document.getElementById('lblDESC_LEASE_EMPLOYEE'));">
											<ASP:LISTITEM Value="Y">Yes</ASP:LISTITEM>
											<ASP:LISTITEM Value="N">No</ASP:LISTITEM>
										</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDESC_LEASE_EMPLOYEE" runat="server"></asp:label><span class="mandatory" id="spnDESC_LEASE_EMPLOYEE">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESC_LEASE_EMPLOYEE" runat="server" maxlength="300" size="30"></asp:textbox><asp:label id="lblDESC_LEASE_EMPLOYEE" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvDESC_LEASE_EMPLOYEE" runat="server" ControlToValidate="txtDESC_LEASE_EMPLOYEE"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capLABOR_INTERCHANGE_OTH_BUSINESS" runat="server">Is there a labor interchange with any other business or subsidiaries?</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbLABOR_INTERCHANGE_OTH_BUSINESS" onfocus="SelectComboIndex('cmbLABOR_INTERCHANGE_OTH_BUSINESS')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_LABOR_INTERCHANGE'),document.getElementById('lblDESC_LABOR_INTERCHANGE'));">
											<ASP:LISTITEM Value="Y">Yes</ASP:LISTITEM>
											<ASP:LISTITEM Value="N">No</ASP:LISTITEM>
										</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDESC_LABOR_INTERCHANGE" runat="server"></asp:label><span class="mandatory" id="spnDESC_LABOR_INTERCHANGE">*</span></TD>
									<TD class="midcolora" width="32%">
										<asp:textbox id="txtDESC_LABOR_INTERCHANGE" runat='server' size='30' maxlength='300'></asp:textbox>
										<asp:label id="lblDESC_LABOR_INTERCHANGE" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<br>
										<asp:requiredfieldvalidator id="rfvDESC_LABOR_INTERCHANGE" runat="server" ControlToValidate="txtDESC_LABOR_INTERCHANGE"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capDAY_CARE_FACILITIES" runat="server">Are day care facilities operated or controlled?</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbDAY_CARE_FACILITIES" onfocus="SelectComboIndex('cmbDAY_CARE_FACILITIES')"
											runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_DAY_CARE'),document.getElementById('lblDESC_DAY_CARE'));">
											<ASP:LISTITEM Value="Y">Yes</ASP:LISTITEM>
											<ASP:LISTITEM Value="N">No</ASP:LISTITEM>
										</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%">
										<asp:Label id="capDESC_DAY_CARE" runat="server"></asp:Label><span class="mandatory" id="spnDESC_DAY_CARE">*</span>
									</TD>
									<TD class="midcolora" width="32%">
										<asp:textbox id="txtDESC_DAY_CARE" runat='server' size='30' maxlength='300'></asp:textbox>
										<asp:label id="lblDESC_DAY_CARE" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<br>
										<asp:requiredfieldvalidator id="rfvDESC_DAY_CARE" runat="server" ControlToValidate="txtDESC_DAY_CARE" Display="Dynamic"></asp:requiredfieldvalidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="32%"><asp:label id="capADDITIONAL_COMMENTS" runat="server"></asp:label></TD>
									<TD class="midcolora" width="18%" colSpan="3"><asp:textbox id="txtADDITIONAL_COMMENTS" onkeypress="MaxLength(this,200);" Height="100" Width="300"
											TextMode="MultiLine" Runat="server" MaxLength="200"></asp:textbox><br>
										<asp:customvalidator id="csvADDITIONAL_COMMENTS" Runat="server" Display="Dynamic" ControlToValidate="txtADDITIONAL_COMMENTS"
											ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator></TD>
								</tr>
								<tr>
									<td class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
									<td class="midcolora" colSpan="2"></td>
									<td class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
								<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
								<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
								<INPUT id="hidCUSTOMER_ID" type="hidden" name="hidCUSTOMER_ID" runat="server"> <INPUT id="hidPOLICY_ID" type="hidden" name="hidPOLICY_ID" runat="server">
								<INPUT id="hidPOLICY_VERSION_ID" type="hidden" name="hidPOLICY_VERSION_ID" runat="server">
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</FORM>
		</div>
		<script>
//RefreshWindowsGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidCUSTOMER_ID').value);
		</script>
	</BODY>
</HTML>

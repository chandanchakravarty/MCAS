<%@ Page validateRequest="false" language="c#" Codebehind="PolicyUmbrellaGenInfo.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Umbrella.PolicyUmbrellaGenInfo" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>UmbrellaGenInfo</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		function DisableEnableDesc(cmbCombo,txtDesc,lblNA)
			{	
				
				if (cmbCombo.selectedIndex > -1)
				{	
					if (cmbCombo.options[cmbCombo.selectedIndex].text == "No")
					{ 
						txtDesc.style.display = "inline";
						lblNA.style.display = "none";
						if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null)
						{
							document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled",true);
							
							if (document.getElementById("rfv" + txtDesc.id.substring(3)).isvalid == false)
								document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "inline";
						}
						if (document.getElementById("spn" + txtDesc.id.substring(3)) != null)
						{
							document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "inline";
						}
											
					}
					else
					{
						txtDesc.style.display = "none";
						lblNA.style.display = "inline";
						lblNA.innerHTML="NA";
						if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null)
						{
							document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled",false);
							document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "none";
						}
						if (document.getElementById("spn" + txtDesc.id.substring(3)) != null)
						{
							document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "none";
						}
					}
				}
				else
				{
					txtDesc.style.display = "none";
					lblNA.style.display = "inline";
					lblNA.innerHTML="NA";
					if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null)
					{
						document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled",false);
						document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "none";
					}
					if (document.getElementById("spn" + txtDesc.id.substring(3)) != null)
					{
						document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "none";
					}
				}
		
						
		}
		
		function ValidateCalcMaxLength(objSource, objArgs)
		{		
			if (document.getElementById(objSource.controltovalidate).value.length > 100)			
				objArgs.IsValid = false;			
			else			
				objArgs.IsValid = true;
		}
		function ValidateRemarksMaxLength(objSource, objArgs)
		{		
			if (document.getElementById(objSource.controltovalidate).value.length > 500)			
				objArgs.IsValid = false;			
			else			
				objArgs.IsValid = true;
		}		
		
					function ResetForm()
					{
						DisableValidators();
							
						if ( document.POL_UMBRELLA_GEN_INFO.hidOldData.value == '' )
						{
							AddData();
						}
						else
						{
							//alert('ol' + document.Form1.hidOldData.value);
							populateFormData(document.POL_UMBRELLA_GEN_INFO.hidOldData.value,POL_UMBRELLA_GEN_INFO);
						}
						
						return false;
					}
					
					function PopulateXML()
					{
						if (document.getElementById("hidOldData").value == "")
						{
							AddData();	
						}
					}
					
					function AddData()
					{
						ChangeColor();
						DisableValidators();
						//document.getElementById('hidCUSTOMER_ID').value	=	'New';
						document.getElementById('cmbANY_AIRCRAFT_OWNED_LEASED').focus();
						document.getElementById('cmbANY_AIRCRAFT_OWNED_LEASED').options.selectedIndex = 2;
						document.getElementById('cmbANY_OPERATOR_CON_TRAFFIC').options.selectedIndex = 2;
						document.getElementById('cmbANY_OPERATOR_IMPIRED').options.selectedIndex = 2;
						document.getElementById('cmbANY_SWIMMING_POOL').options.selectedIndex = 2;
						document.getElementById('cmbREAL_STATE_VEHICLE_USED').options.selectedIndex = 2;
						document.getElementById('cmbREAL_STATE_VEH_OWNED_HIRED').options.selectedIndex = 2;
						document.getElementById('cmbENGAGED_IN_FARMING').options.selectedIndex = 2;
						document.getElementById('cmbHOLD_NON_COMP_POSITION').options.selectedIndex = 2;
						document.getElementById('cmbANY_FULL_TIME_EMPLOYEE').options.selectedIndex = 2;
						document.getElementById('cmbNON_OWNED_PROPERTY_CARE').options.selectedIndex = 2;
						document.getElementById('cmbBUSINESS_PROF_ACTIVITY').options.selectedIndex = 2;
						document.getElementById('cmbREDUCED_LIMIT_OF_LIBLITY').options.selectedIndex = 2;
						document.getElementById('cmbANY_COVERAGE_DECLINED').options.selectedIndex = 2;
						document.getElementById('cmbANIMALS_EXOTIC_PETS').options.selectedIndex = 2;
						document.getElementById('cmbINSU_TRANSFERED_IN_AGENCY').options.selectedIndex = 2;
						document.getElementById('cmbPENDING_LITIGATIONS').options.selectedIndex = 2;
						document.getElementById('cmbIS_TEMPOLINE').options.selectedIndex = 2;
						document.getElementById('cmbHOME_RENT_DWELL').options.selectedIndex = -1;
						document.getElementById('cmbWAT_DWELL').options.selectedIndex = -1;
						document.getElementById('cmbRECR_VEH').options.selectedIndex = -1;
						document.getElementById('cmbAUTO_CYCL_TRUCKS').options.selectedIndex = -1;
						document.getElementById('cmbAPPLI_UNDERSTAND_LIABILITY_EXCLUDED').options.selectedIndex = -1;
						document.getElementById('cmbHAVE_NON_OWNED_AUTO_POL').options.selectedIndex = 2;
						document.getElementById('cmbINS_DOMICILED_OUTSIDE').options.selectedIndex = 2;
						document.getElementById('cmbHOME_DAY_CARE').options.selectedIndex = 2;
						document.getElementById('txtANY_AIRCRAFT_OWNED_LEASED_DESC').value= '';
						document.getElementById('txtANY_OPERATOR_CON_TRAFFIC_DESC').value= '';
						document.getElementById('txtANY_OPERATOR_IMPIRED_DESC').value= '';
						document.getElementById('txtANY_SWIMMING_POOL_DESC').value= '';
						document.getElementById('txtREAL_STATE_VEHICLE_USED_DESC').value= '';
						document.getElementById('txtREAL_STATE_VEH_OWNED_HIRED_DESC').value= '';
						document.getElementById('txtENGAGED_IN_FARMING_DESC').value= '';
						document.getElementById('txtHOLD_NON_COMP_POSITION_DESC').value= '';
						document.getElementById('txtANY_FULL_TIME_EMPLOYEE_DESC').value= '';
						document.getElementById('txtNON_OWNED_PROPERTY_CARE_DESC').value= '';
						document.getElementById('txtBUSINESS_PROF_ACTIVITY_DESC').value= '';
						document.getElementById('txtREDUCED_LIMIT_OF_LIBLITY_DESC').value= '';
						document.getElementById('txtANIMALS_EXOTIC_PETS_DESC').value= '';
						document.getElementById('txtANY_COVERAGE_DECLINED_DESC').value= '';
						document.getElementById('txtPENDING_LITIGATIONS_DESC').value= '';
						document.getElementById('txtINSU_TRANSFERED_IN_AGENCY_DESC').value= '';
						document.getElementById('txtIS_TEMPOLINE_DESC').value= '';
						document.getElementById('txtHAVE_NON_OWNED_AUTO_POL_DESC').value= '';
						document.getElementById('txtINS_DOMICILED_OUTSIDE_DESC').value= '';
						document.getElementById('txtHOME_DAY_CARE_DESC').value= '';
						document.getElementById('txtCALCULATIONS').value= '';
						document.getElementById('txtHOME_RENT_DWELL_DESC').value= '';
						document.getElementById('txtWAT_DWELL_DESC').value= '';
						document.getElementById('txtRECR_VEH_DESC').value= '';
						document.getElementById('txtAUTO_CYCL_TRUCKS_DESC').value= '';
						document.getElementById('txtAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC').value= '';
						document.getElementById('txtUND_REMARKS').value= '';
						document.getElementById('txtFAMILIES').value= '';

						
					}
					
					function EnableDisable()
						{
							EnableDisableDesc(document.getElementById('cmbANY_AIRCRAFT_OWNED_LEASED'),document.getElementById('txtANY_AIRCRAFT_OWNED_LEASED_DESC'),document.getElementById('lblANY_AIRCRAFT_OWNED_LEASED_DESC'));
							EnableDisableDesc(document.getElementById('cmbANY_OPERATOR_CON_TRAFFIC'),document.getElementById('txtANY_OPERATOR_CON_TRAFFIC_DESC'),document.getElementById('lblANY_OPERATOR_CON_TRAFFIC_DESC'));
							EnableDisableDesc(document.getElementById('cmbANY_OPERATOR_IMPIRED'),document.getElementById('txtANY_OPERATOR_IMPIRED_DESC'),document.getElementById('lblANY_OPERATOR_IMPIRED_DESC'));
							EnableDisableDesc(document.getElementById('cmbANY_SWIMMING_POOL'),document.getElementById('txtANY_SWIMMING_POOL_DESC'),document.getElementById('lblANY_SWIMMING_POOL_DESC'));
							EnableDisableDesc(document.getElementById('cmbREAL_STATE_VEHICLE_USED'),document.getElementById('txtREAL_STATE_VEHICLE_USED_DESC'),document.getElementById('lblREAL_STATE_VEHICLE_USED_DESC'));
							EnableDisableDesc(document.getElementById('cmbREAL_STATE_VEH_OWNED_HIRED'),document.getElementById('txtREAL_STATE_VEH_OWNED_HIRED_DESC'),document.getElementById('lblREAL_STATE_VEH_OWNED_HIRED_DESC'));
							EnableDisableDesc(document.getElementById('cmbENGAGED_IN_FARMING'),document.getElementById('txtENGAGED_IN_FARMING_DESC'),document.getElementById('lblENGAGED_IN_FARMING_DESC'));
		 					EnableDisableDesc(document.getElementById('cmbHOLD_NON_COMP_POSITION'),document.getElementById('txtHOLD_NON_COMP_POSITION_DESC'),document.getElementById('lblHOLD_NON_COMP_POSITION_DESC'));
							EnableDisableDesc(document.getElementById('cmbANY_FULL_TIME_EMPLOYEE'),document.getElementById('txtANY_FULL_TIME_EMPLOYEE_DESC'),document.getElementById('lblANY_FULL_TIME_EMPLOYEE_DESC'));
							EnableDisableDesc(document.getElementById('cmbNON_OWNED_PROPERTY_CARE'),document.getElementById('txtNON_OWNED_PROPERTY_CARE_DESC'),document.getElementById('lblNON_OWNED_PROPERTY_CARE_DESC'));
							EnableDisableDesc(document.getElementById('cmbBUSINESS_PROF_ACTIVITY'),document.getElementById('txtBUSINESS_PROF_ACTIVITY_DESC'),document.getElementById('lblBUSINESS_PROF_ACTIVITY_DESC'));
							EnableDisableDesc(document.getElementById('cmbREDUCED_LIMIT_OF_LIBLITY'),document.getElementById('txtREDUCED_LIMIT_OF_LIBLITY_DESC'),document.getElementById('lblREDUCED_LIMIT_OF_LIBLITY_DESC'));
							EnableDisableDesc(document.getElementById('cmbANIMALS_EXOTIC_PETS'),document.getElementById('txtANIMALS_EXOTIC_PETS_DESC'),document.getElementById('lblANIMALS_EXOTIC_PETS_DESC'));
							EnableDisableDesc(document.getElementById('cmbANY_COVERAGE_DECLINED'),document.getElementById('txtANY_COVERAGE_DECLINED_DESC'),document.getElementById('lblANY_COVERAGE_DECLINED_DESC'));
							EnableDisableDesc(document.getElementById('cmbINSU_TRANSFERED_IN_AGENCY'),document.getElementById('txtINSU_TRANSFERED_IN_AGENCY_DESC'),document.getElementById('lblINSU_TRANSFERED_IN_AGENCY_DESC'));
							EnableDisableDesc(document.getElementById('cmbPENDING_LITIGATIONS'),document.getElementById('txtPENDING_LITIGATIONS_DESC'),document.getElementById('lblPENDING_LITIGATIONS_DESC'));
							EnableDisableDesc(document.getElementById('cmbIS_TEMPOLINE'),document.getElementById('txtIS_TEMPOLINE_DESC'),document.getElementById('lblIS_TEMPOLINE_DESC'));
							EnableDisableDesc(document.getElementById('cmbHAVE_NON_OWNED_AUTO_POL'),document.getElementById('txtHAVE_NON_OWNED_AUTO_POL_DESC'),document.getElementById('lblHAVE_NON_OWNED_AUTO_POL_DESC'));
							EnableDisableDesc(document.getElementById('cmbINS_DOMICILED_OUTSIDE'),document.getElementById('txtINS_DOMICILED_OUTSIDE_DESC'),document.getElementById('lblINS_DOMICILED_OUTSIDE_DESC'));
							EnableDisableDesc(document.getElementById('cmbHOME_DAY_CARE'),document.getElementById('txtHOME_DAY_CARE_DESC'),document.getElementById('lblHOME_DAY_CARE_DESC'));
							DisableEnableDesc(document.getElementById('cmbHOME_RENT_DWELL'),document.getElementById('txtHOME_RENT_DWELL_DESC'),document.getElementById('lblHOME_RENT_DWELL_DESC'));
							DisableEnableDesc(document.getElementById('cmbWAT_DWELL'),document.getElementById('txtWAT_DWELL_DESC'),document.getElementById('lblWAT_DWELL_DESC'));
							DisableEnableDesc(document.getElementById('cmbRECR_VEH'),document.getElementById('txtRECR_VEH_DESC'),document.getElementById('lblRECR_VEH_DESC'));
							DisableEnableDesc(document.getElementById('cmbAUTO_CYCL_TRUCKS'),document.getElementById('txtAUTO_CYCL_TRUCKS_DESC'),document.getElementById('lblAUTO_CYCL_TRUCKS_DESC'));
							DisableEnableDesc(document.getElementById('cmbAPPLI_UNDERSTAND_LIABILITY_EXCLUDED'),document.getElementById('txtAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC'),document.getElementById('lblAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC'));
			
							
						 
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
					
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="ApplyColor();PopulateXML();ChangeColor();top.topframe.main1.mousein = false;findMouseIn();showScroll();EnableDisable();">
		<DIV id="myTSMain" style="BEHAVIOR: url('/cms/cmsweb/htc/webservice.htc')"></DIV>
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<div class="pageContent" id="bodyHeight">
			<table class="tableWidth" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td>
						<FORM id="POL_UMBRELLA_GEN_INFO" method="post" runat="server">
							<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD>
										<TABLE width="100%" align="center" border="0">
											<tr>
												<TD class="pageHeader" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow></TD>
											</tr>
											<tr>
												<TD class="pageHeader" id="tdClientTop" colSpan="4"><webcontrol:clienttop id="cltClientTop" runat="server" width="98%"></webcontrol:clienttop><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></TD>
											</tr>
											<tr>
												<td class="headereffectCenter" colSpan="4">Underwriting Questions</td>
											</tr>
											<tr>
												<td class="pageHeader" colSpan="2">Please note that all fields marked with * are 
													mandatory
												</td>
											</tr>
											<tr>
												<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
											</tr>
											<!--1-->
											<tr>
												<TD class="midcolora" width="32%"><asp:label id="capANY_AIRCRAFT_OWNED_LEASED" runat="server">Are any aircraft owned, leased, chartered or furnished for regular use?</asp:label></TD>
												<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbANY_AIRCRAFT_OWNED_LEASED" onfocus="SelectComboIndex('cmbANY_AIRCRAFT_OWNED_LEASED')"
														runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_AIRCRAFT_OWNED_LEASED_DESC'),document.getElementById('lblANY_AIRCRAFT_OWNED_LEASED_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD class="midcolora" width="32%"><asp:label id="capANY_AIRCRAFT_OWNED_LEASED_DESC" runat="server">Aircraft Details</asp:label><span class="mandatory" id="spnANY_AIRCRAFT_OWNED_LEASED_DESC">*</span></TD>
												<TD class="midcolora" width="18%"><asp:textbox id="txtANY_AIRCRAFT_OWNED_LEASED_DESC" runat="server" size="28" MaxLength="250"></asp:textbox><asp:label id="lblANY_AIRCRAFT_OWNED_LEASED_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br><asp:requiredfieldvalidator id="rfvANY_AIRCRAFT_OWNED_LEASED_DESC" runat="server" Display="Dynamic" ControlToValidate="txtANY_AIRCRAFT_OWNED_LEASED_DESC"></asp:requiredfieldvalidator></TD>
											</tr>
											<!--2-->
											<tr>
												<TD class="midcolora" width="32%"><asp:label id="capANY_OPERATOR_CON_TRAFFIC" runat="server">Any operators convicted for any traffic violations during the last 3 years?</asp:label></TD>
												<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbANY_OPERATOR_CON_TRAFFIC" onfocus="SelectComboIndex('cmbANY_OPERATOR_CON_TRAFFIC')"
														runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_OPERATOR_CON_TRAFFIC_DESC'),document.getElementById('lblANY_OPERATOR_CON_TRAFFIC_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD class="midcolora" width="32%"><asp:label id="capANY_OPERATOR_CON_TRAFFIC_DESC" runat="server">Field Traffic Violation Details</asp:label><span class="mandatory" id="spnANY_OPERATOR_CON_TRAFFIC_DESC">*</span></TD>
												<TD class="midcolora" width="18%"><asp:textbox id="txtANY_OPERATOR_CON_TRAFFIC_DESC" runat="server" size="28" MaxLength="250"></asp:textbox><asp:label id="lblANY_OPERATOR_CON_TRAFFIC_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br><asp:requiredfieldvalidator id="rfvANY_OPERATOR_CON_TRAFFIC_DESC" runat="server" Display="Dynamic" ControlToValidate="txtANY_OPERATOR_CON_TRAFFIC_DESC"></asp:requiredfieldvalidator></TD>
											</tr>
											<!--3-->
											<tr>
												<TD class="midcolora" width="32%"><asp:label id="capANY_OPERATOR_IMPIRED" runat="server">Any operator physically or mentally  impaired?</asp:label></TD>
												<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbANY_OPERATOR_IMPIRED" onfocus="SelectComboIndex('cmbANY_OPERATOR_IMPIRED')"
														runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_OPERATOR_IMPIRED_DESC'),document.getElementById('lblANY_OPERATOR_IMPIRED_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD class="midcolora" width="32%"><asp:label id="capANY_OPERATOR_IMPIRED_DESC" runat="server">Field Impaired Details</asp:label><span class="mandatory" id="spnANY_OPERATOR_IMPIRED_DESC">*</span></TD>
												<TD class="midcolora" width="18%"><asp:textbox id="txtANY_OPERATOR_IMPIRED_DESC" runat="server" size="28" MaxLength="250"></asp:textbox><asp:label id="lblANY_OPERATOR_IMPIRED_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br><asp:requiredfieldvalidator id="rfvANY_OPERATOR_IMPIRED_DESC" runat="server" Display="Dynamic" ControlToValidate="txtANY_OPERATOR_IMPIRED_DESC"></asp:requiredfieldvalidator></TD>
											</tr>
											<!-- 4 -->
											<tr>
												<TD class="midcolora" width="32%"><asp:label id="capANY_SWIMMING_POOL" runat="server">Any swimming pool, hot tub or spa on premises?     </asp:label></TD>
												<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbANY_SWIMMING_POOL" onfocus="SelectComboIndex('cmbANY_SWIMMING_POOL')" runat="server"
														onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_SWIMMING_POOL_DESC'),document.getElementById('lblANY_SWIMMING_POOL_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD class="midcolora" width="32%"><asp:label id="capANY_SWIMMING_POOL_DESC" runat="server">Pool/Tub or Spa  Details</asp:label><span class="mandatory" id="spnANY_SWIMMING_POOL_DESC">*</span></TD>
												<TD class="midcolora" width="18%"><asp:textbox id="txtANY_SWIMMING_POOL_DESC" runat="server" size="28" MaxLength="250"></asp:textbox><asp:label id="lblANY_SWIMMING_POOL_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br><asp:requiredfieldvalidator id="rfvANY_SWIMMING_POOL_DESC" runat="server" Display="Dynamic" ControlToValidate="txtANY_SWIMMING_POOL_DESC"></asp:requiredfieldvalidator></TD>
											</tr>
											<!-- 5-->
											<tr>
												<TD class="midcolora" width="32%"><asp:label id="capREAL_STATE_VEHICLE_USED" runat="server">Any real estate, vehicles, watercraft, aircraft used commercially or for business purposes?     </asp:label></TD>
												<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbREAL_STATE_VEHICLE_USED" onfocus="SelectComboIndex('cmbREAL_STATE_VEHICLE_USED')"
														runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtREAL_STATE_VEHICLE_USED_DESC'),document.getElementById('lblREAL_STATE_VEHICLE_USED_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD class="midcolora" width="32%"><asp:label id="capREAL_STATE_VEHICLE_USED_DESC" runat="server">Commercial/Business Use Details</asp:label><span class="mandatory" id="spnREAL_STATE_VEHICLE_USED_DESC">*</span></TD>
												<TD class="midcolora" width="18%"><asp:textbox id="txtREAL_STATE_VEHICLE_USED_DESC" runat="server" size="28" MaxLength="250"></asp:textbox><asp:label id="lblREAL_STATE_VEHICLE_USED_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br><asp:requiredfieldvalidator id="rfvREAL_STATE_VEHICLE_USED_DESC" runat="server" Display="Dynamic" ControlToValidate="txtREAL_STATE_VEHICLE_USED_DESC"></asp:requiredfieldvalidator></TD>
											</tr>
											<!-- 6 -->
											<tr>
												<TD class="midcolora" width="32%"><asp:label id="capREAL_STATE_VEH_OWNED_HIRED" runat="server">Any real estate, vehicles, watercraft, aircraft owned, hired, leased or regularly used not covered by primary policies?     </asp:label></TD>
												<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbREAL_STATE_VEH_OWNED_HIRED" onfocus="SelectComboIndex('cmbREAL_STATE_VEH_OWNED_HIRED')"
														runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtREAL_STATE_VEH_OWNED_HIRED_DESC'),document.getElementById('lblREAL_STATE_VEH_OWNED_HIRED_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD class="midcolora" width="32%"><asp:label id="capREAL_STATE_VEH_OWNED_HIRED_DESC" runat="server">No primary Coverage Details</asp:label><span class="mandatory" id="spnREAL_STATE_VEH_OWNED_HIRED_DESC">*</span></TD>
												<TD class="midcolora" width="18%"><asp:textbox id="txtREAL_STATE_VEH_OWNED_HIRED_DESC" runat="server" size="28" MaxLength="250"></asp:textbox><asp:label id="lblREAL_STATE_VEH_OWNED_HIRED_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br><asp:requiredfieldvalidator id="rfvREAL_STATE_VEH_OWNED_HIRED_DESC" runat="server" Display="Dynamic" ControlToValidate="txtREAL_STATE_VEH_OWNED_HIRED_DESC"></asp:requiredfieldvalidator></TD>
											</tr>
											<!-- 7 -->
											<tr>
												<TD class="midcolora" width="32%"><asp:label id="capENGAGED_IN_FARMING" runat="server">Do you engage in any type of farming operation?</asp:label></TD>
												<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbENGAGED_IN_FARMING" onfocus="SelectComboIndex('cmbENGAGED_IN_FARMING')" runat="server"
														onchange="javascript:EnableDisableDesc(this,document.getElementById('txtENGAGED_IN_FARMING_DESC'),document.getElementById('lblENGAGED_IN_FARMING_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD class="midcolora" width="32%"><asp:label id="capENGAGED_IN_FARMING_DESC" runat="server">Farming Operation Details</asp:label><span class="mandatory" id="spnENGAGED_IN_FARMING_DESC">*</span></TD>
												<TD class="midcolora" width="18%"><asp:textbox id="txtENGAGED_IN_FARMING_DESC" runat="server" size="28" MaxLength="250"></asp:textbox><asp:label id="lblENGAGED_IN_FARMING_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br><asp:requiredfieldvalidator id="rfvENGAGED_IN_FARMING_DESC" runat="server" Display="Dynamic" ControlToValidate="txtENGAGED_IN_FARMING_DESC"></asp:requiredfieldvalidator></TD>
											</tr>
											<!-- 8 -->
											<tr>
												<TD class="midcolora" width="32%"><asp:label id="capHOLD_NON_COMP_POSITION" runat="server">Do you hold any non-compensated positions?     </asp:label></TD>
												<TD class="midcolora" width="18%">
													<asp:DropDownList id='cmbHOLD_NON_COMP_POSITION' OnFocus="SelectComboIndex('cmbHOLD_NON_COMP_POSITION')"
														runat='server' onchange="javascript:EnableDisableDesc(this,document.getElementById('txtHOLD_NON_COMP_POSITION_DESC'),document.getElementById('lblHOLD_NON_COMP_POSITION_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:DropDownList></TD>
												<TD class="midcolora" width="32%">
													<asp:label id="capHOLD_NON_COMP_POSITION_DESC" runat="server">Non-compensated  positions details</asp:label><span class="mandatory" id="spnHOLD_NON_COMP_POSITION_DESC">*</span></TD>
												<TD class="midcolora" width="18%">
													<asp:textbox id="txtHOLD_NON_COMP_POSITION_DESC" runat="server" size="28" MaxLength="250"></asp:textbox>
													<asp:label id="lblHOLD_NON_COMP_POSITION_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
													<asp:requiredfieldvalidator id="rfvHOLD_NON_COMP_POSITION_DESC" runat="server" ControlToValidate="txtHOLD_NON_COMP_POSITION_DESC"
														Display="Dynamic"></asp:requiredfieldvalidator>
												</TD>
											</tr>
											<!-- 9 -->
											<tr>
												<TD class='midcolora' width='32%'>
													<asp:Label id="capANY_FULL_TIME_EMPLOYEE" runat="server">Any full time employees?</asp:Label></TD>
												<TD class='midcolora' width='18%'>
													<asp:DropDownList id='cmbANY_FULL_TIME_EMPLOYEE' OnFocus="SelectComboIndex('cmbANY_FULL_TIME_EMPLOYEE')"
														runat='server' onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_FULL_TIME_EMPLOYEE_DESC'),document.getElementById('lblANY_FULL_TIME_EMPLOYEE_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:DropDownList>
												</TD>
												<TD class="midcolora" width="32%">
													<asp:label id="capANY_FULL_TIME_EMPLOYEE_DESC" runat="server">Employee Details</asp:label><span class="mandatory" id="spnANY_FULL_TIME_EMPLOYEE_DESC">*</span></TD>
												<TD class="midcolora" width="18%">
													<asp:textbox id="txtANY_FULL_TIME_EMPLOYEE_DESC" runat="server" size="28" MaxLength="250"></asp:textbox>
													<asp:label id="lblANY_FULL_TIME_EMPLOYEE_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
													<asp:requiredfieldvalidator id="rfvANY_FULL_TIME_EMPLOYEE_DESC" runat="server" ControlToValidate="txtANY_FULL_TIME_EMPLOYEE_DESC"
														Display="Dynamic"></asp:requiredfieldvalidator>
												</TD>
											</tr>
											<!-- 10 -->
											<tr>
												<TD class='midcolora' width='32%'>
													<asp:Label id="capNON_OWNED_PROPERTY_CARE" runat="server">Any non-owned property exceeding $1000 in value in your care, custody or control?</asp:Label></TD>
												<TD class='midcolora' width='18%'>
													<asp:DropDownList id='cmbNON_OWNED_PROPERTY_CARE' OnFocus="SelectComboIndex('cmbNON_OWNED_PROPERTY_CARE')"
														runat='server' onchange="javascript:EnableDisableDesc(this,document.getElementById('txtNON_OWNED_PROPERTY_CARE_DESC'),document.getElementById('lblNON_OWNED_PROPERTY_CARE_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:DropDownList>
												</TD>
												<TD class="midcolora" width="32%">
													<asp:label id="capNON_OWNED_PROPERTY_CARE_DESC" runat="server">Non-Owned Property Details</asp:label><span class="mandatory" id="spnNON_OWNED_PROPERTY_CARE_DESC">*</span></TD>
												<TD class="midcolora" width="18%">
													<asp:textbox id="txtNON_OWNED_PROPERTY_CARE_DESC" runat="server" size="28" MaxLength="250"></asp:textbox>
													<asp:label id="lblNON_OWNED_PROPERTY_CARE_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
													<asp:requiredfieldvalidator id="rfvNON_OWNED_PROPERTY_CARE_DESC" runat="server" ControlToValidate="txtNON_OWNED_PROPERTY_CARE_DESC"
														Display="Dynamic"></asp:requiredfieldvalidator>
												</TD>
											</tr>
											<!-- 11 -->
											<tr>
												<TD class='midcolora' width='32%'>
													<asp:Label id="capBUSINESS_PROF_ACTIVITY" runat="server">Any business or professional activities included in the primary policies?</asp:Label></TD>
												<TD class='midcolora' width='18%'>
													<asp:DropDownList id='cmbBUSINESS_PROF_ACTIVITY' OnFocus="SelectComboIndex('cmbBUSINESS_PROF_ACTIVITY')"
														runat='server' onchange="javascript:EnableDisableDesc(this,document.getElementById('txtBUSINESS_PROF_ACTIVITY_DESC'),document.getElementById('lblBUSINESS_PROF_ACTIVITY_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:DropDownList>
												</TD>
												<TD class="midcolora" width="32%">
													<asp:label id="capBUSINESS_PROF_ACTIVITY_DESC" runat="server">activities Details</asp:label><span class="mandatory" id="spnBUSINESS_PROF_ACTIVITY_DESC">*</span></TD>
												<TD class="midcolora" width="18%">
													<asp:textbox id="txtBUSINESS_PROF_ACTIVITY_DESC" runat="server" size="28" MaxLength="250"></asp:textbox>
													<asp:label id="lblBUSINESS_PROF_ACTIVITY_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
													<asp:requiredfieldvalidator id="rfvBUSINESS_PROF_ACTIVITY_DESC" runat="server" ControlToValidate="txtBUSINESS_PROF_ACTIVITY_DESC"
														Display="Dynamic"></asp:requiredfieldvalidator>
												</TD>
											</tr>
											<!-- 12 -->
											<tr>
												<TD class='midcolora' width='32%'>
													<asp:Label id="capREDUCED_LIMIT_OF_LIBLITY" runat="server">Does any primary policy have reduced limits of liability or eliminate coverage for specific exposures or list excluded drivers? </asp:Label></TD>
												<TD class='midcolora' width='18%'>
													<asp:DropDownList id='cmbREDUCED_LIMIT_OF_LIBLITY' OnFocus="SelectComboIndex('cmbREDUCED_LIMIT_OF_LIBLITY')"
														runat='server' onchange="javascript:EnableDisableDesc(this,document.getElementById('txtREDUCED_LIMIT_OF_LIBLITY_DESC'),document.getElementById('lblREDUCED_LIMIT_OF_LIBLITY_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:DropDownList>
												</TD>
												<TD class="midcolora" width="32%">
													<asp:label id="capREDUCED_LIMIT_OF_LIBLITY_DESC" runat="server">Reduced Limits Details</asp:label><span class="mandatory" id="spnREDUCED_LIMIT_OF_LIBLITY_DESC">*</span></TD>
												<TD class="midcolora" width="18%">
													<asp:textbox id="txtREDUCED_LIMIT_OF_LIBLITY_DESC" runat="server" size="28" MaxLength="250"></asp:textbox>
													<asp:label id="lblREDUCED_LIMIT_OF_LIBLITY_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
													<asp:requiredfieldvalidator id="rfvREDUCED_LIMIT_OF_LIBLITY_DESC" runat="server" ControlToValidate="txtREDUCED_LIMIT_OF_LIBLITY_DESC"
														Display="Dynamic"></asp:requiredfieldvalidator>
												</TD>
											</tr>
											<!-- 13 -->
											<tr>
												<TD class='midcolora' width='32%'>
													<asp:Label id="capANIMALS_EXOTIC_PETS" runat="server">Does applicant or any tenant have any animals or exotic pets?    </asp:Label></TD>
												<TD class='midcolora' width='18%'>
													<asp:DropDownList id='cmbANIMALS_EXOTIC_PETS' OnFocus="SelectComboIndex('cmbANIMALS_EXOTIC_PETS')"
														runat='server' onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANIMALS_EXOTIC_PETS_DESC'),document.getElementById('lblANIMALS_EXOTIC_PETS_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:DropDownList>
												</TD>
												<TD class="midcolora" width="32%">
													<asp:label id="capANIMALS_EXOTIC_PETS_DESC" runat="server">Exotic Pets Details</asp:label><span class="mandatory" id="spnANIMALS_EXOTIC_PETS_DESC">*</span></TD>
												<TD class="midcolora" width="18%">
													<asp:textbox id="txtANIMALS_EXOTIC_PETS_DESC" runat="server" size="28" MaxLength="250"></asp:textbox>
													<asp:label id="lblANIMALS_EXOTIC_PETS_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
													<asp:requiredfieldvalidator id="rfvANIMALS_EXOTIC_PETS_DESC" runat="server" ControlToValidate="txtANIMALS_EXOTIC_PETS_DESC"
														Display="Dynamic"></asp:requiredfieldvalidator>
												</TD>
											</tr>
											<!-- 14 -->
											<tr>
												<TD class='midcolora' width='32%'>
													<asp:Label id="capANY_COVERAGE_DECLINED" runat="server">Any coverage declined, cancelled or non-renewed during the last 3 years?     </asp:Label></TD>
												<TD class='midcolora' width='18%'>
													<asp:DropDownList id='cmbANY_COVERAGE_DECLINED' OnFocus="SelectComboIndex('cmbANY_COVERAGE_DECLINED')"
														runat='server' onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_COVERAGE_DECLINED_DESC'),document.getElementById('lblANY_COVERAGE_DECLINED_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:DropDownList>
												</TD>
												<TD class="midcolora" width="32%">
													<asp:label id="capANY_COVERAGE_DECLINED_DESC" runat="server">Field Decline/Canceled Details</asp:label><span class="mandatory" id="spnANY_COVERAGE_DECLINED_DESC">*</span></TD>
												<TD class="midcolora" width="18%">
													<asp:textbox id="txtANY_COVERAGE_DECLINED_DESC" runat="server" size="28" MaxLength="250"></asp:textbox>
													<asp:label id="lblANY_COVERAGE_DECLINED_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
													<asp:requiredfieldvalidator id="rfvANY_COVERAGE_DECLINED_DESC" runat="server" ControlToValidate="txtANY_COVERAGE_DECLINED_DESC"
														Display="Dynamic"></asp:requiredfieldvalidator>
												</TD>
											</tr>
											<!--15-->
											<tr>
												<TD class='midcolora' width='32%'>
													<asp:Label id="capPENDING_LITIGATIONS" runat="server">Any pending litigations court proceedings or judgments?     </asp:Label></TD>
												<TD class='midcolora' width='18%'>
													<asp:DropDownList id='cmbPENDING_LITIGATIONS' OnFocus="SelectComboIndex('cmbPENDING_LITIGATIONS')"
														runat='server' onchange="javascript:EnableDisableDesc(this,document.getElementById('txtPENDING_LITIGATIONS_DESC'),document.getElementById('lblPENDING_LITIGATIONS_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:DropDownList>
												</TD>
												<TD class="midcolora" width="32%">
													<asp:label id="capPENDING_LITIGATIONS_DESC" runat="server">Litigation Details</asp:label><span class="mandatory" id="spnPENDING_LITIGATIONS_DESC">*</span></TD>
												<TD class="midcolora" width="18%">
													<asp:textbox id="txtPENDING_LITIGATIONS_DESC" runat="server" size="28" MaxLength="250"></asp:textbox>
													<asp:label id="lblPENDING_LITIGATIONS_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
													<asp:requiredfieldvalidator id="rfvPENDING_LITIGATIONS_DESC" runat="server" ControlToValidate="txtPENDING_LITIGATIONS_DESC"
														Display="Dynamic"></asp:requiredfieldvalidator>
												</TD>
											</tr>
											<!--16-->
											<tr>
												<TD class='midcolora' width='32%'>
													<asp:Label id="capINSU_TRANSFERED_IN_AGENCY" runat="server">Has insurance been transferred within the agency?     </asp:Label></TD>
												<TD class='midcolora' width='18%'>
													<asp:DropDownList id='cmbINSU_TRANSFERED_IN_AGENCY' OnFocus="SelectComboIndex('cmbINSU_TRANSFERED_IN_AGENCY')"
														runat='server' onchange="javascript:EnableDisableDesc(this,document.getElementById('txtINSU_TRANSFERED_IN_AGENCY_DESC'),document.getElementById('lblINSU_TRANSFERED_IN_AGENCY_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:DropDownList>
												</TD>
												<TD class="midcolora" width="32%">
													<asp:label id="capINSU_TRANSFERED_IN_AGENCY_DESC" runat="server">Transferred Details</asp:label><span class="mandatory" id="spnINSU_TRANSFERED_IN_AGENCY_DESC">*</span></TD>
												<TD class="midcolora" width="18%">
													<asp:textbox id="txtINSU_TRANSFERED_IN_AGENCY_DESC" runat="server" size="28" MaxLength="250"></asp:textbox>
													<asp:label id="lblINSU_TRANSFERED_IN_AGENCY_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
													<asp:requiredfieldvalidator id="rfvINSU_TRANSFERED_IN_AGENCY_DESC" runat="server" ControlToValidate="txtINSU_TRANSFERED_IN_AGENCY_DESC"
														Display="Dynamic"></asp:requiredfieldvalidator>
												</TD>
											</tr>
											<!--17-->
											<tr>
												<TD class='midcolora' width='32%'>
													<asp:Label id="capIS_TEMPOLINE" runat="server">Is there a TRAMPOLINE on the premises?</asp:Label></TD>
												<TD class='midcolora' width='18%'>
													<asp:DropDownList id='cmbIS_TEMPOLINE' OnFocus="SelectComboIndex('cmbIS_TEMPOLINE')" runat='server'
														onchange="javascript:EnableDisableDesc(this,document.getElementById('txtIS_TEMPOLINE_DESC'),document.getElementById('lblIS_TEMPOLINE_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:DropDownList>
												</TD>
												<TD class="midcolora" width="32%">
													<asp:label id="capIS_TEMPOLINE_DESC" runat="server">Trampoline Details</asp:label><span class="mandatory" id="spnIS_TEMPOLINE_DESC">*</span></TD>
												<TD class="midcolora" width="18%">
													<asp:textbox id="txtIS_TEMPOLINE_DESC" runat="server" size="28" MaxLength="250"></asp:textbox>
													<asp:label id="lblIS_TEMPOLINE_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
													<asp:requiredfieldvalidator id="rfvIS_TEMPOLINE_DESC" runat="server" ControlToValidate="txtIS_TEMPOLINE_DESC"
														Display="Dynamic"></asp:requiredfieldvalidator>
												</TD>
											</tr>
											<!--18-->
											<tr>
												<TD class='midcolora' width='32%'>
													<asp:Label id="capHAVE_NON_OWNED_AUTO_POL" runat="server">Do you have a Non Owned Auto Policy?</asp:Label></TD>
												<TD class='midcolora' width='18%'>
													<asp:DropDownList id="cmbHAVE_NON_OWNED_AUTO_POL" OnFocus="SelectComboIndex('cmbHAVE_NON_OWNED_AUTO_POL')"
														runat='server' onchange="javascript:EnableDisableDesc(this,document.getElementById('txtHAVE_NON_OWNED_AUTO_POL_DESC'),document.getElementById('lblHAVE_NON_OWNED_AUTO_POL_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:DropDownList>
												</TD>
												<TD class="midcolora" width="32%">
													<asp:label id="capHAVE_NON_OWNED_AUTO_POL_DESC" runat="server">Non Owned Auto Policy Details</asp:label><span class="mandatory" id="spnHAVE_NON_OWNED_AUTO_POL_DESC">*</span></TD>
												<TD class="midcolora" width="18%">
													<asp:textbox id="txtHAVE_NON_OWNED_AUTO_POL_DESC" runat="server" size="28" MaxLength="250"></asp:textbox>
													<asp:label id="lblHAVE_NON_OWNED_AUTO_POL_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
													<asp:requiredfieldvalidator id="rfvHAVE_NON_OWNED_AUTO_POL_DESC" runat="server" ControlToValidate="txtHAVE_NON_OWNED_AUTO_POL_DESC"
														Display="Dynamic"></asp:requiredfieldvalidator>
												</TD>
											</tr>
											<!--19-->
											<tr>
												<TD class='midcolora' width='32%'>
													<asp:Label id="capINS_DOMICILED_OUTSIDE" runat="server">Is the Named insured or applicant  permanently domiciled outside of  Michigan or Indiana for more than 6 months?</asp:Label></TD>
												<TD class='midcolora' width='18%'>
													<asp:DropDownList id="cmbINS_DOMICILED_OUTSIDE" OnFocus="SelectComboIndex('cmbINS_DOMICILED_OUTSIDE')"
														runat='server' onchange="javascript:EnableDisableDesc(this,document.getElementById('txtINS_DOMICILED_OUTSIDE_DESC'),document.getElementById('lblINS_DOMICILED_OUTSIDE_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:DropDownList>
												</TD>
												<TD class="midcolora" width="32%">
													<asp:label id="capINS_DOMICILED_OUTSIDE_DESC" runat="server">Permanently Domiciled Details</asp:label><span class="mandatory" id="spnINS_DOMICILED_OUTSIDE_DESC">*</span></TD>
												<TD class="midcolora" width="18%">
													<asp:textbox id="txtINS_DOMICILED_OUTSIDE_DESC" runat="server" size="28" MaxLength="250"></asp:textbox>
													<asp:label id="lblINS_DOMICILED_OUTSIDE_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
													<asp:requiredfieldvalidator id="rfvINS_DOMICILED_OUTSIDE_DESC" runat="server" ControlToValidate="txtINS_DOMICILED_OUTSIDE_DESC"
														Display="Dynamic"></asp:requiredfieldvalidator>
												</TD>
											</tr>
											<!--20-->
											<tr>
												<TD class='midcolora' width='32%'>
													<asp:Label id="capHOME_DAY_CARE" runat="server">Are you  providing home day care service to a person other than a relative  & receiving money or other compensation?</asp:Label></TD>
												<TD class='midcolora' width='18%'>
													<asp:DropDownList id="cmbHOME_DAY_CARE" OnFocus="SelectComboIndex('cmbHOME_DAY_CARE')" runat='server'
														onchange="javascript:EnableDisableDesc(this,document.getElementById('txtHOME_DAY_CARE_DESC'),document.getElementById('lblHOME_DAY_CARE_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:DropDownList>
												</TD>
												<TD class="midcolora" width="32%">
													<asp:label id="capHOME_DAY_CARE_DESC" runat="server">Home Day Care Details</asp:label><span class="mandatory" id="spnHOME_DAY_CARE_DESC">*</span></TD>
												<TD class="midcolora" width="18%">
													<asp:textbox id="txtHOME_DAY_CARE_DESC" runat="server" size="28" MaxLength="250"></asp:textbox>
													<asp:label id="lblHOME_DAY_CARE_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
													<asp:requiredfieldvalidator id="rfvHOME_DAY_CARE_DESC" runat="server" ControlToValidate="txtHOME_DAY_CARE_DESC"
														Display="Dynamic"></asp:requiredfieldvalidator>
												</TD>
											</tr>
											<!--21-->
											<tr>
												<TD class="midcolora" width="32%"><asp:label ID="capOFFICE_PREMISES" Runat="server">Office Premises </asp:label><%--span class="mandatory" id="spnOFFICE_PREMISES">*</span--%></TD>
												<TD class="midcolora" width="18%" colspan="3"><asp:dropdownlist ID="cmbOFFICE_PREMISES" Runat="server">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="1">1</asp:ListItem>
														<asp:ListItem Value="2">2</asp:ListItem>
														<asp:ListItem Value="3">3</asp:ListItem>
														<asp:ListItem Value="4">4</asp:ListItem>
														<asp:ListItem Value="5">5</asp:ListItem>
														<asp:ListItem Value="6">6</asp:ListItem>
														<asp:ListItem Value="7">7</asp:ListItem>
														<asp:ListItem Value="8">8</asp:ListItem>
														<asp:ListItem Value="9">9</asp:ListItem>
														<asp:ListItem Value="10">10</asp:ListItem>
													</asp:dropdownlist>
												</TD>
											</tr>
											<!--22-->
											<!--Field hide by Swarup as written in Itrack Issue# 1902 -->
											<tr style = display:none>
												<TD class="midcolora" width="32%"><asp:label ID="Label1" Runat="server">Rental Dwellings/Unit </asp:label><%--span class="mandatory" id="spnRENTAL_DWELLINGS_UNIT">*</span--%></TD>
												<TD class="midcolora" width="18%" colspan="3"><asp:dropdownlist ID="cmbRENTAL_DWELLINGS_UNIT" Runat="server">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="1">1</asp:ListItem>
														<asp:ListItem Value="2">2</asp:ListItem>
														<asp:ListItem Value="3">3</asp:ListItem>
														<asp:ListItem Value="4">4</asp:ListItem>
														<asp:ListItem Value="5">5</asp:ListItem>
														<asp:ListItem Value="6">6</asp:ListItem>
														<asp:ListItem Value="7">7</asp:ListItem>
														<asp:ListItem Value="8">8</asp:ListItem>
														<asp:ListItem Value="9">9</asp:ListItem>
														<asp:ListItem Value="10">10</asp:ListItem>
													</asp:dropdownlist>
												</TD>
											</tr>
											<!--23-->
											
											<tr>
												<TD class="midcolora" width="32%"><asp:label id="capCALCULATIONS" runat="server">Calculations</asp:label></TD>
												<TD class="midcolora" width="18%" colspan="3"><asp:textbox onkeypress="MaxLength(this,100);" id="txtCALCULATIONS" runat="server" size="15"
														Columns="20" Rows="4" TextMode="MultiLine" maxlength="200"></asp:textbox><br>
													<asp:customvalidator id="csvCALCULATIONS" Display="Dynamic" ControlToValidate="txtCALCULATIONS" Runat="server"
														ErrorMessage="Please enter only 100 chars." ClientValidationFunction="ValidateCalcMaxLength"></asp:customvalidator></TD>
											</tr>
											<tr>
												<TD class="midcolora" width="32%"><asp:label id="capFAMILIES" runat="server">No of Families</asp:label><span class="mandatory" id="spnFAMILIES">*</span></TD>
												<TD class="midcolora" width="18%" colspan = "3"><asp:textbox id="txtFAMILIES" runat="server" size="2" maxlength="2"></asp:textbox><br>
												<asp:requiredfieldvalidator id="rfvFAMILIES" runat="server" ControlToValidate="txtFAMILIES" Display="Dynamic"></asp:requiredfieldvalidator> 
												<asp:rangevalidator id="rngFAMILIES" runat="server" Display="Dynamic" ControlToValidate="txtFAMILIES"
												MaximumValue="10" MinimumValue="0" Type="Integer"></asp:rangevalidator></td>
											</tr>
											<tr>
												<td class="pageHeader" colSpan="3">Are all the underlying polices with Wolverine 
													for the following Lines of Busines ?
												</td>
											</tr>
									
											<!--1-->
											<tr>
												<TD class="midcolora" width="32%"><asp:label id="capHOME_RENT_DWELL" runat="server">Home or Rental dwellings?</asp:label><span class="mandatory">*</span></TD>
												<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbHOME_RENT_DWELL" onfocus="SelectComboIndex('cmbHOME_RENT_DWELL')" runat="server"
														onchange="javascript:DisableEnableDesc(this,document.getElementById('txtHOME_RENT_DWELL_DESC'),document.getElementById('lblHOME_RENT_DWELL_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="0">N/A</asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:dropdownlist><br>
													<asp:requiredfieldvalidator id="rfvHOME_RENT_DWELL" runat="server" ControlToValidate="cmbHOME_RENT_DWELL" Display="Dynamic"></asp:requiredfieldvalidator>
												</TD>
												<TD class="midcolora" width="32%"><asp:label id="capHOME_RENT_DWELL_DESC" runat="server">Home/Rental Dwelling Details</asp:label><span class="mandatory" id="spnHOME_RENT_DWELL_DESC">*</span></TD>
												<TD class="midcolora" width="18%"><asp:textbox id="txtHOME_RENT_DWELL_DESC" runat="server" MaxLength="250" size="28"></asp:textbox><asp:label id="lblHOME_RENT_DWELL_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
													<asp:requiredfieldvalidator id="rfvHOME_RENT_DWELL_DESC" runat="server" ControlToValidate="txtHOME_RENT_DWELL_DESC"
														Display="Dynamic"></asp:requiredfieldvalidator></TD>
											</tr>
											<!--2-->
											<tr>
												<TD class="midcolora" width="32%"><asp:label id="capWAT_DWELL" runat="server">Watercraft Dwellings?</asp:label><span class="mandatory">*</span></TD>
												<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbWAT_DWELL" onfocus="SelectComboIndex('cmbWAT_DWELL')" runat="server" onchange="javascript:DisableEnableDesc(this,document.getElementById('txtWAT_DWELL_DESC'),document.getElementById('lblWAT_DWELL_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="0">N/A</asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:dropdownlist><br>
													<asp:requiredfieldvalidator id="rfvWAT_DWELL" runat="server" ControlToValidate="cmbWAT_DWELL" Display="Dynamic"></asp:requiredfieldvalidator>
												</TD>
												<TD class="midcolora" width="32%"><asp:label id="capWAT_DWELL_DESC" runat="server">Watercraft Dwelling Details</asp:label><span class="mandatory" id="spnWAT_DWELL_DESC">*</span></TD>
												<TD class="midcolora" width="18%"><asp:textbox id="txtWAT_DWELL_DESC" runat="server" MaxLength="250" size="28"></asp:textbox><asp:label id="lblWAT_DWELL_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
													<asp:requiredfieldvalidator id="rfvWAT_DWELL_DESC" runat="server" ControlToValidate="txtWAT_DWELL_DESC" Display="Dynamic"></asp:requiredfieldvalidator></TD>
											</tr>
											<!--3-->
											<tr>
												<TD class="midcolora" width="32%"><asp:label id="capRECR_VEH" runat="server">Recreational vehicles Details ?</asp:label><span class="mandatory">*</span></TD>
												<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbRECR_VEH" onfocus="SelectComboIndex('cmbRECR_VEH')" runat="server" onchange="javascript:DisableEnableDesc(this,document.getElementById('txtRECR_VEH_DESC'),document.getElementById('lblRECR_VEH_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="0">N/A</asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:dropdownlist><br>
													<asp:requiredfieldvalidator id="rfvRECR_VEH" runat="server" ControlToValidate="cmbRECR_VEH" Display="Dynamic"></asp:requiredfieldvalidator>
												</TD>
												<TD class="midcolora" width="32%"><asp:label id="capRECR_VEH_DESC" runat="server">Recr Veh Details</asp:label><span class="mandatory" id="spnRECR_VEH_DESC">*</span></TD>
												<TD class="midcolora" width="18%"><asp:textbox id="txtRECR_VEH_DESC" runat="server" MaxLength="250" size="28"></asp:textbox><asp:label id="lblRECR_VEH_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
													<asp:requiredfieldvalidator id="rfvRECR_VEH_DESC" runat="server" ControlToValidate="txtRECR_VEH_DESC" Display="Dynamic"></asp:requiredfieldvalidator></TD>
											</tr>
											<!--4-->
											<tr>
												<TD class="midcolora" width="32%"><asp:label id="capAUTO_CYCL_TRUCKS" runat="server">Automobiles/Cycles/Trucks?</asp:label><span class="mandatory">*</span></TD>
												<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbAUTO_CYCL_TRUCKS" onfocus="SelectComboIndex('cmbAUTO_CYCL_TRUCKS')" runat="server"
														onchange="javascript:DisableEnableDesc(this,document.getElementById('txtAUTO_CYCL_TRUCKS_DESC'),document.getElementById('lblAUTO_CYCL_TRUCKS_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="0">N/A</asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:dropdownlist><br>
													<asp:requiredfieldvalidator id="rfvAUTO_CYCL_TRUCKS" runat="server" ControlToValidate="cmbAUTO_CYCL_TRUCKS"
														Display="Dynamic"></asp:requiredfieldvalidator>
												</TD>
												<TD class="midcolora" width="32%"><asp:label id="capAUTO_CYCL_TRUCKS_DESC" runat="server">Automobiles/Cycles/Trucks Details</asp:label><span class="mandatory" id="spnAUTO_CYCL_TRUCKS_DESC">*</span></TD>
												<TD class="midcolora" width="18%"><asp:textbox id="txtAUTO_CYCL_TRUCKS_DESC" runat="server" MaxLength="250" size="28"></asp:textbox><asp:label id="lblAUTO_CYCL_TRUCKS_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
													<asp:requiredfieldvalidator id="rfvAUTO_CYCL_TRUCKS_DESC" runat="server" ControlToValidate="txtAUTO_CYCL_TRUCKS_DESC"
														Display="Dynamic"></asp:requiredfieldvalidator></TD>
											</tr>
											<!--5-->
											<tr>
												<TD class="midcolora" width="32%"><asp:label id="capAPPLI_UNDERSTAND_LIABILITY_EXCLUDED" runat="server">Does applicant understand that Business Pursuits, Professional Liability & Aircraft Liability are excluded?</asp:label><span class="mandatory">*</span></TD>
												<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbAPPLI_UNDERSTAND_LIABILITY_EXCLUDED" onfocus="SelectComboIndex('cmbAUTO_CYCL_TRUCKS')"
														runat="server" onchange="javascript:DisableEnableDesc(this,document.getElementById('txtAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC'),document.getElementById('lblAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC'));">
														<asp:ListItem></asp:ListItem>
														<asp:ListItem Value="Y">Yes</asp:ListItem>
														<asp:ListItem Value="N">No</asp:ListItem>
													</asp:dropdownlist><br>
													<asp:requiredfieldvalidator id="rfvAPPLI_UNDERSTAND_LIABILITY_EXCLUDED" runat="server" ControlToValidate="cmbAPPLI_UNDERSTAND_LIABILITY_EXCLUDED"
														Display="Dynamic"></asp:requiredfieldvalidator>
												</TD>
												<TD class="midcolora" width="32%"><asp:label id="capAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC" runat="server">Understand Coverage Details</asp:label><span class="mandatory" id="spnAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC">*</span></TD>
												<TD class="midcolora" width="18%"><asp:textbox id="txtAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC" runat="server" MaxLength="250"
														size="28"></asp:textbox><asp:label id="lblAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
													<asp:requiredfieldvalidator id="rfvAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC" runat="server" ControlToValidate="txtAPPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC"
														Display="Dynamic"></asp:requiredfieldvalidator></TD>
											</tr>
											<!--6-->
											<tr>
												<TD class="midcolora" width="32%"><asp:label id="capUND_REMARKS" runat="server">Remarks</asp:label></TD>
												<TD class="midcolora" width="18%" colspan="3"><asp:textbox onkeypress="MaxLength(this,500);" id="txtUND_REMARKS" runat="server" size="15" Columns="20"
														Rows="4" TextMode="MultiLine" maxlength="250"></asp:textbox><br>
														<asp:customvalidator id="csvUND_REMARKS" Display="Dynamic" ControlToValidate="txtUND_REMARKS" Runat="server"
														ErrorMessage="Please enter only 500 chars." ClientValidationFunction="ValidateRemarksMaxLength"></asp:customvalidator></TD>
											</tr>
											<tr>
												<td class='midcolora' colspan='1'>
													<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
												</td>
												<td class='midcolora' colspan='1'>
												</td>
												<td class='midcolorr' colspan="2">
													<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
												</td>
											</tr>
											<tr>
												<td colspan="4">
													<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
													<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
													<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCustomerID" type="hidden" runat="server" NAME="hidCustomerID">
													<INPUT id="hidPolicyID" type="hidden" runat="server" NAME="hidPolicyID"> <INPUT id="hidPolicyVersionID" type="hidden" runat="server" NAME="hidPolicyVersionID">
												</td>
											</tr>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</FORM>
					</td>
				</tr>
			</table>
		</div>
	</BODY>
</HTML>



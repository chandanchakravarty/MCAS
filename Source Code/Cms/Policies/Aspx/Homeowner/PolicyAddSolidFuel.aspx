<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PolicyAddSolidFuel.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.aspx.HomeOwners.PolicyAddSolidFuel" validateRequest=false %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>BRICS-ADD SOLID FUEL</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		
		
		//This function used to disable or enable the desc fields
		//if value of combo box passes is Y(yes) thenit enable the specified
		//description fields else disable it
		function EnableDisableDesc(cmbCombo, txtDesc,lblNA,rfvDesc)
		{	
			
			if (cmbCombo.selectedIndex > -1)
			{	
				
				//Checking value only if item is selected
				if (cmbCombo.options[cmbCombo.selectedIndex].text == "Other (Describe)")
				{
					//Disabling the description field, if No is selected
					//alert("Disabling");
					//rfvDesc.setAttribute('enabled',true);
					//rfvDesc.setAttribute('isValid',true);
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
					//Enabling the description field, if yes is selected
					//alert("Enabling");
					//rfvDesc.setAttribute('isValid',false);
				//	rfvDesc.style.display='none';			
					rfvDesc.setAttribute('enabled',false);										
					txtDesc.style.display = "none";
					lblNA.style.display = "inline";
					lblNA.innerHTML="NA";
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
			
			document.getElementById('hidFUEL_ID').value	=	'New';
			document.getElementById('txtMANUFACTURER').value  = '';
			document.getElementById('txtBRAND_NAME').value  = '';
			document.getElementById('txtMODEL_NUMBER').value  = '';
			document.getElementById('cmbFUEL').selectedIndex = -1;
			document.getElementById('cmbSTOVE_TYPE').options.selectedIndex = -1;
			document.getElementById('cmbHAVE_LABORATORY_LABEL').options.selectedIndex = -1;
			document.getElementById('cmbIS_UNIT').options.selectedIndex = -1;
			document.getElementById('txtUNIT_OTHER_DESC').value= '';
			document.getElementById('cmbCONSTRUCTION').options.selectedIndex = -1;
			document.getElementById('cmbLOCATION').options.selectedIndex = -1;
			document.getElementById('txtLOC_OTHER_DESC').value = '';
			document.getElementById('txtYEAR_DEVICE_INSTALLED').value = '';
			document.getElementById('cmbWAS_PROF_INSTALL_DONE').options.selectedIndex = -1;
			document.getElementById('cmbINSTALL_INSPECTED_BY').options.selectedIndex = -1;
			document.getElementById('txtINSTALL_OTHER_DESC').value = '';
			document.getElementById('cmbHEATING_USE').options.selectedIndex = -1;
			document.getElementById('cmbHEATING_SOURCE').options.selectedIndex = -1;
			document.getElementById('txtOTHER_DESC').value = '';
			if(document.getElementById('btnActivateDeactivate'))
				document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			if(document.getElementById('btnDelete') && document.getElementById('hidOldData').value=='')//Condition added by Charles on 21-Oct-09 for Itrack 6599
				document.getElementById('btnDelete').setAttribute('disabled',true);
			if(document.getElementById('hidFormSaved').value != "5")
			document.getElementById('txtMANUFACTURER').focus();
			document.getElementById('cmbSTOVE_INSTALLATION_CONFORM_SPECIFICATIONS').options.selectedIndex = -1;
			
				
			ChangeColor();
			DisableValidators();
			
			
			//document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			}
			function populateXML()
			{
			    //alert(document.getElementById('hidOldData').value);
			    
				//if(document.getElementById('hidFormSaved').value == '0')
				//{
	
					if(document.getElementById('hidOldData').value!="")
					{
					
						populateFormData(document.getElementById('hidOldData').value,POL_HOME_OWNER_SOLID_FUEL);
					}    
		       
					else
					{
						 AddData();
					}
				//}
		
				EnableDisableCtrls();
				return false;
			}
			function EnableDisableCtrls()
			{
				EnableDisableDesc(document.getElementById('cmbIS_UNIT'),document.getElementById('txtUNIT_OTHER_DESC'),document.getElementById('lblUNIT_OTHER_DESC'),document.getElementById('rfvUNIT_OTHER_DESC'));
				EnableDisableDesc(document.getElementById('cmbLOCATION'),document.getElementById('txtLOC_OTHER_DESC'),document.getElementById('lblLOC_OTHER_DESC'),document.getElementById('rfvLOC_OTHER_DESC'));
				EnableDisableDesc(document.getElementById('cmbINSTALL_INSPECTED_BY'),document.getElementById('txtINSTALL_OTHER_DESC'),document.getElementById('lblINSTALL_OTHER_DESC'),document.getElementById('rfvINSTALL_OTHER_DESC'));
				EnableDisableDesc(document.getElementById('cmbHEATING_SOURCE'),document.getElementById('txtOTHER_DESC'),document.getElementById('lblOTHER_DESC'),document.getElementById('rfvOTHER_DESC'));				
			}
			
			
		function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbSTOVE_TYPE":
						lookupMessage	=	"STOVET.";
						break;
					case "cmbIS_UNIT":
						lookupMessage	=	"ISUNIT.";
						break;
					case "cmbCONSTRUCTION":
						lookupMessage	=	"DIVCON.";
						break;
					case "cmbLOCATION":
						lookupMessage	=	"DIVLOC.";
						break;
					case "cmbINSTALL_INSPECTED_BY":
						lookupMessage	=	"DIVINS.";
						break;
					case "cmbHEATING_USE":
						lookupMessage	=	"DIVHEA.";
						break;
					case "cmbHEATING_SOURCE":
						lookupMessage	=	"DHSRC.";
						break;					
					default:
						lookupMessage	=	"Look up code not found";
						break;
						
				}
				showLookupLayer(controlId,lookupMessage);							
			}

		</script>
</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();">
		<FORM id="POL_HOME_OWNER_SOLID_FUEL" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
					<td class="midcolorc" align="right" colSpan="4">
						<asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label>
					</td>
			</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4">
										<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
										Please note that all fields marked with * are mandatory
									</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMANUFACTURER" runat="server">Manufacturer</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMANUFACTURER" runat="server" size="30" maxlength="100"></asp:textbox>
										<br>
										<asp:requiredfieldvalidator id="rfvMANUFACTURER" runat="server" Display="Dynamic" ErrorMessage="MODEL can't be blank."
											ControlToValidate="txtMANUFACTURER"></asp:requiredfieldvalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capBRAND_NAME" runat="server">Brand Name</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtBRAND_NAME" runat="server" size="30" maxlength="75"></asp:textbox>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMODEL_NUMBER" runat="server">Model Number</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMODEL_NUMBER" runat="server" size="30" maxlength="35"></asp:textbox>
										<br>
										<asp:requiredfieldvalidator id="rfvMODEL_NUMBER" runat="server" Display="Dynamic" ErrorMessage="MODEL can't be blank."
											ControlToValidate="txtMODEL_NUMBER"></asp:requiredfieldvalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capFUEL" runat="server">Fuel</asp:label></TD>
									<TD class="midcolora" width="32%">
										<asp:DropDownList id="cmbFUEL" runat="server"></asp:DropDownList>							
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capSTOVE_TYPE" runat="server">Stove Type</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%">
										<asp:dropdownlist id="cmbSTOVE_TYPE" onfocus="SelectComboIndex('cmbSTOVE_TYPE')" runat="server"></asp:dropdownlist>
										<a class="calcolora" href="javascript:showPageLookupLayer('cmbSTOVE_TYPE')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
										<br>
										<asp:requiredfieldvalidator id="rfvSTOVE_TYPE" runat="server" Display="Dynamic" ErrorMessage="Please select stove type."
											ControlToValidate="cmbSTOVE_TYPE"></asp:requiredfieldvalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capHAVE_LABORATORY_LABEL" runat="server">Does the unit have a testing laboratory label (UL, other)</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbHAVE_LABORATORY_LABEL" onfocus="SelectComboIndex('cmbHAVE_LABORATORY_LABEL')"
											runat="server">
											<asp:ListItem Value=''></asp:ListItem>
											<asp:ListItem Value='N'>No</asp:ListItem>
											<asp:ListItem Value='Y'>Yes</asp:ListItem>
										</asp:dropdownlist>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capIS_UNIT" runat="server">Is the unit</asp:label></TD>
									<TD class="midcolora" width="32%">
										<asp:dropdownlist id="cmbIS_UNIT" onfocus="SelectComboIndex('cmbIS_UNIT')" runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtUNIT_OTHER_DESC'),document.getElementById('lblUNIT_OTHER_DESC'),document.getElementById('rfvUNIT_OTHER_DESC'));"></asp:dropdownlist>
										<a class="calcolora" href="javascript:showPageLookupLayer('cmbIS_UNIT')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capUNIT_OTHER_DESC" runat="server">Other Description</asp:label><span id=spnUNIT_OTHER_DESC class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtUNIT_OTHER_DESC" runat="server" MaxLength="200"></asp:textbox><asp:label id="lblUNIT_OTHER_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<br>
										<asp:requiredfieldvalidator id="rfvUNIT_OTHER_DESC" runat="server" Display="Dynamic" ErrorMessage="Please select construction."
											ControlToValidate="txtUNIT_OTHER_DESC"></asp:requiredfieldvalidator>
									</TD>
					</TD>
				</TR>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capLOCATION" runat="server">Location</asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%">
						<asp:dropdownlist id="cmbLOCATION" onfocus="SelectComboIndex('cmbLOCATION')" runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtLOC_OTHER_DESC'),document.getElementById('lblLOC_OTHER_DESC'),document.getElementById('rfvLOC_OTHER_DESC'));"></asp:dropdownlist>
						<a class="calcolora" href="javascript:showPageLookupLayer('cmbLOCATION')"></a>
						<br>
						<asp:requiredfieldvalidator id="rfvLOCATION" runat="server" Display="Dynamic" ErrorMessage="Please select location."
							ControlToValidate="cmbLOCATION"></asp:requiredfieldvalidator>
					</TD>
					<TD class="midcolora" width="18%"><asp:label id="capLOC_OTHER_DESC" runat="server">Other Description</asp:label><span id= spnLOC_OTHER_DESC class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtLOC_OTHER_DESC" runat="server" MaxLength="200"></asp:textbox><asp:label id="lblLOC_OTHER_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
						<br>
						<asp:requiredfieldvalidator id="rfvLOC_OTHER_DESC" runat="server" Display="Dynamic" ErrorMessage="Please select construction."
							ControlToValidate="txtLOC_OTHER_DESC"></asp:requiredfieldvalidator>
					</TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capCONSTRUCTION" runat="server">Construction</asp:label></TD>
					<TD class="midcolora" width="32%">
					<asp:dropdownlist id="cmbCONSTRUCTION" onfocus="SelectComboIndex('cmbCONSTRUCTION')" runat="server"></asp:dropdownlist>
						<a class="calcolora" href="javascript:showPageLookupLayer('cmbCONSTRUCTION')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
					</TD>
					<TD class="midcolora" width="18%"><asp:label id="capYEAR_DEVICE_INSTALLED" runat="server">Year Device Installed</asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtYEAR_DEVICE_INSTALLED" runat="server" MaxLength="4" size="4"></asp:textbox><br>						
						<asp:requiredfieldvalidator id="rfvYEAR_DEVICE_INSTALLED" runat="server" Display="Dynamic" ErrorMessage="Please select construction."
							ControlToValidate="txtYEAR_DEVICE_INSTALLED"></asp:requiredfieldvalidator>
						<asp:rangevalidator id="rngYEAR_DEVICE_INSTALLED" runat="server" ControlToValidate="txtYEAR_DEVICE_INSTALLED"
							Display="Dynamic"></asp:rangevalidator></TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capINSTALL_INSPECTED_BY" runat="server">Installation was inspected by</asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%">
						<asp:dropdownlist id="cmbINSTALL_INSPECTED_BY" onfocus="SelectComboIndex('cmbINSTALL_INSPECTED_BY')"
							runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtINSTALL_OTHER_DESC'),document.getElementById('lblINSTALL_OTHER_DESC'),document.getElementById('rfvINSTALL_OTHER_DESC'));"></asp:dropdownlist>
						<a class="calcolora" href="javascript:showPageLookupLayer('cmbINSTALL_INSPECTED_BY')">
							<img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
						<br>
						<asp:requiredfieldvalidator id="rfvINSTALL_INSPECTED_BY" runat="server" Display="Dynamic" ErrorMessage="Please select inspected by."
							ControlToValidate="cmbINSTALL_INSPECTED_BY"></asp:requiredfieldvalidator>
					</TD>
					<TD class="midcolora" width="18%"><asp:label id="capINSTALL_OTHER_DESC" runat="server">Other Description</asp:label><span id=spnINSTALL_OTHER_DESC class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtINSTALL_OTHER_DESC" runat="server" MaxLength="200"></asp:textbox><asp:label id="lblINSTALL_OTHER_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
						<br>
						<asp:requiredfieldvalidator id="rfvINSTALL_OTHER_DESC" runat="server" Display="Dynamic" ErrorMessage="Please select construction."
							ControlToValidate="txtINSTALL_OTHER_DESC"></asp:requiredfieldvalidator>
					</TD></TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capWAS_PROF_INSTALL_DONE" runat="server">Was installation done by a professional installer such as a contractor?</asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbWAS_PROF_INSTALL_DONE" onfocus="SelectComboIndex('cmbWAS_PROF_INSTALL_DONE')"
							runat="server">
							<asp:ListItem Value=''></asp:ListItem>
							<asp:ListItem Value='N'>No</asp:ListItem>
							<asp:ListItem Value='Y'>Yes</asp:ListItem>
						</asp:dropdownlist>
						<br>
						<asp:requiredfieldvalidator id="rfvWAS_PROF_INSTALL_DONE" runat="server" Display="Dynamic" ErrorMessage="Please select stove type."
							ControlToValidate="cmbWAS_PROF_INSTALL_DONE"></asp:requiredfieldvalidator>
					</TD>
					<TD class="midcolora" width="18%"><asp:label id="capHEATING_USE" runat="server">Heating use</asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%">
						<asp:dropdownlist id="cmbHEATING_USE" onfocus="SelectComboIndex('cmbHEATING_USE')" runat="server"></asp:dropdownlist>
						<a class="calcolora" href="javascript:showPageLookupLayer('cmbHEATING_USE')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
						<BR>
						<asp:requiredfieldvalidator id="rfvHEATING_USE" runat="server" Display="Dynamic" ErrorMessage="Please select stove type."
							ControlToValidate="cmbHEATING_USE"></asp:requiredfieldvalidator>
					</TD>
				<TR>
					<TD class="midcolora" width="18%"><asp:label id="capHEATING_SOURCE" runat="server">What other type of heating source is used?</asp:label><span id="spnHEATING_SOURCE'" class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%">
						<asp:dropdownlist id="cmbHEATING_SOURCE" onfocus="SelectComboIndex('cmbHEATING_SOURCE')" runat="server"
							onchange="javascript:EnableDisableDesc(this,document.getElementById('txtOTHER_DESC'),document.getElementById('lblOTHER_DESC'),document.getElementById('rfvOTHER_DESC'));"></asp:dropdownlist>
						<a class="calcolora" href="javascript:showPageLookupLayer('cmbHEATING_SOURCE')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
						<br>
						<asp:requiredfieldvalidator id="rfvHEATING_SOURCE" runat="server" Display="Dynamic" ErrorMessage="Please select stove type."
							ControlToValidate="cmbHEATING_SOURCE"></asp:requiredfieldvalidator>
					</TD>
					<TD class="midcolora" width="18%"><asp:label id="capOTHER_DESC" runat="server">Other Description</asp:label><span id=spnOTHER_DESC class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtOTHER_DESC" runat="server" MaxLength="200"></asp:textbox><asp:label id="lblOTHER_DESC" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
						<br>
						<asp:requiredfieldvalidator id="rfvOTHER_DESC" runat="server" Display="Dynamic" ErrorMessage="Please select construction."
							ControlToValidate="txtOTHER_DESC"></asp:requiredfieldvalidator>
					</TD></TD></TR>
					<tr>
						<td class="midcolora" colspan="3">
							<asp:Label Runat="server" ID ="capSTOVE_INSTALLATION_CONFORM_SPECIFICATIONS">
							Does the stove installation and use conform to all of its manufacturers	specifications and local fire codes? 
							</asp:Label>
						</td>
						<td class="midcolora">
							<asp:dropdownlist id="cmbSTOVE_INSTALLATION_CONFORM_SPECIFICATIONS" onfocus="SelectComboIndex('cmbSTOVE_INSTALLATION_CONFORM_SPECIFICATIONS')" runat="server"
							></asp:dropdownlist>
						</td>
					</tr>
					
				<tr>
					<td class="midcolora" colSpan="2">
						<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
						<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" Runat="server" CausesValidation="False" text="Activate/Deactivate"></cmsb:cmsbutton>
					</td>
					<td class="midcolorr" colSpan="2">
					<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" CausesValidation="False"></cmsb:cmsbutton>
					<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
					</td>
				</tr>
			</TABLE></TD></TR></TBODY></TABLE><INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidFUEL_ID" type="hidden" value="0" name="hidFUEL_ID" runat="server">
			<INPUT id="hidPOL_ID" type="hidden" name="hidPOL_ID" runat="server"> <INPUT id="hidPOL_VERSION_ID" type="hidden" name="hidPOL_VERSION_ID" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" name="hidCUSTOMER_ID" runat="server"> <INPUT id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server">
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none;Z-INDEX: 101;POSITION: absolute">
			<iframe id="lookupLayerIFrame" style="DISPLAY: none;Z-INDEX: 1001;FILTER: alpha(opacity=0);BACKGROUND-COLOR: #000000"
				width="0" height="0" top="0px;" left="0px"></iframe>
		</div>
		<DIV id="lookupLayer" style="Z-INDEX: 101;VISIBILITY: hidden;POSITION: absolute" onmouseover="javascript:refreshLookupLayer();">
			<table class="TabRow" cellspacing="0" cellpadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b>Add LookUp</b></td>
					<td><p align="right"><a href="javascript:void(0)" onclick="javascript:hideLookupLayer();"><img src="/cms/cmsweb/images/cross.gif" border="0" alt="Close" WIDTH="16" HEIGHT="14"></a></p>
					</td>
				</tr>
				<tr class="SubTabRow">
					<td colspan="2">
						<span id="LookUpMsg"></span>
					</td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->
		<script>
				RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidFUEL_ID').value, false);
		</script>
	</BODY>
</HTML>

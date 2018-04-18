<%@ Page language="c#" Codebehind="PolcyInlandMarineUnderwritingQue.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policy.Aspx.Homeowners.PolcyInlandMarineUnderwritingQue" validateRequest = "false"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>BRICS</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language='javascript'>
		
		function ClearForm()
		{
			
			Reset();
			return false;
			
		}
		
		//Commented by mohit on 17/10/2005
		//This function used to disable or enable the desc fields
		//if value of combo box passes is Y(yes) thenit enable the specified
		//description fields else disable it
		/*function EnableDisableDesc(cmbCombo, txtDesc)
		{	
			//alert('hi');
			if (cmbCombo.selectedIndex > -1)
			{
				//Checking value only if item is selected
				if (cmbCombo.options[cmbCombo.selectedIndex].value == "N" || cmbCombo.options[cmbCombo.selectedIndex].value == '')
				{
					//Disabling the description field, if No is selected
					//txtDesc.setAttribute('disabled',true);
					txtDesc.readOnly = true;
					txtDesc.value = "";
				}
				else
				{
					//Enabling the description field, if yes is selected
					//txtDesc.setAttribute('disabled',false);
					txtDesc.readOnly = false;
				}
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
		}
		
		function AddData()
		{
			document.getElementById('cmbPROPERTY_EXHIBITED').options.selectedIndex = -1;
			document.getElementById('txtDESC_PROPERTY_EXHIBITED').value  = '';
			document.getElementById('cmbDEDUCTIBLE_APPLY').options.selectedIndex = -1;
			document.getElementById('txtDESC_DEDUCTIBLE_APPLY').value  = '';
			document.getElementById('cmbPROPERTY_USE_PROF_COMM').options.selectedIndex = -1;
			document.getElementById('cmbOTHER_INSU_WITH_COMPANY').options.selectedIndex = -1;
			document.getElementById('txtDESC_INSU_WITH_COMPANY').value  = '';
			document.getElementById('cmbLOSS_OCCURED_LAST_YEARS').options.selectedIndex = -1;
			document.getElementById('txtDESC_LOSS_OCCURED_LAST_YEARS').value  = '';
			document.getElementById('cmbDECLINED_CANCELED_COVERAGE').options.selectedIndex = -1;
			document.getElementById('txtADD_RATING_COV_INFO').value  = '';
			
			DisableValidators();
			ChangeColor();
			document.getElementById('cmbPROPERTY_EXHIBITED').focus();
			
		
			
			
		}
		
		function Reset()
		{
				if(document.getElementById('hidOldData').value != "")
				{
					populateFormData(document.getElementById('hidOldData').value, APP_HOME_OWNER_PER_ART_GEN_INFO);	
				}
				else
				{
					AddData();
				}
			//}
			EnableDisableDesc(document.getElementById('cmbPROPERTY_EXHIBITED'),document.getElementById('txtDESC_PROPERTY_EXHIBITED'),document.getElementById('lblDESC_PROPERTY_EXHIBITED'));
			EnableDisableDesc(document.getElementById('cmbDEDUCTIBLE_APPLY'),document.getElementById('txtDESC_DEDUCTIBLE_APPLY'),document.getElementById('lblDESC_DEDUCTIBLE_APPLY'));
			EnableDisableDesc(document.getElementById('cmbOTHER_INSU_WITH_COMPANY'),document.getElementById('txtDESC_INSU_WITH_COMPANY'),document.getElementById('lblDESC_INSU_WITH_COMPANY'));
			EnableDisableDesc(document.getElementById('cmbLOSS_OCCURED_LAST_YEARS'),document.getElementById('txtDESC_LOSS_OCCURED_LAST_YEARS'),document.getElementById('lblDESC_LOSS_OCCURED_LAST_YEARS'));
			EnableDisableDesc(document.getElementById('cmbPROPERTY_USE_PROF_COMM'),document.getElementById('txtDESC_PROPERTY_USE_PROF_COMM'),document.getElementById('lblDESC_PROPERTY_USE_PROF_COMM'))
			return false;
		}
		
		function Initialize()
		{
			//if(document.getElementById('hidFormSaved').value == '0')
			//{
				if(document.getElementById('hidOldData').value != "")
				{
					//populateFormData(document.getElementById('hidOldData').value, APP_HOME_OWNER_PER_ART_GEN_INFO);
					
					//Enabling or disabling the description fields
					//EnableDisableDesc(document.APP_HOME_OWNER_PER_ART_GEN_INFO.cmbPROPERTY_EXHIBITED, document.APP_HOME_OWNER_PER_ART_GEN_INFO.txtDESC_PROPERTY_EXHIBITED);
					//EnableDisableDesc(document.APP_HOME_OWNER_PER_ART_GEN_INFO.cmbDEDUCTIBLE_APPLY, document.APP_HOME_OWNER_PER_ART_GEN_INFO.txtDESC_DEDUCTIBLE_APPLY);
					//EnableDisableDesc(document.APP_HOME_OWNER_PER_ART_GEN_INFO.cmbOTHER_INSU_WITH_COMPANY, document.APP_HOME_OWNER_PER_ART_GEN_INFO.txtDESC_INSU_WITH_COMPANY);
					//EnableDisableDesc(document.APP_HOME_OWNER_PER_ART_GEN_INFO.cmbLOSS_OCCURED_LAST_YEARS, document.APP_HOME_OWNER_PER_ART_GEN_INFO.txtDESC_LOSS_OCCURED_LAST_YEARS);
					
				}
				else
				{
					AddData();
				}
			//}
			EnableDisableDesc(document.getElementById('cmbPROPERTY_EXHIBITED'),document.getElementById('txtDESC_PROPERTY_EXHIBITED'),document.getElementById('lblDESC_PROPERTY_EXHIBITED'));
			EnableDisableDesc(document.getElementById('cmbDEDUCTIBLE_APPLY'),document.getElementById('txtDESC_DEDUCTIBLE_APPLY'),document.getElementById('lblDESC_DEDUCTIBLE_APPLY'));
			EnableDisableDesc(document.getElementById('cmbOTHER_INSU_WITH_COMPANY'),document.getElementById('txtDESC_INSU_WITH_COMPANY'),document.getElementById('lblDESC_INSU_WITH_COMPANY'));
			EnableDisableDesc(document.getElementById('cmbLOSS_OCCURED_LAST_YEARS'),document.getElementById('txtDESC_LOSS_OCCURED_LAST_YEARS'),document.getElementById('lblDESC_LOSS_OCCURED_LAST_YEARS'));
			EnableDisableDesc(document.getElementById('cmbPROPERTY_USE_PROF_COMM'),document.getElementById('txtDESC_PROPERTY_USE_PROF_COMM'),document.getElementById('lblDESC_PROPERTY_USE_PROF_COMM'))
			return false;
		}
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Initialize();ApplyColor();top.topframe.main1.mousein = false;findMouseIn();showScroll();">
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<FORM id='APP_HOME_OWNER_PER_ART_GEN_INFO' method='post' runat='server'>
				<TABLE cellSpacing="0" cellPadding="0" align="center" class="tableWidth" border="0">
					<TR id="trMsg" runat="server">
						<td>
							<asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label>
						</td>
					</TR>
					<TR id="trBody" runat="server">
						<TD>
							<TABLE class="tableWidthHeader" align="center" border="0">
								<tr>
									<td id="tdWorkflow" class="pageHeader" colspan="4">
										<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
									</td>
								</tr>
								<tr>
									<TD id="tdClientTop" class="pageHeader" colSpan="4">
										<webcontrol:ClientTop id="cltClientTop" runat="server" width="98%"></webcontrol:ClientTop>
										<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
									</TD>
								</tr>
								<tr>
									<!--<td class="headereffectCenter" colSpan="4">Underwriting Questions</td>-->
									<td class="headereffectCenter" colSpan="4">Inland Marine - Underwriting Questions</td>
								</tr>
								<tr>
									<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
										mandatory</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr>
									<TD class='midcolora' width='32%'>
										<asp:Label id="capPROPERTY_EXHIBITED" runat="server">Exhibited</asp:Label></TD>
									<TD class='midcolora' width='18%'>
										<asp:DropDownList id='cmbPROPERTY_EXHIBITED' onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_PROPERTY_EXHIBITED'),document.getElementById('lblDESC_PROPERTY_EXHIBITED'));"
											OnFocus="SelectComboIndex('cmbPROPERTY_EXHIBITED');" runat='server'></asp:DropDownList>
									</TD>
									<TD class='midcolora' width='32%'>
										<asp:Label id="capDESC_PROPERTY_EXHIBITED" runat="server">Property</asp:Label><span class="mandatory" id="spnDESC_PROPERTY_EXHIBITED">*</span></TD>
									<TD class='midcolora' width='18%'>
										<asp:textbox id='txtDESC_PROPERTY_EXHIBITED' runat='server' size='30' maxlength='255'></asp:textbox>
										<asp:label id="lblDESC_PROPERTY_EXHIBITED" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator ID="rfvDESC_PROPERTY_EXHIBITED" ControlToValidate="txtDESC_PROPERTY_EXHIBITED" Runat="server"
											Display="Dynamic"></asp:RequiredFieldValidator>
									</TD>
								</tr>
								<tr>
									<TD class='midcolora' width='32%'>
										<asp:Label id="capDEDUCTIBLE_APPLY" runat="server">Apply</asp:Label><span class="mandatory">*</span></TD>
									<TD class='midcolora' width='18%'>
										<asp:DropDownList id='cmbDEDUCTIBLE_APPLY' onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_DEDUCTIBLE_APPLY'),document.getElementById('lblDESC_DEDUCTIBLE_APPLY'));"
											OnFocus="SelectComboIndex('cmbDEDUCTIBLE_APPLY');" runat='server'></asp:DropDownList><br>
										<asp:RequiredFieldValidator ID="rfvDEDUCTIBLE_APPLY" Runat="server" ControlToValidate="cmbDEDUCTIBLE_APPLY"
											Display="Dynamic"></asp:RequiredFieldValidator>
									</TD>
									<TD class='midcolora' width='32%'>
										<asp:Label id="capDESC_DEDUCTIBLE_APPLY" runat="server">Deductible</asp:Label><span class="mandatory" id="spnDESC_DEDUCTIBLE_APPLY">*</span></TD>
									<TD class='midcolora' width='18%'>
										<asp:textbox id='txtDESC_DEDUCTIBLE_APPLY' runat='server' size='30' maxlength='255'></asp:textbox>
										<asp:label id="lblDESC_DEDUCTIBLE_APPLY" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator ID="rfvDESC_DEDUCTIBLE_APPLY" ControlToValidate="txtDESC_DEDUCTIBLE_APPLY" Runat="server"
											Display="Dynamic"></asp:RequiredFieldValidator>
									</TD>
								</tr>
								<tr>
									<TD class='midcolora' width='32%'>
										<asp:Label id="capPROPERTY_USE_PROF_COMM" runat="server">Use</asp:Label></TD>
									<TD class='midcolora' width='18%'>
										<asp:DropDownList id='cmbPROPERTY_USE_PROF_COMM' OnFocus="SelectComboIndex('cmbPROPERTY_USE_PROF_COMM')"
											runat='server' onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_PROPERTY_USE_PROF_COMM'),document.getElementById('lblDESC_PROPERTY_USE_PROF_COMM'));"></asp:DropDownList>
									</TD>
									<TD class='midcolora' width='18%'>
										<asp:Label id="capDESC_PROPERTY_USE_PROF_COMM" runat="server">Description</asp:Label><span class="mandatory" id="spnDESC_PROPERTY_USE_PROF_COMM">*</span></TD>
									<TD class='midcolora' width='32%'>
										<asp:textbox id="txtDESC_PROPERTY_USE_PROF_COMM" runat='server' size='30' maxlength='255'></asp:textbox>
										<asp:label id="lblDESC_PROPERTY_USE_PROF_COMM" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator ID="rfvDESC_PROPERTY_USE_PROF_COMM" ControlToValidate="txtDESC_PROPERTY_USE_PROF_COMM"
											Runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
									</TD>
								</tr>
								<tr>
									<TD class='midcolora' width='32%'>
										<asp:Label id="capOTHER_INSU_WITH_COMPANY" runat="server">Insu</asp:Label></TD>
									<TD class='midcolora' width='18%'>
										<asp:DropDownList id='cmbOTHER_INSU_WITH_COMPANY' onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_INSU_WITH_COMPANY'),document.getElementById('lblDESC_INSU_WITH_COMPANY'));"
											OnFocus="SelectComboIndex('cmbOTHER_INSU_WITH_COMPANY')" runat='server'></asp:DropDownList>
									</TD>
									<TD class='midcolora' width='32%'>
										<asp:Label id="capDESC_INSU_WITH_COMPANY" runat="server">Insu</asp:Label><span class="mandatory" id="spnDESC_INSU_WITH_COMPANY">*</span></TD>
									<TD class='midcolora' width='18%'>
										<asp:textbox id='txtDESC_INSU_WITH_COMPANY' runat='server' size='30' maxlength='255'></asp:textbox>
										<asp:label id="lblDESC_INSU_WITH_COMPANY" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator ID="rfvDESC_INSU_WITH_COMPANY" Runat="server" ControlToValidate="txtDESC_INSU_WITH_COMPANY"
											Display="Dynamic"></asp:RequiredFieldValidator>
									</TD>
								</tr>
								<tr>
									<TD class='midcolora' width='32%'>
										<asp:Label id="capLOSS_OCCURED_LAST_YEARS" runat="server">Occured</asp:Label></TD>
									<TD class='midcolora' width='18%'>
										<asp:DropDownList id='cmbLOSS_OCCURED_LAST_YEARS' onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_LOSS_OCCURED_LAST_YEARS'),document.getElementById('lblDESC_LOSS_OCCURED_LAST_YEARS'));"
											OnFocus="SelectComboIndex('cmbLOSS_OCCURED_LAST_YEARS')" runat='server'></asp:DropDownList>
									</TD>
									<TD class='midcolora' width='32%'>
										<asp:Label id="capDESC_LOSS_OCCURED_LAST_YEARS" runat="server">Loss</asp:Label><span class="mandatory" id="spnDESC_LOSS_OCCURED_LAST_YEARS">*</span></TD>
									<TD class='midcolora' width='18%'>
										<asp:textbox id='txtDESC_LOSS_OCCURED_LAST_YEARS' runat='server' size='30' maxlength='255'></asp:textbox>
										<asp:label id="lblDESC_LOSS_OCCURED_LAST_YEARS" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
										<asp:RequiredFieldValidator ID="rfvDESC_LOSS_OCCURED_LAST_YEARS" ControlToValidate="txtDESC_LOSS_OCCURED_LAST_YEARS"
											Runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
									</TD>
								</tr>
								<tr>
									<TD class='midcolora' width='32%'>
										<asp:Label id="capDECLINED_CANCELED_COVERAGE" runat="server">Canceled</asp:Label></TD>
									<TD class='midcolora' width='18%'>
										<asp:DropDownList id='cmbDECLINED_CANCELED_COVERAGE' OnFocus="SelectComboIndex('cmbDECLINED_CANCELED_COVERAGE')"
											runat='server'></asp:DropDownList>
									</TD>
									<TD class='midcolora' width='32%'>
										<asp:Label id="capADD_RATING_COV_INFO" runat="server">Rating</asp:Label></TD>
									<TD class='midcolora' width='18%'>
										<asp:textbox id='txtADD_RATING_COV_INFO' runat='server' size='30' maxlength='255'></asp:textbox>
									</TD>
								</tr>
								<tr>
									<td class='midcolora' colspan='2'>
										<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
									</td>
									<td class='midcolorr' colspan="2">
										<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
									</td>
								</tr>
							</TABLE>
						</TD>
					</TR>
					<tr>
						<td class="iframsHeightMedium"></td>
					</tr>
				</TABLE>
				<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
				<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
				<INPUT id="hidOldData" type="hidden" value="" name="hidOldData" runat="server">
				<INPUT id="hidCUSTOMER_ID" type="hidden" runat="server" NAME="hidCUSTOMER_ID"> <INPUT id="hidPOLICY_ID" type="hidden" runat="server" NAME="hidPOLICY_ID">
				<INPUT id="hidPOLICY_VERSION_ID" type="hidden" runat="server" NAME="hidPOLICY_VERSION_ID">
			</FORM>
		</div>
	</BODY>
</HTML>

<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddLiabilityType.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddLiabilityType" validateRequest=false %>
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
			function ResetTheForm()
			{
				DisableValidators();
				document.CLM_LIABILITY_TYPE.reset();
				Init();
				return false;
			}
			
			function EnableDisableDesc(cmbCombo,txtDesc,capDesc)
			{	
				if(!(cmbCombo)) return;								
				if (cmbCombo.selectedIndex > -1)
				{
					//Checking value only if item is selected
					if (cmbCombo.options[cmbCombo.selectedIndex].text == "Other")
					{
						//Disabling the description field, if No is selected
						txtDesc.style.display = "inline";
						capDesc.style.display = "inline";
						
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
						ChangeColor();
											
					}
					else
					{
					
						//Enabling the description field, if yes is selected
						txtDesc.style.display = "none";
						txtDesc.value="";
						capDesc.style.display = "none";
						//lblNA.innerHTML="NA";
						
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
					capDesc.style.display = "none";
					//lblNA.innerHTML="NA";
					
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
			function ValidateLength(objSource , objArgs)
			{
				if(document.getElementById('txtTYPE_OF_PREMISES').value.length>256)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}
			function Init()
			{
				ApplyColor();
				ChangeColor();
				EnableDisableDesc(document.getElementById('cmbPREMISES_INSURED'),document.getElementById('txtOTHER_DESCRIPTION'),document.getElementById('capOTHER_DESCRIPTION'))
				document.getElementById("cmbPREMISES_INSURED").focus();				
			}

			
		</script>
</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="CLM_LIABILITY_TYPE" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
										mandatory</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPREMISES_INSURED" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPREMISES_INSURED" onfocus="SelectComboIndex('cmbPREMISES_INSURED')" onChange="javascript:EnableDisableDesc(this,document.getElementById('txtOTHER_DESCRIPTION'),document.getElementById('capOTHER_DESCRIPTION'))"
											runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvPREMISES_INSURED" runat="server" ControlToValidate="cmbPREMISES_INSURED"
											Display="Dynamic" ErrorMessage="Please select PREMISES_INSURED."></asp:requiredfieldvalidator></TD>									
									<TD class="midcolora" width="18%"><asp:label id="capOTHER_DESCRIPTION" runat="server"></asp:label><span class="mandatory" id="spnOTHER_DESCRIPTION">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtOTHER_DESCRIPTION" runat="server" size="40" maxlength="25"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvOTHER_DESCRIPTION" runat="server" ControlToValidate="txtOTHER_DESCRIPTION"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>								
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capTYPE_OF_PREMISES" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtTYPE_OF_PREMISES"  onkeypress="MaxLength(this,256);"  runat="server" size="40" maxlength="50" TextMode="MultiLine" Rows="4" Columns="45"></asp:textbox><br>
										<asp:customvalidator id="csvTYPE_OF_PREMISES" Runat="server" ControlToValidate="txtTYPE_OF_PREMISES" Display="Dynamic"
											ClientValidationFunction="ValidateLength" ErrorMessage="Error"></asp:customvalidator>
										</TD>
									<TD class="midcolora" colspan='2'></TD>
								</tr>
								<tr>
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
			<INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidLIABILITY_TYPE_ID" type="hidden" value="0" name="hidLIABILITY_TYPE_ID" runat="server">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidLIABILITY_TYPE_ID').value,true);			
		</script>
	</BODY>
</HTML>

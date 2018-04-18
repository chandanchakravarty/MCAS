<%@ Page language="c#" Codebehind="AddRecrVehicle.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddRecrVehicle" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>CLM_RECREATIONAL_VEHICLES</title>
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
				document.CLM_RECREATIONAL_VEHICLES.reset();				
				Init();
				return false;
			}
			function Init()
			{
				ChangeColor();
				ApplyColor();				
			}
			function ValidateRemarksLength(objSource , objArgs)
			{
				if(document.getElementById('txtREMARKS').value.length>100)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}			
			
		</script>
</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="CLM_RECREATIONAL_VEHICLES" method="post" runat="server">
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
								<tr id="trPolicyRecVeh" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capPOLICY_REC_VEH" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPOLICY_REC_VEH" onfocus="SelectComboIndex('cmbPOLICY_REC_VEH')" runat="server" AutoPostBack="True">
										</asp:dropdownlist></TD>
									<td colspan="2" class="midcolora"></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_ID_NUMBER" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCOMPANY_ID_NUMBER" runat="server" size="6" maxlength="4"></asp:textbox><BR>
										<asp:rangevalidator id="rngCOMPANY_ID_NUMBER" Display="Dynamic" ControlToValidate="txtCOMPANY_ID_NUMBER" MinimumValue="1" MaximumValue="10000" Runat="server"
											Type="Integer" ></asp:rangevalidator>
										<asp:requiredfieldvalidator id="rfvCOMPANY_ID_NUMBER" runat="server" ControlToValidate="txtCOMPANY_ID_NUMBER" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capYEAR" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtYEAR" runat="server" size="6" maxlength="4"></asp:textbox><BR>
										<asp:rangevalidator id="rngYEAR" Display="Dynamic" ControlToValidate="txtYEAR" MinimumValue="1900" Runat="server" Type="Double" MaximumValue="2008"></asp:rangevalidator>
										<asp:requiredfieldvalidator id="rfvYEAR" runat="server" ControlToValidate="txtYEAR" Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMAKE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMAKE" runat="server" size="40" maxlength="50"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvMAKE" runat="server" ControlToValidate="txtMAKE" Display="Dynamic" ErrorMessage="Please select txtMAKE."></asp:requiredfieldvalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capMODEL" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMODEL" runat="server" size="40" maxlength="50"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvMODEL" runat="server" ControlToValidate="txtMODEL" Display="Dynamic" ErrorMessage="Please select txtMAKE."></asp:requiredfieldvalidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capSERIAL" runat="server">Serial #</asp:label><span class="mandatory">*</span>
									</TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtSERIAL" runat="server" maxlength="30" size="30"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvSERIAL" runat="server" Display="Dynamic" ControlToValidate="txtSERIAL"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capSTATE_REGISTERED" runat="server">Registered State</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSTATE_REGISTERED" onfocus="SelectComboIndex('cmbSTATE_REGISTERED')" runat="server">											
										</asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvSTATE_REGISTERED" runat="server" Display="Dynamic" ControlToValidate="cmbSTATE_REGISTERED"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capVEHICLE_TYPE" runat="server">Type</asp:label><span class="mandatory">*</span>
									</TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbVEHICLE_TYPE" onfocus="SelectComboIndex('cmbVEHICLE_TYPE')" runat="server" ></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvVEHICLE_TYPE" runat="server" Display="Dynamic" ControlToValidate="cmbVEHICLE_TYPE"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" colspan="2"></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capHORSE_POWER" runat="server">H.P./CC's</asp:label><span class="mandatory">*</span>
									</TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtHORSE_POWER" runat="server" maxlength="10" size="12"></asp:textbox>
										<br>
										<asp:RegularExpressionValidator id="revHORSE_POWER" runat="server" ControlToValidate="txtHORSE_POWER" Display="Dynamic"></asp:RegularExpressionValidator>
										<asp:requiredfieldvalidator id="rfvHORSE_POWER" runat="server" Display="Dynamic" ControlToValidate="txtHORSE_POWER"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capREMARKS" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtREMARKS" runat="server" maxlength="250" size="30" TextMode="MultiLine" Columns="40" Rows="3"></asp:textbox><br>
										<asp:customvalidator id="csvREMARKS" Runat="server" ControlToValidate="txtREMARKS" display="Dynamic" ClientValidationFunction="ValidateRemarksLength"></asp:customvalidator></TD>
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
			<INPUT id="hidLOB_ID" type="hidden" name="hidLOB_ID" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidREC_VEH_ID" type="hidden" value="0" name="hidREC_VEH_ID" runat="server">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidREC_VEH_ID').value,true);			
		</script>
	</BODY>
</HTML>

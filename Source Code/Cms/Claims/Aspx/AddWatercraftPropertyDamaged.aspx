<%@ Page language="c#" Codebehind="AddWatercraftPropertyDamaged.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddWatercraftPropertyDamaged" validateRequest=false  %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
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
				document.CLM_INSURED_BOAT.reset();				
				Init();
				return false;
			}
			function Init()
			{
				ChangeColor();
				ApplyColor();				
				document.getElementById("txtDESCRIPTION").focus();
			}
			function ValidateLength(objSource , objArgs)
			{
				if(document.getElementById('txtDESCRIPTION').value.length>255)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			
			
			}
		</script>
</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="CLM_INSURED_BOAT" method="post" runat="server">
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
									<TD class="midcolora" width="18%"><asp:label id="capDESCRIPTION" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESCRIPTION"   onkeypress="MaxLength(this,255);" runat="server" Rows="3" Columns="45" TextMode="MultiLine"></asp:textbox><BR>
										<asp:customvalidator id="csvDESCRIPTION" Runat="server" ControlToValidate="txtDESCRIPTION" Display="Dynamic"
										ClientValidationFunction="ValidateLength" ErrorMessage="Error"></asp:customvalidator>
										<asp:requiredfieldvalidator id="rfvDESCRIPTION" runat="server" ControlToValidate="txtDESCRIPTION" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capOTHER_VEHICLE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbOTHER_VEHICLE" onfocus="SelectComboIndex('cmbOTHER_VEHICLE')" runat="server">
											<asp:ListItem Value="0" Selected="True">No</asp:ListItem>
											<asp:ListItem Value="1">Yes</asp:ListItem>											
										</asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvOTHER_VEHICLE" runat="server" ControlToValidate="cmbOTHER_VEHICLE" Display="Dynamic"></asp:requiredfieldvalidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capOTHER_INSURANCE_NAME" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtOTHER_INSURANCE_NAME" runat="server" size="40" maxlength="50"></asp:textbox>										
									</TD>																	
									<TD class="midcolora" width="18%"><asp:label id="capOTHER_OWNER_NAME" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtOTHER_OWNER_NAME" runat="server" maxlength="50" size="40"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvOTHER_OWNER_NAME" runat="server" ControlToValidate="txtOTHER_OWNER_NAME" Display="Dynamic"></asp:requiredfieldvalidator>
									</TD>									
								</tr>
								<TR>
									<TD class="midcolora" width="18%"><asp:label id="capADDRESS1" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtADDRESS1" runat="server" maxlength="50" size="40"></asp:textbox>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capADDRESS2" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtADDRESS2" runat="server" maxlength="50" size="40"></asp:textbox>
									</TD>
								</TR>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCITY" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCITY" runat="server" maxlength="10" size="15"></asp:textbox>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capSTATE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSTATE" onfocus="SelectComboIndex('cmbSTATE')" runat="server"></asp:dropdownlist>
									</TD>
									
									
								</tr>
								
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capZIP" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtZIP" runat="server" maxlength="10" size="12"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revGRG_ZIP" runat="server" ControlToValidate="txtZIP" ErrorMessage="RegularExpressionValidator"
										Display="Dynamic"></asp:regularexpressionvalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capHOME_PHONE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtHOME_PHONE" runat="server" maxlength="10" size="15"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revHOME_PHONE" runat="server" ControlToValidate="txtHOME_PHONE"
										ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capWORK_PHONE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtWORK_PHONE" runat="server" size="15" maxlength="10"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revWORK_PHONE" runat="server" ControlToValidate="txtWORK_PHONE"
										ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator>
									</TD>
									<td colspan="2" class="midcolora"></td>
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
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidPROPERTY_DAMAGED_ID" type="hidden" value="0" name="hidPROPERTY_DAMAGED_ID" runat="server">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidPROPERTY_DAMAGED_ID').value,true);			
		</script>
	</BODY>
</HTML>

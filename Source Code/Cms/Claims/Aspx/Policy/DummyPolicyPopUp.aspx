<%@ Page language="c#" Codebehind="DummyPolicyPopUp.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.Policy.DummyPolicyPopUp" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="ClaimTop" Src="/cms/cmsweb/webcontrols/ClaimTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS - Add Dummy Policy</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
		<script>
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
			function ChkEFFECTIVE_DATE(objSource , objArgs)
			{
				document.getElementById('txtEFFECTIVE_DATE').value = FormatDateForGrid(document.getElementById('txtEFFECTIVE_DATE'),'');
				var expdate=document.getElementById('txtEFFECTIVE_DATE').value;
				objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);				
			}	
			
			function ChkEXPIRATION_DATE(objSource , objArgs)
			{
				document.getElementById('txtEXPIRATION_DATE').value = FormatDateForGrid(document.getElementById('txtEXPIRATION_DATE'),'');
				objArgs.IsValid = true;
			}
				
			function ResetTheForm()
			{
				DisableValidators();
				document.CLM_DUMMY_POLICY.reset();
				document.getElementById('txtINSURED_NAME').focus();
				ChangeColor();
				return false;
			}
			function StartUpScript()
			{	
				if(window.opener.document.getElementById("hidDUMMY_POLICY_ID"))
					window.opener.document.getElementById("hidDUMMY_POLICY_ID").value = document.getElementById("hidDUMMY_POLICY_ID").value;
				
				
				//alert(document.getElementById("hidDUMMY_POLICY_ID").value);
				//window.opener.CheckForDummyPolicy();
				window.close();
			}
			function ValidateLength(objSource , objArgs)
			{
				if(document.getElementById('txtNOTES').value.length>500)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;


			}
						
			function CloseWindow()
			{
				window.close();
			}
			
		</script>
	</HEAD>
	<body oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="top.topframe.main1.mousein =false;findMouseIn();showScroll();ApplyColor();ChangeColor();">
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<form id="CLM_DUMMY_POLICY" method="post" runat="server">
				<table class="tableWidth" cellSpacing="1" cellPadding="0" border="0" align="center">
					<tr>
						<TD id="tdClientTop" class="pageHeader" colSpan="4">
							<webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer>
						</TD>
					</tr>
					<tr valign="top">
						<TD id="tdClaimTop" class="pageHeader" colSpan="4">
							<webcontrol:ClaimTop id="cltClaimTop" runat="server" width="100%"></webcontrol:ClaimTop>
						</TD>
					</tr>
					<tr>
						<TD class="headereffectCenter" align="center" colSpan="4">Add Claim Against 
							Unmatched Policy</TD>
					</tr>
					<tr>
						<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
							mandatory
						</TD>
					</tr>
					<tr>
						<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
					</tr>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="capINSURED_NAME" runat="server"></asp:label><span class="mandatory" id="spanINSURED_NAME">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtINSURED_NAME" runat="server" maxlength="100" size="25"></asp:textbox><br>
							<asp:requiredfieldvalidator id="rfvINSURED_NAME" Runat="server" Display="Dynamic" ErrorMessage="Please enter Insured Name."
								ControlToValidate="txtINSURED_NAME"></asp:requiredfieldvalidator></TD>
						<TD class="midcolora" width="18%"><asp:label id="capPOLICY_NUMBER" runat="server">Policy Number</asp:label><span class="mandatory" id="spanPOLICY_NUMBER">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtPOLICY_NUMBER" runat="server" maxlength="10" size="25"></asp:textbox><br>
							<asp:requiredfieldvalidator id="rfvPOLICY_NUMBER" Runat="server" Display="Dynamic" ErrorMessage="Please enter Policy Number."
								ControlToValidate="txtPOLICY_NUMBER"></asp:requiredfieldvalidator></TD>
					</tr>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="capDUMMY_ADD1" runat="server">Address 1</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtDUMMY_ADD1" runat="server" maxlength="70" size="30"></asp:textbox><BR>
							<asp:requiredfieldvalidator id="rfvDUMMY_ADD1" runat="server" Display="Dynamic" ErrorMessage="Please enter Insured Add1."
								ControlToValidate="txtDUMMY_ADD1"></asp:requiredfieldvalidator></TD>
						<TD class="midcolora" width="18%"><asp:label id="capDUMMY_ADD2" runat="server">Address 2</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtDUMMY_ADD2" runat="server" maxlength="70" size="30"></asp:textbox></TD>
					</tr>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="capDUMMY_CITY" runat="server">City</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtDUMMY_CITY" runat="server" maxlength="40" size="30"></asp:textbox><BR>
							<asp:requiredfieldvalidator id="rfvDUMMY_CITY" runat="server" Display="Dynamic" ErrorMessage="Please enter Insured City."
								ControlToValidate="txtDUMMY_CITY"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revDUMMY_CITY" runat="server" Display="Dynamic" ErrorMessage="Please enter a valid City."
								ControlToValidate="txtDUMMY_CITY"></asp:regularexpressionvalidator></TD>
						<TD class="midcolora" width="18%"><asp:label id="capDUMMY_COUNTRY" runat="server">Country</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDUMMY_COUNTRY" onfocus="SelectComboIndex('cmbDUMMY_COUNTRY')" runat="server">
								<asp:ListItem Value='0'></asp:ListItem>
							</asp:dropdownlist><BR>
							<asp:requiredfieldvalidator id="rfvDUMMY_COUNTRY" runat="server" Display="Dynamic" ErrorMessage="Please select Insured Country."
								ControlToValidate="cmbDUMMY_COUNTRY"></asp:requiredfieldvalidator></TD>
					</tr>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="capDUMMY_STATE" runat="server">State</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDUMMY_STATE" onfocus="SelectComboIndex('cmbDUMMY_STATE')" runat="server">
								<asp:ListItem Value='0'></asp:ListItem>
							</asp:dropdownlist><BR>
							<asp:requiredfieldvalidator id="rfvDUMMY_STATE" runat="server" Display="Dynamic" ErrorMessage="Please select Insured State."
								ControlToValidate="cmbDUMMY_STATE"></asp:requiredfieldvalidator></TD>
						<TD class="midcolora" width="18%"><asp:label id="capDUMMY_ZIP" runat="server">Zip</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtDUMMY_ZIP" runat="server" maxlength="10" size="13"></asp:textbox>
							<%--asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
								<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
							</asp:hyperlink--%>
							<BR>
							<asp:requiredfieldvalidator id="rfvDUMMY_ZIP" runat="server" Display="Dynamic" ErrorMessage="Please enter Insured Zip."
								ControlToValidate="txtDUMMY_ZIP"></asp:requiredfieldvalidator>
							<asp:regularexpressionvalidator id="revDUMMY_ZIP" runat="server" Display="Dynamic" ErrorMessage="Please enter zip in (##### or #####-####) format."
								ControlToValidate="txtDUMMY_ZIP"></asp:regularexpressionvalidator></TD>
					</tr>
					<TR>
						<TD class="midcolora" width="18%"><asp:label id="capEFFECTIVE_DATE" runat="server"></asp:label><span class="mandatory" id="spanEFFECTIVE_DATE">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_DATE" runat="server" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkEFFECTIVE_DATE" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgEFFECTIVE_DATE" runat="server" ImageUrl="../../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><br>
							<asp:customvalidator id="csvEFFECTIVE_DATE" ControlToValidate="txtEFFECTIVE_DATE" Display="Dynamic" ClientValidationFunction="ChkEFFECTIVE_DATE"
								Runat="server"></asp:customvalidator>
							<asp:regularexpressionvalidator id="revEFFECTIVE_DATE" runat="server" Display="Dynamic" ErrorMessage="Please enter date in MM/dd/yyyy format."
								ControlToValidate="txtEFFECTIVE_DATE"></asp:regularexpressionvalidator>
							<asp:requiredfieldvalidator id="rfvEFFECTIVE_DATE" Runat="server" Display="Dynamic" ErrorMessage="Please enter EFFECTIVE_DATE."
								ControlToValidate="txtEFFECTIVE_DATE"></asp:requiredfieldvalidator>
						</TD>
						<TD class="midcolora" width="18%"><asp:label id="capEXPIRATION_DATE" runat="server"></asp:label><span class="mandatory" id="spanEXPIRATION_DATE">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtEXPIRATION_DATE" runat="server" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkEXPIRATION_DATE" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgEXPIRATION_DATE" runat="server" ImageUrl="../../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><br>
							<asp:customvalidator id="csvEXPIRATION_DATE" ControlToValidate="txtEXPIRATION_DATE" Display="Dynamic" ClientValidationFunction="ChkEXPIRATION_DATE"
								Runat="server"></asp:customvalidator>
							<asp:regularexpressionvalidator id="revEXPIRATION_DATE" runat="server" Display="Dynamic" ErrorMessage="Please enter date in MM/dd/yyyy format."
								ControlToValidate="txtEXPIRATION_DATE"></asp:regularexpressionvalidator>
							<asp:requiredfieldvalidator id="rfvEXPIRATION_DATE" Runat="server" Display="Dynamic" ErrorMessage="Please enter EXPIRATION_DATE."
								ControlToValidate="txtEXPIRATION_DATE"></asp:requiredfieldvalidator></TD>
					</TR>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="capLOB_ID" runat="server"></asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOB_ID" onfocus="SelectComboIndex('cmbLOB_ID')" runat="server"></asp:dropdownlist><br>
							<asp:requiredfieldvalidator id="rfvLOB_ID" runat="server" Display="Dynamic" ErrorMessage="Please select/enter LOB_ID."
								ControlToValidate="cmbLOB_ID"></asp:requiredfieldvalidator></TD>
						<TD class="midcolora"></TD><TD class="midcolora"></TD>
						<%--<TD class="midcolora" width="18%"><asp:label id="capNOTES" runat="server"></asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtNOTES" runat="server" maxlength="500" size="20" TextMode="MultiLine" Columns="32"
								Rows="5"></asp:textbox><br>
							<asp:customvalidator id="csvNOTES" Runat="server" Display="Dynamic" ErrorMessage="Error" ControlToValidate="txtNOTES"
								ClientValidationFunction="ValidateLength"></asp:customvalidator></TD>--%>
					</tr>
					<tr>
						<td class="midcolora" align="left" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" CausesValidation="False" runat="server" Text="Reset"></cmsb:cmsbutton></td>
						<td class="midcolorr" align="left" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save and Add Claim"></cmsb:cmsbutton></td>
					</tr>
					<tr>
						<td colspan="4" height="350px">
						</td>
					</tr>
					<tr>
						<td colspan="4">
							<webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
						</td>
					</tr>
				</table>
				<input id="hidDUMMY_POLICY_ID" type="hidden" name="hidDUMMY_POLICY_ID" runat="server">
				<input id="IS_ACTIVE" type="hidden" name="IS_ACTIVE" runat="server"> <input type="hidden" id="hidFormSaved" name="hidFormSaved" runat="server" value="0">
				<input type="hidden" id="hidCLAIM_ID" name="hidCLAIM_ID" runat="server"> <input type="hidden" id="hidCLAIM_NUMBER" name="hidCLAIM_NUMBER" runat="server">
			</form>
		</div>
	</body>
</HTML>

<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="EndorsementDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Processes.EndorsementDetails" validateRequest=false %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Endorsement Details</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language='javascript'>
		function AddData()
		{
			//document.getElementById('cmbENDORSEMENT_TYPE').options.selectedIndex = -1;
			document.getElementById('txtENDORSEMENT_DESC').value  = '';
			document.getElementById('txtREMARKS').value  = '';
			ChangeColor();
			DisableValidators();
			document.getElementById('cmbENDORSEMENT_TYPE').focus();
		}
		function populateXML()
		{
			if(document.getElementById('hidFormSaved').value == '0')
			{
				var tempXML =document.getElementById("hidOldData").value ;
				if(tempXML != "")
				{
					populateFormData(tempXML,POL_POLICY_ENDORSEMENTS_DETAILS);
				}
				else
				{
					AddData();
				}
			}
			return false;
		}
		</script>
	</HEAD>
	<BODY leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();'>
		<FORM id='POL_POLICY_ENDORSEMENTS_DETAILS' method='post' runat='server'>
			<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
				<TR>
					<TD>
						<TABLE width='100%' border='0' align='center'>
							<tr>
								<td class="headereffectcenter" colspan="2"><asp:Label ID="capHeader" runat="server" ></asp:Label></td><%--Endorsement Details--%>
							</tr>
							<tr>
								<TD class="pageHeader" colSpan="2"><asp:Label ID="capMessage" runat="server"></asp:Label></TD>Please note that all fields marked with * are mandatory
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<!--<tr>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capENDORSEMENT_DATE" runat="server">Date</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='75%'>
									<asp:Label ID="lblENDORSEMENT_DATE" Runat="server" CssClass="LabelFont"></asp:Label>
								</TD>
							</tr>-->
							<tr>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capENDORSEMENT_TYPE" runat="server">Type</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='75%'>
									<asp:DropDownList id='cmbENDORSEMENT_TYPE' OnFocus="SelectComboIndex('cmbENDORSEMENT_TYPE')" runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvENDORSEMENT_TYPE" runat="server" ControlToValidate="cmbENDORSEMENT_TYPE"
										ErrorMessage="ENDORSEMENT_TYPE can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capENDORSEMENT_DESC" runat="server">Description</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='75%'>
									<asp:textbox id='txtENDORSEMENT_DESC' runat='server' size='30' maxlength='30'></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvENDORSEMENT_DESC" runat="server" ControlToValidate="txtENDORSEMENT_DESC"
										ErrorMessage="ENDORSEMENT_DESC can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='25%'>
									<asp:Label id="capREMARKS" runat="server">Remarks</asp:Label>
								</TD>
								<TD class='midcolora' width='70%'>
									<asp:textbox id='txtREMARKS' TextMode="MultiLine" runat='server' size='150' maxlength='0'></asp:textbox>
								</TD>
							</tr>
							<tr>
								<td class='midcolora'>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
								</td>
								<td class='midcolorr'>
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save & Close'></cmsb:cmsbutton>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidENDORSEMENT_DETAIL_ID" type="hidden" value="NEW" name="hidENDORSEMENT_DETAIL_ID"
				runat="server"> <input id="hidPOLICY_ID" type="hidden" name="hidPOLICY_ID" runat="server">
			<input id="hidPOLICY_VERSION_ID" type="hidden" name="hidPOLICY_VERSION_ID" runat="server">
			<input type="hidden" id="hidCUSTOMER_ID" runat="server" NAME="hidCUSTOMER_ID"> <input type="hidden" id="hidENDORSEMENT_NO" runat="server" NAME="hidENDORSEMENT_NO">
		</FORM>
	</BODY>
</HTML>

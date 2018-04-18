<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" Codebehind="PolicyGeneralLiabilityDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.GeneralLiability.PolicyGeneralLiabilityDetails" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>APP_LOCATIONS</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language='javascript'>
		var varLOB;
		
		
		function AddData()
		{		
			DisableValidators();
			//document.getElementById('cmbLOCATION_ID').options.selectedIndex = -1;
			document.getElementById('txtCLASS_CODE').value	=	'';
			document.getElementById('cmbCOVERAGE_TYPE').focus();
			//document.getElementById('cmbLOCATION_ID').focus();
			document.getElementById('hidPOLICY_GEN_ID').value  = 'NEW';
			document.getElementById('txtBUSINESS_DESCRIPTION').value  = '';
			document.getElementById('txtEXPOSURE').value  = '';
			document.getElementById('txtRATE').value  = '';			
			document.getElementById('cmbCOVERAGE_TYPE').selectedIndex = -1;
			document.getElementById('cmbCOVERAGE_FORM').selectedIndex = -1;
			document.getElementById('cmbEXPOSURE_BASE').selectedIndex = -1;	
			ChangeColor();
		}
		function populateXML()
		{
			
			if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
			{				
				if(document.getElementById('hidOldData').value != "")
				{
					populateFormData(document.getElementById('hidOldData').value, POL_GENERAL_LIABILITY_DETAILS);					
				}
				else
				{	
					AddData();					
				}
			return false;
			}
		}
		function FetchCodes()
		{
			__doPostBack('FetchCodes',document.getElementById('txtCLASS_CODE').value);
		}
		function ResetTheForm()
		{
			var url;		
			document.getElementById('hidClassCheck').value="1";	
			if(document.getElementById('hidPOLICY_GEN_ID').value!="NEW" && document.getElementById('hidPOLICY_GEN_ID').value!="")
				url="PolicyGeneralLiabilityDetails.aspx?CUSTOMER_ID="+ document.getElementById('hidCustomer_ID').value + "&POLICY_ID=" + document.getElementById('hidPOLICY_ID').value + "&POLICY_VERSION_ID=" + document.getElementById('hidPOLICY_VERSION_ID').value + "&POLICY_GEN_ID=" + document.getElementById('hidPOLICY_GEN_ID').value + "&transferdata=";
			else
				url="PolicyGeneralLiabilityDetails.aspx?CUSTOMER_ID="+ document.getElementById('hidCustomer_ID').value + "&POLICY_ID=" + document.getElementById('hidPOLICY_ID').value + "&POLICY_VERSION_ID=" + document.getElementById('hidPOLICY_VERSION_ID').value + "&transferdata=";
			window.location.href=url;
			document.getElementById("hidClassCheck").value="0";	
			return false;			
		}		
		</script>
</HEAD>
	<BODY leftMargin='0' topMargin='0' onload="populateXML();ApplyColor();">
		<FORM id='POL_GENERAL_LIABILITY_DETAILS' method='post' runat='server'>
			<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
				<TR>
					<TD>
						<TABLE width='100%' border='0' align='center'>
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
								<TD class='midcolora' width='18%'>
									<asp:Label id="capLOCATION_ID" runat="server"></asp:Label></TD>
								<TD class='midcolora' colspan='3'>
									<asp:Label id="lblLOCATION_ID" runat="server"></asp:Label>
									<%--<asp:DropDownList id="cmbLOCATION_ID" OnFocus="SelectComboIndex('cmbLOCATION_ID')" runat='server'></asp:DropDownList>--%>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capCLASS_CODE" runat="server"></asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtCLASS_CODE' runat='server' size='6' maxlength='4' ReadOnly="True"></asp:textbox>
									<img src="/../../cms/cmsweb/images/selecticon.gif" id="imgSelectDescForClass" runat="server"
										style="CURSOR: hand">
									<BR>
									<asp:requiredfieldvalidator id="rfvCLASS_CODE" runat="server" ControlToValidate="txtCLASS_CODE" ErrorMessage="Please enter class code."
										Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capBUSINESS_DESCRIPTION" runat="server"></asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtBUSINESS_DESCRIPTION" runat='server' size='20' maxlength='50' ReadOnly="True"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvBUSINESS_DESCRIPTION" runat="server" ControlToValidate="txtBUSINESS_DESCRIPTION"
										ErrorMessage="Please enter business description." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capCOVERAGE_TYPE" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id="cmbCOVERAGE_TYPE" OnFocus="SelectComboIndex('cmbCOVERAGE_TYPE')" runat='server'></asp:DropDownList>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capCOVERAGE_FORM" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id="cmbCOVERAGE_FORM" OnFocus="SelectComboIndex('cmbCOVERAGE_FORM')" runat='server'></asp:DropDownList>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capEXPOSURE_BASE" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id="cmbEXPOSURE_BASE" OnFocus="SelectComboIndex('cmbEXPOSURE_BASE')" runat='server'></asp:DropDownList>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capEXPOSURE" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtEXPOSURE" runat='server' size='8' maxlength='6'></asp:textbox><br>
									<asp:rangevalidator id="rngEXPOSURE" ControlToValidate="txtEXPOSURE" Display="Dynamic" Runat="server"
										Type="Integer" MinimumValue="1" MaximumValue="999999"></asp:rangevalidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capRATE" runat="server"></asp:Label></TD>
								<TD class='midcolora' ColSpan='3'>
									<asp:textbox id='txtRATE' runat='server' size='8' maxlength='6'></asp:textbox><BR>
									<asp:rangevalidator id="rngRATE" ControlToValidate="txtRATE" Display="Dynamic" Runat="server" Type="Integer"
										MinimumValue="1" MaximumValue="999999"></asp:rangevalidator>
								</TD>
								
							</tr>
							<tr>
								<td class='midcolora' colspan='1'>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
								</td>
								<td class='midcolorr' colspan="3">
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<input id="hidCUSTOMER_ID" type="hidden" runat="server" NAME="hidCUSTOMER_ID"> <input id="hidPOLICY_ID" type="hidden" runat="server" NAME="hidPOLICY_ID">
			<input id="hidPOLICY_VERSION_ID" type="hidden" runat="server" NAME="hidPOLICY_VERSION_ID">
			<input id="hidPOLICY_GEN_ID" type="hidden" runat="server" NAME="hidPOLICY_GEN_ID"> <INPUT id="hidClassCheck" type="hidden" name="hidClassCheck" runat="server" value="0">
		</FORM>
		<script>
			//RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidPOLICY_GEN_ID').value,true);
		</script>
	</BODY>
</HTML>

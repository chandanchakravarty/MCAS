<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" validateRequest="false" Codebehind="AdditionalWordings.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AdditionalWordings" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>ADDITIONAL_WORDINGS</title>
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
		
		function populateXML()
		{
			if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
			{
				if(document.getElementById('hidOldData').value!=null && document.getElementById('hidOldData').value!= "")
				{
					document.getElementById("rfvPDF_WORDINGS").setAttribute('isValid',false);
					document.getElementById("rfvPDF_WORDINGS").style.display='none';
					//document.getElementById("rfvPDF_WORDINGS").setAttribute('enabled',false);
				}
			}
		}
		
		function ResetScreen()
		{
			DisableValidators();
			document.ADDITIONAL_WORDINGS.reset();
			ChangeColor();	
			return false;
		}
		
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();ChangeColor();">
		<FORM id="ADDITIONAL_WORDINGS" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<TR>
						<TD>
							<TABLE width="100%" align="center" border="0">
								<TBODY>
									<TR>
										<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
									</TR>
									<TR>
										<TD class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></TD>
									</TR>
									<TR>
										<TD class="midcolora" width="10%"><asp:label id="capSTATE" runat="server"></asp:label><span id="spnSTATE" runat="server" class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%" colSpan="3"><asp:dropdownlist id="cmbSTATE" onfocus="SelectComboIndex('cmbSTATE')" runat="server"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvSTATE" runat="server" ErrorMessage="" ControlToValidate="cmbSTATE"
												Display="Dynamic"></asp:requiredfieldvalidator></TD>
									</TR>
									<TR>
										<TD class="midcolora" width="10%"><asp:label id="capLOB" Runat="server"></asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%" colSpan="3"><asp:dropdownlist id="cmbLOB" onfocus="SelectComboIndex('cmbLOB')" runat="server"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvLOB" runat="server" ErrorMessage="" ControlToValidate="cmbLOB"
												Display="Dynamic"></asp:requiredfieldvalidator></TD>
									</TR>
									<TR>
										<TD class="midcolora" style="HEIGHT: 16px" width="10%"><asp:label id="capPOLICY_PROCESS" Runat="server"></asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" style="HEIGHT: 16px" width="32%" colSpan="3"><asp:dropdownlist id="cmbPOLICY_PROCESS" onfocus="SelectComboIndex('cmbPOLICY_PROCESS')" runat="server"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvPOLICY_PROCESS" runat="server" ErrorMessage=""
												ControlToValidate="cmbPOLICY_PROCESS" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									</TR>
									<TR>
										<TD class="midcolora" width="10%"><asp:label id="capPDF_WORDINGS" runat="server"></asp:label><span id="spnPDF_WORDINGS" class="mandatory" runat="server">*</span></TD>
										<TD class="midcolora" width="32%" colspan="3"><asp:TextBox id="txtPDF_WORDINGS" TextMode="MultiLine" name="txtPDF_WORDINGS" runat="server" Columns="100" Rows="5"></asp:TextBox><br>
											<asp:requiredfieldvalidator id="rfvPDF_WORDINGS" runat="server" ControlToValidate="txtPDF_WORDINGS" Display="Dynamic" ErrorMessage="Please enter PDF Wordings"></asp:requiredfieldvalidator></TD>
										<%--<TD class="midcolora" width="10%"><asp:label id="lblFILE_PATH" runat="server">File Path</asp:label></TD>
										<TD class="midcolorc" align="left" width="20%"><asp:label id="lblFILE_PATH_NAME" runat="server" CssClass="errmsg">File Path</asp:label></TD>--%>
									</TR>
									<TR id="trBody" runat="server">
										<TD class="midcolora" align="center" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></TD>
										<TD class="midcolorr" align="center" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></TD>
									</TR>
									<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
									<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
									<INPUT id="hidPDF_WORDINGS" type="hidden" name="hidPDF_WORDINGS" runat="server">
									<INPUT id="hidWORDINGS_ID" type="hidden" name="hidWORDINGS_ID" runat="server">
									<INPUT id="hidROW_ID" type="hidden" name="hidROW_ID" runat="server">
									<TR>
		</FORM>
		</TR></TBODY></TABLE></TD></TR></TBODY></TABLE>
		<script>
			RefreshWebGrid("1",document.getElementById('hidWORDINGS_ID').value);
		</script>
	</BODY>
</HTML>






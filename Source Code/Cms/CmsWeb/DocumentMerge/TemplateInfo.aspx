<%@ Page language="c#" Codebehind="TemplateInfo.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.DocumentMerge.TemplateInfo" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TemplateInfo</title>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
				function copy()
				{
					var strLocation = top.frames["botframe"].document.frames["tabLayer"].document.location.toString();
					if (strLocation != 'about:blank') 
					{
						if (strLocation.indexOf('TemplateInfo') > -1)
							top.frames[1].document.frames["tabLayer"].document.location='TemplateInfo.aspx?TEMPLATE_ID=' + document.getElementById('hidTemplateId').value + '&MODE=COPY';
					}
					return false;
				}
				
				function HideLOB_AGNCY()
				{
				<%if (DeleteFlag) {%>
						var tempType = document.getElementById('ddlTemplateType').value;
						if(tempType == 14126) // Account
						{
							document.getElementById('trLOB_AGNCY').style.display = 'none';
						}
						else
						{
							document.getElementById('trLOB_AGNCY').style.display = 'inline';
						}
				<%}%>	
				}

			</script>
	</HEAD>
	<body oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="ApplyColor();ChangeColor();HideLOB_AGNCY();">
		<form id="TemplateInfo" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4"><asp:label runat="server" Text="Please note that all fields marked with * are mandatory" ID="capMessage"></asp:label></TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4" height="9"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							<%if (DeleteFlag) {%>
							<tr>
								<TD class="midcolora" width="17%"><asp:label id="lblTemplateName" runat="server">Template Name</asp:label><SPAN class="mandatory">*</SPAN></TD>
								<TD class="midcolora" width="33%"><asp:textbox id="txtTemplateName" runat="server" Width="80%" MaxLength="200"></asp:textbox><BR /><asp:requiredfieldvalidator id="rfvRequiredTemplateName" runat="server" Display="Dynamic" ControlToValidate="txtTemplateName"
										ErrorMessage=""></asp:requiredfieldvalidator></TD><%--Please enter  Template Name.--%>
								<TD class="midcolora" width="17%"><asp:label id="lblVersion" runat="server">Version</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="33%"><asp:textbox id="txtVersion" runat="server" Width="30%" MaxLength="2"></asp:textbox><br /><asp:requiredfieldvalidator id="rfvRequiredVersion" runat="server" Display="Dynamic" ControlToValidate="txtVersion"
										ErrorMessage=""></asp:requiredfieldvalidator><asp:rangevalidator id="rfvRangeVersion" runat="server" Display="Dynamic" ControlToValidate="txtVersion"
										ErrorMessage="" MinimumValue="1" MaximumValue="99" Type="Integer"></asp:rangevalidator></TD><%--Please enter Version.--%><%--<Br>Version should be numric.--%>
							</tr>
							<tr>
								<TD class="midcolora" width="17%" height="19"><asp:label id="lblTemplateDesc" runat="server">Template Description</asp:label></TD>
								<TD class="midcolora" colSpan="3" height="19"><asp:textbox onkeypress="MaxLength(this,450);" id="txtTemplateDesc" runat="server" Width="80%" Columns="90" Rows="6" TextMode="MultiLine" MaxLength="450"></asp:textbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="17%" height="19"><asp:label id="lblTemplateType" runat="server">Type</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="33%" height="19"><asp:dropdownlist id="ddlTemplateType" runat="server" OnChange="HideLOB_AGNCY();"></asp:dropdownlist><br/><asp:requiredfieldvalidator id="rfvRequireTemplateType" runat="server" Display="Dynamic" ControlToValidate="ddlTemplateType"
										ErrorMessage=""></asp:requiredfieldvalidator></TD><%--<Br>Please select Type.--%>
								<TD class="midcolora" width="17%" height="19"><asp:label id="lblCreatedBy" runat="server">Created By</asp:label></TD>
								<TD class="midcolora" width="33%" height="19"><asp:dropdownlist id="ddlCreatedBy" runat="server"></asp:dropdownlist></TD>
							</tr>
							<tr id="trLOB_AGNCY">
								<TD class="midcolora" width="17%" height="19"><asp:label id="lblLob" runat="server">Product</asp:label></TD>
								<TD class="midcolora" width="33%" height="19"><asp:dropdownlist id="ddlLob" runat="server"></asp:dropdownlist></TD>
								<TD class="midcolora" width="17%" height="19"><asp:label id="lblAgency" runat="server">Agency</asp:label></TD>
								<TD class="midcolora" width="33%" height="19"><asp:dropdownlist id="ddlAgency" runat="server"></asp:dropdownlist></TD>
							</tr>
							<tr>
								<td class="midcolora" align="left" colSpan="4">&nbsp;</td>
							</tr>
							<tr>
								<td class="midcolora" align="left" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnCopy" runat="server" Text="Copy"></cmsb:cmsbutton></td>
								<td class="midcolorr" align="right" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnEdit" runat="server" Text="Edit Template"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
							<%}%>
						</TABLE>
						<input id="hidTemplateId" type="hidden" runat="server">
					</TD>
				</TR>
			</TABLE>
			<%if (UpdateGrid) {%>
			<script language="javascript">
			 	RefreshWebGrid('1',document.getElementById('hidTemplateId').value ,false);
			</script>
			<%}%>
		</form>
	</body>
</HTML>

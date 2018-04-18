<%@ Page language="c#" Codebehind="AddAgencyDetail.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddAgencyDetail" validateRequest=false  %>
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
		
			function Init()
			{
				ApplyColor();
				ChangeColor();		
				document.getElementById("txtAGENCY_CODE").focus();
				
			}
			function ResetTheForm()
			{
				DisableValidators();
				document.CLM_AGENCY.reset();
				Init();
				return false;				
			}
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="CLM_AGENCY" method="post" runat="server">
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
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_CODE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtAGENCY_CODE" runat="server" size="15" maxlength="10"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvAGENCY_CODE" runat="server" ControlToValidate="txtAGENCY_CODE" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_SUB_CODE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtAGENCY_SUB_CODE" runat="server" size="15" maxlength="10"></asp:textbox>
									</TD>									
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_CUSTOMER_ID" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" colspan="3"><asp:textbox id="txtAGENCY_CUSTOMER_ID" runat="server" size="32" maxlength="30"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvAGENCY_CUSTOMER_ID" runat="server" ControlToValidate="txtAGENCY_CUSTOMER_ID"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>									
								</tr>
								<tr>
									<TD class='midcolora' width='18%'>
										<asp:Label id="capAGENCY_PHONE" runat="server"></asp:Label></TD>
									<TD class='midcolora' width='32%'>
										<asp:textbox id='txtAGENCY_PHONE' runat='server' size='15' maxlength='10'></asp:textbox><br>
										<asp:regularexpressionvalidator id="revAGENCY_PHONE" runat="server" ControlToValidate="txtAGENCY_PHONE" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator>
									</TD>
									<TD class='midcolora' width='18%'>
										<asp:Label id="capAGENCY_FAX" runat="server"></asp:Label></TD>
									<TD class='midcolora' width='32%'>
										<asp:textbox id="txtAGENCY_FAX" runat='server' size='15' maxlength='10'></asp:textbox><br>
										<asp:regularexpressionvalidator id="revAGENCY_FAX" Runat="server" Display="Dynamic" ControlToValidate="txtAGENCY_FAX"></asp:regularexpressionvalidator>
									</TD>
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
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidAGENCY_ID" type="hidden" value="0" name="hidAGENCY_ID" runat="server">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidAGENCY_ID').value,true);			
		</script>
	</BODY>
</HTML>

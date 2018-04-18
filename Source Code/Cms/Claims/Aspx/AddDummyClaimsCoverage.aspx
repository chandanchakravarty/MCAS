<%@ Page language="c#" Codebehind="AddDummyClaimsCoverage.aspx.cs" AutoEventWireup="false" Inherits="Claims.Aspx.AddDummyClaimsCoverage" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Add Claims Coverage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript">
		function Init()
		{
			var oldVal = document.getElementById('hidOldData').value;
			document.getElementById('hidOldData').value = '0';
			if(typeof parent.document.getElementById('hidCOV_ID_CLAIM').value != null)
				document.getElementById('hidCOV_ID_CLAIM').value = parent.document.getElementById('hidCOV_ID_CLAIM').value;
			if(oldVal == '')
				__doPostBack("txtCOV_DES","");
			FormatAmount();//Added for Itrack Issue 5639 on 29 April 2009
		}
		
		function saveSet()
		{
			document.getElementById('hidFormSaved').value = '2';
		}
		//Added for Itrack Issue 5639 on 29 April 2009
		function FormatAmount()
			{
			  document.getElementById('txtLIMIT_1').value = formatAmount(document.getElementById('txtLIMIT_1').value);
			  document.getElementById('txtDEDUCTIBLE_1').value = formatAmount(document.getElementById('txtDEDUCTIBLE_1').value);//Added for Itrack Issue 5639 on 21 May 2009
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="ChangeColor();ApplyColor();Init();"  leftMargin="0" topMargin="0" rightMargin="0">
		<form id="MNT_CLAIM_COVERAGE" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
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
										<TD class="midcolora" width="18%"><asp:label id="capCOV_DES" runat="server">Coverage Description</asp:label><span class="mandatory" id="spnName" runat="server">*</span></TD>
										<TD class="midcolora" width="32%" colspan="3"><asp:textbox id="txtCOV_DES" runat="server" size="80" maxlength="100"></asp:textbox>
											<br>
											<asp:requiredfieldvalidator id="rfvCOV_DES" runat="server" Display="Dynamic" ControlToValidate="txtCOV_DES"
												ErrorMessage="Please enter Coverage Description."></asp:requiredfieldvalidator>
										</TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capLIMIT_1" runat="server">Limit</asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtLIMIT_1" runat="server" size="20" maxlength="10" style="text-align: right"></asp:textbox>
											<br>
											<asp:regularexpressionvalidator id="revLIMIT_1" runat="server" Display="Dynamic" ControlToValidate="txtLIMIT_1"></asp:regularexpressionvalidator>
										</TD>
										<TD class="midcolora" width="18%"><asp:label id="capDEDUCTIBLE_1" runat="server">Deductible</asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtDEDUCTIBLE_1" runat="server" size="20" maxlength="10" style="text-align: right"></asp:textbox>
											<br>
											<asp:regularexpressionvalidator id="revDEDUCTIBLE_1" runat="server" Display="Dynamic" ControlToValidate="txtDEDUCTIBLE_1"></asp:regularexpressionvalidator>
										</TD>
									</tr>
									<tr>
										<td class="midcolorr" colSpan="4"><cmsb:cmsbutton class="clsButton" id="btnActivate" runat="server" Text="Activate/Deactivate"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" Visible="False"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
									</tr>
								</TBODY>
							</TABLE>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidCOV_ID" type="hidden" value="0" name="hidCOV_ID" runat="server">
			<INPUT id="hidCOV_ID_CLAIM" type="hidden" value="0" name="hidCOV_ID_CLAIM" runat="server">
			<INPUT id="hidSTATE_ID" type="hidden" name="hidSTATE_ID" runat="server" value="0">
			<INPUT id="hidLOB_ID" type="hidden" name="hidLOB_ID" runat="server">
			<INPUT id="hidCLAIM_ID" type="hidden" name="hidCLAIM_ID" runat="server"> 
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
		<script>
			if(document.getElementById('hidFormSaved').value == '1' || document.getElementById('hidFormSaved').value == '3')
			{
				RefreshWebGrid('1',document.getElementById('hidCOV_ID').value,true);			
			}
			//if(document.getElementById('hidFormSaved').value == '3')	
			//	document.getElementById('trBody').style.display = 'none';
			//if(document.getElementById('hidCOV_ID').value == '0')	
			//	document.getElementById('btnDelete').setAttribute('disabled',true);	
		</script>
	</body>
</HTML>

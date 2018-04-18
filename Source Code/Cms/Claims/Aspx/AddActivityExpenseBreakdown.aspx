<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddActivityExpenseBreakdown.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddActivityExpenseBreakdown" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Add Expense Breakdown Activity</title>
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
			function ValidateLength(objSource , objArgs)
			{
				if(document.getElementById('txtSERVICE_DESCRIPTION').value.length>300)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}
			
			function formReset()
			{
				document.CLM_EXPENSE_BREAKDOWN.reset();
				DisableValidators();
				ChangeColor();
				return false;
			}
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="ChangeColor();ApplyColor();">
		<FORM id="CLM_EXPENSE_BREAKDOWN" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
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
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capTRANSACTION_CODE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbTRANSACTION_CODE" onfocus="SelectComboIndex('cmbTRANSACTION_CODE')" runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvTRANSACTION_CODE" runat="server" ControlToValidate="cmbTRANSACTION_CODE"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capCOVERAGE_ID" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCOVERAGE_ID" onfocus="SelectComboIndex('cmbCOVERAGE_ID')" runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvCOVERAGE_ID" runat="server" ControlToValidate="cmbCOVERAGE_ID" Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPAID_AMOUNT" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPAID_AMOUNT" runat="server" size="15" MaxLength="8"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvPAID_AMOUNT" runat="server" ControlToValidate="txtPAID_AMOUNT" Display="Dynamic"></asp:requiredfieldvalidator>
										<asp:rangevalidator id="rngPAID_AMOUNT" ControlToValidate="txtPAID_AMOUNT" Display="Dynamic" Runat="server"
											MaximumValue="9999999999" MinimumValue="1" Type="Currency"></asp:rangevalidator></TD>
									<TD class="midcolora" width="50%" colspan="2">&nbsp;</TD>
								</tr>
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" causesvalidation="false" Text="Reset"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" align="right"></cmsb:cmsbutton></td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			<INPUT id="hidEXPENSE_ID" type="hidden" value="0" name="hidEXPENSE_ID" runat="server">
			<INPUT id="hidEXPENSE_BREAKDOWN_ID" type="hidden" value="0" name="hidEXPENSE_BREAKDOWN_ID" runat="server">
			<INPUT id="hidACTIVITY_ID" type="hidden" value="0" name="hidACTIVITY_ID" runat="server">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidEXPENSE_BREAKDOWN_ID').value,true);			
		</script>
	</BODY>
</HTML>

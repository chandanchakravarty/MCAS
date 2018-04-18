<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddRecoveryPayer.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddRecoveryPayer" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Add Payee Details</title>
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
			function ValidateLength(objSource , objArgs)
			{
				if(document.getElementById('txtDESCRIPTION').value.length>300)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}
			
			function formReset()
			{
				document.CLM_ACTIVITY_RECOVERY_PAYER.reset();
				ShowHideAddressDetails();
				DisableValidators();
				ChangeColor();
				return false;
			}
			function ChkRECEIVED_DATE(objSource , objArgs)
			{
				var expdate=document.CLM_ACTIVITY_RECOVERY_PAYER.txtRECEIVED_DATE.value;
				objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);				
			}	
			function Init()
			{
				if(<%=AnyPayeeAdded%> > 0 )
					return;
				ChangeColor();
				ApplyColor();
			}
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="CLM_ACTIVITY_RECOVERY_PAYER" method="post" runat="server">
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
									<%--<TD class="midcolora" width="18%"><asp:label id="capRECOVERY_TYPE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbRECOVERY_TYPE" onfocus="SelectComboIndex('cmbRECOVERY_TYPE')" runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvRECOVERY_TYPE" runat="server" ControlToValidate="cmbRECOVERY_TYPE" Display="Dynamic"></asp:requiredfieldvalidator></TD>--%>
									<TD class="midcolora" width="18%"><asp:label id="capRECEIVED_DATE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtRECEIVED_DATE" runat="server" maxlength="10" size="12" ></asp:textbox><asp:hyperlink id="hlkRECEIVED_DATE" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgRECEIVED_DATE" runat="server" ImageUrl="../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
										</asp:hyperlink><br>
										<asp:requiredfieldvalidator id="rfvRECEIVED_DATE" runat="server" ControlToValidate="txtRECEIVED_DATE" Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revRECEIVED_DATE" runat="server" ControlToValidate="txtRECEIVED_DATE" Display="Dynamic"
											ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator><asp:customvalidator id="csvRECEIVED_DATE" ControlToValidate="txtRECEIVED_DATE" Display="Dynamic" Runat="server"
											ClientValidationFunction="ChkRECEIVED_DATE"></asp:customvalidator></TD>
									<td class="midcolora" colspan="2"></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capRECEIVED_FROM" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtRECEIVED_FROM" runat="server" maxlength="50" size="35"></asp:textbox><br>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capCHECK_NUMBER" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCHECK_NUMBER" runat="server" MaxLength="10" size="15"></asp:textbox><br>
									<asp:RegularExpressionValidator id="rfvCHEQUE" Runat="server" Display="Dynamic" ControlToValidate="txtCHECK_NUMBER"></asp:RegularExpressionValidator></TD>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDESCRIPTION" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox onkeypress="MaxLength(this,300);" id="txtDESCRIPTION" runat="server" TextMode="MultiLine"
											maxlength="300" size="40" Columns="50" Rows="5"></asp:textbox><br>										
										<asp:customvalidator id="csvDESCRIPTION" ControlToValidate="txtDESCRIPTION" Display="Dynamic" ClientValidationFunction="ValidateLength"
											Runat="server"></asp:customvalidator></TD>
									<td colspan="2" class="midcolora"></td>
								</tr>
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" causesvalidation="false" Text="Reset"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			<INPUT id="hidPAYER_ID" type="hidden" value="0" name="hidPAYER_ID" runat="server">
			<INPUT id="hidDefaultValues" type="hidden" value="0" name="hidDefaultValues" runat="server">
			<INPUT id="hidACTIVITY_ID" type="hidden" value="0" name="hidACTIVITY_ID" runat="server">
			<INPUT id="hidPARTY_ID" type="hidden" value="0" name="hidPARTY_ID" runat="server">
			<INPUT id="hidCALLED_FROM" type="hidden" name="hidCALLED_FROM" runat="server">
		</FORM>
		<script>			
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidPAYER_ID').value,true);			
		</script>
	</BODY>
</HTML>

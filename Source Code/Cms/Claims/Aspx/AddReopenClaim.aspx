<%@ Page language="c#" Codebehind="AddReopenClaim.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddReopenClaim" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Re-Open Claims</title>
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
				if(document.getElementById('txtREASON').value.length>500)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}
			
			function Init()
			{				
					
				ApplyColor();
				ChangeColor();
			}	
			
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Init();">		
		<FORM id="CLM_REOPEN_CLAIM" method="post" runat="server">
			<div id="bodyHeight" class="pageContent">
				<TABLE cellSpacing="0" class="tableWidthHeader" cellPadding="0"  border="0">										
					<TR id="trBody" runat="server">
						<TD>
							<TABLE width="100%" align="center" border="0">
								<TBODY>									
									<tr>
										<TD class="pageHeader" colSpan="4"><asp:Label runat="server" ID="lblHeader"></asp:Label></TD><%--Please note that all fields marked with * are 
											mandatory--%>
									</tr>
									<tr>
										<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capREOPEN_DATE" runat="server"></asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtREOPEN_DATE" class="midcolora" BorderStyle="None" runat="server" ReadOnly="True" size="12" maxlength="10"></asp:textbox>
											<%--<asp:hyperlink id="hlkREOPEN_DATE" runat="server" CssClass="HotSpot">
												<ASP:IMAGE id="imgREOPEN_DATE" runat="server" ImageUrl="../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
											</asp:hyperlink><br>
											<asp:requiredfieldvalidator id="rfvREOPEN_DATE" runat="server" Display="Dynamic" ControlToValidate="txtREOPEN_DATE"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revREOPEN_DATE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
												ControlToValidate="txtREOPEN_DATE"></asp:regularexpressionvalidator>--%>
										</TD>
										<TD class="midcolora" width="18%"><asp:label id="capREOPEN_BY" runat="server"></asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtREOPEN_BY" class="midcolora" BorderStyle="None" runat="server" ReadOnly="True"
												size="40" maxlength="50"></asp:textbox></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capREASON" runat="server"></asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" colspan="3"><asp:textbox id="txtREASON" runat="server" Rows="5" TextMode="MultiLine" Columns="150" maxlength="500"
												onkeypress="MaxLength(this,500);"></asp:textbox><br>
											<asp:requiredfieldvalidator id="rfvREASON" runat="server" Display="Dynamic" ControlToValidate="txtREASON"></asp:requiredfieldvalidator>
											<asp:customvalidator id="csvREASON" Runat="server" ControlToValidate="txtREASON" Display="Dynamic" ClientValidationFunction="ValidateLength"
												ErrorMessage="Error"></asp:customvalidator>
										</TD>
									</tr>
									<tr>
										<td class="midcolora" colSpan="2">&nbsp;</td>
										<td class="midcolorr" colSpan="2">
											<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
										</td>
									</tr>
								</TBODY>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</div>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidREOPEN_ID" type="hidden" value="0" name="hidREOPEN_ID" runat="server">
			<INPUT id="hidREOPEN_BY" type="hidden" value="0" name="hidREOPEN_BY" runat="server">
			<INPUT id="hidREOPEN_COUNT" type="hidden" value="0" name="hidREOPEN_COUNT" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCLAIM_ID" type="hidden" name="hidCLAIM_ID" runat="server">
		</FORM>
	<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidREOPEN_ID').value,true);			
		</script>
	</BODY>		
</HTML>

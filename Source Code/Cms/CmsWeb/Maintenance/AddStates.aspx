<%@ Page language="c#" Codebehind="AddStates.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AddStates" validateRequest=false  %>
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

			function AddData()
			{
				ChangeColor();
				DisableValidators();
				
			}
			
			function populateXML()
			{
				if(document.getElementById('hidFormSaved').value == '0')
				{
					var tempXML;
					if(document.getElementById("hidOldData").value!="")
					{
						tempXML=document.getElementById("hidOldData").value;						 						
						populateFormData(tempXML,MNT_COUNTRY_STATE_LIST);							
					}	
					else
					{
						//SetTimeOut has been added as the page gives javascript error at control focus
						setTimeout("AddData();",500);
					}
					
				}
				return false;
			}
			
			function ResetTheForm()
			{
				DisableValidators();
				document.forms[0].reset();
				Init();
				return false;
			}
			function Init()
			{
				populateXML();				
				ApplyColor();
				document.getElementById("cmbCOUNTRY_ID").focus();
			}
		</script>
</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="MNT_COUNTRY_STATE_LIST" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr>
									<td class="midcolora" width="18%"><asp:Label ID="capCOUNTRY_ID" Runat="server"></asp:Label><span class="mandatory">*</span></td>
									<td class="midcolora" colspan="3">
										<asp:DropDownList ID="cmbCOUNTRY_ID" Runat="server" onfocus="SelectComboIndex('cmbCOUNTRY_ID')"></asp:DropDownList><br>
										<asp:RequiredFieldValidator ID="rfvCOUNTRY_ID" Runat="server" Display="Dynamic" ControlToValidate="cmbCOUNTRY_ID"></asp:RequiredFieldValidator>
									</td>
								</tr>
								<tr>
									<td class="midcolora" width="18%"><asp:Label ID="capSTATE_CODE" Runat="server"></asp:Label><span id="spnSTATE_CODE" runat="server" class="mandatory">*</span></td>
									<td class="midcolora" width="32%">
										<asp:TextBox ID="txtSTATE_CODE" Runat="server" MaxLength="2" size="10"></asp:TextBox><br>
										<asp:RequiredFieldValidator ID="rfvSTATE_CODE" Runat="server" Display="Dynamic" ControlToValidate="txtSTATE_CODE"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator ID="revSTATE_CODE" Runat="server" Display="Dynamic" ControlToValidate="txtSTATE_CODE"></asp:RegularExpressionValidator>										
									</td>
									<td class="midcolora" width="18%"><asp:Label ID="capSTATE_NAME" Runat="server"></asp:Label><span ID="spnSTATE_NAME" Runat="server" class="mandatory">*</span></td>
									<td class="midcolora" width="32%">
										<asp:TextBox ID="txtSTATE_NAME" Runat="server"></asp:TextBox><br>
										<asp:RequiredFieldValidator ID="rfvSTATE_NAME" MaxLength="20" size="34" Runat="server" Display="Dynamic" ControlToValidate="txtSTATE_NAME"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator ID="revSTATE_NAME" Runat="server" Display="Dynamic" ControlToValidate="txtSTATE_NAME"></asp:RegularExpressionValidator>										
									</td>
								</tr>
								<tr>
									<td class="midcolora" width="18%"><asp:Label ID="capSTATE_DESC" Runat="server"></asp:Label></td>
									<td class="midcolora" colspan="3">
										<asp:TextBox ID="txtSTATE_DESC" Runat="server"  ></asp:TextBox>										
									</td>
								</tr>
								<tr>
									<td class="midcolora" colSpan="2">
										<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" Visible="False" runat="server" Text="Activate/Deactivate"
											CausesValidation="false"></cmsb:cmsbutton>
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
			<INPUT id="hidCOUNTRY_ID" type="hidden" value="0" name="hidCOUNTRY_ID" runat="server">
			<INPUT id="hidSTATE_ID" type="hidden" value="0" name="hidSTATE_ID" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidSTATE_ID').value,true);			
		</script>
	</BODY>
</HTML>



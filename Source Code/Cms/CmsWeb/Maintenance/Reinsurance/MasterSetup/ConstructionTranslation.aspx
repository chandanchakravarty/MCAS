<%@ Page language="c#" Codebehind="ConstructionTranslation.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup.ConstructionTranslation" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>ConstructionTranslation</title>
		<meta content="False" name="vs_showGrid">
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
			//return;
			document.getElementById('hidREIN_CONSTRUCTION_CODE_ID').value	=	'New';
			document.getElementById('cmbREIN_EXTERIOR_CONSTRUCTION').options.selectedIndex = 0;
			document.getElementById('cmbREIN_DESCRIPTION').options.selectedIndex = 0;
			document.getElementById('txtREIN_REPORT_CODE').value  = '';
			document.getElementById('txtREIN_NISS').value  = '';
			//document.getElementById('cmbREIN_EXTERIOR_CONSTRUCTION').focus();
				
			if(document.getElementById('btnActivateDeactivate'))
				 document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);	
			if(document.getElementById('btnDelete'))
				document.getElementById('btnDelete').style.display = "none";
			
			}


			function populateXML()
			{
			//ResetAfterActivateDeactivate();
				var tempXML;
				tempXML=document.getElementById("hidOldData").value;
				//alert(document.getElementById("hidOldData").value);
				if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
				{				
					if(tempXML!="" && tempXML!="0")
					{
		
						populateFormData(tempXML,"ConstructionTranslation");						
					}
					else
					{
						AddData();
					}
				}
				
				return false;
			}
			
			
			
			
			
																		  
			function Reset()
			{
				DisableValidators();
				document.ConstructionTranslation.reset();
				ChangeColor();
				document.getElementById('cmbREIN_EXTERIOR_CONSTRUCTION').focus();
				return false;
			}						  
			
					
																							
																				  
	
	

		
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();">
		<FORM id="ConstructionTranslation" method="post" runat="server">
			<TABLE  cellSpacing="0" cellPadding="0" width="100%" border="0">
			<TBODY>
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE id="tblBody" width="100%"  align="center" border="0" runat="server">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									
									<TD class="midcolora" width="18%"><asp:label id="capREIN_EXTERIOR_CONSTRUCTION" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREIN_EXTERIOR_CONSTRUCTION" onfocus="SelectComboIndex('cmbREIN_EXTERIOR_CONSTRUCTION')"
											 runat="server"></asp:dropdownlist><br>
										<asp:RequiredFieldValidator id="rfvREIN_EXTERIOR_CONSTRUCTION" Runat="server" ControlToValidate="cmbREIN_EXTERIOR_CONSTRUCTION"
											ErrorMessage="" Display="Dynamic"></asp:RequiredFieldValidator><%--Please select Exterior Construction.--%>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capREIN_DESCRIPTION" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREIN_DESCRIPTION" onfocus="SelectComboIndex('cmbREIN_DESCRIPTION')" 
											onchange="setReinsuranceReportCode();" runat="server"></asp:dropdownlist><br>
										<asp:RequiredFieldValidator id="rfvREIN_DESCRIPTION" Runat="server" ControlToValidate="cmbREIN_DESCRIPTION"
											ErrorMessage="" Display="Dynamic"></asp:RequiredFieldValidator><%--Please select Reinsurance Description.--%>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capREIN_REPORT_CODE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_REPORT_CODE" size="5" maxlength="10"  runat="server"></asp:textbox><br>
										<asp:RequiredFieldValidator id="rfvREIN_REPORT_CODE" Runat="server" ControlToValidate="txtREIN_REPORT_CODE"
											ErrorMessage="" Display="Dynamic"></asp:RequiredFieldValidator><%--Please enter Reinsurance Report Code.--%>
									</TD>
									
									<TD class="midcolora" width="18%"><asp:label id="capREIN_NISS" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_NISS" runat="server" size="3" maxlength="1"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revREIN_NISS" ControlToValidate="txtREIN_NISS" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>
										<asp:RequiredFieldValidator id="rfvREIN_NISS" Runat="server" ControlToValidate="txtREIN_NISS" ErrorMessage=""
											Display="Dynamic"></asp:RequiredFieldValidator><%--Please enter NISS Code.--%>
									</TD>
								</tr>
								<!--END Harmanjeet-->
								<tr>
									<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>&nbsp;
										<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate" visible="True"
											CausesValidation="False"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" visible="True" CausesValidation="false"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
				
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidREIN_CONSTRUCTION_CODE_ID" type="hidden" value="0" name="hidREIN_CONSTRUCTION_CODE_ID"
				runat="server"> <INPUT id="hidReset" type="hidden" value="0" name="hidReset" runat="server">
				</TBODY>
			</TABLE>
		</FORM>
		<script>
		
		RefreshWebGrid(1,document.getElementById('hidREIN_CONSTRUCTION_CODE_ID').value,true);
			function setReinsuranceReportCode()
			{
				document.getElementById("txtREIN_REPORT_CODE").value = document.getElementById("cmbREIN_DESCRIPTION").value;
				EnableValidator('rfvREIN_REPORT_CODE',false);
			}
			
					
		</script>
	</BODY>
</HTML>

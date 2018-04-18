<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Page language="c#" Codebehind="TIVGroup.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup.TIVGroup" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>TIVGroup</title>
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
		<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>
		<script language="javascript" type="text/javascript">
		
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";

			function AddData()
			{
			ChangeColor();
			DisableValidators();
			//return;
			document.getElementById('hidREIN_TIV_GROUP_ID').value	=	'New';
			document.getElementById('cmbREIN_TIV_CONTRACT_NUMBER').value  = '';
			document.getElementById('cmbREIN_TIV_CONTRACT_NUMBER').focus();
			//document.getElementById('txtREIN_TIV_EFFECTIVE_DATE').focus();
			document.getElementById('txtREIN_TIV_FROM').value  = '';
			document.getElementById('txtREIN_TIV_TO').value  = '';
			document.getElementById('txtREIN_TIV_GROUP_CODE').value  = '';
			document.getElementById('txtREIN_TIV_EFFECTIVE_DATE').value  = '';
			
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
					    var REIN_TIV_EFFECTIVE_DATE = $("#txtREIN_TIV_EFFECTIVE_DATE").val();
					    populateFormData(tempXML, "TIVGroup");
					    $("#txtREIN_TIV_EFFECTIVE_DATE").val(REIN_TIV_EFFECTIVE_DATE);				
					}
					else
					{
						AddData();
					}
				}
				//SetTab();
				if(document.getElementById('cmbREIN_TIV_CONTRACT_NUMBER').style.display == "inline")
					document.getElementById('cmbREIN_TIV_CONTRACT_NUMBER').focus();
							//document.getElementById('txtREIN_TIV_EFFECTIVE_DATE').focus();

				return false;
			}
			
			function SetTab()
			{
				if(document.getElementById('hidOldData').value != "")
				{				
					Url="Reinsurance/ReinsuranceContactIndex.aspx?calledfrom=reinsurer&EntityType=Reinsurer&EntityId="+document.getElementById('hidREIN_CONTACT_ID').value + "&";
					DrawTab(2,top.frames[1],'Contact',Url);
					
					Url="AttachmentIndex.aspx?calledfrom=reinsurer&EntityType=Reinsurer&EntityId="+document.getElementById('hidREIN_CONTACT_ID').value + "&";
					DrawTab(3,top.frames[1],'Attachment',Url);
					
					Url="Reinsurance/ReinsuranceBankingDetails.aspx?";
					DrawTab(4,top.frames[1],'Reinsurance Banking Details',Url);			
				}
				else
				{							
					RemoveTab(4,top.frames[1]);
					RemoveTab(3,top.frames[1]);
					RemoveTab(2,top.frames[1]);					
				}	
			}
			
			function ResetAfterActivateDeactivate()
			{
				if (document.getElementById('hidReset').value == "1")
				{				
					document.TIVGroup.reset();			
				}
			}
			function Reset()
			{
				DisableValidators();
				//document.Split.reset();
				ChangeColor();
				document.getElementById('hidFormSaved').value = '0';
				//document.getElementById('hidStateChange').value='0';				
				populateXML();
				return false;
			}					
																		  
			//function Reset()
			//{
			//	DisableValidators();
			//	document.TIVGroup.reset();
			//	ChangeColor();
			//	return false;
			//}						  
			
					
																							
																				  
	function ChkDate(objSource , objArgs)
	{
		var effdate=document.TIVGroup.txtREIN_TIV_EFFECTIVE_DATE.value;
		objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",effdate,jsaAppDtFormat);
	}
	

		
		</script>
</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();">
		<FORM id="TIVGroup" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
			<TBODY>
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE id="tblBody" width="100%" align="center" border="0" runat="server">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									
									<TD class="midcolora" width="18%"><asp:label id="capREIN_TIV_CONTRACT_NUMBER" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREIN_TIV_CONTRACT_NUMBER" onfocus="SelectComboIndex('cmbREIN_TIV_CONTRACT_NUMBER')"
											runat="server"></asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvContract_ID" ControlToValidate="cmbREIN_TIV_CONTRACT_NUMBER" ErrorMessage="" Display="Dynamic"
												Runat="server"></asp:requiredfieldvalidator>		<%--Please Select Contract#--%>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capREIN_TIV_EFFECTIVE_DATE" runat="server"></asp:label><span id="spnREIN_TIV_EFFECTIVE_DATE" runat="server" class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_TIV_EFFECTIVE_DATE" runat="server" size="12" MaxLength="10"></asp:textbox><asp:hyperlink id="hlkEFFECTIVE_DATE" runat="server" CssClass="HotSpot">
											<asp:image id="imgEFFECTIVE_DATE" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif"></asp:image>
										</asp:hyperlink><br>
										<asp:requiredfieldvalidator id="rfvREIN_TIV_EFFECTIVE_DATE" runat="server" ControlToValidate="txtREIN_TIV_EFFECTIVE_DATE" ErrorMessage=""
												Display="Dynamic"></asp:requiredfieldvalidator><%--Effective Date can't be blank.--%>
										<asp:regularexpressionvalidator id="revREIN_TIV_EFFECTIVE_DATE" runat="server" ControlToValidate="txtREIN_TIV_EFFECTIVE_DATE"
											Display="Dynamic"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capREIN_TIV_FROM" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox style="TEXT-ALIGN:right" cssclass="INPUTCURRENCY" id="txtREIN_TIV_FROM" runat="server" size="15" MaxLength="9"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvREIN_TIV_FROM" ControlToValidate="txtREIN_TIV_FROM" ErrorMessage="" Display="Dynamic"
												Runat="server"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revREIN_TIV_FROM" ControlToValidate="txtREIN_TIV_FROM" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>										
									</TD>
									
									<TD class="midcolora" width="18%"><asp:label id="capREIN_TIV_TO" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox style="TEXT-ALIGN:right" cssclass="INPUTCURRENCY" id="txtREIN_TIV_TO" runat="server" size="15" MaxLength="9"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvREIN_TIV_TO" ControlToValidate="txtREIN_TIV_TO" ErrorMessage="" Display="Dynamic"
												Runat="server"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revREIN_TIV_TO" ControlToValidate="txtREIN_TIV_TO" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capREIN_TIV_GROUP_CODE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox style="TEXT-ALIGN:right" id="txtREIN_TIV_GROUP_CODE" runat="server" size="4" MaxLength="2"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvREIN_TIV_GROUP_CODE" ControlToValidate="txtREIN_TIV_GROUP_CODE" ErrorMessage="" Display="Dynamic"
												Runat="server"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revREIN_TIV_GROUP_CODE" ControlToValidate="txtREIN_TIV_GROUP_CODE" Display="Dynamic"
											Runat="server"></asp:regularexpressionvalidator></TD>
									<td class="midcolora" colSpan="2"></td>
								</tr>
								
								<tr>
									<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>&nbsp;
										<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate" visible="True" CausesValidation="False"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" visible="True" CausesValidation="false"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidREIN_TIV_GROUP_ID" type="hidden" value="0" name="hidREIN_TIV_GROUP_ID" runat="server">
			<INPUT id="hidReset" type="hidden" value="0" name="hidReset" runat="server">
			</TBODY>
			</TABLE>
		</FORM>
		<script>
			RefreshWebGrid(1,document.getElementById('hidREIN_TIV_GROUP_ID').value,true);
		</script>
	</BODY>
</HTML>

<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddReinsuranceMajorPart.aspx.cs" AutoEventWireup="false" validateRequest="false" Inherits="Cms.CmsWeb.Maintenance.AddReinsuranceMajorPart" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>AddReinsuranceMajorPart</title>
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
			document.getElementById('hidPARTICIPATION_ID').value	=	'New';
			//document.getElementById('cmbREINSURANCE_COMPANY').focus();
			//document.getElementById('cmbREINSURANCE_COMPANY').options.selectedIndex = 0;
			document.getElementById('txtLAYER').value ='';
			document.getElementById('cmbNET_RETENTION').options.selectedIndex = 1;
			document.getElementById('txtWHOLE_PERCENT').value  = '';
			document.getElementById('cmbMINOR_PARTICIPANTS').options.selectedIndex = 1;
			//document.getElementById('cmbREIN_LINE_OF_BUSINESS').options.selectedIndex = 0;
			document.getElementById('btnActivate').setAttribute("disabled",true);
			//Apply Null check By Raghav
			if(document.getElementById("btnDelete")!=null)
			document.getElementById('btnDelete').setAttribute("disabled",true);	
			document.getElementById("cmbREINSURANCE_COMPANY").focus(); 	
			
					
			
			
			}
		function SetTab()
		{ 
			/*
			if((document.getElementById('hidFormSaved').value == '1') || (document.getElementById("hidOldData").value != "" && document.getElementById("hidOldData").value != "0"))
			{	
					
					var Contract_Type = document.getElementById('cmbMINOR_PARTICIPANTS').value;
					var Pass_Major_Participants = document.getElementById('cmbREINSURANCE_COMPANY').options(document.getElementById('cmbREINSURANCE_COMPANY').selectedIndex).text;
					var Pass_Major_Layer = document.getElementById('txtLAYER').value;
					
					if(Contract_Type == "10963")
					{
						
						Url="ReinsuranceMinorParticipationIndex.aspx?Major_Participants=" +Pass_Major_Participants + "&Layer=" + Pass_Major_Layer +"&";
						//DrawTab(7,top.frames[1],'Minor Participation',Url);
					}
					else
					{		
						RemoveTab(7,top.frames[1]);
					}	
			
			}
			else
			{		
				RemoveTab(7,top.frames[1]);
			}*/	
			if (document.getElementById("cmbREINSURANCE_COMPANY").style=="inline")
				document.getElementById("cmbREINSURANCE_COMPANY").focus(); 
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
						//alert(tempXML);
						populateFormData(tempXML,"AddReinsuranceMajorPart");						
					}
					else
					{
						AddData();
					}
				}
				var ctrl = document.getElementById("trBody");
				if ( ctrl!=null && ctrl.style.display=='inline')
					document.getElementById("cmbREINSURANCE_COMPANY").focus(); 
				
				return false;
			}
								  
			function Reset()
			{
				DisableValidators();
				document.AddReinsuranceMajorPart.reset();
				ChangeColor();
				return false;
			}						  
		function  funcValidatePercent(objSource , objArgs)
		 {
			var newValue=objArgs.Value;
			var OldTotalPercent= document.getElementById('hidOLDTOTALPERCENT').value;
			var oldWholePer=document.getElementById('hidWHOLE_PERCENT').value; 
			var TotalPercent=parseInt(newValue)+parseInt(OldTotalPercent)-parseInt(oldWholePer);  
			if (TotalPercent>100)
			{
			objArgs.IsValid = false;
			return false;
			}
			else
			{
			return true;
			}
			return;
		}
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();SetTab();">
		<FORM id="AddReinsuranceMajorPart" method="post" runat="server">
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
									<!--START April 09 2007 Harmanjeet-->
									<TD class="midcolora" width="18%"><asp:label id="capREINSURANCE_COMPANY" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREINSURANCE_COMPANY" onfocus="SelectComboIndex('cmbREINSURANCE_COMPANY')"
											tabIndex="1" runat="server"></asp:dropdownlist><BR>
											<asp:requiredfieldvalidator id="rfvREINSURANCE_COMPANY" runat="server" ControlToValidate="cmbREINSURANCE_COMPANY"
											ErrorMessage="Please enter Reinsurance Company"></asp:requiredfieldvalidator>	
											</TD>
									<TD class="midcolora" width="18%"><asp:label id="capLAYER" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtLAYER" runat="server" size="12" MaxLength="2" tabIndex="2" CssClass="INPUTCURRENCY"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revLAYER" runat="server" ControlToValidate="txtLAYER" Display="Dynamic"></asp:regularexpressionvalidator><br>
										<asp:requiredfieldvalidator id="rfvLAYER" runat="server" ControlToValidate="txtLAYER"
											ErrorMessage="Please enter Layer"></asp:requiredfieldvalidator>	
										</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capNET_RETENTION" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%">
										<asp:dropdownlist id="cmbNET_RETENTION" onfocus="SelectComboIndex('cmbNET_RETENTION')" tabIndex="3"
											runat="server"></asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvNET_RETENTION" runat="server" ControlToValidate="cmbNET_RETENTION"
											ErrorMessage="Please select Carrier Retention Applicable"></asp:requiredfieldvalidator>		
									</TD>
									<!--<td class="midcolora" colSpan="2"></td>-->
									<TD class="midcolora" width="18%"><asp:label id="capWHOLE_PERCENT" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtWHOLE_PERCENT" tabIndex="4" runat="server" size="12" CssClass="INPUTCURRENCY" maxlength="3"></asp:textbox>
										<asp:regularexpressionvalidator id="revWHOLE_PERCENT" ControlToValidate="txtWHOLE_PERCENT" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>
										<asp:RangeValidator id="rngWHOLE_PERCENT" runat="server" ErrorMessage="Please enter between 0-100"
											MinimumValue="0" MaximumValue="100" ControlToValidate="txtWHOLE_PERCENT" Type="Integer"></asp:RangeValidator><br>
									<asp:customvalidator id="csvWHOLE_PERCENT" Display="Dynamic" ControlToValidate="txtWHOLE_PERCENT" Runat="server"
												ClientValidationFunction="funcValidatePercent" ErrorMessage ="Total Whole % must not be greater than 100."></asp:customvalidator><br>
											<asp:requiredfieldvalidator id="rfvWHOLE_PERCENT" runat="server" ControlToValidate="txtWHOLE_PERCENT"
											ErrorMessage="Please enter % of Whole Layer"></asp:requiredfieldvalidator>			
						
										</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMINOR_PARTICIPANTS" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%">
										<asp:dropdownlist id="cmbMINOR_PARTICIPANTS" onfocus="SelectComboIndex('cmbMINOR_PARTICIPANTS')" tabIndex="5"
											runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvMINOR_PARTICIPANTS" runat="server" ControlToValidate="cmbMINOR_PARTICIPANTS"
											ErrorMessage="Please select Minor Participants"></asp:requiredfieldvalidator>				
									</TD>
									<td class="midcolora" colSpan="2"></td>
								</tr>
								<!--END Harmanjeet-->
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" tabIndex="6"></cmsb:cmsbutton>&nbsp;
										<cmsb:cmsbutton class="clsButton" id="btnActivate" runat="server" Text="Deactivate" visible="True" tabIndex="7"
											CausesValidation="False"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" visible="True" CausesValidation="false" tabIndex="8" ></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" tabIndex="20"></cmsb:cmsbutton></td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidPARTICIPATION_ID" type="hidden" value="0" name="hidPARTICIPATION_ID" runat="server">
			<INPUT id="hidReset" type="hidden" value="0" name="hidReset" runat="server"> 
			<INPUT id="hidCONTRACT_ID" type="hidden" value="0" name="hidCONTRACT_ID" runat="server">
			<INPUT id="hidOLDTOTALPERCENT" type="hidden" value="0" name="hidOLDTOTALPERCENT" runat="server">
			<INPUT id="hidWHOLE_PERCENT" type="hidden" value="0" name="hidWHOLE_PERCENT" runat="server">
			
		</FORM>
		<script>
		RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidPARTICIPATION_ID').value,true);
					//alert(document.getElementById("btnActivateDeactivate").value);	
		</script>
	</BODY>
</HTML>

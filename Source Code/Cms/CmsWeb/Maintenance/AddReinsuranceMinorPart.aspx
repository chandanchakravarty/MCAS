<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddReinsuranceMinorPart.aspx.cs" AutoEventWireup="false" validateRequest="false" Inherits="Cms.CmsWeb.Maintenance.AddReinsuranceMinorPart" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>AddReinsuranceMinorPart</title>
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
			var layerTotal = 0;
			function AddData()
			{
			ChangeColor();
			DisableValidators();
			//return;
			document.getElementById('hidMINOR_PARTICIPATION_ID').value	=	'New';
			//document.getElementById('txtMAJOR_PARTICIPANTS').value =document.getElementById('hidMajor_Participants').value;
			document.getElementById('txtMINOR_LAYER').value =document.getElementById('hidLayer').value;
			document.getElementById('txtMINOR_WHOLE_PERCENT').value  = '';
			//document.getElementById('cmbMINOR_PARTICIPANTS').options.selectedIndex = 1;
			//document.getElementById('cmbREIN_LINE_OF_BUSINESS').options.selectedIndex = 0;
			//Apply Null check By Raghav
			if(document.getElementById("btnDelete")!=null)
			document.getElementById('btnActivate').setAttribute("disabled",true);			
			//Apply Null check By Raghav
			if(document.getElementById("btnDelete")!=null)
			document.getElementById('btnDelete').setAttribute("disabled",true);
			document.getElementById('cmbMAJOR_PARTICIPANTS').focus();
			
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
		
						populateFormData(tempXML,"AddReinsuranceMinorPart");
					}
					else
					{
						AddData();
					}
				}
				//SetTab();
				if (document.getElementById('cmbMAJOR_PARTICIPANTS') !=null && document.getElementById('cmbMAJOR_PARTICIPANTS').style.display=="inline")
						document.getElementById('cmbMAJOR_PARTICIPANTS').focus();
				return false;
			}
								  
			function Reset()
			{
				DisableValidators();
				document.AddReinsuranceMinorPart.reset();
				document.getElementById('cmbMAJOR_PARTICIPANTS').focus();
				ChangeColor();
				return false;
			}						  
			
					
		  function setLayer()																					
		  {
		  var majorCombo=document.getElementById('cmbMAJOR_PARTICIPANTS');
		  if (majorCombo.options.selectedIndex>0)
		  {
		  var MajorValue=majorCombo.options[majorCombo.options.selectedIndex].value;
		  var MajorValues=MajorValue.split('~');
		  document.getElementById('txtMINOR_LAYER').value=MajorValues[1];
		  var major = MajorValues[1];
		  layerTotal = MajorValues[2];
		  }
		  return false;
		  }
		function  funcValidateMinorPercent(objSource , objArgs)
		 {
			var newValue=objArgs.Value;
			var OldTotalPercent= document.getElementById('hidOLDTOTALPERCENT').value;
			var oldWholePer=document.getElementById('hidMINOR_WHOLE_PERCENT').value; 
			var TotalPercent=parseInt(newValue)+parseInt(layerTotal)-parseInt(oldWholePer);   
			//var TotalPercent=parseInt(newValue)+parseInt(OldTotalPercent)-parseInt(oldWholePer);  
			//alert(TotalPercent);
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
		function validate_range(objSource , objArgs)
		{
			var per_layer = document.getElementById('txtMINOR_WHOLE_PERCENT').value;
			if(per_layer>100)
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
	<BODY leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();">
		<FORM id="AddReinsuranceMinorPart" method="post" runat="server">
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
									<!--START April 09 2007 Harmanjeet-->
									<TD class="midcolora" width="18%"><asp:label id="capMAJOR_PARTICIPANTS" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%">
									<asp:dropdownlist id="cmbMAJOR_PARTICIPANTS" tabIndex="1" onfocus="SelectComboIndex('cmbMAJOR_PARTICIPANTS')" 
											runat="server" onclick="setLayer();"></asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvMAJOR_PARTICIPANTS" runat="server" ControlToValidate="cmbMAJOR_PARTICIPANTS"
											ErrorMessage="Please select Major Participant"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capMINOR_LAYER" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMINOR_LAYER" runat="server" size="12" MaxLength="2" tabIndex="2" CssClass="INPUTCURRENCY" ></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvMINOR_LAYER" runat="server" ControlToValidate="txtMINOR_LAYER" ErrorMessage="Please enter Layer"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMINOR_PARTICIPANTS" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbMINOR_PARTICIPANTS" tabIndex="3" onfocus="SelectComboIndex('cmbMINOR_PARTICIPANTS')" 
											runat="server"></asp:dropdownlist><br><asp:requiredfieldvalidator id="rfvMINOR_PARTICIPANTS" runat="server" ControlToValidate="cmbMINOR_PARTICIPANTS"
											ErrorMessage="Please select Minor participants"></asp:requiredfieldvalidator></TD>
									<!--<td class="midcolora" colSpan="2"></td>-->
									<TD class="midcolora" width="18%"><asp:label id="capMINOR_WHOLE_PERCENT" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox style="TEXT-ALIGN:right"  id="txtMINOR_WHOLE_PERCENT" tabIndex="4" runat="server" size="12" maxlength="5" CssClass="INPUTCURRENCY"></asp:textbox><br><asp:requiredfieldvalidator id="rfvMINOR_WHOLE_PERCENT" runat="server" ControlToValidate="txtMINOR_WHOLE_PERCENT"
											ErrorMessage="Please enter % of Whole layer"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revMINOR_WHOLE_PERCENT" Runat="server" Display="Dynamic" ControlToValidate="txtMINOR_WHOLE_PERCENT"></asp:regularexpressionvalidator><br>
									<asp:customvalidator id="csvMINOR_WHOLE_PERCENT_AMOUNT" runat="server" ErrorMessage="Please enter between 0-100"
											ClientValidationFunction="validate_range" ControlToValidate="txtMINOR_WHOLE_PERCENT" Type="Integer"></asp:customvalidator><br>
									<asp:customvalidator id="csvMINOR_WHOLE_PERCENT" Display="Dynamic" ControlToValidate="txtMINOR_WHOLE_PERCENT" Runat="server"
												ClientValidationFunction="funcValidateMinorPercent" ErrorMessage ="Total Whole % must not be greater than 100."></asp:customvalidator>		
									</TD>
								</tr>
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" tabIndex="5"></cmsb:cmsbutton>&nbsp;
										<cmsb:cmsbutton class="clsButton" id="btnActivate" runat="server" Text="Deactivate" CausesValidation="False" tabIndex="6"
											visible="True"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" CausesValidation="false" tabIndex="7"
											visible="True"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" tabIndex="8"></cmsb:cmsbutton></td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidMINOR_PARTICIPATION_ID" type="hidden" value="0" name="hidMINOR_PARTICIPATION_ID"
				runat="server"> <INPUT id="hidReset" type="hidden" value="0" name="hidReset" runat="server">
			<INPUT id="hidCONTRACT_ID" type="hidden" value="0" name="hidCONTRACT_ID" runat="server">
			<INPUT id="hidLayer" type="hidden" value="0" name="hidLayer" runat="server"> 
			<INPUT id="hidMajor_Participants" type="hidden" value="0" name="hidMajor_Participants"
				runat="server">
			<INPUT id="hidOLDTOTALPERCENT" type="hidden" value="0" name="hidOLDTOTALPERCENT" runat="server"> 
			<INPUT id="hidMINOR_WHOLE_PERCENT" type="hidden" value="0" name="hidMINOR_WHOLE_PERCENT" runat="server"> 
		</FORM>
		<script>
		RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidMINOR_PARTICIPATION_ID').value,true);
			
					//alert(document.getElementById("btnActivateDeactivate").value);	
		</script>
	</BODY>
</HTML>

<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Page language="c#" Codebehind="MatchClaims.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.MatchClaims" validateRequest=false  %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>CLM_MATCH_POLICY</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="vbscript">
			Function getUserConfirmationForClaims
				Dim ClaimNumber,PolicyNumber 
				ClaimNumber = document.getElementById("hidDUMMY_CLAIM_NUMBER").value					
				PolicyNumber = document.getElementById("hidPOLICY_NUMBER").value	
				getUserConfirmationForClaims= msgbox("Claim Number " & ClaimNumber & " will be reassigned to the system generated policy " & PolicyNumber & "." & vbCrLf & " The process cannot be undone. " & vbCrLf & " Do you want to continue?",35,"Reassign Now?")
			End function
			Function getUserConfirmationForLOB				
				getUserConfirmationForLOB= msgbox("Line of business on the policy does not match. Do you want to proceed?",35,"Continue?")
			End function
		</script>
		<script language="javascript">
		
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		function ConfirmationMessage()
		{
			if(getUserConfirmationForClaims()==6) //Use selected Yes
				return true;
			else
				return false;
		}
		function LOBConfirmationMessage()
		{
			/*if(getUserConfirmationForLOB()==6) //Use selected Yes
			{
				document.getElementById("hidCONTINUE_WITH_LOB").value = "1";
				__doPostBack('CONTINUE','');
				return true;
			}
			else*/
			alert("Line of business on the policy does not match with the dummy policy.");
			return false;
		}
		function AlertMessageForLossDate()
		{
			alert(document.getElementById("hidMessage").value);
			return false;
		}
		function GoBack()
		{
			document.location.href = "ClaimsTab.aspx?&DUMMY_POLICY_ID=" + document.getElementById("hidDUMMY_POLICY_ID").value + "&DUMMY_CLAIM_NUMBER=" + document.getElementById("hidDUMMY_CLAIM_NUMBER").value + "&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value;
			return false;	
		}
		function Init()
		{
			ApplyColor();
			ChangeColor();
			setfirstTime();
			setfirstTime();
			top.topframe.main1.mousein = false;
			findMouseIn();
			SetMenu();
		}
		function RedirectToSearchPolicy()
		{
			document.location.href = "Policy/SearchPolicy.aspx?&BackToMatchPolicy=1&DUMMY_POLICY_ID=" + document.getElementById("hidDUMMY_POLICY_ID").value + "&DUMMY_CLAIM_NUMBER=" + document.getElementById("hidDUMMY_CLAIM_NUMBER").value + "&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value;
			return false;
		}
		function SetMenu()
		{
			//Enable customer and policy menus at this page if they are available			
			//Custom hidMATCHED hidden variable is used to find whether the policy has been matched with the claim or not
			//Upon successful matching of policy, 1 is set in the variable
			if(document.getElementById("hidMATCHED").value!="" && document.getElementById("hidMATCHED").value!="0")
			{
				top.topframe.enableMenu('2,2,4');
				top.topframe.enableMenu('2,2,5');
				top.topframe.enableMenu('2,2,6');
			}
			else
			{
				//Disable customer and policy menus at this page
				top.topframe.disableMenu('2,2,4');
				top.topframe.disableMenu('2,2,5');
				top.topframe.disableMenu('2,2,6');
			}
		}
			
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Init();">
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<FORM id="CLM_MATCH_POLICY" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="80%" border="0" align="center">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								 <tr>
									<TD class=headereffectCenter align=center colSpan=4 >Attach Claims to System Policy</TD>
								</tr>  
								<tr>
									<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
										mandatory</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capREASSIGN_CLAIM_TO" runat="server"></asp:label><span class="mandatory" id="spnREASSIGN_CLAIM_TO" runat="server">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtREASSIGN_CLAIM_TO" runat="server" size="40" maxlength="20" ReadOnly="True"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvREASSIGN_CLAIM_TO" runat="server" ControlToValidate="txtREASSIGN_CLAIM_TO" Display="Dynamic"></asp:requiredfieldvalidator>
									</TD>
									<TD class="midcolora" colspan="2"></TD>
								</tr>
								<tr>
									<TD class="midcolora" colspan="2">
											<cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Text="Back" CausesValidation=false></cmsb:cmsbutton>&nbsp;&nbsp;
											<cmsb:cmsbutton class="clsButton" id="btnSearchPolicy" runat="server" Text="Search Policy" CausesValidation=false></cmsb:cmsbutton>
									</TD>								
									<td class="midcolorr" colspan="2"><cmsb:cmsbutton class="clsButton" id="btnReassignNow" runat="server" Text="Reassign Now" CausesValidation=true></cmsb:cmsbutton></td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server" language="javascript">
			<INPUT id="hidDUMMY_POLICY_ID" type="hidden" value="0" name="hidDUMMY_POLICY_ID" runat="server">			
			<input type="hidden" name="hidCUSTOMER_ID" id="hidCUSTOMER_ID" runat="server">
			<input type="hidden" name="hidPOLICY_ID" id="hidPOLICY_ID" runat="server">
			<input type="hidden" name="hidPOLICY_VERSION_ID" id="hidPOLICY_VERSION_ID" runat="server">
			<input type="hidden" name="hidPOLICY_NUMBER" id="hidPOLICY_NUMBER" runat="server">
			<input type="hidden" name="hidBackFromPolicy" id="hidBackFromPolicy" runat="server">			
			<input type="hidden" name="hidDUMMY_CLAIM_NUMBER" id="hidDUMMY_CLAIM_NUMBER" runat="server">
			<input type="hidden" name="hidCLAIM_ID" id="hidCLAIM_ID" runat="server">
			<input type="hidden" name="hidLOB_ID" id="hidLOB_ID" runat="server">
			<input type="hidden" name="hidMATCHED" id="hidMATCHED" runat="server">
			<input type="hidden" name="hidMessage" id="hidMessage" runat="server">						
			<input type="hidden" name="hidCONTINUE_WITH_LOB" id="hidCONTINUE_WITH_LOB" value="0" runat="server">
			
			
		</FORM>
	</BODY>
</HTML>

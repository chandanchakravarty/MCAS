<%@ Page language="c#" Codebehind="CorrectiveUserProcess.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Processes.CorrectiveUserProcess" validaterequest=false %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="PolicyTop" Src="/cms/cmsweb/webcontrols/PolicyTop.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Corrective User Process</title>
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
		
		function setMenu()
		{
			
			//IF menu on top frame is not ready then
			//menuXmlReady will false
			//If menu is not ready, we will again call setmenu again after 1 sec
			if (top.topframe.main1.menuXmlReady == false)
				setTimeout('setMenu();',1000);
			
			
			//Enabling or disabling menus
			top.topframe.main1.activeMenuBar = '1';
			top.topframe.createActiveMenu();
			top.topframe.enableMenus('1','ALL');
			top.topframe.enableMenu('1,1,1');			
			top.topframe.enableMenu('1,2,3');
			//top.topframe.disableMenus("1,3","ALL");
			//selectedLOB = lobMenuArr[document.getElementById('hidLOBID').value];
			//top.topframe.enableMenu("1,3," + selectedLOB);			
			
		}
		
		function Showhide()
		{
			document.getElementById('btnCommitInProgress').style.display="inline";
			document.getElementById('btnCommitInProgress').disabled = true;
			document.getElementById('btnCommit_To_Spool').style.display="none";
			document.getElementById('btnRollBack').disabled = true;	
			DisableButton(document.getElementById('btnCommit_To_Spool'));
			top.topframe.disableMenus("1,7","ALL");//Added for Itrack Issue 6203 on 31 July 2009
		}
		function HideShowCommit()
			{
				document.getElementById('btnCommit_To_Spool').disabled = true;
				DisableButton(document.getElementById('btnRollBack'));
				top.topframe.disableMenus('1,7','ALL');//Added for Itrack Issue 6203 on 31 July 2009
			}
		function init()
		{
			document.getElementById('btnCommitInProgress').style.display="none";
			DisplayBody();
			//Added for Itrack Issue 6203 on 31 July 2009
			if (top.topframe.main1.menuXmlReady == false)
				setTimeout("top.topframe.enableMenus('1,7','ALL');",1000);
			else	
				top.topframe.enableMenus('1,7','ALL');
		}
		
		function DisplayBody()
		{
			if (document.getElementById('hidDisplayBody').value == "True")
			{
				document.getElementById('trBody').style.display='none';		
			}
			else
			{
				document.getElementById('trBody').style.display='inline';
			}
			
		}

		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="ApplyColor();ChangeColor();top.topframe.main1.mousein = false;findMouseIn();init();">
		<FORM id="PROCESS_CORRUSER" method="post" runat="server">			
		 <DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>
			<!-- To add bottom menu start -->
			<!-- To add bottom menu end -->
			<TABLE cellSpacing="0" cellPadding="0" width="90%" align="center" border="0">
				<tr>
					<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
				</tr>
				<tr>
					<TD><webcontrol:policytop id="cltPolicyTop" runat="server"></webcontrol:policytop></TD>
				</tr>
				<tr>
					<td><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></td>
				</tr>
				<tr>
					<td class="headereffectcenter"><asp:Label ID="capHeader" runat="server"></asp:Label></td><%--Corrective User Process--%>
				</tr>
				<tr>
				</tr>
				<tr>
					<td id="tdGridHolder"><webcontrol:gridspacer id="Gridspacer2" runat="server"></webcontrol:gridspacer><asp:placeholder id="GridHolder" runat="server"></asp:placeholder></td>
				</tr>
				<tr>
					<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessage" runat="server"></asp:Label></TD><%--Please note that all fields marked with * are mandatory--%>
				</tr>
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<tr id="trBody">
					<td>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="headereffectcenter" colSpan="4"><span id="spnURStatus" runat="server">Underwriting 
										Rules Status</span></TD>
							</tr>
							<tr>
								<td>
									<div id="myDIV" runat="server" align="center" class="midcolora" style="  OVERFLOW : auto;  WIDTH : 900px;  HEIGHT : 189px"></div>
								</td>
							</tr>
							<tr>
								<td class="midcolora" align="left" width="50%">
									<cmsb:cmsbutton class="clsButton" id="btnRollBack" runat="server" Text="RollBack" CausesValidation="false"
										Visible="True"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnBack_To_Search" runat="server" Text="Back To Search" CausesValidation="false"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnBack_To_Customer_Assistant" runat="server" Text="Back To Customer Assistant"
										CausesValidation="false"></cmsb:cmsbutton>
									<cmsb:cmsbutton align="right" class="clsButton" id="btnCommit_To_Spool" runat="server" Text="Commit"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnCommitInProgress" runat="server" Text="Commit in Progress" CausesValidation="false"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnGet_Premium" Visible="false" style="DISPLAY: inline" runat="server" Text="Get Premium"></cmsb:cmsbutton>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidROW_ID" type="hidden" value="0" name="hidROW_ID" runat="server"> <INPUT id="hidPOLICY_ID" type="hidden" name="hidPOLICY_ID" runat="server">
			<INPUT id="hidPOLICY_VERSION_ID" type="hidden" name="hidPOLICY_VERSION_ID" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" name="hidCUSTOMER_ID" runat="server"> <INPUT id="hidPROCESS_ID" type="hidden" name="hidPROCESS_ID" runat="server">
			<INPUT id="hidNEW_POLICY_VERSION_ID" type="hidden" value="0" name="hidNEW_POLICY_VERSION_ID"
				runat="server"> <INPUT id="hidDisplayBody" type="hidden" name="hidDisplayBody" runat="server">
		</FORM>
	</BODY>
</HTML>

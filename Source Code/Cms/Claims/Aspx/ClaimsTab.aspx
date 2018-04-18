<%@ Register TagPrefix="webcontrol" TagName="ClaimTop" Src="/cms/cmsweb/webcontrols/ClaimTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Page language="c#" Codebehind="ClaimsTab.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.ClaimsTab" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS - Policy Tab</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function CreateMenu()
			{
			if (top.topframe.main1.menuXmlReady == false)
				setTimeout("setMenu();",1000);
				
				top.topframe.main1.activeMenuBar = '2';//Claims
				top.topframe.createActiveMenu();
				
			}
			function showTab()
			{
				//if(<%=strNew_Record%>!=1)
					changeTab(0,0);
			}
			/*function Reload()
			{
				document.location.href = document.location.href + "&NEW_RECORD=<%=strNew_Record%>&HOMEOWNER=<%=strHOMEOWNER%>&RECR_VEH=<%=strRECR_VEH%>&IN_MARINE=<%=strIN_MARINE%>";
				return false;
			}*/
			function SetMenu()
			{
				//Enable/Disable Party menu based on the claim id				
				if('<%=strClaimID%>'!='' && '<%=strClaimID%>'!='0')
				{
					//Enable party menu,reserve and activities at this page
					top.topframe.enableMenu('2,2,0');
					top.topframe.enableMenu('2,2,1');
					//Done for Itrack Issue 6752 on 19 Jan 2010
					//top.topframe.enableMenu('2,2,2');
					//top.topframe.enableMenu('2,2,3');
				}
				else
				{
					//Disable party menu,reserve and activities at this page
					top.topframe.disableMenu('2,2,0');
					top.topframe.disableMenu('2,2,1');
					top.topframe.disableMenu('2,2,2');					
					top.topframe.disableMenu('2,2,3');										
				}
				//Enable customer and policy menus at this page if they are available
				if('<%=strCustomerId%>'!='' && '<%=strCustomerId%>'!='0')
				{
					top.topframe.enableMenu('2,2,5');
					top.topframe.enableMenu('2,2,6');					
					top.topframe.enableMenu('2,2,7');
				}
				else
				{
					//Disable customer and policy menus at this page
					top.topframe.disableMenu('2,2,5');
					top.topframe.disableMenu('2,2,6');
					top.topframe.disableMenu('2,2,7');
				}
			}	
			/*function ValidateCheckbox(objSource,objArgs)
			{
				objArgs.IsValid = false;
			}*/		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="top.topframe.main1.mousein =false;findMouseIn();showTab();showScroll();CreateMenu();SetMenu();">
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<form id="PolicyInformation" method="post" runat="server">
				<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td align="center">
							<asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label>
						</td>
					</tr>
					<tr>
						<TD id="tdClaimTop" class="pageHeader" colSpan="4">
							<webcontrol:ClaimTop id="cltClaimTop" runat="server" width="100%"></webcontrol:ClaimTop>
						</TD>
					</tr>
					<tr id="formTable" runat="server">
						<td>
							<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
								<tr>
									<TD id="tdClientTop" class="pageHeader" colSpan="4">
										<webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer>
									</TD>
								</tr>
								<%--<tr>
									<td colspan="4">
										<table width="100%">
											<tbody id="trHOME" runat="server">
												<tr>
													<TD class="headereffectCenter" align="center" colSpan="4">Please choose the screens</TD>
												</tr>
												<tr>
													<td colspan="3" class="midcolora">
														<asp:CheckBox ID="cblHOMEOWNER" Runat="server" Text="Homeowner"></asp:CheckBox>
														<asp:CheckBox ID="cblRV" Runat="server" Text="Recreational Vehicle"></asp:CheckBox>
														<asp:CheckBox ID="cblIM" Runat="server" Text="Inland Marine"></asp:CheckBox><br>														
													</td>
													<td class="midcolorr" align="right">
														<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Continue"></cmsb:cmsbutton>
													</td>
												</tr>
											</tbody>
										</table>
									</td>
								</tr>--%>
								<tr>
									<td>
										<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="AddClaimsNotification.aspx?" TabTitles="Claims Notification"
											TabLength="80"></webcontrol:Tab>
									</td>
								</tr>
								<tr>
									<td>
										<table class="tableeffect" cellpadding="0" cellspacing="0" border="0">
											<tr>
												<td>
													<iframe class="iframsHeightLong" id="tabLayer" runat="server" src="" scrolling="no" frameborder="0"
														width="100%"></iframe>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<asp:Label ID="lblTemplate" Runat="server" Visible="false"></asp:Label>
								<tr>
									<td>
										<webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</form>
		</div>
	</body>
</HTML>

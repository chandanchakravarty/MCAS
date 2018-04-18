<%@ Page language="c#" Codebehind="ActivityIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.ActivityIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClaimTop" Src="/cms/cmsweb/webcontrols/ClaimTop.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Activity Index</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<!-- include these scripts in page where you are using windows grid -->
		<SCRIPT src="/cms/cmsweb/scripts/common.js"></SCRIPT>
		<SCRIPT src="/cms/cmsweb/scripts/form.js"></SCRIPT>
		<STYLE>
		.hide { OVERFLOW: hidden; TOP: 5px }
		.show { OVERFLOW: hidden; TOP: 5px }
		#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">
	
		
		function onRowClicked(num,msDg )
		{
			//alert(strXML)
			rowNum=num;
			/*if(parseInt(num)==0)
				strXML="";
			*/
			populateXML(num,msDg);		
			changeTab(0, 0);
		}		
		function Init()
		{
			/*setfirstTime();
			top.topframe.main1.mousein = false;
			findMouseIn();			*/
			setfirstTime(); // Added by Asfa (08-Feb-2008) - iTrack issue #3588
			if(document.getElementById("hidReserveAdded").value=="" || document.getElementById("hidReserveAdded").value=="0" || document.getElementById("hidReserveAdded").value=="2")
			{
				top.topframe.disableMenu('2,2,3');
				top.topframe.disableMenu('2,2,2');
			}
			else
			{
				top.topframe.enableMenu('2,2,3');
				top.topframe.enableMenu('2,2,2');
			}
			
			if(document.getElementById("hidACTIVITY_ID").value=="" || document.getElementById("hidACTIVITY_ID").value=="0")
				return false;
			if(document.getElementById("hidACTIVITY_ID").value!='-1')
				addRecord();
			return false;
		}
		
		</script>
	</HEAD>
	<body oncontextmenu="return false;" onload="Init();" MS_POSITIONING="GridLayout">
		
		<!-- To add bottom menu -->
		<%--<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>--%>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<form id="indexForm" method="post" runat="server">
				<table width="99%" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td>
							<table width="99%" cellSpacing="0" cellPadding="0" border="0">
								<%--<tr>
									<td id="tdWorkflow" class="pageHeader" colspan="4">
										<webcontrol:ClaimTop id="cltClaimTop" runat="server" ></webcontrol:ClaimTop>
									</td>
								</tr>--%>
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<p align="center"><asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label></p>
								<tr>
									<td class=midcolorr>
										<asp:checkbox id="chkShowAll" AutoPostBack="True" runat="server" Text="Show All"></asp:checkbox>
									</td>
								</tr>
								<tr>
									<td>
										<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
									</td>
								</tr>
								<tr>
									<td class=midcolora><asp:Label ID="lblSummaryRow" Runat="server" Width="100%"></asp:Label></td>
								</tr>
								<tr>
									<td><webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer></td>
								</tr>
								<tr>
									<td>
										<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
											<tr id="tabCtlRow" runat="server">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="AddExpertServiceProviders.aspx?" TabTitles="Expert Service Providers Details"
														TabLength="250"></webcontrol:Tab>
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
										</table>
									</td>
								</tr>
								<tr>
									<td>
										<webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
									</td>
								</tr>
							</table>
							<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
							<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
							<input type="hidden" id="hidACTIVITY_ID" name="hidACTIVITY_ID" runat="server">
							<input type="hidden" id="hidAUTHORIZE" name="hidAUTHORIZE" runat="server">
							<input type="hidden" id="hidReserveAdded" name="hidReserveAdded" runat="server" value="0">
						</td>
					</tr>
				</table>
			</form>
		</div>
		<DIV></DIV>
	</body>
</HTML>

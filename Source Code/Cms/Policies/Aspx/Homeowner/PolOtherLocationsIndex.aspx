<%@ Page language="c#" Codebehind="PolOtherLocationsIndex.aspx.cs" AutoEventWireup="false" Inherits="Policies.Aspx.Homeowner.PolOtherLocationsIndex" validateRequest = "false"%>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>PolOtherLocationsIndex</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
	<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<!--<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">-->
		<STYLE>
			.hide { OVERFLOW: hidden; TOP: 5px }
			.show { OVERFLOW: hidden; TOP: 5px }
			#tabContent { POSITION: absolute; TOP: 160px }
			</STYLE>
		<script language="javascript">
	
		function onRowClicked(num,msDg )
		{
			rowNum = num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);		
			changeTab(0, 0);
			
		}
		function CheckScroll()
		{			
			if(document.getElementById('hidScreenCheck').value == "1")
			{				
				showScroll();								
				top.topframe.main1.mousein = false;
				findMouseIn();
			}		
		}
	
		</script>
</HEAD>
 <body leftmargin="0" topmargin="0" onload="setfirstTime();CheckScroll();"
		MS_POSITIONING="GridLayout">		
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" visible="false" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div class="pageContent" id="bodyHeight">
			<table id="tblGridTable" class=<%=tblHeaderClass%> cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<TD class="pageHeader" colSpan="4">
						<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
					</TD>
				</tr>
					<tr>
									<TD id = "tdClientTop" class="pageHeader" colSpan="4">
										<webcontrol:ClientTop id="cltClientTop" runat="server" width ="98%" ></webcontrol:ClientTop>
									</TD>
								</tr>
				<tr>
					<td>
						<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
							<form id="indexForm" method="post" runat="server">
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<p align="center"><asp:label id="capMessage" CssClass="errmsg" Visible="False" Runat="server"></asp:label></p>
								<tr>
									<td id="tdGridHolder">
										<webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer>
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder></td>
								</tr>
								<tr>
									<td><webcontrol:GridSpacer id="Gridspacer" runat="server"></webcontrol:GridSpacer></td>
								</tr>
								<tr>
									<td>
										<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
											<tr id="tabCtlRow">
												<td><webcontrol:tab id="TabCtl" runat="server" TabLength="150" TabTitles="Other Locations" TabURLs="AddOtherLocations.aspx?"></webcontrol:tab></td>
											</tr>
											<tr>
												<td>
													<table class="tableeffect" cellSpacing="0" cellPadding="0" width="100%" border="0">
														<tr>
															<td><iframe class="iframsHeightLong" id="tabLayer" src="" frameBorder="0" width="100%" scrolling="no"
																	runat="server"></iframe>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<asp:label id="lblTemplate" Visible="false" Runat="server"></asp:label></table>
									</td>
								</tr>
								<tr>
									<td><webcontrol:footer id="pageFooter" runat="server"></webcontrol:footer></td>
								</tr>
						</table>
						<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
						<input type="hidden" id="hidScreenCheck" runat="server" value="0" NAME="hidScreenCheck">
						<input type="hidden" name="hidlocQueryStr"> <input type="hidden" name="hidMode">
						</FORM></td>
				</tr>
			</table>
		</div>
		<DIV></DIV>
	</body>
</HTML>





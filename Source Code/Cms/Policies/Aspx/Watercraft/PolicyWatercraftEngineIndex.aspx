<%@ Page language="c#" Codebehind="PolicyWatercraftEngineIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Watercrafts.PolicyWatercraftEngineIndex" ValidateRequest="false"%>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<HTML>
	<HEAD>
		<title>PriorLossIndex</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<SCRIPT src="/cms/cmsweb/scripts/menubar.js"></SCRIPT>
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
		
		<STYLE>
		.hide { OVERFLOW: hidden; TOP: 5px }
		.show { OVERFLOW: hidden; TOP: 5px }
		#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
			<script language="javascript">
	
		
		function onRowClicked(num,msDg )
		{
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);		
			changeTab(0, 0);
		}
		
		function check()
		{
			firstTime=1;
		}
		</script>
	</HEAD>
	<body onload="top.topframe.main1.mousein = false;check();" MS_POSITIONING="GridLayout">
		
		<div id="bodyHeight" class="pageContent">
			<form id="indexForm" method="post" runat="server">
			<table class="tableWidthHeader" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<TD>
						<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>								
					</TD>
				</tr>
				<tr>
					<td>
						<table  class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
							<tr>
								<td align=center >
									<asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label>
								</td>
							</tr>
							<tr>
								<td>
									<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
									<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
								</td>
							</tr>							
							<tr><td><webcontrol:GridSpacer id="Gridspacer" runat="server"></webcontrol:GridSpacer></td></tr>
							<tr>
								<td>					
									<table class="tableWidthHeader" width="100%" cellSpacing="0" cellPadding="0" border="0" align="center">
										<tr id="tabCtlRow">
											<td>
												<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="PolicyAddWatercraftEngine.aspx?" TabTitles="Outboard Engine Information" TabLength="205"></webcontrol:Tab>
											</td>
										</tr>
										<tr>
											<td>
												<table class="tableWidthHeader" width="100%" cellpadding="0" cellspacing="0" border="0">
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
						<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
					
				</td>
			</tr>
		</table>
		</form>
		</div>
		</div>
	</body>
</HTML>


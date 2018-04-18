<%@ Page language="c#" Codebehind="AG_CheckListingIframe.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AG_CheckListingIframe" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="~/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>ACT_CHECK_INFORMATION</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
			<script language="javascript">
			function SetLink()
			{
				var strlink = '<%=LinkPageName%>';
				document.getElementById('tabLayer').src = strlink;
			}
	</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;showScroll();findMouseIn();SetLink();changeTab(0,0);">
	<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
     <div id="bodyHeight" class="pageContent">
			<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
							<form id="indexForm" method="post" runat="server">
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<tr>
									<TD id="tdClientTop" class="pageHeader" colSpan="4">
										<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
									</TD>
								</tr>
								<tr>
									<td>
										<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
											<tr id="tabCtlRow">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="DepositIndex.aspx?" TabTitles="Checks"
														TabLength="110"></webcontrol:Tab>
												</td>
											</tr>
											<tr>
												<td>
													<table class="tableeffect" cellpadding="0" cellspacing="0" border="0">
														<tr>
															<td>
																<iframe class="iframsHeightLong" id="tabLayer" runat="server" src="" frameborder="0"
																 scrolling="no"	width="100%" ></iframe>
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
						<!--//added on 27/04/2005-->
						<input type="hidden" name="hidlocQueryStr"> <input type="hidden" name="hidMode">
						<!--//added on 27/04/2005--> </FORM>
						<input type="hidden" id="hidCalledFrom" name = "hidCalledFrom" runat="server">
					</td>
				</tr>
			</table>
		</div>
	
  </body>
</html>

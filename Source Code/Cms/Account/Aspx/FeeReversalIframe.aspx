<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="FeeReversalIframe.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.FeeReversalIframe" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FeeReversalIframe</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=base.GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
			function showTab()
			{
				changeTab(0,0);
			}
		
			function SetTab()
				{
			//		Url="feereversal.aspx";    
			//		 
			//		DrawTab(2,top.frames[1],'Discounts/Surcharges',Url); 	
					return;
				}
				
				
				
		</script>
	</HEAD>
	<body leftMargin="0" topmargin="0" rightmargin="0" onload="top.topframe.main1.mousein =false;findMouseIn();SetTab();changeTab(0,0);showScroll();"
		MS_POSITIONING="GridLayout">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<form id="Form1" method="post" runat="server">
				<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
					<tr id="formTable" runat="server">
						<td>
							<table class="tableWidthHeader" cellSpacing="1" cellPadding="0" border="0" align="center">
								<tr>
									<TD id="tdClientTop" class="pageHeader" colSpan="4">
										<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
									</TD>
								</tr>
								<tr>
									<td>
										<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="FeeReversal.aspx?" TabTitles="Fee Reversal"
											TabLength="200"></webcontrol:Tab>
									</td>
								</tr>
								<tr>
									<td>
										<table class="tableeffect" cellpadding="0" cellspacing="0" border="0">
											<tr>
												<td>
													<iframe id="tabLayer" scrolling="no" class="iframsHeightLong" frameborder="0" width="100%"
														src="" height="100%"></iframe>
												</td>
											</tr>
										</table>
									</td>
								</tr>
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

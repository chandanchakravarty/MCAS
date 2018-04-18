<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Page language="c#" Codebehind="PolicyGeneralInfoFrame.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.aspx.HomeOwners.PolicyGeneralInfoFrame" %>
<HTML>
	<HEAD>
		<title></title>
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
			function showTab()
			{
				changeTab(0,0);
			}
		
			function SetTab()
			{
				Url="../../Aspx/PolicyShowDiscountAndSurcharges.aspx";    
				
				DrawTab(2,top.frames[1],'Discount And Surcharge',Url); 	
				return;
			}			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="top.topframe.main1.mousein =false;findMouseIn();changeTab(0,0);showScroll();">
		<!-- To add bottom menu -->
		<webcontrol:Menu id="Menu1" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<form id="Form1" method="post" runat="server">
				<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td align="center">
							<asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label>
						</td>
					</tr>
					<tr id="formTable" runat="server">
						<td>
							<table class="tableWidthHeader" cellSpacing="1" cellPadding="0" border="0" align="center">
								<tr>
									<td id="tdWorkflow" class="pageHeader" colspan="4">
										<webcontrol:WorkFlow isTop="false" id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
									</td>
								</tr>
								<tr>
									<TD id="tdClientTop" class="pageHeader" colSpan="4">
										<webcontrol:ClientTop id="cltClientTop" runat="server" width="100%"></webcontrol:ClientTop>
										<webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer>
									</TD>
								</tr>
								<tr>
									<td>
										<webcontrol:Tab ID="TabCtl" runat="server" TabTitles="Underwriting Questions"
											TabLength="250"></webcontrol:Tab>
									</td>
								</tr>
								<tr>
									<td>
										<table class="tableeffect" cellpadding="0" cellspacing="0" border="0">
											<tr>
												<td>
													<iframe id="tabLayer" class="iframsHeightLong" frameborder="0" width="100%" src="" height="100%"
														scrolling="no"></iframe>
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

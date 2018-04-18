<%@ Register TagPrefix="webcontrol" TagName="ClaimActivityTop" Src="/cms/cmsweb/webcontrols/ClaimActivityTop.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClaimTop" Src="/cms/cmsweb/webcontrols/ClaimTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PaymentDetailsTab.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.PaymentDetailsTab" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Underwritting Questions</title>
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
			var ActivityClientID;
			var ActivityTotalPaymentClientID;
			function showTab()
			{
				changeTab(0,0);
			}
			function GoBack(PageName)
			{
				strURL = PageName + "?&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&ACTION_ON_PAYMENT=" + document.getElementById("hidACTION_ON_PAYMENT").value + "&";
				this.document.location.href = strURL;
				return false;
			}	
			function Init()
			{
				top.topframe.main1.mousein =false;
				findMouseIn();
				changeTab(0,0);
				showScroll();
				ActivityClientID = '<%=ActivityClientID%>'
				ActivityTotalPaymentClientID = '<%=ActivityTotalPaymentClientID%>'
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="Init();">
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
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
									<TD id="tdClaimTop" class="pageHeader" colSpan="4">
										<webcontrol:ClaimTop id="cltClaimTop" runat="server" width="100%"></webcontrol:ClaimTop>
									</TD>
								</tr>
								<tr>
									<td id="tdWorkflow" class="pageHeader" colspan="4">
										<webcontrol:ClaimActivityTop id="cltClaimActivityTop" runat="server"></webcontrol:ClaimActivityTop>
									</td>
								</tr>
								<tr>
									<td align="right" colSpan="4"><cmsb:cmsbutton class="clsButton" id="btnGoBack" runat="server" Text="Claim Activities"></cmsb:cmsbutton></td>
								</tr>
								<tr>
									<td>
										<webcontrol:Tab ID="TabCtl" runat="server" TabLength="200"></webcontrol:Tab>
									</td>
								</tr>
								<tr>
									<td>
										<table class="tableeffect" cellpadding="0" cellspacing="0" border="0">
											<tr>
												<td>
													<iframe class="iframsHeightExtraLong" id="tabLayer" runat="server" src="" scrolling="no" frameborder="0"
														width="100%" marginwidth="0" marginheight="0"></iframe>
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
				<INPUT id="hidCLAIM_ID" type="hidden" name="hidCLAIM_ID" runat="server"> <INPUT id="hidACTIVITY_ID" type="hidden" name="hidACTIVITY_ID" runat="server">
				<INPUT id="hidACTION_ON_PAYMENT" type="hidden" name="hidACTION_ON_PAYMENT" runat="server">
			</form>
		</div>
	</body>
</HTML>

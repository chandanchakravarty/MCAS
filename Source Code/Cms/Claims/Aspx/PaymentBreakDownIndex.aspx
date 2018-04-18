<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Page language="c#" Codebehind="PaymentBreakDownIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.PaymentBreakDownIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="ClaimTop" Src="/cms/cmsweb/webcontrols/ClaimTop.ascx"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PaymentBreakDownIndex</title>
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
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);		
			changeTab(0, 0);
		}		
		function Init()
		{
			ApplyColor();
			ChangeColor();
			setfirstTime();	
			findMouseIn();			
			top.topframe.main1.mousein = false;		
		}
		
		</script>
	</HEAD>
	<body onload="setfirstTime();" leftmargin="0" rightmargin="0" MS_POSITIONING="GridLayout">		
		
		<div id="bodyHeight" class="pageContent">
			<table width="95%" border="0" cellpadding="0" cellspacing="0">
				<%--<tr>
					<TD id="tdClaimTop" class="pageHeader" colSpan="4">
						<webcontrol:ClaimTop id="cltClaimTop" runat="server" width="100%"></webcontrol:ClaimTop>
					</TD>
				</tr>--%>
				<tr>
					<td>
						<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
							<form id="indexForm" method="post" runat="server">
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<p align="center"><asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label></p>
								<tr>
									<td>
										<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
									</td>
								</tr>
								<tr>
									<td><webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer></td>
								</tr>
								<tr>
									<td>
										<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
											<tr id="tabCtlRow" runat="server">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server" TabTitles="Claims Payment Breakdown" TabLength="250"></webcontrol:Tab>
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
						<input type="hidden" name="hidCLAIM_ID" runat="server" id="hidCLAIM_ID"> <input type="hidden" name="hidACTIVITY_ID" runat="server" id="hidACTIVITY_ID">
						<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
						</FORM>
					</td>
				</tr>
			</table>
		</div>
		<DIV></DIV>
	</body>
</HTML>

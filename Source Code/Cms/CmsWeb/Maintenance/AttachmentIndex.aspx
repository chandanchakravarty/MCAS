<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Page language="c#" Codebehind="AttachmentIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AttachmentIndex" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<STYLE>
            .hide { OVERFLOW: hidden; TOP: 5px }
            .show { OVERFLOW: hidden; TOP: 5px }
            #tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<!-- include these scripts in page where you are using windows grid -->
		<SCRIPT src="/cms/cmsweb/scripts/common.js"></SCRIPT>
		<SCRIPT src="/cms/cmsweb/scripts/form.js"></SCRIPT>
		<SCRIPT event="rowClicked" for="gridObject">
		//rowClicked();
		</SCRIPT>
		<script language="javascript">
			function onRowClicked(num,msDg )
			{
				rowNum=num;
				if(parseInt(num)==0)
					strXML="";
				populateXML(num,msDg);		
				changeTab(0, 0);
			}
			
			function findMouseIn()
			{
				if(!top.topframe.main1.mousein)
				{
					//createActiveMenu();
					top.topframe.main1.mousein = true;
				}
				setTimeout('findMouseIn()',5000);
			}
			
		</script>
		<!-- include these scripts in page where you are using windows grid ends here -->
	</HEAD>
	<body oncontextmenu="return false;" class="bodyBackGround" topmargin="0" leftmargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();showScroll();"
		MS_POSITIONING="GridLayout">		
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<table class="tableWidthHeader" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
							<form id="indexForm" method="post" runat="server">
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
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
											<tr id="tabCtlRow">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server" ></webcontrol:Tab>
												</td>
											</tr>
											<tr>
												<td>
													<table class="tableeffect" width="100%" cellpadding="0" cellspacing="0" border="0">
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
						<input type="hidden" name="hidEntityType" id="hidEntityType" runat="server"> <input type="hidden" name="hidEntityId" id="hidEntityId" runat="server">
						</FORM>
					</td>
				</tr>
			</table>
		</div>
		<DIV></DIV>
	</body>
</HTML>













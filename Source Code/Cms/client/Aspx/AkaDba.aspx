<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="../../cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="~/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="AkaDba.aspx.cs" AutoEventWireup="false" Inherits="Cms.Client.Aspx.AkaDba" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="~/cmsweb/webcontrols/basetabcontrol.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AkaDba</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<STYLE>
            .hide { OVERFLOW: hidden; TOP: 5px }
            .show { OVERFLOW: hidden; TOP: 5px }
            #tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<SCRIPT src="../../cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script language="javascript">
			function onRowClicked(num,msDg )
			{
				if(parseInt(num)==0)
					strXML="";
				populateXML(num,msDg);		
				changeTab(0, 0);
			}
			
		/********************************************************************************************************/
		</script>
	</HEAD>
	<body leftmargin="0" topmargin ="0" onload="setfirstTime();" MS_POSITIONING="GridLayout">
		<DIV id="myTSMain" style="BEHAVIOR: url(/cms/cmsweb/htc/webservice.htc)"></DIV>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
					<form id="indexForm" method="post" runat="server">
						<table class="tableWidthHeader"  cellSpacing="0" cellPadding="0" border="0" align="center">
								<tr>
									<td>
										
										<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
										<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
									</td>
								</tr>
								<tr><td><webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer></td></tr>
								<tr>
									<td>
										<table class="tableWidthHeader"  cellSpacing="0" cellPadding="0" border="0" align="center">
											<tr id="tabCtlRow">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server"  TabTitles="AKA/DBA Information" TabLength="150"></webcontrol:Tab>
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
											<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
											<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
									</td>
								</tr>
								<tr>
									<td>
										<webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
									</td>
								</tr>
						</table>
						</FORM>
		</div>
		<DIV></DIV>
	</body>
</HTML>

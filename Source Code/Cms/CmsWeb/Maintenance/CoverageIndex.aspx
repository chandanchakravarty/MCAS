<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Page language="c#" Codebehind="CoverageIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.CoverageIndex" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>EmailIndex</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
		<STYLE>.hide { OVERFLOW: hidden; TOP: 5px }
	.show { OVERFLOW: hidden; TOP: 5px }
	#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">
					
		function onRowClicked(num,msDg )
		{
			document.getElementById('hidCalledFrom').value = 'Index';
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
	</HEAD>
	<body oncontextmenu = "return false;" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();" MS_POSITIONING="GridLayout">		
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<div class="pageContent" id="bodyHeight">
			<FORM id="indexForm" method="post" runat="server">
				<table class="tableWidth" cellSpacing="0" cellPadding="0" border="0">
					<tr>
						<td>
							<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
								<tr>
									<td align="center"><asp:label id="capMessage" CssClass="errmsg" Visible="False" Runat="server"></asp:label></td>
								</tr>
								<!-- <input id="hide" type="hidden" name="ConVar"><span id="singleRec"></span> -->
								<tr>
									<TD class="pageHeader" id="tdClientTop" colSpan="4"><webcontrol:clienttop id="cltClientTop" runat="server" width="100%"></webcontrol:clienttop></TD>
								</tr>
								<tr>
									<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer><asp:placeholder id="GridHolder" runat="server"></asp:placeholder></td>
								</tr>
								<tr>
									<td><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></td>
								</tr>
								<tr>
									<td>
										<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
											<tr id="tabCtlRow">
												<td><webcontrol:tab id="TabCtl" runat="server" TabLength="150" ></webcontrol:tab></td>
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
							<input type="hidden" name="hidTemplateID" id="Hidden1" runat="server"> <input type="hidden" name="hidRowID" id="Hidden2" runat="server">
							<input id="hidlocQueryStr" type="hidden" name="hidlocQueryStr" runat="server"> <input type="hidden" name="hidMode" id="Hidden3" runat="server">
							<input id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server">
						</td>
					</tr>
				</table>
			</FORM>
		</div>
		<DIV></DIV>
	</body>
</HTML>

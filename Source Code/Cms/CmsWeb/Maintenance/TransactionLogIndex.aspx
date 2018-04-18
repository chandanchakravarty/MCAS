<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Page language="c#" Codebehind="TransactionLogIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.TransactionLogIndex1" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Ebix Advantage-<%=hidHeader%></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet />
		<script src="/cms/cmsweb/scripts/xmldom.js" type="text/javascript"></script>
		<script src="/cms/cmsweb/scripts/common.js" type="text/javascript"></script>
		<script src="/cms/cmsweb/scripts/form.js" type="text/javascript"></script>
		<link href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet" />
		<style type="text/css">
		.hide {OVERFLOW: hidden; TOP: 5px	}
		.show {OVERFLOW: hidden; TOP: 5px	}
		#tabContent {POSITION: absolute; TOP: 160px	}
		</style>
		<script language="javascript" type="text/javascript">
					
		function onRowClicked(num,msDg )
		{
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
			
		function Check()
		{
			var temp = '<%=strCalledFrom%>';
			if(temp.toUpperCase()!="INCLT")
			{
				setfirstTime();
				//top.topframe.main1.mousein = false;
				findMouseIn();
			}
			else {
			    try {
                      top.topframe.main1.mousein = true;
				     } catch (err) { }
			    }
		}
		</script>
	</head>
	<body oncontextmenu="return false;" class="bodyBackGround" onload="Check();try {top.topframe.main1.mousein = false;}catch (err) { }" MS_POSITIONING="GridLayout" >		
		<!-- To add bottom menu -->
		<webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<div class="pageContent" id="bodyHeight">
			<form id="indexForm" method="post" runat="server">
				<table class="<%=strCssClass%>" cellspacing="0" cellpadding="0" border="0">
					<tr>
						<td>
							<table class="tableWidthHeader" cellspacing="0" cellpadding="0" align="center" border="0">
								<tr>
									<td align="center"><asp:label id="capMessage" CssClass="errmsg" Visible="False" Runat="server"></asp:label></td>
								</tr>
								<!-- <input id="hide" type="hidden" name="ConVar"><span id="singleRec"></span> -->
								<tr>
									<TD class="pageHeader" id="tdClientTop" colSpan="4"><webcontrol:clienttop id="cltClientTop" runat="server" width="100%"></webcontrol:clienttop></TD>
								</tr>
								
								<tr>
									<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer>
									<div id="myid" style="position:relative;height:210pt;overflow:auto;">
									<asp:placeholder id="GridHolder" runat="server"></asp:placeholder></div></td>
								</tr>
								<tr>
									<td><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></td>
								</tr>
								<tr>
									<td>
										<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
											<tr id="tabCtlRow">
												<td><webcontrol:tab id="TabCtl" runat="server" TabLength="150" TabTitles="Transaction Detail" TabURLs="TransactionLogDetail.aspx?"></webcontrol:tab></td>
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
							<input type="hidden" name="hidTemplateID" /> <input type="hidden" name="hidRowID" />
							<input id="hidlocQueryStr" type="hidden" name="hidlocQueryStr" /> <input type="hidden" name="hidMode" />
							<input id="hidPOLICY_NO" type="hidden" runat=server name="hidPOLICY_NO" />
							
							
						</td>
					</tr>
				</table>
			</form>
		</div>
		<div></div>
	</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DiscountSurchargeIndex.aspx.cs" Inherits="Cms.CmsWeb.Maintenance.DiscountSurchargeIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<HEAD>
		<title>Discount</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css" />
		<script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
		<link href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet" />
		<style type="text/css">
		.hide { OVERFLOW: hidden; POSITION: absolute; TOP: 5px }
		.show { OVERFLOW: hidden; POSITION: absolute; TOP: 5px }
		#tabContents { LEFT: 60px; POSITION: absolute; TOP: 160px }
		</style>
		<script language="javascript" type="text/javascript">	


		    function onRowClicked(num, msDg) {
		        rowNum = num;
		        if (parseInt(num) == 0)
		            strXML = "";
		        populateXML(num, msDg);
		        changeTab(0, 0);
		    }
		    
		    function CreateMenu() {
		        if (top.topframe.main1.menuXmlReady == false)
		            setTimeout("setMenu();", 1000);

		        top.topframe.main1.activeMenuBar = '5'; //Maintenance
		        top.topframe.createActiveMenu();

		    }
		</script>
	</HEAD>
	<body oncontextmenu = "javascript:return false;" onload="javascript:setfirstTime();top.topframe.main1.mousein = false;findMouseIn();" MS_POSITIONING="GridLayout">		
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<table class ="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
							<form id="indexForm" method="post" runat="server">
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<p align="center"><asp:Label ID="capMessage" runat="server" Visible="False" CssClass="errmsg"></asp:Label></p>
								<tr>
									<td>
									<webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer>
					
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
									</td>
								</tr>
								<tr>
								<td><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></td>
                                    </tr>
								<tr>
									<td style="width:100%";>
										<table class ="tableWidthHeader" cellSpacing="" cellPadding="0" border="0" align="left">
											<tr id="tabCtlRow">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server" TabLength="150"></webcontrol:Tab>
												</td>
											</tr>
											<tr>
												<td>
													<table class="tableeffect"  cellpadding="0" cellspacing="0" border="0">
														<tr>
															<td width="100%">
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
						<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID"/>
						<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode"/>
						<input type="hidden" id="hidAddNew" name="hidAddNew" runat="server" value=""/>
						</form>
					</td>
				</tr>
			</table>
		</div>
	</body>
</html>
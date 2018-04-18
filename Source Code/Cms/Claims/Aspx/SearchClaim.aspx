<%@ Page language="c#" Codebehind="searchclaim.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.SearchClaim" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Claims Index</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<!--<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">-->
		<STYLE>
		.hide { OVERFLOW: hidden; TOP: 5px }
		.show { OVERFLOW: hidden; TOP: 5px }
		#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">
		
		function findMouseIn()
		{
			if(!top.topframe.main1.mousein)
			{
				//createActiveMenu();
				top.topframe.main1.mousein = true;
			}
			setTimeout('findMouseIn()',5000);
		}
		function onRowClicked(num,msDg )
		{ 
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";			
			populateXML(num,msDg);		
			if ( !(event.srcElement.outerHTML.indexOf("OPTION") != -1 || event.srcElement.outerHTML.indexOf("button_Click") != -1))
			{
				//top.botframe.location.href = "/cms/claims/aspx/ClaimsNotificationIndex.aspx?" + locQueryStr + "&" ;				
				top.botframe.location.href = "/cms/claims/aspx/ClaimsTab.aspx?" + locQueryStr + "&&" ;
			}
			//changeTab(0,0);
		}
		
		function addNewClaim()
		{
		    document.location.href = "/cms/claims/aspx/policy/SearchPolicy.aspx?";
		    //window.open("http://192.168.91.73:8080/ETS-RV4-WebApp/ProcessLogin.do?userId=SYSTEM&password=pass1!");
		}
		function SetMenu()
		{
			//disable App/Policy Menu added By Pravesh on 3 dec 08 as sesion values are being set to blank on this page
			top.topframe.disableMenus("1","ALL");
			top.topframe.enableMenu("1,0");
			top.topframe.enableMenu("1,1");
			top.topframe.disableMenu("1,1,1");
			top.topframe.disableMenu("1,1,2");
			// end here Added by Pravesh
			//Disable party,reserve and activities menu at this page
			top.topframe.disableMenu('2,2,0');
			top.topframe.disableMenu('2,2,1');
			top.topframe.disableMenu('2,2,2');
			top.topframe.disableMenu('2,2,3');
			//Disable customer and policy menus at this page
			top.topframe.disableMenu('2,2,4');
			top.topframe.disableMenu('2,2,5');
			top.topframe.disableMenu('2,2,6');
			top.topframe.disableMenu('2,2,7');
		}
		
		</script>
	</HEAD>
	<body oncontextmenu="return false;" leftmargin="0" topmargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();SetMenu();"
		MS_POSITIONING="GridLayout">
		
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<form id="indexForm" method="post" runat="server">
				<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td align="center" colSpan="4"><asp:label id="lblError" runat="server" cssClass="errMsg"></asp:label></td>
					</tr>
					<tr>
						<td>
							<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
								<tr>
									<td>
										<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
									</td>
								</tr>
								<tr>
									<td><webcontrol:GridSpacer id="Gridspacer" runat="server"></webcontrol:GridSpacer></td>
								</tr>
								<tr>
									<td>
										<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
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
							<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
							<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
						</td>
					</tr>
				</table>
			</form>
		</div>
		<DIV></DIV>
	</body>
</HTML>


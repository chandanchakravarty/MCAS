<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="SearchCustomerClaimIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.SearchCustomerClaimIndex" %>
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
		<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
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
				top.topframe.callItemClicked('2','')
				top.botframe.location.href = "/cms/claims/aspx/ClaimsTab.aspx?" + locQueryStr + "&&" ;
			}
			//changeTab(0,0);
		}
		
		function addNewClaim()
		{
			//document.location.href = "/cms/claims/aspx/policy/SearchPolicy.aspx?"; // commented By Pravesh on 3 Dec 08 
			top.topframe.callItemClicked('2','')
			top.botframe.location.href ="/cms/claims/aspx/policy/SearchPolicy.aspx?CUSTOMER_ID=" + document.getElementById('hidCUSTOMER_ID').value;
		}
		function SetMenu()
		{
			//Done for Itrack Issue 6619 on 4 Nov 09
			if(document.getElementById("hidWolverineUser").value != "0")
			{
				//Disable party,reserve and activities menu at this page
				top.topframe.disableMenu('2,2,0');
				top.topframe.disableMenu('2,2,1');
				top.topframe.disableMenu('2,2,2');
				top.topframe.disableMenu('2,2,3');
				//Disable customer and policy menus at this page
				top.topframe.disableMenu('2,2,4');
				top.topframe.disableMenu('2,2,5');
				top.topframe.disableMenu('2,2,6');
			}
		}
		
		</script>
	</HEAD>
	<body oncontextmenu="return false;" leftmargin="0" topmargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();SetMenu();"
		MS_POSITIONING="GridLayout">		
		<div id="bodyHeight" class="pageContent">
			<form id="indexForm" method="post" runat="server">
				<table class="tableWidthHeader" border="0" cellpadding="0" cellspacing="0">
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
							<input id="hidWolverineUser" name="hidWolverineUser" type="hidden" runat="server">
							<input id="hidCUSTOMER_ID" name="hidCUSTOMER_ID" type="hidden" runat="server">
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

<%@ Page language="c#" Codebehind="PolicySolidFuelIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.aspx.HomeOwners.PolicySolidFuelIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS-SOLID FUEL</title>
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
	
		var TabNo;
		
		function onRowClicked(num,msDg )
		{
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);		
		}
		
			//=============================================================================================
		function SetTabs(pretab,loadPage)
		{
			if (strSelectedRecordXML != "")		//If record xml exists then adding the tabs
			{
				var CalledFrom	= '<%=strCalledFrom%>';	//Stores the refrence , where the form is called from	
			    var objXmlHandler = new XMLHandler();
				var tree = objXmlHandler.quickParseXML(strSelectedRecordXML);	
							
				if (tree != null)
				{
					//Fetching the values from xml to be passed in url as query string					
					var strPolId			= tree.getElementsByTagName('POLICY_ID')[0].firstChild.text;
					var strPolVerId			= tree.getElementsByTagName('POLICY_VERSION_ID')[0].firstChild.text;
					var strCustomerId		= tree.getElementsByTagName('CUSTOMER_ID')[0].firstChild.text;
					var strFuelId			= tree.getElementsByTagName('FUEL_ID')[0].firstChild.text;								
					
					if(CalledFrom=='Home' || CalledFrom=='HOME')
					{		
						Url="PolicyAddFireProt.aspx?CUSTOMER_ID=" + strCustomerId
						+ "&POL_ID=" + strPolId 
						+ "&POL_VERSION_ID=" + strPolVerId 
						+ "&FUELID=" + strFuelId 
						+ "&CalledFrom=" + CalledFrom
						+ "&"
					    DrawTab(2,this,'Fire Protection/Cleaning Details',Url,null,pretab,! loadPage); 	
						Url="PolicyAddChimneyStove.aspx?CUSTOMER_ID=" + strCustomerId
						+ "&POL_ID=" + strPolId 
						+ "&POL_VERSION_ID=" + strPolVerId
						+ "&CalledFrom=" + CalledFrom
						+ "&"
						DrawTab(3,this,'Chimney / Stovepipe',Url,null,pretab,! loadPage); 	
					}
						return;
				}
			}
			//Record not exists , hence only one tab should come
			//Hence removing other tabs if exists
			RemoveTab(3,this);	
			RemoveTab(2,this);	
			if (loadPage)
					changeTab(0,0);
		}
		//========================================================================================================
				
		</script>
	</HEAD>
	<body leftmargin="0" topmargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();" MS_POSITIONING="GridLayout">
		
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<form id="indexForm" method="post" runat="server">
				<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td>
							<table   class ="tableWidthHeader"  cellSpacing="0" cellPadding="0" border="0" align="center">
							
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<p align="center"><asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label></p>
								<tr>
									<td id="tdWorkflow" class="pageHeader" colspan="4">
										<webcontrol:WorkFlow   id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
									</td>
								</tr>
								<tr>
									<TD id="tdClientTop" class="pageHeader" colSpan="4">
										<webcontrol:ClientTop id="cltClientTop" runat="server" width="98%"></webcontrol:ClientTop>
									</TD>
								</tr>
								<tr>
									<td id="tdGridHolder">
										<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
									</td>
								</tr>
								<tr><td><webcontrol:GridSpacer id="Gridspacer" runat="server"></webcontrol:GridSpacer></td></tr>
								<tr>
									<td>
										<table class ="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
											<tr>
												<td id="tabCtlRow">
													<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="AddSolidFuel.aspx?" TabTitles="Solid Fuel Details"
														TabLength="225"></webcontrol:Tab>
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
						<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
						</FORM>
					</td>
				</tr>
			</table>
		</div>
		<DIV></DIV>
	</body>
</HTML>

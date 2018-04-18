<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Page language="c#" Codebehind="RecrVehiclesIndex.aspx.cs" AutoEventWireup="false" Inherits="Policies.Aspx.Homeowner.RecrVehiclesIndex" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RecrVehiclesIndex</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<!--<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">-->
		<SCRIPT src="/cms/cmsweb/scripts/form.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
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
			
	
			//=============================================================================================
		function SetTabs(pretab,loadPage)
		{
			if (strSelectedRecordXML != "")		//If record xml exists then adding the tabs
			{
				var CalledFrom	= '<%=strCalledFrom%>';	//Stores the refrence , where the form is called from	
				var pageFrom = "HREC";			
	            var objXmlHandler = new XMLHandler();
				var tree = objXmlHandler.quickParseXML(strSelectedRecordXML);	
							
				if (tree != null)
				{
					//Fetching the values from xml to be passed in url as query string					
					var strPolId			= tree.getElementsByTagName('POLICY_ID')[0].firstChild.text;
					var strPolVerId			= tree.getElementsByTagName('POLICY_VERSION_ID')[0].firstChild.text;
					var strCustomerId		= tree.getElementsByTagName('CUSTOMER_ID')[0].firstChild.text;
					var strRecrVehId		= tree.getElementsByTagName('REC_VEH_ID')[0].firstChild.text;								
					
					if(CalledFrom=='Home')
					{		
						Url="../../aspx/Automobile/PolicyAdditionalInterestIndex.aspx?PageFrom=HREC&CalledFrom=HOME&CUSTOMER_ID="+strCustomerId+"&POLICY_ID="+strPolId+"&POLICY_VERSION_ID="+strPolVerId+"&RISK_ID="+strRecrVehId+"&";
						DrawTab(2,this,"Additional Interest",Url,null,pretab,! loadPage); 
					}
					
					return;
				}
			}
			//Record not exists , hence only one tab should come
			//Hence removing other tabs if exists
			RemoveTab(2,this);	
			changeTab(0,0);
		}
		//========================================================================================================
				
		
		</script>
	</HEAD>
	<body leftmargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();"
		MS_POSITIONING="GridLayout">		
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<form id="indexForm" method="post" runat="server">
				<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td class="midcolorc" align="right">
							<asp:label id="lblMessage" runat="server" CssClass="errmsg"></asp:label></td>
					</tr>
					<tr>
						<td>
							<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<tr>
									<td id="tdWorkflow" class="pageHeader" colspan="4">
										<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
									</td>
								</tr>
								<tr>
									<TD id="tdClientTop" class="pageHeader" colSpan="4">
										<webcontrol:ClientTop id="cltClientTop" runat="server" width="98%"></webcontrol:ClientTop>
									</TD>
								</tr>
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
												<td id="tabCtlRow">
													<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="AddRecrVehicle.aspx?" TabTitles="RV Information"
														TabLength="140"></webcontrol:Tab>
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
							<input type="hidden" name="hidEntityType" id="hidEntityType" runat="server"> <input type="hidden" name="hidEntityId" id="hidEntityId" runat="server">
							<INPUT id="hidKeyValues" type="hidden" runat="server" NAME="hidKeyValues">
						</td>
					</tr>
				</table>
			</form>
		</div>
	</body>
</HTML>





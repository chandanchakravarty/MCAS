<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Page language="c#" Codebehind="PolicyWatercraftIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Watercrafts.PolicyWatercraftIndex" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PriorLossIndex</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<SCRIPT src="/cms/cmsweb/scripts/menubar.js"></SCRIPT>
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
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
		
		/*Sets the different tabs urls of tab control*/
		/*Pretab contains the selcted tab
		loadPage contains whether to load page again or not*/
		function SetTabs(pretab,loadPage)
		{
			if (strSelectedRecordXML == "-1")
				return;
			
			if (strSelectedRecordXML != "" )		//If record xml exists then adding the tabs
			{
				var CalledFrom	= '<%=strCalledFrom%>';	//Stores the refrence , where the form is called from				
				var objXmlHandler = new XMLHandler();
				var tree = objXmlHandler.quickParseXML(strSelectedRecordXML);				
				if (tree != null)
				{
					//Fetching the values from xml to be passed in url as query string					
					var strAppId			//= tree.getElementsByTagName('APP_ID')[0].firstChild.text;
					var strAppVerId			//= tree.getElementsByTagName('APP_VERSION_ID')[0].firstChild.text;
					var strCustomerId		= tree.getElementsByTagName('CUSTOMER_ID')[0].firstChild.text;
					var strBoatID			= tree.getElementsByTagName('BOAT_ID')[0].firstChild.text;			
					
					var strTypeOfWatercraft;
					if(!(tree.getElementsByTagName('TYPE_OF_WATERCRAFT')[0]))
						strTypeOfWatercraft="";
					else						
						strTypeOfWatercraft	=	tree.getElementsByTagName('TYPE_OF_WATERCRAFT')[0].firstChild.text
						
						
					
					//var strTypeOfWatercraft = "";//tree.getElementsByTagName('TYPE_OF_WATERCRAFT')[0].firstChild.text;
					
					var lobStr					= "";
					
					if (CalledFrom == "WAT")
					{
						lobStr = "WWAT";
					}
					else if (CalledFrom == "HOME")
					{
						lobStr = "HWAT";
					}
					else if (CalledFrom == "UMB")
					{
						lobStr = "UWAT";
					}
					else if (CalledFrom == "RENT")
					{
						lobStr = "RWAT";
					}	
					
						// Below Drawn tab does not appear incase of UMB : refer comments below
						Url = "PolicyWatercraftEngineIndex.aspx?PageFrom=" + lobStr +"&CalledFrom=" + CalledFrom + "&VehicleID=" + strBoatID + "&PageTitle=Outboard Engine Info" + "&"; 
						DrawTab(2,this,"Outboard Engine Info",Url,null,pretab,! loadPage);
						// This tab has been explicitly removed using RemoveTab() coz' it was 
						// giving problem if we try to remove it by deleting it's DrawTab() code. 
						RemoveTab(2,this);
						var NoOfTabs = arrMainTab.length / 4;
					
					//if(strTypeOfWatercraft == "11369")
					//if(document.getElementById('cmbTYPE_OF_WATERCRAFT').value=="11369")
					//11489>Bass boat (w/Motor)
					//11369>Outboard Boat
					//11487>Outboard (w/Motor)
					//11374>Pontoon (w/Motor)		
					//11672>Sailboat w/outboard
				
						
						if (CalledFrom !="UMB")
						{
							Url = "PolicyWatercraftEngineIndex.aspx?PageFrom=" + lobStr +"&CalledFrom=" + CalledFrom + "&VehicleID=" + strBoatID + "&PageTitle=Outboard Engine Info" + "&"; 
							DrawTab(2,this,"Outboard Engine Info",Url,null,pretab,! loadPage);
							var NoOfTabs = arrMainTab.length / 4;

							Url = "PolicyWatercraftCoverages.aspx?PageFrom=" + lobStr +"&CalledFrom=WAT&VehicleID=" + strBoatID + "&PageTitle=Coverages/Limits Info" + "&"; 
							DrawTab(3,this,"Coverages/Limits Info",Url,null,pretab,! loadPage); 
							
							Url = "../PolicyEndorsement.aspx?PageFrom=" + lobStr +"&CalledFrom=WAT&VehicleID=" + strBoatID + "&PageTitle=Limits/Endorsement Information" + "&"; 
							DrawTab(4,this,"Endorsements",Url,null,pretab,! loadPage); 						
						
							Url="../Automobile/PolicyAdditionalInterestIndex.aspx?PageFrom=" + lobStr +"&CalledFrom=WAT&CUSTOMER_ID="+strCustomerId+"&APP_ID="+strAppId+"&APP_VERSION_ID="+strAppVerId+"&RISK_ID="+strBoatID+"&BOAT_ID1="+strBoatID+"&"; 
							DrawTab(5,this,"Additional Interest",Url,null,pretab,! loadPage); 
						}
					return;
				}
			}
			//Record not exists , hence only one tab should come
			//Hence removing other tabs if exists
			RemoveTab(5,this);	
			RemoveTab(4,this);	
			RemoveTab(3,this);	
			RemoveTab(2,this);
			changeTab(0,0);
		}
		function CopySchRecords()
		{
			window.open('/cms/policies/aspx/PolCopyUmbSchRecords.aspx?CalledFrom=UMB&CalledFor=BOAT&CUSTOMER_ID=<%=customerID.ToString()%>&POLICY_ID=<%=policyID.ToString()%>&POLICY_VERSION_ID=<%=policyVersionID.ToString()%>' ,'Copy',600,300,'Yes','Yes','No','No','No');	
		}
		
		function FetchLossReport()
		{
			window.open('/Cms/policies/Aspx/Homeowner/PolHomeLossReport.aspx?&CalledFrom=<%=strCalledFrom%>',null,"width=600,height=600,scrollbars=1,menubar=0,resizable=0");
		}

		function PriorLossTab()
		{
			//window.open('/cms/Application/priorloss/priorlossindex.aspx',null,"width=1000,height=1000,scrollbars=1,menubar=0,resizable=1");
			window.open('/cms/Application/priorloss/priorlossindex.aspx',null,"width=1000,height=1000,scrollbars=0,menubar=0,resizable=0");
		}

		</script>
	</HEAD>
	<body onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();" MS_POSITIONING="GridLayout">
		
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<form id="indexForm" method="post" runat="server">
				<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td>
							<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
								<tr>
									<td align="center">
										<asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label>
									</td>
								</tr>
								<tr>
									<td id="tdWorkflow" class="pageHeader" colspan="4">
										<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
									</td>
								</tr>
								<tr>
									<TD id="tdClientTop" class="pageHeader" colSpan="4">
										<webcontrol:ClientTop id="cltClientTop" runat="server" width="100%"></webcontrol:ClientTop>
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
										<table class="tableWidthHeader" width="100%" cellSpacing="0" cellPadding="0" border="0"
											align="center">
											<tr id="tabCtlRow">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="PolicyAddWatercraftInformation.aspx?" TabTitles="Watercraft Rating Info"
														TabLength="175"></webcontrol:Tab>
												</td>
											</tr>
											<tr>
												<td>
													<table class="tableWidthHeader" width="100%" cellpadding="0" cellspacing="0" border="0">
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

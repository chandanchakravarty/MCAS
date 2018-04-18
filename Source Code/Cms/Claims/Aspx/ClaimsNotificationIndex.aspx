<%@ Register TagPrefix="webcontrol" TagName="ClaimTop" Src="/cms/cmsweb/webcontrols/ClaimTop.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Page language="c#" Codebehind="ClaimsNotificationIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.ClaimsNotificationIndex" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ClaimsNotificationIndex</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<!-- include these scripts in page where you are using windows grid -->
		<SCRIPT src="/cms/cmsweb/scripts/common.js"></SCRIPT>
		<SCRIPT src="/cms/cmsweb/scripts/form.js"></SCRIPT>
		<STYLE>
		.hide { OVERFLOW: hidden; TOP: 5px }
		.show { OVERFLOW: hidden; TOP: 5px }
		#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">
		
		/*function SetTabs(pretab,loadPage)
		{
			if (strSelectedRecordXML != "")		//If record xml exists then adding the tabs
			{
				
				var objXmlHandler = new XMLHandler();
				var tree = objXmlHandler.quickParseXML(strSelectedRecordXML);				
				
				if (tree != null)
				{
					//Fetching the values from xml to be passed in url as query string					
					if(typeof(tree.getElementsByTagName('POLICY_ID')[0])!='undefined')
						var strPolicyId			= tree.getElementsByTagName('POLICY_ID')[0].firstChild.text;
					if(typeof(tree.getElementsByTagName('POLICY_VERSION_ID')[0])!='undefined')
						var strPolicyVersionId	= tree.getElementsByTagName('POLICY_VERSION_ID')[0].firstChild.text;
					if(typeof(tree.getElementsByTagName('CUSTOMER_ID')[0])!='undefined')
						var strCustomerId		= tree.getElementsByTagName('CUSTOMER_ID')[0].firstChild.text;
					
					var strLOBCode="",strClaimID="";
					if(typeof(tree.getElementsByTagName('LOB_ID')[0])!='undefined')
						strLOBCode			= tree.getElementsByTagName('LOB_ID')[0].firstChild.text;	
					if(typeof(tree.getElementsByTagName('CLAIM_ID')[0])!='undefined')
						strClaimID			= tree.getElementsByTagName('CLAIM_ID')[0].firstChild.text;								
					
					
					Url="AddOccurrenceDetails.aspx?&";
					DrawTab(2,this,"Occurrence Details",Url,null,pretab,! loadPage); 					
					
					//LOB: Automobile/Motor/Umbrella
					if(strLOBCode=='<%=enumLOB.AUTOP%>' || strLOBCode=='<%=enumLOB.CYCL%>')
					{
										
						
						Url="AddClaimsNotification.aspx";
						DrawTab(3,this,"Property Damaged",Url,null,pretab,! loadPage); 		
						
						if(document.getElementById("hidAttachParameters").value=="1")
						{
							Url="OwnerDetailsIndex.aspx?TYPE_OF_OWNER=<%=((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString()%>&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyId + "&POLICY_VERSION_ID=" + strPolicyVersionId + "&";
							DrawTab(4,this,"Owner Details",Url,null,pretab,! loadPage); 										
						
							Url="DriverDetailsIndex.aspx?TYPE_OF_DRIVER=<%=((int)enumTYPE_OF_DRIVER.INSURED_VEHICLE).ToString()%>&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyId + "&POLICY_VERSION_ID=" + strPolicyVersionId + "&";
							DrawTab(5,this,"Driver Details",Url,null,pretab,! loadPage); 						
						}
						else
						{
							Url="OwnerDetailsIndex.aspx?TYPE_OF_OWNER=<%=((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString()%>&";
							DrawTab(4,this,"Owner Details",Url,null,pretab,! loadPage); 				
							
							Url="DriverDetailsIndex.aspx?TYPE_OF_DRIVER=<%=((int)enumTYPE_OF_DRIVER.INSURED_VEHICLE).ToString()%>&";
							DrawTab(5,this,"Driver Details",Url,null,pretab,! loadPage); 									
						}
						
						Url="InsuredVehicleIndex.aspx?&";
						DrawTab(6,this,"Insured Vehicle",Url,null,pretab,! loadPage); 	
					}
					else if(strLOBCode=='<%=enumLOB.HOME%>' || strLOBCode=='<%=enumLOB.REDW%>')
					{
						Url="InsuredLocationIndex.aspx?";
						DrawTab(3,this,"Insured Location",Url,null,pretab,! loadPage); 
						
						if(document.getElementById("hidAttachParameters").value=="1")						
							Url="OwnerDetailsIndex.aspx?TYPE_OF_OWNER=<%=((int)enumTYPE_OF_OWNER.PROPERTY_DAMAGED).ToString()%>&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyId + "&POLICY_VERSION_ID=" + strPolicyVersionId + "&";
						else
							Url="OwnerDetailsIndex.aspx?TYPE_OF_OWNER=<%=((int)enumTYPE_OF_OWNER.PROPERTY_DAMAGED).ToString()%>&";// POLICY_VERSION_ID=" + strPolicyVersionId + "&";
						DrawTab(4,this,"Owner Details",Url,null,pretab,! loadPage); 						
					}
					else if(strLOBCode=='<%=enumLOB.UMB%>')
					{
										
						
						Url="InsuredLocationIndex.aspx?CLAIM_ID=" + strClaimID + "&";
						DrawTab(3,this,"Insured Location",Url,null,pretab,! loadPage); 		
						
						Url="AddClaimsNotification.aspx";
						DrawTab(4,this,"Property Damaged",Url,null,pretab,! loadPage); 										
						
						if(document.getElementById("hidAttachParameters").value=="1")							
							Url="OwnerDetailsIndex.aspx?TYPE_OF_OWNER=<%=((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString()%>&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyId + "&POLICY_VERSION_ID=" + strPolicyVersionId + "&";						
						else
							Url="OwnerDetailsIndex.aspx?TYPE_OF_OWNER=<%=((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString()%>&";
						DrawTab(5,this,"Owner Details",Url,null,pretab,! loadPage); 					
						
						Url="InsuredVehicleIndex.aspx?&";
						DrawTab(6,this,"Insured Vehicle",Url,null,pretab,! loadPage); 	
					}
					else if(strLOBCode=='<%=enumLOB.BOAT%>')
					{
						Url="AddClaimsNotification.aspx";
						DrawTab(3,this,"Insured Items",Url,null,pretab,! loadPage); 					
					}
					else if(strLOBCode=='<%=enumLOB.GENL%>')
					{
						Url="AddClaimsNotification.aspx";
						DrawTab(3,this,"Liability Type",Url,null,pretab,! loadPage); 
						
						if(document.getElementById("hidAttachParameters").value=="1")
						{
							Url="OwnerDetailsIndex.aspx?TYPE_OF_OWNER=<%=((int)enumTYPE_OF_OWNER.LIABILITY_TYPE_OWNER).ToString()%>&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyId + "&POLICY_VERSION_ID=" + strPolicyVersionId + "&";
							DrawTab(4,this,"Owner Details",Url,null,pretab,! loadPage); 	
							
							Url="OwnerDetailsIndex.aspx?TYPE_OF_OWNER=<%=((int)enumTYPE_OF_OWNER.LIABILITY_TYPE_MANUFACTURER).ToString()%>&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolicyId + "&POLICY_VERSION_ID=" + strPolicyVersionId + "&";
							DrawTab(5,this,"Manufacturer Details",Url,null,pretab,! loadPage); 							
						}
						else
						{
							Url="OwnerDetailsIndex.aspx?TYPE_OF_OWNER=<%=((int)enumTYPE_OF_OWNER.LIABILITY_TYPE_OWNER).ToString()%>&";
							DrawTab(4,this,"Owner Details",Url,null,pretab,! loadPage); 	
						
							Url="OwnerDetailsIndex.aspx?TYPE_OF_OWNER=<%=((int)enumTYPE_OF_OWNER.LIABILITY_TYPE_MANUFACTURER).ToString()%>&";
							DrawTab(5,this,"Manufacturer Details",Url,null,pretab,! loadPage); 							
						}
					}
					else
					{
						RemoveTab(6,this);	
						RemoveTab(5,this);	
						RemoveTab(4,this);	
						RemoveTab(3,this);	
					}
					return;
				}
			}
			//Record not exists , hence only one tab should come
			//Hence removing other tabs if exists			
			RemoveTab(6,this);	
			RemoveTab(5,this);	
			RemoveTab(4,this);	
			RemoveTab(3,this);	
			RemoveTab(2,this);	
			changeTab(0,0);
		}*/
	
		
		function onRowClicked(num,msDg )
		{
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);		
			//changeTab(0, 0);
			if ( !(event.srcElement.outerHTML.indexOf("OPTION") != -1 || event.srcElement.outerHTML.indexOf("button_Click") != -1))
			{
				top.botframe.location.href = "/cms/claims/aspx/ClaimsTab.aspx?" + locQueryStr + "&" ;
			}
		}
		function addNewClaim()
		{
			//document.location.href = "/cms/claims/aspx/ClaimsNotificationIndex.aspx?NEW_RECORD=1&";			
			document.location.href = "/cms/claims/aspx/ClaimsTab.aspx?&CUSTOMER_ID="+ document.getElementById("hidCUSTOMER_ID").value +  "&POLICY_ID=" + document.getElementById("hidPOLICY_ID").value + "&POLICY_VERSION_ID=" + document.getElementById("hidPOLICY_VERSION_ID").value + "&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&LOB_ID=" + document.getElementById("hidLOB_ID").value + "&NEW_RECORD=1";
			
		}		
		function TabSet()
		{
			if(document.getElementById("hidNEW_RECORD").value=="1")
				setTimeout('addRecord()',1000);
		}
		function SetMenu()
		{
			//Enable customer and policy menus at this page if they are available
			if(document.getElementById("hidCUSTOMER_ID").value!="" && document.getElementById("hidCUSTOMER_ID").value!="0")
			{
				top.topframe.enableMenu('2,2,5');
				top.topframe.enableMenu('2,2,6');
				top.topframe.enableMenu('2,2,7');				
			}
			else
			{
				//Disable customer and policy menus at this page
				top.topframe.disableMenu('2,2,5');
				top.topframe.disableMenu('2,2,6');
				top.topframe.disableMenu('2,2,7');
			}
			//Disable party,reserve and activities menu
			top.topframe.disableMenu('2,2,1');
			top.topframe.disableMenu('2,2,2');
			top.topframe.disableMenu('2,2,3');
			//Enable/Disable claim id at this page based on value of claim id
			/*if(document.getElementById("hidCLAIM_ID").value!="" && document.getElementById("hidCLAIM_ID").value!="0")
			{
				//Enable party menu at this page
				top.topframe.enableMenu('2,2,1');
			}
			else
			{
				//Disable party menu at this page
				top.topframe.disableMenu('2,2,1');				
			}*/
		}
		
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();TabSet();SetMenu();"
		MS_POSITIONING="GridLayout">
		
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
							<tr>
									<td id="tdWorkflow" class="pageHeader" colspan="4">
										<webcontrol:ClaimTop id="cltClaimTop" runat="server"></webcontrol:ClaimTop>
									</td>
								</tr>
							<form id="indexForm" method="post" runat="server">
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<p align="center"><asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label></p>
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
											<tr id="tabCtlRow" runat="server">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server" TabLength="150"></webcontrol:Tab>
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
						<input type="hidden" name="hidAttachParameters" runat="server" id="hidAttachParameters">
						<input type="hidden" name="hidCUSTOMER_ID" runat="server" id="hidCUSTOMER_ID"> <input type="hidden" name="hidPOLICY_ID" runat="server" id="hidPOLICY_ID">
						<input type="hidden" name="hidPOLICY_VERSION_ID" runat="server" id="hidPOLICY_VERSION_ID">
						<input type="hidden" name="hidLOB_ID" runat="server" id="hidLOB_ID"> <input type="hidden" name="hidCLAIM_ID" runat="server" id="hidCLAIM_ID">
						<input type="hidden" name="hidNEW_RECORD" runat="server" id="hidNEW_RECORD"> <input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr">
						<input type="hidden" name="hidMode"> </FORM>
					</td>
				</tr>
			</table>
		</div>
		<DIV>&nbsp;</DIV>
	</body>
</HTML>

<%@ Page language="c#" Codebehind="PolicyDwellingDetailsIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.aspx.HomeOwners.PolicyDwellingDetailsIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DwellingDetailsIndex</title>
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
			
			
			
			
		/*Sets the different tabs urls of tab control*/
		/*Pretab contains the selcted tab
		loadPage contains whether to load page again or not*/
		function SetTabs(pretab,loadPage)
		{
			//alert(document.getElementById('hidRemoveTab').value);
			if (strSelectedRecordXML != "" && document.getElementById('hidRemoveTab').value != 1)		//If record xml exists then adding the tabs
			{
				
				var objXmlHandler = new XMLHandler();
				var tree = objXmlHandler.quickParseXML(strSelectedRecordXML);
				//alert(strSelectedRecordXML)
				var CalledFrom	= document.getElementById("hidCalledFrom").value;	//Stores the refrence , where the form is called from
				//alert(CalledFrom);
			
				if (tree != null)
				{
					//Fetching the values from xml to be passed in url as query string
					var strPolId		= tree.getElementsByTagName('POLICY_ID')[0].firstChild.text;
					var strPolVerId		= tree.getElementsByTagName('POLICY_VERSION_ID')[0].firstChild.text;
					var strCustomerId	= tree.getElementsByTagName('CUSTOMER_ID')[0].firstChild.text;
					var strDwellingID	= tree.getElementsByTagName('DWELLING_ID')[0].firstChild.text;
				
					
					Url="PolicyOtherStructureIndex.aspx?CustomerID=" + strCustomerId + "&POLICY_ID=" + strPolId+ "&POLICY_VERSION_ID=" + strPolVerId + "&DWELLINGID=" + strDwellingID + "&CalledFrom=" + document.indexForm.hidCalledFrom.value + "&";
					DrawTab(2,this,'Other Struc. Detail',Url,null,pretab,! loadPage); 
					
					Url="PolicyAddHomeRating.aspx?CustomerID=" + strCustomerId + "&POLICY_ID=" + strPolId+ "&POLICY_VERSION_ID=" + strPolVerId + "&DWELLINGID=" + strDwellingID + "&CalledFrom=" + document.indexForm.hidCalledFrom.value + "&";
					DrawTab(3,this,'Rating Info',Url,null,pretab,! loadPage); 
					
					Url="../Automobile/PolicyAdditionalInterestIndex.aspx?Customer_ID=" + strCustomerId + "&PolID=" + strPolId + "&Pol_Version_ID=" + strPolVerId + "&RISK_ID=" + strDwellingID  + "&DWELLINGID=" + strDwellingID  + "&CalledFrom=" + document.indexForm.hidCalledFrom.value + "&";
					DrawTab(4,this,'Add. Interest',Url,null,pretab,! loadPage); 
			
					if (document.getElementById("hidCalledFrom").value == 'Home')
					{										
	
						Url="PolicyCoverages_Section1.aspx?CustomerID=" + strCustomerId + "&PolID=" + strPolId + "&Pol_Version_ID=" + strPolVerId + "&DWELLINGID=" + strDwellingID  + "&CalledFrom=" + document.indexForm.hidCalledFrom.value + "&";
						DrawTab(5,this,'Covg Sec 1',Url,null,pretab,! loadPage); 
				
						Url="PolOtherLocationsIndex.aspx?CustomerID=" + strCustomerId + "&PolID=" + strPolId + "&Pol_Version_ID=" + strPolVerId + "&DWELLINGID=" + strDwellingID  + "&CalledFrom=" + document.indexForm.hidCalledFrom.value + "&";
						DrawTab(6,this,'Other Loc/Liability',Url,null,pretab,! loadPage); 
						
						Url="PolicyCoverages_Section2.aspx?CustomerID=" + strCustomerId + "&PolID=" + strPolId + "&Pol_Version_ID=" + strPolVerId+ "&DWELLINGID=" + strDwellingID  + "&CalledFrom=" + document.indexForm.hidCalledFrom.value + "&";
						DrawTab(7,this,'Covg Sec 2',Url,null,pretab,! loadPage);

						

						Url="PolicyHomeOwnerEndorsements.aspx?CustomerID=" + strCustomerId + "&PolID=" + strPolId+ "&Pol_Version_ID=" + strPolVerId + "&DWELLINGID=" + strDwellingID  + "&CalledFrom=" + document.indexForm.hidCalledFrom.value + "&";
						DrawTab(8, this, 'Endorsement', Url, null, pretab, !loadPage);

						//Added By Kuldeep for FIRe LOB on 11 - feb -2012
						Url = "PolicyAddAccumulationDetails.aspx?CustomerID=" + strCustomerId + "&PolID=" + strPolId + "&Pol_Version_ID=" + strPolVerId + "&DWELLINGID=" + strDwellingID + "&CalledFrom=" + document.indexForm.hidCalledFrom.value + "&";
						DrawTab(9, this, 'Accumulation', Url, null, pretab, !loadPage); 
					}					
					else
					{
						//Rental Coverages 
						Url = "PolicyRentalCoverages.aspx?CustomerID=" + strCustomerId + "&PolID=" + strPolId+ "&Pol_Version_ID=" + strPolVerId + "&DWELLINGID=" + strDwellingID  + "&CalledFrom=" + document.indexForm.hidCalledFrom.value + "&";
						DrawTab(5,this,'Section I & II Coverages',Url,null,pretab,! loadPage); 
										
						Url="PolicyHomeOwnerEndorsements.aspx?CustomerID=" + strCustomerId + "&PolID=" + strPolId+ "&Pol_Version_ID=" + strPolVerId + "&DWELLINGID=" + strDwellingID  + "&CalledFrom=" + document.indexForm.hidCalledFrom.value + "&";
						DrawTab(6,this,'Endorsement',Url,null,pretab,! loadPage); 
						
					}
				}
			}
			else
			{
				RemoveTab(9,this);
				RemoveTab(8,this);
				RemoveTab(7,this);
				RemoveTab(6,this);
				RemoveTab(5,this);
				RemoveTab(4,this);
				RemoveTab(3,this);
				RemoveTab(2,this);
				
				if (loadPage)
					changeTab(0,0);
			}
		}
		
		</script>
	</HEAD>
	<body oncontextmenu="return false;" leftmargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();"
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
							<table class ="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<tr>
									<td id="tdWorkflow" class="pageHeader" colspan="4">
										<webcontrol:WorkFlow   id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
									</td>
								</tr>
								<tr>
									<TD id = "tdClientTop" class="pageHeader" colSpan="4">
										<webcontrol:ClientTop id="cltClientTop" runat="server" width ="98%" ></webcontrol:ClientTop>
									</TD>
								</tr>
								<tr>
									<td>
										<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
									</td>
								</tr>
								<tr><td><webcontrol:GridSpacer id="Gridspacer" runat="server"></webcontrol:GridSpacer></td></tr>
								<tr>
									<td>
										<table class ="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
											<tr id="tabCtlRow">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="PolicyAddDwellingDetails.aspx?" TabTitles="Property Info"
														></webcontrol:Tab>
												</td>
											</tr>
											<tr>
												<td>
													<table class="tableeffect"  cellpadding="0" cellspacing="0" border="0">
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
							<INPUT id="hidCalledFrom" type="hidden" runat="server" NAME="hidCalledFrom">
							<INPUT id="hidKeyValues" type="hidden" runat="server" NAME="hidKeyValues">
							<INPUT id="hidRemoveTab" type="hidden" runat="server" NAME="hidRemoveTab">
						</td>
					</tr>
				</table>
				
				
			</form>
		</div>
		
		<script language="javascript">
				if ( document.indexForm.hidKeyValues.value != '')
				{
					refreshGrid(1,1);
				}
		</script>
		
	</body>
</HTML>

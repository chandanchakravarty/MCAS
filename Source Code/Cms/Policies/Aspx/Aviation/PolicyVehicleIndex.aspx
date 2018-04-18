<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="PolicyVehicleIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Aviation.PolicyVehicleIndex" %>
<HTML>
	<HEAD>
		<title>Policy Vehicle Index</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
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
		}
		function SetTabs(pretab,loadPage)
		{
			if (strSelectedRecordXML != "")		//If record xml exists then adding the tabs
			{
				var CalledFrom	= document.getElementById("hidCalledFrom").value;	//Stores the refrence , where the form is called from
				var objXmlHandler = new XMLHandler();
				var tree = objXmlHandler.quickParseXML(strSelectedRecordXML);
				if (tree != null)
				{
					//Fetching the values from xml to be passed in url as query string
					var strAppId		= tree.getElementsByTagName('POLICY_ID')[0].firstChild.text;
					var strAppVerId		= tree.getElementsByTagName('POLICY_VERSION_ID')[0].firstChild.text;
					var strCustomerId	= tree.getElementsByTagName('CUSTOMER_ID')[0].firstChild.text;
					var strVehicleID	= tree.getElementsByTagName('VEHICLE_ID')[0].firstChild.text;
					var strLobID		= tree.getElementsByTagName('POLICY_LOB')[0].firstChild.text;
					
						var TabTitle = "";
						var TabCounter = 2;
						//else caption of tab should be Vehicle Covg Info
						TabTitle = "Cover Type Details";
						
						Url="../../../Policies/aspx/aviation/PolicyVehicleCoverageDetails.aspx?CalledFrom=" + CalledFrom + "&pageTitle=Vehicle Coverages" + "&VEHICLEID=" + strVehicleID + "&LOB_ID=" + strLobID + "&" ; 
						DrawTab(TabCounter++,this,TabTitle,Url,null,pretab,! loadPage); 
						/*
						Url="../../application/aspx/Endorsements.aspx?CalledFrom=" + CalledFrom + "&pageTitle=Vehicle Coverages" + "&VEHICLEID=" + strVehicleID + "&LOB_ID=" + strLobID + "&" ; 
						DrawTab(TabCounter++,this,'Endorsements',Url,null,pretab,! loadPage); 
						
						Url="../Aspx/AdditionalInterestIndex.aspx?CalledFrom=" + CalledFrom + "&CUSTOMER_ID=" + strCustomerId + "&APP_ID=" + strAppId + "&APP_VERSION_ID=" + strAppVerId + "&VEHICLE_ID=" + strVehicleID + "&RISK_ID=" + strVehicleID + "&"; 
						DrawTab(TabCounter++,this,'Additional Interest',Url,null,pretab,! loadPage); 
						*/
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
		</script>
	</HEAD>
	<body oncontextmenu="return false;" leftmargin="0" topmargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();"
		MS_POSITIONING="GridLayout">
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
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
										<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
											<tr id="tabCtlRow">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server" TabTitles="Vehicle Info" TabLength="175"></webcontrol:Tab>
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
							<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span><INPUT id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
							<INPUT id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server"> <INPUT id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server">
							<INPUT id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server">
						</td>
					</tr>
				</table>
			</form>
		</div>
		<DIV></DIV>
	</body>
</HTML>

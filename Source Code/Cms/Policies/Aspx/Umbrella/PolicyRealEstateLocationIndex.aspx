<%@ Page CodeBehind="PolicyRealEstateLocationIndex.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Umbrella.PolicyRealExtateLocationIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>UmbrellaFarmIndex</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
		
		 function SetTabs(pretab,loadPage)
	   	 {
	   	
	   
			if (strSelectedRecordXML != "")		
			{
				var objXmlHandler = new XMLHandler();
				var tree = objXmlHandler.quickParseXML(strSelectedRecordXML);				
				if (tree != null)
				{
					
					var strPolicyId			= tree.getElementsByTagName('POLICY_ID')[0].firstChild.text;
					var strPolicyVerId		= tree.getElementsByTagName('POLICY_VERSION_ID')[0].firstChild.text;
					var strCustomerId		= tree.getElementsByTagName('CUSTOMER_ID')[0].firstChild.text;
					var strLocation_ID		= tree.getElementsByTagName('LOCATION_ID')[0].firstChild.text;
					
					
					/*Url="/cms/policies/aspx/umbrella/PolicyLocationRemarks.aspx?CUSTOMER_ID="+strCustomerId 
						+ "&POLICY_ID=" + strPolicyId
						+ "&POLICY_VERSION_ID=" + strPolicyVerId
						+ "&LOCATION_ID1=" + strLocation_ID
						+  "&";
					
					DrawTab(2,this,"Remarks",Url,null,pretab,! loadPage); 
					
					
					Url="/cms/policies/aspx/umbrella/PolicyDwellingInfo.aspx?CUSTOMER_ID="+strCustomerId 
						+ "&POLICY_ID=" + strPolicyId
						+ "&POLICY_VERSION_ID=" + strPolicyVerId
						+ "&LOCATION_ID1=" + strLocation_ID
						+  "&";
					
					DrawTab(3,this,"Dwelling Info",Url,null,pretab,! loadPage); */
					
					Url="/cms/policies/aspx/umbrella/PolicyRatingInfo.aspx?CUSTOMER_ID="+strCustomerId 
						+ "&POLICY_ID=" + strPolicyId
						+ "&POLICY_VERSION_ID=" + strPolicyVerId
						+ "&LOCATION_ID1=" + strLocation_ID
						+  "&";
					
					DrawTab(2,this,"Rating Info",Url,null,pretab,! loadPage); 
					RemoveTab(2,this);	
								
			  	}
			  }
			else
			{
				//RemoveTab(4,this);	
				//RemoveTab(3,this);
				RemoveTab(2,this);
				if (loadPage)
					changeTab(0,0);
			}
			
		}
		function CopySchRecords()
		{
			window.open('/cms/policies/aspx/PolCopyUmbSchRecords.aspx?CalledFrom=UMB&CalledFor=LOCATIONS&CUSTOMER_ID=<%=strCustomerID%>&POLICY_ID=<%=strPolicyId%>&POLICY_VERSION_ID=<%=strPolicyVersionId%>' ,'Copy',600,300,'Yes','Yes','No','No','No');	
		}
		function onRowClicked(num,msDg )
		{
		
			rowNum = num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);	
						
		}
		</script>
	</HEAD>
	<body onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();SetTabs();"
		MS_POSITIONING="GridLayout">		
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
							<form id="POLICY_UM_LOCATION_INDEX" method="post" runat="server">
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<p align="center"><asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label></p>
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
									<td id="tdGridHolder">
										<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
									</td>
								</tr>
								<tr>
									<td>&nbsp;</td>
								</tr>
								<tr>
									<td>
										<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
											<tr id="tabCtlRow">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="PolicyAddRealEstateLocation.aspx?" TabTitles="Location Info"
														TabLength="125"></webcontrol:Tab>
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
	</body>
</HTML>



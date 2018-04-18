<%@ Page language="c#" Codebehind="ActivityExpenseIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.ActivityExpenseIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClaimTop" Src="/cms/cmsweb/webcontrols/ClaimTop.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Expense Activity Index</title>
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
	
		function SetTabs(pretab,loadPage)
		{
			if (strSelectedRecordXML != "")		//If record xml exists then adding the tabs
			{
				
				var objXmlHandler = new XMLHandler();
				var tree = objXmlHandler.quickParseXML(strSelectedRecordXML);				
				
				if (tree != null)
				{
					//Fetching the values from xml to be passed in url as query string					
					var strCLAIM_ID			= <%=ClaimID%>;
					var strACTIVITY_ID			= <%=ActivityID%>;
					
					if(typeof(tree.getElementsByTagName('EXPENSE_ID')[0])!='undefined')
					var strEXPENSE_ID			= tree.getElementsByTagName('EXPENSE_ID')[0].firstChild.text;
					
					//Expense Breakdown page has been removed.						
					//Url="ActivityExpenseBreakdownIndex.aspx?&CLAIM_ID=" + strCLAIM_ID + "&ACTIVITY_ID=" + strACTIVITY_ID + "&";
					//DrawTab(2,this,"Expense Breakdown",Url,null,pretab,! loadPage); 
					
					Url="PayeeIndex.aspx?&CLAIM_ID=" + strCLAIM_ID + "&ACTIVITY_ID=" + strACTIVITY_ID + "&CALLED_FROM=" + '<%=CALLED_FROM_EXPENSE%>' + "&";
					DrawTab(2,this,"Payee Details",Url,null,pretab,! loadPage); 			
					return;
				}
			}
			//Record not exists , hence only one tab should come
			//Hence removing other tabs if exists			
			//RemoveTab(3,this);	
			RemoveTab(2,this);				
			changeTab(0,0);
		}
		function GoBack(PageName)
		{
			strURL = PageName + "?&CLAIM_ID=<%=ClaimID%>&ACTIVITY_ID=<%=ActivityID%>&";
			this.document.location.href = strURL;
			return false;
		}	
		
		function onRowClicked(num,msDg )
		{
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);		
			changeTab(0, 0);
		}		
		
		</script>
	</HEAD>
	<body onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();" MS_POSITIONING="GridLayout">
		<DIV id="myTSMain" style="BEHAVIOR: url(/cms/cmsweb/htc/webservice.htc)"></DIV>
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
									<td id="tdWorkflow" class="pageHeader" colspan="4">
										<webcontrol:ClaimTop id="cltClaimTop" runat="server"></webcontrol:ClaimTop>
									</td>
								</tr>
								<tr>
									<td align="right" colSpan="4"><cmsb:cmsbutton class="clsButton" id="btnGoBack" runat="server" Text="Claim Activities"></cmsb:cmsbutton></td>
								</tr>
						
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
													<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="AddExpertServiceProviders.aspx?" TabTitles="Expert Service Providers Details"
														TabLength="250"></webcontrol:Tab>
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
						
					</td>
				</tr>
			</table>
			</FORM>
		</div>
		<DIV></DIV>
	</body>
</HTML>



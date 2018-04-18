<%@ Page language="c#" Codebehind="ClaimAdjustersIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Claims.ClaimAdjustersIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Claim Adjuster</title>
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
		
		function SetTabs_New(pretab,loadPage)
		{
			if (strSelectedRecordXML != "")		//If record xml exists then adding the tabs
			{
				setTimeout('',1000);
				var strADJUSTER_NAME="",strADJUSTER_TYPE="";
				var objXmlHandler = new XMLHandler();
				var tree = objXmlHandler.quickParseXML(strSelectedRecordXML);				
				
				if (tree != null)
				{
					//Fetching the values from xml to be passed in url as query string					
					//if(typeof(tree.getElementsByTagName('ADJUSTER_ID')[0])!='undefined')
					if(typeof(tree.getElementsByTagName('ADJUSTER_NAME')[0])!='undefined')
						strADJUSTER_NAME = tree.getElementsByTagName('ADJUSTER_NAME')[0].firstChild.text;
					if(typeof(tree.getElementsByTagName('ADJUSTER_TYPE')[0])!='undefined')
						strADJUSTER_TYPE= tree.getElementsByTagName('ADJUSTER_TYPE')[0].firstChild.text;
					if(strADJUSTER_TYPE == <%=((int)enumClaimAdjusterTypes.THIRD_PARTY_ADJUSTER).ToString()%>)
					{
						RemoveTab(2,this);
						return;
					}
					Url="ClaimsAdjusterAuthorityIndex.aspx?ADJUSTER_NAME=" + strADJUSTER_NAME + "&";
					
					DrawTab(2,this,"Authority",Url,null,pretab,! loadPage); 			
					return;
				}
			}
			//Record not exists , hence only one tab should come
			//Hence removing other tabs if exists			
			RemoveTab(2,this);	
			changeTab(0,0);
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
		function onRowClicked(num,msDg )
		{ 
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";			
			populateXML(num,msDg);		
			changeTab(0,0);
		}
		
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" leftmargin="0" topmargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();"
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
											<tr id="tabCtlRow">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server"  TabLength="150"></webcontrol:Tab>
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
							<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
						</td>
					</tr>
				</table>
			</form>
		</div>
		<DIV></DIV>
	</body>
</HTML>



<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="RuleCollection.aspx.cs" Inherits="Cms.CmsWeb.Maintenance.RuleCollection"  culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register assembly="CmsButton" namespace="Cms.CmsWeb.Controls" tagprefix="cmsb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Rule Collection</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
     <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
      <LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet" />      
    	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
	    <script type="text/javascript" src="/cms/cmsweb/scripts/Calendar.js"></script>
         <STYLE>
            .hide { OVERFLOW: hidden; TOP: 5px }
            .show { OVERFLOW: hidden; TOP: 5px }
            #tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>

     <script type="text/javascript" language="javascript">
     
         function initPage() {

             ApplyColor();
         }

         function onRowClicked(num, msDg) {
             rowNum = num;
             if (parseInt(num) == 0)
                 strXML = "";
             populateXML(num, msDg);
             changeTab(0, 0);
         }

         function findMouseIn() {
             if (!top.topframe.main1.mousein) {
                 //createActiveMenu();
                 top.topframe.main1.mousein = true;
             }
             setTimeout('findMouseIn()', 5000);
         }
		</script>
         <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 29px;
        }
    </style>
</head>
<body oncontextmenu = "return false;" leftmargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();"
		MS_POSITIONING="GridLayout">
        <webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
    <form id="form1" runat="server">
    <div id="bodyHeight" class="pageContent">
			<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<table  class ="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
							
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<tr>
									<td>
										<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
									</td>
								</tr>								
								<tr>
									<td>
										<table class ="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
											<tr id="tabCtlRow">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="AddRuleCollectionsDetail.aspx?" TabTitles="Add Rule Collections" TabLength="150"></webcontrol:Tab>
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
						
					</td>
				</tr>
			</table>
		</div>
        <input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
						<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
						<input type="hidden" name="hidEntityType" id="hidEntityType" runat="server"> <input type="hidden" name="hidEntityId" id="hidEntityId" runat="server">
    </form>
</body>
</html>

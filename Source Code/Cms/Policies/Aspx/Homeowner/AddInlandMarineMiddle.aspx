<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" Codebehind="AddInlandMarineMiddle.aspx.cs" AutoEventWireup="false" Inherits="Policies.Aspx.Homeowner.AddInlandMarineMiddle" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Add Inland Marine MIDDLE</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="../../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../../cmsweb/scripts/common.js"></script>
		<script src="../../../cmsweb/scripts/form.js"></script>
		<script>
				var onLoad = 0;
				
				/*function SetTabs(pretab,loadPage)
				{
					if (strSelectedRecordXML != "")		//If record xml exists then adding the tabs
					{
						var objXmlHandler = new XMLHandler();
						var tree = objXmlHandler.quickParseXML(strSelectedRecordXML);				
						if (tree != null)
						{
							//Fetching the values from xml to be passed in url as query string					
							var C_ID =	tree.getElementsByTagName('CUSTOMER_ID')[0].firstChild.text;
							var A_ID = 	tree.getElementsByTagName('POL_ID')[0].firstChild.text;
							var V_ID =	tree.getElementsByTagName('POL_VERSION_ID')[0].firstChild.text;
							var I_ID =	tree.getElementsByTagName('ITEM_ID')[0].firstChild.text;
							var D_ID =	tree.getElementsByTagName('ITEM_DETAIL_ID')[0].firstChild.text;
							var clFrom= document.Form1.hidCa
							
							var url = C_ID + "~" + A_ID + "~" + V_ID  + "~" +  I_ID  + "~" + D_ID; 
							//alert("update URL = " + url);
							document.cookie  = "PK_ITEM=" + url;
							DrawTab(0,top.frames[1],'','AddInlandMarineBOTTOM.aspx',null,pretab,true);																			
							//changeTab(0,0);								
							
						}
					}
					else
					{
						var url = "<%=GetCustomerID()%>~<%=GetPolicyID()%>~<%=GetPolicyVersionID()%>~<%=strItemID%>~0" 
						//alert("ADD URL = " + url);
						document.cookie  = "PK_ITEM=" + url;
						DrawTab(0,top.frames[1],'','AddInlandMarineBottom.aspx',null,pretab,true);																			
						//changeTab(0,0);	
					}
					
					RemoveTab(2,this);	
					if (onLoad == 1)
					{
						changeTab(0,0);
						onLoad = 0;
					}
				}*/
		
			function GetGridData(CovgID)
			{
			
			}
			
			
			function onRowClicked(num,msDg )
			{
				
				rowNum = num;
				rowNum=num;
				if(parseInt(num)==0)
					strXML="";
				populateXML(num,msDg);		
				changeTab(0,0);
			}
			
			function ShowItemDetails(PK)
			{
				document.cookie  = "PK_ITEM=" + PK;
				DrawTab(0,top.frames[1],'','AddInlandMarineBOTTOM.aspx');													
				changeTab(0,0);	
				
			}
			
			function Init()
			{
				onLoad = 1;
			}
		</script>
</HEAD>
	<body MS_POSITIONING="GridLayout" leftmargin="0" rightmargin="0" onload="setfirstTime();Init();">
		<div id="bodyHeight" class="pageContent">
			<form id="Form1" method="post" runat="server">
				<table cellSpacing="0" cellPadding="0" border="0" align="center" width="100%">
					<tr>
						<td class="headereffectCenter"><asp:label id="lblTitle" runat="server">&nbsp;</asp:label></td>
					</tr>
					<tr>
						<td>
							<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
						</td>
					</tr>
					<tr>
						<td><webcontrol:gridspacer id="Gridspacer2" runat="server"></webcontrol:gridspacer></td>
					</tr>
					<tr id="tabCtlRow">
						<td>
							<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="AddInlandMarineBOTTOM.aspx?" TabTitles="Sub Category Details"
								TabLength="150"></webcontrol:Tab>
						</td>
					</tr>
					<tr>
						<td>
							<table class="tableeffect" cellpadding="0" cellspacing="0" border="0" width="100%">
								<tr>
									<td>
										<iframe id="tabLayer" runat="server" src="" frameborder="0" width="100%" height="1000">
										</iframe>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<asp:Label ID="lblTemplate" Runat="server" Visible="false"></asp:Label>
				</table>
				<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr">
			</form>
		</div>
		<script>	
			document.getElementById("lblTitle").innerText= document.parentWindow.parent.global_COVG_DESC;			
			GetGridData(document.parentWindow.parent.global_COVG_ID);
		</script>
	</body>
</HTML>

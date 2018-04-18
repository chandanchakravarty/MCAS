<%@ Page language="c#" Codebehind="ProfitCenterWebIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.ProfitCenterWebIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProfitCenterWebIndex</title> 
		<!--******************************************************************************************
			<Author					: - >
			<Start Date				: -	>
			<End Date				: - >
			<Description			: - >
			<Review Date			: - >
			<Reviewed By			: - >
			
			Modification History

			<Modified Date			: - >
			<Modified By			: - >
			<Purpose				: - >
		*******************************************************************************************-->
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
		<STYLE>
		.hide { OVERFLOW: hidden; TOP: 5px }
		.show { OVERFLOW: hidden; TOP: 5px }
		#tabContent { LEFT: 0px; POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">
		var mainSearch="";
		var rowPrimary="";		
		var show=1;		
		var firstTime;
		var headRow=-1;
		var srhCrt="";
		var globSelected=1;
		/***********************************************************/
		
		
		/************************************************************/ 
		/////////////// FUNCTIONS TO BE INCLUDED IN USING PAGE///////		
		
		
		/************* TO BE CALLED ON ONLOAD EVENT OF BODY TAG **********************/
		
		
 		function setfirstTime()
		{
			//showScroll();
			firstTime=1;
			tabContent.style.display="none";
			
		
		}
		
		function onRowClicked(num,msDg )
		{
			//document.indexForm.hidRowID.value=num;
			if(parseInt(num)==0)
				strXML="";
			
			
			
					
			tabContent.style.display="inline";
			footertext.style.display="inline"; 
			changeTab(0, 0);
	
			//if(parseInt(num)!=0)
			//{
				populateXML(num,msDg);				
			//}								
		}
		
			
		function populateXML(num,msDg)
		{
			var tempXML;			
			var tmpTree="";
			var unqId=0;

			var arrAll=msDg.split("~");
			var arrColno=arrAll[0].split("^");
			var arrColname=arrAll[1].split("^");			
			var arrColSize=arrAll[3].split("^");		
			var arrPrimaryCols=sPc.split("^");				
			var i;				
			var rowID=parseInt(num);
				
			if(strXMLBase!="")
			{
				
				tempXML=strXMLBase;
				
				var objXmlHandler = new XMLHandler();
				var tree = (objXmlHandler.quickParseXML(tempXML).getElementsByTagName('temp')[rowID-1]);
				var str=new String();									
				var strSingleRow="";
				var fetch=fetchColumns.split("^");		
				var queryCol;
				
				
				if(tree)
				{				
					for(i=0;i<tree.childNodes.length;i++)
					{
						if(tree.childNodes[i].nodeName=="UniqueGrdId")
						{
							if(tree.childNodes[i].firstChild)
								unqId=tree.childNodes[i].firstChild.text;																											
						}
					}
					
					
					if(strRequire=="Y")
					{
						queryCol=QueryColumns.split("^");
						locQueryStr="";
						for(i=0;i<queryCol.length;i++)			
						{
							var col=queryCol[i];
							locQueryStr = locQueryStr +  col + "=";
							for(i=0;i<tree.childNodes.length;i++)
							{
								if(tree.childNodes[i].nodeName==col)
								{
									if(tree.childNodes[i].firstChild)
									{
										locQueryStr += tree.childNodes[i].firstChild.text + "&";
										break;
									}	
								}
							}
						}						
						
						if(locQueryStr!="")
						{
							locQueryStr = locQueryStr.substring(0,locQueryStr.length-1);
						}
					
					}
					else
					{		
						strXML="<NewDataSet><Table>";
						
						for(i=0;i<fetch.length;i++)			
						{
							var col=fetch[i];
							if(i<tree.childNodes.length)
							{
								if(tree.childNodes[col-1].firstChild)
								{
									strXML += "<" + tree.childNodes[col-1].nodeName + ">";
												
									//if(tree.childNodes[col-1].firstChild)
									//{						
									strXML += tree.childNodes[col-1].firstChild.text ;							
									//}			
									strXML += "</" + tree.childNodes[col-1].nodeName + ">";
								}
							}	
						
						}
						
						strXML+= "</Table>";
						strXML+= "</NewDataSet>";
					}	
				
					for(i=0;i<arrPrimaryCols.length;i++)
						rowPrimary += arrPrimaryCols[i] + "^" ;
				
					setmyuniqueId(unqId)
				}
			}		
		}		//-->
		
		
		/********************************************************************************************************/
		</script>
	</HEAD>
	<body  class="bodyBackGround" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();" MS_POSITIONING="GridLayout">
		<DIV id="myTSMain" style="BEHAVIOR: url(/cms/cmsweb/htc/webservice.htc)"></DIV>
		<DIV id="main1">
			<DIV class="menuBarBottom" style="HEIGHT: 18px"></DIV>
		</DIV>
		<div id="bodyHeight" class="pageContent">
			<!-- PANEL FOR USING WEB GRID CONTROL -->
			<div>
				<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td width='1002'>
							<form id="indexForm" method="post" runat="server">
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<asp:Panel id="pnlGrid" Runat="server" Width="100%">
									<DIV class="hide" id="gridpanel" style="WIDTH: 1002px; HEIGHT: 350px; left-margin: 0px">
										<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder></DIV>
								</asp:Panel>
								<!-- DIV FOR USING TAB CONTROL AND IFRAME-->
								<div id="tabContent" style="LEFT: 0px; WIDTH: 100%; HEIGHT: 324px">
									<table width="100%" cellSpacing="0" cellPadding="0" border="0" align="center">
										<tr>
											<td>
												<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="addprofitcenter.aspx?" TabTitles="Agency" TabLength="150"></webcontrol:Tab>
											</td>
										</tr>
										<tr>
											<td>
												<table class="tableeffectDiary" width="100%" cellpadding="0" cellspacing="0" border="0">
													<tr>
														<td>
															<iframe class="iframsHeightLong" id="tabLayer" runat="server" src="" scrolling="no" frameborder="0"
																width='1002'></iframe>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<asp:Label ID="lblTemplate" Runat="server" Visible="false"></asp:Label>
									</table>
								</div>
								<!-- DIV FOR USING FOOTER CONTROL -->
								<div id="footertext" class="hide" style="WIDTH: 100%;TOP: 250px;HEIGHT: 50px">
									<webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
								</div>
								<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
							</form>
						</td>
					</tr>
				</table>
			</div>
		</div>
	</body>
</HTML>

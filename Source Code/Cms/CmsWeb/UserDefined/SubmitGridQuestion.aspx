<%@ Page language="c#" Codebehind="SubmitGridQuestion.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.UserDefined.SubmitGridQuestion" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
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
				changeTab(0, 0);
			}
			
			function fnSubmitRules()
			{
				var lStrScreenID;				
				var strErrmsg;
				strErrmsg="Please select a question";
				
				var szTemplateID = locQueryStr;			
				var arrszTemplateID;
				var value;
				if(szTemplateID!="" || szTemplateID !="-1" )
				{
					arrszTemplateID=szTemplateID.split("=");
					if(arrszTemplateID.length>0)
					{
						value=arrszTemplateID[1];	
					}
				}
				if(szTemplateID=="" || szTemplateID=="-1")
				{
					alert(strErrmsg);
					return;	
				}
				else
				{
					window.location = "QuoteRules.aspx?QID="+value+"&TABID=" + document.SubmitQuestion.hidTabID.value + "&SCREENID=" + document.SubmitQuestion.hidTemplateID.value + "&GROUPID=" + document.SubmitQuestion.hidGroupID.value;
				}
				
			}
		
			function fnMovePrev()
			{
				var lStrGroup;
				var lStrBack;
				var lStrTab;
				var lStrCalledFrom;
				lStrCalledFrom=document.SubmitQuestion.hidCalledFrom.value;
				lStrBack=document.SubmitQuestion.hidTemplateID.value;				
				lStrGroupID=document.SubmitQuestion.hidGroupID.value;				
				lStrTab=document.SubmitQuestion.hidTabID.value;	
				var qid=document.SubmitQuestion.hidQid.value  ;	
				
				if(lStrGroupID!="" && lStrTab!="0")
				{			
					self.location.href="SubmitQuestion.aspx?CalledFrom="+lStrCalledFrom+"&TabID="+lStrTab+"&ScreenID="+lStrBack+"&groupid=";
				}
				else
				{
					self.location.href="SubmitTab.aspx?CalledFrom="+lStrCalledFrom+"&ScreenID="+lStrBack;
				}
				 
			}
		
			function fnPreviewQuestion()
			{
				var lStrGroup;
				var lStrBack;
				var lStrTab;
				lStrCalledFrom=document.SubmitQuestion.hidCalledFrom.value;
				lStrBack=document.SubmitQuestion.hidTemplateID.value;				
				lStrGroupID=document.SubmitQuestion.hidGroupID.value;				
				lStrTab=document.SubmitQuestion.hidTabID.value;
				
				var lStrGrdMsg="Please select a question";
				var szTemplateID = locQueryStr;			
				var arrszTemplateID;
				var value;
				if(szTemplateID!="" || szTemplateID !="-1" )
				{
					arrszTemplateID=szTemplateID.split("=");
					if(arrszTemplateID.length>0)
					{
						value=arrszTemplateID[1];	
					}
				}
				if(szTemplateID=="" ||szTemplateID=="-1")
				{
					alert(lStrGrdMsg);
					return;	
				}
				else
				{
					window.open("PreviewQuestion.aspx?CalledFrom="+lStrCalledFrom+"&QID="+value+"&ScreenID="+lStrBack+"&TabID="+lStrTab,'','width=500,height=250,menubars=NO,scrollbars=yes,top=225,left=250,statusbar=NO,toolbars=NO');
				}			
			}
		</script>
	</HEAD>
	<body leftmargin="0" topmargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();"
		MS_POSITIONING="GridLayout">
		<DIV id="myTSMain" style="BEHAVIOR: url(/cms/cmsweb/htc/webservice.htc)"></DIV>
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<form id="SubmitQuestion" method="post" runat="server">
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
									<td>&nbsp;</td>
								</tr>
								<tr>
									<td>
										<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
											<tr id="tabCtlRow">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="GridQuestion.aspx?" TabTitles="Question Grid Details"
														TabLength="180"></webcontrol:Tab>
												</td>
											</tr>
											<tr>
												<td>
													<table class="tableeffect" width="100%" cellpadding="0" cellspacing="0" border="0">
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
							<input type="hidden" name="hidTemplateID" id="hidTemplateID" runat="server"> <input type="hidden" name="hidRowID">
							<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
							<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span><input type="hidden" name="hidTabID" id="hidTabID" runat="server">
							<input type="hidden" name="hidGroupID" id="hidGroupID" runat="server"> <input type="hidden" name="hidCalledFrom" id="hidCalledFrom" runat="server">
							<input type="hidden" name="hidQid" id="hidQid" runat="server"> 
							
						</td>
					</tr>
				</table>
			</form>
		</div>
		<DIV></DIV>
	</body>
</HTML>

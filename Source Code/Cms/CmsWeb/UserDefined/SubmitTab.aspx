<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Page language="c#" Codebehind="SubmitTab.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.User_Defined.SubmitTab" %>
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
			
		
		function fnSubmitTab()
		{
			var lStrScreenID;
			lStrScreenID=document.SubmitTab.hidTemplateID.value;
			//alert("Screen ID is" + lStrScreenID);
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
			
			var CalledFrom ="";
			var lStrGrdMsg="Please select tab first";
			
			//alert("Tab ID" + szTemplateID);
			if(szTemplateID=="" || szTemplateID=="-1")
			{
				alert(lStrGrdMsg);
				return;	
			}
			else
			{
				window.location = "SubmitGroups.aspx?CalledFrom="+CalledFrom+"&TabID="+value+"&ScreenID="+lStrScreenID;
			}
			
		}
		function fnSubmitQues()
		{
			var lStrScreenID;
			lStrScreenID=document.SubmitTab.hidTemplateID.value;
			var CalledFrom ="";
			var lStrGrdMsg="Please select tab first";
			//alert(lstrScreenID);
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
			//alert(szTemplateID);
			if(szTemplateID=="" || szTemplateID=="-1")
			{
				alert(lStrGrdMsg);
				return;	
			}
			else
			{
				window.location = "SubmitQuestion.aspx?CalledFrom="+CalledFrom+"&TabID="+value+"&ScreenID="+lStrScreenID+"&GroupID=";
			}
			
		}
		function fnChangeQuesOrder()
		{
			var lStrScreenID;
			lStrScreenID=document.SubmitTab.hidTemplateID.value;
			var lStrGrdMsg="Please select tab first";
			var CalledFrom ="";
			//alert(lstrScreenID);
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
			//alert(szTemplateID);
			if(szTemplateID=="" || szTemplateID=="-1")
			{
				alert(lStrGrdMsg);
				return;	
			}
			else
			{
			 
				window.location = "QuestionSequenceChange.aspx?CalledFrom="+CalledFrom+"&"+szTemplateID+"&ScreenID="+lStrScreenID+"&GroupID=";
			}			
		}
		function fnMovePrev()
		{
			self.location.href="SubmitScreen.aspx";
		}
		function fnPreviewQuestion()
		{
			var lStrScreenID;
			lStrScreenID=document.SubmitTab.hidTemplateID.value;
			var CalledFrom ="";
			
			var lStrGrdMsg="Please select tab first";			
			
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
				alert(lStrGrdMsg);
				return;	
			}
			else
			{
			window.open("PreviewUserQuestion.aspx?CalledFrom="+CalledFrom+"&TabID="+value+"&ScreenID="+lStrScreenID,'','width=800,height=650,menubars=NO,scrollbars=yes,top=20,left=100,statusbar=NO,toolbars=NO');		
				
			//	window.location = "PreviewUserQuestionTab.aspx?ScreenID="+szTemplateID; //Calling the Preview Page with the selected ScreenID.
			}		
		}
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" leftmargin="0" topmargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();"
		MS_POSITIONING="GridLayout">
		<DIV id="myTSMain" style="BEHAVIOR: url(/cms/cmsweb/htc/webservice.htc)"></DIV>
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<form id="SubmitTab" method="post" runat="server">
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
													<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="TabDetails.aspx?" TabTitles="Tab Details"
														TabLength="150"></webcontrol:Tab>
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
							<input type="hidden" name="hidTemplateID" id="hidTemplateID" runat=server  > <input type="hidden" name="hidRowID">
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

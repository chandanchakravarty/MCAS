<%@ Page language="c#" Codebehind="QuickQuoteLoad.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.aspx.QuickQuoteLoad" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>QuickQuoteLoad</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<SCRIPT event="BackToApplication" for="QuickQuote">
			//self.location='quickquotelist.aspx';
			self.location='/cms/client/aspx/CustomerManagerIndex.aspx?customer_id=<%=strClientId%>';
		</SCRIPT>
		<SCRIPT event="ShowApplication" for="QuickQuote">
			self.location='/cms/Application/aspx/ApplicationTab.aspx?customer_id=<%=strClientId%>&app_id=' + document.QuickQuote.QuickQuoteAppId + '&app_version_id=' + document.QuickQuote.QuickQuoteAppVersionId;
		</SCRIPT>
		<SCRIPT event="ShowRatings" for="QuickQuote">
			//ShowPopup('QuickQuoteRatingReport.aspx?ClientId=<%=strClientId%>&QuoteId=<%=strQuoteId%>&Lob=<%=strType%>', 'Rating', '1200', '1200') 
			//myRef =window.open('QuickQuoteRatingReport.aspx?ClientId=<%=strClientId%>&QuoteId=<%=strType%>','Rating Report','height=1500, width=1500');
			window.open('QuickQuoteRatingReport.aspx?ClientId=<%=strClientId%>&QuoteId=<%=strQuoteId%>&LOB=<%=strType%>','mywin','left=215,top=0,width=600,height=700,scrollbars=1,toolbar=0,resizable=1');
			//window.open('QuickQuoteRatingReport.aspx?ClientId=<%=strClientId%>&QuoteId=<%=strQuoteId%>','Rating Report','height=500, width=500,status= no, resizable= yes, scrollbars=no, toolbar=no,location=no,menubar=no');
			//window.open('QuickQuoteRatingReport.aspx?ClientId=<%=strClientId%>&QuoteId=<%=strQuoteId%>');
		</SCRIPT>
		<SCRIPT event="ShowCustomerSearch" for="QuickQuote">
			self.location='/cms/client/aspx/CustomerManagerSearch.aspx';
		</SCRIPT>
		<SCRIPT event="RemoveImage" for="QuickQuote">
			ShowLoadingImage.style.display="none"
		</SCRIPT>
		<script language="javascript">
		function SetMenu()
		{
			top.topframe.enableMenu('1,4');
			top.topframe.enableMenu('1,5');
			top.topframe.enableMenu('1,6');
			top.topframe.enableMenu('1,1,1');
			top.topframe.enableMenu('1,1,2');
			//Enabling the details menu
			top.topframe.enableMenu('1,2');
			//Done for disabling of Risk Information Menu - Done for Itrack Issue 6141 on 23 July 2009
			top.topframe.disableMenu('1,3');
			top.topframe.disableMenu('1,3,0');
			if ('<%=GetAppID()%>' == '')
			{
				top.topframe.disableMenu('1,2,2');
				top.topframe.disableMenu('1,2,3');
			}
		}
	
			var isNS = (navigator.appName == "Netscape") ? 1 : 0; 
			if(navigator.appName == "Netscape") 
				document.captureEvents(Event.MOUSEDOWN||Event.MOUSEUP);  
			function mischandler(){   return false; }  
			function mousehandler(e){
 				var myevent = (isNS) ? e : event;
 				var eventbutton = (isNS) ? myevent.which : myevent.button;  
			if((eventbutton==2)||(eventbutton==3)) return false;
			}
			document.oncontextmenu = mischandler;
			document.onmousedown = mousehandler;
			document.onmouseup = mousehandler;
		
		</script>
	</HEAD>
	<!--<body MS_POSITIONING="GridLayout" onload="top.topframe.main1.mousein = false;findMouseIn();" border="1" cellPadding="0" cellSpacing="0" >-->
	<body MS_POSITIONING="GridLayout"  BACKGROUND="../images/tile.gif" scroll="auto" onload="top.topframe.main1.mousein = false;findMouseIn();SetMenu();" border="1" cellPadding="0" cellSpacing="0">
	<!--<body MS_POSITIONING="GridLayout"  BACKGROUND="../images/tile.gif"  border="1" cellPadding="0" cellSpacing="0">-->
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<form id="Form1" method="post" runat="server">
		<%if (strState == "")
		{
			%>
			<span Class="errmsg" ID="lblErrorMessage"><%=strErrorMsg%></span>
			<%
		}
		else
		{
		%><table class="tableWidth" cellSpacing="0" cellPadding="0" border="0" align="center">
				<tr>
					<TD id="tdClientTop" class="pageHeader">
						<span id="ShowLoadingImage">
							<img SRC="/cms/cmsweb/images/loading.gif" BORDER="0">
						</span>
						<webcontrol:ClientTop id="cltClientTop" runat="server" width="100%"></webcontrol:ClientTop>
						<br>
					</TD>
				</tr>
				<tr><td>
					<table border =1 align="center" class="headereffectcenter">
						<tr> 
							<td align="center">
							<div id="menuFrame1" style="z-index:0">
							<OBJECT id="QuickQuote" classid="<%=strClass%>" VIEWASTEXT>
								<param name="UploadDownloadURL" value="<%=ConfigurationSettings.AppSettings.Get("CmsWebUrl")%>">
								<param name="QuickQuoteState" value="<%=strState%>">
								<param name="QuickQuoteClientId" value="<%=strClientId%>">
								<param name="QuickQuoteUserId" value="<%=strUserId%>">
								<param name="QuickQuoteId" value="<%=strQuoteId%>">
								<param name="QuickQuoteColorScheme" value="<%=GetWindowsGridColor()%>">
								<param name="QuickQuoteOtherDtls" value="<%=strQuickQuoteOtherDtls%>">
							</OBJECT>
							</div>
							</td>
						</tr>
					</table>
				</td></tr>
			</table>
		<%}%>
		</form>
	</body>
</HTML>

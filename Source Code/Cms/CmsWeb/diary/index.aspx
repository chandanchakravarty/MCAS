<%@ Page language="c#" Codebehind="index.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Diary.index" %>
<%@ Register TagPrefix="webcontrol" TagName="DiaryCalendar" Src="/cms/cmsweb/webcontrols/DiaryCalendar.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%--<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
  <head  xmlns="http://www.w3.org/1999/xhtml">
		<title>EBIX Advantage Diary Index</title>
		<%--<meta content="Microsoft Visual Studio 7.0" name="GENERATOR" />
		<meta content="C#" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />--%>
		<link rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css" />
		<script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
		<link href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet" />
		<style type="text/css">
		.hide { OVERFLOW: hidden; POSITION: absolute; TOP: 5px }
		.show { OVERFLOW: hidden; POSITION: absolute; TOP: 5px }
		#tabContents { LEFT: 60px; POSITION: absolute; TOP: 160px }
		</style>
		<script language="javascript" type="text/javascript">	
		
		function onRowClicked(num,msDg )
		{		
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);		
			//alert('first')
			changeTab(0, 0);
			document.getElementById('lblMessage').innerHTML='';	
		}
		
		function Delete()
		{			
			if ( document.indexForm.hidKeyValues.value != '' )
			{
				
				refreshGrid('','');
				document.indexForm.hidKeyValues.value = '';
			}								
		}
		
		function Check()
		{
			var temp = '<%=strCalledFrom%>';
			if(temp.toUpperCase()!="INCLT")
			{
				setfirstTime();
				findMouseIn();				
			}
			else
			{
				firstTime=1;
			}						
			document.getElementById('gridid').style.height=document.body.offsetHeight -20; 
			document.getElementById('diaryColumn').style.width	=	document.body.offsetWidth-document.getElementById('calendarColumn').offsetWidth-12;
			document.getElementById('tabLayer').style.width = document.getElementById('diaryColumn').offsetWidth-12;			
		}
		/*function addRecordClient()
		{
			if(document.getElementById('hidlocQueryStr'))
			{
				document.getElementById('hidlocQueryStr').value = document.getElementById('hidCUSTOMER_ID').value;
			}
			ShowTab();
			addNew = true; 
			show=2;	
			alert(msDg)
			onRowClicked(0,msDg);
		}*/
		//When called fromClaims
		function OpenDetailPage(LISTID)
		{
			//alert(LISTID);
			//RefreshWebgrid(LISTID);//Done for Itrack Issue 5718 on 21 May 2009
			if(document.getElementById('hidSearchText').value!="")
			{
			 RefreshWebgrid(LISTID);//Done for Itrack Issue 5718 on 21 May 2009	
			 externalSearch("CCI.CLAIM_NUMBER",document.getElementById('hidSearchText').value);
			}
		}
		
		function SetFOLLOW_UP_DATE()
		{
			searchStr = 'FOLLOW_UP_DATE';
			externalSearch("T.FOLLOWUPDATE",searchStr);
		}
		
		</script>
</head>
	<body oncontextmenu="javascript:return false;" onload="javascript:top.topframe.main1.mousein = false;Check();Delete();" MS_POSITIONING="GridLayout" >		
		<!-- To add bottom menu -->
		<webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td width="20%" valign="top" id="calendarColumn">
						<webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer>
						<!-- <div id="calDiv"  OVERFLOW: auto; width="190"> -->
							<webcontrol:DiaryCalendar id="userCalendar" runat="server"></webcontrol:DiaryCalendar>
						<!-- </div> -->
					</td>
					<td width="3"></td>
					<td width="80%" id="diaryColumn" valign="top" >
						<form id="indexForm" method="post" runat="server">
							<div id="gridid" style='width:100%; overflow:scroll';> 
							<table cellspacing="0" cellpadding="0" border="0" width="100%" align="left">
								<tr>
									<td><webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer></td>
								</tr>
								<tr>
									<td class="midcolora">										
										<asp:Label ID="lblTopLabel" Runat="server" Font-Bold="true" >View diary entries of:</asp:Label>  
										<asp:DropDownList ID="cmbAllDiary" Runat="server" AutoPostBack="true" style="visibility:visible"></asp:DropDownList><!-- style added by Charles on 17-Sep-09 for Itrack 6420 -->    									
									</td>
								</tr>
								
								<tr>
									<td align="center"  >
										<asp:Label ID="lblMessage" Runat="server" CssClass="errmsg" ></asp:Label>    
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
									</td>
								</tr>
								<tr>
									<td><br />
									</td>
								</tr>
								<tr id="tabCtlRow">
									<td>
										<webcontrol:Tab ID="TabCtl" runat="server" TabTitles="TO DO List" TabLength="150"></webcontrol:Tab>
									</td>
								</tr>
								<tr>
									<td width="82%">
										<table class="tableeffectDiary" cellpadding="0" cellspacing="0" border="0">
											<tr>
												<td>
													<iframe <% if(strCalledFrom!="" && strCalledFrom.ToUpper()=="INCLT") { %>
															class="iframsHeightMedium"
															<% } else {%>
															class="iframsHeightLong"
															<% }%>														 
														id="tabLayer"  src=""  scrolling="yes" frameborder="0"></iframe>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td><webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer></td>
								</tr>								
							</table>							
							<!-- To add footer Control ends here -->
							<input type="hidden" name="hide" /> 
							<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr" />
							<input type="hidden" name="hidMode" /> 
							<input id="hidKeyValues" type="hidden" runat="server" />
							<input type="hidden" id="hidCUSTOMER_ID" name="hidCUSTOMER_ID" runat="server" />						
							<input type="hidden" id="hidDelString" name="hidDelString" runat="server" />   
							<input type="hidden" id="hidErrMsg" name="hidDelString" runat="server" />   
							<input type="hidden" id="hidLISTID" name="hidLISTID" runat="server" />
							<input type="hidden" id="hidSearchText" value="" name="hidSearchText" runat="server" />
						 </div>
						</form>
						
					</td>
				</tr>
			</table>			
		<script type="text/javascript" language="javascript">
		//setTimeout("OpenDetailPage(document.getElementById('hidLISTID').value)",3000);
		//setTimeout("SetFOLLOW_UP_DATE()",2000)//Done for Itrack Issue 5718 on 21 May 2009
		//setTimeout("SetFOLLOW_UP_DATE()",3000)
		if(document.getElementById("cmbAllDiary")!=null)	
		    document.getElementById("cmbAllDiary").style.visibility = 'visible'; 
		
		</script>
	</body>
</html>

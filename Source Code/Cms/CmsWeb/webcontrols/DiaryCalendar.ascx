<%@ Control Language="c#" AutoEventWireup="false" Codebehind="DiaryCalendar.ascx.cs" Inherits="Cms.CmsWeb.WebControls.DiaryCalendar" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
  <head>
		<title>Calendar</title>
<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
<meta content="C#" name="CODE_LANGUAGE"/>
<meta content="JavaScript" name="vs_defaultClientScript"/>
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
<link href="/cms/cmsweb/css/css<%=lStrStyleSheetName%>.css" type="text/css" rel="Stylesheet" />
<script src="/cms/cmsweb/scripts/menubar.js" type="text/javascript"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>
<script language="javascript" type="text/javascript">
		//var oTimer = setInterval('IsGridReady()',4000);
		//var flag=true;			
		
		function OpenPolicyPath(path,CustomerID,PolicyID,AppID,AppVersionID,PolicyLOB,PolicyVersionID)
		{
			path+= "customer_id=" + CustomerID + "&policy_id=" + PolicyID + "&App_id=" + AppID + "&app_version_id=" + AppVersionID + "&Policy_LOB=" + PolicyLOB + "&Policy_Version_ID=" + PolicyVersionID;						
			top.botframe.location.href=path;
		}
		
		function OpenClaimPath(path,CustomerID,PolicyID,ClaimID,PolicyLOB,PolicyVersionID)
		{
			top.topframe.callItemClicked('2','')
			
			path+= "CUSTOMER_ID=" + CustomerID + "&POLICY_ID=" + PolicyID + "&CLAIM_ID=" + ClaimID + "&LOB_ID=" + PolicyLOB + "&POLICY_VERSION_ID=" + PolicyVersionID;						
			top.botframe.location.href=path;
		}

		function openCustomerPath(path, customerid)
		{

		    top.botframe.location.href = '<%=Request.ApplicationPath.ToString() %>' + "/client/aspx/customermanagerindex.aspx?customer_id=" + customerid;
		}
		
		function OpenApplicationPath(path,customerid,appid,appversionid)
		{
			path+="customer_id=" + customerid + "&app_id=" + appid + "&app_version_id=" + appversionid			
			top.botframe.location.href=path;
		}
		
		function openQuickQuote(CustomerID,QuoteId,QuoteType,LOB_DESC,QNo,State)
		{
			top.topframe.callItemClicked('1','')
			//top.botframe.location.href = "../aspx/QuickQuoteLoad.aspx?customer_id=" + CustomerID + "&QQ_ID=" + QuoteId + "&QQ_TYPE=" + QuoteType + "&LOB_DESC=" + LOB_DESC + "&state_name1=" + State  + "&QQ_NUMBER=" + QNo ;
			top.botframe.location.href = "../aspx/QuickQuoteLoad.aspx?customer_id=" + CustomerID + "&QQ_ID=" + QuoteId + "&QQ_TYPE=" + QuoteType + "&LOB_DESC=" + LOB_DESC + "&state_name=" + State  + "&QQ_NUMBER=" + QNo ;
		}
        
        //Added by Charles on 17-Jun-2010 for Quick Application
		function openQuickApp(CustomerID, PolicyID, PolicyVersionID) {        
		    top.topframe.callItemClicked('1', '');
		    var strSystemID = "<%=strSystemID%>";
		    //alert(strSystemID);
		    if (strSystemID.toUpperCase() == "SUAT" || strSystemID.toUpperCase() == "S001") {
		        top.botframe.location.href = '<%=Request.ApplicationPath.ToString() %>' + "/Policies/aspx/QuickQuote.aspx?customer_id=" + CustomerID + "&policy_id=" + PolicyID + "&policy_version_id=" + PolicyVersionID;
		    }
		    else {
		        top.botframe.location.href = '<%=Request.ApplicationPath.ToString() %>' + "/Policies/aspx/QuickApp.aspx?customer_id=" + CustomerID + "&policy_id=" + PolicyID + "&policy_version_id=" + PolicyVersionID;
		    }
		}
					
		function IsGridReady()
		{
			var dc=document.getElementById("dc"); // Added by Charles on 16-Jul-09 for Itrack 6089
			if(dc!=null && typeof(dc)!="undefined")// && flag) // typeof Added by Charles on 13-Jul-09 for Itrack 6089
			{
				fPopCalendar(dc,dc);
				//clearInterval(oTimer);
				flag = false;
						
			}

        }

            $(document).ready(function() {
            IsGridReady();
            });
			
		</script>
</head>
<body>
<script language="javascript" type="text/javascript">
			
			var gSelCell;
			var gdCtrl = new Object();
			var goSelectTag = new Array();

			var gdCurDate = new Date();
			var giYear = gdCurDate.getFullYear();
			var giMonth = gdCurDate.getMonth()+1;
			var giDay = gdCurDate.getDate();
			if ('<%=iLangId%>' == '2')
			    var dispDate = properVal(giDay) + "/" + properVal(giMonth) + "/" + properVal(giYear); // : properVal(giDay) + "/" + properVal(giMonth) + "/" + properVal(giYear);
			else
			    var dispDate = ('<%=aAppCountry%>' == 'US') ? properVal(giMonth) + "/" + properVal(giDay) + "/" + properVal(giYear) : properVal(giDay) + "/" + properVal(giMonth) + "/" + properVal(giYear);
			var dtFormat = '<%=dtFormat%>';
			var gstrshoall='<%=gstrshoall%>';
			var gstrAPQ='<%=gstrAPQ%>';
			var gstrQPA='<%=gstrQPA%>';
			var gstrQPB='<%=gstrQPB%>';
			var gstrEE='<%=gstrEE%>';
			var gstrRR='<%=gstrRR%>';
			var gstrBR='<%=gstrBR%>';
			var gstrER='<%=gstrER%>';
			var gstrAA='<%=gstrAA%>';
			var gStrCanReqHeading='<%=gStrCRHeading%>';
			var gStrReinstateHeading='<%=gStrReinstHeading%>';
			var gstrToday='<%=gstrToday%>';
			var gstrAppoint='<%=gstrAppoint%>';
			var lblMonths='<%=lblMonths%>';
			var lblWeekDay='<%=lblDays%>';
			var gStrAPQC='<%=gStrAPQC%>';
			var gStrQPAC='<%=gStrQPAC%>';
			var gStrQPBC='<%=gStrQPBC%>';
			var gStrEEC='<%=gStrEEC%>';
			var gStrRRC='<%=gStrRRC%>';
			var gStrBRC='<%=gStrBRC%>';
			var gStrERC='<%=gStrERC%>';
			var gStrAAC='<%=gStrAAC%>';
			var gStrCR='<%=gStrCR%>';
			var gStrReinstate='<%=gStrReinstate%>';
			var gstrClmMvmnt='<%=gstrClmMvmnt%>';			
			var gstrClmHeading='<%=gstrClmHeading%>';
			var gAppDatesGlobal = "<%=gStrAppDates%>";
			var listtypeid1 = '<%=listtypeid1%>';
			var listtypeid2 = '<%=listtypeid2%>';
			var listtypeid3 = '<%=listtypeid3%>';
			var listtypeid4 = '<%=listtypeid4%>';
			var listtypeid5 = '<%=listtypeid5%>';
			var listtypeid6 = '<%=listtypeid6%>';
			var listtypeid7 = '<%=listtypeid7%>';			
			var listtypeid8 = '<%=listtypeid8%>';			
			var listtypeid9 = '<%=listtypeid9%>';			
			var listtypeid10 = '<%=listtypeid10%>';			
			var gStrCF='<%=strCF%>';
			var gStrANF='<%=strANF%>';
			var gStrCRE='<%=strCRE%>';
			var cStrCF='<%=cStrCF%>'
			var cStrANF='<%=cStrANF%>'
			var cStrCRE='<%=cStrCRE%>'
			var cookieCustName="<%=cookieCustomerName%>";
			var cookieApplication="<%=cookieApplication%>";
			var cookiePolicy="<%=cookiePolicy%>";
			var cookieQQ="<%=cookieQQ%>";
			var cookieClaim="<%=cookieClaim%>";
			
			var cookieCustDate='<%=cookieCustDate%>';
			var cookieAppDate='<%=cookieAppDate%>';
			var cookiePolDate='<%=cookiePolDate%>';
			var cookieQQDate='<%=cookieQQDate%>';
			var cookieClaimDate='<%=cookieClaimDate%>';
			
			
			
			function properVal(no) {
				return (no < 10 ? "0"+no : no);
			}
			function hideLayer()
			{
				VicPopCal.style.visibility = "hidden";
				/*for (i in goSelectTag)
  					goSelectTag[i].style.visibility = "visible";
				goSelectTag.length = 0;*/ //commented by anshuman
			}

			function fSetDate(iYear, iMonth, iDay){
				//VicPopCal.style.visibility = "hidden";				  
				
				//gdCtrl.value = iDay+"/"+iMonth+"/"+iYear;
				
				gdCtrl.value = ('<%=aAppCountry%>' == 'US') ?  properVal(iMonth) + "/" + properVal(iDay) + "/" + properVal(iYear):properVal(iDay) + "/" + properVal(iMonth) + "/" + properVal(iYear) ;
				//gdCtrl.focus(); Commented by Charles on 14-Jul-09 for Itrack 6089				
				
				if (iMonth < 10)
					iMonth = "0" + iMonth;					
				if (iDay < 10)
					iDay = "0" + iDay;
					
					if(chkShowAll.checked==true)
				{
					chkShowAll.checked=false;
				}
				// for today
				searchStr = ('<%=aAppCountry%>' == 'US') ?  iMonth + "/" + iDay + "/" + iYear:iDay + "/" + iMonth + "/" + iYear;
				externalSearch("T.FOLLOWUPDATE",searchStr);
				//top.frames[1].externalSearch("T.FOLLOWUPDATE",searchStr); 			
			}

			function fSetSelected(aCell){
				var iOffset = 0;
				var iYear = parseInt(tbSelYear.value);
				var iMonth = parseInt(tbSelMonth.value);
				var iMonthLen = tbSelMonth.value.length;
				
				with (aCell.children[0]){  				
  					var iDay= parseInt(innerHTML);
  					var iDayLen = innerHTML.length
  					if (className=="DisabledDate")
						iOffset = (Victor<10)?-1:1;
					iMonth += iOffset;
					if (iMonth<1) {
						iYear--;
						iMonth = 12;
					}else if (iMonth>12){
						iYear++;
						iMonth = 1;
					}				
				}
				if (iMonthLen == 1)
					iMonth = "0" + iMonth;
				if (iDayLen == 1)
					iDay = "0" + iDay;
	//searchStr = ('<%=dtFormat%>' == 'United Kingdom') ? iDay + "/" + iMonth + "/" + iYear : iMonth + "/" + iDay + "/" + iYear
	if ('<%=iLangId%>' == '2')
	    searchStr = iDay + "/" + iMonth + "/" + iYear;
	else
	    searchStr = ('<%=aAppCountry%>' == 'US') ? iMonth + "/" + iDay + "/" + iYear : iDay + "/" + iMonth + "/" + iYear
				//alert(top.frames[1].location )
				if(chkShowAll.checked==true)
				{
					chkShowAll.checked=false;
				}
				externalSearch("T.FOLLOWUPDATE",searchStr); 
				//top.frames[1].externalSearch("T.FOLLOWUPDATE",searchStr); 
			}

			function Point(iX, iY){
				this.x = iX;
				this.y = iY;
			}

			function fBuildCal(iYear, iMonth) {
				var aMonth=new Array();
				for(i=1;i<7;i++)
  					aMonth[i]=new Array(i);
				  
				var dCalDate=new Date(iYear, iMonth-1, 1);
				var iDayOfFirst=dCalDate.getDay();
				var iDaysInMonth=new Date(iYear, iMonth, 0).getDate();
				var iOffsetLast=new Date(iYear, iMonth-1, 0).getDate()-iDayOfFirst+1;
				var iDate = 1;
				var iNext = 1;

				for (d = 0; d < 7; d++)
					aMonth[1][d] = (d<iDayOfFirst)?-(iOffsetLast+d):iDate++;
				for (w = 2; w < 7; w++)
  					for (d = 0; d < 7; d++)
						aMonth[w][d] = (iDate<=iDaysInMonth)?iDate++:-(iNext++);
				return aMonth;
			}

			//function fDrawCal(iYear, iMonth, iCellHeight, sDateTextSize) {
			function fDrawCal(iYear, iMonth) {
				var WeekDay = lblWeekDay.split(",");
		
			//var WeekDay = new Array("SUN","MON","TUE","WED","THU","FRI","SAT");
			//var styleTD = "bgcolor='"+gcBG+"' bordercolor='"+gcBG+"' valign=middle align=center height='"+iCellHeight+"' style='font-family: verdana, arial, MS Sans Serif; cursor:hand; font-size:" +sDateTextSize+"';";            //Coded by Liming Weng(Victor Won) email:victorwon@sina.com
			var prevClass = "";
			with (document) {
				write("<tr>");
				for(i=0; i<7; i++)
					write("<td class='WeekHeads' align='center'>" + WeekDay[i] + "</td>");
				write("</tr>");

  				for (w = 0; w < 6; w++) {
					write("<tr>");
					for (d = 0; d < 7; d++) {
						//write("<td id=calCell class=OtherDate onMouseOver=\"prevClass=this.className; this.className='CurrentDate';\" onMouseOut=\"this.className=prevClass;\" onclick=\"this.className='CurrentDate'; fSetSelected(this);\">");
						write("<td id='calCell_" + w + "_" + d + "' class=midcolorforcal onclick=\"if(gSelCell != null) gSelCell.className=prevClass; prevClass=this.className; gSelCell=this; this.className='CurrentDate'; fSetSelected(this);\" >");
						write("<font id='cellText_" + w + "_" + d + "'  Victor='Liming Weng'> </font>");
						write("</td>")
					}
					write("</tr>");
				}
			}
			}

			function fUpdateCal(iYear, iMonth) {
			    myMonth = fBuildCal(iYear, iMonth);
			var i = 0; 
			var diaryDates = gAppDatesGlobal;
			//alert(diaryDates)
			for (w = 0; w < 6; w++)
				for (d = 0; d < 7; d++)
					with (document.getElementById('cellText_' + w + '_' + d)) {
						Victor = i++;
						if (myMonth[w+1][d]<0) {
							//color = gcGray;
							className = "DisabledDate";
							//innerText = -myMonth[w + 1][d];
							document.getElementById('cellText_' + w + '_' + d).innerHTML = -myMonth[w + 1][d];
							//calCell[(7*w)+d].className = "OtherDate";
							document.getElementById('calCell_' + w + '_' + d).className = "OtherDate";
							
						}else{				
							//color = ((d==0)||(d==6))?"red":"black";
							className = ((d==0)||(d==6))?"Weekend":"ActiveDate";
							if ((iMonth == giMonth) && (myMonth[w+1][d] == giDay) && (iYear == giYear))	//For Current Date the background color will be white.
							{					
								//calCell[(7*w)+d].style.backgroundColor = gcCurrentDate;
								
								tmpDt = ('<%=dtFormat%>' == 'MM/dd/yyyy') ?  properVal(iMonth)+"/"+properVal(myMonth[w+1][d])+"/"+iYear : properVal(myMonth[w+1][d])+"/"+properVal(iMonth)+"/"+iYear ;
								
								if (diaryDates.indexOf(tmpDt,0) > 0) {
									//calCell[(7*w)+d].style.fontWeight = 'bold';
								    document.getElementById('calCell_' + w + '_' + d).style.fontWeight = 'bold';
								}
								else
								{
								    //calCell[(7 * w) + d].style.fontWeight = '';
								    document.getElementById('calCell_' + w + '_' + d).style.fontWeight = '';
								}
								//calCell[(7*w)+d].className = "CurrentDate";
								document.getElementById('calCell_' + w + '_' + d).className = "CurrentDate";
								gSelCell = document.getElementById('calCell_' + w + '_' + d); //calCell[(7*w)+d];
								prevClass = "OtherDate";
							}
							else {
							    //calCell[(7*w)+d].className = "OtherDate";
							    document.getElementById('calCell_' + w + '_' + d).className = "OtherDate";
								//tmpDt = ('<%=dtFormat%>' == 'United Kingdom') ? properVal(myMonth[w+1][d])+"/"+properVal(iMonth)+"/"+iYear : properVal(iMonth)+"/"+properVal(myMonth[w+1][d])+"/"+iYear;
								tmpDt = ('<%=dtFormat%>' == 'MM/dd/yyyy') ?  properVal(iMonth)+"/"+properVal(myMonth[w+1][d])+"/"+iYear : properVal(myMonth[w+1][d])+"/"+properVal(iMonth)+"/"+iYear ;
								//alert(tmpDt + "---)" + diaryDates.indexOf(tmpDt,0))
								if (diaryDates.indexOf(tmpDt,0) > 0) {
									//calCell[(7*w)+d].style.fontWeight = 'bold';
								    document.getElementById('calCell_' + w + '_' + d).style.fontWeight = 'bold';
								}
								else
								{
								    //calCell[(7 * w) + d].style.fontWeight = '';
								    document.getElementById('calCell_' + w + '_' + d).style.fontWeight = '';
								}
							}
							//innerText = myMonth[w + 1][d];
							document.getElementById('cellText_' + w + '_' + d).innerHTML = myMonth[w + 1][d];
						}
					}
					
			}

			function fSetYearMon(iYear, iMon){
				tbSelMonth.options[iMon-1].selected = true;
				for (i = 0; i < tbSelYear.length; i++)
					if (tbSelYear.options[i].value == iYear)
						tbSelYear.options[i].selected = true;
				fUpdateCal(iYear, iMon);
			}

			function fPrevMonth(){
				var iMon = tbSelMonth.value;
				var iYear = tbSelYear.value;
				  
				if (--iMon<1) {
					iMon = 12;
					iYear--;
			}
			  
			fSetYearMon(iYear, iMon);
			}

			function fNextMonth(){
				var iMon = tbSelMonth.value;
				var iYear = tbSelYear.value;
				  
				if (++iMon>12) {
					iMon = 1;
					iYear++;
				}
				fSetYearMon(iYear, iMon);
			}

			function fToggleTags(){
			    with ( document.getElementsByTagName('select')) {
 					for (i=0; i<length; i++)
 						if ((item(i).Victor!="Won")&&fTagInBound(item(i))){
 							item(i).style.visibility = "hidden";
 							goSelectTag[goSelectTag.length] = item(i);
 						}
				}
			}

			function fTagInBound(aTag){
				with (VicPopCal.style){
  					var l = parseInt(left);
  					var t = parseInt(top);
  					var r = l+parseInt(width);
  					var b = t+parseInt(height);
					var ptLT = fGetXY(aTag);
					return !((ptLT.x>r)||(ptLT.x+aTag.offsetWidth<l)||(ptLT.y>b)||(ptLT.y+aTag.offsetHeight<t));
				}
			}

			function fGetXY(aTag){
				var oTmp = aTag;
				var pt = new Point(0,0);
				do {
  					pt.x += oTmp.offsetLeft;
  					pt.y += oTmp.offsetTop;
  					oTmp = oTmp.offsetParent;
				} while(oTmp.tagName!="BODY");
				return pt;
			}

			// Main: popCtrl is the widget beyond which you want this calendar to appear;
			//       dateCtrl is the widget into which you want to put the selected date.
			// i.e.: <input type="text" name="dc" style="text-align:center" readonly><INPUT type="button" value="V" onclick="fPopCalendar(dc,dc);return false">

			function fPopCalendar(popCtrl, dateCtrl)
			{
				 
				gdCtrl = dateCtrl;
			
				
				fSetYearMon(giYear, giMonth); 
				var point = fGetXY(popCtrl);
				with (VicPopCal.style) {
  					left = point.x;
					//top  = point.y+popCtrl.offsetHeight; -- commented by Anshuman to set top as grid's top
					top  = popCtrl.offsetHeight;
					width = VicPopCal.offsetWidth;
					
					height = VicPopCal.offsetHeight;
					fToggleTags(point); 	
					visibility = 'visible';
				}
			}
			function ShowAllApp()
			{
				if(chkShowAll.checked==true)
				{
					searchStr = ""
					//top.frames[1].externalSearch("T.FOLLOWUPDATE",searchStr);
					externalSearch("T.FOLLOWUPDATE",searchStr); 						
				}
				//Added for Itrack Issue 5718 0n 6 May 09
				else
				{
				    searchStr = 'FOLLOW_UP_DATE';
					externalSearch("T.FOLLOWUPDATE",searchStr);
				}
				//	top.frames["botframe"].location.href="/cms/cmsweb/menus/menuBar.aspx?todotype=0&ViewAll=Y";						
			}
			function ShowHyperApp(hyperlink)
			{
				if (hyperlink==24)  // vivek srivastava. Added for Opening Claim Movement Page in the Right Pane.
				{
					
					//top.frames["botframe"].location.href="../ClaimMaintenance/ClaimsAwaitingAuthorisationList.aspx?CalledFrom=Diary";
				}
				else
				{					
					externalSearch("t.listtypeid",hyperlink);
					//top.frames[1].externalSearch("t.listtypeid",hyperlink); 
				}	
			}			
			//To Add for Country Sepecific - vmarwaha ON 27/MAR/03
				
			var gMonths = lblMonths.split(",");
			with (document) 
			{
			    write("<Div id='VicPopCal' style='VISIBILITY:hidden;border:0px ridge;height:475px;top:0px;overflow:scroll';>");
				write("<table border='0' cellspacing='0' cellpadding='0' width='100%'>");
				write("<TR><td class='midcolorc'><marquee truespeed=1 scrolldelay='30' scrollamount=1><b>"+gstrAppoint+"</marquee></b></td></TR>");
				write("<TR><td valign='middle' class='midcolorc'><input type='button' class='clsButton' name='PrevMonth' value='&lt;' onClick='fPrevMonth()'>");
				write("&nbsp;<SELECT name='tbSelYear' id='tbSelYear' onChange='fUpdateCal(tbSelYear.value, tbSelMonth.value)' Victor='Won'>");
				for(i=1900;i<2014;i++)
					write("<OPTION value='"+i+"'>"+i+"</OPTION>");
				write("</SELECT>");
				write("&nbsp;<select name='tbSelMonth' id='tbSelMonth' onChange='fUpdateCal(tbSelYear.value, tbSelMonth.value)' Victor='Won'>");
				for (i=0; i<12; i++)
					write("<option value='"+(i+1)+"'>"+gMonths[i]+"</option>");
				write("</SELECT>");
				write("&nbsp;<input type='button' class='clsButton' name='NextMonth' value='&gt;' onclick='fNextMonth()'>");
				write("</td></TR>");
				write("<TR><td align='center' >");
				write("<DIV style='background-color:white'>");
				write("<table width='100%' cellspacing='1' cellpadding='1' border='0'>");
				// The following function is where the Calendar mathematics are done and the Calendar is generated.
				fDrawCal(giYear, giMonth);
				write("</table></DIV>");
				write("</td></TR>");
				write("<TR><TD class='midcolorc'><b>"+gstrToday+": <A href='#' onclick='javascript: var winOpen = fSetDate(giYear,giMonth,giDay);' class='Dark'><b>" + dispDate + "</b></A></b>");
				write("</TD></TR>");
				write("<TR><TD valign=top class=midcolora>");//<TD valign=top class='midcolora'>
				write("<INPUT type='checkbox'  id='chkShowAll' name='chkShowAll' onClick='javascript:ShowAllApp();'>");
				write(""+gstrshoall+"</TD></tr>");
				write("<TR><TD valign=top height=10  colspan=1>");
				write("</TD></tr>");		
				write("</TABLE><TABLE border=0 cellspacing=0 width=100% cellpadding=0>");
				write("<TR><TD valign=top class='midcolorc' colspan=2>");
				write("<img src='../images/bullet-diary.gif' border=0>&nbsp;<b>" + '<%=strPT %>' + "</b>");
				write("</TD></TR>");
				write("<TR><TD valign=top class='midcolora' nowrap>");
				write("<div id='pendingTaskDiv'></div>");
				write("</td><TR></TABLE><TABLE border=0 cellspacing=0 width=100% cellpadding=0>");
				write("<TR><TD HEIGHT=10 colspan=2></TD></TR><TR><td></td><TD valign=top class='midcolorc' nowrap><b>" + '<%=strRV %>' + "</b></td></tr>");
				//write("<TR><td></td><TD valign=top class='midcolorc' nowrap><b>Recently Visited</b></td></tr>");
				write("<tr><td colspan=2  class='midcolora' >");
				write("<div id='cookieDiv'></div>");				
				write("</td></TR>");
				write("</TABLE>");
				write("<TABLE border=0 cellspacing=0 width=100% cellpadding=0>");
				write("");
				write("<tr><td colspan=2  class='midcolora' nowrap>");
				write("<div id='cookieAppDiv'></div>");				
				write("</td></TR>");
				write("</TABLE>");	
					
				write("<TABLE border=0 cellspacing=0 width=100% cellpadding=0>");
				write("");
				write("<tr><td colspan=2 valign=top class='midcolora' nowrap>");
				write("<div id='cookiePolDiv'></div>");				
				write("</td></TR>");
				write("</TABLE>");
				write("<TABLE border=0 cellspacing=0 width=100% cellpadding=0>");
				write("");
				write("<tr><td colspan=2  class='midcolora' nowrap>");
				write("<div id='cookieQQDiv'></div>");				
				write("</td></TR>");
				write("</TABLE>");					
				write("<TABLE border=0 cellspacing=0 width=100% cellpadding=0>");
				write("");
				write("<tr><td colspan=2  class='midcolora' nowrap>");
				write("<div id='cookieClaimDiv'></div>");				
				write("</td></TR>");
				write("</TABLE>");					
				
				write("<TABLE border=0 cellspacing=0 width=100% cellpadding=0>");
				write("<TR><TD colspan=4 class='midcolora'><div style=\"height:700\"></div></TR>");
				write("</TABLE></Div>");
			}
			writePendingTask();
			writeCookie();

			function writePendingTask() {
			
				var gStrPending=""; 
				
				//1
				if(gStrEEC==-1)
				{
					gStrPending += "<li>"+gStrEEC+" "+gstrEE+"</li>";
				}
				else
				{
					gStrPending += "<li><A href='javascript:' onClick='javascript:ShowHyperApp(\"" + listtypeid1 + "\");' class='CalLink'>"+gStrEEC+" "+gstrEE+"</A></li>";
				}
				//2
				if(gStrAAC==-1)
				{
					gStrPending += "<li>"+gStrAAC+" "+gstrAA+"</li>";
				}
				else
				{
					gStrPending += "<li><A href='javascript:' onClick='javascript:ShowHyperApp(\"" + listtypeid7 + "\");' class='CalLink'>"+gStrAAC+" "+gstrAA+"</A></li>";
				}
				//3
				if(gStrQPAC==-1)
				{
					gStrPending+="<li>"+gStrQPAC+" "+gstrQPA+"</li>";
				}
				else
				{
					gStrPending += "<li><A href='javascript:' onClick='javascript:ShowHyperApp(\"" + listtypeid2 + "\");' class='CalLink'>"+gStrQPAC+" "+gstrQPA+"</A></li>";
				}
				//4
				if(gStrERC==-1)
				{
					gStrPending += "<li>"+gStrERC+" "+gstrER+"</li>";
				}
				else
				{
					gStrPending += "<li><A href='javascript:' onClick='javascript:ShowHyperApp(\"" + listtypeid6 + "\");' class='CalLink'>"+gStrERC+" "+gstrER+"</A></li>";
				}
				//5
				if(gStrBRC==-1)
				{
					gStrPending += "<li>"+gStrBRC+" "+gstrBR+"</li>";
				}
				else
				{
					gStrPending += "<li><A href='javascript:' onClick='javascript:ShowHyperApp(\"" + listtypeid5 + "\");' class='CalLink'>"+gStrBRC+" "+gstrBR+"</A></li>";
				}
				//6
				if(gStrRRC==-1)
				{
					gStrPending += "<li>"+gStrRRC+" "+gstrRR+"</li>";
				}
				else
				{
					gStrPending += "<li><A href='javascript:' onClick='javascript:ShowHyperApp(\"" + listtypeid4 + "\");' class='CalLink'>"+gStrRRC+" "+gstrRR+"</A></li>";
				}
				//7
				if(gStrQPBC==-1)
				{
					gStrPending += "<li>"+gStrQPBC+" "+gstrQPB+"</li>";
				}
				else
				{
					gStrPending += "<li><A href='javascript:' onClick='javascript:ShowHyperApp(\"" + listtypeid3 + "\");' class='CalLink'>"+gStrQPBC+" "+gstrQPB+"</A></li>";
				}
				
				if(cStrCF==-1)
				{
					gStrPending += "<li>"+cStrCF+" "+gStrCF+"</li>";
				}
				else
				{
					gStrPending += "<li><A href='javascript:' onClick='javascript:ShowHyperApp(\"" + listtypeid8 + "\");' class='CalLink'>"+cStrCF+" "+gStrCF+"</A></li>";
				}
				
				if(cStrANF==-1)
				{
					gStrPending += "<li>"+cStrANF+" "+gStrANF+"</li>";
				}
				else
				{
					gStrPending += "<li><A href='javascript:' onClick='javascript:ShowHyperApp(\"" + listtypeid9 + "\");' class='CalLink'>"+cStrANF+" "+gStrANF+"</A></li>";
				}
				
				if(cStrCRE==-1)
				{
					gStrPending += "<li>"+cStrCRE+" "+gStrCRE+"</li>";
				}
				else
				{
					gStrPending += "<li><A href='javascript:' onClick='javascript:ShowHyperApp(\"" + listtypeid10 + "\");' class='CalLink'>"+cStrCRE+" "+gStrCRE+"</A></li>";
				}
				
				document.getElementById('pendingTaskDiv').innerHTML = gStrPending;
			}
			
			function writeCookie()
			{
			    //Try - Catch added by Charles on 11-Mar-10 for Policy Page Implementation
			    try
			    {			    
			        if(cookieCustDate!='')
			            document.getElementById('cookieDiv').innerHTML = "<li><B>" + '<%=strCustomer%>' + "</b></li>" + cookieCustName + " " + '<%=strOn%>' + " " + cookieCustDate + "</td></tr>";
			        ///* Commented by Charles on 11-Mar-10 for Policy Page Implementation 
				    if(cookieApplication!='')
				        document.getElementById('cookieAppDiv').innerHTML = "<li><B>" + '<%=strApplication%>' + "</b></li>" + cookieApplication + " " + '<%=strOn%>' + " " + cookieAppDate + "</td></tr>";
				  
				    if(cookiePolicy!='')
				        document.getElementById('cookiePolDiv').innerHTML = "<li><B>" + '<%=strPolicy%>' + "</b></li>" + cookiePolicy + " "+'<%=strOn%>'+" " + cookiePolDate + "</td></tr>";
				    
				    if(cookieQQ!='')
				        document.getElementById('cookieQQDiv').innerHTML = "<li><B>" + '<%=strQuickAPP%>' + "</b></li>" + cookieQQ + " On " + cookieQQDate + "</td></tr>";			
				   
				    
				    if(cookieClaim!='')
				        document.getElementById('cookieClaimDiv').innerHTML = "<li><B>" + '<%=strClaim%>' + "</b></li>" + cookieClaim + " "+'<%=strOn%>'+" " + cookieClaimDate + "</td></tr>";					
				 }
				 catch(err)
				 { }				
			}		
			
		</script>

<table>
  <tr><!-- Changed from ASP Server Control to HTML Control on 14-Jul-09 for Itrack 6089, Charles -->
    <td><input type="text" id="dc" style="TEXT-ALIGN: center; visibility:hidden"/></td></tr></table>
	</body>
</html>

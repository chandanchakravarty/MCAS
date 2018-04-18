/*
Added by Charles for Multilingual Support
iLangID = 1 : en-US
iLangID = 2 : pt-BR

sCultureDateFormat 
DD/MM/YYYY
MM/DD/YYYY
*/
var gdCtrl = new Object();
var goSelectTag = new Array();

var gcGray = "#bbbbbb";		//Ineffective Date in Calander
var gcToggle = "#ffffff";
var gcBG = "#a7d8f5";
//var gcBG = "#DBE6FF";		//Background Color for Date(Cells)
var gcCurrentDate = "#ffffff";

//var gccol= "#a7d8f5";	
var gccol= "";		//Mouse Over for 'To Day'
var gcToday = "#ffffff";

var gcCalbgColor = "#DBE6FF";

//Purpose: To fix the Cell height & font size of the Calender
//These variable was passed as parameter for fDrawCal() before, now by declaring
//globally if you want to change the Cell Height, just change the value below.
var iCellHeight = "12";
var sDateTextSize = "8pt";

var gdCurDate = new Date();
var giYear = gdCurDate.getFullYear();
var giMonth = gdCurDate.getMonth()+1;
var giDay = gdCurDate.getDate();

var dispDate;
if (sCultureDateFormat == 'DD/MM/YYYY') {
    
    dispDate = giDay + "/" + giMonth + "/" + giYear;
}
else {
    dispDate = giMonth + "/" + giDay + "/" + giYear;
}

function hideLayer()
{
  VicPopCal.style.visibility = "hidden";
  //Amar - Hide the Cover behind the calendar
  DisposeCover();
  
  /*for (i in goSelectTag)
  	goSelectTag[i].style.visibility = "visible";*/
  
  goSelectTag.length = 0;
 }

 function fSetDate(iYear, iMonth, iDay) {
     
  VicPopCal.style.visibility = "hidden";
  //Amar - Hide the Cover behind the calendar
  DisposeCover();
	//To Add for Country Sepecific - BALAJI v ON 27/MAR/03
	//gdCtrl.value = iDay+"/"+iMonth+"/"+iYear;
	//Change by Anurag Verma on 05/04/2005
	
	try {

	    if (sCultureDateFormat == 'DD/MM/YYYY') {
	        gdCtrl.value = iDay + "/" + iMonth + "/" + iYear;
	    }
	    else {
	        gdCtrl.value = iMonth + "/" + iDay + "/" + iYear;
	    }
	    gdCtrl.focus();
	    gdCtrl.fireEvent("onchange"); // To fire the onchange event for the date control, 20/july/2011 by aditya. 
	}
	
	catch (ex)
	{ }
 
  /*for (i in goSelectTag)
  	goSelectTag[i].style.visibility = "visible";*/
  goSelectTag.length = 0;
}

function fSetSelected(aCell){
  var iOffset = 0;
  var iYear = parseInt(tbSelYear.value);
  var iMonth = parseInt(tbSelMonth.value);
  
  aCell.bgColor = gcBG;
  with (aCell.children["cellText"]){
  	var iDay = parseInt(innerText);
  	if (color==gcGray)
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
  fSetDate(iYear, iMonth, iDay);
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
  var year = iYear;
  var month = iMonth;
  
  if (iLangID == 2) {
      var WeekDay = new Array("DOM", "SEG", "TER", "QUA", "QUI", "SEX", "S" + String.fromCharCode(193) + "B");
  }
  else {
      var WeekDay = new Array("SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT");
  }
  
  var styleTD = "bgcolor='"+gcBG+"' bordercolor='"+gcBG+"' valign=middle align=center height='"+iCellHeight+"' style='font-family: verdana, arial, MS Sans Serif; cursor:hand; font-size:" +sDateTextSize+"';";            //Coded by Liming Weng(Victor Won) email:victorwon@sina.com

  with (document) {
	write("<tr>");
	for(i=0; i<7; i++)
		write("<td class=midcolorforcal ><b>" + WeekDay[i] + "</b></td>");
	write("</tr>");

  	for (w = 1; w < 7; w++) {
		write("<tr>");
		for (d = 0; d < 7; d++) {
			write("<td id=calCell class=midcolorforcal onMouseOver='this.bgColor=gcToggle' onMouseOut='this.bgColor=gcBG' onclick='fSetSelected(this)'>");
			write("<font id=cellText Victor='Liming Weng'> </font>");
			write("</td>")
		}
		write("</tr>");
	}
  }
}

function fUpdateCal(iYear, iMonth) {
  myMonth = fBuildCal(iYear, iMonth);  
  var i = 0;  
  for (w = 0; w < 6; w++)
	for (d = 0; d < 7; d++)
		with (cellText[(7*w)+d]) {
			Victor = i++;
			if (myMonth[w+1][d]<0) {
				color = gcGray;
				innerText = -myMonth[w+1][d];
			}else{				
				color = ((d==0)||(d==6))?"red":"black";
				if ((iMonth == giMonth) && (myMonth[w+1][d] == giDay) && (iYear == giYear))	//For Current Date the background color will be white.
				{				
					calCell[(7*w)+d].style.backgroundColor = gcCurrentDate;
				}
				else
				{					
					calCell[(7*w)+d].Class = "midcolorc";
					//calCell[(7*w)+d].Class = "midcolorforcal";
					calCell[(7*w)+d].style.backgroundColor = "";	
				}
				innerText = myMonth[w+1][d];
				onclick='fSetSelected(this)';				
			}
		}
}

function fSetYearMon(iYear, iMon) {
    tbSelMonth.options[iMon - 1].selected = true;
    for (i = 0; i < tbSelYear.length; i++)
        if (tbSelYear.options[i].value == iYear)
        tbSelYear.options[i].selected = true;

    //

    fUpdateCal(iYear, iMon);
} // Function created to accept year upto 1800 on customer and co-Applicant page for creation date when type of customer is commercial



function fSetYearMon(iYear, iMon, popCtrl) {

    if (document.getElementById('cmbCUSTOMER_TYPE') != null) {
        var CustomerType = document.getElementById('cmbCUSTOMER_TYPE').value;
        //var  = popCtrl;
        if (typeof (CustomerType) != 'undefined') {
            if (CustomerType == '11109' && popCtrl != 'undefined' && popCtrl != null && (popCtrl.id == "txtDATE_OF_BIRTH" || popCtrl.id == "txtCO_APPL_DOB")) {
                UpdateCalanderYear(tbSelYear, 1800, 2020)
            } else { UpdateCalanderYear(tbSelYear, 1900, 2020) }
        } else { UpdateCalanderYear(tbSelYear, 1900, 2020) }
    }

  tbSelMonth.options[iMon-1].selected = true;
  for (i = 0; i < tbSelYear.length; i++)
	if (tbSelYear.options[i].value == iYear)
	    tbSelYear.options[i].selected = true;

	//
	fUpdateCal(iYear, iMon);
	  
} // Function created to accept year upto 1800 on customer and co-Applicant page for creation date when type of customer is commercial

function UpdateCalanderYear(tbSelYear, Starts,EndYear) {
    // var option = "";
    tbSelYear.options.length = 0;
    var Index = 0;
    for (i = parseInt(Starts); i < EndYear; i++) {
        var Option = document.createElement("option");
        Option.value = i;
        Option.text = i;
        tbSelYear.add(Option);
        ///Index = parseInt(Index) + 1;
    }

} // Function created to accept year upto 1800 on customer and co-Applicant page for creation date when type of customer is commercial 

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
	//Amar - Do nothing to hide the <Select> tags
	return;
  with (document.all.tags("SELECT")){
 	for (i=0; i<length; i++)
 	{
 		if ((item(i).Victor!="Won")&&fTagInBound(item(i))){
 			item(i).style.visibility = "hidden";
 			goSelectTag[goSelectTag.length] = item(i);
 		}
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
  var previousTag=""; 
  var previousX=0;
  var previousY=0; 
  var pt = new Point(0,0);
  do {
  		if(previousTag==oTmp.tagName)// minus the initial offset values if controls are of same type to keep calander at right place
		{  		
  			pt.x -= previousX;
  			pt.y -= previousY;
  		}
 		previousX=oTmp.offsetLeft;
		previousY=oTmp.offsetTop;
 		pt.x += oTmp.offsetLeft;
  		pt.y += oTmp.offsetTop;	  	
  		previousTag=oTmp.tagName;
  		oTmp = oTmp.offsetParent;  	
  } while(oTmp.tagName!="BODY");
  return pt;
}

// Main: popCtrl is the widget beyond which you want this calendar to appear;
//       dateCtrl is the widget into which you want to put the selected date.
// i.e.: <input type="text" name="dc" style="text-align:center" readonly><INPUT type="button" value="V" onclick="fPopCalendar(dc,dc);return false">

function fPopCalendar(popCtrl, dateCtrl) {
    
	if(dateCtrl.disabled==true || dateCtrl.readOnly==true)
		return;
	
  gdCtrl = dateCtrl;
  //dateCtrl.value = '';
  var szUKdate;
  var szUSdate;
  
    
  if (dateCtrl.value  == '')
      gdCurDate = new Date();  	    
  else	{
	//alert('inside else' + dateCtrl.value)
	//szUSdate  = GetFormattedDate(dateCtrl.value,'MM/DD/YYYY','DD/MM/YYYY','MM/DD/YYYY');
      //alert(szUSdate)
      if (sCultureDateFormat == 'DD/MM/YYYY') {
      
          aDateArr = dateCtrl.value.split('/');

          strDay = aDateArr[0];
          strMonth = aDateArr[1];
          strYear = aDateArr[2];

          gdCurDate = new Date(strMonth + '/' + strDay + '/' + strYear);  
      }
      else {
           gdCurDate = new Date(dateCtrl.value);     
      }
  }  
  
  if (gdCurDate){
	 giYear		= gdCurDate.getFullYear();
	 giMonth	= gdCurDate.getMonth()+1;
	 giDay		= gdCurDate.getDate();
  }	
 if(isNaN(giYear)&& isNaN(giMonth) && isNaN(giDay))
 {	
	return;
 }
 fSetYearMon(giYear, giMonth, popCtrl); 
  var point = fGetXY(popCtrl);
  with (VicPopCal.style) {	
  	left = point.x;  //-85;   ---Commented By Lalit for Calender Position
	top  = point.y+popCtrl.offsetHeight;	
	width = VicPopCal.offsetWidth;
	height = VicPopCal.offsetHeight;
	fToggleTags(point); 	
	visibility = 'visible';
  }
//	dispDate = giDay + "/" + giMonth + "/" + giYear;

	///<summary>
	///	Following code displays the calander header, put in table so that the cross button 
	///	doesn't wrap to next line
	///</summary>
  ///<author>Amar</author>
  var strToday;
  if (iLangID == 2) {
      strToday = "Hoje"; 
  }
  else {
      strToday = "Today";
  }
	
	var sTable = '';
	sTable += "<Table Width='100%' CellSpacing='0' Border='0' CellPadding='0'>";
	sTable += "	<Tr> ";
	sTable += "		<Td Style='font-family: verdana, arial, MS Sans Serif; font-size:" + sDateTextSize + ";color:" + gcToday + "' Width='99%'>"+strToday+":" + dispDate + "</Td> ";
	sTable += "		<Td Width='1%'><img Src='/cms/cmsweb/Images/cross.gif' Border='0' onclick='hideLayer()' alt='Click here to close.'></Td> ";
	sTable += "	</Tr> ";
	sTable += "</Table> ";
	dtarea.innerHTML = sTable;
	////////////////////////////////////////////////////////////////////////////////////////////////////////////
 
	//Amar - Call to Initialize Cover behind the calendar
	InitializeCover();
}
//To Add for Country Sepecific
//dispDate = giDay + "/" + giMonth + "/" + giYear
if (iLangID == 2) {
    var gMonths = new Array("Janeiro", "Fevereiro", "Mar" + String.fromCharCode(231) + "o", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro");
}
else {
    var gMonths = new Array("January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");
}

with (document) {
    write("<html>");
    write("<head>");
    /*write("<LINK href='../stylesheets/Css1.css' rel=stylesheet>");	//Require to pass CSS File here*/
    write("</head>");

    //#region "Code Added by Amar - Adds another Div with IFrame within it."
    write("<Div id='divCover' Style='visibility:Hidden;Position:Absolute;background-Color:Black;Height:0;width:0;Border:0px Solid black;z-index:0;Position:Absolute;overflow:hidden;'>");
    write("	<IFrame Scrolling='No' Width='300' Height='700px;' Style='Position:Absolute;' FrameBorder='1' Src='About:Blank'></IFrame>");
    write("</Div>");
    //#endregion

    write("<Div id='VicPopCal' style='z-Index:10;POSITION:absolute;VISIBILITY:hidden;border:1px ridge;width:200;height:120;background-image: url(/cms/cmsweb/Images/tile.gif)'>");
    write("<table border='0' width='80%'>");
    write("<TR class='tableHeader'><TH class='headereffect'>");
    //write("<p style='text-align:right'><B style='cursor:hand;font-family: verdana, arial, MS Sans Serif; font-size:" + sDateTextSize + ";color:"+ gcToday +"' onclick='fSetDate(giYear,giMonth,giDay)'>Today:" + dispDate + "</B>");
    write("<p style='text-align:right'><B style='cursor:hand;font-family: verdana, arial, MS Sans Serif; font-size:" + sDateTextSize + ";color:" + gcToday + "' onclick='fSetDate(giYear,giMonth,giDay)'><div id=dtarea style='POSITION:relative;border:0px ridge;width:210;height:50></div></B>");
    //write("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img Src='../Images/cross.gif' Border='0' onclick='hideLayer()' alt='Click here to close.'>");
    write("<img Src='/cms/cmsweb/Images/cross.gif' Border='0' onclick='hideLayer()' alt='Click here to close.'>");
    write("</p></TH><TR>");
    write("<td valign='center' align='center'><input type='button' name='PrevMonth' class='clsButton' value='<' style='height:20;width:15;FONT:bold;align:center' onClick='fPrevMonth()'>");
    write("&nbsp;<SELECT name='tbSelYear' class='INPUT' onChange='fUpdateCal(tbSelYear.value, tbSelMonth.value)' Victor='Won'>");
    var CalenderStartYear = 1900;
    for (i = parseInt(CalenderStartYear); i < 2020; i++)//2014
        write("<OPTION value='" + i + "'>" + i + "</OPTION>");
    write("</SELECT>");
    write("&nbsp;<select name='tbSelMonth' class='INPUT' onChange='fUpdateCal(tbSelYear.value, tbSelMonth.value)' Victor='Won'>");
    for (i = 0; i < 12; i++)
        write("<option value='" + (i + 1) + "'>" + gMonths[i] + "</option>");
    write("</SELECT>");
    write("&nbsp;<input type='button' name='PrevMonth' value='>' class='clsButton' style='height:20;width:15;FONT:bold;align:center' onclick='fNextMonth()'>");
    write("</td>");
    write("</TR><TR>");
    write("<td align='center'>");
    write("<DIV><table width='100%' border='0'>");
    fDrawCal(giYear, giMonth);
    write("</table></DIV>");
    write("</td>");
    write("</TR>");
    //write("<TR><TD align='center'>");
    //write("<B style='cursor:hand;font-family: verdana, arial, MS Sans Serif; font-size:" + sDateTextSize + ";color:"+ gcToday +"' onclick='fSetDate(giYear,giMonth,giDay)' onMouseOver='this.style.color=gccol' onMouseOut='this.style.color=0'>Today: " + dispDate + "</B>");
    //write("</TD></TR>");
    //write("<TR><td align='center'>");
    //write("<input type='button' name='btnClose' value='Close' class='clsButton' onclick='hideLayer()'>");
    //write("</TD></TR>");
    write("</TABLE></Div>");
    write("</html>");

}

	function InitializeCover()
	{
			divCover.style.visibility = 'visible';
			divCover.style.width	  = VicPopCal.offsetWidth;
			divCover.style.top		  = VicPopCal.offsetTop;
			divCover.style.left		  = VicPopCal.offsetLeft;
			divCover.style.height	  = VicPopCal.offsetHeight;
	}
	function DisposeCover(){
			divCover.style.visibility = 'Hidden';
			divCover.style.width = '0px';
			divCover.style.top = '0px';
			divCover.style.left = '0px';
			divCover.style.height = '0px';
	}
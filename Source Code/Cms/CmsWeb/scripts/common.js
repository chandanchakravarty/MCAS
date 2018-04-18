/*
Added by Charles for Multilingual Support
iLangID = 1 : en-US
iLangID = 2 : pt-BR

sCultureDateFormat 
DD/MM/YYYY
MM/DD/YYYY

Multilingual Support Added for Currency, Number --Charles (4-Jun-2010)
sCurrencyFormat = 'R$' (Brazil), '$' (US)
sDecimalSep = ',' (Brazil), '.' (US)
sGroupSep = '.' (Brazil), ',' (US)
*/

var conXML;
var strXML;		
var gSessionVal;
var searchClick=-1;
var doubleClick=-1;
var imageClick=-1;
var gridTimer;
var operation="";
//var mainSearch="";
//var rowPrimary="";		
//var show=1;

//var headRow=-1;
//var srhCrt="";
strXML="";
function ShowPanel()
{
	// set initial gridpanel top and height
	if(document.getElementById('gridpanel'))
	{
		document.getElementById('gridpanel').style.top		= '5px';
		document.getElementById('gridpanel').style.height	= '324px';
	}
	// set initial tab's iframe width and height
	if(document.getElementById('tabLayer'))
	{
		document.getElementById('tabLayer').width			= '100%';
		document.getElementById('tabLayer').height			= '400px';
	}
	
	document.getElementById("pnlGrid").style.visibility="visible"; 
	if(document.getElementById('tabContent'))
	document.getElementById('tabContent').style.visibility="hidden";
	if(document.getElementById('footertext'))
	document.getElementById('footertext').style.top = parseInt(document.getElementById('gridpanel').style.height)+10;
	showScroll();
}
function showScroll()
{
	if(document.getElementById('bodyHeight'))
	{
	document.getElementById('bodyHeight').style.height	=	document.body.offsetHeight -20;
	document.getElementById('bodyHeight').style.width	=	document.body.offsetWidth;//1024;
	}
	//document.getElementById('bodyHeight').style.height = screen.height - 240;
	//document.getElementById('bodyHeight').style.width = screen.width - 12;
	
}
/****************** SelectDropdownOption ****************
Description:  Is used to select an option of a combobox.
Parameters :  comobobox, selectedValue value to be selected in combo box.
Added By   :  Shafi
Date       :  11/05/06
*/	
function SelectDropdownOptionByValue(combo,selectedValue)
{
       	for(var j=0; j<combo.options.length; j++)
		{
		if(selectedValue == combo.options[j].value)
			{
			  	combo.options.selectedIndex = j;			  	
				break;
			}
		}
}

//Function added by Charles on 8-Apr-10 for Multilingual Popup
function ShowMultiLingualPopup(PRIMARY_COLUMN, PRIMARY_ID, MASTER_TABLE_NAME, CHILD_TABLE_NAME, DESCRIPTION_COLUMN) {

    if (isNaN(PRIMARY_ID)) {
        PRIMARY_ID = 0;
    }
    var url = "../../cmsweb/aspx/MultilingualSupport.aspx?PRIMARY_COLUMN=" + PRIMARY_COLUMN + "&PRIMARY_ID=" + PRIMARY_ID + "&MASTER_TABLE_NAME=" + MASTER_TABLE_NAME + "&CHILD_TABLE_NAME=" + CHILD_TABLE_NAME + "&DESCRIPTION_COLUMN=" + DESCRIPTION_COLUMN;
    var nuWin = window.open(url, 'MultilingualSupport', 'menubar=no,toolbar=no,location=no,resizable=no,scrollbars=no,status=no,width=500,height=500');
}

function SelectDropdownOptionByValueWithReturn(combo,selectedValue)
{
       	for(var j=0; j<combo.options.length; j++)
		{
		if(selectedValue == combo.options[j].value)
			{
			  	combo.options.selectedIndex = j;
			  	return 1;
				break;
			}
		}
	return 2	
}
function setTabContentPosition(Mode) // Added by Rajan for setting the lower tab position as per the screen resolution.
{
		// Mode can be of two types 
		// Normal 
		//Diary where calander is placed in left.
		
		var scrWidth = window.screen.width;
		var leftPosition ;
		if (Mode == 'Diary')
		{
			leftPosition = 0;
		}
		else
		{
			leftPosition = 188;
		}
		
		if(scrWidth > 800 && scrWidth < 1000 )
		{
			tabContent.style.left = (188 - leftPosition);
		}
		
		if(scrWidth > 1000 && scrWidth < 1280 )
		{
			tabContent.style.left = (188 - leftPosition);
		}		
		
		if(scrWidth >= 1280 )
		{
			tabContent.style.left = (316 - leftPosition)
		}
}

function rowClicked()
{
	strXML="";			
	
	if (typeof(gridFrameIndex) == "undefined")
	{
		var gridFrameIndex = 1;
	}
	
	strXML = document.gridObject.getRowData();
	//alert(strXML);
	document.getElementById('gridpanel').style.visibility='visible';	 
	adjustHeight(document.gridObject.height);
	document.getElementById('tabLayer').style.top=22;
	document.getElementById('footertext').style.top = parseInt(document.getElementById('tabLayer').style.top)+parseInt(document.getElementById('tabLayer').height);
	
	var objXmlHandler = new XMLHandler();
	
	if(strXML!="")
	{
		var tree = (objXmlHandler.quickParseXML(strXML).getElementsByTagName('Table')[0]);
		document.getElementById('tabContent').style.visibility="visible";  
		changeTab(0,0);	
	}
}

function toggledClicked(gridHeight)
{
	document.getElementById('gridpanel').style.top='5';
	if (document.getElementById('gridpanel').className=='hide')
	{
		document.getElementById('gridpanel').className='show'
		document.getElementById('gridpanel').style.height=gridHeight;
	}	
	else
	{
		document.getElementById('gridpanel').className='hide';		
		document.getElementById('gridpanel').style.visibility='visible';
	}
	document.getElementById('tabContent').style.top =parseInt(document.getElementById('gridpanel').style.top) + parseInt(document.getElementById('gridpanel').style.height) + 10;
	document.getElementById('footertext').style.top = parseInt(document.getElementById('tabContent').style.top) + parseInt(document.getElementById('tabLayer').height)+30;
}

function hideTab(gridHeight)
{
	//alert('inside')
	document.getElementById('gridpanel').style.height	= gridHeight;
	ShowPanel();
}

function adjustHeight(gridHeight)
{
	if(document.getElementById('gridpanel')&& document.getElementById('tabContent'))
	{
		document.getElementById('gridpanel').style.height	= gridHeight;
		document.getElementById('tabContent').style.top =parseInt(document.getElementById('gridpanel').style.top) + parseInt(document.getElementById('gridpanel').style.height) + 10;
		
		if (document.getElementById('tabLayer') != null)
		{
			if (document.getElementById('tabLayer').height != "")
			{
				document.getElementById('footertext').style.top = parseInt(document.getElementById('tabContent').style.top) + parseInt(document.getElementById('tabLayer').height)+30;
			}
			else
			{
				document.getElementById('footertext').style.top = parseInt(document.getElementById('tabContent').style.top) + 30;
			}
		}
	}
}

function addNew()
{
	strXML="";
			
	document.getElementById('gridpanel').className='hide';
	document.getElementById('gridpanel').style.height='18';
	document.getElementById('tabContent').style.visibility="visible";  
	document.getElementById('tabContent').style.top = 32;
	document.getElementById('tabLayer').style.top=32; 
	
	var top=0;
	var height=0;
	
	if (document.getElementById('tabLayer').style.top != "")
		top = parseInt(document.getElementById('tabLayer').style.top);
	
	if (document.getElementById('tabLayer').height != "")
		height = parseInt(document.getElementById('tabLayer').height);			
		
	document.getElementById('footertext').style.top = top + height;
	changeTab(0,0);	
}
//////////////by pravesh
function VoidChecks()
{
	if (document.getElementById("hidCheckedRowIDs") == null)
	{	
		var delStr=" ( ";		
		
		for(i=1;i<=iPs;i++)		
		{
		    //var ctrl=eval("document.getElementById('ck_" + i + "')");
		    //Added by Charles on 2-Jun-10 for Itrack 37
		    if ((i + 2) < 10) {
		        var ctrl = eval("document.getElementById('ctl0" + (i + 2) + "_ck_" + i + "')");
		    }
		    else {
		        var ctrl = eval("document.getElementById('ctl" + (i + 2) + "_ck_" + i + "')");
		    }	
			var rowTag=eval("document.getElementById('Row_" + i + "')");
			if(ctrl)						
				if(ctrl.checked)
				{				
					delStr=delStr + rowTag.uniqueID + " OR ";					
				}				
		}		

		if(delStr!=" ( ")
		{
			delStr=delStr.substring (0,delStr.length-3);
			delStr = delStr + ")";			
		}
				
		document.getElementById('hidDelString').value=delStr;
		//alert(document.getElementById('hidDelString').value)
		if (delStr != " ( ")
		    document.indexForm.submit();
		else {
		    if (iLangID == 2) {
		        alert("Nenhum registro selecionado.")
		    }
		    else {
		        alert("No record selected.")
		    }
		}		
	}
	else
	{
		var delStr="";		
		
		for(i=1;i<=iPs;i++)		
		{
		    //var ctrl=eval("document.getElementById('ck_" + i + "')");
		    //Added by Charles on 2-Jun-10 for Itrack 37
		    if ((i + 2) < 10) {
		        var ctrl = eval("document.getElementById('ctl0" + (i + 2) + "_ck_" + i + "')");
		    }
		    else {
		        var ctrl = eval("document.getElementById('ctl" + (i + 2) + "_ck_" + i + "')");
		    }	
			var rowTag=eval("document.getElementById('Row_" + i + "')");
			
			if(ctrl)
			{
				if(ctrl.checked)
				{
					var ColVal= new String;
					ColVal = rowTag.uniqueID;
					index = ColVal.indexOf("=",0);
					ColVal = ColVal.substring(index+1, ColVal.length);
					
					delStr=delStr + ColVal + " ~ ";					
				}
			}				
		}		

		if(delStr!=" ( ")
		{
			delStr=delStr.substring (0,delStr.length-3);
		}
				
		document.getElementById('hidCheckedRowIDs').value=delStr;
		if (delStr != "")
		    document.indexForm.submit();
		else {
		    if (iLangID == 2) {
		        alert("Por favor, selecione os registros.")
		    }
		    else {
		        alert("Please select the records.")
		    }
		}
	}
}
///end here

// These function will be removed if web service approach of save and update has to go
function SaveData(saveXML,what)
{
	document.indexForm.hide.value = saveXML;
	conXML = document.indexForm.hide.value;
	var lstr = "../webservices/wscmsweb.asmx?WSDL";
	myTSMain.useService(lstr.toString(), "TSM");
	
	if(what==1)
	myTSMain.TSM.callService(SaveResult, "CompareData", saveXML);
	else
	myTSMain.TSM.callService(SaveResult, "SaveData", saveXML);
}

function SaveResult(xml)
{
	if (xml.error) {

	    if (iLangID == 2) {
	        alert("chamada sem " + String.fromCharCode(234) + "xito. O erro " + String.fromCharCode(233) + " /n" + xml.errorDetail.string);
	    }
	    else {
	        alert("Unsuccessful call. Error is /n" + xml.errorDetail.string);
	    }
		return;
	}
	else
	{
		if(xml.value=="yes") {

		    if (iLangID == 2) {
		        alert("Dados salvos com sucesso.");
		    }
		    else {
		        alert("Data saved successfully.");
		    }
			if(document.gridObject)
				document.gridObject.refreshcompletegrid();
			else
			{
				refreshGrid("");
				if(prmId!="")				
				{
					var rrid="Row_" + prmId;
					selectROWID(rrid)
				}
			}
		}
		else {

		    if (iLangID == 2) {
		        alert("Simultaneidade encontrado.");
		    }
		    else {
		        alert("Concurrency found.");
		    }
			var FinalXML = xml.value;
			document.indexForm.hide.value = FinalXML;
			window.open("conpopup.aspx");
		}
	}
}

/******************************** FOR WEBGRID SECTION *******************************************************/
var firstTime;

function HideTab() {
 
	if ( document.getElementById("tabLayer") )
	{
	    document.getElementById("tabLayer").style.visibility = "hidden"
	    //Added By Pradeep Kushwaha on 21-03-2011 to Implement Rich Textbok  on Clauses page -Itrack 581
	    if (document.getElementById("tabLayer").contentWindow.document.getElementById("RichHtmlArea") != null) {
	        document.getElementById("tabLayer").contentWindow.document.getElementById("RichHtmlArea").style.display = "none"
	    }
	    
	    //Added till here 
	}

	
	
	if(document.getElementById("tabCtlRow"))
	document.getElementById("tabCtlRow").style.visibility="hidden" 
}

function ShowTab()
{
	if ( document.getElementById("tabLayer") )
	document.getElementById("tabLayer").style.visibility="visible" 
	
	if(document.getElementById("tabCtlRow"))
	document.getElementById("tabCtlRow").style.visibility="visible" 
}
// Added by swarup for notes 
function SingleCombinedText()
{
	if (document.getElementById("hidCheckedRowIDs") == null)
	{
		var delStr="";		
		
		for(i=1;i<=iPs;i++)		
		{
		    //var ctrl=eval("document.getElementById('ck_" + i + "')");
		    //Added by Charles on 2-Jun-10 for Itrack 37
		    if ((i + 2) < 10) {
		        var ctrl = eval("document.getElementById('ctl0" + (i + 2) + "_ck_" + i + "')");
		    }
		    else {
		        var ctrl = eval("document.getElementById('ctl" + (i + 2) + "_ck_" + i + "')");
		    }	
			var rowTag=eval("document.getElementById('Row_" + i + "')");
			if(ctrl)						
				if(ctrl.checked)
				{	
					var ColVal1= new String;
					ColVal1 = rowTag.uniqueID;
					index = ColVal1.indexOf("=",0);
					ColVal1 = ColVal1.substring(index+1, ColVal1.length);			
					delStr=delStr + ColVal1 + "','";				
				}				
		}
		delStr=delStr.substring (0,delStr.length-3);
		document.getElementById('hidDelString').value=delStr;
		if (delStr != "")
		    document.indexForm.submit();
		else {
		    if (iLangID == 2) {
		        alert("Nenhum registro selecionado.");
		    }
		    else {
		        alert("No record selected.");
		    }
		}		
	}
	else
	{
		var delStr="";		
		for(i=1;i<=iPs;i++)		
		{
		    //var ctrl=eval("document.getElementById('ck_" + i + "')");
		    //Added by Charles on 2-Jun-10 for Itrack 37
		    if ((i + 2) < 10) {
		        var ctrl = eval("document.getElementById('ctl0" + (i + 2) + "_ck_" + i + "')");
		    }
		    else {
		        var ctrl = eval("document.getElementById('ctl" + (i + 2) + "_ck_" + i + "')");
		    }	
			var rowTag=eval("document.getElementById('Row_" + i + "')");
			
			if(ctrl)
			{
				if(ctrl.checked)
				{
					var ColVal= new String;
					ColVal = rowTag.uniqueID;
					index = ColVal.indexOf("=",0);
					ColVal = ColVal.substring(index+1, ColVal.length);
					
					delStr=delStr + ColVal + " ~ ";					
				}
			}				
		}		

		if(delStr!=" ( ")
		{
			delStr=delStr.substring (0,delStr.length-3);
		}
				
		document.getElementById('hidCheckedRowIDs').value=delStr;
		if (delStr != "")
		    document.indexForm.submit();
		else {
		    if (iLangID == 2) {
		        alert("Por favor, selecione os registros.")
		    }
		    else {
		        alert("Please select the records.")
		    }
		}
	}	
	}

function DeleteRecords()
{
	if (document.getElementById("hidCheckedRowIDs") == null)
	{	
		var delStr=" ( ";		
		
		for(i=1;i<=iPs;i++)		
		{
		    //var ctrl=eval("document.getElementById('ck_" + i + "')");
		    //Added by Charles on 2-Jun-10 for Itrack 37
		    if ((i + 2) < 10) {
		        var ctrl = eval("document.getElementById('ctl0" + (i + 2) + "_ck_" + i + "')");
		    }
		    else {
		        var ctrl = eval("document.getElementById('ctl" + (i + 2) + "_ck_" + i + "')");
		    }
		    
			var rowTag=eval("document.getElementById('Row_" + i + "')");
			if(ctrl)						
				if(ctrl.checked)
				{				
					delStr=delStr + rowTag.uniqueID + " OR ";					
				}				
		}		

		if(delStr!=" ( ")
		{
			delStr=delStr.substring (0,delStr.length-3);
			delStr = delStr + ")";			
		}
				
		document.getElementById('hidDelString').value=delStr;
		//alert(document.getElementById('hidDelString').value)
		if (delStr != " ( ")
		    document.indexForm.submit();
		else {
		    if (iLangID == 2) {
		        alert("Nenhum registro selecionado.")
		    }
		    else {
		        alert("No record selected.")
		    }
		}		
	}
	else
	{
		var delStr="";		
		
		for(i=1;i<=iPs;i++)		
		{
		    //var ctrl = eval("document.getElementById('ck_" + i + "')");
		    //Added by Charles on 2-Jun-10 for Itrack 37
		    if ((i + 2) < 10) {
		        var ctrl = eval("document.getElementById('ctl0" + (i + 2) + "_ck_" + i + "')");
		    }
		    else {
		        var ctrl = eval("document.getElementById('ctl" + (i + 2) + "_ck_" + i + "')");
		    }
			
			var rowTag=eval("document.getElementById('Row_" + i + "')");
			
			if(ctrl)
			{
				if(ctrl.checked)
				{
					var ColVal= new String;
					ColVal = rowTag.uniqueID;
					index = ColVal.indexOf("=",0);
					ColVal = ColVal.substring(index+1, ColVal.length);
					
					delStr=delStr + ColVal + " ~ ";					
				}
			}				
		}		

		if(delStr!=" ( ")
		{
			delStr=delStr.substring (0,delStr.length-3);
		}
				
		document.getElementById('hidCheckedRowIDs').value=delStr;
		if (delStr != "")
		    document.indexForm.submit();
		else {
		    if (iLangID == 2) {
		        alert("Por favor, selecione os registros.")
		    }
		    else {
		        alert("Please select the records.")
		    }
		}
	}
}

function HideParentTab()
{	
	if ( parent.document.getElementById("tabLayer") )
	{
		parent.document.getElementById("tabLayer").style.visibility="hidden" 
	}
	
	if(parent.document.getElementById("tabCtlRow"))
	parent.document.getElementById("tabCtlRow").style.visibility="hidden" 
}

function ShowParentTab()
{
	if ( parent.document.getElementById("tabLayer") )
	parent.document.getElementById("tabLayer").style.visibility="visible" 
	
	if(parent.document.getElementById("tabCtlRow"))
	parent.document.getElementById("tabCtlRow").style.visibility="visible" 
}

		function FormatPhoneForGrid(strObj)
		{
			var szFieldName = event.srcElement.id;
			var szFormName = document.forms[0].name;
			var szPhoneNo = eval(szFormName + "." + szFieldName).value;
			var szStrippedPhone; 
			var strLength;
			var strCharAtPos
			
			ObjVal =strObj;
			szPhoneNo = ObjVal.value
					
			szPhoneNo = TrimTheString(szPhoneNo) // store the phone no after triming
			strLength = szPhoneNo.length; //store the length of the phone no.
			strCharAtPos = "";			
			
			//modified by shafi on 25/11/2005			
			if(TrimTheString(szPhoneNo) != "")
			{
				{
					szPhoneNo = TrimTheString(szPhoneNo)
					strLength = szPhoneNo.length;
					szPhoneNo = StripDashAndBracket(szPhoneNo);					
					if((szPhoneNo.length > 10 || szPhoneNo.length < 10))
					{					 
						//alert("The length of phone number is not appropriate.");
						return ObjVal.value;
					}
					else
					{
						var FirstPart = szPhoneNo.substr(0,3);
						var SecondPart = szPhoneNo.substr(3,3);
						var ThirdPart = szPhoneNo.substr(6,4);
						if (!(isNaN(FirstPart) || isNaN(SecondPart) || isNaN(ThirdPart)))
						{	
							FullPhone = "(" + FirstPart + ")" + SecondPart + "-" + ThirdPart;
							eval('document.'+szFormName+'.'+szFieldName).value=FullPhone;
							//DisableValidatorsById(szFieldName);										
							//alert()
							ObjVal.value=FullPhone;
							return ObjVal.value;
						}
						else {
						    if (iLangID == 2) {
						        alert("Por favor, digite apenas d" + String.fromCharCode(237) + "gitos. ")
						    }
						    else {
						        alert("Please enter only digits. ")
						    }
							return "";
						}
					}					
				}
			}
			else {

			    if (iLangID == 2) {
			        alert("Por favor, indique o n" + String.fromCharCode(250) + "mero de telefone.");
			    }
			    else {
			        alert("Please enter phone number.");
			    }
				return "";			
			}
			//ValidatorOnChange(); 
		}

/*******************************************************************
'*	Function Name	:	FormatDate() 
'*	Parameters		:	szValue
'*	Author			:	Anurag Verma
'*	Purpose			:   To convert the entered date format ###### or ######## 
'*						into format ##/##/####.
'*	Creation Date	:   20 June,2005
'*******************************************************************/
	function FormatDateForGrid(strObj,strDateVal)
	{
		var strDate;
		var strDay;
		var strMonth;
		var strYear;
		var objVal;
		var dtDSep;
		//Nov 09,2005:Sumit Chhabra:Commented as it is not being used in the given function
		//var szFieldName = event.srcElement.id;
		//var szFormName = document.forms[0].name;

		ObjVal =strObj;
		dtDateValue =ObjVal.value;
		if (dtDateValue=="")
		{
//			ValidatorOnChange(); 
			return "";
		}

	   	dtDSep = GetDateSeparator(dtDateValue);

	   	if(dtDSep == "")
	   	{
	   		dtDSep = "/";
	   	}
	   	else if(dtDSep == " ")
	   	{
	   		dtDSep = "/";
	   		dtDateValue = ReplaceString(dtDateValue," ", "/");
	   	}
		
		if (IsProperDate(ObjVal)==true)
		{
			// After coming out of 'IsProperDate' function if the date value contains '/' then 
			// it means that the value is of correct format. So this value is returned
			if (FindChar(dtDateValue,dtDSep)==true)
			{
				strDate = dtDateValue;
			}
			else if(dtDateValue.length == 8)
			{
			    //Added for Multilingual Support
			    if (sCultureDateFormat == 'DD/MM/YYYY') {
			        strDay = dtDateValue.charAt(0) + dtDateValue.charAt(1);
			        strMonth = dtDateValue.charAt(2) + dtDateValue.charAt(3);
			        strYear = dtDateValue.charAt(4) + dtDateValue.charAt(5) + dtDateValue.charAt(6) + dtDateValue.charAt(7);
			        strDate = strDay + dtDSep + strMonth + dtDSep + strYear;			        
			    }
			    else {
			        strMonth = dtDateValue.charAt(0) + dtDateValue.charAt(1);
			        strDay = dtDateValue.charAt(2) + dtDateValue.charAt(3);
			        strYear = dtDateValue.charAt(4) + dtDateValue.charAt(5) + dtDateValue.charAt(6) + dtDateValue.charAt(7);
			        strDate = strMonth + dtDSep + strDay + dtDSep + strYear;
			    }
			}
			else if (dtDateValue.length == 6) {

			//Added for Multilingual Support
			if (sCultureDateFormat == 'DD/MM/YYYY') {
			        strDay = "0" + dtDateValue.charAt(0);
			        strMonth = "0" + dtDateValue.charAt(1);
			        strYear = dtDateValue.charAt(2) + dtDateValue.charAt(3) + dtDateValue.charAt(4) + dtDateValue.charAt(5);
			        strDate = strDay + dtDSep + strMonth + dtDSep + strYear;			        
			    }
			    else {
			        strMonth = "0" + dtDateValue.charAt(0);
			        strDay = "0" + dtDateValue.charAt(1);
			        strYear = dtDateValue.charAt(2) + dtDateValue.charAt(3) + dtDateValue.charAt(4) + dtDateValue.charAt(5);
			        strDate = strMonth + dtDSep + strDay + dtDSep + strYear;
			    }
			}			
			ObjVal.value=strDate;
			//DisableValidatorsById(szFieldName);
			//ValidatorOnChange(); 
			return ObjVal.value ;
		}
		else {  
		
		    //ValidatorOnChange(); 
			return "";
		}		
	}

function setfirstTime()
{
	//alert('sds')		
	if(typeof(defaultMode) == "undefined")
		showScroll();
	else
	{
		if(defaultMode)//defaultMode varibale is set by code behind
			showScroll();
	}
	
	firstTime=1;
	//setTabContentPosition('Normal');
	//tabContent.style.display="none";
}

function RefreshWebgrid(op)
{
	refreshGrid("",op);			
	operation=op;				
}

function col_exp()
{
	if(imageClick==-1)
	{
		if(searchClick==1)
		{
			if(doubleClick==-1)
			{						
				populateXML(globSelected,msDg);	
				string="toggleImage(HImagePath)";
				setTimeout(string,100);
				doubleClick=1
			}
			else
			{	
				doubleClick=-1			
				initSearch(stPageNo,advanceSearch)
				selectROWID("Row_" + globSelected);
				string="toggleImage(ImagePath)";
				setTimeout(string,100);
			}
		}				
	}
	else if(imageClick==1)
	{
		initSearch(stPageNo,advanceSearch)	
		selectROWID("Row_" + globSelected);
		string="toggleImage(ImagePath)";
		setTimeout(string,100);
		doubleClick=-1	
		imageClick=-1
	}			
}

function toggleImage(str)
{
	document.all('imgCollapse').src=str
}	
	
function populateXMLafterSave(num,msDg)
{
	var tempXML;			
	var tmpTree="";
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
		
		var tree1 = (objXmlHandler.quickParseXML(tempXML).getElementsByTagName('temp'));
		var tree = (objXmlHandler.quickParseXML(tempXML).getElementsByTagName('temp')[rowID-1]);
		var str=new String();									
		var strSingleRow="";
		var fetch=fetchColumns.split("^");		
		
		if(tree1)
		{
			var totalClone=tree1.length;
			for(i=0;i<tree1.length;i++)
			{
				
				for(j=0;j<tree1[i].childNodes.length;j++)
				{
					if(j==0)
					{
						if(tree1[i].childNodes[j].firstChild)
						{
							if(tree1[i].childNodes[j].firstChild.text==operation)
							{
								rowID=i+1;
								break;
							}
						}									
					}				
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
					
					if(tree)					
					for(j=0;j<tree.childNodes.length;j++)
					{
						if(tree.childNodes[j].nodeName==col)
						{
							if(tree.childNodes[j].firstChild)
							{
								locQueryStr += tree.childNodes[j].firstChild.text + "&";
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
				if(tree)
				{	
					for(i=0;i<fetch.length;i++)			
					{
						var col=fetch[i];
						if(i<tree.childNodes.length)
						{
							if(tree.childNodes[col-1].firstChild)
							{
								strXML += "<" + tree.childNodes[col-1].nodeName + ">";
								strXML += tree.childNodes[col-1].firstChild.text ;
								strXML += "</" + tree.childNodes[col-1].nodeName + ">";
							}
						}		
					}
				}	
				
				strXML+= "</Table>";
				strXML+= "</NewDataSet>";					
			}			
		}
	
		//alert(document.getElementById('hidOldData'));
		//parent.document.getElementById('hidOldData').value	= strXML;	
		
		//parent.frames[1].frames[0].document.getElementById('hidOldData').value	= strXML;
	}		

	document.getElementById('hidlocQueryStr').value = locQueryStr ; 
	if(operation=="")
	{
		var rrid="Row_" + rowID;
		selectROWID(rrid)
	}				
}

function addRecord() {	
 
	if(document.getElementById('hidlocQueryStr'))
		document.getElementById('hidlocQueryStr').value="Add";

	if(document.getElementById('hidlocQueryStr'))
	{
		document.getElementById('hidlocQueryStr').value = '';
	}
		
	addNew = true; // addNew variable though is declared in basegrid.ascx but is set here as a status of adding new record. 
	
	//Setting the selected record xml to blank
	strSelectedRecordXML = "";
	show = 2;
	if (browser.isIE)
	    TabFrameDocument = document.frames("tabLayer").document;
	else
	    TabFrameDocument = document.getElementById("tabLayer").contentDocument; 
	if (document.getElementById("tabLayer").style.visibility == "visible" )
	{
	    if (TabFrameDocument.getElementById("btnSave") == null)
		{
			LoadPage();
			return;
		}

		if (TabFrameDocument.getElementById("hidFormSaved") != null)
		{
		    if (!(TabFrameDocument.getElementById("hidFormSaved").value == "0" || TabFrameDocument.getElementById("hidFormSaved").value == "1"))
			{
				LoadPage();
				return;
			}
		}
		
		//Prompting the user for saving the existing record
		userResponse = UserConfirmation(); //getUserConfirmation();
		
		if (userResponse==6)	//6 = yes
		{	
			//User wants to save the record hence
			//Post backing the page
			PostbackPageForSave();
		}
		else if(userResponse==7) //7=no
		{
			//Opening the new page
			LoadPage();
		}
		else if(userResponse==2) //2=cancel
		{
			//Opening the new page
			return;
		}
	}
	else
	{
		//Opening the new page		
		LoadPage();
	}	
}	
function PostbackPageForSave()
{
    if (browser.isIE) {
        TablFrame = document.frames("tabLayer");
        TablFrameDocm = TablFrame.document;
    }
    else {
        TablFrame = document.getElementById("tabLayer");
        TablFrameDocm = TablFrame.contentDocument;
    }
    if (TablFrame.Page_ClientValidate)
        validCilent = TablFrame.Page_ClientValidate();
	else
		validCilent = true;
			 
	if(validCilent)
	{
		strSelectedRecordXML == "-1";
		/*if (TablFrame.__doPostBack)
		    TablFrame.__doPostBack('btnSave', '');
		 */
		btnSave = TablFrameDocm.getElementById('btnSave');
		if (btnSave) {
		    //DisableButtonOnClick(btnSave);
		   TablFrame.__doPostBack(btnSave.id, '');//Modified by Pradeep Kushwaha on 30-Sep-2011 to call Save event-itrack-1023
		}
		//Calling the load page methid, which will open the new page	
		setTimeout('LoadPage(true);', 100);
		//LoadPage(true);
		return ; 
	}
	else {   
	    /*Some validators are fired*/				
		return ;
	}
}

function LoadPage(CheckSave, NoOfAttempt) {
    
	if (browser.isIE)
	    tabFramDoc = document.frames("tabLayer").document;
	else
	    tabFramDoc = document.getElementById("tabLayer").contentDocument;

	if (tabFramDoc.readyState == 'loading')
	{
		setTimeout('LoadPage(' + CheckSave + ',0)',200);
		return;
	}	
	
	if (CheckSave == true)
	{
		if ((strSelectedRecordXML == "-1" || strSelectedRecordXML == "") && (NoOfAttempt < 4))
		{
			//Init
			if (NoOfAttempt == null)
				NoOfAttempt = 0;
			
			//Incrae	
			NoOfAttempt++;
			
			setTimeout('LoadPage(' + CheckSave + ',' + NoOfAttempt + ')',200);
			return;
		}
		
		//Checking whether data saved or not
		var hidFormSaved = tabFramDoc.getElementById('hidFormSaved');
		
		if(hidFormSaved)
		{
			if (hidFormSaved.value != "1")
				return false;
		}		 
	}
	
	if(document.getElementById('hidlocQueryStr'))
	{
		document.getElementById('hidlocQueryStr').value = '';
	}
	if(typeof(msDg) != "undefined")
		onRowClicked(0,msDg);
	
	var strExp = 'ShowTab();strSelectedRecordXML = "";if (window.SetTabs) ' 
	+ '{'
	+ '	window.SetTabs(0,true);'
	+ '};';
	
	setTimeout(strExp,100);
}
			
/******************************************************************************************************************/
function CheckSession()
{
	//alert(gSessionVal);	
	if(gSessionVal)
	{
		if(gSessionVal=="")
		{
			//alert(top.location)
			top.location = "login.aspx";			
		}			
	}
//alert(<%=Session("UserName")%>);
	/*var sesVar=<%=Session("UserName")%>;
	if(sesVar)
	{
	alert(sesVar);
		if(sesVar.toString()=="")
		{
		alert(sesVar);
			//window.top.close();
			//window.top.open("login.aspx");  	
			top.location = "login.aspx";
			alert(sesVar);
		}
	
	}*/
}

/****************** SelectComboOption ****************
Description: Ised to select an option of a combobox.
Parameters:  comboId:- javascript Id of comobobox, selectedValue value to be selected in combo box.
Note: value must be parsed to numeric beforehand if values of combo are numeric.
*/
function SelectComboOption(comboId,selectedValue)
{
	for(var j=0; j<document.getElementById(comboId).options.length; j++)
	{
	if(selectedValue == document.getElementById(comboId).options[j].value)
		{
			document.getElementById(comboId).options.selectedIndex = j;
			DisableValidatorsById(comboId);
			break;
		}
	}
}

function SelectComboOptionByText(comboId,selectedValue)
{
	for(var j=0; j<document.getElementById(comboId).options.length; j++)
	{
	if(selectedValue == document.getElementById(comboId).options[j].text)
		{
			document.getElementById(comboId).options.selectedIndex = j;
			DisableValidatorsById(comboId);
			break;
		}
	}
}

function SelectComboIndex(objIndex)
{
		if(document.getElementById(objIndex).options.length > 0)
		{
			if(document.getElementById(objIndex).selectedIndex < 0)
			{
				document.getElementById(objIndex).options.selectedIndex = 0;
				DisableValidatorsById(objIndex);
			}
		}
}

/****************** AddComboOption ****************
Description: used to add an option of a combobox.
Parameters:  comboId:- Id of comobobox, optionText-the text value of the option to be added.
optionValue- value of the option to be added.
*/
function AddComboOption(comboId,optionText,optionValue)
{
	var j;
	if(document.getElementById(comboId))
	{
		if(document.getElementById(comboId).options)
			j = document.getElementById(comboId).options.length;
		else
			j = 0;
		document.getElementById(comboId).options[j] = new Option(optionValue,optionText);
	}
}

function DisableExt( objPhone, objExtension)
{	
	if (document.getElementById(objPhone).value == "")
	{
		document.getElementById(objExtension).setAttribute("readOnly",true);
		document.getElementById(objExtension).value = "";
	}
	else
	{
		document.getElementById(objExtension).setAttribute("readOnly",false);
	}
}

// Functions for Currency Format.
function getActualValue(num)
{
	//var strVal	 = window.event.srcElement.value;
	var strVal = num;
	var unitChar = trim(strVal.toString().substr(strVal.length-1,1));
	var numVal   = trim(strVal.toString().substr(0, strVal.toString().length-1));
	if((isNaN(numVal)) || (numVal==''))
	return strVal;

	switch(unitChar.toLowerCase())
	{
		/*case 'k':
			strVal = parseFloat(numVal) * 1000;
			break;
		case 'm':
			strVal = parseFloat(numVal) * 1000000;
				break;*/
		default:
			strVal = strVal;
		break;
	}	
	return strVal;
}


/*************************************************************************/
//Added by Anurag Verma on 02/05/2005
		function DateComparer(DateFirst, DateSec, FormatOfComparision) {
		   
			var saperator = '/';
			var firstDate, secDate;
            
			var strDateFirst = DateFirst.split("/");
			var strDateSec = DateSec.split("/");

			if(FormatOfComparision.toLowerCase() == "dd/mm/yyyy")
			{
			    //alert("dd/mm/yyyy")
			    if (strDateFirst.length == 3) {
			        firstDate = (strDateFirst[1].length != 2 ? '0' + strDateFirst[1] : strDateFirst[1]) + '/' + (strDateFirst[0].length != 2 ? '0' + strDateFirst[0] : strDateFirst[0]) + '/' + (strDateFirst[2].length != 4 ? '0' + strDateFirst[2] : strDateFirst[2]);
			        secDate = (strDateSec[1].length != 2 ? '0' + strDateSec[1] : strDateSec[1]) + '/' + (strDateSec[0].length != 2 ? '0' + strDateSec[0] : strDateSec[0]) + '/' + (strDateSec[2].length != 4 ? '0' + strDateSec[2] : strDateSec[2]);
			    } //alert(firstDate + "---" + secDate)
			}

			if(FormatOfComparision.toLowerCase() == "mm/dd/yyyy")
			{
				//alert("mm/dd/yyyy")
				firstDate = DateFirst
				secDate = DateSec;
			}

			firstDate = new Date(firstDate);
			secDate = new Date(secDate);

			firstSpan = Date.parse(firstDate);
			secSpan = Date.parse(secDate);
			
			//alert(firstSpan +"---" + secSpan)
			//alert(firstSpan > secSpan)

			if(firstSpan >= secSpan) 
				return true;	// first is greater
			else 
				return false;	// secound is greater
		}
		//Modified by Pradeep Kushwaha For iTrack - 837 on 10-Feb-2011
		//The fields that have percentage in the system are should be displaying
		//with the Brasilian Standard. Example: NNN,NN or 100,00.
		function formatRate(ctlRate, NoOfDecimalPlace) {
		   
		    if(sDecimalSep != 'undefined')
		        ctlRate = ReplaceAll(ctlRate, sDecimalSep, ".");

		    if (isNaN(ctlRate)) return ctlRate;
		    
		    if (NoOfDecimalPlace == "" || NoOfDecimalPlace == undefined)
		        NoOfDecimalPlace = 2;
		        
		    if (ctlRate != "")
		        ctlRate = parseFloat(ctlRate).toFixed(parseInt(NoOfDecimalPlace));
		        
		    ctlRate = ReplaceAll(ctlRate, ".", sDecimalSep)
		    
		    return ctlRate;

		}
		//Modified by Pradeep Kushwaha For iTrack - 837 on 10-Feb-2011
        //To be use for base currency other then the policy currency
		//The fields that have percentage in the system are should be displaying
		//with the Brasilian Standard. Example: NNN,NN or 100,00.
		function formatRateBase(ctlRate, NoOfDecimalPlace) {
		    if (sBaseDecimalSep != 'undefined')
		        ctlRate = ReplaceAll(ctlRate, sBaseDecimalSep, ".");

		    if (isNaN(ctlRate)) return ctlRate;
		    if (NoOfDecimalPlace == "" || NoOfDecimalPlace == undefined)
		        NoOfDecimalPlace = 2;
		    if (ctlRate != "")
		        ctlRate = parseFloat(ctlRate).toFixed(parseInt(NoOfDecimalPlace));

		    ctlRate = ReplaceAll(ctlRate, ".", sBaseDecimalSep)

		    return ctlRate;

		}
		/**************************************************************************/
		//The following function is used to format number into CPF/CNPJ format
		//Added by praveer panghal for itrack 399
		function FormatCPFCNPJ(vr) {
		 
		    var vr = new String(vr.toString());
		    if (vr != "") {
		        vr = vr.replace(/[.]/g, "");
		        vr = vr.replace("/", "");
		        vr = vr.replace(/[-]/g, "");
		        num = vr.length;
		        if (num == 11) {
		            vr = vr.substr(0, 3) + '.' + vr.substr(3, 3) + '.' + vr.substr(6, 3) + '-' + vr.substr(9, 3);		           
		        }
		        if (num == 14) {
		            vr = vr.substr(0, 2) + '.' + vr.substr(2, 3) + '.' + vr.substr(5, 3) + '/' + vr.substr(8, 4) + '-' + vr.substr(12, 2);		         
		        }
		    }	   
		    return vr;
		}
		/**************************************************************************/
		//The following function is used to format number into Brazilian ZipCode format
		//Added by praveer panghal for itrack 465
		function FormatZipCode(vr) {
		  
		    var vr = new String(vr.toString());
		    if (vr != "") {
		        
		        vr = vr.replace(/[-]/g, "");
		        num = vr.length;
		        if (num == 8) {
		            vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
		        }
		        
		    }
		    return vr; 
		}
/**************************************************************************/
//The following function is used to format amounts into decimal currency format
//This method is used exclusively at Claims Module and should not be altered/modified 
function formatCurrencyWithCents(num) {
			if (num != "")
			{
				amt = num;
				
				amt = ReplaceAll(new String(amt),".","");
				amt = ReplaceAll(new String(amt),",","");
				
				if (amt.length == 1)
					amt = amt + "0";
				
				if ( ! isNaN(amt))
				{
					return  InsertDecimal(amt);
					
					//Validating the amount, againt reguler exp
					//ValidatorOnChange();
				}
			}
			return num;		
}

function formatCurrency(num) {

		if(num==undefined)
		{
		 return num;
		}
		if (trim(num) == sGroupSep /* ',' */ || trim(num) == sCurrencyFormat)//'$')
		{
			return num;
		}		
		var regExp = '/\['+sCurrencyFormat+']|\\'+ sGroupSep +'/g'		
		//num = num.toString().replace(/\$|\,/g, '');
		num = num.toString().replace(regExp, '');
		
		if(num==null || num=='')
		{
		 return num;
		}
		
		num = getActualValue(num);		
		
		if(isNaN(num))
		{		
		    num=trim(num);
		    return num;
		}
		else
		{
		
		sign = (num == (num = Math.abs(num)));
		num = Math.floor(num*100+0.50000000001);
		
		cents = num%100;
		//alert(num)
		num = Math.floor(num/100).toString();
		if(cents<10)
			cents = "0" + cents;

        for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++) {

            num = num.substring(0, num.length - (4 * i + 3)) + sGroupSep /* ',' */ +
		        num.substring(num.length - (4 * i + 3));
        }
			num=trim(num);
			cents=trim(cents);

		return (((sign)?'':'-') +  num);
		}		
}

//Function added For Itrack Issue #5590
function formatCurrency_WriteOff(num) {

		if(num==undefined)
		{
		 return num;
		}

		//if ( trim(num) == ',' || trim(num) == '$')
		if (trim(num) == sGroupSep || trim(num) == sCurrencyFormat)
		{
			return num;
		}

		var regExp = '/\[' + sCurrencyFormat + ']|\\' + sGroupSep + '/g'
		//num = num.toString().replace(/\$|\,/g,'');
		num = num.toString().replace(regExp, '');
		
		if(num==null || num=='')
		{
		 return num;
		}
		
		//Check for [kKmM] entry and change value
		//if(!((window.event.srcElement == null) || (window.event.srcElement == 'undefined'))) 
		num = getActualValue(num);		
		
		if(isNaN(num))
		{
		//num = "0";
		num=trim(num);
		return num;
		}
		else
		{
		//Condition Added For Itrack Issue #5590.
		if(num.toString()=='-0')
		sign =false;
		else
		sign = (num == (num = Math.abs(num)));
		
		num = Math.floor(num*100+0.50000000001);
		
		cents = num%100;
		//alert(num)
		num = Math.floor(num/100).toString();
		if(cents<10)
			cents = "0" + cents;
		for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
		    num = num.substring(0, num.length - (4 * i + 3)) + sGroupSep /* ',' */ +
		num.substring(num.length-(4*i+3));
		//this.value=num;
			num=trim(num);
			cents=trim(cents);
		//return (((sign)?'':'-') +  num + '.' + cents);
		
//		num=Math.round(num)
		return (((sign)?'':'-') +  num);
		}
}

//This Function is to format the base currency
//Added by Pradeep Kushwaha on 01-Oct-2010
function formatBaseCurrencyAmount(num, NoOfDecimalPlace) {

    //Added by Pradeep Kushwaha on 30-March-2011 itrack-440 It work According to the Base Currency
    var roundToDecimalPlace = 2;
    var decimalSymbol = sBaseDecimalSep;
    var digitGroupSymbol = sBaseGroupSep;
    var groupDigits = true;
    var regex = generateRegex(decimalSymbol);
    var symbol = '';
    var positiveFormat = '%s%n';
    var negativeFormat = '-%s%n' //'(%s%n)';

    if (NoOfDecimalPlace != "" && NoOfDecimalPlace != undefined)
        roundToDecimalPlace = NoOfDecimalPlace;

    if (num === '' || (num === '-' && roundToDecimalPlace === -1)) {
        return num;
    }
    // if the number is valid use it, otherwise clean it
    if (isNaN(num)) {
        // clean number
        num = num.replace(regex, '');

        if (num === '' || (num === '-' && roundToDecimalPlace === -1)) {
            return num;
        }

        if (decimalSymbol != '.') {
            num = num.replace(decimalSymbol, '.');  // reset to US decimal for arithmetic
        }
        if (isNaN(num)) {
            num = '0';
        }
    }

    // evalutate number input
    var numParts = String(num).split('.');
    var isPositive = (num == Math.abs(num));
    var hasDecimals = (numParts.length > 1);
    var decimals = (hasDecimals ? numParts[1].toString() : '0');
    var originalDecimals = decimals;


    // format number
    //num = Math.abs(numParts[0]);
    if (!isPositive)
        num = numParts[0].replace("-", "");
    else
        num = (numParts[0]);
    num = isNaN(num) ? 0 : num;
    if (roundToDecimalPlace >= 0) {
        decimals = parseFloat('1.' + decimals);
        decimals = decimals.toFixed(roundToDecimalPlace); // round
        if (decimals.substring(0, 1) == '2') {
            num = Number(num) + 1;
        }
        decimals = decimals.substring(2); // remove "0."
    }
    num = String(num);

    if (groupDigits) {
        for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++) {
            num = num.substring(0, num.length - (4 * i + 3)) + digitGroupSymbol + num.substring(num.length - (4 * i + 3));
        }
    }

    if ((hasDecimals && roundToDecimalPlace == -1) || roundToDecimalPlace > 0) {
        num += decimalSymbol + decimals;
    }
    // format negative
    var format = isPositive ? positiveFormat : negativeFormat;
    var money = format.replace(/%s/g, symbol);
    money = money.replace(/%n/g, num);
    return money;
    //Till Here
    //Commented By Pradeep Kushwaha on 30-March-2011 For ITrack Issue 440(Not formating After insering dicimal number)
//    
//    if (num == undefined) {
//        return num;
//    }

//    var regExp = '/\[' + sBaseCurrencyFormat + ']|\\' + sBaseGroupSep + '/g'
//    //num = num.toString().replace(/\$|\,/g, '');
//    num = num.toString().replace(regExp, '');

//    if (num == null || num == '') {
//        return num;
//    }

//    //Check for [kKmM] entry and change value
//    //if(!((window.event.srcElement == null) || (window.event.srcElement == 'undefined'))) 
//    num = getActualValue(num);

//    if (isNaN(num)) {
//        //num = "0";
//        num = trim(num);
//        return num;
//    }
//    else {
//        sign = (num == (num = Math.abs(num)));
//        num = Math.floor(num * 100 + 0.50000000001);
//        cents = num % 100;
//        num = Math.floor(num / 100).toString();
//        if (cents < 10)
//            cents = "0" + cents;

//        for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
//            num = num.substring(0, num.length - (4 * i + 3)) + sBaseGroupSep + num.substring(num.length - (4 * i + 3));
//        //this.value=num;
//        num = trim(num);
//        cents = trim(cents);
//        return (((sign) ? '' : '-') + num + sBaseDecimalSep /* '.' */ + cents);
    //    }
//Commented Till here on 30 March-2011
}
///

function generateRegex(decimalSymbol) {
  return new RegExp("[^\\d" + decimalSymbol + "-]", "g");
}
 //
//This Function is to format the currency
function formatAmount(num, NoOfDecimalPlace) {
  
 //Added by Pradeep Kushwaha on 30-March-2011 itrack-It work According to the Policy Currency
    var roundToDecimalPlace = 2;
    var decimalSymbol= sDecimalSep;
    var digitGroupSymbol = sGroupSep;
    var groupDigits = true;
    var regex = generateRegex(decimalSymbol);
    var symbol = '';
    var positiveFormat='%s%n';
    var negativeFormat = '-%s%n' //'(%s%n)';
    
    if (NoOfDecimalPlace != "" && NoOfDecimalPlace != undefined)
        roundToDecimalPlace = NoOfDecimalPlace;
           
    if (num === '' || (num === '-' && roundToDecimalPlace === -1)) {
        return num;
    }
    // if the number is valid use it, otherwise clean it
    if (isNaN(num)) {
        // clean number
        num = num.replace(regex, '');

        if (num === '' || (num === '-' && roundToDecimalPlace === -1)) {
            return num;
        }

        if (decimalSymbol != '.') {
            num = num.replace(decimalSymbol, '.');  // reset to US decimal for arithmetic
        }
        if (isNaN(num)) {
            num = '0';
        }
    }

    // evalutate number input
    var numParts = String(num).split('.');
    var isPositive = (num == Math.abs(num));
    var hasDecimals = (numParts.length > 1);
    var decimals = (hasDecimals ? numParts[1].toString() : '0');
    var originalDecimals = decimals;

    
    // format number
    //num = Math.abs(numParts[0]);
    if (!isPositive)
        num = numParts[0].replace("-", "");
    else
        num = (numParts[0]);
    num = isNaN(num) ? 0 : num;
    if (roundToDecimalPlace >= 0) {
        decimals = parseFloat('1.' + decimals); 
        decimals = decimals.toFixed(roundToDecimalPlace); // round
        if (decimals.substring(0, 1) == '2') {
            num = Number(num) + 1;
        }
        decimals = decimals.substring(2); // remove "0."
    }
    num = String(num);
     
    if (groupDigits) {
        for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++) {
            num = num.substring(0, num.length - (4 * i + 3)) + digitGroupSymbol + num.substring(num.length - (4 * i + 3));
        }
    }

    if ((hasDecimals && roundToDecimalPlace == -1) || roundToDecimalPlace > 0) {
        num += decimalSymbol + decimals;
    }
    // format negative
    var format = isPositive ? positiveFormat : negativeFormat;
    var money = format.replace(/%s/g, symbol);
    money = money.replace(/%n/g, num);
    return money;
    //Till Here 
//Commented By Pradeep Kushwaha on 30-March-2011 For ITrack Issue 440(Not formating After insering decimal number)
//			if(num==undefined)
//			{
//				return num;
//			}

//			var regExp = '/\[' + sCurrencyFormat + ']|\\' + sGroupSep + '/g'
//			//num = num.toString().replace(/\$|\,/g, '');
//			num = num.toString().replace(regExp, '');
//		
//			if(num==null || num=='')
//			{
//				return num;
//			}
//		
//			//Check for [kKmM] entry and change value
//			//if(!((window.event.srcElement == null) || (window.event.srcElement == 'undefined'))) 
//			num = getActualValue(num);
//			
//			if(isNaN(num))
//			{
//				//num = "0";
//				num=trim(num);
//				return num;
//			}
//			else
//			{
//				sign = (num == (num = Math.abs(num)));
//				num	= Math.floor(num*100+0.50000000001);
//				cents = num%100;
//				num = Math.floor(num/100).toString();
//				if(cents<10)
//					cents = "0" + cents;
//				
//				for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
//				    num = num.substring(0, num.length - (4 * i + 3)) + sGroupSep + num.substring(num.length - (4 * i + 3));
//					//this.value=num;
//					num=trim(num);
//					cents=trim(cents);
//				return (((sign)?'':'-') +  num + sDecimalSep /* '.' */ + cents);
//			}//Commented till here on 30-March-2011

		}
 
		//Function Name      : ReplaceDateSeparator
		//Parameters		   : Date Value
		//Author             :  
		//Purpose            : To Replace the Date Separator to "/", to use the new Date() constructor in Javascript
		//Creation Date      : 10/May/2004
		//Last Modified Date : 
		//---------------------------------------------------------------------------------------------------------------*/
		function ReplaceDateSeparator(dtDateValue)
		{
			var dtDSep;
	   		dtDSep = GetDateSeparator(dtDateValue);
	   		if(dtDSep =="")
	   			dtDSep = "/"
	   		else(dtDSep ==" ")
	   		{
	   			dtDSep = "/"
	   			dtDateValue = ReplaceString(dtDateValue," ", "")
	   		}
			retDateValue = dtDateValue;
			
			retDateValue = ReplaceString(dtDateValue, dtDSep, "/");
			return retDateValue;
		}
		/**********************************************************
		Function Name	GetDateSeparator
		Input			Date value
		Creation Date	05/9/02
		Purpose			To extract the separator of the date
		Return Value	Date Separator		
		***********************************************************/
		function GetDateSeparator(dtDateValue)
		{
			var retDateSeparator="";			
			var strSeparatorArray = new Array("-"," ","/",".");
			
			for (intElementNr = 0; intElementNr < strSeparatorArray.length; intElementNr++)			
			{
				var separator = dtDateValue.indexOf(strSeparatorArray[intElementNr]);
				if ( separator!= -1)
				{
					retDateSeparator = dtDateValue.charAt(separator);
					break;
				}
			}
			return retDateSeparator;
		}
		
		/**********************************************************
		Function Name	ReplaceString
		Input			Date value
		Creation Date	05/9/02
		Purpose			To extract the separator of the date
		Return Value	Date Separator		
		***********************************************************/
		function ReplaceString(lstrString, lcharReplaceChar, lstrReplaceWith)
		{
			if (lstrString == "" || lstrString == null)
			{
				return ""
			}
			else
			{
				var lintcounter, lintcurrLoc, retText, lintStringLength
				retText = ""
				for (lintcounter =0 ; lintcounter < lstrString.length; lintcounter++)
					if (lstrString.charAt(lintcounter) == lcharReplaceChar)
						retText = retText + lstrReplaceWith;
					else
						retText = retText + lstrString.charAt(lintcounter)
				return retText;
			}
		}
		
		function trim(inputString)
		{
			// Removes leading and trailing spaces from the passed string. Also removes
			// consecutive spaces and replaces it with one space. If something besides
			// a string is passed in (null, custom object, etc.) then return the input.
			if (typeof inputString != "string") { return inputString; }
			var retValue = inputString;
			var ch = retValue.substring(0, 1);
			while (ch == " ")
			{ // Check for spaces at the beginning of the string
				retValue = retValue.substring(1, retValue.length);
				ch = retValue.substring(0, 1);
			}
			ch = retValue.substring(retValue.length-1, retValue.length);
			while (ch == " ")
			{ // Check for spaces at the end of the string
				retValue = retValue.substring(0, retValue.length-1);
				ch = retValue.substring(retValue.length-1, retValue.length);
			}
			while (retValue.indexOf("  ") != -1) 
			{ // Note that there are two spaces in the string - look for multiple spaces within the string
				retValue = retValue.substring(0, retValue.indexOf("  ")) + retValue.substring(retValue.indexOf("  ")+1, retValue.length); // Again, there are two spaces in each of the strings
			}	
			return retValue; // Return the trimmed string back to the user
		} // Ends the
	
	//******************** Function to plcae tab contents in web index pages
	//tabContent is the object instance of div tag enclosing the tab iframe.
	//gridpanel is the  object instance of div tag enclosing the web grid.
	function PlaceTabcontent(tabContent,gridpanel,adjustment)
	{
		if(adjustment=="" || adjustment==null)
			adjustment=40;
		var GridTop = parseInt(gridpanel.style.top.substr(0,gridpanel.style.top.indexOf("px")));
		tabContent.style.top= (parseInt(GridTop) + parseInt(gridpanel.style.height.substr(0,gridpanel.style.height.indexOf("px")))+parseInt(adjustment))+"px";
	}	
	
	// This function is used to open new popup window with out any browser options and called from login.aspx and topframe.aspx
	function openWindow(url)
		{
		var myUrl = url;
		window.open(myUrl,'','toolbar=no, directories=no, location=no, status=yes, menubar=no, resizable=yes, scrollbars=no,  width=900, height=500'); 
		return false;		
		}
	// This function is used to open Home page of Ebix.com with browser options and called from Powered by ebix images.
	function openWindowHomePage(url)
		{
		var myUrl = url;
		window.open(myUrl,'','toolbar=Yes, directories=no, location=yes, status=yes, menubar=yes, resizable=yes, scrollbars=no,  width=900, height=500'); 
		return false;		
		}	//	
	
	// This function is used to check the maxlength of a text box when its Multiline property is true.
	// obj is the text box name 
	// max is the number against which we required check( like 200 char in text box).
	function MaxLength(obj,max)
	{
	//if((document.ProprietorsDirectorsAdd.txtBPB_REGNO.value).length < 3)
		if((obj.value).length >= max)
		event.returnValue=false;
		return false;
	}
	
	// This function is used to check the maxlength of a text box when its Multiline property is true.
	// obj is the text box name 
	// max is the number against which we required check( like 200 char in text box).
	function limitText(textObj, maxCharacters)
	{
		if (textObj.innerText.length >= maxCharacters)
		{
			if ((event.keyCode >= 32 && event.keyCode <= 126) || 
				(event.keyCode >= 128 && event.keyCode <= 254))
			{
				event.returnValue = false;
			}
		}
	}
	
	/****************** readCookie ********************************
	Created by : Anshuman, 15/7/2005
	Description: Is useded to read a cookie by supplying cookie name.
	Parameters:  name - name of the cookie which you want to read
	Note: if cookie with that name is not there then it reurns null
	*/
	function readCookie(name)
	{
		var nameEQ = name + "=";
		var ca = document.cookie.split(';');
		for(var i=0;i < ca.length;i++)
		{
			var c = ca[i];
			while (c.charAt(0)==' ') c = c.substring(1,c.length);
			if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
		}
		return null;
	}
	function createCookie(name,value,days)
	{
		if (days)
		{
			var date = new Date();
			date.setTime(date.getTime()+(days*24*60*60*1000));
			var expires = "; expires="+date.toGMTString();
		}
		else var expires = "";
		document.cookie = name+"="+value+expires+"; path=/";
	}
	function eraseCookie(name)
	{
		createCookie(name,"",-1);
	}
	
	/******************GenerateRandomCode ********************************
	Created by : Sumit Chhabra, 11/11/2005
	Description: It generates a random code based on the two string values passed
	Parameters:  FirstName- FirstName 
				 LastName- LastName	
	*/
	var jsaRandomCode="0";
	function GenerateRandomCode(FirstName,LastName)
	{		
		if(jsaRandomCode=="0")
			jsaRandomCode=new String(Math.floor(Math.random()*51));
		var s1=new String(FirstName);
		var s2=new String(LastName);
		var s3=new String(Math.floor(Math.random()*51));
		if(s1.length<1 && s2.length<1)
			return '';
		//var RandomCode=s1.substring(0,3) + s2.substr(0,4) + s3;
		var RandomCode=s1.substring(0,3) + s2.substr(0,4) + jsaRandomCode;
        return RandomCode;
    }
    /******************SetDropDownValueForConcatenatedString ********************************
	Created by : Sumit Chhabra, 05/11/2006
	Description: The given function is used to set value of a dropdown when the value part of dropdown contains
				 an encoded string.
	Parameters:  comboID- ID of the combo box
				 SelectedValue- Value to be selected at the combo
				 EncodedCharacter - Character used for encoding the string
				 ValuePosition - Position in the encoded string where the value will be found
	Example:	EncodedString as used in value field of combo - Code^Name^Address^Zip
				Here to set combo with particular Code, pass the following parameters
				SetDropDownValueForConcatenatedString(comboID,SelectedValue,'^',1);
	*/
    function SetComboValueForConcatenatedString(comboID,SelectedValue,EncodedCharacter,ValuePosition)
	{
		combo = document.getElementById(comboID);		
		if(combo==null || combo.selectedIndex==-1) return;		
		for(i=0;i<combo.options.length;i++)
		{
			encoded_string = new String(combo.options[i].value);				
			array = encoded_string.split(EncodedCharacter);
			//Get ComboValue
			ComboValue = array[ValuePosition];
			//If the code value matches with selected value, set the selected Index to it and return
			if(ComboValue == SelectedValue)
			{
				combo.options.selectedIndex = i;
				return;
			}				
		}		
	}
	/******************CompareTwoDate ********************************
	Created by : Sumit Chhabra, 05/19/2006
	Description: The given function is used compare two dates on the basis of date format provided.
	Parameters:  DateFirst - First Date
				 DateSec - Second Date
				 FormatOfComparision - Format of date				 	
	*/
	function CompareTwoDate(DateFirst, DateSec, FormatOfComparision,GreaterThanEqualComparison)
		{
			var saperator = '/';
			var firstDate, secDate;
			var strDateFirst = DateFirst.split("/");
			var strDateSec = DateSec.split("/");
			if(FormatOfComparision.toLowerCase() == "dd/mm/yyyy")
			{			
				firstDate = (strDateFirst[1].length != 2 ? '0' + strDateFirst[1] : strDateFirst[1]) + '/' + (strDateFirst[0].length != 2 ? '0' + strDateFirst[0] : strDateFirst[0])  + '/' + (strDateFirst[2].length != 4 ? '0' + strDateFirst[2] : strDateFirst[2]);
				secDate = (strDateSec[1].length != 2 ? '0' + strDateSec[1] : strDateSec[1]) + '/' + (strDateSec[0].length != 2 ? '0' + strDateSec[0] : strDateSec[0])  + '/' + (strDateSec[2].length != 4 ? '0' + strDateSec[2] : strDateSec[2]);
			}
			if(FormatOfComparision.toLowerCase() == "mm/dd/yyyy")
			{				
				firstDate = DateFirst
				secDate = DateSec;
			}
			firstDate = new Date(firstDate);
			secDate = new Date(secDate);
			firstSpan = Date.parse(firstDate);
			secSpan = Date.parse(secDate);			
			if(GreaterThanEqualComparison==undefined || GreaterThanEqualComparison=='' || GreaterThanEqualComparison==0)
			{
				if(firstSpan > secSpan) 
					return true;	// first is greater
				else 
					return false;	// secound is greater
			}
			else if(GreaterThanEqualComparison==1)
			{
				if(firstSpan >= secSpan) 
					return true;	// first is greater than equal
				else 
					return false;	// secound is greater
			}
		}	
		
		/*-------------------------------------------------------------------------------------------------------------
			Function Name      : EnableValidator(ValidatorID, flag)
			Author             : Sumit Chhabra 
			Purpose            : Enable/Disables a validator based on the flag parameter passed
		---------------------------------------------------------------------------------------------------------------*/
		function EnableValidator(ValidatorID,flag)
		{
			if(ValidatorID==null) return;
			
			if(flag) //Enable the validator
			{
				document.getElementById(ValidatorID).setAttribute('enabled',true);
				document.getElementById(ValidatorID).setAttribute('isValid',true);
			}
			else
			{
				document.getElementById(ValidatorID).setAttribute('enabled',false);
				document.getElementById(ValidatorID).style.display = "none";
				document.getElementById(ValidatorID).setAttribute('isValid',false);
			}
		}
		
	/*EFT*/	
	function CalculateCheckDigit(txtTranNumber)
	{
			var tranNum = txtTranNumber;
			var firstDigit = tranNum.slice(0,1);
			var n = tranNum.length;
			//Weights:   3  7  1  3  7  1  3  7
			var strWeights = "37137137";
			var strWeightsNum = new String(strWeights);
			var pos =0;
			var wght =0;
			var val1,val2,val3,val4,val5,val6,val7,val8;
			if(firstDigit != "5")
			{
				if( n == "8")
				{
					var strtranNum = new String(txtTranNumber);
					for(var i=0;i<strtranNum.length;i++)
					{
						//Multiply each digit of the Routing number by the weight factor:	
						pos = strtranNum.charAt(i);
						if(i == "0")
						val1 = pos * 3;
						if(i == "1")
						val2 = pos * 7;
						if(i == "2")
						val3 = pos * 1;
						if(i == "3")
						val4 = pos * 3;
						if(i == "4")
						val5 = pos * 7;
						if(i == "5")
						val6 = pos * 1;
						if(i == "6")
						val7 = pos * 3;
						if(i == "7")
						val8 = pos * 7;
								
					}
					//Add the results of each multiplication.
					var sum = val1 + val2 + val3 + val4 + val5 + val6 + val7 + val8;
					//Subtract the sum from the next highest multiple of 10.  The result is the check digit
					var valsum = parseInt(sum/10);
					var chkdigit = (((valsum)*10) + 10) - sum;
					if(chkdigit == "10") //handle 10
					{
					  chkdigit = "0"						
					}
					//txtTranNumber.value = strtranNum + chkdigit;			
					return  chkdigit;			
				}
			}
		}
			
			function ValidateTransitNumber(txtTransitNumber)
			{
				var strtranNum = new String(txtTransitNumber.value.trim());
			
				if(strtranNum.length == "8")
				{
					var charCheckDigit = CalculateCheckDigit(strtranNum);
					//txtTransitNumber = txtTransitNumber + charCheckDigit;
					txtTransitNumber.value = strtranNum + charCheckDigit;
					return true;
				}
				
				if(strtranNum.length == "9")
				{
					var tempString ;
					var oldCheckDigit; // == 9th char
					oldCheckDigit = strtranNum.charAt(8);
					var tempString = strtranNum.substring(0,8);
					charCheckDigit = CalculateCheckDigit(tempString);
					if(charCheckDigit == oldCheckDigit)
						return true;
					else
						return false;
				}		
					
			}
			/**/
			function ValidateTransitNumberLen(txtTransitNumber)
			{
				var strtranNum = new String(txtTransitNumber.value.trim());
				if(isNaN(strtranNum) == false)
				{
				if(strtranNum.length < 8)
					return false;
				}				
			}	
			/**/
			
			function ValidateZipForInflation(txtZip)
			{
				var strtranNum = new String(txtZip.value.trim());
				if(isNaN(strtranNum) == false)
				{
					if(strtranNum.length < 3)
						return false;
					else 
						return true;
				}
				else
					return true;	
				
			}	
		/* -- NOT USED ANYMORE AS A REGULAR EXPRESSION HAS BEEN WRITTEN FOR FEDERAL ID
			function ValidateFederalIDLen(txtFederalID)
			{
				var strFederal = new String(txtFederalID.value.trim());
				if(isNaN(strFederal) == false)
				{
				if(strFederal.length < 9)
					return false;
				}
				
			}	*/	
			
			/*Federal ID*/
			/*DFI Account Number*/
			function ValidateDFIAcctNo(txtDFIAcctNo)
			{
				var strDFINum = new String(txtDFIAcctNo.value.trim());
				var num = "";
				var val = "";
				for(var i=0;i<strDFINum.length;i++)
				{	
					num = strDFINum.charAt(i);
					//val = val + num.replace(' ','')
					if(num == ' ')	
					return false;
				}				
			}		
			/*DFI Account Number*/
		
	/*END*/
		
	/*-------------------------------------------------------------------------------------------------------------
	Function Name      : FormatDollar()
	Author             : Ashwani 
	---------------------------------------------------------------------------------------------------------------*/
function FormatDollar(szFormName,szFieldName,NumbertoChange,strChar)
{
	var change = false
	
	if(parseFloat(NumbertoChange)<0)
	{
		NumbertoChange = (parseFloat(NumbertoChange) * -1).toString();
		change = true;
	}
	
	var lintLoopCounter, decimalLocation, lboolValueChanged = "0"
	var afterDecimal, beforeDecimal, revString = "", commaString = "", lstrFinal = ""; 
	var ReturnString,szNegSign,szPostSign
	szNegSign = ""
	szPostSign = ""
	if(isNaN(NumbertoChange))
		return ;
    
    // Replaces text with by in string
    //NumbertoChange=ReplaceAll(NumbertoChange,",","");
    NumbertoChange = ReplaceAll(NumbertoChange, sGroupSep, "");	
		
	if (FindChar(NumbertoChange,",") == false)
	{
		if (FindChar(NumbertoChange,"-") == true)
		{
			NumbertoChange = RemoveChar(NumbertoChange,"-")
			szNegSign = "1"
		}	
		if (FindChar(NumbertoChange,"+") == true)
		{
			NumbertoChange = RemoveChar(NumbertoChange,"+")
			szPostSign = "1"	
		}	

		lboolValueChanged = "1"
		//decimalLocation = NumbertoChange.indexOf(".");
		decimalLocation = NumbertoChange.indexOf(sDecimalSep);
		
		if (decimalLocation == "-1" )
			decimalLocation = NumbertoChange.length
		afterDecimal = NumbertoChange.substr(decimalLocation+1)
		beforeDecimal = NumbertoChange.substr(0,decimalLocation)
			
		for (lintLoopCounter=beforeDecimal.length-1; lintLoopCounter >= 0 ; lintLoopCounter--)
			revString = revString + beforeDecimal.charAt(lintLoopCounter)
	
		for (lintLoopCounter=0; lintLoopCounter <= revString.length-1 ; lintLoopCounter+=3 )
		    commaString = commaString + revString.substr(lintLoopCounter, 3) + sGroupSep/* "," */

		if (commaString.charAt(commaString.length - 1) == sGroupSep/* "," */)
			commaString = commaString.substr(0, commaString.length-1)
		revString = commaString;		
		for (lintLoopCounter=revString.length-1; lintLoopCounter >= 0 ; lintLoopCounter--)
			lstrFinal = lstrFinal + revString.charAt(lintLoopCounter)
		
		if (TrimTheString(afterDecimal) == "" )
		{			
			if (TrimTheString(szFormName) == "" )	
				ReturnString = lstrFinal + sDecimalSep + "00"//".00"
			else
				ReturnString = lstrFinal
		}
		else
		{
			ReturnString = lstrFinal + sDecimalSep /* "." */ + afterDecimal
		}
		
		if (TrimTheString(afterDecimal) == "" )
		{
			if(lstrFinal != "")
			    ReturnString = lstrFinal + sDecimalSep + "00"//".00"		
		}
		
		if (TrimTheString(szNegSign) == "1" )
		{			 
			 ReturnString = "-" + ReturnString 
		}
		else if (TrimTheString(szPostSign) == "1" )
			ReturnString = "+" + ReturnString 
		else
			ReturnString = ReturnString		
	}
	
	if (TrimTheString(szFormName) != "" )
	{
		
		if (lboolValueChanged == "1")
		{
			if (change == true)			 
				eval('document.'+szFormName+'.'+szFieldName).value = '-'+ ReturnString;
			else
				eval('document.'+szFormName+'.'+szFieldName).value = ReturnString;
		}
		else
		{
			if (change == true)			 
				eval('document.'+szFormName+'.'+szFieldName).value = '-' + NumbertoChange;		
			else
				eval('document.'+szFormName+'.'+szFieldName).value = NumbertoChange;		
		}	
	}
	else
	{
		
		if (lboolValueChanged == "0")
		{
			if (change == true)
				return '-'+ NumbertoChange
			else
				return NumbertoChange
		}
		else
		{
			if (change == true)
				return '-'+ReturnString
			else
				return ReturnString				
		}
	}
}     
    
		/*-------------------------------------------------------------------------------------------------------------
			Function Name      : ObjectDisplay(ControlID, flag)
			Author             : Swastika Gaur 
			Purpose            : Show/Hide a control based on the flag parameter passed
		---------------------------------------------------------------------------------------------------------------*/
		function ObjectDisplay(ControlID,flag)
		{
			if(ControlID==null) return;
			
			if(flag) //Show the object
			{
				document.getElementById(ControlID).style.display = "inline";
			}
			else
			{
				document.getElementById(ControlID).style.display = "none";
			}
		}    
    
    //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
    //Added by RPSINGH for showing tool tip in drop down
    var drpForToolTip
	function ShowToolTip1(show,objDDL)
	{
			spDiv1.innerHTML = "";		
			if(show)//true
			{
				if(objDDL.selectedIndex>0){
					var point = fGetXY(objDDL);
					with (VicPopCal1.style) {
  					left = point.x;
					top  = point.y + objDDL.offsetHeight;
					width = VicPopCal1.offsetWidth;
					height = VicPopCal1.offsetHeight;
					visibility = 'visible';
					}			
					spDiv1.innerHTML = document.getElementById(objDDL.id).item(objDDL.selectedIndex).text;
					divCover1.style.visibility	= 'visible';
					divCover1.style.width		= VicPopCal1.offsetWidth;
					divCover1.style.top			= VicPopCal1.offsetTop;
					divCover1.style.left		= VicPopCal1.offsetLeft;
					divCover1.style.height		= VicPopCal1.offsetHeight;				
					drpForToolTip = objDDL.id;
					window.setTimeout("ShowToolTip1(false,document.getElementById(drpForToolTip));",3000);
				}
			}
			else
			{	
				VicPopCal1.style.visibility = 'Hidden';
				divCover1.style.visibility = 'Hidden';
				divCover1.style.width = '0px';
				divCover1.style.top = '0px';
				divCover1.style.left = '0px';
				divCover1.style.height = '0px';
			}
	}
	with (document) {
	write("<html>");
	write("<head>");
	write("</head>");
	write("<Div id='divCover1' Style='visibility:Hidden;Position:Absolute;background-Color:Black;Height:0;width:0;Border:0px Solid black;z-index:0;Position:Absolute;overflow:hidden;'>");
	write("	<IFrame Scrolling='No' Width='600' Height='100px;' Style='Position:Absolute;' FrameBorder='1' Src='About:Blank'></IFrame>");
	write("</Div>");
	write("<Div id='VicPopCal1' style='z-Index:10;POSITION:absolute;VISIBILITY:hidden;border:1px ridge;width:200;'>");
	write("<table bgcolor='infobackground' style='BORDER-RIGHT:black 1px solid;WIDTH:100%;BORDER-TOP:black 1px solid; BORDER-LEFT:black 1px solid; BORDER-BOTTOM:black 1px solid'>");
	write("<tr>");
	write("<td><span Style='FONT-SIZE:10px;Color:InfoText;FONT-FAMILY:Verdana' id='spDiv1'>");
	write("</span></td>");
	write("</tr>");
	write("</TABLE></Div>");
	write("</html>");
	}
    
    //End of addition by RPSINGH for showing tool tip in drop down
   
    //Added for ItrackIssue 5553 on 9 march 2009
    function RemoveSpecialChar(file,revDesc) 
		{
			var extArray = new Array('#','*','+',')','@','=','!',':','$','|','.','~','%','(','`','^','?','<','>','{','}','/',';','"',',','&amp;','&','&apos;');
			allowSubmit = true;
			if (!file) 
				return;
				while (file.indexOf("\\") != -1)
					file = file.slice(file.indexOf("\\") + 1);
					
				var ext = new String(file.slice(file.indexOf(""),file.indexOf(".")).toLowerCase()); 
				for (var i = 0; i < ext.length; i++) 
				{
					for(var j=0;j<extArray.length;j++)
					{						
						if(ext.charAt(i)==extArray[j]) 
						{
							allowSubmit = false; 
							break;
						}
					}
				}
				if (allowSubmit) 
				{
					revDesc.setAttribute('enabled',false);
					revDesc.style.display='none';
					revDesc.setAttribute('isVisible',false);
				}
				else
				{
					revDesc.setAttribute('enabled',true);
					revDesc.style.display='inline';
					revDesc.setAttribute('isVisible',true);
				}			
		}		
		
		function RemoveExecutableFiles(file,revDesc) 
		{
			var extensionArray = new Array("exe","bat","dll","com","hlp","adp","jse","msi","sys","bas","pif","ocx","bin","dat","log","tmp","ini","bak");
			allowSubmit = true;
			if (!file) 
				return;
				
				var extensionPos = file.lastIndexOf('.');
				var extensionType = file.substring(extensionPos+1);
				for (var i = 0; i < extensionArray.length; i++) 
				{
					if(extensionType.toLowerCase()==extensionArray[i]) 
					{
						allowSubmit = false; 
						break;
					}
				}
				if (allowSubmit) 
				{
					revDesc.setAttribute('enabled',false);
					revDesc.style.display='none';
					revDesc.setAttribute('isVisible',false);
				}
				else
				{
					revDesc.setAttribute('enabled',true);
					revDesc.style.display='inline';
					revDesc.setAttribute('isVisible',true);
				}			
		}
		
		//Added for ItrackIssue 5706 on 16 April 2009
		function AllowPDFFiles(file,revDesc) 
		{
			allowSubmit = false;
			if (!file) 
				return;
				
				var extensionPos = file.lastIndexOf('.');
				var extensionType = file.substring(extensionPos+1);

				if (extensionType.toLowerCase() == "pdf" || extensionType.toLowerCase() == "doc" || extensionType.toLowerCase() == "docx" || extensionType.toLowerCase() == "rtf") 
				{
					allowSubmit = true; 
				}

				if (allowSubmit) 
				{
					revDesc.setAttribute('enabled',false);
					revDesc.style.display='none';
					revDesc.setAttribute('isVisible',false);
				}
				else
				{
					revDesc.setAttribute('enabled',true);
					revDesc.style.display='inline';
					revDesc.setAttribute('isVisible',true);
				}
        }




        function AllowEXTFiles(file, revDesc) {
            allowSubmit = false;
            if (!file)
                return;

            var extensionPos = file.lastIndexOf('.');
            var extensionType = file.substring(extensionPos + 1);

            if (extensionType.toLowerCase() == "pdf" || extensionType.toLowerCase() == "doc" || extensionType.toLowerCase() == "docx" || extensionType.toLowerCase() == "rtf" || extensionType.toLowerCase() == "htm" || extensionType.toLowerCase() == "html") {
                allowSubmit = true;
            }

            if (allowSubmit) {
                revDesc.setAttribute('enabled', false);
                revDesc.style.display = 'none';
                revDesc.setAttribute('isVisible', false);
            }
            else {
                revDesc.setAttribute('enabled', true);
                revDesc.style.display = 'inline';
                revDesc.setAttribute('isVisible', true);
            }
        }

var main1 = {};
main1.activeMenuBar = null;
main1.mousein = false;
main1.subMenuLeft = 0;
main1.subMenuTop = 0;
main1.subMenuWidth = 0;
main1.subMenuHeight = 0;
main1.fixedButton = null;
main1.normalImage = new Array();
main1.activeImage = new Array();
main1.adjacentImage = new Array();
main1.menuXml = "";
main1.menuXmlReady = true;

var langCulture; //Added by Charles on 15-Mar-10 for Multilingual Implementation

function GetAgencyID() {
//Commented by Charles on 20-May-10 for Itrack 51
//	if(readCookie('ckSysId') != null && readCookie('ckSysId').toUpperCase() == 'W001')
//	{
//		return readCookie('ckSysId').toUpperCase();
//	}
//	else
//	{
		return "";
//	}
}
function GetUserName()
{
	//Done to show User's Full Name without using cookies
	/*if(readCookie('ckSysId') != null)
	{
		return readCookie('ckUserFLNm'); //readCookie('ckUserNm');
	}
	else
	{
		return "";
	}*/
  return Username;
}
/// added for ajax call
function FetchPolicyMenuXML(policyID, policyVersionId, customerID, lob,strAgencyId)
		{
			var ParamArray = new Array();
			obj1 = new Parameter('policyID', policyID)
			ParamArray.push(obj1);
			obj1 = new Parameter('policyVersionId', policyVersionId)
			ParamArray.push(obj1);
			obj1 = new Parameter('customerID', customerID)
			ParamArray.push(obj1);
			obj1 = new Parameter('lob', lob)
			ParamArray.push(obj1);
			obj1 = new Parameter('strAgencyId', strAgencyId)
			ParamArray.push(obj1);
			var objRequest = _CreateXMLHTTPObject();			
			var Action = 'POLMENU';
			//alert(Action);			
			_SendAJAXRequest(objRequest,Action,ParamArray,AjaxCallFunction_CallBack);
		}		
function FetchAppMenuXML(lobString,PolicyLevel,strAgencyId)
		{
			var ParamArray = new Array();
			obj1 = new Parameter('lobString', lobString)
			ParamArray.push(obj1);
			obj1 = new Parameter('PolicyLevel', PolicyLevel)
			ParamArray.push(obj1);
			obj1 = new Parameter('strAgencyId', strAgencyId)
			ParamArray.push(obj1);
			var objRequest = _CreateXMLHTTPObject();			
			var Action = 'APPMENU';			
			//alert(Action)
			_SendAJAXRequest(objRequest,Action,ParamArray,AjaxCallFunction_CallBack);
		}				
function FetchDefaultMenuXML(strAgencyId)
		{
			var ParamArray = new Array();
			obj1 = new Parameter('strAgencyId', strAgencyId)
			ParamArray.push(obj1);
			var objRequest = _CreateXMLHTTPObject();			
			var Action = 'DEFAULTMENU';			
			//alert(Action)
			_SendAJAXRequest(objRequest,Action,ParamArray,AjaxCallFunction_CallBackDefaultMenu);
		}				
function AjaxCallFunction_CallBack(response)
	{
				var Xml = response;
				var objResponse=new Object();
				objResponse.value = Xml;
				createAppMenuData(objResponse);
	}
function AjaxCallFunction_CallBackDefaultMenu(response)
	{
				var Xml = response;
				var objResponse=new Object();
				objResponse.value = Xml;
				createMenuData(objResponse);
	}	
// ajax call end here
//Creating the menus for specific policy and specific lob
//function createPolicyTopMenu(policyID, policyVersionId, customerID, lob)
function createPolicyTopMenu(policyID, policyVersionId, customerID, lob, agencyID)
{
	main1.menuXmlReady = false;
	top.topframe.main1.activeMenuBar = "1";
	/*
	var lstr = "../webservices/menudata.asmx?WSDL";	
	//Getting the currently loged in agency id
	strAgencyId = GetAgencyID();
	if( typeof(myTSMain1.useService) == 'undefined') 
	{
		var exp = "createPolicyTopMenu('" + policyID + "','" 
			+ policyVersionId + "','" 
			+ customerID + "','" + lob + "','" + strAgencyId + "')";
		setTimeout( exp, 1000);
	}
	else 
	{
		var co = myTSMain1.createCallOptions();
		co.async = false;
		co.funcName = "fetchPolicyMenu";
		var oResult;	    
		myTSMain1.useService(lstr.toString(), "TSM");
	  
		//Getting the currently loged in agency id
		strAgencyId = GetAgencyID();		
		//oResult = myTSMain1.TSM.callService(co, policyID, policyVersionId, customerID, lob,
		//		"fetchPolicyMenu");
		
		oResult = myTSMain1.TSM.callService(co, policyID, policyVersionId, customerID, lob,strAgencyId,
				"fetchPolicyMenu");		
		
		if( typeof(oResult.value) == 'undefined') 
		{
			//var exp = "createPolicyTopMenu('" + policyID + "','" 
			//+ policyVersionId + "','" 
			//+ customerID + "')";
			//setTimeout( exp, 1000);			
			var exp = "createPolicyTopMenu('" + policyID + "','" 
			+ policyVersionId + "','" 
			+ customerID + "','" + lob + "','" +  strAgencyId + "')";
			
			setTimeout( exp, 1000);
		}

		createAppMenuData(oResult);
	}
	*/
		strAgencyId = GetAgencyID();	
		oResult = FetchPolicyMenuXML(policyID, policyVersionId, customerID, lob,strAgencyId);		
}


//Create the menus for the specific lob
function createAppTopMenu(lobString, defaultPage, policyLevel)
{
	main1.menuXmlReady = false;
	/*
	var lstr = "../webservices/menudata.asmx?WSDL";
	if( typeof(myTSMain1.useService) == 'undefined') 
	{
		var exp = "createAppTopMenu('" + lobString + "','" + defaultPage +
			"','" + policyLevel + "')";
		setTimeout( exp, 1000);
	}
	else 
	{
		var co = myTSMain1.createCallOptions();
		co.async = false;
		co.funcName = "fetchLobMenu";
		var oResult;
	  
		myTSMain1.useService(lstr.toString(), "TSM");
	  
		//Getting the currently loged in agency id
		strAgencyId = GetAgencyID();
	  
		if (policyLevel == true)
			oResult = myTSMain1.TSM.callService(co, lobString,"1",strAgencyId,
				"fetchLobMenu");
		else
			oResult = myTSMain1.TSM.callService(co, lobString,"0",strAgencyId,
				"fetchLobMenu");
		if( typeof(oResult.value) == 'undefined') 
		{
			var exp = "createAppTopMenu('" + lobString + "','" + defaultPage +
				"','" + policyLevel + "')"
			setTimeout( exp, 1000);
		}
		createAppMenuData(oResult);
	  	
	} */
		strAgencyId = GetAgencyID();
		if (policyLevel == true)
			oResult =FetchAppMenuXML(lobString,"1",strAgencyId)
		else
			oResult =FetchAppMenuXML(lobString,"0",strAgencyId)
}

//*************************************
//Used for testing synchronous webservice calls
function callAsyncService(lobString, defaultPage, policyLevel)
{

	if (policyLevel == false) 
		var lstr = "LobString="+lobString+"&PolicyLevel=0&AgencyCode="+GetAgencyID();
	else
		var lstr = "LobString="+lobString+"&PolicyLevel=1&AgencyCode="+GetAgencyID();
	
	var xmlHTTP = new ActiveXObject("Msxml2.XMLHTTP") ;
	xmlHTTP.onreadystatechange = function(){
		if (xmlHTTP.readyState == 4)
		{
			createAppMenuDataEx(xmlHTTP.responseText);
			
			xmlHTTP.abort();
			if (typeof(callBack) == 'function')
			{
				callBack();
			}
		}
	}
	
	xmlHTTP.Open ( "Post", "../webservices/menudata.asmx/fetchLobMenu",true) ;
	xmlHTTP.setRequestHeader( "Content-Type", "application/x-www-form-urlencoded");
	xmlHTTP.Send(lstr ) ;
	
		
}

function ReplaceAll(string,text,by) 
{
// Replaces text with by in string
	var strLength = string.length, txtLength = text.length;
	if ((strLength == 0) || (txtLength == 0)) return string;

	var i = string.indexOf(text);
	if ((!i) && (text != string.substring(0,txtLength))) return string;
	if (i == -1) return string;

	var newstr = string.substring(0,i) + by;

	if (i+txtLength < strLength)
		newstr += ReplaceAll(string.substring(i+txtLength,strLength),text,by);

	return newstr;
}
		
function DecodeXMLAll(strInput)
{
	var str1 = ReplaceAll(strInput,'&amp;','&');
	str1 = ReplaceAll(str1,'&lt;','<');
	str1 = ReplaceAll(str1,'&gt;','>');
	str1 = ReplaceAll(str1,'&apos;','\'');
	
	return str1;
	
}

function createAppMenuDataEx(xmlMenu)
{
	var objXmlHandler = new XMLHandler();
	var tree = (objXmlHandler.quickParseXML(xmlMenu).getElementsByTagName('string')[0].childNodes[0].text);
	tree = DecodeXMLAll(tree);
	loadImage(tree);
	//main1.menuXml = xmlMenu.value;
	
	//alert(tree);
	if (tree == null || tree == 'undefined') return;
	var objXmlHandler = new XMLHandler();
	main1.treeMenu = objXmlHandler.quickParseXML(tree).getElementsByTagName('button');
}

//**************************************
// menu data is passed to create menu on the 2 frames
function createAppMenuData(xmlMenu)
{
	loadImage(xmlMenu.value);
	//main1.menuXml = xmlMenu.value;
	var tree = xmlMenu.value;
	//alert(tree);
	if (tree == null || tree == 'undefined') return;
	var objXmlHandler = new XMLHandler();
	main1.treeMenu = objXmlHandler.quickParseXML(tree).getElementsByTagName('button');
	main1.menuXmlReady = true;
}


//calls web service to get menu data
function callService1()
{
	/*var lstr = "../webservices/menudata.asmx?WSDL";
	if( typeof(myTSMain1.useService) == 'undefined') 
	{
		setTimeout( 'callService1()', 1000);
	}
	else 
	{
		//Getting the currently loged in agency id
		strAgencyId = GetAgencyID();
		
		myTSMain1.useService(lstr.toString(), "TSM");				
		myTSMain1.TSM.callService(createMenuData, "fetchDefaultMenu", strAgencyId);
	}*/	
	strAgencyId = GetAgencyID();
	FetchDefaultMenuXML(strAgencyId);
}

//Added to show User's Full Name without using cookies  
function callServiceForName(uname)
{
    //Changed by Charles on 15-Mar-10 for Multilingual Implementation
    Username = uname.split('^');    
    langCulture = Username[1];
    Username = Username[0]; 
        
	/* 
	var lstr = "../webservices/menudata.asmx?WSDL";
	if( typeof(myTSMain1.useService) == 'undefined') 
	{
		setTimeout( 'callService1()', 1000);
	}
	else 
	{
		//Getting the currently loged in agency id
		strAgencyId = GetAgencyID();
		
		myTSMain1.useService(lstr.toString(), "TSM");				
		myTSMain1.TSM.callService(createMenuData, "fetchDefaultMenu", strAgencyId);
	}*/	
	strAgencyId = GetAgencyID();
	FetchDefaultMenuXML(strAgencyId);
}

// menu data is passed to create menu on the 2 frames
function createMenuData(xmlMenu)
{
	//alert(xmlMenu.value);
	loadImage(xmlMenu.value);
	main1.menuXml = xmlMenu.value;
	var tree = xmlMenu.value;
	//alert(tree);
	if (tree == null || tree == 'undefined') return;
	var objXmlHandler = new XMLHandler();
	main1.treeMenu = objXmlHandler.quickParseXML(tree).getElementsByTagName('button');
	InitiateMenu();
	showTopButton();
	
	var linkUrl			=	main1.treeMenu[0].linkUrl;
	var firstItem		=	'0';
	callItemClicked(firstItem, linkUrl  );
}
function hideVirticalMenu()
{
	top.topframe.main1.mousein=true;
	if(top.botframe.main1 == null) return;
	top.botframe.main1.innerHTML = '<div class="menuBarBottom"></div>';
}

// first level of menu button and its behaviour is written here
function showTopButton()
{
	hideVirticalMenu();
	strHTML = '<html><body leftMargin=0 topMargin=0><table width="100%" border=0 cellspacing="0" cellpadding="0" leftMargin=0 topMargin=0><tr border=0><td border=0 align=right>';
	var ImageLang = '';
	try {
	    if (langCulture != '' && langCulture != "en-US" && langCulture != "zh-SG") 
            ImageLang = '.' + langCulture;
	}
	catch (err) { } 
	for(var i=0; i<main1.treeMenu.length; i++)
	{
	
		if(main1.treeMenu[i].classText == 'undefined' || main1.treeMenu[i].classText == null) main1.treeMenu[i].classText = 'menuButton';
		if(main1.treeMenu[i].enabled == 'undefined' || main1.treeMenu[i].enabled == null) main1.treeMenu[i].enabled = true;
		
		var textItem		= main1.treeMenu[i].name;
		var linkUrl			= main1.treeMenu[i].linkUrl;
		var mouseOverText = 'main1.mousein=true;MM_swapImage(img' + i + ',"/cms/cmsweb/Images' + colorScheme + '/' + main1.treeMenu[i].image + '_a' + ImageLang + '.jpg");';
		var mouseOutText = 'callItemMouseOut("' + i + '");MM_swapImgRestore(img' + i + ',"/cms/cmsweb/Images' + colorScheme + '/' + main1.treeMenu[i].image + ImageLang + '.jpg");';
		var clickText		= 'callItemClicked("' + i + '","' + linkUrl + '")';
		var classText		= main1.treeMenu[i].classText;
		
		if(!main1.treeMenu[i].enabled)
		{
			mouseOverText = '"top.topframe.main1.mousein = true;"';
			clickText = '"return false;"'
		}
		//var imageTag = "<img  id=img"+i+" border=0 src='/cms/cmsweb/Images/"+main1.treeMenu[i].image+'.gif'+"' onMouseOver='ChangeImage("+i+");' onMouseOut ='RestoreImage("+i+");'/>";
		var imageTag = "<img  id=img" + i + " border=0 src='/cms/cmsweb/Images" + colorScheme + "/" + main1.treeMenu[i].image + ImageLang + ".jpg'/>";
		strHTML += '<a id=buttonLink' + i + ' class=' + classText + ' href=\'javascript:' + clickText + '\' onmouseover=' + mouseOverText + ' onmouseout=' + mouseOutText + '>' + imageTag  + '</a>';
	}
	strHTML +='</td>'
	if (GetUserName()!="")
	{
	    //Added by Charles on 15-Mar-10 for Multilingual Implementation
	    var welTxt = 'Welcome';	    
	    try {	        
	        switch (langCulture) {
	            case "pt-BR":
	                welTxt = "Bem-vindo";
	                break;
	            case "en-US":	                
	            default:
	                welTxt = 'Welcome';
	                break;
	        } 
	    }
	    catch (err) { } //Added till here
	    
		//strHTML += '<td>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp </td>'
		//strHTML += "<td align='right'><font color=red size='2px'>" + welTxt + " ! </font><font size='2px'>" + GetUserName() + "</font></td>"
	}
	strHTML += '</tr></table></body></html>';
	document.getElementById('topButton').innerHTML = strHTML;
}

// 2nd level of menu button in top frame is created here
function showVerticalMenu(menuNumber)
{
	main1.mousein = true;
	//if user has selected to fix the menu
	if(top.topframe.main1.fixedButton != null) return;

	//if (top.botframe.main1 == null) return;
	if (top.botframe.document.getElementById('main1') == null) return;
	var strHTML = '<div class="menuBarBottom" id="menuBar' + menuNumber + '">';
	menuLength = main1.treeMenu[menuNumber].childNodes.length;
	
	for (var i = 0; i < menuLength; i++)
	{
		if(main1.treeMenu[menuNumber].childNodes[i].classText == 'undefined' || main1.treeMenu[menuNumber].childNodes[i].classText == null) main1.treeMenu[menuNumber].childNodes[i].classText = 'menuButton';
		if(main1.treeMenu[menuNumber].childNodes[i].enabled == 'undefined' || main1.treeMenu[menuNumber].childNodes[i].enabled == null) main1.treeMenu[menuNumber].childNodes[i].enabled = true;
		
		var textItem		= main1.treeMenu[menuNumber].childNodes[i].name
		var linkUrl			= main1.treeMenu[menuNumber].childNodes[i].linkUrl;
		var clickText		= 'callItemClicked("' + menuNumber + ',' + i + '","' + linkUrl + '")';
		var classText		= main1.treeMenu[menuNumber].childNodes[i].classText;
		var mouseOutText	= 'callItemMouseOut("' + menuNumber + ',' + i + '")';
		var mouseOverText	= 'createDropMenu1(' + menuNumber + ',' + i + ');buttonClick(event,"menu' + menuNumber + i + '");';
		var styleMenus		= 'styleMenu("' + menuNumber + ',' + i + '");';
		mouseOverText		+= styleMenus;
		if(main1.treeMenu[menuNumber].childNodes[i].childNodes.length == 0)
		{
			mouseOverText	= styleMenus + 'hideDropMenu();resetButton(activeButton);activeButton=null;';
		}
		if((!main1.treeMenu[menuNumber].childNodes[i].enabled)||(main1.treeMenu[menuNumber].childNodes[i].enabled == "false"))
		{
			mouseOverText	= 'top.topframe.main1.mousein=true;resetButton(activeButton);activeButton=null;';
			mouseOutText	= 'top.topframe.main1.mousein=false;';
			clickText		= '';
			classText		= "menuButtonDisable";
		}
		strHTML += '<img src="/cms/cmsweb/images/spacer.gif" width=18px height=1px><a id=menuLink' + menuNumber + i + ' class=' + classText + ' href=\'Javascript:' + clickText + '\' onmouseover=' + mouseOverText + ' onmouseout=' + mouseOutText + '>' + textItem + '</a>';
	}
	strHTML += '</div>';
	if(i != 0){
		//strHTML += '<div id="menuFrame" STYLE="background-color: transparent; position: absolute; visibility: hidden "><iframe id="menuIFrame" style="filter:Alpha(Opacity=0);" width="0px" height="0px" top="25px;" left="0px"></iframe></div><div id="menuFrame2"><iframe id="menuIFrame2" style="filter:Alpha(Opacity=0);" width="0px" height="0px" top="25px;" left="0px"></iframe></div><div id="menuFrame3"><iframe id="menuIFrame3" style="filter:Alpha(Opacity=0);" width="0px" height="0px" top="25px;" left="0px"></iframe></div>';
		//strHTML += '<div id="menuFrame" STYLE="background-color: transparent; position: absolute; visibility: visible; z-index:100 "><iframe id="menuIFrame" style="filter:Alpha(Opacity=0);" width="0px" height="0px" top="25px;" left="0px"></iframe></div>';
		//strHTML += '<div id="menuFrame2" STYLE="background-color: transparent; position: absolute; visibility: visible; z-index:100 "><iframe id="menuIFrame2" style="filter:Alpha(Opacity=0);" width="0px" height="0px" top="25px;" left="0px"></iframe></div>';
		//strHTML += '<div id="menuFrame3" STYLE="background-color: transparent; position: absolute; visibility: visible; z-index:100 "><iframe id="menuIFrame3" style="filter:Alpha(Opacity=0);" width="0px" height="0px" top="25px;" left="0px"></iframe></div>';

		strHTML += '<div id="menuFrame" STYLE="background-color: transparent; position: absolute; visibility: hidden; z-index:100 "><iframe id="menuIFrame" style="filter:Alpha(Opacity=0);" width="0px" height="0px" top="25px;" left="0px"></iframe></div>';
		strHTML += '<div id="menuFrame2" STYLE="background-color: transparent; position: absolute; visibility: hidden; z-index:100 "><iframe id="menuIFrame2" style="filter:Alpha(Opacity=0);" width="0px" height="0px" top="25px;" left="0px"></iframe></div>';
		strHTML += '<div id="menuFrame3" STYLE="background-color: transparent; position: absolute; visibility: hidden; z-index:100 "><iframe id="menuIFrame3" style="filter:Alpha(Opacity=0);" width="0px" height="0px" top="25px;" left="0px"></iframe></div>';

//		strHTML += '<div><iframe style="filter:Alpha(Opacity=0);" width="0px" height="0px" top="0px;" left="0px"><div id="menuFrame" STYLE="background-color: transparent; position: absolute; visibility: visible; z-index:100 "><iframe id="menuIFrame" style="filter:Alpha(Opacity=0);" width="0px" height="0px" top="25px;" left="0px"></iframe></div></iframe></div>';
//		strHTML += '<div><iframe style="filter:Alpha(Opacity=0);" width="0px" height="0px" top="0px;" left="0px"><div id="menuFrame2" STYLE="background-color: transparent; position: absolute; visibility: visible; z-index:100 "><iframe id="menuIFrame2" style="filter:Alpha(Opacity=0);" width="0px" height="0px" top="25px;" left="0px"></iframe></div></iframe></div>';
//		strHTML += '<div><iframe style="filter:Alpha(Opacity=0);" width="0px" height="0px" top="0px;" left="0px"><div id="menuFrame3" STYLE="background-color: transparent; position: absolute; visibility: visible; z-index:100 "><iframe id="menuIFrame3" style="filter:Alpha(Opacity=0);" width="0px" height="0px" top="25px;" left="0px"></iframe></div></iframe></div>';
		
	}

	//top.botframe.main1.innerHTML = strHTML;
	top.botframe.document.getElementById('main1').innerHTML = strHTML;
	for(var j = 0; j < menuLength; j++)
	{
		var dropMenuLength = main1.treeMenu[menuNumber].childNodes[j].childNodes.length;
		if(dropMenuLength > 0)
			createDropMenu(menuNumber,j);
	}
}

function createDropMenu(buttonNumber,menuNumber)
{
	var dropMenuLength = main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes.length;
	var strHTML = '<div id="menu' + buttonNumber + menuNumber + '" class="menu" onmouseover="menuMouseover(event)">';
	
	strHTML += '</div>';
	//top.botframe.main1.innerHTML += strHTML ;
	top.botframe.document.getElementById('main1').innerHTML += strHTML;
	for(var l=0; l< dropMenuLength; l++)
	{
		if(main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[l].childNodes.length > 0)
		{
			createSubMenu(buttonNumber,menuNumber,l);
		}
	}
}
function createSubMenu(buttonNumber,menuNumber,itemNumber)
{
	var subMenuLength	= main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes.length;
	var strHTML			= '<div id="menu' + buttonNumber + menuNumber + itemNumber + '" class="menu" onmouseover="menuMouseover(event)">';
	strHTML += "</div>";
	//top.botframe.main1.innerHTML += strHTML;
	top.botframe.document.getElementById('main1').innerHTML += strHTML;
	for(var m=0; m< subMenuLength; m++)
	{
		if(main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[m].childNodes.length > 0)
			createSubSubMenu(buttonNumber,menuNumber,itemNumber,m);
	}
}

function createSubSubMenu(buttonNumber,menuNumber,itemNumber,subItemNumber)
{
	var subSubMenuLength	= main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[subItemNumber].childNodes.length;
	var strHTML				= '<div id="menu' + buttonNumber + menuNumber + itemNumber + subItemNumber + '" class="menu">';
	strHTML += "</div>";
	//top.botframe.main1.innerHTML += strHTML;
	top.botframe.document.getElementById('main1').innerHTML += strHTML;
}

// disable a list of menu items
function disableMenus(parentitem, list)
{
	//alert('disableMenus');
	listArr		= list.split(',');
	parentArr	= parentitem.split(',');
	var i		= 0;
	var item	= 'top.topframe.main1.treeMenu';
	
	for(i=0; i<parentArr.length; i++)
	{
		if(i == 0)
		{
			item		+=	'[' + parentArr[i] + ']';
		}
		else
		{
			item		+=	'.childNodes[' + parentArr[i] + ']';
		}
	}
	if(list == 'ALL')
	{
		if(eval(item))
			listLength	=	eval(item).childNodes.length;
		for(i=0;i<listLength;i++)
		{
			if(eval(item + '.childNodes[' + i + ']'))
				eval(item + '.childNodes[' + i + ']').enabled = false;
		}
	}
	else
	{
		for(i=0;i<listArr.length;i++)
		{
			if(eval(item + '.childNodes[' + listArr[i] + ']'))
				eval(item + '.childNodes[' + listArr[i] + ']').enabled = false;
		}
	}
	
}

//enable a list of menus
function enableMenus(parentitem, list)
{
	//alert('enableMenus');
	listArr		=	list.split(',');
	parentArr	=	parentitem.split(',');
	var i		=	0;
	var item	= 'top.topframe.main1.treeMenu';
	//listLength = 0;//set length every time when function call,changed by Lalit May 04,2011
	for(i=0; i<parentArr.length; i++)
	{
		if(i == 0)
		{
			item		+=	'[' + parentArr[i] + ']';
		}
		else
		{
			item		+=	'.childNodes[' + parentArr[i] + ']';
		}
	}
	if(list == 'ALL')
	{
		if(eval(item))
			listLength	=	eval(item).childNodes.length;
		for(i=0;i<listLength;i++)
		{
			thisItem	=	item + '.childNodes[' + i + ']';
			if(eval(thisItem))
			{
				eval(thisItem).enabled = true;
				if(eval(thisItem).classText)
				{
					if(eval(thisItem).classText.indexOf('Disable') != -1)
						eval(thisItem).classText = null
				}
			}
		}
	}
	else
	{
		for(i=0;i<listArr.length;i++)
		{
			thisItem	=	item + '.childNodes[' + listArr[i] + ']';
			if(eval(thisItem))
			{
				eval(thisItem).enabled = true;
				if(eval(thisItem).classText)
				{
					if(eval(thisItem).classText.indexOf('Disable') != -1)
						eval(thisItem).classText = null
				}
			}
		}
	}
	temp = top.topframe.main1.fixedButton;
	top.topframe.main1.fixedButton = null;
	showVerticalMenu(parseInt(parentitem));
	if((temp + "") != (parseInt(parentitem) + ""))
	{
		if(temp)
		{
			showVerticalMenu(temp);
			top.topframe.main1.fixedButton = temp;
		}
	}
}

// enable a particular menu item
function enableMenu(itemNumber)
{
	//alert('enableMenu')
	itemArr		=	itemNumber.split(',');
	var i		=	0;
	var item	= 'top.topframe.main1.treeMenu';
	
	for(i=0; i<itemArr.length; i++)
	{
		if(i == 0)
		{
			item		+=	'[' + itemArr[i] + ']';
		}
		else
		{
			item		+=	'.childNodes[' + itemArr[i] + ']';
		}
	}
	if(eval(item))
	{
		eval(item).enabled		= true;
		if(eval(item).classText)
		{
			if(eval(item).classText.indexOf('Disable') != -1) 
				eval(item).classText = null;
		}
	}
	/*temp = top.topframe.main1.fixedButton;
	top.topframe.main1.fixedButton = null;
	showVerticalMenu(parseInt(itemNumber));
	if((temp + "") != (parseInt(itemNumber) + ""))
	{
		if(temp)
		{
			showVerticalMenu(temp);
			top.topframe.main1.fixedButton = temp;
		}
	}*/
}

// disable a particular menu item
function disableMenu(itemNumber)
{
	//alert('disableMenu');
	itemArr		=	itemNumber.split(',');
	var i		=	0;
	var item	= 'top.topframe.main1.treeMenu';
	
	for(i=0; i<itemArr.length; i++)
	{
		if(i == 0)
		{
			item		+=	'[' + itemArr[i] + ']';
		}
		else
		{
			item		+=	'.childNodes[' + itemArr[i] + ']';
		}
	}
	if(eval(item))
	{
		eval(item).enabled		= false;
	}
	/*temp = top.topframe.main1.fixedButton;
	top.topframe.main1.fixedButton = null;
	showVerticalMenu(parseInt(itemNumber));
	if((temp + "") != (parseInt(itemNumber) + ""))
	{
		if(temp)
		{
			showVerticalMenu(temp);
			top.topframe.main1.fixedButton = temp;
		}
	}*/
}

function InitiateMenu()
{
	if(readCookie('ckSysId') != null && readCookie('ckSysId').toUpperCase() == 'W001')
	{
		//enableMenu('5,3');
	}
}
//-------------------------------------



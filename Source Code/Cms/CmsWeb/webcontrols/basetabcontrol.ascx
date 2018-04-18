<%@ Control Language="c#" AutoEventWireup="false" Codebehind="basetabcontrol.ascx.cs" Inherits="Cms.CmsWeb.WebControls.BaseTabControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<script language="vbscript" type="text/vbscript">
	function getUserConfirmation(Msg)
	 
	 'Caption is Changed from 'CMS' to 'EbixAdvantage' as per Issue no. 4291
		getUserConfirmation= msgbox (Msg,35,"EbixAdvantage")
	End function
</script>
<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery.js"></script> 
<script language="javascript" type="text/javascript">
<!--
var sSelectedRow = "";
var sSubTabRow = "";
var sAllRows = "";
var dWidth = 100;
var sURL2Navigate = "";

var sTransferData = "";
var prevTabSelected;
var currentTab = "0,0";
var prvsTab = "";
var SuppressChangeTab = false;

// By Pravesh on 28 Apr 2010 to register change event of page control
function RegisterChangeEvent(){
    $(setTimeout(function(){
         if (browser.isIE)
             TabFrame=document.frames("tabLayer")
        else
             TabFrame=document.getElementById("tabLayer")
         if(TabFrame)
            doc = browser.isIE?TabFrame.document:TabFrame.contentDocument;
        if(doc!=null){
            $(doc).ready(function() {
                $("input",doc.forms[0]).bind('change', changeval);
                $("select",doc.forms[0]).bind('change', changeval);
                $("textarea",doc.forms[0]).bind('change', changeval);
            });
        }
        
    },1000));
}
function changeval() {
        if (browser.isIE)
            document.frames("tabLayer").IsChanged = true;
        else
            document.getElementById("tabLayer").IsChanged = true;
    }
function UserConfirmation() {
  
        if (browser.isIE)
        {
     
           Confirmflag = getUserConfirmation('<%=strMessage %>')
           }
        else
        {
       
           Confirmflag = window.confirm('<%=strMessage %>');
           if (Confirmflag)
           Confirmflag=6;
           else 
           Confirmflag=7;
        }
        return Confirmflag;
    }
                  
//function confirm(str){
//   msgScript = 'msgbox("'+str+'","4132")', "vbscript";
//    msgScript = 'alert("'+str+'")';
//   //execScript('n = msgbox("'+str+'","4132")', "vbscript");
//   eval('self.parent.' + msgScript);
//   return(n == 6);
//}
//  
// New parameter is passed " check " for tab urls Gaurav Tyagi
function changeTab(rowTabId, colTabId, check,showMsg) 
{
	//If the SuppressChangeTab is true then we will not create tab
	if (SuppressChangeTab == true)
		return;

	var TmpPrevTab = "";	//Will stores the previously selected tab
	
	//if(document.frames("tabLayer").document.forms[0].CurrentTabSelection)
	var userResponse = 7;
	var validCilent = true;
	var ShowAlert=true;
	currentTab = rowTabId +","+ colTabId;
	if (currentTab == prvsTab){		
		ShowAlert=false; // to stop alert in case tab is same
	}
	TmpPrevTab = prvsTab;
	prvsTab = currentTab;
	//s
	var eventButton;
	/////////////added to support  firefox
	   tabFrame = document.getElementById("tabLayer");
	   if (browser.isIE)
	    tabFrameDoc = tabFrame.document;
	   else
	    tabFrameDoc = tabFrame.contentDocument;
    /////////end here
	/*added by vijay */
	//if (document.frames["tabLayer"].eventButton == undefined || document.frames["tabLayer"].eventButton == null)
	if (tabFrame.eventButton == undefined || tabFrame.eventButton == null)
	{
		//By default we are assuming that event button will be btnSave as per our convention
		//if(document.frames("tabLayer").location.href.indexOf("DEC_PAGE", 0) == -1)
        if (browser.isIE)
            docHref=tabFrame.document.location.href
        else
            docHref=tabFrame.src
		if(docHref.indexOf("DEC_PAGE", 0) == -1)
		{
			if (browser.isIE)
			   btnSave = tabFrame.document.getElementById("btnSave")
			else
			   btnSave = tabFrame.contentDocument.getElementById("btnSave")
			if (btnSave != null)
			{
				eventButton = "btnSave";	
			}
		}
	}
	else
	{
		eventButton = tabFrame.eventButton;	
	}
	
	 
	//eventButton = document.frames("tabLayer").eventButton;	
	 
	var userResponse = 0;
	if(eventButton!=null && prvsTab!="" && ShowAlert==true && '<%=RequireAutoSubmit%>'=='Yes' && showMsg == null)
	{	
		//Supressing itaa
		/* Commented by Pravesh on 28 April 2010 handel alert message by jquery
		if(typeof(document.frames("tabLayer").IsAnyActionPerformedOnPage) == 'function')
		{	
			if (document.frames("tabLayer").ShowSaveMsgAlways != null && document.frames("tabLayer").ShowSaveMsgAlways == true)
			{
				userResponse = getUserConfirmation();
			}
			else
			{
				if(document.frames("tabLayer").IsAnyActionPerformedOnPage())
					userResponse = getUserConfirmation();
			}
		}
		else
		{
			userResponse = getUserConfirmation();
		}
		*/
		
		//if (document.frames("tabLayer").IsChanged == true)
		if (tabFrame.IsChanged == true)
			userResponse = UserConfirmation();//getUserConfirmation();
		
		if (userResponse!=6)
		{
			//Reseting the previously selected tab
			prvsTab = TmpPrevTab;
		}
			
		if (userResponse==6)
		{
			if(tabFrame.Page_ClientValidate)
				validCilent = tabFrame.Page_ClientValidate();
			else
				validCilent = true;
			 
			if(validCilent)
			{
				//the following code has been added by sumit on 09/20/05
				//refresh the header elements when the user moves from one tab to another and wants to save changes				
				if(tabFrameDoc.getElementById("txtCUSTOMER_FIRST_NAME")!= null)
					RefreshClientTop();
				//document.frames("tabLayer").document.forms[0].OverrideTabNumber.value = (rowTabId*iTabsPerRow)+ colTabId;
				//document.frames("tabLayer").document.getElementById(eventButton).click();
				
				//call the postBack function only when it is there..modified by sumit on 09/20/05
				//if (document.frames("tabLayer").__doPostBack)
				//	document.frames("tabLayer").__doPostBack(eventButton,'');			
				
				if (tabFrameDoc.getElementById("btnSave"))
					tabFrameDoc.getElementById("btnSave").click();			
				
				//Making the flag on, so that if in between changeTab function call will not create tab
				SuppressChangeTab = true;
				//Making the expression for changing the tab,
				//We will call this expression in settimeout after 3 seccond
				//During this duration add page will postback and will get saved.
				//In the first line of this expression, we are disabling the SuppressChangeTab
				//So that changeTab function will be able to create tabs again
				var exp = 'SuppressChangeTab = false;'
				    + 'tablFrame = document.getElementById("tabLayer");'
	                + ' if (browser.isIE)'
	                + ' tablFrameDoc = tablFrame.document;'
	                + ' else '
	                + ' tablFrameDoc = tablFrame.contentDocument;'
				    //+ 'if(document.frames("tabLayer").document.getElementById("hidFormSaved") != null && document.frames("tabLayer").document.getElementById("hidFormSaved").value == "1")'
				    + 'if(tablFrameDoc.getElementById("hidFormSaved") != null && tablFrameDoc.getElementById("hidFormSaved").value == "1")'
					+ ' changeTab(' + rowTabId + ',' + colTabId + ', null, 1);'
					+ ' else '
					+ ' changeTab(' + rowTabId + ',' + colTabId + ', null, 1);';
				setTimeout(exp,1000);
				return false;
				
				
				
				//document.frames("tabLayer").__doPostBackTest(eventButton,'');
				
				//document.frames("tabLayer").Page_ClientValidate();
				//return;
			}
			else
			{
				/*Some validators are fired*/
				prvsTab = TmpPrevTab;//Done for Itrack Issue 5564(Point 2) on 7 April 09
				return false;
			}
		}
	}	

	if (userResponse!=2 && validCilent)
	{
		prvsTab = currentTab;
		if (document.all("lblConfirmMsg") && sAllRows != "") document.all("lblConfirmMsg").innerHTML = '';
				if (iRowsPerTbl != 1) { 
			iTabWidth = Math.floor(100/iTabsPerRow);
			iTabWidth = iTabWidth.toString() + "%";
		}
		else
			iTabWidth = iTabWidth.toString() + "px";
		sSelectedRow = "";
		sAllRows = "";
		finAllRows="";
		sSubTabRow = "";
		flag =true;
		currentScreenId="";
		for(r=0; r<iRowsPerTbl; r++) 
		{
			dWidth = ((r+1)*iTabsPerRow - arrMainTab.length/4) <= 0 ? 100 : 100 - ((r+1)*iTabsPerRow - arrMainTab.length/4)*100/iTabsPerRow;
			if (rowTabId == r) 
			{
					sAllRows = "";
					//sSelectedRow = "\n<table cellpadding=0 cellspacing=0 border=0 width='"+dWidth+"%'>\n<tr>";
					sSelectedRow = "\n<table cellpadding=0 cellspacing=0 border=0>\n<tr>";
					for(c=0; c<iTabsPerRow; c++)
					{
						//Making the tab, only if hide flag is not equal to 'Y'
						//if (arrMainTab[r*iTabsPerRow*4 + c*4 + 3] != 'Y')
						{
							sSelectedRow += "\n<td align='center' width='" + iTabWidth + "' nowrap>"
							//code by vishal to show the image					
							if (rowTabId == r && colTabId == c) 
							{
								//Code commented by Vijay joshi On dated 2 March because pages gets reloaded, while reclicking on Selected/Highlighted tab
								//sSelectedRow += "\n<div id='_tab" + (r*iTabsPerRow + c) + "' class='HeaderEffect' onclick=\"javascript: changeTab(" + r + ", " + c + ");\">" ;
								//To solve the above stated problem, we are removing the javascript function call on click event
								currentScreenId=arrScreenIds[r*iTabsPerRow + c];
								sSelectedRow += "\n<div id='_tab" + (r*iTabsPerRow + c) + "' class='TabRowSel'>" ;
								
								if (arrMainTab[r*iTabsPerRow*4 + c*4 + 3] == 'Y')
								{
									sSelectedRow += "\n<img src='/cms/cmsweb/images/Complete.png' alt=''>&nbsp;"
								}
								else if (getWorkFlowValue(arrScreenIds[r*iTabsPerRow + c])==true)
								    sSelectedRow += "\n<img src='/cms/cmsweb/images/Complete.png' alt=''>&nbsp;"
								sSelectedRow += arrMainTab[r*iTabsPerRow*4 + c*4] + "</div>";
								sURL2Navigate = arrMainTab[r*iTabsPerRow*4 + c*4 + 1]
								if (arrMainTab[r*iTabsPerRow*4 + c*4 + 2] == 1) makeChildBar(r*iTabsPerRow + c + 1);
							}
							else
							{
								sSelectedRow += "\n<div id='_tab" + (r*iTabsPerRow + c) + "' class='TabRow' onclick=\"javascript: changeTab(" + r + ", " + c + ");\">";
								if (arrMainTab[r*iTabsPerRow*4 + c*4 + 3] == 'Y')
								{
									sSelectedRow += "\n<img src='/cms/cmsweb/images/Complete.png' alt=''>&nbsp;"
								}
								else if (getWorkFlowValue(arrScreenIds[r*iTabsPerRow + c])==true)
								    sSelectedRow += "\n<img src='/cms/cmsweb/images/Complete.png' alt=''>&nbsp;"
								sSelectedRow += arrMainTab[r*iTabsPerRow*4 + c*4] + "</div>";
							}
							sSelectedRow += "\n</td>";
						}
						if ((r*iTabsPerRow + c + 1)*4 >= arrMainTab.length) break;
					}
					//if (iRowsPerTbl == 1) sSelectedRow += "\n<td width='90%' valign='bottom'><div style=\"Border-Bottom: 1px outset\">&nbsp;</div></td>";
					sSelectedRow += "\n</tr>\n</table>";
					sSelectedRow += sSubTabRow;
			}
			else
			{
				sAllRows="";
				sAllRows += "\n<table class='tableeffect' cellpadding=0 cellspacing=0 border=0 width='"+dWidth+"%'>\n<tr>";
				for(c=0; c<iTabsPerRow; c++)
				{
					sAllRows += "\n<td align='center' width='" + iTabWidth + "' nowrap>"				
					sAllRows += "\n<div id='_tab" + (r*iTabsPerRow + c) + "' class='SubTabRow' onclick=\"javascript: changeTab(" + r + ", " + c + ");\">";
					if (arrMainTab[r*iTabsPerRow*4 + c*4 + 3] == 'Y')
					{
						sAllRows += "\n<img src='/cms/cmsweb/images/Complete.png' alt=''>&nbsp;"
					}
					else if (getWorkFlowValue(arrScreenIds[r*iTabsPerRow + c])==true)
					    sAllRows += "\n<img src='/cms/cmsweb/images/Complete.png' alt=''>&nbsp;"
					sAllRows+= arrMainTab[r*iTabsPerRow*4 + c*4] + "</div>";
					sAllRows += "\n</td>";
					if ((r*iTabsPerRow + c + 1)*4 >= arrMainTab.length) break;
				}
				
				if (iRowsPerTbl == 1) sAllRows += "\n<td width='90%' valign='bottom'><div style=\"Border-Bottom: 1px outset\">&nbsp;</div></td>";
				sAllRows += "\n</tr>\n</table>";
			}
			finAllRows= sAllRows+finAllRows;	
		}

		finAllRows += sSelectedRow;
	    //alert(finAllRows);
		<%=sCtlId%>_mainTabTbl.innerHTML = finAllRows;
		//setTimeout("navigate(sURL2Navigate)",30);
		if(check==null)
		navigate(sURL2Navigate);
		SetWorkFlowNextPage(rowTabId,colTabId,currentScreenId);
	}
}
//By Pravesh K chandel on 4 Aug 2010 to set workflow if exists
function SetWorkFlowNextPage(rowNumber,currentPage,currentScreenId)
{
    try
    {
    if (workflow)
	    {
	        if (!workflow.tree) 
	        {
	        setTimeout("SetWorkFlowNextPage(" + rowNumber + "," + currentPage + ")",500);
	        return;
	        }
	        if (workflow.ScreenID.indexOf('224')!=-1)
	        {
	            workflow.NextPage = Math.abs(workflow.tree[iTabsPerRow * Math.abs(rowNumber) +  Math.abs(currentPage)].order) ;//iTabsPerRow * Math.abs(rowNumber) +  Math.abs(currentPage) + 1;
	        }
	        else
	        {
	        getWorkFlowValue(currentScreenId,'Y');
	        }    
	    }   
	}
	catch(er)
	{}    
}
function getWorkFlowValue(screenId,isNextPage)
{
          var i			=	0;
         var value='';
        if (screenId==null) return false;
    try{
        if (workflow)
	    {
	      
	        if (workflow.tree) 
	        {
               var treeLength	=	workflow.tree.length;
                for(i=0; i<treeLength; i++)
                {
                    if(workflow.tree[i].id==screenId)
                    {
	                    
	                    value = workflow.tree[i].value;
	                    if (isNextPage!=null)
	                    workflow.NextPage=i+1;
	                    break;
                    }
                }
	        }
	    }
	    if (value=='true')
	    return true;
	    else
	    return false;
	 }
	 catch(er)
	  {} 
}
//=====================================


//the following function has been added by sumit on 09/20/05 to refresh the top header elements
//whenever the user moves from one tab to another
function RefreshClientTop()
{	
	var doc = this.parent.document;
	var objFrameDoc=document.frames("tabLayer").document;
	
	
	document.getElementById("cltClientTop_lblFullName").innerHTML = objFrameDoc.getElementById("txtCUSTOMER_FIRST_NAME").value
					+ " " + objFrameDoc.getElementById("txtCUSTOMER_MIDDLE_NAME").value 
					+ " " + objFrameDoc.getElementById("txtCUSTOMER_LAST_NAME").value;
						
	document.getElementById("cltClientTop_lblClientType").innerHTML = objFrameDoc.getElementById("cmbCUSTOMER_TYPE").options[objFrameDoc.getElementById("cmbCUSTOMER_TYPE").selectedIndex].text;
				
	document.getElementById("cltClientTop_lblClientStatus").innerHTML = objFrameDoc.getElementById("lblIS_ACTIVE").innerText;
	document.getElementById("cltClientTop_lblClientPhone").innerHTML = objFrameDoc.getElementById("txtCUSTOMER_BUSINESS_PHONE").value;
	document.getElementById("cltClientTop_lblClientTitle").innerHTML = objFrameDoc.getElementById("cmbPREFIX").options[objFrameDoc.getElementById("cmbPREFIX").selectedIndex].text;
				
	var add = "";
	add = objFrameDoc.getElementById("txtCUSTOMER_ADDRESS1").value + " "
		+ objFrameDoc.getElementById("txtCUSTOMER_ADDRESS2").value + " "
		+ objFrameDoc.getElementById("txtCUSTOMER_CITY").value + " "		
		+ objFrameDoc.getElementById("cmbCUSTOMER_STATE").options[objFrameDoc.getElementById("cmbCUSTOMER_STATE").selectedIndex].text + " "		
		+ objFrameDoc.getElementById("cmbCUSTOMER_COUNTRY").options[objFrameDoc.getElementById("cmbCUSTOMER_COUNTRY").selectedIndex].text + " "
		+ objFrameDoc.getElementById("txtCUSTOMER_ZIP").value;
		
		
		document.getElementById("cltClientTop_lblClientAddress").innerHTML = add;	
}

//====================================

function makeChildBar(tabno) {
	var tmpArr = eval("arrSubTab_" + tabno);
	sSubTabRow = "\n<table cellpadding=0 cellspacing=0 width='100%' class='SubTabRowTbl'>\n<tr><td height='5px'></td></tr>\n<tr>";
	for(i=0; i<tmpArr.length; i++) {
		sSubTabRow += "\n<td align='center' valign='bottom' width='" + iSubTabWidth + "px' nowrap>"
		if (i == 0) { 
			sSubTabRow += "\n<div id='SubTab" + tabno + "_" + (Math.floor(i/2)+1) + "' class='SubTabRowSel' onclick=\"javascript: changeSubTab(this.id, '" + tmpArr[i+1] + "');\">" + tmpArr[i] + "</div>";		
			prevTabSelected = "SubTab" + tabno + "_1"; 
			sURL2Navigate = tmpArr[i+1]; 
		}
		else
			sSubTabRow += "\n<div id='SubTab" + tabno + "_" + (Math.floor(i/2)+1) + "' class='SubTabRow' onclick=\"javascript: changeSubTab(this.id, '" + tmpArr[i+1] + "');\">" + tmpArr[i] + "</div>";		
		sSubTabRow += "\n</td>"
		i++;
	}
	sSubTabRow += "\n<td width='90%'valign='bottom'><div style=\"Border-Bottom: 1px outset\">&nbsp;</div></td>";
	sSubTabRow += "\n</tr></table>";
}

function changeSubTab(objId, navURL) {
	if(prevTabSelected != null)
		document.all(prevTabSelected).className = "SubTabRow";
	document.all(objId).className = "SubTabRowSel";
	prevTabSelected = objId;
	navigate(navURL);
}

function SetTabSaveStatus(rowNo, colNo, status)
{
	if (status == "Y")
	{
		arrMainTab[rowNo*iTabsPerRow*4 + colNo*4 + 3] = "Y";
	}
	else
	{
		arrMainTab[rowNo*iTabsPerRow*4 + colNo*4 + 3] = "N";
	}
	if (status == "Y")
	{
	var tabNum=rowNo*iTabsPerRow  + colNo;
	var tabHTML= <%=sCtlId%>_mainTabTbl.innerHTML; 
	var tabIndex = tabHTML.indexOf("id=_tab"+tabNum);
	var thisTabHTML=tabHTML.substring(tabIndex);
	var divIndex=thisTabHTML.indexOf("</DIV>")+5
	var thisDivHtml=thisTabHTML.substring(0,divIndex);
	if (thisDivHtml.indexOf("/cms/cmsweb/images/Complete.png")==-1)
	    thisDivHtml=thisDivHtml.replace(">",">\n<img src='/cms/cmsweb/images/Complete.png' alt=''>&nbsp;")
    finaHTML=  tabHTML.substring(0,tabIndex) +  thisDivHtml + tabHTML.substring(tabIndex+divIndex); 	    
	 <%=sCtlId%>_mainTabTbl.innerHTML=finaHTML
	} 
	
}
// added by Pravesh K Chandel
/*
function getWorkFlowScreenStatus(ScreenId)
{
    flag=false;
    if(top.botframe.workflowXML)
	{
		workflowXML = top.botframe.workflowXML;
	}
    if ((workflowXML == null || workflowXML == 'undefined') || workflowXML == '')
		{
			return flag;
		}
		
		var iworkflow = objXmlHandler.quickParseXML(workflowXML).getElementsByTagName('screen');
		if(iworkflow!=null) flag = true;
		stratIndex=0;
		while(stratIndex < iworkflow.tree.length)
		{
				for(loopI = stratIndex; loopI<= endIndex; loopI++ )
				{
					
					if(workflow.tree[loopI].value == 'true')
					{
						strHTML	+=	'<td class="midcolorp">';
						strHTML +=	'<img src="/cms/cmsweb/images/tick2.gif">'
						strHTML +=	''
						strHTML	+=	'</td><td class="midcolorp">' 										
					}
					else
					{
						strHTML	+=	'<td class="midcolorq">';
						workflow.tree[loopI].id + '" ';
						strHTML +=	'<img src="/cms/cmsweb/images/minus.gif">'	
						strHTML +=	''
						strHTML	+='</td><td class="midcolorq">' 							
					}	 
					strHTML		+=	workflow.tree[loopI].desc + '</td>';
				}
			
		}			
		return flag;			
}*/
function navigate(navURL)
{
	if (navURL != "")
	{
		/*//Start: ****default customer case hiding individual contact id if contact type is personal(6)
			if(top.frames[1].strXML !=null && top.frames[1].strXML!="")
			{
				var objXmlHandler = new XMLHandler();
				var nodeValue="";
				var nodeName="";
				var cid="";
				var tree = (objXmlHandler.quickParseXML(top.frames[1].strXML).getElementsByTagName('Table')[0]);
				
				var i=0;
			
				for(i=0;i<tree.childNodes.length;i++)
				{
					if(!tree.childNodes[i].firstChild) continue;
					nodeName = tree.childNodes[i].nodeName;
					if(nodeName!=top.frames[1].addPageId || nodeName!="CONTACT_ID")//"CONTACT_TYPE_ID"
						continue;
					else
						{
							if(nodeName==CONTACT_TYPE_ID)
								nodeValue = tree.childNodes[i].firstChild.text;
							else
								cid = tree.childNodes[i].firstChild.text;
							break;
						}
				}
			}
			//End:**** default customer case hiding individual contact id if contact type is personal(6)*/
		var strQueryString = "";
		
		if(document.getElementById('hidlocQueryStr'))
		{
			strQueryString = document.getElementById('hidlocQueryStr').value;
			//alert(document.frames("tabLayer").document.location.href);
			//alert('jj - ' + document.getElementById('hidlocQueryStr').value);
		}
		if (browser.isIE)
		{            
		if(document.frames("tabLayer").location.href.indexOf("DEC_PAGE", 0) == -1)
		{
			if(navURL.indexOf("?") > 0)
			{
			    qry=BaseTabControl.GetQValue(navURL.substring(navURL.indexOf("?")+1),strQueryString + '&transferdata=' + sTransferData).value;
				document.frames("tabLayer").document.location.href = navURL.substring(0,navURL.indexOf("?")) + qry;
			}
			else
				document.frames("tabLayer").document.location.href = navURL + BaseTabControl.GetQValue("transferdata=" + sTransferData,"").value;
		}
		else
		{
			if(navURL.indexOf("?") > 0)
			{
		        qry=BaseTabControl.GetQValue(navURL.substring(navURL.indexOf("?")+1),strQueryString + '&transferdata=' + sTransferData).value;
				//document.frames("tabLayer").location.href = navURL + strQueryString + "&transferdata=" + sTransferData;
				document.frames("tabLayer").location.href = navURL.substring(0,navURL.indexOf("?")) + qry;
			}	
			else
			    document.frames("tabLayer").location.href = navURL + BaseTabControl.GetQValue("transferdata=" + sTransferData,"").value;
				//document.frames("tabLayer").location.href = navURL + "?transferdata=" + sTransferData;
		}
		}
		else
		{
    		tabFrame = document.getElementById("tabLayer");
            if(tabFrame.src.indexOf("DEC_PAGE", 0) == -1)
		    {
			    if(navURL.indexOf("?") > 0)
			    {
			        qry=BaseTabControl.GetQValue(navURL.substring(navURL.indexOf("?")+1),strQueryString + '&transferdata=' + sTransferData).value;
				    tabFrame.src = navURL.substring(0,navURL.indexOf("?")) + qry;
			    }
			    else
				    tabFrame.src = navURL + BaseTabControl.GetQValue("transferdata=" + sTransferData,"").value;
		    }
		    else
		    {
			    if(navURL.indexOf("?") > 0)
			    {
		            qry=BaseTabControl.GetQValue(navURL.substring(navURL.indexOf("?")+1),strQueryString + '&transferdata=' + sTransferData).value;
				    tabFrame.src = navURL.substring(0,navURL.indexOf("?")) + qry;
			    }	
			    else
			       tabFrame.src = navURL + BaseTabControl.GetQValue("transferdata=" + sTransferData,"").value;
		    }
		}
		 RegisterChangeEvent();
	}
}

//-->
</script>
<asp:Panel id="mainTabTbl" runat="server"></asp:Panel>
<asp:Literal id="firstTabScript" runat="server"></asp:Literal>
<asp:Label ID="textlabel" Runat="server"></asp:Label>

<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WorkFlow.ascx.cs" Inherits="Cms.CmsWeb.WebControls.WorkFlow" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%if(Display){%>
<link href="/cms/cmsweb/css/WorkFlow<%=ColorScheme%>.css" type="text/css" rel="stylesheet"/>
<%if(IsTop){%>
<script language="javascript" type="text/javascript">
	var	mouseInWorkflow	=	false;
	var objXmlHandler = new XMLHandler();
	var workflowXML = '<%=workflowXML%>';
	var workflow = {};
	var EnParam;
	var Popupurl = "";
	var ShowQuoteStatus = "";
	workflow.VisitingTab	=	0;
	workflow.Page			=	0;
	workflow.ScreenID		=	'<%=ScreenID%>';
	workflow.imageIcon		=	new Array();
	workflow.imageIcon[0]	=	new Image();
	workflow.imageIcon[1]	=	new Image();
	workflow.RowLength		=	0;
	
	workflow.imageIcon[0].src	=	"/cms/cmsweb/Images/plus2.gif";
	workflow.imageIcon[1].src	=	"/cms/cmsweb/Images/minus2.gif";
	
	function sortWorkflow()
	{
		var treeLength		=	workflow.tree.length;
		var tempOrder,tempId,tempValue,tempDesc;
		var j				=	0;
		for(var i=0; i<treeLength; i++)
		{

			for(j=i+1; j<treeLength; j++)
			{
			
				if(parseInt(workflow.tree[j].order) < parseInt(workflow.tree[i].order))
				{
					tempOrder	=	workflow.tree[i].order;
					tempId		=	workflow.tree[i].id;
					tempDesc	=	workflow.tree[i].desc;
					tempValue	=	workflow.tree[i].value;
					tempLink	=	workflow.tree[i].menu_link;
					tempTabNumber = workflow.tree[i].tabNumber;
					
					workflow.tree[i].order		=	workflow.tree[j].order;
					workflow.tree[i].id			=	workflow.tree[j].id;
					workflow.tree[i].value		=	workflow.tree[j].value;
					workflow.tree[i].desc		=	workflow.tree[j].desc;
					workflow.tree[i].menu_link	=	workflow.tree[j].menu_link;
					workflow.tree[i].tabNumber	=	workflow.tree[j].tabNumber;
					
					workflow.tree[j].order		=	tempOrder;
					workflow.tree[j].id			=	tempId;
					workflow.tree[j].value		=	tempValue;
					workflow.tree[j].desc		=	tempDesc;
					workflow.tree[j].menu_link	=	tempLink;
					workflow.tree[j].tabNumber	=	tempTabNumber;	
						
				}
			}
		}
}
        //for Account Inquiry    
        function ShowAccountSearch() {
            if (parent.refSubmitWin != null) {
                parent.refSubmitWin.close();
            }
            parent.refSubmitWin = window.open('/cms/Account/Aspx/AR_Inquiry_Info.aspx?CalledFrom=TopFrame&CUSTOMER_ID=' + '<%=CustomerId %>' + '&POLICY_ID=' + '<%=PolicyId %>' + '&POLICY_VERSION_ID=' + '<%=PolicyVersionId %>' + '&CALLEDFOR=WORKFLOW', 'EbixAdvantageAI', 'resizable=yes,scrollbars=no,left=150,top=50,width=800,height=500');
        }
        //Open transaction log
        function TransactionLog(obj, CustomerId) {
            var TrEnPrm = '<%=TranLogEnQuery %>';  //encrypt param
            var CustomerId = '<%=CustomerId %>';
            parent.refSubmitWin = window.open('/Cms/CmsWeb/Maintenance/TransactionLogIndex.aspx' + TrEnPrm, 'TransactionLog', 'resizable=yes,scrollbars=yes,left=150,top=50,width=800,height=500');
        }

        function CreateWorkFlow() {
            //debugger;
	    //Added by Lalit for add icons on workflow
	    var CustomerId = '<%=CustomerId %>';
	    var PolicyId = '<%=PolicyId %>';
	    var PolicyVersionId = '<%=PolicyVersionId %>';
	    var verifyStatus = false;
	    if (ShowQuoteStatus != null && ShowQuoteStatus!='undefined') {
	        if (ShowQuoteStatus == "1") {
	            var verifyStatus = true;
	        }
	    }
	    var QuoteDisplay = '';
	    if (verifyStatus)
	        QuoteDisplay = 'inline';
	    else
	        QuoteDisplay = 'none';
	        
	
		if ((workflowXML == null || workflowXML == 'undefined') || workflowXML == '')
		{
			if(this.parent.top.botframe)
			{
				setTimeout('this.parent.top.botframe.CreateWorkFlow()',1);
			}
			return;
		}
		workflow.tree = objXmlHandler.quickParseXML(workflowXML).getElementsByTagName('screen');
		
		sortWorkflow();	//Sorting the workflow
		SetWorkflowPage();
		
		var strWorkflowMenu	=	'';
		var strHTML			=	'';
		var colLength		=	10;
		var flowDirection	=	0; // 0 means asc, 1 means desc
		var treeLength		=	workflow.tree.length;
		var stratIndex		=	0;
		var endIndex		=	-1;
		var loopI;

		var strverify = '<%=strVerify%>';
		var strCustomer = '<%=strCustomer%>';
		var strTrans = '<%=strTrans%>';
		var strAccount = '<%=strAccount%>';
		var strQuote = '<%=strQuote%>';
		//debugger;	
        //Encripted query string 
		EnParam = '<%=CustMgrEnQuery %>';
		var QueryString = '/Cms/client/aspx/CustomerManagerIndex.aspx'+ EnParam;

		strHTML				='<TABLE  class="tableeffectTopHeader" cellSpacing="0" cellPadding="0" width="100%" border="0">';
		strHTML	+='<tr><td colspan=' + colLength*2 + ' >';
		strHTML	+='<table border="0" cellspacing="0" cellpadding="0" width="100%" class=headereffectCenter>';
		strHTML +='<tr><td width="80%">' + '<%=WorkflowHeader%>' + '</td>';
		strHTML += '<td width="2%"><a href="' + QueryString + '"><img src="/cms/cmsweb/images1/Customer_Ass.gif" visible="true"  border="0" align="middle" alt="' + strCustomer + '" height="15" /></a></td>';
		strHTML +='<td width="2%"><a href=javascript:VerifyPolicy(' + CustomerId + ',' + PolicyId + ',' + PolicyVersionId + ')><img src="/cms/cmsweb/Images/Rule_ver2.png" visible="true"  border="0" align="middle" alt="' + strverify + '" height="16" /></a></td>';
		strHTML += '<td width="2%" style="display:' + QuoteDisplay + '"><a href=javascript:ShowQuote(' + CustomerId + ',' + PolicyId + ',' + PolicyVersionId + ')><img src="/cms/cmsweb/Images/quote.gif" style="display:"' + QuoteDisplay + '"  border="0" align="middle" alt="' + strQuote + '" height="15" /></a></td>';
		strHTML +='<td width="2%"><a href=javascript:TransactionLog(this,' + CustomerId + ') ><img src="/Cms/CmsWeb/images/TransactionLog4.png" style="display:inline"  border="0" align="middle" alt="' + strTrans + '" height="16" /></a></td>';
		strHTML +='<td width="2%"><a href=javascript:ShowAccountSearch() ><img src="/Cms/CmsWeb/images/AccountEnquiry2.png" style="display:inline"  border="0" align="middle" alt="' + strAccount + '" height="20" /></a></td>';
       
		
		//strHTML += '<td width="5%" align="right"></td>';  //<img id="workFlowLink" src="' + workflow.imageIcon[0].src + '" onclick="javascript:showHideWorkFlow();" style="cursor:hand;"></td></tr>';
		strHTML += '<td width="10%" align="right">';
		strHTML += '<a id="workflowGoTo" onmouseover="mouseInWorkflow=true;showWorkflowOptions();" onmouseout="mouseInWorkflow=false;" href="JavaScript:MoveToNextWorkflow();"><b>' + '<%=strNext %>' + '&gt;&gt;</b> &nbsp;</a></td>';

		strHTML += '</tr></table>';
		strHTML				+=	'</td></tr>';
		workflow.RowLength	=	0;
		while(stratIndex < workflow.tree.length)
		{
			workflow.RowLength++;
			
			if(workflow.RowLength > 2)
			{
				strHTML				+=	'<tr id="W' + workflow.RowLength + '" style="display:none">'
			}
			else
			{
			    strHTML += '<tr style="display:none">'
			}
			stratIndex			=	endIndex + 1;
			endIndex			=	(stratIndex + colLength)-1;
			if(endIndex > (treeLength-1))
				endIndex		=	treeLength - 1;
				
			if(flowDirection == 0)
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
					
					if(loopI == workflow.NextPage)
					{
						//Making the currently open screen as selected in goto menu
						workflowClass = 'workflowLink workflowActiveLink';
					}
					else
					{
						workflowClass = 'workflowLink';
					}
					
					//Making the goto menu
//					if((workflow.tree[loopI].tabNumber > 0) && (workflow.tree[loopI].id.substring(0,workflow.tree[loopI].id.indexOf('_')) != workflow.ScreenID.substring(0,workflow.ScreenID.indexOf('_') == -1 ? workflow.ScreenID.length : workflow.ScreenID.indexOf('_'))) && (workflow.tree[loopI].id.substring(workflow.tree[loopI].id.indexOf('_'))))
//					{
//						
//						//Making disable menu
//						workflowClass = 'workflowLink workflowDeactiveLink';
//						strWorkflowMenu	+=	'<a id="workflow' + loopI + '" class="' + workflowClass + '">' + workflow.tree[loopI].desc + '</a>';
//					}
//					else
					{
						//Making enable menu
						strWorkflowMenu	+=	'<a id="workflow' + loopI + '" class="' + workflowClass + '" href="javascript:goToThisWorkflow(' + loopI + ');">' + workflow.tree[loopI].desc + '</a>';
					}
					
					
				}
				flowDirection	=	1;
			}
			else
			{
				// This case is for right - to - left, but since images are not there i m making it left to right
				//for(loopI = endIndex; loopI >= stratIndex; loopI--)
				for(loopI = stratIndex; loopI <= endIndex; loopI++)
				{
					
					if(workflow.tree[loopI].value == 'true')
					{
						strHTML		+=	'<td class="midcolorp">'; //<input disabled type="checkbox" id="' + workflow.tree[loopI].id + '" ';
						strHTML +=	'<img src="/cms/cmsweb/images/tick2.gif" valign="absmiddle" align="absmiddle">'
						strHTML +=	''
						strHTML	+=	'</td><td class="midcolorp">' 
					}
					else
					{
						strHTML		+=	'<td class="midcolorq">'; //<input disabled type="checkbox" id="' + 

					    workflow.tree[loopI].id + '" ';
						strHTML +=	'<img src="/cms/cmsweb/images/minus.gif" valign="absmiddle" align="absmiddle">'						
						strHTML +=	''
						strHTML	+=	'</td><td class="midcolorq">' 
					}	
					
						
					strHTML		+=	workflow.tree[loopI].desc + '</td>';
					loopNum		=	stratIndex + endIndex - loopI;
					
					
					if(loopI == workflow.NextPage)
					{
						//Making the currently open screen as selected in goto menu
						workflowClass = 'workflowLink workflowActiveLink';
					}
					else
					{
						workflowClass = 'workflowLink';
					}
					
					//Making the goto menu
//					if((workflow.tree[loopI].tabNumber > 0) && (workflow.tree[loopI].id.substring(0,workflow.tree[loopI].id.indexOf('_')) != workflow.ScreenID.substring(0,workflow.ScreenID.indexOf('_') == -1 ? workflow.ScreenID.length : workflow.ScreenID.indexOf('_'))))
//					{
//						//Disabled menu
//						workflowClass = 'workflowLink workflowDeactiveLink';
//						strWorkflowMenu	+=	'<a id="workflow' + loopI + '" class="' + workflowClass + '">' + workflow.tree[loopI].desc + '</a>';
//					}
//					else
					{
						//Enabled menu
						strWorkflowMenu	+=	'<a id="workflow' + loopNum + '" class="' + workflowClass + '" href="javascript:goToThisWorkflow(' + loopI + ');">' + workflow.tree[loopI].desc + '</a>';
					}
					
				}
				flowDirection	=	0;
			}
			for(loopI = (endIndex - stratIndex) + 1; loopI < colLength; loopI++)
			{
				strHTML		+=	'<td class="midcolora"></td><td class="midcolora"></td>';
			}
			strHTML			+=	'</tr>'
		}
		/*strHTML += '<tr><td class="midcolora" colspan=2>';
		if((workflow.Page)>0)
		{
		    strHTML += '<a id="workflowPrev" class="DataRow" href="JavaScript:goToThisWorkflow(' + (workflow.Page - 1) + ');">' + '<%=strPrevious %>' + '</a>';
		}
		else
		{
		    strHTML += '<span disabled=false>' + '<%=strPrevious %>' + '</span>';
		}
		strHTML				+= '</td><td class="midcolorr"  colspan=' + (colLength) + '>';
		strHTML += '<a id="workflowGoTo" class="DataRow" onmouseover="mouseInWorkflow=true;showWorkflowOptions();" onmouseout="mouseInWorkflow=false;" href="#"><b>' + '<%=strGoTo %>' + ' &gt;&gt;</b> &nbsp;</a></td><td colspan=4 class="midcolorr">';
		
		if(workflow.NextPage < workflow.tree.length)
		{
		    strHTML += '<a id="workflowNext" class="DataRow" href="JavaScript:goToNextWorkflow();">' + '<%=strNext %>' + '</a>';
		}
		else
		{
		    strHTML += '<span disabled=true>' + '<%=strNext %>' + '</span>'
		}
		strHTML				+= '</td></tr>';
		*/
		strHTML				+=	'</table>';
		document.getElementById('divWorkFlow').innerHTML	=	strHTML;
		document.getElementById('workflowOptions').innerHTML = strWorkflowMenu;
		
	}
	
	function showHideWorkFlow()
	{
		for(var j=workflow.RowLength; j > 2; j--)
		{
			if(document.getElementById('W' + j).style.display	==	'inline')
			{
				document.getElementById('W' + j).style.display	=	'none';
			}
			else
			{
				document.getElementById('W' + j).style.display	=	'inline';
			}
		}
		if(document.getElementById('workFlowLink').src == workflow.imageIcon[0].src)
		{
			document.getElementById('workFlowLink').src	=	workflow.imageIcon[1].src;
		}
		else
		{
			document.getElementById('workFlowLink').src	=	workflow.imageIcon[0].src;
		}
	}
	
	//This function is used to check whether the page is curreltly visible or not
	function isPageVisible()
	{
		if ( document.getElementById("tabLayer") )
		{
			if(document.getElementById("tabLayer").style.visibility=="hidden")
			{
			    alert('<%=strSelectAdd %>');
				return false;
			}
		}
	
		if(document.getElementById("tabCtlRow"))
		{
		
			if(document.getElementById("tabCtlRow").style.visibility=="hidden")
			{
			    alert('<%=strSelectAdd %>');
				return false; 
			}	
			
		}
		return true;
	}
	
	function goToThisWorkflow(optionNumber)
	{
	    workflow.NextPage = optionNumber;
		goToNextWorkflow();
		//SetWorkflowPage();
	}
	function MoveToNextWorkflow() 
	{
	    workflow.Page = workflow.NextPage;
	    goToNextWorkflow();
	    //if (workflow.tree.length > workflow.NextPage)
	     //   workflow.NextPage = workflow.NextPage + 1;
	}
	function goToNextWorkflow()
	{
	
		var TabNumber	=	isTabToVisit();
		
		//Calling the newly made function, which will find the tab number from xml 
		//var TabNumber	=	isTabToVisitNew();
		if(TabNumber != -1)
		{
			var frameNumber	=	numOfUnderscore(workflow.tree[workflow.NextPage].id);
			var NumOfTabs;
			switch(frameNumber)
			{
				case 1 :
				if(typeof(arrMainTab) != 'undefined')
				{
					NumOfTabs	=	(arrMainTab.length)/4;
					if(NumOfTabs > TabNumber)
					{
						if(prvsTab == '')
						{
						    alert('<%=strSelectAdd %>');
							return;
						}
						else
						{
						    if (isPageVisible())	//Changing the tab ,if page is visible
							{
								if (TabNumber < iTabsPerRow)
								    changeTab(0, TabNumber);
								else
								    changeTab(1, TabNumber - iTabsPerRow);
							}	
						}
					}
					else
					{
					    alert('<%=strSelectAdd %>');
					}
				}
				break;
				
				case 2 :
				case 3 :
				case 4 :
				if(typeof(arrMainTab) != 'undefined')
				{
					if(typeof(document.frames("tabLayer").arrMainTab) == 'undefined')
					{
						if(typeof(arrMainTab) != 'undefined')
						{
							NumOfTabs	=	(arrMainTab.length)/4;
							if(NumOfTabs > TabNumber)
							{
								if(prvsTab == '')
								{
								    alert('<%=strSelectAdd %>');
									return;
								}
								else
								{
								    if (isPageVisible())	//Changing the tab ,if page is visible
									{
									    if (TabNumber < iTabsPerRow)
									        changeTab(0, TabNumber);
									    else
									        changeTab(1, TabNumber - iTabsPerRow);
									}    
								}
							}
							else
							{
							    alert('<%=strSelectAdd %>');
							}
						}
					}
					else
					{
						NumOfTabs	=	(document.frames("tabLayer").arrMainTab.length)/4;
						if(NumOfTabs > TabNumber)
						{
							if(document.frames("tabLayer").prvsTab == '')
							{
							    alert('<%=strSelectAdd %>');
								return;
							}
							else
							{
							    if (isPageVisible())	//Changing the tab ,if page is visible
								{
								    if (TabNumber < iTabsPerRow)
								        document.frames("tabLayer").changeTab(0, TabNumber);
    								else
    								    document.frames("tabLayer").changeTab(1, TabNumber - iTabsPerRow);
								}	
							}
						}
						else
						{
						    alert('<%=strSelectAdd %>');
						}
					}
				}
				break;
			}
		}
		else
		{
			if(workflow.tree.length > workflow.NextPage)
			{
				if(workflow.NextPage < 0)
				{
				    alert('<%=strFirst %>');
				}
				else
				{
				    var menulink = workflow.tree[workflow.NextPage].menu_link;
				    if (String(menulink).indexOf("?", 0) == -1)
				        menulink = menulink + '?TabNumber=' + workflow.NextPage + '&';
				    else
				        menulink = menulink + '&TabNumber=' + workflow.NextPage + '&';
				    top.botframe.location.href = menulink;
				}
			}
			else
			{
			    alert('<%=strLast %>');
			}
		}
	}
	
	function SetWorkflowPage()
	{

		var treeLength	=	workflow.tree.length;
		var i			=	0;
		var setPage = false;
		for(i=0; i<treeLength; i++)
		{
			if(workflow.tree[i].id == workflow.ScreenID)
			{
				workflow.Page	=	i;
				workflow.NextPage = i +1;
				setPage			=	true;
				break;
			}
		}
		if(workflow.Page == 0)
		{
			if(!setPage)
			{
				for(i=0; i<treeLength; i++)
				{
					if(workflow.tree[i].id.indexOf(workflow.ScreenID) != -1)
					{
						workflow.Page = i;
						workflow.NextPage = i;
						setPage	= true;
						break;
					}
				}
			}
			if(!setPage)
			{
				workflow.Page		=	0;
				workflow.NextPage	=	0;
				setPage				=	true;
			}
		}
		
	}
	
	function isTabToVisitNew()
	{
		return workflow.tree[workflow.NextPage].tabNumber;
	}
	
	function isTabToVisit()
	{
		//a;
		var tabToVisit = -1;
		var tempString;
		var subScreenID;
		var subNextPageID;
		if(workflow.NextPage < 0)
			return tabToVisit;
		if(!(workflow.tree.length > workflow.NextPage))
			return tabToVisit;
		if(workflow.tree[workflow.NextPage].id.indexOf('_') != -1)
		{
			subNextPageID	=	workflow.tree[workflow.NextPage].id.substring(0, workflow.tree[workflow.NextPage].id.indexOf('_'));
		}
		else
		{
			subNextPageID	=	workflow.tree[workflow.NextPage].id;
		}
		if(workflow.ScreenID.indexOf('_') != -1)
		{
			subScreenID		=	workflow.ScreenID.substring(0, workflow.ScreenID.indexOf('_'));
		}
		else
		{
			subScreenID		=	workflow.ScreenID;
		}
		if(subNextPageID == subScreenID)
		{
			/*
			if(workflow.tree[workflow.NextPage].id.indexOf(workflow.ScreenID) != -1)
			{
				//tempString	=	workflow.tree[workflow.NextPage].id.substring(workflow.tree[workflow.NextPage].id.indexOf('_')+1,workflow.tree[workflow.NextPage].id.length);
				tempString	=	workflow.tree[workflow.NextPage].id.substring(workflow.ScreenID.length+1,workflow.tree[workflow.NextPage].id.length);
			}
			else
			{
				tempString	=	workflow.tree[workflow.NextPage].id.substring(workflow.tree[workflow.NextPage].id.indexOf('_')+1,workflow.tree[workflow.NextPage].id.length);
			}
			
			if(tempString.indexOf('_') == -1)
			{
				return tempString;
			}
			else
			{
				tempString	=	tempString.substring(0,tempString.indexOf('_'));
				return tempString;
			}
			*/
			return workflow.tree[workflow.NextPage].tabNumber;
		}
		return tabToVisit;
	}
	function showWorkflowOptions()
	{
		var button;
		button	=	document.getElementById('workflowGoTo');
		
		button.blur();
		
		var itemList;
		var w, dw;
		itemList = document.getElementById('workflowOptions').getElementsByTagName("A");
		
		w = itemList[0].offsetWidth;
		itemList[0].style.width = w + "px";
		dw = itemList[0].offsetWidth - w;
		w -= dw;
		itemList[0].style.width = w + "px";
		
		var x, y;
		x = getWorkflowOffsetLeft(button);
		x = x - document.getElementById('workflowOptions').offsetWidth + button.offsetWidth + 5;
		y = document.getElementById('divWorkFlow').offsetHeight + document.getElementById('divWorkFlow').offsetTop + 5;


		document.getElementById('workflowFrame').style.left =   x;
		document.all.item('workflowIFrame').left    =   x;
		document.getElementById('workflowFrame').style.top	=	y;
		document.all.item('workflowIFrame').top		=	y;
		document.getElementById('workflowFrame').style.width=	document.getElementById('workflowOptions').offsetWidth;
		document.all.item('workflowIFrame').width	=	document.getElementById('workflowOptions').offsetWidth;
		document.getElementById('workflowFrame').style.height=	document.getElementById('workflowOptions').offsetHeight;
		document.all.item('workflowIFrame').height	=	document.getElementById('workflowOptions').offsetHeight;

		document.getElementById('workflowOptions').style.left = x + "px";
		document.getElementById('workflowOptions').style.top  = y + "px";
		
		document.getElementById('workflowFrame').style.display = 'inline';
		document.getElementById('workflowIFrame').style.display = 'inline';
		document.getElementById('workflowOptions').style.visibility = "visible";
		hideWorkflowOptions();
		return false;
	}
	function hideWorkflowOptions()
	{
		if(!mouseInWorkflow)
		{
			document.getElementById('workflowFrame').style.display		=	'none';
			document.getElementById('workflowIFrame').style.display		=	'none';
			document.getElementById('workflowOptions').style.visibility	=	"hidden";
		}
		else
		{
			setTimeout('hideWorkflowOptions()',2000);
		}
	}
	function getWorkflowOffsetLeft(el) 
	{
		var x;
		// Return the x coordinate of an element relative to the page.
		x = el.offsetLeft;
		if (el.offsetParent != null)
			x += getWorkflowOffsetLeft(el.offsetParent);

		return x;
	}
	function getWorkflowOffsetTop(el) 
	{
		var y;
		// Return the x coordinate of an element relative to the page.
		y = el.offsetTop;
		if (el.offsetParent != null)
		{
			y += getWorkflowOffsetTop(el.offsetParent);
		}
		return y;
	}
	
	function numOfUnderscore(screenid)
	{
		var numOfUnderScore = 0;
		if(screenid.indexOf('_') != -1)
		{
			for(var loop_count = 0; loop_count < screenid.length; loop_count++)
			{
				if(screenid.charAt(loop_count) == '_')
				{
					numOfUnderScore++;
				}
			}
		}
		return numOfUnderScore;
	}
</script>
<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
<div id="workflowFrame" style="background-color: transparent;"><iframe id="workflowIFrame"  width="0px" height="0px" top="0px;" left="0px"></iframe></div>
<div id="divWorkFlow" style="BORDER-TOP: #ffffff 1px solid;">
</div>
<div id="workflowOptions" class="workflowContainer" onmouseover="mouseInWorkflow=true;" onmouseout="mouseInWorkflow=false;">
</div>
<script language="javascript" type="text/javascript">
	if (!(workflowXML == null || workflowXML == 'undefined'))
	{
		setTimeout('CreateWorkFlow()',1);
	}
</script>
<%}else{%>
<script language="javascript" type="text/javascript">
	var innerWorkflowXML = '<%=workflowXML%>';
	top.botframe.workflow.ScreenID = '<%=ScreenID%>';
	if(top.botframe.workflowXML)
	{
	    top.botframe.workflowXML = innerWorkflowXML;
		setTimeout('top.botframe.CreateWorkFlow()',1);
	}
</script>
<%}%>
<%}%>
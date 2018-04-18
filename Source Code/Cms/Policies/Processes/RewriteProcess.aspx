<%@ Page language="c#" Codebehind="RewriteProcess.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Processes.RewriteProcess" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="PolicyTop" Src="/cms/cmsweb/webcontrols/PolicyTop.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ReinstateProcess</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript">	
		
		//Setting the menu
		//This function will be called after starting the endorsement process
		//using RegisterStartupScript method
		function setMenu()
		{
			
			//IF menu on top frame is not ready then
			//menuXmlReady will false
			//If menu is not ready, we will again call setmenu again after 1 sec
			if (top.topframe.main1.menuXmlReady == false)
				setTimeout('setMenu();',1000);
			
			
			//Enabling or disabling menus
			top.topframe.main1.activeMenuBar = '1';
			top.topframe.createActiveMenu();
			top.topframe.enableMenus('1','ALL');
			top.topframe.enableMenu('1,1,1');			
			top.topframe.enableMenu('1,2,3');
			//top.topframe.disableMenus("1,3","ALL");
			//selectedLOB = lobMenuArr[document.getElementById('hidLOBID').value];
			//top.topframe.enableMenu("1,3," + selectedLOB);			
			
		}
		 

		function AddData()
		{
			document.getElementById('hidROW_ID').value	=	'New';
			document.getElementById('txtEFFECTIVE_DATETIME').focus();
			document.getElementById('txtEFFECTIVE_DATETIME').value  = '';
			document.getElementById('cmbREQUESTED_BY').options(document.getElementById('cmbREQUESTED_BY').selectedIndex).value = 0;
			document.getElementById('cmbPOLICY_TERMS').options(document.getElementById('cmbPOLICY_TERMS').selectedIndex).value = 0;
			document.getElementById('txtNEW_POLICY_TERM_EFFECTIVE_DATE').value  = '';
			document.getElementById('txtNEW_POLICY_TERM_EXPIRATION_DATE').value  = '';
			document.getElementById('hidPOLICY_EFF_DATE').value = '' //New
			DisableValidators();
			ChangeColor();
		}
		
		
		function populateXML()
		{
		
			//if(document.getElementById('hidFormSaved').value == '0')
			{
				var tempXML;
				if(document.getElementById("hidOldData").value != "")
				{
					populateFormData(document.getElementById("hidOldData").value, REWRITE_PROCESS);
					
				}
				else
				{
					AddData();
				}
				
			}
			if(document.getElementById('cmbPOLICY_TERMS').options(document.getElementById('cmbPOLICY_TERMS').selectedIndex).value!="" ||
					document.getElementById('cmbPOLICY_TERMS').options(document.getElementById('cmbPOLICY_TERMS').selectedIndex).value!=null)
			{
					rfvPOLICY_TERMS.style.display='none';
			}
			return false;
		}
		function setExpDate() {
		    if (document.getElementById('txtPOLICY_TERMS').value != "" && document.getElementById('txtNEW_POLICY_TERM_EFFECTIVE_DATE').value != "") {
		        var result = RewriteProcess.GetExpDate(document.getElementById('txtPOLICY_TERMS').value, document.getElementById('txtNEW_POLICY_TERM_EFFECTIVE_DATE').value);

		        if (result != null) {
		            document.getElementById('txtNEW_POLICY_TERM_EXPIRATION_DATE').value = result.value;
		        }
		        else {
		            document.getElementById('txtNEW_POLICY_TERM_EXPIRATION_DATE').value = "";
		        }
		    }
		    else {

		        document.getElementById('txtNEW_POLICY_TERM_EXPIRATION_DATE').value = "";
		    } 
		}
		function ShowExpirationDate()
		{
			var sTerm = "";
			var sposfix = "";
			var sDate;
			var sEffDate = "";
			var sNewMonth = 0;
			var sNewYear = 0;
			var sNY = "";
			var dtDSep;
			sTerm = document.REWRITE_PROCESS.txtPOLICY_TERMS.value;
			//sTerm = document.REWRITE_PROCESS.cmbPOLICY_TERMS.value;
			if(sTerm == "0" || sTerm == "")
			{
				 document.REWRITE_PROCESS.txtNEW_POLICY_TERM_EXPIRATION_DATE.value="";
			}
			else
			{
				if(document.REWRITE_PROCESS.txtNEW_POLICY_TERM_EFFECTIVE_DATE.value!="")
					document.REWRITE_PROCESS.txtNEW_POLICY_TERM_EFFECTIVE_DATE.value = FormatDateForGrid(document.REWRITE_PROCESS.txtNEW_POLICY_TERM_EFFECTIVE_DATE,'');	
				sEffDate = TrimTheString(document.REWRITE_PROCESS.txtNEW_POLICY_TERM_EFFECTIVE_DATE.value);
	   			dtDSep = GetDateSeparator(sEffDate);
	   			if(dtDSep =="")
	   				dtDSep = "/"
	   			else(dtDSep ==" ")
	   			{
	   				dtDSep = "/"
	   				sEffDate = ReplaceString(sEffDate," ", "")
	   			}
	   			
//				if(sEffDate != "")
//				{
//					sEffDate = ReplaceDateSeparator(sEffDate);
//					sDate = new Date(sEffDate);
//					sNewMonth = sDate.getMonth() + parseInt(sTerm);
//					if(sNewMonth >= 12)
//					{
//						sNewYear = sNewMonth / 12;
//						sNewYear = sNewYear + sDate.getYear();
//						sNewMonth = sNewMonth % 12;
//					}
//					else
//					{
//						sNewYear = sDate.getYear();
//					}
//	
//					sDate.setMonth(sNewMonth);
//					sDate.setYear(sNewYear);
//	
//					sNewYear = sDate.getYear();
//					if (sNewYear < 1000)
//					{
//						sNewYear = sNewYear + 1900;
//					} 
//					
//					if ('U' == 'E')		//Date in UK format
//						sposfix = new String(sDate.getDate()-1) + dtDSep + new String(sDate.getMonth() + 1) + dtDSep + new String(sNewYear)
//					else
//						
//					
//					var newDate = new Date(new String(sDate.getMonth() + 1) + "/" + new String(sDate.getDate()) + "/" + new String(sNewYear));
//					
//					newDate.setDate(newDate.getDate());
//					
//					sposfix = new String(newDate.getMonth() + 1) + "/" + new String(newDate.getDate()) + "/" + new String(sNewYear)						
//					
//					document.REWRITE_PROCESS.txtNEW_POLICY_TERM_EXPIRATION_DATE.value = sposfix;
//					
//					if(sTerm=="70")
//					{
//						document.REWRITE_PROCESS.txtNEW_POLICY_TERM_EXPIRATION_DATE.value ="";
//					}
//					
//				 
//				}
	   			setExpDate()
				
			}
		}
		function ConfirmCommit(calledfrom)
		{
		 //alert(comb.options[i].value);
				 // alert(document.getElementById('cmbPOLICY_TERMS').value);
		var flag=confirm("<%=str%>");
		Page_ClientValidate();		
		if (flag==false)
			return false;
		else
		 {
			if (Page_IsValid)
			{
				if (calledfrom=='ANYWAY')
					HideShowCommitAnywayInProgress();
				else
					HideShowCommitInProgress();
			}
			return Page_IsValid ;
		 }
		}
		function DisplayBody()
		{
			if (document.getElementById('hidDisplayBody').value == "True")
			{
				document.getElementById('trBody').style.display='inline';		
			}
			else
			{
				document.getElementById('trBody').style.display='none';
			}
			
		}
		
		function ShowDetailsPolicy()
		{
			//alert('in display');
			/*if (document.getElementById('hidNEW_POLICY_VERSION_ID').value !="" &&  document.getElementById('hidNEW_POLICY_VERSION_ID').value!="0")
				top.botframe.callItemClicked('1,2,3','/cms/policies/aspx/policytab.aspx?CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VERSION_ID=' + document.getElementById('hidNEW_POLICY_VERSION_ID').value + '&CALLEDFROM=REWRITE');
			else
				top.botframe.callItemClicked('1,2,3','/cms/policies/aspx/policytab.aspx?CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VERSION_ID=' + document.getElementById('hidPOLICY_VERSION_ID').value + '&CALLEDFROM=REWRITE');
			*/
			if (document.getElementById('hidNEW_POLICY_VERSION_ID').value!="" && document.getElementById('hidNEW_POLICY_VERSION_ID').value!="0")
				window.open('/Cms/Application/Aspx/DecPage.aspx?CALLEDFOR=DECPAGE&CALLEDFROM=POLICY&CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VER_ID=' + document.getElementById('hidNEW_POLICY_VERSION_ID').value + '&');
			else
				window.open('/Cms/Application/Aspx/DecPage.aspx?CALLEDFOR=DECPAGE&CALLEDFROM=POLICY&CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VER_ID=' + document.getElementById('hidPOLICY_VERSION_ID').value + '&');
			return false;
		}
		
		function formReset()
		{
			document.REWRITE_PROCESS.reset();
			DisableValidators();
			ChangeColor();
			return false;
		}
		//Validates the maximum length for comments
		function txtCOMMENTS_VALIDATE(source, arguments)
		{
				var txtArea = arguments.Value;
				if(txtArea.length > 250 ) 
				{
					arguments.IsValid = false;
					return false;   // invalid userName
				}
		}
		
		function chkSEND_ALL_Change()			
		{
			var chk = document.getElementById('chkSEND_ALL');
			if(chk==null)
				return false;								
			document.getElementById("hidADD_INT_ID").value='';
			if(chk.checked==true)
			{
				ShowHideAddIntCombos(true);
				AssignAddInt(true);
			}
			else
			{
				ShowHideAddIntCombos(false);
				UnAssignAddInt(true);
			}
			
			return false;
		}
		function ShowHideAddIntCombos(flag)
		{
			var chk = document.getElementById('chkSEND_ALL');
			if(chk==null)
				return false;	
				
			if(flag && chk.checked==true)
				document.getElementById('trAddIntList').style.display="none";
			else
				document.getElementById('trAddIntList').style.display="inline";

			combo = document.getElementById('cmbADD_INT');
			if(!(combo==null || combo.selectedIndex==-1))
			{
				if(combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString()%>"
				   || combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString()%>")
				{
					document.getElementById('chkSEND_ALL').style.display="inline";
					document.getElementById('capSEND_ALL').style.display="inline";
					//document.getElementById('trAddIntList').style.display="inline";				
					//chkSEND_ALL_Change();
				}
				else
				{
					document.getElementById('chkSEND_ALL').style.display="none";
					document.getElementById('capSEND_ALL').style.display="none";
					document.getElementById('trAddIntList').style.display="none";
					document.getElementById('hidADD_INT_ID').value = '';
				}		
			}	
		}
		function cmbADD_INT_Change()
		{
			combo = document.getElementById('cmbADD_INT');
			if(combo==null || combo.selectedIndex==-1)
				return false;
			if(combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString()%>"
				|| combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString()%>")
			{
				document.getElementById('chkSEND_ALL').style.display="inline";
				document.getElementById('capSEND_ALL').style.display="inline";
				//document.getElementById('trAddIntList').style.display="inline";				
				chkSEND_ALL_Change();
			}
			else
			{
				document.getElementById('chkSEND_ALL').style.display="none";
				document.getElementById('capSEND_ALL').style.display="none";
				document.getElementById('trAddIntList').style.display="none";
				document.getElementById('hidADD_INT_ID').value = '';
			}			
			return false;
		}
		function GetAssignAddInt()
			{
				document.getElementById("hidADD_INT_ID").value = "";
				var coll = document.getElementById('cmbAssignAddInt');
				var len = coll.options.length;
				for(var k = 0;k < len ; k++)
				{
					var szSelectedDept = coll.options(k).value;
					if (document.getElementById("hidADD_INT_ID").value == "")
					{
						document.getElementById("hidADD_INT_ID").value=  szSelectedDept;
					}
					else
					{
						document.getElementById("hidADD_INT_ID").value = document.getElementById("hidADD_INT_ID").value + "~" + szSelectedDept;
					}
				}
					
			}	
			function SetAssignAddInt()
			{
				if(document.getElementById("hidADD_INT_ID").value=='' || document.getElementById("hidADD_INT_ID").value=='0')
					return;
				var selectedAddInt = new String(document.getElementById("hidADD_INT_ID").value);
				var selectedAddIntArr = selectedAddInt.split('~');
				if(selectedAddIntArr==null || selectedAddIntArr.length<1)
					return;				
				
				var coll = document.getElementById('cmbUnAssignAddInt');
				var selIndex = coll.options.selectedIndex;
				var len = coll.options.length;
				var arrLen = selectedAddIntArr.length;
				if(len<1) return;				
				var num=0;				
				for(var j=0;j<arrLen;j++)
				{
					for (var i = len- 1; i > -1 ; i--)
					{
						if(coll.options(i).value == selectedAddIntArr[j])
						{
							num = i;
							var szSelectedDept = coll.options(i).value;
							var innerText = coll.options(i).text;
							document.getElementById('cmbAssignAddInt').options[document.getElementById('cmbAssignAddInt').length] = new Option(innerText,szSelectedDept)
							coll.remove(i);																	
							len = coll.options.length;
						}										
					}
				}
			}
		function AssignAddInt(flag)
		{					
			var coll = document.getElementById('cmbUnAssignAddInt');
			var selIndex = coll.options.selectedIndex;				
			var len = coll.options.length;			
			if(len<1) return;				
			var num=0;				
			for (var i = len- 1; i > -1 ; i--)
			{
				if(coll.options(i).selected == true || flag)
				{
					num = i;
					var szSelectedDept = coll.options(i).value;
					var innerText = coll.options(i).text;
					document.getElementById('cmbAssignAddInt').options[document.getElementById('cmbAssignAddInt').length] = new Option(innerText,szSelectedDept)
					coll.remove(i);																	
				}										
			}	
			len = coll.options.length;	
			if(	num < len )
			{
				document.getElementById('cmbUnAssignAddInt').options(num).selected = true;
			}	
			else
			{
				if(num>0)
					document.getElementById('cmbUnAssignAddInt').options(num - 1).selected = true;
			}			
			
		}
			function UnAssignAddInt(flag)
			{					
				var UnassignableString = "";								
				var Unassignable = UnassignableString.split(",");
				var gszAssignedString = "";
				var Assigned = gszAssignedString.split(",");				
				var coll = document.getElementById('cmbAssignAddInt');
				var selIndex = coll.options.selectedIndex;
				var len = coll.options.length;		
				var num=0;						
				if(len==0) return;
				
				for (var i = len-1; i > -1 ; i--)
				{
					if(coll.options(i).selected == true || flag)
					{
							num = i;						
							var flag = true;
							var szSelectedDept = coll.options(i).value;
							var innerText = coll.options(i).text;
							for(j = 0; j < Unassignable.length ;j++)
							{
								for(k = 0; k < Assigned.length ;k++)
								{							
										if((szSelectedDept == Unassignable[j]) && (szSelectedDept == Assigned[k])) 
										{
											flag = false;
										}
								}
							}
							
							if(flag == true)
							{
								document.getElementById('cmbUnAssignAddInt').options[document.getElementById('cmbUnAssignAddInt').length] = new Option(innerText,szSelectedDept)					
								coll.remove(i);
															
							}
							/*else
							{
								alert("Cannot unassign Department "+innerText+" because some divisions are associasted with it");
							}*/
					}			
				}
				var len = coll.options.length;
				if(len<1) return;				
				if(	num < len )
				{
					document.getElementById('cmbAssignAddInt').options(num).selected = true;
				}	
				else
				{
					document.getElementById('cmbAssignAddInt').options(num - 1).selected = true;
				}
				
			}	
		function CommentEnable()
		{
			if (document.getElementById('cmbPRINT_COMMENTS').selectedIndex != '-1')		
			{
		
				if (document.getElementById('cmbPRINT_COMMENTS').options[document.getElementById('cmbPRINT_COMMENTS').selectedIndex].value == '0')
				{				
					document.getElementById('txtCOMMENTS').style.display='none';
					document.getElementById('txtCOMMENTS').value = '';
					document.getElementById('rfvCOMMENTS').enabled = false;
					document.getElementById('rfvCOMMENTS').style.display='none';
					document.getElementById('CommentsMandatory').style.display='none';
					document.getElementById('lblCOMM').style.display='inline';						
				}
				else
				{			
					document.getElementById('txtCOMMENTS').style.display='inline';
					document.getElementById('rfvCOMMENTS').style.display='inline';
					document.getElementById('CommentsMandatory').style.display='inline';
					document.getElementById('lblCOMM').style.display='none';				
				}
			}
			else
			{
				document.getElementById('txtCOMMENTS').style.display='none';
				document.getElementById('rfvCOMMENTS').style.display='none';
				document.getElementById('CommentsMandatory').style.display='none';
				document.getElementById('lblCOMM').style.display='inline';		
				document.getElementById('txtCOMMENTS').value = '';
			}
	
		}	
			function Init()
			{

				ApplyColor();
				ChangeColor();
				top.topframe.main1.mousein = false;
				findMouseIn(); 
				SetEffectiveDateToCancelDate();
				//alert('populating');
				populateXML();
				//alert('done');
				DisplayBody();
				AutoIdCardDisplay();
				SetAssignAddInt();
				cmbADD_INT_Change();		
				ShowHideAddIntCombos(true);
				ChangeColor();
				document.getElementById('btnCommitInProgress').style.display="none";
				document.getElementById('btnCommitAnywayInProgress').style.display="none";
				//HidePolicyType();
				//CommentEnable();
				//cmbCANCELLATION_TYPE_Change(true);
				//Added for Itrack Issue 6203 on 31 July 2009
				if (top.topframe.main1.menuXmlReady == false)
					setTimeout("top.topframe.enableMenus('1,7','ALL');",1000);
				else	
					top.topframe.enableMenus('1,7','ALL');
				
			}
			
			//Added SetEffectiveDateToCancelDate() praveen Itrack #3894 
			/*The Rewrite process puts in the effective date of the prior policy <BR>Should 
			default to the Cancellation date from the prior version */
			function SetEffectiveDateToCancelDate()
			{
				//alert("effective date :" + document.getElementById('txtEFFECTIVE_DATETIME').value)
				//alert("calncelled date :" + document.getElementById('hidPOLICY_CANCEL_DATE').value);
				//alert("saved date :" + document.getElementById('hidPOLICY_EFF_DATE').value);
				
				if(document.getElementById('hidPOLICY_EFF_DATE').value!="0")
					document.getElementById('txtEFFECTIVE_DATETIME').value = document.getElementById('hidPOLICY_EFF_DATE').value
				else
					document.getElementById('txtEFFECTIVE_DATETIME').value = document.getElementById('hidPOLICY_CANCEL_DATE').value;
			}
			function SetPolicyTerm_Date()
			{
//			    var comb=document.getElementById('cmbPOLICY_TERMS');
//			  if (document.getElementById('hidPOLICY_TERM').value!="")  
//			  {
//			    for (i=comb.length-1;i>=0;i--)
//			    {
//			  
//			      //alert('value='+comb.options[i].value);
//				  if (comb.options[i].value == document.getElementById('hidPOLICY_TERM').value)
//				  {
//					comb.selectedIndex=i;
//					document.getElementById('hidPOLICY_TERM').value=comb.options[i].value;
//					
//				  break;
//				  }
//				}
//			  }
			  if (document.getElementById('txtEFFECTIVE_DATETIME').value==document.getElementById('hidPOLICY_CANCEL_DATE').value)  
			  {
					document.getElementById('txtNEW_POLICY_TERM_EFFECTIVE_DATE').value= document.getElementById('hidPOLICY_EFFECTIVE_DATE').value;
				if (document.getElementById('hidPOLICY_EXPIRY_DATE').value!="")  
			  		document.getElementById('txtNEW_POLICY_TERM_EXPIRATION_DATE').value= document.getElementById('hidPOLICY_EXPIRY_DATE').value;
			  		//document.getElementById('hidPOLICY_TERM').value=document.getElementById('cmbPOLICY_TERMS').options(document.getElementById('cmbPOLICY_TERMS').selectedIndex).value;	
			  		//document.getElementById('cmbPOLICY_TERMS').setAttribute("disabled",true);
				//comb.setAttribute("disabled",true);			  
				document.getElementById('txtNEW_POLICY_TERM_EFFECTIVE_DATE').setAttribute("readOnly",true); 

			  }
			  else
			  {
			  		document.getElementById('txtNEW_POLICY_TERM_EFFECTIVE_DATE').value= document.getElementById('txtEFFECTIVE_DATETIME').value;
			  		ShowExpirationDate();
				  	//comb.setAttribute("disabled",false);			  
					document.getElementById('txtNEW_POLICY_TERM_EFFECTIVE_DATE').setAttribute("readOnly",false); 
			  		
			  }
			}
			
			function HideShowCommitAnywayInProgress()
			{
				GetAssignAddInt();
				Page_ClientValidate();	
				if(!Page_IsValid)
				{
				return false;
				}
				else
				{
				document.getElementById('btnCommitAnywayInProgress').style.display="inline";
				document.getElementById('btnCommitAnywayInProgress').disabled = true;
				document.getElementById('btnComitAynway').style.display="none";	
				document.getElementById('btnSave').disabled = true;
				document.getElementById('btnComplete').disabled = true;
				if(document.getElementById('btnRollBack'))
					document.getElementById('btnRollBack').disabled = true;	
     			 DisableButton(document.getElementById('btnComitAynway'));
     			  top.topframe.disableMenus('1,7',"ALL");//Added for Itrack Issue 6203 on 20 Aug 2009
				}
			}
			function HideShowCommitInProgress()
			{
				GetAssignAddInt();
				Page_ClientValidate();	
				if(!Page_IsValid)
				{
				return false;
				}
				else
				{
				document.getElementById('btnCommitInProgress').style.display="inline";
				document.getElementById('btnCommitInProgress').disabled = true;
				document.getElementById('btnComplete').style.display="none";
				document.getElementById('btnSave').disabled = true;	
				if (document.getElementById('btnComitAynway'))
					document.getElementById('btnComitAynway').disabled = true;
				if(document.getElementById('btnRollBack'))
					document.getElementById('btnRollBack').disabled = true;	
     			 DisableButton(document.getElementById('btnComplete'));
     			 top.topframe.disableMenus("1,7","ALL");//Added for Itrack Issue 6203 on 31 July 2009
				return true;
				}
			}
			function HideShowCommit()
			{
				document.getElementById('btnComplete').disabled = true;
				document.getElementById('btnSave').disabled = true;
				if (document.getElementById('btnComitAynway'))	
					document.getElementById('btnComitAynway').disabled=true;
     			 DisableButton(document.getElementById('btnRollBack'));
     			 top.topframe.disableMenus('1,7',"ALL");//Added for Itrack Issue 6203 on 31 July 2009
			}
	function HidePolicyType()
	{
	    //alert('lob='+document.getElementById('hidLOBID').value);
		if (document.getElementById('hidLOBID').value=="6" || document.getElementById('hidLOBID').value=="1") 
		{
		document.getElementById('policyTR').style.display='inline';
		document.getElementById('rfvPOLICY_TYPE').setAttribute("enabled",true); 
		}
		else
		{
		document.getElementById('policyTR').style.display='none';
		document.getElementById('rfvPOLICY_TYPE').setAttribute("enabled",false);
		document.getElementById('rfvPOLICY_TYPE').style.display ='none' 
		
		}
	}
		function AutoIdCardDisplay()
		{
			if(document.getElementById('hidLOBID').value=='<%=((int)Cms.CmsWeb.cmsbase.enumLOB.AUTOP).ToString()%>' || document.getElementById('hidLOBID').value=='<%=((int)Cms.CmsWeb.cmsbase.enumLOB.CYCL).ToString()%>')
			{
				document.getElementById('trAutoIdHeader').style.display="inline";
				document.getElementById('trAutoIdControls').style.display="inline";
			}
			else
			{
				document.getElementById('trAutoIdHeader').style.display="none";
				document.getElementById('trAutoIdControls').style.display="none";
			}
				
		}	
		
		function txtEFFECTIVE_HOUR_VALIDATE(source, arguments)//Function added by Charles on 31-Aug-09 for Itrack 6323
		{	
			var EFFECTIVE_HOUR= document.getElementById('txtEFFECTIVE_HOUR').value.replace(' ','');			
			if((EFFECTIVE_HOUR == '00' || EFFECTIVE_HOUR == '0') && document.getElementById('cmbMERIDIEM').options[1].selected == '1')
				{
					arguments.IsValid = false;
					return;					
				}
		}
		function disableHourValidator() //Function added by Charles on 1-Sep-09 for Itrack 6323
		{
			var EFFECTIVE_HOUR= document.getElementById('txtEFFECTIVE_HOUR').value.replace(' ','');
			if(document.getElementById('cmbMERIDIEM').options[1].selected == '1' && (EFFECTIVE_HOUR == '00' || EFFECTIVE_HOUR == '0') )
			{	
				document.getElementById('csvEFFECTIVE_HOUR').setAttribute('isValid',false);
				document.getElementById('csvEFFECTIVE_HOUR').style.display='inline';				
			}
			else
			{
				document.getElementById('csvEFFECTIVE_HOUR').setAttribute('isValid',true);
				document.getElementById('csvEFFECTIVE_HOUR').style.display='none';
			}
			
		}
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" scroll="yes" onload="Init();">
		<form id="REWRITE_PROCESS" method="post" runat="server">
			<DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>
			<!-- To add bottom menu -->
			<!-- To add bottom menu ends here -->
			<TABLE cellSpacing="0" cellPadding="0" width="90%" align="center" border="0">
				<tr>
					<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
				</tr>
				<tr>
					<TD><webcontrol:policytop id="cltPolicyTop" runat="server"></webcontrol:policytop></TD>
				</tr>
				<tr>
					<td><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></td>
				</tr>
				<tr>
					<td class="headereffectcenter"><asp:Label ID="capHeader" runat="server"></asp:Label></td><%--Rewrite Process--%>
				</tr>
			</TABLE>
			<TABLE cellSpacing="1" cellPadding="0" width="90%" align="center" border="0">
				<tr>
					<td><webcontrol:gridspacer id="Gridspacer2" runat="server"></webcontrol:gridspacer></td>
				</tr>
				<tr>
					<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessage" runat="server"></asp:Label></TD><%--Please note that all fields marked with * are mandatory--%>
				</tr>
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<tr id="trBody">
					<td>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="midcolora"><asp:label id="capEFFECTIVE_DATETIME" runat="server">Effective Date of Reinstate</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" vAlign="middle"><asp:textbox id="txtEFFECTIVE_DATETIME" onblur="SetPolicyTerm_Date();" runat="server" size="12"
											maxlength="10" Display="Dynamic"></asp:textbox><asp:hyperlink id="hlkReinstateEffectiveDate" runat="server" CssClass="HotSpot">
											<asp:image id="ReinstateCalenderPicker" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
												valign="middle"></asp:image>
										</asp:hyperlink><BR>
										<asp:requiredfieldvalidator id="rfvEFFECTIVE_DATETIME" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_DATETIME"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revEFFECTIVE_DATETIME" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_DATETIME"></asp:regularexpressionvalidator>
										<asp:rangevalidator id="rngEFFECTIVE_DATE" Display="Dynamic" ControlToValidate="txtEFFECTIVE_DATETIME"
											Type="Date" Runat="server"></asp:rangevalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capEFFECTIVE_TIME" runat="server"></asp:label><span class="mandatory">*</span>
									</TD>
									<td class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_HOUR" runat="server" size="3" maxlength="2">12</asp:textbox><asp:label id="lblEFFECTIVE_HOUR" runat="server">HH</asp:label><asp:textbox id="txtEFFECTIVE_MINUTE" runat="server" size="3" maxlength="2">01</asp:textbox><asp:label id="Label1" runat="server">MM</asp:label><asp:dropdownlist id="cmbMERIDIEM" runat="server" ></asp:dropdownlist><!--Removed onfocus="SelectComboIndex('cmbMERIDIEM')" by Charles on 31-Aug-09 for Itrack 6323 --><BR>
										<asp:requiredfieldvalidator id="rfvEFFECTIVE_HOUR" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_HOUR"
											ErrorMessage="Effective Time can't be blank.<br>"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="rfvEFFECTIVE_MINUTE" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_MINUTE"
											ErrorMessage="Effective Time can't be blank.<br>"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="rfvMERIDIEM" runat="server" Display="Dynamic" ControlToValidate="cmbMERIDIEM"
											ErrorMessage="cmbMERIDIEM can't be blank."></asp:requiredfieldvalidator>
										<!-- Range Validator --><asp:rangevalidator id="rnvEFFECTIVE_HOUR" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_HOUR"
											Type="Currency" MaximumValue="12" MinimumValue="0" Text=""></asp:rangevalidator><!-- Added Text for rangevalidator by Charles on 31-Aug-09 for Itrack 6323 --><asp:rangevalidator id="rnvtEFFECTIVE_MINUTE" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_MINUTE"
											Type="Integer" MaximumValue="59" MinimumValue="0" Text=""></asp:rangevalidator><!-- customvalidator added by Charles on 31-Aug-09 for Itrack 6323 --><asp:customvalidator id="csvEFFECTIVE_HOUR" Display="Dynamic" ControlToValidate="txtEFFECTIVE_HOUR" Runat="server"
											ClientValidationFunction="txtEFFECTIVE_HOUR_VALIDATE" ErrorMessage="HH 00 or 0 must be in AM"></asp:customvalidator></td>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4"><asp:Label ID="capTerm" runat="server"></asp:Label></TD><%--Term--%>
								</tr>
								<tr>
									<TD class="midcolora"><asp:label id="capPOLICY_TERMS" runat="server">Policy Term Months</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora">
									<asp:TextBox runat="server" ID="txtPOLICY_TERMS" size="5" MaxLength="3" onchange="ShowExpirationDate()"></asp:TextBox><%=PoliyTermIn%>
									<asp:dropdownlist id="cmbPOLICY_TERMS" onfocus="SelectComboIndex('cmbPOLICY_TERMS')" runat="server"
											onchange="ShowExpirationDate();" style="display:none"></asp:dropdownlist>
											<BR>
										<asp:requiredfieldvalidator id="rfvPOLICY_TERMS" runat="server" Display="Dynamic" ControlToValidate="txtPOLICY_TERMS"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora"><asp:label id="capNEW_POLICY_TERM_EFFECTIVE_DATE" runat="server">Effective Date</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" vAlign="middle"><asp:textbox id="txtNEW_POLICY_TERM_EFFECTIVE_DATE" runat="server" size="12" maxlength="10" Display="Dynamic"></asp:textbox><asp:hyperlink id="hlkTermEffectiveDate" runat="server" CssClass="HotSpot">
											<asp:image id="TermCalenderPicker" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
												valign="middle"></asp:image>
										</asp:hyperlink><BR>
										<asp:requiredfieldvalidator id="rfvNEW_POLICY_TERM_EFFECTIVE_DATE" runat="server" Display="Dynamic" ControlToValidate="txtNEW_POLICY_TERM_EFFECTIVE_DATE"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revNEW_POLICY_TERM_EFFECTIVE_DATE" runat="server" Display="Dynamic" ControlToValidate="txtNEW_POLICY_TERM_EFFECTIVE_DATE"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capNEW_POLICY_TERM_EXPIRATION_DATE" runat="server">Expiration Date</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtNEW_POLICY_TERM_EXPIRATION_DATE" runat="server" size="12" maxlength="10"
											Display="Dynamic" ReadOnly="True"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvNEW_POLICY_TERM_EXPIRATION_DATE" runat="server" Display="Dynamic" ControlToValidate="txtNEW_POLICY_TERM_EXPIRATION_DATE"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" colspan="2"></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capREQUESTED_BY" runat="server">Requested By</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREQUESTED_BY" onfocus="SelectComboIndex('cmbREQUESTED_BY')" runat="server"></asp:dropdownlist></TD>
									<td class="midcolora" colspan="2">
									</td>
									<%--<TD class="midcolora" width="18%"><asp:label id="capAPPLY_INFLATION" runat="server">Apply Inflation</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:checkbox id="chkAPPLY_INFLATION" Runat="server"></asp:checkbox></TD> --%>
								</tr>
								<%--
								<tr>
									<td class="headerEffectSystemParams" colspan="4">Agency Rewrite Instructions
									</td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capANOTHER_AGENCY" runat="server">Another Agency</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbANOTHER_AGENCY" onfocus="SelectComboIndex('cmbANOTHER_AGENCY')" runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capSAME_AGENCY" runat="server">Same Agency</asp:label></TD>
									<TD class="midcolora"><asp:dropdownlist id="cmbSAME_AGENCY" onfocus="SelectComboIndex('cmbSAME_AGENCY')" runat="server"></asp:dropdownlist></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capNEW_AGENCY" runat="server">Agency</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbNEW_AGENCY" onfocus="SelectComboIndex('cmbNEW_AGENCY')" runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" colspan="2"></TD>
								</tr>
								<tr id="policyTR" runat="server">
									<td class="midcolora" colSpan="1"><asp:label id="capPOLICY_TYPE" runat="server">Policy Type</asp:label><span class="mandatory">*</span>
									</td>
									<td class="midcolora" colSpan="3"><asp:dropdownlist id="cmbPOLICY_TYPE" onfocus="SelectComboIndex('cmbPOLICY_TYPE');" runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvPOLICY_TYPE" Display="Dynamic" ControlToValidate="cmbPOLICY_TYPE" Runat="server"></asp:requiredfieldvalidator></td>
								</tr> --%>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4"><asp:Label ID="capPrintingOption" runat="server"></asp:Label></TD><%--Printing Options Details--%>
								</tr>
								<%--<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPRINT_COMMENTS" runat="server">Comments</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPRINT_COMMENTS" onfocus="SelectComboIndex('cmbPRINT_COMMENTS')" runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capCOMMENTS" runat="server">Comments</asp:label><span class="mandatory" id="CommentsMandatory">*</span></TD>
									<TD class="midcolora"><asp:textbox id="txtCOMMENTS" runat="server" maxlength="0" TextMode="MultiLine" Columns="50"
											Rows="10"></asp:textbox><asp:label id="lblCOMM" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvCOMMENTS" runat="server" Display="Dynamic" ControlToValidate="txtCOMMENTS"></asp:requiredfieldvalidator><asp:customvalidator id="csvCOMMENTS" Display="Dynamic" ControlToValidate="txtCOMMENTS" Runat="server"
											ClientValidationFunction="txtCOMMENTS_VALIDATE"></asp:customvalidator></TD>
								</tr> --%>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPRINTING_OPTIONS" runat="server">No printing Required at all</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:checkbox id="chkPRINTING_OPTIONS" Runat="server"></asp:checkbox></TD>
									<td class="midcolora" colSpan="2"></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capINSURED" runat="server">Insured</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbINSURED" Runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capAGENCY_PRINT" runat="server">Agency</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbAGENCY_PRINT" Runat="server"></asp:dropdownlist></TD>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4"><asp:Label ID="capAdditionalInt" runat="server"></asp:Label></TD><%--Additional Interest Details--%>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capADD_INT" runat="server">Additional Interest</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbADD_INT" Runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capSEND_ALL" runat="server">Send to All</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:checkbox id="chkSEND_ALL" Runat="server"></asp:checkbox></TD>
								</tr>
								<tr id="trAddIntList">
									<td class="midcolora" colSpan="4">
										<table>
											<tr>
												<td class="midcolorc" align="center" width="37%"><asp:label id="capUnassignLossCodes" runat="server">All Additional Interests</asp:label></td>
												<td class="midcolorc" vAlign="middle" align="center" width="33%" rowSpan="7"><br>
													<br>
													<input class="clsButton" id="btnAssignLossCodes" onclick="javascript:AssignAddInt(false);"
														type="button" value=">>" name="btnAssignLossCodes" runat="server"><br>
													<br>
													<input class="clsButton" id="btnUnAssignLossCodes" onclick="javascript:UnAssignAddInt(false);"
														type="button" value="<<" name="btnUnAssignLossCodes" runat="server">
												</td>
												<td class="midcolorc" align="center" width="33%"><asp:label id="capAssignedLossCodes" runat="server">Selected Additional Interests</asp:label></td>
											</tr>
											<tr>
												<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="cmbUnAssignAddInt" Runat="server" SelectionMode="Multiple" Height="150px" Width="300px"></asp:listbox></td>
												<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="cmbAssignAddInt" Runat="server" SelectionMode="Multiple" Height="150px" Width="300px"></asp:listbox></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr id="trAutoIdHeader">
									<TD class="headerEffectSystemParams" colSpan="4">Auto ID Card Details</TD>
								</tr>
								<tr id="trAutoIdControls">
									<TD class="midcolora" width="18%"><asp:label id="capAUTO_ID_CARD" runat="server">Auto ID Card</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:DropDownList ID="cmbAUTO_ID_CARD" Runat="server"></asp:DropDownList></TD>
									<TD class="midcolora" width="18%"><asp:label id="capNO_COPIES" runat="server">No. of Copies</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:TextBox ID="txtNO_COPIES" Runat="server" MaxLength="2" size="6"></asp:TextBox><br>
									<asp:rangevalidator id="rngNO_COPIES" Display="Dynamic" ControlToValidate="txtNO_COPIES" Runat="server"
										Type="Integer" MinimumValue="0" MaximumValue="99"></asp:rangevalidator>
									</TD>
								</tr>
								<%--
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4">Letter</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capSTD_LETTER_REQD" runat="server">Standard Letter Required</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSTD_LETTER_REQD" Runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capCUSTOM_LETTER_REQD" runat="server">Customized Letter Required</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCUSTOM_LETTER_REQD" Runat="server"></asp:dropdownlist></TD>
								</tr> --%>
								<!-- end here -->
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" causesValidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnRollBack" runat="server" Text="Rollback" CausesValidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnPolicyDetails" Visible="false" runat="server" Text="View Dec Page"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnPrintPreview" style="DISPLAY: none" runat="server" Text="Preview"></cmsb:cmsbutton><br>
										<cmsb:cmsbutton class="clsButton" id="btnBackToSearch" runat="server" Text="Back To Search" causesValidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnBackToCustomerAssistant" runat="server" Text="Back To Customer Assistant"
											CausesValidation="false"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnGeneratePremiumNotice" style="DISPLAY: none" runat="server"
											Text="Premium Notice" causesValidation="false"></cmsb:cmsbutton>
											<cmsb:cmsbutton class="clsButton" id="btnComplete" runat="server" Text="Commit"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnCommitInProgress" runat="server" Text="Commit in Progress"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnGet_Premium" style="DISPLAY:inline " runat="server" Text="Get Premium"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton><br>
										<cmsb:cmsbutton class="clsButton" id="btnGenerateReinstateDecPage" style="DISPLAY: none" runat="server"
											Text="Reinstatement Dec Page" causesValidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnCommitToSpool" style="DISPLAY: none" runat="server" Text="Commit To Spool"></cmsb:cmsbutton></td>
								</tr>
				</tr>
			</TABLE>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<tr>
					<TD class="headereffectcenter" colSpan="4"><span id="spnURStatus"  runat="server"></asp:Label></span></TD><%--Underwriting Rules Status--%>
				</tr>
				<tr>
					<td>
						<div class="midcolora" id="myDIV" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 189px"
							align="center" runat="server"></div>
					</td>
				</tr>
				<tr>
					<td class="midcolorr" align="left" colSpan="4">
					<cmsb:cmsbutton class="clsButton" id="btnComitAynway" runat="server" Text="Commit Anyway"></cmsb:cmsbutton>
					<cmsb:cmsbutton class="clsButton" id="btnCommitAnywayInProgress" runat="server" Text="Commit in Progress"></cmsb:cmsbutton>
					</td>
				</tr>
			</TABLE>
			</TD></TR></TBODY></TABLE><INPUT id="hidLOBID" type="hidden" name="hidLOBID" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" name="hidCUSTOMER_ID" runat="server"> <INPUT id="hidPOLICY_ID" type="hidden" name="hidPOLICY_ID" runat="server">
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidROW_ID" type="hidden" value="0" name="hidROW_ID" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidDisplayBody" type="hidden" value="True" name="hidDisplayBody" runat="server">
			<input id="hidPROCESS_ID" type="hidden" name="hidPROCESS_ID" runat="server"> <input id="hidPOLICY_VERSION_ID" type="hidden" name="hidPOLICY_VERSION_ID" runat="server">
			<input id="hidNEW_POLICY_VERSION_ID" type="hidden" name="hidNEW_POLICY_VERSION_ID" runat="server">
			<INPUT id="hidUNDERWRITER" type="hidden" value="0" name="hidUNDERWRITER" runat="server">
			<INPUT id="hidSTATE_ID" type="hidden" value="0" name="hidSTATE_ID" runat="server">
			<INPUT id="hidADD_INT_ID" type="hidden" value="0" name="hidADD_INT_ID" runat="server">
			<INPUT id="hidPOLICY_EFFECTIVE_DATE" type="hidden" value="0" name="hidPOLICY_EFFECTIVE_DATE"
				runat="server"> <INPUT id="hidPOLICY_EXPIRY_DATE" type="hidden" value="0" name="hidPOLICY_EXPIRY_DATE"
				runat="server"> <INPUT id="hidPOLICY_TERM" type="hidden" name="hidPOLICY_TERM" runat="server">
			<INPUT id="hidPOLICY_CANCEL_DATE" type="hidden" value="0" name="hidPOLICY_CANCEL_DATE"
				runat="server"> <INPUT id="hidPOLICY_TYPE" type="hidden" value="0" name="hidPOLICY_TYPE" runat="server">
			<INPUT id="hidOLD_AGENCY" type="hidden" value="0" name="hidOLD_AGENCY" runat="server">
			<INPUT id="hidPOLICY_EFF_DATE" type="hidden" value="0" name="hidPOLICY_EFF_DATE" runat="server"> 
		</form>
		<script>
		cmbADD_INT_Change();
		</script>
	</body>
</HTML>

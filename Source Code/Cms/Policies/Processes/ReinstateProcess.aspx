<%@ Page language="c#" Codebehind="ReinstateProcess.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Processes.ReinstateProcess"  ValidateRequest="false"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="PolicyTop" Src="/cms/cmsweb/webcontrols/PolicyTop.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
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
					populateFormData(document.getElementById("hidOldData").value, REINSTATE_PROCESS);
				}
				else
				{
					AddData();
				}
			}
			return false;
		} 
				
	function Check()
	{
		if (document.getElementById('cmbREASON').selectedIndex != '-1')		
		{

				
				if (document.getElementById('cmbREASON').options[document.getElementById('cmbREASON').selectedIndex].value == '11560')
				{
				
					document.getElementById('txtOTHER_REASON').style.display='inline';
					//document.getElementById('lblOTHER_REASON').style.display='none';
					document.getElementById("rfvOTHER_REASON").setAttribute('enabled',true);
					document.getElementById("rfvOTHER_REASON").setAttribute('isValid',true);
					document.getElementById('spnOTHER_REASON').style.display='inline';				
				}
				else
				{
				
					//document.getElementById('txtOTHER_REASON').style.display='none';
					//document.getElementById('lblOTHER_REASON').style.display='inline';
					document.getElementById("rfvOTHER_REASON").setAttribute('enabled',false);
					document.getElementById("rfvOTHER_REASON").setAttribute('isValid',false);
					document.getElementById("rfvOTHER_REASON").style.display = "none";
					document.getElementById('spnOTHER_REASON').style.display='none';
				
				}
				ApplyColor();
				ChangeColor();
		}
		else
		{
				//document.getElementById('txtOTHER_REASON').style.display='none';
				//document.getElementById('lblOTHER_REASON').style.display='inline';
				document.getElementById("rfvOTHER_REASON").setAttribute('enabled',false);
				document.getElementById("rfvOTHER_REASON").setAttribute('isValid',false);
				document.getElementById("rfvOTHER_REASON").style.display = "none";
				document.getElementById('spnOTHER_REASON').style.display='none';
				ApplyColor();
				ChangeColor();
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

			sTerm = document.REINSTATE_PROCESS.cmbPOLICY_TERMS.value;
			document.getElementById('rfvPOLICY_TERMS').setAttribute("enabled",true); 
			if(sTerm == "0" || sTerm == "")
			{
				 document.REINSTATE_PROCESS.txtNEW_POLICY_TERM_EXPIRATION_DATE.value="";
			}
			else
			{
				if(document.REINSTATE_PROCESS.txtNEW_POLICY_TERM_EFFECTIVE_DATE.value!="")
					document.REINSTATE_PROCESS.txtNEW_POLICY_TERM_EFFECTIVE_DATE.value = FormatDateForGrid(document.REINSTATE_PROCESS.txtNEW_POLICY_TERM_EFFECTIVE_DATE,'');	
				sEffDate = TrimTheString(document.REINSTATE_PROCESS.txtNEW_POLICY_TERM_EFFECTIVE_DATE.value);
	   			dtDSep = GetDateSeparator(sEffDate);
	   			if(dtDSep =="")
	   				dtDSep = "/"
	   			else(dtDSep ==" ")
	   			{
	   				dtDSep = "/"
	   				sEffDate = ReplaceString(sEffDate," ", "")
	   			}
	   			
				if(sEffDate != "")
				{
					sEffDate = ReplaceDateSeparator(sEffDate);
					sDate = new Date(sEffDate);
					sNewMonth = sDate.getMonth() + parseInt(sTerm);
			
					if(sNewMonth >= 12)
					{
						sNewYear = sNewMonth / 12;
						sNewYear = sNewYear + sDate.getYear();
						sNewMonth = sNewMonth % 12;
					}
					else
					{
						sNewYear = sDate.getYear();
					}
	
					sDate.setMonth(sNewMonth);
					sDate.setYear(sNewYear);
	
					sNewYear = sDate.getYear();
					if (sNewYear < 1000)
					{
						sNewYear = sNewYear + 1900;
					} 
					
					if ('U' == 'E')		//Date in UK format
						sposfix = new String(sDate.getDate()-1) + dtDSep + new String(sDate.getMonth() + 1) + dtDSep + new String(sNewYear)
					else
						
					
					var newDate = new Date(new String(sDate.getMonth() + 1) + "/" + new String(sDate.getDate()) + "/" + new String(sNewYear));
					
					newDate.setDate(newDate.getDate());
					var mm=new String(newDate.getMonth() + 1);
					var dd= new String(newDate.getDate()) ;
					var yy=new String(sNewYear)	;
					if(dd<10){dd='0'+dd}
					if(mm<10){mm='0'+mm}
					//sposfix = new String(newDate.getMonth() + 1) + "/" + new String(newDate.getDate()) + "/" + new String(sNewYear)						
					sposfix = mm + "/" + dd + "/" + yy
					document.REINSTATE_PROCESS.txtNEW_POLICY_TERM_EXPIRATION_DATE.value = sposfix;
					
					if(sTerm=="70")
					{
						document.REINSTATE_PROCESS.txtNEW_POLICY_TERM_EXPIRATION_DATE.value ="";
					}
					
				 
				}
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
			/*if (document.getElementById('hidNEW_POLICY_VERSION_ID').value !="" &&  document.getElementById('hidNEW_POLICY_VERSION_ID').value!="0")
				top.botframe.callItemClicked('1,2,3','/cms/policies/aspx/policytab.aspx?CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VERSION_ID=' + document.getElementById('hidNEW_POLICY_VERSION_ID').value + '&');
			else
				top.botframe.callItemClicked('1,2,3','/cms/policies/aspx/policytab.aspx?CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VERSION_ID=' + document.getElementById('hidPOLICY_VERSION_ID').value + '&');
			*/	
			if (document.getElementById('hidNEW_POLICY_VERSION_ID').value!="" && document.getElementById('hidNEW_POLICY_VERSION_ID').value!="0")
				window.open('/Cms/Application/Aspx/DecPage.aspx?CALLEDFOR=DECPAGE&CALLEDFROM=POLICY&CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VER_ID=' + document.getElementById('hidNEW_POLICY_VERSION_ID').value + '&');
			else
				window.open('/Cms/Application/Aspx/DecPage.aspx?CALLEDFOR=DECPAGE&CALLEDFROM=POLICY&CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VER_ID=' + document.getElementById('hidPOLICY_VERSION_ID').value + '&');
			return false;
		}
		
		function formReset()
		{
			document.REINSTATE_PROCESS.reset();
			//Added for Itrack Issue 6546 on 12 Oct 09
			cmbADD_INT_Change();
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
		}
		function cmbADD_INT_Change()
		{
			combo = document.getElementById('cmbADD_INT');
			if(combo==null || combo.selectedIndex==-1)
				return false;
			//MICHIGAN_MAILERS Itrack #4068
			if(combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString()%>"
			 || combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.MICHIGAN_MAILERS).ToString()%>")
			{
				document.getElementById('chkSEND_ALL').style.display="inline";
				document.getElementById('capSEND_ALL').style.display="inline";
				//document.getElementById('trAddIntList').style.display="inline";	
				if (document.getElementById('chkSEND_ALL').checked == false)
				   document.getElementById('trAddIntList').style.display="inline";	
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
     			DisableButton(document.getElementById('btnRollBack'));
     			top.topframe.disableMenus('1,7','ALL');//Added for Itrack Issue 6203 on 31 July 2009
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
				populateXML();
				AutoIdCardDisplay();
				DisplayBody();
				SetAssignAddInt();
				cmbADD_INT_Change();
				Check();
				SetPolicyEffective_Date();
				ShowHideAddIntCombos(true);
				ApplyColor();
				ChangeColor();
				//CommentEnable();
				//cmbCANCELLATION_TYPE_Change(true);
				doFocus();
				document.getElementById('btnCommitInProgress').style.display="none";
				//SetReinstatementFee();//Done for Itrack Issue 6262 on 1 Oct 09
				//Added for Itrack Issue 6203 on 31 July 2009
				
				if (top.topframe.main1.menuXmlReady == false)
					setTimeout("top.topframe.enableMenus('1,7','ALL');",1000);
				else	
					top.topframe.enableMenus('1,7','ALL');
			}
			//by pravesh to set Cursor in Date Field
			function doFocus()
			{
			//document.forms[0].txtEFFECTIVE_DATETIME.focus();
			//document.all('txtEFFECTIVE_DATETIME').focus();return false;
			if (document.getElementById('trBody').style.display=='inline')
				document.getElementById('txtEFFECTIVE_DATETIME').focus();
			}
		function AutoIdCardDisplay()
			{
				if(document.getElementById('hidLOBID').value=='<%=((int)Cms.CmsWeb.cmsbase.enumLOB.AUTOP).ToString()%>' || document.getElementById('hidLOBID').value=='<%=((int)Cms.CmsWeb.cmsbase.enumLOB.AUTOP).ToString()%>')
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
			function SetPolicyTerm_Date()
			{
			 
			    //var comb=document.getElementById('cmbPOLICY_TERMS');
//				if (document.getElementById('hidPOLICY_TERM').value!="")  
//				{
//					for (i=comb.length-1;i>=0;i--)
//					{
//					//alert('value='+comb.options[i].value);
//					if (comb.options[i].value == document.getElementById('hidPOLICY_TERM').value)
//					{
//					comb.selectedIndex=i;
//					document.getElementById('rfvPOLICY_TERMS').setAttribute("enabled",false); 
//					document.getElementById('rfvPOLICY_TERMS').style.display ="none"; 
//					break;
//					}
//					}
//				}
			 // document.getElementById('hidPOLICY_TERMS').value=comb.options[comb.selectedIndex].value;
			  SetPolicyEffective_Date();
			  //SetReinstatementFee();
			  }
			  //Added on 3 April 2008 : Kasana
			  function SetReinstatementFee()
			  {
			 	var combFee=document.getElementById('cmbAPPLY_REINSTATE_FEE');
				var mydate=document.getElementById('txtEFFECTIVE_DATETIME').value;
				
				var newDate = new Date(mydate);
				newDate.setDate(newDate.getDate());
					var mm=new String(newDate.getMonth() + 1);
					var dd= new String(newDate.getDate());
					var yy=new String(newDate.getYear());
					if(dd<10){dd='0'+dd}
					if(mm<10){mm='0'+mm}
					sposfix = mm + "/" + dd + "/" + yy
				
				 if (sposfix==document.getElementById('hidPOLICY_CANCEL_DATE').value)  
				  {
					 //Done for Itrack Issue 6262 on 24 Sept 09
					 //combFee.setAttribute("disabled",true);
					 combFee.setAttribute("disabled",false);
					 combFee.selectedIndex=0;
				  }
				  else
				  {
					 combFee.setAttribute("disabled",false);	
					 combFee.selectedIndex=1;	
				  }	
			  }
			  
			  function SetPolicyEffective_Date()
			  {
			  var mydate=document.getElementById('txtEFFECTIVE_DATETIME').value;
			  var txtPolicyTerm=document.getElementById('txtPOLICY_TERMS');
			  FormatDateForGrid(document.getElementById('txtEFFECTIVE_DATETIME'),'');
			    mydate=document.getElementById('txtEFFECTIVE_DATETIME').value; 
			    //alert(document.getElementById('txtEFFECTIVE_DATETIME').value); 
			  if (mydate==document.getElementById('hidPOLICY_CANCEL_DATE').value)  
			  {
					document.getElementById('txtNEW_POLICY_TERM_EFFECTIVE_DATE').value= document.getElementById('hidPOLICY_EFFECTIVE_DATE').value;
				if (document.getElementById('hidPOLICY_EXPIRY_DATE').value!="")  
			  		document.getElementById('txtNEW_POLICY_TERM_EXPIRATION_DATE').value= document.getElementById('hidPOLICY_EXPIRY_DATE').value;
				//comb.setAttribute("disabled",true);			  
				document.getElementById('txtNEW_POLICY_TERM_EFFECTIVE_DATE').setAttribute("readOnly",true); 
				//document.getElementById('cmbAPPLY_REINSTATE_FEE').selectedIndex=0;
			  }
			  else
			  {
			  		// changed by pravesh on 9 july as per itrack issue 2128 not to change term,effective date and expiry date of policy
			  		/* commented
			  		document.getElementById('txtNEW_POLICY_TERM_EFFECTIVE_DATE').value= document.getElementById('txtEFFECTIVE_DATETIME').value;
			  		ShowExpirationDate();
				  	comb.setAttribute("disabled",false);			  
					document.getElementById('txtNEW_POLICY_TERM_EFFECTIVE_DATE').setAttribute("readOnly",false); 
					document.getElementById('cmbAPPLY_REINSTATE_FEE').selectedIndex=1;
					*/
					//added
			  		document.getElementById('txtNEW_POLICY_TERM_EFFECTIVE_DATE').value= document.getElementById('hidPOLICY_EFFECTIVE_DATE').value;
					if (document.getElementById('hidPOLICY_EXPIRY_DATE').value!="")  
			  		document.getElementById('txtNEW_POLICY_TERM_EXPIRATION_DATE').value= document.getElementById('hidPOLICY_EXPIRY_DATE').value;
			//txtPolicyTerm.setAttribute("disabled", true);			  
					document.getElementById('txtNEW_POLICY_TERM_EFFECTIVE_DATE').setAttribute("readOnly",true); 
					//document.getElementById('cmbAPPLY_REINSTATE_FEE').selectedIndex=0;
			  }
			  	document.getElementById('rfvPOLICY_TERMS').setAttribute("enabled",false); 
				document.getElementById('rfvPOLICY_TERMS').style.display ="none"; 
			
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
//onkeydown = "javascript:return ReadOnly();"
		function ReadOnly() {//itrack #712
		
		    if (event.keyCode == 8)
		        return false;
		}
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" scroll="yes" onload="Init();">
		<form id="REINSTATE_PROCESS" method="post" runat="server">
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
					<td class="headereffectcenter"><asp:Label ID="capHeader" runat="server"></asp:Label></td><%--Reinstatement Process--%>
				</tr>
			</TABLE>
			<TABLE cellSpacing="1" cellPadding="0" width="90%" align="center" border="0">
				<tr>
					<td><webcontrol:gridspacer id="Gridspacer2" runat="server"></webcontrol:gridspacer></td>
				</tr>
				<tr>
					<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessage" runat="server"></asp:Label></TD>
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
									<%--Done for Itrack Issue 6262 on 1 Oct 09--%>
									<TD class="midcolora" vAlign="middle"><asp:textbox id="txtEFFECTIVE_DATETIME" onchange="SetPolicyTerm_Date();" onblur="SetPolicyTerm_Date();" runat="server" size="12"
											maxlength="10" Display="Dynamic" ></asp:textbox><asp:hyperlink id="hlkReinstateEffectiveDate" runat="server" CssClass="HotSpot">
											<asp:image id="ReinstateCalenderPicker" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
												valign="middle"></asp:image>
										</asp:hyperlink><BR>
										<asp:requiredfieldvalidator id="rfvEFFECTIVE_DATETIME" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_DATETIME"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revEFFECTIVE_DATETIME" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_DATETIME"></asp:regularexpressionvalidator><asp:rangevalidator id="rngEFFECTIVE_DATE" Display="Dynamic" ControlToValidate="txtEFFECTIVE_DATETIME"
											Type="Date" Runat="server"></asp:rangevalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capEFFECTIVE_TIME" runat="server"></asp:label><span class="mandatory">*</span>
									</TD>
									<td class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_HOUR" runat="server" size="3" maxlength="2">12</asp:textbox><asp:label id="lblEFFECTIVE_HOUR" runat="server">HH</asp:label><asp:textbox id="txtEFFECTIVE_MINUTE" runat="server" size="3" maxlength="2">01</asp:textbox><asp:label id="Label1" runat="server">MM</asp:label><asp:dropdownlist id="cmbMERIDIEM" runat="server"></asp:dropdownlist><!--Removed onfocus="SelectComboIndex('cmbMERIDIEM')" by Charles on 31-Aug-09 for Itrack 6323 --><BR>
										<asp:requiredfieldvalidator id="rfvEFFECTIVE_HOUR" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_HOUR"
											ErrorMessage="Effective Time can't be blank.<br>"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="rfvEFFECTIVE_MINUTE" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_MINUTE"
											ErrorMessage="Effective Time can't be blank.<br>"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="rfvMERIDIEM" runat="server" Display="Dynamic" ControlToValidate="cmbMERIDIEM"
											ErrorMessage="cmbMERIDIEM can't be blank."></asp:requiredfieldvalidator>
										<!-- Range Validator --><asp:rangevalidator id="rnvEFFECTIVE_HOUR" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_HOUR"
											Type="Currency" MaximumValue="12" MinimumValue="0" Text="Hours must be from 0 to 12.<br>"></asp:rangevalidator><!-- Added Text for rangevalidator by Charles on 31-Aug-09 for Itrack 6323 --><asp:rangevalidator id="rnvtEFFECTIVE_MINUTE" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_MINUTE"
											Type="Integer" MaximumValue="59" MinimumValue="0" Text="Minutes must be from 0 to 59.<br>"></asp:rangevalidator><!-- customvalidator added by Charles on 31-Aug-09 for Itrack 6323 --><asp:customvalidator id="csvEFFECTIVE_HOUR" Display="Dynamic" ControlToValidate="txtEFFECTIVE_HOUR" Runat="server"
											ClientValidationFunction="txtEFFECTIVE_HOUR_VALIDATE" ErrorMessage="HH 00 or 0 must be in AM"></asp:customvalidator></td>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4"><asp:Label ID="CapTerm" runat="server"></asp:Label></TD>
								</tr>
								<tr>
									<TD class="midcolora"><asp:label id="capPOLICY_TERMS" runat="server" >Policy Term Months</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora"><asp:TextBox  runat="server" ID="txtPOLICY_TERMS" style="border:none" CssClass="midcolora" onFocus="blur()" size="5"></asp:TextBox><%=PoliyTermIn%>
									
									        <asp:dropdownlist id="cmbPOLICY_TERMS" onfocus="SelectComboIndex('cmbPOLICY_TERMS')" runat="server" style="display:none"
											></asp:dropdownlist>
											
											<BR>
										<asp:requiredfieldvalidator id="rfvPOLICY_TERMS" runat="server" Display="Dynamic" ControlToValidate="cmbPOLICY_TERMS"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora"><asp:label id="capNEW_POLICY_TERM_EFFECTIVE_DATE" runat="server">Effective Date</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" vAlign="middle"><asp:textbox id="txtNEW_POLICY_TERM_EFFECTIVE_DATE" runat="server" size="12" onkeydown = "javascript:return ReadOnly();"  maxlength="10" Display="Dynamic"></asp:textbox><asp:hyperlink id="hlkTermEffectiveDate" runat="server" CssClass="HotSpot">
											<asp:image id="TermCalenderPicker" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
												valign="middle"></asp:image>
										</asp:hyperlink><BR>
										<asp:requiredfieldvalidator id="rfvNEW_POLICY_TERM_EFFECTIVE_DATE" runat="server" Display="Dynamic" ControlToValidate="txtNEW_POLICY_TERM_EFFECTIVE_DATE"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revNEW_POLICY_TERM_EFFECTIVE_DATE" runat="server" Display="Dynamic" ControlToValidate="txtNEW_POLICY_TERM_EFFECTIVE_DATE"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capNEW_POLICY_TERM_EXPIRATION_DATE" runat="server">Expiration Date</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtNEW_POLICY_TERM_EXPIRATION_DATE" onkeydown = "javascript:return ReadOnly();"   runat="server" size="12" maxlength="10"
											Display="Dynamic" ReadOnly="True"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvNEW_POLICY_TERM_EXPIRATION_DATE" runat="server" Display="Dynamic" ControlToValidate="txtNEW_POLICY_TERM_EXPIRATION_DATE"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capAPPLY_REINSTATE_FEE" runat="server">Apply Reinstatement Fee</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbAPPLY_REINSTATE_FEE" Runat="server"></asp:dropdownlist></TD>
								</tr>
								<!-- add by pravesh  -->
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capREASON" runat="server">Reason</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREASON" onfocus="SelectComboIndex('cmbREASON')" onclick="Check();" runat="server"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvREASON" runat="server" Display="Dynamic" ControlToValidate="cmbREASON" ErrorMessage="REASON can't be blank."></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capOTHER_REASON" runat="server">Reason</asp:label><span class="mandatory" id="spnOTHER_REASON">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtOTHER_REASON" runat="server" maxlength="0" TextMode="MultiLine" Columns="50"
											Rows="10"></asp:textbox>
										<%--<asp:label id="lblOTHER_REASON" runat="server" CssClass="LabelFont">-N.A.-</asp:label>--%>
										<BR>
										<asp:customvalidator id="csvOTHER_REASON" Display="Dynamic" ControlToValidate="txtOTHER_REASON" Runat="server"
											ClientValidationFunction="txtCOMMENTS_VALIDATE"></asp:customvalidator><asp:requiredfieldvalidator id="rfvOTHER_REASON" runat="server" Display="Dynamic" ControlToValidate="txtOTHER_REASON"
											ErrorMessage="OTHER_REASON can't be blank."></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capINCLUDE_REASON_DESC" runat="server">Include Reason Description on Cancellation/Forms</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:checkbox id="chkINCLUDE_REASON_DESC" Runat="server"></asp:checkbox></TD>
									<td class="midcolora" colSpan="2"></td>
								</tr>
								<!-- by pk
								<tr id="trPREMIUM">
									<!-- by pk<TD class="midcolora" width="18%"><asp:label id="capREQUESTED_BY" runat="server">Requested By</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREQUESTED_BY" onfocus="SelectComboIndex('cmbREQUESTED_BY')" runat="server"></asp:dropdownlist></TD>
									
									<TD class="midcolora" width="18%"><asp:label id="capRETURN_PREMIUM" runat="server">Premium</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtRETURN_PREMIUM" CssClass="INPUTCURRENCY" size="12" maxlength="8" Runat="server"></asp:textbox><br>
										as p:    regularexpressionvalidator id="revRETURN_PREMIUM" Display="Dynamic" ControlToValidate="txtRETURN_PREMIUM" Runat="server"></asp:regularexpressionvalidator>
										<%--<asp:label id="lblRETURN_PREMIUM" CssClass="labelfont" Runat="server"></asp:label>--%>
									</TD>
									<td class="midcolora" colSpan="2"></td>
								</tr>
								-->
								<!-- by pk
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPRINT_COMMENTS" runat="server">Comments</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPRINT_COMMENTS" onfocus="SelectComboIndex('cmbPRINT_COMMENTS')" runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capCOMMENTS" runat="server">Comments</asp:label><span class="mandatory" id="CommentsMandatory">*</span></TD>
									<TD class="midcolora"><asp:textbox id="txtCOMMENTS" runat="server" maxlength="0" Rows="10" Columns="50" TextMode="MultiLine"></asp:textbox><asp:label id="lblCOMM" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										  :   requiredfieldvalidator id="rfvCOMMENTS" runat="server" Display="Dynamic" ControlToValidate="txtCOMMENTS"> /asp:requiredfieldvalidator> asp:   customvalidator id="csvCOMMENTS" Display="Dynamic" ControlToValidate="txtCOMMENTS" ClientValidationFunction="txtCOMMENTS_VALIDATE"
											Runat="server"><  /asp:customvalidator></  TD>
								</tr>
								-->
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4"><asp:Label ID="capPrintingOption" runat="server"></asp:Label></TD><%--Printing Options Details--%>
								</tr>
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
									<TD class="headerEffectSystemParams" colSpan="4"><asp:Label ID="capAutoCardDetail" runat="server"></asp:Label></TD><%--Auto ID Card Details--%>
								</tr>
								<tr id="trAutoIdControls">
									<TD class="midcolora" width="18%"><asp:label id="capAUTO_ID_CARD" runat="server">Auto ID Card</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbAUTO_ID_CARD" Runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capNO_COPIES" runat="server">No. of Copies</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtNO_COPIES" size="6" Runat="server" MaxLength="3"></asp:textbox><br>
										<asp:rangevalidator id="rngNO_COPIES" Display="Dynamic" ControlToValidate="txtNO_COPIES" Type="Integer"
											MaximumValue="999" MinimumValue="0" Runat="server"></asp:rangevalidator></TD>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" style="DISPLAY: none" colSpan="4">Letter</TD>
								<tr id="trLETTERS" style="DISPLAY: none">
									<TD class="midcolora" width="18%"><asp:label id="capSTD_LETTER_REQD" runat="server">Standard Letter Required</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSTD_LETTER_REQD" Runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capCUSTOM_LETTER_REQD" runat="server">Customized Letter Required</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCUSTOM_LETTER_REQD" Runat="server"></asp:dropdownlist></TD>
								</tr>
								<!-- end here -->
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" causesValidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnRollBack" runat="server" Text="Rollback" CausesValidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnPolicyDetails" runat="server" Text="View Dec Page" Visible="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnPrintPreview" style="DISPLAY: none" runat="server" Text="Preview"></cmsb:cmsbutton><br>
										<cmsb:cmsbutton class="clsButton" id="btnBackToSearch" runat="server" Text="Back To Search" causesValidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnBackToCustomerAssistant" runat="server" Text="Back To Customer Assistant"
											CausesValidation="false"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnGeneratePremiumNotice" style="DISPLAY: none" runat="server"
											Text="Premium Notice" causesValidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnComplete" runat="server" Text="Commit"></cmsb:cmsbutton>
											<cmsb:cmsbutton class="clsButton" id="btnCommitInProgress" runat="server" Text="Commit in Progress"></cmsb:cmsbutton>
											<cmsb:cmsbutton class="clsButton" id="btnGet_Premium" style="DISPLAY: none" runat="server" Text="Get Premium"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton><br>
										<cmsb:cmsbutton class="clsButton" id="btnGenerateReinstateDecPage" style="DISPLAY: none" runat="server"
											Text="Reinstatement Dec Page" causesValidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnCommitToSpool" style="DISPLAY: none" runat="server" Text="Commit To Spool"></cmsb:cmsbutton></td>
								</tr>
				</tr>
				</TBODY></TABLE>
			</TD></TR></TABLE><INPUT id="hidLOBID" type="hidden" name="hidLOBID" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" name="hidCUSTOMER_ID" runat="server"> <INPUT id="hidPOLICY_ID" type="hidden" name="hidPOLICY_ID" runat="server">
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidROW_ID" type="hidden" value="0" name="hidROW_ID" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidDisplayBody" type="hidden" value="True" name="hidDisplayBody" runat="server">
			<input id="hidPROCESS_ID" type="hidden" name="hidPROCESS_ID" runat="server"> <input id="hidPOLICY_VERSION_ID" type="hidden" name="hidPOLICY_VERSION_ID" runat="server">
			<input id="hidNEW_POLICY_VERSION_ID" type="hidden" name="hidNEW_POLICY_VERSION_ID" runat="server">
			<INPUT id="hidLOB_ID" type="hidden" name="hidLOB_ID" runat="server"> <INPUT id="hidUNDERWRITER" type="hidden" value="0" name="hidUNDERWRITER" runat="server">
			<INPUT id="hidSTATE_ID" type="hidden" value="0" name="hidSTATE_ID" runat="server">
			<INPUT id="hidADD_INT_ID" type="hidden" value="0" name="hidADD_INT_ID" runat="server">
			<INPUT id="hidPOLICY_EFFECTIVE_DATE" type="hidden" value="0" name="hidPOLICY_EFFECTIVE_DATE"
				runat="server"> <INPUT id="hidPOLICY_EXPIRY_DATE" type="hidden" value="0" name="hidPOLICY_EXPIRY_DATE"
				runat="server"> <INPUT id="hidPOLICY_TERM" type="hidden" value="0" name="hidPOLICY_TERM" runat="server">
			<INPUT id="hidPOLICY_CANCEL_DATE" type="hidden" value="0" name="hidPOLICY_CANCEL_DATE"
				runat="server"> <INPUT id="hidPOLICY_TERMS" type="hidden" value="0" name="hidPOLICY_TERMS" runat="server">
		</form>
	</body>
</HTML>

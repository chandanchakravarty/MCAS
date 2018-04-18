<%@ Page Language="c#" CodeBehind="CancellationProcess.aspx.cs" AutoEventWireup="false"
    Inherits="Cms.Policies.Processes.CancellationProcess" ValidateRequest="false" %>

<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="PolicyTop" Src="/cms/cmsweb/webcontrols/PolicyTop.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<html>
<head>
    <title>Policy Cancellation Process</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet">

    <script src="/cms/cmsweb/scripts/xmldom.js"></script>

    <script src="/cms/cmsweb/scripts/common.js"></script>

    <script src="/cms/cmsweb/scripts/form.js"></script>

    <script src="/cms/cmsweb/scripts/Calendar.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>

    <script language="javascript">
  
		var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		var app_effective_date;
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
				document.getElementById('txtEFFECTIVE_DATETIME').value  = '';
				document.getElementById('txtEFFECTIVE_HOUR').value  = '12';
				document.getElementById('txtEFFECTIVE_MINUTE').value  = '01';
				document.getElementById('cmbMERIDIEM').options.selectedIndex ='0';
				document.getElementById('cmbCANCELLATION_OPTION').options.selectedIndex = -1;
				document.getElementById('cmbCANCELLATION_TYPE').options.selectedIndex = -1;
				document.getElementById('cmbREASON').options.selectedIndex = -1;
				document.getElementById('txtOTHER_REASON').value  = '';
				document.getElementById('cmbREQUESTED_BY').options.selectedIndex = -1;				
				document.getElementById('lblAGENT_PHONE_NUMBER').value  = '';
				document.getElementById('txtRETURN_PREMIUM').value  = '';
				//document.getElementById('lblPAST_DUE_PREMIUM').value  = '';						
				//document.getElementById('cmbPRINT_COMMENTS').options.selectedIndex = -1;	
				//document.getElementById('txtCOMMENTS').value  = '';
				DisableValidators();
				ChangeColor();
				
			}
			
			function populateXML()
			{
				
				if(document.getElementById('hidFormSaved').value == '0')
				{
					var tempXML;
					if(document.getElementById("hidOldData").value != "" && document.getElementById("hidOldData").value != '0')
					{
						var cmbINSURED =  document.getElementById('cmbINSURED');
						cmbINSURED.setAttribute('disabled',false);
						populateFormData(document.getElementById("hidOldData").value, PROCESS_CANCELLATION);
						document.getElementById("txtEFFECTIVE_DATETIME").value = document.getElementById("hidEFF_DAT").value
						
						
					}
					else
					{
						AddData();
					}
				}
				//Check();
				//CommentEnable();
				
				ShowHideSendNoticesButton();
				document.getElementById('cmbCANCELLATION_TYPE').focus();
				
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
	
	function SetDefaultValues(SelectedComboValue)
	{
		var BILL_TYPE = document.getElementById('hidBILL_TYPE').value;
		var Pro_Rata = "11994";
		var op_Flat = "11995";
		var Op_equity = "11996";
		var CommitToPrintSpool = <%=(int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL%>;
		var MichiganMailers = <%=(int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROC_PRINT_OPTIONS_CANCEL.MICHIGAN_MAILERS%>;
		var NoPrintRequired = <%=(int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED%>;
		var OnDemand = <%=(int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND%>;		
		var CancelledProRateNonPayment = "11550";
		var CancelledFlatNonPayment = "11552";
		var EquityCancelledForNonPayment = "11551"; //Added on 30 June 2008 Itrack 4401
		switch(SelectedComboValue)
		{		
			case "<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.INSURED_REQUEST).ToString()%>":
				SetValues('','12','01',0,Op_equity,false,NoPrintRequired,CommitToPrintSpool,CommitToPrintSpool,true,'11549','10964','6496');
				document.getElementById('hidINSURED').value = NoPrintRequired;
				document.getElementById('hidREQUESTED_BY').value = '6496';
				break;			
			case "<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.CANCEL_REWRITE).ToString()%>":
			//Added by Praveen : 10 April 2008 Itrack #3984
				SetValues('','12','01',0,Pro_Rata,false,NoPrintRequired,CommitToPrintSpool,CommitToPrintSpool,true,'','10964','6495');
				CallService();
				document.getElementById('hidREQUESTED_BY').value = '6495';
				break;
			case "<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.CANCEL_REINSTATEMENT).ToString()%>": 
				SetValues('','12','01',0,Pro_Rata,false,NoPrintRequired,CommitToPrintSpool,CommitToPrintSpool,true,'','10964','6495');
				CallService();
				document.getElementById('hidREQUESTED_BY').value = '6495';
				break;
			case "<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.RESCINDED).ToString()%>": 
				//SetValues('','12','01',0,Pro_Rata,false,NoPrintRequired,CommitToPrintSpool,CommitToPrintSpool,true,'','10964','0');
				//commented by pravesh as these cancellationtype Will be treated as Company Request By Default
				var EffectiveDate =	document.getElementById('hidAPP_EFFECTIVE_DATE').value ;
				EffectiveDate =new Date(EffectiveDate);
				var strEffectiveDate = (EffectiveDate.getMonth() + 1) + "/" + EffectiveDate.getDate() + "/" + EffectiveDate.getFullYear();
				SetValues(strEffectiveDate,'12','01',0,op_Flat,false,OnDemand,CommitToPrintSpool,OnDemand,true,'11545','10964','6495');
				CallService();
				document.getElementById('hidREQUESTED_BY').value = '6495';
				break;
			case "<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.COMPANY_REQUEST).ToString()%>":								
				SetValues(document.getElementById("hidEffDateForCompanyRequest").value,'12','01',0,Pro_Rata,false,OnDemand,CommitToPrintSpool,OnDemand,true,'','10964','6495');
				CallService();
				document.getElementById('hidREQUESTED_BY').value = '6495';
				break;
			case "<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.NSF_REPLACE).ToString()%>":
				SetValues(document.getElementById('hidEFFECTIVE_DATE').value,'12','01',0,Pro_Rata,false,NoPrintRequired,CommitToPrintSpool,CommitToPrintSpool,true,'11553','10964','6495');
				document.getElementById('hidINSURED').value = NoPrintRequired;
				CallService();
				break;
				
			case "<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.NSF_NO_REPLACEMENT).ToString()%>":
				//SetValues(document.getElementById('hidEFFECTIVE_DATE').value,'12','01',0,Pro_Rata,false,OnDemand,CommitToPrintSpool,CommitToPrintSpool,true,'11553','10963','6495');  // commented as per Itrack 5959
				SetValues(document.getElementById('hidEFFECTIVE_DATE').value,'12','01',0,Pro_Rata,false,CommitToPrintSpool,CommitToPrintSpool,CommitToPrintSpool,true,'11553','10963','6495'); // changed as per Itrack 5959
				document.getElementById('hidREQUESTED_BY').value = '6495';
				CallService();
				break;
			case "<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT).ToString()%>":
				if(BILL_TYPE=='<%=Cms.BusinessLayer.BlProcess.ClsCancellationProcess.BILL_PAYMENT_DIRECT_BILL%>')
				{
				
					var ReasonCode=CancelledFlatNonPayment;
					var Can_option =document.getElementById('cmbCANCELLATION_OPTION').options[document.getElementById('cmbCANCELLATION_OPTION').selectedIndex].value;
					if(Can_option==Pro_Rata)
						ReasonCode = CancelledProRateNonPayment;
					var EffectiveDate
					//var ExpiryDate = "<%= APP_EXPIRATION_DATE%>";
					var ExpiryDate = document.getElementById('hidAPP_EXPIRATION_DATE').value;
					var ExpiryDate = new Date(ExpiryDate);
					if(Can_option==Op_equity)	
					{
						var myDate=new Date()
						var GraceDay=parseInt(document.getElementById('hidGRACE_PERIOD').value); //geting grace period
						myDate.setDate(myDate.getDate()+GraceDay)
						/*Commented by pravesh on 6 Aug 2008 as change in logic as discussed with ravinder
						var day=myDate.getDay()
						if (day==6) //if saturday
							myDate.setDate(myDate.getDate()+2)
						else if (day== 0) //if sunday
							myDate.setDate(myDate.getDate()+1)
						*/ 	
						EffectiveDate = new Date(document.getElementById('hidCANCELLATION_DATE_EQUITY').value);
						//EffectiveDate.setDate(EffectiveDate.getDate() + GraceDay);	// Commented by swarup Itrack Issue # 3057
										
						
						if (myDate > EffectiveDate)
							EffectiveDate=myDate;
							
						//Added on 30 June 2008 ITRACK 4401
						ReasonCode = EquityCancelledForNonPayment;	
							
							
					}
					else
					{
					EffectiveDate = new Date(document.getElementById('hidEFFECTIVE_DATE').value);
					}
					EffectiveDate.setDate(EffectiveDate.getDate() + 1);	
					if(EffectiveDate > ExpiryDate)
					{
							EffectiveDate = ExpiryDate;	
					}
										
					var strEffDate = (EffectiveDate.getMonth() + 1) + "/" + EffectiveDate.getDate() + "/" + EffectiveDate.getFullYear();
					
					SetValues(strEffDate,'12','01',0,Can_option,false,MichiganMailers,MichiganMailers,MichiganMailers,true,ReasonCode,'10963','6495');
				}
				else
					SetValues(document.getElementById('hidEFFECTIVE_DATE').value,'12','01',0,Pro_Rata,false,OnDemand,CommitToPrintSpool,CommitToPrintSpool,true,CancelledProRateNonPayment,'10963','6495');
				CallService();	
				break;
			case "<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.AGENTS_REQUEST).ToString()%>":
				SetValues(document.getElementById('hidEFFECTIVE_DATE').value,'12','01',0,Pro_Rata,false,OnDemand,CommitToPrintSpool,CommitToPrintSpool,true,'114189','10963','6494');
				CallService();
				document.getElementById('hidREQUESTED_BY').value = '6494';
				break;				
			default:
				//SetValues('','12','01',0,Pro_Rata,false,NoPrintRequired,CommitToPrintSpool,CommitToPrintSpool,true,'','10964');
				SetValues(document.getElementById("hidEffDateForCompanyRequest").value,'12','01',0,Pro_Rata,false,OnDemand,CommitToPrintSpool,OnDemand,true,'','10964','0');
				CallService();
				break;
		}
	}
	function PopulateCancellationOption(CancellationTypeValue)
	{
		if(document.getElementById("hidCANCEL_RULE_XML").value=="")
			return;
		
		var PaymentCode = document.getElementById("hidPOLICY_PAYMENT_CODE").value;
		var BILL_TYPE = document.getElementById('hidBILL_TYPE').value;
		//var PaymentCode = "NBS-PTRCD";
		//CancellationTypeValue = "11989";
		var xmlDoc = new ActiveXObject("Microsoft.XMLDOM") ;
		var OptionText,OptionValue;
		xmlDoc.async=false;
		xmlDoc.loadXML(document.getElementById("hidCANCEL_RULE_XML").value);
		if(BILL_TYPE=='<%=Cms.BusinessLayer.BlProcess.ClsCancellationProcess.BILL_PAYMENT_DIRECT_BILL%>')
			xmlNodes = xmlDoc.selectNodes("/Root/Group[@Code='" + PaymentCode + "']/Rule[@Action='Master']/Conditions/Condition[@Operand1='$CANC_TYPE' and @Operand2='" + CancellationTypeValue + "']/SubConditions/SubCondition");		
		else
			xmlNodes = xmlDoc.selectNodes("/Root/Group[@Code='AB']/Rule[@Action='Master']/Conditions/Condition[@Operand1='$CANC_TYPE' and @Operand2='" + CancellationTypeValue + "']/SubConditions/SubCondition");		
		if(xmlNodes==null || xmlNodes.length==0)
			return;
		var LookupId;		
		document.getElementById('cmbCANCELLATION_OPTION').options.length=0;						
		for(var i=0;i<xmlNodes.length;i++)
		{
			LookupId = xmlNodes[i].attributes(2).value;	
			document.getElementById('cmbCANCELLATION_OPTION').options[document.getElementById('cmbCANCELLATION_OPTION').options.length] = new Option(CancelOptionDesc(LookupId),LookupId);
		}
		if (document.getElementById('cmbCANCELLATION_OPTION').options.length==1)
		{
		     SelectComboOption('cmbCANCELLATION_OPTION',document.getElementById('cmbCANCELLATION_OPTION').options[0].value);		
		     document.getElementById('hidCANCELLATION_OPTION').value=document.getElementById('cmbCANCELLATION_OPTION').options[0].value
		}
		else
			SelectComboOption('cmbCANCELLATION_OPTION',document.getElementById('hidCANCELLATION_OPTION').value);
	}
	
	
	function SetEffectiveDateOnSendCancellation()
	{
	    var Can_option =document.getElementById('cmbCANCELLATION_TYPE').options[document.getElementById('cmbCANCELLATION_TYPE').selectedIndex].value;
		
		if(Can_option =='11969') // cancellation notice will be send only for non payment Non payment
		{
		return true
//	     alert('Effective Date will be Readjusted.');
//	     SelectedComboValue=document.getElementById('cmbCANCELLATION_TYPE').options[document.getElementById('cmbCANCELLATION_TYPE').selectedIndex].value;
//	     SetDefaultValues(SelectedComboValue)
//		 }
//	     Page_ClientValidate();	     
//	     if (!Page_IsValid)
//	     {      	
//	     	document.getElementById('hidNOTICE_CLICK').VALUE = '1';
//			return false;
//		  }
//		  else
//		  {
//			 //HideRollbackSaveButton();
//			// DisableButton(document.getElementById('btnGenerate_cancellation_notice_or_Memo'));
//			 top.topframe.disableMenus('1,7','ALL');//Added for Itrack Issue 6203 on 31 July 2009
		  }else
		 return false;
	}
	function HideRollbackSaveButton()
	{
				document.getElementById('btnSave').disabled = true;
				document.getElementById('btnRollBack').disabled = true;	
				 DisableButton(document.getElementById('btnComplete'));
	}
	function HideShowCommit()
	{
				document.getElementById('btnComplete').disabled = true;
				if (document.getElementById('btnSave'))	
					document.getElementById('btnSave').disabled=true;
			 DisableButton(document.getElementById('btnRollBack'));
			 top.topframe.disableMenus('1,7','ALL');//Added for Itrack Issue 6203 on 31 July 2009
					
	}
	function CancelOptionDesc(CancelOptionId)
	{
		var xmlDoc = new ActiveXObject("Microsoft.XMLDOM") ;
		xmlDoc.loadXML(document.getElementById("hidCANCEL_OPTIONS_XML").value);		
		var xmlNodes = xmlDoc.selectNodes("/NewDataSet/Table");		
		for(var i=0;i<xmlNodes.length;i++)
		{
			if(xmlNodes[i].childNodes.item(0).text==CancelOptionId)
				return xmlNodes[i].childNodes.item(1).text;		
		}
		return "";
	}
	
	function SetValues(EFFECTIVE_DATE,EFFECTIVE_HOUR,EFFECTIVE_MINUTE,MERIDIEM,CANCELLATION_OPTION,PRINTING_OPTIONS,INSURED,AGENCY_PRINT,ADD_INT,SEND_ALL,REASON,CUSTOM_LETTER_REQD,REQUESTED_BY)
	{
		document.getElementById('hidCANCELLATION_OPTION').value = CANCELLATION_OPTION;
		SelectComboOption('cmbCANCELLATION_OPTION',CANCELLATION_OPTION);
		document.getElementById('txtEFFECTIVE_MINUTE').value = EFFECTIVE_MINUTE;
		document.getElementById('txtEFFECTIVE_HOUR').value = EFFECTIVE_HOUR;
		SelectComboOption('cmbMERIDIEM',MERIDIEM);
		document.getElementById('chkPRINTING_OPTIONS').checked = PRINTING_OPTIONS;
		SelectComboOption('cmbINSURED',INSURED);		
		SelectComboOption('cmbAGENCY_PRINT',AGENCY_PRINT);
		SelectComboOption('cmbADD_INT',ADD_INT);
		SelectComboOption('cmbREQUESTED_BY',REQUESTED_BY);
		document.getElementById('chkSEND_ALL').checked = SEND_ALL;
		SelectComboOption('cmbREASON',REASON);
		//SelectComboOption('cmbCUSTOM_LETTER_REQD',CUSTOM_LETTER_REQD);		
		document.getElementById('txtEFFECTIVE_DATETIME').value = EFFECTIVE_DATE;
		document.getElementById('hidEFFECTIVE_DATETIME').value =EFFECTIVE_DATE;
		//chkSEND_ALL_Change();
		//setEffectiveDateEnableDisable();
		onOptionChange();
	}
	function setEffectiveDateEnableDisable()
	{
	      // if(document.getElementById('cmbCANCELLATION_OPTION').selectedIndex == -1 || document.getElementById('cmbCANCELLATION_OPTION').selectedIndex ==0)
			//return;			
        
		var CANCELLATION_OPTION=document.getElementById('cmbCANCELLATION_OPTION').options[document.getElementById('cmbCANCELLATION_OPTION').selectedIndex].value;
		if (CANCELLATION_OPTION=='11996' || CANCELLATION_OPTION=='11994' ) // equity/short rate  || Pro rata---user can enter date and return premium should calculate from short term rate table//for brazile
        {
		    document.getElementById('txtRETURN_PREMIUM').setAttribute('readOnly',true);
		    document.getElementById('txtEFFECTIVE_DATETIME').setAttribute('readOnly',false);
		    document.getElementById('txtEFFECTIVE_MINUTE').setAttribute('readOnly',false);
		    document.getElementById('txtEFFECTIVE_HOUR').setAttribute('readOnly',false);
		    document.getElementById('cmbMERIDIEM').setAttribute('disabled',false);
		}
		else
		{
		document.getElementById('txtEFFECTIVE_DATETIME').setAttribute('readOnly',false);
		document.getElementById('txtEFFECTIVE_MINUTE').setAttribute('readOnly',false);
		document.getElementById('txtEFFECTIVE_HOUR').setAttribute('readOnly',false);
		document.getElementById('cmbMERIDIEM').setAttribute('disabled',false);
		}
        EffectivdateReadonly();//Added By Lalit
	}
    function EffectivdateReadonly()//function aadded by Lalit for tfs #463
    {
          
       var CANCELLATION_Type=document.getElementById('cmbCANCELLATION_TYPE').value;
       if (CANCELLATION_Type=='11969' || CANCELLATION_Type=='11969' ) //Changed by Lalit for brazil.if cancellation type is non Pay then effective date shold readonly
        {
          document.getElementById('txtEFFECTIVE_DATETIME').setAttribute('readOnly',true);
         
        }
        
    }   
	function onOptionChange()
	{
	
		var cmbOp=document.getElementById('cmbCANCELLATION_OPTION');
		if(cmbOp.selectedIndex>-1) 
		 document.getElementById('hidCANCELLATION_OPTION').value=cmbOp.options[cmbOp.selectedIndex].value;
		 
		 //commented by lalit
//		if (document.getElementById('hidCANCELLATION_OPTION').value=='11995') //if flat make effecive date as pol effective dt and read only itrack# 4427
//		{
//		// EffectiveDate =  document.getElementById('hidEFFECTIVE_DATETIME').value; //new Date(document.getElementById('hidAPP_EFFECTIVE_DATE').value);
//    	 //var strEffDate =EffectiveDate;//DateConvert(EffectiveDate,) //(EffectiveDate.getMonth() + 1) + "/" + EffectiveDate.getDate() + "/" + EffectiveDate.getFullYear();
//		 document.getElementById('txtEFFECTIVE_DATETIME').value=strEffDate;
//		}
		setEffectiveDateEnableDisable();
	}
	function ShowHideSendNoticesButton()
	{
		var combo = document.getElementById('cmbCANCELLATION_TYPE');
		if(combo==null || combo.selectedIndex==0 || combo.selectedIndex== -1)
			return;

		var cmbINSURED =  document.getElementById('cmbINSURED');
		
		if(combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.INSURED_REQUEST).ToString()%>" || combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.NSF_REPLACE).ToString()%>")		
		{
			cmbINSURED.setAttribute('disabled',true);
		}
		else
		{
			cmbINSURED.setAttribute('disabled',false);
		}
			
		/*if(combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.COMPANY_REQUEST).ToString()%>")		
		{
			//The button Save and Send Notices will be applicable only for Company Request.
			document.getElementById('btnSave_Send_Notice').style.display="inline";
		}
		else
		{
			document.getElementById('btnSave_Send_Notice').style.display="none";		
		}*/	
	
	} 
	
		
	function cmbCANCELLATION_TYPE_Change(Init)
	{	

		var combo = document.getElementById('cmbCANCELLATION_TYPE');
		if(combo==null || combo.selectedIndex==0 || combo.selectedIndex== -1)
			return;
		//PopulateCancellationOption(combo.options[combo.selectedIndex].value);
		//ShowHideSendNoticesButton();
		//if(Init)
		//	return;
		
		SetDefaultValues(combo.options[combo.selectedIndex].value);
//		if(combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.RESCINDED).ToString()%>")		
//		{
//			document.getElementById('txtRETURN_PREMIUM').readOnly=false;		
//		}
//		else if(combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.NSF_REPLACE).ToString()%>")		
//		{
//			document.getElementById('txtRETURN_PREMIUM').readOnly=false;
//			SetDefaultValues(combo.options[combo.selectedIndex].value);
//		}
//		else if(combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.NSF_NO_REPLACEMENT).ToString()%>")		
//		{
//			document.getElementById('txtRETURN_PREMIUM').readOnly=false;
//			SetDefaultValues(combo.options[combo.selectedIndex].value);
//		}
		if(combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT).ToString()%>")		
		{
			document.getElementById('txtRETURN_PREMIUM').readOnly=true;
			
			SetNonPayCancellationDate();
			
		}
		else if(combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.COMPANY_REQUEST).ToString()%>")		
		{
			document.getElementById('txtRETURN_PREMIUM').readOnly=true;
               CallService();
               //CarrierRequestCancellation(); 			
		}
		else
		{
			document.getElementById('txtRETURN_PREMIUM').readOnly=true;		
			if(!Init)
				CallService();
		}
		cmbADD_INT_Change();
	SetCancellationNoticButton();
	}	
	//Added By Lalit  For NonPay Cancellation Block	
	function SetNonPayCancellationDate()
	{
	    var CUSTOMER_ID = document.getElementById('hidCUSTOMER_ID').value;
	    var POLICY_ID = document.getElementById('hidPOLICY_ID').value;
	     var POLICY_VERSION_ID = document.getElementById('hidNEW_POLICY_VERSION_ID').value;
     //call page web method	    
	  PageMethod("GetNonPayCancellationDate", ["CUSTOMER_ID", CUSTOMER_ID, "POLICY_ID", POLICY_ID,"POLICY_VERSION_ID",POLICY_VERSION_ID], AjaxSucceeded, AjaxFailed); //With parameters

	}
function PageMethod(fn, paramArray, successFn, errorFn) {

            var pagePath = window.location.pathname;
            var paramList = '';
            if (paramArray.length > 0) {
                for (var i = 0; i < paramArray.length; i += 2) {
                    if (paramList.length > 0) paramList += ',';
                    paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                }
            }
            paramList = '{' + paramList + '}';
            $.ajax({
                type: "POST",
                url: pagePath + "/" + fn,
                contentType: "application/json; charset=utf-8",
                data: paramList,
                dataType: "json",
                success: successFn,
                error: errorFn
            });
        }

        function AjaxSucceeded(result) {      
              setNonPaypolCencDate(result)
        }

        function AjaxFailed(result) {
        CallService();
            document.getElementById('txtEFFECTIVE_DATETIME').value=  DateConvert(document.getElementById('txtEFFECTIVE_DATETIME').value,jsaAppDtFormat)
	        document.getElementById('hidEFFECTIVE_DATETIME').value= document.getElementById('txtEFFECTIVE_DATETIME').value
	        document.getElementById('txtRETURN_PREMIUM').value=  "0";
	        document.getElementById('txtEFFECTIVE_DATETIME').setAttribute("readOnly",true);
	        document.getElementById('txtRETURN_PREMIUM').setAttribute("readOnly",true);
        
         //alert(document.getElementById('txtEFFECTIVE_DATETIME').value)
        }
	    function setNonPaypolCencDate(result)
	    {
	        CallService();
	        document.getElementById('txtEFFECTIVE_DATETIME').value=  DateConvert(result.d,jsaAppDtFormat)
	        document.getElementById('hidEFFECTIVE_DATETIME').value= document.getElementById('txtEFFECTIVE_DATETIME').value
	        document.getElementById('txtRETURN_PREMIUM').value=  "0";
	        document.getElementById('txtEFFECTIVE_DATETIME').setAttribute("readOnly",true);
	        document.getElementById('txtRETURN_PREMIUM').setAttribute("readOnly",true);
	     }
	     
	     //End Non Pay Cancellation Block
	   function DateConvert(Date, dateFormate)
		{
            if(Date=="" || Date.length<8) return "";
            var returnDate=''; 
			var saperator = '/';
			var firstDate, secDate;

			var strDateFirst = Date.split("/");
			//var strDateSec = DateSec.split("/");

			if(dateFormate.toLowerCase() == "dd/mm/yyyy")
			{
				//alert("dd/mm/yyyy")
				returnDate = (strDateFirst[1].length != 2 ? '0' + strDateFirst[1] : strDateFirst[1]) + '/' + (strDateFirst[0].length != 2 ? '0' + strDateFirst[0] : strDateFirst[0])  + '/' + (strDateFirst[2].length != 4 ? '0' + strDateFirst[2] : strDateFirst[2]);
				//secDate = (strDateSec[1].length != 2 ? '0' + strDateSec[1] : strDateSec[1]) + '/' + (strDateSec[0].length != 2 ? '0' + strDateSec[0] : strDateSec[0])  + '/' + (strDateSec[2].length != 4 ? '0' + strDateSec[2] : strDateSec[2]);
			}

			if(dateFormate.toLowerCase() == "mm/dd/yyyy")
			{
				//alert("mm/dd/yyyy")
				returnDate = Date
				//secDate = DateSec;
			}

			return returnDate;
		}
	// End lalit changes
	
	function CommentEnable()
	{
		if (document.getElementById('cmbPRINT_COMMENTS').selectedIndex != '-1')		
		{
		
				if (document.getElementById('cmbPRINT_COMMENTS').options[document.getElementById('cmbPRINT_COMMENTS').selectedIndex].value == '0')
				{				
					document.getElementById('txtCOMMENTS').style.display='none';
					//document.getElementById('txtCOMMENTS').value = '';
					document.getElementById('rfvCOMMENTS').enabled = false;
					document.getElementById('rfvCOMMENTS').style.display='none';
					document.getElementById('CommentsMandatory').style.display='none';
					document.getElementById('lblCOMM').style.display='inline';						
				}
				else
				{			
					document.getElementById('txtCOMMENTS').style.display='inline';
					//document.getElementById('rfvCOMMENTS').style.display='inline';
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
				//document.getElementById('txtCOMMENTS').value = '';
		}
	
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
		//validating Cancellation Type
		function validCANCELLATION_TYPE(source, arguments)
		{
			var CanType = arguments.Value;
			var billtype= document.getElementById('hidBILL_TYPE').value;
			if((billtype!='<%=Cms.BusinessLayer.BlProcess.ClsCancellationProcess.BILL_PAYMENT_DIRECT_BILL%>' &&
			    (CanType=="11992" || CanType=="11993" ) ) 
			    || (CanType=="11987" && billtype=='<%=Cms.BusinessLayer.BlProcess.ClsCancellationProcess.BILL_PAYMENT_DIRECT_BILL%>')) //NSF/ Replace or NSF/ No Replace or Agents requests with AB
				{
					arguments.IsValid = false;
					return false;   // invalid cancellation type
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
		
		
		function DisplayAgentPhoneNo()
		{
			if(document.getElementById('cmbREQUESTED_BY').selectedIndex == -1 || document.getElementById('cmbREQUESTED_BY').selectedIndex == 0)
				return;
				
			if (document.getElementById('cmbREQUESTED_BY').options[document.getElementById('cmbREQUESTED_BY').selectedIndex].value == '1')
			{
				document.getElementById('lblAGENT_PHONE_NUMBER').style.display = 'inline';
				document.getElementById('capAGENTPHONENO').style.display = 'inline';
			}
			else
			{
				document.getElementById('lblAGENT_PHONE_NUMBER').style.display = 'none';
				document.getElementById('capAGENTPHONENO').style.display = 'none';
			}
		}
		
		function ShowDetailsPolicy()
		{
			/*if(document.getElementById('hidNEW_POLICY_VERSION_ID').value!="" &&  document.getElementById('hidNEW_POLICY_VERSION_ID').value!="0")
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
			document.PROCESS_CANCELLATION.reset();
			//CommentEnable();
			//Check();
			DisplayAgentPhoneNo();			
			DisableValidators();
			ChangeColor();
			return false;
		}
		function chkSEND_ALL_Change()			
			{
				var chk = document.getElementById('chkSEND_ALL');
				if(chk==null)
					return false;								
				//document.getElementById("hidADD_INT_ID").value='';
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
				/*var chk = document.getElementById('chkSEND_ALL');
				if(chk==null)
					return false;	*/
					
				if(flag)
					document.getElementById('trAddIntList').style.display="none";
				else
					document.getElementById('trAddIntList').style.display="inline";
			}
		function cmbADD_INT_Change()
		{
			combo = document.getElementById('cmbADD_INT');
			if(combo==null || combo.selectedIndex==-1)
				return false;
			//MICHIGAN_MAILERS #ITRACK 4068	
			if(combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString()%>"
						|| combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()%>"
						|| combo.options[combo.selectedIndex].value=="<%=((int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.MICHIGAN_MAILERS).ToString()%>"
						)
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
				//document.getElementById('hidADD_INT_ID').value = '';
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
				document.getElementById("hidEFFECTIVE_DATETIME").value	 = document.getElementById("txtEFFECTIVE_DATETIME").value
			}	
			function HideShowCommitInProgress()
			{
				IsDBCancellationOptionExists();
				document.getElementById('btnCommitInProgress').style.display="inline";
				document.getElementById('btnCommitInProgress').disabled = true;
				document.getElementById('btnComplete').style.display="none";
				document.getElementById('btnRollBack').disabled = true;	
				 DisableButton(document.getElementById('btnComplete'));
				top.topframe.disableMenus('1,7','ALL');//Added for Itrack Issue 6203 on 31 July 2009
			}
			function IsDBCancellationOptionExists()
			{
				var  canOption=document.getElementById('cmbCANCELLATION_OPTION');
				var  DBcanOption=document.getElementById('hidCANCELLATION_OPTION_DB').value;
				var len = canOption.options.length;			
				var found=false;				
				for (var i = len- 1; i > -1 ; i--)
				{
					if(canOption.options(i).value ==DBcanOption)
					{
					found=true;
					break;
					}										
				}	
				document.getElementById('hidIS_CANCELLATION_OPTION_EXISTS').value = found;
				//return found;
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
				}/*	
				len = coll.options.length;	
				if(	num < len )
				{
					document.getElementById('cmbUnAssignAddInt').options(num).selected = true;
				}	
				else
				{
					if(num>0)
						document.getElementById('cmbUnAssignAddInt').options(num - 1).selected = true;
				}	*/			
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
			function CallService()
			{ 		
            
			
				//setCancellationOptionForEffectiveDate();
				var comboCanType = document.getElementById('cmbCANCELLATION_TYPE');
				if(comboCanType==null || comboCanType.selectedIndex==0)
					Cantype="0";
				else	
					Cantype=comboCanType.options[comboCanType.selectedIndex].value;
				var comboCanOption = document.getElementById('cmbCANCELLATION_OPTION');
				if(comboCanOption==null || comboCanOption.selectedIndex==-1)
					CanOption="0";
				else
					CanOption=comboCanOption.options[comboCanOption.selectedIndex].value;
				
				
				//if( typeof(myTSMain.useService) == 'undefined') 
				//{
				//	setTimeout( 'CallService()', 1000);
				//}
				//else 
				//{
					//myTSMain.useService(lstr.toString(), "TSM");
					
				if(Cantype == '11969')	
				{
				    document.getElementById("txtRETURN_PREMIUM").value = "0";
					document.getElementById("hidRETURN_PREMIUM").value = "0";
					
					return;
				}

                if(comboCanType.value=="<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.INSURED_REQUEST).ToString()%>")		
					if(document.getElementById('txtEFFECTIVE_DATETIME').value!="")
					{
						var strEffectiveDate = document.getElementById("txtEFFECTIVE_DATETIME").value = FormatDateForGrid(document.getElementById("txtEFFECTIVE_DATETIME"),'');						
						if(DateComparer(strEffectiveDate,strEffectiveDate,jsaAppDtFormat))
						{
						    //Commented By Lalit Chauhan for new logic to calculate return premium 
						    //CancellationProcess.AjaxCancelProcReturnPremiumAmount(document.getElementById('hidCUSTOMER_ID').value ,document.getElementById('hidPOLICY_ID').value,document.getElementById('hidPOLICY_VERSION_ID').value,strEffectiveDate,Cantype,CanOption,createData);							
							//Added By Lalit Chauhan for new logic to calculate return premium 
							CancellationProcess.PolicyCancellationReturnPremium(document.getElementById('hidCUSTOMER_ID').value ,document.getElementById('hidPOLICY_ID').value,document.getElementById('hidNEW_POLICY_VERSION_ID').value,strEffectiveDate,Cantype,CanOption,createData);
							//myTSMain.TSM.callService(createData, "CancelProcReturnPremiumAmount", document.getElementById('hidCUSTOMER_ID').value ,document.getElementById('hidPOLICY_ID').value,document.getElementById('hidPOLICY_VERSION_ID').value,strEffectiveDate,Cantype,CanOption);
						}
					}
					
		//					
            if(comboCanType.value=="<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.COMPANY_REQUEST).ToString()%>")		
					if(document.getElementById('txtEFFECTIVE_DATETIME').value!="")
					{
						var strEffectiveDate = document.getElementById("txtEFFECTIVE_DATETIME").value = FormatDateForGrid(document.getElementById("txtEFFECTIVE_DATETIME"),'');						
						if(DateComparer(strEffectiveDate,strEffectiveDate,jsaAppDtFormat))
						{
						    //Added By Lalit Chauhan for new logic to calculate prorated return premium 
							CancellationProcess.CarrierRequestCancellation(document.getElementById('hidCUSTOMER_ID').value ,document.getElementById('hidPOLICY_ID').value,document.getElementById('hidNEW_POLICY_VERSION_ID').value,strEffectiveDate,createData);
							
						}
					}

			
				//}
				//setCancellationOptionForEffectiveDate();
			}	
			//added for Insured Request as per itrack issues 2217		
			function setCancellationOptionForEffectiveDate()
			{
				var combo = document.getElementById('cmbCANCELLATION_TYPE');
				if(combo==null || combo.selectedIndex==0)
					return;
				var CanCtype=combo.options[combo.selectedIndex].value;
				
				if( CanCtype== "<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.CANCEL_REWRITE).ToString()%>"			
				|| CanCtype== "<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.CANCEL_REINSTATEMENT).ToString()%>"
				|| CanCtype== "<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.RESCINDED).ToString()%>")//Added by Charles on 18-Sep-09 for Itrack 6391
				{
					var strEffectiveDate = document.getElementById("txtEFFECTIVE_DATETIME").value = FormatDateForGrid(document.getElementById("txtEFFECTIVE_DATETIME"),'');						
					var strEffectiveDateActual=	document.getElementById('hidAPP_EFFECTIVE_DATE').value;//'<%= APP_EFFECTIVE_DATE %>'; //Changed by Charles on 14-Oct-09 for Itrack 6391					
					if(strEffectiveDate!='')
					{
						if(CompareTwoDate(strEffectiveDate, strEffectiveDateActual, jsaAppDtFormat,''))							
							CANCELLATION_OPTION=11994;	//Pro Rata						
						else //Else added by Charles on 14-Oct-09 for Itrack 6391
							CANCELLATION_OPTION=11995; //Flat							
						document.getElementById('hidCANCELLATION_OPTION').value = CANCELLATION_OPTION;
						SelectComboOption('cmbCANCELLATION_OPTION',CANCELLATION_OPTION);						
					}
				}//Added till here
				
				if ( CanCtype== "<%=((int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.INSURED_REQUEST).ToString()%>")
				{
					var strEffectiveDate = document.getElementById("txtEFFECTIVE_DATETIME").value = FormatDateForGrid(document.getElementById("txtEFFECTIVE_DATETIME"),'');						
					var strEffectiveDateActual=	document.getElementById('hidAPP_EFFECTIVE_DATE').value;//'<%= APP_EFFECTIVE_DATE %>'; //Changed by Charles on 14-Oct-09 for Itrack 6391					
					//"11994">Pro Rata
					//"11995">Flat
					if(CompareTwoDate(strEffectiveDate, strEffectiveDateActual, jsaAppDtFormat,''))
						CANCELLATION_OPTION=11994;
					else
						CANCELLATION_OPTION=11995;
					document.getElementById('hidCANCELLATION_OPTION').value = CANCELLATION_OPTION;
					SelectComboOption('cmbCANCELLATION_OPTION',CANCELLATION_OPTION);
					onOptionChange();
				}
				return false;
			}
			function createData(Result)
			{
				if(!(Result.error) && Result.value!="undefined" && Result.value != "")
				{
				    document.getElementById("txtRETURN_PREMIUM").readOnly=true;
					document.getElementById("txtRETURN_PREMIUM").value = formatCurrency(Result.value);
					document.getElementById("hidRETURN_PREMIUM").value = formatAmount(Result.value);
					//alert(document.getElementById("hidRETURN_PREMIUM").value);
				}else
				{
				    document.getElementById("txtRETURN_PREMIUM").readOnly=true;
					document.getElementById("txtRETURN_PREMIUM").value = formatCurrency("0");
					document.getElementById("hidRETURN_PREMIUM").value = formatAmount("0");
				}
				var Canctype=document.getElementById('cmbCANCELLATION_TYPE').value;
				if(Canctype == '11969')	{
        			document.getElementById("txtRETURN_PREMIUM").value = "0";
        			document.getElementById("hidRETURN_PREMIUM").value= "0";}
			}
			function Init()
			{
			
			 	var Canctype=document.getElementById('cmbCANCELLATION_TYPE').value;
				populateXML();
				
				document.getElementById('hidEFFECTIVE_DATETIME').value = DateConvert(document.getElementById('hidEFFECTIVE_DATETIME').value,jsaAppDtFormat)
				
				DisplayBody();
				if(Canctype == '11969')	
        			document.getElementById("txtRETURN_PREMIUM").value = "0";
               // else
				 // document.getElementById("txtRETURN_PREMIUM").value = formatCurrency(document.getElementById("txtRETURN_PREMIUM").value);
				
				DisplayAgentPhoneNo()
				cmbADD_INT_Change();
				//chkSEND_ALL_Change();				
				SetAssignAddInt();
				//ShowHideAddIntCombos(true);
				
				//cmbCANCELLATION_TYPE_Change(true);
				setEffectiveDateEnableDisable();
				
				top.topframe.main1.mousein = false;
				
				findMouseIn(); 
				Check();
				ApplyColor();
				ChangeColor();
				doFocus();
				document.getElementById('btnCommitInProgress').style.display="none";
				//Added for Itrack Issue 6203 on 31 July 2009
				if (top.topframe.main1.menuXmlReady == false)
					setTimeout("top.topframe.enableMenus('1,7','ALL');",1000);
				else	
					top.topframe.enableMenus('1,7','ALL');
					
		       
            SetCancellationNoticButton();
			}
			function SetCancellationNoticButton()
			{
			    if(document.getElementById('cmbCANCELLATION_TYPE').value == '11969'){
			        if(document.getElementById('hidCANCELLATION_NOTICE_SENT').value == 'Y')
			            document.getElementById('btnGenerate_cancellation_notice_or_Memo').style.display = 'None'
			            else
			            document.getElementById('btnGenerate_cancellation_notice_or_Memo').style.display = 'inline'
			      }else
			       document.getElementById('btnGenerate_cancellation_notice_or_Memo').style.display = 'None'
			}
			function doFocus()
			{
			var CanType=document.getElementById('cmbCANCELLATION_TYPE');
			if (CanType!=null && (document.getElementById('trBody').style.display)=='inline')
			 CanType.focus();
			}
			
			function txtEFFECTIVE_HOUR_VALIDATE(source, arguments)//Function added by Charles on 7-Sep-09 for Itrack 6323
			{	
				var EFFECTIVE_HOUR= document.getElementById('txtEFFECTIVE_HOUR').value.replace(' ','');			
				if((EFFECTIVE_HOUR == '00' || EFFECTIVE_HOUR == '0') && document.getElementById('cmbMERIDIEM').options[1].selected == '1')
					{
						arguments.IsValid = false;
						return;					
					}
			}
			function disableHourValidator() //Function added by Charles on 7-Sep-09 for Itrack 6323
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
			function ReadOnly(Obj)
            {
                if(Obj.readOnly==true)
                   if(event.keyCode==8)
                        return false;
             }
    </script>

</head>
<body leftmargin="0" topmargin="0" scroll="yes" onload="Init();" >
    <form id="PROCESS_CANCELLATION" method="post" runat="server">
    <div>
        <webcontrol:menu id="bottomMenu" runat="server">
        </webcontrol:menu></div>
    <!-- To add bottom menu start -->
    <!-- To add bottom menu end -->
    <table cellspacing="0" cellpadding="0" width="90%" align="center" border="0">
        <tr>
            <td>
                <webcontrol:gridspacer id="grdSpacer" runat="server">
                </webcontrol:gridspacer>
            </td>
        </tr>
        <tr>
            <td>
                <webcontrol:policytop id="cltPolicyTop" runat="server">
                </webcontrol:policytop>
            </td>
        </tr>
        <tr>
            <td>
                <webcontrol:gridspacer id="Gridspacer1" runat="server">
                </webcontrol:gridspacer>
            </td>
        </tr>
        <tr>
            <td class="headereffectcenter">
                <asp:Label ID="capHeader" runat="server"></asp:Label>
            </td>
            <%--Cancellation Process--%>
        </tr>
        <tr>
        </tr>
        <tr>
            <td id="tdGridHolder">
                <webcontrol:gridspacer id="Gridspacer2" runat="server">
                </webcontrol:gridspacer><asp:PlaceHolder ID="GridHolder" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td class="pageHeader" colspan="4">
                <asp:Label ID="capLabel" runat="server"></asp:Label>
            </td>
            <%--Please note that all fields marked with * are mandatory--%>
        </tr>
        <tr>
            <td class="midcolorc" align="right" colspan="4">
                <asp:Label ID="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
            </td>
        </tr>
        <tr id="trBody">
            <td>
                <table width="100%" align="center" border="0">
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capCANCELLATION_TYPE" runat="server">Type</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbCANCELLATION_TYPE" onfocus="SelectComboIndex('cmbCANCELLATION_TYPE')"
                                runat="server">
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvCANCELLATION_TYPE" runat="server" Display="Dynamic"
                                ErrorMessage="CANCELLATION_TYPE can't be blank." ControlToValidate="cmbCANCELLATION_TYPE"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="csvCANCELLATION_TYPE" Display="Dynamic" ControlToValidate="cmbCANCELLATION_TYPE"
                                runat="server" ClientValidationFunction="validCANCELLATION_TYPE" ErrorMessage="Cancellation Type not eligible for this Bill Type"></asp:CustomValidator>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capCANCELLATION_OPTION" runat="server">Option</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbCANCELLATION_OPTION" onfocus="SelectComboIndex('cmbCANCELLATION_OPTION')"
                                runat="server">
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvCANCELLATION_OPTION" runat="server" Display="Dynamic"
                                ErrorMessage="CANCELLATION_OPTION can't be blank." ControlToValidate="cmbCANCELLATION_OPTION"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capEFFECTIVE_DATETIME" runat="server" Text="Effective Date of cancellation"></asp:Label><span
                                class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtEFFECTIVE_DATETIME" runat="server" MaxLength="11" size="12"></asp:TextBox><asp:HyperLink
                                ID="hlkEFFECTIVE_DATE" runat="server" CssClass="HotSpot">
                                <asp:Image ID="imgEFFECTIVE_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif">
                                </asp:Image>
                            </asp:HyperLink><br>
                            <asp:RequiredFieldValidator ID="rfvEFFECTIVE_DATE" runat="server" Display="Dynamic"
                                ErrorMessage="EFFECTIVE_DATETIME can't be blank." ControlToValidate="txtEFFECTIVE_DATETIME"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                    ID="revEFFECTIVE_DATE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                    ControlToValidate="txtEFFECTIVE_DATETIME"></asp:RegularExpressionValidator>
                            <asp:RangeValidator ID="rngEFFECTIVE_DATE" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_DATETIME"
                                Type="Date"></asp:RangeValidator>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capEFFECTIVE_TIME" runat="server"></asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtEFFECTIVE_HOUR" runat="server" MaxLength="2" size="3">12</asp:TextBox><asp:Label
                                ID="lblEFFECTIVE_HOUR" runat="server">HH</asp:Label><asp:TextBox ID="txtEFFECTIVE_MINUTE"
                                    runat="server" MaxLength="2" size="3">01</asp:TextBox><asp:Label ID="Label1" runat="server">MM</asp:Label><asp:DropDownList
                                        ID="cmbMERIDIEM" runat="server">
                                    </asp:DropDownList>
                            <!--Removed onfocus="SelectComboIndex('cmbMERIDIEM')" by Charles on 7-Sep-09 for Itrack 6323 -->
                            <br>
                            <asp:RequiredFieldValidator ID="rfvEFFECTIVE_HOUR" runat="server" Display="Dynamic"
                                ErrorMessage="Effective Time can't be blank.<br>" ControlToValidate="txtEFFECTIVE_HOUR"></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                    ID="rfvEFFECTIVE_MINUTE" runat="server" Display="Dynamic" ErrorMessage="Effective Time can't be blank.<br>"
                                    ControlToValidate="txtEFFECTIVE_MINUTE"></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                        ID="rfvMERIDIEM" runat="server" Display="Dynamic" ErrorMessage="cmbMERIDIEM can't be blank."
                                        ControlToValidate="cmbMERIDIEM"></asp:RequiredFieldValidator>
                            <!-- Range Validator -->
                            <asp:RangeValidator ID="rnvEFFECTIVE_HOUR" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_HOUR"
                                Enabled="True" Type="Integer" MaximumValue="12" MinimumValue="0" ErrorMessage="Hours must be from 0 to 12.<br>"></asp:RangeValidator><asp:RangeValidator
                                    ID="rnvtEFFECTIVE_MINUTE" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_MINUTE"
                                    Text="Minutes must be from 0 to 59.<br>" Type="Integer" MaximumValue="59" MinimumValue="0"></asp:RangeValidator><!-- customvalidator added by Charles on 7-Sep-09 for Itrack 6323 --><asp:CustomValidator
                                        ID="csvEFFECTIVE_HOUR" Display="Dynamic" ControlToValidate="txtEFFECTIVE_HOUR"
                                        runat="server" ClientValidationFunction="txtEFFECTIVE_HOUR_VALIDATE" ErrorMessage="HH 00 or 0 must be in AM"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capREASON" runat="server">Reason</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbREASON" onfocus="SelectComboIndex('cmbREASON')" runat="server"
                                onclick="Check();">
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvREASON" runat="server" Display="Dynamic" ErrorMessage="REASON can't be blank."
                                ControlToValidate="cmbREASON"></asp:RequiredFieldValidator>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capOTHER_REASON" runat="server">Reason</asp:Label><span class="mandatory"
                                id="spnOTHER_REASON">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtOTHER_REASON" runat="server" MaxLength="0" TextMode="MultiLine"
                                Columns="50" Rows="10"></asp:TextBox>
                            <%--<asp:label id="lblOTHER_REASON" runat="server" CssClass="LabelFont">-N.A.-</asp:label>--%>
                            <br>
                            <asp:CustomValidator ID="csvOTHER_REASON" Display="Dynamic" ControlToValidate="txtOTHER_REASON"
                                runat="server" ClientValidationFunction="txtCOMMENTS_VALIDATE"></asp:CustomValidator><asp:RequiredFieldValidator
                                    ID="rfvOTHER_REASON" runat="server" Display="Dynamic" ErrorMessage="OTHER_REASON can't be blank."
                                    ControlToValidate="txtOTHER_REASON"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capINCLUDE_REASON_DESC" runat="server">Include Reason Description on Cancellation/Forms</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:CheckBox ID="chkINCLUDE_REASON_DESC" runat="server"></asp:CheckBox>
                        </td>
                        <td class="midcolora" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capREQUESTED_BY" runat="server">By</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbREQUESTED_BY" onfocus="SelectComboIndex('cmbREQUESTED_BY')"
                                runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capAGENTPHONENO" runat="server">Agent Phone No</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:Label ID="lblAGENT_PHONE_NUMBER" runat="server" CssClass="labelfont"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capRETURN_PREMIUM" runat="server">Premium</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtRETURN_PREMIUM" onkeydown="javascript:return ReadOnly(this);" CssClass="INPUTCURRENCY" MaxLength="8" size="25"
                                runat="server"></asp:TextBox><br>
                            <asp:RegularExpressionValidator ID="revRETURN_PREMIUM" Display="Dynamic" ControlToValidate="txtRETURN_PREMIUM"
                                runat="server"></asp:RegularExpressionValidator>
                            <%--<asp:label id="lblRETURN_PREMIUM" CssClass="labelfont" Runat="server"></asp:label>--%>
                        </td>
                        <%--<TD class="midcolora" width="18%"><asp:label id="capPAST_DUE_PREMIUM" runat="server">Due</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:label id="lblPAST_DUE_PREMIUM" CssClass="labelfont" Runat="server"></asp:label></TD>--%>
                        <%--//ashish--%>
                        <td class='midcolora' width='25%'>
                            <asp:Label ID="capENDORSEMENT_TYPE" runat="server">Endorsment Type</asp:Label><span
                                class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:DropDownList ID='cmbENDORSEMENT_TYPE' OnFocus="SelectComboIndex('cmbENDORSEMENT_TYPE')"
                                runat='server'>
                                <asp:ListItem Value='0'></asp:ListItem>
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvENDORSEMENT_TYPE" runat="server" ControlToValidate="cmbENDORSEMENT_TYPE"
                                ErrorMessage="ENDORSEMENT_TYPE can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <%--//ashish--%>
                    </tr>
                    <%--<tr>
								<TD class="midcolora" width="18%"><asp:label id="capPRINT_COMMENTS" runat="server">Comments</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPRINT_COMMENTS" onfocus="SelectComboIndex('cmbPRINT_COMMENTS')" runat="server"></asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCOMMENTS" runat="server">Comments</asp:label><span class="mandatory" id="CommentsMandatory">*</span></TD>
								<TD class="midcolora"><asp:textbox id="txtCOMMENTS" runat="server" maxlength="0" TextMode="MultiLine" Columns="50"
										Rows="10"></asp:textbox><asp:label id="lblCOMM" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									<asp:requiredfieldvalidator id="rfvCOMMENTS" runat="server" Display="Dynamic" ControlToValidate="txtCOMMENTS"></asp:requiredfieldvalidator><asp:customvalidator id="csvCOMMENTS" Display="Dynamic" ControlToValidate="txtCOMMENTS" Runat="server"
										ClientValidationFunction="txtCOMMENTS_VALIDATE"></asp:customvalidator></TD>
							</tr>--%>
                    <tr>
                        <td class="headerEffectSystemParams" colspan="4">
                            <asp:Label ID="lblPrinting" runat="server">Printing Options Details</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capPRINTING_OPTIONS" runat="server">No printing Required at all</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:CheckBox ID="chkPRINTING_OPTIONS" runat="server"></asp:CheckBox>
                        </td>
                        <td class="midcolora" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capINSURED" runat="server">Insured</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbINSURED" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capAGENCY_PRINT" runat="server">Agency</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbAGENCY_PRINT" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="headerEffectSystemParams" colspan="4">
                            <asp:Label runat="server" ID="lblAdditional">Additional Interest Details</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capADD_INT" runat="server">Additional Interest</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbADD_INT" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capSEND_ALL" runat="server">Send to All</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:CheckBox ID="chkSEND_ALL" runat="server"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr id="trAddIntList">
                        <td class="midcolora" colspan="4">
                            <table>
                                <tr>
                                    <td class="midcolorc" align="center" width="37%">
                                        <asp:Label ID="capUnassignLossCodes" runat="server">All Additional Interests</asp:Label>
                                    </td>
                                    <td class="midcolorc" valign="middle" align="center" width="33%" rowspan="7">
                                        <br>
                                        <br>
                                        <input class="clsButton" id="btnAssignLossCodes" onclick="javascript:AssignAddInt(false);"
                                            type="button" value=">>" name="btnAssignLossCodes" runat="server"><br>
                                        <br>
                                        <input class="clsButton" id="btnUnAssignLossCodes" onclick="javascript:UnAssignAddInt(false);"
                                            type="button" value="<<" name="btnUnAssignLossCodes" runat="server">
                                    </td>
                                    <td class="midcolorc" align="center" width="33%">
                                        <asp:Label ID="capAssignedLossCodes" runat="server">Selected Additional Interests</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="midcolorc" align="center" width="33%" rowspan="6">
                                        <asp:ListBox ID="cmbUnAssignAddInt" runat="server" SelectionMode="Multiple" Height="150px"
                                            Width="300px"></asp:ListBox>
                                    </td>
                                    <td class="midcolorc" align="center" width="33%" rowspan="6">
                                        <asp:ListBox ID="cmbAssignAddInt" runat="server" SelectionMode="Multiple" Height="150px"
                                            Width="300px"></asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
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
                    <tr>
                        <td class="headerEffectSystemParams" colspan="4">
                            <asp:Label ID="capCancel" runat="server"></asp:Label>
                            <%--## Please note that Commit Process option will only be available after sending the
                            Notices--%>
                        </td>
                    </tr>
                    <!-- cms buttons  -->
                    <tr>
                        <td class="midcolora" align="right" colspan="2">
                            <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" Text="Reset"></cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnRollBack" runat="server" Text="RollBack"
                                CausesValidation="false"></cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnPolicyDetails" runat="server" Text="View Dec Page"
                                Visible="false"></cmsb:CmsButton>
                            <br>
                            <cmsb:CmsButton class="clsButton" ID="btnBack_To_Search" runat="server" Text="Back To Search"
                                CausesValidation="false"></cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnBack_To_Customer_Assistant" runat="server"
                                Text="Back To Customer Assistant" CausesValidation="false"></cmsb:CmsButton>
                        </td>
                        <td class="midcolorr" align="left" colspan="2">
                            <%--<cmsb:cmsbutton class="clsButton" id="btnCommit_To_Spool" runat="server" Text="Commit To Spool"></cmsb:cmsbutton>--%>
                            <cmsb:CmsButton class="clsButton" ID="btnSave" runat="server" Text="Save" CausesValidation="True">
                            </cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnGenerate_cancellation_notice_or_Memo" runat="server"
                                Text="Send Cancellation Notice" CausesValidation="True"></cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnSave_Send_Notice" runat="server" Text="Save And Send Notice"
                                CausesValidation="true" Style="display: none"></cmsb:CmsButton><br>
                            <cmsb:CmsButton class="clsButton" ID="btnComplete" runat="server" Text="Commit">
                            </cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnCommitInProgress" runat="server" Text="Commit in Progress">
                            </cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnCalculate_Return_premium" runat="server"
                                Style="display: none" Text="Calculate Return Premium"></cmsb:CmsButton>
                            <%--<cmsb:cmsbutton class="clsButton" id="btnPrint_Preview" runat="server" Text="Preview"></cmsb:cmsbutton>--%>
                            <%--<cmsb:cmsbutton class="clsButton" id="btnGenerate_premium_Notice" runat="server" Text="Premium Notice"></cmsb:cmsbutton></td>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
    <input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
    <input id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
    <input id="hidROW_ID" type="hidden" value="0" name="hidROW_ID" runat="server">
    <input id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
    <input id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID"
        runat="server">
    <input id="hidNEW_POLICY_VERSION_ID" type="hidden" value="0" name="hidNEW_POLICY_VERSION_ID"
        runat="server">
    <input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
    <input id="hidPROCESS_ID" type="hidden" value="0" name="hidPROCESS_ID" runat="server">
    <input id="Hidden1" type="hidden" value="0" name="hidROW_ID" runat="server">
    <input id="hidDisplayBody" type="hidden" value="0" name="hidDisplayBody" runat="server">
    <input id="hidADD_INT_ID" type="hidden" value="0" name="hidADD_INT_ID" runat="server">
    <input id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
    <input id="hidUNDERWRITER" type="hidden" value="0" name="hidUNDERWRITER" runat="server">
    <input id="hidSTATE_ID" type="hidden" value="0" name="hidSTATE_ID" runat="server">
    <input id="hidEFFECTIVE_DATE" type="hidden" value="0" name="hidEFFECTIVE_DATE" runat="server">
    <input id="hidEFFECTIVE_DATETIME" type="hidden" value="" name="hidEFFECTIVE_DATETIME"
        runat="server">
    <input id="hidBILL_TYPE" type="hidden" value="0" name="hidBILL_TYPE" runat="server">
    <input id="hidCANCEL_OPTIONS_XML" type="hidden" name="hidCANCEL_OPTIONS_XML" runat="server">
    <input id="hidPOLICY_PAYMENT_CODE" type="hidden" name="hidPOLICY_PAYMENT_CODE" runat="server">
    <input id="hidCANCEL_RULE_XML" type="hidden" name="hidCANCEL_RULE_XML" runat="server">
    <input id="hidCANCELLATION_OPTION" type="hidden" name="hidCANCELLATION_OPTION" runat="server">
    <input id="hidSTATE_CODE" type="hidden" name="hidSTATE_CODE" runat="server">
    <input id="hidAGENCY_CODE" type="hidden" name="hidAGENCY_CODE" runat="server">
    <input id="hidCancelEffDateXML" type="hidden" name="hidCancelEffDateXML" runat="server">
    <input id="hidPOL_INCEPTION_DATE" type="hidden" name="hidPOL_INCEPTION_DATE" runat="server">
    <input id="hidDateDifference" type="hidden" name="hidDateDifference" runat="server">
    <input id="hidEffDateForCompanyRequest" type="hidden" name="hidEffDateForCompanyRequest"
        runat="server">
    <input type="hidden" id="hidINSURED" runat="server">
    <input id="hidPOLICY_CURRENT_STATUS" type="hidden" name="hidPOLICY_CURRENT_STATUS"
        runat="server">
    <input id="hidPOLICY_PREVIOUS_STATUS" type="hidden" name="hidPOLICY_PREVIOUS_STATUS"
        runat="server">
    <input id="hidCANCELLATION_NOTICE_SENT" type="hidden" name="hidCANCELLATION_NOTICE_SENT"
        runat="server">
    <input id="hidREQUESTED_BY" type="hidden" name="hidREQUESTED_BY" runat="server">
    <input id="hidCANCELLATION_DATE_EQUITY" type="hidden" name="hidCANCELLATION_DATE_EQUITY"
        runat="server">
    <input id="hidGRACE_PERIOD" type="hidden" value="0" name="hidGRACE_PERIOD" runat="server">
    <input id="hidNOTICE_CLICK" type="hidden" value="0" name="hidNOTICE_CLICK" runat="server">
    <input id="hidAPP_EXPIRATION_DATE" type="hidden" value="0" name="hidAPP_EXPIRATION_DATE"
        runat="server">
    <input id="hidAPP_EFFECTIVE_DATE" type="hidden" value="0" name="hidAPP_EFFECTIVE_DATE"
        runat="server">
    <input id="hidCANCELLATION_OPTION_DB" type="hidden" name="hidCANCELLATION_OPTION_DB"
        runat="server">
    <input id="hidIS_CANCELLATION_OPTION_EXISTS" type="hidden" name="hidIS_CANCELLATION_OPTION_EXISTS"
        runat="server">
    <input id="hidRETURN_PREMIUM" type="hidden" name="hidRETURN_PREMIUM" value="0" runat="server">
    <input id="hidEFF_DAT" type="hidden" name="hidEFF_DAT" value="" runat="server">
    </form>
</body>
</html>

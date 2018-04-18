<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="CmsTimer" Src="/cms/cmsweb/webcontrols/CmsTimePicker.ascx" %>
<%@ Page language="c#" Codebehind="AddReinsuranceInfo.aspx.cs" AutoEventWireup="false" validateRequest="false" Inherits="Cms.CmsWeb.Maintenance.ReinsuranceInfo" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>MNT_REINSURANCE_CONTRACT</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript" src="/cms/cmsweb/Scripts/Calendar.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
		<script language="javascript">
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
        function AddData()
        {
	        ChangeColor();
	        DisableValidators();
	        document.getElementById('hidCONTRACT_ID').value	=	'New';
	        document.getElementById('cmbCONTRACT_TYPE').focus();
        //	document.getElementById('txtCONTRACT_NUMBER').value  = '';
        //	document.getElementById('cmbCONTRACT_NAME_ID').options.selectedIndex = -1;
        //	document.getElementById('cmbCONTRACT_TYPE').options.selectedIndex = -1;
        //	document.getElementById('cmbCONTRACT_LOB').options.selectedIndex = -1;
        //	document.getElementById('txtCONTRACT_DESC').value  = '';
        //	document.getElementById('cmbBROKERID').options.selectedIndex = -1;
        //	document.getElementById('cmbSTATE_ID').options.selectedIndex = -1;
        //	document.getElementById('txtREINSURER_REFERENCE_NUM').value  = '';
	        //document.getElementById('txtUW_YEAR').value  = '';
        //	document.getElementById('txtASLOB').value  = '';
	        //document.getElementById('cmbSUBLINE_CODE').options.selectedIndex = -1;
	        //document.getElementById('txtCOVERAGE_CODE').value  = '';
	        //document.getElementById('txtCESSION').value  = '';
	        //document.getElementById('txtEFFECTIVE_DATE').value  = '';
	        //document.getElementById('txtEXPIRATION_DATE').value  = '';
	        //document.getElementById('cmbCALCULATION_BASE').options.selectedIndex = -1;
	        //document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
        }

//function SetTab()
//{ 
//	
//	if((document.getElementById('hidFormSaved').value == '1') || (document.getElementById("hidOldData").value != "" && document.getElementById("hidOldData").value != "0"))
//	{	
//	    var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
//         var tabtitles = TAB_TITLES.split(',');
//         
//		/*
//		var Contract_Type = document.getElementById('cmbCONTRACT_TYPE').value;
//		if(Contract_Type == "1" || Contract_Type == "2")//Contract Type = 'XS'
//		{
//			
//			Url="AttachmentIndex.aspx?calledfrom=REINSURANCE&EntityType=REINSURANCE&EntityId="+document.getElementById('hidCONTRACT_ID').value + "&";
//			DrawTab(2,top.frames[1],'Attachment',Url);
//				
//			Url="ReinsuranceExcessLayerIndex.aspx?ContractID="+ document.getElementById('hidCONTRACT_ID').value + "&" ;
//			DrawTab(4,top.frames[1],'Excess Layer',Url);
//		
//			Url="AddReinsuranceAggStopLoss.aspx?";
//			DrawTab(5,top.frames[1],'Aggregate Stop Loss',Url);			
//			
//			//Url="ReinsuranceMinorParticipationIndex.aspx?";
//			//DrawTab(7,top.frames[1],'Minor Participation',Url);
//			
//			Url="ReinsuranceMajorParticipationIndex.aspx?";
//			DrawTab(6,top.frames[1],'Major Participation',Url);
//			
//			Url="Reinsurance/LossLayer.aspx?";
//			DrawTab(9,top.frames[1],'Loss Layer',Url);
//			
//			
//			
//		
//			RemoveTab(8,top.frames[1]);
//			RemoveTab(7,top.frames[1]);
//			RemoveTab(3,top.frames[1]);
//		}
//		
//		else if(Contract_Type == "3" || Contract_Type == "4")//Contract Type = 'QS'
//		{
//			Url="AttachmentIndex.aspx?calledfrom=REINSURANCE&EntityType=REINSURANCE&EntityId="+document.getElementById('hidCONTRACT_ID').value + "&";
//			DrawTab(2,top.frames[1],'Attachment',Url);
//			
//			Url="ReinsuranceQuotaShare.aspx?"
//			DrawTab(3,top.frames[1],'Pro-Rata/Quota Share',Url);
//			
//			Url="ReinsuranceMajorParticipationIndex.aspx?";
//			DrawTab(6,top.frames[1],'Major Participation',Url);
//			
//			Url="ReinsuranceMinorParticipationIndex.aspx?";
//			DrawTab(7,top.frames[1],'Minor Participation',Url);
//			
//			Url="AddReinsurancePosting.aspx?";
//			DrawTab(8,top.frames[1],'Reinsurance Posting',Url);		
//				
//			Url="Reinsurance/LossLayer.aspx?";
//			DrawTab(9,top.frames[1],'Loss Layer',Url);
//			
//			
//		
//			RemoveTab(7,top.frames[1]);
//			RemoveTab(5,top.frames[1]);
//			RemoveTab(4,top.frames[1]);
//			
//		}	*/
//		//added by Pravesh on 14 Aug as Show all Tab till next update discussed with Rajan Sir.
//		    Url="AttachmentIndex.aspx?calledfrom=REINSURANCE&EntityType=REINSURANCE&EntityId="+document.getElementById('hidCONTRACT_ID').value + "&";
//			DrawTab(2,top.frames[1],tabtitles[0],Url);
//			
//			//Url="ReinsuranceQuotaShare.aspx?"
//			//DrawTab(3,top.frames[1],'Pro-Rata/Quota Share',Url);
//				
//			//Url="ReinsuranceExcessLayerIndex.aspx?ContractID="+ document.getElementById('hidCONTRACT_ID').value + "&" ;
//			//DrawTab(4,top.frames[1],'Excess Layer',Url);
//		
//			//Url="AddReinsuranceAggStopLoss.aspx?";
//			//DrawTab(5,top.frames[1],'Aggregate Stop Loss',Url);			
//		
//			Url="ReinsuranceMajorParticipationIndex.aspx?";
//			DrawTab(3, top.frames[1], tabtitles[1], Url);
//			
//			Url="ReinsuranceMinorParticipationIndex.aspx?";
//			DrawTab(4, top.frames[1], tabtitles[2], Url);
//			
//			//Url="AddReinsurancePosting.aspx?";
//			//DrawTab(8,top.frames[1],'Reinsurance Posting',Url);	
//			
//			Url="Reinsurance/ReinsurancePremiumBuilderIndex.aspx?";
//			DrawTab(5, top.frames[1], tabtitles[3], Url);	
//			
//			Url="Reinsurance/LossLayerIndex.aspx?";
//			DrawTab(6,top.frames[1],tabtitles[4], Url);
//			
//			RemoveTab(9,top.frames[1]);
//			RemoveTab(8,top.frames[1]);
//			RemoveTab(7,top.frames[1]);
//	}
//	else
//	{		
//		RemoveTab(9,top.frames[1]);
//		RemoveTab(8,top.frames[1]);
//		RemoveTab(7,top.frames[1]);
//		RemoveTab(6,top.frames[1]);
//		RemoveTab(5,top.frames[1]);
//		RemoveTab(4,top.frames[1]);					
//		RemoveTab(3,top.frames[1]);
//		RemoveTab(2,top.frames[1]);
//	}
//}

        function SetTab() {
         
            if ((document.getElementById('hidFormSaved').value == '1') || (document.getElementById("hidOldData").value != "" && document.getElementById("hidOldData").value != "0")) {
                var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
                var tabtitles = TAB_TITLES.split(',');
              
                RemoveTab(5, top.frames[1]);
                RemoveTab(4, top.frames[1]);
                RemoveTab(3, top.frames[1]);
                RemoveTab(2, top.frames[1]);
                
                // CONTRACT TYPE IS XOL
                if (document.getElementById('hidIS_XOL_TYPE').value == '3' || document.getElementById('hidIS_XOL_TYPE').value == '4')
                 {

                    
                    Url = "Reinsurance/LossLayerIndex.aspx?";
                    DrawTab(2, top.frames[1], tabtitles[0], Url);



                    Url = "ReinsuranceMajorParticipationIndex.aspx?";
                    DrawTab(3, top.frames[1], tabtitles[1], Url);



                    Url = "XOLIndex.aspx?CONTRACT=" + document.getElementById('hidCONTRACT_ID').value + "+&";
                    DrawTab(4, top.frames[1], tabtitles[3], Url);

                    Url = "AttachmentIndex.aspx?calledfrom=REINSURANCE&EntityType=REINSURANCE&EntityId=" + document.getElementById('hidCONTRACT_ID').value + "&";
                    DrawTab(5, top.frames[1], tabtitles[2], Url);
                }
                else {

                    Url = "Reinsurance/LossLayerIndex.aspx?";
                    DrawTab(2, top.frames[1], tabtitles[0], Url);



                    Url = "ReinsuranceMajorParticipationIndex.aspx?";
                    DrawTab(3, top.frames[1], tabtitles[1], Url);

                    Url = "AttachmentIndex.aspx?calledfrom=REINSURANCE&EntityType=REINSURANCE&EntityId=" + document.getElementById('hidCONTRACT_ID').value + "&";
                    DrawTab(4, top.frames[1], tabtitles[2], Url);
                
                }
                
               

               
            }
            else {
                RemoveTab(9, top.frames[1]);
                RemoveTab(8, top.frames[1]);
                RemoveTab(7, top.frames[1]);
                RemoveTab(6, top.frames[1]);
                RemoveTab(5, top.frames[1]);
                RemoveTab(4, top.frames[1]);
                RemoveTab(3, top.frames[1]);
                RemoveTab(2, top.frames[1]);
            }
        }

        function populateXML()
        { 
	        if(document.getElementById('hidFormSaved').value == '0')
	        {
        		
		        var tempXML;
		        if(top.frames[1].strXML!="")
			        {
				        //document.getElementById('btnReset').style.display='none';
				        tempXML=top.frames[1].strXML;
				        oldRisk=document.getElementById("hidRISK_EXPOSURE").value;
				        //Enabling the activate deactivate button
				        //document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
				        //Storing the XML in hidRowId hidden fields 
				        //document.getElementById('hidOldData').value		=	 tempXML;
				        populateFormData(tempXML,MNT_REINSURANCE_CONTRACT);
				        document.getElementById("hidRISK_EXPOSURE").value = oldRisk;
				        TriggerOnBlurFunction() 
			        }
		        else
			        {
				        AddData();
			        }
	        }
	        return false;
        }
        function TriggerOnBlurFunction() {

            $('#txtPER_OCCURRENCE_LIMIT').blur();
            $('#txtANNUAL_AGGREGATE').blur();
            $('#txtDEPOSIT_PREMIUMS').blur();
            $('#txtMINIMUM_PREMIUM').blur();
            $('#txtCASH_CALL_LIMIT').blur();
            $('#txtCOMMISSION').blur();
            
            return false;
        }

	function CompareExpDateWithEffDate(objSource , objArgs)
	{
		var effdate=document.MNT_REINSURANCE_CONTRACT.txtEFFECTIVE_DATE.value;
		var expdate=document.MNT_REINSURANCE_CONTRACT.txtEXPIRATION_DATE.value;
		if ( document.MNT_REINSURANCE_CONTRACT.txtEFFECTIVE_DATE!=null && document.MNT_REINSURANCE_CONTRACT.txtEXPIRATION_DATE !=null && expdate !=""  && effdate != "")
		{
			objArgs.IsValid = CompareTwoDate(expdate,effdate,jsaAppDtFormat);
		}
	}	

	function Reset()
	{
		DisableValidators();
		document.MNT_REINSURANCE_CONTRACT.reset();
		ChangeColor();
		return false;
	}	
	
	function CompareEffDateWithExpDate(objSource , objArgs)
	{
		var effdate=document.MNT_REINSURANCE_CONTRACT.txtEFFECTIVE_DATE.value;
		var expdate=document.MNT_REINSURANCE_CONTRACT.txtEXPIRATION_DATE.value;
		if ( document.MNT_REINSURANCE_CONTRACT.txtEFFECTIVE_DATE!=null && document.MNT_REINSURANCE_CONTRACT.txtEXPIRATION_DATE !=null && expdate !=""  && effdate != "")
		{
			objArgs.IsValid = CompareTwoDate(expdate,effdate,jsaAppDtFormat);
		}
	}	
	function CompareTwoDate(DateFirst, DateSec, FormatOfComparision)
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
			if(firstSpan > secSpan) 
				return true;	// first is greater
			else 
				return false;	// secound is greater
		}	
		
		function Validate(objSource , objArgs)
		{	
			var comm = parseFloat(document.getElementById('txtCESSION').value);
			if(comm < 0 || comm > 100)
			{
				document.getElementById('txtCESSION').select();
				objArgs.IsValid=false;
			}
			else
				objArgs.IsValid=true;
		}
		
			function RSKAssignLossCodes()
			{					
				//alert("RSKAssignLossCodes()");
				var coll = document.MNT_REINSURANCE_CONTRACT.cmbRISK_EXPOSURE;
				var selIndex = coll.options.selectedIndex;				
				var len = coll.options.length;
				var num=0;				
				for (i = len- 1; i > -1 ; i--)
				{
					if((coll.options(i).selected == true) && (coll.options(i).value > 0))
					{
						num = i;
						var szSelectedDept = coll.options(i).value;
						var innerText = coll.options(i).text;
						document.MNT_REINSURANCE_CONTRACT.cmbRSKAssignLossCodes.options[document.MNT_REINSURANCE_CONTRACT.cmbRSKAssignLossCodes.length] = new Option(innerText,szSelectedDept)
						coll.remove(i);										
						document.getElementById("btnSave").style.display="inline";			
						//document.getElementById("btnReset").style.display="inline";			
					}										
				}	
				len = coll.options.length;			
				funcValidateRiskExposer(coll,coll);		
				if(	num < len )
				{
					document.MNT_REINSURANCE_CONTRACT.cmbRISK_EXPOSURE.options(num).selected = true;
				}	
				else
				{
					if(num>0)
						document.MNT_REINSURANCE_CONTRACT.cmbRISK_EXPOSURE.options(num - 1).selected = true;
				}					
				
			}
			function LOBAssignLossCodes()
			{					
				//alert("LOBAssignLossCodes()");
				var coll = document.MNT_REINSURANCE_CONTRACT.cmbCONTRACT_LOB;
				var selIndex = coll.options.selectedIndex;				
				var len = coll.options.length;
				var num=0;				
				for (i = len- 1; i > -1 ; i--)
				{
					if((coll.options(i).selected == true) && (coll.options(i).value > 0))
					{
						num = i;
						var szSelectedDept = coll.options(i).value;
						var innerText = coll.options(i).text;
						document.MNT_REINSURANCE_CONTRACT.cmbLOBAssignLossCodes.options[document.MNT_REINSURANCE_CONTRACT.cmbLOBAssignLossCodes.length] = new Option(innerText,szSelectedDept)
						coll.remove(i);										
						document.getElementById("btnSave").style.display="inline";			
						//document.getElementById("btnReset").style.display="inline";			
					}										
				}	
				len = coll.options.length;			
				funcValidateLOBs(coll , coll);	
				if(	num < len )
				{
					document.MNT_REINSURANCE_CONTRACT.cmbCONTRACT_LOB.options(num).selected = true;
				}	
				else
				{
					if(num>0)
						document.MNT_REINSURANCE_CONTRACT.cmbCONTRACT_LOB.options(num - 1).selected = true;
				}					
				
			}

			function STATEAssignLossCodes()
			{					
				//alert("STATEAssignLossCodes()");
				var coll = document.MNT_REINSURANCE_CONTRACT.cmbSTATE_ID;
				var selIndex = coll.options.selectedIndex;				
				var len = coll.options.length;
				var num=0;				
				for (i = len- 1; i > -1 ; i--)
				{
					if((coll.options(i).selected == true) && (coll.options(i).value > 0))
					{
						num = i;
						var szSelectedDept = coll.options(i).value;
						var innerText = coll.options(i).text;
						document.MNT_REINSURANCE_CONTRACT.cmbSTATEAssignLossCodes.options[document.MNT_REINSURANCE_CONTRACT.cmbSTATEAssignLossCodes.length] = new Option(innerText,szSelectedDept)
						coll.remove(i);										
						document.getElementById("btnSave").style.display="inline";			
						//document.getElementById("btnReset").style.display="inline";			
					}										
				}	
				len = coll.options.length;			
				funcValidateStates(coll,coll);
				if(	num < len )
				{
					document.MNT_REINSURANCE_CONTRACT.cmbSTATE_ID.options(num).selected = true;
				}	
				else
				{
					if(num>0)
						document.MNT_REINSURANCE_CONTRACT.cmbSTATE_ID.options(num - 1).selected = true;
				}					
				
			}
			function SetRiskExposer()
			{
				if(document.getElementById("hidRISK_EXPOSURE").value=='' || document.getElementById("hidRISK_EXPOSURE").value=='0')
					return;
				var selectedRiskExposer = new String(document.getElementById("hidRISK_EXPOSURE").value);
				var selectedRiskExposerArr = selectedRiskExposer.split(',');
				if(selectedRiskExposerArr==null || selectedRiskExposerArr.length<1)
					return;				
				
				var coll = document.getElementById('cmbRISK_EXPOSURE');
				var selIndex = coll.options.selectedIndex;
				var len = coll.options.length;
				var arrLen = selectedRiskExposerArr.length;
				if(len<1) return;				
				var num=0;				
				for(var j=0;j<arrLen;j++)
				{
					for (var i = len- 1; i > -1 ; i--)
					{
						if(coll.options(i).value == selectedRiskExposerArr[j])
						{
							num = i;
							var szSelectedDept = coll.options(i).value;
							var innerText = coll.options(i).text;
							//document.getElementById('cmbAssignAddInt').options[document.getElementById('cmbAssignAddInt').length] = new Option(innerText,szSelectedDept)
							coll.remove(i);																	
							len = coll.options.length;
						}										
					}
				}
			}
			function SetLOBs()
			{
				if(document.getElementById("hidCONTRACT_LOB").value=='' || document.getElementById("hidCONTRACT_LOB").value=='0')
					return;
				var selectedLobs = new String(document.getElementById("hidCONTRACT_LOB").value);
				var selectedLobsArr = selectedLobs.split(',');
				if(selectedLobsArr==null || selectedLobsArr.length<1)
					return;				
				
				var coll = document.getElementById('cmbCONTRACT_LOB');
				var selIndex = coll.options.selectedIndex;
				var len = coll.options.length;
				var arrLen = selectedLobsArr.length;
				if(len<1) return;				
				var num=0;				
				for(var j=0;j<arrLen;j++)
				{
					for (var i = len- 1; i > -1 ; i--)
					{
						if(coll.options(i).value == selectedLobsArr[j])
						{
							num = i;
							var szSelectedDept = coll.options(i).value;
							var innerText = coll.options(i).text;
							//document.getElementById('cmbAssignAddInt').options[document.getElementById('cmbAssignAddInt').length] = new Option(innerText,szSelectedDept)
							coll.remove(i);																	
							len = coll.options.length;
						}										
					}
				}
			}
			function SetStates()
			{
				if(document.getElementById("hidSTATE_ID").value=='' || document.getElementById("hidSTATE_ID").value=='0')
					return;
				var selectedStates = new String(document.getElementById("hidSTATE_ID").value);
				var selectedStatesArr = selectedStates.split(',');
				if(selectedStatesArr==null || selectedStatesArr.length<1)
					return;				
				
				var coll = document.getElementById('cmbSTATE_ID');
				var selIndex = coll.options.selectedIndex;
				var len = coll.options.length;
				var arrLen = selectedStatesArr.length;
				if(len<1) return;				
				var num=0;				
				for(var j=0;j<arrLen;j++)
				{
					for (var i = len- 1; i > -1 ; i--)
					{
						if(coll.options(i).value == selectedStatesArr[j])
						{
							num = i;
							var szSelectedDept = coll.options(i).value;
							var innerText = coll.options(i).text;
							//document.getElementById('cmbAssignAddInt').options[document.getElementById('cmbAssignAddInt').length] = new Option(innerText,szSelectedDept)
							coll.remove(i);																	
							len = coll.options.length;
						}										
					}
				}
			}
			function RSKUnAssignLossCodes()
			{	//alert("RSKUnAssignLossCodes()");			
				var UnassignableString = ""
				var Unassignable = UnassignableString.split(",")
				var gszAssignedString = ""				
				var Assigned = gszAssignedString.split(",")
				var coll = document.MNT_REINSURANCE_CONTRACT.cmbRSKAssignLossCodes;
				var selIndex = coll.options.selectedIndex;
				var len = coll.options.length;		
				var num=0;						
				if(len==0) return;
				for (i = len- 1; i > -1 ; i--)
				{
					if((coll.options(i).selected == true) && (coll.options(i).value > 0))
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
								document.MNT_REINSURANCE_CONTRACT.cmbRISK_EXPOSURE.options[document.MNT_REINSURANCE_CONTRACT.cmbRISK_EXPOSURE.length] = new Option(innerText,szSelectedDept)					
								coll.remove(i);
															
							}
							else
							{
								alert("Cannot unassign Department "+innerText+" because some divisions are associasted with it");
							}
					}	
					
				}
				var len = coll.options.length;
				funcValidateRiskExposer(coll,coll);		
				if(len<1) return;
				if(	num < len )
				{
					document.MNT_REINSURANCE_CONTRACT.cmbRSKAssignLossCodes.options(num).selected = true;
				}	
				else
				{
					document.MNT_REINSURANCE_CONTRACT.cmbRSKAssignLossCodes.options(num - 1).selected = true;
				}
				
			}
			function LOBUnAssignLossCodes()
			{	//alert("LOBUnAssignLossCodes()");			
				var UnassignableString = ""
				var Unassignable = UnassignableString.split(",")
				var gszAssignedString = ""				
				var Assigned = gszAssignedString.split(",")
				var coll = document.MNT_REINSURANCE_CONTRACT.cmbLOBAssignLossCodes;
				var selIndex = coll.options.selectedIndex;
				var len = coll.options.length;		
				var num=0;						
				if(len==0) return;
				for (i = len- 1; i > -1 ; i--)
				{
					if((coll.options(i).selected == true) && (coll.options(i).value > 0))
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
								document.MNT_REINSURANCE_CONTRACT.cmbCONTRACT_LOB.options[document.MNT_REINSURANCE_CONTRACT.cmbCONTRACT_LOB.length] = new Option(innerText,szSelectedDept)					
								coll.remove(i);
															
							}
							else
							{
								alert("Cannot unassign Department "+innerText+" because some divisions are associasted with it");
							}
					}			
				}
				var len = coll.options.length;
				funcValidateLOBs(coll , coll);
				if(len<1) return;
				if(	num < len )
				{
					document.MNT_REINSURANCE_CONTRACT.cmbLOBAssignLossCodes.options(num).selected = true;
				}	
				else
				{
					document.MNT_REINSURANCE_CONTRACT.cmbLOBAssignLossCodes.options(num - 1).selected = true;
				}
				
			}
			function STATEUnAssignLossCodes()
			{	//alert("STATEUnAssignLossCodes()");			
				var UnassignableString = ""
				var Unassignable = UnassignableString.split(",")
				var gszAssignedString = ""				
				var Assigned = gszAssignedString.split(",")
				var coll = document.MNT_REINSURANCE_CONTRACT.cmbSTATEAssignLossCodes;
				var selIndex = coll.options.selectedIndex;
				var len = coll.options.length;		
				var num=0;						
				if(len==0) return;
				for (i = len- 1; i > -1 ; i--)
				{
					if((coll.options(i).selected == true) && (coll.options(i).value > 0))
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
								document.MNT_REINSURANCE_CONTRACT.cmbSTATE_ID.options[document.MNT_REINSURANCE_CONTRACT.cmbSTATE_ID.length] = new Option(innerText,szSelectedDept)					
								coll.remove(i);
															
							}
							else
							{
								alert("Cannot unassign Department "+innerText+" because some divisions are associasted with it");
							}
					}			
				}
				var len = coll.options.length;
				funcValidateStates(coll,coll);
				if(len<1) return;
				if(	num < len )
				{
					document.MNT_REINSURANCE_CONTRACT.cmbSTATEAssignLossCodes.options(num).selected = true;
				}	
				else
				{
					document.MNT_REINSURANCE_CONTRACT.cmbSTATEAssignLossCodes.options(num - 1).selected = true;
				}
			}
			
			function RSKCountAssignLossCodes()
			{
				
				document.getElementById("hidRISK_EXPOSURE").value = "";
				var coll = document.MNT_REINSURANCE_CONTRACT.cmbRSKAssignLossCodes;
				var len = coll.options.length;
				for( k = 0;k < len ; k++)
				{
					var szSelectedDept = coll.options(k).value;
					if (document.getElementById("hidRISK_EXPOSURE").value == "")
					{
						document.getElementById("hidRISK_EXPOSURE").value =  szSelectedDept;
					}
					else
					{
						document.MNT_REINSURANCE_CONTRACT.hidRISK_EXPOSURE.value = document.MNT_REINSURANCE_CONTRACT.hidRISK_EXPOSURE.value + "," + szSelectedDept;
					}
				}	
				//Page_ClientValidate();
				funcValidateRiskExposer(coll,coll);
				//return Page_IsValid && returnVal;
		//			document.CLM_LOSS_CODES.TextBox1.style.display = 'none';		
			}	
		function LOBCountAssignLossCodes()
			{
				document.getElementById("hidCONTRACT_LOB").value = "";
				var coll = document.MNT_REINSURANCE_CONTRACT.cmbLOBAssignLossCodes;
				var len = coll.options.length;
				for( k = 0;k < len ; k++)
				{
					var szSelectedDept = coll.options(k).value;
					if (document.getElementById("hidCONTRACT_LOB").value == "")
					{
						document.getElementById("hidCONTRACT_LOB").value =  szSelectedDept;
					}
					else
					{
						document.MNT_REINSURANCE_CONTRACT.hidCONTRACT_LOB.value = document.MNT_REINSURANCE_CONTRACT.hidCONTRACT_LOB.value + "," + szSelectedDept;
					}
				}	
				//Page_ClientValidate();
				//var returnVal = funcValidateLOBs();
				//return Page_IsValid && returnVal;		
				funcValidateLOBs(coll , coll);	
				//Page_ClientValidate();		
	//			document.CLM_LOSS_CODES.TextBox1.style.display = 'none';		
			}
			function STATECountAssignLossCodes() {
			   
				document.getElementById("hidSTATE_ID").value = "";
				var coll = document.MNT_REINSURANCE_CONTRACT.cmbSTATEAssignLossCodes;
				var len = coll.options.length;
				for( k = 0;k < len ; k++)
				{
					var szSelectedDept = coll.options(k).value;
					if (document.getElementById("hidSTATE_ID").value == "")
					{
					
						document.getElementById("hidSTATE_ID").value =  szSelectedDept;
					}
					else
					{
						document.MNT_REINSURANCE_CONTRACT.hidSTATE_ID.value = document.MNT_REINSURANCE_CONTRACT.hidSTATE_ID.value + "," + szSelectedDept;
					}
				}	
			funcValidateStates(coll,coll);
	//			document.CLM_LOSS_CODES.TextBox1.style.display = 'none';		
			}	
	function Commision()
	{
	    //Check for Commission Applicable
	    if(document.getElementById('cmbCOMMISSION_APPLICABLE').value == 1)
		{
		document.getElementById('trREINSURANCE_COMMISSION').style.display = "inline";  
			document.getElementById('trRein_Commision').style.display = "inline";                  
		//document.getElementById("rfvREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT").setAttribute("enabled",true);
		//document.getElementById("rfvREINSURANCE_COMMISSION_ACCOUNT").setAttribute("enabled",true);
		 
		}
		else
		{
		//document.getElementById("pnlCommision_Applicable").style.display = "none";  
		//document.getElementById("rfvREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT").setAttribute("enabled",false);
		//document.getElementById("rfvREINSURANCE_COMMISSION_ACCOUNT").setAttribute("enabled",false);
		       
		document.getElementById('trREINSURANCE_COMMISSION').style.display = "none"; 
		document.getElementById('trRein_Commision').style.display = "none"; 
		                  
	 	} 
	}	
	
	function funcValidateRiskExposer(objSource , objArgs)
	{
			//alert(document.getElementById("hidRISK_EXPOSURE").value);
			if (document.getElementById("hidRISK_EXPOSURE").value=="")
			{
			document.getElementById('rfvRSKAssignLossCodes').setAttribute("enabled",true);
			document.getElementById('rfvRSKAssignLossCodes').setAttribute("isValid",false);
			document.getElementById("rfvRSKAssignLossCodes").style.display="inline";
			}
			else
			{
			//EnableValidator('rfvRSKAssignLossCodes',true);
			document.getElementById('rfvRSKAssignLossCodes').setAttribute("enabled",false);
			document.getElementById('rfvRSKAssignLossCodes').setAttribute("isValid",true);
			document.getElementById("rfvRSKAssignLossCodes").style.display="none";
			
			//return true;
			}
			return;
	}	
	function funcValidateLOBs(objSource , objArgs)
	{
			//alert(document.getElementById("hidCONTRACT_LOB").value);
			if (document.getElementById("hidCONTRACT_LOB").value =="")
			{
			
			document.getElementById("rfvLOBAssignLossCodes").style.display="inline";
			document.getElementById('rfvLOBAssignLossCodes').setAttribute("enabled",true);
			document.getElementById('rfvLOBAssignLossCodes').setAttribute("isValid",false);
			
			//return false;
			}
			else
			{
			document.getElementById("rfvLOBAssignLossCodes").style.display="none";
			document.getElementById('rfvLOBAssignLossCodes').setAttribute("enabled",false);
			document.getElementById('rfvLOBAssignLossCodes').setAttribute("isValid",true);
			//return true;
			}
			return;
			
			//alert('in csv lob');
			if(document.getElementById('cmbLOBAssignLossCodes').options.length == 0)
			{
				document.getElementById('cmbLOBAssignLossCodes').className = "MandatoryControl";
				document.getElementById("cmbLOBAssignLossCodes").style.display="inline";
				document.getElementById("csvCONTRACT_LOB").style.display="inline";
				document.getElementById("csvCONTRACT_LOB").innerText = "Please select Lobs";
				document.getElementById("csvCONTRACT_LOB").setAttribute("isValid",false);
				document.getElementById("csvCONTRACT_LOB").setAttribute("enabled",true);
				Page_IsValid=false;
				//objArgs.IsValid =false;
				
				//return false;
			}
			else
			{
				document.getElementById('cmbLOBAssignLossCodes').className = "none";
				//document.getElementById("cmbCATEGORY").style.display="none";
				return true;
			}
	}
	function funcValidateStates(objSource , objArgs)
	{
			//alert(document.getElementById("hidSTATE_ID").value);
			if (document.getElementById("hidSTATE_ID").value =="")
			{
			document.getElementById("rfvSTATEAssignLossCodes").style.display="inline";
			document.getElementById('rfvSTATEAssignLossCodes').setAttribute("enabled",true);
			document.getElementById('rfvSTATEAssignLossCodes').setAttribute("isValid",false);
			//return false;
			}
			else
			{
			document.getElementById("rfvSTATEAssignLossCodes").style.display="none";
			document.getElementById('rfvSTATEAssignLossCodes').setAttribute("enabled",false);
			document.getElementById('rfvSTATEAssignLossCodes').setAttribute("isValid",true);
			//return true;
			}
			return;
			
			objArgs.IsValid = true;
	  		if(document.getElementById('cmbSTATEAssignLossCodes').options.length == 0)
			{
				document.getElementById('cmbSTATEAssignLossCodes').className = "MandatoryControl";
				document.getElementById("cmbSTATEAssignLossCodes").style.display="inline";
				document.getElementById("csvSTATEAssignLossCodes").style.display="inline";
				document.getElementById("csvSTATEAssignLossCodes").innerText = "Please select Risk Exposer";
				document.getElementById("csvSTATEAssignLossCodes").setAttribute("IsValid",false);
				Page_IsValid=false;
				objArgs.IsValid =false;
				//alert(objArgs.IsValid);
				return false;
			}
			else
			{
				document.getElementById('cmbSTATEAssignLossCodes').className = "none";
				//document.getElementById("cmbCATEGORY").style.display="none";
				//alert(objArgs.IsValid);
				return true;
			}
	}
		function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbLOSS_ADJUSTMENT_EXPENSE":
						lookupMessage	=	"REA.";
						break;
					case "cmbCALCULATION_BASE":
						lookupMessage	=	"CALC.";
						break;
					case "cmbDEPOSIT_PREMIUM_PAYABLE":
						lookupMessage	=	"RDP.";
						break;	
					default:
						lookupMessage	=	"Look up code not found";
						break;
						
				}
				showLookupLayer(controlId,lookupMessage);							
			}
			//For Adjusting Date
			function adjustFollowupDate(objSource , objArgs)
			{
				if(document.getElementById('txtEXPIRATION_DATE').value != "")
				{
					trandate = document.getElementById('txtEXPIRATION_DATE').value;
					myDate = FormatDateForGrid(document.getElementById('txtEXPIRATION_DATE'),trandate);
					var datevar = myDate.split("/");
					newDate = new Date(myDate);
					var days_to_add = 60;
					newmDate = new Date(newDate.getTime() - days_to_add*24*60*60*1000);
					newmDate = (newmDate.getMonth()+1) + '/' + newmDate.getDate() + '/' + newmDate.getYear();
					document.getElementById('txtFOLLOW_UP_FIELDS').value = newmDate;
				}
				else
				{
					document.getElementById('txtFOLLOW_UP_FIELDS').value = "";
				}
				
			}
		//Formats the amount and convert 111 into 1.11
		function FormatAmount(txtReceiptAmount)
		{
						
			if (txtReceiptAmount.value != "")
			{
				amt = txtReceiptAmount.value;
				
				amt = ReplaceAll(amt,".","");
				
				if (amt.length == 1)
					amt = amt + "0";
				
				if ( ! isNaN(amt))
				{
					
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					txtReceiptAmount.value = InsertDecimal(amt);
				}
			}
		}
		function FormatAmountForSum(num) {

		    num = ReplaceAll(num, sBaseDecimalSep, '.');
		    return num;
		}
		function validateLimit(objSource, objArgs) {

		    var Limt = document.getElementById(objSource.controltovalidate).value;

		    Limt = FormatAmountForSum(Limt);
		    if (parseFloat(Limt) >= 0 && parseFloat(Limt) <= 100)
		        objArgs.IsValid = true;
		    else
		        objArgs.IsValid = false;
		}
        

	function Init()
	{
	var objSource=document.getElementById('cmbRSKAssignLossCodes');
		funcValidateRiskExposer(objSource , objSource);
		objSource=document.getElementById('cmbLOBAssignLossCodes'); 
		funcValidateLOBs(objSource , objSource);
		objSource=document.getElementById('cmbSTATEAssignLossCodes');
		funcValidateStates(objSource , objSource);
		Commision();
		SetRiskExposer();
		SetLOBs();
		SetStates();
	}	
	</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();SetTab();Init();">
		<FORM id="MNT_REINSURANCE_CONTRACT" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" >
			
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
					<TR ID="tblBody"  runat="server">
					<TD>
						<TABLE  width="100%" align="center" border="0">
							<TBODY>
							<tr>
								<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
							</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCONTRACT_TYPE" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCONTRACT_TYPE" onfocus="SelectComboIndex('cmbCONTRACT_TYPE')" runat="server" tabIndex="1">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvCONTRACT_TYPE" runat="server" ErrorMessage="" ControlToValidate="cmbCONTRACT_TYPE"></asp:requiredfieldvalidator></TD><%--Please select Reinsurance Contract Type.--%>
								<TD class="midcolora" width="18%"><asp:label id="capCONTRACT_NUMBER" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCONTRACT_NUMBER" runat="server" maxlength="20" size="30" tabIndex="2"></asp:textbox><br>
								<asp:requiredfieldvalidator id="rfvCONTRACT_NUMBER" runat="server" ErrorMessage="" ControlToValidate="txtCONTRACT_NUMBER"></asp:requiredfieldvalidator></TD><%--Please enter Contract #.--%>
							</tr>
							<tr>
								<TD class="midcolora"  width="18%"><asp:label id="capCONTRACT_DESC" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCONTRACT_DESC" runat="server" size="500" onkeypress="MaxLength(this,50);" TextMode="MultiLine" tabIndex="3"></asp:textbox><BR>
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="capLOSS_ADJUSTMENT_EXPENSE" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOSS_ADJUSTMENT_EXPENSE" onfocus="SelectComboIndex('cmbLOSS_ADJUSTMENT_EXPENSE')"
										runat="server" tabIndex="4"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbLOSS_ADJUSTMENT_EXPENSE')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A><BR>
									<asp:requiredfieldvalidator id="rfvLOSS_ADJUSTMENT_EXPENSE" runat="server" ErrorMessage=""
										ControlToValidate="cmbLOSS_ADJUSTMENT_EXPENSE"></asp:requiredfieldvalidator></TD><%--Please select Loss Adjustment Expense.--%>
							</tr>
							<tr>
							<TD class="midcolora" width="18%"><asp:label id="capCASH_CALL_LIMIT" runat="server"></asp:label><span class="mandatory">*</span></TD>
							<TD class="midcolora" width="32%"><asp:textbox id="txtCASH_CALL_LIMIT" CssClass="InputCurrency" MaxLength="12" runat="server" ></asp:textbox></br>
							<asp:RequiredFieldValidator ID="rfvCASH_CALL_LIMIT" ControlToValidate='txtCASH_CALL_LIMIT' runat=server></asp:RequiredFieldValidator><br />
							<asp:regularexpressionvalidator id="revCASH_CALL_LIMIT" runat="server" Display="Dynamic" ControlToValidate="txtCASH_CALL_LIMIT"
										ErrorMessage=""></asp:regularexpressionvalidator></TD>
							<TD class="midcolora" width="18%">  <%--Added by Aditya for TFS BUG # 2512--%>
                            <asp:label id="capMAX_NO_INSTALLMENT" runat="server"></asp:label><span class="mandatory">*</span>                     
                            </TD>
							<TD class="midcolora" width="32%">
                            <asp:dropdownlist id="cmbMAX_NO_INSTALLMENT" runat="server"></asp:dropdownlist><br />
                            <asp:requiredfieldvalidator id="rfvMAX_NO_INSTALLMENT" runat="server" ErrorMessage=""
										ControlToValidate="cmbMAX_NO_INSTALLMENT"></asp:requiredfieldvalidator>
                            </TD>
							</tr>
						</TABLE>
						<!--START MULTISELECT COMBO BOX -->
						<!--<TABLE cellSpacing='0' cellPadding='0' class="tableWidthHeader" border='0'>-->
						<TABLE width="100%" align="center" border="0">
							<tr id="trLossCodes" runat="server">
								<td class="midcolorc" style="WIDTH: 289px" align="center" width="289" rowSpan="6"><asp:label id="capRSKUnassignLossCodes" runat="server"></asp:label><br><%--Reinsurance Risk Exposure--%>
								<asp:listbox id="cmbRISK_EXPOSURE" Width="240px" SelectionMode="Multiple" Height="150px" Runat="server" tabIndex="5"></asp:listbox></td>
								
								<td class="midcolorc" style="WIDTH: 412px" vAlign="middle" align="center" width="412"
									rowSpan="7"><br>
									<br>
									<INPUT class="clsButton" id="btnRSKAssignLossCodes" onclick="javascript:RSKAssignLossCodes();"
										type="button" value=">>" name="btnRSKAssignLossCodes" runat="server" tabIndex="6"><br>
									<br>
									<INPUT class="clsButton" id="btnRSKUnAssignLossCodes" onclick="javascript:RSKUnAssignLossCodes();"
										type="button" value="<<" name="btnRSKUnAssignLossCodes" runat="server" tabIndex="8">
								</td>
								<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:label id="capRSKAssignedLossCodes" runat="server"></asp:label><span class="mandatory">*</span><br><%--Applicable Risk Exposures--%>
								<asp:listbox id="cmbRSKAssignLossCodes" Width="240px" SelectionMode="Multiple" Height="150px"
										Runat="server" tabIndex="7"></asp:listbox><br>
										<asp:customvalidator id="csvRISK_EXPOSURE" Display="Dynamic" ControlToValidate="cmbRSKAssignLossCodes" Runat="server"
												ClientValidationFunction="funcValidateRiskExposer" ErrorMessage ="Please 
												select Risk Exposeres."></asp:customvalidator>
										<asp:requiredfieldvalidator id="rfvRSKAssignLossCodes" runat="server" ErrorMessage=""
										ControlToValidate="cmbRSKAssignLossCodes"></asp:requiredfieldvalidator>		<%--Please select Risk Exposure.--%>
										</td>
							</tr>
						</TABLE>
						<!--END MULTISELECT COMBO BOX -->
						<!--START MULTISELECT COMBO BOX -->
						<!--<TABLE cellSpacing='0' cellPadding='0' class="tableWidthHeader" border='0'>-->
						<TABLE width="100%" align="center" border="0">
							<tr id="Tr1" runat="server">
								<td class="midcolorc" style="WIDTH: 289px; HEIGHT: 13px" align="center" width="289"><asp:label id="capLOBUnassignLossCodes" runat="server"></asp:label></td><%--LOB's in System--%>
								<td class="midcolorc" style="WIDTH: 412px" vAlign="middle" align="center" width="412"
									rowSpan="7" ><br>
									<br>
									<INPUT class="clsButton" id="btnLOBAssignLossCodes" onclick="javascript:LOBAssignLossCodes();"
										type="button" value=">>" name="btnLOBAssignLossCodes" runat="server" tabIndex="9"><br>
									<br>
									<INPUT class="clsButton" id="btnLOBUnAssignLossCodes" onclick="javascript:LOBUnAssignLossCodes();"
										type="button" value="<<" name="btnLOBUnAssignLossCodes" runat="server" tabIndex="11">
								</td>
								<td class="midcolorc" style="HEIGHT: 13px" align="center" width="33%"><asp:label id="capLOBAssignedLossCodes" runat="server" ></asp:label><span class="mandatory">*</span></td><%--LOB's in Contract--%>
							</tr>
							<tr>
								<td class="midcolorc" style="WIDTH: 289px" align="center" width="289" rowSpan="6">
								<asp:listbox id="cmbCONTRACT_LOB" Width="240px" SelectionMode="Multiple" Height="150px" Runat="server" tabIndex="8"></asp:listbox></td>
								<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="cmbLOBAssignLossCodes" Width="240px" SelectionMode="Multiple" Height="150px"
										Runat="server" tabIndex="10"></asp:listbox><br>
										<asp:customvalidator id="csvCONTRACT_LOB" Display="Dynamic" ControlToValidate="cmbLOBAssignLossCodes" Runat="server"
												ClientValidationFunction="funcValidateLOBs" ErrorMessage ="Please 
												select LOBs."></asp:customvalidator>
									<asp:requiredfieldvalidator id="rfvLOBAssignLossCodes" runat="server" ErrorMessage=""
										ControlToValidate="cmbLOBAssignLossCodes"></asp:requiredfieldvalidator>		<%--Please select LOB's--%>
										</td>			
										</td>
							</tr>
						</TABLE>
						<!--END MULTISELECT COMBO BOX -->
						<!--START MULTISELECT COMBO BOX -->
						<!--<TABLE cellSpacing='0' cellPadding='0' class="tableWidthHeader" border='0'>-->
						<TABLE width="100%" align="center" border="0">
							<tr id="Tr2" runat="server">
								<td class="midcolorc" style="WIDTH: 289px; HEIGHT: 13px" align="center" width="289">
								<asp:label id="capSTATE_ID" runat="server"></asp:label></td><%--States in System--%>
								<td class="midcolorc" style="WIDTH: 412px" vAlign="middle" align="center" width="412"
									rowSpan="7" ><br>
									<br>
									<INPUT class="clsButton" id="btnSTATEAssignLossCodes" onclick="javascript:STATEAssignLossCodes();"
										type="button" value=">>" name="btnSTATEAssignLossCodes" runat="server" tabIndex="13"><br>
									<br>
									<INPUT class="clsButton" id="btnSTATEUnAssignLossCodes" onclick="javascript:STATEUnAssignLossCodes();"
										type="button" value="<<" name="btnSTATEUnAssignLossCodes" runat="server" tabIndex="15">
								</td>
								<td class="midcolorc" style="HEIGHT: 13px" align="center" width="33%"><asp:label id="capSTATEAssignLossCodes" runat="server"></asp:label><span id="spnSTATEAssignLossCodes"  runat="server"  class="mandatory">*</span></td><%--States in Contract--%>
							</tr>
							<tr>
								<td class="midcolorc" style="WIDTH: 289px" align="center" width="289" rowSpan="6">
								<asp:listbox id="cmbSTATE_ID" Width="240px" SelectionMode="Multiple" Height="150px" Runat="server" tabIndex="12"></asp:listbox></td>
								<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="cmbSTATEAssignLossCodes" Width="240px" SelectionMode="Multiple" Height="150px"
										Runat="server" tabIndex="14"></asp:listbox><br>
										<asp:customvalidator id="csvSTATEAssignLossCodes" Display="Dynamic" ControlToValidate="cmbSTATEAssignLossCodes" Runat="server"
												ClientValidationFunction="funcValidateStates" ErrorMessage ="Please select States"></asp:customvalidator>
											<asp:requiredfieldvalidator id="rfvSTATEAssignLossCodes" runat="server" ErrorMessage=""
										ControlToValidate="cmbSTATEAssignLossCodes"></asp:requiredfieldvalidator>			<%--Please select States.--%>
										</td>
							</tr>
						</TABLE>
						<!--END MULTISELECT COMBO BOX -->
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capORIGINAL_CONTACT_DATE" runat="server"></asp:label><SPAN class="mandatory">*</SPAN></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtORIGINAL_CONTACT_DATE" runat="server" maxlength="10" size="12" Display="Dynamic" tabIndex="16"></asp:textbox><asp:hyperlink id="hlkORIGINAL_CONTACT_DATE" runat="server" CssClass="HotSpot">
										<asp:Image runat="server" ID="imgORIGINAL_CONTACT_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
											valign="middle"></asp:Image>
									</asp:hyperlink><BR>
									<asp:requiredfieldvalidator id="rfvORIGINAL_CONTACT_DATE" runat="server" ErrorMessage=""
										ControlToValidate="txtORIGINAL_CONTACT_DATE"></asp:requiredfieldvalidator><br><%--Please enter Original Contract Date--%>
										<asp:regularexpressionvalidator id="revORIGINAL_CONTACT_DATE" runat="server" Display="Dynamic" ControlToValidate="txtORIGINAL_CONTACT_DATE"
											ErrorMessage=""></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
										</TD>
								<TD class="midcolora" width="18%"><asp:label id="capCONTACT_YEAR" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCONTACT_YEAR" onfocus="SelectComboIndex('cmbCONTACT_YEAR')" runat="server" tabIndex="17">
										
									</asp:dropdownlist><br><asp:requiredfieldvalidator id="rfvCONTACT_YEAR" runat="server" ErrorMessage="" ControlToValidate="cmbCONTACT_YEAR"></asp:requiredfieldvalidator></TD><%--Please select Contract Year--%>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capEFFECTIVE_DATE" runat="server"></asp:label><SPAN class="mandatory">*</SPAN></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_DATE" runat="server" maxlength="10" size="12" Display="Dynamic" tabIndex="18"></asp:textbox><asp:hyperlink id="hlkEffectiveDate" runat="server" CssClass="HotSpot">
										<asp:image id="imgEffectiveDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
											valign="middle"></asp:image>
									</asp:hyperlink><BR>
									<asp:requiredfieldvalidator id="rfvEFFECTIVE_DATE" runat="server" ErrorMessage="" ControlToValidate="txtEFFECTIVE_DATE"></asp:requiredfieldvalidator><br><%--Please enter Contract Effective Date--%>
									<asp:regularexpressionvalidator id="revEFFECTIVE_DATE" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_DATE" ErrorMessage=""></asp:regularexpressionvalidator>
									<asp:CompareValidator ID='cpvDate' ControlToValidate='txtEFFECTIVE_DATE'  Operator="LessThanEqual" Type="Date" ControlToCompare='txtEXPIRATION_DATE' runat="server"></asp:CompareValidator> <%--Changed by Aditya for TFS bug # 923--%>
											
											<%--RegularExpressionValidator--%>
											 </TD>
								<TD class="midcolora" width="18%"><asp:label id="capEXPIRATION_DATE" runat="server">Contract Expiration Date</asp:label><SPAN class="mandatory">*</SPAN></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtEXPIRATION_DATE" runat="server" maxlength="10" size="12" Display="Dynamic" onBlur ="adjustFollowupDate();" tabIndex="19"></asp:textbox><asp:hyperlink id="hlkExpirationDate" runat="server" CssClass="HotSpot">
										<asp:image id="imgExpirationDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
											valign="middle"></asp:image>
									</asp:hyperlink><br><asp:requiredfieldvalidator id="rfvEXPIRATION_DATE" runat="server" ErrorMessage="" ControlToValidate="txtEXPIRATION_DATE"></asp:requiredfieldvalidator><br><%--Please enter Contract Expiration Date--%>
										<asp:regularexpressionvalidator id="revEXPIRATION_DATE" runat="server" Display="Dynamic" ControlToValidate="txtEXPIRATION_DATE"
										ErrorMessage=""></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
							</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCOMMISSION" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<%--MaxLength and Size modified for Itrack Issue 5397 on 4 Feb 09--%>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOMMISSION" runat="server" maxlength="7" size="8"  class="INPUTCURRENCY" tabIndex="20"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvCOMMISSION" runat="server" ErrorMessage="" ControlToValidate="txtCOMMISSION"></asp:requiredfieldvalidator><%--Please enter Commission.--%>
									
									<br><asp:regularexpressionvalidator id="revCOMMISSION" runat="server" Display="Dynamic" ControlToValidate="txtCOMMISSION"
											ErrorMessage=""></asp:regularexpressionvalidator>	<br><%--RegularExpressionValidator--%>
											
									<%--Range made to Double for Itrack Issue 5397 on 4 Feb 09--%>
                                    <asp:CustomValidator ID="csvCOMMISSION" runat="server" ControlToValidate="txtCOMMISSION" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimit"></asp:CustomValidator>
									
									<%--<asp:RangeValidator id="rngCOMMISSION" runat="server"   Display="Dynamic" ControlToValidate="txtCOMMISSION"   Type="Double" MinimumValue="0" MaximumValue="100"></asp:RangeValidator>--%>

																					
									</TD>
									
								<TD class="midcolora" width="18%"><asp:label id="capCALCULATION_BASE" runat="server"></asp:label><SPAN class="mandatory">*</SPAN></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCALCULATION_BASE" runat="server" tabIndex="21"></asp:dropdownlist>
								<A class="calcolora" href="javascript:showPageLookupLayer('cmbCALCULATION_BASE')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
								<br><asp:requiredfieldvalidator id="rfvCALCULATION_BASE" runat="server" ErrorMessage="" ControlToValidate="cmbCALCULATION_BASE"></asp:requiredfieldvalidator></TD><%--Please select Premium Basis.--%>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capPER_OCCURRENCE_LIMIT" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox style="TEXT-ALIGN:right" id="txtPER_OCCURRENCE_LIMIT" runat="server" maxlength="13" size="30" class="INPUTCURRENCY" onblur="formatAmount(this.value);" tabIndex="22"></asp:textbox><BR>
									<%--<asp:requiredfieldvalidator id="rfvPER_OCCURRENCE_LIMIT" runat="server" ErrorMessage="Please enter Per Occurrence Limit."
										ControlToValidate="txtPER_OCCURRENCE_LIMIT"></asp:requiredfieldvalidator>--%>
										<br><asp:regularexpressionvalidator id="revPER_OCCURRENCE_LIMIT" runat="server" Display="Dynamic" ControlToValidate="txtPER_OCCURRENCE_LIMIT"
											ErrorMessage=""></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
										</TD>
								<TD class="midcolora" width="18%"><asp:label id="capANNUAL_AGGREGATE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox style="TEXT-ALIGN:right" id="txtANNUAL_AGGREGATE" runat="server" maxlength="13" size="30" class="INPUTCURRENCY" tabIndex="23"></asp:textbox><br>
								<%--<asp:requiredfieldvalidator id="rfvANNUAL_AGGREGATE" runat="server" ErrorMessage="Please enter Annual Aggregate." ControlToValidate="txtANNUAL_AGGREGATE"></asp:requiredfieldvalidator><br>--%>
								<asp:regularexpressionvalidator id="revANNUAL_AGGREGATE" runat="server" Display="Dynamic" ControlToValidate="txtANNUAL_AGGREGATE" ErrorMessage=""></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capDEPOSIT_PREMIUMS" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox style="TEXT-ALIGN:right" id="txtDEPOSIT_PREMIUMS" runat="server" maxlength="13" size="30" class="INPUTCURRENCY" tabIndex="24"></asp:textbox><BR>
									<%--<asp:requiredfieldvalidator id="rfvDEPOSIT_PREMIUMS" runat="server" ErrorMessage="Please enter Deposit Premiums." ControlToValidate="txtDEPOSIT_PREMIUMS"></asp:requiredfieldvalidator><br>--%>
									<asp:regularexpressionvalidator id="revDEPOSIT_PREMIUMS" runat="server" Display="Dynamic" ControlToValidate="txtDEPOSIT_PREMIUMS"
										ErrorMessage=""></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
						
									</TD>
								<TD class="midcolora" width="18%"><asp:label id="capDEPOSIT_PREMIUM_PAYABLE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDEPOSIT_PREMIUM_PAYABLE" runat="server" tabIndex="25"></asp:dropdownlist>
								<A class="calcolora" href="javascript:showPageLookupLayer('cmbDEPOSIT_PREMIUM_PAYABLE')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
								<%--<br><asp:requiredfieldvalidator id="rfvDEPOSIT_PREMIUM_PAYABLE" runat="server" ErrorMessage="Please select Deposit Premium Payable."
										ControlToValidate="cmbDEPOSIT_PREMIUM_PAYABLE"></asp:requiredfieldvalidator>--%>
										
										</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capMINIMUM_PREMIUM" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox style="TEXT-ALIGN:right" id="txtMINIMUM_PREMIUM" runat="server" maxlength="13" size="30" class="INPUTCURRENCY" tabIndex="26"></asp:textbox><BR>
									<%--<asp:requiredfieldvalidator id="rfvMINIMUM_PREMIUM" runat="server" ErrorMessage="Please enter Minimum Premium" ControlToValidate="txtMINIMUM_PREMIUM"></asp:requiredfieldvalidator><br>--%>
									<asp:regularexpressionvalidator id="revMINIMUM_PREMIUM" runat="server" Display="Dynamic" ControlToValidate="txtMINIMUM_PREMIUM"
										ErrorMessage=""></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
									</TD>
								<TD class="midcolora" width="18%"><asp:label id="capSEQUENCE_NUMBER" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSEQUENCE_NUMBER" runat="server" maxlength="2" size="5" class="INPUTCURRENCY" tabIndex="27"></asp:textbox>
								<%--<br><asp:requiredfieldvalidator id="rfvSEQUENCE_NUMBER" runat="server" ErrorMessage="Please enter Sequence #" ControlToValidate="txtSEQUENCE_NUMBER"></asp:requiredfieldvalidator>--%>
								<br><asp:regularexpressionvalidator id="revSEQUENCE_NUMBER" runat="server" Display="Dynamic" ControlToValidate="txtSEQUENCE_NUMBER"
										ErrorMessage=""></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capTERMINATION_DATE" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtTERMINATION_DATE" runat="server" maxlength="10" size="12" Display="Dynamic" tabIndex="28"></asp:textbox><asp:hyperlink id="hlkTERMINATION_DATE" runat="server" CssClass="HotSpot">
										<asp:Image runat="server" ID="imgTERMINATION_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
											valign="middle"></asp:Image>
									</asp:hyperlink><BR>
									<asp:requiredfieldvalidator id="rfvTERMINATION_DATE" runat="server" ErrorMessage="" ControlToValidate="txtTERMINATION_DATE"></asp:requiredfieldvalidator><br><%--Please enter Termination Date--%>
									<asp:regularexpressionvalidator id="revTERMINATION_DATE" runat="server" Display="Dynamic" ControlToValidate="txtTERMINATION_DATE"
											ErrorMessage=""></asp:regularexpressionvalidator><%--RegularExpressionValidator--%>
									</TD>
								<TD class="midcolora" width="18%"><asp:label id="capTERMINATION_REASON" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtTERMINATION_REASON" runat="server" maxlength="30" size="30" tabIndex="29"></asp:textbox><BR>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" rowspan="2" style="VERTICAL-ALIGN:middle"><asp:label id="capCOMMENTS" runat="server"></asp:label></TD>
								<TD class="midcolora" rowspan="2" style="VERTICAL-ALIGN:middle"><asp:textbox id="txtCOMMENTS" runat="server" onkeypress="MaxLength(this,255);"  TextMode="MultiLine" size="12" Display="Dynamic" tabIndex="30"></asp:textbox><BR>
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="capFOLLOW_UP_FIELDS" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtFOLLOW_UP_FIELDS" runat="server" maxlength="10" size="12" Display="Dynamic" tabIndex="31"></asp:textbox> <asp:hyperlink id="hlkFOLLOW_UP_FIELDS" runat="server" CssClass="HotSpot">
										<asp:Image runat="server" ID="imgFOLLOW_UP_FIELDS" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
											valign="middle"></asp:Image>
									</asp:hyperlink><BR>
								<asp:regularexpressionvalidator id="revFOLLOW_UP_FIELDS" runat="server" Display="Dynamic" ControlToValidate="txtFOLLOW_UP_FIELDS"
											ErrorMessage=""></asp:regularexpressionvalidator>	<%--RegularExpressionValidator--%>
								</TD>
							</tr>
							<tr>
										<TD class="midcolora"><asp:label id="capFOLLOW_UP_FOR" runat="server"></asp:label></TD>
										<TD class="midcolora"><asp:dropdownlist id="cmbFOLLOW_UP_FOR" Runat="server" tabIndex="32"></asp:dropdownlist></TD>

							</tr>
							<tr>
								<TD class="headrow" colSpan="4"><asp:Label ID="capPREMIUM_COMMISION" runat="server"></asp:Label></TD><%--Premium/Commission Section--%>
							</tr>
							<tr id="trRein_Premium">
							<TD class="headerEffectSystemParams" colSpan="4"><asp:Label ID="capPREMIUN_SECTION" runat="server"></asp:Label></TD><%--Premium Section--%>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCOMMISSION_APPLICABLE" runat="server"></asp:label><!--<SPAN class="mandatory">*</SPAN>--></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCOMMISSION_APPLICABLE" onfocus="SelectComboIndex('cmbCOMMISSION_APPLICABLE')"
										runat="server" onChange="javascript:Commision();" tabIndex="33"></asp:dropdownlist>
										<br>
				                </TD>
								<TD class="midcolora" colspan="2"></TD>
							</tr>
							<tr id="trREINSURANCE_PAYABLE">
								<TD class="midcolora" width="18%"><asp:label id="capREINSURANCE_PREMIUM_ACCOUNT" runat="server"></asp:label><!--<span class="mandatory">*</span>--></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREINSURANCE_PREMIUM_ACCOUNT" onfocus="SelectComboIndex('cmbREINSURANCE_PREMIUM_ACCOUNT')"
										runat="server" tabIndex="34">
										<asp:ListItem Value="0" Selected="True">No Value</asp:ListItem>
									</asp:dropdownlist><BR>
									</TD>
			
								<TD class="midcolora" width="18%"><asp:label id="capREINSURANCE_PAYABLE_ACCOUNT" runat="server"></asp:label><!--<span class="mandatory">*</span>--></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREINSURANCE_PAYABLE_ACCOUNT" onfocus="SelectComboIndex('cmbREINSURANCE_PAYABLE_ACCOUNT')"
										runat="server" tabIndex="35">
										<asp:ListItem Value="0" Selected="True">No Value</asp:ListItem>
									</asp:dropdownlist><BR>
								</TD>
							
							</tr>
							<tr id="trRein_Commision">
							<TD class="headerEffectSystemParams" colSpan="4"><asp:Label ID="capCOMMISSION_SECTION" runat="server"></asp:Label></TD><%--Commission Section--%>
							</tr>
							<tr id="trREINSURANCE_COMMISSION">
								<TD class="midcolora" width="18%"><asp:label id="capREINSURANCE_COMMISSION_ACCOUNT" runat="server"></asp:label><!--<span class="mandatory">*</span>--></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREINSURANCE_COMMISSION_ACCOUNT" onfocus="SelectComboIndex('cmbREINSURANCE_COMMISSION_ACCOUNT')"
										runat="server" tabIndex="36">
										<asp:ListItem Value="0" Selected="True">No Value</asp:ListItem>
									</asp:dropdownlist><BR>
									</TD>

								<TD class="midcolora" width="18%"><asp:label id="capREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT" runat="server"></asp:label><!--<span class="mandatory">*</span>--></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT" onfocus="SelectComboIndex('cmbREINSURANCE_COMMISSION_RECEIVABLE_ACCOUNT')"
										runat="server" tabIndex="37">
										<asp:ListItem Value="0" Selected="True">No Value</asp:ListItem>
									</asp:dropdownlist><BR>
									</TD>
							</tr>
							<tr>
								<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" CausesValidation="false" tabIndex="38"></cmsb:cmsbutton>
                                    <cmsb:cmsbutton class="clsButton" id="btnActivate" runat="server" Text="Deactivate"
										visible="False"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" CausesValidation="false" tabIndex="39"
										visible="False"></cmsb:cmsbutton></td>
								<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" tabIndex="40"></cmsb:cmsbutton></td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
							<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
							<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
							<INPUT id="hidCONTRACT_ID" type="hidden" value="0" name="hidCONTRACT_ID" runat="server">
							<INPUT id="hidRISK_EXPOSURE" type="hidden" value="" name="hidRISK_EXPOSURE" runat="server">
							<INPUT id="hidCONTRACT_LOB" type="hidden" value="" name="hidCONTRACT_LOB" runat="server">
							<INPUT id="hidSTATE_ID" type="hidden" value="" name="hidSTATE_ID" runat="server">
							<INPUT id="hidRISK_EXPOSURE_OLD" type="hidden" value="0" name="hidRISK_EXPOSURE_OLD" runat="server">
							<INPUT id="hidCONTRACT_LOB_OLD" type="hidden" value="0" name="hidCONTRACT_LOB_OLD" runat="server">
							<INPUT id="hidSTATE_ID_OLD" type="hidden" value="0" name="hidSTATE_ID_OLD" runat="server">
							<input  type="hidden" runat="server" ID="hidTAB_TITLES"  value=""  name="hidTAB_TITLES"/>  
							<INPUT id="hidIS_XOL_TYPE" type="hidden" value="" name="hidIS_XOL_TYPE" runat="server">
						</TABLE>
					</TD>
					</TBODY> 
				</TR>
			</TABLE>
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none; Z-INDEX: 101; POSITION: absolute"><iframe id="lookupLayerIFrame" style="DISPLAY: none; Z-INDEX: 1001; FILTER: alpha(opacity=0); BACKGROUND-COLOR: #000000"
				width="0" height="0" left="0px" top="0px;"></iframe>
		</div>
		<DIV id="lookupLayer" onmouseover="javascript:refreshLookupLayer();" style="Z-INDEX: 101; VISIBILITY: hidden; POSITION: absolute">
			<table class="TabRow" cellSpacing="0" cellPadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b><asp:label id="CapAddLookup" runat="server"  Text="Add LookUp"></asp:label></b></td>
					<td>
						<p align="right"><A onclick="javascript:hideLookupLayer();" href="javascript:void(0)"><IMG height="14" alt="Close" src="/cms/cmsweb/images/cross.gif" width="16" border="0"></A></p>
					</td>
				</tr>
				<tr class="SubTabRow">
					<td colSpan="2"><span id="LookUpMsg"></span></td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->
		<script>
			RefreshWindowsGrid(1,document.getElementById('hidCONTRACT_ID').value);
			RefreshWebGrid(1,document.getElementById('hidCONTRACT_ID').value,true);
		</script>
	</BODY>
</HTML>

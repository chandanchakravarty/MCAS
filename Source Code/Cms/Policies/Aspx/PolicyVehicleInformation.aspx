<%@ Page language="c#" Codebehind="PolicyVehicleInformation.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.PolicyVehicleInformation" ValidateRequest="false"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Policy Vehicle Infomation</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script src="/cms/cmsweb/scripts/Coverages.js"></script>
		<script language="vbscript">
			Function getUserConfirmationForSymbol
				'Changed  By Rajiv
				'Caption is Changed from 'Amazon' to 'BUKS' as per Issue no. 4291
				getUserConfirmationForSymbol= msgbox("Do you wish to change the symbol as well based on the amount entered?",35,"CMS")
			End function
			
			' Added By Swastika on 9th Mar'06 for Pol Iss #58		
			Function getUserConfirmationForDeactivate
				getUserConfirmationForDeactivate= msgbox("Deactivating the current Vehicle will unassign it from the driver." & vbcrlf & " Do you want to continue?",35,"CMS")
			End function
			Function getUserConfirmationForDelete
				getUserConfirmationForDelete= msgbox("Deleting the current Vehicle will unassign it from the driver." & vbcrlf & " Do you want to continue?",35,"CMS")
			End function
		</script>
		</SCRIPT>
		<script language="javascript">
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
			var GlobalError=false;
		<%
		if(gIntShowVINPopup==1)
		{
		%>		
			ShowLookUpWindow();		
		<%}%>
		
		 
			 
		var varLOB;
		/*function formatCurrencyOnLoad()
		{
			document.getElementById('txtANNUAL_MILEAGE').value=formatCurrency(document.getElementById('txtANNUAL_MILEAGE').value);	
			return false;
		}*/
		var AmountSymbol="0";
		function CallService_Old(VehicleType)
		{ 

			var VehicleUseType = document.getElementById('cmbAPP_USE_VEHICLE_ID').item(document.getElementById('cmbAPP_USE_VEHICLE_ID').selectedIndex).text;
			var Year="0";
			if(VehicleUseType=="Personal")
			{
				/*if(VehicleType=="11868" || VehicleType=="11869")
					{
						document.getElementById("txtSYMBOL").value ="0";
					}
				else
				{*/
					if(document.getElementById("txtVEHICLE_YEAR").value!="")
					{
						Year = document.getElementById("txtVEHICLE_YEAR").value;
							/*if(document.getElementById("txtVEHICLE_YEAR").value >= "1990") 
								Year = "1990";
							else
								Year = "1989"*/
					}
					
					
					if( typeof(myTSMain1.useService) == 'undefined') 
					{	
						setTimeout( 'CallService(VehicleType)', 1000);
					}
					else 
					{
						////Getting the currently loged in agency id
						//strAgencyId = GetAgencyID();
						myTSMain1.useService(lstr.toString(), "TSM");
						if(VehicleType=="11334")
						{

							if(document.getElementById("txtSYMBOL").value=="")
							{	
        						if(document.getElementById("txtAMOUNT").value!="")
								{ 

									var strippedAmount = ReplaceAll(document.getElementById("txtAMOUNT").value,",","");									
									if(!(isNaN(strippedAmount)) && strippedAmount>=0)
										myTSMain1.TSM.callService(createData, "GetSymbolForAppPolicy", VehicleType,strippedAmount,Year);
									AmountSymbol++;
								}
							}
							else if(AmountSymbol >0)
							{
								
									var strippedAmount = ReplaceAll(document.getElementById("txtAMOUNT").value,",","");									
									if(!(isNaN(strippedAmount)) && strippedAmount>=0)
										myTSMain1.TSM.callService(createData, "GetSymbolForAppPolicy", VehicleType,strippedAmount,Year);
									
							}
						}
						else
						{

							if(document.getElementById("txtAMOUNT").value!="")
							{ 
								var strippedAmount = ReplaceAll(document.getElementById("txtAMOUNT").value,",","");									
								if(!(isNaN(strippedAmount)) && strippedAmount>=0)
									myTSMain1.TSM.callService(createData, "GetSymbolForAppPolicy", VehicleType,strippedAmount,Year);
							
							}
							else

								document.getElementById("txtSYMBOL").value ="";
						}
					}
				//}	
			}
			else if(VehicleUseType=="Commercial")
			{
				if(VehicleType!="11338" && VehicleType!="11339" && VehicleType!="11340" && VehicleType!="11871" && VehicleType!="11341") return;			
				
				
					
				if( typeof(myTSMain1.useService) == 'undefined') 
				{
					setTimeout( 'CallService(VehicleType)', 1000);
				}
				else 
				{
					////Getting the currently loged in agency id
					//strAgencyId = GetAgencyID();					
					myTSMain1.useService(lstr.toString(), "TSM");	
					if(document.getElementById("txtAMOUNT").value!="")
						{ 
							var strippedAmount = ReplaceAll(document.getElementById("txtAMOUNT").value,",","");									
							if(!(isNaN(strippedAmount)) && strippedAmount>=0)
								myTSMain1.TSM.callService(createData, "GetSymbolForAppPolicy", VehicleType,strippedAmount,Year);
						}
						else
							document.getElementById("txtSYMBOL").value ="";
				}
			}
		}		
		function createData(Result)
		{
			if(!(Result.error))
			{
				if(Result.value != "0" && Result.value!="undefined")
				document.getElementById("txtSYMBOL").value= Result.value;
				ChangeColor();
			}
		}
		
		
		
		/////////////////START VALIDATE SYMBOL///////////////
		function CustomValidateSymbol(objSource, objArgs)
		{			
			
			var P_symbol = document.getElementById('txtSYMBOL').value;
			var P_amount = ReplaceAll(document.getElementById("txtAMOUNT").value,",","");
						
			P_symbol = ParseFloatEx(P_symbol);			
			
			if(P_symbol!="" && P_symbol!="0" && P_amount!="")
			{
				var Symbol_Amount = ValidateSymbol();			
			
				var Symbol = Symbol_Amount.split('^')[0];
				var Amount = Symbol_Amount.split('^')[1];	
				
				
				if(P_symbol!="" && P_amount!="")
				{
					if( (parseInt(P_amount)) <= (parseInt(Amount)) )
					{
						if( (P_symbol) > ParseFloatEx(Symbol))
						{
							objArgs.IsValid = false;						
						}
					}
					if( (parseInt(P_amount)) >= (parseInt(Amount)) )
					{
						if( (P_symbol) > ParseFloatEx(Symbol))
						{
							objArgs.IsValid = false;						
						}
					}
					
				}	
			}	
		}
		
		function ValidateSymbol()
		{			
		
			var VehicleUse = document.getElementById('cmbAPP_USE_VEHICLE_ID').options[document.getElementById("cmbAPP_USE_VEHICLE_ID").selectedIndex].text;
			
			if(VehicleUse=="Personal")
			{
				VehicleType = document.getElementById("cmbAPP_VEHICLE_PERTYPE_ID").options[document.getElementById("cmbAPP_VEHICLE_PERTYPE_ID").selectedIndex].value;
				var Year = document.getElementById("txtVEHICLE_YEAR").value ;//Added for iTrack Issue 6227 on 10 Aug 09
				if(VehicleType=="11335" || VehicleType=="11336" || VehicleType=="11337" || VehicleType=="11870" || VehicleType=="11334" || VehicleType=="11868" || VehicleType=="11869")
				{
					var result = PolicyVehicleInformation.AjaxValidateSymbol(VehicleType, Year);//Added for iTrack Issue 6227 on 10 Aug 09
					return ResultSymbol(result);
				}
				return "";//Added by Charles on 16-Sep-09 to handle error when VehicleType=="11618"
			}
			if(VehicleUse=="Commercial")
			{			  
				VehicleType = document.getElementById("cmbAPP_VEHICLE_COMTYPE_ID").options[document.getElementById("cmbAPP_VEHICLE_COMTYPE_ID").selectedIndex].value;		
				if(VehicleType=="11338" || VehicleType=="11339" || VehicleType=="11340" || VehicleType=="11871" || VehicleType=="11341")
				{
					var result = PolicyVehicleInformation.AjaxValidateSymbol(VehicleType,0);//Added for iTrack Issue 6227 on 10 Aug 09
					return ResultSymbol(result);		
				}
			}
			
		}
				
		function ResultSymbol(Result)
		{
		
			if(!(Result.error))
			{
				if(Result.value != "0" && Result.value!="undefined")
				{
					return Result.value;
				}	
					
			}
		}
		////////////////END VALIDATE SUMBOL
		
		
		///////////////////////////////// SYMOBL AJAX ///////////////////////
		function CallService(VehicleType)
		{
			var VehicleUseType = document.getElementById('cmbAPP_USE_VEHICLE_ID').item(document.getElementById('cmbAPP_USE_VEHICLE_ID').selectedIndex).text;
			var Year="0";
			if(document.getElementById("txtVEHICLE_YEAR").value!="")
				Year = document.getElementById("txtVEHICLE_YEAR").value;
					
					
			if(VehicleUseType=="Personal")
			{
					if(VehicleType!="11337" && VehicleType!="11336" && VehicleType!="11335" && VehicleType!="11870" && VehicleType!="11334" && VehicleType!="11868" && VehicleType!="11869") return;			
					
						if(VehicleType=="11334")
						{
							if(document.getElementById("txtSYMBOL").value=="")
								{
								if(document.getElementById("txtAMOUNT").value!="")
									{
										var strippedAmount = ReplaceAll(document.getElementById("txtAMOUNT").value,",","");				
										if(!(isNaN(strippedAmount)) && strippedAmount>=0)
										{
											PolicyVehicleInformation.AjaxGetSymbolForAppPolicy(VehicleType,strippedAmount,Year,createData);
											AmountSymbol++;
										}	
									}
								}
							else if(AmountSymbol >0)
								{
									var strippedAmount = ReplaceAll(document.getElementById("txtAMOUNT").value,",","");				
										if(!(isNaN(strippedAmount)) && strippedAmount>=0)
										{
											PolicyVehicleInformation.AjaxGetSymbolForAppPolicy(VehicleType,strippedAmount,Year,createData);
										}	
								}
						}
						else
						{
							if(document.getElementById("txtAMOUNT").value!="")
							{
								var strippedAmount = ReplaceAll(document.getElementById("txtAMOUNT").value,",","");				
								if(!(isNaN(strippedAmount)) && strippedAmount>=0)
								{
									PolicyVehicleInformation.AjaxGetSymbolForAppPolicy(VehicleType,strippedAmount,Year,createData);
								}	
							}
							else
							{
								document.getElementById("txtSYMBOL").value ="";
							}
						}					
				
				
			}
			else if(VehicleUseType=="Commercial")
			{
			
			if(VehicleType!="11338" && VehicleType!="11339" && VehicleType!="11340" && VehicleType!="11871" && VehicleType!="11341") return;			
				
				
					if(document.getElementById("txtAMOUNT").value!="")
					{
						var strippedAmount = ReplaceAll(document.getElementById("txtAMOUNT").value,",","");				
						if(!(isNaN(strippedAmount)) && strippedAmount>=0)
						{
							//myTSMain1.TSM.callService(createData, "GetSymbolForAppPolicy", VehicleType,strippedAmount,Year);
							PolicyVehicleInformation.AjaxGetSymbolForAppPolicy(VehicleType,strippedAmount,Year,createData);
						}	
					}
					else
					{
						document.getElementById("txtSYMBOL").value ="";
					}
				//}	
			}
		}
		///////////////////////////////////END AJAX /////////////////////////
		//On Change Of Year
		function checkForSymbol()
		{
		
		  if(document.getElementById('txtVEHICLE_YEAR').value!="")
		   {
		    GetSymbol();
		   }
		   else
		    {
		      document.getElementById('txtSYMBOL').value='';
		     
		    } 

		}
		
		function GetSymbol()
		{			

			var VehicleUse,VehicleType;
			VehicleUse = document.getElementById('cmbAPP_USE_VEHICLE_ID').item(document.getElementById('cmbAPP_USE_VEHICLE_ID').selectedIndex).text;
			if(VehicleUse=="Personal" )
			{
				if(document.getElementById('cmbAPP_VEHICLE_PERTYPE_ID').selectedIndex==-1) return;
				VehicleType = document.getElementById("cmbAPP_VEHICLE_PERTYPE_ID").options[document.getElementById("cmbAPP_VEHICLE_PERTYPE_ID").selectedIndex].value;
				//if(VehicleUse=="Personal" && VehicleType=="11334")
				//	return;
				//Call the web-service only when the vehicle use is personal and vehicle type is of Motorhome(11336), Trailer(11337) or Customized Van(11335) or Camper & Travel Trailers(11870)
				if(VehicleUse=="Personal" && (VehicleType=="11335" || VehicleType=="11336" || VehicleType=="11337" || VehicleType=="11870" || VehicleType=="11334" || VehicleType=="11868" || VehicleType=="11869"))

					CallService(VehicleType);
			}
			else if(VehicleUse=="Commercial")
			{
			VehicleType = document.getElementById("cmbAPP_VEHICLE_COMTYPE_ID").options[document.getElementById("cmbAPP_VEHICLE_COMTYPE_ID").selectedIndex].value;
			if(document.getElementById('cmbAPP_VEHICLE_COMTYPE_ID').selectedIndex==-1) return;
			if(VehicleUse=="Commercial" && (VehicleType=="11338" || VehicleType=="11339" || VehicleType=="11340" || VehicleType=="11871" || VehicleType=="11341"))	
				CallService(VehicleType);
			}
			
		}
		function txtSymbolMatch()
		{

			if(document.getElementById('cmbAPP_USE_VEHICLE_ID').item(document.getElementById('cmbAPP_USE_VEHICLE_ID').selectedIndex).text =="Commercial" && document.getElementById("txtAMOUNT").value!="")
			{
				GetSymbol();
			}
		}
		 
		function EnableDisableAmount(val)
		{
			if(val==false)
			{
				document.getElementById('rfvAMOUNT').setAttribute("enabled",false);
				document.getElementById('rfvAMOUNT').style.display = "none";
				document.getElementById('spnamount').style.display = "none";
				document.getElementById('txtAmount').className="";
			}
			else
			{
				document.getElementById('rfvAMOUNT').setAttribute("enabled",true);
				//document.getElementById('rfvAMOUNT').style.display = "inline";
				document.getElementById('spnamount').style.display = "inline";
				document.getElementById('txtAmount').className="MandatoryControl";
			}						
			ApplyColor();
			ChangeColor();
		}
		
		
		
		
		function AddData()			
		{			
			document.getElementById('hidCUSTOMER_ID').value	=	'New';
			
			document.getElementById('txtVIN').focus();
			 
			document.getElementById('txtVEHICLE_YEAR').value  = '';
			document.getElementById('txtMAKE').value  = '';
			document.getElementById('txtMODEL').value  = '';
			document.getElementById('txtBODY_TYPE').value  = '';

			document.getElementById('txtVIN').value  = '';
			document.getElementById('txtGRG_ADD1').value  = '';
			document.getElementById('txtGRG_ADD2').value  = '';
			document.getElementById('txtGRG_CITY').value  = '';
			document.getElementById('cmbGRG_COUNTRY').options.selectedIndex = 1;
			document.getElementById('cmbGRG_STATE').options.selectedIndex = -1;
			document.getElementById('txtGRG_ZIP').value  = '';
			document.getElementById('cmbREGISTERED_STATE').options.selectedIndex = -1;
			document.getElementById('txtTERRITORY').value  = '';
			document.getElementById('hidTERRITORY').value  = '';
			
			//document.getElementById('txtCLASS').value  = '';
			//document.getElementById('txtREGN_PLATE_NUMBER').value  = '';
			document.getElementById('cmbST_AMT_TYPE').options.selectedIndex = 1;
			document.getElementById('txtAMOUNT').value  = '';
			document.getElementById('txtSYMBOL').value  = '';
			document.getElementById('txtVEHICLE_AGE').value  = '';
			document.getElementById('hidVEHICLE_AGE').value  = '';
			
			document.getElementById('lblAge').innerHTML = '';
			if(document.getElementById('btnDelete'))
			document.getElementById('btnDelete').setAttribute('disabled',true);
			
			//document.getElementById('cmbIS_OWN_LEASE').focus();
			document.getElementById('cmbIS_OWN_LEASE').options.selectedIndex = -1;
			document.getElementById('txtPURCHASE_DATE').value  = '';
			document.getElementById('cmbIS_NEW_USED').options.selectedIndex = -1;	
			//document.getElementById('cmbMILES_TO_WORK').options.selectedIndex = -1;
			document.getElementById('cmbVEHICLE_USE').options.selectedIndex = 4;	
			//document.getElementById('cmbMULTI_CAR').options.selectedIndex = -1;
			document.getElementById('txtANNUAL_MILEAGE').value  = '';
			
			//NO WILL BE DEFAULT
			//document.getElementById('cmbPASSIVE_SEAT_BELT').options.selectedIndex = -1;
			//document.getElementById('cmbPASSIVE_SEAT_BELT').options.selectedIndex = 1;
			//Added By Raghav on 23-07-2008 Itrack Issue #4548
			document.getElementById('cmbPASSIVE_SEAT_BELT').options.value ="10963";			
						
			document.getElementById('cmbAIR_BAG').options.selectedIndex = -1;
			document.getElementById('cmbANTI_LOCK_BRAKES').options.selectedIndex = -1;
			
			//added by vj 17-10-2005 to reset the value of vehicle use, class and type.
			document.getElementById('cmbAPP_USE_VEHICLE_ID').options.selectedIndex = 2;
			document.getElementById('cmbAPP_VEHICLE_PERCLASS_ID').options.selectedIndex = -1;
			if(document.getElementById('hidSetIndex').value == "1")
				document.getElementById('cmbAPP_VEHICLE_PERTYPE_ID').options.selectedIndex = 4;
			else
				document.getElementById('cmbAPP_VEHICLE_PERTYPE_ID').options.selectedIndex = 6;
				
		
			document.getElementById('cmbAPP_VEHICLE_COMCLASS_ID').options.selectedIndex = -1;
			document.getElementById('cmbAPP_VEHICLE_COMTYPE_ID').options.selectedIndex = -1;
			//document.getElementById('cmbSAFETY_BELT').options.selectedIndex = -1;
			
			document.getElementById('txtMILES_TO_WORK').value  = '';
			document.getElementById('capSNOWPLOW_CONDS').style.display = 'none';
			document.getElementById('cmbSNOWPLOW_CONDS').style.display = 'none';
			document.getElementById('spnSNOWPLOW_CONDS').style.display = 'none';
			if(document.getElementById('cmbCAR_POOL')!=null)
				document.getElementById('cmbCAR_POOL').options.selectedIndex = -1;	
			if(document.getElementById('txtAUTO_POL_NO'))
				document.getElementById('txtAUTO_POL_NO').value='';		
				
			if(document.getElementById('txtRADIUS_OF_USE'))
				document.getElementById('txtRADIUS_OF_USE').value='';
			if(document.getElementById('cmbTRANSPORT_CHEMICAL')!=null)
				document.getElementById('cmbTRANSPORT_CHEMICAL').options.selectedIndex = -1;
			if(document.getElementById('cmbCOVERED_BY_WC_INSU')!=null)
				document.getElementById('cmbCOVERED_BY_WC_INSU').options.selectedIndex = -1;						
            if(document.getElementById('btnActivateDeactivate'))
				document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			
			ChangeColor();
			//if(document.getElementById('cmbUNDERINS_MOTOR_INJURY_COVE')!=null)
			//	RejectUninsured();
			DisableValidators();			
		}
		function ShowHidePopUpIcon()
		{				
			//Display the Window Pop-Up Icon only when the vehicle type is for Private Passenger Auto=11334
			val = document.getElementById('cmbAPP_VEHICLE_PERTYPE_ID').selectedIndex;
			if(val==-1)
			{
				document.getElementById("imgLookUpWindow").style.display="none";			
				return;						
			}
			if(document.getElementById("cmbAPP_VEHICLE_PERTYPE_ID").options[val].value=="11334")			
			{				
				document.getElementById("imgLookUpWindow").style.display="inline";
				EnableDisableAmount(false);
			}
			else
			{
				document.getElementById("imgLookUpWindow").style.display="none";			
				EnableDisableAmount(true);
			}
			/*if(document.getElementById('cmbVEHICLE_TYPE_PER')!=null && document.getElementById('cmbVEHICLE_TYPE_PER').selectedIndex>0)
			{
				var VehicleType = document.getElementById("cmbVEHICLE_TYPE_PER").options[document.getElementById("cmbVEHICLE_TYPE_PER").selectedIndex].value;
				CallService(VehicleType);
			}*/
			
		}
		
		function showPageLookupLayer(controlId)
		{
			var lookupMessage;						
			switch(controlId)
			{
				case "cmbVEHICLE_USE":
					lookupMessage	=	"USECD.";
					break;
				case "cmbPASSIVE_SEAT_BELT":
					lookupMessage	=	"PRTCD.";
					break;
				case "cmbANTI_LOCK_BRAKES":
					lookupMessage	=	"yesno.";
					break;
				case "cmbAIR_BAG":
					lookupMessage	=	"%AIRB.";
					break;
				default:
					lookupMessage	=	"Look up code not found";
					break;
			}
			
			showLookupLayer(controlId,lookupMessage);							
		}
		
		function ResetTheForm()
		{
			var url;		
			document.getElementById("hidSymbolCheck").value="1";	
			if(document.getElementById('hidVehicleID').value!="NEW" && document.getElementById('hidVehicleID').value!="")
				url="PolicyVehicleInformation.aspx?CALLEDFROM=<%=strCalledFrom%>&CUSTOMER_ID="+document.getElementById('hidCustomerID').value+ "&POLICY_ID="+document.getElementById('hidPolicyID').value+"&POLICY_VERSION_ID="+document.getElementById('hidPolicyVersionID').value+"&VH_ID="+document.getElementById('hidVehicleID').value+"&transferdata="
			else
				url="PolicyVehicleInformation.aspx?CALLEDFROM=<%=strCalledFrom%>&CUSTOMER_ID="+document.getElementById('hidCustomerID').value+ "&POLICY_ID="+document.getElementById('hidPolicyID').value+"&POLICY_VERSION_ID="+document.getElementById('hidPolicyVersionID').value+"&transferdata="
			window.location.href=url;
			document.getElementById("hidSymbolCheck").value="0";	
			return false;
			
		}	
		
		
		function populateXML()
		{	
			
			varLOB = document.getElementById('hidAPP_LOB').value;
			
			if (document.getElementById('hidCheckZipSubmit')!=null && document.getElementById('hidCheckZipSubmit').value !="zip")
			{
					if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
					{
						var tempXML;	 
						document.getElementById('spanVIN').style.display = "inline";						
						
						if(document.getElementById('hidOldData')!=null)
						{
							tempXML=document.getElementById('hidOldData').value;	
							if(tempXML!="" && tempXML!=0)
							{	 
							    if(document.getElementById('btnActivateDeactivate'))//Added by Pradeep Kushwaha on 10-sep-2010
					                document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
								if(document.getElementById('btnDelete'))
									document.getElementById('btnDelete').setAttribute('disabled',false); 		 
								populateFormData(tempXML,APP_VEHICLES);	
								document.getElementById('txtAMOUNT').value=formatCurrency(document.getElementById('txtAMOUNT').value);
								document.getElementById('txtANNUAL_MILEAGE').value=formatCurrency(document.getElementById('txtANNUAL_MILEAGE').value);	
								varLOB = document.getElementById('hidAPP_LOB').value;

								if(document.APP_VEHICLES.txtPURCHASE_DATE.value=="01/01/1900")
									document.APP_VEHICLES.txtPURCHASE_DATE.value='';
									
							}
							else
							{
								if(document.getElementById('txtVIN')!=null && document.getElementById('txtVIN').value =="")
								{
									AddData();
								}
								varLOB = document.getElementById('hidAPP_LOB').value;
							}
						}
						else
						{
							
							varLOB = document.getElementById('hidAPP_LOB').value;
						}
					}					
					GetAgeOfVehicle();
					showQuesForMichigan();
					//fxnDispSnowPlow();
					fxnShowCarPool();
					fxnShwPolNo();
					return false;
			}
			else
			{
				if(document.getElementById('hidCheckZipSubmit')!=null)
				{
					document.getElementById('hidCheckZipSubmit').value="";				
					//if(document.getElementById('cmbUNDERINS_MOTOR_INJURY_COVE')!=null)
			         //	RejectUninsured();
			    	return false;
				}
			}
			
			
			
			
		}
		
		
		

		function setTab()
		{
		  
		  
		   if(document.getElementById('hidOldData').value	!= '')
		   {
		
 			if (document.getElementById('hidVehicleID')!=null && document.getElementById('hidVehicleID').value!="NEW")
			{
				var CalledFrom ='';
				if (document.getElementById('hidCalledFrom')!=null)
				{
					CalledFrom=document.getElementById('hidCalledFrom').value;
				}
				var CustomerID='';
				if (document.getElementById('hidCustomerID')!=null)
				{
					CustomerID=document.getElementById('hidCustomerID').value;
				}
				var AppID='';
				if (document.getElementById('hidAPPID')!=null)
				{
					AppID=document.getElementById('hidAPPID').value;
				}
				var AppVersionID='';
				if (document.getElementById('hidAppVersionID')!=null)
				{
					AppVersionID=document.getElementById('hidAppVersionID').value;
				}
				var VehicleID='';
				if (document.getElementById('hidVehicleID')!=null)
				{
					VehicleID=document.getElementById('hidVehicleID').value;
				}
				if(document.getElementById('hidAPP_LOB').value!=null)
				{
				LOB_ID=document.getElementById('hidAPP_LOB').value;
				}
				 
				
				if (CalledFrom !="UMB")
				{
					//Url="../../application/aspx/PersVehicleInformation.aspx?CalledFrom="+CalledFrom+"&CUSTOMER_ID="+CustomerID+"&APP_ID="+AppID+"&APP_VERSION_ID="+AppVersionID+"&VEHICLE_ID="+VehicleID +"&"; 
					//DrawTab(2,top.frames[1],'Personal Vehicle Info',Url); 

					//Url="../../application/aspx/AddAppCoverages.aspx?CoverageType=Vehicle&pageTitle=Vehicle&MaxRows=5&CalledFrom="+CalledFrom+"&CUSTOMER_ID="+CustomerID+"&APP_ID="+AppID+"&APP_VERSION_ID="+AppVersionID+"&VEHICLE_ID="+VehicleID +"&"; 
					//DrawTab(3,top.frames[1],'Vehicle Covg Info',Url); 
					
					Url="../../policies/aspx/PolicyCoverages.aspx?CalledFrom="+CalledFrom+"&pageTitle=Coverages" + "&VEHICLEID=" + VehicleID  + "&LOB_ID="+LOB_ID+"&" ; 
					DrawTab(2,top.frames[1],'Vehicle Coverages',Url); 
					
					Url="../../policies/aspx/PolicyEndorsement.aspx?CalledFrom="+CalledFrom+"&pageTitle=Coverages" + "&VEHICLEID=" + VehicleID  + "&LOB_ID="+LOB_ID+"&" ; 
					DrawTab(3,top.frames[1],'Endorsements',Url); 					
					
					Url="../../policies/aspx/Automobile/PolicyAdditionalInterestIndex.aspx?CalledFrom="+CalledFrom+"&CUSTOMER_ID="+CustomerID+"&APP_ID="+AppID+"&APP_VERSION_ID="+AppVersionID +"&VEHICLE_ID="+VehicleID+"&"; 
					DrawTab(4,top.frames[1],'Additional Interest',Url); 
					
					//Url="AddAutoIdInformation.aspx?CalledFrom="+CalledFrom+"&CUSTOMER_ID="+CustomerID+"&APP_ID="+AppID+"&APP_VERSION_ID="+AppVersionID +"&VEHICLE_ID="+VehicleID+"&"; 
					//DrawTab(5,top.frames[1],'AutoID Info',Url); 
				}
				else
				{
					Url="../../policies/aspx/PolicyEndorsements.aspx?CalledFrom="+CalledFrom+"&pageTitle=Vehicle Coverages" + "&VEHICLEID=" + VehicleID  + "&LOB_ID="+LOB_ID+"&" ; 
					DrawTab(3,top.frames[1],'Endorsements',Url); 	 
					
					Url="../../application/aspx/Coverages.aspx?CalledFrom=UMB&pageTitle=Coverages" + "&VEHICLEID=" + VehicleID  + "&" ; 
					DrawTab(2,top.frames[1],'Vehicle Coverages',Url); 
					
					
					Url="../../policies/aspx/Automobile/PolicyAdditionalInterestIndex.aspx?CalledFrom="+CalledFrom+"&CUSTOMER_ID="+CustomerID+"&APP_ID="+AppID+"&APP_VERSION_ID="+AppVersionID +"&VEHICLE_ID="+VehicleID+"&"; 
					DrawTab(4,top.frames[1],'Remarks',Url); 
				}
			}
			else
			{
				
				//RemoveTab(6,top.frames[1]);	
				//RemoveTab(5,top.frames[1]);	
				RemoveTab(4,top.frames[1]);	
				RemoveTab(3,top.frames[1]);	
				RemoveTab(2,top.frames[1]);	
			}
			}
			else
			{				
				//RemoveTab(5,top.frames[1]);	
				RemoveTab(4,top.frames[1]);	
				RemoveTab(3,top.frames[1]);	
				RemoveTab(2,top.frames[1]);	
				this.parent.strSelectedRecordXML='';
			}
			return false; 
		}

		function setUpperCase()
		{
			var vinNum = document.getElementById('txtVIN').value;					
				vinNum = vinNum.toUpperCase();
				document.getElementById('txtVIN').value = vinNum;				
		}
	
		function ShowLookUpWindow()
		{	 
			//RP -- 18 Aug 2006 -- Gen Issue 3291			
			//CHECK THAT VIN is valid or not ... if it is valid then fetch 
			//MODEL_YEAR , MAKE_NAME , SERIES_NAME , BODY_TYPE from DB. Otherwise open popup			
		
			if(document.getElementById('txtVIN')!=null)
			{	
				var vin = document.getElementById('txtVIN').value;
				PolicyVehicleInformation.AjaxGetDetailsFromVIN(SearchAndReplace(vin,"&","REPLACE_CHAR"),CallBackFunction);															
				
			}
		}
		
		function CallBackFunction(Result)
		{
			if(!(Result.error))
			{
				if(Result.value != "0" && Result.value!="undefined")
				{
					//alert(Result.value);					
					var objXmlHandler = new XMLHandler();
					var tree = (objXmlHandler.quickParseXML(Result.value).getElementsByTagName('NewDataSet')[0]);

					if(tree)
					{
					  //Added for Itrack Issue 5680 on 21 April 2009
					  if( tree.childNodes[0]!=undefined &&  tree.childNodes[0]!='' &&  tree.childNodes[0]!=null)
					  {
						//-1 means VIN does not exist in DB
						if (tree.childNodes[0].childNodes[0].childNodes[0].text == "-1" || tree.childNodes.length>1)//Added for Itrack Issue 5680 on 14 April 2009
						{
							var vin = document.getElementById('txtVIN').value;
							//Replace & with ^..it will be reversed when it is fetched from QueryString
							vin = SearchAndReplace(vin,"&","^");		
							if(tree.childNodes.length>1)//Added for Itrack Issue 5680 on 14 April 2009
							   ShowPopup('/cms/application/Aspx/AddVINMasterPopup.aspx?CalledFrom=' + document.getElementById('hidCalledFrom').value + '&VIN='+vin + '&Msg=Y','VehicleInformation', 400, 225);
							else		
							  ShowPopup('/cms/application/Aspx/AddVINMasterPopup.aspx?CalledFrom=' + document.getElementById('hidCalledFrom').value + '&VIN='+vin,'VehicleInformation', 400, 200);
							return;
						}
						//POPULATE TEXT BOX FROM VALUES FETCHED FROM DB
						else
						{
							var lStrAntiLock='',lStrAirBag='';
							if(tree.childNodes[0].childNodes[1].childNodes[0]!=undefined && tree.childNodes[0].childNodes[1].childNodes[0].text!='')
								document.getElementById("txtVEHICLE_YEAR").value = tree.childNodes[0].childNodes[1].childNodes[0].text;
							if(tree.childNodes[0].childNodes[2].childNodes[0] !=undefined && tree.childNodes[0].childNodes[2].childNodes[0].text!='')
								document.getElementById("txtMAKE").value = tree.childNodes[0].childNodes[2].childNodes[0].text;
							if(tree.childNodes[0].childNodes[3].childNodes[0] !=undefined && tree.childNodes[0].childNodes[3].childNodes[0].text!='')
								document.getElementById("txtMODEL").value = tree.childNodes[0].childNodes[3].childNodes[0].text;
							if(tree.childNodes[0].childNodes[4].childNodes[0] !=undefined && tree.childNodes[0].childNodes[4].childNodes[0].text!='')
								document.getElementById("txtBODY_TYPE").value = tree.childNodes[0].childNodes[4].childNodes[0].text;						
							if(tree.childNodes[0].childNodes[5].childNodes[0] !=undefined && tree.childNodes[0].childNodes[5].childNodes[0].text!='')
								lStrAntiLock = tree.childNodes[0].childNodes[5].childNodes[0].text;				
							if(tree.childNodes[0].childNodes[6].childNodes[0] !=undefined && tree.childNodes[0].childNodes[6].childNodes[0].text!='')
								lStrAirBag = tree.childNodes[0].childNodes[6].childNodes[0].text;				
							if(tree.childNodes[0].childNodes[7].childNodes[0] !=undefined && tree.childNodes[0].childNodes[7].childNodes[0].text!='')
								document.getElementById("txtSYMBOL").value = tree.childNodes[0].childNodes[7].childNodes[0].text;				
							else
							 // if symbol comes blank from db then put it on the basis of amount field
								GetSymbol();
							document.getElementById("cmbANTI_LOCK_BRAKES").selectedIndex = -1;
							
							if(lStrAntiLock=="N" || lStrAntiLock=="n")							
								SelectComboOptionByText("cmbANTI_LOCK_BRAKES","No");
							else if(lStrAntiLock!='')
								SelectComboOptionByText("cmbANTI_LOCK_BRAKES","Yes");
							
							document.getElementById("cmbAIR_BAG").selectedIndex = -1;							
							if(lStrAirBag!='')
								SelectComboOption("cmbAIR_BAG",lStrAirBag);
							GetAgeOfVehicle();
						}
					  }
					}//if tree
				}//if result value <>0
			}//result <> error
		}//function
				
		function ShowCustomerVehicle()
		{
			var customerid = '';
			var appid='';
			var appversionid='';
			if (document.getElementById('hidCustomerId')!=null)
			{
				
				customerid = document.getElementById('hidCustomerId').value;
			}
			if (document.getElementById('hidAPPID')!=null)
			{
				appid = document.getElementById('hidAPPID').value;
			}
			if (document.getElementById('hidAppVersionID')!=null)
			{
				appversionid = document.getElementById('hidAppVersionID').value;
			}
			ShowPopup('/cms/application/Aspx/CustomerVehicle.aspx?CUSTOMER_ID='+customerid +'&APP_ID='+appid+'&APP_VERSION_ID='+appversionid ,'VehicleInformation', 950, 400);
			
		}
		function SetRegisteredState()
		{
			//if(document.getElementById("cmbREGISTERED_STATE").value==null || document.getElementById("cmbREGISTERED_STATE").value=='')
			//{
				SelectComboOption("cmbREGISTERED_STATE", document.getElementById("cmbGRG_STATE").value);
			//}
		}
		function GetAgeOfVehicle()
		{
			var CurrentDate= new Date();			
			//var CurrentYear = CurrentDate.getYear();
			var CurrentYear = document.getElementById("hidAPP_EFFECTIVE_YEAR").value;
			var CurrentMonth = document.getElementById("hidAPP_EFFECTIVE_MONTH").value;
			var modelMonth;
			if (isNaN(document.getElementById('txtVEHICLE_YEAR').value))
			{
				document.getElementById('txtVEHICLE_AGE').value='';
				document.getElementById('hidVEHICLE_AGE').value='';
				
			}
			else
			{
				var Age = '';
				var AgeToCompare;
				if (document.getElementById('txtVEHICLE_YEAR').value != '')
				{
					Age = CurrentYear - document.getElementById('txtVEHICLE_YEAR').value;
				}
				
				//Commented by Charles on 6-Jan-09 for Itrack 6899
				/* if(Age < 0)
					Age = 0; */
				
				if(document.getElementById('cmbAPP_USE_VEHICLE_ID')!=null && document.getElementById('cmbAPP_USE_VEHICLE_ID').selectedIndex!="-1")
				{
					if(document.getElementById('cmbAPP_USE_VEHICLE_ID').options[document.getElementById('cmbAPP_USE_VEHICLE_ID').selectedIndex].value == "11332") //Personal					
						AgeToCompare = parseInt(5);
					else if(document.getElementById('cmbAPP_USE_VEHICLE_ID').options[document.getElementById('cmbAPP_USE_VEHICLE_ID').selectedIndex].value == "11333") //Commercial					
						AgeToCompare = parseInt(3);
					modelMonth = parseInt(10);
					if(parseInt(Age)>AgeToCompare)
					{
						document.getElementById('lblAge').innerHTML = "Rated as " + ++AgeToCompare + " years old";
						if(CurrentMonth < modelMonth)
						{
						document.getElementById('txtVEHICLE_AGE').value = Age+1;	
						document.getElementById('hidVEHICLE_AGE').value = Age+1;	
										
						}
						else
						{
						document.getElementById('txtVEHICLE_AGE').value = Age+2;	
						document.getElementById('hidVEHICLE_AGE').value = Age+2;				
						}
					}
					else
					{
						if(Age <0)//Added by Charles on 6-Jan-2010 for Itrack 6899
						{
							document.getElementById('lblAge').innerHTML = '';
							if(document.getElementById('txtVEHICLE_YEAR').value == '')
							{
								document.getElementById('txtVEHICLE_AGE').value='';
								document.getElementById('hidVEHICLE_AGE').value='';
								}
							else	{				
								document.getElementById('txtVEHICLE_AGE').value='1';
								document.getElementById('hidVEHICLE_AGE').value='1';
								}
							return;
						}//Added till here
						
						document.getElementById('lblAge').innerHTML = '';
						if(document.getElementById('txtVEHICLE_YEAR').value == ''){
							document.getElementById('txtVEHICLE_AGE').value='';
							document.getElementById('hidVEHICLE_AGE').value='';
							}
						else
						if(CurrentMonth < modelMonth)
						{
							document.getElementById('txtVEHICLE_AGE').value=Age+1;
							document.getElementById('hidVEHICLE_AGE').value=Age+1;
						}
						else
						{
							if(parseInt(Age)>=AgeToCompare)
							{
							document.getElementById('lblAge').innerHTML = "Rated as " + ++AgeToCompare + " years old";
							}
							document.getElementById('txtVEHICLE_AGE').value=Age+2;
							document.getElementById('hidVEHICLE_AGE').value=Age+2;
						}
					}
				}
			}			
		}
		function handleResult(res) 
		{
			if(!res.error)
			{
				//if (res.value==true) 
				if (res.value!="") 
				{
					document.getElementById("txtTERRITORY").value= res.value;
					document.getElementById("hidTERRITORY").value= res.value;
					GlobalError=false;
				}
				else
				{
					GlobalError=true;
				}
			}
			else
			{
				GlobalError=true;		
			}			
		}
		
				function Validate()
		{
		    var result = GetZipForState();
			Page_ClientValidate();
		    fxnShowCarPool();			
			Page_IsValid = Page_IsValid && result;
			return Page_IsValid;
		}
			
		function GetZipForState_OLD()
		{		    
			GlobalError=true;					
			if(document.getElementById('txtGRG_ZIP').value!="")
			{
				//if(isNaN(document.getElementById('txtGRG_ZIP').value))
				//	return;				
				var intStateID = parseInt(document.getElementById('cmbGRG_STATE').options[document.getElementById('cmbGRG_STATE').options.selectedIndex].value);
				var strZipID = document.getElementById('txtGRG_ZIP').value;	
				var intLOB = parseInt(document.getElementById('hidAPP_LOB').value);
				var intCustomerId=parseInt(document.getElementById('hidCustomerID').value);
				var intPolId =parseInt(document.getElementById('hidPolicyID').value);
				var intPolVersionId=parseInt(document.getElementById('hidPolicyVersionID').value);
				var intvehicleuse = parseInt(document.getElementById('cmbAPP_USE_VEHICLE_ID').options[document.getElementById('cmbAPP_USE_VEHICLE_ID').options.selectedIndex].value);
				
				/******************************************************************
				Calling webservice synchronously so that web service calling function
				execution waits for web service response function to finish execution
				GlobalError variable is set and access after "handleResult" function set 
				its proper value based on response from webservice.
				
				Custom Validator is enabled on the basis of GlobalError returns false.
				
				This function is called from blur event of Zip textbox and save button click
				therefore control id is checked to ensure the event 				
				*******************************************************************/				
				try
				{
				var co=myTSMain1.createCallOptions();
				//co.funcName = "FetchZipForState";
				co.funcName = "FetchTerritoryForZipStateLob";
				co.async = false;
				co.SOAPHeader= new Object();					
				//var oResult = myTSMain1.FetchZip.callService(co,strZipID,intLOB,intStateID);				
				var oResult = myTSMain1.FetchZip.callService(co,strZipID,intLOB,intStateID,intCustomerId,intPolId,intPolVersionId,'POL',intvehicleuse);								
				handleResult(oResult);
				}
				catch(err)
				{
				GlobalError=true;
				}			
				if(GlobalError)
				{
					document.getElementById('csvGRG_ZIP').setAttribute('enabled',true);
					document.getElementById('csvGRG_ZIP').setAttribute('isValid',true);
					document.getElementById('csvGRG_ZIP').style.display = 'inline';								
					return false;
				}
				else
				{
					document.getElementById('csvGRG_ZIP').setAttribute('enabled',false); 
			   		document.getElementById('csvGRG_ZIP').setAttribute('isValid',false);
					document.getElementById('csvGRG_ZIP').style.display = 'none';
			
					//if(window.event.srcElement.id == "btnSave")
					//	document.forms[0].submit();					
					//if(SavePage)
					//	document.forms[0].submit();
						
					return true;
				}
			}	
			return false;		
		}
		////////AJAX ZIP IMPLEMENTATION
			
		function GetZipForState()
		{	
			GlobalError = true;
			if(document.getElementById('cmbGRG_STATE').value==14 ||document.getElementById('cmbGRG_STATE').value==22||document.getElementById('cmbGRG_STATE').value==49)
			{ 
					if(document.getElementById('txtGRG_ZIP').value!="")
					{
						var intStateID = parseInt(document.getElementById('cmbGRG_STATE').options[document.getElementById('cmbGRG_STATE').options.selectedIndex].value);
						var strZipID = document.getElementById('txtGRG_ZIP').value;	
						var intLOB = parseInt(document.getElementById('hidAPP_LOB').value);
						var intCustomerId=parseInt(document.getElementById('hidCustomerID').value);
						var intPolId =parseInt(document.getElementById('hidPolicyID').value);
						var intPolVersionId=parseInt(document.getElementById('hidPolicyVersionID').value);	
						var intvehicleuse = parseInt(document.getElementById('cmbAPP_USE_VEHICLE_ID').options[document.getElementById('cmbAPP_USE_VEHICLE_ID').options.selectedIndex].value);
						var result = PolicyVehicleInformation.AjaxFetchTerritoryForZipStateLob(strZipID,intLOB,intStateID,intCustomerId,intPolId,intPolVersionId,'POL',intvehicleuse);			
						return	AjaxCallFunction_CallBack(result);
										
					}
					return false;
			}
			else
				{
				    document.getElementById('csvGRG_ZIP').setAttribute('enabled',false); 
					document.getElementById('csvGRG_ZIP').setAttribute('isValid',false);
					document.getElementById('csvGRG_ZIP').style.display = 'none';				
					return true;	
			    } 
		}
		
		function AjaxCallFunction_CallBack(response)
		{		
			
			if(document.getElementById('cmbGRG_STATE').value==14 ||document.getElementById('cmbGRG_STATE').value==22||document.getElementById('cmbGRG_STATE').value==49)
			{ 
				if(document.getElementById('txtGRG_ZIP').value!="")
				{
					handleResult(response);
					if(GlobalError)
					{
						document.getElementById('csvGRG_ZIP').setAttribute('enabled',true);
						document.getElementById('csvGRG_ZIP').setAttribute('isValid',true);
						document.getElementById('csvGRG_ZIP').style.display = 'inline';
						return false;
									
					}
					else
					{
						document.getElementById('csvGRG_ZIP').setAttribute('enabled',false); 
			   			document.getElementById('csvGRG_ZIP').setAttribute('isValid',false);
						document.getElementById('csvGRG_ZIP').style.display = 'none';
						return true;
					}		
				}
				return false;
			}
			else
				return true;	
		}
		//////////////////////END
		
		function ChkResult(objSource , objArgs)
		{
			if(GlobalError==true)
			{
				Page_IsValid = false;
				objArgs.IsValid = false;
			}
			else
			{
				objArgs.IsValid = true;				
			}
			//setInterval("GetTerritory()", 1000);
			//document.getElementById("btnSave").click();
		}
		function GetTerritory()
		{
			
			
			if(document.getElementById('txtGRG_ZIP').value!="")
			{
				//if(isNaN(document.getElementById('txtGRG_ZIP').value))
					//return;
				var strZip = document.getElementById('txtGRG_ZIP').value;
				var intLOB = parseInt(document.getElementById('hidAPP_LOB').value);
				
			}			
		}
		
		function PutTerritory(Result)
		{
			if(Result.error)
			{        
				var xfaultcode   = Result.errorDetail.code;
				var xfaultstring = Result.errorDetail.string;
				var xfaultsoap   = Result.errorDetail.raw;        					
			}
			else{
				document.getElementById("txtTERRITORY").value= Result.value;
				document.getElementById("hidTERRITORY").value= Result.value;
		        }
		}
			
		function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbST_AMT_TYPE":
						lookupMessage	=	"AMT.";
						break;
					case "cmbVEHICLE_TYPE":
						lookupMessage	=	"VEHTYP.";
						break;
					case "cmbCUSTOMER_REASON_CODE":
						lookupMessage	=	"RCFC.";
						break;
					case "cmbCUSTOMER_REASON_CODE3":
						lookupMessage	=	"RCFC.";
						break;
					case "cmbVEHICLE_USE":
						lookupMessage	=	"USECD.";
						break;
						case "cmbPASSIVE_SEAT_BELT":
						lookupMessage	=	"PRTCD.";
						break;
						
					case "cmbANTI_LOCK_BRAKES":
						lookupMessage	=	"yesno.";
						break;
						
					case "cmbAIR_BAG":
						lookupMessage	=	"%AIRB.";
						break;	
					default:
						lookupMessage	=	"Look up code not found";
						break;
						
				}
				showLookupLayer(controlId,lookupMessage);							
			}
			
			function setamount()
			{	
			}
			
			function CheckValidateVIN()
			{	
				//Check added by Sumit on 25/10/2005
				if(document.getElementById('cmbAPP_VEHICLE_PERTYPE_ID')==null)
					return;
				if(document.getElementById('cmbAPP_VEHICLE_PERTYPE_ID').selectedIndex<0)
					return;
				val = document.getElementById('cmbAPP_VEHICLE_PERTYPE_ID').item(document.getElementById('cmbAPP_VEHICLE_PERTYPE_ID').selectedIndex).value;
				
				if(val==11334)
				{
					if (document.getElementById('txtVIN').value == "")
					{
						document.getElementById('rfvVIN').setAttribute("enabled",true);
						//document.getElementById('rfvVIN').style.display = "inline";
						document.getElementById('spanVIN').style.display = "inline";
						//document.getElementById('txtVIN').style.backgroundColor = "lightyellow";
					}
					
					//added by vj on 25-10-2005
					document.getElementById('rfvAMOUNT').setAttribute("enabled",false);
					document.getElementById('rfvAMOUNT').style.display = "none";
					document.getElementById('spnamount').style.display = "none";
					//document.getElementById('txtAMOUNT').style.backgroundColor = "white";
					//Itrack 5310
					
					
					if((document.getElementById('hidOldData').value == null || document.getElementById('hidOldData').value =="" || document.getElementById('hidOldData').value == "0") && document.getElementById("hidCalledFrom").value!='<%=CALLED_FROM_UMBRELLA.ToUpper()%>')
					{
						//Itrack 5310
						if(document.getElementById('hidVehCount').value!="")
						{
							//alert(parseInt(document.getElementById('hidVehCount').value));
							if(parseInt(document.getElementById('hidVehCount').value) >= 1)
							{
								document.getElementById('cmbMULTI_CAR').options.selectedIndex = 2;//Other Car On policy		
							}
							else
								document.getElementById('cmbMULTI_CAR').options.selectedIndex = 1; //NA
						
						}
						/*if(document.getElementById('hidVehCount').value == "0") // First PPA Vehicle
						{document.getElementById('cmbMULTI_CAR').options.selectedIndex = 1;}
						else if(document.getElementById('hidVehCount').value == "1") 	// Second PPA Vehicle
						{document.getElementById('cmbMULTI_CAR').options.selectedIndex = 2;}
						else
						{document.getElementById('cmbMULTI_CAR').options.selectedIndex = -1;}*/
					}

				}
				else
				{
					document.getElementById('rfvVIN').setAttribute("enabled",false);
					document.getElementById('rfvVIN').style.display = "none";
					//document.getElementById('spanVIN').style.display = "none";
					
					document.getElementById('txtVIN').style.backgroundColor = "white";
					
					//added by vj on 25-10-2005
					document.getElementById('rfvAMOUNT').setAttribute("enabled",true);
					//document.getElementById('rfvAMOUNT').style.display = "inline";
					document.getElementById('spnamount').style.display = "inline";
					// Added By Shafi
					//document.getElementById('txtAMOUNT').style.backgroundColor = "lightyellow";
					
				}
				
				// Added by Mohit Agarwal ITrack 4884 14 Oct 08
				if(val == 11869 || val == 11868)
				{
					if(document.getElementById('cmbST_AMT_TYPE').options.length > 0)
						document.getElementById('cmbST_AMT_TYPE').selectedIndex = 3;
				}
				else
				{
					if(document.getElementById('cmbST_AMT_TYPE').options.length > 0)
						document.getElementById('cmbST_AMT_TYPE').selectedIndex = 1;
				}

				//ChangeColor();
			}
			
			function checkError()
			{
				
			}
			
				
		function ChkDate(objSource , objArgs)
		{
			
			var expdate=document.APP_VEHICLES.txtPURCHASE_DATE.value;
			objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);
			
		}
		
		
		function DisplayVehicleClassType()
		{
			var VehicleUse;
			if(document.getElementById('cmbAPP_USE_VEHICLE_ID')!=null)
			{			
				//Check added by Sumit on 25/10/2005
				if(document.APP_VEHICLES.cmbAPP_USE_VEHICLE_ID.selectedIndex<0)
				{
					return;
				}
				
					
				VehicleUse = document.getElementById('cmbAPP_USE_VEHICLE_ID').item(document.getElementById('cmbAPP_USE_VEHICLE_ID').selectedIndex).text;
				
				document.getElementById('rfvAPP_VEHICLE_COMTYPE_ID').setAttribute('enabled',true);
					//document.getElementById('rfvAPP_VEHICLE_COMTYPE_ID').setAttribute('isValid',true);
					//document.getElementById('rfvAPP_VEHICLE_COMTYPE_ID').style.display = "inline";
					
					document.getElementById('rfvAPP_VEHICLE_PERTYPE_ID').setAttribute('enabled',true);
					//document.getElementById('rfvAPP_VEHICLE_PERTYPE_ID').setAttribute('isValid',true);
					//document.getElementById('rfvAPP_VEHICLE_PERTYPE_ID').style.display = "inline";
				if (VehicleUse == "Personal" )
				{
					document.getElementById('rowVehiclePersonal').style.display = "inline";
					document.getElementById('rowVehicleCommercial').style.display = "none";
					document.getElementById('imgLookUpWindow').style.display = "inline";
					document.getElementById('rfvAPP_VEHICLE_COMTYPE_ID').setAttribute('enabled',false);
					document.getElementById('rfvAPP_VEHICLE_COMTYPE_ID').setAttribute('isValid',false);
					document.getElementById('rfvAPP_VEHICLE_COMTYPE_ID').style.display = "none";	
					document.getElementById('capCommClass').style.display = "none";
					document.getElementById('cmbCommClass').style.display = "none";
					document.getElementById('tbCommercial').style.display = "none";
					document.getElementById('tbPersonal').style.display = "inline";
					//ADDED BY PRAVEEN KUMAR(02-03-2009):ITRACK 5518
					document.getElementById('trIsSuspended_COM').style.display='none';
					document.getElementById('rfvIS_SUSPENDED_COM').setAttribute("enabled",false);
					//END PRAVEEN KUMAR
					ShowHidePopUpIcon();	
					shwBUSS_PERM_RESI();
					EnableCommercialValidators(false);									
					EnablePersonalValidators(true);
					fxnShwPolNo();
					fxnDispSnowPlow();
				}
				else if (VehicleUse == "Commercial" )
				{
					document.getElementById('rowVehiclePersonal').style.display = "none";
					document.getElementById('rowVehicleCommercial').style.display = "inline";
					document.getElementById('imgLookUpWindow').style.display = "none";					
					document.getElementById('rfvAPP_VEHICLE_PERTYPE_ID').setAttribute('enabled',false);
					document.getElementById('rfvAPP_VEHICLE_PERTYPE_ID').setAttribute('isValid',false);
					document.getElementById('rfvAPP_VEHICLE_PERTYPE_ID').style.display = "none"						
					document.getElementById('rowBUSS_PERM_RESI').style.display='none';
					document.getElementById('rfvBUSS_PERM_RESI').setAttribute("enabled",false);
					document.getElementById('tbCommercial').style.display = "inline";
					document.getElementById('capCommClass').style.display = "inline";
					document.getElementById('cmbCommClass').style.display = "inline";
					document.getElementById('tbPersonal').style.display = "none";		
					document.getElementById('trIsSuspended').style.display='none';
					document.getElementById('rfvIS_SUSPENDED').setAttribute("enabled",false);		
					document.getElementById('rfvSNOWPLOW_CONDS').setAttribute('enabled',false);												
					document.getElementById('cmbIS_SUSPENDED').options.seletedIndex=-1;
					shwBUSS_PERM_RESI();
					shwIS_SUSPENDED();
					EnableDisableAmount(true);
					EnableCommercialValidators(true);
					EnablePersonalValidators(false);
				}
				else 
				{
					document.getElementById('rowVehiclePersonal').style.display = "none";
					document.getElementById('rowVehicleCommercial').style.display = "none";
					document.getElementById('imgLookUpWindow').style.display = "none";	
					document.getElementById('rfvAPP_VEHICLE_COMTYPE_ID').setAttribute('disabled',true);
					document.getElementById('rfvAPP_VEHICLE_PERTYPE_ID').setAttribute('disabled',true);	
					document.getElementById('rowBUSS_PERM_RESI').style.display='none';
					document.getElementById('rfvBUSS_PERM_RESI').setAttribute("enabled",false);		
					document.getElementById('capCommClass').style.display = "none";
					document.getElementById('cmbCommClass').style.display = "none";
					document.getElementById('tbCommercial').style.display = "none";
					document.getElementById('tbPersonal').style.display = "none";
					document.getElementById('trIsSuspended').style.display='none';
					document.getElementById('rfvIS_SUSPENDED').setAttribute("enabled",false);
					//ADDED BY PRAVEEN KUMAR(02-03-2009):ITRACK 5518
					document.getElementById('trIsSuspended_COM').style.display='none';
					document.getElementById('rfvIS_SUSPENDED_COM').setAttribute("enabled",false);
					//END PRAVEEN KUMAR									
					EnableCommercialValidators(false);
					EnablePersonalValidators(false);
				}
				GetAgeOfVehicle();
				showQuesForMichigan();
			}			
		}
		function EnableValidator(ValidatorID,flag)
		{
			if(flag) //Enable the validator
			{
				document.getElementById(ValidatorID).setAttribute('enabled',true);
				document.getElementById(ValidatorID).setAttribute('isValid',true);
			}
			else
			{
				document.getElementById(ValidatorID).setAttribute('enabled',false);				
				document.getElementById(ValidatorID).style.display = "none";
			}
		}
		function EnablePersonalValidators(flag)
		{

			EnableValidator("rfvMILES_TO_WORK",flag);
			EnableValidator("rngMILES_TO_WORK",flag);						
			EnableValidator("rfvAUTO_CAR_POOL",flag);	
			EnableValidator("rfvAUTO_POL_NO",flag);	
			//EnableValidator("revANNUAL_MILEAGE",flag);	
			EnableValidator("rngANNUAL_MILEAGE",flag);				
			
		}
		function EnableCommercialValidators(flag)
		{
			EnableValidator("rfvRADIUS_OF_USE",flag);
			EnableValidator("rngRADIUS_OF_USE",flag);
			EnableValidator("rfvTRANSPORT_CHEMICAL",flag);	
			if(document.getElementById('hidState').value == "22")
				EnableValidator("rfvCOVERED_BY_WC_INSU",flag);
		}
		function shwBUSS_PERM_RESI()
		{
			var value = document.getElementById('cmbAPP_VEHICLE_PERTYPE_ID').item(document.getElementById('cmbAPP_VEHICLE_PERTYPE_ID').selectedIndex).value;
			var VehicleUse = document.getElementById('cmbAPP_USE_VEHICLE_ID').item(document.getElementById('cmbAPP_USE_VEHICLE_ID').selectedIndex).value;
			if(value==11336 && VehicleUse=="11332")//If the Type is Motor home, Truck or Van Camper she/hide fields
			{			
				document.getElementById('rowBUSS_PERM_RESI').style.display='inline';
				document.getElementById('rfvBUSS_PERM_RESI').setAttribute("enabled",true);
			}
			else
				{
				document.getElementById('rowBUSS_PERM_RESI').style.display='none';
				document.getElementById('cmbBUSS_PERM_RESI').options.selectedIndex = -1;
				document.getElementById('rfvBUSS_PERM_RESI').setAttribute("enabled",false);
				 }
			/*Vehicle Types
				11870->Camper& Travel Trailer
				11618->Suspended-Comp Only
				11337->Utility Trailer
				*/
			//Itrack 5310	
			/*if((value=="11870" || value=="11337" || value=="11618") && VehicleUse=="11332") 
			{
				//11918->Not applicable
				SelectComboOption("cmbMULTI_CAR","11918");
				document.getElementById("cmbMULTI_CAR").disabled = true;
			}
			else
				document.getElementById("cmbMULTI_CAR").disabled = false;*/
			//Itrack 4849
			if((value=="11334" || value=="11336" || value=="11335" || value=="11868" || value=="11869") && VehicleUse=="11332")
			{
				document.getElementById('trIsSuspended').style.display='inline';
				document.getElementById('cmbIS_SUSPENDED').options.selectedIndex = 1;
				document.getElementById('rfvIS_SUSPENDED').setAttribute("enabled",true);
			}
			else
			{
					document.getElementById('trIsSuspended').style.display='none';
					document.getElementById('cmbIS_SUSPENDED').options.selectedIndex = -1;
					document.getElementById('rfvIS_SUSPENDED').setAttribute("enabled",false);
					document.getElementById('rfvIS_SUSPENDED').style.display = 'none';
			
			}		
		}
		
		function shwIS_SUSPENDED()
		{
			if(document.getElementById('cmbAPP_VEHICLE_COMTYPE_ID').selectedIndex != '-1')
			{
				var valueCOM = document.getElementById('cmbAPP_VEHICLE_COMTYPE_ID').item(document.getElementById('cmbAPP_VEHICLE_COMTYPE_ID').selectedIndex).value;
				var VehicleUse = document.getElementById('cmbAPP_USE_VEHICLE_ID').item(document.getElementById('cmbAPP_USE_VEHICLE_ID').selectedIndex).value;
				
				if((valueCOM == "11339" || valueCOM == "11338" || valueCOM == "11871") && VehicleUse == "11333")					
				{
					document.getElementById('trIsSuspended_COM').style.display='inline';
					document.getElementById('cmbIS_SUSPENDED_COM').options.selectedIndex = 1;
					document.getElementById('rfvIS_SUSPENDED_COM').setAttribute("enabled",true);
				}
				else
				{
					document.getElementById('trIsSuspended_COM').style.display='none';
					document.getElementById('cmbIS_SUSPENDED_COM').options.selectedIndex = -1;
					document.getElementById('rfvIS_SUSPENDED_COM').setAttribute("enabled",false);
				}	
			}	
		}
		
		// Display/Hide Snowplow Conditions, Miles to work
		function fxnDispSnowPlow()
		{
			if(document.getElementById('cmbVEHICLE_USE'))
			{
				cmbValue = document.getElementById("cmbVEHICLE_USE").options[document.getElementById("cmbVEHICLE_USE").selectedIndex].value;
				
				if(cmbValue == '11272') // Snowplow Conditions
				{
					document.getElementById('capSNOWPLOW_CONDS').style.display = 'inline';
					document.getElementById('cmbSNOWPLOW_CONDS').style.display = 'inline';
					document.getElementById('spnSNOWPLOW_CONDS').style.display = 'inline';
					document.getElementById('rfvSNOWPLOW_CONDS').setAttribute('isValid',true);
					document.getElementById('rfvSNOWPLOW_CONDS').setAttribute('enabled',true);
					document.getElementById('rowMILES_TO_WORK').style.display = 'none';
					document.getElementById('txtMILES_TO_WORK').value = '';
					document.getElementById('rfvMILES_TO_WORK').setAttribute('isValid',false);
					document.getElementById('rfvMILES_TO_WORK').setAttribute('enabled',false);					
					document.getElementById('rfvAUTO_CAR_POOL').setAttribute('isValid',false);
					document.getElementById('rfvAUTO_CAR_POOL').setAttribute('enabled',false);		
					//Range Validator
					document.getElementById('rngMILES_TO_WORK').setAttribute('isValid',false);
					document.getElementById('rngMILES_TO_WORK').setAttribute('enabled',false);
											
				}
				else if (cmbValue == '11270') // Drive to Work/School
				{
					
					document.getElementById('rowMILES_TO_WORK').style.display = 'inline';
					document.getElementById('rfvMILES_TO_WORK').setAttribute('isValid',true);
					document.getElementById('rfvMILES_TO_WORK').setAttribute('enabled',true);					
					document.getElementById('rfvAUTO_CAR_POOL').setAttribute('isValid',true);
					document.getElementById('rfvAUTO_CAR_POOL').setAttribute('enabled',true);							
					document.getElementById('capSNOWPLOW_CONDS').style.display = 'none';
					document.getElementById('cmbSNOWPLOW_CONDS').style.display = 'none';
					document.getElementById('spnSNOWPLOW_CONDS').style.display = 'none';
					document.getElementById('rfvSNOWPLOW_CONDS').setAttribute('isValid',false);
					document.getElementById('rfvSNOWPLOW_CONDS').setAttribute('enabled',false);
					document.getElementById('rfvSNOWPLOW_CONDS').style.display = 'none';
					//Range Validator
					document.getElementById('rngMILES_TO_WORK').setAttribute('isValid',true);
					document.getElementById('rngMILES_TO_WORK').setAttribute('enabled',true);	
					fxnShowCarPool();//Added for Itrack Issue 5564
				}
				else
				  {

					document.getElementById('capSNOWPLOW_CONDS').style.display = 'none';
					document.getElementById('cmbSNOWPLOW_CONDS').style.display = 'none';
					document.getElementById('spnSNOWPLOW_CONDS').style.display = 'none';
					document.getElementById('rfvSNOWPLOW_CONDS').setAttribute('isValid',false);
					document.getElementById('rfvSNOWPLOW_CONDS').setAttribute('enabled',false);
					document.getElementById('rowMILES_TO_WORK').style.display = 'none';
					document.getElementById('txtMILES_TO_WORK').value = '';
					document.getElementById('rfvMILES_TO_WORK').setAttribute('isValid',false);
					document.getElementById('rfvMILES_TO_WORK').setAttribute('enabled',false);
					document.getElementById('rfvAUTO_CAR_POOL').setAttribute('isValid',false);
					document.getElementById('rfvAUTO_CAR_POOL').setAttribute('enabled',false);
					//Range Validator
					document.getElementById('rngMILES_TO_WORK').setAttribute('isValid',false);
					document.getElementById('rngMILES_TO_WORK').setAttribute('enabled',false);	
							
				}
			}
			else
			{

				document.getElementById('capSNOWPLOW_CONDS').style.display = 'none';
				document.getElementById('cmbSNOWPLOW_CONDS').style.display = 'none';
				document.getElementById('spnSNOWPLOW_CONDS').style.display = 'none';
				document.getElementById('rfvSNOWPLOW_CONDS').setAttribute('isValid',false);
				document.getElementById('rfvSNOWPLOW_CONDS').setAttribute('enabled',false);
				document.getElementById('rowMILES_TO_WORK').style.display = 'none';
				document.getElementById('txtMILES_TO_WORK').value = '';
				document.getElementById('rfvMILES_TO_WORK').setAttribute('isValid',false);
				document.getElementById('rfvMILES_TO_WORK').setAttribute('enabled',false);								
				document.getElementById('rfvAUTO_CAR_POOL').setAttribute('isValid',false);
				document.getElementById('rfvAUTO_CAR_POOL').setAttribute('enabled',false);
				//Range Validator
				document.getElementById('rngMILES_TO_WORK').setAttribute('isValid',false);
				document.getElementById('rngMILES_TO_WORK').setAttribute('enabled',false);	
							
			}
		}
		
		function fxnShowCarPool()
		{ 

				txtValue = document.getElementById('txtMILES_TO_WORK').value;
				if(txtValue > 25)
				{document.getElementById('capCAR_POOL').style.display = 'inline';
				document.getElementById('cmbCAR_POOL').style.display = 'inline';
				document.getElementById('spnCAR_POOL').style.display = 'inline';								
				document.getElementById('rfvAUTO_CAR_POOL').setAttribute('isValid',true);
				document.getElementById('rfvAUTO_CAR_POOL').setAttribute('enabled',true);}
				else
				{

				document.getElementById('capCAR_POOL').style.display = 'none';
				document.getElementById('cmbCAR_POOL').style.display = 'none';
				document.getElementById('spnCAR_POOL').style.display = 'none';				
				document.getElementById('rfvAUTO_CAR_POOL').style.display='none';				
				document.getElementById('rfvAUTO_CAR_POOL').setAttribute('isValid',false);
				document.getElementById('rfvAUTO_CAR_POOL').setAttribute('enabled',false);}
			
		}
		
		function fxnShwPolNo()
		{
			if(document.getElementById("hidCalledFrom").value!='<%=CALLED_FROM_UMBRELLA.ToUpper()%>' && document.getElementById('cmbAPP_USE_VEHICLE_ID').selectedIndex!=-1 && document.getElementById('cmbAPP_USE_VEHICLE_ID').item(document.getElementById('cmbAPP_USE_VEHICLE_ID').selectedIndex).text=="Personal" && document.getElementById('cmbMULTI_CAR') && document.getElementById('cmbMULTI_CAR').selectedIndex != -1)
			{	
				cmbValue = document.getElementById('cmbMULTI_CAR').options[document.getElementById('cmbMULTI_CAR').selectedIndex].value;
				
				if(cmbValue == '11920')  // Other policy with Wolverine
					{

						document.getElementById('capAUTO_POL_NO').style.display='inline';
						document.getElementById('txtAUTO_POL_NO').style.display='inline';
						document.getElementById('spnAUTO_POL_NO').style.display='inline';
					//	document.getElementById('rfvAUTO_POL_NO').style.display='inline';
						document.getElementById('rfvAUTO_POL_NO').setAttribute('IsValid',true);
						document.getElementById('rfvAUTO_POL_NO').setAttribute('enabled',true);
						ApplyColor();
						ChangeColor();
					}
				else
					{
						document.getElementById('capAUTO_POL_NO').style.display='none';
						document.getElementById('txtAUTO_POL_NO').style.display='none';
						document.getElementById('spnAUTO_POL_NO').style.display='none';
						document.getElementById('txtAUTO_POL_NO').value = '';
						document.getElementById('rfvAUTO_POL_NO').style.display='none';
						document.getElementById('rfvAUTO_POL_NO').setAttribute('IsValid',false);
						document.getElementById('rfvAUTO_POL_NO').setAttribute('enabled',false);
					}
			}
			else
			{
				document.getElementById('capAUTO_POL_NO').style.display='none';
				document.getElementById('txtAUTO_POL_NO').style.display='none';
				document.getElementById('spnAUTO_POL_NO').style.display='none';
				document.getElementById('txtAUTO_POL_NO').value = '';
				document.getElementById('rfvAUTO_POL_NO').style.display='none';
				document.getElementById('rfvAUTO_POL_NO').setAttribute('IsValid',false);
				document.getElementById('rfvAUTO_POL_NO').setAttribute('enabled',false);
				
			}
						
		}
		function showQuesForMichigan()
		{
			VehicleUse="";
			//Done by Sibin on 29 Jan 09 fo Itrack Issue 5373
			if (document.getElementById('cmbAPP_USE_VEHICLE_ID'))
			VehicleUse = document.getElementById('cmbAPP_USE_VEHICLE_ID').item(document.getElementById('cmbAPP_USE_VEHICLE_ID').selectedIndex).text;
			if(document.getElementById('hidState').value == "22" && VehicleUse == "Commercial")
			{
				document.getElementById('rowCOVERED_BY_WC_INSU').style.display='inline';
				document.getElementById('rfvCOVERED_BY_WC_INSU').setAttribute('IsValid',true);
				document.getElementById('rfvCOVERED_BY_WC_INSU').setAttribute('enabled',true);
			}
			else
			{
				document.getElementById('rowCOVERED_BY_WC_INSU').style.display='none';
				document.getElementById('rfvCOVERED_BY_WC_INSU').setAttribute('IsValid',false);
				document.getElementById('rfvCOVERED_BY_WC_INSU').setAttribute('enabled',false);
			}
		}
		function ShowClassCode()
		{  
			if(document.getElementById('cmbCommClass').selectedIndex!=-1)
			{ 
				var str = document.getElementById('cmbCommClass').options[document.getElementById('cmbCommClass').selectedIndex].value;
				str = str.split('~')[1];
				if(str!=undefined)
					SelectDropdownOptionByText(document.getElementById('cmbAPP_VEHICLE_COMCLASS_ID'),str);
				combo = document.getElementById('cmbAPP_VEHICLE_COMCLASS_ID');				
				if(combo.selectedIndex!=-1 && combo.options[combo.selectedIndex].value!='' && (document.getElementById('cmbAPP_VEHICLE_COMTYPE_ID').value!="11340" || document.getElementById('cmbAPP_VEHICLE_COMTYPE_ID').value!="11341"))
					document.getElementById('hidAPP_VEHICLE_COMCLASS_ID').value = combo.options[combo.selectedIndex].value;
			}
		} 
		function ShowComClass()
		{
			var strclass = document.getElementById('cmbAPP_VEHICLE_COMCLASS_ID').value;			
			if(document.getElementById('cmbAPP_VEHICLE_COMTYPE_ID').selectedIndex!=-1)
			{
				if(document.getElementById('cmbAPP_VEHICLE_COMTYPE_ID').value=="11340" || document.getElementById('cmbAPP_VEHICLE_COMTYPE_ID').value=="11341")
				{
					document.getElementById('cmbAPP_VEHICLE_COMCLASS_ID').value="";
					document.getElementById('hidAPP_VEHICLE_COMCLASS_ID').value="";
				}
				else
				{
				   document.getElementById('cmbAPP_VEHICLE_COMCLASS_ID').value=strclass;
				}
			}
		}
		function ShowPerClass()
		{
				document.getElementById('hidVEHICLE_TYPE_PER').value = document.getElementById('cmbAPP_VEHICLE_PERCLASS_ID').value;				
				if(document.getElementById('cmbAPP_VEHICLE_PERTYPE_ID').value=="11337" || document.getElementById('cmbAPP_VEHICLE_PERTYPE_ID').value=="11870")
					{
						document.getElementById('cmbAPP_VEHICLE_PERCLASS_ID').value="";
					}
				else
					{
						
					}
		}
		function SetPerClass()
		{		
				if(document.getElementById('cmbAPP_VEHICLE_PERTYPE_ID').value!="11337" && document.getElementById('cmbAPP_VEHICLE_PERTYPE_ID').value!="11870")
					{
						document.getElementById('cmbAPP_VEHICLE_PERCLASS_ID').value=document.getElementById('hidVEHICLE_TYPE_PER').value ;
					}
					else
					{
					document.getElementById('cmbAPP_VEHICLE_PERCLASS_ID').value="";
					}
		}
		function setTrailerclass()
		{
		
			if(document.getElementById('cmbAPP_VEHICLE_PERTYPE_ID').value=="11337" || document.getElementById('cmbAPP_VEHICLE_PERTYPE_ID').value=="11870")
					{
						document.getElementById('cmbAPP_VEHICLE_PERCLASS_ID').value="" ;
					}
		}
		function HideFieldsForUmb()
		{
			if(document.getElementById("hidCalledFrom").value=='<%=CALLED_FROM_UMBRELLA.ToUpper()%>')
			{
				document.getElementById("trMULTI_CAR").style.display="none";
				/*document.getElementById("capMULTI_CAR").style.display="none";				
				document.getElementById("capAUTO_POL_NO").style.display="none";
				document.getElementById("spnAUTO_POL_NO").style.display="none";
				document.getElementById("txtAUTO_POL_NO").style.display="none";*/
				
				EnableValidator("rfvAUTO_POL_NO",false);				
			//	document.getElementById("capSAFETY_BELT").style.display="none";								
			//	document.getElementById("cmbSAFETY_BELT").style.display="none";												
			}
		}
		function SeatBeltDisplay()
		{
			if(document.getElementById("hidState").value=="14")//Indiana State						
				document.getElementById("trPASSIVE_SEAT_BELT").style.display = "none";
			else
				document.getElementById("trPASSIVE_SEAT_BELT").style.display = "inline";
		}
		function Init()
		{
		 
			setamount();
			populateXML();			
			HideFieldsForUmb();					
			DisplayVehicleClassType();
			shwBUSS_PERM_RESI();
			shwIS_SUSPENDED();	
			populateXML();//Function moved up by Sibin for Itrack Issue 5331 on 22 Jan 09		
			CheckValidateVIN();
			fxnShowCarPool();
			fxnShwPolNo();
			SeatBeltDisplay();
			ApplyColor();
			GetZipForState();
			ChangeColor();
			setTrailerclass();
		SetValidators();
		}
		
		function SetValidators()
		{
		    if(document.getElementById('txtTERRITORY')!=null)
		    {
		        if(document.getElementById('txtTERRITORY')!='')
		          document.getElementById('rfvTERRITORY').setAttribute('IsValid',true);
		    }
		}
		
		//Added DefaultVehicleUse() by Sibin on Itrack Issue 4884 on 2 Jan 08
		function DefaultVehicleUse()
		{
		  var VehicleUse,VehicleType;
			
			if(document.getElementById('cmbAPP_USE_VEHICLE_ID').selectedIndex==-1) return;
			VehicleUse = document.getElementById('cmbAPP_USE_VEHICLE_ID').options[document.getElementById('cmbAPP_USE_VEHICLE_ID').selectedIndex].value;
			VehicleType = document.getElementById("cmbAPP_VEHICLE_PERTYPE_ID").options[document.getElementById("cmbAPP_VEHICLE_PERTYPE_ID").selectedIndex].value;
			//Used to set default value to Drive to Work/School when the vehicle type is Suspended/PPA/Customized Van or Truck and when the vehicle use is personal and vehicle type is of Motorhome(11336), Trailer(11337)or Camper & Travel Trailers(11870)
			//Added by Sibin on 02 jan 09 for Itrack Issue 4958
			if(VehicleUse=="11332" && (VehicleType=="11618" || VehicleType=="11334" || VehicleType=="11335"))
			{	
				document.getElementById('cmbVEHICLE_USE').selectedIndex=4;
			}
				
			else if(VehicleUse=="11332" && (VehicleType=="11869" || VehicleType=="11870" || VehicleType=="11868" || VehicleType=="11336" || VehicleType=="11337"))
			{
			    document.getElementById('cmbVEHICLE_USE').selectedIndex=5;
			}
		}
		
		function ActivateDeactivateVehicle()
		{
	
		        var POLICY_VEHICLE_COUNT = document.getElementById('hidVEHICLE_COUNT').value;
		        var jsmsg = document.getElementById('hidJAVASCRIPT_MSG').value;
		        var msg=jsmsg.split(',');
		        var ret;		        
		      if(document.getElementById('hidIS_ACTIVE').value == 'Y'){
		            if(parseInt(POLICY_VEHICLE_COUNT)>1)
		            {
		              //ret = confirm("Vehicle also will remove from remuneration")
		              ret = confirm(msg[1]);
		              
		            }else
		            {
		                //alert("Can not delete/deactivate this vehicle. you have at least 1 vehicle")
		                alert(msg[0]);
		                ret=false;
		            }
		        }
		     else if(document.getElementById('hidIS_ACTIVE').value == 'N'){
		                ret = true;
		        }
		    return ret;
		}
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="APP_VEHICLES" onsubmit="setamount();" method="post" runat="server">
		<P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>			
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
					</tr>
					<tr>
						<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
					</tr>
					<TR id="trBody" runat="server">
						<TD>
							<TABLE width="100%" align="center" border="0">
								<TBODY>
									<tr>
										<TD class="pageHeader" colSpan="4"><webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>Please 
											note that all fields marked with * are mandatory
										</TD>
									</tr>
									<!-- added by vj to show the vehicle use, vehicle type and vehicle class -->
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capUSE_VEHICLE" runat="server">Vehicle Use</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbAPP_USE_VEHICLE_ID" runat="server" onchange="DisplayVehicleClassType();"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvUSE_VEHICLE" runat="server" ControlToValidate="cmbAPP_USE_VEHICLE_ID" ErrorMessage="Please select vehicle use."
												Display="Dynamic"></asp:requiredfieldvalidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capCommClass" Runat="Server">Class Description</asp:label></TD>
										<TD class="midcolora" width="32%">
										<asp:dropdownlist 
											id="cmbCommClass" 										
											onChange="ShowToolTip1(true,this);javascript:ShowClassCode();" 
											onMouseOver="ShowToolTip1(true,this);" 
											onMouseOut="ShowToolTip1(false,this);"
											Runat="server" 
											Width="250">
										</asp:dropdownlist>
										</TD>
									</tr>
									<tr id="rowVehiclePersonal">
										<TD class="midcolora" width="18%" style="HEIGHT: 23px"><asp:label id="capCLASS_PER" runat="server">Class</asp:label></TD>
										<TD class="midcolora" width="32%" style="HEIGHT: 23px"><asp:dropdownlist id="cmbAPP_VEHICLE_PERCLASS_ID" Runat="server"></asp:dropdownlist></TD>
										<TD class="midcolora" width="18%" style="HEIGHT: 23px"><asp:label id="capVEHICLE_TYPE_PER" runat="server">Vehicle Type</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%" style="HEIGHT: 23px"><asp:dropdownlist id="cmbAPP_VEHICLE_PERTYPE_ID" onfocus="SelectComboIndex('cmbAPP_VEHICLE_PERTYPE_ID')"
												runat="server" onblur="CheckValidateVIN();"></asp:dropdownlist><br>
											<asp:RequiredFieldValidator id="rfvAPP_VEHICLE_PERTYPE_ID" runat="server" Display="Dynamic" ErrorMessage="Please Select Vehicle Type"
												ControlToValidate="cmbAPP_VEHICLE_PERTYPE_ID"></asp:RequiredFieldValidator></TD>
									</tr>
									<tr id="trIsSuspended">
										<TD class="midcolora" width="18%"><asp:label id="capIS_SUSPENDED" runat="server">Is Suspended?</asp:label><span id="spnIS_SUSPENDED" class="mandatory">*</span></TD>
										<td class="midcolora" colSpan="3"><asp:dropdownlist id="cmbIS_SUSPENDED" onfocus="SelectComboIndex('cmbIS_SUSPENDED')" Runat="server"></asp:dropdownlist>
											<asp:RequiredFieldValidator id="rfvIS_SUSPENDED" runat="server" Display="Dynamic" ErrorMessage="Please Select Is Suspended?"
																ControlToValidate="cmbIS_SUSPENDED"></asp:RequiredFieldValidator>
										</td>
									</tr>									
									<tr id="rowBUSS_PERM_RESI">
										<TD class="midcolora" width="18%"><asp:label id="capBUSS_PERM_RESI" runat="server">Is this Vehicle used for Business or Permanent Residence?</asp:label><span id="spnBUSS_PERM_RESI" class="mandatory">*</span></TD>
										<td class="midcolora" colspan="3"><asp:DropDownList ID="cmbBUSS_PERM_RESI" Runat="server" onfocus="SelectComboIndex('cmbBUSS_PERM_RESI')"></asp:DropDownList>
											<asp:RequiredFieldValidator id="rfvBUSS_PERM_RESI" runat="server" Display="Dynamic" ErrorMessage="Please Select used for Business or Permanent Residence."
												ControlToValidate="cmbBUSS_PERM_RESI"></asp:RequiredFieldValidator>
										</td>
									</tr>
									<tr id="rowVehicleCommercial">
										<TD class="midcolora" width="18%" style="HEIGHT: 23px"><asp:label id="capCLASS_COM" runat="server">Class</asp:label></TD>
										<TD class="midcolora" width="32%" style="HEIGHT: 23px"><asp:dropdownlist id="cmbAPP_VEHICLE_COMCLASS_ID" Runat="server"></asp:dropdownlist></TD>
										<TD class="midcolora" width="18%" style="HEIGHT: 23px"><asp:label id="capVEHICLE_TYPE_COM" runat="server">Vehicle Type</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%" style="HEIGHT: 23px"><asp:dropdownlist id="cmbAPP_VEHICLE_COMTYPE_ID" onfocus="SelectComboIndex('cmbAPP_VEHICLE_COMTYPE_ID')"
											onchange=ShowClassCode(),ShowComClass(); runat="server"></asp:dropdownlist><br>
											<asp:RequiredFieldValidator id="rfvAPP_VEHICLE_COMTYPE_ID" runat="server" Display="Dynamic" ErrorMessage="Please Select Vehicle Type"
												ControlToValidate="cmbAPP_VEHICLE_COMTYPE_ID"></asp:RequiredFieldValidator></TD>
									</tr>
									<!-- end -->
									<!--ADDED PRAVEEN KUMAR -->
									<tr id="trIsSuspended_COM">
										<TD class="midcolora" width="18%"><asp:label id="capIS_SUSPENDED_COM" runat="server">Is Suspended?</asp:label><span id="spnIS_SUSPENDED_COM" class="mandatory">*</span></TD>
										<td class="midcolora" colSpan="3"><asp:dropdownlist id="cmbIS_SUSPENDED_COM" onfocus="SelectComboIndex('cmbIS_SUSPENDED_COM')" Runat="server"></asp:dropdownlist>
										<asp:RequiredFieldValidator id="rfvIS_SUSPENDED_COM" runat="server" Display="Dynamic" ErrorMessage="Please Select Is Suspended?"
												ControlToValidate="cmbIS_SUSPENDED_COM"></asp:RequiredFieldValidator>
										</td>
									</tr>
									<!-- END -->
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capVIN" runat="server">VIN</asp:label><span class="mandatory" id="spanVIN">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtVIN" runat="server" size="25" maxlength="17" Onblur="setUpperCase();"><%--Removed onkeyup,onkeypress,onkeydown events for itrack issue 5411--%></asp:textbox><IMG id="imgLookUpWindow"  style="CURSOR:hand"  onclick="ShowLookUpWindow();" src="/cms/cmsweb/images<%=GetColorScheme()%>/calender.gif"><br>
											<asp:regularexpressionvalidator id="revHEXA_DECIMAL" runat="server" Display="Dynamic" ControlToValidate="txtVIN"></asp:regularexpressionvalidator>
											<asp:requiredfieldvalidator id="rfvVIN" ControlToValidate="txtVIN" ErrorMessage="Please select/enter VIN Number."
												Display="Dynamic" Runat="server"></asp:requiredfieldvalidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capINSURED_VEH_NUMBER" runat="server">Insured Vehicle Number</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%">
										
										<asp:textbox class="midcolora" id="txtINSURED_VEH_NUMBER" runat="server" style="display:none;" size="20" maxlength="8"
												BorderStyle="None" ></asp:textbox>
												<asp:Label runat="server" ID="lblINSURED_VEH_NUMBER"></asp:Label>
												<input type="hidden" runat="server" id="hidINSURED_VEH_NUMBER" />
												<BR>
											<asp:requiredfieldvalidator id="rfvINSURED_VEH_NUMBER" runat="server" ControlToValidate="txtINSURED_VEH_NUMBER"
												ErrorMessage="INSURED_VEH_NUMBER can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capVEHICLE_YEAR" runat="server">Year</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtVEHICLE_YEAR" runat="server" size="4" maxlength="4" onChange="checkForSymbol();"></asp:textbox><BR>
											<asp:requiredfieldvalidator id="rfvVEHICLE_YEAR" runat="server" ControlToValidate="txtVEHICLE_YEAR" ErrorMessage="VEHICLE_YEAR can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator><asp:rangevalidator id="rngVEHICLE_YEAR" ControlToValidate="txtVEHICLE_YEAR" Display="Dynamic" Runat="server"
												Type="Integer" MinimumValue="1900"></asp:rangevalidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capMAKE" runat="server">Make of Vehicle</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtMAKE" runat="server" size="32" maxlength="28"></asp:textbox><BR>
											<asp:requiredfieldvalidator id="rfvMAKE" runat="server" ControlToValidate="txtMAKE" ErrorMessage="MAKE can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capMODEL" runat="server">Model of Vehicle</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtMODEL" runat="server" size="20" maxlength="28"></asp:textbox><BR>
											<asp:requiredfieldvalidator id="rfvMODEL" runat="server" ControlToValidate="txtMODEL" ErrorMessage="MODEL can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capBODY_TYPE" runat="server">Body Type</asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtBODY_TYPE" runat="server" size="20" maxlength="28"></asp:textbox></TD>
									</tr>
									<tr>
										<TD class="headerEffectSystemParams" colSpan="4">Garage Location
										</TD>
									</tr>
									<tr>
										<TD class="midcolora"><asp:label id="capPullClientAddress" runat="server">Would you like to pull the customer address</asp:label></TD>
						</TD>
						<TD class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnPullClientAddress" runat="server" Text="Pull Customer Address"
								CausesValidation="False"></cmsb:cmsbutton></TD>
					</TR>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="capGRG_ADD1" runat="server">Address 1</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtGRG_ADD1" runat="server" size="40" maxlength="100"></asp:textbox><br>
							<asp:requiredfieldvalidator id="rfvGRG_ADD1" runat="server" ControlToValidate="txtGRG_ADD1" ErrorMessage="Address can't be blank."
								Display="Dynamic"></asp:requiredfieldvalidator></TD>
						<TD class="midcolora" width="18%"><asp:label id="capGRG_ADD2" runat="server">Address 2</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtGRG_ADD2" runat="server" size="40" maxlength="60"></asp:textbox></TD>
					</tr>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="capGRG_CITY" runat="server">City</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtGRG_CITY" runat="server" size="20" maxlength="35"></asp:textbox><br>
							<asp:requiredfieldvalidator id="rfvGRG_CITY" runat="server" ControlToValidate="txtGRG_CITY" ErrorMessage="City can't be blank."
								Display="Dynamic"></asp:requiredfieldvalidator></TD>
						<TD class="midcolora" width="18%"><asp:label id="capGRG_COUNTRY" runat="server">Country</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbGRG_COUNTRY" onfocus="SelectComboIndex('cmbGRG_COUNTRY')" runat="server">
								<ASP:LISTITEM Value="0"></ASP:LISTITEM>
							</asp:dropdownlist><br>
						</TD>
					</tr>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="capGRG_STATE" runat="server">State</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbGRG_STATE" onfocus="SelectComboIndex('cmbGRG_STATE')" runat="server" onchange="javascript:SetRegisteredState();">
								<ASP:LISTITEM Value="0"></ASP:LISTITEM>
							</asp:dropdownlist><br>
							<asp:requiredfieldvalidator id="rfvGRG_STATE" runat="server" ControlToValidate="cmbGRG_STATE" ErrorMessage="Please select/enter state."
								Display="Dynamic"></asp:requiredfieldvalidator></TD>
						<TD class="midcolora" width="18%"><asp:label id="capGRG_ZIP" runat="server">Zip</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtGRG_ZIP" runat="server" size="12" maxlength="10"></asp:textbox>
						<%-- Added by Swarup on 30-mar-2007 --%>
								<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
								<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
								</asp:hyperlink>
								<BR>
							<asp:regularexpressionvalidator id="revGRG_ZIP" runat="server" ControlToValidate="txtGRG_ZIP" ErrorMessage="RegularExpressionValidator"
								Display="Dynamic"></asp:regularexpressionvalidator><asp:requiredfieldvalidator id="rfvGRG_ZIP" runat="server" ControlToValidate="txtGRG_ZIP" ErrorMessage="ZIP can't be blank."
								Display="Dynamic"></asp:requiredfieldvalidator>
							<asp:CustomValidator Enabled=False ID="csvGRG_ZIP" ClientValidationFunction="ChkResult" Runat="server" ControlToValidate="txtGRG_ZIP"
													Display="Dynamic" ErrorMessage="Zip does not belong to the specifed state."></asp:CustomValidator>
								</TD>
					</tr>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="capREGISTERED_STATE" runat="server">Registered State</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREGISTERED_STATE" onfocus="SelectComboIndex('cmbREGISTERED_STATE')" runat="server">
								<ASP:LISTITEM Value="0"></ASP:LISTITEM>
							</asp:dropdownlist>
							<asp:requiredfieldvalidator id="rfvREGISTERED_STATE" runat="server" Display="Dynamic" ErrorMessage="Please select Registered State."
								ControlToValidate="cmbREGISTERED_STATE"></asp:requiredfieldvalidator>
							</TD>
						<TD class="midcolora" width="18%"><asp:label id="capTERRITORY" runat="server">Territory</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%">
						<asp:textbox id="txtTERRITORY" runat="server" size="6" maxlength="5"  ReadOnly="true"></asp:textbox>
					  <input type="hidden" runat="server" id="hidTERRITORY" name="hidTERRITORY" />
							<!--<IMG id="imgSelect" style="CURSOR: hand" src="../../cmsweb/images/selecticon.gif" runat="server">--><BR>
							<asp:requiredfieldvalidator id="rfvTERRITORY" runat="server" ControlToValidate="txtTERRITORY" ErrorMessage=""
								Display="Dynamic"></asp:requiredfieldvalidator>
								<asp:regularexpressionvalidator id="revTERRITORY" ControlToValidate="txtTERRITORY" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator></TD>
					</tr>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="capST_AMT_TYPE" runat="server">Amount Type</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbST_AMT_TYPE" onfocus="SelectComboIndex('cmbST_AMT_TYPE')" runat="server"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbST_AMT_TYPE')"></A></TD>
						<TD class="midcolora" width="18%"><asp:label id="capAMOUNT" runat="server">Amount</asp:label><span class="mandatory" id="spnamount">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtAMOUNT" runat="server" CssClass="INPUTCURRENCY" size="19" maxlength="9"></asp:textbox><br>
							<asp:requiredfieldvalidator id="rfvAMOUNT" runat="server" ControlToValidate="txtAMOUNT" ErrorMessage="Please enter amount."
								Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revAMOUNT" ControlToValidate="txtAMOUNT" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator></TD>
					</tr>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="capSYMBOL" runat="server">Symbol</asp:label><span class="mandatory" id="spnSymbol">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtSYMBOL" runat="server" size="6" maxlength="4"></asp:textbox><br>
							<asp:RequiredFieldValidator id="rfvSYMBOL" runat="server" Display="Dynamic" ControlToValidate="txtSYMBOL"></asp:RequiredFieldValidator>
							<asp:regularexpressionvalidator id="revSYMBOL" ControlToValidate="txtSYMBOL" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>
							<asp:customvalidator id="csvVALID_SYMBOL" ControlToValidate="txtSYMBOL" Display="Dynamic" Runat="server"
							ErrorMessage="Invalid Symbol Assignment. Can not rate." ClientValidationFunction="CustomValidateSymbol"></asp:customvalidator>
							</TD>
						<TD class="midcolora" width="18%"><asp:label id="capVEHICLE_AGE" runat="server">Age</asp:label></TD>
						<TD class="midcolora" width="32%">
							<P>
							<input type="hidden" runat="server" id="hidVEHICLE_AGE" name="hidVEHICLE_AGE" />
							<asp:textbox class="midcolora" id="txtVEHICLE_AGE" runat="server" size="20" maxlength="4" BorderStyle="None"
									ReadOnly="true"></asp:textbox><br>
								<asp:label id="lblAge" Runat="server"></asp:label><br>
								
								<asp:regularexpressionvalidator id="revVEHICLE_AGE" runat="server" ControlToValidate="txtVEHICLE_AGE" ErrorMessage="RegularExpressionValidator"
									Display="Dynamic"></asp:regularexpressionvalidator></P>
						</TD>
					</tr>
					<!--Personal Vehicle Info Section -->
					<tbody id="tbPersonal">
						<tr>
							<TD class="headerEffectSystemParams" colSpan="4">Personal Vehicle Info</TD>
						</tr>
						<tr>
							<TD class="midcolora" width="18%"><asp:label id="capIS_OWN_LEASE" runat="server">Own</asp:label></TD>
							<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbIS_OWN_LEASE" onfocus="SelectComboIndex('cmbIS_OWN_LEASE')" runat="server">
									<ASP:LISTITEM></ASP:LISTITEM>
									<ASP:LISTITEM Value="0">Leased</ASP:LISTITEM>
									<ASP:LISTITEM Value="1">Purchased</ASP:LISTITEM>
								</asp:dropdownlist></TD>
							<TD class="midcolora" width="18%"><asp:label id="capPURCHASE_DATE" runat="server">Date</asp:label></TD>
							<TD class="midcolora" width="32%"><asp:textbox id="txtPURCHASE_DATE" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkPURCHASE_DATE" runat="server" CssClass="HotSpot">
									<ASP:IMAGE id="imgPURCHASE_DATE" runat="server" ImageUrl="../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
								</asp:hyperlink><BR>
								<asp:regularexpressionvalidator id="revPURCHASE_DATE" runat="server" ControlToValidate="txtPURCHASE_DATE" Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvPURCHASE_DATE" ControlToValidate="txtPURCHASE_DATE" Display="Dynamic" Runat="server"
									ClientValidationFunction="ChkDate"></asp:customvalidator></TD>
						</tr>
						<tr>
							<TD class="midcolora" width="18%"><asp:label id="capIS_NEW_USED" runat="server">New</asp:label></TD>
							<TD class="midcolora" width="32%" colSpan="3"><asp:dropdownlist id="cmbIS_NEW_USED" onfocus="SelectComboIndex('cmbIS_NEW_USED')" runat="server">
									<ASP:LISTITEM></ASP:LISTITEM>
									<ASP:LISTITEM Value="0">New</ASP:LISTITEM>
									<ASP:LISTITEM Value="1">Used</ASP:LISTITEM>
								</asp:dropdownlist></TD>
						</tr>
						<tr>
							<TD class="midcolora" width="18%"><asp:label id="capVEHICLE_USE" runat="server">Use</asp:label><span class="mandatory">*</span></TD>
							<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbVEHICLE_USE" onfocus="SelectComboIndex('cmbVEHICLE_USE')" runat="server"
									onchange="fxnDispSnowPlow();"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbVEHICLE_USE')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
								<br>
								<asp:requiredfieldvalidator id="rfvVEHICLE_USE" runat="server" ControlToValidate="cmbVEHICLE_USE" ErrorMessage="Use can't be blank."
									Display="Dynamic"></asp:requiredfieldvalidator></TD>
							<TD class="midcolora" width="18%"><asp:label id="capSNOWPLOW_CONDS" runat="server">Snowplow conditions</asp:label><span class="mandatory" id="spnSNOWPLOW_CONDS">*</span></TD>
							<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSNOWPLOW_CONDS" onfocus="SelectComboIndex('cmbSNOWPLOW_CONDS')" runat="server"></asp:dropdownlist>
							<br><asp:requiredfieldvalidator id="rfvSNOWPLOW_CONDS" Display="Dynamic" ControlToValidate="cmbSNOWPLOW_CONDS" Runat="server" Enabled="False"></asp:requiredfieldvalidator></TD>
						<tr id="rowMILES_TO_WORK">
							<TD class="midcolora" width="18%"><asp:label id="capMILES_TO_WORK" runat="server">Miles Each Way</asp:label><span class="mandatory" id="spnMILES_TO_WORK">*</span></TD>
							<TD class="midcolora" width="32%"><asp:textbox id="txtMILES_TO_WORK" runat="server" size="4" maxlength="3" onblur="fxnShowCarPool();"></asp:textbox><br>
								<asp:RequiredFieldValidator ID="rfvMILES_TO_WORK" ControlToValidate="txtMILES_TO_WORK" Display="Dynamic" Runat="server"
									Enabled="False"></asp:RequiredFieldValidator>
								<asp:rangevalidator id="rngMILES_TO_WORK" ControlToValidate="txtMILES_TO_WORK" Display="Dynamic" Runat="server"
									Type="Integer" MinimumValue="1" MaximumValue="9999"></asp:rangevalidator>
							</TD>
							<TD class="midcolora" width="18%"><asp:label id="capCAR_POOL" runat="server">Used in Car Pool?</asp:label><span class="mandatory" id="spnCAR_POOL">*</span></TD>
							<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCAR_POOL" onfocus="SelectComboIndex('cmbCAR_POOL')" runat="server"></asp:dropdownlist>
							<br><asp:requiredfieldvalidator id="rfvAUTO_CAR_POOL" Display="Dynamic" ControlToValidate="cmbCAR_POOL" Runat="server" Enabled="False"></asp:requiredfieldvalidator></TD>
						</tr>
						<tr id="trMULTI_CAR">
							<TD class="midcolora" width="18%"><asp:label id="capMULTI_CAR" runat="server">Multi Car</asp:label></TD>
							<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbMULTI_CAR" onfocus="SelectComboIndex('cmbMULTI_CAR')" runat="server" onchange="fxnShwPolNo();"></asp:dropdownlist></TD>
							<TD class="midcolora" width="18%"><asp:label id="capAUTO_POL_NO" runat="server">Auto Policy #</asp:label><span class="mandatory" id="spnAUTO_POL_NO">*</span></TD>
							<TD class="midcolora" width="32%"><asp:textbox id="txtAUTO_POL_NO" Runat="server" size="13" MaxLength="10"></asp:textbox><br>
								<asp:requiredfieldvalidator id="rfvAUTO_POL_NO" ControlToValidate="txtAUTO_POL_NO" Display="Dynamic" Runat="server"></asp:requiredfieldvalidator></TD>
						</tr>
						<tr id="trPASSIVE_SEAT_BELT">
							<TD class="midcolora" width="18%"><asp:label id="capPASSIVE_SEAT_BELT" runat="server">Seat</asp:label></TD>
							<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPASSIVE_SEAT_BELT" onfocus="SelectComboIndex('cmbPASSIVE_SEAT_BELT')" runat="server">
									<ASP:LISTITEM></ASP:LISTITEM>
								</asp:dropdownlist>
								<!--<A class="calcolora" href="javascript:showPageLookupLayer('cmbPASSIVE_SEAT_BELT')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>-->
							</TD>
							<td colspan="2" class="midcolora"></td>
							
						</tr>
						<tr>
							<TD class="midcolora" width="18%"><asp:label id="capANTI_LOCK_BRAKES" runat="server">Lock</asp:label></TD>
							<TD class="midcolora" width="32%" ><asp:dropdownlist id="cmbANTI_LOCK_BRAKES" onfocus="SelectComboIndex('cmbANTI_LOCK_BRAKES')" runat="server"></asp:dropdownlist></TD>
							<TD class="midcolora" width="18%"><asp:label id="capAIR_BAG" runat="server">Bag</asp:label></TD>
							<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbAIR_BAG" onfocus="SelectComboIndex('cmbAIR_BAG')" runat="server"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbAIR_BAG')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
							</TD>
						<%--<TD class="midcolora" width="18%"><asp:label id="capSAFETY_BELT" runat="server">Lock</asp:label></TD>
							<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSAFETY_BELT" onfocus="SelectComboIndex('cmbSAFETY_BELT')" runat="server"></asp:dropdownlist></TD>--%>
						</tr>
						<tr>
							<TD class="midcolora" width="18%"><asp:label id="capANNUAL_MILEAGE" runat="server">Mileage</asp:label></TD>
							<TD class="midcolora" width="32%" colspan="3"><asp:textbox id="txtANNUAL_MILEAGE" CssClass="InputCurrency" runat="server" size="8" maxlength="6"></asp:textbox><br>
								<%--<asp:regularexpressionvalidator id="revANNUAL_MILEAGE" Enabled="False" ControlToValidate="txtANNUAL_MILEAGE" Display="Dynamic"
									Runat="server"></asp:regularexpressionvalidator>--%><asp:rangevalidator id="rngANNUAL_MILEAGE" ControlToValidate="txtANNUAL_MILEAGE" Display="Dynamic" Runat="server"
									Type="Currency" MinimumValue="1" MaximumValue="999999"></asp:rangevalidator></TD>
						</tr>
					</tbody>
					
					<tbody id="tbCommercial">
						<!-- COMMERCIAL INFO -->
						<TR>
							<TD class="headerEffectSystemParams" colSpan="4">Commercial Info</TD>
						</TR>
						<TR>
							<TD class="midcolora" width="18%"><asp:Label ID="capRADIUS_OF_USE" Runat="server"></asp:Label><SPAN class="mandatory">*</SPAN></TD>
							<TD class="midcolora" width="32%"><asp:textbox id="txtRADIUS_OF_USE" runat="server" maxlength="4" size="5"></asp:textbox><BR>
								<asp:requiredfieldvalidator id="rfvRADIUS_OF_USE" Display="Dynamic" ControlToValidate="txtRADIUS_OF_USE" Runat="server"></asp:requiredfieldvalidator>
								<asp:rangevalidator id="rngRADIUS_OF_USE" Display="Dynamic" ControlToValidate="txtRADIUS_OF_USE" Runat="server"
									MaximumValue="9999" MinimumValue="1" Type="Integer"></asp:rangevalidator>
							</TD>
							<TD class="midcolora" width="18%"><asp:Label ID="capTRANSPORT_CHEMICAL" Runat="server"></asp:Label><SPAN class="mandatory">*</SPAN></TD>
							<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbTRANSPORT_CHEMICAL" onfocus="SelectComboIndex('cmbTRANSPORT_CHEMICAL')" runat="server"></asp:dropdownlist><br>
								<asp:requiredfieldvalidator id="rfvTRANSPORT_CHEMICAL" Display="Dynamic" ControlToValidate="cmbTRANSPORT_CHEMICAL"
									Runat="server"></asp:requiredfieldvalidator></TD>
						</TR>
						<TR id="rowCOVERED_BY_WC_INSU">
							<TD class="midcolora" width="18%"><asp:Label ID="capCOVERED_BY_WC_INSU" Runat="server"></asp:Label><SPAN class="mandatory">*</SPAN></TD>
							<TD class="midcolora" width="32%" colspan="3"><asp:dropdownlist id="cmbCOVERED_BY_WC_INSU" onfocus="SelectComboIndex('cmbCOVERED_BY_WC_INSU')" runat="server"></asp:dropdownlist><br>
								<asp:requiredfieldvalidator id="rfvCOVERED_BY_WC_INSU" Display="Dynamic" ControlToValidate="cmbCOVERED_BY_WC_INSU"
									Runat="server"></asp:requiredfieldvalidator></TD>
						</TR>
					</tbody>
					<%--<TR id="row1" runat="server" width="100%">
							
					<TD class="midcolora" width="32%"><asp:label id="capUNINS_MOTOR_INJURY_COVE" runat="server"></asp:label></TD>
					<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbUNINS_MOTOR_INJURY_COVE" onfocus="SelectComboIndex('cmbUNINS_MOTOR_INJURY_COVE')"
							runat="server"></asp:dropdownlist></TD>
								<TD class="midcolora" width="32%"><asp:label id="capUNINS_PROPERTY_DAMAGE_COVE" runat="server"></asp:label></TD>
					<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbUNINS_PROPERTY_DAMAGE_COVE" onfocus="SelectComboIndex('cmbUNINS_PROPERTY_DAMAGE_COVE')"
							runat="server"></asp:dropdownlist></TD>
				</TR>
				<tr id="row2" runat="server" width="100%">
				    <TD class="midcolora" width="32%"><asp:label id="capUNDERINS_MOTOR_INJURY_COVE" runat="server"></asp:label></TD>
					<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbUNDERINS_MOTOR_INJURY_COVE" onfocus="SelectComboIndex('cmbUNDERINS_MOTOR_INJURY_COVE')"
							runat="server"></asp:dropdownlist></TD>
					
					<td class="midcolora" colSpan="2"></td>
				</tr>
				</tr>--%>
					<tr>
						<td class="midcolora" align="left" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" causesvalidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnCopy" runat="server" Text="Copy" visible="false" causesvalidation="false"></cmsb:cmsbutton>
							<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"
								causesvalidation="false"></cmsb:cmsbutton>
						</td>
						<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" causesvalidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
					</tr>
			</TABLE>
			</TD></TR></TBODY></TABLE><INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
			<INPUT id="hidVehicleID" type="hidden" value="0" name="hidVehicleID" runat="server">
			<INPUT id="hidAPPID" type="hidden" value="0" name="hidAPPID" runat="server"> <INPUT id="hidAppVersionID" type="hidden" value="0" name="hidAppVersionID" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			<INPUT id="hidMakeCode" type="hidden" value="0" name="hidMakeCode" runat="server">
			<INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
			<INPUT id="hidAPP_LOB" type="hidden" value="0" name="hidAPP_LOB" runat="server">
			<INPUT id="hidCheckZipSubmit" type="hidden" name="hidCheckZipSubmit" runat="server">
			<INPUT id="hidANTI_LCK_BRAKES" type="hidden" name="hidANTI_LCK_BRAKES" runat="server">
			<INPUT id="hidVIN" type="hidden" name="hidVIN" runat="server"> <INPUT id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server">
			<INPUT id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server">
			<INPUT id="hidSymbolCheck" type="hidden" name="hidSymbolCheck" runat="server" value="0">
			<INPUT id="hidAlert" type="hidden" name="hidAlert" runat="server" value="0"> <INPUT id="hidVehCount" type="hidden" value="0" name="hidVehCount" runat="server">
			<INPUT id="hidSetIndex" type="hidden" value="0" name="hidSetIndex" runat="server">
			<INPUT id="hidState" type="hidden" value="" name="hidState" runat="server">
			<INPUT id="hidAPP_EFFECTIVE_YEAR" type="hidden" name="hidAPP_EFFECTIVE_YEAR" runat="server">
			<INPUT id="hidAPP_EFFECTIVE_MONTH" type="hidden" name="hidAPP_EFFECTIVE_MONTH" runat="server">
			<INPUT id="hidAPP_VEHICLE_COMCLASS_ID" type="hidden" name="hidAPP_VEHICLE_COMCLASS_ID" runat="server" value="">
			<INPUT id="hidVEHICLE_TYPE_PER" type="hidden" name="hidVEHICLE_TYPE_PER" runat="server">
			<INPUT id="hidVEHICLE_COUNT" type="hidden" name="hidVEHICLE_COUNT" runat="server">
			<INPUT id="hidJAVASCRIPT_MSG" type="hidden" name="hidJAVASCRIPT_MSG" runat="server">
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none; Z-INDEX: 101; POSITION: absolute"><iframe id="lookupLayerIFrame" style="DISPLAY: none; Z-INDEX: 1001; FILTER: alpha(opacity=0); BACKGROUND-COLOR: #000000"
				width="0" height="0" left="0px" top="0px;"></iframe>
		</div>
		<DIV id="lookupLayer" onmouseover="javascript:refreshLookupLayer();" style="Z-INDEX: 101; VISIBILITY: hidden; POSITION: absolute">
			<table class="TabRow" cellSpacing="0" cellPadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b>Add LookUp</b></td>
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
		
		if (document.getElementById("hidFormSaved").value == "5")
		{
				
			RefreshWebGrid("1","1",false,"1"); 
			/*Record deleted*/
			/*Refreshing the grid and coverting the form into add mode*/
			/*Using the javascript*/
			RemoveTab(5,top.frames[1]);	
			RemoveTab(4,top.frames[1]);	
			RemoveTab(3,top.frames[1]);	
			RemoveTab(2,top.frames[1]);	
			
			document.getElementById('hidVehicleID').value = "NEW";
				
		  }
		  else
		  {
			//Done for Itrack Issue 5609 on 3 Aug 09
			//RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidVehicleID').value);
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidVehicleID').value,true);	
		  }		  
		</script>
	</BODY>
</HTML>

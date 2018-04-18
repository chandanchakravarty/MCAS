<%@ Page language="c#" Codebehind="AddClaimsNotification.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddClaimsNotification" validateRequest=false %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Claim Notification</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="vbscript">		
			Function getUserConfirmationForActivity				
				getUserConfirmationForActivity= msgbox("An incomplete Activity exists, do you wish to complete it??",35,"CMS - Complete Activity")
			End function
			Function DisplayMessage(msg)				
				DisplayMessage = msgbox(msg,64,"CMS - Claims Notification")
			End function
		</script>
		<script language="javascript">		
		
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";						
			var LossDateReturnValue=0;
			var temp=0;
		
			//Stop THIRD PARTY ADJUSTER from adding claims (Added by Asfa 31-Aug-2007)
			function GetAdjusterData(objSource , objArgs)
			{
			
				//Structure of the encoded string is as follows : "ADJUSTER_ID + '^' + ADJUSTER_CODE"
				combo=document.getElementById("cmbADJUSTER_ID");
				if(combo.SelectedIndex== -1) return ;
				encoded_string = new String(combo.options[combo.selectedIndex].value);
				if(encoded_string.length<1)return;
				array=encoded_string.split('^');
				document.getElementById("hidADJUSTER_ID").value=array[0];
				document.getElementById("hidADJUSTER_CODE").value=array[1];
				if(array[1]=="" || array[1]=="0")
				{
					objArgs.IsValid = false;
				}
				else
				{
					objArgs.IsValid = true;
				}
			}
			
			function EnableDiaryUpdate()
			{
				 combo= document.getElementById("cmbCLAIM_STATUS");
				 if(document.getElementById("cmbCLAIM_STATUS").SelectedIndex == -1) return;
				 var Claim_Status = combo.options[combo.selectedIndex].value;
				 
				 if(Claim_Status == '11740') // Closed
				 {
					//document.getElementById('txtDIARY_DATE').readOnly = true ;
					document.getElementById('txtDIARY_DATE').disabled = true ;
					//document.getElementById('imgDIARY_DATE').disabled = true;
					document.getElementById('imgbtnUpdateDiary').disabled=true;
				 }
				 else
				 { 
					//document.getElementById('txtDIARY_DATE').readOnly=false;
					//document.getElementById('imgDIARY_DATE').disabled=false;
					document.getElementById('txtDIARY_DATE').disabled = false ;
					document.getElementById('imgbtnUpdateDiary').disabled=false;
				 }
			}
			
			// Stops the cmbCATASTROPHE_EVENT_CODE to be refreshed on click of Save button - (Added by Asfa - 31/Aug/2007)
			function cmbCATASTROPHE_Changed()
			{
				combo= document.getElementById("cmbCATASTROPHE_EVENT_CODE");
				if(document.getElementById("cmbCATASTROPHE_EVENT_CODE").SelectedIndex == 0) return;
				document.getElementById("hidCATASTROPHE_EVENT_CODE").value= combo.options[combo.selectedIndex].value;
				
				
			}
			
			function AlertMessage()
			{
				if(document.getElementById("hidMessage").value!="")
				{
					alert(document.getElementById("hidMessage").value);
					document.getElementById("txtLOSS_DATE").focus();
					return false;
				}
				return true;
			}
			function OpenNotesWindow()
			{
				var NotesHandle;				
				var strPolicy = document.getElementById("hidPOLICY_ID").value + '-' + document.getElementById("hidPOLICY_VERSION_ID").value + '-POL';
				if(NotesHandle==null)
					NotesHandle = window.open ("/cms/client/Aspx/CustomerNotesIndex.aspx?&CalledFrom=CLAIMS&ClaimsPopUp=1&SelectedPolicy=" + strPolicy,"Notes","resizable=no,scrollbars=no,width=950,height=600,left=125,top=125");
				
					//CalledFrom=CLAIMS&ClaimsPopUp=1
				
			
			}
			function OpenClaimsLookup()
			{				
				var ExistingClaimNumList = document.getElementById('hidCLAIM_ID').value;				
				document.getElementById('hidLINKED_CLAIM_ID_LIST').value = replaceAll(document.getElementById('hidLINKED_CLAIM_ID_LIST').value,",","^");				
				SelectClaimWindow = window.open ("SelectLinkedClaimsPopUp.aspx?ExistingClaimID=" + document.getElementById('hidCLAIM_ID').value + "&ExistingClaimNumList=" + document.getElementById('hidLINKED_CLAIM_ID_LIST').value,"SelectClaimWindow","resizable=no,scrollbars=yes,width=620,height=400,left=150,top=150");
				//SelectClaimWindow = window.open ("SelectLinkedClaimsPopUp.aspx?ExistingClaimID=" + document.getElementById('hidCLAIM_ID').value + ,"SelectClaimWindow","resizable=no,scrollbars=yes,width=620,height=400,left=150,top=150");
			}
			function CreateLinkedClaimsLink(PageLoad,ClaimsList)
			{
		
			
				if(!PageLoad && ClaimsList==document.getElementById('hidLINKED_CLAIM_LIST').value)
					return;
				else
					document.getElementById('hidLINKED_CLAIM_LIST').value = ClaimsList;
					
			     //ADDED BY SANTOSH GAUTAM ON 12 Nov 2010
			     // Concept : I set LITIGATION_FILE value in hidOLD_LITIGATION_FILE and hidCURRENT_LITIGATION_FILE when the page first time
			     //           and when user save record then i fill the current value of LITIGATION_FILE in hidCURRENT_LITIGATION_FILE
			     //           if both the value are not equals then reload the claim tab
			     var strOLD_LITIGATION_FILE= document.getElementById('hidOLD_LITIGATION_FILE').value;	
			     var strCURRENT_LITIGATION_FILE= document.getElementById('hidCURRENT_LITIGATION_FILE').value;	
			     
			     var strOLD_IS_VICTIM_CLAIM= document.getElementById('hidOLD_IS_VICTIM_CLAIM').value;	
			     var strCURRENT_IS_VICTIM_CLAIM= document.getElementById('hidCURRENT_IS_VICTIM_CLAIM').value;	
			     
			    
			     if(strOLD_LITIGATION_FILE!=strCURRENT_LITIGATION_FILE || strOLD_IS_VICTIM_CLAIM!=strCURRENT_IS_VICTIM_CLAIM)
			     {
				 
				    strURL = "ClaimsTab.aspx?CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value + "&POLICY_ID=" + document.getElementById("hidPOLICY_ID").value + "&POLICY_VERSION_ID=" + document.getElementById("hidPOLICY_VERSION_ID").value + "&LOB_ID="+ document.getElementById("hidLOB_ID").value + "&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ADD_NEW=1&RECR_VEH=" + document.getElementById("hidRECR_VEH").value + "&HOMEOWNER=" + document.getElementById("hidHOMEOWNER").value  + "&IN_MARINE=" + document.getElementById("hidIN_MARINE").value + "&";				
				    this.parent.location.href = strURL;
				 }
				  
				 	
				var strLinkedClaims = new String("");						
				//When the page has not loaded and there is no change to the linked columns, no need to create links again, lets leave				
				//if(!PageLoad && ClaimsList==document.getElementById('hidLINKED_CLAIM_LIST').value)
				//	return;
				if(ClaimsList!=null && ClaimsList!='')
				{
					var strClaimsList = new String(ClaimsList);					
					if(strClaimsList.split('~').length>0)
					{
						//document.getElementById('divLINKED_TO_CLAIM').innerHTML = "";
						var strLinks = new String();
						//var strTemp = new String(document.getElementById('divLINKED_TO_CLAIM').innerHTML);
						for(var i=0;i<strClaimsList.split('~').length;i++)
						{
							//We will get the data in the form of "~CLAIM_NUMBER^CUSTOMER_ID^POLICY_ID^POLICY_VERSION_ID^CLAIM_ID^LOB_ID^HOMEOWNER^RECR_VEH^IN_MARINE^LOSS_DATE^LITIGATION_FILE"
							var strClaimsData = new String(strClaimsList.split('~')[i]);							
							var strCLAIM_NUMBER = new String(strClaimsData.split('^')[0]);
							//if(strTemp!='' && strTemp.search(strCLAIM_NUMBER)!='-1')							
							//	continue;							
							var strCUSTOMER_ID = new String(strClaimsData.split('^')[1]);
							var strPOLICY_ID = new String(strClaimsData.split('^')[2]);
							var strPOLICY_VERSION_ID = new String(strClaimsData.split('^')[3]);
							var strCLAIM_ID = new String(strClaimsData.split('^')[4]);
							var strLOB_ID = new String(strClaimsData.split('^')[5]);
							var strADD_NEW= "0";
							var strHOMEOWNER = new String(strClaimsData.split('^')[6]);
							var strRECR_VEH = new String(strClaimsData.split('^')[7]);
							var strIN_MARINE = new String(strClaimsData.split('^')[8]);
							var strLOSS_DATE = new String(strClaimsData.split('^')[9]);
							
							var strLITIGATION_FILE = new String(strClaimsData.split('^')[10]);
							
							var strDIARY_ID =  "0";
							var strLink = "ClaimsTab.aspx?&CUSTOMER_ID=" + strCUSTOMER_ID + "&POLICY_ID=" + strPOLICY_ID + "&POLICY_VERSION_ID=" + strPOLICY_VERSION_ID + "&CLAIM_ID=" + strCLAIM_ID + "&LOB_ID=" + strCLAIM_ID + "&ADD_NEW=" + strADD_NEW + "&HOMEOWNER=" + strHOMEOWNER + "&RECR_VEH=" + strRECR_VEH +"&IN_MARINE=" + strIN_MARINE + +"&LITIGATION_FILE=" + strLITIGATION_FILE +"&transferdata=";							
							//var ClaimHyperLink = "<a onClick='GoToLinkedClaim(" + strCUSTOMER_ID + "," + strPOLICY_ID + "," + strPOLICY_VERSION_ID + "," + strCLAIM_ID + "," + strLOB_ID + "," + strADD_NEW + "," + strHOMEOWNER + "," + strRECR_VEH + "," + strIN_MARINE + ")'; href=#>" + strCLAIM_NUMBER + "</a>";
							var ClaimHyperLink = "<a onClick=GoToLinkedClaim('" + strLink + "'); href='#'>" + strCLAIM_NUMBER + "</a>";							
							strLinks += "," + ClaimHyperLink ;
							strLinkedClaims+='^' + strCLAIM_ID;
						}
						if(strLinks=='')
							return;
							
						strLinks = strLinks.substring(1,strLinks.length);				
					
							
						document.getElementById('divLINKED_TO_CLAIM').innerHTML = strLinks;						
					}
				}	
				if(strLinkedClaims!="" && strLinkedClaims.length>0)
					strLinkedClaims = strLinkedClaims.substring(1,strLinkedClaims.length);
				document.getElementById('hidLINKED_CLAIM_ID_LIST').value = strLinkedClaims;				
			}			
			
			function AddClaimant()
			{
				top.botframe.location.href = "PartyIndex.aspx?&ADD_NEW=1&";
				return false;
			}
			function GoToReserve()
			{
				top.botframe.location.href = "ReserveTab.aspx?ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value+"&CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value + "&POLICY_ID=" + document.getElementById("hidPOLICY_ID").value + "&POLICY_VERSION_ID=" + document.getElementById("hidPOLICY_VERSION_ID").value+"&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&CALLED_FROM=NTF&LOB_ID="+ document.getElementById("hidLOB_ID").value+"&"; //Action on Payment-New Reserve
				return false;
			}
			
			/*function DisableInsuredRelation()
			{
				if(document.getElementById("cmbCLAIMANT_INSURED").options[document.getElementById("cmbCLAIMANT_INSURED").selectedIndex].text=='Yes')
					HideInsuredRelationship(true);	
				else
					HideInsuredRelationship(false);	
			}*/	
			
			/*function HideInsuredRelationship(flag)
			{
				if(flag) //Hide the field when true is passed
				{
					document.getElementById("txtINSURED_RELATIONSHIP").style.display= "none";
					document.getElementById("capINSURED_RELATIONSHIP").style.display= "none";
				}
				else //Show the fields when false is passed
				{
					document.getElementById("txtINSURED_RELATIONSHIP").style.display= "inline";
					document.getElementById("capINSURED_RELATIONSHIP").style.display= "inline";
				}
			}*/
			
			
			/*******************************************************************
			'*	Function Name	:	CheckLossDate() 
			'*	Type			:   JavaScript Function
			'*	Author			:	Praveen Kasana
			'*	Parameters		:   
			'*	Purpose			:   This function is used to validate Loss date 
			'*						Itrack 6276 .
			'*	Creation Date	:   20 August,2009
			'*	Returns			:    
			'*******************************************************************/		
			function CheckLossDate()
			{
				try
				{
					
					
					if(document.getElementById("txtLOSS_DATE").value!="")
					{
							
						var expdate=document.getElementById("txtLOSS_DATE").value;
						var lossHour = document.getElementById("txtLOSS_HOUR").value;
						var lossMinute = document.getElementById("txtLOSS_MINUTE").value;
						var MERIDIEM = document.getElementById("cmbMERIDIEM").selectedIndex;
											
						var custID = document.getElementById('hidCUSTOMER_ID').value;
						var polID = document.getElementById('hidPOLICY_ID').value;
						var polVerId = document.getElementById('hidPOLICY_VERSION_ID').value;
					
						//using AJAX Calls to Validate the Loss Date
						var result = AddClaimsNotification.CheckLossDateAgainstPolicy(custID,polID,polVerId,expdate,lossHour,lossMinute,MERIDIEM);
						//alert(retVal.value)						
						
						var retVal = result.value;
						
						if(retVal == 0)	
						{
							LossDateReturnValue=0;
							return false;
						}
						else if(retVal == -1)
						{
							LossDateReturnValue=-1;						
							return false;
						}
						else if(retVal == -2)
						{
							LossDateReturnValue=-2;
							return false;
						}
						else if (retVal == -3)
						{
							LossDateReturnValue=-3;
							return false;
						}
						else if (retVal == -5)
						{
							LossDateReturnValue=-5;
							return false;
						}
						else if (retVal == -8)
						{
							LossDateReturnValue=-8;
							return false;
						}
						// SOME CLAIM WITH SAME LOSS DATE ALREADY EXISTS FOR PROVIDED POLICY	
						else if (retVal == -15 && (document.getElementById('hidCLAIM_ID').value =="0" ||document.getElementById('hidCLAIM_ID').value==""))
						{
							LossDateReturnValue=-15;
							if(temp==1)
							  return confirm(document.getElementById('hidCLAIM_EXISTS_CONFIRM_MSG').value);
							else
							  alert(document.getElementById('hidCLAIM_EXISTS_ALERT_MSG').value);
							
						}
						else if (retVal == -4) {return true;}
							
						else if(retVal > 0)
						{
							//DisplayMessage("A different version of the Policy has been selected!.");
							polVerId = retVal;
							//Move to this Version
							strURL = "ClaimsTab.aspx?CUSTOMER_ID=" + custID + "&POLICY_ID=" + polID + "&POLICY_VERSION_ID=" + polVerId + "&LOB_ID="+ document.getElementById("hidLOB_ID").value + "&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ADD_NEW=0&RECR_VEH=" + document.getElementById("hidRECR_VEH").value + "&HOMEOWNER=" + document.getElementById("hidHOMEOWNER").value  + "&IN_MARINE=" + document.getElementById("hidIN_MARINE").value + "&LOSS_DATE=" + document.getElementById("txtLOSS_DATE").value + "&";
							this.parent.location.href = strURL;
							return false;
														
						}		
					}		
						
						
					
				}
				catch(ex)
				{	
					alert('Something went Wrong,Please try again');
					return false;
				}	
				return true;
			}
		
			
		
			
			function ValidateLength(objSource , objArgs)
			{
				if(document.getElementById('txtCLAIM_DESCRIPTION').value.length>1000)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}		
			function BtnSaveClick()
			{
			
				temp=1;
			}			
			function ChkLOSS_DATE(objSource , objArgs)			
			{
		
		
				document.getElementById("txtLOSS_DATE").value = FormatDateForGrid(document.getElementById("txtLOSS_DATE"),'');
				var expdate=document.getElementById("txtLOSS_DATE").value;
				objArgs.IsValid = CheckLossDate();//DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);				
				if(LossDateReturnValue!=-15)
				{
			        if(objArgs.IsValid ==false )
			        {
			        
			        
			            if(LossDateReturnValue == 0)							
							objSource.innerHTML=document.getElementById('hidCANCELLED_POLICY_MESSAGE_1').value;							
						else if(LossDateReturnValue == -1)						
							objSource.innerHTML=document.getElementById('hidCANCELLED_POLICY_MESSAGE_2').value;												
//						else if(LossDateReturnValue == -2)
//							objSource.innerHTML="Loss Date does not exists, Please try other date!";							
						else if (LossDateReturnValue == -3)
							objSource.innerHTML=document.getElementById('hidNO_CORRESSPOND_DATE_MESSAGE').value;
						else if (LossDateReturnValue == -5)
							objSource.innerHTML=document.getElementById('hidAS400_MESSAGE').value;
					    else if (LossDateReturnValue == -8 )
							objSource.innerHTML=document.getElementById('hidLOSSDATE_FUTUREDATE_ERRORMESSAGE').value;	
						else
						   objSource.innerHTML=document.getElementById('hidLOSSDATE_COMMON_ERRORMESSAGE').value;
						
						}
			     }
			     else
			     {
			      objSource.innerHTML="";
			      
			     }
			      
//				if(document.getElementById("rfvLOSS_DATE").style.display !="inline" && document.getElementById("revLOSS_DATE").style.display !="inline" && objArgs.IsValid==true && (document.getElementById("hidCLAIM_ID").value=="0" || document.getElementById("hidCLAIM_ID").value==""))
//				{					
//					//__doPostBack('LossDateBlur','0');	
//								
//				}				
			}			
		
				
		

			function ChkDIARY_DATE(objSource , objArgs)
			{
				var expdate=document.CLM_CLAIM_INFO.txtDIARY_DATE.value;
				var nextDate = new Date();

				nextDate.setMonth(nextDate.getMonth() + <%=DIARY_DATE_ADD_MONTHS%>);
				sNextDate = new String(nextDate.getMonth() + 1) + "/" + new String(nextDate.getDate()) + "/" + new String(nextDate.getFullYear());						

				var firstCon = DateComparer(expdate,"<%=System.DateTime.Now%>",jsaAppDtFormat)  //Date is greater then current date
				var SecondCon = DateComparer(sNextDate,expdate,jsaAppDtFormat); //Date is less then Six Months.
				
				if ( firstCon == true && SecondCon == true)
				{
					objArgs.IsValid = true;
				}
				else
				{
					objArgs.IsValid = false;
				}
				
			}		
			function AddData()
			{
				ChangeColor();
				DisableValidators();					
				document.getElementById('hidCLAIM_ID').value		=	'New';
				document.getElementById('txtCLAIM_NUMBER').value	=	'';
				var CurrentDate = new Date();	
				document.getElementById('txtLOSS_DATE').value = CurrentDate.getMonth()+1 + '/' + CurrentDate.getDate(); + '/' + CurrentDate.getFullYear();
				
				
			}			
			
			function ResetTheForm()
			{temp=0;
				DisableValidators();
				document.CLM_CLAIM_INFO.reset();				
				//DisableInsuredRelation();
				ChangeColor();				
				document.getElementById('txtLOSS_DATE').focus();				
				return false;
			}
			
			function CheckIncompleteActivity()
			{
				//User results of click the yes/no/cancel message box
				//6 = yes,7=no,2=cancel
				result = getUserConfirmationForActivity();						
				if(result==2) return false;
				if(result==6)
					GoToActivity();
				if(result==7)
					__doPostBack('DeactivateActivity','0')
				return false;
			}
			
			function CheckForDummyPolicy()
			{	
				return;/*
				if(document.getElementById("hidDUMMY_POLICY_ID").value!="" && document.getElementById("hidDUMMY_POLICY_ID").value!="0" && document.getElementById("hidDUMMY_POLICY_ID").value!="T")				
					document.getElementById("btnMatchPolicy").style.display="inline";									
				else				
					document.getElementById("btnMatchPolicy").style.display="none";
					
				if(document.getElementById("hidDUMMY_POLICY_ID").value!="" && document.getElementById("hidDUMMY_POLICY_ID").value!="0")
					document.getElementById("trCLAIMANT_INSURED").style.display="none";					
				else
					document.getElementById("trCLAIMANT_INSURED").style.display="inline";*/

			}	
				
			function RedirectToSearchPolicy()
			{
				top.botframe.location.href = "Policy/SearchPolicy.aspx?&BackToMatchPolicy=1&DUMMY_POLICY_ID=" + document.getElementById("hidDUMMY_POLICY_ID").value + "&DUMMY_CLAIM_NUMBER=" + document.getElementById("txtCLAIM_NUMBER").value + "&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value;
				return false;
			}
			//<a onClick='GoToLinkedClaim(928,1,1,566,2,0,0,0,0)'; href=#>0106-0011</a>
			function GoToLinkedClaim(strURL)
			{
		
				this.parent.location.href = strURL;
			}			
			function Reload_Save()
			{
			 
				strURL = "ClaimsTab.aspx?CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value + "&POLICY_ID=" + document.getElementById("hidPOLICY_ID").value + "&POLICY_VERSION_ID=" + document.getElementById("hidPOLICY_VERSION_ID").value + "&LOB_ID="+ document.getElementById("hidLOB_ID").value + "&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ADD_NEW=1&RECR_VEH=" + document.getElementById("hidRECR_VEH").value + "&HOMEOWNER=" + document.getElementById("hidHOMEOWNER").value  + "&IN_MARINE=" + document.getElementById("hidIN_MARINE").value + "&";				
				this.parent.location.href = strURL;
			}
				
			function Reload_LossDate()
			{
			 
				//alert(document.getElementById("hidCLAIM_ID").value)
				//strURL = "ClaimsTab.aspx?CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value + "&POLICY_ID=" + document.getElementById("hidPOLICY_ID").value + "&POLICY_VERSION_ID=" + document.getElementById("hidPOLICY_VERSION_ID").value + "&LOB_ID="+ document.getElementById("hidLOB_ID").value + "&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ADD_NEW=0&RECR_VEH=" + document.getElementById("hidRECR_VEH").value + "&HOMEOWNER=" + document.getElementById("hidHOMEOWNER").value  + "&IN_MARINE=" + document.getElementById("hidIN_MARINE").value + "&LOSS_DATE=" + document.getElementById("txtLOSS_DATE").value + "&";
				strURL = "ClaimsTab.aspx?CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value + "&POLICY_ID=" + document.getElementById("hidPOLICY_ID").value + "&POLICY_VERSION_ID=" + document.getElementById("hidPOLICY_VERSION_ID").value + "&LOB_ID="+ document.getElementById("hidLOB_ID").value + "&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ADD_NEW=0&RECR_VEH=" + document.getElementById("hidRECR_VEH").value + "&HOMEOWNER=" + document.getElementById("hidHOMEOWNER").value  + "&IN_MARINE=" + document.getElementById("hidIN_MARINE").value + "&LOSS_DATE=" + document.getElementById("txtLOSS_DATE").value +"&LITIGATION_FILE=" + document.getElementById("txtLITIGATION_FILE").value + "&";
				
				this.parent.location.href = strURL;
			}
				
			function DisplayActivityButtons()
			{
				var CLAIM_STATUS = document.getElementById("cmbCLAIM_STATUS").options[document.getElementById("cmbCLAIM_STATUS").selectedIndex].value;
				if(document.getElementById("hidCLAIM_ID").value=="" || document.getElementById("hidCLAIM_ID").value=="0" || document.getElementById("hidCLAIM_ID").value=="NEW")// || document.getElementById("hidReserveAdded").value=="" || document.getElementById("hidReserveAdded").value=="0")// || document.getElementById("hidReserveAdded").value=="2")
				{
					if(document.getElementById("btnAddClaimant")!=null)
						document.getElementById("btnAddClaimant").style.display="none";
				}	
				else
				{
					if(document.getElementById("btnAddClaimant")!=null)
						document.getElementById("btnAddClaimant").style.display="inline";
				}	
				
				if(document.getElementById("hidCLAIM_ID").value=="" || document.getElementById("hidCLAIM_ID").value=="0" || document.getElementById("hidCLAIM_ID").value=="NEW" || document.getElementById("hidReserveAdded").value=="" || document.getElementById("hidReserveAdded").value=="0" || document.getElementById("hidReserveAdded").value=="2") // || CLAIM_STATUS==<%=((int)enumCLAIM_STATUS.CLOSED).ToString()%> )
				{
					top.topframe.disableMenu('2,2,3');
					top.topframe.disableMenu('2,2,2');
					if(document.getElementById("btnManageTrackActivity")!=null)
						document.getElementById("btnManageTrackActivity").style.display="none";					
				}
				else
				{
					top.topframe.enableMenu('2,2,3');
					top.topframe.enableMenu('2,2,2');
					if(document.getElementById("btnManageTrackActivity")!=null)
						document.getElementById("btnManageTrackActivity").style.display="inline";
				}
				
				if(document.getElementById("hidCLAIM_ID").value=="" || document.getElementById("hidCLAIM_ID").value=="0" || document.getElementById("hidCLAIM_ID").value=="NEW")
				{
					if(document.getElementById("btnReserves"))
						document.getElementById("btnReserves").style.display="none";
					if(document.getElementById("btnReOpenClaims"))	
						document.getElementById("btnReOpenClaims").style.display="none";
					createCookie('AddNewReopenClaim','0');
					//document.getElementById("btnRescind").style.display="none";
					if(document.getElementById("btnAuthorizeTransaction")!=null)
						document.getElementById("btnAuthorizeTransaction").style.display="none";					
					top.topframe.disableMenu('2,2,4');
					document.getElementById("imgSelectPinkSlipNotifyUsers").style.display="none";
				}
				else
				{
					document.getElementById("imgSelectPinkSlipNotifyUsers").style.display="inline";
					if(document.getElementById("btnReserves"))
						document.getElementById("btnReserves").style.display="inline";					
					if(CLAIM_STATUS==<%=((int)enumCLAIM_STATUS.CLOSED).ToString()%>)
					{
						if(document.getElementById("btnReOpenClaims")!=null)
							document.getElementById("btnReOpenClaims").style.display="inline";
						createCookie('AddNewReopenClaim','1');						
					}
					else
					{
						if(document.getElementById("btnReOpenClaims")!=null)
							document.getElementById("btnReOpenClaims").style.display="none";
						createCookie('AddNewReopenClaim','0');						
					}
					
					/*if(CLAIM_STATUS==<%=((int)enumCLAIM_STATUS.RESCINDED).ToString()%>)
						document.getElementById("btnRescind").style.display="none";
					else
						document.getElementById("btnRescind").style.display="inline";*/
					
					if(document.getElementById("hidAuthorized").value!="1")
					{
						if(document.getElementById("btnAuthorizeTransaction")!=null)
							document.getElementById("btnAuthorizeTransaction").style.display="none";
						top.topframe.disableMenu('2,2,4');
					}
					else
					{
						if(document.getElementById("btnAuthorizeTransaction")!=null)
							document.getElementById("btnAuthorizeTransaction").style.display="inline";
						top.topframe.enableMenu('2,2,4');
					}
				}
				
				
				
			}
			function GoToReopenClaims()
			{
				strURL = "ReopenClaimIndex.aspx?&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&";
				this.parent.location.href = strURL;
				return false;
			}
			
			function Init()
			{ temp=0;
				ChangeColor();
				ApplyColor();				
				//CheckForDummyPolicy();
				//DisableInsuredRelation();
				DisplayActivityButtons();
				EnableDiaryUpdate();
				//Make date of loss, reported by and time of loss fields readonly in edit mode
				if(document.getElementById('hidOldData').value!='' && document.getElementById('hidOldData').value!='0')
				{
					document.getElementById('txtLOSS_DATE').readOnly = true;					
					document.getElementById('txtREPORTED_BY').readOnly = true;
					document.getElementById('txtLOSS_MINUTE').readOnly = true;
					document.getElementById('txtLOSS_HOUR').readOnly = true;
					document.getElementById('cmbMERIDIEM').disabled = true;
					CreateLinkedClaimsLink(true,document.getElementById('hidLINKED_CLAIM_LIST').value);
				}								
			}
			
			function GoToActivity()
			{
				strURL = "ActivityTab.aspx?&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&";
				this.parent.location.href = strURL;
				return false;
			}
			function GoToActivityAuthorize()
			{
				strURL = "ActivityTab.aspx?&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&AUTHORIZE=1&";
				this.parent.location.href = strURL;
				return false;
			}
			
			// Added by Asfa Praveen (04-Feb-2008) - iTrack issue #3526 Part 3
			function OpenDiary()
			{

				alert('hidLISTID ' + document.getElementById("hidLISTID").value );
				if(document.getElementById("hidLISTID").value!="0")
				{
					var strPath="/cms/cmsweb/diary/Index.aspx?LISTID=" + document.getElementById("hidLISTID").value 
									+ "&TOUSERID=" + document.getElementById("hidTOUSERID").value + "&CLAIM_NUMBER=" + document.getElementById("txtCLAIM_NUMBER").value + "&"
					top.topframe.callItemClicked('0','');
					this.parent.document.location.href = strPath;
					return false;
				}	
			}
			 function FormatZipCode(vr) {
            
           // document.getElementById('revCUSTOMER_ZIP').setAttribute('enabled', true);
            var vr = new String(vr.toString());
            if (vr != "") {

                vr = vr.replace(/[-]/g, "");
                num = vr.length;
                if (num == 8) {
                    vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
                    document.getElementById('revZIP').setAttribute('enabled', false);
                    
                }

            }

            return vr;
        }
			
			
			function ProcessKeypress() 
			{
				try
				{
					
					if(event.srcElement.type != 'textarea')
					{	
					 
						if (event.keyCode == 13) 
						{       
							Page_ClientValidate();
							if (!Page_IsValid)
							return false;
							
						
							document.getElementById('btnSave').click();
							return false;
							/*if(AlertMessage()!=false)
							{
							DisableButton(document.getElementById('btnSave'));
							}*/
						}
					 }	
				    
				}  
				catch(ex)
				{
					alert('Please Renter the Details.');
				}	
			}
		 function viewFNOLLetter() {
                var Claim_ID = document.getElementById('hidCLAIM_ID').value;
		        var Activity_ID = document.getElementById("hidACTIVITY_ID").value;
		        var POLICY_ID = document.getElementById('hidPOLICY_ID').value;
		        var POLICY_VERSION_ID = document.getElementById('hidPOLICY_VERSION_ID').value;
		        var CUSTOMER_ID = document.getElementById('hidCUSTOMER_ID').value;
		        var url = "CededCOILetterLink.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + Activity_ID + "&POLICY_ID=" + POLICY_ID + "&POLICY_VERSION_ID=" + POLICY_VERSION_ID + "&CUSTOMER_ID=" + CUSTOMER_ID;
		        ShowPopup(url, '', 900, 600);
		        return false;
		    }
			/*function DoLookUp()
			{	
				if(document.getElementById('cmbADJUSTER_ID').selectedIndex==0)
					{
						alert("Please select adjuster before selecting sub-adjuster");
						return;
					}

				combo = document.getElementById("cmbADJUSTER_ID");
				document.getElementById("hidADJUSTER_CODE").value = combo.options[combo.selectedIndex].value;

				var URL = "<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>";
				
				OpenLookup(URL,'SUB_ADJUSTER','SUB_ADJUSTER_CONTACT_NAME','txtSUB_ADJUSTER','txtSUB_ADJUSTER_CONTACT','ClaimsSubAdjuster','Claims SubAdjuster','@ADJUSTER_CODE='+ document.getElementById("hidADJUSTER_CODE").value);
				
			}*/
		</script>
	</HEAD>
	<BODY oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="Init();" >
		<FORM id="CLM_CLAIM_INFO" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td class="midcolorc" align="center" colSpan="4">
						<asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
					</tr>
					<TR id="trBody" runat="server">
						<TD>
							<TABLE width="100%" align="center" border="0">
								<TBODY>
									<tr>
										<TD class="pageHeader" colSpan="3" align="center"><asp:Label ID="capHeader" runat="server"></asp:Label></TD><%--Please note that all fields marked with * are mandatory--%>
									</tr>
									<tr>
										<td class="midcolorc" align="center" colSpan="3"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
									</tr>
									<tr>
										<TD class="midcolora" width="32%"><asp:label id="capCLAIM_NUMBER" runat="server"></asp:label><span class="mandatory">*</span>
										
										<br /><asp:textbox class="midcolora" id="txtCLAIM_NUMBER" runat="server" BorderStyle="None" ReadOnly="True"
												size="20" maxlength="20"></asp:textbox>
										</TD>
										<TD class="midcolora" width="32%"><asp:label id="capLINKED_TO_CLAIM" runat="server"></asp:label>
                                            <br />
												<IMG id="imgSelect" style="CURSOR: hand" alt="" src="../../cmsweb/images/selecticon.gif"												
													runat="server"><br />
                                            <br />
                                        <TD class="midcolora" width="32%"><asp:label id="capCLAIM_OFFICIAL_NUMBER" 
                                                runat="server"></asp:label>
                                                <br />
                                            <asp:label id="lblCLAIM_OFFICIAL_NUMBER" 
                                                runat="server"></asp:label>
                                                  <asp:Button  id="btnOFFCIAL_CLAIM_NUMBER" 
                                                runat="server" Text="Generate" 
                                                    onclick="btnOFFCIAL_CLAIM_NUMBER_Click" Visible="False" />
                                                <br />
                                                </TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%">
										<asp:label id="capLOSS_DATE" runat="server"></asp:label>
										    <span class="mandatory">*</span>
                                            <br />
										<asp:textbox id="txtLOSS_DATE" runat="server" size="12" maxlength="10"></asp:textbox>
										<asp:hyperlink id="hlkLOSS_DATE" runat="server" CssClass="HotSpot">
												<ASP:IMAGE id="imgLOSS_DATE" runat="server" ImageUrl="../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
											</asp:hyperlink>
                                            <br />
											<asp:requiredfieldvalidator id="rfvLOSS_DATE" runat="server" Display="Dynamic" ControlToValidate="txtLOSS_DATE"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revLOSS_DATE" runat="server" Display="Dynamic" ControlToValidate="txtLOSS_DATE"
												ErrorMessage=""></asp:regularexpressionvalidator>
                                            
                                            <asp:customvalidator id="csvLOSS_DATE" Display="Dynamic" ControlToValidate="txtLOSS_DATE" ClientValidationFunction="ChkLOSS_DATE"
												Runat="server" Enabled="true"></asp:customvalidator>
                                               
                                                </TD>
										<TD class="midcolora" width="32%">
                                          <asp:label id="capLOSS_TIME" runat="server"></asp:label><span class="mandatory">*</span>  
                                            <br />
										<asp:textbox id="txtLOSS_HOUR" runat="server" size="3" maxlength="2">12</asp:textbox>
										<asp:label id="lblLOSS_HOUR" runat="server">HH</asp:label>
										<asp:textbox id="txtLOSS_MINUTE" runat="server" size="3" maxlength="2">01</asp:textbox>
										<asp:label id="Label1" runat="server">MM</asp:label>
										<asp:dropdownlist id="cmbMERIDIEM" onfocus="SelectComboIndex('cmbMERIDIEM')" runat="server"></asp:dropdownlist>
                                            <br />
                                            <asp:rangevalidator id="rnvLOSS_HOUR" runat="server" Text="" Display="Dynamic"
												ControlToValidate="txtLOSS_HOUR" MinimumValue="0" MaximumValue="12" Type="Integer"></asp:rangevalidator><asp:rangevalidator id="rnvtLOSS_MINUTE" runat="server" Text="" Display="Dynamic"
												ControlToValidate="txtLOSS_MINUTE" MinimumValue="0" MaximumValue="59" Type="Integer"></asp:rangevalidator><%--Hours must be from 0 to 12.--%><%--Minutes must be from 0 to 59.--%>
                                            <asp:requiredfieldvalidator id="rfvLOSS_HOUR" runat="server" Display="Dynamic" ControlToValidate="txtLOSS_HOUR"
												ErrorMessage=""></asp:requiredfieldvalidator>
                                            <asp:requiredfieldvalidator id="rfvMERIDIEM" runat="server" Display="Dynamic" ControlToValidate="cmbMERIDIEM"
												ErrorMessage=""></asp:requiredfieldvalidator>
                                            <asp:requiredfieldvalidator id="rfvLOSS_MINUTE" runat="server" 
                                                Display="Dynamic" ControlToValidate="txtLOSS_MINUTE"
												ErrorMessage=""></asp:requiredfieldvalidator>
                                           
										    <br />
                                           
										</TD>
                                            <TD class="midcolora" width="18%"><asp:label id="capLITIGATION_FILE" runat="server"></asp:label>
                                                <br />
                                                <asp:dropdownlist id="cmbLITIGATION_FILE" onfocus="SelectComboIndex('cmbLITIGATION_FILE')" runat="server">
											</asp:dropdownlist></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%">
										    <asp:label id="capADJUSTER_CODE" runat="server"></asp:label><span class="mandatory">*</span><br />
                                            <asp:dropdownlist id="cmbADJUSTER_ID" onfocus="SelectComboIndex('cmbADJUSTER_ID')" runat="server"></asp:dropdownlist>
                                            <br />
											<asp:requiredfieldvalidator id="rfvADJUSTER_CODE" runat="server" Display="Dynamic" ControlToValidate="cmbADJUSTER_ID"
												ErrorMessage=""></asp:requiredfieldvalidator>
											<asp:customvalidator id="csvADJUSTER_CODE" Display="Dynamic" ErrorMessage="Error" ControlToValidate="cmbADJUSTER_ID"
										ClientValidationFunction="GetAdjusterData" Runat="server"></asp:customvalidator>
										    <br />
										    </TD>
										<TD class="midcolora" width="32%">
										    <asp:label id="capREPORTED_TO" runat="server"></asp:label>
                                            <br />
                                            <asp:textbox id="txtREPORTED_TO" runat="server" size="40" maxlength="50"></asp:textbox>
										    <br />
										</TD>
                                            <TD class="midcolora" width="18%">
                                                <asp:label id="capREPORTED_BY" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtREPORTED_BY" runat="server" size="40" maxlength="50"></asp:textbox>
                                                <br />
                                        </TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capCATASTROPHE_EVENT_CODE" runat="server"></asp:label>
                                            <br />
                                            <asp:dropdownlist id="cmbCATASTROPHE_EVENT_CODE" onfocus="SelectComboIndex('cmbCATASTROPHE_EVENT_CODE')"
												runat="server"></asp:dropdownlist>
                                            <br />
                                        </TD>
										<TD class="midcolora" width="32%"><asp:label id="capRECIEVE_PINK_SLIP_USERS_LIST" runat="server"></asp:label><br>
											<%--Please select ADJUSTER--%>
											<!--Added by Asfa 31-Aug-2007 -->
											<asp:ListBox id="cmbRECIEVE_PINK_SLIP_USERS_LIST" runat="server" SelectionMode="Multiple" Width="220px"></asp:ListBox>
												
												<IMG id="imgSelectPinkSlipNotifyUsers" valign="top" 
                                                style="CURSOR: hand; height: 14px;" alt="" src="../../cmsweb/images/selecticon.gif"												
													runat="server"><br />
                                            <br />
                                        </TD>
										<TD class="midcolora" width="18%"><asp:label id="capPINK_SLIP_TYPE_LIST" runat="server"></asp:label>
                                            <br />
                                            <asp:ListBox id="cmbPINK_SLIP_TYPE_LIST" runat="server" SelectionMode="Multiple" Width="220px"></asp:ListBox></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capAT_FAULT_INDICATOR" runat="server"></asp:label>
                                            <br />
                                            <asp:dropdownlist id="cmbAT_FAULT_INDICATOR" onfocus="SelectComboIndex('cmbAT_FAULT_INDICATOR')" runat="server"></asp:dropdownlist>
                                           
                                            <br />
                                           
                                        </TD>
										<TD class="midcolora" width="32%"><asp:label id="capCLAIMANT_PARTY" runat="server"></asp:label><span class="mandatory">*</span><br />
                                            <asp:dropdownlist id="cmbCLAIMANT_PARTY" onfocus="SelectComboIndex('cmbCLAIMANT_PARTY')" runat="server"></asp:dropdownlist>
                                            
											<br />
                                            
											<asp:requiredfieldvalidator id="rfvCLAIMANT_PARTY" runat="server" 
                                                Display="Dynamic" ControlToValidate="cmbCLAIMANT_PARTY"></asp:requiredfieldvalidator>
                                            <br />
                                            </TD>
										<TD class="midcolora" width="18%"><asp:label id="capNOTIFY_REINSURER" runat="server"></asp:label>
                                            <br />
                                            <asp:dropdownlist id="cmbNOTIFY_REINSURER" onfocus="SelectComboIndex('cmbNOTIFY_REINSURER')" runat="server">
											</asp:dropdownlist></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capFIRST_NOTICE_OF_LOSS" runat="server"></asp:label>
                                            <br />
                                            <asp:textbox id="txtFIRST_NOTICE_OF_LOSS" runat="server" size="12" maxlength="10"></asp:textbox>
											<asp:hyperlink id="hlkFIRST_NOTICE_OF_LOSS" runat="server" CssClass="HotSpot">
												<ASP:IMAGE id="imgFIRST_NOTICE_OF_LOSS" runat="server" ImageUrl="../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
											</asp:hyperlink>
                                            
											<br />
                                            
											<asp:regularexpressionvalidator id="revFIRST_NOTICE_OF_LOSS" runat="server" Display="Dynamic" ControlToValidate="txtFIRST_NOTICE_OF_LOSS"></asp:regularexpressionvalidator>
										    <br />
										</TD>
										<TD class="midcolora" width="32%"><asp:label id="capPOSSIBLE_PAYMENT_DATE" 
                                                runat="server"></asp:label>
                                            <br />
                                            
                                            <asp:textbox id="txtPOSSIBLE_PAYMENT_DATE" 
                                                runat="server" size="12" maxlength="10"></asp:textbox>
											<asp:hyperlink id="hlkPOSSIBLE_PAYMENT_DATE" runat="server" CssClass="HotSpot">
												<ASP:IMAGE id="imgPOSSIBLE_PAYMENT_DATE" runat="server" 
                                                ImageUrl="../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
											</asp:hyperlink>
											<br />
											<asp:regularexpressionvalidator id="revPOSSIBLE_PAYMENT_DATE" runat="server" 
                                                Display="Dynamic" ControlToValidate="txtPOSSIBLE_PAYMENT_DATE"></asp:regularexpressionvalidator>
											</TD>
										<TD class="midcolora" width="18%"><asp:label id="capLAST_DOC_RECEIVE_DATE" 
                                                runat="server"></asp:label>
                                            <br />
                                            <asp:textbox id="txtLAST_DOC_RECEIVE_DATE" 
                                                runat="server" size="12" maxlength="10"></asp:textbox>
											<asp:hyperlink id="hlkLAST_DOC_RECEIVE_DATE" runat="server" CssClass="HotSpot">
												<ASP:IMAGE id="imgLAST_DOC_RECEIVE_DATE" runat="server" 
                                                ImageUrl="../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
											</asp:hyperlink>
                                            <br />
											<asp:regularexpressionvalidator id="revLAST_DOC_RECEIVE_DATE" runat="server" 
                                                Display="Dynamic" ControlToValidate="txtLAST_DOC_RECEIVE_DATE"></asp:regularexpressionvalidator>
										</TD>
										<%--<TD class="midcolora" width="18%"><asp:label id="capADD_FAULT" runat="server"></asp:label></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbADD_FAULT" onfocus="SelectComboIndex('cmbADD_FAULT')" runat="server">
												<asp:ListItem Selected="True" Value="U">Undetermined</asp:ListItem>
												<asp:ListItem Value="N">No</asp:ListItem>
												<asp:ListItem Value="Y">Yes</asp:ListItem>
											</asp:dropdownlist></TD>--%>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capAGENCY_DISPLAY_NAME" runat="server"></asp:label>
                                            <br />
                                            <asp:label id="lblAGENCY_DISPLAY_NAME" runat="server"></asp:label>
                                           
                                        </TD>
										<TD class="midcolora" width="32%">
                                            <br />
                                            </TD>
										<TD class="midcolora" width="18%">
                                            <br />
                                            </TD>
									</tr>
									<%--Added for Itrack Issue 6620 on 27 Nov 09--%>
									<tr style="height:5px;">
										<TD width="18%" colspan="3"></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capCLAIMANT_NAME" runat="server"></asp:label>
                                            <br />
                                            <asp:textbox id="txtCLAIMANT_NAME"  Enabled="False" runat="server" maxlength="50" size="40"></asp:textbox>
                                           
                                            <br />
                                           
                                        </TD>
										<TD class="midcolora" width="32%"><asp:label id="capADDRESS1" runat="server"></asp:label>
                                            <br />
                                            <asp:textbox id="txtADDRESS1"  Enabled="False" runat="server" maxlength="50" size="40"></asp:textbox></TD>
										<TD class="midcolora" width="18%"><asp:label id="capADDRESS2" runat="server"></asp:label>
                                            <br />
                                            <asp:textbox id="txtADDRESS2"  Enabled="False"  runat="server" maxlength="50" size="40"></asp:textbox>
                                            </TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capCITY" runat="server"></asp:label>
                                            <br />
                                            <asp:textbox id="txtCITY"  Enabled="False" runat="server" maxlength="50" size="40"></asp:textbox>
                                            <br />
                                            <br />
                                        </TD>
										<TD class="midcolora" width="32%"><asp:label id="capSTATE" runat="server"></asp:label>
                                            <br />
                                            <asp:dropdownlist id="cmbSTATE"  Enabled="False" onfocus="SelectComboIndex('cmbSTATE')" runat="server"></asp:dropdownlist>
                                            <br />
                                            </TD>
										<TD class="midcolora" width="32%"><asp:label id="capCOUNTRY" runat="server"></asp:label>
                                            <br />
                                            <asp:dropdownlist id="cmbCOUNTRY"  Enabled="False"  onfocus="SelectComboIndex('cmbCOUNTRY')" runat="server"></asp:dropdownlist>
                                            <br />
                                            </TD>	
									</tr>				
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capZIP" runat="server"></asp:label>
                                            <br />
                                            <asp:textbox id="txtZIP"  Enabled="False" runat="server"  maxlength="10" size="12" OnBlur="this.value=FormatZipCode(this.value)"></asp:textbox>
                                            <br />
											<asp:regularexpressionvalidator id="revZIP" runat="server" ControlToValidate="txtZIP" Display="Dynamic" ErrorMessage=""></asp:regularexpressionvalidator>
                                            <br />
                                        </TD>
										<TD class="midcolora" width="32%"><asp:label id="capWORK_PHONE" runat="server"></asp:label>
                                            <br />
                                            <asp:textbox id="txtWORK_PHONE" runat="server" maxlength="12" size="20" onblur="FormatBrazilPhone()"></asp:textbox>
                                            <br />
											<asp:regularexpressionvalidator id="revWORK_PHONE" runat="server" ControlToValidate="txtWORK_PHONE" Display="Dynamic"
												ErrorMessage=""></asp:regularexpressionvalidator>
											</TD>
										<TD class="midcolora" width="32%"><asp:label id="capEXTENSION" runat="server"></asp:label>
                                            <br />
                                            <asp:textbox id="txtEXTENSION" runat="server" maxlength="6" size="10"></asp:textbox>
                                            <br />
										<asp:rangevalidator id="rngEXTENSION" ControlToValidate="txtEXTENSION" Display="Dynamic" Runat="server"
											Type="Integer" MinimumValue="1" MaximumValue="999999"></asp:rangevalidator>
                                            <br />
											</TD>
											</TD>
										
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capMOBILE_PHONE" runat="server"></asp:label>
                                            <br />
                                            <asp:textbox id="txtMOBILE_PHONE" runat="server" maxlength="12" size="20" onblur="FormatBrazilPhone()"></asp:textbox>
                                            <br />
											<asp:regularexpressionvalidator id="revMOBILE_PHONE" runat="server" ControlToValidate="txtMOBILE_PHONE" Display="Dynamic"
												ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator>
                                            <br />
										</TD>
										<TD class="midcolora" width="32%">
										<asp:label id="capHOME_PHONE" runat="server"></asp:label>
                                            <br />
                                            <asp:textbox id="txtHOME_PHONE" runat="server" maxlength="12" size="20" onblur="FormatBrazilPhone()"></asp:textbox>
                                            <br />
											<asp:regularexpressionvalidator id="revHOME_PHONE" runat="server" ControlToValidate="txtHOME_PHONE" Display="Dynamic"
												ErrorMessage=""></asp:regularexpressionvalidator>
                                            
											</TD>
										<TD class="midcolora" width="18%">
										    <asp:label id="capWHERE_CONTACT" runat="server"></asp:label>
                                            <br />
                                            <asp:textbox id="txtWHERE_CONTACT" runat="server" maxlength="50" size="40"></asp:textbox>
                                            <br />
                                            <br />
                                            
                                        </TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capWHEN_CONTACT" runat="server"></asp:label>
                                            <br />
                                            <asp:textbox id="txtWHEN_CONTACT" runat="server" maxlength="10" size="20"></asp:textbox>
                                            <br />
                                        </TD>
										<TD class="midcolora" width="32%">&nbsp;</TD>
										<TD class="midcolora" width="18%">&nbsp;</TD>
										</tr>
										
									<tr style="height:5px;">
										<TD colspan="3"></TD>
										</tr>
										
											<tr><TD class="midcolora" colSpan="3" width="32%">
										<b><asp:label ID="capCap" runat="server"></asp:label></b>
										</TD>
									</tr>
									
									
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capINSURED_NAME" runat="server"></asp:label>
                                            <br />
                                            <asp:label id="lblINSURED_NAME" runat="server"></asp:label>
                                           
                                            <br />
                                           
                                            <br />
                                           
                                        </TD>
										<TD class="midcolora" width="32%">
										    <asp:label id="capINSUREDADDRESS1" runat="server"></asp:label>
                                            <br />
                                            <asp:label id="lblINSUREDADDRESS1" runat="server"></asp:label>
                                           
										</TD>
										<TD class="midcolora" width="32%"><asp:label id="capINSUREDADDRESS2" runat="server"></asp:label>
                                            <br />
                                            <asp:label id="lblINSUREDADDRESS2" runat="server"></asp:label></TD>
										
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capINSUREDCITY" runat="server"></asp:label>
                                            <br />
                                            <asp:label id="lblINSUREDCITY" runat="server"></asp:label>
                                           
                                            <br />
                                           
                                            <br />
                                           
                                        </TD>
										<TD class="midcolora" width="32%"><asp:label id="capINSUREDSTATE" runat="server"></asp:label>
                                            <br />
                                            <asp:label id="lblINSUREDSTATE" runat="server"></asp:label></TD>
										<TD class="midcolora" width="18%"><asp:label id="capINSUREDCOUNTRY" runat="server"></asp:label>
                                            <br />
                                            <asp:label id="lblINSUREDCOUNTRY" runat="server"></asp:label>
                                            
                                        </TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capINSUREDZIP" runat="server"></asp:label>
                                            <br />
                                            <asp:label id="lblINSUREDZIP" runat="server"></asp:label>
                                            
                                            <br />
                                            
                                        </TD>
										<TD class="midcolora" width="32%">
                                            
                                        </TD>
										<TD class="midcolora" width="18%"><br />
                                            
									    </TD>
									</tr>
										
										<tr style="height:5px;">
										<TD colspan="3">
                                           </TD>
										<tr>
										<TD class="midcolora" width="18%">
										<asp:label id="capDIARY_DATE" runat="server"></asp:label>
                                            <br />
                                            <asp:textbox id="txtDIARY_DATE" runat="server" readonly="true" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkDIARY_DATE" runat="server" CssClass="HotSpot">
									</asp:hyperlink><asp:ImageButton id="imgbtnUpdateDiary" readonly="true" runat="server" ImageUrl="../../CmsWeb/Images/selecticon.gif" ToolTip=""></asp:ImageButton>
										</TD>
										<TD class="midcolora" width="32%"><asp:label id="capCLAIM_STATUS" runat="server"></asp:label><span class="mandatory">*</span><br />
                                            <asp:dropdownlist id="cmbCLAIM_STATUS" 
                                                onfocus="SelectComboIndex('cmbCLAIM_STATUS')" onChange="EnableDiaryUpdate();" 
                                                runat="server" Height="16px" Width="69px"></asp:dropdownlist>
                                            <br />
									<asp:requiredfieldvalidator id="rfvCLAIM_STATUS" runat="server" 
                                                ControlToValidate="cmbCLAIM_STATUS" Display="Dynamic"
									ErrorMessage=""></asp:requiredfieldvalidator>
                                            <br />
                                            </TD>
										<TD class="midcolora" width="18%"><asp:label id="capCLAIM_STATUS_UNDER" runat="server"></asp:label>
                                            <br />
											<asp:dropdownlist id="cmbCLAIM_STATUS_UNDER" onfocus="SelectComboIndex('cmbCLAIM_STATUS_UNDER')" runat="server">
											</asp:dropdownlist>
                                            </TD>
										<tr>
										<TD class="midcolora" width="32%">
                                                    <asp:label id="capOUTSTANDING_RESERVE" runat="server"></asp:label>
                                                    <br />
                                            <asp:label id="lblOUTSTANDING_RESERVE" runat="server"></asp:label>
                                                    <br />
                                            </TD>
										<TD class="midcolora" width="32%"><asp:label id="capRESINSURANCE_RESERVE" runat="server"></asp:label>
										<br />
                                            <asp:label id="lblRESINSURANCE_RESERVE" runat="server"></asp:label>
                                            
                                            <br />
                                            </TD>
										<TD class="midcolora" width="32%">
                                           
                                            <asp:label id="capPAID_LOSS" runat="server"></asp:label>
                                            <br />
                                            <asp:label id="lblPAID_LOSS" runat="server"></asp:label>
                                            
                                            <br />
                                                    <br />
                                            </TD>
									</tr>
										<tr>
										<TD class="midcolora" width="32%">
											<asp:label id="capPAID_EXPENSE" runat="server"></asp:label>
                                            <br />
                                            <asp:label id="lblPAID_EXPENSE" runat="server"></asp:label>
                                            <br />
                                            <br />
                                            </TD>
										<TD class="midcolora" width="32%"><asp:label id="capRECOVERIES" runat="server"></asp:label>
                                            <br />
                                            <asp:label id="lblRECOVERIES" runat="server"></asp:label>
                                           
                                            <br />
                                           
                                            </TD>
										<TD class="midcolora" width="32%">
                                           
                                            <asp:label id="capREINSURANCE_TYPE" 
                                runat="server"></asp:label>
                                            <br />
                                            <asp:dropdownlist id="cmbREINSURANCE_TYPE"  
                                onfocus="SelectComboIndex('cmbCOUNTRY')" runat="server"></asp:dropdownlist>
                                          
                                            <br />
                                          
                                            </TD>
									</tr>
										<tr>
										<TD class="midcolora" width="32%">
											<asp:label id="capREIN_CLAIM_NUMBER" 
                                runat="server"></asp:label>
                                            <br />
                                            <asp:textbox id="txtREIN_CLAIM_NUMBER" 
                                runat="server" maxlength="500" ></asp:textbox>
                                            <br />
                                            </TD>
										<TD class="midcolora" width="32%"><asp:label id="capREIN_LOSS_NOTICE_NUM" 
                                runat="server"></asp:label>
                                            <br />
                                            <asp:textbox id="txtREIN_LOSS_NOTICE_NUM" 
                                runat="server" maxlength="500" ></asp:textbox>
                                            <br />
                                            </TD>
										<TD class="midcolora" width="32%">
                                           
                            <asp:label id="capIS_VICTIM_CLAIM" 
                                runat="server"></asp:label>
                                                    <br />
                            <asp:dropdownlist id="cmbIS_VICTIM_CLAIM"  
                                onfocus="SelectComboIndex('cmbCOUNTRY')" runat="server"></asp:dropdownlist>
                                            <br />
                                            <br />
											</TD>
									</tr>
									</tr>
										<tr style="height:5px;">
											<TD  colspan="3" >
                                                
                                            </TD>
									</tr>
								
					<tr>
						<TD class="midcolora" width="18%">
                            <asp:label id="capCLAIM_DESCRIPTION" runat="server"></asp:label>
                            <br />
                            <asp:textbox onkeypress="MaxLength(this,1000);" 
                                id="txtCLAIM_DESCRIPTION" runat="server" maxlength="1000"
								Columns="150" TextMode="MultiLine" Rows="5" Width="316px"></asp:textbox>
                            
							<asp:customvalidator id="csvCLAIM_DESCRIPTION" ControlToValidate="txtCLAIM_DESCRIPTION" Display="Dynamic"
								ErrorMessage="" Runat="server" ClientValidationFunction="ValidateLength"></asp:customvalidator>
                        </TD>
						<TD class="midcolora" width="32%">
                            
							</TD>
						
						<TD class="midcolora" width="32%">&nbsp;</TD>
					</tr>
					
					
					<%--Added by Asfa (22-Apr-2008) - iTrack issue #3697 --%>
					<%--Current Insured Information--%>
										
					
					<%--<tr>
						<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER" runat="server"></asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtSUB_ADJUSTER" runat="server" maxlength="50" size="40" ReadOnly="True"></asp:textbox><IMG id="imgSelectAdjuster" style="CURSOR: hand" onclick="javascript:DoLookUp();" src="../../cmsweb/images/selecticon.gif"
								runat="server">
						</TD>
						<TD class="midcolora" width="18%"><asp:label id="capSUB_ADJUSTER_CONTACT" runat="server"></asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtSUB_ADJUSTER_CONTACT" runat="server" maxlength="50" size="40" ReadOnly="True"></asp:textbox></TD>
					</tr>--%>
					
					<tr>
						<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" causesvalidation="false"
								visible="False"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnManageTrackActivity" runat="server" Text="Manage/Track Activity"
								causesvalidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnCompleteActivity" runat="server" Visible="False" Text="Complete Activity"
								causesvalidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnReserves" runat="server" Text="Reserves" causesvalidation="false"></cmsb:cmsbutton>
							
							<cmsb:cmsbutton class="clsButton" id="btnMatchPolicy" runat="server" Text="Match Policy" causesvalidation="false" visible="false"></cmsb:cmsbutton></td>
						<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnAuthorizeTransaction" runat="server" Text="Authorize Transaction"
								causesvalidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" OnClientClick="BtnSaveClick();" Text="Save"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnAddClaimant" runat="server" Text="Parties" causesvalidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnReOpenClaims" runat="server" Text="Re-Open Claim" causesvalidation="false"></cmsb:cmsbutton><%--<cmsb:cmsbutton class="clsButton" id="btnRescind" runat="server" Text="Rescind" causesvalidation="false"></cmsb:cmsbutton>--%>
                                 <asp:Button class="clsButton" id="btnFNOL" runat="server" Text="" CausesValidation="False" /></td>

					</tr>
			</TABLE>
			</TD></TR></TBODY></TABLE><INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			<INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			<INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
			<INPUT id="hidDUMMY_POLICY_ID" type="hidden" value="0" name="hidDUMMY_POLICY_ID" runat="server">
			<INPUT id="hidLOSS_DATE_MATCHED" type="hidden" value="2" name="hidLOSS_DATE_MATCHED" runat="server">
			<INPUT id="hidHOMEOWNER" type="hidden" value="2" name="hidHOMEOWNER" runat="server">
			<INPUT id="hidRECR_VEH" type="hidden" value="2" name="hidRECR_VEH" runat="server">
			<INPUT id="hidADJUSTER_CODE" type="hidden" value="0" name="hidADJUSTER_CODE" runat="server">
			<INPUT id="hidIN_MARINE" type="hidden" value="2" name="hidIN_MARINE" runat="server">
			<INPUT id="hidLOB_ID" type="hidden" value="2" name="hidLOB_ID" runat="server"> <INPUT id="hidACTIVITY_ID" type="hidden" name="hidACTIVITY_ID" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			<INPUT id="hidReserveAdded" type="hidden" value="0" name="hidReserveAdded" runat="server">
			<INPUT id="hidAuthorized" type="hidden" value="0" name="hidAuthorized" runat="server">
			<INPUT id="hidMessage" type="hidden" name="hidMessage" runat="server">
			<INPUT id="hidLINKED_CLAIM_ID_LIST" type="hidden" value="" name="hidLINKED_CLAIM_ID_LIST" runat="server">
			<INPUT id="hidLINKED_CLAIM_LIST" type="hidden" value="" name="hidLINKED_CLAIM_LIST" runat="server">
			<INPUT id="hidADJUSTER_ID" type="hidden" value="0" name="hidADJUSTER_ID" runat="server">
			<INPUT ID="hidCATASTROPHE_EVENT_CODE" type="hidden" value="0" name="hidCATASTROPHE_EVENT_CODE" runat="server">
			<INPUT ID="hidLISTID" type="hidden" value="0" name="hidLISTID" runat="server">
			<INPUT ID="hidTOUSERID" type="hidden" value="0" name="hidTOUSERID" runat="server">
			<INPUT ID="hidDIARY_DATE" type="hidden" value="0" name="hidDIARY_DATE" runat="server">
			<INPUT ID="hidOLD_LITIGATION_FILE" type="hidden" value="0" name="hidLITIGATION_FILE" runat="server" />
			<INPUT ID="hidCURRENT_LITIGATION_FILE" type="hidden" value="0" name="hidCURRENT_LITIGATION_FILE" runat="server" />
			<INPUT ID="hidCLAIMANT_TYPE" type="hidden" value="0" name="hidCLAIMANT_TYPE" runat="server" />
			<INPUT ID="hidLOSSDATE_FUTUREDATE_ERRORMESSAGE" type="hidden" value="0" name="hidLOSSDATE_FUTUREDATE_ERRORMESSAGE" runat="server" />
			<INPUT ID="hidLOSSDATE_COMMON_ERRORMESSAGE" type="hidden" value="0" name="hidLOSSDATE_COMMON_ERRORMESSAGE" runat="server" />
		    <INPUT ID="hidOLD_IS_VICTIM_CLAIM" type="hidden" value="0" name="hidOLD_IS_VICTIM_CLAIM" runat="server" />
			<INPUT ID="hidCURRENT_IS_VICTIM_CLAIM" type="hidden" value="0" name="hidCURRENT_IS_VICTIM_CLAIM" runat="server" />
            <INPUT ID="hidCANCELLED_POLICY_MESSAGE_1" type="hidden" value="0" name="hidCANCELLED_POLICY_MESSAGE_1" runat="server" />
            <INPUT ID="hidCANCELLED_POLICY_MESSAGE_2" type="hidden" value="0" name="hidCANCELLED_POLICY_MESSAGE_2" runat="server" />
            <INPUT ID="hidNO_CORRESSPOND_DATE_MESSAGE" type="hidden" value="0" name="hidNO_CORRESSPOND_DATE_MESSAGE" runat="server" />
            <INPUT ID="hidAS400_MESSAGE" type="hidden" value="0" name="hidAS400_MESSAGE" runat="server" />
             <INPUT ID="hidCLAIM_EXISTS_ALERT_MSG" type="hidden" value="0" name="hidCLAIM_EXISTS_ALERT_MSG" runat="server" />
             <INPUT ID="hidCLAIM_EXISTS_CONFIRM_MSG" type="hidden" value="0" name="hidCLAIM_EXISTS_CONFIRM_MSG" runat="server" />
             <INPUT ID="hidCo_Insurance_Type" type="hidden" runat="server" />
             <INPUT ID="hidOFFICIAL_CLAIM_NUMBER" type="hidden" runat="server" />
		</FORM>
		
	</BODY>
</HTML>


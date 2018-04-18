<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Cms.BusinessLayer.BlCommon" Assembly="blcommon" %>
<%@ Page language="c#" Codebehind="SendDocument.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.DocumentMerge.SendDocument" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SendDocument</title>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
			<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
			<script src="/cms/cmsweb/scripts/common.js"></script>
			<script src="/cms/cmsweb/scripts/form.js"></script>
			<script src="/cms/cmsweb/scripts/Calendar.js"></script>
			<!--<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">-->
				<script language="javascript">
		var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
		var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";

			function findMouseIn()
			{
				if(!top.topframe.main1.mousein)
				{
					//createActiveMenu();
					top.topframe.main1.mousein = true;
				}
				setTimeout('findMouseIn()',5000);
			}
			
			function HideMailPanel()
			{
				
				ShowFOLLOW_UP_DATE();
				if(document.getElementById('chkMail')!=null)
				{
					var chkMail = document.getElementById('chkMail').checked;
					if(chkMail == true) // Send Mail
					{
						document.getElementById('TDMAIL').style.display = 'inline';
						EnableMailValidators();
					}
					else
					{
						document.getElementById('TDMAIL').style.display = 'none';
						disableMailValidators();
					}
				}
				else
				{
					if(document.getElementById('chkAccount').checked == true)
					document.getElementById('TDMAIL').style.display = 'none';
				}
					
			}
			
			function disableMailValidators()
			{
				document.getElementById('rfvFROM_NAME').setAttribute('enabled',false);	
				document.getElementById('rfvFROM_NAME').style.display = 'inline';
				
				document.getElementById('rfvFROM_EMAIL').setAttribute('enabled',false);	
				document.getElementById('rfvFROM_EMAIL').style.display = 'inline';
				
				document.getElementById('revFROM_EMAIL').setAttribute('enabled',false);	
				document.getElementById('revFROM_EMAIL').style.display = 'inline';
				
				document.getElementById('revADDITIONAL').setAttribute('enabled',false);
				document.getElementById('revADDITIONAL').style.display = 'inline';
				
				document.getElementById('csvRECIPIENTS').setAttribute('enabled',false);
				document.getElementById('csvRECIPIENTS').style.display = 'inline';
				
				document.getElementById('rfvSUBJECT').setAttribute('enabled',false);	
				document.getElementById('rfvSUBJECT').style.display = 'inline';
							
							
			}
			//added on 29 jan 2008
			function EnableMailValidators()
			{
				document.getElementById('rfvFROM_NAME').setAttribute('enabled',true);	
				document.getElementById('rfvFROM_NAME').style.display = 'none';
				
				document.getElementById('rfvFROM_EMAIL').setAttribute('enabled',true);	
				document.getElementById('rfvFROM_EMAIL').style.display = 'none';
				
				document.getElementById('revFROM_EMAIL').setAttribute('enabled',true);	
				document.getElementById('revFROM_EMAIL').style.display = 'none';
				
				document.getElementById('revADDITIONAL').setAttribute('enabled',true);
				document.getElementById('revADDITIONAL').style.display = 'none';
				
				document.getElementById('csvRECIPIENTS').setAttribute('enabled',true);
				document.getElementById('csvRECIPIENTS').style.display = 'none';
				
				document.getElementById('rfvSUBJECT').setAttribute('enabled',true);		
				document.getElementById('rfvSUBJECT').style.display = 'none';		
							
			}
			
			function Init()
			{
			
				/*var oldVal = document.getElementById('hidPostBack').value;
				document.getElementById('hidPostBack').value = '0';*/
				/*if(typeof parent.document.getElementById('hidCalledFor').value != null)
					document.getElementById('hidCalledFor').value =window.parent.document.getElementById('hidCalledFor').value;
				if(oldVal == '')
					__doPostBack("hidCalledFor","");*/
			}
		
			//Hide n Show section according to Account selection
			function HideInfoACT()
				{
					//alert(document.getElementById('hidCalledFor').value);
					var acctType = document.getElementById('chkAccount').checked;
					if(acctType == true) // Account
					{
						document.getElementById('trCUST').style.display = 'none';
						document.getElementById('trAPPPOL').style.display = 'none';
						document.getElementById('trLOB_AGN').style.display = 'none';
						document.getElementById('trADDINT').style.display = 'none';
						document.getElementById('trClaimParties').style.display = 'none';
						
						
						//Disable mail section
						document.getElementById('trMail').style.display = 'none';
											
						document.getElementById('chkClientType').checked = false;
						document.getElementById('chkApplicationType').checked = false;
						document.getElementById('chkPolicyType').checked = false;
						
						document.getElementById('chkClientType').disabled = true;
						document.getElementById('chkApplicationType').disabled = true;
						document.getElementById('chkPolicyType').disabled = true;
						
						
						document.getElementById('trCHECK_DATE_LBL').style.display = 'inline';
						document.getElementById('trCHECK_DATE').style.display = 'inline';
						document.getElementById('trCHECK_NO_LBL').style.display = 'inline';
						document.getElementById('trCHECK_NO').style.display = 'inline';




						document.getElementById('rfvReqCutomer_Name').setAttribute('enabled', false);
						document.getElementById('rfvReqCutomer_Name').style.display = 'none';
						//ENABLE Date Validators
						document.getElementById('revTO_CHECK_DATE').setAttribute('enabled',true);	
						document.getElementById('revFROM_CHECK_DATE').setAttribute('enabled',true);
					}
					else
					{
						document.getElementById('trCUST').style.display = 'inline';
						document.getElementById('trAPPPOL').style.display = 'inline';
						document.getElementById('trLOB_AGN').style.display = 'inline';
						document.getElementById('trADDINT').style.display = 'inline';
						
						
						
						document.getElementById('trCHECK_DATE_LBL').style.display = 'none';
						document.getElementById('trCHECK_DATE').style.display = 'none';
						document.getElementById('trCHECK_NO_LBL').style.display = 'none';
						document.getElementById('trCHECK_NO').style.display = 'none';




						document.getElementById('rfvReqCutomer_Name').setAttribute('enabled', true);
						if (document.getElementById('txtCUSTOMER_NAME').value != "") {
						    document.getElementById('rfvReqCutomer_Name').setAttribute('enabled', false);
						    document.getElementById('rfvReqCutomer_Name').style.display = 'none';
						}
						//Disable Date Validators
						document.getElementById('revTO_CHECK_DATE').setAttribute('enabled',false);	
						document.getElementById('revFROM_CHECK_DATE').setAttribute('enabled',false);	
						
					}
					
				}
			function OpenAppLookup(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction)
			{
			    var custID = document.getElementById('hidCUSTOMER_ID').value;
			    var strApplication = document.getElementById('hidApplication').value;
				if(custID == null || custID == '' || custID == '0')
					OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','APP_NUMBER_ID','CUSTOMER_ID_NAME','hidAPP_NUMBER_ID','hidCUSTOMER_ID_NAME','Application',strApplication,'','splitApplication()');
				else
					OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','APP_NUMBER_ID','CUSTOMER_ID_NAME','hidAPP_NUMBER_ID','hidCUSTOMER_ID_NAME','ApplicationCustomer',strApplication,'@CUSTID=' + custID,'splitApplication()');
					
					
			}
			
			//Lookup for Co-app : Added on 6 June 2007
			
			function OpenCoAppLookup(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction)
			{
				
				var custID = document.getElementById('hidCUSTOMER_ID').value;
				
				var polID = document.getElementById('hidPOLICY_ID').value;
				var polVersionID = document.getElementById('hidPOLICY_VERSION_ID').value;
				
				var appID = document.getElementById('hidAPP_ID').value;
				var appVersionID = document.getElementById('hidAPP_VERSION_ID').value;
				var strCoApplicant = document.getElementById('hidCoApplicant').value;
				
				if(polID!="" && polID!="0")
				{
				    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'CO_APPLICANT_ID', 'CUSTOMER_ID_NAME', 'hidCO_APP_NUMBER_ID', 'hidCUSTOMER_ID_NAME', 'CoApplicantPolicy', strCoApplicant, '@CUSTID=' + custID + ';@POLICY_ID=' + polID + ';@POLICY_VERSION_ID=' + polVersionID, 'splitCoApplicant()');
				}
				else if(appID!="" && appID!="0")
				{
				    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'CO_APPLICANT_ID', 'CUSTOMER_ID_NAME', 'hidCO_APP_NUMBER_ID', 'hidCUSTOMER_ID_NAME', 'CoApplicantApplication', strCoApplicant, '@CUSTID=' + custID + ';@APP_ID=' + appID + ';@APP_VERSION_ID=' + appVersionID, 'splitCoApplicant()');									
				}
				else if(custID!="" && custID!="0")
				{
				    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'CO_APPLICANT_ID', 'CUSTOMER_ID_NAME', 'hidCO_APP_NUMBER_ID', 'hidCUSTOMER_ID_NAME', 'CoApplicantCustomer', strCoApplicant, '@CUSTID=' + custID, 'splitCoApplicant()');					
				}
				else
				{
				    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'CO_APPLICANT_ID', 'CUSTOMER_ID_NAME', 'hidCO_APP_NUMBER_ID', 'hidCUSTOMER_ID_NAME', 'CoApplicant', strCoApplicant, '', 'splitCoApplicant()');
				}
				
				//if(custID == null || custID == '' || custID == '0')
				//	OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','CO_APPLICANT_ID','CUSTOMER_ID_NAME','hidCO_APP_NUMBER_ID','hidCUSTOMER_ID_NAME','CoApplicant','Co-Applicant','','splitCoApplicant()');
				//else
				//	OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','CO_APPLICANT_ID','CUSTOMER_ID_NAME','hidCO_APP_NUMBER_ID','hidCUSTOMER_ID_NAME','CoApplicantCustomer','Co-Applicant','@CUSTID=' + custID,'splitCoApplicant()');
					
				
			}
			//Lookup for Additional Interest :Added on 6 June 2007
			
			function OpenAddInterestLookup(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction)
			{
			//var strCalled = document.getElementById('hidCalledFor').value;
			var custID =0;
			var polID =0;
			var polVersionID = 0;
			custID =document.getElementById('hidCUSTOMER_ID').value;
			//if(strCalled =="POLICY")
			//{
				polID = document.getElementById('hidPOLICY_ID').value;
				polVersionID = document.getElementById('hidPOLICY_VERSION_ID').value;
			//}
			/*if(strCalled == "APPLICATION")
			{
				polID = document.getElementById('hidAPP_ID').value;
				polVersionID = document.getElementById('hidAPP_VERSION_ID').value;
			}*/	
			
				//Get Additional Intrst according to LOB
				var lob = document.getElementById('hidAPP_LOB').value;
				//alert(custID + '~' + polID + '~'+ polVersionID + '~' + lob);
				if(lob == '2'|| lob =='3' ) //Autp and Cycl
				{
					OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','HOLDER_ID','HOLDER_ID_NAME','hidHOLDER_ID','hidHOLDER_ID_NAME','HolderCustomerPolicyForAUTOMOTOR','Holder','@CUSTOMERID3='+custID+';@POLID3='+polID+';@POLVERSION3='+polVersionID,'splitHolder()');				  
				}
				if(lob == '1'|| lob =='6' ) //Home and Rental
				{
					OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','HOLDER_ID','HOLDER_ID_NAME','hidHOLDER_ID','hidHOLDER_ID_NAME','HolderCustomerPolicyForHOMERENTAL','Holder','@CUSTOMERID3='+custID+';@POLID3='+polID+';@POLVERSION3='+polVersionID,'splitHolder()');				  				
				}
				if(lob=='4') //Boat	
				{			
					OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','HOLDER_ID','HOLDER_ID_NAME','hidHOLDER_ID','hidHOLDER_ID_NAME','HolderCustomerPolicyForBOAT','Holder','@CUSTOMERID3='+custID+';@POLID3='+polID+';@POLVERSION3='+polVersionID,'splitHolder()');
				}
				if(lob=='7') //Genaral Liability	
				{			
					OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','HOLDER_ID','HOLDER_ID_NAME','hidHOLDER_ID','hidHOLDER_ID_NAME','HolderCustomerPolicyForGENERAL','Holder','@CUSTOMERID3='+custID+';@POLID3='+polID+';@POLVERSION3='+polVersionID,'splitHolder()');
				}
				
			}
			
			function OpenPolicyLookup(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction)
			{
				var custID = document.getElementById('hidCUSTOMER_ID').value;
				var appID = document.getElementById('hidAPP_ID').value;
				var appVersionID = document.getElementById('hidAPP_VERSION_ID').value;
				var strPolicy = document.getElementById('hidPolicy').value;
				if(custID == null || custID == '' || custID == '0')
				{
					OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','POLICY_APP_NUMBER','CUSTOMER_ID_NAME','hidPOLICY_APP_NUMBER','hidCUSTOMER_ID_NAME','Policy',strPolicy,'','splitPolicy()');
				}
				else
				{
					if(appID == null || appID == '' || appID == '0')
					    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'POLICY_APP_CUSTOMER_ID_NAME', 'CUSTOMER_ID_NAME', 'hidPOLICY_APP_NUMBER', 'hidCUSTOMER_ID_NAME', 'PolicyCustomer', strPolicy, '@customerID1=' + custID, 'splitPolicy()');
					else
					    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'POLICY_APP_CUSTOMER_ID_NAME', 'CUSTOMER_ID_NAME', 'hidPOLICY_APP_NUMBER', 'hidCUSTOMER_ID_NAME', 'PolicyCustomerApplication', strPolicy, '@customerID2=' + custID + ';@APPID=' + appID + ';@APPVERSION=' + appVersionID, 'splitPolicy()');
				}
				
								
			}
			function OpenCustomerLookup()
			{
			    var strCustomer = document.getElementById('hidCustomer').value;
				OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','CUSTOMER_ID','Name','hidCustomer_ID','txtCUSTOMER_NAME','CustLookupForm',strCustomer,'','splitCustomer()');
			}
			
			
			//Claim Lookup
			function OpenClaimLookup()
			{
				var custID = document.getElementById('hidCUSTOMER_ID').value;
				var polID = document.getElementById('hidPOLICY_ID').value;
				var strClaim = document.getElementById('hidClaim').value;
				//var polVersionID = document.getElementById('hidPOLICY_VERSION_ID').value;
				if(custID == null || custID == '' || custID == '0')
				{
				    
				    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'CLAIM_POLICY_NUMBER', 'CUSTOMER_ID_NAME', 'hidCLAIM_POLICY_NUMBER', 'hidCUSTOMER_ID_NAME', 'ClaimsDocMerge', strClaim, strClaim, 'splitClaim()');
				}
				else
				{	
					if(polID == null || polID == '' || polID == '0')
						OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','CLAIM_POLICY_NUMBER','CUSTOMER_ID_NAME','hidCLAIM_POLICY_NUMBER','hidCUSTOMER_ID_NAME','ClaimsCustomerDocMerge',strClaim,'@CUSTID='+custID,'splitClaim()');
					else //Claims will be fetched on Customer and Policy ID / Not on Policy version ID
						OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','CLAIM_POLICY_NUMBER','CUSTOMER_ID_NAME','hidCLAIM_POLICY_NUMBER','hidCUSTOMER_ID_NAME','ClaimPolicyCustomerDocMerge',strClaim,'@CUSTID='+custID+';@POLICY_ID='+polID,'splitClaim()');
							
				}
			}
			
			function splitClaim()
			{
				var ClaimAppNumber = document.getElementById('hidCLAIM_POLICY_NUMBER').value.split('~');
				document.getElementById('hidCLAIM_ID').value		=	ClaimAppNumber[0];
				document.getElementById('txtCLAIM_NUMBER').value	=	ClaimAppNumber[1];	
				document.getElementById('hidPOLICY_ID').value		=	ClaimAppNumber[2];
				document.getElementById('txtPOLICY_NUMBER').value	=	ClaimAppNumber[3];	
				//document.getElementById('hidPOLICY_VERSION_ID').value	=	ClaimAppNumber[4];
				document.getElementById('hidCLAIM_POLICY_VERSION_ID').value	=	ClaimAppNumber[4];
				
				
				
				var CustomerIdName	=	document.getElementById('hidCUSTOMER_ID_NAME').value.split('~');
				document.getElementById('hidCUSTOMER_ID').value = CustomerIdName[0];
				document.getElementById('txtCUSTOMER_NAME').value = CustomerIdName[1];		
			
			}
			
			function OpenPartyLookup()
			{
			
				var claimID = document.getElementById('hidCLAIM_ID').value;
				if(claimID!='' && claimID!='0')
					OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','PARTY_ID','NAME','hidPARTY_ID','hidPARTY_ID_NAME','ClaimPartiesDocMerge','Party','@CLAIM_ID='+claimID,'splitParty()');
		
			}	
			
			function splitParty()
			{							
					var partyId = document.getElementById('hidPARTY_ID').value;
					document.getElementById('hidPARTY_ID').value = partyId;
					document.getElementById('txtPARTY_NAME').value	=	document.getElementById('hidPARTY_ID_NAME').value
			}
			
			
			
			
			
			
			//End Claim Lookup
			function splitPolicy()
			{
				var PolicyAppNumber = document.getElementById('hidPOLICY_APP_NUMBER').value.split('~');
				document.getElementById('hidPOLICY_ID').value		=	PolicyAppNumber[0] ;
				document.getElementById('hidPOLICY_VERSION_ID').value	=	PolicyAppNumber[1];
				
				document.getElementById('txtPOLICY_NUMBER').value = PolicyAppNumber[2];
				document.getElementById('hidAPP_LOB').value = PolicyAppNumber[6]; //LobID to fetch Add Intst
				if(PolicyAppNumber[5])
				{
					document.getElementById('hidAPP_ID').value		=	PolicyAppNumber[3] ;
					document.getElementById('hidAPP_VERSION_ID').value	=	PolicyAppNumber[4];
					document.getElementById('txtAPP_NUMBER').value = PolicyAppNumber[5];
				}
				
				var CustomerIdName	=	document.getElementById('hidCUSTOMER_ID_NAME').value.split('~');
				document.getElementById('hidCUSTOMER_ID').value = CustomerIdName[0];
				document.getElementById('txtCUSTOMER_NAME').value = CustomerIdName[1];
				
				
			}
			function splitApplication()
			{
				var AppNumberId		=	document.getElementById('hidAPP_NUMBER_ID').value.split('~');
				document.getElementById('hidAPP_ID').value		=	AppNumberId[0] ;
				document.getElementById('hidAPP_VERSION_ID').value	=	AppNumberId[1];
				document.getElementById('txtAPP_NUMBER').value = AppNumberId[2];
				
				var CustomerIdName	=	document.getElementById('hidCUSTOMER_ID_NAME').value.split('~');
				document.getElementById('hidCUSTOMER_ID').value = CustomerIdName[0];
				document.getElementById('txtCUSTOMER_NAME').value = CustomerIdName[1];
				
				
				//Blank Policy Details
				document.getElementById('hidPOLICY_ID').value		=	'0' ;
				document.getElementById('hidPOLICY_VERSION_ID').value	=	'0';
				document.getElementById('txtPOLICY_NUMBER').value = '';
				
			}
			//Split Co AppID
			function splitCoApplicant()
			{
			
				var CustomerIdName	=	document.getElementById('hidCUSTOMER_ID_NAME').value.split('~');
				
				document.getElementById('hidCO_APP_NUMBER_ID').value = CustomerIdName[0];
				document.getElementById('txtCO_APPLICANT').value = CustomerIdName[1];
				
				
				 
								
			}
			//Split Holder
			function splitHolder()
			{
				var HolderIdName	=	document.getElementById('hidHOLDER_ID_NAME').value.split('~');
				document.getElementById('hidHOLDER_ID').value = HolderIdName[0];
				document.getElementById('txtADD_INT').value = HolderIdName[1];
								
			}
			function splitCustomer()
			{
				document.getElementById('hidPOLICY_ID').value		=	'0' ;
				document.getElementById('hidPOLICY_VERSION_ID').value	=	'0';
				document.getElementById('txtPOLICY_NUMBER').value = '';
				
				document.getElementById('hidAPP_ID').value		=	'0';
				document.getElementById('hidAPP_VERSION_ID').value	=	'0';
				document.getElementById('txtAPP_NUMBER').value = '';
				
				//Reset CoApplicants
				document.getElementById('hidCO_APP_NUMBER_ID').value = '0';
				document.getElementById('txtCO_APPLICANT').value = '';
				//document.getElementById('hidHOLDER_ID_NAME').value = '';
				//document.getElementById('txtADD_INT').value = '';
			}
			function ResetValues()
			{
				document.getElementById('hidCUSTOMER_ID').value		=	'0' ;
				document.getElementById('hidPOLICY_ID').value		=	'0' ;
				document.getElementById('hidPOLICY_VERSION_ID').value	=	'0';
				document.getElementById('txtPOLICY_NUMBER').value = '';
				
				document.getElementById('hidAPP_ID').value		=	'0';
				document.getElementById('hidAPP_VERSION_ID').value	=	'0';
				document.getElementById('txtAPP_NUMBER').value = '';
				document.getElementById('txtCUSTOMER_NAME').value = '';
				document.getElementById('hidCUSTOMER_ID_NAME').value = '';
				
				
				document.getElementById('hidCO_APP_NUMBER_ID').value = '0';
				document.getElementById('txtCO_APPLICANT').value = '';
				document.getElementById('hidHOLDER_ID_NAME').value = '';
				document.getElementById('txtADD_INT').value = '';
				
				
				document.getElementById('hidCLAIM_POLICY_NUMBER').value = '0';				
				document.getElementById('txtCLAIM_NUMBER').value = '';
				document.getElementById('hidCLAIM_ID').value = '0';	
				document.getElementById('hidCLAIM_POLICY_VERSION_ID').value = '0';
				document.getElementById('txtPARTY_NAME').value = '';
				document.getElementById('hidPARTY_ID').value = '0';	
				
			}
			function DoBack()
			{
				this.document.location.href = "/Cms/Client/aspx/CustomerManagerSearch.aspx";
				return false;
			}
			function DoBackToAssistant()
			{
				this.document.location.href = "/Cms/Client/aspx/CustomerManagerIndex.aspx";
				return false;
			}

			function ShowFOLLOW_UP_DATE()
			{	
				document.getElementById('imgFOLLOW_UP_DATE').style.display='none'; 
				if  (document.getElementById('cmbDIARY_ITEM_REQ').options[document.getElementById('cmbDIARY_ITEM_REQ').options.selectedIndex].value == '1')
				{
					document.getElementById('txtFOLLOW_UP_DATE').style.display='inline';	
					document.getElementById('capFOLLOW_UP_DATE').style.display='inline';
					document.getElementById('imgFOLLOW_UP_DATE').style.display='inline'; 
					document.getElementById('trDIARY_ITEM_TO').style.display='inline'; 
					//document.getElementById('trDIARY_ITEM').style.display='inline'; 
					if (document.getElementById('rfvFOLLOW_UP_DATE') != null)
						{
							document.getElementById('rfvFOLLOW_UP_DATE').setAttribute('enabled',true);
							if (document.getElementById('rfvFOLLOW_UP_DATE').isvalid == false)
								document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'inline';
						} 	
			         
					if (document.getElementById('spnFOLLOW_UP_DATE') != null)
						{
							document.getElementById('spnFOLLOW_UP_DATE').style.display = "inline";
						}
					
				}  
				else
				{
					document.getElementById('txtFOLLOW_UP_DATE').style.display='none';
					document.getElementById('capFOLLOW_UP_DATE').style.display='none';
					document.getElementById('imgFOLLOW_UP_DATE').style.display='none'; 
					document.getElementById('trDIARY_ITEM_TO').style.display='none'; 
					//document.getElementById('trDIARY_ITEM').style.display='none'; 
					if (document.getElementById('rfvFOLLOW_UP_DATE') != null)
						{
							document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("enabled",false);
							document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("isvalid",true);
							document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'none';
							
						}
					if (document.getElementById('spnFOLLOW_UP_DATE') != null)
						{
							document.getElementById('spnFOLLOW_UP_DATE').style.display = "none";
						}	 
			    
				}     
			}	
			
			function ChkDate(objSource , objArgs)
			{			
				var expdate=document.getElementById('txtFOLLOW_UP_DATE').value;				
				objArgs.IsValid = DateComparer(expdate,"<%=System.DateTime.Now.AddDays(-1)%>",'mm/dd/yyyy');   //jsaAppDtFormat				
			}	
			function CallUrl(url)
			{
				objHandler = window.open(url);
			}
			
		/////////////////MAIL JS FUNCTIONS////////////////
		
		//June 7 2007
			function funcValidateRecipients()
			{
			
				if(document.getElementById('cmbRECIPIENTS').options.length == 0)
				{
					document.getElementById('cmbRECIPIENTS').className = "MandatoryControl";
					document.getElementById("spnRECIPIENTS").style.display="inline";
					return false;
				}
				else
				{
					document.getElementById('cmbRECIPIENTS').className = "none";
					document.getElementById("spnRECIPIENTS").style.display="none";
					return true;
				}
			}		
			
		function selectRecipients()
		{
		
			for (var i=document.getElementById('cmbCONTACTDETAILS').options.length-1;i>=0;i--)
			{
					if (document.getElementById('cmbCONTACTDETAILS').options[i].selected == true)
					{
						document.getElementById('cmbRECIPIENTS').options.length=document.getElementById('cmbRECIPIENTS').length+1;
						document.getElementById('cmbRECIPIENTS').options[document.getElementById('cmbRECIPIENTS').length-1].value=document.getElementById('cmbCONTACTDETAILS').options[i].value;
						document.getElementById('cmbRECIPIENTS').options[document.getElementById('cmbRECIPIENTS').length-1].text=document.getElementById('cmbCONTACTDETAILS').options[i].text;
						document.getElementById('cmbCONTACTDETAILS').options[i] = null;
					}
		  	}
		  	
			if (document.getElementById('txtADDITIONAL').value != '' && document.getElementById('revADDITIONAL').getAttribute('IsValid'))
			{
				document.getElementById('cmbRECIPIENTS').options.length=document.getElementById('cmbRECIPIENTS').length+1;
				document.getElementById('cmbRECIPIENTS').options[document.getElementById('cmbRECIPIENTS').length-1].value=document.getElementById('txtADDITIONAL').value;
				document.getElementById('cmbRECIPIENTS').options[document.getElementById('cmbRECIPIENTS').length-1].text=document.getElementById('txtADDITIONAL').value;
				document.getElementById('txtADDITIONAL').value='';
			} 		
			
			return false;
		  	
		}
	
		
		
		function deselectRecipients()
		{
		
		  for (var i=document.getElementById('cmbRECIPIENTS').options.length-1;i>=0;i--)
			{
			
				if (document.getElementById('cmbRECIPIENTS').options[i].selected == true)
				{
					document.getElementById('cmbCONTACTDETAILS').options.length=document.getElementById('cmbCONTACTDETAILS').length+1;
					document.getElementById('cmbCONTACTDETAILS').options[document.getElementById('cmbCONTACTDETAILS').length-1].value=document.getElementById('cmbRECIPIENTS').options[i].value;
					document.getElementById('cmbCONTACTDETAILS').options[document.getElementById('cmbCONTACTDETAILS').length-1].text=document.getElementById('cmbRECIPIENTS').options[i].text;
					document.getElementById('cmbRECIPIENTS').options[i] = null;
				}
				
		  	}	
		  	return false;			
		
		}	
		function setRecipients()
		{
		
			document.getElementById('hidRECIPIENTS').value=''			
			//document.Email.hidRECIPIENTS.value=''
			for (var i=0;i< document.getElementById('cmbRECIPIENTS').options.length;i++)
			{document.getElementById('hidRECIPIENTS').value = document.getElementById('hidRECIPIENTS').value + document.getElementById('cmbRECIPIENTS').options[i].text + ',';}							
			Page_ClientValidate();
			var returnVal = funcValidateRecipients();
			return Page_IsValid && returnVal;
		}
		
		function addRecipients()
		{
		 var Recipients = document.getElementById("hidRECIPIENTS").value;
		 var Recipient = Recipients.split(",");
				for(j = document.getElementById('cmbRECIPIENTS').length-1; j >=0;j--)
				{
					document.getElementById('cmbRECIPIENTS').options[j]=null;
				}	
				 for(j = 0; j < Recipient.length-1 ;j++)
					{
					document.getElementById('cmbRECIPIENTS').options.length=document.getElementById('cmbRECIPIENTS').length+1;
					document.getElementById('cmbRECIPIENTS').options[document.getElementById('cmbRECIPIENTS').length-1].value=Recipient[j];
					document.getElementById('cmbRECIPIENTS').options[document.getElementById('cmbRECIPIENTS').length-1].text=Recipient[j];
					}
		}
				
		//////////END///////MAIL JS FUNCTIONS////////////////	
		
		
		
				</script>
	</HEAD>
	<body  oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="Init();top.topframe.main1.mousein=false;findMouseIn();ApplyColor();ChangeColor();HideInfoACT();HideMailPanel();setfirstTime();">
		<webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<form id="SendDocument" method="post" runat="server">
			
				<table cellSpacing="0" cellPadding="0" class="tableWidthHeader" border="0" width = "100%">
					<tr>
						<td>
							<table class="tableWidthHeader" align="center" border="0">
								<TBODY>
										<tr>
										<TD class="pageHeader" colSpan="4"><asp:label ID="capMessages" Text="Please note that all fields marked with * are mandatory" runat="server"></asp:label></TD>
									</tr>
									<tr>
										<td class="midcolorc" align="right" colSpan="4" height="9"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
									</tr>
									<tr>
										<TD class="midcolora" colSpan="4">&nbsp;</TD>
									</tr>
									<tr>
										<td class="midcolora" align="left" colSpan="4" height="9"><b><asp:label id="lblTemplateType" runat="server">Select 
													Template Type</asp:label></b></td>
									</tr>
									<tr>
										<TD class="midcolora" width="17%">&nbsp;&nbsp;&nbsp;<asp:label id="lblClientType" runat="server">Customer</asp:label></TD>
										<TD class="midcolora" width="33%"><asp:checkbox id="chkClientType" runat="server" AutoPostBack="True"></asp:checkbox></TD>
										<TD class="midcolora" width="17%">&nbsp;&nbsp;&nbsp;<asp:label id="lblApplicationType" runat="server">Application</asp:label></TD>
										<TD class="midcolora" width="33%"><asp:checkbox id="chkApplicationType" runat="server" AutoPostBack="True"></asp:checkbox></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="17%">&nbsp;&nbsp;&nbsp;<asp:label id="lblPolicyType" runat="server">Policy</asp:label></TD>
										<TD class="midcolora" width="33%"><asp:checkbox id="chkPolicyType" runat="server" AutoPostBack="True"></asp:checkbox></TD>
										<TD class="midcolora" width="17%">&nbsp;&nbsp;&nbsp;<asp:label id="lblAccount" runat="server">Account</asp:label></TD>
										<TD class="midcolora" width="33%"><asp:checkbox id="chkAccount" runat="server" AutoPostBack="True" onChange="HideInfoACT()"></asp:checkbox></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="17%">&nbsp;&nbsp;&nbsp;<asp:label id="lblClaim1" runat="server">Claim</asp:label></TD>
										<TD class="midcolora" width="33%"><asp:checkbox id="chkClaimType" runat="server" AutoPostBack="True"></asp:checkbox></TD>
										<TD class="midcolora" width="17%">&nbsp;&nbsp;&nbsp;</TD>
										<TD class="midcolora" width="33%"></TD>
									</tr>
									<tr id="trLOB_AGN">
										<TD class="midcolora" width="17%">&nbsp;&nbsp;&nbsp;<asp:label id="lblLob" runat="server">Product</asp:label></TD>
										<TD class="midcolora" width="33%"><asp:dropdownlist id="ddlLob" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
										<TD class="midcolora" width="17%">&nbsp;&nbsp;&nbsp;<asp:label id="lblAgency" runat="server">Agency</asp:label></TD>
										<TD class="midcolora" width="33%"><asp:dropdownlist id="ddlAgency" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
									</tr>
									
									<tr id="trClaimParties" style="DISPLAY: none">
										<TD class="midcolora" width="17%">&nbsp;&nbsp;&nbsp;<asp:label id="lblClaimParties" runat="server">Claim Parties</asp:label></TD>
										<TD class="midcolora" width="33%"><asp:dropdownlist id="ddlClaimParties" runat="server"></asp:dropdownlist></TD>
										<TD class="midcolora" width="17%">&nbsp;&nbsp;&nbsp;</TD>
										<TD class="midcolora" width="33%"></TD>
									</tr>
									
									<tr>
										<TD class="midcolora" colSpan="4">&nbsp;</TD>
									</tr>
									<tr>
										<TD class="midcolora" colSpan="4">&nbsp;</TD>
									</tr>
									<tr>
										<TD class="midcolora" width="17%"><asp:label id="LblDocument" runat="server">Document</asp:label><SPAN class="mandatory">*</SPAN></TD>
										<TD class="midcolora" width="83%" colSpan="3"><asp:dropdownlist id="ddlDocument" runat="server"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvReqDocument" runat="server" ErrorMessage="" Display="Dynamic"
												ControlToValidate="ddlDocument"></asp:requiredfieldvalidator></TD><%--Please select Document--%>
									</tr>
									<tr id="trCUST">
										<td class="midcolora" width="17%" height="25"><asp:label id="lblClient" Runat="server">Customer</asp:label><SPAN class="mandatory">*</SPAN></td>
										<td class="midcolora" width="33%" height="25"><asp:textbox id="txtCUSTOMER_NAME" runat="server" Columns="30" ReadOnly="true"></asp:textbox><IMG id="imgCustomer" style="CURSOR: hand" onclick="OpenCustomerLookup()" src="/cms/cmsweb/images/selecticon.gif"
												runat="server"><br>
											<asp:requiredfieldvalidator id="rfvReqCutomer_Name" runat="server" ErrorMessage="" Display="Dynamic"
												ControlToValidate="txtCUSTOMER_NAME"></asp:requiredfieldvalidator><%--RequiredFieldValidator--%><%--Customer can not be blank.--%><asp:requiredfieldvalidator id="rfvReqClientUndefined" runat="server" ErrorMessage=""
												Display="Dynamic" ControlToValidate="txtCUSTOMER_NAME" InitialValue="undefined"></asp:requiredfieldvalidator></td><%--Customer can not be undefined.--%>
												
										<td class="midcolora" width="17%"><asp:label id="lblPOLICY_NUMBER" Runat="server">Policy</asp:label></td>
										<td class="midcolora" width="33%"><asp:textbox id="txtPOLICY_NUMBER" runat="server" Columns="11" ReadOnly="true"></asp:textbox><IMG id="imgPolicy" style="CURSOR: hand" onclick="OpenPolicyLookup()" src="/cms/cmsweb/images/selecticon.gif">
										
										
									</tr>
									<tr id="trAPPPOL">
										<td class="midcolora" width="17%"><asp:label id="lblApplication" Runat="server">Application</asp:label></td>
										<td class="midcolora" width="33%"><asp:textbox id="txtAPP_NUMBER" Runat="server" ReadOnly="True" size="15" MaxLength="50"></asp:textbox><IMG id="imgApplication" style="CURSOR: hand" onclick="OpenAppLookup()" src="/cms/cmsweb/images/selecticon.gif">
										</td>
										
										<td class="midcolora" width="30%" height="25"><asp:label id="lblCoApp" Runat="server">Co-Applicant(Named Insured)</asp:label></td>
										<td class="midcolora" width="3%" height="25"><asp:textbox id="txtCO_APPLICANT" runat="server" Columns="30" ReadOnly="true"></asp:textbox><IMG id="Img1" style="CURSOR: hand" onclick="OpenCoAppLookup()" src="/cms/cmsweb/images/selecticon.gif"
												runat="server"><br>
											<asp:requiredfieldvalidator id="ReqCoAppUndefined" runat="server" ErrorMessage=""
												Display="Dynamic" ControlToValidate="txtCO_APPLICANT" InitialValue="undefined"></asp:requiredfieldvalidator></td><%--Co Applicant can not be undefined.--%>
										</td>
									</tr>
								
									<tr id="trCLAIMS">
										<td class="midcolora" width="17%"><asp:label id="lblClaim" Runat="server">Claim</asp:label></td>
										<td class="midcolora" width="33%"><asp:textbox id="txtCLAIM_NUMBER" Runat="server" ReadOnly="True" size="15" MaxLength="50"></asp:textbox><IMG id="imgClaim" style="CURSOR: hand" onclick="OpenClaimLookup()" src="/cms/cmsweb/images/selecticon.gif">
										</td>
										
										<td class="midcolora" width="30%" height="25"><asp:label id="lblParties" Runat="server">Party(Claim)</asp:label></td>
										<td class="midcolora" width="3%" height="25"><asp:textbox id="txtPARTY_NAME" runat="server" Columns="30" ReadOnly="true"></asp:textbox><IMG id="ImgpParty" style="CURSOR: hand" onclick="OpenPartyLookup()" src="/cms/cmsweb/images/selecticon.gif"
												runat="server"><br>
											<asp:requiredfieldvalidator id="ReqPartiesUndefined" runat="server" ErrorMessage=""
												Display="Dynamic" ControlToValidate="txtPARTY_NAME" InitialValue="undefined"></asp:requiredfieldvalidator></td><%--Party can not be undefined.--%>
										</td>
									</tr>
									
									<tr id="trADDINT">
										<TD class="midcolora" width="17%"><asp:label id="lblAddIntrst" runat="server">Additional Interest</asp:label></TD>
										<TD class="midcolora" width="83%"><asp:textbox id="txtADD_INT" runat="server" Columns="11" ReadOnly="true" Width="128px"></asp:textbox><IMG id="imgAddIntrst" style="CURSOR: hand" onclick="OpenAddInterestLookup()" src="/cms/cmsweb/images/selecticon.gif"></TD>
										<td class="midcolora" width="17%"><asp:Label ID="capDocument" runat="server"></asp:label></TD> <%--Document:--%>
										<td class="midcolora" width="33%"><asp:label id="lblATTACHMENT" Runat="server" visible=false></asp:label></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%" height="38"><asp:label id="capDIARY_ITEM_REQ" runat="server">Diary Item Required</asp:label></TD>
										<TD class="midcolora" width="32%" height="38"><asp:dropdownlist id="cmbDIARY_ITEM_REQ" onfocus="SelectComboIndex('cmbDIARY_ITEM_REQ')" runat="server">
												<%--<asp:ListItem Value='N'>No</asp:ListItem>
												<asp:ListItem Value='Y'>Yes</asp:ListItem>--%>
											</asp:dropdownlist></TD>
										<TD class="midcolora" id="tdFollowupCap" width="18%" height="38"><asp:label id="capFOLLOW_UP_DATE" style="DISPLAY: none" runat="server">Follow up date</asp:label><span class="mandatory" id="spnFOLLOW_UP_DATE" style="DISPLAY: none">*</span></TD>
										<TD class="midcolora" id="tdFollowuptxt" width="32%"><asp:textbox id="txtFOLLOW_UP_DATE" style="DISPLAY: none" runat="server" size="12" maxlength="10">
										</asp:textbox><asp:hyperlink id="hlkFOLLOW_UP_DATE" runat="server" CssClass="HotSpot">
										<ASP:IMAGE id="imgFOLLOW_UP_DATE" runat="server" Border="0" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE></asp:hyperlink>
										<br><asp:requiredfieldvalidator id="rfvFOLLOW_UP_DATE" Display="Dynamic" ControlToValidate="txtFOLLOW_UP_DATE" Runat="server" Enabled="False"></asp:requiredfieldvalidator><asp:customvalidator id="csvFOLLOW_UP_DATE" Display="Dynamic" ControlToValidate="txtFOLLOW_UP_DATE" Runat="server"
												ClientValidationFunction="ChkDate"></asp:customvalidator>
												<asp:regularexpressionvalidator id="revFOLLOW_UP_DATE" runat="server" ErrorMessage="RegularExpressionValidator"
												Display="Dynamic" ControlToValidate="txtFOLLOW_UP_DATE"></asp:regularexpressionvalidator>
										</TD>
									</tr>
									<tr id ="trDIARY_ITEM_TO" runat="server" style="DISPLAY:none">
										<TD class="midcolora" colSpan="1" style="HEIGHT: 38px"><asp:label id="capDIARY_ITEM_TO" runat="server">To</asp:label></TD>
											<TD class="midcolora" colSpan="1" style="HEIGHT: 38px"><asp:dropdownlist id="cmbDIARY_ITEM_TO" onfocus="SelectComboIndex('cmbDIARY_ITEM_TO')" runat="server"></asp:dropdownlist></TD>
										<TD class="midcolora" colSpan="2" style="HEIGHT: 38px"></TD>
									</tr>
									<tr id="trCHECK_DATE_LBL">
										<td class="midcolora" align="left" colSpan="4" height="9"><b><asp:label id="lblchkDate" runat="server">Select Check Date</asp:label></b></td>
									</tr>
									<tr id="trCHECK_DATE">
										<td class="midcolora" width="17%"><asp:label id="lblFrom" Runat="server">From</asp:label></td>
										<td class="midcolora" width="33%"><asp:textbox id="txtFROM_CHECK_DATE" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkFromCheckDate" runat="server" CssClass="HotSpot">
												<asp:image id="imgInceptionExp" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
													valign="middle"></asp:image>
											</asp:hyperlink><br>
											<asp:regularexpressionvalidator id="revFROM_CHECK_DATE" runat="server" ErrorMessage="RegularExpressionValidator"
												Display="Dynamic" ControlToValidate="txtFROM_CHECK_DATE"></asp:regularexpressionvalidator></td>
										<td class="midcolora" width="17%"><asp:label id="lbl" Runat="server">To</asp:label></td>
										<td class="midcolora" width="33%"><asp:textbox id="txtTO_CHECK_DATE" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkToCheckDate" runat="server" CssClass="HotSpot">
												<asp:image id="Image1" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif" valign="middle"></asp:image>
											</asp:hyperlink><br>
											<asp:regularexpressionvalidator id="revTO_CHECK_DATE" runat="server" ErrorMessage="RegularExpressionValidator" Display="Dynamic"
												ControlToValidate="txtTO_CHECK_DATE"></asp:regularexpressionvalidator></td>
									</tr>
									<tr id="trCHECK_NO_LBL">
										<td class="midcolora" align="left" colSpan="4" height="9"><b><asp:label id="lblChkNo" runat="server">Select Check #</asp:label></b></td>
									</tr>
									<tr id="trCHECK_NO">
										<td class="midcolora" width="17%"><asp:label id="lblChkNoFrom" Runat="server">From</asp:label></td>
										<td class="midcolora" width="33%"><asp:textbox id="txtChkNoFrom" runat="server" size="12" maxlength="10"></asp:textbox><asp:regularexpressionvalidator id="revFromCheckNumber" runat="server" ErrorMessage="revFromCheckNumber" Display="Dynamic"
												ControlToValidate="txtChkNoFrom"></asp:regularexpressionvalidator></td>
										<td class="midcolora" width="17%"><asp:label id="lblChkNoTo" Runat="server">To</asp:label></td>
										<td class="midcolora" width="33%"><asp:textbox id="txtChkNoTo" runat="server" size="12" maxlength="10"></asp:textbox><asp:regularexpressionvalidator id="revToCheckNumber" runat="server" ErrorMessage="revFromCheckNumber" Display="Dynamic"
												ControlToValidate="txtChkNoTo"></asp:regularexpressionvalidator></td>
									</tr>
									
								<tr id="trMail">
										<td class="midcolora" width="100%" colSpan="4"><asp:CheckBox id="chkMail" Runat="server" Text="Send Mail" onClick="HideMailPanel()"></asp:CheckBox></td>
									</tr>
									<tr>
										<td class="midcolora" align="left" width="50%" colSpan="2"><cmsb:cmsbutton class="clsButton"  id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
										</td>
										<td class="midcolorr" align="right" width="50%" colSpan="2">
										<cmsb:cmsbutton class="clsButton" id="btnSaveToSpooler" runat="server" Text="Save To Spooler"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnMerge" runat="server" Text="Merge"></cmsb:cmsbutton></td>
									</tr>
									
									<tbody class="tableWidth" id="TDMAIL">
										<TR>
											<TD class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage1" runat="server" CssClass="errmsg" Visible="False"></asp:label></TD>
										</TR>
									
									
										<TR>
											<TD class="midcolora" width="18%"><asp:label id="capFROM_NAME" runat="server">From Name</asp:label><SPAN class="mandatory">*</SPAN></TD>
											<TD class="midcolora" width="32%" colSpan="3"><asp:textbox id="txtFROM_NAME" runat="server" maxlength="50" size="30"></asp:textbox><br>
												<asp:requiredfieldvalidator id="rfvFROM_NAME" runat="server" Display="Dynamic" ControlToValidate="txtFROM_NAME"></asp:requiredfieldvalidator></TD>
										</TR>
										<TR>
											<TD class="midcolora" width="18%"><asp:label id="capFROM_EMAIL" runat="server">From Email</asp:label><SPAN class="mandatory">*</SPAN></TD>
											<TD class="midcolora" width="32%" colSpan="3"><asp:textbox id="txtFROM_EMAIL" runat="server" maxlength="50" size="30"></asp:textbox><br>
												<asp:requiredfieldvalidator id="rfvFROM_EMAIL" runat="server" Display="Dynamic" ControlToValidate="txtFROM_EMAIL"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revFROM_EMAIL" Display="Dynamic" ControlToValidate="txtFROM_EMAIL" Runat="server"></asp:regularexpressionvalidator></TD>
										</TR>
										<tr>
											<TD class="midcolora"><asp:label id="capTO" runat="server">To</asp:label><SPAN class="mandatory">*</SPAN></TD>
											<td class="midcolora" align="center" width="18%"><asp:label id="capCONTACTDETAILS" Runat="server">Contact Details</asp:label><br>
												<asp:listbox id="cmbCONTACTDETAILS" Runat="server" Height="79px" AutoPostBack="False" SelectionMode="Multiple">
													<ASP:LISTITEM Value="0"><--------Select--------></ASP:LISTITEM>
												</asp:listbox><br>
												<asp:label id="capADDITIONAL" Runat="server">Add Additional Recipient</asp:label><br>
												<asp:textbox id="txtADDITIONAL" maxlength="50" size="30" Runat="server"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revADDITIONAL" Display="Dynamic" ControlToValidate="txtADDITIONAL" Runat="server"></asp:regularexpressionvalidator></td>
											<td class="midcolorc" align="center" width="18%"><br>
												<asp:button id="btnSELECT" Runat="server" Text=">>" CausesValidation="True"></asp:button><br>
												<br>
												<br>
												<br>
												<asp:button id="btnDESELECT" Runat="server" Text="<<" CausesValidation="False"></asp:button></td>
											<td class="midcolora" align="center"><asp:label id="capRECIPIENTS1" Runat="server">Recipients</asp:label><br>
												<asp:listbox id="cmbRECIPIENTS" onblur="funcValidateRecipients" Runat="server" Height="79px"
													AutoPostBack="False" SelectionMode="Multiple" onChange="funcValidateRecipients"></asp:listbox><br>
												<asp:customvalidator id="csvRECIPIENTS" Display="Dynamic" ControlToValidate="cmbRECIPIENTS" Runat="server"
													ClientValidationFunction="funcValidateRecipients" Enabled="False"></asp:customvalidator><span id="spnRECIPIENTS" style="DISPLAY: none; COLOR: red">Please select Recipients.</span>
											</td>
										</tr>
									
										<TR id="trRECIEPIENTS" style="DISPLAY: none">
											<TD class="midcolora"><asp:label id="capRECIPIENTS" runat="server">Recipients</asp:label></TD>
											<TD class="midcolora"><asp:textbox id="txtRECIPIENTS" runat="server" maxlength="200" size="30" TextMode="MultiLine"></asp:textbox><BR>
											</TD>
										</TR>
										<tr>
											<TD class="midcolora" width="18%"><asp:label id="capSubject" runat="server">Subject</asp:label><SPAN class="mandatory">*</SPAN></TD>
											<TD class="midcolora" colSpan="3"><asp:textbox id="txtSUBJECT" runat="server" maxlength="50" size="50"></asp:textbox><BR>
												<asp:requiredfieldvalidator id="rfvSUBJECT" runat="server" Display="Dynamic" ControlToValidate="txtSUBJECT"></asp:requiredfieldvalidator></TD>
										</tr>
										<TR>
											<TD class="midcolora" width="18%"><asp:label id="capMESSAGE" runat="server">Message</asp:label></TD>
											<TD class="midcolora" colSpan="3"><asp:textbox onkeypress="MaxLength(txtMESSAGE,500)" id="txtMESSAGE" runat="server" Height="96px"
													TextMode="MultiLine" width="265px"></asp:textbox><BR>
											</TD>
										</TR>
									
										
	
										<tr id="trBody" runat="server">
											<td class="midcolora"><INPUT class="clsButton" id="btnResetMail"  onclick="javascritp:ResetValues()" type="button"
													value="Reset"  name="btnResetMail"></td>
											<td class="midcolorr" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnSend" runat="server" Text="Send"></cmsb:cmsbutton></td>
										</tr>
									</tbody>
									<% if (1==2) { %>
									<tr>
										<td class="midcolora" align="left" colSpan="2"><input class="clsButton" id="btnCustomerAssistant"  onclick="javascript:DoBackToAssistant()"
												type="button" value="Back To Customer Assistant">
										</td>
										<td class="midcolorr" align="right" colSpan="2"><input class="clsButton" id="btnBack"  onclick="javascript:DoBack()" type="button" value="Back To Search">
										</td>
									</tr>
									<%}%>
							</table>
							<input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
							<input id="hidAPP_ID" type="hidden" value="0" name="hidAPP_ID" runat="server"> <input id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
							<input id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
							<input id="hidAPP_VERSION_ID" type="hidden" value="0" name="hidAPP_VERSION_ID" runat="server">
							<input id="hidAPP_NUMBER_ID" type="hidden" value="0" name="hidAPP_NUMBER_ID" runat="server">
							<input id="hidCUSTOMER_ID_NAME" type="hidden" name="hidCUSTOMER_ID_NAME" runat="server">
							<input id="hidPOLICY_APP_NUMBER" type="hidden" name="hidPOLICY_APP_NUMBER" runat="server">
							<input id="hidMergeId" type="hidden" name="hidMergeId" runat="server">
							<input id="hidCO_APP_NUMBER_ID" value="0" type="hidden" name="hidCO_APP_NUMBER_ID" runat="server">
							<input id="hidHOLDER_ID" type="hidden" value="0" name="hidHOLDER_ID" runat="server">
							<input id="hidHOLDER_ID_NAME" type="hidden" name="hidHOLDER_ID_NAME" runat="server">
							<INPUT id="hidRECIPIENTS" type="hidden" value="0" name="hidRECIPIENTS" runat="server">
							<input id="hidAPP_LOB" type="hidden" value="0" name="hidAPP_LOB" runat="server">
							<input id="hidCalledFor" type="hidden" value="" name="hidCalledFor" runat="server">
							<input id="hidPostBack" type="hidden" value="" name="hidPostBack" runat="server">
							
							<input id="hidCLAIM_POLICY_NUMBER" type="hidden" name="hidCLAIM_POLICY_NUMBER" runat="server">
							<input id="hidCLAIM_ID" value="0" type="hidden" name="hidCLAIM_ID" runat="server">
							<input id="hidPARTY_ID" value="0" type="hidden" name="hidPARTY_ID" runat="server">
							<input id="hidPARTY_ID_NAME" value="0" type="hidden" name="hidPARTY_ID_NAME" runat="server">
							<input id="hidCLAIM_POLICY_VERSION_ID" type="hidden" value="0" name="hidCLAIM_POLICY_VERSION_ID" runat="server">
							<input id="hidCustomer" type="hidden" runat="server" />
							<input id="hidApplication" type="hidden" runat="server" />
							<input id="hidClaim" type="hidden" runat="server" />
							<input id="hidPolicy" type="hidden" runat="server" />
							<input id="hidCoApplicant" type="hidden" runat="server" />
							
						</td>
					</tr>
				</table>
				<%if (UpdateGrid) {%>
				<script language="javascript">
					RefreshWebGrid('1',document.getElementById('hidMergeId').value ,false);
				</script>
				<%}%>
				
			</form>
					
	</body>
</HTML>

<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="cc1" Namespace="Cms.BusinessLayer.BlCommon" Assembly="blcommon" %>
<%@ Register TagPrefix="cmsp" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsPanel" %>
<%@ Register TagPrefix="webcontrol" TagName="CmsTimer" Src="/cms/cmsweb/webcontrols/CmsTimePicker.ascx" %>
<%@ Page language="c#" Codebehind="adddiaryentry.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Diary.AddDiaryEntry" ValidateRequest="false"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Add Diary Entry</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript" src="../Scripts/Calendar.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script language="javascript">
			var refWindow;
			function OpenPolicy()
			{
				path = "/cms/policies/aspx/Policytab.aspx?customer_id="+document.getElementById("hidCUSTOMER_ID").value
								+ "&policy_id=" + document.getElementById("hidPOLICY_ID").value
								+ "&policy_version_id=" + document.getElementById("hidPOLICY_VERSION_ID").value
								+ "&app_version_id=" + document.getElementById("hidAPP_VERSION_ID").value
								+ "&app_id=" + document.getElementById("hidAPP_ID").value;
				var customer="<%=strCalledFrom%>";
				if(customer == 'InCLT')
				{
					this.parent.parent.document.location.href = path;
				}
				else
				{
					this.parent.document.location.href = path;
				}
				return false;
			}			
			
				
			function OpenClaim()
			{
				var strPath="/cms/claims/aspx/ClaimsTab.aspx?CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value 
								+ "&customer_id="+ document.getElementById("hidCUSTOMER_ID").value
								+ "&policy_id=" + document.getElementById("hidPOLICY_ID").value
								+ "&policy_version_id=" + document.getElementById("hidPOLICY_VERSION_ID").value + "&";				
				top.topframe.callItemClicked('2','');
				this.parent.document.location.href = strPath;
				return false;
			}
			
			
			
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
			var fromAdd="0";
			function AddData()
			{
				
				//document.getElementById('txtUserID').value='1';
				document.getElementById('txtFOLLOWUPDATE').value	=	DateAdd();
				document.getElementById('txtSUBJECTLINE').value='';
				document.getElementById('txtNOTE').value='';
				document.getElementById('hidLISTID').value	= 'NEW';	
				document.getElementById('txtCUSTOMER_NAME').value	= '';	
				document.getElementById('txtAPP_NUMBER').value	= '';	
				document.getElementById('txtPOLICY_NUMBER').value	= '';
				document.getElementById('txtQUOTE_NUMBER').value	= '';	
				
				document.getElementById('lblFROMUSERNAME').innerText = document.getElementById('hidFROMUSERNAME').value;
				if (document.getElementById('cmbTOUSERID').options.length >0 )
				{
					//document.getElementById('cmbToUserId').selectedIndex = 0;
				}
				
				if (document.getElementById('cmbLISTTYPEID').options.length >0 )
				{	
					document.getElementById('cmbLISTTYPEID').selectedIndex = 0;
				}
				if (document.getElementById('cmbPRIORITY').options.length >0 )
				{
					document.getElementById('cmbPRIORITY').selectedIndex = 0;
				}
				if (document.getElementById('cmbSYSTEMFOLLOWUPID').options.length >0 )
				{
					document.getElementById('cmbSYSTEMFOLLOWUPID').selectedIndex = 0;
				}
				
				if (document.getElementById('cmbSTARTTIMEHOUR').options.length >0 )
				{
					document.getElementById('cmbSTARTTIMEHOUR').selectedIndex = 0;
				}
				if (document.getElementById('cmbSTARTTIMEMINUTE').options.length >0 )
				{
					document.getElementById('cmbSTARTTIMEMINUTE').selectedIndex = 0;
				}
				if (document.getElementById('cmbSTARTTIMEMERIDIAN').options.length >0 )
				{
					document.getElementById('cmbSTARTTIMEMERIDIAN').selectedIndex = 0;
				}
				if (document.getElementById('cmbENDTIMEHOUR').options.length >0 )
				{
					document.getElementById('cmbENDTIMEHOUR').selectedIndex = 0;
				}
				if (document.getElementById('cmbENDTIMEMINUTE').options.length >0 )
				{
					document.getElementById('cmbENDTIMEMINUTE').selectedIndex = 0;
				}
				if (document.getElementById('cmbTOUSERID').options.length >0 )
				{
					document.getElementById('cmbENDTIMEMERIDIAN').selectedIndex = 0;
				}
				//document.getElementById('cmbTOUSERID').selectedIndex = 0;
				
				DisableValidators();
				//Changing the colors of mandtory controls
				ChangeColor();	
				fromAdd="1";
			}
			
			
			function showButton(boolValue,flagShow)
			{
				if(document.getElementById('btnComplete'))
					document.getElementById('btnComplete').style.display=boolValue;
				//document.getElementById('btnTransfer').style.display=boolValue;
				if(document.getElementById('btnTransfer'))
				document.getElementById('btnTransfer').style.display=boolValue;
				if(document.getElementById('btnReminder'))
				document.getElementById('btnReminder').style.display=boolValue;
				if(fromAdd==1)
				{
					if(document.getElementById('btnDelete'))
					document.getElementById('btnDelete').style.display="none";
				}
				
				
				
				if(flagShow==1)
				{
					if(document.getElementById('btnSave'))
					document.getElementById('btnSave').style.display=boolValue;
					if(document.getElementById('btnReset'))
					document.getElementById('btnReset').style.display=boolValue;					
					
				}
									
			}
			
			function populateXML()
			{ 
				var tempXML;
				fromAdd="0";
				tempXML=document.getElementById("hidOldData").value;	
				//alert(tempXML);			
				if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
				{
					if(tempXML!="")
					{
						populateFormData(tempXML,"DiaryForm");											
						//case "STARTHOUR":
						var objXmlHandler = new XMLHandler();
						
						var tree = (objXmlHandler.quickParseXML(tempXML).getElementsByTagName('Table')[0]);
						
						//alert(tree.getTagName('STARTTIMEHOUR'))
						//alert(tree.getElementByTagName('STARTTIMEHOUR').text)

						document.getElementById('txtFOLLOWUPDATE').value = document.getElementById('hidFollow_Up_Date').value;
						
						
							var thisHour = 0;
							//alert(document.getElementById("hidSTARTTIMEHOUR").value)
							if(parseInt(document.getElementById("hidSTARTTIMEHOUR").value) > 12)
							{
								thisHour = parseInt(document.getElementById("hidSTARTTIMEHOUR").value) - 12;
								document.getElementById('cmbSTARTTIMEMERIDIAN').selectedIndex = 2;
								
							}
							else
							{
								thisHour = parseInt(document.getElementById("hidSTARTTIMEHOUR").value);
								document.getElementById('cmbSTARTTIMEMERIDIAN').selectedIndex = 1;
							}
						
							document.getElementById('cmbSTARTTIMEHOUR').selectedIndex = thisHour+1;
							
							document.getElementById('cmbSTARTTIMEMINUTE').selectedIndex = parseInt(document.getElementById("hidSTARTTIMEMINUTE").value) + 1;
							
							var thisHour = 0;
							if(parseInt(document.getElementById("hidENDTIMEHOUR").value) > 12)
							{
								thisHour = parseInt(document.getElementById("hidENDTIMEHOUR").value) - 12;
								document.getElementById('cmbENDTIMEMERIDIAN').selectedIndex = 2;
							}
							else
							{
								thisHour = parseInt(document.getElementById("hidENDTIMEHOUR").value);
								document.getElementById('cmbENDTIMEMERIDIAN').selectedIndex = 1;
							}
							document.getElementById('cmbENDTIMEHOUR').selectedIndex = thisHour +1;
							
							
							document.getElementById('cmbENDTIMEMINUTE').selectedIndex = parseInt(document.getElementById("hidENDTIMEMINUTE").value) + 1;
						/********************************************************************
							Change made by Anurag for showing buttons in edit mode	
						*********************************************************************/
						
						if(document.getElementById("hidLISTOPEN").value!="N")
							showButton('inline',0);		
						else	
							showButton('none',1);		
						/********************************************************************/	
					}
					else
					{
						AddData();
						/********************************************************************
							Change made by Anurag for showing buttons in edit mode	
						*********************************************************************/
							showButton('none',0);
						/********************************************************************/	  
					}
				}
				else
				{
						if(document.getElementById("hidLISTOPEN").value!="N")
							showButton('inline',0);		
						else	
							showButton('none',1);		
				}
				
				//document.getElementById("txtQUOTE_NUMBER").value = document.getElementById("hidQQ_ID").value				
				//return false;
				setTimeRow();
				ShowHideRulesLink();
				
			}		
		
			// Added by mohit on 14/10/2005.
			function doValidationStart(source,args)
			{
				startTime		= "" + document.getElementById('cmbSTARTTIMEHOUR').options[document.getElementById('cmbSTARTTIMEHOUR').selectedIndex].value + ":" + document.getElementById('cmbSTARTTIMEMINUTE').options[document.getElementById('cmbSTARTTIMEMINUTE').selectedIndex].value + " " + document.getElementById('cmbSTARTTIMEMERIDIAN').options[document.getElementById('cmbSTARTTIMEMERIDIAN').selectedIndex].value;			
				if(document.getElementById('cmbSYSTEMFOLLOWUPID').selectedIndex > 0)
				{
				
				  if(!isValidTime(startTime))
					{
						args.IsValid = false;
						return;
					}
					else
					{
						args.IsValid=true;
					}
				}				
			}
			//Added  by Swastika on 14th Mar'06 for Gen Iss # 2353
			function doValidationEnd(source,args)
			{	endTime	= "" + document.getElementById('cmbENDTIMEHOUR').options[document.getElementById('cmbENDTIMEHOUR').selectedIndex].value + ":" + document.getElementById('cmbENDTIMEMINUTE').options[document.getElementById('cmbENDTIMEMINUTE').selectedIndex].value + " " + document.getElementById('cmbENDTIMEMERIDIAN').options[document.getElementById('cmbENDTIMEMERIDIAN').selectedIndex].value;
			
				// if notify me is present then starttime is mandetory
				if(document.getElementById('cmbSYSTEMFOLLOWUPID').selectedIndex > 0)
				{
					if(!isValidTime(endTime))
					{
						args.IsValid = false;
						return;
					}
					else
					{
						args.IsValid = true;
					}	
				}
			
			}
			//Added  by Swastika on 14th Mar'06 for Gen Iss # 2353
			function validateTime(source,args)
			{	
				var dummyDate	= '01/01/2001';
				var unselectedTime = '-1:-1 -1';
				var dateStartTime;	
				var dateEndTime;
				var Hour = document.getElementById('cmbENDTIMEHOUR').options[document.getElementById('cmbENDTIMEHOUR').selectedIndex].value;
				var Minute = document.getElementById('cmbENDTIMEMINUTE').options[document.getElementById('cmbENDTIMEMINUTE').selectedIndex].value;
				var Meridian = document.getElementById('cmbENDTIMEMERIDIAN').options[document.getElementById('cmbENDTIMEMERIDIAN').selectedIndex].value;
				
				endTime	= "" + document.getElementById('cmbENDTIMEHOUR').options[document.getElementById('cmbENDTIMEHOUR').selectedIndex].value + ":" + document.getElementById('cmbENDTIMEMINUTE').options[document.getElementById('cmbENDTIMEMINUTE').selectedIndex].value + " " + document.getElementById('cmbENDTIMEMERIDIAN').options[document.getElementById('cmbENDTIMEMERIDIAN').selectedIndex].value;
				if ((endTime==unselectedTime) || (Hour== -1) || (Minute == -1) || (Meridian== -1))
				{	
					document.getElementById('cfvCheckTime').style.display="none";
					
				}
				else
				{
									
					startTime		= "" + document.getElementById('cmbSTARTTIMEHOUR').options[document.getElementById('cmbSTARTTIMEHOUR').selectedIndex].value + ":" + document.getElementById('cmbSTARTTIMEMINUTE').options[document.getElementById('cmbSTARTTIMEMINUTE').selectedIndex].value + " " + document.getElementById('cmbSTARTTIMEMERIDIAN').options[document.getElementById('cmbSTARTTIMEMERIDIAN').selectedIndex].value;
					endTime			= "" + document.getElementById('cmbENDTIMEHOUR').options[document.getElementById('cmbENDTIMEHOUR').selectedIndex].value + ":" + document.getElementById('cmbENDTIMEMINUTE').options[document.getElementById('cmbENDTIMEMINUTE').selectedIndex].value + " " + document.getElementById('cmbENDTIMEMERIDIAN').options[document.getElementById('cmbENDTIMEMERIDIAN').selectedIndex].value;
					dateEndTime		= new Date(dummyDate + ' ' + endTime);
					dateStartTime	= new Date(dummyDate + ' ' + startTime);
					
					if(dateEndTime > dateStartTime)
					{	
						document.getElementById('cfvCheckTime').style.display="none";
						args.IsValid = true;
					}
					else
					{	
									
						args.IsValid = false;
						return;
					}
				}		
			}					
			
		
		function ShowPopupRulesVerify(url)
		{
		
			var nuWin=window.open(url,'BRICS','menubar=no,toolbar=no,location=no,resizable=no,scrollbars=no,status=no,width=600,height=200,top=286,left=240');
		}
		function RulesViolated()
		{
			aintCustomerID = document.getElementById("hidCUSTOMER_ID").value
			aintPolicyID = document.getElementById("hidPOLICY_ID").value
			aintPolVersionID = document.getElementById("hidPOLICY_VERSION_ID").value
			// if window is already open then first close that window then open.
			if(refWindow!=null)
			{
				refWindow.close();				
			}				
			var url;
			url="../../Application/Aspx/ShowDialog.aspx?CALLEDFROM=<%=Cms.CmsWeb.cmsbase.COMMIT_ANYWAYS_RULES%>&CUSTOMER_ID=" + aintCustomerID + "&POLICY_ID=" + aintPolicyID  + "&POLICY_VERSION_ID=" +  aintPolVersionID;			
			refWindow=window.open(url,"Brics","resizable=yes,scrollbars=yes,width=800,height=500,left=150,top=50");	
			
		}
		function ShowHideRulesLink()
		{	
			//************* Added by Manoj Rathore on 1st Jun 2009 Itrack # 5905 **********	
				EnableValidator("cfvSTARTTIME",false);
				EnableValidator("cfvENDTIME",false);
				EnableValidator("cfvCheckTime",false);
				document.getElementById('spnSTARTTIMEHOUR').style.display = "none";	
				document.getElementById('spnENDTIMEHOUR').style.display = "none";
			//***********************************************

			if(document.getElementById('hidRULES_VERIFIED').value!='1')			
				document.getElementById('imgVerifyApp').style.display="none";							
			else			
				document.getElementById('imgVerifyApp').style.display="inline";
			
			return false;
		}
			
		
		function openWindow()
			{
				var vin = '';
				if(document.getElementById("hidLISTID"))
				{
						vin = document.getElementById("hidLISTID").value;
				}
				var calUser = '-1';
				var calCustomer = '';
				var calAPP_ID = '';
				var calAPP_VERSION_ID = '';
				var calPOL_ID = '';
				var calPOL_VERSION_ID = '';
				
				if(document.getElementById('hidCUSTOMER_ID').value != '0' && document.getElementById('hidCUSTOMER_ID').value!='')
				{
					calCustomer = document.getElementById('hidCUSTOMER_ID').value;
				}
				if(document.getElementById('hidAPP_ID').value != '0' && document.getElementById('hidAPP_ID').value!='')
				{
					calAPP_ID = document.getElementById('hidAPP_ID').value;
				}
				if(document.getElementById('hidAPP_VERSION_ID').value != '0' && document.getElementById('hidAPP_VERSION_ID').value!='')
				{
					calAPP_VERSION_ID = document.getElementById('hidAPP_VERSION_ID').value;
				}
				if(document.getElementById('hidPOLICY_ID').value != '0' && document.getElementById('hidPOLICY_ID').value!='')
				{
					calPOL_ID = document.getElementById('hidPOLICY_ID').value;
				}
				if(document.getElementById('hidPOLICY_VERSION_ID').value != '0' && document.getElementById('hidPOLICY_VERSION_ID').value!='')
				{
					calPOL_VERSION_ID = document.getElementById('hidPOLICY_VERSION_ID').value;
				}
				calUser = parent.document.getElementById('cmbAllDiary').options[parent.document.getElementById('cmbAllDiary').selectedIndex].value;
				window.open ('TransferDiaryEntry.aspx?calledfrom=<%=strCalledFrom%>&listid='+vin+'&calUser='+calUser+'&calCustomer='+calCustomer+'&calAPP_ID='+calAPP_ID+'&calAPP_VERSION_ID='+calAPP_VERSION_ID+'&calPOL_ID='+calPOL_ID+'&calPOL_VERSION_ID='+calPOL_VERSION_ID,'','width=600,height=300'); 
				return false;
			}
			
			function deleteStr()
			{
				var del="<%=delStr%>";
				if(del==1)
				{
					if(document.getElementById('hidCUSTOMER_ID').value != '0' && document.getElementById('hidCUSTOMER_ID').value!='' && "<%=strCalledFrom%>" == 'InCLT')
					parent.location="index.aspx?CalledFrom=<%=strCalledFrom%>&CUSTOMER_ID=" + document.getElementById('hidCUSTOMER_ID').value; 
					else
					parent.location="index.aspx";
				}					
			}
			function splitPolicy()
			{
				var PolicyAppNumber = document.getElementById('hidPOLICY_APP_CUSTOMER_ID_NAME').value.split('~');
				document.getElementById('hidPOLICY_ID').value		=	PolicyAppNumber[0] ;
				document.getElementById('hidPOLICY_VERSION_ID').value	=	PolicyAppNumber[1];
				document.getElementById('txtPOLICY_NUMBER').value = PolicyAppNumber[2];
				if(PolicyAppNumber[5])
				{
					document.getElementById('hidAPP_ID').value		=	PolicyAppNumber[3] ;
					document.getElementById('hidAPP_VERSION_ID').value	=	PolicyAppNumber[4];
					document.getElementById('txtAPP_NUMBER').value = PolicyAppNumber[5];
				}
				
				document.getElementById('hidCUSTOMER_ID').value = PolicyAppNumber[6];
				document.getElementById('txtCUSTOMER_NAME').value = PolicyAppNumber[7];
				document.getElementById('hidQQ_ID').value = PolicyAppNumber[8];	
				document.getElementById('txtQuote_Number').value = PolicyAppNumber[9];			
					

			}
			
			function splitQuote()
			{
				//alert(document.getElementById('hidPOLICY_APP_CUSTOMER_ID_NAME').value);
				var QuoteNumber		=	document.getElementById('hidPOLICY_APP_CUSTOMER_ID_NAME').value.split('~');
				
					
				document.getElementById('txtQuote_Number').value = QuoteNumber[1];
				document.getElementById('hidQQ_ID').value = QuoteNumber[0];	// Quote ID
				
				document.getElementById('txtPOLICY_NUMBER').value		=	QuoteNumber[4] ;
				document.getElementById('hidPOLICY_ID').value = QuoteNumber[2] ;
				
				document.getElementById('txtAPP_NUMBER').value = QuoteNumber[7];
				document.getElementById('hidAPP_ID').value = QuoteNumber[5];
							
				document.getElementById('txtCUSTOMER_NAME').value = QuoteNumber[9];	
				document.getElementById('hidCUSTOMER_ID').value = QuoteNumber[8];
				
				document.getElementById('hidAPP_VERSION_ID').value = QuoteNumber[6];	
				document.getElementById('hidPOLICY_VERSION_ID').value = QuoteNumber[3];						
								
			}
			
			function splitApplication()
			{
				
				var AppNumberId		=	document.getElementById('hidAPP_NUMBER_ID').value.split('~');
				//alert(AppNumberId);
				
				
				document.getElementById('hidAPP_ID').value		=	AppNumberId[0] ;
				document.getElementById('hidAPP_VERSION_ID').value	=	AppNumberId[1];
				//Added for Itrack Issue 6576 on 19 Oct 09
				if(typeof(AppNumberId[2])!= 'undefined')
				{
				 document.getElementById('txtAPP_NUMBER').value = AppNumberId[2];
				}
				/*var CustomerIdName	=	document.getElementById('hidCUSTOMER_ID_NAME').value.split('~');*/
				if(AppNumberId.length>3)
				{
					document.getElementById('hidCUSTOMER_ID').value = AppNumberId[3];
					document.getElementById('txtCUSTOMER_NAME').value = AppNumberId[4];
				}
				//////////////////////////								
				
				if(AppNumberId.length>5)
				{
					document.getElementById('hidPOLICY_ID').value = AppNumberId[5] ;				
					document.getElementById('hidPOLICY_VERSION_ID').value = AppNumberId[6];					
					document.getElementById('txtPOLICY_NUMBER').value		=	AppNumberId[7] ;
				}
				
				if(AppNumberId.length>8)
				{
					document.getElementById('hidQQ_ID').value = AppNumberId[8];	
					document.getElementById('txtQuote_Number').value = AppNumberId[9];	
				}					
								
			}
			
			function splitCustomer()
			{
				document.getElementById('hidPOLICY_ID').value		=	'0' ;
				document.getElementById('hidPOLICY_VERSION_ID').value	=	'0';
				document.getElementById('txtPOLICY_NUMBER').value = '';
				
				document.getElementById('hidAPP_ID').value		=	'0';
				document.getElementById('hidAPP_VERSION_ID').value	=	'0';
				document.getElementById('txtAPP_NUMBER').value = '';
				
				document.getElementById('txtQuote_Number').value = '';
				document.getElementById('hidQQ_ID').value = '0';					
							
								
			}
			function OpenAppLookup(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction)
			{
			    var custID = document.getElementById('hidCUSTOMER_ID').value;
			    var str_app = document.getElementById('hid_app').value;
				if(custID == null || custID == '' || custID == '0')
//					OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','APP_NUMBER_ID','APP_CUSTOMER_ID_NAME','txtAPP_NUMBER','hidAPP_NUMBER_ID','ApplicationCustInfo','Application','','splitApplication()');
				    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'APP_NUMBER_ID', 'APP_CUSTOMER_ID_NAME', 'txtAPP_NUMBER', 'hidAPP_NUMBER_ID', 'ApplicationCustInfoAll', str_app, '', 'splitApplication()');
					
				else
				    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'APP_NUMBER_ID', 'APP_CUSTOMER_ID_NAME', 'txtAPP_NUMBER', 'hidAPP_NUMBER_ID', 'ApplicationCustInfo', str_app, '@CUSTID=' + custID, 'splitApplication()');
//					OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','APP_NUMBER_ID','CUSTOMER_ID_NAME','hidAPP_NUMBER_ID','hidCUSTOMER_ID_NAME','ApplicationCustomer','Application','@CUSTID=' + custID,'splitApplication()');
			}
					
			
			function OpenPolicyLookup(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction)
			{
				var custID = document.getElementById('hidCUSTOMER_ID').value;
				var appID = document.getElementById('hidAPP_ID').value;
				var appVersionID = document.getElementById('hidAPP_VERSION_ID').value;
				var str_pol = document.getElementById('hid_policy').value;

				if (custID == null || custID == '' || custID == '0') 
				{
				   
  					//OpenLookupWithFunction(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction)
				    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'POLICY_APP_CUSTOMER_ID_NAME', 'POLICY_APP_CUSTOMER_ID_NAME', 'txtPOLICY_NUMBER', 'hidPOLICY_APP_CUSTOMER_ID_NAME', 'PolicyCustInfo', str_pol, '', 'splitPolicy()');
//					OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','POLICY_APP_NUMBER','CUSTOMER_ID_NAME','hidPOLICY_APP_NUMBER','hidCUSTOMER_ID_NAME','Policy','Policy','','splitPolicy()');
				}
				else
				{
						if(appID == null || appID == '' || appID == '0')
//						OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','POLICY_APP_NUMBER','POLICY_APP_CUSTOMER_ID_NAME','txtPOLICY_NUMBER','hidPOLICY_APP_CUSTOMER_ID_NAME','PolicyCustInfo','Policy','@customerID1='+custID,'splitPolicy()');
						    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'POLICY_APP_CUSTOMER_ID_NAME', 'POLICY_APP_CUSTOMER_ID_NAME', 'txtPOLICY_NUMBER', 'hidPOLICY_APP_CUSTOMER_ID_NAME', 'PolicyCustomer', str_pol, '@customerID1=' + custID, 'splitPolicy()');

					else
					{
					    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'POLICY_APP_NUMBER', 'POLICY_APP_CUSTOMER_ID_NAME', 'txtPOLICY_NUMBER', 'hidPOLICY_APP_CUSTOMER_ID_NAME', 'PolicyCustomerApplication', str_pol, '@customerID2=' + custID + ';@APPID=' + appID + ';@APPVERSION=' + appVersionID, 'splitPolicy()');
//						OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','POLICY_APP_NUMBER','CUSTOMER_ID_NAME','hidPOLICY_APP_NUMBER','hidCUSTOMER_ID_NAME','PolicyCustInfo','Policy','@customerID2='+custID+';@APPID='+appID+';@APPVERSION='+appVersionID,'splitPolicy()');
					}
				}
			}
			
			function OpenQuoteLookup(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction)
			{
				var custID = document.getElementById('hidCUSTOMER_ID').value;
				var appID = document.getElementById('hidAPP_ID').value;
				var appVersionID = document.getElementById('hidAPP_VERSION_ID').value;
				var str_quote = document.getElementById('hid_Quote').value;			
				if(custID == null || custID == '' || custID == '0')
				{
				    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'QQ_NUMBER', 'POLICY_APP_CUSTOMER_ID_NAME', 'txtQUOTE_NUMBER', 'hidPOLICY_APP_CUSTOMER_ID_NAME', 'Quote', str_quote, '', 'splitQuote()');
				}
				else
				{
				    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'QQ_NUMBER', 'POLICY_APP_CUSTOMER_ID_NAME', 'txtQUOTE_NUMBER', 'hidPOLICY_APP_CUSTOMER_ID_NAME', 'QuoteCust', str_quote, '@CUSTID=' + custID, 'splitQuote()');				
					//if(appID == null || appID == '' || appID == '0')
					//	OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','QQ_NUMBER','CUSTOMER_NAME','txtQUOTE_NUMBER','hidPOLICY_APP_CUSTOMER_ID_NAME','Quote','Policy','@customerID1='+custID,'splitQuote()');

					//else
					//{
					//	OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','QQ_NUMBER','CUSTOMER_NAME','hidPOLICY_APP_CUSTOMER_ID_NAME','txtQUOTE_NUMBER','Quote','Policy','@customerID2='+custID+';@APPID='+appID+';@APPVERSION='+appVersionID,'splitQuote()');
					//}
				}
			}	
						
			
			function OpenCustomerLookup()
			{
			    var str = document.getElementById('hid_customer').value;
					OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','CUSTOMER_ID','Name','hidCustomer_ID','txtCUSTOMER_NAME','CustLookupForm',str,'','splitCustomer()');
			}
			
			function OnSelectedIndexChange()
			{
				setTimeRow();
			}
			
			function setTimeRow()
			{
				if(document.getElementById('cmbSYSTEMFOLLOWUPID').selectedIndex > 0)
				{
					document.getElementById('timeRow').style.display="inline";					
					document.getElementById('cfvSTARTTIME').setAttribute('enabled',true);	
					document.getElementById('cfvENDTIME').setAttribute('enabled',true);	
					document.getElementById('cfvCheckTime').setAttribute('enabled',true);
				}
				else
				{
					document.getElementById('timeRow').style.display="none";					
					document.getElementById('cfvSTARTTIME').setAttribute('enabled',false);	
					document.getElementById('cfvENDTIME').setAttribute('enabled',false);	
					document.getElementById('cfvCheckTime').setAttribute('enabled',false);
				}
			}	
			function CheckPolicy()
			{
				if (document.getElementById("hidPOLICY_ID").value =="" || document.getElementById("hidPOLICY_ID").value =="0")
				{
				  //Done for Itrack Issue 6575 on 4 Nov 09 - Button visiblity was throwing runtime error when we remove button permissions
				  try
				  {
				    document.getElementById("btnPolicy").style.visibility="hidden";
				  }
				  catch(err)
				  {
				  }
				}
				else
				{
				  //Done for Itrack Issue 6575 on 4 Nov 09 - Button visiblity was throwing runtime error when we remove button permissions
				  try
				  {
					document.getElementById("btnPolicy").style.visibility="visible";
				  }
				  catch(err)
				  {
				  }
				}
			}
			function CheckClaim()
			{
				if (document.getElementById("txtCLAIM_NUMBER").value =="")
				{
				  //Done for Itrack Issue 6575 on 4 Nov 09 - Button visiblity was throwing runtime error when we remove button permissions
				  try
				  {
					document.getElementById("btnGoToClaim").style.visibility="hidden";
				  }
				  catch(err)
				  {
				  }
				}
				else
				{
				  //Done for Itrack Issue 6575 on 4 Nov 09 - Button visiblity was throwing runtime error when we remove button permissions
				  try
				  {
					document.getElementById("btnGoToClaim").style.visibility="visible";
				  }
				  catch(e)
				  {
				  }
				}
			}
			function ResetTheForm()
			{
				DisableValidators();
				populateXML();
				ApplyColor();
				deleteStr();
				CheckPolicy();
				CheckClaim();			
				return false;
			}
			
			
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" class="bodyBackGround" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();deleteStr();CheckPolicy();CheckClaim();"
		MS_POSITIONING="GridLayout">
		<form id="DiaryForm" method="post" runat="server">
			<TABLE width="100%" align="center" border="0">
				<tr>
					<td class="pageHeader" colSpan="4"><asp:Label ID='capheader' runat='server'></asp:Label> </td>
					<%--Please note that all fields marked with * are mandatory--%>
				</tr>
				<tr>
					<td class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" Visible="False" Runat="server" CssClass="errmsg"></asp:label></td>
				</tr>
				<tr>
					<td class="midcolora"><asp:label id="capTOUSERID" Runat="server"></asp:label><span class="mandatory">*</span>
					</td>
					<td class="midcolora">
					<%--Done for Itrack Issue 6658 on 3 Nov 09--%>
					<SELECT id="cmbTOUSERID" onfocus="SelectComboIndex('cmbTOUSERID')" runat="server"></SELECT>
					<%--<asp:dropdownlist id="cmbTOUSERID" Runat="server"></asp:dropdownlist>--%>
					<br><asp:requiredfieldvalidator id="rfvTOUSERID" Runat="server" Display="Dynamic" ControlToValidate="cmbTOUSERID"></asp:requiredfieldvalidator></td><!-- Added by Charles on 14-Sep-09 for Itrack 6401 -->
					<td class="midcolora"><asp:label ID="capRecorded" runat="server"></asp:label></td><%--Recorded by--%>
					<td class="midcolora"><asp:label id="lblFROMUSERNAME" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="midcolora" noWrap><asp:label id="capLISTTYPEID" Runat="server"></asp:label><span class="mandatory">*</span></td>
					<td class="midcolora"><asp:dropdownlist id="cmbLISTTYPEID" runat="server"></asp:dropdownlist><br>
						<asp:requiredfieldvalidator id="rfvLISTTYPEID" Runat="server" Display="Dynamic" ControlToValidate="cmbLISTTYPEID"></asp:requiredfieldvalidator></td>
					<td class="midcolora" noWrap><asp:label id="capPRIORITY" Runat="server"></asp:label></td>
					<td class="midcolora"><asp:dropdownlist id="cmbPRIORITY" Runat="server"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td class="midcolora"><asp:label id="capFOLLOWUPDATE" Runat="server"></asp:label><span class="mandatory">*</span></td>
					<td class="midcolora"><asp:textbox id="txtFOLLOWUPDATE" Runat="server" MaxLength="10" Width="70"></asp:textbox><asp:hyperlink id="hlkCalandarDate" runat="server" CssClass="HotSpot">
							<asp:image id="imgCalenderExp" runat="server" ImageUrl="../Images/CalendarPicker.gif"></asp:image>
						</asp:hyperlink><br>
						<asp:requiredfieldvalidator id="rfvFOLLOWUPDATE" Runat="server" Display="Dynamic" ControlToValidate="txtFOLLOWUPDATE"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revFOLLOWUPDATE" Runat="server" Display="Dynamic" ControlToValidate="txtFOLLOWUPDATE"></asp:regularexpressionvalidator>
					<td class="midcolora" noWrap><asp:label id="capNOTIFICATIONLIST" Runat="server"></asp:label></td>
					<td class="midcolora"><asp:dropdownlist id="cmbSYSTEMFOLLOWUPID" Runat="server" onchange="JavaScript:OnSelectedIndexChange();"></asp:dropdownlist></td>
				</tr>
				<tr id="timeRow">
					<td class="midcolora" noWrap><asp:label id="capSTARTTIMEHOUR" Runat="server"></asp:label><span class="mandatory" id="spnSTARTTIMEHOUR">*</span></td>
					<td class="midcolora"><asp:dropdownlist id="cmbSTARTTIMEHOUR" Runat="server"></asp:dropdownlist>:
						<asp:dropdownlist id="cmbSTARTTIMEMINUTE" Runat="server"></asp:dropdownlist>-
						<asp:dropdownlist id="cmbSTARTTIMEMERIDIAN" Runat="server"></asp:dropdownlist><br>
						<asp:customvalidator id="cfvSTARTTIME" runat="server" Display="Dynamic" ControlToValidate="cmbSTARTTIMEMERIDIAN"
							ClientValidationFunction="doValidationStart"></asp:customvalidator></td>
					<td class="midcolora" noWrap><asp:label id="capENDTIMEHOUR" Runat="server"></asp:label><span class="mandatory" id="spnENDTIMEHOUR">*</span></td>
					<td class="midcolora"><asp:dropdownlist id="cmbENDTIMEHOUR" Runat="server"></asp:dropdownlist>:
						<asp:dropdownlist id="cmbENDTIMEMINUTE" Runat="server"></asp:dropdownlist>-
						<asp:dropdownlist id="cmbENDTIMEMERIDIAN" Runat="server"></asp:dropdownlist><br>
						<asp:customvalidator id="cfvENDTIME" runat="server" Display="Dynamic" ControlToValidate="cmbENDTIMEMERIDIAN"
							ClientValidationFunction="doValidationEnd"></asp:customvalidator><asp:customvalidator id="cfvCheckTime" Runat="server" Display="Dynamic" ControlToValidate="cmbENDTIMEMERIDIAN"
							ClientValidationFunction="validateTime"></asp:customvalidator></td>
				</tr>
				<tr>
					<td class="midcolora"><asp:label id="capCUSTOMER_NAME" Runat="server"></asp:label></td>
					<td class="midcolora"><asp:textbox id="txtCUSTOMER_NAME" runat="server" Columns="30" ReadOnly="true"></asp:textbox><IMG id="imgCustomer" style="CURSOR: hand" src="/cms/cmsweb/images/selecticon.gif" runat="server"></td>
					<td class="midcolora"><asp:label id="capAPP_NUMBER" Runat="server"></asp:label></td>
					<td class="midcolora"><asp:textbox id="txtAPP_NUMBER" Runat="server" MaxLength="50" Columns="30" ReadOnly="True" size="15"></asp:textbox><IMG id="imgApplication" style="CURSOR: hand" onclick="OpenAppLookup()" src="/cms/cmsweb/images/selecticon.gif">
					</td>
				</tr>
				<tr>
					<td class="midcolora"><asp:label id="capPOLICY_NUMBER" Runat="server"></asp:label></td>
					<td class="midcolora"><asp:textbox id="txtPOLICY_NUMBER" runat="server" Columns="30" ReadOnly="true"></asp:textbox><IMG id="imgPolicy" style="CURSOR: hand" onclick="OpenPolicyLookup()" src="/cms/cmsweb/images/selecticon.gif">
						<cmsb:cmsbutton id="btnPolicy" runat="server" CssClass="clsButton" Text="Go"></cmsb:cmsbutton></td>
					<td class="midcolora"><asp:label id="capCLAIM_NUMBER" Runat="server"></asp:label></td>
					<td class="midcolora"><asp:textbox id="txtCLAIM_NUMBER" ReadOnly="True" runat="server" MaxLength="20" size="15"></asp:textbox><cmsb:cmsbutton id="btnGoToClaim" runat="server" CssClass="clsButton" Text="Go"></cmsb:cmsbutton></td>
				</tr>
				<tr id="trQUOTE_NUMBER" runat="server">
					<td class="midcolora"><asp:label id="capQUOTE_NUMBER" Runat="server"></asp:label></td>
					<td class="midcolora"><asp:textbox id="txtQUOTE_NUMBER" runat="server" Columns="11" ReadOnly="true"></asp:textbox><IMG id="imgQuote" style="CURSOR: hand" onclick="OpenQuoteLookup()" src="/cms/cmsweb/images/selecticon.gif">
					</td>
					<td class="midcolora"></td>
					<td class="midcolora"></td>
				</tr>
				<tr>
					<td class="midcolora"><asp:label id="capSUBJECTLINE" Runat="server"></asp:label><span class="mandatory">*</span></td>
					<td class="midcolora" colSpan="3"><asp:textbox id="txtSUBJECTLINE" Runat="server" MaxLength="50" size="93"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvSUBJECTLINE" Runat="server" Display="Dynamic" ControlToValidate="txtSUBJECTLINE"></asp:requiredfieldvalidator></td>
				</tr>
				<tr>
					<td class="midcolora"><asp:label id="capNOTE" Runat="server"></asp:label></td>
					<TD class="midcolora" colSpan="4"><asp:textbox id="txtNOTE" runat="server" MaxLength="2000" Width="480px" Height="128px" TextMode="MultiLine"></asp:textbox></TD>
					<td class="midcolora"><A href="javascript:RulesViolated();"><asp:image id="imgVerifyApp" Visible="True" Runat="server" Height="15" ToolTip="Rules Violated"
								ImageAlign="absMiddle" BorderStyle="None" BorderWidth="0"></asp:image></A></td>
				</tr>
				<tr>
					<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="Server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="Server" Text="Delete" CausesValidation="False"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnComplete" runat="Server" Text="Complete"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnTransfer" runat="Server" Text="Transfer" causesValidation="false"></cmsb:cmsbutton>
						<!--		<input type="button" class="clsButton" id="btnTransfer2" value="Transfer" onclick="openWindow()">--></td>
					<TD class="midcolorr" align="Right"  rowSpan="1" nowrap><cmsb:cmsbutton id="btnReminder" runat="server" CssClass="clsButton" Text="Save and Send Reminder"></cmsb:cmsbutton><cmsb:cmsbutton id="btnSave" runat="server" CssClass="clsButton" Text="Save"></cmsb:cmsbutton></TD>
					<td class="midcolorr"></td>
				</tr>
			</TABLE>
			<input id="hidFROMUSERNAME" type="hidden" name="hidFROMUSERNAME" runat="server">
			<input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<input id="hidCustomInfo" type="hidden" value="0" name="hidCustomInfo" runat="server">
			<input id="hidRowId" type="hidden" value="0" name="hidRowId" runat="server"> <input id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<input id="hidOperation" type="hidden" name="hidOperation" runat="server"> <input id="hidLISTOPEN" type="hidden" name="hidLISTOPEN" runat="server">
			<input id="hidLISTID" type="hidden" name="hidLISTID" runat="server"> <input id="hidRECDATE" type="hidden" name="hidRECDATE" runat="server">
			<input id="hidFROMUSERID" type="hidden" name="hidFROMUSERID" runat="server"> <input id="hidSYSTEMFOLLOWUPID" type="hidden" name="hidSYSTEMFOLLOWUPID" runat="server">
			<input id="hidTYPEDESC" type="hidden" name="hidTYPEDESC" runat="server"> <input id="hidSTARTTIMEHOUR" type="hidden" name="hidSTARTTIMEHOUR" runat="server">
			<input id="hidSTARTTIMEMINUTE" type="hidden" name="hidSTARTTIMEMINUTE" runat="server">
			<input id="hidENDTIMEHOUR" type="hidden" name="hidENDTIMEHOUR" runat="server"> <input id="hidENDTIMEMINUTE" type="hidden" name="hidENDTIMEMINUTE" runat="server">
			<input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			<input id="hidAPP_ID" type="hidden" value="0" name="hidAPP_ID" runat="server"> <input id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			<input id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
			<input id="hidAPP_VERSION_ID" type="hidden" value="0" name="hidAPP_VERSION_ID" runat="server">
			<input id="hidAPP_NUMBER_ID" type="hidden" name="hidAPP_NUMBER_ID" runat="server">
			<input id="hidCUSTOMER_ID_NAME" type="hidden" name="hidCUSTOMER_ID_NAME" runat="server">
			<input id="hidPOLICY_APP_CUSTOMER_ID_NAME" type="hidden" name="hidPOLICY_APP_CUSTOMER_ID_NAME"
				runat="server"> <input id="hidPOLICY_APP_NUMBER" type="hidden" name="hidPOLICY_APP_NUMBER" runat="server">
			<input id="hidRULES_VERIFIED" type="hidden" name="hidRULES_VERIFIED" runat="server">
			<input id="hidQQ_ID" type="hidden" name="hidQuote_number" runat="server">
			<input id="hidCLAIM_ID" type="hidden" name="hidCLAIM_ID" runat="server">
			<input id="hidFollow_Up_Date" type="hidden" name="hidFollow_Up_Date" runat="server">
			<input id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"><%--Done for Itrack Issue 6658 on 3 Nov 09--%>
			<input id="hid_customer" type="hidden" runat="server" />
			<input id="hid_policy" type="hidden" runat="server" />
			<input id="hid_Quote" type="hidden" runat="server" />
			<input id="hid_app" type="hidden" runat="server" />
			
		</form>
		<script>
			if(document.getElementById('hidFormSaved').value == '1')
			{
				// set this to make date bold
				parent.gAppDatesGlobal = '<%=gStrAppDates%>';
				//set this to make counts of pending tasks
				parent.gStrEEC	= '<%=gStrEEC%>';
				parent.gStrQPAC = '<%=gStrQPAC%>';
				parent.gStrQPBC = '<%=gStrQPBC%>';
				parent.gStrRRC	= '<%=gStrRRC%>';
				parent.gStrBRC	= '<%=gStrBRC%>';
				parent.gStrERC	= '<%=gStrERC%>';
				parent.gStrAAC	= '<%=gStrAAC%>';
				parent.cStrCRE	= '<%=cStrCRE%>';
				parent.cStrANF	= '<%=cStrANF%>';
				parent.cStrCF	= '<%=cStrCF%>';
				parent.listtypeid1 = '<%=listtypeid1%>';
				parent.listtypeid2 = '<%=listtypeid2%>';
				parent.listtypeid3 = '<%=listtypeid3%>';
				parent.listtypeid4 = '<%=listtypeid4%>';
				parent.listtypeid5 = '<%=listtypeid5%>';
				parent.listtypeid6 = '<%=listtypeid6%>';
				parent.listtypeid7 = '<%=listtypeid7%>';
				parent.listtypeid8 = '<%=listtypeid8%>';
				parent.listtypeid9 = '<%=listtypeid9%>';
				parent.listtypeid10 = '<%=listtypeid10%>';
				//call this function to make date bold in calendar
				parent.fPopCalendar(parent.dc,parent.dc);
				
				//call this to set counts of pending task
				parent.writePendingTask();
}

			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidLISTID').value,true);
		</script>
	</BODY>
</HTML>

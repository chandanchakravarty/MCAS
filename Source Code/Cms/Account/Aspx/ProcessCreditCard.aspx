<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="ProcessCreditCard.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.ProcessCreditCard" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProcessCreditCard</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta http-equiv="Page-Enter" content="revealtrans(duration=0.0)">
		<meta http-equiv="Page-Exit" content="revealtrans(duration=0.0)">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript" src="/cms/cmsweb/Scripts/Calendar.js"></script>
		<script src="../../cmsweb/scripts/AJAXcommon.js"></script>
		<script language="javascript">
		
		//This function open the policy lookup window
		function OpenPolicyLookup()
		{
			var url='<%=URL%>';
			txtCtrl = "txtPOLICY_NUMBER";			
			OpenLookupWithFunction(url,'POLICY_APP_NUMBER','CUSTOMER_ID_NAME','hidPOLICYINFO',txtCtrl,'DBPolicy','Policy','','splitPolicy()');
		}
		
		//To reset
		function ResetForm()
	    {
			DisableValidators();
			
			document.PROCESS_CREDIT_CARD.reset();
			//Itrack #3837 Credit Card screen, once we process credit card transaction and it is succefully processed, 
			//then do not show Process Tranaction button. Let user start a new transaction for this.
			
			//Done for Itrack Issue 6099 on 14 July 2009
			//document.getElementById('btnCCSave').setAttribute('disabled',false)
			document.getElementById('btnSave').setAttribute('disabled',false)
			
			if(document.getElementById('lblMessage')!=null)
				document.getElementById('lblMessage').innerText = '';
			document.getElementById('txtPOLICY_NUMBER').innerText = '';
			document.getElementById('txtAMOUNT').innerText = '';
			document.getElementById('txtREFERENCE_ID').innerText = '';
			document.getElementById('cmbCARD_TYPE').selectedIndex=-1;
			document.getElementById('txtCARD_NO').innerText = '';
			document.getElementById('txtCARD_CVV_NUMBER').innerText = '';
			document.getElementById('txtCARD_DATE_VALID_TO').innerText = '';
			document.getElementById('lblTOTAL_DUE').innerText = '';
			document.getElementById('lblMIN_DUE').innerText = '';
			
			document.getElementById('txtCUSTOMER_FIRST_NAME').innerText = '';
			document.getElementById('txtCUSTOMER_MIDDLE_NAME').innerText = '';
			document.getElementById('txtCUSTOMER_LAST_NAME').innerText = '';
			document.getElementById('txtCUSTOMER_ADDRESS1').innerText = '';
			document.getElementById('txtCUSTOMER_ADDRESS2').innerText = '';
			document.getElementById('txtCUSTOMER_CITY').innerText = '';
			//document.getElementById('cmbCUSTOMER_COUNTRY').selectedIndex=-1;
			document.getElementById('cmbCUSTOMER_STATE').selectedIndex=-1;
			document.getElementById('txtCUSTOMER_ZIP').innerText = '';
			document.getElementById('txtNOTE').innerText = '';
			
			document.getElementById('CC_1').style.display = 'inline';	
			document.getElementById('CC_2').style.display = 'inline';	
			
			//Added by Shikha on 20/03/09.
			if(document.getElementById('btnCCProcessing')!=null)
				document.getElementById('btnCCProcessing').style.display="none";
				
			//Done for Itrack Issue 6099 on 14 July 2009
			/*if(document.getElementById('btnCCSave')!=null)
				{
					document.getElementById('btnCCSave').style.display="inline";
					//document.getElementById('btnSave').disabled="false";
				}*/
			if(document.getElementById('btnSave')!=null)
				{
					document.getElementById('btnSave').style.display="inline";
					//document.getElementById('btnSave').disabled="false";
				}
			//End of addition.
          
	      ChangeColor();
	      fillstateFromCountry();
	      return false;
	   }
		//This function splits the policy id and policy version id and put it in different controls
		function splitPolicy()
		{
			var txtCtrl = "txtPOLICY_NUMBER";			
			if (document.getElementById("hidPOLICYINFO").value.length > 0)
			{
				var arr = document.getElementById("hidPOLICYINFO").value.split("~");
				document.getElementById(txtCtrl).value = arr[2]; // Policy Number
				var PolNum = arr[2];
				var CustId = arr[6]; 
				var PolID = arr[0];
				var PolVerID = arr[1];
				document.getElementById('hidCUSTOMER_ID').value = CustId;
				document.getElementById('hidPOLICY_ID').value = PolID;
				document.getElementById('hidPOLICY_VERSION_ID').value = PolVerID;
				document.getElementById('hidCUSTOMER_NAME').value = arr[3];
				FetchXML(PolVerID,PolID,CustId,PolNum,'');
				//For Customer Info
				FetchCustomerXML(PolNum);
				
			}
		}
		// AJAX Function Call
		function FetchXML(PolVerID,PolID,CustId,PolNum)
		{
			var ParamArray = new Array();
			obj1=new Parameter('POLICY_VERSION_ID',PolVerID);
			obj2=new Parameter('POLICY_ID',PolID);
			obj3=new Parameter('CUSTOMER_ID',CustId);
			obj4=new Parameter('POLICY_NUMBER',PolNum);
			obj5=new Parameter('CALLED_FROM','');
			ParamArray.push(obj1);
			ParamArray.push(obj2);
			ParamArray.push(obj3);
			ParamArray.push(obj4);
			ParamArray.push(obj5);
			var objRequest = _CreateXMLHTTPObject();
			var Action = 'BAL';
			_SendAJAXRequest(objRequest,'BAL',ParamArray,CallbackFun);
	
		}
		//Added for Customer Info
		// AJAX Function Call
		function FetchCustomerXML(PolNum)
		{
			var ParamArray = new Array();
			obj1=new Parameter('POLICY_NUMBER',PolNum);
			ParamArray.push(obj1);
			var objRequest = _CreateXMLHTTPObject();
			var Action = 'CUST_INFO';
			_SendAJAXRequest(objRequest,Action,ParamArray,CallbackFunCust);
	
		}
		function CallbackFunCust(AJAXREsponse)
		{
			if(AJAXREsponse != '')
			{
				FillCustomerInfo(AJAXREsponse);			
			}
							
		}
		
		function FillCustomerInfo(XML)
		{	

		var _DataSet = '<NewDataSet />';
		if(XML!="" && XML!=_DataSet)
		{
					
			var objXmlHandler = new XMLHandler();
			var tree = (objXmlHandler.quickParseXML(XML).getElementsByTagName('Table')[0]);
			var i=0;						
			for(i=0;i<tree.childNodes.length;i++)
			{						
				var nodeName = tree.childNodes[i].nodeName;
				
				var nodeValue ;
				if ( tree.childNodes[i].firstChild == 'undefined' || tree.childNodes[i].firstChild == null )
				{
					nodeValue = '';
				}
				else
				{					
					
					tree.childNodes[i].firstChild.text = tree.childNodes[i].firstChild.text.replace("&amp;","&");					
					nodeValue = tree.childNodes[i].firstChild.text;
				}				
				switch(nodeName)
				{
				case "CUSTOMER_FIRST_NAME":
					document.getElementById('txtCUSTOMER_FIRST_NAME').value = nodeValue;
					break;
				case "CUSTOMER_MIDDLE_NAME":
					 document.getElementById('txtCUSTOMER_MIDDLE_NAME').value = nodeValue;
					 break;
				case "CUSTOMER_LAST_NAME":	
					 document.getElementById('txtCUSTOMER_LAST_NAME').value = nodeValue;
					 break;	
				case "CUSTOMER_ADDRESS1":
					 document.getElementById('txtCUSTOMER_ADDRESS1').value = nodeValue;
					 break;
				case "CUSTOMER_ADDRESS2":
					  document.getElementById('txtCUSTOMER_ADDRESS2').value = nodeValue;
					 break;		
				case "CUSTOMER_CITY":
					  document.getElementById('txtCUSTOMER_CITY').value = nodeValue;
					  break;							
				case "CUSTOMER_COUNTRY":
					  SelectComboOption("cmbCUSTOMER_COUNTRY",nodeValue)
					  //fillstateFromCountry();
					  break;
				case "CUSTOMER_STATE":
					  SelectComboOption("cmbCUSTOMER_STATE",nodeValue)					    					 					  
					  //alert(document.getElementById('cmbCUSTOMER_STATE').value);
					  //SetRegisteredState(nodeValue);
					  break;
				case "CUSTOMER_ZIP":
					   document.getElementById('txtCUSTOMER_ZIP').value = nodeValue;
					   break;
				 }	
			}	
		 }	
			return false;
							
	   }
	   
	   function  SetRegisteredState(nodeValue)
	   {
	     SelectComboOption("cmbCUSTOMER_STATE",nodeValue)
	   }
		
		
		// AJAX Response function
		function CallbackFun(AJAXREsponse)
		{	
			
			if(AJAXREsponse == '0')
			{
				alert('Invalid Policy Number. Please enter only DB type Policy Number.')
				document.getElementById('lblTOTAL_DUE').innerText = '';
				document.getElementById('lblMIN_DUE').innerText = '';
				document.getElementById('txtPOLICY_NUMBER').value = '';
				document.getElementById('hidCUSTOMER_ID').value = '';
				document.getElementById('hidPOLICY_ID').value = '';
				document.getElementById('hidPOLICY_VERSION_ID').value = '';
				return false;
			}
			if(AJAXREsponse == "-1")
			{
				alert('Entered Policy Number is of AB type. Please enter only DB type Policy Number.');
				document.getElementById('lblTOTAL_DUE').innerText = '';
				document.getElementById('lblMIN_DUE').innerText = '';
				document.getElementById('txtPOLICY_NUMBER').value = '';
				document.getElementById('hidCUSTOMER_ID').value = '';
				document.getElementById('hidPOLICY_ID').value = '';
				document.getElementById('hidPOLICY_VERSION_ID').value = '';
				return false;					
			}	
			else
			{
				var strResponse = AJAXREsponse.split('~'); // AJAXREsponse contains Total Due & Min Due separated by '~'
				var TotalDue = document.getElementById('lblTOTAL_DUE');	
				if(TotalDue !=null)
					TotalDue.innerText = formatAmount(strResponse[0]);
				var MinDue = document.getElementById('lblMIN_DUE');
				if(MinDue !=null)
					MinDue.innerText = formatAmount(strResponse[1]);
				document.getElementById('hidCUSTOMER_ID').value = strResponse[6];
				document.getElementById('hidPOLICY_ID').value = strResponse[7];
				document.getElementById('hidPOLICY_VERSION_ID').value = strResponse[8];
				document.getElementById('hidCUSTOMER_NAME').value = strResponse[3];
			}
		}
		
		//DB policies
		function GetPolicyStatus(PolNum) {
		    if(PolNum != "" && PolNum.length < 21)
			{
			    alert('Invalid Policy Number Please enter valid length of Policy Number.');
		        return false;
			}
		    if (PolNum != "")
			{
				FetchXML('','','',PolNum,'T');
				FetchCustomerXML(PolNum); //Added to Fetch Customer Info
			}
			
		}
		
	
		// Validate Credit Card Number Length	
		//changes made by uday on 23rd Nov to show different messages for different credit card.
		function ValCardNumLen(objSource, objArgs)
		{	
			var cardLen = new String(document.getElementById('txtCARD_NO').value.trim());
			document.getElementById('hidCARD_TYPE').value = document.getElementById('cmbCARD_TYPE').value;			
			if(cardLen.length != 15 && document.getElementById('hidCARD_TYPE').value == 14127)				
			{			   
				objArgs.IsValid = false;				
				document.getElementById('csvCARD_NO').innerHTML = "Please enter Credit Card # of 15 digits.";
			}
			else if(cardLen.length != 14 && document.getElementById('hidCARD_TYPE').value == 14128)				
			{			   
				objArgs.IsValid = false;				
				document.getElementById('csvCARD_NO').innerHTML = "Please enter Credit Card # of 14 digits.";
			}
			else if(cardLen.length != 16 && document.getElementById('hidCARD_TYPE').value == 14129)				
			{			   
				objArgs.IsValid = false;				
				document.getElementById('csvCARD_NO').innerHTML = "Please enter  Credit Card # of 16 digits.";
			}
			else if(cardLen.length != 15 && document.getElementById('hidCARD_TYPE').value == 14130)				
			{			   
				objArgs.IsValid = false;				
				document.getElementById('csvCARD_NO').innerHTML = "Please enter Credit Card # of 15 digits.";
			}
			else if(cardLen.length != 15 && cardLen.length != 16 && document.getElementById('hidCARD_TYPE').value == 14131)				
			{			   
				objArgs.IsValid = false;				
				document.getElementById('csvCARD_NO').innerHTML = "Please enter Credit Card # of 15 or 16 digits.";
			}
		
			else if(cardLen.length != 16 && document.getElementById('hidCARD_TYPE').value == 14124)				
			{			   
				objArgs.IsValid = false;				
				document.getElementById('csvCARD_NO').innerHTML = "Please enter Credit Card # of  16 digits.";
			}
			
			else if(cardLen.length != 13 && cardLen.length != 16 && document.getElementById('hidCARD_TYPE').value == 14125)				
			{			   
				objArgs.IsValid = false;				
				document.getElementById('csvCARD_NO').innerHTML = "Please enter Credit Card # of 13 or 16 digits.";
			}		
		
			else
			 {	
			 	objArgs.IsValid = true;
			 }
		}	
		//
		
		// Validate Credit Card CVV Number Length			
		function ValCardCVVNumLen(objSource, objArgs)
		{
		   	
			var cardCVVLen = new String(document.getElementById('txtCARD_CVV_NUMBER').value.trim());			
			if ( (cardCVVLen.length < 3) || (cardCVVLen.length > 4) )
				objArgs.IsValid = false;
			else
				objArgs.IsValid = true;
				
		}
		
		//Formats the amount and convert 111 into 1.11
		function FormatAmount(txtAmount)
		{
			ChkForNegAmt(txtAmount);
			
			if (txtAmount.value != "")
			{
				amt = txtAmount.value;
				amt = ReplaceAll(amt,".","");
				if (amt.length == 1)
					amt = amt + "0";
				if ( ! isNaN(amt))
				{
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					txtAmount.value = InsertDecimal(amt);
				}
			}
		}
		
		// Validate To Year Lenth : must be two digits for single digits also (eg : Instead of Jan -2, it shud be Jan -02)
		function chkToYearLength(objSource , objArgs)
		{
			var ToYearDigitLen = new String(document.getElementById('txtCARD_DATE_VALID_TO').value);
			if(ToYearDigitLen.length < 2)
			{
				objArgs.IsValid = false;
			}
			else
			{
				objArgs.IsValid = true;
			}
		}
	
		function ChkForNegAmt(txtAmount)
		{
			var amt = new String();
			amt = txtAmount.value;
			HodeShowForNegAmt(amt);
			
			if(amt.indexOf('-') == "0")
			{
				document.getElementById('spnREFERENCE_ID').style.display = 'inline';	
				document.getElementById('capREFERENCE_ID').style.display = 'inline';	
				document.getElementById('txtREFERENCE_ID').style.display = 'inline';	
				document.getElementById('rfvREFERENCE_ID').setAttribute('enabled',true);
				
			}
			else
			{
				document.getElementById('spnREFERENCE_ID').style.display = 'none';	
				document.getElementById('capREFERENCE_ID').style.display = 'none';	
				document.getElementById('txtREFERENCE_ID').style.display = 'none';
				document.getElementById('rfvREFERENCE_ID').style.display = 'none';	
				//document.getElementById('rfvREFERENCE_ID').setAttribute('isValid',true);
				document.getElementById('rfvREFERENCE_ID').setAttribute('enabled',false);
			}
		}
		
		function HodeShowForNegAmt(amt)
		{
			
			if(amt.indexOf('-') == "0")
			{
				document.getElementById('CC_1').style.display = 'none';	
				document.getElementById('CC_2').style.display = 'none';	
				document.getElementById('rfvCARD_TYPE').setAttribute('enabled',false);
				document.getElementById('rfvCARD_NO').setAttribute('enabled',false);
				document.getElementById('revCARD_NO').setAttribute('enabled',false);
				document.getElementById('csvCARD_NO').setAttribute('enabled',false);
				document.getElementById('rfvCARD_CVV_NUMBER').setAttribute('enabled',false);
				document.getElementById('csvCARD_CVV_NUMBER').setAttribute('enabled',false);
				document.getElementById('rfvCARD_DATE_VALID_TO').setAttribute('enabled',false);
				document.getElementById('rngCARD_DATE_VALID_TO').setAttribute('enabled',false);
				document.getElementById('csvCARD_DATE_VALID_TO').setAttribute('enabled',false);
			}
			else
			{
				document.getElementById('CC_1').style.display = 'inline';	
				document.getElementById('CC_2').style.display = 'inline';	
				document.getElementById('rfvCARD_TYPE').setAttribute('enabled',true);
				document.getElementById('rfvCARD_NO').setAttribute('enabled',true);
				document.getElementById('revCARD_NO').setAttribute('enabled',true);
				document.getElementById('csvCARD_NO').setAttribute('enabled',true);
				document.getElementById('rfvCARD_CVV_NUMBER').setAttribute('enabled',true);
				document.getElementById('csvCARD_CVV_NUMBER').setAttribute('enabled',true);
				document.getElementById('rfvCARD_DATE_VALID_TO').setAttribute('enabled',true);
				document.getElementById('rngCARD_DATE_VALID_TO').setAttribute('enabled',true);
				document.getElementById('csvCARD_DATE_VALID_TO').setAttribute('enabled',true);
			}
		}
		
		function Init()
		{
			ChkForNegAmt(document.getElementById('txtAmount'));
			 fillstateFromCountry();
			/*document.getElementById('spnREFERENCE_ID').style.display = 'none';	
			document.getElementById('capREFERENCE_ID').style.display = 'none';	
			document.getElementById('txtREFERENCE_ID').style.display = 'none';	
			document.getElementById('rfvREFERENCE_ID').setAttribute('enabled',false);*/
		}
		
		function ChkTextAreaLength(source, arguments)
		{
			var txtArea = arguments.Value;
			if(txtArea.length > 800 ) 
			{
				arguments.IsValid = false;
				return;
			}
		}	
		
		function handleResult(res) 
		{
		if(!res.error)
			{
			if (res.value!="" && res.value!=null ) 
				{
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
		
		function ChkEmpResult(objSource , objArgs)
		{
			objArgs.IsValid = true;
			if(objArgs.IsValid == true)
			{
				objArgs.IsValid = GetZipForState();
				if(objArgs.IsValid == false)
					document.getElementById('csvCUSTOMER_ZIP').innerHTML = "The zip code does not belong to the state";				
			}
			return;
			if(GlobalError==true)
			{
				Page_IsValid = false;
				objArgs.IsValid = false;
			}
			else
			{
				objArgs.IsValid = true;				
			}
			//Done for Itrack Issue 6099 on 14 July 2009
			//document.getElementById("btnCCSave").click();
			document.getElementById("btnSave").click();
		}	
	//for ZIP Details
	function GetZipForState_OLD()
		{		    
			GlobalError=true;
			if(document.getElementById('cmbCUSTOMER_STATE').value==14 ||document.getElementById('cmbCUSTOMER_STATE').value==22||document.getElementById('cmbCUSTOMER_STATE').value==49)
			{ 
				if(document.getElementById('txtCUSTOMER_ZIP').value!="")
				{
					var intStateID = document.getElementById('cmbCUSTOMER_STATE').options[document.getElementById('cmbCUSTOMER_STATE').options.selectedIndex].value;
					var strZipID = document.getElementById('txtCUSTOMER_ZIP').value;						
					var co=myTSMain1.createCallOptions();
					co.funcName = "FetchZipForState";
					co.async = false;
					co.SOAPHeader= new Object();
					var oResult = myTSMain1.FetchZip.callService(co,intStateID,strZipID);					
					handleResult(oResult);	
					if(GlobalError)
					{
						return false;
					}
					else
					{
						return true;
					}
				}	
				return false;
			}
			else 
				return true;		
		}	
		
		
			function GetZipForState()
		{		
			GlobalError=true;
			if(document.getElementById('cmbCUSTOMER_STATE').value==14 ||document.getElementById('cmbCUSTOMER_STATE').value==22||document.getElementById('cmbCUSTOMER_STATE').value==49)
			{ 
				if(document.getElementById('txtCUSTOMER_ZIP').value!="")
				{
					var intStateID = document.getElementById('cmbCUSTOMER_STATE').options[document.getElementById('cmbCUSTOMER_STATE').options.selectedIndex].value;
					var strZipID = document.getElementById('txtCUSTOMER_ZIP').value;	
					var result = ProcessCreditCard.AjaxFetchZipForState(intStateID,strZipID);
					return AjaxCallFunction_CallBack(result);
									
				}
				return false;
			}
			else 
				return true;
				
		}
		////////////////////////////////////AJAX CALLS FOR ZIP///////////////////////////
		function AjaxCallFunction_CallBack(response)
		{		
			if(document.getElementById('cmbCUSTOMER_STATE').value==14 ||document.getElementById('cmbCUSTOMER_STATE').value==22||document.getElementById('cmbCUSTOMER_STATE').value==49)
			{ 
				if(document.getElementById('txtCUSTOMER_ZIP').value!="")
				{
					handleResult(response);
					if(GlobalError)
					{
						return false;
					}
					else
					{
						return true;
					}
				}	
				return false;
			}
			else 
				return true;		
		}
		/////EMP ZIP AJAX////////////////
		//Show Hide
		function HideTransProgress()
		{
			if(document.getElementById('btnCCProcessing')!=null)
				{
				document.getElementById('btnCCProcessing').style.display="none";
				}
		}
		function HideShowTransactionInProgress()	//Changed by Shikha for #5534 on 10 Mar 2009.
		{
			//Done for Itrack Issue 6099 on 13 July 2009
			if(Page_ClientValidate())
			{
			  //Done for Itrack Issue 6099 on 14 July 2009
			  //if(document.getElementById('btnCCSave')!=null)
				//document.getElementById('btnCCSave').style.display="none";
			  if(document.getElementById('btnSave')!=null)
				document.getElementById('btnSave').style.display="none";
				
			  if(document.getElementById('btnCCProcessing')!=null)
				{
					document.getElementById('btnCCProcessing').style.display="inline";
					document.getElementById('btnCCProcessing').disabled="true";
				}
			}
			else //Done for Itrack Issue 6099 on 13 July 2009
			{
			  return false;
			}
		}
		//objTranInfo.State = Request.Form.Get("cmbCUSTOMER_STATE");
	 //Added on 25 Nov 2008 by Sibin
		
		/*function CallService()
		{
		
			fillstateFromCountry();
			//fillState(document.getElementById('hidSTATE_COUNTRY_LIST'));
			//setStateID();
		}*/
		
		
		function setStateID()
		{
			var CmbState=document.getElementById('cmbCUSTOMER_STATE');
			CmbState.options[CmbState.selectedIndex].value=document.getElementById('hidSTATE_ID').value;
			//alert(document.getElementById('hidSTATE_ID').value);
			if (CmbState==null)
				return;
			if	(CmbState.selectedIndex!=-1)		
					document.getElementById('hidSTATE_ID').value=  CmbState.options[CmbState.selectedIndex].value;
			
			
		}
		  	
	  function fillState(Result)
		{
			var strXML;
			if(Result.error)
			{        
				var xfaultcode   = Result.errorDetail.code;
				var xfaultstring = Result.errorDetail.string;
				var xfaultsoap   = Result.errorDetail.raw;        				
			}
			else	
			{	
				strXML= Result.value;	
				
				var xmlDoc = new ActiveXObject("Microsoft.XMLDOM") ;
				xmlDoc.async=false;
				xmlDoc.loadXML(strXML);
				xmlTableNodes = xmlDoc.selectNodes('/NewDataSet/Table');
				oOption = document.createElement("option");
				oOption.value = "";
	 			oOption.text = "";
				document.getElementById('cmbCUSTOMER_STATE').length=0;
				document.getElementById('cmbCUSTOMER_STATE').add(oOption);
				for(var i = 0; i < xmlTableNodes.length; i++ )
				{
					var text = 	xmlTableNodes[i].selectSingleNode('STATE_NAME').text;
					var value = 	xmlTableNodes[i].selectSingleNode('STATE_ID').text;
					
					oOption = document.createElement("option");
					oOption.value = value;
					oOption.text = text;
					document.getElementById('cmbCUSTOMER_STATE').add(oOption);
				}		
				setStateID();  
			}
			
			return false;	
		}
	
		function fillstateFromCountry()
		{	  
			    
			  	GlobalError=true;
			      var CmbState=document.getElementById('cmbCUSTOMER_STATE');
				  var CountryID=  document.getElementById('cmbCUSTOMER_COUNTRY').options[document.getElementById('cmbCUSTOMER_COUNTRY').selectedIndex].value;
				  ///alert(CountryID);
					//var co=myTSMain1.createCallOptions();
					//co.funcName = "FillState";
					//co.async = false;
					//co.SOAPHeader= new Object();					
					//var oResult = myTSMain1.FetchZip.callService(co,CountryID);
					var oResult = '';
					ProcessCreditCard.AjaxFillState(CountryID,fillState);
					//handleResult(oResult);
					fillState(oResult);
					if(GlobalError)
					{
						return false;
					}
					else
					{
						return true;
					}
	 }
	 
	 

	function fillState(Result)
	{
		var strXML;
		if(Result.error)
		{        
			var xfaultcode   = Result.errorDetail.code;
			var xfaultstring = Result.errorDetail.string;
			var xfaultsoap   = Result.errorDetail.raw;        				
		}
		else	
		{	
			strXML= Result.value;	
			
			var xmlDoc = new ActiveXObject("Microsoft.XMLDOM") ;
			xmlDoc.async=false;
			xmlDoc.loadXML(strXML);
			xmlTableNodes = xmlDoc.selectNodes('/NewDataSet/Table');
			oOption = document.createElement("option");
			oOption.value = "";
	 		oOption.text = "";
			document.getElementById('cmbCUSTOMER_STATE').length=0;
			document.getElementById('cmbCUSTOMER_STATE').add(oOption);			
			for(var i = 0; i < xmlTableNodes.length; i++ )
			{
				var text = 	xmlTableNodes[i].selectSingleNode('STATE_NAME').text;
				var value = 	xmlTableNodes[i].selectSingleNode('STATE_ID').text;
				
				oOption = document.createElement("option");
				oOption.value = value;
				oOption.text = text;
				document.getElementById('cmbCUSTOMER_STATE').add(oOption);
			}		
			setStateID();  
		}
		
		return false;	
	}
	
	function DisableZipForCanada()
		{
			var myCountry=document.getElementById("cmbCUSTOMER_COUNTRY");
			if (myCountry.selectedIndex != '-1')		//Added by Shikha on 20/03/2009.
			{
				if(myCountry.options[myCountry.selectedIndex].value=='2')
				{
					document.getElementById("revCUSTOMER_ZIP").setAttribute("enabled",false);
					document.getElementById("revCUSTOMER_ZIP").style.display = 'none';
				}
				
				else
				{
					document.getElementById("revCUSTOMER_ZIP").setAttribute("enabled",true);
				}
			}
		}
//Added till here by Sibin
		</script>
	</HEAD>
	<body class="bodyBackGround" topMargin="0" onload="Init();ApplyColor();ChangeColor();top.topframe.main1.mousein = false;findMouseIn();showScroll();HideTransProgress();DisableZipForCanada();"><%--Added DisableZipForCanada() by Sibin on 25 Nov 08--%>
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<form id='PROCESS_CREDIT_CARD' method="post" runat="server">		 
		 <P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
			<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
				<asp:panel id="pnlCCCust" Runat="server">
					<TBODY>
						<TR>
							<TD>
								<webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></TD>
						</TR>
						<TR>
							<TD class="headereffectcenter" colSpan="4">Process Credit Card</TD>
						</TR>
						<TR>
							<TD class="midcolorc" align="center" colSpan="4">
								<asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False" EnableViewState="False"></asp:label></TD>
						</TR>
						<TR>
							<TD class="midcolora" width="18%">
								<asp:Label id="capPOLICY_NUMBER" runat="server"> Policy Number :</asp:Label><SPAN class="mandatory">*</SPAN></TD>
							<TD class="midcolora" width="32%">
								<asp:textbox id="txtPOLICY_NUMBER" onblur="GetPolicyStatus(this.value);" Runat="server" size="30"
									MaxLength="21"></asp:textbox><SPAN id="spnPOLICY_NO" runat="server"><IMG id="imgPOLICY_NO" style="CURSOR: hand" onclick="OpenPolicyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
										runat="server"> </SPAN>
								<BR>
								<asp:RequiredFieldValidator id="rfvPOLICY_NUMBER" Runat="server" Display="Dynamic" ControlToValidate="txtPOLICY_NUMBER"></asp:RequiredFieldValidator></TD>
							<TD class="midcolora" width="18%">Total Due :
								<asp:Label id="lblTOTAL_DUE" Runat="server" CssClass="clsLabel"></asp:Label></TD>
							<TD class="midcolora" width="32%">Minimum Due :
								<asp:Label id="lblMIN_DUE" Runat="server" CssClass="clsLabel"></asp:Label></TD>
						</TR>
						<TR>
							<TD class="midcolora" width="18%">
								<asp:Label id="capAMOUNT" runat="server">Amount  :</asp:Label><SPAN class="mandatory">*</SPAN></TD>
							<TD class="midcolora" width="32%">
								<asp:textbox id="txtAMOUNT" onblur="FormatAmount(this)" style="TEXT-ALIGN: right" Runat="server"
									size="21" AutoComplete = "Off"></asp:textbox><BR>
								<asp:RequiredFieldValidator id="rfvAMOUNT" Runat="server" Display="Dynamic" ControlToValidate="txtAMOUNT"></asp:RequiredFieldValidator>
								<asp:RegularExpressionValidator id="revAMOUNT" Runat="server" Display="Dynamic" ControlToValidate="txtAMOUNT"></asp:RegularExpressionValidator></TD>
							<TD class="midcolora" width="18%">
								<asp:Label id="capREFERENCE_ID" runat="server">Reference ID  :</asp:Label><SPAN class="mandatory" id="spnREFERENCE_ID">*</SPAN></TD>
							<TD class="midcolora" width="32%">
								<asp:textbox id="txtREFERENCE_ID" Runat="server" size="21" MaxLength="15"></asp:textbox><BR>
								<asp:RequiredFieldValidator id="rfvREFERENCE_ID" Runat="server" Display="Dynamic" ControlToValidate="txtREFERENCE_ID"></asp:RequiredFieldValidator></TD>
						</TR>
						</TR>
						<TR id = "CC_1">
							<TD class="midcolora" width="18%">
								<asp:Label id="capCARD_TYPE" Runat="server">Card Type</asp:Label><SPAN class="mandatory">*</SPAN></TD>
							<TD class="midcolora" width="32%">
								<asp:DropDownList id="cmbCARD_TYPE" onfocus="SelectComboIndex('cmbCARD_TYPE')" Runat="server"></asp:DropDownList><BR>
								<asp:RequiredFieldValidator id="rfvCARD_TYPE" Runat="server" Display="Dynamic" ControlToValidate="cmbCARD_TYPE"></asp:RequiredFieldValidator></TD>
							<TD class="midcolora" width="18%">
								<asp:Label id="capCARD_NO" runat="server">Credit Card #  :</asp:Label><SPAN class="mandatory">*</SPAN></TD>
							<TD class="midcolora" width="32%">
								<asp:textbox id="txtCARD_NO" Runat="server" size="21" MaxLength="16" AutoComplete = "Off"></asp:textbox><BR>
								<asp:RequiredFieldValidator id="rfvCARD_NO" Runat="server" Display="Dynamic" ControlToValidate="txtCARD_NO"></asp:RequiredFieldValidator>
								<asp:RegularExpressionValidator id="revCARD_NO" Runat="server" Display="Dynamic" ControlToValidate="txtCARD_NO"></asp:RegularExpressionValidator>
								<asp:CustomValidator id="csvCARD_NO" Runat="server" Display="Dynamic" ControlToValidate="txtCARD_NO"
									ClientValidationFunction="ValCardNumLen"></asp:CustomValidator></TD>
						</TR>
						<TR  id = "CC_2">
							<TD class="midcolora" width="18%">
								<asp:Label id="capCARD_CVV_NUMBER" Runat="server">Card CVV #</asp:Label><SPAN class="mandatory">*</SPAN></TD>
							<TD class="midcolora" width="32%">
								<asp:TextBox id="txtCARD_CVV_NUMBER" Runat="server" size="4" MaxLength="4" AutoComplete = "Off"></asp:TextBox><BR>
								<asp:RegularExpressionValidator id="revCARD_CVV_NUMBER" Runat="server" ControlToValidate="txtCARD_CVV_NUMBER" Display="Dynamic"></asp:RegularExpressionValidator>
								<asp:RequiredFieldValidator id="rfvCARD_CVV_NUMBER" Runat="server" Display="Dynamic" ControlToValidate="txtCARD_CVV_NUMBER"></asp:RequiredFieldValidator>
								<asp:CustomValidator id="csvCARD_CVV_NUMBER" Runat="server" Display="Dynamic" ControlToValidate="txtCARD_CVV_NUMBER"
									ClientValidationFunction="ValCardCVVNumLen"></asp:CustomValidator></TD>
							<TD class="midcolora" width="18%">
								<asp:label id="capCARD_DATE_VALID_TO" runat="server">Valid Till(Month/Year)</asp:label><SPAN class="mandatory">*</SPAN></TD>
							<TD class="midcolora" width="32%">
								<asp:dropdownlist id="cmbCARD_DATE_VALID_TO" runat="server">
									<ASP:LISTITEM Value="01">January</ASP:LISTITEM>
									<ASP:LISTITEM Value="02">February</ASP:LISTITEM>
									<ASP:LISTITEM Value="03">March</ASP:LISTITEM>
									<ASP:LISTITEM Value="04">April</ASP:LISTITEM>
									<ASP:LISTITEM Value="05">May</ASP:LISTITEM>
									<ASP:LISTITEM Value="06">June</ASP:LISTITEM>
									<ASP:LISTITEM Value="07">July</ASP:LISTITEM>
									<ASP:LISTITEM Value="08">August</ASP:LISTITEM>
									<ASP:LISTITEM Value="09">September</ASP:LISTITEM>
									<ASP:LISTITEM Value="10">October</ASP:LISTITEM>
									<ASP:LISTITEM Value="11">November</ASP:LISTITEM>
									<ASP:LISTITEM Value="12">December</ASP:LISTITEM>
								</asp:dropdownlist>
								<asp:textbox id="txtCARD_DATE_VALID_TO" Runat="server" size="2" MaxLength="2" AutoComplete = "Off"></asp:textbox><BR>
								<asp:RequiredFieldValidator id="rfvCARD_DATE_VALID_TO" Runat="server" Display="Dynamic" ControlToValidate="txtCARD_DATE_VALID_TO"></asp:RequiredFieldValidator>
								<asp:rangevalidator id="rngCARD_DATE_VALID_TO" Runat="server" Display="Dynamic" ControlToValidate="txtCARD_DATE_VALID_TO"
									Type="Integer"></asp:rangevalidator>
								<asp:CustomValidator id="csvCARD_DATE_VALID_TO" Runat="server" Display="Dynamic" ControlToValidate="txtCARD_DATE_VALID_TO"
									ClientValidationFunction="chkToYearLength"></asp:CustomValidator></TD>
						</TR>
						<TR>
							<TD class="headereffectSystemParams" colSpan="4">Customer Credit Card Info</TD>
						</TR>
						<TR>
							<TD colSpan="4">
								<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
									<TR>
										<TD class="midcolora" width="17%">
											<asp:label id="capCUSTOMER_FIRST_NAME" runat="server"></asp:label></TD>
										<TD class="midcolora" width="18%">
											<asp:textbox id="txtCUSTOMER_FIRST_NAME" runat="server" size="25" maxlength="75"></asp:textbox><BR>
										</TD>
										<TD class="midcolora" width="9%">
											<asp:label id="capCUSTOMER_MIDDLE_NAME" runat="server"></asp:label></TD>
										<TD class="midcolora" width="16%">
											<asp:textbox id="txtCUSTOMER_MIDDLE_NAME" runat="server" size="12" maxlength="10"></asp:textbox></TD>
										<TD class="midcolora" width="8%">
											<asp:label id="capCUSTOMER_LAST_NAME" runat="server"></asp:label></TD>
										<TD class="midcolora" width="28%">
											<asp:textbox id="txtCUSTOMER_LAST_NAME" runat="server" size="25" maxlength="25"></asp:textbox><BR>
										</TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
						<TR>
							<TD class="midcolora" width="18%">
								<asp:label id="capCUSTOMER_ADDRESS1" runat="server"></asp:label></TD>
							<TD class="midcolora" width="32%">
								<asp:textbox id="txtCUSTOMER_ADDRESS1" runat="server" size="35" maxlength="150"></asp:textbox><BR>
							</TD>
							<TD class="midcolora" width="18%">
								<asp:label id="capCUSTOMER_ADDRESS2" runat="server"></asp:label></TD>
							<TD class="midcolora" width="32%">
								<asp:textbox id="txtCUSTOMER_ADDRESS2" runat="server" size="35" maxlength="150"></asp:textbox></TD>
						</TR>
						<TR>
							<TD class="midcolora" width="18%">
								<asp:label id="capCUSTOMER_CITY" runat="server"></asp:label></TD>
							<TD class="midcolora" width="32%">
								<asp:textbox id="txtCUSTOMER_CITY" runat="server" size="35" maxlength="35"></asp:textbox><BR>
							</TD>
							<TD class="midcolora" width="18%">
								<asp:label id="capCUSTOMER_COUNTRY" runat="server"></asp:label></TD>
							<TD class="midcolora" width="32%">
								<asp:dropdownlist id="cmbCUSTOMER_COUNTRY" onfocus="SelectComboIndex('cmbCUSTOMER_COUNTRY');" runat="server" onchange="javascript:fillstateFromCountry();"></asp:dropdownlist><BR><%--Called fillstateFromCountry() by Sibin on 25 Nov 08--%>
							</TD>
						</TR>
						<TR>
							<TD class="midcolora" width="18%">
								<asp:label id="capCUSTOMER_STATE" runat="server"></asp:label></TD>
							<TD class="midcolora" width="32%">
								<asp:dropdownlist id="cmbCUSTOMER_STATE" onfocus="SelectComboIndex('cmbCUSTOMER_STATE');" runat="server" onchange="setStateID();"></asp:dropdownlist><BR><%--Called setStateID() by Sibin on 25 Nov 08--%>
							</TD>
							<TD class="midcolora" width="18%">
								<asp:label id="capCUSTOMER_ZIP" runat="server"></asp:label></TD>
							<TD class="midcolora" width="32%">
								<asp:textbox id="txtCUSTOMER_ZIP" runat="server" size="13" maxlength="10" OnBlur="DisableZipForCanada();"></asp:textbox><%--Called DisableZipForCanada() by Sibin on 25 Nov 08--%>
								<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
									<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
								</asp:hyperlink><BR>
								<asp:customvalidator id="csvCUSTOMER_ZIP" Runat="server" Display="Dynamic" ControlToValidate="txtCUSTOMER_ZIP"
									ClientValidationFunction="ChkEmpResult" ErrorMessage=" "></asp:customvalidator>
								<asp:regularexpressionvalidator id="revCUSTOMER_ZIP" Runat="server" Display="Dynamic" ControlToValidate="txtCUSTOMER_ZIP"></asp:regularexpressionvalidator></TD>
						</TR>
						<TR>
							<TD class="midcolora" width="18%">
								<asp:label id="capNOTE" runat="server">Note</asp:label></TD>
							<TD class="midcolora" width="32%">
								<asp:textbox id="txtNOTE" runat="server" Width="220px" Height="48px" TextMode="MultiLine"></asp:textbox><BR>
								<asp:CustomValidator id="csvDESCRIPTION" Runat="server" Display="Dynamic" ControlToValidate="txtNOTE"
									ClientValidationFunction="ChkTextAreaLength" ErrorMessage="Maximum length of Note is 800."></asp:CustomValidator></TD>
							<TD class="midcolora" width="18%"></TD>
							<TD class="midcolora" width="32%"></TD>
						<TR>
						<TR>
							<TD class="midcolora" colSpan="2">
								<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Start New Transaction"></cmsb:cmsbutton></TD>
							<TD class="midcolorr" colSpan="2">
								<%-- Done for Itrack Issue 6099 on 14 July 2009
								CMSB:CMSBUTTON class="clsButton" id="btnCCSave" runat="server" Text="Process Transaction"></CMSB:CMSBUTTON--%>
								<CMSB:CMSBUTTON class="clsButton" id="btnSave" runat="server" Text="Process Transaction"></CMSB:CMSBUTTON>
								<CMSB:CMSBUTTON class="clsButton" id="btnCCProcessing" runat="server" Text="Processing Transaction"></CMSB:CMSBUTTON>
								<%--<asp:Button class="clsButton" id="btnCCProcessing" runat="server" Text="Processing Transaction..."></asp:Button>--%>
							</TD>
						</TR>
				</asp:panel></TBODY></table>
			<INPUT id="hidPOLICYINFO" type="hidden" name="hidPOLICYINFO" runat="server"> <input id="hidCUSTOMER_ID" type="hidden" runat="server">
			<input id="hidPOLICY_ID" type="hidden" runat="server"> <input id="hidPOLICY_VERSION_ID" type="hidden" runat="server">
			<input id="hidCUSTOMER_NAME" type="hidden" runat="server"> <input id="hidCARD_TYPE" type="hidden" runat="server">
			<input id="hidSTATE_COUNTRY_LIST" type="hidden" name="hidSTATE_COUNTRY_LIST" runat="server"> <%--Added by Sibin on 25 Nov 08--%>
			<input id="hidSTATE_ID" type="hidden" name="hidSTATE_ID" runat="server"> <%--Added by Sibin on 25 Nov 08--%>
		</form>
	</body>
</HTML>

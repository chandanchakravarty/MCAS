<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="InstallmentInfo.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.InstallmentInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Installment Info</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript" src="/cms/cmsweb/Scripts/Calendar.js"></script>
		<script language="javascript">
		function SetDisplay()
		{
		 //alert(document.getElementById('hidOldData').value)
		  /*if(document.getElementById('hidOldData').value == "")
		  {
			if(document.getElementById('hidDAY').value != '')
			{
				if(document.getElementById('txtEFT_TENTATIVE_DATE'))
					document.getElementById('txtEFT_TENTATIVE_DATE').value = document.getElementById('hidDAY').value;
			}
		  }*/
		  if(document.getElementById('hidBILL_TYPE').value != 'DB')
			{
				document.getElementById('trBody').style.display = "none";
				document.getElementById('tbBilling').style.display = "none";
				//Added For AB By Raghav For Itrack Issue 4829
				document.getElementById('trBillingPlan').style.display = "none";
				//document.getElementById('lblMessage').style.display = "inline";
				//document.getElementById('lblMessage').value = 'Installment plan not applicable in case of AB policies';
			}
			

		}
		function ValidateTranNo(objSource, objArgs)
		{
			var tranNum = document.getElementById('txtTRANSIT_ROUTING_NO').value;
			var firstDigit = tranNum.slice(0,1);
			if(firstDigit == "5")
				objArgs.IsValid = false;
		}
		function VerifyTranNo(objSource, objArgs)
		{
			var boolval = ValidateTransitNumber(document.getElementById('txtTRANSIT_ROUTING_NO'));
			
			if(boolval == false)
			{
				objArgs.IsValid = false;
			}
		}
		function ValidateTranNoLength(objSource, objArgs)
		{
			var boolval = ValidateTransitNumberLen(document.getElementById('txtTRANSIT_ROUTING_NO'));
			if(boolval == false)
			{
				objArgs.IsValid = false;
			}
			
		}

		function ValidateDFIAcct(objSource, objArgs)
		{
			
			var boolval = ValidateDFIAcctNo(document.getElementById('txtDFI_ACC_NO'));
			if(boolval == false)
			{
				objArgs.IsValid = false;
			}
		}
		
		// Validate Credit Card Number Length
		
		//Added By Raghav For Itrack Issue #4998
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
		// Validate Credit Card CVV Number Length	
		function ValCardCVVNumLen(objSource, objArgs)
		{
			var cardCVVLen = new String(document.getElementById('txtCARD_CVV_NUMBER').value.trim());
			if((cardCVVLen.length < 3) || (cardCVVLen.length > 4))				
				objArgs.IsValid = false;
			else
				objArgs.IsValid = true;
		}
		
			
		// Validate From Year Lenth : must be two digits for single digits also (eg : Instead of Jan -2, it shud be Jan -02)
		function chkFromYearLength(objSource , objArgs)
		{
			var FromYearDigitLen = new String(document.getElementById('txtCARD_DATE_VALID_FROM').value);
			if(FromYearDigitLen.length < 2)
			{
				objArgs.IsValid = false;
			}
			else
			{
				objArgs.IsValid = true;
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
		
		function ChkValidators()
		{
			DisableValidators();
			return true;
		}
		
		
		function FillCustomerName()
		{	
		//alert(document.getElementById('hidCUSTOMER_INFO').value);
					
			var objXmlHandler = new XMLHandler();
			var tree = (objXmlHandler.quickParseXML(document.getElementById('hidCUSTOMER_INFO').value).getElementsByTagName('Table')[0]);
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
					  break;
				case "CUSTOMER_STATE":
					  SelectComboOption("cmbCUSTOMER_STATE",nodeValue)
					  //SetRegisteredState();
					  break;
				case "CUSTOMER_ZIP":
					   document.getElementById('txtCUSTOMER_ZIP').value = nodeValue;
					   break;
				 }	
				}		
			return false;
							
			}	
			
			//Added Fetch Zip State Service
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
			document.getElementById("btnSave").click();
		}	
			
		//for ZIP Details
	 function GetZipForState_Old()
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
		
		////////////////////////////////////AJAX CALLS FOR ZIP///////////////////////////
		function GetZipForState()
		{		
			GlobalError=true;
			if(document.getElementById('cmbCUSTOMER_STATE').value==14 ||document.getElementById('cmbCUSTOMER_STATE').value==22||document.getElementById('cmbCUSTOMER_STATE').value==49)
			{ 
				if(document.getElementById('txtCUSTOMER_ZIP').value!="")
				{
					var intStateID = document.getElementById('cmbCUSTOMER_STATE').options[document.getElementById('cmbCUSTOMER_STATE').options.selectedIndex].value;
					var strZipID = document.getElementById('txtCUSTOMER_ZIP').value;	
					var result = InstallmentInfo.AjaxFetchZipForState(intStateID,strZipID);
					return AjaxCallFunction_CallBack(result);									
				}
				return false;
			}	
			else
			 return true;
		}
		
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
		
		///**********************************PayPal JS Functions *****************************/
		function DisableEnableValidatorsCC(status)
		{
			//Disbaling Validators
				var controlIDs = new Array("rfvCUSTOMER_FIRST_NAME",
					"revCUSTOMER_FIRST_NAME",
					"rfvCUSTOMER_LAST_NAME",
					"csvCUSTOMER_ZIP",
					"rfvCARD_TYPE",
					"revCARD_NO",
					"csvCARD_NO",
					"rfvCARD_NO",
					"rfvCARD_CVV_NUMBER",
					"revCARD_CVV_NUMBER",
					"rfvCARD_DATE_VALID_TO","rngCARD_DATE_VALID_TO","csvCARD_DATE_VALID_TO");
					
					for(i=0;i<controlIDs.length;i++)
					{
						if(document.getElementById(controlIDs[i])!=null)
							document.getElementById(controlIDs[i]).setAttribute("enabled",status);
					}
			
		}
		
		
		function HideCreditCardPanel()
		{
			var payPalID = document.getElementById('hidCCFlag').value;
		
			if(payPalID == "1")
			{
				document.getElementById('tblCC').style.display='none'; 
				document.getElementById('tblBtnSave').style.display='none'; 
			}
			var eftPanel = document.getElementById('hidEFTFlag').value;
			//For EFT and Credit Card
			//alert('CC ' + payPalID)
			//alert('EFT' + eftPanel);
			
			if(eftPanel == "1" && payPalID == "1")
			{
				document.getElementById('tblBtnSave').style.display='inline'; 
				DisableEnableValidatorsCC(false);
			}
			
			if(document.getElementById('lblCancelPayPal')!=null)
				document.getElementById('lblCancelPayPal').style.display='none'; 
				
		}	
		
		
		
		function showCCPanelJS()
		{
			var payPalID = document.getElementById('hidCCFlag').value;
			if(payPalID == "1")
			{
				document.getElementById('tblCC').style.display='inline'; 	
				document.getElementById('tblBtnSave').style.display='inline'; 
				DisableEnableValidatorsCC(true);	
				document.getElementById('lblCancelPayPal').style.display='inline'; 
				
			}
				
		}
		
		function HideCCPanelJS()
		{
		
			document.getElementById('tblCC').style.display='none'; 	
			DisableEnableValidatorsCC(false);	
			document.getElementById('lblCancelPayPal').style.display='none'; 	
			document.getElementById('txtCUSTOMER_FIRST_NAME').value ='';
			
			if (document.getElementById('tblCC').style.display == 'none')
			{
				document.getElementById('tblBtnSave').style.display='none'; 	
					if(document.getElementById('hidEFTFlag').value == "1")
						document.getElementById('tblBtnSave').style.display='inline'; 
						
					
			}
				
		}
		
		function False()
		{
			//To Nullify the Scrolling Effect 
		}
			
		//Added by Mohit Agarwal 29-Aug 2008	
		function DFI_Change()
		{
			document.getElementById('txtDFI_ACC_NO').value = document.getElementById('txtDFI_ACC_NO_HID').value;
			document.getElementById('txtDFI_ACC_NO').style.display = 'inline';
			document.getElementById('txtDFI_ACC_NO_HID').style.display = 'none';			
		}
		
		function Federal_Change()
		{
			document.getElementById('txtFEDERAL_ID').value = document.getElementById('txtFEDERAL_ID_HID').value;
			document.getElementById('txtFEDERAL_ID').style.display = 'inline';
			document.getElementById('txtFEDERAL_ID_HID').style.display = 'none';			
		}
		
		function Transit_change()
		{
			document.getElementById('txtTRANSIT_ROUTING_NO').value = document.getElementById('txtTRANSIT_ROUTING_NO_HID').value;
			document.getElementById('txtTRANSIT_ROUTING_NO').style.display = 'inline';
			document.getElementById('txtTRANSIT_ROUTING_NO_HID').style.display = 'none';			
		}

		
		
		//----------------------------ENCRYPTION----------------------
			function FEDERAL_ID_change()
			{
				
				document.getElementById('txtFEDERAL_ID').value = '';
				document.getElementById('txtFEDERAL_ID').style.display = 'inline';
								
				document.getElementById("revFEDERAL_ID").setAttribute('enabled',true);
				if(document.getElementById("btnENCRYP_FEDERAL_ID").value == 'Edit')
					document.getElementById("btnENCRYP_FEDERAL_ID").value = 'Cancel';
				else if(document.getElementById("btnENCRYP_FEDERAL_ID").value == 'Cancel')
					FEDERAL_ID_hide();
				else
					document.getElementById("btnENCRYP_FEDERAL_ID").style.display = 'none';
					
				
			}
			
			function FEDERAL_ID_hide()
			{
				if(document.getElementById("btnENCRYP_FEDERAL_ID")!=null)
				{
				
					if(document.getElementById('hid_ENCRYP_FEDERAL_ID').value == '0')
					{
					  document.getElementById('txtFEDERAL_ID').style.display = 'inline';
					  document.getElementById('btnENCRYP_FEDERAL_ID').style.display = 'none';
					}
					else
					{
						document.getElementById("btnENCRYP_FEDERAL_ID").style.display = 'inline';
						document.getElementById('txtFEDERAL_ID').value = '';
						document.getElementById('txtFEDERAL_ID').style.display = 'none';
						document.getElementById("revFEDERAL_ID").style.display='none';
						document.getElementById("revFEDERAL_ID").setAttribute('enabled',false);
						document.getElementById("btnENCRYP_FEDERAL_ID").value = 'Edit';
					}	
					
						
				}	
				
			}
			//--------------------------END FEDERAL ID--------------------------
			//-----------------------------------DFI_ACC_NO_change()---------------------		
			
			function DFI_ACC_NO_change()
			{
				document.getElementById('txtDFI_ACC_NO').value = '';
				document.getElementById('txtDFI_ACC_NO').style.display = 'inline';
								
				document.getElementById("revDFI_ACC_NO").setAttribute('enabled',true);
				document.getElementById("rfvDFI_ACC_NO").setAttribute('enabled',true);
				document.getElementById("csvDFI_ACC_NO").setAttribute('enabled',true);
				
				
				if(document.getElementById("btnENCRYP_DFI_ACC_NO").value == 'Edit')
				{
					document.getElementById("btnENCRYP_DFI_ACC_NO").value = 'Cancel';
					document.getElementById("chkREVERIFIED_AC").checked = true;
					
				}
				else if(document.getElementById("btnENCRYP_DFI_ACC_NO").value == 'Cancel')
				{
					document.getElementById("chkREVERIFIED_AC").checked = false;
					DFI_ACC_NO_hide();
				}
				else
					document.getElementById("btnENCRYP_DFI_ACC_NO").style.display = 'none';
					
				
			}
			
			function DFI_ACC_NO_hide()
			{
				
				if(document.getElementById("btnENCRYP_DFI_ACC_NO")!=null)
				{
				
					if(document.getElementById('hid_ENCRYP_DFI_ACC_NO').value == '0')
					{
					  document.getElementById('txtDFI_ACC_NO').style.display = 'inline';
					  document.getElementById('btnENCRYP_DFI_ACC_NO').style.display = 'none';
						document.getElementById("chkREVERIFIED_AC").checked = true;
					}
					else
					{
				
						
				
						document.getElementById("btnENCRYP_DFI_ACC_NO").style.display = 'inline';
						document.getElementById('txtDFI_ACC_NO').value = '';
						document.getElementById('txtDFI_ACC_NO').style.display = 'none';
						
						document.getElementById("revDFI_ACC_NO").style.display='none';
						document.getElementById("revDFI_ACC_NO").setAttribute('enabled',false);
						
						document.getElementById("rfvDFI_ACC_NO").style.display='none';
						document.getElementById("rfvDFI_ACC_NO").setAttribute('enabled',false);
						
						document.getElementById("csvDFI_ACC_NO").style.display='none';
						document.getElementById("csvDFI_ACC_NO").setAttribute('enabled',false);
						
						document.getElementById("btnENCRYP_DFI_ACC_NO").value = 'Edit';
						
					}	
					
				}	
				
	
				
			}
			//--------------------------END DFI NO
			
			//--------------------START TRANS ROUTING NO----------------
			
			function TRANSIT_ROUTING_NO_change()
			{
				document.getElementById('txtTRANSIT_ROUTING_NO').value = '';
				document.getElementById('txtTRANSIT_ROUTING_NO').style.display = 'inline';
				
				document.getElementById("rfvTRANSIT_ROUTING_NO").setAttribute('enabled',true);
				document.getElementById("csvTRANSIT_ROUTING_NO").setAttribute('enabled',true);
				document.getElementById("csvVERIFYFY_TRANSIT_ROUTING_NO").setAttribute('enabled',true);
				document.getElementById("csvVERIFYFY_TRANSIT_ROUTING_NO_LENGHT").setAttribute('enabled',true);
				document.getElementById("revTRANSIT_ROUTING_NO").setAttribute('enabled',true);
				
				
				
				if(document.getElementById("btnENCRYP_TRANSIT_ROUTING_NO").value == 'Edit')
				{
					document.getElementById("btnENCRYP_TRANSIT_ROUTING_NO").value = 'Cancel';
					document.getElementById("chkREVERIFIED_AC").checked = true;
				}
				else if(document.getElementById("btnENCRYP_TRANSIT_ROUTING_NO").value == 'Cancel')
				{
					ENCRYP_TRANSIT_ROUTING_NO_hide();
					document.getElementById("chkREVERIFIED_AC").checked = false;
				}
				else
					document.getElementById("btnENCRYP_TRANSIT_ROUTING_NO").style.display = 'none';
					
				
			}
			
			function ENCRYP_TRANSIT_ROUTING_NO_hide()
			{
				
				if(document.getElementById("btnENCRYP_TRANSIT_ROUTING_NO")!=null)
				{
				
					if(document.getElementById('hid_ENCRYP_TRANSIT_ROUTING_NO').value == '0')
					{
					  document.getElementById('txtTRANSIT_ROUTING_NO').style.display = 'inline';
					  document.getElementById('btnENCRYP_TRANSIT_ROUTING_NO').style.display = 'none';
					  document.getElementById("chkREVERIFIED_AC").checked = true;
					}
					else
					{
					
						document.getElementById("btnENCRYP_TRANSIT_ROUTING_NO").style.display = 'inline';
						document.getElementById('txtTRANSIT_ROUTING_NO').value = '';
						document.getElementById('txtTRANSIT_ROUTING_NO').style.display = 'none';
						
						document.getElementById("rfvTRANSIT_ROUTING_NO").style.display='none';
						document.getElementById("rfvTRANSIT_ROUTING_NO").setAttribute('enabled',false);
						
						document.getElementById("csvTRANSIT_ROUTING_NO").style.display='none';
						document.getElementById("csvTRANSIT_ROUTING_NO").setAttribute('enabled',false);
						
						document.getElementById("csvVERIFYFY_TRANSIT_ROUTING_NO").style.display='none';
						document.getElementById("csvVERIFYFY_TRANSIT_ROUTING_NO").setAttribute('enabled',false);
						
						document.getElementById("csvVERIFYFY_TRANSIT_ROUTING_NO_LENGHT").style.display='none';
						document.getElementById("csvVERIFYFY_TRANSIT_ROUTING_NO_LENGHT").setAttribute('enabled',false);
						
						document.getElementById("revTRANSIT_ROUTING_NO").style.display='none';
						document.getElementById("revTRANSIT_ROUTING_NO").setAttribute('enabled',false);
						
						document.getElementById("btnENCRYP_TRANSIT_ROUTING_NO").value = 'Edit';
						
					}	
					
				}	
				
	
				
			}
			
			
			//END ROUTING NO
		
			
			function SetEncrytion()
			{
				FEDERAL_ID_hide();
				DFI_ACC_NO_hide();
				ENCRYP_TRANSIT_ROUTING_NO_hide();
			}
		
		
			 //Added on 25 Nov 2008 by Sibin
		
		function fillstateFromCountry()
		{	
			try
			{
			    
			  	  GlobalError=true;
			      var CmbState=document.getElementById('cmbCUSTOMER_STATE');
				  var CountryID=  document.getElementById('cmbCUSTOMER_COUNTRY').options[document.getElementById('cmbCUSTOMER_COUNTRY').selectedIndex].value;
				  //alert(CountryID);
					//var co=myTSMain1.createCallOptions();
					//co.funcName = "FillState";
					//co.async = false;
					//co.SOAPHeader= new Object();					
					//var oResult = myTSMain1.FetchZip.callService(co,CountryID);
					//handleResult(oResult);
					InstallmentInfo.AjaxFillState(CountryID,fillState);
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
			catch(ex)
			{}		
	 }
	 
	 function setStateID()
	  {
		var CmbState=document.getElementById('cmbCUSTOMER_STATE');
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
	
	function DisableZipForCanada()
		{
			try
			{
			var myCountry=document.getElementById("cmbCUSTOMER_COUNTRY");
			 
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
			catch(ex)
			{}
		}
				
	//Added till here by Sibin
	
		function  ShowTooltip(custID,polID,installNo,currentTerm)
		{		
			document.getElementById('Popup').style.display="inline";
			InstallmentInfo.AjaxDetailBreakupXML(custID,polID,installNo,currentTerm,CreateBreakUpTable);								
		}

		function HideTooltip()
		{
						
			//document.getElementById('dgPolicyInstallInfo__ctl2_lblINSTALLMENT_NO').style.fontWeight = 'normal';
			//document.getElementById('dgPolicyInstallInfo__ctl2_lblINSTALLMENT_EFFECTIVE_DATE').style.fontWeight = 'normal';
			//document.getElementById('dgPolicyInstallInfo__ctl2_lblINSTALLMENT_DUE_DATE').style.fontWeight = 'normal';
			//document.getElementById('dgPolicyInstallInfo__ctl2_lblPROCESSING_DATE').style.fontWeight = 'normal';
			//document.getElementById('dgPolicyInstallInfo__ctl2_lblPAYMENT_MODE').style.fontWeight = 'normal';
			//document.getElementById('dgPolicyInstallInfo__ctl2_lblRELEASED_STATUS').style.fontWeight = 'normal';
			//document.getElementById('dgPolicyInstallInfo__ctl2_lblAmount').style.fontWeight = 'normal';
			//document.getElementById('dgPolicyInstallInfo__ctl2_lblBalAmount').style.fontWeight = 'normal';
			document.getElementById('Popup').style.display="none";	
			
		}

		function CreateBreakUpTable(res)
		{
			if(!res.error)
			{
				if (res.value!="" && res.value!=null) 
				{
					document.getElementById('Popup').innerHTML = res.value;
				}
			}
		}
		
		    //variable that will store the id of the last clicked row
         var previousRow1;
         previousRow1 = previousRow1 + '_lblAmount';
         var previousRow2;
         previousRow2 = previousRow2 +  '_lblINSTALLMENT_NO';  
         var previousRow3;      
		 previousRow3 = previousRow3 +  '_lblINSTALLMENT_EFFECTIVE_DATE';
		 var previousRow4;      
		 previousRow4 = previousRow4 +  '_lblINSTALLMENT_DUE_DATE';
		 var previousRow5;
		 previousRow5 = previousRow5 +  '_lblPROCESSING_DATE';
		 var previousRow6;
		 previousRow6 = previousRow6 +  '_lblPAYMENT_MODE';
		 var previousRow7;
		 previousRow7 = previousRow7 +  '_lblRELEASED_STATUS';
		 var previousRow8;
		 previousRow8 = previousRow8 +  '_lblBalAmount';
        
        function ChangeRowColor(row)
        {
       
			row1 = row + '_lblAmount';
			row2 = row + '_lblINSTALLMENT_NO';
			row3 = row + '_lblINSTALLMENT_EFFECTIVE_DATE';
			row4 = row + '_lblINSTALLMENT_DUE_DATE';
			row5 = row + '_lblPROCESSING_DATE';
			row6 = row + '_lblPAYMENT_MODE';
			row7 = row + '_lblRELEASED_STATUS';
			row8 = row + '_lblBalAmount';
			
            //If last clicked row and the current clicked row are same
            if (
              previousRow1 == row1 ||
               previousRow2 == row2 ||
                previousRow3 == row3 ||
                 previousRow4 == row4 ||               
                previousRow5 == row5 ||
                previousRow6 == row6 ||
                previousRow7 == row7 ||
                previousRow8 == row8
               )
                return;//do nothing
            //If there is row clicked earlier
            else if (
					previousRow1 != null && previousRow1 != "undefined_lblAmount" &&
					previousRow2 != null && previousRow2 != "undefined_lblINSTALLMENT_NO" &&
					previousRow3 != null && previousRow3 != "undefined_lblINSTALLMENT_EFFECTIVE_DATE" &&
					previousRow4 != null && previousRow4 != "undefined_lblINSTALLMENT_DUE_DATE" &&
					previousRow5 != null && previousRow5 != "undefined_lblPROCESSING_DATE" &&
					previousRow6 != null && previousRow6 != "undefined_lblPAYMENT_MODE" &&
					previousRow7 != null && previousRow7 != "undefined_lblRELEASED_STATUS" &&
					previousRow8 != null && previousRow8 != "undefined_lblBalAmount" 
					
					)
                //change the color of the previous row back to white
                {
					//document.getElementById(previousRow).style.backgroundColor = "red";
					//alert('bold')
					document.getElementById(previousRow1).style.fontWeight = 'normal';
					document.getElementById(previousRow2).style.fontWeight = 'normal';
					document.getElementById(previousRow3).style.fontWeight = 'normal';
					document.getElementById(previousRow4).style.fontWeight = 'normal';
					document.getElementById(previousRow5).style.fontWeight = 'normal';
					document.getElementById(previousRow6).style.fontWeight = 'normal';
					document.getElementById(previousRow7).style.fontWeight = 'normal';
					document.getElementById(previousRow8).style.fontWeight = 'normal';
					
                }
            
            //change the color of the current row to light yellow

			document.getElementById(row1).style.fontWeight='bold';
			document.getElementById(row2).style.fontWeight='bold';
			document.getElementById(row3).style.fontWeight='bold';
			document.getElementById(row4).style.fontWeight='bold';
			document.getElementById(row5).style.fontWeight='bold';
			document.getElementById(row6).style.fontWeight='bold';
			document.getElementById(row7).style.fontWeight='bold';
			document.getElementById(row8).style.fontWeight='bold';
           // document.getElementById(row).style.backgroundColor = "#ffffda";            
            //assign the current row id to the previous row id 
            //for next row to be clicked
            previousRow1 = row1;
            previousRow2 = row2;
            previousRow3 = row3;
            previousRow4 = row4;
            previousRow5 = row5;
            previousRow6 = row6;
            previousRow7 = row7;
            previousRow8 = row8;
        }
		
		

		</script>
	</HEAD>
	<body oncontextmenu="return false;" MS_POSITIONING="GridLayout" leftMargin="0" topMargin="0" onload="ApplyColor();ChangeColor();HideCreditCardPanel();SetEncrytion();">
		<form id="Form1" method="post" runat="server">
		 <P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>	
			<TABLE width="100%">
                <tr>
                    <td class="headereffectCenter" colspan="4">
                        <asp:Label ID="lblHeader" runat="server">Billing Information</asp:Label>
                    </td>
                </tr>
				<tr>
					<td class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False" EnableViewState="False"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD colSpan="4"><asp:datagrid id="dgPolicyInstallInfo" HeaderStyle-CssClass="headereffectWebGrid" ItemStyle-CssClass="midcolora"
							Width="100%" AutoGenerateColumns="False" Runat="server">
							<Columns>
								<asp:TemplateColumn HeaderText="Installment #">
									<ItemTemplate>
										<asp:Label ID="lblINSTALLMENT_NO" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INSTALLMENT_NO") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Installment Effective Date">
									<ItemTemplate>
										<asp:Label ID="lblINSTALLMENT_EFFECTIVE_DATE" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INSTALLMENT_EFFECTIVE_DATE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Installment Due Date">
									<ItemTemplate>
										<asp:Label ID="lblINSTALLMENT_DUE_DATE" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INSTALLMENT_DUE_DATE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Installment Processing Date">
									<ItemTemplate>
										<asp:Label ID="lblPROCESSING_DATE" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INSTALLMENT_PROCESSING_DATE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Payment Mode">
									<ItemTemplate>
										<asp:Label ID="lblPAYMENT_MODE" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PAYMENT_MODE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Processed">
									<ItemTemplate>
										<asp:Label ID="lblRELEASED_STATUS" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RELEASED_STATUS") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Percentage">
									<ItemTemplate>
										<asp:Label ID="lblPERCENT_COMPLETED" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PERCENTAG_OF_PREMIUM") + "%"%>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Amount">
									<ItemTemplate>
										<asp:Label ID="lblAmount" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INSTALLMENT_AMOUNT")%>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Balance Amount">
									<ItemTemplate>
										<asp:Label ID="lblBalAmount" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BALANCE_AMOUNT")%>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>								
							</Columns>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
			<div id="Popup" class="transparent"></div>
			<table width="100%" id="tbBilling">
				<tr id="trBillingPlan" runat="server">
					<td class="headereffectSystemParams" colSpan="3">Readjust Billing Plan</td>
				</tr>
				<tr>
					<td class="errmsg" colSpan="4">If changing to a plan on EFT or Credit Card from a non EFT/Credit Card plan, please enter EFT/Credit Card information after plan is changed.
					</td>
				</tr>
				<tr>
					<td class="midcolora" width="240" style="WIDTH: 240px"><asp:label id="capINSTALL_PLAN_ID" runat="server"></asp:label></td>
					<td class="midcolora" width="269" style="WIDTH: 269px"><select id="cmbINSTALL_PLAN_ID" name="cmbINSTALL_PLAN_ID" runat="server"></select></td>
					<td class="midcolorr" colSpan="2" style="HEIGHT: 15px"><cmsb:cmsbutton class="clsButton" id="btnAdjust" runat="server" Text="Readjust"></cmsb:cmsbutton></td>
				</tr>
				<!-- EFT CUSTOMER INFO --><asp:panel id="pnlEFTCust" Runat="server">
					<TR>
						<TD class="headereffectSystemParams" colSpan="4">Customer EFT Info</TD>
					</TR>
					<TR>
						<TD class="midcolora" width="18%">
							<asp:Label id="capFEDERAL_ID" runat="server"></asp:Label></TD>
						<TD class="midcolora" width="32%">
						
							<asp:label id="capENCRYP_FEDERAL_ID" runat="server" size="10" maxlength="9"></asp:label>
							<input class="clsButton" id="btnENCRYP_FEDERAL_ID" text="Edit" type="button" onclick="FEDERAL_ID_change();"></input>
						
							<asp:textbox id="txtFEDERAL_ID" Runat="server" Width="70px" SIZE="10" MaxLength="9"></asp:textbox><BR>
							<asp:RegularExpressionValidator id="revFEDERAL_ID" Runat="server" ControlToValidate="txtFEDERAL_ID" Display="Dynamic"
								ErrorMessage="Please Enter Valid Federal ID."></asp:RegularExpressionValidator></TD>
						<TD class="midcolora" width="18%">
							<asp:Label id="capDFI_ACCT_NUMBER" runat="server"></asp:Label><SPAN class="mandatory">*</SPAN></TD>
						<TD class="midcolora" width="32%">
						
							<asp:label id="capENCRYP_DFI_ACC_NO" runat="server"></asp:label>
							<input class="clsButton" id="btnENCRYP_DFI_ACC_NO" text="Edit" type="button" onclick="DFI_ACC_NO_change();"></input>
							
							<asp:textbox id="txtDFI_ACC_NO" Runat="server" MaxLength="17" size="23"></asp:textbox><BR>
							<asp:customvalidator id="csvDFI_ACC_NO" Runat="server" ControlToValidate="txtDFI_ACC_NO" Display="Dynamic"
								ErrorMessage="No space allowed in between the numbers." ClientValidationFunction="ValidateDFIAcct"></asp:customvalidator>
							<asp:regularexpressionvalidator id="revDFI_ACC_NO" runat="server" ControlToValidate="txtDFI_ACC_NO" Display="Dynamic"
								ErrorMessage="DIIIIII"></asp:regularexpressionvalidator>
							<asp:requiredfieldvalidator id="rfvDFI_ACC_NO" Runat="server" ControlToValidate="txtDFI_ACC_NO" Display="Dynamic"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR>
						<TD class="midcolora">
							<asp:Label id="capTRAN_ROUT_NUMBER" runat="server"></asp:Label><SPAN class="mandatory">*</SPAN></TD>
						<TD class="midcolora">
						
							<asp:label id="capENCRYP_TRANSIT_ROUTING_NO" runat="server"></asp:label>
							<input class="clsButton" id="btnENCRYP_TRANSIT_ROUTING_NO" text="Edit" type="button" onclick="TRANSIT_ROUTING_NO_change();"></input>
						
							<asp:textbox id="txtTRANSIT_ROUTING_NO" Runat="server" MaxLength="9" size="11"></asp:textbox><BR>
							<asp:requiredfieldvalidator id="rfvTRANSIT_ROUTING_NO" Runat="server" ControlToValidate="txtTRANSIT_ROUTING_NO"
								Display="Dynamic"></asp:requiredfieldvalidator>
							<asp:customvalidator id="csvTRANSIT_ROUTING_NO" Runat="server" ControlToValidate="txtTRANSIT_ROUTING_NO"
								Display="Dynamic" ErrorMessage="Number starting with 5 is invalid." ClientValidationFunction="ValidateTranNo"></asp:customvalidator>
							<asp:customvalidator id="csvVERIFYFY_TRANSIT_ROUTING_NO" Runat="server" ControlToValidate="txtTRANSIT_ROUTING_NO"
								Display="Dynamic" ErrorMessage="Please Verify the 9th digit(Check Digit)." ClientValidationFunction="VerifyTranNo"></asp:customvalidator>
							<asp:customvalidator id="csvVERIFYFY_TRANSIT_ROUTING_NO_LENGHT" Runat="server" ControlToValidate="txtTRANSIT_ROUTING_NO"
								Display="Dynamic" ErrorMessage="Length has to be exactly 8/9 digits." ClientValidationFunction="ValidateTranNoLength"></asp:customvalidator>
							<asp:RegularExpressionValidator id="revTRANSIT_ROUTING_NO" Runat="server" ControlToValidate="txtTRANSIT_ROUTING_NO"
								Display="Dynamic" ErrorMessage="Please Enter Valid Transit Number."></asp:RegularExpressionValidator></TD>
						<TD class="midcolora">
							<asp:Label id="capIS_VERIFIED" runat="server"> Is Verified :</asp:Label></TD>
							<TD class="midcolora"><asp:Label id="lblIS_VERIFIED" runat="server"></asp:Label></TD>
					</TR>
					<TR>
						<TD class="midcolora">
							<asp:Label id="capVERIFIED_DATE" runat="server"> Verified Date :</asp:Label></TD>
						<TD class="midcolora"><asp:Label id="lblVERIFIED_DATE" runat="server"></asp:Label></TD>
						<TD class="midcolora">
							<asp:Label id="capREASON" runat="server"> Reason :</asp:Label></TD>
						<TD class="midcolora">N/A</TD>
					</TR>
					<TR>
						<TD class="midcolora" width="18%"><asp:label id="capREVERIFIED_AC" runat="server"></asp:label></TD>
						<TD class="midcolora" width="32%"><asp:checkbox id="chkREVERIFIED_AC" runat="server"></asp:checkbox></TD>
						<TD class="midcolora"></TD>
						<TD class="midcolora"></TD>
						
					</TR>
					<TR>
						<TD class="midcolora">
							<asp:Label id="capACCOUNT_TYPE" runat="server"> Account Type :</asp:Label></TD>
						<TD class="midcolora">
							<asp:radiobutton id="rdbACC_CASH_ACC_TYPEO" runat="server" Text="Checking" GroupName="ACC_CASH_ACC_TYPE"
								Checked="True"></asp:radiobutton>
							<asp:radiobutton id="rdbACC_CASH_ACC_TYPET" runat="server" Text="Saving" GroupName="ACC_CASH_ACC_TYPE"></asp:radiobutton></TD>
						<TD class="midcolora">
							<asp:Label id="capEFT_TENTATIVE_DATE" Runat="server">EFT Tentative Date:</asp:Label><SPAN class="mandatory">*</SPAN></TD>
						<TD class="midcolora">
							<asp:TextBox id="txtEFT_TENTATIVE_DATE" Runat="server" MaxLength="2" size="2"></asp:TextBox><BR>
							<asp:RequiredFieldValidator id="rfvEFT_TENTATIVE_DATE" Runat="server" ControlToValidate="txtEFT_TENTATIVE_DATE"
								Display="Dynamic"></asp:RequiredFieldValidator>
							<asp:RangeValidator id="rngEFT_TENTATIVE_DATE" Runat="server" ControlToValidate="txtEFT_TENTATIVE_DATE"
								Display="Dynamic" ErrorMessage="Date must be between 1 - 31." Type="Integer" MinimumValue="1" MaximumValue="31"></asp:RangeValidator></TD>
					</TR>
				</asp:panel>
				<asp:panel id="pnlCCMessage" Runat="server">
					<table id='tblShowCC' width="100%">
						<TR>
								<TD class="midcolora" width="80%">
									<asp:Label id="lblPayPalMsg" runat="server" CssClass="errmsg">Credit Card data for this Policy already provided. Click <A href="javascript:False();" onclick="showCCPanelJS();">here</A> to modify.</asp:Label>
								</TD>
								<TD class="midcolora" width="20%">
									<asp:Label id="lblCancelPayPal" runat="server" onclick="HideCCPanelJS();"><A href="javascript:False();">Cancel</A></asp:Label>
								</TD>
						</TR>
					</table>
				</asp:panel>
				<table id ='tblCC'>
				<asp:panel id="pnlCCCust" Runat="server">
					<TR>
						<TD class="headereffectSystemParams" colSpan="4">Customer Credit Card Info</TD>
					</TR>
					<TR>
						<TD class="midcolora">
							<asp:label id="Label1" runat="server">Would you like to pull customer address</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<cmsb:cmsbutton class="clsButton" id="btnPullCustomerAddress" runat="server" Text="Copy Default Customer"></cmsb:cmsbutton></TD>
					</TR>
					<TR>
						<TD colSpan="4">
							<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD class="midcolora" width="17%">
										<asp:label id="capCUSTOMER_FIRST_NAME" runat="server"></asp:label><SPAN class="mandatory">*</SPAN>
									</TD>
									<TD class="midcolora" width="18%">
										<asp:textbox id="txtCUSTOMER_FIRST_NAME" runat="server" size="25" maxlength="75"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvCUSTOMER_FIRST_NAME" Runat="server" ControlToValidate="txtCUSTOMER_FIRST_NAME"
											Display="Dynamic"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revCUSTOMER_FIRST_NAME" Runat="server" ControlToValidate="txtCUSTOMER_FIRST_NAME"
											Display="Dynamic"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="9%">
										<asp:label id="capCUSTOMER_MIDDLE_NAME" runat="server"></asp:label></TD>
									<TD class="midcolora" width="16%">
										<asp:textbox id="txtCUSTOMER_MIDDLE_NAME" runat="server" size="12" maxlength="10"></asp:textbox></TD>
									<TD class="midcolora" width="8%">
										<asp:label id="capCUSTOMER_LAST_NAME" runat="server"></asp:label><SPAN class="mandatory" id="spnMandatory">*</SPAN>
									</TD>
									<TD class="midcolora" width="28%">
										<asp:textbox id="txtCUSTOMER_LAST_NAME" runat="server" size="25" maxlength="25"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvCUSTOMER_LAST_NAME" Runat="server" ControlToValidate="txtCUSTOMER_LAST_NAME"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
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
							<asp:label id="capCUSTOMER_ZIP" runat="server"></asp:label><SPAN class="mandatory" id="spnCUSTOMER_ZIP" runat="server"></SPAN></TD>
						<TD class="midcolora" width="32%">
							<asp:textbox id="txtCUSTOMER_ZIP" runat="server" size="13" maxlength="10" OnBlur="DisableZipForCanada();"></asp:textbox><%--Called DisableZipForCanada() by Sibin on 25 Nov 08--%><%--<A href="#"><asp:image id="imgZipLookup" runat="server" ImageAlign="Bottom" ImageUrl="/cms/cmsweb/images/info.gif"></asp:image></A>--%>
							<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
								<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
							</asp:hyperlink><BR>
								<asp:customvalidator id="csvCUSTOMER_ZIP" Runat="server" ClientValidationFunction="ChkEmpResult" ErrorMessage=" "
							Display="Dynamic" ControlToValidate="txtCUSTOMER_ZIP"></asp:customvalidator>
							<asp:regularexpressionvalidator id="revCUSTOMER_ZIP" Runat="server" ControlToValidate="txtCUSTOMER_ZIP" Display="Dynamic"></asp:regularexpressionvalidator></TD>
					</TR>
					<TR>
						<TD class="midcolora" width="18%">
							<asp:Label id="capCARD_TYPE" Runat="server"></asp:Label><SPAN class="mandatory">*</SPAN></TD>
						<TD class="midcolora" width="32%">
							<asp:DropDownList id="cmbCARD_TYPE" onfocus="SelectComboIndex('cmbCARD_TYPE')" Runat="server"></asp:DropDownList><BR>
							<asp:RequiredFieldValidator id="rfvCARD_TYPE" Runat="server" ControlToValidate="cmbCARD_TYPE" Display="Dynamic"></asp:RequiredFieldValidator></TD>
						<TD class="midcolora" width="18%">
							<asp:Label id="capCARD_NO" runat="server">Credit Card #  :</asp:Label><SPAN class="mandatory">*</SPAN></TD>
						<TD class="midcolora" width="32%">
							<asp:textbox id="txtCARD_NO" Runat="server" MaxLength="16" size="21"></asp:textbox><BR>
							<asp:RegularExpressionValidator id="revCARD_NO" Runat="server" ControlToValidate="txtCARD_NO" Display="Dynamic"></asp:RegularExpressionValidator><BR>
							<asp:CustomValidator id="csvCARD_NO" Runat="server" ControlToValidate="txtCARD_NO" Display="Dynamic"
								ClientValidationFunction="ValCardNumLen"></asp:CustomValidator>
							<asp:RequiredFieldValidator id="rfvCARD_NO" Runat="server" ControlToValidate="txtCARD_NO" Display="Dynamic"></asp:RequiredFieldValidator></TD>
					</TR>
					<TR>
						<TD class="midcolora" width="18%">
							<asp:Label id="capCARD_CVV_NUMBER" Runat="server"></asp:Label><SPAN class="mandatory">*</SPAN></TD>
						<TD class="midcolora" width="32%">
							<asp:TextBox id="txtCARD_CVV_NUMBER" Runat="server" MaxLength="4" size="5"></asp:TextBox><BR>
							<asp:RegularExpressionValidator id="revCARD_CVV_NUMBER" Runat="server" ControlToValidate="txtCARD_CVV_NUMBER" Display="Dynamic"></asp:RegularExpressionValidator>
							<asp:CustomValidator id="csvCARD_CVV_NUMBER" Runat="server" ControlToValidate="txtCARD_CVV_NUMBER" Display="Dynamic"
								ClientValidationFunction="ValCardCVVNumLen"></asp:CustomValidator>
							<asp:RequiredFieldValidator id="rfvCARD_CVV_NUMBER" Runat="server" ControlToValidate="txtCARD_CVV_NUMBER" Display="Dynamic"></asp:RequiredFieldValidator></TD>
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
							<asp:textbox id="txtCARD_DATE_VALID_TO" Runat="server" MaxLength="2" size="2"></asp:textbox><BR>
							<asp:RequiredFieldValidator id="rfvCARD_DATE_VALID_TO" Runat="server" ControlToValidate="txtCARD_DATE_VALID_TO"
								Display="Dynamic"></asp:RequiredFieldValidator>
							<asp:rangevalidator id="rngCARD_DATE_VALID_TO" Runat="server" ControlToValidate="txtCARD_DATE_VALID_TO"
								Display="Dynamic" Type="Integer"></asp:rangevalidator>
							<asp:CompareValidator id="cmpCARD_DATE_VALID_TO" Runat="server" ControlToValidate="txtCARD_DATE_VALID_TO"
								Display="Dynamic" Operator="GreaterThanEqual"></asp:CompareValidator>
							<asp:CustomValidator id="csvCARD_DATE_VALID_TO" Runat="server" ControlToValidate="txtCARD_DATE_VALID_TO"
								Display="Dynamic" ClientValidationFunction="chkToYearLength"></asp:CustomValidator></TD>
					</TR>
				</asp:panel>
				</table>
				<table id ='tblBtnSave' width="100%">
				<TR>
					<TD class="midcolorr" colSpan="4">
						<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></TD>
				</TR>
				</table>
			</table>
			<INPUT id="hidCCFlag" type="hidden" value="0" name="hidCCFlag" runat="server">
		<INPUT id="hidEFTFlag" type="hidden" value="0" name="hidEFTFlag" runat="server">
		
		<INPUT id="hid_ENCRYP_FEDERAL_ID" type="hidden" value="0" name="hid_ENCRYP_FEDERAL_ID" runat="server">
		<INPUT id="hid_ENCRYP_TRANSIT_ROUTING_NO" type="hidden" value="0" name="hid_ENCRYP_TRANSIT_ROUTING_NO" runat="server">
		<INPUT id="hid_ENCRYP_DFI_ACC_NO" type="hidden" value="0" name="hid_ENCRYP_DFI_ACC_NO" runat="server">
		
		</form>
		<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
		<INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
		<INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
		<INPUT id="hidPOLICY_LOB" type="hidden" value="0" name="hidPOLICY_LOB" runat="server">
		<INPUT id="hidDAY" type="hidden" value="0" name="hidDAY" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
		<INPUT id="hidBILL_TYPE" type="hidden" name="hidBILL_TYPE" runat="server"> <INPUT id="hidCUSTOMER_INFO" type="hidden" name="hidCUSTOMER_INFO" runat="server">
		<INPUT id="hidREVERIFIED_AC" type="hidden" value="0" name="hidREVERIFIED_AC" runat="server">
		<!--Added By Raghav For Itrack Issue #4998-->
			<input id="hidCUSTOMER_NAME" type="hidden" runat="server"> 
			<input id="hidCARD_TYPE" type="hidden" runat="server">
			<input id="hidSTATE_COUNTRY_LIST" type="hidden" name="hidSTATE_COUNTRY_LIST" runat="server"> <%--Added by Sibin on 25 Nov 08--%>
			<input id="hidSTATE_ID" type="hidden" name="hidSTATE_ID" runat="server"> <%--Added by Sibin on 25 Nov 08--%>
		
		
		
		<script>
			SetDisplay(); 
			ApplyColor();
			ChangeColor();
		</script>
	</body>
</HTML>

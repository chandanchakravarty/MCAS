
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddApplicantInsued.aspx.cs" AutoEventWireup="false" Inherits="Cms.Client.Aspx.AddApplicantInsued" EnableViewState="false" validateRequest="false" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitiozipnal//EN" >

<script runat="server">

    protected void txtLAST_NAME_TextChanged(object sender, EventArgs e)
    {

    }
</script>

<HTML>
	<HEAD>
		<title>Co-Applicant Insured</title>
		
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script> 
	    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
		<script language="javascript">
		var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
		var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		var serviceCount=0;
		function AddData()
		{
			
			document.getElementById('hidAPPLICANT_ID').value	=	'New';
			document.getElementById('cmbTITLE').focus();
			//document.getElementById('cmbPOSITION').focus();
			document.getElementById('cmbTITLE').options.selectedIndex = -1;
			document.getElementById('txtFIRST_NAME').value  = '';
			document.getElementById('txtMIDDLE_NAME').value  = '';
			document.getElementById('txtLAST_NAME').value  = '';
			document.getElementById('txtADDRESS1').value  = '';
			document.getElementById('txtCITY').value  = '';
			document.getElementById('cmbCOUNTRY').options.selectedIndex = 0;
			document.getElementById('cmbSTATE').options.selectedIndex = -1;
			document.getElementById('txtZIP_CODE').value  = '';
			document.getElementById('txtPHONE').value  = '';
			document.getElementById('txtEMAIL').value  = '';
			document.getElementById('txtMOBILE').value = '';
			//document.getElementById('txtCO_APPLI_EMPL_PHONE').value = '';
			//document.getElementById("trPOSITION").style.display = 'none';
			ChangeColor();
			DisableValidators();
			SSN_change();
		}
		function populateXML() {
		    // Added by mohit on 7/10/2005.
		    
		    if (document.getElementById('hidCUSTOMER_TYPE').value == "11110") {
		        //Added by Sibin on 21 Oct 08 to eliminate null object
		        //					if(document.getElementById('btnMakePrimaryApplicant')!=null)				
		        //						document.getElementById('btnMakePrimaryApplicant').style.display="none";

		        //document.getElementById('cmbCO_APPL_GENDER').style.display="inline";
		        //document.getElementById('capCO_APPL_GENDER').style.display="inline";
		        //document.getElementById('spnG').style.display="inline";
		        //EnableValidator('rfvGENDER',true);  //by pravesh
		        document.getElementById("trPOSITION").style.display = 'none';
		    }
		    else {
		        //Modified by Asfa (05-June-2008) - iTrack #4306
		        if (document.getElementById('hidAPPLICANT_ID').value != "" && document.getElementById('hidAPPLICANT_ID').value != "0" && document.getElementById('hidAPPLICANT_ID').value != "New")
		        { }
		        //Added by Sibin on 21 Oct 08 to eliminate null object
		        //						if(document.getElementById('btnMakePrimaryApplicant')!=null)
		        //							document.getElementById('btnMakePrimaryApplicant').style.display="inline";
		        else {
		            //Added by Sibin on 21 Oct 08 to eliminate null object
		            //						if(document.getElementById('btnMakePrimaryApplicant')!=null)
		            //							document.getElementById('btnMakePrimaryApplicant').style.display="none";
		        }
		        //document.getElementById('cmbCO_APPL_GENDER').style.display="none";
		        //document.getElementById('capCO_APPL_GENDER').style.display="none";
		        //document.getElementById('spnG').style.display="none";
		        //EnableValidator('rfvGENDER',false); //by pravesh
		        document.getElementById("trPOSITION").style.display = 'none';
		    }

		    if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')) {
		        var tempXML = '';
		        tempXML = document.getElementById('hidOldData').value;
		        //alert(tempXML);
		        if (tempXML != "" && tempXML != 0) {
		            populateFormData(tempXML, CLT_APPLICANT_LIST);
		            //alert('hid data' + document.getElementById('hidOldData').value);
		            if (document.getElementById('lblPRIMARY_APPLICANT').innerText == 'Yes' && document.getElementById('hidCUSTOMER_TYPE').value == '11110' && document.getElementById('btnActivateDeactivate') != null) //Added by Sibin//Personal Customer & Primary Co Applicant
		            {
		                document.getElementById('btnActivateDeactivate').style.display = 'none';
		                document.getElementById('hidPRIMARY_APP').value = document.getElementById('lblPRIMARY_APPLICANT').innerText;

		            }
		            else {
		                //Added by Sibin
		                if (document.getElementById('lblPRIMARY_APPLICANT').innerText == 'Yes' && document.getElementById('hidCUSTOMER_TYPE').value == '11110' && document.getElementById('btnActivateDeactivate') != null) {
		                    document.getElementById('btnActivateDeactivate').style.display = "inline";
		                    document.getElementById('hidPRIMARY_APP').value = document.getElementById('lblPRIMARY_APPLICANT').innerText;
		                }
		            }
		            SSN_hide();
		        }
		        else {
		            AddData();
		        }
		        GenerateCustomerCode();
		        
		    }

		    if ('<%=isActive%>' == 'N') {
		        document.getElementById("trRow1").style.display = "none";
		    }
		    else {
		        document.getElementById("trRow1").style.display = "inline";
		    }

		    //Added 2 functions by Sibin 0n 14-10-08 for Itrack Issue 4843

		    EmpDetails_DisableZipForCanada();
		    return false;

		}
        function DoBackToAssistant() {
            
            this.parent.parent.document.location.href = "CustomerManagerIndex.aspx";
            return false;
        }
        
        //Added By Lalit If REG_ID_ISSUE Text Box Is BLANK
//        function RegID() {

//            if (document.getElementById("txtREG_ID_ISSUE").value != "") {
//                rfvREG_ID_ISSUE.setAttribute('enabled', false);
//                rfvREG_ID_ISSUE.setAttribute('isValid', false);
//                rfvREG_ID_ISSUE.style.display = "none";

//            } else {
//                rfvREG_ID_ISSUE.setAttribute('enabled', true);
//                rfvREG_ID_ISSUE.setAttribute('isValid', true);
//                rfvREG_ID_ISSUE.style.display = "inline";
//            }
//        }
		
		function MakeNonMandetoryForCommercialCustomer()
		{
		    
			if(document.getElementById('hidCUSTOMER_TYPE').value =='11109')//commercial
			{
			    //document.getElementById('capFIRST_NAME').innerHTML = "First Name/Company Name";
			    document.getElementById('capFIRST_NAME').innerHTML = "Contact Name";
				EnableDisableDesc(document.getElementById('txtFIRST_NAME'),document.getElementById('rfvFIRST_NAME'),document.getElementById('spnFIRST_NAME'),false);			
			//	EnableDisableDesc(document.getElementById('txtLAST_NAME'),document.getElementById('rfvLAST_NAME'),document.getElementById('spnLAST_NAME'),false);			
				//EnableDisableDesc(document.getElementById('txtCO_APPL_DOB'),document.getElementById('rfvDATE_OF_BIRTH'),document.getElementById('spnCO_APPL_DOB'),false);			
				//EnableDisableDesc(document.getElementById('cmbCO_APPL_MARITAL_STATUS'),document.getElementById('rfvCO_APPL_MARITAL_STATUS'),document.getElementById('spnCO_APPL_MARITAL_STATUS'),false);			
				EnableDisableDesc(document.getElementById('txtADDRESS1'));	
				//,document.getElementById('rfvADDRESS1'),document.getElementById('spnADDRESS1'),false)		
				//EnableDisableDesc(document.getElementById('cmbSTATE'),document.getElementById('rfvSTATE'),document.getElementById('spnSTATE'),false);			
				EnableDisableDesc(document.getElementById('txtZIP_CODE'));
//				document.getElementById('rfvZIP_CODE'),document.getElementById('spnZIP_CODE'),false)			
				//EnableDisableDesc(document.getElementById('cmbCO_APPLI_OCCU'),document.getElementById('rfvCO_APPLI_OCCU'),document.getElementById('spnCO_APPLI_OCCUPATION'),false);			
				document.getElementById('txtFIRST_NAME').size=65;
				
			}
			else if(document.getElementById('hidCUSTOMER_TYPE').value =='11110')//personal
			{
				document.getElementById('capFIRST_NAME').innerHTML = "Contact Name";
				EnableDisableDesc(document.getElementById('txtFIRST_NAME'),document.getElementById('rfvFIRST_NAME'),document.getElementById('spnFIRST_NAME'),true);			
				//EnableDisableDesc(document.getElementById('txtLAST_NAME'),document.getElementById('rfvLAST_NAME'),document.getElementById('spnLAST_NAME'),true);			
				//EnableDisableDesc(document.getElementById('txtCO_APPL_DOB'),document.getElementById('rfvDATE_OF_BIRTH'),document.getElementById('spnCO_APPL_DOB'),true);			
				//EnableDisableDesc(document.getElementById('cmbCO_APPL_MARITAL_STATUS'),document.getElementById('rfvCO_APPL_MARITAL_STATUS'),document.getElementById('spnCO_APPL_MARITAL_STATUS'),true);			
				EnableDisableDesc(document.getElementById('txtADDRESS1'));	
				//,document.getElementById('rfvADDRESS1'),document.getElementById('spnADDRESS1'),true)		
				//EnableDisableDesc(document.getElementById('cmbSTATE'),document.getElementById('rfvSTATE'),document.getElementById('spnSTATE'),true);			
				EnableDisableDesc(document.getElementById('txtZIP_CODE'));
				//,document.getElementById('rfvZIP_CODE'),document.getElementById('spnZIP_CODE'),true);			
				//EnableDisableDesc(document.getElementById('cmbCO_APPLI_OCCU'),document.getElementById('rfvCO_APPLI_OCCU'),document.getElementById('spnCO_APPLI_OCCUPATION'),true);			
				document.getElementById('txtFIRST_NAME').size=30;
			}
		}

		function EnableDisableDesc(txtDesc, rfvDesc, spnDesc, flag) {
		    
		    if (flag == false) {
		        if (rfvDesc != null) {

		            rfvDesc.setAttribute('enabled', false);
		            rfvDesc.setAttribute('isValid', false);
		            rfvDesc.style.display = "none";
		            spnDesc.style.display = "none";
		            txtDesc.className = "";
		        }

		    }
		    else {
		        if (rfvDesc != null) {
		            rfvDesc.setAttribute('enabled', true);
		            rfvDesc.setAttribute('isValid', true);
		            spnDesc.style.display = "inline";
		            txtDesc.className = "MandatoryControl";
		        }
		    }
		}


		function ChkCoapplicantDateOfBirth(objSource, objArgs) {
		    if (document.getElementById("revCO_APPL_DOB").isvalid == true) {
		        var effdate = document.CLT_APPLICANT_LIST.txtCO_APPL_DOB.value;
		        objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
		    }
		    else
		        objArgs.IsValid = true;
		}

		function ChkCreatedate(objSource, objArgs) {
		    if (document.getElementById("revCO_APPL_DOB").isvalid == true) {
		        var effdate = document.CLT_APPLICANT_LIST.txtCO_APPL_DOB.value;
		        objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
		    }
		    else
		        objArgs.IsValid = true;
		}
		function EnableDisable() {
		    if (document.getElementById("hidIS_ACTIVE").Value == "N")
		        document.getElementById("trRow1").style.display = "none";
		    else {
		        document.getElementById("trRow1").style.display = "inline";
		    }
		}
	
	
	// Added by Swastika on 21st Mar'06 for Gen Iss #2367
		function CheckIfPhoneEmpty() {
		    if (document.getElementById('txtCO_APPLI_BUSINESS_PHONE').value == "") {
		        document.getElementById('txtEXT').value = "";
		        return false;
		    }
		    else
		        return true;
		}
	
	

	function showPageLookupLayer(controlId) {
	  
				var lookupMessage;
				switch (controlId) {
				    case "cmbTITLE":
				        lookupMessage = " ";
				        break;
				    case "cmbCO_APPL_RELATIONSHIP":
				        lookupMessage = "DRACD.";
				        break;
				    case "cmbCO_APPLI_OCCU":
				        lookupMessage = "%OCC.";
				        break;
				    case "cmbPOSITION":
				        lookupMessage = "%OCC.";
				        break;
				    default:
				        lookupMessage = "Look up code not found";
				        break;

				}
				showLookupLayer(controlId,lookupMessage);							
			}
			
		function FetchDataTitle()
		{
			SelectComboOption('cmbTITLE',document.getElementById('hidTITLE').value);
		}
		function FetchDataPosition() {
		    SelectComboOption('cmbPOSITION', document.getElementById('hidPOSITION').value);
		}
		function FetchDataOccup()
		{
			//SelectComboOption('cmbCO_APPLI_OCCU',document.getElementById('hidCO_APPLI_OCCU').value);
		}
		
		function FetchDataRelation()
		{
			//SelectComboOption('cmbCO_APPL_RELATIONSHIP',document.getElementById('hidCO_APPL_RELATIONSHIP').value);
		}
		
			
		function GoToNewQuote()
		{		
			parent.parent.document.parentWindow.location.href="/cms/cmsweb/aspx/quotetab.aspx?CalledFromMenu=Y";
			return false;
		}
	
	
		function GoToNewApplication()
		{	
			parent.parent.document.parentWindow.location.href="/CMS/POLICIES/ASPX/POLICYTAB.aspx?CALLEDFROM=CLT";
			return false;
		}

//		function CheckIfPhoneEmpty() {
//		    if (document.getElementById('txtCUSTOMER_BUSINESS_PHONE').value == "") {
//		        //document.getElementById('txtCUSTOMER_EXT').value = ""
//		        return false;
//		    }
//		    else
//		        return true;
//		}
	// Added by Swarup For checking zip code for LOB: Start
	/*
		function GetZipForState_old()
		{		    
			GlobalError=true;
			if(document.getElementById('cmbSTATE').value==14 ||document.getElementById('cmbSTATE').value==22||document.getElementById('cmbSTATE').value==49)
			{ 
				if(document.getElementById('txtZIP_CODE').value!="")
				{
					var intStateID = document.getElementById('cmbSTATE').options[document.getElementById('cmbSTATE').options.selectedIndex].value;
					var strZipID = document.getElementById('txtZIP_CODE').value;	
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
		*/	
		function ChkResult(objSource , objArgs)
		{
			objArgs.IsValid = true;
			if(objArgs.IsValid == true)
			{
				objArgs.IsValid = GetZipForState();
				if(objArgs.IsValid == false)
					document.getElementById('csvZIP_CODE').innerHTML = "The zip code does not belong to the state";				
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
		
		//for Employer Details
		/*
		function GetCoZipForState_Old()
		{		    
			GlobalError=true;
			if(document.getElementById('cmbCO_APPLI_EMPL_STATE').value==14 ||document.getElementById('cmbCO_APPLI_EMPL_STATE').value==22||document.getElementById('cmbCO_APPLI_EMPL_STATE').value==49)
			{ 
				if(document.getElementById('txtCO_APPLI_EMPL_ZIP_CODE').value!="")
				{
					var intStateID = document.getElementById('cmbCO_APPLI_EMPL_STATE').options[document.getElementById('cmbCO_APPLI_EMPL_STATE').options.selectedIndex].value;
					var strZipID = document.getElementById('txtCO_APPLI_EMPL_ZIP_CODE').value;	
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
		*/	
		function ChkCoResult(objSource , objArgs)
		{
			objArgs.IsValid = true;
			if(objArgs.IsValid == true)
			{
				objArgs.IsValid = GetCoZipForState();
				if(objArgs.IsValid == false)
				{}
					//document.getElementById('csvCO_APPLI_EMPL_ZIP_CODE').innerHTML = "The zip code does not belong to the state";				
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
		
		//
		
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
		
		function ResetTheForm()
		{
			DisableValidators();
			document.getElementById('hidRESET').value='1';
			document.getElementById('hidFormSaved').value = '1';
			document.CLT_APPLICANT_LIST.reset();
			//populateXML();			
			return true;
		}
		
		//////////////////////////////////////ZIP AJAX CALL///////////////////////
		function GetZipForState()
		{		    
			GlobalError=true;
			if(document.getElementById('cmbSTATE').value==14 ||document.getElementById('cmbSTATE').value==22||document.getElementById('cmbSTATE').value==49)
			{ 
				if(document.getElementById('txtZIP_CODE').value!="")
				{
					var intStateID = document.getElementById('cmbSTATE').options[document.getElementById('cmbSTATE').options.selectedIndex].value;
					var strZipID = document.getElementById('txtZIP_CODE').value;	
					var result = AddApplicantInsued.AjaxFetchZipForState(intStateID,strZipID);
					return AjaxCallFunction_CallBack(result);
					
				}
				return false;
			}
			else 
				return true;
				
		}
		
		function AjaxCallFunction_CallBack(response)
		{		
		 if(document.getElementById('cmbSTATE').value==14 ||document.getElementById('cmbSTATE').value==22||document.getElementById('cmbSTATE').value==49)
			{ 
				if(document.getElementById('txtZIP_CODE').value!="")
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
		
		function GetCoZipForState()
		{		    
			GlobalError=true;
//			if(document.getElementById('cmbCO_APPLI_EMPL_STATE').value==14 ||document.getElementById('cmbCO_APPLI_EMPL_STATE').value==22||document.getElementById('cmbCO_APPLI_EMPL_STATE').value==49)
//			{ 
//				if(document.getElementById('txtCO_APPLI_EMPL_ZIP_CODE').value!="")
//				{
//					var intStateID = document.getElementById('cmbCO_APPLI_EMPL_STATE').options[document.getElementById('cmbCO_APPLI_EMPL_STATE').options.selectedIndex].value;
//					var strZipID = document.getElementById('txtCO_APPLI_EMPL_ZIP_CODE').value;	
//					var result = AddApplicantInsued.AjaxFetchZipForState(intStateID,strZipID);
//					return AjaxCallFunction_CallBack_CoApp(result);
//					
//				}
//				return false;
//			}
			/*
			else 
				return true;
			*/	
			
		}
		
		function AjaxCallFunction_CallBack_CoApp(response)
		{		
//		 if(document.getElementById('cmbCO_APPLI_EMPL_STATE').value==14 ||document.getElementById('cmbCO_APPLI_EMPL_STATE').value==22||document.getElementById('cmbCO_APPLI_EMPL_STATE').value==49)
//			{ 
//				if(document.getElementById('txtCO_APPLI_EMPL_ZIP_CODE').value!="")
//				{
//					handleResult(response);
//					if(GlobalError)
//					{
//						return false;
//					}
//					else
//					{
//						return true;
//					}
//				}	
//				return false;
//			}
//			else 
//				return true;		
		}
		
		//////AJAX END/////
		
		
	//end Added by Swarup For checking zip code for LOB: End
		</script>
		<script language="javascript">
	//added by pravesh
	function fillstateFromCountry()
	{		   
			    
			  	GlobalError=true;
			      //var CmbState=document.getElementById('cmbSTATE');
				  var CountryID=  document.getElementById('cmbCOUNTRY').options[document.getElementById('cmbCOUNTRY').selectedIndex].value;
                  //var oResult='';
					AddApplicantInsued.AjaxFillState(CountryID,fillState);
					//fillState(oResult);
					if(GlobalError)
					{
						return false;
					}
					else
					{
						return true;
					}


	}
//	if (document.getElementById("cmbAPPLICANT_TYPE").value == "11109") { //  for commercial customer
//	    doc.getElementById("cltClientTop_lblFullName").innerHTML = document.getElementById("txtCUSTOMER_FIRST_NAME").value;
//	    document.getElementById("trMAIN_POSITION").style.display = 'none';
//	}
//	else if (document.getElementById("cmbAPPLICANT_TYPE").value == "11110") { //for personal
//	    var Fname = document.getElementById("txtCUSTOMER_FIRST_NAME").value;
//	    var MName = document.getElementById("txtCUSTOMER_MIDDLE_NAME").value
//	    var Lname = document.getElementById("txtCUSTOMER_LAST_NAME").value;
//	    var Name = trim(Fname + " " + MName + " ") + " " + Lname;
//	    doc.getElementById("cltClientTop_lblFullName").innerHTML = Name;
//	    document.getElementById("trMAIN_POSITION").style.display = 'none';
	
	function setStateID() 
	{
			var CmbState=document.getElementById('cmbSTATE');
		if (CmbState==null)
		    return;
		if (CmbState.selectedIndex != -1) {		    
		    document.getElementById('hidSTATE_ID').value = CmbState.value;
		}
		
	}
	function fillState(Result)
	{
	
	
		//var strXML;
		if(Result.error)
		{        
			var xfaultcode   = Result.errorDetail.code;
			var xfaultstring = Result.errorDetail.string;
			var xfaultsoap   = Result.errorDetail.raw;        				
		}
		else	
		{	
			var statesList = document.getElementById("cmbSTATE");
			statesList.options.length = 0; 
			oOption = document.createElement("option");
			oOption.value = "";
	 		oOption.text = "";
			statesList.add(oOption);
			ds=Result.value;
			if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
			{
				for (var i = 0; i < ds.Tables[0].Rows.length; ++i)
				{
				statesList.options[statesList.options.length] = new Option(ds.Tables[0].Rows[i]["STATE_NAME"],ds.Tables[0].Rows[i]["STATE_ID"]);
				}
			}	
			/*
			strXML= Result.value;	
			
			var xmlDoc = new ActiveXObject("Microsoft.XMLDOM") ;
			xmlDoc.async=false;
			xmlDoc.loadXML(strXML);
			xmlTableNodes = xmlDoc.selectNodes('/NewDataSet/Table');
			oOption = document.createElement("option");
			oOption.value = "";
	 		oOption.text = "";
			document.getElementById('cmbSTATE').length=0;
			document.getElementById('cmbSTATE').add(oOption);
			for(var i = 0; i < xmlTableNodes.length; i++ )
			{
				var text = 	xmlTableNodes[i].selectSingleNode('STATE_NAME').text;
				var value = 	xmlTableNodes[i].selectSingleNode('STATE_ID').text;
				
				oOption = document.createElement("option");
				oOption.value = value;
				oOption.text = text;
				document.getElementById('cmbSTATE').add(oOption);
			}
			*/		
		  
		  
		}
		
		return false;	
	}
	function CallService()
	{
	
		fillstateFromCountry();
		//fillState(document.getElementById('hidSTATE_COUNTRY_LIST'));
		
		
		//For Employee Country and States
		EmpDetails_fillstateFromCountry();
		//EmpDetails_fillState(document.getElementById('hidEmpDetails_STATE_COUNTRY_LIST'));
		EmpDetails_setStateID();
		
		
		//setEncryptFields();
	}
	
		//Added by Mohit Agarwal 3-Sep-08

		function SSN_change()
		{
			//document.getElementById('txtSSN_NO').value = document.getElementById('txtSSN_NO_HID').value;
			//document.getElementById('txtCO_APPL_SSN_NO').value = '';
			//document.getElementById('txtCO_APPL_SSN_NO').style.display = 'inline';
			//document.getElementById("revCO_APPL_SSN_NO").style.display='none';
			//document.getElementById("revCO_APPL_SSN_NO").setAttribute('enabled',true);
//			if(document.getElementById("btnCO_APPL_SSN_NO").value == 'Edit')
//				document.getElementById("btnCO_APPL_SSN_NO").value = 'Cancel';
//			else if(document.getElementById("btnCO_APPL_SSN_NO").value == 'Cancel')
//				SSN_hide();
//			else
//				document.getElementById("btnCO_APPL_SSN_NO").style.display = 'none';
				
			//document.getElementById('txtCO_APPL_SSN_NO_HID').style.display = 'none';			
		}
		
		function SSN_hide()
		{
//			document.getElementById("btnCO_APPL_SSN_NO").style.display = 'inline';
			//document.getElementById('txtCO_APPL_SSN_NO').value = document.getElementById('txtCO_APPL_SSN_NO_HID').value;
//			document.getElementById('txtCO_APPL_SSN_NO').value = '';
//			document.getElementById('txtCO_APPL_SSN_NO').style.display = 'none';
			//document.getElementById("revCO_APPL_SSN_NO").style.display='none';
			//document.getElementById("revCO_APPL_SSN_NO").setAttribute('enabled',false);
//			document.getElementById("btnCO_APPL_SSN_NO").value = 'Edit';
			//document.getElementById('txtCO_APPL_SSN_NO_HID').style.display = 'none';			
		}
		
		//Added by Sibin 0n 14-10-08 for Itrack Issue 4843

		function DisableZipForCanada() {
		 
			var myCountry=document.getElementById("cmbCOUNTRY");
			  
			if(myCountry.options[myCountry.selectedIndex].value=='2')
			{
				document.getElementById("revZIP_CODE").setAttribute("enabled",false);
			}
			
//			else
//			{
//				document.getElementById("revZIP_CODE").setAttribute("enabled",true);
//			}
		}
		
		
		function EmpDetails_setStateID()
		{
//			var CmbState=document.getElementById('cmbCO_APPLI_EMPL_STATE');
//			if (CmbState==null)
//				return;
//			if	(CmbState.selectedIndex!=-1)
//				document.getElementById('hidEmpDetails_STATE_ID').value=  CmbState.options[CmbState.selectedIndex].value;
			
		}
	
	function EmpDetails_fillState(Result)
	{
		//alert(document.getElementById('cmbCO_APPLI_EMPL_STATE').selectedIndex);
		var strXML;
		if(Result.error)
		{        
			var xfaultcode   = Result.errorDetail.code;
			var xfaultstring = Result.errorDetail.string;
			var xfaultsoap   = Result.errorDetail.raw;        				
		}
		else	
		{	
			var statesList = document.getElementById("cmbCO_APPLI_EMPL_STATE");
			statesList.options.length = 0; 
			oOption = document.createElement("option");
			oOption.value = "";
	 		oOption.text = "";
			statesList.add(oOption);
			ds=Result.value;
			if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
			{
				for (var i = 0; i < ds.Tables[0].Rows.length; ++i)
				{
				statesList.options[statesList.options.length] = new Option(ds.Tables[0].Rows[i]["STATE_NAME"],ds.Tables[0].Rows[i]["STATE_ID"]);
				}
			}
			/*
			strXML= Result.value;	
			//alert(strXML);
			var xmlDoc = new ActiveXObject("Microsoft.XMLDOM") ;
			xmlDoc.async=false;
			xmlDoc.loadXML(strXML);
			xmlTableNodes = xmlDoc.selectNodes('/NewDataSet/Table');
			oOption = document.createElement("option");
			oOption.value = "";
	 		oOption.text = "";
			document.getElementById('cmbCO_APPLI_EMPL_STATE').length=0;
			document.getElementById('cmbCO_APPLI_EMPL_STATE').add(oOption);
			for(var i = 0; i < xmlTableNodes.length; i++ )
			{
				var text = 	xmlTableNodes[i].selectSingleNode('STATE_NAME').text;
				var value = 	xmlTableNodes[i].selectSingleNode('STATE_ID').text;
				
				oOption = document.createElement("option");
				oOption.value = value;
				oOption.text = text;
				document.getElementById('cmbCO_APPLI_EMPL_STATE').add(oOption);
			}	
			*/	
		   EmpDetails_setStateID();  
		    document.getElementById('cmbCO_APPLI_EMPL_STATE').value = document.getElementById('hidEMPL_STATE_ID_OLD').value;
		}
		
		return false;	
	}
		
	function EmpDetails_fillstateFromCountry()
		{	
			   
			    
			  	GlobalError=true;
			      //var CmbState=document.getElementById('cmbCO_APPLI_EMPL_STATE');
				  var CountryID=  document.getElementById('cmbCO_APPLI_EMPL_COUNTRY').options[document.getElementById('cmbCO_APPLI_EMPL_COUNTRY').selectedIndex].value;
					var oResult='';
					AddApplicantInsued.AjaxFillState(CountryID,EmpDetails_fillState);
					//EmpDetails_fillState(oResult);
					if(GlobalError)
					{
						return false;
					}
					else
					{
						return true;
					}			
	   }
	
	
			
	function EmpDetails_DisableZipForCanada()
		{

			var myCountry=document.getElementById("cmbCO_APPLI_EMPL_COUNTRY");
			  
			if(myCountry.options[myCountry.selectedIndex].value=='2')
			{
				//document.getElementById("revCO_APPLI_EMPL_ZIP_CODE").setAttribute("enabled",false);
				//document.getElementById("revCO_APPLI_EMPL_ZIP_CODE").style.display = 'none';
			}
			
			else
			{
				//document.getElementById("revCO_APPLI_EMPL_ZIP_CODE").setAttribute("enabled",true);
			}
			/*if(document.getElementById("hidEmpDetails_STATE_COUNTRY_LIST").value != '')
			{
				alert(document.getElementById("cmbCO_APPLI_EMPL_COUNTRY").selectedIndex );
				document.getElementById("cmbCO_APPLI_EMPL_COUNTRY").selectedIndex = document.getElementById("hidEmpDetails_STATE_COUNTRY_LIST").value;
				alert(document.getElementById("cmbCO_APPLI_EMPL_COUNTRY").selectedIndex );
				Check();
			}*/
		}

		//Added By Lalit For Customer Type Change
		function OnCustomerTypeChange() {
		    
		    var customername = '<%= customerName %>';
		    var FirstName = '<%= FirstName %>';
		    if (document.getElementById('cmbAPPLICANT_TYPE').options.selectedIndex == -1)
		        document.getElementById('cmbAPPLICANT_TYPE').options.selectedIndex = 2;

		    if (document.getElementById('cmbAPPLICANT_TYPE').options[document.getElementById('cmbAPPLICANT_TYPE').options.selectedIndex].value == '11110') {

		        //Type is personal

		        //Changing error message of validation control
		        document.getElementById("rfvFIRST_NAME").innerHTML = document.getElementById("rfvFIRST_NAME").getAttribute("ErrMsgFirstName");
		        //document.getElementById("capFIRST_NAME").innerHTML = FirstName;
//		        document.getElementById("capFIRST_NAME").innerHTML = customername;
		        //End
		        
		        document.getElementById("tdF_NAME").colSpan = "1"
		        document.getElementById("tdF_NAME").style.width = "";

		        document.getElementById("tdF_NAME").style.width = "33%";
		        document.getElementById("tdM_NAME").style.width = "34%";
		        document.getElementById("tdL_NAME").style.width = "33%";
		        document.getElementById("tdM_NAME").style.display = "inline";
		        document.getElementById("tdL_NAME").style.display = "inline";
		        document.getElementById("txtFIRST_NAME").size = 35;
		      //  document.getElementById("rfvLAST_NAME").setAttribute('enabled', true);

		        document.getElementById("txtCPF_CNPJ").setAttribute('maxLength', '14');
		        document.getElementById("txtCPF_CNPJ").setAttribute('maxLength', '14');
		        document.getElementById("revCPF_CNPJ").innerText = document.getElementById("revCPF_CNPJ").getAttribute("ErrMsgcpf");
		        //document.getElementById('rfvREG_ID_ISSUE').setAttribute('enabled', true);
		        
		       // document.getElementById('rfvORIGINAL_ISSUE').setAttribute('enabled', true);
		        //document.getElementById('rfvREGIONAL_IDENTIFICATION').setAttribute('enabled', true);
		        document.getElementById('revREG_ID_ISSUE').setAttribute('enabled', true);
		        //document.getElementById('rfvCO_APPL_MARITAL_STATUS').setAttribute('enabled', true);
		        //document.getElementById('rfvGENDER').setAttribute('enabled', true);
		        //document.getElementById('rfvDATE_OF_BIRTH').setAttribute('enabled', true);
		       // document.getElementById('spnREG_ID_ISSUE').style.display = "inline";
		       // document.getElementById('spnORIGINAL_ISSUE').style.display = "inline";
		        //document.getElementById('spnREGIONAL_IDENTIFICATION').style.display = "inline";
		        //document.getElementById('spnMARITAL_STATUS').style.display = "inline";
		        //document.getElementById('spnGENDER').style.display = "inline";
		       // document.getElementById('spnDATE_OF_BIRTH').style.display = "inline";
		       document.getElementById('capCREATION_DATE').style.display = "none";
		       document.getElementById('capCO_APPL_DOB').style.display = "inline";
		       document.getElementById('revCO_APPL_DOB').setAttribute('enabled', true);
		       document.getElementById('csvCO_APPL_DOB').setAttribute('enabled', true);
		       document.getElementById('cpvREG_ID_ISSUE').setAttribute('enabled', true);
		       document.getElementById('cpvREG_ID_ISSUE2').setAttribute('enabled', true);
		       document.getElementById('csvCREATION_DATE').setAttribute('enabled', false );
		      // document.getElementById('csvCO_APPL_DOB').style.display = "inline";
		       document.getElementById("csvCREATION_DATE").style.display = "none";
		       //added by amit
               document.getElementById('cmbCO_APPL_GENDER').style.display = "inline";
		       document.getElementById('capCO_APPL_GENDER').style.display = "inline";
		       document.getElementById("cmbCO_APPL_MARITAL_STATUS").style.display = "inline";
		       document.getElementById("capCO_APPL_MARITAL_STATUS").style.display = "inline";
		       document.getElementById("txtPHONE").style.display = "inline";
		       document.getElementById("capPHONE").style.display = "inline";
//		       var el = document.getElementById("txtCO_APPL_DOB");
//		        el.readOnly = false;
		       document.getElementById("trPOSITION").style.display = 'none';
		       //document.getElementById('rfv2txtCO_APPL_DOB').setAttribute('enabled', false);
		       //document.getElementById("rfv2txtCO_APPL_DOB").style.display = 'none';

		    }
		    else {
		        //Type is commercial and government
               //added by amit
		        document.getElementById('cmbCO_APPL_GENDER').style.display = "none";
		        document.getElementById('capCO_APPL_GENDER').style.display = "none";
		        document.getElementById("cmbCO_APPL_MARITAL_STATUS").style.display = "none";
		        document.getElementById("capCO_APPL_MARITAL_STATUS").style.display = "none";
		        document.getElementById("txtPHONE").style.display = "none";
		        document.getElementById("capPHONE").style.display = "none";
                document.getElementById("rfvFIRST_NAME").innerHTML = document.getElementById("rfvFIRST_NAME").getAttribute("ErrMsgCustomerName");

//		        document.getElementById("capFIRST_NAME").innerHTML = customername;
//		        document.getElementById("txtLAST_NAME").value = '';
//		        document.getElementById("txtMIDDLE_NAME").colSpan = '';
		        document.getElementById("cmbCO_APPL_MARITAL_STATUS").style.display = "none";
		        
		       // document.getElementById("tdF_NAME").colSpan = "3"
//		        document.getElementById("tdM_NAME").style.display = "none";
//		        document.getElementById("tdL_NAME").style.display = "none";
//		        document.getElementById("tdF_NAME").width = "100%";
//		        document.getElementById("txtFIRST_NAME").size = "65";
		        //document.getElementById("rfvLAST_NAME").setAttribute('enabled', false);
		      //  document.getElementById("rfvLAST_NAME").setAttribute('isValid', true);
		      //  document.getElementById("rfvLAST_NAME").style.display = "none";

		        document.getElementById("txtCPF_CNPJ").setAttribute('maxLength', '18');
		        document.getElementById("revCPF_CNPJ").innerText = document.getElementById("revCPF_CNPJ").getAttribute("ErrMsgcnpj");
		       // document.getElementById('rfvREG_ID_ISSUE').setAttribute('enabled', false);
		       
		        //document.getElementById('rfvORIGINAL_ISSUE').setAttribute('enabled', false);
		        //document.getElementById('rfvREGIONAL_IDENTIFICATION').setAttribute('enabled', false);
		        document.getElementById('revREG_ID_ISSUE').setAttribute('enabled', false);
		        //document.getElementById('rfvCO_APPL_MARITAL_STATUS').setAttribute('enabled', false);
		       // document.getElementById('rfvGENDER').setAttribute('enabled', false);
		       // document.getElementById('rfvDATE_OF_BIRTH').setAttribute('enabled', false);
		        //document.getElementById("rfvORIGINAL_ISSUE").style.display = "none";
		        //document.getElementById("rfvREGIONAL_IDENTIFICATION").style.display = "none";
		        //document.getElementById("rfvCO_APPL_MARITAL_STATUS").style.display = "none";
		        //document.getElementById("rfvREG_ID_ISSUE").style.display = "none";
		       // document.getElementById("rfvGENDER").style.display = "none";
		        //document.getElementById("rfvDATE_OF_BIRTH").style.display = "none";
		        //document.getElementById('spnREG_ID_ISSUE').style.display = "none";
		        //document.getElementById('spnORIGINAL_ISSUE').style.display = "none";
		        //document.getElementById('spnREGIONAL_IDENTIFICATION').style.display = "none";
		        //document.getElementById('spnMARITAL_STATUS').style.display = "none";
		        //document.getElementById('spnGENDER').style.display = "none";
		       // document.getElementById('spnDATE_OF_BIRTH').style.display = "none";
		        document.getElementById('capCREATION_DATE').style.display = "inline";
		        document.getElementById('capCO_APPL_DOB').style.display = "none";
		        document.getElementById('revCO_APPL_DOB').setAttribute('enabled', true);
		        document.getElementById("revCO_APPL_DOB").style.display = "none";
		        document.getElementById('csvCO_APPL_DOB').setAttribute('enabled', false);
		        document.getElementById('cpvREG_ID_ISSUE').setAttribute('enabled', true);
		      //  document.getElementById('cpvREG_ID_ISSUE').style.display = "inline";
		        document.getElementById('cpvREG_ID_ISSUE2').setAttribute('enabled', true);
		        //document.getElementById("cpvREG_ID_ISSUE2").style.display = "inline";
		        document.getElementById('csvCREATION_DATE').setAttribute('enabled', true );
		        document.getElementById("csvCO_APPL_DOB").style.display = "none";
		      //  document.getElementById('csvCREATION_DATE').style.display = "inline";
//		        var el = document.getElementById("txtCO_APPL_DOB");
//		        el.readOnly = true;
		        document.getElementById("trPOSITION").style.display = 'none';
		        if (document.getElementById("txtREG_ID_ISSUE").value == document.getElementById("txtCO_APPL_DOB").value)
		            document.getElementById("cpvREG_ID_ISSUE").style.display = 'none';
		        document.getElementById('cpvREG_ID_ISSUE').setAttribute('enabled', false);
		        //document.getElementById('rfv2txtCO_APPL_DOB').setAttribute('enabled', true);

		        if (document.getElementById('cmbAPPLICANT_TYPE').value == '11109') {

//		            var item = document.getElementById('revCO_APPL_DOB').getAttribute("validationexpression");
//		            var item1 = item.replace("(19|20)", "(18|19|20)");
//		            document.getElementById('revCO_APPL_DOB').setAttribute("validationexpression", item1);

		        } else {

//		        var item = document.getElementById('revCO_APPL_DOB').getAttribute("validationexpression");
//		            var item1 = item.replace("(18|19|20)", "(19|20)");
//		            document.getElementById('revCO_APPL_DOB').setAttribute("validationexpression", item1);
		        }


		    }
		    document.getElementById('hidAPPLICANT_TYPE').value = document.getElementById('cmbAPPLICANT_TYPE').value;
		    //alert(document.getElementById('hidAPPLICANT_TYPE').value);
		}



		function CheckCPVMsg() {
		    if (document.getElementById("txtCO_APPL_DOB").value == document.getElementById("txtREG_ID_ISSUE").value) {
		        document.getElementById('cpvREG_ID_ISSUE').setAttribute('enabled', false);
		        document.getElementById('cpvREG_ID_ISSUE').style.display = 'none';
		    }
		    else
		        document.getElementById('cpvREG_ID_ISSUE').setAttribute('enabled', true);

		}
       
		 //For Bind the Titles forom Ajax
		 //AjaxFillTitles
		function FillTitles() {
		    
		    GlobalError = true;
		    if (document.getElementById('cmbAPPLICANT_TYPE').selectedIndex != -1 && document.getElementById('cmbAPPLICANT_TYPE').selectedIndex != 0) {
		        var value = document.getElementById('cmbAPPLICANT_TYPE').options[document.getElementById('cmbAPPLICANT_TYPE').selectedIndex].value;
		        var type = "";
		        //Personal
		        if (value == "11110")
		        //  type = "P"

		          
		            type = "1500"
		        //Commercial and Government
		        if (value == "11109" || value == "14725")
		        // type = "CO" and Gov


		            type = "11109"

		        if (value == "14725") {
		            type = "14725"
		        }
		        
		    }
		    else
		        return false;

		    AddApplicantInsued.AjaxFillTitles(type, BindTitles);
		    AddApplicantInsued.AjaxFillTitles(type, BindPosition);
		    //fillState(oResult);
		    if (GlobalError) {
		        return false;
		    }
		    else {
		        return true;
		    }
		}

		//BindTitles

		function BindTitles(Result) {

		    //var strXML;
		    if (Result.error) {
		        var xfaultcode = Result.errorDetail.code;
		        var xfaultstring = Result.errorDetail.string;
		        var xfaultsoap = Result.errorDetail.raw;
		    }
		    else {

		        // For cmbTITLE
		        var statesList = document.getElementById("cmbTITLE");
		        statesList.options.length = 0;
		        oOption = document.createElement("option");
		        oOption.value = "";
		        oOption.text = "";
		        statesList.add(oOption);
		        ds = Result.value;
		        if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
		            for (var i = 0; i < ds.Tables[0].Rows.length; ++i) {
		                statesList.options[statesList.options.length] = new Option(ds.Tables[0].Rows[i]["ACTIVITY_DESC"], ds.Tables[0].Rows[i]["ACTIVITY_ID"]);
		            }
		        }



		    }

		    return false;
		}
		function BindPosition(Result) {

		    //var strXML;
		    if (Result.error) {
		        var xfaultcode = Result.errorDetail.code;
		        var xfaultstring = Result.errorDetail.string;
		        var xfaultsoap = Result.errorDetail.raw;
		    }
		    else {

		        // For cmbPOSITION
		        var statesList = document.getElementById("cmbPOSITION");
		        statesList.options.length = 0;
		        oOption = document.createElement("option");
		        oOption.value = "";
		        oOption.text = "";
		        statesList.add(oOption);
		        ds = Result.value;
		        if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
		            for (var i = 0; i < ds.Tables[0].Rows.length; ++i) {
		                statesList.options[statesList.options.length] = new Option(ds.Tables[0].Rows[i]["ACTIVITY_DESC"], ds.Tables[0].Rows[i]["ACTIVITY_ID"]);
		            }
		        }



		    }
		   // document.getElementById("rfvcmbPOSITION").setAttribute('disabled', true);
		    
		    return false;
		}

		function GenerateCustomerCode() {
		 
		    
		    //we r generating the code only when user is in add mode
		    //Hence checking the mode of form

		    
		        if (document.getElementById('cmbAPPLICANT_TYPE').options.selectedIndex == 3) {
		            //Type is personal
		            //Code shoud be First 2 char and last name 2 char 

		            //Code should only be genarated when the event is coming from last name
		            if (document.getElementById('txtFIRST_NAME').value != "")
		             {
		                 if (document.getElementById('txtLAST_NAME').value != "") {
		                     document.getElementById('txtCONTACT_CODE').value = GenerateRandomCode(ReplaceAll(document.getElementById('txtFIRST_NAME').value, ' ', ''), ReplaceAll(document.getElementById('txtLAST_NAME').value,' ',''));
		                     if (document.getElementById('txtCONTACT_CODE').value != "") {
		                         document.getElementById("rfvCONTACT_CODE").setAttribute('isValid', true);
		                         document.getElementById("rfvCONTACT_CODE").style.display = "none";
		                     }
		                 }
		                 else {
		                     document.getElementById('txtCONTACT_CODE').value = (GenerateRandomCode(ReplaceAll(document.getElementById('txtFIRST_NAME').value,' ',''), ''));
		                     if (document.getElementById('txtCONTACT_CODE').value != "") {
		                         document.getElementById("rfvCONTACT_CODE").setAttribute('isValid', true);
		                         document.getElementById("rfvCONTACT_CODE").style.display = "none";
		                     }
		                     
		                 }
		            }
		        }
		        else {
		            //Type is commercial
		            //Code should first name first 4 chars
		            //Code should only be genarated when the event is coming from first name

		            if (document.getElementById('txtFIRST_NAME').value != "") {
		                document.getElementById('txtCONTACT_CODE').value = (GenerateRandomCode(document.getElementById('txtFIRST_NAME').value, ''));
		                if (document.getElementById('txtCONTACT_CODE').value != "") {
		                    document.getElementById("rfvCONTACT_CODE").setAttribute('isValid', true);
		                    document.getElementById("rfvCONTACT_CODE").style.display = "none";
		                }
		            }

		            
		        }
		             
		}

		function setlength(obj) {
		    var len = obj.value.length;

		    //Personal

		    if (document.getElementById('cmbAPPLICANT_TYPE').options.value == "11110") {
		        if (len == 14) {

		            window.event.returnValue = false
		        }
		    }
		    //Commercial
		    if (document.getElementById('cmbAPPLICANT_TYPE').options.value == "11109") {
		        if (len == 18) {
		            window.event.returnValue = false
		        }
		    }
		}
	
		function initPage() {
		    ApplyColor();
		    ChangeColor();
		    if (document.getElementById("hidAPPLICANT_ID").value == "" || document.getElementById("hidAPPLICANT_ID").value == "0") {
		    
		        FillTitles();
		    }
		    OnCustomerTypeChange();
		}
		function CountLength(objSource, objArgs) {
		    var objControl = document.getElementById(objSource.controltovalidate)
		    var len = objControl.value.length;
		    if (len > 500) {
		        objArgs.IsValid = false;
		    }
		    else {
		        objArgs.IsValid = true;
		    }
		}

		function SetTITLE() {
		    if (document.getElementById('cmbTITLE') != "") {
		        document.getElementById('hidTITLE').value = document.getElementById('cmbTITLE').value;
		    }
		}
		function setPOSITION() {
		    if (document.getElementById('cmbPOSITION') != "") {
		        document.getElementById('hidPOSITION').value = document.getElementById('cmbPOSITION').value;

		    }
		}
		
		
		</script>
		<script language="javascript" type="text/javascript">
<!--

		    //validate CPf/ CNPJ NO;
		    //For personal customer it accepts 14 digits CPF No
		    //And For Commercial Customer it must bus accept only 18 digit CNPJ NO
		    //we call a common function validar() for validate both CPF/CNPJ No


		    function validatCPF_CNPJ(objSource, objArgs) {
		        //get error message for xml on culture base. 
		        var cpferrormsg =  '<%=javasciptCPFmsg %>';
		        var cnpjerrormsg = '<%=javasciptCNPJmsg %>';
		        var CPF_invalid =  '<%=CPF_invalid %>';
		        var CNPJ_invalid = '<%=CNPJ_invalid %>';

		        var valid = false;
		        var idd = objSource.id;
		        var rfvid = idd.replace('csv', 'rev');
		        if (document.getElementById(rfvid) != null)
		            if (document.getElementById(rfvid).isvalid == true) {
		            var theCPF = document.getElementById(objSource.controltovalidate)
		            var len = theCPF.value.length;
		            if (document.getElementById('cmbAPPLICANT_TYPE').value == '11110') {
		                //for CPF # in if customer type is personal
		                //it check cpf format & valdate bia validar() function, CPF is valid or not
		                if (len == '14') {
		                    valid = validar(objSource, objArgs);
		                    if (valid) { objSource.innerText = ''; } else { objSource.innerText = CPF_invalid; }
		                }
		                else {

		                    if (document.getElementById(rfvid) != null) {
		                        if (document.getElementById(rfvid).isvalid == true) {
		                            objArgs.IsValid = false;
		                            objSource.innerHTML = cpferrormsg; //'Please enter 14 digit CPF No';
		                        } else {
		                            objSource.innerHTML = '';
		                        }
		                    }
		                }
		            } //for CNPJ # in if customer type is commercial
		            //it check CNPJ format & valdate bia validar() function CNPJ is valid or not
		            else if (document.getElementById('cmbAPPLICANT_TYPE').value == '11109' || document.getElementById('cmbAPPLICANT_TYPE').value == '14725') {
		                if (len == '18') {
		                    valid = validar(objSource, objArgs);
		                    if (!valid) {
		                        objSource.innerText = CNPJ_invalid;
		                    } else { objSource.innerText = ''; }
		                }
		                else {
		                    if (document.getElementById(rfvid) != null) {
		                        if (document.getElementById(rfvid).isvalid == true) {
		                            objArgs.IsValid = false;
		                            objSource.innerHTML = cnpjerrormsg; //'validate';
		                        } else { objSource.innerHTML = ''; }
		                    }
		                }
		            }
		        } else { objSource.innerHTML = ''; }
		    }

		    //function validate CPF/CNPJ # .
		    //created by Brazil team 
		    function validar(objSource, objArgs) {

		        var theCPF = document.getElementById(objSource.controltovalidate)
		        var errormsg = '<%=javasciptmsg  %>'
		        var ermsg = errormsg.split(',');
		        var intval = "0123456789";
		        var val = theCPF.value;
		        var flag = false;
		        var realval = "";
		        for (l = 0; l < val.length; l++) {
		            ch = val.charAt(l);
		            flag = false;
		            for (m = 0; m < intval.length; m++) {
		                if (ch == intval.charAt(m)) {
		                    flag = true;
		                    break;
		                }
		            } if (flag)
		                realval += val.charAt(l);
		        }




		        //if (theCPF.value == "") {
		        //alert("Campo inválido. É necessário informar o CPF ou CNPJ");	            
		        //alert(ermsg[0]);
		        // theCPF.focus();

		        //return (false);
		        //}
		        if (((realval.length == 11) && (realval == 11111111111) || (realval == 22222222222) || (realval == 33333333333) || (realval == 44444444444) || (realval == 55555555555) || (realval == 66666666666) || (realval == 77777777777) || (realval == 88888888888) || (realval == 99999999999) || (realval == 00000000000))) {
		            //alert("CPF/CNPJ inválido.");
		            objArgs.IsValid = false;
		            objArgs.innerHTML = ermsg[1];
		            //alert(ermsg[1]);
		            //theCPF.focus();
		            return (false);
		        }


		        if (!((realval.length == 11) || (realval.length == 14))) {
		            //alert("CPF/CNPJ inválido.");
		            //alert(ermsg[1]);
		            //theCPF.focus();
		            //return (false);
		            objArgs.innerHTML = ermsg[1];
		            objArgs.IsValid = false;
		        }

		        var checkOK = "0123456789";
		        var checkStr = realval;
		        var allValid = true;
		        var allNum = "";
		        for (i = 0; i < checkStr.length; i++) {
		            ch = checkStr.charAt(i);
		            for (j = 0; j < checkOK.length; j++)
		                if (ch == checkOK.charAt(j))
		                break;
		            if (j == checkOK.length) {
		                allValid = false;
		                break;
		            }
		            allNum += ch;
		        }
		        if (!allValid) {
		            //alert("Favor preencher somente com dígitos o campo CPF/CNPJ.");
		            //alert(ermsg[2]);
		            //theCPF.focus();
		            //return (false);
		            objArgs.innerHTML = ermsg[2];
		            objArgs.IsValid = false;
		        }

		        var chkVal = allNum;
		        var prsVal = parseFloat(allNum);
		        if (chkVal != "" && !(prsVal > "0")) {
		            //alert("CPF zerado !");
		            //alert(ermsg[3]);
		            //theCPF.focus();
		            //return (false);
		            objArgs.innerHTML = ermsg[3];
		            objArgs.IsValid = false;
		        }

		        if (realval.length == 11) {
		            var tot = 0;

		            for (i = 2; i <= 10; i++)
		                tot += i * parseInt(checkStr.charAt(10 - i));


		            if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(9))) {
		                //alert("CPF/CNPJ inválido.");
		                //alert(ermsg[1]);
		                //theCPF.focus();
		                //return (false);
		                //objArgs.innerHTML = ermsg[1];
		                document.getElementById(objSource.id).innerText = ermsg[1];
		                objArgs.IsValid = false;
		            }

		            tot = 0;

		            for (i = 2; i <= 11; i++)
		                tot += i * parseInt(checkStr.charAt(11 - i));

		            if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(10))) {
		                //alert("CPF/CNPJ inválido.");
		                //alert(ermsg[1]);
		                //theCPF.focus();
		                //return (false);
		                document.getElementById(objSource.id).innerText = ermsg[1];
		                //objArgs.innerHTML = ermsg[1];
		                objArgs.IsValid = false;
		            }
		        }
		        else {
		            var tot = 0;
		            var peso = 2;

		            for (i = 0; i <= 11; i++) {
		                tot += peso * parseInt(checkStr.charAt(11 - i));
		                peso++;
		                if (peso == 10) {
		                    peso = 2;
		                }
		            }

		            if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(12))) {
		                //alert("CPF/CNPJ inválido.");
		                //alert(ermsg[1]);
		                //theCPF.focus();
		                //return (false);
		                objArgs.innerHTML = ermsg[1];
		                objArgs.IsValid = false;
		            }

		            tot = 0;
		            peso = 2;

		            for (i = 0; i <= 12; i++) {
		                tot += peso * parseInt(checkStr.charAt(12 - i));
		                peso++;
		                if (peso == 10) {
		                    peso = 2;
		                }
		            }

		            if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(13))) {
		                // alert("CPF/CNPJ inválido.");
		                //alert(ermsg[1]);
		                //theCPF.focus();
		                //return (false);
		                objArgs.innerHTML = ermsg[1];
		                objArgs.IsValid = false;
		            }
		        }

		    }



		        


       
//-->
    </script>
        <%--Populate the Address based on the ZipeCode using jQuery ------- Added by Pradeep Kushwaha on 07 june 2010--%>
        <script type="text/javascript" language="javascript">

            $(document).ready(function() {


                $("#txtZIP_CODE").change(function() {
                    if (trim($('#txtZIP_CODE').val()) != '') {
                        var ZIPCODE = $("#txtZIP_CODE").val();
                        var COUNTRYID = "5";
                        ZIPCODE = ZIPCODE.replace(/[-]/g,"");
                        PageMethod("GetCustomerAddressDetailsUsingZipeCode", ["ZIPCODE", ZIPCODE, "COUNTRYID", COUNTRYID], AjaxSucceeded, AjaxFailed); //With parameters
                    }
                    else {
//                        $("#txtADDRESS1").val('');
//                        $("#txtADDRESS2").val('');
//                        $("#txtDISTRICT").val('');
//                        $("#txtCITY").val('');
                    }
                });

            });
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
                //Call the page method  
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

                var Addresses = result.d;

                var Addresse = Addresses.split('^');
               
                if (result.d != "" && Addresse[1] != 'undefined') {
                    $("#cmbSTATE").val(Addresse[1]);
                    $("#hidSTATE_ID").val(Addresse[1]);
                  //  $("#txtZIP_CODE").val(Addresse[2]);
                    $("#txtADDRESS1").val(Addresse[3] + ' ' + Addresse[4]);
                    //$("#txtCOMPLIMENT").val(Addresse[4]);
                    $("#txtDISTRICT").val(Addresse[5]);
                    $("#txtCITY").val(Addresse[6]);
                    var ZipeCode = $("#txtZIP_CODE").val();
                    ZipeCode = ZipeCode.replace(/[-]/g, "");
                    if (ZipeCode == "00000000")
                        alert($("#hidZipeCodeVerificationMsg").val());
                }
                else if (document.getElementById('cmbCOUNTRY').options[document.getElementById('cmbCOUNTRY').options.selectedIndex].value == '5') {
                    alert($("#hidZipeCodeVerificationMsg").val());
//                    $("#txtZIP_CODE").val('');
//                    $("#txtADDRESS1").val('');
//                    $("#txtCOMPLIMENT").val('');
//                    $("#txtDISTRICT").val('');
//                    $("#txtCITY").val('');
                }
            }

            function zipcodeval() {

                if (document.getElementById('cmbCOUNTRY').options[document.getElementById('cmbCOUNTRY').options.selectedIndex].value == '6') {
                    document.getElementById('revZIP_CODE').setAttribute('enabled', false);
                }
            }

            function zipcodeval1() {

                if (document.getElementById('cmbCOUNTRY').options[document.getElementById('cmbCOUNTRY').options.selectedIndex].value == '5') {
                    document.getElementById('revZIP_CODE').setAttribute('enabled', true);
                }
            }


            

            function FormatZipCode(vr) {

                var vr = new String(vr.toString());
                if (vr != "" && (document.getElementById('cmbCOUNTRY').options[document.getElementById('cmbCOUNTRY').options.selectedIndex].value == '5')) {

                    vr = vr.replace(/[-]/g, "");
                    num = vr.length;
                    if (num == 8 && (document.getElementById('cmbCOUNTRY').options[document.getElementById('cmbCOUNTRY').options.selectedIndex].value == '5')) {
                        vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
                        document.getElementById('revZIP_CODE').setAttribute('enabled', false);

                    }

                }

                return vr;
            }
            function FormatBankBranch(vr) {

                var vr = new String(vr.toString());
                if (vr != "") {

                    vr = vr.replace(/[-]/g, "");
                    num = vr.length;
                    if (num == 7) {
                        vr = vr.substr(0, 6) + '-' + vr.substr(6, 1);
                    }

                }
                return vr;
            }

            function AjaxFailed(result) {

                alert(result.d);
            }
        </script>
        <%-- End jQuery Implimentation for ZipeCode --%>
	</HEAD>
	<BODY  leftMargin="0" topMargin="0"  onload="initPage();populateXML();">
		<FORM id="CLT_APPLICANT_LIST" method="post" runat="server">			
			<P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
			<TABLE class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0">
				<TBODY>
					<TR>
						<TD>
							<TABLE class="tableWidthHeader" align="center" border="0">
								<TBODY>
                                    <tr>
                                        <td class="headereffectCenter" colspan="4">
                                            <asp:Label ID="lblHeader" runat="server">Co-Applicant Information</asp:Label>
                                        </td>
                                    </tr>

									<tr>
										<TD class="pageHeader" colSpan="3"><asp:Label ID="CapMessage" runat="server"></asp:Label></TD><%--Please note that all fields marked with * are mandatory--%>
									</tr>
									<tr>
										<td class="midcolorc" align="right" colSpan="3"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
										<td></td>
										<%--Added by Sibin for Itrack Issue 4964 on 17 Feb 09
										<td class="midcolorc" align="right" colSpan="0"></td>--%>
									</tr>
									<tr  style="DISPLAY: none" runat="server">
										<TD class="midcolora" width="18%"><asp:label id="capAPPLICANT_STATUS" runat="server">Applicant Status</asp:label></TD>
										<TD class="midcolora" width="32%"><asp:label id="lblAPPLICANT_STATUS" CssClass="labelfont" Runat="server"></asp:label></TD>
										<TD class="midcolora" width="18%"><asp:label id="capPRIMARY_APPLICANT" runat="server">Primary Applicant</asp:label><asp:label id="lblPRIMARY_APPLICANT" CssClass="labelfont" Runat="server"></asp:label></TD>
										
									</tr>
									<tr id="ttrSUFFIX" runat="server" style="display: none">
									
										<TD class="midcolora" width="32%"><!--A class="calcolora" href="javascript:showPageLookupLayer('cmbTITLE')"><IMG id="imgTitle" height="16" src="../../cmsweb/images/selecticon.gif" width="17" border="0"
													runat="server"></A--> 
										<TD class="midcolora" width="18%"><asp:label id="capSUFFIX" runat="server">Suffix</asp:label>
										<asp:textbox id="txtSUFFIX" runat="server" maxlength="5" size="6"></asp:textbox></TD>
									</tr>
									<!--Type,Title,Co-Aplicant Code -->
									<tr>
									 <td id="ttdAPPLICANT_TYPE" runat="server" class="midcolora" width="33%">
                                    <asp:Label ID="capAPPLICANT_TYPE" runat="server">Type</asp:Label><span runat="server" id="spnAPPLICANT_TYPE" class="mandatory">*</span><br />
                                      <asp:DropDownList ID="cmbAPPLICANT_TYPE" onfocus="SelectComboIndex('cmbAPPLICANT_TYPE');"
                                        runat="server">
                                     </asp:DropDownList>
                                     <br>
                                           <asp:RequiredFieldValidator ID="rfvCUSTOMER_TYPE" runat="server" ControlToValidate="cmbAPPLICANT_TYPE"
                                         Display="Dynamic" ></asp:RequiredFieldValidator>
                                   </td>
									<td id="ttdTITLE" runat="server" class="midcolora" width="34%"><asp:label id="capTITLE" runat="server">Title</asp:label>
									<br />
									<asp:dropdownlist id="cmbTITLE" Width="400px" onfocus="SelectComboIndex('cmbTITLE')" runat="server" onchange="SetTITLE()">
								
									</asp:dropdownlist><a id="hlkTITLE" runat="server" class="calcolora" href="javascript:showPageLookupLayer('cmbTITLE')">
                                    <%--<img height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0">--%></a>
									<input type="hidden" id="hidTITLE" runat="server"></td>
									<td id="ttdCONTACT_CODE" runat="server" class="midcolora" width="33%"><asp:Label runat="server" ID="capCONTACT_CODE">Contact Code</asp:Label><span id="spnCONTACT_CODE" runat="server"  class="mandatory">*</span>
									</br>
									<asp:TextBox runat="server" ID="txtCONTACT_CODE" MaxLength="9" ></asp:TextBox>
									<br />
									<asp:RequiredFieldValidator runat="server" ID="rfvCONTACT_CODE" Display="Dynamic" ErrorMessage="" ControlToValidate="txtCONTACT_CODE"></asp:RequiredFieldValidator>
									</td>		
									</tr>
									
									
									<!--First Name,Middle Name,Last Name -->
									
									
									<tr>
							
									
									<td class="midcolora"  runat="server" id="tdF_NAME"><asp:label id="capFIRST_NAME" runat="server">First Name</asp:label><span id="spnFIRST_NAME" class="mandatory">*</span>
									<br />
									<asp:textbox id="txtFIRST_NAME"  MaxLength="200"  runat="server"  size="30"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvFIRST_NAME" runat="server" ControlToValidate="txtFIRST_NAME" Display="Dynamic"></asp:requiredfieldvalidator>
									
									</td>
									<TD class="midcolora"  runat="server" id="tdM_NAME">
										<%-- itrack 1385(show only first name),modified by Naveen--%>
										<asp:label  id="capMIDDLE_NAME"  runat="server" Text="Middle Name"></asp:label>
										<br />
										
										<asp:textbox id="txtMIDDLE_NAME"   runat="server" 
                                            maxlength="100" size="30" 
                                            style="font-weight: 700;"></asp:textbox></TD>	
										
										<TD class="midcolora"  runat="server" id="tdL_NAME">
										<asp:label id="capLAST_NAME" runat="server">Last Name</asp:label>
										<br />
                                            <asp:textbox id="txtLAST_NAME"   runat="server" maxlength="100" size="30" 
                                                 style="font-weight: 700"></asp:textbox><br>
											<%--<asp:requiredfieldvalidator id="rfvLAST_NAME" Runat="server"   ControlToValidate="txtLAST_NAME" Display="Dynamic"></asp:requiredfieldvalidator>--%>
											</TD>		
									</tr>
														
									
									
									
									<!--Zip Code, Address,Number -->
									
									<tr>
									<td class="midcolora" id="ttdZIP_CODE" runat="server" width="33%"><asp:label id="capZIP_CODE" runat="server">Postal Code</asp:label><%--<span class="mandatory" id="spnZIP_CODE" runat="server">*</span>--%>
									<br />
									<%--<asp:textbox id="txtZIP_CODE" runat="server" maxlength="8" size="13" OnBlur="GetZipForState();DisableZipForCanada();"></asp:textbox>--%>
									<%-- Added by Swarup on 30-mar-2007 --%>
									<asp:textbox id="txtZIP_CODE"  OnBlur="this.value=FormatZipCode(this.value);zipcodeval();zipcodeval1();ValidatorOnChange();" runat="server" maxlength="8" size="13" ></asp:textbox>
								    <asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
									<asp:image id="imgZipLookup" Visible="false" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
									</asp:hyperlink>
									<BR>
									<asp:customvalidator id="csvZIP_CODE" Runat="server" ClientValidationFunction="ChkResult" ErrorMessage=" " Display="Dynamic" ControlToValidate="txtZIP_CODE"></asp:customvalidator>
									<%--<asp:requiredfieldvalidator id="rfvZIP_CODE" runat="server" ControlToValidate="txtZIP_CODE" Display="Dynamic"></asp:requiredfieldvalidator>--%><asp:regularexpressionvalidator id="revZIP_CODE" Runat="server" ControlToValidate="txtZIP_CODE" Display="Dynamic"></asp:regularexpressionvalidator>											
									
									</td>
									<td class="midcolora" width="33%" id="ttdADDRESS1" runat="server"><asp:label id="capADDRESS1" runat="server">Address</asp:label><%--<span id="spnADDRESS1" class="mandatory">*</span>--%>
									<br />
									<asp:textbox id="txtADDRESS1" runat="server" maxlength="200" size="35"></asp:textbox><BR>
											<%--<asp:requiredfieldvalidator id="rfvADDRESS1" runat="server" ControlToValidate="txtADDRESS1" Display="Dynamic"></asp:requiredfieldvalidator>--%>
									
									</td>
									<td id="ttdNUMBER" runat="server" class="midcolora" width="33%">
									<asp:Label runat="server" ID="capNUMBER">Number</asp:Label><%--<span class="mandatory">*</span>		--%>
									<br />
									<asp:TextBox runat="server" ID="txtNUMBER" size="25" MaxLength="10" ></asp:TextBox>
								    <br />
								   <%-- <asp:RequiredFieldValidator runat="server" ID="rfvNUMBER"  Display="Dynamic" ErrorMessage="" ControlToValidate="txtNUMBER"></asp:RequiredFieldValidator>--%>
									
									
									</td>
									</tr>
									
									
									<!--Compliment, District,City -->
									
									<tr>
									<td id="ttdADDRESS2" runat="server" class="midcolora" width="33%"><%--<asp:Label runat="server" ID="capCOMPLIMENT">Compliment</asp:Label> Commneted by Amit Kr. mishra for insuror--%>
                                    <asp:Label runat="server" ID="capADDRESS2" runat="server">Compliment</asp:Label>
									<br />
									<asp:textbox id="txtADDRESS2" runat="server" maxlength="150"  size="35"></asp:textbox>
									<%--<asp:TextBox runat="server"  AutoCompleteType="Disabled" ID="txtCOMPLIMENT" Visible="false" size="35" MaxLength="200"></asp:TextBox>--%>
									<br />
                                   

									</td>
									<td id="ttdDISTRICT" runat="server" class="midcolora" width="33%"><asp:Label runat="server" ID="capDISTRICT">District</asp:Label><%--<span class="mandatory">*</span>--%>
									<br />
									<asp:TextBox runat="server" ID="txtDISTRICT"></asp:TextBox>
									<br />
                                     <%--<asp:RequiredFieldValidator runat="server" ID="rfvDISTRICT" Display="Dynamic" ControlToValidate="txtDISTRICT" ErrorMessage=""></asp:RequiredFieldValidator>--%>									</td>
									<td  id="ttdCITY" runat="server" class="midcolora" width="33%"><asp:label id="capCITY" runat="server">City</asp:label><%--<span class="mandatory">*</span>--%>
									<br />
									<asp:textbox id="txtCITY" runat="server" maxlength="50" size="35"></asp:textbox><br />
								<%--<asp:RequiredFieldValidator runat="server" ID="rfvCITY" Display="Dynamic" ControlToValidate="txtCITY" ErrorMessage=""></asp:RequiredFieldValidator>--%>
								
									</td>
									</tr>
									
									
									<!--Country,State,CPF/CNPJ -->
									
									<tr>
									<td id="ttdCOUNTRY" runat="server" class="midcolora" width="33%"><asp:label id="capCOUNTRY" runat="server">Country</asp:label><%--<span class="mandatory">*</span>--%>
									<br />
									<asp:dropdownlist id="cmbCOUNTRY" onfocus="SelectComboIndex('cmbCOUNTRY')" runat="server" onchange="javascript:fillstateFromCountry();">
									<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><BR>
									<%--<asp:requiredfieldvalidator id="rfvCOUNTRY" runat="server" ControlToValidate="cmbCOUNTRY" Display="Dynamic"></asp:requiredfieldvalidator>--%>
									</td>
									<td class="midcolora" id="ttdSTATE" runat="server" width="34%"><asp:label id="capSTATE" runat="server">State</asp:label><%--<span class="mandatory" id="spnSTATE" >*</span>--%>
									<br />
									<asp:dropdownlist id="cmbSTATE" onchange="setStateID();"  runat="server">
									<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><BR>
									<%--<asp:requiredfieldvalidator id="rfvSTATE" runat="server" ControlToValidate="cmbSTATE" Display="Dynamic"></asp:requiredfieldvalidator>--%>
									
									</td>
									
									<td id="ttdCPF_CNPJ" runat="server" class="midcolora" width="33%"><asp:label id="capCPF_CNPJ" runat="server">CPF/CNPJ #</asp:label>
									<br />
									
									<asp:TextBox runat="server" ID="txtCPF_CNPJ" MaxLength="18" OnBlur="this.value=FormatCPFCNPJ(this.value); ValidatorOnChange();" size="30" Enabled=false></asp:TextBox>
									<br />
									<asp:RequiredFieldValidator runat="server" ID="rfvCPF_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtCPF_CNPJ" Enabled=false></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ID="revCPF_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtCPF_CNPJ" Enabled=false></asp:RegularExpressionValidator>
                                    <asp:CustomValidator runat="server" ID="csvCPF_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtCPF_CNPJ" ClientValidationFunction="validatCPF_CNPJ" Enabled=false></asp:CustomValidator>
									</td>
									</tr>
																	
									<!--Home Phone,Business Phone,Extension -->
									
									<tr>
									<td id="ttdPHONE" runat="server" class="midcolora" width="33%"><asp:label id="capPHONE" runat="server">Home Phone</asp:label><br />
									<asp:textbox id="txtPHONE" runat="server" maxlength="15" size="20" onblur="FormatBrazilPhone();"></asp:textbox><br>
											<asp:regularexpressionvalidator id="revPHONE" Runat="server" ControlToValidate="txtPHONE" Display="Dynamic"></asp:regularexpressionvalidator>
									
									</td>
									<td id="ttdCO_APPLI_BUSINESS_PHONE" runat="server" class="midcolora" width="33%">
									<asp:label id="capCO_APPLI_BUSINESS_PHONE" runat="server">Business Phone</asp:label>
									<br />
																		
									<asp:textbox id="txtCO_APPLI_BUSINESS_PHONE" runat="server" maxlength="15" size="20" onblur="FormatBrazilPhone();" ></asp:textbox><br>
											<asp:regularexpressionvalidator id="revCO_APPLI_BUSINESS_PHONE" Runat="server" ControlToValidate="txtCO_APPLI_BUSINESS_PHONE" Display="Dynamic"></asp:regularexpressionvalidator>
									</td>
									<td class="midcolora" id="ttdEXT" runat="server" width="33%"><asp:label id="capEXT" runat="server">Extension</asp:label>
									<br />
									<asp:textbox id="txtEXT" onblur="CheckIfPhoneEmpty();"   runat="server" maxlength="6" size="8"></asp:textbox><br />
									<asp:regularexpressionvalidator id="rev_EXT" Runat="server" ControlToValidate="txtEXT" Display="Dynamic"></asp:regularexpressionvalidator>
									<asp:regularexpressionvalidator id="revEXT" runat="server" ControlToValidate="txtEXT" Enabled="false" Display="Dynamic" ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator></TD>
									</td>
									</tr>			
									
									<!--Mobile Phone,Fax No,Email Address -->
									
									<tr>
									<td id="ttdMOBILE" runat="server" class="midcolora" width="33%"><asp:label id="capMOBILE" runat="server">Mobile Phone</asp:label>
									<br />
									<asp:textbox id="txtMOBILE" runat="server" maxlength="15" size="20" onblur="FormatBrazilPhone();"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revMOBILE" Runat="server" ControlToValidate="txtMOBILE" Display="Dynamic"></asp:regularexpressionvalidator>
									</td>
									<td id="ttdFAX" runat="server" class="midcolora" width="33%"><asp:Label runat="server" ID="capFAX">Fax Number</asp:Label>
									<br />
									<asp:TextBox runat="server" ID="txtFAX" maxlength="15" size="20" AutoCompleteType="Disabled" onblur="FormatBrazilPhone();"></asp:TextBox>
									<br />
									<asp:RegularExpressionValidator runat="server" ID="revFAX" Display="Dynamic" ControlToValidate="txtFAX" ErrorMessage="" ></asp:RegularExpressionValidator>
									</td>
									<td id="ttdEMAIL" runat="server" class="midcolora" width="33%"><asp:label id="capEMAIL" runat="server">Email Address</asp:label>
									<br />
									<asp:textbox id="txtEMAIL" runat="server" maxlength="50" size="35"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revEMAIL" Runat="server" ControlToValidate="txtEMAIL" Display="Dynamic"></asp:regularexpressionvalidator>
									</td>
									</tr>
									
									
									
									<!--Regional Identification,Reg Id Issue,Original Issue  -->          	
							
							        <tr id="ttrREGIONAL_IDENTIFICATION" runat="server">
							        <td class="midcolora" width="33%"><asp:Label runat="server" ID="capREGIONAL_IDENTIFICATION">Regional Identification</asp:Label><%--<span  id="spnREGIONAL_IDENTIFICATION"  runat="server" class="mandatory">*</span>--%>
							        <br />
							        <asp:TextBox runat="server"   AutoCompleteType="Disabled"  
                                            ID="txtREGIONAL_IDENTIFICATION" MaxLength="12"></asp:TextBox><br />
							        <%--<asp:RequiredFieldValidator runat="server" ID="rfvREGIONAL_IDENTIFICATION"  Display="Dynamic" ErrorMessage="" ControlToValidate="txtREGIONAL_IDENTIFICATION"></asp:RequiredFieldValidator>--%>
							        </td>
							        <td class="midcolora" width="34%">
							        <asp:Label runat="server" ID="capREG_ID_ISSUE">Regional ID Issue</asp:Label><%--<span id="spnREG_ID_ISSUE" runat="server" class="mandatory">*</span>--%>
							        <br />
                                    <asp:TextBox runat="server" ID="txtREG_ID_ISSUE" OnBlur="FormatDate();CheckCPVMsg();" AutoCompleteType="Disabled" CausesValidation="true"></asp:TextBox>
                <asp:HyperLink ID="hlkREG_ID_ISSUE" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgREG_ID_ISSUE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif">
                    </asp:Image>
                </asp:HyperLink><br />
                <%--<asp:RequiredFieldValidator runat="server" ID="rfvREG_ID_ISSUE" Display="Dynamic"
                    ErrorMessage="" ControlToValidate="txtREG_ID_ISSUE"></asp:RequiredFieldValidator>--%>
                <asp:RegularExpressionValidator runat="server" ID="revREG_ID_ISSUE" Display="Dynamic"
                    ControlToValidate="txtREG_ID_ISSUE"></asp:RegularExpressionValidator>
                    
                 
                   
                    <asp:comparevalidator id="cpvREG_ID_ISSUE2" ControlToValidate="txtREG_ID_ISSUE" Display="Dynamic" Runat="server" Type="Date"
					             Operator="LessThanEqual" 
					             ValueToCompare="<%# DateTime.Today.ToShortDateString()%>"  ></asp:comparevalidator><br>	
					            <asp:comparevalidator id="cpvREG_ID_ISSUE"  
					             ControlToValidate="txtREG_ID_ISSUE" Display="Dynamic" Runat="server" ControlToCompare="txtCO_APPL_DOB" Type="Date"
			                     Operator="GreaterThan"></asp:comparevalidator>
                    
                                   
                     
							        </td>
							        <td class="midcolora" width="33%"><asp:Label runat="server" ID="capORIGINAL_ISSUE">Original Issue</asp:Label><%--<span  id="spnORIGINAL_ISSUE"  runat="server" class="mandatory">*</span>--%>
							        <br />
							        <asp:TextBox runat="server" ID="txtORIGINAL_ISSUE" MaxLength="20"></asp:TextBox><br />
							        <%--<asp:RequiredFieldValidator runat="server" ID="rfvORIGINAL_ISSUE"  Display="Dynamic" ErrorMessage="" ControlToValidate="txtORIGINAL_ISSUE"></asp:RequiredFieldValidator> --%>
							        </td>

							        </tr>  							
							       
									
									<!--Position,Date Of Birth ,Marital Status  -->        
									
									<tr >
							        <TD class="midcolora" width="33%" id="ttdCO_APPL_GENDER" runat="server"><asp:label id="capCO_APPL_GENDER" runat="server">Gender</asp:label><%--<span  id="spnGENDER" runat="server" class="mandatory">*</span>--%><br />
										<asp:dropdownlist id="cmbCO_APPL_GENDER" onfocus="SelectComboIndex('cmbCO_APPL_GENDER')" runat="server">
												<asp:ListItem Text="" Value="0"></asp:ListItem>
											</asp:dropdownlist><BR>
											<%--<asp:requiredfieldvalidator id="rfvGENDER" Enabled="true" runat="server" ControlToValidate="cmbCO_APPL_GENDER" Display="Dynamic"
												ErrorMessage="GENDER can't be blank." ></asp:requiredfieldvalidator>--%></TD>
							        <TD class="midcolora" id="ttdCO_APPL_DOB" runat="server" width="34%"> <asp:Label ID="capCREATION_DATE" runat="server" Text=""></asp:Label>
							        <asp:label id="capCO_APPL_DOB"  runat="server"></asp:label><%--<span  id="spnDATE_OF_BIRTH" runat="server" class="mandatory">*</span>--%>
										<br /><asp:textbox id="txtCO_APPL_DOB"  runat="server" maxlength="10" size="12" onblur="FormatDate();CheckCPVMsg();"></asp:textbox>
										<asp:hyperlink id="hlkCO_APPL_DOB" runat="server" CssClass="HotSpot">
												<asp:image id="imgCO_APPL_DOB" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif" ></asp:image>
											</asp:hyperlink><BR>
											<%--<asp:requiredfieldvalidator id="rfvDATE_OF_BIRTH" runat="server" ControlToValidate="txtCO_APPL_DOB" Display="Dynamic"
												ErrorMessage=""></asp:requiredfieldvalidator>--%>
												<%--<asp:requiredfieldvalidator id="rfv2txtCO_APPL_DOB" runat="server" ControlToValidate="txtCO_APPL_DOB" Display="Dynamic"
												ErrorMessage=""></asp:requiredfieldvalidator>--%>
												<asp:regularexpressionvalidator  id="revCO_APPL_DOB"  runat="server" ControlToValidate="txtCO_APPL_DOB" Display="Dynamic"></asp:regularexpressionvalidator>
												<asp:customvalidator id="csvCO_APPL_DOB"  Runat="server" ControlToValidate="txtCO_APPL_DOB" Display="Dynamic"
												ClientValidationFunction="ChkCoapplicantDateOfBirth"></asp:customvalidator>
												 <asp:CustomValidator ID="csvCREATION_DATE" runat="server" 
                                     ControlToValidate="txtCO_APPL_DOB" Display="Dynamic" ClientValidationFunction="ChkCreatedate"></asp:CustomValidator></TD>
							        
												<TD class="midcolora" width="33%" id="ttdCO_APPL_MARITAL_STATUS" runat="server"><asp:label id="capCO_APPL_MARITAL_STATUS" runat="server" >Marital Status</asp:label><%--<span id="spnMARITAL_STATUS" runat="server" class="mandatory">*</span>--%>
										        <br />
										        <asp:dropdownlist  id="cmbCO_APPL_MARITAL_STATUS" onfocus="SelectComboIndex('cmbCO_APPL_MARITAL_STATUS')"	runat="server">
										        </asp:dropdownlist>
										        <BR>
											    <%--<asp:requiredfieldvalidator id="rfvCO_APPL_MARITAL_STATUS" Enabled="true"  runat="server" ControlToValidate="cmbCO_APPL_MARITAL_STATUS" Display="Dynamic" ErrorMessage=""></asp:requiredfieldvalidator>--%></TD>
							        </tr>
                                    <%if (getCarrierSystemID().ToUpper() == "I001")
                                      { %>
							        <tr id="ttrACCOUNT_INFORMATION" runat="server">
							                     <td class="midcolora" width="33%"><asp:label id="capACCOUNT_TYPE" runat="server">ACCOUNT_TYPE</asp:label><%--<span class="mandatory">*</span>--%>
                                                 <br />
                                                 <asp:DropDownList ID="cmbACCOUNT_TYPE" runat="server"></asp:DropDownList>
                                                 <br/>
                                                <%-- <asp:RequiredFieldValidator ID="rfvACCOUNT_TYPE" ControlToValidate="cmbACCOUNT_TYPE" runat="server" Display="dynamic"></asp:RequiredFieldValidator>--%>
                                                </td>
                                                <td class="midcolora" width="33%"><asp:label id="capACCOUNT_NUMBER" runat="server" >ACCOUNT_NUMBER</asp:label>
                                                 <br />
                                                <asp:textbox id="txtACCOUNT_NUMBER" runat="server"  size="20" MaxLength="20" ></asp:textbox></br>
                                                <%--<asp:RegularExpressionValidator ID="revACCOUNT_NUMBER" ControlToValidate="txtACCOUNT_NUMBER" runat="server" Display="Dynamic" ></asp:RegularExpressionValidator>--%>
                                                </td>
                                                <td class="midcolora" width="33%"></td>
							        
							        
                                                <td class="midcolora" width="33%"><asp:label id="capBANK_NAME" runat="server">BANK_NAME</asp:label>
                                                 <br />
                                                <asp:textbox id="txtBANK_NAME" runat="server" MaxLength="50" size="25"></asp:textbox>
                                                </td>
                                                <td class="midcolora" width="33%"><asp:label id="capBANK_BRANCH" runat="server">BANK_BRANCH</asp:label>
                                                <br />
                                                 <asp:textbox id="txtBANK_BRANCH" MaxLength="10" runat="server" ></asp:textbox></BR>
                                                 <%--<asp:RegularExpressionValidator ID=revBANK_BRANCH ControlToValidate="txtBANK_BRANCH" runat="server" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                                                </td>
                                                <td class="midcolora" width="33%"><asp:Label runat="server" ID="capBANK_NUMBER">BANK_NUMBER</asp:Label>
                                                <br />
                                                <asp:TextBox runat="server" ID="txtBANK_NUMBER" MaxLength="5" ></asp:TextBox><br />
                                                <%-- <asp:RegularExpressionValidator ID="revBANK_NUMBER" runat="server" 
                                                 ControlToValidate="txtBANK_NUMBER" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                                                </td>
                                    </tr>
							        <%} %>
							       <tr id="trPOSITION">
							      	
												<td class="midcolora"  colspan="3" width="33%"><asp:label id="capPOSITION" runat="server">Position</asp:label><span class="mandatory">*</span>
							        <br />
							        <asp:DropDownList ID="cmbPOSITION" runat="server" onfocus="SelectComboIndex('cmbPOSITION')"  onchange="setPOSITION()"  Width="400px">									

									</asp:DropDownList><a class="calcolora" href="javascript:showPageLookupLayer('cmbPOSITION')">
                    <img height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></a><br />
									<%--<asp:RequiredFieldValidator runat="server" ID="rfvcmbPOSITION" ControlToValidate="cmbPOSITION" Display="Dynamic" ErrorMessage=""></asp:RequiredFieldValidator>--%>
							        </td> </tr>
							        
							          <tr id="ttrREMARKS" runat="server">
							      <td class="midcolora" colspan="3" ><asp:Label runat="server" ID="capNOTE">Remarks</asp:Label>
							        <br />
							        
							        <asp:TextBox runat="server" 
                                                ID="txtNOTE" AutoCompleteType="None"  onkeypress="MaxLength(this,500)" onpaste="MaxLength(this,500)" Rows="3" TextMode="MultiLine" 
                                                Width="500"></asp:TextBox><br />
                    <asp:CustomValidator runat="server" ID="csvNOTE" Display="Dynamic" ControlToValidate="txtNOTE" ClientValidationFunction="CountLength" ></asp:CustomValidator>
							        </td>
							      </tr>
									
									<!-- Start Visible False  Rows-->
									
									<tr  style="display:none">
										
										<TD class="midcolora" width="34%"><asp:label id="capCO_APPL_SSN_NO" Visible="false" runat="server">Social Security Number</asp:label>
										<br /><asp:label id="capCO_APPL_SSN_NO_HID" Visible="false" runat="server" maxlength="11" size="14"></asp:label>
										<input class="clsButton" id="btnCO_APPL_SSN_NO" text="Edit" onclick="SSN_change();" type="button"></input>
										<asp:textbox id="txtCO_APPL_SSN_NO" runat="server" maxlength="11" size="14"></asp:textbox><br>
											<asp:regularexpressionvalidator id="revCO_APPL_SSN_NO" runat="server" Visible="false" Enabled="false" ControlToValidate="txtCO_APPL_SSN_NO" Display="Dynamic"></asp:regularexpressionvalidator></TD>
									
									<tr style="display:none">
									
									<td class="midcolora" width="33%"><asp:label id="capCO_APPLI_EMPL_PHONE" Visible="false" runat="server"></asp:label>
									<br /><asp:textbox id="txtCO_APPLI_EMPL_PHONE" Visible="false" runat="server" maxlength="13" size="16"></asp:textbox><br>
											<asp:regularexpressionvalidator id="revCO_APPLI_EMPL_PHONE" Enabled="false" Runat="server" ControlToValidate="txtCO_APPLI_BUSINESS_PHONE"
												Display="Dynamic"></asp:regularexpressionvalidator></td>
									
									<td class="midcolora" width="34%"><asp:Label runat="server" ID="capID_TYPE">ID Type</asp:Label>
									<br />
									<asp:DropDownList ID="cmbID_TYPE" runat="server"><asp:ListItem Value="0" Text=""></asp:ListItem> 
									<asp:ListItem Value="1" Text="CPF"></asp:ListItem> </asp:DropDownList>
									<asp:TextBox runat="server" ID="txtID_TYPE" ></asp:TextBox>					
									
									</td>
									<td class="midcolora" width="33%"></td>
									</tr>
									<tr style="display:none">
										

												<TD class="midcolora" width="33%"></TD>
									</tr>
									<tr id="ttrCO_APPL_RELATIONSHIP" runat="server" style="display:none">
									<%--	Added by Neeraj Singh on 08 Nov 06 for Gen Iss #9--%>
										<TD id="ttdCO_APPL_RELATIONSHIP" runat="server" class="midcolora" width="33%"><asp:label id="capCO_APPL_RELATIONSHIP" runat="server">Relationship to Insured</asp:label>
										<br /><asp:dropdownlist id="cmbCO_APPL_RELATIONSHIP" onfocus="SelectComboIndex('cmbCO_APPL_RELATIONSHIP')"
												runat="server"></asp:dropdownlist>
											<A class="calcolora" href="javascript:showPageLookupLayer('cmbCO_APPL_RELATIONSHIP')">
												<IMG height="16" src="../../cmsweb/images/selecticon.gif" width="17" border="0" id="imgRelation"
													runat="server" style="display:none"></A><BR>
											<input type="hidden" id="hidCO_APPL_RELATIONSHIP" runat="server" NAME="hidCO_APPL_RELATIONSHIP">
										</TD>
									
												<TD class="midcolora" width="33%"></TD></tr>
									<tr id="ttrpull" runat="server" style="display:none"> 
										<td class="midcolora"><asp:label id="lblPullCustomerAddress" runat="server">Would you like to pull customer address</asp:label>
										<cmsb:cmsbutton class="clsButton" id="btnPullCustomerAddress" OnClientClick="return false;" runat="server" CausesValidation="false"
												Text="Pull Customer Address"></cmsb:cmsbutton></td>
									</tr>
									<tr style="display:none">
										<TD class="midcolora" width="33%">
										<asp:Label runat="server" ID="capID_TYPE_NUMBER" Visible="false">Number</asp:Label><br />
									<asp:TextBox runat="server" id="txtID_TYPE_NUMBER" Visible="false"></asp:TextBox>
										</TD>
										
										<TD class="midcolora" width="34%">										
									
										<asp:label id="capCOMPLIMENT" runat="server" Visible="false">Address2</asp:label><br />
										
										
										</TD>
										<TD class="midcolora" width="33%"></TD>		
									</tr>
									
																		
									<!-- phone number and ext. removed -->
									<TR style="display:none">
										<TD class="headerEffectSystemParams" width="18%" colSpan="3">Employer Details</TD>
									</TR>
									<tr style="display:none">
										<TD class="midcolora" width="33%"><asp:label id="capCO_APPLI_OCCUPATION" runat="server">Co-Applicant's Occupation</asp:label><span id="spnCO_APPLI_OCCUPATION" class="mandatory">*</span><br />
										
										<br /><asp:dropdownlist id="cmbCO_APPLI_OCCU" Enabled="false" onfocus="SelectComboIndex('cmbCO_APPLI_OCCU');" runat="server"></asp:dropdownlist>
											<A class="calcolora" href="javascript:showPageLookupLayer('cmbCO_APPLI_OCCU')"><IMG height="16" src="../../cmsweb/images/selecticon.gif" width="17" border="0" id="imgOccup"
													runat="server"></A><br>
											<asp:requiredfieldvalidator id="rfvCO_APPLI_OCCU" Runat="server" Enabled="false" ControlToValidate="cmbCO_APPLI_OCCU" Display="Dynamic"></asp:requiredfieldvalidator></TD>
										<TD class="midcolora" width="34%"><asp:label id="capDESC_CO_APPLI_OCCU" runat="server">Description</asp:label><span class="mandatory" id="spnDESC_CO_APPLI_OCCU">*</span><br />
										
										<asp:textbox id="txtDESC_CO_APPLI_OCCU" runat="server" maxlength="200" size="30"></asp:textbox><asp:label id="lblDESC_CO_APPLI_OCCU" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
											<asp:requiredfieldvalidator id="rfvDESC_CO_APPLI_OCCU" Enabled="false" runat="server" ControlToValidate="txtDESC_CO_APPLI_OCCU"
												Display="Dynamic"></asp:requiredfieldvalidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capCO_APPLI_EMPL_NAME" runat="server">Co-Applicant's Employer's Name</asp:label><br />
										<asp:textbox id="txtCO_APPLI_EMPL_NAME" runat="server" maxlength="150" size="30"></asp:textbox></TD>	
									</tr>
	
									<tr style="display:none">
										<TD class="midcolora" width="33%"><asp:label id="capCO_APPLI_EMPL_ADDRESS" runat="server">Co-Applicant's Employer's Address1</asp:label>
										<br /><asp:textbox id="txtCO_APPLI_EMPL_ADDRESS" runat="server" maxlength="150" size="30"></asp:textbox></TD>
										<TD class="midcolora" width="34%"><asp:label id="capCO_APPLI_EMPL_ADDRESS1" runat="server">Co-Applicant's Employer's Address2</asp:label><br />
										<asp:textbox id="txtCO_APPLI_EMPL_ADDRESS1" runat="server" maxlength="150" size="30"></asp:textbox></TD>
									<TD class="midcolora" width="33%"><asp:label id="capCO_APPLI_EMPL_CITY" runat="server">City</asp:label>
										<br /><asp:textbox id="txtCO_APPLI_EMPL_CITY" runat="server" maxlength="35" size="35"></asp:textbox></TD>
									</tr>
									<!--start:  Swastika Gaur 10th Apr'06 for #2367-->
									<tr style="display:none">
										
										<TD class="midcolora" width="33%"><asp:label id="capCO_APPLI_EMPL_COUNTRY" runat="server">Country</asp:label><br />
										
										<!--Added onchange event by Sibin for Itrack Issue 4843-->
										<asp:dropdownlist id="cmbCO_APPLI_EMPL_COUNTRY" onfocus="SelectComboIndex('cmbCO_APPLI_EMPL_COUNTRY')" runat="server" >
										
										
												<asp:ListItem Value='0'></asp:ListItem>
											</asp:dropdownlist>
											<!-- onchange="javascript:EmpDetails_fillstateFromCountry();"
										-->
											</TD>
									
										<TD class="midcolora" width="18%"><asp:label id="capCO_APPLI_EMPL_STATE" runat="server">State</asp:label>
										
										<!--Added onchange event by Sibin for Itrack Issue 4843-->
										<br /><asp:dropdownlist id="cmbCO_APPLI_EMPL_STATE" onchange="EmpDetails_setStateID();" onfocus="SelectComboIndex('cmbCO_APPLI_EMPL_STATE')" runat="server">
												<asp:ListItem Value='0'></asp:ListItem>
											</asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capCO_APPLI_EMPL_ZIP_CODE" runat="server">Zip Code</asp:label><br />
										<asp:textbox id="txtCO_APPLI_EMPL_ZIP_CODE" runat="server" maxlength="10" size="13" ></asp:textbox>
											<%-- Added by Swarup on 30-mar-2007 --%>
											<asp:hyperlink id="hlkCoZipLookup" runat="server" CssClass="HotSpot">
												<asp:image id="imgCoZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
											</asp:hyperlink><!-- OnBlur="GetCoZipForState();EmpDetails_DisableZipForCanada();" --> 
											<BR>
											<asp:customvalidator id="csvCO_APPLI_EMPL_ZIP_CODE" Runat="server" ClientValidationFunction="ChkCoResult"
												ErrorMessage=" " Display="Dynamic" ControlToValidate="txtCO_APPLI_EMPL_ZIP_CODE" Enabled="false"></asp:customvalidator>
											<asp:regularexpressionvalidator id="revCO_APPLI_EMPL_ZIP_CODE" Runat="server" Enabled="false" ControlToValidate="txtCO_APPLI_EMPL_ZIP_CODE"
												Display="Dynamic"></asp:regularexpressionvalidator>
												<%--onblur="CheckIfPhoneEmpty();"--%>
												</TD>
									</tr>
							
									<tr style="display:none">
										<TD class="midcolora" width="33%"><asp:label id="capCO_APPLI_EMPL_EMAIL" runat="server">Email</asp:label>
										<br /><asp:textbox id="txtCO_APPLI_EMPL_EMAIL" runat="server" maxlength="50" size="35"></asp:textbox><br>
											<asp:regularexpressionvalidator id="revCO_APPLI_EMPL_EMAIL" Runat="server" Enabled="false" ControlToValidate="txtCO_APPLI_EMPL_EMAIL"
												Display="Dynamic"></asp:regularexpressionvalidator></TD>
										
										<TD class="midcolora" width="18%"><asp:label id="capCO_APPL_YEAR_CURR_OCCU" runat="server">Co Applicants Years With Current Occupation</asp:label>
										<br /><asp:textbox id="txtCO_APPL_YEAR_CURR_OCCU" runat="server" maxlength="2" size="3"></asp:textbox><BR>
											<asp:regularexpressionvalidator id="revCO_APPL_YEAR_CURR_OCCU" Enabled="false" runat="server" ControlToValidate="txtCO_APPL_YEAR_CURR_OCCU"
												Display="Dynamic"></asp:regularexpressionvalidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capCO_APPLI_YEARS_WITH_CURR_EMPL" runat="server">Co Applicants Years With Current Employer</asp:label>
										<br /><asp:textbox id="txtCO_APPLI_YEARS_WITH_CURR_EMPL" runat="server" maxlength="2" size="3"></asp:textbox><BR>
											<asp:regularexpressionvalidator id="revCO_APPLI_YEARS_WITH_CURR_EMPL" Enabled="false" runat="server" ControlToValidate="txtCO_APPLI_YEARS_WITH_CURR_EMPL"
												Display="Dynamic"></asp:regularexpressionvalidator></TD>
									</tr>
									<tr style="display:none">
										<td class="midcolora" colSpan="2">
										<cmsb:cmsbutton class="clsButton" id="btnAddNewQuickQuote" runat="server" Text="Add New Quick Quote"></cmsb:cmsbutton></td>
										<br>
										<td class="midcolorr" colSpan="2">
										<asp:label id="lblAllPolicy" runat="server" CssClass="errmsg" Visible="False"></asp:label>
										</td>
										
										</tr>
									<tr id="trRow1" runat="server" >
										<td class="midcolora" colSpan="2">
										<cmsb:cmsbutton class="clsButton" id="btnAddNewApplication" runat="server" Text="Add New Application" ></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnReset" CausesValidation="false" runat="server" Text="Reset"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" CausesValidation="false" runat="server" Text="Activate/Deactivate"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" CausesValidation="false" id="btnbacktocustomerassistent" Width="200" runat="server" Text="Back To Customer Assistent"></cmsb:cmsbutton>
										
										</td>
										<td class="midcolorr" colSpan="1">
										<cmsb:cmsbutton class="clsButton" id="btnMakePrimaryApplicant" Visible="false" runat="server" Text="Make Primary Applicant"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
									</tr>
									<input type="hidden" id="hidCO_APPLI_OCCU" runat="server" NAME="hidCO_APPLI_OCCU">
									<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
									<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
									<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidAPPLICANT_ID" type="hidden" value="0" name="hidAPPLICANT_ID" runat="server">
									<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
									<INPUT id="hidCUSTOMER_NAME" type="hidden" value="0" name="hidCUSTOMER_NAME" runat="server">
									<INPUT id="hidCUSTOMER_CODE" type="hidden" value="0" name="hidCUSTOMER_CODE" runat="server">
									
									<INPUT id="hidCUSTOMER_TYPE" type="hidden" value="0" name="hidCUSTOMER_TYPE" runat="server">
									<INPUT id="hidPRIMARY_APP" type="hidden" value="0" name="hidPRIMARY_APP" runat="server">
									<INPUT id="hidRESET" type="hidden" value="0" name="hidRESET" runat="server"><input id="hidBackToApplication" type="hidden" name="hidBackToApplication" runat="server">
									<input id="hidSTATE_COUNTRY_LIST" type="hidden" name="hidSTATE_COUNTRY_LIST" runat="server"> 
									<input id="hidEmpDetails_STATE_COUNTRY_LIST" type="hidden" name="hidEmpDetails_STATE_COUNTRY_LIST" runat="server"><!--Added by Sibin for Itrack Issue 4843-->
									<input id="hidSTATE_ID" type="hidden" name="hidSTATE_ID" runat="server">
									<input id="hidEmpDetails_STATE_ID" type="hidden" name="hidEmpDetails_STATE_ID" runat="server"> <!--Added by Sibin for Itrack Issue 4843-->
									<input id="hidCO_APPL_SSN_NO" type="hidden" name="hidCO_APPL_SSN_NO" runat="server">
									<input id="hidSTATE_ID_OLD" type="hidden" name="hidSTATE_ID_OLD" runat="server"> 
									<input id="hidEMPL_STATE_ID_OLD" type="hidden" name="hidEMPL_STATE_ID_OLD" runat="server">
									<input id="hidAPPL_ID" type="hidden" name="hidAPPL_ID" runat="server">  
									<input id="hidZipeCodeVerificationMsg" type="hidden" name="hidZipeCodeVerificationMsg"  runat="server">  
									<input id="hidPOSITION" type="hidden" name="hidPOSITION" runat="server"> 
                                    <input id="hidAPPLICANT_TYPE" type="hidden" name="hidAPPLICANT_TYPE" runat="server"> 
									
									
									
						      </TBODY>
							</TABLE>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none; Z-INDEX: 101; POSITION: absolute"><iframe id="lookupLayerIFrame" style="DISPLAY: none; Z-INDEX: 1001; FILTER: alpha(opacity=0); BACKGROUND-COLOR: #000000"
				width="0" height="0" left="0px" top="0px;"></iframe>
		</div>
		<DIV id="lookupLayer" onmouseover="javascript:refreshLookupLayer();" style="Z-INDEX: 101; VISIBILITY: hidden; POSITION: absolute">
			<table class="TabRow" cellSpacing="0" cellPadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b><asp:Label runat="server" Text="Add LookUp" ID="Caplook"></asp:Label></b></td>
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
		<script type="text/javascript" language="javascript">
				RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidAPPLICANT_ID').value,true);
				EmpDetails_DisableZipForCanada();
		</script>
		
	</BODY>
</HTML>

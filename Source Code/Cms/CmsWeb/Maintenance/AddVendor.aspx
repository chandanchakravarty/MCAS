<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page validateRequest=false language="c#" Codebehind="AddVendor.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AddVendor" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Add Vendor</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
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
	function AddData()
	{
		document.getElementById('hidVENDOR_ID').value	=	'New';
		document.getElementById('txtVENDOR_CODE').value  = '';
		document.getElementById('txtVENDOR_FNAME').value  = '';
		document.getElementById('txtVENDOR_LNAME').value  = '';
		document.getElementById('txtVENDOR_ADD1').value  = '';
		document.getElementById('txtVENDOR_ADD2').value  = '';
		document.getElementById('txtVENDOR_CITY').value  = '';
		//document.getElementById('cmbVENDOR_COUNTRY').options.selectedIndex = 0;
		document.getElementById('cmbVENDOR_STATE').options.selectedIndex = -1;
		document.getElementById('txtCOMPANY_NAME').value  = '';
		document.getElementById('txtCHK_MAIL_ADD1').value  = '';
		document.getElementById('txtMAIL_1099_ADD1').value  = '';
		document.getElementById('txtCHK_MAIL_ADD2').value  = '';
		document.getElementById('txtMAIL_1099_ADD2').value  = '';
		document.getElementById('txtCHK_MAIL_CITY').value  = '';
		document.getElementById('txtMAIL_1099_CITY').value  = '';
		document.getElementById('txtCHK_MAIL_ZIP').value  = '';
		document.getElementById('txtMAIL_1099_ZIP').value  = '';
		//document.getElementById('cmbCHKCOUNTRY').options.selectedIndex = 0;
		document.getElementById('cmbCHK_MAIL_STATE').options.selectedIndex = -1;
		//document.getElementById('cmbMAIL_1099_COUNTRY').options.selectedIndex = 0;
		document.getElementById('cmbMAIL_1099_STATE').options.selectedIndex = -1;
		document.getElementById('cmbPROCESS_1099_OPT').options.selectedIndex = -1;
		document.getElementById('cmbW9_FORM').options.selectedIndex = -1;
		document.getElementById('txtVENDOR_ZIP').value  = '';
		document.getElementById('txtVENDOR_PHONE').value  = '';
		document.getElementById('txtVENDOR_EXT').value  = '';
		document.getElementById('txtVENDOR_FAX').value  = '';
		document.getElementById('txtVENDOR_MOBILE').value  = '';
		document.getElementById('txtVENDOR_EMAIL').value  = '';
		document.getElementById('cmbVENDOR_SALUTATION').options.selectedIndex = -1;
		document.getElementById('txtVENDOR_FEDERAL_NUM').value  = '';
		document.getElementById('txtVENDOR_NOTE').value  = '';
		document.getElementById('txtVENDOR_ACC_NUMBER').value  = '';
		if(document.getElementById('btnActivateDeactivate'))
		document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
		if(document.getElementById('btnDelete'))
		document.getElementById('btnDelete').setAttribute('disabled',true);
		DisableValidators();
		ChangeColor();
		EFT_change(0);
		Transit_change(0);
		Federal_change();
		}

		function setTab()
		{
		    if ((document.getElementById('hidFormSaved').value == '1') || (document.getElementById('hidOldData').value != "")) 
		           
		    {
		         var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
		         var tabtitles = TAB_TITLES.split(',');
		            Url = "AttachmentIndex.aspx?EntityType=Vendor&EntityId=" + document.getElementById('hidVENDOR_ID').value + "&";
		            DrawTab(2,top.frames[1],tabtitles[0],Url);
		            var entityTyp = '';
		            if (document.getElementById('txtCOMPANY_NAME') != null)
		                entityTyp += document.getElementById('txtCOMPANY_NAME').value;
		            if (document.getElementById('txtVENDOR_CODE') != null)
		                entityTyp += '~' + document.getElementById('txtVENDOR_CODE').value;
		            Url ="AttachmentIndex.aspx?calledfrom=vendor&EntityType=Vendor&EntityName=" + entityTyp + "&EntityId=" + document.getElementById('hidVENDOR_ID').value + "&";
		            //Url="AttachmentIndex.aspx?calledfrom=mortgage&EntityType=Mortgage&EntityName=" + entityTyp + "&EntityId="+document.getElementById('hidHOLDER_ID').value + "&";
		            DrawTab(2,top.frames[1],tabtitles[0],Url);
		        													
			}
			else
			{							
				RemoveTab(2,top.frames[1]);
			}


}


        //Shikha itrack - 1129
function LocationCountryChanged() {
    
    GlobalError = true;
    var CountryID = document.getElementById('cmbCHKCOUNTRY').options[document.getElementById('cmbCHKCOUNTRY').selectedIndex].value;
    AddVendor.AjaxFillState(CountryID, FillState);
    if (GlobalError) {
        return false;
    }
    else {
        return true;
    }
}

function FillState(Result) {
    //var strXML;
    if (Result.error) {
        var xfaultcode = Result.errorDetail.code;
        var xfaultstring = Result.errorDetail.string;
        var xfaultsoap = Result.errorDetail.raw;
    }
    else {
        var statesList = document.getElementById("cmbCHK_MAIL_STATE");
        statesList.options.length = 0;
        oOption = document.createElement("option");
        oOption.value = "";
        oOption.text = "";
        statesList.add(oOption);
        ds = Result.value;
        if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
            for (var i = 0; i < ds.Tables[0].Rows.length; ++i) {
               
                statesList.options[statesList.options.length] = new Option(ds.Tables[0].Rows[i]["STATE_NAME"], ds.Tables[0].Rows[i]["STATE_ID"]);
            }

        }
        if (statesList.options.length > 0) {
            //statesList.remove(0);
            document.getElementById('hidCHK_MAIL_STATE').value = statesList.options[0].value;
        }
        document.getElementById("cmbCHK_MAIL_STATE").value = document.getElementById("cmbCHK_MAIL_STATE").value;
    }

    return false;
}


function states() {
    if (document.getElementById("cmbCHK_MAIL_STATE").value == "") {
        //document.getElementById('rfvCHK_MAIL_STATE').setAttribute('enabled', true);
    }


}

function LocationMAIL_1099_CountryChanged() {
    GlobalError = true;
    var CountryID = document.getElementById('cmbMAIL_1099_COUNTRY').options[document.getElementById('cmbMAIL_1099_COUNTRY').selectedIndex].value;
    AddVendor.AjaxFillState(CountryID, FillMAIL_1099_State);
    if (GlobalError) {
        return false;
    }
    else {
        return true;
    }
}

function FillMAIL_1099_State(Result) {
    //var strXML;
    if (Result.error) {
        var xfaultcode = Result.errorDetail.code;
        var xfaultstring = Result.errorDetail.string;
        var xfaultsoap = Result.errorDetail.raw;
    }
    else {
        var statesList = document.getElementById("cmbMAIL_1099_STATE");
        statesList.options.length = 0;
        oOption = document.createElement("option");
        oOption.value = "";
        oOption.text = "";
        statesList.add(oOption);
        ds = Result.value;
        if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
            for (var i = 0; i < ds.Tables[0].Rows.length; ++i) {

                statesList.options[statesList.options.length] = new Option(ds.Tables[0].Rows[i]["STATE_NAME"], ds.Tables[0].Rows[i]["STATE_ID"]);
            }

        }
        if (statesList.options.length > 0) {
            statesList.remove(0);
            document.getElementById('hidMAIL_1099_STATE').value = statesList.options[0].value;
        }
        document.getElementById("cmbMAIL_1099_STATE").value = document.getElementById("cmbMAIL_1099_STATE").value;
    }

    return false;
}

function VENDOR_COUNTRYChanged() {
    GlobalError = true;
    var CountryID = document.getElementById('cmbVENDOR_COUNTRY').options[document.getElementById('cmbVENDOR_COUNTRY').selectedIndex].value;
    AddVendor.AjaxFillState(CountryID, FillVENDOR__State);
    if (GlobalError) {
        return false;
    }
    else {
        return true;
    }
}

function FillVENDOR__State(Result) {
    //var strXML;
    if (Result.error) {
        var xfaultcode = Result.errorDetail.code;
        var xfaultstring = Result.errorDetail.string;
        var xfaultsoap = Result.errorDetail.raw;
    }
    else {
        var statesList = document.getElementById("cmbVENDOR_STATE");
        statesList.options.length = 0;
        oOption = document.createElement("option");
        oOption.value = "";
        oOption.text = "";
        statesList.add(oOption);
        ds = Result.value;
        if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
            for (var i = 0; i < ds.Tables[0].Rows.length; ++i) {

                statesList.options[statesList.options.length] = new Option(ds.Tables[0].Rows[i]["STATE_NAME"], ds.Tables[0].Rows[i]["STATE_ID"]);
            }

        }
        if (statesList.options.length > 0) {
            statesList.remove(0);
            document.getElementById('hidVENDOR_STATE').value = statesList.options[0].value;
        }
        document.getElementById("cmbVENDOR_STATE").value = document.getElementById("cmbVENDOR_STATE").value;
    }

    return false;
}
//By Pravesh
   function CopyData(fromFieldID,ToFieldID)
   {
   
		if (document.getElementById('chkCopyData').checked == true)
		{ 
				var str=new String(fromFieldID);
   				if (str.substr(0,3)=='cmb')
				{
					if (document.getElementById(fromFieldID).options.selectedIndex != -1)
					{
					var selectedvalue;
					selectedvalue=document.getElementById(fromFieldID).options[document.getElementById(fromFieldID).options.selectedIndex].value;
					SelectComboOption(ToFieldID,selectedvalue);
					} 
				}
				else
				{
				document.getElementById(ToFieldID).value =  document.getElementById(fromFieldID).value;   
				}   
		}
		else
		{
		document.getElementById(ToFieldID).value = '';
		}
   
   }
//end here

//by Swarup
function CopyAdd(fromFieldID,ToFieldID)
   {
   
		if (document.getElementById('chkCopyAdd').checked == true)
		{ 
				var str=new String(fromFieldID);
   				if (str.substr(0,3)=='cmb')
				{
					if (document.getElementById(fromFieldID).options.selectedIndex != -1)
					{
					var selectedvalue;
					selectedvalue=document.getElementById(fromFieldID).options[document.getElementById(fromFieldID).options.selectedIndex].value;
					SelectComboOption(ToFieldID,selectedvalue);
					} 
				}
				else
				{
				document.getElementById(ToFieldID).value =  document.getElementById(fromFieldID).value;   
				}   
		}
		else
		{
		document.getElementById(ToFieldID).value = '';
		}
   
   }
//end here

	function populateXML()
	{		
		
	
		var tempXML = document.getElementById('hidOldData').value;
		
		if(document.getElementById('txtCOMPANY_NAME').value!="")
				{
					document.getElementById('txtCOMPANY_NAME').focus();
				}
		
		MakeNonMandatoryFields();
		if(document.getElementById('hidFormSaved').value == '0')
		{
			var tempXML;
			//Enabling the activate deactivate button
			
			if(tempXML!="")
			{
				
				if(document.getElementById('btnActivateDeactivate'))
				document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
				if(document.getElementById('btnDelete'))
				document.getElementById('btnDelete').setAttribute('disabled',false);
				populateFormData(tempXML,AddVendor);
				if(document.getElementById('hidACCOUNT_TYPE').value == 101)
					document.getElementById('rdbACC_CASH_ACC_TYPET').checked = true;
				else
					document.getElementById('rdbACC_CASH_ACC_TYPEO').checked = true;	
				if(document.getElementById('hidREVERIFIED_AC').value == 10963)
					document.getElementById('chkREVERIFIED_AC').checked = true;
				else
					document.getElementById('chkREVERIFIED_AC').checked = false;
					//Added By Raghav For Special Handling.				
				if(document.getElementById('hidREQ_SPECIAL_HANDLING').value == 10963)
					document.getElementById('chkREQ_SPECIAL_HANDLING').checked = true;
				else
					document.getElementById('chkREQ_SPECIAL_HANDLING').checked = false;
					
					
				MakeNonMandatoryFields();
				
				EFT_hide();
				Transit_hide();
				Federal_hide();

			//SetActive();
			//setStatusLabel();
			}
			else
			{
				AddData();

            }
          
			
		}
			/*
			if(document.getElementById('hidFormSaved').value == '0')
			{
				
				var tempXML;
				if(top.frames[1].strXML!="")
				{
					//document.getElementById('btnReset').style.display='none';
					tempXML=top.frames[1].strXML;

					//Enabling the activate deactivate button
					document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
					//Storing the XML in hidRowId hidden fields 
					document.getElementById('hidOldData').value		=	 tempXML;
					populateFormData(tempXML,AddVendor);
				}
				else
					AddData();	
			}*/
		setTab();
			//setEncryptFields();
			return false;
		}
	
	////////////////////////	
		function ValidateTranNo(objSource, objArgs)
		{
	
			var tranNum = document.getElementById('txtROUTING_NUMBER').value;
			var firstDigit = tranNum.slice(0,1);
			if(firstDigit == "5")
				objArgs.IsValid = false;
		}
		function VerifyTranNo(objSource, objArgs)
		{
		
			var boolval = ValidateTransitNumber(document.getElementById('txtROUTING_NUMBER'));
		
			if(boolval == false)
			{
				objArgs.IsValid = false;
			}
		}
		function ValidateTranNoLength(objSource, objArgs)
		{
		
			var boolval = ValidateTransitNumberLen(document.getElementById('txtROUTING_NUMBER'));
			
			if(boolval == false)
			{
				objArgs.IsValid = false;
			}
			
		}
	//////////////	
		function ValidateDFIAcct(objSource, objArgs)
		{
			
			var boolval = ValidateDFIAcctNo(document.getElementById('txtBANK_ACCOUNT_NUMBER'));
			if(boolval == false)
			{
				objArgs.IsValid = false;
			}
		}
		////////////
		function EnableDisableEFT(txtDesc,rfvDesc,spnDesc,flag)
			{		
				
				if (flag==false)
				{			
					if(rfvDesc!=null)
					{					
						rfvDesc.setAttribute('enabled',false);	
						rfvDesc.setAttribute('isValid',false);					
						rfvDesc.style.display="none";	
						spnDesc.style.display = "none";	
						txtDesc.className = "";	
					//	txtDesc.value="";		
					}					
				}
				else
				{	
					if(rfvDesc!=null)
					{
						rfvDesc.setAttribute('enabled',true);					
						rfvDesc.setAttribute('isValid',true);
						spnDesc.style.display = "inline";
						txtDesc.className = "MandatoryControl";
					}
				}	
				//alert(spnDesc.id + " style " + spnDesc.style.display);
				ChangeColor();
			}
			function EnableDisableLables()
			{
					
					document.getElementById('lbl_BANK_NAME').style.display="none";
					document.getElementById('lbl_BANK_BRANCH').style.display="none";
					document.getElementById('lbl_BANK_ACCOUNT_NUMBER').style.display="none";
					document.getElementById('lbl_ROUTING_NUMBER').style.display="none";
					document.getElementById('lbl_BANK_ACCOUNT_NUMBER').style.display="none";
					document.getElementById('lbl_ROUTING_NUMBER').style.display="none";
					document.getElementById('lblACC_CASH_ACC_TYPE').style.display="none";
					CheckForEFT();
			}
		
		//Added by Mohit Agarwal 29-Aug 2008	
		function EFT_change(callfrom)
		{
			//document.getElementById('txtBANK_ACCOUNT_NUMBER').value = document.getElementById('txtBANK_ACCOUNT_NUMBER_HID').value;
			//document.getElementById('txtBANK_ACCOUNT_NUMBER').style.display = 'inline';
		    //document.getElementById('txtBANK_ACCOUNT_NUMBER_HID').style.display = 'none';
		    var Cancel = document.getElementById("hidCancel").value;
		    var Edit = document.getElementById("hidEdit").value;			
			document.getElementById('txtBANK_ACCOUNT_NUMBER').value = '';
			document.getElementById('txtBANK_ACCOUNT_NUMBER').style.display = 'inline';
			document.getElementById("rfvBANK_ACCOUNT_NUMBER").setAttribute('enabled',true);
			document.getElementById("revBANK_ACCOUNT_NUMBER").setAttribute('enabled',true);
			document.getElementById("csvBANK_ACCOUNT_NUMBER").setAttribute('enabled',true);
			if(callfrom == '0')
			{
				if(document.getElementById("btnBANK_ACCOUNT_NUMBER").value == 'Edit'||document.getElementById("btnBANK_ACCOUNT_NUMBER").value == 'Editar')
					document.getElementById("btnBANK_ACCOUNT_NUMBER").value = Cancel;
				else if (document.getElementById("btnBANK_ACCOUNT_NUMBER").value == 'Cancel' || document.getElementById("btnBANK_ACCOUNT_NUMBER").value == 'Cancelar')
					EFT_hide();
				else
					document.getElementById("btnBANK_ACCOUNT_NUMBER").style.display = 'none';
			}
					
		}
		
		function EFT_hide() {
		    var Cancel = document.getElementById("hidCancel").value;
		    var Edit = document.getElementById("hidEdit").value;
			document.getElementById("btnBANK_ACCOUNT_NUMBER").style.display = 'inline';
			document.getElementById("capBANK_ACCOUNT_NUMBER_HID").style.display = 'inline';
			document.getElementById('txtBANK_ACCOUNT_NUMBER').value = '';
			document.getElementById('txtBANK_ACCOUNT_NUMBER').style.display = 'none';
			document.getElementById("rfvBANK_ACCOUNT_NUMBER").style.display='none';
			document.getElementById("rfvBANK_ACCOUNT_NUMBER").setAttribute('enabled',false);
			document.getElementById("revBANK_ACCOUNT_NUMBER").style.display='none';
			document.getElementById("revBANK_ACCOUNT_NUMBER").setAttribute('enabled',false);
			document.getElementById("csvBANK_ACCOUNT_NUMBER").style.display='none';
			document.getElementById("csvBANK_ACCOUNT_NUMBER").setAttribute('enabled',false);
			document.getElementById("btnBANK_ACCOUNT_NUMBER").value = Edit;
		}

		
		function Federal_change() {
		    var Cancel = document.getElementById("hidCancel").value;
		    var Edit = document.getElementById("hidEdit").value;
			//document.getElementById('txtVENDOR_FEDERAL_NUM').value = document.getElementById('txtVENDOR_FEDERAL_NUM_HID').value;
			//document.getElementById('txtVENDOR_FEDERAL_NUM').style.display = 'inline';
			//document.getElementById('txtVENDOR_FEDERAL_NUM_HID').style.display = 'none';			
			document.getElementById('txtVENDOR_FEDERAL_NUM').value = '';
			document.getElementById('txtVENDOR_FEDERAL_NUM').style.display = 'inline';
			document.getElementById("rfvVENDOR_FEDERAL_NUM").setAttribute('enabled',true);
			document.getElementById("revVENDOR_FEDERAL_NUM").setAttribute('enabled',true);
			if (document.getElementById("btnVENDOR_FEDERAL_NUM").value == 'Edit' || document.getElementById("btnVENDOR_FEDERAL_NUM").value == 'Editar')
				document.getElementById("btnVENDOR_FEDERAL_NUM").value = Cancel;
			else if (document.getElementById("btnVENDOR_FEDERAL_NUM").value == 'Cancel' || document.getElementById("btnVENDOR_FEDERAL_NUM").value == 'Cancelar')
				Federal_hide();
			else
				document.getElementById("btnVENDOR_FEDERAL_NUM").style.display = 'none';
		}

		function Federal_hide() {
		    var Cancel = document.getElementById("hidCancel").value;
		    var Edit = document.getElementById("hidEdit").value;
			document.getElementById("btnVENDOR_FEDERAL_NUM").style.display = 'inline';
			document.getElementById('txtVENDOR_FEDERAL_NUM').value = '';
			document.getElementById('txtVENDOR_FEDERAL_NUM').style.display = 'none';
			document.getElementById("rfvVENDOR_FEDERAL_NUM").style.display='none';
			document.getElementById("rfvVENDOR_FEDERAL_NUM").setAttribute('enabled',false);
			document.getElementById("revVENDOR_FEDERAL_NUM").style.display='none';
			document.getElementById("revVENDOR_FEDERAL_NUM").setAttribute('enabled',false);
			document.getElementById("btnVENDOR_FEDERAL_NUM").value = Edit;
		}

		function Transit_change(callfrom) {
		    var Cancel = document.getElementById("hidCancel").value;
		    var Edit = document.getElementById("hidEdit").value;
			//document.getElementById('txtROUTING_NUMBER').value = document.getElementById('txtROUTING_NUMBER_HID').value;
			//document.getElementById('txtROUTING_NUMBER').style.display = 'inline';
		    //document.getElementById('txtROUTING_NUMBER_HID').style.display = 'none';
            			
			document.getElementById('txtROUTING_NUMBER').value = '';
			document.getElementById('txtROUTING_NUMBER').style.display = 'inline';
			document.getElementById("rfvROUTING_NUMBER").setAttribute('enabled',true);
			document.getElementById("revROUTING_NUMBER").setAttribute('enabled',true);
			document.getElementById("csvROUTING_NUMBER").setAttribute('enabled',true);
			document.getElementById("csvVERIFY_ROUTING_NUMBER").setAttribute('enabled',true);
			document.getElementById("csvVERIFY_ROUTING_NUMBER_LENGHT").setAttribute('enabled',true);
			if(callfrom == '0')
			{
			    if (document.getElementById("btnROUTING_NUMBER").value == 'Edit' || document.getElementById("btnROUTING_NUMBER").value == 'Editar')
					document.getElementById("btnROUTING_NUMBER").value = Cancel;
			    else if (document.getElementById("btnROUTING_NUMBER").value == 'Cancel'|| document.getElementById("btnROUTING_NUMBER").value == 'Cancelar')
					Transit_hide();
				else
					document.getElementById("btnROUTING_NUMBER").style.display = 'none';
			}
		}

		function Transit_hide() {
		    var Cancel = document.getElementById("hidCancel").value;
		    var Edit = document.getElementById("hidEdit").value;
			document.getElementById("capROUTING_NUMBER_HID").style.display = 'inline';
			document.getElementById("btnROUTING_NUMBER").style.display = 'inline';
			document.getElementById('txtROUTING_NUMBER').value = '';
			document.getElementById('txtROUTING_NUMBER').style.display = 'none';
			document.getElementById("rfvROUTING_NUMBER").style.display='none';
			document.getElementById("rfvROUTING_NUMBER").setAttribute('enabled',false);
			document.getElementById("revROUTING_NUMBER").style.display='none';
			document.getElementById("revROUTING_NUMBER").setAttribute('enabled',false);
			document.getElementById("csvROUTING_NUMBER").style.display='none';
			document.getElementById("csvROUTING_NUMBER").setAttribute('enabled',false);
			document.getElementById("csvVERIFY_ROUTING_NUMBER").style.display='none';
			document.getElementById("csvVERIFY_ROUTING_NUMBER").setAttribute('enabled',false);
			document.getElementById("csvVERIFY_ROUTING_NUMBER_LENGHT").style.display='none';
			document.getElementById("csvVERIFY_ROUTING_NUMBER_LENGHT").setAttribute('enabled',false);
			document.getElementById("btnROUTING_NUMBER").value = Edit;
		}

		function CheckForEFT()
		{
			var SelectedValue;		 
			//if (document.getElementById('cmbALLOWS_EFT').selectedIndex != '0')	
			//{
			
				SelectedValue= document.getElementById('cmbALLOWS_EFT').options[document.getElementById('cmbALLOWS_EFT').selectedIndex].value; 
				if(SelectedValue==10964) // NO
				{
					document.getElementById('txtBANK_NAME').style.display= "none" ;
					document.getElementById('txtBANK_BRANCH').style.display= "none" ;
					document.getElementById('txtBANK_ACCOUNT_NUMBER').style.display= "none" ;
					document.getElementById('btnBANK_ACCOUNT_NUMBER').style.display= "none" ;
					document.getElementById('capBANK_ACCOUNT_NUMBER_HID').style.display= "none" ;
					document.getElementById('txtROUTING_NUMBER').style.display= "none" ;
					document.getElementById('btnROUTING_NUMBER').style.display= "none" ;
					document.getElementById('capROUTING_NUMBER_HID').style.display= "none" ;
					document.getElementById('tdACC_CASH_ACC_TYPE').style.display= "none" ;
					document.getElementById('revBANK_ACCOUNT_NUMBER').style.display="none";
					document.getElementById('csvBANK_ACCOUNT_NUMBER').style.display="none";
					document.getElementById('revROUTING_NUMBER').style.display="none";
					document.getElementById('csvROUTING_NUMBER').style.display="none";
					document.getElementById('csvVERIFY_ROUTING_NUMBER').style.display="none";
					document.getElementById('csvVERIFY_ROUTING_NUMBER_LENGHT').style.display="none";
					
					
					EnableDisableEFT(document.getElementById('txtBANK_ACCOUNT_NUMBER'),document.getElementById('rfvBANK_ACCOUNT_NUMBER'),document.getElementById('spnBANK_ACCOUNT_NUMBER'),false);							
					EnableDisableEFT(document.getElementById('txtROUTING_NUMBER'),document.getElementById('rfvROUTING_NUMBER'),document.getElementById('spnROUTING_NUMBER'),false);							
					
					document.getElementById('lbl_BANK_NAME').style.display="inline";
					document.getElementById('lbl_BANK_BRANCH').style.display="inline";
					document.getElementById('lbl_BANK_ACCOUNT_NUMBER').style.display="inline";
					document.getElementById('lbl_ROUTING_NUMBER').style.display="inline";
					document.getElementById('lblACC_CASH_ACC_TYPE').style.display="inline";
					
					document.getElementById('lblACCOUNT_ISVERIFIED').innerHTML="-N.A.-";
					document.getElementById('lblACCOUNT_VERIFIED_DATE').innerHTML="-N.A.-";

					
				}
				else if (SelectedValue==10963) //YES
				{
				
					document.getElementById('lbl_BANK_NAME').style.display="none";
					document.getElementById('lbl_BANK_BRANCH').style.display="none";
					document.getElementById('lbl_BANK_ACCOUNT_NUMBER').style.display="none";
					document.getElementById('lbl_ROUTING_NUMBER').style.display="none";
					document.getElementById('lblACC_CASH_ACC_TYPE').style.display="none";

					
					
					EnableDisableEFT(document.getElementById('txtBANK_ACCOUNT_NUMBER'),document.getElementById('rfvBANK_ACCOUNT_NUMBER'),document.getElementById('spnBANK_ACCOUNT_NUMBER'),true);			
					EnableDisableEFT(document.getElementById('txtROUTING_NUMBER'),document.getElementById('rfvROUTING_NUMBER'),document.getElementById('spnROUTING_NUMBER'),true);			
					
					document.getElementById('txtBANK_NAME').style.display= "inline" ;
					document.getElementById('txtBANK_BRANCH').style.display= "inline" ;
					if(document.getElementById("hidBANK_ACCOUNT_NUMBER") != null && typeof document.getElementById("hidBANK_ACCOUNT_NUMBER").value != 'undefined' && document.getElementById("hidBANK_ACCOUNT_NUMBER").value != '')
					{
						EFT_hide();
					}
					else
					{
						EFT_change(1);
					}					
					if(document.getElementById("hidROUTING_NUMBER") != null && typeof document.getElementById("hidROUTING_NUMBER").value != 'undefined' && document.getElementById("hidROUTING_NUMBER").value != '')
					{
						Transit_hide();
					}
					else
					{
						Transit_change(1);
					}					
					document.getElementById('tdACC_CASH_ACC_TYPE').style.display= "inline" ;
					if(document.getElementById('hidVENDOR_ID').value=="new")
						document.getElementById('rdbACC_CASH_ACC_TYPEO').checked = true;
				
				}
				if(document.getElementById("hidVENDOR_FEDERAL_NUM") != null && typeof document.getElementById("hidVENDOR_FEDERAL_NUM").value != 'undefined' && document.getElementById("hidVENDOR_FEDERAL_NUM").value != '')
				{
					Federal_hide();
				}
				else
				{
					Federal_change();
				}					

			//}

            }


            function FormatZipCode(vr) {


                var vr = new String(vr.toString());
                if (vr != "" && (document.getElementById('cmbCHKCOUNTRY').options[document.getElementById('cmbCHKCOUNTRY').options.selectedIndex].value == '5')) { 
                //|| (document.getElementById('cmbMAIL_1099_COUNTRY').options[document.getElementById('cmbMAIL_1099_COUNTRY').options.selectedIndex].value == '5'))) {

                    vr = vr.replace(/[-]/g, "");
                    num = vr.length;
                    if (num == 8 && (document.getElementById('cmbCHKCOUNTRY').options[document.getElementById('cmbCHKCOUNTRY').options.selectedIndex].value == '5')) {
                    //|| (num == 8 && (document.getElementById('cmbMAIL_1099_COUNTRY').options[document.getElementById('cmbMAIL_1099_COUNTRY').options.selectedIndex].value == '5'))) {
                        vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
                       // document.getElementById('revCHK_MAIL_ZIP').setAttribute('enabled', false);
                        //document.getElementById('revMAIL_1099_ZIP').setAttribute('enabled', false);
                        
                    }


                }

                return vr;
            }


            function FormatZipcodeformat(vr) {


                var vr = new String(vr.toString());
                if (vr != "" && (document.getElementById('cmbMAIL_1099_COUNTRY').options[document.getElementById('cmbMAIL_1099_COUNTRY').options.selectedIndex].value == '5')) {

                    vr = vr.replace(/[-]/g, "");
                    num = vr.length;
                    if (num == 8 && (document.getElementById('cmbMAIL_1099_COUNTRY').options[document.getElementById('cmbMAIL_1099_COUNTRY').options.selectedIndex].value == '5')) {
                        vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
                        //document.getElementById('revCHK_MAIL_ZIP').setAttribute('enabled', false);
                        document.getElementById('revMAIL_1099_ZIP').setAttribute('enabled', false);

                    }


                }

                return vr;
            }


            function formatzip_code(vr) {


                var vr = new String(vr.toString());
                if (vr != "" && (document.getElementById('cmbVENDOR_COUNTRY').options[document.getElementById('cmbVENDOR_COUNTRY').options.selectedIndex].value == '5')) {

                    vr = vr.replace(/[-]/g, "");
                    num = vr.length;
                    if (num == 8 && (document.getElementById('cmbVENDOR_COUNTRY').options[document.getElementById('cmbVENDOR_COUNTRY').options.selectedIndex].value == '5')) {
                        vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
                        //document.getElementById('revCHK_MAIL_ZIP').setAttribute('enabled', false);
                        document.getElementById('revVENDOR_ZIP').setAttribute('enabled', false);

                    }


                }

                return vr;
            }
		
		
		function MakeNonMandatoryFields()
		{ 
			//114245	Other - SSN
		    //11735		Other - Federal ID
		    
		    if (document.getElementById('cmbPROCESS_1099_OPT').selectedIndex!=-1)
			var cmbPROCESSValue   = document.getElementById('cmbPROCESS_1099_OPT').options[document.getElementById('cmbPROCESS_1099_OPT').selectedIndex].value;
			if(cmbPROCESSValue == 114245 || cmbPROCESSValue == 11735)
			{
				EnableDisableEFT(document.getElementById('txtMAIL_1099_NAME'),document.getElementById('rfvMAIL_1099_NAME'),document.getElementById('spnMAIL_1099_NAME'),false);//Done for Itrack Issue 6745 on 24 Nov 09				
				EnableDisableEFT(document.getElementById('txtMAIL_1099_ADD1'),document.getElementById('rfvMAIL_1099_ADD1'),document.getElementById('spnMAIL_1099_ADD1'),true);				
				EnableDisableEFT(document.getElementById('txtMAIL_1099_CITY'),document.getElementById('rfvMAIL_1099_CITY'),document.getElementById('spnMAIL_1099_CITY'),true);				
				EnableDisableEFT(document.getElementById('cmbMAIL_1099_STATE'),document.getElementById('rfvMAIL_1099_STATE'),document.getElementById('spnMAIL_1099_STATE'),true);				
				EnableDisableEFT(document.getElementById('cmbMAIL_1099_COUNTRY'),document.getElementById('rfvMAIL_1099_COUNTRY'),document.getElementById('spnMAIL_1099_COUNTRY'),true);				
				EnableDisableEFT(document.getElementById('txtMAIL_1099_ZIP'),document.getElementById('rfvMAIL_1099_ZIP'),document.getElementById('spnMAIL_1099_ZIP'),true);				
				
			}
			else
			{
				EnableDisableEFT(document.getElementById('txtMAIL_1099_NAME'),document.getElementById('rfvMAIL_1099_NAME'),document.getElementById('spnMAIL_1099_NAME'),false);				
				EnableDisableEFT(document.getElementById('txtMAIL_1099_ADD1'),document.getElementById('rfvMAIL_1099_ADD1'),document.getElementById('spnMAIL_1099_ADD1'),false);				
				EnableDisableEFT(document.getElementById('txtMAIL_1099_CITY'),document.getElementById('rfvMAIL_1099_CITY'),document.getElementById('spnMAIL_1099_CITY'),false);				
				EnableDisableEFT(document.getElementById('cmbMAIL_1099_STATE'),document.getElementById('rfvMAIL_1099_STATE'),document.getElementById('spnMAIL_1099_STATE'),false);				
				EnableDisableEFT(document.getElementById('cmbMAIL_1099_COUNTRY'),document.getElementById('rfvMAIL_1099_COUNTRY'),document.getElementById('spnMAIL_1099_COUNTRY'),false);				
				EnableDisableEFT(document.getElementById('txtMAIL_1099_ZIP'),document.getElementById('rfvMAIL_1099_ZIP'),document.getElementById('spnMAIL_1099_ZIP'),false);						
			}
		}
		
		function  setEFTFields()
		{
			var cmbEFTValue = document.getElementById('cmbALLOWS_EFT').options[document.getElementById('cmbALLOWS_EFT').selectedIndex].value;
			if(cmbEFTValue == 10964) // NO
			{
				document.getElementById('txtBANK_NAME').value = "";
				document.getElementById('txtBANK_BRANCH').value = "";
				document.getElementById('txtBANK_ACCOUNT_NUMBER').value = "";
				document.getElementById('txtROUTING_NUMBER').value = "";
			}
		}
		function GenerateVendorCode()
		{	
			if (document.getElementById('hidVENDOR_ID').value == "New")
			{
				document.getElementById('txtVENDOR_CODE').value=(GenerateRandomCode(document.getElementById('txtCOMPANY_NAME').value,''));
			}
		}
		
		</script>
		<script language="javascript"  type="text/javascript" >

		    $(document).ready(function() {
		        $("#cmbCHK_MAIL_STATE").change(function() {
		        $("#hidCHK_MAIL_STATE_2").val($("#cmbCHK_MAIL_STATE option:selected").val());
		    });
		            $("#cmbMAIL_1099_STATE").change(function() {
		            $("#hidCHK_MAIL_STATE_3").val($("#cmbMAIL_1099_STATE option:selected").val());
		            });
		            $("#cmbVENDOR_STATE").change(function() {
		            $("#hidCHK_MAIL_STATE_4").val($("#cmbVENDOR_STATE option:selected").val());
		        });
		    });
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" class="bodyBackGround" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();CheckForEFT();EnableDisableLables();">
		<FORM id="MNT_VENDOR_LIST" method="post" runat="server">
			<P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<%--<td class="midcolora" width="18%"><asp:label id="capBUSI_OWNERNAME" Runat="server">Business Owner Name</asp:label></td>
									<TD class="midcolora" width="32%"><asp:textbox id="txtBUSI_OWNERNAME" runat="server" maxlength="65" size="30"></asp:textbox><BR>
									</TD>--%>
									<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_NAME" runat="server">Company Name</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCOMPANY_NAME" runat="server" maxlength="65" size="30" OnBlur = "GenerateVendorCode();"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvCOMPANY_NAME" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtCOMPANY_NAME"></asp:requiredfieldvalidator></TD><%--Company Name can't be blank.--%>
									<TD class="midcolora" width="18%"><asp:label id="capVENDOR_CODE" runat="server">Vendor Code</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtVENDOR_CODE" runat="server" maxlength="6" size="9"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvVENDOR_CODE" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtVENDOR_CODE"></asp:requiredfieldvalidator></TD><%--VENDOR_CODE can't be blank.--%>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capVENDOR_ACC_NUMBER" runat="server">Account Number</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtVENDOR_ACC_NUMBER" runat="server" maxlength="20" size="25" AutoComplete = "Off"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revVENDOR_ACC_NUMBER" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtVENDOR_ACC_NUMBER"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capVENDOR_FEDERAL_NUM" runat="server">Federal ID</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:label id="capVENDOR_FEDERAL_NUM_HID" runat="server" maxlength="9" size="11"></asp:label><input class="clsButton" id="btnVENDOR_FEDERAL_NUM" text="Edit" onclick="Federal_change();" type="button"></input><asp:textbox id="txtVENDOR_FEDERAL_NUM" runat="server" maxlength="9" size="11" AutoComplete = "Off"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvVENDOR_FEDERAL_NUM" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtVENDOR_FEDERAL_NUM"></asp:requiredfieldvalidator><%--VENDOR_FEDERAL_NUM can't be blank.--%>
										<asp:regularexpressionvalidator id="revVENDOR_FEDERAL_NUM" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtVENDOR_FEDERAL_NUM"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<td class="pageHeader" colSpan="4"> <asp:Label ID="capCHKDETIL" runat="server" ></asp:Label></td><%--Check Mailing Details Check Mailing Details--%>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCHK_MAIL_ADD1" runat="server">Check Mailing Address1</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCHK_MAIL_ADD1" runat="server" maxlength="70" size="50"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvCHK_MAIL_ADD1" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtCHK_MAIL_ADD1"></asp:requiredfieldvalidator></TD><%--Please enter address 1--%>
									<TD class="midcolora" width="18%"><asp:label id="capCHK_MAIL_ADD2" runat="server">Check Mailing Address2</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCHK_MAIL_ADD2" runat="server" maxlength="70" size="50"></asp:textbox><BR>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCHK_MAIL_CITY" runat="server">Check Mailing City</asp:label><span id="spnCHK_MAIL_CITY" runat="server" class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCHK_MAIL_CITY" runat="server" maxlength="40" size="30"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvCHK_MAIL_CITY" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtCHK_MAIL_CITY"></asp:requiredfieldvalidator></TD><%--Check Mailing city can't be blank.--%>
									<TD class="midcolora" width="18%"><asp:label id="capCHK_MAIL_STATE" runat="server">Check Mailing State</asp:label><span id="spnCHK_MAIL_STATE" runat="server" class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCHK_MAIL_STATE" onfocus="SelectComboIndex('cmbCHK_MAIL_STATE')"  onblur="states();" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvCHK_MAIL_STATE" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbCHK_MAIL_STATE" InitialValue=""></asp:requiredfieldvalidator></TD><%--Check Mailing state can't be blank--%>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCHKCOUNTRY" runat="server">Check Mailing Country</asp:label><span class="mandatory">*</span></TD>
									<td class="midcolora" width="32%"><asp:dropdownlist id="cmbCHKCOUNTRY" onfocus="SelectComboIndex('cmbCHKCOUNTRY')" runat="server" onblur="states();" onchange="javascript:LocationCountryChanged();">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvCHKCOUNTRY" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbCHKCOUNTRY"></asp:requiredfieldvalidator></td><%--Check Country can't be blank.--%>
									<TD class="midcolora" width="18%"><asp:label id="capCHK_MAIL_ZIP" runat="server">Check Mailing Zip</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCHK_MAIL_ZIP" runat="server" maxlength="8" CausesValidation="true" size="13"   OnBlur="this.value=FormatZipCode(this.value);ValidatorOnChange();" ></asp:textbox>
                                    <asp:hyperlink id="hlkCHK_MAIL_ZIPLookUp" runat="server" CssClass="HotSpot">
											<asp:image id="imgCHK_MAIL_ZIP" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
										</asp:hyperlink><BR>
										<asp:requiredfieldvalidator id="rfvCHK_MAIL_ZIP" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtCHK_MAIL_ZIP"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revCHK_MAIL_ZIP" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtCHK_MAIL_ZIP"></asp:regularexpressionvalidator></TD><%--Check Mailing Zip can't be blank.--%>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPROCESS_1099_OPT" runat="server">1099 Processing Option</asp:label><span id="spnPROCESS_1099_OPT" runat="server" class="mandatory">*</span></TD>
									<td class="midcolora" width="32%"><asp:dropdownlist id="cmbPROCESS_1099_OPT" onfocus="SelectComboIndex('cmbPROCESS_1099_OPT')" runat="server"
											OnChange="MakeNonMandatoryFields();">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvPROCESS_1099_OPT" runat="server" Display="Dynamic" ErrorMessage=" "
											ControlToValidate="cmbPROCESS_1099_OPT"></asp:requiredfieldvalidator></td> <%--Please select 1099 Processing Option.--%>
									<TD class="midcolora" width="18%"><asp:label id="capW9_FORM" runat="server">F9 Form</asp:label></TD>
									<td class="midcolora" width="32%"><asp:dropdownlist id="cmbW9_FORM" onfocus="SelectComboIndex('cmbW9_FORM')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
									</td>
								</tr>
								<tr>
									<td class="pageHeader" colSpan="4">&nbsp; <asp:Label ID="capMAILDETL" runat ="server"></asp:Label>
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="chkCopyData" runat="server" Text="Copy Default Details"></asp:checkbox> <%--1099 Mailing Details--%>
									</td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_NAME" runat="server">Business Owners Name</asp:label><span class="mandatory" id="spnMAIL_1099_NAME">*</span></TD>
									<TD class="midcolora" width="32%" colspan="3"><asp:textbox id="txtMAIL_1099_NAME" runat="server" maxlength="75" size="85"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvMAIL_1099_NAME" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtMAIL_1099_NAME"></asp:requiredfieldvalidator></TD><%--Please enter Business Owners Name--%>
								</tr>
								<TR>
									<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_ADD1" runat="server">1099 Mail Address1</asp:label><span class="mandatory" id="spnMAIL_1099_ADD1">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMAIL_1099_ADD1" runat="server" maxlength="70" size="50"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvMAIL_1099_ADD1" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtMAIL_1099_ADD1"></asp:requiredfieldvalidator></TD><%--Please enter address 1--%>
									<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_ADD2" runat="server">1099 Mail Address2</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMAIL_1099_ADD2" runat="server" maxlength="70" size="50"></asp:textbox><BR>
									</TD>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_CITY" runat="server">1099 Mail City</asp:label><span class="mandatory" id="spnMAIL_1099_CITY">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMAIL_1099_CITY" runat="server" maxlength="40" size="30"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvMAIL_1099_CITY" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtMAIL_1099_CITY"></asp:requiredfieldvalidator></TD><%--1099 Mailing city can't be blank.--%>
									<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_STATE" runat="server">1099 Mail State</asp:label><span class="mandatory" id="spnMAIL_1099_STATE">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbMAIL_1099_STATE" onfocus="SelectComboIndex('cmbMAIL_1099_STATE')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvMAIL_1099_STATE" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbMAIL_1099_STATE"></asp:requiredfieldvalidator></TD><%--1099 Mailing state can't be blank--%>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_COUNTRY" runat="server">1099 Mail Country</asp:label><span class="mandatory" id="spnMAIL_1099_COUNTRY">*</span></TD>
									<td class="midcolora" width="32%"><asp:dropdownlist id="cmbMAIL_1099_COUNTRY" onfocus="SelectComboIndex('cmbMAIL_1099_COUNTRY')" onchange="javascript:LocationMAIL_1099_CountryChanged();" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvMAIL_1099_COUNTRY" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="cmbMAIL_1099_COUNTRY"></asp:requiredfieldvalidator></td><%--1099 mailng Country can't be blank.--%>
									<TD class="midcolora" width="18%"><asp:label id="capMAIL_1099_ZIP" runat="server">1099 Mail Zip</asp:label><span class="mandatory" id="spnMAIL_1099_ZIP">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMAIL_1099_ZIP" runat="server" maxlength="8" size="13" OnBlur="this.value=FormatZipcodeformat(this.value);ValidatorOnChange();"></asp:textbox><asp:hyperlink id="hlkMAIL_1099_ZIPLookUp" runat="server" CssClass="HotSpot">
											<asp:image id="imgMAIL_1099_ZIP" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
										</asp:hyperlink><BR>
										<asp:requiredfieldvalidator id="rfvMAIL_1099_ZIP" runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtMAIL_1099_ZIP"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revMAIL_1099_ZIP" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtMAIL_1099_ZIP"></asp:regularexpressionvalidator></TD>&nbsp;</tr>
								<tr>
									<TD class="pageHeader" colSpan="4" style="HEIGHT: 21px"><%--Please Enter DFI account number--%>
                                        <asp:Label ID="capBANKINFO" runat ="server"></asp:Label></TD><%--EFT/Bank Information--%>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capALLOWS_EFT" runat="server"></asp:label><span class="mandatory" runat="server" id="spnALLOWS_EFT">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbALLOWS_EFT" onfocus="SelectComboIndex('cmbALLOWS_EFT')" runat="server" onchange="CheckForEFT();"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvALLOWS_EFT" ControlToValidate="cmbALLOWS_EFT" Display="Dynamic" ErrorMessage=""
											Runat="server"></asp:requiredfieldvalidator></TD><%--Please select EFT--%>
									<td class="midcolora" colSpan="2"></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capBANK_NAME" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtBANK_NAME" runat="server" size="23"></asp:textbox>
										<asp:label id="lbl_BANK_NAME" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capBANK_BRANCH" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtBANK_BRANCH" runat="server" size="23"></asp:textbox>
										<asp:label id="lbl_BANK_BRANCH" runat="server" CssClass="LabelFont">-N.A.-</asp:label>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capBANK_ACCOUNT_NUMBER" runat="server"></asp:label>
										<span class="mandatory" id="spnBANK_ACCOUNT_NUMBER">*</span></TD>
									<TD class="midcolora" width="32%"><asp:label id="capBANK_ACCOUNT_NUMBER_HID" runat="server" maxlength="17" size="23"></asp:label><input class="clsButton" id="btnBANK_ACCOUNT_NUMBER" text="Edit" onclick="EFT_change(0);" type="button"></input><asp:textbox id="txtBANK_ACCOUNT_NUMBER" runat="server" maxlength="17" size="23"></asp:textbox>
										<asp:label id="lbl_BANK_ACCOUNT_NUMBER" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
										<asp:requiredfieldvalidator id="rfvBANK_ACCOUNT_NUMBER" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtBANK_ACCOUNT_NUMBER" Runat="server"></asp:requiredfieldvalidator><%--Please Enter DFI account number.--%>
										<asp:regularexpressionvalidator id="revBANK_ACCOUNT_NUMBER" runat="server" Display="Dynamic" ControlToValidate="txtBANK_ACCOUNT_NUMBER"
											ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator>
										<asp:customvalidator id="csvBANK_ACCOUNT_NUMBER" Runat="server" ClientValidationFunction="ValidateDFIAcct"
											ErrorMessage="No space allowed in between the numbers." Display="Dynamic" ControlToValidate="txtBANK_ACCOUNT_NUMBER"></asp:customvalidator><BR>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capROUTING_NUMBER" runat="server"></asp:label>
										<span class="mandatory" id="spnROUTING_NUMBER">*</span></TD>
									<TD class="midcolora" width="32%">
										<asp:label id="capROUTING_NUMBER_HID" Runat="server" MaxLength="9"></asp:label><input class="clsButton" id="btnROUTING_NUMBER" text="Edit" onclick="Transit_change(0);" type="button"></input><asp:textbox id="txtROUTING_NUMBER" Runat="server" MaxLength="9"></asp:textbox>
										<asp:label id="lbl_ROUTING_NUMBER" runat="server" CssClass="LabelFont">-N.A.-</asp:label><BR>
										<asp:requiredfieldvalidator id="rfvROUTING_NUMBER" Runat="server" Display="Dynamic" ErrorMessage="Please Enter Transit/Routing number."
											ControlToValidate="txtROUTING_NUMBER"></asp:requiredfieldvalidator>
										<asp:customvalidator id="csvROUTING_NUMBER" Runat="server" ClientValidationFunction="ValidateTranNo"
											ErrorMessage="Number starting with 5 is invalid." Display="Dynamic" ControlToValidate="txtROUTING_NUMBER"></asp:customvalidator>
										<asp:customvalidator id="csvVERIFY_ROUTING_NUMBER" Runat="server" ClientValidationFunction="VerifyTranNo"
											ErrorMessage="Please Verify the 9th Digit (Check digit)." Display="Dynamic" ControlToValidate="txtROUTING_NUMBER"></asp:customvalidator>
										<asp:customvalidator id="csvVERIFY_ROUTING_NUMBER_LENGHT" Runat="server" ClientValidationFunction="ValidateTranNoLength"
											ErrorMessage="Length has to be exactly 8/9 digits." Display="Dynamic" ControlToValidate="txtROUTING_NUMBER"></asp:customvalidator>
										<asp:regularexpressionvalidator id="revROUTING_NUMBER" Runat="server" ErrorMessage="Please Enter Valid Transit Number."
											Display="Dynamic" ControlToValidate="txtROUTING_NUMBER"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capACCOUNT_ISVERIFIED" runat="server">Is Verified</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:label id="lblACCOUNT_ISVERIFIED" runat="server" CssClass="LabelFont"></asp:label></TD>
									<TD class="midcolora" width="18%"><asp:label id="capACCOUNT_VERIFIED_DATE" runat="server">Verified Date</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblACCOUNT_VERIFIED_DATE" runat="server" CssClass="LabelFont"></asp:label></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capREASON" runat="server"></asp:label></TD>
									<TD class="midcolora" width="18%"><asp:label id="lblREASON" runat="server" CssClass="LabelFont">-N.A.-</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:label id="capREVERIFIED_AC" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:checkbox id="chkREVERIFIED_AC" runat="server"></asp:checkbox></TD>
									
								</tr>
								<TR>
									<TD class="midcolora">
										<asp:Label id="capACCOUNT_TYPE" runat="server"> Account Type :</asp:Label></TD>
									<TD class="midcolora" id="tdACC_CASH_ACC_TYPE">
										<asp:radiobutton id="rdbACC_CASH_ACC_TYPEO" runat="server" Text="Checking" Checked = "True" GroupName="ACC_CASH_ACC_TYPE"></asp:radiobutton>
										<asp:radiobutton id="rdbACC_CASH_ACC_TYPET" runat="server" Text="Saving" GroupName="ACC_CASH_ACC_TYPE"></asp:radiobutton>
									<TD class="midcolora" width="18%"><asp:label id="lblACC_CASH_ACC_TYPE" runat="server" CssClass="LabelFont">-N.A.-</asp:label></TD>
									<TD class="midcolora" colspan="2"></TD>
								</TR>
								<tr>
									<TD class="pageHeader" colSpan="4"><asp:Label ID="capCONTCTDETL" runat="server" ></asp:Label> 
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="chkCopyAdd" runat="server" Text="Copy Default Details"></asp:checkbox><%--Contact Details--%>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capVENDOR_SALUTATION" runat="server">Salutation</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbVENDOR_SALUTATION" onfocus="SelectComboIndex('cmbVENDOR_SALUTATION')" runat="server">
											<asp:ListItem ></asp:ListItem>
											<asp:ListItem ></asp:ListItem>
											<asp:ListItem></asp:ListItem>
										</asp:dropdownlist><BR>
									</TD>
									<TD class="midcolora" colSpan="2"></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capVENDOR_FNAME" runat="server">First Name</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtVENDOR_FNAME" runat="server" maxlength="65" size="30"></asp:textbox><BR>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capVENDOR_LNAME" runat="server">Last Name</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtVENDOR_LNAME" runat="server" maxlength="15" size="18"></asp:textbox><BR>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capVENDOR_ADD1" runat="server">Address1</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtVENDOR_ADD1" runat="server" maxlength="70" size="50"></asp:textbox><BR>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capVENDOR_ADD2" runat="server">Address2</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtVENDOR_ADD2" runat="server" maxlength="70" size="50"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capVENDOR_CITY" runat="server">City</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtVENDOR_CITY" runat="server" maxlength="40" size="30"></asp:textbox><BR>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capVENDOR_STATE" runat="server">State</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbVENDOR_STATE" onfocus="SelectComboIndex('cmbVENDOR_STATE')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capVENDOR_COUNTRY" runat="server">Country</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbVENDOR_COUNTRY" onfocus="SelectComboIndex('cmbVENDOR_COUNTRY')"  onchange="javascript:VENDOR_COUNTRYChanged();" runat="server" >
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capVENDOR_ZIP" runat="server">Zip</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtVENDOR_ZIP" runat="server" maxlength="8" size="13" OnBlur="this.value=formatzip_code(this.value);ValidatorOnChange();"></asp:textbox>
                                    <asp:hyperlink id="hlkVENDOR_ZIPLookUp" runat="server" CssClass="HotSpot">
											<asp:image id="imgVENDOR_ZIP" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
										</asp:hyperlink><BR>
										<asp:regularexpressionvalidator id="revVENDOR_ZIP" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtVENDOR_ZIP"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capVENDOR_PHONE" runat="server">Phone</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtVENDOR_PHONE" runat="server" maxlength="13" size="16"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revVENDOR_PHONE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtVENDOR_PHONE"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capVENDOR_EXT" runat="server">Ext</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtVENDOR_EXT" runat="server" maxlength="4" size="5" ReadOnly="True"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revVENDOR_EXT" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtVENDOR_EXT"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capVENDOR_FAX" runat="server">Fax</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtVENDOR_FAX" runat="server" onblur="FormatPhone();ValidatorOnChange()" maxlength="13" size="16"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revVENDOR_FAX" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtVENDOR_FAX"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capVENDOR_MOBILE" runat="server">Mobile</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtVENDOR_MOBILE"  onblur="FormatPhone();ValidatorOnChange()" runat="server" maxlength="13" size="16"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revVENDOR_MOBILE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtVENDOR_MOBILE"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
								<td class="midcolora" width="18%"><asp:Label ID="capCPF" runat="server"></asp:Label>
								</td>
								<td class="midcolora" width="32%"><asp:TextBox ID="txtCPF" runat="server"></asp:TextBox>
								</br><asp:RegularExpressionValidator runat="server" ID="revCPF_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtCPF"></asp:RegularExpressionValidator>
								</td>
								<td class="midcolora" width="18%">
                                <asp:Label ID="capDATE_OF_BIRTH" runat="server" ></asp:Label>
                                 </td>
                                <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtDATE_OF_BIRTH" runat="server" size="12"  MaxLength="10"></asp:TextBox>
                                <asp:HyperLink ID="hlkDATE_OF_BIRTH" runat="server" CssClass="HotSpot">
                                <asp:Image runat="server" ID="imgDATE_OF_BIRTH" Border="0" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif" ></asp:Image></asp:HyperLink><br/>
                                
                                <asp:comparevalidator id="cpvDATE_OF_BIRTH" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic" Runat="server" Type="Date"
					             Operator="LessThanEqual"   ValueToCompare="<%# DateTime.Today.ToShortDateString()%>"  ></asp:comparevalidator><br/>
                                 <asp:RegularExpressionValidator  ID="revDATE_OF_BIRTH" runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic"></asp:RegularExpressionValidator>
                                </td>
								</tr>
								
								<tr>
								<td class="midcolora" width="18%"><asp:Label ID="capACTIVITY" runat="server"></asp:Label>
								</td>
								<td class="midcolora" width="32%"><asp:DropDownList ID="cmbACTIVITY" runat="server" Width="400px"></asp:DropDownList>
								</td>
								<td class="midcolora" width="18%"><asp:Label ID="capREGIONAL_ID" runat="server"></asp:Label>
								</td>
								<td class="midcolora" width="32%"><asp:TextBox ID="txtREGIONAL_IDENTIFICATION" runat="server"></asp:TextBox>
								</td>
								</tr>
								
								<tr>
								 <td class="midcolora" width="18%">
                                 <asp:Label runat="server" ID="capREGIONAL_ID_ISSUE_DATE">Regional ID Issue Date</asp:Label></td>
                                 <td class="midcolora" width="32%"><asp:TextBox runat="server" ID="txtREG_ID_ISSUE_DATE" AutoCompleteType="Disabled" CausesValidation="true"></asp:TextBox><asp:HyperLink ID="hlkREG_ID_ISSUE_DATE" runat="server" CssClass="HotSpot">
                                 <asp:Image ID="imgREG_ID_ISSUE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif" ></asp:Image></asp:HyperLink><br/>
                                <asp:RegularExpressionValidator runat="server" ID="revREGIONAL_ID_ISSUE_DATE" Display="Dynamic" ControlToValidate="txtREG_ID_ISSUE_DATE"></asp:RegularExpressionValidator>
                                <asp:comparevalidator id="cpv2REG_ID_ISSUE_DATE_FUTURE" ControlToValidate="txtREG_ID_ISSUE_DATE" Display="Dynamic" Runat="server" Type="Date"
					             Operator="LessThanEqual" ValueToCompare="<%# DateTime.Today.ToShortDateString()%>"  ></asp:comparevalidator><br>	
					            <asp:comparevalidator id="cpvREG_ID_ISSUE_DATE" ControlToValidate="txtREG_ID_ISSUE_DATE" Display="Dynamic" Runat="server" ControlToCompare="txtDATE_OF_BIRTH" Type="Date"
			                     Operator="GreaterThan"></asp:comparevalidator>
                                
                                
                                </td>
								<td class="midcolora" width="18%"><asp:Label ID="capREGIONAL_ID_ISSUE" runat="server"></asp:Label>
								</td>
								<td class="midcolora" width="32%"><asp:TextBox ID="txtREG_ID_ISSUE" runat="server"></asp:TextBox>
								</td>
								</tr>
								<tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capVENDOR_EMAIL" runat="server">Email</asp:label></TD>
									<TD class="midcolora" width="32%" colspan="1"><asp:textbox id="txtVENDOR_EMAIL" runat="server" maxlength="50" size="30"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revVENDOR_EMAIL" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtVENDOR_EMAIL"></asp:regularexpressionvalidator></TD>									
									<!--Added By Raghav For Special Handling-->
									<TD class="midcolora" width="18%"><asp:label id="capREQ_SPECIAL_HANDLING" runat="server"></asp:label></TD>																
									<TD class="midcolora" width="32%"><asp:checkbox id="chkREQ_SPECIAL_HANDLING" runat="server"></asp:checkbox></TD>																											
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capVENDOR_NOTE" runat="server">Note</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox onkeypress="MaxLength(this,250);" id="txtVENDOR_NOTE" runat="server" maxlength="250"
											size="50" Width="262px" Height="50px" TextMode="MultiLine"></asp:textbox><br>
										<asp:customvalidator id="csvVENDOR_NOTE" Runat="server" Display="Dynamic" ControlToValidate="txtVENDOR_NOTE"
											ClientValidationFunction="ChkTextAreaLength250"></asp:customvalidator></TD>
									<TD class="midcolora" colSpan="2"></TD>
								</tr>
								<tr>
								<td width="18%" class="midcolora">
				                    <asp:label id="capSUSEP_NUM" runat="server"></asp:label>
				                </td>
				                <td class="midcolora" width="32%" colspan="3">
				                <asp:TextBox id="txtSUSEP_NUM" runat="server" maxlength="20"></asp:TextBox>
				                </td>
								</tr>
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" CausesValidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"></cmsb:cmsbutton></td>
									<td class="midcolorr" align="right" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
					</TD>
				</TR>
				<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
				<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
				<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
				<INPUT id="hidVENDOR_ID" type="hidden" value="0" name="hidVENDOR_ID" runat="server">
				<INPUT id="hidACCOUNT_TYPE" type="hidden" value="0" name="hidACCOUNT_TYPE" runat="server">
				<INPUT id="hidREVERIFIED_AC" type="hidden" value="0" name="hidREVERIFIED_AC" runat="server">
				<INPUT id="hidVENDOR_FEDERAL_NUM" type="hidden" name="hidVENDOR_FEDERAL_NUM" runat="server">
				<INPUT id="hidBANK_ACCOUNT_NUMBER" type="hidden" name="hidBANK_ACCOUNT_NUMBER" runat="server">
				<INPUT id="hidROUTING_NUMBER" type="hidden" name="hidROUTING_NUMBER" runat="server">
				<input  type="hidden" runat="server" ID="hidTAB_TITLES"  value=""  name="hidTAB_TITLES"/> 
				<!--raghav-->
				<INPUT id="hidREQ_SPECIAL_HANDLING" type="hidden" value="0" name="hidREQ_SPECIAL_HANDLING" runat="server">
				<INPUT  type="hidden" runat="server" ID="hidCHK_MAIL_STATE"  value=""  name="hidCHK_MAIL_STATE"/>
				<INPUT  type="hidden" runat="server" ID="hidMAIL_1099_STATE"  value=""  name="hidMAIL_1099_STATE"/>
				<INPUT  type="hidden" runat="server" ID="hidVENDOR_STATE"  value=""  name="hidVENDOR_STATE"/>
		   	    <INPUT  type="hidden" runat="server" ID="hidCHK_MAIL_STATE_2"  value=""  name="hidCHK_MAIL_STATE_2"/>
		   	     <INPUT  type="hidden" runat="server" ID="hidCHK_MAIL_STATE_3"  value=""  name="hidCHK_MAIL_STATE_3"/>
		   	      <INPUT  type="hidden" runat="server" ID="hidCHK_MAIL_STATE_4"  value=""  name="hidCHK_MAIL_STATE_4"/>
				 <input type="hidden" runat="server" ID="hidCancel" name=""/> 	
                 <input type="hidden" runat="server" ID="hidEdit" name=""/> 	
				
			</TABLE>
			</TD></TR></TBODY></TABLE></FORM>
		<script>
				RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidVENDOR_ID').value,true);
				if (document.getElementById("hidFormSaved").value == "5")
				{
					/*Record deleted*/
					/*Refreshing the grid and coverting the form into add mode*/
					/*Using the javascript*/
					RefreshWebGrid("1","1"); 
					document.getElementById("hidFormSaved").value = "0";
					AddData();
				}
				EnableDisableLables();
		</script>
	</BODY>
</HTML>

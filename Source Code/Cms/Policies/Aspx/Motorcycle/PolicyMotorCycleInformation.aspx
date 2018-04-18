<%@ Page Language="c#" CodeBehind="PolicyMotorCycleInformation.aspx.cs" AutoEventWireup="false"
    Inherits="Cms.Policies.Aspx.Motorcycle.PolicyMotorCycleInformation" ValidateRequest="false" %>

<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<html>
<head>
    <title>APP_MOTORVEHICLES</title>
    <meta name="vs_snapToGrid" content="False">
    <meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
    <meta content='C#' name='CODE_LANGUAGE'>
    <meta content='JavaScript' name='vs_defaultClientScript'>
    <meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
    <script src="/cms/cmsweb/scripts/xmldom.js"></script>
    <script src="/cms/cmsweb/scripts/common.js"></script>
    <script src="/cms/cmsweb/scripts/form.js"></script>
    <script language='javascript'>
        var GlobalError = false;
        var varLOB;
        function AddData() {
            //document.getElementById('hidCustomerID').value	=	'New';
            document.getElementById('txtVEHICLE_YEAR').value = '';
            document.getElementById('txtMAKE').value = '';
            document.getElementById('txtMODEL').value = '';
            document.getElementById('txtVIN').value = '';
            document.getElementById('txtGRG_ADD1').value = '';
            document.getElementById('txtGRG_ADD2').value = '';
            document.getElementById('txtGRG_CITY').value = '';
            document.getElementById('cmbGRG_COUNTRY').options.selectedIndex = -1;
            document.getElementById('cmbGRG_STATE').options.selectedIndex = -1;
            document.getElementById('txtGRG_ZIP').value = '';
            document.getElementById('cmbREGISTERED_STATE').options.selectedIndex = -1;
            document.getElementById('cmbCYCL_REGD_ROAD_USE').options.selectedIndex = -1;
            document.getElementById('txtTERRITORY').value = '';
            document.getElementById('txtAMOUNT').value = '';
            document.getElementById('txtVEHICLE_AGE').value = '';
            document.getElementById('cmbMOTORCYCLE_TYPE').selectedIndex = -1;
            document.getElementById('txtVEHICLE_CC').value = '';
            if (document.getElementById('btnDelete'))
                document.getElementById('btnDelete').setAttribute('disabled', true);
            if (document.getElementById('btnActivateDeactivate')) {
                document.getElementById('btnActivateDeactivate').setAttribute('disabled', true);
            }
            DisableValidators();
            ChangeColor();
            document.getElementById('txtVIN').focus();
        }
        //Rule to set symbol
        function SetSymbolRule() {
            var txtCC;
            //if(document.getElementById("hidStateID").value=="22")
            //{				
            txtCC = document.getElementById("txtVEHICLE_CC").value;
            if (txtCC == "" || txtCC == null)
                return;

            if (txtCC > 0 && txtCC <= 50)
                document.getElementById("txtSYMBOL").value = "1";
            else if (txtCC >= 51 && txtCC <= 125)
                document.getElementById("txtSYMBOL").value = "2";
            else if (txtCC >= 126 && txtCC <= 200)
                document.getElementById("txtSYMBOL").value = "3";
            else if (txtCC >= 201 && txtCC <= 500)
                document.getElementById("txtSYMBOL").value = "4";
            else if (txtCC >= 501 && txtCC <= 950)
                document.getElementById("txtSYMBOL").value = "5";
            else if (txtCC >= 951 && txtCC <= 1050)
                document.getElementById("txtSYMBOL").value = "6";
            else if (txtCC >= 1051 && txtCC <= 1250)
                document.getElementById("txtSYMBOL").value = "7";
            else if (txtCC >= 1251 && txtCC <= 1900)
                document.getElementById("txtSYMBOL").value = "8";
            else if (txtCC >= 1901)
                document.getElementById("txtSYMBOL").value = "9";
            ShowHideCYCL_REGD_ROAD_USE();
            //}
        }
        function cmbCYCL_REGD_ROAD_USE_Change() {
            combo = document.getElementById("cmbCYCL_REGD_ROAD_USE");
            if (combo == null || combo.selectedIndex == -1)
                return;

            if (combo.options[combo.selectedIndex].value == "0") {
                //alert("Not eligible for Motorcycle Program  refer to Homeowners for eligibility");
                alert(document.getElementById("hidCYCL_REGD_ROAD_USE_MSG").value);
            }


        }
        function ShowHideCYCL_REGD_ROAD_USE() {
            combo = document.getElementById("cmbCYCL_REGD_ROAD_USE");
            if (document.getElementById("txtVEHICLE_CC").value != "" && !isNaN(document.getElementById("txtVEHICLE_CC").value) && parseInt(document.getElementById("txtVEHICLE_CC").value) <= 50) {
                //combo.style.display = "inline";
                document.getElementById("trCYCL_REGD_ROAD_USE").style.display = "inline";
                document.getElementById('rfvCYCL_REGD_ROAD_USE').setAttribute("enabled", true); //Added by Sibin on 26 Nov 08 for Itrack Issue 5058
                //combo.selectedIndex = -1;
            }
            else {
                //document.getElementById("trCYCL_REGD_ROAD_USE").style.display = "none";
                //document.getElementById('rfvCYCL_REGD_ROAD_USE').setAttribute("enabled",false);//Added by Sibin on 26 Nov 08 for Itrack Issue 5058
            }

        }
        function GetTerritory() {

            if (document.getElementById('txtGRG_ZIP').value != "") {
                if (isNaN(document.getElementById('txtGRG_ZIP').value))
                    return;
                var strZip = document.getElementById('txtGRG_ZIP').value;
                var intLOB = parseInt(document.getElementById('hidAPP_LOB').value);

            }
        }

        function PutTerritory(Result) {
            if (Result.error) {
                var xfaultcode = Result.errorDetail.code;
                var xfaultstring = Result.errorDetail.string;
                var xfaultsoap = Result.errorDetail.raw;

            }
            else
                document.getElementById("txtTERRITORY").value = Result.value;
        }

        /*Now the Territory will be fetched using web-service called through JavaScript
        function GetTerritory()
        {			
        if(document.getElementById('hidCheckZipSubmit')!=null )
        {
        document.getElementById('hidCheckZipSubmit').value="zip";				
        //APP_MOTORVEHICLES.submit();		
        __doPostBack('mm','');
        }
        }*/

        function OpenLookupProxy(url) {
            var EffectiveDate;
            var MayMonth = 5;

            if (document.getElementById('hidAPP_EFFECTIVE_DATE').value != "") {
                EffectiveDate = new Date(document.getElementById('hidAPP_EFFECTIVE_DATE').value);
            }
            else {
                EffectiveDate = new Date();
            }

            var EffectiveYear = EffectiveDate.getYear();
            var EffectiveMonth = EffectiveDate.getMonth();
            var strManufacturer = "";
            if (document.getElementById('txtMake').value == '')
                strManufacturer = "%[^~]%";
            else
                strManufacturer = document.getElementById('txtMake').value;

            var date = (document.getElementById('hidAPP_EFFECTIVE_DATE').value);

            if (date != '') {
                OpenLookupWithFunction(url, 'Model', 'TypeId', 'txtMODEL', 'hidMotorType', 'MOTORCYCLE_MODEL', 'Model', '@Manufacturer=\'' + strManufacturer + '\';@DATE=\'' + date + '\'', 'FetchData()');
                //OpenLookupWithFunction( url,'Model','TypeId','txtMODEL','hidMotorType','ModelMC2008','Model','@Manufacturer=\''  + strManufacturer + '\'','FetchData()');
            }

            //else
            //{
            //	OpenLookupWithFunction( url,'Model','TypeId','txtMODEL','hidMotorType','ModelMC','Model','@Manufacturer=\''  + strManufacturer + '\'','FetchData()');
            //}
            //OpenLookup(url,'Model','Model','','txtMODEL','ModelMC','ModelMC','@Manufacturer='+strManufacturer);

            //OpenLookup(url,'Model','Model','','txtMODEL','ModelMC','ModelMC','@Manufacturer=');
        }

        //Added(Praveen)
        function OpenLookupMotorMake(url) {
            var date = (document.getElementById('hidAPP_EFFECTIVE_DATE').value);
            if (date != '')
                OpenLookup(url, 'Manufacturer', 'Manufacturer', '', 'txtMAKE', 'ManufacturerMC', 'Manufacturer', '@DATE=\'' + date + '\''); document.getElementById('txtMODEL').value = '';
        }

        function populateXML() {


            varLOB = document.getElementById('hidAPP_LOB').value;
            var tempXML;
            //Commented by Sibin for Itrack Issue 5365 on 29 Jan 09
            //document.getElementById('txtVIN').focus();
            // Added by mohit
            if (document.getElementById('btnDelete'))
                document.getElementById('btnDelete').setAttribute('disabled', true);

            if (document.getElementById('hidCheckZipSubmit') != null && document.getElementById('hidCheckZipSubmit').value != "zip") {

                if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1') || (document.getElementById('hidFormSaved').value == '4')) {
                    if (document.getElementById('hidOldData') != null) {

                        tempXML = document.getElementById('hidOldData').value;

                        if (tempXML != "" && tempXML != 0) {
                            //Added by Sibin for Itrack Issue 5365 on 29 Jan 09
                            document.getElementById('txtVIN').focus();
                            if (document.getElementById('btnDelete'))
                                document.getElementById('btnDelete').setAttribute('disabled', false);

                            populateFormData(tempXML, APP_MOTORVEHICLES);
                            document.getElementById('txtAMOUNT').value = formatCurrency(document.getElementById('txtAMOUNT').value);
                            varLOB = document.getElementById('hidAPP_LOB').value;

                        }
                        else {
                            //Added by Sibin for Itrack Issue 5365 on 29 Jan 09
                            document.getElementById('txtVIN').focus();
                            if (document.getElementById('btnDelete')) {
                                document.getElementById('btnDelete').setAttribute('disabled', true);
                            }
                            if (document.getElementById('btnActivateDeactivate')) {
                                document.getElementById('btnActivateDeactivate').setAttribute('disabled', true);
                            }

                            varLOB = document.getElementById('hidAPP_LOB').value;
                        }
                    }
                    else {
                        //Added by Sibin for Itrack Issue 5365 on 29 Jan 09
                        document.getElementById('txtVIN').focus();
                        varLOB = document.getElementById('hidAPP_LOB').value;
                    }
                    //setTab();
                }
                else {
                    // if(document.getElementById('btnDelete'))	
                    //	document.getElementById('btnDelete').setAttribute('disabled',false); 	
                    RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidVehicleID').value);
                    RemoveTab(3, top.frames[1]);
                    RemoveTab(2, top.frames[1]);
                }
                //setTab();
                return false;
            }
            else {
                if (document.getElementById('hidCheckZipSubmit') != null) {
                    document.getElementById('hidCheckZipSubmit').value = "";
                    return false;
                }
            }
        }

        function MoveFocus() {
            /*if(document.getElementById('txtMAKE').value==null || document.getElementById('txtMAKE').value=="")
            {
            document.getElementById('txtMODEL').value="";
            }*/
        }

        function FetchData() {

            for (i = 0; i < document.getElementById('cmbMOTORCYCLE_TYPE').options.length; i++) {
                if (document.getElementById('cmbMOTORCYCLE_TYPE').options[i].value == document.getElementById('hidMotorType').value) {
                    document.getElementById('cmbMOTORCYCLE_TYPE').selectedIndex = i;
                    return;
                }
            }
            //document.getElementById('hidCheckMakeSubmit').value="1";						
            //__doPostBack('mm','');
        }

        function setTab() {//debugger;
            if (document.getElementById('hidOldData').value != '') {
                if (document.getElementById('hidVehicleID') != null && document.getElementById('hidVehicleID').value != "NEW") {
                    var CalledFrom = '';
                    if (document.getElementById('hidCalledFrom') != null) {
                        CalledFrom = document.getElementById('hidCalledFrom').value;
                    }
                    var CustomerID = '';
                    if (document.getElementById('hidCustomerID') != null) {
                        CustomerID = document.getElementById('hidCustomerID').value;
                    }
                    var AppID = '';
                    if (document.getElementById('hidAPPID') != null) {
                        AppID = document.getElementById('hidAPPID').value;
                    }
                    var AppVersionID = '';
                    if (document.getElementById('hidAppVersionID') != null) {
                        AppVersionID = document.getElementById('hidAppVersionID').value;
                    }
                    var VehicleID = '';
                    if (document.getElementById('hidVehicleID') != null) {
                        VehicleID = document.getElementById('hidVehicleID').value;
                    }

                    if (document.getElementById('hidAPP_LOB').value != null) {
                        LOB_ID = document.getElementById('hidAPP_LOB').value;
                    }
                    //Url="../../application/aspx/AddAppCoverages.aspx?CoverageType=Vehicle&pageTitle=Motorcycle&MaxRows=5&CalledFrom="+CalledFrom+"&CUSTOMER_ID="+CustomerID+"&APP_ID="+AppID+"&APP_VERSION_ID="+AppVersionID+"&VEHICLE_ID="+VehicleID +"&";  
                    //DrawTab(2,top.frames[1],'Cycle Coverage Info',Url); 
                    var TabTitle = "";
                    if (document.getElementById("hidCalledFrom").value == "MOT") {
                        //IF called from mototcyle lob then caption of tab should be motorcycle coverage
                        TabTitle = "Motorcycle Coverages";
                    }
                    else {
                        //else caption of tab should be Vehicle Covg Info
                        TabTitle = "Vehicle Covg Info";
                    }

                    Url = "../../Policies/aspx/PolMiscellaneousEquipmentValuesDetails.aspx?CalledFrom=" + CalledFrom + "&CUSTOMER_ID=" + CustomerID + "&POLICY_ID=" + AppID + "&POLICY_VERSION_ID=" + AppVersionID + "&VEHICLE_ID=" + VehicleID + "&RISK_ID=" + VehicleID + "&";
                    DrawTab(2, top.frames[1], "Miscellaneous Equipment", Url);

                    Url = "../../policies/aspx/PolicyCoverages.aspx?CalledFrom=" + CalledFrom + "&pageTitle=Coverages" + "&VEHICLEID=" + VehicleID + "&LOB_ID=" + LOB_ID + "&";
                    DrawTab(3, top.frames[1], "Vehicle Coverages", Url);

                    Url = "../../policies/aspx/PolicyEndorsement.aspx?CalledFrom=" + CalledFrom + "&pageTitle=Vehicle Coverages" + "&VEHICLEID=" + VehicleID + "&LOB_ID=" + LOB_ID + "&";
                    DrawTab(4, top.frames[1], 'Endorsements', Url);

                    Url = "../Aspx/Automobile/PolicyAdditionalInterestIndex.aspx?CalledFrom=" + CalledFrom + "&CUSTOMER_ID=" + CustomerID + "&APP_ID=" + AppID + "&APP_VERSION_ID=" + AppVersionID + "&VEHICLE_ID=" + VehicleID + "&RISK_ID=" + VehicleID + "&";
                    DrawTab(5, top.frames[1], 'Additional Interest', Url);

                    //Url="AddAutoIdInformation.aspx?CalledFrom="+CalledFrom+"&CUSTOMER_ID="+CustomerID+"&APP_ID="+AppID+"&APP_VERSION_ID="+AppVersionID+"&VEHICLE_ID="+VehicleID +"&"; 
                    //DrawTab(4,top.frames[1],'AutoID Information',Url); 
                }
                else {
                    RemoveTab(5, top.frames[1]);
                    RemoveTab(4, top.frames[1]);
                    RemoveTab(3, top.frames[1]);
                    RemoveTab(2, top.frames[1]);

                }
            }
            else {
                RemoveTab(4, top.frames[1]);
                RemoveTab(3, top.frames[1]);
                RemoveTab(2, top.frames[1]);
                this.parent.strSelectedRecordXML = '';
            }

            return false;
        }


        function GetAgeOfVehicle() {
            var OctMonth = 10;
            var CurrentDate; // changeed By Pravesh on 12 jan 08 Itrack 5092
            if (document.getElementById('hidAPP_EFFECTIVE_DATE').value != "")
                CurrentDate = new Date(document.getElementById('hidAPP_EFFECTIVE_DATE').value);
            else
                CurrentDate = new Date();
            var CurrentYear = CurrentDate.getYear();
            var CurrentMonth = CurrentDate.getMonth() + 1;
            //			if (isNaN(document.getElementById('txtVEHICLE_YEAR').value))
            //			{
            //				document.getElementById('txtVEHICLE_AGE').value='';
            //			}
            //			else
            //			{
            //				var Age = '';
            //				
            //				if (document.getElementById('txtVEHICLE_YEAR').value != '')
            //				{
            //					Age = CurrentYear - document.getElementById('txtVEHICLE_YEAR').value;
            //				}
            //				//alert(Age)
            //				
            //				//Commented by Charles on 6-Jan-09 for Itrack 6899
            //				/* if(Age == -1)
            //					Age ++;		*/
            //				if(Age < 0) //Added by Charles on 6-Jan-2010 for Itrack 6899
            //				{
            //					document.getElementById('lblAge').innerHTML = '';
            //					if(document.getElementById('txtVEHICLE_YEAR').value == '')
            //						document.getElementById('txtVEHICLE_AGE').value='';
            //					else					
            //						document.getElementById('txtVEHICLE_AGE').value='1';
            //					return;
            //				}//Added till here	
            //					
            //				if(CurrentMonth>=OctMonth)	Age ++; // By Pravesh	
            //				if(parseInt(Age)>2)
            //				{
            //					document.getElementById('lblAge').innerHTML = 'Rated as 3 years old';
            //					document.getElementById('txtVEHICLE_AGE').value = Age+1;
            //					//document.getElementById('txtVEHICLE_AGE').style.display = "none";
            //				}
            //				else
            //				{
            //					document.getElementById('lblAge').innerHTML = '';
            //					if(document.getElementById('txtVEHICLE_YEAR').value == '')
            //						document.getElementById('txtVEHICLE_AGE').value='';
            //					else
            //						{	
            //							document.getElementById('txtVEHICLE_AGE').value=Age+1;
            //						}						
            //				}
            //			}			
        }

        function ShowLookUpWindow() {
            var vin = '';
            if (document.getElementById('txtVIN') != null) {
                vin = document.getElementById('txtVIN').value;
            }
            ShowPopup('AddVINNoMotorcycle.aspx?', 'VehicleInformation', 400, 200);

        }
        function ShowCustomerVehicle() {
            var customerid = '';
            var appid = '';
            var appversionid = '';
            if (document.getElementById('hidCustomerID') != null) {
                customerid = document.getElementById('hidCustomerID').value;
            }
            if (document.getElementById('hidAPPID') != null) {
                appid = document.getElementById('hidAPPID').value;
            }
            if (document.getElementById('hidAppVersionID') != null) {
                appversionid = document.getElementById('hidAppVersionID').value;
            }
            ShowPopup('CustomerVehicle.aspx?CUSTOMER_ID=' + customerid + '&APP_ID=' + appid + '&APP_VERSION_ID=' + appversionid, 'VehicleInformation', 950, 400);


        }
        function SetRegisteredState() {
            if (document.getElementById("cmbREGISTERED_STATE").value != null)// || document.getElementById("cmbREGISTERED_STATE").value=='')
            {
                SelectComboOption("cmbREGISTERED_STATE", document.getElementById("cmbGRG_STATE").value);
            }
        }
        function showPageLookupLayer(controlId) {
            var lookupMessage;
            switch (controlId) {
                case "cmbMOTORCYCLE_TYPE":
                    lookupMessage = "CYCTY.";
                    break;
                default:
                    lookupMessage = "Look up code not found";
                    break;

            }
            showLookupLayer(controlId, lookupMessage);
        }

        // Added by Swastika on 2nd Mar'06 for Gen Iss #2355	
        function resetValues() {
            document.APP_MOTORVEHICLES.reset();
        }
        function Validate() {
            var result = GetZipForState();
            Page_ClientValidate();
            Page_IsValid = Page_IsValid && result;
            return Page_IsValid;
        }
        function handleResult(res) {
            if (!res.error) {
                //if (res.value==true) 
                if (res.value != "") {
                    document.getElementById("txtTERRITORY").value = res.value;
                    GlobalError = false;
                }
                else {
                    GlobalError = true;
                }
            }
            else {
                GlobalError = true;
            }
        }

        function GetZipForState_OLD() {
            GlobalError = true;

            if (document.getElementById('txtGRG_ZIP').value != "") {
                //if(isNaN(document.getElementById('txtGRG_ZIP').value))
                //	return;
                var intStateID = parseInt(document.getElementById('cmbGRG_STATE').options[document.getElementById('cmbGRG_STATE').options.selectedIndex].value);
                var strZipID = document.getElementById('txtGRG_ZIP').value;
                var intLOB = parseInt(document.getElementById('hidAPP_LOB').value);
                var intCustomerId = parseInt(document.getElementById('hidCustomerID').value);
                var intPolId = parseInt(document.getElementById('hidPolicyID').value);
                var intPolVersionId = parseInt(document.getElementById('hidPolicyVersionID').value);

                /******************************************************************
                Calling webservice synchronously so that web service calling function
                execution waits for web service response function to finish execution
                GlobalError variable is set and access after "handleResult" function set 
                its proper value based on response from webservice.
				
                Custom Validator is enabled on the basis of GlobalError returns false.
				
                This function is called from blur event of Zip textbox and save button click
                therefore control id is checked to ensure the event 				
                *******************************************************************/
                var co = myTSMain1.createCallOptions();
                //co.funcName = "FetchZipForState";
                co.funcName = "FetchTerritoryForZipStateLob";
                co.async = false;
                co.SOAPHeader = new Object();

                //var oResult = myTSMain1.FetchZip.callService(co,strZipID,intLOB,intStateID);				
                var oResult = myTSMain1.FetchZip.callService(co, strZipID, intLOB, intStateID, intCustomerId, intPolId, intPolVersionId, 'POL');
                handleResult(oResult);
                if (GlobalError) {
                    document.getElementById('csvGRG_ZIP').setAttribute('enabled', true);
                    document.getElementById('csvGRG_ZIP').setAttribute('isValid', true);
                    document.getElementById('csvGRG_ZIP').style.display = 'inline';
                    return false;
                }
                else {
                    document.getElementById('csvGRG_ZIP').setAttribute('enabled', false);
                    document.getElementById('csvGRG_ZIP').setAttribute('isValid', false);
                    document.getElementById('csvGRG_ZIP').style.display = 'none';

                    //if(window.event.srcElement.id == "btnSave")
                    //	document.forms[0].submit();

                    return true;
                }
            }
            return false;
        }

        ////////AJAX ZIP IMPLEMENTATION

        function GetZipForState() {
            GlobalError = true;
            try {
                if (document.getElementById('cmbGRG_STATE').value == 14 || document.getElementById('cmbGRG_STATE').value == 22 || document.getElementById('cmbGRG_STATE').value == 49) {
                    if (document.getElementById('txtGRG_ZIP').value != "") {
                        var intStateID = parseInt(document.getElementById('cmbGRG_STATE').options[document.getElementById('cmbGRG_STATE').options.selectedIndex].value);
                        var strZipID = document.getElementById('txtGRG_ZIP').value;
                        var intLOB = parseInt(document.getElementById('hidAPP_LOB').value);
                        var intCustomerId = parseInt(document.getElementById('hidCustomerID').value);
                        var intPolId = parseInt(document.getElementById('hidPolicyID').value);
                        var intPolVersionId = parseInt(document.getElementById('hidPolicyVersionID').value);
                        var intvehicleuse = 0;
                        //var intvehicleuse = parseInt(document.getElementById('cmbAPP_USE_VEHICLE_ID').options[document.getElementById('cmbAPP_USE_VEHICLE_ID').options.selectedIndex].value);
                        var result = PolicyMotorCycleInformation.AjaxFetchTerritoryForZipStateLob(strZipID, intLOB, intStateID, intCustomerId, intPolId, intPolVersionId, 'POL', intvehicleuse);
                        return AjaxCallFunction_CallBack(result);

                    }
                    return false;
                }
                else
                    return true;

            }
            catch (ex) {
                GlobalError = true;
            }
        }
        function AjaxCallFunction_CallBack(response) {

            if (document.getElementById('cmbGRG_STATE').value == 14 || document.getElementById('cmbGRG_STATE').value == 22 || document.getElementById('cmbGRG_STATE').value == 49) {
                if (document.getElementById('txtGRG_ZIP').value != "") {
                    handleResult(response);
                    if (GlobalError) {
                        document.getElementById('csvGRG_ZIP').setAttribute('enabled', true);
                        document.getElementById('csvGRG_ZIP').setAttribute('isValid', true);
                        document.getElementById('csvGRG_ZIP').style.display = 'inline';
                        return false;

                    }
                    else {
                        document.getElementById('csvGRG_ZIP').setAttribute('enabled', false);
                        document.getElementById('csvGRG_ZIP').setAttribute('isValid', false);
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
        function ChkResult(objSource, objArgs) {
            if (GlobalError == true) {
                Page_IsValid = false;
                objArgs.IsValid = false;
            }
            else {
                objArgs.IsValid = true;
            }
            //setInterval("GetTerritory()", 1000);
            //document.getElementById("btnSave").click();
        }
        //Done for Itrack Issue 5888 on 25 May 2009
        function setUpperCase() {
            if (document.getElementById('txtVIN').value != '' && document.getElementById('txtVIN').value != null) {
                var vinNum = document.getElementById('txtVIN').value;
                vinNum = vinNum.toUpperCase();
                document.getElementById('txtVIN').value = vinNum;
            }
        }

        function setValues() {
            //debugger   

            if (document.getElementById('cmbMAKE').value == "") {
                document.getElementById('cmbMODEL').innerHTML = "";
                document.getElementById('cmbMOTORCYCLE_TYPE').innerHTML = "";

            }
            if (document.getElementById('cmbMAKE').value != document.getElementById('hidMakeCode').value) {
                GetValues(document.getElementById('cmbMAKE').value);
            }
            else {
                if (document.getElementById('cmbMODEL').value == "" || document.getElementById('cmbMOTORCYCLE_TYPE').value == "") {
                    GetValues(document.getElementById('cmbMAKE').value);
                }
                else {
                    SetModel();
                    SetModelType();
                }

            }

        }

        function GetValues(MakeID) {
            document.getElementById('hidMakeCode').value = document.getElementById('cmbMAKE').value
            if (MakeID != "" && MakeID != "0") {
                var result = PolicyMotorCycleInformation.AjaxFetchVehicleModelType(MakeID);

                //fillDTCombo(result.value, document.getElementById('cmbPOLICY_SUBLOB'), 'SUB_LOB_ID', 'SUB_LOB_DESC', 0);
                fillDTCombo(result.value, document.getElementById('cmbMODEL'), 'ID', 'MODEL', 0);
                fillDTCombo(result.value, document.getElementById('cmbMOTORCYCLE_TYPE'), 'ID', 'MODEL_TYPE', 1);
            }
        }

        function fillDTCombo(objDT, combo, valID, txtDesc, tabIndex) {
            combo.innerHTML = "";
            if (objDT != null) {

                for (i = 0; i < objDT.Tables[tabIndex].Rows.length; i++) {

                    if (i == 0) {
                        oOption = document.createElement("option");
                        oOption.value = "";
                        oOption.text = "";
                        combo.add(oOption);
                    }
                    oOption = document.createElement("option");
                    oOption.value = objDT.Tables[tabIndex].Rows[i][valID];
                    oOption.text = objDT.Tables[tabIndex].Rows[i][txtDesc];
                    combo.add(oOption);
                }
            }
        }

        function SetModel() {
            //debugger;
            if (document.getElementById('cmbMODEL').value != "") {
                document.getElementById('hidMODEL').value = document.getElementById('cmbMODEL').value;
                //alert(document.getElementById('cmbMODEL').value);
                //document.getElementById("rfvMODEL").style.display = "none";
            }
            //////	        else {
            //////	            document.getElementById("rfvMODEL").style.display = "inline";
            //////	        }
        }

        function SetModelType() {
            if (document.getElementById('cmbMOTORCYCLE_TYPE').value != "") {
                document.getElementById('hidMotorType').value = document.getElementById('cmbMOTORCYCLE_TYPE').value;
            }
        }
	
    </script>
</head>
<body leftmargin='0' topmargin='0' onload='populateXML();ApplyColor();ChangeColor();ShowHideCYCL_REGD_ROAD_USE();GetAgeOfVehicle();'>
    <form id='APP_MOTORVEHICLES' method='post' runat='server'>
    <p>
        <uc1:addressverification id="AddressVerification1" runat="server">
        </uc1:addressverification></p>
    <table cellspacing='0' cellpadding='0' width='100%' border='0'>
        <tr>
            <td class="midcolorc" align="right" colspan="4">
                <asp:Label ID="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
            </td>
        </tr>
        <tr id="trBody" runat="server">
            <td>
                <table width='100%' border='0' align='center'>
                    <tr>
                        <td class="pageHeader" colspan="4">
                            <webcontrol:WorkFlow id="myWorkFlow" runat="server">
                            </webcontrol:WorkFlow>
                            Please note that all fields marked with * are mandatory
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolorc" align="right" colspan="4">
                            <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capINSURED_VEH_NUMBER" runat="server">Insured Vehicle Number</asp:Label><%--<span class="mandatory">*</span>--%>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID="txtINSURED_VEH_NUMBER" runat="server" size="20" MaxLength="8" CssClass="midcolora"
                                ReadOnly="true" BorderStyle="None"></asp:TextBox>
                            <%--<BR><asp:requiredfieldvalidator id="rfvINSURED_VEH_NUMBER" runat="server" ControlToValidate="txtINSURED_VEH_NUMBER"
										ErrorMessage="INSURED_VEH_NUMBER can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>--%>
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capRISK_CURRENCY" runat="server">Risk Currency</asp:Label><span id="spnRISK_CURRENCY"
                                runat="server" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <%--<asp:textbox id="txtPAINT_TYPE" tabIndex="5" runat='server' size='20' maxlength='28'></asp:textbox>--%>
                            <asp:DropDownList ID="cmbRISK_CURRENCY" TabIndex="12" OnFocus="SelectComboIndex('cmbRISK_CURRENCY')"
                                runat='server' Visible="true">
                            </asp:DropDownList>
                            <%--<img src="/cms/cmsweb/images/selecticon.gif" id="img1" runat="server"
										style="CURSOR: hand">--%>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvRISK_CURRENCY" runat="server" ControlToValidate="cmbRISK_CURRENCY"
                                ErrorMessage="Currency can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capVIN" runat="server">VIN</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtVIN' onblur="setUpperCase()" TabIndex="1" runat='server' size='25'
                                MaxLength='17'></asp:TextBox><%--Done for Itrack Issue 5888 on 25 May 2009--%>
                            <%--<asp:hyperlink id="lnkVINMASTER" runat="server" CssClass="HotSpot" Visible="False">
										<asp:image id="imgPopupWindow" runat="server" ImageUrl="~/cmsweb/images/calender.gif"></asp:image>
									</asp:hyperlink>--%>
                            <!--<img src="/cms/cmsweb/images/calender.gif" onclick="ShowLookUpWindow();">-->
                        </td>
                        <%--<TD class='midcolora' width='18%'>
									<asp:Label id="capINSURED_VEH_NUMBER" runat="server">Insured Vehicle Number</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtINSURED_VEH_NUMBER"  tabIndex="2" runat="server" size="20" maxlength="8" CssClass="midcolora"
										ReadOnly="true" BorderStyle="None"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvINSURED_VEH_NUMBER" runat="server" ControlToValidate="txtINSURED_VEH_NUMBER"
										ErrorMessage="INSURED_VEH_NUMBER can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>--%>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capCHASIS" runat="server">Chasis Number</asp:Label><span id="spnCHASIS"
                                runat="server" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID="txtCHASIS" TabIndex="2" runat="server" size="20" MaxLength="8"></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvCHASIS" runat="server" ControlToValidate="txtCHASIS"
                                ErrorMessage="CHASIS NUMBER can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capVEHICLE_YEAR" runat="server">Year of Registration</asp:Label><span
                                class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID="txtVEHICLE_YEAR" TabIndex="3" runat='server' size='6' MaxLength='4'></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvVEHICLE_YEAR" runat="server" ControlToValidate="txtVEHICLE_YEAR"
                                ErrorMessage="VEHICLE_YEAR can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="rngVEHICLE_YEAR" Type="Integer" runat="server" ControlToValidate="txtVEHICLE_YEAR"
                                Display="Dynamic" MinimumValue="1900"></asp:RangeValidator>
                        </td>
                        <%--<TD class='midcolora' width='18%'>
									<asp:Label id="capMAKE" runat="server">Make of Vehicle</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtMAKE" runat='server' size='20' onBlur="javascript:document.getElementById('txtMODEL').value='';" tabIndex="4"
										maxlength='28'></asp:textbox>
									<img src="/cms/cmsweb/images/selecticon.gif" id="imgSelectForVehicleMake" runat="server"
										style="CURSOR: hand">
									<BR>
									<asp:requiredfieldvalidator id="rfvMAKE" runat="server" ControlToValidate="txtMAKE" ErrorMessage="MAKE can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>--%>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capREG_NO" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtREG_NO" TabIndex="4" runat="server" size="12" MaxLength="10"
                                Width="150px" Height="18px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capMAKE" runat="server">Make of Vehicle</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <%--<asp:textbox id="txtMAKE" runat='server' size='20' onBlur="javascript:document.getElementById('txtMODEL').value='';" tabIndex="4"
										maxlength='28'></asp:textbox>--%>
                            <asp:DropDownList ID="cmbMAKE" TabIndex="5" onchange="setValues();" onblur="setValues();"
                                OnFocus="SelectComboIndex('cmbMAKE')" runat='server' Visible="true">
                            </asp:DropDownList>
                            <img src="/cms/cmsweb/images/selecticon.gif" id="imgSelectForVehicleMake" runat="server"
                                visible="false" style="cursor: hand">
                            <br>
                            <asp:RequiredFieldValidator ID="rfvMAKE" runat="server" ControlToValidate="cmbMAKE"
                                ErrorMessage="MAKE can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capMODEL" runat="server">Model of Vehicle</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <%--<asp:textbox id="txtMODEL" tabIndex="5" runat='server' size='20' maxlength='28'></asp:textbox>--%>
                            <img src="/cms/cmsweb/images/selecticon.gif" id="imgSelectVehicleModel" runat="server"
                                visible="false" style="cursor: hand">
                            <asp:DropDownList ID="cmbMODEL" TabIndex="6" onchange="SetModel();" onblur="SetModel();"
                                OnFocus="SelectComboIndex('cmbMODEL')" runat='server' Visible="true">
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvMODEL" runat="server" ControlToValidate="cmbMODEL"
                                ErrorMessage="MODEL can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <%--<TD class='midcolora' width='18%'>
									<asp:Label id="capMODEL" runat="server">Model of Vehicle</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtMODEL" tabIndex="5" runat='server' size='20' maxlength='28'></asp:textbox>
									<img src="/cms/cmsweb/images/selecticon.gif" id="imgSelectVehicleModel" runat="server"
										style="CURSOR: hand">
									<BR>
									<asp:requiredfieldvalidator id="rfvMODEL" runat="server" ControlToValidate="txtMODEL" ErrorMessage="MODEL can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>--%>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capMOTORCYCLE_TYPE" runat="server">Motorcycle Type</asp:Label><span
                                class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:DropDownList ID="cmbMOTORCYCLE_TYPE" TabIndex="7" onchange="SetModelType();"
                                onblur="SetModelType();" OnFocus="SelectComboIndex('cmbMOTORCYCLE_TYPE')" runat='server'
                                Visible="true">
                            </asp:DropDownList>
                            <a class="calcolora" href="javascript:showPageLookupLayer('cmbMOTORCYCLE_TYPE')">
                                <%--<img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0">--%></a>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvMOTORCYCLE_TYPE" runat="server" ControlToValidate="cmbMOTORCYCLE_TYPE"
                                ErrorMessage="Select Motorcycle type." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capVEHICLE_CC" runat="server">Cubic Capacity</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID="txtVEHICLE_CC" TabIndex="8" ReadOnly="False" runat='server' size='5'
                                MaxLength='4'></asp:TextBox>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvVEHICLE_CC" runat="server" ControlToValidate="txtVEHICLE_CC"
                                ErrorMessage="CC can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revVEHICLE_CC" runat="server" ControlToValidate="txtVEHICLE_CC"
                                ErrorMessage="RegularExpressionValidator" Enabled="False" Display="Dynamic"></asp:RegularExpressionValidator>
                            <asp:RangeValidator ID="rngVEHICLE_CC" runat="server" Display="Dynamic" ControlToValidate="txtVEHICLE_CC"
                                Type="Integer" MinimumValue="1" MaximumValue="9999"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr id="trClass" runat="server" visible="true">
                        <%--<TD class='midcolora' width='18%'>
									<asp:Label id="capVEHICLE_CC" runat="server">Cubic Capacity</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtVEHICLE_CC" tabIndex="7" ReadOnly="False" runat='server' size='5' maxlength='4'></asp:textbox>
									<BR>
									<asp:requiredfieldvalidator id="rfvVEHICLE_CC" runat="server" ControlToValidate="txtVEHICLE_CC" ErrorMessage="CC can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revVEHICLE_CC" runat="server" ControlToValidate="txtVEHICLE_CC" ErrorMessage="RegularExpressionValidator"
										Enabled="False" Display="Dynamic"></asp:regularexpressionvalidator>
									<asp:rangevalidator id="rngVEHICLE_CC" runat="server" Display="Dynamic" ControlToValidate="txtVEHICLE_CC"
										Type="Integer" MinimumValue="1" MaximumValue="9999"></asp:rangevalidator>
								</TD>--%>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capAPP_VEHICLE_CLASS" runat="server">Class</asp:Label>
                        </td>
                        <td class='midcolora' width='82%' colspan="3">
                            <asp:DropDownList ID="cmbAPP_VEHICLE_CLASS" TabIndex="8" runat="server" OnFocus="SelectComboIndex('cmbAPP_VEHICLE_CLASS')"
                                Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trSymbol" runat="server" visible="true">
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capSYMBOL" runat="server"></asp:Label>
                            <!-- Commented by Charles for Itrack 5895 on 1-Jun-2009 -->
                            <span id="spnSYMBOL" runat="server" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <!--Added ReadOnly="true" for Itrack 5895 on 1-Jun-2009 -->
                            <asp:TextBox ID="txtSYMBOL" TabIndex="9" runat='server' size='5' MaxLength='4'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSYMBOL" runat="server" ControlToValidate="txtSYMBOL"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revSYMBOL" runat="server" ControlToValidate="txtSYMBOL"
                                Display="Dynamic" Enabled="False"></asp:RegularExpressionValidator>
                            <asp:RangeValidator ID="rngSYMBOL" ControlToValidate="txtSYMBOL" runat="server" Display="Dynamic"
                                MinimumValue="1" MaximumValue="9999" Type="Integer"></asp:RangeValidator>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capCOMPRH_ONLY" runat="server">COMPREHENSIVE ONLY</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbCOMPRH_ONLY" TabIndex="10" onfocus="SelectComboIndex('cmbCOMPRH_ONLY')"
                                runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capTRANSMISSION_TYPE" runat="server">Transmission Type</asp:Label><span
                                id="spnTRANSMISSION_TYPE" runat="server" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <%--<asp:textbox id="txtTRANSMISSION_TYPE" runat='server' size='20' onBlur="javascript:document.getElementById('txtMODEL').value='';" tabIndex="4"
										maxlength='28'></asp:textbox>--%>
                            <asp:DropDownList ID="cmbTRANSMISSION_TYPE" TabIndex="9" OnFocus="SelectComboIndex('cmbTRANSMISSION_TYPE')"
                                runat='server' Visible="true">
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvTRANSMISSION_TYPE" runat="server" ControlToValidate="cmbTRANSMISSION_TYPE"
                                ErrorMessage="MAKE can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capTOTAL_PASSENGERS" runat="server">No of Passengers</asp:Label><span
                                id="spnTOTAL_PASSENGERS" runat="server" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID="txtTOTAL_PASSENGERS" TabIndex="10" runat='server' size='20' MaxLength='28'></asp:TextBox>
                            <%--<img src="/cms/cmsweb/images/selecticon.gif" id="img2" runat="server"
										style="CURSOR: hand">--%>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvTOTAL_PASSENGERS" runat="server" ControlToValidate="txtTOTAL_PASSENGERS"
                                ErrorMessage="MODEL can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capFUEL_TYPE" runat="server">Fuel Type</asp:Label><span id="spnFUEL_TYPE"
                                runat="server" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <%--<asp:textbox id="txtFUEL_TYPE" runat='server' size='20' maxlength='28'></asp:textbox>--%>
                            <asp:DropDownList ID="cmbFUEL_TYPE" TabIndex="11" OnFocus="SelectComboIndex('cmbFUEL_TYPE')"
                                runat='server' Visible="true">
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvFUEL_TYPE" runat="server" ControlToValidate="cmbFUEL_TYPE"
                                ErrorMessage="MAKE can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capPAINT_TYPE" runat="server">Paint Type</asp:Label><span id="spnPAINT_TYPE"
                                runat="server" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <%--<asp:textbox id="txtPAINT_TYPE" tabIndex="5" runat='server' size='20' maxlength='28'></asp:textbox>--%>
                            <asp:DropDownList ID="cmbPAINT_TYPE" TabIndex="12" OnFocus="SelectComboIndex('cmbPAINT_TYPE')"
                                runat='server' Visible="true">
                            </asp:DropDownList>
                            <%--<img src="/cms/cmsweb/images/selecticon.gif" id="img1" runat="server"
										style="CURSOR: hand">--%>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvPAINT_TYPE" runat="server" ControlToValidate="cmbPAINT_TYPE"
                                ErrorMessage="MODEL can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="trCYCL_REGD_ROAD_USE" runat="server" visible="false">
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capCYCL_REGD_ROAD_USE" runat="server"></asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbCYCL_REGD_ROAD_USE" TabIndex="11" OnFocus="SelectComboIndex('cmbCYCL_REGD_ROAD_USE')"
                                runat="server" Visible="true">
                            </asp:DropDownList>
                            <br />
                            <!--Added by Sibin on 26 Nov for Itrack Issue 5058-->
                            <asp:RequiredFieldValidator ID="rfvCYCL_REGD_ROAD_USE" runat="server" ControlToValidate="cmbCYCL_REGD_ROAD_USE"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td class="midcolora" colspan="2">
                        </td>
                    </tr>
                    <tr id="trGARAGE" runat="server" visible="false">
                        <td class="headerEffectSystemParams" colspan="4">
                            Garage Location
                        </td>
                    </tr>
                    <tr id="trCUST_ADD" runat="server" visible="false">
                        <td class="midcolora">
                            <asp:Label ID="Label1" runat="server">Would you like to pull customer address</asp:Label>
                        </td>
                        <td colspan="3" class="midcolora">
                            <cmsb:CmsButton class="clsButton" ID="btnPullCustomerAddress" TabIndex="12" runat="server"
                                Text="Pull Customer Address"></cmsb:CmsButton>
                        </td>
                    </tr>
                    <tr id="trGRG_ADDRESS" runat="server" visible="false">
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capGRG_ADD1" runat="server">Address 1</asp:Label><span id="spnGRG_ADD1"
                                runat="server" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtGRG_ADD1' TabIndex="13" runat='server' size='40' MaxLength='70'></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvGRG_ADD1" runat="server" ControlToValidate="txtGRG_ADD1"
                                ErrorMessage="Address can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capGRG_ADD2" runat="server">Address 2</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtGRG_ADD2' TabIndex="14" runat='server' size='40' MaxLength='70'></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trGRG_CITY" runat="server" visible="false">
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capGRG_CITY" runat="server">City</asp:Label><span id="spnGRG_CITY"
                                runat="server" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtGRG_CITY' TabIndex="15" runat='server' size='20' MaxLength='35'></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvGRG_CITY" runat="server" ControlToValidate="txtGRG_CITY"
                                ErrorMessage="City can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capGRG_COUNTRY" runat="server">Country</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:DropDownList ID='cmbGRG_COUNTRY' TabIndex="16" OnFocus="SelectComboIndex('cmbGRG_COUNTRY')"
                                runat='server'>
                            </asp:DropDownList>
                            <br>
                        </td>
                    </tr>
                    <tr id="trGRG_STATE" runat="server" visible="false">
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capGRG_STATE" runat="server">State</asp:Label><span id="spnGRG_STATE"
                                runat="server" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:DropDownList ID='cmbGRG_STATE' TabIndex="17" OnFocus="SelectComboIndex('cmbGRG_STATE')"
                                runat='server' onchange="javascript:SetRegisteredState();">
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvGRG_STATE" runat="server" ControlToValidate="cmbGRG_STATE"
                                ErrorMessage="Please select state." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capGRG_ZIP" runat="server">Zip</asp:Label><span id="spnGRG_ZIP" runat="server"
                                class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtGRG_ZIP' TabIndex="18" runat='server' size='12' MaxLength='10'></asp:TextBox>
                            <%-- Added by Swarup on 05-apr-2007 --%>
                            <asp:HyperLink ID="hlkZipLookup" runat="server" CssClass="HotSpot">
                                <asp:Image ID="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif"
                                    ImageAlign="Bottom"></asp:Image>
                            </asp:HyperLink><br>
                            <asp:RegularExpressionValidator ID="revGRG_ZIP" runat="server" ControlToValidate="txtGRG_ZIP"
                                ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvGRG_ZIP" runat="server" ControlToValidate="txtGRG_ZIP"
                                ErrorMessage="ZIP can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CustomValidator Enabled="False" ID="csvGRG_ZIP" ClientValidationFunction="ChkResult"
                                runat="server" ControlToValidate="txtGRG_ZIP" Display="Dynamic" ErrorMessage="Zip does not belong to the specifed state."></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr id="trGRG_TERRITORY" runat="server" visible="false">
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capREGISTERED_STATE" runat="server">Registered State</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:DropDownList ID='cmbREGISTERED_STATE' TabIndex="19" OnFocus="SelectComboIndex('cmbREGISTERED_STATE')"
                                runat='server'>
                            </asp:DropDownList>
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capTERRITORY" runat="server">Territory</asp:Label><span id="spnTERRITORY"
                                runat="server" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtTERRITORY' runat='server' size='6' MaxLength='4'></asp:TextBox>
                            <!--	<img src="../../cmsweb/images/selecticon.gif" id="imgSelect" runat="server" style="CURSOR: hand">-->
                            <br>
                            <asp:RequiredFieldValidator ID="rfvTERRITORY" runat="server" ControlToValidate="txtTERRITORY"
                                ErrorMessage="TERRITORY can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revTERRITORY" runat="server" ControlToValidate="txtTERRITORY"
                                Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capAMOUNT" runat="server">Amount</asp:Label><span id="spnAMOUNT" runat="server"
                                class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtAMOUNT' TabIndex="20" runat='server' size='18' MaxLength='10'
                                CssClass="INPUTCURRENCY"></asp:TextBox>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvAMOUNT" runat="server" ControlToValidate="txtAMOUNT"
                                ErrorMessage="Cost of Vehicle can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revAMOUNT" runat="server" ControlToValidate="txtAMOUNT"
                                Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capVEHICLE_AGE" runat="server" Visible="false">Age</asp:Label>
                            <asp:Label ID="capVEHICLE_COVERAGE" runat="server">Coverages</asp:Label><span id="spnVEHICLE_COVERAGE"
                                runat="server" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtVEHICLE_AGE' runat='server' size='5' MaxLength='3' CssClass='midcolora'
                                ReadOnly="True" BorderStyle="None" Visible="false"></asp:TextBox><asp:Label ID="lblAge"
                                    runat="server" Visible="false"></asp:Label>
                            <asp:DropDownList ID='cmbVEHICLE_COVERAGE' TabIndex="19" OnFocus="SelectComboIndex('cmbVEHICLE_COVERAGE')"
                                runat='server'>
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvVEHICLE_COVERAGE" runat="server" ControlToValidate="cmbVEHICLE_COVERAGE"
                                ErrorMessage="Please select Coverage type." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class='midcolora' colspan='2' align="left">
                            <cmsb:CmsButton class="clsButton" ID='btnReset' TabIndex="21" CausesValidation="false"
                                runat="server" Text='Reset'></cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnCopy" TabIndex="22" runat="server" CausesValidation="false"
                                Text='Copy' Visible="false"></cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnActivateDeactivate" TabIndex="23" runat="server"
                                Text="Activate/Deactivate" CausesValidation="false"></cmsb:CmsButton>
                        </td>
                        <td class='midcolorr' colspan='2'>
                            <cmsb:CmsButton class="clsButton" ID="btnDelete" runat="server" TabIndex="24" Text='Delete'>
                            </cmsb:CmsButton>&nbsp;
                            <cmsb:CmsButton class="clsButton" ID='btnSave' TabIndex="25" runat="server" Text='Save'>
                            </cmsb:CmsButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
    <input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
    <input id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
    <input id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
    <input id="hidVehicleID" type="hidden" value="0" name="hidVehicleID" runat="server">
    <input id="hidAPPID" type="hidden" value="0" name="hidAPPID" runat="server">
    <input id="hidAppVersionID" type="hidden" value="0" name="hidAppVersionID" runat="server">
    <input id="hidMakeCode" type="hidden" value="0" name="hidMakeCodeCode" runat="server">
    <input id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
    <input id="hidAPP_LOB" type="hidden" value="0" name="hidAPP_LOB" runat="server">
    <input id="hidCheckZipSubmit" type="hidden" name="hidCheckZipSubmit" runat="server">
    <input id="hidCheckMakeSubmit" value="0" type="hidden" name="hidCheckMakeSubmit"
        runat="server">
    <input id="hidPolicyID" type="hidden" value="0" name="hidPolicyID" runat="server">
    <input id="hidPolicyVersionID" type="hidden" value="0" name="hidPolicyVersionID"
        runat="server">
    <input id="hidMotorType" value="0" type="hidden" name="hidMotorType" runat="server">
    <input id="hidAPP_VEHICLE_CLASS" value="0" type="hidden" name="hidAPP_VEHICLE_CLASS"
        runat="server">
    <input id="hidStateID" value="0" type="hidden" name="hidStateID" runat="server">
    <input id="hidCYCL_REGD_ROAD_USE_MSG" type="hidden" name="hidCYCL_REGD_ROAD_USE_MSG"
        runat="server">
    <input id="hidAPP_EFFECTIVE_DATE" type="hidden" name="hidAPP_EFFECTIVE_DATE" runat="server">
    <input id="hidINSURED_VEH_NUMBER" type="hidden" name="hidINSURED_VEH_NUMBER" runat="server">
    <input id="hidMODEL" value="0" type="hidden" name="hidMODEL" runat="server" />
    </form>
    <!-- For lookup layer -->
    <div id="lookupLayerFrame" style="display: none; z-index: 101; position: absolute">
        <iframe id="lookupLayerIFrame" style="display: none; z-index: 1001; filter: alpha(opacity=0);
            background-color: #000000" width="0" height="0" top="0px;" left="0px"></iframe>
    </div>
    <div id="lookupLayer" style="z-index: 101; visibility: hidden; position: absolute"
        onmouseover="javascript:refreshLookupLayer();">
        <table class="TabRow" cellspacing="0" cellpadding="2" width="100%">
            <tr class="SubTabRow">
                <td>
                    <b>Add LookUp</b>
                </td>
                <td>
                    <p align="right">
                        <a href="javascript:void(0)" onclick="javascript:hideLookupLayer();">
                            <img src="/cms/cmsweb/images/cross.gif" border="0" alt="Close" width="16" height="14"></a></p>
                </td>
            </tr>
            <tr class="SubTabRow">
                <td colspan="2">
                    <span id="LookUpMsg"></span>
                </td>
            </tr>
        </table>
    </div>
    <!-- For lookup layer ends here-->
    <script>
        //RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidVehicleID').value);
        if (document.getElementById("hidFormSaved").value == "3") {
            /*Record deleted*/
            /*Refreshing the grid and coverting the form into add mode*/
            /*Using the javascript*/
            RefreshWebGrid("1", "1", false, "1");
            RemoveTab(4, top.frames[1]);
            RemoveTab(3, top.frames[1]);
            RemoveTab(2, top.frames[1]);
            //RefreshWebGrid("1","1"); 
            document.getElementById('hidVehicleID').value = "NEW";
        }
        else if (document.getElementById("hidFormSaved").value == "1") {
            this.parent.strSelectedRecordXML = "-1";
            //Done for Itrack issue 6187 on 3 Aug 09
            //RefreshWebGrid(document.getElementById('hidFormSaved').value,TrimTheString(document.getElementById('hidVehicleID').value),false);
            RefreshWebGrid(document.getElementById('hidFormSaved').value, TrimTheString(document.getElementById('hidVehicleID').value), true);

        }
    </script>
    </SCRIPT>
</body>
</html>

<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>

<%@ Page Language="c#" CodeBehind="AddAgency.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AddAgency"
    ValidateRequest="false" %>

<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<html>
<head>
    <title>MNT_AGENCY_LIST</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet">
    <script src="../../cmsweb/scripts/xmldom.js"></script>
    <script src="../../cmsweb/scripts/common.js"></script>
    <script src="../../cmsweb/scripts/form.js"></script>
    <script src="/cms/cmsweb/scripts/Calendar.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script>
    <script language="javascript">

        var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
        var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
        var jsaAppDtFormat = "<%=aAppDtFormat  %>";
        var strSystemId = '<%=GetSystemId().ToUpper()%>';

        function LocationAgencyChanged() {

            GlobalError = true;
            var CountryID = document.getElementById('cmbAGENCY_COUNTRY').options[document.getElementById('cmbAGENCY_COUNTRY').selectedIndex].value;
            AddAgency.AjaxFillState(CountryID, FillState);
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
                var statesList = document.getElementById("cmbAGENCY_STATE");
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
                    document.getElementById('hidAGENCY_STATE').value = statesList.options[0].value;
                }
                document.getElementById("cmbAGENCY_STATE").value = document.getElementById("cmbAGENCY_STATE").value;
            }

            return false;
        }
        function M_AGENCY_COUNTRYChanged() {

            GlobalError = true;
            var CountryID = document.getElementById('cmbM_AGENCY_COUNTRY').options[document.getElementById('cmbM_AGENCY_COUNTRY').selectedIndex].value;
            AddAgency.AjaxFillState(CountryID, FillM_AGENCY_STATE);
            if (GlobalError) {
                return false;
            }
            else {
                return true;
            }
        }
        function FillM_AGENCY_STATE(Result) {
            //var strXML;
            if (Result.error) {
                var xfaultcode = Result.errorDetail.code;
                var xfaultstring = Result.errorDetail.string;
                var xfaultsoap = Result.errorDetail.raw;
            }
            else {
                var statesList = document.getElementById("cmbM_AGENCY_STATE");
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
                    document.getElementById('hidM_AGENCY_STATE').value = statesList.options[0].value;
                }
                document.getElementById("cmbM_AGENCY_STATE").value = document.getElementById("cmbM_AGENCY_STATE").value;
            }

            return false;
        }
        ////////////////////////	
        function ValidateTranNo(objSource, objArgs) {

            var tranNum = document.getElementById('txtROUTING_NUMBER').value;
            var firstDigit = tranNum.slice(0, 1);


            if (firstDigit == "5") {
                if (ValidateRequired)
                    objArgs.IsValid = true;
                else
                    objArgs.IsValid = false;

            }
        }
        function VerifyTranNo(objSource, objArgs) {
//            if (document.getElementById('revROUTING_NUMBER').isvalid == true)  //Added by aditya for tfs # 2688
//                return
            var boolval = ValidateTransitNumber(document.getElementById('txtROUTING_NUMBER'));
            if (ValidateRequired()) {
                boolval = true;
            }

            if (boolval == false) {
                objArgs.IsValid = false;
            }
        }
        function ValidateTranNolength(objSource, objArgs) {
        
            var boolval = ValidateTransitNumberLen(document.getElementById('txtROUTING_NUMBER'));
            if (ValidateRequired) {
                boolval = true;
            }
            if (boolval == false) {
                objArgs.IsValid = false;
            }

        }
        //////////////	
        function ValidateTranNo1(objSource, objArgs) { 

            var tranNum = document.getElementById('txtROUTING_NUMBER1').value;
            var firstDigit = tranNum.slice(0, 1);
            if (firstDigit == "5")
                objArgs.IsValid = false;
        }
        function VerifyTranNo1(objSource, objArgs) { 

            var boolval = ValidateTransitNumber(document.getElementById('txtROUTING_NUMBER1'));

            if (boolval == false) {
                objArgs.IsValid = false;
            }
        }
        function ValidateTranNolength1(objSource, objArgs) {

            var boolval = ValidateTransitNumberLen(document.getElementById('txtROUTING_NUMBER1'));
            if (boolval == false) {
                objArgs.IsValid = false;
            }

        }
        /////////////

        function ValidateDFIAcct(objSource, objArgs) {

            var boolval = ValidateDFIAcctNo(document.getElementById('txtBANK_ACCOUNT_NUMBER'));
            if (boolval == false) {
                objArgs.IsValid = false;
            }
        }

        ////////////
        function ValidateDFIAcct1(objSource, objArgs) {

            var boolval = ValidateDFIAcctNo(document.getElementById('txtBANK_ACCOUNT_NUMBER1'));
            if (boolval == false) {
                objArgs.IsValid = false;
            }
        }
        ///////////
        function AddData() {
            ChangeColor();

            DisableValidators();
            document.getElementById('hidAGENCY_NEW').value = 'New';
            document.getElementById('hidAGENCY_ID').value = 'New';
            document.getElementById('txtAGENCY_DISPLAY_NAME').focus();
            document.getElementById('txtAGENCY_DBA').value = '';
            document.getElementById('cmbAGENCYNAME').options.selectedIndex = -1;
            document.getElementById('txtAGENCY_COMBINED_CODE').value = '';
            //document.getElementById('txtAGENCY_CODE').value = '';
            document.getElementById('txtAGENCY_DISPLAY_NAME').value = '';
            document.getElementById('txtAGENCY_LIC_NUM').value = '';
            document.getElementById('txtAGENCY_ADD1').value = '';
            document.getElementById('txtAGENCY_ADD2').value = '';
            document.getElementById('txtAGENCY_CITY').value = '';
            //document.getElementById('cmbAGENCY_STATE').options.selectedIndex = -1;
            document.getElementById('txtAGENCY_ZIP').value = '';
            //document.getElementById('cmbAGENCY_COUNTRY').value = '<%=aCountry%>';
            document.getElementById('txtAGENCY_PHONE').value = '';
            document.getElementById('txtAGENCY_EXT').value = '';
            document.getElementById('txtAGENCY_FAX').value = '';
            document.getElementById('txtAGENCY_SPEED_DIAL').value = '';

            document.getElementById('txtPRINCIPAL_CONTACT').value = '';
            document.getElementById('txtOTHER_CONTACT').value = '';
            document.getElementById('txtFEDERAL_ID').value = '';
            document.getElementById('txtORIGINAL_CONTRACT_DATE').value = '';
            document.getElementById('txtCURRENT_CONTRACT_DATE').value = '';
            //document.getElementById('lstUNDERWRITER_ASSIGNED_AGENCY').value  = '';
            document.getElementById('txtBANK_ACCOUNT_NUMBER').value = '';
            document.getElementById('txtBANK_NAME').value = '';
            document.getElementById('txtBANK_BRANCH').value = '';
            document.getElementById('txtBANK_NAME_2').value = '';
            document.getElementById('txtBANK_BRANCH_2').value = '';

            document.getElementById('txtROUTING_NUMBER').value = '';
            document.getElementById('txtBANK_ACCOUNT_NUMBER1').value = '';
            document.getElementById('txtROUTING_NUMBER1').value = '';


            document.getElementById('txtAGENCY_EMAIL').value = '';
            document.getElementById('txtAGENCY_WEBSITE').value = '';
            //document.getElementById('cmbAGENCY_PAYMENT_METHOD').options.selectedIndex = 0;
            document.getElementById('cmbAGENCY_BILL_TYPE').options.selectedIndex = 0;
            document.getElementById('cmbTERMINATION_NOTICE').options.selectedIndex = -1;
            //NEW FIELDS
            document.getElementById('txtM_AGENCY_ADD_1').value = '';
            document.getElementById('txtM_AGENCY_ADD_2').value = '';
            document.getElementById('txtM_AGENCY_CITY').value = '';
            //		document.getElementById('cmbM_AGENCY_COUNTRY').value  = '';
            document.getElementById('cmbM_AGENCY_STATE').value = '';
            document.getElementById('txtAGENCY_ZIP').value = '';
            document.getElementById('txtAGENCY_PHONE').value = '';
            //document.getElementById('txtM_AGENCY_EXT').value  = '';
            //document.getElementById('txtM_AGENCY_FAX').value  = '';
            document.getElementById('cmbALLOWS_EFT').options.selectedIndex = 0;
            if (document.getElementById('btnActivateDeactivate'))
                document.getElementById('btnActivateDeactivate').setAttribute('disabled', true);
            if (document.getElementById('btnDelete'))
                document.getElementById('btnDelete').style.display = 'none';
            BANK_ACCOUNT_NUMBER_change(0);
            ROUTING_NUMBER_change(0);
            BANK_ACCOUNT_NUMBER1_change(0);
            ROUTING_NUMBER1_change(0);
            FEDERAL_ID_change();

            CheckForEFT();
            OnBrokerTypeChange();

        }

        function ResetAgency() {
            populateXML();
        }

        function populateXML() {
            document.getElementById('trAGENCY_PAYMENT_METHOD').style.display = 'none';
            ResetAfterActivateDeactivate();
            var tempXML;
            tempXML = document.getElementById("hidOldData").value;
            //alert(tempXML);

            if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')) {
                if (tempXML != "" && tempXML != "0") {
                    //debugger
                    //document.getElementById('btnDelete').style.display='none';
                    populateFormData(tempXML, "MNT_AGENCY_LIST");



                    /*Checking =100, id="rdbACC_CASH_ACC_TYPEO" runat="server" Text="Checking"*/
                    /*Saving	=101,id="rdbACC_CASH_ACC_TYPET" runat="server" Text="Saving"*/


                    if (document.getElementById('hidACCOUNT_TYPE').value == 100)
                        document.getElementById('rdbACC_CASH_ACC_TYPEO').checked = true;

                    if (document.getElementById('hidACCOUNT_TYPE').value == 101)
                        document.getElementById('rdbACC_CASH_ACC_TYPET').checked = true;


                    if (document.getElementById('hidACCOUNT_TYPE_2').value == 100)
                        document.getElementById('rdbACC_CASH_ACC_TYPEO_2').checked = true;

                    if (document.getElementById('hidACCOUNT_TYPE_2').value == 101)
                        document.getElementById('rdbACC_CASH_ACC_TYPET_2').checked = true;

                    //Reverify
                    if (document.getElementById('hidREVERIFIED_AC1').value == 10963)
                        document.getElementById('chkREVERIFIED_AC1').checked = true;
                    else
                        document.getElementById('chkREVERIFIED_AC1').checked = false;

                    if (document.getElementById('hidREVERIFIED_AC2').value == 10963)
                        document.getElementById('chkREVERIFIED_AC2').checked = true;
                    else
                        document.getElementById('chkREVERIFIED_AC2').checked = false;
                    //Added By Raghav For Special Handling.				
                    if (document.getElementById('hidREQ_SPECIAL_HANDLING').value == 10963)
                        document.getElementById('chkREQ_SPECIAL_HANDLING').checked = true;
                    else
                        if (document.getElementById("chkREQ_SPECIAL_HANDLING") != null) {
                            document.getElementById('chkREQ_SPECIAL_HANDLING').checked = false;
                        }

                    //Added by Sibin on 22-09-2008 Itrack No-4768
                    //document.getElementById('txtAGENCY_CODE').readOnly="readonly";
                    //document.getElementById('txtNUM_AGENCY_CODE').readOnly="readonly";
                    //END by Sibin on 22-09-2008 Itrack No-4768							

                    BANK_ACCOUNT_NUMBER_change_hide();
                    ROUTING_NUMBER_change_hide();
                    BANK_ACCOUNT_NUMBER1_change_hide();
                    ROUTING_NUMBER1_change_hide();
                    FEDERAL_ID_change_hide();


                    document.getElementById('txtBROKER_DATE_OF_BIRTH').value = document.getElementById('hidNEW_BROKER_DATE_OF_BIRTH').value;
                    document.getElementById('txtREGIONAL_ID_ISSUE_DATE').value = document.getElementById('hidNEWREGIONAL_ID_ISSUE_DATE').value;
                    document.getElementById('txtTERMINATION_DATE').value = document.getElementById('hidNEW_TERMINATION_DATE').value;
                    document.getElementById('txtTERMINATION_DATE_RENEW').value = document.getElementById('hidNEW_TERMINATION_DATE_RENEW').value;
                    document.getElementById('txtORIGINAL_CONTRACT_DATE').value = document.getElementById('hidNEW_ORIGINAL_CONTRACT_DATE').value;
                    document.getElementById('txtCURRENT_CONTRACT_DATE').value = document.getElementById('hidNEW_CURRENT_CONTRACT_DATE').value;



                }
                else {
                    AddData();
                }
            }
            SetTab();
            ChkNotes();
            CheckForEFT();
            OnBrokerTypeChange();
            return false;
        }
        function ResetAfterActivateDeactivate() {
            if (document.getElementById('hidReset').value == "1") {
                document.MNT_AGENCY_LIST.reset();
            }
        }

        function SetTab() {
            var counter = 6;

            if ('<%=strSystemID.Trim().ToUpper()%>' != '<%=strCarrierSystemID.Trim().ToUpper()%>')
                counter--;

            if ((document.getElementById('hidFormSaved').value == '1') || (document.getElementById("hidOldData").value != "")) {//debugger;
                var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
                var tabtitles = TAB_TITLES.split(',');

                Url = "AgencyStateLobAssoc.aspx?EntityType=Agency&EntityId=" + document.getElementById('hidAGENCY_ID').value + "&";
                DrawTab(counter--, top.frames[1], tabtitles[4], Url);

                Url = "AddDefaultHierarchy.aspx?EntityType=Agency&EntityId=" + document.getElementById('hidAGENCY_ID').value + "&";
                DrawTab(counter--, top.frames[1], tabtitles[3], Url);

                if ('<%=strSystemID.Trim().ToUpper()%>' == '<%=strCarrierSystemID.Trim().ToUpper()%>') {
                    Url = "AddUnderwriterAssignment.aspx?EntityType=Agency&EntityId=" + document.getElementById('hidAGENCY_ID').value + "&";
                    DrawTab(counter--, top.frames[1], tabtitles[2], Url);
                }

                Url = "AttachmentIndex.aspx?calledfrom=agency&EntityType=Agency&EntityId=" + document.getElementById('hidAGENCY_ID').value + "&";
                DrawTab(counter--, top.frames[1], tabtitles[1], Url);
                Url = "UserIndex.aspx?CalledFrom=AGENCY&AGENCY_CODE=" + document.getElementById('hidAGENCY_CODE').value + "&";
                DrawTab(counter--, top.frames[1], tabtitles[0], Url);

            }
            else {
                //Added by Asfa (01-May-2008) - iTrack issue #4132
                RemoveTab(6, top.frames[1]);
                RemoveTab(5, top.frames[1]);
                RemoveTab(4, top.frames[1]);
                RemoveTab(3, top.frames[1]);
                RemoveTab(2, top.frames[1]);
            }
        }
        //Not In use 
        function generateCode() {
            var strname = new String();
            strname = document.getElementById("txtAGENCY_DISPLAY_NAME").value;

            if (document.getElementById('hidAGENCY_ID').value == 'New') {
                if (strname.length > 4)
                    document.getElementById("txtAGENCY_CODE").value = strname.substring(0, 4);
                else
                    document.getElementById("txtAGENCY_CODE").value = strname;
            }


        }

        function CopyPhysicalAddress() {
            if (document.getElementById('txtM_AGENCY_ADD_1').value == "")
                document.getElementById('txtM_AGENCY_ADD_1').value = document.getElementById('txtAGENCY_ADD1').value;
            if (document.getElementById('txtM_AGENCY_ADD_2').value == "")
                document.getElementById('txtM_AGENCY_ADD_2').value = document.getElementById('txtAGENCY_ADD2').value;
            if (document.getElementById('txtM_AGENCY_CITY').value == "")
                document.getElementById('txtM_AGENCY_CITY').value = document.getElementById('txtAGENCY_CITY').value;
            if (document.getElementById('cmbM_AGENCY_COUNTRY').value == "")
                document.getElementById('cmbM_AGENCY_COUNTRY').value = document.getElementById('cmbAGENCY_COUNTRY').value;
            if (document.getElementById('cmbM_AGENCY_STATE').value == "")
                document.getElementById('cmbM_AGENCY_STATE').value = document.getElementById('cmbAGENCY_STATE').value;
            if (document.getElementById('txtM_AGENCY_ZIP').value == "")
                document.getElementById('txtM_AGENCY_ZIP').value = document.getElementById('txtAGENCY_ZIP').value;
            //if(document.getElementById('txtM_AGENCY_PHONE').value=="")
            //document.getElementById('txtM_AGENCY_PHONE').value =document.getElementById('txtAGENCY_PHONE').value	;
            //if(document.getElementById('txtM_AGENCY_EXT').value=="")
            //document.getElementById('txtM_AGENCY_EXT').value =document.getElementById('txtAGENCY_EXT').value	;
            //if(document.getElementById('txtM_AGENCY_FAX').value=="")
            //document.getElementById('txtM_AGENCY_FAX').value =document.getElementById('txtAGENCY_FAX').value ;
            ChangeColor();
            if (document.getElementById('txtM_AGENCY_ADD_1').value != "") {
                document.getElementById('rfvM_AGENCY_ADD_1').setAttribute('enabled', false);
                document.getElementById('rfvM_AGENCY_ADD_1').style.display = 'none';
            }
            return false;
        }
        function EnableDisableRfv() {
            if (document.getElementById('txtM_AGENCY_ADD_1').value == "") {
                document.getElementById('rfvM_AGENCY_ADD_1').setAttribute('enabled', true);
                document.getElementById('rfvM_AGENCY_ADD_1').style.display = 'inline';
            }
        }

        function EnableDisableEFT(txtDesc, rfvDesc, spnDesc, flag) {


            var temp = '<%=strSystemID%>';
            if (flag == false) {
                if (rfvDesc != null) {
                    rfvDesc.setAttribute('enabled', false);
                    rfvDesc.setAttribute('isValid', false);
                    rfvDesc.style.display = "none";
                    spnDesc.style.display = "none";
                    txtDesc.className = "";
                    txtDesc.value = "";
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
            //alert(spnDesc.id + " style " + spnDesc.style.display);
            ChangeColor();
        }
        function EnableDisableLables() {

            document.getElementById('lbl_BANK_NAME').style.display = "none";
            document.getElementById('lbl_BANK_BRANCH').style.display = "none";

            document.getElementById('lbl_BANK_NAME_2').style.display = "none";
            document.getElementById('lbl_BANK_BRANCH_2').style.display = "none";

            document.getElementById('lbl_BANK_ACCOUNT_NUMBER').style.display = "none";

            document.getElementById('lbl_ROUTING_NUMBER').style.display = "none";
            document.getElementById('lbl_BANK_ACCOUNT_NUMBER1').style.display = "none";
            document.getElementById('lbl_ROUTING_NUMBER1').style.display = "none";

            document.getElementById('lbl_REVERIFIED_AC1').style.display = "none";
            document.getElementById('lbl_REVERIFIED_AC2').style.display = "none";
            //CheckForEFT();
            ShowHideEFT();
            ShowHideSweep();

        }


        function ShowHideSweep() {
            var SelectedValue;

            SelectedValue = document.getElementById('cmbALLOWS_CUSTOMER_SWEEP').options[document.getElementById('cmbALLOWS_CUSTOMER_SWEEP').selectedIndex].value;

            if (SelectedValue == 10964) //no
            {

                document.getElementById('txtBANK_NAME_2').style.display = "none";
                document.getElementById('txtBANK_BRANCH_2').style.display = "none";

                document.getElementById('chkREVERIFIED_AC2').style.display = "none";


                document.getElementById('txtBANK_ACCOUNT_NUMBER1').style.display = "none";
                document.getElementById('btnBANK_ACCOUNT_NUMBER1').style.display = "none";
                document.getElementById('capBANK_ACCOUNT_NUMBER1_HID').style.display = "none";
                document.getElementById('txtROUTING_NUMBER1').style.display = "none";
                document.getElementById('btnROUTING_NUMBER1').style.display = "none";
                document.getElementById('capROUTING_NUMBER1_HID').style.display = "none";

                document.getElementById('tdACC_CASH_ACC_TYPE').style.display = "none";


                document.getElementById('revBANK_ACCOUNT_NUMBER1').style.display = "none";
                document.getElementById('csvDFI_ACC_NO1').style.display = "none";
                document.getElementById('revROUTING_NUMBER1').style.display = "none";
                document.getElementById('csvROUTING_NUMBER1').style.display = "none";
                document.getElementById('csvVERIFY_ROUTING_NUMBER1').style.display = "none";
                document.getElementById('csvVERIFY_ROUTING_NUMBER_LENGHT1').style.display = "none";


                EnableDisableEFT(document.getElementById('txtBANK_ACCOUNT_NUMBER1'), document.getElementById('rfvBANK_ACCOUNT_NUMBER1'), document.getElementById('spnBANK_ACCOUNT_NUMBER1'), false);
                EnableDisableEFT(document.getElementById('txtROUTING_NUMBER1'), document.getElementById('rfvROUTING_NUMBER1'), document.getElementById('spnROUTING_NUMBER1'), false);


                document.getElementById('lbl_BANK_NAME_2').style.display = "inline";
                document.getElementById('lbl_BANK_BRANCH_2').style.display = "inline";


                document.getElementById('lbl_BANK_ACCOUNT_NUMBER1').style.display = "inline";
                if ('<%=strCommonCarrierCode%>' != 'S001' && '<%=strCommonCarrierCode%>' != 'SUAT')
                    document.getElementById('lbl_ROUTING_NUMBER1').style.display = "inline";

                document.getElementById('lblACC_CASH_ACC_TYPE').style.display = "inline";

                document.getElementById('lbl_REVERIFIED_AC2').style.display = "inline";
                document.getElementById('chkREVERIFIED_AC2').checked = false;

                document.getElementById('lblVARIFIED2').innerHTML = "-N.A.-";
                document.getElementById('lblVARIFIED_DATE2').innerHTML = "-N.A.-";



            }
            else if (SelectedValue == 10963) //yes
            {

                document.getElementById('lbl_BANK_NAME_2').style.display = "none";
                document.getElementById('lbl_BANK_BRANCH_2').style.display = "none";


                document.getElementById('lbl_BANK_ACCOUNT_NUMBER1').style.display = "none";
                document.getElementById('lbl_ROUTING_NUMBER1').style.display = "none";

                document.getElementById('lblACC_CASH_ACC_TYPE').style.display = "none";

                document.getElementById('lbl_REVERIFIED_AC2').style.display = "none";

                document.getElementById('tdACC_CASH_ACC').style.display = "none";
                EnableDisableEFT(document.getElementById('txtBANK_ACCOUNT_NUMBER1'), document.getElementById('rfvBANK_ACCOUNT_NUMBER1'), document.getElementById('spnBANK_ACCOUNT_NUMBER1'), true);
                EnableDisableEFT(document.getElementById('txtROUTING_NUMBER1'), document.getElementById('rfvROUTING_NUMBER1'), document.getElementById('spnROUTING_NUMBER1'), true);


                document.getElementById('txtBANK_NAME_2').style.display = "inline";
                document.getElementById('txtBANK_BRANCH_2').style.display = "inline";

                //document.getElementById('txtBANK_ACCOUNT_NUMBER1').style.display= "inline" ;
                if (document.getElementById('hidBANK_ACCOUNT_NUMBER1') != null && typeof document.getElementById('hidBANK_ACCOUNT_NUMBER1') != 'undefined' && document.getElementById('hidBANK_ACCOUNT_NUMBER1').value != '') {
                    BANK_ACCOUNT_NUMBER1_change_hide();
                }
                else {
                    BANK_ACCOUNT_NUMBER1_change(1);
                }
                //document.getElementById('txtROUTING_NUMBER1').style.display= "inline" ;	
                if (document.getElementById('hidROUTING_NUMBER1') != null && typeof document.getElementById('hidROUTING_NUMBER1') != 'undefined' && document.getElementById('hidROUTING_NUMBER1').value != '') {
                    ROUTING_NUMBER1_change_hide();
                }
                else {
                    ROUTING_NUMBER1_change(1);
                }
                document.getElementById('tdACC_CASH_ACC_TYPE').style.display = "inline";

                document.getElementById('chkREVERIFIED_AC2').style.display = "inline";


                var strSystemID = '<%=GetSystemId().ToUpper()%>';

                if (strSystemId == 'S001' || strSystemId == 'SUAT') {

                    document.getElementById('csvROUTING_NUMBER').style.display = "none";
                    document.getElementById('csvVERIFY_ROUTING_NUMBER').style.display = "none";
                    document.getElementById('csvVERIFY_ROUTING_NUMBER_LENGHT').style.display = "none";
                    document.getElementById('rfvROUTING_NUMBER').style.display = "none";

                    EnableDisableEFT(document.getElementById('txtROUTING_NUMBER'), document.getElementById('rfvROUTING_NUMBER'), document.getElementById('spnROUTING_NUMBER'), false);

                }
            }
        }

        function ShowHideEFT() {
            var SelectedValue;
            //if (document.getElementById('cmbALLOWS_EFT').selectedIndex != '0')	
            //{


            SelectedValue = document.getElementById('cmbALLOWS_EFT').options[document.getElementById('cmbALLOWS_EFT').selectedIndex].value;

            if (SelectedValue == 10964) //no
            {
                document.getElementById('txtBANK_NAME').style.display = "none";
                document.getElementById('txtBANK_BRANCH').style.display = "none";


                document.getElementById('txtBANK_ACCOUNT_NUMBER').style.display = "none";
                document.getElementById('capBANK_ACCOUNT_NUMBER_HID').style.display = "none";
                document.getElementById('btnBANK_ACCOUNT_NUMBER').style.display = "none";
                document.getElementById('capROUTING_NUMBER_HID').style.display = "none";
                document.getElementById('btnROUTING_NUMBER').style.display = "none";
                document.getElementById('txtROUTING_NUMBER').style.display = "none";

                document.getElementById('tdACC_CASH_ACC_TYPE_2').style.display = "none";

                document.getElementById('chkREVERIFIED_AC1').style.display = "none";

                document.getElementById('revBANK_ACCOUNT_NUMBER').style.display = "none";
                document.getElementById('csvDFI_ACC_NO').style.display = "none";
                document.getElementById('revROUTING_NUMBER').style.display = "none";
                document.getElementById('csvROUTING_NUMBER').style.display = "none";
                document.getElementById('csvVERIFY_ROUTING_NUMBER').style.display = "none";
                document.getElementById('csvVERIFY_ROUTING_NUMBER_LENGHT').style.display = "none";





                EnableDisableEFT(document.getElementById('txtBANK_ACCOUNT_NUMBER'), document.getElementById('rfvBANK_ACCOUNT_NUMBER'), document.getElementById('spnBANK_ACCOUNT_NUMBER'), false);
                EnableDisableEFT(document.getElementById('txtROUTING_NUMBER'), document.getElementById('rfvROUTING_NUMBER'), document.getElementById('spnROUTING_NUMBER'), false);

                document.getElementById('lbl_BANK_NAME').style.display = "inline";
                document.getElementById('lbl_BANK_BRANCH').style.display = "inline";

                document.getElementById('lbl_BANK_ACCOUNT_NUMBER').style.display = "inline";
                if ('<%=strCommonCarrierCode%>' != 'S001' && '<%=strCommonCarrierCode%>' != 'SUAT')
                    document.getElementById('lbl_ROUTING_NUMBER').style.display = "inline";

                document.getElementById('lblACC_CASH_ACC_TYPE_2').style.display = "inline";

                document.getElementById('lbl_REVERIFIED_AC1').style.display = "inline";
                document.getElementById('chkREVERIFIED_AC1').checked = false;

                document.getElementById('lblVARIFIED1').innerHTML = "-N.A.-";
                document.getElementById('lblVARIFIED_DATE1').innerHTML = "-N.A.-";


            }
            else if (SelectedValue == 10963) //yes
            {
                var strSystemId = '<%=GetSystemId().ToUpper().Trim()%>';
                document.getElementById('lbl_BANK_NAME').style.display = "none";
                document.getElementById('lbl_BANK_BRANCH').style.display = "none";


                document.getElementById('lbl_BANK_ACCOUNT_NUMBER').style.display = "none";
                document.getElementById('lbl_ROUTING_NUMBER').style.display = "none";

                document.getElementById('lblACC_CASH_ACC_TYPE_2').style.display = "none";

                document.getElementById('lbl_REVERIFIED_AC1').style.display = "none";


                EnableDisableEFT(document.getElementById('txtBANK_ACCOUNT_NUMBER'), document.getElementById('rfvBANK_ACCOUNT_NUMBER'), document.getElementById('spnBANK_ACCOUNT_NUMBER'), true);
                if ('<%=strCommonCarrierCode%>' != 'S001' && '<%=strCommonCarrierCode%>' != 'SUAT')
                    EnableDisableEFT(document.getElementById('txtROUTING_NUMBER'), document.getElementById('rfvROUTING_NUMBER'), document.getElementById('spnROUTING_NUMBER'), true);


                document.getElementById('txtBANK_NAME').style.display = "inline";
                document.getElementById('txtBANK_BRANCH').style.display = "inline";

                //document.getElementById('txtBANK_ACCOUNT_NUMBER').style.display= "inline" ;
                if (document.getElementById('hidBANK_ACCOUNT_NUMBER') != null && typeof document.getElementById('hidBANK_ACCOUNT_NUMBER') != 'undefined' && document.getElementById('hidBANK_ACCOUNT_NUMBER').value != '') {
                    BANK_ACCOUNT_NUMBER_change_hide();
                }
                else {
                    BANK_ACCOUNT_NUMBER_change(1);
                }
                //document.getElementById('txtROUTING_NUMBER1').style.display= "inline" ;	
                if (document.getElementById('hidROUTING_NUMBER') != null && typeof document.getElementById('hidROUTING_NUMBER') != 'undefined' && document.getElementById('hidROUTING_NUMBER').value != '') {
                    ROUTING_NUMBER_change_hide();
                }
                else {
                    ROUTING_NUMBER_change(1);
                }

                document.getElementById('tdACC_CASH_ACC_TYPE_2').style.display = "inline";

                document.getElementById('chkREVERIFIED_AC1').style.display = "inline";

                document.getElementById('btnROUTING_NUMBER').style.display = "none";




            }


            if (strSystemId == 'S001' || strSystemId == 'SUAT') {

                document.getElementById('csvROUTING_NUMBER').style.display = "none";
                document.getElementById('csvVERIFY_ROUTING_NUMBER').style.display = "none";
                document.getElementById('csvVERIFY_ROUTING_NUMBER_LENGHT').style.display = "none";
                document.getElementById('rfvROUTING_NUMBER').style.display = "none";

                //EnableDisableEFT(document.getElementById('txtROUTING_NUMBER'), document.getElementById('rfvROUTING_NUMBER'), document.getElementById('spnROUTING_NUMBER'), false);

            }
        }

        function CheckForEFT() {
            var SelectedValue;
            //if (document.getElementById('cmbALLOWS_EFT').selectedIndex != '0')	
            //{

            SelectedValue = document.getElementById('cmbALLOWS_EFT').options[document.getElementById('cmbALLOWS_EFT').selectedIndex].value;

            if (SelectedValue == 10964) {
                document.getElementById('txtBANK_NAME').style.display = "none";
                document.getElementById('txtBANK_BRANCH').style.display = "none";

                document.getElementById('txtBANK_NAME_2').style.display = "none";
                document.getElementById('txtBANK_BRANCH_2').style.display = "none";

                document.getElementById('txtBANK_ACCOUNT_NUMBER').style.display = "none";
                document.getElementById('capBANK_ACCOUNT_NUMBER_HID').style.display = "none";
                document.getElementById('btnBANK_ACCOUNT_NUMBER').style.display = "none";
                document.getElementById('capROUTING_NUMBER_HID').style.display = "none";
                document.getElementById('btnROUTING_NUMBER').style.display = "none";
                if ('<%=strCommonCarrierCode%>' != 'S001' && '<%=strCommonCarrierCode%>' != 'SUAT')
                    document.getElementById('txtROUTING_NUMBER').style.display = "none";
                document.getElementById('txtBANK_ACCOUNT_NUMBER1').style.display = "none";
                document.getElementById('capBANK_ACCOUNT_NUMBER1_HID').style.display = "none";
                document.getElementById('btnBANK_ACCOUNT_NUMBER1').style.display = "none";
                document.getElementById('txtROUTING_NUMBER1').style.display = "none";
                document.getElementById('capROUTING_NUMBER1_HID').style.display = "none";
                document.getElementById('btnROUTING_NUMBER1').style.display = "none";
                document.getElementById('tdACC_CASH_ACC_TYPE').style.display = "none";
                document.getElementById('tdACC_CASH_ACC_TYPE_2').style.display = "none";

                document.getElementById('revBANK_ACCOUNT_NUMBER').style.display = "none";
                document.getElementById('csvDFI_ACC_NO').style.display = "none";
                document.getElementById('revROUTING_NUMBER').style.display = "none";
                document.getElementById('csvROUTING_NUMBER').style.display = "none";
                document.getElementById('csvVERIFY_ROUTING_NUMBER').style.display = "none";
                document.getElementById('csvVERIFY_ROUTING_NUMBER_LENGHT').style.display = "none";

                document.getElementById('revBANK_ACCOUNT_NUMBER1').style.display = "none";
                document.getElementById('csvDFI_ACC_NO1').style.display = "none";
                document.getElementById('revROUTING_NUMBER1').style.display = "none";
                document.getElementById('csvROUTING_NUMBER1').style.display = "none";
                document.getElementById('csvVERIFY_ROUTING_NUMBER1').style.display = "none";
                document.getElementById('csvVERIFY_ROUTING_NUMBER_LENGHT1').style.display = "none";

                document.getElementById('chkREVERIFIED_AC1').style.display = "none";

                //EnableDisableEFT(document.getElementById('txtBANK_NAME'),document.getElementById('rfvBANK_NAME'),document.getElementById('spnBANK_NAME'),false);								
                //EnableDisableEFT(document.getElementById('txtBANK_BRANCH'),document.getElementById('rfvBANK_BRANCH'),document.getElementById('spnBANK_BRANCH'),false);	

                //EnableDisableEFT(document.getElementById('txtBANK_NAME_2'),document.getElementById('rfvBANK_NAME_2'),document.getElementById('spnBANK_NAME_2'),false);								
                //EnableDisableEFT(document.getElementById('txtBANK_BRANCH_2'),document.getElementById('rfvBANK_BRANCH_2'),document.getElementById('spnBANK_BRANCH_2'),false);	


                EnableDisableEFT(document.getElementById('txtBANK_ACCOUNT_NUMBER'), document.getElementById('rfvBANK_ACCOUNT_NUMBER'), document.getElementById('spnBANK_ACCOUNT_NUMBER'), false);
                if ('<%=strCommonCarrierCode%>' != 'S001' && '<%=strCommonCarrierCode%>' != 'SUAT')
                    EnableDisableEFT(document.getElementById('txtROUTING_NUMBER'), document.getElementById('rfvROUTING_NUMBER'), document.getElementById('spnROUTING_NUMBER'), false);
                EnableDisableEFT(document.getElementById('txtBANK_ACCOUNT_NUMBER1'), document.getElementById('rfvBANK_ACCOUNT_NUMBER1'), document.getElementById('spnBANK_ACCOUNT_NUMBER1'), false);
                EnableDisableEFT(document.getElementById('txtROUTING_NUMBER1'), document.getElementById('rfvROUTING_NUMBER1'), document.getElementById('spnROUTING_NUMBER1'), false);

                document.getElementById('lbl_BANK_NAME').style.display = "inline";
                document.getElementById('lbl_BANK_BRANCH').style.display = "inline";

                document.getElementById('lbl_BANK_NAME_2').style.display = "inline";
                document.getElementById('lbl_BANK_BRANCH_2').style.display = "inline";

                document.getElementById('lbl_BANK_ACCOUNT_NUMBER').style.display = "inline";
                document.getElementById('lbl_ROUTING_NUMBER').style.display = "inline";
                document.getElementById('lbl_BANK_ACCOUNT_NUMBER1').style.display = "inline";
                if ('<%=strCommonCarrierCode%>' != 'S001' && '<%=strCommonCarrierCode%>' != 'SUAT')
                    document.getElementById('lbl_ROUTING_NUMBER1').style.display = "inline";
                document.getElementById('lblACC_CASH_ACC_TYPE').style.display = "inline";
                document.getElementById('lblACC_CASH_ACC_TYPE_2').style.display = "inline";


                document.getElementById('lbl_REVERIFIED_AC1').style.display = "inline";
                document.getElementById('chkREVERIFIED_AC1').checked = false;
                /*document.getElementById('rfvBANK_ACCOUNT_NUMBER').style.display= "none" ;
                document.getElementById('rfvROUTING_NUMBER').style.display= "none" ;
                document.getElementById('rfvBANK_ACCOUNT_NUMBER1').style.display= "none" ;
                document.getElementById('rfvROUTING_NUMBER1').style.display= "none" ;
					
                document.getElementById('spnBANK_ACCOUNT_NUMBER').style.display= "none" ;
                document.getElementById('spnROUTING_NUMBER').style.display= "none" ;
                document.getElementById('spnBANK_ACCOUNT_NUMBER1').style.display= "none" ;
                document.getElementById('spnROUTING_NUMBER1').style.display= "none" ;*/

            }
            else if (SelectedValue == 10963) {
                document.getElementById('lbl_BANK_NAME').style.display = "none";
                document.getElementById('lbl_BANK_BRANCH').style.display = "none";

                document.getElementById('lbl_BANK_NAME_2').style.display = "none";
                document.getElementById('lbl_BANK_BRANCH_2').style.display = "none";

                document.getElementById('lbl_BANK_ACCOUNT_NUMBER').style.display = "none";
                document.getElementById('lbl_ROUTING_NUMBER').style.display = "none";
                document.getElementById('lbl_BANK_ACCOUNT_NUMBER1').style.display = "none";
                document.getElementById('lbl_ROUTING_NUMBER1').style.display = "none";
                document.getElementById('lblACC_CASH_ACC_TYPE').style.display = "none";
                document.getElementById('lblACC_CASH_ACC_TYPE_2').style.display = "none";

                document.getElementById('lbl_REVERIFIED_AC1').style.display = "none";



                //EnableDisableEFT(document.getElementById('txtBANK_NAME'),document.getElementById('rfvBANK_NAME'),document.getElementById('spnBANK_NAME'),true);			
                //EnableDisableEFT(document.getElementById('txtBANK_BRANCH'),document.getElementById('rfvBANK_BRANCH'),document.getElementById('spnBANK_BRANCH'),true);		

                //EnableDisableEFT(document.getElementById('txtBANK_NAME_2'),document.getElementById('rfvBANK_NAME_2'),document.getElementById('spnBANK_NAME_2'),true);			
                //EnableDisableEFT(document.getElementById('txtBANK_BRANCH_2'),document.getElementById('rfvBANK_BRANCH_2'),document.getElementById('spnBANK_BRANCH_2'),true);			

                EnableDisableEFT(document.getElementById('txtBANK_ACCOUNT_NUMBER'), document.getElementById('rfvBANK_ACCOUNT_NUMBER'), document.getElementById('spnBANK_ACCOUNT_NUMBER'), true);
                if ('<%=strCommonCarrierCode%>' != 'S001' && '<%=strCommonCarrierCode%>' != 'SUAT')
                    EnableDisableEFT(document.getElementById('txtROUTING_NUMBER'), document.getElementById('rfvROUTING_NUMBER'), document.getElementById('spnROUTING_NUMBER'), true);
                EnableDisableEFT(document.getElementById('txtBANK_ACCOUNT_NUMBER1'), document.getElementById('rfvBANK_ACCOUNT_NUMBER1'), document.getElementById('spnBANK_ACCOUNT_NUMBER1'), true);
                EnableDisableEFT(document.getElementById('txtROUTING_NUMBER1'), document.getElementById('rfvROUTING_NUMBER1'), document.getElementById('spnROUTING_NUMBER1'), true);

                document.getElementById('txtBANK_NAME').style.display = "inline";
                document.getElementById('txtBANK_BRANCH').style.display = "inline";

                document.getElementById('txtBANK_NAME_2').style.display = "inline";
                document.getElementById('txtBANK_BRANCH_2').style.display = "inline";

                //document.getElementById('txtBANK_ACCOUNT_NUMBER1').style.display= "inline" ;
                if (document.getElementById('hidBANK_ACCOUNT_NUMBER') != null && typeof document.getElementById('hidBANK_ACCOUNT_NUMBER') != 'undefined' && document.getElementById('hidBANK_ACCOUNT_NUMBER').value != '') {
                    BANK_ACCOUNT_NUMBER_change_hide();
                }
                else {
                    BANK_ACCOUNT_NUMBER_change(1);
                }
                //document.getElementById('txtROUTING_NUMBER1').style.display= "inline" ;	
                if (document.getElementById('hidROUTING_NUMBER') != null && typeof document.getElementById('hidROUTING_NUMBER') != 'undefined' && document.getElementById('hidROUTING_NUMBER').value != '') {
                    ROUTING_NUMBER_change_hide();
                }
                else {
                    ROUTING_NUMBER_change(1);
                }
                //document.getElementById('txtBANK_ACCOUNT_NUMBER1').style.display= "inline" ;
                if (document.getElementById('hidBANK_ACCOUNT_NUMBER1') != null && typeof document.getElementById('hidBANK_ACCOUNT_NUMBER1') != 'undefined' && document.getElementById('hidBANK_ACCOUNT_NUMBER1').value != '') {
                    BANK_ACCOUNT_NUMBER1_change_hide();
                }
                else {
                    BANK_ACCOUNT_NUMBER1_change(1);
                }
                //document.getElementById('txtROUTING_NUMBER1').style.display= "inline" ;	
                if (document.getElementById('hidROUTING_NUMBER1') != null && typeof document.getElementById('hidROUTING_NUMBER1') != 'undefined' && document.getElementById('hidROUTING_NUMBER1').value != '') {
                    ROUTING_NUMBER1_change_hide();
                }
                else {
                    ROUTING_NUMBER1_change(1);
                }
                document.getElementById('tdACC_CASH_ACC_TYPE').style.display = "inline";
                document.getElementById('tdACC_CASH_ACC_TYPE_2').style.display = "inline";

                document.getElementById('chkREVERIFIED_AC1').style.display = "inline";

                //					document.getElementById('rdbACC_CASH_ACC_TYPEO').checked = true;
                //					document.getElementById('rdbACC_CASH_ACC_TYPEO_2').checked = true;		
            }

            if (document.getElementById('txtFEDERAL_ID').value != '')
                if (document.getElementById('hidFEDERAL_ID') != null && typeof document.getElementById('hidFEDERAL_ID') != 'undefined' && document.getElementById('hidFEDERAL_ID').value != '') {
                    FEDERAL_ID_change_hide();
                }
                else {
                    FEDERAL_ID_change();
                }

            //}
            //else
            //{
            //EnableDisableEFT(document.getElementById('txtBANK_NAME'),document.getElementById('rfvBANK_NAME'),document.getElementById('spnBANK_NAME'),false);								
            //EnableDisableEFT(document.getElementById('txtBANK_BRANCH'),document.getElementById('rfvBANK_BRANCH'),document.getElementById('spnBANK_BRANCH'),false);	

            //EnableDisableEFT(document.getElementById('txtBANK_NAME_2'),document.getElementById('rfvBANK_NAME_2'),document.getElementById('spnBANK_NAME_2'),false);								
            //EnableDisableEFT(document.getElementById('txtBANK_BRANCH_2'),document.getElementById('rfvBANK_BRANCH_2'),document.getElementById('spnBANK_BRANCH_2'),false);			

            //EnableDisableEFT(document.getElementById('txtBANK_ACCOUNT_NUMBER'),document.getElementById('rfvBANK_ACCOUNT_NUMBER'),document.getElementById('spnBANK_ACCOUNT_NUMBER'),false);							
            //EnableDisableEFT(document.getElementById('txtROUTING_NUMBER'),document.getElementById('rfvROUTING_NUMBER'),document.getElementById('spnROUTING_NUMBER'),false);							
            //EnableDisableEFT(document.getElementById('txtBANK_ACCOUNT_NUMBER1'),document.getElementById('rfvBANK_ACCOUNT_NUMBER1'),document.getElementById('spnBANK_ACCOUNT_NUMBER1'),false);							
            //EnableDisableEFT(document.getElementById('txtROUTING_NUMBER1'),document.getElementById('rfvROUTING_NUMBER1'),document.getElementById('spnROUTING_NUMBER1'),false);							

            //}

        }

        function showHide() {
            if (document.getElementById('RowId').value != 'New') {
                if (document.getElementById('btnActivateDeactivate'))
                    document.getElementById('btnActivateDeactivate').setAttribute('disabled', false);
                //document.getElementById('btnReset').style.display="none";
            }
        }
        /*
        function CountUnderWriter()
        {
        document.getElementById("hidUnderWriter").value = "";
        var coll = document.MNT_AGENCY_LIST.lstUNDERWRITER_ASSIGNED_AGENCY;
        var len = coll.options.length;
        var k;
        var szSelectedDept;
        //alert(document.getElementById("lstWATERS_NAVIGATED").options[0].selected);
        for( k = 0;k < len ; k++)
        {											
        if(document.getElementById("lstUNDERWRITER_ASSIGNED_AGENCY").options[k].selected == true)
        {
        szSelectedDept = coll.options(k).value;
        if (document.getElementById("hidUnderWriter").value == "")
        {
        document.getElementById("hidUnderWriter").value =  szSelectedDept;
        }
        else
        {
        document.MNT_AGENCY_LIST.hidUnderWriter.value = document.MNT_AGENCY_LIST.hidUnderWriter.value + "," + szSelectedDept;
						
        }
        //alert(document.APP_WATERCRAFT_INFO.hidWaterNavigateID.value);
        }	
			
        }
        //			document.AssignDivDept.TextBox1.style.display = 'none';		
        }*/
        function ChkDate(objSource, objArgs) {
            var effdate = document.MNT_AGENCY_LIST.txtORIGINAL_CONTRACT_DATE.value;
            objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>", effdate, jsaAppDtFormat);
        }

        function ChkNotes() {
            if (document.getElementById('txtTERMINATION_DATE_RENEW').value == '') {
                document.getElementById('spnTERMINATION_NOTICE').style.display = "none";
                document.getElementById('rfvTERMINATION_NOTICE').enabled = false;
                document.getElementById('rfvTERMINATION_NOTICE').style.display = 'none';
                document.getElementById("cmbTERMINATION_NOTICE").style.backgroundColor = "white";
            }
            else {
                if (document.getElementById("cmbTERMINATION_NOTICE").value == "")
                    document.getElementById("cmbTERMINATION_NOTICE").style.backgroundColor = "#FFFFD1";
                else
                    document.getElementById("cmbTERMINATION_NOTICE").style.backgroundColor = "white";
                document.getElementById('spnTERMINATION_NOTICE').style.display = "inline";
                document.getElementById('rfvTERMINATION_NOTICE').enabled = true;
            }
        }
        function BANK_ACCOUNT_NUMBER_change(callfrom) {
            var Cancel = document.getElementById("hidCancel").value;
            var Edit = document.getElementById("hidEdit").value;
            //document.getElementById('txtSSN_NO').value = document.getElementById('txtSSN_NO_HID').value;
            document.getElementById('txtBANK_ACCOUNT_NUMBER').value = '';
            document.getElementById('txtBANK_ACCOUNT_NUMBER').style.display = 'inline';
            document.getElementById("rfvBANK_ACCOUNT_NUMBER").setAttribute('enabled', true);
            document.getElementById("revBANK_ACCOUNT_NUMBER").setAttribute('enabled', true);
            document.getElementById("csvDFI_ACC_NO").setAttribute('enabled', true);

            if (callfrom == '0') {
                if (document.getElementById("btnBANK_ACCOUNT_NUMBER").value == 'Edit' || document.getElementById("btnBANK_ACCOUNT_NUMBER").value == 'Editar')
                    document.getElementById("btnBANK_ACCOUNT_NUMBER").value = Cancel;
                else if (document.getElementById("btnBANK_ACCOUNT_NUMBER").value == 'Cancel' || document.getElementById("btnBANK_ACCOUNT_NUMBER").value == 'Cancelar')
                    BANK_ACCOUNT_NUMBER_change_hide();
                else
                    document.getElementById("btnBANK_ACCOUNT_NUMBER").style.display = 'none';
            }

        }
        function ROUTING_NUMBER_change(callfrom) {
            var Cancel = document.getElementById("hidCancel").value;
            var Edit = document.getElementById("hidEdit").value;
            if ('<%=strCommonCarrierCode%>' != 'S001' && '<%=strCommonCarrierCode%>' != 'SUAT') {
                document.getElementById('txtROUTING_NUMBER').value = '';
                document.getElementById('txtROUTING_NUMBER').style.display = 'inline';
                document.getElementById("rfvROUTING_NUMBER").setAttribute('enabled', true);
                document.getElementById("revROUTING_NUMBER").setAttribute('enabled', true);
                document.getElementById("csvROUTING_NUMBER").setAttribute('enabled', true);
                document.getElementById("csvVERIFY_ROUTING_NUMBER").setAttribute('enabled', true);
                document.getElementById("csvVERIFY_ROUTING_NUMBER_LENGHT").setAttribute('enabled', true);

                if (callfrom == '0') {
                    if (document.getElementById("btnROUTING_NUMBER").value == 'Edit' || document.getElementById("btnROUTING_NUMBER").value == 'Editar')
                        document.getElementById("btnROUTING_NUMBER").value = Cancel;
                    else if (document.getElementById("btnROUTING_NUMBER").value == 'Cancel' || document.getElementById("btnROUTING_NUMBER").value == 'Cancelar')
                        ROUTING_NUMBER_change_hide();
                    else
                        document.getElementById("btnROUTING_NUMBER").style.display = 'none';
                }
            }
            //document.getElementById('txtSSN_NO_HID').style.display = 'none';			
        }

        function BANK_ACCOUNT_NUMBER_change_hide() {
            var Cancel = document.getElementById("hidCancel").value;
            var Edit = document.getElementById("hidEdit").value;
            document.getElementById("capBANK_ACCOUNT_NUMBER_HID").style.display = 'inline';
            document.getElementById("btnBANK_ACCOUNT_NUMBER").style.display = 'inline';
            if ('<%=strCommonCarrierCode%>' != 'S001' && '<%=strCommonCarrierCode%>' != 'SUAT') {
                document.getElementById("btnROUTING_NUMBER").style.display = 'inline';
            }
            document.getElementById('txtBANK_ACCOUNT_NUMBER').value = '';
            document.getElementById('txtBANK_ACCOUNT_NUMBER').style.display = 'none';
            document.getElementById("rfvBANK_ACCOUNT_NUMBER").style.display = 'none';
            document.getElementById("rfvBANK_ACCOUNT_NUMBER").setAttribute('enabled', false);
            document.getElementById("revBANK_ACCOUNT_NUMBER").style.display = 'none';
            document.getElementById("revBANK_ACCOUNT_NUMBER").setAttribute('enabled', false);
            document.getElementById("csvDFI_ACC_NO").style.display = 'none';
            document.getElementById("csvDFI_ACC_NO").setAttribute('enabled', false);
            document.getElementById("btnBANK_ACCOUNT_NUMBER").value = Edit;
        }
        function ROUTING_NUMBER_change_hide() {
            var Cancel = document.getElementById("hidCancel").value;
            var Edit = document.getElementById("hidEdit").value;
            document.getElementById("capROUTING_NUMBER_HID").style.display = 'inline';
            if ('<%=strCommonCarrierCode%>' != 'S001' && '<%=strCommonCarrierCode%>' != 'SUAT') {
                document.getElementById("btnROUTING_NUMBER").value = 'Edit';

                document.getElementById("btnROUTING_NUMBER").style.display = 'inline';
            }
            if ('<%=strCommonCarrierCode%>' != 'S001' && '<%=strCommonCarrierCode%>' != 'SUAT') {
                document.getElementById('txtROUTING_NUMBER').value = '';
                document.getElementById('txtROUTING_NUMBER').style.display = 'none';
                document.getElementById('rfvROUTING_NUMBER').style.display = 'none';
                document.getElementById("rfvROUTING_NUMBER").setAttribute('enabled', false);
                document.getElementById('revROUTING_NUMBER').style.display = 'none';
                document.getElementById("revROUTING_NUMBER").setAttribute('enabled', false);
                document.getElementById('csvROUTING_NUMBER').style.display = 'none';
                document.getElementById("csvROUTING_NUMBER").setAttribute('enabled', false);
                document.getElementById('csvVERIFY_ROUTING_NUMBER').style.display = 'none';
                document.getElementById("csvVERIFY_ROUTING_NUMBER").setAttribute('enabled', false);
                document.getElementById('csvVERIFY_ROUTING_NUMBER_LENGHT').style.display = 'none';
                document.getElementById("csvVERIFY_ROUTING_NUMBER_LENGHT").setAttribute('enabled', false);

                document.getElementById("btnROUTING_NUMBER").value = Edit;
            }

            //document.getElementById('txtSSN_NO_HID').style.display = 'none';			
        }

        function BANK_ACCOUNT_NUMBER1_change(callfrom) {
            //document.getElementById('txtSSN_NO').value = document.getElementById('txtSSN_NO_HID').value;
            document.getElementById('txtBANK_ACCOUNT_NUMBER1').value = '';
            document.getElementById('txtBANK_ACCOUNT_NUMBER1').style.display = 'inline';
            document.getElementById("rfvBANK_ACCOUNT_NUMBER1").setAttribute('enabled', true);
            document.getElementById("revBANK_ACCOUNT_NUMBER1").setAttribute('enabled', true);
            document.getElementById("csvDFI_ACC_NO1").setAttribute('enabled', true);

            if (callfrom == '0') {
                if (document.getElementById("btnBANK_ACCOUNT_NUMBER1").value == 'Edit')
                    document.getElementById("btnBANK_ACCOUNT_NUMBER1").value = 'Cancel';
                else if (document.getElementById("btnBANK_ACCOUNT_NUMBER1").value == 'Cancel')
                    BANK_ACCOUNT_NUMBER1_change_hide();
                else
                    document.getElementById("btnBANK_ACCOUNT_NUMBER1").style.display = 'none';
            }
        }
        function ROUTING_NUMBER1_change(callfrom) {
            document.getElementById('txtROUTING_NUMBER1').value = '';
            document.getElementById('txtROUTING_NUMBER1').style.display = 'inline';
            document.getElementById("rfvROUTING_NUMBER1").setAttribute('enabled', true);
            document.getElementById("revROUTING_NUMBER1").setAttribute('enabled', true);
            document.getElementById("csvROUTING_NUMBER1").setAttribute('enabled', true);
            document.getElementById("csvVERIFY_ROUTING_NUMBER1").setAttribute('enabled', true);
            document.getElementById("csvVERIFY_ROUTING_NUMBER_LENGHT1").setAttribute('enabled', true);

            if (callfrom == '0') {
                if (document.getElementById("btnROUTING_NUMBER1").value == 'Edit')
                    document.getElementById("btnROUTING_NUMBER1").value = 'Cancel';
                else if (document.getElementById("btnROUTING_NUMBER1").value == 'Cancel')
                    ROUTING_NUMBER1_change_hide();
                else
                    document.getElementById("btnROUTING_NUMBER1").style.display = 'none';
            }
            //document.getElementById('txtSSN_NO_HID').style.display = 'none';			
        }

        function BANK_ACCOUNT_NUMBER1_change_hide() {
            document.getElementById("capBANK_ACCOUNT_NUMBER1_HID").style.display = 'inline';
            document.getElementById("btnBANK_ACCOUNT_NUMBER1").style.display = 'inline';
            document.getElementById('txtBANK_ACCOUNT_NUMBER1').value = '';
            document.getElementById('txtBANK_ACCOUNT_NUMBER1').style.display = 'none';
            document.getElementById("rfvBANK_ACCOUNT_NUMBER1").style.display = 'none';
            document.getElementById("rfvBANK_ACCOUNT_NUMBER1").setAttribute('enabled', false);
            document.getElementById("revBANK_ACCOUNT_NUMBER1").style.display = 'none';
            document.getElementById("revBANK_ACCOUNT_NUMBER1").setAttribute('enabled', false);
            document.getElementById("csvDFI_ACC_NO1").style.display = 'none';
            document.getElementById("csvDFI_ACC_NO1").setAttribute('enabled', false);
            document.getElementById("btnBANK_ACCOUNT_NUMBER1").value = 'Edit';
        }
        function ROUTING_NUMBER1_change_hide() {
            document.getElementById("btnROUTING_NUMBER1").style.display = 'inline';
            document.getElementById("capROUTING_NUMBER1_HID").style.display = 'inline';
            document.getElementById('txtROUTING_NUMBER1').value = '';
            document.getElementById('txtROUTING_NUMBER1').style.display = 'none';
            document.getElementById('rfvROUTING_NUMBER1').style.display = 'none';
            document.getElementById("rfvROUTING_NUMBER1").setAttribute('enabled', false);
            document.getElementById('revROUTING_NUMBER1').style.display = 'none';
            document.getElementById("revROUTING_NUMBER1").setAttribute('enabled', false);
            document.getElementById('csvROUTING_NUMBER1').style.display = 'none';
            document.getElementById("csvROUTING_NUMBER1").setAttribute('enabled', false);
            document.getElementById('csvVERIFY_ROUTING_NUMBER1').style.display = 'none';
            document.getElementById("csvVERIFY_ROUTING_NUMBER1").setAttribute('enabled', false);
            document.getElementById('csvVERIFY_ROUTING_NUMBER_LENGHT1').style.display = 'none';
            document.getElementById("csvVERIFY_ROUTING_NUMBER_LENGHT1").setAttribute('enabled', false);
            document.getElementById("btnROUTING_NUMBER1").value = 'Edit';

            //document.getElementById('txtSSN_NO_HID').style.display = 'none';			
        }

        function FEDERAL_ID_change() {
            var Cancel = document.getElementById("hidCancel").value;
            var Edit = document.getElementById("hidEdit").value;
            //document.getElementById('txtSSN_NO').value = document.getElementById('txtSSN_NO_HID').value;
            document.getElementById('txtFEDERAL_ID').value = '';
            document.getElementById('txtFEDERAL_ID').style.display = 'inline';
            //document.getElementById("rfvFEDERAL_ID").setAttribute('enabled',true);
            document.getElementById("revFEDERAL_ID").setAttribute('enabled', true);

            if (document.getElementById("btnFEDERAL_ID").value == 'Edit' || document.getElementById("btnFEDERAL_ID").value == 'Editar')
                document.getElementById("btnFEDERAL_ID").value = Cancel;
            else if (document.getElementById("btnFEDERAL_ID").value == 'Cancel' || document.getElementById("btnFEDERAL_ID").value == 'Cancelar')
                FEDERAL_ID_change_hide();
            else
                document.getElementById("btnFEDERAL_ID").style.display = 'none';

        }

        function FEDERAL_ID_change_hide() {
            var Cancel = document.getElementById("hidCancel").value;
            var Edit = document.getElementById("hidEdit").value;
            document.getElementById("capFEDERAL_ID_HID").style.display = 'inline';
            document.getElementById("btnFEDERAL_ID").style.display = 'inline';
            document.getElementById('txtFEDERAL_ID').value = '';
            document.getElementById('txtFEDERAL_ID').style.display = 'none';
            //				document.getElementById('rfvFEDERAL_ID').style.display = 'none';
            //				document.getElementById("rfvFEDERAL_ID").setAttribute('enabled',false);
            document.getElementById('revFEDERAL_ID').style.display = 'none';
            document.getElementById("revFEDERAL_ID").setAttribute('enabled', false);
            document.getElementById("btnFEDERAL_ID").value = Edit;

            //document.getElementById('txtSSN_NO_HID').style.display = 'none';
        }

        function validatCPF_CNPJ(objSource, objArgs) {
            //get error message for xml on culture base. 
            var cpferrormsg = '<%=javasciptCPFmsg %>';
            var cnpjerrormsg = '<%=javasciptCNPJmsg %>';
            var CPF_invalid = '<%=CPF_invalid %>';
            var CNPJ_invalid = '<%=CNPJ_invalid %>';
            document.getElementById("revBROKER_CPF_CNPJ").innerText = document.getElementById("revBROKER_CPF_CNPJ").getAttribute("ErrMsgcpf");
            var valid = false;
            var idd = objSource.id;
            var rfvid = idd.replace('csv', 'rev');
            if (document.getElementById(rfvid) != null)
                if (document.getElementById(rfvid).isvalid == true) {
                    var theCPF = document.getElementById(objSource.controltovalidate)
                    var len = theCPF.value.length;
                    if (document.getElementById('cmbBROKER_TYPE').value == '11110') {
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
                    else if (document.getElementById('cmbBROKER_TYPE').value == '11109' || document.getElementById('cmbBROKER_TYPE').value == '14725') {
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

            if (((realval.length == 11) && (realval == 11111111111) || (realval == 22222222222) || (realval == 33333333333) || (realval == 44444444444) || (realval == 55555555555) || (realval == 66666666666) || (realval == 77777777777) || (realval == 88888888888) || (realval == 99999999999) || (realval == 00000000000))) {

                objArgs.IsValid = false;
                objSource.innerHTML = ermsg[1];
                return (false);
            }

            if (!((realval.length == 11) || (realval.length == 14))) {
                objSource.innerHTML = ermsg[1];
                objArgs.IsValid = false;
                return (false);
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
                objSource.innerHTML = ermsg[2];
                objArgs.IsValid = false;
                return (false);
            }

            var chkVal = allNum;
            var prsVal = parseFloat(allNum);
            if (chkVal != "" && !(prsVal > "0")) {
                objSource.innerHTML = ermsg[3];
                objArgs.IsValid = false;
                return (false);
            }

            if (realval.length == 11) {
                var tot = 0;
                for (i = 2; i <= 10; i++)
                    tot += i * parseInt(checkStr.charAt(10 - i));

                if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(9))) {
                    objSource.innerHTML = ermsg[1];
                    objArgs.IsValid = false;
                    return (false);
                }

                tot = 0;

                for (i = 2; i <= 11; i++)
                    tot += i * parseInt(checkStr.charAt(11 - i));
                if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(10))) {
                    objSource.innerHTML = ermsg[1];
                    objArgs.IsValid = false;
                    return (false);
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
                    objSource.innerHTML = ermsg[1];
                    objArgs.IsValid = false;
                    return (false);
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
                    objSource.innerHTML = ermsg[1];
                    objArgs.IsValid = false;
                    return (false);
                }
            }
            return (true);
        }


        function ChkDateOfBirth(objSource, objArgs) {

            if (document.getElementById("revDATE_OF_BIRTH").isvalid == true) {

                var effdate = document.MNT_AGENCY_LIST.txtBROKER_DATE_OF_BIRTH.value;
                objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
            }
            else
                objArgs.IsValid = true;
        }

        function zipcodeval() {

            if (document.getElementById('cmbAGENCY_COUNTRY').options[document.getElementById('cmbAGENCY_COUNTRY').options.selectedIndex].value == '6') {
                document.getElementById('revAGENCY_ZIP').setAttribute('enabled', false);
            }
        }



        function zipcodeval1() {

            if (document.getElementById('cmbAGENCY_COUNTRY').options[document.getElementById('cmbAGENCY_COUNTRY').options.selectedIndex].value == '5') {
                document.getElementById('revAGENCY_ZIP').setAttribute('enabled', true);
            }
        }


        function FormatZipCode(vr) {  //Changes done by Aditya for TFS BUG # 1832

            var vr = new String(vr.toString());
            num = vr.length;
            if (vr != "" && (document.getElementById('cmbAGENCY_COUNTRY').options[document.getElementById('cmbAGENCY_COUNTRY').options.selectedIndex].value == '5') || (document.getElementById('cmbAGENCY_COUNTRY').options[document.getElementById('cmbAGENCY_COUNTRY').options.selectedIndex].value == '6') || (document.getElementById('cmbAGENCY_COUNTRY').options[document.getElementById('cmbAGENCY_COUNTRY').options.selectedIndex].value == '1')) {
                //|| (document.getElementById('cmbMAIL_1099_COUNTRY').options[document.getElementById('cmbMAIL_1099_COUNTRY').options.selectedIndex].value == '5'))) {

                vr = vr.replace(/[-]/g, "");
                num = vr.length;
                if (iLangID == '2') //If the language id is 2 then
                {
                    if (num == 8 && (document.getElementById('cmbM_AGENCY_COUNTRY').options[document.getElementById('cmbM_AGENCY_COUNTRY').options.selectedIndex].value == '5')) {
                        //|| (num == 8 && (document.getElementById('cmbMAIL_1099_COUNTRY').options[document.getElementById('cmbMAIL_1099_COUNTRY').options.selectedIndex].value == '5'))) {
                        vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
                        document.getElementById('revAGENCY_ZIP').setAttribute('enabled', false);
                        //document.getElementById('revMAIL_1099_ZIP').setAttribute('enabled', false);

                    }
                }
                if (iLangID.toString() == "1") //If the language id is 1 then
                {
                    if (num == '9' && (document.getElementById('cmbAGENCY_COUNTRY').options[document.getElementById('cmbAGENCY_COUNTRY').options.selectedIndex].value == '1') || (document.getElementById('cmbAGENCY_COUNTRY').options[document.getElementById('cmbAGENCY_COUNTRY').options.selectedIndex].value == '5') || (document.getElementById('cmbAGENCY_COUNTRY').options[document.getElementById('cmbAGENCY_COUNTRY').options.selectedIndex].value == '6')) {
                        vr = vr.substr(0, 5) + '-' + vr.substr(5, 4);
                        document.getElementById('revAGENCY_ZIP').setAttribute('enabled', false);
                    }
                    document.getElementById('revAGENCY_ZIP').setAttribute('enabled', true);
                }

            }

            return vr;
        }


        function zipcodevalid() {

            if (document.getElementById('cmbM_AGENCY_COUNTRY').options[document.getElementById('cmbM_AGENCY_COUNTRY').options.selectedIndex].value == '6') {
                document.getElementById('revM_AGENCY_ZIP').setAttribute('enabled', false);
            }
        }



        function zipcodevalid1() {

            if (document.getElementById('cmbM_AGENCY_COUNTRY').options[document.getElementById('cmbM_AGENCY_COUNTRY').options.selectedIndex].value == '5') {
                document.getElementById('revM_AGENCY_ZIP').setAttribute('enabled', true);
            }
        }


        function FormatZipCodeformat(vr) {  //Changes done by Aditya for TFS BUG # 1832

            var vr = new String(vr.toString());
            num = vr.length;
            if (vr != "" && (document.getElementById('cmbM_AGENCY_COUNTRY').options[document.getElementById('cmbM_AGENCY_COUNTRY').options.selectedIndex].value == '5') || (document.getElementById('cmbM_AGENCY_COUNTRY').options[document.getElementById('cmbM_AGENCY_COUNTRY').options.selectedIndex].value == '6') || (document.getElementById('cmbM_AGENCY_COUNTRY').options[document.getElementById('cmbM_AGENCY_COUNTRY').options.selectedIndex].value == '1')) {
                //|| (document.getElementById('cmbMAIL_1099_COUNTRY').options[document.getElementById('cmbMAIL_1099_COUNTRY').options.selectedIndex].value == '5'))) {

                vr = vr.replace(/[-]/g, "");
                num = vr.length;
                if (iLangID == '2') //If the language id is 2 then
                {
                    if (num == 8 && (document.getElementById('cmbM_AGENCY_COUNTRY').options[document.getElementById('cmbM_AGENCY_COUNTRY').options.selectedIndex].value == '5')) {
                        //|| (num == 8 && (document.getElementById('cmbMAIL_1099_COUNTRY').options[document.getElementById('cmbMAIL_1099_COUNTRY').options.selectedIndex].value == '5'))) {
                        vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
                        document.getElementById('revM_AGENCY_ZIP').setAttribute('enabled', false);
                        //document.getElementById('revMAIL_1099_ZIP').setAttribute('enabled', false);

                    }
                }
                if (iLangID.toString() == "1") //If the language id is 1 then
                {
                    if (num == '9' && (document.getElementById('cmbM_AGENCY_COUNTRY').options[document.getElementById('cmbM_AGENCY_COUNTRY').options.selectedIndex].value == '1') || (document.getElementById('cmbM_AGENCY_COUNTRY').options[document.getElementById('cmbM_AGENCY_COUNTRY').options.selectedIndex].value == '5') || (document.getElementById('cmbM_AGENCY_COUNTRY').options[document.getElementById('cmbM_AGENCY_COUNTRY').options.selectedIndex].value == '6')) {
                        vr = vr.substr(0, 5) + '-' + vr.substr(5, 4);
                        document.getElementById('revM_AGENCY_ZIP').setAttribute('enabled', false);
                    }
                    document.getElementById('revM_AGENCY_ZIP').setAttribute('enabled', true);
                }

            }

            return vr;
        }


        function OnBrokerTypeChange() {

            // if (document.getElementById('cmbBROKER_TYPE').options.selectedIndex == 0)
            //  document.getElementById('cmbBROKER_TYPE').options.selectedIndex = 1;
            if (document.getElementById('cmbBROKER_TYPE').options[document.getElementById('cmbBROKER_TYPE').options.selectedIndex].value == '11110') {
                //Type is personal
                //Changing error message of validation control


                document.getElementById('trpersonal1').style.display = 'inline';
                document.getElementById('trpersonal2').style.display = 'inline';
                document.getElementById('trpersonal3').style.display = 'inline';

                document.getElementById('rfvBROKER_REGIONAL_ID').setAttribute('enabled', true);
                //  document.getElementById('rfvBROKER_REGIONAL_ID').style.display = "inline";
                document.getElementById('rfvREGIONAL_ID_ISSUANCE').setAttribute('enabled', true);
                // document.getElementById('rfvREGIONAL_ID_ISSUANCE').style.display = "inline";
                document.getElementById('rfvREGIONAL_ID_ISSUE_DATE').setAttribute('enabled', true);
                // document.getElementById('rfvREGIONAL_ID_ISSUE_DATE').style.display = "inline";
                document.getElementById('rfvMARITAL_STATUS').setAttribute('enabled', true);
                // document.getElementById('rfvMARITAL_STATUS').style.display = "inline";
                document.getElementById('rfvGENDER').setAttribute('enabled', true);
                // document.getElementById('rfvGENDER').style.display = "inline";
                document.getElementById('rfvDATE_OF_BIRTH').setAttribute('enabled', true);
                //  document.getElementById('rfvDATE_OF_BIRTH').style.display = "inline";


            }
            else {
                //Type is commercial or Government

                document.getElementById('trpersonal1').style.display = 'none';
                document.getElementById('trpersonal2').style.display = 'none';
                document.getElementById('trpersonal3').style.display = 'none';
                document.getElementById('rfvBROKER_REGIONAL_ID').setAttribute('enabled', false);
                document.getElementById('rfvBROKER_REGIONAL_ID').style.display = "none";
                document.getElementById('rfvREGIONAL_ID_ISSUANCE').setAttribute('enabled', false);
                document.getElementById('rfvREGIONAL_ID_ISSUANCE').style.display = "none";
                document.getElementById('rfvREGIONAL_ID_ISSUE_DATE').setAttribute('enabled', false);
                document.getElementById('rfvREGIONAL_ID_ISSUE_DATE').style.display = "none";
                document.getElementById('rfvMARITAL_STATUS').setAttribute('enabled', false);
                document.getElementById('rfvMARITAL_STATUS').style.display = "none";
                document.getElementById('rfvGENDER').setAttribute('enabled', false);
                document.getElementById('rfvGENDER').style.display = "none";
                document.getElementById('rfvDATE_OF_BIRTH').setAttribute('enabled', false);
                document.getElementById('rfvDATE_OF_BIRTH').style.display = "none";



            }



        }

        function allnumeric(objSource, objArgs) {//debugger
            var numbers = /\d{1,2}\/\d\d?\/\d{4}/;
            var inputxt = document.getElementById('txtREGIONAL_ID_ISSUE_DATE').value;
            if (document.getElementById(objSource.controltovalidate).value.match(numbers)) {
                document.getElementById('revREGIONAL_ID_ISSUE_DATE').setAttribute('enabled', true);
                document.getElementById('cpvREGIONAL_ID_ISSUE_DATE').setAttribute('enabled', true);
                document.getElementById('cpv2REGIONAL_ID_ISSUE_DATE').setAttribute('enabled', true);

                objArgs.IsValid = true;
            }
            else {
                document.getElementById('revREGIONAL_ID_ISSUE_DATE').setAttribute('enabled', false);
                document.getElementById('revREGIONAL_ID_ISSUE_DATE').style.display = 'none';
                document.getElementById('cpvREGIONAL_ID_ISSUE_DATE').setAttribute('enabled', false);
                document.getElementById('cpvREGIONAL_ID_ISSUE_DATE').style.display = 'none';
                document.getElementById('cpv2REGIONAL_ID_ISSUE_DATE').setAttribute('enabled', false);
                document.getElementById('cpv2REGIONAL_ID_ISSUE_DATE').style.display = 'none';
                document.getElementById('csvREGIONAL_ID_ISSUE_DATE').setAttribute('enabled', true);
                document.getElementById('csvREGIONAL_ID_ISSUE_DATE').style.display = 'inline';
                objArgs.IsValid = false;
            }
        } 


    </script>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            $("#txtAGENCY_ZIP").change(function () {

                if (trim($('#txtAGENCY_ZIP').val()) != '') {
                    var ZIPCODE = $("#txtAGENCY_ZIP").val();
                    var COUNTRYID = "5";
                    ZIPCODE = ZIPCODE.replace(/[-]/g, "");
                    PageMethod("GetValidateZipeCode", ["ZIPCODE", ZIPCODE, "COUNTRYID", COUNTRYID], AjaxSucceeded, AjaxFailed); //With parameters
                }
                else {
                    //$("#txtAGENCY_ZIP").val('');
                }
            });


            function PageMethod(fn, paramArray, successFn, errorFn) {

                var pagePath = window.location.pathname;
                //Create list of parameters in the form:  
                //{"paramName1":"paramValue1","paramName2":"paramValue2"}  
                var paramList = '';
                if (paramArray.length > 0) {
                    for (var i = 0; i < paramArray.length; i += 2) {
                        if (paramList.length > 0) paramList += ',';
                        paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';

                        if (paramArray[i] == "ZIPCODE") {
                            $('#hidZIPCODE').val(paramArray[i]);
                        }

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

                var numbers = result.d;
                var Addresse = numbers.split('^'); //added by aditya on 04/08/2011 for itrack - 1183
                if (result.d != "" && Addresse[1] != 'undefined') {
                    $("#cmbAGENCY_STATE").val(Addresse[1]);
                    // $("#hidTEST_STATE_ID").val(Addresse[1]);
                    //$("#hidSTATE_ID").val(Addresse[1]);
                    //  $("#txtCONTACT_ZIP").val(Addresse[2]);
                    $("#txtAGENCY_ADD1").val(Addresse[3] + ' ' + Addresse[4]);
                    $("#txtDISTRICT").val(Addresse[5]);
                    $("#txtAGENCY_CITY").val(Addresse[6]);
                }
                if ($('#hidZIPCODE').val() == "ZIPCODE") {
                    var Address = numbers.split('^');
                    var previousZipcode = $("#txtAGENCY_ZIP").val();
                    var Zipecode = previousZipcode.replace('-', '');

                    if (result.d != "" && Address[1] != undefined) {
                        $("#txtAGENCY_ZIP").val(previousZipcode);

                    }
                    else if (document.getElementById('cmbAGENCY_COUNTRY').options[document.getElementById('cmbAGENCY_COUNTRY').options.selectedIndex].value == '5') {
                        //$("#txtAGENCY_ZIP").val('');
                        //$("#txtAGENCY_ZIP").focus();
                        //  ValidatorEnable(rfvZIP_CODE, true)
                        alert($("#hidZIP_CodeMsg").val());
                        return false;


                    }
                    //$('#hidZIPCODE').val('');
                }
            }


            $("#txtM_AGENCY_ZIP").change(function () {


                if (trim($('#txtM_AGENCY_ZIP').val()) != '') {
                    var ZIPCODE = $("#txtM_AGENCY_ZIP").val();
                    var COUNTRYID = "5";
                    ZIPCODE = ZIPCODE.replace(/[-]/g, "");
                    PageMethodM("GetValidateZipeCode", ["ZIPCODE", ZIPCODE, "COUNTRYID", COUNTRYID], AjaxSucceededM, AjaxFailed); //With parameters
                }
                else {
                    //$("#txtM_AGENCY_ZIP").val('');
                }
            });

            function PageMethodM(fn, paramArray, successFn, errorFn) {


                var pagePath = window.location.pathname;
                //Create list of parameters in the form:  
                //{"paramName1":"paramValue1","paramName2":"paramValue2"}  
                var paramList = '';
                if (paramArray.length > 0) {
                    for (var i = 0; i < paramArray.length; i += 2) {
                        if (paramList.length > 0) paramList += ',';
                        paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';

                        if (paramArray[i] == "ZIPCODE") {
                            $('#hidZIPCODE').val(paramArray[i]);
                        }

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
            function AjaxSucceededM(result) {


                var numbers = result.d;
                var Addresse = numbers.split('^');  //added by aditya on 04/08/2011 for itrack - 1183
                if (result.d != "" && Addresse[1] != 'undefined') {
                    $("#cmbM_AGENCY_STATE").val(Addresse[1]);
                    // $("#hidTEST_STATE_ID").val(Addresse[1]);
                    //$("#hidSTATE_ID").val(Addresse[1]);
                    //  $("#txtCONTACT_ZIP").val(Addresse[2]);
                    $("#txtM_AGENCY_ADD_1").val(Addresse[3] + ' ' + Addresse[4]);
                    //$("#txtDISTRICT").val(Addresse[5]);
                    $("#txtM_AGENCY_CITY").val(Addresse[6]);
                }
                if ($('#hidZIPCODE').val() == "ZIPCODE") {
                    var Address = numbers.split('^');
                    var previousZipcode = $("#txtM_AGENCY_ZIP").val();
                    var Zipecode = previousZipcode.replace('-', '');

                    if (result.d != "" && Address[1] != undefined) {
                        $("#txtM_AGENCY_ZIP").val(previousZipcode);

                    }
                    else if (document.getElementById('cmbM_AGENCY_COUNTRY').options[document.getElementById('cmbM_AGENCY_COUNTRY').options.selectedIndex].value == '5') {

                        //$("#txtM_AGENCY_ZIP").val('');
                        //$("#txtM_AGENCY_ZIP").focus();
                        //  ValidatorEnable(rfvZIP_CODE, true)
                        alert($("#hidZIP_CodeMsg").val());
                        return false;


                    }
                    //$('#hidZIPCODE').val('');
                }
            }
            function AjaxFailed(result) {


            }

            $("#cmbAGENCY_STATE").change(function () {

                document.getElementById('hidAGENCY_STATE').value = document.getElementById("cmbAGENCY_STATE").value;

            })

            $("#cmbM_AGENCY_STATE").change(function () {


                document.getElementById('hidM_AGENCY_STATE').value = document.getElementById("cmbM_AGENCY_STATE").value;

            })



        });


        function ValidateRequired() { 
            var strSystemId = "";            
           strSystemId= "<%=GetSystemId().ToUpper()%>";
           if (strSystemId == "S001" || strSystemId == "SUAT") { //Changed by aditya for tfs # 2688
                return true;
            }
            else {
                return false;
            }


        }
    </script>
</head>
<body oncontextmenu="return true;" leftmargin="0" topmargin="0" onload="populateXML();ApplyColor();ShowHideEFT();ShowHideSweep();EnableDisableLables();">
    <form id="MNT_AGENCY_LIST" method="post" runat="server">
    <p>
        <uc1:addressverification id="AddressVerification1" runat="server">
        </uc1:addressverification></p>
    <table runat="server" id="Agency" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="midcolorc" align="right" colspan="4">
                <asp:Label ID="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr id="trBody" runat="server">
            <td>
                <table width="100%" align="center" border="0">
                    <tbody>
                        <tr>
                            <td class="pageHeader" colspan="4">
                                <asp:Label ID="capMANDATORY_FIELD" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolorc" align="right" colspan="4">
                                <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capAGENCY_TYPE_ID" runat="server"></asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:DropDownList ID="cmbAGENCY_TYPE_ID" runat="server">
                                </asp:DropDownList>                             
                                <asp:RequiredFieldValidator ID="rfvAGENCY_TYPE_ID" runat="server" ControlToValidate="cmbAGENCY_TYPE_ID"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capBROKER_TYPE" runat="server" Text="BROKER_TYPE"></asp:Label><span
                                    class="mandatory" id="spnBROKER_TYPE" runat="server">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:DropDownList ID="cmbBROKER_TYPE" runat="server" onfocus="SelectComboIndex('cmbBROKER_TYPE');">
                                </asp:DropDownList>
                                </br>
                                <asp:RequiredFieldValidator ID="rfvBROKER_TYPE" runat="server" ControlToValidate="cmbBROKER_TYPE"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capAGENCY_DISPLAY_NAME" runat="server"></asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtAGENCY_DISPLAY_NAME" runat="server" MaxLength="75" Width="100%"></asp:TextBox><br>
                                <asp:RequiredFieldValidator ID="rfvAGENCY_DISPLAY_NAME" runat="server" ControlToValidate="txtAGENCY_DISPLAY_NAME"
                                    Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revAGENCY_DISPLAY_NAME"
                                        ControlToValidate="txtAGENCY_DISPLAY_NAME" Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capNUM_AGENCY_CODE" runat="server"></asp:Label><span id="spnNUM_AGENCY_CODE"
                                    runat="server" class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtNUM_AGENCY_CODE" runat="server" size="6" MaxLength="4"></asp:TextBox><br>
                                <asp:RegularExpressionValidator ID="revNUM_AGENCY_CODE" ControlToValidate="txtNUM_AGENCY_CODE"
                                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                                <!--Added By Sibin on 22-09-2008-->
                                <asp:RequiredFieldValidator ID="rfvNUM_AGENCY_CODE" runat="server" ControlToValidate="txtNUM_AGENCY_CODE"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capAGENCY_CODE" runat="server"></asp:Label><span id="spnAGENCY_CODE"
                                    runat="server" class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtAGENCY_CODE" runat="server" size="8" MaxLength="6"></asp:TextBox><br>
                                <asp:RequiredFieldValidator ID="rfvAGENCY_CODE" runat="server" ControlToValidate="txtAGENCY_CODE"
                                    Display="Dynamic" ErrorMessage=""></asp:RequiredFieldValidator><%--Please enter agency code--%>
                                <!--Added By Sibin on 22-09-2008-->
                                <%--Modified by Lalit,03 2011. itrack # 988--%>
                                <asp:CompareValidator ID="cmpvAGENCY_CODE" runat="server" Enabled="false" ControlToCompare="txtNUM_AGENCY_CODE"
                                    ControlToValidate="txtAGENCY_CODE" SetFocusOnError="True" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capAGENCY_COMBINED_CODE" runat="server"></asp:Label><span id="spnAGENCY_COMBINED_CODE"
                                    runat="server" class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtAGENCY_COMBINED_CODE" runat="server" size="8" MaxLength="6"></asp:TextBox><br>
                                <!--Added By Sibin on 22-09-2008-->
                                <asp:RequiredFieldValidator ID="rfvAGENCY_COMBINED_CODE" runat="server" ControlToValidate="txtAGENCY_COMBINED_CODE"
                                    Display="Dynamic" ErrorMessage=""></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capAGENCY_DBA" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtAGENCY_DBA" runat="server" TextMode="MultiLine"></asp:TextBox><br>
                                <asp:RegularExpressionValidator ID="revAGENCY_DBA" ControlToValidate="txtAGENCY_DBA"
                                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capAgencyDecName" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:DropDownList ID="cmbAGENCYNAME" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label runat="server" ID="capBROKER_CPF_CNPJ">BROKER_CPF_CNPJ #</asp:Label><span
                                    id="spnBROKER_CPF_CNPJ" class="mandatory" runat="server">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox runat="server" ID="txtBROKER_CPF_CNPJ" CausesValidation="true" OnBlur="this.value=FormatCPFCNPJ(this.value);ValidatorOnChange();"
                                    AutoCompleteType="Disabled" MaxLength="18" size="25"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator runat="server" ID="rfvBROKER_CPF_CNPJ" ErrorMessage=""
                                    Display="Dynamic" ControlToValidate="txtBROKER_CPF_CNPJ"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revBROKER_CPF_CNPJ" ErrorMessage=""
                                    Display="Dynamic" ControlToValidate="txtBROKER_CPF_CNPJ"></asp:RegularExpressionValidator>
                                <asp:CustomValidator runat="server" ID="csvBROKER_CPF_CNPJ" ErrorMessage="" Display="Dynamic"
                                    ControlToValidate="txtBROKER_CPF_CNPJ" ClientValidationFunction="validatCPF_CNPJ"></asp:CustomValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                &nbsp;
                            </td>
                            <td class="midcolora" width="32%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr id="trpersonal1">
                            <td class="midcolora" width="18%">
                                <asp:Label runat="server" ID="capBROKER_REGIONAL_ID">BROKER_REGIONAL_ID</asp:Label><span
                                    id="spnBROKER_REGIONAL_ID" runat="server" class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox runat="server" AutoCompleteType="Disabled" ID="txtBROKER_REGIONAL_ID"
                                    CausesValidation="true" MaxLength="12"></asp:TextBox><br />
                                <asp:RequiredFieldValidator runat="server" ID="rfvBROKER_REGIONAL_ID" Display="Dynamic"
                                    ErrorMessage="" ControlToValidate="txtBROKER_REGIONAL_ID"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label runat="server" ID="capREGIONAL_ID_ISSUANCE">REGIONAL_ID_ISSUANCE</asp:Label><span
                                    id="spnREGIONAL_ID_ISSUANCE" runat="server" class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox runat="server" AutoCompleteType="Disabled" ID="txtREGIONAL_ID_ISSUANCE"
                                    MaxLength="20"></asp:TextBox><br />
                                <asp:RequiredFieldValidator runat="server" ID="rfvREGIONAL_ID_ISSUANCE" Display="Dynamic"
                                    ControlToValidate="txtREGIONAL_ID_ISSUANCE"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trpersonal2">
                            <td class="midcolora" width="18%">
                                <asp:Label runat="server" ID="capREGIONAL_ID_ISSUE_DATE">REGIONAL_ID_ISSUE_DATE</asp:Label><span
                                    id="spnREGIONAL_ID_ISSUE_DATE" runat="server" class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox runat="server" ID="txtREGIONAL_ID_ISSUE_DATE" AutoCompleteType="Disabled"
                                    CausesValidation="true"></asp:TextBox>
                                <asp:HyperLink ID="hlkREGIONAL_ID_ISSUE_DATE" runat="server" CssClass="HotSpot">
                                    <asp:Image ID="imgREGIONAL_ID_ISSUE_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif">
                                    </asp:Image>
                                </asp:HyperLink><br />
                                <asp:RequiredFieldValidator runat="server" ID="rfvREGIONAL_ID_ISSUE_DATE" Display="Dynamic"
                                    ErrorMessage="" ControlToValidate="txtREGIONAL_ID_ISSUE_DATE"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revREGIONAL_ID_ISSUE_DATE" Display="Dynamic"
                                    ControlToValidate="txtREGIONAL_ID_ISSUE_DATE"></asp:RegularExpressionValidator>
                                <asp:CompareValidator ID="cpv2REGIONAL_ID_ISSUE_DATE" ControlToValidate="txtREGIONAL_ID_ISSUE_DATE"
                                    Display="Dynamic" runat="server" Type="Date" Operator="LessThanEqual" ValueToCompare="<%# DateTime.Today.ToShortDateString()%>"></asp:CompareValidator>
                                <asp:CompareValidator ID="cpvREGIONAL_ID_ISSUE_DATE" ControlToValidate="txtREGIONAL_ID_ISSUE_DATE"
                                    Display="Dynamic" runat="server" ControlToCompare="txtBROKER_DATE_OF_BIRTH" Type="Date"
                                    Operator="GreaterThan"></asp:CompareValidator>
                                <asp:CustomValidator runat="server" ID="csvREGIONAL_ID_ISSUE_DATE" ControlToValidate="txtREGIONAL_ID_ISSUE_DATE"
                                    ClientValidationFunction="allnumeric" Display="Dynamic"></asp:CustomValidator>
                                <%--<asp:comparevalidator id="cpv2REGIONAL_ID_ISSUE_DATE" ControlToValidate="txtREGIONAL_ID_ISSUE_DATE" Display="Dynamic" Runat="server" Type="String"
					Operator="LessThan" ErrorMessage="reg cant be future" 
					ValueToCompare="<%# DateTime.Today.ToShortDateString()%>"></asp:comparevalidator>	--%>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capMARITAL_STATUS" runat="server">Marital Status</asp:Label><span
                                    id="spnMARITAL_STATUS" runat="server" class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:DropDownList ID="cmbMARITAL_STATUS" runat="server" onfocus="SelectComboIndex('cmbMARITAL_STATUS')">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvMARITAL_STATUS" runat="server" ControlToValidate="cmbMARITAL_STATUS"
                                    Display="Dynamic" ErrorMessage=""></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trpersonal3">
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capGENDER" runat="server">Gender</asp:Label><span id="spnGENDER" runat="server"
                                    class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:DropDownList ID="cmbGENDER" runat="server" onfocus="SelectComboIndex('cmbGENDER')">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvGENDER" runat="server" ControlToValidate="cmbGENDER"
                                    Display="Dynamic" ErrorMessage=""></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capDATE_OF_BIRTH" runat="server">DOB</asp:Label><span class="mandatory"
                                    id="spnDOB">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtBROKER_DATE_OF_BIRTH" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:HyperLink
                                    ID="hlkDATE_OF_BIRTH" runat="server" CssClass="HotSpot">
                                    <asp:Image runat="server" ID="imgDATE_OF_BIRTH" Border="0" ImageUrl="../../cmsweb/Images/CalendarPicker.gif">
                                    </asp:Image>
                                </asp:HyperLink><br />
                                <asp:RequiredFieldValidator ID="rfvDATE_OF_BIRTH" runat="server" ControlToValidate="txtBROKER_DATE_OF_BIRTH"
                                    Display="Dynamic" ErrorMessage=""></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revDATE_OF_BIRTH" runat="server" ControlToValidate="txtBROKER_DATE_OF_BIRTH"
                                    Display="Dynamic"></asp:RegularExpressionValidator>
                                <asp:CustomValidator ID="csvDATE_OF_BIRTH" runat="server" ControlToValidate="txtBROKER_DATE_OF_BIRTH"
                                    Display="Dynamic" ClientValidationFunction="ChkDateOfBirth"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="headerEffectSystemParams" colspan="4">
                                <asp:Label ID="capPHYSICAL_ADDRESS" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capAGENCY_ADD1" runat="server"></asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtAGENCY_ADD1" runat="server" MaxLength="70"></asp:TextBox><br>
                                <asp:RequiredFieldValidator ID="rfvAGENCY_ADD1" runat="server" ControlToValidate="txtAGENCY_ADD1"
                                    Display="Dynamic" ErrorMessage=" "></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capAGENCY_ADD2" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtAGENCY_ADD2" runat="server" MaxLength="70"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capAGENCY_CITY" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtAGENCY_CITY" runat="server" MaxLength="40"></asp:TextBox><br>
                                <asp:RegularExpressionValidator ID="revAGENCY_CITY" ControlToValidate="txtAGENCY_CITY"
                                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capAGENCY_COUNTRY" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:DropDownList ID="cmbAGENCY_COUNTRY" onfocus="SelectComboIndex('cmbAGENCY_COUNTRY')"
                                    onchange="javascript:LocationAgencyChanged();" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capDISTRICT" runat="server">DISTRICT</asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtDISTRICT" runat="server" MaxLength="20" size="20"></asp:TextBox>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capNUMBER" runat="server">NUMBER</asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtNUMBER" runat="server" MaxLength="20" size="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capAGENCY_STATE" runat="server"></asp:Label><span class="mandatory"
                                    id="spnAGENCY_STATE" runat="server">*</span>
                            </td>
                            <%--Added for Itrack Issue 5811 on 12 May 09--%>
                            <td class="midcolora" width="32%">
                                <asp:DropDownList ID="cmbAGENCY_STATE" onfocus="SelectComboIndex('cmbAGENCY_STATE')"
                                    runat="server">
                                </asp:DropDownList>
                                </br>
                                <asp:RequiredFieldValidator ID="rfvAGENCY_STATE" ControlToValidate="cmbAGENCY_STATE"
                                    Display="Dynamic" runat="server"></asp:RequiredFieldValidator><%--Added for Itrack Issue 5811 on 12 May 09--%>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capAGENCY_ZIP" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtAGENCY_ZIP" runat="server" MaxLength="9" OnBlur="this.value=FormatZipCode(this.value);ValidatorOnChange();zipcodeval();zipcodeval1();"
                                    size="13"></asp:TextBox>
                                <asp:HyperLink ID="hlkAgencyZipLookup" runat="server" CssClass="HotSpot">
                                    <asp:Image ID="imgAgencyZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif"
                                        ImageAlign="Bottom"></asp:Image>
                                </asp:HyperLink><br>
                                <asp:RegularExpressionValidator ID="revAGENCY_ZIP" ControlToValidate="txtAGENCY_ZIP"
                                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                                <br>
                                <asp:RegularExpressionValidator ID="revAGENCY_ZIP_NUMERIC" ControlToValidate="txtAGENCY_ZIP"
                                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capAGENCY_PHONE" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtAGENCY_PHONE" runat="server" size="19" MaxLength="15" onblur="FormatBrazilPhone()"></asp:TextBox><br>
                                <asp:RegularExpressionValidator ID="revAGENCY_PHONE" ControlToValidate="txtAGENCY_PHONE"
                                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capAGENCY_EXT" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtAGENCY_EXT" runat="server" size="19" onblur="FormatBrazilPhone()"
                                    MaxLength="15"></asp:TextBox><br>
                                <asp:RegularExpressionValidator ID="revAGENCY_EXT" ControlToValidate="txtAGENCY_EXT"
                                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capAGENCY_FAX" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtAGENCY_FAX" runat="server" size="19" MaxLength="15" onblur="FormatBrazilPhone()"></asp:TextBox><br>
                                <asp:RegularExpressionValidator ID="revAGENCY_FAX" ControlToValidate="txtAGENCY_FAX"
                                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capAGENCY_SPEED_DIAL" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtAGENCY_SPEED_DIAL" runat="server" size="4" MaxLength="4"></asp:TextBox><br>
                                <asp:RegularExpressionValidator ID="revAGENCY_SPEED_DIAL" ControlToValidate="txtAGENCY_SPEED_DIAL"
                                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="headerEffectSystemParams" style="height: 21px" colspan="4">
                                <asp:Label ID="capMAILING_ADDRESS" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr >
                            <td class="midcolora" width="18%">
                                <asp:Label ID="lblCopy_Address" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <cmsb:CmsButton class="clsButton" ID="btnCopyPhysicalAddress" runat="server"></cmsb:CmsButton>
                            </td>
            </td>
            <td class="midcolora" colspan="2">
            </td>
       </tr>
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capM_AGENCY_ADD_1" runat="server"></asp:Label><span class="mandatory">*</span>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtM_AGENCY_ADD_1" runat="server" MaxLength="70"></asp:TextBox><br>
                <asp:RequiredFieldValidator ID="rfvM_AGENCY_ADD_1" runat="server" ControlToValidate="txtM_AGENCY_ADD_1"
                    Display="Dynamic" ErrorMessage=""></asp:RequiredFieldValidator>
            </td>
            <%--Aviation Risk Info--%>
            <td class="midcolora" width="18%">
                <asp:Label ID="capM_AGENCY_ADD_2" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtM_AGENCY_ADD_2" runat="server" MaxLength="70"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capM_AGENCY_CITY" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtM_AGENCY_CITY" runat="server" MaxLength="40"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revM_AGENCY_CITY" ControlToValidate="txtM_AGENCY_CITY"
                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="capM_AGENCY_COUNTRY" runat="server"></asp:Label><!--<span class="mandatory">*</span>-->
            </td>
            <td class="midcolora" width="32%">
                <asp:DropDownList ID="cmbM_AGENCY_COUNTRY" onfocus="SelectComboIndex('cmbM_AGENCY_COUNTRY')"
                    onchange="javascript:M_AGENCY_COUNTRYChanged();" runat="server">
                </asp:DropDownList>
                <br>
                <asp:RequiredFieldValidator ID="rfvM_AGENCY_COUNTRY" runat="server" ControlToValidate="cmbM_AGENCY_COUNTRY"
                    Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capM_AGENCY_STATE" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:DropDownList ID="cmbM_AGENCY_STATE" onfocus="SelectComboIndex('cmbM_AGENCY_STATE')"
                    runat="server">
                </asp:DropDownList>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="capM_AGENCY_ZIP" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtM_AGENCY_ZIP" runat="server" MaxLength="9" OnBlur="this.value=FormatZipCodeformat(this.value);ValidatorOnChange();zipcodevalid();zipcodevalid1();"
                    size="13"></asp:TextBox><asp:HyperLink ID="hlkAgencyMailZipLookup" runat="server"
                        CssClass="HotSpot">
                        <asp:Image ID="imgAgencyMailZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif"
                            ImageAlign="Bottom"></asp:Image>
                    </asp:HyperLink><br>
                <asp:RegularExpressionValidator ID="revM_AGENCY_ZIP" ControlToValidate="txtM_AGENCY_ZIP"
                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                <br>
                <asp:RegularExpressionValidator ID="revM_AGENCY_ZIP_NUMERIC" ControlToValidate="txtM_AGENCY_ZIP"
                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capALLOWS_EFT" runat="server"></asp:Label><span class="mandatory"
                    id="spnALLOWS_EFT">*</span>
            </td>
            <td class="midcolora" width="32%">
                <asp:DropDownList ID="cmbALLOWS_EFT" onfocus="SelectComboIndex('cmbALLOWS_EFT')"
                    runat="server" onchange="ShowHideEFT();">
                </asp:DropDownList>
                <br>
                <asp:RequiredFieldValidator ID="rfvALLOWS_EFT" ControlToValidate="cmbALLOWS_EFT"
                    Display="Dynamic" runat="server" ErrorMessage=""></asp:RequiredFieldValidator>
            </td>
            <%--Please select EFT--%>
            <td class="midcolora" width="18%">
                <asp:Label ID="capALLOWS_CUSTOMER_SWEEP" runat="server"></asp:Label><span class="mandatory"
                    id="spnALLOWS_CUSTOMER_SWEEP" runat="server">*</span>
            </td>
            <td class="midcolora" width="32%">
                <asp:DropDownList ID="cmbALLOWS_CUSTOMER_SWEEP" onfocus="SelectComboIndex('cmbALLOWS_CUSTOMER_SWEEP')"
                    runat="server" onchange="ShowHideSweep();">
                </asp:DropDownList>
                <br>
            </td>
        </tr>
        <!--EFT/BANK INFORMATION :S-->
        <tr>
            <td class="headerEffectSystemParams" style="height: 21px" colspan="4">
                <asp:Label ID="capEFT_BANK_INFORMATION" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="midcolora" style="height: 21px" colspan="4">
                <asp:Label ID="capACCOUNT_FOR_AGENCY_MSG" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capBANK_NAME" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtBANK_NAME" runat="server" size="23"></asp:TextBox><asp:Label
                    ID="lbl_BANK_NAME" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                <asp:RegularExpressionValidator ID="revBANK_NAME" runat="server" ControlToValidate="txtBANK_NAME"
                    Display="Dynamic" ErrorMessage=""></asp:RegularExpressionValidator>
            </td>
            <%--RegularExpressionValidator--%>
            <td class="midcolora" width="18%">
                <asp:Label ID="capBANK_BRANCH" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtBANK_BRANCH" runat="server"></asp:TextBox><asp:Label ID="lbl_BANK_BRANCH"
                    runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                <asp:RegularExpressionValidator ID="revBANK_BRANCH" runat="server" ControlToValidate="txtBANK_BRANCH"
                    Display="Dynamic" ErrorMessage=""></asp:RegularExpressionValidator>
            </td>
            <%--RegularExpressionValidator--%>
        </tr>
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capBANK_ACCOUNT_NUMBER" runat="server"></asp:Label><span class="mandatory"
                    id="spnBANK_ACCOUNT_NUMBER">*</span>
            </td>
            <td class="midcolora" width="32%">
                <asp:Label ID="capBANK_ACCOUNT_NUMBER_HID" runat="server" size="25" maxlength="11"></asp:Label>
                <input class="clsButton" id="btnBANK_ACCOUNT_NUMBER" size="15" text="Edit" onclick="BANK_ACCOUNT_NUMBER_change(0);"
                    type="button"></input>
                <asp:TextBox ID="txtBANK_ACCOUNT_NUMBER" runat="server" MaxLength="17" size="23"></asp:TextBox><asp:Label
                    ID="lbl_BANK_ACCOUNT_NUMBER" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                <asp:RequiredFieldValidator ID="rfvBANK_ACCOUNT_NUMBER" ControlToValidate="txtBANK_ACCOUNT_NUMBER"
                    Display="Dynamic" runat="server" ErrorMessage=" "></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="csvDFI_ACC_NO" ControlToValidate="txtBANK_ACCOUNT_NUMBER"
                    Display="Dynamic" runat="server" ErrorMessage="" ClientValidationFunction="ValidateDFIAcct"></asp:CustomValidator>
                <asp:RegularExpressionValidator ID="revBANK_ACCOUNT_NUMBER" runat="server" ControlToValidate="txtBANK_ACCOUNT_NUMBER"
                    Display="Dynamic" ErrorMessage=""></asp:RegularExpressionValidator><%--RegularExpressionValidator--%>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="capROUTING_NUMBER" runat="server"></asp:Label><span class="mandatory"
                    id="spnROUTING_NUMBER" runat="server">*</span>
            </td>
            <%--<asp:textbox id="txtROUTING_NUMBER" runat="server" Maxlength="9" size="23"></asp:textbox><BR>
						<asp:requiredfieldvalidator id="rfvROUTING_NUMBER" Display="Dynamic" ControlToValidate="txtROUTING_NUMBER" Runat="server"></asp:requiredfieldvalidator>--%>
            <td class="midcolora" width="32%">
                <asp:Label ID="capROUTING_NUMBER_HID" runat="server" size="25" maxlength="11"></asp:Label><input
                    class="clsButton" id="btnROUTING_NUMBER" size="15" text="Edit" onclick="ROUTING_NUMBER_change(0);"
                    type="button"></input><asp:TextBox ID="txtROUTING_NUMBER" runat="server" MaxLength="9"></asp:TextBox><asp:Label
                        ID="lbl_ROUTING_NUMBER" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                <asp:RequiredFieldValidator ID="rfvROUTING_NUMBER" ControlToValidate="txtROUTING_NUMBER"
                    Display="Dynamic" runat="server" ErrorMessage=" "></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="csvROUTING_NUMBER" ControlToValidate="txtROUTING_NUMBER"
                    Display="Dynamic" runat="server" ErrorMessage="Number starting with 5 is invalid."
                    ClientValidationFunction="ValidateTranNo"></asp:CustomValidator>
                <asp:CustomValidator ID="csvVERIFY_ROUTING_NUMBER" ControlToValidate="txtROUTING_NUMBER"
                    Display="Dynamic" runat="server" ErrorMessage="Please Verify the 9th Digit (Check digit)."
                    ClientValidationFunction="VerifyTranNo"></asp:CustomValidator>
                <asp:CustomValidator ID="csvVERIFY_ROUTING_NUMBER_LENGHT" ControlToValidate="txtROUTING_NUMBER"
                    Display="Dynamic" runat="server" ErrorMessage="length has to be exactly 8/9 digits."
                    ClientValidationFunction="ValidateTranNolength"></asp:CustomValidator><asp:RegularExpressionValidator
                        ID="revROUTING_NUMBER" ControlToValidate="txtROUTING_NUMBER" Display="Dynamic"
                        runat="server" ErrorMessage="Please Enter Valid Transit Number."></asp:RegularExpressionValidator>
            </td>
            <%--<asp:regularexpressionvalidator id="revROUTING_NUMBER" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
						ControlToValidate="txtROUTING_NUMBER"></asp:regularexpressionvalidator></TD>
						<asp:customvalidator CssClass="errmag" id="cvROUTING_NUMBER" runat="server" Display="Dynamic" ErrorMessage="Wrong Entry"
							clientvalidationfunction="VerifyTranNo" ControlToValidate="txtROUTING_NUMBER"></asp:customvalidator>--%>
        </tr>
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capVARIFIED1" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="lblVARIFIED1" runat="server" CssClass="LabelFont">-N.A.-</asp:Label>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="capVARIFIED_DATE1" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="lblVARIFIED_DATE1" runat="server" CssClass="LabelFont">-N.A.-</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capREASON1" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="lblREASON1" runat="server" CssClass="LabelFont">-N.A.-</asp:Label>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="capREVERIFIED_AC1" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:CheckBox ID="chkREVERIFIED_AC1" runat="server"></asp:CheckBox><asp:Label ID="lbl_REVERIFIED_AC1"
                    runat="server" CssClass="LabelFont">-N.A.-</asp:Label>
            </td>
        </tr>
        <!-- Add By kranti -->
        <!-- For Account Type -->
        <tr>
            <td class="midcolora">
                <asp:Label ID="capACCOUNT_TYPE" runat="server"> Account Type 1:</asp:Label>
            </td>
            <td class="midcolora" id="tdACC_CASH_ACC_TYPE_2">
                <asp:RadioButton ID="rdbACC_CASH_ACC_TYPEO" runat="server" Text="" GroupName="ACC_CASH_ACC_TYPE"
                    Checked="True"></asp:RadioButton><asp:Label ID="capRAD_CHK" runat="server"></asp:Label><%--Checking--%>
                <asp:RadioButton ID="rdbACC_CASH_ACC_TYPET" runat="server" Text="" GroupName="ACC_CASH_ACC_TYPE">
                </asp:RadioButton><asp:Label ID="capRAD_SAV" runat="server"></asp:Label><%--Saving--%>
                <td class="midcolora" width="18%">
                    <asp:Label ID="lblACC_CASH_ACC_TYPE_2" runat="server" CssClass="LabelFont">-N.A.-</asp:Label>
                </td>
                <td class="midcolora" colspan="2">
                </td>
        </tr>
        <!-- For second Bank Information-->
        <tr>
            <td class="midcolora" style="height: 21px" colspan="4">
                <asp:Label ID="capACCOUNT" runat="server"></asp:Label>
            </td>
            <%--Account For DB Payments 	Sweep--%>
        </tr>
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capBANK_NAME_2" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtBANK_NAME_2" runat="server" size="23"></asp:TextBox><asp:Label
                    ID="lbl_BANK_NAME_2" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                <asp:RegularExpressionValidator ID="revBANK_NAME_2" runat="server" ControlToValidate="txtBANK_NAME_2"
                    Display="Dynamic" ErrorMessage=""></asp:RegularExpressionValidator>
            </td>
            <%--RegularExpressionValidator--%>
            <td class="midcolora" width="18%">
                <asp:Label ID="capBANK_BRANCH_2" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtBANK_BRANCH_2" runat="server"></asp:TextBox><asp:Label ID="lbl_BANK_BRANCH_2"
                    runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                <asp:RegularExpressionValidator ID="revBANK_BRANCH_2" runat="server" ControlToValidate="txtBANK_BRANCH_2"
                    Display="Dynamic" ErrorMessage=""></asp:RegularExpressionValidator>
            </td>
            <%--RegularExpressionValidator--%>
        </tr>
        <!-- End Add By kranti -->
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capBANK_ACCOUNT_NUMBER1" runat="server"></asp:Label><span class="mandatory"
                    id="spnBANK_ACCOUNT_NUMBER1">*</span>
            </td>
            <td class="midcolora" width="32%">
                <asp:Label ID="capBANK_ACCOUNT_NUMBER1_HID" runat="server" size="25" maxlength="11"></asp:Label><input
                    class="clsButton" id="btnBANK_ACCOUNT_NUMBER1" size="15" text="Edit" onclick="BANK_ACCOUNT_NUMBER1_change(0);"
                    type="button"></input><asp:TextBox ID="txtBANK_ACCOUNT_NUMBER1" runat="server" MaxLength="17"
                        size="23"></asp:TextBox><asp:Label ID="lbl_BANK_ACCOUNT_NUMBER1" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                <asp:RequiredFieldValidator ID="rfvBANK_ACCOUNT_NUMBER1" ControlToValidate="txtBANK_ACCOUNT_NUMBER1"
                    Display="Dynamic" runat="server" ErrorMessage=" "></asp:RequiredFieldValidator><br>
                <asp:CustomValidator ID="csvDFI_ACC_NO1" ControlToValidate="txtBANK_ACCOUNT_NUMBER1"
                    Display="Dynamic" runat="server" ErrorMessage="No space allowed in between the numbers."
                    ClientValidationFunction="ValidateDFIAcct1"></asp:CustomValidator><asp:RegularExpressionValidator
                        ID="revBANK_ACCOUNT_NUMBER1" runat="server" ControlToValidate="txtBANK_ACCOUNT_NUMBER1"
                        Display="Dynamic" ErrorMessage=""></asp:RegularExpressionValidator><%--RegularExpressionValidator--%>
            </td>
            <td class="midcolora" width="20%">
                <asp:Label ID="capROUTING_NUMBER1" runat="server"></asp:Label><span class="mandatory"
                    id="spnROUTING_NUMBER1">*</span>
            </td>
            <%-- --%>
            <td class="midcolora" width="32%">
                <asp:Label ID="capROUTING_NUMBER1_HID" runat="server" size="25" maxlength="11"></asp:Label><input
                    class="clsButton" id="btnROUTING_NUMBER1" size="15" text="Edit" onclick="ROUTING_NUMBER1_change(0);"
                    type="button"></input><asp:TextBox ID="txtROUTING_NUMBER1" runat="server" MaxLength="9"></asp:TextBox><asp:Label
                        ID="lbl_ROUTING_NUMBER1" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                <asp:RequiredFieldValidator ID="rfvROUTING_NUMBER1" ControlToValidate="txtROUTING_NUMBER1"
                    Display="Dynamic" runat="server" ErrorMessage=" "></asp:RequiredFieldValidator><br>
                <asp:CustomValidator ID="csvROUTING_NUMBER1" ControlToValidate="txtROUTING_NUMBER1"
                    Display="Dynamic" runat="server" ErrorMessage="" ClientValidationFunction="ValidateTranNo1"></asp:CustomValidator>
                <asp:CustomValidator ID="csvVERIFY_ROUTING_NUMBER1" ControlToValidate="txtROUTING_NUMBER1"
                    Display="Dynamic" runat="server" ErrorMessage="" ClientValidationFunction="VerifyTranNo1"></asp:CustomValidator>
                <asp:CustomValidator ID="csvVERIFY_ROUTING_NUMBER_LENGHT1" ControlToValidate="txtROUTING_NUMBER1"
                    Display="Dynamic" runat="server" ErrorMessage="" ClientValidationFunction="ValidateTranNolength1"></asp:CustomValidator>
                <asp:RegularExpressionValidator ID="revROUTING_NUMBER1" ControlToValidate="txtROUTING_NUMBER1"
                    Display="Dynamic" runat="server" ErrorMessage=""></asp:RegularExpressionValidator>
            </td>
            <%--<TD class="midcolora" width="32%"><asp:textbox id="txtROUTING_NUMBER1" runat="server" Maxlength="9" size="23"></asp:textbox><BR>
						<asp:requiredfieldvalidator id="rfvROUTING_NUMBER1" Display="Dynamic" ControlToValidate="txtROUTING_NUMBER1" Runat="server"></asp:requiredfieldvalidator>
					<asp:regularexpressionvalidator id="revROUTING_NUMBER1" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
						ControlToValidate="txtROUTING_NUMBER1"></asp:regularexpressionvalidator></TD>
						<asp:customvalidator  id="cvROUTING_NUMBER1" runat="server" CssClass="errmag" ErrorMessage="Wrong Entry" Display="Dynamic"
							clientvalidationfunction="VerifyTranNo1" ControlToValidate="txtROUTING_NUMBER1"></asp:customvalidator></TD>--%>
        </tr>
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capVARIFIED2" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="lblVARIFIED2" runat="server" CssClass="LabelFont">-N.A.-</asp:Label>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="capVARIFIED_DATE2" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="lblVARIFIED_DATE2" runat="server" CssClass="LabelFont">-N.A.-</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capREASON2" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="lblREASON2" runat="server" CssClass="LabelFont">-N.A.-</asp:Label>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="capREVERIFIED_AC2" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:CheckBox ID="chkREVERIFIED_AC2" runat="server"></asp:CheckBox><asp:Label ID="lbl_REVERIFIED_AC2"
                    runat="server" CssClass="LabelFont">-N.A.-</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="midcolora">
                <asp:Label ID="capACCOUNT_TYPE_2" runat="server"> Account Type 2:</asp:Label>
            </td>
            <td class="midcolora" id="tdACC_CASH_ACC_TYPE">
                <asp:RadioButton ID="rdbACC_CASH_ACC_TYPEO_2" runat="server" Text="" GroupName="ACC_CASH_ACC_TYPE_2"
                    Checked="True"></asp:RadioButton><asp:RadioButton ID="rdbACC_CASH_ACC_TYPET_2" runat="server"
                        Text="" GroupName="ACC_CASH_ACC_TYPE_2"></asp:RadioButton>
            </td>
            <td class="midcolora" width="18%" id="tdACC_CASH_ACC">
                <asp:Label ID="lblACC_CASH_ACC_TYPE" runat="server" CssClass="LabelFont">-N.A.-</asp:Label>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="capBROKER_BANK_NUMBER" runat="server">BROKER_BANK_NUMBER</asp:Label><span
                    id="spnBROKER_BANK_NUMBER" runat="server" class="mandatory">*</span>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtBROKER_BANK_NUMBER" runat="server" MaxLength="10" size="20"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvBROKER_BANK_NUMBER" runat="server" Display="Dynamic"
                    ControlToValidate="txtBROKER_BANK_NUMBER" ErrorMessage=""></asp:RequiredFieldValidator>
            </td>
        </tr>
        <!--EFT/BANK INFORMATION :E-->
        <!--VARIFICATION :S-->
        <%--<tr>
					<TD class="midcolora" width="18%"><asp:label id="capVARIFIED1" runat="server"></asp:label></TD>
					<TD class="midcolora" width="18%"><asp:label id="lblVARIFIED1" runat="server" CssClass="LabelFont">-N.A.-</asp:label></TD>
					<TD class="midcolora" width="18%"><asp:label id="capVARIFIED2" runat="server"></asp:label></TD>
					<TD class="midcolora" width="18%"><asp:label id="lblVARIFIED2" runat="server" CssClass="LabelFont">-N.A.-</asp:label></TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capVARIFIED_DATE1" runat="server"></asp:label></TD>
					<TD class="midcolora" width="18%"><asp:label id="lblVARIFIED_DATE1" runat="server" CssClass="LabelFont">-N.A.-</asp:label></TD>
					<TD class="midcolora" width="18%"><asp:label id="capVARIFIED_DATE2" runat="server"></asp:label></TD>
					<TD class="midcolora" width="18%"><asp:label id="lblVARIFIED_DATE2" runat="server" CssClass="LabelFont">-N.A.-</asp:label></TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capREASON1" runat="server"></asp:label></TD>
					<TD class="midcolora" width="18%"><asp:label id="lblREASON1" runat="server" CssClass="LabelFont">-N.A.-</asp:label></TD>
					<TD class="midcolora" width="18%"><asp:label id="capREASON2" runat="server"></asp:label></TD>
					<TD class="midcolora" width="18%"><asp:label id="lblREASON2" runat="server" CssClass="LabelFont">-N.A.-</asp:label></TD>
				</tr>--%>
        <!--VARIFICATION :E-->
        <tr>
            <td class="headerEffectSystemParams" style="height: 21px" colspan="4">
                <asp:Label ID="capOTHER_INFORMATION" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capAGENCY_EMAIL" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtAGENCY_EMAIL" runat="server" MaxLength="50"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revAGENCY_EMAIL" ControlToValidate="txtAGENCY_EMAIL"
                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="capAGENCY_WEBSITE" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtAGENCY_WEBSITE" runat="server" MaxLength="70"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revAGENCY_WEBSITE" ControlToValidate="txtAGENCY_WEBSITE"
                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr id="trAGENCY_PAYMENT_METHOD">
            <td class="midcolora" width="18%">
                <asp:Label ID="capAGENCY_PAYMENT_METHOD" runat="server"></asp:Label><span class="mandatory">*</span>
            </td>
            <td class="midcolora" width="32%">
                <asp:DropDownList ID="cmbAGENCY_PAYMENT_METHOD" onfocus="SelectComboIndex('cmbAGENCY_PAYMENT_METHOD')"
                    runat="server">
                </asp:DropDownList>
                <br>
                <asp:RequiredFieldValidator ID="rfvAGENCY_PAYMENT_METHOD" runat="server" ControlToValidate="cmbAGENCY_PAYMENT_METHOD"
                    Display="Dynamic" Enabled="False"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capAGENCY_BILL_TYPE" runat="server"></asp:Label><span class="mandatory">*</span>
            </td>
            <td class="midcolora" width="32%">
                <asp:DropDownList ID="cmbAGENCY_BILL_TYPE" onfocus="SelectComboIndex('cmbAGENCY_BILL_TYPE')"
                    runat="server">
                </asp:DropDownList>
                <br>
                <asp:RequiredFieldValidator ID="rfvAGENCY_BILL_TYPE" runat="server" ControlToValidate="cmbAGENCY_BILL_TYPE"
                    Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="capAGENCY_LIC_NUM" runat="server"></asp:Label><span id="spnAGENCY_LIC_NUM"
                    runat="server" class="mandatory">*</span>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtAGENCY_LIC_NUM" runat="server" size="4" MaxLength="4"></asp:TextBox><br>
                <asp:RequiredFieldValidator ID="rfvAGENCY_LIC_NUM" runat="server" ControlToValidate="txtAGENCY_LIC_NUM"
                    Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revAGENCY_LIC_NUM"
                        ControlToValidate="txtAGENCY_LIC_NUM" Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <!--START- NEW FIELDS ADDED-->
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capPRINCIPAL_CONTACT" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtPRINCIPAL_CONTACT" runat="server" size="17" MaxLength="50"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revPRINCIPAL_CONTACT" runat="server" ControlToValidate="txtPRINCIPAL_CONTACT"
                    Display="Dynamic" ErrorMessage=""></asp:RegularExpressionValidator>
            </td>
            <%--RegularExpressionValidator--%>
            <td class="midcolora" width="18%">
                <asp:Label ID="capOTHER_CONTACT" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtOTHER_CONTACT" runat="server" MaxLength="50"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revOTHER_CONTACT" runat="server" ControlToValidate="txtOTHER_CONTACT"
                    Display="Dynamic" ErrorMessage=""></asp:RegularExpressionValidator>
            </td>
            <%--RegularExpressionValidator--%>
        </tr>
        <tr runat="server" id="ttrFEDERAL_ID">
            <td class="midcolora" width="18%">
                <asp:Label ID="capFEDERAL_ID" runat="server"></asp:Label>
            </td>
            <td class="midcolora" id="ttdFEDERAL_ID" runat="server" width="32%">
                <asp:Label ID="capFEDERAL_ID_HID" runat="server" maxlength="9" size="11"></asp:Label><input
                    class="clsButton" id="btnFEDERAL_ID" size="15" text="Edit" onclick="FEDERAL_ID_change();"
                    type="button"></input>
                <asp:TextBox ID="txtFEDERAL_ID" runat="server" MaxLength="9" size="11" AutoComplete="Off"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revFEDERAL_ID" runat="server" ControlToValidate="txtFEDERAL_ID"
                    Display="Dynamic" ErrorMessage=""></asp:RegularExpressionValidator>
            </td>
            <%--RegularExpressionValidator--%>
            <td class="midcolora" width="18%">
                <asp:Label ID="capORIGINAL_CONTRACT_DATE" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtORIGINAL_CONTRACT_DATE" runat="server" MaxLength="10" size="12"></asp:TextBox><asp:HyperLink
                    ID="hlkORIGINAL_CONTRACT_DATE" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgORIGINAL_CONTRACT_DATE" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif">
                    </asp:Image>
                </asp:HyperLink><asp:RegularExpressionValidator ID="revORIGINAL_CONTRACT_DATE" ControlToValidate="txtORIGINAL_CONTRACT_DATE"
                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator><asp:CustomValidator
                        ID="csvORIGINAL_CONTRACT_DATE" ControlToValidate="txtORIGINAL_CONTRACT_DATE"
                        Display="Dynamic" runat="server" ClientValidationFunction="ChkDate"></asp:CustomValidator>
            </td>
        </tr>
        <tr id="rowTermination" runat="server">
            <td class="midcolora" width="18%">
                <asp:Label ID="capCURRENT_CONTRACT_DATE" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtCURRENT_CONTRACT_DATE" runat="server" MaxLength="10" size="12"></asp:TextBox><asp:HyperLink
                    ID="hlkCURRENT_CONTRACT_DATE" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgCURRENT_CONTRACT_DATE" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif">
                    </asp:Image>
                </asp:HyperLink><asp:RegularExpressionValidator ID="revCURRENT_CONTRACT_DATE" ControlToValidate="txtCURRENT_CONTRACT_DATE"
                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator><asp:CustomValidator
                        ID="csvCURRENT_CONTRACT_DATE" ControlToValidate="txtCURRENT_CONTRACT_DATE" Display="Dynamic"
                        runat="server" ClientValidationFunction=""></asp:CustomValidator>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="capREQ_SPECIAL_HANDLING" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:CheckBox ID="chkREQ_SPECIAL_HANDLING" runat="server"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capTERMINATION_DATE" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtTERMINATION_DATE" runat="server" MaxLength="10" size="12"></asp:TextBox><asp:HyperLink
                    ID="hlkTERMINATION_DATE" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgTERMINATION_DATE" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif">
                    </asp:Image>
                </asp:HyperLink><asp:RegularExpressionValidator ID="revTERMINATION_DATE" ControlToValidate="txtTERMINATION_DATE"
                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="capTERMINATION_DATE_RENEW" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtTERMINATION_DATE_RENEW" runat="server" MaxLength="10" size="12"
                    onblur="ChkNotes();"></asp:TextBox>
                <asp:HyperLink ID="hlkTERMINATION_DATE_RENEW" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgTERMINATION_DATE_RENEW" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif">
                    </asp:Image>
                </asp:HyperLink><asp:RegularExpressionValidator ID="revTERMINATION_DATE_RENEW" ControlToValidate="txtTERMINATION_DATE_RENEW"
                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <!--
					<td class="midcolora" width="18%">
						<asp:label id="capTERMINATION_REASON" runat="server"></asp:label>
					</td>
					<td class="midcolora" width="32%">
						<asp:textbox id="txtTERMINATION_REASON" runat="server" maxlength="10" size="12"></asp:textbox>
					</td> -->
            <td class="midcolora" style="height: 38px" width="18%">
                <asp:Label ID="capTERMINATION_NOTICE" runat="server">Termination Notice</asp:Label><span
                    class="mandatory" id="spnTERMINATION_NOTICE">*</span>
            </td>
            <td class="midcolora" style="height: 38px" width="32%">
                <asp:DropDownList ID="cmbTERMINATION_NOTICE" runat="server" onfocus="SelectComboIndex('cmbTERMINATION_NOTICE');"
                    onChange="ChkNotes();">
                    <asp:ListItem Value=''></asp:ListItem>
                    <asp:ListItem Value='N'>No</asp:ListItem>
                    <asp:ListItem Value='Y'>Yes</asp:ListItem>
                </asp:DropDownList>
                <br>
                <asp:RequiredFieldValidator ID="rfvTERMINATION_NOTICE" runat="server" ControlToValidate="cmbTERMINATION_NOTICE"
                    ErrorMessage="" Display="Dynamic"></asp:RequiredFieldValidator><%--Please select Termination Notice.--%>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="capNOTES" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtNOTES" runat="server" size="23" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="midcolora" style="height: 30px" width="18%">
                <asp:Label ID="capINCORPORATED_LICENSE" runat="server">Incorporated License</asp:Label>
            </td>
            <td class="midcolora" style="height: 30px" width="20%">
                <asp:DropDownList ID="cmbINCORPORATED_LICENSE" runat="server">
                    <asp:ListItem Value=''></asp:ListItem>
                    <asp:ListItem Value='N'>No</asp:ListItem>
                    <asp:ListItem Value='Y'>Yes</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="midcolora" style="height: 30px" width="18%">
                <asp:Label ID="capPROCESS_1099" runat="server">1099 Process</asp:Label><span id="spnPROCESS_1099"
                    runat="server" class="mandatory">*</span>
            </td>
            <td class="midcolora" style="height: 30px" width="20%">
                <asp:DropDownList ID="cmbPROCESS_1099" onfocus="SelectComboIndex('cmbPROCESS_1099')"
                    runat="server">
                </asp:DropDownList>
                <br>
                <asp:RequiredFieldValidator ID="rfvPROCESS_1099" runat="server" ControlToValidate="cmbPROCESS_1099"
                    Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <!--END - NEW FIELDS ADDED-->
        <tr>
            <td class="midcolora">
                <asp:Label ID="capSUSEP_NUMBER" runat="server"></asp:Label>
            </td>
            <td class="midcolora">
                <asp:TextBox ID="txtSUSEP_NUMBER" runat="server" MaxLength="20" size="20"></asp:TextBox>
            </td>
            <td class="midcolora">
                <asp:Label ID="capBROKER_CURRENCY" runat="server" Text="capBROKER_CURRENCY"></asp:Label><span
                    class="mandatory">*</span>
            </td>
            <td class="midcolora">
                <asp:DropDownList ID="cmbBROKER_CURRENCY" runat="server">
                </asp:DropDownList>
                <br />
                <asp:RequiredFieldValidator ID="rfvBROKER_CURRENCY" runat="server" ControlToValidate="cmbBROKER_CURRENCY"
                    Display="Dynamic" ErrorMessage=""></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="midcolora" colspan="2">
                <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" Text="Reset"></cmsb:CmsButton>&nbsp;
                <cmsb:CmsButton class="clsButton" ID="btnActivateDeactivate" runat="server" Text=""
                    CausesValidation="false"></cmsb:CmsButton>
            </td>
            <td class="midcolorr" colspan="2">
                <cmsb:CmsButton class="clsButton" ID="btnDelete" runat="server" Text="Delete" CausesValidation="False"
                    causevalidation="false"></cmsb:CmsButton><cmsb:CmsButton class="clsButton" ID="btnSave"
                        runat="server" Text="Save" OnClientClick="javascript:return Page_ClientValidate()">
                    </cmsb:CmsButton>
            </td>
        </tr>
    </table>
    </TD></TR></TBODY></TABLE><input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved"
        runat="server">
    <input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
    <input id="hidOldData" type="hidden" name="hidOldData" runat="server">
    <input id="Hidden1" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
    <input id="hidAGENCY_ID" type="hidden" value="0" name="hidAGENCY_ID" runat="server">
    <input id="hidAGENCY_CODE" type="hidden" name="hidAGENCY_CODE" runat="server">
    <input id="hidUnderWriter" type="hidden" name="hidUnderWriter" runat="server">
    <input id="hidReset" type="hidden" value="0" name="hidReset" runat="server">
    <input id="hidACCOUNT_TYPE" type="hidden" value="0" name="hidACCOUNT_TYPE" runat="server">
    <input id="hidACCOUNT_TYPE_2" type="hidden" value="0" name="hidACCOUNT_TYPE_2" runat="server">
    <input id="hidAGENCY_NEW" type="hidden" value="0" name="hidAGENCY_NEW" runat="server">
    <input id="hidREVERIFIED_AC1" type="hidden" value="0" name="hidREVERIFIED_AC1" runat="server">
    <input id="hidREVERIFIED_AC2" type="hidden" value="0" name="hidREVERIFIED_AC2" runat="server">
    <input id="hidBANK_ACCOUNT_NUMBER" type="hidden" name="hidBANK_ACCOUNT_NUMBER" runat="server">
    <input id="hidBANK_ACCOUNT_NUMBER1" type="hidden" name="hidBANK_ACCOUNT_NUMBER1"
        runat="server">
    <input id="hidFEDERAL_ID" type="hidden" name="hidFEDERAL_ID" runat="server">
    <input id="hidROUTING_NUMBER" type="hidden" name="hidROUTING_NUMBER" runat="server">
    <input id="hidROUTING_NUMBER1" type="hidden" name="hidROUTING_NUMBER1" runat="server">
    <input id="hidREQ_SPECIAL_HANDLING" type="hidden" value="0" name="hidREQ_SPECIAL_HANDLING"
        runat="server">
    <input type="hidden" runat="server" id="hidTAB_TITLES" value="" name="hidTAB_TITLES" />
    <input type="hidden" runat="server" id="hidNEW_BROKER_DATE_OF_BIRTH" value="" name="hidTAB_TITLES" />
    <input type="hidden" runat="server" id="hidNEWREGIONAL_ID_ISSUE_DATE" value="" name="hidTAB_TITLES" />
    <input type="hidden" runat="server" id="hidNEW_TERMINATION_DATE" value="" name="hidTAB_TITLES" />
    <input type="hidden" runat="server" id="hidNEW_TERMINATION_DATE_RENEW" value="" name="hidTAB_TITLES" />
    <input type="hidden" runat="server" id="hidNEW_ORIGINAL_CONTRACT_DATE" value="" name="hidTAB_TITLES" />
    <input type="hidden" runat="server" id="hidNEW_CURRENT_CONTRACT_DATE" value="" name="hidTAB_TITLES" />
    <input type="hidden" runat="server" id="hidM_AGENCY_STATE" value="" name="hidM_AGENCY_STATE" />
    <input type="hidden" runat="server" id="hidAGENCY_STATE" value="" name="hidAGENCY_STATE" />
    <input type="hidden" runat="server" id="hidZIPCODE" name="hidZIPCODE" />
    <input type="hidden" runat="server" id="hidZIP_CodeMsg" name="hidZIP_CodeMsg" />
    <input type="hidden" runat="server" id="hidtab" name="" />
    <input type="hidden" runat="server" id="hidCancel" name="" />
    <input type="hidden" runat="server" id="hidEdit" name="" />
    </form>
    <script>
		//RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidAGENCY_ID').value,true);
			
			var temp = '<%=strSystemID%>';
			var tab=document.getElementById('hidtab').value;
			if(document.getElementById('hidAGENCY_NEW').value == 'New')
			{
				RemoveTab(1	,top.frames[1]);
				var Url="AddAgency.aspx?NEW_AGENCY_ID=" + document.getElementById('hidAGENCY_NEW').value + "&";
				DrawTab(1,top.frames[1],tab,Url);	
			}
			
			if(temp.toUpperCase() == <%=  "'" + CarrierSystemID.ToUpper() + "'" %> || document.getElementById('hidFormSaved').value=="1")
			{
				RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidAGENCY_ID').value,true);
			}
    </script>
</body>
</html>

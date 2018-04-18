<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="PolicyTop" Src="/cms/cmsweb/webcontrols/PolicyTop.ascx" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>

<%@ Page Language="c#" CodeBehind="EndorsementProcess.aspx.cs" AutoEventWireup="false"
    Inherits="Cms.Policies.Processes.EndorsementProcess" ValidateRequest="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>EndorsementProcess</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet">

    <script src="/cms/cmsweb/scripts/xmldom.js"></script>

    <script src="/cms/cmsweb/scripts/common.js"></script>

    <script src="/cms/cmsweb/scripts/form.js"></script>

    <script src="/cms/cmsweb/scripts/Calendar.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script>

    <script language="javascript" type="text/javascript">
        //Added by Pradeep Kushwaha on 07-March-2011 itrack 441
        //Reason to add this code
        //The system is not accepting dates without format (Mask)
        //ON EFFECTIVE DATE USER CAN ADD 01012011 AND USER WILL ASSUME IT AS 01/01/2011. 
        //HOWEVER, ON EXPIRE DATE IF USER INCLUDES 01012011 SYSTEM DOES NOT ACCEPT AND REQUIRES USER TO INCLUDE THE DATE WITH THE MASK.
        $(document).ready(function() {
            $('body').bind('keydown', function(event) {
                if (event.keyCode == '13') {
                    $("#txtEXPIRY_DATE").blur();
                    $("#btnSave").focus();

                }
            });

        });
        //Added till here 
        //Setting the menu
        //This function will be called after starting the endorsement process
        //using RegisterStartupScript method
        var onPageload = true;
        function setMenu() {

            //IF menu on top frame is not ready then
            //menuXmlReady will false
            //If menu is not ready, we will again call setmenu again after 1 sec
            if (top.topframe.main1.menuXmlReady == false)
                setTimeout('setMenu();', 1000);


            //Enabling or disabling menus
            top.topframe.main1.activeMenuBar = '1';
            top.topframe.createActiveMenu();
            top.topframe.enableMenus('1', 'ALL');
            top.topframe.enableMenu('1,1,1');
            top.topframe.enableMenu('1,2,3');
            top.topframe.enableMenu('1,1,2');
            //top.topframe.disableMenus("1,3","ALL");
            //selectedLOB = lobMenuArr[document.getElementById('hidLOBID').value];
            //top.topframe.enableMenu("1,3," + selectedLOB);

        }

        function AddData() {
            document.getElementById('hidROW_ID').value = 'New';
            document.getElementById('txtCOMMENTS').focus();
            document.getElementById('txtCOMMENTS').value = '';
            document.getElementById('txtEFFECTIVE_DATETIME').value = '';
            document.getElementById('txtEXPIRY_DATE').value = '';
            document.getElementById('chkINTERNAL_CHANGE').checked = false;
            document.getElementById('chkPRINTING_OPTIONS').checked = false;
            document.getElementById('cmbADD_INT').selectedIndex = 0;
            document.getElementById("hidADD_INT_ID").value = '';
            document.getElementById('chkSEND_ALL').checked = false;
            document.getElementById('cmbINSURED').selectedIndex = 0;
            document.getElementById('cmbAGENCY_PRINT').selectedIndex = -1;
            document.getElementById('cmbADVERSE_LETTER_REQD').selectedIndex = -1;
            document.getElementById('txtNO_COPIES').value = '1'; //Done for Itrack Issue 5574

            DisableValidators();
            ChangeColor();
        }
        function populatePageXML() {
            //if(document.getElementById('hidFormSaved').value == '0')
            if (document.getElementById("hidOldData").value != "") {
                var tempXML;

                if (document.getElementById("hidOldData").value != "") {
                    populateFormData(document.getElementById("hidOldData").value, EndorsementProcess);

                    if (document.getElementById('hidEND_EFFECTIVE_DATE').value != '')
                        document.getElementById('txtEFFECTIVE_DATETIME').value = document.getElementById('hidEND_EFFECTIVE_DATE').value;

                    if (document.getElementById('hidEND_EXPIRY_DATE').value != '')
                        document.getElementById('txtEXPIRY_DATE').value = document.getElementById('hidEND_EXPIRY_DATE').value;

                    if (document.getElementById('hidPrinting_Options').value == '1') {
                        document.getElementById('chkPRINTING_OPTIONS').checked = true;
                        onPageload = true;
                    }
                    else {
                        document.getElementById('chkPRINTING_OPTIONS').checked = false;
                        onPageload = false;
                    }
                    PrintOptions();
                    if (document.getElementById('chkINTERNAL_CHANGE').checked == true)
                    { document.getElementById('chkPRINTING_OPTIONS').disabled = true; }
                    //
                    var chk = document.getElementById('chkSEND_ALL');
                    if (chk.checked == true)
                        document.getElementById('trAddIntList').style.display = "none";
                    else
                        document.getElementById('trAddIntList').style.display = "inline";
                    //
                    SetAssignAddInt();
                    var sversion = document.getElementById('hidSOURCE_VERSION_ID').value;
                    var Co_app = document.getElementById('hidCO_APPLICANT_ID').value;
                    var cmbVal = sversion + '^' + Co_app;
                    for (i = 0; i < document.getElementById('cmbSOURCE_VERSION_ID').length; i++) {
                        if (document.getElementById('cmbSOURCE_VERSION_ID').options[i].value == cmbVal) {
                            document.getElementById('cmbSOURCE_VERSION_ID').options[i].selected = true;
                            document.getElementById('cmbSOURCE_VERSION_ID').disabled = true;
                            document.getElementById('rfvSOURCE_VERSION_ID').setAttribute('enabled', false);
                            document.getElementById('rfvSOURCE_VERSION_ID').setAttribute('isValid', true);
                            document.getElementById('rfvSOURCE_VERSION_ID').style.display = 'none';
                            break;
                        }
                    }


                }
                else {
                    AddData();
                }
            }
            else {
                //disable printing options if internal change is checked
                if (document.getElementById('chkINTERNAL_CHANGE').checked == true)
                { document.getElementById('chkPRINTING_OPTIONS').disabled = true; }

                //if additional interest send all then do not display list of add. int
                if (document.getElementById('chkSEND_ALL').checked == true)
                { document.getElementById('trAddIntList').style.display = "none"; }
                else
                { document.getElementById('trAddIntList').style.display = "inline"; SetAssignAddInt(); }

                //if printing option is checked then call print options
                if (document.getElementById('hidPrinting_Options').value == '1') {
                    document.getElementById('chkPRINTING_OPTIONS').checked = true;
                    onPageload = true;
                }
                else {
                    document.getElementById('chkPRINTING_OPTIONS').checked = false;
                    onPageload = false;
                }
                PrintOptions();
                if (document.getElementById('hidLOB_ID').value == '<%=((int)Cms.CmsWeb.cmsbase.enumLOB.AUTOP).ToString()%>' || document.getElementById('hidLOB_ID').value == '<%=((int)Cms.CmsWeb.cmsbase.enumLOB.CYCL).ToString()%>')
                    document.getElementById('txtNO_COPIES').value = '2';
            }
            return false;
        }

        //Validates the maximum length for comments
        function txtCOMMENTS_VALIDATE(source, arguments) {
            var txtArea = arguments.Value;
            if (txtArea.length > 1000) {
                arguments.IsValid = false;
                return false;   // invalid userName
            }
        }
        function onRowClicked(num, msDg) {
            rowNum = num;
            rowNum = num;
            if (parseInt(num) == 0)
                strXML = "";
            populateXML(num, msDg);
            changeTab(0, 0);
        }

        function ShowDetailsPolicy() {
            /*if (document.getElementById("hidNEW_POLICY_VERSION_ID").value == "")
            top.botframe.callItemClicked('1,2,3','/cms/policies/aspx/policytab.aspx?CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VERSION_ID=' + document.getElementById('hidPOLICY_VERSION_ID').value + '&');
            else
            top.botframe.callItemClicked('1,2,3','/cms/policies/aspx/policytab.aspx?CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VERSION_ID=' + document.getElementById('hidNEW_POLICY_VERSION_ID').value + '&');
            */
            if (document.getElementById('hidNEW_POLICY_VERSION_ID').value != "" && document.getElementById('hidNEW_POLICY_VERSION_ID').value != "0")
                window.open('/Cms/Application/Aspx/DecPage.aspx?CALLEDFOR=DECPAGE&CALLEDFROM=POLICY&CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VER_ID=' + document.getElementById('hidNEW_POLICY_VERSION_ID').value + '&');
            else
                window.open('/Cms/Application/Aspx/DecPage.aspx?CALLEDFOR=DECPAGE&CALLEDFROM=POLICY&CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VER_ID=' + document.getElementById('hidPOLICY_VERSION_ID').value + '&');
            return false;
        }

        function cmbADD_INT_Change() {
            combo = document.getElementById('cmbADD_INT');
            if (combo == null || combo.selectedIndex == -1)
                return false;
            //MICHIGAN_MAILERS ITRACK #4068
            if (combo.options[combo.selectedIndex].value == "<%=((int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString()%>"
			  || combo.options[combo.selectedIndex].value == "<%=((int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString()%>") {
                document.getElementById('chkSEND_ALL').style.display = "inline";
                document.getElementById('capSEND_ALL').style.display = "inline";
                if (document.getElementById('chkSEND_ALL').checked == false)
                    document.getElementById('trAddIntList').style.display = "inline";
                chkSEND_ALL_Change();
            }
            else {
                document.getElementById('chkSEND_ALL').style.display = "none";
                document.getElementById('capSEND_ALL').style.display = "none";
                document.getElementById('trAddIntList').style.display = "none";
                //document.getElementById('hidADD_INT_ID').value = '';
            }
            return false;
        }
        function DisplayBody() {
            if (document.getElementById('hidDisplayBody').value == "True") {
                document.getElementById('trBody').style.display = 'inline';
            }
            else {
                document.getElementById('trBody').style.display = 'none';
            }

        }
        function formReset() {
            document.EndorsementProcess.reset();
            populatePageXML();
            DisableValidators();
            ChangeColor();
            return false;

        }
        function Init() {
            //"setfirstTime();populatePageXML();ApplyColor();ChangeColor();top.topframe.main1.mousein = false;findMouseIn();showScroll();"

            setfirstTime();
            //populateXML();
            populatePageXML();

            top.topframe.main1.mousein = false;
            findMouseIn();
            showScroll();
            //DisplayBody();
            SetAssignAddInt();			
            cmbADD_INT_Change();
            //chkSEND_ALL_Change();
            InternalChange();
            ApplyColor();
            ChangeColor();
            doFocus();
            AutoIdCardDisplay();
            DisplayBody();
            //var option = document.getElementById('hidENDORSEMENT_OPTION').value;
            var Status = document.getElementById('hidPOLICY_CURRENT_STATUS').value;
            document.getElementById('btnCommitInProgress').style.display = "none";
            document.getElementById('btnCommitAnywayInProgress').style.display = "none";

            //Added for Itrack Issue 6203 on 31 July 2009
            try {

                if (top.topframe.main1.menuXmlReady == false)
                    if (Status != 'MENDORSE' && '<%=EligibleForProcess %>'.toUpperCase() != 'FALSE')
                    setTimeout("top.topframe.enableMenus('1,7','ALL');", 1000);

                else
                    top.topframe.enableMenus('1,7', 'ALL');

            } catch (err) { }
            //Commented by Lalit remove endorsement option functionality
            //setEndorsementOption();

            EnableCoApplicant();
            openEndorsementsList(0);
            OnChangeEndorsement();

        }
        //co-applicant is mandatory for master policy
        function EnableCoApplicant() {

            var Saved = '<%=Saved %>';
            if (document.getElementById('hidPOLICY_TYPE').value == '<%=Cms.CmsWeb.cmsbase.MASTER_POLICY %>') {
                document.getElementById('trCO_APPLICANT_ID').style.display = "inline";
                document.getElementById('rfvCO_APPLICANT_ID').setAttribute('enable', true);
                if (Saved.toUpperCase() != 'FALSE')
                    if (document.getElementById('cmbCO_APPLICANT_ID').value > 0) {
                    document.getElementById('cmbCO_APPLICANT_ID').disabled = true;
                }
            }
            else {
                document.getElementById('trCO_APPLICANT_ID').style.display = "none";
                document.getElementById('rfvCO_APPLICANT_ID').setAttribute("enabled", false);
                document.getElementById('rfvCO_APPLICANT_ID').setAttribute('isValid', true);
                document.getElementById('rfvCO_APPLICANT_ID').style.display = "none";
            }



        }
        //         function setEndorsementOption() {
        //             var Crcode = document.getElementById("hidCARRIER_CODE").value; //W001
        //             var ALBA = 'ALBA'
        //            var ALBA_UAT = 'ALBAUAT'
        //            if (Crcode == ALBA || Crcode == ALBA_UAT) {
        //                document.getElementById('cmbENDORSEMENT_OPTION').value = "14833";
        //                document.getElementById('trENDORSEMENT_OPTION').style.display = 'none';
        //            } else { document.getElementById('trENDORSEMENT_OPTION').style.display = 'inline'; }
        //         }
        //by pravesh to set Cursor in Date Field
        function doFocus() {
            //document.forms[0].txtEFFECTIVE_DATETIME.focus();
            //document.all('txtEFFECTIVE_DATETIME').focus();return false;
            if (document.getElementById('trBody').style.display == 'inline')
                document.getElementById('txtEFFECTIVE_DATEIME').focus();
        }
        function AssignAddInt(flag) {
            var coll = document.getElementById('cmbUnAssignAddInt');
            var selIndex = coll.options.selectedIndex;
            var len = coll.options.length;

            if (len < 1) return;
            var num = 0;
            for (var i = len - 1; i > -1; i--) {
                if (coll.options(i).selected == true || flag) {
                    num = i;
                    var szSelectedDept = coll.options(i).value;
                    var innerText = coll.options(i).text;
                    document.getElementById('cmbAssignAddInt').options[document.getElementById('cmbAssignAddInt').length] = new Option(innerText, szSelectedDept)
                    coll.remove(i);
                }
            }
            len = coll.options.length;
            if (num < len) {
                document.getElementById('cmbUnAssignAddInt').options(num).selected = true;
            }
            else {
                if (num > 0)
                    document.getElementById('cmbUnAssignAddInt').options(num - 1).selected = true;
            }

        }

        function UnAssignAddInt(flag) {
            var UnassignableString = "";
            var Unassignable = UnassignableString.split(",");
            var gszAssignedString = "";
            var Assigned = gszAssignedString.split(",");
            var coll = document.getElementById('cmbAssignAddInt');
            var selIndex = coll.options.selectedIndex;
            var len = coll.options.length;
            var num = 0;
            if (len == 0) return;
            document.getElementById('chkSEND_ALL').checked = false;
            for (var i = len - 1; i > -1; i--) {
                if (coll.options(i).selected == true || flag) {
                    num = i;
                    var flag = true;
                    var szSelectedDept = coll.options(i).value;
                    var innerText = coll.options(i).text;
                    for (j = 0; j < Unassignable.length; j++) {
                        for (k = 0; k < Assigned.length; k++) {
                            if ((szSelectedDept == Unassignable[j]) && (szSelectedDept == Assigned[k])) {
                                flag = false;
                            }
                        }
                    }

                    if (flag == true && coll.options(i).selected == true) {
                        document.getElementById('cmbUnAssignAddInt').options[document.getElementById('cmbUnAssignAddInt').length] = new Option(innerText, szSelectedDept)
                        coll.remove(i);

                    }
                    /*else
                    {
                    alert("Cannot unassign Department "+innerText+" because some divisions are associasted with it");
                    }*/
                }
            }
            var len = coll.options.length;
            if (len < 1) return;
            if (num < len) {
                document.getElementById('cmbAssignAddInt').options(num).selected = true;
            }
            else {
                document.getElementById('cmbAssignAddInt').options(num - 1).selected = true;
            }

        }
        function UnAssignAddIntchkSEND_ALL(flag) {
            var UnassignableString = "";
            var Unassignable = UnassignableString.split(",");
            var gszAssignedString = "";
            var Assigned = gszAssignedString.split(",");
            var coll = document.getElementById('cmbAssignAddInt');
            var selIndex = coll.options.selectedIndex;
            var len = coll.options.length;
            var num = 0;
            if (len == 0) return;

            for (var i = len - 1; i > -1; i--) {
                //Done for Itrack 8493 - TFS 543 on 10 Sept 2011
                num = i;
                var flag = true;
                var szSelectedDept = coll.options(i).value;
                var innerText = coll.options(i).text;
                for (j = 0; j < Unassignable.length; j++) {
                    for (k = 0; k < Assigned.length; k++) {
                        if ((szSelectedDept == Unassignable[j]) && (szSelectedDept == Assigned[k])) {
                            flag = false;
                        }
                    }
                }
                document.getElementById('cmbUnAssignAddInt').options[document.getElementById('cmbUnAssignAddInt').length] = new Option(innerText, szSelectedDept)
                coll.remove(i);
            }
            var len = coll.options.length;
            if (len < 1) return;
            if (num < len) {
                document.getElementById('cmbAssignAddInt').options(num).selected = true;
            }
            else {
                document.getElementById('cmbAssignAddInt').options(num - 1).selected = true;
            }
        }
        function chkSEND_ALL_Change() {
            var chk = document.getElementById('chkSEND_ALL');
            if (chk == null)
                return false;
            //document.getElementById("hidADD_INT_ID").value='';
            if (chk.checked == true) {
                document.getElementById('trAddIntList').style.display = "none";
                AssignAddInt(true);
            }
            else {
                document.getElementById('trAddIntList').style.display = "inline";
                UnAssignAddIntchkSEND_ALL(true);
            }

            return false;
        }
        function GetAssignAddInt() {
            document.getElementById("hidADD_INT_ID").value = "";
            var coll = document.getElementById('cmbAssignAddInt');
            var len = coll.options.length;
            for (var k = 0; k < len; k++) {
                var szSelectedDept = coll.options(k).value;
                if (document.getElementById("hidADD_INT_ID").value == "") {
                    document.getElementById("hidADD_INT_ID").value = szSelectedDept;
                }
                else {
                    document.getElementById("hidADD_INT_ID").value = document.getElementById("hidADD_INT_ID").value + "~" + szSelectedDept;
                }
            }
        }
        function savePage() {
            GetAssignAddInt();
            Page_ClientValidate();
            if (!CheckEndorsementEffectiveDate())
                return false;
            if (!Page_IsValid) {
                return false;
            }
            else {
                return true;
            }
        }

        function CheckEndorsementEffectiveDate() {

            var msg1 = '<%=jmessage1 %>';
            var msg2 = '<%=jmessage2 %>';
            var val;
            var Customer_id = document.getElementById("hidCUSTOMER_ID").value;
            var Policy_id = document.getElementById("hidPOLICY_ID").value;
            var Policy_version_id = document.getElementById("hidNEW_POLICY_VERSION_ID").value;
            var CO_APPLICANT = document.getElementById("hidCO_APPLICANT_ID").value;
            var EFF_DATE = document.getElementById("txtEFFECTIVE_DATETIME").value;
            var EXP_DATE = document.getElementById("txtEXPIRY_DATE").value;
            //Added for Multilingual Support
            if (sCultureDateFormat == 'DD/MM/YYYY') {    // changed by praveer for TFS# 996
                aDateArr = EFF_DATE.split('/');
                strDay = aDateArr[0];
                strMonth = aDateArr[1];
                strYear = aDateArr[2];
                effDate = strMonth + '/' + strDay + '/' + strYear
                aDateArr = EXP_DATE.split('/');
                strDay = aDateArr[0];
                strMonth = aDateArr[1];
                strYear = aDateArr[2];
                expDate = strMonth + '/' + strDay + '/' + strYear
            }
            else {
                effDate = EFF_DATE;
                expDate = EXP_DATE;
            }
            var confirmval = EndorsementProcess.chkEndorsmentDiff(Customer_id, Policy_id, Policy_version_id, CO_APPLICANT, effDate, expDate)
            //alert(confirm.value);
            if (confirmval.value == "1") {
                val = confirm(msg1)

            }
            else if (confirmval.value == "2") {
                val = confirm(msg2)
            } else
                val = true;

            return val;
        }
        function HideShowCommitInProgress() {

            GetAssignAddInt();
            Page_ClientValidate();
            if (!Page_IsValid) {
                return false;
            }
            else {
                document.getElementById('btnCommitInProgress').style.display = "inline";
                document.getElementById('btnCommitInProgress').disabled = true;
                document.getElementById('btnCommit').style.display = "none";
                document.getElementById('btnSave').disabled = true;
                if (document.getElementById('btnRollback'))
                    document.getElementById('btnRollback').disabled = true;
                if (document.getElementById('btnComitAynway'))
                    document.getElementById('btnComitAynway').disabled = true;
                DisableButton(document.getElementById('btnCommit'));
                //Added for Itrack Issue 6203 on 31 July 2009
                top.topframe.disableMenus('1,7', 'ALL');
                return true;

            }
        }
        function ConfirmSubmit() {
    
            var msg = document.getElementById('hidCONFM_MSG').value;
            var confm = confirm(msg);
            if (confm) {
                if (Page_ClientValidate()) {
                    if (CheckEndorsementEffectiveDate()) {
                        if (Installment_End_Result())
                            return true
                        else
                            return false;
                    } else
                        return false
                } else
                    return false;
            } else
                return false;
//            if (confm) {
//                if (!CheckEndorsementEffectiveDate()) {
//                    return false;
//                }
//                Page_ClientValidate();
//                //   return true;
//            }
//            else
//                return false;
        }
        function HideShowCommitAnywayInProgress() {
            GetAssignAddInt();
            Page_ClientValidate();
            if (!Page_IsValid) {
                return false;
            }
            else {
                document.getElementById('btnCommitAnywayInProgress').style.display = "inline";
                document.getElementById('btnCommitAnywayInProgress').disabled = true;
                document.getElementById('btnComitAynway').style.display = "none";
                document.getElementById('btnSave').disabled = true;
                document.getElementById('btnCommit').disabled = true;
                if (document.getElementById('btnRollback'))
                    document.getElementById('btnRollback').disabled = true;
                DisableButton(document.getElementById('btnComitAynway'));
                //Added for Itrack Issue 6203 on 31 July 2009
                top.topframe.disableMenus('1,7', 'ALL');
                return true;
            }
        }
        function HideShowCommit() {
            document.getElementById('btnCommit').disabled = true;
            document.getElementById('btnSave').disabled = true;
            if (document.getElementById('btnComitAynway'))
                document.getElementById('btnComitAynway').disabled = true;
            DisableButton(document.getElementById('btnRollback'));
            top.topframe.disableMenus('1,7', 'ALL'); //Added for Itrack Issue 6203 on 31 July 2009
        }

        function InternalChange() {
            //alert(document.getElementById('chkINTERNAL_CHANGE').checked);
            if (document.getElementById('chkINTERNAL_CHANGE').checked == true) // printing option is true and disabled
            {
                document.getElementById('trINSURED').style.display = "inline";

                document.getElementById('trSENDALL').style.display = "inline";
                document.getElementById('trAddIntListHeading').style.display = "inline";

                document.getElementById('trAddIntList').style.display = "inline";
                document.getElementById('trLetter').style.display = "inline";
                document.getElementById('trLetterdropdowns').style.display = "inline";
                document.getElementById('chkPRINTING_OPTIONS').style.display = "inline";
                document.getElementById('chkPRINTING_OPTIONS').setAttribute("checked", true);
                document.getElementById('chkPRINTING_OPTIONS').disabled = true;
                document.getElementById('hidPrinting_Options').value = '1';
                PrintOptions();

            }
            else {
                document.getElementById('chkPRINTING_OPTIONS').disabled = false;
                if (onPageload == false)
                    document.getElementById('chkPRINTING_OPTIONS').setAttribute("checked", false);
                PrintOptions();
            }

        }
        function PrintOptions() {
            if (document.getElementById('chkPRINTING_OPTIONS').checked == false) {
                document.getElementById('trINSURED').style.display = "inline";
                document.getElementById('trAGENCY').style.display = "inline";
                document.getElementById('trSENDALL').style.display = "inline";
                document.getElementById('trAddIntListHeading').style.display = "inline";
                if (document.getElementById('chkSEND_ALL').checked == false)
                    document.getElementById('trAddIntList').style.display = "inline";
                document.getElementById('trLetter').style.display = "inline";
                document.getElementById('trLetterdropdowns').style.display = "inline";
                document.getElementById('hidPrinting_Options').value = '0';
            }
            else {

                document.getElementById('trINSURED').style.display = "none";
                document.getElementById('cmbADD_INT').selectedIndex = 0;
                document.getElementById("hidADD_INT_ID").value = '';
                document.getElementById('chkSEND_ALL').checked = false;
                document.getElementById('cmbINSURED').selectedIndex = 0;
                //document.getElementById('cmbAGENCY_PRINT').selectedIndex = -1;
                document.getElementById('trAGENCY').style.display = "none";
                document.getElementById('trSENDALL').style.display = "none";
                document.getElementById('trAddIntListHeading').style.display = "none";

                document.getElementById('trAddIntList').style.display = "none";
                document.getElementById('trLetter').style.display = "none";

                document.getElementById('trLetterdropdowns').style.display = "none";
                document.getElementById('hidPrinting_Options').value = '1';


            }
            SetAssignAddInt();
        }

        function SetAssignAddInt() {
            //alert(document.getElementById("hidADD_INT_ID").value);
            if (document.getElementById("hidADD_INT_ID").value == '' || document.getElementById("hidADD_INT_ID").value == '0')
                return;
            var selectedAddInt = new String(document.getElementById("hidADD_INT_ID").value);
            var selectedAddIntArr = selectedAddInt.split('~');
            if (selectedAddIntArr == null || selectedAddIntArr.length < 1)
                return;

            var coll = document.getElementById('cmbUnAssignAddInt');
            var selIndex = coll.options.selectedIndex;
            var len = coll.options.length;
            var arrLen = selectedAddIntArr.length;
            if (len < 1) return;
            var num = 0;
            for (var j = 0; j < arrLen; j++) {
                for (var i = len - 1; i > -1; i--) {
                    if (coll.options(i).value == selectedAddIntArr[j]) {
                        num = i;
                        var szSelectedDept = coll.options(i).value;
                        var innerText = coll.options(i).text;
                        document.getElementById('cmbAssignAddInt').options[document.getElementById('cmbAssignAddInt').length] = new Option(innerText, szSelectedDept)
                        coll.remove(i);
                        len = coll.options.length;
                    }
                }
            } /*	
			len = coll.options.length;	
			if(	num < len )
			{
				document.getElementById('cmbUnAssignAddInt').options(num).selected = true;
			}	
			else
			{
				if(num>0)
					document.getElementById('cmbUnAssignAddInt').options(num - 1).selected = true;
			}	*/
        }
        function AutoIdCardDisplay() {
            if (document.getElementById('hidLOB_ID').value == '<%=((int)Cms.CmsWeb.cmsbase.enumLOB.AUTOP).ToString()%>' || document.getElementById('hidLOB_ID').value == '<%=((int)Cms.CmsWeb.cmsbase.enumLOB.CYCL).ToString()%>') {
                document.getElementById('trAutoIdHeader').style.display = "inline";
                document.getElementById('trAutoIdControls').style.display = "inline";
            }
            else {
                document.getElementById('trAutoIdHeader').style.display = "none";
                document.getElementById('trAutoIdControls').style.display = "none";
            }

        }
        function EffectiveClickJS() {
            window.open("/cms/application/Aspx/Quote/Quote.aspx?CALLEDFROM=QUOTE_POL&CUSTOMER_ID=" + document.getElementById('hidCUSTOMER_ID').value + "&POLICY_ID=" + document.getElementById('hidPOLICY_ID').value + "&POLICY_VERSION_ID=" + document.getElementById('hidPOLICY_VERSION_ID').value + "&NEW_POLICY_VERSION_ID=" + document.getElementById('hidNEW_POLICY_VERSION_ID').value + "&LOBID=" + document.getElementById('hidLOB_ID').value + "&QUOTE_ID=" + "0" + "&ONLYEFFECTIVE=" + "true" + "&SHOW=" + "0" + "&EFFECTIVE_DATETIME=" + document.getElementById('hidEFFECTIVE_DATETIME').value + "&EXPIRY_DATE=" + document.getElementById('hidEXPIRY_DATE').value + "&,'Quote','resizable=yes,scrollbars=yes,left=150,top=50,width=700,height=600'");
            return false;
        }

        function setCO_APPLICANT(CO_APPLICANT_ID) {
            document.getElementById("hidCO_APPLICANT_ID").value = CO_APPLICANT_ID;
        }
        function openEndorsementsList(obj) {
            if (!document.getElementById('chkENDORSEMENT_RE_ISSUE').checked) {
                document.getElementById('tdchkENDORSEMENT_RE_ISSUE').colSpan = '3';
                document.getElementById('tdcmbSOURCE_VERSION_ID').style.display = 'none';
                document.getElementById('tdcapSOURCE_VERSION_ID').style.display = 'none';
                document.getElementById('rfvSOURCE_VERSION_ID').setAttribute('enabled', false)
                document.getElementById('rfvSOURCE_VERSION_ID').setAttribute('isvalid', true);
                document.getElementById('txtEFFECTIVE_DATETIME').disabled = false;
                document.getElementById('txtEXPIRY_DATE').disabled = false;


            } else {

                document.getElementById('hidEND_EFFECTIVE_DATE').value = document.getElementById('txtEFFECTIVE_DATETIME').value; //split[0];
                document.getElementById('hidEND_EXPIRY_DATE').value = document.getElementById('txtEXPIRY_DATE').value; //split[2];


                document.getElementById('tdchkENDORSEMENT_RE_ISSUE').colSpan = '1';
                document.getElementById('tdcmbSOURCE_VERSION_ID').style.display = 'inline';
                document.getElementById('tdcapSOURCE_VERSION_ID').style.display = 'inline';
                document.getElementById('rfvSOURCE_VERSION_ID').setAttribute('enabled', true);

                //                if (document.getElementById('cmbENDOSEMENTS').length !=1)
                //                document.getElementById('chkENDORSEMENT_RE_ISSUE').disabled = true;
                //                if(obj==1)
                //                    document.getElementById('cmbENDOSEMENTS').selectedIndex = 0;
            }
        }
        function OnChangeEndorsement(obj) {
            
            if (!document.getElementById('chkENDORSEMENT_RE_ISSUE').checked) {
                document.getElementById('hidEND_EFFECTIVE_DATE').value = document.getElementById('txtEFFECTIVE_DATETIME').value; //split[0];
                document.getElementById('hidEND_EXPIRY_DATE').value = document.getElementById('txtEXPIRY_DATE').value; //split[2];
                document.getElementById('cmbSOURCE_VERSION_ID').selectedIndex = -1;
                document.getElementById('hidSOURCE_VERSION_ID').value = "";
                if (typeof (obj) != 'undefined') {
                    document.getElementById('cmbCO_APPLICANT_ID').disabled = false;
                    document.getElementById('cmbCO_APPLICANT_ID ').selectedIndex = 0;
                }
                return;
            }


            if (typeof (obj) == 'undefined')
                obj = document.getElementById('cmbSOURCE_VERSION_ID')
            var EffeciveDate = '';
            var HTML = obj.options[obj.selectedIndex].innerHTML;
            var value = obj.value;
            var arr = value.split('^');
            var CO_APPLICANT_ID;
            if (typeof (arr[0]) != 'undefined' && typeof (arr[1]) != 'undefined') {
                document.getElementById('hidSOURCE_VERSION_ID').value = arr[0];
                CO_APPLICANT_ID = arr[1];
                //alert(CO_APPLICANT_ID);
                for (i = 0; i < document.getElementById('cmbCO_APPLICANT_ID').length; i++) {
                    if (document.getElementById('cmbCO_APPLICANT_ID').options[i].value == CO_APPLICANT_ID) {
                        document.getElementById('cmbCO_APPLICANT_ID').options[i].selected = true;
                        document.getElementById('cmbCO_APPLICANT_ID').disabled = true;
                        document.getElementById('hidCO_APPLICANT_ID').value = CO_APPLICANT_ID;
                    }
                }
            }

            var split = HTML.split(' ')
            if (typeof (split[0]) != 'undefined' && typeof (split[2]) != 'undefined') {
                document.getElementById('txtEFFECTIVE_DATETIME').value = split[0];
                document.getElementById('hidEND_EFFECTIVE_DATE').value = split[0];
                document.getElementById('txtEXPIRY_DATE').value = split[2];
                document.getElementById('hidEND_EXPIRY_DATE').value = split[2];

                document.getElementById('txtEFFECTIVE_DATETIME').disabled = true;
                document.getElementById('txtEXPIRY_DATE').disabled = true;

                document.getElementById('rfvEFFECTIVE_DATETIME').setAttribute('isValid', true);
                document.getElementById('rfvEFFECTIVE_DATETIME').style.display = "none";

                document.getElementById('rfvEXPIRY_DATE').setAttribute('isValid', true);
                document.getElementById('rfvEXPIRY_DATE').style.display = "none";


            }
        }

        // Added by Shikha 
        function Installment_End_Result() {
    
            var strCOUNT = document.getElementById('hidCOUNT').value;
            if (strCOUNT == "1") {
                 return confirm(document.getElementById('hidpopup').value);
             }
             return true;
        }

    </script>

</head>
<body oncontextmenu="return false;" leftmargin="0" topmargin="0" scroll="yes" onload="Init();">
    <form id="EndorsementProcess" method="post" runat="server">
    <div>
        <webcontrol:menu id="bottomMenu" runat="server">
        </webcontrol:menu></div>
    <!-- To add bottom menu -->
    <!-- To add bottom menu ends here -->
    <table cellspacing="0" cellpadding="0" width="90%" align="center" border="0">
        <tr>
            <td>
                <webcontrol:gridspacer id="grdSpacer" runat="server">
                </webcontrol:gridspacer>
            </td>
        </tr>
        <tr>
            <td>
                <webcontrol:policytop id="cltPolicyTop" runat="server">
                </webcontrol:policytop>
            </td>
        </tr>
        <tr>
            <td>
                <webcontrol:gridspacer id="Gridspacer1" runat="server">
                </webcontrol:gridspacer>
            </td>
        </tr>
        <tr>
            <td class="headereffectcenter">
                <asp:Label ID="capHeader" runat="server"></asp:Label>
            </td>
            <%--Endorsement Process--%>
        </tr>
        <tr>
            <td id="tdGridHolder">
                <webcontrol:gridspacer id="Gridspacer4" runat="server">
                </webcontrol:gridspacer><asp:PlaceHolder ID="Placeholder1" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td class="pageHeader">
                <asp:Label ID="capMessage" runat="server"></asp:Label>
            </td>
            <%--Please note that all fields marked with * are mandatory--%>
        </tr>
        <tr>
            <td class="midcolorc">
                <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr id="trBody">
            <td>
                <table id="tblMain" width="100%" align="center" border="0" runat="server">
                    <%--<tr id="trENDORSEMENT_OPTION">
								<td class="midcolora" colSpan="2" width="50%"></td>
								<td class="midcolora" width="25%"><asp:label id="capENDORSEMENT_OPTION" runat="server">Endorsement Option</asp:label><span class="mandatory">*</span></td>
								
								<td class="midcolora" width="25%">
								<asp:DropDownList runat="server" ID="cmbENDORSEMENT_OPTION">
								</asp:DropDownList><br />
								<asp:RequiredFieldValidator runat="server" ID="rfvENDORSEMENT_OPTION" Display="Dynamic" ErrorMessage="" ControlToValidate="cmbENDORSEMENT_OPTION" 
								></asp:RequiredFieldValidator>
								</td>
								
                                    
							</tr>--%>
                    <tr>
                        <td class="midcolora" width="25%">
                            <asp:Label runat="server" ID="capENDORSEMENT_RE_ISSUE">Re-Issue Endorsement</asp:Label>
                        </td>
                        <td class="midcolora" width="25%" id="tdchkENDORSEMENT_RE_ISSUE">
                            <asp:CheckBox runat="server" ID="chkENDORSEMENT_RE_ISSUE" onclick="openEndorsementsList(1)" />
                        </td>
                        <td class="midcolora" width="25%" id="tdcapSOURCE_VERSION_ID">
                            <asp:Label runat="server" ID="capSOURCE_VERSION_ID">Commited Endorsement</asp:Label>
                        </td>
                        <td class="midcolora" width="25%" id="tdcmbSOURCE_VERSION_ID">
                            <input type="hidden" runat="server" id="hidSOURCE_VERSION_ID" />
                            <asp:DropDownList runat="server" ID="cmbSOURCE_VERSION_ID" onchange="OnChangeEndorsement(this)">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator runat="server" ID="rfvSOURCE_VERSION_ID" ControlToValidate="cmbSOURCE_VERSION_ID"
                                Display="Dynamic" ErrorMessage="please select endorsement"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capEFFECTIVE_DATETIME" runat="server">Effective Date</asp:Label><span
                                class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtEFFECTIVE_DATETIME" runat="server" size="11" MaxLength="10"></asp:TextBox><asp:HyperLink
                                ID="hlkEFFECTIVE_DATETIME" runat="server" CssClass="HotSpot">
                                <asp:Image ID="imgEFFECTIVE_DATETIME" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif">
                                </asp:Image>
                            </asp:HyperLink><br>
                            <asp:RequiredFieldValidator ID="rfvEFFECTIVE_DATETIME" runat="server" ControlToValidate="txtEFFECTIVE_DATETIME"
                                ErrorMessage="EFFECTIVE_DATETIME can't be blank." Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                    ID="revEFFECTIVE_DATETIME" runat="server" ControlToValidate="txtEFFECTIVE_DATETIME"
                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator><asp:CompareValidator
                                        ID="cmpEFFECTIVE_DATETIME" ControlToValidate="txtEFFECTIVE_DATETIME" ErrorMessage=""
                                        runat="server" Display="Dynamic" Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1950"></asp:CompareValidator>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capEXPIRY_DATE" runat="server">Expiry Date</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtEXPIRY_DATE" runat="server" size="11" MaxLength="10"></asp:TextBox><asp:HyperLink
                                ID="hlkEXPIRY_DATE" runat="server" CssClass="HotSpot">
                                <asp:Image ID="imgEXPIRY_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif">
                                </asp:Image>
                            </asp:HyperLink><br>
                            <asp:RequiredFieldValidator ID="rfvEXPIRY_DATE" runat="server" ControlToValidate="txtEXPIRY_DATE"
                                ErrorMessage="EXPIRY_DATE can't be blank." Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                    ID="revEXPIRY_DATE" runat="server" ControlToValidate="txtEXPIRY_DATE" ErrorMessage="RegularExpressionValidator"
                                    Display="Dynamic"></asp:RegularExpressionValidator><asp:CompareValidator ID="cmpEXPIRY_DATE"
                                        ControlToValidate="txtEXPIRY_DATE" ErrorMessage="" runat="server" Display="Dynamic"
                                        Operator="GreaterThanEqual" Type="Date" ControlToCompare="txtEFFECTIVE_DATETIME"
                                        ValueToCompare="01/01/1950"></asp:CompareValidator><asp:CompareValidator ID="cmpEXPIRY_DATE_POLICY_EXP_DATE"
                                            ControlToValidate="txtEXPIRY_DATE" ErrorMessage="" runat="server" Display="Dynamic"
                                            Operator="LessThanEqual" Type="Date" ValueToCompare="01/01/3000"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr id="trPROPERTY_INSPECTION_CREDIT" runat="server">
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capPROPERTY_INSPECTION_CREDIT" runat="server">Inspection Credit</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:CheckBox ID="chkPROPERTY_INSPECTION_CREDIT" runat="server"></asp:CheckBox>
                        </td>
                        <td class="midcolora" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capCOMMENTS" runat="server">Comments</asp:Label>
                        </td>
                        <td class="midcolora" colspan="1">
                            <asp:TextBox ID="txtCOMMENTS" runat="server" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox><asp:CustomValidator
                                ID="csvCOMMENTS" ControlToValidate="txtCOMMENTS" Display="Dynamic" runat="server"
                                ClientValidationFunction="txtCOMMENTS_VALIDATE"></asp:CustomValidator>
                        </td>
                        <td class="midcolora" width="32%" colspan="1">
                            <asp:Label ID="capENDORSEMENT_TYPE" runat="server"></asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%" colspan="1">
                            <asp:DropDownList ID='cmbENDORSEMENT_TYPE' OnFocus="SelectComboIndex('cmbENDORSEMENT_TYPE')"
                                runat="server">
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvENDORSEMENT_TYPE" runat="server" ControlToValidate="cmbENDORSEMENT_TYPE"
                                ErrorMessage=" " Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="trInternalChange">
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capINTERNAL_CHANGE" runat="server"></asp:Label>
                        </td>
                        <td class="midcolora" width="32%" colspan="1">
                            <asp:CheckBox ID="chkINTERNAL_CHANGE" runat="server"></asp:CheckBox>
                        </td>
                        <td class="midcolora" width="18%" colspan="1">
                            <asp:Label ID="capCOINSURANCE_NUMBER" runat="server" Text="capCOINSURANCE_NUMBER "></asp:Label>
                        </td>
                        <td class="midcolora" width="32%" colspan="1">
                            <asp:TextBox ID="txtCOINSURANCE_NUMBER" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trCO_APPLICANT_ID">
                        <td class="midcolora" width="25%">
                            <asp:Label ID="capCO_APPLICANT_ID" runat="server"></asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="25%">
                            <asp:DropDownList runat="server" ID="cmbCO_APPLICANT_ID" onchange="setCO_APPLICANT(this.value);">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator runat="server" ID="rfvCO_APPLICANT_ID" ErrorMessage=""
                                Display="Dynamic" ControlToValidate="cmbCO_APPLICANT_ID"></asp:RequiredFieldValidator>
                            <input id="hidCO_APPLICANT_ID" type="hidden" runat="server" name="hidCO_APPLICANT_ID" />
                        </td>
                        <td class="midcolora" width="50%" colspan="2">
                        </td>
                    </tr>
                    <tr id="trPrintOptionHeading">
                        <td class="headerEffectSystemParams" colspan="4">
                            <asp:Label ID="lblPrinting" runat="server"></asp:Label>
                        </td>
                        <%--Printing Options Details--%>
                    </tr>
                    <tr id="trPrintOptions">
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capPRINTING_OPTIONS" runat="server">No printing Required at all</asp:Label>
                        </td>
                        <td class="midcolora" width="32%" colspan="3">
                            <asp:CheckBox ID="chkPRINTING_OPTIONS" runat="server"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr id="trINSURED">
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capINSURED" runat="server">Printing Options</asp:Label>
                        </td>
                        <td class="midcolora" width="32%" colspan="3">
                            <asp:DropDownList ID="cmbINSURED" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trAgency">
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capAGENCY_PRINT" runat="server">Agency</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbAGENCY_PRINT" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="midcolora" colspan="2">
                        </td>
                    </tr>
                    <tr id="trAddIntListHeading">
                        <td class="headerEffectSystemParams" colspan="4">
                            <asp:Label ID="lblAdditional" runat="server"></asp:Label>
                        </td>
                        <%--Additional Interest Details--%>
                    </tr>
                    <tr id="trSENDALL">
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capADD_INT" runat="server">Additional Interest</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbADD_INT" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capSEND_ALL" runat="server">Send to All</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:CheckBox ID="chkSEND_ALL" runat="server"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr id="trAddIntList">
                        <td class="midcolora" colspan="4">
                            <table>
                                <tr>
                                    <td class="midcolorc" align="center" width="37%">
                                        <asp:Label ID="capUnassignLossCodes" runat="server">All Additional Interest</asp:Label>
                                    </td>
                                    <td class="midcolorc" valign="middle" align="center" width="33%" rowspan="7">
                                        <br>
                                        <br>
                                        <input class="clsButton" id="btnAssignLossCodes" onclick="javascript:AssignAddInt(false);"
                                            type="button" value=">>" name="btnAssignLossCodes" runat="server"><br>
                                        <br>
                                        <input class="clsButton" id="btnUnAssignLossCodes" onclick="javascript:UnAssignAddInt(false);"
                                            type="button" value="<<" name="btnUnAssignLossCodes" runat="server">
                                    </td>
                                    <td class="midcolorc" align="center" width="33%">
                                        <asp:Label ID="capAssignedLossCodes" runat="server">Selected Additional Interest</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="midcolorc" align="center" width="33%" rowspan="6">
                                        <asp:ListBox ID="cmbUnAssignAddInt" runat="server" Width="300px" Height="150px" SelectionMode="Multiple">
                                        </asp:ListBox>
                                    </td>
                                    <td class="midcolorc" align="center" width="33%" rowspan="6">
                                        <asp:ListBox ID="cmbAssignAddInt" runat="server" Width="300px" Height="150px" SelectionMode="Multiple">
                                        </asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trLetter">
                        <td class="headerEffectSystemParams" colspan="4">
                            <asp:Label ID="lblLetter" runat="server"></asp:Label><%--Letter--%>
                        </td>
                    </tr>
                    <tr id="trLetterdropdowns">
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capADVERSE_LETTER_REQD" runat="server">Adverse Letter Required</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbADVERSE_LETTER_REQD" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="midcolora" colspan="2">
                        </td>
                    </tr>
                    <tr id="trAutoIdHeader">
                        <td class="headerEffectSystemParams" colspan="4">
                            Auto ID Card Details
                        </td>
                    </tr>
                    <tr id="trAutoIdControls">
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capAUTO_ID_CARD" runat="server">Auto ID Card</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbAUTO_ID_CARD" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capNO_COPIES" runat="server">No. of Copies</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtNO_COPIES" runat="server" MaxLength="2" size="6"></asp:TextBox><br>
                            <asp:RangeValidator ID="rngNO_COPIES" Display="Dynamic" ControlToValidate="txtNO_COPIES"
                                runat="server" Type="Integer" MinimumValue="0" MaximumValue="99"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" colspan="2">
                            <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" CausesValidation="false"
                                Text="Reset"></cmsb:CmsButton><cmsb:CmsButton class="clsButton" ID="btnRollback"
                                    runat="server" CausesValidation="false" Text=""></cmsb:CmsButton><cmsb:CmsButton
                                        class="clsButton" ID="btnPolicyDetails" Style="display: none" runat="server"
                                        Text="View Dec Page"></cmsb:CmsButton><br>
                            <cmsb:CmsButton class="clsButton" ID="btnBack_To_Search" runat="server" CausesValidation="false"
                                Text="Back To Search"></cmsb:CmsButton><cmsb:CmsButton class="clsButton" ID="btnBack_To_Customer_Assistant"
                                    runat="server" CausesValidation="false" Text="Back To Customer Assistant">
                            </cmsb:CmsButton>
                        </td>
                        <td class="midcolorr" colspan="2">
                            <cmsb:CmsButton class="clsButton" ID="btnCommit" runat="server" OnClientClick="return ConfirmSubmit();"
                                CausesValidation="true" Text="Commit"></cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnCommitInProgress" runat="server" CausesValidation="false"
                                Text="Commit in Progress"></cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnGet_Premium" Style="display: none" runat="server"
                                Text="Get Premium"></cmsb:CmsButton><cmsb:CmsButton class="clsButton" ID="btnEffectivePremium"
                                    Style="display: inline" runat="server" Text="Effective Premium"></cmsb:CmsButton><cmsb:CmsButton
                                        class="clsButton" ID="btnSave" runat="server" Text="Save" OnClientClick="javascript:return CheckEndorsementEffectiveDate()">
                                    </cmsb:CmsButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="90%" align="center" border="0">
        <tr>
            <td class="headereffectcenter" colspan="4">
                <span id="spnURStatus" runat="server">
                    <asp:Label ID="lblheader" runat="server"></asp:Label></span>
            </td>
            <%--<%--Underwriting Rules Status--%--%>
        </tr>
        <tr>
            <td>
                <div class="midcolora" id="myDIV" style="overflow: auto; width: 100%; height: 189px"
                    align="center" runat="server">
                </div>
            </td>
        </tr>
        <tr>
            <td class="midcolorr" align="left" colspan="4">
                <cmsb:CmsButton class="clsButton" ID="btnComitAynway" runat="server" Text=""></cmsb:CmsButton>
                <cmsb:CmsButton class="clsButton" ID="btnCommitAnywayInProgress" runat="server" Text="Commit in Progress">
                </cmsb:CmsButton>
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="90%" align="center" border="0">
        <tr>
            <td id="tdGridHolder">
                <webcontrol:gridspacer id="Gridspacer2" runat="server">
                </webcontrol:gridspacer><asp:PlaceHolder ID="GridHolder" runat="server"></asp:PlaceHolder>
                <webcontrol:gridspacer id="Gridspacer3" runat="server">
                </webcontrol:gridspacer>
            </td>
        </tr>
        <tr id="tabCtlRow">
            <td>
                <webcontrol:tab id="TabCtl" runat="server" TabLength="150" TabTitles="Endorsement Details"
                    TabURLs="/cms/cmsweb/maintenance/TransactionLogDetail.aspx?calledfrom=ENDORSEMENT&amp;">
                </webcontrol:tab>
            </td>
        </tr>
        <tr>
            <td>
                <table class="tableeffect" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td>
                            <iframe class="iframsHeightLong" id="tabLayer" src="" frameborder="0" width="100%"
                                scrolling="no" runat="server"></iframe>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <webcontrol:gridspacer id="Gridspacer5" runat="server">
                </webcontrol:gridspacer>
            </td>
        </tr>
    </table>
    <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
    <input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
    <input id="hidCALLED_FROM" type="hidden" name="hidCALLED_FROM" runat="server">
    <input id="hidVEHICLE_ID" type="hidden" value="0" name="hidVEHICLE_ID" runat="server">
    <input id="hidOldData" type="hidden" name="hidOldData" runat="server">
    <input id="hidROW_ID" type="hidden" name="hidROW_ID" runat="server">
    <input id="hidPOLICY_ID" type="hidden" name="hidPOLICY_ID" runat="server">
    <input id="hidPOLICY_VERSION_ID" type="hidden" name="hidPOLICY_VERSION_ID" runat="server">
    <input id="hidCUSTOMER_ID" type="hidden" name="hidCUSTOMER_ID" runat="server">
    <input id="hidPROCESS_ID" type="hidden" name="hidPROCESS_ID" runat="server">
    <input id="hidENDORSEMENT_NO" type="hidden" name="hidENDORSEMENT_NO" runat="server">
    <input type="hidden" name="hidTemplateID">
    <input type="hidden" name="hidRowID">
    <input id="hidlocQueryStr" type="hidden" name="hidlocQueryStr">
    <input id="hidNEW_POLICY_VERSION_ID" type="hidden" name="hidNEW_POLICY_VERSION_ID"
        runat="server">
    <input id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
    <input id="hidADD_INT_ID" type="hidden" name="hidADD_INT_ID" runat="server">
    <input id="hidPrinting_Options" type="hidden" name="hidPrinting_Options" runat="server">
    <input id="hidUNDERWRITER" type="hidden" name="hidUNDERWRITER" runat="server" value="0">
    <input id="hidDisplayBody" type="hidden" value="0" name="hidDisplayBody" runat="server">
    <input id="hidEFFECTIVE_DATETIME" type="hidden" value="0" name="hidEFFECTIVE_DATETIME"
        runat="server">
    <input id="hidEXPIRY_DATE" type="hidden" value="0" name="hidEXPIRY_DATE" runat="server">
    <input id="hidSTATE_ID" type="hidden" value="0" name="hidSTATE_ID" runat="server">
    <input id="hidSTATE_CODE" type="hidden" name="hidSTATE_CODE" runat="server">
    <input id="hidPOLICY_TYPE" type="hidden" name="hidPOLICY_TYPE" runat="server">
    <input id="hidEND_EFFECTIVE_DATE" type="hidden" name="hidEND_EFFECTIVE_DATE" runat="server">
    <input id="hidEND_EXPIRY_DATE" type="hidden" name="hidEND_EXPIRY_DATE" runat="server">
    <input id="hidENDORSEMENT_OPTION" type="hidden" name="hidENDORSEMENT_OPTION" runat="server">
    <input id="hidPOLICY_CURRENT_STATUS" type="hidden" name="hidPOLICY_CURRENT_STATUS"
        runat="server">
    <input id="hidCARRIER_CODE" type="hidden" name="hidCARRIER_CODE" runat="server">
    <input id="hidEND_OPTION" type="hidden" name="hidEND_OPTION" runat="server">
    <input id="hidCONFM_MSG" type="hidden" name="hidCONFM_MSG" runat="server">
    <input id="hidPROCESS_TYPE" type="hidden" name="hidPROCESS_TYPE" runat="server"  value="">
    <input id="hidCOUNT" type="hidden" name="hidCOUNT" runat="server">
    <INPUT id="hidpopup" type="hidden" name="hidpopup" runat="server">
    </form>
</body>
</html>

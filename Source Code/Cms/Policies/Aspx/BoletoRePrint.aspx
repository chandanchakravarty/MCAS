<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoletoRePrint.aspx.cs" Inherits="Cms.Policies.Aspx.BoletoRePrint" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>Boleto Re-Print</title>
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >

        <script src="/cms/cmsweb/scripts/xmldom.js"></script>
        <script src="/cms/cmsweb/scripts/common.js"></script>
        <script src="/cms/cmsweb/scripts/form.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>

        <script language="javascript" type="text/javascript">
            var isPagePost = true;
            var isPagePost1 = true;
            var Succeeded = false;
            var jsaAppDtFormat = "<%=aAppDtFormat  %>";
            function Init() {
               
                ApplyColor();
                ChangeColor();
                DisableSave();
            }
            function DisableSave() {
                if (document.getElementById('btnSave') != null) {
                    if (parseInt(document.getElementById('hidGRID_ROW_COUNT').value) < 1)
                        document.getElementById('btnSave').disabled = true;
                    else
                        document.getElementById('btnSave').disabled = false;
                } 
             
            }
            //Calculate installment total
            function InstallmentTotal(objcontrol) {
                var objcontrolID = objcontrol.id;
                var num = objcontrol.value;

                var splID = objcontrolID.split('_');   //Genrate dynamic id for each textbox in gridview for every row for calculate sum 
                var PREMIUM = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtPREMIUM';  //installment premium textbox
                var INTEREST_AMOUNT = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtINTEREST_AMOUNT'; //installment interest amount textbox
                var FEE = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtFEE'; //installment fees amount textbox
                var TAXES = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtTAXES'; //installment taxes amount textbox
                var TOTAL = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtTOTAL'; //installment total(sum of all amount textbox)
                var hidTOTAL = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'hidTOTAL'; //hid field for calculated sum


                /* get the value from gridview textbox For calculate sum row wise and assign it into total & hid total column */

                var txtPREMIUM = document.getElementById(PREMIUM).value == '' ? '0' : document.getElementById(PREMIUM).value;
                txtPREMIUM = FormatAmountForSum(txtPREMIUM);


                var txtINTEREST_AMOUNT = document.getElementById(INTEREST_AMOUNT).value == '' ? '0' : document.getElementById(INTEREST_AMOUNT).value;
                txtINTEREST_AMOUNT = FormatAmountForSum(txtINTEREST_AMOUNT);

                var txtFEE = document.getElementById(FEE).value == '' ? '0' : document.getElementById(FEE).value;
                txtFEE = FormatAmountForSum(txtFEE);

                var txtTAXES = document.getElementById(TAXES).value == '' ? '0' : document.getElementById(TAXES).value;
                txtTAXES = FormatAmountForSum(txtTAXES);


                if (isNaN(parseFloat(txtPREMIUM)))
                    txtPREMIUM = '0';
                if (isNaN(parseFloat(txtINTEREST_AMOUNT)))
                    txtINTEREST_AMOUNT = '0';
                if (isNaN(parseFloat(txtFEE)))
                    txtFEE = '0';
                if (isNaN(parseFloat(txtTAXES)))
                    txtTAXES = '0';


                var sumTOTAL = parseFloat(txtPREMIUM) + parseFloat(txtINTEREST_AMOUNT) + parseFloat(txtFEE) + parseFloat(txtTAXES)

                sumTOTAL = roundNumber(sumTOTAL, 2);

                /* End main installment textbox calculation part*/
                if (!isNaN(sumTOTAL)) {
                    document.getElementById(TOTAL).value = formatAmount(parseFloat(sumTOTAL));
                    document.getElementById(hidTOTAL).value = formatAmount(parseFloat(sumTOTAL));
                }
                else {  //if calculated sum is less then 0 then assign it as blank .
                    document.getElementById(TOTAL).value = roundNumber('0', 2);
                    document.getElementById(hidTOTAL).value = '';
                }
            }

            function FormatAmountForSum(num) {
                num = ReplaceAll(num, sGroupSep, '');
                num = ReplaceAll(num, sDecimalSep, '.');
                return num;
            }
            function roundNumber(num, dec) {
                var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
                return result;
            }
            function GenerateBoleto(Installmentno, Type, Policy_version, CO_APPLICANT_ID) {
                var str;
                str = "/cms/Policies/Aspx/PolicyBoleto.aspx?CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value + "&POLICY_ID=" + document.getElementById("hidPOLICY_ID").value
		              + "&POLICY_VERSION_ID=" + Policy_version + "&INSTALLMENT_NO=" + Installmentno //+ "&CO_APPLICANT_ID=" + CO_APPLICANT_ID
                
                if (CO_APPLICANT_ID != '' && CO_APPLICANT_ID != undefined)
                    str = str + '&CO_APPLICANT_ID=' + CO_APPLICANT_ID
                          
                if (Type != '' && Type != 'undefined')
                    str = str + '&GENERATE_ALL_INSTALLMENT=' + Type

                window.open(str, "Boleto", "resizable=yes,toolbar=0,location=0, directories=0, status=0, menubar=0,scrollbars=yes,width=800,height=500,left=150,top=50");

            }
            
        </script>
        
<script type="text/javascript">
    function GetUpdatedRow() {
        $("#grdBOLETO_REPRINT tr input[id $= 'txtINTEREST_AMOUNT']").live('change', function(e) {
            if ($("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidUPDATEFlag']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val() == "") {
                CountRows();
                $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidUPDATEFlag']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val("1");
            }
           
        });
        $("#grdBOLETO_REPRINT tr input[id $= 'txtINSTALLMENT_DATE']").live('change', function(e) {
            if ($("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidUPDATEFlag']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val() == "") {
                CountRows();
                $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidUPDATEFlag']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val("1");
            }
        });

    }
    function CountRows() {
        if (document.getElementById("hidUPDATEDROWS").value != '') {
            var hidUPDATEDROWS = document.getElementById("hidUPDATEDROWS").value;
            hidUPDATEDROWS = parseInt(hidUPDATEDROWS) + 1;
            document.getElementById("hidUPDATEDROWS").value = hidUPDATEDROWS;
        }
        else {
            document.getElementById("hidUPDATEDROWS").value = 1;
        }
    }

    $(document).ready(function() {
        // GetUpdatedRow();
        $('body').bind('keydown', function(event) {
            if (event.keyCode == '13') {
                if ((isPagePost) && (isPagePost1)) {
                    return true; // $("#btnSave").trigger('click');//Commented by Pradeep on 21-July-2011 
                }
                else return false;
            }
        });
        $("#btnSave").click(function() {
            if ((isPagePost) && (isPagePost1)) return true;
            else return false;

        });


        $("#grdBOLETO_REPRINT tr input[id $= 'btnEDIT']").live('click', function(e) {

            if ($('#hidCHANGEDROW').val() != "1") {
                $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'txtINSTALLMENT_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).attr('readonly', false);
                $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'txtINSTALLMENT_EXPIRE_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).attr('readonly', false);
            }
            else {
                if ($('#hidChangedflage').val() != "1")
                    $('#lblMessage').text($('#hidUnSavedMsg').val());
            }
            return false;
        });

        $("#grdBOLETO_REPRINT tr input[id $= 'txtINSTALLMENT_DATE']").live('change', function(e) {

            //debugger;
            //Chenged row
            $("#grdBOLETO_REPRINT tr input[id $= 'txtINSTALLMENT_DATE']").blur();
            if ($("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidUPDATEFlag']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val() == "") {
                CountRows();
                $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidUPDATEFlag']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val("1");
            }
            $('#hidCHANGEDROW').val("1");

            //till here

            //Itrack-835 Added By Pradeep Kushwaha on 1-April-2011
            //            var PrevINSTALLMENT_EXPIRE_DATE = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_EXPIRE_DATE1']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();
            //            PrevINSTALLMENT_EXPIRE_DATE = DateConvert(PrevINSTALLMENT_EXPIRE_DATE, jsaAppDtFormat);

            //            var PrevINSTAL_EXPIRE_DATE = new Date(PrevINSTALLMENT_EXPIRE_DATE);
            //            PrevINSTAL_EXPIRE_DATE = Date.parse(PrevINSTAL_EXPIRE_DATE)

            //            var CurrentINSTALLMENT_DATE = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'txtINSTALLMENT_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();
            //            $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val(CurrentINSTALLMENT_DATE);
            //            CurrentINSTALLMENT_DATE = DateConvert(CurrentINSTALLMENT_DATE, jsaAppDtFormat);

            //            var CurrINSTALLMENT_DATE = new Date(CurrentINSTALLMENT_DATE);
            //            CurrINSTALLMENT_DATE = Date.parse(CurrINSTALLMENT_DATE)

            //            if (CurrINSTALLMENT_DATE > PrevINSTAL_EXPIRE_DATE) {
            //                $('#lblMessage0').text($('#HidInstalDueDateFromCancelMsg').val());
            //                $('#hidChangedflage').val("1");
            //                isPagePost1 = false;
            //                return;
            //            }
            //till here on 1-April-2011

            var TranType = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidTRAN_TYPE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();

            if (TranType == "NBS") {

                var PolicyEffectiveDate = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidPOLICY_EFFECTIVE_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();
                var PolicyExpireDate = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidPOLICY_EXPIRATION_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();

                var installeffdate = '';
                var installment_no = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_NO']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();

                if (installment_no == "1")
                    installeffdate = PolicyEffectiveDate;
                else
                    installeffdate = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 2].id).val();

                PolicyEffectiveDate = installeffdate;

                var CurrentInstallDueDate = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'txtINSTALLMENT_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();
                var vInstallDueDate = CurrentInstallDueDate;

                var vINSTALLMENT_EXPIRE_DATE = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'txtINSTALLMENT_EXPIRE_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();


                $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val(CurrentInstallDueDate);

                CurrentInstallDueDate = DateConvert(CurrentInstallDueDate, jsaAppDtFormat);

                vINSTALLMENT_EXPIRE_DATE = DateConvert(vINSTALLMENT_EXPIRE_DATE, jsaAppDtFormat);
                var InstallExpDate = new Date(vINSTALLMENT_EXPIRE_DATE);
                InstallExpDate = Date.parse(InstallExpDate)

                var CurrInstallDueDate = new Date(CurrentInstallDueDate);
                CurrInstallDueDate = Date.parse(CurrInstallDueDate)

                PolicyEffectiveDate = DateConvert(PolicyEffectiveDate, jsaAppDtFormat);

                var PolEffdate = new Date(PolicyEffectiveDate);
                PolEffdate.setDate(PolEffdate.getDate() + 30);
                PolEffdate = Date.parse(PolEffdate);

                if (CurrInstallDueDate > PolEffdate) {
                    if (installment_no == "1")
                        $('#lblMessage0').text($('#hidFirstInstallMsg').val());
                    else
                        $('#lblMessage0').text($('#hidSecandInstallMsg').val());

                    $('#hidChangedflage').val("1");
                    isPagePost1 = false;
                }
                else {
                    isPagePost1 = true;
                    $('#lblMessage0').text('');
                    $('#hidChangedflage').val('');
                    if (InstallExpDate < CurrInstallDueDate) {

                        $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'txtINSTALLMENT_EXPIRE_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val(vInstallDueDate);
                        $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_EXPIRE_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val(vInstallDueDate);
                        $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_EXPIRE_DATE1']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val(vInstallDueDate);
                    }

                }

            }
            else if (TranType == "END") {

                var EndEffectiveDate = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidEND_EFFECTIVE_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();
                var EndExpireDate = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidEND_EXPIRY_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();


                var installeffdate = '';
                var installment_no = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_NO']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();

                if (installment_no == "1")
                    installeffdate = EndEffectiveDate;
                else
                    installeffdate = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 2].id).val();

                EndEffectiveDate = installeffdate;


                var PrevInstallDueDate = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();
                var CurrentInstallDueDate = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'txtINSTALLMENT_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();
                var EndCurrentInstallDueDate = CurrentInstallDueDate;

                var END_INSTALLMENT_EXPIRE_DATE = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'txtINSTALLMENT_EXPIRE_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();

                $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val(CurrentInstallDueDate);

                END_INSTALLMENT_EXPIRE_DATE = DateConvert(END_INSTALLMENT_EXPIRE_DATE, jsaAppDtFormat);
                var EndInstallExpDate = new Date(END_INSTALLMENT_EXPIRE_DATE);
                EndInstallExpDate = Date.parse(EndInstallExpDate)


                CurrentInstallDueDate = DateConvert(CurrentInstallDueDate, jsaAppDtFormat);

                EndEffectiveDate = DateConvert(EndEffectiveDate, jsaAppDtFormat);

                var EndEffDate = new Date(EndEffectiveDate);

                EndEffDate.setDate(EndEffDate.getDate() + 30);
                EndEffDate = Date.parse(EndEffDate);

                var vCurrInstallDueDate = new Date(CurrentInstallDueDate);
                vCurrInstallDueDate = Date.parse(vCurrInstallDueDate)

                if (vCurrInstallDueDate > EndEffDate) {
                    if (installment_no == "1")
                        $('#lblMessage0').text($('#hidFirstInstallMsg').val());
                    else
                        $('#lblMessage0').text($('#hidSecandInstallMsg').val());

                    $('#hidChangedflage').val("1");
                    isPagePost1 = false;
                }
                else {
                    isPagePost1 = true;
                    $('#lblMessage0').text('');
                    $('#hidChangedflage').val('');
                    if (EndInstallExpDate < vCurrInstallDueDate) {
                        $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'txtINSTALLMENT_EXPIRE_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val(EndCurrentInstallDueDate);
                        $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_EXPIRE_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val(EndCurrentInstallDueDate);
                        $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_EXPIRE_DATE1']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val(EndCurrentInstallDueDate);
                    }
                }
            }
        });

        $("#grdBOLETO_REPRINT tr input[id $= 'txtINSTALLMENT_EXPIRE_DATE']").live('change', function(e) {
           //debugger;
            $("#grdBOLETO_REPRINT tr input[id $= 'txtINSTALLMENT_EXPIRE_DATE']").blur();
            //debugger;
            //Chenged row
            if ($("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidUPDATEFlag']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val() == "") {
                CountRows();
                $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidUPDATEFlag']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val("1");
            }
            //till here
            $('#hidCHANGEDROW').val("1");

            var PrevExpireDate = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_EXPIRE_DATE1']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();
            var CurrentExpireDate = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'txtINSTALLMENT_EXPIRE_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();



            var CUSTOMER_ID = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidCUSTOMER_ID']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();
            var POLICY_ID = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidPOLICY_ID']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();
            var POLICY_VERSION_ID = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidPOLICY_VERSION_ID']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();
            var INSTALLMENT_NO = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_NO']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();
            //Itrack-835 Added By Pradeep Kushwaha on 29-March-2011
            var INSTALLMENT_DATE = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_DATE1']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();
            INSTALLMENT_DATE = DateConvert(INSTALLMENT_DATE, jsaAppDtFormat);

            var CurrentInstallDueDate = $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'txtINSTALLMENT_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val();

            var vCurrentInstallDueDate = new Date(CurrentInstallDueDate);
            vCurrentInstallDueDate = Date.parse(vCurrentInstallDueDate);

            var vCurrentExpireDate = CurrentExpireDate;
            vCurrentExpireDate = new Date(vCurrentExpireDate);
            vCurrentExpireDate = Date.parse(vCurrentExpireDate);

            if (vCurrentInstallDueDate < vCurrentExpireDate) {
                $('#hidPrevExpireDate').val(PrevExpireDate);
                $('#hidCurrentExpireDate').val(CurrentExpireDate);
                //Added till here 
                $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_EXPIRE_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val(CurrentExpireDate);

                PageMethod("GetPolicyExpireDate", ["CUSTOMER_ID", CUSTOMER_ID, "POLICY_ID", POLICY_ID, "POLICY_VERSION_ID", POLICY_VERSION_ID, "INSTALLMENT_NO", INSTALLMENT_NO, "INSTALLMENT_DATE", INSTALLMENT_DATE], AjaxSucceeded, AjaxFailed); //With parameters
            }
            else {
                $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'txtINSTALLMENT_EXPIRE_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val(CurrentExpireDate);
                $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_EXPIRE_DATE']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val(CurrentExpireDate);
                $("#" + $("#grdBOLETO_REPRINT tr input[id $= 'hidINSTALLMENT_EXPIRE_DATE1']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val(CurrentExpireDate);
                
                $('#lblMessage').text('');
                $('#hidChangedflage').val('');
                isPagePost = true;
            }


        });
    });
    function DateConvert(Date, dateFormate) {
       // debugger;
        if (Date == "" || Date.length < 8) return "";
        var returnDate = '';
        var saperator = '/';
        var firstDate, secDate;

        var strDateFirst = Date.split("/");
        //var strDateSec = DateSec.split("/");

        if (dateFormate.toLowerCase() == "dd/mm/yyyy") {
            //alert("dd/mm/yyyy")
            returnDate = (strDateFirst[1].length != 2 ? '0' + strDateFirst[1] : strDateFirst[1]) + '/' + (strDateFirst[0].length != 2 ? '0' + strDateFirst[0] : strDateFirst[0]) + '/' + (strDateFirst[2].length != 4 ? '0' + strDateFirst[2] : strDateFirst[2]);

        }
        if (dateFormate.toLowerCase() == "mm/dd/yyyy") {
            //alert("mm/dd/yyyy")
            returnDate = Date
            //secDate = DateSec;
        }

        return returnDate;
    }
   
    function PageMethod(fn, paramArray, successFn, errorFn) {
        var pagePath = window.location.pathname;
        var paramList = '';
        if (paramArray.length > 0) {
            for (var i = 0; i < paramArray.length; i += 2) {
                if (paramList.length > 0) paramList += ',';
                paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';}
        }
        paramList = '{' + paramList + '}';
        //Call the page method  
        $.ajax({type: "POST",
            url: pagePath + "/" + fn,
            contentType: "application/json; charset=utf-8",
            data: paramList,dataType: "json",success: successFn,error: errorFn
        });
    }
    function AjaxSucceeded(result) {
        
        var PrevExpireDate = $('#hidPrevExpireDate').val();
       
        
        var ExpireDate = result.d;
        
        if (ExpireDate != "") {
            PrevExpireDate = ExpireDate;
        }

        PrevExpireDate = DateConvert(PrevExpireDate, jsaAppDtFormat);
        
        var CurrentExpireDate = $('#hidCurrentExpireDate').val();
        CurrentExpireDate = DateConvert(CurrentExpireDate, jsaAppDtFormat);
        
        var vPrevExpireDate = new Date(PrevExpireDate);
        vPrevExpireDate = Date.parse(vPrevExpireDate);

        var vCurrentExpireDate = new Date(CurrentExpireDate);
        vCurrentExpireDate = Date.parse(vCurrentExpireDate);

        if (vCurrentExpireDate > vPrevExpireDate) {
            $('#lblMessage').text($('#hidInsatallExipreMsg').val());
            $('#hidChangedflage').val("1");
            isPagePost = false;
        }
        else {
            $('#lblMessage').text('');
            $('#hidChangedflage').val('');
            isPagePost = true;
        }
     
    }
    function AjaxFailed(result) {
        alert(result.status + ' ' + result.statusText); //Display the error message (If there is any error while retriving record)
        Succeeded = false;
    }
    function ReturnBack() {
        var CALLED_FROM = document.getElementById('hidCALLED_FOR').value;
        if (CALLED_FROM == 'MST_POL_BILL')
            self.location = '/cms/Policies/Aspx/MasterPolicyBillingInfo.aspx?CALLEDFROM=' + CALLED_FROM + '&';
        else
            self.location = '/cms/Policies/Aspx/BillingInfo.aspx?CALLEDFROM=' + CALLED_FROM + '&';

        return false;
    }
</script>
    <style type="text/css">

        .trd
        {
            display: none;
        }
    </style>
</head>
<body leftmargin="0" rightmargin="0" ms_positioning="GridLayout" onload="Init();">
        <form id="BOLETO_RE_PRINT_INFO" runat="server">
        
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="headereffectCenter" colspan="2" style="width:100%">
                    <asp:Label ID="lblHeader" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="midcolorc" colspan="2" style="width:100%">
                    <asp:Label ID="lblFormLoad" runat="server" CssClass="errmsg"></asp:Label>
                </td>
            </tr>
            <tbody>
                <tr>
                    <td class="midcolorc" style="height:15px;width:100%" colspan="2" >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="midcolorc" style="height:15px;width:100%" colspan="2" >
                       <asp:Label runat="server" ID="lblMessage" Text=""  CssClass="errmsg"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="midcolorc" style="height:15px;width:100%" colspan="2" >
                       <asp:Label runat="server" ID="lblMessage0" Text=""  CssClass="errmsg"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="midcolorc" style="width:100%" colspan="2">
                        <asp:Label ID="lblFormLoadMessage" runat="server" Visible="False" CssClass="errmsg"></asp:Label> 
                    </td>
                </tr>
                 <tr>
                    <td class="midcolorc" style="height:15px;width:100%"  colspan="2">                       
                    <asp:GridView AutoGenerateColumns="False" runat="server" ID="grdBOLETO_REPRINT" 
                          Width="100%" onrowdatabound="grdBOLETO_REPRINT_RowDataBound" >
                      <HeaderStyle CssClass="headereffectWebGrid" HorizontalAlign="Center"></HeaderStyle>
                        <RowStyle CssClass="midcolora"></RowStyle>
                        <Columns> 
                        <%-- display None Column  start--%>
                         <asp:TemplateField  HeaderStyle-CssClass="trd" ItemStyle-CssClass="trd" FooterStyle-CssClass="trd">                          
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblROW_ID" Text='<%# Eval("ROW_ID.CurrentValue") %>'></asp:Label>
                                </ItemTemplate>

                                <FooterStyle CssClass="trd"></FooterStyle>

                                <HeaderStyle CssClass="trd"></HeaderStyle>

                                <ItemStyle CssClass="trd"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField   HeaderStyle-CssClass="trd" ItemStyle-CssClass="trd" FooterStyle-CssClass="trd" >                          
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPOLICY_VERSION_ID" Text='<%# Eval("POLICY_VERSION_ID.CurrentValue") %>'></asp:Label>
                                </ItemTemplate>

                                <FooterStyle CssClass="trd"></FooterStyle>

                                <HeaderStyle CssClass="trd"></HeaderStyle>

                                <ItemStyle CssClass="trd"></ItemStyle>
                            </asp:TemplateField>
                          
                        <%-- end display none Column  --%>
                         <asp:TemplateField >                       
                           <ItemStyle Width="2%" VerticalAlign="Middle" HorizontalAlign="Center" />    
                             <HeaderTemplate>
                            
                                    <asp:Label runat="server" ID="capINSLAMENT_NO" Text="">Install #</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblINSTALLMENT_NO"  Text='<%# Eval("INSTALLMENT_NO.CurrentValue") %>'></asp:Label>
                                    <input type="hidden" runat="server" id="hidINSTALLMENT_NO" name="hidINSTALLMENT_NO" value='<%# Eval("INSTALLMENT_NO.CurrentValue") %>' />
                                    <input type="hidden" runat="server" id="hidUPDATEFlag" name="hidUPDATEFlag" />
                                    <input type="hidden" runat="server" id="hidCUSTOMER_ID" name="hidCUSTOMER_ID" value='<%# Eval("CUSTOMER_ID.CurrentValue") %>' />
                                    <input type="hidden" runat="server" id="hidPOLICY_ID" name="hidPOLICY_ID" value='<%# Eval("POLICY_ID.CurrentValue") %>' />
                                    <input type="hidden" runat="server" id="hidPOLICY_VERSION_ID" name="hidPOLICY_VERSION_ID" value='<%# Eval("POLICY_VERSION_ID.CurrentValue") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                         
                         <asp:TemplateField >   
                           <ItemStyle Width="8%" VerticalAlign="Middle" HorizontalAlign="Center" />                      
                             <HeaderTemplate>
                                    <asp:Label runat="server" ID="capBOLETO_NO" Text="">Our Number</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                <asp:TextBox runat="server" ReadOnly="true" ID="txtBOLETO_NO" 
                                        Text='<%# Eval("BOLETO_NO.CurrentValue") %>' Width="100px"  ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                         
                        <asp:TemplateField >
                           <ItemStyle Width="8%" VerticalAlign="Middle" HorizontalAlign="Left" />          
                             <HeaderTemplate>
                                    <asp:Label runat="server" ID="capINSTALLMENT_DATE" Text="">Installment Date</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox runat="server" ID="txtINSTALLMENT_DATE" ReadOnly="true" Text='<%# Eval("INSTALLMENT_EFFECTIVE_DATE.CurrentValue") %>' onblur="FormatDate();" Width="75px"></asp:TextBox>
                                    <br />
                                   <input id="hidINSTALLMENT_DATE" runat="server" type="hidden" value='<%# Eval("INSTALLMENT_EFFECTIVE_DATE.CurrentValue") %>' />
                                   <input id="hidINSTALLMENT_DATE1" runat="server" type="hidden" value='<%# Eval("INSTALLMENT_EFFECTIVE_DATE.CurrentValue") %>' />
                                   <input id="hidEND_EFFECTIVE_DATE" runat="server" type="hidden" value='<%# Eval("END_EFFECTIVE_DATE.CurrentValue") %>' />
                                   <input id="hidEND_EXPIRY_DATE" runat="server" type="hidden" value='<%# Eval("END_EXPIRY_DATE.CurrentValue") %>' />
                                   <input id="hidPOLICY_EFFECTIVE_DATE" runat="server" type="hidden" value='<%# Eval("POLICY_EFFECTIVE_DATE.CurrentValue") %>' />
                                   <input id="hidPOLICY_EXPIRATION_DATE" runat="server" type="hidden" value='<%# Eval("POLICY_EXPIRATION_DATE.CurrentValue") %>' />
                                   <input id="hidTRAN_TYPE" runat="server" type="hidden" value='<%# Eval("TRAN_TYPE.CurrentValue") %>' />
                                  
                                   <asp:RequiredFieldValidator runat="server" ErrorMessage="" Display="Dynamic" ID="rfvINSTALLMENT_DATE" ControlToValidate="txtINSTALLMENT_DATE"></asp:RequiredFieldValidator>
                                   <asp:RegularExpressionValidator runat="server"  ErrorMessage="" Display="Dynamic" ID="revINSTALLMENT_DATE" ControlToValidate="txtINSTALLMENT_DATE"></asp:RegularExpressionValidator>
                                   
                                   
                               </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField >
                           <ItemStyle Width="8%" VerticalAlign="Middle" HorizontalAlign="Left" />          
                             <HeaderTemplate>
                                    <asp:Label runat="server" ID="capINSTALLMENT_EXPIRE_DATE" Text="">Expire Date</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox runat="server" ID="txtINSTALLMENT_EXPIRE_DATE"  ReadOnly="true" Text='<%# Eval("INSTALLMENT_EXPIRE_DATE.CurrentValue") %>' onblur="FormatDate();" Width="75px" ></asp:TextBox>
                                   <br />
                                   <input id="hidINSTALLMENT_EXPIRE_DATE" runat="server" type="hidden" value='<%# Eval("INSTALLMENT_EXPIRE_DATE.CurrentValue") %>' />
                                   <input id="hidINSTALLMENT_EXPIRE_DATE1" runat="server" type="hidden" value='<%# Eval("INSTALLMENT_EXPIRE_DATE.CurrentValue") %>' />
                                   
                                   <asp:RequiredFieldValidator runat="server" ErrorMessage="" Display="Dynamic" ID="rfvINSTALLMENT_EXPIRE_DATE" ControlToValidate="txtINSTALLMENT_EXPIRE_DATE"></asp:RequiredFieldValidator>
                                   <asp:RegularExpressionValidator runat="server"  ErrorMessage="" Display="Dynamic" ID="revINSTALLMENT_EXPIRE_DATE" ControlToValidate="txtINSTALLMENT_EXPIRE_DATE"></asp:RegularExpressionValidator>
                                   
                                </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField >
                           <ItemStyle Width="8%" VerticalAlign="Middle" HorizontalAlign="Left" />        
                             <HeaderTemplate>
                                    <asp:Label runat="server" ID="capPREMIUM" Text="">Premiun</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <asp:TextBox runat="server" ReadOnly="true" Width="80px"  CssClass="inputcurrency" MaxLength="15" ID="txtPREMIUM" Text='<%# Convert.ToDouble(Eval("INSTALLMENT_AMOUNT.CurrentValue")).ToString("N", numberFormatInfo) %>'></asp:TextBox>
                               <br /> <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
                                        ID="revPREMIUM" ControlToValidate="txtPREMIUM"></asp:RegularExpressionValidator>
                               </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField>
                                 
                                 <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Left" />        
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capINTEREST_AMOUNT" Text="">Policy Interest Amount</asp:Label>
                                </HeaderTemplate>
                            <ItemTemplate>
                                    <asp:TextBox runat="server" ReadOnly="true" CssClass="inputcurrency" Width="70px" MaxLength="15" ID="txtINTEREST_AMOUNT" Text='<%# Convert.ToDouble(Eval("INTEREST_AMOUNT.CurrentValue")).ToString("N", numberFormatInfo) %>'></asp:TextBox>
                                  <br /> 
                                   <asp:RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="" ID="rfvINTEREST_AMOUNT"
                                        ControlToValidate="txtINTEREST_AMOUNT"></asp:RequiredFieldValidator>
                                  <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
                                        ID="revINTEREST_AMOUNT" ControlToValidate="txtINTEREST_AMOUNT"></asp:RegularExpressionValidator>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField>
                                 
                                 <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Left" />        
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capFEE" Text="">Policy Fee</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ReadOnly="true" Width="70px" CssClass="inputcurrency" MaxLength="15" ID="txtFEE" Text='<%# Convert.ToDouble(Eval("FEE.CurrentValue")).ToString("N", numberFormatInfo) %>'></asp:TextBox>
                                 <br /> <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
                                        ID="revFEE" ControlToValidate="txtFEE"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField>
                                     <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Left" /> 
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capTAXES" Text="">Taxes</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ReadOnly="true" Width="60px" CssClass="inputcurrency" MaxLength="15" ID="txtTAXES" Text='<%# Convert.ToDouble(Eval("TAXES.CurrentValue")).ToString("N", numberFormatInfo) %>'></asp:TextBox>
                               <br />  <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
                                        ID="revTAXES" ControlToValidate="txtTAXES"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                          </asp:TemplateField>
                             
                            <asp:TemplateField>
                                <ItemStyle Width="8%" VerticalAlign="Middle" HorizontalAlign="Left" /> 
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capTOTAL" Text="">Total</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox  runat="server" CssClass="inputcurrency" ReadOnly="true" 
                                        MaxLength="15" ID="txtTOTAL" 
                                        Text='<%# Convert.ToDouble(Eval("TOTAL.CurrentValue")).ToString("N", numberFormatInfo) %>' 
                                        Width="89px"></asp:TextBox>
                                    <input type="hidden" runat="server" id="hidTOTAL" name="hidTOTAL" />
                                </ItemTemplate>
                          </asp:TemplateField>
                           <asp:TemplateField>
                           <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capBOLETO" Text="Boleto"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate >
                                    <a href="javascript:GenerateBoleto('<%# Eval("INSTALLMENT_NO.CurrentValue") %>','','<%# Eval("POLICY_VERSION_ID.CurrentValue") %>','<%# Eval("CO_APPLICANT_ID.CurrentValue") %>');">
                                        <asp:Label runat="server" ID="lblBOLETO">View</asp:Label></a>
                                
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField>
                                 <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="8%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <HeaderTemplate >
                                    <asp:Label runat="server" ID="capEDIT" Text="Edit"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate >
                                    <asp:Button ID="btnEDIT" CssClass="clsButton" runat="server"  />
                                 
                            </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </td>
                </tr>
                 <tr >
                  <td class="midcolora" style="width:50%">                  
                       <asp:Button runat="server" id="btnBack" CssClass="clsButton" Text="Back" 
                           OnClientClick = "javascript:return ReturnBack() " /></td>      
                  <td class="midcolorr" style="width:50%">
                       <cmsb:CmsButton runat="server" id="btnSave" CssClass="clsButton" Text="Save" 
                           onclick="btnSave_Click" /></td>                    
                </tr>
                
            </tbody>
            <tr>
            <td>
                <input type="hidden" runat="server" id="hidCUSTOMER_ID" name="hidCUSTOMER_ID" />
                <input type="hidden" runat="server" id="hidPOLICY_ID" name="hidPOLICY_ID" />
                <input type="hidden" runat="server" id="hidPOLICY_VERSION_ID" name="hidPOLICY_VERSION_ID" />
                <input type="hidden" runat="server" id="hidPOLICY_STATUS" name="hidPOLICY_STATUS" />
                <input type="hidden" runat="server" id="hidGRID_ROW_COUNT" name="hidGRID_ROW_COUNT"  value="0"/>
                <input type="hidden" runat="server" id="hidUPDATEDROWS" name="hidUPDATEDROWS" />
                <input type="hidden" runat="server" id="hidCALLED_FROM" name="hidCALLED_FROM" />
                <input type="hidden" runat="server" id="hidCALLED_FOR" name="hidCALLED_FOR" />
                <input type="hidden" runat="server" id="hidCHANGEDROW" name="hidCHANGEDROW" />
                <input type="hidden" runat="server" id="hidUnSavedMsg" name="hidUnSavedMsg" />
                <input type="hidden" runat="server" id="hidFirstInstallMsg" name="hidFirstInstallMsg" />
                <input type="hidden" runat="server" id="hidSecandInstallMsg" name="hidSecandInstallMsg" />
                <input type="hidden" runat="server" id="hidInsatallExipreMsg" name="hidUnSavedMsg" />
                <input type="hidden" runat="server" id="hidChangedflage" name="hidChangedflage" />
                <input type="hidden" runat="server" id="hidCurrentExpireDate" name="hidCurrentExpireDate" />
                <input type="hidden" runat="server" id="hidPrevExpireDate" name="hidPrevExpireDate" />
                <input type="hidden" runat="server" id="HidInstalDueDateFromCancelMsg" name="HidInstalDueDateFromCancelMsg" />
                
                 
                
            </td>
            </tr>
        </table>
        </form>
    </body>
</html>

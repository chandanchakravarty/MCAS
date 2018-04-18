<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDepositDetails.aspx.cs" Inherits="Cms.Account.Aspx.AddDepositDetails" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Deposit Details page</title>
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
    <script type="text/javascript" type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script>
    <script src="/cms/cmsweb/scripts/calendar.js"></script>

    <script type="text/javascript" language="javascript">
        var jsaAppDtFormat = "<%=aAppDtFormat  %>";
        var DatasetObject = '';
        var DatasetObjectInstall = '';
        function Init() {
           
            ApplyColor();
            ChangeColor();
            if ( $("#txtRECEIPT_AMOUNT").val() == "") 
                ShowAndHideValidator(rfvRECEIPT_AMOUNT, true);
            else
                ValidatorEnable(rfvRECEIPT_AMOUNT, false);

            if ($("#txtTOTAL_PREMIUM_COLLECTION").val() == "" && $("#hidDEPOSIT_TYPE1").val() == "14831")
             ShowAndHideValidator(rfvTOTAL_PREMIUM_COLLECTION, true);
            else
                ValidatorEnable(rfvTOTAL_PREMIUM_COLLECTION, false);
            fnShowAndHideTRusingDepositType();
     }
     function ChkFutureDate(objSource, objArgs) {

         if (document.getElementById("revPAYMENT_DATE").isvalid == true) {

             var effdate = document.getElementById("txtPAYMENT_DATE").value;
             objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
         }
         else
             objArgs.IsValid = true;
     }
     function fnShowAndHideTRusingDepositType() {
         
         var DEPOSIT_TYPE = $("#hidDEPOSIT_TYPE1").val();
         switch (DEPOSIT_TYPE) {
             case "14831": //Normal
                 $("#txtRISK_PREMIUM").attr('readonly', 'true');
                 $("#txtFEE").attr('readonly', 'true');
                 $("#txtTAX").attr('readonly', 'true');
                 $("#txtINTEREST").attr('readonly', 'true');

                 $("#tdBoleto").show();
                 $("#tdPolicyNumber").hide();
                 $("#TrLEADER_POLICY_NUMBER").hide();
                 $("#trCoInsurance").hide();
                 $("#trCoInsurance1").hide();
                 $("#trRefundDeposit").hide();
                 $("#trTOTAL_PREMIUM_COLLECTION").show();

                 ShowAndHideValidator(rfvPOLICY_NUMBER, false);
                 ShowAndHideValidator(rfvOUR_NUMBER, true);
                 ShowAndHideValidator(rfvRECEIPT_AMOUNT, true);
                 ShowAndHideValidator(rfvTOTAL_PREMIUM_COLLECTION, true);
                 ShowAndHideValidator(rfvLEADER_POLICY_NUMBER, false);
                 ShowAndHideValidator(rfvLEADER_ENDORSEMENT_NUMBER, false);
                 ShowAndHideValidator(rfvINSTALLMENT, false);
                 ShowAndHideValidator(rfvINSTALLMENT_RISK_PREMIUM, false);

                 ShowAndHideValidator(rfvPOLICY_DESCRIPTION, false);
                 ShowAndHideValidator(rfvPOLICY_INSTALLMENT, false);
                 ShowAndHideValidator(rfvRECEIPT_FROM_ID, false);
                 ShowAndHideValidator(rfvREFUND_AMOUNT, false);
                 ShowAndHideValidator(revREFUND_AMOUNT, false);

                 //Added ti hide the Coi Total Premium Collection 
                 $("#trCoInsurance2").hide();
                 ShowAndHideValidator(rfvCOI_TOTAL_PREMIUM_COLLECTION, false);
                 ShowAndHideValidator(revCOI_TOTAL_PREMIUM_COLLECTION, false);
                 //Added till here 

                 //ValidatorEnable(rfvOUR_NUMBER, true);


                 //These fields are mandatory when Mode of Receipt (Deposit Information screen) = Check (itrack - 1495)
                 if ($("#hidRECEIPT_MODE").val() != "11975") {
                     //Added to hide the Bank Details of Normal Receipt - itrack- 1495
                     $("#trBankDetails").hide();
                     ShowAndHideValidator(rfvBANK_NUMBER, false);
                     ShowAndHideValidator(revBANK_NUMBER, false);
                     ShowAndHideValidator(rfvBANK_AGENCY_NUMBER, false);
                     ShowAndHideValidator(revBANK_AGENCY_NUMBER, false);
                     ShowAndHideValidator(rfvBANK_ACCOUNT_NUMBER, false);
                     ShowAndHideValidator(revBANK_ACCOUNT_NUMBER, false);
                     //Added till here 
                 }



                 break;
             case "14832": //Co-Insurance
                
                 $("#tdBoleto").hide();
                 $("#trTOTAL_PREMIUM_COLLECTION").hide();
                 $("#tdPolicyNumber").hide();

                 $("#trNormalType").hide();
                 $("#trNormalType1").hide();
                 ShowAndHideValidator(rfvTOTAL_PREMIUM_COLLECTION, false);
                 ShowAndHideValidator(rfvOUR_NUMBER, false);
                 ShowAndHideValidator(rfvPOLICY_NUMBER, false);
                 ShowAndHideValidator(rfvRECEIPT_AMOUNT, false);
                 ShowAndHideValidator(revRECEIPT_AMOUNT, false);
                 ShowAndHideValidator(revRISK_PREMIUM, false);
                 ShowAndHideValidator(revFEE, false);
                 ShowAndHideValidator(revTAX, false);
                 ShowAndHideValidator(revINTEREST, false);
                 ShowAndHideValidator(revLATE_FEE, false);
                 ShowAndHideValidator(revTOTAL_PREMIUM_COLLECTION, false);
                 ShowAndHideValidator(rfvRISK_PREMIUM, false);
                
                 $("#txtNET_INSTALLMENT_AMOUNT").attr('readonly', 'true');
                 $("#trRefundDeposit").hide();
                 ShowAndHideValidator(rfvPOLICY_DESCRIPTION, false);
                 ShowAndHideValidator(rfvPOLICY_INSTALLMENT, false);
                 ShowAndHideValidator(rfvRECEIPT_FROM_ID, false);
                 ShowAndHideValidator(rfvREFUND_AMOUNT, false);
                 ShowAndHideValidator(revREFUND_AMOUNT, false);
    
                 if ($("#cmbINSTALLMENT option:selected").text() == "")
                     ShowAndHideValidator(rfvINSTALLMENT, true);
                 else
                     ValidatorEnable(rfvINSTALLMENT, false);

                 if ($("#cmbLEADER_ENDORSEMENT_NUMBER option:selected").text() == "")
                     ShowAndHideValidator(rfvLEADER_ENDORSEMENT_NUMBER, true);
                 else
                     ValidatorEnable(rfvLEADER_ENDORSEMENT_NUMBER, false);
                 //Comment to display Receipt date for Co-Insurance
                 //$("#tdPAYMENT_DATE").hide();
                 //ShowAndHideValidator(rfvPAYMENT_DATE, false);
                 //ShowAndHideValidator(revPAYMENT_DATE, false);
                 //ShowAndHideValidator(csvPAYMENT_DATE, false);
                 //Commented till  till 
                 $("#tdPAYMENT_DATE").attr('colspan', 3);

                 //Added By Pradeep -- Make read only or disabled controls for Co-Insurance as discussed by Anurag
                 $("#txtINSTALLMENT_RISK_PREMIUM").attr('readonly', 'true');
                 $("#txtINTEREST_AMOUNT").attr('readonly', 'true');
                 $("#txtCO_COMMISSION_AMOUNT").attr('readonly', 'true');
                 $("#txtBROKER_COMMISSION_AMOUNT").attr('readonly', 'true');
                 $("#txtNET_INSTALLMENT_AMOUNT").attr('readonly', 'true');
                 //Added till here 
                 //Added to hide the Bank Details of Normal Receipt - itrack- 1495
                 $("#trBankDetails").hide();
                 ShowAndHideValidator(rfvBANK_NUMBER, false);
                 ShowAndHideValidator(revBANK_NUMBER, false);
                 ShowAndHideValidator(rfvBANK_AGENCY_NUMBER, false);
                 ShowAndHideValidator(revBANK_AGENCY_NUMBER, false);
                 ShowAndHideValidator(rfvBANK_ACCOUNT_NUMBER, false);
                 ShowAndHideValidator(revBANK_ACCOUNT_NUMBER, false);
                 //Added till here 
                 break;
             case "14916": //Broker Refund
             case "14917": //Reinsurance Refund
             case "14918": //Ceded CO Refund
                 $("#tdBoleto").hide();
                 $("#TrLEADER_POLICY_NUMBER").hide();
                 $("#trCoInsurance").hide();
                 $("#trCoInsurance1").hide();
                 $("#trNormalType").hide();
                 $("#trNormalType1").hide();
                 $("#trTOTAL_PREMIUM_COLLECTION").hide();


                 ShowAndHideValidator(rfvTOTAL_PREMIUM_COLLECTION, false);
                 ShowAndHideValidator(rfvOUR_NUMBER, false);
                
                 ShowAndHideValidator(rfvRECEIPT_AMOUNT, false);
                 ShowAndHideValidator(revRECEIPT_AMOUNT, false);
                 ShowAndHideValidator(revRISK_PREMIUM, false);
                 ShowAndHideValidator(revFEE, false);
                 ShowAndHideValidator(revTAX, false);
                 ShowAndHideValidator(revINTEREST, false);
                 ShowAndHideValidator(revLATE_FEE, false);
                 ShowAndHideValidator(revTOTAL_PREMIUM_COLLECTION, false);
                 ShowAndHideValidator(rfvRISK_PREMIUM, false);
               

                 ShowAndHideValidator(rfvLEADER_POLICY_NUMBER, false);
                 ShowAndHideValidator(rfvLEADER_ENDORSEMENT_NUMBER, false);
                 ShowAndHideValidator(rfvINSTALLMENT, false);
                 ShowAndHideValidator(rfvINSTALLMENT_RISK_PREMIUM, false);
                 ShowAndHideValidator(revINSTALLMENT_RISK_PREMIUM, false);

                 ShowAndHideValidator(revINTEREST_AMOUNT, false);
                 ShowAndHideValidator(revCO_COMMISSION_AMOUNT, false);
                 ShowAndHideValidator(revBROKER_COMMISSION_AMOUNT, false);
                 ShowAndHideValidator(revNET_INSTALLMENT_AMOUNT, false);
                 
                 if ($("#cmbPOLICY_DESCRIPTION option:selected").text() == "")
                     ShowAndHideValidator(rfvPOLICY_DESCRIPTION, true);
                 else
                     ValidatorEnable(rfvPOLICY_DESCRIPTION, false);

                 if ($("#cmbPOLICY_INSTALLMENT option:selected").text() == "")
                     ShowAndHideValidator(rfvPOLICY_INSTALLMENT, true);
                 else
                     ValidatorEnable(rfvPOLICY_INSTALLMENT, false);
                 if ($("#cmbRECEIPT_FROM_ID option:selected").text() == "")
                     ShowAndHideValidator(rfvRECEIPT_FROM_ID, true);
                 else
                     ValidatorEnable(rfvRECEIPT_FROM_ID, false);
                //Itrack-1515
                 //Comment to display Receipt date 
                //$("#tdPAYMENT_DATE").hide();
                //ShowAndHideValidator(rfvPAYMENT_DATE, false);
                //ShowAndHideValidator(revPAYMENT_DATE, false);
                 //ShowAndHideValidator(csvPAYMENT_DATE, false);
                 $("#tdPAYMENT_DATE").attr('colspan', 3);
                 //till here 
                 
                 //Added to hide the Coi Total Premium Collection 
                 $("#trCoInsurance2").hide();
                 ShowAndHideValidator(rfvCOI_TOTAL_PREMIUM_COLLECTION, false);
                 ShowAndHideValidator(revCOI_TOTAL_PREMIUM_COLLECTION, false);
                 //Added till here 


                 //Added to hide the Bank Details of Normal Receipt - itrack- 1495
                 $("#trBankDetails").hide();
                 ShowAndHideValidator(rfvBANK_NUMBER, false);
                 ShowAndHideValidator(revBANK_NUMBER, false);
                 ShowAndHideValidator(rfvBANK_AGENCY_NUMBER, false);
                 ShowAndHideValidator(revBANK_AGENCY_NUMBER, false);
                 ShowAndHideValidator(rfvBANK_ACCOUNT_NUMBER, false);
                 ShowAndHideValidator(revBANK_ACCOUNT_NUMBER, false);
                 //Added till here 

                 break;
             default:
                 break;
         }


     }
    
    function ShowAndHideValidator(validatorId, flag) {
       if (flag == true)//show
        document.getElementById(validatorId.id).setAttribute('enabled', true);
        else if (flag == false)//hide
        document.getElementById(validatorId.id).setAttribute('enabled', false);
    }
   
    function ResetTheForm() {
        document.ACT_CURRENT_DEPOSIT_LINE_ITEMS.reset();
    }
   
    //This function open the policy lookup window
    function OpenPolicyLookup() {
        var url = '<%=URL%>';
        var Policy = document.getElementById('hidPolicy').value;
       OpenLookupWithFunction(url, 'POLICY_APP_NUMBER', 'CUSTOMER_ID_NAME', 'hidPOLICYINFO', $("#txtPOLICY_NUMBER").text().trim(), 'DBPolicy', Policy , '', 'splitPolicy()');
    }
    //This function splits the policyid and policy version id and put it in different controls
    function splitPolicy() {
        if (document.getElementById("hidPOLICYINFO").value.length > 0) {
          
            var arr = document.getElementById("hidPOLICYINFO").value.split("~");
            $("#hidPOLICY_ID").val(arr[0]);
            $("#hidPOLICY_VERSION_ID").val(arr[1]);
            $("#txtPOLICY_NUMBER").val(arr[2]);
            $("#hidCUSTOMER_ID").val(arr[6]);
            
            var CUSTOMER_ID = $("#hidCUSTOMER_ID").val();
            var POLICY_ID = $("#hidPOLICY_ID").val();
            var POLICY_VERSION_ID = $("#hidPOLICY_VERSION_ID").val();

            if ($("#txtPOLICY_NUMBER").val().trim() != '' && $("#txtPOLICY_NUMBER").val().trim().length >= 11) {
                
                var POLICY_NUMBER = $("#txtPOLICY_NUMBER").val();

                var result = AddDepositDetails.AjaxPolicyDetailsInfo(POLICY_NUMBER);
                fillDTCombo(result.value, document.getElementById('cmbPOLICY_DESCRIPTION'), 'POLICY_VERSION_ID', 'POLICY_DESCRIPTION', 0);
            }
            else {
                alert($('#hidPolicyNumberMsg').val());
                $("#txtPOLICY_NUMBER").val('');
            }
        }
    }
    </script>
    <script type="text/javascript" language="javascript">
        function fillDTCombo(objDT, combo, valID, txtDesc, tabIndex) {
            //debugger;
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
        //Format the Branch bank itrack 1495
        function fnFormatBranchBank(vr) {
            
            var vr = new String(vr.toString());
            if (vr != "") {
                vr = vr.replace(/[-]/g, "");
                num = vr.length;
                if (num == 5) {
                    vr = vr.substr(0, 4) + '-' + vr.substr(4, 1);
                }

            }
            return vr;
        }
        $(document).ready(function () {
            
           

            $("#btnAccept").click(function () {
                if ($("#cmbAPPROVE_REFUND option:selected").val() == "") {
                    ValidatorEnable(rfvAPPROVE_REFUND, true);
                    return false;
                }
            });
            //Added By Pradeep Kushwaha on 13-May-2011 for Leader policy # Validation
            $("#txtLEADER_POLICY_NUMBER").blur(function () {
                //Modified the LEADER_POLICY_NUMBER's lenght from 7 to 5 for the itrack-1597
                if ($("#txtLEADER_POLICY_NUMBER").val().trim() != '' && $("#txtLEADER_POLICY_NUMBER").val().trim().length >= 5) {
                    var LEADER_POLICY_NUMBER = $("#txtLEADER_POLICY_NUMBER").val();
                    var result = AddDepositDetails.AjaxLeaderPolicyDetailsInfo(LEADER_POLICY_NUMBER);
                    fillDTCombo(result.value, document.getElementById('cmbLEADER_ENDORSEMENT_NUMBER'), 'POLICY_VERSION_ID', 'CO_ENDORSEMENT_NO', 0);
                    if (result.value.Tables[1].Rows.length > 0 && result.value.Tables[0].Rows.length > 0) {//itrack 1148 to get the details only if the policy is committed As discussed by Anurag
                        $("#hidCUSTOMER_ID").val(result.value.Tables[1].Rows[0]["CUSTOMER_ID"]);
                        $("#hidPOLICY_ID").val(result.value.Tables[1].Rows[0]["POLICY_ID"]);
                        $("#hidPOLICY_VERSION_ID").val(result.value.Tables[1].Rows[0]["POLICY_VERSION_ID"]);
                        $("#hidRECEIPT_FROM_ID").val(result.value.Tables[1].Rows[0]["COINSURANCE_ID"]);
                        ShowAndHideValidator(rfvLEADER_POLICY_NUMBER, false);
                    }
                    else {
                        alert($("#hidLeaderPolicyNumberMsg").val());
                        $("#txtLEADER_POLICY_NUMBER").val('');
                    }
                }
                else {
                    alert($("#hidLeaderPolicyNumberMsg").val());
                    $("#txtLEADER_POLICY_NUMBER").val('');
                }
            });
            $("#cmbLEADER_ENDORSEMENT_NUMBER").change(function () {
                if ($("#cmbLEADER_ENDORSEMENT_NUMBER option:selected").val() != '') {

                    var LEADER_POLICY_NUMBER = $("#txtLEADER_POLICY_NUMBER").val();

                    $("#hidLEADER_ENDORSEMENT_NUMBER").val($("#cmbLEADER_ENDORSEMENT_NUMBER option:selected").text());

                    $("#hidPOLICY_VERSION_ID").val($("#cmbLEADER_ENDORSEMENT_NUMBER option:selected").val());

                    var result = AddDepositDetails.AjaxGetInstallmentDetailsInfo($("#hidCUSTOMER_ID").val(), $("#hidPOLICY_ID").val(), $("#hidPOLICY_VERSION_ID").val(), 0);
                    fillDTCombo(result.value, document.getElementById('cmbINSTALLMENT'), 'INSTALLMENT_NO', 'INSTALLMENT', 0);
                    ShowAndHideValidator(rfvLEADER_ENDORSEMENT_NUMBER, false);
                }
            });
            $("#cmbINSTALLMENT").change(function () {
                if ($("#cmbINSTALLMENT option:selected").val() != '') {
                    $("#hidINSTALLMENT_NO").val($("#cmbINSTALLMENT option:selected").val());
                    $("#hidLEADER_ENDORSEMENT_NUMBER").val($("#cmbLEADER_ENDORSEMENT_NUMBER option:selected").text());

                    var result = AddDepositDetails.AjaxGetInstallmentDetailsInfo($("#hidCUSTOMER_ID").val(), $("#hidPOLICY_ID").val(), $("#hidPOLICY_VERSION_ID").val(), $("#hidINSTALLMENT_NO").val());
                    if (result.value.Tables[0].Rows.length > 0) {
                        $("#txtINSTALLMENT_RISK_PREMIUM").addClass("InputCurrency");
                        $("#txtINTEREST_AMOUNT").addClass("InputCurrency");
                        $("#txtINSTALLMENT_RISK_PREMIUM").val(formatBaseCurrencyAmount(result.value.Tables[0].Rows[0]["INSTALLMENT_AMOUNT"]));
                        $("#txtINTEREST_AMOUNT").val(formatBaseCurrencyAmount(result.value.Tables[0].Rows[0]["INTEREST_AMOUNT"]));
                        $("#txtCO_COMMISSION_AMOUNT").val(formatBaseCurrencyAmount(result.value.Tables[0].Rows[0]["CO_COMM_AMT"])); //Co-commission Amount Added As discussed by Anurag itrack-1148
                        fnCalculateNetIntallmentAmount();

                    }

                    ShowAndHideValidator(rfvINSTALLMENT, false);

                }
            });
            function fnCalculateNetIntallmentAmount() {
                var txtINSTALLMENT_RISK_PREMIUM = document.getElementById("txtINSTALLMENT_RISK_PREMIUM").value == '' ? document.getElementById("txtINSTALLMENT_RISK_PREMIUM").value = '0' : document.getElementById("txtINSTALLMENT_RISK_PREMIUM").value;
                txtINSTALLMENT_RISK_PREMIUM = FormatAmountForSum(txtINSTALLMENT_RISK_PREMIUM);

                var txtCO_COMMISSION_AMOUNT = document.getElementById("txtCO_COMMISSION_AMOUNT").value == '' ? document.getElementById("txtCO_COMMISSION_AMOUNT").value = '0' : document.getElementById("txtCO_COMMISSION_AMOUNT").value;
                txtCO_COMMISSION_AMOUNT = FormatAmountForSum(txtCO_COMMISSION_AMOUNT);

                var txtBROKER_COMMISSION_AMOUNT = document.getElementById("txtBROKER_COMMISSION_AMOUNT").value == '' ? document.getElementById("txtBROKER_COMMISSION_AMOUNT").value = '0' : document.getElementById("txtBROKER_COMMISSION_AMOUNT").value;
                txtBROKER_COMMISSION_AMOUNT = FormatAmountForSum(txtBROKER_COMMISSION_AMOUNT);

                var txtINTEREST_AMOUNT = document.getElementById("txtINTEREST_AMOUNT").value == '' ? document.getElementById("txtINTEREST_AMOUNT").value = '0' : document.getElementById("txtINTEREST_AMOUNT").value;
                txtINTEREST_AMOUNT = FormatAmountForSum(txtINTEREST_AMOUNT);

                var TOTAL = parseFloat(txtINSTALLMENT_RISK_PREMIUM) - parseFloat(txtBROKER_COMMISSION_AMOUNT) - parseFloat(txtCO_COMMISSION_AMOUNT) + parseFloat(txtINTEREST_AMOUNT);

                if (!isNaN(TOTAL)) {
                    $("#txtNET_INSTALLMENT_AMOUNT").val(formatBaseCurrencyAmount(parseFloat(TOTAL)));
                    $("#hidNET_INSTALLMENT_AMOUNT").val(formatBaseCurrencyAmount(parseFloat(TOTAL)));
                    $("#txtCOI_TOTAL_PREMIUM_COLLECTION").val(formatBaseCurrencyAmount(parseFloat(TOTAL))); //Added by Pradeep to get the total Premium Colelction for Co-insurance 
                }

            }
            $("#cmbPOLICY_DESCRIPTION").change(function () {

                if ($("#cmbPOLICY_DESCRIPTION option:selected").val() != '') {

                    $("#hidPOLICY_VERSION_ID").val($("#cmbPOLICY_DESCRIPTION option:selected").val());
                    var result = AddDepositDetails.AjaxGetInstallmentDetailsInfo($("#hidCUSTOMER_ID").val(), $("#hidPOLICY_ID").val(), $("#hidPOLICY_VERSION_ID").val(), 0);
                    DatasetObjectInstall = '';
                    DatasetObjectInstall = result.value;
                    fillDTCombo(result.value, document.getElementById('cmbPOLICY_INSTALLMENT'), 'INSTALLMENT_NO', 'INSTALLMENT', 0);

                    //ShowAndHideValidator(rfvPOLICY_INSTALLMENT, false);

                }
            });
            $("#cmbPOLICY_INSTALLMENT").change(function () {
                if ($("#cmbPOLICY_INSTALLMENT option:selected").val() != '') {
                    //debugger;
                    $("#hidINSTALLMENT_NO").val($("#cmbPOLICY_INSTALLMENT option:selected").val());

                    var result = AddDepositDetails.AjaxGetInsurerInfo($("#hidCUSTOMER_ID").val(), $("#hidPOLICY_ID").val(), $("#hidPOLICY_VERSION_ID").val());
                    DatasetObject = '';
                    DatasetObject = result.value;

                    var DEPOSIT_TYPE = $("#hidDEPOSIT_TYPE1").val();
                    var TableNo = 0;
                    switch (DEPOSIT_TYPE) {
                        case "14916": //Broker Refund
                            TableNo = 0;
                            break;
                        case "14917": //Reinsurance Refund
                            TableNo = 1;
                            break;
                        case "14918": //Ceded CO Refund
                            TableNo = 2;
                            break;
                        default:
                            TableNo = 0;
                            break;
                    }
                    if (DatasetObjectInstall != null && typeof (DatasetObjectInstall) == "object" && DatasetObjectInstall.Tables != null) {
                        for (var i = 0; i < DatasetObjectInstall.Tables[0].Rows.length; ++i) {
                            if (DatasetObjectInstall.Tables[0].Rows[i]["INSTALLMENT_NO"] == $("#cmbPOLICY_INSTALLMENT option:selected").val()) {
                                $("#hidINSTALLMENT_AMOUNT").val(DatasetObjectInstall.Tables[0].Rows[i]["INSTALLMENT_AMOUNT"]);
                                break;
                            }
                        }
                    }
                    fillDTCombo(result.value, document.getElementById('cmbRECEIPT_FROM_ID'), 'RECEIPT_FROM_ID', 'RECEIPT_FROM_NAME', TableNo);

                }
            });
            function fnCalculateRefundAmount() {
                //debugger;
                var RiskPREMIUM = document.getElementById("hidINSTALLMENT_AMOUNT").value == '' ? document.getElementById("hidINSTALLMENT_AMOUNT").value = '0' : document.getElementById("hidINSTALLMENT_AMOUNT").value;
                //RiskPREMIUM = FormatAmountForSum(RiskPREMIUM);

                var COMM_PERCENT = document.getElementById("hidCOMM_PERCENT").value == '' ? document.getElementById("hidCOMM_PERCENT").value = '0' : document.getElementById("hidCOMM_PERCENT").value;
                //COMM_PERCENT = FormatAmountForSum(COMM_PERCENT);


                //ADDED BY ATUL FOT I-TRACK 1172 Notes Added for Broker refund

                var COMM_PERCENT_POLICY_LEVEL = document.getElementById("hidCOMM_POLICY_LEVEL").value == '' ? document.getElementById("hidCOMM_POLICY_LEVEL").value = '0' : document.getElementById("hidCOMM_POLICY_LEVEL").value;
                //COMM_PERCENT_POLICY_LEVEL = FormatAmountForSum(COMM_PERCENT_POLICY_LEVEL);

                if (COMM_PERCENT_POLICY_LEVEL == '0') {
                    var RefundTOTAL = (parseFloat(RiskPREMIUM) * parseFloat(COMM_PERCENT) / 100);
                }
                else {
                    var RefundTOTAL = ((parseFloat(RiskPREMIUM) * parseFloat(COMM_PERCENT_POLICY_LEVEL) / 100) * parseFloat(COMM_PERCENT) / 100);
                }
                if (!isNaN(RefundTOTAL)) {
                    $("#txtREFUND_AMOUNT").val(formatBaseCurrencyAmount(RefundTOTAL));
                    $("#txtREFUND_AMOUNT").addClass("InputCurrency");
                    $("#hidREFUND_CAL_AMOUNT").val(formatBaseCurrencyAmount(RefundTOTAL));
                }

            }
            $("#txtREFUND_AMOUNT").change(function () {
                //debugger;
                var DEPOSIT_TYPE = $("#hidDEPOSIT_TYPE1").val();
                switch (DEPOSIT_TYPE) {
                    case "14916": //Broker Refund
                    case "14918": //Ceded CO Refund
                        var REFUND_CAL_AMOUNT = document.getElementById("hidREFUND_CAL_AMOUNT").value == '' ? document.getElementById("hidREFUND_CAL_AMOUNT").value = '0' : document.getElementById("hidREFUND_CAL_AMOUNT").value;
                        REFUND_CAL_AMOUNT = FormatAmountForSum(REFUND_CAL_AMOUNT);

                        var REFUND_AMOUNT = document.getElementById("txtREFUND_AMOUNT").value == '' ? document.getElementById("txtREFUND_AMOUNT").value = '0' : document.getElementById("txtREFUND_AMOUNT").value;
                        REFUND_AMOUNT = FormatAmountForSum(REFUND_AMOUNT);

                        if (!isNaN(REFUND_CAL_AMOUNT)) {
                            if (parseFloat(REFUND_CAL_AMOUNT) > parseFloat(REFUND_AMOUNT) ||
                                parseFloat(REFUND_CAL_AMOUNT) < parseFloat(REFUND_AMOUNT)) {
                                $("#capExceptionRefund").text($("#hidExceptionMsg").val());
                                $("#hidEXCEPTION").val("YES");

                                if (DEPOSIT_TYPE == "14916")
                                    $("#hidEXCEPTION_REASON").val("406"); //406 -Change in Broker Refund Amount.
                                if (DEPOSIT_TYPE == "14918")
                                    $("#hidEXCEPTION_REASON").val("407"); //407 -Change in Follower Refund Amount.
                            }
                            else {
                                $("#hidEXCEPTION_REASON").val('');
                                $("#capExceptionRefund").text('');
                                $("#hidEXCEPTION").val("NO");
                            }
                        }
                        else {
                            $("#hidEXCEPTION_REASON").val('');
                            $("#hidEXCEPTION").val('');
                        }
                        break;
                    default:
                        break;
                }

            });
            $("#cmbRECEIPT_FROM_ID").change(function () {////debugger
                if ($("#cmbRECEIPT_FROM_ID option:selected").val() != '') {
                    //debugger;
                    $("#hidRECEIPT_FROM_ID").val($("#cmbRECEIPT_FROM_ID option:selected").val());

                    var DEPOSIT_TYPE = $("#hidDEPOSIT_TYPE1").val();
                    var TableNo = 0;
                    switch (DEPOSIT_TYPE) {
                        case "14916": //Broker Refund
                            TableNo = 0;
                            break;
                        case "14917": //Reinsurance Refund
                            TableNo = 1;
                            break;
                        case "14918": //Ceded CO Refund
                            TableNo = 2;
                            break;
                        default:
                            TableNo = 0;
                            break;
                    }




                    if (DatasetObject != null && typeof (DatasetObject) == "object" && DatasetObject.Tables != null) {
                        for (var i = 0; i < DatasetObject.Tables[TableNo].Rows.length; ++i) {
                            if (DatasetObject.Tables[TableNo].Rows[i]["RECEIPT_FROM_ID"] == $("#hidRECEIPT_FROM_ID").val() &&
                                DatasetObject.Tables[TableNo].Rows[i]["RECEIPT_FROM_NAME"] == $("#cmbRECEIPT_FROM_ID option:selected").text()) {

                                $("#hidCOMM_PERCENT").val(DatasetObject.Tables[TableNo].Rows[i]["COMM_PERCENT"]);
                                //ADDED BY ATUL FOT I-TRACK 1172 Notes Added for Broker refund
                                $("#hidCOMM_POLICY_LEVEL").val(DatasetObject.Tables[TableNo].Rows[i]["POLICY_LEVEL_COMMISSION"]);
                                break;
                            }
                        }
                    }
                    ShowAndHideValidator(rfvRECEIPT_FROM_ID, false);
                    fnCalculateRefundAmount();
                }
            });
            //Added till here
            $("#txtPOLICY_NUMBER").blur(function () {
                //debugger;
                if ($("#txtPOLICY_NUMBER").val().trim() != '' && $("#txtPOLICY_NUMBER").val().trim().length >= 11) {
                    var POLICY_NUMBER = $("#txtPOLICY_NUMBER").val();

                    var result = AddDepositDetails.AjaxPolicyDetailsInfo(POLICY_NUMBER);
                    fillDTCombo(result.value, document.getElementById('cmbPOLICY_DESCRIPTION'), 'POLICY_VERSION_ID', 'POLICY_DESCRIPTION', 0);
                    if (result.value.Tables[1].Rows.length > 0) {
                        $("#hidCUSTOMER_ID").val(result.value.Tables[1].Rows[0]["CUSTOMER_ID"]);
                        $("#hidPOLICY_ID").val(result.value.Tables[1].Rows[0]["POLICY_ID"]);
                        //ShowAndHideValidator(rfvPOLICY_NUMBER, false);
                    }
                    else {
                        alert($('#hidPolicyNumberMsg').val());
                        $("#txtPOLICY_NUMBER").val('');
                    }

                }
                else {
                    alert($('#hidPolicyNumberMsg').val());
                    $("#txtPOLICY_NUMBER").val('');
                }
            });

            $("#txtOUR_NUMBER").blur(function () {
                //debugger;
                if ($("#txtOUR_NUMBER").val().trim() != '' && $("#txtOUR_NUMBER").val().trim().length <= 13) {

                    var OUR_NUMBER = $("#txtOUR_NUMBER").val();
                    CallPageMethod("GetInstallmentDetails", ["OUR_NUMBER", OUR_NUMBER], AjaxSucceeded, AjaxFailed);
                }
                else {
                    alert($('#hidOurNumberMsg').val());
                    $("#txtOUR_NUMBER").val('');
                }
            });
            var DEPOSIT_TYPE = $("#hidDEPOSIT_TYPE1").val();

            $("#txtLATE_FEE").change(function () {
                CalculateReceiptAmount();
            });
            //Added by Pradeep on 24-March-2011 for itrack 913
            $("#txtTOTAL_PREMIUM_COLLECTION").change(function () {
                $("#txtTOTAL_PREMIUM_COLLECTION").addClass("InputCurrency");

                //ClaculateTotalPremiumCollection();
                $("#btnSave").focus();
                return true;
            });
            $("#btnSave").click(function () {
                //                //debugger;
                //                Page_ClientValidate() 
            });

            //For Co-Insurance 
            $("#txtINSTALLMENT_RISK_PREMIUM").change(function () {
                fnCalculateNetInstallmentAmount();
            });
            $("#txtINTEREST_AMOUNT").change(function () {
                fnCalculateNetInstallmentAmount();
            });
            $("#txtCO_COMMISSION_AMOUNT").change(function () {
                fnCalculateNetInstallmentAmount();
            });
            $("#txtBROKER_COMMISSION_AMOUNT").change(function () {
                fnCalculateNetInstallmentAmount();
            });
            //Added till here 
        });

        
          function FormatAmountForSum(num) {
              num = ReplaceAll(num, sBaseGroupSep, '#');
              num = ReplaceAll(num, sBaseDecimalSep, '.');
              num = ReplaceAll(num, '#', '');
            return num;
          }
          //Added by Pradeep on 16-Jun-2011 
          //Calculate Net Installment Amount for the Co-Insurance
          function fnCalculateNetInstallmentAmount() {
              
              var txtINSTALLMENT_RISK_PREMIUM = document.getElementById("txtINSTALLMENT_RISK_PREMIUM").value == '' ? document.getElementById("txtINSTALLMENT_RISK_PREMIUM").value = '0' : document.getElementById("txtINSTALLMENT_RISK_PREMIUM").value;
              txtINSTALLMENT_RISK_PREMIUM = FormatAmountForSum(txtINSTALLMENT_RISK_PREMIUM);

              var txtCO_COMMISSION_AMOUNT = document.getElementById("txtCO_COMMISSION_AMOUNT").value == '' ? document.getElementById("txtCO_COMMISSION_AMOUNT").value = '0' : document.getElementById("txtCO_COMMISSION_AMOUNT").value;
              txtCO_COMMISSION_AMOUNT = FormatAmountForSum(txtCO_COMMISSION_AMOUNT);

              var txtBROKER_COMMISSION_AMOUNT = document.getElementById("txtBROKER_COMMISSION_AMOUNT").value == '' ? document.getElementById("txtBROKER_COMMISSION_AMOUNT").value = '0' : document.getElementById("txtBROKER_COMMISSION_AMOUNT").value;
              txtBROKER_COMMISSION_AMOUNT = FormatAmountForSum(txtBROKER_COMMISSION_AMOUNT);

              var txtINTEREST_AMOUNT = document.getElementById("txtINTEREST_AMOUNT").value == '' ? document.getElementById("txtINTEREST_AMOUNT").value = '0' : document.getElementById("txtINTEREST_AMOUNT").value;
              txtINTEREST_AMOUNT = FormatAmountForSum(txtINTEREST_AMOUNT);

              var TOTAL = parseFloat(txtINSTALLMENT_RISK_PREMIUM) - parseFloat(txtBROKER_COMMISSION_AMOUNT) - parseFloat(txtCO_COMMISSION_AMOUNT) + parseFloat(txtINTEREST_AMOUNT);
              if (!isNaN(TOTAL)) {
                  //debugger;
                  //The tolerance limits for accepted coinsurance
                  var TOLERANCE_LIMIT_PERCENTAGE = $("#hidCOACC_TOLERANCE_LIMIT_PERCENTAGE").val();
                  var TOLERANCE_LIMIT_AMOUNT = $("#hidCOACC_TOLERANCE_LIMIT_AMOUNT").val();
                  //Code to check Tolerance limit for Amount
                  //Code to check Tolerance limit for percentage
                  TOLERANCE_LIMIT_AMOUNT = FormatAmountForSum(TOLERANCE_LIMIT_AMOUNT);
                  TOLERANCE_LIMIT_PERCENTAGE = FormatAmountForSum(TOLERANCE_LIMIT_PERCENTAGE);
                  var OLD_TOTAL = '';
                  if ($("#hidCD_LINE_ITEM_ID").val() != "NEW")
                      OLD_TOTAL = $("#hidNET_INSTALLMENT_AMOUNT_ON_UPDATE").val();
                  else
                      OLD_TOTAL = $("#hidNET_INSTALLMENT_AMOUNT").val();
                      
                  OLD_TOTAL = FormatAmountForSum(OLD_TOTAL);
                  var DEF_VALUE = parseFloat(TOTAL) - parseFloat(OLD_TOTAL);
                  DEF_VALUE = roundNumber(DEF_VALUE, 2);
                  DEF_VALUE = Math.abs(DEF_VALUE);
                  
                  var TOLERANCE_LIMIT_PERCENTAGE_AMOUNT = (parseFloat(OLD_TOTAL) * parseFloat(TOLERANCE_LIMIT_PERCENTAGE) / 100);
                  if ((parseFloat(DEF_VALUE) > parseFloat(TOLERANCE_LIMIT_AMOUNT))
                    || (parseFloat(DEF_VALUE) > parseFloat(TOLERANCE_LIMIT_PERCENTAGE_AMOUNT)))  
                   {
                       $("#capIS_EXCEPTION_COINS").text($("#hidExceptionMsg").val());
                       $("#hidEXCEPTION").val("YES");
                       //Modified for Itrack - 1148/1363 as discussed by Anurag
                       //434- Payment against installment already paid.
                       //435-Payment against installment already in progress.
                       if ($("#hidEXCEPTION_REASON").val() != "435" && $("#hidEXCEPTION_REASON").val() != "434") {
                           $("#hidEXCEPTION_REASON").val("409"); //Change in Original Net Installment Amount.
                       }
                       
                  }
                   else {
                       //Modified for Itrack - 1148/1363 as discussed by Anurag
                      if ($("#hidEXCEPTION_REASON").val() != "435" && $("#hidEXCEPTION_REASON").val() != "434") {
                          $("#capIS_EXCEPTION_COINS").text('');
                          $("#hidEXCEPTION").val("NO");
                          $("#hidEXCEPTION_REASON").val('');
                      }//Till here 
                      
                  }
                   $("#txtNET_INSTALLMENT_AMOUNT").val(formatBaseCurrencyAmount(parseFloat(TOTAL)));
                   $("#hidNET_INSTALLMENT_AMOUNT").val(formatBaseCurrencyAmount(parseFloat(TOTAL)));
              }
               else {
                   //Modified for Itrack - 1148/1363 as discussed by Anurag
                  if ($("#hidEXCEPTION_REASON").val() != "435" && $("#hidEXCEPTION_REASON").val() != "434") {
                      $("#capIS_EXCEPTION_COINS").text('');
                      $("#hidEXCEPTION").val('');
                  }//Till here 
                  
              }
          }
        //Added till here 
        //this fucntion use only to set the exception while update
     
        //Calculate Total amount i.e Receipt Amount=Total Risk Preimum + Fee +Taxes+interest+Late fee
        function CalculateReceiptAmount(obj) {
            //debugger;
            var txtRiskPREMIUM = document.getElementById("txtRISK_PREMIUM").value == '' ? document.getElementById("txtRISK_PREMIUM").value = '0' : document.getElementById("txtRISK_PREMIUM").value;
            txtRiskPREMIUM = FormatAmountForSum(txtRiskPREMIUM);

            var txtFEE = document.getElementById("txtFEE").value == '' ? document.getElementById("txtFEE").value = '0' : document.getElementById("txtFEE").value;
            txtFEE = FormatAmountForSum(txtFEE);

            var txtTAX = document.getElementById("txtTAX").value == '' ? document.getElementById("txtTAX").value = '0' : document.getElementById("txtTAX").value;
            txtTAX = FormatAmountForSum(txtTAX);

            var txtINTEREST = document.getElementById("txtINTEREST").value == '' ? document.getElementById("txtINTEREST").value = '0' : document.getElementById("txtINTEREST").value;
            txtINTEREST = FormatAmountForSum(txtINTEREST);
    
            var txtLATE_FEE = document.getElementById("txtLATE_FEE").value == '' ? document.getElementById("txtLATE_FEE").value = '0' : document.getElementById("txtLATE_FEE").value;
            txtLATE_FEE = FormatAmountForSum(txtLATE_FEE);
            
            var TOTAL = parseFloat(txtRiskPREMIUM) + parseFloat(txtFEE) + parseFloat(txtTAX) + parseFloat(txtINTEREST) + parseFloat(txtLATE_FEE);
          
            if (!isNaN(TOTAL)) {
                if ($("#hidDEPOSIT_TYPE1").val() == "14831")//If the Deposit Type Normal(For Boleto receiving)
                 {
                     
                    var TOLERANCE_LIMIT = $("#hidBOLETO_TOLERANCE_LIMIT").val();
                    //TOLERANCE_LIMIT = 0;
                    TOLERANCE_LIMIT = FormatAmountForSum(TOLERANCE_LIMIT);

                    var OLD_TOTAL = '';
                    
                    if ($("#hidCD_LINE_ITEM_ID").val() != "NEW") {
                        OLD_TOTAL = $("#hidTOTAL_VALUE_ON_UPDATE").val();
                    }
                    else {
                        OLD_TOTAL = $("#hidTOTAL").val();
                     }
                    
                    OLD_TOTAL = FormatAmountForSum(OLD_TOTAL);

                    var DEF_VALUE = parseFloat(TOTAL) - parseFloat(OLD_TOTAL);
                    DEF_VALUE = roundNumber(DEF_VALUE, 2);
                    DEF_VALUE = Math.abs(DEF_VALUE);
                   
                    if (parseFloat(DEF_VALUE) > parseFloat(TOLERANCE_LIMIT)) {
                        $("#capIS_EXCEPTION").text($("#hidExceptionMsg").val());
                            $("#hidEXCEPTION").val("YES");
                            if ($("#hidEXCEPTION_REASON").val() != "385" && $("#hidEXCEPTION_REASON").val() != "294") {
                                //$("#hidEXCEPTION_REASON").val("292"); //Beyond Tolerance Limit //Commented for itrack-913/966
                                $("#hidEXCEPTION_REASON").val("403");
                            }
                          
                        } //if (parseFloat(DEF_VALUE) > parseFloat(TOLERANCE_LIMIT)) {
                    else {
                        $("#capIS_EXCEPTION").text('');
                        $("#hidEXCEPTION").val("NO");
                        if ($("#hidEXCEPTION_REASON").val() != "385" && $("#hidEXCEPTION_REASON").val() != "294")
                            $("#hidEXCEPTION_REASON").val('');
                        //ClaculateTotalPremiumCollection();
                    }
                } //if ($("#cmbPAY_MODE option:selected").val() == "14692") {
                else {
                    $("#capIS_EXCEPTION").text('');
                    $("#hidEXCEPTION").val('');
                    $("#hidEXCEPTION_TYPE").val('');
                }
                $("#txtRECEIPT_AMOUNT").val(formatBaseCurrencyAmount(parseFloat(TOTAL)));
                $("#hidRECEIPT_AMOUNT").val(formatBaseCurrencyAmount(parseFloat(TOTAL)));
                $("#txtTOTAL_PREMIUM_COLLECTION").val(formatBaseCurrencyAmount(parseFloat(TOTAL)));//Added for itrack-913
            } // if (!isNaN(TOTAL)) { 
            else {
            $("#hidRECEIPT_AMOUNT").val('');
            $("#txtRECEIPT_AMOUNT").val('');
            $("#hidEXCEPTION_TYPE").val('');
        }
       
        if ($("#hidEXCEPTION_REASON").val() == "385" || $("#hidIS_APPROVE").val()=="A") 
        {
            $("#capIS_EXCEPTION").text($("#hidExceptionMsg").val());
            $("#hidEXCEPTION").val("YES");
            $("#hidEXCEPTION_REASON").val("385"); //Changes made in already approved Boleto.
        }
 
        
        if ($("#hidIS_ACTIVE").val() == "N") {
            $("#capIS_EXCEPTION").text($("#hidExceptionMsg").val());
            $("#hidEXCEPTION").val("YES");
            $("#hidEXCEPTION_REASON").val("293"); //Already cancelled Boleto
        }

        }
        function roundNumber(num, dec) {
            var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
            return result;
        }
            
        //Call Page Method
        function CallPageMethod(fn, paramArray, successFn, errorFn) {
            
              var pagePath = window.location.pathname;
              var paramList = '';
              if (paramArray.length > 0) {
                  for (var i = 0; i < paramArray.length; i += 2) {
                      if (paramList.length > 0) paramList += ',';
                      paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                      if (paramArray[i] == "POLICY_NUMBER") {
                          $('#hidPOLICY_NUM').val(paramArray[i]);
                      }
                      if (paramArray[i] == "OUR_NUMBER") {
                          $('#hidOUR_NUM').val(paramArray[i]);
                      }
                      
                  }
              }
              paramList = '{' + paramList + '}';
              //Call the page method  
              $.ajax({type: "POST",url: pagePath + "/" + fn,contentType: "application/json; charset=utf-8",
                  data: paramList,dataType: "json",success: successFn,error: errorFn});

          }
          function AjaxSucceeded(result) {
              
               
              if ($('#hidPOLICY_NUM').val() == "POLICY_NUMBER") {
                  ////debugger;
                  var Values = result.d.split('^');
                  if (result.d != "") {
                   
                      $("#hidCUSTOMER_ID").val(Values[0]);
                      $("#hidPOLICY_ID").val(Values[1]);
                      $("#hidPOLICY_VERSION_ID").val(Values[2]);
                      $("#txtPOLICY_NUMBER").val(Values[3]);
                      $("#capPolicyStatus").text(Values[4]);
                  }
                  else {
                      alert($('#hidPolicyNumberMsg').val());
                      $("#txtPOLICY_NUMBER").val('');
                      $("#capPolicyStatus").text('');
                  }
              }
             
             
              if ($('#hidOUR_NUM').val() == "OUR_NUMBER") {
                  var ReturnValues = result.d.split('^');
                
                  if (result.d != "" && ReturnValues[9] != "") {
                     
                      $("#hidBOLETO_NO").val(ReturnValues[9]);
                      $("#hidCUSTOMER_ID").val(ReturnValues[0]);
                      $("#hidPOLICY_ID").val(ReturnValues[1]);
                      $("#hidPOLICY_VERSION_ID").val(ReturnValues[2]);
                      $("#txtRISK_PREMIUM").val(formatBaseCurrencyAmount(ReturnValues[3]));
                      $("#txtFEE").val(formatBaseCurrencyAmount(ReturnValues[4]));
                      $("#txtTAX").val(formatBaseCurrencyAmount(ReturnValues[5]));
                      $("#txtINTEREST").val(formatBaseCurrencyAmount(ReturnValues[6]));
                      $("#txtLATE_FEE").val(formatBaseCurrencyAmount('0'));
                      $("#txtRECEIPT_AMOUNT").val(formatBaseCurrencyAmount(ReturnValues[7]));
                      $("#txtTOTAL_PREMIUM_COLLECTION").val(formatBaseCurrencyAmount(ReturnValues[7]));
                      $("#hidRECEIPT_AMOUNT").val(formatBaseCurrencyAmount(ReturnValues[7]));
                      $("#hidTOTAL").val(formatBaseCurrencyAmount(ReturnValues[7]));
                      $("#txtOUR_NUMBER").val(ReturnValues[8]);
                      $("#hidINSTALLMENT_NO").val(ReturnValues[11]);
                      
                      $("#hidIS_ACTIVE").val(ReturnValues[10]); //if Is_Active value is 'N' then this deposit item would go in exception.
                      if ($("#hidIS_ACTIVE").val() == "N") {
                          $("#capIS_EXCEPTION").text($("#hidExceptionMsg").val());
                          $("#hidEXCEPTION").val("YES");
                          $("#hidEXCEPTION_REASON").val("293"); //Already cancelled Boleto
                      }
                      else {
                          $("#capIS_EXCEPTION").text('');
                          $("#hidEXCEPTION").val("NO");
                          $("#hidEXCEPTION_REASON").val(''); 
                      }
                      //Css Added on run time
                      $("#txtRISK_PREMIUM").addClass("InputCurrency");
                      $("#txtFEE").addClass("InputCurrency");
                      $("#txtTAX").addClass("InputCurrency");
                      $("#txtINTEREST").addClass("InputCurrency");
                      $("#txtRECEIPT_AMOUNT").addClass("InputCurrency");
                      $("#txtTOTAL_PREMIUM_COLLECTION").addClass("InputCurrency");
                      
                      //End
                      ShowAndHideValidator(rfvOUR_NUMBER, false);
                      ShowAndHideValidator(rfvRISK_PREMIUM, false);
                      ShowAndHideValidator(rfvRECEIPT_AMOUNT, false);
                      ShowAndHideValidator(rfvTOTAL_PREMIUM_COLLECTION, false);
                  }
                  else {
                      $("#txtRISK_PREMIUM").val('');
                      $("#txtFEE").val('');
                      $("#txtTAX").val('');
                      $("#txtINTEREST").val('');
                      $("#txtRECEIPT_AMOUNT").val('');
                      $("#hidTOTAL").val('');
                      alert($('#hidOurNumberMsg').val());
                      $("#txtOUR_NUMBER").val('');
                  }
              }
              $('#hidPOLICY_NUM').val('');
              $('#hidPOL_CUSTOMER').val('');
              $('#hidOUR_NUM').val('');
            }
            function AjaxFailed(result) {
                alert("Please enter valid Number");

            }
            
                    
    </script>

</head>
<body leftmargin="0" topmargin="0" onload="Init();">
    <form id="ACT_CURRENT_DEPOSIT_LINE_ITEMS" runat="server" method="post">
    <table cellspacing="2" cellpadding="2" width="100%" border="0" class="tableWidthHeader">
       
        <tr>
            <td class="midcolorc" align="right" colspan="3">
                <asp:Label ID="lblmsg" runat="server" CssClass="errmsg"></asp:Label>
                <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
            </td>
            
        </tr>
        <tr>
         <td class="midcolorc" align="right" colspan="3">
            <asp:Label ID="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
        </td>
        </tr>
        <tr id="trBody" runat="server">
             <td>
                <table cellspacing="2" cellpadding="2" width="100%" border="0" border="0">
                <tr>
                    <td class="pageHeader" colspan="3">
                        <asp:Label ID="capMandatoryNotes" runat="server"></asp:Label>
                    </td>
                 </tr>
        </tr>
        <tr >
            <td colspan width="33%" class="midcolora">
                &nbsp;</td>
            <td  width="33%" class="midcolorr">
                &nbsp;</td>  
            <td width="33%" class="midcolora">
                &nbsp;</td>
        </tr>
        <tr id="trAPPROVE_REFUND" runat="server" visible="false" >
            <td  width="33%"  class="midcolorr">
             
            <td  width="33%" class="midcolorr">
             <asp:Label ID="capAPPROVE_REFUND" runat="server" ></asp:Label><span class="mandatory">*</span>
              <asp:DropDownList ID="cmbAPPROVE_REFUND"  runat="server"></asp:DropDownList> <br />
                <asp:RequiredFieldValidator ID="rfvAPPROVE_REFUND" runat="server" Display="Dynamic" Enabled="false" ControlToValidate="cmbAPPROVE_REFUND"></asp:RequiredFieldValidator></td>
                </td>  
            
            <td width="33%" class="midcolora">
               <cmsb:cmsbutton class="clsButton" id="btnAccept" runat="server" Text="Accept" 
                    Visible="false" causesvalidation="false" onclick="btnAccept_Click"  ></cmsb:cmsbutton>
                    </td>
        </tr>
        <%--Added three more fields on Customer Receipt as per the itrack -  1495 (Date 17-08-2011)---%>
         <tr id="trBankDetails" >
            <td width="33%" class="midcolora">
                <asp:Label ID="capBANK_NUMBER" runat="server" ></asp:Label><span class="mandatory">*</span><br />
                <asp:TextBox ID="txtBANK_NUMBER" MaxLength="4" Width="146px"   runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvBANK_NUMBER" runat="server" Display="Dynamic" ControlToValidate="txtBANK_NUMBER"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic" ID="revBANK_NUMBER" ControlToValidate="txtBANK_NUMBER"></asp:RegularExpressionValidator>
            </td>
            <td  width="33%" class="midcolora">
                <asp:Label ID="capBANK_AGENCY_NUMBER" runat="server" ></asp:Label><span class="mandatory">*</span><br />
                <asp:TextBox ID="txtBANK_AGENCY_NUMBER" MaxLength="6" Width="146px"   runat="server" OnBlur="this.value=fnFormatBranchBank(this.value);ValidatorOnChange();"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvBANK_AGENCY_NUMBER" runat="server" Display="Dynamic" ControlToValidate="txtBANK_AGENCY_NUMBER"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic" ID="revBANK_AGENCY_NUMBER" ControlToValidate="txtBANK_AGENCY_NUMBER"></asp:RegularExpressionValidator>
            </td>  
            <td width="33%" class="midcolora">
                <asp:Label ID="capBANK_ACCOUNT_NUMBER" runat="server" ></asp:Label><span class="mandatory">*</span><br />
                <asp:TextBox ID="txtBANK_ACCOUNT_NUMBER" MaxLength="20" Width="146px"   runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvBANK_ACCOUNT_NUMBER" runat="server" Display="Dynamic" ControlToValidate="txtBANK_ACCOUNT_NUMBER"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic" ID="revBANK_ACCOUNT_NUMBER" ControlToValidate="txtBANK_ACCOUNT_NUMBER"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <%--Added till here --%>
        <tr>
            <td id="tdBoleto" runat="server" class="midcolora" >
                <asp:Label ID="capBOLETO_NO" runat="server" ></asp:Label><span class="mandatory">*</span><br />
                <asp:TextBox ID="txtOUR_NUMBER" runat="server" MaxLength="16" Width="235px"></asp:TextBox>
                <br />
               <asp:RequiredFieldValidator ID="rfvOUR_NUMBER" runat="server" Display="Dynamic" ControlToValidate="txtOUR_NUMBER"></asp:RequiredFieldValidator>
               </td>
               <td id="tdPAYMENT_DATE" runat="server" class="midcolora" colspan="2">
               <asp:Label ID="capPAYMENT_DATE" runat="server" ></asp:Label><span class="mandatory">*</span><br />
               <asp:textbox id="txtPAYMENT_DATE" runat="server" maxlength="10" size="12" ></asp:textbox>
			   <asp:hyperlink id="hlkPAYMENT_DATE" runat="server" CssClass="HotSpot">
			   <asp:image id="imgPAYMENT_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image></asp:hyperlink>
			   <br />
			   <asp:requiredfieldvalidator id="rfvPAYMENT_DATE" runat="server"  Display="Dynamic" ErrorMessage="" ControlToValidate="txtPAYMENT_DATE"></asp:requiredfieldvalidator>
               <asp:regularexpressionvalidator id="revPAYMENT_DATE" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtPAYMENT_DATE"></asp:regularexpressionvalidator>
			   <asp:customvalidator id="csvPAYMENT_DATE" runat="server" Display="Dynamic"  ControlToValidate="txtPAYMENT_DATE" ClientValidationFunction="ChkFutureDate"></asp:customvalidator>
			   
               </td>
        </tr>
        <tr id="tdPolicyNumber">
            <td  runat="server" class="midcolora">
                <asp:Label ID="capPOLICY_NUMBER" runat="server" ></asp:Label><span class="mandatory">*</span><br />
                <asp:TextBox ID="txtPOLICY_NUMBER" runat="server" MaxLength="21" Width="235px"></asp:TextBox>
                <img id="imgPOLICY_NUMBER" style="CURSOR: hand" onclick="OpenPolicyLookup();" alt="" src="../../cmsweb/images/selecticon.gif" runat="server">
                <br />
                <asp:RequiredFieldValidator ID="rfvPOLICY_NUMBER" runat="server" Display="Dynamic" ControlToValidate="txtPOLICY_NUMBER"></asp:RequiredFieldValidator>
            </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capPOLICY_DESCRIPTION" runat="server" Text="Policy Version" ></asp:Label><span class="mandatory">*</span><br />
                <asp:DropDownList ID="cmbPOLICY_DESCRIPTION" runat="server" Width="235px"></asp:DropDownList><br />
                <asp:RequiredFieldValidator ID="rfvPOLICY_DESCRIPTION" runat="server" Display="Dynamic" ControlToValidate="cmbPOLICY_DESCRIPTION"></asp:RequiredFieldValidator>
            </td>
            <td  width="33%" class="midcolora">
                <asp:Label ID="capPOLICY_INSTALLMENT" runat="server" Text="Policy Installment" ></asp:Label><span class="mandatory">*</span><br />
                <asp:DropDownList ID="cmbPOLICY_INSTALLMENT" runat="server" Width="146px"></asp:DropDownList><br />
                <asp:RequiredFieldValidator ID="rfvPOLICY_INSTALLMENT" runat="server" Display="Dynamic" ControlToValidate="cmbPOLICY_INSTALLMENT"></asp:RequiredFieldValidator>
           </td>
        </tr>
        <tr id="trRefundDeposit" runat="server">
            <td width="33%" class="midcolora">
             <asp:Label ID="capRECEIPT_FROM_ID" runat="server" Text="Name" ></asp:Label><span class="mandatory">*</span><br />
                <asp:DropDownList ID="cmbRECEIPT_FROM_ID"  runat="server" Width="235px"></asp:DropDownList> <br />
                <asp:RequiredFieldValidator ID="rfvRECEIPT_FROM_ID" runat="server" Display="Dynamic" ControlToValidate="cmbRECEIPT_FROM_ID"></asp:RequiredFieldValidator>
            </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capREFUND_AMOUNT" runat="server" Text="Refund Amount" ></asp:Label><span class="mandatory">*</span><br />
                <asp:TextBox ID="txtREFUND_AMOUNT" runat="server" MaxLength="15" Width="200px" CssClass="InputCurrency" onblur="this.value=formatBaseCurrencyAmount(this.value)"></asp:TextBox>
                &nbsp; &nbsp;<asp:Label ID="capExceptionRefund" ForeColor="#FF3300"  runat="server" Font-Bold="True" ></asp:Label><br />
                <br />
                <asp:RequiredFieldValidator ID="rfvREFUND_AMOUNT" runat="server" Display="Dynamic" ControlToValidate="txtREFUND_AMOUNT"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic" ID="revREFUND_AMOUNT" ControlToValidate="txtREFUND_AMOUNT"></asp:RegularExpressionValidator>
                
            </td>
            <td width="33%" class="midcolora">
          
            </td>
        </tr>
        <tr id="TrLEADER_POLICY_NUMBER">
            <td class="midcolora">
                <asp:Label ID="capLEADER_POLICY_NUMBER" runat="server" ></asp:Label><span class="mandatory">*</span><br />
                <asp:TextBox ID="txtLEADER_POLICY_NUMBER" runat="server" MaxLength="21" Width="235px"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvLEADER_POLICY_NUMBER" runat="server" Display="Dynamic" ControlToValidate="txtLEADER_POLICY_NUMBER"></asp:RequiredFieldValidator>
            </td>
            <td width="33%" class="midcolora">
            <asp:Label ID="capLEADER_ENDORSEMENT_NUMBER" runat="server" Text="Leader endorsement #" ></asp:Label><span class="mandatory">*</span><br />
                <asp:DropDownList ID="cmbLEADER_ENDORSEMENT_NUMBER" runat="server" Width="146px"></asp:DropDownList> <br />
                <asp:RequiredFieldValidator ID="rfvLEADER_ENDORSEMENT_NUMBER" runat="server" Display="Dynamic" ControlToValidate="cmbLEADER_ENDORSEMENT_NUMBER"></asp:RequiredFieldValidator>
            </td>
            <td width="33%" class="midcolora">
              
            </td>
            
        </tr>
        <tr id="trCoInsurance" runat="server">
            <td width="33%" class="midcolora">
             <asp:Label ID="capINSTALLMENT" runat="server" Text="Installment" ></asp:Label><span class="mandatory">*</span><br />
                <asp:DropDownList ID="cmbINSTALLMENT"  runat="server" Width="146px"></asp:DropDownList> <br />
                <asp:RequiredFieldValidator ID="rfvINSTALLMENT" runat="server" Display="Dynamic" ControlToValidate="cmbINSTALLMENT"></asp:RequiredFieldValidator>
            </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capINSTALLMENT_RISK_PREMIUM" runat="server" Text="Installment Risk Premium" ></asp:Label><span class="mandatory">*</span><br />
                <asp:TextBox ID="txtINSTALLMENT_RISK_PREMIUM" runat="server" MaxLength="15" Width="146px" CssClass="InputCurrency" onblur="this.value=formatBaseCurrencyAmount(this.value)"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvINSTALLMENT_RISK_PREMIUM" runat="server" Display="Dynamic" ControlToValidate="txtINSTALLMENT_RISK_PREMIUM"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic" ID="revINSTALLMENT_RISK_PREMIUM" ControlToValidate="txtINSTALLMENT_RISK_PREMIUM"></asp:RegularExpressionValidator>
                
            </td>
            <td width="33%" class="midcolora">
            <asp:Label ID="capINTEREST_AMOUNT" runat="server" Text="Interest Amount" ></asp:Label><br />
                <asp:TextBox ID="txtINTEREST_AMOUNT" runat="server" MaxLength="15" Width="146px" CssClass="InputCurrency" onblur="this.value=formatBaseCurrencyAmount(this.value)"></asp:TextBox>
                <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic" ID="revINTEREST_AMOUNT" ControlToValidate="txtINTEREST_AMOUNT"></asp:RegularExpressionValidator>
                
            </td>
        </tr>
        <tr id="trCoInsurance1" runat="server">
            <td width="33%" class="midcolora">
             <asp:Label ID="capCO_COMMISSION_AMOUNT" runat="server" Text="Co-Commission Amount" ></asp:Label><br />
                <asp:TextBox ID="txtCO_COMMISSION_AMOUNT" runat="server" MaxLength="15" Width="146px" CssClass="InputCurrency" onblur="this.value=formatBaseCurrencyAmount(this.value)"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revCO_COMMISSION_AMOUNT" ControlToValidate="txtCO_COMMISSION_AMOUNT" runat="server" ErrorMessage="" Display="Dynamic" ></asp:RegularExpressionValidator>
                
             </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capBROKER_COMMISSION_AMOUNT" runat="server" Text="Broker Commission Amount" ></asp:Label><br />
                <asp:TextBox ID="txtBROKER_COMMISSION_AMOUNT" runat="server" MaxLength="15" Width="146px" CssClass="InputCurrency" onblur="this.value=formatBaseCurrencyAmount(this.value)"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revBROKER_COMMISSION_AMOUNT" ControlToValidate="txtBROKER_COMMISSION_AMOUNT" runat="server" ErrorMessage="" Display="Dynamic" ></asp:RegularExpressionValidator>
                
            </td>
            <td width="33%" class="midcolora">
             <asp:Label ID="capNET_INSTALLMENT_AMOUNT" runat="server" Text="Net Installment Amount" ></asp:Label><br />
                <asp:TextBox ID="txtNET_INSTALLMENT_AMOUNT" runat="server" MaxLength="15" Width="146px" CssClass="InputCurrency" onblur="this.value=formatBaseCurrencyAmount(this.value)"></asp:TextBox>
                &nbsp; &nbsp;<asp:Label ID="capIS_EXCEPTION_COINS" ForeColor="#FF3300"  runat="server" Font-Bold="True" ></asp:Label><br />
                <asp:RegularExpressionValidator ID="revNET_INSTALLMENT_AMOUNT" ControlToValidate="txtNET_INSTALLMENT_AMOUNT" runat="server" ErrorMessage="" Display="Dynamic" ></asp:RegularExpressionValidator>
             </td>

        </tr>
        <%--Added for 1148/1363 as discussed by Anurag--%>
        <tr id="trCoInsurance2" runat="server">
            <td width="33%" class="midcolora">
                &nbsp;</td>
            <td width="33%" class="midcolora">
                &nbsp;</td>
            <td width="33%" class="midcolora">
             <asp:Label ID="capCOI_TOTAL_PREMIUM_COLLECTION" runat="server" ></asp:Label><span class="mandatory">*</span><br />
                <asp:TextBox ID="txtCOI_TOTAL_PREMIUM_COLLECTION" MaxLength="15" 
                    CssClass="InputCurrency" runat="server" 
                    onblur="this.value=formatBaseCurrencyAmount(this.value)" Width="146px"></asp:TextBox> 
                <br />
                 <asp:RequiredFieldValidator ID="rfvCOI_TOTAL_PREMIUM_COLLECTION" runat="server" Display="Dynamic" ControlToValidate="txtCOI_TOTAL_PREMIUM_COLLECTION"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic" ID="revCOI_TOTAL_PREMIUM_COLLECTION" ControlToValidate="txtCOI_TOTAL_PREMIUM_COLLECTION"></asp:RegularExpressionValidator>

            </td>

        </tr>
        <%--Added till here --%>
        <tr id="trNormalType" runat="server">
            <td width="33%" class="midcolora">
                <asp:Label ID="capRISK_PREMIUM" runat="server" ></asp:Label><span class="mandatory">*</span><br />
                <asp:TextBox ID="txtRISK_PREMIUM" MaxLength="9" Width="146px" CssClass="InputCurrency" onblur="this.value=formatBaseCurrencyAmount(this.value)" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvRISK_PREMIUM" runat="server" Display="Dynamic" ControlToValidate="txtRISK_PREMIUM"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic" ID="revRISK_PREMIUM" ControlToValidate="txtRISK_PREMIUM"></asp:RegularExpressionValidator>
            </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capFEE" runat="server" ></asp:Label><br />
                <asp:TextBox ID="txtFEE" MaxLength="9" Width="146px" CssClass="InputCurrency" onblur="this.value=formatBaseCurrencyAmount(this.value)" runat="server"></asp:TextBox><br />
                <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic" ID="revFEE" ControlToValidate="txtFEE"></asp:RegularExpressionValidator>
            </td>
            <td width="33%" class="midcolora">
                <asp:Label ID="capTAX" runat="server" ></asp:Label><br />
                <asp:TextBox ID="txtTAX" MaxLength="9" Width="146px" CssClass="InputCurrency" onblur="this.value=formatBaseCurrencyAmount(this.value)" runat="server"></asp:TextBox><br />
                <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic" ID="revTAX" ControlToValidate="txtTAX"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr id="trNormalType1" runat="server">
            <td width="33%" class="midcolora">
                <asp:Label ID="capINTEREST" runat="server" ></asp:Label><br />
                <asp:TextBox ID="txtINTEREST" MaxLength="9" Width="146px" CssClass="InputCurrency" onblur="this.value=formatBaseCurrencyAmount(this.value)" runat="server"></asp:TextBox><br />
                <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic" ID="revINTEREST" ControlToValidate="txtINTEREST"></asp:RegularExpressionValidator>
                 
            </td>
            <td width="33%" class="midcolora">
               <asp:Label ID="capLATE_FEE" runat="server" ></asp:Label><br />
                <asp:TextBox ID="txtLATE_FEE" MaxLength="15"  Width="146px" CssClass="InputCurrency" onblur="this.value=formatBaseCurrencyAmount(this.value)" runat="server"></asp:TextBox> <br />
                <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic" ID="revLATE_FEE" ControlToValidate="txtLATE_FEE"></asp:RegularExpressionValidator>
            </td>
            <td  width="33%" class="midcolora">
                <asp:Label ID="capRECEIPT_AMOUNT" runat="server" ></asp:Label><span class="mandatory">*</span><br />
                <asp:TextBox ID="txtRECEIPT_AMOUNT" Width="146px" MaxLength="10" CssClass="InputCurrency" ReadOnly="true" runat="server"></asp:TextBox>
                &nbsp; &nbsp;<asp:Label ID="capIS_EXCEPTION" ForeColor="#FF3300"  runat="server" Font-Bold="True" ></asp:Label><br />
                 <asp:RequiredFieldValidator ID="rfvRECEIPT_AMOUNT" runat="server" Display="Dynamic" ControlToValidate="txtRECEIPT_AMOUNT"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic" ID="revRECEIPT_AMOUNT" ControlToValidate="txtRECEIPT_AMOUNT"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr id="trTOTAL_PREMIUM_COLLECTION">
            <td width="33%" class="midcolora">
                &nbsp;</td>
            <td width="33%" class="midcolora">
                &nbsp;</td>
            <td  width="33%" class="midcolora">
             <asp:Label ID="capTOTAL_PREMIUM_COLLECTION" runat="server" ></asp:Label><span class="mandatory">*</span><br />
                <asp:TextBox ID="txtTOTAL_PREMIUM_COLLECTION" MaxLength="15" 
                    CssClass="InputCurrency" runat="server" 
                    onblur="this.value=formatBaseCurrencyAmount(this.value)" Width="146px"></asp:TextBox> 
                <br />
                 <asp:RequiredFieldValidator ID="rfvTOTAL_PREMIUM_COLLECTION" runat="server" Display="Dynamic" ControlToValidate="txtTOTAL_PREMIUM_COLLECTION"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic" ID="revTOTAL_PREMIUM_COLLECTION" ControlToValidate="txtTOTAL_PREMIUM_COLLECTION"></asp:RegularExpressionValidator>
               </td>
        </tr>

        <tr>
            <td class="midcolora">
                <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" Text="Reset" CausesValidation="False">
                </cmsb:CmsButton>
            </td>
            <td class="midcolora">
                &nbsp;</td>

            <td class="midcolorc" >  <%--Change Align from right to center - itrack-1720 -tfs-188 by Pradeep  --%>
                  <cmsb:cmsbutton class="clsButton" id="btnGenerateReceipt" runat="server" Visible="false" causesvalidation="false" 
                    onclick="btnGenerateReceipt_Click"  ></cmsb:cmsbutton>
                <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" 
                    causesvalidation="false" onclick="btnDelete_Click" ></cmsb:cmsbutton>
                <cmsb:CmsButton class="clsButton" ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" >
                </cmsb:CmsButton>
            </td>
        </tr>
    </table>
                    </td></tr>
    </table>
    <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server" />
    <input id="hidPOLICYINFO" type="hidden" runat="server"/>
    <input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server"/>    
    <input id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server"/>  
    <input id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server"/>  
    <input id="hidDEPOSIT_ID" type="hidden" value="0" name="hidDEPOSIT_ID" runat="server"/>  
    <input id="hidCD_LINE_ITEM_ID" type="hidden" value="0" name="hidCD_LINE_ITEM_ID" runat="server"/>  
    <input id="hidBOLETO_NO" type="hidden" value="0" name="hidBOLETO_NO" runat="server"/>  
    <input id="hidRECEIPT_AMOUNT" type="hidden" name="hidRECEIPT_AMOUNT" runat="server"/>  
    <input id="hidPOL_CUSTOMER" type="hidden" name="hidPOL_CUSTOMER" runat="server"/>  
    <input id="hidPOLICY_NUM" type="hidden" name="hidPOLICY_NUM" runat="server"/>  
    <input id="hidOUR_NUM" type="hidden" name="hidOUR_NUM" runat="server"/>  
    <input id="hidBOLETO_TOLERANCE_LIMIT" type="hidden" name="hidBOLETO_TOLERANCE_LIMIT" runat="server"/>  
    <input id="hidTOTAL" type="hidden" name="hidTOTAL" runat="server"/>  
    <input id="hidIS_EXCEPTION" type="hidden" name="hidIS_EXCEPTION" runat="server"/>  
    <input id="hidEXCEPTION" type="hidden" name="hidEXCEPTION" runat="server"/>  
    <input id="hidOurNumberMsg" type="hidden" name="hidOurNumberMsg" runat="server"/>  
    <input id="hidPolicyNumberMsg" type="hidden" name="hidPolicyNumberMsg" runat="server"/>  
    <input id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"/>  
    <input id="hidEXCEPTION_REASON" type="hidden" name="hidEXCEPTION_REASON" runat="server"/>  
    <input id="hidTOTAL_VALUE_ON_UPDATE" type="hidden" name="hidTOTAL_UPDATE" runat="server"/>  
    <input id="hidDEPOSIT_NUMBER" type="hidden" value="0" name="hidDEPOSIT_NUMBER" runat="server"/>  
    <input id="hidDEPOSIT_TYPE" type="hidden" name="hidDEPOSIT_TYPE" runat="server"/>  
    <input id="hidDEPOSIT_TYPE1" type="hidden" name="hidDEPOSIT_TYPE1" runat="server"/>  
    <input id="hidINSTALLMENT_NO" type="hidden" name="hidINSTALLMENT_NO" runat="server"/>  
    <input id="hidIS_APPROVE" type="hidden" runat="server" name="hidIS_APPROVE" />
    <input id="hidExceptionMsg" type="hidden" runat="server" />
    <input id="hidNET_INSTALLMENT_AMOUNT" type="hidden" value="0" name="hidNET_INSTALLMENT_AMOUNT" runat="server"/>  
    <input id="hidLEADER_ENDORSEMENT_NUMBER" type="hidden"  name="hidLEADER_ENDORSEMENT_NUMBER" runat="server"/>  
    <input id="hidLeaderPolicyNumberMsg" type="hidden" name="hidPolicyNumberMsg" runat="server"/>  
    <input id="hidRECEIPT_FROM_ID" type="hidden"  name="hidRECEIPT_FROM_ID" runat="server"/>  
    <input id="hidINSTALLMENT_AMOUNT" type="hidden" value="0" name="hidINSTALLMENT_AMOUNT" runat="server"/>  
    <input id="hidCOMM_PERCENT" type="hidden" value="0"  name="hidCOMM_PERCENT" runat="server"/>  
    <input id="hidCOMM_POLICY_LEVEL" type="hidden" value="0"  name="hidCOMM_POLICY_LEVEL" runat="server"/>  
    <input id="hidREFUND_CAL_AMOUNT" type="hidden" value="0"  name="hidREFUND_CAL_AMOUNT" runat="server"/>  
    <input id="hidCOACC_TOLERANCE_LIMIT_PERCENTAGE" type="hidden" name="hidCOACC_TOLERANCE_LIMIT_PERCENTAGE" runat="server"/>  
    <input id="hidCOACC_TOLERANCE_LIMIT_AMOUNT" type="hidden" name="hidCOACC_TOLERANCE_LIMIT_AMOUNT" runat="server"/>  
    <input id="hidNET_INSTALLMENT_AMOUNT_ON_UPDATE" type="hidden" name="hidNET_INSTALLMENT_AMOUNT_ON_UPDATE" runat="server"/>
    <input id="hidPolicy" type="hidden" name="hidPolicy" runat="server"/>    
    <input id="hidRECEIPT_MODE" type="hidden" name="hidRECEIPT_MODE" runat="server"/>    
    
    </form>
    <script type="text/javascript" >
        if (document.getElementById('hidFormSaved').value == "1") 
        {
        try {
            RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidCD_LINE_ITEM_ID').value);
        }
        catch (err) {}
         }
		</script>
</body>
</html>

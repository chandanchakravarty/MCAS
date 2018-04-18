<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BillingInfo.aspx.cs" ValidateRequest="false" Inherits="Cms.Policies.Aspx.BillingInfo" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>POL_BILLINGINFO</title>
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >
    
    <style>
        .trd
        {
            display: none;
        }
        .CustomCss
        {
        	PADDING-LEFT: 5px;
        	FONT-WEIGHT: normal; 
        	FONT-SIZE:8pt;
        	vertical-align:TOP; 
        	COLOR: #000000; 
        	FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; 
        	BACKGROUND-COLOR: #f3f3f3; 
        	TEXT-ALIGN:right
        }
    </style>

    <script src="/cms/cmsweb/scripts/xmldom.js"></script>

    <script src="/cms/cmsweb/scripts/common.js"></script>

    <script src="/cms/cmsweb/scripts/form.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>

    <script type="text/javascript" language="javascript">

        //Added by Pradeep for itrack 1512/tfs#240
        
        function validateLimitRange(sender, args) {

            var input = args.Value;
            input = FormatAmountForSum(input)
            var max = 99999999999999.99;
            if (parseFloat(input) <= parseFloat(max)) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }
        //Added till here 

        //Calculate grid view row amount sumn for installment total

        var PagePost;
        var nthidTotalPremium;
        var nhidTotalInt;
        var nhidTotalFee;
        var nhidTotalTaxes;
        var Policy_Total_premium;


        function InstallmentTotal(objcontrol) {
         

            var objcontrolID = objcontrol.id;
            var num = objcontrol.value;


            var splID = objcontrolID.split('_');   //Genrate dynamic id for each textbox in gridview for every row for calculate sum 
            var PREMIUM = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtPREMIUM';  //installment premium textbox
            var INTEREST_AMOUNT = splID[0] + '_' + splID[1] + '_' + splID[ 2] + '_' + 'txtINTEREST_AMOUNT'; //installment interest amount textbox
            var FEE = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtFEE'; //installment fees amount textbox
            var TAXES = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtTAXES'; //installment taxes amount textbox
            var TOTAL = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtTOTAL'; //installment total(sum of all amount textbox)
            var hidTOTAL = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'hidTOTAL'; //hid field for calculated sum       
            //declare gridview trans control id ;
//            var TRAN_PREMIUM = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtTRAN_PREMIUM_AMOUNT';
//            var TRAN_INTEREST_AMOUNT = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtTRAN_INTEREST_AMOUNT';
//            var TRAN_FEE = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtTRAN_FEE';
//            var TRAN_TAXES = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtTRAN_TAXES';
//            var TRAN_TOTAL = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtTRAN_TOTAL';
            var TRAN_hidTOTAL = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'hidTRAN_TOTAL';

            /* get the value from gridview textbox For calculate sum row wise and assign it into total & hid total column */
           
            var txtPREMIUM = document.getElementById(PREMIUM).value == '' ? '0' : document.getElementById(PREMIUM).value;
            txtPREMIUM = FormatAmountForSum(txtPREMIUM);


            var txtINTEREST_AMOUNT = document.getElementById(INTEREST_AMOUNT).value == '' ? '0' : document.getElementById(INTEREST_AMOUNT).value;
            txtINTEREST_AMOUNT = FormatAmountForSum(txtINTEREST_AMOUNT);

            var txtFEE = document.getElementById(FEE).value == '' ? '0' : document.getElementById(FEE).value;
            txtFEE = FormatAmountForSum(txtFEE);

            var txtTAXES = document.getElementById(TAXES).value == '' ? '0' : document.getElementById(TAXES).value;
            txtTAXES = FormatAmountForSum(txtTAXES);
                if (txtTAXES <'0') {
                txtTAXES =formatAmount('0');
            }
            // changed by praveer panghal for itrack no 1761
           
//            var hid_IOF_PERCENTAGE = document.getElementById('hid_IOF_PERCENTAGE').value == '' ? '0' : document.getElementById('hid_IOF_PERCENTAGE').value;
//            hid_IOF_PERCENTAGE = ReplaceAll(hid_IOF_PERCENTAGE, sGroupSep, '');
//            hid_IOF_PERCENTAGE = ReplaceAll(hid_IOF_PERCENTAGE, sDecimalSep, '.');   
//            var txtTAXES = parseFloat(parseFloat(hid_IOF_PERCENTAGE) * (parseFloat(txtPREMIUM) + parseFloat(txtINTEREST_AMOUNT) + parseFloat(txtFEE)) / 100);
//            txtTAXES = formatAmount(txtTAXES);
//            if (txtTAXES <'0') {
//                txtTAXES =formatAmount('0');
//            }
//            document.getElementById(TAXES).value = (txtTAXES);

            

            var sumTOTAL = parseFloat(parseFloat(txtPREMIUM) + parseFloat(txtINTEREST_AMOUNT) + parseFloat(txtFEE) + parseFloat(txtTAXES));
            sumTOTAL = roundNumber(sumTOTAL, 2);

            /* End main installment textbox calculation part*/


            //Start for tran control calculation

//            var TRAN_PREMIUM = document.getElementById(TRAN_PREMIUM).value == '' ? '0' : document.getElementById(TRAN_PREMIUM).value;
//            TRAN_PREMIUM = FormatAmountForSum(TRAN_PREMIUM);
//            if (isNaN(parseFloat(TRAN_PREMIUM)))
//                TRAN_PREMIUM = 0;

//            var TRAN_INTEREST_AMOUNT = document.getElementById(TRAN_INTEREST_AMOUNT).value == '' ? '0' : document.getElementById(TRAN_INTEREST_AMOUNT).value;
//            TRAN_INTEREST_AMOUNT = FormatAmountForSum(TRAN_INTEREST_AMOUNT);
//            if (isNaN(parseFloat(TRAN_INTEREST_AMOUNT)))
//                TRAN_INTEREST_AMOUNT = 0;


//            var TRAN_FEE = document.getElementById(TRAN_FEE).value == '' ? '0' : document.getElementById(TRAN_FEE).value;
//            TRAN_FEE = FormatAmountForSum(TRAN_FEE);
//            if (isNaN(parseFloat(TRAN_FEE)))
//                TRAN_FEE = 0;

//            var TRAN_TAXES = document.getElementById(TRAN_TAXES).value == '' ? '0' : document.getElementById(TRAN_TAXES).value;
//            TRAN_TAXES = FormatAmountForSum(TRAN_TAXES);
//            if (isNaN(parseFloat(TRAN_TAXES)))
//                TRAN_FEE = 0;

//            var sumTRAN_TOTAL = parseFloat(parseFloat(TRAN_PREMIUM) + parseFloat(TRAN_INTEREST_AMOUNT) + parseFloat(TRAN_FEE) + parseFloat(TRAN_TAXES));
//            sumTRAN_TOTAL = roundNumber(sumTRAN_TOTAL, 2);

            //End tran control

            //If sum is greater then 0 then assigns formated(comma seprated or according to culture based format of amount) sum into textbox

            if (sumTOTAL != NaN ) {
                if (sumTOTAL < 9999999999999) {
                    document.getElementById(TOTAL).value = formatAmount(parseFloat(sumTOTAL));
                    document.getElementById(hidTOTAL).value = formatAmount(parseFloat(sumTOTAL)); //parseFloat(sumTOTAL);
                }
//                document.getElementById(TRAN_TOTAL).value = formatAmount(parseFloat(sumTRAN_TOTAL));
//                document.getElementById(TRAN_hidTOTAL).value = parseFloat(sumTRAN_TOTAL);

            }
            else {  //if calculated sum is less then 0 then assign it as blank .
                document.getElementById(TOTAL).value = '0';
                document.getElementById(hidTOTAL).value = '';

//                document.getElementById(TRAN_TOTAL).value = '0';
                document.getElementById(TRAN_hidTOTAL).value = '';
            }
            var returnnval = ValidateColumnTotal(objcontrolID);
        }

        /*Function called on gridview textbox onblur for check Is all installments total equal to 
        total value */
        function ValidateColumnTotal(objcontrolID) {
            isPageValid_ToSubmit = true;    
            if (document.getElementById('btnSave') != null)
                document.getElementById('btnSave').disabled = false;
            var sum = 0;
            var TRAN_sum = 0;
            var splID = objcontrolID.split('_');

            var hidROWCOUNT = document.getElementById("hidROWCOUNT").value;
            if (document.getElementById("hidROWCOUNT").value == "" || document.getElementById("hidROWCOUNT").value == '0') { return; }

            var objTOTAL_CONTROL;
            var objTRAN_Cntrl;
            var objInstallment_CONTROL;
            var objcsvcontrol;
            var csvcntrl;

            switch (splID[3]) {
                case "txtPREMIUM":
                    objInstallment_CONTROL = "txtPREMIUM";
                    objTOTAL_CONTROL = "txtTOTAL_PREMIUM";
                    objcsvcontrol = "csvPREMIUM";
                    //objTRAN_Cntrl = "txtTRAN_PREMIUM_AMOUNT";
                    csvcntrl = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + objcsvcontrol;
                    break;

                case "txtINTEREST":
                    objInstallment_CONTROL = "txtINTEREST_AMOUNT";
                    objTOTAL_CONTROL = "txtTOTAL_INTEREST_AMOUNT";
                    objcsvcontrol = "csvINTEREST_AMOUNT";
//                    objTRAN_Cntrl = "txtTRAN_INTEREST_AMOUNT";
                    csvcntrl = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + objcsvcontrol;
                    break;

                case "txtFEE":
                    objInstallment_CONTROL = "txtFEE";
                    objTOTAL_CONTROL = "txtTOTAL_FEES";
                    objcsvcontrol = "csvFEE";
//                    objTRAN_Cntrl = "txtTRAN_FEE";
                    csvcntrl = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + objcsvcontrol;
                    break;

                case "txtTAXES":
                    objInstallment_CONTROL = "txtTAXES";
                    objTOTAL_CONTROL = "txtTOTAL_TAXES";
                    objcsvcontrol = "csvTAXES";
//                    objTRAN_Cntrl = "txtTRAN_TAXES";
                    csvcntrl = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + objcsvcontrol;
                    break;

                default:
                    if (splID[3] + '_' + splID[4] == "txtTRAN_TAXES") {
                        objInstallment_CONTROL = "txtTAXES";
                        objTOTAL_CONTROL = "txtTOTAL_TAXES";
                        objcsvcontrol = "csvTAXES";
//                        objTRAN_Cntrl = "txtTRAN_TAXES";
                        csvcntrl = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + objcsvcontrol;
                    }
                    else if (splID[3] + '_' + splID[4] == "txtTRAN_PREMIUM") {
                        objInstallment_CONTROL = "txtPREMIUM";
                        objTOTAL_CONTROL = "txtTOTAL_PREMIUM";
                        objcsvcontrol = "csvPREMIUM";
                       // objTRAN_Cntrl = "txtTRAN_PREMIUM_AMOUNT";
                        csvcntrl = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + objcsvcontrol;
                    }
                    else if (splID[3] + '_' + splID[4] == "txtTRAN_INTEREST") {
                        objInstallment_CONTROL = "txtINTEREST_AMOUNT";
                        objTOTAL_CONTROL = "txtTOTAL_INTEREST_AMOUNT";
                        objcsvcontrol = "csvINTEREST_AMOUNT";
//                        objTRAN_Cntrl = "txtTRAN_INTEREST_AMOUNT";
                        csvcntrl = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + objcsvcontrol;
                    }
                    else if (splID[3] + '_' + splID[4] == "txtTRAN_FEE") {
                        objInstallment_CONTROL = "txtFEE";
                        objTOTAL_CONTROL = "txtTOTAL_FEES";
                        objcsvcontrol = "csvFEE";
//                        objTRAN_Cntrl = "txtTRAN_FEE";
                        csvcntrl = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + objcsvcontrol;
                    }

                    break;
            }

    
            var TotalPremium = document.getElementById(objTOTAL_CONTROL).value;
            TotalPremium = FormatAmountForSum(TotalPremium);

            // In endorsment all installment totol will compare with (Totol policy premium + total tran premium - Inforce premium)
            // Other then endorsment all installemnt preimum total comapre with only total policy premium
            //            if (splID[3] == "txtPREMIUM" || (splID[3] + '_' + splID[4] == "txtTRAN_PREMIUM")) {
            //                if (document.getElementById('hidPOLICY_STATUS').value == 'UENDRS' || document.getElementById('hidPOLICY_STATUS').value == 'MENDORSE') {

            //                    TotalPremium = document.getElementById('hidINSTALL_PREMIUM').value;
            //                    TotalPremium = parseFloat(TotalPremium) - parseFloat(document.getElementById('hidPRORATED_PREMIUM').Value) + parseFloat(document.getElementById('txtTOTAL_TRAN_PREMIUM').Value);

            //                } else {
            //                    //                if (splID[3] == "txtPREMIUM" || (splID[3] + '_' + splID[4] == "txtTRAN_PREMIUM")) {
            //                    //                    if (document.getElementById('txtTOTAL_TRAN_PREMIUM') != null && document.getElementById('txtTOTAL_TRAN_PREMIUM') != 'undefined')
            //                    //                        var tranPremium = document.getElementById('txtTOTAL_TRAN_PREMIUM').value == "" ? "0" : document.getElementById('txtTOTAL_TRAN_PREMIUM').value;

            //                    //                    if (document.getElementById('txtTOTAL_END_INFO') != null && document.getElementById('txtTOTAL_END_INFO') != 'undefined')
            //                    //                        var trEnfor = document.getElementById('txtTOTAL_END_INFO').value == "" ? "0" : document.getElementById('txtTOTAL_END_INFO').value;

            //                    TotalPremium = TotalPremium

            //                }
            //            }


            // TotalPremium = ReplaceAll(TotalPremium, ",", "");

            var as = new Array();           
            var END_PREMIUM = 0;
            for (i = 2; i < parseInt(hidROWCOUNT) + 2; i++) {
                if (parseInt(i) < 10) {
                    var rowcntrl = 'grdBILLING_INFO_ctl0' + i + '_' + objInstallment_CONTROL;
                    var rowtran_cntrl = 'grdBILLING_INFO_ctl0' + i + '_' + objTRAN_Cntrl;
                    as[i - 2] = 'grdBILLING_INFO_ctl0' + i + '_' + objcsvcontrol;
                    var TRAN_TYPE = 'grdBILLING_INFO_ctl0' + i + '_' + 'lblTRAN_TYPE';
                    var rowcntrlversion = 'grdBILLING_INFO_ctl0' + i + '_' + 'hidinsdPOLICY_VERSION_ID';
                    
                }
                else {
                    var rowcntrl = 'grdBILLING_INFO_ctl' + i + '_' + objInstallment_CONTROL;
                    var rowtran_cntrl = 'grdBILLING_INFO_ctl' + i + '_' + objTRAN_Cntrl;
                    as[i - 2] = 'grdBILLING_INFO_ctl' + i + '_' + objcsvcontrol;
                    var TRAN_TYPE = 'grdBILLING_INFO_ctl' + i + '_' + 'lblTRAN_TYPE';
                    var rowcntrlversion = 'grdBILLING_INFO_ctl' + i + '_' + 'hidinsdPOLICY_VERSION_ID';
                    
                }
                var rowcntrlvalue = document.getElementById(rowcntrl).value == '' ? '0' : document.getElementById(rowcntrl).value;
//                var rowtran_cntrlvalue = document.getElementById(rowtran_cntrl).value == '' ? '0' : document.getElementById(rowtran_cntrl).value;

                rowcntrlvalue = FormatAmountForSum(rowcntrlvalue)
               
                //                rowtran_cntrlvalue = FormatAmountForSum(rowtran_cntrlvalue);
                if (document.getElementById(TRAN_TYPE).innerHTML.indexOf('END') != -1 && document.getElementById(rowcntrlversion).value == document.getElementById("hidPOLICY_VERSION_ID").value) {
                    
                    END_PREMIUM = parseFloat(END_PREMIUM) + parseFloat(rowcntrlvalue)
                 }

                sum = parseFloat(sum) + parseFloat(rowcntrlvalue)//+ parseFloat(rowtran_cntrlvalue);

                
            }


            if (document.getElementById('hidPROCESS_ID').value == '3' || document.getElementById('hidPROCESS_ID').value == '14') {
                if (splID[3] == "txtINTEREST") {
                    document.getElementById("txtTOTAL_TRAN_INTEREST_AMOUNT").value = formatAmount(parseFloat(END_PREMIUM));
                 }

                if (splID[3] == "txtFEE") {
                    document.getElementById("txtTOTAL_TRAN_FEES").value = formatAmount(parseFloat(END_PREMIUM));
                }
                if (splID[3] == "txtTAXES") {
                    document.getElementById("txtTOTAL_TRAN_TAXES").value = formatAmount(parseFloat(END_PREMIUM));
                }
            }//

            sum = roundNumber(sum, 2)
            //Added by Lalit itrack # 1761
            if (splID[3] == "txtINTEREST" || splID[3] == "txtFEE" || splID[3] == "txtTAXES") {
                document.getElementById(objTOTAL_CONTROL).value = formatAmount(parseFloat(sum));
                var hidId = objTOTAL_CONTROL.replace('txt', 'hid')
                document.getElementById(hidId).value = formatAmount(parseFloat(sum)) ;
                TotalPremium = sum;
            }


            // itrack no 693 for reference
            // parseFloat automatically round off number length greater than 13 digit so for that round off to 0.
            if (parseFloat(TotalPremium)> 9999999999999 ) {
                PagePost = true;
                return true;
             }
            
            
            if (TotalPremium < sum || TotalPremium > sum) {
                document.getElementById("lblMessage").innerText = '';
                document.getElementById("lblcustommsg").innerText = document.getElementById(as[0]).innerText;
                PagePost = false;

                return false
            } else {
                document.getElementById("lblcustommsg").innerText = '';
                PagePost = true;

                return true;
            }
        }
        //Function Name =>  roundNumber
        //Description   =>  round a folt no at 2 position


        function roundNumber(num, dec) {
            var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
            return result;
        }

        //Calculate sum of Total amount controls i.e Total Policy Preimum + Total Policy Interest Amount +  Total Policy Fees + Total policy Taxes
        //Format calculated sum and assign it into hidden field and Total Amount control
        function CalcTotalAmount(obj) {


            var txtTotalPREMIUM = document.getElementById("txtTOTAL_PREMIUM").value == '' ? document.getElementById("txtTOTAL_PREMIUM").value = '0' : document.getElementById("txtTOTAL_PREMIUM").value;
            document.getElementById("hidTOTAL_PREMIUM").value = txtTotalPREMIUM;
            txtTotalPREMIUM = FormatAmountForSum(txtTotalPREMIUM);
            if (isNaN(parseFloat(txtTotalPREMIUM)))
                txtTotalPREMIUM = 0

            var txtTotalINTEREST_AMOUNT = document.getElementById("txtTOTAL_INTEREST_AMOUNT").value == '' ? document.getElementById("txtTOTAL_INTEREST_AMOUNT").value = '0' : document.getElementById("txtTOTAL_INTEREST_AMOUNT").value;
            document.getElementById("hidTOTAL_INTEREST_AMOUNT").value = txtTotalINTEREST_AMOUNT;
            txtTotalINTEREST_AMOUNT = FormatAmountForSum(txtTotalINTEREST_AMOUNT);
            if (isNaN(parseFloat(txtTotalINTEREST_AMOUNT)))
                txtTotalINTEREST_AMOUNT = 0


            var txtTotalFEE = document.getElementById("txtTOTAL_FEES").value == '' ? document.getElementById("txtTOTAL_FEES").value = '0' : document.getElementById("txtTOTAL_FEES").value;
            document.getElementById("hidTOTAL_FEES").value = txtTotalFEE;
            txtTotalFEE = FormatAmountForSum(txtTotalFEE);
            if (isNaN(parseFloat(txtTotalFEE)))
                txtTotalFEE = 0

            var txtTotalTAXES = document.getElementById("txtTOTAL_TAXES").value == '' ? document.getElementById("txtTOTAL_TAXES").value = '0' : document.getElementById("txtTOTAL_TAXES").value;
            document.getElementById("hidTOTAL_TAXES").value = txtTotalTAXES;
            txtTotalTAXES = FormatAmountForSum(txtTotalTAXES);
            if (isNaN(parseFloat(txtTotalTAXES)))
                txtTotalTAXES = 0

            var Sum = parseFloat(txtTotalPREMIUM) + parseFloat(txtTotalINTEREST_AMOUNT) + parseFloat(txtTotalFEE) + parseFloat(txtTotalTAXES); //+parseFloat()+


            switch (obj.id) {    // try genrate related gridview textbox control id for validate installment total amd total value
                case "txtTOTAL_PREMIUM":
                    objInstallment_CONTROL = "txtPREMIUM";
                    break;

                case "txtTOTAL_INTEREST_AMOUNT":
                    objInstallment_CONTROL = "txtINTEREST_AMOUNT";
                    break;

                case "txtTOTAL_FEES":
                    objInstallment_CONTROL = "txtFEE";
                    break;

                case "txtTOTAL_TAXES":
                    objInstallment_CONTROL = "txtTAXES";
                    break;
                default:
                    break;
            }
            var cntrlid = 'grdBILLING_INFO_ctl0' + '_' + objInstallment_CONTROL;


            ValidateColumnTotal(cntrlid);  //call  function at total amount controls onblur which check the sum of all installment is equal to total amount or not

            if (Sum != NaN) {
                document.getElementById("txtTOTAL_AMOUNT").value = formatAmount(parseFloat(Sum));
                document.getElementById("hidTOTAL_AMOUNT").value = formatAmount(parseFloat(Sum));

            } else {
                document.getElementById("txtTOTAL_AMOUNT").value = '';
            }

        }

        //disable plan id combo require filed validator at any post back accept to readjust btn click
        function DisablePlanIDrfv() {

            document.getElementById("rfvBILLING_PLAN").setAttribute("enabled", false);
            document.getElementById("rfvBILLING_PLAN").setAttribute("isvalid", true);
            document.getElementById("rfvBILLING_PLAN").style.display = "none";


        }
        //disable page validators when genrating installments
        function DisableValidator(Type) {
           
            if (Type == 'grid') { //disable validator for genrate installment s
                if (typeof (Page_Validators) == 'undefined')
                    var i, val;
             
                for (i = 0; i < Page_Validators.length; i++) {
                    val = Page_Validators[i];
                    if (val.id.indexOf('grdBILLING_INFO') != -1 || val.id.indexOf('TENTATIVE_DATE') != -1 || val.id.indexOf('DFI_ACC_NO') != -1 || val.id.indexOf('TRANSIT_ROUTING_NO') != -1 || val.id.indexOf('CARD_NO') != -1)                        
                        val.setAttribute('enabled', false);

                    DisableEnableValidatorsCC(false);
                }
            } else if (Type == 'all') { ///disable all validator at
                if (typeof (Page_Validators) == 'undefined')
                    var i, val;

                for (i = 0; i < Page_Validators.length; i++) {
                    val = Page_Validators[i];
                    // if (val.id.indexOf('grdBILLING_INFO') != -1)
                    //if (val.id.indexOf('_rev') != -1)
                    val.setAttribute('enabled', false);
                }
            }

           
        }
        //enable plan id combo require filed validator at readjust btn click
        function EnablePlanIDrfv() {
            if (document.getElementById("cmbBILLING_PLAN").value == "") {

                document.getElementById("rfvBILLING_PLAN").setAttribute("enabled", true);
                document.getElementById("rfvBILLING_PLAN").setAttribute("isvalid", false);
                document.getElementById("rfvBILLING_PLAN").style.display = "inline";
                return false;
            } else {
                return true;
            }
        }
        function setBillingPlan() {
            if (document.getElementById('cmbBILLING_PLAN').value != "") {
                if (document.getElementById('hidPOLICY_STATUS').value != 'UENDRS' && document.getElementById('hidPROCESS_ID').value != '3')
                    document.getElementById('hidSELECTED_PLAN_ID').value = document.getElementById('cmbBILLING_PLAN').value;
             }
         }
        //Check total contorols values is equal to installments totals at save btn click


        function checkBlankGrid() {
          
            if (document.getElementById('pnlEFTCust') != null) {
                disableEFTValidor(false);
            }
            if (document.getElementById('pnlCCCust') != null) {
               
                DisableEnableValidatorsCC(false);
            }
            Ar = new Array();
            Ar[0] = 'grdBILLING_INFO_ctl0_txtPREMIUM';
            Ar[1] = 'grdBILLING_INFO_ctl0_txtINTEREST_AMOUNT';
            Ar[2] = 'grdBILLING_INFO_ctl0_txtFEE';
            Ar[3] = 'grdBILLING_INFO_ctl0_txtTAXES';
            for (n = 0; n < parseInt(Ar.length); n++) {
                var validd = ValidateColumnTotal(Ar[n]);
                if (validd)
                    continue;
                else break;
            }
            validateTRANPremium();
            if (PagePost) {
                var hidROWCOUNT = document.getElementById("hidROWCOUNT").value;
                if (hidROWCOUNT == '') {
                    return false;
                } else if (parseInt(hidROWCOUNT) > 0) {
                   
                    return Page_ClientValidate();
                    isPageValid_ToSubmit = true;
                }
            } else {
                isPageValid_ToSubmit = false;
                return false;
            }
        }

        //J-query Start
        $(document).ready(function() {
            var hidROWCOUNT = document.getElementById("hidROWCOUNT").value;
            var gridid;
            var DATE;
            var PREMIUM;
            var INTEREST_AMOUNT;
            var FEE;
            var TAXES;
            var TRAN_AMOUNT;
            var TRAN_INTEREST;
            var TRAN_FEE;
            var TRAN_TAXES;

            var Arr
            var arIdArr = new Array();

            for (i = 2; i < parseInt(hidROWCOUNT) + 2; i++) {
                if (i < 10)
                    gridid = 'grdBILLING_INFO_ctl0' + i + '_';
                else
                    gridid = 'grdBILLING_INFO_ctl' + i + '_';
                Arr = new Array();
                DATE = gridid + 'txtINSTALLMENT_EFFECTIVE_DATE';
                PREMIUM = gridid + 'txtPREMIUM';
                INTEREST_AMOUNT = gridid + 'txtINTEREST_AMOUNT';
                FEE = gridid + 'txtFEE';
                TAXES = gridid + 'txtTAXES';

             //   TRAN_AMOUNT = gridid + 'txtTRAN_PREMIUM_AMOUNT';
//                TRAN_INTEREST = gridid + 'txtTRAN_INTEREST_AMOUNT';
//                TRAN_FEE = gridid + 'txtTRAN_FEE';
//                TRAN_TAXES = gridid + 'txtTRAN_TAXES';


                Arr[i - 2] = DATE;
                Arr[i - 2 + 1] = PREMIUM;
                Arr[i - 2 + 2] = INTEREST_AMOUNT;
                Arr[i - 2 + 3] = FEE;
                Arr[i - 2 + 4] = TAXES;
//                Arr[i - 2 + 5] = TRAN_AMOUNT;
//                Arr[i - 2 + 6] = TRAN_INTEREST;
//                Arr[i - 2 + 7] = TRAN_FEE;
//                Arr[i - 2 + 8] = TRAN_TAXES;
                arIdArr[i - 2] = Arr;

            }
            for (i = 0; i < parseInt(arIdArr.length); i++) {
                var Arr = arIdArr[i];
                for (j = 0; j < parseInt(Arr.length); j++) {
                    $("#" + Arr[j]).change(function() {
                        change(this);
                    })
                }
            }

        })

        function change(obj) {
            RetunrIdPart(obj.id)
        }
        function RetunrIdPart(objcontrolID) {

            var splID = objcontrolID.split('_');

            if (document.getElementById("hidArrObj").value != '')
                arspl = document.getElementById("hidArrObj").value.split(',');
            else
                arspl = new Array();

            if (document.getElementById("hidUPDATEDROWS").value != '') {
                if (document.getElementById("hidSPLID").value != splID[2]) {
                    var notexist = false;
                    for (i = 0; i < arspl.length; i++) {
                        if (arspl[i] == splID[2])
                            notexist = true;
                    }
                    if (!notexist) {
                        var hidUPDATEDROWS = document.getElementById("hidUPDATEDROWS").value;
                        hidUPDATEDROWS = parseInt(hidUPDATEDROWS) + 1;
                        document.getElementById("hidUPDATEDROWS").value = hidUPDATEDROWS;
                        document.getElementById("hidSPLID").value = splID[2]
                        arspl[parseInt(hidUPDATEDROWS) - 1] = splID[2];
                    }
                }

            } else {
                document.getElementById("hidUPDATEDROWS").value = 1;
                document.getElementById("hidSPLID").value = splID[2]
                arspl[0] = splID[2];
            }
            document.getElementById("hidArrObj").value = arspl.join();
            var hidUpdateFlag;
            switch (splID[3]) {
                case "txtPREMIUM":
                    hidUpdateFlag = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'hidUPDATEFlag';
                    document.getElementById(hidUpdateFlag).value = 1;
                    break;

                case "txtINTEREST":
                    hidUpdateFlag = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'hidUPDATEFlag';
                    document.getElementById(hidUpdateFlag).value = 1;
                    break;

                case "txtFEE":
                    hidUpdateFlag = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'hidUPDATEFlag';
                    document.getElementById(hidUpdateFlag).value = 1;
                    break;

                case "txtTAXES":
                    hidUpdateFlag = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'hidUPDATEFlag';
                    document.getElementById(hidUpdateFlag).value = 1;
                    break;

                case "txtINSTALLMENT":
                    hidUpdateFlag = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'hidUPDATEFlag';
                    document.getElementById(hidUpdateFlag).value = 1;
                    break;

                default:
                    if (splID[3] + '_' + splID[4] == "txtTRAN_PREMIUM") {
                        hidUpdateFlag = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'hidUPDATEFlag';
                        document.getElementById(hidUpdateFlag).value = 1;
                    }
                    else if (splID[3] + '_' + splID[4] == "txtTRAN_INTEREST") {
                        hidUpdateFlag = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'hidUPDATEFlag';
                        document.getElementById(hidUpdateFlag).value = 1;
                    }
                    else if (splID[3] + '_' + splID[4] == "txtTRAN_FEE") {
                        hidUpdateFlag = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'hidUPDATEFlag';
                        document.getElementById(hidUpdateFlag).value = 1;
                    }
                    else if (splID[3] + '_' + splID[4] == "txtTRAN_TAXES") {
                        hidUpdateFlag = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'hidUPDATEFlag';
                        document.getElementById(hidUpdateFlag).value = 1;
                    }
                    break;
            }
        }

        //End- J-Query
        function enableReadjust() {
            if (document.getElementById("btnAdjust") != null) {
                if (document.getElementById("cmbBILLING_PLAN").value != '') {
                    if (document.getElementById("btnGenrateInstallment").disabled == true) {
                        if (document.getElementById("hidSELECTED_PLAN_ID").value != document.getElementById("cmbBILLING_PLAN").value)
                            document.getElementById("btnAdjust").style.display = 'none';
                        else
                            document.getElementById("btnAdjust").style.display = 'none';
                    }
                    else
                        document.getElementById("btnAdjust").style.display = 'none';

                }
                else
                    if (document.getElementById("btnAdjust") != null) {
                        document.getElementById("btnAdjust").style.display = 'none';
                }

            }

            if (document.getElementById("hidREL_INSTLL_NO").value == document.getElementById("hidROWCOUNT").value) {
                if (document.getElementById("btnAdjust") != null && document.getElementById("btnSave") != null) {
                    document.getElementById("btnAdjust").style.display = 'none';
                    document.getElementById("btnSave").style.display = 'none';
                }
            }

        }

        function OpenBoletoReprint() {
           
            document.location = "/cms/Policies/Aspx/BoletoRePrint.aspx?CALLEDFROM=" + document.getElementById("hidCALLED_FROM").value;
            return false;
        }


        //Added by Praveen Kumar for Generate Boleto
        function GenerateBoleto(Installmentno, Type, Policy_version) {
            var str;
            //str = "http://localhost:4846/Bank/HSBCBank.aspx
            //GENERATE_ALL_INSTALLMENT=ALL --For genraet all boleto
            str = "/cms/Policies/Aspx/PolicyBoleto.aspx?CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value + "&POLICY_ID=" + document.getElementById("hidPOLICY_ID").value
		              + "&POLICY_VERSION_ID=" + Policy_version + "&INSTALLMENT_NO=" + Installmentno
            if (Type != '' && Type != 'undefined')
                str = str + '&GENERATE_ALL_INSTALLMENT=' + Type

            window.open(str, "Boleto", "resizable=yes,toolbar=0,location=0, directories=0, status=0, menubar=0,scrollbars=yes,width=800,height=500,left=150,top=50");

        }


        /* 
        add transaction amount and total amounts when user edit transaction amount        
        */



        function addTranAmount(objtxt) {

            if (document.getElementById('txtTOTAL_TRAN_PREMIUM').value != "")
                var tranPremium = FormatAmountForSum(document.getElementById('txtTOTAL_TRAN_PREMIUM').value);
                else
                var tranPremium = 0;
            if (document.getElementById('txtTOTAL_TRAN_INTEREST_AMOUNT').value != "")
                var tranInt = FormatAmountForSum(document.getElementById('txtTOTAL_TRAN_INTEREST_AMOUNT').value);
            else
                var tranInt = 0;
            if (document.getElementById('txtTOTAL_TRAN_FEES').value != "")
                var tranFee = FormatAmountForSum(document.getElementById('txtTOTAL_TRAN_FEES').value);
            else
                var tranFee = 0
            if (document.getElementById('txtTOTAL_TRAN_TAXES').value != "")
                var tranTaxes = FormatAmountForSum(document.getElementById('txtTOTAL_TRAN_TAXES').value);
            else
                var tranTaxes = 0;

            // nthidTotalPremium 
            // nhidTotalInt
            // nhidTotalFee
            // nhidTotalTaxes
            var SumPrmAmount = parseFloat(tranPremium) + parseFloat(Policy_Total_premium);
            if (!isNaN(SumPrmAmount)) {
                document.getElementById('txtTOTAL_PREMIUM').value = formatAmount(SumPrmAmount);
                document.getElementById('hidTOTAL_PREMIUM').value = formatAmount(SumPrmAmount);
            } else {
            document.getElementById('txtTOTAL_PREMIUM').value = formatAmount(Policy_Total_premium);
            document.getElementById('hidTOTAL_PREMIUM').value = formatAmount(Policy_Total_premium);
            
              }
            

            var SumIntrAmount = parseFloat(tranInt) + parseFloat(nhidTotalInt)
            if (!isNaN(SumIntrAmount)) {
                document.getElementById('txtTOTAL_INTEREST_AMOUNT').value = formatAmount(SumIntrAmount);
            } else {
            document.getElementById('txtTOTAL_INTEREST_AMOUNT').value = formatAmount(nhidTotalInt);
             }

            var SumFeesAmount = parseFloat(tranFee) + parseFloat(nhidTotalFee)
            if (!isNaN(SumFeesAmount))
                document.getElementById('txtTOTAL_FEES').value = formatAmount(SumFeesAmount);
            else {
                document.getElementById('txtTOTAL_FEES').value = formatAmount(nhidTotalFee);
             }

            var SumTaxesAmount = parseFloat(tranTaxes) + parseFloat(nhidTotalTaxes)
            if (!isNaN(SumTaxesAmount))
                document.getElementById('txtTOTAL_TAXES').value = formatAmount(SumTaxesAmount);
                else
                    document.getElementById('txtTOTAL_TAXES').value = formatAmount(nhidTotalTaxes);

            document.getElementById('txtTOTAL_AMOUNT').value = parseFloat(document.getElementById('txtTOTAL_PREMIUM').value) + parseFloat(document.getElementById('txtTOTAL_INTEREST_AMOUNT').value) + parseFloat(document.getElementById('txtTOTAL_FEES').value) + parseFloat(document.getElementById('txtTOTAL_TAXES').value)
            document.getElementById('hidTOTAL_AMOUNT').value = document.getElementById('txtTOTAL_AMOUNT').value;

            document.getElementById('txtTOTAL_TRAN_AMOUNT').value = formatAmount(parseFloat(tranPremium) + parseFloat(tranInt) + parseFloat(tranFee) + parseFloat(tranTaxes));
            document.getElementById('hidTOTAL_TRAN_AMOUNT').value = parseFloat(tranPremium) + parseFloat(tranInt) + parseFloat(tranFee) + parseFloat(tranTaxes)


            //document.getElementById('hidTOTAL_PREMIUM').value = 
            document.getElementById('hidTOTAL_INTEREST_AMOUNT').value = document.getElementById('txtTOTAL_INTEREST_AMOUNT').value
            document.getElementById('hidTOTAL_FEES').value = document.getElementById('txtTOTAL_TAXES').value
            document.getElementById('hidTOTAL_TAXES').value = document.getElementById('txtTOTAL_TAXES').value

            document.getElementById('hidTOTAL_PREMIUM').value = document.getElementById('txtTOTAL_PREMIUM').value


            //document.getElementById('txtTOTAL_INFO_PRM').value = document.getElementById('txtTOTAL_PREMIUM').value;
            //document.getElementById('txtTOTAL_CHANGE_INFORCE_PRM').value = formatAmount(tranPremium)
            //document.getElementById('hidTOTAL_CHANGE_INFORCE_PRM').value = tranPremium
            
            switch (objtxt.id) {
                case 'txtTOTAL_TRAN_PREMIUM':
                    CalcTotalAmount(document.getElementById('txtTOTAL_PREMIUM'));
                    break;
                case 'txtTOTAL_TRAN_INTEREST_AMOUNT':
                    CalcTotalAmount(document.getElementById('txtTOTAL_INTEREST_AMOUNT'));
                    break;
                case 'txtTOTAL_TRAN_FEES':
                    CalcTotalAmount(document.getElementById('txtTOTAL_FEES'));
                    break;
                case 'txtTOTAL_TRAN_TAXES':
                    CalcTotalAmount(document.getElementById('txtTOTAL_TAXES'));
                    break;

            }
            
        }
        function hidAssignTotoal() {
           
            if (document.getElementById('hidNBS_PREMIUM').value != "")
                Policy_Total_premium = FormatAmountForSum(document.getElementById('hidNBS_PREMIUM').value);
            else
                Policy_Total_premium = "0";
            if (document.getElementById('hidTOTAL_PREMIUM').value != "")
                nthidTotalPremium = FormatAmountForSum(document.getElementById('hidTOTAL_PREMIUM').value);
            else
                nthidTotalPremium = "0";
            if (document.getElementById('hidNBS_INTEREST').value != "")
                nhidTotalInt = FormatAmountForSum(document.getElementById('hidNBS_INTEREST').value);
            else
                nhidTotalInt = "0";
            if (document.getElementById('hidNBS_FEES').value != "")
                nhidTotalFee = FormatAmountForSum(document.getElementById('hidNBS_FEES').value);
            else
                nhidTotalFee = "0";
            if (document.getElementById('hidNBS_TAXES').value != "")
                nhidTotalTaxes = FormatAmountForSum(document.getElementById('hidNBS_TAXES').value);
            else
                nhidTotalTaxes = "0";
           
            
        }
        function addInfoAmt() {
            var amtInfo = document.getElementById('txtTOTAL_END_INFO').value
            var amtotal = document.getElementById('txtTOTAL_PREMIUM').value
            document.getElementById('txtTOTAL_PREMIUM').value = parseFloat(nthidTotalPremium) + parseFloat(amtInfo);
            
            document.getElementById('hidTOTAL_PREMIUM').value = document.getElementById('txtTOTAL_PREMIUM').value;
            
            // document.getElementById('hidTOTAL_PREMIUM').value = parseFloat(nthidTotalPremium) + parseFloat(amtInfo);
            document.getElementById('txtTOTAL_AMOUNT').value = parseFloat(document.getElementById('txtTOTAL_PREMIUM').value) + parseFloat(document.getElementById('txtTOTAL_INTEREST_AMOUNT').value) + parseFloat(document.getElementById('txtTOTAL_FEES').value) + parseFloat(document.getElementById('txtTOTAL_TAXES').value)

        }
        function validateTRANPremium() {
            if (document.getElementById('hidPOLICY_STATUS').value == 'UENDRS' && document.getElementById('hidPOLICY_STATUS').value == 'MENDORSE') {
                var hidROWCOUNT = document.getElementById('hidROWCOUNT').value;
                var Inssum = '';
                var rowcntrlversion, rowcntrltr, txtrowcntrl;
                for (i = 2; i < parseInt(hidROWCOUNT) + 2; i++) {
                    if (parseInt(i) < 10) {
                        var rowcntrl = 'grdBILLING_INFO_ctl0' + i + '_' //+ txtPREMIUM;               
                    }
                    else {
                        var rowcntrl = 'grdBILLING_INFO_ctl' + i + '_' //+ txtPREMIUM;                   
                    }
                    rowcntrltr = rowcntrl + 'lblTRAN_TYPE';
                    rowcntrlversion = rowcntrl + 'lblPOLICY_VERSION_ID';
                    txtrowcntrl = rowcntrl + 'txtPREMIUM';
                    if (document.getElementById(rowcntrltr).innerText == 'END' && document.getElementById(rowcntrlversion).innerText == document.getElementById('hidPOLICY_VERSION_ID').value) {
                        if (Inssum != '')
                            Inssum = parseFloat(Inssum) + parseFloat(document.getElementById(txtrowcntrl).value);
                        else
                            Inssum = parseFloat(document.getElementById(txtrowcntrl).value);
                    }
                }
                var trnAMOUNT = document.getElementById('txtTOTAL_TRAN_PREMIUM').value;
                trnAMOUNT = ReplaceAll(trnAMOUNT, ',', '');
                if (parseFloat(trnAMOUNT) != parseFloat(Inssum)) {

                    document.getElementById("lblMessage").innerText = '';
                    document.getElementById("lblcustommsg").innerText = 'Total transaction premium and Policy current version installment premium total are not same '
                    PagePost = false;
                    return false
                } else {
                    document.getElementById("lblMessage").innerText = '';
                    PagePost = true;
                    return true;
                }
            }
        }
        function RemoveReadonlyFields() {
            if (document.getElementById('txtTOTAL_INFO_PRM') != null  ) 
                document.getElementById('txtTOTAL_INFO_PRM').readOnly = false;
            if (document.getElementById('txtTOTAL_CHANGE_INFORCE_PRM') != null)
                document.getElementById('txtTOTAL_CHANGE_INFORCE_PRM').readOnly = false;
             
        }
       
        function EnableReadonly() {

            var scrXML = '<%= gstrSecurityXML %>';
            var frm = document.POL_BILLING_INFO;
            if (scrXML == '<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>') {
                for (i = 0; i < frm.length; i++) {
                    e = frm.elements[i];
                    if (e.type == 'input' || e.type == 'text')
                        e.readOnly = true;
                }
            }

        }
        function FormatAmountForSum(num) {
            num = ReplaceAll(num, sGroupSep, '');
            num = ReplaceAll(num, sDecimalSep, '.');
            return num;
        }
        function formatAmountByCulture() {
            var frm = document.POL_BILLING_INFO;
            for (i = 0; i < frm.length; i++) {
                e = frm.elements[i];
                if (e.type == 'text') {
                    if (e.id.indexOf('_DATE') == -1 && e.id.indexOf('_ZIP') == -1 && e.id.indexOf('_CVV_NUMBER') == -1 && e.id.indexOf('_CITY') == -1 && e.id.indexOf('_ADDRESS2') == -1 && e.id.indexOf('_ADDRESS1') == -1 && e.id.indexOf('_NAME') == -1 && e.id.indexOf('CARD_NO') == -1) {
                        e.value = formatAmount(e.value);
                    }
                }
            }
        }
        function StateFee(objSource, objArgs) {  
            if (document.getElementById('revTOTAL_FEES').isvalid == true) {
                var statefees = document.getElementById('hidSTATE_FEES').value;
                var policy_fees = document.getElementById('txtTOTAL_FEES').value;
                if (parseInt(policy_fees) < parseInt(statefees)) {
                    objArgs.IsValid = false;
                } else { objArgs.IsValid = true; }
            } 
        }
        //------------Start END FEDERAL ID Encryption--------------------------
        function FEDERAL_ID_change() {

            document.getElementById('txtFEDERAL_ID').value = '';
            document.getElementById('txtFEDERAL_ID').style.display = 'inline';

            document.getElementById("revFEDERAL_ID").setAttribute('enabled', true);
            if (document.getElementById("btnENCRYP_FEDERAL_ID").value == 'Edit')
                document.getElementById("btnENCRYP_FEDERAL_ID").value = 'Cancel';
            else if (document.getElementById("btnENCRYP_FEDERAL_ID").value == 'Cancel')
                FEDERAL_ID_hide();
            else
                document.getElementById("btnENCRYP_FEDERAL_ID").style.display = 'none';


        }
        function FEDERAL_ID_hide() {
            if (document.getElementById("btnENCRYP_FEDERAL_ID") != null) {

                if (document.getElementById('hid_ENCRYP_FEDERAL_ID').value == '0' || document.getElementById('hid_ENCRYP_FEDERAL_ID').value == '') {
                    document.getElementById('txtFEDERAL_ID').style.display = 'inline';
                    document.getElementById('btnENCRYP_FEDERAL_ID').style.display = 'none';
                }
                else {
                    document.getElementById("btnENCRYP_FEDERAL_ID").style.display = 'inline';
                    document.getElementById('txtFEDERAL_ID').value = '';
                    document.getElementById('txtFEDERAL_ID').style.display = 'none';
                    document.getElementById("revFEDERAL_ID").style.display = 'none';
                    document.getElementById("revFEDERAL_ID").setAttribute('enabled', false);
                    document.getElementById("btnENCRYP_FEDERAL_ID").value = 'Edit';
                }
            }
        }
        //--------------------------END FEDERAL ID--------------------------
        //-----------------------------------DFI_ACC_NO_change()---------------------		

        function DFI_ACC_NO_change() {
            document.getElementById('txtDFI_ACC_NO').value = '';
            document.getElementById('txtDFI_ACC_NO').style.display = 'inline';

            document.getElementById("revDFI_ACC_NO").setAttribute('enabled', true);
            document.getElementById("rfvDFI_ACC_NO").setAttribute('enabled', true);
            document.getElementById("csvDFI_ACC_NO").setAttribute('enabled', true);


            if (document.getElementById("btnENCRYP_DFI_ACC_NO").value == 'Edit') {
                document.getElementById("btnENCRYP_DFI_ACC_NO").value = 'Cancel';
                document.getElementById("chkREVERIFIED_AC").checked = true;

            }
            else if (document.getElementById("btnENCRYP_DFI_ACC_NO").value == 'Cancel') {
                document.getElementById("chkREVERIFIED_AC").checked = false;
                DFI_ACC_NO_hide();
            }
            else
                document.getElementById("btnENCRYP_DFI_ACC_NO").style.display = 'none';


        }
        function DFI_ACC_NO_hide() {

            if (document.getElementById("btnENCRYP_DFI_ACC_NO") != null) {

                if (document.getElementById('hid_ENCRYP_DFI_ACC_NO').value == '0') {
                    document.getElementById('txtDFI_ACC_NO').style.display = 'inline';
                    document.getElementById('btnENCRYP_DFI_ACC_NO').style.display = 'none';
                    document.getElementById("chkREVERIFIED_AC").checked = true;
                }
                else {



                    document.getElementById("btnENCRYP_DFI_ACC_NO").style.display = 'inline';
                    document.getElementById('txtDFI_ACC_NO').value = '';
                    document.getElementById('txtDFI_ACC_NO').style.display = 'none';

                    document.getElementById("revDFI_ACC_NO").style.display = 'none';
                    document.getElementById("revDFI_ACC_NO").setAttribute('enabled', false);

                    document.getElementById("rfvDFI_ACC_NO").style.display = 'none';
                    document.getElementById("rfvDFI_ACC_NO").setAttribute('enabled', false);

                    document.getElementById("csvDFI_ACC_NO").style.display = 'none';
                    document.getElementById("csvDFI_ACC_NO").setAttribute('enabled', false);

                    document.getElementById("btnENCRYP_DFI_ACC_NO").value = 'Edit';

                }

            }



        }
        function disableEFTValidor(Disable) {
            //For DFI ACC NO
            document.getElementById("revDFI_ACC_NO").style.display = 'none';
            document.getElementById("revDFI_ACC_NO").setAttribute('enabled', Disable);

            document.getElementById("rfvDFI_ACC_NO").style.display = 'none';
            document.getElementById("rfvDFI_ACC_NO").setAttribute('enabled', Disable);

            document.getElementById("csvDFI_ACC_NO").style.display = 'none';
            document.getElementById("csvDFI_ACC_NO").setAttribute('enabled', Disable);

            //For Rounting No
            document.getElementById("rfvTRANSIT_ROUTING_NO").style.display = 'none';
            document.getElementById("rfvTRANSIT_ROUTING_NO").setAttribute('enabled', Disable);

            document.getElementById("csvTRANSIT_ROUTING_NO").style.display = 'none';
            document.getElementById("csvTRANSIT_ROUTING_NO").setAttribute('enabled', Disable);

            document.getElementById("csvVERIFYFY_TRANSIT_ROUTING_NO").style.display = 'none';
            document.getElementById("csvVERIFYFY_TRANSIT_ROUTING_NO").setAttribute('enabled', Disable);

            document.getElementById("csvVERIFYFY_TRANSIT_ROUTING_NO_LENGHT").style.display = 'none';
            document.getElementById("csvVERIFYFY_TRANSIT_ROUTING_NO_LENGHT").setAttribute('enabled', Disable);

            document.getElementById("revTRANSIT_ROUTING_NO").style.display = 'none';
            document.getElementById("revTRANSIT_ROUTING_NO").setAttribute('enabled', Disable);

            //For Tentative Date
            document.getElementById("rfvEFT_TENTATIVE_DATE").style.display = 'none';
            document.getElementById("rfvEFT_TENTATIVE_DATE").setAttribute('enabled', Disable);

            document.getElementById("rngEFT_TENTATIVE_DATE").style.display = 'none';
            document.getElementById("rngEFT_TENTATIVE_DATE").setAttribute('enabled', Disable);
            
                                                
         }
        
        //--------------------------END DFI NO

        //--------------------START TRANS ROUTING NO----------------

        function TRANSIT_ROUTING_NO_change() {
            document.getElementById('txtTRANSIT_ROUTING_NO').value = '';
            document.getElementById('txtTRANSIT_ROUTING_NO').style.display = 'inline';

            document.getElementById("rfvTRANSIT_ROUTING_NO").setAttribute('enabled', true);
            document.getElementById("csvTRANSIT_ROUTING_NO").setAttribute('enabled', true);
            document.getElementById("csvVERIFYFY_TRANSIT_ROUTING_NO").setAttribute('enabled', true);
            document.getElementById("csvVERIFYFY_TRANSIT_ROUTING_NO_LENGHT").setAttribute('enabled', true);
            document.getElementById("revTRANSIT_ROUTING_NO").setAttribute('enabled', true);



            if (document.getElementById("btnENCRYP_TRANSIT_ROUTING_NO").value == 'Edit') {
                document.getElementById("btnENCRYP_TRANSIT_ROUTING_NO").value = 'Cancel';
                document.getElementById("chkREVERIFIED_AC").checked = true;
            }
            else if (document.getElementById("btnENCRYP_TRANSIT_ROUTING_NO").value == 'Cancel') {
                ENCRYP_TRANSIT_ROUTING_NO_hide();
                document.getElementById("chkREVERIFIED_AC").checked = false;
            }
            else
                document.getElementById("btnENCRYP_TRANSIT_ROUTING_NO").style.display = 'none';


        }

        function ENCRYP_TRANSIT_ROUTING_NO_hide() {

            if (document.getElementById("btnENCRYP_TRANSIT_ROUTING_NO") != null) {

                if (document.getElementById('hid_ENCRYP_TRANSIT_ROUTING_NO').value == '0') {
                    document.getElementById('txtTRANSIT_ROUTING_NO').style.display = 'inline';
                    document.getElementById('btnENCRYP_TRANSIT_ROUTING_NO').style.display = 'none';
                    document.getElementById("chkREVERIFIED_AC").checked = true;
                }
                else {

                    document.getElementById("btnENCRYP_TRANSIT_ROUTING_NO").style.display = 'inline';
                    document.getElementById('txtTRANSIT_ROUTING_NO').value = '';
                    document.getElementById('txtTRANSIT_ROUTING_NO').style.display = 'none';

                    document.getElementById("rfvTRANSIT_ROUTING_NO").style.display = 'none';
                    document.getElementById("rfvTRANSIT_ROUTING_NO").setAttribute('enabled', false);

                    document.getElementById("csvTRANSIT_ROUTING_NO").style.display = 'none';
                    document.getElementById("csvTRANSIT_ROUTING_NO").setAttribute('enabled', false);

                    document.getElementById("csvVERIFYFY_TRANSIT_ROUTING_NO").style.display = 'none';
                    document.getElementById("csvVERIFYFY_TRANSIT_ROUTING_NO").setAttribute('enabled', false);

                    document.getElementById("csvVERIFYFY_TRANSIT_ROUTING_NO_LENGHT").style.display = 'none';
                    document.getElementById("csvVERIFYFY_TRANSIT_ROUTING_NO_LENGHT").setAttribute('enabled', false);

                    document.getElementById("revTRANSIT_ROUTING_NO").style.display = 'none';
                    document.getElementById("revTRANSIT_ROUTING_NO").setAttribute('enabled', false);

                    document.getElementById("btnENCRYP_TRANSIT_ROUTING_NO").value = 'Edit';

                }

            }
        }

        //--------------------END ROUTING NO

        
        function SetEncrytion() {
            FEDERAL_ID_hide();
            DFI_ACC_NO_hide();
            ENCRYP_TRANSIT_ROUTING_NO_hide();
        }
        function showCCPanelJS() {
            var payPalID = document.getElementById('hidCCFlag').value;
            if (payPalID == "1") {
                document.getElementById('tblCC').style.display = 'inline';
                document.getElementById('tblbtnSavePayInfo').style.display = 'inline';
                DisableEnableValidatorsCC(true);
                document.getElementById('lblCancelPayPal').style.display = 'inline';

            }

        }
        function HideCCPanelJS() {

            document.getElementById('tblCC').style.display = 'none';
            DisableEnableValidatorsCC(false);
            document.getElementById('lblCancelPayPal').style.display = 'none';
            document.getElementById('txtCUSTOMER_FIRST_NAME').value = '';

            if (document.getElementById('tblCC').style.display == 'none') {
                document.getElementById('tblbtnSavePayInfo').style.display = 'none';
                if (document.getElementById('hidEFTFlag').value == "1")
                    document.getElementById('tblbtnSavePayInfo').style.display = 'inline';


            }

        }
        function FillCustomerName() {
            //alert(document.getElementById('hidCUSTOMER_INFO').value);

            var objXmlHandler = new XMLHandler();
            var tree = (objXmlHandler.quickParseXML(document.getElementById('hidCUSTOMER_INFO').value).getElementsByTagName('Table')[0]);
            var i = 0;

            for (i = 0; i < tree.childNodes.length; i++) {
                var nodeName = tree.childNodes[i].nodeName;

                var nodeValue;
                if (tree.childNodes[i].firstChild == 'undefined' || tree.childNodes[i].firstChild == null) {
                    nodeValue = '';
                }
                else {
                    nodeValue = tree.childNodes[i].firstChild.text;
                }
                switch (nodeName) {
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
                        SelectComboOption("cmbCUSTOMER_COUNTRY", nodeValue)
                        setCountryId();
                        break;
                    case "CUSTOMER_STATE":
                        BindCountryState();
                        //SelectComboOption("cmbCUSTOMER_STATE", nodeValue)
                        //sethidStateID
                        document.getElementById("hidSTATE_ID").value = nodeValue;
                        sethidStateID();
                        //SetRegisteredState();
                        break;
                    case "CUSTOMER_ZIP":
                        document.getElementById('txtCUSTOMER_ZIP').value = nodeValue;
                        break;
                }
            }
            return false;

        }
      
     function sethidStateID() {
         for (var j = 0; j < document.getElementById('cmbCUSTOMER_STATE').options.length; j++) {
             if (document.getElementById("hidSTATE_ID").value == document.getElementById('cmbCUSTOMER_STATE').options[j].value) {
                 document.getElementById('cmbCUSTOMER_STATE').options.selectedIndex = j;                
                 break;
             } 
         } 
     }
         
     
        function BindCountryState() {
            ServerMethod = "BillingInfo.aspx/FillState";
            SuccessMethod = "BindDropdown";
            ErrorMethod = "ShowError";
            TargetControl = "cmbCUSTOMER_STATE";
            ItemValue = "STATE_ID";
            ItemText = "STATE_NAME";
            var Parameters = "{'Param':'" + document.getElementById('cmbCUSTOMER_COUNTRY').value + "'}";
            Pagemethod(ServerMethod, Parameters, SuccessMethod, ErrorMethod, '#' + this.TargetControl, this.ItemValue, this.ItemText);
            setCountryId();       
         }
        function Pagemethod(ServerMethod, Parameters, SuccessMethod, ErrorMethod, ControlID, ItemValue, ItemText) {
         
            $.ajax({

                type: "POST",
                url: ServerMethod,
                data: Parameters,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function(xhr, status, errorThrown) {
                    Error = xhr;
                    eval(ErrorMethod + "()");
                },
                success: function(msg) {
                    Result = msg;
                    var rr = SuccessMethod + "('" + ControlID + "','" + ItemValue + "','" + ItemText + "');";
                    eval(rr);
                    //eval(SuccessMethod + "()");//without parameter
                }
            });
        }
        
        function BindDropdown(ControlID, ItemValue, ItemText) {

            var opt = '';
            opt += '<option value=0>' + " " + '</option>';
            if (Result.d.Table.length > 0) {
                for (var row in Result.d.Table) {

                    if (row >= 0) {
                        opt += '<option value="' + eval('Result.d.Table[row].' + ItemValue) + '">' + eval('Result.d.Table[row].' + ItemText) + '</option>';
                    }
                }
            }

            $(ControlID).html(opt);

        }
        function ShowError() {
            alert(Error.responseText);
        }
        
        //here to do
        function setCountryId() {
            document.getElementById("hidCUSTOMER_COUNTRY").value = document.getElementById("cmbCUSTOMER_COUNTRY").value;

        }

        function setStateID()
         {
             document.getElementById("hidSTATE_ID").value = document.getElementById("cmbCUSTOMER_STATE").value;
         }
         function ChkEmpResult(objSource, objArgs) {
             objArgs.IsValid = true;
             if (objArgs.IsValid == true) {
                 objArgs.IsValid = GetZipForState();
                 if (objArgs.IsValid == false)
                     document.getElementById('csvCUSTOMER_ZIP').innerHTML = "The zip code does not belong to the state";
             }
             return;
             if (GlobalError == true) {
                 Page_IsValid = false;
                 objArgs.IsValid = false;
             }
             else {
                 objArgs.IsValid = true;
             }
             document.getElementById("btnSave").click();
         }
         function GetZipForState() {
             GlobalError = true;
             if (document.getElementById('cmbCUSTOMER_STATE').value == 14 || document.getElementById('cmbCUSTOMER_STATE').value == 22 || document.getElementById('cmbCUSTOMER_STATE').value == 49) {
                 if (document.getElementById('txtCUSTOMER_ZIP').value != "") {
                     var intStateID = document.getElementById('cmbCUSTOMER_STATE').options[document.getElementById('cmbCUSTOMER_STATE').options.selectedIndex].value;
                     var strZipID = document.getElementById('txtCUSTOMER_ZIP').value;
                     var result = BillingInfo.AjaxFetchZipForState(intStateID, strZipID);
                     return AjaxCallFunction_CallBack(result);
                 }
                 return false;
             }
             else
                 return true;
         }
         function AjaxCallFunction_CallBack(response) {
             if (document.getElementById('cmbCUSTOMER_STATE').value == 14 || document.getElementById('cmbCUSTOMER_STATE').value == 22 || document.getElementById('cmbCUSTOMER_STATE').value == 49) {
                 if (document.getElementById('txtCUSTOMER_ZIP').value != "") {
                     handleResult(response);
                     if (GlobalError) {
                         return false;
                     }
                     else {
                         return true;
                     }
                 }
                 return false;
             }
             else
                 return true;
         }
         function handleResult(res) {
             if (!res.error) {
                 if (res.value != "" && res.value != null) {
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
			
         function ValidateDFIAcct(objSource, objArgs) {

             var boolval = ValidateDFIAcctNo(document.getElementById('txtDFI_ACC_NO'));
             if (boolval == false) {
                 objArgs.IsValid = false;
             }
         }
         function ValidateTranNo(objSource, objArgs) {
             var tranNum = document.getElementById('txtTRANSIT_ROUTING_NO').value;
             var firstDigit = tranNum.slice(0, 1);
             if (firstDigit == "5")
                 objArgs.IsValid = false;
         }
         function VerifyTranNo(objSource, objArgs) {
             var boolval = ValidateTransitNumber(document.getElementById('txtTRANSIT_ROUTING_NO'));

             if (boolval == false) {
                 objArgs.IsValid = false;
             }
         }
         function ValidateTranNoLength(objSource, objArgs) {
             var boolval = ValidateTransitNumberLen(document.getElementById('txtTRANSIT_ROUTING_NO'));
             if (boolval == false) {
                 objArgs.IsValid = false;
             }

         }

         function ValidateDFIAcct(objSource, objArgs) {

             var boolval = ValidateDFIAcctNo(document.getElementById('txtDFI_ACC_NO'));
             if (boolval == false) {
                 objArgs.IsValid = false;
             }
         }

         // Validate Credit Card Number Length

         //Added By Raghav For Itrack Issue #4998
         function ValCardNumLen(objSource, objArgs) {
             var cardLen = new String(document.getElementById('txtCARD_NO').value.trim());
             document.getElementById('hidCARD_TYPE').value = document.getElementById('cmbCARD_TYPE').value;
             if (cardLen.length != 15 && document.getElementById('hidCARD_TYPE').value == 14127) {
                 objArgs.IsValid = false;
                 document.getElementById('csvCARD_NO').innerHTML = "Please enter Credit Card # of 15 digits.";
             }
             else if (cardLen.length != 14 && document.getElementById('hidCARD_TYPE').value == 14128) {
                 objArgs.IsValid = false;
                 document.getElementById('csvCARD_NO').innerHTML = "Please enter Credit Card # of 14 digits.";
             }
             else if (cardLen.length != 16 && document.getElementById('hidCARD_TYPE').value == 14129) {
                 objArgs.IsValid = false;
                 document.getElementById('csvCARD_NO').innerHTML = "Please enter  Credit Card # of 16 digits.";
             }
             else if (cardLen.length != 15 && document.getElementById('hidCARD_TYPE').value == 14130) {
                 objArgs.IsValid = false;
                 document.getElementById('csvCARD_NO').innerHTML = "Please enter Credit Card # of 15 digits.";
             }
             else if (cardLen.length != 15 && cardLen.length != 16 && document.getElementById('hidCARD_TYPE').value == 14131) {
                 objArgs.IsValid = false;
                 document.getElementById('csvCARD_NO').innerHTML = "Please enter Credit Card # of 15 or 16 digits.";
             }

             else if (cardLen.length != 16 && document.getElementById('hidCARD_TYPE').value == 14124) {
                 objArgs.IsValid = false;
                 document.getElementById('csvCARD_NO').innerHTML = "Please enter Credit Card # of  16 digits.";
             }

             else if (cardLen.length != 13 && cardLen.length != 16 && document.getElementById('hidCARD_TYPE').value == 14125) {
                 objArgs.IsValid = false;
                 document.getElementById('csvCARD_NO').innerHTML = "Please enter Credit Card # of 13 or 16 digits.";
             }

             else {
                 objArgs.IsValid = true;
             }
         }
         // Validate Credit Card CVV Number Length	
         function ValCardCVVNumLen(objSource, objArgs) {
             var cardCVVLen = new String(document.getElementById('txtCARD_CVV_NUMBER').value.trim());
             if ((cardCVVLen.length < 3) || (cardCVVLen.length > 4))
                 objArgs.IsValid = false;
             else
                 objArgs.IsValid = true;
         }


         // Validate From Year Lenth : must be two digits for single digits also (eg : Instead of Jan -2, it shud be Jan -02)
         function chkFromYearLength(objSource, objArgs) {
             var FromYearDigitLen = new String(document.getElementById('txtCARD_DATE_VALID_FROM').value);
             if (FromYearDigitLen.length < 2) {
                 objArgs.IsValid = false;
             }
             else {
                 objArgs.IsValid = true;
             }
         }

         // Validate To Year Lenth : must be two digits for single digits also (eg : Instead of Jan -2, it shud be Jan -02)
         function chkToYearLength(objSource, objArgs) {
             var ToYearDigitLen = new String(document.getElementById('txtCARD_DATE_VALID_TO').value);
             if (ToYearDigitLen.length < 2) {
                 objArgs.IsValid = false;
             }
             else {
                 objArgs.IsValid = true;
             }
         }

         function ChkValidators() {
             DisableValidators();
             return true;
         }
         function DisableEnableValidatorsCC(status) {
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
					"rfvCARD_DATE_VALID_TO", "rngCARD_DATE_VALID_TO", "csvCARD_DATE_VALID_TO");

             for (i = 0; i < controlIDs.length; i++) {
                 if (document.getElementById(controlIDs[i]) != null)
                     document.getElementById(controlIDs[i]).setAttribute("enabled", status);
             }

         }
         function HideCreditCardPanel() {
             var payPalID = document.getElementById('hidCCFlag').value;
           
             if (payPalID == "1") {
                 document.getElementById('tblCC').style.display = 'none';
                 document.getElementById('tblbtnSavePayInfo').style.display = 'none';
             }
             var eftPanel = document.getElementById('hidEFTFlag').value;
             //For EFT and Credit Card
             //alert('CC ' + payPalID)
             //alert('EFT' + eftPanel);

             if (eftPanel == "1" && payPalID == "1") {
                 document.getElementById('tblbtnSavePayInfo').style.display = 'inline';
                 DisableEnableValidatorsCC(false);
             }

             if ((eftPanel == "0" && payPalID == "0") || document.getElementById('hidROWCOUNT').value == '') {
                 document.getElementById('tblbtnSavePayInfo').style.display = 'none';
                 DisableEnableValidatorsCC(false);
             }

             if (document.getElementById('lblCancelPayPal') != null)
                 document.getElementById('lblCancelPayPal').style.display = 'none';

         }
         function viewAllBoleto() {

             var POLICY_VERSION = document.getElementById('hidPOLICY_VERSION_ID').value;
             GenerateBoleto('0', 'ALL', POLICY_VERSION);
             return false;
         }
         function btnShowHideAllBoleto() {
            
             var Count = document.getElementById('hidROWCOUNT').value;
             if (Count != '' && parseInt(Count) != 0 && Count != 'undefined') {
                 if (document.getElementById('btnView_All_Boleto') != null)
                     document.getElementById('btnView_All_Boleto').style.display = 'inline';
                 if (document.getElementById('btnBoletoReprint') != null)
                     document.getElementById('btnBoletoReprint').style.display = 'inline';
             }
              else {
                 if (document.getElementById('btnView_All_Boleto') != null)
                     document.getElementById('btnView_All_Boleto').style.display = 'none';
                 if (document.getElementById('btnBoletoReprint') != null)
                     document.getElementById('btnBoletoReprint').style.display = 'none';
             }
          }
          function ReadOnlyFeesTaxes() {
              var Policy_Co_insurance = document.getElementById('hidCO_INSURANCE').value;
              if (document.getElementById('trINFOR').style.display == 'inline') {
                  if (Policy_Co_insurance == '<%=CO_INSURANCE_FOLLOWER %>') {
                      document.getElementById('txtTOTAL_TRAN_TAXES').readOnly = true;
                      document.getElementById('txtTOTAL_TRAN_FEES').readOnly = true;
                  } 
              }
              else {
                  if (Policy_Co_insurance == '<%=CO_INSURANCE_FOLLOWER %>') {
                      document.getElementById('txtTOTAL_FEES').readOnly = true;
                      document.getElementById('txtTOTAL_TAXES').readOnly = true;

                  } else {
                      document.getElementById('txtTOTAL_FEES').readOnly = false;
                      document.getElementById('txtTOTAL_TAXES').readOnly = false;
                  }
              }
          }
          function Init() {
             
            ApplyColor();
            ChangeColor();
            DisablePlanIDrfv();
            enableReadjust();
            hidAssignTotoal();
            EnableReadonly();
            //formatAmountByCulture();
            SetEncrytion();
            HideCreditCardPanel();
            btnShowHideAllBoleto();
            ReadOnlyFeesTaxes();
            showNBSDetails();
            SetReadonlyFieldStyle()
        }

        //Added By Lalit April 19 ,2011
        //i-track #593
        //set read only field css class and border 
        //Better way to so that user cannot edit it
        function SetReadonlyFieldStyle() {
        
//         var frm = document.POL_BILLING_INFO;
//         for (i = 0; i < frm.length; i++) {
//             e = frm.elements[i];
//             if (e.type == 'input' || e.type == 'text' && e.readOnly == true) {
//                 e.className = "inputcurrency midcolora";
//                 e.style.border = 'none';
//              }
//         }
            if (document.getElementById('txtTOTAL_PREMIUM').readOnly == true) {
                var Element1 = document.getElementById('txtTOTAL_PREMIUM');
                Element1.className  = "CustomCss";
                Element1.style.border = 'none';

            }  //  changes by praveer for itrack no 1761  
                var Element2 = document.getElementById('txtTOTAL_TAXES');
                var Element3 = document.getElementById('txtTOTAL_FEES');
                var Element4 = document.getElementById('txtTOTAL_INTEREST_AMOUNT');
                var Element5 = document.getElementById('txtTOTAL_AMOUNT');
                var Element6 = document.getElementById('txtTOTAL_TRAN_INTEREST_AMOUNT'); //changes by praveer for itrack no 1761  
                var Element7 = document.getElementById('txtTOTAL_TRAN_FEES');
                var Element8 = document.getElementById('txtTOTAL_TRAN_TAXES');
                var Element9 = document.getElementById('txtTOTAL_TRAN_AMOUNT');                

                document.getElementById('revTOTAL_PREMIUM').setAttribute("enabled", false);
                document.getElementById('revTOTAL_PREMIUM').style.display = 'none';

                Element2.className = Element3.className = Element4.className = Element5.className = Element6.className = Element7.className = Element8.className = Element9.className = "CustomCss";
                Element2.style.border = Element3.style.border = Element4.style.border = Element5.style.border = Element6.style.border = Element7.style.border = Element8.style.border = Element9.style.border = 'none';
            
         }
       
         
    </script>

    <script language="javascript" type="text/javascript">
//For J Query services

        $(document).ready(function() {

            $("#hlkZipLookup").click(function() {
                VerifyAddressDetailsForBR(document.getElementById('txtCUSTOMER_ADDRESS1'), document.getElementById('txtCUSTOMER_ADDRESS2'), document.getElementById('txtCUSTOMER_CITY'), document.getElementById('cmbCUSTOMER_STATE'), document.getElementById('txtCUSTOMER_ZIP'))
            });
            $("#txtCUSTOMER_ZIP").change(function() {
             
                if (trim($('#txtCUSTOMER_ZIP').val()) != '') {
                    var ZIPCODE = $("#txtCUSTOMER_ZIP").val();
                    var COUNTRYID = $("#cmbCUSTOMER_COUNTRY").val();
                    if (COUNTRYID == "5") {
                        PageMethod("GetCustomerAddressDetailsUsingZipeCode", ["ZIPCODE", ZIPCODE, "COUNTRYID", COUNTRYID], AjaxSucceeded, AjaxFailed); //With parameters
                    }
                }
                else {
                    $("#txtCUSTOMER_ADDRESS1").val('');
                    $("#txtCUSTOMER_ADDRESS2").val('');
                    $("#txtDISTRICT").val('');
                    $("#txtCUSTOMER_CITY").val('');
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
                    $("#cmbCUSTOMER_STATE").val(Addresse[1]);
                    $("#hidSTATE_ID").val(Addresse[1]);
                    $("#txtCUSTOMER_ZIP").val(Addresse[2]);
                    $("#txtCUSTOMER_ADDRESS1").val(Addresse[3] + ' ' + Addresse[4]);
                    //$("#txtCUSTOMER_ADDRESS2").val(Addresse[4]);
                    $("#txtCUSTOMER_ADDRESS2").val(Addresse[5]);
                    $("#txtCUSTOMER_CITY").val(Addresse[6]);
                }
                else {
                    alert($("#hidZipeCodeVerificationMsg").val());
                    $("#txtCUSTOMER_ZIP").val('');
                    $("#txtCUSTOMER_ADDRESS1").val('');
                    $("#txtCUSTOMER_ADDRESS2").val('');
                    $("#txtDISTRICT").val('');
                    $("#txtCUSTOMER_CITY").val('');
                }
            
            

        }

        function AjaxFailed(result) {

            alert(result.d);
        }
        function SetPREFIX() {
            if (document.getElementById('cmbPREFIX') != "") {
                document.getElementById('hidPREFIX').value = document.getElementById('cmbPREFIX').value;
            }
        }
        function setMAIN_TITLE() {
            if (document.getElementById('cmbMAIN_TITLE') != "") {
                document.getElementById('hidMAIN_TITLE').value = document.getElementById('cmbMAIN_TITLE').value;

            }
        }

        function showNBSDetails() {
            if (document.getElementById("hidPROCESS_ID").value == "3" ||
            document.getElementById("hidPROCESS_ID").value == "14" || 
            document.getElementById("hidPROCESS_ID").value == "12" ||
            document.getElementById("hidPROCESS_ID").value == "37" ||
             document.getElementById("hidPROCESS_ID").value == "35"
            )
                document.getElementById('trNBS_AMOUNT_DTL').style.display = 'inline';
                else
                    document.getElementById('trNBS_AMOUNT_DTL').style.display = 'none';
         }

//         $(document).ready(function() {

//            var gridView1Control = document.getElementById("#grdBILLING_INFO");
//             //format interest column
//             $('input[id$=txtINTEREST_AMOUNT]', gridView1Control).each(function(index, item) {            
//                 var InterestAmount = $("#" + item.id).val();
//                 InterestAmount = formatAmount(InterestAmount);
//                 $("#" + item.id).val(InterestAmount);
//             });
//             //format fees column
//             $('input[id$=txtFEE]', gridView1Control).each(function(index, item) {
//                 var InterestAmount = $("#" + item.id).val();
//                 InterestAmount = formatAmount(InterestAmount);
//                 $("#" + item.id).val(InterestAmount);
//             });
//             //format tax column
//             $('input[id$=txtTAXES]', gridView1Control).each(function(index, item) {
//                 var InterestAmount = $("#" + item.id).val();
//                 InterestAmount = formatAmount(InterestAmount);
//                 $("#" + item.id).val(InterestAmount);
//             });
//             //format total column
//             $('input[id$=txtTOTAL]', gridView1Control).each(function(index, item) {
//                 var InterestAmount = $("#" + item.id).val();
//                 InterestAmount = formatAmount(InterestAmount);
//                 $("#" + item.id).val(InterestAmount);
//             });
   //      });

         function UserIsManager() {
          //    document.getElementById("grdBILLING_INFO").rows[0].cells[0].style.display = "none";
             var mgr = <%= "'" + getIsUserSuperVisor() + "'" %>
             var gridId="grdBILLING_INFO_ctl";
             var hidROWCOUNT = document.getElementById('hidROWCOUNT').value;
             // changed by praveer panghal for itrack no 1761
            if(mgr=="Y" &&  document.getElementById("hidCO_INSURANCE").value!= "14549" ){
             if (hidROWCOUNT != '' && parseInt(hidROWCOUNT) != 0 && hidROWCOUNT != 'undefined' && hidROWCOUNT !=1) 
             {
                    for (i = 2; i < parseInt(hidROWCOUNT) + 2; i++) {
                   
                    var ctlId = i
                    if (i <10) ctlId = "0" + i ;
                    document.getElementById(gridId + ctlId + "_txtFEE").setAttribute("readOnly",false);
                    document.getElementById(gridId + ctlId + "_txtINTEREST_AMOUNT").readOnly=false;
                    document.getElementById(gridId + ctlId + "_txtTAXES").readOnly=false;
                     document.getElementById("txtTOTAL_TRAN_FEES").setAttribute("readOnly",true);
                    document.getElementById("txtTOTAL_TRAN_INTEREST_AMOUNT").readOnly=true;
                    document.getElementById("txtTOTAL_TRAN_TAXES").readOnly=true;
                     document.getElementById("txtTOTAL_FEES").setAttribute("readOnly",true);
                    document.getElementById("txtTOTAL_INTEREST_AMOUNT").readOnly=true;
                    document.getElementById("txtTOTAL_TAXES").readOnly=true;
                    
                   }
            }
            }

          else
             { 
             if (hidROWCOUNT != '' && parseInt(hidROWCOUNT) != 0 && hidROWCOUNT != 'undefined' && hidROWCOUNT !=1) 
             {
                    for (i = 2; i < parseInt(hidROWCOUNT) + 2; i++) {
                   
                    var ctlId = i
                    if (i <10) ctlId = "0" + i ;
                    document.getElementById(gridId + ctlId + "_txtFEE").setAttribute("readOnly",true);
                    document.getElementById(gridId + ctlId + "_txtINTEREST_AMOUNT").readOnly=true;
                    document.getElementById(gridId + ctlId + "_txtTAXES").readOnly=true;
                     document.getElementById("txtTOTAL_TRAN_FEES").setAttribute("readOnly",true);
                    document.getElementById("txtTOTAL_TRAN_INTEREST_AMOUNT").readOnly=true;
                    document.getElementById("txtTOTAL_TRAN_TAXES").readOnly=true;
                     document.getElementById("txtTOTAL_FEES").setAttribute("readOnly",true);
                    document.getElementById("txtTOTAL_INTEREST_AMOUNT").readOnly=true;
                    document.getElementById("txtTOTAL_TAXES").readOnly=true;
                    
                   }
            }
                       
           }
           
          }
    </script>

</head>
<body oncontextmenu="return true;" leftmargin="0" rightmargin="0" ms_positioning="GridLayout"
    onload="Init();UserIsManager();">
    <form id="POL_BILLING_INFO" runat="server" name="POL_BILLING_INFO" onsubmit="" method="post">
   <%-- <p>
        <uc1:addressverification id="AddressVerification1" runat="server">
        </uc1:addressverification></p>--%>
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <%--<tr>
            <td class="headereffectCenter" colspan="6">
                <asp:Label ID="lblHeader" runat="server"></asp:Label>
            </td>
        </tr>
        <tr id="trload" runat="server">
            <td class="midcolorc" colspan="6">
                <asp:Label ID="lblFormLoad" runat="server" CssClass="errmsg"></asp:Label>
            </td>
        </tr>--%>
        <tbody id="trdetails" runat="server">
        <tr>
            <td class="headereffectCenter" colspan="6">
                <asp:Label ID="lblHeader" runat="server"></asp:Label>
            </td>
        </tr> <!-- lblHeader -->
        <tr id="trload" runat="server">
            <td class="midcolorc" colspan="6">
                <asp:Label ID="lblFormLoad" runat="server" CssClass="errmsg"></asp:Label>
            </td>
        </tr> <!-- lblFormLoad -->
            <tr>
                <td class="midcolorc" colspan="6">
                    <asp:Label ID="lblFormLoadMessage" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
                </td>
            </tr> <!--lblFormLoadMessage -->
            <tr>
                <td class="pageHeader" align="left" colspan="6">
                    <%--<br />--%>
                    <asp:Label ID="lblManHeader" runat="server" colspan="2"></asp:Label>
                </td>
            </tr> <!-- lblManHeader -->
            <tr>
                <td class="midcolorc" colspan="6">
                    <asp:Label ID="lblMessage" runat="server" CssClass="errmsg"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="midcolorc" colspan="6">
                    <asp:Label ID="lblcustommsg" runat="server" CssClass="errmsg"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="6" class="midcolorc">
                </td>
            </tr>
            <tr>
                <td class="midcolorr" width="18%" colspan="6">
                    <cmsb:CmsButton runat="server" ID="btnGet_Premium" CssClass="clsButton" Text="Get Premium"
                        OnClick="btnGet_Premium_Click" />
                </td>
            </tr>
            <tr id="trNBS_AMOUNT_DTL">
                <td class="midcolora" width="18%">
                   <b> <asp:Label runat="server" ID="capNBS_PREMIUM">Policy Premium</asp:Label></b>
                    <br />
                   <span class="midcolorr"><asp:Label runat="server" ID="lblNBS_PREMIUM"  ></asp:Label></span>  
                </td>
                <td class="midcolora" width="18%">
                   <b>  <asp:Label runat="server" ID="capNBS_INTR">Policy Interest</asp:Label></b><br />
                    <asp:Label runat="server" ID="lblNBS_INTR"></asp:Label>
                </td>
                <td class="midcolora" width="18%">
                   <b>  <asp:Label runat="server" ID="capNBS_FEES">Policy Fees</asp:Label></b>
                    <br />
                   <asp:Label runat="server" ID="lblNBS_FEES"></asp:Label>
                </td>
                <td class="midcolora" width="18%">
                    <b> <asp:Label runat="server" ID="capNBS_TAX">Policy Taxes</asp:Label></b>
                    <br />
                   <asp:Label runat="server" ID="lblNBS_TAX"></asp:Label>
                </td>
                <td class="midcolora" width="18%">
                   <b>  <asp:Label runat="server" ID="capNBS_TOTAL">Total Amount</asp:Label></b>
                    <br />
                     <asp:Label runat="server" ID="lblNBS_TOTAL"></asp:Label>
                   </td>
                    <td class="midcolora" width="18%">
                  
                   </td>
            </tr>
            <tr>
                <td class="midcolora" colspan="6" style="height: 10px">
                </td>
            </tr>
            
            <tr>
                <td class="midcolora" width="18%">
                    <asp:Label runat="server" ID="capTOTAL_PREMIUM">Total Policy Premium</asp:Label>
                    <span class="mandatory">*</span>
                    <br />
                    <asp:TextBox runat="server"  MaxLength = "17" width="90%" CssClass="inputcurrency" ID="txtTOTAL_PREMIUM" onblur="CalcTotalAmount(this);this.value=formatAmount(this.value)"></asp:TextBox>
                    <input type="hidden" runat="server" id="hidTOTAL_PREMIUM" />
                    <br />
                    <asp:RequiredFieldValidator runat="server" ID="rfvTOTAL_PREMIUM" ErrorMessage=""
                        Display="Dynamic" ControlToValidate="txtTOTAL_PREMIUM"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="revTOTAL_PREMIUM" Display="Dynamic"
                        ErrorMessage="" ControlToValidate="txtTOTAL_PREMIUM"></asp:RegularExpressionValidator>
                    <%-- Added by Pradeep itrack no 1512/TFS#240--%>
                    <asp:CustomValidator ID="csvTOTAL_PREMIUM" runat="server" ControlToValidate="txtTOTAL_PREMIUM" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimitRange"></asp:CustomValidator>
                </td>
                <td class="midcolora" width="18%">
                    <asp:Label runat="server" ID="capTOTAL_INTEREST_AMOUNT">Total Policy Interest Amount</asp:Label><br />
                     <%-- changed by praveer itrack no 1512/TFS#240--%>
                    <asp:TextBox runat="server" CssClass="inputcurrency" 
                        ID="txtTOTAL_INTEREST_AMOUNT" MaxLength = "14" Width="100%" ></asp:TextBox>
                      <%--  onblur="CalcTotalAmount(this);this.value=formatAmount(this.value);--%>
                    <input type="hidden" runat="server" id="hidTOTAL_INTEREST_AMOUNT" />
                    <br />
                    <asp:RequiredFieldValidator runat="server" ID="rfvTOTAL_INTEREST_AMOUNT" Enabled="false"
                        ErrorMessage="" Display="Dynamic" ControlToValidate="txtTOTAL_INTEREST_AMOUNT"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="revTOTAL_INTEREST_AMOUNT" Display="Dynamic"
                        ErrorMessage="" ControlToValidate="txtTOTAL_INTEREST_AMOUNT" Enabled="False"></asp:RegularExpressionValidator>
                </td>
                <td class="midcolora" width="18%">
                    <asp:Label runat="server" ID="capTOTAL_FEES">Total Policy Fees</asp:Label>
                    <br />  <%-- changed by praveer itrack no 1512/TFS#240--%>
                    <asp:TextBox runat="server" CssClass="inputcurrency" MaxLength = "14" 
                        ID="txtTOTAL_FEES" width="100%"></asp:TextBox>
                        <%--onblur="CalcTotalAmount(this);this.value=formatAmount(this.value);"--%>
                    <input type="hidden" runat="server" id="hidTOTAL_FEES" />
                    <input type="hidden" runat="server" id="hidSTATE_FEES" />
                    <input type="hidden" runat="server" id="hidSTATE_TRAN_FEES" />
                    <br />
                    <asp:RequiredFieldValidator runat="server" ID="rfvTOTAL_FEES" ErrorMessage="" Enabled="false"
                        Display="Dynamic" ControlToValidate="txtTOTAL_FEES"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="revTOTAL_FEES" Display="Dynamic"
                        ErrorMessage="" ControlToValidate="txtTOTAL_FEES" Enabled="False"></asp:RegularExpressionValidator>
                    <asp:CustomValidator runat="server" ID="csvTOTAL_FEES" Display="Dynamic" ControlToValidate="txtTOTAL_FEES"
                        ErrorMessage="" ClientValidationFunction="StateFee" Enabled="False"></asp:CustomValidator>
                </td>
                <td class="midcolora" width="18%">
                    <asp:Label runat="server" ID="capTOTAL_TAXES">Total Policy Taxes</asp:Label>
                    <br />  <%-- changed by praveer itrack no 1512/TFS#240--%>
                    <asp:TextBox runat="server" MaxLength = "14" CssClass="inputcurrency" 
                        ID="txtTOTAL_TAXES" width="100%" ></asp:TextBox>
                        <%--onblur="CalcTotalAmount(this);this.value=formatAmount(this.value);"--%>
                    <input type="hidden" runat="server" id="hidTOTAL_TAXES" />
                    <br />
                    <asp:RequiredFieldValidator runat="server" ID="rfvTOTAL_TAXES" ErrorMessage="" Enabled="false"
                        Display="Dynamic" ControlToValidate="txtTOTAL_TAXES"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="revTOTAL_TAXES" Display="Dynamic"
                        ErrorMessage="" ControlToValidate="txtTOTAL_TAXES" Enabled="False"></asp:RegularExpressionValidator>
                </td>
                <td class="midcolora" width="18%">
                    <asp:Label runat="server" ID="capTOTAL_AMOUNT">Total Policy Amount</asp:Label>
                    <br />
                    <asp:TextBox runat="server" width="100%" CssClass="inputcurrency" ID="txtTOTAL_AMOUNT" ReadOnly="true"></asp:TextBox>
                    <input type="hidden" runat="server" id="hidTOTAL_AMOUNT" name="hidTOTAL_AMOUNT" />
                    <br />
                    <asp:RegularExpressionValidator runat="server" ID="revTOTAL_AMOUNT" Enabled="false"
                        Display="Dynamic" ErrorMessage="" ControlToValidate="txtTOTAL_AMOUNT"></asp:RegularExpressionValidator>
                </td>
                <td class="midcolorc" width="10%" style="vertical-align: middle;">
                    <cmsb:CmsButton runat="server" ID="btnGenrateInstallment" Width="170px" Text="Genrate Installment"
                        OnClick="btnGenrateInstallment_Click" CssClass="clsButton" />
                </td>
            </tr>
            
            <tr>
                <td class="midcolora" colspan="6" style="height: 10px">
                </td>
            </tr>
            
            
            
            
            <tr id="trTran" runat="server">
                <td class="midcolora">
                    <asp:Label runat="server" ID="capTOTAL_TRAN_PREMIUM">Transaction premium</asp:Label><br />
                    <asp:TextBox runat="server" MaxLength = "15" ID="txtTOTAL_TRAN_PREMIUM" CssClass="inputcurrency" 
                        onblur="addTranAmount(this);this.value=formatAmount(this.value);" width="90%" > </asp:TextBox>
                     <asp:RegularExpressionValidator runat="server" ID="revTOTAL_TRAN_PREMIUM" Display="Dynamic"
                        ErrorMessage="" ControlToValidate="txtTOTAL_TRAN_PREMIUM"></asp:RegularExpressionValidator>
                    <input type="hidden" runat="server" id="hidTOTAL_TRAN_PREMIUM" />
                </td>
                <td class="midcolora">
                    <asp:Label runat="server" ID="capTOTAL_TRAN_INTEREST_AMOUNT">Transaction Inerest Amount</asp:Label><br />
                    <asp:TextBox runat="server" MaxLength = "15" ID="txtTOTAL_TRAN_INTEREST_AMOUNT" CssClass="inputcurrency" ></asp:TextBox><br />
                      <%--  onblur="addTranAmount(this);this.value=formatAmount(this.value);"--%>
                       
                    <asp:RegularExpressionValidator runat="server" ID="revTOTAL_TRAN_INTEREST_AMOUNT" Display="Dynamic"
                        ErrorMessage="" ControlToValidate="txtTOTAL_TRAN_INTEREST_AMOUNT" Enabled="False" Visible="False"></asp:RegularExpressionValidator>
                </td>
                <td class="midcolora">
                    <asp:Label ID="capTOTAL_TRAN_FEES" runat="server">Transaction Fees</asp:Label><br />
                    <asp:TextBox runat="server" MaxLength = "15" ID="txtTOTAL_TRAN_FEES" 
                        CssClass="inputcurrency" ></asp:TextBox>
                       <%-- onblur="addTranAmount(this);this.value=formatAmount(this.value);"--%>
                  <br /> <asp:RegularExpressionValidator runat="server" ID="revTOTAL_TRAN_FEES" Display="Dynamic"
                        ErrorMessage="" ControlToValidate="txtTOTAL_TRAN_FEES" Enabled="False" Visible="False"></asp:RegularExpressionValidator> 
                </td>
                <td class="midcolora">
                    <asp:Label ID="capTOTAL_TRAN_TAXES" runat="server">Transaction Taxes</asp:Label><br />
                    <asp:TextBox runat="server" MaxLength = "15" ID="txtTOTAL_TRAN_TAXES" 
                        CssClass="inputcurrency" ></asp:TextBox>
                       <%-- onblur="addTranAmount(this);this.value=formatAmount(this.value);"--%>
                     <br /><asp:RegularExpressionValidator runat="server" ID="revTOTAL_TRAN_TAXES" Display="Dynamic"
                        ErrorMessage="" ControlToValidate="txtTOTAL_TRAN_TAXES" Enabled="False" Visible="False"></asp:RegularExpressionValidator> 
                </td>
                <td colspan="2" class="midcolora">
                    <asp:Label ID="capTOTAL_TRAN_AMOUNT" runat="server">Total Transaction Amount</asp:Label><br />
                    <asp:TextBox runat="server" ID="txtTOTAL_TRAN_AMOUNT" width="150px"   ReadOnly="true" CssClass="inputcurrency"></asp:TextBox>
                    <input type="hidden" runat="server" id="hidTOTAL_TRAN_AMOUNT" />
                </td>
            </tr>
            <%--Display none for distribution option,i-track #593--%>
            <tr id="trEND_PRM_DIST_OPTION" runat="server" style="display:none">
                <td class="midcolora" colspan="6" style="height: 10px">
                    <asp:Label ID="capPRM_DIST_TYPE" runat="server">Endorsment Premium Distribution Option</asp:Label><br />
                    <asp:DropDownList runat="server" ID="cmbPRM_DIST_TYPE">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trINFOR" runat="server">
                <td class="midcolora">
                <asp:Label ID="capTOTAL_CHANGE_INFORCE_PRM" runat="server" Style="font-weight: bold">Change Inforced Premium</asp:Label><br />
                    <asp:TextBox runat="server" ID="txtTOTAL_CHANGE_INFORCE_PRM" ReadOnly="true" CssClass="midcolora"
                        Style="border:none" onchange="addInfoAmt()" onFocus="blur()"></asp:TextBox>
                </td>
                <td colspan="5" class="midcolora">
                    <asp:Label ID="capTOTAL_INFO_PRM" runat="server" Style="font-weight: bold">Total Inforced premium</asp:Label><br />
                    <asp:TextBox runat="server" ID="txtTOTAL_INFO_PRM" ReadOnly="true" CssClass="midcolora"
                        Style="border:none" onchange="addInfoAmt()" onFocus="blur()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5" class="midcolorba">
                    <asp:Label runat="server" ID="capBILLING_PLAN">Billing Plan</asp:Label>
                    <br />
                    <asp:DropDownList runat="server" ID="cmbBILLING_PLAN" onchange="setBillingPlan()">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <cmsb:CmsButton runat="server" Text="Re Adjust" 
                        CssClass="clsButton" CausesValidation="false" ID="btnAdjust" OnClick="btnAdjust_Click" />
                    <br />
                    <asp:RequiredFieldValidator runat="server" ID="rfvBILLING_PLAN" ErrorMessage="" Display="Dynamic"
                        ControlToValidate="cmbBILLING_PLAN"></asp:RequiredFieldValidator>
                </td>
                <td class="midcolorba">
                </td>
            </tr>
            <tr>
                <td colspan="6" class="midcolorba" style="height: 5px;">
                </td>
            </tr>
            <tr>
                <td colspan="6" class="midcolorc">
                    <asp:GridView AutoGenerateColumns="false" runat="server" ID="grdBILLING_INFO" Width="100%"
                        OnRowDataBound="grdBILLING_INFO_RowDataBound" >
                        <HeaderStyle CssClass="headereffectWebGrid" HorizontalAlign="Center"></HeaderStyle>
                        <RowStyle CssClass="midcolora"></RowStyle>
                        <Columns>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate  >
                                    <asp:Label runat="server" ID="lblPOLICY_VERSION_ID" Text='<%# Eval("POLICY_VERSION_ID.CurrentValue") %>'></asp:Label>
                                    <%--<input type="hidden" id="hidinsdPOLICY_VERSION_ID" runat="server"  value='<%# Eval("POLICY_VERSION_ID.CurrentValue") %>'/>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="2%" CssClass="midcolorc" />
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capINSLAMENT_NO" Text="">Insalallement #</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblINSTALLMENT_NO" Text='<%# Eval("INSTALLMENT_NO.CurrentValue") %>'></asp:Label>
                                    <input type="hidden" runat="server" id="hidUPDATEFlag" name="hidUPDATEFlag" />
                                      <input type="hidden" id="hidinsdPOLICY_VERSION_ID" runat="server"  value='<%# Eval("POLICY_VERSION_ID.CurrentValue") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="2%" />
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capTRAN_TYPE" Text="">Tran Type</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <%-- <div id="divNBS">
                                        <asp:Label runat="server" ID="lblTRAN_TYPE_NBS" ReadOnly="true"></asp:Label></div>--%>
                                    <div id="divEND">
                                        <asp:Label runat="server" ID="lblTRAN_TYPE" ReadOnly="true"></asp:Label>
                                    </div>
                                    <%--<asp:TextBox runat="server" ID="txtTRAN_TYPE" Width="100%" ReadOnly="true" Text='<%# Eval("TRAN_TYPE") %>'></asp:TextBox>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="5%" />
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capINSTALLMENT_DATE" Text="">Installment Date</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtINSTALLMENT_EFFECTIVE_DATE" Width="100%" MaxLength="10" Text='<%# Eval("INSTALLMENT_EFFECTIVE_DATE.CurrentValue") %>'
                                        onblur="FormatDate();"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="" ID="rfvINSTALLMENT_DATE"
                                        ControlToValidate="txtINSTALLMENT_EFFECTIVE_DATE"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
                                        ID="revINSTALLMENT_DATE" ControlToValidate="txtINSTALLMENT_EFFECTIVE_DATE"></asp:RegularExpressionValidator>
                                    <asp:CompareValidator runat="server" Enabled="false" ID="cmpINSTALLMENT_EFF_DATE" ControlToValidate="txtINSTALLMENT_EFFECTIVE_DATE" Display="Dynamic"
                                    ValueToCompare="01/01/1950" ErrorMessage="" Type="Date" Operator="GreaterThanEqual">
                                    </asp:CompareValidator>
                                    
                                    <asp:CompareValidator runat="server" Enabled="false"  ID= "cmpINSTALLMENT_EXP_DATE" ControlToValidate="txtINSTALLMENT_EFFECTIVE_DATE" Display="Dynamic"
                                    ValueToCompare="01/01/1950" ErrorMessage="" Type="Date" Operator="LessThanEqual">
                                    </asp:CompareValidator>

                                 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="5%" />
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capPREMIUM" Text="">Policy Premium</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox runat="server" Width="100px" MaxLength = "15" CssClass="inputcurrency" ID="txtPREMIUM"
                                        onblur="InstallmentTotal(this);this.value=formatAmount(this.value);validateTRANPremium()"
                                        Text='<%# Convert.ToDecimal(Eval("INSTALLMENT_AMOUNT.CurrentValue")).ToString("N", numberFormatInfo) %>' ></asp:TextBox>
                                   <%-- <asp:TextBox runat="server" CssClass="inputcurrency" MaxLength="15" ID="txtTRAN_PREMIUM_AMOUNT"
                                        onblur="InstallmentTotal(this);this.value=formatAmount(this.value)" Text='<%# Convert.ToDecimal(Eval("TRAN_PREMIUM_AMOUNT.CurrentValue")).ToString("N", numberFormatInfo) %>'></asp:TextBox>--%>
                                    <br />
                                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="" ID="rfvPREMIUM"
                                        ControlToValidate="txtPREMIUM"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
                                        ID="revPREMIUM" ControlToValidate="txtPREMIUM"></asp:RegularExpressionValidator>
                                    <asp:CustomValidator runat="server" ID="csvPREMIUM" Display="Dynamic" Enabled="false"
                                        ClientValidationFunction="ValidateSum" ErrorMessage="Total can't greater tahn total premium Amount"
                                        ControlToValidate="txtPREMIUM"></asp:CustomValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="5%" />
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capINTEREST_AMOUNT" Text="">Policy Interest Amount</asp:Label>
                                </HeaderTemplate>
                                <%--javascript:this.value=formatAmount(this.value);--%>
                                <ItemTemplate>
                                    <asp:TextBox runat="server" Width="100px"   CssClass="inputcurrency" MaxLength="20" ID="txtINTEREST_AMOUNT"  Text='<%# Convert.ToDecimal(Eval("INTEREST_AMOUNT.CurrentValue")).ToString("N", numberFormatInfo) %>'  onblur="InstallmentTotal(this);this.value=formatAmount(this.value)" ></asp:TextBox>
                                    <%--<asp:TextBox runat="server" CssClass="inputcurrency" MaxLength="15" ID="txtTRAN_INTEREST_AMOUNT"
                                        onblur="InstallmentTotal(this);this.value=formatAmount(this.value)" Text='<%# Convert.ToDecimal(Eval("TRAN_INTEREST_AMOUNT.CurrentValue")).ToString("N", numberFormatInfo) %>'></asp:TextBox>
--%>                                    <br />
                                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="" ID="rfvINTEREST_AMOUNT"
                                        ControlToValidate="txtINTEREST_AMOUNT"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
                                        ID="revINTEREST_AMOUNT" ControlToValidate="txtINTEREST_AMOUNT"></asp:RegularExpressionValidator>
                                    <asp:CustomValidator runat="server" ID="csvINTEREST_AMOUNT" Enabled="false" ClientValidationFunction="ValidateSum"
                                        Display="Dynamic" ErrorMessage="Total can't greater tahn total Interest Amount"
                                        ControlToValidate="txtINTEREST_AMOUNT"></asp:CustomValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="5%" />
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capFEE" Text="">Policy Fee</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox runat="server" Width="100px" MaxLength = "15" CssClass="inputcurrency"  onblur="InstallmentTotal(this);this.value=formatAmount(this.value)"
                                        ID="txtFEE" Text='<%# Convert.ToDecimal(Eval("FEE.CurrentValue")).ToString("N", numberFormatInfo) %>'></asp:TextBox>
                                    <%--<asp:TextBox runat="server" CssClass="inputcurrency" MaxLength="15" ID="txtTRAN_FEE"
                                        onblur="InstallmentTotal(this);this.value=formatAmount(this.value)" Text='<%# Convert.ToDecimal(Eval("TRAN_FEE.CurrentValue")).ToString("N", numberFormatInfo) %>'></asp:TextBox>--%>
                                    <br />
                                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="" ID="rfvFEE"
                                        ControlToValidate="txtFEE"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
                                        ID="revFEE" ControlToValidate="txtFEE"></asp:RegularExpressionValidator>
                                    <asp:CustomValidator runat="server" ID="csvFEE" Enabled="false" Display="Dynamic"
                                        ClientValidationFunction="ValidateSum" ErrorMessage="Total can't greater tahn total fee"
                                        ControlToValidate="txtFEE"></asp:CustomValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="5%" />
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capTAXES" Text="">Taxes</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate><%--onblur="InstallmentTotal(this);this.value=formatAmount(this.value)"--%>
                                    <asp:TextBox runat="server" Width="" MaxLength = "15"  CssClass="inputcurrency"  ID="txtTAXES"
                                        Text='<%# Convert.ToDecimal(Eval("TAXES.CurrentValue")).ToString("N", numberFormatInfo) %>' onblur= "InstallmentTotal(this);this.value=formatAmount(this.value)" ></asp:TextBox>
                                   <%-- <asp:TextBox runat="server" CssClass="inputcurrency" MaxLength="15" ID="txtTRAN_TAXES"
                                        onblur="InstallmentTotal(this);this.value=formatAmount(this.value)" Text='<%# Convert.ToDecimal(Eval("TRAN_TAXES.CurrentValue")).ToString("N", numberFormatInfo) %>'></asp:TextBox>--%>
                                    <br />
                                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="" ID="rfvTAXES"
                                        ControlToValidate="txtTAXES"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
                                        ID="revTAXES" ControlToValidate="txtTAXES"></asp:RegularExpressionValidator>
                                    <asp:CustomValidator runat="server" ID="csvTAXES" Display="Dynamic" ClientValidationFunction="ValidateSum"
                                        Enabled="false" ErrorMessage="Total can't greater tahn total taxes Amount" ControlToValidate="txtTAXES"></asp:CustomValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="4%" />
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capTOTAL" Text="">Total</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox runat="server" CssClass="inputcurrency" ID="txtTOTAL" ReadOnly="true"
                                        Text='<%# Convert.ToDecimal(Eval("TOTAL.CurrentValue")).ToString("N", numberFormatInfo) %>'></asp:TextBox>
                                    <input type="hidden" runat="server" id="hidTOTAL" name="hidTOTAL" />
                                    <%--<asp:TextBox runat="server" CssClass="inputcurrency" ID="txtTRAN_TOTAL" ReadOnly="true"
                                        Text='<%# Convert.ToDecimal(Eval("TRAN_TOTAL.CurrentValue")).ToString("N", numberFormatInfo) %>'></asp:TextBox>--%>
                                    <input type="hidden" runat="server" id="hidTRAN_TOTAL" name="hidTRAN_TOTAL" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="2%" />
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capRELEASED_STATUS" Text="">Released Status</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate >
                                    <asp:Label runat="server" Text='<%# Eval("RELEASED_STATUS.CurrentValue") %>' ID="lblRELEASED_STATUS"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                             <asp:TemplateField>
                                <ItemStyle Width="3%" />
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capRECEIVED_DATE" Text="">Received  Date</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtRECEIVED_DATE" Width="80px" Text='<%# Eval("RECEIVED_DATE.CurrentValue") %>' ReadOnly="true"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                                <ItemStyle Width="5%" VerticalAlign="Middle" />
                                <HeaderTemplate>
                                    <asp:Label runat="server"  ID="capBOLETO" Text=""></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href="javascript:GenerateBoleto('<%# Eval("INSTALLMENT_NO.CurrentValue") %>','','<%# Eval("POLICY_VERSION_ID.CurrentValue") %>');">
                                        <asp:Label runat="server" ID="lblBOLETO"></asp:Label></a>
                                    <br />
                                    <%--<a href="javascript:GenerateBoleto('<%# Eval("INSTALLMENT_NO.CurrentValue") %>','Tran','<%# Eval("POLICY_VERSION_ID.CurrentValue") %>');">
                                        <asp:Label runat="server" ID="lblTRAN_BOLETO"></asp:Label></a>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="5" class="midcolorc" height="10px">
                </td>
                <td class="midcolorr" width="20%">
                   <%-- <br />--%>
                    <asp:Button ID="btnView_All_Boleto" runat="server" CssClass="clsButton"   />
                     </td>
                
            </tr>
            <tr>
                <td colspan="5" class="midcolorba">
                </td>
                <td class="midcolorr" width="30%">
                    <%--<br />--%>
                    <asp:Button ID="btnBoletoReprint" runat="server" CausesValidation="true" Text="Boleto Reprint" OnClientClick="javascript:return OpenBoletoReprint()" CssClass="clsButton"  /> 
                    <cmsb:CmsButton ID="btnSave" runat="server" CausesValidation="true" Text="Save" CssClass="clsButton"
                        OnClick="btnSave_Click"></cmsb:CmsButton>
                </td>
            </tr>
        </tbody>
    </table>
    <table width="100%">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlEFTCust" runat="server" Width="100%">
                                <table width="100%">
                                    <tr>
                                        <td class="headereffectSystemParams" colspan="4">
                                            <asp:Label runat="server" ID="lblCUSTOMER_EFT_INFO">CUSTOMER EFT INFO</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora" width="18%">
                                            <asp:Label ID="capFEDERAL_ID" runat="server">Federal ID</asp:Label>
                                        </td>
                                        <td class="midcolora" width="32%">
                                            <asp:Label ID="capENCRYP_FEDERAL_ID" runat="server" size="10" maxlength="9"></asp:Label>
                                            <input class="clsButton" id="btnENCRYP_FEDERAL_ID" text="Edit" type="button" onclick="FEDERAL_ID_change();"></input>
                                            <asp:TextBox ID="txtFEDERAL_ID" runat="server" Width="70px" SIZE="10" MaxLength="9"></asp:TextBox><br>
                                            <asp:RegularExpressionValidator ID="revFEDERAL_ID" runat="server" ControlToValidate="txtFEDERAL_ID"
                                                Display="Dynamic" ErrorMessage="Please Enter Valid Federal ID."></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="midcolora" width="18%">
                                            <asp:Label ID="capDFI_ACCT_NUMBER" runat="server"></asp:Label><span class="mandatory">*</span>
                                        </td>
                                        <td class="midcolora" width="32%">
                                            <asp:Label ID="capENCRYP_DFI_ACC_NO" runat="server"></asp:Label>
                                            <input class="clsButton" id="btnENCRYP_DFI_ACC_NO" text="Edit" type="button" onclick="DFI_ACC_NO_change();"></input>
                                            <asp:TextBox ID="txtDFI_ACC_NO" runat="server" MaxLength="17" size="23"></asp:TextBox><br>
                                            <asp:CustomValidator ID="csvDFI_ACC_NO" runat="server" ControlToValidate="txtDFI_ACC_NO"
                                                Display="Dynamic" ErrorMessage="No space allowed in between the numbers." ClientValidationFunction="ValidateDFIAcct"></asp:CustomValidator>
                                            <asp:RegularExpressionValidator ID="revDFI_ACC_NO" runat="server" ControlToValidate="txtDFI_ACC_NO"
                                                Display="Dynamic" ErrorMessage="DIIIIII"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="rfvDFI_ACC_NO" runat="server" ControlToValidate="txtDFI_ACC_NO"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora">
                                            <asp:Label ID="capTRAN_ROUT_NUMBER" runat="server"></asp:Label><span class="mandatory">*</span>
                                        </td>
                                        <td class="midcolora">
                                            <asp:Label ID="capENCRYP_TRANSIT_ROUTING_NO" runat="server"></asp:Label>
                                            <input class="clsButton" id="btnENCRYP_TRANSIT_ROUTING_NO" text="Edit" type="button"
                                                onclick="TRANSIT_ROUTING_NO_change();"></input>
                                            <asp:TextBox ID="txtTRANSIT_ROUTING_NO" runat="server" MaxLength="9" size="11"></asp:TextBox><br>
                                            <asp:RequiredFieldValidator ID="rfvTRANSIT_ROUTING_NO" runat="server" ControlToValidate="txtTRANSIT_ROUTING_NO"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="csvTRANSIT_ROUTING_NO" runat="server" ControlToValidate="txtTRANSIT_ROUTING_NO"
                                                Display="Dynamic" ErrorMessage="Number starting with 5 is invalid." ClientValidationFunction="ValidateTranNo"></asp:CustomValidator>
                                            <asp:CustomValidator ID="csvVERIFYFY_TRANSIT_ROUTING_NO" runat="server" ControlToValidate="txtTRANSIT_ROUTING_NO"
                                                Display="Dynamic" ErrorMessage="Please Verify the 9th digit(Check Digit)." ClientValidationFunction="VerifyTranNo"></asp:CustomValidator>
                                            <asp:CustomValidator ID="csvVERIFYFY_TRANSIT_ROUTING_NO_LENGHT" runat="server" ControlToValidate="txtTRANSIT_ROUTING_NO"
                                                Display="Dynamic" ErrorMessage="Length has to be exactly 8/9 digits." ClientValidationFunction="ValidateTranNoLength"></asp:CustomValidator>
                                            <asp:RegularExpressionValidator ID="revTRANSIT_ROUTING_NO" runat="server" ControlToValidate="txtTRANSIT_ROUTING_NO"
                                                Display="Dynamic" ErrorMessage="Please Enter Valid Transit Number."></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="midcolora">
                                            <asp:Label ID="capIS_VERIFIED" runat="server"></asp:Label>
                                        </td>
                                        <td class="midcolora">
                                            <asp:Label ID="lblIS_VERIFIED" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora">
                                            <asp:Label ID="capVERIFIED_DATE" runat="server"> Verified Date :</asp:Label>
                                        </td>
                                        <td class="midcolora">
                                            <asp:Label ID="lblVERIFIED_DATE" runat="server"></asp:Label>
                                        </td>
                                        <td class="midcolora">
                                            <asp:Label ID="capREASON" runat="server"> Reason :</asp:Label>
                                        </td>
                                        <td class="midcolora">
                                            N/A
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora" width="18%">
                                            <asp:Label ID="capREVERIFIED_AC" runat="server"></asp:Label>
                                        </td>
                                        <td class="midcolora" width="32%">
                                            <asp:CheckBox ID="chkREVERIFIED_AC" runat="server"></asp:CheckBox>
                                        </td>
                                        <td class="midcolora">
                                        </td>
                                        <td class="midcolora">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora">
                                            <asp:Label ID="capACCOUNT_TYPE" runat="server"> Account Type :</asp:Label>
                                        </td>
                                        <td class="midcolora">
                                            <asp:RadioButton ID="rdbACC_CASH_ACC_TYPEO" runat="server" Text="Checking" GroupName="ACC_CASH_ACC_TYPE"
                                                Checked="True"></asp:RadioButton>
                                            <asp:RadioButton ID="rdbACC_CASH_ACC_TYPET" runat="server" Text="Saving" GroupName="ACC_CASH_ACC_TYPE">
                                            </asp:RadioButton>
                                        </td>
                                        <td class="midcolora">
                                            <asp:Label ID="capEFT_TENTATIVE_DATE" runat="server">EFT Tentative Date:</asp:Label><span
                                                class="mandatory">*</span>
                                        </td>
                                        <td class="midcolora">
                                            <asp:TextBox ID="txtEFT_TENTATIVE_DATE" runat="server" MaxLength="2" size="4"></asp:TextBox><br>
                                            <asp:RequiredFieldValidator ID="rfvEFT_TENTATIVE_DATE" runat="server" ControlToValidate="txtEFT_TENTATIVE_DATE"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="rngEFT_TENTATIVE_DATE" runat="server" ControlToValidate="txtEFT_TENTATIVE_DATE"
                                                Display="Dynamic" ErrorMessage="Date must be between 1 - 31." Type="Integer"
                                                MinimumValue="1" MaximumValue="31"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <table width="100%">
                            </asp:Panel>
                            <asp:Panel ID="pnlCCMessage" runat="server">
                                <table id='tblShowCC' width="100%">
                                    <tr>
                                        <td class="midcolora" width="80%">
                                            <asp:Label ID="lblPayPalMsg" runat="server" CssClass="errmsg">Credit Card data for this Policy already provided. Click <A href="javascript:showCCPanelJS();" onclick="showCCPanelJS();"><%=strhere%></A> <%=modify%> </asp:Label>
                                        </td>
                                        <td class="midcolora" width="20%">
                                            <asp:Label ID="lblCancelPayPal" runat="server" onclick="HideCCPanelJS();"><A href="javascript:HideCCPanelJS();">Cancel</A></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlCCCust" runat="server">
                                <table id='tblCC'>
                                    <tr>
                                        <td class="headereffectSystemParams" colspan="4">
                                            <asp:Label runat="server" ID="lblCCCARD_INFO">Customer Credit Card Info</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora">
                                            <asp:Label ID="lblCLT_ADDRESS" runat="server">Would you like to pull customer address</asp:Label>
                                        </td>
                                        <td class="midcolora" colspan="3">
                                            <cmsb:CmsButton class="clsButton" ID="btnPULL_CUSTOMER_ADDRESS" runat="server" Text="">
                                            </cmsb:CmsButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tr>
                                                    <td class="midcolora" width="17%">
                                                        <asp:Label ID="capCUSTOMER_FIRST_NAME" runat="server"></asp:Label><span class="mandatory">*</span>
                                                    </td>
                                                    <td class="midcolora" width="18%">
                                                        <asp:TextBox ID="txtCUSTOMER_FIRST_NAME" runat="server" size="25" MaxLength="75"></asp:TextBox><br>
                                                        <asp:RequiredFieldValidator ID="rfvCUSTOMER_FIRST_NAME" runat="server" ControlToValidate="txtCUSTOMER_FIRST_NAME"
                                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revCUSTOMER_FIRST_NAME" runat="server" ControlToValidate="txtCUSTOMER_FIRST_NAME"
                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td class="midcolora" width="9%">
                                                        <asp:Label ID="capCUSTOMER_MIDDLE_NAME" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="midcolora" width="16%">
                                                        <asp:TextBox ID="txtCUSTOMER_MIDDLE_NAME" runat="server" size="12" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td class="midcolora" width="8%">
                                                        <asp:Label ID="capCUSTOMER_LAST_NAME" runat="server"></asp:Label><span class="mandatory"
                                                            id="spnMandatory">*</span>
                                                    </td>
                                                    <td class="midcolora" width="28%">
                                                        <asp:TextBox ID="txtCUSTOMER_LAST_NAME" runat="server" size="25" MaxLength="25"></asp:TextBox><br>
                                                        <asp:RequiredFieldValidator ID="rfvCUSTOMER_LAST_NAME" runat="server" ControlToValidate="txtCUSTOMER_LAST_NAME"
                                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora" width="18%">
                                            <asp:Label ID="capCUSTOMER_ADDRESS1" runat="server"></asp:Label>
                                        </td>
                                        <td class="midcolora" width="32%">
                                            <asp:TextBox ID="txtCUSTOMER_ADDRESS1" runat="server" size="35" MaxLength="150"></asp:TextBox><br>
                                        </td>
                                        <td class="midcolora" width="18%">
                                            <asp:Label ID="capCUSTOMER_ADDRESS2" runat="server">District</asp:Label>
                                        </td>
                                        <td class="midcolora" width="32%">
                                            <asp:TextBox ID="txtCUSTOMER_ADDRESS2" runat="server" size="35" MaxLength="150"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora" width="18%">
                                            <asp:Label ID="capCUSTOMER_CITY" runat="server"></asp:Label>
                                        </td>
                                        <td class="midcolora" width="32%">
                                            <asp:TextBox ID="txtCUSTOMER_CITY" runat="server" size="35" MaxLength="35"></asp:TextBox><br>
                                        </td>
                                        <td class="midcolora" width="18%">
                                            <asp:Label ID="capCUSTOMER_COUNTRY" runat="server"></asp:Label>
                                        </td>
                                        <td class="midcolora" width="32%">
                                            <asp:DropDownList ID="cmbCUSTOMER_COUNTRY" runat="server" onfocus="SelectComboIndex('cmbCUSTOMER_COUNTRY');"
                                                onchange="BindCountryState();setCountryId()">
                                            </asp:DropDownList>
                                            <%--   <asp:DropDownList ID="cmbCUSTOMER_COUNTRY" onfocus="SelectComboIndex('cmbCUSTOMER_COUNTRY');"
                                                runat="server" SuccessMethod="outputDTSUBLOB"  TargetControl="cmbCUSTOMER_STATE" ErrorMethod="ShowError" onchange="setCountryId()"
                                            ItemValue="STATE_ID" ItemText="STATE_NAME" class="FillDD" ServerMethod="BillingInfo.aspx/FillState">
                                            </asp:DropDownList>--%>
                                            <br>
                                            <%--Called fillstateFromCountry() by Sibin on 25 Nov 08--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora" width="18%">
                                            <asp:Label ID="capCUSTOMER_STATE" runat="server"></asp:Label>
                                        </td>
                                        <td class="midcolora" width="32%">
                                            <asp:DropDownList ID="cmbCUSTOMER_STATE" onfocus="SelectComboIndex('cmbCUSTOMER_STATE');"
                                                runat="server" onchange="setStateID()">
                                            </asp:DropDownList>
                                            <br>
                                            <%--Called setStateID() by Sibin on 25 Nov 08--%>
                                        </td>
                                        <td class="midcolora" width="18%">
                                            <asp:Label ID="capCUSTOMER_ZIP" runat="server"></asp:Label><span class="mandatory"
                                                id="spnCUSTOMER_ZIP" runat="server"></span>
                                        </td>
                                        <td class="midcolora" width="32%">
                                            <asp:TextBox ID="txtCUSTOMER_ZIP" runat="server" size="13" MaxLength="10" OnBlur="GetZipForState();"></asp:TextBox><%--Called DisableZipForCanada() by Sibin on 25 Nov 08--%><%--<A href="#"><asp:image id="imgZipLookup" runat="server" ImageAlign="Bottom" ImageUrl="/cms/cmsweb/images/info.gif"></asp:image></A>--%>
                                            <asp:HyperLink ID="hlkZipLookup" runat="server" CssClass="HotSpot">
                                                <asp:Image ID="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif"
                                                    ImageAlign="Bottom"></asp:Image>
                                            </asp:HyperLink><br>
                                            <asp:CustomValidator ID="csvCUSTOMER_ZIP" runat="server" ClientValidationFunction="ChkEmpResult"
                                                ErrorMessage=" " Display="Dynamic" ControlToValidate="txtCUSTOMER_ZIP"></asp:CustomValidator>
                                            <asp:RegularExpressionValidator ID="revCUSTOMER_ZIP" runat="server" ControlToValidate="txtCUSTOMER_ZIP"
                                                Display="Dynamic"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora" width="18%">
                                            <asp:Label ID="capCARD_TYPE" runat="server">Card Type </asp:Label><span class="mandatory">*</span>
                                        </td>
                                        <td class="midcolora" width="32%">
                                            <asp:DropDownList ID="cmbCARD_TYPE" onfocus="SelectComboIndex('cmbCARD_TYPE')" runat="server">
                                            </asp:DropDownList>
                                            <br>
                                            <asp:RequiredFieldValidator ID="rfvCARD_TYPE" runat="server" ControlToValidate="cmbCARD_TYPE"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="midcolora" width="18%">
                                            <asp:Label ID="capCARD_NO" runat="server">Credit Card #  :</asp:Label><span class="mandatory">*</span>
                                        </td>
                                        <td class="midcolora" width="32%">
                                            <asp:TextBox ID="txtCARD_NO" runat="server" MaxLength="16" size="21"></asp:TextBox><br>
                                            <asp:RegularExpressionValidator ID="revCARD_NO" runat="server" ControlToValidate="txtCARD_NO"
                                                Display="Dynamic"></asp:RegularExpressionValidator><br>
                                            <asp:CustomValidator ID="csvCARD_NO" runat="server" ControlToValidate="txtCARD_NO"
                                                Display="Dynamic" ClientValidationFunction="ValCardNumLen"></asp:CustomValidator>
                                            <asp:RequiredFieldValidator ID="rfvCARD_NO" runat="server" ControlToValidate="txtCARD_NO"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora" width="18%">
                                            <asp:Label ID="capCARD_CVV_NUMBER" runat="server">Card CVV/CCV</asp:Label><span class="mandatory">*</span>
                                        </td>
                                        <td class="midcolora" width="32%">
                                            <asp:TextBox ID="txtCARD_CVV_NUMBER" runat="server" MaxLength="4" size="5"></asp:TextBox><br>
                                            <asp:RegularExpressionValidator ID="revCARD_CVV_NUMBER" runat="server" ControlToValidate="txtCARD_CVV_NUMBER"
                                                Display="Dynamic"></asp:RegularExpressionValidator>
                                            <asp:CustomValidator ID="csvCARD_CVV_NUMBER" runat="server" ControlToValidate="txtCARD_CVV_NUMBER"
                                                Display="Dynamic" ClientValidationFunction="ValCardCVVNumLen"></asp:CustomValidator>
                                            <asp:RequiredFieldValidator ID="rfvCARD_CVV_NUMBER" runat="server" ControlToValidate="txtCARD_CVV_NUMBER"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="midcolora" width="18%">
                                            <asp:Label ID="capCARD_DATE_VALID_TO" runat="server">Valid Till(Month/Year)</asp:Label><span
                                                class="mandatory">*</span>
                                        </td>
                                        <td class="midcolora" width="32%">
                                            <asp:DropDownList ID="cmbCARD_DATE_VALID_TO" runat="server">
                                                <asp:ListItem Value="01">January</asp:ListItem>
                                                <asp:ListItem Value="02">February</asp:ListItem>
                                                <asp:ListItem Value="03">March</asp:ListItem>
                                                <asp:ListItem Value="04">April</asp:ListItem>
                                                <asp:ListItem Value="05">May</asp:ListItem>
                                                <asp:ListItem Value="06">June</asp:ListItem>
                                                <asp:ListItem Value="07">July</asp:ListItem>
                                                <asp:ListItem Value="08">August</asp:ListItem>
                                                <asp:ListItem Value="09">September</asp:ListItem>
                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">December</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtCARD_DATE_VALID_TO" runat="server" MaxLength="2" size="3" Text="11"></asp:TextBox><br>
                                            <asp:RequiredFieldValidator ID="rfvCARD_DATE_VALID_TO" runat="server" ControlToValidate="txtCARD_DATE_VALID_TO"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="rngCARD_DATE_VALID_TO" runat="server" MaximumValue="99" MinimumValue="00"
                                                ControlToValidate="txtCARD_DATE_VALID_TO" Display="Dynamic" Type="Integer"></asp:RangeValidator>
                                            <asp:CompareValidator ID="cmpCARD_DATE_VALID_TO" runat="server" ControlToValidate="txtCARD_DATE_VALID_TO"
                                                Display="Dynamic" Operator="GreaterThanEqual"></asp:CompareValidator>
                                            <asp:CustomValidator ID="csvCARD_DATE_VALID_TO" runat="server" ControlToValidate="txtCARD_DATE_VALID_TO"
                                                Display="Dynamic" ClientValidationFunction="chkToYearLength"></asp:CustomValidator>
                                                
                                             
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <table id='tblbtnSavePayInfo' width="100%">
                                <tr>
                                    <td class="midcolorr" colspan="4">
                                        <cmsb:CmsButton class="clsButton" ID="btnSAVE_PAYMENT_INFO" runat="server" Text="Save"
                                            OnClick="btnSAVE_PAYMENT_INFO_Click" style="display:none;"></cmsb:CmsButton>
                                         
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <input type="hidden" runat="server" id="hidROWCOUNT" name="hidROWCOUNT" />
                <input type="hidden" runat="server" id="hidCUSTOMER_ID" name="hidCUSTOMER_ID" />
                <input type="hidden" runat="server" id="hidPOLICY_ID" name="hidPOLICY_ID" />
                <input type="hidden" runat="server" id="hidPOLICY_VERSION_ID" name="hidPOLICY_VERSION_ID" />
                <input type="hidden" runat="server" id="hidforSavedValues" name="hidforSavedValues" />
                <input type="hidden" runat="server" id="hidUPDATEDROWS" name="hidUPDATEDROWS" />
                <input type="hidden" runat="server" id="hidSPLID" name="hidSPLID" />
                <input type="hidden" runat="server" id="hidArrObj" name="hidArrObj" />
                <input type="hidden" runat="server" id="hidSELECTED_PLAN_ID" name="hidSELECTED_PLAN_ID" />
                <input type="hidden" runat="server" id="hidPOL_TERMS" name="hidPOL_TERMS" />
                <input type="hidden" runat="server" id="hidREL_INSTLL_NO" name="hidREL_INSTLL_NO" />
                <input type="hidden" runat="server" id="hidPOL_COV_PRE" name="hidPOL_COV_PRE" />
                <input type="hidden" runat="server" id="hidPOLICY_STATUS" name="hidPOLICY_STATUS" />
                <input type="hidden" runat="server" id="hidPOLICY_CURRENCY" name="hidPOLICY_CURRENCY" />
                <input type="hidden" runat="server" id="hidPRINTPARAMETER_FROM" name="hidPRINTPARAMETER_FROM" />
                <%--NBS Amount Details--%>
                <input type="hidden" runat="server" id="hidNBS_PREMIUM" name="hidNBS_PREMIUM" />
                <input type="hidden" runat="server" id="hidNBS_FEES" name="hidNBS_FEES" />
                <input type="hidden" runat="server" id="hidNBS_TAXES" name="hidNBS_TAXES" />
                <input type="hidden" runat="server" id="hidNBS_INTEREST" name="hidNBS_INTEREST" />
                <input type="hidden" runat="server" id="hidINSTALL_PREMIUM" name="hidINSTALL_PREMIUM" />
                <input type="hidden" runat="server" id="hidPRORATED_PREMIUM" name="hidPRORATED_PREMIUM" />
                <input type="hidden" runat="server" id="hidPOLICY_BILL_TYPE" name="hidPOLICY_BILL_TYPE" />
                <input type="hidden" runat="server" id="hidPOL_EFF_DATE" name="hidPOL_EFF_DATE" />
                <input type="hidden" runat="server" id="hidEXPIRY_DATE" name="hidEXPIRY_DATE" />
                <input type="hidden" runat="server" id="hidInstallGen" name="hidInstallGen" />
                <input type="hidden" runat="server" id="hidNBS_INFO_PRM" name="hidNBS_INFO_PRM" />
                <input type="hidden" runat="server" id="hidCALLED_FROM" name="hidCALLED_FROM" />
                <input type="hidden" runat="server" id="hidPROCESS_ID" name="hidPROCESS_ID" />
                <input type="hidden" runat="server" id="hid_ENCRYP_FEDERAL_ID" name="hid_ENCRYP_FEDERAL_ID"
                    value="0" />
                <input type="hidden" runat="server" id="hid_ENCRYP_DFI_ACC_NO" name="hid_ENCRYP_DFI_ACC_NO"
                    value="0" />
                <input type="hidden" runat="server" id="hid_ENCRYP_TRANSIT_ROUTING_NO" name="hid_ENCRYP_TRANSIT_ROUTING_NO"
                    value="0" />
                <input type="hidden" runat="server" id="hidOldData" name="hidOldData" />
                <input type="hidden" runat="server" id="hidDAY" name="hidDAY" value="0" />
                <input type="hidden" runat="server" id="hidCCFlag" name="hidCCFlag" />
                <input type="hidden" runat="server" id="hidEFTFlag" name="hidEFTFlag" />
                <input type="hidden" runat="server" id="hidBILL_TYPE" name="hidBILL_TYPE" />
                <input type="hidden" runat="server" id="hidCUSTOMER_INFO" name="hidCUSTOMER_INFO" />
                <input type="hidden" runat="server" id="hidSTATE_ID" name="hidSTATE_ID" />
                <input type="hidden" runat="server" id="hidCUSTOMER_COUNTRY" name="hidCUSTOMER_COUNTRY" />
                <input type="hidden" runat="server" id="hidCARD_TYPE" name="hidCARD_TYPE" />
                <input type="hidden" runat="server" id="hidPOL_EFFECTIVE_DATE" name="hidCUSTOMER_COUNTRY" />
                <input type="hidden" runat="server" id="hidPOL_EXPIRY_DATE" name="hidCARD_TYPE" />
                <input type="hidden" runat="server" id="hidCO_INSURANCE" name="hidCO_INSURANCE"  />
                <input type="hidden" runat="server" id="hidTOTAL_CHANGE_INFORCE_PRM" name="hidTOTAL_CHANGE_INFORCE_PRM" value= "0" />
                <input type="hidden" runat="server" id="hidTOTAL_INFO_PRM" name="hidTOTAL_INFO_PRM" value= "0" />
                <input type="hidden" runat="server" id="hidDATE_MSG" name="hidDATE_MSG" />
                <input type="hidden" runat="server" id="hidPREV_VERSION_ID" name="hidPREV_VERSION_ID" value="0" />
               <input type="hidden" runat="server" id="hid_IOF_PERCENTAGE" name="hid_IOF_PERCENTAGE" value="0" />  
                <%--End Here--%>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

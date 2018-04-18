<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BillingDetails.aspx.cs" Inherits="Cms.Policies.Aspx.BillingDetails" %>
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
        .CustomCssLeftAligned
        {
        	PADDING-LEFT: 0px;
        	FONT-WEIGHT: normal; 
        	FONT-SIZE:8pt;
        	vertical-align:TOP; 
        	COLOR: #000000; 
        	FONT-FAMILY: Verdana, 'MS Sans Serif', Arial; 
        	BACKGROUND-COLOR: #f3f3f3; 
        	TEXT-ALIGN:left
        }
        .style1
        {
            width: 9%;
        }
        .style2
        {
            width: 15%;
        }
    </style>

    <script src="/cms/cmsweb/scripts/xmldom.js"></script>

    <script src="/cms/cmsweb/scripts/common.js"></script>

    <script src="/cms/cmsweb/scripts/form.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>

    <script type="text/javascript" language="javascript">
        function Init() {
  
            ApplyColor();
            ChangeColor();
            //DisablePlanIDrfv();
            //enableReadjust();
//            hidAssignTotoal();
            EnableReadonly();
            //formatAmountByCulture();
//            SetEncrytion();
//            HideCreditCardPanel();
//            btnShowHideAllBoleto();
//            ReadOnlyFeesTaxes();
//            showNBSDetails();
//            SetReadonlyFieldStyle()


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
        function UserIsManager() {
       
          //    document.getElementById("grdBILLING_INFO").rows[0].cells[0].style.display = "none";
             var mgr = <%= "'" + getIsUserSuperVisor() + "'" %>
             var gridId="grdBILLING_DETAILS";
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
                     document.getElementById("txtTotal_After_GST").setAttribute("readOnly",true);
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
                     document.getElementById("txtTotal_After_GST").setAttribute("readOnly",true);
                    document.getElementById("txtTOTAL_INTEREST_AMOUNT").readOnly=true;
                    document.getElementById("txtTOTAL_TAXES").readOnly=true;
                    
                   }
            }
                       
           }
           
          }
          function setBillingPlan() {
            if (document.getElementById('cmbBILLING_PLAN').value != "") {
                if (document.getElementById('hidPOLICY_STATUS').value != 'UENDRS' && document.getElementById('hidPROCESS_ID').value != '3')
                    document.getElementById('hidSELECTED_PLAN_ID').value = document.getElementById('cmbBILLING_PLAN').value;
             }
         }
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
          function FormatAmountForSum(num) {
            num = ReplaceAll(num, sGroupSep, '');
            num = ReplaceAll(num, sDecimalSep, '.');
            return num;
        }

        /*Field Mapping with existing billing info to new one for Singapore
        txtTOTAL_PREMIUM		    hidTOTAL_PREMIUM    (For Gross Premium)
        txtTOTAL_INTEREST_AMOUNT	hidTOTAL_INTEREST_AMOUNT (For Other Charges)
        txtTotal_Before_GST		    hidTotal_Before_GST 
        txtTOTAL_TAXES			    hidTOTAL_TAXES		(For GST)			
        txtTotal_After_GST          hidTotal_After_GST
        txtTOTAL_FEES			    	
        txtGross_Commission		    hidGross_Commission
        txtGST_on_Commission		hidGST_on_Commission					
        txtTot_Comm_After_GST		hidTot_Comm_After_GST
        txtTOTAL_AMOUNT			    hidTOTAL_AMOUNT		(For Net Amount)		
        */
function CalcTotalAmount(obj) {

     
            var txtGrossPremium = document.getElementById("txtTOTAL_PREMIUM").value == '' ? document.getElementById("txtTOTAL_PREMIUM").value = '0' : document.getElementById("txtTOTAL_PREMIUM").value;
            document.getElementById("hidTOTAL_PREMIUM").value = txtGrossPremium;
            txtGrossPremium = FormatAmountForSum(txtGrossPremium);
            if (isNaN(parseFloat(txtGrossPremium)))
                txtGrossPremium = 0

            var txtOtherCharges = document.getElementById("txtTOTAL_INTEREST_AMOUNT").value == '' ? document.getElementById("txtTOTAL_INTEREST_AMOUNT").value = '0' : document.getElementById("txtTOTAL_INTEREST_AMOUNT").value;
            document.getElementById("hidTOTAL_INTEREST_AMOUNT").value = txtOtherCharges;
            txtOtherCharges = FormatAmountForSum(txtOtherCharges);
            if (isNaN(parseFloat(txtOtherCharges)))
                txtOtherCharges = 0

                    //Added By  kuldeep for TFS 3408 Billing Info
             var txtTotal_Before_GST =  parseFloat(txtGrossPremium)+  parseFloat(txtOtherCharges);

               //For Demo Purpose GST is Fixed at 7%
               var txtGST=parseFloat(txtTotal_Before_GST) * 7/100;
                
//                var txtGST = document.getElementById("txtTOTAL_TAXES").value == '' ? document.getElementById("txtTOTAL_TAXES").value = '0' : document.getElementById("txtTOTAL_TAXES").value;
//            document.getElementById("hidTOTAL_TAXES").value = txtGST;
//            txtGST = FormatAmountForSum(txtGST);
//            if (isNaN(parseFloat(txtGST)))
//                txtGST = 0
                var txtTotalAfterGST=parseFloat(txtTotal_Before_GST) + parseFloat(txtGST);
                            
//                var txtTotalAfterGST = document.getElementById("txtTOTAL_FEES").value == '' ? document.getElementById("txtTOTAL_FEES").value = '0' : document.getElementById("txtTOTAL_FEES").value;
//            document.getElementById("hidTOTAL_FEES").value = txtTotalAfterGST;
//            txtTotalAfterGST = FormatAmountForSum(txtTotalAfterGST);
//            if (isNaN(parseFloat(txtTotalAfterGST)))
//                txtTotalAfterGST = 0
          
           if(document.getElementById("hidComm_percentage").value!="0")
           {
           var comm_percentage=parseFloat(document.getElementById("hidComm_percentage").value);
           var comm_amt= txtTotal_Before_GST * comm_percentage/100;
                document.getElementById("txtGross_Commission").value=comm_amt.toString();
           }
                var txtGross_Commission = document.getElementById("txtGross_Commission").value == '' ? document.getElementById("txtGross_Commission").value = '0' : document.getElementById("txtGross_Commission").value;
            document.getElementById("hidGross_Commission").value = txtGross_Commission;
            txtGross_Commission = FormatAmountForSum(txtGross_Commission);
            if (isNaN(parseFloat(txtGross_Commission)))
                txtGross_Commission = 0

               var txtGST_on_Commission= parseFloat(txtGross_Commission) * 7/100;
//                    var txtGST_on_Commission = document.getElementById("txtGST_on_Commission").value == '' ? document.getElementById("txtGST_on_Commission").value = '0' : document.getElementById("txtGST_on_Commission").value;
//            document.getElementById("hidGST_on_Commission").value = txtTotalTAXES;
//            txtGST_on_Commission = FormatAmountForSum(txtGST_on_Commission);
//            if (isNaN(parseFloat(txtGST_on_Commission)))
//                txtGST_on_Commission = 0

                 var txtTot_Comm_After_GST=parseFloat(txtGross_Commission) + parseFloat(txtGST_on_Commission);

//                    var txtTot_Comm_After_GST = document.getElementById("txtTot_Comm_After_GST").value == '' ? document.getElementById("txtTot_Comm_After_GST").value = '0' : document.getElementById("txtTot_Comm_After_GST").value;
//            document.getElementById("hidTot_Comm_After_GST").value = txtTotalTAXES;
//            txtTot_Comm_After_GST = FormatAmountForSum(txtTot_Comm_After_GST);
//            if (isNaN(parseFloat(txtTot_Comm_After_GST)))
//                txtTot_Comm_After_GST = 0

            var Sum = parseFloat(txtTotalAfterGST) - parseFloat(txtTot_Comm_After_GST);



            switch (obj.id) {    // try generate related gridview textbox control id for validate installment total amd total value
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
            //var cntrlid = 'grdBILLING_INFO_ctl0' + '_' + objInstallment_CONTROL;


            //ValidateColumnTotal(cntrlid);  //call  function at total amount controls onblur which check the sum of all installment is equal to total amount or not

            if (Sum != NaN) {
                document.getElementById("txtTOTAL_AMOUNT").value = formatAmount(parseFloat(Sum));
                document.getElementById("hidTOTAL_AMOUNT").value = formatAmount(parseFloat(Sum));
                //alert(document.getElementById("hidTOTAL_AMOUNT").value);

            } else {
                document.getElementById("txtTOTAL_AMOUNT").value = '';
            }
            if(txtTotal_Before_GST!=NaN)
            {
             document.getElementById("txtTotal_Before_GST").value = formatAmount(parseFloat(txtTotal_Before_GST));
                document.getElementById("hidTotal_Before_GST").value = formatAmount(parseFloat(txtTotal_Before_GST));
            }
            else
            {
            document.getElementById("txtTotal_Before_GST").value='';
            }
            if(txtGST!=NaN)
            {
             document.getElementById("txtTOTAL_TAXES").value = formatAmount(parseFloat(txtGST));
                document.getElementById("hidTOTAL_TAXES").value = formatAmount(parseFloat(txtGST));
            }
            else
            {
            document.getElementById("txtTOTAL_TAXES").value='';
            }
            if(txtTotalAfterGST!=NaN)
            {
             document.getElementById("txtTotal_After_GST").value = formatAmount(parseFloat(txtTotalAfterGST));
                document.getElementById("hidtotal_After_GST").value = formatAmount(parseFloat(txtTotalAfterGST));
            }
            else
            {
            document.getElementById("txtTOTAL_FEES").value='';
            }
            if(txtGST_on_Commission!=NaN)
            {
             document.getElementById("txtGST_on_Commission").value = formatAmount(parseFloat(txtGST_on_Commission));
                document.getElementById("hidGST_on_Commission").value = formatAmount(parseFloat(txtGST_on_Commission));
            }
            else
            {
            document.getElementById("txtGST_on_Commission").value='';
            }
            if(txtTot_Comm_After_GST!=NaN)
            {
             document.getElementById("txtTot_Comm_After_GST").value = formatAmount(parseFloat(txtTot_Comm_After_GST));
                document.getElementById("hidTot_Comm_After_GST").value = formatAmount(parseFloat(txtTot_Comm_After_GST));
            }
            else
            {
            document.getElementById("txtTot_Comm_After_GST").value='';
            }

        }

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
            }

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
        function roundNumber(num, dec) {
            var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
            return result;
        } 
       $(document).ready(function(){
            CalcTotalAmount(this);
        });


    </script>
</head>
<body oncontextmenu="return true;" leftmargin="0" rightmargin="0" ms_positioning="GridLayout" onload="Init();UserIsManager();">
    <form id="POL_BILLING_INFO" runat="server" name="POL_BILLING_INFO" onsubmit="" method="post">
  
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
       
        <tbody id="trdetails" runat="server">
        <tr>
            <td class="headereffectCenter" colspan="10">
                <asp:Label ID="lblHeader" runat="server">Billing Information</asp:Label>
            </td>
        </tr> <!-- lblHeader -->
        <tr id="trload" runat="server">
            <td class="midcolorc" colspan="10">
                <asp:Label ID="lblFormLoad" runat="server" CssClass="errmsg"></asp:Label>
            </td>
        </tr> <!-- lblFormLoad -->
            <tr>
                <td class="midcolorc" colspan="10">
                    <asp:Label ID="lblFormLoadMessage" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
                </td>
            </tr> <!--lblFormLoadMessage -->
            <tr>
                <td class="pageHeader" align="left" colspan="10">
                    <%--<br />--%>
                    <asp:Label ID="lblManHeader" runat="server" colspan="2"></asp:Label>
                </td>
            </tr> <!-- lblManHeader -->
            <tr>
                <td class="midcolorc" colspan="10">
                    <asp:Label ID="lblMessage" runat="server" CssClass="errmsg"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="midcolorc" colspan="10">
                    <asp:Label ID="lblcustommsg" runat="server" CssClass="errmsg"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="5" class="midcolorc">
                </td>
            </tr>
            <tr>
                <td class="midcolorr" width="18%" colspan="10">
                   
                </td>
            </tr>
                        <tr id="trNBS_AMOUNT_DTL" >
                <td class="midcolora" width="12%" align="left">
               <%-- Changed by Kuldeep on 4-Feb-2012 for TFS # 3408--%>
                   <b> <asp:Label runat="server" ID="capNBS_PREMIUM">Gross Premium</asp:Label></b>
                     <span class="mandatory">*</span>
                    <br />
                   <span class="midcolorr"><asp:Label runat="server" ID="lblNBS_PREMIUM"  Visible="false"></asp:Label></span>  
                </td>
                <td class="midcolora" width="10%" align="left">
                   <b>  <asp:Label runat="server" ID="capNBS_INTR">Other Charges</asp:Label></b><br />
                    <asp:Label runat="server" ID="lblNBS_INTR" Visible="false"></asp:Label>
                </td>
                <%--for Total Before GST --%>

                 <td class="midcolora" width="10%">
                   <b>  <asp:Label runat="server" ID="capTotalBeforeGST">Total Premium Before GST</asp:Label></b><br />
                    <asp:Label runat="server" ID="lblTotalBeforeGST"></asp:Label>
                </td>
                <%--for GST --%>
                  <td class="midcolora" width="10%">
                    <b> <asp:Label runat="server" ID="capNBS_TAX">GST</asp:Label></b>
                    <br />
                   <asp:Label runat="server" ID="lblNBS_TAX" Visible="false"></asp:Label>
                </td>
                <%--Total After GST erlier it was Policy Fees--%>
                    
               <td class="midcolora" width="10%">
                   <b>  <asp:Label runat="server" ID="capNBS_FEES">Total Premium After GST</asp:Label></b>
                    <br />
                   <asp:Label runat="server" ID="lblNBS_FEES" Visible="false"></asp:Label>
                </td>
              

                <%--Gross Commission --%>
                 <td class="midcolora" width="10%">
                   <b>  <asp:Label runat="server" ID="capGrossCommission">Gross Premium Comission</asp:Label></b><br />
                    <asp:Label runat="server" ID="lblGrossCommission"></asp:Label>
                </td>
                 <%--GST on Commission --%>
                 <td class="midcolora" width="10%">
                   <b>  <asp:Label runat="server" ID="capGSTonCommission">GST on Commission</asp:Label></b><br />
                    <asp:Label runat="server" ID="lblGSTonCommission"></asp:Label>
                </td>
                 <%--Total Commission after GST --%>
                 <td class="midcolora" width="10%">
                   <b>  <asp:Label runat="server" ID="capToatlCommAfterGST">Total Commission after GST</asp:Label></b><br />
                    <asp:Label runat="server" ID="lblToatlCommAfterGST"></asp:Label>
                </td>
           
                <td class="midcolora" width="10%">
                   <b>  <asp:Label runat="server" ID="capNBS_TOTAL">Net Amount</asp:Label></b>
                    <br />
                     <asp:Label runat="server" ID="lblNBS_TOTAL" Visible="false"></asp:Label>
                   </td>
                    <td class="midcolora" width="10%">
                   <cmsb:CmsButton runat="server" ID="btnGet_Premium" CssClass="clsButton" Text="Get Premium"
                        OnClick="btnGet_Premium_Click" style="display:none"/>
                   </td>
            </tr>
            <tr align="left">
                <td class="midcolora" width="10%">
                    <%--<asp:Label runat="server" ID="capTOTAL_PREMIUM">Gross Premium</asp:Label>--%>
                  <%--  <span class="mandatory">*</span>--%>
                  <%--  <br />--%>
                    <asp:TextBox runat="server"  MaxLength = "17" width="90%" CssClass="inputcurrency" ID="txtTOTAL_PREMIUM" onblur="CalcTotalAmount(this);this.value=formatAmount(this.value)"></asp:TextBox>
                    <input type="hidden" runat="server" id="hidTOTAL_PREMIUM" />
                    <br />
                    <asp:RequiredFieldValidator runat="server" ID="rfvTOTAL_PREMIUM" ErrorMessage=""
                        Display="Dynamic" ControlToValidate="txtTOTAL_PREMIUM" ValidationGroup="Billing"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="revTOTAL_PREMIUM" Display="Dynamic"
                        ErrorMessage="" ControlToValidate="txtTOTAL_PREMIUM" ValidationGroup="Billing"></asp:RegularExpressionValidator>
               
                    <asp:CustomValidator ID="csvTOTAL_PREMIUM" runat="server" ControlToValidate="txtTOTAL_PREMIUM" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimitRange" ValidationGroup="Billing"></asp:CustomValidator>
                </td>
                <td class="midcolora" width="10%">
                 <%--   <asp:Label runat="server" ID="capTOTAL_INTEREST_AMOUNT">Other Charges</asp:Label>--%>
           
                    <asp:TextBox runat="server" CssClass="inputcurrency" 
                        ID="txtTOTAL_INTEREST_AMOUNT" MaxLength = "14" Width="100%" onblur="CalcTotalAmount(this);this.value=formatAmount(this.value);" ></asp:TextBox>
                        
                    <input type="hidden" runat="server" id="hidTOTAL_INTEREST_AMOUNT" />
                    <br />
                    <asp:RequiredFieldValidator runat="server" ID="rfvTOTAL_INTEREST_AMOUNT" Enabled="false"
                        ErrorMessage="" Display="Dynamic" ControlToValidate="txtTOTAL_INTEREST_AMOUNT" ValidationGroup="Billing"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="revTOTAL_INTEREST_AMOUNT" Display="Dynamic"
                        ErrorMessage="" ControlToValidate="txtTOTAL_INTEREST_AMOUNT" Enabled="False" ValidationGroup="Billing"></asp:RegularExpressionValidator>
                </td>
                   <%-- for total before GST--%>
                 <td class="midcolora" width="10%">
                    <%--<asp:Label runat="server" ID="capTotal_Before_GST">Total Before GST</asp:Label><br />--%>
                     
                    <asp:TextBox runat="server" CssClass="CustomCssLeftAligned" BorderWidth=0 ReadOnly="true"
                        ID="txtTotal_Before_GST" MaxLength = "14" Width="100%" 
                          ></asp:TextBox>
                   
                    <input type="hidden" runat="server" id="hidTotal_Before_GST" />
                    <br />
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Enabled="false"
                        ErrorMessage="" Display="Dynamic" ControlToValidate="txtTOTAL_INTEREST_AMOUNT"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" Display="Dynamic"
                        ErrorMessage="" ControlToValidate="txtTOTAL_INTEREST_AMOUNT" Enabled="False"></asp:RegularExpressionValidator>
                </td>
                   <td class="midcolora" width="10%">
                    <%--<asp:Label runat="server" ID="capTOTAL_TAXES">GST</asp:Label>
                    <br />--%> 
                    <asp:TextBox runat="server" MaxLength = "14" CssClass="CustomCssLeftAligned" BorderWidth=0 ReadOnly="true"
                        ID="txtTOTAL_TAXES" width="100%" ></asp:TextBox>
           
                    <input type="hidden" runat="server" id="hidTOTAL_TAXES" />
                    <br />
                    <asp:RequiredFieldValidator runat="server" ID="rfvTOTAL_TAXES" ErrorMessage="" Enabled="false"
                        Display="Dynamic" ControlToValidate="txtTOTAL_TAXES"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="revTOTAL_TAXES" Display="Dynamic"
                        ErrorMessage="" ControlToValidate="txtTOTAL_TAXES" Enabled="False"></asp:RegularExpressionValidator>
                </td>
                <td class="midcolora" width="10%">
                    <%--<asp:Label runat="server" ID="capTOTAL_FEES">Total After GST</asp:Label>
                    <br />  --%>
                    <asp:TextBox runat="server"  MaxLength = "14" 
                        ID="txtTOTAL_FEES" style="display:none" value="0"></asp:TextBox>
                     <asp:TextBox runat="server" CssClass="CustomCssLeftAligned" BorderWidth=0 ReadOnly="true" MaxLength = "14" 
                        ID="txtTotal_After_GST" ></asp:TextBox>
                         <input type="hidden" runat="server" id="hidTotal_After_GST" />
                    <input type="hidden" runat="server" id="hidTOTAL_FEES" value="0"/>
                    <input type="hidden" runat="server" id="hidSTATE_FEES" />
                    <input type="hidden" runat="server" id="hidSTATE_TRAN_FEES" />
                    <br />
                  <%--  <asp:RequiredFieldValidator runat="server" ID="rfvTOTAL_FEES" ErrorMessage="" Enabled="false"
                        Display="Dynamic" ControlToValidate="txtTOTAL_FEES"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="revTOTAL_FEES" Display="Dynamic"
                        ErrorMessage="" ControlToValidate="txtTOTAL_FEES" Enabled="False"></asp:RegularExpressionValidator>
                    <asp:CustomValidator runat="server" ID="csvTOTAL_FEES" Display="Dynamic" ControlToValidate="txtTOTAL_FEES"
                        ErrorMessage="" ClientValidationFunction="StateFee" Enabled="False"></asp:CustomValidator>
                        Comment By Kuldeep--%>
                </td>
                <td class="midcolora" width="10%">
                    <%--<asp:Label runat="server" ID="Label2">Gross Commission</asp:Label><br />--%>
                     
                    <asp:TextBox runat="server" CssClass="CustomCssLeftAligned" BorderWidth=0 ReadOnly="true" 
                        ID="txtGross_Commission" MaxLength = "14" Width="100%" ></asp:TextBox>
                   
                    <input type="hidden" runat="server" id="hidGross_Commission" />
                    <br />
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" Enabled="false"
                        ErrorMessage="" Display="Dynamic" ControlToValidate="txtTOTAL_INTEREST_AMOUNT"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" Display="Dynamic"
                        ErrorMessage="" ControlToValidate="txtTOTAL_INTEREST_AMOUNT" Enabled="False"></asp:RegularExpressionValidator>
                </td><td class="midcolora" width="10%">
                    <%--<asp:Label runat="server" ID="Label3">GST on Comm.</asp:Label><br />
                     --%>
                    <asp:TextBox runat="server" CssClass="CustomCssLeftAligned" BorderWidth=0 ReadOnly="true"
                        ID="txtGST_on_Commission" MaxLength = "14" Width="100%" ></asp:TextBox>
                   
                    <input type="hidden" runat="server" id="hidGST_on_Commission" />
                    <br />
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" Enabled="false"
                        ErrorMessage="" Display="Dynamic" ControlToValidate="txtTOTAL_INTEREST_AMOUNT"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator3" Display="Dynamic"
                        ErrorMessage="" ControlToValidate="txtTOTAL_INTEREST_AMOUNT" Enabled="False"></asp:RegularExpressionValidator>
                </td><td class="midcolora" width="10%">
                    <%--<asp:Label runat="server" ID="Label4">Total Comm. After GST</asp:Label><br />--%>
                     
                    <asp:TextBox runat="server" CssClass="CustomCssLeftAligned" BorderWidth=0 ReadOnly="true"
                        ID="txtTot_Comm_After_GST" MaxLength = "14" Width="100%" ></asp:TextBox>
                   
                    <input type="hidden" runat="server" id="hidTot_Comm_After_GST" />
                    <br />
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" Enabled="false"
                        ErrorMessage="" Display="Dynamic" ControlToValidate="txtTOTAL_INTEREST_AMOUNT"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator4" Display="Dynamic"
                        ErrorMessage="" ControlToValidate="txtTOTAL_INTEREST_AMOUNT" Enabled="False"></asp:RegularExpressionValidator>
                </td>
                <td class="midcolora" width="10%">
                    <%--<asp:Label runat="server" ID="capTOTAL_AMOUNT">Net Amount</asp:Label>
                    <br />--%>
                    <asp:TextBox runat="server" width="100%" CssClass="CustomCssLeftAligned" BorderWidth=0 ReadOnly="true" ID="txtTOTAL_AMOUNT" ></asp:TextBox>
                    <input type="hidden" runat="server" id="hidTOTAL_AMOUNT" name="hidTOTAL_AMOUNT" />
                    <br />
                    <asp:RegularExpressionValidator runat="server" ID="revTOTAL_AMOUNT" Enabled="false"
                        Display="Dynamic" ErrorMessage="" ControlToValidate="txtTOTAL_AMOUNT"></asp:RegularExpressionValidator>
                </td>
                 <td class="midcolora" colspan="10" style="height: 10px">
                </td>
              
            </tr>
            <tr>
              <td colspan="9" class="midcolorba">
                </td>
                <td class="midcolorr" width="30%">
                  <cmsb:CmsButton ID="btnGenrateInstallment" name="btnGenrateInstallment" runat="server"  Width="170px" Text="Generate Installment"
                        OnClick="btnGenrateInstallment_Click" CssClass="clsButton" ValidationGroup="Billing"  />
                        </td>
            </tr>
            <tr>
                <td class="midcolora" colspan="10" style="height: 10px">
                </td>
            </tr>
             <tr>
               <td colspan="9" class="midcolorba">
                  
                </td>
                <td class="midcolorba">
                </td>
            </tr>
            <%--Meeaning of This row and next is not yet clear(Kuldeep) display none by kuldeep--%>
           <tr id="trTran" runat="server" style="display:none">
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
            <tr id="trEND_PRM_DIST_OPTION" runat="server" style="display:none">
                <td class="midcolora" colspan="6" style="height: 10px">
                    <asp:Label ID="capPRM_DIST_TYPE" runat="server">Endorsment Premium Distribution Option</asp:Label><br />
                    <asp:DropDownList runat="server" ID="cmbPRM_DIST_TYPE">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trINFOR" runat="server" style="display:none">
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
                <td colspan="9" class="midcolorba">
                      <asp:Label runat="server" ID="capBILLING_PLAN">Billing Plan</asp:Label>
                    <br />
                    <asp:DropDownList runat="server" ID="cmbBILLING_PLAN" onchange="setBillingPlan()">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <cmsb:CmsButton runat="server" Text="Re Adjust" 
                        CssClass="clsButton" CausesValidation="false" ID="btnAdjust" OnClick="btnAdjust_Click" Visible=false />
                    <br />
                    <asp:RequiredFieldValidator runat="server" ID="rfvBILLING_PLAN" ErrorMessage="" Display="Dynamic"
                        ControlToValidate="cmbBILLING_PLAN" ValidationGroup="Billing"></asp:RequiredFieldValidator>
                </td>
                <td class="midcolorba">
                </td>
            </tr>
            <tr>
                <td colspan="10" class="midcolorba" style="height: 5px;">
                </td>
            </tr>
            <tr>
            <td colspan="10" class="midcolorc" >
            <asp:GridView AutoGenerateColumns="true" runat="server" ID="grdBILLING_DETAILS" Width="100%">
             <HeaderStyle CssClass="headereffectWebGrid" HorizontalAlign="Center"></HeaderStyle>
                        <RowStyle CssClass="midcolora"></RowStyle>
                        </asp:GridView>
            </td>
            </tr>
            <tr style="display:none">
                <td colspan="10" class="midcolorc" >
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
                                    <asp:Label runat="server" ID="capINSTALLMENT_DATE" Text="">Installment Effective Date</asp:Label>
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
                            
                            <asp:TemplateField visible=false>
                                <ItemStyle Width="5%" />
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capPREMIUM" Text="">Policy Premium</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox runat="server" Width="100px" MaxLength = "15" CssClass="inputcurrency" ID="txtPREMIUM"
                                        onblur="InstallmentTotal(this);this.value=formatAmount(this.value);"
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
                 <td colspan="9" class="midcolorba">
                </td>
                <td class="midcolorr" width="30%" style="display:none">
                 <cmsb:CmsButton ID="btnSave" runat="server" CausesValidation="true" Text="Save" CssClass="clsButton"
                        OnClick="btnSave_Click"></cmsb:CmsButton>
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
               <input type="hidden" runat="server" id="hidComm_percentage" name="hidComm_percentage" value="0" />  
                <%--End Here--%>
            </td>
        </tr>
            </tbody>
            </table>
            </form>
            
</body>
</html>

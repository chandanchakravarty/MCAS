<%@ Page Language="c#" CodeBehind="AddInstallmentPlan.aspx.cs" AutoEventWireup="false"
    Inherits="Cms.CmsWeb.Accounting.AddInstallmentPlan" ValidateRequest="false" %>

<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>AddInstallmentPlan</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet">

    <script src="/cms/cmsweb/scripts/xmldom.js"></script>

    <script src="/cms/cmsweb/scripts/common.js"></script>

    <script src="/cms/cmsweb/scripts/form.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>

    <script language="vbscript" type="text/vbscript">
    
		function getUserConfirmation(msg)
		
			getUserConfirmation= msgbox(msg,vbYesNo,"CMS")
		End function
		
		function getUserConfirmationRP(msg1)
	
			getUserConfirmationRP= msgbox(msg1,vbYesNo,"CMS")
		End function
	
    </script>

    <script language="javascript" type="text/javascript">




        //Function for checking whether total of all break ups is eqla to 100
        function CheckTotalAmount() {
            //Validating the controls
            Page_ClientValidate();
            //Calculating total amount
            CalculateTotal();

            if (document.getElementById("spnTotalAmount").innerHTML == "" ||
				parseInt(document.getElementById("spnTotalamount").innerHTML) != 100) {
                alert('<%=totbusinessprocess %>');
                return false;
            }
            return true;
        }

        function CheckTotalAmountRP() {
            //Validating the controls
            Page_ClientValidate();
            //Calculating total amount 
            CalculateTotalRP();
            if (document.getElementById("spnTotalAmountRP").innerHTML == "" ||
				parseInt(document.getElementById("spnTotalamountRP").innerHTML) != 100) {
                alert('<%=totrenewalprocess %>');
                return false;
            }
            return true;
        }
        //Added For Itrack Issue #6746   
        function CompareFunction() {
            if (CheckTotalAmount() == false || CheckTotalAmountRP() == false) {
                return false;
            }
            else
                return true;
        }

        //Shows the amount fields based on the number of payments
        function ShowAmountFields(flag) {
          
            var blnCalc = true;
            if (flag == null) {


                /*show a prompt*/
                var Reply = getUserConfirmation('<%=recalbusinessprocess %>');

                if (Reply == 6) {

                    blnCalc = true;
                    DisableValidators();


                }
                else
                    blnCalc = false;
            }
            else {
                blnCalc = flag;
            }


            var months = document.getElementById("cmbNO_OF_PAYMENTS").value


            var amt = 100 / months;

            amt = parseFloat(FormatAmountForSum(amt.toFixed(3)));

            var ctr = 0, total = 0; ;

            total = amt * months;

            for (ctr = 1; ctr <= months; ctr++) {
                document.getElementById("txtPERCENT_BREAKDOWN" + ctr.toString()).style.display = "inline";
                document.getElementById("spnPERCENT_BREAKDOWN" + ctr.toString()).style.display = "inline";
                document.getElementById("rfvPERCENT_BREAKDOWN" + ctr.toString()).setAttribute("enabled", true);

                if (blnCalc == true) {
                    //debugger;
                    document.getElementById("txtPERCENT_BREAKDOWN" + ctr.toString()).value =formatRateBase(FormatAmountForSum(amt.toFixed(4)));
                }
            }

            for (ctr = ctr; ctr <= 12; ctr++) {
                document.getElementById("txtPERCENT_BREAKDOWN" + ctr.toString()).value = "";
                document.getElementById("txtPERCENT_BREAKDOWN" + ctr.toString()).style.display = "none";
                document.getElementById("spnPERCENT_BREAKDOWN" + ctr.toString()).style.display = "none";
                document.getElementById("rfvPERCENT_BREAKDOWN" + ctr.toString()).setAttribute("enabled", false);
                document.getElementById("rfvPERCENT_BREAKDOWN" + ctr.toString()).style.display = "none";
            }

            if (blnCalc == true) {
                diff = 100 - total;
                diff = parseFloat(diff.toFixed(3));
                if (total < 100) {
                    document.getElementById("txtPERCENT_BREAKDOWN1").value = formatRateBase(((diff) + parseFloat(FormatAmountForSum(document.getElementById("txtPERCENT_BREAKDOWN1").value))).toFixed(4));
                }
                else if (total > 100) {
                document.getElementById("txtPERCENT_BREAKDOWN1").value = formatRateBase((parseFloat(FormatAmountForSum(document.getElementById("txtPERCENT_BREAKDOWN1").value)) - (diff * -1)).toFixed(4));

                }
            } 
            document.getElementById("spnTotalAmount").innerHTML = formatRateBase(100.0000);

        }

        function CalculateTotal() {
           // debugger
            var ctr = 0, total = 0.0;
            for (ctr = 1; ctr <= 12; ctr++) {
                if (document.getElementById("txtPERCENT_BREAKDOWN" + ctr.toString()).style.display == "inline") {
                    val = FormatAmountForSum(document.getElementById("txtPERCENT_BREAKDOWN" + ctr.toString()).value);
                    if (!isNaN(val) && val.trim() != "") {
                        document.getElementById("txtPERCENT_BREAKDOWN" + ctr.toString()).value =formatRateBase(parseFloat(FormatAmountForSum(document.getElementById("txtPERCENT_BREAKDOWN" + ctr.toString()).value)).toFixed(4));
                        total = total + parseFloat(FormatAmountForSum(document.getElementById("txtPERCENT_BREAKDOWN" + ctr.toString()).value));
                    }
                }
            }
            if (document.getElementById("hidSYSTEM_GENERATED_FULL_PAY").value == "true") {
                document.getElementById("spnTotalAmount").innerHTML = formatRateBase(100.00);

            }
            else {
                document.getElementById("spnTotalAmount").innerHTML =formatRateBase(FormatAmountForSum(total.toFixed(4)));
            }
        }

        // For Renewal Process

        function ShowAmountFieldsRP(flag) {


            var blnCalc = true;
            if (flag == null) {

                /*show a prompt*/
                var Reply = getUserConfirmationRP('<%=recalrenewalprocess %>');
                if (Reply == "6") {
                    blnCalc = true;
                    DisableValidators();
                }
                else
                    blnCalc = false;
            }
            else {
                blnCalc = flag;
            }


            var months = document.getElementById("cmbNO_OF_PAYMENTS").value


            var amt = 100 / months;

            amt = parseFloat(FormatAmountForSum(amt.toFixed(3)));

            var ctr = 0, total = 0; ;

            total = amt * months;

            for (ctr = 1; ctr <= months; ctr++) {
                document.getElementById("txtPERCENT_BREAKDOWNRP" + ctr.toString()).style.display = "inline";
                document.getElementById("spnPERCENT_BREAKDOWNRP" + ctr.toString()).style.display = "inline";
                document.getElementById("rfvPERCENT_BREAKDOWNRP" + ctr.toString()).setAttribute("enabled", true);

                if (blnCalc == true) {
                    document.getElementById("txtPERCENT_BREAKDOWNRP" + ctr.toString()).value =formatRateBase(FormatAmountForSum(amt.toFixed(4)));
                }
            }

            for (ctr = ctr; ctr <= 12; ctr++) {
                document.getElementById("txtPERCENT_BREAKDOWNRP" + ctr.toString()).value = "";
                document.getElementById("txtPERCENT_BREAKDOWNRP" + ctr.toString()).style.display = "none";
                document.getElementById("spnPERCENT_BREAKDOWNRP" + ctr.toString()).style.display = "none";
                document.getElementById("rfvPERCENT_BREAKDOWNRP" + ctr.toString()).setAttribute("enabled", false);
                document.getElementById("rfvPERCENT_BREAKDOWNRP" + ctr.toString()).style.display = "none";
            }

            if (blnCalc == true) {
                diff = 100 - total;
                diff = parseFloat(diff.toFixed(3));
                if (total < 100) {
                    document.getElementById("txtPERCENT_BREAKDOWNRP1").value = formatRateBase(((diff) + parseFloat(FormatAmountForSum(document.getElementById("txtPERCENT_BREAKDOWNRP1").value))).toFixed(4));
                }
                else if (total > 100) {
                document.getElementById("txtPERCENT_BREAKDOWNRP1").value = formatRateBase((parseFloat(FormatAmountForSum(document.getElementById("txtPERCENT_BREAKDOWNRP1").value))- (diff * -1)).toFixed(4));

                }
            }

            document.getElementById("spnTotalAmountRP").innerHTML = formatRateBase(100.0000);
        }



        // RP total
        function CalculateTotalRP() {
            var ctr = 0, total = 0.0;
            for (ctr = 1; ctr <= 12; ctr++) {
                if (document.getElementById("txtPERCENT_BREAKDOWNRP" + ctr.toString()).style.display == "inline") {
                    val = FormatAmountForSum(document.getElementById("txtPERCENT_BREAKDOWNRP" + ctr.toString()).value);
                    if (!isNaN(val) && val.trim() != "") {
                        document.getElementById("txtPERCENT_BREAKDOWNRP" + ctr.toString()).value = formatRateBase(parseFloat(FormatAmountForSum(document.getElementById("txtPERCENT_BREAKDOWNRP" + ctr.toString()).value)).toFixed(4));
                        total = total + parseFloat(FormatAmountForSum(document.getElementById("txtPERCENT_BREAKDOWNRP" + ctr.toString()).value));
                    }
                }
            }
            if (document.getElementById("hidSYSTEM_GENERATED_FULL_PAY").value == "true") {
                document.getElementById("spnTotalAmountRP").innerHTML = formatRateBase(100.00);
            }
            else {
                document.getElementById("spnTotalAmountRP").innerHTML =formatRateBase(FormatAmountForSum(total.toFixed(4)));
            }

        }



        function AddData() {

            document.getElementById('hidIDEN_PLAN_ID').value = 'New';
            document.getElementById('txtPLAN_CODE').focus();
            document.getElementById('txtPLAN_CODE').value = '';
            document.getElementById('txtPLAN_DESCRIPTION').value = '';
            document.getElementById('cmbNO_OF_PAYMENTS').options.selectedIndex = 0;
            //document.getElementById('txtMONTHS_BETWEEN').value = '';
            document.getElementById('txtPERCENT_BREAKDOWN1').value = '';
            document.getElementById('txtPERCENT_BREAKDOWN2').value = '';
            document.getElementById('txtPERCENT_BREAKDOWN3').value = '';
            document.getElementById('txtPERCENT_BREAKDOWN4').value = '';
            document.getElementById('txtPERCENT_BREAKDOWN5').value = '';
            document.getElementById('txtPERCENT_BREAKDOWN6').value = '';
            document.getElementById('txtPERCENT_BREAKDOWN7').value = '';
            document.getElementById('txtPERCENT_BREAKDOWN8').value = '';
            document.getElementById('txtPERCENT_BREAKDOWN9').value = '';
            document.getElementById('txtPERCENT_BREAKDOWN10').value = '';
            document.getElementById('txtPERCENT_BREAKDOWN11').value = '';
            document.getElementById('txtPERCENT_BREAKDOWN12').value = '';
            document.getElementById('cmbAPPLABLE_POLTERM').options.selectedIndex = 0;
            document.getElementById('cmbMODE_OF_DOWNPAY').options.selectedIndex = -1;
            document.getElementById('cmbMODE_OF_DOWNPAY_RENEW').options.selectedIndex = -1;

            //document.getElementById('txtNO_INS_DOWNPAY').value = '';
            //document.getElementById('txtNO_INS_DOWNPAY_RENEW').value = '';
            document.getElementById('txtPAST_DUE_RENEW').value = '';
            document.getElementById('cmbAPPLABLE_POLTERM').options.selectedIndex = -1;

            //document.getElementById('txtSERVICE_CHARGE').value  = '';
            //document.getElementById('txtCONVENIENCE_FEES').value  = '';
            document.getElementById('txtGRACE_PERIOD').value = '';
            //Added By Shafi 09-01-2006
            //Check whether the object is null or not
            if (document.getElementById("btnActivateDeactivate") != null)
                document.getElementById('btnActivateDeactivate').setAttribute('disabled', true);
            ChangeColor();
            DisableValidators();

            //ShowAmountFields
        }
        function PopulatePERCENT_BREAKDOWNControls(tree) {
            
            for (Count = 0; Count < tree.childNodes.length; Count++) {
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWN1' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWN1').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWN2' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWN2').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWN3' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWN3').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWN4' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWN4').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWN5' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWN5').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWN6' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWN6').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWN7' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWN7').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWN8' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWN8').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWN9' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWN9').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWN10' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWN10').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWN11' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWN11').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWN12' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWN12').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                //
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWNRP1' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWNRP1').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWNRP2' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWNRP2').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWNRP3' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWNRP3').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWNRP4' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWNRP4').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWNRP5' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWNRP5').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWNRP6' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWNRP6').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWNRP7' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWNRP7').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWNRP8' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWNRP8').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWNRP9' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWNRP9').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWNRP10' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWNRP10').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWNRP11' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWNRP11').value = formatRateBase(tree.childNodes[Count].firstChild.text);
                if (tree.childNodes[Count].nodeName == 'PERCENT_BREAKDOWNRP12' && tree.childNodes[Count].firstChild != null && tree.childNodes[Count].firstChild.text != "")
                    document.getElementById('txtPERCENT_BREAKDOWNRP12').value = formatRateBase(tree.childNodes[Count].firstChild.text);
            }
        }
        function populateXML() {
         
            if (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1') {
                var tempXML = document.getElementById('hidOldData').value;
                if (tempXML != "") {
                    populateFormData(tempXML, ACT_INSTALL_PLAN_DETAIL);
                    CalculateTotal();
                    //alert(tempXML);
                    /////////
                    var objXmlHandler = new XMLHandler();
                    var unqID;
                    var tree = (objXmlHandler.quickParseXML(tempXML).getElementsByTagName('Table')[0]);
                    if (tree) {
                        PopulatePERCENT_BREAKDOWNControls(tree);
                        for (i = 0; i < tree.childNodes.length; i++) {
                            if (tree.childNodes[i].nodeName == 'MODE_OF_DOWNPAY_RENEW1' || tree.childNodes[i].nodeName == 'MODE_OF_DOWNPAY_RENEW2') {
                                if (tree.childNodes[i].firstChild) {
                                    if (tree.childNodes[i].firstChild.text != "" && tree.childNodes[i].firstChild.text != "0") {
                                        for (j = 0; j < document.getElementById('cmbMODE_OF_DOWNPAY_RENEW').options.length; j++) {

                                            if (document.getElementById('cmbMODE_OF_DOWNPAY_RENEW').options[j].value == tree.childNodes[i].firstChild.text)
                                                document.getElementById('cmbMODE_OF_DOWNPAY_RENEW').options[j].selected = true;

                                        }
                                    }
                                }

                            }

                            if (tree.childNodes[i].nodeName == 'MODE_OF_DOWNPAY1' || tree.childNodes[i].nodeName == 'MODE_OF_DOWNPAY2') {
                                if (tree.childNodes[i].firstChild) {
                                    if (tree.childNodes[i].firstChild.text != "" && tree.childNodes[i].firstChild.text != "0") {
                                        for (j = 0; j < document.getElementById('cmbMODE_OF_DOWNPAY').options.length; j++) {

                                            if (document.getElementById('cmbMODE_OF_DOWNPAY').options[j].value == tree.childNodes[i].firstChild.text)
                                                document.getElementById('cmbMODE_OF_DOWNPAY').options[j].selected = true;

                                        }
                                    }
                                }
                            }


                        }
                    }
                    /////////
                    TriggerOnBlurFunction() 

                }
                else {

                    AddData();
                }
            }
            else {
                document.getElementById('btnActivateDeactivate').setAttribute('disabled', true);
            }
            ShowAmountFields(false);
            ShowAmountFieldsRP(false);

            CheckSystemGeneratedFullPayPlan();
            return false;
        }

        //Disable some sections based on whether the plan is system generated or not
        function CheckSystemGeneratedFullPayPlan() {

            if (document.getElementById("hidSYSTEM_GENERATED_FULL_PAY").value == "true") {
                flag = true;
                if (document.getElementById("btnDelete")) {
                    document.getElementById("btnDelete").style.display = "none";
                }
                if (document.getElementById("btnActivateDeactivate")) {
                    document.getElementById("btnActivateDeactivate").style.display = "none";
                }
            }
            else {
                flag = false;
                if (document.getElementById("btnDelete")) {
                    document.getElementById("btnDelete").style.display = "inline";
                }
                if (document.getElementById("btnActivateDeactivate")) {
                    document.getElementById("btnActivateDeactivate").style.display = "inline";
                }
            }

            document.getElementById("txtPLAN_CODE").readOnly = flag;
            document.getElementById("txtPLAN_DESCRIPTION").readOnly = flag;
            //document.getElementById("txtMONTHS_BETWEEN").readOnly = flag;
            document.getElementById("cmbNO_OF_PAYMENTS").disabled = flag;
            document.getElementById("txtPERCENT_BREAKDOWN1").readOnly = flag;
            //document.getElementById("txtNO_INS_DOWNPAY_RENEW").readOnly = flag;
            //document.getElementById("txtNO_INS_DOWNPAY").readOnly = flag;
            document.getElementById("cmbAPPLABLE_POLTERM").disabled = flag;
            document.getElementById("cmbPLAN_PAYMENT_MODE").selectedValue;
            document.getElementById("cmbPLAN_PAYMENT_MODE").disabled = flag;


        }
        function formReset() {
            document.ACT_INSTALL_PLAN_DETAIL.reset();
            populateXML();
            DisableValidators();
            ChangeColor();
            return false;
        }
        function CopyNBStoRP() {
            if (document.getElementById("chkCopy").checked == true) {
                for (ctr = 1; ctr <= 12; ctr++) {
                    document.getElementById("txtPERCENT_BREAKDOWNRP" + ctr.toString()).value
				 = document.getElementById("txtPERCENT_BREAKDOWN" + ctr.toString()).value;
                }
                document.getElementById("spnTotalAmountRP").innerHTML = document.getElementById("spnTotalAmount").innerHTML;
                // document.getElementById("txtNO_INS_DOWNPAY_RENEW").value = document.getElementById("txtNO_INS_DOWNPAY").value;

                for (j = 0; j < document.getElementById("cmbMODE_OF_DOWNPAY_RENEW").options.length; j++)
                    document.getElementById("cmbMODE_OF_DOWNPAY_RENEW").options[j].selected = false;

                var ln = document.getElementById("cmbMODE_OF_DOWNPAY").options.length;
                for (j = 0; j < ln; j++) {
                    var vl = document.getElementById("cmbMODE_OF_DOWNPAY").options[j].value;
                    if (document.getElementById("cmbMODE_OF_DOWNPAY").options[j].selected == true) {
                        document.getElementById("cmbMODE_OF_DOWNPAY_RENEW").options[j].selected = true;
                    }

                }

                var ln1 = document.getElementById("cmbDOWN_PAYMENT_PLAN").options.length;
                for (j = 0; j < ln1; j++) {
                    var vl = document.getElementById("cmbDOWN_PAYMENT_PLAN").options[j].value;
                    if (document.getElementById("cmbDOWN_PAYMENT_PLAN").options[j].selected == true) {
                        document.getElementById("cmbDOWN_PAYMENT_PLAN_RENEWAL").options[j].selected = true;
                    }
                }


            }


        }

        function ModeOfDownPayment() {
            if (document.getElementById("txtPLAN_CODE").value == 'FULLPAY') {
                document.getElementById("cmbMODE_OF_DOWNPAY").disabled = true;
                document.getElementById("cmbMODE_OF_DOWNPAY_RENEW").disabled = true;
            }
            else {
                document.getElementById("cmbMODE_OF_DOWNPAY").disabled = false;
                document.getElementById("cmbMODE_OF_DOWNPAY_RENEW").disabled = false;
            }
        }


        function FillSubLOB() {   		//if (document.getElementById('hidFormSaved')!= null  && document.getElementById('hidFormSaved').value=='3')

            if (document.getElementById('trDETAILS') != null && (document.getElementById('trDETAILS').style.display == "inline" || document.getElementById('trDETAILS').style.display == "")) {
                document.getElementById('cmbAPPLICABLE_LOB').innerHTML = '';
                var Xml = document.getElementById('hidLOBXML').value;

                var LOBId = "";
                var stID = "";

                if (document.getElementById('cmbAPPLICABLE_LOB').style.display == "inline") {
                    if (document.getElementById('cmbAPPLICABLE_LOB').selectedIndex != -1) {
                        LOBId = document.getElementById('cmbAPPLICABLE_LOB').options[document.getElementById('cmbAPPLICABLE_LOB').selectedIndex].value
                    }
                }
                else {
                    LOBId = document.getElementById('hidLOBId').value
                }
            }
        }

        function ValidateInstallment(objSource, objArgs) {
            var ddlval = document.getElementById("cmbSUBSEQUENT_INSTALLMENTS_OPTION");
            var txtval = document.getElementById("txtDAYS_SUBSEQUENT_INSTALLMENTS").value;
            var csvmsg = document.getElementById("csvSUBSEQUENT_INSTALLMENTS")
            var csvmsgoption = document.getElementById("csvSUBSEQUENT_INSTALLMENTS_OPTION")


            if (ddlval.selectedIndex == 1) {

                if (parseInt(txtval) > 12) {
                    objSource.innerHTML = csvmsg.getAttribute("ErrMsgmonths");
                    objSource.style.display = "inline";
                    objArgs.IsValid = false;
                }
            }

            else if (ddlval.selectedIndex == 2) {
                if (parseInt(txtval) > 53) {
                    objSource.innerHTML = csvmsg.getAttribute("ErrMsgweeks");
                    objSource.style.display = "inline";
                    objArgs.IsValid = false;
                }
            }
            else {
                if (parseInt(txtval) > 31) {
                    objSource.innerHTML = csvmsg.getAttribute("ErrMsgdays");
                    objSource.style.display = "inline";
                    objArgs.IsValid = false;
                }

            }

            if (objSource.id == csvmsg.id) {
                csvmsgoption.style.display = "none";
            }

            else {
                csvmsg.style.display = "none";
            }

        }
    </script>

    <script language="javascript" type="text/javascript">

        $(document).ready(function() {


            $("#cmbNO_OF_PAYMENTS").change(function() {
                //alert("Show Msg Fromjquery");
                ShowAmountFields();
                ShowAmountFieldsRP();
            });

        });
        //Added By Pradeep Kushwaha on 15 Dec 2010
        function TriggerOnBlurFunction() {
            
            $('#txtINTREST_RATES').blur();
            $('#txtMIN_INSTALLMENT_AMOUNT').blur();
            $('#txtREINSTATEMENT_FEES').blur();
            $('#txtNON_SUFFICIENT_FUND_FEES').blur();
            $('#txtINSTALLMENT_FEES').blur();
            $('#txtLATE_FEES').blur();
            
            return false;
        }
        function FormatAmountForSum(num) {
 
            num = ReplaceAll(num, sBaseDecimalSep, '.');
            return num;
        }

       
        function validateLimit(objSource, objArgs) {
      
           var Limt = document.getElementById(objSource.controltovalidate).value;
           
           Limt = FormatAmountForSum(Limt);
           if(parseFloat(Limt) >=0 && parseFloat(Limt) <=100)
                objArgs.IsValid = true;
            else
                objArgs.IsValid = false;
        }
        

    </script>

</head>
<body oncontextmenu="return false;" leftmargin="0" topmargin="0" onload="populateXML();ApplyColor(); ChangeColor(); ModeOfDownPayment();">
    <form id="ACT_INSTALL_PLAN_DETAIL" method="post" runat="server" onsubmit="document.getElementById('cmbAPPLABLE_POLTERM').disabled = false;">
    
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td>
                <table width="100%" align="center" border="0">
                    <tbody>
                        <tr>
                            <td class="pageHeader" colspan="3">
                               <asp:label ID="capheader" runat="server"></asp:label><%-- Please note that all fields marked with * are mandatory--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolorc" align="right" colspan="3">
                                <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="35%">
                                <asp:Label ID="capPLAN_CODE" runat="server">Plan Code</asp:Label><span class="mandatory">*</span>
                                <br />
                                <asp:TextBox ID="txtPLAN_CODE" runat="server" size="13" MaxLength="10"></asp:TextBox><br>
                                <asp:RequiredFieldValidator ID="rfvPLAN_CODE" runat="server" ControlToValidate="txtPLAN_CODE"
                                    ErrorMessage="PLAN_CODE can't be blank." Display="Dynamic"></asp:RequiredFieldValidator><%--<asp:RegularExpressionValidator
                                        ID="revPLAN_CODE" runat="server" ControlToValidate="txtPLAN_CODE" ErrorMessage="RegularExpressionValidator"
                                        Display="Dynamic"></asp:RegularExpressionValidator>--%>
                            </td>
                            <td class="midcolora" width="30%">
                                <asp:Label ID="capPLAN_DESCRIPTION" runat="server">Plan Description</asp:Label><span
                                    class="mandatory">*</span><br />
                                <asp:TextBox ID="txtPLAN_DESCRIPTION" runat="server" size="30" MaxLength="35"></asp:TextBox>
                                <a class="calcolora" href="#">
                                    <img id="imgSelect" style="cursor: hand" alt="" src="../../cmsweb/images/selecticon.gif"
                                        border="0" autopostback="false" runat="server">
                                </a>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvPLAN_DESCRIPTION" runat="server" ControlToValidate="txtPLAN_DESCRIPTION"
                                    ErrorMessage="PLAN_DESCRIPTION can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="33%">
                                <asp:Label ID="capAPPLABLE_POLTERM" runat="server">Applicable to Policy Term</asp:Label><span
                                    class="mandatory">*</span></br>
                                <asp:DropDownList ID="cmbAPPLABLE_POLTERM" runat="server">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbAPPLABLE_POLTERM"
                                    ErrorMessage="" Display="Dynamic"></asp:RequiredFieldValidator><%--Please enter Applicable to policy term.--%>
                            </td>
                        </tr>
                        <!---- added by pravesh    -->
                        <tr>
                            <td class="midcolora" width="35%">
                                <asp:Label ID="capAPPLICABLE_LOB" runat="server">Applicable to Products</asp:Label><span
                                    class="mandatory">*</span> </br><asp:DropDownList ID="cmbAPP_LOB" runat="server"
                                        onfocus="SelectComboIndex('cmbAPP_LOB')">
                                    </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvAPPLICABLE_LOB" runat="server" ControlToValidate="cmbAPPLABLE_POLTERM"
                                    ErrorMessage="Please enter Applicable to policy term." Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="33%">
                                <asp:Label ID="capPLAN_PAYMENT_MODE" runat="server">plan payment mode</asp:Label><span
                                    class="mandatory">*</span><br />
                                <asp:DropDownList ID="cmbPLAN_PAYMENT_MODE" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPLAN_PAYMENT_MODE" runat="server" ControlToValidate="cmbPLAN_PAYMENT_MODE"
                                    ErrorMessage="plan payment mode can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                                <td class="midcolora" width="33%">
                                    <asp:Label ID="capNO_OF_PAYMENTS" runat="server">Number of Payments</asp:Label><span
                                        class="mandatory">*</span><br />
                                    <asp:DropDownList ID="cmbNO_OF_PAYMENTS" runat="server">
                                        <asp:ListItem> </asp:ListItem>
                                        <asp:ListItem Value='1'>1</asp:ListItem>
                                        <asp:ListItem Value='2'>2</asp:ListItem>
                                        <asp:ListItem Value='3'>3</asp:ListItem>
                                        <asp:ListItem Value='4'>4</asp:ListItem>
                                        <asp:ListItem Value='5'>5</asp:ListItem>
                                        <asp:ListItem Value='6'>6</asp:ListItem>
                                        <asp:ListItem Value='7'>7</asp:ListItem>
                                        <asp:ListItem Value='8'>8</asp:ListItem>
                                        <asp:ListItem Value='9'>9</asp:ListItem>
                                        <asp:ListItem Value='10'>10</asp:ListItem>
                                        <asp:ListItem Value='11'>11</asp:ListItem>
                                        <asp:ListItem Value='12'>12</asp:ListItem>
                                    </asp:DropDownList>
                                    <br>
                                    <asp:RequiredFieldValidator ID="rfvNO_OF_PAYMENTS" runat="server" ControlToValidate="cmbNO_OF_PAYMENTS"
                                        ErrorMessage="NO_OF_PAYMENTS can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="33%">
                                <asp:Label ID="capINTREST_RATES" runat="server">Intrest Rate</asp:Label><span class="Mandatory">*</span>
                                </br><asp:TextBox ID="txtINTREST_RATES" runat="server" MaxLength="8"></asp:TextBox></br>
                                <asp:RequiredFieldValidator ID="rfvINTREST_RATES" runat="server" ControlToValidate="txtINTREST_RATES"
                                    ErrorMessage="Intrest rates can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                                
                                <asp:RegularExpressionValidator runat="server" ID="revINTREST_RATES" ErrorMessage="" Display="Dynamic" ControlToValidate="txtINTREST_RATES"></asp:RegularExpressionValidator>
                                
                                <asp:CustomValidator ID="csvINTREST_RATES" runat="server" ControlToValidate="txtINTREST_RATES" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimit"></asp:CustomValidator>	
                             <%--   <asp:RangeValidator ID="rvalidator3" ControlToValidate="txtINTREST_RATES" runat="server"
                                    MaximumValue="100.0000" MinimumValue="0.0000"  ErrorMessage="The value must be in range"
                                    Display="Dynamic"></asp:RangeValidator>--%><%--Type="Double"--%>
                            </td>
                            <td class="midcolora" width="33%">
                                <asp:Label ID="capBASE_DATE_DOWNPAYMENT" runat="server">Base Date for Downpayment</asp:Label>
                                </br>
                                <asp:DropDownList ID="cmbBASE_DATE_DOWNPAYMENT" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="midcolora" width="33%">
                                <asp:Label ID="capDUE_DAYS_DOWNPYMT" runat="server">Number of Due Days For DownPayment</asp:Label><span
                                    class="mandatory">*</span> </br>
                                <asp:TextBox ID="txtDUE_DAYS_DOWNPYMT" runat="server" MaxLength="2" OnTextChanged="txtNUMBER_OF_DUE_DAYS_FOR_DOWNPAYMENT_TextChanged"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="rfvDUE_DAYS_DOWNPYMT" runat="server" ControlToValidate="txtDUE_DAYS_DOWNPYMT"
                                    ErrorMessage="Please enter no. of due days for down payment" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="rvalidator1" runat="server" ControlToValidate="txtDUE_DAYS_DOWNPYMT"
                                    MinimumValue="0" MaximumValue="30" Type="Integer" ErrorMessage="Enter between 0 to 30"
                                    Display="Dynamic"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="33%">
                                <asp:Label ID="capBDATE_INSTALL_NXT_DOWNPYMT" runat="server">Base Date for installments next to DownPayment</asp:Label>
                                </br><asp:DropDownList ID="cmbBDATE_INSTALL_NXT_DOWNPYMT" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="midcolora" width="33%">
                                <asp:Label ID="capDAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT" runat="server"> No. of Days Left For Due Payment next to downpayment</asp:Label><span
                                    class="mandatory">*</span> </br><asp:TextBox ID="txtDAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT"
                                        runat="server" MaxLength="2"></asp:TextBox></br>
                                <asp:RequiredFieldValidator ID="rfvDAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT" runat="server"
                                    ControlToValidate="txtDAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT" ErrorMessage="No. of Days Left For Due Payment next to downpayment"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="rvalidator2" runat="server" ControlToValidate="txtDAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT"
                                    MinimumValue="0" MaximumValue="30" Type="Integer" ErrorMessage="Enter between 0 to 30"
                                    Display="Dynamic"></asp:RangeValidator>
                            </td>
                            <td class="midcolora" width="33%">
                                <asp:Label ID="capDAYS_SUBSEQUENT_INSTALLMENTS" runat="server">Days Between Subsequent Installments</asp:Label>
                                </br><asp:TextBox ID="txtDAYS_SUBSEQUENT_INSTALLMENTS" runat="server" value="1" Width="43px"
                                    MaxLength="2"></asp:TextBox>&nbsp;&nbsp;
                                <asp:DropDownList ID="cmbSUBSEQUENT_INSTALLMENTS_OPTION" runat="server">
                                </asp:DropDownList>
                                <br />
                                <asp:CustomValidator ID="csvSUBSEQUENT_INSTALLMENTS" runat="server" ControlToValidate="txtDAYS_SUBSEQUENT_INSTALLMENTS"
                                    ClientValidationFunction="ValidateInstallment"></asp:CustomValidator>
                                <asp:CustomValidator ID="csvSUBSEQUENT_INSTALLMENTS_OPTION" runat="server" ControlToValidate="cmbSUBSEQUENT_INSTALLMENTS_OPTION"
                                    ClientValidationFunction="ValidateInstallment"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora">
                            </td>
                            <td class="midcolora">
                            </td>
                            <td class="midcolora">
                                <asp:Label ID="capDEFAULT_PLAN" runat="server">Default Plan</asp:Label><asp:CheckBox
                                    ID="chkDEFAULT_PLAN" runat="server"></asp:CheckBox>
                            </td>
                        </tr>
                        <!--- end  -->
                        <asp:Panel ID="PlanBody" runat="server">
                            <%--<tr>
                                <td class="midcolora" width="18%">
                                    <asp:Label ID="capPLAN_TYPE" runat="server">Plan Type</asp:Label>
                                </td>
                                <td class="midcolora" width="32%">
                                    <asp:Label ID="lblPLAN_TYPE" runat="server" CssClass="LabelFont" size="30" Text="Monthly"></asp:Label>
                                </td>
                                <td class="midcolora" width="18%">
                                    <asp:Label ID="capMONTHS_BETWEEN" runat="server">Months Between Payments</asp:Label><span
                                        class="mandatory">*</span>
                                </td>
                                <td class="midcolora" width="32%">
                                    <asp:TextBox ID="txtMONTHS_BETWEEN" runat="server" size="2" MaxLength="2"></asp:TextBox><br>
                                    <asp:RequiredFieldValidator ID="rfvMONTHS_BETWEEN" runat="server" ControlToValidate="txtMONTHS_BETWEEN"
                                        ErrorMessage="MONTHS_BETWEEN can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revMONTHS_BETWEEN" runat="server" ControlToValidate="txtMONTHS_BETWEEN"
                                        ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                    <asp:RangeValidator ID="rngMONTHS_BETWEEN" runat="server" ControlToValidate="txtMONTHS_BETWEEN"
                                        ErrorMessage="RegularExpressionValidator" Display="Dynamic" MinimumValue="1"
                                        MaximumValue="12" Type="Integer"></asp:RangeValidator>
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="headereffectSystemParams" colspan="4">
                                    <asp:Label ID='lblNEW' runat="server"> Monthly Breakdown for New Business </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="midcolora">
                                    <asp:Label ID="lblPERCENT_BREAKDOWN" runat="server">Percent Breakdown For Each Month For New Business Process : </asp:Label><span
                                        class="mandatory">*</span>
                                </td>
                                <td colspan="2">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="midcolora" width="4%">
                                                <span id="spnPERCENT_BREAKDOWN1">1.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWN1" runat="server" CssClass="inputcurrency" size="8"
                                                    MaxLength="7" onchange="CalculateTotal();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWN1" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN1"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWN1" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN1"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora" width="4%">
                                                <span id="spnPERCENT_BREAKDOWN2">2.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWN2" runat="server" CssClass="inputcurrency" size="8"
                                                    MaxLength="7" onchange="CalculateTotal();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWN2" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN2"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWN2" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN2"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora" width="4%">
                                                <span id="spnPERCENT_BREAKDOWN3">3.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWN3" runat="server" CssClass="inputcurrency" size="8"
                                                    MaxLength="7" onchange="CalculateTotal();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWN3" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN3"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWN3" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN3"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora" width="4%">
                                                <span id="spnPERCENT_BREAKDOWN4">4.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWN4" runat="server" CssClass="inputcurrency" size="8"
                                                    MaxLength="7" onchange="CalculateTotal();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWN4" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN4"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWN4" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN4"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora" width="4%">
                                                <span id="spnPERCENT_BREAKDOWN5">5.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWN5" runat="server" CssClass="inputcurrency" size="8"
                                                    MaxLength="7" onchange="CalculateTotal();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWN5" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN5"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWN5" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN5"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora" width="4%">
                                                <span id="spnPERCENT_BREAKDOWN6">6.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWN6" runat="server" CssClass="inputcurrency" size="8"
                                                    MaxLength="7" onchange="CalculateTotal();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWN6" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN6"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWN6" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN6"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora">
                                                <span id="spnPERCENT_BREAKDOWN7">7.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWN7" runat="server" CssClass="inputcurrency" size="8"
                                                    MaxLength="7" onchange="CalculateTotal();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWN7" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN7"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWN7" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN7"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora">
                                                <span id="spnPERCENT_BREAKDOWN8">8.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWN8" runat="server" CssClass="inputcurrency" size="8"
                                                    MaxLength="7" onchange="CalculateTotal();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWN8" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN8"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWN8" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN8"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora">
                                                <span id="spnPERCENT_BREAKDOWN9">9.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWN9" runat="server" CssClass="inputcurrency" size="8"
                                                    onchange="CalculateTotal();" MaxLength="7"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWN9" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN9"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWN9" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN9"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora">
                                                <span id="spnPERCENT_BREAKDOWN10">10.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWN10" runat="server" CssClass="inputcurrency"
                                                    size="8" MaxLength="7" onchange="CalculateTotal();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWN10" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN10"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWN10" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN10"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora">
                                                <span id="spnPERCENT_BREAKDOWN11">11.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWN11" runat="server" CssClass="inputcurrency"
                                                    size="8" MaxLength="7" onchange="CalculateTotal();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWN11" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN11"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWN11" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN11"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora">
                                                <span id="spnPERCENT_BREAKDOWN12">12.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWN12" runat="server" CssClass="inputcurrency"
                                                    size="8" MaxLength="7" onchange="CalculateTotal();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWN12" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN12"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWN12" runat="server" ControlToValidate="txtPERCENT_BREAKDOWN12"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="midcolora">
                                    <span id="spanTotal_For_New_Business_Process" runat="server">Total For Renewal Process
                                        :</span>
                                    <br />
                                    <asp:Label class="Labelfont" ID="spnTotalAmount" runat="server"></asp:Label>
                                </td>
                                <td class="midcolora" width="33%">
                                    <asp:Label ID="capDOWN_PAYMENT_PLAN" runat="server">Down Payment Plan</asp:Label><span
                                        class="mandatory">*</span>
                                    <br></br>
                                    <asp:DropDownList ID="cmbDOWN_PAYMENT_PLAN" runat="server">
                                        <asp:ListItem> </asp:ListItem>
                                        <asp:ListItem Value="0">0+N</asp:ListItem>
                                        <asp:ListItem Value="1">1+N</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <asp:RequiredFieldValidator ID="RfvDOWN_PAYMENT_PLAN" runat="server" ControlToValidate="cmbDOWN_PAYMENT_PLAN"
                                        ErrorMessage="Please select Down Payment Plan" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td class="midcolora" width="33%">
                                    <asp:Label ID="capMODE_OF_DOWNPAY" runat="server">Mode of Down Payment</asp:Label><span
                                        class="mandatory">*</span><br></br>
                                    <asp:ListBox ID="cmbMODE_OF_DOWNPAY" runat="server" SelectionMode="Multiple" Height="45px">
                                    </asp:ListBox>
                                    <br>
                                    <asp:RequiredFieldValidator ID="rfvMODE_OF_DOWNPAY" runat="server" ControlToValidate="cmbMODE_OF_DOWNPAY"
                                        ErrorMessage="Please select at least one option" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <!-- DUMMY FIELDS FOR DEMO SAKE  :: Added by Swastika /Completed by Pravesh on 30 nov 2006 -->
                            <%--<tr>
                <td class="midcolora" width="18%">
                    <asp:Label ID="capNO_INS_DOWNPAY" runat="server">No. of Installments in Down Payment</asp:Label><span
                        class="mandatory">*</span>
                </td>
                <td class="midcolora" width="32%">
                    <asp:TextBox ID="txtNO_INS_DOWNPAY" MaxLength="2" runat="server" Width="40px"></asp:TextBox><br>
                    <asp:RegularExpressionValidator ID="revNO_INS_DOWNPAY" runat="server" ControlToValidate="txtNO_INS_DOWNPAY"
                        ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvNO_INS_DOWNPAY" runat="server" ControlToValidate="txtNO_INS_DOWNPAY"
                        ErrorMessage="can't be blank" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td class="midcolora" width="18%">
                   
                </td>
                <td class="midcolora" width="32%">
                   
                   
                </td>
                </td>
            </tr>--%>
            
           
                            <tr>
                                <td class="headereffectSystemParams" colspan="4">
                                    <asp:Label ID='Label3' runat="server"> Monthly Breakdown for Renewal</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="midcolora" colspan="3">
                                    <asp:CheckBox ID="chkCopy" onclick="CopyNBStoRP();" runat="server"></asp:CheckBox>
                                    <asp:Label ID="Label2" runat="server">Copy New Business Process Plan to Renewal Process</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="midcolora">
                                    <asp:Label ID="Label1" runat="server">Percent Breakdown For Each Month For Renewal Process : </asp:Label><span
                                        class="mandatory">*</span>
                                </td>
                                <td colspan="2">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="midcolora" width="4%">
                                                <span id="spnPERCENT_BREAKDOWNRP1">1.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWNRP1" runat="server" CssClass="inputcurrency"
                                                    size="8" MaxLength="7" onchange="CalculateTotalRP();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWNRP1" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP1"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWNRP1" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP1"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora" width="4%">
                                                <span id="spnPERCENT_BREAKDOWNRP2">2.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWNRP2" runat="server" CssClass="inputcurrency"
                                                    size="8" MaxLength="7" onchange="CalculateTotalRP();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWNRP2" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP2"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWNRP2" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP2"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora" width="4%">
                                                <span id="spnPERCENT_BREAKDOWNRP3">3.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWNRP3" runat="server" CssClass="inputcurrency"
                                                    size="8" MaxLength="7" onchange="CalculateTotalRP();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWNRP3" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP3"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWNRP3" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP3"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora" width="4%">
                                                <span id="spnPERCENT_BREAKDOWNRP4">4.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWNRP4" runat="server" CssClass="inputcurrency"
                                                    size="8" MaxLength="7" onchange="CalculateTotalRP();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWNRP4" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP4"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWNRP4" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP4"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora" width="4%">
                                                <span id="spnPERCENT_BREAKDOWNRP5">5.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWNRP5" runat="server" CssClass="inputcurrency"
                                                    size="8" MaxLength="7" onchange="CalculateTotalRP();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWNRP5" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP5"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWNRP5" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP5"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora" width="4%">
                                                <span id="spnPERCENT_BREAKDOWNRP6">6.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWNRP6" runat="server" CssClass="inputcurrency"
                                                    size="8" MaxLength="7" onchange="CalculateTotalRP();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWNRP6" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP6"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWNRP6" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP6"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora">
                                                <span id="spnPERCENT_BREAKDOWNRP7">7.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWNRP7" runat="server" CssClass="inputcurrency"
                                                    size="8" MaxLength="7" onchange="CalculateTotalRP();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWNRP7" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP7"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWNRP7" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP7"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora">
                                                <span id="spnPERCENT_BREAKDOWNRP8">8.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWNRP8" runat="server" CssClass="inputcurrency"
                                                    size="8" MaxLength="7" onchange="CalculateTotalRP();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWNRP8" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP8"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWNRP8" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP8"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora">
                                                <span id="spnPERCENT_BREAKDOWNRP9">9.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWNRP9" runat="server" CssClass="inputcurrency"
                                                    size="8" onchange="CalculateTotal();" MaxLength="7"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWNRP9" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP9"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWNRP9" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP9"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora">
                                                <span id="spnPERCENT_BREAKDOWNRP10">10.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWNRP10" runat="server" CssClass="inputcurrency"
                                                    size="8" MaxLength="7" onchange="CalculateTotalRP();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWNRP10" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP10"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWNRP10" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP10"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora">
                                                <span id="spnPERCENT_BREAKDOWNRP11">11.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWNRP11" runat="server" CssClass="inputcurrency"
                                                    size="8" MaxLength="7" onchange="CalculateTotalRP();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWNRP11" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP11"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWNRP11" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP11"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="midcolora">
                                                <span id="spnPERCENT_BREAKDOWNRP12">12.</span>
                                            </td>
                                            <td class="midcolora" width="12%">
                                                <asp:TextBox ID="txtPERCENT_BREAKDOWNRP12" runat="server" CssClass="inputcurrency"
                                                    size="8" MaxLength="7" onchange="CalculateTotalRP();"></asp:TextBox><br>
                                                <asp:RegularExpressionValidator ID="revPERCENT_BREAKDOWNRP12" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP12"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPERCENT_BREAKDOWNRP12" runat="server" ControlToValidate="txtPERCENT_BREAKDOWNRP12"
                                                    ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="midcolora">
                                    <span id="spanTotal_For_Renewal_Process" runat="server">Total For Renewal Process :</span><br>
                                    </br>
                                    <asp:Label class="Labelfont" ID="spnTotalAmountRP" runat="server"></asp:Label>
                                </td>
                                <td class="midcolora" width="18%">
                                    <asp:Label ID="capDOWN_PAYMENT_PLAN_RENEWAL" runat="server">Down Payment Plan</asp:Label><span
                                        class="mandatory">*</span>
                                    <br></br>
                                    <asp:DropDownList ID="cmbDOWN_PAYMENT_PLAN_RENEWAL" runat="server">
                                        <asp:ListItem> </asp:ListItem>
                                        <asp:ListItem Value="0">0+N</asp:ListItem>
                                        <asp:ListItem Value="1">1+N</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <asp:RequiredFieldValidator ID="RfvDOWN_PAYMENT_PLAN_RENEWAL" runat="server" ControlToValidate="cmbDOWN_PAYMENT_PLAN_RENEWAL"
                                        ErrorMessage="" Display="Dynamic"></asp:RequiredFieldValidator><%--Please select Down Payment Plan Renewal --%>
                                </td>
                                <td class="midcolora" width="18%" style="height: 55px">
                                    <asp:Label ID="capMODE_OF_DOWNPAY_RENEW" runat="server">Mode of Down Payment</asp:Label><span
                                        class="mandatory">*</span><br></br>
                                    <asp:ListBox ID="cmbMODE_OF_DOWNPAY_RENEW" runat="server" SelectionMode="Multiple"
                                        Height="45px"></asp:ListBox>
                                    <br>
                                    <asp:RequiredFieldValidator ID="rfvMODE_OF_DOWNPAY_RENEW" runat="server" ControlToValidate="cmbMODE_OF_DOWNPAY_RENEW"
                                        ErrorMessage="Please select at least one option" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <!-- DUMMY FIELDS FOR DEMO SAKE  :: Added by Swastika  /Implemented by Pravesh on 30 nov 2006-->
                            <%--<tr>
                <td class="midcolora" width="18%">
                    <asp:Label ID="capNO_INS_DOWNPAY_RENEW" runat="server">No. of Installments in Down Payment</asp:Label><span
                        class="mandatory">*</span>
                </td>
                <td class="midcolora" width="32%">
                    <asp:TextBox ID="txtNO_INS_DOWNPAY_RENEW" MaxLength="2" runat="server" Width="40px"></asp:TextBox><br>
                    <asp:RegularExpressionValidator ID="revNO_INS_DOWNPAY_RENEW" runat="server" ControlToValidate="txtNO_INS_DOWNPAY_RENEW"
                        ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvNO_INS_DOWNPAY_RENEW" runat="server" ControlToValidate="txtNO_INS_DOWNPAY_RENEW"
                        ErrorMessage="can't be blank" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td class="midcolora" width="18%" style="height: 55px">
                </td>
                <td class="midcolora" width="18%" style="height: 55px">
                    <br>
                </td>
                </TD>
            </tr>--%>
                        </asp:Panel>
                        <tr>
                            <td class="headereffectSystemParams" colspan="4">
                              <asp:Label ID="capHead" runat="server"></asp:Label>   <%--Accounting Parameters--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td class="midcolora" width="33%">
                                                <asp:Label ID="capINSTALLMENT_FEES" runat="server">Fees</asp:Label><span class="mandatory">*</span><br />
                                                <asp:TextBox class="InputCurrency" ID="txtINSTALLMENT_FEES" runat="server" size="6"
                                                    MaxLength="6" Style="text-align: Right"></asp:TextBox><br>
                                                <asp:RequiredFieldValidator ID="rfvINSTALLMENT_FEES" runat="server" ControlToValidate="txtINSTALLMENT_FEES"
                                                    ErrorMessage="INSTALLMENT_FEES can't be blank." Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                        ID="revINSTALLMENT_FEES" runat="server" ControlToValidate="txtINSTALLMENT_FEES"
                                                        Display="Dynamic"></asp:RegularExpressionValidator>
                                            </td>
                                            <td class="midcolora" width="33%">
                                                <asp:Label ID="capNON_SUFFICIENT_FUND_FEES" runat="server">Sufficient</asp:Label><span
                                                    class="mandatory">*</span><br />
                                                <asp:TextBox class="InputCurrency" Style="text-align: Right" ID="txtNON_SUFFICIENT_FUND_FEES"
                                                    runat="server" size="6" MaxLength="6"></asp:TextBox><br>
                                                <asp:RequiredFieldValidator ID="rfvNON_SUFFICIENT_FUND_FEES" runat="server" ControlToValidate="txtNON_SUFFICIENT_FUND_FEES"
                                                    ErrorMessage="NON_SUFFICIENT_FUND_FEES can't be blank." Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                        ID="revNON_SUFFICIENT_FUND_FEES" runat="server" ControlToValidate="txtNON_SUFFICIENT_FUND_FEES"
                                                        Display="Dynamic"></asp:RegularExpressionValidator>
                                            </td>
                                            <td class="midcolora" width="33%">
                                                <asp:Label ID="capREINSTATEMENT_FEES" runat="server">Fees</asp:Label><span class="mandatory">*</span><br />
                                                <asp:TextBox Style="text-align: Right" class="InputCurrency" ID="txtREINSTATEMENT_FEES"
                                                    runat="server" size="6" MaxLength="6"></asp:TextBox><br>
                                                <asp:RequiredFieldValidator ID="rfvREINSTATEMENT_FEES" runat="server" ControlToValidate="txtREINSTATEMENT_FEES"
                                                    ErrorMessage="REINSTATEMENT_FEES can't be blank." Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                        ID="revREINSTATEMENT_FEES" runat="server" ControlToValidate="txtREINSTATEMENT_FEES"
                                                        Display="Dynamic"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" width="33%">
                                                <asp:Label ID="capLATE_FEES" runat="server">Late Fees</asp:Label><span class="mandatory">*</span><br />
                                                <asp:TextBox Style="text-align: Right" class="InputCurrency" ID="txtLATE_FEES" runat="server"
                                                    size="6" MaxLength="6"></asp:TextBox><br>
                                                <asp:RequiredFieldValidator ID="rfvLATE_FEES" runat="server" ControlToValidate="txtLATE_FEES"
                                                    Display="Dynamic"></asp:RequiredFieldValidator><br>
                                                <asp:RegularExpressionValidator ID="revLATE_FEES" runat="server" ControlToValidate="txtLATE_FEES"
                                                    Display="Dynamic"></asp:RegularExpressionValidator>
                                            </td>
                                            <td class="midcolora" width="33%">
                                                <asp:Label ID="capAMTUNDER_PAYMENT" runat="server">Payment</asp:Label><span class="mandatory">*</span><br />
                                                <asp:TextBox class="InputCurrency" Style="text-align: Right" ID="txtAMTUNDER_PAYMENT"
                                                    runat="server" size="6" MaxLength="3"></asp:TextBox><br>
                                                <asp:RequiredFieldValidator ID="rfvAMTUNDER_PAYMENT" runat="server" ControlToValidate="txtAMTUNDER_PAYMENT"
                                                    ErrorMessage="AMTUNDER_PAYMENT can't be blank." Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                        ID="revAMTUNDER_PAYMENT" runat="server" ControlToValidate="txtAMTUNDER_PAYMENT"
                                                        Display="Dynamic"></asp:RegularExpressionValidator><asp:RangeValidator ID="rngAMTUNDER_PAYMENT"
                                                            ControlToValidate="txtAMTUNDER_PAYMENT" ErrorMessage="Percentage should lie between 0-100"
                                                            Display="Dynamic" runat="server" MinimumValue="0" MaximumValue="100" Type="Currency"></asp:RangeValidator>
                                            </td>
                                            <td class="midcolora" width="33%">
                                                <asp:Label ID="capMIN_INSTALLMENT_AMOUNT" runat="server">Billing</asp:Label><span
                                                    class="mandatory">*</span><br />
                                                <asp:TextBox class="InputCurrency" ID="txtMIN_INSTALLMENT_AMOUNT" Style="text-align: Right"
                                                    runat="server" size="5" MaxLength="4"></asp:TextBox><br>
                                                <asp:RequiredFieldValidator ID="rfvMIN_INSTALLMENT_AMOUNT" runat="server" ControlToValidate="txtMIN_INSTALLMENT_AMOUNT"
                                                    ErrorMessage="MIN_INSTALLMENT_AMOUNT can't be blank." Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                        ID="revMIN_INSTALLMENT_AMOUNT" runat="server" ControlToValidate="txtMIN_INSTALLMENT_AMOUNT"
                                                        Display="Dynamic"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="midcolora" width="33%">
                                                <asp:Label ID="capPAST_DUE_RENEW" runat="server">Percentage for Past Due at Renewal</asp:Label><span
                                                    class="mandatory">*</span><br />
                                                <asp:TextBox ID="txtPAST_DUE_RENEW" runat="server" size="6" MaxLength="3" CssClass="InputCurrency"
                                                    Style="text-align: Right"></asp:TextBox><br>
                                                <asp:RequiredFieldValidator ID="rfvPAST_DUE_RENEW" runat="server" ControlToValidate="txtPAST_DUE_RENEW"
                                                    ErrorMessage="PAST_DUE_RENEW can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revPAST_DUE_RENEW" runat="server" ControlToValidate="txtPAST_DUE_RENEW"
                                                    ErrorMessage="Please enter numeric value." Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:RangeValidator ID="rngPAST_DUE_RENEW" runat="server" ControlToValidate="txtPAST_DUE_RENEW"
                                                    ErrorMessage="Percentage should lie between 0-100" Display="Dynamic" MinimumValue="0"
                                                    MaximumValue="100" Type="Integer"></asp:RangeValidator>
                                            </td>
                                            <td class="midcolora" width="33%">
                                            </td>
                                            <td class="midcolora" width="33%">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="headereffectSystemParams" colspan="4">
                               <asp:Label ID="capCanc" runat="server"></asp:Label> <%--Cancellation Parameters--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td class="midcolora" width="32%">
                                                <asp:Label ID="capMINDAYS_CANCEL" runat="server">Cancel</asp:Label><span class="mandatory">*</span><br />
                                                <asp:TextBox ID="txtMINDAYS_CANCEL" runat="server" size="6" MaxLength="2"></asp:TextBox><br>
                                                <asp:RequiredFieldValidator ID="rfvMINDAYS_CANCEL" runat="server" ControlToValidate="txtMINDAYS_CANCEL"
                                                    ErrorMessage="MINDAYS_CANCEL can't be blank." Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                        ID="revMINDAYS_CANCEL" runat="server" ControlToValidate="txtMINDAYS_CANCEL" ErrorMessage="Please enter numeric data"
                                                        Display="Dynamic"></asp:RegularExpressionValidator>
                                            </td>
                                            <td class="midcolora" width="32%">
                                                <asp:Label ID="capPOST_PHONE" runat="server">Phone</asp:Label><span class="mandatory">*</span><br />
                                                <asp:TextBox ID="txtPOST_PHONE" runat="server" size="6" MaxLength="2"></asp:TextBox><br>
                                                <asp:RequiredFieldValidator ID="rfvPOST_PHONE" runat="server" ControlToValidate="txtPOST_PHONE"
                                                    ErrorMessage="POST_PHONE can't be blank." Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                        ID="revPOST_PHONE" runat="server" ControlToValidate="txtPOST_PHONE" Display="Dynamic"></asp:RegularExpressionValidator>
                                            </td>
                                            <td class="midcolora" width="32%">
                                                <asp:Label ID="capPOST_CANCEL" runat="server">Cancel</asp:Label><span class="mandatory">*</span><br />
                                                <asp:TextBox ID="txtPOST_CANCEL" runat="server" size="6" MaxLength="2"></asp:TextBox><br>
                                                <asp:RequiredFieldValidator ID="rfvPOST_CANCEL" runat="server" ControlToValidate="txtPOST_CANCEL"
                                                    ErrorMessage="POST_CANCEL can't be blank." Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                        ID="revPOST_CANCEL" runat="server" ControlToValidate="txtPOST_CANCEL" Display="Dynamic"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capGRACE_PERIOD" runat="server">Grace Period Cancellation Notice</asp:Label><span
                                    class="mandatory">*</span><br />
                                <asp:TextBox ID="txtGRACE_PERIOD" runat="server" size="6" MaxLength="2"></asp:TextBox><br>
                                <asp:RequiredFieldValidator ID="rfvGRACE_PERIOD" runat="server" ControlToValidate="txtGRACE_PERIOD"
                                    ErrorMessage="Grace Period can't be blank." Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                        ID="revGRACE_PERIOD" runat="server" ControlToValidate="txtGRACE_PERIOD" Display="Dynamic"></asp:RegularExpressionValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capMINDAYS_PREMIUM" runat="server">Premium</asp:Label><span class="mandatory">*</span><br />
                                <asp:TextBox ID="txtMINDAYS_PREMIUM" runat="server" size="6" MaxLength="2"></asp:TextBox><br>
                                <asp:RequiredFieldValidator ID="rfvMINDAYS_PREMIUM" runat="server" ControlToValidate="txtMINDAYS_PREMIUM"
                                    ErrorMessage="MINDAYS_PREMIUM can't be blank." Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                        ID="revMINDAYS_PREMIUM" runat="server" ControlToValidate="txtMINDAYS_PREMIUM"
                                        Display="Dynamic"></asp:RegularExpressionValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capRECVE_NOTIFICATION_DOC" runat="server">When to generate receivable notification document</asp:Label><br />
                                <asp:DropDownList ID="cmbRECVE_NOTIFICATION_DOC" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <%-- <tr>
                        <td class="midcolora" width="32%">
                            <asp:Label ID="capDAYS_DUE_PREM_NOTICE_PRINTD" runat="server">Number of days in advance the due date premium notice will be printed</asp:Label>
                            <span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="18%" colspan="3">
                            <asp:TextBox ID="txtDAYS_DUE_PREM_NOTICE_PRINTD" runat="server" size="6" MaxLength="2"></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvDAYS_DUE_PREM_NOTICE_PRINTD" runat="server" ControlToValidate="txtDAYS_DUE_PREM_NOTICE_PRINTD"
                                ErrorMessage="DAYS_DUE_PREM_NOTICE_PRINTD can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revDAYS_DUE_PREM_NOTICE_PRINTD" runat="server"
                                ControlToValidate="txtDAYS_DUE_PREM_NOTICE_PRINTD" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                    </tr>--%>
                        <tr>
                            <td class="midcolora" colspan="2">
                                <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" Text="Reset"></cmsb:CmsButton><cmsb:CmsButton
                                    class="clsButton" ID="btnActivateDeactivate" runat="server" Text="">
                                </cmsb:CmsButton>
                            </td>
                            <td class="midcolorr" colspan="2">
                                <cmsb:CmsButton class="clsButton" ID="btnSave" runat="server" Text="Save"></cmsb:CmsButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
    <%-- <tr>
        <td class="headereffectSystemParams" colspan="4">
            Fees applicable to this plan
        </td>
    </tr>--%>
    <!--
										<tr>
											<TD class="midcolora" width="32%"><asp:label id="capSERVICE_CHARGE" runat="server">Service Charge</asp:label></TD>
											<TD class="midcolora" width="18%"><asp:textbox class="InputCurrency" id="txtSERVICE_CHARGE" runat="server" maxlength="6" size="6"></asp:textbox><BR>
												</TD>
											<TD class="midcolora" width="32%"><asp:label id="capCONVENIENCE_FEES" runat="server">Convenience Fees</asp:label></TD>
											<TD class="midcolora" width="18%"><asp:textbox class="InputCurrency" id="txtCONVENIENCE_FEES" runat="server" maxlength="6" size="6"></asp:textbox><BR>
												</TD>
										</tr>  -->
    <%-- </table> </td> </tr>
   
    </TBODY></TABLE></TD></TR></TBODY></TABLE>--%>
    <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
    <input id="hidOldData" type="hidden" name="hidOldData" runat="server">
    <input id="hidIDEN_PLAN_ID" type="hidden" value="0" name="hidIDEN_PLAN_ID" runat="server">
    <input id="hidIS_ACTIVE" type="hidden" runat="server" value="0">
    <input id="hidSYSTEM_GENERATED_FULL_PAY" type="hidden">
    <input id="hidrecalrenewalprocess" type="hidden" value="abc" />
    <input id="hidrecalbusinessprocess" type="hidden" value="abc" />
    <input id="hidAny" type="hidden" runat="server" /> 
    </form>

    <script>
        RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidIDEN_PLAN_ID').value, false);
    </script>

</body>
</html>

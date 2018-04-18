<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>

<%@ Page Language="c#" CodeBehind="AddCustomer.aspx.cs" AutoEventWireup="false" Inherits="Cms.Client.Aspx.AddCustomer"
    ValidateRequest="false" %>

<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<html>
<head>
    <title>CLT_CUSTOMER_LIST</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet">

    <script src="/cms/cmsweb/scripts/xmldom.js"></script>
    <script src="/cms/cmsweb/scripts/common.js"></script>
    <script src="/cms/cmsweb/scripts/form.js"></script>
    <script src="/cms/cmsweb/scripts/calendar.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script> 
	<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
	<script type="text/javascript" src="/cms/cmsweb/scripts/CmsHelpScript/jQueryPageHelpFile.js"></script> 
    <script language="javascript">
        var jsaAppDtFormat = "<%=aAppDtFormat %>";

        //This function is used to refresh the contents of client top controls
        function InsuranceScoreChange() {
            //			if(document.getElementById("txtCUSTOMER_INSURANCE_SCORE").value!="" && !isNaN(document.getElementById("txtCUSTOMER_INSURANCE_SCORE").value))
            //				document.getElementById("txtCUSTOMER_INSURANCE_RECEIVED_DATE").readOnly = false;
            //			else
            //				document.getElementById("txtCUSTOMER_INSURANCE_RECEIVED_DATE").readOnly =false // true;
        }







        function trim(stringToTrim) {
            return stringToTrim.replace(/^\s+|\s+$/g, "");
        }
        function RefreshClientTop() {

            var doc = this.parent.document;
            if (doc.getElementById("cltClientTop_PanelClient") == null) {
                if (document.getElementById("hidRefreshTabIndex").value == 'Y') {
                    var strQueryString = "CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value + "&SaveMsg=" + document.getElementById("hidSaveMsg").value;
                    parent.document.location.href = "CustomerTabIndex.aspx?" + strQueryString; trMAIN_POSITION
                    document.getElementById("trMAIN_POSITION").style.display = 'none';
                }
                return;
            }
            if (document.getElementById("cmbCUSTOMER_TYPE").value == "11109") { //  for commercial customer
                doc.getElementById("cltClientTop_lblFullName").innerHTML = document.getElementById("txtCUSTOMER_FIRST_NAME").value;
                document.getElementById("trMAIN_POSITION").style.display = 'none';
            }
            else if (document.getElementById("cmbCUSTOMER_TYPE").value == "11110") { //for personal
                var Fname = document.getElementById("txtCUSTOMER_FIRST_NAME").value;
                var MName = document.getElementById("txtCUSTOMER_MIDDLE_NAME").value
                var Lname = document.getElementById("txtCUSTOMER_LAST_NAME").value;
                var Name = trim(Fname + " " + MName + " ") + " " + Lname;
                doc.getElementById("cltClientTop_lblFullName").innerHTML = Name;
                document.getElementById("trMAIN_POSITION").style.display = 'none';

            }   ///  Added by lalit on 11 May,2010

            if (document.getElementById("txtCUSTOMER_SUFFIX").value != '')
                doc.getElementById("cltClientTop_lblFullName").innerHTML += ' ' + document.getElementById("txtCUSTOMER_SUFFIX").value;

            doc.getElementById("cltClientTop_lblClientType").innerHTML = document.getElementById("cmbCUSTOMER_TYPE").options[document.getElementById("cmbCUSTOMER_TYPE").selectedIndex].text;

            doc.getElementById("cltClientTop_lblClientStatus").innerHTML = document.getElementById("lblIS_ACTIVE").innerHTML;

            if (document.getElementById("cmbCUSTOMER_TYPE").options[document.getElementById("cmbCUSTOMER_TYPE").selectedIndex].value == "11109" || document.getElementById("cmbCUSTOMER_TYPE").options[document.getElementById("cmbCUSTOMER_TYPE").selectedIndex].value == "14725")
                doc.getElementById("cltClientTop_lblClientPhone").innerHTML = document.getElementById("txtCUSTOMER_BUSINESS_PHONE").value;
            else
                doc.getElementById("cltClientTop_lblClientPhone").innerHTML = document.getElementById("txtCUSTOMER_HOME_PHONE").value;
            if (document.getElementById("cmbPREFIX").value != '') {
                if (document.getElementById("cmbPREFIX").options[document.getElementById("cmbPREFIX").selectedIndex].text != null) {
                    doc.getElementById("cltClientTop_lblClientTitle").innerHTML = document.getElementById("cmbPREFIX").options[document.getElementById("cmbPREFIX").selectedIndex].text;
                }
            }
            var add = "";
            if (document.getElementById("txtCUSTOMER_ADDRESS1").value != "") {
                add = document.getElementById("txtCUSTOMER_ADDRESS1").value
            }

            if (document.getElementById("txtCUSTOMER_ADDRESS1").value != "") {
                if (document.getElementById("txtNUMBER").value != "") {
                    add += ", "
                }

            }


            add += document.getElementById("txtNUMBER").value + " "
            if (document.getElementById("txtCUSTOMER_ADDRESS2").value != '') {
                add += document.getElementById("txtCUSTOMER_ADDRESS2").value
            }

            if (document.getElementById("txtDISTRICT").value != '') {
                add += " - "
            }

            if (document.getElementById("txtDISTRICT").value != "") {
                add += document.getElementById("txtDISTRICT").value
            }

            if (document.getElementById("txtCUSTOMER_CITY").value != "") {
                add += " - "
            }

            if (document.getElementById("txtCUSTOMER_CITY").value != "") {
                add += document.getElementById("txtCUSTOMER_CITY").value
            }

            if (document.getElementById("cmbCUSTOMER_STATE").value != "") {
                if (document.getElementById("txtCUSTOMER_CITY").value != "") {
                    add += "/"
                }
                else {
                    add += " - "
                }
            }
            if (document.getElementById("cmbCUSTOMER_STATE").selectedIndex != -1 && document.getElementById("cmbCUSTOMER_STATE").value != "")
            //add += document.getElementById("cmbCUSTOMER_STATE").options[document.getElementById("cmbCUSTOMER_STATE").selectedIndex].text + ", "
                add += document.getElementById("hidState_Code").value
            //            if (document.getElementById("cmbCUSTOMER_COUNTRY").selectedIndex != -1)
            //                add += document.getElementById("cmbCUSTOMER_COUNTRY").options[document.getElementById("cmbCUSTOMER_COUNTRY").selectedIndex].text + ", "

            if (document.getElementById("txtCUSTOMER_ZIP").value != "") {
                if (document.getElementById("cmbCUSTOMER_STATE").value != "") {
                    add += " - "
                }
                else add += " "
            }
            if (document.getElementById("txtCUSTOMER_ZIP").value != "") {
                add += document.getElementById("txtCUSTOMER_ZIP").value;
            }
            doc.getElementById("cltClientTop_lblClientStatus").innerHTML = document.getElementById("lblIS_ACTIVE").innerHTML;
            doc.getElementById("cltClientTop_lblClientAddress").innerHTML = add;


            //Set the value of titles based on user-preference
            if (document.getElementById('hidPREFIX').value != "") {
                for (i = 0; i < document.getElementById('cmbPREFIX').options.length; i++) {
                    if (document.getElementById('cmbPREFIX').options[i].value == document.getElementById('hidPREFIX').value) {
                        document.getElementById('cmbPREFIX').options[i].selected = true;
                        return false;
                    }
                }
            }
            else
                document.getElementById('cmbPREFIX').selectedIndex = -1;

            if (document.getElementById('hidMAIN_TITLE').value != "") {
                for (i = 0; i < document.getElementById('cmbMAIN_TITLE').options.length; i++) {
                    if (document.getElementById('cmbMAIN_TITLE').options[i].value == document.getElementById('cmbMAIN_TITLE').value) {
                        document.getElementById('cmbMAIN_TITLE').options[i].selected = true;
                        return false;
                    }
                }
            } else
                document.getElementById('cmbMAIN_TITLE').selectedIndex = -1;

            if (document.getElementById('hidMAIN_POSITION').value != "") {
                for (i = 0; i < document.getElementById('cmbMAIN_POSITION').options.length; i++) {
                    if (document.getElementById('cmbMAIN_POSITION').options[i].value == document.getElementById('cmbMAIN_POSITION').value) {
                        document.getElementById('cmbMAIN_POSITION').options[i].selected = true;
                        return false;
                    }
                }
            } else
                document.getElementById('cmbMAIN_POSITION').selectedIndex = -1;
        }

        //            function chkcrdate() {
        //            
        //            
        //             if (document.getElementById("cmbCUSTOMER_TYPE").options[document.getElementById("cmbCUSTOMER_TYPE").selectedIndex].value == "11109") {
        //                //  for commercial customer
        //                document.getElementById("rfvDATE_OF_BIRTH").innerHTML = document.getElementById("hidCREATION_DATE").value;

        //            }

        //            else if (document.getElementById("cmbCUSTOMER_TYPE").options[document.getElementById("cmbCUSTOMER_TYPE").selectedIndex].value == "11110")  // for personal
        //                document.getElementById("rfvDATE_OF_BIRTH").innerHTML = document.getElementById("hidDATE_OF_BIRTH").value;


        //        
        //        
        //                }


        function ChkDate(objSource, objArgs) {
            //			var expdate = '' //document.getElementById("txtCUSTOMER_INSURANCE_RECEIVED_DATE").value;
            //			
            //			if ( expdate == '')
            //			{
            //				objArgs.IsValid = true;	
            //			}
            //			
            //			objArgs.IsValid = DateComparer("<%=DateTime.Now.ToString()%>",expdate,jsaAppDtFormat);
        }


        function DoBack() {
            this.parent.document.location.href = "CustomerManagerSearch.aspx";
            return false;
        }
        function DoBackToAssistant() {
            this.parent.document.location.href = "CustomerManagerIndex.aspx";
            return false;
        }
        function GoToNewQuote() {
            //parent.document.location.href = "/cms/cmsweb/aspx/quotetab.aspx?CalledFromMenu=Y";
            parent.document.location.href = "/cms/Policies/Aspx/QuickQuote.aspx?CALLEDFROM=QAPP";
            return false;
        }
        function GoToNewApplication() {
            parent.document.location.href = "/CMS/POLICIES/ASPX/POLICYTAB.aspx?CALLEDFROM=CLT";
            return false;
        }
        function OnCustomerTypeChange() {
            //var CustomerType = document.getElementById('cmbCUSTOMER_TYPE').options[document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex].value;
            var customername = '<%= customerName %>';
            var FirstName = '<%= FirstName %>';
            if (document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex == -1)
                document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex = 0;
            if (document.getElementById('cmbCUSTOMER_TYPE').options[document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex].value == '11110') {
                //Type is personal
                //Changing error message of validation control
                document.getElementById("rfvCUSTOMER_FIRST_NAME").innerHTML = document.getElementById("rfvCUSTOMER_FIRST_NAME").getAttribute("ErrMsgFirstName");
                document.getElementById("capCUSTOMER_FIRST_NAME").innerHTML = customername;

                //First, middle and last name should be visible
                document.getElementById("tdF_NAME").colSpan = "1";
                document.getElementById("tdBUSS_TYPE").colSpan = "2";
                document.getElementById("tdF_NAME").style.width = "";
                document.getElementById("tdF_NAME").style.width = "33%";
                document.getElementById("tdM_NAME").style.width = "33%";
                document.getElementById("tdL_NAME").style.width = "33%";
                document.getElementById("tdID_TYPE").style.width = "33%";
                document.getElementById("tdMONTHLY_INCOME").style.width = "33%";
                document.getElementById("tdAMOUNT_TYPE").style.width = "33%";
                document.getElementById("tdM_NAME").style.display = "inline";
                document.getElementById("tdL_NAME").style.display = "inline";
                document.getElementById("trRNE").style.display = "inline";
                document.getElementById("tdCADEMP").style.display = "none";
                document.getElementById("tdNET_ASSETS_AMOUNT").style.display = "none";
                document.getElementById("trRNE").style.display = "inline";
                // document.getElementById("revNET_ASSETS_AMOUNT").style.display = "none";
                // document.getElementById("revMONTHLY_INCOME").style.display = "inline";
                document.getElementById("txtCUSTOMER_FIRST_NAME").size = 65;
                //                document.getElementById("rfvCUSTOMER_LAST_NAME").setAttribute('enabled', true);
                //                document.getElementById("rfvCUSTOMER_LAST_NAME").setAttribute('isValid', false);
                //                document.getElementById("rfvCUSTOMER_LAST_NAME").style.display = "inline";
                //                document.getElementById("rfvCUSTOMER_LAST_NAME").style.display = "none";
                document.getElementById("trMAIN_POSITION").style.display = 'none';
                document.getElementById("txtCPF_CNPJ").setAttribute('maxLength', '14');
                // document.getElementById("txtMAIN_CPF_CNPJ").setAttribute('maxLength', '14');
                document.getElementById("revCPF_CNPJ").innerText = document.getElementById("revCPF_CNPJ").getAttribute("ErrMsgcpf");
                //  document.getElementById("revMAIN_CPF_CNPJ").innerText = document.getElementById("revMAIN_CPF_CNPJ").getAttribute("ErrMsgcpf");

                if (document.getElementById("rdYES").checked == true) {

                    document.getElementById("tdID_TYPE").style.display = "inline";
                    document.getElementById("tdMONTHLY_INCOME").style.display = "inline";
                    document.getElementById("tdAMOUNT_TYPE").style.display = "inline";
                    document.getElementById("tdBUSS_TYPE").colSpan = "1";
                }

                //By Kuldeep to hide Marital Status, home Phone and Gender if Customer type != Personal
                document.getElementById("capMARITAL_STATUS").style.display = 'inline';
                document.getElementById("cmbMARITAL_STATUS").style.display = 'inline';
                document.getElementById("capGENDER").style.display = 'inline';
                document.getElementById("cmbGENDER").style.display = 'inline';
                document.getElementById("txtCUSTOMER_HOME_PHONE").style.display = 'inline';
                document.getElementById("capCUSTOMER_HOME_PHONE").style.display = 'inline';


            }
            else {
                //Type is commercial



                //Changing error message of validation control
                document.getElementById("rfvCUSTOMER_FIRST_NAME").innerHTML = document.getElementById("rfvCUSTOMER_FIRST_NAME").getAttribute("ErrMsgCustomerName");
                document.getElementById("capCUSTOMER_FIRST_NAME").innerHTML = customername;
                document.getElementById("tdF_NAME").colSpan = "3"
                document.getElementById("tdBUSS_TYPE").colSpan = "2";
                //document.getElementById("tdNET_ASSETS_AMOUNT").colSpan = "2"
                document.getElementById("tdM_NAME").style.display = "none";
                document.getElementById("tdL_NAME").style.display = "none";
                document.getElementById("tdID_TYPE").style.display = "none";
                document.getElementById("tdMONTHLY_INCOME").style.display = "none";
                document.getElementById("tdAMOUNT_TYPE").style.display = "none";
                document.getElementById("tdF_NAME").style.width = "100%";
                document.getElementById("txtCUSTOMER_FIRST_NAME").size = "65";
                document.getElementById("trRNE").style.display = "inline";
                // document.getElementById("revNET_ASSETS_AMOUNT").style.display = "inline";
                document.getElementById("revMONTHLY_INCOME").style.display = "none";
                //document.getElementById("revNET_ASSETS_AMOUNT").style.display = "inline";
                //                document.getElementById("rfvCUSTOMER_LAST_NAME").setAttribute('enabled', false);
                //                document.getElementById("rfvCUSTOMER_LAST_NAME").setAttribute('isValid', true);
                //                document.getElementById("rfvCUSTOMER_LAST_NAME").style.display = "none";
                document.getElementById("trMAIN_POSITION").style.display = 'none';
                //document.getElementById("cpv3REG_ID_ISSUE").style.display = 'none';
                document.getElementById("txtCPF_CNPJ").setAttribute('maxLength', '18');
                //document.getElementById("txtMAIN_CPF_CNPJ").setAttribute('maxLength', '18');

                document.getElementById("revCPF_CNPJ").innerText = document.getElementById("revCPF_CNPJ").getAttribute("ErrMsgCNPJ");

                //document.getElementById("revMAIN_CPF_CNPJ").innerText = document.getElementById("revMAIN_CPF_CNPJ").getAttribute("ErrMsgCNPJ");

                if (document.getElementById("rdYES").checked == true) {
                    document.getElementById("tdCADEMP").style.display = "inline";
                    document.getElementById("tdNET_ASSETS_AMOUNT").style.display = "inline";
                    document.getElementById("trRNE").style.display = "inline";
                }
                //document.getElementById('cmbCUSTOMER_TYPE').value

                //fPopCalendar(document.getElementById("trRNE"), document.getElementById("trRNE"));
                if (document.getElementById('cmbCUSTOMER_TYPE').value == '11109') {

                    var item = document.getElementById('revDATE_OF_BIRTH').getAttribute("validationexpression");
                    var item1 = item.replace("(19|20)", "(18|19|20)");
                    document.getElementById('revDATE_OF_BIRTH').setAttribute("validationexpression", item1);

                } else {

                    var item = document.getElementById('revDATE_OF_BIRTH').getAttribute("validationexpression");
                    var item1 = item.replace("(18|19|20)", "(19|20)");
                    document.getElementById('revDATE_OF_BIRTH').setAttribute("validationexpression", item1);
                }

                //By Kuldeep to hide Marital Status, home Phone and Gender if Customer type != Personal
                document.getElementById("capMARITAL_STATUS").style.display = 'none';
                document.getElementById("cmbMARITAL_STATUS").style.display = 'none';
                document.getElementById("capGENDER").style.display = 'none';
                document.getElementById("cmbGENDER").style.display = 'none';
                document.getElementById("txtCUSTOMER_HOME_PHONE").style.display = 'none';
                document.getElementById("capCUSTOMER_HOME_PHONE").style.display = 'none';


            }



        }

        function CheckCPVMsg() {
            if (document.getElementById('cmbCUSTOMER_TYPE').options[document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex].value == '11110') {
                if (document.getElementById("txtREG_ID_ISSUE").value == document.getElementById("txtDATE_OF_BIRTH").value) {
                    document.getElementById('cpvREG_ID_ISSUE').setAttribute('enabled', false);
                    document.getElementById('cpvREG_ID_ISSUE').style.display = 'none';
                }
                else
                    document.getElementById('cpvREG_ID_ISSUE').setAttribute('enabled', true);
            }
        }

        function CompareDates() {   //Added by Aditya on 05-10-2011 for TFS Bug # 553
            if ((document.getElementById('cmbCUSTOMER_TYPE').options[document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex].value == '11109') || (document.getElementById('cmbCUSTOMER_TYPE').options[document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex].value == '14725')) {
                var str1 = document.getElementById("txtREG_ID_ISSUE").value;
                var str2 = document.getElementById("txtDATE_OF_BIRTH").value;
                var date1 = new Date(str1);
                var date2 = new Date(str2);

                if (date2 > date1) {
                    document.getElementById('cpvREG_ID_ISSUE').setAttribute('enabled', false);
                    document.getElementById('cpvREG_ID_ISSUE').style.display = 'none';
                }
            }
        }


        //        function RegIdIssueDate() {
        //            if (document.getElementById("revREG_ID_ISSUE").isvalid == true) {
        //                document.getElementById('cpvREG_ID_ISSUE').setAttribute("enabled", false);
        //               document.getElementById('cpvREG_ID_ISSUE').style.display = 'none';
        //                document.getElementById('cpvREG_ID_ISSUE2').setAttribute("enabled", true);
        //                
        //                
        //            }
        //            else
        //                document.getElementById('cpvREG_ID_ISSUE2').setAttribute("enabled", false);
        //         //document.getElementById('cpvREG_ID_ISSUE').setAttribute("enabled", false);


        //        }
        //        


        function FormatAmountForSum(num) {
            num = ReplaceAll(num, sGroupSep, '');
            num = ReplaceAll(num, sDecimalSep, '.');
            return num;
        }

        //Custom validator function for premium > 0
        function validateAmount(objSource, objArgs) {
            var Amount = document.getElementById(objSource.controltovalidate).value;
            Amount = FormatAmountForSum(Amount);

            if (Amount != "" && parseFloat(Amount) > 0)
                objArgs.IsValid = true;
            else
                objArgs.IsValid = false;
        }

        function checkRadioButton() {
            if (document.getElementById('cmbCUSTOMER_TYPE').options[document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex].value == '11110') {

                if (document.getElementById("rdYES").checked == true) {
                    document.getElementById("tdMONTHLY_INCOME").style.display = "inline";
                    document.getElementById("tdID_TYPE").style.display = "inline";
                    document.getElementById("tdAMOUNT_TYPE").style.display = "inline";
                    document.getElementById("revMONTHLY_INCOME").style.display = "none";
                    document.getElementById("revNET_ASSETS_AMOUNT").style.display = "none";
                    document.getElementById("tdBUSS_TYPE").colSpan = "1";
                }
                else {
                    document.getElementById("tdBUSS_TYPE").colSpan = "2";
                    document.getElementById("tdMONTHLY_INCOME").style.display = "none";
                    document.getElementById("tdID_TYPE").style.display = "none";
                    document.getElementById("tdAMOUNT_TYPE").style.display = "none";
                }
            }
            if ((document.getElementById('cmbCUSTOMER_TYPE').options[document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex].value == '11109') || (document.getElementById('cmbCUSTOMER_TYPE').options[document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex].value == '14725')) {

                if (document.getElementById("rdYES").checked == true) {
                    document.getElementById("tdCADEMP").style.display = "inline";
                    document.getElementById("tdNET_ASSETS_AMOUNT").style.display = "inline";
                    document.getElementById("tdID_TYPE").style.display = "none";
                    //document.getElementById("revNET_ASSETS_AMOUNT").style.display = "inline"; 
                    // document.getElementById("revNET_ASSETS_AMOUNT").style.display = "none";
                    //document.getElementById("trRNE").style.display = "inline";  
                }

                else {
                    document.getElementById("tdCADEMP").style.display = "none";
                    document.getElementById("tdNET_ASSETS_AMOUNT").style.display = "none";
                    //document.getElementById("trRNE").style.display = "none"; 
                }
            }
            if (document.getElementById('cmbCUSTOMER_TYPE').options[document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex].value == '') {
                // document.getElementById("tdMONTHLY_INCOME").style.display = "none";
                //document.getElementById("tdID_TYPE").style.display = "none";
                //document.getElementById("tdAMOUNT_TYPE").style.display = "none";
                document.getElementById("tdCADEMP").style.display = "none";
                document.getElementById("tdNET_ASSETS_AMOUNT").style.display = "none";
                //document.getElementById("trRNE").style.display = "none";
            }


        }

        function filltext() {


            if (document.getElementById('txtCPF_CNPJ').value != "" && document.getElementById('rdYES').checked == true) {

                document.getElementById('txtCADEMP').value = "";
                document.getElementById('rfvCPF_CNPJ').setAttribute('enabled', false);
            }
        }

        function filltxt() {
            if (document.getElementById('txtCADEMP').value != "" && document.getElementById('rdYES').checked == true) {
                document.getElementById('rfvCPF_CNPJ').setAttribute('enabled', false);
                document.getElementById('txtCPF_CNPJ').value = "";
                //document.getElementById('spnCPF_CNPJ').visible = false;
                //document.getElementById('rfvCPF_CNPJ').setAttribute('enabled', false);
                //document.getElementById('rfvCPF_CNPJ').setAttribute('visible', false);
                //document.getElementById('txtCPF_CNPJ').disabled = true;
            }


        }
        function disablecpf_cnpj() {

            if ((document.getElementById('cmbCUSTOMER_TYPE').value == '11109' || document.getElementById('cmbCUSTOMER_TYPE').value == '14725') && document.getElementById('rdYES').checked == true) {
                document.getElementById('rfvCPF_CNPJ').setAttribute('enabled', false);
                document.getElementById('rfvCPF_CNPJ').setAttribute('visible', false);
                document.getElementById('spnCPF_CNPJ').setAttribute('visible', false);
            }
        }
        //    function enablecpf_cnpj() {
        //        if (document.getElementById('txtCADEMP').value == "" && document.getElementById('rdYES').checked == true) {
        //            document.getElementById('txtCPF_CNPJ').enabled = true;
        //        }
        //    }

        function fillcpf_cnpj() {
            if (document.getElementById('rdYES').checked == true) {
                document.getElementById('txtCPF_CNPJ').enabled = false;
            }

            else if (document.getElementById('rdYES').checked == false) {
                document.getElementById('txtCPF_CNPJ').enabled = true;
            }

        }

        function enablecademp() {

            if (document.getElementById('rdYES').checked == true && document.getElementById('txtCPF_CNPJ').value == "") {
                document.getElementById('txtCADEMP').setAttribute('disabled', false);
                document.getElementById('rfvCPF_CNPJ').setAttribute('enabled', false);
            }
        }

        function disablerfvcnpj() {

            if (document.getElementById('rdYES').checked == true && document.getElementById('cmbCUSTOMER_TYPE').value != '11110') {
                document.getElementById('rfvCPF_CNPJ').setAttribute('enabled', false);
            }
        }

        function enablerfvcnpj() {

            if (document.getElementById('rdYES').checked == false) {
                document.getElementById('rfvCPF_CNPJ').setAttribute('enabled', true);
                document.getElementById('spnCPF_CNPJ').setAttribute('display', 'inline');
            }
        }

        function enablerfvcpf() {

            if (document.getElementById('cmbCUSTOMER_TYPE').value == '11110') {
                document.getElementById('rfvCPF_CNPJ').setAttribute('enabled', true);
                document.getElementById('spnCPF_CNPJ').setAttribute('display', 'inline');
            }
        }

        //    function filltxt() {

        //        if (document.getElementById('txtCPF_CNPJ').value != "" && document.getElementById('rdYES').checked == true) {
        //            //document.getElementById('rfvCPF_CNPJ').enabled = false;
        //            document.getElementById('txtCADEMP').enabled = false;
        //        }

        //        else if (document.getElementById('txtCPF_CNPJ').value == "") {
        //            document.getElementById('rfvCPF_CNPJ').enabled = false;
        //            document.getElementById('txtCADEMP').enabled = true;
        //        }
        //    }


        function ValidateChkList(source, arguments) {
            if ($("#rdYES").attr("checked") == true || $("#rdNO").attr("checked") == true) {
                arguments.IsValid = true;
            }
            else {
                arguments.IsValid = false;
            }
        }


        //This function is used to generate the new code to the customer
        //calls in the blur event of CustomerFirstName and CustomeLastName for personal
        function CustomerLength(crtl) {
            if (document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex == 3) {

                var str = document.CLT_CUSTOMER_LIST.txtCUSTOMER_FIRST_NAME.value;
                if (Ctl >= 15) {
                    crtl.charcode = 0;
                    return;
                }
                if (document.CLT_CUSTOMER_LIST.txtCUSTOMER_FIRST_NAME.value.length >= 15) {

                    var st = str.substring(0, 14);
                    document.CLT_CUSTOMER_LIST.txtCUSTOMER_FIRST_NAME.value = st;
                    crtl.charcode = 0;
                    return;
                }


            }
        }
        function GenerateCustomerCode(fncntrl, lnamecntrl, codecntrl, rfvcode) {



            //we r generating the code only when user is in add mode
            //Hence checking the mode of form


            if (document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex == 3) {
                //Type is personal
                //Code shoud be First 2 char and last name 2 char 

                //Code should only be genarated when the event is coming from last name
                if (document.getElementById(fncntrl).value != "") {
                    // if (document.getElementById(lnamecntrl).value != "") 
                    //  {
                    document.getElementById(codecntrl).value = (GenerateRandomCode(ReplaceAll(document.getElementById(fncntrl).value, " ", ""), ReplaceAll(document.getElementById(lnamecntrl).value, " ", "")));
                    if (document.getElementById(codecntrl).value != "") {
                        document.getElementById(rfvcode).setAttribute('isValid', true);
                        document.getElementById(rfvcode).style.display = "none";
                    }
                    //  }

                }
            }
            else {
                //Type is commercial
                //Code should first name first 4 chars
                //Code should only be genarated when the event is coming from first name

                if (document.getElementById(fncntrl).value != "") {
                    document.getElementById(codecntrl).value = (GenerateRandomCode(document.getElementById(fncntrl).value, ''));
                    if (document.getElementById(codecntrl).value != "") {
                        document.getElementById(rfvcode).setAttribute('isValid', true);
                        document.getElementById(rfvcode).style.display = "none";
                    }
                }


            }

        }

        //***********

        //Main Contact information
        //Genrate COntact Code From Last name
        //function Main_Contact_code(fncntrl, lnamecntrl, codecntrl, rfvcode) {
        function Main_Contact_code(fncntrl, lnamecntrl, codecntrl) {
            //we r generating the code only when user is in add mode
            //Hence checking the mode of form           

            //Code shoud be First 2 char and last name 2 char 
            //Code should only be genarated when the event is coming from last name
            if (document.getElementById(fncntrl).value != "") {
                if (document.getElementById(lnamecntrl).value != "") {
                    document.getElementById(codecntrl).value = (GenerateRandomCode(ReplaceAll(document.getElementById(fncntrl).value, " ", ""), ReplaceAll(document.getElementById(lnamecntrl).value, " ", "")));
                    if (document.getElementById(codecntrl).value != "") {
                        //                        document.getElementById(rfvcode).setAttribute('isValid', true);
                        //                        document.getElementById(rfvcode).style.display = "none";
                    }
                }
            }

        }


        //***********
        function AddData() {

            document.forms[0].reset();
            document.getElementById('hidCUSTOMER_ID').value = 'New';
            document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex = -1;
            document.getElementById('txtCUSTOMER_PARENT_TEXT').value = '';
            document.getElementById('hidCUSTOMER_PARENT').value = '';
            document.getElementById('txtCUSTOMER_CODE').value = '';
            document.getElementById('txtCUSTOMER_SUFFIX').value = '';
            document.getElementById('txtCUSTOMER_FIRST_NAME').value = '';
            document.getElementById('txtCUSTOMER_ADDRESS1').value = '';
            document.getElementById('txtCUSTOMER_CITY').value = '';
            document.getElementById('cmbCUSTOMER_STATE').options.selectedIndex = -1;
            document.getElementById('txtCUSTOMER_ZIP').value = '';
            document.getElementById('txtCUSTOMER_CONTACT_NAME').value = '';
            document.getElementById('txtCUSTOMER_BUSINESS_PHONE').value = '';
            document.getElementById('txtCUSTOMER_MOBILE').value = '';
            document.getElementById('txtCUSTOMER_FAX').value = '';
            top.topframe.disableMenus('1', 'ALL');

            //Changing the color of mandatory controls
            ChangeColor();
            OnCustomerTypeChange();
            FillTitles();

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


        function setTab() {

            try {
                var lob = '';

                if (document.getElementById('hidCUSTOMER_ID').value != 'New') {
                    if (parent.CALLED_FROM_APPLICATION != undefined)
                        parent.CALLED_FROM_APPLICATION = 0;

                    var tabtitiles = '<%= tabcaption %>';
                    var tabtitile = tabtitiles.split(',');

                    Url = "ApplicantInsuedIndex.aspx?calledfrom=" + lob + "&Customer_ID=" + document.getElementById('hidCUSTOMER_ID').value + "&CUSTOMER_TYPE=" + document.getElementById('hidCust_Type').value + "&BackOption=" + "Y" + "&BACK_TO_APPLICATION=" + document.getElementById("hidBackToApplication").value + "&";
                    DrawTab(2, top.frames[1], tabtitile[1], Url);

                    Url = "/cms/cmsweb/Maintenance/ContactIndex.aspx?CALLEDFROM=CUSTOMER&EntityId=" + document.getElementById('hidCUSTOMER_ID').value + "&EntityType= CUSTOMER&CONTACT_TYPE_ID=2";  //+ document.getElementById('hidCust_Type').value + "&BackOption=" + "Y" + "&BACK_TO_APPLICATION=" + document.getElementById("hidBackToApplication").value + "&";
                    DrawTab(3, top.frames[1], tabtitile[2], Url);

                    Url = "/cms/cmsweb/Maintenance/AttachmentIndex.aspx?CALLEDFROM=CUSTOMER&EntityId=" + document.getElementById('hidCUSTOMER_ID').value + "&EntityType= CUSTOMER";  //+ document.getElementById('hidCust_Type').value + "&BackOption=" + "Y" + "&BACK_TO_APPLICATION=" + document.getElementById("hidBackToApplication").value + "&";
                    DrawTab(4, top.frames[1], tabtitile[3], Url);



                    //Url = "AttentionNotes.aspx?calledfrom=" + lob + "&CustomerID=" + document.getElementById('hidCUSTOMER_ID').value + "&BackOption=" + "Y" + "&BACK_TO_APPLICATION=" + document.getElementById("hidBackToApplication").value + "&";
                    //DrawTab(4, top.frames[1], 'Attention Note', Url);

                }
                else {

                    RemoveTab(2, top.frames[1]);

                    top.topframe.enableMenu('1,0');
                    top.topframe.enableMenu('1,1');
                }
            } catch (ex) { }
        }

        function Initialize() {

            //setTimeout("GetColorAgency()", 300);
            if (document.getElementById('hidTabInsScore').value == 1) {
                //document.getElementById('txtCUSTOMER_INSURANCE_SCORE').focus();
            }
            else {
                if (document.getElementById('hidFormSaved').value != 5) {
                    document.getElementById('cmbCUSTOMER_TYPE').focus();
                }
                var tempXML = document.getElementById('hidOldXML').value;


                if (document.getElementById('cmbCUSTOMER_TYPE').options[document.getElementById('cmbCUSTOMER_TYPE').selectedIndex].value == '11110') {
                    document.getElementById('hidCust_Type').value = "Personal";
                    document.getElementById('capCREATION_DATE').style.display = "none";
                    document.getElementById('capDATE_OF_BIRTH').style.display = "inline";
                }
                else {
                    document.getElementById('hidCust_Type').value = "Commercial";
                    document.getElementById('capCREATION_DATE').style.display = "inline";
                    document.getElementById('capDATE_OF_BIRTH').style.display = "none";
                }
            }
            if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')) {
                if (tempXML != "" && tempXML != 0 && tempXML != "<NewDataSet />") {
                    setMenu();
                    //Enabling the activate deactivate button
                    //document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
                    //					if(document.getElementById('btnActivateDeactivate'))
                    //					document.getElementById('btnActivateDeactivate').style.display='inline';

                    //Storing the XML in hidCUSTOMER_ID hidden fields 
                    if (tempXML != undefined)//Done for Itrack Issue 5454 on 20 April 09
                        document.getElementById('hidOldData').value = tempXML;

                    //Populating the data by the calling the common function in form,.js
                    //populateFormData(tempXML,CLT_CUSTOMER_LIST);
                    var stat = document.getElementById('hidMessage').value;
                    var Personal = document.getElementById('HidPersonal').value;
                    var Commercial = document.getElementById('HidBussiness').value;
                    var status1 = document.getElementById('hidstatus1').value;
                    var status2 = document.getElementById('hidstatus2').value;


                    try {
                        var doc = this.parent.document;
                        if (doc.getElementById("cltClientTop_lblClientStatus").innerHTML != null) {
                            if (document.getElementById("lblIS_ACTIVE").innerHTML == "N")
                                doc.getElementById("cltClientTop_lblClientStatus").innerHTML = status2; //"Inactive";
                            else
                                doc.getElementById("cltClientTop_lblClientStatus").innerHTML = status1; //"Active";
                        }
                        //if(doc.getElementById("cltClientTop_lblClientPhone").innerHTML != null)
                        //doc.getElementById("cltClientTop_lblClientPhone").innerHTML  = document.getElementById('txtCUSTOMER_HOME_PHONE').value;

                        //if (doc.getElementById("cltClientTop_lblClientTitle").innerHTML != null)
                        // doc.getElementById("cltClientTop_lblClientTitle").innerHTML = document.getElementById("cmbPREFIX").options[document.getElementById("cmbPREFIX").selectedIndex].text;


                        if (document.getElementById("cmbCUSTOMER_TYPE").options[document.getElementById("cmbCUSTOMER_TYPE").selectedIndex].value == "11109" || document.getElementById("cmbCUSTOMER_TYPE").options[document.getElementById("cmbCUSTOMER_TYPE").selectedIndex].value == "14725") {
                            doc.getElementById("cltClientTop_lblFullName").innerHTML = document.getElementById("txtCUSTOMER_FIRST_NAME").value
                            doc.getElementById("cltClientTop_lblPhone").innerHTML = Commercial; //"Business Phone";
                            doc.getElementById("cltClientTop_lblClientPhone").innerHTML = document.getElementById('txtCUSTOMER_BUSINESS_PHONE').value;
                        }
                        else {
                            doc.getElementById("cltClientTop_lblPhone").innerHTML = Personal;  //"Home Phone";
                        }
                    } catch (ex) { }


                    if (document.getElementById("hidIS_ACTIVE").value == "N") {
                        document.getElementById("lblIS_ACTIVE").innerHTML = status2//"Inactive";
                        document.getElementById("lblIS_ACTIVE").style.color = 'Red';
                    }
                    else {
                        document.getElementById("lblIS_ACTIVE").innerHTML = status1; //"Active";
                    }
                    //DrowSecondTab();
                    //Set Cust State  
                    //                    document.getElementById("cmbCUSTOMER_STATE").value = document.getElementById("hidSTATE_ID_OLD").value;
                    //                    //Set Employer Cust State					
                    //                    document.getElementById("cmbEMPLOYER_STATE").value = document.getElementById("hidEmpDetails_STATE_ID_OLD").value;

                }
                else {
                    AddData();
                }
            }

            if (document.getElementById("hidCarrierId").value == "1") {
                //document.getElementById("lblCUSTOMER_REFERRED_BY").style.display = "none"
                document.getElementById("lblCUSTOMER_AGENCY_ID").style.display = "none"
                //document.getElementById("cmbCUSTOMER_REFERRED_BY").style.display = "inline"
                document.getElementById("cmbCUSTOMER_AGENCY_ID").setAttribute('display', 'inline');


            }
            else {
                //document.getElementById("lblCUSTOMER_REFERRED_BY").style.display = "inline"
                document.getElementById("lblCUSTOMER_AGENCY_ID").style.display = "inline"
                document.getElementById("lblCUSTOMER_AGENCY_ID").innerHTML = document.getElementById("cmbCUSTOMER_AGENCY_ID").options[document.getElementById("cmbCUSTOMER_AGENCY_ID").selectedIndex].text;
                //document.getElementById("cmbCUSTOMER_REFERRED_BY").style.display = "none"
                document.getElementById("cmbCUSTOMER_AGENCY_ID").setAttribute('display', 'none');

            }

            setTab();
            //Check();
            //This function will enable or disable the middle and last name depending upon
            //the type of customer
            OnCustomerTypeChange();

        }

        function refreshtabIndex() {

            if (document.getElementById("hidRefreshTabIndex").value == 'Y') {
                RefreshClientTop();
                var strQueryString = "CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value + "&SaveMsg=" + document.getElementById("hidSaveMsg").value;
            }
        }

        function setMenu() {
            if (top.topframe != null && top.topframe != undefined) {
                if (document.getElementById('hidCUSTOMER_ID').value != "") {
                    if ((!top.topframe.main1.treeMenu[1].childNodes[3].enabled) || (top.topframe.main1.treeMenu[1].childNodes[3].enabled == "false")) {
                        top.topframe.enableMenu("1,1");
                        top.topframe.enableMenu("1,2");
                        top.topframe.enableMenu("1,4");
                        top.topframe.enableMenu("1,5");
                        top.topframe.enableMenu("1,6");
                        top.topframe.enableMenu("1,7");
                        //Disable application and policy menus when new customer is added
                        top.topframe.disableMenu('1,2,2');
                        top.topframe.disableMenu('1,2,3');

                        top.topframe.enableMenu('1,1,2'); //Enabling the add quote menu
                        top.topframe.enableMenu('1,1,1'); //Enabling the add application menu
                    }
                    else {
                        top.topframe.enableMenus('1', 'ALL');
                    }

                    if (document.getElementById("hidFormSaved").value == "1") {
                        //Customer has been saved, hence disabling the Application details menu
                        //top.topframe.disableMenu('1,2,2');	//Disabling the application details menu
                    }
                }
            }
        }

        //        function CheckIfPhoneEmpty() {
        //            if (document.getElementById('txtCUSTOMER_BUSINESS_PHONE').value == "") {
        //                document.getElementById('txtCUSTOMER_EXT').value = ""
        //                return false;
        //            }
        //            else
        //                return true;
        //        }

        function showButtons() {
            if (document.CLT_CUSTOMER_LIST.hidCUSTOMER_ID.value == "New") {
                if (document.getElementById("btnBack"))
                    document.getElementById("btnBack").style.display = "inline";
                //				if(document.getElementById("btnCustomerAssistant"))
                //				document.getElementById("btnCustomerAssistant").style.display="none";   
                //Nov11,2005:Sumit Chhabra:Added the code to show/hide the add quick quote and application buttons				
                //document.getElementById("btnAddNewQuickQuote").style.display="none";   
                //document.getElementById("btnAddNewApplication").style.display="none";   
            }
            else {
                if (document.getElementById("btnBack"))
                    document.getElementById("btnBack").style.display = "none";
                //				if(document.getElementById("btnCustomerAssistant"))
                //				document.getElementById("btnCustomerAssistant").style.display="inline";   
                //Nov11,2005:Sumit Chhabra:Added the code to show/hide the add quick quote and application buttons
                //document.getElementById("btnAddNewQuickQuote").style.display="inline";   
                //document.getElementById("btnAddNewApplication").style.display="inline";   

            }

        }

        function CheckInsuranceScore() {

            var diff = document.CLT_CUSTOMER_LIST.hidDT_LAST_INSURANCE_SCORE_FETCHED.value;

            var period = parseInt('<%=insurancePeriod%>');

            if (diff == '') {
                return true;
            }

            if (diff != -1) {
                if (diff <= period) {
                    alert('Cannot retrieve Insurance Score within ' + period + ' months');
                    return false;
                }
                else
                    return true;
            }
            return true;

        }
        function LoadSelf() {
            if (document.getElementById('hidCUSTOMER_ID').value == 'New') {
                AddData();
                top.topframe.enableMenu('1,0');
                top.topframe.enableMenu('1,1');
            }
            else
                document.location.href = "AddCustomer.aspx?CUSTOMER_ID=" + document.getElementById('hidCUSTOMER_ID').value + "&CalledFrom=" + '<%=strCalledFrom%>';
            return false;
        }
        function ChkTextAreaLength(source, arguments) {
            var txtArea = arguments.Value;
            if (txtArea.length > 1000) {
                arguments.IsValid = false;
                return;   // invalid userName
            }
        }

        function ViewMap() {
            var z = document.getElementById('txtCUSTOMER_ZIP').value;
            window.open("http://maps.yahoo.com/maps_result?addr=&csz=" + z + "&country=us&new=1&name=&qty=");
            return false;
        }
        function showPageLookupLayer(controlId) {

            var lookupMessage;
            switch (controlId) {
                case "cmbPREFIX":
                    lookupMessage = "";
                    break;
                case "cmbMAIN_POSITION":
                    lookupMessage = "%OCC.";
                    break;
                case "cmbCUSTOMER_REASON_CODE":
                    lookupMessage = "RCFC.";
                    break;
                default:
                    lookupMessage = "";
                    break;

            }
            showLookupLayer(controlId, lookupMessage);
        }
        function ChkDateOfBirth(objSource, objArgs) {

            if (document.getElementById("revDATE_OF_BIRTH").isvalid == true) {

                var effdate = document.CLT_CUSTOMER_LIST.txtDATE_OF_BIRTH.value;
                objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
            }
            else
                objArgs.IsValid = true;
        }
        function ChkCreatedate(objSource, objArgs) {

            if (document.getElementById("revDATE_OF_BIRTH").isvalid == true) {

                var effdate = document.CLT_CUSTOMER_LIST.txtDATE_OF_BIRTH.value;
                objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
            }
            else
                objArgs.IsValid = true;
        }



        function ChkResult(objSource, objArgs) {
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

        function ChkMainResult(objSource, objArgs) {
            objArgs.IsValid = true;
            if (objArgs.IsValid == true) {
                objArgs.IsValid = GetMainZipForState();
                if (objArgs.IsValid == false)
                    document.getElementById('csvMAIN_ZIPCODE').innerHTML = "The zip code does not belong to the state";
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

        //

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


        ////////////////////////////////////AJAX CALLS FOR ZIP///////////////////////////
        function GetZipForState() {
            GlobalError = true;
            if (document.getElementById('cmbCUSTOMER_STATE').value == 14 || document.getElementById('cmbCUSTOMER_STATE').value == 22 || document.getElementById('cmbCUSTOMER_STATE').value == 49) {
                if (document.getElementById('txtCUSTOMER_ZIP').value != "") {
                    var intStateID = document.getElementById('cmbCUSTOMER_STATE').options[document.getElementById('cmbCUSTOMER_STATE').options.selectedIndex].value;
                    var strZipID = document.getElementById('txtCUSTOMER_ZIP').value;
                    var result = AddCustomer.AjaxFetchZipForState(intStateID, strZipID);
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
        /////EMP ZIP AJAX////////////////
        function GetMainZipForState() {

            GlobalError = true;
            if (document.getElementById('cmbMAIN_STATE').value == 14 || document.getElementById('cmbMAIN_STATE').value == 22 || document.getElementById('cmbEMPLOYER_STATE').value == 49) {
                if (document.getElementById('txtMAIN_ZIPCODE').value != "") {
                    var intStateID = document.getElementById('cmbMAIN_STATE').options[document.getElementById('cmbMAIN_STATE').options.selectedIndex].value;
                    var strZipID = document.getElementById('cmbMAIN_STATE').value;
                    var result = AddCustomer.AjaxFetchZipForState(intStateID, strZipID);
                    return AjaxCallFunction_CallBack_Emp(result);
                }
                return false;
            }
            else
                return true;

        }

        function AjaxCallFunction_CallBack_Emp(response) {
            if (document.getElementById('cmbEMPLOYER_STATE').value == 14 || document.getElementById('cmbEMPLOYER_STATE').value == 22 || document.getElementById('cmbEMPLOYER_STATE').value == 49) {
                if (document.getElementById('txtEMPLOYER_ZIPCODE').value != "") {
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

        //////EMP ZIP AJAX END//////////


        ////////////////////////////////////////END ZIP//////////////////////////////////


        // Added by Swarup For checking zip code for LOB: End
        //SetAgency Color Added By MANOJ Rathore
        function GetColorAgency() {
            var varCusAgnID = document.getElementById('hidCustomer_AGENCY_ID').value;
            array = varCusAgnID.split("^");
            for (i = 0; i < array.length; i++) {
                var arr = array[i];




                for (cntr = 0; cntr < document.getElementById('cmbCUSTOMER_AGENCY_ID').length; cntr++) {
                    if (document.getElementById('cmbCUSTOMER_AGENCY_ID').options[cntr].value == arr) {
                        document.getElementById('cmbCUSTOMER_AGENCY_ID').options[cntr].className = "DeactivatedInstallmentPlan";
                    }

                }
            }
        }
        function RefreshCustomerTabIndex() {
            strURL = "/cms/client/aspx/CustomerTabIndex.aspx?CalledFrom=Direct&CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value;
            this.parent.location.href = strURL;

        }

        //Added by Mohit Agarwal 3-Sep-08



        //-------------------------------------------------------------------------------------------------------------
        //*****************************Added by Sibin for Itrack Issue 4843 on 16 OCT 08******************

        function fillstateFromCountry() {

            GlobalError = true;
            //var CmbState=document.getElementById('cmbCUSTOMER_STATE');
            var CountryID = document.getElementById('cmbCUSTOMER_COUNTRY').options[document.getElementById('cmbCUSTOMER_COUNTRY').selectedIndex].value;
            //var oResult='';
            AddCustomer.AjaxFillState(CountryID, fillState);


            if (GlobalError) {
                return false;
            }
            else {
                return true;
            }
        }
        function setStateID() {

            var CmbState = document.getElementById('cmbCUSTOMER_STATE');
            if (CmbState == null)
                return;
            if (CmbState.selectedIndex != -1) {
                document.getElementById('hidSTATE_ID').value = CmbState.options[CmbState.selectedIndex].value;

            }

        }
        function fillState(Result) {

            if (Result.error) {
                var xfaultcode = Result.errorDetail.code;
                var xfaultstring = Result.errorDetail.string;
                var xfaultsoap = Result.errorDetail.raw;
            }
            else {
                var statesList = document.getElementById("cmbCUSTOMER_STATE");
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
                // setStateID()';
                //document.getElementById("cmbCUSTOMER_STATE").value = document.getElementById("hidSTATE_ID_OLD").value;



            }

            return false;
        }



        function DisableZipForCanada() {
            //			var myCountry=document.getElementById("cmbCUSTOMER_COUNTRY");
            //			  
            //			if(myCountry.options[myCountry.selectedIndex].value=='2')
            //			{
            //				document.getElementById("revCUSTOMER_ZIP").setAttribute("enabled",false);
            //			}
            //			
            //			else
            //			{
            //				document.getElementById("revCUSTOMER_ZIP").setAttribute("enabled",true);
            //			}
        }


        function EmpDetails_setStateID() {
            var CmbState = document.getElementById('cmbEMPLOYER_STATE');
            if (CmbState == null)
                return;
            if (CmbState.selectedIndex != -1)
                document.getElementById('hidEmpDetails_STATE_ID').value = CmbState.options[CmbState.selectedIndex].value;

        }

        function EmpDetails_fillState(Result) {
            //alert(document.getElementById('cmbCO_APPLI_EMPL_STATE').selectedIndex);
            var strXML;
            if (Result.error) {
                var xfaultcode = Result.errorDetail.code;
                var xfaultstring = Result.errorDetail.string;
                var xfaultsoap = Result.errorDetail.raw;
            }
            else {
                var statesList = document.getElementById("cmbEMPLOYER_STATE");
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

                EmpDetails_setStateID();
                document.getElementById("cmbEMPLOYER_STATE").value = document.getElementById("hidEmpDetails_STATE_ID_OLD").value;
            }

            return false;
        }

        function EmpDetails_fillstateFromCountry() {
            GlobalError = true;
            var CountryID = document.getElementById('cmbEMPLOYER_COUNTRY').options[document.getElementById('cmbEMPLOYER_COUNTRY').selectedIndex].value;
            AddCustomer.AjaxFillState(CountryID, EmpDetails_fillState);
            if (GlobalError) {
                return false;
            }
            else {
                return true;
            }
        }

        function EmpDetails_DisableZipForCanada() {
            //			var myCountry=document.getElementById("cmbEMPLOYER_COUNTRY");
            //			  
            //			if(myCountry.options[myCountry.selectedIndex].value=='2')
            //			{
            //				document.getElementById("revEMPLOYER_ZIPCODE").setAttribute("enabled",false);
            //				document.getElementById("revEMPLOYER_ZIPCODE").style.display = 'none';
            //			}
            //			
            //			else
            //			{
            //				document.getElementById("revEMPLOYER_ZIPCODE").setAttribute("enabled",true);
            //			}

        }

        //Added functions till here by Sibin for Itrack Issue 4843
        function fillmainstateFromCountry() {

            GlobalError = true;
            //var CmbState=document.getElementById('cmbCUSTOMER_STATE');
            var CountryID = document.getElementById('cmbMAIN_COUNTRY').options[document.getElementById('cmbMAIN_COUNTRY').selectedIndex].value;
            //var oResult='';

            AddCustomer.AjaxFillState(CountryID, fillmainState);


            if (GlobalError) {
                return false;
            }
            else {
                return true;
            }
        }
        function fillmainState(Result) {
            //var strXML;
            if (Result.error) {
                var xfaultcode = Result.errorDetail.code;
                var xfaultstring = Result.errorDetail.string;
                var xfaultsoap = Result.errorDetail.raw;
            }
            else {
                var statesList = document.getElementById("cmbMAIN_STATE");
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


                //document.getElementById("cmbMAIN_STATE").value = document.getElementById("hidSTATE_ID_OLD").value;



            }

            return false;
        }


        //  For Bind the Titles forom Ajax
        //AjaxFillTitles
        function FillTitles() {

            GlobalError = true;
            if (document.getElementById('cmbCUSTOMER_TYPE').selectedIndex != -1 && document.getElementById('cmbCUSTOMER_TYPE').selectedIndex != 0) {
                var value = document.getElementById('cmbCUSTOMER_TYPE').options[document.getElementById('cmbCUSTOMER_TYPE').selectedIndex].value;
                var type = "";
                //debugger;
                document.getElementById('hidCREATION_DATE').value = value;

                //Personal
                if (value == "11110")
                //   type = "P"
                {
                    type = "11110"
                    //document.getElementById('rfvREG_ID_ISSUE').setAttribute('enabled', true);
                    //document.getElementById('rfvORIGINAL_ISSUE').setAttribute('enabled', true);
                    // document.getElementById('rfvNET_ASSETS_AMOUNT').setAttribute('enabled', false);
                    //document.getElementById('rfvREGIONAL_IDENTIFICATION').setAttribute('enabled', true);
                    document.getElementById('revREG_ID_ISSUE').setAttribute('enabled', true);
                    //document.getElementById('rfvMARITAL_STATUS').setAttribute('enabled', true);
                    //document.getElementById('rfvGENDER').setAttribute('enabled', true);
                    //document.getElementById('rfvDATE_OF_BIRTH').setAttribute('enabled', true);
                    document.getElementById('csvDATE_OF_BIRTH').setAttribute('enabled', true);
                    document.getElementById('csvCREATION_DATE').setAttribute('enabled', false);
                    //document.getElementById('rfv2DATE_OF_BIRTH').setAttribute('enabled', false);
                    //document.getElementById('revNET_ASSETS_AMOUNT').setAttribute('enabled', false);
                    // document.getElementById('revMONTHLY_INCOME').setAttribute('enabled', true);
                    //document.getElementById('rfv2DATE_OF_BIRTH').style.display = "none";
                    //document.getElementById('revNET_ASSETS_AMOUNT').style.display = "none";
                    //document.getElementById('revMONTHLY_INCOME').style.display = "inline";
                    //document.getElementById('spnREG_ID_ISSUE').style.display = "inline";
                    // document.getElementById('spnORIGINAL_ISSUE').style.display = "inline";
                    //document.getElementById('spnREGIONAL_IDENTIFICATION').style.display = "none";
                    // document.getElementById('spnMARITAL_STATUS').style.display = "inline";
                    //document.getElementById('spnGENDER').style.display = "inline";
                    //document.getElementById('spnDATE_OF_BIRTH').style.display = "inline";
                    document.getElementById('capCREATION_DATE').style.display = "none";
                    document.getElementById('capDATE_OF_BIRTH').style.display = "inline"; //Added by Aditya on 05-10-2011 for TFS Bug # 553
                    document.getElementById('cpvREG_ID_ISSUE').setAttribute('enabled', false);
                    document.getElementById('cpvREG_ID_ISSUE').style.display = "none";
                    //   document.getElementById('cpvREG_ID_ISSUE2').setAttribute('enabled', true);


                    //  document.getElementById('rfvDATE_OF_BIRTH').style.display = "inline";
                    //                    var el = document.getElementById("txtDATE_OF_BIRTH");                    
                    //                    el.readOnly = false;
                }
                //Commercial and government
                if (value == "11109" || value == "14725")
                //  type = "CO"
                {
                    //document.getElementById('rfvREG_ID_ISSUE').setAttribute('enabled',false);
                    //document.getElementById('rfvORIGINAL_ISSUE').setAttribute('enabled', false);
                    // document.getElementById('rfvNET_ASSETS_AMOUNT').setAttribute('enabled', false);
                    //document.getElementById('rfvREGIONAL_IDENTIFICATION').setAttribute('enabled', false);
                    document.getElementById('revREG_ID_ISSUE').setAttribute('enabled', true);
                    //document.getElementById('rfvMARITAL_STATUS').setAttribute('enabled', false);
                    //document.getElementById('rfvGENDER').setAttribute('enabled', false);
                    //document.getElementById('rfvDATE_OF_BIRTH').setAttribute('enabled', false);
                    document.getElementById('csvDATE_OF_BIRTH').setAttribute('enabled', false);
                    document.getElementById('csvCREATION_DATE').setAttribute('enabled', true);

                    //document.getElementById('rfv2DATE_OF_BIRTH').setAttribute('enabled', true);
                    //document.getElementById('revNET_ASSETS_AMOUNT').setAttribute('enabled', true);
                    //document.getElementById('revNET_ASSETS_AMOUNT').style.display = "inline";
                    document.getElementById('revMONTHLY_INCOME').setAttribute('enabled', false);
                    document.getElementById('revMONTHLY_INCOME').style.display = "none";
                    //document.getElementById('revNET_ASSETS_AMOUNT').setAttribute('enabled', true);
                    // document.getElementById('revNET_ASSETS_AMOUNT').style.display = "inline";
                    //document.getElementById('rfvDATE_OF_BIRTH').style.display = "none";
                    //document.getElementById('rfvREG_ID_ISSUE').style.display = "none";
                    // document.getElementById('rfvORIGINAL_ISSUE').style.display = "none";
                    //document.getElementById('rfvREGIONAL_IDENTIFICATION').style.display = "none";
                    //document.getElementById('revREG_ID_ISSUE').style.display = "none";
                    //document.getElementById('rfvMARITAL_STATUS').style.display = "none";
                    //document.getElementById('rfvGENDER').style.display = "none";
                    //document.getElementById('rfvDATE_OF_BIRTH').style.display = "inline";
                    //document.getElementById('spnREG_ID_ISSUE').style.display = "none";
                    //document.getElementById('spnORIGINAL_ISSUE').style.display = "none";
                    //document.getElementById('spnREGIONAL_IDENTIFICATION').style.display = "none";
                    // document.getElementById('spnMARITAL_STATUS').style.display = "none";
                    //document.getElementById('spnGENDER').style.display = "none";
                    //document.getElementById('spnDATE_OF_BIRTH').style.display = "inline";
                    document.getElementById('capCREATION_DATE').style.display = "inline";
                    document.getElementById('capDATE_OF_BIRTH').style.display = "none";
                    //document.getElementById("txtDATE_OF_BIRTH").value = "";
                    //document.getElementById("cstREG_ID_ISSUE").setAttribute('enabled', false);                
                    document.getElementById("cpvREG_ID_ISSUE2").setAttribute('enabled', false);  //Added by Aditya on 05-10-2011 for TFS Bug # 553
                    document.getElementById("cpvREG_ID_ISSUE2").style.display = "none";
                    document.getElementById("cpvREG_ID_ISSUE").setAttribute('enabled', false);  //Added by Aditya on 05-10-2011 for TFS Bug # 553
                    document.getElementById("cpvREG_ID_ISSUE").style.display = "none";

                    // document.getElementById("cpvREG_ID_ISSUE").setAttribute('enabled', true);
                    // document.getElementById("cpvREG_ID_ISSUE2").setAttribute('enabled', true);

                    //document.getElementById('cpv3REG_ID_ISSUE').setAttribute('enabled', false);
                    // var el = document.getElementById("txtDATE_OF_BIRTH");

                    //el.readOnly = true;



                    type = "11109"
                }
                if (value == "14725")
                //  type = "CO"
                {
                    //document.getElementById('rfvREG_ID_ISSUE').setAttribute('enabled',false);
                    //document.getElementById('rfvORIGINAL_ISSUE').setAttribute('enabled', false);
                    // document.getElementById('rfvNET_ASSETS_AMOUNT').setAttribute('enabled', false);
                    //document.getElementById('rfvREGIONAL_IDENTIFICATION').setAttribute('enabled', false);
                    document.getElementById('revREG_ID_ISSUE').setAttribute('enabled', true);
                    //document.getElementById('rfvMARITAL_STATUS').setAttribute('enabled', false);
                    //document.getElementById('rfvGENDER').setAttribute('enabled', false);
                    //document.getElementById('rfvDATE_OF_BIRTH').setAttribute('enabled', false);
                    document.getElementById('csvDATE_OF_BIRTH').setAttribute('enabled', false);
                    document.getElementById('csvCREATION_DATE').setAttribute('enabled', true);

                    //document.getElementById('rfv2DATE_OF_BIRTH').setAttribute('enabled', true);
                    //document.getElementById('revNET_ASSETS_AMOUNT').setAttribute('enabled', true);
                    //document.getElementById('revNET_ASSETS_AMOUNT').style.display = "inline";
                    document.getElementById('revMONTHLY_INCOME').setAttribute('enabled', false);
                    document.getElementById('revMONTHLY_INCOME').style.display = "none";
                    //document.getElementById('revNET_ASSETS_AMOUNT').setAttribute('enabled', true);
                    // document.getElementById('revNET_ASSETS_AMOUNT').style.display = "inline";
                    //document.getElementById('rfvDATE_OF_BIRTH').style.display = "none";
                    //document.getElementById('rfvREG_ID_ISSUE').style.display = "none";
                    // document.getElementById('rfvORIGINAL_ISSUE').style.display = "none";
                    //document.getElementById('rfvREGIONAL_IDENTIFICATION').style.display = "none";
                    //document.getElementById('revREG_ID_ISSUE').style.display = "none";
                    //document.getElementById('rfvMARITAL_STATUS').style.display = "none";
                    //document.getElementById('rfvGENDER').style.display = "none";
                    //document.getElementById('rfvDATE_OF_BIRTH').style.display = "inline";
                    //document.getElementById('spnREG_ID_ISSUE').style.display = "none";
                    //document.getElementById('spnORIGINAL_ISSUE').style.display = "none";
                    //document.getElementById('spnREGIONAL_IDENTIFICATION').style.display = "none";
                    // document.getElementById('spnMARITAL_STATUS').style.display = "none";
                    //document.getElementById('spnGENDER').style.display = "none";
                    //document.getElementById('spnDATE_OF_BIRTH').style.display = "inline";
                    document.getElementById('capCREATION_DATE').style.display = "inline";
                    document.getElementById('capDATE_OF_BIRTH').style.display = "none";
                    //document.getElementById("txtDATE_OF_BIRTH").value = "";
                    //document.getElementById("cstREG_ID_ISSUE").setAttribute('enabled', false);                
                    document.getElementById("cpvREG_ID_ISSUE2").setAttribute('enabled', false);  //Added by Aditya on 05-10-2011 for TFS Bug # 553
                    document.getElementById("cpvREG_ID_ISSUE2").style.display = "none";
                    document.getElementById("cpvREG_ID_ISSUE").setAttribute('enabled', false);  //Added by Aditya on 05-10-2011 for TFS Bug # 553
                    document.getElementById("cpvREG_ID_ISSUE").style.display = "none";

                    // document.getElementById("cpvREG_ID_ISSUE").setAttribute('enabled', true);
                    // document.getElementById("cpvREG_ID_ISSUE2").setAttribute('enabled', true);

                    //document.getElementById('cpv3REG_ID_ISSUE').setAttribute('enabled', false);
                    // var el = document.getElementById("txtDATE_OF_BIRTH");

                    //el.readOnly = true;



                    type = "14725"
                }

            }
            else
                return false;

            AddCustomer.AjaxFillTitles(type, BindTitles);
            //AddCustomer.AjaxFillTitles('P', BindPersonalTitles);
            AddCustomer.AjaxFillTitles('11110', BindPersonalTitles);
            AddCustomer.AjaxFillTitles(type, BindPosition);

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

                // For cmbPREFIX
                var statesList = document.getElementById("cmbPREFIX");
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

        //BindPersonalTitles
        function BindPersonalTitles(Result) {

            //var strXML;
            if (Result.error) {
                var xfaultcode = Result.errorDetail.code;
                var xfaultstring = Result.errorDetail.string;
                var xfaultsoap = Result.errorDetail.raw;
            }
            else {

                var statesList = document.getElementById("cmbMAIN_TITLE");
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
            } return false;
        }

        //BindPostion
        function BindPosition(Result) {

            //var strXML;
            if (Result.error) {
                var xfaultcode = Result.errorDetail.code;
                var xfaultstring = Result.errorDetail.string;
                var xfaultsoap = Result.errorDetail.raw;
            }
            else {

                var statesList = document.getElementById("cmbMAIN_POSITION");
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
            } return false;
        }

        //        function DefaultCountry() {

        //            var obj1 = document.getElementById('cmbCUSTOMER_COUNTRY');
        //            var obj2 = document.getElementById('cmbMAIN_COUNTRY');
        //            var len = obj1.options.length;

        //            for (i = 0; i <= len - 1; i++) {
        //                var cntryname = obj1.options[i].value;
        //                if (cntryname == "5") {
        //                    obj1.options[i].selected = true;
        //                    obj2.options[i].selected = true;
        //                    fillstateFromCountry();
        //                    fillmainstateFromCountry();
        //                }

        //            }

        //        }


        function initPage() {
            Initialize();
            InsuranceScoreChange();
            ApplyColor();
            showButtons();
            RefreshClientTop();

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
    </script>

    <script language="javascript" type="text/javascript">
<!--
        //validate CPf / CNPJ #;
        //For personal customer it accepts 14 digits CPF No
        //And For Commercial Customer it must bus accept only 18 digit CNPJ NO
        //we call a common function validar() for validate both CPF/CNPJ No

        function validatCPF_CNPJ(objSource, objArgs) {
            //get error message for xml on culture base. 
            var cpferrormsg = '<%=javasciptCPFmsg %>';
            var cnpjerrormsg = '<%=javasciptCNPJmsg %>';
            var CPF_invalid = '<%=CPF_invalid %>';
            var CNPJ_invalid = '<%=CNPJ_invalid %>';

            var valid = false;
            var idd = objSource.id;
            var rfvid = idd.replace('csv', 'rev');
            if (document.getElementById(rfvid) != null)
                if (document.getElementById(rfvid).isvalid == true) {
                    var theCPF = document.getElementById(objSource.controltovalidate)
                    var len = theCPF.value.length;
                    if (document.getElementById('cmbCUSTOMER_TYPE').value == '11110') {

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
                    else if (document.getElementById('cmbCUSTOMER_TYPE').value == '11109' || document.getElementById('cmbCUSTOMER_TYPE').value == '14725') {
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

        function FormatZipCode(vr) {

            // document.getElementById('revCUSTOMER_ZIP').setAttribute('enabled', true);
            var vr = new String(vr.toString());
            if (vr != "" && (document.getElementById('cmbCUSTOMER_COUNTRY').options[document.getElementById('cmbCUSTOMER_COUNTRY').options.selectedIndex].value == '5')) {

                vr = vr.replace(/[-]/g, "");
                num = vr.length;
                if (num == 8 && (document.getElementById('cmbCUSTOMER_COUNTRY').options[document.getElementById('cmbCUSTOMER_COUNTRY').options.selectedIndex].value == '5')) {
                    vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
                    document.getElementById('revCUSTOMER_ZIP').setAttribute('enabled', false);
                    document.getElementById('revMAIN_ZIPCODE').setAttribute('enabled', false);
                }

            }

            return vr;
        }
        //validate CPF for main Contact info

        function validatCPF(objSource, objArgs) {

            var cpferrormsg = '<%=javasciptCPFmsg %>';
            var CPF_invalid = '<%=CPF_invalid %>';


            var valid = false;
            var idd = objSource.id;
            var rfvid = idd.replace('csv', 'rev');
            if (document.getElementById(rfvid) != null)
                if (document.getElementById(rfvid).isvalid == true) {
                    var theCPF = document.getElementById(objSource.controltovalidate)
                    var len = theCPF.value.length;
                    if (len == '14') {
                        valid = validar(objSource, objArgs);
                        if (valid) { objSource.innerText = ''; } else { objSource.innerText = CPF_invalid; }
                    }
                    else {

                        if (document.getElementById(rfvid) != null) {
                            if (document.getElementById(rfvid).isvalid == true) {
                                objArgs.IsValid = false;
                                objSource.innerHTML = cpferrormsg; //'Please enter 14 digit CPF No';
                            } else { objSource.innerHTML = ''; }
                        }
                    }
                    //for CNPJ # in if customer type is commercial
                    //it check CNPJ format & valdate bia validar() function CNPJ is valid or not
                }
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
     
     	
        
       
//-->
    </script>
    <%--Populate the Customer Address based on the ZipeCode using jQuery ------- Added by Pradeep Kushwaha on 31 May 2010--%>
    <script language="javascript" type="text/javascript">

  
        $(document).ready(function () {


            $("#hlkZipLookup").click(function () {
                VerifyAddressDetailsForBR(document.getElementById('txtCUSTOMER_ADDRESS1'), document.getElementById('txtDISTRICT'), document.getElementById('txtCUSTOMER_CITY'), document.getElementById('cmbCUSTOMER_STATE'), document.getElementById('txtCUSTOMER_ZIP'))
            });
            $("#hypMAIN_ZIPCODE").click(function () {
                VerifyAddressDetailsForBR(document.getElementById('txtMAIN_ADDRESS'), document.getElementById('txtMAIN_DISTRICT'), document.getElementById('txtMAIN_CITY'), document.getElementById('cmbMAIN_STATE'), document.getElementById('txtMAIN_ZIPCODE'))
            });

            $("#txtCUSTOMER_ZIP").change(function () {

                if (trim($('#txtCUSTOMER_ZIP').val()) != '') {
                    var ZIPCODE = $("#txtCUSTOMER_ZIP").val();
                    $("#hidZipCodeCalledfor").val("CUSTOMER_ZIP");
                    //Change by kuldeep to fill data as per selected country earlier it was only for country id 5
                    var COUNTRYID = $("#cmbCUSTOMER_COUNTRY").val(); //"5";
                    ZIPCODE = ZIPCODE.replace(/[-]/g, "");
                    PageMethod("GetCustomerAddressDetailsUsingZipeCode", ["ZIPCODE", ZIPCODE, "COUNTRYID", COUNTRYID], AjaxSucceeded, AjaxFailed); //With parameters

                }
                else {

                    //                    $("#txtCUSTOMER_ADDRESS1").val('');
                    //                    $("#txtCUSTOMER_ADDRESS2").val('');
                    //                    $("#txtDISTRICT").val('');
                    //                    $("#txtCUSTOMER_CITY").val('');
                }

            });


            $("#txtMAIN_ZIPCODE").change(function () {
                if (trim($('#txtMAIN_ZIPCODE').val()) != '') {
                    var ZIPCODE = $("#txtCUSTOMER_ZIP").val();
                    $("#hidZipCodeCalledfor").val("MAIN_ZIPCODE");
                    //Change by kuldeep to fill data as per selected country earlier it was only for country id 5

                    var COUNTRYID = $("#cmbMAIN_COUNTRY").val(); // "5";

                    ZIPCODE = ZIPCODE.replace(/[-]/g, "");
                    PageMethod("GetCustomerAddressDetailsUsingZipeCode", ["ZIPCODE", ZIPCODE, "COUNTRYID", COUNTRYID], AjaxSucceeded, AjaxFailed); //With parameters

                }
                else {

                    //                    $("#txtMAIN_ADDRESS").val('');
                    //                    $("#txtMAIN_COMPLIMENT").val('');
                    //                    $("#txtMAIN_DISTRICT").val('');
                    //                    $("#txtMAIN_CITY").val('');
                }
            });

            //Copy Customer Address btnCopyCustomerAddress Added by Pradeep Kushwaha on 21-sep-2010

            $("#btnCopyCustomerAddress").click(function () {

                $("#txtMAIN_ADDRESS").val($("#txtCUSTOMER_ADDRESS1").val());
                $("#txtMAIN_NUMBER").val($("#txtNUMBER").val());
                $("#txtMAIN_COMPLIMENT").val($("#txtCUSTOMER_ADDRESS2").val());
                $("#txtMAIN_DISTRICT").val($("#txtDISTRICT").val());
                $("#txtMAIN_ZIPCODE").val($("#txtCUSTOMER_ZIP").val());
                $("#txtMAIN_CITY").val($("#txtCUSTOMER_CITY").val());
                if ($("#cmbCUSTOMER_STATE option:selected").text() != "") {
                    $("#cmbMAIN_STATE option:contains(" + $("#cmbCUSTOMER_STATE option:selected").text() + ")").attr('selected', 'selected');
                }
                return false;
            });

            //Copy customer Address Code ended till here 


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
            if ($("#hidZipCodeCalledfor").val() == "CUSTOMER_ZIP") {
                if (result.d != "" && Addresse[1] != 'undefined') {
                    $("#cmbCUSTOMER_STATE").val(Addresse[1]);
                    $("#hidSTATE_ID").val(Addresse[1]);
                    //$("#txtCUSTOMER_ZIP").val(Addresse[2]);
                    $("#txtCUSTOMER_ADDRESS1").val(Addresse[3] + ' ' + Addresse[4]);
                    //$("#txtCUSTOMER_ADDRESS2").val(Addresse[4]);
                    $("#txtDISTRICT").val(Addresse[5]);
                    $("#txtCUSTOMER_CITY").val(Addresse[6]);
                    var ZipeCode = $("#txtCUSTOMER_ZIP").val();
                    ZipeCode = ZipeCode.replace(/[-]/g, "");
                    if (ZipeCode == "00000000")
                        alert($("#hidZipeCodeVerificationMsg").val());
                }
                else if (document.getElementById('cmbCUSTOMER_COUNTRY').options[document.getElementById('cmbCUSTOMER_COUNTRY').options.selectedIndex].value == '5') {

                    //alert($("#hidZipeCodeVerificationMsg").val());
                    //                    $("#txtCUSTOMER_ZIP").val('');
                    //                    $("#txtCUSTOMER_ADDRESS1").val('');
                    //                    $("#txtCUSTOMER_ADDRESS2").val('');
                    //                    $("#txtDISTRICT").val('');
                    //                    $("#txtCUSTOMER_CITY").val('');
                }
            }
            else if ($("#hidZipCodeCalledfor").val() == "MAIN_ZIPCODE") {
                if (result.d != "" && Addresse[1] != 'undefined') {
                    $("#cmbMAIN_STATE").val(Addresse[1]);
                    // $("#txtMAIN_ZIPCODE").val(Addresse[2]);
                    $("#txtMAIN_ADDRESS").val(Addresse[3] + ' ' + Addresse[4]);
                    // $("#txtMAIN_COMPLIMENT").val(Addresse[4]);
                    $("#txtMAIN_DISTRICT").val(Addresse[5]);
                    $("#txtMAIN_CITY").val(Addresse[6]);
                    var MAINZipeCode = $("#txtMAIN_ZIPCODE").val();
                    MAINZipeCode = MAINZipeCode.replace(/[-]/g, "");
                    if (MAINZipeCode == "00000000")
                        alert($("#hidZipeCodeVerificationMsg").val());

                }
                else {
                    //alert($("#hidZipeCodeVerificationMsg").val());
                    //                    $("#txtMAIN_ZIPCODE").val('');
                    //                    $("#txtMAIN_ADDRESS").val('');
                    //                    $("#txtMAIN_COMPLIMENT").val('');
                    //                    $("#txtMAIN_DISTRICT").val('');
                    //                    $("#txtMAIN_CITY").val('');
                }
            }

        }

        function AjaxFailed(result) {

            //alert(result.d);
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

        function setMAIN_POSTION() {
            if (document.getElementById('cmbMAIN_POSITION') != "") {
                document.getElementById('hidMAIN_POSITION').value = document.getElementById('cmbMAIN_POSITION').value;

            }
        }
        function validateincome(objSource, objArgs) {

            var Premium = document.getElementById(objSource.controltovalidate).value;
            Premium = FormatForRate(Premium);
            if (parseFloat(Premium) > 0)
                objArgs.IsValid = true;
            else
                objArgs.IsValid = false;
        }

         
        
    </script> 
       <%-- End jQuery Implimentation for ZipeCode --%>
</head>
<body  leftmargin="10" topmargin="0" onload="initPage();checkRadioButton();">


    <p>
        <br />
    </p>
    <form id="CLT_CUSTOMER_LIST" method="post" runat="server">
    <p>
        <uc1:addressverification id="AddressVerification1" runat="server">
        </uc1:addressverification></p>
    <table id="tabCUSTOMER" runat="server" class="tableWidthHeader" width="95%" cellspacing="0" cellpadding="0" border="0" >
        <tr>
            <td>
                <table class="tableWidthHeader" align="center" border="0" width="100%">
                    <tr>
                        <td class="pageHeader" colspan="3">
                        <asp:Label ID="capMessages" runat="server"></asp:Label>
                        <%--    Please note that all fields marked with * are mandatory--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolorc" align="right" colspan="3">
                            <asp:Label ID="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td class="midcolorc" align="right" colspan="3">
                            <asp:Label ID="lblMessage1" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <!--Status,Type ,Parent -->
                        <td class="midcolora" width="33%" rowspan="">
                            <asp:Label ID="capIS_ACTIVE" runat="server">Status</asp:Label></br>
                            <asp:Label ID="lblIS_ACTIVE" CssClass="LabelFont" runat="server"></asp:Label>
                        </td>
                        <td class="midcolora" width="34%">
                            <asp:Label ID="capCUSTOMER_TYPE" runat="server">Type</asp:Label><span class="mandatory">*</span><br />
                            <asp:DropDownList ID="cmbCUSTOMER_TYPE" onfocus="SelectComboIndex('cmbCUSTOMER_TYPE');"
                                onblur="document.getElementById('imgSelect').focus();" runat="server" >
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvCUSTOMER_TYPE" runat="server" ControlToValidate="cmbCUSTOMER_TYPE"
                                Display="Dynamic" Enabled="True"></asp:RequiredFieldValidator>
                        </td>
                        <td class="midcolora" width="33%">
                            <asp:Label ID="capCUSTOMER_PARENT" runat="server">Parent</asp:Label>
                            <br />
                            <asp:TextBox ID="txtCUSTOMER_PARENT_TEXT" runat="server" size="35" ReadOnly="true"></asp:TextBox><a
                                class="calcolora" href="#">
                                
                                <img id="imgSelect" style="cursor: hand" alt="" src="../../cmsweb/images/selecticon.gif"
                                    border="0" autopostback="false" runat="server">
                        </td>
                    </tr>
        <!--First Name,Middle Name,Last Name -->
        <tr>
            <td class="midcolora" width="33%" id="tdF_NAME" runat="server">
                <asp:Label ID="capCUSTOMER_FIRST_NAME" runat="server">Customer name</asp:Label><span
                    class="mandatory">*</span><br />                <asp:TextBox ID="txtCUSTOMER_FIRST_NAME"   runat="server" CausesValidation="true" 
                    size="35" MaxLength="200" AutoCompleteType="Disabled"> </asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvCUSTOMER_FIRST_NAME" runat="server" ControlToValidate="txtCUSTOMER_FIRST_NAME"
                    Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td class="midcolora" width="34%" id="tdM_NAME" runat="server">
                <asp:Label ID="capCUSTOMER_MIDDLE_NAME"  style="display:none" runat="server"></asp:Label><br />
                <asp:TextBox ID="txtCUSTOMER_MIDDLE_NAME"  style="display:none"  runat="server" size="35" MaxLength="100"></asp:TextBox>
            </td>
            <td class="midcolora" width="33%" id="tdL_NAME" runat="server">
                <asp:Label ID="capCUSTOMER_LAST_NAME"  style="display:none"  runat="server">Last Name</asp:Label><%--<span class="mandatory"
                    id="spnMandatory">*</span>--%><br />
                <asp:TextBox ID="txtCUSTOMER_LAST_NAME" style="display:none"  runat="server" size="35" MaxLength="100"></asp:TextBox><br>
                <%--<asp:RequiredFieldValidator ID="rfvCUSTOMER_LAST_NAME" runat="server" ControlToValidate="txtCUSTOMER_LAST_NAME"
                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
                <asp:RegularExpressionValidator ID="revCUSTOMER_FIRST_NAME" Enabled="false" runat="server" ControlToValidate="txtCUSTOMER_LAST_NAME"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        
        <tr id="ttrIS_POLITICALLY_EXPOSED" runat="server">
        <td class="midcolora" width="33%" colspan="3" id="ttdIS_POLITICALLY_EXPOSED" runat="server">
                <asp:Label ID="capIS_POLITICALLY_EXPOSED" runat="server">Politically Exposed</asp:Label><span id="spnIS_POLITICALLY_EXPOSED"  runat="server" class="mandatory">*</span></br>
                
        
        <%-- <asp:RadioButtonList ID="RadioButtonList1" runat="Server">
                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                           </asp:RadioButtonList>
--%>
       
        <asp:RadioButton ID="rdYES" GroupName="rdYESNO"  runat="server" onClick="checkRadioButton();disablecpf_cnpj();" />
          <asp:Label ID="lblYES" runat="server">Yes</asp:Label>
      
        <asp:RadioButton ID="rdNO" GroupName="rdYESNO"  runat="server"  onClick="checkRadioButton();enablerfvcnpj();" />
        <asp:Label ID="lblNO" runat="server">No</asp:Label>
         <asp:CustomValidator ID="csvIS_POLITICALLY_EXPOSED" runat="server" ClientValidationFunction="ValidateChkList" ></asp:CustomValidator>
     <%-- <asp:RequiredFieldValidator ID="rfvyesno" runat="server" ControlToValidate="rdYES"
                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
                    
   
        </td>
        
        </tr>
        
        
        
        
        
        <!--Title,Customer Name,Code -->
        
            <tr>
            
            <td class="midcolora" width="33%" colspan="2" width="90%">
                <asp:Label runat="server" ID="capCUSTOMER_AGENCY_ID">Broker</asp:Label><span class="mandatory"
                    id="spnCUSTOMER_AGENCY_ID" runat="server">*</span>
                <br />
                <span id="span_Agency">
                    <asp:DropDownList ID="cmbCUSTOMER_AGENCY_ID" 
                        runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="lblCUSTOMER_AGENCY_ID" CssClass="LabelFont" runat="server"></asp:Label></span><br>
                <asp:RequiredFieldValidator ID="rfvCUSTOMER_AGENCY_ID" runat="server" ControlToValidate="cmbCUSTOMER_AGENCY_ID"
                    Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td class="midcolora" width="33%" colspan="1">
                <asp:Label ID="capCUSTOMER_CODE" runat="server">Code</asp:Label><span class="mandatory">*</span>
                <br />
                <asp:TextBox ID="txtCUSTOMER_CODE" runat="server" size="13" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox><br>
                <asp:RequiredFieldValidator ID="rfvCUSTOMER_CODE" runat="server" ControlToValidate="txtCUSTOMER_CODE"
                    Display="Dynamic"></asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator ID="revCUSTOMER_CODE" runat="server" Display="Dynamic"
                    ErrorMessage="" ControlToValidate="txtCUSTOMER_CODE"></asp:RegularExpressionValidator>
                    
            </td>
            <%--<td class="midcolora" width="33%">
            </td>--%>
        </tr>
                
        
        <tr>
            <td class="midcolora" width="33%" colspan="3">
                <asp:Label ID="capPREFIX" runat="server">Title</asp:Label></br>
                <asp:DropDownList ID="cmbPREFIX"   Width="90%" onfocus="SelectComboIndex('cmbPREFIX');"  runat="server" onchange="SetPREFIX()">
                </asp:DropDownList>
               <asp:HyperLink ID="hlkTitleLookup" runat="server" NavigateUrl="javascript:showPageLookupLayer('cmbPREFIX')">
               <asp:Image ID="imgTitleLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom" Visible="true" />
                </asp:HyperLink>
                <%--<a class="calcolora" href="javascript:showPageLookupLayer('cmbPREFIX')">
                    <img height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></a>--%>
               <%--<input type="hidden" runat="server" id="hidPREFIX" />--%>
   
            </td>
            </tr>
        <!--Zip Code,Address,Number -->
        <tr id="ttrCUSTOMER_ZIP" runat="server">
            <td id="ttdCUSTOMER_ZIP" runat="server" class="midcolora" width="33%">
                <asp:Label ID="capCUSTOMER_ZIP" runat="server">Zip Code</asp:Label><span class="mandatory"
                    id="spnCUSTOMER_ZIP" runat="server">*</span><br />
                <asp:TextBox ID="txtCUSTOMER_ZIP" CausesValidation="true" AutoCompleteType="Disabled" runat="server" size="13"
                    MaxLength="8" OnBlur="this.value=FormatZipCode(this.value);ValidatorOnChange();GetZipForState();DisableZipForCanada();"></asp:TextBox><%--<img height="16" src="" width="17" border="0">--%>
                    
                <asp:HyperLink ID="hlkZipLookup" Visible="true" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif"
                        ImageAlign="Bottom" Visible="true"></asp:Image>
                </asp:HyperLink>
                <br>
                <asp:CustomValidator ID="csvCUSTOMER_ZIP" runat="server" ClientValidationFunction="ChkResult"
                    ErrorMessage=" " Display="Dynamic" ControlToValidate="txtCUSTOMER_ZIP"></asp:CustomValidator>
                <%--<asp:RequiredFieldValidator ID="rfvCUSTOMER_ZIP" runat="server" ControlToValidate="txtCUSTOMER_ZIP"
                    Display="Dynamic"></asp:RequiredFieldValidator>--%><asp:RegularExpressionValidator ID="revCUSTOMER_ZIP"
                        runat="server" ControlToValidate="txtCUSTOMER_ZIP" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td class="midcolora" width="34%">
                <asp:Label ID="capCUSTOMER_ADDRESS1" runat="server">Address</asp:Label><%--<span class="mandatory">*</span>--%>
                <br />
                <asp:TextBox ID="txtCUSTOMER_ADDRESS1" runat="server" size="30" MaxLength="200" AutoCompleteType="Disabled"></asp:TextBox><br>
               <%-- <asp:RequiredFieldValidator ID="rfvCUSTOMER_ADDRESS1" runat="server" ControlToValidate="txtCUSTOMER_ADDRESS1"
                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
            </td>
            <td class="midcolora" width="33%">
                <asp:Label runat="server" ID="capNUMBER">Number</asp:Label><%--<span class="mandatory">*</span>--%><br />
                <asp:TextBox runat="server" ID="txtNUMBER" AutoCompleteType="Disabled" CausesValidation="true"
                    size="25" MaxLength="10"></asp:TextBox><br />
              <%-- <asp:RequiredFieldValidator runat="server" ID="rfvNUMBER" Display="Dynamic"  ControlToValidate="txtNUMBER" ErrorMessage=""></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <!--Compliment,District,CIty -->
        <tr>
            <td class="midcolora" width="33%">
                <asp:Label ID="capCUSTOMER_ADDRESS" runat="server">Compliment</asp:Label>
                <br />
                <asp:TextBox ID="txtCUSTOMER_ADDRESS2" AutoCompleteType="Disabled" runat="server"
                    size="35" MaxLength="75"></asp:TextBox>
            </td>
            <td class="midcolora" width="34%">
                <asp:Label ID="capDISTRICT" runat="server">District</asp:Label><%--<span class="mandatory">*</span>--%><br />
                <asp:TextBox runat="server" AutoCompleteType="Disabled" ID="txtDISTRICT" size="25"
                    MaxLength="35" CausesValidation="true"></asp:TextBox><br />
                <%--<asp:RequiredFieldValidator runat="server" ID="rfvDISTRICT" Display="Dynamic" ControlToValidate="txtDISTRICT"
                    ErrorMessage=""></asp:RequiredFieldValidator>--%>
            </td>
            <td class="midcolora" width="33%">
                <asp:Label ID="capCUSTOMER_CITY" runat="server">City</asp:Label><%--<span class="mandatory">*</span>--%><br />
                <asp:TextBox ID="txtCUSTOMER_CITY" runat="server" size="35" MaxLength="35" AutoCompleteType="Disabled"></asp:TextBox><br>
                <%--<asp:RequiredFieldValidator ID="rfvCUSTOMER_CITY" runat="server" ControlToValidate="txtCUSTOMER_CITY"
                    Display="Dynamic" ErrorMessage=""></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <!--State,Country,CPF,CNPJ  -->
        <tr>
         <td class="midcolora" width="33%">
                <asp:Label ID="capCUSTOMER_COUNTRY" runat="server">Country</asp:Label><%--<span class="mandatory">*</span>--%><br />
                <asp:DropDownList ID="cmbCUSTOMER_COUNTRY" onfocus="SelectComboIndex('cmbCUSTOMER_COUNTRY');"
                    runat="server" onchange="javascript:fillstateFromCountry();">
                </asp:DropDownList>
                <br>
               <%-- <asp:RequiredFieldValidator ID="rfvCUSTOMER_COUNTRY" runat="server" ControlToValidate="cmbCUSTOMER_COUNTRY"
                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
            </td>
            <td class="midcolora" width="34%">
                <asp:Label ID="capCUSTOMER_STATE" runat="server">State</asp:Label><%--<span class="mandatory"
                    id="spnCUSTOMER_STATE" runat="server">*</span>--%>
                <br />
                <asp:DropDownList ID="cmbCUSTOMER_STATE" onchange="setStateID();" onfocus="SelectComboIndex('cmbCUSTOMER_STATE');"
                    runat="server">
                </asp:DropDownList>
                <br>
                <%--<asp:RequiredFieldValidator ID="rfvCUSTOMER_STATE" runat="server" ControlToValidate="cmbCUSTOMER_STATE"
                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
            </td>
           
            <td class="midcolora" width="33%">
                <asp:Label runat="server" ID="capCPF_CNPJ">CPF_CNPJ #</asp:Label><span class="mandatory" runat="server"
                    id="spnCPF_CNPJ">*</span>
                <br />
                <asp:TextBox runat="server" ID="txtCPF_CNPJ" CausesValidation="true" OnBlur="this.value=FormatCPFCNPJ(this.value); ValidatorOnChange();enablecademp();enablerfvcpf();" onchange=" filltext();"   AutoCompleteType="Disabled" MaxLength="18"  size="25"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator runat="server" ID="rfvCPF_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtCPF_CNPJ"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="revCPF_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtCPF_CNPJ"></asp:RegularExpressionValidator>
                    <asp:CustomValidator runat="server" ID="csvCPF_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtCPF_CNPJ" ClientValidationFunction="validatCPF_CNPJ"></asp:CustomValidator>
            </td>
        </tr>
        <!--Broker  -->
        <tr>
            
            <td class="midcolora" width="34%">
                <asp:Label ID="capCUSTOMER_WEBSITE" runat="server">Website</asp:Label><br />
                  <asp:TextBox ID="txtCUSTOMER_WEBSITE" runat="server" size="35" MaxLength="150" CausesValidation="true"></asp:TextBox><br>
                <asp:RegularExpressionValidator  ID="revCUSTOMER_WEBSITE" runat="server"
                    ControlToValidate="txtCUSTOMER_WEBSITE" Display="Dynamic"></asp:RegularExpressionValidator>
                
            </td>
            <td class="midcolora" id="tdBUSS_TYPE" width="33%" >
            <asp:Label ID="capCUSTOMER_BUSINESS_TYPE"  runat="server">Buiness Type</asp:Label><br />
<asp:TextBox ID="txtCUSTOMER_BUSINESS_TYPE_NAME"  runat="server" size="35"
                    ReadOnly="True"></asp:TextBox><a class="calcolora" href="#"><img id="imgBusinessType"
                        style="cursor: hand" alt="" src="../../cmsweb/images/selecticon.gif" border="0"
                        runat="server"></a>
                <input id="hidCUSTOMER_BUSINESS_TYPE" type="hidden" name="hidCUSTOMER_BUSINESS_TYPE"
                    runat="server">
            </td>
            
         <td class="midcolora" width="34%" id="tdID_TYPE">
                <asp:Label ID="CapID_TYPE" runat="server">Id Type</asp:Label>
                <br />
                <asp:TextBox ID="txtID_TYPE" runat="server" size="30" MaxLength="20" AutoCompleteType="Disabled"></asp:TextBox><br>
               <%-- <asp:RequiredFieldValidator ID="rfvID_TYPE" runat="server" ControlToValidate="txtID_TYPE"
                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
            </td>
            
        </tr>
        <tr>
        <td class="midcolora" width="33%">
                <asp:Label runat="server" ID="capREG_ID_ISSUE">Regional ID Issue</asp:Label><%--<span id="spnREG_ID_ISSUE"  runat="server" class="mandatory">*</span>--%>
              <%--  <asp:Label runat="server" ID="lblREG_ID_ISSUE" class="mandatory" Text="*" ></asp:Label>--%>
                <br />
                <asp:TextBox runat="server" ID="txtREG_ID_ISSUE" AutoCompleteType="Disabled" onblur="FormatDate();CompareDates();CheckCPVMsg();ValidatorOnChange();"  CausesValidation="true"></asp:TextBox>
                <asp:HyperLink ID="hlkREG_ID_ISSUE" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgREG_ID_ISSUE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif">
                    </asp:Image>
                </asp:HyperLink><br />
               <%-- <asp:RequiredFieldValidator runat="server" ID="rfvREG_ID_ISSUE" Display="Dynamic"
                    ErrorMessage="" ControlToValidate="txtREG_ID_ISSUE"></asp:RequiredFieldValidator>--%>
                <asp:RegularExpressionValidator runat="server" ID="revREG_ID_ISSUE" Display="Dynamic"
                    ControlToValidate="txtREG_ID_ISSUE"></asp:RegularExpressionValidator>
                   <%-- <asp:comparevalidator id="cpvREG_ID_ISSUE" ControlToValidate="txtREG_ID_ISSUE" Display="Dynamic" Runat="server" ControlToCompare="txtDATE_OF_BIRTH" Type="Date" 
										Operator="GreaterThan"></asp:comparevalidator> <%--ashish--%> 
				 <asp:comparevalidator id="cpvREG_ID_ISSUE2" ControlToValidate="txtREG_ID_ISSUE" Display="Dynamic" Runat="server" Type="Date"
					             Operator="LessThanEqual" 
					             ValueToCompare="<%# DateTime.Today.ToShortDateString()%>"  ></asp:comparevalidator><br>	
					            <asp:comparevalidator id="cpvREG_ID_ISSUE"  
					             ControlToValidate="txtREG_ID_ISSUE" Display="Dynamic" Runat="server" ControlToCompare="txtDATE_OF_BIRTH" Type="Date"
			                     Operator="GreaterThan"></asp:comparevalidator>
			                     
              <%--  <asp:DropDownList Visible="false" ID="cmbREG_ID_ISSUE" runat="server">
                    <asp:ListItem Text="" Value="0"></asp:ListItem>    --%>
               <%-- </asp:DropDownList>--%>
                
            </td>
            <td class="midcolora" width="34%">
                <asp:Label runat="server" ID="capORIGINAL_ISSUE">Original Issue</asp:Label><%--<span id="spnORIGINAL_ISSUE"  runat="server" class="mandatory">*</span>--%>
                <br />
                <asp:TextBox runat="server" AutoCompleteType="Disabled" ID="txtORIGINAL_ISSUE"></asp:TextBox><br />
                <%--<asp:RequiredFieldValidator runat="server" ID="rfvORIGINAL_ISSUE" Display="Dynamic"
                    ControlToValidate="txtORIGINAL_ISSUE"></asp:RequiredFieldValidator>--%>
            </td>
           
            <td class="midcolora" width="33%">
                <asp:Label runat="server" ID="capREGIONAL_IDENTIFICATION">Regional Identification</asp:Label><%--<span id="spnREGIONAL_IDENTIFICATION"  runat="server" class="mandatory">*</span>--%>
                <br />
                <asp:TextBox runat="server" AutoCompleteType="Disabled" 
                    ID="txtREGIONAL_IDENTIFICATION" CausesValidation="true" MaxLength="12"></asp:TextBox><br />
                <%--<asp:RequiredFieldValidator runat="server" ID="rfvREGIONAL_IDENTIFICATION" Display="Dynamic"
                    ErrorMessage="" ControlToValidate="txtREGIONAL_IDENTIFICATION"></asp:RequiredFieldValidator>--%>
            </td>
            
        </tr>
        <tr>
          <td class="midcolora" width="34%" >
                <asp:Label ID="capCUSTOMER_Email" runat="server">Email Address</asp:Label>
                <br />
                <asp:TextBox ID="txtCUSTOMER_Email" runat="server" size="35" AutoCompleteType="Disabled"
                    MaxLength="50"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revCUSTOMER_Email" runat="server" ControlToValidate="txtCUSTOMER_Email"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
             <td class="midcolora" width="33%">
            <asp:Label ID="capMARITAL_STATUS" runat="server">Marital Status</asp:Label><%--<span id="spnMARITAL_STATUS" runat="server" class="mandatory">*</span>--%>
            
            <br />
            <asp:DropDownList ID="cmbMARITAL_STATUS" runat="server" 
                onfocus="SelectComboIndex('cmbMARITAL_STATUS')">
            </asp:DropDownList>
            <br />
           <%--<asp:RequiredFieldValidator ID="rfvMARITAL_STATUS" runat="server" 
                ControlToValidate="cmbMARITAL_STATUS" Display="Dynamic" 
                ErrorMessage=""></asp:RequiredFieldValidator>--%>
            </td>
            <td class="midcolora" width="33%">
               <asp:Label ID="capCREATION_DATE" runat="server" Text=""></asp:Label> <asp:Label ID="capDATE_OF_BIRTH" runat="server" ></asp:Label><span id="spnDATE_OF_BIRTH" runat="server" class="mandatory">*</span>
                
                <br /><asp:TextBox ID="txtDATE_OF_BIRTH" runat="server" size="12"  onBlur="FormatDate();CompareDates();ValidatorOnChange();" onchange="CheckCPVMsg();" MaxLength="10"></asp:TextBox><asp:HyperLink ID="hlkDATE_OF_BIRTH" runat="server" CssClass="HotSpot" >
                    <asp:Image runat="server" ID="imgDATE_OF_BIRTH" Border="0" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"
                        ></asp:Image>
                </asp:HyperLink><br/>
                <asp:RequiredFieldValidator ID="rfvDATE_OF_BIRTH" 
                    runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic" ErrorMessage=""></asp:RequiredFieldValidator>
                    <%--<asp:RequiredFieldValidator ID="rfv2DATE_OF_BIRTH" 
                    runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic" ErrorMessage=""></asp:RequiredFieldValidator>--%>
                <asp:RegularExpressionValidator  ID="revDATE_OF_BIRTH" 
                    runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic"></asp:RegularExpressionValidator>
                <asp:CustomValidator ID="csvDATE_OF_BIRTH" runat="server" 
                    ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic" ClientValidationFunction="ChkDateOfBirth"></asp:CustomValidator>
                    <asp:CustomValidator ID="csvCREATION_DATE" runat="server" 
                    ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic" ClientValidationFunction="ChkCreatedate"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
        <td  width="33%"  class="midcolora">
        <asp:Label ID="capCUSTOMER_BUSINESS_DESC"  runat="server" Width="150">Business Desc</asp:Label><br />
        <asp:TextBox onkeypress="MaxLength(this,1000)" ID="txtCUSTOMER_BUSINESS_DESC" runat="server"
                    Columns="50" MaxLength="1000"  TextMode="MultiLine" Rows="4"></asp:TextBox><br>
                <asp:CustomValidator ID="csvCUSTOMER_BUSINESS_DESC" runat="server" 
                    ControlToValidate="txtCUSTOMER_BUSINESS_DESC" Display="Dynamic" ClientValidationFunction="ChkTextAreaLength"></asp:CustomValidator>
        
        </td>
        <td class="midcolora" width="34%">
            <asp:Label ID="capGENDER" runat="server">Gender</asp:Label><%--<span id="spnGENDER" runat="server" class="mandatory">*</span> --%>
            
            <br />
            <asp:DropDownList ID="cmbGENDER"  runat="server" 
                onfocus="SelectComboIndex('cmbGENDER')">    
              
            </asp:DropDownList>
            <br />
            <%--<asp:RequiredFieldValidator ID="rfvGENDER" runat="server" 
                ControlToValidate="cmbGENDER" Display="Dynamic" 
                ErrorMessage=""></asp:RequiredFieldValidator>--%>
            </td>
         <td class="midcolora" width="33%">
                <asp:Label ID="capCUSTOMER_HOME_PHONE" runat="server">Home Phone</asp:Label>
                <br />
                <asp:TextBox ID="txtCUSTOMER_HOME_PHONE" runat="server" size="25" MaxLength="15" onblur="FormatBrazilPhone()"
                    AutoCompleteType="Disabled"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revCUSTOMER_HOME_PHONE" runat="server" ControlToValidate="txtCUSTOMER_HOME_PHONE"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
         
        </tr>
       <tr>
         
            <td class="midcolora" width="33%" id="tdMONTHLY_INCOME">
                <asp:Label runat="server" ID="CapMONTHLY_INCOME">Total amount in assets or monthly income</asp:Label><%--<span class="mandatory">*</span>--%><br />
                <asp:TextBox runat="server" ID="txtMONTHLY_INCOME" CssClass="INPUTCURRENCY" AutoCompleteType="Disabled" CausesValidation="true"
                    size="15" MaxLength="8"></asp:TextBox><br />
              <%-- <asp:RequiredFieldValidator runat="server" ID="rfvMONTHLY_INCOME" Display="Dynamic"  ControlToValidate="txtMONTHLY_INCOME" ErrorMessage=""></asp:RequiredFieldValidator>--%>
              <asp:regularexpressionvalidator id="revMONTHLY_INCOME" runat="server" ControlToValidate="txtMONTHLY_INCOME" ErrorMessage="RegularExpressionValidator"
															Display="Dynamic"></asp:regularexpressionvalidator>
            </td>
        
         <td class="midcolora" width="34%" ID="tdAMOUNT_TYPE">
                <asp:Label ID="CapAMOUNT_TYPE" runat="server">Amount Type</asp:Label>
                <br />
                <asp:DropDownList ID="cmbAMOUNT_TYPE" runat="server">
                </asp:DropDownList>
                <br>
                <%--<asp:RequiredFieldValidator ID="rfvAMOUNT_TYPE" runat="server" ControlToValidate="cmbAMOUNT_TYPE"
                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
            </td>
            
             <td class="midcolora" width="33%">
             </td>
        
        </tr>
        <tr> 
        <td class="midcolora" width="33%" id="tdCADEMP">
            <asp:Label id="capCADEMP" runat ="server">CADEMP Number</asp:Label><br />
            <asp:TextBox ID="txtCADEMP" MaxLength="18" runat="server" onblur="disablerfvcnpj();" onchange="filltxt();"></asp:TextBox><br />
            <%--<asp:RequiredFieldValidator ID="rfvCADMP_NUM" runat="server" ControlToValidate="capTXT_CADMP_NUM"
                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
        </td>
        <td class="midcolora" width="34%" id="tdNET_ASSETS_AMOUNT">
            <asp:Label ID="capNET_ASSETS_AMOUNT" runat="server">Net Assets amount</asp:Label><%--<span class="mandatory">*</span>--%><br />
            <asp:TextBox ID="txtNET_ASSETS_AMOUNT" runat="server" CssClass="INPUTCURRENCY" MaxLength="8"></asp:TextBox><br />
          <%-- <asp:RequiredFieldValidator ID="rfvNET_ASSETS_AMOUNT" runat="server" ControlToValidate="txtNET_ASSETS_AMOUNT"
                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
                   <asp:regularexpressionvalidator id="revNET_ASSETS_AMOUNT" runat="server" ControlToValidate="txtNET_ASSETS_AMOUNT" ErrorMessage="RegularExpressionValidator"
															Display="Dynamic"></asp:regularexpressionvalidator>
        </td>
        
             <td class="midcolora" width="33%">
             </td>
        
        </tr>
        <tr>
		 <td class="midcolora" width="33%"><asp:label id="capACCOUNT_TYPE" runat="server">ACCOUNT_TYPE</asp:label><%--<span class="mandatory">*</span>--%>
          <br />
          <asp:DropDownList ID="cmbACCOUNT_TYPE" runat="server"></asp:DropDownList>
          <br/>
        <%-- <asp:RequiredFieldValidator ID="rfvACCOUNT_TYPE" ControlToValidate="cmbACCOUNT_TYPE" runat="server" Display="dynamic"></asp:RequiredFieldValidator>--%>
         </td>
         <td class="midcolora" width="33%"><asp:label id="capACCOUNT_NUMBER" runat="server" >ACCOUNT_NUMBER</asp:label>
       <br />
      <asp:textbox id="txtACCOUNT_NUMBER" runat="server"  size="20" MaxLength="20" ></asp:textbox></br>
      <%-- <asp:RegularExpressionValidator ID="revACCOUNT_NUMBER" ControlToValidate="txtACCOUNT_NUMBER" runat="server" Display="Dynamic" ></asp:RegularExpressionValidator>--%>
       </td>
        <td class="midcolora" width="33%"></td>
		</tr>					        
		<tr>
        <td class="midcolora" width="33%"><asp:label id="capBANK_NAME" runat="server">BANK_NAME</asp:label>
         <br />
         <asp:textbox id="txtBANK_NAME" runat="server" MaxLength="50" size="25"></asp:textbox>
         </td>
      <td class="midcolora" width="33%"><asp:label id="capBANK_BRANCH" runat="server">BANK_BRANCH</asp:label>
       <br />
     <asp:textbox id="txtBANK_BRANCH" MaxLength="10" runat="server" ></asp:textbox></BR>
      <%-- <asp:RegularExpressionValidator ID=revBANK_BRANCH ControlToValidate="txtBANK_BRANCH" runat="server" Display="Dynamic"></asp:RegularExpressionValidator>--%>
       </td>
      <td class="midcolora" width="33%"><asp:Label runat="server" ID="capBANK_NUMBER">BANK_NUMBER</asp:Label>
       <br />
      <asp:TextBox runat="server" ID="txtBANK_NUMBER" MaxLength="5" ></asp:TextBox><br />
     <%--<asp:RegularExpressionValidator ID="revBANK_NUMBER" runat="server" 
      ControlToValidate="txtBANK_NUMBER" Display="Dynamic"></asp:RegularExpressionValidator>--%>
      </td>
      </tr>
      <tr style="display: none">
            <td class="midcolora" width="33%">
                <asp:Label ID="capCUSTOMER_SUFFIX" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="34%">
                <asp:TextBox ID="txtCUSTOMER_SUFFIX" runat="server" size="6" MaxLength="5"></asp:TextBox>
            </td>
            <td class="midcolora" width="33%">
                <asp:TextBox runat="server" AutoCompleteType="None" ID="txtCOMPLIMENT" size="60"
                    MaxLength="35"></asp:TextBox>
            </td>
        </tr>
        <tr id="rd1" style="display: none">
            <td class="midcolora" width="33%">
                
            </td>
            <td class="midcolora" width="34%">
                
            </td>
            <td class="midcolora" width="33%">
                <asp:Label ID="capSSN_NO" runat="server" Visible="false"></asp:Label><br />
            
                <asp:Label ID="capSSN_NO_HID" Visible="false" runat="server" size="14" maxlength="11"></asp:Label>
                <input class="clsButton" style="display: none" id="btnSSN_NO" text="Edit" visible="false"
                   type="button"></input>
                <asp:TextBox ID="txtSSN_NO" runat="server" Visible="false" size="14" MaxLength="11"
                    AutoComplete="Off"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revSSN_NO" runat="server" Visible="false" Enabled="false"
                    ControlToValidate="txtSSN_NO" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr id="rd2" style="display: none">
            <td class="midcolora" width="33%">

            </td>
            <td class="midcolora" width="34%">
             
            </td>
            <td class="midcolora" width="33%">
                <%--<A href="#"><asp:image id="imgZipLookup" runat="server" ImageAlign="Bottom" ImageUrl="/cms/cmsweb/images/info.gif"></asp:image></A>--%>
               </td>
        </tr>
        <tr style="display: none">
            <td class="midcolora" width="33%">
                <asp:Label ID="capCUSTOMER_ADDRESS2" Visible="false" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="34%">
            </td>
             <td class="midcolora" width="33%">
            </td>
        </tr>
        <tr style="display: none">
            <td class="midcolora" width="33%">
            </td>
            <td class="midcolora" width="34%">
            </td>
            <td class="midcolora" id="tdPER_CUST_MOBILE1" width="33%">
                <asp:Label ID="capPER_CUST_MOBILE" runat="server"></asp:Label>
            
                <asp:TextBox ID="txtPER_CUST_MOBILE" runat="server" size="16" MaxLength="13"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revPER_CUST_MOBILE" Enabled="false" runat="server" ControlToValidate="txtPER_CUST_MOBILE"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <%--<tr style="display: none">
            <td class="midcolora" width="33%">
                <span id="span_Agency_cap">
                    <asp:Label ID="capCUSTOMER_AGENCY_ID" runat="server"></asp:Label><span class="mandatory">*</span></span>
            </td>
            <td class="midcolora" width="34%" >
            </td><td class="midcolora" width="33%" >
            </td>
        </tr>--%>
        <tr style="display: none">
            <td class="headerEffectSystemParams"  colspan="3" visible="false">
                Insurance Score
            </td>
        </tr>
        <tr style="display: none">
            <td class="midcolora" width="33%" visible="false">
                <asp:Label ID="capCUSTOMER_INSURANCE_SCORE" Visible="false" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="34%" visible="false">
                <asp:CheckBox ID="chkSCORE" runat="server" Visible="false"></asp:CheckBox>
                <asp:Label ID="capSCORE" runat="server" Visible="false"></asp:Label><br>
                <asp:TextBox ID="txtCUSTOMER_INSURANCE_SCORE" Visible="false" runat="server" size="8"
                    MaxLength="3"></asp:TextBox>
                <cmsb:CmsButton class="clsButton" ID="btnGetInsuranceScore" Visible="false" runat="server"
                    Text="Get Insurance Score" CausesValidation="False"></cmsb:CmsButton><br>
                <asp:Label ID="lblSCORE" runat="server" Visible="false"></asp:Label><br>
                <%--(Insurance Score can be obtained when Customer is Saved)<br>--%>
                <asp:Label ID="lblCUSTOMER_INSURANCE_SCORE" Visible="False" runat="server"></asp:Label>
                <asp:RegularExpressionValidator ID="revCUSTOMER_INSURANCE_SCORE" runat="server" Enabled="false"
                    Visible="false" ControlToValidate="txtCUSTOMER_INSURANCE_SCORE" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td class="midcolora" width="33%">
                <asp:Label ID="capCUSTOMER_INSURANCE_RECEIVED_DATE" runat="server"></asp:Label>
           <br />
                <asp:TextBox ID="txtCUSTOMER_INSURANCE_RECEIVED_DATE" runat="server" size="13" MaxLength="10"></asp:TextBox><asp:HyperLink
                    ID="hlkCUSTOMER_INSURANCE_RECEIVED_DATE" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgCO_APPL_DOB" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif">
                    </asp:Image>
                </asp:HyperLink><br>
                <asp:RegularExpressionValidator ID="revCUSTOMER_INSURANCE_RECEIVED_DATE" Enabled="false"
                    runat="server" ControlToValidate="txtCUSTOMER_INSURANCE_RECEIVED_DATE" Display="Dynamic"></asp:RegularExpressionValidator><asp:CustomValidator
                        ID="csvCUSTOMER_INSURANCE_RECEIVED_DATE" runat="server" ControlToValidate="txtCUSTOMER_INSURANCE_RECEIVED_DATE"
                        Display="Dynamic" Enabled="false" ClientValidationFunction="ChkDate"></asp:CustomValidator>
            </td>
        </tr>
        <tr style="display: none">
           
                        <td class="midcolora" width="17%">
                            <asp:Label ID="capCUSTOMER_REASON_CODE" Visible="false" runat="server"></asp:Label>
                        <br />
                            <asp:DropDownList ID="cmbCUSTOMER_REASON_CODE" Visible="false" onfocus="SelectComboIndex('cmbCUSTOMER_REASON_CODE')"
                                runat="server">
                            </asp:DropDownList>
                            <a class="calcolora" visible="false" href="javascript:showPageLookupLayer('cmbCUSTOMER_REASON_CODE')">
                                <img height="16" src="/cms/cmsweb/images/info.gif" visible="false" width="17" align="bottom"
                                    border="0"></a>
                        </td>
                        <td class="midcolora" width="10.5%">
                            <asp:Label ID="capCUSTOMER_REASON_CODE2" Visible="false" runat="server"></asp:Label>
                        <br />
                            <asp:DropDownList ID="cmbCUSTOMER_REASON_CODE2" Visible="false" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="midcolora" width="10.5%">
                            <asp:Label ID="capCUSTOMER_REASON_CODE3" runat="server" Visible="false"></asp:Label>
                       <br />
                            <asp:DropDownList ID="cmbCUSTOMER_REASON_CODE3" runat="server" Visible="false">
                            </asp:DropDownList>
                        </td>          
            
        </tr>
        <tr>
        <td colspan="3" style="display:none">        
                            <asp:Label ID="capCUSTOMER_REASON_CODE4" runat="server" Visible="false"></asp:Label>
                       <br />
                            <asp:DropDownList ID="cmbCUSTOMER_REASON_CODE4" runat="server" Visible="false">
                            </asp:DropDownList>
                       
        </td>
        </tr>
        
        <tr id="trHeader">
            <td class="headerEffectSystemParams" colspan="3">
            <%--    Main Contact Information--%>
            <asp:Label ID="capMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <!-- Main_title  -->
        
        <tr>
            <td class="midcolora" colspan="3">
             <cmsb:CmsButton class="clsButton" ID="btnCopyCustomerAddress"  runat="server" CausesValidation="False"></cmsb:CmsButton>
            </td>
        </tr>
        <tr>
            <td class="midcolora" width="34%" colspan="3">
                <asp:Label runat="server" ID="capMAIN_TITLE">Title</asp:Label><br />
                <asp:DropDownList runat="server" ID="cmbMAIN_TITLE"  Width="90%" onchange="setMAIN_TITLE();">
                </asp:DropDownList>
             <asp:HyperLink ID="hlkMainTitleLookup" runat="server" NavigateUrl="javascript:showPageLookupLayer('cmbMAIN_TITLE')">
               <asp:Image ID="imgMainTitleLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom" Visible="true" Height="16" Width="17" />
                </asp:HyperLink>
                   <%--<a class="calcolora" href="" >
                    <img height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></a>--%>
                    <%--<input type="hidden" runat="server" id="hidMAIN_TITLE" />--%>
           
            </td>
            
           </tr>
           
            
        <!-- First Name,Middele Name,Last Name  -->
        <tr>
            <td class="midcolora" width="33%">
                <asp:Label runat="server" ID="capMAIN_FIRST_NAME">First Name</asp:Label><br />
                <asp:TextBox runat="server" ID="txtMAIN_FIRST_NAME" AutoCompleteType="Disabled" CausesValidation="true" MaxLength="200"></asp:TextBox><br />
                <%--<asp:RequiredFieldValidator ID="rfvMAIN_FIRST_NAME" runat="server" ControlToValidate="txtMAIN_FIRST_NAME"
                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
            </td>
            <td class="midcolora" width="34%">
                <asp:Label  style="display:none"  runat="server" ID="capMAIN_MIDDLE_NAME">Middle Name</asp:Label><br />
                <asp:TextBox  style="display:none"  runat="server" ID="txtMAIN_MIDDLE_NAME" AutoCompleteType="Disabled"
                    CausesValidation="true" MaxLength="100"></asp:TextBox>
            </td>
            <td class="midcolora" width="33%">
                <asp:Label  style="display:none" runat="server" ID="capMAIN_LAST_NAME">Last Name</asp:Label><br />
                <asp:TextBox  style="display:none" runat="server" ID="txtMAIN_LAST_NAME" MaxLength="100" AutoCompleteType="Disabled" CausesValidation="true"></asp:TextBox><br />
              <%--  <asp:RequiredFieldValidator runat="server" ID="rfvMAIN_LAST_NAME" Display="Dynamic"
                    ControlToValidate="txtMAIN_LAST_NAME"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr style="display: none">
            <td class="midcolora" width="33%">
            </td>
            <td class="midcolora" colspan="2"> 
                <asp:Label ID="capCUSTOMER_CONTACT_NAME" runat="server">Contact Name</asp:Label>
                <br />
                <asp:TextBox AutoCompleteType="Disabled" ID="txtCUSTOMER_CONTACT_NAME" runat="server"
                    size="35" MaxLength="35"></asp:TextBox>
            </td>
        </tr>
        <!-- Contact Code,Zip Code,Address,  -->
        <tr id="ttrMAIN_CONTACT" runat="server">
            <td class="midcolora" width="33%">
                <asp:Label ID="capMAIN_CONTACT_CODE" runat="server">Contact Code</asp:Label> 
                <br />
                <asp:TextBox runat="server" AutoCompleteType="Disabled" ID="txtMAIN_CONTACT_CODE"
                    CausesValidation="true" size="25" MaxLength="35"></asp:TextBox><br />
             <%--   <asp:RequiredFieldValidator runat="server" ID="rfvMAIN_CONTACT_CODE" Display="Dynamic"
                    ErrorMessage="" ControlToValidate="txtMAIN_CONTACT_CODE"></asp:RequiredFieldValidator>--%>
                    <asp:RegularExpressionValidator ID="revMAIN_CONTACT_CODE" runat="server" Display="Dynamic"
                    ErrorMessage="" ControlToValidate="txtMAIN_CONTACT_CODE"></asp:RegularExpressionValidator>
            </td>
            <td id="ttrMAIN_ZIPCODE" runat="server" class="midcolora" width="34%">
                <asp:Label ID="capMAIN_ZIPCODE" runat="server">Zip Code</asp:Label><span id="spnMAIN_ZIPCODE" runat="server" class="mandatory">*</span>
                <br />
                <asp:TextBox ID="txtMAIN_ZIPCODE" AutoCompleteType="Disabled" runat="server" size="13"
                    MaxLength="8" OnBlur="this.value=FormatZipCode(this.value);ValidatorOnChange();" CausesValidation="true"></asp:TextBox>
                <asp:HyperLink ID="hypMAIN_ZIPCODE" runat="server" CssClass="HotSpot" Visible="true" style="display:none">
                    <asp:Image ID="imgMAIN_ZIPCODE" runat="server" ImageUrl="/cms/cmsweb/images/info.gif"
                        Visible="true" ImageAlign="Bottom" style="display:none"></asp:Image>
                </asp:HyperLink>
                <br>
               <%-- <asp:RequiredFieldValidator runat="server" ID="rfvMAIN_ZIPCODE" Display="Dynamic" ControlToValidate="txtMAIN_ZIPCODE" ErrorMessage=""></asp:RequiredFieldValidator>--%>
                <asp:CustomValidator ID="csvMAIN_ZIPCODE" runat="server" ClientValidationFunction="ChkMainResult"
                    ErrorMessage=" " Display="Dynamic" ControlToValidate="txtMAIN_ZIPCODE" ></asp:CustomValidator>
                <asp:RegularExpressionValidator ID="revMAIN_ZIPCODE" runat="server" ControlToValidate="txtMAIN_ZIPCODE"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td class="midcolora" width="33%">
                <asp:Label runat="server" ID="capMAIN_ADDRESS">Address</asp:Label>
                <br />
                <asp:TextBox runat="server" AutoCompleteType="Disabled" ID="txtMAIN_ADDRESS" CausesValidation="true"
                    size="35" MaxLength="200"></asp:TextBox><br />
               <%-- <asp:RequiredFieldValidator runat="server" ID="rfvMAIN_ADDRESS" Display="Dynamic"
                    ErrorMessage="" ControlToValidate="txtMAIN_ADDRESS"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <!--Number,Compliment,District -->
        <tr>
            <td class="midcolora" width="33%">
                <asp:Label runat="server" ID="capMAIN_NUMBER">Number</asp:Label>
                <br />
                <asp:TextBox runat="server" ID="txtMAIN_NUMBER" AutoCompleteType="Disabled" size="25"
                    MaxLength="35"></asp:TextBox><br />
               <%-- <asp:RequiredFieldValidator runat="server" ID="rfvMAIN_NUMBER" Display="Dynamic"
                    ControlToValidate="txtMAIN_NUMBER"></asp:RequiredFieldValidator>--%>
            </td>
            <td class="midcolora" width="34%">
                <asp:Label runat="server" ID="capMAIN_COMPLIMENT">Compliment</asp:Label>
                <br />
                <asp:TextBox runat="server" ID="txtMAIN_COMPLIMENT" CausesValidation="true" AutoCompleteType="Disabled" size="35"
                    MaxLength="200"></asp:TextBox>
            </td>
            <td class="midcolora" width="33%">
                <asp:Label runat="server" ID="capMAIN_DISTRICT">District</asp:Label>
                <br />
                <asp:TextBox runat="server" ID="txtMAIN_DISTRICT" size="25" AutoCompleteType="Disabled"
                    MaxLength="35"></asp:TextBox><br />
                <%--<asp:RequiredFieldValidator runat="server" ID="rfvMAIN_DISTRICT" Display="Dynamic"
                    ControlToValidate="txtMAIN_DISTRICT"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <!--City,State,Country-->
        <tr>
            <td class="midcolora" width="33%">
                <asp:Label runat="server" ID="capMAIN_CITY">City</asp:Label>
                <br />
                <asp:TextBox runat="server" ID="txtMAIN_CITY" AutoCompleteType="Disabled" MaxLength="30"></asp:TextBox><br />
               <%-- <asp:RequiredFieldValidator ID="rfvMAIN_CITY" runat="server" ControlToValidate="txtMAIN_CITY"
                    Display="Dynamic" ErrorMessage=""></asp:RequiredFieldValidator>--%>
            </td>
             <td class="midcolora" width="34%">
                <asp:Label ID="capMAIN_COUNTRY" runat="server">Country</asp:Label>
                <br />
                <asp:DropDownList ID="cmbMAIN_COUNTRY" onfocus="SelectComboIndex('cmbMAIN_COUNTRY');"
                    runat="server" onchange="javascript:fillmainstateFromCountry()">
                </asp:DropDownList>
                <br>
               <%-- <asp:RequiredFieldValidator ID="rfvMAIN_COUNTRY" Enabled="false" runat="server" ControlToValidate="cmbMAIN_COUNTRY"
                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
            </td>
            <td class="midcolora" width="33%">
                <asp:Label ID="capMAIN_STATE" runat="server">State</asp:Label><br />
                <asp:DropDownList ID="cmbMAIN_STATE" onfocus="SelectComboIndex('cmbMAIN_STATE');" runat="server"> 
                </asp:DropDownList>
                <br>
               <%-- <asp:RequiredFieldValidator ID="rfvMAIN_STATE" runat="server" ControlToValidate="cmbMAIN_STATE"
                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
            </td>
           
        </tr>
        <!--CPF/CNPJ  ,Home Phone , Mobile Phone  -->
        <tr>
            <td class="midcolora" width="33%" colspan=2>
                <asp:Label runat="server" ID="capMAIN_CPF_CNPJ">CPF/CNPJ #</asp:Label> 
                <br />
                <asp:TextBox runat="server" ID="txtMAIN_CPF_CNPJ" MaxLength="14" OnBlur="this.value=FormatCPFCNPJ(this.value); ValidatorOnChange();" AutoCompleteType="Disabled" size="25"
                    ></asp:TextBox><br />
                    <%--<asp:RequiredFieldValidator runat="server" ID="rfvMAIN_CPF_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtMAIN_CPF_CNPJ"></asp:RequiredFieldValidator>--%>
                    <asp:RegularExpressionValidator runat="server" ID="revMAIN_CPF_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtMAIN_CPF_CNPJ"></asp:RegularExpressionValidator>
                    <asp:CustomValidator runat="server" ID="csvMAIN_CPF_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtMAIN_CPF_CNPJ" ClientValidationFunction="validatCPF"></asp:CustomValidator>
            </td>
            
            <td class="midcolora" width="33%">
                <asp:Label ID="capCUSTOMER_MOBILE" runat="server">Mobile Phone</asp:Label>
                <br />
                <asp:TextBox ID="txtCUSTOMER_MOBILE" runat="server" size="35" AutoCompleteType="Disabled"  onblur="FormatBrazilPhone()"
                    MaxLength="15"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revCUSTOMER_MOBILE" runat="server" ControlToValidate="txtCUSTOMER_MOBILE"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <!--Business Phone,Extension,Fax No,  -->
        <tr>
            <td class="midcolora" width="33%">
                <asp:Label ID="capCUSTOMER_BUSINESS_PHONE" runat="server">Business Phone</asp:Label><br />
                <asp:TextBox ID="txtCUSTOMER_BUSINESS_PHONE" AutoCompleteType="Disabled" runat="server" onblur="FormatBrazilPhone()"
                    size="25" MaxLength="15"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revCUSTOMER_BUSINESS_PHONE" runat="server" ControlToValidate="txtCUSTOMER_BUSINESS_PHONE"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td class="midcolora" width="34%">
                <asp:Label ID="capCUSTOMER_EXT" runat="server">Extension</asp:Label><br />
                <asp:TextBox ID="txtCUSTOMER_EXT" runat="server" size="7"
                    MaxLength="6" AutoCompleteType="Disabled"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revCUSTOMER_EXT" runat="server" ControlToValidate="txtCUSTOMER_EXT"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>   
            <td class="midcolora" width="33%">
                <asp:Label ID="capCUSTOMER_FAX" runat="server">Fax Number</asp:Label>
                <br />
                <asp:TextBox ID="txtCUSTOMER_FAX" runat="server" size="25" AutoCompleteType="Disabled" onblur="FormatBrazilPhone()"
                    MaxLength="15"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revCUSTOMER_FAX" runat="server" ControlToValidate="txtCUSTOMER_FAX"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <!--Email Address,Position,Regional Identification,  -->
        <tr id="trMAIN_POSITION">
          
            <td class="midcolora" width="34%" colspan="3">
                <asp:Label runat="server" ID="capMAIN_POSITION"></asp:Label> 
                </br>
                <asp:DropDownList runat="server" ID="cmbMAIN_POSITION" onchange="setMAIN_POSTION();" CausesValidation="true" Width="100%" >
                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                </asp:DropDownList>
                 <a class="calcolora" href="javascript:showPageLookupLayer('cmbMAIN_POSITION')">
                    <img height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></a>
               <br />
             <%--   <asp:RequiredFieldValidator ID="rfvAPPLICANT_OCCU" runat="server" ControlToValidate="cmbMAIN_POSITION"
                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
            </td>
            </tr>
            <tr>
             <td class="midcolora" width="33%" colspan="3"> <asp:Label runat="server" ID="capMAIN_NOTE">Remarks</asp:Label>
                <br />
                <asp:TextBox runat="server" AutoCompleteType="None" ID="txtMAIN_NOTE" onkeypress="MaxLength(this,500);" onpaste="MaxLength(this,500)" TextMode="MultiLine"
                   Rows="3"  Columns="45"></asp:TextBox><br />
                    <asp:CustomValidator runat="server" ID="csvMAIN_NOTE" Display="Dynamic" ControlToValidate="txtMAIN_NOTE" ClientValidationFunction="CountLength" ></asp:CustomValidator>
                    </td>
           
        </tr>
        
        
        <tr id="trRNE">
        <td class="midcolora" width="33%">
          <asp:Label ID="capREGIONAL_IDENTIFICATION_TYPE" runat="server">Regional Identification Type</asp:Label><br />
          <asp:TextBox ID="txtREGIONAL_IDENTIFICATION_TYPE" runat="server" MaxLength="50" size="30"></asp:TextBox><br />
          <%--<asp:RequiredFieldValidator ID="rfvRegional_Identification_Type" ControlToValidate="txtRegional_Identification_Type" runat="server" Display="Dynamic"></asp:RequiredFieldValidator>--%>
         </td>
         <td class="midcolora" width="33%">
            <asp:Label id="capNATIONALITY" runat="server">Nationality</asp:Label><br />
            <asp:TextBox ID="txtNATIONALITY" runat="server" MaxLength="50" size="30"></asp:TextBox><br />
             <%--<asp:RequiredFieldValidator ID="rfvcapNationality" runat="server" ControlToValidate="captxt_Nationality"
                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
        </td>
        <td class="midcolora" width="33%">
                <asp:Label ID="capEMAIL_ADDRESS" runat="server">Email Address</asp:Label>
                 <br />
                <asp:TextBox ID="txtEMAIL_ADDRESS" runat="server" size="35" AutoCompleteType="Disabled"
                    MaxLength="50"></asp:TextBox><br>
               <asp:RegularExpressionValidator ID="revEMAIL_ADDRESS" runat="server" ControlToValidate="txtEMAIL_ADDRESS"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            
            </td>
       </tr>
       
        
        <!--reg Id Issue,Original Issue,Remark -->
        <!-- Marital Status,Gender, Remarks  -->
        
        <tr style="display: none">
            <td class="midcolora" width="33%">
                <asp:Label ID="capEMPLOYER_EMAIL" runat="server">Email Address</asp:Label>
            </td>
            <td class="midcolora" width="34%">
                <asp:TextBox ID="txtEMPLOYER_HOMEPHONE" runat="server" size="35" MaxLength="13"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revEMPLOYER_HOMEPHONE" runat="server" ControlToValidate="txtEMPLOYER_HOMEPHONE"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td class="midcolora" width="33%">
            </td>
        </tr>
        <tr id="trName" style="display: none">      
                
                <td class="midcolora" width="33%">
                    <asp:TextBox ID="txtEMPLOYER_EMAIL" runat="server" MaxLength="50" size="25"></asp:TextBox>
                    <br />
                    <asp:RegularExpressionValidator ID="revEMPLOYER_EMAIL" runat="server" ControlToValidate="txtEMPLOYER_EMAIL"
                        Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
                <td class="midcolora" width="34%">
                    <asp:Label ID="capEMPLOYER_CITY" runat="server">City</asp:Label>
                </td>
                <td class="midcolora" width="33%">
                </td>
          
        </tr>
        <tr id="trPhone" style="display: none">
            <td class="midcolora" width="33%">
                <asp:TextBox ID="txtEMPLOYER_ZIPCODE" runat="server" size="13" MaxLength="10" OnBlur="GetEmpZipForState();EmpDetails_DisableZipForCanada();"></asp:TextBox>
                <asp:HyperLink ID="hlkEmpZipLookup" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgEmpZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif"
                        ImageAlign="Bottom"></asp:Image>
                </asp:HyperLink>
                <br>
                <asp:CustomValidator ID="csvEMPLOYER_ZIPCODE" Enabled="false" Visible="false" runat="server"
                    ClientValidationFunction="ChkEmpResult" ErrorMessage=" " Display="Dynamic" ControlToValidate="txtEMPLOYER_ZIPCODE"></asp:CustomValidator>
                <asp:RegularExpressionValidator ID="revEMPLOYER_ZIPCODE" Enabled="false" Visible="false"
                    runat="server" ControlToValidate="txtEMPLOYER_ZIPCODE" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td class="midcolora" width="34%">
            </td>
            <td class="midcolora" width="33%">
                <asp:TextBox ID="txtEMPLOYER_CITY" runat="server" MaxLength="150" size="35"></asp:TextBox>
            </td>
        </tr>
        <tr style="display: none">
            <td class="midcolora" width="33%">
                <asp:Label ID="capEMP_EXT" runat="server">Extension</asp:Label>
                <asp:TextBox ID="txtEMP_EXT" runat="server" size="5" MaxLength="4" AutoCompleteType="Disabled"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revEMP_EXT" Enabled="false" runat="server" ControlToValidate="txtEMP_EXT"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td class="midcolora" width="34%">
            </td>
            <td class="midcolora" width="33%">
            </td>
        </tr>
        <tr id="trMobile" style="display: none">
            <td class="midcolora" width="33%">
                <asp:Label ID="capEMPLOYER_ZIPCODE" runat="server">Zip Code</asp:Label>
            </td>
            <td class="midcolora" width="34%">
                
            </td>
            <td class="midcolora" width="33%">
              
            </td>
        </tr>
        <tr id="trFax" style="display: none">
            <td class="midcolora" width="33%">
                <asp:Label ID="capEMPLOYER_HOMEPHONE" runat="server">Home Phone</asp:Label>
            </td>
            <td class="midcolora" width="34%">
                <asp:Label ID="capCUSTOMER_PAGER_NO" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="33%">
                <asp:TextBox ID="txtCUSTOMER_PAGER_NO" runat="server" size="19" MaxLength="15"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revCUSTOMER_PAGER_NO" runat="server" Enabled="false"
                    ControlToValidate="txtCUSTOMER_PAGER_NO" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr id="trBusiness" style="display: none">
            <td class="midcolora" width="33%">
            </td>
            <td class="midcolora" width="34%">
                <asp:DropDownList ID="cmbCUSTOMER_BUSINESS_TYPE" onfocus="SelectComboIndex('cmbCUSTOMER_BUSINESS_TYPE')"
                    runat="server" Visible="False">
                </asp:DropDownList>
                
                
            </td>
            <td class="midcolora" width="33%">
                
            </td>
        </tr>
        <tr id="trHeader1" style="display: none">
            <td class="headerEffectSystemParams" colspan="3">
                Employer Details
            </td>
        </tr>
        <tr id="AppDetail1" style="display: none">
            <td class="midcolora" width="33%">
                <asp:Label ID="capAPPLICANT_OCCU" runat="server" Visible="false"></asp:Label><span
                    class="mandatory">*</span>
            </td>
            <td class="midcolora" width="34%">
                <asp:DropDownList ID="cmbAPPLICANT_OCCU" runat="server">
                </asp:DropDownList>
                <a class="calcolora" href="javascript:showPageLookupLayer('cmbAPPLICANT_OCCU')">
                    <img height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></a>
                <br>
            </td>
            <td class="midcolora" width="33%">
                <asp:Label ID="capDESC_APPLICANT_OCCU" runat="server"></asp:Label><span class="mandatory"
                    id="spnDESC_APPLICANT_OCCU">*</span>
            <br />
                <asp:TextBox ID="txtDESC_APPLICANT_OCCU" runat="server" size="30" MaxLength="200"></asp:TextBox><asp:Label
                    ID="lblDESC_APPLICANT_OCCU" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                <asp:RequiredFieldValidator ID="rfvDESC_APPLICANT_OCCU" runat="server" Enabled="false"
                    ControlToValidate="txtDESC_APPLICANT_OCCU" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="trHeader2" style="display: none">
            <td class="midcolora" width="33%">
                <asp:Label ID="capEMPLOYER_NAME" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="34%">
                <asp:TextBox ID="txtEMPLOYER_NAME" runat="server" size="30" MaxLength="150"></asp:TextBox>
            </td>
            <td class="midcolora" width="33%">
                <asp:Label ID="capEMPLOYER_ADDRESS" runat="server"></asp:Label>
            <br />
                <asp:TextBox ID="txtEMPLOYER_ADDRESS" runat="server" size="30" MaxLength="150" TextMode="multiline"></asp:TextBox>
            </td>
           
        </tr>
        <tr id="trHeader3" style="display: none">
            <td class="midcolora" width="33%">
                <asp:Label ID="capEMPLOYER_ADD1" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="34%">
                <asp:TextBox ID="txtEMPLOYER_ADD1" runat="server" size="30" MaxLength="150"></asp:TextBox>
            </td>
            <td class="midcolora" width="33%">
                <asp:Label ID="capEMPLOYER_ADD2" runat="server"></asp:Label>
          <br />
                <asp:TextBox ID="txtEMPLOYER_ADD2" runat="server" size="30" MaxLength="150"></asp:TextBox>
            </td>
        </tr>
        <tr id="trHeader4" style="display: none">
            <td class="midcolora" width="33%">
                <asp:DropDownList ID="cmbEMPLOYER_STATE" onchange="EmpDetails_setStateID();" onfocus="SelectComboIndex('cmbEMPLOYER_STATE');"
                    runat="server">
                </asp:DropDownList>
                <br>
                <asp:RequiredFieldValidator ID="rfvEMPLOYER_STATE" Enabled="false" runat="server"
                    ControlToValidate="cmbEMPLOYER_STATE" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td class="midcolora" width="34%">
            </td>
            <td class="midcolora" width="33%">
            <br />
                <asp:Label ID="capEMPLOYER_STATE" runat="server">State</asp:Label>
            </td>
        </tr>
      
        <tr id="trHeader6" style="display: none">
            <%--Employer Business Phone is saved in DB as HOME PHONE --%>
            <td class="midcolora" width="33%">
                <asp:Label ID="capEMPLOYER_COUNTRY" runat="server">Country</asp:Label>
            </td>
            <td class="midcolora" width="34%">
                <asp:DropDownList ID="cmbEMPLOYER_COUNTRY" onfocus="SelectComboIndex('cmbEMPLOYER_COUNTRY');"
                    runat="server" onchange="EmpDetails_fillstateFromCountry();">
                </asp:DropDownList>
                <br>
                <asp:RequiredFieldValidator ID="rfvEMPLOYER_COUNTRY" Enabled="false" runat="server"
                    ControlToValidate="cmbEMPLOYER_COUNTRY" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td class="midcolora" width="33%">
            </td>
           
        </tr>
       
        <tr id="trHeader7" style="display: none">
            <td class="midcolora" width="33%">
                <asp:Label ID="capYEARS_WITH_CURR_OCCU" Visible="false" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="34%">
                <asp:TextBox ID="txtYEARS_WITH_CURR_OCCU" Visible="false" runat="server" size="3"
                    MaxLength="2"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revYEARS_WITH_CURR_OCCU" runat="server" Visible="false"
                    Enabled="false" ControlToValidate="txtYEARS_WITH_CURR_OCCU" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td class="midcolora" width="33%">
                <asp:Label ID="capYEARS_WITH_CURR_EMPL" Visible="false" runat="server"></asp:Label>
           <br />
                <asp:TextBox ID="txtYEARS_WITH_CURR_EMPL" runat="server" size="3" MaxLength="2"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revYEARS_WITH_CURR_EMPL" runat="server" Visible="false"
                    ControlToValidate="txtYEARS_WITH_CURR_EMPL" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        
        <%-- <tr id="tr1">
            <td class="headerEffectSystemParams" colspan="3">
            <%--    IS POLITICALLY EXPOSED--%>
           <%-- <asp:Label ID="capMessage1" runat="server">Politically Exposed</asp:Label>
            </td>
        </tr>--%>
        
     <%--<tr>
        
         <td class="midcolora" width="34%" colspan="3">
          <asp:checkbox id="chkType" runat="server" Text="Is Politically Exposed" AutoPostBack="false" onClick="checkRadioButton()"></asp:checkbox>
             
         </td>
         
        </tr>--%>
             
         
        
        <!-- Buttons -->
        <tr>
            <td class="midcolora" colspan="2">
                <!--Nov 08,2005:Sumit Chhabra: Two new buttons for adding new quick quote and new application have been added-->
                <cmsb:CmsButton class="clsButton" ID="btnAddNewQuickQuote" Visible="true" runat="server"
                    Text="Add New Quick Quote"></cmsb:CmsButton>
                <cmsb:CmsButton class="clsButton" ID="btnAddNewApplication" runat="server" Text="Add New Application">
                </cmsb:CmsButton>
                <cmsb:CmsButton class="clsButton" ID="btnBack" runat="server" Text="Back To Search"
                    Visible="false"></cmsb:CmsButton>
                <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" Text="Reset"></cmsb:CmsButton>
                <cmsb:CmsButton class="clsButton" ID="btnActivateDeactivate" Width="155px" runat="server"
                    Text="Activate/Deactivate" CausesValidation="false"></cmsb:CmsButton>
                <cmsb:CmsButton class="clsButton" ID="btnCustomerAssistant" runat="server" Width="210px"
                    Text="Back To Customer Assistant"></cmsb:CmsButton>
            </td>
            <td class="midcolorr" colspan="1">
                <cmsb:CmsButton class="clsButton" ID="btnViewMap" runat="server" Text="View Map"
                    Visible="false"></cmsb:CmsButton>
                <cmsb:CmsButton class="clsButton" ID="btnSave" CausesValidation="true" runat="server"
                    Text="Save"></cmsb:CmsButton><cmsb:CmsButton class="clsButton" ID="btnCopyClient" runat="server" Text="Copy Client"></cmsb:CmsButton>
            </td>
        </tr>
    </table>
    <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
    <input id="hidCUSTOMER_ID" type="hidden" value="0" name="Hidden1" runat="server">
    <input id="hidCUSTOMER_PARENT" type="hidden" name="hidCUSTOMER_PARENT" runat="server">
    <input id="hidOldData" type="hidden" name="Hidden1" runat="server">
    <input id="hidIS_ACTIVE" type="hidden" name="Hidden1" runat="server">
    <input id="Hidden1" type="hidden" name="Hidden1" runat="server">
    <input id="hidOldXML" type="hidden" name="hidOldXML" runat="server">
    <input id="hidRefreshTabIndex" type="hidden" name="hidRefreshTabIndex" runat="server">
    <input id="hidSaveMsg" type="hidden" name="hidSaveMsg" runat="server">
    <input id="hidCarrierId" type="hidden" name="hidCarrierId" runat="server">
    <input id="hidLAST_INSURANCE_SCORE_FETCHED" type="hidden" name="hidLAST_INSURANCE_SCORE_FETCHED"
        runat="server">
    <input id="hidDT_LAST_INSURANCE_SCORE_FETCHED" type="hidden" name="hidDT_LAST_INSURANCE_SCORE_FETCHED"
        runat="server">
    <input type="hidden" runat="server" id="hidMessage" />
    <input id="hidMsg" type="hidden" name="hidMsg" runat="server">
    <input id="hidCust_Type" type="hidden" name="hidCust_Type" runat="server">
    <input id="hidPREFIX" type="hidden" name="hidPREFIX" runat="server">
    <input id="hidTabInsScore" type="hidden" name="hidTabInsScore" runat="server">
    <input id="hidBackToApplication" type="hidden" value="0" name="hidBackToApplication"
        runat="server">
    <input id="hidCustomer_AGENCY_ID" type="hidden" name="hidCustomer_AGENCY_ID" runat="server">
    <input id="hidSSN_NO" type="hidden" name="hidSSN_NO" runat="server">
    <input id="hidSTATE_COUNTRY_LIST" type="hidden" name="hidSTATE_COUNTRY_LIST" runat="server">
    <!--Added by Sibin for Itrack Issue 4843-->
    <input id="hidEmpDetails_STATE_COUNTRY_LIST" type="hidden" name="hidEmpDetails_STATE_COUNTRY_LIST"
        runat="server">
    <!--Added by Sibin for Itrack Issue 4843-->
    <input id="hidSTATE_ID" type="hidden" name="hidSTATE_ID" runat="server">
    <!--Added by Sibin for Itrack Issue 4843-->
    <input id="hidEmpDetails_STATE_ID" type="hidden" name="hidEmpDetails_STATE_ID" runat="server">
    <!--Added by Sibin for Itrack Issue 4843-->
    <input id="hidSTATE_ID_OLD" type="hidden" name="hidSTATE_ID_OLD" runat="server">
    <!--Added by Sibin for Itrack Issue 4843-->
    <input id="hidEmpDetails_STATE_ID_OLD" type="hidden" name="hidEmpDetails_STATE_ID_OLD"
        runat="server">
    <!--Added by Sibin for Itrack Issue 4843-->
    <input type="hidden" runat="server" id="hidDEFAULT_COUNTRY" />
    <input type="hidden" runat="server" id="hidstatus1" />
    <input type="hidden" runat="server" id="hidstatus2" />
     <input type="hidden" runat="server" id="HidBussiness" />
     <input type="hidden" runat="server" id="HidPersonal" />
    <input type="hidden" runat="server" id="hidMAIN_TITLE" />
      <input type="hidden" runat="server" id="hidMAIN_POSITION"/>
     <input type="hidden" runat="server" id="hidZipCodeCalledfor" name="hidZipCodeCalledfor" />
     <input type="hidden" runat="server" id="hidZipeCodeVerificationMsg" name="hidZipeCodeVerificationMsg" />
     <input type="hidden" runat="server" id="hidDATE_OF_BIRTH" />
     <input type="hidden" runat="server" id="hidCREATION_DATE" />
     <input type="hidden" runat="server" id="hidState_Code" />
       
    </TD> </TR> </TABLE>
    </form>
    <!-- For lookup layer -->
    <div id="lookupLayerFrame" style="display: none; z-index: 101; position: absolute">
        <iframe id="lookupLayerIFrame" style="display: none; z-index: 1001; filter: alpha(opacity=0);
            background-color: #000000" width="0" height="0" top="0px;" left="0px"></iframe>
    </div>
    <div id="lookupLayer" onmouseover="javascript:refreshLookupLayer();" style="z-index: 101;
        visibility: hidden; position: absolute">
        <table class="TabRow" cellspacing="0" cellpadding="2" width="100%">
            <tr class="SubTabRow">
                <td>
                    <b><asp:Label runat="server" Text="Add LookUp" ID="Caplook"></asp:Label></b>
                </td>
                <td>
                    <p align="right">
                        <a onclick="javascript:hideLookupLayer();" href="javascript:void(0)">
                            <img height="14" alt="Close" src="/cms/cmsweb/images/cross.gif" width="16" border="0"></a></p>
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
        //RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidCUSTOMER_ID').value,true);
        refreshtabIndex();
        if (document.getElementById('hidOldXML').value != "<NewDataSet />")
            SetActivateDeactivateButton(document.getElementById('hidOldXML').value);

        //ADDED BY RP - GEN ISSUE 3401
        //		    if (document.getElementById("txtCUSTOMER_BUSINESS_TYPE_NAME").value == "")
        //		        document.getElementById("txtCUSTOMER_BUSINESS_TYPE_NAME").value = " ";

        //Added by Mohit Agarwal 5-Sep-08
        if (document.getElementById("hidSSN_NO") != null && typeof document.getElementById("hidSSN_NO").value != 'undefined' && document.getElementById("hidSSN_NO").value != '') {
            //SSN_hide();
        }
        else {
            //SSN_change();
        }

    </script>

</body>
</html>

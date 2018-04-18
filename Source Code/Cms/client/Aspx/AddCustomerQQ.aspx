<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCustomerQQ.aspx.cs" Inherits="Cms.client.Aspx.AddCustomerQQ" %>

<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>QQ_CUSTOMER_LIST</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script src="/cms/cmsweb/scripts/common.js"></script>
	<script src="/cms/cmsweb/scripts/form.js"></script>
	<script src="/cms/cmsweb/scripts/calendar.js"></script> 
	
	<script language="javascript" type="text/javascript">

	    var jsaAppDtFormat = "<%=aAppDtFormat  %>";

	    //This function is used to refresh the contents of client top controls
	    function InsuranceScoreChange() {
	        if (document.getElementById("txtCUSTOMER_INSURANCE_SCORE").value != "" && !isNaN(document.getElementById("txtCUSTOMER_INSURANCE_SCORE").value))
	            document.getElementById("txtCUSTOMER_INSURANCE_RECEIVED_DATE").readOnly = false;
	        else
	            document.getElementById("txtCUSTOMER_INSURANCE_RECEIVED_DATE").readOnly = false // true;
	    }

	    function RefreshClientTop() {
	        var doc = this.parent.document;
	        if (doc.getElementById("cltClientTop_PanelClient") == null) {
	            if (document.getElementById("hidRefreshTabIndex").value == 'Y') {
	                var strQueryString = "CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value + "&SaveMsg=" + document.getElementById("hidSaveMsg").value;
	                parent.document.location.href = "CustomerTabIndex.aspx?" + strQueryString;
	            }
	            return;
	        }
	        doc.getElementById("cltClientTop_lblFullName").innerHTML = document.getElementById("txtCUSTOMER_FIRST_NAME").value;
	        //Done for Itrack Issue 5485 on 17 April 2009
	        if (document.getElementById("txtCUSTOMER_MIDDLE_NAME").value != '')
	            doc.getElementById("cltClientTop_lblFullName").innerHTML += ' ' + document.getElementById("txtCUSTOMER_MIDDLE_NAME").value;

	        if (document.getElementById("txtCUSTOMER_LAST_NAME").value != '')
	            doc.getElementById("cltClientTop_lblFullName").innerHTML += ' ' + document.getElementById("txtCUSTOMER_LAST_NAME").value;
	        //Added 'Suffix' for Itrack Issue 5485 on 17 April 2009
	        if (document.getElementById("txtCUSTOMER_SUFFIX").value != '')
	            doc.getElementById("cltClientTop_lblFullName").innerHTML += ' ' + document.getElementById("txtCUSTOMER_SUFFIX").value;

	        doc.getElementById("cltClientTop_lblClientType").innerHTML = document.getElementById("cmbCUSTOMER_TYPE").options[document.getElementById("cmbCUSTOMER_TYPE").selectedIndex].text;

	        doc.getElementById("cltClientTop_lblClientStatus").innerHTML = document.getElementById("lblIS_ACTIVE").innerHTML;

	        if (document.getElementById("cmbCUSTOMER_TYPE").options[document.getElementById("cmbCUSTOMER_TYPE").selectedIndex].value == "11109")
	            doc.getElementById("cltClientTop_lblClientPhone").innerHTML = document.getElementById("txtCUSTOMER_BUSINESS_PHONE").value;
	        else
	            doc.getElementById("cltClientTop_lblClientPhone").innerHTML = document.getElementById("txtCUSTOMER_HOME_PHONE").value;

	        if (document.getElementById("cmbPREFIX").options[document.getElementById("cmbPREFIX").selectedIndex].text != null) {
	            doc.getElementById("cltClientTop_lblClientTitle").innerHTML = document.getElementById("cmbPREFIX").options[document.getElementById("cmbPREFIX").selectedIndex].text;
	        }
	        var add = "";
	        add = document.getElementById("txtCUSTOMER_ADDRESS1").value + ", "
	        if (document.getElementById("txtCUSTOMER_ADDRESS2").value != "") {
	            add += document.getElementById("txtCUSTOMER_ADDRESS2").value + ", "
	        }
	        add += document.getElementById("txtCUSTOMER_CITY").value + ", "

	        add += document.getElementById("cmbCUSTOMER_STATE").options[document.getElementById("cmbCUSTOMER_STATE").selectedIndex].text + ", "
	        add += document.getElementById("cmbCUSTOMER_COUNTRY").options[document.getElementById("cmbCUSTOMER_COUNTRY").selectedIndex].text + ", "
	        add += document.getElementById("txtCUSTOMER_ZIP").value;
	        doc.getElementById("cltClientTop_lblClientStatus").innerHTML = document.getElementById("lblIS_ACTIVE").innerHTML;
	        doc.getElementById("cltClientTop_lblClientAddress").innerHTML = add;
	    }

	    function LoadTitles() {
	        if (document.getElementById('cmbCUSTOMER_TYPE').selectedIndex < 0) return;

	        document.getElementById('cmbPREFIX').length = 0;
	        if (document.getElementById('cmbCUSTOMER_TYPE').options[document.getElementById('cmbCUSTOMER_TYPE').selectedIndex].value == "11110") //customer is of Personal type
	        {
	            AddComboOption("cmbPREFIX", '0', '');
	            AddComboOption("cmbPREFIX", '768', 'Admiral');
	            AddComboOption("cmbPREFIX", '777', 'Bishop');
	            AddComboOption("cmbPREFIX", '782', 'Brother');
	            AddComboOption("cmbPREFIX", '769', 'Captain');
	            AddComboOption("cmbPREFIX", '11024', 'CEO');
	            AddComboOption("cmbPREFIX", '9768', 'Chief Financial Officer');
	            AddComboOption("cmbPREFIX", '9769', 'Chief Information Officer');
	            AddComboOption("cmbPREFIX", '9770', 'Chief Operating Officer');
	            AddComboOption("cmbPREFIX", '765', 'Col.');
	            AddComboOption("cmbPREFIX", '9771', 'Comptroller');
	            AddComboOption("cmbPREFIX", '11020', 'Director');
	            AddComboOption("cmbPREFIX", '760', 'Dr.');
	            AddComboOption("cmbPREFIX", '787', 'Dr. & Mrs.');
	            AddComboOption("cmbPREFIX", '786', 'Drs.');
	            AddComboOption("cmbPREFIX", '773', 'Father');
	            AddComboOption("cmbPREFIX", '770', 'General');
	            AddComboOption("cmbPREFIX", '779', 'Gentlemen');
	            AddComboOption("cmbPREFIX", '774', 'Governor');
	            AddComboOption("cmbPREFIX", '780', 'Judge');
	            AddComboOption("cmbPREFIX", '789', 'Ladies');
	            AddComboOption("cmbPREFIX", '788', 'Ladies and Gentlemen');
	            AddComboOption("cmbPREFIX", '764', 'Lt.');
	            AddComboOption("cmbPREFIX", '766', 'Lt. Col.');
	            AddComboOption("cmbPREFIX", '767', 'Major');
	            AddComboOption("cmbPREFIX", '10113', 'Member');
	            AddComboOption("cmbPREFIX", '762', 'Miss');
	            AddComboOption("cmbPREFIX", '757', 'Mr.');
	            AddComboOption("cmbPREFIX", '771', 'Mr. & Mrs.');
	            AddComboOption("cmbPREFIX", '758', 'Mrs.');
	            AddComboOption("cmbPREFIX", '759', 'Ms.');
	            AddComboOption("cmbPREFIX", '784', 'No Title - Use First Name');
	            AddComboOption("cmbPREFIX", '9772', 'Owner');
	            AddComboOption("cmbPREFIX", '9773', 'Partner');
	            AddComboOption("cmbPREFIX", '2573', 'President');
	            AddComboOption("cmbPREFIX", '9774', 'Proprietor');
	            AddComboOption("cmbPREFIX", '775', 'Rabbi');
	            AddComboOption("cmbPREFIX", '785', 'Rev. And Mrs.');
	            AddComboOption("cmbPREFIX", '761', 'Reverend');
	            AddComboOption("cmbPREFIX", '11022', 'Secretary');
	            AddComboOption("cmbPREFIX", '783', 'Senator');
	            AddComboOption("cmbPREFIX", '763', 'Sgt.');
	            AddComboOption("cmbPREFIX", '776', 'Sir');
	            AddComboOption("cmbPREFIX", '778', 'Sirs');
	            AddComboOption("cmbPREFIX", '781', 'Sister');
	            AddComboOption("cmbPREFIX", '772', 'The Honorable');
	            AddComboOption("cmbPREFIX", '11023', 'Treasurer');
	            AddComboOption("cmbPREFIX", '11021', 'Vice President');
	        }
	        else					//Customer is of Commercial type
	        {
	            AddComboOption("cmbPREFIX", '0', '');
	            AddComboOption("cmbPREFIX", '11698', 'Association');
	            AddComboOption("cmbPREFIX", '11718', 'Bank');
	            AddComboOption("cmbPREFIX", '11024', 'CEO');
	            AddComboOption("cmbPREFIX", '11726', 'Church');
	            AddComboOption("cmbPREFIX", '11699', 'City Commission');
	            AddComboOption("cmbPREFIX", '11724', 'City Department');
	            AddComboOption("cmbPREFIX", '11700', 'Co-Employers');
	            AddComboOption("cmbPREFIX", '11719', 'Condominiums/Condo Associations');
	            AddComboOption("cmbPREFIX", '11701', 'Corporation');
	            AddComboOption("cmbPREFIX", '11702', 'Corporations (More Than One)');
	            AddComboOption("cmbPREFIX", '11703', 'County');
	            AddComboOption("cmbPREFIX", '11704', 'Estate Of');
	            AddComboOption("cmbPREFIX", '11705', 'Fraternity');
	            AddComboOption("cmbPREFIX", '11706', 'Government Agency');
	            AddComboOption("cmbPREFIX", '11727', 'Hospital');
	            AddComboOption("cmbPREFIX", '11707', 'Individual');
	            AddComboOption("cmbPREFIX", '11708', 'Individuals (More Than One)');
	            AddComboOption("cmbPREFIX", '11730', 'Joint Venture');
	            AddComboOption("cmbPREFIX", '11722', 'Limited Corporationn');
	            AddComboOption("cmbPREFIX", '11723', 'Limited Liability Company');
	            AddComboOption("cmbPREFIX", '11709', 'Limited Partnership');
	            AddComboOption("cmbPREFIX", '11710', 'Municipality');
	            AddComboOption("cmbPREFIX", '11721', 'Not for Profit Organization');
	            AddComboOption("cmbPREFIX", '11729', 'Organizations/Clubs');
	            AddComboOption("cmbPREFIX", '11711', 'Other');
	            AddComboOption("cmbPREFIX", '11713', 'Partnership');
	            AddComboOption("cmbPREFIX", '11712', 'Proprietorship');
	            AddComboOption("cmbPREFIX", '11717', 'Savings & Loan');
	            AddComboOption("cmbPREFIX", '11728', 'School');
	            AddComboOption("cmbPREFIX", '11714', 'School Board');
	            AddComboOption("cmbPREFIX", '11731', 'Sorority');
	            AddComboOption("cmbPREFIX", '11720', 'Subchapter S Corporation');
	            AddComboOption("cmbPREFIX", '11715', 'Township');
	            AddComboOption("cmbPREFIX", '11725', 'Trust');
	            AddComboOption("cmbPREFIX", '11716', 'Unincorporated Association');
	        }
	        //Set the value of titles based on user-preference
	        if (document.getElementById('hidPREFIX').value != "") {
	            for (i = 0; i < document.getElementById('cmbPREFIX').options.length; i++) {
	                if (document.getElementById('cmbPREFIX').options[i].value == document.getElementById('hidPREFIX').value) {
	                    document.getElementById('cmbPREFIX').options[i].selected = true;
	                    //document.getElementById('hidLOC_TERRITORY').value="";
	                    return false;
	                }
	            }
	        }
	        else
	            document.getElementById('cmbPREFIX').selectedIndex = -1;
	    }


	    function ChkDate(objSource, objArgs) {
	        var expdate = document.getElementById("txtCUSTOMER_INSURANCE_RECEIVED_DATE").value;

	        if (expdate == '') {
	            objArgs.IsValid = true;
	        }

	        objArgs.IsValid = DateComparer("<%=DateTime.Now.ToString()%>", expdate, jsaAppDtFormat);
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
	        parent.document.location.href = "/cms/Policies/Aspx/QuickQuote.aspx?CalledFrom=QAPP";
	        return false;
	    }
	    function GoToNewApplication() {
	        parent.document.location.href = "/CMS/APPLICATION/ASPX/APPLICATIONTAB.aspx?CALLEDFROM=CLT";
	        return false;
	    }
	    function OnCustomerTypeChange() {

	        if (document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex == -1)
	            document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex = 2;

	        if (document.getElementById('cmbCUSTOMER_TYPE').options[document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex].value == '11110') {

	            //Type is personal

	            //Changing error message of validation control
	            document.getElementById("rfvCUSTOMER_FIRST_NAME").innerHTML = document.getElementById("rfvCUSTOMER_FIRST_NAME").getAttribute("ErrMsgFirstName");

	            //First, middle and last name should be visible
	            document.getElementById("capCUSTOMER_FIRST_NAME").innerHTML = "First Name";
	            document.getElementById("txtCUSTOMER_FIRST_NAME").size = 25;
	            document.getElementById("tdFname").width = '25%';
	            document.getElementById("txtCUSTOMER_MIDDLE_NAME").style.display = "inline";
	            document.getElementById("txtCUSTOMER_LAST_NAME").style.display = "inline";
	            document.getElementById("capCUSTOMER_MIDDLE_NAME").style.display = "inline";
	            document.getElementById("capCUSTOMER_LAST_NAME").style.display = "inline";
	            //document.getElementById("spn_Personal1").style.display = "inline";
	            document.getElementById("spnMandatory").style.display = "inline";

	            document.getElementById("rfvCUSTOMER_LAST_NAME").setAttribute('enabled', true);
	            document.getElementById("rfvCUSTOMER_LAST_NAME").setAttribute('isValid', false);
	            //document.getElementById("revCUSTOMER_WEBSITE").setAttribute('enabled', false);
	            document.getElementById("rfvCUSTOMER_LAST_NAME").style.display = "none";


	            GenerateCustomerCode("txtCUSTOMER_LAST_NAME");

	            //contact name should be visible 
//	            document.getElementById("capCUSTOMER_CONTACT_NAME").style.display = "none";
//	            document.getElementById("txtCUSTOMER_CONTACT_NAME").style.display = "none";
//	            document.getElementById("spnCUSTOMER_CONTACT_NAME").style.display = "none";
//	            document.getElementById("trHeader").style.display = "none";

////	            document.getElementById("trMobile").style.display = "none";
////	            document.getElementById("trFax").style.display = "none";
////	            document.getElementById("trBusiness").style.display = "none";

////	            document.getElementById("trPhone").style.display = "none";
////	            document.getElementById("trHeader1").style.display = "inline";
////	            document.getElementById("trHeader2").style.display = "inline";
	            // Added by mohit on 4/11/2005
//	            document.getElementById("trHeader3").style.display = "inline";
	            //Added by Sumit on 10-04-2006
//	            document.getElementById("trHeader4").style.display = "inline";
//	            document.getElementById("trHeader5").style.display = "inline";
//	            document.getElementById("trHeader6").style.display = "inline";
//	            document.getElementById("trHeader6a").style.display = "inline";
//	            document.getElementById("trHeader7").style.display = "inline";
	            // End
//	            document.getElementById("AppDetail1").style.display = "inline";

	            document.getElementById("capDATE_OF_BIRTH").style.display = "inline";
	            document.getElementById("txtDATE_OF_BIRTH").style.display = "inline";
	            document.getElementById("rfvDATE_OF_BIRTH").setAttribute('enabled', true);
	            document.getElementById("capSSN_NO").style.display = "inline";
	            document.getElementById("txtSSN_NO").style.display = "inline";
	            //document.getElementById("btnSSN_NO").style.display = "none";
	            //Added by Mohit Agarwal 5-Sep-08
	            if (document.getElementById("hidSSN_NO") != null && typeof document.getElementById("hidSSN_NO").value != 'undefined' && document.getElementById("hidSSN_NO").value != '') {
	                //SSN_hide();
	            }
	            else {
	                //SSN_change();
	            }
	            document.getElementById("capMARITAL_STATUS").style.display = "inline";
	            document.getElementById("cmbMARITAL_STATUS").style.display = "inline";
	            document.getElementById("rfvMARITAL_STATUS").setAttribute('enabled', true);
	            document.getElementById("revDATE_OF_BIRTH").setAttribute('isValid', true);
	            document.getElementById("capGENDER").style.display = "inline";
	            document.getElementById("cmbGENDER").style.display = "inline";
	            document.getElementById("rfvGENDER").setAttribute('enabled', true);
	            document.getElementById("csvDATE_OF_BIRTH").setAttribute('enabled', true);
	            document.getElementById("hlkDATE_OF_BIRTH").style.display = "inline";
	            document.getElementById("revSSN_NO").setAttribute('isValid', true);
	            document.getElementById("spnMS").style.display = "inline";

	            document.getElementById("spnDOB").style.display = "inline";
	            document.getElementById("rd1").style.display = "inline";
	            document.getElementById("rd2").style.display = "inline";
	            //document.getElementById("capCUSTOMER_HOME_PHONE").innerHTML ="Phone";
	            document.getElementById("capPER_CUST_MOBILE").style.display = "inline";
	            document.getElementById("txtPER_CUST_MOBILE").style.display = "inline";
	            document.getElementById("txtPER_CUST_MOBILE").setAttribute('isValid', true);
////	            document.getElementById("rfvAPPLICANT_OCCU").setAttribute('isValid', true);
////	            document.getElementById("rfvAPPLICANT_OCCU").setAttribute('enabled', true);

	        }
	        else {
	            //Type is commercial
	            //Changing error message of validation control
	            document.getElementById("rfvCUSTOMER_FIRST_NAME").innerHTML = document.getElementById("rfvCUSTOMER_FIRST_NAME").getAttribute("ErrMsgCustomerName");
	            document.getElementById("tdFname").width = '50%';
	            document.getElementById("txtCUSTOMER_FIRST_NAME").size = 65;
	            document.getElementById("rfvCUSTOMER_LAST_NAME").setAttribute('enabled', false);
	            document.getElementById("txtCUSTOMER_MIDDLE_NAME").style.display = "none";
	            document.getElementById("txtCUSTOMER_LAST_NAME").style.display = "none";
	            document.getElementById("capCUSTOMER_MIDDLE_NAME").style.display = "none";
	            document.getElementById("capCUSTOMER_LAST_NAME").style.display = "none";
	            document.getElementById("spnMandatory").style.display = "none";
	            document.getElementById("rfvCUSTOMER_LAST_NAME").setAttribute('isValid', true);
//	            document.getElementById("revCUSTOMER_WEBSITE").setAttribute('enabled', true);
//	            document.getElementById("revCUSTOMER_WEBSITE").setAttribute('isValid', false);
	            document.getElementById("rfvCUSTOMER_LAST_NAME").style.display = "none";
	            document.getElementById("capDATE_OF_BIRTH").style.display = "none";
	            document.getElementById("txtDATE_OF_BIRTH").style.display = "none";
	            document.getElementById("rfvDATE_OF_BIRTH").setAttribute('enabled', false);
	            document.getElementById("capSSN_NO").style.display = "none";
	            document.getElementById("txtSSN_NO").style.display = "none";
	            document.getElementById("capSSN_NO_HID").style.display = "none";
	            //document.getElementById("btnSSN_NO").style.display = "none";
	            document.getElementById("capMARITAL_STATUS").style.display = "none";
	            document.getElementById("cmbMARITAL_STATUS").style.display = "none";
	            document.getElementById("rfvMARITAL_STATUS").setAttribute('enabled', false);
	            document.getElementById("capGENDER").style.display = "none";
	            document.getElementById("cmbGENDER").style.display = "none";
	            document.getElementById("rfvGENDER").setAttribute('enabled', false);
	            document.getElementById("revDATE_OF_BIRTH").setAttribute('isValid', false);
	            document.getElementById("csvDATE_OF_BIRTH").setAttribute('enabled', false);
	            document.getElementById("hlkDATE_OF_BIRTH").style.display = "none";
	            document.getElementById("revSSN_NO").setAttribute('isValid', false);
	            document.getElementById("spnMS").style.display = "none";
	            document.getElementById("spnDOB").style.display = "none";
	            document.getElementById("rd1").style.display = "none";
	            document.getElementById("rd2").style.display = "none";
//	            document.getElementById("rfvAPPLICANT_OCCU").setAttribute('isValid', false);
//	            document.getElementById("rfvAPPLICANT_OCCU").setAttribute('enabled', false);
	            document.getElementById("capCUSTOMER_FIRST_NAME").innerHTML = "Customer Name";
	            GenerateCustomerCode("txtCUSTOMER_FIRST_NAME")
	            //contact name should be visible 
//	            document.getElementById("capCUSTOMER_CONTACT_NAME").style.display = "inline";
//	            document.getElementById("txtCUSTOMER_CONTACT_NAME").style.display = "inline";
//	            document.getElementById("spnCUSTOMER_CONTACT_NAME").style.display = "none";
//	            document.getElementById("trHeader").style.display = "inline";
//	            document.getElementById("trMobile").style.display = "inline";
//	            document.getElementById("trFax").style.display = "inline";
//	            document.getElementById("trBusiness").style.display = "inline";
//	            document.getElementById("trPhone").style.display = "inline";
//	            document.getElementById("trHeader1").style.display = "none";
//	            document.getElementById("trHeader2").style.display = "none";
//	            document.getElementById("trHeader3").style.display = "none";
//	            document.getElementById("trHeader4").style.display = "none";
//	            document.getElementById("trHeader5").style.display = "none";
//	            document.getElementById("trHeader6").style.display = "none";
//	            document.getElementById("trHeader6a").style.display = "none";
//	            document.getElementById("trHeader7").style.display = "none";
//	            document.getElementById("AppDetail1").style.display = "none";
//	            document.getElementById("capPER_CUST_MOBILE").style.display = "none";
//	            document.getElementById("txtPER_CUST_MOBILE").style.display = "none";
//	            document.getElementById("txtPER_CUST_MOBILE").setAttribute('isValid', false);
	        }
	        //Call to new method to display titles based on the type of customer
	        LoadTitles();

	    }

	    //This function is used to generate the new code to the customer
	    //calls in the blur event of CustomerFirstName and CustomeLastName
	    function CustomerLength(crtl) {
	        if (document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex == 2) {

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
	    function GenerateCustomerCode(Ctrl) {

	        //we r generating the code only when user is in add mode
	        //Hence checking the mode of form
	        if (document.getElementById('hidCUSTOMER_ID').value == "New") {
	            if (document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex == 1) {
	                //Type is personal
	                //Code shoud be First 2 char and last name 2 char 

	                //Code should only be genarated when the event is coming from last name
	                if (document.getElementById("txtCUSTOMER_FIRST_NAME").value + document.getElementById("txtCUSTOMER_LAST_NAME").value != "") {
	                    document.getElementById('txtCUSTOMER_CODE').value = (GenerateRandomCode(document.getElementById('txtCUSTOMER_FIRST_NAME').value, document.getElementById('txtCUSTOMER_LAST_NAME').value));
	                }
	            }
	            else {
	                //Type is commercial
	                //Code should first name first 4 chars
	                //Code should only be genarated when the event is coming from first name
	                if (Ctrl == "txtCUSTOMER_FIRST_NAME") {
	                    if (document.getElementById("txtCUSTOMER_FIRST_NAME").value != "") {
	                        document.getElementById('txtCUSTOMER_CODE').value = (GenerateRandomCode(document.getElementById('txtCUSTOMER_FIRST_NAME').value, ''));
	                    }
	                }

	            }
	        }
	    }
	    function AddData() {

	        document.forms[0].reset();
	        document.getElementById('hidCUSTOMER_ID').value = 'New';
	        document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex = -1;
	        document.getElementById('txtCUSTOMER_PARENT_TEXT').value = '';
	        document.getElementById('hidCUSTOMER_PARENT').value = '';
	        document.getElementById('txtCUSTOMER_CODE').value = '';
	        document.getElementById('txtCUSTOMER_SUFFIX').value = '';
	        document.getElementById('txtCUSTOMER_FIRST_NAME').value = '';
	        document.getElementById('txtCUSTOMER_MIDDLE_NAME').value = '';
	        document.getElementById('txtCUSTOMER_LAST_NAME').value = '';
	        document.getElementById('txtCUSTOMER_ADDRESS1').value = '';
	        document.getElementById('txtCUSTOMER_ADDRESS2').value = '';
	        document.getElementById('txtCUSTOMER_CITY').value = '';
	        document.getElementById('cmbCUSTOMER_STATE').options.selectedIndex = -1;
	        document.getElementById('txtCUSTOMER_ZIP').value = '';
	        //document.getElementById('txtCUSTOMER_BUSINESS_DESC').value = '';
//	        document.getElementById('txtCUSTOMER_CONTACT_NAME').value = '';
//	        document.getElementById('txtCUSTOMER_BUSINESS_PHONE').value = '';
//	        document.getElementById('txtEMP_EXT').value = '';
//	        document.getElementById('txtCUSTOMER_EXT').value = '';
	        document.getElementById('txtCUSTOMER_HOME_PHONE').value = '';
	        //document.getElementById('txtCUSTOMER_MOBILE').value = '';
	        document.getElementById('txtPER_CUST_MOBILE').value = '';
//	        document.getElementById('txtCUSTOMER_FAX').value = '';
//	        document.getElementById('txtCUSTOMER_PAGER_NO').value = '';
	        document.getElementById('txtCUSTOMER_Email').value = '';
//	        document.getElementById('txtCUSTOMER_WEBSITE').value = '';
//	        document.getElementById('txtCUSTOMER_INSURANCE_SCORE').value = '';
//	        document.getElementById('txtCUSTOMER_INSURANCE_RECEIVED_DATE').value = '';
//	        document.getElementById('cmbCUSTOMER_REASON_CODE').options.selectedIndex = 0;
//	        document.getElementById('cmbCUSTOMER_REASON_CODE2').options.selectedIndex = 0;
//	        document.getElementById('cmbCUSTOMER_REASON_CODE3').options.selectedIndex = 0;
//	        document.getElementById('cmbCUSTOMER_REASON_CODE4').options.selectedIndex = 0;
	        document.getElementById('cmbPREFIX').options.selectedIndex = -1;
	        document.getElementById('txtDATE_OF_BIRTH').value = '';
	        document.getElementById('txtSSN_NO').value = '';
//	        document.getElementById('txtEMPLOYER_NAME').value = '';
//	        document.getElementById('txtYEARS_WITH_CURR_EMPL').value = '';
	        document.getElementById('cmbMARITAL_STATUS').options.selectedIndex = -1;
	        document.getElementById('cmbGENDER').options.selectedIndex = -1;
	        document.getElementById('cmbAPPLICANT_OCCU').options.selectedIndex = 0;
//	        document.getElementById('txtEMPLOYER_ADD1').value = '';
//	        document.getElementById('txtEMPLOYER_ADD2').value = '';
//	        document.getElementById('txtEMPLOYER_CITY').value = '';
//	        document.getElementById('txtEMPLOYER_ZIPCODE').value = '';
//	        document.getElementById('txtEMPLOYER_HOMEPHONE').value = '';
//	        document.getElementById('txtYEARS_WITH_CURR_OCCU').value = '';
//	        document.getElementById('txtEMPLOYER_EMAIL').value = '';
//	        document.getElementById('cmbEMPLOYER_COUNTRY').options.selectedIndex = 0;
//	        document.getElementById('cmbEMPLOYER_STATE').options.selectedIndex = -1;

	        if (document.getElementById('btnActivateDeactivate'))
	            document.getElementById('btnActivateDeactivate').style.display = 'none';

	        //Disable the Risk Information and Application details menu when new customer is being added
	        top.topframe.disableMenus('1', 'ALL');
	        //Disabling validators
	        DisableValidators();

	        //Changing the color of mandatory controls
	        ChangeColor();
	        OnCustomerTypeChange();
	        SSN_change();

	    }

	    function setTab() {

	        var lob = '';

	        if (document.getElementById('hidCUSTOMER_ID').value != 'New') {
	            if (parent.CALLED_FROM_APPLICATION != undefined)
	                parent.CALLED_FROM_APPLICATION = 0;
	            Url = "AttentionNotes.aspx?calledfrom=" + lob + "&CustomerID=" + document.getElementById('hidCUSTOMER_ID').value + "&BackOption=" + "Y" + "&BACK_TO_APPLICATION=" + document.getElementById("hidBackToApplication").value + "&";
	            DrawTab(3, top.frames[1], 'Attention Note', Url);
	            Url = "ApplicantInsuedIndex.aspx?calledfrom=" + lob + "&Customer_ID=" + document.getElementById('hidCUSTOMER_ID').value + "&CUSTOMER_TYPE=" + document.getElementById('hidCust_Type').value + "&BackOption=" + "Y" + "&BACK_TO_APPLICATION=" + document.getElementById("hidBackToApplication").value + "&";
	            DrawTab(2, top.frames[1], 'Co-Applicant Details', Url);

	        }
	        else {
	            RemoveTab(3, top.frames[1]);
	            RemoveTab(2, top.frames[1]);
	            top.topframe.enableMenu('1,0');
	            top.topframe.enableMenu('1,1');
	        }
	    }

	    function Initialize() {
	        setTimeout("GetColorAgency()", 300);
	        if (document.getElementById('hidTabInsScore').value == 1) {
	            document.getElementById('txtCUSTOMER_INSURANCE_SCORE').focus();
	        }
	        else {
	            if (document.getElementById('hidFormSaved').value != 5) {
	                document.getElementById('cmbCUSTOMER_TYPE').focus();
	            }
	            var tempXML = document.getElementById('hidOldXML').value;


	            if (document.getElementById('cmbCUSTOMER_TYPE').options[document.getElementById('cmbCUSTOMER_TYPE').selectedIndex].value == '11110') {
	                document.getElementById('hidCust_Type').value = "Personal";
	            }
	            else {
	                document.getElementById('hidCust_Type').value = "Commercial";
	            }
	        }
	        if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')) {
	            if (tempXML != "" && tempXML != 0 && tempXML != "<NewDataSet />") {
	                setMenu();
	                //Enabling the activate deactivate button
	                //document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
	                if (document.getElementById('btnActivateDeactivate'))
	                    document.getElementById('btnActivateDeactivate').style.display = 'inline';

	                //Storing the XML in hidCUSTOMER_ID hidden fields 
	                if (tempXML != undefined)//Done for Itrack Issue 5454 on 20 April 09
	                    document.getElementById('hidOldData').value = tempXML;

	                //Populating the data by the calling the common function in form,.js
	                //populateFormData(tempXML,CLT_CUSTOMER_LIST);

	                var doc = this.parent.document;
	                if (doc.getElementById("cltClientTop_lblClientStatus").innerHTML != null) {
	                    if (document.getElementById("lblIS_ACTIVE").innerHTML == "N")
	                        doc.getElementById("cltClientTop_lblClientStatus").innerHTML = "Inactive";
	                    else
	                        doc.getElementById("cltClientTop_lblClientStatus").innerHTML = "Active";
	                }
	                //if(doc.getElementById("cltClientTop_lblClientPhone").innerHTML != null)
	                //doc.getElementById("cltClientTop_lblClientPhone").innerHTML  = document.getElementById('txtCUSTOMER_HOME_PHONE').value;

	                if (doc.getElementById("cltClientTop_lblClientTitle").innerHTML != null)
	                    doc.getElementById("cltClientTop_lblClientTitle").innerHTML = document.getElementById("cmbPREFIX").options[document.getElementById("cmbPREFIX").selectedIndex].text;


	                if (document.getElementById("cmbCUSTOMER_TYPE").options[document.getElementById("cmbCUSTOMER_TYPE").selectedIndex].text == "Commercial") {
	                    doc.getElementById("cltClientTop_lblFullName").innerHTML = document.getElementById("txtCUSTOMER_FIRST_NAME").value
	                    doc.getElementById("cltClientTop_lblPhone").innerHTML = "Business Phone";
	                    doc.getElementById("cltClientTop_lblClientPhone").innerHTML = document.getElementById('txtCUSTOMER_BUSINESS_PHONE').value;
	                }
	                else {
	                    doc.getElementById("cltClientTop_lblPhone").innerHTML = "Home Phone";
	                }

	                if (document.getElementById("hidIS_ACTIVE").value == "N") {
	                    document.getElementById("lblIS_ACTIVE").innerHTML = "Inactive";
	                    document.getElementById("lblIS_ACTIVE").style.color = 'Red';
	                }
	                else {
	                    document.getElementById("lblIS_ACTIVE").innerHTML = "Active";
	                }
	                //DrowSecondTab();
	                //Set Cust State  
	                document.getElementById("cmbCUSTOMER_STATE").value = document.getElementById("hidSTATE_ID_OLD").value;
	                //Set Employer Cust State					
//	                document.getElementById("cmbEMPLOYER_STATE").value = document.getElementById("hidEmpDetails_STATE_ID_OLD").value;

	            }
	            else {
	                AddData();
	                //document.getElementById('hidCUSTOMER_ID').value = 'New';
	            }
	        }

	        if (document.getElementById("hidCarrierId").value == "1") {
	            //document.getElementById("lblCUSTOMER_REFERRED_BY").style.display = "none"
	            document.getElementById("lblCUSTOMER_AGENCY_ID").style.display = "none"
	            //document.getElementById("cmbCUSTOMER_REFERRED_BY").style.display = "inline"
	            document.getElementById("cmbCUSTOMER_AGENCY_ID").style.display = "inline"

	        }
	        else {
	            //document.getElementById("lblCUSTOMER_REFERRED_BY").style.display = "inline"
	            document.getElementById("lblCUSTOMER_AGENCY_ID").style.display = "inline"
	            document.getElementById("lblCUSTOMER_AGENCY_ID").innerHTML = document.getElementById("cmbCUSTOMER_AGENCY_ID").options[document.getElementById("cmbCUSTOMER_AGENCY_ID").selectedIndex].text;
	            //document.getElementById("cmbCUSTOMER_REFERRED_BY").style.display = "none"
	            document.getElementById("cmbCUSTOMER_AGENCY_ID").style.display = "none"
	        }

	        setTab();
	        //Check();
	        //This function will enable or disable the middle and last name depending upon
	        //the type of customer
	        OnCustomerTypeChange();

	        //setEncryptFields();

	    }
	    function chkScore_clicked() {

	        if (document.getElementById('chkSCORE').checked == true) {
	            document.getElementById('revCUSTOMER_INSURANCE_SCORE').setAttribute('enabled', false);
	            document.getElementById('txtCUSTOMER_INSURANCE_SCORE').value = "-2";
	            document.getElementById('txtCUSTOMER_INSURANCE_SCORE').style.display = "none";
	            document.getElementById('revCUSTOMER_INSURANCE_SCORE').style.display = "none";
	            document.getElementById('btnGetInsuranceScore').style.display = "none";
	            if (document.getElementById('lblCUSTOMER_INSURANCE_SCORE'))
	                document.getElementById('lblCUSTOMER_INSURANCE_SCORE').style.display = "none";


	            document.getElementById('lblSCORE').style.display = "none";
	        }
	        else {
	            document.getElementById('revCUSTOMER_INSURANCE_SCORE').setAttribute('enabled', true);
	            if (document.getElementById('txtCUSTOMER_INSURANCE_SCORE').value == -2)
	                document.getElementById('txtCUSTOMER_INSURANCE_SCORE').value = "";
	            document.getElementById('txtCUSTOMER_INSURANCE_SCORE').style.display = "inline";
	            document.getElementById('btnGetInsuranceScore').style.display = "inline";
	            document.getElementById('lblSCORE').style.display = "inline";
	            if (document.getElementById('lblCUSTOMER_INSURANCE_SCORE'))
	                document.getElementById('lblCUSTOMER_INSURANCE_SCORE').style.display = "inline";

	            document.getElementById('txtCUSTOMER_INSURANCE_RECEIVED_DATE').style.display = "inline";
	            document.getElementById('capCUSTOMER_INSURANCE_RECEIVED_DATE').style.display = "inline";
	            document.getElementById('imgCO_APPL_DOB').style.display = "inline";
	        }

	    }
	    function refreshtabIndex() {
	        if (document.getElementById("hidRefreshTabIndex").value == 'Y') {
	            RefreshClientTop();
	            var strQueryString = "CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value + "&SaveMsg=" + document.getElementById("hidSaveMsg").value;
	        }
	    }

	    function setMenu() {
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

	    function CheckIfPhoneEmpty() {
	        if (document.getElementById('txtCUSTOMER_BUSINESS_PHONE').value == "") {
	            document.getElementById('txtCUSTOMER_EXT').value = ""
	            return false;
	        }
	        else
	            return true;
	    }

	    function showButtons() {
	        if (document.CLT_CUSTOMER_LIST.hidCUSTOMER_ID.value == "New") {
	            if (document.getElementById("btnBack"))
	                document.getElementById("btnBack").style.display = "inline";
	            if (document.getElementById("btnCustomerAssistant"))
	                document.getElementById("btnCustomerAssistant").style.display = "none";
	            //Nov11,2005:Sumit Chhabra:Added the code to show/hide the add quick quote and application buttons				
	            document.getElementById("btnAddNewQuickQuote").style.display = "none";
	            document.getElementById("btnAddNewApplication").style.display = "none";
	        }
	        else {
	            if (document.getElementById("btnBack"))
	                document.getElementById("btnBack").style.display = "none";
	            if (document.getElementById("btnCustomerAssistant"))
	                document.getElementById("btnCustomerAssistant").style.display = "inline";
	            //Nov11,2005:Sumit Chhabra:Added the code to show/hide the add quick quote and application buttons
	            document.getElementById("btnAddNewQuickQuote").style.display = "inline";
	            document.getElementById("btnAddNewApplication").style.display = "inline";

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
	            document.location.href = "AddCustomerQQ.aspx?CUSTOMER_ID=" + document.getElementById('hidCUSTOMER_ID').value + "&CalledFrom=AGENTQQ&";  //+ '<%=strCalledFrom%>';
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
	                lookupMessage = "%SAL.";
	                break;
	            case "cmbAPPLICANT_OCCU":
	                lookupMessage = "%OCC.";
	                break;
	            case "cmbCUSTOMER_REASON_CODE":
	                lookupMessage = "RCFC.";
	                break;
	            default:
	                lookupMessage = "Look up code not found";
	                break;

	        }
	        showLookupLayer(controlId, lookupMessage);
	    }
	    function ChkDateOfBirth(objSource, objArgs) {
	        if (document.getElementById("revDATE_OF_BIRTH").isValid == true) {
	            var effdate = document.CLT_CUSTOMER_LIST.txtDATE_OF_BIRTH.value;
	            objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
	        }
	        else
	            objArgs.IsValid = true;
	    }





	    function Check() {
	        if (document.getElementById('cmbAPPLICANT_OCCU').selectedIndex != '-1') {
	            //alert(document.getElementById('cmbCO_APPLI_OCCU').options[document.getElementById('cmbCO_APPLI_OCCU').selectedIndex].text)
//	            if (document.getElementById('cmbAPPLICANT_OCCU').options[document.getElementById('cmbAPPLICANT_OCCU').selectedIndex].value == '11427') {
//	                document.getElementById('txtDESC_APPLICANT_OCCU').style.display = 'inline';
//	                document.getElementById('lblDESC_APPLICANT_OCCU').style.display = 'none';
//	                //rfvDESC_APPLICANT_OCCU
//	                document.getElementById("rfvDESC_APPLICANT_OCCU").setAttribute('enabled', true);
//	                document.getElementById("rfvDESC_APPLICANT_OCCU").setAttribute('isValid', true);

//	                document.getElementById('spnDESC_APPLICANT_OCCU').style.display = 'inline';
//	            }
//	            else {

//	                document.getElementById('txtDESC_APPLICANT_OCCU').style.display = 'none';
//	                document.getElementById('lblDESC_APPLICANT_OCCU').style.display = 'inline';
//	                document.getElementById('lblDESC_APPLICANT_OCCU').innerHTML = '-N.A.-';
//	                document.getElementById("rfvDESC_APPLICANT_OCCU").setAttribute('isValid', false);
//	                document.getElementById("rfvDESC_APPLICANT_OCCU").style.display = 'none';
//	                document.getElementById("rfvDESC_APPLICANT_OCCU").setAttribute('enabled', false);
//	                document.getElementById('spnDESC_APPLICANT_OCCU').style.display = 'none';

//	            }
	        }
	        else {
//	            document.getElementById('txtDESC_APPLICANT_OCCU').style.display = 'none';
//	            document.getElementById('lblDESC_APPLICANT_OCCU').style.display = 'inline';
//	            document.getElementById('lblDESC_APPLICANT_OCCU').innerHTML = '-N.A.-';
//	            document.getElementById("rfvDESC_APPLICANT_OCCU").setAttribute('isValid', false);
//	            document.getElementById("rfvDESC_APPLICANT_OCCU").style.display = 'none';
//	            document.getElementById("rfvDESC_APPLICANT_OCCU").setAttribute('enabled', false);
//	            document.getElementById('spnDESC_APPLICANT_OCCU').style.display = 'none';
	        }


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

	    function ChkEmpResult(objSource, objArgs) {
	        objArgs.IsValid = true;
	        if (objArgs.IsValid == true) {
	            objArgs.IsValid = GetEmpZipForState();
	            if (objArgs.IsValid == false)
	                document.getElementById('csvEMPLOYER_ZIPCODE').innerHTML = "The zip code does not belong to the state";
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
	    function GetEmpZipForState() {

	        GlobalError = true;
	        if (document.getElementById('cmbEMPLOYER_STATE').value == 14 || document.getElementById('cmbEMPLOYER_STATE').value == 22 || document.getElementById('cmbEMPLOYER_STATE').value == 49) {
	            if (document.getElementById('txtEMPLOYER_ZIPCODE').value != "") {
	                var intStateID = document.getElementById('cmbEMPLOYER_STATE').options[document.getElementById('cmbEMPLOYER_STATE').options.selectedIndex].value;
	                var strZipID = document.getElementById('txtEMPLOYER_ZIPCODE').value;
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
	    function SSN_change() {
	        //alert(document.getElementById("btnSSN_NO").value);
	        //document.getElementById('txtSSN_NO').value = document.getElementById('txtSSN_NO_HID').value;
	        document.getElementById('txtSSN_NO').value = '';
	        document.getElementById('txtSSN_NO').style.display = 'inline';
	        //document.getElementById("rfvSSN_NO").style.display='none';
	        //document.getElementById("rfvSSN_NO").setAttribute('enabled',true);
	        //document.getElementById("revSSN_NO").style.display='none';
	        document.getElementById("revSSN_NO").setAttribute('enabled', true);
//	        if (document.getElementById("btnSSN_NO").value == 'Edit')
//	            document.getElementById("btnSSN_NO").value = 'Cancel';
//	        else if (document.getElementById("btnSSN_NO").value == 'Cancel')
//	            SSN_hide();
//	        else
//	            document.getElementById("btnSSN_NO").style.display = 'none';

	        //document.getElementById('txtSSN_NO_HID').style.display = 'none';			
	    }

	    function SSN_hide() {
	        document.getElementById("capSSN_NO_HID").style.display = 'inline';
	        //document.getElementById("btnSSN_NO").style.display = 'none';
	        //document.getElementById('txtSSN_NO').value = document.getElementById('txtSSN_NO_HID').value;
	        document.getElementById('txtSSN_NO').value = '';
	        document.getElementById('txtSSN_NO').style.display = 'none';
	        //document.getElementById("rfvSSN_NO").style.display='none';
	        //document.getElementById("rfvSSN_NO").setAttribute('enabled',false);
	        document.getElementById("revSSN_NO").style.display = 'none';
	        document.getElementById("revSSN_NO").setAttribute('enabled', false);
	        //document.getElementById("btnSSN_NO").value = 'Edit';
	        //document.getElementById('txtSSN_NO_HID').style.display = 'none';			
	    }
	    //-------------------------------------------------------------------------------------------------------------
	    //*****************************Added by Sibin for Itrack Issue 4843 on 16 OCT 08******************

	    function fillstateFromCountry() {

	        GlobalError = true;
	        //var CmbState=document.getElementById('cmbCUSTOMER_STATE');
	        var CountryID = document.getElementById('cmbCUSTOMER_COUNTRY').options[document.getElementById('cmbCUSTOMER_COUNTRY').selectedIndex].value;
	        //var oResult='';
	        AddCustomer.AjaxFillState(CountryID, fillState);

	        //fillState(oResult);
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
	        //var strXML;
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
	            setStateID();

	            document.getElementById("cmbCUSTOMER_STATE").value = document.getElementById("hidSTATE_ID_OLD").value;



	        }

	        return false;
	    }


	    function DisableZipForCanada() {
	        var myCountry = document.getElementById("cmbCUSTOMER_COUNTRY");

	        if (myCountry.options[myCountry.selectedIndex].value == '2') {
	            document.getElementById("revCUSTOMER_ZIP").setAttribute("enabled", false);
	        }

	        else {
	            document.getElementById("revCUSTOMER_ZIP").setAttribute("enabled", true);
	        }
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
	        var myCountry = document.getElementById("cmbEMPLOYER_COUNTRY");

	        if (myCountry.options[myCountry.selectedIndex].value == '2') {
	            document.getElementById("revEMPLOYER_ZIPCODE").setAttribute("enabled", false);
	            document.getElementById("revEMPLOYER_ZIPCODE").style.display = 'none';
	        }

	        else {
	            document.getElementById("revEMPLOYER_ZIPCODE").setAttribute("enabled", true);
	        }

	    }

	    //Added functions till here by Sibin for Itrack Issue 4843
	    function initPage() {
	       // alert();
	        Initialize();
	        //InsuranceScoreChange();
	        ApplyColor();
	        showButtons();
	        LoadTitles();
	        //chkScore_clicked();
	        RefreshClientTop();
	    }
		</script>
</head>
<BODY  oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="initPage();">
		<FORM id="CLT_CUSTOMER_LIST" method="post" runat="server">
			<P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
			<TABLE class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD>
						<TABLE class="tableWidthHeader" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
									mandatory</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capIS_ACTIVE" runat="server">Status</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:label id="lblIS_ACTIVE" CssClass="LabelFont" Runat="server"></asp:label></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_TYPE" runat="server">Status</asp:label><SPAN class="mandatory">*</SPAN></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCUSTOMER_TYPE" onfocus="SelectComboIndex('cmbCUSTOMER_TYPE');" onblur="document.getElementById('imgSelect').focus();"
										runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvCUSTOMER_TYPE" Runat="server" ControlToValidate="cmbCUSTOMER_TYPE" Display="Dynamic"
										Enabled="True"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_PARENT" runat="server">Parent1</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCUSTOMER_PARENT_TEXT" runat="server" size="40" ReadOnly="true"></asp:textbox><A class="calcolora" href="#"><IMG id="imgSelect" style="CURSOR: hand" alt="" src="../../cmsweb/images/selecticon.gif"
											border="0" autopostback="false" runat="server"></A>
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="capPREFIX" runat="server">Title</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPREFIX" onfocus="SelectComboIndex('cmbPREFIX');" runat="server"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbPREFIX')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
								</TD>
							</tr>
							<tr>
								<td colSpan="4">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD class="midcolora" width="19%"><asp:label id="capCUSTOMER_FIRST_NAME" runat="server"></asp:label><SPAN class="mandatory">*</SPAN>
											</TD>
											<TD class="midcolora" width="19%" id='tdFname'><asp:textbox id="txtCUSTOMER_FIRST_NAME" runat="server" size="25" maxlength="75"></asp:textbox><br>
												<asp:requiredfieldvalidator id="rfvCUSTOMER_FIRST_NAME" Runat="server" ControlToValidate="txtCUSTOMER_FIRST_NAME"
													Display="Dynamic"></asp:requiredfieldvalidator></TD>
											<TD class="midcolora" width="13%"><asp:label id="capCUSTOMER_MIDDLE_NAME" runat="server"></asp:label></TD>
											<TD class="midcolora" width="16%"><asp:textbox id="txtCUSTOMER_MIDDLE_NAME" runat="server" size="12" maxlength="10"></asp:textbox></TD>
											<TD class="midcolora" width="8%"><asp:label id="capCUSTOMER_LAST_NAME" runat="server"></asp:label><SPAN class="mandatory" id="spnMandatory">*</SPAN>
											</TD>
											<TD class="midcolora" width="25%"><asp:textbox id="txtCUSTOMER_LAST_NAME" runat="server" size="25" maxlength="25"></asp:textbox><br>
												<asp:requiredfieldvalidator id="rfvCUSTOMER_LAST_NAME" Runat="server" ControlToValidate="txtCUSTOMER_LAST_NAME"
													Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revCUSTOMER_FIRST_NAME" Runat="server" ControlToValidate="txtCUSTOMER_FIRST_NAME"
													Display="Dynamic"></asp:regularexpressionvalidator></TD>
										</TR>
									</table>
								</td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_SUFFIX" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCUSTOMER_SUFFIX" runat="server" size="6" maxlength="5"></asp:textbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_CODE" runat="server"></asp:label><SPAN class="mandatory">*</SPAN></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCUSTOMER_CODE" runat="server" size="13" maxlength="10"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvCUSTOMER_CODE" Runat="server" ControlToValidate="txtCUSTOMER_CODE" Display="Dynamic"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revCUSTOMER_CODE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtCUSTOMER_CODE"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr id="rd1">
								<TD class="midcolora" width="18%"><asp:label id="capDATE_OF_BIRTH" runat="server"></asp:label><span class="mandatory" id="spnDOB">*</span></TD>
								<TD class="midcolora" width="32%">
								    <asp:textbox id="txtDATE_OF_BIRTH" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkDATE_OF_BIRTH" runat="server" CssClass="HotSpot">
										<asp:Image runat="server" ID="imgDATE_OF_BIRTH" Border="0" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:Image>
									</asp:hyperlink><BR>
									<asp:requiredfieldvalidator id="rfvDATE_OF_BIRTH" runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic"	ErrorMessage="Date of Birth can't be blank."></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revDATE_OF_BIRTH" runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic"></asp:regularexpressionvalidator><br>
									<asp:customvalidator id="csvDATE_OF_BIRTH" Runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic" ClientValidationFunction="ChkDateOfBirth"></asp:customvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSSN_NO" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%">
								    <asp:label id="capSSN_NO_HID" runat="server" size="14" maxlength="11"></asp:label>								    
								    <asp:textbox id="txtSSN_NO" runat="server" size="14" maxlength="11" AutoComplete = "Off"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revSSN_NO" runat="server" ControlToValidate="txtSSN_NO" Display="Dynamic"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr id="rd2">
								<TD class="midcolora" width="18%"><asp:label id="capMARITAL_STATUS" runat="server"></asp:label><span class="mandatory" id="spnMS">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbMARITAL_STATUS" onfocus="SelectComboIndex('cmbMARITAL_STATUS')" runat="server"></asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvMARITAL_STATUS" runat="server" ControlToValidate="cmbMARITAL_STATUS" Display="Dynamic"
										ErrorMessage="MARITAL_STATUS can't be blank."></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capGENDER" runat="server"></asp:label><span class="mandatory" id="spnG">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbGENDER" onfocus="SelectComboIndex('cmbGENDER')" runat="server">
										<asp:ListItem Value=""></asp:ListItem>
										<asp:ListItem Value="F">Female</asp:ListItem>
										<asp:ListItem Value="M">Male</asp:ListItem>
									</asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvGENDER" runat="server" ControlToValidate="cmbGENDER" Display="Dynamic" ErrorMessage="GENDER can't be blank."></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_ADDRESS1" runat="server"></asp:label><SPAN class="mandatory">*</SPAN></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCUSTOMER_ADDRESS1" runat="server" size="35" maxlength="150"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvCUSTOMER_ADDRESS1" Runat="server" ControlToValidate="txtCUSTOMER_ADDRESS1"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_ADDRESS2" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCUSTOMER_ADDRESS2" runat="server" size="35" maxlength="150"></asp:textbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_CITY" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCUSTOMER_CITY" runat="server" size="35" maxlength="35"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvCUSTOMER_CITY" Runat="server" ControlToValidate="txtCUSTOMER_CITY" Display="Dynamic"
										ErrorMessage=""></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_COUNTRY" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCUSTOMER_COUNTRY" onfocus="SelectComboIndex('cmbCUSTOMER_COUNTRY');" runat="server" onchange="javascript:fillstateFromCountry();"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvCUSTOMER_COUNTRY" Runat="server" ControlToValidate="cmbCUSTOMER_COUNTRY"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_STATE" runat="server"></asp:label><span class="mandatory" id="spnCUSTOMER_STATE" runat="server">*</span></TD>
								
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCUSTOMER_STATE" onchange="setStateID();" onfocus="SelectComboIndex('cmbCUSTOMER_STATE');" runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvCUSTOMER_STATE" Runat="server" ControlToValidate="cmbCUSTOMER_STATE" Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_ZIP" runat="server"></asp:label><span class="mandatory" id="spnCUSTOMER_ZIP" runat="server">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCUSTOMER_ZIP" runat="server" size="13" maxlength="10" OnBlur="GetZipForState();DisableZipForCanada();"></asp:textbox><%--<A href="#"><asp:image id="imgZipLookup" runat="server" ImageAlign="Bottom" ImageUrl="/cms/cmsweb/images/info.gif"></asp:image></A>--%>
									<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
										<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
									</asp:hyperlink>
									<BR>
									<asp:customvalidator id="csvCUSTOMER_ZIP" Runat="server" ClientValidationFunction="ChkResult" ErrorMessage=" "
												Display="Dynamic" ControlToValidate="txtCUSTOMER_ZIP"></asp:customvalidator>
									<asp:requiredfieldvalidator id="rfvCUSTOMER_ZIP" Runat="server" ControlToValidate="txtCUSTOMER_ZIP" Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revCUSTOMER_ZIP" Runat="server" ControlToValidate="txtCUSTOMER_ZIP" Display="Dynamic"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_HOME_PHONE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCUSTOMER_HOME_PHONE" runat="server" size="16" maxlength="13"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revCUSTOMER_HOME_PHONE" Runat="server" ControlToValidate="txtCUSTOMER_HOME_PHONE"
										Display="Dynamic"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" id="tdPER_CUST_MOBILE1" width="18%"><asp:label id="capPER_CUST_MOBILE" runat="server"></asp:label></TD>
								<TD class="midcolora" id="tdPER_CUST_MOBILE2" width="32%"><asp:textbox id="txtPER_CUST_MOBILE" runat="server" size="16" maxlength="13"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revPER_CUST_MOBILE" Runat="server" ControlToValidate="txtPER_CUST_MOBILE" Display="Dynamic"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_Email" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%" colspan="3"><asp:textbox id="txtCUSTOMER_Email" runat="server" size="35" maxlength="50"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revCUSTOMER_Email" Runat="server" ControlToValidate="txtCUSTOMER_Email" Display="Dynamic"></asp:regularexpressionvalidator></TD>
								
							</tr>
							<tr id="appOccupation">
								<TD class="midcolora" width="18%"><asp:label id="capAPP_OCC" runat="server">Occupation</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%" colspan="3"><asp:dropdownlist id="cmbAPPLICANT_OCCU" runat="server"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbAPPLICANT_OCCU')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
									<br>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator1" Runat="server" ControlToValidate="cmbAPPLICANT_OCCU" Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<%--<TD class="midcolora" width="18%"><asp:label id="Label2" runat="server"></asp:label><span class="mandatory" id="Span1">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="Textbox1" runat="server" size="30" maxlength="200"></asp:textbox><asp:label id="Label3" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" ControlToValidate="Textbox1"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>--%>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><span id="span_Agency_cap"><asp:label id="capCUSTOMER_AGENCY_ID" runat="server"></asp:label><SPAN class="mandatory">*</SPAN></span></TD>
								<TD class="midcolora" width="32%" colspan="3"><span id="span_Agency"><asp:dropdownlist id="cmbCUSTOMER_AGENCY_ID" onfocus="SelectComboIndex('cmbCUSTOMER_AGENCY_ID');"
											runat="server"></asp:dropdownlist><asp:label id="lblCUSTOMER_AGENCY_ID" CssClass="LabelFont" Runat="server"></asp:label></span><br>
									<asp:requiredfieldvalidator id="rfvCUSTOMER_AGENCIES" Runat="server" ControlToValidate="cmbCUSTOMER_AGENCY_ID"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr id="trInsScore" runat="server" visible="false">
							    <td colSpan="4">
							        <table id="tbOtherInfo" visible="false">
							            <tr id="trInsScore" visible="false">
								        <TD class="headerEffectSystemParams" width="18%" colSpan="4">Insurance Score</TD>
							            </tr>
							            <tr>
							            </tr>
							<TR visible="false">
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_INSURANCE_SCORE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%">
									<asp:checkbox id="chkSCORE" runat="server"></asp:checkbox>
									<asp:label id="capSCORE" runat="server"></asp:label><br>
									<asp:textbox id="txtCUSTOMER_INSURANCE_SCORE" runat="server" size="8" maxlength="3"></asp:textbox>
									<cmsb:cmsbutton class="clsButton" id="btnGetInsuranceScore" runat="server" Text="Get Insurance Score"
										CausesValidation="False"></cmsb:cmsbutton><br>
									<asp:label id="lblSCORE" Runat="server"></asp:label><br>
									<%--(Insurance Score can be obtained when Customer is Saved)<br>--%>
									<asp:label id="lblCUSTOMER_INSURANCE_SCORE" Visible="False" Runat="server"></asp:label>
									<asp:regularexpressionvalidator id="revCUSTOMER_INSURANCE_SCORE" Runat="server" ControlToValidate="txtCUSTOMER_INSURANCE_SCORE"
										Display="Dynamic"></asp:regularexpressionvalidator>
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_INSURANCE_RECEIVED_DATE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCUSTOMER_INSURANCE_RECEIVED_DATE" runat="server" size="13" maxlength="10"></asp:textbox><asp:hyperlink id="hlkCUSTOMER_INSURANCE_RECEIVED_DATE" runat="server" CssClass="HotSpot">
										<asp:image id="imgCO_APPL_DOB" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
									</asp:hyperlink><br>
									<asp:regularexpressionvalidator id="revCUSTOMER_INSURANCE_RECEIVED_DATE" Runat="server" ControlToValidate="txtCUSTOMER_INSURANCE_RECEIVED_DATE"
										Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvCUSTOMER_INSURANCE_RECEIVED_DATE" Runat="server" ControlToValidate="txtCUSTOMER_INSURANCE_RECEIVED_DATE"
										Display="Dynamic" ClientValidationFunction="ChkDate"></asp:customvalidator></TD>
							</TR>
							<tr visible="false">
								<td colSpan="4">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="midcolora" width="17%"><asp:label id="capCUSTOMER_REASON_CODE" runat="server"></asp:label></td>
											<td class="midcolora" width="12.5%"><asp:dropdownlist id="cmbCUSTOMER_REASON_CODE" onfocus="SelectComboIndex('cmbCUSTOMER_REASON_CODE')"
													runat="server"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbCUSTOMER_REASON_CODE')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" align="bottom" border="0"></A>
											</td>
											<td class="midcolora" width="10.5%"><asp:label id="capCUSTOMER_REASON_CODE2" runat="server"></asp:label></td>
											<td class="midcolora" width="12.5%"><asp:dropdownlist id="cmbCUSTOMER_REASON_CODE2" runat="server"></asp:dropdownlist></td>
											<td class="midcolora" width="10.5%"><asp:label id="capCUSTOMER_REASON_CODE3" runat="server"></asp:label></td>
											<td class="midcolora" width="12.5%"><asp:dropdownlist id="cmbCUSTOMER_REASON_CODE3" runat="server"></asp:dropdownlist></td>
											<td class="midcolora" width="10.5%"><asp:label id="capCUSTOMER_REASON_CODE4" runat="server"></asp:label></td>
											<td class="midcolora" width="12.5%"><asp:dropdownlist id="cmbCUSTOMER_REASON_CODE4" runat="server"></asp:dropdownlist></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr id="trHeader" visible="false">
								<TD class="headerEffectSystemParams" colSpan="4">Additional Customer Information</TD>
							</tr>
							<tr id="trName" visible="false">
								<span id="spn_Personal1">
									<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_CONTACT_NAME" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCUSTOMER_CONTACT_NAME" runat="server" size="35" maxlength="35"></asp:textbox><asp:label id="spnCUSTOMER_CONTACT_NAME" runat="server" CssClass="LabelFont">-N.A.-</asp:label></TD>
									<td class="midcolora" width="50%" colSpan="2"></td>
								</span>
							</tr>
							<tr id="trPhone" visible="false">
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_BUSINESS_PHONE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCUSTOMER_BUSINESS_PHONE" runat="server" size="16" maxlength="13"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revCUSTOMER_BUSINESS_PHONE" Runat="server" ControlToValidate="txtCUSTOMER_BUSINESS_PHONE"
										Display="Dynamic"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_EXT" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCUSTOMER_EXT" onblur="CheckIfPhoneEmpty();" runat="server" size="5" maxlength="4"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revCUSTOMER_EXT" Runat="server" ControlToValidate="txtCUSTOMER_EXT" Display="Dynamic"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr id="trMobile" visible="false">
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_MOBILE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCUSTOMER_MOBILE" runat="server" size="16" maxlength="13"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revCUSTOMER_MOBILE" Runat="server" ControlToValidate="txtCUSTOMER_MOBILE" Display="Dynamic"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_WEBSITE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCUSTOMER_WEBSITE" runat="server" size="35" maxlength="150"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revCUSTOMER_WEBSITE" Runat="server" ControlToValidate="txtCUSTOMER_WEBSITE"
										Display="Dynamic"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr id="trFax" visible="false">
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_FAX" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCUSTOMER_FAX" runat="server" size="16" maxlength="13"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revCUSTOMER_FAX" Runat="server" ControlToValidate="txtCUSTOMER_FAX" Display="Dynamic"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_PAGER_NO" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCUSTOMER_PAGER_NO" runat="server" size="19" maxlength="15"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revCUSTOMER_PAGER_NO" Runat="server" ControlToValidate="txtCUSTOMER_PAGER_NO"
										Display="Dynamic"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr id="trBusiness" visible="false">
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_BUSINESS_TYPE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%">
									<asp:dropdownlist id="cmbCUSTOMER_BUSINESS_TYPE" onfocus="SelectComboIndex('cmbCUSTOMER_BUSINESS_TYPE')"
										runat="server" Visible="False"></asp:dropdownlist>
									<asp:textbox id="txtCUSTOMER_BUSINESS_TYPE_NAME" runat="server" size="35" ReadOnly="True"></asp:textbox><A class="calcolora" href="#"><IMG id="imgBusinessType" style="CURSOR: hand" alt="" src="../../cmsweb/images/selecticon.gif"
											border="0" runat="server"></A> <INPUT id="hidCUSTOMER_BUSINESS_TYPE" type="hidden" name="hidCUSTOMER_BUSINESS_TYPE" runat="server">
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_BUSINESS_DESC" runat="server" Width="150"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox onkeypress="MaxLength(this,1000)" id="txtCUSTOMER_BUSINESS_DESC" runat="server"
										size="35" maxlength="1000" Width="250" TextMode="MultiLine" Rows="4"></asp:textbox><br>
									<asp:customvalidator id="csvCUSTOMER_BUSINESS_DESC" Runat="server" ControlToValidate="txtCUSTOMER_BUSINESS_DESC"
										Display="Dynamic" ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator></TD>
							</tr>							
							        </table>
							    </td>
							</tr>
							<tr id ="trEmploymentDetails" runat="server" visible="false">
							    <td colspan="4">
							        <table>
							            <tr id="trHeader1" visible="false">
								<TD class="headerEffectSystemParams" colSpan="4">Employer Details</TD>
							</tr>
							<tr id="AppDetail1" visible="false">
								<TD class="midcolora" width="18%"><asp:label id="capAPPLICANT_OCCU" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%" colspan="3"><asp:dropdownlist id="cmbAPPLICANT_OCCU1" runat="server"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbAPPLICANT_OCCU1')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
									<br>
									<asp:requiredfieldvalidator id="rfvAPPLICANT_OCCU" Runat="server" ControlToValidate="cmbAPPLICANT_OCCU1" Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%" visible="false"><asp:label id="capDESC_APPLICANT_OCCU" runat="server"></asp:label><span class="mandatory" id="spnDESC_APPLICANT_OCCU">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtDESC_APPLICANT_OCCU" runat="server" size="30" maxlength="200"></asp:textbox><asp:label id="lblDESC_APPLICANT_OCCU" runat="server" CssClass="LabelFont">-N.A.-</asp:label><br>
									<asp:requiredfieldvalidator id="rfvDESC_APPLICANT_OCCU" runat="server" ControlToValidate="txtDESC_APPLICANT_OCCU"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr id="trHeader2" visible="false">
								<TD class="midcolora" width="18%"><asp:label id="capEMPLOYER_NAME" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtEMPLOYER_NAME" runat="server" size="30" maxlength="150"></asp:textbox></TD>
								<%--<TD class="midcolora" width="18%"><asp:label id="capEMPLOYER_ADDRESS" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtEMPLOYER_ADDRESS" runat="server" size="30" maxlength="150" TextMode="multiline"></asp:textbox></TD>--%>
								<td class="midcolora" colSpan="2">&nbsp;</td>
							</tr>
							<tr id="trHeader3" visible="false">
								<TD class="midcolora" width="18%"><asp:label id="capEMPLOYER_ADD1" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtEMPLOYER_ADD1" runat="server" size="30" maxlength="150"></asp:textbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capEMPLOYER_ADD2" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtEMPLOYER_ADD2" runat="server" size="30" maxlength="150"></asp:textbox></TD>
							</tr>
							<tr id="trHeader4" visible="false">
								<TD class="midcolora" width="18%"><asp:label id="capEMPLOYER_CITY" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtEMPLOYER_CITY" runat="server" size="30" maxlength="150"></asp:textbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capEMPLOYER_COUNTRY" runat="server"></asp:label></TD>
								
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbEMPLOYER_COUNTRY" onfocus="SelectComboIndex('cmbEMPLOYER_COUNTRY');" runat="server" onchange="EmpDetails_fillstateFromCountry();"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvEMPLOYER_COUNTRY" Runat="server" ControlToValidate="cmbEMPLOYER_COUNTRY"
										Display="Dynamic" Enabled="False"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr id="trHeader5" visible="false">
								<TD class="midcolora" width="18%"><asp:label id="capEMPLOYER_STATE" runat="server"></asp:label></TD>
								
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbEMPLOYER_STATE" onchange="EmpDetails_setStateID();" onfocus="SelectComboIndex('cmbEMPLOYER_STATE');" runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvEMPLOYER_STATE" Runat="server" ControlToValidate="cmbEMPLOYER_STATE" Display="Dynamic"
										Enabled="False"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capEMPLOYER_ZIPCODE" runat="server"></asp:label></TD>
								
								<!--Added funtion EmpDetails_DisableZipFor Canada by Sibin for Itrack Issue 4843 on 15-10-08-->
								<TD class="midcolora" width="32%"><asp:textbox id="txtEMPLOYER_ZIPCODE" runat="server" size="13" maxlength="10" OnBlur="GetEmpZipForState();"></asp:textbox>
									<asp:hyperlink id="hlkEmpZipLookup" runat="server" CssClass="HotSpot">
										<asp:image id="imgEmpZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
									</asp:hyperlink>
									<BR>
									<asp:customvalidator id="csvEMPLOYER_ZIPCODE" Runat="server" ClientValidationFunction="ChkEmpResult" ErrorMessage=" "
												Display="Dynamic" ControlToValidate="txtEMPLOYER_ZIPCODE"></asp:customvalidator>
									<asp:regularexpressionvalidator id="revEMPLOYER_ZIPCODE" Runat="server" ControlToValidate="txtEMPLOYER_ZIPCODE"
										Display="Dynamic"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr id="trHeader6" visible="false">
								<%--Employer Business Phone is saved in DB as HOME PHONE --%>
								<TD class="midcolora" width="18%"><asp:label id="capEMPLOYER_HOMEPHONE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtEMPLOYER_HOMEPHONE" runat="server" size="16" maxlength="13"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revEMPLOYER_HOMEPHONE" Runat="server" ControlToValidate="txtEMPLOYER_HOMEPHONE"
										Display="Dynamic"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capEMP_EXT" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtEMP_EXT" runat="server" size="5" maxlength="4"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revEMP_EXT" Runat="server" ControlToValidate="txtEMP_EXT" Display="Dynamic"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr id="trHeader6a" visible="false">
								<TD class="midcolora" width="18%"><asp:label id="capEMPLOYER_EMAIL" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%" colSpan="3"><asp:textbox id="txtEMPLOYER_EMAIL" runat="server" size="35" maxlength="50"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revEMPLOYER_EMAIL" Runat="server" ControlToValidate="txtEMPLOYER_EMAIL" Display="Dynamic"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr id="trHeader7" visible="false">
								<TD class="midcolora" width="18%"><asp:label id="capYEARS_WITH_CURR_OCCU" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtYEARS_WITH_CURR_OCCU" runat="server" size="3" maxlength="2"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revYEARS_WITH_CURR_OCCU" runat="server" ControlToValidate="txtYEARS_WITH_CURR_OCCU"
										Display="Dynamic"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capYEARS_WITH_CURR_EMPL" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtYEARS_WITH_CURR_EMPL" runat="server" size="3" maxlength="2"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revYEARS_WITH_CURR_EMPL" runat="server" ControlToValidate="txtYEARS_WITH_CURR_EMPL"
										Display="Dynamic"></asp:regularexpressionvalidator></TD>
							</tr>
							        </table>
							    </td>
							</tr>
							
							<tr></tr>
							<tr>
								<td class="midcolora" colSpan="3">
									<!--Nov 08,2005:Sumit Chhabra: Two new buttons for adding new quick quote and new application have been added-->
									<cmsb:cmsbutton class="clsButton" id="btnAddNewQuickQuote" runat="server" Text="Add New Quick Quote"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnAddNewApplication" runat="server" Text="Add New Application"></cmsb:cmsbutton><br>
									<cmsb:cmsbutton class="clsButton" id="btnCustomerAssistant" runat="server" Text="Back To Customer Assistant"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Text="Back To Search"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate" CausesValidation="false"></cmsb:cmsbutton>
								</td>
								<td class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnViewMap" runat="server" Text="View Map"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnCopyClient" runat="server" Text="Copy Client"></cmsb:cmsbutton></td>
							</tr>
						</TABLE>
						<input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
						<input id="hidCUSTOMER_ID" type="hidden" value="0" name="Hidden1" runat="server"> <input id="hidCUSTOMER_PARENT" type="hidden" name="hidCUSTOMER_PARENT" runat="server">
						<input id="hidOldData" type="hidden" name="Hidden1" runat="server"> <input id="hidIS_ACTIVE" type="hidden" name="Hidden1" runat="server">
						<input id="Hidden1" type="hidden" name="Hidden1" runat="server"> <INPUT id="hidOldXML" type="hidden" name="hidOldXML" runat="server">
						<INPUT id="hidRefreshTabIndex" type="hidden" name="hidRefreshTabIndex" runat="server">
						<INPUT id="hidSaveMsg" type="hidden" name="hidSaveMsg" runat="server"> <input id="hidCarrierId" type="hidden" name="hidCarrierId" runat="server">
						<input id="hidLAST_INSURANCE_SCORE_FETCHED" type="hidden" name="hidLAST_INSURANCE_SCORE_FETCHED"
							runat="server"> <input id="hidDT_LAST_INSURANCE_SCORE_FETCHED" type="hidden" name="hidDT_LAST_INSURANCE_SCORE_FETCHED"
							runat="server"> <INPUT id="hidMsg" type="hidden" name="hidMsg" runat="server">
						<INPUT id="hidCust_Type" type="hidden" name="hidCust_Type" runat="server"> <INPUT id="hidPREFIX" type="hidden" name="hidPREFIX" runat="server">
						<INPUT id="hidTabInsScore" type="hidden" name="hidTabInsScore" runat="server"> <INPUT id="hidBackToApplication" type="hidden" value="0" name="hidBackToApplication" runat="server">
						<INPUT id="hidCustomer_AGENCY_ID" type="hidden" name="hidCustomer_AGENCY_ID" runat="server">
						<INPUT id="hidSSN_NO" type="hidden" name="hidSSN_NO" runat="server">
						
						<input id="hidSTATE_COUNTRY_LIST" type="hidden" name="hidSTATE_COUNTRY_LIST" runat="server"> <!--Added by Sibin for Itrack Issue 4843-->
						<input id="hidEmpDetails_STATE_COUNTRY_LIST" type="hidden" name="hidEmpDetails_STATE_COUNTRY_LIST" runat="server"> <!--Added by Sibin for Itrack Issue 4843-->
						<input id="hidSTATE_ID" type="hidden" name="hidSTATE_ID" runat="server"> <!--Added by Sibin for Itrack Issue 4843-->
						<input id="hidEmpDetails_STATE_ID" type="hidden" name="hidEmpDetails_STATE_ID" runat="server"> <!--Added by Sibin for Itrack Issue 4843-->
						<input id="hidSTATE_ID_OLD" type="hidden" name="hidSTATE_ID_OLD" runat="server"> <!--Added by Sibin for Itrack Issue 4843-->
						<input id="hidEmpDetails_STATE_ID_OLD" type="hidden" name="hidEmpDetails_STATE_ID_OLD" runat="server"> <!--Added by Sibin for Itrack Issue 4843-->											
					</TD>
				</TR>
			</TABLE>
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none; Z-INDEX: 101; POSITION: absolute"><iframe id="lookupLayerIFrame" style="DISPLAY: none; Z-INDEX: 1001; FILTER: alpha(opacity=0); BACKGROUND-COLOR: #000000"
				width="0" height="0" top="0px;" left="0px"></iframe>
		</div>
		<DIV id="lookupLayer" onmouseover="javascript:refreshLookupLayer();" style="Z-INDEX: 101; VISIBILITY: hidden; POSITION: absolute">
			<table class="TabRow" cellSpacing="0" cellPadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b>Add LookUp</b></td>
					<td>
						<p align="right"><A onclick="javascript:hideLookupLayer();" href="javascript:void(0)"><IMG height="14" alt="Close" src="/cms/cmsweb/images/cross.gif" width="16" border="0"></A></p>
					</td>
				</tr>
				<tr class="SubTabRow">
					<td colSpan="2"><span id="LookUpMsg"></span></td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->

		<script>
		    //RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidCUSTOMER_ID').value,true);
		    refreshtabIndex();
		    if (document.getElementById('hidOldXML').value != "<NewDataSet />")
		        SetActivateDeactivateButton(document.getElementById('hidOldXML').value);

		    //ADDED BY RP - GEN ISSUE 3401
////		    if (document.getElementById("txtCUSTOMER_BUSINESS_TYPE_NAME").value == "")
////		        document.getElementById("txtCUSTOMER_BUSINESS_TYPE_NAME").value = " ";

		    //Added by Mohit Agarwal 5-Sep-08
		    if (document.getElementById("hidSSN_NO") != null && typeof document.getElementById("hidSSN_NO").value != 'undefined' && document.getElementById("hidSSN_NO").value != '') {
		        SSN_hide();
		    }
		    else {
		        SSN_change();
		    }

		</script>
	</BODY>
</html>

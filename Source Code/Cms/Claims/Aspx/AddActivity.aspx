<%@ Page language="c#" Codebehind="AddActivity.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddActivity" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Add Activity</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		
		<script>
		    function cmbValue() {
		        var strAction = new String(document.getElementById("cmbACTION_ON_PAYMENT").options[document.getElementById("cmbACTION_ON_PAYMENT").selectedIndex].value);
		        return strAction.split('^')[1];
		    }
		</script>
		
		<%-----Commented for Itrack Issue 6353 on 17 Sept 09 and added Javascript below-----
		<SCRIPT language=VBScript>
		'Changes in function For Itrack Issue #6353'
		function ConfirmCompletion()
			dim ACTION_ON_PAYMENT 
			ACTION_ON_PAYMENT = cmbValue()
			dim retVal
			if ACTION_ON_PAYMENT = 167 then	'CLOSE RESERVE ACTIVITY
				retVal = MsgBox ("Completing the activity will close the reserve and will bring it to zero.Do you want to proceed?",vbYesNo)
				IF retVal = vbNo THEN
					ConfirmCompletion = false
				END IF
			end if
		 end function
		</SCRIPT--%>
		
		<script language="javascript">
		    //Added for Itrack Issue 6353 on 17 Sept 09
		    var varActivity_Reason = "<%=strActivity_Reason  %>";

		    function ConfirmCompletion() {
		  
		        var ACTION_ON_PAYMENT;
		        ACTION_ON_PAYMENT = cmbValue();
		        var retVal;
		        //Added  For Itrack Issue #6274,6372 on 23-sep-2009
		        if (ACTION_ON_PAYMENT == 167 || ACTION_ON_PAYMENT == 171)//	'CLOSE RESERVE ACTIVITY
		        {
		            var AlertMessage = document.getElementById("hidCloseReserveMessage").value;
		            retVal = confirm(AlertMessage);
		            if (retVal) {
		           
		                DisableButton(document.getElementById("btnCompleteActivity"));
		                return false;
		            }
		            else {
		                return false;
		            }
		        }
		        else {
		          
		           //var ReasonDescription_Tamplate= DisableButton(document.getElementById("btnCompleteActivity"));
		            return GetValue();
		        }
		    }

		    //Activity Constants
		    var Claim_Payment = "11775", Expense_Payment = "11774", Recovery = "11776", Reinsurance = "11777", Reserve_Update = "11773", Reserve_Creation = "11836"

		    function ValidateLength(objSource, objArgs) {
		        if (document.getElementById('txtREASON_DESCRIPTION').value.length > 500)
		            objArgs.IsValid = false;
		        else
		            objArgs.IsValid = true;
		    }
		    function AddingGeneralActivity() {
		        //document.location.href = document.location.href;	
		        __doPostBack("AddingGeneralActivity", 1);
		        //document.location.href = document.location.href + "&ADD_GENERAL=1";	
		        return false;
		    }

		    function chkActivity() {
		       
		        var strAction = new String(document.getElementById("cmbACTION_ON_PAYMENT").options[document.getElementById("cmbACTION_ON_PAYMENT").selectedIndex].value);
		        var ACTION_ON_PAYMENT = strAction.split('^')[1];

		        if (strAction.split('^')[0] == "11775")
		            document.getElementById("cmbCOI_TRAN_TYPE").disabled = false;
		        else {
		        
		            var cmbTRAN_TYPE = document.getElementById("cmbCOI_TRAN_TYPE");
		            for (var i = 0; i < cmbTRAN_TYPE.length; i++)
		             {
		                 if (cmbTRAN_TYPE.options[i].value == 14849)
		                     cmbTRAN_TYPE.selectedIndex = i;
		             }
		            
		            cmbTRAN_TYPE.disabled = true;
    		            
    //	            cmbTRAN_TYPE.selectedIndex = 0;
    //	            cmbTRAN_TYPE.options[0].value = "14849";
    //	            cmbTRAN_TYPE.disabled = true;
		        }

		        //Added  For Itrack Issue #6274,6372 on 23-sep-2009
		        if (ACTION_ON_PAYMENT == 167 || ACTION_ON_PAYMENT == 171) // CLOSE RESERVE
		        {
		            //Added for Itrack Issue 6079 on 10 July 2009
		            var claim_id = document.getElementById("hidCLAIM_ID").value;
		            //Added  For Itrack Issue #6274,6372 on 23-sep-2009
		            var result = AddActivity.CheckCloseReserveDetails(claim_id, ACTION_ON_PAYMENT);
		            var reserve_status = AjaxCallFunction_CallBack(result);

		            if (reserve_status == "0") {
		                if (document.getElementById("btnCompleteActivity"))
		                    document.getElementById("btnCompleteActivity").style.display = "none";

		                if (document.getElementById("btnContinue"))
		                    document.getElementById("btnContinue").style.display = "none";

		                if (document.getElementById("btnActivateDeactivate"))
		                    document.getElementById("btnActivateDeactivate").style.display = "none"

		                alert("There is no reserve to close. Please add a Reserve.");
		                return false;

		            }
		            //Added till here
		            if (document.getElementById("btnCompleteActivity"))
		                document.getElementById("btnCompleteActivity").style.display = "inline";

		            if (document.getElementById("btnContinue"))
		                document.getElementById("btnContinue").style.display = "none";
		        } //Added for Itrack issue 7619 on 3 May 2010									
		        else if (strAction.split('^')[0] == "11774" || strAction.split('^')[0] == "11775" || strAction.split('^')[0] == "11776") {
		            var resultVoidedActivity = AddActivity.CheckVoidedActivity(ACTION_ON_PAYMENT);
		            var isVoidedActivity = AjaxCallFunction_CallBack(resultVoidedActivity);

		            if (isVoidedActivity == "1") {
		                if (document.getElementById("btnCompleteActivity"))
		                    document.getElementById("btnCompleteActivity").style.display = "none";

		                if (document.getElementById("btnContinue"))
		                    document.getElementById("btnContinue").style.display = "none";

		                if (document.getElementById("btnActivateDeactivate"))
		                    document.getElementById("btnActivateDeactivate").style.display = "none"

		                if (document.getElementById("btnVoidActivity"))
		                    document.getElementById("btnVoidActivity").style.display = "none"

		                if (document.getElementById("btnReverseActivity"))
		                    document.getElementById("btnReverseActivity").style.display = "none"

		                if (document.getElementById("chkACCOUNTING_SUPPRESSED"))
		                    document.getElementById("chkACCOUNTING_SUPPRESSED").style.display = "none"

		                if (document.getElementById("capACCOUNTING_SUPPRESSED_CHECKED"))
		                    document.getElementById("capACCOUNTING_SUPPRESSED_CHECKED").style.display = "none"

		                alert("You can't add this activity.");
		                return false;
		            }
		            else {



		                if (document.getElementById("btnContinue"))
		                    document.getElementById("btnContinue").style.display = "inline";

		                if (document.getElementById("btnCompleteActivity"))
		                    document.getElementById("btnCompleteActivity").style.display = "none";


		                return true;
		            }
		        }
		        else {
		            if (document.getElementById("btnCompleteActivity"))
		                document.getElementById("btnCompleteActivity").style.display = "none";

		            if (document.getElementById("btnContinue"))
		                document.getElementById("btnContinue").style.display = "inline";
		        }
		    }
		    //Added for Itrack Issue 6079 on 10 July 2009	
		    function AjaxCallFunction_CallBack(res) {
		        if (!res.error) {
		            if (res.value != "" && res.value != null) {
		                return res.value;
		            }
		        }
		    }

		    function GoToActivity() {
		     
		        var strURL = "";
		        if (document.getElementById("cmbACTION_ON_PAYMENT") == null || document.getElementById("cmbACTION_ON_PAYMENT").selectedIndex == -1)
		            return;

		        var strAction = new String(document.getElementById("cmbACTION_ON_PAYMENT").options[document.getElementById("cmbACTION_ON_PAYMENT").selectedIndex].value);
		        if (typeof (strAction) == 'undefined' && strAction.length < 2)
		            return;
		        switch (strAction.split('^')[0]) {
		            case Recovery:
		                //strURL = "ActivityRecoveryIndex.aspx?CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&";
		                //strURL = "RecoveryTabs.aspx?CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&ACTION_ON_PAYMENT=" + strAction.split('^')[1] + "&";
		                strURL = "ReserveTab.aspx?ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value + "&POLICY_ID=" + document.getElementById("hidPOLICY_ID").value + "&POLICY_VERSION_ID=" + document.getElementById("hidPOLICY_VERSION_ID").value + "&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&CALLED_FROM=ACT&ACTIVITY_REASON=" + Recovery;
		                break;
		            case Reserve_Update:
		            case Reserve_Creation:
		                strURL = "ReserveTab.aspx?ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value + "&POLICY_ID=" + document.getElementById("hidPOLICY_ID").value + "&POLICY_VERSION_ID=" + document.getElementById("hidPOLICY_VERSION_ID").value + "&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&CALLED_FROM=ACT&ACTIVITY_REASON=" + Claim_Payment;
		                break;
		            case Reinsurance:
		                //if(document.getElementById("hidACTIVITY_STATUS").value!='11801')
		                //	strURL = "ReserveDetails.aspx?CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&RESERVE_UPDATE=0" + "&ACTION_ON_PAYMENT=" + strAction.split('^')[1] + "&";
		                strURL = "ReserveTab.aspx?ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value + "&POLICY_ID=" + document.getElementById("hidPOLICY_ID").value + "&POLICY_VERSION_ID=" + document.getElementById("hidPOLICY_VERSION_ID").value + "&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&CALLED_FROM=ACT&ACTIVITY_REASON=" + Reinsurance;
		                //strURL = "AddReserveDetails.aspx?CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&RESERVE_UPDATE=0" + "&CALLED_FROM=NTF&"; 
		                break;
		            case Expense_Payment:
		                strURL = "ActivityExpenseTab.aspx?CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&ACTION_ON_PAYMENT=" + strAction.split('^')[1] + "&";
		                break;
		            case Claim_Payment:
		                //strURL = "PaymentDetailsTab.aspx?CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&ACTION_ON_PAYMENT=" + strAction.split('^')[1] + "&";
		                strURL = "ReserveTab.aspx?ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value + "&POLICY_ID=" + document.getElementById("hidPOLICY_ID").value + "&POLICY_VERSION_ID=" + document.getElementById("hidPOLICY_VERSION_ID").value + "&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&CALLED_FROM=ACT&ACTIVITY_REASON=" + Claim_Payment;
		                break;
		            default:
		                strURL = "ReserveTab.aspx?ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value + "&POLICY_ID=" + document.getElementById("hidPOLICY_ID").value + "&POLICY_VERSION_ID=" + document.getElementById("hidPOLICY_VERSION_ID").value + "&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&CALLED_FROM=ACT&ACTIVITY_REASON=" + Claim_Payment;
		                break;
		        }
		        if (strURL != "")
		            top.botframe.location.href = strURL;
		        return false;
		    }
		    function setTab() {
		        //				Url="Reports/PaidClaimsByCoverageReport.aspx?&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value +  "&";
		        //				DrawTab(2,top.frames[1],'Coverage Report',Url);								
		    }

		    function Init() {
		        
		        if (document.getElementById("hidACTIVITY_ID").value != null && document.getElementById("hidACTIVITY_ID").value != "" && document.getElementById("hidACTIVITY_STATUS").value != '11801' && document.getElementById("hidACTIVITY_STATUS").value != '11986') {
		            if (document.getElementById("hidIS_ACTIVE").value == "Y") {
		                if (document.getElementById("btnCompleteActivity"))
		                    document.getElementById("btnCompleteActivity").style.display = "inline";
		            }
		            else {
		                if (document.getElementById("btnCompleteActivity")) {

		                    document.getElementById("btnCompleteActivity").style.display = "none";
		                    //document.getElementById("btnActivateDeactivate").value = "Activate";				        

		                    if (document.getElementById("btnActivateDeactivate")) {
		                        document.getElementById("btnActivateDeactivate").style.display = "inline"
		                    }


		                }
		            }


		            if (document.getElementById("btnActivateDeactivate")) {

		                if (document.getElementById("hidIS_ACTIVE").value == "Y") {
		                    //Done for Itrack Issue 7723 (Note 7) on 6 Sept 2010
		                    if (document.getElementById("hidIS_VOIDED_REVERSED_ACTIVITY").value == "1") {
		                        if (document.getElementById("btnActivateDeactivate"))
		                            document.getElementById("btnActivateDeactivate").style.display = "none";
		                    }
		                    else {



		                        if (document.getElementById("btnActivateDeactivate"))
		                            document.getElementById("btnActivateDeactivate").style.display = "inline";
		                    }
		                }
		                else {


		                    if (document.getElementById("btnCompleteActivity"))
		                        document.getElementById("btnCompleteActivity").style.display = "none";
		                }
		            }
		            //if(document.getElementById("btnActivateDeactivate"))
		            //	document.getElementById("btnActivateDeactivate").style.display = "inline";
		        }
		        else {
		          
		           
		            if (document.getElementById("btnCompleteActivity") )
		                document.getElementById("btnCompleteActivity").style.display = "none";

		            if (document.getElementById("btnActivateDeactivate") )
		                document.getElementById("btnActivateDeactivate").style.display = "none";
		       
		            var strAction = document.getElementById("cmbACTION_ON_PAYMENT").value;
		            var ACTION_ON_PAYMENT = strAction.split('^')[0];

		            if (ACTION_ON_PAYMENT == "11774" || ACTION_ON_PAYMENT == "11775" || ACTION_ON_PAYMENT == "11776") {
		                if (document.getElementById("hidACTIVITY_ID").value == "") {
		                    if (document.getElementById("btnVoidActivity"))
		                        document.getElementById("btnVoidActivity").style.display = "none"

		                    if (document.getElementById("btnReverseActivity"))
		                        document.getElementById("btnReverseActivity").style.display = "none"
		                }
		                else if (document.getElementById("hidACTIVITY_ID").value != "") {
		                    if (document.getElementById("hidIS_BNK_RECONCILED").value == "N") {
		                        if (document.getElementById("btnVoidActivity"))
		                            document.getElementById("btnVoidActivity").style.display = "inline"

		                        if (ACTION_ON_PAYMENT == "11774" || ACTION_ON_PAYMENT == "11775") {
		                            if (document.getElementById("btnReverseActivity"))
		                                document.getElementById("btnReverseActivity").style.display = "inline"
		                        }
		                        else {
		                            if (document.getElementById("btnReverseActivity"))
		                                document.getElementById("btnReverseActivity").style.display = "none"
		                        }
		                    }
		                    else if (document.getElementById("hidACCOUNTING_SUPPRESSED").value == "Y") {
		                        if (document.getElementById("btnVoidActivity"))
		                            document.getElementById("btnVoidActivity").style.display = "none"

		                        if (ACTION_ON_PAYMENT == "11774" || ACTION_ON_PAYMENT == "11775") {
		                            if (document.getElementById("btnReverseActivity"))
		                                document.getElementById("btnReverseActivity").style.display = "inline"
		                        }
		                        else {
		                            if (document.getElementById("btnReverseActivity"))
		                                document.getElementById("btnReverseActivity").style.display = "none"
		                        }
		                    }
		                }
		            }
		            else {
		                if (document.getElementById("btnVoidActivity"))
		                    document.getElementById("btnVoidActivity").style.display = "none"

		                if (document.getElementById("btnReverseActivity"))
		                    document.getElementById("btnReverseActivity").style.display = "none"
		            }

		            if (document.getElementById("chkACCOUNTING_SUPPRESSED"))
		                document.getElementById("chkACCOUNTING_SUPPRESSED").style.display = "none"

		            if (document.getElementById("capACCOUNTING_SUPPRESSED"))
		                document.getElementById("capACCOUNTING_SUPPRESSED").style.display = "none"

		            if (document.getElementById("hidACCOUNTING_SUPPRESSED").value == "" || document.getElementById("hidACCOUNTING_SUPPRESSED").value == "0" || document.getElementById("hidACCOUNTING_SUPPRESSED").value == "false") {
		                if (document.getElementById("capACCOUNTING_SUPPRESSED_CHECKED"))
		                    document.getElementById("capACCOUNTING_SUPPRESSED_CHECKED").style.display = "none"
		            }
		            else {
		                if (document.getElementById("capACCOUNTING_SUPPRESSED_CHECKED"))
		                    document.getElementById("capACCOUNTING_SUPPRESSED_CHECKED").style.display = "inline"
		            }
		        }

		        //cmbACTIVITY_REASON_Change();
		        setTab();
		        ApplyColor();
		        ChangeColor();
		    }
		    function SetParentActivitySummaryRow() {
		        parent.document.getElementById('lblSummaryRow').innerHTML = document.getElementById('hidSummaryRow').value
		    }
		    /*function cmbACTIVITY_REASON_Change()
		    {
		    combo = document.getElementById("cmbACTIVITY_REASON");
		    if(combo!=null && combo.selectedIndex!=-1 && combo.options[combo.selectedIndex].value==11773)
		    {
		    document.getElementById("rfvRESERVE_TRAN_CODE").setAttribute("isValid",true);
		    document.getElementById("rfvRESERVE_TRAN_CODE").setAttribute("enabled",true);					
		    document.getElementById("trReseve").style.display="inline";
		    }
		    else
		    {
		    document.getElementById("rfvRESERVE_TRAN_CODE").setAttribute("isValid",false);
		    document.getElementById("rfvRESERVE_TRAN_CODE").setAttribute("enabled",false);
		    document.getElementById("rfvRESERVE_TRAN_CODE").style.display="none";
		    document.getElementById("trReseve").style.display="none";
		    }
					
		    }*/


		    //Added new function for dropdown cmbID
		    //Give the value in the format of Text1#Text2#Text3...

		    // txtREASON_DESCRIPTION is the id of textbox which contain value Text1#Text2#Text3...
		    //  txtDESCRIPTION is the id of textbox which contain Template corresponding to perticular value of dropdown.
		    var variable_ReasonDescription = '';
		    var variable_Description = '';
		    var Final_String1 = '';
		    var Split_Value = '';
		    function DropDownToTextBox(cmbID, txtDESCRIPTION) {
		        // debugger;
		        document.getElementById("txtREASON_DESCRIPTION").value = '';
		        variable_ReasonDescription = '';
		        variable_Description = '';
		        document.getElementById("txtDESCRIPTION").value = cmbID.options[cmbID.selectedIndex].value;
		        var Description_Tamplate = document.getElementById("txtDESCRIPTION").value;
		        var ReasonDescription_Split = Description_Tamplate.split('####');
		        var Count = '';
		        for (CountWord = 0; CountWord < ReasonDescription_Split.length; CountWord++) {
		            Count++;
		        }
		        var txt = 'Text';
		        var no = 1;
		        for (var i = 0; i < (Count - 1); i++) {
		            document.getElementById("txtREASON_DESCRIPTION").value += txt + no + '#';
		            variable_ReasonDescription += txt + no + '#';
		            no++;

		        }
		        var len = (document.getElementById("txtREASON_DESCRIPTION").value).length;
		        Final_String = document.getElementById("txtREASON_DESCRIPTION").value;

		        document.getElementById("txtREASON_DESCRIPTION").value = Final_String.substring(0, len - 1);
		        Final_String1 = document.getElementById("txtREASON_DESCRIPTION").value;
		        Split_Value = Final_String1.split('#');
		        variable_Description = document.getElementById("txtDESCRIPTION").value;
		    }

		    //function for repalcing value of txtDESCRIPTION by value of txtREASON_DESCRIPTION
		    function GetValue() {
		       
		        Page_ClientValidate("ACTIVITY");
		        if (!Page_IsValid)
		            return Page_IsValid;
		        else {
		            var ACTION_ON_PAYMENT;
		            ACTION_ON_PAYMENT = cmbValue();
		            //Modified by shubhanshu on date 05/07/2011 Itrack 1263 
		            if (document.getElementById("cmbACTION_ON_PAYMENT").disabled == false || ACTION_ON_PAYMENT < 180 || ACTION_ON_PAYMENT > 192)//	'FOR PAYMENT ACTIVITY
		            {
		                return true;
		            }
		        }

		        var ReasonDescription_Tamplate = document.getElementById("txtREASON_DESCRIPTION").value;
		        var Description_Tamplate = document.getElementById("txtDESCRIPTION").value;
		        var Dropdown_sel_Text = document.getElementById("cmbID").options[document.getElementById("cmbID").selectedIndex].text;
		        var ReasonDescription_Split = ReasonDescription_Tamplate.split('#');
		        var Variable_Final = variable_ReasonDescription.split('#');
		        var OlderValue_Description = variable_Description.split('####');
		        var Description_Split = variable_Description.split('####');

		        // IF DATA IS ALREADY UPDATED THEN NO NEED TO UPDATE FURTHER
		        if (OlderValue_Description.length > 0 && OlderValue_Description.length < 2)
		            return true;

		        // If txtREASON_DESCRIPTION is not edited.
		        if ((ReasonDescription_Split.length + 1) != Description_Split.length) {
		            var AlertMessag = document.getElementById("hidValidMessage").value;
		            alert(AlertMessag + ' ' + '(' + Final_String1 + ')');
		            return false;
		        }

		        var Final_Var = '';
		        var Flag = 0;
		        var i = 0;
		        for (; i < ReasonDescription_Split.length; i++) {
		            if (ReasonDescription_Split[i] != Variable_Final[i]) {

		                Description_Split[i] += " " + ReasonDescription_Split[i];
		                Final_Var += Description_Split[i];
		                Flag = 1;
		            }
		            // IF FORMAT IS NOT CORRECT.
		            else {
		                var AlertMessage = document.getElementById("hidTextMessage").value;
		                alert(AlertMessage + " " + Split_Value);
		                Flag = 0;
		                break; 		                
		            }
		        }

		        if (Description_Split[i] != undefined && ReasonDescription_Split[i] != Variable_Final[i]) {
		            Final_Var += " " + Description_Split[i];
		            Flag = 1;
		        }

		        if (Flag == 1) {
		            document.getElementById("txtDESCRIPTION").value = Final_Var;
		            document.getElementById("hidTEXT_DESCRIPTION").value = Final_Var;
		            document.getElementById("hidREASON_DESCRIPTION").value = ReasonDescription_Tamplate;
		            document.getElementById("hidTEXT_ID").value = Dropdown_sel_Text

		        }
		        else {
		            document.getElementById("txtDESCRIPTION").value = Description_Tamplate;
		            document.getElementById("hidTEXT_DESCRIPTION").value = Description_Tamplate;
		            return false;
		        }
		    }


		    /*----------------------------------------------------
		    ADDED BY SANTOSH KUMAR GAUTAM TO PRINT CLAIM RECEIPT
		    -----------------------------------------------------*/

		    function PrintClaimReceipt(strURL) {

		        window.open(strURL, null, 'resizable=yes,toolbar=0,location=0, directories=0, status=0, menubar=0,scrollbars=yes,width=800,height=500,left=150,top=50 ');

		        return false;
		    }

		    /*----------------------------------------------------
		    ADDED BY SANTOSH KUMAR GAUTAM TO PRINT CLAIM LETTER TO FOLLOWER
		    -----------------------------------------------------*/

		    function PrintClaimletter(strURL) {

		        window.open(strURL, null, 'resizable=yes,toolbar=0,location=0, directories=0, status=0, menubar=0,scrollbars=yes,width=800,height=500,left=150,top=50 ');

		        return false;
		    }
		    function viewCOILetter() {

		        var Claim_ID = document.getElementById('hidCLAIM_ID').value;
		        var Activity_ID = document.getElementById('hidACTIVITY_ID').value;
		        var POLICY_ID = document.getElementById('hidPOLICY_ID').value;
		        var POLICY_VERSION_ID = document.getElementById('hidPOLICY_VERSION_ID').value;
		        var CUSTOMER_ID = document.getElementById('hidCUSTOMER_ID').value;
		        var url = "CededCOILetterLink.aspx?Claim_ID=" + Claim_ID + "&Activity_ID=" + Activity_ID + "&POLICY_ID=" + POLICY_ID + "&POLICY_VERSION_ID=" + POLICY_VERSION_ID + "&CUSTOMER_ID=" + CUSTOMER_ID;
		        ShowPopup(url, '', 900, 600);
		        return false;
		    }

		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="CLM_ACTIVITY" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="2"><asp:Label runat="server" ID="capLabel"></asp:Label></TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="2"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="40%"><asp:label id="capACTIVITY_DATE" runat="server"></asp:label>
                                        <br />
                                        <asp:label id="lblACTIVITY_DATE" runat="server"></asp:label>
                                        <br />
                                        <br />
                                    </TD>
									<TD class="midcolora" width="32%"><asp:label id="capCREATED_BY" runat="server"></asp:label>
                                        <br />
                                        <asp:label id="lblCREATED_BY" runat="server"></asp:label></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capACTION_ON_PAYMENT" runat="server"></asp:label><span class="mandatory">*<br />
                                        </span><asp:dropdownlist id="cmbACTION_ON_PAYMENT" onfocus="SelectComboIndex('cmbACTION_ON_PAYMENT')" onchange="chkActivity();" runat="server"></asp:dropdownlist>
                                        <br />
										<asp:requiredfieldvalidator id="rfvACTION_ON_PAYMENT" runat="server" Display="Dynamic" ControlToValidate="cmbACTION_ON_PAYMENT"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="32%"><asp:label id="capCOI_TRAN_TYPE" runat="server"></asp:label><br />
										<asp:dropdownlist id="cmbCOI_TRAN_TYPE" 
                                           
                                            runat="server"></asp:dropdownlist><BR>
										
                                    </TD>
								</tr>
								
							
                                 <tr id="TrTextID" runat="server" >
									<TD class="midcolora" width="18%"><asp:label id="capID" runat="server"  ></asp:label><%--<span class="mandatory" id="span1" style="display:none;">*</span>--%><br />
                                        <asp:dropdownlist id="cmbID"  onfocus="SelectComboIndex('cmbACTION_ON_PAYMENT')"  runat="server"></asp:dropdownlist>
                                        <br />
										<asp:requiredfieldvalidator id="rfvTEXT_ID" runat="server" Display="Dynamic" 
                                            ControlToValidate="cmbID" ValidationGroup="ACTIVITY"></asp:requiredfieldvalidator>
										<%--<asp:customvalidator id="Customvalidator1" Runat="server" ControlToValidate="txtREASON_DESCRIPTION"
											Display="Dynamic" ClientValidationFunction="ValidateLength" ErrorMessage="Error"></asp:customvalidator>--%><br />
                                     </TD>
											<TD class="midcolora" width="32%">&nbsp;</TD>
						
								</tr>

								<tr id="TrReasonDesc" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capREASON_DESCRIPTION" runat="server"></asp:label>
                                        <br />
                                        <asp:textbox id="txtREASON_DESCRIPTION" runat="server"  Rows="5" TextMode="MultiLine" Columns="50"
											size="40" maxlength="500" OnBlur="GetValue();" onkeypress="MaxLength(this,1000);"></asp:textbox>
											
                                        <br />
										<asp:customvalidator id="csvREASON_DESCRIPTION" Runat="server" ControlToValidate="txtREASON_DESCRIPTION"
											Display="Dynamic" ClientValidationFunction="ValidateLength" ErrorMessage="Error"></asp:customvalidator></TD>
									<TD class="midcolora" width="32%">
                                       
                                       <asp:textbox id="txtDESCRIPTION"  ReadOnly="true" runat="server"  Rows="11" TextMode="MultiLine" Columns="80"
											size="40" maxlength="1000"  onkeypress="MaxLength(this,1000);"></asp:textbox>
                                      
                                      
                                      
									</TD>
								</tr>
								
								
								<tr id="TrReasonDescCase" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capREASON_DESCRIPTION_CASE" runat="server"></asp:label>
                                        <br />
                                        <asp:textbox id="txtREASON_DESCRIPTION_CASE" runat="server"  Rows="5" TextMode="MultiLine" Columns="50"
											size="40" maxlength="500" onkeypress="MaxLength(this,1000);"></asp:textbox>
											
                                        <br />
										<asp:customvalidator id="csvREASON_DESCRIPTION_CASE" Runat="server" ControlToValidate="txtREASON_DESCRIPTION"
											Display="Dynamic" ClientValidationFunction="ValidateLength" ErrorMessage="Error"></asp:customvalidator></TD>
									<TD class="midcolora" width="32%">&nbsp;</TD>
								</tr>
								
								<tr id="TrDesc" runat="server">
									<TD class="midcolora" width="18%">
                                       
                                        &nbsp;</TD>
									<TD class="midcolora" width="32%">&nbsp;</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%">
									<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server"  CausesValidation="False"></cmsb:cmsbutton>
                                        <asp:Button  class="clsButton"  ID="btnDelete" runat="server" Text="Delete" 
                                            onclick="btnDelete_Click" />
                                        <asp:Button  class="clsButton"  ID="btnClaimReciept" runat="server" 
                                            Text="Claim Receipt" onclick="btnClaimReciept_Click1" />
                                        <asp:Button  class="clsButton"  ID="btnClaimLetter" runat="server" 
                                            Text="Claim letter" />
                                        <asp:Button class="clsButton" id="btnCOI" runat="server" 
                                            Text="" CausesValidation="False" />
									<asp:label id="capACCOUNTING_SUPPRESSED_CHECKED" runat="server">Nothing was posted to GL for this activity as Suppress Accounting was Checked when activity was committed.</asp:label>
									</TD>
									<TD class="midcolorr" width="32%">
										<asp:checkbox id="chkACCOUNTING_SUPPRESSED" runat="server"></asp:checkbox><asp:label id="capACCOUNTING_SUPPRESSED" runat="server"></asp:label>
										<cmsb:cmsbutton class="clsButton" id="btnVoidActivity" runat="server" 
                                            Text="Void Activity" ></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnReverseActivity"  runat="server" Text="Reverse Activity"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnCompleteActivity" runat="server" OnClientClick="javascript:return ConfirmCompletion();" Text="Complete Activity"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnContinue" runat="server" OnClientClick="javascript:return GetValue();" Text="Continue"></cmsb:cmsbutton>
									</TD>
								</tr>
								<tr>
									<td class="midcolora" colSpan="2">
									&nbsp;
									    </td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			<INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			<INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
			<INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
			<INPUT id="hidSummaryRow" type="hidden" value="" name="hidSummaryRow" runat="server">
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			<INPUT id="hidACTIVITY_ID" type="hidden" value="0" name="hidACTIVITY_ID" runat="server">
			<INPUT id="hidACTION_ON_PAYMENT" type="hidden" value="0" name="hidACTION_ON_PAYMENT" runat="server">
			<INPUT id="hidACTIVITY_STATUS" type="hidden" name="hidACTIVITY_STATUS" runat="server">
			<INPUT id="hidAddGeneralActivity" type="hidden" name="hidAddGeneralActivity" runat="server">
			<INPUT id="hidCLAIM_STATUS" type="hidden" name="hidCLAIM_STATUS" runat="server">
			<INPUT id="hidALLOW_MANUAL" type="hidden" name="hidALLOW_MANUAL" runat="server" value="">
			<INPUT id="hidIS_SYSTEM_GENERATED" type="hidden" name="hidIS_SYSTEM_GENERATED" runat="server">
			<INPUT id="hidGL_POSTING_REQUIRED" type="hidden" name="hidGL_POSTING_REQUIRED" runat="server">
			<input type="hidden" id="hidAUTHORIZE" name="hidAUTHORIZE" runat="server">
			<input type="hidden" id="hidIS_BNK_RECONCILED" name="hidIS_BNK_RECONCILED" runat="server">
			<input type="hidden" id="hidCHECK_ID" name="hidCHECK_ID" runat="server">
			<input type="hidden" id="hidACCOUNTING_SUPPRESSED" name="hidACCOUNTING_SUPPRESSED" runat="server">
			<input type="hidden" id="hidIS_VOIDED_REVERSED_ACTIVITY" name="hidIS_VOIDED_REVERSED_ACTIVITY" runat="server">
			<input type="hidden" id="hidACTIVITY_AT_VOID" value="" name="hidACTIVITY_AT_VOID" runat="server">
			<input type="hidden" id="hidFOLLOWUP_ACTIVITY_AT_VOID" name="hidFOLLOWUP_ACTIVITY_AT_VOID" runat="server">
			<input type="hidden" id="hidTEXT_DESCRIPTION" name="hidTEXT_DESCRIPTION" runat="server">
			<input type="hidden" id="hidTEXT_ID" name="hidTEXT_ID" runat="server">
			<input type="hidden" id="hidCloseReserveMessage" name="hidCloseReserveMessage" runat="server">
			<input type="hidden" id="hidTextMessage" name="hidTextMessage" runat="server">
		    <input type="hidden" id="hidValidMessage" name="hidValidMessage" runat="server">
		    <input type="hidden" id="hidREASON_DESCRIPTION" name="hidREASON_DESCRIPTION" runat="server">
		    <input type="hidden" id="hidREASON_DESCRIPTION_CASE" name="hidREASON_DESCRIPTION_CASE" runat="server">
		    <input type="hidden" id="hidALERT_FLG" name="hidALERT_FLG" runat="server">
            <input type="hidden" id="hidPayment_Amt" name="hidPayment_Amt" runat="server">
           

<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
	<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> 
		</FORM>
		<script>
		    RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidACTIVITY_ID').value, true);
		</script>
	</BODY>
</HTML>

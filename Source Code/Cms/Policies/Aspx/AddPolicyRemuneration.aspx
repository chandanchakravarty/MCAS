<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPolicyRemuneration.aspx.cs"
    Inherits="Cms.Policies.Aspx.PolicyRemuneration" %>

<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>POL_REMUNERATION</title>
<link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >
    <style type="text/css">
        .DisplayNone
        {
            display: none;
        }
    </style>

    <script src="/cms/cmsweb/scripts/xmldom.js"></script>

    <script src="/cms/cmsweb/scripts/common.js"></script>

    <script src="/cms/cmsweb/scripts/form.js"></script>

    <script src="/cms/cmsweb/scripts/calendar.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>

    <script type="text/javascript" language="javascript">
        var PobjectID = new Array();
        var glCommissionType;
        function check() {

            var confirmmessage = '<%= confirmmessage %>';
            var alertmsg = '<%= alertmsg %>';
            var frm = document.POL_REMUNERATION;
            var boolAllChecked;
            boolAllChecked = false;

            if (SelectLeader()) {


                for (i = 0; i < frm.length; i++) {
                    e = frm.elements[i];

                    if (e.type == 'checkbox' && e.name.indexOf('chkSELECT') != -1) {

                        var splitid = e.id.split('_');

                        var HdnBrokerID = splitid[0] + '_' + splitid[1] + '_' + 'HdnBrokerID';
                        var cmbCOMMISSION_TYPE = splitid[0] + '_' + splitid[1] + '_' + 'cmbCOMMISSION_TYPE';
                        var flag = '<%=flag %>';
                        var BrokerNewID = document.getElementById(HdnBrokerID).value;
                        var comtype = document.getElementById(cmbCOMMISSION_TYPE).value;

                        if (e.checked == true && (flag != BrokerNewID || comtype != 43)) {

                            boolAllChecked = true;
                            break;

                        }
                    }


                }
                if (boolAllChecked == false) {
                    alert(alertmsg);
                    return false;
                }
                else {
                    var k = confirm(confirmmessage);
                    // return false;
                    return k;
                }
            } else
                return false;
        } //alert when delete row
        function Validate(objSource, objArgs) {
            var comm = parseFloat(document.getElementById(objSource.controltovalidate).value);
            if (comm < 0 || comm > 100) {
                document.getElementById(objSource.controltovalidate).select();
                objArgs.IsValid = false;
            }
            else
                objArgs.IsValid = true;
        } 	//validate commission %
        function onChangeType(objcmb, objcmbNameCntrl, objtxtNameCntrl) {
            var splitid = objcmbNameCntrl.split('_');
            var rfvname = splitid[0] + '_' + splitid[1] + '_' + 'rfvBROKER_NAME';
            var Leader = splitid[0] + '_' + splitid[1] + '_' + 'chkLEADER';
            var branchid = splitid[0] + '_' + splitid[1] + '_' + 'txtBRANCH';
            var Amount = splitid[0] + '_' + splitid[1] + '_' + 'txtAMOUNT';
            var txtCommission = splitid[0] + '_' + splitid[1] + '_' + 'txtCOMMISSION';
            var hidCOMMISSION_TYPE = splitid[0] + '_' + splitid[1] + '_' + 'hidCOMMISSION_TYPE';
            if (typeof (objcmb) != 'undefined') {
                document.getElementById(hidCOMMISSION_TYPE).value = document.getElementById(objcmb).value;
                if (document.getElementById(objcmb).value == '43') {         //43 For commission type Commission 14629

                    document.getElementById(objcmbNameCntrl).selectedIndex = -1;
                   // document.getElementById(objtxtNameCntrl).style.display = 'none';
                    document.getElementById(objcmbNameCntrl).style.display = 'inline';

                    // document.getElementById(rfvname).setAttribute('enabled', true);
                    //document.getElementById(rfvname).setAttribute('isValid', false);

                    document.getElementById(Leader).disabled = false;
                    //document.getElementById(branchid).disabled = false;

                    document.getElementById(Amount).value = '' // to fix itrack issue 199
                    document.getElementById(Amount).disabled = true;
                    document.getElementById(txtCommission).value = '';
                    document.getElementById(txtCommission).disabled = false;

                }
                else if (document.getElementById(objcmb).value == '44') {        //44 For Enrollment fee  14630
                    document.getElementById(objcmbNameCntrl).selectedIndex = -1;
                   // document.getElementById(objtxtNameCntrl).style.display = 'none';
                    document.getElementById(objcmbNameCntrl).style.display = 'inline';

                    // document.getElementById(rfvname).setAttribute('enabled', true);
                    //document.getElementById(rfvname).setAttribute('isValid', false);

                    document.getElementById(Leader).disabled = true;
                    //document.getElementById(branchid).disabled = false;

                    document.getElementById(Amount).value = '';
                    document.getElementById(Amount).disabled = true;

                    document.getElementById(txtCommission).value = '';
                    document.getElementById(txtCommission).disabled = false;


                }

                else if (document.getElementById(objcmb).value == '45') {                      //45 for Pro-Labore commission Type 14631

//                    document.getElementById(objcmbNameCntrl).selectedIndex = -1;
//                    document.getElementById(objtxtNameCntrl).style.display = 'inline';
//                    document.getElementById(objcmbNameCntrl).style.display = 'none';

//                    //document.getElementById(rfvname).setAttribute('enabled', false);
//                    //document.getElementById(rfvname).setAttribute('isValid', true);
//                    // document.getElementById(rfvname).style.display = "none";

//                    document.getElementById(Leader).disabled = true;
//                    document.getElementById(branchid).disabled = true;


//                    document.getElementById(Amount).value = '';
//                    document.getElementById(Amount).disabled = true;

//                    document.getElementById(branchid).value = '';
//                    document.getElementById(txtCommission).value = '';
//                    document.getElementById(txtCommission).disabled = false;
                //                    document.getElementById(Leader).checked = false;

                document.getElementById(objcmbNameCntrl).selectedIndex = -1;
                //document.getElementById(objtxtNameCntrl).style.display = 'none';
                document.getElementById(objcmbNameCntrl).style.display = 'inline';

                // document.getElementById(rfvname).setAttribute('enabled', true);
                //document.getElementById(rfvname).setAttribute('isValid', false);

                document.getElementById(Leader).disabled = true;
                //document.getElementById(branchid).disabled = false;

               
                document.getElementById(txtCommission).value = '';
                document.getElementById(txtCommission).disabled = false;


                }

            }
        } //show the broker Name or Named Insured on commission Type Change

        function init() {
            ChangeColor();
            ApplyColor();
            DisablePolicyCommission();
            FillBrokerOnExceptionCase();

        }
        function DisablePolicyCommission() {
            var PRODUCT_TYPE = document.getElementById("hidPRODUCT_TYPE").value;
            if (PRODUCT_TYPE == '<%= MASTER_POLICY %>') {
                document.getElementById("trPOLICY_LEVEL_COMMISSION").style.display = 'none';
                document.getElementById("trSPACE").style.height = '25px';

            }

        }
        $(document).ready(function() {
            $('body').bind('keydown', function(event) {
                if (event.keyCode == '13') {
                    if (validateBroker())
                        return true;
                    else
                        return false;
                    //$("#btnSave").trigger('click');
                }
            });
            $("#btnSave").click(function() {
                if (validateBroker())
                    return true;
                else
                    return false;
            });
        });

     
        function validateBroker() {
            var PRODUCT_TYPE = document.getElementById("hidPRODUCT_TYPE").value;
            if (PRODUCT_TYPE == '<%= MASTER_POLICY %>') { //for master policy
                if (CheckCommissionPercentage()) {
                    if (CheckExists() == true) {
                        return Page_ClientValidate();
                    } else {
                        return false;
                    }

                } else {
                    return false;
                }
            }
            else {
                if (CheckExists() == true) {
                    return Page_ClientValidate();
                } else
                    return false;
            }

        }

        function CheckCommissionPercentage() {
            var msg = '<%=jscriptmsg %>'; //'"Commission percentage can't greater then 100%";
            msg = msg.split(',');
            var rowcount = document.getElementById("hidROW_COUNT").value;
            var PRODUCT_TYPE = document.getElementById("hidPRODUCT_TYPE").value;
            var CO_APPLICANT_ID = '';
            var COMMISSION_TYPE = '';
            var SELECT = '';
            var totalpercentage;
            var CommissionPes = 0;
            var EnrollmentPes = 0;
            var Prolabore = 0;
            var CoApplicant = new Array();
            var percentagevalue;

            for (i = 2; i < parseInt(rowcount) + 2; i++) {
                if (i < 10) {
                    var cmbCO_APPLICANT_ID1 = 'grdBROKER_ctl0' + i + '_' + 'cmbCO_APPLICANT_ID';
                    var cmbtypeid = 'grdBROKER_ctl0' + i + '_' + 'cmbCOMMISSION_TYPE';
                    var chkbox = 'grdBROKER_ctl0' + i + '_' + 'chkSELECT';
                    var percentage = 'grdBROKER_ctl0' + i + '_' + 'txtCOMMISSION';
                }
                else {
                    var cmbCO_APPLICANT_ID1 = 'grdBROKER_ctl' + i + '_' + 'cmbCO_APPLICANT_ID';
                    var cmbtypeid = 'grdBROKER_ctl' + i + '_' + 'cmbCOMMISSION_TYPE';
                    var chkbox = 'grdBROKER_ctl' + i + '_' + 'chkSELECT';
                    var percentage = 'grdBROKER_ctl' + i + '_' + 'txtCOMMISSION';

                }

               
                if (document.getElementById(cmbCO_APPLICANT_ID1) != null) { CO_APPLICANT_ID = document.getElementById(cmbCO_APPLICANT_ID1).value; }
                if (document.getElementById(cmbtypeid) != null) { COMMISSION_TYPE = document.getElementById(cmbtypeid).value; }
                if (document.getElementById(chkbox) != null) { SELECT = document.getElementById(chkbox).checked; }
                if (document.getElementById(percentage) != null) { percentagevalue = document.getElementById(percentage).value; percentagevalue = ReplaceAll(percentagevalue, ',', '.') }

                //if (SELECT) {
                var l = 0;
                var exists = false;
                for (k = 0; k < CoApplicant.length; k++) {
                    if (CoApplicant[k][0] == CO_APPLICANT_ID) {
                        exists = true;
                        l = k;
                    }
                } //end for loop

                if (!exists) {
                    var PercentageArr = new Array();
                    PercentageArr.add(CO_APPLICANT_ID)
                    if (COMMISSION_TYPE == '43')//for commission
                    {
                        CommissionPes = parseFloat(percentagevalue);
                        PercentageArr[1] = CommissionPes
                        //if (parseFloat(CommissionPes) > 100) { alert(msg[0]); return false }


                    }
                    else if (COMMISSION_TYPE == '44') //for enrollment fees
                    {
                        EnrollmentPes = parseFloat(percentagevalue);
                        PercentageArr[2] = EnrollmentPes
                        //if (parseFloat(EnrollmentPes) > 100) { alert(msg[1]); return false }
                    }
                    else if (COMMISSION_TYPE == '45') //for pro-labore fees
                    {
                        Prolabore = parseFloat(percentagevalue);
                        PercentageArr[3] = Prolabore
                        //if (parseFloat(Prolabore) > 100) { alert(msg[2]); return false }
                    }
                    CoApplicant.add(PercentageArr);
                } else {
                    var OldCOApplicant = CoApplicant[l];
                    if (COMMISSION_TYPE == '43')//for commission
                    {
                        if (CoApplicant[l][1] != 'undefined' && CoApplicant[l][1] != null) {
                            var a = parseFloat(CoApplicant[l][1]) + parseFloat(percentagevalue);
                            CoApplicant[l][1] = a;
                            //if (parseFloat(a) > 100) { alert(msg[0]); return false }
                        } else {
                            CoApplicant[l][1] = parseFloat(percentagevalue);
                            // if (parseFloat(percentagevalue) > 100) { alert(msg[0]); return false }
                        }
                    }
                    else if (COMMISSION_TYPE == '44') //for enrollment fees
                    {

                        if (CoApplicant[l][2] != 'undefined' && CoApplicant[l][2] != null) {
                            var a = parseFloat(CoApplicant[l][2]) + parseFloat(percentagevalue);
                            CoApplicant[l][2] = a;
                            //if (parseFloat(a) > 100) { alert(msg[1]); return false }
                        } else {
                            CoApplicant[l][2] = parseFloat(percentagevalue);
                            //if (parseFloat(percentagevalue) > 100) { alert(msg[1]); return false }
                        }
                        //var a = parseFloat(CoApplicant[l][2]) + parseFloat(percentagevalue);
                    }
                    else if (COMMISSION_TYPE == '45') //for enrollment fees
                    {

                        if (CoApplicant[l][3] != 'undefined' && CoApplicant[l][3] != null) {
                            var a = parseFloat(CoApplicant[l][3]) + parseFloat(percentagevalue);
                            CoApplicant[l][3] = a;
                            //if (parseFloat(a) > 100) { alert(msg[2]); return false }
                        } else {
                            CoApplicant[l][3] = parseFloat(percentagevalue);
                            //if (parseFloat(percentagevalue) > 100) { alert(msg[2]); return false }
                        }
                    }
                } //end  if (!exists)/else 
                // } //end  if (SELECT)
            }  //end first for loop

            for (j = 0; j < CoApplicant.length; j++) {
                if (CoApplicant[j][1] != 'undefined' && CoApplicant[j][1] != null) {
                    var b = parseFloat(CoApplicant[j][1])
                    if (parseFloat(b) > 100 || parseFloat(b) < 100) {
                        document.getElementById("lblMessage").innerHTML = msg[0];
                        return false;
                    }
                }
                if (CoApplicant[j][2] != 'undefined' && CoApplicant[j][2] != null) {
                    var b = parseFloat(CoApplicant[j][2])
                    if (parseFloat(b) > 100 || parseFloat(b) < 100) {
                        document.getElementById("lblMessage").innerHTML = msg[1];
                        return false;
                    }
                }
                if (CoApplicant[j][3] != 'undefined' && CoApplicant[j][3] != null) {
                    var b = parseFloat(CoApplicant[j][3])
                    if (parseFloat(b) > 100 || parseFloat(b) < 100) {
                        document.getElementById("lblMessage").innerHTML = msg[2];
                        return false;
                    }
                }
            }
            document.getElementById("lblMessage").innerHTML = '';
            return true;
        } //end function

        function CheckExists() {
            var msg = document.getElementById("hidSCRIPT_MSG").value;
            var rowcount = document.getElementById("hidROW_COUNT").value;

            for (i = 2; i < parseInt(rowcount) + 2; i++) {
                if (i < 10) {
                    var cmbCO_APPLICANT_ID1 = 'grdBROKER_ctl0' + i + '_' + 'cmbCO_APPLICANT_ID';
                    var cmbbrokerid = 'grdBROKER_ctl0' + i + '_' + 'cmbName';
                    var cmbtypeid = 'grdBROKER_ctl0' + i + '_' + 'cmbCOMMISSION_TYPE';
                    var riskid = 'grdBROKER_ctl0' + i + '_' + 'hdnPRODUCT_RISK_ID';
                    var chkbox = 'grdBROKER_ctl0' + i + '_' + 'chkSELECT';
                }
                else {
                    var cmbbrokerid = 'grdBROKER_ctl' + i + '_' + 'cmbName';
                    var cmbtypeid = 'grdBROKER_ctl' + i + '_' + 'cmbCOMMISSION_TYPE';
                    var riskid = 'grdBROKER_ctl' + i + '_' + 'hdnPRODUCT_RISK_ID';
                    var chkbox = 'grdBROKER_ctl' + i + '_' + 'chkSELECT';
                    var cmbCO_APPLICANT_ID1 = 'grdBROKER_ctl' + i + '_' + 'cmbCO_APPLICANT_ID';

                }
                if (document.getElementById(chkbox) != null && document.getElementById(cmbbrokerid) != null && document.getElementById(cmbtypeid) != null) {
                    if (document.getElementById(chkbox) != null) {
                        document.getElementById(chkbox).checked = document.getElementById(chkbox).checked;
                    }
                    if (document.getElementById(cmbbrokerid) != null) {
                        var cmbBrokervalue = document.getElementById(cmbbrokerid).value;
                    }
                    if (document.getElementById(cmbtypeid) != null) {
                        var cmbTypevalue = document.getElementById(cmbtypeid).value;
                    }
                    if (document.getElementById(riskid) != null) {
                        var riskvalue = document.getElementById(riskid).value;
                    }

                    if (document.getElementById(cmbCO_APPLICANT_ID1) != null) {
                        var Co_applicantid = document.getElementById(cmbCO_APPLICANT_ID1).value;
                    }
                    for (j = i + 1; j < parseInt(rowcount) + 2; j++) {
                       
                        
                        if (i < 9) {
                            var cmbbrokerid2 = 'grdBROKER_ctl0' + j + '_' + 'cmbName';
                            var cmbtypeid2 = 'grdBROKER_ctl0' + j + '_' + 'cmbCOMMISSION_TYPE';
                            var riskid2 = 'grdBROKER_ctl0' + j + '_' + 'hdnPRODUCT_RISK_ID';
                            var cmbCO_APPLICANT_ID2 = 'grdBROKER_ctl0' + j + '_' + 'cmbCO_APPLICANT_ID';
                            var chkbox2 = 'grdBROKER_ctl0' + j + '_' + 'chkSELECT';
                        }
                        else {
                            var cmbbrokerid2 = 'grdBROKER_ctl' + j + '_' + 'cmbName';
                            var cmbtypeid2 = 'grdBROKER_ctl' + j + '_' + 'cmbCOMMISSION_TYPE';
                            var riskid2 = 'grdBROKER_ctl' + j + '_' + 'hdnPRODUCT_RISK_ID';
                            var cmbCO_APPLICANT_ID2 = 'grdBROKER_ctl' + j + '_' + 'cmbCO_APPLICANT_ID';
                            var chkbox2 = 'grdBROKER_ctl' + j + '_' + 'chkSELECT';
                        }

                        //if (document.getElementById(chkbox2) != null)
                            //if (!document.getElementById(chkbox2).checked)
                           // continue;
                        
                        if (document.getElementById(cmbbrokerid2) != null) {
                            var cmbBrokervalue2 = document.getElementById(cmbbrokerid2).value;
                        }
                        if (document.getElementById(cmbtypeid2) != null) {
                            var cmbTypevalue2 = document.getElementById(cmbtypeid2).value;
                        }
                        if (document.getElementById(riskid2) != null) {
                            var riskvalue2 = document.getElementById(riskid2).value;
                        }

                        if (document.getElementById(cmbCO_APPLICANT_ID2) != null) {
                            var CO_APPLICANT_ID2 = document.getElementById(cmbCO_APPLICANT_ID2).value;
                        }

                        if (document.getElementById(cmbbrokerid2) != null && document.getElementById(cmbtypeid2) != null && document.getElementById(riskid2) != null) {
                            if (cmbBrokervalue != 'undefined' && cmbBrokervalue2 != 'undefined' && cmbTypevalue != 'undefined' && cmbTypevalue2 != 'undefined' && riskvalue != 'undefined' && riskvalue2 != 'undefined')
                                if (cmbBrokervalue == cmbBrokervalue2 && cmbTypevalue == cmbTypevalue2 && riskvalue == riskvalue2) {
                                if (document.getElementById(cmbCO_APPLICANT_ID1) != null && document.getElementById(cmbCO_APPLICANT_ID2) != null && CO_APPLICANT_ID2 != 'undefined' && Co_applicantid != 'undefined') {
                                    if (CO_APPLICANT_ID2 == Co_applicantid) {
                                        document.getElementById("lblMessage").innerHTML = msg;
                                        return false;
                                    }
                                } else {
                                    document.getElementById("lblMessage").innerHTML = msg;
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
            //Page_ClientValidate()

        }    //Check  if Broker Name is already in select

        function CheckLeader(objchk, riskid) {
            var frm = document.POL_REMUNERATION
            for (i = 0; i < frm.length; i++) {
                var e = frm.elements[i];
                var ctl = objchk.id.split('_');
                var hdnid = ctl[0] + '_' + ctl[1] + '_hdnPRODUCT_RISK_ID';
                if (e.type == 'checkbox' && e.id.indexOf('_chkLEADER') != -1 && document.getElementById(hdnid).value == riskid) {
                    document.getElementById(e.id).checked = false;
                }
            }

            objchk.checked = true;
        }  //select only one broker as leader at a time

        function validProLabore() {
            var frm = document.POL_REMUNERATION
            for (i = 0; i < frm.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'select-one' && e.name.indexOf('cmbCOMMISSION_TYPE') != -1) {
                    var ctl = e.id.split('_');
                    var cmbid = ctl[0] + '_' + ctl[1] + '_cmbName';
                    var txtid = ctl[0] + '_' + ctl[1] + '_txtNAME';
                    var rfvid = ctl[0] + '_' + ctl[1] + '_rfvBROKER_NAME';
                    var chkid = ctl[0] + '_' + ctl[1] + '_chkLEADER';
                    var branchid = ctl[0] + '_' + ctl[1] + '_txtBRANCH';
                    var amountid = ctl[0] + '_' + ctl[1] + '_txtAMOUNT';
                    var txtCommission = ctl[0] + '_' + ctl[1] + '_txtCommission';

                    if (e.value == '45') {                 //  14631 For Pro-labour
                        document.getElementById(cmbid).style.display = 'inline';
                        //document.getElementById(txtid).style.display = 'none';


                        document.getElementById(chkid).disabled = true;
                        //document.getElementById(branchid).disabled = false;
                        document.getElementById(amountid).value = '';
                        document.getElementById(amountid).disabled = true;
                    }
                    if (e.value == '44') {                 //   For Enrollment Fee 14630

                        document.getElementById(cmbid).style.display = 'inline';
                        //document.getElementById(txtid).style.display = 'none';

                        document.getElementById(chkid).disabled = false;
                        //document.getElementById(branchid).disabled = true;
                        document.getElementById(txtCommission).disabled = false;

                    }
                    if (e.value == '43') {                 // For Commission 43 14629

                        document.getElementById(cmbid).style.display = 'inline';
                        //document.getElementById(txtid).style.display = 'none';

                        document.getElementById(chkid).disabled = false;

                        //document.getElementById(branchid).disabled = false;

                        document.getElementById(amountid).value = '';
                        document.getElementById(amountid).disabled = true;


                    }
                }
            }
        }

        function ShowAlertMessageForSave(chkbox) {
            var checked = false;
            var alertmsg = '<%= alertmsg %>';
            validatordisable();
            for (var i = 0; i < document.POL_REMUNERATION.length; i++) {
                control = document.POL_REMUNERATION.elements[i];
                if (control.type == 'checkbox' && control.name.indexOf(chkbox) != -1) {
                    var splitid = control.id.split('_');

                    var HdnBrokerID = splitid[0] + '_' + splitid[1] + '_' + 'HdnBrokerID';
                    var flag = '<%=flag %>';
                    var BrokerNewID = document.getElementById(HdnBrokerID).value;

                    if (control.checked || parseInt(flag) == parseInt(BrokerNewID)) {
                        checked = true;


                        var rfvCOMMISSION_TYPE = splitid[0] + '_' + splitid[1] + '_' + 'rfvCOMMISSION_TYPE';
                        var rfvBROKER_NAME = splitid[0] + '_' + splitid[1] + '_' + 'rfvBROKER_NAME';
                        var revBRANCH = splitid[0] + '_' + splitid[1] + '_' + 'revBRANCH';
                        var rfvCOMMISSION = splitid[0] + '_' + splitid[1] + '_' + 'rfvCOMMISSION';
                        var revCOMMISSION = splitid[0] + '_' + splitid[1] + '_' + 'revCOMMISSION';
                        var csvCOMMISSION = splitid[0] + '_' + splitid[1] + '_' + 'csvCOMMISSION';
                        var rfvAMOUNT = splitid[0] + '_' + splitid[1] + '_' + 'rfvAMOUNT';
                        var revAMOUNT = splitid[0] + '_' + splitid[1] + '_' + 'revAMOUNT';
                        var cmbCOMMISSION_TYPE = splitid[0] + '_' + splitid[1] + '_' + 'cmbCOMMISSION_TYPE';
                        var cmbName = splitid[0] + '_' + splitid[1] + '_' + 'cmbName';
                        var txtBRANCH = splitid[0] + '_' + splitid[1] + '_' + 'txtBRANCH';
                        var txtCOMMISSION = splitid[0] + '_' + splitid[1] + '_' + 'txtCOMMISSION';
                        var txtAMOUNT = splitid[0] + '_' + splitid[1] + '_' + 'txtAMOUNT';

                        if (document.getElementById(cmbCOMMISSION_TYPE).selectedIndex == 0) {

                            document.getElementById(rfvCOMMISSION_TYPE).setAttribute('enabled', true);
                            document.getElementById(rfvCOMMISSION_TYPE).setAttribute('isValid', false);
                            document.getElementById(rfvCOMMISSION_TYPE).style.display = "inline";

                        }
                        if (document.getElementById(cmbCOMMISSION_TYPE).value == '43' || document.getElementById(cmbCOMMISSION_TYPE).value == '45') {
                            if (document.getElementById(txtCOMMISSION).value == '') {
                                document.getElementById(rfvCOMMISSION).setAttribute('enabled', true);
                                document.getElementById(rfvCOMMISSION).setAttribute('isValid', false);
                                document.getElementById(rfvCOMMISSION).style.display = "inline";

                            }
                            else {
                                document.getElementById(revCOMMISSION).setAttribute('enabled', true);
                                document.getElementById(revCOMMISSION).setAttribute('isValid', false);
                                document.getElementById(revCOMMISSION).style.display = "inline";
                                document.getElementById(csvCOMMISSION).setAttribute('enabled', true);
                                document.getElementById(csvCOMMISSION).setAttribute('isValid', false);
                                document.getElementById(csvCOMMISSION).style.display = "inline";
                            }
                        }

                        if (document.getElementById(cmbCOMMISSION_TYPE).value == '43' || document.getElementById(cmbCOMMISSION_TYPE).value == '44') {
                            if (document.getElementById(cmbName).selectedIndex == -1) {

                                document.getElementById(rfvBROKER_NAME).setAttribute('enabled', true);
                                document.getElementById(rfvBROKER_NAME).setAttribute('isValid', false);
                                document.getElementById(rfvBROKER_NAME).style.display = "inline";

                            }
                        }

                        if (document.getElementById(txtBRANCH).value != '') {
                            document.getElementById(revBRANCH).setAttribute('enabled', true);
                            document.getElementById(revBRANCH).setAttribute('isValid', false);
                            document.getElementById(revBRANCH).style.display = "inline";

                        }

                        if (document.getElementById(cmbCOMMISSION_TYPE).value == '44') {

                            if (document.getElementById(txtAMOUNT).value == '') {

                                document.getElementById(rfvAMOUNT).setAttribute('enabled', true);
                                document.getElementById(rfvAMOUNT).setAttribute('isValid', false);
                                document.getElementById(rfvAMOUNT).style.display = "inline";

                            }
                            else {
                                document.getElementById(revAMOUNT).setAttribute('enabled', true);
                                document.getElementById(revAMOUNT).setAttribute('isValid', false);
                                document.getElementById(revAMOUNT).style.display = "inline";

                            }
                        }

                        Page_ClientValidate();

                        //break;
                    }
                    else {
                        checked = false;

                    }
                }
            }

            if (checked == false) {
                alert(alertmsg);
                return checked;
            }
            else {
                return CheckExists();
            }

        }
        function validatordisable() {
            if (typeof (Page_Validators) == "undefined")
                return;
            var i, val;
            for (i = 0; i < Page_Validators.length; i++) {
                val = Page_Validators[i];
                val.setAttribute('enabled', false);
            }

        }
        function disableCurrentRowvalidator(objid) {
            var arrsplID = objid.split('_');

            if (typeof (Page_Validators) == "undefined")
                return;
            var i, val;
            for (i = 0; i < Page_Validators.length; i++) {
                val = Page_Validators[i];
                if (val.id.indexOf(arrsplID[0]) != -1 && val.id.indexOf(arrsplID[1]) != -1 && val.id.indexOf("AMOUNT") == -1)
                    val.setAttribute('enabled', false);
            }

        }
        function EnableCurrentRowvalidator(objid) {

            var arrsplID = objid.split('_');

            if (typeof (Page_Validators) == "undefined")
                return;
            var i, val;
            for (i = 0; i < Page_Validators.length; i++) {
                val = Page_Validators[i];
                if (val.id.indexOf(arrsplID[0]) != -1 && val.id.indexOf(arrsplID[1]) != -1 && val.id.indexOf("AMOUNT") == -1)
                    val.setAttribute('enabled', true);
            }

        }

        function addnew(riskid) {
            var oldval = document.getElementById("hidRisk_ID").value;

            //		        if (oldval == "" && oldval != riskid) {
            //		          
            document.getElementById("hidRisk_ID").value = ""

            document.getElementById("hidRisk_ID").value = riskid;
            //		        }


        }

        function onCheckedChange(chkid) {

            var selectid = chkid.split('_');
            var hidSELECT = selectid[0] + '_' + selectid[1] + '_' + 'hidSELECT';

            if (document.getElementById(chkid).checked == true) {
                document.getElementById(hidSELECT).value = "1";
                EnableCurrentRowvalidator(chkid);
            }
            else {
                document.getElementById(hidSELECT).value = "0";
                disableCurrentRowvalidator(chkid)
            }
        }
        function setBrokerId(objCntrl) {
            var objCntrlid = objCntrl.id;
            var selectid = objCntrlid.split('_');
            var hidSELECT = selectid[0] + '_' + selectid[1] + '_' + 'HdnBrokerID';
            //var index = objCntrlid.substring(parseInt(objCntrlid.lastIndexOf('_')) + 1, objCntrlid.length).length;
           // var hidid = objCntrlid.substring(0, (objCntrlid.length - index)) + 'HdnBrokerID';
            if (document.getElementById(hidSELECT) != null) {
                document.getElementById(hidSELECT).value = objCntrl.value
            }

        }



        function BindBrokerName(obj) {
            PobjectID.add(obj.id);
            var splID = obj.id.split('_');
            glCommissionType = obj.value;
            var COMMISSION_TYPE = obj.value; //document.getElementById('cmbCOMMISSION_TYPE').value;
            var hiddenID = splID[0] + '_' + splID[1] + '_' + 'hidCOMMISSION_TYPE';
            if (document.getElementById(hiddenID) != null)
                document.getElementById(hiddenID).value = COMMISSION_TYPE;

            var CUSTOMER_ID = document.getElementById('hidCUSTOMER_ID').value;
            var POLICY_ID = document.getElementById('hidPOLICY_ID').value;
            var POLICY_VERSION_ID = document.getElementById('hidPOLICY_VERSION_ID').value;
            
            
//            if (CUSTOMER_ID != '' && POLICY_ID != '' && POLICY_VERSION_ID != '') {
//                //PageMethod("GetBrokerName", ["CUSTOMER_ID", CUSTOMER_ID, "POLICY_ID", POLICY_ID, "POLICY_VERSION_ID", POLICY_VERSION_ID, "COMMISSION_TYPE", COMMISSION_TYPE], AjaxSucceeded, AjaxFailed); //With parameters
//                PolicyRemuneration.GerBrokers(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, COMMISSION_TYPE, AjaxSucceeded, AjaxFailed)
//                //ConvertDataTableToDDLString
            
            var Broker = splID[0] + '_' + splID[1] + '_' + 'cmbName';
            
            filld(obj.id, COMMISSION_TYPE);
            document.getElementById(Broker).setAttribute('width', '100%');
            //}
        }



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

            for (i = 0; i < parseInt(PobjectID.length); i++) {
                FillBroker(result, PobjectID[i]);
            }
        }

        function AjaxFailed(result) {
        }


        function FillBroker(result, id) {
            var splObjectID = id.split('_');
            var FillcmbId = splObjectID[0] + '_' + splObjectID[1] + '_' + 'cmbName';

            
            // if (glCommissionType != '45')
            FillDDL(FillcmbId, "AGENCY_ID", "AGENCY_NAME_ACTIVE_STATUS", result)
            //            else
            //                FillDDL(FillcmbId, "APPLICANT_ID", "APPLICANTNAME", result)
        }

        function filld(id, CommissionType) {
            try {
                var splObjectID = id.split('_');
                var FillcmbId = splObjectID[0] + '_' + splObjectID[1] + '_' + 'cmbName';

                var HTML = '';
                if (CommissionType == '43')//Fill Broker/Agency If Commission Type is Commission in Name DDL
                    HTML = document.getElementById('hidBROKER').value;
                //else if (CommissionType == '44')//Fill Sales Agent If Commission Type is Enrollment Fee in Name DDL
                //  HTML = document.getElementById('hidSALES_AGENT').value;
               //itrack # 668  ,changed by Lalit Chauhan. Jan 07,2011
                //Fill  Broker/Agency + Sales Agent If Commission Type is ProLabore/Enrollment Fee in Name DDL ()  
                else if (CommissionType == '44' || CommissionType == '45')
                    HTML = document.getElementById('hidSALES_AGENT').value + document.getElementById('hidBROKER').value;

                //Add Blank value on Top
                var FinalHTML = "<OPTION value=''></OPTION>" + HTML;


                //$('#' + FillcmbId).html(result.value);
                $('#' + FillcmbId).html(FinalHTML);
                document.getElementById(FillcmbId).setAttribute('width', '100%');
                //setAttribute
            } catch (exx) {
                $('#' + FillcmbId).html("<OPTION value=''></OPTION>");
             }
        }
        
        function FillDDL(ControlID, ItemValue, ItemText, Result) {
            document.getElementById(ControlID).innerHTML = '';
            var Option = document.createElement("option")
            Option.value = "0";
            Option.text = "";
            document.getElementById(ControlID).add(Option);
            if (Result.d.Table.length > 0) {
                for (var row in Result.d.Table) {
                    if (row >= 0) {
                        var oOption = document.createElement("option");
                        oOption.value = eval('Result.d.Table[row].' + ItemValue)
                        oOption.text = eval('Result.d.Table[row].' + ItemText)
                        document.getElementById(ControlID).add(oOption);
                    }
                }
            }
        }
        function FillBrokerOnExceptionCase() {
            if (document.getElementById('hidReject').value == "1") {
                if (document.getElementById('hidROW_COUNT').value != '') {
                    for (i = 0; i < Page_Validators.length; i++) {
                        val = Page_Validators[i];
                        if (val.id.indexOf('grdBROKER') != -1 && val.id.indexOf('rfv') != -1 && (val.style.display == 'inline' || val.style.display == '')) {

                            var splcontrl_id = val.id.split('_');
                            var Nameid = splcontrl_id[0] + '_' + splcontrl_id[1] + '_' + 'cmbName';
                            var Typeid = splcontrl_id[0] + '_' + splcontrl_id[1] + '_' + 'cmbCOMMISSION_TYPE';
                            BindBrokerName(document.getElementById(Typeid))
                        }  //val.setAttribute('enabled', true);
                    }

                }
            }
        }
        function SelectAll(obj) {
        
            var frm = document.POL_REMUNERATION;
            for (i = 0; i < frm.length; i++) {
                e = frm.elements[i];
                if (obj.checked == true) {
                    if (e.type == 'checkbox' && e.disabled == false && e.id.indexOf('_chkSELECT') != -1) {
                        var index = e.id.substring(parseInt(e.id.lastIndexOf('_')) + 1, e.id.length).length;
                        var hidSELECT = e.id.substring(0, (e.id.length - index)) + 'hidSELECT';
                        document.getElementById(hidSELECT).value = '1';
                        e.checked = true;
                    }
                } else {
                if (e.type == 'checkbox' && e.disabled == false && e.id.indexOf('_chkSELECT') != -1) {
                    var index = e.id.substring(parseInt(e.id.lastIndexOf('_')) + 1, e.id.length).length;
                    var hidSELECT = e.id.substring(0, (e.id.length - index)) + 'hidSELECT';
                    document.getElementById(hidSELECT).value = '0';
                        e.checked = false;
                    }
                }
            }

        }

        function SelectLeader() {
            var Leadermsg = '<% =DeleteAlert %>';
            var frm = document.POL_REMUNERATION;
            var OneLeader = false;
             for (i = 0; i < frm.length; i++) {
                 e = frm.elements[i];
                 var objId = e.id.split('_');
                 var Select = objId[0] + '_' + objId[1] + '_chkLEADER';
                 if (e.type == 'checkbox' && e.disabled == false && e.id.indexOf('_chkSELECT') != -1 && document.getElementById(Select).checked == true) {
                     if (e.checked == true) {
                         alert(Leadermsg);
                         return false
                     }
                  }
              }
              return true;
             
         }
    </script>

</head>
<body oncontextmenu="return true;" leftmargin="0" onload="init();validProLabore();"
    rightmargin="0" ms_positioning="GridLayout">
    <form id="POL_REMUNERATION" runat="server" name="POL_REMUNERATION" onsubmit="" method="post">
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="midcolorc" align="right" colspan="4">
                <asp:Label ID="lblFormLoadMessage" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table2" width="100%" align="center" border="0">
                    <tbody>
                        <tr>
                            <td class="headereffectCenter" colspan="2">
                                <asp:Label ID="lblHeader" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="pageHeader">
                                <br />
                                <asp:Label ID="lblManHeader" runat="server" colspan="2"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolorc" align="center" colspan="2">
                                <asp:Label ID="lblMessage" runat="server" CssClass="errmsg"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trPOLICY_LEVEL_COMMISSION">
                            <td class="midcolora" colspan="2">
                                <asp:Label ID="capPOLICY_LEVEL_COMMISSION" runat="server">Policy Level Commission</asp:Label>
                                <br />
                                <asp:TextBox ID="txtPOLICY_LEVEL_COMMISSION" CssClass="INPUTCURRENCY" runat="server"
                                    ReadOnly="true"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr id="trSPACE">
                            <td class="midcolora" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolorc" style="text-align: left" colspan="2">
                                <asp:GridView runat="server" ID="grdBROKER" AutoGenerateColumns="False" OnRowDataBound="grdBROKER_RowDataBound"
                                    Width="100%">
                                    <HeaderStyle CssClass="headereffectWebGrid" HorizontalAlign="Left"></HeaderStyle>
                                    <RowStyle CssClass="midcolorba"></RowStyle>
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle Width="2%" />
                                            <HeaderTemplate>
                                            <input type="checkbox" id="chkALLSELECT" runat="server" />                                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkSELECT"></asp:CheckBox>
                                                <asp:HiddenField ID="hidSELECT" runat="server" />
                                                <asp:HiddenField runat="server" ID="hidREMUNERATION_ID" Value='<%# Eval("REMUNERATION_ID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="capREMUNERATION_ID" Text='<%# Eval("REMUNERATION_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="capBROKER_ID" Text='<%# Eval("BROKER_ID") %>'></asp:Label>
                                                <asp:HiddenField runat="server" ID="hidBROKER_ID" Value='<%# Eval("BROKER_ID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle Width="15%" />
                                            <HeaderTemplate>
                                                <asp:Label runat="server" ID="capCO_APPLICANT_ID">Co-Applicant</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList Width="100%" runat="server" ID="cmbCO_APPLICANT_ID">
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator runat="server" ErrorMessage="sdfs" Display="Dynamic"
                                                    ID="rfvCO_APPLICANT_ID" ControlToValidate="cmbCO_APPLICANT_ID"></asp:RequiredFieldValidator>
                                                <asp:HiddenField runat="server" ID="hidCO_APPLICANT_ID" Value='<%# Eval("CO_APPLICANT_ID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle Width="15%" />
                                            <HeaderTemplate>
                                                <asp:Label runat="server" ID="capCOMMISSION_TYPE">Commission Type</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList Width="100%" runat="server" ID="cmbCOMMISSION_TYPE">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator runat="server" ErrorMessage="" Display="Dynamic" ID="rfvCOMMISSION_TYPE"
                                                    ControlToValidate="cmbCOMMISSION_TYPE"></asp:RequiredFieldValidator>
                                                <asp:Label ID="COMMISSION_TYPE" Text='<%# Eval("COMMISSION_TYPE") %>' runat="server"
                                                    Visible="false"></asp:Label>
                                                <asp:HiddenField runat="server" ID="hidCOMMISSION_TYPE" Value='<%# Eval("COMMISSION_TYPE") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle Width="48%" />
                                            <HeaderTemplate>
                                                <asp:Label runat="server" ID="capName"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList Width="100%" runat="server" ID="cmbName">
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox runat="server" Visible="false" ID="txtNAME" size="50"></asp:TextBox>
                                                <br />
                                                <asp:RequiredFieldValidator runat="server" Enabled="true" ErrorMessage="" Display="Dynamic"
                                                    ID="rfvNAME" ControlToValidate="cmbName"></asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle Width="10%" />
                                            <HeaderTemplate>
                                                <asp:Label runat="server" ID="capBRANCH">Branch</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" Width="100%" ID="txtBRANCH" CausesValidation="true" MaxLength="6"
                                                    AutoCompleteType="Disabled" Text='<%# Eval("BRANCH") %>'></asp:TextBox><br />
                                                <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
                                                    ID="revBRANCH" ControlToValidate="txtBRANCH"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle Width="8%" />
                                            <HeaderTemplate>
                                                <asp:Label runat="server" Width="80%" ID="capCOMMISSION" Text="">Commission%</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtCOMMISSION" CssClass="INPUTCURRENCY" MaxLength="7"
                                                    CausesValidation="true" onblur="formatRate(this)" AutoCompleteType="Disabled"
                                                    Text='<%# Eval("COMMISSION_PERCENT") %>'></asp:TextBox><br />
                                                <asp:RequiredFieldValidator runat="server" Enabled="false" ErrorMessage="" Display="Dynamic"
                                                    ID="rfvCOMMISSION" ControlToValidate="txtCOMMISSION"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
                                                    ID="revCOMMISSION" ControlToValidate="txtCOMMISSION"></asp:RegularExpressionValidator>
                                                <asp:CustomValidator ID="csvCOMMISSION" Display="Dynamic" ControlToValidate="txtCOMMISSION"
                                                    ClientValidationFunction="Validate" runat="server"></asp:CustomValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="DisplayNone" ItemStyle-CssClass="DisplayNone">
                                            <ItemStyle Width="0%" />
                                            <HeaderTemplate>
                                                <asp:Label runat="server" ID="capAMOUNT">Amount</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtAMOUNT" CausesValidation="true" MaxLength="12"
                                                    CssClass="INPUTCURRENCY" AutoCompleteType="Disabled" Text='<%# Eval("AMOUNT") %>'></asp:TextBox><br />
                                                <asp:RequiredFieldValidator runat="server" ErrorMessage="" Enabled="false" Display="Dynamic"
                                                    ID="rfvAMOUNT" ControlToValidate="txtAMOUNT"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
                                                    ID="revAMOUNT" ControlToValidate="txtAMOUNT"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle Width="2%" />
                                            <HeaderTemplate>
                                                <asp:Label runat="server" ID="capLEADER">Leader</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkLEADER"></asp:CheckBox>
                                                <asp:HiddenField ID="HdnBrokerID" Value='<%# Eval("BROKER_ID") %>' runat="server" />
                                                <asp:Label ID="lblLEADER" runat="server" Text='<%# Eval("LEADER") %>' Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hdnPRODUCT_RISK_ID" runat="server" Value='<%# Eval("PRODUCT_RISK_ID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPRODUCT_RISK_ID" runat="server" Text='<%# Eval("PRODUCT_RISK_ID") %>'
                                                    Visible="true"></asp:Label>
                                                <asp:Label ID="lblRISK_NAME" runat="server" Text='<%# Eval("RISK_NAME") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblROW_ID" runat="server" Text='<%# Eval("lblROW_ID") %>'
                                                    Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolorc" colspan="2">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td class="midcolorc" style="text-align: left">
                                            <cmsb:CmsButton runat="server" ID="btnAdd" CausesValidation="false" Text="Add" CssClass="clsButton"
                                                OnClick="btnAdd_Click" Visible="false" /><cmsb:CmsButton runat="server" CausesValidation="false"
                                                    ID="btnDelete" OnClientClick="return check()" OnClick="btnDelete_Click" Text="Delete"
                                                    CssClass="clsButton"></cmsb:CmsButton>
                                        </td>
                                        <td class="midcolorc" style="text-align: right">
                                            <cmsb:CmsButton ID="btnSave" runat="server" CausesValidation="true" OnClick="btnSave_Click"
                                                Text="Save" CssClass="clsButton"></cmsb:CmsButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input type="hidden" id="hidAction" name="hidAction" runat="server" />
                                <input type="hidden" id="hidCOMMISSION_TYPE" name="hidCOMMISSION_TYPE" runat="server" />
                                <input type="hidden" id="hidBROKER_NAME" name="hidBROKER_NAME" runat="server" />
                                <input type="hidden" id="hidBRANCH" name="hidBRANCH" runat="server" />
                                <input type="hidden" id="hidCOMMISSION_PERCENT" name="hidCOMMISSION_PERCENT" runat="server" />
                                <input type="hidden" id="hidAMOUNT" name="hidAMOUNT" runat="server" />
                                <input type="hidden" id="hidLEADER" name="hidLEADER" runat="server" />
                                <input type="hidden" id="hidNAMED_INSURED" name="hidNAMED_INSURED" runat="server" />
                                <input type="hidden" id="hidROW_COUNT" name="hidROW_COUNT" runat="server" />
                                <input type="hidden" id="hidSCRIPT_MSG" name="hidSCRIPT_MSG" runat="server" />
                                <input type="hidden" id="hidRisk_ID" name="hidRisk_ID" runat="server" />
                                <input type="hidden" id="hidPRODUCT_TYPE" name="hidPRODUCT_TYPE" runat="server" />
                                <input type="hidden" id="hidCUSTOMER_ID" name="hidCUSTOMER_ID" runat="server" />
                                <input type="hidden" id="hidPOLICY_ID" name="hidPOLICY_ID" runat="server" />
                                <input type="hidden" id="hidPOLICY_VERSION_ID" name="hidPOLICY_VERSION_ID" runat="server" />
                                <input type="hidden" id="hidReject" name="hidReject" runat="server" value="0" />
                                <input type="hidden" id="hidSALES_AGENT" name="hidSALES_AGENT" runat="server" value="0" />
                                <input type="hidden" id="hidBROKER" name="hidBROKER" runat="server" value="0" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

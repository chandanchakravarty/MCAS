<%@ Page Language="c#" CodeBehind="AddCoverageDetails.aspx.cs" AutoEventWireup="false"
    Inherits="Cms.CmsWeb.Maintenance.AddCoverageDetails" ValidateRequest="false" %>

<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>MNT_COVERAGE</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet" />
    <script src="/cms/cmsweb/scripts/xmldom.js"></script>
    <script src="/cms/cmsweb/scripts/common.js"></script>
    <script src="/cms/cmsweb/scripts/form.js"></script>
    <script src="/cms/cmsweb/scripts/calendar.js"></script>
    <script language="javascript">
        var jsaAppDtFormat = "<%=aAppDtFormat  %>";

        function Initialise() {
           
            //While Adding a New Coverage :Message will not be displayed
            if (document.getElementById('hidCOV_ID') != null) {
                if (document.getElementById('hidCOV_ID').value == "0") {
                    document.getElementById("lblMsg").style.display = 'none';
                }
            }

            //if(document.getElementById('hidCOV_IDNew').value == '0')
            if (parent.document.getElementById('hidCalledFrom').value == 'Index') {
                var tab3 = document.getElementById('hidtab3').value;
                RemoveTab(1, top.frames[1]);
                var Url = "AddCoverageDetails.aspx?";
                DrawTab(1, top.frames[1], tab3, Url);
            }
            if (document.getElementById('hidCOV_IDNew').value != '0') {
                parent.document.getElementById('hidCalledFrom').value = 'Add';

            }
            CheckDeductible();
            setTab();
            //OnLobChanged();
            Check();
            DisableValidatorForDisabled();
            OnTypeChange();
            EnableDisablevalidator();
            CheckDeductible();
            EnableDisableReinsurance();
            OnLobChanged();
            makeMandatoryNonMandatory();
            HideUnhideAddDeductible();
            //EnableDisable();
            MandatoryDate();
            DefaultDate();
            // FillSubLOB();


        }
        function showPageLookupLayer(controlId) {
            var lookupMessage;
            switch (controlId) {
                case "cmbReinsuranceCov":
                    lookupMessage = "RRB.";
                    //lookupMessage	=	"RCC.";
                    break;
                case "cmbASLOB":
                    lookupMessage = "ASLOB.";
                    break;
                case "cmbReinsuranceCalc":
                    lookupMessage = "RC.";
                    break;
                case "cmbREIN_REPORT_BUCK":
                    lookupMessage = "RRB.";
                    break;
                case "cmbREIN_ASLOB":
                    lookupMessage = "ASLOB.";
                    break;
                case "cmbREIN_REPORT_BUCK_COMM":
                    lookupMessage = "RRB.";
                    break;

                default:
                    lookupMessage = "Look up code not found";
                    break;

            }
            showLookupLayer(controlId, lookupMessage);
        }

        function CheckDeductible() {
            if (document.getElementById('chkIsDeductApplicable').getAttribute('checked', 'true'))
                if (document.getElementById('hidLOB_ID').value != "1" || document.getElementById('hidLOB_ID').value != "6")
                    document.getElementById('hidDrawDeductible').value = "1";

        }
        function PreventTab() {
            document.getElementById('hidSTATE_REFRESH').value = '1';
        }

        function DisableValidatorForDisabled() {
            if (!(document.MNT_COVERAGE.hidCOV_ID.value == "0" || document.MNT_COVERAGE.hidCOV_ID.value == "New")) {
                EnableValidator('rfvADDDEDUCTIBLE_TYPE', false);
                EnableValidator('rfvDeductibleType', false);
                EnableValidator('rfvLIMITTYPE', false);
            }
        }
        function setTab() {
                        var tab1 = document.getElementById('hidtab1').value;
            var tab2 = document.getElementById('hidtab2').value;
            var tab3 = document.getElementById('hidtab3').value;
            var tab4 = document.getElementById('hidtab4').value;
            var tab5 = document.getElementById('hidtab5').value;
            var tab6 = document.getElementById('hidtab6').value;
            //Commented Endorsement ID check Itrack #3692 
            //Modified by hidTYPE .If Type is 2 then Endorsement tab will be shown. 
            if (document.getElementById('hidSTATE_REFRESH').value == '1') {

                document.getElementById('hidSTATE_REFRESH').value = '0';
                document.getElementById("cmbSTATE_ID").focus();
                return;
            }
          
            /*alert(document.MNT_COVERAGE.hidCOV_ID.value);
            alert('hidDrawLimit' +  +document.getElementById('hidDrawLimit').value);
            alert('hidDrawDeductible' +  +document.getElementById('hidDrawDeductible').value);
            alert('hidDrawAddDeductible' +  +document.getElementById('hidDrawAddDeductible').value);
            alert('hidENDORSEMENT_ID' +  +document.getElementById('hidENDORSEMENT_ID').value);
            alert('hidLOB_ID' +  +document.getElementById('hidLOB_ID').value);*/
            //Added By Ravindra (05-08-2006)
            if (document.MNT_COVERAGE.hidCOV_ID.value == "0" || document.MNT_COVERAGE.hidCOV_ID.value == "New") {
                RemoveTab(6, top.frames[1]);
                RemoveTab(5, top.frames[1]);
                RemoveTab(4, top.frames[1]);
                RemoveTab(3, top.frames[1]);
                RemoveTab(2, top.frames[1]);
                document.getElementById("cmbTYPE").focus();
            }
            else if (document.getElementById("cmbTYPE").selectedIndex == 3) {
                RemoveTab(6, top.frames[1]);
                RemoveTab(5, top.frames[1]);
                RemoveTab(4, top.frames[1]);
                RemoveTab(3, top.frames[1]);
                RemoveTab(2, top.frames[1]);
                //document.getElementById("cmbTYPE").focus();
                document.getElementById("lblMsg").style.display = 'none';
            }
            else {
                //none of Limit Deductible and Additional deductible is applicable to coverage
                //if(document.getElementById('hidISLIMITAPPLICABLE').value == "0" && document.getElementById('hidISDEDUCTIBELEAPPLICABLE').value == "0" && document.getElementById('hidISADDDEDUCTIBLE_APP').value =="0")
                if (document.getElementById('chkIsLimitApplicable').checked == false && document.getElementById('chkIsDeductApplicable').checked == false && document.getElementById('chkISADDDEDUCTIBLE_APP').checked == false) {

                    RemoveTab(6, top.frames[1]);
                    RemoveTab(5, top.frames[1]);
                    RemoveTab(4, top.frames[1]);
                    RemoveTab(3, top.frames[1]);
                    RemoveTab(2, top.frames[1]);

                    if (document.getElementById("txtCOV_CODE").value == '') {
                        document.getElementById("cmbTYPE").focus();
                        // Add New case
                    }
                    //else if(document.getElementById('hidENDORSEMENT_ID').value != '' && document.getElementById('hidENDORSEMENT_ID').value != '0')
                    else if (document.getElementById('hidTYPE').value == '2') {
                        Url = "AddEndorsment.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                        DrawTab(2, top.frames[1], tab4, Url);

                        Url = "EndorsementAttachmentIndex.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                        DrawTab(3, top.frames[1], tab5, Url);
                        document.getElementById("lblMsg").style.display = 'none';
                    }
                    else if (document.getElementById("cmbTYPE").selectedIndex == 2) {
                        //Url="AddEndorsment.aspx?";
                        Url = "AddEndorsment.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&'; //Done for Itrack Issue 5868 on 25 May 2009
                        DrawTab(2, top.frames[1], tab4, Url);

                        //Url="EndorsementAttachmentIndex.aspx?";
                        Url = "EndorsementAttachmentIndex.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + "&STATE_ID=" + document.getElementById('hidSTATE_ID').value + '&&'; //Done for Itrack Issue 5868 on 25 May 2009
                        DrawTab(3, top.frames[1], tab5, Url);
                        document.getElementById("lblMsg").style.display = 'none';
                    }
                }
                // Limit and Deductibile and Additional Deductible is applicable
                //else if(document.getElementById('hidISLIMITAPPLICABLE').value == "1" && document.getElementById('hidISDEDUCTIBELEAPPLICABLE').value == "1" && document.getElementById('hidISADDDEDUCTIBLE_APP').value == "1")
                else if (document.getElementById('chkIsLimitApplicable').checked == true && document.getElementById('chkIsDeductApplicable').checked == true && document.getElementById('chkISADDDEDUCTIBLE_APP').checked == true) {
                    RemoveTab(6, top.frames[1]);
                    RemoveTab(5, top.frames[1]);
                    RemoveTab(4, top.frames[1]);
                    RemoveTab(3, top.frames[1]);
                    RemoveTab(2, top.frames[1]);
                    Url = "AddCoverageRange.aspx?COVID=" + document.getElementById('hidCOV_ID').value + "&CalledFor=Limit" + "&LimitType=" + document.getElementById('hidLimitTypeValue').value + "&";
                    DrawTab(2, top.frames[1], tab1, Url);

                    if (document.getElementById('cmbLOB_ID').value == "1" || document.getElementById('cmbLOB_ID').value == "6") {
                        Url = "AddCoverageRange.aspx?COVID=" + document.getElementById('hidCOV_ID').value + "&CalledFor=Deduct" + "&LimitType=" + document.getElementById('hidLimitTypeValue').value + "&";
                        DrawTab(3, top.frames[1], tab6, Url);
                        Url = "AddCoverageRange.aspx?COVID=" + document.getElementById('hidCOV_ID').value + "&CalledFor=AddDeduct" + "&LimitType=" + document.getElementById('hidLimitTypeValue').value + "&";
                        DrawTab(4, top.frames[1], tab2, Url);
                    }
                    else {
                        Url = "AddCoverageRange.aspx?COVID=" + document.getElementById('hidCOV_ID').value + "&CalledFor=Deduct" + "&LimitType=" + document.getElementById('hidLimitTypeValue').value + "&";
                        DrawTab(3, top.frames[1], tab2, Url);
                    }

                    //if(document.getElementById('hidENDORSEMENT_ID').value != '' && document.getElementById('hidENDORSEMENT_ID').value != '0')
                    if (document.getElementById('hidTYPE').value == '2') {

                        if (document.getElementById('cmbLOB_ID').value == "1" || document.getElementById('cmbLOB_ID').value == "6") {
                            Url = "AddEndorsment.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(5, top.frames[1], tab4, Url);

                            Url = "EndorsementAttachmentIndex.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + "&STATE_ID=" + document.getElementById('hidSTATE_ID').value + '&&';
                            DrawTab(6, top.frames[1], tab5, Url);
                            document.getElementById("lblMsg").style.display = 'none';
                        }
                        else {
                            Url = "AddEndorsment.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(4, top.frames[1], tab4, Url);

                            Url = "EndorsementAttachmentIndex.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(5, top.frames[1], tab5, Url);
                            document.getElementById("lblMsg").style.display = 'none';
                        }
                    }
                    else if (document.getElementById("cmbTYPE").selectedIndex == 2) {
                        if (document.getElementById('cmbLOB_ID').value == "1" || document.getElementById('cmbLOB_ID').value == "6") {

                            Url = "AddEndorsment.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(5, top.frames[1], tab4, Url);

                            Url = "EndorsementAttachmentIndex.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + "&STATE_ID=" + document.getElementById('hidSTATE_ID').value + '&&';
                            DrawTab(6, top.frames[1], tab5, Url);
                            document.getElementById("lblMsg").style.display = 'none';
                        }
                        else {

                            Url = "AddEndorsment.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(4, top.frames[1], tab4, Url);

                            Url = "EndorsementAttachmentIndex.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(5, top.frames[1], tab5, Url);
                            document.getElementById("lblMsg").style.display = 'none';
                        }
                    }

                }
                // Limit and Additional Deductible is applicable
                //else if(document.getElementById('hidISLIMITAPPLICABLE').value == "1" && document.getElementById('hidISADDDEDUCTIBLE_APP').value == "1")
                else if (document.getElementById('chkIsLimitApplicable').checked == true && document.getElementById('chkISADDDEDUCTIBLE_APP').checked == true) {
                    RemoveTab(6, top.frames[1]);
                    RemoveTab(5, top.frames[1]);
                    RemoveTab(4, top.frames[1]);
                    RemoveTab(3, top.frames[1]);
                    RemoveTab(2, top.frames[1]);
                    Url = "AddCoverageRange.aspx?COVID=" + document.getElementById('hidCOV_ID').value + "&CalledFor=Limit" + "&LimitType=" + document.getElementById('hidLimitTypeValue').value + "&";
                    DrawTab(2, top.frames[1], tab, Url);
                    //Url="AddCoverageRange.aspx?COVID="+ document.getElementById('hidCOV_ID').value +"&CalledFor=AddDeduct"+"&LimitType="+document.getElementById('hidLimitTypeValue').value+"&";
                    //DrawTab(3,top.frames[1],'Additional Deductible Ranges',Url);

                    if (document.getElementById('cmbLOB_ID').value == "1" || document.getElementById('cmbLOB_ID').value == "6") {
                        Url = "AddCoverageRange.aspx?COVID=" + document.getElementById('hidCOV_ID').value + "&CalledFor=AddDeduct" + "&LimitType=" + document.getElementById('hidLimitTypeValue').value + "&";
                        DrawTab(3, top.frames[1], tab2, Url);

                        //if(document.getElementById('hidENDORSEMENT_ID').value != '' && document.getElementById('hidENDORSEMENT_ID').value != '0')
                        if (document.getElementById('hidTYPE').value == '2') {

                            Url = "AddEndorsment.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(4, top.frames[1], tab4, Url);

                            Url = "EndorsementAttachmentIndex.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(5, top.frames[1], tab5, Url);
                            document.getElementById("lblMsg").style.display = 'none';
                        }
                        else if (document.getElementById("cmbTYPE").selectedIndex == 2) {

                            Url = "AddEndorsment.aspx?";
                            DrawTab(4, top.frames[1], tab4, Url);

                            Url = "EndorsementAttachmentIndex.aspx?";
                            DrawTab(5, top.frames[1], tab5, Url);
                            document.getElementById("lblMsg").style.display = 'none';
                        }
                    }
                    else {
                        //if(document.getElementById('hidENDORSEMENT_ID').value != '' && document.getElementById('hidENDORSEMENT_ID').value != '0')
                        if (document.getElementById('hidTYPE').value == '2') {

                            Url = "AddEndorsment.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(3, top.frames[1], tab4, Url);

                            Url = "EndorsementAttachmentIndex.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(4, top.frames[1], tab5, Url);
                            document.getElementById("lblMsg").style.display = 'none';
                        }
                        else if (document.getElementById("cmbTYPE").selectedIndex == 2) {
                            Url = "AddEndorsment.aspx?";
                            DrawTab(3, top.frames[1], tab4, Url);

                            Url = "EndorsementAttachmentIndex.aspx?";
                            DrawTab(4, top.frames[1], tab5, Url);
                            document.getElementById("lblMsg").style.display = 'none';
                        }
                    }
                }
                //Both Deductibile and Additional deductible is applicable
                //else if(document.getElementById('hidISADDDEDUCTIBLE_APP').value == "1" && document.getElementById('hidISDEDUCTIBELEAPPLICABLE').value == "1")
                else if (document.getElementById('chkIsDeductApplicable').checked == true && document.getElementById('chkISADDDEDUCTIBLE_APP').checked == true) {

                    RemoveTab(6, top.frames[1]);
                    RemoveTab(5, top.frames[1]);
                    RemoveTab(4, top.frames[1]);
                    RemoveTab(3, top.frames[1]);
                    RemoveTab(2, top.frames[1]);
                    if (document.getElementById('cmbLOB_ID').value == "1" || document.getElementById('cmbLOB_ID').value == "6") {
                        Url = "AddCoverageRange.aspx?COVID=" + document.getElementById('hidCOV_ID').value + "&CalledFor=Deduct" + "&LimitType=" + document.getElementById('hidLimitTypeValue').value + "&";
                        DrawTab(2, top.frames[1], tab6, Url);

                        Url = "AddCoverageRange.aspx?COVID=" + document.getElementById('hidCOV_ID').value + "&CalledFor=AddDeduct" + "&LimitType=" + document.getElementById('hidLimitTypeValue').value + "&";
                        DrawTab(3, top.frames[1], tab2, Url);

                        //if(document.getElementById('hidENDORSEMENT_ID').value != '' && document.getElementById('hidENDORSEMENT_ID').value != '0')
                        if (document.getElementById('hidTYPE').value == '2') {
                            //alert(document.getElementById('hidENDORSEMENT_ID').value)
                            Url = "AddEndorsment.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(4, top.frames[1], tab4, Url);

                            Url = "EndorsementAttachmentIndex.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(5, top.frames[1], tab5, Url);
                            document.getElementById("lblMsg").style.display = 'none';
                        }
                        else if (document.getElementById("cmbTYPE").selectedIndex == 2) {
                            Url = "AddEndorsment.aspx?";
                            DrawTab(4, top.frames[1], tab4, Url);

                            Url = "EndorsementAttachmentIndex.aspx?";
                            DrawTab(5, top.frames[1], tab5, Url);
                            document.getElementById("lblMsg").style.display = 'none';
                        }

                    }
                    else {
                        Url = "AddCoverageRange.aspx?COVID=" + document.getElementById('hidCOV_ID').value + "&CalledFor=Deduct" + "&LimitType=" + document.getElementById('hidLimitTypeValue').value + "&";
                        DrawTab(2, top.frames[1], tab2, Url);

                        //if(document.getElementById('hidENDORSEMENT_ID').value != '' && document.getElementById('hidENDORSEMENT_ID').value != '0')
                        if (document.getElementById('hidTYPE').value == '2') {
                            Url = "AddEndorsment.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(3, top.frames[1], tab4, Url);

                            Url = "EndorsementAttachmentIndex.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(4, top.frames[1], tab5, Url);
                            document.getElementById("lblMsg").style.display = 'none';
                        }
                        else if (document.getElementById("cmbTYPE").selectedIndex == 2) {
                            Url = "AddEndorsment.aspx?";
                            DrawTab(3, top.frames[1], tab4, Url);

                            Url = "EndorsementAttachmentIndex.aspx?";
                            DrawTab(4, top.frames[1], tab5, Url);
                            document.getElementById("lblMsg").style.display = 'none';
                        }

                    }

                }
                //Both Limit and Deductibile is applicable
                //else if(document.getElementById('hidISLIMITAPPLICABLE').value == "1" && document.getElementById('hidISDEDUCTIBELEAPPLICABLE').value == "1")
                else if (document.getElementById('chkIsLimitApplicable').checked == true && document.getElementById('chkIsDeductApplicable').checked == true) {
                    RemoveTab(6, top.frames[1]);
                    RemoveTab(5, top.frames[1]);
                    RemoveTab(4, top.frames[1]);
                    RemoveTab(3, top.frames[1]);
                    RemoveTab(2, top.frames[1]);
                    Url = "AddCoverageRange.aspx?COVID=" + document.getElementById('hidCOV_ID').value + "&CalledFor=Limit" + "&LimitType=" + document.getElementById('hidLimitTypeValue').value + "&";
                    DrawTab(2, top.frames[1], tab1, Url);
                    //Url="AddCoverageRange.aspx?COVID="+ document.getElementById('hidCOV_ID').value +"&CalledFor=Deduct"+"&LimitType="+document.getElementById('hidLimitTypeValue').value+"&";
                    //DrawTab(3,top.frames[1],'Deductible Ranges',Url);
                    if (document.getElementById('cmbLOB_ID').value == "1" || document.getElementById('cmbLOB_ID').value == "6") {
                        Url = "AddCoverageRange.aspx?COVID=" + document.getElementById('hidCOV_ID').value + "&CalledFor=Deduct" + "&LimitType=" + document.getElementById('hidLimitTypeValue').value + "&";
                        DrawTab(3, top.frames[1], tab6, Url);
                    }
                    else {
                        Url = "AddCoverageRange.aspx?COVID=" + document.getElementById('hidCOV_ID').value + "&CalledFor=Deduct" + "&LimitType=" + document.getElementById('hidLimitTypeValue').value + "&";
                        DrawTab(3, top.frames[1], tab2, Url);
                    }
                    //if(document.getElementById('hidENDORSEMENT_ID').value != '' && document.getElementById('hidENDORSEMENT_ID').value != '0')
                    if (document.getElementById('hidTYPE').value == '2') {
                        Url = "AddEndorsment.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                        DrawTab(4, top.frames[1], tab4, Url);

                        Url = "EndorsementAttachmentIndex.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                        DrawTab(5, top.frames[1], tab5, Url);
                        document.getElementById("lblMsg").style.display = 'none';
                    }
                    else if (document.getElementById("cmbTYPE").selectedIndex == 2) {
                        Url = "AddEndorsment.aspx?";
                        DrawTab(4, top.frames[1], tab4, Url);

                        Url = "EndorsementAttachmentIndex.aspx?";
                        DrawTab(5, top.frames[1], tab5, Url);
                        document.getElementById("lblMsg").style.display = 'none';
                    }

                }
                //Only Limit is Applicable
                //else if(document.getElementById('hidISLIMITAPPLICABLE').value == "1") 
                else if (document.getElementById('chkIsLimitApplicable').checked == true) {
                    //if(document.getElementById('hidDrawAddDeductible').value == "1" && document.getElementById('hidDrawAddDeductible').innerHTML =="Are Deductibles Applicable?")
                    RemoveTab(6, top.frames[1]);
                    RemoveTab(5, top.frames[1]);
                    RemoveTab(4, top.frames[1]);
                    RemoveTab(3, top.frames[1]);
                    RemoveTab(2, top.frames[1]);
                    Url = "AddCoverageRange.aspx?COVID=" + document.getElementById('hidCOV_ID').value + "&CalledFor=Limit" + "&LimitType=" + document.getElementById('hidLimitTypeValue').value + "&";
                    DrawTab(2, top.frames[1], tab1, Url);
                    //if(document.getElementById('hidENDORSEMENT_ID').value != '' && document.getElementById('hidENDORSEMENT_ID').value != '0')
                    if (document.getElementById('hidTYPE').value == '2') {
                        Url = "AddEndorsment.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                        DrawTab(3, top.frames[1], tab4, Url);

                        Url = "EndorsementAttachmentIndex.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                        DrawTab(4, top.frames[1], tab5, Url);
                        document.getElementById("lblMsg").style.display = 'none';
                    }
                    else if (document.getElementById("cmbTYPE").selectedIndex == 2) {
                        Url = "AddEndorsment.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                        DrawTab(3, top.frames[1], tab4, Url);

                        Url = "EndorsementAttachmentIndex.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                        DrawTab(4, top.frames[1], tab5, Url);
                        document.getElementById("lblMsg").style.display = 'none';
                    }

                }
                //Only Deductible is applicable
                //else if(document.getElementById('hidISDEDUCTIBELEAPPLICABLE').value == "1") 
                else if (document.getElementById('chkIsDeductApplicable').checked == true || (document.getElementById('chkISADDDEDUCTIBLE_APP').checked == true && document.getElementById('hidDrawAddDeductible').innerHTML == "Are Deductibles Applicable?")) {
                    RemoveTab(6, top.frames[1]);
                    RemoveTab(5, top.frames[1]);
                    RemoveTab(4, top.frames[1]);
                    RemoveTab(3, top.frames[1]);
                    RemoveTab(2, top.frames[1]);
                    //Url="AddCoverageRange.aspx?COVID="+ document.getElementById('hidCOV_ID').value +"&CalledFor=Deduct"+"&LimitType="+document.getElementById('hidLimitTypeValue').value+"&";
                    //DrawTab(2,top.frames[1],'Deductible Ranges',Url);
                    if (document.getElementById('cmbLOB_ID').value == "1" || document.getElementById('cmbLOB_ID').value == "6") {
                        Url = "AddCoverageRange.aspx?COVID=" + document.getElementById('hidCOV_ID').value + "&CalledFor=Deduct" + "&LimitType=" + document.getElementById('hidLimitTypeValue').value + "&";
                        DrawTab(2, top.frames[1], tab6, Url);
                    }
                    else {
                        Url = "AddCoverageRange.aspx?COVID=" + document.getElementById('hidCOV_ID').value + "&CalledFor=Deduct" + "&LimitType=" + document.getElementById('hidLimitTypeValue').value + "&";
                        DrawTab(2, top.frames[1], tab2, Url);
                    }
                    //if(document.getElementById('hidENDORSEMENT_ID').value != '' && document.getElementById('hidENDORSEMENT_ID').value != '0')
                    if (document.getElementById('hidTYPE').value == '2') {
                        Url = "AddEndorsment.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                        DrawTab(3, top.frames[1], tab4, Url);

                        Url = "EndorsementAttachmentIndex.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                        DrawTab(4, top.frames[1], tab5, Url);
                        document.getElementById("lblMsg").style.display = 'none';
                    }
                    else if (document.getElementById("cmbTYPE").selectedIndex == 2) {
                        Url = "AddEndorsment.aspx?";
                        DrawTab(3, top.frames[1], tab4, Url);

                        Url = "EndorsementAttachmentIndex.aspx?";
                        DrawTab(4, top.frames[1], tab5, Url);
                        document.getElementById("lblMsg").style.display = 'none';
                    }

                }
                //Only Additional Deductible is applicable
                //else if(document.getElementById('hidISADDDEDUCTIBLE_APP').value == "1") 
                else if (document.getElementById('chkISADDDEDUCTIBLE_APP').checked == true) {
                    RemoveTab(6, top.frames[1]);
                    RemoveTab(5, top.frames[1]);
                    RemoveTab(4, top.frames[1]);
                    RemoveTab(3, top.frames[1]);
                    RemoveTab(2, top.frames[1]);
                    if (document.getElementById('cmbLOB_ID').value == "1" || document.getElementById('cmbLOB_ID').value == "6") {
                        Url = "AddCoverageRange.aspx?COVID=" + document.getElementById('hidCOV_ID').value + "&CalledFor=AddDeduct" + "&LimitType=" + document.getElementById('hidLimitTypeValue').value + "&";
                        DrawTab(2, top.frames[1], tab2, Url);
                        //if(document.getElementById('hidENDORSEMENT_ID').value != '' && document.getElementById('hidENDORSEMENT_ID').value != '0')
                        if (document.getElementById('hidTYPE').value == '2') {
                            Url = "AddEndorsment.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(3, top.frames[1], tab4, Url);

                            Url = "EndorsementAttachmentIndex.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(4, top.frames[1], tab5, Url);
                            document.getElementById("lblMsg").style.display = 'none';
                        }
                        else if (document.getElementById("cmbTYPE").selectedIndex == 2) {
                            Url = "AddEndorsment.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(3, top.frames[1], tab4, Url);

                            Url = "EndorsementAttachmentIndex.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(4, top.frames[1], tab5, Url);
                            document.getElementById("lblMsg").style.display = 'none';
                        }
                    }
                    else {
                        //if(document.getElementById('hidENDORSEMENT_ID').value != '' && document.getElementById('hidENDORSEMENT_ID').value != '0')
                        if (document.getElementById('hidTYPE').value == '2') {
                            Url = "AddEndorsment.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(2, top.frames[1], tab4, Url);

                            Url = "EndorsementAttachmentIndex.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSEMENT_ID').value + '&&';
                            DrawTab(3, top.frames[1], tab5, Url);
                            document.getElementById("lblMsg").style.display = 'none';
                        }
                        else if (document.getElementById("cmbTYPE").selectedIndex == 2) {
                            Url = "AddEndorsment.aspx?";
                            DrawTab(2, top.frames[1], tab4, Url);

                            Url = "EndorsementAttachmentIndex.aspx?";
                            DrawTab(3, top.frames[1], tab5, Url);
                            document.getElementById("lblMsg").style.display = 'none';
                        }
                    }


                }
            }
            //Added By Ravindra Ends here


        }


        function ResetScreen() {
            var covID = document.getElementById('hidCOV_ID').value;
            if (document.getElementById('hidOldData').value != "")
                document.location.href = "AddCoverageDetails.aspx?COV_ID=" + covID + "&transferdata=";
            else
                document.location.href = "AddCoverageDetails.aspx?"

            return false;
        }

        function SaveScreen() {
            var covID = document.getElementById('hidCOV_ID').value;

            if (covID != "")
                document.location.href = "AddCoverageDetails.aspx?COV_ID=" + covID + "&transferdata=";

            return false;
        }

        function Check() {

            if (document.getElementById('cmbCOVERAGE_TYPE').options[document.getElementById('cmbCOVERAGE_TYPE').selectedIndex].value == 'NO' ||
				document.getElementById('cmbCOVERAGE_TYPE').options[document.getElementById('cmbCOVERAGE_TYPE').selectedIndex].value == 'PL' ||
				document.getElementById('cmbCOVERAGE_TYPE').options[document.getElementById('cmbCOVERAGE_TYPE').selectedIndex].value == 'RL' ||
				document.getElementById('cmbCOVERAGE_TYPE').options[document.getElementById('cmbCOVERAGE_TYPE').selectedIndex].value == '') {


                document.getElementById('capINCLUDED').style.display = 'none';
                document.getElementById('txtINCLUDED').style.display = 'none';
                document.getElementById("rfvINCLUDED").setAttribute('isValid', false);
                document.getElementById("rfvINCLUDED").style.display = 'none';
                document.getElementById("rfvINCLUDED").setAttribute('enabled', false);
                document.getElementById("spnMandatory").style.display = "none";
            }
            else if (document.getElementById('cmbCOVERAGE_TYPE').options[document.getElementById('cmbCOVERAGE_TYPE').selectedIndex].value == 'S2') {
                document.getElementById('capINCLUDED').style.display = 'inline';
                document.getElementById('txtINCLUDED').style.display = 'inline';
                document.getElementById("rfvINCLUDED").setAttribute('enabled', false);
                document.getElementById("rfvINCLUDED").style.display = 'none';
                document.getElementById("rfvINCLUDED").setAttribute('isValid', false);
                document.getElementById("spnMandatory").style.display = "none";
            }
            else {
                document.getElementById('capINCLUDED').style.display = 'inline';
                document.getElementById('txtINCLUDED').style.display = 'inline';
                document.getElementById("rfvINCLUDED").setAttribute('enabled', false);
                document.getElementById("rfvINCLUDED").style.display = 'none';
                document.getElementById("rfvINCLUDED").setAttribute('isValid', false);
                document.getElementById("spnMandatory").style.display = "none";
            }

        }

        function LimitCheck() {

            if (document.getElementById('chkIsLimitApplicable').getAttribute('checked', 'true')) {
                document.getElementById('cmbLIMIT_TYPE').setAttribute('disabled', false)
            }
            else {
                document.getElementById('cmbLIMIT_TYPE').setAttribute('disabled', true)
                document.getElementById('cmbLIMIT_TYPE').style.backgroundColor = "White";
            }
            //document.getElementById('cmbLIMIT_TYPE')
            EnableDisablevalidator();
        }
        function DeductCheck() {
            if (document.getElementById('chkIsDeductApplicable').getAttribute('checked', 'true')) {
                document.getElementById('cmbDEDUCTIBLE_TYPE').setAttribute('disabled', false)
            }
            else {
                document.getElementById('cmbDEDUCTIBLE_TYPE').setAttribute('disabled', true)
                document.getElementById('cmbDEDUCTIBLE_TYPE').style.backgroundColor = "White";
            }
            //document.getElementById('cmbLIMIT_TYPE')
            EnableDisablevalidator();
        }
        function AddDeductCheck() {
            if (document.getElementById('chkISADDDEDUCTIBLE_APP').getAttribute('checked', 'true')) {
                document.getElementById('cmbADDDEDUCTIBLE_TYPE').setAttribute('disabled', false)
            }
            else {
                document.getElementById('cmbADDDEDUCTIBLE_TYPE').setAttribute('disabled', true)
                document.getElementById('cmbADDDEDUCTIBLE_TYPE').style.backgroundColor = "White";
            }
            //document.getElementById('cmbLIMIT_TYPE')
            EnableDisablevalidator();
        }

        function CheckEndDate(objSource, objArgs) {
            var startdate = document.getElementById('txtEFFECTIVE_FROM_DATE').value;
            var enddate = document.getElementById('txtEFFECTIVE_TO_DATE').value;
            objArgs.IsValid = CompareTwoDate(startdate, enddate, jsaAppDtFormat);
        }

        function CheckDisabledDate(objSource, objArgs) {
            var disableddate = document.getElementById('txtDISABLED_DATE').value;
            var enddate = document.getElementById('txtEFFECTIVE_TO_DATE').value;
            objArgs.IsValid = CompareTwoDate(enddate, disableddate, jsaAppDtFormat);

        }

        function CompareTwoDate(DateFirst, DateSec, FormatOfComparision) {
            if (DateFirst == "" || DateFirst == null)
                return false;
            if (DateSec == "" || DateSec == null)
                return true;
            var saperator = '/';
            var firstDate, secDate;
            var strDateFirst = DateFirst.split("/");
            var strDateSec = DateSec.split("/");
            if (FormatOfComparision.toLowerCase() == "dd/mm/yyyy") {
                firstDate = (strDateFirst[1].length != 2 ? '0' + strDateFirst[1] : strDateFirst[1]) + '/' + (strDateFirst[0].length != 2 ? '0' + strDateFirst[0] : strDateFirst[0]) + '/' + (strDateFirst[2].length != 4 ? '0' + strDateFirst[2] : strDateFirst[2]);
                secDate = (strDateSec[1].length != 2 ? '0' + strDateSec[1] : strDateSec[1]) + '/' + (strDateSec[0].length != 2 ? '0' + strDateSec[0] : strDateSec[0]) + '/' + (strDateSec[2].length != 4 ? '0' + strDateSec[2] : strDateSec[2]);
            }
            if (FormatOfComparision.toLowerCase() == "mm/dd/yyyy") {
                firstDate = DateFirst
                secDate = DateSec;
            }
            firstDate = new Date(firstDate);
            secDate = new Date(secDate);
            firstSpan = Date.parse(firstDate);
            secSpan = Date.parse(secDate);
            if (firstSpan <= secSpan)
                return true; // first is less than or equal
            else
                return false; // First date is greater
        }
        function HideUnhideAddDeductible() {

            document.getElementById('trADDAPPLICABLE').style.display = 'none';
            var cmblobID = document.getElementById('cmbLOB_ID');
            var st = document.getElementById('HidAPPDEDUCT').value;
            var srg = document.getElementById('HidDeductApplicable').value;
            var strg = document.getElementById('hidMess').value;
            if (cmblobID.options.selectedIndex != -1) {
                lobID = cmblobID.options[cmblobID.selectedIndex].value;
                if (document.getElementById('hidLOB_ID').value == "1" || document.getElementById('hidLOB_ID').value == "6" || lobID == "1" || lobID == "6") {
                    document.getElementById('trADDAPPLICABLE').style.display = 'inline';
                    document.getElementById('capDeduct_Applicable').innerHTML = "Are Additional Applicable?";
                    document.getElementById('capDeductibleType').innerHTML = "Additional Type";
                    document.getElementById('rfvDeductibleType').innerHTML = "Please select Additional type";

                }
                else {
                    document.getElementById('trADDAPPLICABLE').style.display = 'none';
                    document.getElementById('capDeduct_Applicable').innerHTML = st;
                    document.getElementById('capDeductibleType').innerHTML = srg;
                    document.getElementById('rfvDeductibleType').innerHTML = strg;
                }
            }
            EnableDisableReinsurance();
            //OnLobChanged();
        }
        function EnableDisablevalidator() {
            if (document.getElementById('chkIsLimitApplicable').getAttribute('checked', 'true')) {
                document.getElementById('rfvLIMITTYPE').setAttribute("enabled", true);
                document.getElementById('spnLIMITTYPE').style.display = "inline";
            }
            else {
                document.getElementById('rfvLIMITTYPE').setAttribute("enabled", false);
                document.getElementById('rfvLIMITTYPE').style.display = "none";
                document.getElementById('spnLIMITTYPE').style.display = "none";
            }
            if (document.getElementById('chkIsDeductApplicable').getAttribute('checked', 'true')) {
                document.getElementById('rfvDeductibleType').setAttribute("enabled", true);
                document.getElementById('spnDEDUCTTYPE').style.display = "inline";
            }
            else {
                document.getElementById('rfvDeductibleType').setAttribute("enabled", false);
                document.getElementById('rfvDeductibleType').style.display = "none";
                document.getElementById('spnDEDUCTTYPE').style.display = "none";
            }
            if (document.getElementById('chkISADDDEDUCTIBLE_APP').getAttribute('checked', 'true')) {
                document.getElementById('rfvADDDEDUCTIBLE_TYPE').setAttribute("enabled", true);
                document.getElementById('spnADDDEDUCTTYPE').style.display = "inline";
            }
            else {
                document.getElementById('rfvADDDEDUCTIBLE_TYPE').setAttribute("enabled", false);
                document.getElementById('spnADDDEDUCTTYPE').style.display = "none";
                document.getElementById('rfvADDDEDUCTIBLE_TYPE').style.display = "none";
            }
            DisableValidatorForDisabled();
            //ApplyColor();
            //ChangeColor();
        }

        function EnableDisableReinsurance() {
            //alert(document.getElementById('cmbTYPE').options[document.getElementById('cmbTYPE').selectedIndex].value);
            var SelectedValue;
            SelectedValue = document.getElementById('cmbREINSURANCE').options[document.getElementById('cmbREINSURANCE').selectedIndex].value;
            if (SelectedValue == 10963) {
                document.getElementById('hrReinsurance').style.display = "inline";
                document.getElementById('trReinsuranceCov').style.display = "inline";
                //                document.getElementById('trReinsuranceCalc').style.display = "inline";
                if (document.getElementById('cmbLOB_ID').value != "") {
                    if (document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value == "2") {
                        //if(document.getElementById('cmbTYPE').options[document.getElementById('cmbTYPE').selectedIndex].value==3)
                        //{
                        document.getElementById('hrReinsuranceCommercial').style.display = "inline";
                        document.getElementById('trCommVehicle').style.display = "inline";
                        //}
                        //else
                        //{
                        //document.getElementById('cmbCOMM_VEHICLE').options[document.getElementById('cmbCOMM_VEHICLE').selectedIndex].value = "";
                        //	document.getElementById('cmbCOMM_VEHICLE').selectedIndex =0;
                        //	document.getElementById('hrReinsuranceCommercial').style.display="none";
                        //	document.getElementById('trCommVehicle').style.display="none";
                        //}
                    }
                    else {
                        //document.getElementById('cmbCOMM_VEHICLE').options[document.getElementById('cmbCOMM_VEHICLE').selectedIndex].value = "";
                        document.getElementById('cmbCOMM_VEHICLE').selectedIndex = 0;
                        document.getElementById('hrReinsuranceCommercial').style.display = "none";
                        document.getElementById('trCommVehicle').style.display = "none";
                    }
                }
            }
            else {
                document.getElementById('hrReinsurance').style.display = "none";
                document.getElementById('trReinsuranceCov').style.display = "none";
                //                document.getElementById('trReinsuranceCalc').style.display = "none";
                document.getElementById('cmbCOMM_VEHICLE').selectedIndex = 0;
                document.getElementById('hrReinsuranceCommercial').style.display = "none";
                document.getElementById('trCommVehicle').style.display = "none";
                blankReinsuranceData();
            }
            //makeMandatoryNonMandatory();
            showHideReinsurance();
        }
        function OnTypeChange() {
            var SelectedValue;
            SelectedValue = document.getElementById('cmbTYPE').options[document.getElementById('cmbTYPE').selectedIndex].value;
            if (SelectedValue != "3") {
                document.getElementById('cmbPURPOSE').style.display = "inline";
                document.getElementById('capPURPOSE').style.display = "inline";
                document.getElementById('spnPURPOSE').style.display = "inline";
                EnableValidator('rfvPURPOSE', true);
                if (document.getElementById('hidMode').value == '0') {
                    document.getElementById('cmbREINSURANCE').selectedIndex = 1;
                }
            }
            else {
                document.getElementById('cmbPURPOSE').style.display = "none";
                document.getElementById('capPURPOSE').style.display = "none";
                document.getElementById('spnPURPOSE').style.display = "none";
                EnableValidator('rfvPURPOSE', false);
                if (document.getElementById('hidMode').value == '0') {
                    document.getElementById('cmbREINSURANCE').selectedIndex = 2;
                }
            }
            EnableDisableReinsurance();
            showHideReinsurance();
            //OnLobChanged();
        }

        function OnLobChanged() {
            if (document.getElementById('cmbLOB_ID').value != "") {
                if (document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value == "2") {

                    document.getElementById('hrReinsuranceCommercial').style.display = "inline";
                    document.getElementById('trCommVehicle').style.display = "inline";
                    //                    document.getElementById('trCommReinCovCat').style.display = "inline";
                    document.getElementById('trCommCalc').style.display = "inline";
                    EnableValidator('rfvCOMM_REIN_COV_CAT', true);
                    //EnableValidator('rfvREIN_ASLOB',true);
                    //                    document.getElementById('trComASLOB').style.display = "inline";
                }
                else {
                    document.getElementById('hrReinsuranceCommercial').style.display = "none";
                    document.getElementById('trCommVehicle').style.display = "none";
                    //                    document.getElementById('trCommReinCovCat').style.display = "none";
                    document.getElementById('trCommCalc').style.display = "none";
                    EnableValidator('rfvCOMM_REIN_COV_CAT', false);
                    //EnableValidator('rfvREIN_ASLOB',false);
                    //                    document.getElementById('trComASLOB').style.display = "none";
                    blankCommercialData();
                }
            }
            else {
                document.getElementById('hrReinsuranceCommercial').style.display = "none";
                document.getElementById('trCommVehicle').style.display = "none";
                //                document.getElementById('trCommReinCovCat').style.display = "none";
                document.getElementById('trCommCalc').style.display = "none";
                EnableValidator('rfvCOMM_REIN_COV_CAT', false);
                //EnableValidator('rfvREIN_ASLOB',false);
                blankCommercialData();
            }
            //makeMandatoryNonMandatory();
            showHideReinsurance();
        }

        function makeMandatoryNonMandatory() {
            if (document.getElementById('cmbCOMM_VEHICLE').options[document.getElementById('cmbCOMM_VEHICLE').selectedIndex].value == "10963") {
                document.getElementById('spnCOMM_REIN_COV_CAT').style.display = "inline";
                //document.getElementById('spnREIN_ASLOB').style.display="inline";
                EnableValidator('rfvCOMM_REIN_COV_CAT', true);
                //EnableValidator('rfvREIN_ASLOB',true);
                document.getElementById("cmbCOMM_REIN_COV_CAT").style.backgroundColor = "#FFFFD1";
                //document.getElementById("cmbREIN_ASLOB").style.backgroundColor="#FFFFD1";
            }
            else {
                document.getElementById('spnCOMM_REIN_COV_CAT').style.display = "none";
                //document.getElementById('spnREIN_ASLOB').style.display="none";
                EnableValidator('rfvCOMM_REIN_COV_CAT', false);
                //EnableValidator('rfvREIN_ASLOB',false);
                document.getElementById("cmbCOMM_REIN_COV_CAT").style.backgroundColor = "white";
                //document.getElementById("cmbREIN_ASLOB").style.backgroundColor="white";
            }


        }

        function showHideReinsurance() {
            if (document.getElementById('cmbCOMM_VEHICLE').options[document.getElementById('cmbCOMM_VEHICLE').selectedIndex].value == '10963') {
                if (document.getElementById('cmbLOB_ID').value != "") {
                    if (document.getElementById('cmbLOB_ID').options[document.getElementById('cmbLOB_ID').selectedIndex].value == "2") {
                        //                        document.getElementById('trCommReinCovCat').style.display = "inline";
                        document.getElementById('trCommCalc').style.display = "inline";
                        EnableValidator('rfvCOMM_REIN_COV_CAT', true);
                        //EnableValidator('rfvREIN_ASLOB',true);
                        makeMandatoryNonMandatory();
                    }
                    else {
                        //                        document.getElementById('trCommReinCovCat').style.display = "none";
                        document.getElementById('trCommCalc').style.display = "none";
                        EnableValidator('rfvCOMM_REIN_COV_CAT', false);
                        //EnableValidator('rfvREIN_ASLOB',false);
                        blankCommercialData();
                    }

                }
            }
            else {

                //                document.getElementById('trCommReinCovCat').style.display = "none";
                document.getElementById('trCommCalc').style.display = "none";
                EnableValidator('rfvCOMM_REIN_COV_CAT', false);
                //EnableValidator('rfvREIN_ASLOB',false);
                blankCommercialData();
            }

        }
        function blankReinsuranceData() {
            document.getElementById('cmbReinsuranceCov').selectedIndex = 0;
            //document.getElementById('cmbASLOB').selectedIndex =0;
            document.getElementById('cmbReinsuranceCalc').selectedIndex = 0;
            document.getElementById('cmbREIN_REPORT_BUCK').selectedIndex = 0;
        }
        function blankCommercialData() {
            document.getElementById('cmbCOMM_REIN_COV_CAT').selectedIndex = 0;
            //document.getElementById('cmbREIN_ASLOB').selectedIndex =0;
            document.getElementById('cmbCOMM_CALC').selectedIndex = 0;
            document.getElementById('cmbREIN_REPORT_BUCK_COMM').selectedIndex = 0;
        }

        function MandatoryDate() {

            if (document.getElementById('cmbIS_MANDATORY').options[document.getElementById('cmbIS_MANDATORY').selectedIndex].value == '1') {
                document.getElementById('rfvMANDATORY_DATE').setAttribute('enabled', true);
                document.getElementById('spnMANDATORY_DATE').style.display = 'inline';
                document.getElementById('spnNON_MANDATORY_DATE').style.display = 'none';
                document.getElementById('rfvNON_MANDATORY_DATE').setAttribute('enabled', false);
                document.getElementById('rfvNON_MANDATORY_DATE').style.display = 'none';
            }
            else if (document.getElementById('cmbIS_MANDATORY').options[document.getElementById('cmbIS_MANDATORY').selectedIndex].value == '0') {
                document.getElementById('rfvNON_MANDATORY_DATE').setAttribute('enabled', true);
                document.getElementById('spnNON_MANDATORY_DATE').style.display = 'inline';
                document.getElementById('spnMANDATORY_DATE').style.display = 'none';
                document.getElementById('rfvMANDATORY_DATE').setAttribute('enabled', false);
                document.getElementById('rfvMANDATORY_DATE').style.display = 'none';
            }
            else { //Modified by Ruchika Chauhan for TFS # 846 on 27-Dec-2011
                document.getElementById('rfvMANDATORY_DATE').setAttribute('enabled', false);
                document.getElementById('rfvMANDATORY_DATE').style.display = 'none';
                document.getElementById('rfvNON_MANDATORY_DATE').setAttribute('enabled', false);
                document.getElementById('rfvNON_MANDATORY_DATE').style.display = 'none';
                document.getElementById('spnMANDATORY_DATE').style.display = 'none';
                document.getElementById('spnNON_MANDATORY_DATE').style.display = 'none';
            }
        }

        function DefaultDate() {
            if (document.getElementById('cmbIS_DEFAULT').options[document.getElementById('cmbIS_DEFAULT').selectedIndex].value == '1') {
                document.getElementById('rfvDEFAULT_DATE').setAttribute('enabled', true);
                document.getElementById('spnDEFAULT_DATE').style.display = 'inline';
                document.getElementById('spnNON_DEFAULT_DATE').style.display = 'none';
                document.getElementById('rfvNON_DEFAULT_DATE').setAttribute('enabled', false);
                document.getElementById('rfvNON_DEFAULT_DATE').style.display = 'none';
            }
            else if (document.getElementById('cmbIS_DEFAULT').options[document.getElementById('cmbIS_DEFAULT').selectedIndex].value == '0') {
                document.getElementById('rfvNON_DEFAULT_DATE').setAttribute('enabled', true);
                document.getElementById('spnNON_DEFAULT_DATE').style.display = 'inline';
                document.getElementById('spnDEFAULT_DATE').style.display = 'none';
                document.getElementById('rfvDEFAULT_DATE').setAttribute('enabled', false);
                document.getElementById('rfvDEFAULT_DATE').style.display = 'none';
            }
            else { //Modified by Ruchika Chauhan for TFS # 846 on 27-Dec-2011
                document.getElementById('rfvDEFAULT_DATE').setAttribute('enabled', false);
                document.getElementById('rfvDEFAULT_DATE').style.display = 'none';
                document.getElementById('rfvNON_DEFAULT_DATE').setAttribute('enabled', false);
                document.getElementById('rfvNON_DEFAULT_DATE').style.display = 'none';
                document.getElementById('spnDEFAULT_DATE').style.display = 'none';
                document.getElementById('spnNON_DEFAULT_DATE').style.display = 'none';
            }
        }


        function setValues() {

            document.getElementById('hidLOB_Id').value = document.getElementById('cmbLOB_ID').value;

            if (document.getElementById('cmbLOB_ID').value == "") {
                document.getElementById('cmbSUB_LOB_ID').innerHTML = "";
                //document.getElementById('cmbINSTALL_PLAN_ID').innerHTML = "";
                //document.getElementById('cmbAPP_TERMS').innerHTML = "";//Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
                //document.getElementById('cmbBILL_TYPE').innerHTML = "";
            }
            else {
                GetValues(document.getElementById('cmbLOB_ID').value);
                //GetCSRProducer(document.getElementById('cmbAGENCY_ID').value, document.getElementById('cmbAPP_LOB').value)
            }

            //document.getElementById('txtAPP_EFFECTIVE_DATE').value = "";
            //document.getElementById('txtAPP_EXPIRATION_DATE').value = "";    
        }

        function GetValues(iLOB_ID) {

            if (iLOB_ID != "" && iLOB_ID != "0") {
                var result = AddCoverageDetails.AjaxFetchInfo(iLOB_ID);
                //alert(result.value)
                fillDTCombo(result.value, document.getElementById('cmbSUB_LOB_ID'), 'SUB_LOB_ID', 'SUB_LOB_DESC', 0);
            }

        }
        function fillDTCombo(objDT, combo, valID, txtDesc, tabIndex) {
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
        function SetSUB_LOB_ID() {
            if (document.getElementById('cmbSUB_LOB_ID') != "") {
                document.getElementById('hidSUB_LOB_ID').value = document.getElementById('cmbSUB_LOB_ID').value;
                //alert(document.getElementById('hidSUB_LOB_ID').value)
            }

        }

        function cUpper(cObj) {
            cObj.value = cObj.value.toUpperCase();
        }

        function allnumeric(objSource, objArgs) {//debugger
            var numbers = /\d{1,2}\/\d\d?\/\d{4}/;
            var inputxt = document.getElementById('txtMANDATORY_DATE').value;
            if (document.getElementById(objSource.controltovalidate).value.match(numbers)) {
                document.getElementById('revMANDATORY_DATE').setAttribute('enabled', true);

                objArgs.IsValid = true;
            }
            else {
                document.getElementById('revMANDATORY_DATE').setAttribute('enabled', false);
                document.getElementById('revMANDATORY_DATE').style.display = 'none';
                document.getElementById('csvMANDATORY_DATE').setAttribute('enabled', true);
                document.getElementById('csvMANDATORY_DATE').style.display = 'inline';
                objArgs.IsValid = false;
            }
        }
        function allnumeric1(objSource, objArgs) {//debugger
            var numbers = /\d{1,2}\/\d\d?\/\d{4}/;
            var inputxt = document.getElementById('txtNON_MANDATORY_DATE').value;
            if (document.getElementById(objSource.controltovalidate).value.match(numbers)) {
                document.getElementById('revNON_MANDATORY_DATE').setAttribute('enabled', true);

                objArgs.IsValid = true;
            }
            else {
                document.getElementById('revNON_MANDATORY_DATE').setAttribute('enabled', false);
                document.getElementById('revNON_MANDATORY_DATE').style.display = 'none';
                document.getElementById('csvNON_MANDATORY_DATE').setAttribute('enabled', true);
                document.getElementById('csvNON_MANDATORY_DATE').style.display = 'inline';
                objArgs.IsValid = false;
            }
        }

        function allnumeric2(objSource, objArgs) {//debugger
            var numbers = /\d{1,2}\/\d\d?\/\d{4}/;
            var inputxt = document.getElementById('txtNON_DEFAULT_DATE').value;
            if (document.getElementById(objSource.controltovalidate).value.match(numbers)) {
                document.getElementById('revNON_DEFAULT_DATE').setAttribute('enabled', true);

                objArgs.IsValid = true;
            }
            else {
                document.getElementById('revNON_DEFAULT_DATE').setAttribute('enabled', false);
                document.getElementById('revNON_DEFAULT_DATE').style.display = 'none';
                document.getElementById('csvNON_DEFAULT_DATE').setAttribute('enabled', true);
                document.getElementById('csvNON_DEFAULT_DATE').style.display = 'inline';
                objArgs.IsValid = false;
            }
        }


        function allnumeric3(objSource, objArgs) {//debugger
            var numbers = /\d{1,2}\/\d\d?\/\d{4}/;
            var inputxt = document.getElementById('txtDEFAULT_DATE').value;
            if (document.getElementById(objSource.controltovalidate).value.match(numbers)) {
                document.getElementById('revDEFAULT_DATE').setAttribute('enabled', true);

                objArgs.IsValid = true;
            }
            else {
                document.getElementById('revDEFAULT_DATE').setAttribute('enabled', false);
                document.getElementById('revDEFAULT_DATE').style.display = 'none';
                document.getElementById('csvDEFAULT_DATE').setAttribute('enabled', true);
                document.getElementById('csvDEFAULT_DATE').style.display = 'inline';
                objArgs.IsValid = false;
            }
        }


        function allnumeric6(objSource, objArgs) {//debugger
            var numbers = /\d{1,2}\/\d\d?\/\d{4}/;
            var inputxt = document.getElementById('txtDISABLED_DATE').value;
            if (document.getElementById(objSource.controltovalidate).value.match(numbers)) {
                document.getElementById('revDISABLED_DATE').setAttribute('enabled', true);
                document.getElementById('csvDISABLED_DATE').setAttribute('enabled', true);

                objArgs.IsValid = true;
            }
            else {
                document.getElementById('revDISABLED_DATE').setAttribute('enabled', false);
                document.getElementById('revDISABLED_DATE').style.display = 'none';
                document.getElementById('csvDISABLED_DATE').setAttribute('enabled', false);
                document.getElementById('csvDISABLED_DATE').style.display = 'none';
                document.getElementById('csvDISABLED_DATE1').setAttribute('enabled', true);
                document.getElementById('csvDISABLED_DATE1').style.display = 'inline';
                objArgs.IsValid = false;
            }
        }  
 
 


          
    </script>
</head>
<body oncontextmenu="return TRUE;" leftmargin="0" topmargin="0" onload="Initialise();ApplyColor();ChangeColor();">
    <div id="bodyHeight" class="pageContent" style='width: 100%; overflow-x: auto;'>
        <form id="MNT_COVERAGE" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td>
                        <table width="100%" align="center" border="0">
                            <tbody>
                                <tr>
                                    <td class="pageHeader" bgcolor="#ffffcc" colspan="3">
                                        <asp:Label ID="capheader" runat="server"></asp:Label>
                                        <%--Please note that all fields marked with * are mandatory--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="midcolorc" align="right" colspan="3">
                                        <asp:Label ID="lblMsg" runat="server" CssClass="errmsg"></asp:Label><br>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="midcolorc" align="right" colspan="3">
                                        <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capTYPE" runat="server" EnableViewState="True">Type</asp:Label><span
                                            class="mandatory">*</span> </br>
                                        <asp:DropDownList ID="cmbTYPE" runat="server" BackColor="White" onChange="OnTypeChange();">
                                            <asp:ListItem></asp:ListItem>
                                            <%--<asp:ListItem Value="1" Selected="True">Coverage</asp:ListItem>
                                        <asp:ListItem Value="2">Endorsement Coverage</asp:ListItem>
                                        <asp:ListItem Value="3">Reinsurance Coverage</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <br>
                                            <asp:RequiredFieldValidator ID="rfvTYPE" runat="server" ControlToValidate="cmbTYPE"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                            <td class="midcolora" width="33%">
                                                <asp:Label ID="capPURPOSE" runat="server">Coverage Purpose</asp:Label><span class="mandatory"
                                                    id="spnPURPOSE">*</span>
                                        </br>
                                        <asp:DropDownList ID="cmbPURPOSE" runat="server">
                                            <asp:ListItem></asp:ListItem>
                                            <%--  <asp:ListItem Value="1">New Business Request</asp:ListItem>
                                        <asp:ListItem Value="2">Renewal Request</asp:ListItem>
                                        <asp:ListItem Value="3" Selected="True">Both</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <br>
                                        <asp:RequiredFieldValidator ID="rfvPURPOSE" runat="server" ControlToValidate="cmbPURPOSE"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capSTATE_ID" runat="server">State</asp:Label><span class="mandatory"
                                            id="spnSTATE_ID" runat="server">*</span></br>
                                        <asp:DropDownList ID="cmbSTATE_ID" EnableViewState="True" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="cmbSTATE_ID_SelectedIndexChanged" OnChange="PreventTab();">
                                        </asp:DropDownList>
                                        <br>
                                        <asp:RequiredFieldValidator ID="rfvSTATE_ID" runat="server" ControlToValidate="cmbSTATE_ID"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capLOB_ID" runat="server">Product</asp:Label><span class="mandatory">*</span></br>
                                        <asp:DropDownList ID="cmbLOB_ID" onChange="HideUnhideAddDeductible(),setValues();"
                                            EnableViewState="True" runat="server" onfocus="SelectComboIndex('cmbLOB_ID')">
                                        </asp:DropDownList>
                                        <br>
                                        <asp:RequiredFieldValidator ID="rfvLOB_ID" runat="server" ControlToValidate="cmbLOB_ID"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="midcolora" width="33%">
                                        <%--<asp:Label ID="capCOV_REF_CODE" runat="server">Reference</asp:Label></br>
                                    <asp:TextBox ID="txtCOV_REF_CODE" runat="server" size="7" MaxLength="5"></asp:TextBox>--%>
                                        <asp:Label ID="capSUB_LOB_ID" runat="server">SUBLOB_ID</asp:Label></br>
                                        <asp:DropDownList ID="cmbSUB_LOB_ID" runat="server" onfocus="SelectComboIndex('cmbSUB_LOB_ID')"
                                            onchange="SetSUB_LOB_ID()">
                                        </asp:DropDownList>
                                        </br>
                                        <asp:RequiredFieldValidator ID="rfvSUB_LOB_ID" runat="server" ControlToValidate="cmbSUB_LOB_ID"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capCOV_CODE" runat="server">Coverage Code</asp:Label><span class="mandatory">*</span></br>
                                        <asp:TextBox ID="txtCOV_CODE" runat="server" size="7" MaxLength="9"></asp:TextBox><br>
                                        <asp:RequiredFieldValidator ID="rfvCOV_CODE" runat="server" ControlToValidate="txtCOV_CODE"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capCOV_DES" runat="server">Description</asp:Label><span class="mandatory">*</span>
                                        </br><asp:TextBox ID="txtCOV_DES" runat="server" size="50" MaxLength="250"></asp:TextBox>
                                        <a class="calcolora" href="#">
                                            <img id="imgSelect" style="cursor: hand" alt="" src="../../cmsweb/images/selecticon.gif"
                                                border="0" autopostback="false" runat="server">
                                        </a></br>
                                        <asp:RequiredFieldValidator ID="rfvCOV_DES" runat="server" ControlToValidate="txtCOV_DES"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <%--<td class="midcolora" width="33%">
                                    <asp:Label ID="capDefault" runat="server">Default</asp:Label>
                                    </br><asp:DropDownList ID="cmbIS_DEFAULT" runat="server">
                                    </asp:DropDownList>
                                </td>--%>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capLimit_Applicable" runat="server">Are Limits Applicable?</asp:Label>
                                        </br><asp:CheckBox ID="chkIsLimitApplicable" EnableViewState="True" runat="server"
                                            Text="" TextAlign="Left"></asp:CheckBox>
                                    </td>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capLIMITTYPE" runat="server">Limit Type</asp:Label><span class="mandatory"
                                            id="spnLIMITTYPE">*</span> </br><asp:Label ID="lblLIMITTYPE" runat="server"></asp:Label><asp:DropDownList
                                                ID="cmbLIMIT_TYPE" runat="server">
                                                <asp:ListItem></asp:ListItem>
                                                <%-- <asp:ListItem Value="1" Selected="True">Flat</asp:ListItem>
                                            <asp:ListItem Value="2">Split</asp:ListItem>
                                            <asp:ListItem Value="3">Open</asp:ListItem>
                                            <asp:ListItem Value="4">ReadOnly</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        <br>
                                        <asp:RequiredFieldValidator ID="rfvLIMITTYPE" runat="server" ControlToValidate="cmbLIMIT_TYPE"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <%--   <td class="midcolora" width="33%">
                                    <asp:Label ID="capLIMITTYPE" runat="server">Limit Type</asp:Label><span class="mandatory"
                                        id="spnLIMITTYPE">*</span> </br><asp:Label ID="lblLIMITTYPE" runat="server"></asp:Label><asp:DropDownList
                                            ID="cmbLIMIT_TYPE" runat="server">
                                            <asp:ListItem></asp:ListItem>
                                           <%-- <asp:ListItem Value="1" Selected="True">Flat</asp:ListItem>
                                            <asp:ListItem Value="2">Split</asp:ListItem>
                                            <asp:ListItem Value="3">Open</asp:ListItem>
                                            <asp:ListItem Value="4">ReadOnly</asp:ListItem>
                                        </asp:DropDownList>
                                    <br>
                                    <asp:RequiredFieldValidator ID="rfvLIMITTYPE" runat="server" ControlToValidate="cmbLIMIT_TYPE"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>--%>
                                    <%--<td class="midcolora" width="33%">
                                    <asp:Label ID="capDefault" runat="server">Default</asp:Label>
                                    </br><asp:DropDownList ID="cmbIS_DEFAULT" runat="server">
                                    </asp:DropDownList>
                                </td>--%>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capFORM_NUMBER" runat="server"></asp:Label>
                                        </br><asp:TextBox ID="txtFORM_NUMBER" runat="server" size="20" MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capDeduct_Applicable" runat="server">Are Deductibles Applicable?</asp:Label>
                                        </br><asp:CheckBox ID="chkIsDeductApplicable" onclick="CheckDeductible();" EnableViewState="True"
                                            runat="server" Text=" " TextAlign="Left"></asp:CheckBox>
                                    </td>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capDeductibleType" runat="server">Deductible Type</asp:Label><span
                                            class="mandatory" id="spnDEDUCTTYPE">*</span> </br><asp:Label ID="lblDeductibleType"
                                                runat="server"></asp:Label><asp:DropDownList ID="cmbDEDUCTIBLE_TYPE" runat="server">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="1" Selected="True">Flat</asp:ListItem>
                                                    <asp:ListItem Value="2">Split</asp:ListItem>
                                                    <asp:ListItem Value="3">Open</asp:ListItem>
                                                    <asp:ListItem Value="4">ReadOnly</asp:ListItem>
                                                </asp:DropDownList>
                                        <br>
                                        <asp:RequiredFieldValidator ID="rfvDeductibleType" runat="server" ControlToValidate="cmbDEDUCTIBLE_TYPE"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr id="trADDAPPLICABLE" runat="server">
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capAddDeduct_Applicable" runat="server">Are Deductibles Applicable?</asp:Label>
                                        <br />
                                        <asp:CheckBox ID="chkISADDDEDUCTIBLE_APP" onclick="AddDeductCheck();" EnableViewState="True"
                                            runat="server" Text=" " TextAlign="Left" />
                                    </td>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capAddDeductibleType" runat="server">Deductible Type</asp:Label><span
                                            class="mandatory" id="spnADDDEDUCTTYPE">*</span>
                                        <br />
                                        <asp:Label ID="lblAddDeductibleType" runat="server"></asp:Label><asp:DropDownList
                                            ID="cmbADDDEDUCTIBLE_TYPE" runat="server">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">Flat</asp:ListItem>
                                            <asp:ListItem Value="2">Split</asp:ListItem>
                                            <asp:ListItem Value="3">Open</asp:ListItem>
                                            <asp:ListItem Value="4">ReadOnly</asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvADDDEDUCTIBLE_TYPE" runat="server" ControlToValidate="cmbADDDEDUCTIBLE_TYPE"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="midcolora" width="33%">
                                    </td>
                                    <%-- <td class="midcolora" width="33%">
                                    <asp:Label ID="capRANK" runat="server">Rank</asp:Label>span class="mandatory">*</span 
                                    </br><asp:TextBox ID="txtRANK" runat="server" size="3" MaxLength="2"></asp:TextBox><br>
                                    <asp:RegularExpressionValidator ID="revRANK" runat="server" ControlToValidate="txtRANK"
                                        Display="Dynamic"></asp:RegularExpressionValidator>
                              </td>--%>
                                </tr>
                                <tr>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capCOVERAGE_TYPE" runat="server">Coverage Type</asp:Label><span class="mandatory">*</span>
                                        <br />
                                        <asp:DropDownList ID="cmbCOVERAGE_TYPE" runat="server" onchange="Check();">
                                        </asp:DropDownList>
                                        <br>
                                        <asp:RequiredFieldValidator ID="rfvCOVERAGE_TYPE" runat="server" ControlToValidate="cmbCOVERAGE_TYPE"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capINCLUDED" runat="server">Included</asp:Label><span class="mandatory"
                                            id="spnMandatory">*</span>
                                        <br />
                                        <asp:TextBox ID="txtINCLUDED" runat="server" size="6" MaxLength="5"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="rfvINCLUDED" runat="server" ControlToValidate="txtINCLUDED"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revINCLUDED" runat="server" ControlToValidate="txtINCLUDED"
                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capRANK" runat="server">Rank</asp:Label>
                                        <br />
                                        <asp:TextBox ID="txtRANK" runat="server" size="3" MaxLength="2"></asp:TextBox><br />
                                        <asp:RegularExpressionValidator ID="revRANK" runat="server" ControlToValidate="txtRANK"
                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                        <%--<asp:requiredfieldvalidator id="rfvRANK" runat="server" ControlToValidate="txtRANK" Display="Dynamic"</asp:requiredfieldvalidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capCOV_TYPE_ABBR" runat="server">Coverage type abbreviation</asp:Label></BR>
                                        <asp:TextBox ID="txtCOV_TYPE_ABBR" runat="server" size="10" MaxLength="10" onblur="return cUpper(this);"></asp:TextBox>
                                    </td>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capSUSEP_COV_CODE" runat="server">SUSEP Coverage Code</asp:Label></BR>
                                        <asp:TextBox ID="txtSUSEP_COV_CODE" runat="server" size="10" MaxLength="4"></asp:TextBox></br>
                                        <asp:RegularExpressionValidator ID="revSUSEP_COV_CODE" runat="server" Display="Dynamic"
                                            ControlToValidate="txtSUSEP_COV_CODE" ErrorMessage=""></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="midcolora" width="33%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capIS_MANDATORY" runat="server">Is Mandatory</asp:Label><span class="mandatory">*</span>
                                        </br><asp:DropDownList ID="cmbIS_MANDATORY" runat="server" onchange="MandatoryDate();">
                                        </asp:DropDownList>
                                        <br>
                                        <asp:RequiredFieldValidator ID="rfvIS_MANDATORY" runat="server" ControlToValidate="cmbIS_MANDATORY"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capMANDATORY_DATE" runat="server">Mandatory Date</asp:Label>
                                        <span class="mandatory" id='spnMANDATORY_DATE' runat="server">*</span>
                                        <br />
                                        <asp:TextBox ID="txtMANDATORY_DATE" runat="server" MaxLength="10" SIZE="15"></asp:TextBox><asp:HyperLink
                                            ID="hlkMANDATORY_DATE" runat="server" CssClass="HotSpot">
                                            <asp:Image runat="server" ID="imgMANDATORY_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif">
                                            </asp:Image>
                                        </asp:HyperLink></br>
                                        <asp:RegularExpressionValidator ID="revMANDATORY_DATE" runat="server" Display="Dynamic"
                                            ControlToValidate="txtMANDATORY_DATE"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="rfvMANDATORY_DATE" runat="server" ControlToValidate="txtMANDATORY_DATE"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="csvMANDATORY_DATE" runat="server" ControlToValidate="txtMANDATORY_DATE"
                                            Display="Dynamic" ClientValidationFunction="allnumeric"></asp:CustomValidator>
                                    </td>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capNON_MANDATORY_DATE" runat="server">Non Mandatory Date
                                        </asp:Label><span class="mandatory" id='spnNON_MANDATORY_DATE' runat="server">*</span>
                                        <br />
                                        <asp:TextBox ID="txtNON_MANDATORY_DATE" runat="server" MaxLength="10" SIZE="15"></asp:TextBox><asp:HyperLink
                                            ID="hlkNON_MANDATORY_DATE" runat="server" CssClass="HotSpot">
                                            <asp:Image runat="server" ID="imgNON_MANDATORY_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif">
                                            </asp:Image>
                                        </asp:HyperLink><br />
                                        <asp:RegularExpressionValidator ID="revNON_MANDATORY_DATE" runat="server" Display="Dynamic"
                                            ControlToValidate="txtNON_MANDATORY_DATE"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="rfvNON_MANDATORY_DATE" runat="server" ControlToValidate="txtNON_MANDATORY_DATE"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="csvNON_MANDATORY_DATE" runat="server" ControlToValidate="txtNON_MANDATORY_DATE"
                                            Display="Dynamic" ClientValidationFunction="allnumeric1"></asp:CustomValidator>
                                    </td>
                                    <%--asp:requiredfieldvalidator id="rfvRANK" runat="server" ControlToValidate="txtRANK" Display="Dynamic"</asp:requiredfieldvalidator>--%>
                                </tr>
                                <td class="midcolora" width="33%">
                                    <asp:Label ID="capDefault" runat="server">Default</asp:Label><span class="mandatory"
                                        id="spnIS_DEFAULT" runat="server">*</span> </br><asp:DropDownList ID="cmbIS_DEFAULT"
                                            runat="server" onchange="DefaultDate();">
                                        </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvIS_DEFAULT" runat="server" ControlToValidate="cmbIS_DEFAULT"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td class="midcolora" width="33%">
                                    <asp:Label ID="capDEFAULT_DATE" runat="server"></asp:Label>
                                    <span class="mandatory" id='spnDEFAULT_DATE' runat="server">*</span>
                                    <br />
                                    <asp:TextBox ID="txtDEFAULT_DATE" runat="server" MaxLength="10" SIZE="15"></asp:TextBox><asp:HyperLink
                                        ID="hlkDEFAULT_DATE" runat="server" CssClass="HotSpot">
                                        <asp:Image runat="server" ID="imgDEFAULT_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif">
                                        </asp:Image>
                                    </asp:HyperLink><br />
                                    <asp:RegularExpressionValidator ID="revDEFAULT_DATE" runat="server" Display="Dynamic"
                                        ControlToValidate="txtDEFAULT_DATE"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="rfvDEFAULT_DATE" runat="server" ControlToValidate="txtDEFAULT_DATE"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="csvDEFAULT_DATE" runat="server" ControlToValidate="txtDEFAULT_DATE"
                                        Display="Dynamic" ClientValidationFunction="allnumeric3"></asp:CustomValidator>
                                </td>
                                <td class="midcolora" width="33%">
                                    <asp:Label ID="capNON_DEFAULT_DATE" runat="server"></asp:Label>
                                    <span class="mandatory" id='spnNON_DEFAULT_DATE' runat="server">*</span>
                                    <br />
                                    <asp:TextBox ID="txtNON_DEFAULT_DATE" runat="server" MaxLength="10" SIZE="15"></asp:TextBox><asp:HyperLink
                                        ID="hlkNON_DEFAULT_DATE" runat="server" CssClass="HotSpot">
                                        <asp:Image runat="server" ID="imgNON_DEFAULT_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif">
                                        </asp:Image>
                                    </asp:HyperLink>
                                    <br />
                                    <asp:RegularExpressionValidator ID="revNON_DEFAULT_DATE" runat="server" Display="Dynamic"
                                        ControlToValidate="txtNON_DEFAULT_DATE"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="rfvNON_DEFAULT_DATE" runat="server" ControlToValidate="txtNON_DEFAULT_DATE"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="csvNON_DEFAULT_DATE" runat="server" ControlToValidate="txtNON_DEFAULT_DATE"
                                        Display="Dynamic" ClientValidationFunction="allnumeric2"></asp:CustomValidator>
                                    <%--  </td>
                                </tbody>
                                </table>--%>
                                </td>
                </tr>
                <%-- <table><tbody>--%>
                <tr>
                    <%--    <td class="midcolora" width="33%">
                                    <asp:Label ID="capCOVERAGE_TYPE" runat="server">Coverage Type</asp:Label><span class="mandatory">*</span>
                                    <br />
                                    <asp:DropDownList ID="cmbCOVERAGE_TYPE" runat="server" onchange="Check();">
                                    </asp:DropDownList>
                                    <br>
                                    <asp:RequiredFieldValidator ID="rfvCOVERAGE_TYPE" runat="server" ControlToValidate="cmbCOVERAGE_TYPE"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>--%>
                    <td class="midcolora" width="33%">
                        <asp:Label ID="capEFFECTIVE_FROM_DATE" runat="server"></asp:Label><span class="mandatory">*</span>
                        <br />
                        <asp:TextBox ID="txtEFFECTIVE_FROM_DATE" runat="server" MaxLength="10" SIZE="15"></asp:TextBox><asp:HyperLink
                            ID="hlkEFFECTIVE_FROM_DATE" runat="server" CssClass="HotSpot">
                            <asp:Image runat="server" ID="imgEFFECTIVE_FROM_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif">
                            </asp:Image>
                        </asp:HyperLink><br>
                        <asp:RequiredFieldValidator ID="rfvEFFECTIVE_FROM_DATE" runat="server" ControlToValidate="txtEFFECTIVE_FROM_DATE"
                            Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revEFFECTIVE_FROM_DATE"
                                runat="server" ControlToValidate="txtEFFECTIVE_FROM_DATE" Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                    <td class="midcolora" width="33%">
                        <asp:Label ID="capEFFECTIVE_TO_DATE" runat="server"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtEFFECTIVE_TO_DATE" runat="server" MaxLength="10" SIZE="15"></asp:TextBox><asp:HyperLink
                            ID="hlkEFFECTIVE_TO_DATE" runat="server" CssClass="HotSpot">
                            <asp:Image runat="server" ID="imgEFFECTIVE_TO_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif">
                            </asp:Image>
                        </asp:HyperLink><br>
                        <asp:RegularExpressionValidator ID="revEFFECTIVE_TO_DATE" runat="server" ControlToValidate="txtEFFECTIVE_TO_DATE"
                            Display="Dynamic"></asp:RegularExpressionValidator><asp:CustomValidator ID="csvEFFECTIVE_TO_DATE"
                                runat="server" ControlToValidate="txtEFFECTIVE_TO_DATE" Display="Dynamic" ClientValidationFunction="CheckEndDate"></asp:CustomValidator>
                    </td>
                    <td class="midcolora" width="33%">
                        <asp:Label ID="capDISABLED_DATE" runat="server"></asp:Label>
                        </br><asp:TextBox ID="txtDISABLED_DATE" runat="server" MaxLength="10" SIZE="15"></asp:TextBox><asp:HyperLink
                            ID="hlkDISABLED_DATE" runat="server" CssClass="HotSpot">
                            <asp:Image runat="server" ID="imgDISABLED_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif">
                            </asp:Image>
                        </asp:HyperLink><br>
                        <asp:RegularExpressionValidator ID="revDISABLED_DATE" runat="server" ControlToValidate="txtDISABLED_DATE"
                            Display="Dynamic"></asp:RegularExpressionValidator><asp:CustomValidator ID="csvDISABLED_DATE"
                                runat="server" ControlToValidate="txtDISABLED_DATE" Display="Dynamic" ClientValidationFunction="CheckDisabledDate"></asp:CustomValidator>
                        <asp:CustomValidator ID="csvDISABLED_DATE1" runat="server" ControlToValidate="txtDISABLED_DATE"
                            Display="Dynamic" ClientValidationFunction="allnumeric6"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td class="midcolora" width="33%">
                        <asp:Label ID="capCOV_REF_CODE" runat="server">Reference</asp:Label></br>
                        <asp:TextBox ID="txtCOV_REF_CODE" runat="server" size="7" MaxLength="5"></asp:TextBox>
                    </td>
                    <td class="midcolora" width="33%">
                    </td>
                    <td class="midcolora" width="33%">
                    </td>
                </tr>
                <%--   <tr>--%>
                <%--<td class="midcolora" width="33%">
                                    <asp:Label ID="capEFFECTIVE_TO_DATE" runat="server"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtEFFECTIVE_TO_DATE" runat="server" MaxLength="10" SIZE="15"></asp:TextBox><asp:HyperLink
                                        ID="hlkEFFECTIVE_TO_DATE" runat="server" CssClass="HotSpot">
                                        <asp:Image runat="server" ID="imgEFFECTIVE_TO_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif">
                                        </asp:Image>
                                    </asp:HyperLink><br>
                                    <asp:RegularExpressionValidator ID="revEFFECTIVE_TO_DATE" runat="server" ControlToValidate="txtEFFECTIVE_TO_DATE"
                                        Display="Dynamic"></asp:RegularExpressionValidator><asp:CustomValidator ID="csvEFFECTIVE_TO_DATE"
                                            runat="server" ControlToValidate="txtEFFECTIVE_TO_DATE" Display="Dynamic" ClientValidationFunction="CheckEndDate"></asp:CustomValidator>
                                </td>--%>
                <%--          <tr>
                                <%--<td class="midcolora" width="33%">
                                    <asp:Label ID="capDISABLED_DATE" runat="server"></asp:Label>
                                    </br><asp:TextBox ID="txtDISABLED_DATE" runat="server" MaxLength="10" SIZE="15"></asp:TextBox><asp:HyperLink
                                        ID="hlkDISABLED_DATE" runat="server" CssClass="HotSpot">
                                        <asp:Image runat="server" ID="imgDISABLED_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif">
                                        </asp:Image>
                                    </asp:HyperLink><br>
                                    <asp:RegularExpressionValidator ID="revDISABLED_DATE" runat="server" ControlToValidate="txtDISABLED_DATE"
                                        Display="Dynamic"></asp:RegularExpressionValidator><asp:CustomValidator ID="csvDISABLED_DATE"
                                            runat="server" ControlToValidate="txtDISABLED_DATE" Display="Dynamic" ClientValidationFunction="CheckDisabledDate"></asp:CustomValidator>
                                </td>--%>
                <%--<td class="midcolora" width="33%">
                                    <asp:Label ID="capFORM_NUMBER" runat="server"></asp:Label>
                                    </br><asp:TextBox ID="txtFORM_NUMBER" runat="server" size="20" MaxLength="20"></asp:TextBox>
                                </td>--%>
                <%--  <td class="midcolora" width="33%">
                                    <asp:Label ID="capDefault" runat="server">Default</asp:Label>
                                    </br><asp:DropDownList ID="cmbIS_DEFAULT" runat="server">
                                    </asp:DropDownList>
                                </td>
                                
                             
                            </tr>--%>
                <tr id="trReinsurance">
                    <td class="midcolora" width="33%">
                        <asp:Label ID="capREINSURANCE" runat="server"></asp:Label>
                        </br><asp:DropDownList ID="cmbREINSURANCE" onfocus="SelectComboIndex('cmbREINSURANCE')"
                            runat="server" onChange="EnableDisableReinsurance();">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <div>
                            <table>
                                <tr>
                                    <td class="midcolora" width="33%">
                                        <asp:Label ID="capREIN_ASLOB" runat="server"></asp:Label>
                                        </br><asp:DropDownList ID="cmbREIN_ASLOB" runat="server">
                                        </asp:DropDownList>
                                        <a class="calcolora" href="javascript:showPageLookupLayer('cmbREIN_ASLOB')">
                                            <img height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></a>
                                    </td>
                                    <%--<td class="midcolora">
                                                </td>--%>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td class="midcolora" width="33%">
                        <asp:Label ID="capASLOB" runat="server"></asp:Label>
                        <br />
                        <asp:DropDownList ID="cmbASLOB" runat="server">
                        </asp:DropDownList>
                        <a class="calcolora" href="javascript:showPageLookupLayer('cmbASLOB')">
                            <img height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></a>
                    </td>
                </tr>
                <!-- Added by Praveen Kumar on 19/08/2010 starts-->
                <tr>
                    <td class="midcolora" width="33%">
                        <asp:Label ID="capDISPLAYONCLAIM" runat="server"></asp:Label>
                        <br />
                        <asp:DropDownList ID="cmbDISPLAYONCLAIM" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="midcolora" width="33%">
                        <asp:Label ID="capCLAIMRESERVEAPPLY" runat="server"></asp:Label>
                        <br />
                        <asp:DropDownList ID="cmbCLAIMRESERVEAPPLY" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="midcolora" width="33%">
                        <asp:Label ID="capISMAIN" runat="server"></asp:Label>
                        <br />
                        <asp:CheckBox ID="chkIS_MAIN" runat="server"></asp:CheckBox>
                    </td>
                </tr>
                <!-- Added by Praveen Kumar on 19/08/2010 Ends-->
                <%--Added by Swarup on 28-Mar-2007 : Start --%>
                <tr id="hrReinsurance">
                    <td class="headerEffectSystemParams" colspan="3">
                        <asp:Label ID='CapRein' runat='server'>Reinsurance</asp:Label>
                    </td>
                </tr>
                <tr id="trReinsuranceCov">
                    <td class="midcolora" width="33%">
                        <asp:Label ID="capReinsuranceCov" runat="server"></asp:Label><br />
                        <asp:DropDownList ID="cmbReinsuranceCov" runat="server">
                        </asp:DropDownList>
                        <a class="calcolora" href="javascript:showPageLookupLayer('cmbReinsuranceCov')">
                            <img height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></a>
                    </td>
                    <td class="midcolora" width="33%">
                        <asp:Label ID="capReinsuranceCalc" runat="server"></asp:Label></br>
                        <asp:DropDownList ID="cmbReinsuranceCalc" runat="server">
                        </asp:DropDownList>
                        <a class="calcolora" href="javascript:showPageLookupLayer('cmbReinsuranceCalc')">
                            <img height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></a>
                    </td>
                    <td class="midcolora" width="33%">
                        <asp:Label ID="capRptBkt" runat="server"></asp:Label></br>
                        <asp:DropDownList ID="cmbREIN_REPORT_BUCK" runat="server">
                        </asp:DropDownList>
                        <a class="calcolora" href="javascript:showPageLookupLayer('cmbREIN_REPORT_BUCK')">
                            <img height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0" id="imgREIN_REPORT_BUCK"></a>
                    </td>
                </tr>
                <%--<tr id="trReinsuranceCalc">
                                
                            </tr>--%>
                <%--Added by Swarup on 28-Mar-2007 : End --%>
                <%--Added by Swarup on 01-Oct-2007 : Start --%>
                <tr id="hrReinsuranceCommercial">
                    <td class="headerEffectSystemParams" colspan="3">
                        <asp:Label ID="CapComm" runat="server">Reinsurance/Commercial</asp:Label>
                    </td>
                    <%--<td class="midcolora" width="33%"></td>
                                <td class="midcolora" width="33%"></td>--%>
                </tr>
                <tr id="trCommVehicle">
                    <td class="midcolora" width="33%" colspan="3">
                        <asp:Label ID="capCOMM_VEHICLE" runat="server"></asp:Label>
                        </br>
                        <asp:DropDownList ID="cmbCOMM_VEHICLE" onfocus="SelectComboIndex('cmbCOMM_VEHICLE')"
                            onChange="showHideReinsurance()" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <%--  <tr id="trCommReinCovCat">                               
                                 <td class="midcolora" width="33%"></td>
                                <td class="midcolora" width="33%"></td>                              
                            </tr>--%>
                <tr id="trCommCalc">
                    <td class="midcolora" width="33%">
                        <asp:Label ID="capCOMM_CALC" runat="server"></asp:Label></br>
                        <asp:DropDownList ID="cmbCOMM_CALC" onfocus="SelectComboIndex('cmbCOMM_CALC')" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="midcolora" width="33%">
                        <asp:Label ID="capREIN_REPORT_BUCK_COMM" runat="server"></asp:Label><br />
                        <asp:DropDownList ID="cmbREIN_REPORT_BUCK_COMM" runat="server">
                        </asp:DropDownList>
                        <a class="calcolora" href="javascript:showPageLookupLayer('cmbREIN_REPORT_BUCK_COMM')">
                            <img height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0" id="imgREIN_REPORT_BUCK_COMM"></a>
                    </td>
                    <td class="midcolora" width="33%">
                        <asp:Label ID="capCOMM_REIN_COV_CAT" runat="server"></asp:Label><span id="spnCOMM_REIN_COV_CAT"
                            class="mandatory">*</span></br>
                        <asp:DropDownList ID="cmbCOMM_REIN_COV_CAT" onfocus="SelectComboIndex('cmbCOMM_REIN_COV_CAT')"
                            runat="server">
                        </asp:DropDownList>
                        <br>
                        <asp:RequiredFieldValidator ID="rfvCOMM_REIN_COV_CAT" runat="server" ControlToValidate="cmbCOMM_REIN_COV_CAT"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <%--Added by Swarup on 01-Oct-2007 : End --%>
                <tr>
                    <td class="midcolora" colspan="1">
                        <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" Text="Reset" CausesValidation="false">
                        </cmsb:CmsButton><cmsb:CmsButton class="clsButton" ID="btnActivateDeactivate" runat="server"
                            CausesValidation="False" Text="Activate/Deactivate"></cmsb:CmsButton>
                    </td>
                    <td class="midcolora">
                    </td>
                    <td class="midcolorr" colspan="1">
                        <cmsb:CmsButton class="clsButton" ID="btnSave" runat="server" Text="Save"></cmsb:CmsButton>
                    </td>
                </tr>
                <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
                <input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
                <input id="hidOldData" type="hidden" name="hidOldData" runat="server">
                <input id="hidCOV_ID" type="hidden" value="0" name="hidCOV_ID" runat="server">
                <input id="hidENDORSEMENT_ID" type="hidden" value="0" name="hidENDORSEMENT_ID" runat="server">
                <input id="hidMode" type="hidden" value="0" name="hidMode" runat="server">
                <input id="hidLimitTypeValue" type="hidden" value="0" name="hidLimitTypeValue" runat="server">
                <input id="hidDeductTypeValue" type="hidden" value="0" name="hidDeductTypeValue"
                    runat="server">
                <input id="hid_CoverageUsed" type="hidden" value="0" name="hid_CoverageUsed" runat="server">
                <input id="hidISLIMITAPPLICABLE" type="hidden" value="0" name="hidISLIMITAPPLICABLE"
                    runat="server">
                <input id="hidISDEDUCTIBELEAPPLICABLE" type="hidden" value="0" name="hidISDEDUCTIBELEAPPLICABLE"
                    runat="server">
                <input id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
                <input id="hidSUB_LOB_ID" type="hidden" value="0" name="hidSUB_LOB" runat="server" />
                <input id="hidISADDDEDUCTIBLE_APP" type="hidden" value="0" name="hidISADDDEDUCTIBLE_APP"
                    runat="server">
                <input id="hidDrawLimit" type="hidden" value="0" name="hidDrawLimit" runat="server">
                <input id="hidDrawDeductible" type="hidden" value="0" name="hidDrawDeductible" runat="server">
                <input id="hidDrawAddDeductible" type="hidden" value="0" name="hidDrawAddDeductible"
                    runat="server">
                <input id="hidSTATE_ID" type="hidden" value="0" name="hidSTATE_ID" runat="server">
                <input id="hidSTATE_REFRESH" type="hidden" value="0" name="hidSTATE_REFRESH" runat="server">
                <input id="hidCOV_IDNew" type="hidden" value="0" name="hidCOV_IDNew" runat="server">
                <input id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server">
                <input id="hidTYPE" type="hidden" name="hidTYPE" runat="server">
                <input id="hidREINSURANCE" type="hidden" value="0" name="hidREINSURANCE" runat="server">
                <input id="HidDeductApplicable" type="hidden" value="0" name="HidDeductApplicable"
                    runat="server">
                <input id="HidAPPDEDUCT" type="hidden" value="0" name="HidAPPDEDUCT" runat="server">
                <input id="hidMess" type="hidden" value="0" name="hidMess" runat="server">
                <input id="hidtab1" type="hidden" name="hidtab1" runat="server" />
                <input id="hidtab2" type="hidden" name="hidtab2" runat="server" />
                <input id="hidtab3" type="hidden" name="hidtab3" runat="server" />
                <input id="hidtab4" type="hidden" name="hidtab4" runat="server" />
                <input id="hidtab5" type="hidden" name="hidtab5" runat="server" />
                <input id="hidtab6" type="hidden" name="hidtab6" runat="server" />
            </tbody>
        </table>
        <%-- </td>
            </tr>
        </tbody>
    </table>--%>
        </form>
        <!-- For lookup layer -->
        <div id="lookupLayerFrame" style="display: none; z-index: 101; position: absolute">
            <iframe id="lookupLayerIFrame" style="display: none; z-index: 1001; filter: alpha(opacity=0);
                background-color: #000000" width="0" height="0" left="0px" top="0px;"></iframe>
        </div>
        <div id="lookupLayer" onmouseover="javascript:refreshLookupLayer();" style="z-index: 101;
            visibility: hidden; position: absolute">
            <table class="TabRow" cellspacing="0" cellpadding="2" width="100%">
                <tr class="SubTabRow">
                    <td>
                        <b>
                            <asp:Label ID='CapAddLookup' runat='server'>Add LookUp</asp:Label></b>
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
        <script type="text/javascript" language="javascript">

            if (document.getElementById('hidCalledFrom').value == 'Add')
                parent.document.getElementById('hidCalledFrom').value = 'Add';
            document.getElementById('hidCalledFrom').value = parent.document.getElementById('hidCalledFrom').value;

            RefreshWindowsGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidCOV_ID').value, true);
            var tab3 = document.getElementById('hidtab3').value;
            if (document.getElementById('hidCOV_IDNew').value != '0' && parent.document.getElementById('hidCalledFrom').value == 'Add') {
                RemoveTab(1, top.frames[1]);
                var Url = "AddCoverageDetails.aspx?COV_ID=" + document.getElementById('hidCOV_IDNew').value + "&";
                DrawTab(1, top.frames[1], tab3, Url);
            }

            //Added by Charles on 3-Jun-10 for Itrack 19
            function refreshFromPopUp() {
                RefreshWindowsGrid(1, document.getElementById('hidCOV_ID').value);
            }
        </script>
        <%--</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>--%>
    </div>
</body>
</html>

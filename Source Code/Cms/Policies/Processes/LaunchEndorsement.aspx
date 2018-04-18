<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LaunchEndorsement.aspx.cs"
    Inherits="Cms.Policies.Processes.LaunchEndorsement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Auto Endorsement</title>
    <link href="/cms/cmsweb/css/css<%=base.GetColorScheme()%>.css" type="text/css" rel="stylesheet">

    <script src="/cms/cmsweb/scripts/xmldom.js"></script>

    <script src="/cms/cmsweb/scripts/common.js"></script>

    <script src="/cms/cmsweb/scripts/form.js"></script>

    <script src="/cms/cmsweb/scripts/AJAXcommon.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>

    <script language="javascript" type="text/javascript">
        function Init() {
            ApplyColor();
            ChangeColor();
            top.topframe.main1.mousein = false;
            showScroll();
            findMouseIn();
            document.getElementById('txtPOLICY_ID').focus();
            disableEndrfv();
            EndType();
            EndorsmentOp();
        }
        function EndorsmentOp() {
            if (document.getElementById('hidPOLICY_FLAG').value != "1") {
                document.getElementById('trEndorsmentOp').style.display = 'none';
                document.getElementById('trbtnLaunch').style.display = 'none';


            } else {
                document.getElementById('trEndorsmentOp').style.display = 'inline';
                document.getElementById('trbtnLaunch').style.display = 'inline';
            }
        }
        function DisablePolicyIDrfv(Enable, Display) {
          
            document.getElementById('rfvPOLICY_ID').setAttribute('enabled', Enable)
            document.getElementById('rfvPOLICY_ID').style.display = Display;


        }
        function disableEndrfv() {
            document.getElementById('rfvENDORSEMENT_TYPE').setAttribute('enabled', false);
            document.getElementById('rfvENDORSEMENT_TYPE').setAttribute('isValid', true);
            document.getElementById('rfvENDORSEMENT_TYPE').style.display = "none";

        }
        function EnableEndrfv() {
            
            document.getElementById('rfvENDORSEMENT_TYPE').setAttribute('enabled', true);
            document.getElementById('rfvENDORSEMENT_TYPE').setAttribute('isValid', false);
            document.getElementById('rfvENDORSEMENT_TYPE').style.display = "inline";
            //Page_ClientValidate();
        }
        function EndType() {
            if (document.getElementById('cmbENDORSEMENT_TYPE').value != '14676' && document.getElementById('cmbENDORSEMENT_TYPE').value != '') {

                document.getElementById('trReduceLimit').style.display = 'inline';
                document.getElementById('rfvCOVERAGES').setAttribute('enabled', true);
                document.getElementById('rfvCOVERAGES').setAttribute('isvalid', false);
                document.getElementById('cmbCOVERAGES').selectedIndex = 0;

                document.getElementById('rfvREDUCE_LIMIT').setAttribute('enabled', true);
                document.getElementById('rfvREDUCE_LIMIT').setAttribute('isvalid', false);
                document.getElementById('txtREDUCE_LIMIT').value = '';

                document.getElementById('rfvRISK_NAME').setAttribute('enabled', true);
                document.getElementById('rfvRISK_NAME').setAttribute('isvalid', false);
                document.getElementById('cmbRISK_NAME').selectedIndex = -1;
                clearCoverage();
            } else {
                document.getElementById('trReduceLimit').style.display = 'none';

                document.getElementById('rfvCOVERAGES').setAttribute('enabled', false);
                document.getElementById('rfvCOVERAGES').setAttribute('isValid', true);
                document.getElementById('rfvCOVERAGES').style.display = 'none';

                document.getElementById('rfvREDUCE_LIMIT').setAttribute('enabled', false);
                document.getElementById('rfvREDUCE_LIMIT').setAttribute('isValid', true);
                document.getElementById('rfvREDUCE_LIMIT').style.display = 'none';

                document.getElementById('rfvRISK_NAME').setAttribute('enabled', false);
                document.getElementById('rfvRISK_NAME').setAttribute('isValid', true);
                document.getElementById('rfvRISK_NAME').style.display = 'none';
                //                document.getElementById('cmbRISK_NAME').selectedIndex = -1;
                //                clearCoverage();
            }


        }
        function Disableallrfv() {
            if (typeof (Page_Validators) == 'undefined')
                var i, val;
            for (i = 0; i < Page_Validators.length; i++) {
                val = Page_Validators[i];
                val.setAttribute('enabled', false);
            }
        }



        $(document).ready(function() {
            $("#cmbCOVERAGES").change(function(e) {

                var RISK_ID = document.getElementById('cmbRISK_NAME').value;
                var COVERAGE_CODE_ID = document.getElementById('cmbCOVERAGES').value;
                if (document.getElementById('txtPOLICY_ID').value != '' && RISK_ID != '' && COVERAGE_CODE_ID != '') {
                    //CallAJAX("LaunchEndorsement.aspx/GetPolicy_Coverages", ["LOCATION_ID", LOCATION_ID], "outputDTRiskCOv", "ShowError", "#cmbRISK_NAME", "LOCATION_ID", "LOCATION");
                    PageMethod("GetCov_Prm", ["RISK_ID", RISK_ID, "COVERAGE_CODE_ID", COVERAGE_CODE_ID], AjaxSucceeded, AjaxFailed); //With parameters


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
            BindPremiumtxt(result);
        }

        function AjaxFailed(result) {
        }

        function BindPremiumtxt(result) {
            try {
                if (result.d.Table[0] != null)
                    document.getElementById('txtCOVERAGE_LIMIT').value = result.d.Table[0].LIMIT_1;

            } catch (ex) { }

        }

        function ClearExtOption() {
            
            if (document.getElementById('cmbCOVERAGES') != null) {
                if (document.getElementById('cmbCOVERAGES').options.length > 0) {
                    for (i = 0; i < document.getElementById('cmbCOVERAGES').options.length; i++) {
                        var option = document.getElementById('cmbCOVERAGES').options[i];
                        if (option.innerHTML == "") {
                            option.value = "";
                        }
                       else if (option.innerHTML == "All"){
                            document.getElementById('cmbCOVERAGES').remove(i);
                        }
                    }
                }
            }
        }
        function clearCoverage() {
            if (document.getElementById('cmbRISK_NAME').value == '') {
                document.getElementById('cmbCOVERAGES').innerHTML = '';
                document.getElementById('cmbCOVERAGES').innerHTML = '<option value=0></option>';
            }
        }
        function setCovCodeID() {
            if (document.getElementById('cmbCOVERAGES').value != '') {
                document.getElementById('hidCOVERAGE_CODE_ID').value = document.getElementById('cmbCOVERAGES').value;
            }
        }
        function setLocationID() {
            if (document.getElementById('cmbRISK_NAME').value != '') {
                document.getElementById('hidRISK_ID').value = document.getElementById('cmbRISK_NAME').value;
            }
        }

        function FetchPolInfoXML(PolNum) {
            var ParamArray = new Array();
            obj1 = new Parameter('POLICY_NUMBER', PolNum);
            ParamArray.push(obj1);
            var objRequest = _CreateXMLHTTPObject();
            var Action = 'AI_INFO';

            //If else Condition Added For according to PRAVEEN KASANA mail.
            if (document.getElementById('txtPOLICY_ID').value.length < 20) {
                alert(document.getElementById('hidMess').value);
                document.getElementById('txtPOLICY_ID').value = "";
                return false;
            }

        }
    </script>

</head>
<body ms_positioning="GridLayout" leftmargin="0" rightmargin="0" onload="Init();"
    onkeydown="if(event.keyCode==13){ __doPostBack('btnFIND',null); return false;}">
    <webcontrol:menu id="bottomMenu" runat="server">
    </webcontrol:menu>
    <form id="MNT_LAUNCH_AUTO_END" runat="server" method="post">
    <table cellspacing="0" cellpadding="0" align="center" border="0" width="90%">
        <tr>
            <td>
                <table id="Table1" cellspacing="1" cellpadding="0" width="100%" align="center" border="0">
                    <tr>
                        <td>
                            <webcontrol:gridspacer id="grdSpacer" runat="server">
                            </webcontrol:gridspacer>
                        </td>
                    </tr>
                    <tr>
                        <td class="pageheader" align="center" colspan="4">
                        </td>
                    </tr>
                    <tr align="center">
                        <td align="center" class="headereffectcenter" colspan="4">
                            <asp:Label ID="capHeaderLabel" runat="server">Auto Endorsement</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolorc" align="right" colspan="5">
                            <asp:Label ID="lblMessage" runat="server" CssClass="errmsg"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="trRuleHd" visible="false">
                        <td class="headereffectcenter" colspan="5">
                            <asp:Label runat="server" ID="lblUWRULES_STATUS"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <div class="midcolora" id="myDIV" style="overflow: auto; width: 100%; height: 189px;
                                display: None" align="center" runat="server">
                            </div>
                        </td>
                    </tr>
                    <tr align="center">
                        <td class="midcolorr" width="40%" colspan="2">
                            <asp:Label ID="capPOLICY_ID" runat="server">Policy Number</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="60%" colspan="2">
                            <asp:TextBox ID="txtPOLICY_ID" runat="server" size="30" MaxLength="21" onchange="FetchPolInfoXML(this.value)"></asp:TextBox>
                            <img id="imgSelect" style="cursor: hand" alt="" src="../../cmsweb/images/selecticon.gif"
                                runat="server" />
                            <cmsb:CmsButton class="clsButton" ID="btnFIND" runat="server" Text="Find" OnClick="btnFIND_Click">
                            </cmsb:CmsButton>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvPOLICY_ID" runat="server" ControlToValidate="txtPOLICY_ID"
                                Display="Dynamic" ErrorMessage=""></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="trEndorsmentOp">
                        <td class="midcolorr" width="40%" colspan="2">
                            <asp:Label runat="server" ID="capENDORSEMENT_TYPE">Endorsement options</asp:Label><span
                                class="mandatory">*</span>
                        </td>
                        <td class="midcolora" colspan="2" width="60%">
                            <asp:DropDownList runat="server" ID="cmbENDORSEMENT_TYPE" onchange="EndType()">
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvENDORSEMENT_TYPE" runat="server" Display="Dynamic"
                                ErrorMessage="" ControlToValidate="cmbENDORSEMENT_TYPE"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table id="trReduceLimit" width="100%">
                                <tr runat="server">
                                    <td class="midcolorc" width="35%">
                                        <asp:Label runat="server" ID="capRISK_NAME">Select Risk</asp:Label><span class="mandatory">*</span>
                                        <br />
                                        <asp:DropDownList runat="server" ID="cmbRISK_NAME" SuccessMethod="outputDTSUBLOB"
                                            onchange="clearCoverage();setLocationID();" TargetControl="cmbCOVERAGES" ErrorMethod="ShowError"
                                            ItemValue="COVERAGE_CODE_ID" ItemText="COV_DES" class="FillDD" ServerMethod="LaunchEndorsement.aspx/GetPolicy_Coverages">
                                            <asp:ListItem Text="" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <input runat="server" id="hidRISK_ID" type="hidden" />
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvRISK_NAME" runat="server" ControlToValidate="cmbRISK_NAME"
                                            Display="Dynamic" ErrorMessage=""></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="midcolora" width="30%">
                                        <asp:Label runat="server" ID="capCOVERAGES">Select coverage</asp:Label><span class="mandatory">*</span>
                                        <br />
                                        <asp:DropDownList runat="server" ID="cmbCOVERAGES" onfocus="ClearExtOption()" onchange="setCovCodeID()">
                                            <asp:ListItem Text="" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <input runat="server" id="hidCOVERAGE_CODE_ID" type="hidden" />
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvCOVERAGES" runat="server" ControlToValidate="cmbCOVERAGES"
                                            Display="Dynamic" ErrorMessage=""></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="midcolora" width="10%">
                                        <asp:Label runat="server" ID="capREDUCE_LIMIT">Reduce Premium</asp:Label><span class="mandatory">*</span><br />
                                        <asp:TextBox runat="server" ID="txtREDUCE_LIMIT"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator runat="server" ID="rfvREDUCE_LIMIT" ControlToValidate="txtREDUCE_LIMIT"
                                            Display="Dynamic" ></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator runat="server" ID="revREDUCE_LIMIT" ControlToValidate="txtREDUCE_LIMIT"
                                            Display="Dynamic" ErrorMessage="Please enter numeric value"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="midcolora" width="10%">
                                        <asp:Label runat="server" ID="capCOVERAGE_LIMIT">Coverage Premium</asp:Label><br />
                                        <asp:TextBox runat="server" ID="txtCOVERAGE_LIMIT" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr  id="trbtnLaunch">
                        <td class="midcolora" colspan="3">
                        </td>
                        <td class="midcolorr" width="20%">
                            <cmsb:CmsButton class="clsButton" ID="btnLAUNCH" runat="server" Text="Launch" OnClick="btnLAUNCH_Click">
                            </cmsb:CmsButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <input type="hidden" runat="server" id="hidCUSTOMER_ID" />
                <input type="hidden" runat="server" id="hidPOLICY_ID" />
                <input type="hidden" runat="server" id="hidPOLICY_VERSION_ID" />
                <input type="hidden" runat="server" id="hidENDORSEMENT_NO" />
                <input type="hidden" runat="server" id="hidPOLICY_EFF_DATE" />
                <input type="hidden" runat="server" id="hidPOLICY_EXP_DATE" />
                <input type="hidden" runat="server" id="hidNEW_POLICY_VERSION_ID" />
                <input type="hidden" runat="server" id="hidUNDERWRITER" />
                <input type="hidden" runat="server" id="hidLOB_ID" />
                <input type="hidden" runat="server" id="hidSTATE_CODE" />
                <input type="hidden" runat="server" id="hidSTATE_ID" />
                <input type="hidden" runat="server" id="hidPROCESS_ID" />
                <input type="hidden" runat="server" id="hidROW_ID" />
                <input type="hidden" runat="server" id="hidPOLICY_FLAG" />
                <input id="hidMess" type="hidden" runat="server" name="hidMess">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

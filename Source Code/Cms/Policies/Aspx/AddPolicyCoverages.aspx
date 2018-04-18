<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="AddPolicyCoverages.aspx.cs"
    ValidateRequest="false" Inherits="Cms.Policies.Aspx.AddPolicyCoverages" %>

<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Coverages</title>
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet">

    <script src="../../cmsweb/scripts/xmldom.js"></script>

    <script src="../../cmsweb/scripts/common.js"></script>

    <script src="../../cmsweb/scripts/form.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>

    <script src="../../cmsweb/scripts/Coverages.js"></script>

    <script language="javascript" type="text/javascript">
        var policyPrefix = "dgPolicyCoverages_ctl";
        var ShowSaveMsgAlways = true;
        var firstTime = false;
        var SubCoverage='';
        function ShowDeductibleText(ctl) {
            var lastIndex1 = ctl.id.lastIndexOf('_');
            var policyPrefix = ctl.id.substring(0, lastIndex1);
            var covid = document.getElementById(policyPrefix + '_lblCOV_ID').innerHTML;
            var covDesc = document.getElementById(policyPrefix + '_lblCOV_DESC').innerHTML;
            if (covid == 4191) {

                covDesc = String(covDesc).substring(0, 500);

            }
            var deductCtl = document.getElementById(policyPrefix + '_hidDEDUCTIBLE_TEXT')
            var deductCtlText = deductCtl.value;
            //window.open("/cms/CmsWeb/aspx/ViewEditDeductible.aspx?CALLEDFROM=POL&CUSTOMER_ID=" + document.getElementById('hidCUSTOMERID').value + "&POLICY_ID=" + document.getElementById('hidPOLID').value + "&POLICY_VERSION_ID=" + document.getElementById('hidPolVersionID').value + "&RISK_ID=" + document.getElementById('hidRISK_ID').value + "&COV_ID=" + covid + "&COV_DESC=" + covDesc + "&DEDUCT_CTL_ID=" + deductCtl.id + "&DeductText=" + deductCtlText + "&", 'Deductible', "'resizable=no,scrollbars=yes,left=150,top=50,width=700,height=600'");
            window.open("/cms/CmsWeb/aspx/ViewEditDeductible.aspx?CALLEDFROM=POL&COV_ID=" + covid + "&COV_DESC=" + covDesc + "&DEDUCT_CTL_ID=" + deductCtl.id + "&DeductText=" + deductCtlText + "&", 'Deductible', "'resizable=no,scrollbars=yes,left=150,top=50,width=500,height=500'");
            document.getElementById(policyPrefix + '_lnkDEDUCTIBLE').style.color = 'purple';
            return false;
            // window.showModalDialog("/cms/CmsWeb/aspx/ViewEditDeductible.aspx?CALLEDFROM=POL&COV_ID=" + covid + "&COV_DESC=" + covDesc + "&DEDUCT_CTL_ID=" + deductCtl.id + "&DeductText=" + deductCtlText + "&", window, "'resizable=no,scrollbars=yes,left=150,top=50,width=500,height=500'");
        }
        //Displays the controls in a row according to coverage and deductible type
        function DisableControls(strcbDelete) {

        }
        //Sets the current value of the passed in check box into a hidden field
        function SetHiddenField(checkBoxID) {

            var lastIndex = checkBoxID.lastIndexOf('_');

            var hid = checkBoxID.substring(0, lastIndex) + '_hidcbDelete';

            var hidField = document.getElementById(hid);

            if (hidField != null) {
                hidField.value = document.getElementById(checkBoxID).checked;
                hidField.disabled = false;
                //alert(hidField.id + ' ' + hidField.value);
            }

            //return hidField;

        }

        function onButtonClick(chk, rowCount) {

            var span = chk.parentElement;
            var covID = 0;
            if (span == null) return;
            SetHiddenField(chk.id);

            var covID = span.getAttribute("COV_ID");
            var covCode = span.getAttribute("COV_CODE");
            //alert('Button CLick --  ' + covCode);
            var lastIndex1 = chk.id.lastIndexOf('__');
            var dgPrefix = chk.id.substring(0, lastIndex1);
            if (chk.checked == true) {
                var toDisable = GetControlInGridFromCode(covCode, '_hidCHECKDDISABLE');               
                if (toDisable == null) return;
                var lastIndex = toDisable.id.lastIndexOf('_');
                var prefix = toDisable.id.substring(0, lastIndex);
                var toEnable = document.getElementById(prefix + '_hidCHECKDENABLE');
                if (toDisable != "")
                    SubCoverage = SubCoverage + ',' + toEnable.value;
                var toUncheck = document.getElementById(prefix + '_hidCHECKDDSELECT');
                var toCheck = document.getElementById(prefix + '_hidCHECKDSELECT');
                var SubCoverageArr = SubCoverage.split(',');
                var EnableValidator = true;
                for (i = 0; i < SubCoverageArr.length; i++) {
                    if (covCode == SubCoverageArr[i]) {
                        EnableValidator = false;
                        break;           
                    }
                    
                 }
                 if (EnableValidator) {
                     if (document.getElementById(prefix + '_rfvPREMIUM') != null)
                         document.getElementById(prefix + '_rfvPREMIUM').setAttribute('enabled', true);
                     if (document.getElementById(prefix + '_rfvLIMIT') != null)
                         document.getElementById(prefix + '_rfvLIMIT').setAttribute('enabled', true);
//                     if (document.getElementById(prefix + '_csvLIMIT') != null)
//                         document.getElementById(prefix + '_csvLIMIT').setAttribute('enabled', true);
                 }
            }
            else if (chk.checked == false) {
            var toDisable = GetControlInGridFromCode(covCode, '_hidCHECKDDISABLE');
            if (toDisable == null) return;
            var lastIndex = toDisable.id.lastIndexOf('_');
            var prefix = toDisable.id.substring(0, lastIndex);
            //for itrack no 1362
            if (document.getElementById(prefix + '_rfvPREMIUM') != null) {
                document.getElementById(prefix + '_rfvPREMIUM').setAttribute('enabled', false);
                document.getElementById(prefix + '_rfvPREMIUM').style.display = "none";
            }
            if (document.getElementById(prefix + '_rfvLIMIT') != null) {
                document.getElementById(prefix + '_rfvLIMIT').setAttribute('enabled', false);
                document.getElementById(prefix + '_rfvLIMIT').style.display = "none";
            }
//                if (document.getElementById(prefix + '_csvLIMIT') != null)
//                    document.getElementById(prefix + '_csvLIMIT').setAttribute('enabled', false);
                var toDisable = GetControlInGridFromCode(covCode, '_hidUNCHECKDDISABLE');
                if (toDisable == null) return;

                var lastIndex = toDisable.id.lastIndexOf('_');
                var prefix = toDisable.id.substring(0, lastIndex);

                var toEnable = document.getElementById(prefix + '_hidUNCHECKDENABLE');
                var toUncheck = document.getElementById(prefix + '_hidUNCHECKDDSELECT');
                var toCheck = document.getElementById(prefix + '_hidUNCHECKDSELECT');
            }
            if (trim(toDisable.value) != "") {
                var toDisableArray = toDisable.value.split(",");
                for (i = 0; i < toDisableArray.length; i++) {
                    var cbCTRL = GetControlInGridFromCode(toDisableArray[i], '_cbDelete');
                    if (cbCTRL != null) {
                        //cbCTRL.parentElement.parentElement.parentElement.disabled = true;
                        EnableDisableRow(cbCTRL.parentElement.parentElement.parentElement, true);
                        cbCTRL.disabled = true;
                        //if(document.getElementById(cbCTRL.id.substring(0, cbCTRL.id.lastIndexOf('_')) + '_rfvPREMIUM')!=null)
                        //  document.getElementById(cbCTRL.id.substring(0, cbCTRL.id.lastIndexOf('_')) + '_rfvPREMIUM').setAttribute('enabled', false);
                    }
                }
            }
            if (trim(toEnable.value) != "") {
                var toEnableArray = toEnable.value.split(",");
                for (i = 0; i < toEnableArray.length; i++) {
                    var cbCTRL = GetControlInGridFromCode(toEnableArray[i], '_cbDelete');
                    if (cbCTRL != null) {
                        //cbCTRL.parentElement.parentElement.parentElement.disabled = false;
                        EnableDisableRow(cbCTRL.parentElement.parentElement.parentElement, false);
                        cbCTRL.disabled = false;
                        //if (document.getElementById(cbCTRL.id.substring(0, cbCTRL.id.lastIndexOf('_')) + '_rfvPREMIUM') != null)
                        //document.getElementById(cbCTRL.id.substring(0, cbCTRL.id.lastIndexOf('_')) + '_rfvPREMIUM').setAttribute('enabled', false);

                    }
                }
            }
            if (trim(toUncheck.value) != "") {
                var toUnCheckArray = toUncheck.value.split(",");
                for (i = 0; i < toUnCheckArray.length; i++) {
                    var cbCTRL = GetControlInGridFromCode(toUnCheckArray[i], '_cbDelete');
                    if (cbCTRL != null) {
                        cbCTRL.checked = false;
                        DisableControls(cbCTRL.id);
                        SetHiddenField(cbCTRL.id);
                    }
                }
            }
            if (trim(toCheck.value) != "") {
                var toCheckArray = toCheck.value.split(",");
                for (i = 0; i < toCheckArray.length; i++) {
                    var cbCTRL = GetControlInGridFromCode(toCheckArray[i], '_cbDelete');
                    var ddlCTRL = GetControlInGridFromCode(toCheckArray[i], '_ddlLIMIT');
                    if (cbCTRL != null) {
                        if (firstTime == false)
                            cbCTRL.checked = true;
                        DisableControls(cbCTRL.id);
                        SetHiddenField(cbCTRL.id);
                    }
                }
            }


        }
        //		    function formatRate(ctlRate) {
        //		        if (isNaN(ctlRate)) return ctlRate;
        //		        if (ctlRate != "")
        //		            ctlRate = parseFloat(ctlRate).toFixed(4);
        //		        return ctlRate;
        //		    }
        //Added By Lalit for enable require field validator on premium field
        function onloadEnablePremiumRfv() {
            return;
            var frm = document.POL_PRODUCT_COVERAGES;
            for (i = 0; i < frm.length; i++) {
                e = frm.elements[i];
                if (e.type == 'checkbox' && e.id.indexOf('_cbDelete') != -1) {
                    var Len = e.id.length;
                    var ReplaceText = '_cbDelete';
                    var id = e.id.substring(0, (Len - ReplaceText.length))
                    if (document.getElementById(id + '_rfvPREMIUM') != null) {
                        if (e.checked == true) {
                            document.getElementById(id + '_rfvPREMIUM').setAttribute('enabled', true);
                            //                            document.getElementById(id + '_csvPREMIUM').setAttribute('enabled', true);
                            document.getElementById(id + '_rfvLIMIT').setAttribute('enabled', true);
                            document.getElementById(id + '_csvLIMIT').setAttribute('enabled', true);
                        } else {
                            document.getElementById(id + '_rfvPREMIUM').setAttribute('enabled', false);
                            //                            document.getElementById(id + '_csvPREMIUM').setAttribute('enabled', false);
                            document.getElementById(id + '_rfvLIMIT').setAttribute('enabled', false);
                            document.getElementById(id + '_csvLIMIT').setAttribute('enabled', false);
                        }
                    }
                }
            }
        }
        //   enable/disable when coverage is selected
        function onselectEnablePremiumRfv(obj) {
            return;
            var Len = obj.id.length;
            var ReplaceText = '_cbDelete';
            var id = obj.id.substring(0, (Len - ReplaceText.length))
            if (document.getElementById(id + '_rfvPREMIUM') != null) {
                if (obj.checked == true) {
                    document.getElementById(id + '_rfvPREMIUM').setAttribute('enabled', true);
                    // document.getElementById(id + '_csvPREMIUM').setAttribute('enabled', true);
                    document.getElementById(id + '_rfvLIMIT').setAttribute('enabled', true);
                    document.getElementById(id + '_csvLIMIT').setAttribute('enabled', true);


                } else {
                    document.getElementById(id + '_rfvPREMIUM').setAttribute('enabled', false);
                    // document.getElementById(id + '_csvPREMIUM').setAttribute('enabled', false);
                    document.getElementById(id + '_rfvLIMIT').setAttribute('enabled', false);
                    document.getElementById(id + '_csvLIMIT').setAttribute('enabled', false);

                }
            }
        }
        //Custom validator function for premium > 0
        function validatePremium(objSource, objArgs) {
            var Premium = document.getElementById(objSource.controltovalidate).value;
            if (parseFloat(Premium) > 0)
                objArgs.IsValid = true;
            else
                objArgs.IsValid = false;
        }
        function FormatAmountForSum(num) {
            num = ReplaceAll(num, sGroupSep, '');
            num = ReplaceAll(num, sDecimalSep, '.');
            return num;
        }
        function validateLimit(objSource, objArgs) {


            var Change = false;


            var Limt = document.getElementById(objSource.controltovalidate).value;
            Limt = FormatAmountForSum(Limt);

            if (parseFloat(Limt) >= 0)
                objArgs.IsValid = true;

            else
                objArgs.IsValid = false;

        }

        function validateRange(objSource, objArgs) {

            var Range = document.getElementById(objSource.controltovalidate).value;
            Range = FormatAmountForSum(Range);
            if (parseFloat(Range) <= 100)
                objArgs.IsValid = true;
            else
                objArgs.IsValid = false;
        }
        function validateLimitRange(sender, args) {
            
            var input = args.Value;
            input = FormatAmountForSum(input)
            var max = 999999999.99;//modified by pradeep - itrack 1512
            if (parseFloat(input) <= parseFloat(max)) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }
        //Added by Pradeep  itrack 1512
        function validatePremiumRange(sender, args) {

            var input = args.Value;
            input = FormatAmountForSum(input)
            var max = 922337203685477.5807;
            if (parseFloat(input) <= parseFloat(max)) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }
        //Added till here 
        function LimitChange(obj) {

            var id = obj.id;
            var hidID = id.lastIndexOf('_')
            hidID = id.substring(0, hidID) + '_hidLIMIT';
            if (document.getElementById(hidID) != null) {
                if (FormatAmountForSum(document.getElementById(hidID).value) != FormatAmountForSum(obj.value)) {
                    var CustomValidatorId = id.substring(0, id.lastIndexOf('_')) + '_csvLIMIT'
                    if (document.getElementById(CustomValidatorId) != null) {
                        var a = document.getElementById(CustomValidatorId).getattribute(ClientValidationFunction);
                    }
                }
            }
        }
        //Added by Pradeep Kushwaha on 03-Aug-2011 for itrack - 943
        function fnCalculateTotalCoveragePremium() {

            var Amount = 0;
            $("#dgPolicyCoverages input[name*='txtPREMIUM']").each(function (index) {
                //Check if number is not empty               
                if ($.trim($(this).val()) != "")
                    Amount = Amount + parseFloat(FormatAmountForSum($(this).val()));
            });
            $("#txtTOTAL_COVERAGE_PREMIUM").val(formatAmount(Amount));
        }
        //Added till here 
        //Added by Pradeep Kushwaha on 28-April-2011 (iTrack-1052)
        $(document).ready(function () {
            //Added by Pradeep Kushwaha on 03-Aug-2011 for itrack - 943
            $("#txtTOTAL_COVERAGE_PREMIUM").attr('readonly', 'true'); //Added for itrack-943
            fnCalculateTotalCoveragePremium(); //Added for itrack-943
            $("#dgPolicyCoverages tr input[id $= 'txtPREMIUM']").live('blur', function (e) {
                var Amount = 0;
                $("#dgPolicyCoverages input[name*='txtPREMIUM']").each(function (index) {
                    //Check if number is not empty               
                    if ($.trim($(this).val()) != "")
                        Amount = Amount + parseFloat(FormatAmountForSum($(this).val()));
                });
                $("#txtTOTAL_COVERAGE_PREMIUM").val(formatAmount(Amount));

            });
            //Added till here 

            $('body').bind('keydown', function(event) {

                if (event.keyCode == '13') {
                    $("#btnSave").trigger('click');
                    return false;
                }
            });
            var gridView1Control = document.getElementById("#dgPolicyCoverages");
            $("#btnSave").click(function(e) {
                var Checked = false;
                $('input:checkbox[id$=_cbDelete]', gridView1Control).each(function(item, index) {

                    if ($(this).next('input:hidden[id$=_cbDelete]:checked').context.checked) {
                        Checked = true;
                        return false;
                    }

                });
                if (Checked == false) {
                    alert($("#hidAlertMsg").val());
                    isPageValid_ToSubmit = false;
                    return false;
                }


            });
            $("#dgPolicyCoverages tr input:checkbox[id$=chkIS_ACTIVE]").live('click', function(e) {
                if ($(this).next('input:hidden[id$=chkIS_ACTIVE]:checked').context.checked) {
                    var txtLIMIT = FormatAmountForSum($("#" + $("#dgPolicyCoverages tr input[id $= 'txtLIMIT']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).val());
                    if (parseFloat(txtLIMIT) > 0) {
                        alert($("#hidExcludeMsg").val());
                        $("#" + $("#dgPolicyCoverages tr input[id $= 'txtLIMIT']")[$(this).closest('td').parent().attr('sectionRowIndex') - 1].id).focus();
                    }
                }
            });

        });


        //Added till here 
    </script>

</head>
<body oncontextmenu="return true;" leftmargin="0" rightmargin="1" onload="ApplyColor();ChangeColor();"
    ms_positioning="GridLayout">
    <form id="POL_PRODUCT_COVERAGES" method="post" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td class="headereffectCenter">
                <asp:Label ID="lblTitle" runat="server">Coverages</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="midcolora">
                <asp:Label ID="lblMsg" runat="server">Coverages marked in bold are primary coverages</asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblMessage" runat="server" EnableViewState="False" CssClass="errmsg"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="headerEffectSystemParams">
                <asp:Label ID="lblPolicyCaption" runat="server">Policy Level Coverages</asp:Label>
            </td>
        </tr>
        <tr id="trPOLICY_LEVEL_GRID" runat="server">
            <td class="midcolora">
                <asp:DataGrid ID="dgPolicyCoverages" runat="server" Width="100%" AutoGenerateColumns="False"
                    DataKeyField="COVERAGE_ID">
                    <AlternatingItemStyle></AlternatingItemStyle>
                    <ItemStyle></ItemStyle>
                    <HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn ItemStyle-Width="5%" HeaderText="Select">
                            <ItemTemplate>
                                <asp:Label ID="lblCOV_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
                                </asp:Label>
                                <asp:CheckBox ID="cbDelete" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'
                                    COV_ID='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'></asp:CheckBox>
                                <input type="hidden" id="hidcbDelete" name="hidcbDelete" runat="server" />
                                <asp:Label ID="lblLIMIT_TYPE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT_TYPE") %>'> </asp:Label>
                                <asp:Label ID="lblDEDUCTIBLE_TYPE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_TYPE") %>'> </asp:Label>
                                <asp:Label ID="lblIS_LIMIT_APPLICABLE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IS_LIMIT_APPLICABLE") %>'> </asp:Label>
                                <asp:Label ID="lblIS_DEDUCT_APPLICABLE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IS_DEDUCT_APPLICABLE") %>'> </asp:Label>
                                <asp:Label ID="lblAdd_IS_DEDUCT_APPLICABLE" Style="display: none" runat="server"
                                    Text='<%# DataBinder.Eval(Container, "DataItem.ISADDDEDUCTIBLE_APP") %>'> </asp:Label>
                                <input type="hidden" id="hidCHECKDDISABLE" runat="server" cov_code='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'
                                    name="hidCHECKDDISABLE" />
                                <input type="hidden" id="hidCHECKDENABLE" runat="server" cov_code='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'
                                    name="hidCHECKDENABLE" />
                                <input type="hidden" id="hidCHECKDSELECT" runat="server" cov_code='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'
                                    name="hidCHECKDSELECT" />
                                <input type="hidden" id="hidCHECKDDSELECT" runat="server" cov_code='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'
                                    name="hidCHECKDDSELECT" />
                                <input type="hidden" id="hidUNCHECKDDISABLE" runat="server" cov_code='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'
                                    name="hidUNCHECKDDISABLE" />
                                <input type="hidden" id="hidUNCHECKDENABLE" runat="server" cov_code='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'
                                    name="hidUNCHECKDENABLE" />
                                <input type="hidden" id="hidUNCHECKDSELECT" runat="server" cov_code='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'
                                    name="hidUNCHECKDSELECT" />
                                <input type="hidden" id="hidUNCHECKDDSELECT" runat="server" cov_code='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'
                                    name="hidUNCHECKDDSELECT" />
                                <input type="hidden" id="hidDEDUCTIBLE_TEXT" runat="server" cov_code='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'
                                    name="hidDEDUCTIBLE_TEXT" value='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE2_AMOUNT_TEXT") %>' />
                                <input type="hidden" id="hidCOV_CODE" name="hidCOV_CODE" value='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'
                                    runat="server" />
                                <input type="hidden" id="hidCOV_REF_CODE" name="hidCOV_REF_CODE" value='<%# DataBinder.Eval(Container, "DataItem.COV_REF_CODE") %>'
                                    runat="server" />
                                <input type="hidden" id="hidCOV_ID" name="hidCOV_ID" value='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'
                                    runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="5%"></ItemStyle>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn ItemStyle-Width="2%" Visible="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkIS_ACTIVE" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'
                                    COV_ID='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'></asp:CheckBox>
                                <input type="hidden" id="hidIS_ACTIVE" name="hidIS_ACTIVE" value='<%# DataBinder.Eval(Container, "DataItem.IS_ACTIVE") %>'
                                    runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="5%"></ItemStyle>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn ItemStyle-Width="5%" HeaderText="RI Applies" Visible="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbRIDelete" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'
                                    COV_ID='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'></asp:CheckBox>
                                <input type="hidden" id="hidcbRIDelete" name="hidcbRiDelete" runat="server">
                            </ItemTemplate>
                            <ItemStyle Width="5%"></ItemStyle>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Coverage">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCOV_DESC" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="20%"></ItemStyle>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn ItemStyle-Width="5%" HeaderText="Indemnity Period" Visible="true">
                            <ItemStyle HorizontalAlign="right"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtIndemnity_Period" runat="server" MaxLength="4" size="6"></asp:TextBox>
                                <br>
                                <asp:RegularExpressionValidator ID="revIndemnity" Enabled="False" runat="server"
                                    ControlToValidate="txtIndemnity_Period" Display="Dynamic"></asp:RegularExpressionValidator>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Sum Insured/ Sub Limit" ItemStyle-Width="13%">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <select id="ddlLIMIT" visible="True" runat="server" cov_code='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'
                                    onchange="javascript:OnDDLChange(this);" name="ddlLIMIT">
                                </select>
                                <asp:Label ID="lblLIMIT" CssClass="labelfont" runat="server">
											<%# DataBinder.Eval(Container, "DataItem.INCLUDED","{0:,#,###}") %>
                                </asp:Label>
                                 <%-- changed by praveer itrack no 1512/TFS#240--%>
                                <asp:TextBox ID="txtLIMIT" runat="server" CssClass="INPUTCURRENCY" MaxLength="12"
                                    size="22"></asp:TextBox>
                                <input type="hidden" id="hidLIMIT" runat="server"></input>
                                <br>
                                <asp:RegularExpressionValidator ID="revLIMIT" Enabled="False" runat="server" ControlToValidate="txtLIMIT"
                                    Display="Dynamic"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfvLIMIT" runat="server" Enabled="false" ControlToValidate="txtLIMIT"
                                    ErrorMessage="" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="csvLIMIT" runat="server" ControlToValidate="txtLIMIT" ErrorMessage=""
                                    Display="Dynamic" ClientValidationFunction="validateLimit"></asp:CustomValidator>
                                <asp:CustomValidator ID="csvLIMIT1" runat="server" ControlToValidate="txtLIMIT" ErrorMessage=""
                                    Display="Dynamic" ClientValidationFunction="validateLimitRange"></asp:CustomValidator>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Deductible Type" ItemStyle-Width="10%">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <select id="ddlDEDUCTIBLETYPE" visible="True" runat="server" name="ddlDEDUCTIBLETYPE">
                                </select>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Deductible Amount" ItemStyle-Width="10%">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <select id="ddlDEDUCTIBLE" visible="True" runat="server" name="ddlDEDUCTIBLE">
                                </select>
                                <asp:Label ID="lblDEDUCTIBLE" CssClass="labelfont" runat="server">N.A.</asp:Label>
                                <asp:TextBox ID="txtDEDUCTIBLE" runat="server" CssClass="INPUTCURRENCY" MaxLength="10"
                                    size="10"></asp:TextBox>
                                <br>
                                <asp:RegularExpressionValidator ID="revDEDUCTIBLE" Enabled="False" runat="server"
                                    ControlToValidate="txtDEDUCTIBLE" Display="Dynamic"></asp:RegularExpressionValidator>
                                <asp:CustomValidator ID="csvDEDUCTIBLE" runat="server" ControlToValidate="txtDEDUCTIBLE"
                                    ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimit"></asp:CustomValidator>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Minimun Deductible" ItemStyle-Width="10%">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtMIN_DEDUCTIBLE" runat="server" CssClass="INPUTCURRENCY" MaxLength="12"
                                    size="12"></asp:TextBox>
                                <a href="#">
                                    <asp:Label ID="lnkDEDUCTIBLE" CssClass="labelfont" runat="server" onclick="javascript:return ShowDeductibleText(this);">View/Edit Text</asp:Label></a>
                                <asp:RegularExpressionValidator ID="revMIN_DEDUCTIBLE" Enabled="False" runat="server"
                                    ControlToValidate="txtMIN_DEDUCTIBLE" Display="Dynamic"></asp:RegularExpressionValidator>
                                <asp:CustomValidator ID="csvMIN_DEDUCTIBLE" runat="server" ControlToValidate="txtMIN_DEDUCTIBLE"
                                    ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimit"></asp:CustomValidator>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn ItemStyle-Width="3%" HeaderText="Deduct Reduces SI/ SubLimit">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbDedReduce" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'
                                    COV_ID='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'></asp:CheckBox>
                                <input type="hidden" id="hidcbDedReduce" name="hidcbDedReduce" runat="server">
                            </ItemTemplate>
                            <ItemStyle Width="3%"></ItemStyle>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Initial Rate" ItemStyle-Width="5%">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtINITIAL_RATE" runat="server" CssClass="INPUTCURRENCY" MaxLength="7"
                                    size="7"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revINITIAL_RATE" Enabled="False" runat="server"
                                    ControlToValidate="txtINITIAL_RATE" Display="Dynamic"></asp:RegularExpressionValidator>
                                <asp:CustomValidator ID="csvINITIAL_RATE" runat="server" ControlToValidate="txtINITIAL_RATE"
                                    ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimit"></asp:CustomValidator>
                                <asp:CustomValidator ID="csvGREATER_RATE" runat="server" ControlToValidate="txtINITIAL_RATE"
                                    ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateRange"></asp:CustomValidator>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Final Rate" ItemStyle-Width="5%">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txtFINAL_RATE" runat="server" CssClass="INPUTCURRENCY" MaxLength="7"
                                    size="7"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revFINAL_RATE" Enabled="False" runat="server"
                                    ControlToValidate="txtFINAL_RATE" Display="Dynamic"></asp:RegularExpressionValidator>
                                <asp:CustomValidator ID="csvFINAL_RATE" runat="server" ControlToValidate="txtFINAL_RATE"
                                    ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimit"></asp:CustomValidator>
                                <asp:CustomValidator ID="csvGREATER_FINAL_RATE" runat="server" ControlToValidate="txtFINAL_RATE"
                                    ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateRange"></asp:CustomValidator>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn ItemStyle-Width="1%" HeaderText="Average Rate">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbAvarageRate" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'
                                    COV_ID='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'></asp:CheckBox>
                                <input type="hidden" id="hidcbAvarageRate" name="hidcbAvarageRate" runat="server">
                            </ItemTemplate>
                            <ItemStyle Width="1%"></ItemStyle>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Premium" ItemStyle-Width="13%">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            <ItemTemplate>
                           <%-- changed by praveer itrack no 1512/TFS#240--%>
                                <asp:TextBox ID="txtPREMIUM" runat="server" CssClass="INPUTCURRENCY" MaxLength="17" 
                                    size="22"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revPREMIUM" Enabled="False" runat="server" ControlToValidate="txtPREMIUM"
                                    Display="Dynamic"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfvPREMIUM" runat="server" Enabled="false" ControlToValidate="txtPREMIUM"
                                    ErrorMessage="" Display="Dynamic"></asp:RequiredFieldValidator>
                                <%--<asp:CustomValidator ID="csvPREMIUM" runat="server" ControlToValidate="txtPREMIUM"
                                    ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimit"></asp:CustomValidator>--%>
                                <asp:CustomValidator ID="csvPREMIUM1" runat="server" ControlToValidate="txtPREMIUM"
                                    ErrorMessage="" Display="Dynamic" ClientValidationFunction="validatePremiumRange"></asp:CustomValidator>
                                
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblCOV_CODE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblCOV_TYPE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COVERAGE_TYPE") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn ItemStyle-Width="5%" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIS_MAIN" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IS_MAIN") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%"></ItemStyle>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>
        <%--Added by Pradeep for itrack 943--%>
        <tr align="right" id="trTOTAL_COVERAGE_PREMIUM" runat="server">
         <td class="midcolorr" align="right">
         <asp:Label ID="lblTOTAL_COVERAGE_PREMIUM" Font-Bold  runat="server"></asp:Label>
         <asp:TextBox ID="txtTOTAL_COVERAGE_PREMIUM" Font-Bold  CssClass="INPUTCURRENCY" size="22"  runat="server"></asp:TextBox>
        </td>
        </tr>
        <%--Added till here --%>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td class="midcolora" colspan="3">
                        </td>
                        <td class="midcolorr">
                            <cmsb:CmsButton class="clsButton" ID="btnSave" runat="server" Text="Save"></cmsb:CmsButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <input id="hidOldData" type="hidden" name="hidOldData" runat="server" />
                <input id="hidRISK_ID" type="hidden" value="0" name="hidRISK_ID" runat="server" />
                <input id="hidPolID" type="hidden" name="hidPolID" value="0" runat="server" />
                <input id="hidPolVersionID" type="hidden" name="hidPolVersionID" value="0" runat="server" />
                <input id="hidCustomerID" type="hidden" name="hidCustomerID" value="0" runat="server" />
                <input id="hidPOLICY_ROW_COUNT" type="hidden" value="0" name="hidPOLICY_ROW_COUNT"
                    runat="server" />
                <input id="hidCoverageXML" type="hidden" runat="server" name="hidCoverageXML" />
                <input id="hidLOBState" type="hidden" name="hidLOBState" runat="server" />
                <input id="hidCustomInfo" type="hidden" name="hidCustomInfo" runat="server" />
                <input id="hidControlXML" type="hidden" name="hidControlXML" runat="server" />
                <input id="hidCO_APPLICANT_ID" type="hidden" value="0" name="hidControlXML" runat="server" />
                <input id="hidAlertMsg" type="hidden" value="0" name="hidAlertMsg" runat="server" />
                <input id="hidPROCESS_ID" type="hidden" name="hidPROCESS_ID" runat="server" />
                <input id="hidExcludeMsg" type="hidden" name="hidExcludeMsg" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

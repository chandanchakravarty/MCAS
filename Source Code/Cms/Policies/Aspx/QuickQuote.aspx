<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuickQuote.aspx.cs" Inherits="Cms.Policies.Aspx.QuickQuote"  ValidateRequest="false" %>

<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Quick Quote</title>
    <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
    <link href="/cms/cmsweb/css/css1.css" type="text/css" rel="Stylesheet" />
    <style type="text/css">
    
    .selectedcolor
    {
        color:Red;
        background:yellow;
    }

    </style>
    
    <script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>

    <script type="text/javascript" src="/cms/cmsweb/scripts/Calendar.js"></script>
    
    <script type="text/javascript" language="javascript">
        var mortgageeBill = '11276';
        var InsuredMortgageeBill = '11278';
        var refWindow;
        var jsaAppDtFormat = "<%=aAppDtFormat  %>";
        function ValCheck() {

            if (Page_ClientValidate()) {
                document.getElementById('lblManHeader').style.display = 'none';
                return true;
            }
            else {
                document.getElementById('lblManHeader').style.display = 'inline';
                return false;
            }
        }
        function setValues() {
            var msg = document.getElementById('hidLOB_MSG').value;
            document.getElementById('LOBmsg').innerHTML = '';
            document.getElementById('LOBmsg').style.display = 'none'
            if (document.getElementById('btnSave') != null)
                document.getElementById('btnSave').disabled = false;
            document.getElementById('hidPOLICY_LOB').value = document.getElementById('cmbPOLICY_LOB').value;

            if (document.getElementById('cmbPOLICY_LOB').value == "") {
                document.getElementById('cmbPOLICY_SUBLOB').innerHTML = "";
                document.getElementById('cmbINSTALL_PLAN_ID').innerHTML = "";
                document.getElementById('cmbAPP_TERMS').innerHTML = "";
                document.getElementById('cmbBILL_TYPE_ID').innerHTML = "";
            }
            else {
                if (parseInt(document.getElementById('hidPOLICY_LOB').value) > 8)
                    GetValues(document.getElementById('cmbPOLICY_LOB').value);
                else {
                    document.getElementById('LOBmsg').innerHTML = msg; //'Sorry !! this product is not implimented in Quick App';
                    document.getElementById('LOBmsg').style.display = 'inline'
                    if (document.getElementById('btnSave') != null)
                        document.getElementById('btnSave').disabled = true;

                }
               
                    SetMarineCargoCaptions();
                
            }

            //document.getElementById('txtAPP_EFFECTIVE_DATE').value = "";
            
                if (parseInt(document.getElementById('hidPOLICY_LOB').value) == 13) {
                     document.getElementById('txtAPP_EXPIRATION_DATE').value = "";
            }
        }
        function SetMarineCargoCaptions() {
            
            if (parseInt(document.getElementById('hidPOLICY_LOB').value) == 13) {
                document.getElementById('capAPP_EFFECTIVE_DATE').innerHTML = "Voyage From Date";
                document.getElementById('capAPP_EXPIRATION_DATE').innerHTML = "Voyage To Date";
                document.getElementById('capAPP_TERMS').style.display = 'none';
                document.getElementById('cmbAPP_TERMS').style.display = 'none';
                document.getElementById('spnAPP_TERMS').style.display = 'none';
                document.getElementById('cmbAPP_TERMS').innerHTML = "0";
                ValidatorEnable(document.getElementById("rfvAPP_TERMS"), false);
                document.QUICK_QUOTE.txtAPP_EXPIRATION_DATE.readOnly = false;
                document.getElementById('hidAPP_TERMS').value = 0;
                
            }
            else {
                document.getElementById('capAPP_EFFECTIVE_DATE').innerHTML = "Effective Date";
                document.getElementById('capAPP_EXPIRATION_DATE').innerHTML = "Expiration Date";
                document.getElementById('capAPP_TERMS').style.display = 'inline';
                document.getElementById('cmbAPP_TERMS').style.display = 'inline';
                document.getElementById('spnAPP_TERMS').style.display = 'inline';
                ValidatorEnable(document.getElementById("rfvAPP_TERMS"), true);
                document.QUICK_QUOTE.txtAPP_EXPIRATION_DATE.readOnly = true;
            }
                                    
        }
        function GetValues(iLOB_ID) {
            
            if (iLOB_ID != "" && iLOB_ID != "0") {
                var result = QuickQuote.AjaxFetchInfo(iLOB_ID);

                //fillDTCombo(result.value, document.getElementById('cmbPOLICY_SUBLOB'), 'SUB_LOB_ID', 'SUB_LOB_DESC', 0);
                //fillDTCombo(result.value, document.getElementById('cmbBILL_TYPE_ID'), 'LOOKUP_UNIQUE_ID', 'LOOKUP_VALUE_DESC', 1);
                fillDTCombo(result.value, document.getElementById('cmbAPP_TERMS'), 'LOOKUP_VALUE_CODE', 'LOOKUP_VALUE_DESC', 2);

               // document.getElementById('cmbINSTALL_PLAN_ID').innerHTML = "";
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

        function fillBillPlan() {
            if (document.getElementById('cmbAPP_TERMS').value != "") {
                //var result = QuickQuote.GetInstallPlan(document.getElementById('cmbAPP_TERMS').value);
                //fillDTCombo(result.value, document.getElementById('cmbINSTALL_PLAN_ID'), 'INSTALL_PLAN_ID', 'BILLING_PLAN', 0);
                //SetINSTALL_PLAN_ID();
            }
            else {
                //document.getElementById('cmbINSTALL_PLAN_ID').innerHTML = "";
            }
        }

        function HideShowBillingInfo() {
            var ctrl = document.getElementById('cmbBILL_TYPE_ID');
            if (ctrl == null || ctrl.selectedIndex == -1) {

                //document.getElementById('lblINSTALL_PLAN_ID').style.display = "none";

                //EnableValidator('rfvINSTALL_PLAN_ID', false);
                return;
            }
            if (ctrl.options[ctrl.selectedIndex].value == "8460" || ctrl.options[ctrl.selectedIndex].value == "11150"
            || ctrl.options[ctrl.selectedIndex].value == "11278" || ctrl.options[ctrl.selectedIndex].value == mortgageeBill
            || ctrl.options[ctrl.selectedIndex].value == InsuredMortgageeBill) {

                document.getElementById('lblINSTALL_PLAN_ID').style.display = "none";
                document.getElementById('cmbINSTALL_PLAN_ID').style.display = "inline";

                EnableValidator('rfvINSTALL_PLAN_ID', true);
                SetINSTALL_PLAN_ID();
            }
            else {

                document.getElementById('lblINSTALL_PLAN_ID').innerText = "N.A.";
                document.getElementById('lblINSTALL_PLAN_ID').style.display = "inline";

                document.getElementById('cmbINSTALL_PLAN_ID').style.display = "none";
                document.getElementById('hidINSTALL_PLAN_ID').value = "0";

                EnableValidator('rfvINSTALL_PLAN_ID', false);
            }
        }

        function initPage() {
            setfirstTime();
            top.topframe.main1.mousein = false;
            findMouseIn();
            ApplyColor();
            HideShowBillingInfo();
            SetMarineCargoCaptions();
            if (document.getElementById('hidOldData').value != "") {
                SelectComboOption("cmbPOLICY_LOB", document.getElementById('hidPOLICY_LOB').value);
                setValues();
               
                //SelectComboOption("cmbAPP_TERMS", document.getElementById('hidAPP_TERMS').value); commented by kuldeep on 21-feb-2012 as terms are not getting filled
                fillBillPlan();
                populateFormData(document.getElementById('hidOldData').value, QUICK_QUOTE);
                //Added by kuldeep for terms
                document.getElementById('hidAPP_TERMS').value = Math.round(parseInt(document.getElementById('hidAPP_TERMS').value) / 30);

                SelectComboOption("cmbAPP_TERMS", document.getElementById('hidAPP_TERMS').value);
                HideShowBillingInfo();
                document.getElementById('div_qapp').style.display = 'block';
                if (document.getElementById('hidCUSTOMER_ID').value != "-100") {
                    document.getElementById('spnCustomerIndex').style.display = 'none';
                    document.getElementById('div_customer').style.display = 'block';
                    if (document.getElementById('btnMakeApp')) {
                        document.getElementById('btnMakeApp').disabled = false;
                    }

                }
                else {
                    document.getElementById('spnCustomerIndex').style.display = 'inline';
                    document.getElementById('div_customer').style.display = 'none';
//                    if (document.getElementById('btnMakeApp'))
//                        document.getElementById('btnMakeApp').disabled = false;//Change by Kuldeep to attach with new customer

                }
                if (document.getElementById('hidAPP_STATUS').value != "QAPP") {
                    document.getElementById('btnApplication').style.display = 'block';
                    document.getElementById('div_appno').style.display = 'block';


                }
                else {
                    document.getElementById('btnApplication').style.display = 'none';
                    document.getElementById('div_appno').style.display = 'none';

                }

                document.getElementById('spnRate').style.display = 'inline';
            }
            else {
                document.getElementById('spnRate').style.display = 'none';
                document.getElementById('btnApplication').style.display = 'none';
            }
        }

        function setDefaultPlan() {
            if (document.getElementById('cmbAPP_TERMS').value != "") {
                var result = QuickQuote.GetDefaultInstallmentPlan(document.getElementById('cmbAPP_TERMS').value);
                if (result != null) {
                    SelectComboOption("cmbINSTALL_PLAN_ID", result.value);
                    SetINSTALL_PLAN_ID();
                }
            }
        }

        function setExpDate() {
            if (document.getElementById('cmbAPP_TERMS').value != "" && document.getElementById('txtAPP_EFFECTIVE_DATE').value != "") {
                var result = QuickQuote.GetExpDate(document.getElementById('cmbAPP_TERMS').value, document.getElementById('txtAPP_EFFECTIVE_DATE').value);
                if (result != null) {
                    document.getElementById('txtAPP_EXPIRATION_DATE').value = result.value;
                }
                else {
                    document.getElementById('txtAPP_EXPIRATION_DATE').value = "";
                }
            }
            else {
                document.getElementById('txtAPP_EXPIRATION_DATE').value = "";
            }
        }

        function SetPOLICY_SUBLOB() {
            if (document.getElementById('cmbPOLICY_SUBLOB').value != "") {
                document.getElementById('hidPOLICY_SUBLOB').value = document.getElementById('cmbPOLICY_SUBLOB').value;
            }
        }

        function SetAPP_TERMS() {
            
            if (document.getElementById('cmbAPP_TERMS').value != "") {
                document.getElementById('hidAPP_TERMS').value = document.getElementById('cmbAPP_TERMS').value;
            }
        }

        function SetBILL_TYPE_ID() {
            if (document.getElementById('cmbBILL_TYPE_ID').value != "") {
                document.getElementById('hidBILL_TYPE_ID').value = document.getElementById('cmbBILL_TYPE_ID').value;
            }
        }

        function SetINSTALL_PLAN_ID() {
            if (document.getElementById('cmbINSTALL_PLAN_ID').value != "") {
                document.getElementById('hidINSTALL_PLAN_ID').value = document.getElementById('cmbINSTALL_PLAN_ID').value;
            }
        }
        function SetDIV_ID_DEPT_ID_PC_ID() {
            if (document.getElementById('cmbDIV_ID_DEPT_ID_PC_ID').value != "") {
                document.getElementById('hidDIV_ID_DEPT_ID_PC_ID').value = document.getElementById('cmbDIV_ID_DEPT_ID_PC_ID').value;
            }
        }
        function SetExpiryDateForMarine() {
            if (document.getElementById('txtAPP_EXPIRATION_DATE').value != "") {
                document.getElementById('hidAPP_EXPIRATION_DATE').value = document.getElementById('txtAPP_EXPIRATION_DATE').value;
            }
        }

        function SetPOLICY_CURRENCY() {
            if (document.getElementById('cmbINSTALL_PLAN_ID').value != "") {
                document.getElementById('hidINSTALL_PLAN_ID').value = document.getElementById('cmbINSTALL_PLAN_ID').value;
            }

        }      
    </script>
</head>
<body onload="javascript:initPage();">
    <webcontrol:Menu id="bottomMenu" runat="server">
    </webcontrol:Menu>
    <div id="bodyHeight" class="pageContent" style="height: 100%; vertical-align:top">
    <form id="QUICK_QUOTE" runat="server" method="post">
    <table id="QuickQuoteInfo" border="0" width="95%" align="center">
        <tbody>
            <tr>
                <td colspan="3" align="left" valign="top">
                    <asp:Menu ID="QuickAppMenu" runat="server" DynamicHorizontalOffset="2"
                        Font-Names="Verdana" Font-Size="0.8em" ForeColor="#0089CF" StaticSubMenuIndent="10px"
                        BackColor="White" OnMenuItemDataBound="QuickAppMenu_MenuItemDataBound" StaticDisplayLevels="5"
                        Orientation="Horizontal" StaticEnableDefaultPopOutImage="true"  >
                        <StaticSelectedStyle ForeColor="Chocolate" />
                        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <DynamicMenuStyle BackColor="#fefefe" />
                        <DynamicSelectedStyle BackColor="#0683c7" ForeColor="White" />
                        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        
                    </asp:Menu>
                    <asp:XmlDataSource ID="XmlDataSourceMenu" runat="server" DataFile="~/Policies/Aspx/QuickQuote.xml">
                    </asp:XmlDataSource>
                </td>
                <%--<td colspan="1" align="right">
                        <cmsb:CmsButton ID="btnNext" CssClass="clsbutton" Text="Next" runat="server" OnClick="btnNext_Click"
                            CausesValidation="false" Visible="false" />
                </td>--%>
            </tr>
            <tr>
                <td colspan="3" align="right">
                        <cmsb:CmsButton ID="btnNext" CssClass="clsbutton" Text="Next" runat="server" OnClick="btnNext_Click"
                            CausesValidation="false" Visible="false" />
                </td>
            </tr>
            <tr>
                <td class="headereffectCenter" colspan="3">
                    <asp:Label ID="lblHeader" runat="server">QuickQuote Information</asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Label ID="lblMessage" runat="server" CssClass="errmsg"></asp:Label>
                    <asp:Label ID="LOBmsg" runat="server" CssClass="errmsg"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="pageHeader" colspan="3">
                    <asp:Label ID="lblManHeader" runat="server" Style="display: none">Please note that all fields marked with * are mandatory</asp:Label>
                </td>
            </tr>
            <tr class="midcolora">
                
               <td class="midcolora" id="td_qapp" runat="server" > 
                <div  id="div_qapp" style="display: none" runat="server" >
                    <asp:Label ID="capQApp" runat="server">QQ#:</strong></asp:Label><br />
                    <asp:Label ID="capQApp_No" runat="server"></asp:Label>
                </div>
               </td>
               
               <td id="td_customer" runat="server" class="midcolora">
                
                   <div id="div_customer" runat="server" style="display: none">
                    <asp:Label ID="capCustomer" runat="server"><b>Customer:</b></asp:Label><br />
                    <asp:Label ID="capCustomer_Name" runat="server"></asp:Label>
                   </div>
               </td>
                 <td  id="td_appno" runat="server"  class="midcolora"> 
                    <div id="div_appno" runat="server" style="display: none">
                    <asp:Label ID="capApp" runat="server"><b>Application No:</b></asp:Label><br />
                    <asp:Label ID="capApp_No" runat="server"></asp:Label></div></td>
                    
                </tr>                    
             
             <tr>
                    <td class="midcolora" style="width: 40%">
                        <asp:Label ID="capPOLICY_LOB" runat="server">Line of Business</asp:Label><span class="mandatory">*</span>
                        <br />
                        <asp:DropDownList ID="cmbPOLICY_LOB" runat="server" onchange="setValues();">
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvPOLICY_LOB" runat="server" Display="Dynamic" ControlToValidate="cmbPOLICY_LOB"
                            ErrorMessage="Please select product" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                    <td class="midcolora" style="width: 30%">
                       <%-- <asp:Label ID="capPOLICY_SUBLOB" runat="server">Line of Business</asp:Label><span
                            class="mandatory">*</span>
                        <br />
                        <asp:DropDownList ID="cmbPOLICY_SUBLOB" onfocus="SelectComboIndex('cmbPOLICY_SUBLOB')"
                            runat="server" onchange="javascript:SetPOLICY_SUBLOB();">
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvPOLICY_SUBLOB" runat="server" Display="Dynamic"
                            ControlToValidate="cmbPOLICY_SUBLOB" ErrorMessage="Please select lob" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td class="midcolora" style="width: 30%">
                        <asp:Label ID="capPOLICY_CURRENCY" runat="server">Policy Currency</asp:Label><span
                            class="mandatory">*</span>
                        <br />
                        <asp:DropDownList ID="cmbPOLICY_CURRENCY" runat="server" onfocus="SelectComboIndex('cmbPOLICY_CURRENCY')">
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvPOLICY_CURRENCY" runat="server" Display="Dynamic"
                            ControlToValidate="cmbPOLICY_CURRENCY" ErrorMessage="Please select Policy Currency"></asp:RequiredFieldValidator>
                    </td>
                </tr>  
                
                <tr>
                    <td class="midcolora" style="width: 40%">
                        <asp:Label ID="capAPP_TERMS" runat="server">Policy Term</asp:Label><span class="mandatory" id="spnAPP_TERMS" runat="server">*</span>
                        <br />
                        <asp:DropDownList ID="cmbAPP_TERMS" runat="server" onchange="javascript:SetAPP_TERMS();setExpDate();">
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvAPP_TERMS" runat="server" Display="Dynamic" ControlToValidate="cmbAPP_TERMS"
                            ErrorMessage="Please select policy term" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                    <td class="midcolora" style="width: 30%">
                        <asp:Label ID="capAPP_EFFECTIVE_DATE" runat="server">Effective Date</asp:Label><span
                            class="mandatory">*</span>
                        <br />
                        <asp:TextBox ID="txtAPP_EFFECTIVE_DATE" runat="server" MaxLength="10" size="12"></asp:TextBox>
                        <asp:HyperLink ID="hlkCalandarDate" runat="server" CssClass="HotSpot">
                            <asp:Image ID="imgAPP_EFFECTIVE_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                Style="vertical-align: middle"></asp:Image>
                        </asp:HyperLink>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvAPP_EFFECTIVE_DATE" runat="server" Display="Dynamic"
                            ControlToValidate="txtAPP_EFFECTIVE_DATE" ErrorMessage="please enter effective date"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revAPP_EFFECTIVE_DATE" runat="server" Display="Dynamic"
                            ControlToValidate="txtAPP_EFFECTIVE_DATE"></asp:RegularExpressionValidator>
                    </td>
                    <td class="midcolora" style="width: 30%">
                        <asp:Label ID="capAPP_EXPIRATION_DATE" runat="server">Expiration Date</asp:Label><%--<span
                            class="mandatory">*</span>--%>
                        <br />
                        <asp:TextBox ID="txtAPP_EXPIRATION_DATE" runat="server" ReadOnly="True" MaxLength="10"
                            size="12" onchange="javascript:SetExpiryDateForMarine();"></asp:TextBox>
                              <asp:HyperLink ID="hlkAPP_EXPIRATION_DATE" runat="server" CssClass="HotSpot">
                            <asp:Image ID="imgAPP_EXPIRATION_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                Style="vertical-align: middle"></asp:Image>
                        </asp:HyperLink>
                        <br />
                         <%--<asp:RequiredFieldValidator ID="rfvAPP_EXPIRATION_DATE" runat="server" Display="Dynamic"
                            ControlToValidate="txtAPP_EXPIRATION_DATE" ErrorMessage="Please enter date."
                           ></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="revAPP_EXPIRATION_DATE" runat="server" Display="Dynamic"
                            ControlToValidate="txtAPP_EXPIRATION_DATE" ErrorMessage="Please enter valid date."></asp:RegularExpressionValidator>
                    </td>
                </tr>
                
                <tr id="trBill" runat="server" visible="false">
                    <td class="midcolora" style="width: 40%">
                        <asp:Label ID="capBILL_TYPE_ID" runat="server">Bill Type</asp:Label><span class="mandatory">*</span>
                        <br />
                        <asp:DropDownList ID="cmbBILL_TYPE_ID" onfocus="SelectComboIndex('cmbBILL_TYPE_ID')"
                            runat="server" onchange="javascript:SetBILL_TYPE_ID();fillBillPlan();setDefaultPlan();HideShowBillingInfo();">
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvBILL_TYPE" runat="server" Display="Dynamic" ControlToValidate="cmbBILL_TYPE_ID"
                            ErrorMessage="Please select bill type" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                    <td class="midcolora" style="width: 30%">
                        <asp:Label ID="capINSTALL_PLAN_ID" runat="server">Billing Plan</asp:Label><span class="mandatory">*</span>
                        <br />
                        <asp:Label ID="lblINSTALL_PLAN_ID" runat="server" CssClass="LabelFont" Style="display: none">N.A.</asp:Label>
                        <asp:DropDownList ID="cmbINSTALL_PLAN_ID" onfocus="SelectComboIndex('cmbINSTALL_PLAN_ID')"
                            runat="server" onchange="javascript:SetINSTALL_PLAN_ID();">
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvINSTALL_PLAN_ID" runat="server" Display="Dynamic"
                            ControlToValidate="cmbINSTALL_PLAN_ID" ErrorMessage="Please select billing plan"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                   <td class="midcolora" style="width: 30%">&nbsp;</td>
                </tr> 
                
                <tr>
                    <td class="midcolora" colspan="2">
                        <span id="spnCustomerIndex" runat="server" style="cursor: hand" onclick="javascript:OpenCustLookup();">
                            <asp:Label ID="lblCustomerIndex" runat="server"></asp:Label>
                            <img id="imgCustomerIndex" alt="" src="../../cmsweb/images/selecticon.gif" />
                        </span>&nbsp; 
                        <span id="spnNewCustomerIndex" runat="server" style="cursor: hand" onclick="javascript:OpenNewCustomer();">
                            <asp:Label ID="lblNewCustomerIndex" runat="server" style="display:none"></asp:Label>
                            <img id="imgNewCustomerIndex" alt="" src="../../cmsweb/images/selecticon.gif" style="display:none"/>
                        </span>&nbsp; 
                        <span id="spnRate" runat="server" style="cursor: hand" onclick="javascript:ShowRate();">
                            <asp:Label ID="lblRate" runat="server"></asp:Label>
                            <img id="imgrate" alt="" src="../../cmsweb/Images/quote.gif" />
                        </span>
                    </td>
                    <td class="midcolorr" colspan="1">
                        <cmsb:CmsButton ID="btnApplication" CssClass="clsbutton" Text="Go To Application"
                            runat="server" OnClick="btnGo_Application" />
                        <cmsb:CmsButton ID="btnMakeApp" CssClass="clsbutton" Text="Make Application"
                            runat="server" CausesValidation="true" OnClick="btnMakeApp_Click" />
                        <cmsb:CmsButton ID="btnSave" CssClass="clsbutton" Text="Save" runat="server" CausesValidation="true"
                            OnClick="btnSave_Click" />
                    </td>
                </tr>
                <tr>
                    <td id="tdHidden" runat="server">
                        <input id="hidCUSTOMER_ID" runat="server" type="hidden" value="-100" />
                        <input id="hidPOLICY_ID" runat="server" type="hidden" value="0" />
                        <input id="hidPOLICY_VERSION_ID" runat="server" type="hidden" value="0" />
                        <input id="hidPOLICY_LOB" runat="server" type="hidden" value="" />
                        <input id="hidPOLICY_SUBLOB" runat="server" type="hidden" value="" />
                        <input id="hidAPP_TERMS" runat="server" type="hidden" value="" />
                        <input id="hidBILL_TYPE_ID" runat="server" type="hidden" value="0" />
                        <input id="hidINSTALL_PLAN_ID" runat="server" type="hidden" value="0" />
                        <input id="hidRiskUrl" runat="server" type="hidden" value="" />
                        <input id="hidOldData" runat="server" type="hidden" value="" />
                        <input id="hidAttachUrl" runat="server" type="hidden" value="" />
                        <input id="hidAPP_STATUS" runat="server" type="hidden" value="" />
                        <input id="hidDIV_ID_DEPT_ID_PC_ID" runat="server" type="hidden" value="" />
                        <input id="hidLangCulture" type="hidden" name="hidLangCulture" runat="server" />
                        <input id="hidappnumber" type="hidden" name="hidappnumber" runat="server" />
                        <input id="hidLOB_MSG" type="hidden" name="hidLOB_MSG" runat="server" />
                        <input id="hidQQ_ID" type="hidden" name="hidQQ_ID" runat="server" />
                        <input id="hidAPP_EXPIRATION_DATE" type="hidden" name="hidAPP_EXPIRATION_DATE" runat="server" />
                        
                    </td>
                </tr>            
            </tbody>
    </table>    
    </form>
    </div>
    
    <script type="text/javascript" language="javascript">
        function OpenCustLookup() {
            var url = document.getElementById('hidAttachUrl').value;
            //OpenLookup(url, '', '', '', '', 'QuickApp', document.getElementById('lblCustomerIndex').innerText, '');
            OpenLookupWithFunction(url, 'CUSTOMER_ID', 'CUSTOMER_ID', 'hidCUSTOMER_ID', '', 'QuickApp', document.getElementById('lblCustomerIndex').innerText, '', 'attCustomer()');
        }

        //By kuldeep to attach with new customer tfs 2005
        function OpenNewCustomer() {
            //var url = document.getElementById('/cms/clients/aspx/AddCustomer.aspx').value;
            //OpenLookup(url, '', '', '', '', 'QuickApp', document.getElementById('lblCustomerIndex').innerText, '');
            //OpenLookupWithFunction(url, 'CUSTOMER_ID', 'CUSTOMER_ID', 'hidCUSTOMER_ID', '', 'QuickApp', document.getElementById('lblCustomerIndex').innerText, '', 'attCustomer()');
      
            var win = window.open("/CMS/client/Aspx/AddCustomer.aspx", 'review', 'height=600, width=600,status= no, resizable=yes, scrollbars=Yes, toolbar=no,location=no,menubar=no');
            // window.open('/cms/clients/aspx/AddCustomer.aspx?CalledFrom=Quote', 'Attach to New Customer', 'height=600, width=600,status= no, resizable=yes, scrollbars=no, toolbar=no,location=no,menubar=no');
            
            /*DataValueField=' + DataValueField + '&DataTextField=' +
						DataTextField + '&DataValueFieldID=' + DataValueFieldID +
						'&DataTextFieldID=' + DataTextFieldID + '&LookupCode=' + LookupCode +
						'&Args=' + Args + '&JSFunction=' + JSFunction,
						'review', 'height=600, width=600,status= no, resizable=yes, scrollbars=no, toolbar=no,location=no,menubar=no');
                        */
        }

        function ShowRate() {

            if (refWindow != null) {
                refWindow.close();
            }

            var url = "/cms/application/Aspx/QuoteGenerator.aspx?CUSTOMER_ID=" + document.getElementById('hidCUSTOMER_ID').value + "&APP_ID="
                + document.getElementById('hidPOLICY_ID').value + "&APP_VERSION_ID=" + document.getElementById('hidPOLICY_VERSION_ID').value + "&LOB_ID="
                + document.getElementById('hidPOLICY_LOB').value + "&POLICY_ID="
                + document.getElementById('hidPOLICY_ID').value + "&POLICY_VERSION_ID=" + document.getElementById('hidPOLICY_VERSION_ID').value + "&CALLEDFROM=QAPP";
            refWindow = window.open(url, "Quote", "resizable=yes,scrollbars=yes,width=800,height=500,left=150,top=50");
        }

        function attCustomer() {
            
            if (document.getElementById('hidCUSTOMER_ID').value != "" &&
                document.getElementById('hidCUSTOMER_ID').value != "0" &&
                document.getElementById('hidCUSTOMER_ID').value != "-100") {
                var result = QuickQuote.AjaxAttachCustomer(document.getElementById('hidCUSTOMER_ID').value);

                if (result.value != '') {

                    document.getElementById('spnCustomerIndex').style.display = 'none';
                    document.getElementById('btnMakeApp').disabled = false;

                    var arr = result.value.split('~')
                    document.getElementById('hidCUSTOMER_ID').value = arr[0];
                    document.getElementById('hidPOLICY_ID').value = arr[1];
                    document.getElementById('hidPOLICY_VERSION_ID').value = "1";
                    document.getElementById('btnApplication').style.display = 'none';
                    document.getElementById('div_customer').style.display = 'block';
                    document.getElementById('capCustomer_Name').innerText = arr[3];

                    alert('<%= objResourceManager.GetString("lblAttachSuccess") %>');
                }
                else {
                    alert('<%= objResourceManager.GetString("lblAttachFail") %>');
                }
            }
        }
    </script>
</body>
</html>

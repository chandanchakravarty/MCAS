<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPolicyLocations.aspx.cs" Inherits="Cms.Policies.Aspx.AddPolicyLocations"  validateRequest=false%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
        <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >
        <script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		 <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script> 
		  <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
    <script language="javascript" type="text/javascript">

        function SetBlankofExt() {
            if (trim($("#txtPHONE_NUMBER").val())=='') {
                $("#txtEXT").val('');
            }
         }
        

        function fillstateFromCountry() {
            
            GlobalError = true;
            var CountryID = document.getElementById('cmbLOC_COUNTRY').options[document.getElementById('cmbLOC_COUNTRY').selectedIndex].value;
            AddPolicyLocations.AjaxFillState(CountryID, fillState);
          
            if (GlobalError) 
            {
                return false;
            }
            else 
            {
                return true;
            }
        }
        
        function ResetForm() 
        {
            document.POL_LOCATIONS.reset();
            ChangeColor();
            return false;
        }
        function fillState(Result) 
        {
            if (Result.error)
            {
                var xfaultcode = Result.errorDetail.code;
                var xfaultstring = Result.errorDetail.string;
                var xfaultsoap = Result.errorDetail.raw;
            }
            else {
            
                var statesList = document.getElementById("cmbLOC_STATE");
                statesList.options.length = 0;
                oOption = document.createElement("option");
                oOption.value = "";
                oOption.text = "";
                statesList.add(oOption);
                ds = Result.value;
                if (ds != null && typeof (ds) == "object" && ds.Tables != null) 
                {
                    for (var i = 0; i < ds.Tables[0].Rows.length; ++i) 
                    {
                        statesList.options[statesList.options.length] = new Option(ds.Tables[0].Rows[i]["STATE_NAME"], ds.Tables[0].Rows[i]["STATE_ID"]);
                    }
                }
                //Added by Pradeep for itrack - 921
                if (document.getElementById('hidSTATE_ID')) {
                    if (document.getElementById('hidSTATE_ID').value != "") {
                        for (var i = 0; i < statesList.options.length; i++) {
                            if (statesList.options[i].value == document.getElementById('hidSTATE_ID').value) {
                                statesList.options[i].selected = true;
                            }
                        }
                    }
                }
                //Added Till here 
            }

            return false;
        }

        function setStateID() {

            var CmbState = document.getElementById('cmbCUSTOMER_STATE');
            if (CmbState == null)
                return;
            if (CmbState.selectedIndex != -1) {
                document.getElementById('hidSTATE_ID').value = CmbState.options[CmbState.selectedIndex].value;

            }

        }
        function GetZipForState(flag)
         {
            GlobalError = true;
            if (document.getElementById('cmbLOC_STATE').value == 14 || document.getElementById('cmbLOC_STATE').value == 22) {
                if (document.getElementById('txtLOC_ZIP').value != "") 
                    {
                        var intStateID = document.getElementById('cmbLOC_STATE').options[document.getElementById('cmbLOC_STATE').options.selectedIndex].value;
                        var strZipID = document.getElementById('txtLOC_ZIP').value;
                        var result = PolicyAddLocation.AjaxFetchZipForState(intStateID, strZipID);
                        AjaxCallFunction_CallBack(result);

                        if (GlobalError) 
                            {
                                document.getElementById('csvLOC_ZIP').setAttribute('enabled', true);
                                document.getElementById('csvLOC_ZIP').setAttribute('isValid', true);
                                document.getElementById('csvLOC_ZIP').style.display = 'inline';
                                return false;
                            }
                        else 
                            {
                                document.getElementById('csvLOC_ZIP').setAttribute('enabled', false);
                                document.getElementById('csvLOC_ZIP').setAttribute('isValid', false);
                                document.getElementById('csvLOC_ZIP').style.display = 'none';
                                return true;
                            }

                    }
                return false;
            }
            else
                return true;
        }

        function AjaxCallFunction_CallBack(response)
         {
             if (document.getElementById('cmbLOC_STATE').value == 14 || document.getElementById('cmbLOC_STATE').value == 22)
             {
                 if (document.getElementById('txtLOC_ZIP').value != "") 
                    {
                        handleResults(response);
                        if (GlobalError) 
                            {
                                return false;
                            }
                        else 
                            {
                                return true;
                            }
                    }
                return false;
            }
            else
                return true;
        }
        
        /////EMP ZIP AJAX////////////////
        function handleResults(res) 
        {
            if (!res.error) 
            {
               if (res.value != "" && res.value != null)
                   {
                      GlobalError = false;
                   }
                   else 
                   {
                      GlobalError = true;
                   }
            }
            else 
            {
              GlobalError = true;
            }
        }

        function AddData() {
            document.getElementById('hidLOCATION_ID').value = 'New';
          //  document.getElementById('txtCAL_NUM').value = '';
            document.getElementById('txtLOC_NUM').value = '';
            document.getElementById('txtNAME').value = '';
            document.getElementById('txtLOC_ZIP').value = '';
            document.getElementById('txtLOC_ADD1').value = '';
            document.getElementById('txtNUMBER').value = '';
            document.getElementById('txtLOC_ADD2').value = '';
            document.getElementById('txtDISTRICT').value = '';
            document.getElementById('txtLOC_CITY').value = "";
            document.getElementById('txtPHONE_NUMBER').value = ''
            document.getElementById('txtEXT').value = '';
            document.getElementById('txtFAX_NUMBER').value = '';
            document.getElementById('txtCATEGORY').value = ''; ;
            document.getElementById('cmbLOC_STATE').options.selectedIndex = 1;
            //document.getElementById('cmbLOC_COUNTRY').options.selectedIndex = 1;
            document.getElementById('cmbACTIVITY_TYPE').options.selectedIndex = 1;
            document.getElementById('cmbCONSTRUCTION').options.selectedIndex = 1;
            document.getElementById('cmbOCCUPIED').options.selectedIndex = 1;
            //document.getElementById('cmbLOC_STATE').options.selectedIndex = 1;

            ChangeColor();
            DisableValidators();

        }

        function populateXML() {
        
            //alert(document.getElementById('hidIS_ACTIVE').value)
            //alert(document.getElementById('hidFormSaved').value)
            if (document.getElementById('hidFormSaved').value == '0') 
            {
                if (document.getElementById('hidLOCATION_ID').value == "") {

                        AddData();
                 }
            }
            return false;
        }

        function SetTabs() {
            if (document.getElementById('hidLOCATION_ID').value != "NEW" && document.getElementById('hidLOCATION_ID').value != "0") {
               
                LocationId = document.getElementById('hidLOCATION_ID').value;
                Url = "ProtectiveDevicesInfo.aspx?LOCATION_ID=" + LocationId + "&";
                this.parent.parent.SetTabSaveStatus(1, 0, 'Y');
                DrawTab(2, this.parent, document.getElementById('hidTabTitle').value, Url);
               
            }
            else {
                RemoveTab(2, this.parent);
            }
        }

        function CheckIfPhoneEmpty() {
            if (document.getElementById('txtPHONE_NUMBER').value == "") {
                document.getElementById('txtEXT').value = ""
                return false;
            }
            else
                return true;
        }
    </script>
      <%--Populate the Customer Address based on the ZipeCode using jQuery ------- Added by Pradeep Kushwaha on 03 Jun 2010--%>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            $("#cmbLOCATION").change(function () {
                $("#hidLOCATION").val($("#cmbLOCATION option:selected").text());
                $("#hidLocation_Value").val($("#cmbLOCATION option:selected").val());
            });
            //added for tfs# 2701 by pradeep
            $("#btnSave").click(function () {
                if ($('#hidValidateLoc').val() == "1") {
                    $('#capLocValidationMessage').text($('#hidLocationNumberMsg').val());
                    return false;
                }
                else
                    $('#capLocValidationMessage').text('');

            });
            $('body').bind('keydown', function (event) {
                if (event.keyCode == '13') {
                    $("#btnSave").trigger('click');
                    return false;
                }
            });
            //Added till here 

            $("#txtLOC_ZIP").change(function () {
                if (trim($('#txtLOC_ZIP').val()) != '') {
                    var ZIPCODE = $("#txtLOC_ZIP").val();
                    var COUNTRYID = "5";
                    PageMethod("GetCustomerAddressDetailsUsingZipeCode", ["ZIPCODE", ZIPCODE, "COUNTRYID", COUNTRYID], AjaxSucceeded, AjaxFailed); //With parameters
                }
                else {
                    $("#txtLOC_ADD1").val('');
                    $("#txtLOC_ADD2").val('');
                    $("#txtDISTRICT").val('');
                    $("#txtLOC_CITY").val('');
                }
            });

            //Added By Pradeep On 02-July-2010 to check the Loaction number exists or no
            $("#txtLOC_NUM").change(function () {

                if ($("#txtLOC_NUM").val() != $("#hidOLD_Location_num").val()) {
                    if (trim($('#txtLOC_NUM').val()) != '') {

                        var CUSTOMER_ID = $('#hidCUSTOMER_ID').val();
                        var LOCATION_NUMBER = $('#txtLOC_NUM').val();
                        var flag = "2"; //Called to check whether LOCATION_NUMBER exists or not
                        PageMethod("GetMaxIdOfLocationNumber", ["CUSTOMER_ID", CUSTOMER_ID, "LOCATION_NUMBER", LOCATION_NUMBER, "flag", flag], AjaxSucceeded, AjaxFailed); //With parameters
                    }

                }
            });
            //Added by Pradeep for itrack#1152 / TFS# 2598
            $("#cmbACTIVITY_TYPE").change(function () {
                
                if ($("#cmbACTIVITY_TYPE option:selected").val() != '') {
                    var _rubrica = $("#cmbACTIVITY_TYPE option:selected").val().split('^');
                    $("#hidACTIVITY_TYPE").val(_rubrica[0].toString());
                    var result = AddPolicyLocations.AjaxGetOccupied(_rubrica[1].toString());
                    fillDTCombo(result.value, document.getElementById('cmbOCCUPIED'), 'OCCUPIED_ID', 'OCCUPIED_AS', 0);
                }
            });

            $("#cmbOCCUPIED").change(function () {
                if ($("#cmbOCCUPIED option:selected").val() != '')
                    $("#hidOCCUPIED").val($("#cmbOCCUPIED option:selected").val());
            });
            //Added till here 
        });
        //Added by Pradeep for itrack#1152 / TFS# 2598
        function fillDTCombo(objDT, combo, valID, txtDesc, tabIndex) {
            //debugger;
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
        //Added till here 

        function PageMethod(fn, paramArray, successFn, errorFn) {
            var pagePath = window.location.pathname;
            var paramList = '';
            if (paramArray.length > 0) {
                for (var i = 0; i < paramArray.length; i += 2) {
                    if (paramList.length > 0) paramList += ',';
                    paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                    //To check if PageMethod called from Zipecode then add the param value into  hidZipCodeParam ( Hidden Control ) 
                   if (paramArray[i] == "ZIPCODE") {
                    
                        $('#hidZipCodeParam').val(paramArray[i]);
                    }
                    
                }
            }
            paramList = '{' + paramList + '}';
            $.ajax({ type: "POST",url: pagePath + "/" + fn,contentType: "application/json; charset=utf-8",
                data: paramList,dataType: "json",success: successFn,error: errorFn});

        }
        function AjaxSucceeded(result) {
           
            var Addresses = result.d;
            if ($('#hidZipCodeParam').val() == 'ZIPCODE') {
                var Addresse = Addresses.split('^');
                if (result.d != "" && Addresse[1] != 'undefined') {
                    $("#cmbLOC_STATE").val(Addresse[1]);
                   // $("#txtLOC_ZIP").val(Addresse[2]);
                    $("#txtLOC_ADD1").val(Addresse[3] + ' ' + Addresse[4]);
                    $("#txtDISTRICT").val(Addresse[5]);
                    $("#txtLOC_CITY").val(Addresse[6]);
                    $('#hidZipCodeParam').val('');
                }
                else if (document.getElementById('cmbLOC_COUNTRY').options[document.getElementById('cmbLOC_COUNTRY').options.selectedIndex].value == '5') {

                    alert($("#hidZipeCodeVerificationMsg").val());
                    $("#txtLOC_ADD1").val('');
                    $("#txtLOC_ADD2").val('');
                    $("#txtDISTRICT").val('');
                    $("#txtLOC_CITY").val('');
                }
            }
            else {
               
                var numbers = result.d;
                if (numbers != '') {
                    var number = numbers.split(',');
                    
                    //if LOC_NUM exists then return -5 
                    if ((number[0] == "-5") && (number[2] == "2")) {
                        //Location # exist
                       /* document.getElementById("rfvtxtLOC_NUM").setAttribute('enabled', false);
                        document.getElementById("rfvtxtLOC_NUM").setAttribute('isValid', true);
                        document.getElementById("rfvtxtLOC_NUM").style.display = "inline";
                        $('#rfvtxtLOC_NUM').text($('#hidLocationNumberMsg').val());*/

                        $('#hidValidateLoc').val("1"); //added for tfs#2701
                       
                    }
                    else if ((number[0] == "-6") && (number[2] == "2")) //--if LOC_NUM exists then return -6  
                    {
                        
                        //Location # not exist
                        /*document.getElementById("rfvtxtLOC_NUM").setAttribute('enabled', false);
                        document.getElementById("rfvtxtLOC_NUM").setAttribute('isValid', true);
                        document.getElementById("rfvtxtLOC_NUM").style.display = "none";*/

                        $('#hidValidateLoc').val("0");//added for tfs#2701
                       
                    }
 
                }
                
                
            }
        }
         
        
    function FormatZipCode(vr) {
    
    var vr = new String(vr.toString());
    if (vr != "" && (document.getElementById('cmbLOC_COUNTRY').options[document.getElementById('cmbLOC_COUNTRY').options.selectedIndex].value == '5')) {

        vr = vr.replace(/[-]/g, "");
        num = vr.length;
        if (num == 8 && (document.getElementById('cmbLOC_COUNTRY').options[document.getElementById('cmbLOC_COUNTRY').options.selectedIndex].value == '5')) {
            vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
            document.getElementById('revLOC_ZIP').setAttribute('enabled', false);
           
        }

            }
            return vr;
        }

        function zipcodeval() {

            if (document.getElementById('cmbLOC_COUNTRY').options[document.getElementById('cmbLOC_COUNTRY').options.selectedIndex].value == '6') {
                document.getElementById('revLOC_ZIP').setAttribute('enabled', false);
            }
        }

        function zipcodeval1() {

            if (document.getElementById('cmbLOC_COUNTRY').options[document.getElementById('cmbLOC_COUNTRY').options.selectedIndex].value == '5') {
                document.getElementById('revLOC_ZIP').setAttribute('enabled', true);
            }
        } 
        

        function AjaxFailed(result) {
           
            //alert(result.d);
        }
        function CheckSlectedValue() {
            var val = document.getElementById('cmbLOCATION').value;
            if (val != "")
                return true
            else
                return false;        
         }
        
    </script> 
       <%-- End jQuery Implimentation for ZipeCode --%>
</head>
<body leftMargin="0" topMargin="0" <%--oncontextmenu="return false;"--%> onload="populateXML();ApplyColor();ChangeColor();SetTabs();">
  
    <form id="POL_LOCATIONS" runat="server" method="post">
     <p>
        <uc1:addressverification id="AddressVerification1" runat="server">
        </uc1:addressverification></p>
    <table width="100%" class="tableWidthHeader">
   <%-- <tr><td>
                <table width="100%" class="tableWidthHeader">                    
                    <tr>
						<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
					</tr>
				</table></td>
	</tr>--%>
	<tr id="trBody" runat="server">
	<td>
				<table width="100%" class="tableWidthHeader" id="tableBody">
                    <tr>
                        <td class="headereffectCenter" colspan="4">
                            <asp:Label ID="lblHeader" runat="server">Location Details</asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
			            <td class="midcolorc" align="right" colSpan="4">
			                <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
			            </td>
		            </tr>
		            
                    <tr>
                        <td class="pageHeader" colSpan="3">
                        <asp:Label runat="server" ID="capMAN_MSG"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr id="trcmbLOCATION" runat="server" >
                        <td width="33%" class="midcolora" colspan="3">
                        <asp:Label ID="capLOCATION" runat="server"></asp:Label></br>
                        <asp:DropDownList runat="server" ID="cmbLOCATION" width="100%" Height="17px">
                        </asp:DropDownList>
                        <cmsb:cmsbutton class="clsButton" id="btnSelect" runat="server" OnClientClick="javascript:return CheckSlectedValue()"
                                onclick="btnSelect_Click" CausesValidation="false" ></cmsb:cmsbutton>
                        </td>
                        
                    </tr>
                    <tr>
                       <%-- <td class="midcolora"  width="33%">
                           <asp:Label runat="server" ID="capCAL_NUM">
                            </asp:Label><span class="mandatory">*</span>
                            <br />
                            <asp:TextBox runat="server" ID="txtCAL_NUM" onkeypress="MaxLength(this,10)" 
                                onpaste="MaxLength(this,10)" MaxLength="10"></asp:TextBox><br />     
                                 <asp:Regularexpressionvalidator id="revCAL_NUM" runat="server" ControlToValidate="txtCAL_NUM" ErrorMessage="" Display="Dynamic"></asp:Regularexpressionvalidator>                
                            <asp:RequiredFieldValidator ID="rfvtxtCAL_NUM" runat="server" ControlToValidate="txtCAL_NUM"></asp:RequiredFieldValidator>
                          
                        </td>--%>
                        <td class="midcolora"  width="33%">
                            <asp:Label ID="capLOC_NUM" runat="server" >
                            </asp:Label><span class="mandatory">*</span>
                            <br />
                            <%----Modified length 9 to 11 per the tfs#2701 itrack-1366    --%>
                            <asp:TextBox ID="txtLOC_NUM" runat="server" 
                                onkeypress="MaxLength(this,11)" onpaste="MaxLength(this,11)" MaxLength="11"></asp:TextBox>
                            <br />
                            <asp:Label ID="capLocValidationMessage" runat="server" ForeColor="Red"    ></asp:Label><%--added for tfs#2701 by pradeep--%>
                            <asp:RequiredFieldValidator ID="rfvtxtLOC_NUM" runat="server" ControlToValidate="txtLOC_NUM" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:Regularexpressionvalidator id="revLOC_NUM" runat="server" ControlToValidate="txtLOC_NUM" ErrorMessage="" Display="Dynamic"></asp:Regularexpressionvalidator>
                            
                            <%--<asp:CustomValidator ID="csvLOC_NUM" ControlToValidate="txtLOC_NUM" Display="Dynamic" runat="server"  ></asp:CustomValidator> --%>
                        </td>
                        <td class="midcolora" width="33%">
                            <asp:Label ID="capNAME" runat="server" Text="Building Name:">
                            </asp:Label><span class="mandatory" id = "spnNAME" runat ="server">*</span>
                            <br />
                            <asp:TextBox ID="txtNAME" runat="server" onkeypress="MaxLength(this,30)" 
                                onpaste="MaxLength(this,30)" MaxLength="30"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvNAME" runat="server" ControlToValidate="txtNAME"></asp:RequiredFieldValidator>
                        </td>
                        
                        <td width="34%" class="midcolora" colspan="1">
                        </br>
                            <asp:CheckBox ID="chkIsBillingAddress"   runat="server" /><asp:Label ID="capIsBillingAddress" runat="server" ></asp:Label>
                        </td>
                        
                        
                    </tr>
                    <%--Added by Pradeep - Itrack 921--%>
                    <tr id="trPullCustomerAddress">
								<td class="midcolora">
									<asp:label id="capPullCustomerAddress" runat="server" ></asp:label>
								</td>
								<td colspan="2" class="midcolora">
									<cmsb:cmsbutton class="clsButton" id="btnPullCustomerAddress" runat="server" Text="Pull Customer Address"></cmsb:cmsbutton>
								</td>
					</tr>
                    <%--Added till here--%>
                    <tr>
                        <td class="midcolora" width="33%">
                            <asp:Label runat="server" ID="capLOC_ZIP"></asp:Label><span id = "spnLOC_ZIP" class="mandatory" runat ="server">*</span><br />
                            <asp:TextBox ID="txtLOC_ZIP" runat="server"  OnBlur="this.value=FormatZipCode(this.value);ValidatorOnChange();zipcodeval();zipcodeval1();" onkeypress="MaxLength(this,8)" onpaste="MaxLength(this,8)" ></asp:TextBox>
                            <asp:HyperLink ID="hlkZipLookup" Visible="true" runat="server" CssClass="HotSpot">
                            <asp:Image ID="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom" Visible="true"></asp:Image>
                            </asp:HyperLink><br /><asp:regularexpressionvalidator id="revLOC_ZIP" runat="server" ControlToValidate="txtLOC_ZIP" ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator>
                            <asp:RequiredFieldValidator ID="rfvLOC_ZIP" runat="server" ControlToValidate="txtLOC_ZIP"></asp:RequiredFieldValidator>
                        </td>
                        <td class="midcolora" width="33%">
                            <asp:Label runat="server" ID="capLOC_ADD1" Text="Address:">
                            </asp:Label><span class="mandatory">*</span><br />
                            <asp:TextBox ID="txtLOC_ADD1" runat="server" onkeypress="MaxLength(this,75)" onpaste="MaxLength(this,75)" MaxLength="150"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvLOC_ADD1" runat="server" ControlToValidate="txtLOC_ADD1"></asp:RequiredFieldValidator>
                        </td>
                        <td id="ttdNumber" runat="server" class="midcolora" width="34%">
                            <asp:Label runat="server" ID="capNUMBER" Text="Number:"></asp:Label><%--<span class="mandatory">*</span><br />--%><%--Commented by Aditya for TFS BUG # 1911--%></br>
                            <asp:TextBox ID="txtNUMBER" runat="server" onkeypress="MaxLength(this,50)" onpaste="MaxLength(this,50)" MaxLength="50"></asp:TextBox>
                           <%-- <asp:Regularexpressionvalidator id="revNUMBER" runat="server" ControlToValidate="txtNUMBER" ErrorMessage="" Display="Dynamic"></asp:Regularexpressionvalidator>--%>
                            <%--<asp:RequiredFieldValidator ID="rfvNUMBER" Display="Dynamic" runat="server" ControlToValidate="txtNUMBER"></asp:RequiredFieldValidator>--%> <%--Commented by Aditya for TFS BUG # 1911--%>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="33%">
                            <asp:Label runat="server" ID="capLOC_ADD2" Text="Complement:"></asp:Label><br />
                            <asp:TextBox ID="txtLOC_ADD2" runat="server" onkeypress="MaxLength(this,75)" onpaste="MaxLength(this,75)" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="midcolora" width="33%">
                        <asp:Label runat="server" ID="capDISTRICT" Text="District:"></asp:Label><span id="spnDISTRICT" runat="server" class="mandatory">*</span>
                        <br />
                        <asp:TextBox ID="txtDISTRICT" runat="server" onkeypress="MaxLength(this,50)" onpaste="MaxLength(this,50)" MaxLength="50"></asp:TextBox><br />
                         <asp:RequiredFieldValidator ID="rfvDISTRICT" runat="server" ControlToValidate="txtDISTRICT"></asp:RequiredFieldValidator>
                        </td>
                        <td class="midcolora" width="34%">
                        <asp:Label runat="server" ID="capLOC_CITY" Text="City:"></asp:Label><span id="spnLOC_CITY" runat="server" class="mandatory">*</span>
                        <br />
                        <asp:TextBox ID="txtLOC_CITY" runat="server" onkeypress="MaxLength(this,50)" 
                                onpaste="MaxLength(this,50)" MaxLength="150"></asp:TextBox>  
                        <br />             
                        <asp:requiredfieldvalidator id="rfvLOC_CITY" Runat="server" ControlToValidate="txtLOC_CITY" 
				        Display="Dynamic"></asp:requiredfieldvalidator>
                        </td>
                </tr>
                <tr>
                    <td class="midcolora" width="33%">
                        <asp:Label runat="server" ID="capLOC_COUNTRY" Text="Country:">
                        </asp:Label><span class="mandatory">*</span>
                        <br />
                        <asp:DropDownList runat="server" ID="cmbLOC_COUNTRY" Height="26px" onchange="javascript:fillstateFromCountry();"
                            Width="131px">
                        </asp:DropDownList><br />
                        <asp:requiredfieldvalidator id="rfvLOC_COUNTRY" Runat="server" ControlToValidate="cmbLOC_COUNTRY" 
				        Display="Dynamic"></asp:requiredfieldvalidator>
                    </td>
                    <td class="midcolora" width="33%">
                        <asp:Label runat="server" ID="capLOC_STATE" Text="State:">
                        </asp:Label><span id="spnLOC_STATE" runat="server" class="mandatory">*</span>
                        <br />
                        <asp:DropDownList runat="server" ID="cmbLOC_STATE" Height="16px" Width="131px">
                        <asp:ListItem></asp:ListItem></asp:DropDownList><br />
                        <asp:requiredfieldvalidator id="rfvLOC_STATE" Runat="server" ControlToValidate="cmbLOC_STATE" 
				        Display="Dynamic"></asp:requiredfieldvalidator>
                    </td>
                    <%--<td class="midcolora" width="34%">
                        <asp:Label runat="server" ID="capOCCUPIED" Text="Occupied As:">
                        </asp:Label><span class="mandatory">*</span>
                        <br />
                        <asp:DropDownList runat="server" ID="cmbOCCUPIED" Height="16px" Width="131px">
                        </asp:DropDownList><br />
                        <asp:requiredfieldvalidator id="rfvOCCUPIED" Runat="server" ControlToValidate="cmbOCCUPIED" 
				        Display="Dynamic"></asp:requiredfieldvalidator>
                    </td>--%>
                    <td class="midcolora" width="34%">
                        <asp:Label runat="server" ID="capCONSTRUCTION" Text="Construction:">
                        </asp:Label>
                        <br />
                        <asp:DropDownList runat="server" ID="cmbCONSTRUCTION" Height="16px">
                        </asp:DropDownList>
                    </td>
                   
                </tr>
                <tr>
                    <td class="midcolora" width="33%">
                        <asp:Label runat="server" ID="capPHONE_NUMBER" Text="Phone#:">
                        </asp:Label>
                        <br />
                        <asp:TextBox ID="txtPHONE_NUMBER" runat="server" 
                            onkeypress="MaxLength(this,15)" onpaste="MaxLength(this,15)" MaxLength="20" onblur="FormatBrazilPhone()" onchange="SetBlankofExt();"></asp:TextBox>
                        <br />
                        <asp:regularexpressionvalidator id="revPHONE_NUMBER" runat="server" ControlToValidate="txtPHONE_NUMBER" ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator>
                    </td>
                    <td class="midcolora" width="33%">
                        <asp:Label runat="server" ID="capEXT" Text="Extension:">
                        </asp:Label>
                        <br />
                        <asp:TextBox ID="txtEXT" runat="server" MaxLength="6" 
                            onblur="CheckIfPhoneEmpty();" AutoCompleteType="Disabled"></asp:TextBox>
                        <br />
                        <asp:regularexpressionvalidator id="revEXT" runat="server" ControlToValidate="txtEXT" ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator>
                    </td>
                    <td class="midcolora" width="34%">
                        <asp:Label runat="server" ID="capFAX_NUMBER" Text="Fax#:">
                        </asp:Label>
                        <br />
                        <asp:TextBox ID="txtFAX_NUMBER" runat="server" onkeypress="MaxLength(this,15)" 
                            onpaste="MaxLength(this,15)" MaxLength="20" onblur="FormatBrazilPhone()"></asp:TextBox>
                        <br />
                        <asp:regularexpressionvalidator id="revFAX_NUMBER" runat="server" ControlToValidate="txtFAX_NUMBER" ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator>
                    </td>
                </tr>
                <tr>
                    <td class="midcolora" width="33%">
                        <asp:Label runat="server" ID="capCATEGORY">
                        </asp:Label>
                        <br />
                        <asp:TextBox ID="txtCATEGORY" runat="server" onkeypress="MaxLength(this,20)" 
                            onpaste="MaxLength(this,20)" MaxLength="20"></asp:TextBox>
                    </td>
                    <td class="midcolora" width="33%">
                    </td>
                     <td class="midcolora" width="33%">
                    </td>
                   <%--<td class="midcolora" width="33%">
                        <asp:Label runat="server" ID="capACTIVITY_TYPE" >
                      </asp:Label>  <span class="mandatory">*</span>
                        <br />
                        <asp:DropDownList runat="server" ID="cmbACTIVITY_TYPE" Height="16px" 
                            Width="130px">
                        </asp:DropDownList><br />
                        <asp:requiredfieldvalidator id="rfvACTIVITY_TYPE" Runat="server" ControlToValidate="cmbACTIVITY_TYPE" 
				        Display="Dynamic"></asp:requiredfieldvalidator>
                    </td>--%>
                    <%--<td class="midcolora" width="34%">
                        <asp:Label runat="server" ID="capCONSTRUCTION" Text="Construction:">
                        </asp:Label>
                        <br />
                        <asp:DropDownList runat="server" ID="cmbCONSTRUCTION" Height="16px" 
                            Width="131px    ">
                        </asp:DropDownList>
                    </td>--%>
                </tr>
                <tr>
                   <td class="midcolora" colspan="3">
                        <asp:Label runat="server" ID="capACTIVITY_TYPE" >
                      </asp:Label> 
                        <br />
                        <asp:DropDownList runat="server" ID="cmbACTIVITY_TYPE" Height="16px" 
                            Width="840px">
                        </asp:DropDownList><br />
                    </td>
                    </tr>
                <tr>
                <td class="midcolora" colspan="3">
                        <asp:Label runat="server" ID="capOCCUPIED" Text="Occupied As:">
                        </asp:Label>
                        <br />
                        <asp:DropDownList runat="server" ID="cmbOCCUPIED" Height="16px" Width="840px" >
                        </asp:DropDownList><br />
                    </td>
                   </tr>
                   
                   
                <tr>
                    <td colspan="3" class="midcolora" align="right" width="100%">
                    <asp:Label runat="server" ID="capDESCRIPTION">
                    </asp:Label><br />
                    <asp:TextBox ID="txtDESCRIPTION" runat="server" TextMode="MultiLine" Height="71px" 
                            Width="288px" onkeypress="MaxLength(this,1000)" 
                            onpaste="MaxLength(this,1000)" MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td class="midcolora" width="33%">
                    <table>
                        <tr>
                             <td class="midcolora"  >
                                <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" CausesValidation="False" ></cmsb:cmsbutton>
                            </td>
                            <td class="midcolora" >
                               <cmsb:CmsButton class="clsButton" runat="server" id="btnActivateDeactivate" text="Activate\Deactivate" onclick="btnActivateDeactivate_Click"></cmsb:CmsButton>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="midcolora" width="33%">
                </td>
                <td class="midcolora" width="34%">
                    <table width="100%" class="tableWidthHeader">
                        <tr>
                             
                            <td class="midcolorr" align="right">
                               <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" CausesValidation="False" onclick="btnDelete_Click"></cmsb:cmsbutton>
                               <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" onclick="btnSave_Click"></cmsb:cmsbutton>
                            </td>
                        </tr>
                    </table>
                </td>
                </tr>
                        
                    <input id="hidLOC_STATE" type="hidden" name="hidLOC_STATE" runat="server"/>
                    <input id="hidPOL_VERSION_ID" type="hidden" name="hidPOL_VERSION_ID" runat="server"/>
	                <input id="hidCUSTOMER_ID" type="hidden" name="hidCUSTOMER_ID" runat="server" />
	                <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"/>
	                <input id="hidPOL_ID" type="hidden" name="hidPOL_ID" runat="server"/>
	                <input id="hidIS_ACTIVE" type="hidden" value="Y" name="hidIS_ACTIVE" runat="server"/>
	                <input id="hidOldData" type="hidden" name="hidOldData" runat="server"/> 
	                <input id="hidLOCATION_ID" type="hidden" value="" name="hidLOCATION_ID" runat="server"/>
                    <input id="hidNew" type="hidden" value="" name="hidNew" runat="server"/>
                    <input id="hidTabTitle" type="hidden" value="" name="hidTabTitle" runat="server"/>
                    <input id="hidZipeCodeVerificationMsg" type="hidden" value="" name="hidZipeCodeVerificationMsg" runat="server"/>
                    <input id="hidZipCodeParam" type="hidden" value="" name="hidZipCodeParam" runat="server"/> 
                    <input id="hidLocationNumberMsg" type="hidden" value="" name="hidLocationNumberMsg" runat="server"/> 
                    <input id="hidOLD_Location_num" type="hidden" name="hidOLD_Location_num" runat="server"/>
                    <input id="hidOld_Loc_Msg" type="hidden" value="" name="hidOld_Loc_Msg" runat="server"/> 
                     <input id="hidIS_BILLING" type="hidden" value="" name="hidIS_BILLING" runat="server"/> 
                     <input id="hidLOCATION" type="hidden" value="" name="hidLOCATION" runat="server"/>  
                     <input id="hidLocation_Value" type="hidden" value="" name="hidLocation_Value" runat="server"/>  <%--added for tfs#2701 by pradeep--%>
                     <input id="hidValidateLoc" type="hidden" value="0" name="hidValidateLoc" runat="server"/>  
                     <%--Added for itrack-1152/TFS # 2598--%>
                     <input id="hidACTIVITY_TYPE" type="hidden" value="0" name="hidACTIVITY_TYPE" runat="server"/>  
                     <input id="hidOCCUPIED" type="hidden" value="0" name="hidOCCUPIED" runat="server"/>  
                     <input id="hidRUBRICA" type="hidden" value="0" name="hidRUBRICA" runat="server"/>  
                     <%--Added till here --%>

                     <%--Added for itrack 921--%>
                      <input type="hidden" id="hidSTATE_ID" name="hidSTATE_ID" runat="server" />
                      <%--till here --%>
            </table>
     </td>
     </tr>
   
    <script>
//      alert(document.getElementById('hidFormSaved').value);
//      alert("<%=primaryKeyValues%>");
        //      alert(document.getElementById('hidOldData').value);
        RefreshWebGrid(document.getElementById('hidFormSaved').value, "<%=primaryKeyValues%>", true);
		</script>
		</table>
		
    </form>
  
</body>
</html>

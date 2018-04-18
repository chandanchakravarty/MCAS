<%@ Page Language="c#" CodeBehind="AddDivision.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.clsAddDivision"
    ValidateRequest="false" %>

<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<html>
<head>
    <title>MNT_DIV_LIST</title>
    <meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
    <meta content='C#' name='CODE_LANGUAGE'>
    <meta content='JavaScript' name='vs_defaultClientScript'>
    <meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
    <script src="/cms/cmsweb/scripts/xmldom.js"></script>
    <script src="/cms/cmsweb/scripts/common.js"></script>
    <script src="/cms/cmsweb/scripts/form.js"></script>
    <script src="/cms/cmsweb/scripts/calendar.js" type="text/javascript"></script>

    <script language='javascript'>
        var eventButton = "btnSave";
        function AddData() {

            document.getElementById('hidDIV_ID').value = 'New';
            document.getElementById('txtDIV_CODE').focus();
            document.getElementById('txtDIV_CODE').value = '';
            document.getElementById('txtDIV_NAME').value = '';
            document.getElementById('txtDIV_ADD1').value = '';
            document.getElementById('txtDIV_ADD2').value = '';
            document.getElementById('txtDIV_CITY').value = '';
            //document.getElementById('cmbDIV_STATE').options.selectedIndex = -1;
            document.getElementById('txtDIV_ZIP').value = '';
            //document.getElementById('cmbDIV_COUNTRY').options.selectedIndex = -1;
            document.getElementById('txtDIV_PHONE').value = '';
            document.getElementById('txtDIV_EXT').value = '';
            document.getElementById('txtDIV_FAX').value = '';
            document.getElementById('txtDIV_EMAIL').value = '';
            document.getElementById('txtBRANCH_CNPJ').value = '';
            document.getElementById('txtDATE_OF_BIRTH').value = '';
            document.getElementById('txtREGIONAL_IDENTIFICATION').value = '';
            document.getElementById('txtREG_ID_ISSUE_DATE').value = '';
            document.getElementById('txtREG_ID_ISSUE').value = '';
           
            if (document.getElementById('btnActivateDeactivate'))
                document.getElementById('btnActivateDeactivate').setAttribute('disabled', true);
            document.getElementById('txtDIV_NAME').focus();
            if (document.getElementById('btnDelete'))
                document.getElementById('btnDelete').setAttribute('disabled', true);
            ChangeColor();

            DisableValidators();
        }

        function SetTab() {
            if ((document.getElementById('hidFormSaved').value == '1') || (document.getElementById('hidOldData').value != "")) {
                var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
                var tabtitles = TAB_TITLES.split(',');
                Url = "AttachmentIndex.aspx?calledfrom=division&EntityType=Division&EntityId=" + document.getElementById('hidDIV_ID').value + "&";
                DrawTab(2, top.frames[1], tabtitles[0], Url);
            }
            else {
                RemoveTab(2, top.frames[1]);
            }
        }

        function ResetAfterActivateDeactivate() {
            if (document.getElementById('hidReset').value == "1") {
                document.MNT_DIV_LIST.reset();
            }
        }

        function populateXML() {

            ResetAfterActivateDeactivate();
            //if(document.getElementById('hidFormSaved').value == '0')
            if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')) {
                //alert(document.getElementById('hidOldData').value);
                if (document.getElementById('hidOldData').value != "") {
                    //Enabling the activate deactivate button
                    if (document.getElementById('btnActivateDeactivate'))
                        document.getElementById('btnActivateDeactivate').setAttribute('disabled', false);

                    if (document.getElementById('btnDelete'))
                        document.getElementById('btnDelete').setAttribute('disabled', false);
                    //Storing the XML in hidRowId hidden fields

                    populateFormData(document.getElementById('hidOldData').value, MNT_DIV_LIST);


                }
                else {
                    AddData();
                }
            }
            //DisableExt('txtDIV_PHONE', 'txtDIV_EXT');

            SetTab();
            return false;
        }

        function generateCode() {
            var strname = new String();
            strname = document.getElementById("txtDIV_NAME").value;

            if (document.getElementById('hidDIV_ID').value == 'New') {
                if (strname.length > 6)
                    document.getElementById("txtDIV_CODE").value = strname.substring(0, 6);
                else
                    document.getElementById("txtDIV_CODE").value = strname;
            }


        }


        function setExtFocus() {
            /*alert(document.getElementById('revDIV_PHONE').getAttribute('IsValid'));
            if (document.getElementById('revDIV_PHONE').getAttribute('IsValid') == 'false')
            {
            alert('a');
            document.getElementById('txtDIV_PHONE').focus();
			     
			  }
            else if(document.getElementById('revDIV_PHONE').getAttribute('IsValid') == 'true')
            {
            alert('b');
            document.getElementById('txtDIV_EXT').focus();
				
			  }*/
        }

        /////ZIP AJAX CALL///
        function GetZipForState() {
            GlobalError = true;
            if (document.getElementById('cmbDIV_STATE').value == 14 || document.getElementById('cmbDIV_STATE').value == 22 || document.getElementById('cmbDIV_STATE').value == 49) {

                if (document.getElementById('txtDIV_ZIP').value != "") {
                    var intStateID = document.getElementById('cmbDIV_STATE').options[document.getElementById('cmbDIV_STATE').options.selectedIndex].value;
                    var strZipID = document.getElementById('txtDIV_ZIP').value;

                    var result = clsAddDivision.AjaxFetchZipForState(intStateID, strZipID);

                    return AjaxCallFunction_CallBack_Zip(result);

                }
                return false;
            }
            else
                return true;

        }
        function AjaxCallFunction_CallBack_Zip(response) {

            if (document.getElementById('cmbDIV_STATE').value == 14 || document.getElementById('cmbDIV_STATE').value == 22 || document.getElementById('cmbDIV_STATE').value == 49) {
                if (document.getElementById('txtDIV_ZIP').value != "") {

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

        //////AJAX END/////
        function ChkResult(objSource, objArgs) {
            objArgs.IsValid = true;
            if (objArgs.IsValid == true) {
                objArgs.IsValid = GetZipForState();
                if (objArgs.IsValid == false)
                    document.getElementById('csvDIV_ZIP').innerHTML = "The zip code does not belong to the state";
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

        //added by sonal
        function fillstateFromCountry() {

            GlobalError = true;
            var CountryID = document.getElementById('cmbDIV_COUNTRY').options[document.getElementById('cmbDIV_COUNTRY').selectedIndex].value;
            clsAddDivision.AjaxFillState(CountryID, fillState);
            if (GlobalError) {
                return false;
            }
            else {
                return true;
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
                var statesList = document.getElementById("cmbDIV_STATE");
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

                // setStateID();
                //document.getElementById('cmbPC_STATE').value = document.getElementById('hidSTATE_ID_OLD').value;
            }

            return false;
        }

        function saveext() {
            document.getElementById("hidExt").value = document.getElementById("txtDIV_EXT").value;
            document.getElementById("hidSTATE").value = document.getElementById('cmbDIV_STATE').options[document.getElementById('cmbDIV_STATE').selectedIndex].value;


        }


        function zipcodeval() {

            if (document.getElementById('cmbDIV_COUNTRY').options[document.getElementById('cmbDIV_COUNTRY').options.selectedIndex].value == '6') {
                document.getElementById('revDIV_ZIP').setAttribute('enabled', false);
            }
        }

        function zipcodeval1() {

            if (document.getElementById('cmbDIV_COUNTRY').options[document.getElementById('cmbDIV_COUNTRY').options.selectedIndex].value == '5') {
                document.getElementById('revDIV_ZIP').setAttribute('enabled', true);
            }
        } 
        
        function FormatZipCode(vr) {


            var vr = new String(vr.toString());
            if (vr != "" && (document.getElementById('cmbDIV_COUNTRY').options[document.getElementById('cmbDIV_COUNTRY').options.selectedIndex].value == '5')) {

                vr = vr.replace(/[-]/g, "");
                num = vr.length;
                if (num == 8 && (document.getElementById('cmbDIV_COUNTRY').options[document.getElementById('cmbDIV_COUNTRY').options.selectedIndex].value == '5')) {
                    vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
                    document.getElementById('revDIV_ZIP').setAttribute('enabled', false);
                }


            }

            return vr;
        }
        function validatCNPJ(objSource, objArgs) {




            var cnpjerrormsg = '<%=javasciptCNPJmsg %>';
            var CNPJ_invalid = '<%=CNPJ_invalid %>';


            var valid = false;
            var idd = objSource.id;
            var rfvid = idd.replace('csv', 'rev');
            if (document.getElementById(rfvid) != null)
                if (document.getElementById(rfvid).isvalid == true) {
                var theCPF = document.getElementById(objSource.controltovalidate)
                var len = theCPF.value.length;
                if (len == '18') {
                    valid = validar(objSource, objArgs);
                    if (valid) { objSource.innerText = ''; } else { objSource.innerText = CNPJ_invalid; }
                }
                else {

                    if (document.getElementById(rfvid) != null) {
                        if (document.getElementById(rfvid).isvalid == true) {
                            objArgs.IsValid = false;
                            objSource.innerHTML = cnpjerrormsg; //'Please enter 18 digit CPF No';
                        } else { objSource.innerHTML = ''; }
                    }
                }
                //for CNPJ # in if customer type is commercial
                //it check CNPJ format & valdate bia validar() function CNPJ is valid or not
            }
        }

        function Checkcpvmsg() { 
            if (document.getElementById("txtREG_ID_ISSUE_DATE").value == document.getElementById("txtDATE_OF_BIRTH").value) {
                document.getElementById("cpvREG_ID_ISSUE").style.display = 'none';
                document.getElementById('cpvREG_ID_ISSUE').setAttribute('enabled', false);
            }
            else {
                //document.getElementById("cpvREG_ID_ISSUE").style.display = 'inline';
                document.getElementById('cpvREG_ID_ISSUE').setAttribute('enabled', true);
            }
            
        }
        function ChkCreatedate(objSource, objArgs) {
            if (document.getElementById("revDATE_OF_BIRTH").isvalid == true) {
                var effdate = document.MNT_DIV_LIST.txtDATE_OF_BIRTH.value;
                objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
            }
            else
                objArgs.IsValid = true;
        }

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
        var jsaAppDtFormat = "<%=aAppDtFormat  %>";
        function ChkDOB(objSource, objArgs) {
            var dob = document.MNT_DIV_LIST.txtDATE_OF_BIRTH.value;
            objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>", dob, jsaAppDtFormat);
        }


        function Reset() {
            //DisableValidators();
            document.MNT_DIV_LIST.reset();
            //ChangeColor();
            return false;
        }

        function validate2() {


            if (document.getElementById('csvDATE_OF_BIRTH').isvalid == false) {
                document.getElementById('csvCREATION_DATE').setAttribute('enabled', false);

            }
            else
                document.getElementById('csvCREATION_DATE').setAttribute('enabled', true);
        }

        function validate3()                             //done by abhinav 
        {

           
                if (document.getElementById('revREGIONAL_ID_ISSUE_DATE').isvalid == false) {
                    document.getElementById('cpvREG_ID_ISSUE').setAttribute('enabled', false);
                    document.getElementById("cpvREG_ID_ISSUE").style.display = 'none';
                    document.getElementById('cpvREG_ID_ISSUE_DATE').setAttribute('enabled', false);
                    document.getElementById("cpvREG_ID_ISSUE_DATE").style.display = 'none';
                }
                else {
                    document.getElementById('cpvREG_ID_ISSUE').setAttribute('enabled', true);
                    document.getElementById('cpvREG_ID_ISSUE_DATE').setAttribute('enabled', true);
                }
            }
     

    </script>

    <style type="text/css">
        .style2
        {
            height: 30px;
        }
        .style3
        {
            height: 46px;
        }
    </style>

</head>
<body oncontextmenu="return false;" leftmargin='0' topmargin='0' onload='populateXML();ApplyColor();'>
    <form id='MNT_DIV_LIST' method='post' runat='server'>
    <p>
        <uc1:addressverification id="AddressVerification1" runat="server">
        </uc1:addressverification></p>
    <table cellspacing='0' cellpadding='0' width='100%' border='0'>
        <tr>
            <td class="midcolorc" align="right" colspan="4">
                <asp:Label ID="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
            </td>
        </tr>
        <tr id="trBody" runat="server">
            <td>
                <table width='100%' border='0' align='center'>
                    <tr>
                        <td class="pageHeader" colspan="4">
                           <asp:Label ID="capMessages" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolorc" align="right" colspan="4">
                            <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capDIV_NAME" runat="server">Name</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtDIV_NAME' runat='server' size='30' MaxLength='70'></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvDIV_NAME" runat="server" ControlToValidate="txtDIV_NAME"
                                ErrorMessage="DIV_NAME can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                           <%-- <asp:RegularExpressionValidator ID="revDIV_NAME" runat="server" ControlToValidate="txtDIV_NAME"
                                ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capDIV_CODE" runat="server">Code</asp:Label><span id="spnDIV_CODE" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtDIV_CODE' runat='server' size='8' MaxLength='6'></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvDIV_CODE" runat="server" ControlToValidate="txtDIV_CODE"
                                ErrorMessage="DIV_CODE can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                           <%-- <asp:RegularExpressionValidator ID="revDIV_CODE" runat="server" ControlToValidate="txtDIV_CODE"
                                ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capDIV_ADD1" runat="server">Add1</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtDIV_ADD1' runat='server' size='30' MaxLength='70'></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvDIV_ADD1" runat="server" ControlToValidate="txtDIV_ADD1"
                                ErrorMessage="DIV_ADD1 can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capDIV_ADD2" runat="server">Add2</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtDIV_ADD2' runat='server' size='30' MaxLength='70'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capDIV_CITY" runat="server">City</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtDIV_CITY' runat='server' size='30' MaxLength='40'></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvDIV_CITY" runat="server" ControlToValidate="txtDIV_CITY"
                                ErrorMessage="DIV_CITY can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                           <%-- <asp:RegularExpressionValidator ID="revDIV_CITY" runat="server" ControlToValidate="txtDIV_CITY"
                                ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capDIV_COUNTRY" runat="server">Country</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:DropDownList ID='cmbDIV_COUNTRY' OnFocus="SelectComboIndex('cmbDIV_COUNTRY')"
                                runat='server' onchange="javascript:fillstateFromCountry();">
                                <asp:ListItem Value='0'></asp:ListItem>
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvDIV_COUNTRY" runat="server" ControlToValidate="cmbDIV_COUNTRY"
                                ErrorMessage="DIV_COUNTRY can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capDIV_STATE" runat="server">State</asp:Label><span id="spnDIV_STATE" runat="server" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:DropDownList ID='cmbDIV_STATE' OnFocus="SelectComboIndex('cmbDIV_STATE')" runat='server'>
                                <asp:ListItem Value='0'></asp:ListItem>
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvDIV_STATE" runat="server" ControlToValidate="cmbDIV_STATE"
                                ErrorMessage="DIV_STATE can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capDIV_ZIP" runat="server">Zip</asp:Label><span id="spnDIV_ZIP" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtDIV_ZIP' runat='server' size='13' MaxLength='8' OnBlur="this.value=FormatZipCode(this.value);zipcodeval();zipcodeval1();ValidatorOnChange();GetZipForState();"></asp:TextBox>
                            <%-- Added by Swarup on 30-mar-2007 --%>
                            <asp:HyperLink ID="hlkZipLookup" runat="server" CssClass="HotSpot"> <asp:Image ID="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif"
                                    ImageAlign="Bottom"></asp:Image>
                            </asp:HyperLink>
                            <br>
                            <asp:CustomValidator ID="csvDIV_ZIP" runat="server" ClientValidationFunction="ChkResult"
                                ErrorMessage=" " Display="Dynamic" ControlToValidate="txtDIV_ZIP"></asp:CustomValidator>
                            <asp:RequiredFieldValidator ID="rfvDIV_ZIP" runat="server" ControlToValidate="txtDIV_ZIP"
                                ErrorMessage="DIV_ZIP can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revDIV_ZIP" runat="server" ControlToValidate="txtDIV_ZIP"
                                ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capDIV_PHONE" runat="server">Phone</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtDIV_PHONE' runat='server' size='30' MaxLength="15" onblur="FormatBrazilPhone()" ></asp:TextBox><br>
                            <asp:RegularExpressionValidator ID="revDIV_PHONE" runat="server" ControlToValidate="txtDIV_PHONE"
                                ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capDIV_EXT" runat="server">Ext</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtDIV_EXT' runat='server' size='30' MaxLength='6' ></asp:TextBox><br>
                            <asp:RegularExpressionValidator ID="revDIV_EXT" runat="server" ControlToValidate="txtDIV_EXT"
                                ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capDIV_FAX" runat="server">Fax</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtDIV_FAX' runat='server' size='30' MaxLength='15' onblur="FormatBrazilPhone()" ></asp:TextBox><br>
                            <asp:RegularExpressionValidator ID="revDIV_FAX" runat="server" ControlToValidate="txtDIV_FAX"
                                ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capDIV_EMAIL" runat="server">Email</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtDIV_EMAIL' runat='server' size='30' MaxLength='50'></asp:TextBox><br>
                            <asp:RegularExpressionValidator ID="revDIV_EMAIL" runat="server" ControlToValidate="txtDIV_EMAIL"
                                ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capNAIC_CODE" runat="server"></asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID="txtNAIC_CODE" runat='server' size='30' MaxLength='10'></asp:TextBox>
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capDIV_BRANCHCODE" runat="server">Branch Code</asp:Label><span ID="spnBRANCH_CODE" runat="server" class="mandatory">*</span></asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtBRANCH_CODE' runat='server' size='12' MaxLength='10'></asp:TextBox>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvBRANCH_CODE" runat="server" ControlToValidate="txtBRANCH_CODE"
                                ErrorMessage="DIV_BRANCHCODE can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revDIV_BRANCHCODE" runat="server" ControlToValidate="txtBRANCH_CODE"
                                ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr id="tr001" runat="server">
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capDIV_BRANCHCNPJ" runat="server">CNPJ</asp:Label></asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:textbox id="txtBRANCH_CNPJ" runat="server" MaxLength="18"  
                                OnBlur="this.value=FormatCPFCNPJ(this.value); ValidatorOnChange();" size="20" Width="164px"></asp:textbox><br />
                          <asp:RegularExpressionValidator runat="server" ID="revBRANCH_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtBRANCH_CNPJ"></asp:RegularExpressionValidator>
                    <asp:CustomValidator runat="server" ID="csvBRANCH_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtBRANCH_CNPJ" ClientValidationFunction="validatCNPJ"></asp:CustomValidator>
                          </td>
                      <td class='midcolora' width='32%'>
                        <asp:Label ID="capDATE_OF_BIRTH" runat="server" ></asp:Label>
               
               
                        </td>
                       
                        <TD class="midcolora" width="32%"><asp:textbox id="txtDATE_OF_BIRTH" runat="server" size="12" maxlength="10" onblur="Checkcpvmsg();ValidatorOnChange();validate2();"></asp:textbox><asp:hyperlink id="hlkDATE_OF_BIRTH" runat="server" CssClass="HotSpot"> <ASP:IMAGE id="imgDATE_OF_BIRTH" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink>
							
							<asp:regularexpressionvalidator id="revDATE_OF_BIRTH" runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic"></asp:regularexpressionvalidator>
							<asp:customvalidator id="csvDATE_OF_BIRTH" Runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic"
								ClientValidationFunction="ChkDOB"></asp:customvalidator>
								 <asp:CustomValidator ID="csvCREATION_DATE" runat="server" 
                                     ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic" ClientValidationFunction="ChkCreatedate"></asp:CustomValidator>
                        </td>
                        
                    </tr>
                    
                                   
                     <tr>
                                 <td class="midcolora" width="18%"><asp:Label ID="capACTIVITY" runat="server"></asp:Label>
								</td>
								<td class="midcolora" width="32%"><asp:DropDownList ID="cmbACTIVITY" runat="server" Width="400px"></asp:DropDownList>
								</td>
								<td class="midcolora" width="18%"><asp:Label ID="capREGIONAL_ID" runat="server"></asp:Label>
								</td>
								<td class="midcolora" width="32%"><asp:TextBox ID="txtREGIONAL_IDENTIFICATION" runat="server" MaxLength='40'></asp:TextBox>
								</td>
                        
                   </tr>
                   
                    <tr>
                        <td class="midcolora" width="18%">
                                 <asp:Label runat="server" ID="capREGIONAL_ID_ISSUE_DATE">Regional ID Issue Date</asp:Label></td>
                                 <td class="midcolora" width="32%"><asp:TextBox runat="server" ID="txtREG_ID_ISSUE_DATE" AutoCompleteType="Disabled" CausesValidation="true" onblur="Checkcpvmsg();ValidatorOnChange();validate3();"></asp:TextBox><asp:HyperLink ID="hlkREG_ID_ISSUE_DATE" runat="server" CssClass="HotSpot"> <asp:Image ID="imgREG_ID_ISSUE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif" ></asp:Image></asp:HyperLink><br/>
                                <asp:RegularExpressionValidator runat="server" ID="revREGIONAL_ID_ISSUE_DATE" Display="Dynamic" ControlToValidate="txtREG_ID_ISSUE_DATE"></asp:RegularExpressionValidator>
                               <asp:comparevalidator id="cpvREG_ID_ISSUE_DATE" ControlToValidate="txtREG_ID_ISSUE_DATE" Display="Dynamic" Runat="server" Type="Date"
					             Operator="LessThanEqual" 
					             ValueToCompare="<%#DateTime.Today.ToShortDateString()%>"></asp:comparevalidator><br>	
                                <asp:comparevalidator id="cpvREG_ID_ISSUE"  
					             ControlToValidate="txtREG_ID_ISSUE_DATE" Display="Dynamic" Runat="server" ControlToCompare="txtDATE_OF_BIRTH" Type="Date"
			                     Operator="GreaterThan"></asp:comparevalidator>
			                     
                                </td>
								<td class="midcolora" width="18%"><asp:Label ID="capREG_ID_ISSUE" runat="server"></asp:Label>
								</td>
								<td class="midcolora" width="32%"><asp:TextBox ID="txtREG_ID_ISSUE" runat="server" MaxLength='40'></asp:TextBox>
								</td>
                          
                     
                        
                   </tr>
                   
                  <%-- <tr>
                        <td class='midcolora' width='18%'>
                          <asp:Label runat="server" ID="capREG_ID_ISSUE">Regional Id Isuue</asp:Label> 
                        </td>
                        <td class='midcolora' width='32%'>
                         <asp:TextBox runat="server" AutoCompleteType="Disabled" ID="txtREG_ID_ISSUE"></asp:TextBox><br />
                <asp:RequiredFieldValidator runat="server" ID="rfvREG_ID_ISSUE" Display="Dynamic"
                    ControlToValidate="txtREG_ID_ISSUE"></asp:RequiredFieldValidator>
                          </td>
                          
                          <td class='midcolora' width='18%'>
                           
                        </td>
                        <td class='midcolora' width='32%'">
                         
                          </td>
                          
                     
                        
                   </tr>
                   --%>
                   
                    <tr>
                        <td class="midcolora" colspan="2">
                            <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" Text="Reset"></cmsb:CmsButton><cmsb:CmsButton
                                class="clsButton" ID="btnActivateDeactivate" CausesValidation="false" runat="server"
                                Text="Activate/Deactivate"></cmsb:CmsButton>
                        </td>
                        <td class='midcolorr' colspan="2">
                            <cmsb:CmsButton class="clsButton" ID="btnDelete" runat="server" Text="Delete"></cmsb:CmsButton><cmsb:CmsButton
                                class="clsButton" ID='btnSave' runat="server" Text='Save' OnClientClick="saveext()"></cmsb:CmsButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
    <input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
    <input id="hidOldData" type="hidden" name="hidOldData" runat="server">
    <input id="hidDIV_ID" type="hidden" name="hidDIV_ID" runat="server">
    <input id="hidExt" type="hidden" name="hidExt" runat="server">
     <input id="hidDIV_COUNTRY" type="hidden" name="hidDIV_COUNTRY" runat="server">
     <input id="hidSTATE" type="hidden" name="hidSTATE" runat="server">
    <input id="hidReset" type="hidden" value="0" name="hidReset" runat="server">
    <input  type="hidden" runat="server" ID="hidTAB_TITLES"  value=""  name="hidTAB_TITLES"/>  
    </form>

    <script>
        RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidDIV_ID').value, true);
        if (document.getElementById("hidFormSaved").value == "5") {
            /*Record deleted*/
            /*Refreshing the grid and coverting the form into add mode*/
            /*Using the javascript*/
            RemoveTab(2, top.frames[1]);
            RefreshWebGrid("1", "1");
            document.getElementById('hidDIV_ID').value = "NEW";

        }
    </script>

</body>
</html>

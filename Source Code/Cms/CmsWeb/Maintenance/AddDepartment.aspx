<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>

<%@ Page Language="c#" CodeBehind="AddDepartment.aspx.cs" AutoEventWireup="false"
    Inherits="Cms.CmsWeb.Maintenance.AddDepartment" ValidateRequest="false" %>

<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<html>
<head>
    <title>MNT_DEPT_LIST</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet">

    <script src="/cms/cmsweb/scripts/xmldom.js"></script>

    <script src="/cms/cmsweb/scripts/common.js"></script>

    <script src="/cms/cmsweb/scripts/form.js"></script>

    <script language="javascript">
        function AddData() {

            document.getElementById('hidDEPT_ID').value = 'New';
            document.getElementById('txtDEPT_CODE').focus();
            document.getElementById('txtDEPT_CODE').value = '';
            document.getElementById('txtDEPT_NAME').value = '';
            document.getElementById('txtDEPT_ADD1').value = '';
            document.getElementById('txtDEPT_ADD2').value = '';
            document.getElementById('txtDEPT_CITY').value = '';
            //document.getElementById('cmbDEPT_STATE').options.selectedIndex = -1;
            document.getElementById('txtDEPT_ZIP').value = '';
            //document.getElementById('cmbDEPT_COUNTRY').options.selectedIndex = -1;
            document.getElementById('txtDEPT_PHONE').value = '';
            document.getElementById('txtDEPT_EXT').value = '';
            document.getElementById('txtDEPT_FAX').value = '';
            document.getElementById('txtDEPT_EMAIL').value = '';

            if (document.getElementById('btnActivateDeactivate'))
                document.getElementById('btnActivateDeactivate').setAttribute('disabled', true);
            document.getElementById('txtDEPT_NAME').focus();
            if (document.getElementById('btnDelete'))
                document.getElementById('btnDelete').setAttribute('disabled', true);
            ChangeColor();
            DisableValidators();
        }

        function setTab() {
            if ((document.getElementById('hidFormSaved').value == '1') || (document.getElementById('hidOldData').value != "")) {
                var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
                var tabtitles = TAB_TITLES.split(',');
         
                Url = "AttachmentIndex.aspx?calledfrom=department&EntityType=Department&EntityId=" + document.getElementById('hidDEPT_ID').value + "&";

                var entityTyp = '';
                if (document.getElementById('txtDEPT_NAME') != null)
                    entityTyp += document.getElementById('txtDEPT_NAME').value;
                if (document.getElementById('txtDEPT_CODE') != null)
                    entityTyp += '~' + document.getElementById('txtDEPT_CODE').value;



                Url = "AttachmentIndex.aspx?calledfrom=department&EntityType=Department&EntityName=" + entityTyp + "&EntityId=" + document.getElementById('hidDEPT_ID').value + "&";
                DrawTab(2, top.frames[1],tabtitles[0], Url);
            }
            else {
                RemoveTab(2, top.frames[1]);
            }
        }
        function ResetAfterActivateDeactivate() {
            if (document.getElementById('hidReset').value == "1") {
                document.MNT_DEPT_LIST.reset();
            }
        }
        function populateXML() {


            ResetAfterActivateDeactivate();


            if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')) {
                //alert(document.getElementById('hidOldData').value);
                if (document.getElementById('hidOldData').value != "") {
                    //Enabling the activate deactivate button
                    if (document.getElementById('btnActivateDeactivate'))
                        document.getElementById('btnActivateDeactivate').setAttribute('disabled', false);
                    if (document.getElementById('btnDelete'))
                        document.getElementById('btnDelete').setAttribute('disabled', false);
                    //Storing the XML in hidRowId hidden fields
                    populateFormData(document.getElementById('hidOldData').value, MNT_DEPT_LIST);
                   
                    setTimeout("selectedstate()", 300);
                }
                else {
                    AddData();
                }
            }
            DisableExt('txtDEPT_PHONE', 'txtDEPT_EXT')
            setTab();
            return false;
        }
        function generateCode() {
            var strname = new String();
            strname = document.getElementById("txtDEPT_NAME").value;

            if (document.getElementById('hidDEPT_ID').value == 'New') {
                if (strname.length > 6)
                    document.getElementById("txtDEPT_CODE").value = strname.substring(0, 6);
                else
                    document.getElementById("txtDEPT_CODE").value = strname;
            }


        }


        /////ZIP AJAX CALL///
        function GetZipForState() {
            GlobalError = true;
            if (document.getElementById('cmbDEPT_STATE').value == 14 || document.getElementById('cmbDEPT_STATE').value == 22 || document.getElementById('cmbDEPT_STATE').value == 49) {
                if (document.getElementById('txtDEPT_ZIP').value != "") {
                    var intStateID = document.getElementById('cmbDEPT_STATE').options[document.getElementById('cmbDEPT_STATE').options.selectedIndex].value;
                    var strZipID = document.getElementById('txtDEPT_ZIP').value;
                    var result = AddDepartment.AjaxFetchZipForState(intStateID, strZipID);
                    return AjaxCallFunction_CallBack_Zip(result);

                }
                return false;
            }
            else
                return true;

        }
        function AjaxCallFunction_CallBack_Zip(response) {
            if (document.getElementById('cmbDEPT_STATE').value == 14 || document.getElementById('cmbDEPT_STATE').value == 22 || document.getElementById('cmbDEPT_STATE').value == 49) {
                if (document.getElementById('txtDEPT_ZIP').value != "") {
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
                    document.getElementById('csvDEPT_ZIP').innerHTML = "The zip code does not belong to the state";
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



        // Added by Swarup For checking zip code for LOB: End	

        function CopyDivisionAddress() {
            var URL = "<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>";

            document.getElementById('hidDIV_ADDRESS').value = '';
            var title = document.getElementById('hidTitle').value;
            OpenLookupWithFunction(URL, 'DIV_ADDRESS', 'ADDRESS', 'hidDIV_ADDRESS', 'hidDIV_ADDRESS', 'CopyDivisionAddress', title, '', 'FetchDivisionAddress()');
            return false;

        }
        function FetchDivisionAddress() {
            //	debugger
            var div_address = document.getElementById('hidDIV_ADDRESS').value.split('~');

            if (div_address.length >= 10) {

                document.getElementById('txtDEPT_ADD1').value = div_address[0];
                document.getElementById('txtDEPT_ADD2').value = div_address[1];
                document.getElementById('txtDEPT_CITY').value = div_address[2];

                var cmbcountry = document.getElementById('cmbDEPT_COUNTRY');
                //cmbcountry.options[cmbcountry.selectedIndex].value = div_address[5];

                for (i = 0; i < document.getElementById('cmbDEPT_COUNTRY').options.length; i++) {

                    if (document.getElementById('cmbDEPT_COUNTRY').options[i].value == div_address[5])
                        document.getElementById('cmbDEPT_COUNTRY').options[i].selected = true;

                }
                //debugger
                fillstateFromCountry();

                document.getElementById("hidSTATE").value = div_address[3];
                setTimeout("selectedstate()", 300);


                //cmbstate.options[cmbstate.selectedIndex].value = div_address[3];
                //document.getElementById('cmbDEPT_STATE').selectedIndex =div_address[3] ;
                document.getElementById('txtDEPT_ZIP').value = div_address[4];
                //document.getElementById('cmbDEPT_COUNTRY').selectedIndex =div_address[5] ;

                document.getElementById('txtDEPT_PHONE').value = div_address[6];
                document.getElementById('txtDEPT_EXT').value = div_address[7];
                document.getElementById('txtDEPT_FAX').value = div_address[8];
                document.getElementById('txtDEPT_EMAIL').value = div_address[9];
            }

        }

        //added by sonal

        function selectedstate() {
            //alert(document.getElementById("hidSTATE").value);
            for (j = 0; j < document.getElementById('cmbDEPT_STATE').options.length; j++) {

                if (document.getElementById('cmbDEPT_STATE').options[j].value == document.getElementById("hidSTATE").value)
                    document.getElementById('cmbDEPT_STATE').options[j].selected = true;


            }
        }
        function fillstateFromCountry() {

            GlobalError = true;
            var CountryID = document.getElementById('cmbDEPT_COUNTRY').options[document.getElementById('cmbDEPT_COUNTRY').selectedIndex].value;
            AddDepartment.AjaxFillState(CountryID, fillState);
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
                var statesList = document.getElementById("cmbDEPT_STATE");
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
            document.getElementById("hidExt").value = document.getElementById("txtDEPT_EXT").value;
            document.getElementById("hidSTATE").value = document.getElementById('cmbDEPT_STATE').options[document.getElementById('cmbDEPT_STATE').selectedIndex].value;

        }

        function zipcodeval() {

            if (document.getElementById('cmbDEPT_COUNTRY').options[document.getElementById('cmbDEPT_COUNTRY').options.selectedIndex].value == '6') {
                document.getElementById('revDEPT_ZIP').setAttribute('enabled', false);
            }
        }

        function zipcodeval1() {

            if (document.getElementById('cmbDEPT_COUNTRY').options[document.getElementById('cmbDEPT_COUNTRY').options.selectedIndex].value == '5') {
                document.getElementById('revDEPT_ZIP').setAttribute('enabled', true);
            }
        } 

        function FormatZipCode(vr) {


            var vr = new String(vr.toString());
            if (vr != "" && (document.getElementById('cmbDEPT_COUNTRY').options[document.getElementById('cmbDEPT_COUNTRY').options.selectedIndex].value == '5')) {

                vr = vr.replace(/[-]/g, "");
                num = vr.length;
                if (num == 8 && (document.getElementById('cmbDEPT_COUNTRY').options[document.getElementById('cmbDEPT_COUNTRY').options.selectedIndex].value == '5')) {
                    vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
                    document.getElementById('revDEPT_ZIP').setAttribute('enabled', false);
                }


            }

            return vr;
        }
	
	
    </script>

</head>
<body oncontextmenu="return false;" leftmargin="0" topmargin="0" onload="populateXML();ApplyColor();">
    <form id="MNT_DEPT_LIST" method="post" runat="server">
    <p>
        <uc1:addressverification id="AddressVerification1" runat="server">
        </uc1:addressverification></p>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="midcolorc" align="right" colspan="4">
                <asp:Label ID="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
            </td>
        </tr>
        <tr id="trBody" runat="server">
            <td>
                <table width="100%" align="center" border="0">
                    <tr>
                        <td class="pageHeader" colspan="4">
                            <asp:Label ID="capMessages" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolorc" align="right" colspan="4">
                            <asp:Label ID="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capDEPT_NAME" runat="server">Name</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtDEPT_NAME" runat="server" MaxLength="70" size="30"></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvDEPT_NAME" runat="server" Display="Dynamic" ErrorMessage=""
                                ControlToValidate="txtDEPT_NAME"></asp:RequiredFieldValidator><%--DEPT_NAME can't be blank.--%>
                               <%-- <asp:RegularExpressionValidator
                                    ID="revDEPT_NAME" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                    ControlToValidate="txtDEPT_NAME"></asp:RegularExpressionValidator>--%>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capDEPT_CODE" runat="server">Code</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtDEPT_CODE" runat="server" MaxLength="6" size="8"></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvDEPT_CODE" runat="server" Display="Dynamic" ErrorMessage=""
                                ControlToValidate="txtDEPT_CODE"></asp:RequiredFieldValidator><%--DEPT_CODE can't be blank.--%>
                                <%--<asp:RegularExpressionValidator
                                    ID="revDEPT_CODE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                    ControlToValidate="txtDEPT_CODE"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="lblCopy_Address" runat="server"></asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <span class="labelfont" id="spnHOLDER_ID" runat="server"></span>
                            <cmsb:CmsButton class="clsButton" ID="btnCopyDivisionAddress" runat="server" Text="Copy Division Address">
                            </cmsb:CmsButton>
                        </td>
                        <td class="midcolora" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capDEPT_ADD1" runat="server">Address 1</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtDEPT_ADD1" runat="server" MaxLength="70" size="30"></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvDEPT_ADD1" runat="server" Display="Dynamic" ErrorMessage=""
                                ControlToValidate="txtDEPT_ADD1"></asp:RequiredFieldValidator><%--DEPT_ADD1 can't be blank.--%>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capDEPT_ADD2" runat="server">Address 2</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtDEPT_ADD2" runat="server" MaxLength="70" size="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capDEPT_CITY" runat="server">City</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtDEPT_CITY" runat="server" MaxLength="40" size="30"></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvDEPT_CITY" runat="server" Display="Dynamic" ErrorMessage=""
                                ControlToValidate="txtDEPT_CITY"></asp:RequiredFieldValidator><%--DEPT_CITY can't be blank.--%>
                               <%-- <asp:RegularExpressionValidator
                                    ID="revDEPT_CITY" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                    ControlToValidate="txtDEPT_CITY"></asp:RegularExpressionValidator>--%>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capDEPT_COUNTRY" runat="server">Country</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbDEPT_COUNTRY" onfocus="SelectComboIndex('cmbDEPT_COUNTRY')"
                                runat="server" onchange="javascript:fillstateFromCountry();">
                                <asp:ListItem Value='0'></asp:ListItem>
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvDEPT_COUNTRY" runat="server" Display="Dynamic"
                                ErrorMessage="" ControlToValidate="cmbDEPT_COUNTRY"></asp:RequiredFieldValidator><%--DEPT_COUNTRY can't be blank.--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capDEPT_STATE" runat="server">State</asp:Label><span id="spnDEPT_STATE" runat="server" class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbDEPT_STATE" onfocus="SelectComboIndex('cmbDEPT_STATE')"
                                runat="server">
                                <asp:ListItem Value='0'></asp:ListItem>
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvDEPT_STATE" runat="server" Display="Dynamic" ErrorMessage=""
                                ControlToValidate="cmbDEPT_STATE"></asp:RequiredFieldValidator><%--DEPT_STATE can't be blank.--%>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capDEPT_ZIP" runat="server">Zip</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtDEPT_ZIP" runat="server" MaxLength="8" size="13" OnBlur="this.value=FormatZipCode(this.value);zipcodeval();zipcodeval1();ValidatorOnChange();GetZipForState();"></asp:TextBox>
                            <%-- Added by Swarup on 30-mar-2007 --%>
                            <asp:HyperLink ID="hlkZipLookup" runat="server" CssClass="HotSpot">
                                <asp:Image ID="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif"
                                    ImageAlign="Bottom"></asp:Image>
                            </asp:HyperLink>
                            <br>
                            <asp:CustomValidator ID="csvDEPT_ZIP" runat="server" ClientValidationFunction="ChkResult"
                                ErrorMessage=" " Display="Dynamic" ControlToValidate="txtDEPT_ZIP"></asp:CustomValidator>
                            <asp:RequiredFieldValidator ID="rfvDEPT_ZIP" runat="server" Display="Dynamic" ErrorMessage=""
                                ControlToValidate="txtDEPT_ZIP"></asp:RequiredFieldValidator><%--DEPT_ZIP can't be blank.--%>
                            <asp:RegularExpressionValidator ID="revDEPT_ZIP" runat="server" Display="Dynamic"
                                ErrorMessage="RegularExpressionValidator" ControlToValidate="txtDEPT_ZIP"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capDEPT_PHONE" runat="server">Phone</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtDEPT_PHONE" runat="server" MaxLength="15" size="30" ></asp:TextBox><br>
                            <asp:RegularExpressionValidator ID="revDEPT_PHONE" runat="server" Display="Dynamic"
                                ErrorMessage="RegularExpressionValidator" ControlToValidate="txtDEPT_PHONE"></asp:RegularExpressionValidator>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capDEPT_EXT" runat="server">Ext</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtDEPT_EXT" runat="server" MaxLength="6" size="30" ReadOnly="True"></asp:TextBox><br>
                            <asp:RegularExpressionValidator ID="revDEPT_EXT" runat="server" Display="Dynamic"
                                ErrorMessage="RegularExpressionValidator" ControlToValidate="txtDEPT_EXT"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capDEPT_FAX" runat="server">Fax</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtDEPT_FAX" runat="server" MaxLength="15" size="30" onblur="FormatBrazilPhone()" ></asp:TextBox><br>
                            <asp:RegularExpressionValidator ID="revDEPT_FAX" runat="server" Display="Dynamic"
                                ErrorMessage="RegularExpressionValidator" ControlToValidate="txtDEPT_FAX"></asp:RegularExpressionValidator>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capDEPT_EMAIL" runat="server">Email</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtDEPT_EMAIL" runat="server" MaxLength="50" size="30"></asp:TextBox><br>
                            <asp:RegularExpressionValidator ID="revDEPT_EMAIL" runat="server" Display="Dynamic"
                                ErrorMessage="RegularExpressionValidator" ControlToValidate="txtDEPT_EMAIL"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" colspan="2">
                            <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" Text="Reset"></cmsb:CmsButton><cmsb:CmsButton
                                class="clsButton" ID="btnActivateDeactivate" runat="server" CausesValidation="false"
                                Text="Activate/Deactivate"></cmsb:CmsButton>
                        </td>
                        <td class="midcolorr" colspan="2">
                            <cmsb:CmsButton class="clsButton" ID="btnDelete" runat="server" Text="Delete"></cmsb:CmsButton><cmsb:CmsButton
                                class="clsButton" ID="btnSave" runat="server" Text="Save" OnClientClick="saveext()">
                            </cmsb:CmsButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
    <input id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server">
    <input id="hidOldData" type="hidden" name="hidOldData" runat="server">
    <input id="hidDEPT_ID" type="hidden" value="0" name="hidDEPT_ID" runat="server">
    <input id="hidReset" type="hidden" value="0" name="hidReset" runat="server">
    <input id="hidDIV_ADDRESS" type="hidden" name="hidDIV_ADDRESS" runat="server">
    <input id="hidExt" type="hidden" name="hidExt" runat="server">
    <input id="hidSTATE" type="hidden" name="hidSTATE" runat="server">
    <input id="hidTitle" type="hidden" name="hidTitle" runat="server" />
     <input id="hidDEPT_COUNTRY" type="hidden" name="hidDEPT_COUNTRY" runat="server">
    <input  type="hidden" runat="server" ID="hidTAB_TITLES"  value=""  name="hidTAB_TITLES"/> 
    </form>

    <script>

        RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidDEPT_ID').value, true);
        if (document.getElementById("hidFormSaved").value == "5") {
            /*Record deleted*/
            /*Refreshing the grid and coverting the form into add mode*/
            /*Using the javascript*/
            RemoveTab(2, top.frames[1]);
            RefreshWebGrid("1", "1");
            document.getElementById('hidDEPT_ID').value = "NEW";

        }
    </script>

</body>
</html>

<%@ Page ValidateRequest="false" Language="c#" CodeBehind="addprofitcenter.aspx.cs"
    AutoEventWireup="false" Inherits="Cms.CmsWeb.AddProfitCenter" %>

<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<html>
<head>
    <title>MNT_PROFIT_CENTER_LIST</title>
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

            document.getElementById('hidPC_ID').value = 'New';
            //document.getElementById('txtPC_CODE').focus();
            document.getElementById('txtPC_CODE').value = '';
            document.getElementById('txtPC_NAME').value = '';
            document.getElementById('txtPC_ADD1').value = '';
            document.getElementById('txtPC_ADD2').value = '';
            document.getElementById('txtPC_CITY').value = '';
            //document.getElementById('cmbPC_STATE').options.selectedIndex = -1;
            document.getElementById('txtPC_ZIP').value = '';
            //document.getElementById('cmbPC_COUNTRY').options.selectedIndex = -1;
            document.getElementById('txtPC_PHONE').value = '';
            document.getElementById('txtPC_EXT').value = '';
            document.getElementById('txtPC_FAX').value = '';
            document.getElementById('txtPC_EMAIL').value = '';
            if (document.getElementById('btnActivateDeactivate'))
                document.getElementById('btnActivateDeactivate').setAttribute('disabled', true);
            document.getElementById('txtPC_NAME').focus();

            if (document.getElementById('btnDelete'))
                document.getElementById('btnDelete').setAttribute('disabled', true);
            ChangeColor();
            DisableValidators();
        }

        function setTab() {
            if ((document.getElementById('hidFormSaved').value == '1') || (document.getElementById('hidOldData').value != "")) {
                var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
                var tabtitles = TAB_TITLES.split(',');
            
                Url = "AttachmentIndex.aspx?EntityType=ProfitCenter&EntityId=" + document.getElementById('hidPC_ID').value + "&";
                var entityTyp = '';
                if (document.getElementById('txtPC_NAME') != null)
                    entityTyp += document.getElementById('txtPC_NAME').value;
                if (document.getElementById('txtPC_CODE') != null)
                    entityTyp += '~' + document.getElementById('txtPC_CODE').value;
                Url = "AttachmentIndex.aspx?calledfrom=ProfitCenter&EntityType=ProfitCenter&EntityName=" + entityTyp + "&EntityId=" + document.getElementById('hidPC_ID').value + "&";
                DrawTab(2, top.frames[1], tabtitles[0], Url);
            }
            else {
                RemoveTab(2, top.frames[1]);
            }
        }
        function ResetAfterActivateDeactivate() {
            if (document.getElementById('hidReset').value == "1") {
                document.MNT_PROFIT_CENTER_LIST.reset();
            }
        }

        function populateXML() {
            ResetAfterActivateDeactivate();
            //alert(document.getElementById('hidOldData').value);
            //if(document.getElementById('hidFormSaved').value == '0')
            if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')) {
                if (document.getElementById('hidOldData').value != "") {
                    //Enabling the activate deactivate button
                    if (document.getElementById('btnActivateDeactivate'))
                        document.getElementById('btnActivateDeactivate').setAttribute('disabled', false);
                    if (document.getElementById('btnDelete'))
                        document.getElementById('btnDelete').setAttribute('disabled', false);
                    //Storing the XML in hidRowId hidden fields
                    populateFormData(document.getElementById('hidOldData').value, MNT_PROFIT_CENTER_LIST);
                    setTimeout("selectedstate()", 300);

                }
                else {
                    AddData();
                }
            }
            DisableExt('txtPC_PHONE', 'txtPC_EXT');

            setTab();
            return false;
        }
        function generateCode() {
            var strname = new String();
            strname = document.getElementById("txtPC_NAME").value;

            if (document.getElementById('hidPC_ID').value == 'New') {
                if (strname.length > 6)
                    document.getElementById("txtPC_CODE").value = strname.substring(0, 6);
                else
                    document.getElementById("txtPC_CODE").value = strname;
            }


        }

        /////ZIP AJAX CALL///
        function GetZipForState() {
            GlobalError = true;
            if (document.getElementById('cmbPC_STATE').value == 14 || document.getElementById('cmbPC_STATE').value == 22 || document.getElementById('cmbPC_STATE').value == 49) {
                if (document.getElementById('txtPC_ZIP').value != "") {
                    var intStateID = document.getElementById('cmbPC_STATE').options[document.getElementById('cmbPC_STATE').options.selectedIndex].value;
                    var strZipID = document.getElementById('txtPC_ZIP').value;
                    var result = AddProfitCenter.AjaxFetchZipForState(intStateID, strZipID);
                    return AjaxCallFunction_CallBack_Zip(result);

                }
                return false;
            }
            else
                return true;

        }

        function FormatZipCode(vr) {
           
           
            var vr = new String(vr.toString());
            if (vr != "" && (document.getElementById('cmbPC_COUNTRY').options[document.getElementById('cmbPC_COUNTRY').options.selectedIndex].value == '5')) {

                vr = vr.replace(/[-]/g, "");
                num = vr.length;
                if (num == 8 && (document.getElementById('cmbPC_COUNTRY').options[document.getElementById('cmbPC_COUNTRY').options.selectedIndex].value == '5')) {
                    vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
                    document.getElementById('revPC_ZIP').setAttribute('enabled', false);
                }
               

            }
           
            return vr;
        }


        function zipcodeval() {

            if (document.getElementById('cmbPC_COUNTRY').options[document.getElementById('cmbPC_COUNTRY').options.selectedIndex].value == '6') {
                document.getElementById('revPC_ZIP').setAttribute('enabled', false);
            }
        }

        function zipcodeval1() {

            if (document.getElementById('cmbPC_COUNTRY').options[document.getElementById('cmbPC_COUNTRY').options.selectedIndex].value == '5') {
                document.getElementById('revPC_ZIP').setAttribute('enabled', true);
            }
        }

        
        
        
        function AjaxCallFunction_CallBack_Zip(response) {
            if (document.getElementById('cmbPC_STATE').value == 14 || document.getElementById('cmbPC_STATE').value == 22 || document.getElementById('cmbPC_STATE').value == 49) {
                if (document.getElementById('txtPC_ZIP').value != "") {
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
                    document.getElementById('csvPC_ZIP').innerHTML = "The zip code does not belong to the state";
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
            OpenLookupWithFunction(URL, 'DIV_ADDRESS', 'DIV_ADDRESS', 'hidDIV_ADDRESS', 'hidDIV_ADDRESS', 'CopyDivisionAddress', title, '', 'FetchDivisionAddress()');
            return false;

        }
        function FetchDivisionAddress() {
            var div_address = document.getElementById('hidDIV_ADDRESS').value.split('~');

            if (div_address.length >= 10) {
                document.getElementById('txtPC_ADD1').value = div_address[0];
                document.getElementById('txtPC_ADD2').value = div_address[1];
                document.getElementById('txtPC_CITY').value = div_address[2];
                document.getElementById('txtPC_ZIP').value = div_address[4];

                for (i = 0; i < document.getElementById('cmbPC_COUNTRY').options.length; i++) {

                    if (document.getElementById('cmbPC_COUNTRY').options[i].value == div_address[5])
                        document.getElementById('cmbPC_COUNTRY').options[i].selected = true;

                }


                document.getElementById('txtPC_PHONE').value = div_address[6];
                document.getElementById('txtPC_EXT').value = div_address[7];
                document.getElementById('txtPC_FAX').value = div_address[8];
                document.getElementById('txtPC_EMAIL').value = div_address[9];

                fillstateFromCountry();
                document.getElementById("hidSTATE").value = div_address[3];
                setTimeout("selectedstate()", 300);



            }
        }

        function selectedstate() {
           
            for (j = 0; j < document.getElementById('cmbPC_STATE').options.length; j++) {
              
                if (document.getElementById('cmbPC_STATE').options[j].value == document.getElementById("hidSTATE").value)
                    document.getElementById('cmbPC_STATE').options[j].selected = true;


            }
        }

        //added by sonal
        function fillstateFromCountry() {

            GlobalError = true;
            var CountryID = document.getElementById('cmbPC_COUNTRY').options[document.getElementById('cmbPC_COUNTRY').selectedIndex].value;
            AddProfitCenter.AjaxFillState(CountryID, fillState);
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
                var statesList = document.getElementById("cmbPC_STATE");
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
            document.getElementById("hidExt").value = document.getElementById("txtPC_EXT").value;
            document.getElementById("hidSTATE").value = document.getElementById('cmbPC_STATE').options[document.getElementById('cmbPC_STATE').selectedIndex].value;
           
        }

    </script>

</head>
<body oncontextmenu="return false;" leftmargin="0" topmargin="0" onload="populateXML();ApplyColor();">
    <form id="MNT_PROFIT_CENTER_LIST" method="post" runat="server">
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
                            <asp:Label ID="capPC_NAME" runat="server">Name</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtPC_NAME" runat="server" MaxLength="140" size="30"></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvPC_NAME" runat="server" Display="Dynamic" ErrorMessage="PC_NAME can't be blank."
                                ControlToValidate="txtPC_NAME"></asp:RequiredFieldValidator>
                <%--                <asp:RegularExpressionValidator
                                    ID="revPC_NAME" runat="server" Display="Dynamic" ControlToValidate="txtPC_NAME"></asp:RegularExpressionValidator>--%>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capPC_CODE" runat="server">Code</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtPC_CODE" runat="server" MaxLength="6" size="8"></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvPC_CODE" runat="server" Display="Dynamic" ErrorMessage="PC_CODE can't be blank."
                                ControlToValidate="txtPC_CODE"></asp:RequiredFieldValidator>
                              <%-- <asp:RegularExpressionValidator
                                    ID="revPC_CODE" runat="server" Display="Dynamic" ControlToValidate="txtPC_CODE"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="lblCopy_Address" runat="server"></asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <cmsb:CmsButton class="clsButton" ID="btnCopyDivisionAddress" runat="server" Text="Copy Division Address">
                            </cmsb:CmsButton>
                        </td>
                        <td class="midcolora" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capPC_ADD1" runat="server">Address1</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtPC_ADD1" runat="server" MaxLength="140" size="30"></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvPC_ADD1" runat="server" Display="Dynamic" ErrorMessage="PC_ADD1 can't be blank."
                                ControlToValidate="txtPC_ADD1"></asp:RequiredFieldValidator>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capPC_ADD2" runat="server">Address2</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtPC_ADD2" runat="server" MaxLength="140" size="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capPC_CITY" runat="server">City</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtPC_CITY" runat="server" MaxLength="80" size="30"></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvPC_CITY" runat="server" Display="Dynamic" ErrorMessage="PC_CITY can't be blank."
                                ControlToValidate="txtPC_CITY"></asp:RequiredFieldValidator>
                                <%--<asp:RegularExpressionValidator
                                    ID="revPC_CITY" runat="server" Display="Dynamic" ControlToValidate="txtPC_CITY"></asp:RegularExpressionValidator>--%>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capPC_COUNTRY" runat="server">Country</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbPC_COUNTRY" onfocus="SelectComboIndex('cmbPC_COUNTRY')"
                                runat="server" onchange="javascript:fillstateFromCountry();">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvPC_COUNTRY" runat="server" Display="Dynamic" ErrorMessage="PC_COUNTRY can't be blank."
                                ControlToValidate="cmbPC_COUNTRY"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capPC_STATE" runat="server">State</asp:Label><span id="spnPC_STATE" runat="server" class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbPC_STATE" onfocus="SelectComboIndex('cmbPC_STATE')" runat="server">
                                <asp:ListItem Value='0'></asp:ListItem>
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvPC_STATE" runat="server" Display="Dynamic" ErrorMessage="PC_STATE can't be blank."
                                ControlToValidate="cmbPC_STATE"></asp:RequiredFieldValidator>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capPC_ZIP" runat="server">Zip</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtPC_ZIP" runat="server" MaxLength="8" size="13" OnBlur="this.value=FormatZipCode(this.value);ValidatorOnChange();GetZipForState();zipcodeval();zipcodeval1();"></asp:TextBox>
                            <%-- Added by Swarup on 30-mar-2007 --%>
                            <asp:HyperLink ID="hlkZipLookup" runat="server" CssClass="HotSpot">
                                <asp:Image ID="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif"
                                    ImageAlign="Bottom"></asp:Image>
                            </asp:HyperLink>
                            <br>
                            <asp:CustomValidator ID="csvPC_ZIP" runat="server" ClientValidationFunction="ChkResult"
                                ErrorMessage=" " Display="Dynamic" ControlToValidate="txtPC_ZIP"></asp:CustomValidator>
                            <asp:RequiredFieldValidator ID="rfvPC_ZIP" runat="server" Display="Dynamic" ErrorMessage="PC_ZIP can't be blank."
                                ControlToValidate="txtPC_ZIP"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                    ID="revPC_ZIP" runat="server" Display="Dynamic" ControlToValidate="txtPC_ZIP"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capPC_PHONE" runat="server">Phone</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtPC_PHONE" runat="server" MaxLength="15" size="30" ></asp:TextBox><br>
                            <asp:RegularExpressionValidator ID="revPC_PHONE" runat="server" Display="Dynamic"
                                ControlToValidate="txtPC_PHONE"></asp:RegularExpressionValidator>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capPC_EXT" runat="server">Ext</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtPC_EXT" runat="server" MaxLength="4" size="30" ReadOnly="True"></asp:TextBox><br>
                            <asp:RegularExpressionValidator ID="revPC_EXT" runat="server" Display="Dynamic" ControlToValidate="txtPC_EXT"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capPC_FAX" runat="server">Fax</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtPC_FAX" runat="server" MaxLength="13" size="30" onblur="FormatBrazilPhone()"></asp:TextBox><br>
                            <asp:RegularExpressionValidator ID="revPC_FAX" runat="server" Display="Dynamic" ControlToValidate="txtPC_FAX"></asp:RegularExpressionValidator>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capPC_EMAIL" runat="server">Email</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtPC_EMAIL" runat="server" MaxLength="100" size="30"></asp:TextBox><br>
                            <asp:RegularExpressionValidator ID="revPC_EMAIL" runat="server" Display="Dynamic"
                                ControlToValidate="txtPC_EMAIL"></asp:RegularExpressionValidator>
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
                                class="clsButton" ID="btnSave" runat="server" Text="Save" OnClientClick="saveext()"></cmsb:CmsButton>
                        </td>
                    </tr>
                    <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
                    <input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
                    <input id="hidOldData" type="hidden" name="hidOldData" runat="server">
                    <input id="hidPC_ID" type="hidden" value="0" name="hidPC_ID" runat="server">
                    <input id="hidReset" type="hidden" value="0" name="hidReset" runat="server">
                    <input id="hidDIV_ADDRESS" type="hidden" name="hidDIV_ADDRESS" runat="server">
                    <input id="hidExt" type="hidden" name="hidExt" runat="server">
                     <input id="hidPC_COUNTRY" type="hidden" name="hidPC_COUNTRY" runat="server">
                    <input id="hidSTATE" type="hidden" name="hidSTATE" runat="server">
                     <input id="hidTitle" type="hidden" name="hidTitle" runat="server" />
                   <input  type="hidden" runat="server" ID="hidTAB_TITLES"  value=""  name="hidTAB_TITLES"/> 
                </table>
            </td>
        </tr>
    </table>
    </form>

    <script>
        RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidPC_ID').value, true);
        if (document.getElementById("hidFormSaved").value == "5") {
            /*Record deleted*/
            /*Refreshing the grid and coverting the form into add mode*/
            /*Using the javascript*/
            RemoveTab(2, top.frames[1]);
            RefreshWebGrid("1", "1");
            document.getElementById('hidPC_ID').value = "NEW";
        }
    </script>

</body>
</html>

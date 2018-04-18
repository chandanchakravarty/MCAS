<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNewCustomer.aspx.cs"
    Inherits="Cms.client.Aspx.AddNewCustomer" %>

<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title>Customer Details</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet">

    <script src="/cms/cmsweb/scripts/xmldom.js"></script>

    <script src="/cms/cmsweb/scripts/common.js"></script>

    <script src="/cms/cmsweb/scripts/form.js"></script>

    <script src="/cms/cmsweb/scripts/calendar.js"></script>

    <script language="javascript">
        function formatLastName() {
            document.getElementById('txtCUSTOMER_LAST_NAME').value = 'FormattedValue';
        }
        function formatLastName(ctl) {
            ctl.value = 'FormattedValue1';
        }
        function ValidatePage() {
            //Page_ClientValidate();
            //alert(Page_IsValid);
            if (!Page_IsValid)
                return false;
        }
        function OnCustomerTypeChange() {

            if (document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex == -1)
                document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex = 0;
            if (document.getElementById('cmbCUSTOMER_TYPE').options[document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex].value == '11110') {
                //Type is personal
                document.getElementById("rfvCUSTOMER_FIRST_NAME").innerHTML = document.getElementById("rfvCUSTOMER_FIRST_NAME").getAttribute("ErrMsgFirstName");
                document.getElementById("capCUSTOMER_FIRST_NAME").innerHTML = "Personal Customer";
                //First, middle and last name should be visible
                document.getElementById("tdF_NAME").colSpan = "1";
                document.getElementById("tdF_NAME").style.width = "";
                document.getElementById("tdF_NAME").style.width = "33%";
                document.getElementById("tdM_NAME").style.width = "33%";
                document.getElementById("tdL_NAME").style.width = "33%";
                document.getElementById("tdM_NAME").style.display = "inline";
                document.getElementById("tdL_NAME").style.display = "inline";
                document.getElementById("txtCUSTOMER_FIRST_NAME").size = 35;
            }
            else {
                //Type is commercial

                //Changing error message of validation control
                document.getElementById("rfvCUSTOMER_FIRST_NAME").innerHTML = document.getElementById("rfvCUSTOMER_FIRST_NAME").getAttribute("ErrMsgCustomerName");
                document.getElementById("capCUSTOMER_FIRST_NAME").innerHTML = "Comercial;Custoemr";
                document.getElementById("tdF_NAME").colSpan = "3"
                document.getElementById("tdM_NAME").style.display = "none";
                document.getElementById("tdL_NAME").style.display = "none";
                document.getElementById("tdF_NAME").style.width = "100%";
                document.getElementById("txtCUSTOMER_FIRST_NAME").size = "65";
            }


        }
        function ChkResult(objSource, objArgs) {
            objArgs.IsValid = false;
            return objArgs.IsValid;
        }

        function FormatZipCode(vr) {

            // document.getElementById('revCUSTOMER_ZIP').setAttribute('enabled', true);
            var vr = new String(vr.toString());
            if (vr != "") {

                vr = vr.replace(/[-]/g, "");
                num = vr.length;
                if (num == 8) {
                    vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
                    document.getElementById('revCUSTOMER_ZIP').setAttribute('enabled', false);
                }

            }

            return vr;
        }
    </script>

</head>
<body>
    <form id="CLT_CUSTOMER_LIST" method="post" runat="server">
    <table class="tableWidthHeader" width="95%" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td>
                <table class="tableWidthHeader" align="center" border="0" width="100%">
                    <tr>
                        <td class="pageHeader" colspan="3">
                            <asp:Label ID="capMessages" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolorc" align="right" colspan="3">
                            <asp:Label ID="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolorc" align="right" colspan="3">
                            <asp:Label ID="lblMessage1" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="33%" rowspan="">
                            <asp:Label ID="capIS_ACTIVE" runat="server">Status</asp:Label></br>
                            <asp:Label ID="lblIS_ACTIVE" CssClass="LabelFont" runat="server"></asp:Label>
                        </td>
                        <td class="midcolora" width="34%">
                            <asp:Label ID="capCUSTOMER_TYPE" runat="server">Type</asp:Label><span id="spnCUSTOMER_TYPE"
                                class="mandatory">*</span><br />
                            <asp:DropDownList ID="cmbCUSTOMER_TYPE" onfocus="SelectComboIndex('cmbCUSTOMER_TYPE');"
                                onblur="document.getElementById('imgSelect').focus();" runat="server">
                            </asp:DropDownList>
                            <br>
                            <asp:RequiredFieldValidator ID="rfvCUSTOMER_TYPE" runat="server" ControlToValidate="cmbCUSTOMER_TYPE"
                                Display="Dynamic" Enabled="True"></asp:RequiredFieldValidator>
                        </td>
                        <td class="midcolora" width="33%">
                            <asp:Label ID="capCUSTOMER_PARENT" runat="server">Parent</asp:Label>
                            <br />
                            <asp:TextBox ID="txtCUSTOMER_PARENT_TEXT" runat="server" size="35" ReadOnly="true"></asp:TextBox><a
                                class="calcolora" href="#">
                                <img id="imgSelect" style="cursor: hand" alt="" src="../../cmsweb/images/selecticon.gif"
                                    border="0" autopostback="false" runat="server">
                        </td>
                    </tr>
                    <!--First Name,Middle Name,Last Name -->
                    <tr>
                        <td class="midcolora" width="33%" id="tdF_NAME" runat="server">
                            <asp:Label ID="capCUSTOMER_FIRST_NAME" runat="server">Customer name</asp:Label><span
                                id="spnCUSTOMER_FIRST_NAME" runat="server" class="mandatory">*</span><br />
                            <asp:TextBox ID="txtCUSTOMER_FIRST_NAME" runat="server" CausesValidation="true" MaxLength="100"
                                size="35" AutoCompleteType="Disabled"> </asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvCUSTOMER_FIRST_NAME" runat="server" ControlToValidate="txtCUSTOMER_FIRST_NAME"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td class="midcolora" width="34%" id="tdM_NAME" runat="server">
                            <asp:Label ID="capCUSTOMER_MIDDLE_NAME" runat="server"></asp:Label><br />
                            <asp:TextBox ID="txtCUSTOMER_MIDDLE_NAME" runat="server" size="35" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="midcolora" width="33%" id="tdL_NAME" runat="server">
                            <asp:Label ID="capCUSTOMER_LAST_NAME" runat="server">Last Name</asp:Label><br />
                            <asp:TextBox ID="txtCUSTOMER_LAST_NAME" runat="server" size="35" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="33%">
                            <asp:Label ID="capCUSTOMER_ZIP" runat="server">Zip Code</asp:Label> <br />
                            <asp:TextBox ID="txtCUSTOMER_ZIP" CausesValidation="true" AutoCompleteType="Disabled"
                                runat="server" size="13" MaxLength="8" OnBlur="this.value=FormatZipCode(this.value);ValidatorOnChange();"></asp:TextBox>
                            <br>
                            <asp:CustomValidator ID="csvCUSTOMER_ZIP" runat="server" ClientValidationFunction="ChkResult"
                                ErrorMessage=" " Display="Dynamic" ControlToValidate="txtCUSTOMER_ZIP"></asp:CustomValidator>
                            <asp:RegularExpressionValidator ID="revCUSTOMER_ZIP" runat="server" ControlToValidate="txtCUSTOMER_ZIP" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                        <td class="midcolora" width="34%">
                        </td>
                        <td class="midcolora" width="33%">
                        </td>
                    </tr>
                    <!-- Buttons -->
                    <tr>
                        <td class="midcolora" colspan="2">
                            <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" Text="Reset"></cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnAddNewApplication" runat="server" Text="Add New Application"></cmsb:CmsButton>
                        </td>
                        <td class="midcolorr" colspan="1">
                            <cmsb:CmsButton class="clsButton" ID="btnSave" CausesValidation="true" runat="server"
                                Text="Save123"></cmsb:CmsButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

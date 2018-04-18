<%@ Page Language="c#" CodeBehind="LookupForm2.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.aspx.lookup2.LookupForm2" SmartNavigation="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head xmlns="http://www.w3.org/1999/xhtml">
    <title>LookupForm2</title>
    <%--<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
    <script src="/cms/cmsweb/scripts/form.js" type="text/javascript"></script>
    <script src="/cms/cmsweb/scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet" />

    <script language="javascript" type="text/javascript">

        function OnClick() {
            return false;
        }
        function ChangeDateFormat() {
            var val = document.getElementById('ddlSearchColumn').options[document.getElementById('ddlSearchColumn').selectedIndex].text

            if (val == 'Effective Date' || val == 'Expiration Date') {
                searchDate = FormatDateForGrid(document.getElementById('txtSearch'), document.getElementById('txtSearch').value);
            }
        }

        function ClearFields() {
//            var dataTextField = document.getElementById('hidDataTextFieldID').value;
//            var dataValueField = document.getElementById('hidDataValueFieldID').value;

//            if (dataTextField != null && dataTextField != '') {
//                if (window.opener.document.getElementById(dataTextField) != null) {
//                    var type = window.opener.document.getElementById(dataTextField).type;
//                    var prefix = dataTextField.substring(0, 3);
//                    //alert(prefix);
//                    if (type == 'text' || type == 'hidden') {
//                        window.opener.document.getElementById(dataTextField).value = '';
//                    }

//                    if (type == 'span') {
//                        window.opener.document.getElementById(dataTextField).innerHTML = '';
//                    }

//                    //window.opener.DisableValidatorsById(TextFieldID);

//                }
//            }

//            if (dataValueField != null && dataValueField != '') {
//                if (window.opener.document.getElementById(dataValueField) != null) {
//                    var prefix = dataValueField.substring(0, 3);

//                    var type = window.opener.document.getElementById(dataValueField).type;

//                    //alert(type);
//                    if (type == 'text' || type == 'hidden') {
//                        window.opener.document.getElementById(dataValueField).value = '';
//                    }

//                    if (type == 'span') {
//                        window.opener.document.getElementById(dataValueField).innerHTML = '';
//                    }

//                }
//            }
//            //Added by Mohit Agarwal 17-Jan	
//            if (window.opener.document.title == "ClientList Report") {
//                window.opener.__doPostBack('hidAGENCY_ID', '')
//            }
//            //
//            if (window.opener.document.SendDocument != null) {
//                if (window.opener.document.SendDocument.id == "SendDocument") {
//                    if (dataValueField == "hidCO_APP_NUMBER_ID") {
//                        if (window.opener.document.getElementById('txtCO_APPLICANT') != null) {
//                            window.opener.document.getElementById('txtCO_APPLICANT').value = '';
//                        }
//                    }
//                    if (dataValueField == "hidPOLICY_APP_NUMBER") {
//                        if (window.opener.document.getElementById('txtPOLICY_NUMBER') != null) {
//                            window.opener.document.getElementById('txtPOLICY_NUMBER').value = '';
//                            window.opener.document.getElementById('hidPOLICY_ID').value = '0';
//                            window.opener.document.getElementById('hidPOLICY_VERSION_ID').value = '0';

//                            // Clear thr Claim and party Info too.(As they are linked with the Policy varibales)
//                            window.opener.document.getElementById('txtCLAIM_NUMBER').value = '';
//                            window.opener.document.getElementById('hidCLAIM_ID').value = '0';
//                            window.opener.document.getElementById('hidCLAIM_POLICY_VERSION_ID').value = '0';
//                            window.opener.document.getElementById('txtPARTY_NAME').value = '';
//                            window.opener.document.getElementById('hidPARTY_ID').value = '0';
//                        }
//                    }
//                    if (dataValueField == "hidAPP_NUMBER_ID") {
//                        if (window.opener.document.getElementById('txtAPP_NUMBER') != null) {
//                            window.opener.document.getElementById('txtAPP_NUMBER').value = '';
//                            window.opener.document.getElementById('hidAPP_ID').value = '0';
//                            window.opener.document.getElementById('hidAPP_VERSION_ID').value = '0';
//                        }
//                    }
//                    if (dataValueField == "hidHOLDER_ID") {
//                        if (window.opener.document.getElementById('txtADD_INT') != null) {
//                            window.opener.document.getElementById('txtADD_INT').value = '';
//                            window.opener.document.getElementById('hidHOLDER_ID').value = '0';

//                        }
//                    }
//                    if (dataValueField == "hidCLAIM_POLICY_NUMBER") {
//                        if (window.opener.document.getElementById('txtCLAIM_NUMBER') != null) {
//                            window.opener.document.getElementById('txtCLAIM_NUMBER').value = '';
//                            window.opener.document.getElementById('hidCLAIM_ID').value = '0';
//                            window.opener.document.getElementById('hidCLAIM_POLICY_VERSION_ID').value = '0';

//                            //Also Clear the Policy Info.
//                            window.opener.document.getElementById('txtPOLICY_NUMBER').value = '';
//                            window.opener.document.getElementById('hidPOLICY_ID').value = '0';
//                            window.opener.document.getElementById('hidPOLICY_VERSION_ID').value = '0';


//                            //Also Clear the Party Info.
//                            if (window.opener.document.getElementById('txtPARTY_NAME') != null) {
//                                window.opener.document.getElementById('txtPARTY_NAME').value = '';
//                                window.opener.document.getElementById('hidPARTY_ID').value = '0';
//                            }

//                        }
//                    }
//                    if (dataValueField == "hidPARTY_ID") {
//                        //Clear the Party Info.
//                        if (window.opener.document.getElementById('txtPARTY_NAME') != null) {
//                            window.opener.document.getElementById('txtPARTY_NAME').value = '';
//                            window.opener.document.getElementById('hidPARTY_ID').value = '0';
//                        }
//                    }
//                }
//            }
            //            window.close();

            document.getElementById('txtSearch').value = "";
        }

        function SetTitle() {
            if (window.opener.lookupTitle == null) {
                document.title = 'Lookup';
                if (document.Form1.spnHeader)
                    document.Form1.spnHeader.innerHTML = 'Lookup';
                return;
            }

            if (window.opener.lookupTitle == '') {
                document.title = 'Lookup';
                document.Form1.spnHeader.innerHTML = 'Lookup';
                return;
            }

            document.title = window.opener.lookupTitle;

            document.getElementById("spnHeader").innerHTML = window.opener.lookupTitle;
        }

        function OnDoubleClick(TextFieldID, ValueFieldID, TextFieldValue, ValueFieldValue, JSFunction) {
          
            if (window.opener.document.getElementById(TextFieldID) != null) {
                var type = window.opener.document.getElementById(TextFieldID).type;
                var prefix = TextFieldID.substring(0, 3);
            
                //alert(prefix);
                if (type == 'text' || type == 'hidden') {
                    window.opener.document.getElementById(TextFieldID).value = TextFieldValue.replace(/\s+$/, ""); //replace added by Charles on 7-Sep-09 for Itrack 6296 to remove right spaces
                }

                if (type == 'span') {
                    window.opener.document.getElementById(TextFieldID).innerHTML = TextFieldValue;
                }
                //Added By Pradeep: Dec 9,2005
                if (type == 'text') {
                    //if text box is disabled then do not focus:						
                    if (window.opener.document.getElementById(TextFieldID).getAttribute('disabled') != true)
                        window.opener.document.getElementById(TextFieldID).focus();
                }

                window.opener.DisableValidatorsById(TextFieldID);
            }

            if (window.opener.document.getElementById(ValueFieldID) != null) {
                var prefix = ValueFieldID.substring(0, 3);

                var type = window.opener.document.getElementById(ValueFieldID).type;

                //alert(type);
                if (type == 'text' || type == 'hidden') {
                    window.opener.document.getElementById(ValueFieldID).value = ValueFieldValue.replace(/\s+$/, ""); //replace added by Charles on 7-Sep-09 for Itrack 6296 to remove right spaces
                }

                if (type == 'span') {
                    window.opener.document.getElementById(ValueFieldID).innerHTML = ValueFieldValue;
                }

            }

            if (JSFunction != '') {
                eval('window.opener.' + JSFunction);
            }
            if (JSFunction != 'openDecPages()' && JSFunction != 'openAutoIdCard()')
                window.close();

        }

        function changecursor(obj) {
            obj.style.cursor = "hand";
        }

        function OnKeyDown() {
            if (event.keyCode == 13) {
                if (event.srcElement.type != 'submit' && event.srcElement.type != 'textarea') {
                    __doPostBack('btnSearch', 'btnSearch');
                    return false;
                }
            }
        }

        function fillDropDown() {
           
            var val = document.getElementById('ddlSearchColumn').options[document.getElementById('ddlSearchColumn').selectedIndex].value.split(';');
            var show = false;
            if (val) {

                for (var i = 0; i < val.length; i++) {
                    if (val[i] == "intDDL") {
                        show = true;
                        break;
                    }
                }
            }
            
            if (show) {
                document.getElementById('cmbSearch').style.display = 'inline'
                document.getElementById('txtSearch').style.display = 'none'

                if (document.getElementById('hidParam').value != "" && document.getElementById('hidParam').value != "0") {
                    AjaxGetDDL(document.getElementById('hidParam').value, document.getElementById('cmbSearch'));
                }
            }
            else {
                document.getElementById('cmbSearch').style.display = 'none'
                document.getElementById('txtSearch').style.display = 'inline'
            }
        }

        function AjaxGetDDL(type, combo) {
           
                var result = LookupForm2.AjaxFetchDDL(type);
                return AjaxReturn(result, combo, type);            
        }
        function AjaxReturn(objDS, combo, type) {        
            
            if(objDS!=null) {

                var types = type.split('^');               
                
                for (i = 0; i < objDS.value.Tables[0].Rows.length; i++) {

                    if (i == 0) {
                        oOption = document.createElement("option");
                        oOption.value = "";
                        oOption.text = "";
                        combo.add(oOption);   
                    }
                    oOption = document.createElement("option");
                    oOption.value = objDS.value.Tables[0].Rows[i][types[1]];
                    oOption.text = objDS.value.Tables[0].Rows[i][types[2]];
                    combo.add(oOption);


                }
                SelectComboOption("cmbSearch", document.getElementById('hidSearch').value);               
            }
        }

        function SetHidVal() {
            document.getElementById('hidSearch').value = document.getElementById('cmbSearch').options[document.getElementById('cmbSearch').selectedIndex].value;
        }
    </script>

</head>
<body oncontextmenu="javascript:return false;" onload="fillDropDown();" onkeydown="javascript:OnKeyDown();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table width="100%">
        <tr>
            <td class="headereffectCenter" colspan="2">
                <span id="spnHeader"></span>
            </td>
        </tr>
        <tr>
            <td class="midcolora" style="height: 44px">
                <span id="spnOption"><asp:Label ID="capSearch_option" runat="server"></asp:Label>  </span><%--Search Option--%>
                <asp:DropDownList ID="ddlSearchColumn" runat="server">
                </asp:DropDownList>
            </td>
            <td class="midcolora" style="height: 38px">
                <span id="spnCriteria"><asp:Label ID="capSEARCH_CRITERIA" runat="server"></asp:Label>  </span><%--Search Criteria--%>
                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                <asp:DropDownList ID="cmbSearch" onfocus="SelectComboIndex('cmbSearch')" onchange="javascript:SetHidVal();" runat="server" style="display:none"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="midcolora">
                <asp:Button ID="btnReset" runat="server" CssClass="clsButton" Text=""></asp:Button><%--Reset--%>
            </td>
            <td class="midcolorr" align="right">
                <asp:Button ID="btnSearch" runat="server" CssClass="clsButton" Text="Search"></asp:Button>
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td align="center">
                <asp:DataGrid ID="dgLookup" runat="server" AllowPaging="False" AutoGenerateColumns="False" Width="100%" AllowSorting="True">
                    <AlternatingItemStyle CssClass="AlternateDataRow"></AlternatingItemStyle>
                    <ItemStyle CssClass="DataRow"></ItemStyle>
                    <HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
                    <Columns>
                        <asp:BoundColumn Visible="False"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>
        <tr class="AlternateDataRow">
            <td valign="middle" align="center">
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="btnFirst" ImageUrl="../images/firstoff.gif" runat="server"></asp:ImageButton>
                            <asp:ImageButton ID="btnPrevious" ImageUrl="../images/prevoff.gif" runat="server"></asp:ImageButton>
                            <asp:Label ID="lblCurrentPage" runat="server"></asp:Label>
                            <asp:ImageButton ID="btnNext" ImageUrl="../images/next.gif" runat="server"></asp:ImageButton>
                            <asp:ImageButton ID="btnLast" ImageUrl="../images/last.gif" runat="server"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <input class="clsButton" id="btn_Close" onclick="javascript:window.close();" type="button" value="Close" runat="server" />
                <input class="clsButton" id="btn_ClearField"  onclick="javascript:ClearFields();" type="button" value="Clear Field" runat ="server" />
                <input id="hidDataValueFieldID" type="hidden" runat="server" />
                <input id="hidDataTextFieldID" type="hidden" runat="server" />
                <input id="hidParam" type="hidden" runat="server" />
                <input id="hidSearch" type="hidden" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

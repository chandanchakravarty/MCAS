<%@ Page Language="c#" CodeBehind="NewBusinessProcess.aspx.cs" AutoEventWireup="false"
    Inherits="Cms.Policies.Processes.NewBusinessProcess" ValidateRequest="false" %>

<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="PolicyTop" Src="/cms/cmsweb/webcontrols/PolicyTop.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>NewBusinessProcess</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet">

    <script src="/cms/cmsweb/scripts/xmldom.js"></script>

    <script src="/cms/cmsweb/scripts/common.js"></script>

    <script src="/cms/cmsweb/scripts/form.js"></script>

    <script src="/cms/cmsweb/scripts/Calendar.js"></script>

    <script language="javascript">

        //Setting the menu
        //This function will be called after starting the new business process
        //using RegisterStartupScript method
        var refSubmitWin;
        function setMenu() {

            //IF menu on top frame is not ready then
            //menuXmlReady will false
            //If menu is not ready, we will again call setmenu again after 1 sec
            if (top.topframe.main1.menuXmlReady == false)
                setTimeout("setMenu();", 1000);


            //Enabling or disabling menus
            top.topframe.main1.activeMenuBar = '1';
            top.topframe.createActiveMenu();
            top.topframe.enableMenus("1", "ALL");
            top.topframe.enableMenu("1,1,1");
            top.topframe.enableMenu("1,1,2");

            //Changed by Lalit Feb 25,2011
            //i-track # 862
            if ('<%=GetPolicyStatus().Trim().ToUpper() %>' != '<%=  Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_REJECT.ToString()%>'
	    && '<%=GetPolicyStatus().Trim().ToUpper() %>' != '<%=  Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_UNDER_ISSUE.ToString()%>') {
                top.topframe.enableMenu("1,2,3");
            }

            //top.topframe.enableMenu("1,2,3");
            //Done for Itrack Issue 6749 on 5 Jan 2010
            //top.topframe.enableMenu("1,7");//Done for Itrack Issue 6749 on 30 Nov 09
            if ('<%=Status %>' != 'REJECT') {
                if (top.topframe.main1.menuXmlReady == false)
                    setTimeout("top.topframe.enableMenus('1,7','ALL');", 1000);
                else
                    top.topframe.enableMenus('1,7', 'ALL');
            }
        }
        //        Shikha 
        function Installment_Result() {
            var strCOUNT = document.getElementById('hidCOUNT').value;
            if (strCOUNT == "1") {

                return confirm(document.getElementById('hidpopup').value);
            }
        }

        function AddData() {
            document.getElementById('hidROW_ID').value = 'New';
            document.getElementById('txtCOMMENTS').focus();
            document.getElementById('txtCOMMENTS').value = '';
            document.getElementById('txtNO_COPIES').value = '';
            document.getElementById('chkPRINTING_OPTIONS').checked = false;
            document.getElementById('cmbINSURED').selectedIndex = -1;
            //document.getElementById('cmbSTD_LETTER_REQD').selectedIndex = -1;
            document.getElementById('cmbAGENCY_PRINT').selectedIndex = -1;
            document.getElementById('cmbSEND_INSURED_COPY_TO').selectedIndex = -1;
            //document.getElementById('cmbCUSTOM_LETTER_REQD').selectedIndex = -1;
            document.getElementById('cmbAUTO_ID_CARD').selectedIndex = -1;
            //document.getElementById('cmbSEND_INSURED_COPY_TO').selectedIndex = -1;

            DisableValidators();
            ChangeColor();
        }
        function populateXML() {

            var tempXML;
            if (document.getElementById("hidOldData").value != "") {
                populateFormData(document.getElementById("hidOldData").value, NewBusinessProcess);
            }
            else {
                AddData();
            }

            return false;
        }

        //Validates the maximum length for comments
        function txtCOMMENTS_VALIDATE(source, arguments) {
            var txtArea = arguments.Value;
            if (txtArea.length > 500) {
                arguments.IsValid = false;
                return false;
            }
        }

        function DisplayBody() {
            if (document.getElementById('hidDisplayBody').value == "True") {
                document.getElementById('trBody').style.display = 'inline';
            }
            else {
                document.getElementById('trBody').style.display = 'none';
            }

        }
        function cmbADD_INT_Change() {
            combo = document.getElementById('cmbADD_INT');
            if (combo == null || combo.selectedIndex == -1)
                return false;

            if (combo.options[combo.selectedIndex].value == "<%=((int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString()%>"
			  || combo.options[combo.selectedIndex].value == "<%=((int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString()%>") {
                document.getElementById('chkSEND_ALL').style.display = "inline";
                document.getElementById('capSEND_ALL').style.display = "inline";
                //document.getElementById('trAddIntList').style.display="inline";				
                chkSEND_ALL_Change();
            }
            else {
                document.getElementById('chkSEND_ALL').style.display = "none";
                document.getElementById('capSEND_ALL').style.display = "none";
                document.getElementById('trAddIntList').style.display = "none";
                //document.getElementById('hidADD_INT_ID').value = '';
            }
            return false;
        }
        function ResetTheForm() {
            DisableValidators();
            Init();
            return false;
        }

        function ShowDetailsPolicy() {
            //top.botframe.callItemClicked('1,2,3','/cms/policies/aspx/policytab.aspx?CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VERSION_ID=' + document.getElementById('hidPOLICY_VERSION_ID').value + '&');
            window.open('/Cms/Application/Aspx/DecPage.aspx?CALLEDFOR=DECPAGE&CALLEDFROM=POLICY&CUSTOMER_ID=' + document.getElementById('hidCUSTOMER_ID').value + '&POLICY_ID=' + document.getElementById('hidPOLICY_ID').value + '&POLICY_VER_ID=' + document.getElementById('hidPOLICY_VERSION_ID').value + '&');
            return false;
        }
        function Init() {
            showScroll();
            top.topframe.main1.mousein = false;
            findMouseIn();
            populateXML();
            AutoIdCardDisplay();
            DisplayBody();
            cmbADD_INT_Change();
            //chkSEND_ALL_Change();
            //ShowHideAddIntCombos(true);
            SetAssignAddInt();
            ApplyColor();
            ChangeColor();
            document.getElementById('btnCommitInProgress').style.display = "none";
            document.getElementById('btnCommitAnywayInProgress').style.display = "none";
            //Added for Itrack Issue 6203 on 31 July 2009

            if ('<%=Status %>' != 'REJECT') {
                if (top.topframe.main1.menuXmlReady == false)
                    setTimeout("top.topframe.enableMenus('1,7','ALL');", 1000);
                else
                    top.topframe.enableMenus('1,7', 'ALL');
            }
            //Added by Lalit April 13,2011
            //id user is not supervisor check box for visible commitAnyway button shoud not visible
            if ('<%=getIsUserSuperVisor().Trim().ToUpper() %>' != 'Y')
                if (document.getElementById('chkComitAynway') != null) {
                    document.getElementById('chkComitAynway').style.display = 'none';
                    document.getElementById('chkComitAynway').nextSibling.innerHTML = ''; //remove lable text which is used for check box desxription of chkcommitAnyway
                    
            }
        }
        function AutoIdCardDisplay() {
            if (document.getElementById('hidLOB_ID').value == '<%=((int)Cms.CmsWeb.cmsbase.enumLOB.AUTOP).ToString()%>' || document.getElementById('hidLOB_ID').value == '<%=((int)Cms.CmsWeb.cmsbase.enumLOB.CYCL).ToString()%>') {
                document.getElementById('trAutoIdHeader').style.display = "inline";
                document.getElementById('trAutoIdControls').style.display = "inline";
            }
            else {
                document.getElementById('trAutoIdHeader').style.display = "none";
                document.getElementById('trAutoIdControls').style.display = "none";
            }

        }

        function AssignAddInt(flag) {
            var coll = document.getElementById('cmbUnAssignAddInt');
            var selIndex = coll.options.selectedIndex;
            var len = coll.options.length;
            if (len < 1) return;
            var num = 0;
            for (var i = len - 1; i > -1; i--) {
                if (coll.options(i).selected == true || flag) {
                    num = i;
                    var szSelectedDept = coll.options(i).value;
                    var innerText = coll.options(i).text;
                    document.getElementById('cmbAssignAddInt').options[document.getElementById('cmbAssignAddInt').length] = new Option(innerText, szSelectedDept)
                    coll.remove(i);
                }
            }
            len = coll.options.length;
            if (num < len) {
                document.getElementById('cmbUnAssignAddInt').options(num).selected = true;
            }
            else {
                if (num > 0)
                    document.getElementById('cmbUnAssignAddInt').options(num - 1).selected = true;
            }

        }
        function UnAssignAddInt(flag) {
            var UnassignableString = "";
            var Unassignable = UnassignableString.split(",");
            var gszAssignedString = "";
            var Assigned = gszAssignedString.split(",");
            var coll = document.getElementById('cmbAssignAddInt');
            var selIndex = coll.options.selectedIndex;
            var len = coll.options.length;
            var num = 0;
            if (len == 0) return;

            for (var i = len - 1; i > -1; i--) {
                if (coll.options(i).selected == true || flag) {
                    num = i;
                    var flag = true;
                    var szSelectedDept = coll.options(i).value;
                    var innerText = coll.options(i).text;
                    for (j = 0; j < Unassignable.length; j++) {
                        for (k = 0; k < Assigned.length; k++) {
                            if ((szSelectedDept == Unassignable[j]) && (szSelectedDept == Assigned[k])) {
                                flag = false;
                            }
                        }
                    }

                    if (flag == true) {
                        document.getElementById('cmbUnAssignAddInt').options[document.getElementById('cmbUnAssignAddInt').length] = new Option(innerText, szSelectedDept)
                        coll.remove(i);

                    }
                    /*else
                    {
                    alert("Cannot unassign Department "+innerText+" because some divisions are associasted with it");
                    }*/
                }
            }
            var len = coll.options.length;
            if (len < 1) return;
            if (num < len) {
                document.getElementById('cmbAssignAddInt').options(num).selected = true;
            }
            else {
                document.getElementById('cmbAssignAddInt').options(num - 1).selected = true;
            }

        }
        function chkSEND_ALL_Change() {
            var chk = document.getElementById('chkSEND_ALL');
            if (chk == null)
                return false;
            //document.getElementById("hidADD_INT_ID").value='';
            if (chk.checked == true) {
                ShowHideAddIntCombos(true);
                AssignAddInt(true);
            }
            else {
                ShowHideAddIntCombos(false);
                UnAssignAddInt(true);
            }

            return false;
        }
        function ShowHideAddIntCombos(flag) {
            /*var chk = document.getElementById('chkSEND_ALL');
            if(chk==null)
            return false;	*/

            if (flag)
                document.getElementById('trAddIntList').style.display = "none";
            else
                document.getElementById('trAddIntList').style.display = "inline";
        }
        function GetAssignAddInt() {
            //btnTextChange();

            //	return;
            //alert("GetAssignAddInt");
            document.getElementById("hidADD_INT_ID").value = "";
            var coll = document.getElementById('cmbAssignAddInt');
            var len = coll.options.length;
            for (var k = 0; k < len; k++) {
                var szSelectedDept = coll.options(k).value;
                if (document.getElementById("hidADD_INT_ID").value == "") {
                    document.getElementById("hidADD_INT_ID").value = szSelectedDept;
                }
                else {
                    document.getElementById("hidADD_INT_ID").value = document.getElementById("hidADD_INT_ID").value + "~" + szSelectedDept;
                }
            }


        }
        function HideShowCommitInProgress() {
            Page_ClientValidate();
            if (!Page_IsValid) {
                return false;
            }
            else {
                document.getElementById('btnCommitInProgress').style.display = "inline";
                document.getElementById('btnCommitInProgress').disabled = true;
                document.getElementById('btnCommit').style.display = "none";
                document.getElementById('btnSave').disabled = true;
                if (document.getElementById('btnComitAynway'))
                    document.getElementById('btnComitAynway').disabled = true;
                if (document.getElementById('btnRollback'))
                    document.getElementById('btnRollback').disabled = true;
            }
            DisableButton(document.getElementById('btnCommit'));
            top.topframe.disableMenus("1,7", "ALL"); //Added for Itrack Issue 6203 on 31 July 2009
        }
        function HideShowCommitAnywayInProgress() {
            Page_ClientValidate();
            if (!Page_IsValid) {
                return false;
            }
            else {
                document.getElementById('btnCommitAnywayInProgress').style.display = "inline";
                document.getElementById('btnCommitAnywayInProgress').disabled = true;
                document.getElementById('btnComitAynway').style.display = "none";
                document.getElementById('btnSave').disabled = true;
                document.getElementById('btnCommit').disabled = true;
                if (document.getElementById('btnRollback'))
                    document.getElementById('btnRollback').disabled = true;
            }
            DisableButton(document.getElementById('btnComitAynway'));
            top.topframe.disableMenus("1,7", "ALL"); //Added for Itrack Issue 6203 on 31 July 2009

        }
        function HideShowCommitAnywayButton() {
            if (document.getElementById('chkComitAynway').checked == true && document.getElementById('hidCALLED_FOR').value != "ROLLBACK")
                if (document.getElementById('btnComitAynway') != null)
                document.getElementById('btnComitAynway').style.display = "inline";
            else
                if (document.getElementById('btnComitAynway') != null)
                document.getElementById('btnComitAynway').style.display = "none";
        }
        function HideShowCommit() {
            document.getElementById('btnCommit').disabled = true;
            document.getElementById('btnSave').disabled = true;
            if (document.getElementById('btnComitAynway'))
                document.getElementById('btnComitAynway').disabled = true;
            DisableButton(document.getElementById('btnRollback'));

        }


        function SetAssignAddInt() {
            if (document.getElementById("hidADD_INT_ID").value == '' || document.getElementById("hidADD_INT_ID").value == '0')
                return;
            var selectedAddInt = new String(document.getElementById("hidADD_INT_ID").value);
            var selectedAddIntArr = selectedAddInt.split('~');
            if (selectedAddIntArr == null || selectedAddIntArr.length < 1)
                return;

            var coll = document.getElementById('cmbUnAssignAddInt');
            var selIndex = coll.options.selectedIndex;
            var len = coll.options.length;
            var arrLen = selectedAddIntArr.length;
            if (len < 1) return;
            var num = 0;
            for (var j = 0; j < arrLen; j++) {
                for (var i = len - 1; i > -1; i--) {
                    if (coll.options(i).value == selectedAddIntArr[j]) {
                        num = i;
                        var szSelectedDept = coll.options(i).value;
                        var innerText = coll.options(i).text;
                        document.getElementById('cmbAssignAddInt').options[document.getElementById('cmbAssignAddInt').length] = new Option(innerText, szSelectedDept)
                        coll.remove(i);
                        len = coll.options.length;
                    }
                }
            } /*	
				len = coll.options.length;	
				if(	num < len )
				{
					document.getElementById('cmbUnAssignAddInt').options(num).selected = true;
				}	
				else
				{
					if(num>0)
						document.getElementById('cmbUnAssignAddInt').options(num - 1).selected = true;
				}	*/
        }

        /*function btnTextChange()	
        {
			
				if(document.getElementById("btnCommit").value =="Commit")
        {
        document.NewBusinessProcess.submit(); 
        document.getElementById("btnCommit").value ="Wait for Commit";
        return Page_IsValid;
        }
        else if(document.getElementById("btnCommit").value =="Wait for Commit")
        {
        alert("Commit Process is in progress. Please wait");
        return false;
        }
				
			}*/
			
    </script>

</head>
<body oncontextmenu="return false;" leftmargin="0" topmargin="0" onload="Init();"
    scroll="yes">
    <form id="NewBusinessProcess" method="post" runat="server">
    <div>
        <webcontrol:menu id="bottomMenu" runat="server">
        </webcontrol:menu></div>
    <!-- To add bottom menu -->
    <!-- To add bottom menu ends here -->
    <table cellspacing="0" cellpadding="0" width="90%" align="center" border="0">
        <tr>
            <td>
                <webcontrol:gridspacer id="grdSpacer" runat="server">
                </webcontrol:gridspacer>
            </td>
        </tr>
        <tr>
            <td>
                <webcontrol:policytop id="cltPolicyTop" runat="server">
                </webcontrol:policytop>
            </td>
        </tr>
        <tr>
            <td>
                <webcontrol:gridspacer id="Gridspacer1" runat="server">
                </webcontrol:gridspacer>
            </td>
        </tr>
        <tr>
            <td class="headereffectcenter">
                <asp:Label ID="capHeader" runat="server"></asp:Label>
            </td>
            <%--New Business Process--%>
        </tr>
        <tr>
            <td id="tdGridHolder">
                <webcontrol:gridspacer id="Gridspacer2" runat="server">
                </webcontrol:gridspacer><asp:PlaceHolder ID="GridHolder" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td class="pageHeader" colspan="4">
                <asp:Label ID="capMessage" runat="server"></asp:Label>
            </td>
            <%--Please note that all fields marked with * are mandatory--%>
        </tr>
        <tr>
            <td class="midcolorc" align="right" colspan="4">
                <asp:Label ID="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
            </td>
        </tr>
        <tr id="trBody">
            <td>
                <table width="100%" align="center" border="0">
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capCOMMENTS" runat="server">Comments</asp:Label>
                        </td>
                        <td class="midcolora" colspan="3">
                            <asp:TextBox ID="txtCOMMENTS" runat="server" Rows="5" Columns="50" TextMode="MultiLine"></asp:TextBox><br>
                            <asp:CustomValidator ID="csvCOMMENTS" ControlToValidate="txtCOMMENTS" Display="Dynamic"
                                ClientValidationFunction="txtCOMMENTS_VALIDATE" runat="server"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="headerEffectSystemParams" colspan="4">
                            <asp:Label ID="capPrinting" runat="server"></asp:Label>
                        </td>
                        <%--Printing Options Details--%>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capPRINTING_OPTIONS" runat="server">No printing Required at all</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:CheckBox ID="chkPRINTING_OPTIONS" runat="server"></asp:CheckBox>
                        </td>
                        <td colspan="2" class="midcolora">
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capINSURED" runat="server">Insured</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbINSURED" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capSEND_INSURED_COPY_TO" runat="server">Send Insured Copy To</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbSEND_INSURED_COPY_TO" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capAGENCY_PRINT" runat="server">Agency</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbAGENCY_PRINT" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td colspan="2" class="midcolora">
                        </td>
                    </tr>
                    <tr>
                        <td class="headerEffectSystemParams" colspan="4">
                            <asp:Label ID="capAdditional" runat="server"></asp:Label>
                        </td>
                        <%--Additional Interest Details--%>
                    </tr>
                    <tr>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capADD_INT" runat="server">Additional Interest</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbADD_INT" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capSEND_ALL" runat="server">Send to All</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:CheckBox ID="chkSEND_ALL" runat="server"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr id="trAddIntList">
                        <td colspan="4" class="midcolora">
                            <table>
                                <tr>
                                    <td class="midcolorc" align="center" width="37%">
                                        <asp:Label ID="capUnassignLossCodes" runat="server">All Additional Interests</asp:Label>
                                    </td>
                                    <td class="midcolorc" valign="middle" align="center" width="33%" rowspan="7">
                                        <br>
                                        <br>
                                        <input class="clsButton" runat="server" onclick="javascript:AssignAddInt(false);"
                                            type="button" value=">>" name="btnAssignLossCodes" id="btnAssignLossCodes"><br>
                                        <br>
                                        <input class="clsButton" runat="server" onclick="javascript:UnAssignAddInt(false);"
                                            type="button" value="<<" name="btnUnAssignLossCodes" id="btnUnAssignLossCodes">
                                    </td>
                                    <td class="midcolorc" align="center" width="33%">
                                        <asp:Label ID="capAssignedLossCodes" runat="server">Selected Additional Interests</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="midcolorc" align="center" width="33%" rowspan="6">
                                        <asp:ListBox ID="cmbUnAssignAddInt" runat="server" Width="300px" Height="150px" SelectionMode="Multiple">
                                        </asp:ListBox>
                                    </td>
                                    <td class="midcolorc" align="center" width="33%" rowspan="6">
                                        <asp:ListBox ID="cmbAssignAddInt" runat="server" Width="300px" Height="150px" SelectionMode="Multiple">
                                        </asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trAutoIdHeader">
                        <td class="headerEffectSystemParams" colspan="4">
                            Auto ID Card Details
                        </td>
                    </tr>
                    <tr id="trAutoIdControls">
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capAUTO_ID_CARD" runat="server">Auto ID Card</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:DropDownList ID="cmbAUTO_ID_CARD" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="midcolora" width="18%">
                            <asp:Label ID="capNO_COPIES" runat="server">No. of Copies</asp:Label>
                        </td>
                        <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtNO_COPIES" runat="server" MaxLength="3" size="6"></asp:TextBox><br>
                            <asp:RangeValidator ID="rngNO_COPIES" Display="Dynamic" ControlToValidate="txtNO_COPIES"
                                runat="server" Type="Integer" MinimumValue="0" MaximumValue="999"></asp:RangeValidator>
                        </td>
                    </tr>
                    <!--
							<tr>
								<TD class="headerEffectSystemParams" colSpan="4">Letter</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSTD_LETTER_REQD" runat="server">Standard Letter Required</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:DropDownList ID="cmbSTD_LETTER_REQD" Runat="server"></asp:DropDownList></td>
								<TD class="midcolora" width="18%"><asp:label id="capCUSTOM_LETTER_REQD" runat="server">Customized Letter Required</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:DropDownList ID="cmbCUSTOM_LETTER_REQD" Runat="server"></asp:DropDownList></td>
							</tr> -->
                    <!-- cms buttons  -->
                    <tr>
                        <td class="midcolora" align="right" colspan="2">
                            <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" Text="Reset"></cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnRollback" runat="server" Text="Reject">
                            </cmsb:CmsButton><%--<cmsb:cmsbutton class="clsButton" id="btnDecline" runat="server" Text="Decline Policy"></cmsb:cmsbutton>--%><cmsb:CmsButton
                                class="clsButton" ID="btnPolicyDetails" Style="display: none" runat="server"
                                Text="View Dec Page"></cmsb:CmsButton><cmsb:CmsButton class="clsButton" ID="btnGenerate_Policy"
                                    runat="server" Text="Generate Policy" Style="display: none"></cmsb:CmsButton><br>
                            <cmsb:CmsButton class="clsButton" ID="btnBack_To_Search" runat="server" Text="Back To Search">
                            </cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnBack_To_Customer_Assistant" runat="server"
                                Text="Back To Customer Assistant"></cmsb:CmsButton>
                        </td>
                        <td class="midcolorr" align="left" colspan="2">
                            <cmsb:CmsButton class="clsButton" ID="btnPrint_Preview" runat="server" Text="Print Preview"
                                Visible="false"></cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnCommit" runat="server" Text="Commit"></cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnCommitInProgress" runat="server" Text="Commit in Progress">
                            </cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnGet_Premium" Style="display: inline" runat="server"
                                Text="Get Premium"></cmsb:CmsButton>
                            <cmsb:CmsButton class="clsButton" ID="btnSave" runat="server" Text="Save"></cmsb:CmsButton><br>
                            <cmsb:CmsButton class="clsButton" ID="btnRescind" runat="server" Text="Rescind" Visible="False">
                            </cmsb:CmsButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <webcontrol:gridspacer id="Gridspacer3" runat="server">
                </webcontrol:gridspacer>
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="90%" align="center" border="0">
        <tr>
            <td class="headereffectcenter" colspan="4">
                <span id="spnURStatus" runat="server">Underwriting Rules Status</span>
            </td>
        </tr>
        <tr>
            <td>
                <div id="myDIV" runat="server" align="center" class="midcolora" style="overflow: auto;
                    width: 100%; height: 189px">
                </div>
            </td>
        </tr>
        <tr>
            <td class="midcolorr" align="left" colspan="4">
                <asp:CheckBox ID="chkComitAynway" runat="server" Checked="False"></asp:CheckBox><br>
                <cmsb:CmsButton class="clsButton" ID="btnComitAynway" runat="server" Text="Commit Anyway">
                </cmsb:CmsButton>
                <cmsb:CmsButton class="clsButton" ID="btnCommitAnywayInProgress" runat="server" Text="Commit in Progress">
                </cmsb:CmsButton>
            </td>
        </tr>
    </table>
    <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
    <input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
    <input id="hidCALLED_FROM" type="hidden" name="hidCALLED_FROM" runat="server">
    <input id="hidVEHICLE_ID" type="hidden" value="0" name="hidVEHICLE_ID" runat="server">
    <input id="hidOldData" type="hidden" name="hidOldData" runat="server">
    <input id="hidROW_ID" type="hidden" name="hidROW_ID" runat="server">
    <input id="hidPOLICY_ID" type="hidden" name="hidPOLICY_ID" runat="server">
    <input id="hidPOLICY_VERSION_ID" type="hidden" name="hidPOLICY_VERSION_ID" runat="server">
    <input id="hidCUSTOMER_ID" type="hidden" name="hidCUSTOMER_ID" runat="server">
    <input id="hidPROCESS_ID" type="hidden" name="hidPROCESS_ID" runat="server">
    <input id="hidDisplayBody" type="hidden" name="hidDisplayBody" runat="server">
    <input id="hidLOB_ID" type="hidden" name="hidLOB_ID" runat="server" value="0">
    <input id="hidADD_INT_ID" type="hidden" name="hidADD_INT_ID" runat="server">
    <input id="hidUNDERWRITER" type="hidden" name="hidUNDERWRITER" runat="server">
    <input id="hidAPP_EFFECTIVE_DATE" type="hidden" name="hidAPP_EFFECTIVE_DATE" runat="server">
    <input id="hidCALLED_FOR" type="hidden" name="hidCALLED_FOR" runat="server">
    <input id="hidCOUNT" type="hidden" name="hidCOUNT" runat="server">
    <INPUT id="hidpopup" type="hidden" name="hidpopup" runat="server">
    </form>
</body>
</html>

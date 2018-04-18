<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyUWQMarine.aspx.cs" Inherits="Cms.Policies.Aspx.MariTime.PolicyUWQMarine" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="~/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>PolicyUWQMarine</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet">
    <script src="/cms/cmsweb/scripts/xmldom.js"></script>
    <script src="/cms/cmsweb/scripts/common.js"></script>
    <script src="/cms/cmsweb/scripts/form.js"></script>
    <script src="/cms/cmsweb/scripts/Calendar.js"></script>
    <script language="javascript">

        function AddData() {
            document.getElementById('cmbANY_PRE_INSPECTION_DONE').options.selectedIndex = 0;
            document.getElementById('hidDETAIL_TYPE_ID').value = 'New';          
        }
        function populateXML() {
            var tempXML;
            tempXML = document.getElementById('hidOldData').value;
                        
            if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')) {

                if (tempXML != "" && tempXML != 0 && tempXML != "<NewDataSet />") {
                    populateFormData(tempXML, PolicyUWQMarine);
                }

                else {
                    AddData();
                }
            }           
            return false;
        }
        function Reset() {
                document.PolicyUWQMarine.reset();
                populateXML();
                ChangeColor();
                return false;
            }
       </script>    
</head>
    <body oncontextmenu="return false;" topmargin="0" leftmargin="0" rightmargin="0"
    onload="populateXML();top.topframe.main1.mousein = false;">
    <form id="PolicyUWQMarine" method="post" runat="server">
    <table cellspacing="1" cellpadding="1" border="0" width="100%">                           
                        <tr>
                            <td class="pageHeader" colspan="4">
                             <webcontrol:Tab ID="TabCtl" runat="server" TabTitles="Underwriting Questions" TabLength="250"></webcontrol:Tab>
                            </td>
                        </tr>
                        <tr>
                            <td class="pageHeader" colspan="4">
                                Please note that all fields marked with * are mandatory
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolorc" align="right" colspan="4">
                                <asp:Label ID="lblMessage" runat="server" CssClass="errmsg"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class='midcolora' width='50%'>
                                <asp:Label ID="capANY_PRE_INSPECTION_DONE" runat="server">Any pre-inspection done?</asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class='midcolora' width='50%'>
                                  <asp:DropDownList ID="cmbANY_PRE_INSPECTION_DONE" runat="server">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvANY_PRE_INSPECTION_DONE" ControlToValidate="cmbANY_PRE_INSPECTION_DONE"
                                    runat="server" Display="Dynamic" ErrorMessage = "Please Select Yes Or No"></asp:RequiredFieldValidator>
                            </td>
                             <td class='midcolora' width='50%'>
                            </td>
                            <td class='midcolora' width='50%'>
                            </td>
                         </tr>
                         <tr>
                            <td class='midcolora' width='18%'>
                                <asp:Label ID="capTYPEOFPACKAGING" runat="server">Type of Packaging</asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class='midcolora' width='32%'>
                                <asp:TextBox ID="txtTYPEOFPACKAGING" runat="server" TextMode="MultiLine" Rows="3"
                                Width="300px" Height="60px" MaxLength= "200" onkeypress="MaxLength(this,200)" onpaste="MaxLength(this,200)"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvTYPEOFPACKAGING" ControlToValidate="txtTYPEOFPACKAGING" runat="server"
                                    Display="Dynamic" ErrorMessage = "Please Insert Type Of Packaging" ></asp:RequiredFieldValidator>
                            </td>
                             <td class='midcolora' width='18%'>
                            </td>
                            <td class='midcolora' width='32%'>
                            </td>
                        </tr>
                        <tr>
                            <td class='midcolora' colspan='2'>
                                <cmsb:CmsButton class="clsButton" ID='btnReset' runat="server" Text='Reset'></cmsb:CmsButton>
                            </td>
                            <td class='midcolorr' colspan="2">
                                <cmsb:CmsButton class="clsButton" ID='btnSave' runat="server" Text='Save' ></cmsb:CmsButton>
                            </td>
                        </tr>
             </table>
                <INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server" />				
                <input id="hidDETAIL_TYPE_ID" type="hidden" value="New" name="hidDETAIL_TYPE_ID" runat="server" /> 
				<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server" />
				<INPUT id="hidAPP_ID" type="hidden" value="0" name="hidAPP_ID" runat="server" />                
				<INPUT id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server" />
                 <input id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server"/>
	            <input id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server"/>				
        </form>
</body>
</html>

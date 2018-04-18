<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BillingInformation.aspx.cs" Inherits="Cms.Policies.Aspx.BillingInformation" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POL BILLING INFO</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta http-equiv="Cache-Control" content="no-cache">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"><LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script src="/cms/cmsweb/scripts/common.js"></script>
	<script src="/cms/cmsweb/scripts/form.js"></script>
	<script src="/cms/cmsweb/scripts/calendar.js"></script>
	<script src="/cms/cmsweb/scripts/AJAXcommon.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
    <script language="javascript" type="text/javascript">

     function ResetTheForm()
            {
                document.getElementById('cmbBILLING_TYPE').value = '';
                document.getElementById('cmbDOWN_PAYMENT_MODE').value = '';
                document.getElementById('txtUNDERWRITER').value = '';
                document.getElementById('cmbROLLOVER').value = '';
                document.getElementById('chkCOMP_APP_BONUS_APPLIES').value = '';
                document.getElementById('txtCURRENT_RESIDENCE').value = '';
                document.getElementById('txtRECIVED_PREMIUM').value = '';
                document.getElementById('cmbPROXY_SIGN_OBTAIN').value = '';
                document.getElementById('cmbBILLING_PLAN').value = '';
		       
		    }
            </script>
</head>
<body>
    <form id="POL_BILLING_INFO" runat="server">
         <table  width='100%' border='0' align='center' border="1">
           <tr id="tr007" runat="server">
                <td class="headereffectCenter" colspan=4>
                    <asp:label ID="capHEADER" Text="Billing info" runat="server" ></asp:Label><br />
                </td>
            </tr>
            <tr id="trMessages" runat="server">
                <td id="tdMessages" runat="server" class="pageHeader" colspan=4>
                    <asp:label ID="capMessages" runat="server" Text= "Please note that all fields marked with * are mandatory"></asp:Label><br />
                </td>
            </tr>
            <tr id="trErrorMsgs" runat="server">
                <td id="tdErrorMsgs" runat="server" class="midcolorc" align="right" colSpan="4">
                    <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label><br />
                </td>
            </tr>
            <tr id="trBody" runat="server">
                <td class="midcolorc" align="right" colSpan="4">
                    <asp:label id="lblDelete" Text="" runat="server" CssClass="errmsg" Visible="False"></asp:Label><br />
                </td>
            </tr>
            <tr id="tr001" runat="server">
                <td id="tdBILLING_TYPE" runat="server" class='midcolora' width='18%'>
                    <asp:Label ID="capBILLING_TYPE" runat="server"></asp:Label><span id="spnBILLING_TYPE" runat="server">*</span> 
               <br />
                    <asp:DropDownList ID="cmbBILLING_TYPE" runat="server"></asp:DropDownList><br />
                    <asp:RequiredFieldValidator ID="rfvBILLING_TYPE" runat="server" ControlToValidate="cmbBILLING_TYPE" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td class='midcolora' width='18%'>
                    <asp:Label ID="capBILLING_PLAN" runat="server"></asp:Label><span id="spnBILLING_PLAN" runat="server">*</span> 
                <br />
                     <asp:DropDownList ID="cmbBILLING_PLAN" runat="server"></asp:DropDownList><br />
                     <asp:RequiredFieldValidator ID="rfvBILLING_PLAN" runat="server" ControlToValidate="cmbBILLING_PLAN" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td id="tdDOWN_PAYMENT_MODE" runat="server" class='midcolora' width='18%'>
                    <asp:Label ID="capDOWN_PAYMENT_MODE" runat="server"></asp:Label><span id="spnDOWN_PAYMENT_MODE" runat="server">*</span> 
                <br />
                    <asp:DropDownList ID="cmbDOWN_PAYMENT_MODE" runat="server"></asp:DropDownList><br />
                    <asp:RequiredFieldValidator ID="rfvDOWN_PAYMENT_MODE" runat="server"  ControlToValidate="cmbDOWN_PAYMENT_MODE" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>

             <tr id="tr002" runat="server">
                
                <td class='midcolora' width='18%'>
                    <asp:Label ID="capPROXY_SIGN_OBTAIN" runat="server"></asp:Label><span id="spnPROXY_SIGN_OBTAIN" runat="server">*</span> 
                <br />
                     <asp:DropDownList ID="cmbPROXY_SIGN_OBTAIN" runat="server"></asp:DropDownList><br />
                     <asp:RequiredFieldValidator ID="rfvPROXY_SIGN_OBTAIN" runat="server"  ControlToValidate="cmbPROXY_SIGN_OBTAIN" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td style="display:none" id="tdUNDERWRITER" runat="server" class='midcolora' width='18%'>
                    <asp:Label ID="capUNDERWRITER" runat="server"></asp:Label>
                <br />
                    <asp:TextBox ID="txtUNDERWRITER" runat="server"></asp:TextBox>
                </td>
                 <td class='midcolora' width='18%'>
                    <asp:Label ID="capROLLOVER" runat="server"></asp:Label>
               <br />
                 <asp:DropDownList ID="cmbROLLOVER" runat="server"></asp:DropDownList>
                </td>
                 <td class='midcolora' width='18%'>
                    <asp:Label ID="capRECIVED_PREMIUM" runat="server"></asp:Label>
               <br />
                    <asp:TextBox ID="txtRECIVED_PREMIUM" runat="server"></asp:TextBox><br />
                    <asp:RegularExpressionValidator id="revRECIVED_PREMIUM" runat="server" ControlToValidate="txtRECIVED_PREMIUM" Display="Dynamic" ></asp:RegularExpressionValidator>
                </td>
            </tr>
           <%--  <tr id="tr004" runat="server">
               
               
                <td  class='midcolora' width='18%'>
                </td>
                 <td  class='midcolora' width='18%'>
                </td>
            </tr>--%>
             <tr id="tr005" runat="server">
                <td id="td1" runat="server" class='midcolora' width='18%'>
                    <asp:Label ID="capCOMP_APP_BONUS_APPLIES" runat="server"></asp:Label> 
                <br />
                    <asp:CheckBox ID="chkCOMP_APP_BONUS_APPLIES" runat="server"></asp:CheckBox>
                </td>
                <td  class='midcolora' width='18%'><asp:Label ID="capCURRENT_RESIDENCE" runat="server"></asp:Label> 
                <br />
                    <asp:TextBox ID="txtCURRENT_RESIDENCE" runat="server"></asp:TextBox></td>
                 <td  class='midcolora' width='18%'></td>
            </tr>
           <%--  <tr id="tr006" runat="server">
                <td id="td2" runat="server" class='midcolora' width='18%'>
                    
                </td>
                <td class='midcolora' width='18%'></td>
                
            </tr>--%>
               <tr>
						<td class='midcolora' width='24%'>
							<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
							<%--<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server"
							Text="Deactivate" CausesValidation="False" onclick="btnActivateDeactivate_Click"></cmsb:cmsbutton>--%>
						</td>
						<td class='midcolorr' colspan="3">
							<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save' 
                                onclick="btnSave_Click"></cmsb:cmsbutton>
                                <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" 
                                Text="Delete" CausesValidation="False"
							causevalidation="false" onclick="btnDelete_Click"></cmsb:cmsbutton>
						</td>
					</tr>
         </table>
         <input id="hidCUSTOMER_ID" type="hidden" runat="server" />
         <input id="hidPOLICY_ID" type="hidden" runat="server" />
         <input id="hidPOLICY_VERSION_ID" type="hidden" runat="server" />
         <input id="hidBILLING_ID" type="hidden" runat="server" />
         <input id="hidLOB_ID" type="hidden" runat="server" />
         <input id="hidFormSaved" type="hidden" runat="server" />
    </form>

     
</body>
</html>

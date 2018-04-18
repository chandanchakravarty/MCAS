<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMasterSetup.aspx.cs" Inherits="Cms.CmsWeb.Maintenance.AddMasterSetup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<HTML>
	<HEAD runat="server">
		<title>MASTER DETAIL</title>
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

		    function ResetTheForm() {
		        document.getElementById('txtTYPE_CODE').value = '';
		        document.getElementById('txtCONTACT_PERSON').value = '';
		        document.getElementById('txtTYPE_NAME').value = '';
		        document.getElementById('txtADDRESS').value = '';
		        document.getElementById('txtCITY').value = '';
		        document.getElementById('txtPOST_CODE').value = '';
		        document.getElementById('txtPROVINCE').value = '';
		        document.getElementById('txtTEL_NO_OFF').value = '';
		        document.getElementById('txtTEL_NO_RES').value = '';
		        document.getElementById('txtMOBILE_NO').value = '';
		        document.getElementById('txtFAX_NO').value = '';
		        document.getElementById('txtE_MAIL').value = '';
		        document.getElementById('txtPRIVATE_E_MAIL').value = '';
		        document.getElementById('txtGST').value = '';
		        document.getElementById('txtGST_REG_NO').value = '';
		        document.getElementById('txtWITHHOLDING_TAX').value = '';
		        document.getElementById('txtCLASSIFICATION').value = '';
		        document.getElementById('txtMEMO').value = '';
		        document.getElementById('txtADDRESS1').value = ''; 
		     }
        </script>
</HEAD>
<body  oncontextmenu ="return false;"  leftMargin='0' topMargin='0' onload='ApplyColor();'>
    <form  id='MNT_MASTER_DETAIL' method='post' runat='server'>
     <TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
		<tr>
			<td>
                <TABLE width='100%' border='0' align='center'>
                             
                    <tr id="trMessages" runat="server">
                      <td id="tdMessages" runat="server" class="pageHeader">
                        <asp:label ID="capMessages" runat="server" Text= "Please note that all fields marked with * are mandatory"></asp:Label><br />
                      </td>
                    </tr>
                    <tr id="trErrorMsgs" runat="server">
                        <td id="tdErrorMsgs" runat="server" class="midcolorc" align="right" colSpan="4">
                            <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label><br />
                        </td>
                    </tr>
                                  
                    <tr id="trTYPE_CODE" runat="server">
                     <td id="tdTYPE_CODE" width="33%" runat="server"  class='midcolora'>
                      <asp:Label ID="capTYPE_CODE" runat="server"></asp:Label>
                      <span id="spnTYPE_CODE" runat="server"></span> <br />
                      <asp:TextBox ID="txtTYPE_CODE" runat="server"></asp:TextBox><br />
                      <asp:RequiredFieldValidator ID="rfvTYPE_CODE" runat ="server" ControlToValidate="txtTYPE_CODE" Display="Dynamic" ErrorMessage="Please Enter Solicitor Code."></asp:RequiredFieldValidator>
                     </td>
                     <td id="tdSOLICITOR_TYPE" width="33%" class='midcolora'>
                      <asp:Label ID="capSOLICITOR_TYPE" runat="server"></asp:Label><br />
                         <asp:DropDownList ID="cmbSOLICITOR_TYPE" runat="server"></asp:DropDownList>
                     </td>
                    </tr>
                    <tr id="trTYPE_NAME" runat="server">
                     <td id="tdTYPE_NAME" width="33%" runat="server"  class='midcolora'>
                          <asp:Label ID="capTYPE_NAME" runat="server"></asp:Label>
                          <span id="spnTYPE_NAME" runat="server" class="mandatory">*</span><br />
                          <asp:TextBox ID="txtTYPE_NAME" runat="server"></asp:TextBox><br />
                          <asp:requiredfieldvalidator id="rfvTYPE_NAME" runat="server" ControlToValidate="txtTYPE_NAME" Display="Dynamic"></asp:requiredfieldvalidator>
                      </td>
                       <td id="tdCONTACT_PERSON" width="33%" runat="server"  class='midcolora'>
                            <asp:Label ID="capCONTACT_PERSON" runat="server"></asp:Label><br />
                            <asp:TextBox ID="txtCONTACT_PERSON" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trADDRESS" runat="server">
                     <td id="tdADDRESS" runat="server" width="33%" class='midcolora'>
                          <asp:Label ID="capADDRESS" runat="server"></asp:Label>
                          <span id="spnADDRESS" runat="server" class="mandatory">*</span><br />
                          <asp:TextBox ID="txtADDRESS" runat="server"></asp:TextBox><br />
                          <asp:requiredfieldvalidator id="rfvADDRESS" runat="server" ControlToValidate="txtADDRESS" Display="Dynamic"></asp:requiredfieldvalidator>
                      </td>
                      <td id="tdSTATUS" runat="server" width="33%" class='midcolora'>
                            <asp:Label ID="capSTATUS" runat="server"></asp:Label><br />
                            <asp:DropDownList ID="cmbSTATUS"  onfocus="SelectComboIndex('cmbSTATUS')" runat="server"></asp:DropDownList>
                      </td>
                    </tr>
                     <tr>
                     <td id="td1" runat="server" width="33%" class='midcolora'>
                           <asp:TextBox ID="txtADDRESS1" runat="server"></asp:TextBox><br />
                          <asp:requiredfieldvalidator id="rfvADDRESS1" runat="server" ControlToValidate="txtADDRESS1" Display="Dynamic"></asp:requiredfieldvalidator>
                      </td>
                    <td id="tdSURVEYOR_SOURCE" runat="server" width="33%" class='midcolora'>
                            <asp:Label ID="capSURVEYOR_SOURCE" runat="server"></asp:Label><br />
                            <asp:DropDownList ID="cmbSURVEYOR_SOURCE" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                    <td id="tdCITY" runat="server" width="33%"  class='midcolora'>
                              <asp:Label ID="capCITY" runat="server"></asp:Label>
                              <span id="spnCITY" runat="server" class="mandatory">*</span><br />
                              <asp:TextBox ID="txtCITY" runat="server"></asp:TextBox><br />
                              <asp:requiredfieldvalidator id="rfvCITY" runat="server" ControlToValidate="txtCITY" Display="Dynamic"></asp:requiredfieldvalidator>
                         </td>
                    <td id="tdPROVINCE" runat="server" width="33%"  class='midcolora'>
                            <asp:Label ID="capPROVINCE" runat="server"></asp:Label><br />
                            <asp:TextBox id="txtPROVINCE" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trCOUNTRY" runat="server">
                         <td id="tdCOUNTRY" runat="server" width="33%" class='midcolora'>
                              <asp:Label ID="capCOUNTRY" runat="server"></asp:Label><br />
                              <asp:DropDownList ID="cmbCOUNTRY"   runat="server" ></asp:DropDownList>
                         </td>
                         <td id="tdPOST_CODE" runat="server" width="33%"  class='midcolora'>
                            <asp:Label ID="capPOST_CODE" runat="server"></asp:Label><br />
                            <asp:TextBox ID="txtPOST_CODE" runat="server"></asp:TextBox><br />
                              <asp:regularexpressionvalidator id="revPOST_CODE" runat="server" Display="Dynamic" ControlToValidate="txtPOST_CODE"></asp:regularexpressionvalidator>
                        </td>
                    </tr>
                    <tr id="trTEL_NO_OFF" runat="server">
                        <td id="tdTEL_NO_OFF" runat="server" width="33%" class='midcolora'>
                            <asp:Label ID="capTEL_NO_OFF" runat="server"></asp:Label>
                             <span id="spnTEL_NO_OFF" runat="server" class="mandatory">*</span><br />
                            <asp:TextBox ID="txtTEL_NO_OFF" runat="server"></asp:TextBox><br />
                        </td>
                        <td id="tdTEL_NO_RES" runat="server" width="33%"  class='midcolora'>
                            <asp:Label ID="capTEL_NO_RES" runat="server"></asp:Label><br />
                            <asp:TextBox ID="txtTEL_NO_RES" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trMOBILE_NO" runat="server">
                         <td id="tdMOBILE_NO" runat="server" width="33%" class='midcolora'>
                              <asp:Label ID="capMOBILE_NO" runat="server"></asp:Label><br />
                              <asp:TextBox ID="txtMOBILE_NO" runat="server"></asp:TextBox><br />
                              <asp:regularexpressionvalidator id="revMOBILE_NO" runat="server" Display="Dynamic" ControlToValidate="txtMOBILE_NO"></asp:regularexpressionvalidator>
                         </td>
                         <td id="tdFAX_NO" runat="server" width="33%" class='midcolora'>
                            <asp:Label ID="capFAX_NO" runat="server"></asp:Label><br />
                            <asp:TextBox ID="txtFAX_NO" runat="server"></asp:TextBox><br />
                            <asp:regularexpressionvalidator id="revFAX_NO" runat="server" Display="Dynamic" ControlToValidate="txtFAX_NO"></asp:regularexpressionvalidator>
                        </td>
                    </tr>
                    <tr id="trE_MAIL" runat="server">
                        <td id="tdE_MAIL" runat="server" width="33%" class='midcolora'>
                            <asp:Label ID="capE_MAIL" runat="server"></asp:Label><br />
                            <asp:TextBox ID="txtE_MAIL" runat="server"></asp:TextBox><br />
                            <asp:regularexpressionvalidator id="revE_MAIL" runat="server" Display="Dynamic" ControlToValidate="txtE_MAIL"></asp:regularexpressionvalidator>
                        </td>
                         <td id="tdPRIVATE_E_MAIL" runat="server" width="33%"  class='midcolora'>
                            <asp:Label ID="capPRIVATE_E_MAIL" runat="server"></asp:Label><br />
                            <asp:TextBox ID="txtPRIVATE_E_MAIL" runat="server"></asp:TextBox><br />
                            <asp:regularexpressionvalidator id="revPRIVATE_E_MAIL" runat="server" Display="Dynamic" ControlToValidate="txtPRIVATE_E_MAIL"></asp:regularexpressionvalidator>
                        </td>
                    </tr>
                    <tr  runat="server">
                       <td runat="server" width="33%"  class='midcolora'>
                         <table cellpadding="0" cellspacing="0">
                           <tr id="trGST" runat="server">
                            <td id="tdGST" runat="server" class='midcolora'>
                              <asp:Label ID="capGST" runat="server"></asp:Label><br />
                              <asp:DropDownList ID="cmbGST" runat="server"></asp:DropDownList>
                            </td>
                          </tr>
                          <tr>
                            <td class='midcolora'>
                            <asp:TextBox ID="txtGST" runat="server"></asp:TextBox>
                           </td>
                          </tr>
                         </table>
                         </td>
                         <td id="tdGST_REG_NO" runat="server" width="33%" class='midcolora'>
                            <asp:Label ID="capGST_REG_NO" runat="server"></asp:Label><br />
                            <asp:TextBox ID="txtGST_REG_NO" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr  runat="server">
                        <td width="33%"  class='midcolora'>
                         <table cellpadding="0" cellspacing="0">
                           <tr id="trWITHHOLDING_TAX" runat="server">
                        <td id="tdWITHHOLDING_TAX" runat="server" width="33%"  class='midcolora'>
                            <asp:Label ID="capWITHHOLDING_TAX" runat="server"></asp:Label><br />
                            <asp:DropDownList ID="cmbWITHHOLDING_TAX" runat="server"></asp:DropDownList>
                            </td></tr>
                            <tr>
                            <td>
                            <asp:TextBox ID="txtWITHHOLDING_TAX" runat="server"></asp:TextBox>
                            </td>
                            </tr>
                            </table>
                        </td>
                        <td  class='midcolora'>
                        
                        </td>
                  </tr>
                    <tr id="trCLASSIFICATION" runat="server">
                         <td id="tdCLASSIFICATION" runat="server" width="33%" class='midcolora'>
                              <asp:Label ID="capCLASSIFICATION" runat="server"></asp:Label><br />
                              <asp:TextBox ID="txtCLASSIFICATION" runat="server"></asp:TextBox><br />
                         </td>
                         <td  class='midcolora'></td>
                    </tr>
                    <tr id="trMEMO" runat="server">
                         <td id="tdMEMO" runat="server" width="33%"  class='midcolora'>
                              <asp:Label ID="capMEMO" runat="server"></asp:Label><br />
                              <asp:TextBox ID="txtMEMO" runat="server"></asp:TextBox><br />
                         </td>
                         <td  class='midcolora'></td>
                    </tr>

                  <tr>
						<td class='midcolora' width='24%'>
							<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
							<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server"
							Text="Deactivate" CausesValidation="False" onclick="btnActivateDeactivate_Click"   ></cmsb:cmsbutton>
						</td>
						<td class='midcolorr' colspan="3">
							<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save' onclick="btnSave_Click"></cmsb:cmsbutton>
						</td>
					</tr>
                  </TABLE>
                  

     <input id="hidTYPE_ID" type="hidden" value="0" name="hidTYPE_ID" runat="server" />
     <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server" />
     <INPUT id="hidTYPE_UNIQUE_ID" type="hidden" value="NEW" name="hidTYPE_UNIQUE_ID" runat="server">
     <INPUT id="hidCountry" type="hidden"  name="hidCountry" runat="server">
     <INPUT id="hidOldData" type="hidden"  name="hidOldData" runat="server">
    </form>
          <script type="text/javascript">

              try {
                  if (document.getElementById('hidFormSaved').value == "1") {

                      RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidTYPE_UNIQUE_ID').value);
                  }
              }
              catch (err) {
              }      
		    </script>
</body>
</HTML>

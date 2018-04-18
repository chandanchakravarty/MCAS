<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddWarehouseDetail.aspx.cs" Inherits="Cms.Policies.Aspx.BOP.AddWarehouseDetail" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POL SUP FORM WAREHOUSE</title>
     <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta http-equiv="Cache-Control" content="no-cache">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script src="/cms/cmsweb/scripts/common.js"></script>
	<script src="/cms/cmsweb/scripts/form.js"></script>
	<script src="/cms/cmsweb/scripts/calendar.js"></script>
	<script src="/cms/cmsweb/scripts/AJAXcommon.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
     <script language="javascript" type="text/javascript">
         function ResetTheForm() {
             document.getElementById('cmbBUILDINGS').value = '';
             document.getElementById('cmbSTORAGEUNITS').value = '';
             document.getElementById('cmbIS_FENCED').value = '';
             document.getElementById('cmbRES_MGMR').value = '';
             document.getElementById('cmbIS_PRKNG_AVL').value = '';
             document.getElementById('cmbDAYTIME_ATTNDT').value = '';
             document.getElementById('cmbIS_BOAT_PRKNG_AVL').value = '';
             document.getElementById('cmbANY_BUSS_ACTY').value = '';
             document.getElementById('cmbGUARD_DOG').value = '';
             document.getElementById('cmbVLT_STYLE').value = '';
             document.getElementById('cmbANY_FIREARMS').value = '';
             document.getElementById('cmbTRUCK_RENTAL').value = '';
             document.getElementById('cmbBUILDINGS').value = '';
             document.getElementById('cmbTENANT_LCKS_CHK').value = '';
             document.getElementById('cmbMGMR_KYS_CUST_UNIT').value = '';
             document.getElementById('cmbANY_BUSN_GUIDELINES').value = '';
             document.getElementById('cmbNOTICE_SENT').value = '';
             document.getElementById('cmbNO_DYS_TENANT_PROP_SOLD').value = '';
             document.getElementById('txtSALES_TENANT_LST_TWELVE_MNTHS').value = '';
             document.getElementById('cmbDISP_PUBL').value = '';
             document.getElementById('cmbANY_COLD_STORAGE').value = '';
             document.getElementById('cmbANY_CLIMATE_CNTL').value = '';
             document.getElementById('cmbMGMR_TYPE').value = '';
             document.getElementById('cmbOWN_MGMR').value = '';
         }
    </script>
</head>
<body>
    <form id="POL_SUP_FORM_WAREHOUSE" runat="server">
     <table  width='100%' border='0' align='center' border=1>
      <tr id="tr007" runat="server">
                <td class="headereffectCenter" colspan=4>
                    <asp:label ID="capHEADER" Text="Mini Warehouse" runat="server" ></asp:label><br />
                </td>
            </tr>
            <tr id="trMessages" runat="server">
                <td id="tdMessages" runat="server" class="pageHeader" colspan=4>
                    <asp:label ID="capMessages" runat="server" Text= "Please note that all fields marked with * are mandatory"></asp:label><br />
                </td>
            </tr>
            <tr id="trErrorMsgs" runat="server">
                <td id="tdErrorMsgs" runat="server" class="midcolorc" align="right" colspan=4>
                    <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="true"></asp:label><br />
                </td>
            </tr>
            <tr id="trBody" runat="server">
                <td class="midcolorc" align="right" colspan=4>
                    <asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="true"></asp:label><br />
                </td>
            </tr>

             <tr id="tr001" runat="server">
              <td class='midcolora' width='18%'>
                  <asp:Label ID="capBUILDINGS" runat="server"></asp:Label><br />
                  <asp:DropDownList ID="cmbBUILDINGS" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
                  <asp:Label ID="capSTORAGEUNITS" runat="server"></asp:Label><br />
                  <asp:DropDownList ID="cmbSTORAGEUNITS" runat="server"></asp:DropDownList>
              </td>
              <td class='midcolora' width='18%'>
                  <asp:Label ID="capOWN_MGMR" runat="server"></asp:Label><br />
                  <asp:DropDownList ID="cmbOWN_MGMR" runat="server"></asp:DropDownList>
              </td>
            </tr>

              <tr id="tr002" runat="server">
              
               <td class='midcolora' width='18%'>
                  <asp:Label ID="capIS_FENCED" runat="server"></asp:Label><br />
                  <asp:DropDownList ID="cmbIS_FENCED" runat="server"></asp:DropDownList>
              </td>
               <td class='midcolora' width='18%'>
                <asp:Label ID="capRES_MGMR" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbRES_MGMR" runat="server"></asp:DropDownList>
            </td>
               <td class='midcolora' width='18%'>
                <asp:Label ID="capIS_PRKNG_AVL" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbIS_PRKNG_AVL" runat="server"></asp:DropDownList>
            </td>
            </tr>
           

         <tr id="tr004" runat="server">
            <td class='midcolora' width='18%'>
                <asp:Label ID="capDAYTIME_ATTNDT" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbDAYTIME_ATTNDT" runat="server"></asp:DropDownList>
            </td>
            <td class='midcolora' width='18%'>
                <asp:Label ID="capIS_BOAT_PRKNG_AVL" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbIS_BOAT_PRKNG_AVL" runat="server"></asp:DropDownList>
            </td>
            <td class='midcolora' width='18%'>
                <asp:Label ID="capANY_BUSS_ACTY" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbANY_BUSS_ACTY" runat="server"></asp:DropDownList>
            </td>
        </tr>

         <tr id="tr005" runat="server">
            <td class='midcolora' width='18%'>
                <asp:Label ID="capGUARD_DOG" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbGUARD_DOG" runat="server"></asp:DropDownList>
            </td>
            <td class='midcolora' width='18%'>
                <asp:Label ID="capVLT_STYLE" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbVLT_STYLE" runat="server"></asp:DropDownList>
            </td>
            <td class='midcolora' width='18%'>
                <asp:Label ID="capANY_FIREARMS" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbANY_FIREARMS" runat="server"></asp:DropDownList>
            </td>
        </tr>

        

        
         <tr id="tr008" runat="server">
            <td class='midcolora' width='18%'>
                <asp:Label ID="capTRUCK_RENTAL" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbTRUCK_RENTAL" runat="server"></asp:DropDownList>
            </td>
            <td class='midcolora' width='18%'>
                <asp:Label ID="capTENANT_LCKS_CHK" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbTENANT_LCKS_CHK" runat="server"></asp:DropDownList>
            </td>
            <td class='midcolora' width='18%'>
                <asp:Label ID="capMGMR_KYS_CUST_UNIT" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbMGMR_KYS_CUST_UNIT" runat="server"></asp:DropDownList>
            </td>
        </tr>

        
         <tr id="tr009" runat="server">
            
            <td class='midcolora' width='18%'>
                <asp:Label ID="capANY_BUSN_GUIDELINES" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbANY_BUSN_GUIDELINES" runat="server"></asp:DropDownList>
            </td>
             <td class='midcolora' width='18%'>
                <asp:Label ID="capNOTICE_SENT" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbNOTICE_SENT" runat="server"></asp:DropDownList>
            </td>
            <td class='midcolora' width='18%'>
                <asp:Label ID="capNO_DYS_TENANT_PROP_SOLD" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbNO_DYS_TENANT_PROP_SOLD" runat="server"></asp:DropDownList>
            </td>
        </tr>

       
         <tr id="tr0011" runat="server">
            <td class='midcolora' width='18%'>
                <asp:Label ID="capSALES_TENANT_LST_TWELVE_MNTHS" runat="server"></asp:Label><br />
                <asp:TextBox ID="txtSALES_TENANT_LST_TWELVE_MNTHS" runat="server"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="revSALES_TENANT_LST_TWELVE_MNTHS" runat="server" ControlToValidate="txtSALES_TENANT_LST_TWELVE_MNTHS"></asp:RegularExpressionValidator>
            </td>
            <td class='midcolora' width='18%'>
                <asp:Label ID="capDISP_PUBL" runat="server"></asp:Label><br />
                 <asp:DropDownList ID="cmbDISP_PUBL" runat="server"></asp:DropDownList>
            </td>
          
            <td class='midcolora' width='18%'>
                <asp:Label ID="capANY_COLD_STORAGE" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbANY_COLD_STORAGE" runat="server"></asp:DropDownList>
            </td>
          
        
        </tr>

         

         <tr id="tr0013" runat="server">
           <td class='midcolora' width='18%'>
                <asp:Label ID="capANY_CLIMATE_CNTL" runat="server"></asp:Label><br />
                <asp:DropDownList ID="cmbANY_CLIMATE_CNTL" runat="server"></asp:DropDownList>
            </td>
            <td class='midcolora' width='18%'>
                <asp:Label ID="capMGMR_TYPE" runat="server"></asp:Label><br />
                 <asp:DropDownList ID="cmbMGMR_TYPE" runat="server"></asp:DropDownList>
            </td>
            <td class='midcolora' width='18%'>
                
               
            </td>
        
        </tr>
          <tr>
						<td class='midcolora' width='24%'>
							<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
						
						</td>
						<td class='midcolorr' colspan="3">
							<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save' onclick="btnSave_Click" 
                                ></cmsb:cmsbutton>
                                <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" 
                                Text="Delete" CausesValidation="False"	causevalidation="false" onclick="btnDelete_Click" 
                                ></cmsb:cmsbutton>
						</td>
					</tr>                
     </table>

        <input id="hidCUSTOMER_ID" type="hidden" runat="server" />
         <input id="hidPOLICY_ID" type="hidden" runat="server" />
         <input id="hidPOLICY_VERSION_ID" type="hidden" runat="server" />
         <input id="hidPREMISES_ID" type="hidden" runat="server" />
         <input id="hidWAREHOUSE_ID" type="hidden" runat="server" />
         <input id="hidFormSaved" type="hidden" runat="server" />
         <input id="hidLOCATION_ID" type="hidden" runat="server" />
    </form>
</body>
</html>

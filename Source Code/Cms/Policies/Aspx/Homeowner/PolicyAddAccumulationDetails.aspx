<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyAddAccumulationDetails.aspx.cs" Inherits="Cms.Policies.Aspx.Homeowner.PolicyAddAccumulationDetails" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  
    <title>PolicyAddAccumulationDetails</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/DwellingInfo.js"></script>
        <script language="javascript">
			
		function PostFromLookup() {
		    
			/*Post back the form to show the details of holder*/
			document.getElementById("hidLOOKUP").value = "Y";
			__doPostBack('hidLOOKUP','')
		}
        </script>
		
</head>
<body>
    <form id="Pol_Accumulation_Details" runat="server">
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
                           <asp:Label ID="capMessages" runat="server">Please note that all fields marked with * are 
									mandatory</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="midcolorc" align="right" colspan="4">
                            <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td class='midcolora' width='18%'>
                            <asp:Label ID="capAcc_ref" runat="server">Accumulation Reference</asp:Label><span id="spnAcc_ref" class="mandatory">*</span>
                          
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtAcc_ref' runat='server' size='30' MaxLength='6'></asp:TextBox><a class="calcolora" href="#"><img id="imgBusinessType"
                        style="cursor: hand" alt="" src="/cms/cmsweb/images/selecticon.gif" border="0"
                        runat="server"></a><br>
                            <asp:RequiredFieldValidator ID="rfvAcc_ref" runat="server" ControlToValidate="txtAcc_ref"
                                ErrorMessage="Please Select Accumulation Reference." Display="Dynamic"></asp:RequiredFieldValidator>
               
                        </td>
                        
                          <td class='midcolora' width='18%'>
                            <asp:Label ID="capTot_sum_insured" runat="server">Total Sum Insured</asp:Label><span id="spnDIV_STATE" runat="server" class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                          <asp:TextBox ID='txtTot_sum_insured' runat='server' size='30' MaxLength='40'></asp:TextBox><br>
                        </td>
                     
                    </tr>
                    <tr>
                      <td class='midcolora' width='18%'>
                            <asp:Label ID="capAcc_code" runat="server">Accumulation Code</asp:Label><span class="mandatory">*</span>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtAcc_code' runat='server' size='15' MaxLength='70' ></asp:TextBox><br>
                            <asp:RequiredFieldValidator ID="rfvAcc_code" runat="server" ControlToValidate="txtAcc_code"
                                ErrorMessage="Please enetr Accumulation Code.." Display="Dynamic"></asp:RequiredFieldValidator>
                         </td>
                           <td class='midcolora' width='18%'>
                            <asp:Label ID="capFacultative_RI" runat="server">Less: Facultative RI</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtFacultative_RI' runat='server' size='30' MaxLength='40'></asp:TextBox><br>
                        </td>
                      
                      
                    </tr>
                    <tr>
                         <td class='midcolora' width='18%'>
                            <asp:Label ID="capTot_policies" runat="server">Total No. of Policies</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtTot_policies' runat='server' size='30' MaxLength='70'></asp:TextBox>
                            
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capGross_ret_SI" runat="server">Gross Retained Sum Insured</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                          <asp:TextBox ID='txtGross_ret_SI' runat='server' size='30' MaxLength="15" ></asp:TextBox>
                           
                        </td>
                      
                    </tr>
                    <tr>
                      <td class='midcolora' width='18%'>
                            <asp:Label ID="capOwn_ret_limit" runat="server">Own Retention Limit</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtOwn_ret_limit' runat='server' size='30' MaxLength='70'></asp:TextBox>
                        </td>
                        <td class='midcolora' width='18%'>
                            <asp:Label ID="capOwn_ret" runat="server">Own Retention</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtOwn_ret' runat='server' size='30' MaxLength='6' ></asp:TextBox>
                           
                        </td>
                     
                    </tr>
                    <tr>
                     <td class='midcolora' width='18%'>
                            <asp:Label ID="capTreaty_cap_limit" runat="server">Treaty Capacity Limit</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                            <asp:TextBox ID='txtTreaty_cap_limit' runat='server' size='30' MaxLength='40'></asp:TextBox>
                            
                        </td>
                          <td class='midcolora' width='18%'>
                            
                            <asp:Label ID="capIst_Surplus" runat="server">Ist Surplus</asp:Label>

                        </td>
                        <td class='midcolora' width='32%'>
                       
                            <asp:TextBox ID='txtIst_Surplus' runat='server' size='30' MaxLength='50'></asp:TextBox><br>
                        </td>
                       
                    </tr>
                    <tr>
                      <td class='midcolora' width='18%'>
                            <asp:Label ID="capAcc_limit_avl" runat="server">Accumulation Limit Available</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                           <asp:TextBox ID='txtAcc_limit_avl' runat='server' size='30' MaxLength='40'></asp:TextBox>
                           
                        </td>
                     
                       <td class='midcolora' width='18%'>
                         
                           <asp:Label ID="capQuota_share" runat="server">Quota Share</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                          
                                <asp:TextBox ID='txtQuota_share' runat='server' size='30' MaxLength='15' ></asp:TextBox>
                        </td>
                       
                    </tr>
                    <tr>
                      <td class='midcolora' width='18%'>
                           
                        </td>
                        <td class='midcolora' width='32%'>
                           
                            
                        </td>
                         <td class='midcolora' width='18%'>
                           <asp:Label ID="Label1" runat="server">Own Absolute Net Retention</asp:Label>
                        </td>
                        <td class='midcolora' width='32%'>
                          
                            <asp:TextBox ID="txtOwn_abs_net_ret" runat='server' size='30' MaxLength='10'></asp:TextBox> 
                        </td>
                       
                    </tr>
                    
                   
                    <tr>
                        <td class="midcolora" colspan="2">
                            <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" Text="Reset"></cmsb:CmsButton>
                          </td>
                        <td class='midcolorr' colspan="2">
                          <cmsb:CmsButton class="clsButton" ID='btnSave' runat="server" Text='Save' ></cmsb:CmsButton>
                        </td>
                    </tr>
                 <tr>
                                        <td class="headereffectCenter" colspan="4">
                                            <asp:Label ID="lblHeader" runat="server">List of Accumulations</asp:Label>
                                        </td>
                                    </tr>
                    <tr>
                     <asp:GridView AutoGenerateColumns="true" runat="server" ID="grdAccumulated_Policy_details" Width="100%">
             <HeaderStyle CssClass="headereffectWebGrid" HorizontalAlign="Center"></HeaderStyle>
                        <RowStyle CssClass="midcolora"></RowStyle>
                        </asp:GridView>
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
    
    <input id="hidLOOKUP" type="hidden" value="0" name="hidLOOKUP" runat="server">
    <input id="hidHOLDER_NAME" type="hidden" runat="server" NAME="hidHOLDER_NAME">
     <input  type="hidden" runat="server" ID="hidDETAIL_TYPE_ID"  value=""  name="hidDETAIL_TYPE_ID"/>
    </form>
</body>
</html>

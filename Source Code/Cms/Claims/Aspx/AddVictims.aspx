<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddVictims.aspx.cs" Inherits="Cms.Claims.Aspx.AddVictims" %>

<%@ Register assembly="CmsButton" namespace="Cms.CmsWeb.Controls" tagprefix="cmsb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>AddCoveragesInfo</title>
    
       <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
	
	<script type="text/javascript" src="/cms/cmsweb/scripts/Calendar.js"></script>

     <script type="text/javascript" language="javascript">
        
         
         function initPage() {

             ApplyColor();


         }


         
         
         //Custom validator function for premium > 0
         function validateRequestedAmount(objSource, objArgs) {
            
             var Premium = document.getElementById(objSource.controltovalidate).value;
             Premium = FormatAmountForSum(Premium);
             if (parseFloat(Premium) > 0)
                 objArgs.IsValid = true;
             else
                 objArgs.IsValid = false;
         }

         function FormatAmountForSum(num) {
             num = ReplaceAll(num, sGroupSep, '');
             num = ReplaceAll(num, sDecimalSep, '.');
             return num;
         }

         //Custom validator function for premium > 0
         function validateOfferedAmount(objSource, objArgs) {
             var Amount = document.getElementById(objSource.controltovalidate).value;
             Amount = FormatAmountForSum(Amount);
             
             if (Amount != "" && parseFloat(Amount) > 0)
                 objArgs.IsValid = true;
             else 
                 objArgs.IsValid = false;
         }
         function ValidateEstimateAmount(objSource, objArgs) {
           
             var RegExpVal = document.getElementById("revESTIMATE_CLASSIFICATION");

             if (RegExpVal.isvalid == true) {
                 var Amount = document.getElementById(objSource.controltovalidate).value;

                 if (Amount != "" && parseInt(Amount) >= 0 && parseInt(Amount) < 101)
                     objArgs.IsValid = true;
                 else
                     objArgs.IsValid = false;
             }
             else
                 objArgs.IsValid = true;
         }
         
         
         function ResetTheForm() {
             document.AddLitigationInfo.reset();
             return false;
         }
//         function resetForm() {            
//                 document.getElementById("txtJUDICIAL_PROCESS_NO").value = "";
//                 document.getElementById("txtJUDICIAL_PROCESS_DATE").value = "";
//                 document.getElementById("txtPLAINTIFF_NAME").value = "";
//                 document.getElementById("txtPLAINTIFF_CPF").value = "";
//                 document.getElementById("txtPLAINTIFF_REQUESTED_AMOUNT").value = "";
//                 document.getElementById("txtDEFEDANT_OFFERED_AMOUNT").value = "";
//                 
//                 var cmbState=document.getElementById("txtDEFEDANT_OFFERED_AMOUNT")
//                 formElements.elements[i].selectedIndex = jsidvalue;
//             }

         
     </script>
    </head>
<body oncontextmenu="return false;" leftMargin="0" topMargin="0"  onload="initPage();">
    <form id="AddLitigationInfo" runat="server">
    
							<TABLE width="100%" align="center" border="0">
								<TBODY>
									<tr>
										<TD class="pageHeader" colSpan="3">
                    <asp:label id="lblRequiredFieldsInformation" runat="server" 
                                        ></asp:label>
                                        </TD>
									</tr>
									<tr>
										<td class="midcolorc" align="center" colSpan="3">
                    <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
                                  
                                        </td>
									</tr>
								
									<tr>
										<TD width="33%" class="midcolora" valign="top">
                                                <asp:label id="capNAME" 
                        runat="server"></asp:label>
                                                    <span class="mandatory">*</span><br />
                                                <asp:textbox id="txtNAME" 
                        runat="server" size="32" maxlength="256" Width="171px"></asp:textbox>
												    <br />
                                             
                                                <asp:RequiredFieldValidator ID="rfvNAME" runat="server" 
                                                    ControlToValidate="txtNAME"></asp:RequiredFieldValidator>
												    <br />
                                                </TD>
										<TD  width="33%" class="midcolora" valign="top">
                                                <asp:label id="capSTATUS" runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:DropDownList ID="cmbSTATUS" runat="server">
                                                </asp:DropDownList>
                                                <br />
                                             
                                                <asp:RequiredFieldValidator ID="rfvSTATUS" runat="server" 
                                                    ControlToValidate="cmbSTATUS"></asp:RequiredFieldValidator>
                                                <TD class="midcolora" valign="top">
                                                <asp:label id="capINJURY_TYPE" 
                        runat="server"></asp:label>
                                                    <span class="mandatory">*<br />
                                                    </span>
                                                <asp:DropDownList ID="cmbINJURY_TYPE" runat="server">
                                                </asp:DropDownList>
                                             
                                                    <br />
                                             
                                                <asp:RequiredFieldValidator ID="rfvINJURY_TYPE" runat="server" 
                                                    ControlToValidate="cmbINJURY_TYPE"></asp:RequiredFieldValidator>
                                                </TD>
									</tr>
								
									<tr>
										<TD class="midcolora" colspan="3">&nbsp;</TD>
									</tr>
									<tr>
										<TD colspan="3" class="midcolora">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="50%">
                                                <cmsb:cmsbutton class="clsButton" CausesValidation="false" id="btnReset" runat="server" Text="Reset" 
                                                     OnClientClick="javascript:return ResetTheForm();" />
					                        </td>
                            <td class="midcolorr">
                                                <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" 
                                                    onclick="btnDelete_Click"></cmsb:cmsbutton>
                                                <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" 
                                                    onclick="btnSave_Click"></cmsb:cmsbutton>
                                            </td>
                        </tr>
                    </table>
                    </TD>
                    </tr>
                    </TBODY>
                    </TABLE>
                    
                <INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
                <INPUT id="hidVICTIM_ID" type="hidden" value="" name="hidVICTIM_ID" runat="server">
                <INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			    <INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
			    <INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			    <INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			    <INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
			    <INPUT id="hidIS_USER_CREATED" type="hidden" value="" name="hidIS_USER_CREATED" runat="server">
              
                 
							                    
    </form>
   

      <script type="text/javascript">

          try {
      
              if (document.getElementById('hidFormSaved').value == "1") {

                  RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidVICTIM_ID').value);
              }
          }
          catch (err) {
          }      
		</script>
</body>
</html>

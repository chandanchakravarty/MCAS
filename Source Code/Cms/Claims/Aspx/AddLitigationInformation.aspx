<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddLitigationInformation.aspx.cs" Inherits="Cms.Claims.Aspx.AddLitigationInformation" %>

<%@ Register assembly="CmsButton" namespace="Cms.CmsWeb.Controls" tagprefix="cmsb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AddLitigationInfo</title>
    
       <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
	<script type="text/javascript" src="/cms/cmsweb/scripts/Calendar.js"></script>

     <script type="text/javascript" language="javascript">

        
         function initPage() {

             ApplyColor();


         }
         
         //Custom validator function for premium > 0
         function validateRequestedAmount(objSource, objArgs) {
            
             var Premium = document.getElementById(objSource.controltovalidate).value;
             Premium = FormatAmountForSum(Premium);
             if (parseFloat(Premium) >= 0)
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
             
             if (Amount != "" && parseFloat(Amount) >= 0)
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
                                                <asp:label id="capJUDICIAL_PROCESS_NO" 
                        runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:textbox id="txtJUDICIAL_PROCESS_NO" runat="server" size="32" maxlength="40"></asp:textbox>
                                                <br />
                                                <asp:RequiredFieldValidator ID="rfvJUDICIAL_PROCESS_NO" runat="server" 
                                                    ControlToValidate="txtJUDICIAL_PROCESS_NO" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </TD>
										<TD  width="33%" class="midcolora" valign="top">
                                                <asp:label id="capJUDICIAL_COMPLAINT_STATE" 
                        runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:dropdownlist id="cmbJUDICIAL_COMPLAINT_STATE" 
                                                    
                      runat="server">
									</asp:dropdownlist>
                                                <br />
                                                <asp:RequiredFieldValidator ID="rfvJUDICIAL_COMPLAINT_STATE" runat="server" 
                                                    ControlToValidate="cmbJUDICIAL_COMPLAINT_STATE" Display="Dynamic"></asp:RequiredFieldValidator>
												<TD class="midcolora" valign="top">
                                                <asp:label id="capJUDICIAL_PROCESS_DATE" runat="server"></asp:label>
                                                   <span class="mandatory">*</span> <br /> <%--Added by aditya for itrack # 1503 on 09-08-2011--%>
                                                <asp:textbox id="txtJUDICIAL_PROCESS_DATE" 
                        runat="server" size="32" maxlength="10" Width="120px"></asp:textbox>
                                                <asp:HyperLink ID="hlkJUDICIAL_PROCESS_DATE" runat="server" CssClass="HotSpot">
                                                <asp:Image ID="imgJUDICIAL_PROCESS_DATE" runat="server" 
                                                    ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif" valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                                    <br />
                                                <asp:RegularExpressionValidator ID="revJUDICIAL_PROCESS_DATE" runat="server" 
                                                    ControlToValidate="txtJUDICIAL_PROCESS_DATE" Display="Dynamic"></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="rfvJUDICIAL_PROCESS_DATE" runat="server" 
                                                    ControlToValidate="txtJUDICIAL_PROCESS_DATE" Display="Dynamic"></asp:RequiredFieldValidator>  <%--Added by aditya for itrack # 1503 on 09-08-2011--%>
                                            </TD>
									</tr>
								
									<tr>
										<TD class="midcolora">
                                                <asp:label id="capPLAINTIFF_NAME" runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />  <%--Added by aditya for itrack # 1503 on 09-08-2011--%>
                                                <asp:textbox id="txtPLAINTIFF_NAME" runat="server" 
                        size="32" maxlength="40"></asp:textbox><br />
                        
<asp:RequiredFieldValidator ID="rfvPLAINTIFF_NAME" runat="server" 
                                                    ControlToValidate="txtPLAINTIFF_NAME" Display="Dynamic"></asp:RequiredFieldValidator> <%--Added by aditya for itrack # 1503 on 09-08-2011--%>
                                                </TD>
										<TD valign="top" class="midcolora">
                                                <asp:label id="capPLAINTIFF_CPF" runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:textbox id="txtPLAINTIFF_CPF" runat="server" 
                        size="32" maxlength="40" Width="120px" OnBlur="this.value=FormatCPFCNPJ(this.value); ValidatorOnChange();" ></asp:textbox>
                                                <br />
                                                <asp:RequiredFieldValidator ID="rfvCPF_CNPJ" runat="server" 
                                                    ControlToValidate="txtPLAINTIFF_CPF" Display="Dynamic" ErrorMessage=""></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revCPF_CNPJ" runat="server" 
                                                    ControlToValidate="txtPLAINTIFF_CPF" Display="Dynamic" ErrorMessage=""></asp:RegularExpressionValidator>
                                            <TD class="midcolora" valign="top">
                                                <asp:label id="capPLAINTIFF_REQUESTED_AMOUNT" 
                        runat="server"></asp:label>
                                               <span class="mandatory">*</span> <br />  <%--Added by aditya for itrack # 1503 on 09-08-2011--%>
                                                <asp:textbox id="txtPLAINTIFF_REQUESTED_AMOUNT" 
                        runat="server" size="32"  CssClass="INPUTCURRENCY" maxlength="10" Width="120px"></asp:textbox>
												<br />
												<asp:RegularExpressionValidator ID="revPLAINTIFF_REQUESTED_AMOUNT"  Runat="server" ControlToValidate="txtPLAINTIFF_REQUESTED_AMOUNT"
											Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:CustomValidator ID="csvPLAINTIFF_REQUESTED_AMOUNT" runat="server" ControlToValidate="txtPLAINTIFF_REQUESTED_AMOUNT" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateRequestedAmount"></asp:CustomValidator>	
                                                <asp:RequiredFieldValidator ID="rfvPLAINTIFF_REQUESTED_AMOUNT" runat="server" 
                                                    ControlToValidate="txtPLAINTIFF_REQUESTED_AMOUNT" Display="Dynamic"></asp:RequiredFieldValidator>  <%--Added by aditya for itrack # 1503 on 09-08-2011--%>
                                                </TD>
									</tr>
									<tr>
										<TD class="midcolora" valign="top">
                                                <asp:label id="capDEFEDANT_OFFERED_AMOUNT" 
                        runat="server"></asp:label>
                                              <span class="mandatory">*</span>  <br />  <%--Added by aditya for itrack # 1503 on 09-08-2011--%>
                                                <asp:textbox id="txtDEFEDANT_OFFERED_AMOUNT" 
                        runat="server" size="32"  CssClass="INPUTCURRENCY" maxlength="10" Width="120px"></asp:textbox>
                                                <br />
                                                <asp:RegularExpressionValidator ID="revDEFEDANT_OFFERED_AMOUNT"  Runat="server" ControlToValidate="txtDEFEDANT_OFFERED_AMOUNT"
											Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:CustomValidator ID="csvDEFEDANT_OFFERED_AMOUNT" runat="server" ControlToValidate="txtDEFEDANT_OFFERED_AMOUNT" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateOfferedAmount"></asp:CustomValidator>
                                                <asp:RequiredFieldValidator ID="rfvDEFEDANT_OFFERED_AMOUNT" runat="server" 
                                                    ControlToValidate="txtDEFEDANT_OFFERED_AMOUNT" Display="Dynamic"></asp:RequiredFieldValidator>	 <%--Added by aditya for itrack # 1503 on 09-08-2011--%>
                                            </TD>
										<TD class="midcolora" valign="top">
                                                <asp:label id="capESTIMATE_CLASSIFICATION" 
                        runat="server"></asp:label>
                                              <span class="mandatory">*</span>  <br />  <%--Added by aditya for itrack # 1503 on 09-08-2011--%>
                                                <asp:textbox id="txtESTIMATE_CLASSIFICATION" runat="server" 
                        size="32" maxlength="3" Width="120px" 
                                                    OnBlur="this.value=FormatCPFCNPJ(this.value); ValidatorOnChange();" ></asp:textbox>
                                                <br />
                                                
                                                <asp:RegularExpressionValidator ID="revESTIMATE_CLASSIFICATION" runat="server" 
                                                    ControlToValidate="txtESTIMATE_CLASSIFICATION" Display="Dynamic" 
                                                    ErrorMessage=""></asp:RegularExpressionValidator>
                                                                                            
                                               
                                                <asp:CustomValidator ID="csvESTIMATE_CLASSIFICATION" runat="server" 
                                                    ControlToValidate="txtESTIMATE_CLASSIFICATION" ErrorMessage="" 
                                                    Display="Dynamic" ClientValidationFunction="ValidateEstimateAmount"></asp:CustomValidator>
                                                    <asp:RequiredFieldValidator ID="rfvESTIMATE_CLASSIFICATION" runat="server" 
                                                    ControlToValidate="txtESTIMATE_CLASSIFICATION" Display="Dynamic"></asp:RequiredFieldValidator>	 <%--Added by aditya for itrack # 1503 on 09-08-2011--%>
                                                                                                                                        
                                            <TD class="midcolora"></TD>
									<tr>
                                    <TD class="midcolora" valign="top" colspan="3">
                                                <asp:label id="capEXPERT_SERVICE_ID" 
                        runat="server"></asp:label>
                                                <br />
                                                <asp:dropdownlist id="cmbEXPERT_SERVICE_ID" 
                                                    
                       runat="server">
									</asp:dropdownlist>
                                                </TD>
									</tr>
                                    </tr>
									<tr>
										<TD class="midcolora" valign="top">
                                                <asp:label id="capOPERATION_REASON" runat="server"></asp:label>
                                               <span class="mandatory">*</span> <br />  <%--Added by aditya for itrack # 1503 on 09-08-2011--%>
                                                <asp:dropdownlist id="cmbOPERATION_REASON"   runat="server"> 	</asp:dropdownlist>
                                                <asp:RequiredFieldValidator ID="rfvOPERATION_REASON" runat="server" 
                                                    ControlToValidate="cmbOPERATION_REASON" Display="Dynamic"></asp:RequiredFieldValidator>  <%--Added by aditya for itrack # 1503 on 09-08-2011--%>
                                                </TD>
										<TD class="midcolora" valign="top">
                                                <br />
                                                <TD class="midcolora" valign="top">
                                                    &nbsp;</TD>
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
                                                                        <cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" 
                                                                            onclick="btnActivateDeactivate_Click"></cmsb:cmsbutton>
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
                <INPUT id="hidLITIGATION_ID" type="hidden" value="" name="hidLITIGATION_ID" runat="server">
                <INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			    <INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
			    <INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			    <INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			    <INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
			     <INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
                                     
    </form>
   

      <script type="text/javascript">

          try {
              if (document.getElementById('hidFormSaved').value == "1") {
                 
                  RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidLITIGATION_ID').value);
              }
          }
          catch (err) {
          }      
		</script>
</body>
</html>

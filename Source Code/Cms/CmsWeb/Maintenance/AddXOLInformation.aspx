<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddXOLInformation.aspx.cs" Inherits="Cms.CmsWeb.Maintenance.AddXOLInformation" %>

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


         function initPage() 
         {
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
         
         function validateRate(objSource, objArgs) {

             var Limt = document.getElementById(objSource.controltovalidate).value;

             Limt = FormatAmountForSum(Limt);
             if (parseFloat(Limt) >= 0 && parseFloat(Limt) <= 100)
                 objArgs.IsValid = true;
             else
                 objArgs.IsValid = false;
         }

         
         function ResetTheForm() {
             document.AddClaimCoverage.reset();
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
    <form id="AddClaimCoverage" runat="server">
    
							<TABLE width="100%" align="center" border="0">
								<TBODY>
									<tr>
										<TD class="pageHeader" colSpan="4">
                    <asp:label id="lblRequiredFieldsInformation" runat="server" 
                                        ></asp:label>
                                        </TD>
									</tr>
									<tr>
										<td class="midcolorc" align="center" colSpan="4">
                    <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
                                  
                                        </td>
									</tr>
								
									<tr>
										<TD  class="midcolora" valign="top">
                                                <asp:label id="capLOB_ID" 
                        runat="server"></asp:label>
                                                <span class="mandatory">*</span></TD>
										<TD   class="midcolora" valign="top" colspan="3">
                                                <asp:DropDownList ID="cmbLOB_ID" runat="server">
                                                </asp:DropDownList>
                                             
                                                <br />
                                             
                                                <asp:RequiredFieldValidator ID="rfvLOB_ID" runat="server" 
                                                    ControlToValidate="cmbLOB_ID"></asp:RequiredFieldValidator>
											</TD>
									</tr>
								
									<tr>
										<TD  class="midcolora" valign="top">
                                                <asp:label id="capRECOVERY_BASE" runat="server"></asp:label>
                                                <span class="mandatory">*</span></TD>
										<TD   class="midcolora" valign="top">
                                                <asp:DropDownList ID="cmbRECOVERY_BASE" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                             
                                                <br />
                                             
                                                <asp:RequiredFieldValidator ID="rfvRECOVERY_BASE" runat="server" 
                                                    ControlToValidate="cmbRECOVERY_BASE" Display="Dynamic"></asp:RequiredFieldValidator>
											</TD>
                                        <TD class="midcolora" valign="top">
                                        <asp:label id="capLOSS_DEDUCTION" runat="server"></asp:label>
                                    </TD>
                                        <TD class="midcolora" valign="top">
                                        <asp:textbox id="txtLOSS_DEDUCTION" runat="server" 
                size="32" maxlength="14" Width="135px" CssClass="INPUTCURRENCY"></asp:textbox>
										    <br />
										<asp:RegularExpressionValidator ID="revLOSS_DEDUCTION"  Runat="server" ControlToValidate="txtLOSS_DEDUCTION"
									Display="Dynamic"></asp:RegularExpressionValidator>
                                    </TD>
									</tr>
								
									<tr>
										<TD class="midcolora" valign="top">
                                                <asp:label id="capAGGREGATE_LIMIT" runat="server"></asp:label>
                                                </TD>
										<TD   class="midcolora" valign="top">
                                                <asp:textbox id="txtAGGREGATE_LIMIT" runat="server" 
                        size="32" maxlength="14" Width="135px" CssClass="INPUTCURRENCY"></asp:textbox>
                                                <br />
												<asp:RegularExpressionValidator ID="revAGGREGATE_LIMIT"  Runat="server" ControlToValidate="txtAGGREGATE_LIMIT"
											Display="Dynamic"></asp:RegularExpressionValidator>
											</TD>
                                        <TD class="midcolora" valign="top">
                                                <asp:label id="capUSED_AGGREGATE_LIMIT" runat="server"></asp:label>
                                    </TD>
                                        <TD class="midcolora" valign="top">
                                                <asp:textbox id="txtUSED_AGGREGATE_LIMIT" runat="server" 
                        size="32" maxlength="14" Width="135px" CssClass="INPUTCURRENCY"></asp:textbox>
										    <br />
                                    </TD>
									</tr>
								
									<tr>
										<TD class="midcolora" valign="top">
                                        <asp:label id="capMIN_DEPOSIT_PREMIUM" 
                runat="server"></asp:label>
                                                </TD>
										<TD   class="midcolora" valign="top">
                                        <asp:textbox id="txtMIN_DEPOSIT_PREMIUM" 
                runat="server" size="32"  CssClass="INPUTCURRENCY" maxlength="14" Width="135px"></asp:textbox>
										        <br />
										<asp:RegularExpressionValidator ID="revMIN_DEPOSIT_PREMIUM"  Runat="server" ControlToValidate="txtMIN_DEPOSIT_PREMIUM"
									Display="Dynamic"></asp:RegularExpressionValidator>
											</TD>
                                        <TD class="midcolora" valign="top">
                                                <asp:label id="capFLAT_ADJ_RATE" runat="server"></asp:label>
                                    </TD>
                                        <TD class="midcolora" valign="top">
                                                <asp:textbox id="txtFLAT_ADJ_RATE" runat="server" 
                        size="32" maxlength="7" Width="135px" 
                                                    OnBlur="this.value=FormatCPFCNPJ(this.value); ValidatorOnChange();" 
                                                    CssClass="INPUTCURRENCY" ></asp:textbox>
                                                <br />
                                                <asp:RegularExpressionValidator ID="revFLAT_ADJ_RATE" runat="server" 
                                                    ControlToValidate="txtFLAT_ADJ_RATE" Display="Dynamic" 
                                                    ErrorMessage=""></asp:RegularExpressionValidator>
                                                <asp:CustomValidator ID="csvFLAT_ADJ_RATE" runat="server" 
                                                    ControlToValidate="txtFLAT_ADJ_RATE" 
                                                    ClientValidationFunction="validateRate" Display="Dynamic"></asp:CustomValidator>
                                    </TD>
									</tr>
								
									<tr>
										<TD  class="midcolora" valign="top">
                                        <asp:label id="capREINSTATE_PREMIUM_RATE" runat="server"></asp:label>
                                                </TD>
										<TD   class="midcolora" valign="top">
                                                <asp:textbox id="txtREINSTATE_PREMIUM_RATE" runat="server" 
                        size="32" maxlength="7" Width="135px" CssClass="INPUTCURRENCY" 
                                                        ></asp:textbox>
                                                    <br />
												<asp:RegularExpressionValidator ID="revREINSTATE_PREMIUM_RATE"  Runat="server" ControlToValidate="txtREINSTATE_PREMIUM_RATE"
											Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:CustomValidator ID="csvREINSTATE_PREMIUM_RATE" runat="server" 
                                                    ControlToValidate="txtREINSTATE_PREMIUM_RATE" 
                                                        ClientValidationFunction="validateRate" Display="Dynamic"></asp:CustomValidator>
                                                <br />
											</TD>
                                        <TD class="midcolora" valign="top">
                                                <asp:label id="capREINSTATE_NUMBER" runat="server"></asp:label>
                                    </TD>
                                        <TD class="midcolora" valign="top">
                                                <asp:textbox id="txtREINSTATE_NUMBER" runat="server" 
                        size="32" maxlength="9" Width="135px" CssClass="INPUTCURRENCY" 
                                                  ></asp:textbox>
                                                    <br />
												<asp:RegularExpressionValidator ID="revREINSTATE_NUMBER"  Runat="server" ControlToValidate="txtREINSTATE_NUMBER"
											Display="Dynamic"></asp:RegularExpressionValidator>
                                            </TD>
									</tr>
								
									<tr>
										<TD  class="midcolora" valign="top" width="25%">
                                        <asp:label id="capPREMIUM_DISCOUNT" runat="server"></asp:label>
                                                </TD>
										<TD   class="midcolora" valign="top" width="25%">
                                                <asp:textbox id="txtPREMIUM_DISCOUNT" runat="server" 
                        size="32" maxlength="7" Width="135px" CssClass="INPUTCURRENCY" 
                                                    ></asp:textbox>
                                                    <br />
												<asp:RegularExpressionValidator ID="revPREMIUM_DISCOUNT"  Runat="server" ControlToValidate="txtPREMIUM_DISCOUNT"
											Display="Dynamic"></asp:RegularExpressionValidator>
                                                <br />
                                                <asp:CustomValidator ID="csvPREMIUM_DISCOUNT" runat="server" 
                                                    ControlToValidate="txtPREMIUM_DISCOUNT" 
                                                    ClientValidationFunction="validateRate"></asp:CustomValidator>
											</TD>
                                        <TD class="midcolora" valign="top" width="25%">
                                                <asp:label id="capMIN_CLAIM_LIMIT" runat="server"></asp:label>
                                        </TD>
                                        <TD class="midcolora" valign="top">
                                                <asp:textbox id="txtMIN_CLAIM_LIMIT" runat="server" 
                        size="32" maxlength="9" Width="135px" CssClass="INPUTCURRENCY" 
                                                  ></asp:textbox>
                                                    <br />
												<asp:RegularExpressionValidator ID="revMIN_CLAIM_LIMIT"  Runat="server" ControlToValidate="txtMIN_CLAIM_LIMIT"
											Display="Dynamic"></asp:RegularExpressionValidator>
                                            </TD>
									</tr>
								
									<tr>
										<TD colspan="4" class="midcolora">
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
                <INPUT id="hidXOL_ID" type="hidden" value="" name="hidXOL_ID" runat="server">                
			    <INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
			    <INPUT id="hidIS_ACTIVE" type="hidden" value="" name="hidIS_ACTIVE" runat="server">
			   	<INPUT id="hidCONTRACT_ID" type="hidden" value="" name="hidCONTRACT_ID" runat="server">

                 
							                    
    </form>
   

      <script type="text/javascript">

          try {
      
              if (document.getElementById('hidFormSaved').value == "1") {

                  RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidXOL_ID').value);
              }
          }
          catch (err) {
          }      
		</script>
</body>
</html>

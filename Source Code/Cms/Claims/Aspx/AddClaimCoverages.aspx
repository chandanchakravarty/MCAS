<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddClaimCoverages.aspx.cs" Inherits="Cms.Claims.Aspx.AddClaimCoverages" %>

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

         function validateLimitRange(sender, args) {
             if (document.getElementById('revLIMIT_1').isvalid == false || document.getElementById('revPOLICY_LIMIT').isvalid == false)//Added By Abhinav For Itrack-971
                 return 
            
             var input = args.Value;
             input = FormatAmountForSum(input)
             var max = 922337203685477.5807;
             if (parseFloat(input) <= parseFloat(max)) {
                 args.IsValid = true;
             }
             else {
                 args.IsValid = false;
             }


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
    <style type="text/css">
        .style1
        {
            height: 25px;
        }
    </style>
    </head>
<body oncontextmenu="return false;" leftMargin="0" topMargin="0"  onload="initPage();">
    <form id="AddClaimCoverage" runat="server">
    
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
										<TD width="33%" class="midcolora" valign="top" colspan="3">
                                                <asp:label id="capPRODUCT_LIST" 
                        runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:DropDownList ID="cmbPRODUCT_LIST" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="cmbPRODUCT_LIST_SelectedIndexChanged">
                                                </asp:DropDownList>
                                             
                                                <br />
                                                <br />
                                             
                                                </TD>
									</tr>
								
									<tr>
										<TD class="midcolora" valign="top" colspan="3">
                                                <asp:label id="capCOVERAGE_CODE_ID" 
                        runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:DropDownList ID="cmbCOVERAGE_CODE_ID" runat="server">
                                                </asp:DropDownList>
                                             
                                                <asp:RequiredFieldValidator ID="rfvCOVERAGE_CODE_ID" runat="server" 
                                                    ControlToValidate="cmbCOVERAGE_CODE_ID" ErrorMessage="Please select coverage."></asp:RequiredFieldValidator>
                                                <br />
                                                <br />
                                             
                                                </TD>
									</tr>
								
									<tr>
										<TD width="33%" class="midcolora" valign="top">
                                                <asp:label id="capVICTIM_ID" 
                        runat="server"></asp:label>
                                                <br />
                                                <asp:DropDownList ID="cmbVICTIM_ID" runat="server">
                                                </asp:DropDownList>
                                                </TD>
										<TD  width="33%" class="midcolora" valign="top">
                                                <asp:label id="capLIMIT_OVERRIDE" 
                        runat="server"></asp:label>
                                                <br />
                                                <asp:DropDownList ID="cmbLIMIT_OVERRIDE" runat="server">
                                                </asp:DropDownList>
                                                <TD class="midcolora" valign="top">
                                                <asp:label id="capLIMIT_1" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtLIMIT_1" runat="server" 
                        size="32" maxlength="20" Width="127px" CssClass="INPUTCURRENCY"></asp:textbox>
                                                <br />
												<asp:RegularExpressionValidator ID="revLIMIT_1"  Runat="server" ControlToValidate="txtLIMIT_1"
											Display="Dynamic"></asp:RegularExpressionValidator>
											<asp:CustomValidator ID="csvLIMIT1" runat="server" ControlToValidate="txtLIMIT_1" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimitRange" ></asp:CustomValidator>	
                                            </TD>
									</tr>
								
									<tr>
										<TD width="33%" class="midcolora" valign="top">
                                                <asp:label id="capPOLICY_LIMIT" 
                        runat="server"></asp:label>
                                                    <br />
                                                <asp:textbox id="txtPOLICY_LIMIT" 
                        runat="server" size="32"  CssClass="INPUTCURRENCY" maxlength="20" Width="120px"></asp:textbox>
                        
												    <br />
												<asp:RegularExpressionValidator ID="revPOLICY_LIMIT"  Runat="server" ControlToValidate="txtPOLICY_LIMIT"
											Display="Dynamic"></asp:RegularExpressionValidator>
											<asp:CustomValidator ID="csvPOLICY_LIMIT" runat="server" ControlToValidate="txtPOLICY_LIMIT" ErrorMessage="" Display="Dynamic" ClientValidationFunction="validateLimitRange" ></asp:CustomValidator>	
                                                </TD>
										<TD  width="33%" class="midcolora" valign="top">
                                                <asp:label id="capMINIMUM_DEDUCTIBLE" runat="server"></asp:label>
                                                <br />
                                                <asp:textbox id="txtMINIMUM_DEDUCTIBLE" runat="server" 
                        size="32" maxlength="10" Width="120px" 
                                                    OnBlur="this.value=FormatCPFCNPJ(this.value); ValidatorOnChange();" 
                                                    CssClass="INPUTCURRENCY" ></asp:textbox>
                                                <br />
                                                <asp:RegularExpressionValidator ID="revMINIMUM_DEDUCTIBLE" runat="server" 
                                                    ControlToValidate="txtMINIMUM_DEDUCTIBLE" Display="Dynamic" 
                                                    ErrorMessage=""></asp:RegularExpressionValidator>
                                                <TD class="midcolora" valign="top">
                                                <asp:label id="capDEDUCTIBLE1_AMOUNT_TEXT" 
                        runat="server"></asp:label>
                                                    <br />
                                                <asp:textbox id="txtDEDUCTIBLE1_AMOUNT_TEXT" 
                        runat="server" size="32" maxlength="1000" Width="213px" Height="50px" TextMode="MultiLine"></asp:textbox>
                                            </TD>
									</tr>
								
									<tr>
										<TD width="33%" class="midcolora" valign="top">
                                                <asp:label id="capCOVERAGE_SI_FLAG" 
                        runat="server"></asp:label>
                                                <asp:CheckBox ID="chkCOVERAGE_SI_FLAG" runat="server" />
                                                </TD>
										<TD  width="33%" class="midcolora" valign="top" colspan="2">
                                                <asp:label id="lblPLS_CHECK_POLICY" 
                        runat="server" ForeColor="Red" Visible="False"></asp:label>
                                                    </tr>
								
									<tr>
										<TD width="33%" class="midcolora" valign="top" colspan="3">
                                                    <br />
                                                </TD>
									</tr>
								
									<tr>
										<TD colspan="3" class="midcolora">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="50%">
                                                <cmsb:cmsbutton class="clsButton" CausesValidation="false" id="btnReset" runat="server" Text="Reset" 
                                                     OnClientClick="javascript:return ResetTheForm();" />
                                                <asp:Button class="clsButton" id="btnCopy" runat="server" Text="Copy" 
                                                    onclick="btnCopy_Click" />
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
                <INPUT id="hidCLAIM_COV_ID" type="hidden" value="" name="hidCLAIM_COV_ID" runat="server">
                <INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			    <INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
			    <INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			    <INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			    <INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
			    <INPUT id="hidIS_USER_CREATED" type="hidden" value="" name="hidIS_USER_CREATED" runat="server">
                 <input type="hidden" id="hidPRODUCT_ID" name="hidPRODUCT_ID" runat="server">
                     <INPUT id="hidALLOW_ADD_COVERAGE" type="hidden" value="0" name="hidALLOW_ADD_COVERAGE" runat="server">
                     <INPUT id="hidACC_COI_FLG" type="hidden" value="0" name="hidACC_COI_FLG" runat="server">
                     
                 
							                    
    </form>
   

      <script type="text/javascript">

          try {
      
              if (document.getElementById('hidFormSaved').value == "1") {

                  RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidCLAIM_COV_ID').value);
              }
          }
          catch (err) {
          }      
		</script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBeneficiaryDetails.aspx.cs" Inherits="Cms.Policies.Aspx.AddBeneficiaryDetails" %>

<%@ Register assembly="CmsButton" namespace="Cms.CmsWeb.Controls" tagprefix="cmsb" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AddBeneficiaryDetails</title>
    
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

          function ResetTheForm() {
              document.AddBeneficiaryInfo.reset();
             return false;
         }

         $(document).ready(function() {
         $("#txtBENEFICIARY_SHARE").blur();


     });

    

//     function Validate(objSource, objArgs) { 
//         
//         var Limt = objArgs.Value;

//         Limt = FormatAmountForSum(Limt);
//          objSource.innerHTML  = document.getElementById('hidmsg1').value;
//          if (parseFloat(Limt) >= 0 && parseFloat(Limt) <= 100) {
//              objArgs.IsValid = true;             
//          }
//          else {
//              objArgs.IsValid = false;
//              return false;          
//          }
//         
//             var newValue = objArgs.Value;

//            objSource.innerHTML  = document.getElementById('hidmsg2').value;
//             var OldTotalPercent = document.getElementById('hidOLDTOTALPERCENT').value;
//             newValue = FormatAmountForSum(newValue);
//             if (OldTotalPercent == "")
//                OldTotalPercent = 0;
//             else                 
//                 OldTotalPercent = FormatAmountForSum(OldTotalPercent);

//             var TotalPercent = parseFloat(newValue) + parseFloat(OldTotalPercent);
//             if (TotalPercent > 100) {
//                 objArgs.IsValid = false;
//                 return false;
//             }
//             else {
//                 return true;
//             }
//             return;       
//                 
//         }
//         function FormatAmountForSum(num) {
//             
//             num = ReplaceAll(num, ',', '.');             
//             return num;
//         }

         function FormatAmountForSum(num) {
             num = ReplaceAll(num, sGroupSep, '');
             num = ReplaceAll(num, sDecimalSep, '.');


             return num;
         }
         function Validate(objSource, objArgs) { 

             var comm = parseFloat(FormatAmountForSum(document.getElementById(objSource.controltovalidate).value));

             if (comm < 0 || comm > 100) {
                 document.getElementById(objSource.controltovalidate).select();
                 objArgs.IsValid = false;
             }
             else
                 objArgs.IsValid = true;
         } 	//validate commission %
                
     </script>
</head>
<body oncontextmenu="return false;" leftMargin="0" topMargin="0"  onload="initPage();">
    <form id="AddBeneficiaryInfo" runat="server">
    
   
							<TABLE  id="tblBody" runat="server" width="100%" align="center" border="0">
								<TBODY >
							<tr>
								<td class="midcolorc" align="right" colspan="3">
                <asp:Label ID="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
            </td>
								</tr>
									<tr>
									<TD class="pageHeader" runat="server" id="capmsg" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
								</tr>
									<tr>
										<td class="midcolorc" align="center" colSpan="3">
                    <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
                                  
                                        </td>
									</tr>
									<tr id="trBody">
									
								                                        
										<TD width="33%" class="midcolora" valign="top">
                                                <asp:label id="capBENEFICIARY_NAME" runat="server">BENEFICIARY NAME</asp:label>
                                               <span class="mandatory">*</span><br />
                                                <asp:textbox id="txtBENEFICIARY_NAME" runat="server" size="32" maxlength="60" 
                                                    Width="189px"></asp:textbox>
                                                <br />
                                              <asp:requiredfieldvalidator id="rfvBENEFICIARY_NAME" runat="server" Display="Dynamic" ErrorMessage="Name can't be blank."
										ControlToValidate="txtBENEFICIARY_NAME"></asp:requiredfieldvalidator>
                                                </TD>
                                                <TD width="33%" class="midcolora" valign="top">
                                                <asp:label id="capBENEFICIARY_SHARE" runat="server">BENEFICIARY SHARE</asp:label>
                                               <br />
                                                <asp:textbox id="txtBENEFICIARY_SHARE" runat="server" size="32" maxlength="8" 
                                                  style="TEXT-ALIGN: right" CssClass="INPUTCURRENCY" onblur="this.value=formatAmount(this.value,2);"  onChange = "this.value=formatAmount(this.value,2)" Width="184px" CausesValidation="true"></asp:textbox>
                                                <br />
                                              
                                                <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
                                                    ID="revBENEFICIARY_SHARE" ControlToValidate="txtBENEFICIARY_SHARE"></asp:RegularExpressionValidator> 
                                                     <asp:CustomValidator ID="csvBENEFICIARY_SHARE" Display="Dynamic" ControlToValidate="txtBENEFICIARY_SHARE"
                                                    ClientValidationFunction="Validate" runat="server" ></asp:CustomValidator>  
                                                    
                                                </TD>
	
										
											<TD class="midcolora" valign="top">
                                                <asp:label id="capBENEFICIARY_RELATION" runat="server">BENEFICIARY RELATION</asp:label>
                                                    <br />
                                                <asp:textbox id="txtBENEFICIARY_RELATION" runat="server" size="32" 
                                                    maxlength="60" Width="200px" ></asp:textbox>
                        
                                              
                                            </TD>
												
									</tr>
								
									
									
									<tr id="trBody1">
									
										<TD colspan="3" class="midcolora">
                   <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="90%">
                                                <cmsb:cmsbutton class="clsButton" CausesValidation="false" id="btnReset" runat="server" Text="Reset" 
                                                     OnClientClick="javascript:return ResetTheForm();" />
					                        </td>
					                        <td >
                                        <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server"  causesvalidation="false" align="right" onclick="btnDelete_Click"></cmsb:cmsbutton>
                                     </td>
                            <td>
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
                
                <INPUT id="hidBeneficiaryIndexID" type="hidden" value="NEW" name="hidBeneficiaryIndexID" runat="server">
                <INPUT id="hidRisk_Id" type="hidden" value="" name="hidRisk_Id" runat="server">
                <INPUT id="hidOLDTOTALPERCENT" type="hidden" value="" name="hidOLDTOTALPERCENT" runat="server">
                <INPUT id="hidLAYER_ERROR_MESSAGE" type="hidden" value="" name="hidLAYER_ERROR_MESSAGE" runat="server">
                 <INPUT id="hidCONTRACT_ID" type="hidden" value="0" name="hidCONTRACT_ID" runat="server">  
                 <INPUT id="hidmsg1" type="hidden" value="0" name="hidmsg1" runat="server">  
                 <INPUT id="hidmsg2" type="hidden" value="0" name="hidmsg2" runat="server">   
                 <INPUT id="hidCO_APPLICANT_ID" type="hidden" value="0" name="hidCO_APPLICANT_ID" runat="server">   
                                  
    </form>
   

      <script type="text/javascript">

          try {
              if (document.getElementById('hidFormSaved').value == "1") {

                  RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidBeneficiaryIndexID').value);
              }
          }
          catch (err) {
          }      
		</script>
</body>
</html>

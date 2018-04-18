<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMonetaryDetails.aspx.cs" Inherits="Cms.CmsWeb.Maintenance.AddMonetaryDetails" %>

<%@ Register assembly="CmsButton" namespace="Cms.CmsWeb.Controls" tagprefix="cmsb" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AddMonataryDetails</title>
    
       <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
		
	<script type="text/javascript" src="/cms/cmsweb/scripts/Calendar.js"></script>

     <script type="text/javascript" language="javascript">
         function initPage() {

             ApplyColor();


         }

         function TriggerOnBlurFunction() {

             $('#txtINTEREST_RATE').blur();
             $('#txtINFLATION_RATE').blur();

             return false;
         }

         function FormatAmountForSum(num) {
            
             num = ReplaceAll(num, sBaseDecimalSep, '.');
             return num;
         }


         function validateLimit(objSource, objArgs) {
             if (document.getElementById('revINFLATION_RATE').isvalid == false||document.getElementById('revINTEREST_RATE').isvalid == false)
             return
           
             var Limt = document.getElementById(objSource.controltovalidate).value;

             Limt = FormatAmountForSum(Limt);
             if (parseFloat(Limt) >= 0 && parseFloat(Limt) <= 100)
                 objArgs.IsValid = true;
             else
                 objArgs.IsValid = false;
         }

          function ResetTheForm() {
             document.AddMonetaryInfo.reset();
             return false;
         }
         
         function allnumeric(objSource, objArgs) {
             var numbers = /\d{1,2}\/\d\d?\/\d{4}/;
             var inputxt = document.getElementById('txtDATE').value;
             if (document.getElementById(objSource.controltovalidate).value.match(numbers)) {
                 document.getElementById('revDATE').setAttribute('enabled', true);
                 objArgs.IsValid = true;
             }
             else {
                 document.getElementById('revDATE').setAttribute('enabled', false);
                 document.getElementById('revDATE').style.display = 'none';
                 document.getElementById('csvDATE').setAttribute('enabled', true);
                 document.getElementById('csvDATE').style.display = 'inline';
                 objArgs.IsValid = false;
             }
         } 
             
                
     </script>
</head>
<body oncontextmenu="return false;" leftMargin="0" topMargin="0"  onload="initPage();">
    <form id="AddMonetaryInfo" runat="server">
    
   
							<TABLE width="100%" align="center" border="0">
								<TBODY>
									<tr>
									<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
								</tr>
									<tr>
										<td class="midcolorc" align="center" colSpan="3">
                    <asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
                                  
                                        </td>
									</tr>
									<tr>
									
								                                        
										<TD width="33%" class="midcolora" valign="top">
                                                <asp:label id="capINFLATION_RATE" runat="server"></asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:textbox id="txtINFLATION_RATE" style="TEXT-ALIGN: right" 
                                                    CssClass="INPUTCURRENCY" runat="server" size="32" maxlength="5" 
                                                    Width="90px"></asp:textbox>
                                                <br />
                                                <asp:RequiredFieldValidator ID="rfvINFLATION_RATE" runat="server" Display="Dynamic"
                                ErrorMessage="Inflation Rate can't be blank." ControlToValidate="txtINFLATION_RATE"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                    ID="revINFLATION_RATE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                    ControlToValidate="txtINFLATION_RATE"></asp:RegularExpressionValidator><asp:CustomValidator
                                        ID="csvINFLATION_RATE" Display="Dynamic" ControlToValidate="txtINFLATION_RATE"
                                        ClientValidationFunction="validateLimit" runat="server"></asp:CustomValidator>
                                                </TD>
                                                <TD width="33%" class="midcolora" valign="top">
                                                <asp:label id="capINTEREST_RATE" runat="server">Interest_Rate</asp:label>
                                                <span class="mandatory">*</span><br />
                                                <asp:textbox id="txtINTEREST_RATE" style="TEXT-ALIGN: right" 
                                                        CssClass="INPUTCURRENCY" runat="server" size="32" maxlength="5" 
                                                        Width="90px"></asp:textbox>
                                                <br />
                                                <asp:RequiredFieldValidator ID="rfvINTEREST_RATE" runat="server" Display="Dynamic"
                                ErrorMessage="Interest rates can't be blank." ControlToValidate="txtINTEREST_RATE"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                    ID="revINTEREST_RATE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                    ControlToValidate="txtINTEREST_RATE"></asp:RegularExpressionValidator><asp:CustomValidator
                                        ID="csvINTEREST_RATE" Display="Dynamic" ControlToValidate="txtINTEREST_RATE"
                                        ClientValidationFunction="validateLimit" runat="server"></asp:CustomValidator>

                                                </TD>
										
										
											<TD class="midcolora" valign="top">
                                                <asp:label id="capDATE" runat="server"></asp:label><SPAN class="mandatory">*</SPAN>
                                                    <br />
                                                <asp:textbox id="txtDATE" 
                        runat="server" size="32" maxlength="10" Width="104px"></asp:textbox>
                        
                                                <asp:HyperLink ID="hlkDATE" runat="server" CssClass="HotSpot">
                                                <asp:Image ID="imgDATE" runat="server" 
                                                    ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif" valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfvDATE" runat="server" Display="Dynamic"
                                ErrorMessage="Date can't be blank." ControlToValidate="txtDATE"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revDATE" runat="server" 
                                                    ControlToValidate="txtDATE" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:CustomValidator ID="csvDATE" runat="server" ControlToValidate="txtDATE" Display="Dynamic" ClientValidationFunction="allnumeric"></asp:CustomValidator>
                                            </TD>
												
									</tr>
								
									
									
									
									
										<TD colspan="3" class="midcolora">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="50%">
                                                <cmsb:cmsbutton class="clsButton" CausesValidation="false" id="btnReset" runat="server" Text="Reset" 
                                                     OnClientClick="javascript:return ResetTheForm();" />
					                        </td>
                            <td class="midcolorr">
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
                
                <INPUT id="hidMonetaryIndexID" type="hidden" value="NEW" name="hidMonetaryIndexID" runat="server">
                                     
    </form>
   

      <script type="text/javascript">

          try {
              if (document.getElementById('hidFormSaved').value == "1") {

                  RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidMonetaryIndexID').value);
              }
          }
          catch (err) {
          }      
		</script>
</body>
</html>

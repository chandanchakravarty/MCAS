<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterPolicyBillingInfo.aspx.cs" Inherits="Cms.Policies.Aspx.MasterPolicyBillingInfo" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Billing info</title>
      <style>
        .trd
        {
            display: none;
        }
    </style>
     
        <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >

        <script src="/cms/cmsweb/scripts/xmldom.js"></script>

        <script src="/cms/cmsweb/scripts/common.js"></script>

        <script src="/cms/cmsweb/scripts/form.js"></script>

        <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>

        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>

        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/JQCommon.js"></script>

        <script language="javascript" type="text/javascript">
            function Init() {
                ApplyColor();
                ChangeColor();
                DisableSave();
                btnShowHideAllBoleto();
            }
            function btnShowHideAllBoleto() {
                
                var Count = document.getElementById('hidGRID_ROW_COUNT').value;
                if (Count != '' && parseInt(Count) != 0 && Count != 'undefined') {
                    if (document.getElementById('btnView_All_Boleto') != null)
                        document.getElementById('btnView_All_Boleto').style.display = 'inline';
                    if (document.getElementById('btnBoletoReprint') != null)
                        document.getElementById('btnBoletoReprint').style.display = 'inline';
                }
                else {
                    if (document.getElementById('btnView_All_Boleto') != null)
                        document.getElementById('btnView_All_Boleto').style.display = 'none';
                    if (document.getElementById('btnBoletoReprint') != null)
                        document.getElementById('btnBoletoReprint').style.display = 'none';
                }
                if ($("#hidCO_INSURANCE").val() == "14549")//Follower (Added by Pradeep Kushwaha on 20-Jan-2011 )
                {
                    $("#btnBoletoReprint").hide();
                } 
            }
            function OpenBoletoReprint() {
                
                document.location = "/cms/Policies/Aspx/BoletoRePrint.aspx?CALLED_FOR=MST_POL_BILL&CALLEDFROM=" + document.getElementById("hidCALLED_FROM").value;
                return false;
            }

            function viewAllBoleto() {

                var POLICY_VERSION = document.getElementById('hidPOLICY_VERSION_ID').value;
                GenerateBoleto('0', 'ALL', POLICY_VERSION);
                return false;
            }
            function DisableSave() {
                if (document.getElementById('btnSave') != null) {
                    if (parseInt(document.getElementById('hidGRID_ROW_COUNT').value) < 1)
                        document.getElementById('btnSave').disabled = true;
                    else
                        document.getElementById('btnSave').disabled = false;
                }
            }

            //claculate installment total

            function InstallmentTotal(objcontrol) {
                var objcontrolID = objcontrol.id;
                var num = objcontrol.value;
                
                var splID = objcontrolID.split('_');   //Genrate dynamic id for each textbox in gridview for every row for calculate sum 
                var PREMIUM = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtPREMIUM';  //installment premium textbox
                var INTEREST_AMOUNT = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtINTEREST_AMOUNT'; //installment interest amount textbox
                var FEE = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtFEE'; //installment fees amount textbox
                var TAXES = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtTAXES'; //installment taxes amount textbox
                var TOTAL = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'txtTOTAL'; //installment total(sum of all amount textbox)
                //var hidTOTAL = splID[0] + '_' + splID[1] + '_' + splID[2] + '_' + 'hidTOTAL'; //hid field for calculated sum

        
                /* get the value from gridview textbox For calculate sum row wise and assign it into total & hid total column */

                var txtPREMIUM = document.getElementById(PREMIUM).value == '' ? '0' : document.getElementById(PREMIUM).value;
                txtPREMIUM = FormatAmountForSum(txtPREMIUM);


                var txtINTEREST_AMOUNT = document.getElementById(INTEREST_AMOUNT).value == '' ? '0' : document.getElementById(INTEREST_AMOUNT).value;
                txtINTEREST_AMOUNT = FormatAmountForSum(txtINTEREST_AMOUNT);

                var txtFEE = document.getElementById(FEE).value == '' ? '0' : document.getElementById(FEE).value;
                txtFEE = FormatAmountForSum(txtFEE);

                var txtTAXES = document.getElementById(TAXES).value == '' ? '0' : document.getElementById(TAXES).value;
                txtTAXES = FormatAmountForSum(txtTAXES);
               
                 // changed by praveer panghal for itrack no 1761
                
//            var hid_IOF_PERCENTAGE = document.getElementById('hid_IOF_PERCENTAGE').value == '' ? '0' : document.getElementById('hid_IOF_PERCENTAGE').value;
//            hid_IOF_PERCENTAGE = ReplaceAll(hid_IOF_PERCENTAGE, sGroupSep, '');
//            hid_IOF_PERCENTAGE = ReplaceAll(hid_IOF_PERCENTAGE, sDecimalSep, '.');   
//            var txtTAXES = parseFloat(parseFloat(hid_IOF_PERCENTAGE) * (parseFloat(txtPREMIUM) + parseFloat(txtINTEREST_AMOUNT) + parseFloat(txtFEE)) / 100);
//            txtTAXES = formatAmount(txtTAXES);
//            document.getElementById(TAXES).value = (txtTAXES);


                if( isNaN(parseFloat(txtPREMIUM)))
                    txtPREMIUM = '0';
                if (isNaN(parseFloat(txtINTEREST_AMOUNT)))
                    txtINTEREST_AMOUNT = '0';
                if (isNaN(parseFloat(txtFEE)))
                    txtFEE = '0';
                if (isNaN(parseFloat(txtTAXES)))
                    txtTAXES = '0';
                
                
               var sumTOTAL = parseFloat(txtPREMIUM)+ parseFloat(txtINTEREST_AMOUNT) + parseFloat(txtFEE) + parseFloat(txtTAXES)
                
                
                
                sumTOTAL = roundNumber(sumTOTAL, 2);

                /* End main installment textbox calculation part*/
                if (!isNaN(sumTOTAL)) {       
                    document.getElementById(TOTAL).value = formatAmount(parseFloat(sumTOTAL));
                }
                else {  //if calculated sum is less then 0 then assign it as blank .
                    document.getElementById(TOTAL).value = roundNumber('0', 2);
                }
            }

            function FormatAmountForSum(num) {
                num = ReplaceAll(num, sGroupSep, '');
                num = ReplaceAll(num, sDecimalSep, '.');
                return num;
            }
            function roundNumber(num, dec) {
                var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
                return result;
            }
            function GenerateBoleto(Installmentno, Type, Policy_version, CO_APPLICANT_ID) {
                
                var str;
                str = "/cms/Policies/Aspx/PolicyBoleto.aspx?CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value + "&POLICY_ID=" + document.getElementById("hidPOLICY_ID").value

		              + "&POLICY_VERSION_ID=" + Policy_version + "&INSTALLMENT_NO=" + Installmentno 
                if (CO_APPLICANT_ID != '' && CO_APPLICANT_ID != undefined)
                    str = str + '&CO_APPLICANT_ID=' + CO_APPLICANT_ID
                         
                if (Type != '' && Type != undefined)
                    str = str + '&GENERATE_ALL_INSTALLMENT=' + Type
                    
                window.open(str, "Boleto", "resizable=yes,toolbar=0,location=0, directories=0, status=0, menubar=0,scrollbars=yes,width=800,height=500,left=150,top=50");

            }

        function UserIsManager() {
             var mgr = <%= "'" + getIsUserSuperVisor() + "'" %>            
             var gridId="grdBILLING_INFO_ctl";
             var hidROWCOUNT = document.getElementById('hidGRID_ROW_COUNT').value;
             var hidFlag =   document.getElementById('hidFlag').value;
             // changed by praveer panghal for itrack no 1761
            if(mgr=="Y" &&  document.getElementById("hidCO_INSURANCE").value!= "14549" ){
             if (hidROWCOUNT != '' && parseInt(hidROWCOUNT) != 0 && hidROWCOUNT != 'undefined' && hidROWCOUNT !=1) 
             {
                    for (i = 2; i < parseInt(hidROWCOUNT) + 2; i++) {
                   
                    var ctlId = i
                    if (i <10) ctlId = "0" + i ;
                    document.getElementById(gridId + ctlId + "_txtFEE").setAttribute("readOnly",false);
                    document.getElementById(gridId + ctlId + "_txtINTEREST_AMOUNT").readOnly=false;
                    document.getElementById(gridId + ctlId + "_txtTAXES").readOnly=false;
                   }
            }
            }
             else
             { 
             if (hidROWCOUNT != '' && parseInt(hidROWCOUNT) != 0 && hidROWCOUNT != 'undefined' && hidFlag=="Y") 
             {
                    for (i = 2; i < parseInt(hidROWCOUNT) + 2; i++) {
                   
                    var ctlId = i
                    if (i <10) ctlId = "0" + i ;
                    document.getElementById(gridId + ctlId + "_txtFEE").setAttribute("readOnly",true);
                    document.getElementById(gridId + ctlId + "_txtINTEREST_AMOUNT").readOnly=true;
                    document.getElementById(gridId + ctlId + "_txtTAXES").readOnly=true;
                    
                   }
            }
                       
           }
          }
        </script>

    </head>
    <body oncontextmenu="return false;" leftmargin="0" rightmargin="0" ms_positioning="GridLayout"
        onload="Init();UserIsManager();">
        <%--	changes by praveer panghal for itrack no 1318--%>
		<div id="bodyHeight" class="pageContent" style='width:100%; overflow: auto;'>
        <form id="POL_MASTER_POLICY_BILLING_INFO" runat="server">
<%--        <p>
            <uc1:addressverification id="AddressVerification1" runat="server">
            </uc1:addressverification></p>--%>
        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="headereffectCenter" >
                    <asp:Label ID="lblHeader" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="midcolorc" >
                    <asp:Label ID="lblFormLoad" runat="server" CssClass="errmsg"></asp:Label>
                </td>
            </tr>
            <tbody>
             <tr>
                    <td class="midcolorc" style="height:15px">
                       
                    </td>
                </tr>
                <tr>
                    <td class="midcolorc" style="height:15px">
                       <asp:Label runat="server" ID="lblMessage" Text="" CssClass="errmsg"></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td class="midcolorc">
                        <asp:Label ID="lblFormLoadMessage" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
                    </td>
                </tr>
                <tr>
                  <td class="midcolorr">
                        <cmsb:CmsButton runat="server" id="btnGENRATE_INSTALLMENTS" 
                            CssClass="clsButton" Text="Generate Installments" 
                            onclick="btnGENRATE_INSTALLMENTS_Click" />
                    </td>
                </tr>
                 <tr>
                    <td class="midcolorc" style="height:15px;">                       
                    </td>
                </tr>
                 <tr>
                  <td class="midcolorc">
                    <asp:GridView AutoGenerateColumns="false" runat="server" ID="grdBILLING_INFO" 
                          Width="100%" onrowdatabound="grdBILLING_INFO_RowDataBound">
                      <HeaderStyle CssClass="headereffectWebGrid" HorizontalAlign="Center"></HeaderStyle>
                        <RowStyle CssClass="midcolora"></RowStyle>
                        <Columns> 
                        <%-- display None Column  start--%>
                         <asp:TemplateField  HeaderStyle-CssClass="trd" ItemStyle-CssClass="trd" FooterStyle-CssClass="trd">                          
                                <ItemStyle Width="0%" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblROW_ID" Text='<%# Eval("ROW_ID.CurrentValue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField   HeaderStyle-CssClass="trd" ItemStyle-CssClass="trd" FooterStyle-CssClass="trd" >                          
                                <ItemStyle Width="0%" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPOLICY_VERSION_ID" Text='<%# Eval("POLICY_VERSION_ID.CurrentValue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField   HeaderStyle-CssClass="trd" ItemStyle-CssClass="trd" FooterStyle-CssClass="trd" >                          
                                <ItemStyle Width="0%" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCO_APPLICANT_ID" Text='<%# Eval("CO_APPLICANT_ID.CurrentValue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <%-- end display none Column  --%>
                         <asp:TemplateField  >    
                         <ItemStyle Width="5%" />
                             <HeaderTemplate>
                                    <asp:Label runat="server" ID="capINSLAMENT_NO" Text="">Install #</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblINSTALLMENT_NO"  Text='<%# Eval("INSTALLMENT_NO.CurrentValue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                         
                         <asp:TemplateField >
                         <ItemStyle Width="5%" />
                             <HeaderTemplate>
                                    <asp:Label runat="server" ID="capTRAN_TYPE" Text="">Tran Type</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Label runat="server" ID="lblTRAN_TYPE" Text='<%# Eval("TRAN_TYPE.CurrentValue") %>'></asp:Label>
                                   <asp:Label runat="server" ID="lblENDO_NO" Text='<%# Eval("ENDO_NO.CurrentValue") %>' CssClass="trd" ></asp:Label>
                               </ItemTemplate>
                         </asp:TemplateField>
                         
                         <asp:TemplateField >
                         <ItemStyle Width="15%" />
                             <HeaderTemplate>
                                    <asp:Label runat="server" ID="capCO_APPLICANT_NAME" Text="">CoApp Name</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Label runat="server" ID="lblCO_APPLICANT_NAME" Text='<%# Eval("CO_APPLICANT_NAME.CurrentValue") %>'></asp:Label>
                               </ItemTemplate>
                         </asp:TemplateField>
                         
                          <asp:TemplateField >
                         <ItemStyle Width="10%" />
                             <HeaderTemplate>
                                    <asp:Label runat="server" ID="capINSTALLMENT_DATE" Text="">Installment Date</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox runat="server" ID="txtINSTALLMENT_DATE" Width="100%" Text='<%# Eval("INSTALLMENT_EFFECTIVE_DATE.CurrentValue") %>'></asp:TextBox>
                             <br /><asp:RequiredFieldValidator runat="server" ErrorMessage="" Display="Dynamic"
                                        ID="rfvINSTALLMENT_DATE" ControlToValidate="txtINSTALLMENT_DATE"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator runat="server"  ErrorMessage="" Display="Dynamic"
                                        ID="revINSTALLMENT_DATE" ControlToValidate="txtINSTALLMENT_DATE"></asp:RegularExpressionValidator>
                               </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField >
                          <ItemStyle Width="10%" />
                             <HeaderTemplate>
                                    <asp:Label runat="server" ID="capPREMIUM" Text="">Premiun</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <asp:TextBox runat="server" CssClass="inputcurrency" Width="100px" MaxLength="15" ID="txtPREMIUM" Text='<%# Convert.ToDecimal(Eval("INSTALLMENT_AMOUNT.CurrentValue")).ToString("N", numberFormatInfo) %>'></asp:TextBox>
                               <br /> <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
                                        ID="revPREMIUM" ControlToValidate="txtPREMIUM"></asp:RegularExpressionValidator>
                               </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField>
                          <ItemStyle Width="10%" />                                
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capINTEREST_AMOUNT" Text="">Policy Interest Amount</asp:Label>
                                </HeaderTemplate>
                            <ItemTemplate>
                                    <asp:TextBox runat="server"  CssClass="inputcurrency" Width="100px" MaxLength="15" ID="txtINTEREST_AMOUNT" Text='<%# Convert.ToDecimal(Eval("INTEREST_AMOUNT.CurrentValue")).ToString("N", numberFormatInfo) %>' ></asp:TextBox>
                                  <br /> <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
                                        ID="revINTEREST_AMOUNT" ControlToValidate="txtINTEREST_AMOUNT" Enabled="False"></asp:RegularExpressionValidator>
                            </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField>
                                <ItemStyle Width="10%" />
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capFEE" Text="">Policy Fee</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox runat="server"  CssClass="inputcurrency" Width="100px" MaxLength="15" ID="txtFEE" Text='<%# Convert.ToDecimal(Eval("FEE.CurrentValue")).ToString("N", numberFormatInfo) %>' ></asp:TextBox>
                                 <br /> <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
                                        ID="revFEE" ControlToValidate="txtFEE" Enabled="False"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                          </asp:TemplateField>
                          <asp:TemplateField>
                                <ItemStyle Width="10%" />
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capTAXES" Text="">Taxes</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox runat="server"   CssClass="inputcurrency" Width="100px" MaxLength="15" ID="txtTAXES" Text='<%# Convert.ToDecimal(Eval("TAXES.CurrentValue")).ToString("N", numberFormatInfo) %>' ></asp:TextBox>
                               <br />  <asp:RegularExpressionValidator runat="server" ErrorMessage="" Display="Dynamic"
                                        ID="revTAXES" ControlToValidate="txtTAXES" Enabled="False"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                          </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle Width="10%" />
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capTOTAL" Text="">Total</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox runat="server" CssClass="inputcurrency" Width="100px" ReadOnly="true" MaxLength="15" ID="txtTOTAL" Text='<%# Convert.ToDecimal(Eval("TOTAL.CurrentValue")).ToString("N", numberFormatInfo) %>'></asp:TextBox>
                                </ItemTemplate>
                          </asp:TemplateField>
                            <asp:TemplateField Visible ="false" >
                                <ItemTemplate  >
                                    <asp:Label runat="server" id="lblBOLETO_NO" Text= '<%# Eval("BOLETO_NO.CurrentValue") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="5%" />
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capRELEASED_STATUS" Text="">Released Status</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate >
                                    <asp:Label runat="server" Text='<%# Eval("RELEASED_STATUS.CurrentValue") %>' ID="lblRELEASED_STATUS"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                                <ItemStyle Width="5%" />
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capRECEIVED_DATE" Text="">Received  Date</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtRECEIVED_DATE" Width="100%" Text='<%# Eval("RECEIVED_DATE.CurrentValue") %>' ReadOnly="true"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                           <asp:TemplateField>
                                <ItemStyle Width="5%" VerticalAlign="Middle" />
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="capBOLETO" Text="Boleto"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href="javascript:GenerateBoleto('<%# Eval("INSTALLMENT_NO.CurrentValue") %>','','<%# Eval("POLICY_VERSION_ID.CurrentValue") %>','<%# Eval("CO_APPLICANT_ID.CurrentValue") %>');">
                                        <asp:Label runat="server" ID="lblBOLETO">View</asp:Label></a>
                                
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            
                            
                            
                        </Columns>
                    </asp:GridView>
                    </td>                    
                </tr>
                 <%--	changes by praveer panghal for itrack no 1318--%>
                <tr style='height:40px'>
                 <td class="midcolorr">  <asp:Button ID="btnView_All_Boleto" runat="server" CssClass="clsButton"   />
                 <asp:Button ID="btnBoletoReprint" runat="server" CausesValidation="true" Text="Boleto Reprint" OnClientClick="javascript:return OpenBoletoReprint()" CssClass="clsButton"  /> 
                 
                 <%--<cmsb:CmsButton ID="btnBoletoReprint" runat="server" CausesValidation="true" OnClientClick="javascript:return OpenBoletoReprint()" CssClass="clsButton"></cmsb:CmsButton>--%>
                        <cmsb:CmsButton runat="server" id="btnSave" CssClass="clsButton" Text="Save" 
                            onclick="btnSave_Click" />
                    </td>
                </tr>
            </tbody>
            <tr>
            <td>
                <input type="hidden" runat="server" id="hidCUSTOMER_ID" name="hidCUSTOMER_ID" />
                <input type="hidden" runat="server" id="hidPOLICY_ID" name="hidPOLICY_ID" />
                <input type="hidden" runat="server" id="hidPOLICY_VERSION_ID" name="hidPOLICY_VERSION_ID" />
                <input type="hidden" runat="server" id="hidCALLED_FROM" name="hidCALLED_FROM" />
                <input type="hidden" runat="server" id="hidPOLICY_STATUS" name="hidPOLICY_STATUS" />
                <input type="hidden" runat="server" id="hidGRID_ROW_COUNT" name="hidGRID_ROW_COUNT"  value="0"/>
                <input type="hidden" runat="server" id="hidCO_INSURANCE" name="hidCO_INSURANCE"  />
                <input type="hidden" runat="server" id="hidPROCESS_ID" name="hidPROCESS_ID" />
              <%--  change by praveer for itrack no 1511--%>
                <input type="hidden" runat="server" id="hidSELECTED_PLAN_ID" name="hidSELECTED_PLAN_ID" />
                 <input type="hidden" runat="server" id="hidPLAN_TYPE" name="hidPLAN_TYPE" /> <%--changed by praveer for itrack no 1567/TFS # 629--%>    
                <input type="hidden" runat="server" id="hid_IOF_PERCENTAGE" name="hid_IOF_PERCENTAGE" /> <%--changed by praveer for itrack no 1761--%> 
                <input type="hidden" runat="server" id="hidFlag" name="hidFlag" />     
                
            </td>
            </tr>
        </table>
        </form>
        </div>
    </body>
    </html>

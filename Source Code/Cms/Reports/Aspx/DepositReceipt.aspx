<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepositReceipt.aspx.cs" Inherits="Cms.Reports.Aspx.DepositReceipt" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet />
  
     <script language='javascript' type="text/javascript">
         
         function GenerateReceipt(RECEIPT_NUM, DEPOSIT_ID, CD_LINE_ITEM_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID) {
            
             var str = "/cms/Account/Aspx/GeneratePaymentReceipt.aspx?DEPOSIT_ID=" + DEPOSIT_ID + "&CD_LINE_ITEM_ID=" + CD_LINE_ITEM_ID + "&RECEIPT_NUM=" + RECEIPT_NUM +
                 "&CUSTOMER_ID=" + CUSTOMER_ID + "&POLICY_ID=" + POLICY_ID + "&POLICY_VERSION_ID=" + POLICY_VERSION_ID + "&CALLED_FOR=";
             window.open(str, "GenerateReceipt", "resizable=yes,toolbar=0,location=0, directories=0, status=0, menubar=0,scrollbars=yes,width=800,height=500,left=150,top=50")
              
         }

         function fnViewAllReceipt() {
              
           
             var DEPOSIT_ID = document.getElementById("hidDEPOSIT_ID").value;
             var str = "/cms/Account/Aspx/GeneratePaymentReceipt.aspx?DEPOSIT_ID=" + DEPOSIT_ID +"&CALLED_FOR=ALL";
             window.open(str, "GenerateReceipt", "resizable=yes,toolbar=0,location=0, directories=0, status=0, menubar=0,scrollbars=yes,width=800,height=500,left=150,top=50")
         
             return false;
         }
        
     </script>
     
</head>
<body>
    <form id="form1" runat="server">
  <table align="center" border="1"  width="100%" cellspacing="0" class='tableWidth'> 
   <tr> 
        <td align="center" class="headereffectCenter">
            <asp:Label ID="lblDEPOSIT" runat="server" ></asp:Label>
         </td>
  </tr>  
  <tr > <td align="right" width="100%">
  
     
            <asp:GridView AutoGenerateColumns="False" runat="server" ID="grdCOMMITED_DEPOSIT" 
                          Width="100%" onrowdatabound="grdCOMMITED_DEPOSIT_RowDataBound" 
                meta:resourcekey="grdCOMMITED_DEPOSITResource1" >
                      <HeaderStyle  CssClass="headereffectCenter" HorizontalAlign="Center"></HeaderStyle>
                        <RowStyle CssClass="midcolora" HorizontalAlign="Right"></RowStyle>
                        
                        <Columns> 
                          <asp:TemplateField >
                                 <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="25%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <HeaderTemplate >
                                   <asp:Label ID="lblNAME" runat="server" ></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle CssClass='DataGridRow' />
                                <ItemTemplate >
                                     <%#DataBinder.Eval(Container, "DataItem.NAME")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField >
                                 <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="8%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <HeaderTemplate >
                                   <asp:Label ID="lblPOLICY_NUMBER" runat="server" ></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle CssClass='DataGridRow' />
                                <ItemTemplate >
                                     <%#DataBinder.Eval(Container, "DataItem.POLICY_NUMBER")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField >
                                 <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="2%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <HeaderTemplate >
                                   <asp:Label ID="lblENDORSEMENT_NO" runat="server" ></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle CssClass='DataGridRow' />
                                <ItemTemplate >
                                     <%#DataBinder.Eval(Container, "DataItem.ENDORSEMENT_NO")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField >
                                 <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="2%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <HeaderTemplate >
                                   <asp:Label ID="lblINSTALLMENT_NO" runat="server" ></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle CssClass='DataGridRow' />
                                <ItemTemplate >
                                     <%#DataBinder.Eval(Container, "DataItem.INSTALLMENT_NO")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField >
                                 <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="25%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <HeaderTemplate >
                                  <asp:Label ID="lblOUR_NUMBER" runat="server" ></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle CssClass='DataGridRow' />
                                <ItemTemplate >
                                     <%#DataBinder.Eval(Container, "DataItem.OUR_NUMBER")%>
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField >
                                 <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="12%" VerticalAlign="Middle" HorizontalAlign="Right" />
                                <HeaderTemplate >
                                   <asp:Label ID="lblRISK_PREMIUM" runat="server"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle CssClass='DataGridRow' />
                                <ItemTemplate  >
                                     <%#DataBinder.Eval(Container, "DataItem.RISK_PREMIUM")%>
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField >
                                 <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Right" />
                                <HeaderTemplate >
                                    <asp:Label ID="lblFEE" runat="server" ></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle CssClass='DataGridRow' />
                                <ItemTemplate >
                                     <%#DataBinder.Eval(Container, "DataItem.FEE")%>
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField >
                                 <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Right" />
                                <HeaderTemplate >
                                    <asp:Label ID="lblTAX" runat="server" ></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle CssClass='DataGridRow' />
                                <ItemTemplate >
                                     <%#DataBinder.Eval(Container, "DataItem.TAX")%>
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField >
                                 <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="8%" VerticalAlign="Middle" HorizontalAlign="Right" />
                                <HeaderTemplate >
                                    <asp:Label ID="lblINTEREST" runat="server" ></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle CssClass='DataGridRow' />
                                <ItemTemplate >
                                     <%#DataBinder.Eval(Container, "DataItem.INTEREST")%>
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField>
                                 <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="8%" VerticalAlign="Middle" HorizontalAlign="Right" />
                                <HeaderTemplate >
                                    <asp:Label ID="lblLATE_FEE" runat="server" ></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle CssClass='DataGridRow' />
                                <ItemTemplate >
                                     <%#DataBinder.Eval(Container, "DataItem.LATE_FEE")%>
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField>
                                 <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="15%" VerticalAlign="Middle" HorizontalAlign="Right" />
                                <HeaderTemplate >
                                    <asp:Label ID="lblRECEIPT_AMOUNT" runat="server" ></asp:Label>
                                </HeaderTemplate>
                              
                                <ItemStyle CssClass='DataGridRow' />
                                <ItemTemplate >
                                     <%#DataBinder.Eval(Container, "DataItem.RECEIPT_AMOUNT")%>
                                </ItemTemplate>
                           </asp:TemplateField> 
                           <asp:TemplateField >
                                 <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="8%" VerticalAlign="Middle" HorizontalAlign="Left" />
                                <HeaderTemplate >
                                    <asp:Label ID="lblEXCEPTION_REASON" runat="server" ></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle CssClass='DataGridRow' />
                                <ItemTemplate  >
                                     <%#DataBinder.Eval(Container, "DataItem.EXCEPTION_REASON")%>
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField >
                                 <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <HeaderTemplate >
                                    <asp:Label ID="lblIS_APPROVE" runat="server" ></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle CssClass='DataGridRow' />
                                <ItemTemplate >
                                     <%#DataBinder.Eval(Container, "DataItem.IS_APPROVE")%>
                                </ItemTemplate>
                           </asp:TemplateField>
                            <%--iTrack 1323 Notes By Paula Dated 13-July-2011--%>
                            <asp:TemplateField >
                                 <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <HeaderTemplate >
                                    <asp:Label ID="lblRECEIPT_DATE" runat="server" ></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle CssClass='DataGridRow' />
                                <ItemTemplate >
                                     <asp:Label ID="lblPAYMENT_DATE" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.PAYMENT_DATE")%>' ></asp:Label>
                                </ItemTemplate>
                           </asp:TemplateField>
                          <%-- till here --%>
                           <asp:TemplateField >
                                 <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="8%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                <HeaderTemplate >
                                    <asp:Label ID="lblRECEIPT" runat="server" ></asp:Label>
                                </HeaderTemplate>
                                 <ItemStyle CssClass='DataGridRow' />
                                <ItemTemplate  >
                                    <input id="hidINSTALLMENT_NO" type="hidden" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.INSTALLMENT_NO") %>'/>
                                    <input id="hidIS_APPROVE1" type="hidden" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.IS_APPROVE1") %>'/>
                                    <input id="hidDEPOSIT_ID" type="hidden" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.DEPOSIT_ID") %>'/>
                                    <input id="hidRECEIPT_NUM" type="hidden" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.RECEIPT_NUM") %>'/>
                                    <input id="hidCD_LINE_ITEM_ID" type="hidden" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.CD_LINE_ITEM_ID") %>'/>
                                    <input id="hidCUSTOMER_ID" type="hidden" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.CUSTOMER_ID") %>'/>
                                    <input id="hidPOLICY_ID" type="hidden" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.POLICY_ID") %>'/>
                                    <input id="hidPOLICY_VERSION_ID" type="hidden" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.POLICY_VERSION_ID") %>'/>
                                    <asp:HyperLink ID="hlGenerateReceipt"  NavigateUrl="#"  runat="server" Width="100"> </asp:HyperLink>
                                </ItemTemplate>
                           </asp:TemplateField>
                           
                        </Columns>
                    </asp:GridView>
                    <cmsb:cmsbutton class="clsButton" id="btnIOF_REPORT" runat="server" 
                Text="" onclick="btnIOF_REPORT_Click"></cmsb:cmsbutton>
                    <cmsb:cmsbutton class="clsButton" id="btnViewAllReceipt" runat="server" Text="View All receipt" OnClientClick="javascript:return fnViewAllReceipt()"></cmsb:cmsbutton>
            
            <br />
              
  </td></tr>                 
  <tr>
  <td>
    <asp:GridView runat="server" id="grvIOFReport" 
          onrowdatabound="grvIOFReport_RowDataBound" >
    </asp:GridView>
  </td>
  </tr>             
  </table>
  <input id="hidDEPOSIT_ID" type="hidden" name="hidDEPOSIT_ID" onclick="return fnViewAllReceipt();" runat="server"/> <%--Changed by Aditya on 20-09-2011 for Bug # 47--%>

    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RIBreakdown.aspx.cs" Inherits="Cms.Reports.Aspx.RIBreakdown" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title id="hdtitle" runat="server">RI COI Breakdown</title>
    <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
    </head>
<body scroll="yes" class="bodyBackGround" >
    <form id="form1" runat="server">
         <div>    
            <asp:Repeater ID="rptrein" runat="server" 
                 onitemdatabound="rptrein_ItemDataBound">
                    <HeaderTemplate>
                        <table align="center" border="1" cellspacing="0" class='tableWidth'> 
                        <tr> 
                        <td colspan="14" align="center" class="headereffectCenter">
                        <asp:Label ID="lblRI_BREAKDOWN" runat="server"></asp:Label>
                       <%-- <b>RI BREAKDOWN</b>--%>
                        </td>
                        </tr>                                
                            <tr>
                                <b>
                                <td class="midcolora">
                                       <asp:Label ID="lblPOLICY_DISP_VERSION" runat="server">Policy Display Version</asp:Label>
                                       <%--<B>Policy Number</B>--%>
                                    </td>
                                    <td class="midcolora">
                                       <asp:Label ID="lblPOLICY_NUMBER" runat="server"></asp:Label>
                                       <%--<B>Policy Number</B>--%>
                                    </td>
                                    <td class="midcolora">
                                    <asp:Label ID="lblREINSURER_NAME" runat="server"></asp:Label>
                                      <%-- <B>Reinsurer Name</B>--%>
                                    </td>
                                    <td class="midcolora">
                                    <asp:Label ID="lblCONTRACT_NUMBER" runat="server"></asp:Label>
                                        <%--<B>Contract Number</B>--%>
                                    </td>
                                    <td class="midcolora">
                                    <asp:Label ID="lblLAYER" runat="server"></asp:Label>
                                       <%-- <B>Layer</B>--%>
                                    </td>
                                    <td class="midcolora">
                                    <asp:Label ID="lblLAYER_AMOUNT" runat="server"></asp:Label>
                                      <%--  <B>Layer Amount</B>--%>
                                    </td>
                                    
                                    <td class="midcolora">
                                    <asp:Label ID="lblTIV" runat="server"></asp:Label>
                                       <%--<B>TIV</B>--%>
                                    </td>
                                    <td class="midcolora">
                                     <asp:Label ID="lblRISK_ID" runat="server"></asp:Label>
                                    <%--<B>Risk Id</B>--%>
                                    </td>
                                    <td class="midcolora">
                                     <asp:Label ID="lblTRAN_PREMIUM" runat="server"></asp:Label>
                                    <%--<B>Transaction Premium</B>--%>
                                    </td >
                                    <td class="midcolora">
                                      <asp:Label ID="lblRETENTION_PER" runat="server"></asp:Label>
                                       <%-- <B>Retention %</B>--%>
                                    </td>
                                     <td class="midcolora">
                                     <asp:Label ID="lblCOMM_PER" runat="server"></asp:Label>
                                    <%--<B>Comm %</B>--%>
                                    </td>
                                    
                                    <td class="midcolora">
                                     <asp:Label ID="lblREIN_PREMIUM" runat="server"></asp:Label>
                                   <%-- <B>Rein Premium</B>--%>
                                    </td>
                                    
                                    <td class="midcolora">
                                    <asp:Label ID="lblRATE" runat="server"></asp:Label>
                                  <%--  <B>COMM PERCENTAGE</B>--%>
                                    </td>
                                    
                                    <td class="midcolora">
                                    <asp:Label ID="lblCOMM_AMOUNT" runat="server"></asp:Label>
                                    <%--<B>Comm Amount</B>--%>
                                    </td>
                                   
                                    
                                   <%-- <td class="midcolora">
                                    <asp:Label ID="lblREIN_CEDED" runat="server"></asp:Label>
                                   <B>Rein Ceded%</B>
                                    </td>--%>
                                    
                                    
                                    
                                </b>

                            </tr>
                            
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="center" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.POLICY_DISP_VERSION")%>
                            </td>
                            <td class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.POLICY_NUMBER")%>
                            </td>
                            <td class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.REIN_COMAPANY_NAME")%>
                            </td>
                            <td align="right" class='DataGridRow'> 
                                <%#DataBinder.Eval(Container, "DataItem.CONTRACT")%>
                            </td>
                            <td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.LAYER")%>
                            </td>
                            <td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.TOTAL_INS_VALUE")%>
                            </td>
                            <td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.CESSION_AMOUNT_LAYER")%>
                            </td>
                            <td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.RISK_ID")%>
                            </td>
                             <td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.TRAN_PREMIUM")%>
                            </td>
                            <td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.RETENTION_PER")%>
                            </td>
                           
                           <td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.COMM_PERCENTAGE")%>
                            </td>
                            <td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.REIN_PREMIUM")%>
                            </td>
                            
                             <td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.CONTRACT_COMM_PERCENTAGE")%>
                            </td>
                            
                            <td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.COMM_AMOUNT")%>
                            </td>
                          
                         
                            
                            
                        </tr>  
                         
                    </ItemTemplate>   
                                             
                  </asp:Repeater>
             </div>
           
           <table><tr></tr></table>
           
           
             <div>
             
                  
             
                   <asp:Repeater ID="rptrein1" runat="server" onitemcommand="rptrein1_ItemCommand" 
                       onitemdatabound="rptrein1_ItemDataBound">
                    <HeaderTemplate>
                        <table align="center" border="1" cellspacing="0" class='tableWidth'> 
                        <tr> 
                        <td colspan="13" align="center" class="headereffectCenter">
                        <asp:Label ID="lblCOI_BREAKDOWN" runat="server"></asp:Label>
                        <%--<b>COI BREAKDOWN</b>--%>
                        </td>
                        </tr>                                
                            <tr>
                                <b>
                                     <td class="midcolora">
                                     <asp:Label ID="lblPOLICY_VERSION_ID" runat="server"></asp:Label>
                                      <%-- <B>Rein Company Code</B>--%>
                                      </td>
                                    <td class="midcolora">
                                     <asp:Label ID="lblREIN_COM_CODE" runat="server"></asp:Label>
                                      <%-- <B>Rein Company Code</B>--%>
                                      </td>
                                    <td class="midcolora">
                                     <asp:Label ID="lblREIN_COMPANY_NAME" runat="server"></asp:Label>
                                        <%--<B>Rein Company Name</B>--%>
                                    </td>
                                    <td class="midcolora">
                                     <asp:Label ID="lblPREMIUM" runat="server"></asp:Label>
                                       <%-- <B>Premium</B>--%>
                                    </td>
                                    <td class="midcolora">
                                     <asp:Label ID="lblINTEREST" runat="server"></asp:Label>
                                       <%-- <B>Interst</B>--%>
                                    </td>
                                    <td class="midcolora">
                                     <asp:Label ID="lblTOTAL_AMOUNT" runat="server"></asp:Label>
                                       <%-- <B>Total Amount</B>--%>
                                    </td>
                                    <td class="midcolora">
                                     <asp:Label ID="lblCOMM_AMOUNT" runat="server"></asp:Label>
                                   <%-- <B>Commission Amount</B>--%>
                                    </td  
                                    
                                </b>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                         <td align="center" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.POLICY_DISP_VERSION")%>
                            </td>
                            <td class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.REIN_COMAPANY_CODE")%>
                            </td>
                            <td align="right" class='DataGridRow'> 
                                <%#DataBinder.Eval(Container, "DataItem.REIN_COMAPANY_NAME")%>
                            </td>
                            <td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.INSTALLMENT_AMOUNT")%>
                            </td>
                            <td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.INTEREST_AMOUNT")%>
                            </td>
                            <td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.TOTAL")%>
                            </td>
                            <td align="right" class='DataGridRow'>
                                <%#DataBinder.Eval(Container, "DataItem.CO_COMM_AMT")%>
                            </td>   
                        </tr>
                       
                    </ItemTemplate>  
                                                 
                  </asp:Repeater>
            </div>                       
    </form>
</body>
</html>

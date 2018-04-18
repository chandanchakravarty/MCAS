<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolicyOpenItems.aspx.cs" Inherits="Cms.Account.Aspx.PolicyOpenItems" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
     
    	<title>Open Items</title>
	    <link href="/cms/cmsweb/css/css<%=base.GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script src="../../cmsweb/scripts/AJAXcommon.js"></script>
		
		<script language="javascript"  type="text/javascript">
		    
		    function FetchPolInfoXML(PolNum) {
		   	    var ParamArray = new Array();
		        obj1 = new Parameter('POLICY_NUMBER', PolNum);
		        ParamArray.push(obj1);
		        var objRequest = _CreateXMLHTTPObject();
		        var Action = 'AI_INFO';

		        //If else Condition Added For according to PRAVEEN KASANA mail.
		        if (document.getElementById('txtPOLICY_ID').value.length < 20) {
		            alert(document.getElementById('hidMess').value);
		            document.getElementById('txtPOLICY_ID').value = "";
		            return false;
		        }
		       
		    }
		 
		    
		</script>
	 
</head>
<body leftmargin="0" rightmargin="0" onload="ApplyColor();ChangeColor();top.topframe.main1.mousein = false;showScroll();findMouseIn();javascript:document.ACT_CUSTOMER_OPEN_ITEMS.txtPOLICY_ID.focus();" 
    MS_POSITIONING="GridLayout" onkeydown = "if(event.keyCode==13){ __doPostBack('btnFind',null); return false;}" >
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		    <form id="ACT_CUSTOMER_OPEN_ITEMS" method="post" runat="server">
			<div id="bodyHeight" class="pageContent"   >
				<table cellSpacing="0" cellPadding="0" align="center" border="0" width="100%" class="pageContent"   >
					<tr>
						<td>
							<table id="Table1" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
								<tr>
									<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
								</tr>
								<tr>
									<td class="pageheader" align="center" colSpan="4"></td>
								</tr>
								<tr align="center">
									<td  align="center" class="headereffectcenter" colSpan="4"><asp:Label ID="capHeaderLabel" runat="server" ></asp:Label></td>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="5">
									    <asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label>
									</td>
								</tr>
								<tr align="center">
									<td class="midcolorr" width="25%">
									    <asp:label id="capPOLICY_ID" runat="server"></asp:label><span class="mandatory">*</span>
									</td>
									<td align="left" class="midcolora" width="32%">
									<asp:textbox id="txtPOLICY_ID"  runat="server" size="30" maxlength="21" onchange="FetchPolInfoXML(this.value);" onkeyup="FetchPolInfoXML(this.value);"></asp:textbox>
									    <img id="imgSelect" style="CURSOR: hand" alt="" src="../../cmsweb/images/selecticon.gif" runat="server">
									    <br>
									    <%--<asp:regularexpressionvalidator id="revPOLICY_ID" Runat="server" ControlToValidate="txtPOLICY_ID" Display="Dynamic" ></asp:regularexpressionvalidator>--%>
									    <asp:requiredfieldvalidator id="rfvPOLICY_ID" runat="server" ControlToValidate="txtPOLICY_ID" Display="Dynamic"></asp:requiredfieldvalidator>
									</td>
								</tr>
								<tr>
									<td class="midcolorr" colSpan="4">
									    <cmsb:cmsbutton class="clsButton" id="btnFind" runat="server" 
                                            onclick="btnFind_Click"></cmsb:cmsbutton>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="pageHeader"></td>
					</tr>
				
					<tr class="headereffectWebGrid">
						<td align="center">
                            <asp:Label ID="capHeaderLabelOfCustomer" runat="server" ></asp:Label></td>
					</tr>
		
					<tr>
					<td>
					  
                        <asp:GridView id="GvCustomerOpenItem" runat="server"  Width="100%" AlternatingItemStyle-CssClass="alternatedatarow"
					            ItemStyle-CssClass="datarow" HeaderStyle-CssClass="headereffectCenter" AutoGenerateColumns="False"
								PagerStyle-HorizontalAlign="Center" PageSize="10" PagerStyle-CssClass="datarow" 
                            ShowFooter="True" AllowPaging="True" 
                            onpageindexchanging="GvCustomerOpenItem_PageIndexChanging" 
                            onrowdatabound="GvCustomerOpenItem_RowDataBound">
                             <HeaderStyle  CssClass="headereffectCenter" HorizontalAlign="Center"></HeaderStyle>
                               <RowStyle CssClass="midcolora" HorizontalAlign="Right"></RowStyle>
								<Columns>
								    <asp:TemplateField >
									    <HeaderStyle Width="7%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capCustITEM_TRAN_CODE_TYPE" ></asp:Label> 
									    </HeaderTemplate>
										<ItemTemplate>
											<asp:Label ID="lblITEM_TRAN_CODE_TYPE" Runat="server" text='<%#Eval("TRAN_CODE_TYPE")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									
									 <asp:TemplateField >
									    <HeaderStyle Width="7%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capCustPAYOR_TYPE" ></asp:Label> 
									    </HeaderTemplate>
										<ItemTemplate>
											<asp:Label ID="lblPAYOR_TYPE" Runat="server" text='<%#Eval("PAYOR_TYPE")%>'> 
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									
									 <asp:TemplateField >
									    <HeaderStyle Width="7%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capCustITEM_TRAN_CODE" ></asp:Label> 
									    </HeaderTemplate>
										<ItemTemplate>
											<asp:Label ID="lblITEM_TRAN_CODE" Runat="server" text='<%#Eval("ITEM_TRAN_CODE")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									
									<asp:TemplateField >
									    <HeaderStyle Width="5%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capCustCOMMISSION_TYPE" ></asp:Label> 
									    </HeaderTemplate>
										<ItemTemplate>
											<asp:Label ID="lblCOMMISSION_TYPE" Runat="server" text='<%#Eval("COMMISSION_TYPE")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									 <asp:TemplateField >
									    <HeaderStyle Width="7%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capCustTransDesc" ></asp:Label> 
									    </HeaderTemplate>
										<ItemTemplate>
											<asp:Label ID="lblPLAN_DESCRIPTION" Runat="server" text='<%#Eval("TRANS_DESC")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField >
										<HeaderStyle Width="6%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capCustDueDate" ></asp:Label> 
									    </HeaderTemplate>
									    
										<ItemTemplate>
											<asp:Label ID="lblDUE_DATE" Runat="server" text='<%#Eval("DUE_DATE")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField >
									    <HeaderStyle Width="8%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capCustAmountDue" ></asp:Label> 
									    </HeaderTemplate>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<%--<asp:Label ID="lblAMOUNT_DUE" Runat="server" text='<%# DataBinder.Eval(Container, "DataItem.TOTAL_DUE", "{0:,#,###}")%>'></asp:Label>--%>
					                    <asp:Label ID="lblAMOUNT_DUE" Runat="server" text='<%# Eval("TOTAL_DUE")%>'></asp:Label> 
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField >
									    <HeaderStyle Width="8%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capCustAmountPaid" ></asp:Label> 
									    </HeaderTemplate>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblAMOUNT_PAID" Runat="server" text='<%# DataBinder.Eval(Container, "DataItem.TOTAL_PAID", "{0:,#,###}")%>' ></asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField >
									    <HeaderStyle Width="8%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capCustCoApp" ></asp:Label> 
									    </HeaderTemplate>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblCustCO_APP" Runat="server" text='<%#Eval("CO_APPLICANT")%>' ></asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
								</Columns>

                                        <PagerStyle HorizontalAlign="Center" CssClass="datarow"></PagerStyle>

                                            <HeaderStyle CssClass="headereffectCenter"></HeaderStyle>
					</asp:GridView>   
                        
						
				    </td>
					</tr>
					<tr>
						<td class="midcolorr" align="left">
						</td>
					</tr>
					<tr>
						<td class="midcolorr" align="left">
						    &nbsp;</td>
					</tr>
						<tr class="headereffectWebGrid">
						<td align="center"><asp:Label ID="capHeaderLabelOfAgency" runat="server"></asp:Label></td>
					</tr>
					<tr>
					<td>
							<asp:GridView id="GvAgencyOpenItem" runat="server" Width="100%" AlternatingItemStyle-CssClass="alternatedatarow"
					            ItemStyle-CssClass="datarow" HeaderStyle-CssClass="headereffectCenter" AutoGenerateColumns="False"
								PagerStyle-HorizontalAlign="Center" PagerStyle-CssClass="datarow" AllowPaging="True" 
                                onpageindexchanging="GvAgencyOpenItem_PageIndexChanging" ShowFooter="True" 
                                onrowdatabound="GvAgencyOpenItem_RowDataBound">
                                  <HeaderStyle  CssClass="headereffectCenter" HorizontalAlign="Center"></HeaderStyle>
                               <RowStyle CssClass="midcolora" HorizontalAlign="Right"></RowStyle>
								<Columns>
									 <asp:TemplateField >
									    <HeaderStyle Width="6%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capAgencyAGENCY_NAME" ></asp:Label> 
									    </HeaderTemplate>
										<ItemTemplate>
											<asp:Label ID="lblAGENCY_NAME" Runat="server" text='<%#Eval("AGENCY_NAME")%>'>
											</asp:Label>
										</ItemTemplate>

                                        <HeaderStyle ForeColor="White" Width="7%"></HeaderStyle>
									</asp:TemplateField>
									<asp:TemplateField >
									    <HeaderStyle Width="7%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capAgencyCOMMISSION_TYPE" ></asp:Label> 
									    </HeaderTemplate>
										<ItemTemplate>
											<asp:Label ID="lblCOMMISSION_TYPE" Runat="server" text='<%#Eval("COMMISSION_TYPE")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField >
									    <HeaderStyle Width="7%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capAgencyTransDesc" ></asp:Label> 
									    </HeaderTemplate>
										<ItemTemplate>
											<asp:Label ID="lblPLAN_DESCRIPTION" Runat="server" text='<%#Eval("TRANS_DESC")%>'>
											</asp:Label>
										</ItemTemplate>

                                        <HeaderStyle ForeColor="White" Width="7%"></HeaderStyle>
									</asp:TemplateField>
									<asp:TemplateField >
									    <HeaderStyle Width="6%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capAgencyPostingDate" ></asp:Label> 
									    </HeaderTemplate>
										<ItemTemplate>
											<asp:Label ID="lblDUE_DATE" Runat="server" text='<%#Eval("POSTING_DATE")%>'>
											</asp:Label>
										</ItemTemplate>

                                            <HeaderStyle ForeColor="White" Width="6%"></HeaderStyle>
									</asp:TemplateField>
									<asp:TemplateField >
                                        <HeaderStyle ForeColor="White" Width="8%"></HeaderStyle>
                                        <HeaderTemplate >
									         <asp:Label runat="server" ID="capAgencyAmountDue" ></asp:Label> 
									    </HeaderTemplate>            
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblAMOUNT_DUE" Runat="server" text='<%# DataBinder.Eval(Container, "DataItem.TOTAL_DUE", "{0:,#,###}")%>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField >
                                        <HeaderStyle ForeColor="White" Width="8%"></HeaderStyle>
                                        <HeaderTemplate >
									         <asp:Label runat="server" ID="capAgencyAmountPaid" ></asp:Label> 
									    </HeaderTemplate>                

										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblAMOUNT_PAID" Runat="server" text='<%# DataBinder.Eval(Container, "DataItem.TOTAL_PAID", "{0:,#,###}")%>' > </asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
								 	<asp:TemplateField >
                                        <HeaderStyle ForeColor="White" Width="8%"></HeaderStyle>
                                        <HeaderTemplate >
									         <asp:Label runat="server" ID="capAgencyAgencyCommAmt" ></asp:Label> 
									    </HeaderTemplate>  
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblAGENCY_COMM_AMT" Runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.AGENCY_COMM_AMT", "{0:,#,###}")%>' ></asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
								    <asp:TemplateField >
                                        <HeaderStyle ForeColor="White" Width="8%"></HeaderStyle>
                                        <HeaderTemplate >
									         <asp:Label runat="server" ID="capAgencyGrossAmt" ></asp:Label> 
									    </HeaderTemplate>   
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblGROSS_AMOUNT" Runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.GROSS_AMOUNT", "{0:,#,###}")%>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField >
                                        <HeaderStyle ForeColor="White" Width="8%"></HeaderStyle>
                                        <HeaderTemplate >
									         <asp:Label runat="server" ID="capAgencyNePremium" ></asp:Label> 
									    </HeaderTemplate>   
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblNET_PREMIUM" Runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.NET_PREMIUM", "{0:,#,###}")%>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField >
									    <HeaderStyle Width="8%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capAgencyCoApp" ></asp:Label> 
									    </HeaderTemplate>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblAgencyCO_APP" Runat="server" text='<%#Eval("CO_APPLICANT")%>' ></asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									
								</Columns>

                                <PagerStyle HorizontalAlign="Center" CssClass="datarow"></PagerStyle>

                                <HeaderStyle CssClass="headereffectCenter"></HeaderStyle>
					</asp:GridView>				
										
						</td>
					</tr>
					
					
					 <tr>
						<td class="midcolorr" align="left">
						</td>
					</tr>
					<tr>
						<td class="midcolorr" align="left">
						    &nbsp;</td>
					</tr>
						<tr class="headereffectWebGrid">
						<td align="center"><asp:Label ID="capHeaderLabelOfCOI" runat="server"></asp:Label></td>
					</tr>
					<tr>
					<td>
							<asp:GridView id="GvCOIOpenItem" runat="server" Width="100%" AlternatingItemStyle-CssClass="alternatedatarow"
					            ItemStyle-CssClass="datarow" HeaderStyle-CssClass="headereffectCenter" AutoGenerateColumns="False"
								PagerStyle-HorizontalAlign="Center" PagerStyle-CssClass="datarow" AllowPaging="True" 
                                onpageindexchanging="GvCOIOpenItem_PageIndexChanging" ShowFooter="True" 
                                onrowdatabound="GvCOIOpenItem_RowDataBound">
                                  <HeaderStyle  CssClass="headereffectCenter" HorizontalAlign="Center"></HeaderStyle>
                               <RowStyle CssClass="midcolora" HorizontalAlign="Right"></RowStyle>
								<Columns>
									 <asp:TemplateField >
									    <HeaderStyle Width="6%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capCO_NAME" ></asp:Label> 
									    </HeaderTemplate>
										<ItemTemplate>
											<asp:Label ID="lblCO_NAME" Runat="server" text='<%#Eval("COMPANY_NAME")%>'>
											</asp:Label>
										</ItemTemplate>

                                        <HeaderStyle ForeColor="White" Width="7%"></HeaderStyle>
									</asp:TemplateField>
									<asp:TemplateField >
									    <HeaderStyle Width="7%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capCOCOMMISSION_TYPE" ></asp:Label> 
									    </HeaderTemplate>
										<ItemTemplate>
											<asp:Label ID="lblCOMMISSION_TYPE" Runat="server" text='<%#Eval("COMMISSION_TYPE")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField >
									    <HeaderStyle Width="7%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capCOTransDesc" ></asp:Label> 
									    </HeaderTemplate>
										<ItemTemplate>
											<asp:Label ID="lblPLAN_DESCRIPTION" Runat="server" text='<%#Eval("TRANS_DESC")%>'>
											</asp:Label>
										</ItemTemplate>

                                        <HeaderStyle ForeColor="White" Width="7%"></HeaderStyle>
									</asp:TemplateField>
									<asp:TemplateField >
									    <HeaderStyle Width="6%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capCOPostingDate" ></asp:Label> 
									    </HeaderTemplate>
										<ItemTemplate>
											<asp:Label ID="lblDUE_DATE" Runat="server" text='<%#Eval("POSTING_DATE")%>'>
											</asp:Label>
										</ItemTemplate>

                                            <HeaderStyle ForeColor="White" Width="6%"></HeaderStyle>
									</asp:TemplateField>
									<asp:TemplateField >
                                        <HeaderStyle ForeColor="White" Width="8%"></HeaderStyle>
                                        <HeaderTemplate >
									         <asp:Label runat="server" ID="capCOAmountDue" ></asp:Label> 
									    </HeaderTemplate>            
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblAMOUNT_DUE" Runat="server" text='<%# DataBinder.Eval(Container, "DataItem.TOTAL_DUE", "{0:,#,###}")%>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField >
                                        <HeaderStyle ForeColor="White" Width="8%"></HeaderStyle>
                                        <HeaderTemplate >
									         <asp:Label runat="server" ID="capCOAmountPaid" ></asp:Label> 
									    </HeaderTemplate>                

										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblAMOUNT_PAID" Runat="server" text='<%# DataBinder.Eval(Container, "DataItem.TOTAL_PAID", "{0:,#,###}")%>' > </asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
								 	<asp:TemplateField >
                                        <HeaderStyle ForeColor="White" Width="8%"></HeaderStyle>
                                        <HeaderTemplate >
									         <asp:Label runat="server" ID="capCOCommAmt" ></asp:Label> 
									    </HeaderTemplate>  
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblCO_COMM_AMT" Runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.AGENCY_COMM_AMT", "{0:,#,###}")%>' ></asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
								    <asp:TemplateField >
                                        <HeaderStyle ForeColor="White" Width="8%"></HeaderStyle>
                                        <HeaderTemplate >
									         <asp:Label runat="server" ID="capCOGrossAmt" ></asp:Label> 
									    </HeaderTemplate>   
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblGROSS_AMOUNT" Runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.GROSS_AMOUNT", "{0:,#,###}")%>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField >
                                        <HeaderStyle ForeColor="White" Width="8%"></HeaderStyle>
                                        <HeaderTemplate >
									         <asp:Label runat="server" ID="capCONePremium" ></asp:Label> 
									    </HeaderTemplate>   
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblNET_PREMIUM" Runat="server" text= '<%# DataBinder.Eval(Container, "DataItem.NET_PREMIUM", "{0:,#,###}")%>' >
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField >
									    <HeaderStyle Width="8%"  ForeColor="white" />
									    <HeaderTemplate >
									         <asp:Label runat="server" ID="capCOICoApp" ></asp:Label> 
									    </HeaderTemplate>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblCOICO_APP" Runat="server" text='<%#Eval("CO_APPLICANT")%>' ></asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									
								</Columns>

                                <PagerStyle HorizontalAlign="Center" CssClass="datarow"></PagerStyle>

                                <HeaderStyle CssClass="headereffectCenter"></HeaderStyle>
					</asp:GridView>				
										
						</td>
					</tr>
					<tr>
						<td class="midcolora">
							<table width="100%">
								<tr>
									<td class="midcolora">
										&nbsp;</td>
									<td class="midcolorr">
										&nbsp;</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td><webcontrol:footer id="pageFooter" runat="server"></webcontrol:footer></td>
						<input id="hidBalanceAmount" type="hidden" runat="server" NAME="hidBalanceAmount">
						<INPUT id="hidPolicyID" type="hidden" runat="server" NAME="hidPolicyID">
						<INPUT id="hidMess" type="hidden" runat="server" NAME="hidMess">
					</tr>
				</table>
			</div>
		</form>
	</body>
</html>

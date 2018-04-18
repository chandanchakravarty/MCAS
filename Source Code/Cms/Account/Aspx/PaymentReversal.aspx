<%@ Page language="c#" Codebehind="PaymentReversal.aspx.cs" AutoEventWireup="false" Inherits="Account.Aspx.PaymentReversal" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PaymentReversal</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=base.GetColorScheme()%>.css" type=text/css rel=stylesheet>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<!--<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">-->
		<STYLE>
			.hide { OVERFLOW: hidden; TOP: 5px }
			.show { OVERFLOW: hidden; TOP: 5px }
			#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">			

		function SetFocus()
		{
			document.getElementById("txtPOLICY_ID").focus();			
		}
		
		//Formats the amount and convert 111 into 1.11
		function FormatAmount(txtAmount)
		{
			if (txtAmount.value != "")
			{
				amt = txtAmount.value;
				
				amt = ReplaceAll(amt,".","");
				
				if (amt.length == 1)
					amt = amt + "0";
				
				if ( ! isNaN(amt))
				{
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					txtAmount.value = InsertDecimal(amt);
				}
			}
		}
		</script>
	</HEAD>
	<body leftmargin="0" rightmargin="0" onload="ApplyColor();ChangeColor();top.topframe.main1.mousein = false;findMouseIn();setfirstTime();SetFocus();"
		MS_POSITIONING="GridLayout">
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<form id="Form1" method="post" runat="server">
			<div class="pageContent" id="bodyHeight">
				<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
					<tr>
						<td>
							<TABLE id="Table1" cellSpacing="1" cellPadding="0" width="90%" align="center" border="0">
								<tr>
									<TD class="pageHeader" colSpan="2">Please note that all fields marked with * are 
										mandatory</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="center" colSpan="2"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPOLICY_ID" runat="server">Policy Number</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPOLICY_ID" runat="server" maxlength="8" size="11" ReadOnly="False"></asp:textbox><IMG id="imgSelect" style="CURSOR: hand" alt="" src="../../cmsweb/images/selecticon.gif"
											runat="server"><INPUT id="hidPolicyID" type="hidden" runat="server" NAME="hidPolicyID"><br>
										<asp:regularexpressionvalidator id="revPOLICY_ID" ControlToValidate="txtPOLICY_ID" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator><br>
										<asp:requiredfieldvalidator id="rfvPOLICY_ID" runat="server" ControlToValidate="txtPOLICY_ID" Display="Dynamic"
											ErrorMessage="Please enter Policy Number."></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<td class="midcolorr" colSpan="4"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Find"></cmsb:cmsbutton></td>
								</tr>
							</TABLE>
						</td>
					</tr>
					<tr>
						<td>
							<table cellSpacing="1" cellPadding="0" width="90%" align="center" border="0">
								<tr>
									<td><asp:datagrid id="grdPaymentReversal" runat="server" DataKeyField="IDEN_ROW_ID" Width="100%" AlternatingItemStyle-CssClass="alternatedatarow"
											ItemStyle-CssClass="datarow" HeaderStyle-CssClass="headereffectCenter" AutoGenerateColumns="False"
											AllowPaging="True" PageSize="15" PagerStyle-Mode="NextPrev" PagerStyle-HorizontalAlign="Center"
											PagerStyle-CssClass="datarow" PagerStyle-NextPageText="Next >>" PagerStyle-PrevPageText="<< Prev"
											OnPageIndexChanged="grdPaymentReversal_Paging" >
											<Columns>
												<asp:TemplateColumn HeaderStyle-Width="12%" HeaderText="Policy Number" SortExpression="POLICY_NUMBER"
													HeaderStyle-ForeColor="white">
													<ItemTemplate>
														<INPUT id=hidPOLICY_ID type=hidden 	value='<%# DataBinder.Eval(Container.DataItem,"POLICY_ID")%>' name=hidPOLICY_ID runat="server">
														<INPUT id=hidPOLICY_VER_ID type=hidden 	value='<%# DataBinder.Eval(Container.DataItem,"POLICY_VERSION_ID")%>' name=hidPOLICY_VER_ID runat="server">
														<INPUT id=hidCUSTOMER_ID type=hidden value='<%# DataBinder.Eval(Container.DataItem,"CUSTOMER_ID")%>' name=hidCUSTOMER_ID runat="server">
														<INPUT id="hidCD_LINE_ITEM_ID" type=hidden value='<%# DataBinder.Eval(Container.DataItem,"CD_LINE_ITEM_ID")%>'  name=CD_LINE_ITEM_ID runat="server">
														<INPUT id="hidDEPOSIT_NO" type=hidden value='<%# DataBinder.Eval(Container.DataItem,"DEPOSIT_NO")%>'  name=hidDEPOSIT_NO runat="server">
														<INPUT id="hidRECEIPT_AMOUNT" type=hidden value='<%# DataBinder.Eval(Container.DataItem,"RECEIPT_AMOUNT")%>'  name=hidRECEIPT_AMOUNT runat="server">
														<INPUT id="hidREVERSAL_DATE" type=hidden value='<%# DataBinder.Eval(Container.DataItem,"REVERSAL_DATE")%>'  name=hidREVERSAL_DATE runat="server">
														<INPUT id=hidIDEN_ROW_ID type=hidden value='<%# DataBinder.Eval(Container.DataItem,"IDEN_ROW_ID")%>' name=hidIDEN_ROW_ID runat="server">
														<INPUT id=hidRECEIPT_MODE type=hidden value='<%# DataBinder.Eval(Container.DataItem,"RECEIPT_MODE")%>' name=hidIDEN_ROW_ID runat="server">
														<asp:Label ID="lblcustomerName_PolicyNo" Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"POLICY_NUMBER")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderStyle-Width="12%" HeaderText="Deposit Number" SortExpression="DEPOSIT_NUMBER"
													HeaderStyle-ForeColor="white">
													<ItemTemplate>
														<asp:Label ID="lblDepositNo" Runat="server" text='<%#DataBinder.Eval(Container.DataItem,"DEPOSIT_NO")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderStyle-Width="12%" HeaderText="Payment Date" SortExpression="PAYMENT_DATE"
													HeaderStyle-ForeColor="white">
													<ItemTemplate>
														<asp:Label ID="lblPaymentDate" Runat="server" text='<%#DataBinder.Eval(Container.DataItem,"PAYMENT_DATE")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderStyle-Width="15%" HeaderText="Payment Mode" SortExpression="PAYMENT_MODE"
													HeaderStyle-ForeColor="white">
													<ItemTemplate>
														<asp:Label ID="lblPaymentMode" Runat="server" text='<%#DataBinder.Eval(Container.DataItem,"PAYMENT_MODE")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderStyle-Width="15%" HeaderText="Payment Amount" SortExpression="PAYMENT_AMOUNT"
													HeaderStyle-ForeColor="white">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<asp:Label ID="lblPAYMENTAMOUNT" CssClass="INPUTCURRENCY" Runat="server" text='<%#String.Format("{0:c}",DataBinder.Eval(Container.DataItem,"PAYMENT_AMOUNT"))%>' onblur="FormatAmount(this);">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderStyle-Width="5%" HeaderText="Reversed" SortExpression="REVERSED" HeaderStyle-ForeColor="white">
													<ItemTemplate>
														<asp:Label ID="lblREVERSED" Runat="server" text='<%#DataBinder.Eval(Container.DataItem,"REVERSED")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderStyle-Width="19%" HeaderText="Reversal Date" SortExpression="REVERSAL_DATE"
													HeaderStyle-ForeColor="white">
													<ItemTemplate>
														<asp:Label ID="lblREVERSEDDATE" Runat="server" text='<%#DataBinder.Eval(Container.DataItem,"REVERSAL_DATE")%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												
														<asp:TemplateColumn HeaderText="Return Funds" ItemStyle-Width="10%">
															<ItemTemplate>
																<asp:CheckBox id="chkSWEEP" runat="server"></asp:CheckBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														
												<asp:TemplateColumn HeaderStyle-Width="10%" HeaderText="Reverse" HeaderStyle-ForeColor="white">
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Button ID="btnReverseAmt" Runat="server" Text="Reverse" cssclass="clsButton" CommandName="ReverseAmount"></asp:Button>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</HTML>

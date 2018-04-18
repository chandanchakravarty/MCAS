<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="DepositOthers.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.DepositOthers" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>DepositOthers</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta http-equiv="Page-Enter" content="revealtrans(Duration=0.0)">
		<meta http-equiv="Page-Exit" content="revealtrans(duration=0.0)">
		<LINK href="../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script language="javascript">
		
		//Formats the amount and convert 111 into 1.11
		function FormatAmount(txtReceiptAmount)
		{
						
			if (txtReceiptAmount.value != "")
			{
				amt = txtReceiptAmount.value;
				
				amt = ReplaceAll(amt,".","");
				
				if (amt.length == 1)
					amt = amt + "0";
				
				if ( ! isNaN(amt))
				{
					
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					
					txtReceiptAmount.value = InsertDecimal(amt);
				}
			}
		}
		
		
		// Checking whether any record selected or not
		function DeleteRows()
		{
			//Checking whtether any row(s) is being selected or not
			for (ctr=1; ctr<=18; ctr++)
			{
				flag = 0;
				if(document.getElementById("dgDepositDetails__ctl" + (ctr + 1)+ "_chkDelete") != null)
				{
					if (document.getElementById("dgDepositDetails__ctl" + (ctr + 1)+ "_chkDelete").checked == true)
					{
						flag = 1;
						break;
					}
				}
			}
			
			if (flag == 0)
			{	
				// Not a single row is selected , hence returning false
				alert("Please select the row, which you want to delete.");
				return false;
			}
		}
		
	
		//This function checks the validation of controls
		function DoValidationCheck()
		{
			var Result = true;
			//calculating the total
			CalculateTotalAmount();
			
			if (parseFloat(ReplaceString(document.getElementById("lblTotalAmount").innerHTML,",","")) !=  parseFloat(ReplaceString(document.getElementById("txtTapeTotal").value,",","")))
			{
				alert("Tape total is not equal to total amount.");
				document.getElementById("txtTapeTotal").focus();
				return false;
			}
			return (Page_IsValid && Result)
		}
				
		//Calculates the total of receipts amount
		function CalculateTotalAmount()
		{
			var total = 0;
			var prefix;
			
			for (ctr=1; ctr<=20; ctr++)
			{
				rowNo = ctr + 1;
				prefix = "dgDepositDetails__ctl" + rowNo;
				
				ctrl = document.getElementById(prefix + "_txtRECEIPT_AMOUNT");
				
				if (ctrl != null)
				{
					amt = ReplaceAll(ctrl.value,",","");
					if (amt != "" && !isNaN(amt))
					{
						total = total + parseFloat(amt);
					}
				}
			}
			document.getElementById("lblTotalAmount").innerHTML = InsertDecimal(total.toFixed(2));
		}
		
		//This function will open the Disttribution window
		function OpenDistributeWindow(LineItemId, Amount,PaymentFrom)
		{
			if (LineItemId == null)
				return;
			if (PaymentFrom == null)
				return;
		 	var depNum = document.getElementById('lblDEPOSIT_NUM').innerText;
		 	var leftVal = 20;
		 	var topVal =  100;	
			mywindow = window.open("DistributeCashReceipt.aspx?GROUP_ID=" + LineItemId
				+ "&GROUP_TYPE=DEP&DISTRIBUTION_AMOUNT=" + Amount + "&DEPOSIT_NUM=" + depNum + "&SUB_CALLED_FROM=MISC" + "&PAYMENT_FROM=" + escape(PaymentFrom)
				,"DistributeDeposits","height=500, width=960,left="+leftVal+",top="+topVal+",status= no, resizable= yes, scrollbars=no, toolbar=no,location=no,menubar=no" );
		}
		
		//Resets the form to its original state
		function Reset()
		{
			DisableValidators();
			document.ACT_CURRENT_DEPOSIT_LINE_ITEMS.reset();
			return false;
		}
		//This function activates the first tab
		function BackClick()
		{
			this.parent.changeTab(0,0);
			return false;
		}
			 
		function SetParentElement()
		{
		
			var Deopsit_Id = document.getElementById('hidDEPOSIT_ID').value;
			//alert(window.parent.self.document.forms[0].hidSELECTED_DEP_ID.value);
			window.parent.self.document.forms[0].hidSELECTED_DEP_ID.value = Deopsit_Id ;
			//window.parent.self.document.forms[0].hidlocQueryStr.value=Deopsit_Id  + "&DEPOSIT_TYPE";
			//alert(window.parent.self.document.forms[0].hidSELECTED_DEP_ID.value);

		}

		</script>
</HEAD>
	<body leftmargin="0" rightmargin="0" MS_POSITIONING="GridLayout" onload="CalculateTotalAmount();SetParentElement();">
		<form id="ACT_CURRENT_DEPOSIT_LINE_ITEMS" method="post" runat="server">
			<table width="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td align="center" class="midcolorc"><asp:label id="lblMessage" runat="server" EnableViewState="False" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<tr>
					<td class="midcolora" colspan="2"><b><asp:Label ID="lblDepositNum" Runat="server">Deposit Number :</asp:Label></b>
					<b><asp:Label ID="lblDEPOSIT_NUM" Runat="server"></asp:Label></b></td>
				</tr>
				<tr>
					<td class="midcolora">
						<asp:datagrid id="dgDepositDetails" runat="server" Width="100%" 
                            AutoGenerateColumns="False" DataKeyField="CD_LINE_ITEM_ID">
							<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
							<ItemStyle CssClass="midcolora"></ItemStyle>
							<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
							<ItemStyle Height="4"></ItemStyle>
							<Columns>
								<asp:TemplateColumn ItemStyle-Width="1%" Visible="False">
									<ItemTemplate>
										<input type="hidden" id="hidCD_LINE_ITEM_ID" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "CD_LINE_ITEM_ID")%>' NAME="hidCD_LINE_ITEM_ID">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Delete" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<asp:CheckBox id="chkDelete" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Claim #" ItemStyle-Width="11%">
									<ItemTemplate>
										<%--Done for Itrack Issue 6065 on 7 July 09>
										<asp:TextBox ID="txtCLAIM_NUMBER" MaxLength="8" size="10" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CLAIM_NUMBER")%>'> --%>
										<asp:TextBox ID="txtCLAIM_NUMBER" MaxLength="9" size="11" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CLAIM_NUMBER")%>' >
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Receipt Amount" ItemStyle-Width="15%">
									<ItemTemplate>
										<asp:TextBox ID="txtRECEIPT_AMOUNT" MaxLength="11" CssClass="InputCurrency" Runat="server" size="12" Text='<%# FormatMoney(DataBinder.Eval(Container.DataItem, "RECEIPT_AMOUNT"))%>'>
										</asp:TextBox>
										<br>
										<asp:RegularExpressionValidator ID="revRECEIPT_AMOUNT" Runat="server" Display="Dynamic" ControlToValidate="txtRECEIPT_AMOUNT"></asp:RegularExpressionValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Payment From" ItemStyle-Width="36%">
									<ItemTemplate>
										<asp:TextBox ID="txtPAYMENT_FROM" Runat="server" MaxLength="50" size="50" Text='<%# DataBinder.Eval(Container.DataItem, "RECEIPT_FROM_NAME")%>'>
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Distribute" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="21%" ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<asp:HyperLink ID="hlkDistribute" runat="server">Distribute</asp:HyperLink>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Distribution Status" Visible="true">
									<ItemTemplate>
										<ASP:Label id="lblStatus" Runat="server" Visible="true" CssClass="errmsg" Text='<%# DataBinder.Eval(Container, "DataItem.DISTRIBUTION_STATUS") %>'>
										</ASP:Label>
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td class="midcolorc"><asp:label id="lblPagingLeft" runat="server"></asp:label><asp:label id="lblPageCurr" runat="server"></asp:label><asp:label id="lblPagingRight" runat="server"></asp:label><asp:label id="lblPageLast" runat="server"></asp:label>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%">
							<tr>
								<td class="midcolorr" width="18%">
									<asp:Label ID="lblMsg" CssClass="labelfont" Runat="server">Total Amount&nbsp;</asp:Label>
								</td>
								<td class="midcolora" width="32%">
									<asp:Label ID="lblTotalAmount" Runat="server" CssClass="LabelFont"></asp:Label>
								</td>
								<td class="midcolora" width="18%">
									<asp:Label ID="lblTapeTotal" CssClass="labelfont" Runat="server">Tape Total&nbsp;</asp:Label>
								</td>
								<td class="midcolora" width="32%">
									<asp:TextBox ID="txtTapeTotal" Runat="server" CssClass="INPUTCURRENCY"></asp:TextBox>
									<br>
									<asp:RegularExpressionValidator ID="rfvTapeTotal" ControlToValidate="txtTapeTotal" Display="Dynamic" ErrorMessage="Please enter a ValidationExpression amount"
										Runat="server"></asp:RegularExpressionValidator>
								</td>
							</tr>
							<tr>
								<td class="midcolora" colspan="2">
									<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>									
									<cmsb:cmsbutton class="clsButton" id="btnCompareTotal" runat="server" Text="Compare Total" Visible="false"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnPrevious" runat="server" Text="Previous"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnNext" runat="server" Text="Next"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Text="Back"></cmsb:cmsbutton>
								</td>
								<td class="midcolorr" colspan="2">
									
									<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save &amp; Confirm"></cmsb:cmsbutton>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidDEPOSIT_ID" type="hidden" value="0" name="hidDEPOSIT_ID" runat="server"><INPUT id="hidDEPOSIT_TYPE" type="hidden" name="hidDEPOSIT_TYPE" runat="server">
					</td>
				</tr>
			</table>
			<input type="hidden" id="hidGLAccountXML" runat="server" NAME="hidGLAccountXML">
			<input type="hidden" id="hidPOLICYINFO" runat="server" NAME="hidPOLICYINFO">
		</form>
	</body>
</HTML>

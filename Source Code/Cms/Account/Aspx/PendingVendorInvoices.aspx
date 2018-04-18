<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="PendingVendorInvoices.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.PendingVendorInvoices" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Pending Vendor Invoices</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script language="javascript">
		
		//Formats the amount and convert 111 into 1.11
		function FunClose()
		{
			//window.opener.__doPostBack('btnSave', null);
			var loc=window.opener.location.href;   
			window.opener.location=loc.toString();  
			window.close();
		}		
		
		function OnSaveFunction()
		{
			//window.opener.__doPostBack('btnSave', null);
			var loc=window.opener.location.href;   
			window.opener.location=loc.toString();  
			
		}		
		//change by uday
		function onAmountChange(RowNo)
		{
		   strBalance = "dgPendingInvoices__ctl" + RowNo + "_lblBalAmt";
		   strAmount =  "dgPendingInvoices__ctl" + RowNo + "_txtPayAmt";
		  // strBalance = "dgPendingInvoices__ctl" + RowNo + "_lblBalAmt";
		  // strAmount = "dgPendingInvoices__ctl" + RowNo + "_txtPayAmt";			
			strDue = "dgPendingInvoices__ctl" + RowNo + "_lblTotalDue";
			
			fillAmtOnTxtChng();
			if (document.getElementById(strBalance) == null)
			{
				return;
			}
			
			if (document.getElementById(strAmount) == null)
			{
				return;
			}
			FormatAmount(document.getElementById(strAmount));
			
			amount = document.getElementById(strAmount).value.trim();
			if (amount == "" || isNaN(amount))
			{
				document.getElementById(strBalance).innerHTML = document.getElementById(strDue).innerHTML;
			}
			else
			{
				document.getElementById(strBalance).innerHTML = parseFloat(document.getElementById(strDue).innerHTML) - parseFloat(amount);
			}
			
		
			document.getElementById(strBalance).innerHTML = (parseFloat(document.getElementById(strBalance).innerHTML).toFixed(2));
			CalculateTotalAmount();
			
			} 
		//

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
	
		function ComparePayAndBalAmt(RowNo)
		{
				
				strBalance = "dgPendingInvoices__ctl" + RowNo + "_lblBalAmt";
				strAmount = "dgPendingInvoices__ctl" + RowNo + "_txtPayAmt";
				/*if(document.getElementById(objlblBalAmtClientID)==null || 
					document.getElementById(strAmount).value.trim()==null) return true;*/
				if (document.getElementById(strBalance) == null)
				{
					return;
				}
				if (document.getElementById(strAmount) == null)
				{
					return;
				}	
				FormatAmount(document.getElementById(strAmount));
				txtValue = document.getElementById(strAmount).value.trim();
				AmountValue = document.getElementById(strBalance).innerHTML;	
				txtValue = ReplaceString(txtValue, ',', '');
				if(AmountValue!="" && !isNaN(AmountValue) && txtValue!="" && !isNaN(txtValue) && parseFloat(txtValue)>parseFloat(AmountValue))
				{
					alert("Pay Amount cannot be greater than the Balance");
					document.getElementById(strAmount).value = "";
					return false;
				}
				else
					return true;
			
				
							
		}
		//Calculates the total balance amount
		//
		function CalculateTotalAmount()
		{
			var total = 0;
			var prefix;
			
			for (ctr=1; ctr<=20; ctr++)
			{
				rowNo = ctr + 1;
				prefix = "dgPendingInvoices__ctl" + rowNo;
				
				ctrl = document.getElementById(prefix + "_txtPayAmt");
				
				if (ctrl != null)
				{
					amt = ReplaceAll(ctrl.value,",","");
					
					if (amt != "" && !isNaN(amt))
					{
						total = total + parseFloat(amt);
					}
				}
			}		
			document.getElementById("lblTotalDue").innerHTML = InsertDecimal(total.toFixed(2));			
		}
		
		//
		function CalculateTotalBal()
		{	
		
			var flag = true;
			var ctr = 1;
			var total = 0;
			
			while (flag == true)
			{
				rowNo = ctr + 1;
				prefix = "dgPendingInvoices__ctl" + rowNo;
				ctrl = document.getElementById(prefix + "_txtPayAmt");
				
				if (ctrl != null)
				{
					amt = ReplaceAll(ctrl.value,",","");
								
					if (amt != "" && !isNaN(amt))
					{
						total = total + parseFloat(amt);
					}					
				}
				else
				{
					flag = false;
				}
				ctr++;
			}
			document.getElementById("lblTotalDue").innerHTML = (total.toFixed(2));
			
			
		}
		
		//changes made by uday on 20th nov
		function refreshParent()
		{
			//var loc=window.opener.location.href;   
			//window.opener.location=loc.toString();  
			window.opener.__doPostBack('btnFind', null);
		}
		
		function fillAmtOnChkChng(ctr,Amt)
		{
			var selChkID  = "dgPendingInvoices__ctl" + ctr + "_chkSelect";
			var objChk    = document.getElementById(selChkID);
			var txtAmt = "dgPendingInvoices__ctl" + ctr + "_txtPayAmt";
					
			if(objChk.checked)
			{
				var total = 0;
				if(document.getElementById(txtAmt).value=="")
				{
					document.getElementById(txtAmt).value = Amt;
				}	
				/*if(document.getElementById("lblTotalAmount").innerHTML != "")
				{
					total = total + parseFloat(document.getElementById("lblTotalAmount").innerHTML);					
				}
				if (document.getElementById(txtAmt).value.trim() != "" && ! isNaN(document.getElementById(txtAmt).value.trim()))
				{
					total = total + parseFloat(document.getElementById(txtAmt).value.trim());
					tempTotal = total;
				}*/
			}
			else
			{
			
				/*tempTotal = document.getElementById("lblTotalAmount").innerHTML;
				if (document.getElementById(txtAmt).value.trim() != " ")
				{
					tempTotal = tempTotal - parseFloat(document.getElementById(txtAmt).value.trim());
					document.getElementById(txtAmt).value = "";
				}
				else
				{*/
					document.getElementById(txtAmt).value = "";
				//}
			}
			fillAmtOnTxtChng();	
			//document.getElementById("lblTotalAmount").innerHTML = (tempTotal.toFixed(2));
		}
		
		function fillAmtOnTxtChng()
		{
			
			var intRowCount =document.getElementById('hidRowCount').value;
			//intRowCount = intRowCount + 2;
			 var total = 0;
			 var tempTotal=0;
			 var count = 0;
			 var ctr =2;
			 var amt = new String();
			for (count = 0;count<intRowCount;count++)
			{
				txtAmt = "dgPendingInvoices__ctl" + ctr + "_txtPayAmt";
				
				if(document.getElementById(txtAmt).value != "")
				{
					amt = document.getElementById(txtAmt).value;
					amt = amt.replace(",","");
					total = parseFloat(amt); // + parseFloat(document.getElementById("lblTotalAmount").innerHTML);
					tempTotal = (tempTotal + total);
				}
				ctr++;
			}
		    document.getElementById("lblTotalDue").innerHTML = (total.toFixed(2));
		//	document.getElementById("lblTotAmt").innerHTML = tempTotal.toFixed(2);
			
		}
		//
		
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout" onload="CalculateTotalBal();CalculateTotalAmount();">
		<form id="form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td>
						<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
						<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
					</td>
				</tr>
				<tr>
					<TD class="headereffectCenter" colSpan="2">Pending Vendor Invoices</TD>
				</tr>
				<tr>
					<TD class="pageHeader">Please note that all operations are performed on selected 
						checks only.</TD>
				</tr>
				<tr>
					<td align="center"><asp:label id="lblMessage" runat="server" EnableViewState="False" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				
				<tr>
					<td class="midcolora"><asp:datagrid id="dgPendingInvoices" runat="server" ItemStyle-Height="5" DataKeyField="IDEN_ROW_ID"
							Width="100%" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
							<ItemStyle CssClass="midcolora"></ItemStyle>
							<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
							<ItemStyle Height="4"></ItemStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Select" ItemStyle-Width="5%">
									<ItemTemplate>
										<asp:CheckBox id="chkSelect" runat="server"></asp:CheckBox>
										<input type="hidden" id ="hidSELECTED" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "SELECTED")%>'>
										<input type="hidden" id ="hidREF_INVOICE_ID" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "INVOICE_ID")%>'>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Invoice #" ItemStyle-Width="11%">
									<ItemTemplate>
										<asp:Label ID="lblInvoiceNum" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INVOICE_NUM")%>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Ref #" ItemStyle-Width="11%">
									<ItemTemplate>
										<asp:Label ID="lblRefNum" Runat="server" text='<%# DataBinder.Eval(Container.DataItem, "REF_NUM")%>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Total Amount" ItemStyle-Width="11%"  ItemStyle-CssClass="INPUTCURRENCY">
									<ItemTemplate>
										<asp:Label ID="lblTotAmt" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TOTAL_DUE")%>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Paid Amount" ItemStyle-Width="11%" ItemStyle-CssClass="INPUTCURRENCY">
									<ItemTemplate>
										<asp:Label ID="lblPaidAmt" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TOTAL_PAID")%>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Balance Amount" ItemStyle-Width="11%"  ItemStyle-CssClass="INPUTCURRENCY">
									<ItemTemplate>
										<asp:Label ID="lblBalAmt" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BALANCE")%>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pay Amount" ItemStyle-Width="11%">
									<ItemTemplate>
										<asp:TextBox ID="txtPayAmt" Runat="server" onBlur="FormatAmount(this)" class="INPUTCURRENCY" Text='<%# DataBinder.Eval(Container.DataItem, "AMOUNT_TO_APPLY")%>'>
										</asp:TextBox>
										<asp:RegularExpressionValidator ID="revPayAmt" Runat="server" ControlToValidate="txtPayAmt" Display="Dynamic" ErrorMessage="expression"></asp:RegularExpressionValidator>
										<asp:CustomValidator ID="csvPayAmt" Runat="server" ControlToValidate="txtPayAmt" Display="Dynamic"></asp:CustomValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="1%" Visible="False">
									<ItemTemplate>
										<asp:Label ID="lblOPEN_ITEM_ID" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OPEN_ITEM_ID")%>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				
				<tr>
					<td class="midcolorr">
						Total:<asp:label id="lblTotalDue" runat="server"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
				<tr>
					<td class="midcolora" width="100%">&nbsp;</td>
				</tr>
				<tr>
					<td class="midcolorr">
						<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
						<cmsb:cmsbutton class="clsButton" id="btnClose" runat="server" Text="Close"></cmsb:cmsbutton>
						<cmsb:cmsbutton class="clsButton" id="btnSaveClose" runat="server" Text="Save & Close"></cmsb:cmsbutton>
					</td>
				</tr>
				<tr>
					<td><INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
					    <input id="hidRowCount" type="hidden" runat="server" NAME="hidRowCount">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

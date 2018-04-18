<%@ Page  validateRequest=false language="c#" Codebehind="ItemsToReconcile.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.ItemsToReconcile" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>BRICS-<%=Request.QueryString["Mode"]==null?"":"Add "%>
			Items to Reconcile</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=base.GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		var SelectedCheckBoxes=0;
		function CheckBoxClicked(objCheckBox)
		{
			if(objCheckBox.checked)
				SelectedCheckBoxes++;
			else
				SelectedCheckBoxes--;
			if(SelectedCheckBoxes>0)
			{
				if(document.getElementById('btnRemove')!=null)
					document.getElementById('btnRemove').setAttribute('disabled',false);
				if(document.getElementById('btnSave')!=null)
					document.getElementById('btnSave').setAttribute('disabled',false);
			}
			else
			{
				if(document.getElementById('btnRemove')!=null)
					document.getElementById('btnRemove').setAttribute('disabled',true);
				if(document.getElementById('btnSave')!=null)
					document.getElementById('btnSave').setAttribute('disabled',true);
			}
		}
		function OpenAddItemsTobeReconcilied()
		{
			var acID = '<%=Request.QueryString["ACCOUNT_ID"]%>';
			var ReconId = '<%=Request.QueryString["AC_RECONCILIATION_ID"]%>';		
			var url="ItemsToReconcile.aspx?AC_RECONCILIATION_ID="+ReconId+"&ACCOUNT_ID="+acID+"&Mode=Add";	
			ShowPopup(url,'AddItemsToReconcile',850,550);	
			return false;
		}
			//Refresh the parent page:
		function refreshParent()
		{
		
			var loc="";
			var strQueryString = new String(window.opener.location.href);
			
				var reconID = window.opener.document.getElementById('hidAC_RECONCILIATION_ID').value;		
				
				// Check incorporated coz' at the time of a new check creation, loc did not contain reconID Id thereby 
				// opening the parent window in 'Add' Mode.
				if(strQueryString.indexOf('AC_RECONCILIATION_ID')==-1)
					loc=window.opener.location.href + "&AC_RECONCILIATION_ID=" + reconID + "&";  
				else
					loc=window.opener.location.href;
			
				
			window.opener.location=loc.toString();  
			
		}
			var CheckVisible = 1;
			var DepositVisible = 1;
			var JEVisible = 1;
			var InvoiceVisible = 1;
			
			function ShowHideChecks()
			{	
			
				var total_checks = 0;
				if(document.getElementById('hidTOTAL_CHECKS').value != ""
					 || document.getElementById('hidTOTAL_CHECKS').value != "0")
				{
					total_checks= parseInt(document.getElementById('hidTOTAL_CHECKS').value);
				}
				if(CheckVisible == 1)
				{
					for(i=2;i<=total_checks+1;i++)
					{
						if(document.getElementById("grdReconcileItems__trCheckDetail" + i)!=null)
						{
							document.getElementById("grdReconcileItems__trCheckDetail" + i).style.display =  'none'; 
							ExpandCollapse("grdReconcileItems__trCheckHead","none");
						}
					}
					
					CheckVisible= 0;
				}
				else
				{
					for(i=2;i<=total_checks+1;i++)
					{
						if(document.getElementById("grdReconcileItems__trCheckDetail" + i)!=null)
						{
							document.getElementById("grdReconcileItems__trCheckDetail" + i).style.display =  'inline'; 
							ExpandCollapse("grdReconcileItems__trCheckHead","inline");
						}
					}
					CheckVisible= 1;
				}
			}
			function ShowHideDeposits()
			{
			
			
				var total_deposits = 0;
				if(document.getElementById('hidTOTAL_DEPOSITS').value != ""
					 || document.getElementById('hidTOTAL_DEPOSITS').value != "0")
				{
					total_deposits= parseInt(document.getElementById('hidTOTAL_DEPOSITS').value);
				}
				if(DepositVisible == 1)
				{
					for(i=2;i<=total_deposits+1;i++)
					{
						if(document.getElementById("grdReconcileItems__trDepositDetail" + i)!=null)
						{
							document.getElementById("grdReconcileItems__trDepositDetail" + i).style.display =  'none'; 
							ExpandCollapse("grdReconcileItems__trDepositHead","none");
						}	
					}
					
					DepositVisible= 0;
				}
				else
				{
					for(i=2;i<=total_deposits+1;i++)
					{
						if(document.getElementById("grdReconcileItems__trDepositDetail" + i)!=null)
						{
							document.getElementById("grdReconcileItems__trDepositDetail" + i).style.display =  'inline'; 
							ExpandCollapse("grdReconcileItems__trDepositHead","inline");
						}	
					}
					DepositVisible= 1;
				}
			}
			
			function ShowHideJEs()
			{
			
				var total_je = 0;
				if(document.getElementById('hidTOTAL_JE').value != ""
					 || document.getElementById('hidTOTAL_JE').value != "0")
				{
					total_je= parseInt(document.getElementById('hidTOTAL_JE').value);
					
				}
				if(JEVisible == 1)
				{
					for(i=2;i<=total_je+1;i++)
					{
						if(document.getElementById("grdReconcileItems__trJEDetail" + i)!=null)
						{
							document.getElementById("grdReconcileItems__trJEDetail" + i).style.display =  'none'; 
							ExpandCollapse("grdReconcileItems__trJEHead","none");
						}	
					}
					
					JEVisible= 0;
				}
				else
				{
					for(i=2;i<=total_je+1;i++)
					{
						if(document.getElementById("grdReconcileItems__trJEDetail" + i)!=null)
						{
							document.getElementById("grdReconcileItems__trJEDetail" + i).style.display =  'inline'; 
							ExpandCollapse("grdReconcileItems__trJEHead","inline");
						}	
					}
					JEVisible= 1;
				}
			}
			
			function ShowHideInvoices()
			{
			
				var total_Invoice = 0;
				if(document.getElementById('hidTOTAL_INVOICE').value != ""
					 || document.getElementById('hidTOTAL_INVOICE').value != "0")
				{
					total_Invoice= parseInt(document.getElementById('hidTOTAL_INVOICE').value);
				}
				if(InvoiceVisible == 1)
				{
					for(i=2;i<=total_Invoice+1;i++)
					{
						if(document.getElementById("grdReconcileItems__trInvoiceDetail" + i)!=null)
						{
							document.getElementById("grdReconcileItems__trInvoiceDetail" + i).style.display =  'none'; 
							ExpandCollapse("grdReconcileItems__trINVOICEHead","none");
						}	
						
					}
					
					InvoiceVisible= 0;
				}
				else
				{
					for(i=1;i<=total_Invoice+1;i++)
					{
						if(document.getElementById("grdReconcileItems__trInvoiceDetail" + i)!=null)
						{
							document.getElementById("grdReconcileItems__trInvoiceDetail" + i).style.display =  'inline'; 
							ExpandCollapse("grdReconcileItems__trINVOICEHead","inline");
						}	
						
					}
					InvoiceVisible= 1;
				}
			}
			
			function ExpandDetailsOnLoad()
			{			
				ShowHideChecks();
				ShowHideDeposits();
				ShowHideInvoices();
				ShowHideJEs();					
			}
			
			function ExpandCollapse(grid,display)
			{
				var cntrl = grid + "_imgCollaspe";
				if(display == "none")
					document.getElementById(cntrl).src	=	'<%=pathPlus%>';
				else
					document.getElementById(cntrl).src	=	'<%=pathMinus%>';	
				
			}
			
				
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="ExpandDetailsOnLoad();">
		<form id="Form1" method="post" runat="server">
			<TABLE cellPadding="0" cellspacing="0" width="90%" align="center" border="0">
				<tr>
					<TD class="headereffectCenter" colSpan="5"><%=Request.QueryString["Mode"]==null?"":"Add "%>Bank 
						Reconciliation Item Details</TD>
				</tr>
				<tr>
					<TD class="midcolorc" width="100%" colSpan="5"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></TD>
				</tr>
				<tr>
					<TD class="midcolora" width="20%" colSpan="1">Account Number:
					</TD>
					<TD class="midcolorr" width="15%" colSpan="1"><asp:label id="lblAcc_Number" runat="server"></asp:label></TD>
					<td class="midcolora" width="10%">&nbsp;</td>
					<TD class="midcolorc" colSpan="2">Summary of transactions</TD>
				</tr>
				<tr>
					<TD class="midcolora" width="20%" colSpan="1">Starting Balance:
					</TD>
					<TD class="midcolorr" width="15%" colSpan="1"><asp:label id="lblStartBal" runat="server"></asp:label></TD>
					<td class="midcolora" width="10%">&nbsp;</td>
					<TD class="midcolora" width="20%" colSpan="1">Total Deposits:</TD>
					<TD class="midcolorr" width="15%" colSpan="1"><asp:label id="lblTotalDep" runat="server"></asp:label></TD>
				</tr>
				<tr>
					<TD class="midcolora" colSpan="1">Ending Balance(bank):
					</TD>
					<TD class="midcolorr" colSpan="1"><asp:label id="lblEndBal" runat="server"></asp:label></TD>
					<td class="midcolora" width="10%">&nbsp;</td>
					<TD class="midcolora" colSpan="1">Total Checks:</TD>
					<TD class="midcolorr" colSpan="1"><asp:label id="lblTotalChecks" runat="server"></asp:label></TD>
				</tr>
				<tr>
					<td class="midcolora" colSpan="1">Total Transactions:
					</td>
					<td class="midcolorr" colspan="1"><asp:Label ID="lblTotalTransactions" Runat="server"></asp:Label>
					</td>
					<td class="midcolora" width="10%">&nbsp;</td>
					<TD class="midcolora" colSpan="1">Total JE:</TD>
					<TD class="midcolorr" colSpan="1"><asp:label id="lblTotalJE" runat="server"></asp:label></TD>
				</tr>
				<tr>
					<td class="midcolora" colSpan="1">Bank Charges:
					</td>
					<td class="midcolorr" colspan="1"><asp:Label ID="lblBankCharges" Runat="server"></asp:Label>
					</td>
					<td class="midcolora" width="10%">&nbsp;</td>
					<TD class="midcolora" colSpan="1">Total Invoices:</TD>
					<TD class="midcolorr" colSpan="1"><asp:label id="lblTotalInvoices" runat="server"></asp:label></TD>
				</tr>
				<tr>
					<td class="midcolora" colSpan="1">Calculated Ending Balance:
					</td>
					<td class="midcolorr" colspan="1"><asp:Label ID="lblCalcEndBal" Runat="server"></asp:Label>
					</td>
					<td class="midcolora" width="10%">&nbsp;</td>
					<TD class="midcolora" colSpan="1">Total Other:</TD>
					<TD class="midcolorr" colSpan="1"><asp:label id="lblTotalOther" runat="server"></asp:label></TD>
				</tr>
				<tr>
					<TD class="midcolora" colSpan="1">Difference:
					</TD>
					<TD class="midcolorr" colSpan="1"><asp:label id="lblDiff" runat="server"></asp:label></TD>
					<TD class="midcolora" colSpan="3"></TD>
				</tr>
				<tr>
					<td colSpan="5" id="Grid">
						<asp:datagrid id="grdReconcileItems" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headereffectCenter"
							ItemStyle-CssClass="datarow" AlternatingItemStyle-CssClass="alternatedatarow" Width="100%"
							DataKeyField="IDENTITY_ROW_ID">
							<Columns>
								<asp:TemplateColumn HeaderText="Select">
									<ItemTemplate>
										<asp:CheckBox ID="chkSelect" name="chkSelect" Runat="server"></asp:CheckBox>
										<input type = "hidden" id="hidIS_ALREADY_CLEARED" name="hidIS_ALREADY_CLEARED" runat="server" value='<%# DataBinder.Eval(Container.DataItem,"IS_ALREADY_CLEARED")%>'>
										<asp:Image ID='imgCollaspe' Runat="server" ImageUrl="/cms/cmsweb/Images/plus2.gif" Visible="False"></asp:Image>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Transaction Date">
									<ItemTemplate>
										<asp:label ID="lblSOURCE_TRAN_DATE" Runat=server text='<%# DataBinder.Eval(Container.DataItem,"SOURCE_TRAN_DATE")%>'>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Source Number">
									<ItemTemplate>
										<asp:label ID="lblSOURCE_NUM" Runat=server text='<%# DataBinder.Eval(Container.DataItem,"SOURCE_NUM")%>'>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Transaction Amount" ItemStyle-CssClass="midcolorr">
									<ItemTemplate>
										<asp:label ID="lblTRANSACTION_AMOUNT" Runat=server text='<%# DataBinder.Eval(Container.DataItem,"TRANSACTION_AMOUNT")%>'>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Transaction Type">
									<ItemTemplate>
										<asp:label ID="lblTransactionType" Runat=server text='<%# DataBinder.Eval(Container.DataItem,"TransactionType")%>'>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Note/Memo">
									<ItemTemplate>
										<asp:label ID="lblNote" Runat=server text='<%# DataBinder.Eval(Container.DataItem,"NoteMemo")%>'>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Payment Mode">
									<ItemTemplate>
										<asp:label ID="lblPayMent" Runat=server text='<%# DataBinder.Eval(Container.DataItem,"PAYMENT_MODE")%>'>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td class="midcolora" colSpan="5">&nbsp;</td>
				</tr>
				<tr>
					<td class="midcolora" colSpan="4">
						<INPUT class="clsButton" onclick="window.close();" type="button" value="Close">
						<cmsb:cmsbutton class="clsButton" id="btnRemove" runat="server" Text="Remove Item" enabled="false"></cmsb:cmsbutton>
						<cmsb:cmsbutton class="clsButton" id="btnAddItem" runat="server" Text="Add Item"></cmsb:cmsbutton>
						<INPUT class="clsButton" onclick="window.print();" type="button" value="Print"></cmsb:cmsbutton></td> 
					<td class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
				</tr>
			</TABLE>
			<input id="hidTOTAL_DEPOSITS" type="hidden" name="hidTOTAL_DEPOSITS" runat="server">
			<input id="hidTOTAL_CHECKS" type="hidden" name="hidTOTAL_CHECKS" runat="server">
			<input type="hidden" id="hidTOTAL_JE" runat="server" NAME="hidTOTAL_JE"> <input type="hidden" id="hidTOTAL_INVOICE" runat="server" NAME="hidTOTAL_INVOICE">
		</form>
	</body>
</HTML>

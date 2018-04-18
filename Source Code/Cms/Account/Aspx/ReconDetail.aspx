<%@ Page language="c#" Codebehind="ReconDetail.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.ReconDetail" validateRequest="false"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Apply Open Items</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script language="javascript">
		
		//Resetting the total amount
		function Reset()
		{
			DisableValidators();
			document.RECON_DETAILS.reset();
			return false;
		}
		
		//COUNT NO OF CHECKBOXES CHECKED:
		var chkCount
		function CountCheckBoxes() 
		{
			chkCount = 0
			var checkBoxID = 'chkAPPLY_ALL';	
			re = new RegExp(':' + checkBoxID + '$')  //generated controlname starts with a colon
				
			for(i = 0; i < document.RECON_DETAILS.elements.length; i++) 
			{
				elm = document.RECON_DETAILS.elements[i]

				if (elm.type == 'checkbox') 
				{
					
					if (re.test(elm.name)) 
					if (elm.checked)
					{
					chkCount = chkCount + 1;
					}
				}
		   }
		}
		
		
		function Select()
		{
			CountCheckBoxes(); 
			if (chkCount == 0)
			{
				alert("Please Select atleast One Record. !")
				return false;
			}		
		}
		
		//Caluclates the total balance amount
		function OnAmountChange(ctrlAmount, RowIndex)
		{
		
			var prefix = "dgOpenItems__Ctl";
			
			ctrlDue = document.getElementById(prefix + RowIndex + "_lblDUE");
			ctrlBalance= document.getElementById(prefix + RowIndex + "_lblBALANCE");
			
		
			if (ctrlDue.innerHTML.trim() == "" || isNaN(ctrlDue.innerHTML))
				due = 0;
			else
				due = parseFloat(ctrlDue.innerHTML);
				
			if (ctrlAmount.value.trim() == "" || isNaN(ctrlAmount.value))
				amount = 0;
			else
				amount = parseFloat(ctrlAmount.value);
			
			//Calucalting the line items balance
			ctrlBalance.innerHTML = (due - amount).toFixed(2);
			
			var total = 0;
			ctrl = document.getElementById(prefix + RowIndex + "_txtRECON_AMOUNT");
			if (ctrl == null)
			{ 
				return false;;
			}
			else
			{
				if(ctrlBalance.innerHTML > 0)
				{
					if(ctrl.value < 0)
					{
						alert('Amount to Apply cannot be negative for this Balance.')
						ctrl.value = "";
						document.getElementById(prefix + RowIndex + "_chkAPPLY_ALL").checked = false;
						OnAmountChange(ctrl,RowIndex);
						return;
					}
				}
				else if(ctrlBalance.innerHTML < 0)
				{
					if(ctrl.value > 0)
					{
						alert('Amount to Apply cannot be Positive for this Balance.')
						ctrl.value = "";
						document.getElementById(prefix + RowIndex + "_chkAPPLY_ALL").checked = false;
						OnAmountChange(ctrl,RowIndex);
						return;
					}
				}
				
				else
					if (ctrl.value == "" || isNaN(ctrl.value))
						total = total + 0;
					else
						total = total + parseFloat(ctrl.value);
			}
		}
		
		//Calls when user check the Apply all checkbox
		function OnApplyAll(ctrl,rowIndex)
		{
			
			prefix = "dgOpenItems__Ctl";
			txtAmtCtrl = document.getElementById(prefix + rowIndex + "_txtRECON_AMOUNT");
			if (ctrl.checked == true)
			{
				txtAmtCtrl.value = formatAmount(document.getElementById(prefix + rowIndex + "_lblDUE").innerHTML);
				//Itrack #3831
				OnAmountChange(txtAmtCtrl,rowIndex);
			}
			else
			{
				txtAmtCtrl.value ="";
				OnAmountChange(txtAmtCtrl,rowIndex);
			}
		}
		
		//Function Set Amount at Load
		//Added  by praveen on 28 march 2008 Itrack #3831
		function SetReconAmount()
		{
		
			var flag = true;
			var ctr = 0;
			
			while (flag == true)
			{
				rowNo = ctr + 2;
				prefix = "dgOpenItems__ctl" + rowNo;
				chkCtrl = document.getElementById(prefix + "_chkAPPLY_ALL");
				if(chkCtrl !=null)
				{
					if(chkCtrl.checked)
					{ 
						if (document.getElementById(prefix + "_txtRECON_AMOUNT") != null)
						{
							if (document.getElementById(prefix + "_txtRECON_AMOUNT").value.trim() != "")
							{
								//amt = parseFloat(document.getElementById(prefix + "_txtRECON_AMOUNT").value);
								//alert(document.getElementById(prefix + "_txtRECON_AMOUNT").value);
								OnAmountChange(document.getElementById(prefix + "_txtRECON_AMOUNT"),rowNo);
							}
						}
					}
				}
				else
				{
					flag = false;					
				}
				ctr++;			
			}
			
			
						
		}
		
		/* UNNECESSARY CODE
		// Initilize the form and sets the default values
		function Init()
		{
			var totalRows = '<%=System.Configuration.ConfigurationSettings.AppSettings["CoverageRows"]%>';
			totalRows = parseInt(totalRows);
			for (ctr=1; ctr<=totalRows; ctr++)
			{
				if (document.getElementById("dgOpenItems__ctl" + (ctr + 1)+ "_txtRECON_AMOUNT") != null)
				{
					OnAmountChange(document.getElementById("dgOpenItems__ctl" + (ctr + 1)+ "_txtRECON_AMOUNT"), ctr + 1);
				}
			}
		}
		*/
		
		//function checks the descrepence in total reconciled amount and amount reconcile: PK
		function CheckTotalRconcileAmount()
		{
			var flag = true;
			var ctr = 0;
			
			var totalReconcileAmt = 0, positiveReconAmt = 0;
			var totalDueAmt = 0, amt = 0;
						
			while (flag == true)
			{
				rowNo = ctr + 2;
				prefix = "dgOpenItems__ctl" + rowNo;
				chkCtrl = document.getElementById(prefix + "_chkAPPLY_ALL");
				if(chkCtrl !=null)
				{
					if(chkCtrl.checked)
					{ 
						if (document.getElementById(prefix + "_txtRECON_AMOUNT") != null)
						{
							if (document.getElementById(prefix + "_txtRECON_AMOUNT").value.trim() != "")
							{
								//If the Amount is Formated ie. 1,40,200 then Replace commas
								amt = parseFloat(ReplaceAll(document.getElementById(prefix + "_txtRECON_AMOUNT").value,",",""));
																					
								if (amt > 0)
								{
									//Addint the positive amount total
									positiveReconAmt = positiveReconAmt + amt;
								}
							
								//Adding to total reconcile amount
								totalReconcileAmt = totalReconcileAmt + amt;
								
							}
						
							if (parseFloat(ReplaceAll(document.getElementById(prefix +  "_lblDUE").innerHTML,",","")) < 0)
							{
								totalDueAmt = totalDueAmt + (parseFloat(ReplaceAll(document.getElementById(prefix +  "_lblDUE").innerHTML,",","")) * -1);							    
							}
						}
					}	
				}
				else
				{
					flag = false;
				}
				ctr++;
			}
			
			if (totalReconcileAmt.toFixed(2) != 0)
			{
				alert("Please reconcile the amount properly. \nTotal of reconciled amount should be equal to zero.");
				return false;
			}//(Praveen)Modified 3 September 2009 
			else if(positiveReconAmt.toFixed(2) > totalDueAmt)
			{
				alert("Please reconcile the amount properly. \nTotal of reconcile amount is greater then the amount that can be reconcile.");
				return false;
			}
			
			
			
		}
		
		//Added by Praveen : for JS Error while pressing Enter
		function PressEnter()
		{
			if (Select()== false) return false;
		}
		
		//END
		
		//This function checks the descrepance in total reconcile amount and amount reconcile --NOT USED
		/*
		function CheckTotalRconcileAmount1()
		{
			var totalRows = '<%=System.Configuration.ConfigurationSettings.AppSettings["CoverageRows"]%>';
			var prefix = "dgOpenItems__ctl";
			totalRows = parseInt(totalRows);
			
			var totalReconcileAmt = 0, positiveReconAmt = 0;
			var totalDueAmt = 0;
			
			var amt = 0;
			for (ctr=1; ctr<=totalRows; ctr++)
			{
				if (document.getElementById(prefix + (ctr + 1)+ "_txtRECON_AMOUNT") != null)
				{
					if (document.getElementById(prefix + (ctr + 1)+ "_txtRECON_AMOUNT").value.trim() != "")
					{
						amt = parseFloat(document.getElementById(prefix + (ctr + 1)+ "_txtRECON_AMOUNT").value);
						
						if (amt > 0)
						{
							//Addint the positive amount total
							positiveReconAmt = positiveReconAmt + amt;
						}
						
						//Adding to total reconcile amount
						totalReconcileAmt = totalReconcileAmt + amt;
						//alert(totalReconcileAmt)
					}
					
					if (parseFloat(document.getElementById(prefix + (ctr + 1)+ "_lblDUE").innerHTML) < 0)
					{
						totalDueAmt = totalDueAmt + (parseFloat(document.getElementById(prefix + (ctr + 1)+ "_lblDUE").innerHTML) * -1);
						//alert(totalDueAmt)
					}
				}
			}
			
			if (totalReconcileAmt != 0)
			{
				alert("Please reconcile the amount properly. \nTotal of reconciled amount should be equal to zero.");
				return false;
				return true;
			}
			else if(positiveReconAmt > totalDueAmt)
			{
				alert("Please reconcile the amount properly. \nTotal of reconcile amount is greater then the amount that can be reconcile.");
				return false;
				
			}
		}
		*/
		
		//Checking whether the total reconcile amuont is greater then the balance amount
		function CheckTotalAmount(objSource, objArgs)
		{
			var ctrl = objSource.id.replace("csvRECON_AMOUNT","lblDUE");
			var val= (document.getElementById(objSource.controltovalidate));
			//alert(val.value); //obj
			//alert(parseFloat(val.value));
			if (parseFloat(document.getElementById(ctrl).innerHTML) < parseFloat(val))
				objArgs.IsValid = false;
			else
				objArgs.IsValid = true;
		}
		
	
		function FormatAmount(txtAppliedAmount)
		{
				
			if (txtAppliedAmount.value != "")
			{
				amt = txtAppliedAmount.value;
				
				amt = ReplaceAll(amt,".","");
				
				if (amt.length == 1)
					amt = amt + "0";
				
				if ( ! isNaN(amt))
				{
					
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					
					txtAppliedAmount.value = InsertDecimal(amt);
				}
			}
		}
		
		</script>
	</HEAD>
	<body leftMargin="0" onload="showScroll();setfirstTime();SetReconAmount();" rightMargin="0" MS_POSITIONING="GridLayout">
		<div style="HEIGHT: 100%">
			<form id="RECON_DETAILS" method="post" runat="server">
				<TABLE class="tablewidthHeader" align="center" border="0">
					<tr>
						<td align="center" colSpan="6"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False" EnableViewState="False"></asp:label></td>
					</tr>
					<tr>
						<td class="headereffectCenter" colSpan="6">Apply Open Items
						</td>
					</tr>
					<tr>
						<td class="midcolora" width="17%"><asp:checkbox id="chkApplyOldestItems" runat="server" style="DISPLAY:none" Text="Apply to oldest items"></asp:checkbox></td>
						<td class="midcolora" width="17%"><asp:checkbox id="Checkbox1" runat="server" style="DISPLAY:none" Text="Show History"></asp:checkbox></td>
						<td class="midcolorr" width="17%"><asp:label id="capTotalOwed" runat="server" text="Total Owed"></asp:label></td>
						<td class="midcolora" width="17%"><asp:label id="lblTotalOwed" CssClass="LabelFont" Runat="server"></asp:label></td>
						<td class="midcolorr" width="17%"></td>
						<td class="midcolorr" width="17%"></td>
					</tr>
					<tr>
	
						<td class="midcolora" colSpan="6">
					<div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: <%=gridCount%>px">
							<asp:datagrid id="dgOpenItems" runat="server" DataKeyField="IDEN_ROW_NO" AutoGenerateColumns="False" height="100%">
								<ALTERNATINGITEMSTYLE CssClass="midcolora"></ALTERNATINGITEMSTYLE>
								<ITEMSTYLE CssClass="midcolora"></ITEMSTYLE>
								<HEADERSTYLE CssClass="headereffectWebGrid"></HEADERSTYLE>
								<COLUMNS>
									<ASP:TEMPLATECOLUMN Visible="true">
										<ITEMTEMPLATE>
											<INPUT id="hidITEM_REF_ID" type="hidden" name="hidITEM_REF_ID" value='<%# DataBinder.Eval(Container.DataItem, "ITEM_REF_ID")%>' runat="server">
										</ITEMTEMPLATE>
									</ASP:TEMPLATECOLUMN>
									<ASP:TEMPLATECOLUMN HeaderText="Reconcile">
										<ITEMTEMPLATE>
											<ASP:CHECKBOX id="chkAPPLY_ALL" runat="server"></ASP:CHECKBOX>
										</ITEMTEMPLATE>
									</ASP:TEMPLATECOLUMN>
									<ASP:TEMPLATECOLUMN HeaderText="Amount To Apply" ItemStyle-Width="10%">
										<ITEMTEMPLATE>
											<ASP:TEXTBOX id="txtRECON_AMOUNT" size="12" CssClass="InputCurrency" text='<%# DataBinder.Eval(Container.DataItem, "RECON_AMOUNT")%>' Runat="server">
											</ASP:TEXTBOX>
											<ASP:REGULAREXPRESSIONVALIDATOR id="revRECON_AMOUNT" Runat="server" ControlToValidate="txtRECON_AMOUNT" Display="Dynamic"></ASP:REGULAREXPRESSIONVALIDATOR>
											<ASP:CustomValidator ID="csvRECON_AMOUNT" Runat="server" ControlToValidate="txtRECON_AMOUNT" Display="Dynamic" ClientValidationFunction="CheckTotalAmount" ErrorMessage="Amount can not be greater then net due amount."></ASP:CustomValidator>
										</ITEMTEMPLATE>
									</ASP:TEMPLATECOLUMN>
									<ASP:TEMPLATECOLUMN HeaderText="Policy #">
										<ITEMTEMPLATE>
											<ASP:LABEL id="lblPOLICY_NUMBER" Runat="server" text='<%# DataBinder.Eval(Container.DataItem, "POLICY_NUMBER")%>' >
											</ASP:LABEL>
										</ITEMTEMPLATE>
									</ASP:TEMPLATECOLUMN>
									<ASP:TEMPLATECOLUMN HeaderText="Item Type" Visible="False">
										<ITEMTEMPLATE>
											<ASP:LABEL id="lblITEM_TYPE" Runat="server" text='<%# DataBinder.Eval(Container.DataItem, "DISPLAY_UPDATED_FROM")%>' >
											</ASP:LABEL>
											<INPUT id="hidUPDATED_FROM" type="hidden" name="hidUPDATED_FROM" value='<%# DataBinder.Eval(Container.DataItem, "UPDATED_FROM")%>' runat="server">
										</ITEMTEMPLATE>
									</ASP:TEMPLATECOLUMN>
									<ASP:TEMPLATECOLUMN HeaderText="Source Number">
										<ITEMTEMPLATE>
											<ASP:LABEL id="SOURCE_NUMBER" text='<%# DataBinder.Eval(Container.DataItem, "SOURCE_NUM")%>' Runat="server">
											</ASP:LABEL>
										</ITEMTEMPLATE>
									</ASP:TEMPLATECOLUMN>
									<ASP:TEMPLATECOLUMN HeaderText="Transaction Code">
										<ITEMTEMPLATE>
											<ASP:LABEL id="lblTranCode" Runat="server" text='<%# DataBinder.Eval(Container.DataItem, "TRANSACTION_TYPE")%>'>
											</ASP:LABEL>
										</ITEMTEMPLATE>
									</ASP:TEMPLATECOLUMN>
									<ASP:TEMPLATECOLUMN HeaderText="Effective Date">
										<ITEMTEMPLATE>
											<ASP:LABEL id="txtEffectiveDate" text='<%# DataBinder.Eval(Container.DataItem, "SOURCE_EFF_DATE")%>' Runat="server">
											</ASP:LABEL>
										</ITEMTEMPLATE>
									</ASP:TEMPLATECOLUMN>
									<ASP:TEMPLATECOLUMN HeaderText="Transaction Date">
										<ITEMTEMPLATE>
											<ASP:LABEL id="lblTransactionDate" text='<%# DataBinder.Eval(Container.DataItem, "SOURCE_TRAN_DATE")%>' Runat="server">
											</ASP:LABEL>
										</ITEMTEMPLATE>
									</ASP:TEMPLATECOLUMN>
									<ASP:TEMPLATECOLUMN HeaderText="Total Due" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="8%" ItemStyle-CssClass="midcolorr" ItemStyle-Wrap="False">
										<ITEMTEMPLATE>
											<ASP:LABEL id="Label1" Width="10%" text=<%# DataBinder.Eval(Container.DataItem, "TOTAL_DUE")%> Runat="server">
											</ASP:LABEL>
										</ITEMTEMPLATE>
									</ASP:TEMPLATECOLUMN>
									<ASP:TEMPLATECOLUMN HeaderText="Net Due" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="8%" ItemStyle-CssClass="midcolorr" ItemStyle-Wrap="False">
										<ITEMTEMPLATE>
											<ASP:LABEL id="lblDUE" Width="10%" text='<%# DataBinder.Eval(Container.DataItem, "BALANCE")%>' Runat="server">
											</ASP:LABEL>
										</ITEMTEMPLATE>
									</ASP:TEMPLATECOLUMN>
									<ASP:TEMPLATECOLUMN HeaderText="Balance" HeaderStyle-HorizontalAlign="Right" ItemStyle-Width="15%" ItemStyle-CssClass="midcolorr" ItemStyle-Wrap="False">
										<ITEMTEMPLATE>
											<ASP:LABEL id="lblBALANCE" Width="10%" text='<%# DataBinder.Eval(Container.DataItem, "BALANCE")%>' Runat="server">
											</ASP:LABEL>
										</ITEMTEMPLATE>
									</ASP:TEMPLATECOLUMN>

								</COLUMNS>
							</asp:datagrid>
							</div>
						</td>
					</tr>
					<TR>
						<TD class="midcolora" colSpan="6"></TD>
					</TR>
					<tr>
						<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
						<td class="midcolorr" colSpan="3">
							<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton>
							<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
					</tr>
					<tr>
						<td><INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidDEPOSIT_ID" type="hidden" value="0" name="hidDEPOSIT_ID" runat="server">
						</td>
					</tr>
				</TABLE>
				<input id="hidENTITY_ID" type="hidden" name="hidENTITY_ID" runat="server"> <input id="hidENTITY_TYPE" type="hidden" name="hidENTITY_TYPE" runat="server">
				<input id="hidRECEIPT_AMOUNT" type="hidden" name="hidRECEIPT_AMOUNT" runat="server">
				<input id="hidPARENT_GROUP_ID" type="hidden" name="hidPARENT_GROUP_ID" runat="server">
			</form>
			</div>
	</body>
</HTML>

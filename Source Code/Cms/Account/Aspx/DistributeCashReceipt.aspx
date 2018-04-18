<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="DistributeCashReceipt.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.DistributeCashReceipt" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Brics-<%=EntityName%>  Distribution</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		/*function CheckAmount(objSource, objArgs)
		{
			if (objSource.controltovalidate != null )
			{
				alert("mohit");
				var ctrl = document.getElementById(objSource.controltovalidate);
				var amountCtrl = objSource.getAttribute("RowNo");
			
				amountCtrl = document.getElementById("dgrCashReceipt__ctl" + amountCtrl + "_txtAmount");
				
				if (amountCtrl.value != "")
				{
					objArgs.IsValid = true;
				}
				else
				{
					objArgs.IsValid = false;
				}
			}
			}*/	
			var distributedAmount=0.00;
			var Total = 0.0;
			var RemainingAmount=0.0;
			function GetTotalAmount()
			{
				Total = '<%=Request.QueryString["DISTRIBUTION_AMOUNT"]%>';
				while(Total.indexOf(",")>-1)
					Total = Total.replace(",","");
				Total = parseFloat(Total);	
				return Total;
			}
			function CalcDistributedAmount()
			{
				var TotalSum = '<%=TotalSum%>'
				distributedAmount=0.00;
				RemainingAmount=0.0;
				var i=0;
				while(document.getElementById('dgrCashReceipt__ctl' + i + '_txtAmount')==null)
					i++;
				for(;document.getElementById('dgrCashReceipt__ctl' + i + '_txtAmount')!=null;i++)
				{
					if((!isNaN(ReplaceAll(document.getElementById('dgrCashReceipt__ctl' + i + '_txtAmount').value,",",""))) && document.getElementById('dgrCashReceipt__ctl' + i + '_txtAmount').value != "")
					{
						distributedAmount += parseFloat(ReplaceAll(document.getElementById('dgrCashReceipt__ctl' + i + '_txtAmount').value,",",""));
						//alert('distributedAmount = ' +distributedAmount);
						//document.getElementById('dgrCashReceipt__ctl'+i+'_txtAmount').value = formatCurrency(document.getElementById('dgrCashReceipt__ctl'+i+'_txtAmount').value);
					}
				}
				distributedAmount = parseFloat(distributedAmount) + parseFloat(TotalSum)
				
				//RemainingAmount = Math.round(parseFloat(Total)-(distributedAmount));
				RemainingAmount = (Total)-(distributedAmount);
				//alert('RemainingAmount = ' +RemainingAmount);
				Distributed.innerHTML = "<b>" + InsertDecimal(distributedAmount.toFixed(2)) + "</b>";
				//Remaining.innerHTML = "<b>" + InsertDecimal(RemainingAmount.toFixed(2)) + "</b>";
				
				if(RemainingAmount.toFixed(2) == "-0.00")
				{
				   Remaining.innerHTML = "<b>" + InsertDecimal(RemainingAmount.toFixed(2)) + "</b>";
				}
				else
				{
					Remaining.innerHTML = "<b>" + InsertDecimal(RemainingAmount.toFixed(2)) + "</b>";
				}
			}
			function CalcAmount(percentageId)
			{
				
				var RowNum = percentageId.replace("dgrCashReceipt__ctl","");
				RowNum = RowNum.replace("_txtPercentage","");
				perc = parseFloat(document.getElementById(percentageId).value);
			
				if(!isNaN(perc))
				{
					if(perc!=0)
					{
						//document.getElementById(percentageId).value=formatCurrency(document.getElementById(percentageId).value);
						document.getElementById('dgrCashReceipt__ctl'+RowNum+'_txtAmount').value=InsertDecimal(((perc*Total)/100).toFixed(2));						
						
					}
					else
					{
						document.getElementById(percentageId).value="";
						document.getElementById('dgrCashReceipt__ctl'+RowNum+'_txtAmount').value="";		
					}
					
				}
				CalcDistributedAmount();
			}
			function CalcPercentage(amountId)
			{
				
				var RowNum = amountId.replace("dgrCashReceipt__ctl","");
				RowNum = RowNum.replace("_txtAmount","");
				amount = document.getElementById(amountId).value
				amount = ReplaceAll(amount.toString(),",","");
				if (amount != "" && ! isNaN(amount))
				{
					document.getElementById(amountId).value = InsertDecimal(amount);
				}
				amount = document.getElementById(amountId).value;
				amount = parseFloat(ReplaceAll(amount, ",", ""));
			
				if(!isNaN(amount))
				{
					
					if(amount != 0)
					{
					
						//document.getElementById(amountId).value=formatCurrency(document.getElementById(amountId).value);
						document.getElementById('dgrCashReceipt__ctl'+RowNum+'_txtPercentage').value = ((amount/Total)*100).toFixed(2);
							
					}
					else
					{
						document.getElementById(amountId).value="";
						document.getElementById('dgrCashReceipt__ctl'+RowNum+'_txtPercentage').value="";		
					}
					
				}
				CalcDistributedAmount();
			}
			function ValidateOnSave()
			{
				Page_ClientValidate();
					if((RemainingAmount.toFixed(2))<0)
				{
					alert("Amount distributed Can not be greater than Total Amount.");
					return false;
				}
			}
			
		// if no check box is selected then return false
		// Checking whether any record selected or not
		function DeleteRows()
		{
			for (ctr=1; ctr<=10; ctr++)
			{
				flag = 0;
				if(document.getElementById("dgrCashReceipt__ctl" + (ctr + 1)+ "_chkSelect") != null)
				{
					if (document.getElementById("dgrCashReceipt__ctl" + (ctr + 1)+ "_chkSelect").checked == true)
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
			
			var rowno = 0;
			var blnIsValid = true;
			for (ctr=1; ctr<=20; ctr++)
			{
				rowno = ctr + 1;
				var prefix = "dgrCashReceipt__ctl" + rowno;
				// get the value of account drop down
				
				var cmbAcc = document.getElementById(prefix + "_cmbAccount");				
				if (cmbAcc == null)
				break;
				
				var accCtrl= cmbAcc.options[cmbAcc.selectedIndex].value;
				if (accCtrl != null)
				{
					if (accCtrl != "")
					{
						//Checking whether the other controls r properly filled or not						
						// percentage
						var percentage = document.getElementById(prefix + "_txtPercentage");
						// amount
						var ctrAmount = document.getElementById(prefix + "_txtAmount");
						
						if (percentage.value.trim() == "")
						{
							document.getElementById(prefix + "_csvPercentage").style.display = "inline";
							blnIsValid = false;
						}
						
						//Checking for seconf policy number
						if (ctrAmount.value.trim() == "")
						{
							document.getElementById(prefix + "_csvAmount").style.display = "inline";
							blnIsValid = false;
						}		
			
					}
					else
					{						
						//Checking whether all other fields are empty or not
						var percentage = document.getElementById(prefix + "_txtpercentage");
						var ctrAmount = document.getElementById(prefix + "_txtAmount");					
						
						
						if (percentage.value.trim() != "")
						{
							blnIsValid = false;
							document.getElementById(prefix + "_csvAccount").style.display = "inline";

						}						
						//Checking for amount
						if (ctrAmount.value.trim() != "")
						{
							blnIsValid = false;
							document.getElementById(prefix + "_csvAccount").style.display = "inline";
						}
					}
				}
			}
			Page_ClientValidate();
			if((RemainingAmount.toFixed(2))<0)
			{
				alert("Amount distributed can not be greater than Total Amount.");
				return false;
			}	
			if((RemainingAmount.toFixed(2))!=0)
			{
				alert("Amount has not been fully distributed.");
				return true;
			}
			
			if (blnIsValid == false)
			{				
				return false;
			}		
		}
		
		//Validating the receipt amount(whther entered or not)
		function ValidateRECEIPT_AMOUNT(objSource, objArgs)
		{
			var ctrlAmt = objSource.id;
			ctrlAmt = document.getElementById(ctrlAmt);
			
			if (ctrlAmt.value == "")
			{
				//Validating only if amount id entered else declare as valid field
				objArgs.IsValid = true;
			}
			else
			{
				if (document.getElementById(objSource.controltovalidate).value == "")
				{
					//For empty field is invalid
					objArgs.IsValid = false;
				}
				else
				{
					//For empty field is valid
					objArgs.IsValid = true;
				}
			}
		}
		
		// validate percentage
		function ValidateAMOUNT_PERCENTAGE(objSource, objArgs)
		{
		
			var ctrlAmt = objSource.id;
			ctrlAmt = document.getElementById(ctrlAmt);						
			if (ctrlAmt.value == "")
			{
				//Validating only if amount id entered else declare as valid field
				objArgs.IsValid = true;
			}
			else
			{
				if (document.getElementById(objSource.controltovalidate).value == "")
				{
					//For empty field is invalid
					objArgs.IsValid = false;
				}
				else
				{
					//For empty field is valid
					objArgs.IsValid = true;
				}
			}	
		}				
		
		// validate percentage
		function ValidateAcount(objSource, objArgs)
		{					
			var ctrlAmt = objSource.id;
			ctrlAmt = document.getElementById(ctrlAmt);
			return false;						
			if (ctrlAmt.value == "")
			{
				//Validating only if amount id entered else declare as valid field
				objArgs.IsValid = true;
			}
			else
			{
				if (document.getElementById(objSource.controltovalidate).value == "")
				{
					//For empty field is invalid
					objArgs.IsValid = false;
				}
				else
				{
					//For empty field is valid
					objArgs.IsValid = true;
				}
			}	
		}
		// calling the Vendor Invoice page
	
	
		// set Account in combo on basis of account no entered in AccountTextbox
		function SelectComboOptionByPartOfText(txtAccountID,comboId)// Acc text, Acc ComboID
		{
			var cmbtext;
			var cmpCmbText;
			var searchresult=0;
			var accText = document.getElementById(txtAccountID).value;
			if(accText != "")
			{
				// loop through all the options of corresponding combo
				for(var j=1; j<document.getElementById(comboId).options.length; j++)
				{
					cmbtext= document.getElementById(comboId).options[j].text; // Combo text
					var strCmbText = new String();
					strCmbText = cmbtext;
					var cmbAccNo = strCmbText.substr(strCmbText.indexOf(': ') + 2,8);
					searchresult=cmbAccNo.indexOf(accText, 0) // search for Acc text in Combo Text
					if(searchresult != -1) // if true, set that combo text as selected
					{
						document.getElementById(comboId).options.selectedIndex = j;
						document.getElementById(txtAccountID).value = cmbAccNo;
						break;
					}
				}
			}
		}
		
		// Get IDs of Account text & combo box
		function searchAccount(txtAccountID,cmbAccountID)
		{
				// passing the Text of Account textbox and respective Account combobox ID
				SelectComboOptionByPartOfText(txtAccountID,cmbAccountID)
		}
		
		// fill Account Textbox on change of Account combo
		function fillAccount(txtAccountID,cmbAccountID)
		{
			var cmbText = document.getElementById(cmbAccountID).options[document.getElementById(cmbAccountID).selectedIndex].text;
			var strCmbText = new String();
			strCmbText = cmbText;
			var cmbAccNo = strCmbText.substr(strCmbText.indexOf(': ') + 2,8);
			document.getElementById(txtAccountID).value = cmbAccNo; // set combo acc no in account textbox
		}
		
		// fill Textbox with account num of all the temporary saved records.
		function fillAccNumTextBox(chkSelectID,txtAccountID,cmbAccountID)
		{
			for (ctr=1; ctr<=20; ctr++)
			{
				if(document.getElementById("dgrCashReceipt__ctl" + (ctr + 1)+ "_chkSelect") != null)
				{
					if (document.getElementById("dgrCashReceipt__ctl" + (ctr + 1)+ "_chkSelect").checked == true)
					{
						var txt = document.getElementById("dgrCashReceipt__ctl" + (ctr + 1)+ "_txtAccount");			
						var cmb = document.getElementById("dgrCashReceipt__ctl" + (ctr + 1)+ "_cmbAccount");	
						fillAccount(txt.id,cmb.id);
					}
				}
			}
		}
		
		//Refresh the parent page:
		function refreshParent()
		{
		
			var flag='<%=validDistribution%>';
			var loc="";
			var entityName = '<%=EntityName%>';
			
			var strQueryString = new String(window.opener.location.href);
			
			if(entityName == "Vendor")
			{
			
				var invID = window.opener.document.getElementById('hidINVOICE_ID').value;			
				// Check incorporated coz' at the time of a new check creation, loc did not contain Invoice Id thereby 
				// opening the parent window in 'Add' Mode.
				if(strQueryString.indexOf('INVOICE_ID')==-1)
					loc=window.opener.location.href + "&INVOICE_ID=" + invID + "&";  
				else
					loc=window.opener.location.href;
					
			}
			else if(entityName == "Bank")
			{
				var invID = window.opener.document.getElementById('hidAC_RECONCILIATION_ID').value;			
				// Check incorporated coz' at the time of a new check creation, loc did not contain Invoice Id thereby 
				// opening the parent window in 'Add' Mode.
				if(strQueryString.indexOf('AC_RECONCILIATION_ID')==-1)
					loc=window.opener.location.href + "&AC_RECONCILIATION_ID=" + invID + "&";  
				else
					loc=window.opener.location.href;
			}
			else
			{
			
				var chqID = window.opener.document.getElementById('hidCHECK_ID').value;			
				if(strQueryString.indexOf('CHECK_ID')==-1)
					loc=window.opener.location.href + "&CHECK_ID=" + chqID + "&";  
				else
					loc=window.opener.location.href;
			}
			window.opener.location=loc.toString();  
			window.close();
		}
		
		
		
		function FuncReset()
		{
			DisableValidators();
			document.ACT_DISTRIBUTION_DETAILS.reset();
			for (ctr=1; ctr<=20; ctr++)
			{
				if(document.getElementById("dgrCashReceipt__ctl" + (ctr + 1)+ "_chkSelect") != null)
				{
					if (document.getElementById("dgrCashReceipt__ctl" + (ctr + 1)+ "_chkSelect").checked == true)
					{
						var txt = document.getElementById("dgrCashReceipt__ctl" + (ctr + 1)+ "_txtAccount");			
						var cmb = document.getElementById("dgrCashReceipt__ctl" + (ctr + 1)+ "_cmbAccount");	
						fillAccount(txt.id,cmb.id);
					}
				}
			}
			ApplyColor();
			return false;
		}
		
		function Init()
		{
			var subCalledFrom = '<%=Request.QueryString["SUB_CALLED_FROM"]%>';
			if(subCalledFrom == "MISC")
				document.getElementById('tbMiscDep').style.display='inline';
			else
				document.getElementById('tbMiscDep').style.display='none';
				
			if(document.getElementById("hidCalledFrom").value=="VEN")
				document.getElementById("tbVendorInvoiceDetails").style.display="inline";
			else
				document.getElementById("tbVendorInvoiceDetails").style.display="none";
		}
	
		</script>
</HEAD>
	<body oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="Init();GetTotalAmount();CalcDistributedAmount();">
		<form id="ACT_DISTRIBUTION_DETAILS" method="post" runat="server">
			<webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer>
			<TABLE class="tablewidth" cellSpacing="0" cellPadding="0" align="center" border="0">
				<TBODY>
					<TR class="midcolora">
						<TD class="headereffectcenter" colSpan="2"><%=EntityName%>
							Distribution Information
						</TD>
					</TR>
					<TR>
						<td class="midcolorc" align="right" colSpan="2"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
					<TR>
					</TR>
					<tr>
						<td colSpan="2">
							<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tbody id="tbMiscDep">
								<tr>
									<td class="midcolora" width="35%">Deposit Number:
									</td>
									<td class="midcolora" width="15%"><b>
											<script>document.write('<%=Request.QueryString["DEPOSIT_NUM"]%>');</script>
										</b>
									</td>
									<td class="midcolorr"></td>
								</tr>
								<tr>
									<td class="midcolora" width="35%">Payment From:</td>
									<td class="midcolora" width="15%"><b><asp:Label ID="lblPaymentFrom" Runat="server"></asp:Label></b></td>
									<td class="midcolorr"></td>
								</tr>
							</tbody>
							<tbody id="tbVendorInvoiceDetails" style="DISPLAY:none">
								<tr>
									<td class="midcolora" width="35%">Vendor Name:
									</td>
									<td class="midcolora" colspan="2">
										<b>
											<asp:Label ID="lblVENDOR_NAME" Runat="server"></asp:Label>
										</b>										
									</td>		
									
														
								</tr>
								<tr>
									<td class="midcolora" width="35%">Invoice #:
									</td>									
									<td class="midcolora" colspan="2">
										<b>
											<asp:Label ID="lblINVOICE_NUMBER" Runat="server"></asp:Label>
										</b>										
									</td>												
								</tr>
							</tbody>
								<tr>
									<td class="midcolora" width="10%">Total Amount:
									</td>
									<td class="midcolora" width="15%"><b>
											<script>document.write(InsertDecimal('<%=Request.QueryString["DISTRIBUTION_AMOUNT"]%>'));</script>
										</b>
									</td>
									<td class="midcolorr"></td>
								</tr>
								<tr>
									<td class="midcolora" width="10%">Distributed:
									</td>
									<td class="midcolora" id="Distributed" width="15%"></td>
									<td class="midcolorr"></td>
								</tr>
								<tr>
									<td class="midcolora" width="10%">Remaining:
									</td>
									<td class="midcolora" id="Remaining" width="15%"></td>
									<td class="midcolorr" width="80%"></td>
								</tr>
							</table>
						</td>
					</tr>
					<TR>
						<TD class="midcolora" align="center" colSpan="2"><asp:datagrid id="dgrCashReceipt" runat="server" DataKeyField="IDEN_ROW_ID" AutoGenerateColumns="False"
								Width="100%">
								<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
								<ItemStyle CssClass="midcolora"></ItemStyle>
								<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
								<ItemStyle Height="4"></ItemStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Select">
										<ItemTemplate>
											<asp:CheckBox ID="chkSelect" Visible="False" Runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Account#">
										<ItemTemplate>
											<asp:TextBox ID="txtAccount" size="9" Runat="server" ReadOnly="False" MaxLength="8"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Description - Account#">
										<ItemTemplate>
											<asp:DropDownList ID="cmbAccount" Runat="server"></asp:DropDownList>
											<asp:CustomValidator id="csvAccount" runat="server" ControlToValidate="cmbAccount" ErrorMessage="Please select Account." ClientValidationFunction="ValidateAcount" Display="Dynamic"></asp:CustomValidator>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Percentage">
										<ItemTemplate>
											<asp:TextBox ID="txtPercentage" onchange="CalcAmount(this.id)" size="8" CssClass="INPUTCURRENCY" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DISTRIBUTION_PERCT")%>' ReadOnly="False" MaxLength="5">
											</asp:TextBox>
											<asp:CustomValidator id="csvPercentage" runat="server" ControlToValidate="txtPercentage" ErrorMessage="Please enter percentage." ClientValidationFunction="ValidateAMOUNT_PERCENTAGE" Display="Dynamic"></asp:CustomValidator>
											<asp:RegularExpressionValidator ID="revPercentage" Runat="server" ControlToValidate="txtPercentage" Display="Dynamic"></asp:RegularExpressionValidator><br>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Amount">
										<ItemTemplate>
											<asp:TextBox ID="txtAmount" onchange="CalcPercentage(this.id)" size="18" CssClass="INPUTCURRENCY" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DISTRIBUTION_AMOUNT")%>' ReadOnly="False" MaxLength="15">
											</asp:TextBox>
											<asp:RegularExpressionValidator ID="revAmount" Runat="server" ControlToValidate="txtAmount" Display="Dynamic"></asp:RegularExpressionValidator>
											<asp:CustomValidator id="csvAmount" runat="server" ControlToValidate="txtAmount" ErrorMessage="Please enter amount." ClientValidationFunction="ValidateRECEIPT_AMOUNT" Display="Dynamic"></asp:CustomValidator>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Note">
										<ItemTemplate>
											<asp:TextBox ID="txtNote" Runat="server" size="28" Text='<%# DataBinder.Eval(Container.DataItem, "NOTE")%>' ReadOnly="False" MaxLength="50">
											</asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<td class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton><asp:button id="btnPrevious" CssClass="clsButton" Text="Previous" CommandName="Previous" OnCommand="Navigation_Click"
								Runat="server"></asp:button><asp:button id="btnNext" CssClass="clsButton" Text="Next" CommandName="Next" OnCommand="Navigation_Click"
								Runat="server"></asp:button></td>
						<TD class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnSavClos" runat="server" Text="Save &amp; Close"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnClose" runat="server" Text="Close"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></TD>
					</TR>
					<TR>
						<TD colSpan="2"><INPUT id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
							<INPUT id="hidAppVersionID" type="hidden" value="0" name="hidAppVersionID" runat="server">
							<INPUT id="hidAppID" type="hidden" value="0" name="hidAppID" runat="server"> <INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
							<INPUT id="hidtotalPages" type="hidden" value="0" name="hidtotalPages" runat="server">
							<INPUT id="hidCurrentPage" type="hidden" value="0" name="hidCurrentPage" runat="server">
							<INPUT id="hidLineItemId" type="hidden" value="0" name="hidLineItemId" runat="server">
							<INPUT id="hidAddNew" type="hidden" value="0" name="hidAddNew" runat="server"> <INPUT id="hidCash" type="hidden" value="0" name="hidCash" runat="server">
							<INPUT id="hidpageDefaultsize" type="hidden" value="0" name="hidpageDefaultsize" runat="server">
							<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</form></TR></TBODY></TABLE></FORM>
	</body>
</HTML>

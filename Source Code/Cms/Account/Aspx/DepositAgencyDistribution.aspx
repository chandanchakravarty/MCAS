<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="DepositAgencyDistribution.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.DepositAgencyDistribution" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Agency Receipt Distribution</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script language="javascript">
		var total = 0;
		var tempTotal = 0;
		//Caluclates the row wise due amount 
		function onAmountChange(RowNo)
		{
			strBalance = "dgOpenItems__ctl" + RowNo + "_lblbalance";
			strDue = "dgOpenItems__ctl" + RowNo + "_lblDUE_AMOUNT";
			strAmount = "dgOpenItems__ctl" + RowNo + "_txtAPPLIED_AMOUNT";
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
				document.getElementById(strBalance).innerHTML = ParseFloatEx(document.getElementById(strDue).innerHTML) - ParseFloatEx(amount);
			}
			
		
			document.getElementById(strBalance).innerHTML = (ParseFloatEx(document.getElementById(strBalance).innerHTML).toFixed(2));
			CalculateTotalAmount();
			
			}
	
		//Calculates the total due amount
		function CalculateTotalDue()
		{
			var flag = true;
			var ctr = 1;
			var total = 0;
			
			while (flag == true)
			{
				strAmount = "dgOpenItems__ctl" + (ctr + 1) + "_lblDUE_AMOUNT";
				
				if (document.getElementById(strAmount) != null)
				{
					if (document.getElementById(strAmount).innerHTML.trim() != "" && ! isNaN(document.getElementById(strAmount).innerHTML.trim()))
						total = total + ParseFloatEx(document.getElementById(strAmount).innerHTML);
				}
				else
				{
					flag = false;
				}
				ctr++;
			}
			document.getElementById("lblTotalDue").innerHTML = (total.toFixed(2));
		}
		
		//Modified by praveen April 10 2008 : 
		function InsertDecimalAmt(AmtValues)
		{
			AmtValues = ReplaceAll(AmtValues,".","");
			DollarPart = AmtValues.substring(0, AmtValues.length - 2);
			CentPart = AmtValues.substring(AmtValues.length - 2);
			//tmp = formatCurrency(DollarPart) + "." + CentPart;
			tmp = DollarPart + "." + CentPart;
			return tmp;
		}
		
		//Formats the amount and convert 111 into 1.11
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
					txtAppliedAmount.value = InsertDecimalAmt(amt);
				}
			}
			
			
		}
		
		//Validated the form before submitting for save
		function OnSaveClick()
		{
				
				chkCount = 0
				textCount = 0
				var checkBoxID = 'chkSelect';	
				re = new RegExp(':' + checkBoxID + '$')  //generated controlname starts with a colon
				
				for(i = 0; i < document.ACT_AGENCY_DEPOSIT_DISTRIBUTION.elements.length; i++) 
				{
					elm = document.ACT_AGENCY_DEPOSIT_DISTRIBUTION.elements[i]
					if (elm.type == 'checkbox') 
					{
						if (re.test(elm.name)) 
							if (elm.checked)
							{
								chkCount = chkCount + 1;
							}
					}
					if(elm.type == 'text') 
					{
						if(elm.value != "")
						{
							textCount = textCount + 1;
						}
					}
					
				} 
			  if (chkCount == 0)
			  {
					alert("Please Select at least One Record.")
					return false;
			  }
			  if(chkCount < textCount)
			  {
					alert("Amount will be saved for selected records only. Please select record.")
					return false;
			  }
			  if(ValidateAmountApplied() == false)
				return false;
			  Page_ClientValidate();
		}
	
		function ValidateAmountApplied()
		{
			var TotalDue = ParseFloatEx(document.getElementById('lblTotalDue').innerText);
			var ReceiptAmt = ParseFloatEx(document.getElementById('lblTotalReceptAmount').innerText);
			var AppliedAmt = ParseFloatEx(document.getElementById('lblTotalAmount').innerText)
			var LesserAmt;
			
			// find lesser amount of receipt amt and TotalDue amt
			if(TotalDue < ReceiptAmt)
				LesserAmt = TotalDue;
			else
				LesserAmt = ReceiptAmt
			
			// chk for amt applied to be lesser than or equal to the 'LesserAmt'
			if(AppliedAmt > LesserAmt)
			{
				alert('Total amount applied cannot be greater than the Receipt/Total Due amount');
				document.getElementById('hidReconStatus').value = "N"; //Not Distributed
				return false;
			}
			else if(AppliedAmt < LesserAmt)
			{
				document.getElementById('hidReconStatus').value = "N"; //Not Distributed
			}
			else
			{
				document.getElementById('hidReconStatus').value = "Y"; // Fully Distributed
			}
		}
		
		//Uday
		function Uncheck()
		{
			document.getElementById('chSelectAll').checked = false;
		}
		//
		//Resets the form to its original state
		function Reset()
		{
			DisableValidators();
			document.ACT_AGENCY_DEPOSIT_DISTRIBUTION.reset();
			CalculateTotalAmount();
			return false;
		}
	
		//Validates the amount
		function txtAMOUNT_Validate(objSource,objArgs)
		{
			ctrlId = objSource.controltovalidate;
			ctrlId = ctrlId.replace('txtAPPLIED_AMOUNT', 'lblDUE_AMOUNT');
			
			FormatAmount(document.getElementById(objSource.controltovalidate));
			value = document.getElementById(objSource.controltovalidate).value;
			
			value = ReplaceString(value, ",","");
			if (value != "")
			{
				if (isNaN(value))
				{
					objArgs.IsValid = false;
					return;
				}
				else
				{
					if (ParseFloatEx(value) == 0)
					{
						objArgs.IsValid = false;
						return;
					}
				}
			}
			
			dueCtrl = document.getElementById(ctrlId);
			if ( dueCtrl != null)
			{
				if (ParseFloatEx(dueCtrl.innerHTML) > 0)
				{
					if (value > ParseFloatEx(dueCtrl.innerHTML) || value < 0)
					{
						objSource.innerHTML = "Applied amount can not be greater than due amount and can not be less then zero."
						objArgs.IsValid = false;
						return;
					}
				}
				else
				{
					if (value < ParseFloatEx(dueCtrl.innerHTML) || value >0 )
					{
						objSource.innerHTML = "Applied amount can not be less then due amount and can not be greater then zero."
						objArgs.IsValid = false;
						return;
					}
				}
			}
			objArgs.IsValid = true;
			
		}
		
		function refreshParent()
		{
			//var loc=window.opener.location.href;   
			//window.opener.location=loc.toString();  
			window.opener.__doPostBack('btnFind', null);
		}
		
		//Added to select all & deselect all checkboxes on 23 Jan : Uday
		
		//Fill Amount on Select all : Uday
			function FillPayAmounts(chkAll)
			{
			
				var flag = true;
				var ctr = 1;
				if(chkAll.checked)
				{
					while(flag == true)
					{
						rowNo = ctr + 1;
						prefix = "dgOpenItems__ctl" + rowNo;
						balCtrl = document.getElementById(prefix + "_lblDUE_AMOUNT");
						ctrl = document.getElementById(prefix + "_txtAPPLIED_AMOUNT");
						 			 
						if(balCtrl!=null && ctrl!=null)
						{
						
							var balAmt = new String();
							balAmt =  balCtrl.innerHTML;
							if(ctrl.value=="")
								ctrl.value = balAmt;
						}
						else
						{
							flag = false;
						}										 
						ctr++;
					 
					}
				}
				else
				{
					while(flag == true)
					{
						rowNo = ctr + 1;
						prefix = "dgOpenItems__ctl" + rowNo;
						balCtrl = document.getElementById(prefix + "_lblDUE_AMOUNT");
						ctrl = document.getElementById(prefix + "_txtAPPLIED_AMOUNT");
						 			 
						if(balCtrl!=null && ctrl!=null)
						{
						
							var balAmt = new String();
							balAmt =  balCtrl.innerHTML;
							if(ctrl.value!="")
								ctrl.value = "";
						}
						else
						{
							flag = false;
						}										 
						ctr++;
					 
					}
				//added to get total applied amount on 25 Jan
				document.getElementById("lblTotalAmount").innerHTML = tempTotal.toFixed(2);	
				}
				fillAmtOnTxtChng();
			}
		
		//Select deselect Checkboxs : Uday
			function confun(elm)
				{					
						if (elm.checked)
						{
								aspCheckBoxID = 'chkSelect';	//for seond checkbox section
								re = new RegExp(':' + aspCheckBoxID + '$')  //generated controlname starts with a colon
								for(i = 0; i < document.ACT_AGENCY_DEPOSIT_DISTRIBUTION.elements.length; i++) {
									elm = document.ACT_AGENCY_DEPOSIT_DISTRIBUTION.elements[i]
									if (elm.type == 'checkbox') {
										if (re.test(elm.name)) 
										elm.checked = true;
										
									}
								}
						}
						else
						{
							
								aspCheckBoxID = 'chkSelect';	//for seond checkbox section
								re = new RegExp(':' + aspCheckBoxID + '$')  //generated controlname starts with a colon
								for(i = 0; i < document.ACT_AGENCY_DEPOSIT_DISTRIBUTION.elements.length; i++) {
									elm = document.ACT_AGENCY_DEPOSIT_DISTRIBUTION.elements[i]
									if (elm.type == 'checkbox') {
										if (re.test(elm.name)) 
										elm.checked = false;
									}
								}
						}
				}		
		
		//uday
		
		
		
		function fillAmtOnChkChng(ctr,Amt)
		{
			var selChkID  = "dgOpenItems__ctl" + ctr + "_chkSelect";
			var objChk    = document.getElementById(selChkID);
			var txtAmt = "dgOpenItems__ctl" + ctr + "_txtAPPLIED_AMOUNT";
					
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
				txtAmt = "dgOpenItems__ctl" + ctr + "_txtAPPLIED_AMOUNT";
				
				if(document.getElementById(txtAmt).value != "")
				{
					amt = document.getElementById(txtAmt).value;
					amt = amt.replace(",","");
					total = ParseFloatEx(amt); // + parseFloat(document.getElementById("lblTotalAmount").innerHTML);
					tempTotal = (tempTotal + total);
				}
				ctr++;
			}
		
			document.getElementById("lblTotalAmount").innerHTML = tempTotal.toFixed(2);
			
		}
		//Caluclates the total of applied amount
		function CalculateTotalAmount()
		{
			var flag = true;
			var ctr = 1;
			var total = 0;
			var tmpAmt = new String();
			while (flag == true)
			{
				strAmount = "dgOpenItems__ctl" + (ctr + 1) + "_txtAPPLIED_AMOUNT";
				
				if (document.getElementById(strAmount) != null)
				{
					if (document.getElementById(strAmount).value.trim() != "" )//&& ! isNaN(document.getElementById(strAmount).value.trim()))
					{
						tmpAmt = document.getElementById(strAmount).value;
						tmpAmt = tmpAmt.replace(",",""); 
						total = total + ParseFloatEx(tmpAmt);
					}
						
				}
				else
				{
					flag = false;
				}
				ctr++;
			}
			//alert(document.getElementById("lblTotalAmount").innerHTML)
			document.getElementById("lblTotalAmount").innerHTML = (total.toFixed(2));
			//alert(document.getElementById("lblTotalAmount").innerHTML)
		}
		
		</script>
	</HEAD>
	<body onload="CalculateTotalDue();CalculateTotalAmount();Uncheck();" leftMargin="20" rightMargin="20" MS_POSITIONING="GridLayout" onunload="refreshParent();">
		<form id="ACT_AGENCY_DEPOSIT_DISTRIBUTION" method="post" runat="server">
			<TABLE  border="0" width="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td align="center" colSpan="5">
						<asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False" EnableViewState="False"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="headereffectCenter" colSpan="5">Agency Receipt Distribution
					</td>
				</tr>
				<tr>
					<td class="midcolora" colspan="5">
						<asp:Label Runat="server" id="Label1">Total Receipt Amount : </asp:Label>
						<asp:Label ID="lblTotalReceptAmount" Runat="server" CssClass="LabelFont">Total Receipt Amount</asp:Label>
					</td>
				</tr>
				<TR class="headereffectWebGrid">
					<TD colSpan="5"><asp:checkbox id="chSelectAll" Runat="server" Onclick="confun(this);FillPayAmounts(this);"></asp:checkbox>Select 
						All
					</TD>
				</TR>
				<tr>
					<td class="midcolora" colSpan="5">
						<asp:datagrid id="dgOpenItems" runat="server" DataKeyField="ROW_ID" AutoGenerateColumns="False"
							Width="100%">
							<alternatingitemstyle cssclass="midcolora"></alternatingitemstyle>
							<itemstyle cssclass="midcolora"></itemstyle>
							<headerstyle cssclass="headereffectwebgrid"></headerstyle>
							<columns>
								<asp:TemplateColumn HeaderText="Select" ItemStyle-Width="5%">
									<ItemTemplate>
										<asp:CheckBox ID="chkSelect" Runat="server"></asp:CheckBox>
										<input id="hidSOURCE_ROW_ID" type="hidden" name="hidSOURCE_ROW_ID" value='<%# DataBinder.Eval(Container.DataItem, "ROW_ID")%>' runat="server">
										<input id="hidIDEN_ROW_NO" type="hidden" name="hidIDEN_ROW_NO" runat="server">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Policy #" ItemStyle-Width ="10%">
									<ItemTemplate>
										<asp:Label ID="lblPOLICY_NUMBER" Runat="server">
											<%# DataBinder.Eval(Container.DataItem, "POLICY_NUMBER")%>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Customer Name" ItemStyle-Width ="30%">
									<ItemTemplate>
										<asp:Label ID="Label2" Runat="server">
											<%# DataBinder.Eval(Container.DataItem, "CUSTOMER_NAME")%>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tran Type" ItemStyle-Width ="10%">
									<ItemTemplate>
										<asp:Label ID="Label3" Runat="server">
											<%# DataBinder.Eval(Container.DataItem, "TRAN_TYPE")%>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="LOB" ItemStyle-Width ="10%">
									<ItemTemplate>
										<asp:Label ID="Label4" Runat="server">
											<%# DataBinder.Eval(Container.DataItem, "LOB_CODE")%>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Due Amount" ItemStyle-Width ="10%" ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="Right">
									<ItemTemplate>
										<asp:Label Runat="server" ID="lblDUE_AMOUNT"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Applied Amount" ItemStyle-Width ="15%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign ="Right">
									<ItemTemplate>
										<asp:TextBox id="txtAPPLIED_AMOUNT" CssClass="inputcurrency" Runat="server" Text= <%# DataBinder.Eval(Container.DataItem, "RECON_AMOUNT")%>></asp:TextBox>
										<br>
										<asp:RegularExpressionValidator  ID="revAPPLIED_AMOUNT" Enabled="False" ControlToValidate="txtAPPLIED_AMOUNT"
											display="Dynamic" ErrorMessage="Please enter valid numeric value." 
											Runat="server"></asp:RegularExpressionValidator>
										<asp:CustomValidator id="csvAPPLIED_AMOUNT" ErrorMessage="Please enter valid amount." runat="server" Display="Dynamic" 
										ControlToValidate="txtAPPLIED_AMOUNT" ClientValidationFunction="txtAMOUNT_Validate"></asp:CustomValidator>
										<asp:RequiredFieldValidator ID="rfvAPPLIED_AMOUNT" Runat="server" ControlToValidate="txtAPPLIED_AMOUNT" Display="Dynamic" ErrorMessage="Please enter Applied Amount." Enabled="False"></asp:RequiredFieldValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:templatecolumn HeaderText="Balance" HeaderStyle-HorizontalAlign="Right" ItemStyle-CssClass="midcolorr"
									ItemStyle-Wrap="False" ItemStyle-Width ="10%" Visible="False">
									<itemtemplate>
										<asp:label id="lblbalance" width="10%" runat="server"></asp:label>
									</itemtemplate>
								</asp:templatecolumn>
							</columns>
						</asp:datagrid>
					</td>
				</tr>
				<tr>
					<td colspan="1" class="midcolorr">Total Due :
						<asp:label id="lblTotalDue" cssclass="LabelFont" width="10%" runat="server"></asp:label>
					</td>
					<td colspan="3" class="midcolorr">Total Applied :
						<asp:label id="lblTotalAmount" cssclass="LabelFont"  runat="server"></asp:label>
					</td>
					<td class="midcolorr" colspan="1"></td>
				</tr>
				<tr>
					<td class="midcolora" colspan="3">
						<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
					</td>
					<td class="midcolorr" colspan="2">
						<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>&nbsp;
						<cmsb:cmsbutton class="clsButton" id="btnClose" runat="server" Text="Close"></cmsb:cmsbutton>
					</td>
				</tr>
				<tr>
					<td>
						<input id="hidOldData" type="hidden" name="hidOldData" runat="server"> <input id="hidDEPOSIT_ID" type="hidden" value="0" name="hidDEPOSIT_ID" runat="server">
						<input id="hidAGENCY_ID" type="hidden" name="hidAGENCY_ID" runat="server"> <input id="hidMONTH" type="hidden" name="hidMONTH" runat="server">
						<input id="hidYEAR" type="hidden" name="hidYEAR" runat="server">
						<input type="hidden" id ="hidGROUP_ID" runat="server">
						<input id="hidCD_LINE_ITEM_ID" type="hidden" runat="server">
						<input id="hidRowCount" type="hidden" runat="server" NAME="hidRowCount">
						<input id="hidReconStatus" type="hidden" runat="server" NAME="hidReconStatus">
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

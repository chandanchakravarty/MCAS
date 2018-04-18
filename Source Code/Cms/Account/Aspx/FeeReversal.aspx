<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Page language="c#" Codebehind="FeeReversal.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.FeeReversal" validateRequest="false"  %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FeeReversal</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=base.GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">	
			
		function ClearValidators()
		{
			
			var ctr = 3;
			var rfvValidator = document.getElementById("grdFeesReversal__ctl" + ctr + "_rfvFeeAmtReversed");
			var FeeAmtReversed = document.getElementById("grdFeesReversal__ctl" + ctr + "_txtFeeAmtReversed");			
			//var rnvFeeAmtReversed = document.getElementById("grdFeesReversal__ctl" + ctr + "_rnvFeeAmtReversed");
			
			while (rfvValidator != null)
			{
				if (rfvValidator == null )//|| rnvFeeAmtReversed == null)
				{
					break;
				}
				else
				{
					rfvValidator.setAttribute("enabled", false);
					rfvValidator.style.display = "none";
					
//					rnvFeeAmtReversed.setAttribute("enabled", false);
//					rnvFeeAmtReversed.style.display = "none";
					
					FeeAmtReversed.enabled = false;
				}
				ctr++;
				rfvValidator = document.getElementById("grdFeesReversal__ctl" + ctr + "_rfvFeeAmtReversed");								
				FeeAmtReversed = document.getElementById("grdFeesReversal__ctl" + ctr + "_txtFeeAmtReversed");			
//				rnvFeeAmtReversed = document.getElementById("grdFeesReversal__ctl" + ctr + "_rnvFeeAmtReversed");
			}
		}
		
		function CheckDelete()
		{
			for (ctr=1; ctr<=16; ctr++)
			{
				flag = 0;
				if(document.getElementById("grdFeesReversal__ctl" + (ctr + 1)+ "_chkSelect") != null)
				{
					if (document.getElementById("grdFeesReversal__ctl" + (ctr + 1)+ "_chkSelect").checked == true)
					{
						flag = 1;
						break;					
					}
				}
			}
			
			if (flag == 0)
			{	
				// Not a single row is selected , hence returning false
				alert("Please check the check box(es).");
				if(document.getElementById('btnReverseInProgress')!=null)
				{
					document.getElementById('btnReverseInProgress').style.display="none";
					document.getElementById('btnReverse').style.display="inline";
				}
				return false;
			}
		}		
		// enable validators when chk box is checked
		function EnableValidators(ctr)
		{
			var rfvValidator = document.getElementById("grdFeesReversal__ctl" + ctr + "_rfvFeeAmtReversed");			
			var FeeAmtReversed = document.getElementById("grdFeesReversal__ctl" + ctr + "_txtFeeAmtReversed");			
			var revFeeAmtReversed = document.getElementById("grdFeesReversal__ctl" + ctr + "_revFeeAmtReversed");			
			var csvFeeAmtReversed = document.getElementById("grdFeesReversal__ctl" + ctr + "_csvFeeAmtReversed");			
			
			if (document.getElementById("grdFeesReversal__ctl" + (ctr)+ "_chkSelect").checked == true)
			{				
				rfvValidator.setAttribute("enabled",true);
				rfvValidator.setAttribute("isValid",false);		
				rfvValidator.style.display = "none";														
				
				revFeeAmtReversed.setAttribute("enabled",true);
				revFeeAmtReversed.setAttribute("isValid",false);
				
				FeeAmtReversed.readOnly = false;
				
				csvFeeAmtReversed.setAttribute("enabled",true);
				csvFeeAmtReversed.setAttribute("isValid",false);					
				//FeeAmtReversed.setAttribute("isValid",false);					
				
			}
			else if (document.getElementById("grdFeesReversal__ctl" + (ctr)+ "_chkSelect").checked == false)
			{
				rfvValidator.setAttribute("enabled",false);
				rfvValidator.setAttribute("isValid",true);	
				rfvValidator.style.display = "none";		
					
				revFeeAmtReversed.setAttribute("enabled",false);
				revFeeAmtReversed.setAttribute("isValid",true);	
				revFeeAmtReversed.style.display = "none";		
				
				FeeAmtReversed.readOnly = true;
				
				csvFeeAmtReversed.setAttribute("enabled",false);
				csvFeeAmtReversed.setAttribute("isValid",true);					
				csvFeeAmtReversed.style.display ="none"
				//FeeAmtReversed.value = "";
				
			}
			
		}		
		function formReset()
		{
			document.location.href = "FeeReversal.aspx";
		    return false;
		}
		//Formats the amount and convert 111 into 1.11
		function FormatAmount(txtReverseAmount)
		{
						
			if (txtReverseAmount.value != "")
			{
				amt = txtReverseAmount.value;
				
				amt = ReplaceAll(amt,".","");
				
				if (amt.length == 1)
					amt = amt + "0";
				
				if ( ! isNaN(amt))
				{
					
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					
					txtReverseAmount.value = InsertDecimal(amt);
				}
			}
		}
		//Fees Amount to be Reversed = Fee Amount - Fee Reversed
		function onChange(RowNo)
		{		
				chkSelect_ID  = "grdFeesReversal__ctl" + RowNo + "_chkSelect";
				chkSelect = document.getElementById(chkSelect_ID);
		    	tobeReversed  = "grdFeesReversal__ctl" + RowNo + "_txtFeeAmtReversed";
		    	//var rfvFeeAmtRev = document.getElementById("grdFeesReversal__ctl" + RowNo + "_rfvFeeAmtReversed");
		    	
		  		if (chkSelect.checked == true)
				{
					//rfvFeeAmtRev.setAttribute('enabled',true);
		    		/*feeAmount = "grdFeesReversal__ctl" + RowNo + "_lblFeeAmount";
					feeAmount_val = document.getElementById(feeAmount).innerHTML;
					
					feePaid = "grdFeesReversal__ctl" + RowNo + "_lblFeeAmtReceived";
					feePaid_val = document.getElementById(feePaid).innerHTML;
												
					feeReverse = "grdFeesReversal__ctl" + RowNo + "_lblTRANS_DESC";
					feeReverse_val = document.getElementById(feeReverse).innerHTML;*/
					
					maxReverse ="grdFeesReversal__ctl" + RowNo + "_hidMAX_REVERSE";
					maxReverse_Val = document.getElementById(maxReverse).value;
					document.getElementById(tobeReversed).value  = maxReverse_Val;
					
					//document.getElementById(tobeReversed).value = (feeAmount_val - feeReverse_val) //* 100; //Format Amt
					
					FormatAmount(document.getElementById(tobeReversed));
				}
				else
				{
					document.getElementById(tobeReversed).value = "";
				//	rfvFeeAmtRev.setAttribute('enabled',false);
					
				}							
				
				
				
		}
		
		function ValidateFeeAmtForReversal(AmtCtrlID)
		{ 
			var AppliedAmtVal = document.getElementById(AmtCtrlID).value;
			var tmpStr = new String();
			tmpStr = AmtCtrlID;
			var lblAmt = tmpStr.replace("txtFeeAmtReversed","hidMAX_REVERSE");
			var TotAmtVal = document.getElementById(lblAmt).value;
			var csvVal = tmpStr.replace("txtFeeAmtReversed","csvFeeAmtReversed");
			//alert(TotAmtVal)
			if((parseFloat(AppliedAmtVal.replace(",","")) > parseFloat(TotAmtVal)) || parseFloat(AppliedAmtVal.replace(",","")) == 0)
			{
				document.getElementById(csvVal).setAttribute('IsValid',false);
				document.getElementById(csvVal).setAttribute('enabled',true);
				document.getElementById(csvVal).style.display = 'inline';
				Page_IsValid = false;
			}	
			else
			{
				document.getElementById(csvVal).setAttribute('IsValid',true);
				document.getElementById(csvVal).setAttribute('enabled',false);
				document.getElementById(csvVal).style.display = 'none';
				Page_IsValid = true;
			}
		}
		function SetFocus()
		{
			document.getElementById("txtPOLICY_ID").focus();	
			//Added by Shikha #5534, 10/03/09.
			if(document.getElementById('btnReverseInProgress')!=null)
				{
					document.getElementById('btnReverseInProgress').style.display="none";
				}	
			//End.
		}
		//Code added by Shikha Dixit for #5534 on 10-03-09.
		function HideShowReverse()
		{
			if(document.getElementById('btnReverse')!=null)
				document.getElementById('btnReverse').style.display="none";
				
			if(document.getElementById('btnReverseInProgress')!=null)
				{
					document.getElementById('btnReverseInProgress').style.display="inline";
					document.getElementById('btnReverseInProgress').disabled="true";
				}
							
		}
		//End of addition.
		</script>
	</HEAD>
	<body oncontextmenu="return false;" leftmargin="0" rightmargin="0" onload="ApplyColor();ChangeColor();setfirstTime();ClearValidators();SetFocus();"
		MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div id="bodyHeight" class="pageContent">
				<table cellSpacing="0" cellPadding="0" align="center" border="0" width="100%">
					<tr>
						<td>
							<TABLE id="Table1" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
								<tr>
									<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
										mandatory</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="5"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPOLICY_ID" runat="server">Policy Number</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPOLICY_ID" ReadOnly="False" runat="server" size="14" maxlength="10"></asp:textbox><IMG id="imgSelect" style="CURSOR: hand" alt="" src="../../cmsweb/images/selecticon.gif"
											runat="server"> <INPUT id="hidPolicyID" type="hidden" runat="server" NAME="hidPolicyID"><br>
										<asp:regularexpressionvalidator id="revPOLICY_ID" Runat="server" Display="Dynamic" ControlToValidate="txtPOLICY_ID"></asp:regularexpressionvalidator>
										<br>
										<asp:requiredfieldvalidator id="rfvPOLICY_ID" runat="server" ControlToValidate="txtPOLICY_ID" ErrorMessage="Please enter Policy Number."
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capFEE_TYPE" runat="server">Fee Type</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbFEETYPE" runat="server" CausesValidation="false">
											<asp:ListItem Value="ALL">All</asp:ListItem>
											<asp:ListItem Value="INSF">Installment</asp:ListItem>
											<asp:ListItem Value="RENSF">Reinstatement</asp:ListItem>
											<asp:ListItem Value="NSFF">Non Sufficient Fund Fees</asp:ListItem>
											<asp:ListItem Value="LF"> Late Fees</asp:ListItem>
										</asp:dropdownlist><br>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%">From Date</TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtFromDate" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkFromDate" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgFromDate" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
										</asp:hyperlink><br>
										<asp:regularexpressionvalidator id="revFromDate" Runat="server" Display="Dynamic" ControlToValidate="txtFromDate"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%">To Date</TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtToDate" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkToDate" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgToDate" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
										</asp:hyperlink><br>
										<asp:regularexpressionvalidator id="revToDate" Runat="server" Display="Dynamic" ControlToValidate="txtToDate"></asp:regularexpressionvalidator><br>
										<asp:comparevalidator id="cmpToDate" Runat="server" Display="Dynamic" ControlToValidate="txtToDate" ControlToCompare="txtFromDate"
											Type="Date" Operator="GreaterThanEqual"></asp:comparevalidator></TD>
								</tr>
								<tr>
									<td class="midcolorr" colSpan="4"><cmsb:cmsbutton class="clsButton" id="btnFind" runat="server" Text="Find"></cmsb:cmsbutton></td>
								</tr>
							</TABLE>
						</td>
					</tr>
					<tr>
						<TD class="pageHeader" colSpan="4">Please note that all operations are performed on 
							selected check boxes only.</TD>
					</tr>
					<tr>
						<td><asp:datagrid id="grdFeesReversal" runat="server" DataKeyField="IDEN_ROW_ID" Width="100%" AlternatingItemStyle-CssClass="alternatedatarow"
								ItemStyle-CssClass="datarow" HeaderStyle-CssClass="headereffectCenter" AutoGenerateColumns="False"
								AllowPaging="True" PageSize="15" PagerStyle-Mode="NextPrev" PagerStyle-HorizontalAlign="Center"
								PagerStyle-CssClass="datarow" PagerStyle-NextPageText="Next >>" PagerStyle-PrevPageText="<< Prev"
								OnPageIndexChanged="grdFeesReversal_Paging" AllowSorting="true" OnSortCommand="Sort_Grid">
								<Columns>
									<asp:TemplateColumn HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center" HeaderText="Reverse">
										<ItemTemplate>
											<asp:CheckBox id="chkSelect" Runat="server"></asp:CheckBox>
											<INPUT id=hidPOLICY_ID_GRID type=hidden 	value='<%# DataBinder.Eval(Container.DataItem,"POLICY_ID")%>' name=hidPOLICY_ID_GRID runat="server">
											<INPUT id=hidCUSTOMER_ID type=hidden 	value='<%# DataBinder.Eval(Container.DataItem,"CUSTOMER_ID")%>' name=hidCUSTOMER_ID runat="server">
											<INPUT id=hidPOLICY_VERSION_ID type=hidden value='<%# DataBinder.Eval(Container.DataItem,"POLICY_VERSION_ID")%>'  name=hidPOLICY_VERSION_ID runat="server">
											<INPUT id="hidCUSTOMER_OPEN_ITEM_ID" type=hidden value='<%# DataBinder.Eval(Container.DataItem,"IDEN_ROW_ID")%>'  name=hidCUSTOMER_OPEN_ITEM_ID runat="server">
											<INPUT id="hidAFR_IDEN_ROW_ID" type=hidden value='<%# DataBinder.Eval(Container.DataItem,"AFR_IDEN_ROW_ID")%>'  name=hidAFR_IDEN_ROW_ID runat="server">
											<INPUT id="hidDUE_DATE" type=hidden value='<%# DataBinder.Eval(Container.DataItem,"DUE_DATE")%>'  name=hidAFR_IDEN_ROW_ID runat="server">
											<INPUT id="hidTOTAL_REVERSED" type=hidden value='<%# DataBinder.Eval(Container.DataItem,"TOTAL_REVERSED")%>'  name=hidTOTAL_REVERSED runat="server">
											<INPUT id="hidFEES_AMOUNT" type=hidden value='<%#DataBinder.Eval(Container.DataItem,"FEES_AMOUNT")%> ' name=hidFEES_AMOUNT runat="server">
											<INPUT id="hidFEES_AMT_RECD" type=hidden value='<%#DataBinder.Eval(Container.DataItem,"FEES_AMT_RECD")%> ' name=hidFEES_AMT_RECD runat="server">
											<INPUT id="hidITEM_CODE" type=hidden value='<%#DataBinder.Eval(Container.DataItem,"ITEM_CODE")%> ' name="hidITEM_CODE" runat="server">
											<INPUT id="hidMAX_REVERSE" type=hidden value='<%#DataBinder.Eval(Container.DataItem,"MAX_REVERSE")%> ' name="hidMAX_REVERSE" runat="server">
											<INPUT ID="hidTOTAL_DUE" type=hidden  Runat="server" value='<%#DataBinder.Eval(Container.DataItem,"TOTAL_DUE")%>' name="hidTOTAL_DUE" runat="server">
											<INPUT ID="hidINSTALLMENT_FEES" type=hidden  Runat="server" value='<%#DataBinder.Eval(Container.DataItem,"INSTALLMENT_FEES")%>' name="hidINSTALLMENT_FEES" runat="server">
											<INPUT ID="hidDATERECD" type=hidden  Runat="server" value='<%#DataBinder.Eval(Container.DataItem,"DATE_RECD")%>' name="hidDATERECD" runat="server">
											<INPUT ID="hidRecvdAmt" type=hidden  Runat="server" value='<%#DataBinder.Eval(Container.DataItem,"FEES_AMT_RECD")%>' name="hidRecvdAmt" runat="server">
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Width="15%" HeaderText="Customer Name/Policy Number" SortExpression="POLICY_NUMBER"
										HeaderStyle-ForeColor="white">
										<ItemTemplate>
											<asp:Label ID="lblcustomerName_PolicyNo" Runat="server" text='<%# DataBinder.Eval(Container.DataItem,"POLICY_NUMBER")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Width="6%" HeaderText="Fees Type" SortExpression="FEES_TYPE" HeaderStyle-ForeColor="white">
										<ItemTemplate>
											<asp:Label ID="lblFeeType" Runat="server" text='<%#DataBinder.Eval(Container.DataItem,"FEES_TYPE")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Width="8%" HeaderText="Fees Status(Received)" SortExpression="FEES_STATUS"
										HeaderStyle-ForeColor="white">
										<ItemTemplate>
											<asp:Label ID="lblFeeStatus" Runat="server" text='<%#DataBinder.Eval(Container.DataItem,"FEES_STATUS")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Width="8%" HeaderText="Date Recd" SortExpression="DATE_RECD" HeaderStyle-ForeColor="white">
										<ItemTemplate>
											<asp:Label ID="lblDateRecd" Runat="server" text='<%#DataBinder.Eval(Container.DataItem,"DATE_RECD")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<%--SortExpression="DUE_DATE" changed in to DUE_DATE1 for Itrack Issue 6799.  --%>
									<asp:TemplateColumn HeaderStyle-Width="8%" HeaderText="Fees Due Date" SortExpression="DUE_DATE1" HeaderStyle-ForeColor="white">
										<ItemTemplate>
											<asp:Label ID="lblDueDate" Runat="server" text='<%#DataBinder.Eval(Container.DataItem,"DUE_DATE")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Width="8%" HeaderText="Fees Amount" SortExpression="FEES_AMOUNT" HeaderStyle-ForeColor="white">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblFeeAmount" Runat="server" text='<%#DataBinder.Eval(Container.DataItem,"FEES_AMOUNT")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Width="8%" HeaderText="Fees Amount(Received)" HeaderStyle-HorizontalAlign="center"
										SortExpression="FEES_AMT_RECD" HeaderStyle-ForeColor="white">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblFeeAmtReceived" Runat="server" text='<%#DataBinder.Eval(Container.DataItem,"FEES_AMT_RECD")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Width="8%" HeaderText="Fees Reversed" SortExpression="TOTAL_REVERSED"
										HeaderStyle-ForeColor="white">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblTRANS_DESC" Runat="server" text='<%#DataBinder.Eval(Container.DataItem,"TOTAL_REVERSED")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Width="10%" HeaderText="Fees Amount to be Reversed" HeaderStyle-HorizontalAlign="center">
										<ItemTemplate>
											<asp:TextBox ReadOnly="True" CssClass="INPUTCURRENCY" ID="txtFeeAmtReversed" Runat="server" size="12"
												maxlength="10"></asp:TextBox>
											<asp:requiredfieldvalidator id="rfvFeeAmtReversed" runat="server" ControlToValidate="txtFeeAmtReversed" ErrorMessage="FeeAmtReversed  can not be blank."
												Display="Dynamic"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revFeeAmtReversed" runat="server" ControlToValidate="txtFeeAmtReversed" ErrorMessage="Please insert integer value."
												Display="Dynamic"></asp:regularexpressionvalidator><br>
											<asp:CustomValidator ID="csvFeeAmtReversed" Runat="server" ControlToValidate="txtFeeAmtReversed" Display="Dynamic"
												ErrorMessage="Fees Amount to be reversed can neither be 0 nor can be greater than Fees Amount."></asp:CustomValidator>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Width="8%" HeaderText="Recharge Fees" HeaderStyle-ForeColor="white">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Button ID="btnReverseAmt" Visible="false" Runat="server" Text="Recharge" cssclass="clsButton" CommandName="UpdateReverseAmount"></asp:Button>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></td>
					</tr>
					<tr>
						<td class="midcolora">
							<table width="100%">
								<tr>
									<td class="midcolora">
										<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" enabled="false"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" enabled="false" Visible="False"></cmsb:cmsbutton>
									</td>
									<td class="midcolorr">
										<cmsb:cmsbutton class="clsButton" id="btnReverseInProgress" runat="server" Text="Reversal Process In Progress" Visible="True"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnReverse" runat="server" Text="Reverse"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" enabled="false" Visible="False"></cmsb:cmsbutton>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td><webcontrol:footer id="pageFooter" runat="server"></webcontrol:footer></td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</HTML>

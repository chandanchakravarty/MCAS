<%@ Page language="c#" Codebehind="AddActivityExpense.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddActivityExpense" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddInlandMarine</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
			var rows=0;			
			
			var policyRows=0;			
			var vehicleRows=0;			
			var ctl = "ctl";
			var vehiclePaymentGridID='<%=PaymentDataGridID%>';			
			var policyPaymentGridID='<%=PolicyPaymentDataGridID%>';			
			var PolicyPrefix=policyPaymentGridID + "__" + ctl;
			var VehiclePrefix=vehiclePaymentGridID + "__" + ctl;
			var OutstandingPrefix = "_lblOUTSTANDING";
			var PaymentPrefix = "_txtPAYMENT_AMOUNT";
			
			function ValidateLength(objSource , objArgs)
			{
				if(document.getElementById('txtCLAIM_DESCRIPTION').value.length>255)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}
			//Added by Asfa(28-Mar-2008) - iTrack issue #3967
			function cmbPAYMENT_METHOD_Change()
			{
				
				combo = document.getElementById('cmbPAYMENT_METHOD');
				if(combo==null || combo.selectedIndex==-1)
					return;
				else if(combo.options[combo.selectedIndex].value == "11979") //Manual Check
				{
					if(document.getElementById('capCHECK_NUMBER') !=null)
					{
					    
						document.getElementById('capCHECK_NUMBER').style.display="inline";	
						document.getElementById('txtCHECK_NUMBER').style.display="inline";
						document.getElementById("revCHECK_NUMBER").setAttribute("enabled",true);
					}
				}
				else
				{
					if(document.getElementById('capCHECK_NUMBER') !=null)
					{
						document.getElementById('capCHECK_NUMBER').style.display="none";	
						document.getElementById('txtCHECK_NUMBER').style.display="none";
						document.getElementById('revCHECK_NUMBER').style.display="none";						
						document.getElementById("revCHECK_NUMBER").setAttribute("enabled",false);
					}
				}
			  return
			}
			function OpenWindow()
			{
				url = "PayeeIndex.aspx?CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&CALLED_FROM=EXPENSE&ACTION_ON_PAYMENT=" + document.getElementById("hidACTION_ON_PAYMENT").value + "&";
				document.location.href = url;				
				return false;
			}
			function CalculateTotal(Prefix,ElementPrefix)
			{
				if(Prefix==PolicyPrefix)
					rows = policyRows;
				else
					rows = vehicleRows;
					
				var Sum = parseFloat(0);
				for(var i=2;i<=rows;i++)
				{
					txtID = Prefix + i + ElementPrefix;												
					if(document.getElementById(txtID)==null) continue;
					document.getElementById(txtID).value = formatCurrencyWithCents(document.getElementById(txtID).value);
					txtValue = new String(document.getElementById(txtID).value);
					txtValue = replaceAll(txtValue,",","");
					if(txtValue!="" && !isNaN(txtValue))
					{
						Sum+=parseFloat(txtValue);
						//document.getElementById(txtID).value = formatCurrencyWithCents(txtValue);
					}
				}
				return Sum;
			}
			function CalculateTotalPayment()
			{		
				var Sum = parseFloat(0);
				Sum = CalculateTotal(PolicyPrefix,PaymentPrefix) + CalculateTotal(VehiclePrefix,PaymentPrefix);																
				document.getElementById("txtADDITIONAL_EXPENSE").value = formatCurrencyWithCents(document.getElementById("txtADDITIONAL_EXPENSE").value);
				AddExpVal = new String(document.getElementById("txtADDITIONAL_EXPENSE").value);				
				AddExpVal = replaceAll(AddExpVal,",","");
				if(AddExpVal!="" && !isNaN(AddExpVal))				
				{
					Sum+=parseFloat(AddExpVal);					
					document.getElementById("txtADDITIONAL_EXPENSE").value = formatCurrencyWithCents(document.getElementById("txtADDITIONAL_EXPENSE").value);
				}
				Sum = Math.round(Sum*10000)/100;
				document.getElementById("txtTOTAL_PAYMENT").value = formatCurrencyWithCents(Sum);								
			}
			function CalculateTotalRIReserve()
			{	
				/*var Sum = parseFloat(0);				
				Sum = CalculateTotal(PolicyPrefix,"_txtRI_RESERVE") + CalculateTotal(VehiclePrefix,"_txtRI_RESERVE");								
				document.getElementById("txtTOTAL_RI_RESERVE").value = formatCurrencyWithCents(Sum);								*/
				
			}
			function GoBack(PageName)
			{
				strURL = PageName + "?&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&";
				this.parent.location.href = strURL;
				return false;
			}
			function Init()
			{
				ApplyColor();
				ChangeColor();
				//setfirstTime();	
				//findMouseIn();			
				//top.topframe.main1.mousein = false;				
				policyRows = parseInt(document.getElementById("hidPolicyRowCount").value)+1;
				vehicleRows = parseInt(document.getElementById("hidVehicleRowCount").value)+1;				
				CalculateTotalPayment();
				cmbPAYMENT_METHOD_Change();
				CalculateTotalRIReserve();
				parent.document.getElementById(parent.ActivityTotalPaymentClientID).innerText = formatCurrencyWithCents(document.getElementById("txtTOTAL_PAYMENT").value);
				//if(document.getElementById("hidOldData").value=="" || document.getElementById("hidOldData").value=="0")
				//	document.getElementById("btnPaymentBreakdown").style.display="none";
			}
			function CompareAllOutstandingAndPayment()
			{
				Page_ClientValidate();								
				for(var i=2;i<=policyRows;i++)
				{
					txtOutstandingID = PolicyPrefix + i + OutstandingPrefix;												
					txtPaymentID = PolicyPrefix + i + PaymentPrefix;
					if(!CompareOutstandingAndPayment(txtOutstandingID,txtPaymentID))
						return false;
					
						
				}
				for(var i=2;i<=vehicleRows;i++)
				{
					txtOutstandingID = VehiclePrefix + i + OutstandingPrefix;												
					txtPaymentID = VehiclePrefix + i + PaymentPrefix
					if(!CompareOutstandingAndPayment(txtOutstandingID,txtPaymentID))
						return false;
				}		
				CalculateTotalPayment();		
				return Page_IsValid;
			}
			function CompareOutstandingAndPayment(txtOutstandingID,txtPaymentID)
			{
				if(document.getElementById(txtOutstandingID)==null)
					return true;
				OutstandingValue = new String(document.getElementById(txtOutstandingID).innerText);				
				OutstandingValue = replaceAll(OutstandingValue,",","");
				PaymentValue = new String(document.getElementById(txtPaymentID).value);				
				PaymentValue = replaceAll(PaymentValue,",","");		
				if(document.getElementById(txtPaymentID).readOnly)
					return true;
					
				if(!isNaN(parseInt(PaymentValue)))				
				{
					document.getElementById(txtPaymentID).value = formatCurrencyWithCents(document.getElementById(txtPaymentID).value);					
					/*if(parseInt(PaymentValue)>parseInt(OutstandingValue))
					{
						alert("Payment amount cannot be greater than Outstanding amount");						
						//document.getElementById(txtPaymentID).focus();
						return false;
					}*/
				}								
				return true;				
			}			
			
			function SetTabs()
			{
				if(document.getElementById('hidOldData').value != "" && document.getElementById('hidOldData').value!="0")
				{
					Url = "PayeeIndex.aspx?CLAIM_ID=" + document.getElementById('hidCLAIM_ID').value + "&ACTIVITY_ID=" + document.getElementById('hidACTIVITY_ID').value + "&CALLED_FROM=<%=CALLED_FROM_EXPENSE.ToString()%>&";					
					DrawTab(2,top.frames[1],'Payee Details',Url);					
				}
				else
				{	
					RemoveTab(2,top.frames[1]);										
				}							
			}			
			
		</script>
	</HEAD>
	<body onload="Init();SetTabs();" MS_POSITIONING="GridLayout">
		<!-- To add bottom menu -->
		<%--<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>--%>
		<!-- To add bottom menu ends here -->
		<div class="pageContent" id="bodyHeight">
			<form id="Form1" method="post" runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%">
					<%-- <TR>
    <TD>
<webcontrol:gridspacer id=grdspacer runat="server"></webcontrol:gridspacer></TD></TR>
<tr>
	<TD id="tdClaimTop" class="pageHeader" colSpan="4">
		<webcontrol:ClaimTop id="cltClaimTop" runat="server" width="100%"></webcontrol:ClaimTop>
	</TD>
</tr>--%>
					<tr>
						<td class="headereffectCenter" colSpan="4"><asp:label id="lblTitle" runat="server">Expense Payment</asp:label></td>
					</tr>
					<tr>
						<td class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" runat="server" EnableViewState="False" Visible="False" CssClass="errmsg"></asp:label></td>
					</tr>
					<TR id="trBody" runat="server">
						<TD colSpan="4">
							<table cellSpacing="0" cellPadding="0" width="100%">
								<%--<tr>
									<TD class="midcolora" width="18%"><asp:label id="capACTIVITY_DATE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblACTIVITY_DATE" runat="server"></asp:label></TD>--
									<td class="midcolorr" colSpan="4"><cmsb:cmsbutton class="clsButton" id="btnGoBack" runat="server" Text="Activity Details"></cmsb:cmsbutton></td>
								</tr>--%>
								<tr id="trPolicyPayment" runat="server">
									<TD class="headerEffectSystemParams" colSpan="4">Policy Level Expense Payments</TD>
								</tr>
								<tr>
									<td class="midcolora" colSpan="4"><asp:datagrid id="dgPolicyPayment" runat="server" Width="100%" AutoGenerateColumns="False">
											<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
											<ItemStyle CssClass="midcolora"></ItemStyle>
											<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="COV_DESC" HeaderText="Coverage" ItemStyle-Width="25%"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Limit" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblLIMIT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT") %>'>></asp:Label>
														<asp:Label ID="lblRESERVE_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RESERVE_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblEXPENSE_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.EXPENSE_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblCOV_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_TYPE" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_TYPE") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblOUTSTANDING" Runat="server" ></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<%--<asp:TemplateColumn HeaderText="Paid Loss" ItemStyle-Width="10%">
													<ItemTemplate>																												
														<asp:Label ID="lblPRIMARY_EXCESS" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PRIMARY_EXCESS","{0:,#,###}") >'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>--%>
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblRI_RESERVE" Runat="server"  ></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reinsurance Carrier" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblREINSURANCE_CARRIER" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REINSURANCECARRIER","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="MCCA Attachment Point" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblMCCA_ATTACHMENT_POINT" Runat="server" ></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="MCCA Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblMCCA_APPLIES" Runat="server" ></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Payment Amount" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="txtPAYMENT_AMOUNT" CssClass="INPUTCURRENCY" Runat="server" MaxLength="10"  >></asp:TextBox><br>
														<%--<asp:RangeValidator ID="rngPAYMENT_AMOUNT" MinimumValue="1" MaximumValue="9999999999" Type="Currency"
															Runat="server" Display="Dynamic" ControlToValidate="txtPAYMENT_AMOUNT"></asp:RangeValidator>--%>
														<asp:RegularExpressionValidator ID="revPAYMENT_AMOUNT" ControlToValidate="txtPAYMENT_AMOUNT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>
								<tr id="trVehiclePayment" runat="server">
									<TD class="headerEffectSystemParams" colSpan="4">Vehicle Level Expense Payments</TD>
								</tr>
								<tr>
									<td class="midcolora" colSpan="4"><asp:datagrid id="dgPayment" runat="server" Width="100%" AutoGenerateColumns="False">
											<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
											<ItemStyle CssClass="midcolora"></ItemStyle>
											<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
											<Columns>
												<%--<asp:BoundColumn DataField="COV_DESC" HeaderText="Coverage"></asp:BoundColumn>--%>
												<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="25%">
													<ItemTemplate>
														<asp:Label ID="lblCOV_DESC" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
														</asp:Label>
														<asp:Label ID="lblVEHICLE_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.VEHICLE_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblRESERVE_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RESERVE_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblEXPENSE_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.EXPENSE_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblCOV_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_TYPE" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_TYPE") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Limit" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblLIMIT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblOUTSTANDING" Runat="server"  ></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<%--<asp:TemplateColumn HeaderText="Paid Loss" ItemStyle-Width="10%">
													<ItemTemplate>														
														<asp:Label ID="lblPRIMARY_EXCESS" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PRIMARY_EXCESS","{0:,#,###}") >'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>--%>
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblRI_RESERVE" Runat="server"  ></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reinsurance Carrier" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblREINSURANCE_CARRIER" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REINSURANCECARRIER","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="MCCA Attachment Point" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblMCCA_ATTACHMENT_POINT" Runat="server" ></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="MCCA Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblMCCA_APPLIES" Runat="server" ></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Payment Amount" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="txtPAYMENT_AMOUNT" CssClass="INPUTCURRENCY" Runat="server" MaxLength="10"  >></asp:TextBox><br>
														<%--<asp:RangeValidator ID="rngPAYMENT_AMOUNT" MinimumValue="1" MaximumValue="9999999999" Type="Currency"
															Runat="server" Display="Dynamic" ControlToValidate="txtPAYMENT_AMOUNT"></asp:RangeValidator>--%>
														<asp:RegularExpressionValidator ID="revPAYMENT_AMOUNT" ControlToValidate="txtPAYMENT_AMOUNT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>
								<tr>
									<td colSpan="4">&nbsp;</td>
								</tr>
								<tr>
									<td class="midcolora"><b><asp:label id="capADDITIONAL_EXPENSE" runat="server">Amount</asp:label></b></td>
									<td class="midcolorr" colSpan="3"><asp:textbox id="txtADDITIONAL_EXPENSE" CssClass="INPUTCURRENCY" runat="server" MaxLength="10"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revADDITIONAL_EXPENSE" Runat="server" Display="Dynamic" ControlToValidate="txtADDITIONAL_EXPENSE"></asp:regularexpressionvalidator></td>
								</tr>
								<tr>
									<td colSpan="4">&nbsp;</td>
								</tr>
								<tr class="midcolora" id="trTotalPayments" runat="server">
									<TD class="midcolora" colSpan="1"><b>Total</b></TD>
									<td class="midcolora" colSpan="3">
										<table cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td class="midcolora" width="29%"></td>
												<TD width="55%"><asp:textbox class="midcolora" id="txtTOTAL_OUTSTANDING" Runat="server" BorderStyle="None" size="15"
														BorderWidth="0" ReadOnly="True"></asp:textbox></TD>
												<TD><asp:textbox class="midcolora" id="txtTOTAL_PAYMENT" Runat="server" BorderStyle="None" size="15"
														BorderWidth="0" ReadOnly="True"></asp:textbox></TD>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td colSpan="4">&nbsp;</td>
								</tr>
								<tr>
									<td class="midcolora" width="18%"><asp:label id="capCLAIM_DESCRIPTION" Runat="server"></asp:label></td>
									<td class="midcolora" colSpan="3"><asp:textbox id="txtCLAIM_DESCRIPTION" Runat="server" Columns="40" Rows="4" TextMode="MultiLine" onkeypress="MaxLength(this,255);"></asp:textbox><br>
										<asp:customvalidator Runat="server" id="csvCLAIM_DESCRIPTION"  Display="Dynamic" ControlToValidate="txtCLAIM_DESCRIPTION" ClientValidationFunction="ValidateLength"></asp:customvalidator></td>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" width="10" colSpan="4">&nbsp;</TD>
								</tr>
								<%--<tr>--%>
								<%--<td class="midcolora" width="18%"><asp:Label ID="capPAYEE" Runat="server"></asp:Label><span class="mandatory">*</span></td>
									<td class="midcolora" width="32%">
										<asp:ListBox ID="cmbPAYEE" Runat="server" SelectionMode="Multiple"></asp:ListBox><br>
										<asp:RequiredFieldValidator ID="rfvPAYEE" Display="Dynamic" ControlToValidate="cmbPAYEE" ErrorMessage="select" 
											Runat="server"></asp:RequiredFieldValidator>
									</td>--%>
								<%--<td class="midcolora" width="18%"><asp:label id="capACTION_ON_PAYMENT" Runat="server"></asp:label></td>
									<td class="midcolora" width="32%"><asp:dropdownlist id="cmbACTION_ON_PAYMENT" Runat="server" AutoPostBack="True"></asp:dropdownlist></td>
									<td class="midcolorr" colSpan="2"></td>
								</tr>--%>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPAYMENT_METHOD" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPAYMENT_METHOD" onfocus="SelectComboIndex('cmbPAYMENT_METHOD')" runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvPAYMENT_METHOD" runat="server" ControlToValidate="cmbPAYMENT_METHOD" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<%--<TD class="midcolora" width="50%" colspan="2"></TD>--%>
									<td class="midcolora" width="18%"><asp:label id="capCHECK_NUMBER" Runat="server">Check Number</asp:label></td>
									<td class="midcolora" colSpan="3"><asp:textbox id="txtCHECK_NUMBER" Runat="server" maxlength="20"></asp:textbox><br>
								    <asp:regularexpressionvalidator id="revCHECK_NUMBER" Runat="server" Display="Dynamic" ControlToValidate="txtCHECK_NUMBER"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<td class="midcolora" width="18%">Account to Debit<span class="mandatory">*</span></td>
									<td class="midcolora" width="32%"><asp:dropdownlist id="cmbDrAccts" Width="200" Runat="server" AutoPostBack="False"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvDrAccts" Runat="server" Display="Dynamic" ControlToValidate="cmbDrAccts"
											ErrorMessage="Please select account to debit."></asp:requiredfieldvalidator></td>
									<td class="midcolora" width="18%">Account to Credit<span class="mandatory">*</span></td>
									<td class="midcolora" width="32%"><asp:dropdownlist id="cmbCrAccts" Width="200" Runat="server" AutoPostBack="False"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvCrAccts" Runat="server" Display="Dynamic" ControlToValidate="cmbCrAccts"
											ErrorMessage="Please select account to credit."></asp:requiredfieldvalidator></td>
								</tr>
							</table>
						</TD>
					</TR>
					<tr>
						<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Text="Back"></cmsb:cmsbutton></td>
						<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnPaymentBreakdown" runat="server" Visible="False" Text="Payment Breakdown"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
					</tr>
				</TABLE>
				<INPUT id="hidACTION_ON_PAYMENT" type="hidden" name="hidACTION_ON_PAYMENT" runat="server">
				<INPUT id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
				<INPUT id="hidLOB_ID" type="hidden" name="hidLOB_ID" runat="server"> <INPUT id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server">
				<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"> <INPUT id="hidPolicyRowCount" type="hidden" value="0" name="hidPolicyRowCount" runat="server">
				<INPUT id="hidVehicleRowCount" type="hidden" value="0" name="hidVehicleRowCount" runat="server">
				<INPUT id="hidCLAIM_ID" type="hidden" name="hidCLAIM_ID" runat="server"> <INPUT id="hidACTIVITY_ID" type="hidden" name="hidACTIVITY_ID" runat="server">
				<INPUT id="hidPayeeID" type="hidden" name="hidPayeeID" runat="server"> <INPUT id="hidSTATE_ID" type="hidden" name="hidSTATE_ID" runat="server">
			</form>
		</div>
	</body>
</HTML>

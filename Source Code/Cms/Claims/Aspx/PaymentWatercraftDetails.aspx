<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClaimTop" Src="/cms/cmsweb/webcontrols/ClaimTop.ascx"%>
<%@ Page language="c#" validateRequest=false Codebehind="PaymentWatercraftDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.PaymentWatercraftDetails" %>
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
			var vehicleRows=0;			
			var ctl = "ctl";
			var vehiclePaymentGridID='<%=PaymentDataGridID%>';									
			var VehiclePrefix=vehiclePaymentGridID + "__" + ctl;
			var OutstandingPrefix = "_lblOUTSTANDING";
			var PaymentPrefix = "_txtPAYMENT_AMOUNT";
			//Added for Itrack Issue 7663
			var WaterEquipCoverageRows = 0;
			var WaterEquipCoveragePrefix="WaterEquip__" + ctl;
			var WaterEquipPaymentPrefix = "txtWATER_EQUIP_PAYMENT_AMOUNT_";
			var WaterEquipOutstandingElementPrefix = "lblOUTSTANDING_WATER_EQUIP_AMOUNT_";
			//Added till here
			
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
				url = "PaymentDetailsTab.aspx?CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&";
				document.location.href = url;
				return false;
			}
			function CalculateTotal(Prefix,ElementPrefix)
			{
				if(Prefix==WaterEquipCoveragePrefix)
					rows = WaterEquipCoverageRows;
				else
					rows = vehicleRows;	
								
				var Sum = parseFloat(0);
				for(i=2;i<=rows;i++)
				{
					if(Prefix==WaterEquipCoveragePrefix)//Added for Itrack Issue 7663
						txtID = ElementPrefix + i ;
					else
						txtID = Prefix + i + ElementPrefix;												
					if(document.getElementById(txtID)==null) continue;
					document.getElementById(txtID).value = formatCurrencyWithCents(document.getElementById(txtID).value);					
					txtValue = new String(document.getElementById(txtID).value);
					txtValue = txtValue.replace(/\,/g,'');								
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
				Sum = CalculateTotal(VehiclePrefix,PaymentPrefix)+  CalculateTotal(WaterEquipCoveragePrefix,WaterEquipPaymentPrefix);
				Sum = Math.round(Sum*10000)/100;
				document.getElementById("txtTOTAL_PAYMENT").value = formatCurrencyWithCents(Sum);				
				
			}
			function GoBack()
			{
				strURL = "ActivityTab.aspx?&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&";
				this.parent.location.href = strURL;
				return false;
			}
			function ReLoadTab()
			{
				strURL = "PaymentDetailsTab.aspx?&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&";
				this.parent.location.href = strURL;
				return false;
			}			
			function Init()
			{
				//setfirstTime();	
				//findMouseIn();			
				//top.topframe.main1.mousein = false;								
				vehicleRows = parseFloat(document.getElementById("hidVehicleRowCount").value)+1;	
				//Added for Itrack Issue 7663							
				WaterEquipCoverageRows = parseFloat(document.getElementById("hidWaterEquipItemRowCount").value)+1;			
				CalculateTotalPayment();
				SetTabs();
				ApplyColor();
				ChangeColor();
				cmbPAYMENT_METHOD_Change();
				/*if(document.getElementById("hidOldData").value=="" || document.getElementById("hidOldData").value=="0")
					document.getElementById("btnPaymentBreakdown").style.display="none";*/
			}
			function CompareAllOutstandingAndPayment()
			{
				Page_ClientValidate();						
				
				for(i=2;i<=vehicleRows;i++)
				{
					txtOutstandingID = VehiclePrefix + i + OutstandingPrefix;												
					txtPaymentID = VehiclePrefix + i + PaymentPrefix
					if(!CompareOutstandingAndPayment(txtOutstandingID,txtPaymentID))
						return false;
				}	
				
				for(i=2;i<=WaterEquipCoveragePrefix;i++)
				{
					txtOutstandingID = VehiclePrefix + i + OutstandingPrefix;												
					txtPaymentID = VehiclePrefix + i + PaymentPrefix
					if(!CompareOutstandingAndPayment(txtOutstandingID,txtPaymentID))
						return false;
				}		
				CalculateTotalPayment();		
				return true;
			}
			function CompareOutstandingAndPayment(txtOutstandingID,txtPaymentID)
			{
				if(document.getElementById(txtOutstandingID)==null || document.getElementById(txtOutstandingID).value=="")
					return true;
				txtPaymentID.value = formatCurrencyWithCents(txtPaymentID.value);
				OutstandingValue = new String(document.getElementById(txtOutstandingID).innerText);
				OutstandingValue = OutstandingValue.replace(/\,/g,'');
				PaymentValue = new String(document.getElementById(txtPaymentID).value);
				PaymentValue = PaymentValue.replace(/\,/g,'');				
				if(document.getElementById(txtPaymentID).readOnly)
					return true;
					
				if(!isNaN(parseFloat(PaymentValue)))				
				{
					document.getElementById(txtPaymentID).value = formatCurrencyWithCents(document.getElementById(txtPaymentID).value);					
					//VOID LOSS CHECK Itrack #4596
					var VOID_PAID_LOSS = 240;
					var CREDIT_PAID_LOSS = 188;
					if(document.getElementById('hidACTION_ON_PAYMENT').value != VOID_PAID_LOSS
						&& document.getElementById('hidACTION_ON_PAYMENT').value != CREDIT_PAID_LOSS)
					{
						if(parseFloat(PaymentValue)>parseFloat(OutstandingValue))
						{
							alert("Payment amount cannot be greater than Outstanding amount");						
							//document.getElementById(txtPaymentID).focus();
							return false;
						}
					}
				}								
				return true;				
			}	
			function SetTabs()
			{
				if(document.getElementById('hidOldData').value != "" && document.getElementById('hidOldData').value!="0")
				{
					Url = "PayeeIndex.aspx?CLAIM_ID=" + document.getElementById('hidCLAIM_ID').value + "&ACTIVITY_ID=" + document.getElementById('hidACTIVITY_ID').value + "&CALLED_FROM=<%=CALLED_FROM_PAYMENT.ToString()%>&";					
					DrawTab(2,top.frames[1],'Payee Details',Url);
					//parent.document.getElementById('cltClaimActivityTop_pnlApplication').style.display="inline";					
					//parent.document.getElementById(parent.ActivityClientID).style.display = "inline";
					parent.document.getElementById(parent.ActivityTotalPaymentClientID).innerText = formatCurrencyWithCents(document.getElementById("txtTOTAL_PAYMENT").value);
				}
				else
				{	
					RemoveTab(2,top.frames[1]);					
					//parent.document.getElementById('cltClaimActivityTop_pnlApplication').style.display="none";
					//parent.document.getElementById(parent.ActivityClientID).style.display = "none";
				}			
			}							
			
		</script>
</HEAD>
	<body MS_POSITIONING="GridLayout" onload="Init();">
		<div id="bodyHeight" class="pageContent">
			<form id="Form1" method="post" runat="server">
<TABLE cellSpacing=0 cellPadding=0 width="100%">
  <TR>
    <TD>
<webcontrol:gridspacer id=grdspacer runat="server"></webcontrol:gridspacer></TD></TR>
<%--<tr>
	<TD id="tdClaimTop" class="pageHeader" colSpan="4">
		<webcontrol:ClaimTop id="cltClaimTop" runat="server" width="100%"></webcontrol:ClaimTop>
	</TD>
</tr>--%>
	<tr>
		<td class="headereffectCenter" colspan="4"><asp:label id="lblTitle" runat="server">Indemnity Payment Details</asp:label></td>
	</tr>
	<tr>
		<td align="center" colspan="4" class="midcolorc"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False" EnableViewState="False"></asp:label></td>
	</tr>
  <TR id=trBody runat="server">
    <TD colspan="4">
				<table cellSpacing="0" cellPadding="0" WIDTH="100%">
								
								
								<%--<tr>
									<TD class="midcolora" width="18%"><asp:label id="capACTIVITY_DATE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblACTIVITY_DATE" runat="server"></asp:label></TD>
									<TD class="midcolora"  colspan="2"></TD>									
								</tr>--%>								
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4">Boat Level Payments</TD>
								</tr>
								<tr>
									<td class="midcolora" colspan="4">
										<asp:datagrid id="dgPayment" runat="server" AutoGenerateColumns="False"
											Width="100%">
											<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
											<ItemStyle CssClass="midcolora"></ItemStyle>
											<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
											<Columns>																								
												<%--<asp:BoundColumn DataField="COV_DESC" HeaderText="Coverage"></asp:BoundColumn>--%>
												<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="30%">
													<ItemTemplate>
														<asp:Label ID="lblCOV_DESC" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
														</asp:Label>
														<asp:Label ID="lblBOAT_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.VEHICLE_ID") %>'></asp:Label>														
														<asp:Label ID="lblRESERVE_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RESERVE_ID") %>'></asp:Label>														
														<asp:Label ID="lblPAYMENT_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.PAYMENT_ID") %>'></asp:Label>
														<asp:Label ID="lblCOV_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'></asp:Label>
														<asp:Label ID="lblACTUAL_RISK_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_TYPE" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_TYPE") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>												
												<asp:TemplateColumn HeaderText="Limit" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:Label ID="lblLIMIT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>	
												<%--<asp:TemplateColumn HeaderText="Attachment Point" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:Label ID="lblATTACHMENT_POINT" Runat="server" Text='<%#  DataBinder.Eval(Container, "DataItem.ATTACHMENT_POINT","{0:,#,###}")  >'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>--%>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>												
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:Label ID="lblOUTSTANDING" Runat="server" ></asp:Label>														
													</ItemTemplate>
												</asp:TemplateColumn>	
												<asp:TemplateColumn HeaderText="RI Reserve"   ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:Label ID="lblRI_RESERVE" Runat="server" ></asp:Label>														
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reinsurance Carrier"   ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:Label ID="lblREINSURANCECARRIER" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REINSURANCECARRIER","{0:,#,###}") %>'>></asp:Label>																												
													</ItemTemplate>
												</asp:TemplateColumn>	
												<asp:TemplateColumn HeaderText="Payment Amount" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:TextBox ID="txtPAYMENT_AMOUNT"  CssClass="INPUTCURRENCY" Runat="server" MaxLength="14"></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revPAYMENT_AMOUNT" ControlToValidate="txtPAYMENT_AMOUNT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngPAYMENT_AMOUNT" MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server" Display="Dynamic" ControlToValidate="txtPAYMENT_AMOUNT" Enabled="False"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>												
											</Columns>
										</asp:datagrid></td>
								</tr>					
								<tr id="trWatercraftEquipmentCoveragesRow" runat="server">
									<td class="headerEffectSystemParams" colSpan="4"><asp:label id="lblWatercraftEquipmentCoverages" Runat="server">Water Equipment Coverages</asp:label></td>
								</tr>
								<tr id="trWatercraftEquipmentCoveragesGridRow" runat="server">
									<td  colSpan="4">
										<asp:PlaceHolder ID="plcWatercraftEquipmentCovg" Runat="server"></asp:PlaceHolder>
									</td>
								</tr>
								<tr>
									<td colspan="4">&nbsp;</td>
								</tr>					
								<tr class="midcolora">
									<TD class="midcolora" colSpan="1"><asp:textbox class="midcolora" id="txtGrossTotal" Font-Bold="True" Runat="server" ReadOnly="True" BorderWidth="0" Text="Total"
														 BorderStyle="None"></asp:textbox></TD>
									<td colspan="3">
										<table width="100%" border="0"  >
											<tr>
												<td width="43%"></td>
												<TD width="41%"><asp:textbox class="midcolora" id="txtTOTAL_OUTSTANDING" Runat="server" ReadOnly="True" BorderWidth="0"  size="15"
														BorderStyle="None"></asp:textbox></TD>																								
												<TD ><asp:textbox  id="txtTOTAL_PAYMENT" class="midcolora" Runat="server" ReadOnly="True" BorderWidth="0" size="15"
														BorderStyle="None"></asp:textbox></TD>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td colspan="4">&nbsp;</td>
								</tr>																		
								<tr>
									<td class="midcolora" width="18%"><asp:Label ID="capCLAIM_DESCRIPTION" Runat="server"></asp:Label></td>
									<td class="midcolora" colSpan="3"><asp:textbox id="txtCLAIM_DESCRIPTION" Runat="server" Columns="40" Rows="4" TextMode="MultiLine" onkeypress="MaxLength(this,255);"></asp:textbox><br>
										<asp:customvalidator Runat="server" id="csvCLAIM_DESCRIPTION"  Display="Dynamic" ControlToValidate="txtCLAIM_DESCRIPTION" ClientValidationFunction="ValidateLength"></asp:customvalidator></td>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4" width="10">&nbsp;</TD>
								</tr>			
								<%--<tr>--%>
									<%--<td class="midcolora" width="18%"><asp:Label ID="capPAYEE" Runat="server"></asp:Label><span class="mandatory">*</span></td>
									<td class="midcolora" width="32%">
										<asp:ListBox ID="cmbPAYEE" Runat="server" SelectionMode="Multiple"></asp:ListBox><br>
										<asp:RequiredFieldValidator ID="rfvPAYEE" Display="Dynamic" ControlToValidate="cmbPAYEE" ErrorMessage="select" 
											Runat="server"></asp:RequiredFieldValidator>
									</td>--%>
									<%--<td class="midcolora" width="18%"><asp:Label ID="capACTION_ON_PAYMENT" Runat="server"></asp:Label></td>
									<td class="midcolora" width="32%">
										<asp:DropDownList ID="cmbACTION_ON_PAYMENT" Runat="server"></asp:DropDownList>
									</td>
									<td  class="midcolora" colspan="2">&nbsp;</td>
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
									<td class="midcolora" width="32%">
										<asp:dropdownlist id="cmbDrAccts" Runat="server" AutoPostBack="False" Width="350"></asp:dropdownlist>
										<br>
										<asp:RequiredFieldValidator Runat="server" ID="rfvDrAccts" ControlToValidate="cmbDrAccts" Display="Dynamic"
											ErrorMessage="Please select account to debit."></asp:RequiredFieldValidator>
									</td>
									<td class="midcolora" width="18%">Account to Credit<span class="mandatory">*</span></td>
									<td class="midcolora" width="32%">
										<asp:dropdownlist id="cmbCrAccts" Runat="server" AutoPostBack="False" Width="350"></asp:dropdownlist>
										<br>
										<asp:RequiredFieldValidator Runat="server" ID="rfvCrAccts" ControlToValidate="cmbCrAccts" Display="Dynamic"
											ErrorMessage="Please select account to credit."></asp:RequiredFieldValidator>
									</td>
								</tr>												
							</table></TD></TR>
							<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Text="Back"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="2">
											<cmsb:cmsbutton class="clsButton" id="btnPaymentBreakdown" visible="False" runat="server" Text="Payment Breakdown"></cmsb:cmsbutton>
											<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
									</td>
								</tr>	
							</TABLE>
						<INPUT id="hidACTION_ON_PAYMENT" type="hidden" name="hidACTION_ON_PAYMENT" runat="server">
						<INPUT id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server">
						<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
						<INPUT id="hidLOB_ID" type="hidden" name="hidLOB_ID" runat="server">
						<INPUT id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server">
						<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server">													
						<INPUT id="hidVehicleRowCount" type="hidden" name="hidVehicleRowCount" runat="server" value="0"> 										
						<INPUT id="hidCLAIM_ID" type="hidden" name="hidCLAIM_ID" runat="server"> 										
						<INPUT id="hidACTIVITY_ID" type="hidden" name="hidACTIVITY_ID" runat="server"> 	
						<INPUT id="hidSTATE_ID" type="hidden" name="hidSTATE_ID" runat="server"><!--Done for Itrack Issue 6299 on 26 Aug 09-->									
						<%--<INPUT id="hidPayeeID" type="hidden" name="hidPayeeID" runat="server" >--%>
						<INPUT id="hidWaterEquipItemRowCount" type="hidden" value="0" name="hidWaterEquipItemRowCount" runat="server">
						<INPUT id="hidWaterEquipCovgID" type="hidden" value="" name="hidWaterEquipCovgID" runat="server">
						<INPUT id="hidWaterEquipRsvID" type="hidden" value="" name="hidWaterEquipRsvID" runat="server">
						<INPUT id="hidWaterEquipPmntID" type="hidden" value="" name="hidWaterEquipPmntID" runat="server">
						<INPUT id="hidEquip_ACTUAL_RISK_ID" type="hidden" value="" name="hidEquip_ACTUAL_RISK_ID" runat="server">
						<INPUT id="hidEquip_ACTUAL_RISK_TYPE" type="hidden" value="" name="hidEquip_ACTUAL_RISK_TYPE" runat="server">
						<INPUT id="hidEquipDWELLING_ID" type="hidden" value="" name="hidEquipDWELLING_ID" runat="server">
			</form>
		</div>
	</body>
</HTML>

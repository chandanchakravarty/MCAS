<%@ Register TagPrefix="webcontrol" TagName="ClaimActivityTop" Src="/cms/cmsweb/webcontrols/ClaimActivityTop.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClaimTop" Src="/cms/cmsweb/webcontrols/ClaimTop.ascx"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Page language="c#" validateRequest=false Codebehind="ReserveWatercraftDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.ReserveWatercraftDetails" %>
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
			var DummyPolicyRows = 0;			
			var ctl = "ctl";
			var vehicleCoverageGridID='<%=VehicleCoverageGridID%>';									
			var VehiclePrefix=vehicleCoverageGridID + "__" + ctl;
			var CoveragePrefix="dgCoverages__" + ctl;
			var ReserveElementPrefix = "_txtRI_RESERVE";
			var LimitElementPrefix = "_lblLIMIT";			
			var OutstandingElementPrefix = "_txtOUTSTANDING";
			//Added for Itrack Issue 7663
			var WaterEquipCoverageRows = 0;
			var WaterEquipCoveragePrefix="WaterEquip__" + ctl;
			var WaterEquipOutstandingElementPrefix = "txtOUT_STANDING_WATER_EQUIP_";
			var WaterEquipReserveElementPrefix = "txtRI_RESERVE_WATER_EQUIP_";
			//Added till here
			var ActivityClientID;
			var ActivityTotalPaymentClientID;
		
			function OpenWindow()
			{
				url = "ReserveBreakDownIndex.aspx?CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&";
				document.location.href = url;
				return false;
			}
			function Init()
			{
				vehicleRows = parseFloat(document.getElementById("hidVehicleRowCount").value)+1;				
				DummyPolicyRows = parseFloat(document.getElementById("hidDummyPolicyCoverageRowCount").value)+1;
				//Added for Itrack Issue 7663							
				WaterEquipCoverageRows = parseFloat(document.getElementById("hidWaterEquipItemRowCount").value)+1;
				CalculateTotalOutstanding(true);
				CalculateTotalRIReserve(true);
				//Added for Itrack Issue 7663
				/*CalculateTotalWaterEquipOutstanding(true);
				CalculateTotalWaterEquipRI_Reserve(true);*/	
				SetMenu();			
				ApplyColor();
				ChangeColor();
				setfirstTime();	
				findMouseIn();			
				top.topframe.main1.mousein = false;		
				ActivityClientID = '<%=ActivityClientID%>';
				ActivityTotalPaymentClientID = '<%=ActivityTotalPaymentClientID%>';
				if(document.getElementById(ActivityTotalPaymentClientID))
					document.getElementById(ActivityTotalPaymentClientID).innerText = formatCurrencyWithCents(document.getElementById("txtTOTAL_OUTSTANDING").value);
			}
			function SetMenu()
			{
				//if(document.getElementById("hidOldData").value=="" || document.getElementById("hidOldData").value=="0")
				if(document.getElementById("hidTRANSACTION_ID").value >= "0" && '<%=IS_RESERVE_ADDED%>' == "1")
				{
					//document.getElementById("btnReserveBreakdown").style.display="none";
					//top.topframe.enableMenu('2,2,3'); //Done for Itrack Issue 6752 on 28 Jan 2010
					top.topframe.enableMenu('2,2,2');
				}
				else
				{
					top.topframe.disableMenu('2,2,3');
					top.topframe.disableMenu('2,2,2');
				}
			}
			function GoToClaimDetailPage()
			{
				//Done for Itrack Issue 6343 on 23 Sept 2010
				if('<%=IS_RESERVE_ADDED%>' == "1")
				{
					top.botframe.location.href = "ActivityTab.aspx?&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&";
					return false;
				}
				else
				{
					top.botframe.location.href = "ClaimsTab.aspx?Customer_ID=" + document.getElementById("hidCustomerID").value + "&Policy_ID=" + document.getElementById("hidPolicyID").value + "&Policy_version_ID=" + document.getElementById("hidPolicyVersionID").value + "&Claim_ID=" + document.getElementById("hidCLAIM_ID").value + "&LOB_ID=" + document.getElementById("hidLOB_ID").value;
					return false;
				}
			}
			function ShowClaimDetail()
			{
				top.botframe.location.href = "ClaimsTab.aspx?Customer_ID=" + document.getElementById("hidCustomerID").value + "&Policy_ID=" + document.getElementById("hidPolicyID").value + "&Policy_version_ID=" + document.getElementById("hidPolicyVersionID").value + "&Claim_ID=" + document.getElementById("hidCLAIM_ID").value + "&LOB_ID=" + document.getElementById("hidLOB_ID").value;
				return false;
			}
			function CalculateTotal(Prefix,ElementPrefix)
			{
				if((document.getElementById("hidCustomerID").value == "0"  || document.getElementById("hidCustomerID").value == "") && (document.getElementById("hidPolicyID").value == "0"  || document.getElementById("hidPolicyID").value == ""))
					rows = DummyPolicyRows;
				else	
					rows = vehicleRows;					
					
				var Sum = parseFloat(0);
				for(var i=2;i<=rows;i++)
				{
					if(Prefix==WaterEquipCoveragePrefix)//Added for Itrack Issue 7663
						txtID = ElementPrefix + i ;
					else
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
			function CalculateTotalOutstanding(flag)
			{
				var Sum = parseFloat(0);
				if((document.getElementById("hidCustomerID").value == "0"  || document.getElementById("hidCustomerID").value == "") && (document.getElementById("hidPolicyID").value == "0"  || document.getElementById("hidPolicyID").value == ""))
					Sum = CalculateTotal(CoveragePrefix,"_txtOUTSTANDING");
				else
					Sum = CalculateTotal(VehiclePrefix,"_txtOUTSTANDING")+  CalculateTotal(WaterEquipCoveragePrefix,WaterEquipOutstandingElementPrefix);//Added for Itrack Issue 7663
				/*if(flag)
					document.getElementById("hidOldTotalOutstanding").value = Sum;
				*/
				Sum = Math.round(Sum*10000)/100;
				document.getElementById("txtTOTAL_OUTSTANDING").value = formatCurrencyWithCents(Sum);				
								
			}
			function CalculateTotalRIReserve(flag)
			{	
				var Sum = parseFloat(0);
				Sum = CalculateTotal(VehiclePrefix,"_txtRI_RESERVE")+  CalculateTotal(WaterEquipCoveragePrefix,WaterEquipReserveElementPrefix);//Added for Itrack Issue 7663;
				/*if(flag)				
					document.getElementById("hidOldTotalRiReserve").value = Sum;
				*/
				Sum = Math.round(Sum*10000)/100;				
				document.getElementById("txtTOTAL_RI_RESERVE").value = formatCurrencyWithCents(Sum);								
				
			}	
			//Added for Itrack Issue 7663
			function CalculateTotalWaterEquipOutstanding(flag)
			{
				var Sum = parseFloat(0);
				if((document.getElementById("hidCustomerID").value == "0"  || document.getElementById("hidCustomerID").value == "") && (document.getElementById("hidPolicyID").value == "0"  || document.getElementById("hidPolicyID").value == ""))
					Sum = CalculateTotal(CoveragePrefix,"_txtOUTSTANDING");
				else
					Sum = CalculateTotal(VehiclePrefix,"_txtOUTSTANDING")+  CalculateTotal(WaterEquipCoveragePrefix,WaterEquipOutstandingElementPrefix);//Added for Itrack Issue 7663
				
				Sum = Math.round(Sum*10000)/100;
				document.getElementById("txtTOTAL_OUTSTANDING").value = formatCurrencyWithCents(Sum);				
								
			}
			function CalculateTotalWaterEquipRI_Reserve(flag)
			{	
				var Sum = parseFloat(0);
				Sum = CalculateTotal(VehiclePrefix,"_txtRI_RESERVE")+  CalculateTotal(WaterEquipCoveragePrefix,WaterEquipReserveElementPrefix);//Added for Itrack Issue 7663;
				
				Sum = Math.round(Sum*10000)/100;				
				document.getElementById("txtTOTAL_RI_RESERVE").value = formatCurrencyWithCents(Sum);								
				
			}	
			function GoToActivity()
			{
				SetMenu();
				strURL = "ActivityTab.aspx?&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&";
				this.document.location.href = strURL;
				return false;
			}	
			function CompareLimitAndOutstanding(lblLimitClientID,txtOustandingClientID)
			{
				var NewValue;				
				if(lblLimitClientID==null || txtOustandingClientID==null) return true;
				txtOustandingClientID.value = formatCurrencyWithCents(txtOustandingClientID.value);
				txtValue = new String(txtOustandingClientID.value);
				txtValue = replaceAll(txtValue,",","");
				LimitValue = new String(lblLimitClientID.innerText);												
				if(LimitValue!="")
				{
					if(typeof(LimitValue.split('/')[1])!='undefined')
					{
						LimitValue = LimitValue.split('/')[1];					
						LimitValue = replaceAll(LimitValue,",","");						
						NewValue = (parseFloat(LimitValue) * 1000);						
					}
					else
						NewValue = replaceAll(LimitValue,",","");			
					
					/*if(NewValue!="" && !isNaN(NewValue) && txtValue!="" && !isNaN(txtValue) && parseFloat(txtValue)>parseFloat(NewValue))
					{
						alert("Outstanding amount cannot be greater than limit amount");
						return false;
					}*/
				}				
				CalculateTotalOutstanding(false);
				CalculateTotalRIReserve(false);
				//Added for Itrack Issue 7663
				/*CalculateTotalWaterEquipOutstanding(false);
				CalculateTotalWaterEquipRI_Reserve(false);*/
				return true;					
			}
			function CompareAllLimitAndOutstanding()
			{
				Page_ClientValidate();
				var Prefix,rows;
				var FinalResult = true;
				var ReturnValue = true;
				/*Prefix = PolicyPrefix;
				rows = policyRows;
				
				for(var ctr=2;ctr<=rows;ctr++)
				{
					txtOutstandingID = document.getElementById(Prefix + ctr + OutstandingElementPrefix);
					lblLimitID = document.getElementById(Prefix + ctr + LimitElementPrefix);
					ReturnValue = CompareLimitAndOutstanding(lblLimitID,txtOutstandingID);
					if(!ReturnValue)
					{
						FinalResult = false;
						return (Page_IsValid && FinalResult);
					}
				} */				
				Prefix = VehiclePrefix;
				rows = vehicleRows;
				
				FinalResult = true;
				ReturnValue = true;
				for(var ctr=2;ctr<=rows;ctr++)
				{
					txtOutstandingID = document.getElementById(Prefix + ctr + OutstandingElementPrefix);
					lblLimitID = document.getElementById(Prefix + ctr + LimitElementPrefix);
					ReturnValue = CompareLimitAndOutstanding(lblLimitID,txtOutstandingID);
					if(!ReturnValue)
					{
						FinalResult = false;
						return (Page_IsValid && FinalResult);
					}
				} 
				
				//Added for Itrack Issue 7663
				Prefix=WaterEquipCoveragePrefix;
				rows = WaterEquipCoverageRows;
								
				FinalResult = true;
				ReturnValue = true;
				for(var ctr=2;ctr<=rows;ctr++)
				{
					txtOutstandingID = document.getElementById(Prefix + ctr + OutstandingElementPrefix);
					lblLimitID = document.getElementById(Prefix + ctr + LimitElementPrefix);
					ReturnValue = CompareLimitAndOutstanding(lblLimitID,txtOutstandingID);
					if(!ReturnValue)
					{
						FinalResult = false;
						return (Page_IsValid && FinalResult);
					}
				} 
				return Page_IsValid;
			}	
		</script>
	</HEAD>
	<body onload="Init();" MS_POSITIONING="GridLayout">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<div class="pageContent" id="bodyHeight">
			<form id="Form1" method="post" runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="95%" border="0">
					<tr>
						<TD class="pageHeader" id="tdClaimTop" colSpan="4"><webcontrol:claimtop id="cltClaimTop" runat="server" width="100%"></webcontrol:claimtop></TD>
					</tr>
					<tr>
						<td id="tdWorkflow" class="pageHeader" colspan="4">
							<webcontrol:ClaimActivityTop id="cltClaimActivityTop" runat="server"></webcontrol:ClaimActivityTop>
						</td>
					</tr>
					<tr>
						<TD class="pageHeader" colSpan="4">&nbsp;</TD>
					</tr>
					<tr>
						<td class="headereffectCenter" colSpan="4"><asp:label id="lblTitle" runat="server">Reserves</asp:label></td>
					</tr>
					<tr>
						<td class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" runat="server" EnableViewState="False" Visible="False" CssClass="errmsg"></asp:label></td>
					</tr>
					<TR id="trBody" runat="server">
						<TD colspan="4">
							<table cellSpacing="0" cellPadding="0" width="100%">
								<%--<tr>
									<TD class="midcolora" width="18%"><asp:label id="capACTIVITY_DATE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblACTIVITY_DATE" runat="server"></asp:label></TD>
									<TD class="midcolora" colSpan="2"></TD>
								</tr>--%>
								<tr id="trVehicleRow" runat="server">
									<td class="headerEffectSystemParams"><asp:label id="Label8" Runat="server">Boat Level Coverages</asp:label></td>
								</tr>
								<tr id="trVehicleGridRow" runat="server">
									<td class="midcolora" colSpan="4">
										<asp:datagrid id="dgCoverages" runat="server" AutoGenerateColumns="False" Width="100%">
											<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
											<ItemStyle CssClass="midcolora"></ItemStyle>
											<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
											<Columns>
												<%--<asp:BoundColumn DataField="COV_DESC" HeaderText="Coverage" ItemStyle-Width="15%"></asp:BoundColumn>--%>
												<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="25%">
													<ItemTemplate>
														<asp:Label ID="lblCOV_DESC" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
														</asp:Label>
														<asp:Label ID="lblBOAT_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.VEHICLE_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblRESERVE_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RESERVE_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblCOV_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_TYPE" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_TYPE") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Limit" ItemStyle-CssClass="midcolorr" ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:Label ID="lblLIMIT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<%--<asp:TemplateColumn HeaderText="Attachment Point"  ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:TextBox ID="txtATTACHMENT_POINT" Runat="server" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.ATTACHMENT_POINT","{0:,#,###}") '>></asp:TextBox>
														<asp:RangeValidator ID="rngATTACHMENT_POINT" MinimumValue="1" MaximumValue="9999999999" Type="Currency"
															Runat="server" Display="Dynamic" ControlToValidate="txtATTACHMENT_POINT"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>--%>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-CssClass="midcolorr" ItemStyle-Width="15%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE" Runat="server"  ></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:TextBox ID="txtOUTSTANDING"  CssClass="INPUTCURRENCY" Runat="server" size="20" MaxLength="10" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revOUTSTANDING" ControlToValidate="txtOUTSTANDING" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngOUTSTANDING" MinimumValue="1" MaximumValue="9999999999" Type="Currency" Runat="server" Display="Dynamic" ControlToValidate="txtOUTSTANDING" Enabled="False"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:TextBox ID="txtRI_RESERVE" CssClass="INPUTCURRENCY" Runat="server" size="20" MaxLength="10" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revRI_RESERVE" ControlToValidate="txtRI_RESERVE" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngRI_RESERVE" MinimumValue="1" MaximumValue="9999999999" Type="Currency" Runat="server" Display="Dynamic" ControlToValidate="txtRI_RESERVE" Enabled="False"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reinsurance Carrier" ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:DropDownList ID="cmbREINSURANCE_CARRIER" Runat="server"></asp:DropDownList>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>
								<!--Added for Itrack Issue 7663-->
								<tr id="trWatercraftEquipmentCoveragesRow" runat="server">
									<td class="headerEffectSystemParams" colSpan="4"><asp:label id="lblWatercraftEquipmentCoverages" Runat="server">Watercraft Equipment Coverages</asp:label></td>
								</tr>
								<tr id="trWatercraftEquipmentCoveragesGridRow" runat="server">
									<td colSpan="4">
										<asp:PlaceHolder ID="plcWatercraftEquipmentCovg" Runat="server"></asp:PlaceHolder>
									</td>
								</tr>
								
								<tr>
									<td colspan="4">&nbsp;</td>
								</tr>
								<tr class="midcolora" id="trTotalRow" runat="server">
									<TD class="midcolora">
										<asp:textbox class="midcolora" id="txtGrossTotal" Font-Bold="True" Runat="server" ReadOnly="True"
											BorderWidth="0" Text="Gross Total" BorderStyle="None"></asp:textbox>
									</TD>
									<td colspan="3">
										<table width="100%" border="0">
											<tr>
												<td width="55%">&nbsp;</td>
												<TD width="20%">&nbsp;<asp:textbox class="midcolora" id="txtTOTAL_OUTSTANDING" Runat="server" ReadOnly="True" BorderWidth="0"
														size="20" BorderStyle="None"></asp:textbox></TD>
												<TD><asp:textbox id="txtTOTAL_RI_RESERVE" class="midcolora" Runat="server" ReadOnly="True" BorderWidth="0"
														size="20" BorderStyle="None"></asp:textbox></TD>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4">Estimation</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capRECOVERY" Runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtRECOVERY" CssClass="INPUTCURRENCY"  Runat="server" MaxLength="10"></asp:textbox><br>
									<asp:RegularExpressionValidator ID="revRECOVERY" ControlToValidate="txtRECOVERY" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
										<asp:rangevalidator id="rngRECOVERY" Runat="server" ControlToValidate="txtRECOVERY" Display="Dynamic" Type="Currency" MaximumValue="9999999999" MinimumValue="1" Enabled="False"></asp:rangevalidator></TD>
									<%--<TD class="midcolora" width="18%"><asp:label id="capPAYMENT_AMOUNT" Runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPAYMENT_AMOUNT" Runat="server" MaxLength="10"></asp:textbox><br>
										<asp:rangevalidator id="rngPAYMENT_AMOUNT" Runat="server" ControlToValidate="txtPAYMENT_AMOUNT" Display="Dynamic"
											Type="Currency" MaximumValue="9999999999" MinimumValue="1"></asp:rangevalidator></TD>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4">Expenses</TD>
								</tr>
								<tr>--%>
									<TD class="midcolora" width="18%"><asp:label id="capEXPENSES" Runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEXPENSES"  CssClass="INPUTCURRENCY" Runat="server" MaxLength="10"></asp:textbox><br>
									<asp:RegularExpressionValidator ID="revEXPENSES" ControlToValidate="txtEXPENSES" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
										<asp:rangevalidator id="rngEXPENSES" Runat="server" ControlToValidate="txtEXPENSES" Display="Dynamic" Type="Currency" MaximumValue="9999999999" MinimumValue="1" Enabled="False"></asp:rangevalidator></TD>
									<%--<td class="midcolora" colSpan="2"></td>--%>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" width="10" colSpan="4">&nbsp;</TD>
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
							</table>
						</TD>
					</TR>
					<tr>
						<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Text="Back"></cmsb:cmsbutton></td>
						<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReserveBreakdown" runat="server" Visible="False" Text="Reserve Breakdown"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
					</tr>
				</TABLE>
				<INPUT id="hidACTION_ON_PAYMENT" type="hidden" name="hidACTION_ON_PAYMENT" runat="server">
				<INPUT id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
				<INPUT id="hidLOB_ID" type="hidden" name="hidLOB_ID" runat="server"> <INPUT id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server">
				<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server">
				<%--<INPUT id="hidPolicyRowCount" type="hidden" name="hidPolicyRowCount" runat="server" value="0">--%>
				<INPUT id="hidPolicyRowCount" type="hidden" name="hidPolicyRowCount" runat="server" value="0">
				<INPUT id="hidDummyPolicyCoverageRowCount" type="hidden" name="hidDummyPolicyCoverageRowCount" runat="server" value="0">
				<INPUT id="hidCLAIM_ID" type="hidden" name="hidCLAIM_ID" runat="server"> <INPUT id="hidACTIVITY_ID" type="hidden" name="hidACTIVITY_ID" runat="server">
				<INPUT id="hidVehicleRowCount" type="hidden" name="hidVehicleRowCount" runat="server" value="0">
				<INPUT id="hidNewReserve" type="hidden" name="hidNewReserve" runat="server" value="0">
				<INPUT id="hidOldTotalOutstanding" type="hidden" name="hidOldTotalOutstanding" runat="server" value="0">
				<INPUT id="hidOldTotalRiReserve" type="hidden" name="hidOldTotalRiReserve" runat="server" value="0">
				<INPUT id="hidTRANSACTION_ID" type="hidden" name="hidTRANSACTION_ID" runat="server" value="0">
				<INPUT id="hidTRANSACTION_CATEGORY" type="hidden" name="hidTRANSACTION_CATEGORY" runat="server">
				<INPUT id="hidSTATE_ID" type="hidden" name="hidSTATE_ID" runat="server"><!--Done for Itrack Issue 6299 on 26 Aug 09-->
				<INPUT id="hidEquip_ACTUAL_RISK_ID" type="hidden" value="" name="hidEquip_ACTUAL_RISK_ID" runat="server">
				<INPUT id="hidEquip_ACTUAL_RISK_TYPE" type="hidden" value="" name="hidEquip_ACTUAL_RISK_TYPE" runat="server">
				<INPUT id="hidEquipDWELLING_ID" type="hidden" value="" name="hidEquipDWELLING_ID" runat="server">
				<INPUT id="hidWaterEquipItemRowCount" type="hidden" value="0" name="hidWaterEquipItemRowCount" runat="server">
				<INPUT id="hidWaterEquipCovgID" type="hidden" value="" name="hidWaterEquipCovgID" runat="server">
				<INPUT id="hidWaterEquipRsvID" type="hidden" value="" name="hidWaterEquipRsvID" runat="server">
				<!--Added for Itrack Issue 7663-->
				<%--<INPUT id="hidWaterEquipItemRowCount" type="hidden" value="0" name="hidWaterEquipItemRowCount" runat="server">
				<INPUT id="hidWaterEquipCovgID" type="hidden" value="" name="hidWaterEquipCovgID" runat="server">
				<INPUT id="hidWaterEquipRsvID" type="hidden" value="" name="hidWaterEquipRsvID" runat="server">--%>
			</form>
		</div>
	</body>
</HTML>

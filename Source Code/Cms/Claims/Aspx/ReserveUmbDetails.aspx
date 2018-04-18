<%@ Register TagPrefix="webcontrol" TagName="ClaimActivityTop" Src="/cms/cmsweb/webcontrols/ClaimActivityTop.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClaimTop" Src="/cms/cmsweb/webcontrols/ClaimTop.ascx"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Page language="c#" Codebehind="ReserveUmbDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.ReserveUmbDetails" %>
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
			var CoverageGridID='<%=CoverageGridID%>';									
			var VehiclePrefix=CoverageGridID + "__" + ctl;
			var CoveragePrefix="dgCoverages__" + ctl;
			var ReserveElementPrefix = "_txtRI_RESERVE";
			var LimitElementPrefix = "_lblLIMIT";			
			var OutstandingElementPrefix = "_txtOUTSTANDING";
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
				CalculateTotalOutstanding(true);
				CalculateTotalRIReserve(true);	
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
				if(Prefix==CoveragePrefix)
					rows = DummyPolicyRows;
				else 
					rows = vehicleRows;					
				var Sum = parseFloat(0);
				for(var i=2;i<=rows;i++)
				{
					txtID = Prefix + i + ElementPrefix;					
					if(document.getElementById(txtID)==null) continue;
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
					Sum = CalculateTotal(VehiclePrefix,"_txtOUTSTANDING");
				/*if(flag)
					document.getElementById("hidOldTotalOutstanding").value = Sum;
				*/
				Sum = Math.round(Sum*10000)/100;
				document.getElementById("txtTOTAL_OUTSTANDING").value = formatCurrencyWithCents(Sum);				
								
			}
			function CalculateTotalRIReserve(flag)
			{	
				var Sum = parseFloat(0);
				Sum = CalculateTotal(VehiclePrefix,"_txtRI_RESERVE");
				/*if(flag)				
					document.getElementById("hidOldTotalRiReserve").value = Sum;
				*/
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
					<tr>
						<TD class="midcolora" width="50%"><asp:label id="capPOLICY_LIMITS" runat="server"></asp:label></TD>
						<TD class="midcolora">
							<asp:dropdownlist id="cmbPOLICY_LIMITS" runat="server"></asp:dropdownlist>
						</TD>
						<td colspan="2" class="midcolora"></td>
					</tr>
					<tr>
						<TD class="midcolora" width="50%"><asp:label id="capRETENTION_LIMITS" runat="server"></asp:label></TD>
						<TD class="midcolora"><asp:textbox id="txtRETENTION_LIMITS" runat="server" maxlength="3" size="7" CssClass="INPUTCURRENCY"></asp:textbox><BR>
						<asp:RegularExpressionValidator ID="revRETENTION_LIMITS" ControlToValidate="txtRETENTION_LIMITS" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
							<asp:RangeValidator id="rngRETENTION_LIMITS" runat="server" ControlToValidate="txtRETENTION_LIMITS" Type="Double" MaximumValue="250" MinimumValue="1" Display="Dynamic"></asp:RangeValidator></TD>
						<td colspan="2" class="midcolora"></td>
					</tr>
					<TR id="trBody" runat="server">
						<TD colspan="4">
							<table cellSpacing="0" cellPadding="0" width="100%">
								<tr id="trVehicleRow" runat="server">
									<td class="headerEffectSystemParams"><asp:label id="Label8" Runat="server">Coverages</asp:label></td>
								</tr>
								<tr id="trVehicleGridRow" runat="server">
									<td class="midcolora" colSpan="4">
										<asp:datagrid id="dgCoverages" runat="server" AutoGenerateColumns="False" Width="100%">
											<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
											<ItemStyle CssClass="midcolora"></ItemStyle>
											<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="25%">
													<ItemTemplate>
														<asp:Label ID="lblCOV_DESC" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
														</asp:Label>
														<asp:Label ID="lblRESERVE_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RESERVE_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblCOV_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:TextBox ID="txtOUTSTANDING"  CssClass="INPUTCURRENCY" Runat="server" size="15" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revOUTSTANDING" ControlToValidate="txtOUTSTANDING" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngOUTSTANDING" MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server" Display="Dynamic" ControlToValidate="txtOUTSTANDING" Enabled="False"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:TextBox ID="txtRI_RESERVE" CssClass="INPUTCURRENCY" Runat="server" size="15" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revRI_RESERVE" ControlToValidate="txtRI_RESERVE" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngRI_RESERVE" MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server" Display="Dynamic" ControlToValidate="txtRI_RESERVE" Enabled="False"></asp:RangeValidator>
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
												<td width="49%">&nbsp;</td>
												<TD width="17%">&nbsp;<asp:textbox class="midcolora" id="txtTOTAL_OUTSTANDING" Runat="server" ReadOnly="True" BorderWidth="0"
														size="15" BorderStyle="None"></asp:textbox></TD>
												<TD><asp:textbox id="txtTOTAL_RI_RESERVE" class="midcolora" Runat="server" ReadOnly="True" BorderWidth="0"
														size="15" BorderStyle="None"></asp:textbox></TD>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4">Estimation</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capRECOVERY" Runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtRECOVERY" CssClass="INPUTCURRENCY" Runat="server" MaxLength="14"></asp:textbox><br>
									<asp:RegularExpressionValidator ID="revRECOVERY" ControlToValidate="txtRECOVERY" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
										<asp:rangevalidator id="rngRECOVERY" Runat="server" ControlToValidate="txtRECOVERY" Display="Dynamic" Type="Currency" MaximumValue="99999999999999" MinimumValue="1" Enabled="False"></asp:rangevalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capEXPENSES" Runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEXPENSES" CssClass="INPUTCURRENCY" Runat="server" MaxLength="14"></asp:textbox><br>
									<asp:RegularExpressionValidator ID="revEXPENSES" ControlToValidate="txtEXPENSES" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
										<asp:rangevalidator id="rngEXPENSES" Runat="server" ControlToValidate="txtEXPENSES" Display="Dynamic" Type="Currency" MaximumValue="99999999999999" MinimumValue="1" Enabled="False"></asp:rangevalidator></TD>
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
				<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"> <INPUT id="hidCLAIM_ID" type="hidden" name="hidCLAIM_ID" runat="server">
				<INPUT id="hidPolicyRowCount" type="hidden" name="hidPolicyRowCount" runat="server" value="0">
				<INPUT id="hidDummyPolicyCoverageRowCount" type="hidden" name="hidDummyPolicyCoverageRowCount" runat="server" value="0">
				<INPUT id="hidACTIVITY_ID" type="hidden" name="hidACTIVITY_ID" runat="server"> <INPUT id="hidVehicleRowCount" type="hidden" name="hidVehicleRowCount" runat="server" value="0">
				<INPUT id="hidNewReserve" type="hidden" name="hidNewReserve" runat="server" value="0">
				<INPUT id="hidOldTotalOutstanding" type="hidden" name="hidOldTotalOutstanding" runat="server" value="0">
				<INPUT id="hidOldTotalRiReserve" type="hidden" name="hidOldTotalRiReserve" runat="server" value="0">
				<INPUT id="hidTRANSACTION_ID" type="hidden" name="hidTRANSACTION_ID" runat="server" value="0">
				<INPUT id="hidTRANSACTION_CATEGORY" type="hidden" name="hidTRANSACTION_CATEGORY" runat="server">
			</form>
		</div>
	</body>
</HTML>

<%@ Register TagPrefix="webcontrol" TagName="ClaimActivityTop" Src="/cms/cmsweb/webcontrols/ClaimActivityTop.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClaimTop" Src="/cms/cmsweb/webcontrols/ClaimTop.ascx"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Page language="c#" Codebehind="ReserveHomeDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.ReserveHomeDetails" %>
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
			var Section1Rows=0;			
			var Section2Rows=0;			
			var ScheduledItemsCoverageRows=0;
			var DummyPolicyRows = 0;
			var WatercraftCoveragesRows=0;
			var ctl = "ctl";
			var Section2CoverageGridID='<%=Section2CoverageGridID%>';			
			var Section1CoverageGridID='<%=Section1CoverageGridID%>';			
			var ScheduledItemsCoverageGridID = '<%=ScheduledItemsCoverageGridID%>';			
			var WatercraftCoverageGridID = '<%=WatercraftCoverageGridID%>';
			var Section1Prefix=Section1CoverageGridID + "__" + ctl;
			var Section2Prefix=Section2CoverageGridID + "__" + ctl;			
			var CoveragePrefix="dgCoverages__" + ctl;
			var ScheduledItemsCoveragePrefix=ScheduledItemsCoverageGridID + "__" + ctl;
			var WatercraftCoveragesPrefix=WatercraftCoverageGridID + "__" + ctl;
			var ReserveElementPrefix = "_txtRI_RESERVE";
			var ScheduledReserveElementPrefix = "txtRI_RESERVE_";
			var OutstandingElementPrefix = "_txtOUTSTANDING";
			var ScheduledOutstandingElementPrefix = "txtOUT_STANDING_";
			var LimitElementPrefix = "_lblLIMIT";			
			var OutstandingElementPrefix = "_txtOUTSTANDING";
			//Added for Itrack Issue 7663
			var WaterEquipCoverageRows = 0;
			var WaterEquipCoverageGridID = '<%=WaterEquipCoverageGridID%>';
			var WaterEquipCoveragePrefix="WaterEquip__" + ctl;
			var WaterEquipOutstandingElementPrefix = "txtOUT_STANDING_WATER_EQUIP_";
			var WaterEquipReserveElementPrefix = "txtRI_RESERVE_WATER_EQUIP_";
			var RecVehCoverageRows = 0;
			var RecVehCoverageGridID = '<%=RecVehCoverageGridID%>';
			var RecVehCoveragePrefix=RecVehCoverageGridID + "__" + ctl;
			//Added till here
			var ActivityClientID;
			var ActivityTotalPaymentClientID;
			
			
			function Init()
			{
				Section1Rows = parseFloat(document.getElementById("hidSection1RowCount").value)+1;				
				Section2Rows = parseFloat(document.getElementById("hidSection2RowCount").value)+1;
				DummyPolicyRows = parseFloat(document.getElementById("hidDummyPolicyCoverageRowCount").value)+1;
				ScheduledItemsCoverageRows = parseFloat(document.getElementById("hidScheduledItemRowCount").value)+1;
				WatercraftCoveragesRows = parseFloat(document.getElementById("hidWatercraftRowCount").value)+1;	
				//Added for Itrack Issue 7663							
				WaterEquipCoverageRows = parseFloat(document.getElementById("hidWaterEquipItemRowCount").value)+1;
				RecVehCoverageRows = parseFloat(document.getElementById("hidRecVehRowCount").value)+1;
				
				ApplyColor();
				ChangeColor();
				setfirstTime();	
				SetMenu();
				findMouseIn();			
				top.topframe.main1.mousein = false;								
				CalculateTotalOutstanding(true);
				CalculateTotalRIReserve(true);
				//CalculateTotalWaterEquipOutstanding(true);
				//CalculateTotalWaterEquipRI_Reserve(true);
				//CalculateTotalScheduledOutstanding(true);
				//CalculateTotalScheduledRI_Reserve(true);
				ActivityClientID = '<%=ActivityClientID%>';
				ActivityTotalPaymentClientID = '<%=ActivityTotalPaymentClientID%>';						
				if(document.getElementById(ActivityTotalPaymentClientID))
					document.getElementById(ActivityTotalPaymentClientID).innerText = formatCurrencyWithCents(document.getElementById("txtTOTAL_OUTSTANDING").value);
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
				//CalculateTotalWaterEquipOutstanding(false);
				//CalculateTotalWaterEquipRI_Reserve(false);
				//CalculateTotalScheduledOutstanding(false);
				//CalculateTotalScheduledRI_Reserve(false);
				return true;					
			}
			function CompareAllLimitAndOutstanding()
			{
				Page_ClientValidate();
				var Prefix,rows;
				var FinalResult = true;
				var ReturnValue = true;
				Prefix = Section1Prefix;
				rows = Section1Rows;
				
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
								
				Prefix = Section2Prefix;
				rows = Section2Rows;
				
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
				
				Prefix=ScheduledItemsCoveragePrefix;
				rows = ScheduledItemsCoverageRows;
								
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
				
				Prefix=WatercraftCoveragesPrefix;
				rows = WatercraftCoveragesRows;
				
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
				
				Prefix=RecVehCoveragePrefix;
				rows = RecVehCoverageRows;
				
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
			function GoToActivity()
			{
				SetMenu();
				strURL = "ActivityTab.aspx?&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&";
				this.document.location.href = strURL;
				return false;
			}
			function CalculateTotal(Prefix,ElementPrefix)
			{
				if(Prefix==CoveragePrefix)
					rows = DummyPolicyRows;
				else if(Prefix==Section1Prefix)
					rows = Section1Rows;
				else if(Prefix==Section2Prefix)
					rows = Section2Rows;	
				else if(Prefix==ScheduledItemsCoveragePrefix)
					rows = ScheduledItemsCoverageRows;
				else if(Prefix==WatercraftCoveragesPrefix)
					rows = WatercraftCoveragesRows;
				//Added for Itrack Issue 7663
				else if(Prefix==WaterEquipCoveragePrefix)
					rows = WaterEquipCoverageRows;
				else if(Prefix==RecVehCoveragePrefix)
					rows = RecVehCoverageRows;
				else
					return 0;
				
				var Sum = parseFloat(0);				
				for(var i=2;i<=rows;i++)
				{
					if(Prefix==ScheduledItemsCoveragePrefix)
						txtID = ElementPrefix + i ;	
					else if(Prefix==WaterEquipCoveragePrefix)//Added for Itrack Issue 7663
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
					Sum = CalculateTotal(Section1Prefix,OutstandingElementPrefix) + CalculateTotal(Section2Prefix,OutstandingElementPrefix) +  CalculateTotal(ScheduledItemsCoveragePrefix,ScheduledOutstandingElementPrefix) +  CalculateTotal(WatercraftCoveragesPrefix,OutstandingElementPrefix)+  CalculateTotal(WaterEquipCoveragePrefix,WaterEquipOutstandingElementPrefix)+CalculateTotal(RecVehCoveragePrefix,OutstandingElementPrefix);//Added for Itrack Issue 7663																								
				/*if(flag)									
					document.getElementById("hidOldTotalOutstanding").value = Sum;
				*/
				Sum = Math.round(Sum*10000)/100;
				document.getElementById("txtTOTAL_OUTSTANDING").value = formatCurrencyWithCents(Sum);				
								
			}
			
			function CalculateTotalScheduledOutstanding(flag)
			{
				var Sum = parseFloat(0);								
				Sum = CalculateTotal(Section1Prefix,OutstandingElementPrefix) + CalculateTotal(Section2Prefix,OutstandingElementPrefix) +  CalculateTotal(ScheduledItemsCoveragePrefix,ScheduledOutstandingElementPrefix) +  CalculateTotal(WatercraftCoveragesPrefix,OutstandingElementPrefix)+  CalculateTotal(WaterEquipCoveragePrefix,WaterEquipOutstandingElementPrefix)+CalculateTotal(RecVehCoveragePrefix,OutstandingElementPrefix);//Added for Itrack Issue 7663																								
				/*if(flag)									
					document.getElementById("hidOldTotalOutstanding").value = Sum;
				*/
				Sum = Math.round(Sum*10000)/100;
				document.getElementById("txtTOTAL_OUTSTANDING").value = formatCurrencyWithCents(Sum);				
			}
			
			function CalculateTotalScheduledRI_Reserve(flag)
			{	
				var Sum = parseFloat(0);				
				Sum = CalculateTotal(Section1Prefix,ReserveElementPrefix) + CalculateTotal(Section2Prefix,ReserveElementPrefix) + CalculateTotal(ScheduledItemsCoveragePrefix,ScheduledReserveElementPrefix) + CalculateTotal(WatercraftCoveragesPrefix,ReserveElementPrefix)+CalculateTotal(RecVehCoveragePrefix,ReserveElementPrefix);//Added for Itrack Issue 7663								
				/*if(flag)				
					document.getElementById("hidOldTotalRiReserve").value = Sum;
				*/
				Sum = Math.round(Sum*10000)/100;
				document.getElementById("txtTOTAL_RI_RESERVE").value = formatCurrencyWithCents(Sum);												
			}	
			function CalculateTotalRIReserve(flag)
			{	
				var Sum = parseFloat(0);				
				Sum = CalculateTotal(Section1Prefix,ReserveElementPrefix) + CalculateTotal(Section2Prefix,ReserveElementPrefix) + CalculateTotal(ScheduledItemsCoveragePrefix,ScheduledReserveElementPrefix) + CalculateTotal(WatercraftCoveragesPrefix,ReserveElementPrefix)+CalculateTotal(RecVehCoveragePrefix,ReserveElementPrefix)+  CalculateTotal(WaterEquipCoveragePrefix,WaterEquipReserveElementPrefix);//Added for Itrack Issue 7663								
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
				Sum = CalculateTotal(Section1Prefix,OutstandingElementPrefix) + CalculateTotal(Section2Prefix,OutstandingElementPrefix) +  CalculateTotal(ScheduledItemsCoveragePrefix,ScheduledOutstandingElementPrefix) +  CalculateTotal(WatercraftCoveragesPrefix,OutstandingElementPrefix) +  CalculateTotal(WaterEquipCoveragePrefix,WaterEquipOutstandingElementPrefix)+ CalculateTotal(RecVehCoveragePrefix,OutstandingElementPrefix);																								
				
				Sum = Math.round(Sum*10000)/100;
				document.getElementById("txtTOTAL_OUTSTANDING").value = formatCurrencyWithCents(Sum);			
			}
			
			function CalculateTotalWaterEquipRI_Reserve(flag)
			{	
				var Sum = parseFloat(0);				
				Sum = CalculateTotal(Section1Prefix,ReserveElementPrefix) + CalculateTotal(Section2Prefix,ReserveElementPrefix) +  CalculateTotal(ScheduledItemsCoveragePrefix,ScheduledReserveElementPrefix) +  CalculateTotal(WatercraftCoveragesPrefix,ReserveElementPrefix) +  CalculateTotal(WaterEquipCoveragePrefix,WaterEquipReserveElementPrefix)+CalculateTotal(RecVehCoveragePrefix,ReserveElementPrefix);								
				
				Sum = Math.round(Sum*10000)/100;
				document.getElementById("txtTOTAL_RI_RESERVE").value = formatCurrencyWithCents(Sum);												
			}
		</script>
	</HEAD>
	<body onload="Init();" MS_POSITIONING="GridLayout">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<div class="pageContent" id="bodyHeight">
			<form id="Form1" method="post" runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="95%">
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
						<td class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False" EnableViewState="False"></asp:label></td>
					</tr>
					<TR id="trBody" runat="server">
						<TD colSpan="4">
							<table cellSpacing="0" cellPadding="0" width="100%">
								<tr style="DISPLAY: none">
									<TD class="midcolora" width="18%"><asp:label id="capACTIVITY_DATE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblACTIVITY_DATE" runat="server"></asp:label></TD>
									<TD class="midcolora" colSpan="2"></TD>
								</tr>
								<tr>
									<td colSpan="4" height="5"></td>
								</tr>
								<!--Moved down for Itrack Issue 7663-->
								<%--<tr id="trWatercraftCoveragesRow" runat="server">
									<td class="headerEffectSystemParams"><asp:label id="lblWatercraftCoverages" Runat="server">Watercraft Coverages</asp:label></td>
								</tr>
								<tr id="trWatercraftCoveragesGridRow" runat="server">
									<td class="midcolora" colSpan="4"><asp:datagrid id="dgWatercraftCoveragesGrid" runat="server" AutoGenerateColumns="False" Width="100%">
											<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
											<ItemStyle CssClass="midcolora"></ItemStyle>
											<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="COV_DESC" HeaderText="Coverage" ItemStyle-Width="15%"></asp:BoundColumn>-->
												<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="30%">
													<ItemTemplate>
														<asp:Label ID="lblCOV_DESC"  Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") >'>
														</asp:Label>
														<asp:Label ID="lblDWELLING_ID"    Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.DWELLING_ID") >'>
														</asp:Label>
														<asp:Label ID="lblRESERVE_ID"    Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RESERVE_ID") >'>
														</asp:Label>
														<asp:Label ID="lblCOV_ID"    Runat="server"  Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") >'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Limit" ItemStyle-CssClass="midcolorr" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:Label ID="lblLIMIT"  Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT") >'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-CssClass="midcolorr" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE"  Runat="server"  ></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:TextBox ID="txtOUTSTANDING"  CssClass="INPUTCURRENCY" Runat="server" size="20" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revOUTSTANDING" ControlToValidate="txtOUTSTANDING" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngOUTSTANDING"  MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtOUTSTANDING" Enabled="False"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<%--<asp:TemplateColumn HeaderText="Paid Loss" ItemStyle-Width="5%">
													<ItemTemplate>														
														<asp:TextBox ID="txtPRIMARY_EXCESS" Runat="server" size="10" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.PRIMARY_EXCESS") >'>></asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>-->
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:TextBox ID="txtRI_RESERVE" CssClass="INPUTCURRENCY" Runat="server" size="20" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revRI_RESERVE" ControlToValidate="txtRI_RESERVE" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngRI_RESERVE"  MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtRI_RESERVE" Enabled="False"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reinsurance Carrier" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:DropDownList ID="cmbREINSURANCE_CARRIER"  Runat="server"></asp:DropDownList>
													</ItemTemplate>
												</asp:TemplateColumn>
												<%--<asp:TemplateColumn HeaderText="MCCA Attachment Point" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblMCCA_ATTACHMENT_POINT" Runat="server" Text=""></asp:Label>
														<asp:TextBox ID="txtMCCA_ATTACHMENT_POINT" size="10" Runat="server" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.MCCA_ATTACHMENT_POINT","{0:,#,###}") >'>></asp:TextBox>
														<asp:RangeValidator ID="rngMCCA_ATTACHMENT_POINT" MinimumValue="1" MaximumValue="9999999999" Type="Currency"
															Runat="server" Display="Dynamic" ControlToValidate="txtMCCA_ATTACHMENT_POINT"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="MCCA Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblMCCA_APPLIES" Runat="server" Text=""></asp:Label>
														<asp:TextBox ID="txtMCCA_APPLIES" Runat="server" size="10" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.MCCA_APPLIES","{0:,#,###}") >'>></asp:TextBox><br>
														<asp:RangeValidator ID="rngMCCA_APPLIES" MinimumValue="1" MaximumValue="9999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtMCCA_APPLIES"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Attachment Point" Visible="False" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:TextBox ID="txtATTACHMENT_POINT"  CssClass="INPUTCURRENCY" Runat="server" size="10" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.ATTACHMENT_POINT","{0:,#,###}") >'>></asp:TextBox>
														<asp:RegularExpressionValidator ID="revATTACHMENT_POINT" ControlToValidate="txtATTACHMENT_POINT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngATTACHMENT_POINT" MinimumValue="1" MaximumValue="9999999999" Type="Currency"
															Runat="server" Display="Dynamic" ControlToValidate="txtATTACHMENT_POINT" Enabled="False"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>
								<tr id="trScheduledItemsCoveragesRow" runat="server">
									<td class="headerEffectSystemParams" colSpan="4"><asp:label id="lblScheduledItemsCoverages" Runat="server">Scheduled Items Coverages</asp:label></td>
								</tr>
								<tr id="trScheduledItemsCoveragesGridRow" runat="server">
									<td colSpan="4">
										<asp:PlaceHolder ID="plcScheduledCovg" Runat="server"></asp:PlaceHolder>
									</td>
								</tr>--%>
								<%--<tr id="trScheduledItemsCoveragesGridRow" runat="server">
									<td class="midcolora" colSpan="4"><asp:datagrid id="dgScheduledItemsCoveragesGrid" runat="server" AutoGenerateColumns="False" Width="100%">
											<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
											<ItemStyle CssClass="midcolora"></ItemStyle>
											<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="30%">
													<ItemTemplate>
														<asp:Label ID="lblCOV_DESC"  Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") >'>
														</asp:Label>
														<asp:Label ID="lblRESERVE_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RESERVE_ID") >'>
														</asp:Label>
														<asp:Label ID="lblCOV_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") >'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Amount" ItemStyle-CssClass="midcolorr" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:Label ID="lblSCHEDULED_ITEM_COVERAGE_AMOUNT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SCHEDULED_ITEM_COVERAGE_AMOUNT","{0:,#,###}") >'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-CssClass="midcolorr" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE"  Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE","{0:,#,###}") >'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:TextBox ID="txtOUTSTANDING"  Runat="server" size="10" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.OUTSTANDING","{0:,#,###}") >'>></asp:TextBox><br>
														<asp:RangeValidator ID="rngOUTSTANDING"  MinimumValue="1" MaximumValue="9999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtOUTSTANDING"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:TextBox ID="txtRI_RESERVE"  Runat="server" MaxLength="10" size="10" Text='<%# DataBinder.Eval(Container, "DataItem.RI_RESERVE","{0:,#,###}") >'>></asp:TextBox><Br>
														<asp:RangeValidator ID="rngRI_RESERVE"  MinimumValue="1" MaximumValue="9999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtRI_RESERVE"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reinsurance Carrier" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:DropDownList ID="cmbREINSURANCE_CARRIER" Runat="server"></asp:DropDownList>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>	--%>	
								<tr id="trSection1CoveragesRow" runat="server">
									<td class="headerEffectSystemParams" colSpan="4"><asp:label id="lblSection1Coverages" Runat="server">Section I Coverages</asp:label></td>
								</tr>						
								<tr id="trSection1CoveragesGridRow" runat="server">
									<td class="midcolora" colSpan="4"><asp:datagrid id="dgSection1CoveragesGrid" runat="server" AutoGenerateColumns="False" Width="100%">
											<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
											<ItemStyle CssClass="midcolora"></ItemStyle>
											<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="30%">
													<ItemTemplate>
														<asp:Label ID="lblCOV_DESC" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
														</asp:Label>
														<asp:Label ID="lblDWELLING_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.DWELLING_ID") %>'>
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
												<asp:TemplateColumn HeaderText="Included" ItemStyle-CssClass="midcolorr" ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:Label ID="lblLIMIT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Additional" ItemStyle-CssClass="midcolorr" ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE" Runat="server"  ></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE2" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE2","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:TextBox ID="txtOUTSTANDING"  CssClass="INPUTCURRENCY" Runat="server" size="20" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revOUTSTANDING" ControlToValidate="txtOUTSTANDING" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngOUTSTANDING" MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtOUTSTANDING" Enabled="False"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:TextBox ID="txtRI_RESERVE" CssClass="INPUTCURRENCY" Runat="server" size="20" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revRI_RESERVE" ControlToValidate="txtRI_RESERVE" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngRI_RESERVE" MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtRI_RESERVE" Enabled="False"></asp:RangeValidator>
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
								<tr id="trSection2CoveragesRow" runat="server">
									<td class="headerEffectSystemParams" colSpan="4"><asp:label id="lblSection2Coverages" Runat="server">Section II Coverages</asp:label></td>
								</tr>
								<tr id="trSection2CoveragesGridRow" runat="server">
									<td class="midcolora" colSpan="4"><asp:datagrid id="dgSection2CoveragesGrid" runat="server" AutoGenerateColumns="False" Width="100%">
											<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
											<ItemStyle CssClass="midcolora"></ItemStyle>
											<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="30%">
													<ItemTemplate>
														<asp:Label ID="lblCOV_DESC" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
														</asp:Label>
														<asp:Label ID="lblDWELLING_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.DWELLING_ID") %>'>
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
												<asp:TemplateColumn HeaderText="Included" ItemStyle-CssClass="midcolorr" ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:Label ID="lblLIMIT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Additional" ItemStyle-CssClass="midcolorr" ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE" Runat="server"  ></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE2" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE2","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:TextBox ID="txtOUTSTANDING"  CssClass="INPUTCURRENCY" Runat="server" size="20" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revOUTSTANDING" ControlToValidate="txtOUTSTANDING" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngOUTSTANDING" MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtOUTSTANDING" Enabled="False"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:TextBox ID="txtRI_RESERVE" CssClass="INPUTCURRENCY" Runat="server" size="20" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revRI_RESERVE" ControlToValidate="txtRI_RESERVE" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngRI_RESERVE" MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtRI_RESERVE" Enabled="False"></asp:RangeValidator>
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
								<!--Moved here for Itrack Issue 7663-->
								<tr id="trScheduledItemsCoveragesRow" runat="server">
									<td class="headerEffectSystemParams" colSpan="4"><asp:label id="lblScheduledItemsCoverages" Runat="server">Scheduled Items Coverages</asp:label></td>
								</tr>
								<tr id="trScheduledItemsCoveragesGridRow" runat="server">
									<td colSpan="4">
										<asp:PlaceHolder ID="plcScheduledCovg" Runat="server"></asp:PlaceHolder>
									</td>
								</tr>
								<!--Added for Itrack Issue 7663-->
								<tr id="trRecVehCoveragesRow" runat="server">
									<td class="headerEffectSystemParams" colSpan="4"><asp:label id="lblRecVehCoverages" Runat="server">Recreational Vehicle Coverages</asp:label></td>
								</tr>
								<tr id="trRecVehCoveragesGridRow" runat="server">
									<td class="midcolora" colSpan="4"><asp:datagrid id="dgRecVehCoveragesGrid" runat="server" AutoGenerateColumns="False" Width="100%">
											<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
											<ItemStyle CssClass="midcolora"></ItemStyle>
											<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="30%">
													<ItemTemplate>
														<asp:Label ID="lblCOV_DESC" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
														</asp:Label>
														<asp:Label ID="lblDWELLING_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.POL_DWELLING_ID") %>'>
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
												<asp:TemplateColumn HeaderText="Included" ItemStyle-CssClass="midcolorr" ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:Label ID="lblLIMIT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Additional" ItemStyle-CssClass="midcolorr" ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE" Runat="server"  ></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE2" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE2","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:TextBox ID="txtOUTSTANDING"  CssClass="INPUTCURRENCY" Runat="server" size="20" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revOUTSTANDING" ControlToValidate="txtOUTSTANDING" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngOUTSTANDING" MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtOUTSTANDING" Enabled="False"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="12%">
													<ItemTemplate>
														<asp:TextBox ID="txtRI_RESERVE" CssClass="INPUTCURRENCY" Runat="server" size="20" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revRI_RESERVE" ControlToValidate="txtRI_RESERVE" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngRI_RESERVE" MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtRI_RESERVE" Enabled="False"></asp:RangeValidator>
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
								<tr id="trWatercraftCoveragesRow" runat="server">
									<td class="headerEffectSystemParams"><asp:label id="lblWatercraftCoverages" Runat="server">Watercraft Coverages</asp:label></td>
								</tr>
								<tr id="trWatercraftCoveragesGridRow" runat="server">
									<td class="midcolora" colSpan="4"><asp:datagrid id="dgWatercraftCoveragesGrid" runat="server" AutoGenerateColumns="False" Width="100%">
											<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
											<ItemStyle CssClass="midcolora"></ItemStyle>
											<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
											<Columns>
												<%--<asp:BoundColumn DataField="COV_DESC" HeaderText="Coverage" ItemStyle-Width="15%"></asp:BoundColumn>--%>
												<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="30%">
													<ItemTemplate>
														<asp:Label ID="lblCOV_DESC"  Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
														</asp:Label>
														<asp:Label ID="lblDWELLING_ID"    Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.DWELLING_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblRESERVE_ID"    Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RESERVE_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblCOV_ID"    Runat="server"  Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_TYPE" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_TYPE") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Limit" ItemStyle-CssClass="midcolorr" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:Label ID="lblLIMIT"  Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-CssClass="midcolorr" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE"  Runat="server"  ></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:TextBox ID="txtOUTSTANDING"  CssClass="INPUTCURRENCY" Runat="server" size="20" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revOUTSTANDING" ControlToValidate="txtOUTSTANDING" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngOUTSTANDING"  MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtOUTSTANDING" Enabled="False"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<%--<asp:TemplateColumn HeaderText="Paid Loss" ItemStyle-Width="5%">
													<ItemTemplate>														
														<asp:TextBox ID="txtPRIMARY_EXCESS" Runat="server" size="10" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.PRIMARY_EXCESS") >'>></asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>--%>
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:TextBox ID="txtRI_RESERVE" CssClass="INPUTCURRENCY" Runat="server" size="20" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revRI_RESERVE" ControlToValidate="txtRI_RESERVE" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngRI_RESERVE"  MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtRI_RESERVE" Enabled="False"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reinsurance Carrier" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:DropDownList ID="cmbREINSURANCE_CARRIER"  Runat="server"></asp:DropDownList>
													</ItemTemplate>
												</asp:TemplateColumn>
												<%--<asp:TemplateColumn HeaderText="MCCA Attachment Point" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblMCCA_ATTACHMENT_POINT" Runat="server" Text=""></asp:Label>
														<asp:TextBox ID="txtMCCA_ATTACHMENT_POINT" size="10" Runat="server" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.MCCA_ATTACHMENT_POINT","{0:,#,###}") >'>></asp:TextBox>
														<asp:RangeValidator ID="rngMCCA_ATTACHMENT_POINT" MinimumValue="1" MaximumValue="9999999999" Type="Currency"
															Runat="server" Display="Dynamic" ControlToValidate="txtMCCA_ATTACHMENT_POINT"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="MCCA Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblMCCA_APPLIES" Runat="server" Text=""></asp:Label>
														<asp:TextBox ID="txtMCCA_APPLIES" Runat="server" size="10" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.MCCA_APPLIES","{0:,#,###}") >'>></asp:TextBox><br>
														<asp:RangeValidator ID="rngMCCA_APPLIES" MinimumValue="1" MaximumValue="9999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtMCCA_APPLIES"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>--%>
												<asp:TemplateColumn HeaderText="Attachment Point" Visible="False" ItemStyle-Width="14%">
													<ItemTemplate>
														<asp:TextBox ID="txtATTACHMENT_POINT"  CssClass="INPUTCURRENCY" Runat="server" size="10" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.ATTACHMENT_POINT","{0:,#,###}") %>'>></asp:TextBox>
														<asp:RegularExpressionValidator ID="revATTACHMENT_POINT" ControlToValidate="txtATTACHMENT_POINT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngATTACHMENT_POINT" MinimumValue="1" MaximumValue="9999999999" Type="Currency"
															Runat="server" Display="Dynamic" ControlToValidate="txtATTACHMENT_POINT" Enabled="False"></asp:RangeValidator>
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
									<td colSpan="4">&nbsp;</td>
								</tr>
								<tr class="midcolora" id="trTotalRow" runat="server">
									<TD class="midcolora"><b><asp:textbox class="midcolora" id="txtGrossTotal" Runat="server" BorderStyle="None" Text="Gross Total"
												BorderWidth="0" ReadOnly="True" Font-Bold="True"></asp:textbox></b></TD>
									<td colSpan="3">
										<table width="100%" border="0">
											<tr>
												<TD width="55%"></td>																								
												<TD width="20%"><asp:textbox class="midcolora" id="txtTOTAL_OUTSTANDING" Runat="server" BorderStyle="None" BorderWidth="0" size="20"
														ReadOnly="True"></asp:textbox></TD>
												<TD><asp:textbox class="midcolora" id="txtTOTAL_RI_RESERVE" Runat="server"  BorderStyle="None" size="20"
														BorderWidth="0" ReadOnly="True"></asp:textbox></TD>												
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4">Estimation</TD>
								</tr>
								<tr class="midcolora">
									<TD class="midcolora" width="18%"><asp:label id="capRECOVERY" Runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtRECOVERY"  CssClass="INPUTCURRENCY" Runat="server" MaxLength="14"></asp:textbox><br>
									<asp:RegularExpressionValidator ID="revRECOVERY" ControlToValidate="txtRECOVERY" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
										<asp:rangevalidator id="rngRECOVERY" Runat="server" MinimumValue="1" MaximumValue="99999999999999" Type="Currency"
											Display="Dynamic" ControlToValidate="txtRECOVERY" Enabled="False"></asp:RangeValidator></td>
									<TD class="midcolora" width="18%"><asp:label id="capEXPENSES" Runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEXPENSES"  CssClass="INPUTCURRENCY" Runat="server" MaxLength="14"></asp:textbox><br>
									<asp:RegularExpressionValidator ID="revEXPENSES" ControlToValidate="txtEXPENSES" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
										<asp:rangevalidator id="rngEXPENSES" Runat="server" MinimumValue="1" MaximumValue="99999999999999" Type="Currency"
											Display="Dynamic" ControlToValidate="txtEXPENSES" Enabled="False"></asp:RangeValidator></td>
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
						<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
					</tr>
				</TABLE>
				<INPUT id="hidACTION_ON_PAYMENT" type="hidden" name="hidACTION_ON_PAYMENT" runat="server">
				<INPUT id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
				<INPUT id="hidLOB_ID" type="hidden" name="hidLOB_ID" runat="server"> <INPUT id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server">
				<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"> <INPUT id="hidSection1RowCount" type="hidden" value="0" name="hidSection1RowCount" runat="server">
				<INPUT id="hidPolicyRowCount" type="hidden" name="hidPolicyRowCount" runat="server" value="0">
				<INPUT id="hidDummyPolicyCoverageRowCount" type="hidden" name="hidDummyPolicyCoverageRowCount" runat="server" value="0">
				<INPUT id="hidCLAIM_ID" type="hidden" name="hidCLAIM_ID" runat="server"> <INPUT id="hidACTIVITY_ID" type="hidden" name="hidACTIVITY_ID" runat="server">
				<INPUT id="hidSection2RowCount" type="hidden" value="0" name="hidSection2RowCount" runat="server">
				<INPUT id="hidScheduledItemRowCount" type="hidden" value="0" name="hidScheduledItemRowCount" runat="server">
				<INPUT id="hidWatercraftRowCount" type="hidden" value="0" name="hidWatercraftRowCount" runat="server">
				<INPUT id="hidNewReserve" type="hidden" value="0" name="hidNewReserve" runat="server">
				<INPUT id="hidScheduledCovgID" type="hidden" value="" name="hidScheduledCovgID" runat="server">				
				<INPUT id="hidScheduledRsvID" type="hidden" value="" name="hidScheduledRsvID" runat="server">
				<INPUT id="hidOldTotalOutstanding" type="hidden" name="hidOldTotalOutstanding" runat="server" value="0">
				<INPUT id="hidOldTotalRiReserve" type="hidden" name="hidOldTotalRiReserve" runat="server" value="0">
				<INPUT id="hidTRANSACTION_ID" type="hidden" name="hidTRANSACTION_ID" runat="server" value="0">
				<INPUT id="hidTRANSACTION_CATEGORY" type="hidden" name="hidTRANSACTION_CATEGORY" runat="server">
				<!--Added for Itrack Issue 7663-->
				<INPUT id="hidSTATE_ID" type="hidden" name="hidSTATE_ID" runat="server">
				<INPUT id="hidWaterEquipItemRowCount" type="hidden" value="0" name="hidWaterEquipItemRowCount" runat="server">
				<INPUT id="hidWaterEquipCovgID" type="hidden" value="" name="hidWaterEquipCovgID" runat="server">
				<INPUT id="hidWaterEquipRsvID" type="hidden" value="" name="hidWaterEquipRsvID" runat="server">
				<INPUT id="hidRecVehRowCount" type="hidden" value="0" name="hidRecVehRowCount" runat="server">
				<INPUT id="hidRecVehCovgID" type="hidden" value="" name="hidRecVehCovgID" runat="server">
				<INPUT id="hidRecVehRsvID" type="hidden" value="" name="hidRecVehRsvID" runat="server">
				<!--Added for Itrack Issue 7663 on 19 Aug 2010-->
				<INPUT id="hidSch_ACTUAL_RISK_ID" type="hidden" value="" name="hidSch_ACTUAL_RISK_ID" runat="server">
				<INPUT id="hidSch_ACTUAL_RISK_TYPE" type="hidden" value="" name="hidSch_ACTUAL_RISK_TYPE" runat="server">
				<INPUT id="hidEquip_ACTUAL_RISK_ID" type="hidden" value="" name="hidEquip_ACTUAL_RISK_ID" runat="server">
				<INPUT id="hidEquip_ACTUAL_RISK_TYPE" type="hidden" value="" name="hidEquip_ACTUAL_RISK_TYPE" runat="server">
				<INPUT id="hidSchDWELLING_ID" type="hidden" value="" name="hidSchDWELLING_ID" runat="server">
				<INPUT id="hidEquipDWELLING_ID" type="hidden" value="" name="hidEquipDWELLING_ID" runat="server">
			</form>
		</div>
	</body>
</HTML>

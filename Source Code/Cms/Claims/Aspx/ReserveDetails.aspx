<%@ Register TagPrefix="webcontrol" TagName="ClaimActivityTop" Src="/cms/cmsweb/webcontrols/ClaimActivityTop.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClaimTop" Src="/cms/cmsweb/webcontrols/ClaimTop.ascx"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Page language="c#" validateRequest=false Codebehind="ReserveDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.ReserveDetails" %>
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
			var DummyPolicyRows = 0;
			var ctl = "ctl";
			var vehicleCoverageGridID='<%=VehicleCoverageGridID%>';			
			var policyCoverageGridID='<%=PolicyCoverageGridID%>';			
			var PolicyPrefix=policyCoverageGridID + "__" + ctl;
			var CoveragePrefix="dgCoverages__" + ctl;
			var VehiclePrefix=vehicleCoverageGridID + "__" + ctl;
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
				policyRows = parseFloat(document.getElementById("hidPolicyRowCount").value)+1;				
				vehicleRows = parseFloat(document.getElementById("hidVehicleRowCount").value)+1;
				DummyPolicyRows = parseFloat(document.getElementById("hidDummyPolicyCoverageRowCount").value)+1;
				CalculateTotalOutstanding(true);
				CalculateTotalRIReserve(true);
				CalculateTotalMCCA_APPLIES();
				//For Indiana state, we do not have the MCCA column in the grid
				if(document.getElementById("hidSTATE_ID").value != <%=((int)enumState.Indiana).ToString()%>)
				{
					ShowHide(PolicyPrefix,policyRows);
					ShowHide(VehiclePrefix,vehicleRows);
				}
				ApplyColor();
				ChangeColor();
				setfirstTime();	
				SetMenu();
				findMouseIn();			
				top.topframe.main1.mousein = false;		
				ActivityClientID = '<%=ActivityClientID%>'
				ActivityTotalPaymentClientID = '<%=ActivityTotalPaymentClientID%>'						
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
				return true;				
			}
			function CompareAllLimitAndOutstanding()
			{
				Page_ClientValidate();
				var Prefix,rows;
				var FinalResult = true;
				var ReturnValue = true;
				Prefix = PolicyPrefix;
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
				} 				
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
				return Page_IsValid;
			}
			function CalculateTotal(Prefix,ElementPrefix)
			{
				var rows;
				if(Prefix==CoveragePrefix) 
				{ 
				 //Done for Itrack Issue 5548 on 9 June 09
				 if(document.getElementById("hidDummyPolicyCoverageRowCount").value == "" || document.getElementById("hidDummyPolicyCoverageRowCount").value == "0" ) 
				 { 
				   rows = vehicleRows; 
				 } 
				else
				 {
				   rows = DummyPolicyRows; 
				 } 
				} 
				else if(Prefix==PolicyPrefix) 
				{
				rows = policyRows; 
				}
				//Commented for Itrack Issue 5548 on 9 June 09
				/*if(Prefix==CoveragePrefix)
					rows = DummyPolicyRows;
				else if(Prefix==PolicyPrefix)
					rows = policyRows;
				else
					rows = vehicleRows;	*/			
				var Sum = parseFloat(0);
				for(var totalCtr=2;totalCtr<=rows;totalCtr++)
				{
					txtID = Prefix + totalCtr + ElementPrefix;												
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
					Sum = CalculateTotal(PolicyPrefix,"_txtOUTSTANDING") + CalculateTotal(VehiclePrefix,"_txtOUTSTANDING");																				
				/*if(flag)				
					document.getElementById("hidOldTotalOutstanding").value = Sum;
				alert(document.getElementById("hidOldTotalOutstanding").value);
				*/
				Sum = Math.round(Sum*10000)/100;
				document.getElementById("txtTOTAL_OUTSTANDING").value = formatCurrencyWithCents(Sum);
								
			}
			function CalculateTotalRIReserve(flag)
			{	
				var Sum = parseFloat(0);				
				Sum = CalculateTotal(PolicyPrefix,"_txtRI_RESERVE") + CalculateTotal(VehiclePrefix,"_txtRI_RESERVE");								
				/*if(flag)				
					document.getElementById("hidOldTotalRiReserve").value = Sum;
				*/
				Sum = Math.round(Sum*10000)/100;
				document.getElementById("txtTOTAL_RI_RESERVE").value = formatCurrencyWithCents(Sum);								
				
			}
			function CalculateTotalMCCA_APPLIES()
			{	
				//For Indiana state, we do not have the MCCA column in the grid, lets return
				if(document.getElementById("hidSTATE_ID").value == <%=((int)enumState.Indiana).ToString()%>)
				{
					return;
				}
				else
				{
					var Sum = parseFloat(0);
					Sum = CalculateTotal(PolicyPrefix,"_txtMCCA_APPLIES") + CalculateTotal(VehiclePrefix,"_txtMCCA_APPLIES");								
					Sum = Math.round(Sum*10000)/100;
					document.getElementById("txtTOTAL_MCCA_APPLIES").value = formatCurrencyWithCents(Sum);					
				}
				
			}		
				
			function ShowHide(Prefix, rows)
			{
				for(var i=2;i<=rows;i++)
				{
					//strPRIMARY_EXCESS =  Prefix + i + "_txtPRIMARY_EXCESS";
					strTxtMCCA_ATTACHMENT_POINT = Prefix + i + "_txtMCCA_ATTACHMENT_POINT";
					strtxtMCCA_APPLIES = Prefix + i + "_txtMCCA_APPLIES";
					strLblMCCA_APPLIES = Prefix + i + "_lblMCCA_APPLIES";
					strLblMCCA_ATTACHMENT_POINT = Prefix + i + "_lblMCCA_ATTACHMENT_POINT";		
					strlblCOV_ID = Prefix + i + "_lblCOV_ID";			
					
					//if(document.getElementById(strPRIMARY_EXCESS)==null)
					//	continue;
					//txtPRIMARY_EXCESS = document.getElementById(strPRIMARY_EXCESS);
					
					txtMCCA_ATTACHMENT_POINT = document.getElementById(strTxtMCCA_ATTACHMENT_POINT);
					lblMCCA_ATTACHMENT_POINT = document.getElementById(strLblMCCA_ATTACHMENT_POINT);
					txtMCCA_APPLIES = document.getElementById(strtxtMCCA_APPLIES);
					lblMCCA_APPLIES = document.getElementById(strLblMCCA_APPLIES);
					
					if(document.getElementById(strlblCOV_ID)!=null)
						lblCOV_ID		= document.getElementById(strlblCOV_ID);	
					
					//ShowHideMCCA(txtPRIMARY_EXCESS,txtMCCA_ATTACHMENT_POINT,lblMCCA_ATTACHMENT_POINT,txtMCCA_APPLIES,lblMCCA_APPLIES,lblCOV_ID);
					if(txtMCCA_ATTACHMENT_POINT==null)
						continue;
					ShowHideMCCA(txtMCCA_ATTACHMENT_POINT,lblMCCA_ATTACHMENT_POINT,txtMCCA_APPLIES,lblMCCA_APPLIES,lblCOV_ID);
					
				}
			}
			//function ShowHideMCCA(txtPRIMARY_EXCESS,txtMCCA_ATTACHMENT_POINT,lblMCCA_ATTACHMENT_POINT,txtMCCA_APPLIES,lblMCCA_APPLIES,lblCOV_ID)
			function ShowHideMCCA(txtMCCA_ATTACHMENT_POINT,lblMCCA_ATTACHMENT_POINT,txtMCCA_APPLIES,lblMCCA_APPLIES,lblCOV_ID)
			{
				//var strPrimary = new String(txtPRIMARY_EXCESS.value);						
				var strCoverageID="";				
				if(lblCOV_ID!=null) 
					strCoverageID = new String(lblCOV_ID.innerHTML);				
				//strPrimary = strPrimary.toUpperCase();				
				if(strCoverageID=='<%=PIP_COV_ID%>')
				{	
					txtMCCA_ATTACHMENT_POINT.style.display = "inline";
					txtMCCA_APPLIES.style.display = "inline";
					lblMCCA_ATTACHMENT_POINT.style.display = "none";
					lblMCCA_APPLIES.style.display = "none";
					if (parseFloat(txtMCCA_ATTACHMENT_POINT.value) > 0)
						txtMCCA_ATTACHMENT_POINT.value = txtMCCA_ATTACHMENT_POINT.value
					else
					{
						if(<%=mccaAttachmentPoint%>!=0)
							txtMCCA_ATTACHMENT_POINT.value = <%=mccaAttachmentPoint%>
					}
				}
				else
				{
					txtMCCA_ATTACHMENT_POINT.value="";
					txtMCCA_ATTACHMENT_POINT.style.display = "none";
					txtMCCA_APPLIES.style.display = "none";
					lblMCCA_ATTACHMENT_POINT.style.display = "inline";
					lblMCCA_APPLIES.style.display = "inline";
				}
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
	<body oncontextmenu="return false;" onload="Init();" MS_POSITIONING="GridLayout">
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
						<td class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" runat="server" EnableViewState="False" Visible="False" CssClass="errmsg"></asp:label></td>
					</tr>
					<TR id="trBody" runat="server">
						<TD colspan="4">
							<table cellSpacing="0" cellPadding="0" width="100%">
								<tr style="DISPLAY:none">
									<TD class="midcolora" width="18%"><asp:label id="capACTIVITY_DATE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblACTIVITY_DATE" runat="server"></asp:label></TD>
									<TD class="midcolora" colSpan="2"></TD>
								</tr>
								<tr>
									<td colSpan="4" height="5"></td>
								</tr>
								<tr id="trPolicyrow" runat="server">
									<td class="headerEffectSystemParams"><asp:label id="lblPolicyCaption" Runat="server">Policy Level Coverages</asp:label></td>
								</tr>
								<tr id="trPolicyGridRow" runat="server">
									<td class="midcolora" colSpan="4"><asp:datagrid id="dgPolicyCoverages" runat="server" Width="100%" AutoGenerateColumns="False">
											<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
											<ItemStyle CssClass="midcolora"></ItemStyle>
											<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="COV_DESC" HeaderText="Coverage" ItemStyle-Width="20%"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Limit" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblLIMIT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT") %>'>></asp:Label>
														<asp:Label ID="lblACTUAL_RISK_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_TYPE" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_TYPE") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-CssClass="midcolorr" ItemStyle-Width="5%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE" Runat="server" ></asp:Label>
														<asp:Label ID="lblRESERVE_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RESERVE_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblCOV_ID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="txtOUTSTANDING"  CssClass="INPUTCURRENCY" Runat="server" size="20" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revOUTSTANDING" ControlToValidate="txtOUTSTANDING" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngOUTSTANDING" MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server" Enabled="False" Display="Dynamic" ControlToValidate="txtOUTSTANDING"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="txtRI_RESERVE" CssClass="INPUTCURRENCY" Runat="server" size="20" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revRI_RESERVE" ControlToValidate="txtRI_RESERVE" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngRI_RESERVE" MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server" Enabled="False"
															Display="Dynamic" ControlToValidate="txtRI_RESERVE"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reinsurance Carrier" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:DropDownList ID="cmbREINSURANCE_CARRIER" Runat="server"></asp:DropDownList>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="MCCA Attachment Point" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblMCCA_ATTACHMENT_POINT" Runat="server" Text=""></asp:Label>
														<asp:TextBox ID="txtMCCA_ATTACHMENT_POINT" Runat="server" size="10" MaxLength="10" CssClass="INPUTCURRENCY" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revMCCA_ATTACHMENT_POINT" ControlToValidate="txtMCCA_ATTACHMENT_POINT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>														
														<asp:RangeValidator ID="rngMCCA_ATTACHMENT_POINT" MinimumValue="1" MaximumValue="9999999999" Type="Currency" Enabled="False"
															Runat="server" Display="Dynamic" ControlToValidate="txtMCCA_ATTACHMENT_POINT"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="MCCA Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblMCCA_APPLIES" Runat="server" Text=""></asp:Label>
														<asp:TextBox ID="txtMCCA_APPLIES" Runat="server" size="10" MaxLength="10" CssClass="INPUTCURRENCY" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revMCCA_APPLIES" ControlToValidate="txtMCCA_APPLIES" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngMCCA_APPLIES" MinimumValue="1" MaximumValue="9999999999" Type="Currency" Runat="server" Enabled="False"
															Display="Dynamic" ControlToValidate="txtMCCA_APPLIES"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Attachment Point" Visible="False" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="txtATTACHMENT_POINT" Runat="server" size="10" MaxLength="10" CssClass="INPUTCURRENCY"></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revATTACHMENT_POINT" ControlToValidate="txtATTACHMENT_POINT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngATTACHMENT_POINT" MinimumValue="1" MaximumValue="9999999999" Type="Currency" Enabled="False"
															Runat="server" Display="Dynamic" ControlToValidate="txtATTACHMENT_POINT"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>
								<tr id="trVehicleRow" runat="server">
									<td class="headerEffectSystemParams"><asp:label id="Label8" Runat="server">Vehicle Level Coverages</asp:label></td>
								</tr>
								<tr id="trVehicleGridRow" runat="server">
									<td class="midcolora" colSpan="4"><asp:datagrid id="dgCoverages" runat="server" Width="100%" AutoGenerateColumns="False">
											<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
											<ItemStyle CssClass="midcolora"></ItemStyle>
											<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
											<Columns>
												<%--<asp:BoundColumn DataField="COV_DESC" HeaderText="Coverage" ItemStyle-Width="15%"></asp:BoundColumn>--%>
												<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="20%">
													<ItemTemplate>
														<asp:Label ID="lblCOV_DESC" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_TYPE" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_TYPE") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Limit" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblLIMIT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-CssClass="midcolorr" ItemStyle-Width="5%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE" Runat="server" ></asp:Label>
														<asp:Label ID="lblVEHICLE_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.VEHICLE_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblRESERVE_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RESERVE_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblCOV_ID" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="txtOUTSTANDING"  CssClass="INPUTCURRENCY" Runat="server" size="20" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revOUTSTANDING" ControlToValidate="txtOUTSTANDING" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngOUTSTANDING" MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server" Enabled="False"
															Display="Dynamic" ControlToValidate="txtOUTSTANDING"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="txtRI_RESERVE" CssClass="INPUTCURRENCY" Runat="server" size="20" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revRI_RESERVE" ControlToValidate="txtRI_RESERVE" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngRI_RESERVE" MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server" Enabled="False"
															Display="Dynamic" ControlToValidate="txtRI_RESERVE"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reinsurance Carrier" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:DropDownList ID="cmbREINSURANCE_CARRIER" Runat="server"></asp:DropDownList>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="MCCA Attachment Point" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblMCCA_ATTACHMENT_POINT" Runat="server" Text=""></asp:Label>
														<asp:TextBox ID="txtMCCA_ATTACHMENT_POINT" size="10" Runat="server" CssClass="INPUTCURRENCY"  MaxLength="10" ></asp:TextBox><br>														
														<asp:RegularExpressionValidator ID="revMCCA_ATTACHMENT_POINT" ControlToValidate="txtMCCA_ATTACHMENT_POINT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngMCCA_ATTACHMENT_POINT" MinimumValue="1" MaximumValue="9999999999" Type="Currency" Enabled="False"
															Runat="server" Display="Dynamic" ControlToValidate="txtMCCA_ATTACHMENT_POINT"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="MCCA Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblMCCA_APPLIES" Runat="server" Text=""></asp:Label>
														<asp:TextBox ID="txtMCCA_APPLIES" Runat="server" size="10" MaxLength="10"  CssClass="INPUTCURRENCY" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revMCCA_APPLIES" ControlToValidate="txtMCCA_APPLIES" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngMCCA_APPLIES" MinimumValue="1" MaximumValue="9999999999" Type="Currency" Runat="server" Enabled="False"
															Display="Dynamic" ControlToValidate="txtMCCA_APPLIES"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Attachment Point" Visible="False" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="txtATTACHMENT_POINT"  CssClass="INPUTCURRENCY" Runat="server" size="10" MaxLength="10" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revATTACHMENT_POINT" ControlToValidate="txtATTACHMENT_POINT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngATTACHMENT_POINT" MinimumValue="1" MaximumValue="9999999999" Type="Currency" Enabled="False"
															Runat="server" Display="Dynamic" ControlToValidate="txtATTACHMENT_POINT"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>
								<tr>
									<td colspan="4">&nbsp;</td>
								</tr>
								<tr class="midcolora" id="trTotalRow" runat="server">
									<TD class="midcolora" width="25%"><b>
											<asp:textbox class="midcolora" id="txtGrossTotal"  Font-Bold="True" Runat="server" ReadOnly="True"
												BorderWidth="0" Text="Gross Total" BorderStyle="None"></asp:textbox>
										</b>
									</TD>
									<td colSpan="3">
										<table width="100%" border="0">
											<tr>
												<td id="tdFirst" runat="server" width="45%">&nbsp;</td>
												<TD id="tdSecond" runat="server" width="23%"><asp:textbox class="midcolora" id="txtTOTAL_OUTSTANDING"   Runat="server" ReadOnly="True" BorderWidth="0" size="20"
														BorderStyle="None"></asp:textbox></TD>
												<TD id="tdThird" runat="server" width="37%"><asp:textbox id="txtTOTAL_RI_RESERVE"   class="midcolora" Runat="server" ReadOnly="True" BorderWidth="0" size="20"
														 BorderStyle="None"></asp:textbox></TD>
												<TD><asp:textbox id="txtTOTAL_MCCA_APPLIES" class="midcolora" Runat="server"  ReadOnly="True" BorderWidth="0" size="12"
														BorderStyle="None"></asp:textbox></TD>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4"><asp:label ID="capMessage" runat="server"></asp:label></TD><%--Estimation--%>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capRECOVERY" Runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtRECOVERY"  CssClass="INPUTCURRENCY" Runat="server" MaxLength="14"></asp:textbox><br>
										<asp:RegularExpressionValidator ID="revRECOVERY" ControlToValidate="txtRECOVERY" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>																			
										<asp:rangevalidator id="rngRECOVERY" Runat="server" ControlToValidate="txtRECOVERY" Display="Dynamic" Enabled="False" Type="Currency" MaximumValue="99999999999999" MinimumValue="1"></asp:rangevalidator></TD>
									<%--<TD class="midcolora" width="18%"><asp:label id="capPAYMENT_AMOUNT" Runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPAYMENT_AMOUNT" Runat="server" MaxLength="10"></asp:textbox><br>
										<asp:rangevalidator id="rngPAYMENT_AMOUNT" Runat="server" ControlToValidate="txtPAYMENT_AMOUNT" Display="Dynamic"
											Type="Currency" MaximumValue="999999999999" MinimumValue="1"></asp:rangevalidator></TD>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4">Expenses</TD>
								</tr>
								<tr>--%>
									<TD class="midcolora" width="18%"><asp:label id="capEXPENSES" Runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEXPENSES"  CssClass="INPUTCURRENCY" Runat="server" MaxLength="14"></asp:textbox><br>
									<asp:RegularExpressionValidator ID="revEXPENSES" ControlToValidate="txtEXPENSES" Display="Dynamic"  Runat="server" ></asp:RegularExpressionValidator>																			
										<asp:rangevalidator id="rngEXPENSES" Runat="server" ControlToValidate="txtEXPENSES" Display="Dynamic" Enabled="False"
											Type="Currency" MaximumValue="99999999999999" MinimumValue="1"></asp:rangevalidator></TD>
									<%--<td class="midcolora" colSpan="2"></td>--%>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" width="10" colSpan="4">&nbsp;</TD>
								</tr>
								<tr>
									<td class="midcolora" width="18%">
<asp:label ID="lblaccdb" runat="server"></asp:label><span class="mandatory">*</span></td><%--Account to Debit--%>
									<td class="midcolora" width="32%">
										<asp:dropdownlist id="cmbDrAccts" Runat="server" AutoPostBack="False" Width="350"></asp:dropdownlist>
										<br>
										<asp:RequiredFieldValidator Runat="server" ID="rfvDrAccts" ControlToValidate="cmbDrAccts" Display="Dynamic"
											ErrorMessage="Please select account to debit."></asp:RequiredFieldValidator>
									</td>
									<td class="midcolora" width="18%">
<asp:label ID="lblacccr" runat="server"></asp:label><span class="mandatory">*</span></td><%--Account to Credit--%>
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
				<INPUT id="hidPolicyRowCount" type="hidden" name="hidPolicyRowCount" runat="server" value="0">
				<INPUT id="hidDummyPolicyCoverageRowCount" type="hidden" name="hidDummyPolicyCoverageRowCount" runat="server" value="0">
				<INPUT id="hidCLAIM_ID" type="hidden" name="hidCLAIM_ID" runat="server"> <INPUT id="hidACTIVITY_ID" type="hidden" name="hidACTIVITY_ID" runat="server">
				<INPUT id="hidVehicleRowCount" type="hidden" name="hidVehicleRowCount" runat="server" value="0">
				<INPUT id="hidNewReserve" type="hidden" name="hidNewReserve" runat="server" value="0">
				<INPUT id="hidSTATE_ID" type="hidden" name="hidSTATE_ID" runat="server" value="0">
				<INPUT id="hidOldTotalOutstanding" type="hidden" name="hidOldTotalOutstanding" runat="server" value="0">
				<INPUT id="hidOldTotalRiReserve" type="hidden" name="hidOldTotalRiReserve" runat="server" value="0">
				<INPUT id="hidTRANSACTION_ID" type="hidden" name="hidTRANSACTION_ID" runat="server" value="0">
				<INPUT id="hidTRANSACTION_CATEGORY" type="hidden" name="hidTRANSACTION_CATEGORY" runat="server">
			</form>
		</div>
	</body>
</HTML>

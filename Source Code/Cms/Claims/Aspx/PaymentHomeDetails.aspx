<%@ Page language="c#" Codebehind="PaymentHomeDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.PaymentHomeDetails" %>
<%@ Register TagPrefix="webcontrol" TagName="ClaimTop" Src="/cms/cmsweb/webcontrols/ClaimTop.ascx"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
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
			var WatercraftCoveragesRows=0;
			var ctl = "ctl";
			var Section2CoverageGridID='<%=Section2CoverageGridID%>';			
			var Section1CoverageGridID='<%=Section1CoverageGridID%>';			
			var ScheduledItemsCoverageGridID = '<%=ScheduledItemsCoverageGridID%>';			
			var WatercraftCoverageGridID = '<%=WatercraftCoverageGridID%>';
			var Section1Prefix=Section1CoverageGridID + "__" + ctl;
			var Section2Prefix=Section2CoverageGridID + "__" + ctl;			
			var ScheduledItemsCoveragePrefix=ScheduledItemsCoverageGridID + "__" + ctl;
			var WatercraftCoveragesPrefix=WatercraftCoverageGridID + "__" + ctl;
			var ReserveElementPrefix = "_txtRI_RESERVE";
			var OutstandingElementPrefix = "_lblOUTSTANDING";
			var PaymentPrefix = "_txtPAYMENT_AMOUNT";
			var ScheduledPaymentPrefix = "txtPAYMENT_AMOUNT_";
			var ScheduledOutstandingElementPrefix = "lblOUTSTANDING_AMOUNT_";	
			//Added for Itrack Issue 7663
			var RecVehCoverageRows = 0;
			var RecVehCoverageGridID = '<%=RecVehCoverageGridID%>';
			var RecVehCoveragePrefix=RecVehCoverageGridID + "__" + ctl;
			var WaterEquipCoverageRows = 0;
			var WaterEquipItemsCoveragePrefix="WaterEquip__" + ctl;
			var WaterEquipPaymentPrefix = "txtWATER_EQUIP_PAYMENT_AMOUNT_";
			var WaterEquipOutstandingElementPrefix = "lblOUTSTANDING_WATER_EQUIP_AMOUNT_";	
					
			//Commented for Itrack Issue 6635 on 29 Oct 09
			//var ScheduledReserveElementPrefix = "tdRI_RESERVE_";
			
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
			}
			function Init()
			{
				Section1Rows = parseFloat(document.getElementById("hidPolicyRowCount").value)+1;				
				Section2Rows = parseFloat(document.getElementById("hidVehicleRowCount").value)+1;
				Section1Rows = parseFloat(document.getElementById("hidSection1RowCount").value)+1;				
				Section2Rows = parseFloat(document.getElementById("hidSection2RowCount").value)+1;
				ScheduledItemsCoverageRows = parseFloat(document.getElementById("hidScheduledItemRowCount").value)+1;
				WatercraftCoveragesRows = parseFloat(document.getElementById("hidWatercraftRowCount").value)+1;	
				//Added for Itrack Issue 7663							
				WaterEquipCoverageRows = parseFloat(document.getElementById("hidWaterEquipItemRowCount").value)+1;
				RecVehCoverageRows = parseFloat(document.getElementById("hidRecVehRowCount").value)+1;											
				//setfirstTime();	
				//SetMenu();
				//findMouseIn();			
				//top.topframe.main1.mousein = false;	
				//CalculateTotalOutstanding();	
				//CalculateTotalRIReserve();
				CalculateTotalPayment();
				SetTabs();
				ApplyColor();
				ChangeColor();
				cmbPAYMENT_METHOD_Change();
			}
			function SetTabs()
			{
				if(document.getElementById('hidOldData').value != "" && document.getElementById('hidOldData').value!="0")
				{
					Url = "PayeeIndex.aspx?CLAIM_ID=" + document.getElementById('hidCLAIM_ID').value + "&ACTIVITY_ID=" + document.getElementById('hidACTIVITY_ID').value + "&CALLED_FROM=<%=CALLED_FROM_PAYMENT.ToString()%>&";					
					DrawTab(2,top.frames[1],'Payee Details',Url);
					//parent.document.getElementById('cltClaimActivityTop_pnlApplication').style.display="inline";										
					//parent.document.getElementById(parent.ActivityClientID).style.display = "inline";					
					//alert(document.getElementById("txtTOTAL_PAYMENT").value);
					parent.document.getElementById(parent.ActivityTotalPaymentClientID).innerText = formatCurrencyWithCents(document.getElementById("txtTOTAL_PAYMENT").value);
					
				}
				else
				{	
					RemoveTab(2,top.frames[1]);					
					//parent.document.getElementById('cltClaimActivityTop_pnlApplication').style.display="none";
					//parent.document.getElementById(parent.ActivityClientID).style.display = "none";
				}							
			}			
			function CalculateTotal(Prefix,ElementPrefix)
			{
				if(Prefix==Section1Prefix)
					rows = Section1Rows;
				else if(Prefix==Section2Prefix)
					rows = Section2Rows;	
				else if(Prefix==ScheduledItemsCoveragePrefix)
					rows = ScheduledItemsCoverageRows;
				else if(Prefix==WatercraftCoveragesPrefix)
					rows = WatercraftCoveragesRows;
				//Added for Itrack Issue 7663
				else if(Prefix==RecVehCoveragePrefix)
					rows = RecVehCoverageRows;
				else if(Prefix==WaterEquipItemsCoveragePrefix)
					rows = WaterEquipCoverageRows;
				else
					return 0;
				
				var Sum = parseFloat(0);
				for(i=2;i<=rows;i++)
				{
					if(Prefix==ScheduledItemsCoveragePrefix)
						txtID = ElementPrefix + i ;
					else if(Prefix==WaterEquipItemsCoveragePrefix)
						txtID = ElementPrefix + i ;
					else
						txtID = Prefix + i + ElementPrefix;												
					if(document.getElementById(txtID)==null) continue;
					document.getElementById(txtID).value = formatCurrencyWithCents(document.getElementById(txtID).value);
					txtValue = new String(document.getElementById(txtID).value);
					txtValue = txtValue.replace(/\,/g,'');	
					//alert(txtValue);							
					if(txtValue!="" && !isNaN(txtValue))
					{
						Sum+=parseFloat(txtValue);
						//document.getElementById(txtID).value = formatCurrencyWithCents(txtValue);
					}
				}
				return Sum;
			}
			/*function CalculateTotalOutstanding()
			{
				var Sum = parseFloat(0);				
				Sum = CalculateTotal(PolicyPrefix,"_txtOUTSTANDING") + CalculateTotal(VehiclePrefix,"_txtOUTSTANDING");																
				document.getElementById("txtTOTAL_OUTSTANDING").value = formatCurrencyWithCents(Sum);				
								
			}
			function CalculateTotalRIReserve()
			{	
				return;
				var Sum = parseFloat(0);				
				Sum = CalculateTotal(Section1Prefix,ReserveElementPrefix) + CalculateTotal(Section2Prefix,ReserveElementPrefix) + CalculateTotal(ScheduledItemsCoveragePrefix,ReserveElementPrefix) + CalculateTotal(WatercraftCoveragesPrefix,ReserveElementPrefix);
				document.getElementById("txtTOTAL_RI_RESERVE").value = formatCurrencyWithCents(Sum);												
			}*/	
			function GoBack(PageName)
			{
				strURL = PageName + "?&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&";
				this.parent.location.href = strURL;
				return false;
			}
			function GoToClaimDetailPage()
			{
				top.botframe.location.href = "ClaimsTab.aspx?Customer_ID=" + document.getElementById("hidCustomerID").value + "&Policy_ID=" + document.getElementById("hidPolicyID").value + "&Policy_version_ID=" + document.getElementById("hidPolicyVersionID").value + "&Claim_ID=" + document.getElementById("hidCLAIM_ID").value + "&LOB_ID=" + document.getElementById("hidLOB_ID").value;
				return false;
			}			
			function GoToActivity()
			{
				strURL = "ActivityTab.aspx?&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&";
				this.document.location.href = strURL;
				return false;
			}	
			function CompareAllOutstandingAndPayment()
			{
				Page_ClientValidate();
				var FinalResult = true;
				var ReturnValue = true;
				for(i=2;i<=ScheduledItemsCoverageRows;i++)
				{
					//Done for Itrack Issue 6635 on 27 Nov 09
					//txtOutstandingID = ScheduledReserveElementPrefix + i;
					txtOutstandingID = ScheduledOutstandingElementPrefix + i ;	
					txtPaymentID = ScheduledPaymentPrefix + i ;
					ReturnValue = CompareOutstandingAndPayment(txtOutstandingID,txtPaymentID);
					if(!ReturnValue)
					{
						FinalResult = false;						
						return (Page_IsValid && FinalResult);
					}
				}
				
				var FinalResult = true;
				var ReturnValue = true;
				for(i=3;i<=Section1Rows;i++)
				{
					txtOutstandingID = Section1Prefix + i + OutstandingElementPrefix ;	
					txtPaymentID = Section1Prefix + i + PaymentPrefix ;
					ReturnValue = CompareOutstandingAndPayment(txtOutstandingID,txtPaymentID);
					if(!ReturnValue)
					{
						FinalResult = false;						
						return (Page_IsValid && FinalResult);
					}
				}
				
				FinalResult = true;
				ReturnValue = true;
				for(i=2;i<=Section2Rows;i++)
				{
					txtOutstandingID = Section2Prefix + i + OutstandingElementPrefix ;												
					txtPaymentID = Section2Prefix + i + PaymentPrefix ;
					ReturnValue = CompareOutstandingAndPayment(txtOutstandingID,txtPaymentID);
					if(!ReturnValue)
					{
						FinalResult = false;						
						return (Page_IsValid && FinalResult);
					}
				}		
				
				//Added for Itrack Issue 7663
				var FinalResult = true;
				var ReturnValue = true;
				for(i=2;i<=RecVehCoverageRows;i++)
				{
					txtOutstandingID = RecVehCoveragePrefix + i + OutstandingElementPrefix ;	
					txtPaymentID = RecVehCoveragePrefix + i + PaymentPrefix ;
					ReturnValue = CompareOutstandingAndPayment(txtOutstandingID,txtPaymentID);
					if(!ReturnValue)
					{
						FinalResult = false;						
						return (Page_IsValid && FinalResult);
					}
				}
				//Done for Itrack Issue 7663 on 26 Aug 2010
				var FinalResult = true;
				var ReturnValue = true;
				for(i=2;i<=WatercraftCoveragesRows;i++)
				{
					txtOutstandingID = WatercraftCoveragesPrefix + i + OutstandingElementPrefix ;	
					txtPaymentID = WatercraftCoveragesPrefix + i + PaymentPrefix ;
					ReturnValue = CompareOutstandingAndPayment(txtOutstandingID,txtPaymentID);
					
					if(!ReturnValue)
					{
						FinalResult = false;						
						return (Page_IsValid && FinalResult);
					}
				}
				
				var FinalResult = true;
				var ReturnValue = true;
				for(i=2;i<=WaterEquipCoverageRows;i++)
				{
					txtOutstandingID = WaterEquipOutstandingElementPrefix + i ;	
					txtPaymentID = WaterEquipPaymentPrefix + i ;
					ReturnValue = CompareOutstandingAndPayment(txtOutstandingID,txtPaymentID);
					if(!ReturnValue)
					{
						FinalResult = false;						
						return (Page_IsValid && FinalResult);
					}
				}
				
				CalculateTotalPayment();		
				return Page_IsValid;
			}
			function CalculateTotalPayment()
			{
				var Sum = parseFloat(0);
				Sum = CalculateTotal(Section1Prefix,PaymentPrefix) + CalculateTotal(Section2Prefix,PaymentPrefix) + CalculateTotal(ScheduledItemsCoveragePrefix,ScheduledPaymentPrefix) + CalculateTotal(WatercraftCoveragesPrefix,PaymentPrefix) + CalculateTotal(RecVehCoveragePrefix,PaymentPrefix) + CalculateTotal(WaterEquipItemsCoveragePrefix,WaterEquipPaymentPrefix);	//Added for Itrack Issue 7663											
				Sum = Math.round(Sum*10000)/100;
				//alert("Sum  = " + Sum);
				//var amount = Sum;
				/*if(amount != "")
				{
					var dollarpart = amount.substring(0,amount.indexOf('.'));
					var centpart;
					alert(dollarpart);
					if( dollarpart == "")
					{
						centpart = "0" + amount;
						alert(centpart);
					}
					else
					{
						centpart = amount;
					}
					
					document.getElementById(txtID).value = formatCurrencyWithCents(centpart);
				}*/
				document.getElementById("txtTOTAL_PAYMENT").value = formatCurrencyWithCents(Sum);				
				
			}
			function CompareOutstandingAndPayment(txtOutstandingID,txtPaymentID)
			{
				if(document.getElementById(txtOutstandingID)==null || document.getElementById(txtOutstandingID).innerText=="")
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
					var amount = document.getElementById(txtPaymentID).value;
					var dollarpart = amount.substring(0,amount.indexOf('.'));
					var centpart;
					//alert(dollarpart);
					if( dollarpart == "")
					{
						centpart = "0" + amount;
						///alert(centpart);
					}
					else
					{
						centpart = amount;
					}
						
					//document.getElementById(txtPaymentID).value = formatCurrencyWithCents(document.getElementById(txtPaymentID).value);
					document.getElementById(txtPaymentID).value = formatCurrencyWithCents(centpart);
					
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
		</script>
	</HEAD>
	<body onload="Init();" MS_POSITIONING="GridLayout">		
		<div class="pageContent" id="bodyHeight">
			<form id="Form1" method="post" runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%">
					<tr>
						<td class="headereffectCenter" colSpan="4"><asp:label id="lblTitle" runat="server">Indemnity Payment Details</asp:label></td>
					</tr>
					<tr>
						<td class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False" EnableViewState="False"></asp:label></td>
					</tr>
					<TR id="trBody" runat="server">
						<TD colSpan="4">
							<table cellSpacing="0" cellPadding="0" width="100%">
								<%--<tr style="DISPLAY: none">
									<TD class="midcolora" width="18%"><asp:label id="capACTIVITY_DATE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblACTIVITY_DATE" runat="server"></asp:label></TD>
									<TD class="midcolora" colSpan="2"></TD>
								</tr>--%>
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
												<%--<asp:BoundColumn DataField="COV_DESC" HeaderText="Coverage" ItemStyle-Width="15%"></asp:BoundColumn>-->
												<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="20%">
													<ItemTemplate>
														<asp:Label ID="lblCOV_DESC"  Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") >'>
														</asp:Label>
														<asp:Label ID="lblDWELLING_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.DWELLING_ID") >'>
														</asp:Label>
														<asp:Label ID="lblRESERVE_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RESERVE_ID") >'>
														</asp:Label>
														<asp:Label ID="lblCOV_ID"  Runat="server"  Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") >'>
														</asp:Label>
														<asp:Label ID="lblPAYMENT_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.PAYMENT_ID") >'></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Limit" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblLIMIT"  Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT") >'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-CssClass="midcolorr" ItemStyle-Width="5%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE"  Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE","{0:,#,###}") >'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblOUTSTANDING"  Runat="server" ></asp:Label>														
													</ItemTemplate>
												</asp:TemplateColumn>
												<%--<asp:TemplateColumn HeaderText="Paid Loss" ItemStyle-Width="5%">
													<ItemTemplate>														
														<asp:TextBox ID="txtPRIMARY_EXCESS" Runat="server" size="10" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.PRIMARY_EXCESS") >'>></asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>--%>
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblRI_RESERVE"  Runat="server" ></asp:Label>
														<%--<asp:TextBox ID="txtRI_RESERVE" Runat="server" MaxLength="10" size="10" Text='<%# DataBinder.Eval(Container, "DataItem.RI_RESERVE","{0:,#,###}") >'>></asp:TextBox><br>
														<asp:RangeValidator ID="rngRI_RESERVE" MinimumValue="1" MaximumValue="9999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtRI_RESERVE"></asp:RangeValidator>-->
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reinsurance Carrier" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblREINSURANCE_CARRIER"  Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REINSURANCECARRIER","{0:,#,###}") >'>></asp:Label>
														<%--<asp:DropDownList ID="cmbREINSURANCE_CARRIER" Runat="server"></asp:DropDownList>-->
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
												</asp:TemplateColumn>-->
												<%--<asp:TemplateColumn HeaderText="Attachment Point" Visible="False" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="txtATTACHMENT_POINT" CssClass="INPUTCURRENCY"  Runat="server" size="10" MaxLength="10" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revATTACHMENT_POINT" ControlToValidate="txtATTACHMENT_POINT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngATTACHMENT_POINT" MinimumValue="1" MaximumValue="9999999999" Type="Currency"
															Runat="server" Display="Dynamic" ControlToValidate="txtATTACHMENT_POINT" Enabled="False"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>-->
												<asp:TemplateColumn HeaderText="Payment Amount" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="txtPAYMENT_AMOUNT"  CssClass="INPUTCURRENCY" Runat="server" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revPAYMENT_AMOUNT" ControlToValidate="txtPAYMENT_AMOUNT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngPAYMENT_AMOUNT" MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server" Display="Dynamic" ControlToValidate="txtPAYMENT_AMOUNT" Enabled="False"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>
								<tr id="trScheduledItemsCoveragesRow" runat="server">
									<td class="headerEffectSystemParams" colSpan="4"><asp:label id="lblScheduledItemsCoverages" Runat="server">Scheduled Items Coverages</asp:label></td>
								</tr>
								<tr id="trScheduledItemsCoveragesGridRow" runat="server">
									<td  colSpan="4">
										<asp:PlaceHolder ID="plcScheduledCovg" Runat="server"></asp:PlaceHolder>
									<asp:datagrid id="dgScheduledItemsCoveragesGrid" runat="server" AutoGenerateColumns="False" Width="100%" Visible="False">
											<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
											<ItemStyle CssClass="midcolora"></ItemStyle>
											<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="30%">
													<ItemTemplate>
														<asp:Label ID="lblCOV_DESC" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") >'>
														</asp:Label>
														<asp:Label ID="lblRESERVE_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RESERVE_ID") >'>
														</asp:Label>
														<asp:Label IID="lblCOV_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") >'>
														</asp:Label>
														<asp:Label ID="lblPAYMENT_ID"      Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.PAYMENT_ID") >'></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Amount" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblSCHEDULED_ITEM_COVERAGE_AMOUNT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SCHEDULED_ITEM_COVERAGE_AMOUNT","{0:,#,###}") >'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE"  Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE","{0:,#,###}") >'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblOUTSTANDING" Runat="server" ></asp:Label>														
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblRI_RESERVE"  Runat="server" ></asp:Label>
														<%--<asp:TextBox ID="txtRI_RESERVE" Runat="server" MaxLength="10" size="10" Text='<%# DataBinder.Eval(Container, "DataItem.RI_RESERVE","{0:,#,###}") >'>></asp:TextBox>
														<asp:RangeValidator ID="rngRI_RESERVE" MinimumValue="1" MaximumValue="9999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtRI_RESERVE"></asp:RangeValidator>-->
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reinsurance Carrier" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblREINSURANCE_CARRIER" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REINSURANCECARRIER","{0:,#,###}") >'>></asp:Label>
														<%--<asp:DropDownList ID="cmbREINSURANCE_CARRIER" Runat="server"></asp:DropDownList>-->
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Payment Amount" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="txtPAYMENT_AMOUNT"  CssClass="INPUTCURRENCY"  Runat="server" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revPAYMENT_AMOUNT" ControlToValidate="txtPAYMENT_AMOUNT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngPAYMENT_AMOUNT"  MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server" Display="Dynamic" ControlToValidate="txtPAYMENT_AMOUNT" Enabled="False"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>	
											</Columns>
										</asp:datagrid></td>--%>
								</tr>
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
														<asp:Label ID="lblPAYMENT_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.PAYMENT_ID") %>'></asp:Label>
														<asp:Label ID="lblACTUAL_RISK_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_TYPE" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_TYPE") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Included" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblLIMIT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Additional" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE2" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE2","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblOUTSTANDING" Runat="server" ></asp:Label>														
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblRI_RESERVE" Runat="server" ></asp:Label>
														<%--<asp:TextBox ID="txtRI_RESERVE" Runat="server" size="10" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.RI_RESERVE","{0:,#,###}") >'>></asp:TextBox>
														<asp:RangeValidator ID="rngRI_RESERVE" MinimumValue="1" MaximumValue="9999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtRI_RESERVE"></asp:RangeValidator>--%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reinsurance Carrier" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblREINSURANCE_CARRIER" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REINSURANCECARRIER","{0:,#,###}") %>'>></asp:Label>
														<%--<asp:DropDownList ID="cmbREINSURANCE_CARRIER" Runat="server"></asp:DropDownList>--%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Payment Amount" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="txtPAYMENT_AMOUNT"  CssClass="INPUTCURRENCY"  Runat="server" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revPAYMENT_AMOUNT" ControlToValidate="txtPAYMENT_AMOUNT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngPAYMENT_AMOUNT"   MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server" Display="Dynamic" ControlToValidate="txtPAYMENT_AMOUNT" Enabled="False"></asp:RangeValidator>
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
														<asp:Label ID="lblPAYMENT_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.PAYMENT_ID") %>'></asp:Label>
														<asp:Label ID="lblACTUAL_RISK_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_TYPE" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_TYPE") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Included" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblLIMIT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Additional" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE2" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE2","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblOUTSTANDING" Runat="server" ></asp:Label>														
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblRI_RESERVE" Runat="server" ></asp:Label>
														<%--<asp:TextBox ID="txtRI_RESERVE" Runat="server" MaxLength="10" size="10" Text='<%# DataBinder.Eval(Container, "DataItem.RI_RESERVE","{0:,#,###}") >'>></asp:TextBox>
														<asp:RangeValidator ID="rngRI_RESERVE" MinimumValue="1" MaximumValue="9999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtRI_RESERVE"></asp:RangeValidator>--%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reinsurance Carrier" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblREINSURANCE_CARRIER" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REINSURANCECARRIER","{0:,#,###}") %>'>></asp:Label>
														<%--<asp:DropDownList ID="cmbREINSURANCE_CARRIER" Runat="server"></asp:DropDownList>--%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Payment Amount" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="txtPAYMENT_AMOUNT" CssClass="INPUTCURRENCY"   Runat="server" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revPAYMENT_AMOUNT" ControlToValidate="txtPAYMENT_AMOUNT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngPAYMENT_AMOUNT"  MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server" Display="Dynamic" ControlToValidate="txtPAYMENT_AMOUNT" Enabled="False"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>	
											</Columns>
										</asp:datagrid></td>
								</tr>
								<tr id="trScheduledItemsCoveragesRow" runat="server">
									<td class="headerEffectSystemParams" colSpan="4"><asp:label id="lblScheduledItemsCoverages" Runat="server">Scheduled Items Coverages</asp:label></td>
								</tr>
								<tr id="trScheduledItemsCoveragesGridRow" runat="server">
									<td  colSpan="4">
										<asp:PlaceHolder ID="plcScheduledCovg" Runat="server"></asp:PlaceHolder>
									<asp:datagrid id="dgScheduledItemsCoveragesGrid" runat="server" AutoGenerateColumns="False" Width="100%" Visible="False">
											<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
											<ItemStyle CssClass="midcolora"></ItemStyle>
											<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="30%">
													<ItemTemplate>
														<asp:Label ID="lblCOV_DESC" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
														</asp:Label>
														<asp:Label ID="lblRESERVE_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RESERVE_ID") %>'>
														</asp:Label>
														<asp:Label IID="lblCOV_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblPAYMENT_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.PAYMENT_ID") %>'></asp:Label>
														<asp:Label ID="lblACTUAL_RISK_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_TYPE" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_TYPE") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Amount" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblSCHEDULED_ITEM_COVERAGE_AMOUNT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SCHEDULED_ITEM_COVERAGE_AMOUNT","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE"  Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblOUTSTANDING" Runat="server" ></asp:Label>														
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblRI_RESERVE"  Runat="server" ></asp:Label>
														<%--<asp:TextBox ID="txtRI_RESERVE" Runat="server" MaxLength="10" size="10" Text='<%# DataBinder.Eval(Container, "DataItem.RI_RESERVE","{0:,#,###}") >'>></asp:TextBox>
														<asp:RangeValidator ID="rngRI_RESERVE" MinimumValue="1" MaximumValue="9999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtRI_RESERVE"></asp:RangeValidator>--%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reinsurance Carrier" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblREINSURANCE_CARRIER" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REINSURANCECARRIER","{0:,#,###}") %>'>></asp:Label>
														<%--<asp:DropDownList ID="cmbREINSURANCE_CARRIER" Runat="server"></asp:DropDownList>--%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Payment Amount" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="txtPAYMENT_AMOUNT"  CssClass="INPUTCURRENCY"  Runat="server" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revPAYMENT_AMOUNT" ControlToValidate="txtPAYMENT_AMOUNT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngPAYMENT_AMOUNT"  MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server" Display="Dynamic" ControlToValidate="txtPAYMENT_AMOUNT" Enabled="False"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>	
											</Columns>
										</asp:datagrid></td>
								</tr>
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
														<asp:Label ID="lblPAYMENT_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.PAYMENT_ID") %>'></asp:Label>
														<asp:Label ID="lblACTUAL_RISK_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_TYPE" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_TYPE") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Included" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblLIMIT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Additional" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE2" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE2","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblOUTSTANDING" Runat="server" ></asp:Label>														
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblRI_RESERVE" Runat="server" ></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reinsurance Carrier" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblREINSURANCE_CARRIER" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REINSURANCE_CARRIER","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Payment Amount" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="txtPAYMENT_AMOUNT"  CssClass="INPUTCURRENCY"  Runat="server" MaxLength="14" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revPAYMENT_AMOUNT" ControlToValidate="txtPAYMENT_AMOUNT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngPAYMENT_AMOUNT"   MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server" Display="Dynamic" ControlToValidate="txtPAYMENT_AMOUNT" Enabled="False"></asp:RangeValidator>
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
												<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="20%">
													<ItemTemplate>
														<asp:Label ID="lblCOV_DESC"  Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
														</asp:Label>
														<asp:Label ID="lblDWELLING_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.DWELLING_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblRESERVE_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RESERVE_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblCOV_ID"  Runat="server"  Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblPAYMENT_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.PAYMENT_ID") %>'></asp:Label>
														<asp:Label ID="lblACTUAL_RISK_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_TYPE" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_TYPE") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Limit" ItemStyle-CssClass="midcolorr" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblLIMIT"  Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-CssClass="midcolorr" ItemStyle-Width="5%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE"  Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblOUTSTANDING"  Runat="server" ></asp:Label>														
													</ItemTemplate>
												</asp:TemplateColumn>
												<%--<asp:TemplateColumn HeaderText="Paid Loss" ItemStyle-Width="5%">
													<ItemTemplate>														
														<asp:TextBox ID="txtPRIMARY_EXCESS" Runat="server" size="10" MaxLength="10" Text='<%# DataBinder.Eval(Container, "DataItem.PRIMARY_EXCESS") >'>></asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>--%>
												<asp:TemplateColumn HeaderText="RI Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblRI_RESERVE"  Runat="server" ></asp:Label>
														<%--<asp:TextBox ID="txtRI_RESERVE" Runat="server" MaxLength="10" size="10" Text='<%# DataBinder.Eval(Container, "DataItem.RI_RESERVE","{0:,#,###}") >'>></asp:TextBox><br>
														<asp:RangeValidator ID="rngRI_RESERVE" MinimumValue="1" MaximumValue="9999999999" Type="Currency" Runat="server"
															Display="Dynamic" ControlToValidate="txtRI_RESERVE"></asp:RangeValidator>--%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Reinsurance Carrier" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblREINSURANCE_CARRIER"  Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REINSURANCECARRIER","{0:,#,###}") %>'>></asp:Label>
														<%--<asp:DropDownList ID="cmbREINSURANCE_CARRIER" Runat="server"></asp:DropDownList>--%>
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
												<%--<asp:TemplateColumn HeaderText="Attachment Point" Visible="False" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="txtATTACHMENT_POINT" CssClass="INPUTCURRENCY"  Runat="server" size="10" MaxLength="10" ></asp:TextBox><br>
														<asp:RegularExpressionValidator ID="revATTACHMENT_POINT" ControlToValidate="txtATTACHMENT_POINT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
														<asp:RangeValidator ID="rngATTACHMENT_POINT" MinimumValue="1" MaximumValue="9999999999" Type="Currency"
															Runat="server" Display="Dynamic" ControlToValidate="txtATTACHMENT_POINT" Enabled="False"></asp:RangeValidator>
													</ItemTemplate>
												</asp:TemplateColumn>--%>
												<asp:TemplateColumn HeaderText="Payment Amount" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="txtPAYMENT_AMOUNT"  CssClass="INPUTCURRENCY" Runat="server" MaxLength="14" ></asp:TextBox><br>
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
									<td colSpan="4">&nbsp;</td>
								</tr>
								<tr class="midcolora" id="trTotalPayments" runat="server">
									<TD class="midcolora" colSpan="1"><b>Total</b></TD>
									<td class="midcolora" colSpan="3">
										<table width="100%" border="0" >
											<tr>
												<td width="25%" class="midcolora"></td>
												<TD width="59%"><asp:textbox class="midcolora" id="txtTOTAL_OUTSTANDING" Runat="server" ReadOnly="True" BorderWidth="0" size="15"
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
									<td class="midcolora" colspan="3">
										<asp:textbox id="txtCLAIM_DESCRIPTION" Runat="server" Columns="40" Rows="4" TextMode="MultiLine" onkeypress="MaxLength(this,255);"></asp:textbox><br>
										<asp:customvalidator Runat="server" id="csvCLAIM_DESCRIPTION"  Display="Dynamic" ControlToValidate="txtCLAIM_DESCRIPTION" ClientValidationFunction="ValidateLength"></asp:customvalidator></td>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4" width="10">&nbsp;</TD>
								</tr>			
								<%--<tr>									
									<td class="midcolora" width="18%"><asp:Label ID="capACTION_ON_PAYMENT" Runat="server"></asp:Label></td>
									<td class="midcolora" width="32%">
										<asp:DropDownList ID="cmbACTION_ON_PAYMENT" Runat="server"></asp:DropDownList>
									</td>
									<td class="midcolorr" colSpan="2"></td>
								</tr>--%>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPAYMENT_METHOD" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPAYMENT_METHOD" onfocus="SelectComboIndex('cmbPAYMENT_METHOD')" runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvPAYMENT_METHOD" runat="server" ControlToValidate="cmbPAYMENT_METHOD" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<%--<TD class="midcolora" width="50%" colspan="2"></TD>--%>
									<td class="midcolora" width="18%"><asp:label id="capCHECK_NUMBER" Runat="server">Check Number</asp:label></td>
									<td class="midcolora" colSpan="3"><asp:textbox id="txtCHECK_NUMBER" Runat="server" maxlength="20"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revCHECK_NUMBER" Runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtCHECK_NUMBER"></asp:regularexpressionvalidator></TD>
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
				<INPUT id="hidCLAIM_ID" type="hidden" name="hidCLAIM_ID" runat="server"> <INPUT id="hidACTIVITY_ID" type="hidden" name="hidACTIVITY_ID" runat="server">
				<INPUT id="hidSection2RowCount" type="hidden" value="0" name="hidSection2RowCount" runat="server">
				<INPUT id="hidScheduledItemRowCount" type="hidden" value="0" name="hidScheduledItemRowCount" runat="server">
				<INPUT id="hidWatercraftRowCount" type="hidden" value="0" name="hidWatercraftRowCount" runat="server">
				<INPUT id="hidNewReserve" type="hidden" value="0" name="hidNewReserve" runat="server">
				<INPUT id="hidScheduledCovgID" type="hidden" value="" name="hidScheduledCovgID" runat="server">				
				<INPUT id="hidScheduledRsvID" type="hidden" value="" name="hidScheduledRsvID" runat="server">				
				<INPUT id="hidScheduledPmntID" type="hidden" value="" name="hidScheduledPmntID" runat="server">
				<INPUT id="hidVehicleRowCount" type="hidden" name="hidVehicleRowCount" runat="server" value="0">
				<INPUT id="hidPolicyRowCount" type="hidden" name="hidPolicyRowCount" runat="server" value="0">
				<!--Added for Itrack Issue 7663-->
				<INPUT id="hidSTATE_ID" type="hidden" name="hidSTATE_ID" runat="server">
				<INPUT id="hidRecVehRowCount" type="hidden" value="0" name="hidRecVehRowCount" runat="server">
				<INPUT id="hidRecVehCovgID" type="hidden" value="" name="hidRecVehCovgID" runat="server">
				<INPUT id="hidRecVehRsvID" type="hidden" value="" name="hidRecVehRsvID" runat="server">
				<INPUT id="hidWaterEquipItemRowCount" type="hidden" value="0" name="hidWaterEquipItemRowCount" runat="server">
				<INPUT id="hidWaterEquipCovgID" type="hidden" value="" name="hidWaterEquipCovgID" runat="server">
				<INPUT id="hidWaterEquipRsvID" type="hidden" value="" name="hidWaterEquipRsvID" runat="server">
				<INPUT id="hidWaterEquipPmntID" type="hidden" value="" name="hidWaterEquipPmntID" runat="server">
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

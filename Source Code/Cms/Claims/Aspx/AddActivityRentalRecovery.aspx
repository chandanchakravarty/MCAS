<%@ Page language="c#" Codebehind="AddActivityRentalRecovery.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddActivityRentalRecovery" %>
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
			
			var Section2Rows=0;			
			var Section1Rows=0;			
			var ctl = "ctl";
			var Section1RecoveryGridID='<%=Section1RecoveryDataGridID%>';			
			var Section2RecoveryGridID='<%=Section2RecoveryDataGridID%>';			
			var Section2Prefix=Section2RecoveryGridID + "__" + ctl;
			var Section1Prefix=Section1RecoveryGridID + "__" + ctl;
			var OutstandingPrefix = '<%=LabelPrefix%>';
			var RecoveryPrefix = "_txtAMOUNT";
			
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
			function OpenWindow()
			{
				url = "RecoveryTabs.aspx?CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&";
				document.location.href = url;
				return false;
			}
			function CalculateTotal(Prefix,ElementPrefix)
			{
				if(Prefix==Section2Prefix)
					rows = Section2Rows;
				else
					rows = Section1Rows;
					
				var Sum = parseFloat(0);
				for(i=2;i<=rows;i++)
				{
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
			function CalculateTotalRecovery()
			{		
				var Sum = parseFloat(0);
				Sum = CalculateTotal(Section2Prefix,RecoveryPrefix) + CalculateTotal(Section1Prefix,RecoveryPrefix);								
				Sum = Math.round(Sum*10000)/100;
				document.getElementById("txtTOTAL_RECOVERY").value = formatCurrencyWithCents(Sum);				
				
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
				Section2Rows = parseFloat(document.getElementById("hidSection2RowCount").value)+1;
				Section1Rows = parseFloat(document.getElementById("hidSection1RowCount").value)+1;				
				CalculateTotalRecovery();												
				parent.document.getElementById(parent.ActivityTotalPaymentClientID).innerText = formatCurrencyWithCents(document.getElementById("txtTOTAL_RECOVERY").value);
				cmbPAYMENT_METHOD_Change();
				//if(document.getElementById("hidOldData").value=="" || document.getElementById("hidOldData").value=="0")
				//	document.getElementById("btnPaymentBreakdown").style.display="none";
			}
			function CompareAllOutstandingAndRecovery()
			{
				Page_ClientValidate();								
				for(i=2;i<=Section2Rows;i++)
				{
					txtOutstandingID = Section2Prefix + i + OutstandingPrefix;												
					txtPaymentID = Section2Prefix + i + RecoveryPrefix;
					if(!CompareOutstandingAndPayment(txtOutstandingID,txtPaymentID))
						return false;
					
						
				}
				for(i=2;i<=Section1Rows;i++)
				{
					txtOutstandingID = Section1Prefix + i + OutstandingPrefix;												
					txtPaymentID = Section1Prefix + i + RecoveryPrefix
					if(!CompareOutstandingAndPayment(txtOutstandingID,txtPaymentID))
						return false;
				}		
				CalculateTotalRecovery();		
				return true;
			}
			function CompareOutstandingAndPayment(txtOutstandingID,txtPaymentID)
			{	
				if(document.getElementById(txtOutstandingID)==null)
					return true;
				OutstandingValue = new String(document.getElementById(txtOutstandingID).innerText);
				OutstandingValue = OutstandingValue.replace(/\,/g,'');
				PaymentValue = new String(document.getElementById(txtPaymentID).value);
				PaymentValue = PaymentValue.replace(/\,/g,'');				
				if(document.getElementById(txtPaymentID).readOnly)
					return true;
				//Condition hidACTION_ON_RECOVERY = 241  Added For Itrack Issue #6185.				
				if(document.getElementById("hidACTION_ON_RECOVERY").value != "241" && document.getElementById("hidACTION_ON_RECOVERY").value != "254" && document.getElementById("hidACTION_ON_RECOVERY").value != "255" && document.getElementById("hidACTION_ON_RECOVERY").value != "189" && document.getElementById("hidACTION_ON_RECOVERY").value != "190")//Done for Itrack Issue 6078 on 14th Oct 09
				{
					if(!isNaN(parseFloat(PaymentValue)))				
					{
						document.getElementById(txtPaymentID).value = formatCurrencyWithCents(document.getElementById(txtPaymentID).value);					
						if(parseFloat(PaymentValue)>parseFloat(OutstandingValue))
						{
							alert("Recovery amount cannot be greater than Outstanding amount");						
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
					Url = "RecoveryPayerIndex.aspx?CLAIM_ID=" + document.getElementById('hidCLAIM_ID').value + "&ACTIVITY_ID=" + document.getElementById('hidACTIVITY_ID').value + "&CALLED_FROM=<%=CALLED_FROM_PAYMENT.ToString()%>&";					
					DrawTab(2,top.frames[1],'Payer Details',Url);
					//parent.document.getElementById('cltClaimActivityTop_pnlApplication').style.display="none";					
				}
				else
				{	
					RemoveTab(2,top.frames[1]);					
					//parent.document.getElementById('cltClaimActivityTop_pnlApplication').style.display="none";
				}			
			}			
			
		</script>
</HEAD>
	<body MS_POSITIONING="GridLayout" onload="Init();SetTabs();">
		<!-- To add bottom menu -->
		<%--<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>--%>
		<!-- To add bottom menu ends here -->

		<div id="bodyHeight" class="pageContent">
			<form id="Form1" method="post" runat="server">
<TABLE cellSpacing=0 cellPadding=0 width="100%" ?>
 <%-- <TR>
    <TD>
<webcontrol:gridspacer id=grdspacer runat="server"></webcontrol:gridspacer></TD></TR>
<tr>
	<TD id="tdClaimTop" class="pageHeader" colSpan="4">
		<webcontrol:ClaimTop id="cltClaimTop" runat="server" width="100%"></webcontrol:ClaimTop>
	</TD>
</tr>--%>
<tr>
	<TD class="pageHeader" colSpan="4">&nbsp;</TD>
</tr>
  <TR id=trBody runat="server">
    <TD>
				<table cellSpacing="0" cellPadding="0" WIDTH="100%">
								
								<tr>
									<td class="headereffectCenter" colspan="4"><asp:label id="lblTitle" runat="server">Recovery Details</asp:label></td>
								</tr>
								<tr>
									<td class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False" EnableViewState="False"></asp:label></td>
								</tr>
								<%--<tr>
									<TD class="midcolora" width="18%"><asp:label id="capACTIVITY_DATE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblACTIVITY_DATE" runat="server"></asp:label></TD>--
									<td class="midcolorr" colSpan="4"><cmsb:cmsbutton class="clsButton" id="btnGoBack" runat="server" Text="Activity Details"></cmsb:cmsbutton></td>
								</tr>--%>
								<tr id="trSection1Recovery" runat="server">
									<TD class="headerEffectSystemParams" colSpan="4">Section I Recovery</TD>
								</tr>
								<tr>
									<td class="midcolora" colspan="4">
										<asp:datagrid id="dgSection1Recovery" runat="server" AutoGenerateColumns="False"
											Width="100%">
											<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
											<ItemStyle CssClass="midcolora"></ItemStyle>
											<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
											<Columns>																								
												<%--<asp:BoundColumn DataField="COV_DESC" HeaderText="Coverage" ItemStyle-Width="25%"></asp:BoundColumn>--%>												
												<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="25%">
													<ItemTemplate>
														<asp:Label ID="lblCOV_DESC" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC","{0:,#,###}") %>'>></asp:Label>
														<asp:Label ID="lblACTUAL_RISK_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_TYPE" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_TYPE") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												
												<asp:TemplateColumn HeaderText="Included" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDWELLING_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.DWELLING_ID") %>'></asp:Label>
														<asp:Label ID="lblLIMIT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT") %>'>></asp:Label>
														<asp:Label ID="lblRESERVE_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RESERVE_ID") %>'></asp:Label>
														<asp:Label ID="lblRECOVERY_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RECOVERY_ID") %>'></asp:Label>
														<asp:Label ID="lblCOV_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'></asp:Label>
														
													</ItemTemplate>
												</asp:TemplateColumn>	
												<asp:TemplateColumn HeaderText="Additional" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE2" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE2","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblOUTSTANDING" Runat="server"  ></asp:Label>														
													</ItemTemplate>
												</asp:TemplateColumn>
												<%--<asp:TemplateColumn HeaderText="Paid Loss" ItemStyle-Width="10%">
													<ItemTemplate>														
														<asp:Label ID="lblPRIMARY_EXCESS" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PRIMARY_EXCESS") >'>></asp:Label>
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
												<%--<asp:TemplateColumn HeaderText="MCCA Attachment Point" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblMCCA_ATTACHMENT_POINT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MCCA_ATTACHMENT_POINT","{0:,#,###}") >'>></asp:Label>																												
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="MCCA Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblMCCA_APPLIES" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MCCA_APPLIES","{0:,#,###}") >'>></asp:Label>																												
													</ItemTemplate>
												</asp:TemplateColumn>--%>
												<asp:TemplateColumn HeaderText="Recovery Amount" ItemStyle-Width="10%">
													<ItemTemplate>
																									<asp:TextBox ID="txtAMOUNT"  CssClass="INPUTCURRENCY" Runat="server" MaxLength="14"  ></asp:TextBox><br>
														<%--<asp:RangeValidator ID="rngAMOUNT" MinimumValue="1" MaximumValue="9999999999" Type="Currency" Runat="server" Display="Dynamic" ControlToValidate="txtAMOUNT" ></asp:RangeValidator>--%>
														<asp:RegularExpressionValidator ID="revAMOUNT" ControlToValidate="txtAMOUNT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
													</ItemTemplate>
												</asp:TemplateColumn>	
																					
											</Columns>
										</asp:datagrid></td>
								</tr>	
								<tr id="trSection2Recovery" runat="server">
									<TD class="headerEffectSystemParams" colSpan="4">Section II Recovery</TD>
								</tr>
								<tr>
									<td class="midcolora" colspan="4">
										<asp:datagrid id="dgSection2Recovery" runat="server" AutoGenerateColumns="False"
											Width="100%">
											<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
											<ItemStyle CssClass="midcolora"></ItemStyle>
											<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
											<Columns>																								
												<%--<asp:BoundColumn DataField="COV_DESC" HeaderText="Coverage" ItemStyle-Width="25%"></asp:BoundColumn>--%>
												<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="25%">
													<ItemTemplate>
														<asp:Label ID="lblCOV_DESC" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC","{0:,#,###}") %>'>></asp:Label>
														<asp:Label ID="lblACTUAL_RISK_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_ID") %>'>
														</asp:Label>
														<asp:Label ID="lblACTUAL_RISK_TYPE" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ACTUAL_RISK_TYPE") %>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Included" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDWELLING_ID" Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.DWELLING_ID") %>'></asp:Label>
														<asp:Label ID="lblLIMIT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT") %>'>></asp:Label>
														<asp:Label ID="lblRESERVE_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RESERVE_ID") %>'></asp:Label>
														<asp:Label ID="lblRECOVERY_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RECOVERY_ID") %>'></asp:Label>
														<asp:Label ID="lblCOV_ID"  Runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'></asp:Label>
														
													</ItemTemplate>
												</asp:TemplateColumn>	
												<asp:TemplateColumn HeaderText="Additional" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deductible" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblDEDUCTIBLE2" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE2","{0:,#,###}") %>'>></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Outstanding" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblOUTSTANDING" Runat="server"  ></asp:Label>														
													</ItemTemplate>
												</asp:TemplateColumn>
												<%--<asp:TemplateColumn HeaderText="Paid Loss" ItemStyle-Width="10%">
													<ItemTemplate>														
														<asp:Label ID="lblPRIMARY_EXCESS" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PRIMARY_EXCESS") >'>></asp:Label>
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
												<%--<asp:TemplateColumn HeaderText="MCCA Attachment Point" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblMCCA_ATTACHMENT_POINT" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MCCA_ATTACHMENT_POINT","{0:,#,###}") >'>></asp:Label>																												
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="MCCA Reserve" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:Label ID="lblMCCA_APPLIES" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MCCA_APPLIES","{0:,#,###}") >'>></asp:Label>																												
													</ItemTemplate>
												</asp:TemplateColumn>--%>
												<asp:TemplateColumn HeaderText="Recovery Amount" ItemStyle-Width="10%">
													<ItemTemplate>
														<asp:TextBox ID="txtAMOUNT"  CssClass="INPUTCURRENCY" Runat="server" MaxLength="10"  ></asp:TextBox><br>
														<%--<asp:RangeValidator ID="rngAMOUNT" MinimumValue="1" MaximumValue="9999999999" Type="Currency" Runat="server" Display="Dynamic" ControlToValidate="txtAMOUNT" ></asp:RangeValidator>--%>
														<asp:RegularExpressionValidator ID="revAMOUNT" ControlToValidate="txtAMOUNT" Display="Dynamic" Runat="server" Type="Currency"></asp:RegularExpressionValidator>
													</ItemTemplate>
												</asp:TemplateColumn>	
																					
											</Columns>
										</asp:datagrid></td>
								</tr>					
								<tr>
									<td colspan="4">&nbsp;</td>
								</tr>				
								<tr class="midcolora" id="trTotalPayments" runat="server">
									<TD class="midcolora" colSpan="1"><b>Total</b></TD>
									<td class="midcolora" colSpan="3">
										<table width="100%" border="0" >
											<tr>
												<td width="47%"></td>
												<TD width="12%"><asp:textbox class="midcolora" id="txtTOTAL_OUTSTANDING" Runat="server" ReadOnly="True" BorderWidth="0"  size="15"
														BorderStyle="None"></asp:textbox></TD>
												<TD width="25%"><asp:textbox  id="txtTotalRI_Reserve" class="midcolora" Runat="server" ReadOnly="True" BorderWidth="0" size="15"
														BorderStyle="None"></asp:textbox></TD>																								
												<TD ><asp:textbox  id="txtTOTAL_RECOVERY" class="midcolora" Runat="server" ReadOnly="True" BorderWidth="0" size="15"
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
									<%--<td class="midcolora" width="18%"><asp:Label ID="capACTION_ON_RECOVERY" Runat="server"></asp:Label></td>
									<td class="midcolora" width="32%">
										<asp:DropDownList ID="cmbACTION_ON_RECOVERY" Runat="server"></asp:DropDownList>
									</td>
									<td class="midcolorr" colSpan="2"></td>
								</tr>--%>								
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPAYMENT_METHOD" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPAYMENT_METHOD" onfocus="SelectComboIndex('cmbPAYMENT_METHOD')" runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvPAYMENT_METHOD" runat="server" ControlToValidate="cmbPAYMENT_METHOD" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<%--<TD class="midcolora" width="50%" colspan="2"></TD>--%>
									<td class="midcolora" width="18%"><asp:label id="capCHECK_NUMBER" Runat="server">Check Number</asp:label></td>
									<td class="midcolora" colSpan="3"><asp:textbox id="txtCHECK_NUMBER" Runat="server"  maxlength="20"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revCHECK_NUMBER" Runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtCHECK_NUMBER"></asp:regularexpressionvalidator></TD>
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
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Text="Back"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="2">
											<cmsb:cmsbutton class="clsButton" id="btnPaymentBreakdown" Visible="False" runat="server" Text="Payment Breakdown"></cmsb:cmsbutton>
											<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
									</td>
								</tr>					
							</table></TD></TR></TABLE>
						<INPUT id="hidACTION_ON_RECOVERY" type="hidden" name="hidACTION_ON_RECOVERY" value="" runat="server">
						<INPUT id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server">
						<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
						<INPUT id="hidLOB_ID" type="hidden" name="hidLOB_ID" runat="server">
						<INPUT id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server">
						<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server">
						<INPUT id="hidSection2RowCount" type="hidden" name="hidSection2RowCount" runat="server" value="0"> 										
						<INPUT id="hidSection1RowCount" type="hidden" name="hidSection1RowCount" runat="server" value="0"> 										
						<INPUT id="hidCLAIM_ID" type="hidden" name="hidCLAIM_ID" runat="server"> 										
						<INPUT id="hidACTIVITY_ID" type="hidden" name="hidACTIVITY_ID" runat="server"> 										
						<INPUT id="hidPayeeID" type="hidden" name="hidPayeeID" runat="server" >
						<INPUT id="hidTRANSACTION_CATEGORY" type="hidden" name="hidTRANSACTION_CATEGORY" runat="server" value="">
			</form>
		</div>
	</body>
</HTML>

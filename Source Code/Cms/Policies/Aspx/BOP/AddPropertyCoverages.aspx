<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="AddPropertyCoverages.aspx.cs"
    ValidateRequest="false" Inherits="Cms.Policies.Aspx.BOP.AddPropertyCoverages" %>

<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<title>Coverages_Section1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../../cmsweb/scripts/common.js"></script>
		<script src="../../../cmsweb/scripts/form.js"></script>
		<script src="../../../cmsweb/scripts/Coverages.js"></script>
		<script language="javascript">
        
        </script>
	</HEAD>
	<body  MS_POSITIONING="GridLayout" onload=""> 
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr id="trError" runat="server">
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblError" runat="server" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<table cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td class="headereffectCenter"><asp:label id="lblTitle" runat="server">Property Coverages</asp:label></td>
							</tr>
							<tr>
								<td align="center"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False" EnableViewState="False"></asp:label></td>
							</tr>
							<tr>
								<td class="midcolora"><asp:datagrid id="dgCoverages" runat="server" DataKeyField="COVERAGE_ID" AutoGenerateColumns="False"
										Width="100%">
										<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="Opt/Reject" ItemStyle-Width="10%">
												<ItemTemplate>
													<asp:CheckBox id="cbDelete" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' COV_ID='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
													</asp:CheckBox>
													<input type="hidden" id="hidcbDelete" value="" name="hidcbDelete" runat="server" />
													<asp:Label ID="lblLIMIT_TYPE" style="display:none" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT_TYPE") %>'>
													</asp:Label>
													<asp:Label ID="lblDEDUCTIBLE_TYPE" style="display:none" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_TYPE") %>'>
													</asp:Label>
													<asp:Label ID="lblAddDEDUCTIBLE_TYPE" style="display:none" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ADDDEDUCTIBLE_TYPE") %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Coverage">
												<ItemTemplate>
													<asp:Label runat="server" ID="lblCOV_DESC" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Included" ItemStyle-Width="15%">
												<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox ID="txtLIMIT" CssClass="INPUTCURRENCY" MaxLength="10" Visible="False" Runat="server" onBlur="this.value=formatCurrency(this.value);" Text='<%# DataBinder.Eval(Container, "DataItem.INCLUDED","{0:,#,###}") %>'> CssClass="INPUTCURRENCY" MaxLength="6"  ></asp:TextBox>
													<asp:label id="lblNoCoverageLimit"  CssClass="labelfont" Runat="server">No Coverages</asp:label>
													<span id="lblLIMIT" class="LabelFont" Runat="server">
														<%# DataBinder.Eval(Container, "DataItem.INCLUDED_TEXT","{0:,#,###}") %>
													</span><input type="hidden" id="hidLIMIT" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.INCLUDED","{0:,#,###}") %>' NAME="hidLIMIT">
													<select  id="ddlLIMIT" Runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="ddlLIMIT">
													</select>
													<br>
													<asp:RequiredFieldValidator ID="rfvLIMIT" Runat="server" Display="Dynamic" ControlToValidate="txtLIMIT" Enabled="False"></asp:RequiredFieldValidator>
													<asp:rangevalidator id="rngDWELLING_LIMIT" runat="server" ControlToValidate="txtLIMIT" Display="Dynamic"
														Type="Currency" MaximumValue="0" MinimumValue="0" ErrorMessage="Coverage A" Enabled="False"></asp:rangevalidator>
													<asp:RegularExpressionValidator ID="revLIMIT" Runat="server" ControlToValidate="txtLIMIT" Display="Dynamic"></asp:RegularExpressionValidator>
													<asp:CustomValidator ID="csvLIMIT" Runat="server" Display="Dynamic" ErrorMessage="Limit already at max. allowed ($6000)."
														ControlToValidate="txtLIMIT" Enabled="False"></asp:CustomValidator>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Additional" ItemStyle-Width="15%">
												<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<select id="ddladdDEDUCTIBLE" Runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_1") %>' >
													</select>
													<asp:TextBox CssClass="INPUTCURRENCY" MaxLength="8" id="txtDEDUCTIBLE_1_TYPE"  Visible="false" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_1","{0:,#,###}") %>'>
													</asp:TextBox>
													<asp:Label id="lblDEDUCTIBLE" CssClass="labelfont" Visible=False Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_1_DISPLAY","{0:,#,###}") %>'>
													</asp:Label>
													<input id="hidDEDUCTIBLE" type="hidden" value='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_1_DISPLAY","{0:,#,###}") %>' name="hidDEDUCTIBLE" runat="server">
													<asp:label id="lblNoCoverage" CssClass="labelfont" Runat="server">No Coverages</asp:label>
													<BR>
													<asp:RegularExpressionValidator ID="revLIMIT_DEDUC_AMOUNT" Runat="server" ControlToValidate="txtDEDUCTIBLE_1_TYPE"
														Display="Dynamic"></asp:RegularExpressionValidator>
													<asp:RangeValidator ID="rngDEDUCTIBLE" Runat="server" ControlToValidate="txtDEDUCTIBLE_1_TYPE" Display="Dynamic"
														Type="Currency" MaximumValue="200000" MinimumValue="1" Enabled="False"></asp:RangeValidator>
													<asp:CustomValidator ID="csvLIMIT_DEDUC_AMOUNT" Runat="server" Display="Dynamic" ErrorMessage="The maximum combined limit canot exceed $6000."
														ControlToValidate="txtDEDUCTIBLE_1_TYPE" Enabled="False"></asp:CustomValidator>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn Visible="True" HeaderText="Deductible" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
												<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<!--<span id="spnDEDUCTIBLE_AMOUNT_TEXT" class="labelfont" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
														<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_TEXT") %>
													</span>--><INPUT id="hidlbl_DEDUCTIBLE_AMOUNT"  type="hidden" value='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE") %> ' name="hidDEDUCTIBLE_AMOUNT" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
													<asp:Label ID="lblDEDUCTIBLE_AMOUNT" CssClass="labelfont" Visible=False runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_TEXT") %>' COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
													</asp:Label>
													<asp:label id="lblNoaddDEDUCTIBLE" CssClass="labelfont" Runat="server">No Coverages</asp:label>
													<select id="ddlDEDUCTIBLE" Runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'  NAME="ddladdDEDUCTIBLE">
													</select>
													<asp:TextBox CssClass="INPUTCURRENCY" MaxLength="6" id="txtaddDEDUCTIBLE" Visible="false" Runat="server" onBlur="this.value=formatCurrency(this.value);" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE","{0:,#,###}") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn Visible="False" HeaderText="COV_ID">
												<ItemTemplate>
													<asp:Label ID="lblCOV_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="COV_CODE" Visible="True"></asp:BoundColumn>
										</Columns>
									</asp:datagrid></td>
							</tr>
							<tr>
								<td>
									<table width="100%">
										<tr>
											<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Visible="False" Text="Delete" Enabled="False"></cmsb:cmsbutton></td>
											<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td><INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidREC_VEH_ID" type="hidden" value="0" name="hidREC_VEH_ID" runat="server">
									<INPUT id="hidAppID" type="hidden" name="hidAppID" runat="server"> <INPUT id="hidAppVersionID" type="hidden" name="hidAppVersionID" runat="server">
									<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"> <INPUT id="hidAPP_LOB" type="hidden" value="0" name="hidAPP_LOB" runat="server">
									<INPUT id="hidPolcyType" type="hidden" name="hidPolcyType" runat="server"> <INPUT id="hidROW_COUNT" type="hidden" value="0" name="hidAPP_LOB" runat="server">
									<INPUT id="hidCoverageA" type="hidden" name="hidCoverageA" runat="server"> <INPUT id="hidProduct" type="hidden" name="hidProduct" runat="server">
									<INPUT id="hidCustomInfo" type="hidden" name="hidCustomInfo" runat="server"> <input id="hidControlXML" type="hidden" name="hidControlXML" runat="server">
									<input id="hidcbWBSPO" type="hidden" name="hidcbWBSPO" runat="server"> <input id="hidWBSPOEXIST" type="hidden" value="NOTEXIST" name="hidWBSPOEXIST" runat="server">
									<INPUT id="hidREP_COST" type="hidden" name="hidREP_COST" value="0" runat="server">
									<input id="hidStateId" type="hidden" name="hidStateId" runat="server"> 
									<INPUT id="hidHO216" type="hidden" value="0" name="hidHO216" runat="server">
									<input id="hidho42Taken" type="hidden" name="hidho42Taken" runat="server">
                                    <input id="hidLOCATION_ID" type="hidden" name="hidLOCATION_ID" runat="server">
                                    <input id="hidPREMISES_ID" type="hidden" name="hidPREMISES_ID" runat="server">
									<input id="hidEarthQuakeDeductible" type="hidden" name="hidEarthQuakeDeductible" runat="server">
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>

<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="~/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page CodeBehind="PolicyAddLimit1.aspx.cs" validateRequest="false" Language="c#" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Umbrella.PolicyAddLimit1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript">
			function ValidateLength(objSource , objArgs)
			{
				if(document.getElementById('txtOTHER_DESCRIPTION').value.length>65)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			
			
			}
			function ResetForm()
			{
				DisableValidators();
				document.POL_UMBRELLA_LIMITS.reset();
				return false;
			}
			
			function AddData()
			{
				ChangeColor();
				DisableValidators();
				document.getElementById('txtPOLICY_LIMITS').focus();
				document.getElementById('txtPOLICY_LIMITS').value  = '';
				document.getElementById('txtRETENTION_LIMITS').value  = '250.00';
				document.getElementById('txtUNINSURED_MOTORIST_LIMIT').value  = '';
				document.getElementById('txtUNDERINSURED_MOTORIST_LIMIT').value  = '';
				document.getElementById('txtOTHER_LIMIT').value  = '';
				document.getElementById('txtOTHER_DESCRIPTION').value  = '';
			}
			function Initialise()
				{
					if(document.getElementById("cmbPOLICY_LIMITS"))
						document.getElementById("cmbPOLICY_LIMITS").focus();
					ApplyColor();
					ChangeColor();
										
				}
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Initialise();">
		<FORM id="POL_UMBRELLA_LIMITS" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<TD class="pageHeader" colSpan="4">
						<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
					</TD>
				</tr>
				<tr id="trError" runat="server">
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblError" runat="server" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE class="tableWidthHeader" width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="2">Please note that all fields marked with * are 
									mandatory</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="2"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="50%"><asp:label id="capPOLICY_LIMITS" runat="server">Policy Limits    </asp:label><span class="mandatory" id="spnPOLICY_LIMITS" runat="server">*</span></TD>
								<TD class="midcolora" width="50%">
									<asp:dropdownlist id="cmbPOLICY_LIMITS" runat="server"></asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvPOLICY_LIMITS" runat="server" ControlToValidate="cmbPOLICY_LIMITS" Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="50%"><asp:label id="capRETENTION_LIMITS" runat="server">Retention Limits    </asp:label><span class="mandatory" id="spnRETENTION_LIMITS" runat="server">*</span></TD>
								<TD class="midcolora" width="50%"><asp:textbox id="txtRETENTION_LIMITS" runat="server" maxlength="3" size="7" CssClass="INPUTCURRENCY"></asp:textbox><BR>
								<asp:requiredfieldvalidator id="rfvRETENTION_LIMITS" runat="server" ControlToValidate="txtRETENTION_LIMITS" Display="Dynamic"></asp:requiredfieldvalidator>
									<asp:RangeValidator id="rngRETENTION_LIMITS" runat="server" ControlToValidate="txtRETENTION_LIMITS"
										Type="Double" MaximumValue="250" MinimumValue="1" Display="Dynamic"></asp:RangeValidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="50%"><asp:label id="capTERRITORY" runat="server">Territory    </asp:label><span class="mandatory" id="spnTERRITORY" runat="server">*</span></TD>
								<TD class="midcolora" width="50%"><asp:dropdownlist id="cmbTERRITORY" runat="server"></asp:dropdownlist><br>
								<asp:requiredfieldvalidator id="rfvTERRITORY" runat="server" ControlToValidate="cmbTERRITORY" Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="50%"><asp:label id="capCLIENT_UPDATE_DATE" runat="server">Client Update  </asp:label></TD>
								<TD class="midcolora"><asp:TextBox id="txtCLIENT_UPDATE_DATE" runat="server" maxlength="11" size="12"></asp:TextBox>
									<asp:hyperlink id="hlkCLIENT_UPDATE_DATE" runat="server" CssClass="HotSpot">
										<ASP:IMAGE id="imgCLIENT_UPDATE_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
									</asp:hyperlink><br>
									<asp:regularexpressionvalidator id="revCLIENT_UPDATE_DATE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtCLIENT_UPDATE_DATE"></asp:regularexpressionvalidator>
								</TD>
							</tr>
							<tr style="DISPLAY:none">
								<TD class="midcolora" width="50%"><asp:label id="capUNINSURED_MOTORIST_LIMIT" runat="server">Uninsured Motorist Limit   </asp:label></TD>
								<TD class="midcolora" width="50%"><asp:textbox id="txtUNINSURED_MOTORIST_LIMIT" runat="server" maxlength="10" size="18" CssClass="INPUTCURRENCY"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revUNINSURED_MOTORIST_LIMIT" Display="Dynamic" ControlToValidate="txtUNINSURED_MOTORIST_LIMIT"
										Runat="server"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr style="DISPLAY:none">
								<TD class="midcolora" width="50%"><asp:label id="capUNDERINSURED_MOTORIST_LIMIT" runat="server">Underinsured Motorist Limit   </asp:label></TD>
								<TD class="midcolora" width="50%"><asp:textbox id="txtUNDERINSURED_MOTORIST_LIMIT" runat="server" maxlength="10" size="18" CssClass="INPUTCURRENCY"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revUNDERINSURED_MOTORIST_LIMIT" Display="Dynamic" ControlToValidate="txtUNDERINSURED_MOTORIST_LIMIT"
										Runat="server"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr style="DISPLAY:none">
								<TD class="midcolora" width="50%"><asp:label id="capOTHER_LIMIT" runat="server">Other Limit   </asp:label></TD>
								<TD class="midcolora" width="50%"><asp:textbox id="txtOTHER_LIMIT" runat="server" maxlength="10" size="18" CssClass="INPUTCURRENCY"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revOTHER_LIMIT" Display="Dynamic" ControlToValidate="txtOTHER_LIMIT" Runat="server"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr style="DISPLAY:none">
								<TD class="midcolora" width="50%"><asp:label id="capOTHER_DESCRIPTION" runat="server">Other Description</asp:label></TD>
								<TD class="midcolora" width="50%">
									<asp:textbox id="txtOTHER_DESCRIPTION" runat="server" maxlength="65" TextMode="MultiLine" Rows="5"
										Columns="30" onkeypress="MaxLength(this,65);"></asp:textbox><br>
									<asp:customvalidator id="csvOTHER_DESCRIPTION" Runat="server" ControlToValidate="txtOTHER_DESCRIPTION"
										Display="Dynamic" ClientValidationFunction="ValidateLength" ErrorMessage="Error"></asp:customvalidator>
								</TD>
							</tr>
							<tr>
								<td colSpan="2">
									<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
									<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
									<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCustomerID" type="hidden" runat="server" NAME="hidCustomerID">
									<INPUT id="hidPolicyID" type="hidden" runat="server" NAME="hidPolicyID"> <INPUT id="hidPolicyVersionID" type="hidden" runat="server" NAME="hidPolicyVersionID">
									<INPUT id="hidOldCoverageXml" type="hidden" runat="server" NAME="hidOldCoverageXml">
								</td>
							</tr>
							<!--Added By Pravesh for Umbrella Coverages --  Start -->
							<tr>
								<td width="100%" colspan="3">
									<table align="center" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="midcolora" width="100%">
												<asp:datagrid id="dgCoverages" class="midcolora" runat="server" Width="100%" AutoGenerateColumns="False"
													DataKeyField="COVERAGE_ID">
													<AlternatingItemStyle></AlternatingItemStyle>
													<ItemStyle></ItemStyle>
													<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
													<Columns>
														<asp:TemplateColumn HeaderText="Required /Optional" ItemStyle-Width="10%">
															<ItemTemplate>
																<asp:Label ID="lblCOV_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>' Visible="False" >
																</asp:Label>
																<asp:CheckBox ID="cbDelete" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' COV_ID='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>' HAS_MOTORIST_PROTECTION='<%# DataBinder.Eval(Container, "DataItem.HAS_MOTORIST_PROTECTION") %>' LOWER_LIMITS='<%# DataBinder.Eval(Container, "DataItem.LOWER_LIMITS") %>' IS_BOAT_EXCLUDED='<%# DataBinder.Eval(Container, "DataItem.IS_BOAT_EXCLUDED") %>' LOC_EXCLUDED='<%# DataBinder.Eval(Container, "DataItem.LOC_EXCLUDED") %>' IS_EXCLUDED='<%# DataBinder.Eval(Container, "DataItem.IS_EXCLUDED") %>' AUTO_LIABILITY='<%# DataBinder.Eval(Container, "DataItem.AUTO_LIABILITY") %>'>
																</asp:CheckBox>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="50%">
															<ItemTemplate>
																<asp:Label runat="server" ID="lblCOV_DESC" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn ItemStyle-Width="30%" Visible="True">
															<ItemTemplate>
																<asp:Label ID="lblCOV_CODE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' >
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
												</asp:datagrid>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<!--- End 
							-->
							<tr width = "100%">
								<td class="midcolora" width="30%"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" CausesValidation="False"></cmsb:cmsbutton></td>
								<td class="midcolorr" width="50%"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								
							</tr>
							
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</BODY>
</HTML>

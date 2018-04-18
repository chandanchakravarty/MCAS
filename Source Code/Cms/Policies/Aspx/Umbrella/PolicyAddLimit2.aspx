<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="~/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page CodeBehind="PolicyAddLimit2.aspx.cs" validateRequest="false" Language="c#" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Umbrella.PolicyAddLimit2" %>
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
		<script language="javascript">
					function ValidateLength(objSource , objArgs)
					{
						if(document.getElementById('txtCALCULATIONS').value.length>100)
							objArgs.IsValid = false;
						else
							objArgs.IsValid = true;
					
					
					}
					
					function AddData()
					{
						ChangeColor();
						DisableValidators();
						document.getElementById('hidCUSTOMER_ID').value	=	'New';
						document.getElementById('txtBASIC').focus();
						document.getElementById('txtBASIC').value  = '';
						document.getElementById('txtRESIDENCES_OWNER_OCCUPIED').value  = '';
						document.getElementById('txtNUM_OF_RENTAL_UNITS').value  = '';
						document.getElementById('txtRENTAL_UNITS').value  = '';
						document.getElementById('txtNUM_OF_AUTO').value  = '';
						document.getElementById('txtAUTOMOBILES').value  = '';
						document.getElementById('txtNUM_OF_OPERATORS').value  = '';
						document.getElementById('txtOPER_UNDER_AGE').value  = '';
						document.getElementById('txtNUM_OF_UNLIC_RV').value  = '';
						document.getElementById('txtUNLIC_RV').value  = '';
						document.getElementById('txtNUM_OF_UNINSU_MOTORIST').value  = '';
						document.getElementById('txtUNISU_MOTORIST').value  = '';
						document.getElementById('txtUNDER_INSURED_MOTORIST').value  = '';
						document.getElementById('txtWATERCRAFT').value  = '';
						document.getElementById('txtNUM_OF_OTHER').value  = '';
						document.getElementById('txtOTHER').value  = '';
						document.getElementById('txtDEPOSIT').value  = '';
						document.getElementById('txtESTIMATED_TOTAL_PRE').value  = '';
						document.getElementById('txtCALCULATIONS').value  = '';
						
					}

					function ResetForm()
					{
						DisableValidators();
							
						if ( document.POL_UMBRELLA_LIMITS2.hidOldData.value == '' )
						{
							AddData();
						}
						else
						{
						
							populateFormData(document.POL_UMBRELLA_LIMITS2.hidOldData.value,POL_UMBRELLA_LIMITS2);
							
							document.getElementById('txtBASIC').value=formatCurrency(document.getElementById('txtBASIC').value);
							document.getElementById('txtRESIDENCES_OWNER_OCCUPIED').value=formatCurrency(document.getElementById('txtRESIDENCES_OWNER_OCCUPIED').value);
							document.getElementById('txtRENTAL_UNITS').value=formatCurrency(document.getElementById('txtRENTAL_UNITS').value);
							document.getElementById('txtAUTOMOBILES').value=formatCurrency(document.getElementById('txtAUTOMOBILES').value);
							document.getElementById('txtOPER_UNDER_AGE').value=formatCurrency(document.getElementById('txtOPER_UNDER_AGE').value);
							document.getElementById('txtUNLIC_RV').value=formatCurrency(document.getElementById('txtUNLIC_RV').value);
							document.getElementById('txtUNISU_MOTORIST').value=formatCurrency(document.getElementById('txtUNISU_MOTORIST').value);
							document.getElementById('txtUNDER_INSURED_MOTORIST').value=formatCurrency(document.getElementById('txtUNDER_INSURED_MOTORIST').value);
							document.getElementById('txtWATERCRAFT').value=formatCurrency(document.getElementById('txtWATERCRAFT').value);
							document.getElementById('txtOTHER').value=formatCurrency(document.getElementById('txtOTHER').value);
							document.getElementById('txtDEPOSIT').value=formatCurrency(document.getElementById('txtDEPOSIT').value);
							document.getElementById('txtESTIMATED_TOTAL_PRE').value=formatCurrency(document.getElementById('txtESTIMATED_TOTAL_PRE').value);

						}
						
						return false;
					}
					
					
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="ApplyColor();ChangeColor();">
		<div class="pageContent" id="bodyHeight">
			<FORM id="POL_UMBRELLA_LIMITS2" method="post" runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD>
							<TABLE width="100%" align="center" border="0">
								<tr>
									<TD class="pageHeader" colSpan="4">
										<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
										Please note that all fields marked with * are mandatory
									</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="25%"><asp:label id="capBASIC" runat="server">Basic</asp:label></TD>
									<TD class="midcolora" width="25%"><asp:textbox id="txtBASIC" runat="server" CssClass="INPUTCURRENCY" size="18" maxlength="10"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revBASIC" runat="server" Display="Dynamic" ControlToValidate="txtBASIC"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="25%"><asp:label id="capRESIDENCES_OWNER_OCCUPIED" runat="server">Residences  Owner Occupied </asp:label></TD>
									<TD class="midcolora" width="25%"><asp:textbox id="txtRESIDENCES_OWNER_OCCUPIED" runat="server" CssClass="INPUTCURRENCY" size="18"
											maxlength="10"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revRESIDENCES_OWNER_OCCUPIED" runat="server" Display="Dynamic" ControlToValidate="txtRESIDENCES_OWNER_OCCUPIED"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="25%"><asp:label id="capNUM_OF_RENTAL_UNITS" runat="server">No Of Rental Units</asp:label></TD>
									<TD class="midcolora" width="25%"><asp:textbox id="txtNUM_OF_RENTAL_UNITS" runat="server" size="3" maxlength="3"></asp:textbox><br>
										<asp:rangevalidator id="rngNUM_OF_RENTAL_UNITS" runat="server" Display="Dynamic" ControlToValidate="txtNUM_OF_RENTAL_UNITS"
											Type="Integer" MaximumValue="9999" MinimumValue="0"></asp:rangevalidator></TD>
									<TD class="midcolora" width="25%"><asp:label id="capRENTAL_UNITS" runat="server">Rental Units</asp:label></TD>
									<TD class="midcolora" width="25%"><asp:textbox id="txtRENTAL_UNITS" runat="server" CssClass="INPUTCURRENCY" size="18" maxlength="10"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revRENTAL_UNITS" runat="server" Display="Dynamic" ControlToValidate="txtRENTAL_UNITS"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="25%"><asp:label id="capNUM_OF_AUTO" runat="server">No Of Automobiles</asp:label></TD>
									<TD class="midcolora" width="25%"><asp:textbox id="txtNUM_OF_AUTO" runat="server" size="3" maxlength="3"></asp:textbox><br>
										<asp:rangevalidator id="rngNUM_OF_AUTO" runat="server" Display="Dynamic" ControlToValidate="txtNUM_OF_AUTO"
											Type="Integer" MaximumValue="9999" MinimumValue="0"></asp:rangevalidator></TD>
									<TD class="midcolora" width="25%"><asp:label id="capAUTOMOBILES" runat="server">Automobiles</asp:label></TD>
									<TD class="midcolora" width="25%"><asp:textbox id="txtAUTOMOBILES" runat="server" CssClass="INPUTCURRENCY" size="18" maxlength="10"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revAUTOMOBILES" runat="server" Display="Dynamic" ControlToValidate="txtAUTOMOBILES"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="25%"><asp:label id="capNUM_OF_OPERATORS" runat="server"># of Operators </asp:label></TD>
									<TD class="midcolora" width="25%"><asp:textbox id="txtNUM_OF_OPERATORS" runat="server" size="3" maxlength="3"></asp:textbox><br>
										<asp:rangevalidator id="rngNUM_OF_OPERATORS" runat="server" Display="Dynamic" ControlToValidate="txtNUM_OF_OPERATORS"
											Type="Integer" MaximumValue="9999" MinimumValue="0"></asp:rangevalidator></TD>
									<TD class="midcolora" width="25%"><asp:label id="capOPER_UNDER_AGE" runat="server"># of Operators under age of 25 </asp:label></TD>
									<TD class="midcolora" width="25%"><asp:textbox id="txtOPER_UNDER_AGE" runat="server" size="3" maxlength="3"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revOPER_UNDER_AGE" runat="server" Display="Dynamic" ControlToValidate="txtOPER_UNDER_AGE"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="25%"><asp:label id="capNUM_OF_UNLIC_RV" runat="server">No Of Unlicensed Recreational Vehicles   </asp:label></TD>
									<TD class="midcolora" width="25%"><asp:textbox id="txtNUM_OF_UNLIC_RV" runat="server" size="3" maxlength="3"></asp:textbox><br>
										<asp:rangevalidator id="rngNUM_OF_UNLIC_RV" runat="server" Display="Dynamic" ControlToValidate="txtNUM_OF_UNLIC_RV"
											Type="Integer" MaximumValue="9999" MinimumValue="0"></asp:rangevalidator></TD>
									<TD class="midcolora" width="25%"><asp:label id="capUNLIC_RV" runat="server">Unlicensed Recreational Vehicles   </asp:label></TD>
									<TD class="midcolora" width="25%"><asp:textbox id="txtUNLIC_RV" runat="server" CssClass="INPUTCURRENCY" size="18" maxlength="10"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revUNLIC_RV" runat="server" Display="Dynamic" ControlToValidate="txtUNLIC_RV"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="25%"><asp:label id="capNUM_OF_UNINSU_MOTORIST" runat="server">No Of Uninsured Motorist</asp:label></TD>
									<TD class="midcolora" width="25%"><asp:textbox id="txtNUM_OF_UNINSU_MOTORIST" runat="server" size="3" maxlength="3"></asp:textbox><br>
										<asp:rangevalidator id="rngNUM_OF_UNINSU_MOTORIST" runat="server" Display="Dynamic" ControlToValidate="txtNUM_OF_UNINSU_MOTORIST"
											Type="Integer" MaximumValue="9999" MinimumValue="0"></asp:rangevalidator></TD>
									<TD class="midcolora" width="25%"><asp:label id="capUNISU_MOTORIST" runat="server">Uninsured Motorist</asp:label></TD>
									<TD class="midcolora" width="25%"><asp:textbox id="txtUNISU_MOTORIST" runat="server" CssClass="INPUTCURRENCY" size="16" maxlength="10"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revUNISU_MOTORIST" runat="server" Display="Dynamic" ControlToValidate="txtUNISU_MOTORIST"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="25%"><asp:label id="capUNDER_INSURED_MOTORIST" runat="server">Underinsured Motorist</asp:label></TD>
									<TD class="midcolora" width="25%"><asp:textbox id="txtUNDER_INSURED_MOTORIST" runat="server" CssClass="INPUTCURRENCY" size="18"
											maxlength="10"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revUNDER_INSURED_MOTORIST" runat="server" Display="Dynamic" ControlToValidate="txtUNDER_INSURED_MOTORIST"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="25%"><asp:label id="capWATERCRAFT" runat="server">Watercraft</asp:label></TD>
									<TD class="midcolora" width="25%"><asp:textbox id="txtWATERCRAFT" runat="server" CssClass="INPUTCURRENCY" size="18" maxlength="10"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revWATERCRAFT" runat="server" Display="Dynamic" ControlToValidate="txtWATERCRAFT"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="25%"><asp:label id="capNUM_OF_OTHER" runat="server">No Of Other</asp:label></TD>
									<TD class="midcolora" width="25%"><asp:textbox id="txtNUM_OF_OTHER" runat="server" size="3" maxlength="3"></asp:textbox><br>
										<asp:rangevalidator id="rngNUM_OF_OTHER" runat="server" Display="Dynamic" ControlToValidate="txtNUM_OF_OTHER"
											Type="Integer" MaximumValue="9999" MinimumValue="0"></asp:rangevalidator></TD>
									<TD class="midcolora" width="25%"><asp:label id="capOTHER" runat="server">Other</asp:label></TD>
									<TD class="midcolora" width="25%"><asp:textbox id="txtOTHER" runat="server" CssClass="INPUTCURRENCY" size="18" maxlength="10"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revOTHER" runat="server" Display="Dynamic" ControlToValidate="txtOTHER"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="25%"><asp:label id="capDEPOSIT" runat="server">Deposit</asp:label></TD>
									<TD class="midcolora" width="25%"><asp:textbox id="txtDEPOSIT" runat="server" CssClass="INPUTCURRENCY" size="18" maxlength="10"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revDEPOSIT" runat="server" Display="Dynamic" ControlToValidate="txtDEPOSIT"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="25%"><asp:label id="capESTIMATED_TOTAL_PRE" runat="server">Estimated Total Premium   </asp:label></TD>
									<TD class="midcolora" width="25%"><asp:textbox id="txtESTIMATED_TOTAL_PRE" runat="server" CssClass="INPUTCURRENCY" size="18" maxlength="10"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revESTIMATED_TOTAL_PRE" runat="server" Display="Dynamic" ControlToValidate="txtESTIMATED_TOTAL_PRE"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" style="HEIGHT: 89px" width="25%"><asp:label id="capCALCULATIONS" runat="server">Calculations</asp:label></TD>
									<TD class="midcolora" style="HEIGHT: 89px" width="25%"><asp:textbox onkeypress="MaxLength(this,100);" id="txtCALCULATIONS" runat="server" size="18"
											maxlength="100" Columns="30" Rows="5" TextMode="MultiLine"></asp:textbox><br>
										<asp:customvalidator id="csvCALCULATIONS" Runat="server" ControlToValidate="txtCALCULATIONS" Display="Dynamic"
											ClientValidationFunction="ValidateLength" ErrorMessage="Error"></asp:customvalidator>
									</TD>
									<TD class="midcolora" style="HEIGHT: 89px" colSpan="2"></TD>
								</tr>
								<tr>
									<td class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
									<td class="midcolora" colSpan="1"></td>
									<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
								<tr>
									<td colSpan="4">
										<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
										<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server">
										<INPUT id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server"> <INPUT id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server">
									</td>
								</tr>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</FORM>
		</div>
	</BODY>
</HTML>












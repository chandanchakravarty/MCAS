<%@ Page language="c#" Codebehind="AddWatercraftCompany.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddWatercraftCompany" validateRequest=false  %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>CLM_CLAIM_COMPANY</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript">
		
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
			function ResetTheForm()
			{
				DisableValidators();
				document.CLM_CLAIM_COMPANY.reset();				
				return true;
				Init();
				//LoadInsuredContact();
				return false;
			}
			function Init()
			{
				ChangeColor();
				ApplyColor();
				SetComboValueForConcatenatedString("cmbINSURED_CONTACT_ID",document.getElementById("hidINSURED_CONTACT_ID").value,"^","0")
				document.getElementById("txtNAIC_CODE").focus();
			}			
			
			/*function ChkEXPIRATION_DATE(objSource , objArgs)
			{
				var effective_date=document.CLM_CLAIM_COMPANY.txtEFFECTIVE_DATE.value;
				var expiration_date=document.CLM_CLAIM_COMPANY.txtEXPIRATION_DATE.value;
				if (effective_date != "")
				{
					objArgs.IsValid = CompareTwoDate(expiration_date,effective_date,jsaAppDtFormat);
				}
			}*/		
			function LoadInsuredContact()
			{
				
				combo = document.getElementById("cmbINSURED_CONTACT_ID");				
				if(combo.selectedIndex==-1) return;
				//When the user chooses Yes, fetch data and put it in appropriate fields
				if(combo.options[combo.selectedIndex].value!='-1')
				{
					//We obtain the encoded string as follows
					//NAMED_INSURED_ID^NAMED_INSURED^ADDRESS1^ADDRESS2^CITY^STATE^COUNTRY^ZIP_CODE^PHONE
					encoded_string = new String(combo.options[combo.selectedIndex].value);										
					array = encoded_string.split('^');					
					//Traverse through the array and put the values in relavent fields					
					document.getElementById("hidINSURED_CONTACT_ID").value = array[0];					
					document.getElementById("txtCONTACT_NAME").value = array[1];					
					document.getElementById("txtCONTACT_ADDRESS1").value = array[2];
					document.getElementById("txtCONTACT_ADDRESS2").value = array[3];
					document.getElementById("txtCONTACT_CITY").value = array[4];
					SelectComboOption("cmbCONTACT_STATE",array[5]);											
					document.getElementById("txtCONTACT_ZIP").value = array[6];
					document.getElementById("txtCONTACT_HOMEPHONE").value = array[7];	
					document.getElementById("txtCONTACT_WORKPHONE").value = array[8];	
					SelectComboOption("cmbCONTACT_COUNTRY",array[11]);								
					
				}
				else
				{
					document.getElementById("hidINSURED_CONTACT_ID").value = "0";
					document.getElementById("txtCONTACT_NAME").value = "";					
					document.getElementById("txtCONTACT_ADDRESS1").value = "";
					document.getElementById("txtCONTACT_ADDRESS2").value = "";
					document.getElementById("txtCONTACT_CITY").value = "";
					document.getElementById("cmbCONTACT_STATE").selectedIndex = -1;
					//document.getElementById("cmbCONTACT_COUNTRY").selectedIndex = -1;					
					document.getElementById("txtCONTACT_ZIP").value = "";					
					document.getElementById("txtCONTACT_WORKPHONE").value = "";
					document.getElementById("txtCONTACT_HOMEPHONE").value = "";
					
				}
				return false;
				
			}	
			/*function ChkACCIDENT_DATE(objSource , objArgs)
			{
				var expdate=document.CLM_CLAIM_COMPANY.txtACCIDENT_DATE.value;
				objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);				
			}*/			
			
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="CLM_CLAIM_COMPANY" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
										mandatory</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capNAIC_CODE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtNAIC_CODE" runat="server" size="15" maxlength="10"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvNAIC_CODE" runat="server" ControlToValidate="txtNAIC_CODE" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" colSpan="2"></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPOLICY_NUMBER" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPOLICY_NUMBER" class="midcolora" BorderWidth="0" runat="server" size="40"
											maxlength="50" ReadOnly="True"></asp:textbox></TD>
									<TD class="midcolora" width="18%"><asp:label id="capPOLICY_TYPE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPOLICY_TYPE" runat="server" class="midcolora" BorderWidth="0" size="40" maxlength="50"
											ReadOnly="True"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capREFERENCE_NUMBER" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtREFERENCE_NUMBER" runat="server" size="20" maxlength="15"></asp:textbox></TD>
									<TD class="midcolora" width="18%"><asp:label id="capCAT_NUMBER" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCAT_NUMBER" runat="server" size="20" maxlength="15"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capEFFECTIVE_DATE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_DATE" runat="server" class="midcolora" BorderWidth="0" size="40"
											maxlength="50" ReadOnly="True"></asp:textbox>
										<%--<ASP:IMAGE id="imgEFFECTIVE_DATE" runat="server" ImageUrl="../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE><br>
										<asp:regularexpressionvalidator id="revEFFECTIVE_DATE" runat="server" ControlToValidate="txtEFFECTIVE_DATE" Display="Dynamic"
											ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator>--%>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capEXPIRATION_DATE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEXPIRATION_DATE" runat="server" class="midcolora" BorderWidth="0" size="40"
											maxlength="50" ReadOnly="True"></asp:textbox>
										<%--<ASP:IMAGE id="imgEXPIRATION_DATE" runat="server" ImageUrl="../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE><br>
										<asp:regularexpressionvalidator id="revEXPIRATION_DATE" runat="server" ControlToValidate="txtEXPIRATION_DATE" Display="Dynamic"
											ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator><asp:customvalidator id="csvEXPIRATION_DATE" ControlToValidate="txtEXPIRATION_DATE" Display="Dynamic"
											Runat="server" ClientValidationFunction="ChkEXPIRATION_DATE"></asp:customvalidator>--%>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPREVIOUSLY_REPORTED" runat="server">Country</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPREVIOUSLY_REPORTED" onfocus="SelectComboIndex('cmbPREVIOUSLY_REPORTED')"
											runat="server">
											<asp:ListItem Value="0" Selected="True">No</asp:ListItem>
											<asp:ListItem Value="1">Yes</asp:ListItem>
										</asp:dropdownlist></TD>
									<td class="midcolora" colSpan="2"></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capACCIDENT_DATE" runat="server"></asp:label><%--<span class="mandatory">*</span>--%></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtACCIDENT_DATE" runat="server" class="midcolora" BorderWidth="0" size="40"
											maxlength="50" ReadOnly="True"></asp:textbox>
										<%--<asp:hyperlink id="hlkACCIDENT_DATE" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgACCIDENT_DATE" runat="server" ImageUrl="../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
										</asp:hyperlink><br>
										<asp:requiredfieldvalidator id="rfvACCIDENT_DATE" runat="server" Display="Dynamic" ControlToValidate="txtACCIDENT_DATE"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revACCIDENT_DATE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtACCIDENT_DATE"></asp:regularexpressionvalidator>
										<asp:customvalidator id="csvACCIDENT_DATE" ControlToValidate="txtACCIDENT_DATE" Display="Dynamic" ClientValidationFunction="ChkACCIDENT_DATE"
											Runat="server"></asp:customvalidator>--%>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capACCIDENT_TIME" runat="server"></asp:label><%--<span class="mandatory">*</span>--%>
									</TD>
									<td class="midcolora" width="32%"><%--<asp:textbox id="txtACCIDENT_HOUR" runat="server" class="midcolora" BorderWidth="1" runat="server" size="1"
															maxlength="1" ReadOnly="True"></asp:textbox>--%>
										<asp:label id="lblACCIDENT_HOUR" runat="server"></asp:label>
										<asp:label id="capACCIDENT_HOUR" runat="server">HH</asp:label><%--<asp:textbox id="txtACCIDENT_MINUTE" runat="server" class="midcolora" BorderWidth="1" runat="server" size="1"
															maxlength="1" ReadOnly="True"></asp:textbox>--%>
										<asp:label id="lblACCIDENT_MINUTE" runat="server"></asp:label>
										<asp:label id="capACCIDENT_MINUTE" runat="server">MM</asp:label><asp:dropdownlist id="cmbMERIDIEM" onfocus="SelectComboIndex('cmbMERIDIEM')" runat="server"></asp:dropdownlist>
										<asp:label id="lblMERIDIEM" runat="server">MM</asp:label>
										<%--<asp:requiredfieldvalidator id="rfvACCIDENT_HOUR" runat="server" ControlToValidate="txtACCIDENT_HOUR" ErrorMessage=""
											Display="Dynamic"></asp:requiredfieldvalidator>--%>
										<%--<asp:requiredfieldvalidator id="rfvACCIDENT_MINUTE" runat="server" ControlToValidate="txtACCIDENT_MINUTE" ErrorMessage="Effective Time can't be blank.<br>"
											Display="Dynamic"></asp:requiredfieldvalidator>--%>
										<%--<asp:requiredfieldvalidator id="rfvMERIDIEM" runat="server" ControlToValidate="cmbMERIDIEM" ErrorMessage="cmbMERIDIEM can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator>--%>
										<!-- Range Validator -->
										<%--<asp:rangevalidator id="rngACCIDENT_HOUR" runat="server" ControlToValidate="txtACCIDENT_HOUR" MinimumValue="0"
											MaximumValue="12" Type="Integer" Display="Dynamic" ></asp:rangevalidator>--%>
										<%--<asp:rangevalidator id="rngACCIDENT_MINUTE" runat="server" ControlToValidate="txtACCIDENT_MINUTE" MinimumValue="0"
											MaximumValue="59" Display="Dynamic" Type="Integer" ></asp:rangevalidator>--%>
									</td>
								</tr>
								<TR>
									<TD class="headerEffectSystemParams" colSpan="4">Insured Contact</TD>
								</TR>
								<tr id="trINSURED_CONTACT" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capINSURED_CONTACT_ID" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbINSURED_CONTACT_ID" onfocus="SelectComboIndex('cmbINSURED_CONTACT_ID')" runat="server"
											AutoPostBack="True"></asp:dropdownlist></TD>
									<td class="midcolora" colSpan="2"></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCONTACT_NAME" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCONTACT_NAME" runat="server" size="40" maxlength="20"></asp:textbox></TD>
									<TD class="midcolora" colSpan="2"></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCONTACT_ADDRESS1" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCONTACT_ADDRESS1" runat="server" size="40" maxlength="20"></asp:textbox><BR>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capCONTACT_ADDRESS2" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCONTACT_ADDRESS2" runat="server" size="40" maxlength="20"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCONTACT_CITY" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCONTACT_CITY" runat="server" size="20" maxlength="15"></asp:textbox></TD>
									<TD class="midcolora" width="18%"><asp:label id="capCONTACT_COUNTRY" runat="server">Country</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCONTACT_COUNTRY" onfocus="SelectComboIndex('cmbCONTACT_COUNTRY')" runat="server"></asp:dropdownlist></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCONTACT_STATE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCONTACT_STATE" onfocus="SelectComboIndex('cmbCONTACT_STATE')" runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capCONTACT_ZIP" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCONTACT_ZIP" runat="server" size="12" maxlength="10"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revCONTACT_ZIP" runat="server" ControlToValidate="txtCONTACT_ZIP" Display="Dynamic"
											ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCONTACT_HOMEPHONE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCONTACT_HOMEPHONE" runat="server" size="20" maxlength="12"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revCONTACT_HOMEPHONE" runat="server" ControlToValidate="txtCONTACT_HOMEPHONE"
											Display="Dynamic" ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capCONTACT_WORKPHONE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCONTACT_WORKPHONE" runat="server" size="20" maxlength="12"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revCONTACT_WORKPHONE" ControlToValidate="txtCONTACT_WORKPHONE" Display="Dynamic"
											Runat="server"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			<INPUT id="hidAGENCY_ID" type="hidden" value="0" name="hidAGENCY_ID" runat="server">
			<INPUT id="hidINSURED_CONTACT_ID" type="hidden" value="0" name="hidINSURED_CONTACT_ID"
				runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidCOMPANY_ID" type="hidden" value="0" name="hidCOMPANY_ID" runat="server">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidCOMPANY_ID').value,true);			
		</script>
	</BODY>
</HTML>

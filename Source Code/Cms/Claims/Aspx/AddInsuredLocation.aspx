<%@ Page language="c#" Codebehind="AddInsuredLocation.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddInsuredLocation" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Insured Locations</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		function ChkTextAreaLength(source, arguments)
		{
			var txtArea = arguments.Value;
			if(txtArea.length > 500 ) 
			{
				arguments.IsValid = false;
				return;   // invalid userName
			}
		}		
		function FetchLocationData()
		{
			combo = document.getElementById("cmbLOCATION");				
			//Structure of the encoded string is as follows : 
			/* LOCATION_ID^DESCRIPTION^LOC_ADD1^LOC_ADD2^LOC_CITY^LOC_STATE^LOC_ZIP^LOC_COUNTRY^LOC_NUM*/  
			//We will split the user_id field to obtain the relavent information
			if(combo.selectedIndex==-1 || combo.selectedIndex==0) return;
			encoded_string = new String(combo.options[combo.selectedIndex].value);
			
			if(encoded_string.length<1) return;
			array = encoded_string.split('^');
			//Traverse through the array and put the values in relavent fields
			
//				
				document.getElementById("hidLOCATION").value = array[0];
				document.getElementById("txtDESCRIPTION").value = array[1];
				document.getElementById("txtLOC_ADD1").value = array[2];
				document.getElementById("txtLOC_ADD2").value = array[3];
				document.getElementById("txtLOC_CITY").value = array[4];
				//SelectComboOption(comboId,selectedValue)																
				//SelectComboOption('cmbLOC_STATE',array[5]);
				document.getElementById("txtLOC_ZIP").value = array[6];
				SelectComboOption("cmbLOC_COUNTRY",array[7]);
				ChangeColor();
		}		
		function Init()
		{
			if(document.getElementById("hidOldData").value!="" && document.getElementById("hidOldData").value!="0")
			{
				//Set the adjuster dropdown with the hidden value					
				//SetComboValueForConcatenatedString("cmbLOCATION_ID",document.getElementById("hidLOCATION_ID").value,'^',0);
				document.getElementById("trLOCATION").style.display = "none";
			}
			ApplyColor();
			ChangeColor();			
		}
		function ResetTheForm()
		{
			DisableValidators();
			document.INSURED_LOCATIONS.reset();							
			Init();
			return true;
		}		
		
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload='Init();'>
		<FORM id="INSURED_LOCATIONS" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr><%--Added for Itrack Issue 5833 on 21 July 2009--%>
						<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				  </tr>
					<TR id="trBody" runat="server">
						<TD>
							<TABLE width="100%" align="center" border="0">
								<TBODY>
									<tr>
										<TD class="pageHeader" colSpan="4">
											Please note that all fields marked with * are mandatory
										</TD>
									</tr>
									<tr>
										<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
									</tr>
									<tr id="trLOCATION" runat="server">
										<TD class="midcolora" width="18%"><asp:label id="capLOCATION" runat="server">Location</asp:label></TD>
										<TD class="midcolora" colspan="3"><asp:dropdownlist id="cmbLOCATION" onfocus="SelectComboIndex('cmbLOCATION')" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
									</tr>									
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capLOC_ADD1" runat="server">Address1</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtLOC_ADD1" runat="server" maxlength="50" size="35"></asp:textbox><BR>
											<asp:requiredfieldvalidator id="rfvLOC_ADD1" runat="server" Display="Dynamic" ControlToValidate="txtLOC_ADD1"></asp:requiredfieldvalidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capLOC_ADD2" runat="server">Address2</asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtLOC_ADD2" runat="server" maxlength="50" size="35"></asp:textbox></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capLOC_CITY" runat="server">City</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtLOC_CITY" runat="server" maxlength="50" size="35"></asp:textbox><BR>
											<asp:RequiredFieldValidator ID="rfvLOC_CITY" Runat="server" Display="Dynamic" ControlToValidate="txtLOC_CITY"></asp:RequiredFieldValidator>
										</TD>
										<TD class="midcolora" width="18%"><asp:label id="capLOC_COUNTRY" runat="server">Country</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOC_COUNTRY" onfocus="SelectComboIndex('cmbLOC_COUNTRY')" runat="server"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvLOC_COUNTRY" runat="server" Display="Dynamic" ControlToValidate="cmbLOC_COUNTRY"></asp:requiredfieldvalidator>
										</TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capLOC_STATE" runat="server">State</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOC_STATE" onfocus="SelectComboIndex('cmbLOC_STATE')" runat="server"></asp:dropdownlist><BR>
											<asp:requiredfieldvalidator id="rfvLOC_STATE" runat="server" Display="Dynamic" ControlToValidate="cmbLOC_STATE" Enabled ="false"></asp:requiredfieldvalidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capLOC_ZIP" runat="server">Zip</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%">
											<asp:textbox id="txtLOC_ZIP" runat="server" maxlength="10" size="12"></asp:textbox>
											<BR>
											<asp:requiredfieldvalidator id="rfvLOC_ZIP" runat="server" Display="Dynamic" ControlToValidate="txtLOC_ZIP"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revLOC_ZIP" runat="server" Display="Dynamic" ControlToValidate="txtLOC_ZIP"></asp:regularexpressionvalidator></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capDESCRIPTION" runat="server">Description</asp:label></TD>
										<TD class="midcolora" colspan='3'><asp:TextBox ID="txtDESCRIPTION" Runat="server" TextMode="MultiLine" onkeypress="MaxLength(this,500);"
												Rows="8" Columns="150" MaxLength="500"></asp:TextBox><br>
											<asp:customvalidator id="csvDESCRIPTION" Runat="server" ControlToValidate="txtDESCRIPTION" Display="Dynamic"
												ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator>
										</TD>
									</tr>
									<tr>
										<td class="midcolora" colSpan="2">
											<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" CausesValidation="False"></cmsb:cmsbutton>
											<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text='ActivateDeactivate'></cmsb:cmsbutton><%--Added for Itrack Issue 5833 on 20 July 2009--%>
										</td>
										<td class="midcolorr" colSpan="2">
											<cmsb:cmsbutton class="clsButton" id='btnDelete' runat="server" Text='Delete'></cmsb:cmsbutton><%--Added for Itrack Issue 5833 on 20 July 2009--%>
											<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
										</td>
									</tr>
								</TBODY>
							</TABLE>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidLOCATION_ID" type="hidden" value="0" name="hidLOCATION_ID" runat="server">
			<INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			<!--Following hidden variable will be used to store the value of location_id chosen in the drop-down list box-->
			<INPUT id="hidLOCATION" type="hidden" value="0" name="hidLOCATION" runat="server">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidLOCATION_ID').value,false);			
		</script>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</BODY>
</HTML>

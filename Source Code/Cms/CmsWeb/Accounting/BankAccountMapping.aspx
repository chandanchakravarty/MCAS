<%@ Register  TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="BankAccountMapping.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Accounting.BankAccountMapping" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BankAccountMapping</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js">7</script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language='javascript'>
		
		function AddData()
		{
			DisableValidators();
			document.getElementById('hidGL_ID').value	=	'New';
			
			document.getElementById('cmbBnk_Over_Payment').focus();
			document.getElementById('cmbBnk_Suspense_Amount').focus();
			document.getElementById('cmbBnk_Return_Prm_Payment').focus();
			document.getElementById('cmbBNK_CLAIMS_DEFAULT_AC').focus();
			document.getElementById('cmbBNK_REINSURANCE_DEFAULT_AC').focus();
			document.getElementById('cmbBNK_DEPOSITS_DEFAULT_AC').focus();
			
			document.getElementById('cmbBnk_Over_Payment').options.selectedIndex = -1;
			document.getElementById('cmbBnk_Suspense_Amount').options.selectedIndex = -1;
			document.getElementById('cmbBnk_Return_Prm_Payment').options.selectedIndex = -1;
			document.getElementById('cmbBNK_CLAIMS_DEFAULT_AC').options.selectedIndex = -1;
			document.getElementById('cmbBNK_REINSURANCE_DEFAULT_AC').options.selectedIndex = -1;
			document.getElementById('cmbBNK_DEPOSITS_DEFAULT_AC').options.selectedIndex = -1;
			document.getElementById('cmbBNK_MISC_CHK_DEFAULT_AC').options.selectedIndex = -1;
			
			ChangeColor();
		}
		
		function populateXML()
		{
			var tempXML = document.getElementById('hidOldData').value;
			//alert(tempXML);

			if(document.getElementById('hidFormSaved').value == '0')
			{
				AddData();
				if(tempXML!="")
				{
					populateFormData(tempXML,ACT_GENERAL_LEDGER);
				}				
				ChangeColor();
			}
			return false;
		}
		
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload='populateXML();ApplyColor();'>
		<form id="ACT_GENERAL_LEDGER" method="post" runat="server" style="overflow :auto ;width:980px;height:350px"> <%--Added by Aditya for TFS BUG # 1845--%>  
			<table cellSpacing="0" cellPadding="0" width= "100%" border="0">
				<tr>
					<td>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
									mandatory</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							<tr>
								<td class="midcolora" width="25%"><asp:label id="capBnk_Over_Payment" runat="server">Check for Over Payment</asp:label><span class="mandatory">*</span>
								</td>
								<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbBnk_Over_Payment" onfocus="SelectComboIndex('cmbBnk_Over_Payment')" runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvBnk_Over_Payment" runat="server" Display="Dynamic" ErrorMessage="cmbBnk_Over_Payment can't be blank."
										ControlToValidate="cmbBnk_Over_Payment"></asp:requiredfieldvalidator></TD>
								<td class="midcolora" width="25%"><asp:label id="capBNK_CLAIMS_DEFAULT_AC" runat="server">Check for claims Amount</asp:label><span class="mandatory">*</span>
								</td>
								<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbBNK_CLAIMS_DEFAULT_AC" onfocus="SelectComboIndex('cmbBNK_CLAIMS_DEFAULT_AC')"
										runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvBNK_CLAIMS_DEFAULT_AC" runat="server" Display="Dynamic" ErrorMessage="cmbBNK_CLAIMS_DEFAULT_AC can't be blank."
										ControlToValidate="cmbBNK_CLAIMS_DEFAULT_AC"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<td class="midcolora" width="25%"><asp:label id="capBnk_Suspense_Amount" runat="server">Check for Suspense Amount</asp:label><span class="mandatory">*</span>
								</td>
								<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbBnk_Suspense_Amount" onfocus="SelectComboIndex('cmbBnk_Suspense_Amount')"
										runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvBnk_Suspense_Amount" runat="server" Display="Dynamic" ErrorMessage="cmbBnk_Suspense_Amount can't be blank."
										ControlToValidate="cmbBnk_Suspense_Amount"></asp:requiredfieldvalidator></TD>
								<td class="midcolora" width="25%"><asp:label id="capBNK_MISC_CHK_DEFAULT_AC" runat="server">Check for Miscellaneous Payment</asp:label><span class="mandatory">*</span>
								</td>
								<TD class="midcolora" width="25%" colspan="3"><asp:dropdownlist id="cmbBNK_MISC_CHK_DEFAULT_AC" onfocus="SelectComboIndex('cmbBNK_MISC_CHK_DEFAULT_AC')"
										runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvBNK_MISC_CHK_DEFAULT_AC" runat="server" Display="Dynamic" ErrorMessage="cmbBNK_MISC_CHK_DEFAULT_AC can't be blank."
										ControlToValidate="cmbBNK_MISC_CHK_DEFAULT_AC"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<td class="midcolora" width="25%"><asp:label id="capBnk_Return_Prm_Payment" runat="server">Check for Cancellation and Change Premium Payment</asp:label><span class="mandatory">*</span>
								</td>
								<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbBnk_Return_Prm_Payment" onfocus="SelectComboIndex('cmbBnk_Return_Prm_Payment')"
										runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfv_Bnk_Return_Prm_Payment" runat="server" Display="Dynamic" ErrorMessage="cmbBnk_Return_Prm_Payment can't be blank."
										ControlToValidate="cmbBnk_Return_Prm_Payment"></asp:requiredfieldvalidator></TD>
								<td class="midcolora" width="25%"><asp:label id="capBNK_DEPOSITS_DEFAULT_AC" runat="server">Default Deposit Bank Account</asp:label><span class="mandatory">*</span>
								</td>
								<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbBNK_DEPOSITS_DEFAULT_AC" onfocus="SelectComboIndex('cmbBNK_DEPOSITS_DEFAULT_AC')"
										runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvBNK_DEPOSITS_DEFAULT_AC" runat="server" Display="Dynamic" ErrorMessage="cmbBNK_DEPOSITS_DEFAULT_AC can't be blank."
										ControlToValidate="cmbBNK_DEPOSITS_DEFAULT_AC"></asp:requiredfieldvalidator></TD>
							</tr>
							
							
							<tr>
								<td class="midcolora" width="25%"><asp:label id="capCLM_CHECK_DEFAULT_AC" runat="server">Default Account for Claims Check</asp:label><span class="mandatory">*</span></td>
								<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbCLM_CHECK_DEFAULT_AC" onfocus="SelectComboIndex('cmbCLM_CHECK_DEFAULT_AC')"
										runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvCLM_CHECK_DEFAULT_AC" runat="server" Display="Dynamic" ErrorMessage="cmbCLM_CHECK_DEFAULT_AC can't be blank."
										ControlToValidate="cmbCLM_CHECK_DEFAULT_AC"></asp:requiredfieldvalidator></TD>
								
								<td class="midcolora" width="25%"><asp:label id="capBNK_CUST_DEP_EFT_CARD" runat="server">Customer Deposit EFT/Credit Card</asp:label><span class="mandatory">*</span></td>
								<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbBNK_CUST_DEP_EFT_CARD" onfocus="SelectComboIndex('cmbBNK_CUST_DEP_EFT_CARD')"
										runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvBNK_CUST_DEP_EFT_CARD" runat="server" Display="Dynamic" ErrorMessage="cmbBNK_CUST_DEP_EFT_CARD can't be blank."
										ControlToValidate="cmbBNK_CUST_DEP_EFT_CARD"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<td class="midcolora" width="25%"><asp:label id="capBNK_AGEN_CHK_DEFAULT_AC" runat="server">Check for Agency Payment</asp:label><span class="mandatory">*</span></td>
								<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbBNK_AGEN_CHK_DEFAULT_AC" onfocus="SelectComboIndex('cmbBNK_AGEN_CHK_DEFAULT_AC')"
										runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvBNK_AGEN_CHK_DEFAULT_AC" runat="server" Display="Dynamic" ErrorMessage="cmbBNK_AGEN_CHK_DEFAULT_AC can't be blank."
										ControlToValidate="cmbBNK_AGEN_CHK_DEFAULT_AC"></asp:requiredfieldvalidator></TD>
								
								<td class="midcolora" width="25%"><asp:label id="capBNK_REINSURANCE_DEFAULT_AC" runat="server"></asp:label><span class="mandatory">*</span></td>
								<TD class="midcolora" width="25%"><asp:dropdownlist id="cmbBNK_REINSURANCE_DEFAULT_AC" onfocus="SelectComboIndex('cmbBNK_REINSURANCE_DEFAULT_AC')"
										runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvBNK_REINSURANCE_DEFAULT_AC" runat="server" Display="Dynamic" ErrorMessage="cmbBNK_REINSURANCE_DEFAULT_AC can't be blank."
										ControlToValidate="cmbBNK_REINSURANCE_DEFAULT_AC"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<td class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
								<td class="midcolora" colSpan="1"></td>
								<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
						</TABLE>
						<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
						<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
						<INPUT id="hidGL_ID" type="hidden" value="0" name="hidGL_ID" runat="server">
					</td>
				</tr>
			</table>            
		</form>
	</body>
</HTML>

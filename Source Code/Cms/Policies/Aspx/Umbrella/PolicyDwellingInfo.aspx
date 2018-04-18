<%@ Page validateRequest="false" CodeBehind="PolicyDwellingInfo.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Umbrella.PolicyDwellingInfo" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet">
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		
		function ResetForm()
		{
			
			ChangeColor();
			document.POL_UMB_DWELLINGS_INFO.reset();
			DisableValidators();
			return false;
		}
			
			function AddData()
			{
				
				document.getElementById('hidDWELLING_ID').value	=	'0';
				document.getElementById('txtDWELLING_NUMBER').focus();
				document.getElementById('txtDWELLING_NUMBER').value  = '';
				document.getElementById('cmbLOCATION_ID').selectedIndex = -1;
				document.getElementById('txtYEAR_BUILT').value  = '';
				document.getElementById('txtPURCHASE_YEAR').value  = '';
				document.getElementById('txtPURCHASE_PRICE').value  = '';
				document.getElementById('txtMARKET_VALUE').value  = '';
				document.getElementById('txtREPLACEMENT_COST').value  = '';
				document.getElementById('txtREPAIR_COST').value  = '';
				document.getElementById('cmbBUILDING_TYPE').selectedIndex = -1;
				document.getElementById('cmbOCCUPANCY').selectedIndex = -1;
				document.getElementById('txtNEED_OF_UNITS').value  = '';
				document.getElementById('cmbNEIGHBOURS_VISIBLE').selectedIndex = -1;
				document.getElementById('cmbOCCUPIED_DAILY').selectedIndex = -1;
				document.getElementById('txtNO_WEEKS_RENTED').value = '';
				document.getElementById('cmbLOCATION_ID').selectedIndex = -1;
						
				ApplyColor();	
				ChangeColor();
				
			}
					
			
			
			
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
			
	function ChkOccurenceDate(objSource , objArgs)
	{
		if(document.getElementById("revYEAR_BUILT").isvalid==true)
		{
			var effdate=document.POL_UMB_DWELLINGS_INFO .txtYEAR_BUILT.value;

			var date='<%=DateTime.Now.Year%>';
			if(effdate.length<4)
			{
				objArgs.IsValid = false;
			}
			else
			{
				if(effdate > date)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}	
		 }
		 else
			objArgs.IsValid = true;
	}
	
	function ChkPurchaseDate(objSource , objArgs)
	{
	    if(document.getElementById("revPURCHASE_YEAR").isvalid==true )
	    {
			var effdate = parseInt(document.POL_UMB_DWELLINGS_INFO.txtPURCHASE_YEAR.value);

			var date=parseInt('<%=DateTime.Now.Year%>');
			if(effdate.length < 4)
			{
				objArgs.IsValid = false;
			}
			else
			{
				if(parseInt(effdate) <= parseInt(date) && parseInt(effdate) >= parseInt(document.getElementById("txtYEAR_BUILT").value) )
				{
					objArgs.IsValid = true;
				}
				else
				{
					objArgs.IsValid = false;
				}
			}
		}
		else
			objArgs.IsValid = true;
				
	}
	
	
	
	function CheckReplacementCost(objSource , objArgs)
	{
		value = document.getElementById("txtREPLACEMENT_COST").value;
			value = ReplaceString(value, ",","");
			if (value != "")
			{
				if (isNaN(value))
				{
					objArgs.IsValid = false;
					return;
				}
				else
				{
					if (parseFloat(value) == 0)
					{
						objArgs.IsValid = false;
						return;
					}
				}
			}
			objArgs.IsValid = true;
	}
	
	
		

		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="ApplyColor();ChangeColor();">
		<form id="POL_UMB_DWELLINGS_INFO" method="post" runat="server">
			<asp:repeater id="Repeater1" runat="server"></asp:repeater>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr id="trError" runat="server">
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblError" runat="server" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<table>
							<tr>
								<TD class="pageHeader" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow>Please 
									note that all fields marked with * are mandatory
								</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capDWELLING_NUMBER" runat="server">Client Dwelling Number</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtDWELLING_NUMBER" runat="server" maxlength="10" size="15"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvDWELLING_NUMBER" runat="server" Display="Dynamic" ErrorMessage="Please enter the Client dwelling number"
										ControlToValidate="txtDWELLING_NUMBER"></asp:requiredfieldvalidator><asp:rangevalidator id="rngDWELLING_NUMBER" Display="Dynamic" ControlToValidate="txtDWELLING_NUMBER"
										Runat="server" MaximumValue="2147483647" MinimumValue="0" Type="Integer"></asp:rangevalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capLOCATION_ID" runat="server">Location</asp:label></TD>
								<TD class="midcolora" width="32%">
									<asp:label id="lblLOCATION_ID" runat="server"></asp:label>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capYEAR_BUILT" runat="server">Year built</asp:label><span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtYEAR_BUILT" runat="server" maxlength="4" size="5"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvYEAR_BUILT" Display="Dynamic" ControlToValidate="txtYEAR_BUILT" Runat="server"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revYEAR_BUILT" runat="server" Display="Dynamic" ControlToValidate="txtYEAR_BUILT"></asp:regularexpressionvalidator><asp:customvalidator id="csvYEAR_BUILT" Display="Dynamic" ErrorMessage="Year built should be less or equal to current year."
										Enabled="False" ControlToValidate="txtYEAR_BUILT" Runat="server" ClientValidationFunction="ChkOccurenceDate"></asp:customvalidator>
									<asp:rangevalidator id="rngYEAR_BUILT" ControlToValidate="txtYEAR_BUILT" Display="Dynamic" Type="Integer"
										Runat="server" MinimumValue="1900"></asp:rangevalidator>
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="capPURCHASE_YEAR" runat="server">Purchase Year</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtPURCHASE_YEAR" runat="server" maxlength="4" size="5"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revPURCHASE_YEAR" runat="server" Display="Dynamic" ControlToValidate="txtPURCHASE_YEAR"></asp:regularexpressionvalidator><asp:customvalidator id="csvPurchase_YEAR" runat="server" Display="Dynamic" ErrorMessage="Purchase year should be between year built and current year."
										ControlToValidate="txtPURCHASE_YEAR" ClientValidationFunction="ChkPurchaseDate" Enabled="True"></asp:customvalidator>
									<asp:rangevalidator id="rngPURCHASE_YEAR" ControlToValidate="txtPURCHASE_YEAR" Display="Dynamic" Type="Integer"
										Runat="server" MinimumValue="1900"></asp:rangevalidator>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capPURCHASE_PRICE" runat="server">Purchase price</asp:label></TD>
								<TD class="midcolora" width="32%">
									<asp:textbox id="txtPURCHASE_PRICE" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="20"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revPURCHASE_PRICE" runat="server" Display="Dynamic" ControlToValidate="txtPURCHASE_PRICE"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capMARKET_VALUE" runat="server">Market Value</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtMARKET_VALUE" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="20"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revMARKET_VALUE" runat="server" Display="Dynamic" ControlToValidate="txtMARKET_VALUE"></asp:regularexpressionvalidator>
									<asp:customvalidator id="csvMARKET_VALUE" Runat="server" Display="Dynamic" ControlToValidate="txtREPLACEMENT_COST"></asp:customvalidator>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capREPLACEMENT_COST" runat="server">Replacement Cost</asp:label><span class="mandatory" id="spnREPLACEMENT_COST">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtREPLACEMENT_COST" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="20"></asp:textbox><br>
									<asp:comparevalidator id="cmpREPLACEMENT_COST" Display="Dynamic" ControlToValidate="txtREPLACEMENT_COST"
										Runat="server" Type="Currency" Operator="GreaterThanEqual" ValueToCompare="0"></asp:comparevalidator><asp:requiredfieldvalidator id="rfvREPLACEMENT_COST" runat="server" Display="Dynamic" ControlToValidate="txtREPLACEMENT_COST"></asp:requiredfieldvalidator>
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="capREPAIR_COST" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtREPAIR_COST" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="20"></asp:textbox><br>
									<asp:regularexpressionvalidator id="revREPAIR_COST" runat="server" Display="Dynamic" ControlToValidate="txtREPAIR_COST"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capBUILDING_TYPE" runat="server">Building type</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbBUILDING_TYPE" onfocus="SelectComboIndex('cmbBUILDING_TYPE')" runat="server"></asp:dropdownlist>
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="capOCCUPANCY" runat="server">Occupancy</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbOCCUPANCY" onfocus="SelectComboIndex('cmbOCCUPANCY')" runat="server"></asp:dropdownlist></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capNEED_OF_UNITS" runat="server">Need # of families/units </asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtNEED_OF_UNITS" runat="server" maxlength="2" size="3"></asp:textbox><br>
									<asp:rangevalidator id="rngNEED_OF_UNITS" runat="server" Display="Dynamic" ControlToValidate="txtNEED_OF_UNITS"
										MaximumValue="100" MinimumValue="1" Type="Integer"></asp:rangevalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capNEIGHBOURS_VISIBLE" runat="server">Visible to neighbors</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbNEIGHBOURS_VISIBLE" onfocus="SelectComboIndex('cmbNEIGHBOURS_VISIBLE')" runat="server">
										<asp:ListItem></asp:ListItem>
										<asp:ListItem Value="Y">Yes</asp:ListItem>
										<asp:ListItem Value="N">No</asp:ListItem>
									</asp:dropdownlist></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capOCCUPIED_DAILY" runat="server">Occupied daily</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbOCCUPIED_DAILY" onfocus="SelectComboIndex('cmbOCCUPIED_DAILY')" runat="server">
										<asp:ListItem></asp:ListItem>
										<asp:ListItem Value="Y">Yes</asp:ListItem>
										<asp:ListItem Value="N">No</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"><asp:label id="capNO_WEEKS_RENTED" runat="server"># of weeks rented</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtNO_WEEKS_RENTED" runat="server" maxlength="2" size="3"></asp:textbox><br>
									<asp:rangevalidator id="rngNO_WEEKS_RENTED" runat="server" Display="Dynamic" ControlToValidate="txtNO_WEEKS_RENTED"
										MaximumValue="100" MinimumValue="0" Type="Integer"></asp:rangevalidator></TD>
							</tr>
							<tr>
								<td class="midcolora" colSpan="2">
									<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" CausesValidation="False"></cmsb:cmsbutton>
								</td>
								<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
						</table>
						<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
						<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
						<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidDWELLING_ID" type="hidden" value="0" name="hidDWELLING_ID" runat="server">
						<INPUT id="hidPolicyID" type="hidden" name="hidAppID" runat="server"> <INPUT id="hidPolicyVersionID" type="hidden" name="hidAppVersionID" runat="server">
						<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"> <INPUT id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server">
						<INPUT id="hidCheckDelete" type="hidden" name="hidCheckDelete" runat="server"> <INPUT id="hidPolicyType" type="hidden" name="hidPolicyType" runat="server">
						<INPUT id="hidStateId" type="hidden" name="hidStateId" runat="server"> <INPUT id="hidPercent" type="hidden" name="hidPercent" runat="server">
						<INPUT id="hidLOCATION_ID" type="hidden" name="hidLOCATION_ID" runat="server">
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>



<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="LimitsOfAuthorityDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Claims.LimitsOfAuthorityDetails" validateRequest=false  %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>MNT_AGENCY_LIST</title>
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

			function AddData()
			{
				ChangeColor();
				DisableValidators();
				document.getElementById('hidLIMIT_ID').value	=	'New';
				//document.getElementById('txtAUTHORITY_LEVEL').value = '';
				document.getElementById('txtTITLE').value = '';
				document.getElementById('txtPAYMENT_LIMIT').value = '';
				document.getElementById('txtRESERVE_LIMIT').value = '';	
				document.getElementById('chkCLAIM_ON_DUMMY_POLICY').checked=false;		
				try
				{		
					document.getElementById('txtAUTHORITY_LEVEL').focus();
				}
				catch(ex)
				{	}
			}
			
			function populateXML() {
			    
				if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
				{
					var tempXML;
					if(document.getElementById("hidOldData").value!="")
					{
					    tempXML = document.getElementById("hidOldData").value;
					    //alert(tempXML);											
						populateFormData(tempXML,CLM_AUTHORITY_LIMIT);	
						if(document.getElementById('btnDelete')!=null)
						    document.getElementById('btnDelete').setAttribute('disabled', false);
						
			document.getElementById('txtPAYMENT_LIMIT').value = document.getElementById('hidPAYMENT_AMOUNT').value;
			document.getElementById('txtRESERVE_LIMIT').value = document.getElementById('hidRESERVE_AMOUNT').value;
			//alert(formatBaseCurrencyAmount(document.getElementById('hidREOPEN_CLAIM_LIMIT').value,2));
			document.getElementById('txtREOPEN_CLAIM_LIMIT').value = formatBaseCurrencyAmount(document.getElementById('hidREOPEN_CLAIM_LIMIT').value,2);
			document.getElementById('txtGRATIA_CLAIM_AMOUNT').value = formatBaseCurrencyAmount(document.getElementById('hidGRATIA_CLAIM_AMOUNT').value,2);
			
            						
						document.getElementById("txtAUTHORITY_LEVEL").readOnly = true;
					}
					else
					{
						AddData();
						if(document.getElementById('btnDelete')!=null)
							document.getElementById('btnDelete').setAttribute('disabled',true);
					}					
				}
				else
				{
					if(document.getElementById('hidOldData').value=="")
					{
						if(document.getElementById('btnDelete')!=null)
							document.getElementById('btnDelete').setAttribute('disabled',true);
					}	
					document.getElementById("txtAUTHORITY_LEVEL").readOnly = false;
				}	
				try
				{
					document.getElementById("txtAUTHORITY_LEVEL").focus();			
				}
				catch(er)
				{ }
				return false;
                }

                function FormatAmountForSum(num) {

                    num = ReplaceAll(num, sBaseDecimalSep, '.');
                    return num;
                }


                function validateAmount(objSource, objArgs) {
                    if (document.getElementById('revRESERVE_LIMIT').isvalid == false||document.getElementById('revPAYMENT_LIMIT').isvalid == false)
                        return 
			   
                    var Limt = document.getElementById(objSource.controltovalidate).value;
                    
                    Limt = FormatAmountForSum(Limt);
                    if (parseFloat(Limt) > 0) {
                        objArgs.IsValid = true;
                    }
                    else
                        objArgs.IsValid = false;
                }

                function validateLengthOfAmount(objSource, objArgs) {
                    if (document.getElementById('revRESERVE_LIMIT').isvalid == false ||document.getElementById('revPAYMENT_LIMIT').isvalid == false)
                        return 
                    var amtlength = document.getElementById(objSource.controltovalidate).value;
                        if (parseFloat(amtlength) > parseFloat('9999999999999.99')) {
                        objArgs.IsValid = false;
                    }
                    else {
                        objArgs.IsValid = true;
                    }
                }

                function ResetForm() {
                    document.CLM_AUTHORITY_LIMIT.reset();
                    DisableValidators();
                    populateXML();
                    ChangeColor();
                    return false;
                }
                
 </script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();">
		<FORM id="CLM_AUTHORITY_LIMIT" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capAUTHORITY_LEVEL" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtAUTHORITY_LEVEL" runat="server" maxlength="3" size="8"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvAUTHORITY_LEVEL" runat="server" Display="Dynamic" ControlToValidate="txtAUTHORITY_LEVEL"></asp:requiredfieldvalidator>
										<asp:rangevalidator id="rngAUTHORITY_LEVEL" ControlToValidate="txtAUTHORITY_LEVEL" Display="Dynamic"
											Runat="server" Type="Integer" MinimumValue="1" MaximumValue="999"></asp:rangevalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capTITLE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtTITLE" runat="server" MaxLength="50" size="40"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvTITLE" runat="server" Display="Dynamic" ControlToValidate="txtTITLE"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPAYMENT_LIMIT" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPAYMENT_LIMIT" runat="server" maxlength="16" size="20" CssClass="INPUTCURRENCY"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvPAYMENT_LIMIT" runat="server" Display="Dynamic" ControlToValidate="txtPAYMENT_LIMIT"></asp:requiredfieldvalidator>
										<asp:RegularExpressionValidator  ID="revPAYMENT_LIMIT" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtPAYMENT_LIMIT"></asp:RegularExpressionValidator>
                                    <asp:CustomValidator ID="csvPAYMENT_LIMIT" Display="Dynamic" ControlToValidate="txtPAYMENT_LIMIT"
                                                         ClientValidationFunction="validateAmount" runat="server"></asp:CustomValidator>
                                    <asp:CustomValidator ID="csvPAYMENT_LIMIT_MaxAmt" Display="Dynamic" ControlToValidate="txtPAYMENT_LIMIT"
                                                         ClientValidationFunction="validateLengthOfAmount" runat="server"></asp:CustomValidator>
                                                          
                                                         
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capRESERVE_LIMIT" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtRESERVE_LIMIT" runat="server" maxlength="16" size="20" CssClass="INPUTCURRENCY"></asp:textbox><br>
										<asp:RegularExpressionValidator  ID="revRESERVE_LIMIT" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtRESERVE_LIMIT"></asp:RegularExpressionValidator>
                                    <asp:CustomValidator ID="csvRESERVE_LIMIT" Display="Dynamic" ControlToValidate="txtRESERVE_LIMIT"
                                                         ClientValidationFunction="validateAmount" runat="server"></asp:CustomValidator>
                                  <asp:CustomValidator ID="csvRESERVE_LIMIT_MaxAmt" Display="Dynamic" ControlToValidate="txtRESERVE_LIMIT"
                                                         ClientValidationFunction="validateLengthOfAmount" runat="server"></asp:CustomValidator>
									</TD>
								</tr>
                                <!-- Added by Agniswar for Singapore Implementation -->
                                <tr id="trRowCLAIM" runat="server" visible="true">
									<TD class="midcolora" width="18%"><asp:label id="capREOPEN_CLAIM_LIMIT" runat="server"></asp:label><span id="spnREOPEN_CLAIM_LIMIT" runat="server" class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtREOPEN_CLAIM_LIMIT" runat="server" maxlength="16" size="20" CssClass="INPUTCURRENCY"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvREOPEN_CLAIM_LIMIT" runat="server" Display="Dynamic" ControlToValidate="txtREOPEN_CLAIM_LIMIT"></asp:requiredfieldvalidator>
										<asp:RegularExpressionValidator  ID="revREOPEN_CLAIM_LIMIT" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtREOPEN_CLAIM_LIMIT"></asp:RegularExpressionValidator>
                                    <asp:CustomValidator ID="csvREOPEN_CLAIM_LIMIT" Display="Dynamic" ControlToValidate="txtREOPEN_CLAIM_LIMIT"
                                                         ClientValidationFunction="validateAmount" runat="server"></asp:CustomValidator>
                                    <asp:CustomValidator ID="csvREOPEN_CLAIM_LIMIT_MaxAmt" Display="Dynamic" ControlToValidate="txtREOPEN_CLAIM_LIMIT"
                                                         ClientValidationFunction="validateLengthOfAmount" runat="server"></asp:CustomValidator>
                                                          
                                                         
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capGRATIA_CLAIM_AMOUNT" runat="server"></asp:label><span id="spnGRATIA_CLAIM_AMOUNT" runat="server" class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtGRATIA_CLAIM_AMOUNT" runat="server" maxlength="16" size="20" CssClass="INPUTCURRENCY"></asp:textbox><br>
										<asp:RegularExpressionValidator  ID="revGRATIA_CLAIM_AMOUNT" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtGRATIA_CLAIM_AMOUNT"></asp:RegularExpressionValidator>
                                    <asp:CustomValidator ID="csvGRATIA_CLAIM_AMOUNT" Display="Dynamic" ControlToValidate="txtGRATIA_CLAIM_AMOUNT"
                                                         ClientValidationFunction="validateAmount" runat="server"></asp:CustomValidator>
                                  <asp:CustomValidator ID="csvGRATIA_CLAIM_AMOUNT_MaxAmt" Display="Dynamic" ControlToValidate="txtGRATIA_CLAIM_AMOUNT"
                                                         ClientValidationFunction="validateLengthOfAmount" runat="server"></asp:CustomValidator>
									</TD>
								</tr>
                                <!-- Till here -->
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCLAIM_ON_DUMMY_POLICY" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:CheckBox ID="chkCLAIM_ON_DUMMY_POLICY" Runat="server"></asp:CheckBox>
									</TD>
									<TD class="midcolora" colspan='2'></TD>
								</tr>
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>&nbsp;<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" visible="false"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnDelete" visible="false" runat="server" Text="Delete" causesvalidation="false"></cmsb:cmsbutton>
									</td>
									<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidPAYMENT_AMOUNT" type="hidden" value="" name="hidPAYMENT_AMOUNT" runat="server">
			<INPUT id="hidRESERVE_AMOUNT" type="hidden" value="" name="hidRESERVE_AMOUNT" runat="server">

            <INPUT id="hidREOPEN_CLAIM_LIMIT" type="hidden" value="" name="hidREOPEN_CLAIM_LIMIT" runat="server">
			<INPUT id="hidGRATIA_CLAIM_AMOUNT" type="hidden" value="" name="hidGRATIA_CLAIM_AMOUNT" runat="server">

			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidLIMIT_ID" type="hidden" value="0" name="hidLIMIT_ID" runat="server">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidLIMIT_ID').value,true);			
		</script>
	</BODY>
</HTML>

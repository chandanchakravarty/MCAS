<%@ Page language="c#" Codebehind="AddBudgetCatgory.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Accounting.AddBudgetCatgory"  validateRequest="false"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>ACT_BUDGET_CATEGORY</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language='javascript'>
function AddData()
{	
	ChangeColor();
	DisableValidators();	
	document.getElementById('hidCATEGEORY_ID').value	=	'New';	
	//added by uday 	
	try
	{	 
	 document.getElementById('txtCATEGEORY_CODE').focus();
	}
	catch(e)
	{
	 return false;
	}	
	//document.getElementById('txtCATEGEORY_CODE').focus();
	document.getElementById('txtCATEGEORY_CODE').value  = '';
	document.getElementById('txtCATEGORY_DEPARTEMENT_NAME').value  = '';
	document.getElementById('txtRESPONSIBLE_EMPLOYEE_NAME').value  = '';
	//Check whether the object is null or not
	if(document.getElementById("btnActivateDeactivate")!=null)
	document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
}
function populateXML()
{
	if(document.getElementById('hidFormSaved').value == '0')
	{
		var tempXML;
		if(document.getElementById('hidOldData').value!="")
		{
				
			//Enabling the activate deactivate button
			document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
					
		}
		else
		{
			AddData();
		}
	}
return false;
}

function ResetForm()
{
	document.ACT_BUDGET_CATEGORY.reset();
	DisableValidators();
	ChangeColor();
	return false;
	
}
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin='0' topMargin='0' onload='ApplyColor();populateXML();'>
		<FORM id='ACT_BUDGET_CATEGORY' method='post' runat='server'>
			<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
				<TR>
					<TD>
						<TABLE width='100%' border='0' align='center'>
							<tr>
								<TD class="pageHeader" colSpan="4"><asp:Label runat="server" ID="capMessages"></asp:Label></TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capCATEGEORY_CODE" runat="server">Code</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtCATEGEORY_CODE' runat='server' size='3' maxlength='2'></asp:textbox><br>
									<asp:RangeValidator id="rnvCode" runat="server" Display="Dynamic" ControlToValidate="txtCATEGEORY_CODE"
										ErrorMessage="Please enter numeric code" MaximumValue="99" MinimumValue="1" Type="Integer"></asp:RangeValidator>
									<asp:requiredfieldvalidator id="rfvCATEGEORY_CODE" runat="server" ControlToValidate="txtCATEGEORY_CODE" Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capCATEGORY_DEPARTEMENT_NAME" runat="server">Departement</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtCATEGORY_DEPARTEMENT_NAME' runat='server' size='50' maxlength='50'></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvCATEGORY_DEPARTEMENT_NAME" runat="server" ControlToValidate="txtCATEGORY_DEPARTEMENT_NAME"
										Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%' style="HEIGHT: 26px">
									<asp:Label id="capRESPONSIBLE_EMPLOYEE_NAME" runat="server">Employee</asp:Label></TD>
								<TD class='midcolora' width='32%' style="HEIGHT: 26px">
									<asp:textbox id='txtRESPONSIBLE_EMPLOYEE_NAME' runat='server' size='50' maxlength='50'></asp:textbox><br>
								</TD>
								<TD class='midcolora' ColSpan='2' style="HEIGHT: 26px"></TD>
							</tr>
							<tr>
								<td class='midcolora' colspan='2'>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset' CausesValidation="False"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id='btnActivateDeactivate' runat="server" Text='Activate/Deactivate'></cmsb:cmsbutton>
								</td>
								<td class='midcolorr' colspan="2">
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
								</td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
							<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
							<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCATEGEORY_ID" type="hidden" value="0" name="hidCATEGEORY_ID" runat="server">
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
		<script>
				RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidCATEGEORY_ID').value,true);
		</script>
	</BODY>
</HTML>

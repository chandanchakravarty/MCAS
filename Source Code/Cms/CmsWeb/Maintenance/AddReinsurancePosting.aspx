<%@ Register  TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page   validateRequest=false language="c#" Codebehind="AddReinsurancePosting.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AddReinsurancePosting" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Reinsurance Premium / Commission Processing</title>
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

DisableValidators();
document.getElementById('hidGL_ID').value	=	'New';
document.getElementById('cmbCommision_Applicable').focus();
document.getElementById('cmbRein_Premium_Act').options.selectedIndex = -1;
document.getElementById('cmbRein_Payment_Act').options.selectedIndex = -1;
document.getElementById('cmbRein_Commision_Act').options.selectedIndex = -1;
document.getElementById('cmbRein_Commision_Recevable').options.selectedIndex = -1;

ChangeColor();
}
function Commision()
{

//Check for Commission Applicable
if(document.getElementById('cmbCommision_Applicable').value == 1)
{
 document.getElementById('trRein_Commision').style.display = "inline";                   
 document.getElementById('trRein_Commision_Act').style.display = "inline";   
 document.getElementById('trRein_Commision_Recevable').style.display = "inline";                    
 document.getElementById("rfvRein_Commision_Recevable").setAttribute("enabled",true);
 document.getElementById("rfvRein_Commision_Act").setAttribute("enabled",true);
 
}
else
{
 //document.getElementById("pnlCommision_Applicable").style.display = "none";  
 document.getElementById("rfvRein_Commision_Recevable").setAttribute("enabled",false);
 document.getElementById("rfvRein_Commision_Act").setAttribute("enabled",false);
       
 document.getElementById('trRein_Commision').style.display = "none";                   
 document.getElementById('trRein_Commision_Act').style.display = "none";   
 document.getElementById('trRein_Commision_Recevable').style.display = "none";                    
 
 
} 


}
function populateXML()
{
if(document.getElementById('hidOldData').value == '')
{
	AddData();
	ChangeColor();
}
Commision();


return false;
}

		</script>
	</HEAD>
	<BODY leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();'>
		<FORM id='MNT_REINSURANCE_POSTING' method='post' runat='server'>
			<TABLE width='100%' border='0' align='center'>
				<tr>
					<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
						mandatory</TD>
				</tr>
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<tr>
					<TD class='midcolora' width='18%'>
						<asp:Label id="capContract_Name" runat="server"></asp:Label></TD>
					<TD class='midcolora' width='32%' colspan='3'>
						<asp:Label id="capContract_Namet" runat="server"></asp:Label></TD>
					</TD>
				</tr>
				<tr>
					<TD class='midcolora' width='18%'>
						<asp:Label id="capCommision_Applicable" runat="server" DESIGNTIMEDRAGDROP="1550"></asp:Label><SPAN class="mandatory">*</SPAN></TD>
					<TD class="midcolora" width="32%" colspan='3'>
						<asp:DropDownList id="cmbCommision_Applicable" onfocus="SelectComboIndex('cmbCommision_Applicable')"
							runat="server" onChange="javascript:Commision()"></asp:DropDownList><BR>
						<asp:requiredfieldvalidator id="rfvCommision_Applicable" runat="server" Display="Dynamic" ControlToValidate="cmbCommision_Applicable"></asp:requiredfieldvalidator>
					</TD>
				</tr>
				<TR>
					<TD class="headrow" colSpan="4">Premium Section</TD>
				</TR>
				<TR>
					<TD class="midcolora" width="18%">
						<asp:Label id="capRein_Premium_Act" runat="server">Uncollected Premium in Suspense-Direct Bill</asp:Label><SPAN class="mandatory">*</SPAN></TD>
					<TD class="midcolora" width="32%" colspan='3'>
						<asp:DropDownList id="cmbRein_Premium_Act" onfocus="SelectComboIndex('cmbRein_Premium_Act')" runat="server"></asp:DropDownList><br>
						<asp:requiredfieldvalidator id="rfvRein_Premium_Act" runat="server" Display="Dynamic" ControlToValidate="cmbRein_Premium_Act"></asp:requiredfieldvalidator></TD>
				</TR>
				<tr>
					<TD class="midcolora" width="18%">
						<asp:Label id="capRein_Payment_Act" runat="server"></asp:Label><SPAN class="mandatory">*</SPAN>
					</TD>
					<TD class="midcolora" width="32%" colspan='3'>
						<asp:DropDownList id="cmbRein_Payment_Act" onfocus="SelectComboIndex('cmbRein_Payment_Act')" runat="server"></asp:DropDownList><BR>
						<asp:requiredfieldvalidator id="rfvReinsurance_Pay_Act" runat="server" Display="Dynamic" ControlToValidate="cmbRein_Payment_Act"></asp:requiredfieldvalidator></TD>
				</tr>
				
					
						<tr id="trRein_Commision">
							<TD class="headrow" colSpan="4">Commission Section</TD>
						</TR>
						<TR id="trRein_Commision_Act">
							<TD class="midcolora" width="18%">
								<asp:Label id="capRein_Commision_Act" runat="server"></asp:Label><SPAN class="mandatory">*</SPAN></TD>
							<TD class="midcolora" width="32%" colspan='3'>
								<asp:DropDownList id="cmbRein_Commision_Act" onfocus="SelectComboIndex('cmbRein_Commision_Act')" runat="server"></asp:DropDownList><BR>
								<asp:requiredfieldvalidator id="rfvRein_Commision_Act" runat="server" Display="Dynamic" ControlToValidate="cmbRein_Commision_Act"></asp:requiredfieldvalidator></TD>
						</TR>
						<tr id ="trRein_Commision_Recevable">
							<TD class="midcolora" width="18%">
								<asp:Label id="capRein_Commision_Recevable" runat="server"></asp:Label><SPAN class="mandatory">*</SPAN></TD>
							<TD class="midcolora" width="32%" colspan='3'>
								<asp:DropDownList id="cmbRein_Commision_Recevable" onfocus="SelectComboIndex('cmbRein_Commision_Recevable')"
									runat="server"></asp:DropDownList><BR>
								<asp:requiredfieldvalidator id="rfvRein_Commision_Recevable" runat="server" Display="Dynamic" ControlToValidate="cmbRein_Commision_Recevable"></asp:requiredfieldvalidator></TD>
						</tr>
					</asp:Panel>
				<TR>
					<TD class="midcolora" colSpan="1">
						<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></TD>
					<TD class="midcolora" colSpan="2"></TD>
					<TD class="midcolorr" colSpan="1">
						<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></TD>
				</TR>
			</TABLE>
			</TD></TR></TABLE> <INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
			<INPUT id="hidGL_ID" type="hidden" value="0" name="hidGL_ID" runat="server"> 
			<INPUT id="hidContact_id" type="hidden" value="0" name="hidGL_ID" runat="server">
			<INPUT id="hidREIN_COMPANY_ID" type="hidden" value="0" name="hidREIN_COMPANY_ID" runat="server"> 			
		</FORM>
	</BODY>
</HTML>

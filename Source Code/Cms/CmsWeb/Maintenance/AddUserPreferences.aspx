<%@ Page language="c#" Codebehind="AddUserPreferences.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AddUserPreferences" validateRequest=false %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddUserPreferences</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		function AddData()
		{		
			DisableValidators();
			ChangeColor();
			document.getElementById('cmbUSER_COLOR_SCHEME').focus();
			document.getElementById('cmbUSER_COLOR_SCHEME').options.selectedIndex = -1;
			document.getElementById('cmbGRID_SIZE').options.selectedIndex = -1;	
		}
		
		function populateXML()
		{		
			if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
			{
				var tempXML='';
				tempXML=document.getElementById('hidOldData').value;
				if(tempXML!="" && tempXML!=0)
					{							
						populateFormData(tempXML,MNT_USER_LIST);
					}
				else
					{
						AddData();
					}
		return false;
	 }	 
	 }
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();">
		<FORM id="MNT_USER_LIST" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TR>
								<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
							</TR>
							<TR>
								<TD class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></TD>
							</TR>
							<TR>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capUSER_COLOR_SCHEME" runat="server">Color Scheme</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id='cmbUSER_COLOR_SCHEME' OnFocus="SelectComboIndex('cmbUSER_COLOR_SCHEME')" runat='server'>
										<asp:ListItem Value='1'>Blue</asp:ListItem>
										<asp:ListItem Value='4'>Brown</asp:ListItem>
										<asp:ListItem Value='3'>Red</asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvUSER_COLOR_SCHEME" runat="server" ControlToValidate="cmbUSER_COLOR_SCHEME"
										ErrorMessage="USER_COLOR_SCHEME can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capGRID_SIZE" runat="server">Grid Size</asp:Label><span class="mandatory">*</span>
								</TD>
								<TD class='midcolora' width='32%'>
								 <asp:DropDownList id="cmbGRID_SIZE" OnFocus="SelectComboIndex('cmbGRID_SIZE')" runat='server'>
										<asp:ListItem Value='5'>5</asp:ListItem>
										<asp:ListItem Value='10'>10</asp:ListItem>
										<asp:ListItem Value='15'>15</asp:ListItem>
										<asp:ListItem Value='20'>20</asp:ListItem>
										<asp:ListItem Value='25'>25</asp:ListItem>
										<asp:ListItem Value='50'>50</asp:ListItem>
									</asp:DropDownList>
								</TD>
							</TR>
							<tr>
							<td width='18%' class='midcolora'>
									<asp:Label id="capLANG_ID" runat="server">Language</asp:Label><span class="mandatory">*</span>
							</td>
							<td class='midcolora' colspan='3'>
							<asp:DropDownList ID='cmbLANG_ID' runat="server" 
                                   >
							</asp:DropDownList>
							</td>
							</tr>
							<TR>
								<td class='midcolora' colspan='2'>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
								</td>
								<td class='midcolorr' colspan="2">
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
								</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidUSER_ID" type="hidden" value="0" name="hidUSER_ID" runat="server">
		</FORM>
	</BODY>
</HTML>

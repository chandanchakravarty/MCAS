<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="GroupDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.UserDefined.GroupDetails" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GroupDetails</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script>
		<%
		if(gIntRefresh==1)
		{%>			 
			var newid;		
			newid="<%=gIntSavedGroupID%>";
			RefreshWebGrid(1,newid,false);											
		<%}%>
		
		function ResetScreenForm()
		{
			document.GroupDetail.reset();  			 
			DisableValidators();		
		}
		</script>
	</HEAD>
		<body class="bodyBackGround" leftMargin="0" topMargin="0" onload='ApplyColor();'>
				<!-- To add bottom menu -->
		<webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<form id="GroupDetail" method="post" runat="server">
			<table width="100%">
								<asp:Panel id="pnlMsg" Runat="server" Visible="False">
					<TR>
						<TD class="midcolorc" colSpan="2">
							<asp:Label id="lblMessage" Runat="server" CssClass="errmsg"></asp:Label></TD>
							<asp:Label ID="lblMessageDisp" Runat="server" CssClass="errmsg" Visible="False"></asp:Label>
					</TR>
				</asp:Panel>
				
				
				<tr>
					<td class="midcolora" valign="top" width="30%">
						<asp:Label ID="lblGroupName" Runat="server"></asp:Label>
						<span id="lblMandatory" class="mandatory">*</span>
					</td>
					<td class="midcolora" width="70%"><asp:TextBox ID="txtGroupName" cssclass="TEXTAREA" Runat="server" Textmode="multiline" Width="250"
							Height="54px" MaxLength="500"></asp:TextBox>
						<br>
						<asp:requiredfieldvalidator ForeColor="" id="reqtxtGroupName" cssclass="errmsg" runat="server" display="dynamic"
							controltovalidate="txtGroupName"></asp:requiredfieldvalidator>
						<asp:customvalidator id="CVtxtGroupDesc" runat="server" ForeColor="" cssclass="errmsg" Display="Dynamic"
							clientvalidationfunction="txtAreaChk" ControlToValidate="txtGroupName"></asp:customvalidator>
						<script>
						function txtAreaChk(source, arguments)
						{
							var txtAreaVal = arguments.Value;
							if(txtAreaVal.length > 500) {
								arguments.IsValid = false;
								return;   
							}
						}
						</script>
					</td>
				</tr>
				<tr>
					<td align="left" valign="top" class="midcolora">
						<input type=reset class="clsButton" id='btnReset' runat="server" value='Reset'>
						<cmsb:cmsbutton class="clsButton" id='btnActivateDeactivate' runat="server" Text='Activate/Deactivate'></cmsb:cmsbutton>
					</td>
					<td align="right" colspan="1" class="midcolorr">
						<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
					</td>
					<!--<td align="Right" valign="top"><input type="Button" ID="btnCancel" class="clsButton" onmouseover="f_ChangeClass(this, 'HoverButton');" onmouseout="f_ChangeClass(this, 'clsButton');" onclick="javascript:GoBack();" Runat="server" NAME="btnCancel"></asp:Button></td>-->
				</tr>
			</table>
			<asp:TextBox ID="txtDeactivateVal" Runat="server" Visible="False"></asp:TextBox>
			<asp:Label ID="lblTemplate" Runat="server" Visible="False"></asp:Label>
			<asp:Label ID="lblTabID" Runat="server" Visible="False"></asp:Label>
			<asp:Label ID="lblBackID" Runat="server" Visible="False"></asp:Label>
		</form>
	</body>
</HTML>

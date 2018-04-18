<%@ Page language="c#" Codebehind="ScreenDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.User_Defined.ScreenDetails" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
			<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
			<script src="/cms/cmsweb/scripts/common.js"></script>
			<script src="/cms/cmsweb/scripts/form.js"></script>
			<script language="javascript">  
		
		function GoBack()
		{
			//top.location.href="SubmitScreen.aspx" ;
			parent.location.href="SubmitScreen.aspx" ;
		}
		
			</script>
			<script>
		<%
		if(gIntRefresh==1)
		{%>			 
			var newid;		
			newid="<%=gIntSavedScreenID%>";
			RefreshWebGrid(1,newid,false);											
		<%}%>
		
		function ResetScreenForm()
		{
			document.ScreenDetails.reset();  
			 
			DisableValidators();
		
		}
		
			</script>
	</HEAD>
	<body oncontextmenu = "return false;" class="bodyBackGround" leftMargin="0" topMargin="0" onload='ApplyColor();'>
		<form id="ScreenDetails" method="post" runat="server">
			<table width='100%' border='0'>
				<asp:Panel ID="pnlMsg" Runat="server" Visible="False">
					<TBODY>
						<TR>
							<TD class="midcolorc" align="center" colSpan="4">
								<asp:Label id="lblMessage" Visible="False" Runat="server" CssClass="ErrMsg"></asp:Label></TD>
						</TR>
				</asp:Panel>
				<tr>
					<td class="midcolora">
						<asp:Label Runat="server" ID="lblClass"></asp:Label>
						<span id="spnClass" class="mandatory">*</span>
					</td>
					<td class="midcolora" colspan="3">
						<asp:DropDownList ID="ddlClass" Runat="server" EnableViewState="True" AutoPostBack="True"></asp:DropDownList>
						<br>
						<asp:requiredfieldvalidator ForeColor="" id="reqddlClass" cssclass="errmsg" runat="server" display="dynamic"
							controltovalidate="ddlClass"></asp:requiredfieldvalidator>
					</td>
				</tr>
				<tr>
					<td class="midcolora">
						<asp:Label Runat="server" ID="lblSubClass"></asp:Label>
						<span id="spnsubClass" class="mandatory" runat="server">*</span>
					</td>
					<td class="midcolora" colspan="3">
						<asp:DropDownList ID="ddlSubClass" Runat="server" EnableViewState="True" AutoPostBack="False"></asp:DropDownList>
						<br>
						<asp:requiredfieldvalidator ForeColor="" id="rfvSubClass" cssclass="errmsg" runat="server" display="dynamic"
							controltovalidate="ddlSubClass"></asp:requiredfieldvalidator>
					</td>
				</tr>
				<tr>
					<td class="midcolora">
						<asp:Label Runat="server" ID="lblScreenName"></asp:Label>
						<span id="spnscreen" class="mandatory">*</span>
					</td>
					<td class="midcolora" colspan="3">
						<asp:TextBox ID="txtScreenName" cssclass="INPUT" Runat="server" Maxlength="255" Width="250"></asp:TextBox>
						<br>
						<asp:requiredfieldvalidator ForeColor="" id="reqtxtScreenDetails" cssclass="errmsg" runat="server" display="dynamic"
							controltovalidate="txtScreenName"></asp:requiredfieldvalidator>
						<asp:RegularExpressionValidator ForeColor="" id="regScreenDetails" cssclass="errmsg" runat="server" display="dynamic"
							controltovalidate="txtScreenName"></asp:RegularExpressionValidator>
					</td>
				</tr>
				<tr>
					<td class="midcolora">
						<asp:Label Runat="server" ID="lblDispName"></asp:Label>
						<span id="spnDispscreen" class="mandatory">*</span>
					</td>
					<td class="midcolora" colspan="3">
						<asp:TextBox ID="txtdispName" cssclass="INPUT" Runat="server" Maxlength="50" Width="250"></asp:TextBox>
						<br>
						<asp:requiredfieldvalidator ForeColor="" id="ReqDispScreen" cssclass="errmsg" runat="server" display="dynamic"
							controltovalidate="txtdispName"></asp:requiredfieldvalidator>
						<asp:RegularExpressionValidator ForeColor="" id="RegDispScreen" cssclass="errmsg" runat="server" display="dynamic"
							controltovalidate="txtdispName"></asp:RegularExpressionValidator>
					</td>
				</tr>
				<tr>
					<td align="left" valign="top" colspan="2" class="midcolora">
						<input type="button" class="clsButton" id='btnReset' runat="server" CausesValidation="false" onserverclick="btnReset_ServerClick" value='Reset' />
						<cmsb:cmsbutton class="clsButton" id='btnActivateDeactivate' runat="server" Text='Activate/Deactivate'></cmsb:cmsbutton>
					</td>
					<td align="right" colspan="2" class="midcolorr">
						<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
					</td>
					<!--	<input type="button" ID="btnCancel" onmouseover="f_ChangeClass(this, 'HoverButton');" onmouseout="f_ChangeClass(this, 'clsButton');" class="clsButton" onclick="javascript:GoBack();" Runat="server" NAME="btnCancel"></td>-->
				</tr>
				</TBODY>
			</table>
			<asp:TextBox ID="txtDeactivateVal" Runat="server" Visible="False"></asp:TextBox>
			<asp:Label ID="lblHidTempID" Runat="server" Visible="False"></asp:Label>
		</form>
	</body>
</HTML>

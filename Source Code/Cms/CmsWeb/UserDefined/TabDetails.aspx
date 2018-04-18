<%@ Page language="c#" Codebehind="TabDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.User_Defined.TabDetails" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TabDetails</title>
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
			newid="<%=gIntSavedTabID%>";
			RefreshWebGrid(1,newid,false);											
		<%}%>
		function ResetScreenForm()
		{
			document.TabDetail.reset();  
			DisableValidators();
		
		}
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" class="bodyBackGround" leftMargin="0" topMargin="0" onload='ApplyColor();'>
		<form id="TabDetail" method="post" runat="server">
			<table cellspacing="1" cellpadding="1" width="100%">
				<asp:Panel ID="pnlMsg" Runat="server" Visible="False">
					<TBODY>
						<TR>
							<TD class="midcolorc" align="center" colSpan="2">
								<asp:Label id="lblMessage" Visible="True" Runat="server" CssClass="errMsg"></asp:Label></TD>
						</TR>
				</asp:Panel>
				<tr>
					<td class="midcolora" valign="top" width="30%">
						<asp:Label ID="lblTabName" Runat="server"></asp:Label>
						<span id="lblMandatory" class="mandatory">*</span>
					</td>
					<td class="midcolora" valign="middle" width="70%"><asp:TextBox ID="txtTabName" cssclass="TEXTAREA" Runat="server" Textmode="multiline" Width="250"
							Height="44px" Columns="25"></asp:TextBox>
						<br>
						<asp:requiredfieldvalidator ForeColor="" id="reqtxtTabName" cssclass="errmsg" runat="server" display="dynamic"
							controltovalidate="txtTabName"></asp:requiredfieldvalidator>
						<asp:RegularExpressionValidator ForeColor="" id="regTabName" cssclass="errmsg" runat="server" display="dynamic"
							controltovalidate="txtTabName"></asp:RegularExpressionValidator>
						<asp:customvalidator id="CVtxtTabDesc" runat="server" ForeColor="" cssclass="errmsg" Display="Dynamic"
							clientvalidationfunction="txtAreaChk" ControlToValidate="txtTabName"></asp:customvalidator>
						<script>
						function txtAreaChk(source, arguments)
						{
							var txtAreaVal = arguments.Value;
							if(txtAreaVal.length > 50) {
								arguments.IsValid = false;
								return;   
							}
						}
						</script>
					</td>
				</tr>
				<TR>
					<TD class="midcolora" vAlign="top" width="30%"><asp:Label ID="lblRepetableControls" Runat="server"></asp:Label>
						<span id="lblMandatory" class="mandatory">*</span></TD>
					<TD class="midcolora" vAlign="middle" width="70%">
						<asp:TextBox ID="txtRepeatControls" Runat="server" Width="50px"></asp:TextBox>
						<asp:requiredfieldvalidator id="reqRepeatControls" runat="server" cssclass="errmsg" controltovalidate="txtRepeatControls"
							display="dynamic" ForeColor=""></asp:requiredfieldvalidator>
						<asp:RangeValidator id="rngRepeatValidators" runat="server" CssClass="errmsg" Display="Dynamic" MaximumValue="5"
							MinimumValue="1" Type="Integer" ControlToValidate="txtRepeatControls"></asp:RangeValidator>
						<asp:CompareValidator id="cvRepeatableControls" runat="server" ControlToValidate="txtRepeatControls" CssClass="errmsg"
							Display="Dynamic" Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>
					</TD>
				</TR>
				<tr>
					<td valign="top" class="midcolora">
						<input type="reset" class="clsButton" id='btnReset' runat="server" value='Reset'></CMSB:CMSBUTTON>
						<cmsb:cmsbutton class="clsButton" id='btnActivateDeactivate' runat="server" Text='Activate/Deactivate'></cmsb:cmsbutton>
					</td>
					<td align="right" colspan="1" class="midcolorr">
						<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
					</td>
					<!--<td align="Right" valign="top"><input type="Button" ID="btnCancel" class="clsButton" onmouseover="f_ChangeClass(this, 'HoverButton');" onmouseout="f_ChangeClass(this, 'clsButton');" onclick="javascript:GoBack();" Runat="server" NAME="btnCancel" /></td>-->
				</tr>
				</TBODY>
			</table>
			<asp:TextBox ID="txtDeactivateVal" Runat="server" Visible="False"></asp:TextBox>
			<asp:Label ID="lblTemplate" Runat="server" Visible="False"></asp:Label>
			<asp:Label ID="lblScreenID" Runat="server" Visible="False"></asp:Label>
			<asp:Label ID="lblMessageDisp" Runat="server" Visible="False"></asp:Label>
		</form>
		</FORM>
	</body>
</HTML>

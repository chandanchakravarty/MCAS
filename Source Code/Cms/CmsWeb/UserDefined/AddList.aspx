<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddList.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.UserDefined.AddList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>User Defined Screens</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="0" leftmargin="0" rightmargin="0" background="../images/<%=cssFolder%>tile1.gif">
		<form id="AddList" method="post" runat="server">
			<table align="center" cellpadding="1" cellspacing="1" width="100%" border="0">
				<tr>
					<td height="20" colspan="2">&nbsp;
					</td>
				</tr>
				<tr>
											<td colspan="2" class="headereffectCenter">Add User Defined List</td>
				</tr>
				<tr>
					<td class="midcolorc" width="50%" valign="middle">
						<asp:Label Runat="server" ID="lblListName"></asp:Label>
					</td>
					<td class="midcolora" width="50%" valign="middle"><asp:TextBox ID="txtListName" Runat="server" Width="150px" MaxLength="200"></asp:TextBox>
						<br>
						<asp:requiredfieldvalidator ForeColor="" id="reqListName" CssClass="errmsg" runat="server" display="dynamic"
							controltovalidate="txtListName"></asp:requiredfieldvalidator>
					</td>
				</tr>
				<tr>
					<td colspan="2" align="center">
						<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
					</td>
				</tr>
			</table>
			<asp:Label ID="lblQID" Runat="server" Visible="False"></asp:Label>
			<input type="hidden" name="hidListID" id="hidListID" runat="server"> <input type="hidden" name="hidQidID" id="hidQidID" runat="server">
			<input type="hidden" name="hidScreenID" id="hidScreenID" runat="server"> <input type="hidden" name="hidTabID" id="hidTabID" runat="server">
			<input type="hidden" name="hidGroupID" id="hidGroupID" runat="server"> <input type="hidden" name="hidCalledFrom" id="hidCalledFrom" runat="server">
		</form>
	</body>
</HTML>

<%@ Page language="c#" Codebehind="QuestionMapping.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.UserDefined.QuestionMapping" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Question Mapping</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body oncontextmenu = "return false;" MS_POSITIONING="GridLayout">
		<form id="QuestionMapping" method="post" runat="server">
			<table cellSpacing="1" cellPadding="0" width="90%" align="center" border="0">
				<tr>
					<td class="headereffectcenter" colSpan="2">Questions Mapping</td>
				</tr>
				<TR class="midcolora">
					<TD width="30%">Mapped Fields -Questions</TD>
					<TD>
						<asp:ListBox id="lstQuestionMapped" runat="server" Width="240px"></asp:ListBox></TD>
				</TR>
				<tr>
					<td colspan="2" height="18" class="midcolorc">
						<asp:label id="lblMsg" CssClass="errmsg" Runat="server"></asp:label></td>
				</tr>
				<tr class="midcolora">
					<td>Mapping Field Name<span class="mandatory">*</span></td>
					<td>
						<asp:DropDownList id="ddlField" runat="server"></asp:DropDownList>
						<asp:RequiredFieldValidator id="rfvField" runat="server" CssClass="errormsg" ErrorMessage="Select Field Name"
							Display="Dynamic" ControlToValidate="ddlField"></asp:RequiredFieldValidator></td>
				</tr>
				<tr class="midcolora">
					<td>Questions<span class="mandatory">*</span></td>
					<td>
						<asp:DropDownList id="ddlQuestion" runat="server"></asp:DropDownList>
						<asp:RequiredFieldValidator id="rfvQuestion" runat="server" ErrorMessage="Select a Question" Display="Dynamic"
							ControlToValidate="ddlQuestion"></asp:RequiredFieldValidator></td>
				</tr>
				<tr>
					<td colspan="2" height="18" class="midcolora"></td>
				</tr>
				<tr class="midcolora">
					<td class="midcolora">
						<input type="button" runat="server" value="Close" Class="clsButton" onclick="self.close();"
							ID="Button1" NAME="Button1"> &nbsp;&nbsp;<input type="button" runat="server" value="Reset" Class="clsButton" onclick="javascript:location.href=location.href"
							ID="Button2" NAME="Button1"></td>
					<td class="midcolorr">
						<asp:Button id="btnSave" runat="server" Text="Save Mapping" CssClass="clsButton"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

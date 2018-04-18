<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="CopyRecrVehicles.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Homeowner.CopyRecrVehicles" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Copy Recreational Vehicles</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/form.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr>
					<td colspan="2" class="midcolorc">
						<asp:Label id="lblMessage" runat="server" CssClass="errmsg" Visible="False" EnableViewState="False"></asp:Label>
					</td>
				</tr>
				<tr>
					<td colspan="2" class="headereffectCenter">
						<span id="spnHeader">Copy Recreational Vehicles</span>
					</td>
				</tr>
				<tr>
					<td class='midcolora'>
						<asp:label id="Label1" runat="server">How many copies do you want to make of the current record?</asp:label></td>
					<td class='midcolora'>
						<asp:dropdownlist id="cmbNumber" runat="server">
							<asp:ListItem Value="1">1</asp:ListItem>
							<asp:ListItem Value="2">2</asp:ListItem>
							<asp:ListItem Value="3">3</asp:ListItem>
							<asp:ListItem Value="4">4</asp:ListItem>
							<asp:ListItem Value="5">5</asp:ListItem>
							<asp:ListItem Value="6">6</asp:ListItem>
							<asp:ListItem Value="7">7</asp:ListItem>
							<asp:ListItem Value="8">8</asp:ListItem>
							<asp:ListItem Value="9">9</asp:ListItem>
							<asp:ListItem Value="10">10</asp:ListItem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td align="center" colSpan="2">
						<cmsb:cmsbutton class="clsButton" id="btnOK" runat="server" Text="OK"></cmsb:cmsbutton></td>
				</tr>
			</table>
			<asp:Panel ID="pnlGrid" Runat="server" Visible="False">
				<TABLE align="center">
					<TR>
						<TD align="center" colSpan="2">
							<asp:DataGrid id="dgSerialNumbers" runat="server" ShowHeader="False" AutoGenerateColumns="False">
								<ItemStyle CssClass="DataGridRow"></ItemStyle>
								<Columns>
									<asp:TemplateColumn>
										<ItemTemplate>
											<asp:Label id=lblCaption runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Text") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<ItemTemplate>
											<asp:TextBox id="txtSerialNumber" runat="server" MaxLength="30"></asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:DataGrid></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="2">
							<cmsb:cmsbutton class="clsButton" id="btnClose"  runat="server"
								Text="Close" CausesValidation="False"></cmsb:cmsbutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									
							<!--</TD>
						<TD class="midcolorr" colSpan="2">-->
							<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></TD>
					</TR>
				</TABLE>
			</asp:Panel>
		</form>
	</body>
</HTML>

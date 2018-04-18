<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ClaimActivityTop.ascx.cs" Inherits="Cms.CmsWeb.WebControls.ClaimActivityTop" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table cellSpacing="0" cellPadding="0" width="100%" align="center">
	<TR>
		<TD><asp:panel id="pnlApplication" Visible="False" Runat="server" Width="100%">
				<TABLE cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD>
							<webcontrol:GridSpacer id="GridspacerApp1" runat="server"></webcontrol:GridSpacer></TD>
					</TR>
				</TABLE>
				<TABLE class="tableeffectTopHeader" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD class="midcolora" vAlign="top" width="20%">
							<asp:Label id="lblActivityNumber" Runat="server" Visible="True" Font-Bold="True"></asp:Label>:</TD>
						<TD class="midcolora" vAlign="top" width="40%">
							<asp:Label id="lblSActivityNumber" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top" width="20%">
							<asp:Label id="lblActivityTranDesc" Runat="server" Font-Bold="True"></asp:Label>:</TD>
						<TD class="midcolora" vAlign="top" width="20%">
							<asp:Label id="lblSActivityTranDesc" Runat="server"></asp:Label></TD>
					</TR>
					<TR>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblTotalPayment" Runat="server" Font-Bold="True"></asp:Label>:</TD>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblSTotalPayment" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblActivityDate" Runat="server" Font-Bold="True"></asp:Label>:</TD>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblSActivityDate" Runat="server"></asp:Label></TD>
					</TR>
				</TABLE>
			</asp:panel></TD>
	</TR>
</table>

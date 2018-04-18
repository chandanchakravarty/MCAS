<%@ Page language="c#" validateRequest=false Codebehind="AddScheduledItems.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddScheduledItems" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddInlandMarine</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript">
			
		</script>
	</HEAD>
	<body onload="setfirstTime();"
		MS_POSITIONING="GridLayout">

		<div id="bodyHeight" class="pageContent">
			<form id="Form1" method="post" runat="server">
				<table cellSpacing="0" cellPadding="0" WIDTH="99%"">					
					<tr>
						<td><webcontrol:gridspacer id="grdspacer" runat="server"></webcontrol:gridspacer></td>
					</tr>				
					<TR id="trBody" runat="server">
						<TD>
							<table cellSpacing="0" cellPadding="0" width="100%">
								<tr>
									<td class="headereffectCenter"><asp:label id="lblTitle" runat="server">Scheduled Items / Coverages</asp:label></td>
								</tr>
								<tr>
									<td align="center"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False" EnableViewState="False"></asp:label></td>
								</tr>
								<tr>
									<td class="midcolora">
										<asp:datagrid id="dgCoverages" runat="server" AutoGenerateColumns="False"
											Width="100%">
											<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
											<ItemStyle CssClass="midcolora"></ItemStyle>
											<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="COV_DES" HeaderText="Coverage" ItemStyle-Width="50%"></asp:BoundColumn>
												<asp:BoundColumn DataField="DETAILED_DESC" HeaderText="Description" ItemStyle-Width="20%"></asp:BoundColumn>
												<asp:BoundColumn DataField="LIMIT_1" HeaderText="Amount" ItemStyle-Width="15%"></asp:BoundColumn>
												<asp:BoundColumn DataField="DEDUCTIBLE_1" HeaderText="Deductible" ItemStyle-Width="15%"></asp:BoundColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>								
								<tr>
									<td>
										<INPUT id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server">
										<INPUT id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server">
										<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"> 										
									</td>
								</tr>
							</table>
						</TD>
					</TR>
				</table>
			</form>
		</div>
	</body>
</HTML>

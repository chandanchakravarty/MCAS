<%@ Page language="c#" Codebehind="CopyReinsuranceContractsInfo.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.CopyReinsuranceContractsInfo" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CopyReinsuranceContractsInfo</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		
		</script>
	</HEAD>
	<body onload="ChangeColor();ApplyColor();showScroll();">
		<div class="pageContent" id="bodyHeight">
			<form id="Form1" method="post" runat="server">
				<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0">
					<tr>
						<td>
							<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
								<tr>
									<td>&nbsp;</td>
								</tr>
								<TR class="midcolora">
									<TD class="headereffectCenter"><asp:label id="lblHeader" Runat="server"></asp:label></TD>
								</TR>
								<TR>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</TR>
								<tr>
									<td class="midcolora"><asp:datagrid id="dgrReinsurenceContract" Runat="server" Width="100%" HeaderStyle-CssClass="HeadRow"
											AutoGenerateColumns="False" DataKeyField="CONTRACT_ID">
											<Columns>
												<asp:TemplateColumn HeaderText="Select">
													<ItemTemplate>
														<asp:CheckBox ID="chkSelect" Runat="server" Checked="True"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="CONTRACT_ID" Visible="False"></asp:BoundColumn>
												<asp:BoundColumn HeaderText="Contract Type" DataField="CONTRACT_TYPE" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn HeaderText="Contract Number" DataField="CONTRACT_NUMBER" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn HeaderText="Premium Basis" DataField="PREMIUM_BASIS" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn HeaderText="Effective Date" DataField="EFFECTIVE_DATE" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn HeaderText="Expiration Date" DataField="EXPIRATION_DATE" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn HeaderText="Commission" DataField="COMMISSION" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>
								<TR>
									<TD class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSubmit" runat="server" Text="Submit"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnClose" runat="server" Text="Close"></cmsb:cmsbutton></TD>
								</TR>
								<tr>
									<td class="midcolorr"><INPUT id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
										<INPUT id="hidContractID" type="hidden" value="0" name="hidAppVersionID" runat="server">
										<INPUT id="hidAppID" type="hidden" value="0" name="hidAppID" runat="server"> <INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
										<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
										<INPUT id="hidTitle" type="hidden" value="0" name="hidTitle" runat="server"> <INPUT id="hidCalledFor" type="hidden" value="0" name="hidCalledFor" runat="server">
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</form>
		</div>
	</body>
</HTML>

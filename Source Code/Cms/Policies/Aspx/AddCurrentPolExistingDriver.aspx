<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddCurrentPolExistingDriver.aspx.cs" AutoEventWireup="false" Inherits="Policies.Aspx.AddCurrentPolExistingDriver" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>
			<%=strTitle%>
		</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		
		function SetTitle()
		{
			document.title=document.getElementById('hidTitle').value;
		}		
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" onload="SetTitle();ChangeColor();ApplyColor();showScroll();">
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
									<td class="midcolora"><asp:datagrid id="dgrExistingDriver" Runat="server" Width="100%" HeaderStyle-CssClass="HeadRow"
											AutoGenerateColumns="False" DataKeyField="DRIVER_ID">
											<Columns>
												<asp:TemplateColumn HeaderText="Select">
													<ItemTemplate>
														<asp:CheckBox ID="chkSelect" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="CUSTOMERID" Visible="False"></asp:BoundColumn>
												<asp:BoundColumn HeaderText="POL ID" DataField="POLICY_ID" Visible="False"></asp:BoundColumn>
												<asp:BoundColumn HeaderText="Policy Number" DataField="POLICY_NUMBER" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="POLICY_Version_Id" Visible="False"></asp:BoundColumn>
												<asp:BoundColumn HeaderText="Version Number" DataField="POLICY_DISP_VERSION" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVER_CODE" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn DataField="DRIVERNAME" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
												<asp:BoundColumn HeaderText="Customer Name" DataField="CUSTOMERNAME" ItemStyle-CssClass="DataRow"></asp:BoundColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>
								<TR>
									<TD class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSubmit" runat="server" Text="Submit"></cmsb:cmsbutton></TD>
								</TR>
								<tr>
									<td class="midcolorr"><INPUT id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
										<INPUT id="hidPolicyVersionID" type="hidden" value="0" name="hidPolicyVersionID" runat="server">
										<INPUT id="hidPolicyID" type="hidden" value="0" name="hidPolicyID" runat="server">
										<INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
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

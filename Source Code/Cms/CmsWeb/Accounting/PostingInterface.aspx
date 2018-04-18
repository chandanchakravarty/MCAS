<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Page language="c#" Codebehind="PostingInterface.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Accounting.PostingInterface" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddInformationTab</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">

		<script language="javascript">
			function showTab()
			{
				changeTab(0,0);
			}
			function SetFiscalID()
			{
				document.getElementById("hidFiscalID").value = document.getElementById("cmbFISCAL_ID") .options[document.getElementById("cmbFISCAL_ID").selectedIndex].value;
			}
		</script>
		
	</HEAD>
	<body oncontextmenu = "return false;" MS_POSITIONING="GridLayout" onload="top.topframe.main1.mousein =false;findMouseIn();showTab();">		
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<form id="Form1" method="post" runat="server">
				<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<TD class="headereffectCenter"><asp:Label ID="capGENLEDPOST" runat="server"></asp:Label></TD><%--General Ledger Posting--%>
					</tr>
					 <tr>
						<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
					</tr>
					<tr>
						<TD class="pageHeader" colSpan="4"><asp:Label ID="capGENLED" runat="server"></asp:Label></TD><%-- General Ledger :	--%>
					</tr>
					<tr height="10px">
						<td class="midcolora">
							<asp:dropdownlist id="cmbFISCAL_ID" runat="server" AutoPostBack="true" onchange="SetFiscalID();"></asp:dropdownlist>
							<br>
						</td>
					</tr>
					<tr>
						<TD class="pageHeader" colSpan="4">&nbsp;</TD>
					</tr>
					<tr id="formTable" runat="server">
						<td>
							<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
								<tr>
									<td>
										<webcontrol:Tab ID="TabCtl" runat="server" TabURLs=""
											TabTitles="Asset,Liability,Equity,Income,Expense,Default A/C Mapping" TabLength="150"></webcontrol:Tab>
									</td>
								</tr>
								<tr>
									<td>
										<table class="tableeffect" cellpadding="0" cellspacing="0" border="0">
											<tr>
												<td>
													<iframe class="iframsHeightLong" id="tabLayer" runat="server" src="" scrolling="no" frameborder="0"
														width="100%"></iframe>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<asp:Label ID="lblTemplate" Runat="server" Visible="false"></asp:Label>
								<tr>
									<td>
										<webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
									</td>
								</tr>
								<INPUT id="hidFiscalID" type="hidden" name="hidFiscalID" runat="server">
							</table>
						</td>
					</tr>
				</table>
			</form>
		</div>
	</body>
</HTML>

<%@ Page language="c#" Codebehind="EODProcess.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Processes.EODProcess" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Policy Cancellation Process</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
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
	<BODY leftMargin="0" topMargin="0" onload="ApplyColor();ChangeColor();top.topframe.main1.mousein = false;findMouseIn();">
		<FORM id="PROCESS_EOD" method="post" runat="server">
			<DIV id="myTSMain" style="BEHAVIOR: url(/cms/cmsweb/htc/webservice.htc)"></DIV>
			<!-- To add bottom menu start --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
			<!-- To add bottom menu end -->
			<TABLE cellSpacing="0" cellPadding="0" width="90%" align="center" border="0">
				<tr>
					<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
				</tr>
				<tr>
					<td class="headereffectcenter"><asp:Label ID="capHeader" runat="server"></asp:Label></td><%--End of Day Process--%>
				</tr>
				<tr>
				</tr>
				<tr>
					<td id="tdGridHolder"><webcontrol:gridspacer id="Gridspacer2" runat="server"></webcontrol:gridspacer><asp:placeholder id="GridHolder" runat="server"></asp:placeholder></td>
				</tr>
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<tr id="trBody">
					<td>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<td class="midcolora" align="right" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnLaunch" runat="server" Text="Launch"></cmsb:cmsbutton></td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</FORM>
	</BODY>
</HTML>

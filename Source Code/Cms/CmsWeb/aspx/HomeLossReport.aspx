<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="HomeLossReport.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.aspx.HomeLossReport" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MvrForm</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<STYLE>.hide { OVERFLOW: hidden; TOP: 5px }
	.show { OVERFLOW: hidden; TOP: 5px }
	#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">
			function CloseClicked()
			{
			window.close(); 
			}
			function refershParent()
			{
				window.opener.location.reload(true);
			}

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onunload="refershParent()">
		<form id="Form1" method="post" runat="server">
			<table id="tblLOSS" width="100%" align="center">
				<tr>
					<td class="midcolorc" align="left"><asp:label id="lblMessage" runat="server" CssClass="errmsg">Label</asp:label></td>
				</tr>
			</table>
			<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
							<tr>
								<td>
									<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
									<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
								</td>
							</tr>
							<tr>
								<td>
									<webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

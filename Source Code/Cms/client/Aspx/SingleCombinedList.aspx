<%@ Page language="c#" Codebehind="SingleCombinedList.aspx.cs" AutoEventWireup="false" Inherits="Cms.Client.Aspx.SingleCombinedList" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SingleCombinedList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<!--<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">-->
		<script language="javascript">
		
		function DoBack()
		{
			document.location.href = "CustomerNotesIndex.aspx";
			return false;
		}
		</script>
	</HEAD>
	<body onload="setfirstTime();top.topframe.main1.mousein = false;showScroll();findMouseIn();"
		MS_POSITIONING="GridLayout">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<div class="pageContent" id="bodyHeight">
			<form id="SingleCombinedList" method="post" runat="server">
				<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
					<tr>
						<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
					</tr>
					<tr>
						<td class="headereffectcenter" colSpan="3"><asp:Label ID="capHeader" runat="server"></asp:Label></td><%--Single Combined List--%>
					</tr>
					<tr>
						<td class="midcolora"><asp:textbox id="txtDescription" Runat="server" TextMode="MultiLine" ReadOnly=True Width="900px" Height="400px"></asp:textbox></td>
					</tr>
					<tr>
						<td class="midcolora">
							<cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Text='Back'></cmsb:cmsbutton></td>
					</tr>
				</table>
			</form>
		</div>
	</body>
</HTML>

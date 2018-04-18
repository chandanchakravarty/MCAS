<%@ Page language="c#" Codebehind="PreviewUserQuestionTab.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.UserDefined.PreviewUserQuestionTab" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>QuestionTab</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
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
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" onload="showTab();<%if (Request["CallerId"] != null && Request["CallerId"] == "menu"){%>top.topframe.main1.mousein = false;findMouseIn();"<%}else{%>"<%}%>>
		<!-- To add bottom menu -->
		<%if (Request["CallerId"] != null && Request["CallerId"] == "menu"){%>
		<webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<%}%>
		<!-- To add bottom menu ends here -->
		<form id="QuestionTab" method="post" runat="server">
			<table width="90%" cellSpacing="0" cellPadding="0" border="0" align="center">
				<%if (Request["CallerId"] != null && Request["CallerId"] == "menu"){%>
				<tr>
					<td colspan="2" class="pageHeader"><webcontrol:ClientTop id="cltClientTop" runat="server" width="100%"></webcontrol:ClientTop></td>
				</tr>
				<%}%>
				<tr>
					<td colspan="2" height="22px"></td>
				</tr>
				<tr>
					<td><asp:PlaceHolder id="TabHolder" runat="server"></asp:PlaceHolder></td>
				</tr>
				<tr>
					<td>
					<iframe id="tabLayer" class="iframsHeightLong" width="100%" scrolling="no" frameborder="0"></iframe>
					</td>
				</tr>
			</table>
			<asp:Label ID="lblScreenID" Runat="server" Visible="False"></asp:Label>
		</form>
		</FORM>
	</body>
</HTML>

<%@ Page language="c#" Codebehind="SearchLocation.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.aspx.SearchLocation" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Busca de Local</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<STYLE>
		.hide { OVERFLOW: hidden; TOP: 5px }
		.show { OVERFLOW: hidden; TOP: 5px }
		#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">
		
		function CloseWin()
		{
		 window.close();
		}
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" MS_POSITIONING="GridLayout">
		<div class="pageContent" id="bodyHeight">
			<form id="Form1" method="post" runat="server">
				<table width="700px" cellpadding="0" align=center  cellspacing="0" border="0" style="OVERFLOW: visible">
                <tr>
					<td>
						<webcontrol:gridspacer id="Gridspacer2" runat="server"></webcontrol:gridspacer>
						<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
					</td>
							
				</tr>
	
				</table>
				<asp:label id="lblTemplate" Runat="server" Visible="false"></asp:label></TABLE>
			</form>
		</div>
	</body>
</HTML>

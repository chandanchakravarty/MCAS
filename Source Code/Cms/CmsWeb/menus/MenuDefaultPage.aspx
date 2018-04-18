<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Page language="c#" Codebehind="MenuDefaultPage.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.menus.MenuDefaultPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MenuDefaultPage</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script language="javascript">
		function setMenu()
		{
			top.topframe.disableMenus("1","ALL");
			top.topframe.enableMenu("1,0");
			top.topframe.enableMenu("1,1");
			top.topframe.disableMenu("1,1,1");
			top.topframe.disableMenu("1,1,2");
		}
		</script>
	</HEAD>
	
	<body oncontextmenu="return false;" onload="top.topframe.main1.mousein = false;findMouseIn();setMenu();" MS_POSITIONING="GridLayout">
		<webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<DIV ID="Layer1" STYLE="position:absolute; left:178px; top:59px; width:529px; height:29px; z-index:1"> 
			<TABLE WIDTH="100%" BORDER="0" CELLSPACING="0" CELLPADDING="0">
					<tr align="left" valign="middle"> 
						<td width="1%"><img src="/cms/cmsweb/images<%=GetColorScheme()%>/left_border.gif" width="10" height="36"></td>
						<td width="96%" align="center"   background="/cms/cmsweb/images<%=GetColorScheme()%>/mid_border.gif">
							<FONT FACE="Verdana, Arial, Helvetica, sans-serif" SIZE="1"><B><FONT SIZE="2">
							<B><FONT SIZE="2"><asp:Label ID="capGeneralMessage" Runat="server"></asp:Label></FONT></B></FONT>
						</td>
						<td width="3%" ><img src="/cms/cmsweb/images<%=GetColorScheme()%>/right_border.gif" width="10" height="36"></td>
					</tr>
				<%--<TR> 
					<TD WIDTH="19" VALIGN="TOP"><IMG SRC="/cms/cmsweb/images/blank.gif" WIDTH="1" HEIGHT="1"></TD>
					<TD WIDTH="11" VALIGN="TOP"><IMG SRC="/cms/cmsweb/images/curve_left_top.gif" WIDTH="11" HEIGHT="11"></TD>
					<TD VALIGN="TOP" BACKGROUND="/cms/cmsweb/images/topborder.gif"><IMG SRC="/cms/cmsweb/images/blank.gif" WIDTH="1" HEIGHT="1"></TD>
					<TD ALIGN="RIGHT" WIDTH="11" VALIGN="TOP"><IMG SRC="/cms/cmsweb/images/curve_right_top.gif" WIDTH="11" HEIGHT="11"></TD>
				</TR>
				<TR> 
					<TD WIDTH="19"><IMG SRC="/cms/cmsweb/images/blank.gif" WIDTH="19" HEIGHT="8"></TD>
					<TD WIDTH="11" BACKGROUND="/cms/cmsweb/images/left_border.gif" VALIGN="TOP"><IMG SRC="/cms/cmsweb/images/blank.gif" WIDTH="1" HEIGHT="1"></TD>
					<TD BGCOLOR="#E4F7FF" VALIGN="TOP"><FONT FACE="Verdana, Arial, Helvetica, sans-serif" SIZE="1"><B><FONT SIZE="2">
					<B><FONT SIZE="2"><asp:Label ID="capGeneralMessage" Runat="server"></asp:Label></FONT></B></FONT>
					</TD>
					<TD ALIGN="RIGHT" WIDTH="11" BACKGROUND="/cms/cmsweb/images/right_border.gif" VALIGN="TOP"><IMG SRC="/cms/cmsweb/images/blank.gif" WIDTH="1" HEIGHT="1"></TD>
				</TR>
				<TR> 
					<TD WIDTH="19" VALIGN="TOP"><IMG SRC="/cms/cmsweb/images/blank.gif" WIDTH="1" HEIGHT="1"></TD>
					<TD WIDTH="11" VALIGN="TOP"><IMG SRC="/cms/cmsweb/images/curve_left_bottom.gif" WIDTH="11" HEIGHT="11"></TD>
					<TD VALIGN="TOP" BACKGROUND="/cms/cmsweb/images/bottom_border.gif"><IMG SRC="/cms/cmsweb/images/blank.gif" WIDTH="1" HEIGHT="1"></TD>
					<TD ALIGN="RIGHT" WIDTH="11" VALIGN="TOP"><IMG SRC="/cms/cmsweb/images/curve_right_bottom.gif" WIDTH="11" HEIGHT="11"></TD>
				</TR>--%>
			</TABLE>
		</DIV>
	</body>
</HTML>

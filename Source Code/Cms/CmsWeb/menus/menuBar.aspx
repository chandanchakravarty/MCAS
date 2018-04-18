<%@ Page language="c#" Codebehind="menuBar.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.menus.menuBar" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Sample Frame Menu</title>
		<meta http-equiv="Content-Type" content="text/html;charset=utf-8" >
		<link href="/cms/cmsweb/css/menu<%=GetColorScheme()%>.css" rel="stylesheet" type="text/css" >
		<link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" rel="stylesheet" type="text/css" >
		<script type="text/javascript" src="/cms/cmsweb/scripts/menubar.js"></script>
</HEAD>
<BODY leftMargin=0 topMargin=0 rightMargin=0>
<DIV id="main1" valign="top" >
	<DIV class=menuBarBottom background-image="/cms/cmsweb/images<%=GetColorScheme()%>/bg_links.jpg"></DIV>
</DIV>
<!--
		<br>
		<br>
		<input type="button" onclick="top.topframe.callService1();" value="call service" ID="Button12" NAME="Button12">
		<br>
		<br>
		<input type="button" onclick="top.topframe.disableMenu('button','0')" value="disable - Diary" ID="Button1" NAME="Button1">
		--><!--
		<input type="button" onclick="top.topframe.disableMenu('topMenu','0,0')" value="disable - 00" ID="Button2" NAME="Button2" DESIGNTIMESP=8490>
		<input type="button" onclick="top.topframe.disableMenu('menu','0,0,1')" value="disable - 001" ID="Button3" NAME="Button3" designtimesp=8491>
		<input type="button" onclick="top.topframe.disableMenu('subMenu','0,0,1,0')" value="disable - 0010" ID="Button4" NAME="Button4" designtimesp=8492>
		<input type="button" onclick="top.topframe.disableMenu('subSubMenu','0,0,1,0,0')" value="disable - 00100" ID="Button5" NAME="Button5" designtimesp=8493>
		<br designtimesp=8494>
		<input type="button" onclick="top.topframe.disableMenu('button','1')" value="disable - 1" ID="Button6" NAME="Button6" designtimesp=8495>
		<input type="button" onclick="top.topframe.disableMenu('topMenu','1,0')" value="disable - 10" ID="Button7" NAME="Button7" designtimesp=8496>
		<input type="button" onclick="top.topframe.disableMenu('menu','1,0,1')" value="disable - 101" ID="Button8" NAME="Button8" designtimesp=8497>
		<input type="button" onclick="top.topframe.disableMenu('subMenu','1,0,1,0')" value="disable - 1010" ID="Button9" NAME="Button9" designtimesp=8498>
		<input type="button" onclick="top.topframe.disableMenu('subSubMenu','1,0,1,0,0')" value="disable - 10100" ID="Button10" NAME="Button10" designtimesp=8499>
		-->
		<!--
		<br>
		<input type="button" onclick="top.topframe.showTopButton()" value="Disable Now" ID="Button11" NAME="Button11">
		<br>
		<input type="button" onclick="top.topframe.enableMenu('button','0')" value="enable - 0" ID="Button13" NAME="Button13">
		<textarea id="myTextarea"></textarea>
		-->
	</BODY>
</HTML>

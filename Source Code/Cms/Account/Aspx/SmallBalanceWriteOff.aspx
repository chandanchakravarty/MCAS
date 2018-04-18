<%@ Page language="c#" Codebehind="SmallBalanceWriteOff.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.SMALL_BAL_WRITEOFF" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Small balance Write Off</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		function HideShowTransactionInProgress()
		{
			DisableButton(document.getElementById('btnWriteOff'));
		}
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" class="bodyBackGround" topMargin="0" onload="ApplyColor();ChangeColor(); top.topframe.main1.mousein = false;findMouseIn();showScroll();"
		MS_POSITIONING="GridLayout">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<form id="SMALL_BAL_WRITEOFF" method="post" runat="server">
			<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
				<tr>
					<td class="headereffectcenter" colSpan="2">Small Balance Write off
					</td>
				</tr>
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<tr>
					<TD class="midcolora"><asp:label id="capAPP_EFFECTIVE_DATE" runat="server">Date</asp:label>
						<span class="mandatory">*</span></TD>
					<TD class="midcolora" vAlign="middle"><asp:textbox id="txtDATE" runat="server" Display="Dynamic" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkCalandarDate" runat="server" Display="Dynamic" CssClass="HotSpot">
							<asp:image id="imgCalenderExp" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
								valign="middle" Display="Dynamic"></asp:image>
						</asp:hyperlink><BR>
						<asp:requiredfieldvalidator id="rfvDATE" runat="server" ErrorMessage="Date can't be blank." Display="Dynamic"
							ControlToValidate="txtDATE"></asp:requiredfieldvalidator>
						<asp:regularexpressionvalidator id="revtxtDATE" runat="server" ControlToValidate="txtDATE" ErrorMessage="RegularExpressionValidator"
							Display="Dynamic"></asp:regularexpressionvalidator>
					</TD>
				<TR>
					<td class="midcolora"></td>
					<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnWriteOff" runat="server" Text="Write Off"></cmsb:cmsbutton></td>
				</TR>
			</table>
		</form>
	</body>
</HTML>

<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PostCustomerTransaction.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.PostCustomerTransaction" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>PostCustomerTransaction</title>
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
		//This function open the customer lookup window
		function OpenCustomerLookup()
		{
			OpenLookup('<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>','CUSTOMER_ID','Name','hidCustomer_ID','txtCustomerDetail','CustLookupForm','Customer','');
		}
		</script>
</HEAD>
	<body class="bodyBackGround" topMargin="0" onload="ApplyColor();ChangeColor(); top.topframe.main1.mousein = false;findMouseIn();showScroll();"
		MS_POSITIONING="GridLayout">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<form id="Form1" method="post" runat="server">
			<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
				<tr>
					<td class="headereffectcenter" colSpan="2">Post Customer Transaction
					</td>
				</tr>
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<tr>
					<td class="midcolora">Customer <span class="mandatory">*</span></td>
					<td class='midcolora' width="75%">
						<asp:TextBox id='txtCustomerDetail' ReadOnly="True" runat='server' size="40"></asp:TextBox>
						<IMG id="imgCustomer" style="CURSOR: hand" onclick="OpenCustomerLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
							runat="server">
						<BR>
						<asp:requiredfieldvalidator id="rfvCustomerDetail" runat="server" ControlToValidate="txtCustomerDetail" ErrorMessage="Please select Customer."
							Display="Dynamic"></asp:requiredfieldvalidator>
					</td>
				</tr>
				<tr>
					<TD class="midcolora"><asp:label id="capAPP_EFFECTIVE_DATE" runat="server">Date</asp:label> <span class="mandatory">*</span></TD>
					<TD class="midcolora" vAlign="middle"><asp:textbox id="txtDATE" runat="server" Display="Dynamic" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkCalandarDate" runat="server" Display="Dynamic" CssClass="HotSpot">
							<asp:image id="imgCalenderExp" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif" valign="middle" Display="Dynamic"></asp:image>
						</asp:hyperlink><BR>
						<asp:requiredfieldvalidator id="rfvDATE" runat="server" ErrorMessage="Date can't be blank." Display="Dynamic"
							ControlToValidate="txtDATE"></asp:requiredfieldvalidator>
							<asp:regularexpressionvalidator id="revtxtDATE" runat="server" ControlToValidate="txtDATE" ErrorMessage="RegularExpressionValidator"
										Display="Dynamic"></asp:regularexpressionvalidator>						
							</TD>
				<TR>
					<td class="midcolora"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Visible="false" Text="Reset"></cmsb:cmsbutton></td>
					<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnExecute" runat="server" Text="Execute"></cmsb:cmsbutton></td>
				</TR>
			</table>
			<input id="hidCustomer_ID" type="hidden" name="hidCustomer_ID" runat="server">
		</form>
	</body>
</HTML>

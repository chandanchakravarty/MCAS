<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExceptionItemsDetails.aspx.cs" Inherits="Cms.Account.Aspx.ExceptionItemsDetails" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title><%=capTitle%></title>
    <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">

		   
		function showPrint()
		{
			spn_Button.style.display = "none"
			bgcolor = buttons.style.background
			buttons.style.background = "white"
			window.print()
			spn_Button.style.display = "inline"
		}
		</script>
</head>
<body oncontextmenu = "return false;" class="bodyBackGround" topmargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing='1' cellPadding='0' border='0' class='tableWidth' align="center">
				<tr>
					<td>
						<webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer>
					</td>
				</tr>
				<tr>
					<td class="headereffectCenter" colspan="2">
                        <asp:Label ID="lblDepositLineException" runat="server" ></asp:Label></td>
				</tr>
				<tr>
					<td class="midcolorr" width="50%">
                        <asp:Label ID="lblTodayDate" runat="server" ></asp:Label> &nbsp;</td>
					<td class="midcolora" width="50%"><%=DateTime.Now.ToShortDateString()%></td>
				</tr>
				<tr>
					<td class="midcolorr" width="50%"><asp:label ID="capTime" runat="server"></asp:label></td><%--Time : &nbsp;--%>
					<td class="midcolora" width="50%"><%=DateTime.Now.ToString("hh:mm:ss tt")%></td>
				</tr>
				<tr>
					<td class="midcolorc" colspan="2"><b><asp:Label ID="lblDepositLineItmes" runat="server" ></asp:Label>
                        </b></td>
				</tr>
				<tr>
					<td class="midcolora" colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td class="errmsg"><asp:Label ID="lblMessage" Runat="server"></asp:Label></td>
				</tr>
				<tr>
				<td id="tdRTLImportHistoryDetails" runat="server" colspan="2"></td>
                 <td><input id="hidPrint" type="hidden" runat="server" />
                 <input id="hid_close" type="hidden" runat="server" /></td>
				 
				</tr>
			</table>
		</form>
	</body>
</html>

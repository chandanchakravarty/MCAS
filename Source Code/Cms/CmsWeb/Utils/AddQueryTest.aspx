<%@ Page language="c#" Codebehind="AddQueryTest.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Utils.AddQueryTest" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddQueryTest</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		<!-- Charles added colspan="2" for all rows with single column on 9-Jun-09 -->
			<table class="tableWidthHeader">
				<tr>
					<td class="midcolora" colspan="2">
						<asp:Label id="lblMessage" runat="server" CssClass="errmsg"></asp:Label></td>
				</tr>
				<tr>
					<td class="midcolora" colspan="2">
						<asp:Label id="lbltop" runat="server">Please enter a valid SQL query to execute:</asp:Label></td>
				</tr>
				<tr>
					<td class="midcolora" colspan="2">
						<asp:TextBox id="txtQuery" runat="server" TextMode="MultiLine" Rows="10" Columns="150"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="midcolora" colspan="2">
						<asp:Label id="lblPerm" runat="server">Please provide a password to execute SQL commands:</asp:Label></td>
				</tr>
				<tr>
					<td class="midcolora" colspan="2">
						<asp:TextBox id="txtPasswd" TextMode="Password" runat="server" Rows="10" Columns="40"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="midcolora" align="center" colspan="2">
						<asp:Button id="btnPerm" runat="server" Text="Sign In"></asp:Button>
					</td>
				</tr>
				<tr>
					<td class="midcolora" align="center">
						<asp:Button id="Button2" runat="server" Text="Execute SQL"></asp:Button>						
					</td>
					<!-- Added by Charles on 8-Jun-2009 -->
					<td class="midcolorr" align="right">
						<asp:Button id="btnViewAgencyDetails" runat="server" Text="View Agency Details"></asp:Button>
					</td>
				</tr>
			</table>
			<table class="tableWidthHeader">
				<tr>
					<td class="midcolora">
						<asp:DataGrid id="dgResult" runat="server" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
							BackColor="White" CellPadding="3" GridLines="Vertical">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#DCDCDC"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
							<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
							<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

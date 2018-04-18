<%@ Page language="c#" Codebehind="DataGridExport.aspx.cs" AutoEventWireup="false" Inherits="Glasses.Test.DataGridExport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DataGridExport</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<P>&nbsp;</P>
			<P>
				<asp:Button id="Button1" runat="server" Text="Export to Excel"></asp:Button>
				<asp:Button id="Button2" runat="server" Text="Export to Word"></asp:Button>
				<asp:Button id="Button3" runat="server" Text="Export to Text File"></asp:Button></P>
			<asp:DataGrid id="myDataGrid" runat="server"></asp:DataGrid>
		</form>
	</body>
</HTML>

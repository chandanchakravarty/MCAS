<%@ Page language="c#" Codebehind="PrintChartOfAccountRanges.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.PrintChartOfAccountRanges" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS-Chart Of Account Ranges</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script>
		function  ShowPrintDialog()
		{
			window.print();
		}
		
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE width='100%' border='0' align='center' cellpadding="0" cellspacing="1">				
				<tr>
					<TD class="headereffectCenter" colSpan="5"><asp:Label ID="capChart_of_Account_Ranges" runat ="server"></asp:Label></TD><%--Chart of Account Ranges--%>
				</tr>
				<tr>
					<TD class="midcolorc" colSpan="5"><asp:Label ID="capDate" runat="server"></asp:Label>   <%=DateTime.Now.ToString().Substring(0,DateTime.Now.ToString().IndexOf(' '))%></TD><%--Date:--%>
				</tr>
				<tr>
					<TD class="midcolorc" colSpan="5"><asp:Label ID="capTime" runat="server"> </asp:Label>  <%=DateTime.Now.TimeOfDay.ToString().Substring(0,DateTime.Now.TimeOfDay.ToString().IndexOf('.'))%></TD><%--Time:--%>
				</tr>
				<tr>
					<td>
						<asp:DataGrid id="DataGrid1" Width="100%" AlternatingItemStyle-CssClass="AlternateDataRow2" ItemStyle-CssClass="DataRow2"
							HeaderStyle-CssClass="headereffectCenter" runat="server"></asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><INPUT class="clsButton" id="btnPrint" type="button" value="Print" onclick="ShowPrintDialog();">
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

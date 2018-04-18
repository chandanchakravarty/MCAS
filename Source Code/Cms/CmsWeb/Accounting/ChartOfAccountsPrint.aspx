<%@ Page language="c#" Codebehind="ChartOfAccountsPrint.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Accounting.ChartOfAccountsPrint" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>EBIX Advantage-Chart Of Accounts</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script>
		function  ShowPrintDialog()
		{
			/*document.getElementById("print").style.display	 = "None";
			document.getElementById("close").style.display	 = "None";
			document.getElementById("print2").style.display	 = "None";
			document.getElementById("close2").style.display  = "None";*/
			//document.Form1.
			//iReport.print();
			window.print();
			/*document.getElementById("print").style.display	= "inline";
			document.getElementById("close").style.display	= "inline";
			document.getElementById("print2").style.display = "inline";
			document.getElementById("close2").style.display = "inline";*/
			
			//document.getElementById('print'
		}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE width=100% border="0" cellpadding="0" cellspacing="1">
				<tr>
					<td class="midcolora">
						<INPUT id="print" class="clsButton" type="button" value="Print" runat="server" onclick="ShowPrintDialog();">
						<INPUT id="close" class="clsButton" type="button" value="Close" runat="server" onclick="window.close();">
					</td>
				</tr>
				<tr>
					<td class=""><div id="iReport" name="iReport">
						<asp:Literal id="litReport" runat="server" EnableViewState=False></asp:Literal></div>
					</td>
				</tr>
				<tr>
					<td class="midcolora">
						<INPUT id="print2" class="clsButton" type="button" value="Print" runat="server" onclick="ShowPrintDialog();">
						<INPUT id="close2" class="clsButton" type="button" value="Close" runat="server" onclick="window.close();">
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

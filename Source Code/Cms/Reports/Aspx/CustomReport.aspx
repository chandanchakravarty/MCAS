<%@ Page language="c#" EnableViewStateMac="False" Codebehind="CustomReport.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.CustomReport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CustomReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
			
			function Init()
			{
				/*var oldVal = document.getElementById('hidOldData').value;
				document.getElementById('hidOldData').value = '0';
				//alert(window.opener.urlData);
				document.getElementById('hidMONTH').value= window.opener.Month;
				document.getElementById('hidPageName').value= window.opener.PageName;
				document.getElementById('hidGeneralLedgerId').value= window.opener.GeneralLedgerId;
				document.getElementById('hidFinancialYear').value= window.opener.FinancialYear;
				if(oldVal == '' && document.getElementById('hidPageName').value=='ProfitLoss')
					__doPostBack("hidMONTH","");	
				else if(oldVal == '' && document.getElementById('hidPageName').value=='TrialBalance')
					__doPostBack("hidMONTH","");
				else if(oldVal == '' && document.getElementById('hidPageName').value=='Balancesheet')
					__doPostBack("hidMONTH","");
				//else 
					//__doPostBack("hidMONTH","");	*/		
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="Init();">
		<form id="Form1" method="post" runat="server">
		<%--
			<cc1:reportviewer id="ReportViewer1" style="Z-INDEX: 101; LEFT: 208px; POSITION: absolute; TOP: 248px"
				runat="server" Width="100%" Height="100%"></cc1:reportviewer>
					<INPUT id="hidOldData" type="hidden" value="" name="hidOldData" runat="server">
					<INPUT id="hidMONTH" type="hidden" value="" name="hidMONTH" runat="server">
					<INPUT id="hidPageName" type="hidden" value="" name="hidPageName" runat="server">
					<INPUT id="hidGeneralLedgerId" type="hidden" value="" name="hidGeneralLedgerId" runat="server">
					<INPUT id="hidFinancialYear" type="hidden" value="" name="hidFinancialYear" runat="server">
	--%>				
				</form>
	</body>
</HTML>

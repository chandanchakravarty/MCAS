<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Page language="c#" Codebehind="BankReconciliationReport.aspx.cs" AutoEventWireup="false" Inherits="Account.BankReconciliationReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>Bank Reconciliation Report</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<!--<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">-->
        <script language="javascript">
		function showPrint()      
		{
			spn_Button.style.display = "none"
			bgcolor = buttons.style.background
			buttons.style.background = "white"
			window.print()
		}			
	</script>						
  </head>
  <body MS_POSITIONING="GridLayout">  
  <table class="tableWidth" cellSpacing="0" cellPadding="1" border="0" align="center">	
	<form id="indexForm" method="post" runat="server">
	<tr>
	<td HorizontalAlign="Center" colspan="3" class="headereffectcenter">Bank Reconciliation Report</td>
	</tr>
		
	<tr>
	<td align="center">
	<asp:label  id="lblerr" runat="server" Visible="True" cssclass="errmsg"></asp:label>
	</td>
	</tr>
	<tr>
	<td class="midcolora" width="18%">Filter	
		 <class="midcolora" ><asp:dropdownlist id="cmb_MatchRecordStatus" runat="server" AutoPostBack ="True">
		<asp:ListItem Value="-1">All</asp:ListItem>
		<asp:ListItem Value="1">Matched</asp:ListItem>
		<asp:ListItem Value="0">Unmatched</asp:ListItem>
		
		
</asp:dropdownlist></td>
	</tr>		
	<tr>		
		<td><br>
	<asp:datagrid id="dgReconcheck" align="center" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
	PagerStyle-Mode="NumericPages" PagerStyle-HorizontalAlign="Center" PagerStyle-CssClass="datarow"
	HorizontalAlign="Center" HeaderStyle-CssClass="headereffectCenter" ItemStyle-CssClass="datarow"
	 OnPageIndexChanged="dgReconcheck_Paging">
					<ALTERNATINGITEMSTYLE CssClass="alternatedatarow"></ALTERNATINGITEMSTYLE>
						<HEADERSTYLE CssClass="headereffectCenter"></HEADERSTYLE>
						<COLUMNS>
				<%-- Check Amount added  and Account_number Replace For Itrack #Issue 5517--%>
				<ASP:BOUNDCOLUMN HeaderText="Check Amount" DataField="CHECK_AMOUNT"></ASP:BOUNDCOLUMN>
				<ASP:BOUNDCOLUMN HeaderText="Serial Number" DataField="SERIAL_NUMBER"></ASP:BOUNDCOLUMN>
				<asp:TemplateColumn HeaderText="Date Cleared" HeaderStyle-Width="10%">
						<ItemTemplate>
				<asp:Label ID="lbldate" Runat="server" text ='<%#String.Format("{0:d}",DataBinder.Eval(Container.DataItem, "CHECK_DATE"))%>'>/</asp:Label>
				</ItemTemplate>
				</asp:TemplateColumn>
				<ASP:BOUNDCOLUMN HeaderText="Additional Data" DataField="ADDITIONAL_DATA"></ASP:BOUNDCOLUMN>
				<ASP:BOUNDCOLUMN HeaderText="Sequence Number" DataField="SEQUENCE_NUMBER"></ASP:BOUNDCOLUMN>
				<ASP:BOUNDCOLUMN HeaderText="Error Desc" DataField="ERROR_DESC"></ASP:BOUNDCOLUMN>
				<ASP:BOUNDCOLUMN HeaderText="Match Record Status" DataField="MATCHED_RECORD_STATUS"></ASP:BOUNDCOLUMN>													
					</COLUMNS>
					</asp:datagrid>					
					</td>								
				</tr>	
		</table>
		<input id="hidFILE_ID" type="hidden" name="hidFILE_ID" runat="server">		
		</form>	
  </body>
</html>

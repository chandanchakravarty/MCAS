<%@ Page language="c#" Codebehind="BankReconcilationHistory.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.BankReconcilationHistory" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<!--<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">-->
		<STYLE>
			.hide { OVERFLOW: hidden; TOP: 5px }
			.show { OVERFLOW: hidden; TOP: 5px }
			#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">		
		function showPrint()      
		{
			spn_Button.style.display = "none"
			bgcolor = buttons.style.background
			buttons.style.background = "white"
			window.print()
		}	
		
		function ShowBankReconDetails(path)
		{			
			window.open(path,'BRICS','resizable=yes,scrollbars=yes,left=150,top=50,width=800,height=500');
			return false;
		}	
		</script>
	</HEAD>
	<body oncontextmenu="return false;" leftmargin="0" rightmargin="0" onload="top.topframe.main1.mousein = false;findMouseIn();"
		MS_POSITIONING="GridLayout">
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->		
			<table class="tableWidth" cellSpacing="1" cellPadding="0" border="0" align="center">										
			<form id="indexForm" method="post" runat="server">							
			         <tr>
			<td HorizontalAlign="Center" colspan="3" class="headereffectcenter">Bank 
					Reconciliation</td>
			</tr><tr></tr><tr></tr><tr></tr>							
			<br><tr>
		<TD class="midcolora" width="18%">Select Bank Account</TD>
		<TD class="midcolora" width="32%" colSpan="2">
				<asp:dropdownlist id="cmbBank_Account" AutoPostBack="True" runat="server">
				</asp:dropdownlist>									
			    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:dropdownlist id = "cmb_Year" width = "20%" runat = "server">												
							</asp:dropdownlist>	
							</tr>
				<tr>
				<td align="center"><br>
		      <asp:datagrid id="dgRecCheck" align="center" runat="server" Width="277%" AutoGenerateColumns="False"								
						HorizontalAlign="Center" HeaderStyle-CssClass="headereffectCenter" ItemStyle-CssClass="datarow"
						AlternatingItemStyle-CssClass="alternatedatarow">				    	
			    	<COLUMNS>
				     	<ASP:BOUNDCOLUMN HeaderText="File Name" DataField="FILE_NAME"></ASP:BOUNDCOLUMN>											
					    <ASP:BOUNDCOLUMN HeaderText="Created Datetime" DataField="CREATED_DATETIME"></ASP:BOUNDCOLUMN>
						<ASP:BOUNDCOLUMN HeaderText="Created By" DataField="CREATED_BY"></ASP:BOUNDCOLUMN>
						<ASP:BOUNDCOLUMN HeaderText="File Id"  DataField = FILE_ID Visible="False"></ASP:BOUNDCOLUMN>																								
						<ASP:TEMPLATECOLUMN HeaderText="View" >
							<ITEMTEMPLATE>
								<ASP:HYPERLINK id="hlnkView" target ="_blank" runat="server">Details</ASP:HYPERLINK>
							</ITEMTEMPLATE>
						</ASP:TEMPLATECOLUMN>
					</COLUMNS>
				</asp:datagrid>
						</td>									
						</tr>			
			</FORM>	
			</table>		
	</body>
</HTML>

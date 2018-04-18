<%@ Page language="c#" Codebehind="LookupForm.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.aspx.LookUpForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Look up Form</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		
		<script src="/cms/cmsweb/scripts/form.js"></script>
		
		<script language="javascript">
			function OnDoubleClick(TextFieldID,ValueFieldID,TextFieldValue,ValueFieldValue)
			{
				if ( window.opener.document.getElementById(TextFieldID) != null )
				{
					window.opener.document.getElementById(TextFieldID).value = TextFieldValue;
				}
				
				if ( window.opener.document.getElementById(ValueFieldID) != null )
				{	
					window.opener.document.getElementById(ValueFieldID).value = ValueFieldValue;
				}
				window.close();
				
			}
			
			function changecursor(obj)
			{
    			obj.style.cursor="hand";
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table>
				<tr>
					<td class="midcolora"><span>Search Option</span><asp:dropdownlist id="ddlSearchColumn" runat="server"></asp:dropdownlist>
						<span>Search Criteria</span><asp:textbox id="txtSearch" runat="server"></asp:textbox></td>
					<td></td>
					<td><asp:button CssClass="clsButton" id="btnSearch" runat="server" Text="Search"></asp:button></td>
					<td><asp:button CssClass="clsButton" id="btnReset" runat="server" Text="Reset"></asp:button></td>
				</tr>
			</table>
			<table>
				<tr>
					<td align="center">
						<div id="div-datagrid" >
						<asp:datagrid id="dgLookup" runat="server" AllowPaging="False" AutoGenerateColumns="False" Width="100%"
							AllowSorting="True">
							<AlternatingItemStyle CssClass="AlternateDataRow"></AlternatingItemStyle>
							<ItemStyle CssClass="DataRow"></ItemStyle>
							<HeaderStyle CssClass="DIV#div-datagrid">
							
							</HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False"></asp:BoundColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
						</div>
					</td>
				</tr>
				<tr>
					<td align="center">
						<INPUT type="button" class="clsButton" value="Close" onclick="javascript:window.close();">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

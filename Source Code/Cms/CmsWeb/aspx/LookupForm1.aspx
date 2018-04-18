<%@ Page language="c#" Codebehind="LookupForm1.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.aspx.Lookup.LookupForm1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>LookupForm1</title>
		<%--<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">--%>
		<link rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css" />
		<script src="/cms/cmsweb/scripts/form.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">			
			function SetTitle()
			{
				if ( window.opener.lookupTitle == null )
				{
					document.title = 'Lookup';
					document.getElementById('spnHeader').innerHTML = 'Lookup';
					return;
				}
				
				if ( window.opener.lookupTitle == '' )
				{
					document.title = 'Lookup';
					document.getElementById('spnHeader').innerHTML = 'Lookup';
					return;
				}
				
				document.title = window.opener.lookupTitle;
				document.getElementById("spnHeader").innerHTML = window.opener.lookupTitle;
			}
			
			function OnDoubleClick(TextFieldID,ValueFieldID,TextFieldValue,ValueFieldValue, JSFunction)
			{			   
				if ( window.opener.document.getElementById(TextFieldID) != null )
				{
					var type = window.opener.document.getElementById(TextFieldID).type;
					var prefix = TextFieldID.substring(0,3);
					//alert(prefix);
					if ( type == 'text' || type == 'hidden')
					{
						window.opener.document.getElementById(TextFieldID).value = TextFieldValue;
					}
					
					if ( type == 'span')
					{
						window.opener.document.getElementById(TextFieldID).innerHTML = TextFieldValue;
					}					
					window.opener.DisableValidatorsById(TextFieldID);					
				}
				
				if ( window.opener.document.getElementById(ValueFieldID) != null )
				{	
					var prefix = ValueFieldID.substring(0,3);
					
					var type = window.opener.document.getElementById(ValueFieldID).type;
					
					//alert(type);
					if ( type == 'text' || type == 'hidden')
					{	
						window.opener.document.getElementById(ValueFieldID).value = ValueFieldValue;
					}
					
					if ( type == 'span')
					{	
						window.opener.document.getElementById(ValueFieldID).innerHTML = ValueFieldValue;
					}					
				}
				
				if ( JSFunction != '' )
				{					
					eval('window.opener.' +  JSFunction);
				}
				
				window.close();				
			}
			
			function changecursor(obj)
			{
    			obj.style.cursor="hand";
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="javascript:SetTitle();">
		<form id="Form1" method="post" runat="server">
			<table width="100%">
				<tr>
					<td colspan="2" class="headereffectCenter">
						<span id="spnHeader"></span>
					</td>
				</tr>
				<tr>
					<td class="midcolora">
						<span>Search Option</span>
						<asp:dropdownlist id="ddlSearchColumn" runat="server"></asp:dropdownlist>
					</td>
					<td class="midcolora"><span>Search Criteria</span><asp:textbox id="txtSearch" runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="midcolora"><asp:button CssClass="clsButton" id="btnReset" runat="server" Text="Reset"></asp:button></td>
					<td align="right" class="midcolorr"><asp:button CssClass="clsButton" id="btnSearch" runat="server" Text="Search"></asp:button></td>
				</tr>
			</table>
			<table width="100%">
				<tr>
					<td align="center">
						<div id="div-datagrid">
							<asp:datagrid id="dgLookup" runat="server" AllowPaging="False" AutoGenerateColumns="False" Width="100%"
								AllowSorting="True">
								<AlternatingItemStyle CssClass="AlternateDataRow"></AlternatingItemStyle>
								<ItemStyle CssClass="DataRow"></ItemStyle>
								<HeaderStyle CssClass="DIV#div-datagrid"></HeaderStyle>
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
						<input type="button" class="clsButton" value="Close" onclick="javascript:window.close();" />
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

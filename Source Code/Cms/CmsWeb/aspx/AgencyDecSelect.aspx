<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AgencyDecSelect.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.aspx.AgencyDecSelect" validateRequest = "false" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Agency Dec Select</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<STYLE>.hide { OVERFLOW: hidden; TOP: 5px }
	.show { OVERFLOW: hidden; TOP: 5px }
	#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">
		//function FetchAjaxResponse()
		//{
		//	AgencyDecSelect.AjaxCallFunction(AjaxCallFunction_CallBack);
		//}
		//function AjaxCallFunction_CallBack(response)
		//{
		//}
		function FetchDec()
		{
		}
		
		function DecSelChange(checkfield)
		{
			var field1 = document.getElementById(checkfield);
			var field = field1.getElementsByTagName("input");
			if(document.getElementById('chkDec').checked == true)
			{
				for (var i = 0; i < field.length; i++) 
				{
					field[i].checked = true;
				}
				if('<%=chkAddl.Visible%>' == 'True')
					document.getElementById('chkAddl').checked = true;
			}
			else
			{
				for (var i = 0; i < field.length; i++) 
				{
					field[i].checked = false;
				}
				if('<%=chkAddl.Visible%>' == 'True')
					document.getElementById('chkAddl').checked = false;
			}	
			if('<%=chkAddl.Visible%>' == 'True')
				AddlSelChange('chkAddlList');			
		}
		
		function AddlSelChange(checkfield)
		{
			var field1 = document.getElementById(checkfield);
			var field = field1.getElementsByTagName("input");
			if(document.getElementById('chkAddl').checked == true)
			{
				for (var i = 0; i < field.length; i++) 
				{
					field[i].checked = true;
				}
			}
			else
			{
				for (var i = 0; i < field.length; i++) 
				{
					field[i].checked = false;
				}
			}				
		}
				
		</script>
	</HEAD>
	<body onload="" MS_POSITIONING="GridLayout">
		<form id="indexForm" method="post" runat="server">			
			<table id="tblMVR" width="100%" align="center">
				<tr>
					<td class="midcolora" align="left"><asp:label id="lblMessage" runat="server" CssClass="errmsg">Please select the Dec Page types you want to view.</asp:label></td>
				</tr>
				<tr>
					<!--<td class="midcolorc" align="center"><INPUT class="clsButton" onclick="javascript:window.close();" type="button" value="Close">
					</td>--></tr>
			</table>
			<table width="100%" align="center">
				<tr>
					<TD class="headerEffectSystemParams" colSpan="4">Declaration / Additional Interest 
						selection</TD>
				</tr>
				<tr>
					<td class="midcolorc" align="left"><asp:checkbox id="chkDec" runat="server" onclick="DecSelChange('chkDecList');" Height="56px" Width="176px" Display="Dynamic" Text="  All Declaration Pages"
							align="left" AutoPostBack="False"></asp:checkbox></td>
					<td class="midcolorc" style="WIDTH: 471px" align="left"><asp:checkboxlist class="midcolorba" id="chkDecList" runat="server" Height="56px" Width="350px" Display="Dynamic" AutoPostBack="false"></asp:checkboxlist></td>
				</tr>
				<tr>
					<td class="midcolorc" align="left"><asp:checkbox id="chkAddl" runat="server" onclick="AddlSelChange('chkAddlList');" Height="56px" Width="176px" Display="Dynamic" Text="All Additional Interest"
							align="left" AutoPostBack="False"></asp:checkbox></td>
					<td class="midcolorc" style="WIDTH: 471px" align="left"><asp:checkboxlist class="midcolorba" id="chkAddlList" runat="server" Height="56px" Width="350px" Display="Dynamic" AutoPostBack="false"></asp:checkboxlist></td>
				</tr>
				<tr id="trDecButtons">
					<td class="midcolorc" align="left" colspan="2">
								<INPUT class="clsButton" id="btnClose" onclick="javascript:window.close();" type="button"
								value="Cancel Request" name="btnClose" runat="server"> 
								<INPUT class="clsButton" id="btnFetchDec" onclick="javascript:FetchDec();" type="button"
								value="Request Dec Page(s)" name="btnFetchDec" runat="server">
					</td>
				</tr>
				<tr>
				<!---    end here         -->
				<tr>
					<td align="center" colspan="2"><webcontrol:footer id="pageFooter" runat="server"></webcontrol:footer></td>
				</tr>
			</table>
			<INPUT id="hidKeyValues" type="hidden" name="hidKeyValues" runat="server">
		</form>
		<script>
		</script>
	</body>
</HTML>

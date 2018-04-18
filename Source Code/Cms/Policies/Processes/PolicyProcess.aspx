<%@ Page language="c#" Codebehind="PolicyProcess.aspx.cs" AutoEventWireup="false" Inherits="Policies.Processes.PolicyProcess" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>PolicyProcess</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
    <script src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script src="/cms/cmsweb/scripts/common.js"></script>
	<script src="/cms/cmsweb/scripts/form.js"></script>
	<script src="/cms/cmsweb/scripts/Calendar.js"></script>
  </head>
  <body leftMargin="0" topMargin="0" onload="ApplyColor();ChangeColor();top.topframe.main1.mousein = false;findMouseIn();">
	
    <form id="Form1" method="post" runat="server">
    <DIV><webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu></DIV>
		<!-- To add bottom menu -->
		
		<!-- To add bottom menu ends here -->
		
		<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
		<table class="tableWidth" border="0" cellpadding="0" cellspacing="0" align="center">
			<tr>
				<td align="center" >
					<asp:Label ID="lblMessage"  Runat="server" CssClass="errmsg"></asp:Label>
				</td>
			</tr>
			
		</table>
     </form>
	
  </body>
</html>

<%@ Page language="c#" Codebehind="NegateProcess.aspx.cs" AutoEventWireup="false" Inherits="Policies.Processes.NegateProcess" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="PolicyTop" Src="/cms/cmsweb/webcontrols/PolicyTop.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>NegateProcess</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <script src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script src="/cms/cmsweb/scripts/common.js"></script>
	<script src="/cms/cmsweb/scripts/form.js"></script>
	<script src="/cms/cmsweb/scripts/Calendar.js"></script>
  </head>
  <body leftMargin="0" topMargin="0" onload="ApplyColor();ChangeColor();top.topframe.main1.mousein = false;findMouseIn();">
	
    <form id="Form1" method="post" runat="server">
		<DIV id="myTSMain" style="BEHAVIOR: url(/cms/cmsweb/htc/webservice.htc)"></DIV>
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		
		<TABLE cellSpacing="0" cellPadding="0" width="90%" border="0" Align="center">
			<tr><td><webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer></td></tr>
			<tr>
				<TD><webcontrol:PolicyTop id="cltPolicyTop" runat="server"></webcontrol:PolicyTop></TD>
			</tr>
			<tr><td><webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer></td></tr>
				<tr>
					<td class ="headereffectcenter"><asp:Label ID="capHeader" runat="server"></asp:Label></td><%--Negate Process--%>
				</tr>
		</TABLE>
     </form>
	
  </body>
</html>

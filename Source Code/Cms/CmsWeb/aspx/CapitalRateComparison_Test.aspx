<%@ Page language="c#" Codebehind="CapitalRateComparison_Test.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.CapitalRateComparison_Test" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>Capital Rate Comparison</TITLE>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<LINK href="Common.css" type="text/css" rel="stylesheet">
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
		<script src="/cms/cmsweb/scripts/menubar.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script language="javascript">
		function ShowCapitalQuote()
		{
		  window.open('CapitalRate_QuoteReport.aspx?gID=' + document.getElementById('cmbRUID').options[document.getElementById('cmbRUID').selectedIndex].text);
		}
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" onload="" MS_POSITIONING="GridLayout">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<div class="pageContent" id="bodyHeight">
			<!-- To add bottom menu ends here -->
			<FORM id="frmCapitalInput" method="post" runat="server">
				<table class="tableWidth" cellSpacing="0" width="90%" cellPadding="0" border="0">
					<TR>
						<TD class="pageHeader" style="HEIGHT: 47px" colSpan="4"><asp:label ID="capMessages" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" Visible="False" CssClass="errmsg" Runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD class="midcolora" width="30%"><asp:label id="capATTACH_FILE_NAME" runat="server">File Name</asp:label><SPAN class="mandatory" id="spnFileName" runat="server">*</SPAN></TD>
						<TD class="midcolora" width="70%"><INPUT id="txtATTACH_FILE_NAME" type="file" size="70" name="txtATTACH_FILE_NAME" runat="server">
							<asp:label id="lblATTACH_FILE_NAME" Runat="server"></asp:label><BR>
							<asp:requiredfieldvalidator id="rfvATTACH_FILE_NAME" runat="server" ControlToValidate="txtATTACH_FILE_NAME"
								Display="Dynamic"></asp:requiredfieldvalidator></TD>
						<TD class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Generate Quote"></cmsb:cmsbutton></TD>
					</TR>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="capRUID" runat="server">Service Request ID</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbRUID" runat="server"></asp:dropdownlist></br></TD>
						<TD class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnMakeApp" Visible="true" runat="server" Text="Make Application"></cmsb:cmsbutton></TD>
						<TD class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnShowQuote" Visible="true" runat="server" Text="Show Quote"></cmsb:cmsbutton></TD>
					</tr>
				</table>
				<INPUT id="hidRootPath" type="hidden" name="hidRootPath" runat="server">
			</FORM>
		</div>
	</BODY>
</HTML>

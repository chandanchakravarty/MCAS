<%@ Page language="c#" Codebehind="CheckRegisterItemsPrintPopup.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.CheckRegisterItemsPrintPopup" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS-Checks Register</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script>
		function  ShowPrintDialog()
		{
			for(var i=1;document.getElementById('NotPrintable'+i)!=null;i++)
				document.getElementById('NotPrintable'+i).style.display = "None";
			window.print();
			for(var i=1;document.getElementById('NotPrintable'+i)!=null;i++)
				document.getElementById('NotPrintable'+i).style.display = "Inline";
			return false;
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="ApplyColor();ChangeColor();">
		<form id="frmPrint" method="post" runat="server">
			<TABLE width='100%' border='0' align='center' cellpadding="0" cellspacing="1">
				<tr id="NotPrintable1">
					<td class="midcolora">
						<cmsb:cmsbutton class="clsButton" id="btnDisplay1" runat="server" Text="Display"></cmsb:cmsbutton>
						<cmsb:cmsbutton class="clsButton" id="btnExportToExcel1" runat="server" Text="Export To Excel"></cmsb:cmsbutton>
						<cmsb:cmsbutton class="clsButton" id="btnPrint1" runat="server" Text="Print"></cmsb:cmsbutton>
						<INPUT type="button" class="clsButton" value="Close" onclick="window.close();"></td>
				</TD>
				<tr>
					<TD class="midcolorc" colSpan="1">Date:
						<%=DateTime.Now.ToString().Substring(0,DateTime.Now.ToString().IndexOf(' '))%>
					</TD>
				</tr>
				<tr>
					<TD class="midcolorc" colSpan="1">Time:
						<%=DateTime.Now.TimeOfDay.ToString().Substring(0,DateTime.Now.TimeOfDay.ToString().IndexOf('.'))%>
					</TD>
				</tr>
				<tr id="NotPrintable2">
					<td>
						<TABLE width='100%' id="Table1" border='0' align='center' cellpadding="0" cellspacing="1">
							<tr>
								<TD width="18%" class="midcolora" colSpan="1">From Date <span class="mandatory">*</span></TD>
								<TD class="midcolora" width="36%" colSpan="1"><asp:textbox id="txtFromDate" runat="server" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkFromDate" runat="server" CssClass="HotSpot">
										<asp:image id="imgFromDate" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
									</asp:hyperlink><BR>
									<asp:requiredfieldvalidator id="rfvFromDate" runat="server" Display="Dynamic" ErrorMessage="Please enter From Date."
										ControlToValidate="txtFromDate"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revFromDate" runat="server" ControlToValidate="txtFromDate" ErrorMessage="RegularExpressionValidator"
										Display="Dynamic"></asp:regularexpressionvalidator>
								</TD>
								<TD width="18%" class="midcolora" colSpan="1">To Date<span class="mandatory">*</span></TD>
								<TD width="36%" class="midcolora" colSpan="2">
									<asp:textbox id="txtToDate" runat="server" maxlength="10" size="12"></asp:textbox>
									<asp:hyperlink id="hlkToDate" runat="server" CssClass="HotSpot">
										<asp:image id="imgToDate" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
									</asp:hyperlink><BR>
									<asp:requiredfieldvalidator id="rfvToDate" runat="server" Display="Dynamic" ErrorMessage="Please enter To Date."
										ControlToValidate="txtToDate"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revToDate" runat="server" ControlToValidate="txtToDate" ErrorMessage="RegularExpressionValidator"
										Display="Dynamic"></asp:regularexpressionvalidator>
									<asp:comparevalidator id="cpvEND_DATE" ControlToValidate="txtToDate" Display="Dynamic" Runat="server"
										ErrorMessage="To Date can not be less than From date." ControlToCompare="txtFromDate" Type="Date"
										Operator="GreaterThanEqual"></asp:comparevalidator>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" colSpan="1">Check Type<span class="mandatory">*</span></TD>
								<TD class="midcolora" colSpan="4"><asp:dropdownlist id="cmbCHECK_TYPE" onfocus="SelectComboIndex('cmbCHECK_TYPE')" runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvCHECK_TYPE" runat="server" Display="Dynamic" ErrorMessage="Please Select check Type"
										ControlToValidate="cmbCHECK_TYPE"></asp:requiredfieldvalidator></TD>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td class=""><div id="iReport" name="iReport">
							<asp:Literal id="litReport" runat="server" EnableViewState="False"></asp:Literal></div>
					</td>
				</tr>
				<tr>
					<td class="midcolora" id="NotPrintable3">
						<cmsb:cmsbutton class="clsButton" id="btnDisplay" runat="server" Text="Display"></cmsb:cmsbutton>
						<cmsb:cmsbutton class="clsButton" id="btnExportToExcel" runat="server" Text="Export to Excel"></cmsb:cmsbutton>
						<cmsb:cmsbutton class="clsButton" id="btnPrint" runat="server" Text="Print"></cmsb:cmsbutton>
						<INPUT type="button" class="clsButton" value="Close" onclick="window.close();">
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

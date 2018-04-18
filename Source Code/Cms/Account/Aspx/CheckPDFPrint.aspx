<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="CheckPDFPrint.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.CheckPDFPrint" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS-Checks Register</title> 
		<!--meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script-->
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<STYLE>.hide {
	OVERFLOW: hidden; TOP: 5px
}
.show {
	OVERFLOW: hidden; TOP: 5px
}
#tabContent {
	POSITION: absolute; TOP: 160px
}
		</STYLE>
		<script language="javascript">
		function OpenDecPage()
				{
					VoidChecks();
					if(document.getElementById('hidCheckedRowIDs').value!='')
					{

						dt = new Date();
						var MyWindowName = 'BRICS' + dt.getYear() + dt.getMonth() + dt.getDate() + dt.getHours() + dt.getMinutes() + dt.getSeconds() + dt.getMilliseconds();
						var url ="../../../../cms/application/aspx/DecPage.aspx?CalledFrom=CHECKPDFPRINT&CALLEDFORPRINT=CHECK&CHECK_ID=" + document.getElementById('hidCheckedRowIDs').value ;
						window.open(url,MyWindowName);
						RefreshWebgrid(1);
					}	
					return false;
				} 
		</script>
	</HEAD>
	<body oncontextmenu="return false;" onload="setfirstTime();top.topframe.main1.mousein = false;showScroll();findMouseIn();"
		MS_POSITIONING="GridLayout">
		<form id="indexForm" method="post" runat="server">
			<DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>
			<!-- To add bottom menu -->
			<!-- To add bottom menu ends here -->
			<div class="pageContent" id="bodyHeight">
				<table class="tableWidth" cellSpacing="0" cellPadding="0" border="0">
					<tr>
						<td>
							<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<p align="center"><asp:label id="capMessage" CssClass="errmsg" Visible="False" Runat="server"></asp:label></p>
								<tr>
									<td><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></td>
								</tr>
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
											<tr>
												<td class="midcolorc" align="right" colSpan="2"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
											</tr>
											<TR>
												<TD class="midcolora" width="18%"><asp:label id="capCHECK_TYPE" runat="server">Select Check Type</asp:label><span class="mandatory">*</span></TD>
												<TD class="midcolora" width="82%"><asp:dropdownlist id="cmbCHECK_TYPE" onfocus="SelectComboIndex('cmbCHECK_TYPE')" runat="server" AutoPostBack="True"></asp:dropdownlist><BR>
													<asp:requiredfieldvalidator id="rfvCHECK_TYPE" runat="server" ControlToValidate="cmbCHECK_TYPE" ErrorMessage="Please Select check Type"
														Display="Dynamic"></asp:requiredfieldvalidator></TD>
												<!--td class="midcolora" id="NotPrintable3">
													<cmsb:cmsbutton class="clsButton" id="btnPrint" runat="server" Text="Print To PDF" Enabled="True"></cmsb:cmsbutton>
													<INPUT id="hidAccount" type="hidden" value="0" name="hidAccount" runat="server">
												</td--></TR>
										</table>
									</td>
								</tr>
								<tr>
									<td><asp:placeholder id="GridHolder" runat="server"></asp:placeholder></td>
								</tr>
								<tr>
									<td>&nbsp;</td>
								</tr>
								<tr id="trCheckInfo">
									<td>
										<TABLE class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
											<tr id="tabCtlRow">
												<td><webcontrol:tab id="TabCtl" runat="server" TabLength="150" TabTitles="Check Information" TabURLs="AddCheck.aspx?CalledFrom=Register"></webcontrol:tab></td>
											</tr>
											<tr>
												<td>
													<table class="tableeffect" cellSpacing="0" cellPadding="0" width="100%" border="0">
														<tr>
															<td><iframe class="iframsHeightLong" id="tabLayer" src="" frameBorder="0" width="100%" scrolling="no"
																	runat="server"></iframe>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<asp:label id="lblTemplate" Visible="false" Runat="server"></asp:label></TABLE>
									</td>
								</tr>
								<tr>
									<td><webcontrol:footer id="pageFooter" runat="server"></webcontrol:footer></td>
								</tr>
							</table>
							<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID"><INPUT id="hidKeyValues" type="hidden" name="hidKeyValues" runat="server">
							<input id="hidlocQueryStr" type="hidden" name="hidlocQueryStr"> <input type="hidden" name="hidMode">
							<input id="hidVoidClicked" type="hidden" name="hidVoidClicked" runat="server"> <input id="hidDelString" type="hidden" name="hidDelString" runat="server">
							<input id="hidCheckedRowIDs" type="hidden" name="hidCheckedRowIDs" runat="server">
							<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
						</td>
					</tr>
				</table>
			</div>
			<DIV>&nbsp;</DIV>
		</form>
	</body>
</HTML>

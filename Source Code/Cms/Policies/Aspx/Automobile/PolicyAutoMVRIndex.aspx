<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="PolicyAutoMVRIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.PolicyAutoMVRIndex" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddMvrInformationIndex</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<SCRIPT src="/cms/cmsweb/scripts/common.js"></SCRIPT>
		<SCRIPT src="/cms/cmsweb/scripts/form.js"></SCRIPT>
		<!--<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">-->
		<STYLE>.hide { OVERFLOW: hidden; TOP: 5px }
	.show { OVERFLOW: hidden; TOP: 5px }
	#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">
		function onRowClicked(num,msDg )
		{			
			rowNum = num;
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);		
			changeTab(0, 0);
		}
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout" onload="setfirstTime();">
		
		<div class="pageContent" id="bodyHeight">
			<form id="indexForm" method="post" runat="server">
				<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0">
					<tr>
						<TD class="pageHeader" colSpan="4">
							<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
						</TD>
						<td>
							<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
								<tr>
									<td align="center">
										<asp:label id="capMessage" CssClass="errmsg" Visible="False" Runat="server"></asp:label>
									</td>
								</tr>
								<tr>
									<td id="tdGridHolder">
										<webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer>
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
									</td>
								</tr>
								<tr>
									<td>
										<webcontrol:gridspacer id="Gridspacer" runat="server"></webcontrol:gridspacer>
									</td>
								</tr>
								<tr>
									<td>
										<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
											<tr id="tabCtlRow">
												<td>
													<webcontrol:tab id="TabCtl" runat="server" TabLength="150" TabTitles="MVR Information" TabURLs="AddMvrInformation.aspx?"></webcontrol:tab>
												</td>
											</tr>
											<tr>
												<td>
													<table class="tableeffect" cellSpacing="0" cellPadding="0" border="0">
														<tr>
															<td>
																<iframe class="iframsHeightLong" id="tabLayer" src="" frameBorder="0" width="100%" scrolling="no"
																	runat="server"></iframe>
															</td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
										<asp:label id="lblTemplate" Visible="false" Runat="server"></asp:label>
									</td>
								</tr>
								<tr>
									<td>
										<webcontrol:footer id="pageFooter" runat="server"></webcontrol:footer>
									</td>
								</tr>
							</table>
							<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
							<input type="hidden" id="hidCustomerID" name="hidCustomerID" runat="server"> <input type="hidden" id="hidAppVersionID" name="hidAppVersionID" runat="server">
							<input type="hidden" id="hidAppID" name="hidAppID" runat="server"> <input type="hidden" id="hidDriverID" name="hidDriverID" runat="server">
							<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
							<input type="hidden" id="hidCalledFrom" runat="server" NAME="hidCalledFrom"> <input id="hide" type="hidden" name="ConVar">
							<span id="singleRec"></span>
						</td>
					</tr>
				</table>
			</form>
		</div>
	</body>
</HTML>

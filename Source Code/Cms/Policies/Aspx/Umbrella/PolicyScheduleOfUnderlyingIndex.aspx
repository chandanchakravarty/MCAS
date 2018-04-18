<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page validateRequest="false" CodeBehind="PolicyScheduleOfUnderlyingIndex.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Umbrella.PolicyScheduleOfUnderlyingIndex" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ScheduleOfUnderlyingIndex</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<!--<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">-->
		<STYLE>
		.hide { OVERFLOW: hidden; TOP: 5px }
		.show { OVERFLOW: hidden; TOP: 5px }
		#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">
	
		
		function onRowClicked(num,msDg )
		{
		
			rowNum = num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);	
			changeTab(0,0)
			
		}
		
	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" leftmargin="0" topmargin="0" rightmargin="0" onload="setfirstTime();">
		
		<div class="pageContent" id="bodyHeight">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td>
						<form id="indexForm" method="post" runat="server">
							<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
							<p align="center"><asp:label id="capMessage" CssClass="errmsg" Visible="False" Runat="server"></asp:label></p>
							<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<tr>
									<td class="pageHeader" id="tdWorkflow" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow></td>
								</tr>
								<tr>
									<td id="tdGridHolder"><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer><asp:placeholder id="GridHolder" runat="server"></asp:placeholder></td>
								</tr>
								<tr>
									<td>&nbsp;</td>
								</tr>
								<tr>
									<td>
										<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
											<tr id="tabCtlRow">
												<td><webcontrol:tab id="TabCtl" runat="server" TabLength="200" TabTitles="Schedule Of Underlying" TabURLs="PolicyAddScheduleOfUnderlying.aspx?"></webcontrol:tab></td>
											</tr>
											<tr>
												<td>
													<table class="tableeffect" cellSpacing="0" cellPadding="0" border="0">
														<tr>
															<td><iframe class="iframsHeightLong" id="tabLayer" src="" frameBorder="0" width="100%" scrolling="no"
																	runat="server"></iframe>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<asp:label id="lblTemplate" Visible="false" Runat="server"></asp:label></table>
									</td>
								</tr>
								<tr>
									<td><webcontrol:footer id="pageFooter" runat="server"></webcontrol:footer></td>
								</tr>
							</table>
							<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
							<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
						</form>
					</td>
				</tr>
			</table>
		</div>
		<DIV></DIV>
	</body>
</HTML>



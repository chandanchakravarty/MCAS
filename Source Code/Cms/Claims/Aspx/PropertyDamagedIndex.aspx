<%@ Page language="c#" Codebehind="PropertyDamagedIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.PropertyDamagedIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>InsuredVehicleIndex</title>
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
		document.getElementById('hidCalledFrom').value = 'Index';
			rowNum = num;
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);		
			changeTab(0, 0);
		}
		</script>
	</HEAD>
	<body oncontextmenu="return false;" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout" onload="setfirstTime();" >
		
		<div class="pageContent" id="bodyHeight">
			<form id="indexForm" method="post" runat="server">				
				<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0">
					<tr>						
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
													<webcontrol:tab id="TabCtl" runat="server"></webcontrol:tab>
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
						<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
						<input type="hidden" id="hidCUSTOMER_ID" runat="server" name="hidCUSTOMER_ID">
						<input type="hidden" id="hidPOLICY_ID" runat="server" name="hidPOLICY_ID">
						<input type="hidden" id="hidPOLICY_VERSION_ID" runat="server" name="hidPOLICY_VERSION_ID">
						<input type="hidden" id="hidLOB_ID" runat="server" name="hidLOB_ID" value="">
						<%--<input type="hidden" id="hidTYPE_OF_HOME" runat="server" name="hidTYPE_OF_HOME">--%>
						<input type="hidden" id="hidCLAIM_ID" runat="server" name="hidCLAIM_ID">
						<input id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server">						
						</td>
					</tr>
				</table>
			</form>
		</div>
	</body>
</HTML>

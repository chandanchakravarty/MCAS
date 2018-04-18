<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Page language="c#" Codebehind="EndorsementAttachmentIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.EndorsementAttachmentIndex" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<HTML>
	<HEAD>
		<title>Endorsment</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		
		<STYLE>
		.hide { OVERFLOW: hidden; TOP: 5px }
		.show { OVERFLOW: hidden; TOP: 5px }
		#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">
		function onRowClicked(num,msDg )
		{
		//alert('Hello');
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);		
			changeTab(0, 0);
		}
		</script>
	</HEAD>
	<body leftmargin="0" topmargin ="0" rightmargin="0" onload="setfirstTime();showScroll();"  MS_POSITIONING="GridLayout">
		<div id="bodyHeight" class="pageContent">
		<table class="tableWidthHeader" border="0" cellpadding="0" cellspacing="0">
			<tr>
				<td>
					<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
						<form id="indexForm" method="post" runat="server">
							<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
							<p align="center"><asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label></p>
							<tr>
								<td>
									<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
									<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
								</td>
							</tr>
							<tr>
								<td><webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer></td>
							</tr>
							<tr>
								<td>
									<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
										<tr id="tabCtlRow">
											<td>
												<webcontrol:Tab ID="TabCtl" runat="server" TabTitles="Endorsement Attachment"
													TabLength="250"></webcontrol:Tab>
											</td>
										</tr>
										<tr>
											<td>
												<table class="tableeffect" cellpadding="0" cellspacing="0" border="0">
													<tr>
														<td>
															<iframe class="iframsHeightLong" id="tabLayer" runat="server" src="" scrolling="no" frameborder="0"
																width="100%"></iframe>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<asp:Label ID="lblTemplate" Runat="server" Visible="false"></asp:Label>
									</table>
								</td>
							</tr>
							<tr>
								<td>
									<webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
								</td>
							</tr>
					</table>
					<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
					<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
					</FORM>
				</td>
			</tr>
		</table>
		</div>
	</body>
</HTML>

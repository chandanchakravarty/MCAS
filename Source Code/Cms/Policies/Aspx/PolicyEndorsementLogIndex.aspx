<%@ Page language="c#" Codebehind="PolicyEndorsementLogIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.PolicyEndorsementLogIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Endorsement Log Index</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<!--<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">-->
		<STYLE>
		.hide { OVERFLOW: hidden; TOP: 5px }
		.show { OVERFLOW: hidden; TOP: 5px }
		#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<SCRIPT src="../../cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script language="javascript">
		
		function findMouseIn()
		{
			if(!top.topframe.main1.mousein)
			{
				//createActiveMenu();
				top.topframe.main1.mousein = true;
			}
			setTimeout('findMouseIn()',5000);
		}
		
		function onRowClicked(num,msDg )
		{ 
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";			
			populateXML(num,msDg);		
			changeTab(0,0);
		}
		</script>
	</HEAD>
	<body oncontextmenu="return false;" leftmargin="0" topmargin="0" onload="setfirstTime();top.topframe.main1.activeMenuBar='1,2';top.topframe.main1.mousein = false;findMouseIn();"
		MS_POSITIONING="GridLayout">		
		<div id="bodyHeight" class="pageContent">
			<form id="indexForm" method="post" runat="server">
				<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
					<tr>
						<td>
							<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
							<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
						</td>
					</tr>
					<tr>
						<td><webcontrol:GridSpacer id="Gridspacer" runat="server"></webcontrol:GridSpacer></td>
					</tr>
					<tr id="tabCtlRow">
						<td>
							<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="/cms/cmsweb/maintenance/TransactionLogDetail.aspx?calledfrom=ENDORSEMENT&amp;"
								TabTitles="Endorsement Details" TabLength="150"></webcontrol:Tab>
						</td>
					</tr>
					<tr>
						<td>
							<table class="tableWidthHeader" height="100%" cellSpacing="0" cellPadding="0" border="0" align="center">
								<tr>
									<td>
										<table class="tableeffect" cellSpacing="0" cellPadding="0" border="0">
											<tr>
												<td><iframe class="iframsHeightMedium" id="tabLayer" src="" frameBorder="0" width="100%" scrolling="yes"
														runat="server"></iframe>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<asp:Label ID="lblTemplate" Runat="server" Visible="false"></asp:Label>
							</table>
						</td>
					</tr>
				</table>
				<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
				<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
				<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span><INPUT id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
				<INPUT id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server">
			</form>
		</div>
		<DIV></DIV>
	</body>
</HTML>

<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Page language="c#" AutoEventWireup="false" CodeBehind  = "PriorPolicyIndex.aspx.cs" Inherits="Cms.Application.Aspx.PriorPolicyIndex" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Sample for Wolvorine</title>
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
			#tabContent {POSITION: absolute; TOP: 160px }
		</STYLE>
		<SCRIPT src="../../cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script language="javascript">
		//Done for Itrack Issue 6708 on 19 Nov 09
		function FetchPriorPolicyReport()
		{
		    var msgCon = document.getElementById('hidmsg').value;
		    var con = confirm(msgCon); //"Do you wish to Fetch Prior Policy Report?"
			if(con)
			{
			  window.open('\\cms\\cmsweb\\aspx\\LossReport.aspx?CUSTOMER_ID=<%=customer_ID%>&CalledFor=PriorPolicy',null,"width=600,height=600,scrollbars=1,menubar=0,resizable=1");
			}
			else
			{
				return false;
			}
		}
		
		function onRowClicked(num,msDg )
		{
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);		
			changeTab(0, 0);
}
        function Init() {         
    try {     
        //setfirstTime();
        top.topframe.main1.mousein = false;
       
    }
    catch (e) {
    }
}
		</script>
		
	</HEAD>
	<body oncontextmenu="return false;" leftmargin="0" topmargin="0" MS_POSITIONING="GridLayout" onload="Init();">
		
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<form id="indexForm" method="post" runat="server">
			<table class="tableWidth" border="0" cellpadding="0" cellspacing="0" align="center">
				<tr>
					<td>
						<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
							<TR>
								<TD align=center>
									<asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label>
								</TD>
							</TR>	
							<%--<tr>
								<TD id = "tdClientTop" class="pageHeader" colSpan="4">
									<webcontrol:ClientTop id="cltClientTop" runat="server" ></webcontrol:ClientTop>
								</TD>
							</tr>	--%>	
							<tr>
								<td>
									<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
									<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
								</td>
							</tr>
							<tr><td><webcontrol:GridSpacer id="Gridspacer" runat="server"></webcontrol:GridSpacer></td></tr>
							<tr>
								<td>					
									<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
										<tr id="tabCtlRow">
											<td>
												<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="AddPriorPolicy.aspx?" TabTitles="Prior Policy" TabLength="150"></webcontrol:Tab>
											</td>
										</tr>
										<tr>
											<td>
												<table class="tableeffect" width="100%" cellpadding="0" cellspacing="0" border="0">
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
                        <input type="hidden" id="hidmsg" runat="server" />	
						<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
						<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
						<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>	
                       </td>
				</tr>
			</table>
			</form>
		</div>
		</div>
	</body>
</HTML>

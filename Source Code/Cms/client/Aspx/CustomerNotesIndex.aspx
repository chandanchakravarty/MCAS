<%@ Page language="c#" Codebehind="CustomerNotesIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Client.aspx.CustomerNotesIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="ClaimTop" Src="/cms/cmsweb/webcontrols/ClaimTop.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Receber Notificação</title>
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
	
		
		function onRowClicked(num,msDg )
		{
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);		
			changeTab(0, 0);
		}
		function showList()
		{
				SingleCombinedText();
				if(document.getElementById('hidDelString').value!="")
				//var delstring = document.getElementById('hidDelString').value;
				document.location.href ="SingleCombinedList.aspx?customer_ID=<%=customer_ID%>&NOTES_ID="+document.getElementById('hidDelString').value;
				else 
				return false
		}		
		
		function showTop()
		{
			if (document.getElementById('hidCalledFrom').value == 'CLAIMS')
			{
				document.getElementById('trClaimTop').style.display="inline";
				document.getElementById('trClientTop').style.display="none";
				if(document.getElementById('hidClaimsPopUp').value=="1")
					setTimeout("addRecord();",1000);
			}
			else
			{
				document.getElementById('trClientTop').style.display="inline";
				document.getElementById('trClaimTop').style.display="none";
			}
			
		}
		function Init()
		{
			setfirstTime();
			if(document.getElementById('hidCalledFrom').value == 'CLAIMS' && document.getElementById('hidClaimsPopUp').value == '1')
			{
				if(document.getElementById('bottomMenu'))
					document.getElementById('bottomMenu').style.display="none";
			}
			else
			{
				
				if(typeof(top.topframe)!='undefined')
				{
					top.topframe.main1.mousein = false;
					findMouseIn();
				}
				
			}
			showTop();
			//SingleCombinedText();
		}		
		</script>
	</HEAD>
	<body oncontextmenu="return false;" leftMargin="90" rightMargin="0" onload="Init();" MS_POSITIONING="GridLayout"><!--Done for Itrack Issue 5138 on 21 May 09-->		
		<!-- To add bottom menu -->
		<%if(!(hidCalledFrom.Value=="CLAIMS" && hidClaimsPopUp.Value=="1"))%>
			<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 100%"><%--!--UnCommented for Itrack Issue 5138 on 21 May 09%--%>
		<div id="bodyHeight1" class="pageContent"><%-- Commented for Itrack Issue 5138 on 21 May 09--%>
			<table class="tableWidth"  border="0" cellpadding="0" cellspacing="0" width="90%">
				<tr>
					<td>
						<table class="tableWidthHeader"  cellSpacing="0" cellPadding="0" border="0" align="center">
						
						<form id="indexForm" method="post" runat="server">
							<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
							<p align="center"><asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label></p>
						<tr id="trClientTop">
							<TD id = "tdClientTop" class="pageHeader" colSpan="4">
								<webcontrol:ClientTop id="cltClientTop" runat="server" width ="98%" ></webcontrol:ClientTop>
							</TD>
						</tr>
						<tr id="trClaimTop">
							<td class="pageHeader" colspan="4">
								<webcontrol:ClaimTop id="cltClaimTop" runat="server"></webcontrol:ClaimTop>
							</td>
						</tr>
						<tr><td><webcontrol:GridSpacer id="Gridspacer2" runat="server"></webcontrol:GridSpacer></td></tr>	
						<tr>
							<td>
								<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
							</td>
						</tr>
						<tr><td><webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer></td></tr>
						<tr>
							<td>					
								<table class="tableWidthHeader"  cellSpacing="0" cellPadding="0" border="0" align="center">
									<tr id="tabCtlRow">
										<td>
												<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="AddCustomerNotes.aspx?" TabTitles="Customer Notes" TabLength="150"></webcontrol:Tab>
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
						<input type="hidden" name="hidCalledFrom" id="hidCalledFrom" runat="server" value="">
						<input type="hidden" name="hidSelectedPolicy" id="hidSelectedPolicy" runat="server" value="">
						<input type="hidden" name="hidClaimsPopUp" id="hidClaimsPopUp" runat="server" value="">
						<input type="hidden" name="hidCLAIM_ID" id="hidCLAIM_ID" runat="server" value="">
						<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
						<input type="hidden" id="hidDelString" name="hidDelString" runat=server>   
					</form>
				</td>
			</tr>
		</table>
		<!--/div-->
		</div>
	</body>
</HTML>

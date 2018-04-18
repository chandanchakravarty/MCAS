<%@ Page language="c#" Codebehind="JournalEntryDetailIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.JournalEntryDetailIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS</title>
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
		<script language="javascript">
	
		function onRowClicked(num,msDg )
		{
			rowNum = num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);		
			changeTab(0, 0);
			
		}
		function back()
		{
			parent.changeTab(0,0);
			return false;
		}
		function ShowJournalInfo()
		{
			//document.indexForm.scrollIntoView(1);
			if (document.getElementById("hidJournalInfoXML").value != "")
			{
				//Parsing the XML
				var objXmlHandler = new XMLHandler();
				var tree = (objXmlHandler.quickParseXML(document.getElementById("hidJournalInfoXML").value).getElementsByTagName('Table')[0]);
				
				nod = tree.getElementsByTagName("LEDGER_NAME");
				if (nod != null)
				{
					document.getElementById("lblGeneralLedger").innerHTML = nod[0].childNodes[0].text;
				}
				nod = tree.getElementsByTagName("TRANS_DATE");
				if (nod != null)
				{
					document.getElementById("lblDate").innerHTML = nod[0].childNodes[0].text;
				}
				nod = tree.getElementsByTagName("JOURNAL_ENTRY_NO");
				if (nod != null)
				{
					document.getElementById("lblEntryNo").innerHTML = nod[0].childNodes[0].text;
				}
				nod = tree.getElementsByTagName("PROFF");
				if (nod != null)
				{
					//document.getElementById("lblProof").innerHTML = parseFloat(nod[0].childNodes[0].text).toFixed(2);
					document.getElementById("lblProof").innerHTML = nod[0].childNodes[0].text;
				}
				
			}
		}
		function SetParentElement()
		{
			var Deopsit_Id = document.getElementById('hidJOURNAL_ID').value;
			window.parent.self.document.forms[0].hidSELECTED_JOURNAL_ID.value = Deopsit_Id ;
		}
		
		</script>
	</HEAD>
	<body leftmargin="0" rightmargin="0" onload="setfirstTime();ShowJournalInfo();SetParentElement();top.topframe.main1.mousein = false;" MS_POSITIONING="GridLayout">

		

		<div id="bodyHeight" class="pageContent">
			<form id="indexForm" method="post" runat="server">
				<table class="tableWidthHeader" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td>
							<table class="tableWidthHeader" width="100%" cellSpacing="0" cellPadding="0" border="0" align="center">

								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<p align="center"><asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label></p>
								<tr>
									<td>
										<table class="tableeffectTopHeader" width="100%" cellpadding="0" cellspacing="0">
											<tr>
												<td class="midcolora" width="15%">
													<span class="labelfont">Journal Entry No :</span>
												</td>
												<td class="midcolora" width="59%">
													<asp:Label ID="lblEntryNo" Runat="server"></asp:Label>
												</td>
												<td class="midcolora" width="7%">
													<span class="labelfont">Date :</span>
												</td>
												<td class="midcolora" width="27%">
													<asp:Label ID="lblDate" Runat="server"></asp:Label>
												</td>
											</tr>
											<tr>
												<td class="midcolora">
													<span class="labelfont">General Ledger :</span>
												</td>
												<td class="midcolora">
													<asp:Label ID="lblGeneralLedger" Runat="server"></asp:Label>
												</td>
												<td class="midcolora">
													<span class="labelfont">Proof :</span>
												</td>
												<td class="midcolora">
													<asp:Label ID="lblProof" Runat="server"></asp:Label>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td><webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer></td>
								</tr>
								<tr>
									<td id="tdGridHolder">
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
									</td>
								</tr>
								<tr>
									<td><webcontrol:GridSpacer id="Gridspacer" runat="server"></webcontrol:GridSpacer></td>
								</tr>
								<tr>
									<td>
										<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
											<tr id="tabCtlRow">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="AddJournalEntryDetail.aspx?" TabTitles="Journal Entry Information" TabLength="190"></webcontrol:Tab>
												</td>
											</tr>
											<tr>
												<td>
													<table class="tableeffect" cellpadding="0" cellspacing="0" border="0">
														<tr>
															<td>
																<iframe class="iframsHeightLong" id="tabLayer" runat="server" src="" scrolling="no" frameborder="0" width="100%"></iframe>
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
							<input type="hidden" id="hidJournalInfoXML" runat="server" name="hidJournalInfoXML">
							<INPUT id="hidJOURNAL_ID" type="hidden" value="0" name="hidJOURNAL_ID" runat="server">
						</td>
					</tr>
				</table>
			</form>
		</div>
	</body>
</HTML>

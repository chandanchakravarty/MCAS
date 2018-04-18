<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepositDetailsIndex.aspx.cs" Inherits="Cms.Account.Aspx.DepositDetailsIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>Deposit Details Index Page</title>
    		<link rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css"/>
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
    <STYLE>
		.hide { OVERFLOW: hidden; TOP: 5px }
		.show { OVERFLOW: hidden; TOP: 5px }
		#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">

		    function onRowClicked(num, msDg) {
		        rowNum = num;
		        if (parseInt(num) == 0)
		            strXML = "";
		        populateXML(num, msDg);
		        arrScreenIds[0] = "187_0";
		        changeTab(0, 0);

		    }
		    function SetParentElement() {

		        var Deopsit_Id = document.getElementById('hidDEPOSIT_ID').value;
		        window.parent.self.document.forms[0].hidSELECTED_DEP_ID.value = Deopsit_Id;
		    }

	
		</script>
</head>
<body oncontextmenu="return false;" leftmargin="0" rightmargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;SetParentElement();"
		MS_POSITIONING="GridLayout">
		
		<div id="bodyHeight" class="pageContent">
			<table class="tableWidthHeader" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<table class="tableWidthHeader" width="100%" cellSpacing="0" cellPadding="0" border="0"
							align="center">
							<form id="indexForm" method="post" runat="server">
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<p align="center"><asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label></p>
								<tr>
								<td class="midcolora" colspan="2"><b><asp:Label ID="lblDepositNum" Runat="server">Deposit Number :</asp:Label></b>
									<b>
										<asp:Label ID="lblDEPOSIT_NUM" Runat="server"></asp:Label></b></td>
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
													<webcontrol:Tab ID="TabCtl" runat="server" ></webcontrol:Tab>
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
						<input id="hidDEPOSIT_ID" type="hidden" value="0" name="hidDEPOSIT_ID" runat="server"/>  
						</FORM>
					</td>
				</tr>
			</table>
		</div>
		<DIV></DIV>
	</body>
</html>

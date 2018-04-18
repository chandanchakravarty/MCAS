<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReserveTab.aspx.cs" Inherits="Cms.Claims.Aspx.ReserveTab" %>

<%@ Register TagPrefix="webcontrol" TagName="ClaimTop" Src="/cms/cmsweb/webcontrols/ClaimTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>

<%@ Register TagPrefix="webcontrol" TagName="ClaimActivityTop" Src="/cms/cmsweb/webcontrols/ClaimActivityTop.ascx" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    
  
		
		<title>AddReserveDetails</title>
     <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
		<script language="javascript">
//		    function CreateMenu() {
//		        if (top.topframe.main1.menuXmlReady == false)
//		            setTimeout("setMenu();", 1000);

//		        top.topframe.main1.activeMenuBar = '2'; //Claims
//		        top.topframe.createActiveMenu();

//		    }
//		    function showTab() {
//		     
//		        changeTab(0, 0);
//		    }
//		   
//		    function SetMenu() {
//		      
//		    }
//		    /*function ValidateCheckbox(objSource,objArgs)
//		    {
//		    objArgs.IsValid = false;
		    //		    }*/
		    var ActivityClientID;
		    var ActivityTotalPaymentClientID;

		    function showTab() {
		        changeTab(0, 0);
		    }
		    function GoBack(PageName) {
		        strURL = PageName + "?&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&ACTIVITY_ID=" + document.getElementById("hidACTIVITY_ID").value + "&";
		        this.document.location.href = strURL;
		        return false;
		    }
		    function Init() {
		        top.topframe.main1.mousein = false;
		        findMouseIn();
		        changeTab(0, 0);
		        showScroll();
		        ActivityClientID = '<%=ActivityClientID%>'
		        ActivityTotalPaymentClientID = '<%=ActivityTotalPaymentClientID%>'
		    }
		</script>

</head>
<body onload="Init();">
    <webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<form id="ReserveDetails" method="post" runat="server">
				<table class="tableWidth"   border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td align="center">
							<asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label>
						</td>
					</tr>
					<tr>
						<TD id="tdClaimTop" class="pageHeader">
							<webcontrol:ClaimTop id="cltClaimTop" runat="server" width="100%"></webcontrol:ClaimTop>
						</TD>
					</tr>
					<tr>
						<TD id="tdClaimTop" class="pageHeader">
							<webcontrol:ClaimActivityTop id="cltClaimActivityTop" runat="server"></webcontrol:ClaimActivityTop>
						</TD>
					</tr>
					<tr id="formTable" runat="server" style="width:100%" >
						<td  >
							<table class="tableWidthHeader" style="width:100%"  cellSpacing="0" cellPadding="0" border="0" align="center">
								<tr>
									<TD id="tdClientTop" class="pageHeader" colSpan="4">
										<webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer>
									</TD>
								</tr>
								
								<tr>
									<td>
										<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="" TabTitles=""
											TabLength="80"></webcontrol:Tab>
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
								<tr>
									<td>
										<webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
				<INPUT id="hidCALLED_FROM" type="hidden" value="0" name="hidCALLED_FROM" runat="server">
                <INPUT id="hidACTIVITY_ID" type="hidden" value="0" name="hidACTIVITY_ID" runat="server">
                 <INPUT id="hidACTIVITY_TYPE" type="hidden" value="0" name="hidACTIVITY_TYPE" runat="server">
                 <INPUT id="hidACTIVITY_ACTION_ON_PAYMENT" type="hidden" value="0" name="hidACTIVITY_ACTION_ON_PAYMENT" runat="server">
                 <INPUT id="hidACTIVITY_STATUS" type="hidden" value="0" name="hidACTIVITY_STATUS" runat="server">
                <INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			    <INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
			    <INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			    <INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			    <INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
			    <INPUT id="hidIS_RESERVE_EXIST" type="hidden" value="N" name="hidLOCATION_STATE" runat="server">
			    <INPUT id="hidMESSAGE" type="hidden" value="" name="hidMESSAGE" runat="server">
			</form>
		</div>
</body>
</html>

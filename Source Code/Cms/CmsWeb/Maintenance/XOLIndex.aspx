<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XOLIndex.aspx.cs" Inherits=" Cms.CmsWeb.Maintenance.XOLIndex" %>

<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>

<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>





<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    
     <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
		
		
		<script type="text/javascript">


		    function onRowClicked(num, msDg) {
		        rowNum = num;
		        if (parseInt(num) == 0)
		            strXML = "";
		        populateXML(num, msDg);
		        changeTab(0, 0);
		    }

		    function findMouseIn() {
		        if (!top.topframe.main1.mousein) {
		            //createActiveMenu();
		            top.topframe.main1.mousein = true;
		        }
		        setTimeout('findMouseIn()', 5000);
		    }

		</script>
</head>
<body oncontextmenu="return false;" leftMargin="0" topMargin="0" rightMargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();">
    <form id="form1" runat="server">
    <div class="pageContent" id="bodyHeight">
    <input type="hidden" id="Hidden3" runat="server" name="Hidden3" />
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
													<webcontrol:tab id="TabCtl" runat="server"  TabLength="150" TabTitles="Third Party Damage" ></webcontrol:tab>
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
							
						
						<input type="hidden" id="hidCUSTOMER_ID" runat="server" name="hidCUSTOMER_ID" />
						<input type="hidden" id="hidPOLICY_ID" runat="server" name="hidPOLICY_ID" />
						<input type="hidden" id="hidPOLICY_VERSION_ID" runat="server" name="hidPOLICY_VERSION_ID" />
						<input type="hidden" id="hidLOB_ID" runat="server" name="hidLOB_ID" value="" />						
						<input type="hidden" id="hidCLAIM_ID" runat="server" name="hidCLAIM_ID" />
						<input id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server" />		
						<input type="hidden" name="hidTemplateID" id="hidTemplateID"  runat="server"> 
						<input type="hidden" name="hidRowID"  id="hidRowID" runat="server">
						<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"  runat="server"> 
						<input type="hidden" name="hidMode"  id="hidMode" runat="server">	
						<input type="hidden" id="hidAUTHORIZE" name="hidAUTHORIZE" runat="server">		
						
						<input type="hidden" id="hidCONTRACT_ID" name="hidCONTRACT_ID" runat="server">		
						
						
						</td>
					</tr>
				</table>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="RatesTab.aspx.cs" Inherits="Cms.CmsWeb.Maintenance.RatesTab" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	   <%-- <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet">--%>
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
        <link href="/cms/cmsweb/css/css1.css" type="text/css" rel="stylesheet">
        
        
		<script language="javascript">
		    function showTab() {
		        changeTab(0, 0);                
		    }           
		    
		</script>
</head>
<body  oncontextmenu = "return false;" MS_POSITIONING="GridLayout" onload="showTab();">		
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
    <div id="bodyHeight"   class="pageContent">
			<form id="Form1" width="100%" method="post" runat="server">
				<table  width="100%" border="0" cellpadding="0" cellspacing="0">				
					<tr id="formTable" runat="server">
						<td>
							<table class="tableWidthHeader" height="100%"  width="100%" cellSpacing="0" cellPadding="0" border="0" align="center">
								<tr>
									<td width="100%">
										<webcontrol:Tab ID="TabCtl" runat="server" TabURLs=""
											TabTitles="Interest,Fee,IOF" TabLength="310"></webcontrol:Tab>
									</td>
								</tr>
								<tr>
									<td>
										<table class="tableeffect" width="100%" height="100%" cellpadding="0" cellspacing="0" border="0">
											<tr>
												<td>
													<iframe class="iframsHeightLong" id="tabLayer"  runat="server" src="" scrolling="no" frameborder="0"
														width="100%"></iframe>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								
								<tr>
									<td><asp:Label ID="lblTemplate" Runat="server" Visible="false"></asp:Label>
										<webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>                                         
									</td>
								</tr>								
							</table>
						</td>
					</tr>
				</table>
			</form>
		</div>
</body>
</html>

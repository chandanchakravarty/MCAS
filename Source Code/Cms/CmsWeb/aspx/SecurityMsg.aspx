<%@ Page language="c#" Codebehind="SecurityMsg.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Aspx.SecurityMsg" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>SecurityMsg</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
    <LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
    <script language=javascript>
    function disablePopupMenu()
    {
		
		if(top.topframe)
		{
			//alert(top.topframe)
			top.topframe.main1.mousein = false;findMouseIn();
			}
		
			
    }
    </script>  
  </head>
  
  <body MS_POSITIONING="GridLayout" onload="disablePopupMenu();">
	<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
	<!-- To add bottom menu ends here -->
	<div id="bodyHeight" class="pageContent">
    <form id="Form1" method="post" runat="server">
			<table cellSpacing='0' class="tableWidth" cellPadding='0' border='0'>
				<tr>
					<TD id="tdClientTop" class="pageHeader" colSpan="4">
						<webcontrol:ClientTop id="cltClientTop" runat="server" width="100%"></webcontrol:ClientTop>
					</TD>
				</tr>
				<tr>
					<td class="pageHeader" align="center">
						<table width='100%' border='0' align='center'>
							<tr><td>&nbsp;</td></tr>
							<tr>
								<td class="midcolorc"  >
									<asp:Label Runat="server" ID='lblSecurityScr'></asp:Label>	<%--The rights to access this screen is not available.
									<BR> Please contact administrator for further communications.--%>
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

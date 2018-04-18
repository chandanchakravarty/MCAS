<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Page language="c#" Codebehind="EntitiesExcludedIn1099.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.Entities_Excluded_In_1099" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Entities Excluded 1099</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta http-equiv="Page-Enter" content="revealtrans(duration=0.0)">
		<meta http-equiv="Page-Exit" content="revealtrans(duration=0.0)">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
	
			function ShowReport()
			{	
				var Year="";
				Year  = document.getElementById('cmb_Year').options[document.getElementById('cmb_Year').selectedIndex].value;
								
				if(Year == "")
					Year = "NULL";
											
				var url="CustomReport.aspx?PageName=1099Report&YEAR="+ Year;
				var windowobj = window.open(url,'1099Report','resizable=yes,scrollbar=yes,top=100,left=50')
				windowobj.focus();							
			}	
	
	
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" class="bodyBackGround" topMargin="0" onload="ApplyColor();ChangeColor();top.topframe.main1.mousein = false;findMouseIn();showScroll();">
		<webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<form id="EntitiesExcludedIn1099" method="post" runat="server">
			<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
				<TBODY align="center">
					<TR>
						<TD class="headereffectcenter" colSpan="4">Entities Excluded 1099</TD>
					</TR>
					<TR></TR><TR></TR><TR></TR>
					
					<TR>
						<TD class="midcolora"  width="23%"></TD>					
						<TD class="midcolorr" colSpan="2">
							YEAR: <asp:dropdownlist id="cmb_Year" width="43%" runat="server"></asp:dropdownlist></TD>
							</TD>						
						<TD class="midcolorr" align = "center">
						<CMSB:CMSBUTTON class="clsButton" id="btnReport" runat="server" Text="Display Report"></CMSB:CMSBUTTON>
						</TD>
					</TR>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>

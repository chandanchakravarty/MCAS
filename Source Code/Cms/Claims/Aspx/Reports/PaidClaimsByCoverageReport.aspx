<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PaidClaimsByCoverageReport.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.Reports.PaidClaimsByCoverageReport" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>Claims Count by Adjuster with Summary</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript">
		
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";

			
		function PrintReport()
		{
			//document.getElementById("btnExportToFile").style.display="none";
			document.getElementById("btnExportToExcel").style.display="none";
			document.getElementById("btnPrintReport").style.display="none";
			//document.getElementById("pnlCoverageReport").style.overflow = "";
			//document.getElementById("pnlCoverageReport").style.height = "";
			document.getElementById("tblCovReport").setAttribute("border","1");
			document.getElementById("tblCovReport").setAttribute("style","border-collapse: collapse");			
			window.print();
			//document.getElementById("pnlCoverageReport").style.overflow = "auto";
			//document.getElementById("pnlCoverageReport").style.height = "400px";
			document.getElementById("tblCovReport").setAttribute("border","0");
			document.getElementById("tblCovReport").setAttribute("style","");
			//document.getElementById("btnExportToFile").style.display="inline";
			document.getElementById("btnExportToExcel").style.display="inline";
			document.getElementById("btnPrintReport").style.display="inline";
			return false;
		}
		function ExportToExcel()
		{
			
			handler = window.open('PaidClaimsByCoverageReport.aspx?PAGE_ACTION=<%=((int)enumPAGE_ACTION.EXPORT_TO_EXCEL).ToString()%>&CLAIM_ID=' + document.getElementById("hidCLAIM_ID").value,'Coverage',600,300,'Yes','Yes','No','No','No');			
			return false;
		}
		function ExportToFile()
		{
			handler = window.open('PaidClaimsByCoverageReport.aspx?PAGE_ACTION=<%=((int)enumPAGE_ACTION.EXPORT_TO_FILE).ToString()%>&CLAIM_ID=' + document.getElementById("hidCLAIM_ID").value,'Coverage',600,300,'Yes','Yes','No','No','No');	
			return false;
		}
		function Init()
		{
		
			if(document.getElementById('trCust1')!=null)
				document.getElementById('trCust1').style.display	=	'none';
			if(document.getElementById('trCust2')!=null)
				document.getElementById('trCust2').style.display	=	'none';
			//if(document.getElementById('trCust3')!=null)
			//	document.getElementById('trCust3').style.display	=	'none';	
			
			//document.getElementById("btnExportToFile").setAttribute("disabled","true");
			//document.getElementById("pnlCoverageReport").style.overflow = "auto";
			//document.getElementById("pnlCoverageReport").style.height = '100%';
			//document.getElementById("pnlCoverageReport").style.height = "400px";
		}
		function showHideCustInfo()
		{
			
		var path = document.getElementById('img').src;
		
		
		if(path.indexOf('plus2') != -1)
		{
			document.getElementById('img').src	=	'<%=pathMinus%>';
			document.getElementById('trCust1').style.display	=	'inline';
			document.getElementById('trCust2').style.display	=	'inline';
			//document.getElementById('trCust3').style.display	=	'inline';
			
			
			
		}
		else
		{
			document.getElementById('img').src	=	'<%=pathPlus%>';
			document.getElementById('trCust1').style.display	=	'none';
			document.getElementById('trCust2').style.display	=	'none';
			//document.getElementById('trCust3').style.display	=	'none';		
			
			
		}
		
		
		
			
		//document.getElementById('trCust1').style.display	=	'none';
		//document.getElementById('trCust2').style.display	=	'none';
		//document.getElementById('trCust3').style.display	=	'none';
		//document.getElementById('trCust4').style.display	=	'none';
		
			
			
		}
	
		</script>
</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Init();" >
		<!--div ID="CoverageReport" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 100%"-->
		<FORM id="ADJUSTER_SUMMARY" method="post" runat="server">
			<TABLE cellSpacing='1' cellPadding='0'align="center" class="tableWidth" border='0' style="scroll:always">				
				<tr>
					<td class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" runat="server" cssclass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<%--<tr>
					<td colspan="4">
						<asp:DataGrid ID="dgPaidClaims" Runat="server" AutoGenerateColumns="True" CssClass="midcolora" Width="100%" Visible="False">							
						</asp:DataGrid>
						<asp:Table id="tblPaidClaims" runat="server" width="100%" Border="0"></asp:Table>
					</td>
				</tr>--%>
				<tr>
					<td>
						<asp:Panel ID="pnlCoverageReport" Runat="server">					
							<%if(sbReportHandler!=null) Response.Write(sbReportHandler.ToString());%>
						</asp:Panel>
						
					</td>
				</tr>
			</TABLE>
			<table class="tableWidth" align="center" border="0" >
				<TBODY>
					<tr id="trButtonRow" runat="server" style="display:inline">
						<td class="midcolorr" vAlign="middle" align="left" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnPrintReport" runat="Server" text="Print Report"></cmsb:cmsbutton></td>
						<td class="midcolora" valign="middle" align="left" colspan="2">
							<cmsb:cmsbutton class="clsButton" id="btnExportToExcel" runat="server" Text="Export to Excel"></cmsb:cmsbutton>
							<cmsb:cmsbutton class="clsButton" id="btnExportToFile" runat="server" Text="Export to File" visible="false"></cmsb:cmsbutton>
							</td>
					</tr>		
		</TBODY></table>		
		<input type="hidden" name="hidCLAIM_ID" id="hidCLAIM_ID" runat="server">
		<input type="hidden" name="hidPAGE_ACTION" id="hidPAGE_ACTION" runat="server" value="">				
		</FORM>
		<!--/div-->
	</BODY>
</HTML>

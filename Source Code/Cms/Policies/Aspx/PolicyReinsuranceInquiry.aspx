<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PolicyReinsuranceInquiry.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.PolicyReinsuranceInquiry" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS - Reinsurance Inquiry</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
		
		<script language="javascript">
		function openBreakDownDetails(customerID,PolicyID,PolVersionID,ProcessID,transactioNO)
		{
		window.open("/cms/cmsweb/Maintenance/Reinsurance/ReinsuranceBreakdown.aspx?CUSTOMER_ID="+customerID+"&POLICY_ID="+PolicyID + "&POL_VERSION_ID="+PolVersionID +"&PROCESSID="+ProcessID + "&TRAN_SN="+ transactioNO,'ReinsuranceBreakdown',"width=1000,height=700,screenX=50,screenY=150,top=10,left=10,scrollbars=yes,resizable=yes,menubar=no,toolbar=no,status=no","");	
		return false;
		}
		
		//Added by Sibin on 03-10-08 to use the print button
		function PrintReport()
		{
		
			document.getElementById("btnPrintReport").style.display="none";			
			window.print();
			document.getElementById("btnPrintReport").style.display="inline";
			return false;
		}
		
		</script>
	</HEAD>
	<body oncontextmenu="return false;" MS_POSITIONING="GridLayout" >
			<form id="ReinsuranceInquiry" method="post" runat="server">
			<div id="bodyHeight" class="pageContent">
				<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td align="center">
							<asp:Label ID="capMessage" Runat="server" Visible="True" CssClass="errmsg"></asp:Label>
						</td>
					</tr>
					<tr id="formTable" runat="server">
						<td>
							<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
								<!--tr>
									<td id="tdWorkflow" class="pageHeader" colspan="4">
										<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
									</td>
								</tr-->
								<asp:Label ID="lblTemplate" Runat="server" Visible="True"></asp:Label>
								<!--
								<tr>
									<td>
										<webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
									</td>
								</tr>
								-->
							  <tr>
							  <td><asp:Label ID="lblReinsuranceDetals" Runat="server" Visible="True"></asp:Label></td>
							  </tr>	
							  
							  <!--Added by Sibin on 3-10-08 to add the print buttton-->
							  <tr >
								<td class="midcolorr" vAlign="middle" align="left" colSpan="2">
								<cmsb:cmsbutton class="clsButton" id="btnPrintReport" runat="Server" text="Print Report"></cmsb:cmsbutton></td>
							</tr>	
							</table>
						</td>
					</tr>
				</table>
				<input type="hidden" name="hidTabNumber" runat="server" id="hidTabNumber">
			</div>
		</form>
	</body>
</HTML>

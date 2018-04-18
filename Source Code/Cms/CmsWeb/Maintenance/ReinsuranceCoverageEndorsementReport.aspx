<%@ Page language="c#" Codebehind="ReinsuranceCoverageEndorsementReport.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.ReinsuranceCoverageEndorsementReport" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Reinsurance Coverage/Endorsement Report </title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Page-Enter" content="revealtrans(duration=0.0)">
		<meta http-equiv="Page-Exit" content="revealtrans(duration=0.0)">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">			
		
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" onload="setfirstTime();ApplyColor();ChangeColor();top.topframe.main1.mousein = false;showScroll();findMouseIn();">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<div class="pageContent" id="bodyHeight" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 100%">
			<form id="ReinsuranceCoverageEndorsementReport" method="post" runat="server">
				<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
					<TBODY>
					
						<tr>
							<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
						</tr>
						
						<tr>
							<%--<td class="pageheader" align="center" colSpan="4">Please select Display Report Button 
								to view Reinsurance Coverage/Endorsement Report</td>--%>
								<td class="pageheader" align="center" colSpan="4"><asp:label ID="lblmessage" Text="Please select Display Report Button 
								to view Reinsurance Coverage/Endorsement Report" runat="server"></asp:label></td>
						</tr>
						
						<tr>
							<%--<td class="headereffectcenter" colSpan="4">Reinsurance Coverage/Endorsement Report</td>--%>
							<td class="headereffectcenter" colSpan="4"><asp:Label ID="lblheading" runat="server" Text="Reinsurance Coverage/Endorsement Report"></asp:Label></td>
						</tr>				
					    <tr>
							<td class="midcolora" width="18%">
								<asp:label id="lblState" runat="server">Select Country</asp:label></TD>
							<td class="midcolora" width="32%">
								<asp:ListBox id="lstStateName" runat="server" Height="100px" Width="240px" SelectionMode="Multiple"></asp:ListBox></td>
												
							<td class="midcolora" width="18%">
								<asp:label id="lblLOB" runat="server">Product</asp:label></td>
							<td class="midcolora" width="32%">
								<asp:ListBox id="lstLOB" runat="server" Height="100px" Width="240px" SelectionMode="Multiple"></asp:ListBox></td>						
						</tr>
						
						<tr>
							<td class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnReport" runat="server" Text="Display Report"></cmsb:cmsbutton></td>
							<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnExcelReport" runat="server" Text="Export to Excel"></cmsb:cmsbutton></td>
						</tr>
						
						<tr>
							<td><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></td>
						</tr>
						
						<tr>
							<td align="center" colSpan="4"><asp:label id="lblDatagrid" Visible="False" Runat="server" CssClass="errmsg"></asp:label></td>
						</tr>
						
						<tbody id="tbDataGrid" runat="server">
							<tr>
								<td class="midcolora" colSpan="4">
								<asp:datagrid id="dgVenPendInv" Runat="server" Width="100%" ItemStyle-CssClass="datarow" HeaderStyle-CssClass="headereffectCenter"
										AllowPaging="True" PageSize="15" PagerStyle-Mode="NextPrev" PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="FALSE"
										PagerStyle-CssClass="datarow" OnItemDataBound="dgVenPendInv_DataBound" PagerStyle-NextPageText="Next >>" PagerStyle-PrevPageText="<< Prev" AlternatingItemStyle-CssClass="alternatedatarow"
										OnPageIndexChanged="dgVenPendInv_Paging">
										<COLUMNS>
											<ASP:BOUNDCOLUMN HeaderText="Coverage Desc" DataField="COV_DES"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="State Code"  ItemStyle-HorizontalAlign="CENTER" DataField="STATE_CODE"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Product Code"   DataField="LOB_CODE"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Display on Reinsurance Product" ItemStyle-HorizontalAlign="center" DataField="REINSURANCE_LOB"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Reinsurance Coverage Category" DataField="REINSURANCE_COV"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="ASLOB" DataField="ASLOB"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Calculation" DataField="REINSURANCE_CALC"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Always Display on Reinsurance Inquiry" DataField="Always_Display" ItemStyle-HorizontalAlign="center"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Start Date" DataField="EFFECTIVE_FROM_DATE"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="End Date" DataField="EFFECTIVE_TO_DATE"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Reinsurance Report Buckets" DataField="REIN_REPORT_BUCK"></ASP:BOUNDCOLUMN>
											
										</COLUMNS>
									</asp:datagrid></td>
							</tr>
						</tbody>
				</table>
				
			</form>
		</div>
	</body>
</HTML>

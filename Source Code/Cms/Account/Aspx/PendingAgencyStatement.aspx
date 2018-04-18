<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PendingAgencyStatement.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.PendingAgencyStatement" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Pending Agency Statements</title>
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
		
		function OpenAgencyLookup()
		{
			var url='<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>';
			var idField,valueField,lookUpTagName,lookUpTitle;
	
			idField			=	'AGENCY_ID';
			valueField		=	'Name';
			lookUpTagName	=	'Agency';
			lookUpTitle		=	"Agency Names";
			
			OpenLookup( url,idField,valueField,"hidAGENCY_ID","txtAGENCY_NAME",lookUpTagName,lookUpTitle,'');
		}
		
		//Formats the amount and convert 111 into 1.11
		function FormatAmount(txtAmount)
		{
						
			if (txtAmount.value != "")
			{
				amt = txtAmount.value;
				
				amt = ReplaceAll(amt,".","");
				
				if (amt.length == 1)
					amt = amt + "0";
				
				if ( ! isNaN(amt))
				{
					
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					txtAmount.value = InsertDecimal(amt);
				}
			}
		}		
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" onload="setfirstTime();ApplyColor();ChangeColor();top.topframe.main1.mousein = false;showScroll();findMouseIn();">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<form id="PendingAgencyStatement" method="post" runat="server">
			<div class="pageContent" id="bodyHeight">
				<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
					<TBODY>
						<tr>
							<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
						</tr>
						<tr>
							<td class="pageheader" align="center" colSpan="3">Please select Run Report Button 
								to view Pending Agency Statements</td>
						</tr>
						<tr>
							<td class="headereffectcenter" colSpan="3">Pending Agency Statements</td>
						</tr>
						<tr>
							<td class="midcolora" width="18%">Agency</td>
							<td class="midcolora" width="32%" colSpan="2"><asp:textbox id="txtAGENCY_NAME" runat="server" ReadOnly="False" size="30"></asp:textbox><IMG id="imgAGENCY_NAME" style="CURSOR: hand" onclick="OpenAgencyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
									runat="server"><br>
								<asp:requiredfieldvalidator id="rfvAGENCY_NAME" runat="server" Visible="False" ControlToValidate="txtAGENCY_NAME"
									ErrorMessage="Please select agency." Display="Dynamic"></asp:requiredfieldvalidator></td>
						</tr>
						<tr>
							<td class="midcolora" width="18%">Month/Year</td>
							<td class="midcolora" width="32%" colSpan="2"><asp:dropdownlist id="cmbMonth" Runat="server">
									<asp:ListItem Selected="True" Value=""></asp:ListItem>
									<asp:ListItem Value="1">Jan</asp:ListItem>
									<asp:ListItem Value="2">Feb</asp:ListItem>
									<asp:ListItem Value="3">Mar</asp:ListItem>
									<asp:ListItem Value="4">Apr</asp:ListItem>
									<asp:ListItem Value="5">May</asp:ListItem>
									<asp:ListItem Value="6">Jun</asp:ListItem>
									<asp:ListItem Value="7">Jul</asp:ListItem>
									<asp:ListItem Value="8">Aug</asp:ListItem>
									<asp:ListItem Value="9">Sep</asp:ListItem>
									<asp:ListItem Value="10">Oct</asp:ListItem>
									<asp:ListItem Value="11">Nov</asp:ListItem>
									<asp:ListItem Value="12">Dec</asp:ListItem>
								</asp:dropdownlist><asp:textbox id="txtYear" runat="server" size="4" MaxLength="4"></asp:textbox><br>
								<asp:regularexpressionvalidator id="revYear" runat="server" ControlToValidate="txtYear" ErrorMessage="Please enter valid Year only."
									Display="Dynamic"></asp:regularexpressionvalidator><asp:rangevalidator id="rnvYear" ControlToValidate="txtYear" ErrorMessage="Year should be greater than 1950 and less than 2099."
									Display="Dynamic" Runat="Server" Type="Integer" MaximumValue="2099" MinimumValue="1950">
									</asp:rangevalidator></td>
						</tr>
						<TR>
							<TD class="midcolora" style="HEIGHT: 20px" width="50%">Commission Type <span class="mandatory">
							*</span></TD>
								<TD class="midcolora" style="HEIGHT: 20px" width="50%"><asp:dropdownlist id="CmbCommType" Runat="server" Width="216px">
								<asp:ListItem Value="REG">Regular Commission</asp:ListItem>
								<asp:ListItem Value="ADC">Additional Commission</asp:ListItem>
								<asp:ListItem Value="CAC">Complete App Commission</asp:ListItem>
								<asp:ListItem Value="OP">Refund Agency Over Payment</asp:ListItem>
							</asp:dropdownlist></TD>
						</TR>
						<tr>
							<td class="midcolora" width="18%" colSpan="4"><cmsb:cmsbutton class="clsButton" id="btnReport" runat="server" Text="Run Report"></cmsb:cmsbutton></td>
						</tr>
						<tr>
							<td><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></td>
						</tr>
						<tr>
							<td align="center" colSpan="4"><asp:label id="lblDatagrid" Visible="False" Runat="server" CssClass="errmsg"></asp:label></td>
						</tr>
						<tbody id="tbDataGrid" runat="server">
							<tr>
								<td class="midcolora" colSpan="4"><asp:datagrid id="dgVenPendInv" Runat="server" Width="100%" ItemStyle-CssClass="datarow" HeaderStyle-CssClass="headereffectCenter"
										AllowPaging="True" PageSize="15" PagerStyle-Mode="NextPrev" PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="FALSE"
										PagerStyle-CssClass="datarow" PagerStyle-NextPageText="Next >>" PagerStyle-PrevPageText="<< Prev" AlternatingItemStyle-CssClass="alternatedatarow"
										OnPageIndexChanged="dgVenPendInv_Paging">
										<COLUMNS>
											<ASP:BOUNDCOLUMN HeaderText="Agency Name" DataField="AGENCY_DISPLAY_NAME"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Agency Code" ItemStyle-HorizontalAlign="Center" DataField="AGENCY_CODE"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Month Year" ItemStyle-HorizontalAlign="Center" DataField="MONTHYEAR"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Stmt Amount" ItemStyle-HorizontalAlign="Right" DataField="STMT_AMOUNT"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Bal Amount" DataField="BALANCE_AMOUNT" ItemStyle-HorizontalAlign="Right"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Comm Amount (AB)" DataField="COMMISSION_AMOUNT_AB" ItemStyle-HorizontalAlign="Right"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Comm Amount (DB)" DataField="COMMISSION_AMOUNT_DB" ItemStyle-HorizontalAlign="Right"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Prem Amount (AB)" DataField="PREMIUM_AMOUNT_AB" ItemStyle-HorizontalAlign="Right"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Type" DataField="TYPE" ItemStyle-HorizontalAlign="Right"></ASP:BOUNDCOLUMN>
										</COLUMNS>
									</asp:datagrid></td>
							</tr>
						</tbody>
				</table>
				<input id="hidAGENCY_ID" type="hidden" value="0" name="hidAGENCY_ID" runat="server">
			</div>
		</form>
	</body>
</HTML>

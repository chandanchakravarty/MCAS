<%@ Page language="c#" Codebehind="AccountPosting.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AccountPosting" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AccountPosting</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
	
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
			
			/*
				function AccountDetails()
				{
					var frmSource=document.getElementById('txtFrom').value;
					var toSource=document.getElementById('txtTo').value;
					var frmDate=document.getElementById('txtDateFrom').value;
					var toDate=document.getElementById('txtDateTo').value;
					
					var acc=document.getElementById('txtAccount').value;
					var updFrom=document.getElementById('cmbUpdatedFrom').options[document.getElementById('cmbUpdatedFrom').selectedIndex].value;
					var lob=document.getElementById('cmbLob').options[document.getElementById('cmbLob').selectedIndex].value;
					var state=document.getElementById('cmbState').options[document.getElementById('cmbState').selectedIndex].value;
										
				 window.open("AccountDetails.aspx?frmSource=" + frmSource + "&toSource=" +toSource+"&frmDate="+frmDate+"&toDate="+toDate+"&acc="+acc+"&updFrom="+updFrom+"&lob="+lob+"&state="+state+"");	
				}
				*/
				
	function ChkDateFrom(objSource , objArgs)
	{
		var frmdate=document.AccountPosting.txtDateFrom.value;
		objArgs.IsValid = DateComparer("<%=DateTime.Now%>", frmdate, jsaAppDtFormat);
		return false;
	}
	function ChkDateTo(objSource , objArgs)
	{
		var todate=document.AccountPosting.txtDateTo.value;
		objArgs.IsValid = DateComparer("<%=DateTime.Now%>", todate, jsaAppDtFormat);
		
		
	}
	/*
	function ChkDateCheck(objSource , objArgs)
	{
		var frmdate=document.AccountPosting.txtDateFrom.value;
		var todate=document.AccountPosting.txtDateTo.value;
		objArgs.IsValid = DateComparer(frmdate, todate, jsaAppDtFormat);
		return false;
	}*/		
		</script>
	</HEAD>
	<body onload="setfirstTime();ApplyColor();ChangeColor();top.topframe.main1.mousein = false;showScroll();findMouseIn();"
		MS_POSITIONING="GridLayout">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<div class="pageContent" id="bodyHeight">
			<form id="AccountPosting" method="post" runat="server">
				<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
					<tr>
						<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
					</tr>
					<tr>
						<td class="pageheader" align="center" colSpan="3">Please select Run Report Button 
							to view General Ledger Account Detail</td>
					</tr>
					<tr>
						<td class="headereffectcenter" colSpan="3">General Ledger Account Detail</td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">Account<span class="mandatory">*</span></td>
						<td class="midcolora" width="32%" colspan="2"><asp:textbox id="txtAccount" Runat="server" MaxLength="15"></asp:textbox><br>
							<asp:RequiredFieldValidator ID="rfvAccount" Runat="server" ControlToValidate="txtAccount" Display="Dynamic"></asp:RequiredFieldValidator>
							<asp:regularexpressionvalidator id="revAccount" Runat="server" ControlToValidate="txtAccount" Display="Dynamic"></asp:regularexpressionvalidator></td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">Updated From</td>
						<td class="midcolora" width="32%" colspan="2"><asp:dropdownlist id="cmbUpdatedFrom" Runat="server">
								<asp:ListItem></asp:ListItem>
								<asp:ListItem Value="P">Premium</asp:ListItem>
								<asp:ListItem Value="C">Checks</asp:ListItem>
								<asp:ListItem Value="D">Deposit</asp:ListItem>
								<asp:ListItem Value="J">Journal</asp:ListItem>
								<asp:ListItem Value="I">Invoice</asp:ListItem>
							</asp:dropdownlist></td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">Line Of Business</td>
						<td class="midcolora" width="32%" colspan="2"><asp:dropdownlist id="cmbLob" Runat="server"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">State</td>
						<td class="midcolora" width="32%" colspan="2"><asp:DropDownList ID="cmbState" Runat="server"></asp:DropDownList></td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">Source No</td>
						<td class="midcolora" width="32%">From<asp:textbox id="txtFrom" Runat="server"></asp:textbox><br>
							<asp:regularexpressionvalidator id="revFrom" Runat="server" ControlToValidate="txtFrom" Display="Dynamic"></asp:regularexpressionvalidator></td>
						<td class="midcolora" width="33%">To<asp:textbox id="txtTo" Runat="server"></asp:textbox><br>
							<asp:regularexpressionvalidator id="revTo" Runat="server" ControlToValidate="txtTo" Display="Dynamic"></asp:regularexpressionvalidator></td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">Transaction Date</td>
						<td class="midcolora" width="32%">From<asp:textbox id="txtDateFrom" Runat="server"></asp:textbox>
							<asp:hyperlink id="hlkDateFrom" runat="server" CssClass="HotSpot">
								<asp:image id="imgDateFrom" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
							</asp:hyperlink><br>
							<asp:customvalidator id="csvDateFrom" Runat="server" ControlToValidate="txtDateFrom" Display="Dynamic"
								ClientValidationFunction="ChkDateFrom"></asp:customvalidator><asp:regularexpressionvalidator id="revDateFrom" Runat="server" ControlToValidate="txtDateFrom" Display="Dynamic"></asp:regularexpressionvalidator></td>
						<td class="midcolora" width="18%">&nbsp;&nbsp;&nbsp;<asp:textbox id="txtDateTo" Runat="server"></asp:textbox>
							<asp:hyperlink id="hlkDateTo" runat="server" CssClass="HotSpot">
								<asp:image id="imgDateTo" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
							</asp:hyperlink><br>
							<asp:customvalidator id="csvDateTo" Runat="server" ControlToValidate="txtDateTo" Display="Dynamic" ClientValidationFunction="ChkDateTo"></asp:customvalidator>
							<asp:regularexpressionvalidator id="revToDate" Runat="server" ControlToValidate="txtDateTo" Display="Dynamic"></asp:regularexpressionvalidator></td>
					</tr>
					<tr>
						<td class="midcolora" colspan="3">
							<cmsb:cmsbutton class="clsButton" id="btnReport" runat="server" Text='Run Report'></cmsb:cmsbutton>
						</td>
					</tr>
				</table>
			</form>
		</div>
	</body>
</HTML>

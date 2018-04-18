<%@ Page language="c#" Codebehind="EFTSweepHistory.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.EFTSweepHistory" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>EFTSweepHistory</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
				
	function ChkDateFromSpool(objSource , objArgs)
	{
		var frmdate=document.EFTSweepHistory.txtDateFromSpool.value;
		objArgs.IsValid = DateComparer("<%=DateTime.Now%>", frmdate, jsaAppDtFormat);
		return false;
	}
	function ChkDateToSpool(objSource , objArgs)
	{
		var todate=document.EFTSweepHistory.txtDateToSpool.value;
		objArgs.IsValid = DateComparer("<%=DateTime.Now%>", todate, jsaAppDtFormat);
		
		
	}
	
	function ChkDateFromSweep(objSource , objArgs)
	{
		var frmdate=document.EFTSweepHistory.txtDateFromSweep.value;
		objArgs.IsValid = DateComparer("<%=DateTime.Now%>", frmdate, jsaAppDtFormat);
		return false;
	}
	function ChkDateToSweep(objSource , objArgs)
	{
		var todate=document.EFTSweepHistory.txtDateToSweep.value;
		objArgs.IsValid = DateComparer("<%=DateTime.Now%>", todate, jsaAppDtFormat);
		
		
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
	<body oncontextmenu = "return false;" onload="setfirstTime();ApplyColor();ChangeColor();top.topframe.main1.mousein = false;showScroll();findMouseIn();"
		MS_POSITIONING="GridLayout">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<div class="pageContent" id="bodyHeight">
			<form id="EFTSweepHistory" method="post" runat="server">
				<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
					<tr>
						<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
					</tr>
					<tr>
						<td class="pageheader" align="center" colSpan="3">Please select Run Report Button 
							to view EFT Sweep History Detail</td>
					</tr>
					<tr>
						<td class="headereffectcenter" colSpan="4">EFT Sweep History</td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">Spool Date From</td>
						<td class="midcolora" width="32%"><asp:textbox id="txtDateFromSpool" MaxLength="10" size="12" Runat="server"></asp:textbox><asp:hyperlink id="hlkDateFromSpool" runat="server" CssClass="HotSpot">
								<asp:image id="imgDateFromSpool" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
							</asp:hyperlink><br>
							<asp:customvalidator id="csvDateFromSpool" Runat="server" Display="Dynamic" ControlToValidate="txtDateFromSpool"
								ClientValidationFunction="ChkDateFromSpool"></asp:customvalidator>
							<asp:regularexpressionvalidator id="revDateFromSpool" Runat="server" Display="Dynamic" ControlToValidate="txtDateFromSpool"></asp:regularexpressionvalidator>
							<asp:CompareValidator id="cmpSpoolDate" Runat="server" Display="Dynamic" ErrorMessage="End Date can't be less than Start Date."
								ControlToValidate="txtDateToSpool" ControlToCompare="txtDateFromSpool" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator></td>
						<td class="midcolora" width="18%">Spool Date To</td>
						<td class="midcolora" width="32%"><asp:textbox id="txtDateToSpool" MaxLength="10" size="12" Runat="server"></asp:textbox><asp:hyperlink id="hlkDateToSpool" runat="server" CssClass="HotSpot">
								<asp:image id="imgDateToSpool" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
							</asp:hyperlink><br>
							<asp:customvalidator id="csvDateToSpool" Runat="server" Display="Dynamic" ControlToValidate="txtDateToSpool"
								ClientValidationFunction="ChkDateToSpool"></asp:customvalidator>
							<asp:regularexpressionvalidator id="revToDateSpool" Runat="server" Display="Dynamic" ControlToValidate="txtDateToSpool"></asp:regularexpressionvalidator></td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">Sweep Date From</td>
						<td class="midcolora" width="32%"><asp:textbox id="txtDateFromSweep" MaxLength="10" size="12" Runat="server"></asp:textbox><asp:hyperlink id="hlkDateFromSweep" runat="server" CssClass="HotSpot">
								<asp:image id="imgDateFromSweep" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
							</asp:hyperlink><br>
							<asp:customvalidator id="csvDateFromSweep" Runat="server" ControlToValidate="txtDateFromSweep" Display="Dynamic"
								ClientValidationFunction="ChkDateFromSweep"></asp:customvalidator>
							<asp:regularexpressionvalidator id="revDateFromSweep" Runat="server" ControlToValidate="txtDateFromSweep" Display="Dynamic"></asp:regularexpressionvalidator>
							<asp:CompareValidator id="cmpSweepDate" Runat="server" Display="Dynamic" ErrorMessage="End Date can't be less than Start Date."
								ControlToValidate="txtDateToSweep" ControlToCompare="txtDateFromSweep" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator></td>
						<td class="midcolora" width="18%">Sweep Date To</td>
						<td class="midcolora" width="32%"><asp:textbox id="txtDateToSweep" MaxLength="10" size="12" Runat="server"></asp:textbox><asp:hyperlink id="hlkDateToSweep" runat="server" CssClass="HotSpot">
								<asp:image id="imgDateToSweep" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
							</asp:hyperlink><br>
							<asp:customvalidator id="csvDateToSweep" Runat="server" ControlToValidate="txtDateToSweep" Display="Dynamic"
								ClientValidationFunction="ChkDateToSweep"></asp:customvalidator>
							<asp:regularexpressionvalidator id="revToDateSweep" Runat="server" ControlToValidate="txtDateToSweep" Display="Dynamic"></asp:regularexpressionvalidator></td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">Entity Type</td>
						<td class="midcolora" colspan="1"><asp:DropDownList ID="cmbEntityType" Runat="server"></asp:DropDownList></td>
						<td class="midcolora" width="18%">Amount</td>
						<td class="midcolora" width="18%"><asp:textbox id="txtTransactionAmount" CssClass="INPUTCURRENCY" MaxLength="10" size="12" Runat="server" onblur="FormatAmount(this);"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revTransactionAmount" Runat="server" Display="Dynamic" ControlToValidate="txtTransactionAmount" ErrorMessage="Please enter Valid Amount."></asp:regularexpressionvalidator></td>
					</tr>
					<tr>
						<td class="midcolora" colspan="4">
							<cmsb:cmsbutton class="clsButton" id="btnReport" runat="server" Text='Run Report'></cmsb:cmsbutton>
						</td>
					</tr>
				</table>
			</form>
		</div>
	</body>
</HTML>


















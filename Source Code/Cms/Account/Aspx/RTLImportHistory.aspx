<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="RTLImportHistory.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.RTLImportHistory" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RTLImportHistory</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script>
    
		<script language="javascript" type="text/javascript">	
		
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";

			$(document).ready(function() {

			    document.getElementById('txtDepositNumber').focus();
			   
			    $("#btnReport").click(function() {

			        var DateFrom = $("#txtDateFrom").val();
			        var DateTo = $("#txtDateTo").val();
			        var DepositNumber = $("#txtDepositNumber").val();
			        var StatusListValue = $("#StatusList option:selected").val();
			        var DepositType = $("#cmbDEPOSIT_TYPE option:selected").val();

			        var str = "/cms/Account/Aspx/RTLImportHistoryDetails.aspx?DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&DepositNumber=" + DepositNumber +
                 "&ProcessStatus=" + StatusListValue + "&DepositType=" + DepositType;
			        window.open(str, "ImportHistoryDetails", "resizable=yes,toolbar=0,location=0, directories=0, status=0, menubar=0,scrollbars=yes,width=800,height=500,left=150,top=50")
			        return false;
			    });
			});
			
		function ChkDateFrom(objSource , objArgs)
		{
			var frmdate=document.RTLImportHistory.txtDateFrom.value;
			objArgs.IsValid = DateComparer("<%=DateTime.Now%>", frmdate, jsaAppDtFormat);
			return false;
		}
		function ChkDateTo(objSource , objArgs)
		{
			var todate=document.RTLImportHistory.txtDateTo.value;
			objArgs.IsValid = DateComparer("<%=DateTime.Now%>", todate, jsaAppDtFormat);
		}
		 
		
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" onload="setfirstTime();ApplyColor();ChangeColor();top.topframe.main1.mousein = false;showScroll();findMouseIn();" MS_POSITIONING="GridLayout">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<div class="pageContent" id="bodyHeight">
			<form id="RTLImportHistory" method="post" runat="server">
				<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
					<tr>
						<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
					</tr>
					<tr>
						<td class="pageheader" align="center" colSpan="4"><asp:Label ID="capMessage" runat="server"></asp:Label></td><%--Please select Run Report Button 
							to view RTL Import History Detail--%>
					</tr>
					<tr>
						<td class="headereffectcenter" colSpan="4"><asp:Label ID="capHeader" runat="server"></asp:Label></td><%--RTL Import History--%>
					</tr>
					<tr>
						<td class="midcolora" width="18%"><asp:Label ID="capDept_Number" runat="server"></asp:Label></td><%--Deposit Number--%>
						<TD class="midcolora" colSpan="1"><asp:textbox style="Text-Align:Right" id="txtDepositNumber"  MaxLength="10" size="12" Runat="server"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revDepositNumber" Runat="server" Display="Dynamic" ControlToValidate="txtDepositNumber"></asp:regularexpressionvalidator></TD>
						<td class="midcolora" width="18%"></td>
						<td class="midcolora" width="18%"></td>
					</tr>
					<tr>
						<td class="midcolora" width="18%"><asp:Label ID="capFrom_Date" runat="server"></asp:Label></td><%--From Date--%>
						<td class="midcolora" width="32%"><asp:textbox id="txtDateFrom" MaxLength="10" size="12" Runat="server"></asp:textbox><asp:hyperlink id="hlkDateFrom" runat="server" CssClass="HotSpot">
								<asp:image id="imgDateFrom" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
							</asp:hyperlink><br>
							<asp:customvalidator id="csvDateFrom" Runat="server" Display="Dynamic" ControlToValidate="txtDateFrom"
								ClientValidationFunction="ChkDateFrom"></asp:customvalidator>
							<asp:regularexpressionvalidator id="revDateFrom" Runat="server" Display="Dynamic" ControlToValidate="txtDateFrom"></asp:regularexpressionvalidator></td>
						<td class="midcolora" width="18%"><asp:Label ID="capTo_Date" runat="server"></asp:Label></td><%--To Date--%>
						<td class="midcolora" width="32%"><asp:textbox id="txtDateTo" MaxLength="10" size="12" Runat="server"></asp:textbox><asp:hyperlink id="hlkDateTo" runat="server" CssClass="HotSpot">
								<asp:image id="imgDateTo" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
							</asp:hyperlink><br>
							<asp:customvalidator id="csvDateTo" Runat="server" Display="Dynamic" ControlToValidate="txtDateTo" ClientValidationFunction="ChkDateTo"></asp:customvalidator>
							<asp:regularexpressionvalidator id="revToDate" Runat="server" Display="Dynamic" ControlToValidate="txtDateTo"></asp:regularexpressionvalidator>
							<asp:CompareValidator id="cmpDate" Runat="server" Display="Dynamic" ErrorMessage="To Date can't be less than From Date."
								ControlToValidate="txtDateTo" ControlToCompare="txtDateFrom" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator></td>
					</tr>
					<tr>
					<td class="midcolora" width="18%"><asp:Label ID="capStatus" runat="server"></asp:Label></td><%--Status--%>
					<TD class="midcolora" colSpan="1"><asp:dropdownlist id="StatusList" runat="server">
					<asp:ListItem Value=""></asp:ListItem>
					<%--<asp:ListItem Value="0">Done</asp:ListItem>
					<asp:ListItem Value="120">Failed</asp:ListItem>--%>
					</asp:dropdownlist></TD>
					<TD class="midcolora" width="18%"><asp:label id="capDEPOSIT_TYPE" runat="server">Deposit Type</asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%" colspan=3><asp:dropdownlist id="cmbDEPOSIT_TYPE" runat="server" >
					<asp:ListItem Value='0'></asp:ListItem></asp:dropdownlist><BR></TD>
					<%--<td class="midcolora" width="18%"></td>
					<td class="midcolora" width="18%"></td>--%>
					</tr>
					<tr>
						<td class="midcolora" colspan="4">
							<cmsb:cmsbutton class="clsButton" id="btnReport" runat="server" Text='Run Report'></cmsb:cmsbutton>
						</td>
					</tr>
				</table>
				    <input id="hidDEPOSIT_TYPE" type="hidden" name="hidDEPOSIT_TYPE" runat="server"/>  
			</form>
		</div>
	</body>
</HTML>

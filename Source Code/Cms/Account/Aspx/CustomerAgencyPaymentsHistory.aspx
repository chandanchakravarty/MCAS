<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="CustomerAgencyPaymentsHistory.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.CustomerAgencyPaymentsHistory" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CustomerAgencyPaymentsHistory</title>
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
	var url='<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>';
	//Function change For Itrack Issue #5942.
	function OpenCustomerLookup()
	{
		 var systemID = '<%=strSystemID%>';	     				
		 var agencyID  = '<%=strAgencyID%>'; 
		 
		 //Changed by Charles on 19-May-10 for Itrack 51				
		if(systemID.toUpperCase() == '<%= Cms.CmsWeb.cmsbase.CarrierSystemID.ToUpper() %>')		
		{ 		
		    OpenLookupWithFunction(url,'CUSTOMER_ID','Name','hidCustomer_ID','txtCUSTOMER_NAME','CustLookupForm','Customer','','');
		}
		else
		{		
		    OpenLookupWithFunction(url,'CUSTOMER_ID','Name','hidCustomer_ID','txtCUSTOMER_NAME','CustLookupFormAgency','Customer','@systemID=\''+systemID + '\'','','');
		}
	}

	function OpenAgencyLookup()
	{		

		OpenLookupWithFunction( url,"AGENCY_ID","Name","hidAGENCY_ID","txtAGENCY","Agency","Agency Names",'','');			
	}
	//Function change For Itrack Issue #5942.
	function OpenPolicyLookup()
	{  
	    var systemID = '<%=strSystemID%>';	     				
		var agencyID  = '<%=strAgencyID%>';		
		
		//Changed by Charles on 19-May-10 for Itrack 51
		if (systemID.toUpperCase() == '<%= Cms.CmsWeb.cmsbase.CarrierSystemID.ToUpper() %>')		
		{ 
		  OpenLookupWithFunction(url,'POL_INFORM','POLICY_NUMBER','hidPOLICYINFO','txtPolicyNo','ARREPORT','Policy','','');		
		}
		else
		{
		 OpenLookupWithFunction(url,'POL_INFORM','POLICY_NUMBER','hidPOLICYINFO','txtPolicyNo','ARREPORTAGENCY','Policy','@systemID=\''+ systemID + '\'','','');          
		}
	}
	
	function ChkDateFrom(objSource , objArgs)
	{
		var frmdate=document.CustomerAgencyPaymentsHistory.txtDateFrom.value;
		objArgs.IsValid = DateComparer("<%=DateTime.Now%>", frmdate, jsaAppDtFormat);
		return false;
	}
	function ChkDateTo(objSource , objArgs)
	{
		var todate=document.CustomerAgencyPaymentsHistory.txtDateTo.value;
		objArgs.IsValid = DateComparer("<%=DateTime.Now%>", todate, jsaAppDtFormat);		
	}
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" onload="setfirstTime();ApplyColor();ChangeColor();top.topframe.main1.mousein = false;showScroll();findMouseIn();"
		MS_POSITIONING="GridLayout">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<div class="pageContent" id="bodyHeight">
			<form id="CustomerAgencyPaymentsHistory" method="post" runat="server">
				<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
					<tr>
						<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
					</tr>
					<tr>
						<td class="pageheader" align="center" colSpan="4">Please select Run Report Button 
							to view Customer Agency Payments History Detail</td>
					</tr>
					<tr>
						<td class="headereffectcenter" colSpan="4">Customer Agency Payments History</td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">Policy Number</td>
						<td class="midcolora" width="18%" colspan="3"><asp:textbox id="txtPolicyNo" Runat="server" MaxLength="8" size="10"></asp:textbox>
							<span id="spnPOLICY_NO" runat="server"><IMG id="imgPOLICY_NO" style="CURSOR: hand" onclick="OpenPolicyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
									runat="server"></span></td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">From Date</td>
						<td class="midcolora" width="32%"><asp:textbox id="txtDateFrom" MaxLength="10" size="12" Runat="server"></asp:textbox><asp:hyperlink id="hlkDateFrom" runat="server" CssClass="HotSpot">
								<asp:image id="imgDateFrom" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
							</asp:hyperlink><br>
							<asp:customvalidator id="csvDateFrom" Runat="server" Display="Dynamic" ControlToValidate="txtDateFrom"
								ClientValidationFunction="ChkDateFrom"></asp:customvalidator>
							<asp:regularexpressionvalidator id="revDateFrom" Runat="server" Display="Dynamic" ControlToValidate="txtDateFrom"></asp:regularexpressionvalidator>
							</td>
						<td class="midcolora" width="18%">To Date</td>
						<td class="midcolora" width="32%"><asp:textbox id="txtDateTo" MaxLength="10" size="12" Runat="server"></asp:textbox><asp:hyperlink id="hlkDateTo" runat="server" CssClass="HotSpot">
								<asp:image id="imgDateTo" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
							</asp:hyperlink><br>
							<asp:customvalidator id="csvDateTo" Runat="server" Display="Dynamic" ControlToValidate="txtDateTo" ClientValidationFunction="ChkDateTo"></asp:customvalidator>
							<asp:regularexpressionvalidator id="revDateTo" Runat="server" Display="Dynamic" ControlToValidate="txtDateTo"></asp:regularexpressionvalidator>
							<asp:CompareValidator id="cmpSpoolDate" Runat="server" Display="Dynamic" ErrorMessage="To Date can't be less than From Date."
								ControlToValidate="txtDateTo" ControlToCompare="txtDateFrom" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator></td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">Agency</td>
						<td class="midcolora" width="32%"><asp:textbox id="txtAGENCY" size="50" runat="server" readonly = "true"></asp:textbox>
							<IMG id="imgAGENCY" style="CURSOR: hand" src="/cms/cmsweb/images/selecticon.gif" onclick="OpenAgencyLookup()" runat="server"></td>
						<td class="midcolora" width="18%">Customer</td>
						<TD class="midcolora" colSpan="1"><asp:textbox id="txtCUSTOMER_NAME" size="50" runat="server" readonly = "true"></asp:textbox>
							<IMG id="imgCustomer" style="CURSOR: hand" onclick="OpenCustomerLookup()" src="/cms/cmsweb/images/selecticon.gif"
								runat="server"></TD>
					</tr>
					<tr>
						<td class="midcolora" width="18%">Amount</td>
						<td class="midcolora" width="18%" colspan="3"><asp:textbox id="txtAMOUNT" runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="midcolora" colspan="4">
							<cmsb:cmsbutton class="clsButton" id="btnReport" runat="server" Text='Run Report'></cmsb:cmsbutton>
						</td>
					</tr>
				</table>
				<input id="hidCustomer_ID" type="hidden" name="hidCustomer_ID" runat="server">
				<input id="hidAGENCY_ID" type="hidden" name="hidAGENCY_ID" runat="server">
				<input id="hidPOLICYINFO" type="hidden" name="hidPOLICYINFO" runat="server">							
			</form>
		</div>
	</body>
</HTML>

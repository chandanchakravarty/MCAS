<%@ Page language="c#" Codebehind="NISSReport.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.NISSReport" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NISSReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language=javascript>
		function SetBegindate()
		{
		
			if(document.getElementById('txtCalendarDate')!=null && document.getElementById('txtCalendarDate')!= "undefined")
					year  =  document.getElementById('cmbFiscalYearFrom').options[document.getElementById('cmbFiscalYearFrom').selectedIndex].text;
			else
				year  =  document.getElementById('txtCalendarDate').value;
			var month =  document.getElementById('cmbFISCAL_BEGIN_MONTH').options[document.getElementById('cmbFISCAL_BEGIN_MONTH').selectedIndex].value;
			if(month<=9) month = "0"+month;
			document.getElementById('lblFISCAL_BEGIN_DATE').innerHTML = month+"/01/"+year;
			document.getElementById('hidFISCAL_BEGIN_DATE').value= month+"/01/"+year;
			if(document.getElementById('cmbFISCAL_END_MONTH').selectedIndex!=-1) SetEndDate();
			
		}
		
		function SetEndDate()
		{
			if(document.getElementById('cmbFiscalYearFrom')!=null && document.getElementById('cmbFiscalYearFrom')!= "undefined")
				year  =  document.getElementById('cmbFiscalYearFrom').options[document.getElementById('cmbFiscalYearFrom').selectedIndex].text;
			else
				year  =  document.getElementById('txtFiscalYearFrom').value;
			var begMonth =  document.getElementById('cmbFISCAL_BEGIN_MONTH').options[document.getElementById('cmbFISCAL_BEGIN_MONTH').selectedIndex].value;
			var month =  document.getElementById('cmbFISCAL_END_MONTH').options[document.getElementById('cmbFISCAL_END_MONTH').selectedIndex].value;
			month = (parseInt(begMonth)-1)+parseInt(month)
			if(month>12)
			{
				month =	parseInt(month)-12
				year++;
			}
			var days = GetDays(month,year);
			if(month<=9) month = "0"+month;			
			document.getElementById('lblFISCAL_END_DATE').innerHTML = month+"/"+days+"/"+year;
			document.getElementById('hidFISCAL_END_DATE').value = month+"/"+days+"/"+year;
			
		}
		function GetDays(month,year)
		{
			var days=30;
			if(month!=2)
			{
				switch(month)
				{
					case 1:
					case 3:
					case 5:
					case 7:
					case 8:
					case 10:
					case 12:
						days = 31;
				}
			}
			else
			{
				days=28;
				if((year%400)==0)
					days=29;
				else if((year%4)==0 && (year%100)!=0)	
					days=29;
			}
			return days;
		}
		function AddLob()
		{
		var LOB = document.getElementById('cmbAPP_LOB').options[document.getElementById('cmbAPP_LOB').selectedIndex].value;
		if(LOB!=2 && LOB!=3)
			{
				document.getElementById("capValue_Of_Date").style.display = "none";				
				document.getElementById("txtValue_Of_Date").style.display = "none";
			}
			else 
			{
				 //document.getElementById("capValue_Of_Date").setAttribute('enabled',false); 
				 document.getElementById("capValue_Of_Date").style.display = "inline";				
				 document.getElementById("txtValue_Of_Date").style.display = "inline";	
			}
			
		}
		function LobMessage()
		{
			var lob = document.getElementById('cmbAPP_LOB').options[document.getElementById('cmbAPP_LOB').selectedIndex].value;
			if(lob=="")
			{
				alert('Please select LOB');
				return false;
			}
		}
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" onload="top.topframe.main1.mousein = false;findMouseIn();ApplyColor();" class="bodyBackGround" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="RPT_NISS_REPORT" method="post" runat="server" >	
		<DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>		
			<!-- To add bottom menu -->
			<!-- To add bottom menu ends here --><asp:panel id="Panel1" Runat="server">
			<table class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
				<TR>
										<TD class="headereffectCenter" colSpan="5"> </TD>
				</TR>
				<TR>
					<TD class="headereffectCenter" colSpan="5">NISS Data String
					</TD>
				</TR>
				<TR>
					<TD class="midcolorc" colSpan="5"></TD>
				</TR>
				<TR>
					<TD class="midcolora" colSpan="2"><asp:label id="capCalendarDate" runat="server">Calendar Year</asp:label></TD>
					<TD class="midcolora" colSpan="3"><asp:textbox id="txtCalendarDate" runat="server" MaxLength="10" size="12"></asp:textbox>
				</TR>
				<TR>
					<TD class="midcolora" colSpan="1"><asp:label id="capAccountingDate" runat="server">	Accounting Year</asp:label></TD>
					<TD class="midcolora" ><asp:label id="capAccountingFromDate" runat="server">From:</asp:label></TD>
					<TD class="midcolora" colSpan="1">
					<asp:textbox id="txtAccountingDate" runat="server" MaxLength="10" size="12" ></asp:textbox><br>
					</TD>
					<TD class="midcolora" colSpan="1"><asp:label id="capAct_To_Date" runat="server">To:</asp:label></TD>
					<TD class="midcolora" colSpan="1"><asp:textbox id="txtAct_To_Date"  runat="server" MaxLength="10" size="12"></asp:textbox><BR>
					</TD>
				</TR>
				<TR>
					<TD class="midcolora" colSpan="2"><asp:label id="capCustomer" runat="server">Customer</asp:label></TD>
					<TD class="midcolora"><asp:textbox id="txtCustomer" runat="server">All</asp:textbox></TD>
					<TD class="midcolora" colSpan="1"><asp:label id="capPOLICY_ID" runat="server">Policy Number</asp:label></TD>
					<TD class="midcolora" colSpan="1"><asp:textbox id="txtPOLICY_ID" runat="server" size="15" maxlength="10" >All</asp:textbox>
					</TD>
				</TR>
				<TR>
					<TD class="midcolora" colSpan="2"><asp:label id="capAPP_LOB" runat="server">Call Request</asp:label></TD>
					<TD class="midcolora" colspan=3><asp:dropdownlist id="cmbAPP_LOB" onchange="AddLob();" onfocus="SelectComboIndex('cmbAPP_LOB')" runat="server"  ></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="midcolora" colSpan="1"><asp:label id="capCumulationDate" runat="server">Cumulative Date</asp:label></TD>
					<TD class="midcolora" colSpan="1"><asp:label id="capCumulationFromDate" runat="server">From:</asp:label></TD>
					<TD class="midcolora" colSpan="1"><asp:textbox id="txtCumulationFromDate" runat="server" MaxLength="10" size="12"></asp:textbox>
							
						</TD>
					<TD class="midcolora" colSpan="1"><asp:label id="capCumulationToDate" runat="server">To:</asp:label></TD>
					<TD class="midcolora" colSpan="1"><asp:textbox id="txtCumulationToDate" runat="server" MaxLength="10" size="12"></asp:textbox>
							<BR>
						</TD>
				</TR>
				<tr>
					<td class="midcolora" colspan=2><asp:label id="capValue_Of_Date" runat="server">Value as of Date:</asp:label></td>
					<td class="midcolora" colspan=4>
						<asp:TextBox ID="txtValue_Of_Date" Runat="server" size="12" MaxLength="10"></asp:TextBox></td>
				</tr>
				<TR>
				<TD class="midcolora" >
				<asp:checkbox id="chkNAMEONPOLICY" runat="server"></asp:checkbox>
				<asp:label id="capNAMEONPOLICY" runat="server" >Name on Policy</asp:label></TD>
				<TD class="midcolora" >
				<asp:checkbox id="chkPOLICYNUMBER" runat="server"></asp:checkbox>
				<asp:label id="capPOLICYNUMBER" runat="server">Policy Number</asp:label></TD>
				<TD class="midcolora" colspan="3" >
				<asp:checkbox id="chkTRANSEFFECTIVEDATE" runat="server"></asp:checkbox>
				<asp:label id="capTRANSEFFECTIVEDATE" runat="server" >Transaction Effective date</asp:label></TD>			
				</TR>
				
				<tr>
					<td class="midcolora" colspan="5">
						<cmsb:cmsbutton class="clsButton" id="btnExportExcel" runat="server" Text='Export to Excel'></cmsb:cmsbutton>
						<cmsb:cmsbutton class="clsButton" id="btnReport" runat="server" Text='Run Report'></cmsb:cmsbutton>
						<cmsb:cmsbutton class="clsButton" id="btnTextFile" runat="server" Text='Text File'></cmsb:cmsbutton>
					</td>
				</tr>
			</table>
			</asp:panel>
		</form>
	</body>
</HTML>

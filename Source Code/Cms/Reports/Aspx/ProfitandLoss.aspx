<%@ Page language="c#" Codebehind="ProfitandLoss.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.ProfitandLoss" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Accounting Report</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
			
			var calledfrom;
			calledfrom = "<%=strCalledfrom%>";
			//alert(calledfrom);
				
			function GetValue(obj,type,defaultValue)
			{
				var sVal="";
				
				if(type=='D')
				{
					for(var i=0;i<obj.length;i++)
					{
						if(obj.options(i).selected)
						{
							if(obj.options(i).value=='All')
							{
								if((defaultValue!='undefined') && (defaultValue!=null)) 
									sVal=defaultValue+",";
								else
									sVal="0,";
								break;
							}
							else
								sVal += obj.options(i).value + ",";
						}
					}
					if(sVal!='')
						sVal=sVal.toString().substr(0,sVal.length-1);
				}
				else if(type=='T')
				{
					sVal = obj.value;
				}
				
				return sVal;
			}
			
			
			function ShowReport()
			{	
				var Month="";
				var FinalcialYear="";
				var GeneralLedgerId ="";
						
				GeneralLedgerId	= GetValue(document.getElementById('cmbGeneralLedger'),'D');
				Month = GetValue(document.getElementById('cmbMonth'),'T');
				FinancialYear  = GetValue(document.getElementById('cmbFinancialYear'),'T');
				
				//alert('GeneralLedgerId '+ GeneralLedgerId);
				//alert('Month '+ Month);
				//alert('FinancialYear '+ FinancialYear);		
									
				if (GeneralLedgerId == '')
				{
					alert("Please Select General Ledger");
					return
				}
				
				if (Month == '')
				{
					alert("Please Select Month");
					return
				}
				
				if (FinancialYear == '')
				{
					alert("Please Select Financial Year");
					return
				}
				
				if (FinancialYear != "")
				{
					var MyArray = new Array();
					MyArray = FinancialYear.split("-");
					var YFrom="";
					var YTo="";
					YFrom=MyArray[0];
					YTo=MyArray[1];						
				}
				
				if (calledfrom == "TB")
				{					
					var url="CustomReport.aspx?PageName=TrialBalance&GLID="+ GeneralLedgerId + "&YEARFROM="+ YFrom + "&YEARTO=" + YTo + "&MMONTH=" + Month; 
					var windowobj = window.open(url,'TrialBalance','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();
				}
				
				if (calledfrom == "PL")
				{
					var url="CustomReport.aspx?PageName=ProfitLoss&GLID="+ GeneralLedgerId + "&YEARFROM="+ YFrom + "&YEARTO=" + YTo + "&MMONTH=" + Month; 
					var windowobj = window.open(url,'ProfitLoss','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();
					//var url="CustomReport.aspx?PageName=Test&CID=NULL"; 					
				}	
				
				if (calledfrom == "BS")
				{
					var url="CustomReport.aspx?PageName=BalanceSheet&GLID="+ GeneralLedgerId + "&YEARFROM="+ YFrom + "&YEARTO=" + YTo + "&MMONTH=" + Month; 
					var windowobj = window.open(url,'BalanceSheet','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();
				}							
			}						
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" scroll="yes" onload="top.topframe.main1.mousein = false;findMouseIn();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">		
		<DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>	
			<!-- To add bottom menu -->
			<!-- To add bottom menu ends here --><asp:panel id="Panel1" Runat="server">
				<TABLE class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD class="pageHeader" id="tdClientTop" colSpan="4">
							<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer></TD>
					</TR>
					<TR>
						<TD class="headereffectCenter" colSpan="4">
							<SCRIPT language="javascript">
								if (calledfrom == "PL")
								{
									document.write("Profit and Loss Report Selection Criteria");
								}
								else if(calledfrom == "TB")
								{
									document.write("Trial Balance Report Selection Criteria");
								}
								else
								{
									document.write("Balance Sheet Report Selection Criteria");
								}
							</SCRIPT>
						</TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					
					
					<TR>										
						<TD class="midcolora" colSpan="1">Month</TD>
						<TD class="midcolora" colSpan="1">
							<asp:dropdownlist id="cmbMonth" Runat="server">
								<asp:ListItem Value="1">January</asp:ListItem>
								<asp:ListItem Value="2">February</asp:ListItem>
								<asp:ListItem Value="3">March</asp:ListItem>
								<asp:ListItem Value="4">April</asp:ListItem>
								<asp:ListItem Value="5">May</asp:ListItem>
								<asp:ListItem Value="6">June</asp:ListItem>
								<asp:ListItem Value="7">July</asp:ListItem>
								<asp:ListItem Value="8">August</asp:ListItem>
								<asp:ListItem Value="9">September</asp:ListItem>
								<asp:ListItem Value="10">October</asp:ListItem>
								<asp:ListItem Value="11">November</asp:ListItem>
								<asp:ListItem Value="12">December</asp:ListItem>
							</asp:dropdownlist></TD>
							
							<TD class="midcolora" colSpan="1">
							<asp:Label id="lblFinancialYearStart" runat="server">Financial Year</asp:Label></TD>
						<TD class="midcolora" colSpan="1">
							<asp:DropDownList id="cmbFinancialYear" runat="server"></asp:DropDownList></TD>
					</TR>
					
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:Label id="lblGeneralLedger" runat="server">General Ledger</asp:Label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:DropDownList id="cmbGeneralLedger" runat="server"></asp:DropDownList></TD>
					</TR>
					
					
					<TR>
						<TD class="midcolorc" colSpan="4"><cmsb:cmsbutton class="clsbutton" id="btnReport" Runat="server" Text="Display Report"></cmsb:cmsbutton>
						</TD>
					</TR>
				</TABLE>
			</asp:panel></form>
	</body>
</HTML>

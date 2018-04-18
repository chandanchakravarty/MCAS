<%@ Page language="c#" Codebehind="BudgetcategoryReport.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.BudgetcategoryReport" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Budget Category Report</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
			
			function BindEvent()
			{	
				document.getElementById('trBudgetCategory').style.display= "inline";		
				document.getElementById('trDepartment').style.display= "none";						
			}
			
			function ShowDetails()
			{						
					if (document.getElementById("cmbReportType").value == 0)
					{
						document.getElementById('trBudgetCategory').style.display= "inline";		
						document.getElementById('trDepartment').style.display= "none";
					}					
					else
					{	
						document.getElementById('trBudgetCategory').style.display= "none";		
						document.getElementById('trDepartment').style.display= "inline";					
					}					
			}	
							
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
				var BudgetCategoryId ="";
				var FiscalId="";
				var YFrom="";
				var YTo="";
				
				var DeptId ="";
				DeptId	= GetValue(document.getElementById('cmbDepartment'),'D');
				var ReportType ="";
				ReportType	= GetValue(document.getElementById('cmbReportType'),'D');
							
				BudgetCategoryId	= GetValue(document.getElementById('cmbBudgetCategory'),'D');
				Month = GetValue(document.getElementById('cmbMonth'),'T');
				FinancialYear  = document.getElementById("cmbFinancialYear").options[document.getElementById("cmbFinancialYear").selectedIndex].text
				FiscalId  = GetValue(document.getElementById('cmbFinancialYear'),'D');
				
				/*alert('BudgetCategoryId '+ BudgetCategoryId);
				alert('Month '+ Month);
				alert('FinancialYear '+ FinancialYear);					
				alert('FiscalId '+ FiscalId);	*/
									
				/*				
				if (Month == '')
				{
					alert("Please Select Month");
					return
				}
				
				if (FinancialYear == '')
				{
					alert("Please Select Financial Year");
					return
				}*/
				
				if (FinancialYear != "All")
				{
					var MyArray = new Array();
					MyArray = FinancialYear.split("-");					
					YFrom=MyArray[0];
					YTo=MyArray[1];	
				}	
				else
				{
					YFrom="";
					YTo="";	
				}			
								
				if (BudgetCategoryId == 0)
				{
					BudgetCategoryId="NULL"
				}	
				if (DeptId == 0)
				{
					DeptId="NULL"
				}		
				
				if (FiscalId == 0)
				{
					FiscalId="NULL"	
				}
				
				if (YFrom == '')
				{
					YFrom="NULL"	
				}
				
				if (YTo == '')
				{
					YTo="NULL"	
				}
				
				//alert('YFrom '+ YFrom);
				//alert('YTo '+ YTo);
				
				if (FinancialYear != '')
				{
					if (ReportType == 0 )
					{
						var url="CustomReport.aspx?PageName=BudgetCategoryBC&BCID="+ BudgetCategoryId + "&YEARFROM="+ YFrom + "&YEARTO=" + YTo + "&FISCALID=" + FiscalId + "&MONTH=" + Month; 
						//alert(url);
						var windowobj = window.open(url,'BudgetCategoryByBC','resizable=yes,scrollbar=yes,top=100,left=50')
						windowobj.focus();											
					}
					else
					{
						var url="CustomReport.aspx?PageName=BudgetCategoryDept&BCID="+ DeptId + "&YEARFROM="+ YFrom + "&YEARTO=" + YTo + "&FISCALID=" + FiscalId + "&MONTH=" + Month; 
						//alert(url);
						var windowobj = window.open(url,'BudgetCategoryByDept','resizable=yes,scrollbar=yes,top=100,left=50')
						windowobj.focus();	
					}
				}
			}						
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" scroll="yes" onload="top.topframe.main1.mousein = false;findMouseIn();BindEvent();" MS_POSITIONING="GridLayout">
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
								
									document.write("Budget Category Report Selection Criteria");
								
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
						<TD class="midcolora" colSpan="1">
							<asp:Label id="lblReportType" runat="server">Select Report Type</asp:Label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:DropDownList id="cmbReportType" runat="server"></asp:DropDownList></TD>					
					</TR>
					
					<TR>
						<TD class="midcolora" colSpan="1">Month</TD>
						<TD class="midcolora" colSpan="1"><asp:dropdownlist id="cmbMonth" Runat="server">
								<asp:ListItem Value="01">January</asp:ListItem>
								<asp:ListItem Value="02">February</asp:ListItem>
								<asp:ListItem Value="03">March</asp:ListItem>
								<asp:ListItem Value="04">April</asp:ListItem>
								<asp:ListItem Value="05">May</asp:ListItem>
								<asp:ListItem Value="06">June</asp:ListItem>
								<asp:ListItem Value="07">July</asp:ListItem>
								<asp:ListItem Value="08">August</asp:ListItem>
								<asp:ListItem Value="09">September</asp:ListItem>
								<asp:ListItem Value="10">October</asp:ListItem>
								<asp:ListItem Value="11">November</asp:ListItem>
								<asp:ListItem Value="12">December</asp:ListItem>
							</asp:dropdownlist></TD>
							
						<TD class="midcolora" colSpan="1">
							<asp:Label id="lblFinancialYearStart" runat="server">Financial Period</asp:Label></TD>
						<TD class="midcolora" colSpan="1">
							<asp:DropDownList id="cmbFinancialYear" runat="server"></asp:DropDownList></TD>
					
					<TR>	
					
					<TR id="trBudgetCategory">
						<TD class="midcolora" colSpan="1">
							<asp:Label id="lblBudgetCategory" runat="server">Budget Category</asp:Label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:DropDownList id="cmbBudgetCategory" runat="server"></asp:DropDownList></TD>
					</TR>
					
					<TR id="trDepartment">
						<TD class="midcolora" colSpan="1">
							<asp:Label id="lblDepartment" runat="server">Department</asp:Label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:DropDownList id="cmbDepartment" runat="server"></asp:DropDownList></TD>
					</TR>
					
					<TR>
						<TD class="midcolorc" colSpan="4"><cmsb:cmsbutton class="clsbutton" id="btnReport" Runat="server" Text="Display Report"></cmsb:cmsbutton>
						</TD>
					</TR>
				</TABLE>
			</asp:panel></form>
	</body>
</HTML>

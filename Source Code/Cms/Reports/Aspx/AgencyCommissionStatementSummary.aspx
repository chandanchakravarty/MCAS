<%@ Page language="c#" Codebehind="AgencyCommissionStatementSummary.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.AgencyCommissionStatementSummary" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Agency Commission Statement Summary</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
			var calledfrom;
			calledfrom = "<%=strCalledfrom%>";
											
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
			//Added By Raghav For Itrack Issue #4617
			function changeYear()					
			{
			  var carrierId = '<%=strCarrier%>';
			   var getGetSystemId  = '<%=strAgencyId%>';			   
			  if (getGetSystemId.toUpperCase()!=carrierId.toUpperCase())
			   {
				var Month;
				var Year = document.getElementById('cmbYEAR').options[document.getElementById('cmbYEAR').selectedIndex].value;
				
				if(Year == <%=strCurrentYear%>)
				{
					for(Month=document.getElementById('cmbMONTH').options.length-1; Month>=<%=strCurrentMonth%>; Month--)
					{
						document.getElementById('cmbMONTH').options[Month] = null;
					}
				}
				else
				{
					for(Month=document.getElementById('cmbMONTH').options.length-1; Month>=0; Month--)
					{
						document.getElementById('cmbMONTH').options[Month] = null;
					}
					
					var oOption = new Option('January',1); 
					document.getElementById('cmbMONTH').options.add(oOption, 0); 

					var oOption = new Option('Febrary',2); 
					document.getElementById('cmbMONTH').options.add(oOption, 1); 

					var oOption = new Option('March',3); 
					document.getElementById('cmbMONTH').options.add(oOption, 2); 

					var oOption = new Option('April',4); 
					document.getElementById('cmbMONTH').options.add(oOption, 3); 

					var oOption = new Option('May',5); 
					document.getElementById('cmbMONTH').options.add(oOption, 4); 

					var oOption = new Option('June',6); 
					document.getElementById('cmbMONTH').options.add(oOption, 5); 

					var oOption = new Option('July',7); 
					document.getElementById('cmbMONTH').options.add(oOption, 6); 

					var oOption = new Option('August',8); 
					document.getElementById('cmbMONTH').options.add(oOption, 7); 

					var oOption = new Option('September',9); 
					document.getElementById('cmbMONTH').options.add(oOption, 8); 

					var oOption = new Option('October',10); 
					document.getElementById('cmbMONTH').options.add(oOption, 9); 

					var oOption = new Option('November',11); 
					document.getElementById('cmbMONTH').options.add(oOption, 10); 

					var oOption = new Option('December',12); 
					document.getElementById('cmbMONTH').options.add(oOption, 11); 

				}
             }   
		}
			
			function ShowReport()
			{	
				var Agency="";
				var Month="";
				var Year="";
								
				//Agency	= document.getElementById('hidAGENCY_ID').value;
				Agency	= GetValue(document.getElementById('lstAgencyName'),'D');
				Month = GetValue(document.getElementById('cmbMonth'),'T');
				Year  = GetValue(document.getElementById('cmbYEAR'),'T');
				
				/*alert('Agency '+ Agency);
				alert('Month '+ Month);
				alert('Year '+ Year);	*/			
									
				/*if (Agency == '0')
				{
					alert("Please Select Agency");
					return
				}*/
				
				if (calledfrom == "SR")
				{	
					var url="CustomReport.aspx?PageName=SummaryAgenyStatementRegular&AGENCY_ID="+ Agency + "&MONTH=" + Month + "&YEAR=" + Year + "&COMM_TYPE=REG";  
					var windowobj = window.open(url,'SummaryAgenyStatementRegular','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();
					//alert("Report Under Development");
				}								
				
				if (calledfrom == "SA")
				{	
					//alert("Report Under Development");
					var url="CustomReport.aspx?PageName=SummaryAgenyStatementAdditional&AGENCY_ID="+ Agency + "&MONTH=" + Month + "&YEAR=" + Year + "&COMM_TYPE=ADC";  
					var windowobj = window.open(url,'SummaryAgenyStatementAdditional','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();
				}	
			}						
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" class="bodyBackGround" topmargin="0" MS_POSITIONING="GridLayout" onload="ApplyColor();ChangeColor();top.topframe.main1.mousein = false;findMouseIn();showScroll();changeYear();">
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<form id="Form1" method="post" runat="server">
			<table cellSpacing='1' cellPadding='0' border='0' class='tableWidth' align="center">
				<tr>
					<td class="headereffectcenter" colspan="2">
						<SCRIPT language="javascript">
								if (calledfrom == "SR")
								{
									document.write("Summary of Agency Statements - Regular");
								}
								else if(calledfrom == "SA")
								{
									document.write("Summary of Agency Statements - Additional");
								}								
						</SCRIPT>
					</td>
				</tr>
				
				<TR>
					<TD class="midcolora" colSpan="1">
						<asp:label id="lblAgency" runat="server">Select Agency</asp:label></TD>
					<TD class="midcolora" colSpan="1">
						<asp:ListBox id="lstAgencyName" runat="server" Height="100px" Width="360px" SelectionMode="Multiple"></asp:ListBox></TD>
				</TR>
				
				<tr>
					<td class="midcolora">Year</td>
					<td class="midcolora">
						<asp:DropDownList ID="cmbYEAR" Runat="server" onchange="changeYear();"></asp:DropDownList>
					</td>
				</tr>
				
				<tr>
					<td class="midcolora">Month</td>
					<td class="midcolora">
						<asp:DropDownList ID="cmbMonth" Runat="server">
							<asp:ListItem Selected="True" Value="1">January</asp:ListItem>
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
						</asp:DropDownList>
					</td>
				</tr>
				
				<TR>
					<TD class="midcolorc" colSpan="2"><cmsb:cmsbutton class="clsbutton" id="btnReport" Runat="server" Text="Display Report"></cmsb:cmsbutton>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>

<%@ Page language="c#" Codebehind="EarnedPremium.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.EarnedPremium" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Earned Premium Report</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
										
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
			
			function ShowReportOld()
			{	
				var Month="";
				var Year="";
								
				Month = GetValue(document.getElementById('cmbMonth'),'T');
				Year  = GetValue(document.getElementById('cmbYEAR'),'T');
				
				/*
				alert('Month '+ Month);
				alert('Year '+ Year);*/				
								
				var url="CustomReport.aspx?PageName=EarnedPremium&MONTH=" + Month + "&YEAR=" + Year; 
				var windowobj = window.open(url,'EarnedPremium','resizable=yes,scrollbar=yes,top=100,left=50')
				windowobj.focus();				
			}		
			
			function ShowReport(calledfrom)
			{	
				var Month="";
				var Year="";
								
				Month = GetValue(document.getElementById('cmbMonth'),'T');
				Year  = GetValue(document.getElementById('cmbYEAR'),'T');
				
				/*
				alert('Month '+ Month);
				alert('Year '+ Year);*/				
				
				if (calledfrom == "N")
				{				
					var url="CustomReport.aspx?PageName=EarnedPremium&MONTH=" + Month + "&YEAR=" + Year + "&CalledFrom=N"; 
					var windowobj = window.open(url,'EarnedPremiumN','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();				
				}
				
				if (calledfrom == "G")
				{				
					var url="CustomReport.aspx?PageName=EarnedPremium&MONTH=" + Month + "&YEAR=" + Year + "&CalledFrom=G"; 
					var windowobj = window.open(url,'EarnedPremiumG','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();				
				}
			}						
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" class="bodyBackGround" topmargin="0" MS_POSITIONING="GridLayout" onload="ApplyColor();ChangeColor();top.topframe.main1.mousein = false;findMouseIn();showScroll();">
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<form id="Form1" method="post" runat="server">
			<table cellSpacing='1' cellPadding='0' border='0' class='tableWidth' align="center">
				<TR>
						<TD class="pageHeader" id="tdClientTop" colSpan="4">
							<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer></TD>
					</TR>
					<TR>
						<TD class="headereffectCenter" colSpan="4">Earned Premium Report Selection 
							Criteria</TD>
					</TR>
				<tr>
					<td class="midcolora" colSpan="2">Month</td>
					<td class="midcolora" colSpan="2">
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
				<tr>
					<td class="midcolora" colSpan="2">Year</td>
					<td class="midcolora" colSpan="2">
						<asp:DropDownList ID="cmbYEAR" Runat="server"></asp:DropDownList></td>
				</tr>
				<TR>
					<TD class="midcolorc" colSpan="2"><cmsb:cmsbutton class="clsbutton" id="btnReport" Runat="server" Text="Display Report"></cmsb:cmsbutton>
					</TD>
					<TD class="midcolorc" colSpan="2"><cmsb:cmsbutton class="clsbutton" id="btnReportGrpBy" Runat="server" Text="Display Report by ASLOB"></cmsb:cmsbutton>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>

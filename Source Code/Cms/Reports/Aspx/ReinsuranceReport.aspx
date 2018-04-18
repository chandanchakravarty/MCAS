<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="ReinsuranceReport.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.ReinsuranceReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ReinsuranceReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		
			function GetValue()
			{			
				var obj=document.getElementById('lstContractNumbers');
				var sVal="";
				for(var i=0;i<obj.length;i++)
					{
						if(obj.options(i).selected)
						{
								sVal += obj.options(i).text + ",";
						}
					}
					
					return sVal.substring(0,sVal.lastIndexOf(","));
			}
			
			function GetloB()
			{			
				var obj=document.getElementById('lloB');
				var sVal="";
				for(var i=0;i<obj.length;i++)
					{
						if(obj.options(i).selected)
						{
								sVal += obj.options(i).value + ",";
						}
					}
					
					return sVal.substring(0,sVal.lastIndexOf(","));
			}
			
			function ShowReport()
			{	
				if(Page_IsValid)
				{
					var StartMonth="";
					var EndMonth="";
					var ContractNumbers="";
					var YearFrom="";
					var YearTo="";
					var PolicyNumber="";
					var LOB="";
					LOB= GetloB();
					
					StartMonth  = document.getElementById('ddlStartMonth').options[document.getElementById('ddlStartMonth').selectedIndex].value;
					EndMonth  = document.getElementById('ddlEndMonth').options[document.getElementById('ddlEndMonth').selectedIndex].value;
					ContractNumbers=	GetValue();
					YearFrom	=	document.getElementById('txtYearFrom').value;	
					YearTo		=	document.getElementById('txtYearTo').value;	
					PolicyNumber =	document.getElementById('txtPolicyNumber').value;
					if(YearFrom == "")
						YearFrom = "0";
					if(YearTo == "")
						YearTo = "0";
					var url="CustomReport.aspx?PageName=ReinsuranceReport&StartMonth="+ StartMonth +" &EndMonth="+ EndMonth +" &ContractNumbers="+ ContractNumbers +" &YearFrom=" + YearFrom +" &YearTo=" + YearTo +" &PolicyNumber=" + PolicyNumber +" &LOB=" + LOB ;
					var windowobj = window.open(url,'ReinsuranceReport','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();	
				}
				else
					return;						
			}	
			
			function ContractYearTo_Check(obj,args)
			{
				var YearFrom	=	document.getElementById('txtYearFrom').value;
				var YearTo = document.getElementById('txtYearTo').value;
					
				if(YearTo < YearFrom)
				{
					args.IsValid=false;
					return;
				}
				args.IsValid=true;
			}
			
			function ContractYearFrom_Check(obj,args)
			{
				var YearFrom	=	document.getElementById('txtYearFrom').value;
				var YearTo = document.getElementById('txtYearTo').value;
					
				if(YearFrom > YearTo)
				{
					args.IsValid=false;
					return;
				}
				args.IsValid=true;
			}
			
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" onload="top.topframe.main1.mousein = false;findMouseIn();ApplyColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>			
			<asp:panel id="Panel1" Runat="server">
				<TABLE class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD class="pageHeader" id="tdClientTop" colSpan="4">
							<WEBCONTROL:GRIDSPACER id="grdSpacer" runat="server"></WEBCONTROL:GRIDSPACER></TD>
					</TR>
					<TR>
						<TD class="headereffectCenter" colSpan="4">Reinsurance Report Criteria</TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<ASP:LABEL id="lblStartDate" runat="server">Start Month</ASP:LABEL></TD>
						<TD class="midcolora" colSpan="1">
							<asp:DropDownList ID="ddlStartMonth" Runat="server" Width="167px" Height="100px">
								<asp:ListItem Value="All"></asp:ListItem>
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
							</asp:DropDownList>
						</TD>
						<TD class="midcolora" colSpan="1">
							<ASP:LABEL id="lblEndDate" runat="server">End Month</ASP:LABEL>
						<TD class="midcolora" colSpan="1">
							<asp:DropDownList ID="ddlEndMonth" Runat="server" Width="167px" Height="100px">
								
								<asp:ListItem Value=""></asp:ListItem>
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
							</asp:DropDownList>
						</TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<ASP:LABEL id="lblContractNumbers" Runat="server">Contract Numbers</ASP:LABEL>
						</TD>
						<TD class="midcolora" colSpan="1">
							<ASP:LISTBOX id="lstContractNumbers" Runat="server" SelectionMode="Multiple"></ASP:LISTBOX></TD>
						<TD class="midcolora" colSpan="1"><asp:label id="lblLOB" runat="server">Line of Business</asp:label>
						</TD>
						<TD class="midcolora" colSpan="1">
							<asp:LISTBOX  ID="lloB" runat="server" SelectionMode="Multiple">
							</asp:LISTBOX>
						</TD>
							
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<ASP:LABEL id="lblContractYearFrom" Runat="server">Contract Year From</ASP:LABEL>
						</TD>
						<TD class="midcolora" colSpan="1">
							<asp:TextBox ID="txtYearFrom" Runat="server" size="12" MaxLength="4"></asp:TextBox>
							<div><asp:rangevalidator id="rngtxtYearFrom" Display="Dynamic" ControlToValidate="txtYearFrom" Runat="server"
								MinimumValue="1950" Type="Integer"></asp:rangevalidator></div>
							<div>
								<asp:CustomValidator ID="cvtxtYearFrom" Runat="server" ControlToValidate="txtYearFrom" ClientValidationFunction="ContractYearFrom_Check" Display="Dynamic" ErrorMessage="Contract Year From can not be greater than Contract Year To"></asp:CustomValidator>
							</div>
						</TD>
						<TD class="midcolora" colSpan="1">
							<ASP:LABEL id="lblContractYearTo" Runat="server">Contract Year To</ASP:LABEL>
						</TD>
						<TD class="midcolora" colSpan="1">
							<asp:TextBox ID="txtYearTo" Runat="server" size="12" MaxLength="4"></asp:TextBox>
							<div><asp:rangevalidator id="rngtxtYearTo" Display="Dynamic" ControlToValidate="txtYearTo" Runat="server"
								MinimumValue="1950" Type="Integer"></asp:rangevalidator></div>
							<div>
								<asp:CustomValidator ID="cvtxtYearTo" Runat="server" ControlToValidate="txtYearTo" ClientValidationFunction="ContractYearTo_Check" Display="Dynamic" ErrorMessage="Contract Year To can not be less than Contract Year From"></asp:CustomValidator>
							</div>
						</TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<ASP:LABEL id="lblPolicyNumber" Runat="server">Enter Policy Number</ASP:LABEL>
						</TD>
						<TD class="midcolora" colSpan="3">
							<ASP:TEXTBOX id="txtPolicyNumber" Runat="server"></ASP:TEXTBOX></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4">
							<CMSB:CMSBUTTON class="clsbutton" id="btnReport" Runat="server" Text="Display Reinsurance Report"></CMSB:CMSBUTTON></TD>
					</TR>
				</TABLE>
			</asp:panel></form>
	</body>
</HTML>

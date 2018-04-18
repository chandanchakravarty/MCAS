<%@ Page language="c#" Codebehind="AgencyStatementPreview.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AgencyStatementPreview" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AgencyStatementPreview</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		//This function open the agency lookup window
		function OpenAgencyLookup()
		{
			var url='<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>';
			var idField,valueField,lookUpTagName,lookUpTitle;
	
			idField			=	'AGENCY_ID';
			valueField		=	'Name';
			lookUpTagName	=	'Agency';
			lookUpTitle		=	"Agency Names";
			
			OpenLookup( url,idField,valueField,"hidAGENCY_ID","txtAGENCY_NAME",lookUpTagName,lookUpTitle,'');
		}
		
		function ShowPrintPreview()
		{
		
			if (document.getElementById("txtAGENCY_NAME").value != "")
			{		
				var monthYear =document.getElementById("cmbYEAR").options[document.getElementById("cmbYEAR").selectedIndex].value ;
				window.open("agencyStatement.aspx?AGENCY_ID=" 
					+ document.getElementById("hidAGENCY_ID").value 
					+ "&MONTH=" + document.getElementById("cmbMonth").options[document.getElementById("cmbMonth").selectedIndex].value 
					+ "&YEAR=" + monthYear
					+ "&COMM_TYPE=" + document.getElementById("CmbCommType").options[document.getElementById("CmbCommType").selectedIndex].value 
					+ "&","AGENCYSTATEMENT");
					}
			else
			{
				alert("Please select agency first.");
			}		
		}
		</script>
	</HEAD>
	<body class="bodyBackGround" topmargin="0" MS_POSITIONING="GridLayout" onload="ApplyColor();ChangeColor();top.topframe.main1.mousein = false;findMouseIn();showScroll();">
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<form id="Form1" method="post" runat="server">
			<table cellSpacing='1' cellPadding='0' border='0' class='tableWidth' align="center">
				<tr>
					<td class="headereffectcenter" colspan="2">
						Agency Statement
					</td>
				</tr>
				<tr>
					<td class="midcolora">Agency
					</td>
					<td class='midcolora' width="75%">
						<asp:TextBox id='txtAGENCY_NAME' ReadOnly="True" runat='server' size="60"></asp:TextBox>
						<IMG id="imgAGENCY_NAME" style="CURSOR: hand" onclick="OpenAgencyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
							runat="server">
					</td>
				</tr>
				<tr>
					<td class="midcolora">Month</td>
					<td class="midcolora">
						<asp:DropDownList ID="cmbMonth" Runat="server">
							<asp:ListItem Selected="True" Value="1">Jan</asp:ListItem>
							<asp:ListItem Value="2">Feb</asp:ListItem>
							<asp:ListItem Value="3">Mar</asp:ListItem>
							<asp:ListItem Value="4">Apr</asp:ListItem>
							<asp:ListItem Value="5">May</asp:ListItem>
							<asp:ListItem Value="6">Jun</asp:ListItem>
							<asp:ListItem Value="7">Jul</asp:ListItem>
							<asp:ListItem Value="8">Aug</asp:ListItem>
							<asp:ListItem Value="9">Sep</asp:ListItem>
							<asp:ListItem Value="10">Oct</asp:ListItem>
							<asp:ListItem Value="11">Nov</asp:ListItem>
							<asp:ListItem Value="12">Dec</asp:ListItem>
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td class="midcolora">Year</td>
					<td class="midcolora">
						<asp:DropDownList ID="cmbYEAR" Runat="server"></asp:DropDownList>
					</td>
				</tr>
				<TR>
					<TD class="midcolora" style="HEIGHT: 20px">Commission Type</TD>
					<TD class="midcolora" style="HEIGHT: 20px">
						<asp:DropDownList id="CmbCommType" Runat="server" Width="150px">
							<asp:ListItem Value="REG">Regular Commission</asp:ListItem>
							<asp:ListItem Value="ADC">Additional Commission</asp:ListItem>
							<asp:ListItem Value="CAC">Complete App Commission</asp:ListItem>
						</asp:DropDownList></TD>
				</TR>
				<tr>
					<td class="midcolora">
						<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
					</td>
					<td class="midcolorr">
						<cmsb:cmsbutton class="clsButton" id="btnPrint" runat="server" Text="Print"></cmsb:cmsbutton>
					</td>
				</tr>
			</table>
			<input id="hidAGENCY_ID" type="hidden" name="hidAGENCY_ID" value="0" runat="server">
		</form>
	</body>
</HTML>

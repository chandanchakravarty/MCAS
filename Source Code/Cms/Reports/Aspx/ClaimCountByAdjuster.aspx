<%@ Page language="c#" Codebehind="ClaimCountByAdjuster.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.ClaimCountByAdjuster" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Claim Count by Adjuster Report</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
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
			
			
			function ShowReport()
			{		
				//var ExpirationStartDate="";
				//var ExpirationEndDate="";
				var PartyType ="";
				var Adjuster="";
				var ClaimStatus="";
				var LOB="";
				var FirstSort="";
				var SecondSort="";
					
				//ExpirationStartDate	= GetValue(document.getElementById('txtExpirationStartDate'),'T');
				//ExpirationEndDate	= GetValue(document.getElementById('txtExpirationEndDate'),'T');
				PartyType			= GetValue(document.getElementById('lstPartyType'),'D');	
				Adjuster			= GetValue(document.getElementById('lstAdjuster'),'D');					
				ClaimStatus			= GetValue(document.getElementById('lstClaimStatus'),'D','');				
				LOB					= GetValue(document.getElementById('lstLOB'),'D');
				FirstSort			= GetValue(document.getElementById('cmbFirstSort'),'T');
				SecondSort			= GetValue(document.getElementById('cmbSecondSort'),'T');
				
				var Month="";
				var Year="";
				Month = GetValue(document.getElementById('cmbMonth'),'T');
				Year  = GetValue(document.getElementById('cmbYEAR'),'T');
				//var url="CustomReport.aspx?PageName=AgentCommissionStatmentRegular&AGENCY_ID="+ Agency + "&MONTH=" + Month + "&YEAR=" + Year + "&COMM_TYPE=REG";
																
				//alert('ExpirationStartDate' + ExpirationStartDate);
				//alert('ExpirationEndDate' + ExpirationEndDate);
				
				/*alert('Month ' + Month);
				alert('Year ' + Year);
				alert('PartyType '+ PartyType);
				alert('Adjuster '+ Adjuster);				
				alert('ClaimStatus '+ ClaimStatus);
				alert('LOB '+ LOB);
				alert('FirstSort '+ FirstSort);
				alert('SecondSort '+ SecondSort);*/
								
				/*if (document.getElementById('txtAGENCY_NAME').value == '')
				{
					alert("Please Select Agency");
					return
				}
								
				if (Customer!= '')
				{*/					
					//var url="CustomReport.aspx?PageName=ClaimCountbyAdjuster&PartyType="+ PartyType + "&Adjuster="+ Adjuster + "&ClaimStatus=" + ClaimStatus + "&LOB=" + LOB  +  "&StartDate=" + ExpirationStartDate + "&EndDate=" + ExpirationEndDate + "&FirstSort=" + FirstSort + "&SecondSort=" + SecondSort; 
					var url="CustomReport.aspx?PageName=ClaimCountbyAdjuster&PartyType="+ PartyType + "&Adjuster="+ Adjuster + "&ClaimStatus=" + ClaimStatus + "&LOB=" + LOB  +  "&MONTH=" + Month + "&YEAR=" + Year + "&FirstSort=" + FirstSort + "&SecondSort=" + SecondSort; 
					//alert(url);
					var windowobj = window.open(url,'ClaimCountbyAdjuster','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();
				/*}
				else
				{
					alert("Please Select Customer");
				}*/
			}
			
			function OpenAgencyLookup()
			{			
				var url='<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>';
				OpenLookupWithFunction( url,"AGENCY_ID","Name","hidAGENCY_ID","txtAGENCY_NAME","Agency","Agency Names",'','PostFromLookup()');			
			}
		
			function PostFromLookup()
			{
				/*Post back the form */			
				__doPostBack('hidAGENCY_ID','');				
			}
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" scroll="yes" onload="top.topframe.main1.mousein = false;findMouseIn();ApplyColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>
			<!-- To add bottom menu -->
			<!-- To add bottom menu ends here--><asp:panel id="Panel1" Runat="server">
				<TABLE class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD class="pageHeader" id="tdClientTop" colSpan="4">
							<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer></TD>
					</TR>
					<TR>
						<TD class="headereffectCenter" colSpan="4">Claim Count by Adjuster Report Selection 
							Criteria</TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					
					<TR>
					<td class="midcolora" colSpan="1">Month</td>
					<td class="midcolora" colSpan="1">
						<asp:DropDownList ID="cmbMonth" Runat="server">
							<asp:ListItem Selected="True" Value="1">January</asp:ListItem>
							<asp:ListItem Value="2">Feburary</asp:ListItem>
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
					<td class="midcolora">Year</td>
					<td class="midcolora">
						<asp:DropDownList ID="cmbYEAR" Runat="server"></asp:DropDownList>
					</td>
					</TR>
					
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblPartyType" runat="server">Select Party Type</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstPartyType" runat="server" SelectionMode="Multiple" Width="240px" Height="100px"></asp:ListBox></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblAdjuster" runat="server">Select Adjuster</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstAdjuster" runat="server" SelectionMode="Multiple" Width="240px" Height="100px"></asp:ListBox></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblClaimStatus" runat="server">Select Claim Status</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstClaimStatus" runat="server" SelectionMode="Multiple" Width="240px" Height="100px"></asp:ListBox></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblLOB" runat="server">Select Line of Business</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstLOB" runat="server" SelectionMode="Multiple" Width="240px" Height="100px"></asp:ListBox></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblFirstSort" runat="server">First Sort</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:DropDownList id="cmbFirstSort" runat="server" Width="240px" Height="100px">
								<asp:ListItem Value="" Selected="True"></asp:ListItem>
								<asp:ListItem Value="ADJUSTER_NAME">Adjuster Name</asp:ListItem>
								<asp:ListItem Value="LOB_DESC">Line of Business</asp:ListItem>
								<%-- asp:ListItem Value="LOOKUP_VALUE_DESC">Description</asp:ListItem>
								<asp:ListItem Value="AGENCY_DISPLAY_NAME">Agency Name</asp:ListItem>
								<asp:ListItem Value="Claimant_Name">Claimant Name</asp:ListItem>
								<asp:ListItem Value="Policy_id">Policy Number</asp:ListItem>
								<asp:ListItem Value="Claim_Number">Claim Number</asp:ListItem>
								<asp:ListItem Value="Loss_Date">Loss Date</asp:ListItem>
								<asp:ListItem Value="Outstanding_Reserver">Outstanding Reserver</asp:ListItem>
								<asp:ListItem Value="Earned">Earned</asp:ListItem>
								<asp:ListItem Value="TotalPaid">Total Paid</asp:ListItem>
								<asp:ListItem Value="Recoveries">Recoveries</asp:ListItem>
								<asp:ListItem Value="TotalIncurred">Total Incurred</asp:ListItem>
								<asp:ListItem Value="Ratio">Ratio</asp:ListItem>
								<asp:ListItem Value="Status">Status</asp:ListItem --%>
							</asp:DropDownList></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblSecondSort" runat="server">Second Sort</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:DropDownList id="cmbSecondSort" runat="server" Width="240px" Height="100px">
								<asp:ListItem Value="" Selected="True"></asp:ListItem>
								<asp:ListItem Value="ADJUSTER_NAME">Adjuster Name</asp:ListItem>
								<asp:ListItem Value="LOB_DESC">Line of Business</asp:ListItem>
								<%--asp:ListItem Value="LOOKUP_VALUE_DESC">Description</asp:ListItem>
								<asp:ListItem Value="AGENCY_DISPLAY_NAME">Agency Name</asp:ListItem>
								<asp:ListItem Value="Claimant_Name">Claimant Name</asp:ListItem>
								<asp:ListItem Value="Policy_id">Policy Number</asp:ListItem>
								<asp:ListItem Value="Claim_Number">Claim Number</asp:ListItem>
								<asp:ListItem Value="Loss_Date">Loss Date</asp:ListItem>
								<asp:ListItem Value="Outstanding_Reserver">Outstanding Reserver</asp:ListItem>
								<asp:ListItem Value="Earned">Earned</asp:ListItem>
								<asp:ListItem Value="TotalPaid">Total Paid</asp:ListItem>
								<asp:ListItem Value="Recoveries">Recoveries</asp:ListItem>
								<asp:ListItem Value="TotalIncurred">Total Incurred</asp:ListItem>
								<asp:ListItem Value="Ratio">Ratio</asp:ListItem>
								<asp:ListItem Value="Status">Status</asp:ListItem --%>
							</asp:DropDownList></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"><cmsb:cmsbutton class="clsbutton" id="btnReport" Runat="server" Text="Display Report"></cmsb:cmsbutton>
						</TD>
					</TR>
				</TABLE>
			</asp:panel><input id="hidAGENCY_ID" type="hidden" name="hidAGENCY_ID" runat="server">
		</form>
	</body>
</HTML>

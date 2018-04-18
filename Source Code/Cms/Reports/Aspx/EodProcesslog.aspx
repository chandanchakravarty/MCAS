<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="EodProcesslog.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.EodProcesslog" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>EOD Process Log Report</title>
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
				
				var StartDate="";
				var EndDate="";
				var ProcessArea="";										
				var Option="";
				var Activity = "";
				var LangId = "";							
				
				StartDate	= GetValue(document.getElementById('txtExpirationStartDate'),'T');
				EndDate		= GetValue(document.getElementById('txtExpirationEndDate'),'T');
				//Code Cahanged By Lalit For Multilingual change
                if (document.getElementById("lstVendorList").selectedIndex > 0)
				    ProcessArea = GetValue(document.getElementById('lstVendorList'),'T'); 
                else
                    ProcessArea = "All"
				
                if (document.getElementById("lstStatusList").selectedIndex > 0)
				    Status =  GetValue(document.getElementById('lstStatusList'), 'T');
				else
				    Status = "All";
				
                if (document.getElementById("lstActivity").selectedIndex > 0)
                    Activity = GetValue(document.getElementById('lstActivity'), 'D');
                else
                    Activity = "0";

                if (document.getElementById("hidLandId").value != "")
                    LangId = document.getElementById("hidLandId").value;
                else
                    LangId = 'null';
                    
				//alert('StartDate '+ StartDate);
				//alert('EndDate '+ EndDate);
				//alert('ProcessArea ' + ProcessArea);
				//alert('Status ' + Status);
				//alert('Activity ' + Activity);
				
				//Added by Uday to check start & end Date before showing report
				Page_ClientValidate();				
				if (Page_IsValid == false)
				{				  
					return;
				}
				//
				if(StartDate == "")
				{
					StartDate = "NULL";
				}
				
				if(EndDate == "")
				{
					EndDate = "NULL";
				}
				
				if(ProcessArea == "All")
				{
					ProcessArea = "NULL";
				}
				
				if(Status == "All")
				{
					Status = "NULL";
				}
				if(Activity =="0")
				{
					Activity = "NULL";
				}
	var url = "CustomReport.aspx?PageName=EODProcesslog&FROMDATE=" + StartDate + "&TODATE=" + EndDate + "&StatusID=" + ProcessArea + "&Option=" + Status + "&Activity=" + Activity + "&LANG_ID=" + LangId; 
				//alert (url);
				var windowobj = window.open(url,'EODProcesslog','resizable=yes,scrollbar=yes,top=100,left=50')
				windowobj.focus();							
			}						
						
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" scroll="yes" onload="top.topframe.main1.mousein = false;findMouseIn();ApplyColor();"
		MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>
			<!-- To add bottom menu -->
			<!-- To add bottom menu ends here -->
			<asp:panel id="Panel1" Runat="server">
				<TABLE class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD class="pageHeader" id="tdClientTop" colSpan="4">
							<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer></TD>
					</TR>
					<TR>
						<TD class="headereffectCenter" colSpan="4"><asp:Label ID="lblHeader" runat="server" Text="EOD Process Log Report Selection Criteria"></asp:Label></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR id="trDate">
						<TD class="midcolora" width="18%">
							<asp:label id="lblExpirationStartDate" runat="server">From Date</asp:label></TD>
						<TD class="midcolora" width="32%">
							<asp:TextBox id="txtExpirationStartDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkExpirationStartDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgExpirationStartDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR>
							<asp:regularexpressionvalidator id="revExpirationStartDate" Runat="server" Display="Dynamic" ErrorMessage="Please enter Date in 'mm/dd/yyyy' format."
								ControlToValidate="txtExpirationStartDate"></asp:regularexpressionvalidator>
							<asp:CompareValidator id="cmpExpirationDate" Runat="server" Display="Dynamic" ErrorMessage="End Date can't be less than Start Date."
								ControlToValidate="txtExpirationEndDate" ControlToCompare="txtExpirationStartDate" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator></TD>
						<TD class="midcolora" width="18%">
							<asp:label id="lblExpirationEndDate" runat="server">To Date</asp:label></TD>
						<TD class="midcolora" width="32%">
							<asp:TextBox id="txtExpirationEndDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkExpirationEndDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgExpirationEndDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR>
							<asp:regularexpressionvalidator id="revExpirationEndDate" Runat="server" Display="Dynamic" ErrorMessage="Please enter Date in 'mm/dd/yyyy' format."
								ControlToValidate="txtExpirationEndDate"></asp:regularexpressionvalidator></TD>
					</TR>
					<TR>
							<TD class="midcolora" colSpan="1">
							<asp:label id="lblStatus" runat="server">Status</asp:label></TD>
							<TD class="midcolora" colSpan="3">
							<asp:dropdownlist id="lstStatusList" runat="server"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblVendor" runat="server">Process Area</asp:label></TD>
						<TD class="midcolora" colSpan="1">
							<asp:dropdownlist id="lstVendorList" runat="server" AutoPostBack ="True"></asp:dropdownlist></TD>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblActivity" runat="server">Activity</asp:label></TD>
						<TD class="midcolora" colSpan="1">
							<asp:ListBox id="lstActivity" runat="server" Height="80px" Width="150px" SelectionMode="Multiple"></asp:ListBox></TD>

					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4">
							<cmsb:cmsbutton class="clsbutton" id="btnReport" Runat="server" Text="Display Report"></cmsb:cmsbutton></TD>
					</TR>
                    <tr>
                    <td>
                    <input type="hidden" runat="server" id="hidLandId" />
                    </td>
                    </tr>

				</TABLE>
			</asp:panel>
		</form>
	</body>
</HTML>

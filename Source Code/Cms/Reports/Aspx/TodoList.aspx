<%@ Page language="c#" Codebehind="TodoList.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.TodoList" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>To do List Report</title>
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
				var Underwriter="";
				var Address="";
				var NameFormat="";
			
				StartDate			= GetValue(document.getElementById('txtInceptionStartDate'),'T');
				EndDate				= GetValue(document.getElementById('txtInceptionEndDate'),'T');
				Underwriter			= GetValue(document.getElementById('lstUnderWriter'),'D');
				
				if(document.getElementById('rdoConsider1').checked)
					NameFormat="1";
				else if(document.getElementById('rdoConsider2').checked)
					NameFormat="0";
				
				if(document.getElementById('rdAddress1').checked)
					Address="1";
				else if(document.getElementById('rdAddress2').checked)
					Address="0";	
				
				/*alert('StartDate '+ StartDate);
				alert('EndDate '+ EndDate);
				alert('Underwriter '+ Underwriter);
				alert('NameFormat '+ NameFormat);				
				alert('Address '+ Address);*/
				
				if (Underwriter!= '')
				{					
					var url="CustomReport.aspx?PageName=TodoList&Underwriterid="+ Underwriter + "&StartDateid="+ StartDate + "&EndDateid=" + EndDate  + "&NameFormatid=" + NameFormat + "&Addressid=" + Address; 
					var windowobj = window.open(url,'TodoList','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();
				}
			}		
			
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" onload="top.topframe.main1.mousein = false;findMouseIn();ApplyColor();" MS_POSITIONING="GridLayout">
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
						<TD class="headereffectCenter" colSpan="4">To do List Report Selection Criteria</TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblInceptionStartDate" runat="server">Start Date</asp:label></TD>
						<TD class="midcolora" colSpan="1">
							<asp:TextBox id="txtInceptionStartDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkInceptionStartDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgInceptionStartDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR>
							<asp:regularexpressionvalidator id="revInceptionStartDate" Runat="server" ControlToValidate="txtInceptionStartDate"
								ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator></TD>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblInceptionEndDate" runat="server">End Date</asp:label></TD>
						<TD class="midcolora" colSpan="1">
							<asp:TextBox id="txtInceptionEndDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkInceptionEndDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgInceptionEndDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink>
							<asp:regularexpressionvalidator id="revInceptionEndDate" Runat="server" ControlToValidate="txtInceptionEndDate"
								ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator><BR>
							<asp:comparevalidator id="cmpToDate" Runat="server" ControlToValidate="txtInceptionEndDate" ErrorMessage="End date can not be less than Start Date."
								Display="Dynamic" ControlToCompare="txtInceptionStartDate" Type="Date" Operator="GreaterThanEqual"></asp:comparevalidator></TD>
					</TR>
					<TR>
						<TD class="midcolora" width="36%">
							<asp:label id="lblUnderwriter" runat="server">Select Underwriter</asp:label></TD>
						<TD class="midcolora" width="64%" colspan ="3">
							<asp:ListBox id="lstUnderwriter" runat="server" SelectionMode="Multiple" Width="240px" Height="100px"></asp:ListBox></TD>
							
					</tr>
					
					<tr>		
						<TD class="midcolora" colspan ="1">
							<asp:label id="Label1" runat="server">Name Format</asp:label></TD>
						<TD class="midcolora" colspan ="3">
							<asp:RadioButton id="rdoConsider1" Runat="server" GroupName="F" Checked="True"></asp:RadioButton>Last 
							Name First Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:RadioButton id="rdoConsider2" Runat="server" GroupName="F"></asp:RadioButton>First 
							Name Last Name</TD>
					</TR>
					<TR>
						<TD class="midcolora" colspan ="1">
							<asp:label id="lblAddress" runat="server">Address Information on Report</asp:label></TD>
						<TD class="midcolora" colspan ="3">
							<asp:RadioButton id="rdAddress1" Runat="server" GroupName="Add" Checked="True"></asp:RadioButton>Place 
							Underneath Client Name &nbsp;							
							<asp:RadioButton id="rdAddress2" Runat="server" GroupName="Add"></asp:RadioButton>Separate 
							into Individual Columns
						</TD>
						
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"><cmsb:cmsbutton class="clsbutton" id="btnReport" Runat="server" Text="Display Report"></cmsb:cmsbutton>
						</TD>
					</TR>
				</TABLE>
			</asp:panel>
		</form>
	</body>
</HTML>

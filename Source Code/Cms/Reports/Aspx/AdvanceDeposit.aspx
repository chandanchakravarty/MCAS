<%@ Page language="c#" Codebehind="AdvanceDeposit.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.AdvanceDeposit" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Advance Deposit Report</title>
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
			
			
				var Customer="";
				var Agency="";	
				var Month="";
				var Year =""; 			
				
				Customer	= GetValue(document.getElementById('lstCustomerName'),'D');
				Agency		= document.getElementById('hidAGENCY_ID').value;
				Month       = document.getElementById('cmbMonth').options[document.getElementById('cmbMonth').selectedIndex].value;
				Year        = document.getElementById('txtYear').value;
				
				if (Customer == '0')
				{
					Customer='';
				}
				if(Month == "")
				{
				  Month = "Null";
				}
				if(Year == "")
				{
				  Year = "Null";
				}				
								
				//alert('Customer '+ Customer);
				//alert('Agency '+ Agency);
								
				//if (document.getElementById('txtAGENCY_NAME').value == '')
				//{
					//alert("Please Select Agency");
					//return
					
				//}
								
				//if (Customer!= '')
				//{		
				if(Page_IsValid == true)
				{			
					var url="CustomReport.aspx?PageName=AdvanceDeposit&Customerid="+ Customer + "&Agencyid="+ Agency + "&Month="+Month+"&Year="+Year ; 
					//alert(url);
					var windowobj = window.open(url,'AdvanceDeposit','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();					
				}
				//}
			     
			}
			
			function OpenAgencyLookup()
			{			
				var url='<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>';
				OpenLookupWithFunction( url,"AGENCY_ID","Name","hidAGENCY_ID","txtAGENCY_NAME","Agency","Agency Names",'','PostFromLookup()');			
			}
		
			function PostFromLookup()
			{
				__doPostBack('hidAGENCY_ID','')
			}
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" onload="top.topframe.main1.mousein = false;findMouseIn();ApplyColor();" MS_POSITIONING="GridLayout"
		scroll="yes">
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
						<TD class="headereffectCenter" colSpan="4">Advance Deposit Report Selection 
							Criteria</TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					
					<TR>
						<TD class="midcolora" width="36%">
							<asp:label id="lblAgent" runat="server">Select Agency</asp:label></TD>
						<TD class="midcolora" width="64%" colSpan="3">
							<asp:TextBox id="txtAGENCY_NAME" runat="server" size="40" ReadOnly="True"></asp:TextBox><IMG id="imgAGENCY_NAME" style="CURSOR: hand" onclick="OpenAgencyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
								runat="server"></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblCustomer" runat="server">Select Customers</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstCustomerName" runat="server" Height="100px" Width="240px" SelectionMode="Multiple"></asp:ListBox></TD>
					</TR>
					
					<!--Added By Raghav-->
					<TR>
					    <td class="midcolora" width="18%">Month/Year <span class="mandatory">*</span></td>
					<td class="midcolora" width="32%" colSpan="3"><asp:dropdownlist id="cmbMonth" Runat="server">
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
						</asp:dropdownlist><asp:textbox id="txtYear" runat="server" size="4" MaxLength="4"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revYear" runat="server" ControlToValidate="txtYear" ErrorMessage="Please enter valid Year only."
							Display="Dynamic"></asp:regularexpressionvalidator><asp:rangevalidator id="rnvYear" ControlToValidate="txtYear" ErrorMessage="Year should be greater than 1950 and less than 2099."
							Display="Dynamic" Runat="Server" Type="Integer" MaximumValue="2099" MinimumValue="1950"></asp:rangevalidator><asp:requiredfieldvalidator id="rfvYear" runat="server" ControlToValidate="txtYear" ErrorMessage="Please insert Year."
							Display="Dynamic"></asp:requiredfieldvalidator></td>			   
					    
					</TR>					
					<TR>
						<TD class="midcolorc" colSpan="4"><cmsb:cmsbutton class="clsbutton" id="btnReport" Runat="server" Text="Display Report"></cmsb:cmsbutton></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<input id="hidAGENCY_ID" type="hidden" name="hidAGENCY_ID" runat="server">
		</form>
	</body>
</HTML>

<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="NonPayCancellation.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.NonPayCancellation" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>NonPayCancellation</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
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
			
		function funcSetOrderBy()
		{
			var lstOrderBy = document.getElementById('lstOrderBy');	// ListBx 1
			var lstSelOrderBy = document.getElementById('lstSelOrderBy');// ListBx 2
			var i;
		
			for (i=lstOrderBy.options.length-1;i>=0;i--)
			{
				if(lstOrderBy.options[i].selected == true)
				{
					lstSelOrderBy.options.length = lstSelOrderBy.length + 1;
					lstSelOrderBy.options[lstSelOrderBy.length-1].value = lstOrderBy.options[i].value;
					lstSelOrderBy.options[lstSelOrderBy.length-1].text = lstOrderBy.options[i].text;
					lstOrderBy.options[i] = null;
				}
			}
		}		
		
		
		function funcRemoveOrderBy()
		{
			var lstOrderBy = document.getElementById('lstOrderBy');	// ListBx 1
			var lstSelOrderBy = document.getElementById('lstSelOrderBy');// ListBx 2
			var i;
			
			for (i=lstSelOrderBy.options.length-1;i>=0;i--)
			{
			
				if (lstSelOrderBy.options[i].selected == true)
				{
					lstOrderBy.options.length=lstOrderBy.length+1;
					lstOrderBy.options[lstOrderBy.length-1].value=lstSelOrderBy.options[i].value;
					lstOrderBy.options[lstOrderBy.length-1].text=lstSelOrderBy.options[i].text;
					lstSelOrderBy.options[i] = null;
				}
			}
		}
		
		function funcValidateSelOrderBy()
		{		
			if(document.getElementById('lstSelOrderBy').options.length == 0)
			{
				document.getElementById('lstSelOrderBy').className = "MandatoryControl";
				document.getElementById("spnSelOrderBy").style.display="inline";
				return false;
				
			}
			else
			{
				document.getElementById('lstSelOrderBy').className = "none";
				document.getElementById("spnSelOrderBy").style.display="none";
				return true;
			}
		
		}	
		
		
		function ShowReport()
		{
		 var Agency="";
		 
		     Agency	= GetValue(document.getElementById('txtAGENCY_NAME'),'D');
			    
			    if(Agency =='0')
				{
					Agency ="NULL";
				}
				
		   var returnVal = 	funcValidateSelOrderBy();
			if(returnVal == false)
			return Page_IsValid && returnVal;
			
			 var FromDate="";
			 var ToDate="";
			 var OrderBy="";	
			 
			FromDate	= GetValue(document.getElementById('txtFromTransactionDate'),'T');
			ToDate  	= GetValue(document.getElementById('txtToTransactionDate'),'T');
			
			var LstLen = document.getElementById('lstSelOrderBy').options.length;
			var strOrderBy = new String();
			for(var i = 0;i<LstLen;i++)
			{strOrderBy = strOrderBy + document.getElementById('lstSelOrderBy').options[i].value + ',';}			
				
			OrderBy = strOrderBy.substr(0,strOrderBy.length-1); 	 
			
			if(FromDate == "")
			{					
					FromDate = "NULL";
			}				
			
			if(ToDate == "")
			{					
				ToDate = "NULL";
			}	
						
			if(OrderBy =="")
			{
				OrderBy = "NULL";					
			}
			
			    //alert(FromDate  + 'FromDate' );
			    //alert(ToDate + 'ToDate');
			    //alert(OrderBy + 'OrderBy' );
			    
			
			if (Page_ClientValidate())
			{
				
				var url="CustomReport.aspx?PageName=NonPayCancellation&FROM_DATE=" + FromDate + "&TO_DATE=" + ToDate + "&ORDER_BY=" + OrderBy +"&AGENCY_ID=" + Agency ;
				var windowobj = window.open(url,'NonPayCancellation','resizable=yes,scrollbar=yes,top=100,left=50')
				windowobj.focus();
			}
			else
			return false;
		 
		}	
		
				
			
			</script>
  </head>
  <body  scroll="yes" onload="top.topframe.main1.mousein = false;findMouseIn();ApplyColor();" MS_POSITIONING="GridLayout">
	
    <form id="Form1" method="post" runat="server">
        <DIV><webcontrol:menu id="bottomMenu" scroll="yes"  runat="server"></webcontrol:menu></DIV>
			<TABLE class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD class="pageHeader" id="tdClientTop" colSpan="4">
							<WEBCONTROL:GRIDSPACER id="grdSpacer" runat="server"></WEBCONTROL:GRIDSPACER></TD>
					</TR>					
					<TR>
						<TD class="headereffectCenter" colSpan="4">Non-Pay Cancellation In Progress Report</TD>
					</TR>
					<TR></TR>
					<tr id = "trAllAgency">
					<td class="midcolora">Agency</td>
					<td class='midcolora' colSpan="40" width="40%">
						<asp:ListBox id='txtAGENCY_NAME' runat='server' Height="100px" Width="270px" SelectionMode="Multiple"></asp:ListBox>										
					</td>
					</tr>					
					<TR></TR>
					<TR id="trDate">
					<TD class="midcolora" width="18%"><asp:label id="lblFromTransactionDate" runat="server">From Transaction Date</asp:label></TD>	
					<TD class="midcolora" width="32%">
						<asp:TextBox id="txtFromTransactionDate" runat="server" size="12" MaxLength="10"></asp:TextBox>						
						<asp:hyperlink id="hlkFromTransactionDate" runat="server" CssClass="HotSpot">
							<ASP:IMAGE id="imgFromTransactionDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>								
						</asp:hyperlink><BR>
						<asp:requiredfieldvalidator id="rfvFromDate" runat="server" Display="Dynamic" ErrorMessage="Please enter from date."
										ControlToValidate="txtFromTransactionDate"></asp:requiredfieldvalidator>
						<asp:regularexpressionvalidator id="revFromTransactionDate" Runat="server" ControlToValidate="txtFromTransactionDate" 
							ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator>
						<asp:CompareValidator id="cmpFromTransactionDate" Runat="server" ControlToValidate="txtToTransactionDate" 
						ErrorMessage="End Date can't be less than Start Date." Display="Dynamic" Operator="GreaterThanEqual" Type="Date" 
						ControlToCompare="txtFromTransactionDate"></asp:CompareValidator>
					</TD>						
					
					<TD class="midcolora" width="18%"><asp:label id="lblToTransactionDate" runat="server">To Transaction Date</asp:label></TD>						
					<TD class="midcolora" width="32%">
						<asp:TextBox id="txtToTransactionDate" runat="server" size="12" MaxLength="10"></asp:TextBox>
						<asp:hyperlink id="hlkToTransactionDate" runat="server" CssClass="HotSpot">
							<ASP:IMAGE id="imgToTransactionDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
						</asp:hyperlink><BR>
						<asp:requiredfieldvalidator id="rfvToDate" runat="server" Display="Dynamic" ErrorMessage="Please enter to date."
							ControlToValidate="txtToTransactionDate"></asp:requiredfieldvalidator>
						<asp:regularexpressionvalidator id="revToTransactionDate" Runat="server" ControlToValidate="txtToTransactionDate" 
							ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator>
					</TD>
				</TR>					
				
							<TR>
						<TD class="midcolora" colSpan="1" width="20%"><asp:label id="lblOrderBy" runat="server">Order By</asp:label><!--<SPAN class="mandatory" id="spnOrderBy">*</SPAN>--></TD>
						<TD class="midcolora" colSpan="1" width="20%"><asp:ListBox id="lstOrderBy" runat="server" Height="65px" SelectionMode="Multiple" Width="150px" AutoPostBack="false">
								<asp:ListItem Value="POLICY_NUMBER">Policy Number</asp:ListItem>
								<asp:ListItem Value="CUSTOMER_NAME">Customer</asp:ListItem>
								<asp:ListItem Value="CANCEL_DUE_DATE">Cancel Due Date</asp:ListItem>
								<asp:ListItem Value="AGENCY_CODE">Agency Code</asp:ListItem></asp:ListBox>
								</TD>								
						<TD class="midcolora" vAlign="middle" colSpan="1" width="20%">&nbsp;<asp:Button id="btnSel" Runat="server" Text=">>"></asp:Button><BR><BR>
							&nbsp;<asp:Button id="btnDeSel" Runat="server" Text="<<"></asp:Button>
						</TD>
						<TD class="midcolora" colSpan="1" width="20%"><asp:ListBox id="lstSelOrderBy" onblur="funcValidateSelOrderBy" runat="server" Height="65px" SelectionMode="Multiple" Width="110px" AutoPostBack="False" onchange="funcValidateSelOrderBy">
							</asp:ListBox><br><asp:customvalidator id="csvSelOrderBy" Runat="server" ControlToValidate="lstSelOrderBy" Display="Dynamic" ClientValidationFunction="funcValidateSelOrderBy" Enabled="False"></asp:customvalidator><SPAN id="spnSelOrderBy" style="DISPLAY: none; COLOR: red">Please 
								select Order
								By.</SPAN> </TD></TR>							
								
									<TR>
						<TD class="midcolorc" colspan = "4">
							<cmsb:cmsbutton class="clsbutton" id="btnNonpay" Runat="server" Text="Non Pay Cancellation In Progress  Report"></cmsb:cmsbutton>
						</TD>
						</TR>
								
					
					
			</TABLE>
     </form>
	
  </body>
</html>

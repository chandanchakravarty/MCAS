<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="PriorLossSummary.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.PriorLossSummary" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<HTML>
	<HEAD>
		<title>PriorLossSummary</title>
		 <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		var url='<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>';
		
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
			
		  function OpenCustomerLookup()
		  {
		    var systemID = '<%=strSystemID%>';	     				
			var agencyID  = '<%=strAgencyID%>';
			
			if(systemID.toUpperCase() == <%=  "'" + CarrierSystemID.ToString().ToUpper() + "'" %>)		
			 { 	
				OpenLookupWithFunction(url,'CUSTOMER_ID','Name','hidCustomer_ID','txtCUSTOMER_NAME','CustLookupForm','Customer','','');						
			 }
			 else
			 {	
			  OpenLookupWithFunction(url,'CUSTOMER_ID','Name','hidCustomer_ID','txtCUSTOMER_NAME','CustLookupFormAgency','Customer','@systemID=\''+systemID + '\'','','');
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
								{
									sVal=defaultValue+",";				
								}
								else
								{
									sVal="0,";
									}
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
			  function CustomerName()
			  {
			  document.getElementById('hidCustomer_ID').value = "";
			  }					
		
			function ShowReport()
			{
				if (!funcValidateSelOrderBy())
					return false;
						
			    var StartDate="";
			    var EndDate="";
			    var Agency="";
			    var lob="";
			    var CustomerID="";
			    var Sort="";
			   
			    
			    
				StartDate = document.getElementById("txtStartDate").value;
				EndDate  =  document.getElementById("txtEndDate").value;
				Agency	= GetValue(document.getElementById('txtAGENCY_NAME'),'D');
				lob     =  GetValue(document.getElementById('txtlob'),'D'); 				
				var LstLen = document.getElementById('lstSelOrderBy').options.length;
				var strOrderBy = new String();
				for(var i = 0;i<LstLen;i++)
					{strOrderBy = strOrderBy + document.getElementById('lstSelOrderBy').options[i].value + ',';}				
					 
				Sort	=  strOrderBy.substr(0,strOrderBy.length-1); 
				CustomerID = document.getElementById('hidCustomer_ID').value;
				
				if(StartDate == "")
				{
					StartDate = "NULL";
				}
					
				if(EndDate == "")
				{
					EndDate = "NULL";
				}
												
				if(Agency == '0')
				{
					Agency = "NULL";
				}
				
				if(lob == '0')
				{
					lob = "NULL";
				}
												
				if(CustomerID == "")
				{
					
					CustomerID = "NULL";
				}
				
				if(Sort =="")
				{
					Sort = "NULL";					
				}	
										    
			   /*alert(StartDate  + 'StartDate' );
			    alert(EndDate + 'EndDate');
			    alert(Agency + 'Agency' );
			    alert(lob + 'lob' );
			    alert(CustomerID + 'CustomerID' );
			     alert(Sort + 'Sort' );*/
			   
				if (Page_ClientValidate())
				{
					var url="CustomReport.aspx?PageName=PriorLossSummary&STARTDATE=" + StartDate + "&ENDDATE=" + EndDate + "&AGENCYID=" + Agency + "&LOBID=" + lob + "&CLAIMENTID=" + CustomerID + "&SORTBY=" + Sort; 
					var windowobj = window.open(url,'PriorLossSummary','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();
				}
					else
					return false;	
			}
			
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" scroll="yes" onload="top.topframe.main1.mousein = false;findMouseIn();ApplyColor();" MS_POSITIONING="GridLayout">	
	 <form id="Form1" method="post" runat="server">
	<DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>
	<TABLE class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						 <TD class="pageHeader" id="tdClientTop" colSpan="4">
							<WEBCONTROL:GRIDSPACER id="grdSpacer" runat="server"></WEBCONTROL:GRIDSPACER>
						 </TD>
					</TR>
										
					<TR>
						<TD class="headereffectCenter" colSpan="4">Paid Loss Report</TD>
					</TR>					
				
					
					<TR id="trDate">
						
				   <TD class="midcolora" width="18%">
				        <asp:label id="lblStartDate" runat="server">Start Date</asp:label></TD>						
				   <TD class="midcolora" width="32%">
				         <asp:TextBox id="txtStartDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkStartDate" runat="server" CssClass="HotSpot">						
						 <ASP:IMAGE id="imgStartDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>							
							</asp:hyperlink>
				 <BR><asp:regularexpressionvalidator id="revStartDate" Runat="server" ControlToValidate="txtStartDate" ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator><asp:CompareValidator id="cmpStartDate" Runat="server" ControlToValidate="txtEndDate" ErrorMessage="End Date can't be less than Start Date." Display="Dynamic" Operator="GreaterThanEqual" Type="Date" ControlToCompare="txtStartDate"></asp:CompareValidator></TD>						
				
				<TD class="midcolora" width="18%"><asp:label id="lblEndDate" runat="server">End Date</asp:label></TD>						
				 <TD class="midcolora" width="32%"><asp:TextBox id="txtEndDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkEndDate" runat="server" CssClass="HotSpot">
					 <ASP:IMAGE id="imgEndDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
					</asp:hyperlink><BR><asp:regularexpressionvalidator id="revEndDate" Runat="server" ControlToValidate="txtEndDate" ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator></TD></TR>						
					
					
					<tr id = "trAllAgency">
					<td class="midcolora">Agency</td>
					<td class='midcolora' colSpan="40" width="40%">
						<asp:ListBox id='txtAGENCY_NAME' runat='server' Height="100px" Width="270px"></asp:ListBox>										
					</td>
					
					<tr id = "trlob">
					<td class="midcolora">Line of Business</td>
					<td class='midcolora' colSpan="40" width="40%">
						<asp:ListBox id='txtlob' runat='server' Height="100px" Width="270px" SelectionMode="Multiple"></asp:ListBox>										
					</td>
					
					<tr id = "trCustomer">
					<td class="midcolora">Select Insured or Claimant</td>
						<TD class="midcolora" colSpan="3" width="20%">
							<asp:textbox id="txtCUSTOMER_NAME" onchange="CustomerName()" runat="server" size="40" maxlength="10" ReadOnly="False"></asp:textbox><IMG id="Img1" style="CURSOR: hand" onclick="OpenCustomerLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
								runat="server">
						</TD>
						
						<TR>
						<TD class="midcolora" colSpan="1" width="20%"><asp:label id="lblOrderBy" runat="server">Order By</asp:label><!--<SPAN class="mandatory" id="spnOrderBy">*</SPAN>--></TD>
						
						<TD class="midcolora" colSpan="1" width="20%"><asp:ListBox id="lstOrderBy" runat="server" Height="65px" SelectionMode="Multiple" Width="150px" AutoPostBack="false">								
								<asp:ListItem Value="AGENCY_DISPLAY_NAME">Agency</asp:ListItem>
								<asp:ListItem Value="OCCURENCE_DATE">Start Date</asp:ListItem>								
								<asp:ListItem Value="LOB">Line of Business</asp:ListItem>
								</asp:ListBox></TD>
						<TD class="midcolora" vAlign="middle" colSpan="1" width="20%">&nbsp;<asp:Button id="btnSel" Runat="server" Text=">>"></asp:Button><BR><BR>
							&nbsp;<asp:Button id="btnDeSel" Runat="server" Text="<<"></asp:Button>
						</TD>
						<TD class="midcolora" colSpan="1" width="20%"><asp:ListBox id="lstSelOrderBy" onblur="funcValidateSelOrderBy" runat="server" Height="65px" SelectionMode="Multiple" Width="110px" AutoPostBack="False" onchange="funcValidateSelOrderBy">
						   <asp:ListItem Value="CUSTOMER_NAME">Name Insured</asp:ListItem>
							</asp:ListBox><br><asp:customvalidator id="csvSelOrderBy" Runat="server" ControlToValidate="lstSelOrderBy" Display="Dynamic" ClientValidationFunction="funcValidateSelOrderBy" Enabled="False"></asp:customvalidator><SPAN id="spnSelOrderBy" style="DISPLAY: none; COLOR: red">Please 
								select Order
								By.</SPAN> </TD></TR>
								
								<TR>
						<TD class="midcolorc" colSpan="4"><cmsb:cmsbutton class="clsbutton" id="btnDisplayreport" Runat="server" Text="Display Report"></cmsb:cmsbutton>
						</TD>
						
						

      </TABLE>
      <input id="hidCustomer_ID" type="hidden" name="hidCustomer_ID" runat="server">
  </form>
	
  </body>
</HTML>

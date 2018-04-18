<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="AgencyTransaction.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.AgencyTransaction" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>Agency Transaction</title>
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
		//var calledfrom =0;
		
			function ShowEffectiveDate()
			{			
				if (document.getElementById("chk_MonthYearEffectivedate"))
				{
					if (document.getElementById("chk_MonthYearEffectivedate").checked == true)
					 {					
						document.getElementById('trSpecificAgency').style.display= "inline";
						document.getElementById('trDate').style.display= "none";											
						if(document.getElementById('cmbMonthEffectiveDate').options.selectedIndex != "0" )
						{
						     if(document.getElementById('txtEffectiveyear').value =="")
						     {						     
								document.getElementById('rfvEffectiveYear').style.display= "inline";
								document.getElementById('rfvEffectiveYear').setAttribute('IsValid',true);
								document.getElementById('rfvEffectiveYear').setAttribute ('enabled',true);								
							  }							  
							  else
							  {							  
							 							  
							  document.getElementById('rfvEffectiveYear').style.display= "none";
							  document.getElementById('rfvEffectiveYear').setAttribute('IsValid',false);
							  document.getElementById('rfvEffectiveYear').setAttribute ('enabled',false);							   
							  }
						}						
						else
						{						
						document.getElementById('rfvEffectiveYear').style.display= "none";
						document.getElementById('rfvEffectiveYear').setAttribute('IsValid',false);
						document.getElementById('rfvEffectiveYear').setAttribute('enabled',false);
						document.getElementById('txtEffectiveyear').value = "";														
						}
					
					}					
					else
					{					
					document.getElementById('trSpecificAgency').style.display= "none";
					document.getElementById('trDate').style.display= "inline";
					document.getElementById('rfvEffectiveYear').style.display= "none";
					document.getElementById('rfvEffectiveYear').setAttribute('IsValid',false);
					document.getElementById('rfvEffectiveYear').setAttribute('enabled',false);	
				
				
				}	
			
			}		   
		       
		}			
				
		
			function BindEffectiveEvent()
			{
				document.getElementById('trSpecificAgency').style.display= "none";				
				if (document.getElementById("chk_MonthYearEffectivedate"))
				{
				document.getElementById('chk_MonthYearEffectivedate').onclick = ShowEffectiveDate;				
				}			
				
			}
			
			
		
			function ShowTransactionDate()
			{
				if (document.getElementById("chk_MonthYearTransactiondate"))
				{
				if (document.getElementById("chk_MonthYearTransactiondate").checked == true)
				{
				document.getElementById('trSpecificAgencyTransaction').style.display= "inline";
				document.getElementById('trTransactionDate').style.display= "none";
				//document.getElementById('rfvTransactionYear').setAttribute('IsValid',true);
				//document.getElementById('rfvTransactionYear').setAttribute('enabled',true);
				
				}					
				else
				{	
				document.getElementById('trSpecificAgencyTransaction').style.display= "none";
				document.getElementById('trTransactionDate').style.display= "inline";					
				//document.getElementById('rfvTransactionYear').setAttribute('IsValid',true);
				//document.getElementById('rfvTransactionYear').setAttribute('enabled',true);
				}	
				}
				
				if (document.getElementById("chk_MonthYearTransactiondate"))
				{
					if (document.getElementById("chk_MonthYearTransactiondate").checked == true)
					 {					
						document.getElementById('trSpecificAgencyTransaction').style.display= "inline";
						document.getElementById('trTransactionDate').style.display= "none";											
						if(document.getElementById('cmbMonthTransactionDate').options.selectedIndex != "0" )
						{
						     if(document.getElementById('txtTransactionyear').value =="")
						     {						     
								document.getElementById('rfvTransactionYear').style.display= "inline";
								document.getElementById('rfvTransactionYear').setAttribute('IsValid',true);
								document.getElementById('rfvTransactionYear').setAttribute ('enabled',true);								
							  }							  
							  else
							  {							  
							 							  
							  document.getElementById('rfvTransactionYear').style.display= "none";
							  document.getElementById('rfvTransactionYear').setAttribute('IsValid',false);
							  document.getElementById('rfvTransactionYear').setAttribute ('enabled',false);							   
							  }
						}						
						else
						{						
						document.getElementById('rfvTransactionYear').style.display= "none";
						document.getElementById('rfvTransactionYear').setAttribute('IsValid',false);
						document.getElementById('rfvTransactionYear').setAttribute('enabled',false);
						document.getElementById('txtTransactionyear').value = "";														
						}
					
					}					
					else
					{					
					document.getElementById('trSpecificAgencyTransaction').style.display= "none";
					document.getElementById('trTransactionDate').style.display= "inline";
					document.getElementById('rfvTransactionYear').style.display= "none";
					document.getElementById('rfvTransactionYear').setAttribute('IsValid',false);
					document.getElementById('rfvTransactionYear').setAttribute('enabled',false);	
				
				
				}	
			
			}		   
			}	
		
						function BindTransactionEvent()
						{
							document.getElementById('trSpecificAgencyTransaction').style.display= "none";					
							if (document.getElementById("chk_MonthYearTransactiondate"))
							{
							document.getElementById('chk_MonthYearTransactiondate').onclick = ShowTransactionDate;				
							}
						}
		
		
				    function OpenCustomerLookup()
					{
						var systemID = '<%=strSystemID%>';	     				
						var agencyID  = '<%=strAgencyID%>';
						
						if(systemID.toUpperCase() == <%=  "'" + CarrierSystemID.ToUpper() + "'" %>)		
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
			  
           			
			function ShowReport()
			{
			    
		     var returnVal = 	funcValidateSelOrderBy();
				if(returnVal == false)
					return Page_IsValid && returnVal;
					
			    var Agency="";
			    var MonthEffective="";
			    var YearEffective="";
			    var EffectiveStartDate="";
			    var EffectiveEndDate="";
			    var MonthTransaction="";
			    var YearTransaction="";
			    var TransactionStartDate="";
			    var TransactionEndDate="";
			    var PolicyNumber="";
			    var CustomerID="";
			    var OrderBy="";			
				
			    
			    Agency	= GetValue(document.getElementById('txtAGENCY_NAME'),'D');
			    
			    if(Agency =='0')
				{
					Agency ="NULL";
				}
				
				if(document.getElementById('chk_MonthYearEffectivedate').checked == true)
				{
					MonthEffective = GetValue(document.getElementById('cmbMonthEffectiveDate'),'D');					
					YearEffective  = GetValue(document.getElementById('txtEffectiveyear'),'T');
					
				}				
				else
				{
					EffectiveStartDate	= GetValue(document.getElementById('txtExpirationStartDate'),'T');					
					EffectiveEndDate	= GetValue(document.getElementById('txtExpirationEndDate'),'T');
					
				}
				
				if(document.getElementById('chk_MonthYearTransactiondate').checked == true)
				{
					MonthTransaction = GetValue(document.getElementById('cmbMonthTransactionDate'),'D');					
					YearTransaction  = GetValue(document.getElementById('txtTransactionyear'),'T');
					
				}				
				else
				{
					TransactionStartDate	= GetValue(document.getElementById('txtTransactionStartDate'),'T');
					TransactionEndDate  	= GetValue(document.getElementById('txtTransactionEndDate'),'T');
					
					
				}
				
				PolicyNumber		    = GetValue(document.getElementById('txtPOLICY_ID'),'T');								
				CustomerID				= document.getElementById('hidCustomer_ID').value;
				
				
				var LstLen = document.getElementById('lstSelOrderBy').options.length;
				var strOrderBy = new String();
				for(var i = 0;i<LstLen;i++)
					{strOrderBy = strOrderBy + document.getElementById('lstSelOrderBy').options[i].value + ',';}
				
				
				OrderBy = strOrderBy.substr(0,strOrderBy.length-1); 
				
				
				if(EffectiveStartDate == "")
				{
					EffectiveStartDate = "NULL";
				}
												
				if(EffectiveEndDate == "")
				{
					EffectiveEndDate = "NULL";
				}
				
				if(TransactionStartDate == "")
				{
					TransactionStartDate = "NULL";
				}
												
				if(TransactionEndDate == "")
				{
					TransactionEndDate = "NULL";
				}
				
				if(MonthEffective == "")
				{
					MonthEffective = "NULL";
				}
				
				if(YearEffective == "")
				{
					YearEffective = "NULL";
				}	
				
				if(MonthTransaction == "")
				{
					MonthTransaction = "NULL";
				}
				
				if(YearTransaction == "")
				{
					YearTransaction = "NULL";
				}	
				
				if(PolicyNumber == "0" ||PolicyNumber == "")
				{					
					PolicyNumber = "NULL";
				}
				
				if(CustomerID == "")
				{					
					CustomerID = "NULL";
				}
				
				if(OrderBy =="")
				{
					OrderBy = "NULL";					
				}						    
			    /*alert(Agency  + 'Agency' );
			    alert(MonthEffective + 'MonthEffective');
			    alert(YearEffective + 'YearEffective' );
			    alert(EffectiveStartDate + 'EffectiveStartDate' );
			    alert(EffectiveEndDate + 'EffectiveEndDate' );
			    alert(MonthTransaction + 'MonthTransaction' );
			    alert(YearTransaction + 'YearTransaction' );
			    alert(TransactionStartDate + 'TransactionStartDate' );
			     alert(TransactionEndDate + 'TransactionEndDate');
			      alert(PolicyNumber + 'PolicyNumber' );
			       alert(CustomerID +' CustomerID');
			        alert(OrderBy + 'OrderBy' );*/
			
				//if (calledfrom == "AGENCYTRANSACTION") 
				//{	
					if (Page_ClientValidate())
					{
					var url="CustomReport.aspx?PageName=AgencyTransaction&AGENCY_ID=" + Agency + "&EFFECTIVE_MONTH=" + MonthEffective + "&EFFECTIVE_YEAR=" + YearEffective + "&FROM_EFF_DATE=" + EffectiveStartDate + "&TO_EFF_DATE=" + EffectiveEndDate +  "&TRAN_MONTH=" + MonthTransaction +  "&TRAN_YEAR=" + YearTransaction + "&FROM_TRAN_DATE=" + TransactionStartDate + "&TO_TRAN_DATE=" + TransactionEndDate + "&POLICY_NUMBER=" + PolicyNumber + "&CUSTOMER_ID=" + CustomerID + "&ORDER_BY=" + OrderBy ;
					var windowobj = window.open(url,'AgencyTransaction','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();
					}
					else
					return false;
				/*}
				if (calledfrom == "AGENCYTRANWITHCOMM") 
				{ 					  	
				  	if(Page_ClientValidate())
				  	{
				  	var url="CustomReport.aspx?PageName=AgencyTransaction&AGENCY_ID=" + Agency + "&EFFECTIVE_MONTH=" + MonthEffective + "&EFFECTIVE_YEAR=" + YearEffective + "&FROM_EFF_DATE=" + EffectiveStartDate + "&TO_EFF_DATE=" + EffectiveEndDate +  "&TRAN_MONTH=" + MonthTransaction +  "&TRAN_YEAR=" + YearTransaction + "&FROM_TRAN_DATE=" + TransactionStartDate + "&TO_TRAN_DATE=" + TransactionEndDate + "&POLICY_NUMBER=" + PolicyNumber + "&CUSTOMER_ID=" + CustomerID + "&ORDER_BY=" + OrderBy +"&calledfrom=AgencyTranWithComm";   									
					var windowobj = window.open(url,'AgencyTransactionAGENCYTRANWITHCOMM','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();
					}
					else 
					return false;
				} */ 			    
			 
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
			return true;
		}		
					
								
		function OpenPolicyLookup()
		{  
			var systemID = '<%=strSystemID%>';	     				
			var agencyID  = '<%=strAgencyID%>';				
			
			if(systemID.toUpperCase() == <%=  "'" + CarrierSystemID.ToUpper() + "'" %>)		
				{ 
					OpenLookupWithFunction(url,'POL_INFORM','POLICY_NUMBER','hidPOLICYINFO','txtPOLICY_ID','ARREPORT','Policy','','');		
				}
			else
				{
					OpenLookupWithFunction(url,'POL_INFORM','POLICY_NUMBER','hidPOLICYINFO','txtPOLICY_ID','ARREPORTAGENCY','Policy','@systemID=\''+ systemID + '\'','',''); 										          
				}
		}
		
		</script>		
  </head>
  <body oncontextmenu = "return false;" scroll="yes" onload="top.topframe.main1.mousein = false;findMouseIn();ApplyColor();BindEffectiveEvent();BindTransactionEvent();" MS_POSITIONING="GridLayout">	
    <form id="Form1" method="post" runat="server">
	<DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>	
    <TABLE class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD class="pageHeader" id="tdClientTop" colSpan="4">
							<WEBCONTROL:GRIDSPACER id="grdSpacer" runat="server"></WEBCONTROL:GRIDSPACER></TD>
					</TR>					
					<TR>
						<TD class="headereffectCenter" colSpan="4">Agency Transaction Report</TD>
					</TR>					
					<TR>
						<TD class="midcolorc" colSpan="4"></TD></TR>					
					<TR>
						<TD class="midcolorc" colSpan="4"></TD></TR>	
					<tr id = "trAllAgency">
					<td class="midcolora">Agency</td>
					<td class='midcolora' colSpan="40" width="40%">
						<asp:ListBox id='txtAGENCY_NAME' runat='server' Height="100px" Width="270px" SelectionMode="Multiple"></asp:ListBox>										
					</td>
				</tr>					
					 <TR>
						<TD class="midcolora" colSpan="2" width="20%"> Specific Month\Year Effective Date </TD>
						<TD class="midcolora" colSpan="2" width="20%"><asp:CheckBox id="chk_MonthYearEffectivedate" runat="server"></asp:CheckBox></TD></TR>
					
					<TR id="trDate">
						<TD class="midcolora" width="18%"><asp:label id="lblExpirationStartDate" runat="server">From Effective Date</asp:label></TD>						
						<TD class="midcolora" width="32%"><asp:TextBox id="txtExpirationStartDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkExpirationStartDate" runat="server" CssClass="HotSpot">						
								<ASP:IMAGE id="imgExpirationStartDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>							
							</asp:hyperlink><BR><asp:regularexpressionvalidator id="revExpirationStartDate" Runat="server" ControlToValidate="txtExpirationStartDate" ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator><asp:CompareValidator id="cmpExpirationDate" Runat="server" ControlToValidate="txtExpirationEndDate" ErrorMessage="End Date can't be less than Start Date." Display="Dynamic" Operator="GreaterThanEqual" Type="Date" ControlToCompare="txtExpirationStartDate"></asp:CompareValidator></TD>						
						<TD class="midcolora" width="18%"><asp:label id="lblExpirationEndDate" runat="server">To Effective Date</asp:label></TD>						
						<TD class="midcolora" width="32%"><asp:TextBox id="txtExpirationEndDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkExpirationEndDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgExpirationEndDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR><asp:regularexpressionvalidator id="revExpirationEndDate" Runat="server" ControlToValidate="txtExpirationEndDate" ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator></TD></TR>					
							
					 <TR id="trSpecificAgency">
						<TD class="midcolora" width="20%" colSpan="1">Effective Month</TD>
						<TD class="midcolora" width="20%" colSpan="1"><asp:dropdownlist id="cmbMonthEffectiveDate" onchange="ShowEffectiveDate();" Runat="server" >
								<asp:ListItem></asp:ListItem>
								<asp:ListItem Value="01">January</asp:ListItem>
								<asp:ListItem Value="02">February</asp:ListItem>
								<asp:ListItem Value="03">March</asp:ListItem>
								<asp:ListItem Value="04">April</asp:ListItem>
								<asp:ListItem Value="05">May</asp:ListItem>
								<asp:ListItem Value="06">June</asp:ListItem>
								<asp:ListItem Value="07">July</asp:ListItem>
								<asp:ListItem Value="08">August</asp:ListItem>
								<asp:ListItem Value="09">September</asp:ListItem>
								<asp:ListItem Value="10">October</asp:ListItem>
								<asp:ListItem Value="11">November</asp:ListItem>
								<asp:ListItem Value="12">December</asp:ListItem>
							</asp:dropdownlist></TD>
						<TD class="midcolora" colSpan="1" width ="20%">Effective Year</TD>
						<TD class="midcolora" colSpan="1" width ="20%"><asp:textbox id="txtEffectiveyear" onblur = "ShowEffectiveDate();" runat="server" size="5" MaxLength="4"></asp:textbox><BR><asp:regularexpressionvalidator id="revEffectiveYear" runat="server" ControlToValidate="txtEffectiveyear" ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator><asp:rangevalidator id="rngEffectiveYEAR" Runat="server" ControlToValidate="txtEffectiveyear" Display="Dynamic" Type="Integer" MinimumValue="1950"></asp:rangevalidator>
						
						<asp:requiredfieldvalidator id="rfvEffectiveYear" runat="server" Display="Dynamic" ErrorMessage="Please enter Year."
										ControlToValidate="txtEffectiveyear"></asp:requiredfieldvalidator></TD></TR>
											
					<TR>
						<TD class="midcolora" colSpan="2" width ="20%"> Specific Month\Year Transaction Date </TD>
						<TD class="midcolora" colSpan="2" width="20%"><asp:CheckBox id="chk_MonthYearTransactiondate" runat="server"></asp:CheckBox></TD></TR>
					<TR id="trTransactionDate">
						
						<TD class="midcolora" width="18%"><asp:label id="lblTransactionStartDate" runat="server">From Transaction Date</asp:label></TD>						
						<TD class="midcolora" width="32%"><asp:TextBox id="txtTransactionStartDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkTransactionStartDate" runat="server" CssClass="HotSpot">								
								<ASP:IMAGE id="imgTransactionStartDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>							
						 </asp:hyperlink><BR><asp:regularexpressionvalidator id="revTransactionStartDate" Runat="server" ControlToValidate="txtTransactionStartDate" ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator><asp:CompareValidator id="cmpTransactionDate" Runat="server" ControlToValidate="txtTransactionEndDate" ErrorMessage="End Date can't be less than Start Date." Display="Dynamic" Operator="GreaterThanEqual" Type="Date" ControlToCompare="txtTransactionStartDate"></asp:CompareValidator></TD>						
						<TD class="midcolora" width="18%"><asp:label id="lblTransactionEndDate" runat="server">To Transaction Date</asp:label></TD>						
						<TD class="midcolora" width="32%"><asp:TextBox id="txtTransactionEndDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkTransactionEndDate" runat="server" CssClass="HotSpot">							
								<ASP:IMAGE id="imgTransactionEndDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>							
							</asp:hyperlink><BR><asp:regularexpressionvalidator id="revTransactionEndDate" Runat="server" ControlToValidate="txtTransactionEndDate" ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator></TD></TR>
				
					 <TR id="trSpecificAgencyTransaction">
						<TD class="midcolora" colSpan="1" width="20%">Transaction Month</TD>
						<TD class="midcolora" colSpan="1" width="20%"><asp:dropdownlist id="cmbMonthTransactionDate" onchange="ShowTransactionDate();" Runat="server">
								<asp:ListItem></asp:ListItem>
								<asp:ListItem Value="01">January</asp:ListItem>
								<asp:ListItem Value="02">February</asp:ListItem>
								<asp:ListItem Value="03">March</asp:ListItem>
								<asp:ListItem Value="04">April</asp:ListItem>
								<asp:ListItem Value="05">May</asp:ListItem>
								<asp:ListItem Value="06">June</asp:ListItem>
								<asp:ListItem Value="07">July</asp:ListItem>
								<asp:ListItem Value="08">August</asp:ListItem>
								<asp:ListItem Value="09">September</asp:ListItem>
								<asp:ListItem Value="10">October</asp:ListItem>
								<asp:ListItem Value="11">November</asp:ListItem>
								<asp:ListItem Value="12">December</asp:ListItem>
							</asp:dropdownlist></TD>
						<TD class="midcolora" colSpan="1" width="20%">Transaction Year</TD>
						<TD class="midcolora" colSpan="1" width="20%"><asp:textbox id="txtTransactionyear" onblur = "ShowTransactionDate();" runat="server" size="5" MaxLength="4"></asp:textbox><BR><asp:regularexpressionvalidator id="revTransactionYear" runat="server" ControlToValidate="txtTransactionyear" ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator><asp:rangevalidator id="rngTransactionYEAR" Runat="server" ControlToValidate="txtTransactionyear" Display="Dynamic" Type="Integer" MinimumValue="1950"></asp:rangevalidator>						
						<asp:requiredfieldvalidator id="rfvTransactionYear" runat="server" Display="Dynamic" ErrorMessage="Please enter Year."
										ControlToValidate="txtTransactionyear"></asp:requiredfieldvalidator>
						</TD></TR>						
					  <TR>					  
						<TD class="midcolora" colSpan="1" width="20%">
							<asp:label id="capPOLICY_ID" runat="server">Policy Number</asp:label></TD>
						<TD class="midcolora" colSpan="1" width="20%">
							<asp:textbox id="txtPOLICY_ID" runat="server" size="14" maxlength="10" ReadOnly="False"></asp:textbox><IMG id="imgSelect" style="CURSOR: hand" onclick="OpenPolicyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
								runat="server">
								
						</TD>
						<TD class="midcolora" colSpan="1" width="20%">
							<asp:label id="capCUSTOMER_NAME" runat="server">Customer Name</asp:label></TD>
						<TD class="midcolora" colSpan="1" width="20%">
							<asp:textbox id="txtCUSTOMER_NAME" runat="server" size="40" maxlength="10" ReadOnly="False"></asp:textbox><IMG id="Img1" style="CURSOR: hand" onclick="OpenCustomerLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
								runat="server">
						</TD>
						</TR>				
						
						<TR>
						<TD class="midcolora" colSpan="1" width="20%"><asp:label id="lblOrderBy" runat="server">Order By</asp:label><!--<SPAN class="mandatory" id="spnOrderBy">*</SPAN>--></TD>
						<TD class="midcolora" colSpan="1" width="20%"><asp:ListBox id="lstOrderBy" runat="server" Height="65px" SelectionMode="Multiple" Width="150px" AutoPostBack="false">
								<asp:ListItem Value="POLICY_NUMBER">Policy Number</asp:ListItem>
								<asp:ListItem Value="CUSTOMER_NAME">Customer</asp:ListItem>
								<asp:ListItem Value="EFFECTIVE_DATE">Effective Date</asp:ListItem>
								<asp:ListItem Value="TRANSACTION_DATE">Transaction Date</asp:ListItem>								
								<asp:ListItem Value="BILL_TYPE">Bill Type</asp:ListItem>
								<asp:ListItem Value="TRANSACTION_DESCRIPTION">Transaction Description</asp:ListItem></asp:ListBox></TD>
						<TD class="midcolora" vAlign="middle" colSpan="1" width="20%">&nbsp;<asp:Button id="btnSel" Runat="server" Text=">>"></asp:Button><BR><BR>
							&nbsp;<asp:Button id="btnDeSel" Runat="server" Text="<<"></asp:Button>
						</TD>
						<TD class="midcolora" colSpan="1" width="20%"><asp:ListBox id="lstSelOrderBy" onblur="funcValidateSelOrderBy" runat="server" Height="65px" SelectionMode="Multiple" Width="110px" AutoPostBack="False" onchange="funcValidateSelOrderBy">
							</asp:ListBox><br><asp:customvalidator id="csvSelOrderBy" Runat="server" ControlToValidate="lstSelOrderBy" Display="Dynamic" ClientValidationFunction="funcValidateSelOrderBy" Enabled="False"></asp:customvalidator><SPAN id="spnSelOrderBy" style="DISPLAY: none; COLOR: red">Please 
								select Order
								By.</SPAN> </TD></TR>
								
						<TR>
						<TD class="midcolorc" colspan = "4">
							<cmsb:cmsbutton class="clsbutton" id="btnAgencyTran" Runat="server" Text="Agency Transaction Report"></cmsb:cmsbutton>
						</TD>
						</TR> 	
						
				
    </TABLE>
    <input id="hidPOLICYINFO" type="hidden" name="hidPOLICYINFO" runat="server">
    <input id="hidAGENCY_ID" type="hidden" name="hidAGENCY_ID" runat="server">
    <input id="hidCustomer_ID" type="hidden" name="hidCustomer_ID" runat="server">
     </form>
	
  </body>
</html>

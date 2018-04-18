<%@ Page language="c#" Codebehind="GeneralLedger_AccountSummary.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.GeneralLedger_AccountSummary" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Summary Report</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		
		function BindEvent()
		{	
			document.getElementById('trSpecificPolicy').style.display= "none";					
			if (document.getElementById("CheckBox1"))			
			{
				ShowContact();			
			}
			
			document.getElementById('trSpecificAgency').style.display= "none";	
			if (document.getElementById("CheckBox2"))
			{
				document.getElementById('CheckBox2').onclick = ShowDate;				
			}
			
			if (document.getElementById("CheckBox3"))
			{
				document.getElementById('CheckBox3').onclick = ShowAllPolicies;				
			}
		}
		
		function ShowContact()
		{
			
			if (document.getElementById("CheckBox1"))
			{			
				if (document.getElementById("CheckBox1").checked == true)
				{
					document.getElementById('trSpecificPolicy').style.display= "inline";
					document.getElementById('trAgency').style.display= "none";
					document.getElementById('trPolicies').style.display= "none";
					document.getElementById('trAllPolicies').style.display= "none";					
				}
									
				else
				{	
					document.getElementById('trSpecificPolicy').style.display= "none";
					document.getElementById('trAgency').style.display= "inline";
					document.getElementById('trPolicies').style.display= "inline";	
					document.getElementById('trAllPolicies').style.display= "inline";					
				}	
			}
		}
		
		function ShowAllPolicies()
		{					
			if (document.getElementById("CheckBox3"))
			{
			
				if (document.getElementById("CheckBox3").checked == true)
				{					
					document.getElementById('trAgency').style.display= "none";
					document.getElementById('trPolicies').style.display= "none";
				}
					
				else
				{	
					document.getElementById('trAgency').style.display= "inline";
					document.getElementById('trPolicies').style.display= "inline";	
				}	
			}
		}
		
		function ShowDate()
		{
			if (document.getElementById("CheckBox2"))
			{
				if (document.getElementById("CheckBox2").checked == true)
				{
					document.getElementById('trSpecificAgency').style.display= "inline";
					document.getElementById('trDate').style.display= "none";
				}					
				else
				{	
					document.getElementById('trSpecificAgency').style.display= "none";
					document.getElementById('trDate').style.display= "inline";					
				}	
			}
		}
		
		function txtAMOUNT_To_Validate(objSource,objArgs)
		{
			value = document.getElementById("txtToRange").value;
			value = ReplaceString(value, ",","");
			if (value != "")
			{
				if (isNaN(value))
				{
					objArgs.IsValid = false;
					return;
				}
				else
				{
					if (parseFloat(value) == 0)
					{
						objArgs.IsValid = false;
						return;
					}
				}
			}
			objArgs.IsValid = true;
		}
		
		function txtAMOUNT_From_Validate(objSource,objArgs)
		{
			value = document.getElementById("txtFromRange").value;
			value = ReplaceString(value, ",","");
			if (value != "")
			{
				if (isNaN(value))
				{
					objArgs.IsValid = false;
					return;
				}
				else
				{
					if (parseFloat(value) == 0)
					{
						objArgs.IsValid = false;
						return;
					}
				}
			}
			objArgs.IsValid = true;
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
				var Month="";
				var Year="";
				var ExpirationStartDate="";
				var ExpirationEndDate="";
				var Account="";
				var	State="";
				var LOB="";
				var Vendor="";
				var Transaction="";
				var AmountFrome="";
				var AmountTo="";
				var Combination="";				
				var PolicyID="";
				var CustomerID="";
				var VersionID="";
				var Agency="";								
											
				if(document.getElementById('CheckBox2').checked == true)
				{
					Month = GetValue(document.getElementById('cmbMonth'),'D');
					Year  = GetValue(document.getElementById('txtyear'),'T');
				}				
				else
				{
					ExpirationStartDate	= GetValue(document.getElementById('txtExpirationStartDate'),'T');
					ExpirationEndDate	= GetValue(document.getElementById('txtExpirationEndDate'),'T');
				}
				
				Agency	= document.getElementById('hidAGENCY_ID').value;
								
				if(document.getElementById('CheckBox1').checked == true)
				{
					//alert("specific policy");
					
					Combination = document.getElementById('hidPolicyID').value;
					
					if (Combination == "")
					{
						alert("Please Select a Policy");
						return;
					}
										
					if(Combination !='')
					{
						var arrCPV=Combination.split("^");
						CustomerID = arrCPV[2];						
						PolicyID = arrCPV[0];
						VersionID = arrCPV[1];						
					}					
				}
								
				else if(Agency == "")
				{
					//alert("No selection");
					CustomerID = "NULL";					
					PolicyID = "NULL";
					VersionID = "NULL";
				}
				else
				{
					//alert("Multiple Policies");
					Combination		= GetValue(document.getElementById('lstPolicies'),'D');	
					//alert(Combination);
					
					if (Combination!= "0")
					{
						var arrComma=Combination.split(",");
						var i;
						var CustIdList="";
						var PolIdList="";
						var VerIdList="";
						for (i=0;i<arrComma.length;i++)
						{
							var arrHyphen = arrComma[i].split('-');
							
							if (CustIdList == "")
								CustIdList = arrHyphen[0];
							else
								CustIdList = CustIdList + "," + arrHyphen[0];								
								
							if (PolIdList == "")
								PolIdList = arrHyphen[1];
							else
								PolIdList = PolIdList + "," + arrHyphen[1];							
						
							if (VerIdList == "")
								VerIdList = arrHyphen[2];
							else
								VerIdList = VerIdList + "," + arrHyphen[2];							
						}
					
						//alert("CustIdList = " + CustIdList);
						//alert("PolIdList = " + PolIdList);
						//alert("VerIdList = " + VerIdList);
						CustomerID = CustIdList;					
						PolicyID = PolIdList;
						VersionID = VerIdList;				
					}
					else
					{
						CustomerID = "NULL";					
						PolicyID = "NULL";
						VersionID = "NULL";					
					}
				}
				
				if(document.getElementById('CheckBox3').checked == true)
				{
					//alert("All Policies");
					CustomerID = "NULL";					
					PolicyID = "NULL";
					VersionID = "NULL";
				}
			
				Account				= GetValue(document.getElementById('lstAccount'),'D');
				AmountFrom			= GetValue(document.getElementById('txtFromRange'),'T');
				AmountTo			= GetValue(document.getElementById('txtToRange'),'T');
				State				= GetValue(document.getElementById('lstStateName'),'D');				
				Transaction			= GetValue(document.getElementById('lstTransaction'),'D');
				LOB					= GetValue(document.getElementById('lstLOB'),'D');
				Vendor			    = GetValue(document.getElementById('lstVendorList'),'D');
				
				if(ExpirationStartDate == "")
				{
					ExpirationStartDate = "NULL";
				}
												
				if(ExpirationEndDate == "")
				{
					ExpirationEndDate = "NULL";
				}
				
				if(Account == "0")
				{
					Account = "NULL";
				}	
				
				if(State == "0")
				{
					State = "NULL";
				}	
				
				if(LOB == "0")
				{
					LOB = "NULL";
				}
				
				if(Vendor == "0")
				{
					Vendor = "NULL";
				}
				
				if(Transaction == "0")
				{
					Transaction = "NULL";
				}
				
				if(Month == "")
				{
					Month = "NULL";
				}
				
				if(Year == "")
				{
					Year = "NULL";
				}	
				
				if(AmountFrom == "")
				{
					AmountFrom = "NULL";
				}
				
				if(AmountTo == "")
				{
					AmountTo = "NULL";
				}
																
				/*alert('Month '+ Month);
				alert('Year '+ Year);				
				alert('ExpirationStartDate '+ ExpirationStartDate);
				alert('ExpirationEndDate '+ ExpirationEndDate);
				alert('AmountFrom '+ AmountFrom);
				alert('AmountTo '+ AmountTo);					
				alert('Policy '+ Policy);
				alert('Agency '+ Agency);								
				alert('Account '+ Account);
				alert('State '+ State);	
				alert('Transaction '+ Transaction);
				alert('LOB '+ LOB);
				alert('Vendor ' + Vendor);*/
				
				//var url="CustomReport.aspx?PageName=AccountSummary&FROMDATE="+ ExpirationStartDate + "&TODATE="+ ExpirationEndDate + "&ACCOUNT_ID=" + Account + "&STATE=" + State  + "&LOB=" + LOB +  "&MONTH=" + Month + "&YEAR=" + Year + "&VENDORID=" + Vendor + "&FROMAMT=" + AmountFrom + "&TOAMT=" + AmountTo + "&TRANSTYPE=" + Transaction + "&CustomerID=" + CustomerID + "&PolicyID=" + PolicyID + "&VersionID=" + VersionID; 
				var url="CustomReport.aspx?PageName=AccountSummary&FROMDATE="+ ExpirationStartDate + "&TODATE="+ ExpirationEndDate + "&ACCOUNT_ID=" + Account + "&STATE=" + State  + "&LOB=" + LOB +  "&MONTH=" + Month + "&YEAR=" + Year + "&VENDORID=" + Vendor + "&FROMAMT=" + AmountFrom + "&TOAMT=" + AmountTo + "&TRANSTYPE=" + Transaction + "&CustomerID=" + CustomerID + "&PolicyID=" + PolicyID + "&VersionID=" + VersionID + "&CalledFrom=GLAS"; 
				//alert(url);
				var windowobj = window.open(url,'mywindow','resizable=yes,scrollbar=yes,top=100,left=50')
				windowobj.focus();
								
				/*if (Policy == '')
				{
					alert("Please Specify Policy");
					return
				}
				else
				{
					return;
					/*var url="CustomReport.aspx?PageName=PolicyExpiration&Customerid="+ Customer + "&Agencyid="+ Agency + "&Underwriterid=" + Underwriter + "&LOBid=" + LOB  + "&BillTypeid=" + BillType +  "&PolicyStatusid=" + PolicyStatus + "&ExpirationStartDateid=" + ExpirationStartDate + "&ExpirationEndDateid=" + ExpirationEndDate + "&Addressid=" + Address; 
					var windowobj = window.open(url,'mywindow','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();
				}*/				
			}
			
			function OpenAgencyLookup()
			{			
				var url='<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>';
				OpenLookupWithFunction( url,"AGENCY_ID","Name","hidAGENCY_ID","txtAGENCY_NAME","Agency","Agency Names",'','PostFromLookup()');			
			}
			
			function OpenPolicyLookup()
			{			
				var url='<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>';
				//OpenLookupWithFunction( url,"POLICY_ID","POLICY_NUMBER","hidPolicyID","txtPOLICY_ID","Policy","Policy",'','PostFromLookup()');					
				OpenLookupWithFunction( url,"POL_INFORM","POLICY_NUMBER","hidPolicyID","txtPOLICY_ID","NFS","NFS",'','PostFromLookup()');					
			}
		
			function PostFromLookup()
			{
				//alert(document.getElementById('hidPolicyID').value);
				__doPostBack('hidAGENCY_ID','')
				__doPostBack('hidPolicyID','')
			}						
						
		</script>
	</HEAD>
	<body scroll="yes" onload="top.topframe.main1.mousein = false;findMouseIn();BindEvent();"
		MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			
			<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
			<!-- To add bottom menu ends here --><asp:panel id="Panel1" Runat="server">
				<TABLE class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD class="pageHeader" id="tdClientTop" colSpan="4">
							<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer></TD>
					</TR>
					<TR>
						<TD class="headereffectCenter" colSpan="4">Account Summary Report Selection 
							Criteria</TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">Specific Month Year</TD>
						<TD class="midcolora" colSpan="3">
							<asp:CheckBox id="Checkbox2" runat="server"></asp:CheckBox></TD>
					</TR>
					<TR id="trDate">
						<TD class="midcolora" width="18%">
							<asp:label id="lblExpirationStartDate" runat="server">Start Date</asp:label></TD>
						<TD class="midcolora" width="32%">
							<asp:TextBox id="txtExpirationStartDate" runat="server" MaxLength="10" size="15"></asp:TextBox>
							<asp:hyperlink id="hlkExpirationStartDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgExpirationStartDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR>
							<asp:regularexpressionvalidator id="revExpirationStartDate" Runat="server" Display="Dynamic" ErrorMessage="Please enter Date in 'mm/dd/yyyy' format."
								ControlToValidate="txtExpirationStartDate"></asp:regularexpressionvalidator>
							<asp:CompareValidator id="cmpExpirationDate" Runat="server" Display="Dynamic" ErrorMessage="End Date can't be less than Start Date."
								ControlToValidate="txtExpirationEndDate" ControlToCompare="txtExpirationStartDate" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator></TD>
						<TD class="midcolora" width="18%">
							<asp:label id="lblExpirationEndDate" runat="server">End Date</asp:label></TD>
						<TD class="midcolora" width="32%">
							<asp:TextBox id="txtExpirationEndDate" runat="server" MaxLength="10" size="15"></asp:TextBox>
							<asp:hyperlink id="hlkExpirationEndDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgExpirationEndDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR>
							<asp:regularexpressionvalidator id="revExpirationEndDate" Runat="server" Display="Dynamic" ErrorMessage="Please enter Date in 'mm/dd/yyyy' format."
								ControlToValidate="txtExpirationEndDate"></asp:regularexpressionvalidator></TD>
					</TR>
					<TR id="trSpecificAgency">
						<TD class="midcolora" colSpan="1">Month</TD>
						<TD class="midcolora" colSpan="1">
							<asp:dropdownlist id="cmbMonth" Runat="server">
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
						<TD class="midcolora" colSpan="1">Year</TD>
						<TD class="midcolora" colSpan="1">
							<asp:textbox id="txtyear" runat="server" MaxLength="4" size="5"></asp:textbox><BR>
							<asp:regularexpressionvalidator id="revYear" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
								ControlToValidate="txtyear"></asp:regularexpressionvalidator>
							<asp:rangevalidator id="rngYEAR" Runat="server" Display="Dynamic" ControlToValidate="txtyear" Type="Integer"
								MinimumValue="1950"></asp:rangevalidator></TD>
					</TR>
					<TR>
						<TD class="midcolora" width="18%">
							<asp:Label id="lblFromRange" runat="server">Amount From</asp:Label></TD>
						<TD class="midcolora" width="32%">
							<asp:textbox id="txtFromRange" runat="server" size="15"></asp:textbox><BR>
							<asp:CustomValidator id="csvAMOUNT" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
								ControlToValidate="txtFromRange" ClientValidationFunction="txtAMOUNT_From_Validate"></asp:CustomValidator></TD>
						<TD class="midcolora" width="18%">
							<asp:Label id="lblToRange" runat="server">Amount To</asp:Label></TD>
						<TD class="midcolora" width="32%">
							<asp:textbox id="txtToRange" runat="server" size="15"></asp:textbox><BR>
							<asp:CustomValidator id="csvAMOUNTto" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
								ControlToValidate="txtToRange" ClientValidationFunction="txtAMOUNT_To_Validate"></asp:CustomValidator></TD>
						</TD></TR>
					<TR>
						<TD class="midcolora" colSpan="1">Specific Policy</TD>
						<TD class="midcolora" colSpan="3">
							<asp:CheckBox id="CheckBox1" runat="server"></asp:CheckBox></TD>
					</TR>
					<TR id="trAllPolicies">
						<TD class="midcolora" colSpan="1">All Policies</TD>
						<TD class="midcolora" colSpan="3">
							<asp:CheckBox id="Checkbox3" runat="server"></asp:CheckBox></TD>
					</TR>
					<TR id="trAgency">
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblAgent" runat="server">Agency</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:TextBox id="txtAGENCY_NAME" runat="server" size="40" ReadOnly="True"></asp:TextBox><IMG id="imgAGENCY_NAME" style="CURSOR: hand" onclick="OpenAgencyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
								runat="server"></TD>
					</TR>
					<TR id="trPolicies">
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblPolicies" runat="server">Policies</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstPolicies" runat="server" Height="100px" Width="240px" SelectionMode="Multiple"></asp:ListBox></TD>
					</TR>
					<TR id="trSpecificPolicy">
						<TD class="midcolora" colSpan="1">
							<asp:label id="capPOLICY_ID" runat="server">Policy Number</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:textbox id="txtPOLICY_ID" runat="server" size="14" ReadOnly="False" maxlength="10"></asp:textbox><IMG id="imgSelect" style="CURSOR: hand" onclick="OpenPolicyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
								runat="server">
						</TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblAccount" runat="server">Account Number</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstAccount" runat="server" Height="100px" Width="240px" SelectionMode="Multiple"></asp:ListBox></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblState" runat="server">State</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstStateName" runat="server" Height="100px" Width="240px" SelectionMode="Multiple"></asp:ListBox></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblTransaction" runat="server">Transaction Type</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstTransaction" runat="server" Height="100px" Width="240px" SelectionMode="Multiple"></asp:ListBox></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblLOB" runat="server">Line of Business</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstLOB" runat="server" Height="100px" Width="240px" SelectionMode="Multiple"></asp:ListBox></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblVendor" runat="server">Vendor</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstVendorList" runat="server" Height="100px" Width="240px" SelectionMode="Multiple"></asp:ListBox></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"><cmsb:cmsbutton class="clsbutton" id="btnReport" Runat="server" Text="Display Report"></cmsb:cmsbutton>
						</TD>
					</TR>
				</TABLE>
			</asp:panel><input id="hidAGENCY_ID" type="hidden" name="hidAGENCY_ID" runat="server"><input id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server">
		</form>
	</body>
</HTML>

<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="AccountSummary.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.AccountSummary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>General Ledger Report</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">		
		var calledfrom =0;
		function BindEvent()
		{
			document.getElementById('trSpecificAgency').style.display= "none";				
			document.getElementById('trAccountSpecific').style.display= "none";				
						
			if (document.getElementById("CheckBox2"))
			{
				document.getElementById('CheckBox2').onclick = ShowDate;				
			}
			
			if (document.getElementById("ChkAccountNumber"))
			{
				document.getElementById('ChkAccountNumber').onclick = ShowAccountNumber;				
			}
			
			//colorChange();
			
		}
		function colorChange()
		{
			if(document.getElementById('lstSelOrderBy').options.length == 0)
			{
				document.getElementById('lstSelOrderBy').className = "MandatoryControl";
			}
			else
			{
				document.getElementById('lstSelOrderBy').className = "none";
			}	
		}
		
		function funcValidateSelOrderBy()
		{
			/*if(document.getElementById('lstSelOrderBy').options.length == 0)
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
			}*/
			return true;
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
		
		function ShowAccountNumber()
		{
			if (document.getElementById("ChkAccountNumber"))
			{
				if (document.getElementById("ChkAccountNumber").checked == true)
				{
					document.getElementById('trAccountSpecific').style.display= "inline";
					document.getElementById('trAccountRange').style.display= "none";
				}					
				else
				{	
					document.getElementById('trAccountSpecific').style.display= "none";
					document.getElementById('trAccountRange').style.display= "inline";					
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
				
		function txtSource_From_Validate(objSource,objArgs)
		{
			value = document.getElementById("txtSourceFrom").value;
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
			
			
		function txtAccount_To_Validate(objSource,objArgs)
		{
			value = document.getElementById("txtAccountTo").value;
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
		
		function txtAccount_From_Validate(objSource,objArgs)
		{
			value = document.getElementById("txtAccountFrom").value;
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

			function GetValue(obj,type,defaultValue) {

			   // alert("fhsdf");
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

            //for iTrack 8318 TFS-498 by vivek.....
			function splitSourceNumber() 
            {

              var str=document.getElementById("txtSourceFrom").value;
              var str_array = str.split("-");
              var strConcatenate = "";

              if (str_array[1] != null) 
              {
                 
                   strConcatenate = str_array[0].split(' ').join('') + " " + "-" + " " + str_array[1].split(' ').join('');
              }
               else 
              {
                 
                  strConcatenate = str;
              }

              return strConcatenate;
          }

			function ShowReport(calledfrom)
			{	
				var returnVal = 	funcValidateSelOrderBy();
				if(returnVal == false)
					return Page_IsValid && returnVal;
			
				var valueAmountFrom;
				valueAmountFrom = document.getElementById("txtFromRange").value;
				valueAmountFrom = ReplaceString(valueAmountFrom, ",","");			
				if (valueAmountFrom != "")
				{
					if (isNaN(valueAmountFrom))
					{					
						alert("Please enter numeric value for Amount From");
						return					
					}
				}
				
				var valueAccountTo;
				valueAccountTo = document.getElementById("txtToRange").value;
				valueAccountTo = ReplaceString(valueAccountTo, ",","");			
				if (valueAccountTo != "")
				{
					if (isNaN(valueAccountTo))
					{					
						alert("Please enter numeric value for Amount To");
						return					
					}
				}			
					
				var valueAccountFrom;
				valueAccountFrom = document.getElementById("txtAccountFrom").value;
				valueAccountFrom = ReplaceString(valueAccountFrom, ",","");			
				if (valueAccountFrom != "")
				{
					if (isNaN(valueAccountFrom))
					{					
						alert("Please enter numeric value for Account From");
						return					
					}
				}
				
				var valueAccountTo;
				valueAccountTo = document.getElementById("txtAccountTo").value;
				valueAccountTo = ReplaceString(valueAccountTo, ",","");			
				if (valueAccountTo != "")
				{
					if (isNaN(valueAccountTo))
					{					
						alert("Please enter numeric value for Account To");
						return					
					}
				}	
				
				var valueSourceFrom;
				valueSourceFrom = document.getElementById("txtSourceFrom").value;
				valueSourceFrom = ReplaceString(valueSourceFrom, ",","");			
				if (valueSourceFrom != "") {

                    //code commented for 8318 by vivek  .....
					//if (isNaN(valueSourceFrom))
					//{					
					//	alert("Please enter numeric value for Source Number");
					//	return					
					//}
				}
							
				var Month="";
				var Year="";
				var ExpirationStartDate="";
				var ExpirationEndDate="";
				var	State="";
				var LOB="";
				var Vendor="";
				var Transaction="";
				var AmountFrome="";
				var AmountTo="";
				var PolicyID="";
				var CustomerID="";
				var VersionID="";
				var Agency="";	
				var Account="0";
				var AccountFrom="";
				var AccountTo="";
				var SourceFrom="";
				var UpdatedFrom="";
				var OrderBy = "";		
				var ClaimNumber = "";									
				
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
				
				if(document.getElementById('ChkAccountNumber').checked == true)
				{
					Account	= GetValue(document.getElementById('lstAccount'),'D');
				}				
				
                else
				{
					AccountFrom	= GetValue(document.getElementById('txtAccountFrom'),'T');
					AccountTo	= GetValue(document.getElementById('txtAccountTo'),'T');
				}
											
				//Account				= GetValue(document.getElementById('lstAccount'),'D');
				AmountFrom			= GetValue(document.getElementById('txtFromRange'),'T');
				AmountTo			= GetValue(document.getElementById('txtToRange'),'T');
				
				AmountFrom = ReplaceString(AmountFrom, ",","");
				AmountTo = ReplaceString(AmountTo, ",","");
				
				Vendor			    = GetValue(document.getElementById('lstVendorList'),'D');
				
				var PolicyNumber="";
				//PolicyNumber		    = GetValue(document.getElementById('lstPolicyNumber'),'D');
				//PolicyNumber = ReplaceString(PolicyNumber, ",","','");
				PolicyNumber		= GetValue(document.getElementById('txtPolicyNo'),'T');
				ClaimNumber		    = GetValue(document.getElementById('txtClaimNo'),'T');
				//Added for iTrack 8313 by vivek.......
				//SourceFrom = GetValue(document.getElementById('txtSourceFrom'), 'T');
				SourceFrom          = splitSourceNumber();
				UpdatedFrom			= GetValue(document.getElementById('cmbUpdatedFrom'),'T');
				
				//funcValidateSelOrderBy();				
				// Get Order By						
				var LstLen = document.getElementById('lstSelOrderBy').options.length;
				var strOrderBy = new String();
				for(var i = 0;i<LstLen;i++)
					{strOrderBy = strOrderBy + document.getElementById('lstSelOrderBy').options[i].value + ',';}
				
				
				OrderBy = strOrderBy.substr(0,strOrderBy.length-1); // remove ',' from last
				
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
				
				if(AccountFrom == "")
				{
					AccountFrom = "NULL";
				}
				
				if(AccountTo == "")
				{
					AccountTo = "NULL";
				}	
								
				if(Vendor == "0")
				{
					Vendor = "NULL";
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
				
				if(SourceFrom == "")
				{
					SourceFrom = "NULL";
				}
				
				if(UpdatedFrom == "0")
				{
					UpdatedFrom = "NULL";
				}
				
				if(OrderBy =="")
				{
					OrderBy = "SOURCE_TRAN_DATE1";
				}
				
				if(PolicyNumber == "0" ||PolicyNumber == "")
				{					
					PolicyNumber = "NULL";
				}
				if(ClaimNumber=="0"||ClaimNumber=="")
				{
				ClaimNumber="NULL";
				}
				
				
				State = "NULL";
				LOB = "NULL";
				Transaction = "NULL";
				CustomerID = "NULL";					
				PolicyID = "NULL";
				VersionID = "NULL";
				SourceTo = "NULL";
			
				//manipulated for 8114	for CLAIM_NUMBER		
				if (calledfrom == "S")
				{
				
					//var url="CustomReport.aspx?PageName=AccountSummary&FROMSOURCE=" + SourceFrom + "&TOSOURCE=" + SourceTo + "&FROMDATE="+ ExpirationStartDate + "&TODATE="+ ExpirationEndDate + "&ACCOUNT_ID=" + Account + "&UPDATED_FROM=" + UpdatedFrom + "&STATE=" + State  + "&LOB=" + LOB +  "&MONTH=" + Month + "&YEAR=" + Year + "&VENDORID=" + Vendor + "&CustomerID=" + CustomerID + "&FROMAMT=" + AmountFrom + "&TOAMT=" + AmountTo + "&TRANSTYPE=" + Transaction + "&PolicyID=" + PolicyID + "&FROMACT=" + AccountFrom + "&TOACT=" + AccountTo  + "&CalledFrom=GLAS" + "&ORDERBY=" + OrderBy;
					//var url="CustomReport.aspx?PageName=AccountSummary&FROMSOURCE=" + SourceFrom + "&TOSOURCE=" + SourceTo + "&FROMDATE="+ ExpirationStartDate + "&TODATE="+ ExpirationEndDate + "&ACCOUNT_ID=" + Account + "&UPDATED_FROM=" + UpdatedFrom + "&STATE=" + State  + "&LOB=" + LOB +  "&MONTH=" + Month + "&YEAR=" + Year + "&VENDORID=" + Vendor + "&CustomerID=" + CustomerID + "&FROMAMT=" + AmountFrom + "&TOAMT=" + AmountTo + "&TRANSTYPE=" + Transaction + "&PolicyID=" + PolicyID + "&FROMACT=" + AccountFrom + "&TOACT=" + AccountTo  + "&CalledFrom=GLAS" + "&ORDERBY=NULL";
				    var url = "CustomReport.aspx?PageName=AccountSummary&FROMSOURCE=" + SourceFrom + "&TOSOURCE=" + SourceTo + "&FROMDATE=" + ExpirationStartDate + "&TODATE=" + ExpirationEndDate + "&ACCOUNT_ID=" + Account + "&UPDATED_FROM=" + UpdatedFrom + "&STATE=" + State + "&LOB=" + LOB + "&MONTH=" + Month + "&YEAR=" + Year + "&VENDORID=" + Vendor + "&CustomerID=" + CustomerID + "&FROMAMT=" + AmountFrom + "&TOAMT=" + AmountTo + "&TRANSTYPE=" + Transaction + "&PolicyID=" + PolicyID + "&FROMACT=" + AccountFrom + "&TOACT=" + AccountTo + "&CalledFrom=GLAS" + "&ORDERBY=NULL" + "&POLICY_NUMBER=" + PolicyNumber + "&CLAIM_NUMBER=" + ClaimNumber;
				   	var windowobj = window.open(url,'AccountSummaryS','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();	
				}
				
				if (calledfrom == "T")
				{			
					
					//var url="CustomReport.aspx?PageName=AccountSummary&FROMSOURCE=" + SourceFrom + "&TOSOURCE=" + SourceTo + "&FROMDATE="+ ExpirationStartDate + "&TODATE="+ ExpirationEndDate + "&ACCOUNT_ID=" + Account + "&UPDATED_FROM=" + UpdatedFrom + "&STATE=" + State  + "&LOB=" + LOB +  "&MONTH=" + Month + "&YEAR=" + Year + "&VENDORID=" + Vendor + "&CustomerID=" + CustomerID + "&FROMAMT=" + AmountFrom + "&TOAMT=" + AmountTo + "&TRANSTYPE=" + Transaction + "&PolicyID=" + PolicyID + "&FROMACT=" + AccountFrom + "&TOACT=" + AccountTo + "&CalledFrom=GLAT" + "&ORDERBY=" + OrderBy;
					var url="CustomReport.aspx?PageName=AccountSummary&FROMSOURCE=" + SourceFrom + "&TOSOURCE=" + SourceTo + "&FROMDATE="+ ExpirationStartDate + "&TODATE="+ ExpirationEndDate + "&ACCOUNT_ID=" + Account + "&UPDATED_FROM=" + UpdatedFrom + "&STATE=" + State  + "&LOB=" + LOB +  "&MONTH=" + Month + "&YEAR=" + Year + "&VENDORID=" + Vendor + "&CustomerID=" + CustomerID + "&FROMAMT=" + AmountFrom + "&TOAMT=" + AmountTo + "&TRANSTYPE=" + Transaction + "&PolicyID=" + PolicyID + "&FROMACT=" + AccountFrom + "&TOACT=" + AccountTo + "&CalledFrom=GLAT" + "&ORDERBY=" + OrderBy + "&POLICY_NUMBER=" + PolicyNumber +"&CLAIM_NUMBER=" + ClaimNumber;
					var windowobj = window.open(url,'AccountSummaryT','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();	
				}	
				
				if (calledfrom == "N")
				{					
					var url="CustomReport.aspx?PageName=AccountSummary&FROMSOURCE=" + SourceFrom + "&TOSOURCE=" + SourceTo + "&FROMDATE="+ ExpirationStartDate + "&TODATE="+ ExpirationEndDate + "&ACCOUNT_ID=" + Account + "&UPDATED_FROM=" + UpdatedFrom + "&STATE=" + State  + "&LOB=" + LOB +  "&MONTH=" + Month + "&YEAR=" + Year + "&VENDORID=" + Vendor + "&CustomerID=" + CustomerID + "&FROMAMT=" + AmountFrom + "&TOAMT=" + AmountTo + "&TRANSTYPE=" + Transaction + "&PolicyID=" + PolicyID + "&FROMACT=" + AccountFrom + "&TOACT=" + AccountTo + "&CalledFrom=GLAN" + "&ORDERBY=" + OrderBy;
					var windowobj = window.open(url,'AccountSummaryN','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();	
				}	
				
															
			}	
			
		function FormatAmount(txtAmount)
		{
			if (txtAmount.value != "")
			{
				amt = txtAmount.value;
				
				amt = ReplaceAll(amt,".","");
				
				if (amt.length == 1)
					amt = amt + "0";
				
				if ( ! isNaN(amt))
				{
					
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					
					txtAmount.value = InsertDecimal(amt);
				}
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
		//Edited for 8114	
		function OpenPolicyLookup()
		{
			
			var url='<%=URL%>';
			if(document.getElementById('txtClaimNo').value=="")
			{
			
			OpenLookupWithFunction(url,'POL_INFORM','POL_INFORM','txtPolicyNo','hidPOLICYINFO','ATRPOLLOOKUP','Policy','','splitPolicy()');			
			}
			else
			if(document.getElementById('txtClaimNo').value!="")
			{
			
			var claim_number="'"+document.getElementById('txtClaimNo').value+"'";
			//alert("claim_number"+claim_number);
			//var policy_ID=document.getElementById('hidPolicyID').value	;
			//var policy_VerID=	document.getElementById('hidPolicyVerID').value	;
			//var Customer_ID=	document.getElementById('hidCustomerID').value	;
			
			OpenLookupWithFunction(url,'POL_INFORM','POL_INFORM','txtPolicyNo','hidPOLICYINFO','ARRECLAIMPORT','Policy','@claim_number='+claim_number,'splitPolicyClaim()');			
			}
			
		}
		//Added for 8114		    
		function OpenClaimLookup()	
		{
			var url='<%=URL%>';
			if(document.getElementById('txtPolicyNo').value=="")
			{
			
			OpenLookupWithFunction(url,'CLM_INFORM','CLM_INFORM','txtClaimNo','hidCLAIM_POLICY_NUMBER','CLAIMPORT','Claim','','splitClaim()');			
			}
			else
			if((document.getElementById('txtPolicyNo').value)!="")
			{
				//var policy_ID=document.getElementById('hidPolicyID').value;
				//var policy_VerID=document.getElementById('hidPolicyVerID').value;
				//var Customer_ID=document.getElementById('hidCustomerID').value;
				var policy_number= "'" + document.getElementById('txtPolicyNo').value + "'";
				//alert("policy_number"+policy_number);
				OpenLookupWithFunction(url,'CLM_INFORM','txtPolicyNo','hidCLAIM_POLICY_NUMBER','txtClaimNo','POLCLAIMPORT','Claim','@policy_number='+policy_number,'splitClaim()');	
			}
				 
		}	
		//newely added for 8114 BY VIVEK ON 19 MAY 2011
		function splitPolicyClaim()
		{
		      
		        var PolicyDetails = document.getElementById('hidPOLICYINFO').value.split('~');
		       
		        document.getElementById('hidCustomerID').value	=	PolicyDetails[0];
				document.getElementById('hidPolicyID').value	=	PolicyDetails[1];
				document.getElementById('hidPolicyVerID').value	=	PolicyDetails[2];
				document.getElementById('hidPOLICYINFO').value	=	PolicyDetails[3];
				document.getElementById('txtPolicyNo').value	=	PolicyDetails[3];
				
		}
		function splitPolicy()
		{
		      
		        var PolicyDetails = document.getElementById('hidPOLICYINFO').value.split('~');
		      	document.getElementById('hidPolicyID').value	=	PolicyDetails[0];
				document.getElementById('hidPolicyVerID').value	=	PolicyDetails[1];
				document.getElementById('hidCustomerID').value	=	PolicyDetails[2];
				document.getElementById('hidPOLICYINFO').value	=	PolicyDetails[3];
				document.getElementById('txtPolicyNo').value	=	PolicyDetails[3];
				
		}
		// added for 8114 BY VIVEK
		function splitClaim()
		{
				var ClaimPolicyDetails = document.getElementById('hidCLAIM_POLICY_NUMBER').value.split('~');
				
				document.getElementById('hidCustomerID').value	=	ClaimPolicyDetails[0];
				document.getElementById('hidPolicyID').value	=	ClaimPolicyDetails[1];
				document.getElementById('hidPolicyVerID').value	=	ClaimPolicyDetails[2];
				document.getElementById('hidPOLICYINFO').value	=	ClaimPolicyDetails[3];
				document.getElementById('txtPolicyNo').value	=	ClaimPolicyDetails[3];
				document.getElementById('hidCLAIM_ID').value	=	ClaimPolicyDetails[4];
				document.getElementById('txtClaimNo').value		=	ClaimPolicyDetails[4];
			
				
		}
		
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" scroll="yes" onload="top.topframe.main1.mousein = false;findMouseIn();BindEvent();ApplyColor();ChangeColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>
			<!-- To add bottom menu -->
			<!-- To add bottom menu ends here --><asp:panel id="Panel1" Runat="server">
				<TABLE class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD class="pageHeader" id="tdClientTop" colSpan="4"><webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer></TD></TR>
					<TR>
						<TD class="headereffectCenter" colSpan="4">
							<SCRIPT language="javascript">
							
								/*if (calledfrom == "S")
								{
									document.write("General Ledger Account Summary Report Selection Criteria");
								}
								else
								{
									document.write("General Ledger Account Transaction Report Selection Criteria");
								}*/
								document.write("General Ledger Report Selection Criteria");
							</SCRIPT>
						</TD></TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD></TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD></TR>
					<TR>
						<TD class="midcolora" colSpan="1">Specific Month Year</TD>
						<TD class="midcolora" colSpan="3"><asp:CheckBox id="Checkbox2" runat="server"></asp:CheckBox></TD></TR>
					<TR id="trDate">
						<TD class="midcolora" width="18%"><asp:label id="lblExpirationStartDate" runat="server">From Date</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:TextBox id="txtExpirationStartDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkExpirationStartDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgExpirationStartDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR><asp:regularexpressionvalidator id="revExpirationStartDate" Runat="server" ControlToValidate="txtExpirationStartDate" ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator><asp:CompareValidator id="cmpExpirationDate" Runat="server" ControlToValidate="txtExpirationEndDate" ErrorMessage="End Date can't be less than Start Date." Display="Dynamic" Operator="GreaterThanEqual" Type="Date" ControlToCompare="txtExpirationStartDate"></asp:CompareValidator></TD>
						<TD class="midcolora" width="18%"><asp:label id="lblExpirationEndDate" runat="server">To Date</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:TextBox id="txtExpirationEndDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkExpirationEndDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgExpirationEndDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR><asp:regularexpressionvalidator id="revExpirationEndDate" Runat="server" ControlToValidate="txtExpirationEndDate" ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator></TD></TR>
					<TR id="trSpecificAgency">
						<TD class="midcolora" colSpan="1">Month</TD>
						<TD class="midcolora" colSpan="1"><asp:dropdownlist id="cmbMonth" Runat="server">
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
						<TD class="midcolora" colSpan="1"><asp:textbox id="txtyear" runat="server" size="5" MaxLength="4"></asp:textbox><BR><asp:regularexpressionvalidator id="revYear" runat="server" ControlToValidate="txtyear" ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator><asp:rangevalidator id="rngYEAR" Runat="server" ControlToValidate="txtyear" Display="Dynamic" Type="Integer" MinimumValue="1950"></asp:rangevalidator></TD></TR>
					<TR>
						<TD class="midcolora" colSpan="1"><asp:label id="lblUpdatedFrom" runat="server">Updated From</asp:label></TD>
						<TD class="midcolora" colSpan="3"><asp:DropDownList id="cmbUpdatedFrom" runat="server" Height="100px">
								<asp:ListItem Value="0" Selected="True">All</asp:ListItem>
								<asp:ListItem Value="C">Check</asp:ListItem>
								<asp:ListItem Value="V">Void Check</asp:ListItem>
								<asp:ListItem Value="L">Claim</asp:ListItem>
								<asp:ListItem Value="D">Deposit</asp:ListItem>
								<asp:ListItem Value="F">Fees</asp:ListItem>
								<asp:ListItem Value="P">Premium Posting</asp:ListItem>
								<asp:ListItem Value="J">Journal</asp:ListItem>
								<asp:ListItem Value="I">Invoice</asp:ListItem>
							</asp:DropDownList></TD></TR>
					<TR>
						<TD class="midcolora" width="18%"><asp:Label id="lblFromRange" runat="server">Amount From</asp:Label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtFromRange" style="TEXT-ALIGN: right" runat="server" size="15"></asp:textbox><BR><asp:CustomValidator id="csvAMOUNT" runat="server" ControlToValidate="txtFromRange" ErrorMessage="RegularExpressionValidator" Display="Dynamic" ClientValidationFunction="txtAMOUNT_From_Validate"></asp:CustomValidator></TD>
						<TD class="midcolora" width="18%"><asp:Label id="lblToRange" runat="server">Amount To</asp:Label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtToRange" style="TEXT-ALIGN: right" runat="server" size="15"></asp:textbox><BR><asp:CustomValidator id="csvAMOUNTto" runat="server" ControlToValidate="txtToRange" ErrorMessage="RegularExpressionValidator" Display="Dynamic" ClientValidationFunction="txtAMOUNT_To_Validate"></asp:CustomValidator></TD></TR>
					<TR>
						<TD class="midcolora" colSpan="1"><asp:Label id="lblSourceFrom" runat="server">Source Number</asp:Label></TD>
						<TD class="midcolora" colSpan="3"><asp:textbox id="txtSourceFrom" runat="server" size="15"></asp:textbox><BR><asp:CustomValidator id="csvSourceFrom" runat="server" ControlToValidate="txtToRange" ErrorMessage="RegularExpressionValidator" Display="Dynamic" ClientValidationFunction="txtSource_From_Validate"></asp:CustomValidator></TD></TR>
					<TR>
						<TD class="midcolora" colSpan="1">Specific Account Number</TD>
						<TD class="midcolora" colSpan="3"><asp:CheckBox id="ChkAccountNumber" runat="server"></asp:CheckBox></TD></TR>
					<TR id="trAccountRange">
						<TD class="midcolora" width="18%"><asp:Label id="lblAccountFrom" runat="server">Account From</asp:Label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtAccountFrom" runat="server" size="15"></asp:textbox><BR><asp:CustomValidator id="csvAccountFrom" runat="server" ControlToValidate="txtAccountFrom" ErrorMessage="RegularExpressionValidator" Display="Dynamic" ClientValidationFunction="txtAccount_From_Validate"></asp:CustomValidator></TD>
						<TD class="midcolora" width="18%"><asp:Label id="lblAccountTo" runat="server">Account To</asp:Label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtAccountTo" runat="server" size="15"></asp:textbox><BR><asp:CustomValidator id="csvAccountTo" runat="server" ControlToValidate="txtAccountTo" ErrorMessage="RegularExpressionValidator" Display="Dynamic" ClientValidationFunction="txtAccount_To_Validate"></asp:CustomValidator></TD></TR>
					<TR id="trAccountSpecific">
						<TD class="midcolora" colSpan="1"><asp:label id="lblAccount" runat="server">Account Number</asp:label></TD>
						<TD class="midcolora" colSpan="3"><asp:ListBox id="lstAccount" runat="server" Height="100px" SelectionMode="Multiple" Width="240px"></asp:ListBox></TD></TR>
					<TR>
					
						<TD class="midcolora" colSpan="1"><asp:label id="lblVendor" runat="server">Vendor</asp:label></TD>
						<TD class="midcolora" colSpan="1"><asp:ListBox id="lstVendorList" runat="server" Height="100px" SelectionMode="Multiple" Width="240px"></asp:ListBox></TD>
						<%-- For Swarup Itrack # 8114 By vivek--%>
						<TD class="midcolora" colSpan="1"><asp:label id="lblPolicyNumber" runat="server">Policy No.</asp:label><asp:textbox id="txtPolicyNo" Runat="server" MaxLength="8" size="10"></asp:textbox>
						<span id="spnPOLICY_NO" runat="server"><IMG id="imgPOLICY_NO" style="CURSOR: hand" onclick="OpenPolicyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
					runat="server"></span></TD>
						<%-- Commented by Swarup Itrack # 3194
						<TD class="midcolora" colSpan="1"><asp:ListBox id="lstPolicyNumber" runat="server" Height="100px" SelectionMode="Multiple" Width="200px"></asp:ListBox></TD>--%>
						<TD class="midcolora" colSpan="1"><asp:label id="lblClaimNumber" runat="server">Claim No.</asp:label><asp:textbox id="txtClaimNo" Runat="server" MaxLength="9" size="12"></asp:textbox>
						<span id="spnCLAIM_NO" runat="server"><IMG id="imgCLAIM_NO" style="CURSOR: hand" onclick="OpenClaimLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
					runat="server"></span></td>
					</TR>
					<!-- order by fields -->
					<TR>
						<TD class="midcolora" colSpan="1"><asp:label id="lblOrderBy" runat="server">Order By</asp:label><!--<SPAN class="mandatory" id="spnOrderBy">*</SPAN>--></TD>
						<TD class="midcolora" colSpan="1"><asp:ListBox id="lstOrderBy" runat="server" Height="65px" SelectionMode="Multiple" Width="110px" AutoPostBack="false">
								<asp:ListItem Value="ACCOUNT">Account</asp:ListItem>
								<asp:ListItem Value="SOURCE_TRAN_DATE1">Date</asp:ListItem>
								<asp:ListItem Value="UPDATED_FROM">Updated From</asp:ListItem>
								<asp:ListItem Value="SOURCE_NUM">Source Number</asp:ListItem></asp:ListBox></TD>
						<TD class="midcolora" vAlign="middle" colSpan="1">&nbsp;<asp:Button id="btnSel" Runat="server" Text=">>"></asp:Button><BR><BR>
							&nbsp;<asp:Button id="btnDeSel" Runat="server" Text="<<"></asp:Button>
						</TD>
						<TD class="midcolora" colSpan="1"><asp:ListBox id="lstSelOrderBy" onblur="funcValidateSelOrderBy" runat="server" Height="65px" SelectionMode="Multiple" Width="110px" AutoPostBack="False" onchange="funcValidateSelOrderBy">
							</asp:ListBox><br><asp:customvalidator id="csvSelOrderBy" Runat="server" ControlToValidate="lstSelOrderBy" Display="Dynamic" ClientValidationFunction="funcValidateSelOrderBy" Enabled="False"></asp:customvalidator><SPAN id="spnSelOrderBy" style="DISPLAY: none; COLOR: red">Please 
								select Order
								By.</SPAN> </TD></TR>
					<TR>
						<TD class="midcolorc" colSpan="2"><cmsb:cmsbutton class="clsbutton" id="btnAccSumm" Runat="server" Text="Account Summary Report"></cmsb:cmsbutton>
						</TD>
						<!--<TD class="midcolorc" colSpan="2"><cmsb:cmsbutton class="clsbutton" id="btnCombTran" Runat="server" Text="Combined Transaction Report"></cmsb:cmsbutton>
						</TD>-->
						<TD class="midcolorc" colSpan="2"><cmsb:cmsbutton class="clsbutton" id="btnTran" Runat="server" Text="Transaction Report"></cmsb:cmsbutton>
						</TD></TR></TABLE>
			</asp:panel>
							<input id="hidPolicyID" type="hidden" value="0" name="hidPolicyID" runat="server">
							<input id="hidPolicyVerID" type="hidden" value="0" name="hidPolicyVerID" runat="server">
							<input id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
							
							<input id="hidPOLICYINFO" type="hidden" name="hidPOLICYINFO" runat="server">
							<input id="hidCLAIM_POLICY_NUMBER" type="hidden" name="hidCLAIM_POLICY_NUMBER" runat="server">
							<input id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
						
			</form>
	</body>
</HTML>

<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="ClaimStatus.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.ClaimStatus" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Claim Status</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		var url = '<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>'
		var systemId = "'<%=strSystemID%>'";
		var carriersystemId = "'<%=strCarrierSystemID%>'";
		var agency_id ="'<%=strAgencyID%>'";
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
					//var tmdate = sVal.split('/');
					//if(tmdate.length >= 3)
					//	sVal = tmdate[1] + '/' + tmdate[0] + '/' + tmdate[2];
				}
				//alert(sVal);				
				return sVal;
			}
			
			
		function ShowReport()
			{			
				//Added for Itrack Issue 6070 on 8 July 09
				if (!funcValidateSelOrderBy(document.getElementById('csvSelOrderBy'),document.getElementById('csvSelOrderBy')))
					return false;
					
				var Customer="";
				var Agency="";
				var Underwriter="";
				var LOB="";
				var StartDate="";
				var EndDate="";
				
				var CustomerID = "";						
				var PolicyID = "";
				var VersionID = "";
				var OrderBy = "";
											
				StartDate	= GetValue(document.getElementById('txtStartDate'),'T');
				EndDate		= GetValue(document.getElementById('txtEndDate'),'T');
				
				Insured = document.getElementById('hidINSURED_ID').value
				//PolicyID = arrINS[0];
				
				Agency				= document.getElementById('hidAGENCY_ID').value;
				Claim				= document.getElementById('hidCLAIM_POLICY_NUMBER').value
				//Policy				= document.getElementById('hidPOLICYINFO').value
				Combination = document.getElementById('hidPOLICYINFO').value;
				
				// Get Order --- //Added for Itrack Issue 6070 on 8 July 09				
				var LstLen = document.getElementById('lstSelOrderBy').options.length;
				var strOrderBy = new String();
				for(var i = 0;i<LstLen;i++)
				{
					if(document.getElementById('lstSelOrderBy').options[i].value == "LOSS_DATE" || document.getElementById('lstSelOrderBy').options[i].value == "CLAIM_NUMBER")
					  strOrderBy = strOrderBy + document.getElementById('lstSelOrderBy').options[i].value + ' DESC,';
					else
					  strOrderBy = strOrderBy + document.getElementById('lstSelOrderBy').options[i].value + ',';
				}
					
				OrderBy = strOrderBy.substr(0,strOrderBy.length-1); // remove ',' from last
				//Added uptil here
				if (Combination == "")
				{
					//alert("Please Select a Policy");
					//return;
					CustomerID = "NULL";						
					PolicyID = "NULL";
					VersionID = "NULL";
				}
									
				if(Combination !='')
					{
						var arrCPV=Combination.split("^");
						CustomerID = arrCPV[1];						
						PolicyID = arrCPV[0];
						VersionID = arrCPV[3];	
						//alert(CustomerID);alert(PolicyID);alert(VersionID);					
					}
					
				if (Insured == '')
				{
					Insured="NULL"
				}
				if (Claim == '')
				{
					Claim="NULL"
				}
				if (StartDate == '')
				{
					StartDate="NULL"
				}
				if (EndDate == '')
				{
					EndDate="NULL"
				}
				/*if (Agency!= '')
				{*/					
					//Done for Itrack Issue 6070 on 8 July 09
					//var url="CustomReport.aspx?PageName=ClaimStatus&Agencyid="+ Agency + "&Insuredid=" + Insured + "&Claimid=" + Claim  + "&CustomerID=" + CustomerID + "&PolicyID=" + PolicyID + "&VersionID=" + VersionID +  "&StartDateid=" + StartDate + "&EndDateid=" + EndDate; 
					var url="CustomReport.aspx?PageName=ClaimStatus&Agencyid="+ Agency + "&Insuredid=" + Insured + "&Claimid=" + Claim  + "&CustomerID=" + CustomerID + "&PolicyID=" + PolicyID + "&VersionID=" + VersionID +  "&StartDateid=" + StartDate + "&EndDateid=" + EndDate + "&OrderBy=" + OrderBy; 
					//alert(url);
					var windowobj = window.open(url,'ClaimStatus','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();
				/*}
				else
				{
					alert("Please Select Agency");
				}*/
				
								
			}
		function OpenInsuredLookup()
		{
			var agency_id = document.getElementById('hidAGENCY_ID').value;
			var insPolicyID = "";
			var PolCombination = document.getElementById('hidPOLICYINFO').value;
			var claim_id =  document.getElementById('hidCLAIM_POLICY_NUMBER').value;
			if(agency_id!="")
			{
				if(claim_id!="" && PolCombination != "")
				{
					var arrCPV=PolCombination.split("^");
					insPolicyID = arrCPV[0];
					OpenLookupWithFunction( url,"NAME","NAME","hidINSURED_ID","lstInsuredName","InsuredClaimantClaimPol","Insured","@AGENCY_ID="+agency_id+";@POLICY_ID="+insPolicyID+";@CLAIM="+claim_id,'');	
				}
						
				else if(claim_id!="" && PolCombination == "")
				{
					OpenLookupWithFunction( url,"NAME","NAME","hidINSURED_ID","lstInsuredName","InsuredClaimantClaim","Insured","@AGENCY_ID="+agency_id+";@CLAIM="+claim_id,'');	
				}
				else if (claim_id=="" && PolCombination == "")
				{
						OpenLookupWithFunction( url,"NAME","NAME","hidINSURED_ID","lstInsuredName","InsuredClaimant","Insured","@AGENCY_ID="+agency_id,'');	
				}
									
				else if(claim_id=="" && PolCombination !='')
				{
					var arrCPV=PolCombination.split("^");
					insPolicyID = arrCPV[0];
					OpenLookupWithFunction( url,"NAME","NAME","hidINSURED_ID","lstInsuredName","InsuredClaimantPol","Insured","@AGENCY_ID="+agency_id+";@POLICY_ID="+insPolicyID,'');	
				}
			}
			else
			{
				if(claim_id!="" && PolCombination != "")
				{
					var arrCPV=PolCombination.split("^");
					insPolicyID = arrCPV[0];
					OpenLookupWithFunction( url,"NAME","NAME","hidINSURED_ID","lstInsuredName","InsuredClaimantClaimPolWAgency","Insured","@POLICY_ID="+insPolicyID+";@CLAIM="+claim_id,'');	
				}
						
				else if(claim_id!="" && PolCombination == "")
				{
					OpenLookupWithFunction( url,"NAME","NAME","hidINSURED_ID","lstInsuredName","InsuredClaimantClaimWAgency","Insured","@CLAIM="+claim_id,'');	
				}
				else if (claim_id=="" && PolCombination == "")
				{
						OpenLookupWithFunction( url,"NAME","NAME","hidINSURED_ID","lstInsuredName","InsuredClaimantWAgency","Insured",'','');	
				}
									
				else if(claim_id=="" && PolCombination !='')
				{
					var arrCPV=PolCombination.split("^");
					insPolicyID = arrCPV[0];
					OpenLookupWithFunction( url,"NAME","NAME","hidINSURED_ID","lstInsuredName","InsuredClaimantPolWAgency","Insured","@POLICY_ID="+insPolicyID,'');	
				}
			}
		}
		function OpenAgencyLookup()
		{			
			OpenLookupWithFunction( url,"AGENCY_ID","Name","hidAGENCY_ID","txtAGENCY_NAME","Agency","Agency Names",'','PostFromLookup()');			
		}
		function OpenPolicyLookup()
		{
			/*if(systemId != carriersystemId)
			{
				OpenLookupWithFunction(url,'POL_INFORM','POLICY_NUMBER','hidPOLICYINFO','lstPolicy','ClaimPolicy','Policy',"@AGENCY_ID="+systemId,'');
			}
			else
			{*/
			var name = document.getElementById('hidINSURED_ID').value;
				var agency_id = document.getElementById('hidAGENCY_ID').value;
				var claim_id =  document.getElementById('hidCLAIM_POLICY_NUMBER').value;
				if(agency_id!="")
				{		
					if(claim_id!="")
							OpenLookupWithFunction(url,'POL_INFORM','POLICY_NUMBER','hidPOLICYINFO','lstPolicy','ClaimPolicyclaim','Policy',"@AGENCY_ID="+agency_id+";@NAME="+name+";@CLAIM="+claim_id,'');
					else
							OpenLookupWithFunction(url,'POL_INFORM','POLICY_NUMBER','hidPOLICYINFO','lstPolicy','ClaimPolicy','Policy',"@AGENCY_ID="+agency_id+";@NAME="+name,'');
				}
				else
				{
					if(claim_id!="")
							OpenLookupWithFunction(url,'POL_INFORM','POLICY_NUMBER','hidPOLICYINFO','lstPolicy','ClaimPolicyclaimWAgency','Policy',"@NAME="+name+";@CLAIM="+claim_id,'');
					else
							OpenLookupWithFunction(url,'POL_INFORM','POLICY_NUMBER','hidPOLICYINFO','lstPolicy','ClaimPolicyWAgency','Policy',"@NAME="+name,'');
				}

			//}
				
		}
		function OpenClaimLookup(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction)
		{
			var agency_id = document.getElementById('hidAGENCY_ID').value;
			var name = document.getElementById('hidINSURED_ID').value;
			var PolCombination = document.getElementById('hidPOLICYINFO').value;
				if(agency_id!="")
				{
					if (PolCombination == "")
					{
							OpenLookupWithFunction(url,'CLAIM_ID','CLAIM_NUMBER','hidCLAIM_POLICY_NUMBER','lstClaim','ClaimCustomer4Agency','Claim',"@AGENCY_ID="+agency_id+";@NAME="+name,'');
					}
										
					if(PolCombination !='')
					{
						var arrCPV=PolCombination.split("^");
						insPolicyID = arrCPV[0];
						insCustomerId = arrCPV[1];
						OpenLookupWithFunction(url,'CLAIM_ID','CLAIM_NUMBER','hidCLAIM_POLICY_NUMBER','lstClaim','ClaimCustomer4AgencyPol','Claim',"@AGENCY_ID="+agency_id+";@NAME="+name+";@POLICY_ID="+insPolicyID+";@CUSTOMER_ID="+insCustomerId,'');
					}
				}
				else
				{
					if (PolCombination == "")
					{
							OpenLookupWithFunction(url,'CLAIM_ID','CLAIM_NUMBER','hidCLAIM_POLICY_NUMBER','lstClaim','ClaimCustomer4AgencyWAgency','Claim',"@NAME="+name,'');
					}
										
					if(PolCombination !='')
					{
						var arrCPV=PolCombination.split("^");
						insPolicyID = arrCPV[0];
						insCustomerId = arrCPV[1];
						OpenLookupWithFunction(url,'CLAIM_ID','CLAIM_NUMBER','hidCLAIM_POLICY_NUMBER','lstClaim','ClaimCustomer4AgencyPolWAgency','Claim',"@NAME="+name+";@POLICY_ID="+insPolicyID+";@CUSTOMER_ID="+insCustomerId,'');
					}

				}
				
			//}
		}
		function PostFromLookup()
		{
			/*Post back the form */	
			document.getElementById('lstInsuredName').value = '';	
			document.getElementById('lstPolicy').value = '';
			document.getElementById('lstClaim').value = '';
			document.getElementById('txtStartDate').value = '';
			document.getElementById('txtEndDate').value = '';	
				
			document.getElementById('hidINSURED_ID').value = '';	
			document.getElementById('hidPOLICYINFO').value = '';
			document.getElementById('hidCLAIM_POLICY_NUMBER').value = '';
			__doPostBack('hidAGENCY_ID','');				
		}		
		function PostFromLookupInsured()
		{
			/*Post back the form */	
			if(document.getElementById('lstInsuredName').value!="")
			{
				document.getElementById('lstPolicy').value = '';
				document.getElementById('lstClaim').value = '';
				document.getElementById('txtStartDate').value = '';
				document.getElementById('txtEndDate').value = '';			
			}
			//__doPostBack('hidAGENCY_ID','');				
		}	
		function PostFromLookupPolicy()
		{
			/*Post back the form */	
			if(document.getElementById('lstPolicy').value!="")
			{
				document.getElementById('lstInsuredName').value = '';	
				document.getElementById('lstClaim').value = '';
				document.getElementById('txtStartDate').value = '';
				document.getElementById('txtEndDate').value = '';	
			}		
			//__doPostBack('hidAGENCY_ID','');				
		}	
		function PostFromLookupClaim()
		{
			/*Post back the form */	
			if(document.getElementById('lstClaim').value!="")
			{
				document.getElementById('lstInsuredName').value = '';	
				document.getElementById('lstPolicy').value = '';
				document.getElementById('txtStartDate').value = '';
				document.getElementById('txtEndDate').value = '';			
			}
			//__doPostBack('hidAGENCY_ID','');				
		}	  
		
		//Added for Itrack Issue 6070 on 8 July 09
		function funcValidateSelOrderBy(sender,args)
		{
			if(document.getElementById("lstSelOrderBy").options.length < 1 )
			{
	     		args.IsValid = false;
	     		document.getElementById("csvSelOrderBy").setAttribute("isValid",false);
	     		document.getElementById("csvSelOrderBy").setAttribute('enabled',true);
	     		document.getElementById("csvSelOrderBy").style.display = "inline";
	     		return false;
			}
			//Added for Itrack Issue 6070 on 18 Sept 09
			else if(document.getElementById("lstSelOrderBy").options.length > 3 )
			{
	     		alert("Please select atmost 3 sort criteria.");
	     		return false;
			}
			else
			{
	    		args.IsValid = true;
	    		document.getElementById("csvSelOrderBy").setAttribute("isValid",true);
	    		document.getElementById("csvSelOrderBy").setAttribute('enabled',false);
	    		document.getElementById("csvSelOrderBy").style.display = "none";
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
		//added till here
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" onload="top.topframe.main1.mousein = false;findMouseIn();ApplyColor();" MS_POSITIONING="GridLayout"
		scroll="yes">
		<form id="Form1" method="post" runat="server">
		<DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>			
			<!-- To add bottom menu -->
			<!-- To add bottom menu ends here-->
			<asp:panel id="Panel1" Runat="server">
				<TABLE class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD class="pageHeader" id="tdClientTop" colSpan="4">
							<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer></TD>
					</TR>
					<TR>
						<TD class="headereffectCenter" colSpan="4">Claim Status Report Selection Criteria</TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolora" colspan = "1">
							<asp:label id="lblAgent" runat="server">Select Agency</asp:label></TD>
						<TD class="midcolora" colspan = "1">
							<asp:TextBox id="txtAGENCY_NAME" runat="server" size="25" ReadOnly="True"></asp:TextBox><IMG id="imgAGENCY_NAME" style="CURSOR: hand" onclick="OpenAgencyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
								runat="server">
								<asp:requiredfieldvalidator id="rfvAGENCY_NAME" runat="server" Display="Dynamic" ErrorMessage="AGENCY_NAME can't be blank."
								ControlToValidate="txtAGENCY_NAME"></asp:requiredfieldvalidator>
							</TD>
						<TD class="midcolora" colspan = "1">
							<asp:label id="lblInsured" runat="server">Select Insured/Claimant</asp:label></TD>
						<TD class="midcolora" colspan = "1">
							<asp:TextBox id="lstInsuredName" runat="server" size="25" ReadOnly="True"></asp:TextBox><IMG id="imgINSURED_NAME" style="CURSOR: hand" onclick="OpenInsuredLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
								runat="server"></TD>
					</TR>
					<TR>
						<TD class="midcolora" colspan = "1">
							<asp:label id="lblPolicy" runat="server">Select Policy</asp:label></td>
						<TD class="midcolora" colspan = "1">
							<asp:TextBox id="lstPolicy" runat="server" size="25" ReadOnly="True"></asp:TextBox><IMG id="imgPolicy" style="CURSOR: hand" onclick="OpenPolicyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
								runat="server"></TD>
						<TD class="midcolora" colspan = "1">
							<asp:label id="lblClaim" runat="server">Select Claim</asp:label></TD>
						<TD class="midcolora" colspan = "1">
							<asp:TextBox id="lstClaim" runat="server" size="25" ReadOnly="True"></asp:TextBox><IMG id="imgClaim" style="CURSOR: hand" onclick="OpenClaimLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
								runat="server"></TD>
					</TR>
					<TR>
						<TD class="midcolora" colspan = "1">
							<asp:label id="lblStartDate" runat="server">Start Date</asp:label></TD>
						<TD class="midcolora" colspan = "1">
							<asp:TextBox id="txtStartDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkStartDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgStartDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR>
							<asp:regularexpressionvalidator id="revStartDate" Runat="server" ControlToValidate="txtStartDate" ErrorMessage="Please enter Date in 'mm/dd/yyyy' format."
								Display="Dynamic"></asp:regularexpressionvalidator></TD>
						<TD class="midcolora" colspan = "1">
							<asp:label id="lblEndDate" runat="server">End Date</asp:label></TD>
						<TD class="midcolora" colspan = "1">
							<asp:TextBox id="txtEndDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkEndDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgEndDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR>
							<asp:regularexpressionvalidator id="revEndDate" Runat="server" ControlToValidate="txtEndDate" ErrorMessage="Please enter Date in 'mm/dd/yyyy' format."
								Display="Dynamic"></asp:regularexpressionvalidator>
							<asp:CompareValidator id="cmpDate" Runat="server" ControlToValidate="txtEndDate" ErrorMessage="End Date can't be less than Start Date."
								Display="Dynamic" Operator="GreaterThanEqual" Type="Date" ControlToCompare="txtStartDate"></asp:CompareValidator></TD>
					</TR>
					
					<!-- order by fields -- //Added for Itrack Issue 6070 on 8 July 09-->
					<TR>
						<TD class="midcolora" colSpan="1"><asp:label id="lblOrderBy" runat="server">Sort By</asp:label><!--<SPAN class="mandatory" id="spnOrderBy">*</SPAN>--></TD>
						<TD class="midcolora" colSpan="1">
						<!-- Sort by fields added-- //Added for Itrack Issue 6070 on 18 Sept 09-->
							<asp:ListBox id="lstOrderBy" runat="server" Height="65px" SelectionMode="Multiple" Width="140px" AutoPostBack="false">
								<asp:ListItem Value="LOSS_DATE">Date of Loss</asp:ListItem>
								<asp:ListItem Value="CLAIM_NUMBER">Claim Number</asp:ListItem>
								<asp:ListItem Value="CLAIM_STATUS">Claim Status</asp:ListItem>
								<asp:ListItem Value="POLICY_NUMBER">Policy Number</asp:ListItem>
								<asp:ListItem Value="LOB_DESC">LOB</asp:ListItem>
								<asp:ListItem Value="INSURED">Insured</asp:ListItem>
								<asp:ListItem Value="ADJUSTER_NAME">Adjuster</asp:ListItem>
								<asp:ListItem Value="PAID_LOSS">Paid Loss</asp:ListItem>
							</asp:ListBox>
						</TD>
						<TD class="midcolora" vAlign="middle" colSpan="1">&nbsp;<asp:Button id="btnSel" Runat="server" Text=">>"></asp:Button><BR><BR>
							&nbsp;<asp:Button id="btnDeSel" Runat="server" Text="<<"></asp:Button>
						</TD>
						<TD class="midcolora" colSpan="1"><asp:ListBox id="lstSelOrderBy" runat="server" Height="65px" SelectionMode="Multiple" Width="140px" AutoPostBack="False">
							</asp:ListBox><br><asp:customvalidator id="csvSelOrderBy" Runat="server" ControlToValidate="lstSelOrderBy" Display="Dynamic" ErrorMessage="Please select the sort criteria"></asp:customvalidator>
						</TD>
				   </TR>
				   <!--Added till here-->
				   <TR>
						<TD class="midcolorc" colSpan="4">
							<cmsb:cmsbutton class="clsbutton" id="btnReport" Runat="server" Text="Display Report"></cmsb:cmsbutton></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<input id="hidAGENCY_ID" type="hidden" name="hidAGENCY_ID" runat="server"> <input id="hidPOLICYINFO" type="hidden" name="hidPOLICYINFO" runat="server">
			<input id="hidCLAIM_POLICY_NUMBER" type="hidden" name="hidCLAIM_POLICY_NUMBER" runat="server">
			<input id="hidCUSTOMER_ID_NAME" type="hidden" name="hidCUSTOMER_ID_NAME" runat="server">
			<input id="hidINSURED_ID" type="hidden" name="hidINSURED_ID" runat="server">
		</form>
		</TD></TR></TBODY></TABLE></FORM>
	</body>
</HTML>

<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="MonthlyCheck.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.MonthlyCheck" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Monthly Check Report</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">		
		
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
				var ExpirationStartDate="";
				var ExpirationEndDate="";
				var Account="";
				var	CheckType="";
				var AmountFrome="";
				var AmountTo="";
				var Combination ="";
				var PolicyID="";
				var CustomerID="";
				var VersionID="";
				
				var Payee ="";
				var ClaimNo="";
				var CheckNo="";
				
				//var CustomerID = "";						
				//var PolicyID = "";
				//var VersionID = "";
				var Combination ="";
				var FirstSort="";
				var VoidChecks="";
				var chkVal = "";
				var PolicyNumber="";
								
				ExpirationStartDate	= GetValue(document.getElementById('txtExpirationStartDate'),'T');
				ExpirationEndDate	= GetValue(document.getElementById('txtExpirationEndDate'),'T');
				Account				= GetValue(document.getElementById('lstAccount'),'D');
				AmountFrom			= GetValue(document.getElementById('txtFromRange'),'T');
				AmountTo			= GetValue(document.getElementById('txtToRange'),'T');
				CheckType			= GetValue(document.getElementById('lstCheckType'),'D');				
				Payee	= GetValue(document.getElementById('txtPayee'),'T');
				ClaimNo	= GetValue(document.getElementById('txtClaimNo'),'T');
				CheckNo	= GetValue(document.getElementById('txtCheckNo'),'T');
				FirstSort			= GetValue(document.getElementById('cmbFirstSort'),'T');
				chkVal			= document.getElementById('chkVoidChecks').getAttribute('checked');
				PolicyNumber       = GetValue(document.getElementById('txtPOLICY_ID'),'T');
				
				if(chkVal)
				{VoidChecks = "1";}
				else
				{VoidChecks = "0";}
											
				Combination = document.getElementById('hidPolicyID').value;
				//alert(Combination);
				
				/*if (Combination == "")
				{
					//alert("Please Select a Policy");
					//return;
					CustomerID = "0";						
					PolicyID = "0";
					VersionID = "0";
				}*/
									
				if(Combination !='')
					{
						var arrCPV=Combination.split("^");
						CustomerID = arrCPV[2];						
						PolicyID = arrCPV[0];
						VersionID = arrCPV[1];						
					}					
								
				/*alert('ExpirationStartDate '+ ExpirationStartDate);
				alert('ExpirationEndDate '+ ExpirationEndDate);
				alert('AmountFrom '+ AmountFrom);
				alert('AmountTo '+ AmountTo);					
				alert('CustomerID ' +  CustomerID);						
				alert('PolicyID ' +  PolicyID);						
				alert('VersionID ' +  VersionID);	
										
				alert('Account '+ Account);
				alert('CheckType '+ CheckType);	
				
				alert('Payee '+ Payee);
				alert('ClaimNo '+ ClaimNo);
				alert('CheckNo '+ CheckNo);
				alert('FirstSort '+ FirstSort);*/
				//alert(VoidChecks);
							
				var url="CustomReport.aspx?PageName=MonthlyCheck&FROMDATE="+ ExpirationStartDate + "&TODATE="+ ExpirationEndDate + "&CheckType=" + CheckType + "&CheckNo=" + CheckNo  + "&ClaimNo=" + ClaimNo +  "&Payee=" + Payee + "&FROMAMT=" + AmountFrom + "&TOAMT=" + AmountTo + "&VersionID=" + VersionID + "&AccountId=" + Account + "&FirstSortid=" + FirstSort + "&VoidChecks=" + VoidChecks + "&PolicyNumber=" + PolicyNumber;  
				//alert(url);
				var windowobj = window.open(url,'MonthlyCheck','resizable=yes,scrollbar=yes,top=100,left=50')
				windowobj.focus();				
			}
			
			function OpenAgencyLookup()
			{			
				var url='<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>';
				OpenLookupWithFunction( url,"AGENCY_ID","Name","hidAGENCY_ID","txtAGENCY_NAME","Agency","Agency Names",'','PostFromLookup()');			
			}
			
			function OpenPolicyLookup()
			{			
				var url='<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>';
				OpenLookupWithFunction( url,"POL_INFORM","POLICY_NUMBER","hidPolicyID","txtPOLICY_ID","Policy","Policy",'','PostFromLookup()');					
			}
		
			function PostFromLookup()
			{
				//alert(document.getElementById('hidPolicyID').value);
				__doPostBack('hidAGENCY_ID','')
				__doPostBack('hidPolicyID','')
			}						
						
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" scroll="yes" onload="top.topframe.main1.mousein = false;findMouseIn();ApplyColor();"
		MS_POSITIONING="GridLayout">
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
						<TD class="headereffectCenter" colSpan="4">Monthly Check Report Selection Criteria</TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolora" width="18%">
							<asp:label id="lblExpirationStartDate" runat="server">Start Date</asp:label></TD>
						<TD class="midcolora" width="32%">
							<asp:TextBox id="txtExpirationStartDate" runat="server" MaxLength="10" size="12"></asp:TextBox>
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
							<asp:TextBox id="txtExpirationEndDate" runat="server" MaxLength="10" size="12"></asp:TextBox>
							<asp:hyperlink id="hlkExpirationEndDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgExpirationEndDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR>
							<asp:regularexpressionvalidator id="revExpirationEndDate" Runat="server" Display="Dynamic" ErrorMessage="Please enter Date in 'mm/dd/yyyy' format."
								ControlToValidate="txtExpirationEndDate"></asp:regularexpressionvalidator></TD>
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
						<TD class="midcolora" colSpan="1">
							<asp:label id="capPOLICY_ID" runat="server">Policy Number</asp:label></TD>
						<TD class="midcolora" colSpan="1">
							<asp:textbox id="txtPOLICY_ID" runat="server" size="14" maxlength="10" ReadOnly="False"></asp:textbox><IMG id="imgSelect" style="CURSOR: hand" onclick="OpenPolicyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
								runat="server">
						</TD>
						<TD class="midcolora" colSpan="1">
							<asp:Label id="lblPayee" runat="server">Payee</asp:Label></TD>
						<TD class="midcolora" colSpan="1">
							<asp:textbox id="txtPayee" runat="server" size="15"></asp:textbox></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblCheckNumber" runat="server">Check Number</asp:label></TD>
						<TD class="midcolora" colSpan="1">
							<asp:textbox id="txtCheckNo" runat="server" size="15"></asp:textbox></TD>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblClaimNumber" runat="server">Claim Number</asp:label></TD>
						<TD class="midcolora" colSpan="1">
							<asp:textbox id="txtClaimNo" runat="server" size="15"></asp:textbox></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblAccount" runat="server">Account Number</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstAccount" runat="server" Height="100px" Width="240px" SelectionMode="Multiple"></asp:ListBox></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblCheckType" runat="server">Check Type</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstCheckType" runat="server" Height="100px" Width="240px" SelectionMode="Multiple"></asp:ListBox></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblFirstSort" runat="server">Sort</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:DropDownList id="cmbFirstSort" runat="server" Height="100px" Width="240px">
								<asp:ListItem Value="" Selected="True"></asp:ListItem>
								<asp:ListItem Value="Acc_Disp_Number">Account No.</asp:ListItem>
								<asp:ListItem Value="Check_Date">Check Date</asp:ListItem>
								<asp:ListItem Value="Check_Number">Check Number</asp:ListItem>
								<asp:ListItem Value="Payee_Entity_Name">Payee</asp:ListItem>
								<asp:ListItem Value="Check_Type">Check Type</asp:ListItem>
								<asp:ListItem Value="Check_Amount">Check Amount</asp:ListItem>
								<asp:ListItem Value="Cleared">Cleared</asp:ListItem>
								<asp:ListItem Value="Status">Status</asp:ListItem>
							</asp:DropDownList></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:Label id="lblVoidChecks" Runat="server">Include Voided Checks ?</asp:Label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:CheckBox id="chkVoidChecks" Runat="server"></asp:CheckBox>
						</TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"><cmsb:cmsbutton class="clsbutton" id="btnReport" Runat="server" Text="Display Report"></cmsb:cmsbutton>
						</TD>
					</TR>
				</TABLE>
			</asp:panel><input id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server">
		</form>
	</body>
</HTML>

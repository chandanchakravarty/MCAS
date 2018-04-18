<%@ Page language="c#" Codebehind="PolicyExpirationList.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.PolicyExpirationList" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Policy Expiration Report</title>
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
			
			//Added For Itrack	Issue #6057.
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
		
		//Added For Itrack	Issue #6057.
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
			//Uncommented for Itrack Issue 6183 on 30 July 2009
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
			//return true;
		}		
			
			function ShowReport()
			{	
			    //Added for Itrack Issue 6183 on 30 July 2009
				if (!funcValidateSelOrderBy())
					return false;
							
				var Customer="";
				var Agency="";
				var Underwriter="";
				var LOB="";
				var BillType="";
				var ExpirationStartDate=null;
				var ExpirationEndDate=null;
				var Address="";
				var OrderBy = "";
			
				
				ExpirationStartDate	= GetValue(document.getElementById('txtExpirationStartDate'),'T');
				ExpirationEndDate	= GetValue(document.getElementById('txtExpirationEndDate'),'T');
				Customer			= GetValue(document.getElementById('lstCustomerName'),'D');
				//Agency				= GetValue(document.getElementById('lstAgentName'),'D');
				Agency				= document.getElementById('hidAGENCY_ID').value;
				Underwriter			= GetValue(document.getElementById('lstUnderWriterName'),'D');
				LOB					= GetValue(document.getElementById('lstLOB'),'D','');
				BillType			= GetValue(document.getElementById('lstBillType'),'D','-99');
				PolicyStatus		= GetValue(document.getElementById('lstPolicyStatus'),'D','');
				
				if(document.getElementById('rdAddress1').checked)
					Address="1";
				else if(document.getElementById('rdAddress2').checked)
					Address="2";
					
					
				var LstLen = document.getElementById('lstSelOrderBy').options.length;
				var strOrderBy = new String();
				for(var i = 0;i<LstLen;i++)
					{strOrderBy = strOrderBy + document.getElementById('lstSelOrderBy').options[i].value + ',';}
				
				
				OrderBy = strOrderBy.substr(0,strOrderBy.length-1); // remove ',' from last		
			
				/*alert('ExpirationStartDate '+ ExpirationStartDate);
				alert('ExpirationEndDate '+ ExpirationEndDate);
				alert('Customer '+ Customer);
				alert('Agency '+ Agency);
				alert('Underwriter '+ Underwriter);
				alert('LOB '+ LOB);
				alert('BillType '+ BillType);
				alert('Address '+ Address);*/
				//alert('PolicyStatus '+ PolicyStatus);
				
				/*
				if (document.getElementById('txtAGENCY_NAME').value == '')
				{
					alert("Please Select Agency");
					return
				}
				 */
				
				/*if (Customer!= '')
				{					
				*/	
					//alert(OrderBy + 'OrderBy')
					var url="CustomReport.aspx?PageName=PolicyExpiration&Customerid="+ Customer + "&Agencyid="+ Agency + "&Underwriterid=" + Underwriter + "&LOBid=" + LOB  + "&BillTypeid=" + BillType +  "&PolicyStatusid=" + PolicyStatus + "&ExpirationStartDateid=" + ExpirationStartDate + "&ExpirationEndDateid=" + ExpirationEndDate + "&Addressid=" + Address + "&ORDERBY=" + OrderBy; 
					var windowobj = window.open(url,'PolicyExpiration','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();
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
						<TD class="headereffectCenter" colSpan="4">Policy Status Selection Criteria</TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolora" width="18%">
							<asp:label id="lblExpirationStartDate" runat="server">Expiration Start Date</asp:label></TD>
						<TD class="midcolora" width="32%">
							<asp:TextBox id="txtExpirationStartDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkExpirationStartDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgExpirationStartDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR>
							<asp:regularexpressionvalidator id="revExpirationStartDate" Runat="server" ControlToValidate="txtExpirationStartDate"
								ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator>
							<asp:CompareValidator id="cmpExpirationDate" Runat="server" ControlToValidate="txtExpirationEndDate" ErrorMessage="End Date can't be less than Start Date."
								Display="Dynamic" Operator="GreaterThanEqual" Type="Date" ControlToCompare="txtExpirationStartDate"></asp:CompareValidator></TD>
						<TD class="midcolora" width="18%">
							<asp:label id="lblExpirationEndDate" runat="server">Expiration End Date</asp:label></TD>
						<TD class="midcolora" width="32%">
							<asp:TextBox id="txtExpirationEndDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkExpirationEndDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgExpirationEndDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR>
							<asp:regularexpressionvalidator id="revExpirationEndDate" Runat="server" ControlToValidate="txtExpirationEndDate"
								ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator></TD>
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
							<asp:ListBox id="lstCustomerName" runat="server" SelectionMode="Multiple" Width="240px" Height="100px"></asp:ListBox></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblLOB" runat="server">Select Line of Business</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstLOB" runat="server" SelectionMode="Multiple" Width="240px" Height="100px"></asp:ListBox></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblUnderwriter" runat="server">Select Underwriter</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstUnderWriterName" runat="server" SelectionMode="Multiple" Width="240px" Height="100px"></asp:ListBox></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblBillType" runat="server">Select Bill Type</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstBillType" runat="server" SelectionMode="Multiple" Width="240px" Height="100px"></asp:ListBox></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblPolicyStaus" runat="server">Select Policy Status</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstPolicyStatus" runat="server" SelectionMode="Multiple" Width="240px" Height="100px"></asp:ListBox></TD>
					</TR>
					
					
						<!-- order by fields -->
					<TR>
						<TD class="midcolora" colSpan="1"><asp:label id="lblOrderBy" runat="server">Order By</asp:label><!--<SPAN class="mandatory" id="spnOrderBy">*</SPAN>--></TD>
						<TD class="midcolora" colSpan="1"><asp:ListBox id="lstOrderBy" runat="server" Height="65px" SelectionMode="Multiple" Width="110px" AutoPostBack="false">
								<asp:ListItem Value="AGENCYNAME">Agency</asp:ListItem>
								<asp:ListItem Value="CUSTOMER_NAME1">Customer</asp:ListItem>
								<asp:ListItem Value="UNDERWRITER">Underwriter</asp:ListItem>
								<asp:ListItem Value="BILL_TYPE">Bill Type</asp:ListItem>								
								<asp:ListItem Value="POLICY_DESCRIPTION">Policy Status</asp:ListItem>								
								 <asp:ListItem Value="APP_EXPIRATION_DATE">Expiration Date</asp:ListItem> </asp:ListBox></TD>
						<TD class="midcolora" vAlign="middle" colSpan="1">&nbsp;<asp:Button id="btnSel" Runat="server" Text=">>"></asp:Button><BR><BR>
							&nbsp;<asp:Button id="btnDeSel" Runat="server" Text="<<"></asp:Button>
						</TD>
						<TD class="midcolora" colSpan="1"><asp:ListBox id="lstSelOrderBy" onblur="funcValidateSelOrderBy" runat="server" Height="65px" SelectionMode="Multiple" Width="110px" AutoPostBack="False" onchange="funcValidateSelOrderBy">
							</asp:ListBox><br><asp:customvalidator id="csvSelOrderBy" Runat="server" ControlToValidate="lstSelOrderBy" Display="Dynamic" ClientValidationFunction="funcValidateSelOrderBy" Enabled="False"></asp:customvalidator><SPAN id="spnSelOrderBy" style="DISPLAY: none; COLOR: red">Please 
								select the sort criteria. 
								</SPAN> </TD></TR>
					
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblAddress" runat="server">Address Information on Report</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:RadioButton id="rdAddress1" Runat="server" GroupName="Add" Checked="True"></asp:RadioButton>Place 
							Underneath Client Name
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
			<input id="hidAGENCY_ID" type="hidden" name="hidAGENCY_ID" runat="server">
		</form>
	</body>
</HTML>

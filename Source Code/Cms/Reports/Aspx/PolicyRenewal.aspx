<%@ Page language="c#" Codebehind="PolicyRenewal.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.PolicyRenewal" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Policy Renewal Report</title>
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
				var Underwriter="";
				var LOB="";
				var BillType="";
				var ExpirationStartDate="";
				var ExpirationEndDate="";
				var Address="";
			
				ExpirationStartDate	= GetValue(document.getElementById('txtExpirationStartDate'),'T');
				ExpirationEndDate	= GetValue(document.getElementById('txtExpirationEndDate'),'T');
				Customer			= GetValue(document.getElementById('lstCustomerName'),'D');
				//Agency				= GetValue(document.getElementById('txtAGENCY_NAME'),'T');
				Agency				= document.getElementById('hidAGENCY_ID').value;
				Underwriter			= GetValue(document.getElementById('lstUnderWriterName'),'D');
				LOB					= GetValue(document.getElementById('lstLOB'),'D','');
				BillType			= GetValue(document.getElementById('lstBillType'),'D','-99');
				//Address				= "0";//GetValue(document.getElementById('Add'),'R');
			
				if(document.getElementById('rdAddress1').checked)
					Address="1";
				else if(document.getElementById('rdAddress2').checked)
					Address="2";
												
				/*alert('ExpirationStartDate' + ExpirationStartDate);
				alert('ExpirationEndDate' + ExpirationEndDate);
				alert('Customer '+ Customer);
				alert('Agency '+ Agency);
				alert('Underwriter '+ Underwriter);
				alert('LOB '+ LOB);
				alert('BillType '+ BillType);
				alert('Address '+ Address);*/
								
				if (document.getElementById('txtAGENCY_NAME').value == '')
				{
					alert("Please Select Agency");
					return
				}
								
				if (Customer!= '')
				{					
					var url="CustomReport.aspx?PageName=PolicyRenewal&Customerid="+ Customer + "&Agencyid="+ Agency + "&Underwriterid=" + Underwriter + "&LOBid=" + LOB  + "&BillTypeid=" + BillType +  "&ExpirationStartDateid=" + ExpirationStartDate + "&ExpirationEndDateid=" + ExpirationEndDate + "&Addressid=" + Address; 
					var windowobj = window.open(url,'PolicyRenewal','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();
				}
				else
				{
					alert("Please Select Customer");
				}
			}
			//This function open the agency lookup window
			function OpenAgencyLookup()
			{			
				var url='<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>';
				//var idField,valueField,lookUpTagName,lookUpTitle;
		
				/*idField			=	'AGENCY_ID';
				valueField		=	'Name';
				lookUpTagName	=	'Agency';
				lookUpTitle		=	"Agency Names";	*/		
				OpenLookupWithFunction( url,"AGENCY_ID","Name","hidAGENCY_ID","txtAGENCY_NAME","Agency","Agency Names",'','PostFromLookup()');			
				
			}
		
			function PostFromLookup()
			{
				/*Post back the form */			
				//alert(document.getElementById('hidAGENCY_ID').value);
				__doPostBack('hidAGENCY_ID','')
				
			}
		</script>
	</HEAD>
	<body onload="top.topframe.main1.mousein = false;findMouseIn();ApplyColor();" MS_POSITIONING="GridLayout" scroll="yes">
		<form id="Form1" method="post" runat="server">
			<DIV id="myTSMain" style="BEHAVIOR: url(/cms/cmsweb/htc/webservice.htc)"></DIV>
			<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
			<!-- To add bottom menu ends here-->
			<asp:panel id="Panel1" Runat="server"> 
				<TABLE class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD class="pageHeader" id="tdClientTop" colSpan="4">
							<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer></TD>
					</TR>
					<TR>
						<TD class="headereffectCenter" colSpan="4">Policy Renewal Report Selection Criteria</TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					
					<TR>
						<TD class="midcolora" colspan="1">
							<asp:label id="lblExpirationStartDate" runat="server">Expiration Start Date</asp:label></TD>
						<TD class="midcolora" colspan="1">
							<asp:TextBox id="txtExpirationStartDate" runat="server" MaxLength="10" size="12"></asp:TextBox><asp:hyperlink id="hlkExpirationStartDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgExpirationStartDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR>
							<asp:regularexpressionvalidator id="revExpirationStartDate" Runat="server" Display="Dynamic" ErrorMessage="Please enter Date in 'mm/dd/yyyy' format."
								ControlToValidate="txtExpirationStartDate"></asp:regularexpressionvalidator>
							<asp:CompareValidator id="cmpExpirationDate" Runat="server" Display="Dynamic" ErrorMessage="End Date can't be less than Start Date."
								ControlToValidate="txtExpirationEndDate" ControlToCompare="txtExpirationStartDate" Type="Date" Operator="GreaterThanEqual"></asp:CompareValidator></TD>
						<TD class="midcolora" colspan="1">
							<asp:label id="lblExpirationEndDate" runat="server">Expiration End Date</asp:label></TD>
						<TD class="midcolora" colspan="1">
							<asp:TextBox id="txtExpirationEndDate" runat="server" MaxLength="10" size="12"></asp:TextBox><asp:hyperlink id="hlkExpirationEndDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgExpirationEndDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR>
							<asp:regularexpressionvalidator id="revExpirationEndDate" Runat="server" Display="Dynamic" ErrorMessage="Please enter Date in 'mm/dd/yyyy' format."
								ControlToValidate="txtExpirationEndDate"></asp:regularexpressionvalidator></TD>
					</TR>
										
					<TR>
						<TD class="midcolora" width ="36%">
							<asp:label id="lblAgent" runat="server">Select Agency</asp:label></TD>
						<TD class="midcolora" width ="64% "colSpan="3">
							<asp:TextBox id="txtAGENCY_NAME" runat="server" size="40" ReadOnly="True"></asp:TextBox><IMG id="imgAGENCY_NAME" style="CURSOR: hand" onclick="OpenAgencyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
							runat="server"></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1"
							<asp:label id="lblCustomer" runat="server">Select Customers</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstCustomerName" runat="server" Height="100px" Width="240px" SelectionMode="Multiple"></asp:ListBox></TD>
					</TR>
					
					<TR>	
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblLOB" runat="server">Select Line of Business</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstLOB" runat="server" Height="100px" Width="240px" SelectionMode="Multiple"></asp:ListBox></TD>
					</TR>
					
					<TR>
						<TD class="midcolora" colspan="1">
							<asp:label id="lblUnderwriter" runat="server">Select Underwriter</asp:label></TD>
						<TD class="midcolora" colspan="3">
							<asp:ListBox id="lstUnderWriterName" runat="server" Height="100px" Width="240px" SelectionMode="Multiple"></asp:ListBox></TD>
					</TR>
					
					<TR>
						<TD class="midcolora" colspan="1">
							<asp:label id="lblBillType" runat="server">Select Bill Type</asp:label></TD>
						<TD class="midcolora" colspan="3">
							<asp:ListBox id="lstBillType" runat="server" Height="100px" Width="240px" SelectionMode="Multiple"></asp:ListBox></TD>
					</TR>
										
					<TR>		
						<TD class="midcolora" colspan="1">
							<asp:label id="lblAddress" runat="server">Address Information on Report</asp:label></TD>
						<TD class="midcolora" colspan="3">
							<asp:RadioButton id="rdAddress1" Runat="server" Checked="True" GroupName="Add"></asp:RadioButton>Place 
							Underneath Client Name
							<asp:RadioButton id="rdAddress2" Runat="server" GroupName="Add"></asp:RadioButton>Separate 
							into Individual Columns
						</TD>
						</TD>
					</TR>
					
					<TR>
						<TD class="midcolorc" colSpan="4"><cmsb:cmsbutton class="clsbutton" id="btnReport" Runat="server" Text="Display Report"></cmsb:cmsbutton>
						</TD>
					</TR>
				</TABLE>
			</asp:panel> 
			<input id="hidAGENCY_ID" type="hidden" name="hidAGENCY_ID"  runat="server">
		</form>
	</body>
</HTML>


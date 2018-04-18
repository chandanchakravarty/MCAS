<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="ClientPolicyList.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.ClientPolicyList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Client Policy List Report</title>
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
				var InceptionStartDate=""
				var InceptionEndDate=""
				var EffectiveStartDate=""
				var EffectiveEndDate=""
				var Address="";
						
			
				ExpirationStartDate	= GetValue(document.getElementById('txtExpirationStartDate'),'T');
				ExpirationEndDate	= GetValue(document.getElementById('txtExpirationEndDate'),'T');
				InceptionStartDate	= GetValue(document.getElementById('txtInceptionStartDate'),'T');
				InceptionEndDate	= GetValue(document.getElementById('txtInceptionEndDate'),'T');
				EffectiveStartDate	= GetValue(document.getElementById('txtEffectiveStartDate'),'T');
				EffectiveEndDate	= GetValue(document.getElementById('txtEffectiveEndDate'),'T');
				Customer			= GetValue(document.getElementById('lstCustomerName'),'D');
				//Agency				= GetValue(document.getElementById('lstAgentName'),'D');
				Agency				= document.getElementById('hidAGENCY_ID').value;
				Underwriter			= GetValue(document.getElementById('lstUnderWriterName'),'D');
				LOB					= GetValue(document.getElementById('lstLOB'),'D','');
				BillType			= GetValue(document.getElementById('lstBillType'),'D','-99');
							
				if(document.getElementById('rdAddress1').checked)
					Address="1";
				else if(document.getElementById('rdAddress2').checked)
					Address="2";
				
				/*alert('ExpirationStartDate '+ ExpirationStartDate);
				alert('ExpirationEndDate '+ ExpirationEndDate);
				alert('InceptionStartDate '+ InceptionStartDate);
				alert('InceptionEndDate '+ InceptionEndDate);
				alert('EffectiveStartDate '+ EffectiveStartDate);
				alert('EffectiveEndDate '+ EffectiveEndDate);
				alert('Customer '+ Customer);
				alert('Agency '+ Agency);
				alert('Underwriter '+ Underwriter);
				alert('LOB '+ LOB);
				alert('BillType '+ BillType);
				alert('Address '+ Address);*/
				
				/*if (document.getElementById('txtAGENCY_NAME').value == '')
				{
					alert("Please Select Agency");
					return
				}
				
				if (Customer!= '')
				{*/					
					var url="CustomReport.aspx?PageName=ClientPolicyList&Customerid="+ Customer + "&Agencyid="+ Agency + "&Underwriterid=" + Underwriter + "&LOBid=" + LOB  + "&BillTypeid=" + BillType +  "&InceptionStartDateid=" + InceptionStartDate +  "&InceptionEndDateid=" + InceptionEndDate +  "&EffectiveStartDateid=" + EffectiveStartDate +  "&EffectiveEndDateid=" + EffectiveEndDate  + "&ExpirationStartDateid=" + ExpirationStartDate + "&ExpirationEndDateid=" + ExpirationEndDate + "&Addressid=" + Address; 
					var windowobj = window.open(url,'ClientPolicyList','resizable=yes,scrollbar=yes,top=100,left=50')
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
						<TD class="headereffectCenter" colSpan="4">Client Policy List Report Selection 
							Criteria</TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblInceptionStartDate" runat="server">Inception Start Date</asp:label></TD>
						<TD class="midcolora" colSpan="1">
							<asp:TextBox id="txtInceptionStartDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkInceptionStartDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgInceptionStartDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR>
							<asp:regularexpressionvalidator id="revInceptionStartDate" Runat="server" ControlToValidate="txtInceptionStartDate"
								ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator></TD>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblInceptionEndDate" runat="server">Inception End Date</asp:label></TD>
						<TD class="midcolora" colSpan="1">
							<asp:TextBox id="txtInceptionEndDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkInceptionEndDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgInceptionEndDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR>
							<asp:regularexpressionvalidator id="revInceptionEndDate" Runat="server" ControlToValidate="txtInceptionEndDate"
								ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator>
							<asp:CompareValidator id="cmpInceptionDate" Runat="server" ControlToValidate="txtInceptionEndDate" ErrorMessage="End Date can't be less than Start Date."
								Display="Dynamic" Operator="GreaterThanEqual" Type="Date" ControlToCompare="txtInceptionStartDate"></asp:CompareValidator></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblEffectiveStartDate" runat="server">Effective Start Date</asp:label></TD>
						<TD class="midcolora" colSpan="1">
							<asp:TextBox id="txtEffectiveStartDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkEffectiveStartDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgEffectiveStartDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR>
							<asp:regularexpressionvalidator id="revEffectiveStartDate" Runat="server" ControlToValidate="txtEffectiveStartDate"
								ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator></TD>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblEffectiveEndDate" runat="server">Effective End Date</asp:label></TD>
						<TD class="midcolora" colSpan="1">
							<asp:TextBox id="txtEffectiveEndDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkEffectiveEndDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgEffectiveEndDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR>
							<asp:regularexpressionvalidator id="revEffectiveEndDate" Runat="server" ControlToValidate="txtEffectiveEndDate"
								ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator>
							<asp:CompareValidator id="cmpEffectiveDate" Runat="server" ControlToValidate="txtEffectiveEndDate" ErrorMessage="End Date can't be less than Start Date."
								Display="Dynamic" Operator="GreaterThanEqual" Type="Date" ControlToCompare="txtEffectiveStartDate"></asp:CompareValidator></TD>
					</TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblExpirationStartDate" runat="server">Expiration Start Date</asp:label></TD>
						<TD class="midcolora" colSpan="1">
							<asp:TextBox id="txtExpirationStartDate" runat="server" size="12" MaxLength="10"></asp:TextBox><asp:hyperlink id="hlkExpirationStartDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgExpirationStartDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR>
							<asp:regularexpressionvalidator id="revExpirationStartDate" Runat="server" ControlToValidate="txtExpirationStartDate"
								ErrorMessage="Please enter Date in 'mm/dd/yyyy' format." Display="Dynamic"></asp:regularexpressionvalidator>
							<asp:CompareValidator id="cmpExpirationDate" Runat="server" ControlToValidate="txtExpirationEndDate" ErrorMessage="End Date can't be less than Start Date."
								Display="Dynamic" Operator="GreaterThanEqual" Type="Date" ControlToCompare="txtExpirationStartDate"></asp:CompareValidator></TD>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblExpirationEndDate" runat="server">Expiration End Date</asp:label></TD>
						<TD class="midcolora" colSpan="1">
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
							<asp:label id="lblAddress" runat="server">Address Information on Report</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:RadioButton id="rdAddress1" Runat="server" GroupName="Add" Checked="True"></asp:RadioButton>Place 
							Underneath Client Name
							<asp:RadioButton id="rdAddress2" Runat="server" GroupName="Add"></asp:RadioButton>Separate 
							into Individual Columns
						</TD>
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

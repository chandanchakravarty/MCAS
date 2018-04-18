<%@ Page language="c#" Codebehind="ClientList.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.ClientList" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Client List Report</title>
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
				var State="";
				var Zip="";
				var NameFormat="";
				var ClientType="";
				var Address="";
							
				Customer			= GetValue(document.getElementById('lstCustomerName'),'D');
				//Agency				= GetValue(document.getElementById('lstAgentName'),'D');			
				Agency				= document.getElementById('hidAGENCY_ID').value;
				State				= GetValue(document.getElementById('lstStateName'),'D','-99');
				Zip					= GetValue(document.getElementById('txtZip'),'T');
							
				if(document.getElementById('rdAddress1').checked)
					Address="1";
				else if(document.getElementById('rdAddress2').checked)
					Address="2";
					
				if(document.getElementById('rdNameFormat1').checked)
					NameFormat="1";
				else if(document.getElementById('rdNameFormat2').checked)
					NameFormat="0";
					
				if(document.getElementById('rdClientType1').checked)
					ClientType="Y";
				else if(document.getElementById('rdClientType2').checked)
					ClientType="N";
				else if(document.getElementById('rdClientType3').checked)
					ClientType="";
				
				
				/*alert('CustomerID '+ Customer);
				alert('AgencyID '+ Agency);
				alert('ClientState '+ State);
				alert('ClientZip '+ Zip);
				alert('NameFormat '+ NameFormat);
				alert('IsClientActive '+ ClientType);
				alert('NameAddress '+ Address);*/
				
				/*if (document.getElementById('txtAGENCY_NAME').value == '')
				{
					alert("Please Select Agency");
					return
				}
				
				if (Customer!= '')
				{*/					
					var url="CustomReport.aspx?PageName=ClientList&Customerid="+ Customer + "&Agencyid="+ Agency + "&Stateid=" + State + "&Zipid=" + Zip  + "&NameFormatid=" + NameFormat +  "&ClientTypeid=" + ClientType + "&Addressid=" + Address; 
					//alert(url);
					var windowobj = window.open(url,'ClientList','resizable=yes,scrollbar=yes,top=100,left=50')
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
	<body oncontextmenu = "return false;" onload="top.topframe.main1.mousein = false;findMouseIn();" MS_POSITIONING="GridLayout" scroll="yes">
		<form id="Form1" method="post" runat="server">
			<DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>
			<!-- To add bottom menu -->
			<!-- To add bottom menu ends here --><asp:panel id="Panel1" Runat="server">
				<TABLE class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD class="pageHeader" id="tdClientTop" colSpan="4">
							<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer></TD></TR>
					<TR>
						<TD class="headereffectCenter" colSpan="4">Client List Report Selection
							Criteria</TD></TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD></TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD></TR>
					<TR>
						<TD class="midcolora" width="36%">
							<asp:label id="lblAgent" runat="server">Select Agency</asp:label></TD>
						<TD class="midcolora" width="64%" colSpan="3">
							<asp:TextBox id="txtAGENCY_NAME" runat="server" ReadOnly="True" size="40"></asp:TextBox><IMG id="imgAGENCY_NAME" style="CURSOR: hand" onclick="OpenAgencyLookup();" alt="" src="../../cmsweb/images/selecticon.gif" runat="server"></TD></TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblCustomer" runat="server">Select Customer</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstCustomerName" runat="server" Height="100px" Width="240px" SelectionMode="Multiple"></asp:ListBox></TD></TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblState" runat="server">Select State</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:ListBox id="lstStateName" runat="server" Height="100px" Width="240px" SelectionMode="Multiple"></asp:ListBox></TD></TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:Label id="lblZip" runat="server">Enter Zip</asp:Label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:textBox id="txtZip" runat="server"></asp:textBox>
							<asp:regularexpressionvalidator id="revCUSTOMER_ZIP" Runat="server" ControlToValidate="txtZip" Display="Dynamic"></asp:regularexpressionvalidator></TD></TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblNameFormat" runat="server">Client Name Format</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:RadioButton id="rdNameFormat1" Runat="server" Checked="True" GroupName="NF"></asp:RadioButton>Last
							Name First Name
							&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:RadioButton id="rdNameFormat2" Runat="server" GroupName="NF"></asp:RadioButton>First
							Name Last Name</TD></TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblAddress" runat="server">Address Information</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:RadioButton id="rdAddress1" Runat="server" Checked="True" GroupName="Add"></asp:RadioButton>Place
							Underneath Client Name
							<asp:RadioButton id="rdAddress2" Runat="server" GroupName="Add"></asp:RadioButton>Separate
							into Individual Columns </TD></TR>
					<TR>
						<TD class="midcolora" colSpan="1">
							<asp:label id="lblClientsType" runat="server">Clients Considered</asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:RadioButton id="rdClientType1" Runat="server" Checked="True" GroupName="CT"></asp:RadioButton>Active
							<asp:RadioButton id="rdClientType2" Runat="server" GroupName="CT"></asp:RadioButton>Inactive
							<asp:RadioButton id="rdClientType3" Runat="server" GroupName="CT"></asp:RadioButton>Both
						</TD></TR>
					<TR>
						<TD class="midcolorc" colSpan="4"><cmsb:cmsbutton class="clsbutton" id="btnReport" Runat="server" Text="Display Report"></cmsb:cmsbutton>
						</TD></TR></TABLE>
			</asp:panel>
			<input id="hidAGENCY_ID" type="hidden" name="hidAGENCY_ID" runat="server">
		</form>
	</body>
</HTML>

<%@ Page language="c#" Codebehind="EndorsementReport.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.EndorsementReport" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Endorsement Report</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
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
			
			else if(type=='P')
			{
				for(var i=0;i<obj.length;i++)
				{
					sVal += obj.options(i).value + ",";					
				}
				if(sVal!='')
					sVal=sVal.toString().substr(0,sVal.length-1);
				else	
					sVal="-1";
			}
				
		return sVal;
		}
			
			
		function ShowReport()
		{			
			var Customer="";
			var Policy="";
			var Agency="";
			var Agency = '<%=strAgencyID%>';
			
			
			
			if(document.getElementById('CheckBox1').checked == true)
			{
				Customer="0";
				Policy="0";
			}
			else if(document.getElementById('CheckBox2').checked == true)
			{
				Customer	= GetValue(document.getElementById('lstAssignUser'),'P');
				Policy="0";
			}
			else
			{
				Customer			= GetValue(document.getElementById('lstAssignUser'),'P');
				Policy				= GetValue(document.getElementById('lstAssignPolicy'),'P');
			}	
			
			if(Customer == "-1")
			{
				Customer = "0";
			}
			
			/* Commented by Asfa Praveen (01-Feb-2008) - iTrack issue #3479
			if(Policy == "-1")
			{
				Policy = "0";
			}
			*/
			if(Agency == "")
			{
				Agency = "0";
			}		
			/*				
			alert('Customer '+ Customer);
			alert('Policy '+ Policy);
			alert(Agency);
			*/
			if (Customer!= ''&& Policy!= '')
			{			
				//var url="CustomReport.aspx?PageName=Endorsement&Customerid="+ Customer + "&Policyid="+ Policy; 
				var url="CustomReport.aspx?PageName=Endorsement&Customerid="+ Customer + "&Policyid="+ Policy + "&Agencyid="+ Agency; 
				var windowobj = window.open(url,'Endorsement','resizable=yes,scrollbar=yes,top=100,left=50')
				windowobj.focus();
			}
		}		
				
		function ShowContact()
		{					
			if (document.getElementById("CheckBox1"))
			{
				if (document.getElementById("CheckBox1").checked == true)
				{
					document.getElementById('trUser').style.display= "none"
					document.getElementById('trUserheading').style.display= "none"
					document.getElementById('trPolicy').style.display= "none"
					document.getElementById('trPolicyheading').style.display= "none"
					document.getElementById('trPolicyAll').style.display= "none"
				}
					
				else
				{
					document.getElementById('trUser').style.display= "inline"
					document.getElementById('trUserheading').style.display= "inline"
					document.getElementById('trPolicy').style.display= "inline"
					document.getElementById('trPolicyheading').style.display= "inline"
					document.getElementById('trPolicyAll').style.display= "inline"
				}	
			}
		}
		
		
		function ShowPolicy()
		{
			if (document.getElementById("CheckBox2"))
			{
				if (document.getElementById("CheckBox2").checked == true)
				{
					document.getElementById('trPolicy').style.display= "none"
					document.getElementById('trPolicyheading').style.display= "none"
				}
					
				else
				{					
					document.getElementById('trPolicy').style.display= "inline"
					document.getElementById('trPolicyheading').style.display= "inline"					
				}	
			}
		}
			
		function BindEvent()
		{
		
			if (document.getElementById("CheckBox1").checked == true)
			{
				document.getElementById('trUser').style.display= "none";
				document.getElementById('trUserheading').style.display= "none";
				document.getElementById('trPolicy').style.display= "none";
				document.getElementById('trPolicyheading').style.display= "none";
				document.getElementById('trPolicyAll').style.display= "none";
			}				
			else
			{
				document.getElementById('trUser').style.display= "inline";
				document.getElementById('trUserheading').style.display= "inline";
				document.getElementById('trPolicyAll').style.display= "inline";
				
				if (document.getElementById("Checkbox2").checked == true)
				{
					document.getElementById('trPolicyheading').style.display= "none";
					document.getElementById('trPolicy').style.display= "none";
				}
				else
				{
					document.getElementById('trPolicyheading').style.display= "inline";
					document.getElementById('trPolicy').style.display= "inline";
				}
			}			
				
		}
		
			function CountAssignCodes()
			{
				document.getElementById("hidSelectedPolicy").value = "";
				document.getElementById("hidSelectedPolicyName").value = "";
				var coll = document.forms[0].lstAssignPolicy;
				var len = coll.options.length;
				for( k = 0;k < len ; k++)
				{
					var szSelected = coll.options(k).value;
										
					if (document.getElementById("hidSelectedPolicy").value == "")
					{
						document.getElementById("hidSelectedPolicy").value =  szSelected;
					}
					else
					{
						document.forms[0].hidSelectedPolicy.value = document.forms[0].hidSelectedPolicy.value + "," + szSelected;
					}
					
					var szSelectedName = coll.options(k).text;
					if (document.getElementById("hidSelectedPolicyName").value == "")
					{
						document.getElementById("hidSelectedPolicyName").value =  szSelectedName;
					}
					else
					{
						document.forms[0].hidSelectedPolicyName.value = document.forms[0].hidSelectedPolicyName.value + "," + szSelectedName;
					}
				}
			}	
	
			function AssignPolicies()
			{	
				var coll = document.forms[0].lstUnAssignPolicy;
				var len = coll.options.length;
				var num=0;				
				for (i = len-1; i > -1 ; i--)
				{
					if((coll.options(i).selected == true))
					{
						num = i;
						var szSelectedDept = coll.options(i).value;
						var innerText = coll.options(i).text;
						document.forms[0].lstAssignPolicy.options[document.forms[0].lstAssignPolicy.length] = new Option(innerText,szSelectedDept)
						coll.remove(i);	
					}										
				}
				CountAssignCodes();	
			}
			
			function UnAssignPolicies()
			{	
				var coll = document.forms[0].lstAssignPolicy;
				var len = coll.options.length;
				var num=0;				
				for (i = len- 1; i > -1 ; i--)
				{
					if((coll.options(i).selected == true))
					{
						num = i;
						var szSelectedDept = coll.options(i).value;
						var innerText = coll.options(i).text;
						document.forms[0].lstUnAssignPolicy.options[document.forms[0].lstUnAssignPolicy.length] = new Option(innerText,szSelectedDept)
						coll.remove(i);	
					}										
				}
				CountAssignCodes();	
			}		

		</script>
		
	</HEAD>
	<body oncontextmenu = "return false;" onload="top.topframe.main1.mousein = false;findMouseIn();BindEvent();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		 <DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>	
			<!-- To add bottom menu -->
			<!-- To add bottom menu ends here --><asp:panel id="Panel1" Runat="server">
				<TABLE class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD class="pageHeader" id="tdClientTop" colSpan="4">
							<WEBCONTROL:GRIDSPACER id="grdSpacer" runat="server"></WEBCONTROL:GRIDSPACER></TD>
					</TR>
					<TR>
						<TD class="headereffectCenter" colSpan="4">Endorsement Report Selection Criteria
						</TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolora" width="25%">All Customers</TD>
						<TD class="midcolora" colSpan="3">
							<asp:CheckBox id="CheckBox1" runat="server"></asp:CheckBox></TD>
					</TR>
					<TR id="trUserheading">
						<TD class="midcolora" style="HEIGHT: 13px" width="25%">Unselected Customers</TD>
						<TD class="midcolorc" style="HEIGHT: 13px" width="25%"></TD>
						<TD class="midcolora" style="HEIGHT: 13px" colSpan="2">Selected Customers</TD>
					</TR>
					<TR id="trUser">
						<TD class="midcolora" width="33%">
							<asp:listbox id="lstUnAssignUser" Runat="server" SelectionMode="Multiple" Height="100px" Width="240px"></asp:listbox></TD>
						<TD class="midcolorc" width="33%"><BR>
							<BR>
							<cmsb:cmsbutton class="clsButton" id="btnAddClient" runat="server" Text=">>"></cmsb:cmsbutton><BR>
							<BR>
							<cmsb:cmsbutton class="clsButton" id="btnRemoveClient" runat="server" Text="<<"></cmsb:cmsbutton><BR>
							<BR>
						</TD>
						<TD class="midcolora" width="33%" colSpan="2">
							<asp:listbox id="lstAssignUser" Runat="server" SelectionMode="Multiple" Height="100px" Width="240px"></asp:listbox></TD>
					</TR>
					<TR id="trPolicyAll">
						<TD class="midcolora" width="25%">All Policies</TD>
						<TD class="midcolora" colSpan="3">
							<asp:CheckBox id="Checkbox2" runat="server"></asp:CheckBox></TD>
					</TR>
					<TR id="trPolicyheading">
						<TD class="midcolora" style="HEIGHT: 13px" width="25%">Unselected Policies</TD>
						<TD class="midcolorc" style="HEIGHT: 13px" width="25%"></TD>
						<TD class="midcolora" style="HEIGHT: 13px" colSpan="2">Selected Policies</TD>
					</TR>
					<TR id="trPolicy">
						<TD class="midcolora" width="33%">
							<asp:listbox id="lstUnAssignPolicy" Runat="server" SelectionMode="Multiple" Height="100px" Width="240px"></asp:listbox></TD>
						<TD class="midcolorc" width="33%"><BR>
							<%--<BR>
							<INPUT class="clsButton" onclick="javascript:AssignPolicies();" type="button" value=">>"
								name="AssignPolicy"><BR>
							<BR>
							<INPUT class="clsButton" onclick="javascript:UnAssignPolicies();" type="button" value="<<"
								name="UnAssignPolicy">--%>
							<BR>
							<cmsb:cmsbutton class="clsButton" id="btnAddPolicy" Runat="server" Text=">>"></cmsb:cmsbutton><BR>
							<BR>
							<cmsb:cmsbutton class="clsButton" id="btnRemovePolicy" Runat="server" Text="<<"></cmsb:cmsbutton><BR>
							<BR>
						</TD>
						<TD class="midcolora" width="33%" colSpan="2">
							<asp:listbox id="lstAssignPolicy" Runat="server" SelectionMode="Multiple" Height="100px" Width="240px"></asp:listbox></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"><cmsb:cmsbutton class="clsbutton" id="btnReport" Runat="server" Text="Display Report"></cmsb:cmsbutton>
						</TD>
					</TR>
				</TABLE>
			</asp:panel>
			<INPUT id="hidSelectedPolicy" type="hidden" name="hidSelectedPolicy" runat="server">
			<INPUT id="hidSelectedPolicyName" type="hidden" name="hidSelectedPolicyName" runat="server">
		</form>
	</body>
</HTML>

<%@ Page language="c#" Codebehind="DemoReport.aspx.cs" AutoEventWireup="false" Inherits="Reports.Aspx.DemoReport" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Holder List Report</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">	
		
			var Customer="<%=strSelectedClients%>"
			var Policy="<%=strSelectedPolicies%>"
			
			alert(Customer);
			alert(Policy);								
			if (Customer!= ''&& Policy!= '')
			{			
				var url="CustomReport.aspx?PageName=Demo&Customerid="+ Customer + "&Policyid="+ Policy; 
				//alert(url);
				var windowobj = window.open(url,'mywindow','resizable=yes,scrollbar=yes,top=100,left=50')
				windowobj.focus();
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
		
		/*function ShowPopup()
		{			
			if(document.getElementById('CheckBox1').checked == true)
			{
				var url="CustomReport.aspx?PageName=UserList&Userid=0";
				//alert(url);
				var windowobj = window.open(url,'mywindow','resizable=yes,scrollbar=yes,top=100,left=50')
				windowobj.focus();
			}
			else
			{
				CountAssignCodes();
				if (document.forms[0].hidSelectedUsers.value!= '')
				{
					var url="CustomReport.aspx?PageName=UserList&Userid="+ document.getElementById('hidSelectedUsers').value;
					//alert(url);
					var windowobj = window.open(url,'mywindow','resizable=yes,scrollbar=yes,top=100,left=50')
					windowobj.focus();
				}
				else
				{
					alert("No value Selected");
				}
				
			}
			//var url="CustomReport.aspx?Userid="+ document.getElementById('hidSelectedUsers').value;
				
		}*/
			
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
		
			
		/*	function AssignUsers()
			{					
				var coll = document.forms[0].lstUnAssignUser;
				var selIndex = coll.options.selectedIndex;				
				var len = coll.options.length;
				var num=0;				
				for (i = len- 1; i > -1 ; i--)
				{
					if((coll.options(i).selected == true) && (coll.options(i).value > 0))
					{
						num = i;
						var szSelectedDept = coll.options(i).value;
						var innerText = coll.options(i).text;
						document.forms[0].lstAssignUser.options[document.forms[0].lstAssignUser.length] = new Option(innerText,szSelectedDept)
						
						coll.remove(i);	
														
						//document.getElementById("btnSave").style.display="inline";			
								
					}										
				}	
				
				len = coll.options.length;			
				
				if(	num < len )
				{
					document.forms[0].lstUnAssignUser.options(num).selected = true;
				}	
				else
				{
					if(num>0)
						document.forms[0].lstUnAssignUser.options(num - 1).selected = true;
				}					
				
			}
			

		function UnAssignUsers()
			{					
				var UnassignableString = ""
				var Unassignable = UnassignableString.split(",")
				var gszAssignedString = ""				
				var Assigned = gszAssignedString.split(",")
				var coll = document.forms[0].lstAssignUser;
				var selIndex = coll.options.selectedIndex;
				var len = coll.options.length;		
				var num=0;						
				if(len==0) return;
				for (i = len- 1; i > -1 ; i--)
				{
					if((coll.options(i).selected == true) && (coll.options(i).value > 0))
					{	
							num = i;						
							var flag = true;
							var szSelectedDept = coll.options(i).value;
							var innerText = coll.options(i).text;
							for(j = 0; j < Unassignable.length ;j++)
							{
								for(k = 0; k < Assigned.length ;k++)
								{							
										if((szSelectedDept == Unassignable[j]) && (szSelectedDept == Assigned[k])) 
										{
											flag = false;
										}
								}
							}
							if(flag == true)
							{
								document.forms[0].lstUnAssignUser.options[document.forms[0].lstUnAssignUser.length] = new Option(innerText,szSelectedDept)					
								coll.remove(i);
															
							}
							else
							{
								//alert("Cannot unassign Department "+innerText+" because some divisions are associasted with it");
							}
					}			
				}
				var len = coll.options.length;
				if(len<1) return;
				if(	num < len )
				{
					document.forms[0].lstAssignUser.options(num).selected = true;
				}	
				else
				{
					document.forms[0].lstAssignUser.options(num - 1).selected = true;
				}
			}	*/		
			
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
	<body onload="top.topframe.main1.mousein = false;findMouseIn();BindEvent();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<DIV id="myTSMain" style="BEHAVIOR: url(/cms/cmsweb/htc/webservice.htc)"></DIV>
			<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
			<!-- To add bottom menu ends here --><asp:panel id="Panel1" Runat="server">
				<TABLE class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD class="pageHeader" id="tdClientTop" colSpan="4">
							<WEBCONTROL:GRIDSPACER id="grdSpacer" runat="server"></WEBCONTROL:GRIDSPACER></TD>
					</TR>
					<TR>
						<TD class="headereffectCenter" colSpan="4">Holder List Report Selection Criteria
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
							<BR>
							<INPUT class="clsButton" onclick="javascript:AssignPolicies();" type="button" value=">>"
								name="AssignPolicy"><BR>
							<BR>
							<INPUT class="clsButton" onclick="javascript:UnAssignPolicies();" type="button" value="<<"
								name="UnAssignPolicy">
						</TD>
						<TD class="midcolora" width="33%" colSpan="2">
							<asp:listbox id="lstAssignPolicy" Runat="server" SelectionMode="Multiple" Height="100px" Width="240px"></asp:listbox></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4">
							<cmsb:cmsbutton class="clsButton" id="btnSumbit" runat="server" Text="Display Report"></cmsb:cmsbutton></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<INPUT id="hidSelectedPolicy" type="hidden" name="hidSelectedPolicy" runat="server">
			<INPUT id="hidSelectedPolicyName" type="hidden" name="hidSelectedPolicyName" runat="server">
		</form>
	</body>
</HTML>

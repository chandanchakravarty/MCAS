<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AssignProfitcenterToDepartment.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AssignProfitcenterToDepartment" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>AssignProfitcenterToDepartment</TITLE>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<LINK href="Common.css" type="text/css" rel="stylesheet">
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<SCRIPT src="/cms/cmsweb/scripts/common.js"></SCRIPT>
		<SCRIPT src="/cms/cmsweb/scripts/form.js"></SCRIPT>
		<script language="javascript">
	
		function AssignPC() 
		{
			var colm = document.Form1.lbUnAssignPC;
			var setIndex = colm.options.selectedIndex; 
			var len = colm.options.length;
			var num=0; 
			for (i = len- 1; i > -1 ; i--) 
			{ 
				if((colm.options(i).selected == true) && (colm.options(i).value > 0)) 
				{ 
					num = i;
					var szSelectedPC = colm.options(i).value;  
					var innerText = colm.options(i).text; 
					document.Form1.lbAssignPC.options[document.Form1.lbAssignPC.length]= new Option(innerText,szSelectedPC) 
					colm.remove(i); 
				}
			} 
			len = colm.options.length;
			if(	num < len )
			{
				document.Form1.lbUnAssignPC.options(num).selected = true;
			}	
			else
			{
				document.Form1.lbUnAssignPC.options(num - 1).selected = true;
			}						
		}		
		
		
	 function UnAssignPC() 
	 {
		//var UnassignableString = "" 
		//var Unassignable = UnassignableString.split(",")
	     var gszAssignedString = document.getElementById("hidunassgnPC").value
		var Unassignalert = '<%=Unassignalert %>'
		var Assigned = gszAssignedString.split(",") 
		var colm = document.Form1.lbAssignPC;
		var setIndex = colm.options.selectedIndex;
		var len = colm.options.length;
		var num = 0;
		var Deptname = "";
		if (len == 0) return;
		for (i = len- 1; i >-1 ; i--)
		{
			if((colm.options(i).selected == true) &&(colm.options(i).value > 0))
			{ 
				num = i;
				var flag = true; 				
				var szSelectedPC =colm.options(i).value; 
				var innerText = colm.options(i).text;
				//for(j = 0; j < Unassignable.length ;j++) 
			//	{ 
					for(k = 0; k < Assigned.length ;k++) 
					{ 
						if(szSelectedPC== Assigned[k])//&& (szSelectedPC == Unassignable[j]) ) 
						{
						    flag = false;
						    Deptname = Deptname + innerText + ',';
						}
					}   
				// } 
				if(flag == true) 
				{
					document.Form1.lbUnAssignPC.options[document.Form1.lbUnAssignPC.length] = new Option(innerText,szSelectedPC) 
					colm.remove(i);
				} 
				// else
				//{ 
				//	alert("Cannot unassign Profit Center "+innerText+" because some departments are associasted with it"); 
				//} 
			}

}

if (Deptname != "") {
    alert(Deptname + ' ' + Unassignalert);
    return false;
}
		 var len = colm.options.length;
		if(	num < len )
		{
			document.Form1.lbAssignPC.options(num).selected = true;
		}	
		else
		{
			document.Form1.lbAssignPC.options(num - 1).selected = true;
		}			
	 }
	 	 
		 
		function CountAssignPC()
		 { 
			document.getElementById("hidPCID").value = "";
			var colm =document.Form1.lbAssignPC; 
			var len = colm.options.length; 
			for( k = 0;k <len ; k++)
			{
				var szSelectedPC = colm.options(k).value; 
				if(document.getElementById("hidPCID").value == "")
				{ 
						document.getElementById("hidPCID").value = szSelectedPC; 
				} 
				else
				{ 
						document.Form1.hidPCID.value = document.Form1.hidPCID.value + "," + szSelectedPC;
				}
			} 
		  }	  
		  
		function Initialize()
		{
			//document.getElementById('cmbDepartments').options.selectedIndex = -1;			
		}			
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" class="bodyBackGround" leftmargin="0" topmargin="0" onload="top.topframe.main1.mousein = false;findMouseIn();showScroll();Initialize(); "
		onresize="SmallScroll(); ">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<!--Start: to add band space below menu-->
		
		<!--End: band space below menu-->
		<div id="bodyHeight" class="pageContent">
			<form id="Form1" method="post" runat="server">
				<TABLE cellSpacing='0' cellPadding='0' class="tableWidth" border='0'>
					<tr>
						<td>
							<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
						</td>	
					</tr>	
					<tr>
						<td class="headereffectCenter" colSpan="3"><asp:label ID="Header1" Text="Assign Profit Centers to Department" runat="server"></asp:label></td>
					</tr>
					<tr>
						<TD class="pageHeader" width="100%" colSpan="4"><asp:label ID="Messages" Text="Please click on the buttons to assign or unassign 
							Profit Centers to Department." runat="server"></asp:label></TD>
					</tr>
					<tr>
						<td class="midcolorc" align="center" colSpan="3"><asp:label id="lblMessage" runat="server" Visible="False" cssclass="errmsg"></asp:label></td>
					</tr>
					
					<tr>
						<td class="midcolorc" align="center" colSpan="3"><asp:label ID="Department" Text="Department:" runat="server"></asp:label><asp:dropdownlist OnFocus="SelectComboIndex('cmbDepartments')"  id="cmbDepartments" Runat="server" AutoPostBack="True"></asp:dropdownlist>
						</td>
					</tr>
					<tr>
						<td class="midcolorc" align="center" width="37%"><asp:label ID="Unassigned" Text="Unassigned Profit Centers" runat="server"></asp:label></td>
						<td class="midcolorc" vAlign="middle" align="center" width="33%" rowSpan="7"><br>
							<br>
							<input class="clsButton" onclick="javascript:AssignPC();" type="button" value=">>" name="AssignDepts"><br>
							<br>
							<input class="clsButton" onclick="javascript:UnAssignPC();" type="button" value="<<" name="UnAssignDepts">
						</td>
						<td class="midcolorc" align="center" width="33%"><asp:label ID="assigned" Text="Assigned Profit Centers" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="lbUnAssignPC" Runat="server" SelectionMode="Multiple" Height="88px"></asp:listbox></td>
						<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="lbAssignPC" Runat="server" SelectionMode="Multiple" Height="88px"></asp:listbox></td>
					</tr>
				</TABLE>
				<table class="tableWidth" border="0">
					<tr>
						<td class="midcolorr" vAlign="middle" align="right" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="Server" text="Save"></cmsb:cmsbutton></td>
					</tr>
					<!-- Added to show scroll bar on the page-->
					<tr>
							<td class="iframsHeightMedium"></td></tr>
					<!--End Here-->
				</table>
				<INPUT id="hidPCID" type="hidden" name="hidPCID" runat="server">
				<INPUT id="hidunassgnPC" type="hidden" name="hidunassgnPC" runat="server">
				</form>
		</div>
	</BODY>
</HTML>













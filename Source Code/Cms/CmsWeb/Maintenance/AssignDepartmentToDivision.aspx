<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="AssignDepartmentToDivision.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AssignDepartmentToDivision" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AssignDepartmentToDivision</title>
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
		function AssignDepts()
		{
			var coll = document.AssignDivDept.lbUnAssignDept;
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
					document.AssignDivDept.lbAssignDept.options[document.AssignDivDept.lbAssignDept.length] = new Option(innerText,szSelectedDept)
					coll.remove(i);													
				}										
			}	
			len = coll.options.length;
			if(	num < len )
			{
				document.AssignDivDept.lbUnAssignDept.options(num).selected = true;
			}	
			else
			{
				document.AssignDivDept.lbUnAssignDept.options(num - 1).selected = true;
			}						
		}
		function UnAssignDepts()
		{
		    //var UnassignableString = ""
            //var Unassignable = UnassignableString.split(",")
		    var gszAssignedString = document.getElementById("hidunassgndept").value
		    var Unassignalert = '<%=Unassignalert %>'
			//var gszAssignedString = ""
			var Assigned = gszAssignedString.split(",")
			var coll = document.AssignDivDept.lbAssignDept;
			var selIndex = coll.options.selectedIndex;
			var len = coll.options.length;
			var num = 0;
			var Deptname = "";
			if (len == 0) return;
			for (i = len- 1; i > -1 ; i--)
			{
				if((coll.options(i).selected == true) && (coll.options(i).value > 0))
				{	
						num = i;
						var flag = true;
						var szSelectedDept = coll.options(i).value;
						var innerText = coll.options(i).text;
						//for(j = 0; j < Unassignable.length ;j++)
						//{
							for(k = 0; k < Assigned.length ;k++)
							{
							    if (szSelectedDept == Assigned[k])// && (szSelectedDept == Unassignable[j])) 
									{
									    flag = false;
									    Deptname = Deptname + innerText + ',';
									}
							}
					//	}
						if(flag == true)
						{
							document.AssignDivDept.lbUnAssignDept.options[document.AssignDivDept.lbUnAssignDept.length] = new Option(innerText,szSelectedDept)					
							coll.remove(i);
														
						}
						//else
						//{
						//	alert("Cannot unassign Department "+innerText+" because some divisions are associasted with it");
						//}
				}
}
if (Deptname != "") {
    alert(Deptname + ' ' + Unassignalert);
    return false;
}
			var len = coll.options.length;
			if(	num < len )
			{
				document.AssignDivDept.lbAssignDept.options(num).selected = true;
			}	
			else
			{
				document.AssignDivDept.lbAssignDept.options(num - 1).selected = true;
			}					
		}
		
		function CountAssignDepts()
		{
			document.getElementById("hidDeptID").value = "";
			var coll = document.AssignDivDept.lbAssignDept;
			var len = coll.options.length;
			for( k = 0;k < len ; k++)
			{
				var szSelectedDept = coll.options(k).value;
				if (document.getElementById("hidDeptID").value == "")
				{
					document.getElementById("hidDeptID").value =  szSelectedDept;
				}
				else
				{
					document.AssignDivDept.hidDeptID.value = document.AssignDivDept.hidDeptID.value + "," + szSelectedDept;
				}
			}
//			document.AssignDivDept.TextBox1.style.display = 'none';		
		}				
		function Initialize()
		{
			//document.getElementById('cmbDivisions').options.selectedIndex = -1;
		}				
		</script>
	</HEAD>
	<BODY  oncontextmenu = "return false;" class="bodyBackGround" leftmargin="0" topmargin="0" onload="top.topframe.main1.mousein = false;findMouseIn();showScroll();Initialize();"
		onresize="SmallScroll();">
		<!-- To add bottom menu -->
		<webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<!--Start: to add band space below menu-->
		
		<!--End: band space below menu-->
		<div id="bodyHeight" class="pageContent">
			<form id="AssignDivDept" method="post" runat="server">
				<TABLE cellSpacing='0' cellPadding='0' class="tableWidth" border='0'>
					<tr>
						<td>
							<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
						</td>	
					</tr>	
					<tr>
						<td class="headereffectCenter" colSpan="3"><asp:label ID="header" runat="server" >Assign Departments to Divisions</asp:label></td>
					</tr>
					<tr>
						<TD class="pageHeader" width="100%" colSpan="4"><asp:label runat="server" ID="Message"> Please click on the buttons to assign or unassign 
							Departments to Divisions.</asp:label></TD>
					</tr>
					<tr>
						<td class="midcolorc" align="center" colSpan="3">
							<asp:label id="lblMessage" runat="server" Visible="False" cssclass="errmsg"></asp:label>
						</td>
					</tr>
					
					<tr>
						<td class="midcolorc" align="center" colSpan="3"><asp:label runat="server" ID="lblDivision">Division:</asp:label><asp:dropdownlist OnFocus="SelectComboIndex('cmbDivisions')"  id="cmbDivisions" Runat="server" AutoPostBack="True">
						
						</asp:dropdownlist>
						</td>
					</tr>
					<tr>
						<td class="midcolorc" align="center" width="37%"><asp:label ID="lblUnassigned"  Text="Unassigned Departments" runat="server"></asp:label></td>
						<td class="midcolorc" align="center" valign="middle" width="33%" rowspan="7"><br>
							<br>
							<input class="clsButton" onclick="javascript:AssignDepts();"  type="button" value=">>" name="AssignDivisions"><br>
							<br>
							<input class="clsButton" onclick="javascript:UnAssignDepts();" type="button" value="<<"
								name="UnAssignDivisions">
						</td>
						<td class="midcolorc" align="center" width="33%"><asp:label ID=lblAssigned Text="Assigned Departments" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="lbUnAssignDept" Runat="server" SelectionMode="Multiple" Height="88px"></asp:listbox></td>
						<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="lbAssignDept" Runat="server" SelectionMode="Multiple" Height="88px"></asp:listbox></td>
					</tr>
				</TABLE>
				<table width="100%" class="tableWidth" border="0">
					<tr>
						<td class="midcolorr" vAlign="middle" align="right" colSpan="3"><cmsb:cmsbutton id="btnSave" class="clsButton" runat="Server" text="Save"></cmsb:cmsbutton></td>
					</tr>
					<!-- Added to show scroll bar on the page-->
					<tr>
							<td class="iframsHeightMedium"></td></tr>
					<!--End here-->
				</table>
				<INPUT id="hidDeptID" runat="server" type="hidden">
				<INPUT id="hidunassgndept" type="hidden" name="hidunassgndept" runat="server">
			</form>
		</div>
	</BODY>
</HTML>

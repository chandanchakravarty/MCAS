<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AssignAgency.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AssignAgency" %>

<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Assign_Agency</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<LINK href="Common.css" type="text/css" rel="stylesheet">
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<SCRIPT src="/cms/cmsweb/scripts/menubar.js"></SCRIPT>
		<script language="javascript">
			function AssignAgency()
			{
				var coll = document.Assign_Agency.lbUnassignAgency;
				var selIndex = coll.options.selectedIndex;
				var len = coll.options.length;
				var num=0;
				for (i = len- 1; i > -1 ; i--)
				{
					if((coll.options(i).selected == true) && (coll.options(i).value > 0))
					{
						num = i;
						var szSelectedAgency = coll.options(i).value;
						var innerText = coll.options(i).text;
						document.Assign_Agency.lbAssignAgency.options[document.Assign_Agency.lbAssignAgency.length] = new Option(innerText,szSelectedAgency)
						coll.remove(i);	
																	
					}										
				}	
				
					
			}
			function UnAssignAgency()
			{	
				var coll = document.Assign_Agency.lbAssignAgency;
				var selIndex = coll.options.selectedIndex;
				var len = coll.options.length;
				var num=0;
				for (i = len- 1; i > -1 ; i--)
				{
					if((coll.options(i).selected == true) && (coll.options(i).value > 0))
					{
						num = i;
						var szSelectedAgency = coll.options(i).value;
						var innerText = coll.options(i).text;
						document.Assign_Agency.lbUnassignAgency.options[document.Assign_Agency.lbUnassignAgency.length] = new Option(innerText,szSelectedAgency)
						coll.remove(i);	
																	
					}										
				}	
				
					
			}
			
			function CountUnassignAgency()
			{
				document.getElementById("hidAgencyID").value = "";
				var coll = document.Assign_Agency.lbUnassignAgency;
				var len = coll.options.length;
				for( k = 0;k < len ; k++)
				{
					var szSelectedState = coll.options(k).value;
					if (document.getElementById("hidAgencyID").value == "")
					{
						document.getElementById("hidAgencyID").value =  szSelectedState;
					}
					else
					{
						document.getElementById("hidAgencyID").value = document.getElementById("hidAgencyID").value + "," + szSelectedState;
					}
				}
			
		}		
		
		function CountAssignAgency()
			{
				document.getElementById("hidAssignAgencyID").value = "";
				var coll = document.Assign_Agency.lbUnassignAgency;
				var len = coll.options.length;
				for( k = 0;k < len ; k++)
				{
					var szSelectedState = coll.options(k).text;
					if (document.getElementById("hidAssignAgencyID").value == "")
					{
						document.getElementById("hidAssignAgencyID").value =  szSelectedState;
					}
					else
					{
						document.getElementById("hidAssignAgencyID").value = document.getElementById("hidAssignAgencyID").value + ", " + szSelectedState;
					}
				}
			
		}		
		
		</script>
	</HEAD>
	<BODY class="bodyBackGround" leftmargin="0" topmargin="0" onload="top.topframe.main1.mousein = false;findMouseIn();showScroll();"
		onresize="SmallScroll();">
		<div id="bodyHeight" class="pageContent">
			<FORM id="Assign_Agency" method="post" runat="server">
				<!--TABLE cellSpacing='0' cellPadding='0' class="tableWidth" border='0'-->
				<TABLE width="100%" align="center" border="0">
					<tr>
						<td class="headereffectCenter" colSpan="4"><asp:label ID="CapAssign" runat="server">Assign Agency to User</asp:label></td>
					</tr>
					<tr>
						<TD class="pageHeader" width="100%" colSpan="4"><asp:label ID="CapLabel" runat="server">Please click on the buttons to 
							assign or unassign Agency to User.</asp:label></TD>
					</tr>
					<tr>
						<td class="midcolorc" align="center" colSpan="3">
							<asp:label id="lblMessage" runat="server" Visible="False" cssclass="errmsg"></asp:label>
						</td>
					</tr>
					<tr>
						<td class="midcolorc" align="center" width="37%"><asp:label runat="server" ID="CapUnAssigned">Unassigned Agencies</asp:label></td> <%--Changed by Aditya on 14-oct-2011 for TFS Bug # 1895--%>
						<td class="midcolorc" align="center" valign="middle" width="33%" rowspan="7"><br>
							<br>
							<input class="clsButton" type="button" onclick="javascript:UnAssignAgency();" value=">>" name="UnAssignAgencies">
							<br>
							<br>
							<input class="clsButton" type="button" onclick="javascript:AssignAgency();" value="<<" name="AssignAgencies">
						</td>
						<td class="midcolorc" align="center" width="33%"><asp:label runat="server" ID="CapAssigned">Assigned Agencies</asp:label></td>
					</tr>
					<tr>
						<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="lbAssignAgency" Runat="server" SelectionMode="Multiple" Height="88px"></asp:listbox></td>
						<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="lbUnassignAgency" Runat="server" SelectionMode="Multiple" Height="88px"></asp:listbox></td>
					</tr>
				</TABLE>
				<table width="100%" align="center" border="0">
					<tr>
						<td class="midcolorr" vAlign="middle" align="right" colSpan="4"><cmsb:cmsbutton id="btnSave" class="clsButton" runat="Server" text="Save"></cmsb:cmsbutton></td>
					</tr>
					<!-- Added to show scroll bar on the page-->
					<tr>
						<td class="iframsHeightMedium"></td>
					</tr>
					<!--End here-->
				</table>
				<INPUT id="hidUserID" runat="server" type="hidden" NAME="hidUserID">
				<INPUT id="hidAgencyID" runat="server" type="hidden" NAME="hidUserID">
				<INPUT id="hidAssignAgencyID" runat="server" type="hidden" NAME="hidAssignAgencyID">
			</FORM>
		</div>
	</BODY>
</HTML>

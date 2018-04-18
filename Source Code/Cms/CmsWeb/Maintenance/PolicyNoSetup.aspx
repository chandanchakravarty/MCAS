<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="ClsPolicyNoSetup.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.ClsPolicyNoSetup" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>MNT_LOB_MASTER</title>
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
		function AssignStates()
		{
			var coll = document.MNT_LOB_MASTER.lbUnAssignStates;
			var selIndex = coll.options.selectedIndex;
			var len = coll.options.length;
			var num=0;
			for (i = len- 1; i > -1 ; i--)
			{
				if((coll.options(i).selected == true) && (coll.options(i).value > 0))
				{
					num = i;
					var szSelectedStates = coll.options(i).value;
					var innerText = coll.options(i).text;
					document.MNT_LOB_MASTER.lbAssignStates.options[document.MNT_LOB_MASTER.lbAssignStates.length] = new Option(innerText,szSelectedStates)
					coll.remove(i);	
																	
				}										
			}	
			len = coll.options.length;
			if(	num < len )
			{
				document.MNT_LOB_MASTER.lbUnAssignStates.options(num).selected = true;
			}	
			else
			{
				document.MNT_LOB_MASTER.lbUnAssignStates.options(num - 1).selected = true;
			}						
		}
		function UnAssignStates()
		{	
			var UnassignableString = ""
			var Unassignable = UnassignableString.split(",")
			var gszAssignedString = ""
			var Assigned = gszAssignedString.split(",")
			var coll = document.MNT_LOB_MASTER.lbAssignStates;
			var selIndex = coll.options.selectedIndex;
			var len = coll.options.length;
			for (i = len- 1; i > -1 ; i--)
			{
				if((coll.options(i).selected == true) && (coll.options(i).value > 0))
				{	
						num = i;
						var flag = true;
						var szSelectedStates = coll.options(i).value;
						var innerText = coll.options(i).text;
						for(j = 0; j < Unassignable.length ;j++)
						{
							for(k = 0; k < Assigned.length ;k++)
							{							
									if((szSelectedStates == Unassignable[j]) && (szSelectedStates == Assigned[k])) 
									{
										flag = false;
									}
							}
						}
						if(flag == true)
						{
							document.MNT_LOB_MASTER.lbUnAssignStates.options[document.MNT_LOB_MASTER.lbUnAssignStates.length] = new Option(innerText,szSelectedStates)					
							coll.remove(i);
														
						}
						else
						{
							alert("Cannot unassign State "+innerText+" because some Lobs are associasted with it");
						}
				}			
			}
			var len = coll.options.length;
			if(	num < len )
			{
				document.MNT_LOB_MASTER.lbAssignStates.options(num).selected = true;
			}	
			else
			{
				document.MNT_LOB_MASTER.lbAssignStates.options(num - 1).selected = true;
			}					
		}
		function CountAssignDepts()
		{
			document.getElementById("hidStateID").value = "";
			var coll = document.MNT_LOB_MASTER.lbAssignStates;
			var len = coll.options.length;
			for( k = 1;k < len ; k++)
			{
				var szSelectedState = coll.options(k).value;
				if (document.getElementById("hidStateID").value == "")
				{
					document.getElementById("hidStateID").value =  szSelectedState;
				}
				else
				{
					document.MNT_LOB_MASTER.hidStateID.value = document.MNT_LOB_MASTER.hidStateID.value + "," + szSelectedState;
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
		<div id="bodyHeight" class="pageContent">
			<FORM id="MNT_LOB_MASTER" method="post" runat="server">
				<TABLE cellSpacing='0' cellPadding='0' class="tableWidth" border='0'>
					<tr>
						<td>
							<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
						</td>
					</tr>
					<tr>
						<td class="headereffectCenter" colSpan="3">
                            <asp:Label ID="lblassignstate" runat="server" Text="Label"></asp:Label></td>
					</tr>
					<tr>
						<TD class="pageHeader" width="100%" colSpan="4">
                            <asp:Label ID="lblpleaseassign" runat="server" Text="Label"></asp:Label></TD>
					</tr>
					<tr>
						<td class="midcolorc" align="center" colSpan="3">
							<asp:label id="lblMessage" runat="server" Visible="False" cssclass="errmsg"></asp:label>
						</td>
					</tr>
					<tr>
						<td class="midcolorc" align="center" colSpan="3">
                            <asp:Label ID="lblproduct" runat="server" Text="Label"></asp:Label><asp:dropdownlist OnFocus="SelectComboIndex('cmbLOB')" id="cmbLOB" Runat="server" AutoPostBack="True"></asp:dropdownlist>
						</td>
					</tr>
					<tr>
						<td class="midcolorc" align="center" width="37%">
                            <asp:Label ID="lblunassigned" runat="server" Text="Label"></asp:Label></td>
						<td class="midcolorc" align="center" valign="middle" width="33%" rowspan="7"><br>
							<br>
							<input class="clsButton" onclick="javascript:AssignStates();" type="button" value=">>"
							name="AssignNewStates">
							<br>
							<br>
							<input class="clsButton" onclick="javascript:UnAssignStates();" type="button" value="<<"
								name="UnAssignNewStates">
						</td>
						<td class="midcolorc" align="center" width="33%">
						<asp:Label ID="lblassigned" runat="server" Text="Label"></asp:Label></td>
					</tr>
					<tr>
						<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="lbUnAssignStates" Runat="server" SelectionMode="Multiple" Height="88px"></asp:listbox></td>
						<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="lbAssignStates" Runat="server" SelectionMode="Multiple" Height="88px"></asp:listbox></td>
					</tr>
				</TABLE>
				<table width="100%" class="tableWidth" border="0">
					<tr>
						<td class="midcolorr" vAlign="middle" align="right" colSpan="3"><cmsb:cmsbutton id="btnSave" class="clsButton" runat="Server" text="Save"></cmsb:cmsbutton></td>
					</tr>
					<!-- Added to show scroll bar on the page-->
					<tr>
						<td class="iframsHeightMedium"></td>
					</tr>
					<!--End here-->
				</table>
				<INPUT id="hidStateID" runat="server" type="hidden" NAME="hidStateID">
			</FORM>
		</div>
	</BODY>
</HTML>

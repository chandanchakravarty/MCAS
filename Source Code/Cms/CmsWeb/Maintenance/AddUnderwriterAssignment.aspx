<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddUnderwriterAssignment.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AddUnderwriterAssignment" %>
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
		function AssignUnderwriter()
		{			
			var coll = document.AssignLOBUnderwriter.lbUnAssignUnderwriter;
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
					document.AssignLOBUnderwriter.lbAssignUnderwriter.options[document.AssignLOBUnderwriter.lbAssignUnderwriter.length] = new Option(innerText,szSelectedDept)
					coll.remove(i);										
					document.getElementById("btnSave").style.display="inline";			
				}										
			}	
			len = coll.options.length;			
			if(	num < len )
			{
				document.AssignLOBUnderwriter.lbUnAssignUnderwriter.options(num).selected = true;
			}	
			else
			{
				if(num>0)
					document.AssignLOBUnderwriter.lbUnAssignUnderwriter.options(num - 1).selected = true;
			}	
			
		}
		function UnAssignUnderwriter() {

		    //var UnassignableString = document.getElementById("hidunassgnuw").value
			//var Unassignable = UnassignableString.split(",")
		    var gszAssignedString = document.getElementById("hidunassgnuw").value
		    var Unassignalert = '<%=Unassignalert %>'
			var Assigned = gszAssignedString.split(",")
			var coll = document.AssignLOBUnderwriter.lbAssignUnderwriter;
			var selIndex = coll.options.selectedIndex;
			var len = coll.options.length;
			var num = 0;
			var Deptname="";			
			if(len==0) return;
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
							    if ((szSelectedDept == Assigned[k]))// && (szSelectedDept == Assigned[k])) 
									{
									    flag = false;
									    Deptname = Deptname + innerText + ',';
									}
 						}
						//}
						if(flag == true)
						{
							document.AssignLOBUnderwriter.lbUnAssignUnderwriter.options[document.AssignLOBUnderwriter.lbUnAssignUnderwriter.length] = new Option(innerText,szSelectedDept)					
							coll.remove(i);
														
						}
						//else
						//{
						    //alert(innerText+' '+Unassignalert);
						    //return false;
						//}
		}


}

if (Deptname != "") {
    alert(Deptname + ' ' + Unassignalert);
    return false;
}

 

                
			var len = coll.options.length;
			if(len<1) return;
			if(	num < len )
			{
				document.AssignLOBUnderwriter.lbAssignUnderwriter.options(num).selected = true;
			}	
			else
			{
				document.AssignLOBUnderwriter.lbAssignUnderwriter.options(num - 1).selected = true;
			}
		}
		
		function CountAssignUnderwriter()
		{
		    document.getElementById("hidUnderwriterID").value = "";
		    var coll = document.AssignLOBUnderwriter.lbAssignUnderwriter;
		    var len = coll.options.length;

		    if (len == 0) {

		        EnableValidator('rfvlbAssignUnderwriter', true);
		        document.getElementById("rfvlbAssignUnderwriter").style.display = 'inline'
		        return false;
		    }
		    else {

		        EnableValidator('rfvlbAssignUnderwriter', false);
		        document.getElementById("rfvlbAssignUnderwriter").style.display = 'none'


		        for (k = 0; k < len; k++) {
		            var szSelectedDept = coll.options(k).value;
		            var szSelectedDeptNames = coll.options(k).text;
		            if (document.getElementById("hidUnderwriterID").value == "") {
		                document.getElementById("hidUnderwriterID").value = szSelectedDept;
		                document.getElementById("hidUnderwriterNames").value = szSelectedDeptNames;
		            }
		            else {
		                document.AssignLOBUnderwriter.hidUnderwriterID.value = document.AssignLOBUnderwriter.hidUnderwriterID.value + "," + szSelectedDept;
		                document.AssignLOBUnderwriter.hidUnderwriterNames.value = document.AssignLOBUnderwriter.hidUnderwriterNames.value + "," + szSelectedDeptNames;
		            }
		        }
		    }


			CountAssignMarketeer();
//			document.AssignLOBUnderwriter.TextBox1.style.display = 'none';		
		}				
		//Start
		function AssignMarketeer()
		{			
			var coll = document.AssignLOBUnderwriter.lbUnAssignMarketeer;
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
					document.AssignLOBUnderwriter.lbAssignMarketeer.options[document.AssignLOBUnderwriter.lbAssignMarketeer.length] = new Option(innerText,szSelectedDept)
					coll.remove(i);										
					document.getElementById("btnSave").style.display="inline";			
				}										
			}	
			len = coll.options.length;			
			if(	num < len )
			{
				document.AssignLOBUnderwriter.lbUnAssignMarketeer.options(num).selected = true;
			}	
			else
			{
				if(num>0)
					document.AssignLOBUnderwriter.lbUnAssignMarketeer.options(num - 1).selected = true;
			}	
			
		}
		function UnAssignMarketeer() {
		 
			var UnassignableString = ""
			var Unassignable = UnassignableString.split(",")
			var gszAssignedString = ""
			var Assigned = gszAssignedString.split(",")
			var coll = document.AssignLOBUnderwriter.lbAssignMarketeer;
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
							document.AssignLOBUnderwriter.lbUnAssignMarketeer.options[document.AssignLOBUnderwriter.lbUnAssignMarketeer.length] = new Option(innerText,szSelectedDept)					
							coll.remove(i);
														
						}
						else
						{
							alert("Cannot unassign Department "+innerText+" because some divisions are associasted with it");
						}
		}

		
			}
			var len = coll.options.length;
			if(len<1) return;
			if(	num < len )
			{
				document.AssignLOBUnderwriter.lbAssignMarketeer.options(num).selected = true;
			}	
			else
			{
				document.AssignLOBUnderwriter.lbAssignMarketeer.options(num - 1).selected = true;
			}
		}
		
		function CountAssignMarketeer()
		{
			document.getElementById("hidMarketeerID").value = "";
			var coll = document.AssignLOBUnderwriter.lbAssignMarketeer;
			var len = coll.options.length;
			for( k = 0;k < len ; k++)
			{
			    var szSelectedDept = coll.options(k).value;
			    var szSelectedDeptNames = coll.options(k).text;
			    
				if (document.getElementById("hidMarketeerID").value == "")
				{
				    document.getElementById("hidMarketeerID").value = szSelectedDept;
				    document.getElementById("hidMarketeerNames").value = szSelectedDeptNames;					    
				}
				else
				{
				    document.AssignLOBUnderwriter.hidMarketeerID.value = document.AssignLOBUnderwriter.hidMarketeerID.value + "," + szSelectedDept;
				    document.AssignLOBUnderwriter.hidMarketeerNames.value = document.AssignLOBUnderwriter.hidMarketeerNames.value + "," + szSelectedDeptNames;				    
				}
			}
//			document.AssignLOBUnderwriter.TextBox1.style.display = 'none';		
		}				
		//End
		function Initialize()
		{
			//document.getElementById('cmbLOB').options.selectedIndex = -1;
		}	
         </script>
</HEAD>
	<BODY class="bodyBackGround" leftMargin="0" topMargin="0">
		<!--Start: to add band space below menu-->
		<!--End: band space below menu-->
		<form id="AssignLOBUnderwriter" method="post" runat="server">
			<TABLE cellSpacing='0' cellPadding='0' class="tableWidthHeader" border='0'>
				<tr>
					<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
				</tr>
				<tr>
					<td class="headereffectCenter" colSpan="3"><asp:label ID="capheader" runat="server" >Assign  UW/Marketing to Product</asp:label></td>
				</tr>
				<tr>
					<TD class="pageHeader" width="100%" colSpan="4"><asp:label ID="capMessages" runat="server"> Please select from dropdownlist to 
						assign or unassign  UW/Marketing to Product.</asp:label></TD>
				</tr>
				<tr>
					<td class="midcolorc" align="center" colSpan="3"><asp:label id="lblMessage" runat="server" cssclass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<tr>
					<td class="midcolorc" align="center" colSpan="3"><asp:label ID=capProduct runat="server">Product:</asp:label><asp:DropDownList ID="cmbLOB" Runat="server" OnFocus="SelectComboIndex('cmbLOB')" AutoPostBack="True"></asp:DropDownList>
					<asp:RequiredFieldValidator ID=rfvLob Runat=server Display=Dynamic ControlToValidate=cmbLOB ErrorMessage="Please select Product"></asp:RequiredFieldValidator></td>
				</tr>
				<tr>
					<td class="midcolorc" align="center" width="37%"><asp:label id="capUnassignUnderwriter" runat="server">Unassigned Underwriter</asp:label></td>
					<td class="midcolorc" vAlign="middle" align="center" width="10%" rowSpan="2"><br>
						<br>
						<input class="clsButton" runat="server"  onclick="javascript:AssignUnderwriter();" type="button" value=">>"
							name="AssignUnderwriters" id="AssignUnderwriters"><br>
						<br>
						<input class="clsButton" runat="server" onclick="javascript:UnAssignUnderwriter();" type="button" value="<<"
                         name="UnAssignUnderwriters" id="UnAssignUnderwriters" >
							
					</td>
					<td class="midcolorc" align="center" width="33%"><asp:label id="capAssignedUnderwriter" runat="server">Assigned Underwriter</asp:label></td>				</tr>
				<tr>
					<td class="midcolorc" align="center" width="33%" ><asp:listbox id="lbUnAssignUnderwriter" Runat="server" Height="88px" Width="40%" SelectionMode="Multiple"></asp:listbox></td>
					<td class="midcolorc" align="center" width="33%" ><asp:listbox id="lbAssignUnderwriter" Runat="server" Height="88px"  Width="40%" SelectionMode="Multiple"></asp:listbox><br />
					<asp:RequiredFieldValidator ID="rfvlbAssignUnderwriter" Runat="server"  Display="Dynamic" ControlToValidate="lbAssignUnderwriter"></asp:RequiredFieldValidator>
					</td>
				</tr>
				
				
			</TABLE>
			<table cellSpacing='0' cellPadding='0' class="tableWidthHeader" border='0' >
				<!--Start  -->
				<tr>
					<td class="midcolorc" align="center" width="37%"><asp:label id="capUnassignMarketeer" runat="server">Unassigned Marketing</asp:label></td>
					<td class="midcolorc" vAlign="middle" align="center" width="10%" rowSpan="2"><br>
						<br>
						<input class="clsButton" runat="server"  onclick="javascript:AssignMarketeer();" type="button" value=">>"
							name="AssignMarketeers" id="AssignMarketeers"><br>
						<br>
						<input class="clsButton" runat="server" onclick="javascript:UnAssignMarketeer();" type="button" value="<<"
							name="UnAssignMarketeers" id="UnAssignMarketeers">
					</td>
					<td class="midcolorc" align="center" width="33%"><asp:label id="capAssignedMarketeer" runat="server">Assigned Marketing</asp:label>
                       
                    </td>
				</tr>
				<tr>
					<td class="midcolorc" align="center" width="33%" ><asp:listbox id="lbUnAssignMarketeer" Runat="server" Height="88px" Width="40%" SelectionMode="Multiple"></asp:listbox></td>
					<td class="midcolorc" align="center" width="33%" >
                        <asp:listbox id="lbAssignMarketeer" Runat="server" Height="88px" Width="40%" 
                            SelectionMode="Multiple" ></asp:listbox></td>
				</tr>
				<!--End  -->
			</table>
			<table class="tableWidthHeader" border="0">
				<tr>
					<td class="midcolorr" vAlign="middle" align="right" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="Server" text="Save"></cmsb:cmsbutton></td>
				</tr>
				<!-- Added to show scroll bar on the page-->
				<tr>
					<td class="iframsHeightMedium"></td>
				</tr>
				<!--End here--></table>
			<INPUT id="hidUnderwriterID" type="hidden" name="hidUnderwriterID" runat="server">
			<INPUT id="hidMarketeerID" type="hidden" name="hidMarketeerID" runat="server">
			<INPUT id="hidUnderwriterNames" type="hidden" name="hidUnderwriterNames" runat="server">
		    <INPUT id="hidMarketeerNames" type="hidden" name="hidMarketeerNames" runat="server">
		    <INPUT id="hidunassgnuw" type="hidden" name="hidunassgnuw" runat="server">
		    <INPUT id="hidunassgnuwnm" type="hidden" name="hidunassgnuwnm" runat="server">
		</form>
	</BODY>
</HTML>

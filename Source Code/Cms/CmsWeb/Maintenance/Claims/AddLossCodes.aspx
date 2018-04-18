<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddLossCodes.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Claims.AddLossCodes" validateRequest=false  %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>MNT_AGENCY_LIST</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript">
		
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";

			function AddData()
			{
				ChangeColor();
				DisableValidators();
				document.getElementById('hidLOSS_CODE_ID').value	=	'New';				
			}			
			function ShowControls(flag)
			{
				if(flag)
				{
					document.getElementById("cmbUnAssignLossCodes").style.display = "inline";
					document.getElementById("cmbAssignLossCodes").style.display = "inline";
					document.getElementById("capUnassignLossCodes").style.display = "inline";
					document.getElementById("capAssignedLossCodes").style.display = "inline";
					document.getElementById("btnAssignLossCodes").style.display = "inline";
					document.getElementById("btnUnAssignLossCodes").style.display = "inline";
					document.getElementById("btnUnAssignLossCodes").style.display = "inline";
					//document.getElementById("btnSave").style.display = "inline";
				}
				else
				{
					document.getElementById("cmbUnAssignLossCodes").style.display = "none";
					document.getElementById("cmbAssignLossCodes").style.display = "none";
					document.getElementById("capUnassignLossCodes").style.display = "none";
					document.getElementById("capAssignedLossCodes").style.display = "none";
					document.getElementById("btnAssignLossCodes").style.display = "none";
					document.getElementById("btnUnAssignLossCodes").style.display = "none";
					document.getElementById("btnUnAssignLossCodes").style.display = "none";
					document.getElementById("btnSave").style.display = "none";
				}
					
			}
			function cmbLOB_ID_Changed()
			{
				if(document.getElementById("cmbLOB_ID"))
				{
					if(document.getElementById("cmbLOB_ID").selectedIndex<1)
						ShowControls(false);
					else
						ShowControls(true);
				}
			}
			function AssignLossCodes()
			{					
				var coll = document.CLM_LOSS_CODES.cmbUnAssignLossCodes;
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
						document.CLM_LOSS_CODES.cmbAssignLossCodes.options[document.CLM_LOSS_CODES.cmbAssignLossCodes.length] = new Option(innerText,szSelectedDept)
						coll.remove(i);										
						document.getElementById("btnSave").style.display="inline";			
						//document.getElementById("btnReset").style.display="inline";			
					}										
				}	
				len = coll.options.length;			
				
				if(	num < len )
				{
					document.CLM_LOSS_CODES.cmbUnAssignLossCodes.options(num).selected = true;
				}	
				else
				{
					if(num>0)
						document.CLM_LOSS_CODES.cmbUnAssignLossCodes.options(num - 1).selected = true;
				}					
				
			}
			function UnAssignLossCodes()
			{					
				var UnassignableString = ""
				var Unassignable = UnassignableString.split(",")
				var gszAssignedString = ""				
				var Assigned = gszAssignedString.split(",")
				var coll = document.CLM_LOSS_CODES.cmbAssignLossCodes;
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
								document.CLM_LOSS_CODES.cmbUnAssignLossCodes.options[document.CLM_LOSS_CODES.cmbUnAssignLossCodes.length] = new Option(innerText,szSelectedDept)					
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
					document.CLM_LOSS_CODES.cmbAssignLossCodes.options(num).selected = true;
				}	
				else
				{
					document.CLM_LOSS_CODES.cmbAssignLossCodes.options(num - 1).selected = true;
				}
			}
			
			function CountAssignLossCodes()
			{
				document.getElementById("hidLOSS_CODE_TYPE").value = "";
				var coll = document.CLM_LOSS_CODES.cmbAssignLossCodes;
				var len = coll.options.length;
				for( k = 0;k < len ; k++)
				{
					var szSelectedDept = coll.options(k).value;
					if (document.getElementById("hidLOSS_CODE_TYPE").value == "")
					{
						document.getElementById("hidLOSS_CODE_TYPE").value =  szSelectedDept;
					}
					else
					{
						document.CLM_LOSS_CODES.hidLOSS_CODE_TYPE.value = document.CLM_LOSS_CODES.hidLOSS_CODE_TYPE.value + "," + szSelectedDept;
					}
				}				
	//			document.CLM_LOSS_CODES.TextBox1.style.display = 'none';		
			}	
			function ResetTheForm()
			{
				//ChangeColor();
				DisableValidators();	
				document.getElementById("hidRESET").value="1";				
				document.location.href = document.location.href;
				return false;
			}
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="ApplyColor();cmbLOB_ID_Changed();">
		<FORM id="CLM_LOSS_CODES" method="post" runat="server">
			<TABLE cellSpacing='0' cellPadding='0' class="tableWidthHeader" border='0'>
				<tr>
					<TD class="pageHeader" width="100%" colSpan="4"> 
                        <asp:Label ID="capbtnassign" runat="server" Text="Label"></asp:Label>
						</TD>
				</tr>
				<tr>
					<td class="midcolorc" align="center" colSpan="3"><asp:label id="lblMessage" runat="server" cssclass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<tr id="trLOB_ID" runat="server">
					<td class="midcolorc" align="center" colSpan="3"><asp:Label ID="capLOB_ID" Runat="server"></asp:Label><asp:DropDownList ID="cmbLOB_ID" Runat="server" OnFocus="SelectComboIndex('cmbLOB_ID')"></asp:DropDownList><br>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						&nbsp;&nbsp;
						<asp:RequiredFieldValidator ID="rfvLOB_ID" Runat="server" Display="Dynamic" ControlToValidate="cmbLOB_ID" ErrorMessage="Please select Product"></asp:RequiredFieldValidator></td>
				</tr>
				<tr id="trLossCodes" runat="server">
					<td class="midcolorc" align="center" width="37%"><asp:label id="capUnassignLossCodes" runat="server"></asp:label></td>
					<td class="midcolorc" vAlign="middle" align="center" width="33%" rowSpan="7"><br>
						<br>
						<input class="clsButton" runat="server" onclick="javascript:AssignLossCodes();" type="button"
							value=">>" name="btnAssignLossCodes" id="btnAssignLossCodes"><br>
						<br>
						<input class="clsButton" runat="server" onclick="javascript:UnAssignLossCodes();" type="button"
							value="<<" name="btnUnAssignLossCodes" id="btnUnAssignLossCodes">
					</td>
					<td class="midcolorc" align="center" width="33%"><asp:label id="capAssignedLossCodes" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="cmbUnAssignLossCodes" Runat="server" Width="300px" Height="150px" SelectionMode="Multiple"></asp:listbox></td>
					<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="cmbAssignLossCodes" Runat="server" Width="300px"  Height="150px" SelectionMode="Multiple"></asp:listbox></td>
				</tr>
			</TABLE>
			<table class="tableWidthHeader" border="0">
				<tr>
					<td class="midcolora" valign="middle" align="left" colspan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
					<td class="midcolorr" vAlign="middle" align="left" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="Server" text="Save"></cmsb:cmsbutton></td>
				</tr>
				<!-- Added to show scroll bar on the page-->
				<tr>
					<td class="iframsHeightMedium"></td>
				</tr>
				<!--End here--></table>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidRESET" type="hidden" value="0" name="hidRESET" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidLOSS_CODE_ID" type="hidden" value="0" name="hidLOSS_CODE_ID" runat="server">
			<INPUT id="hidLOSS_CODE_TYPE" type="hidden" value="0" name="hidLOSS_CODE_TYPE" runat="server">
			<INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidLOSS_CODE_ID').value,true);			
		</script>
	</BODY>
</HTML>

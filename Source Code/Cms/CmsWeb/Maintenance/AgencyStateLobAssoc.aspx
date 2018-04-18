<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AgencyStateLobAssoc.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AgencyStateLobAssoc" validateRequest=false  %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>MNT_AGENCY_STATE_LOB_ASSOC</title>
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
				document.getElementById('hidMNT_AGENCY_STATE_LOB_ASSOC_ID').value	=	'New';				
			}
			function ShowControls(flag)
			{		
				if(flag)
				{
					document.getElementById("cmbUnAssignLossLob").style.display = "inline";
					document.getElementById("cmbAssignLossLob").style.display = "inline";
					document.getElementById("capUnassignLossLob").style.display = "inline";
					document.getElementById("capAssignedLossLob").style.display = "inline";
					document.getElementById("btnAssignLossCodes").style.display = "inline";
					document.getElementById("btnUnAssignLossLob").style.display = "inline";
					document.getElementById("btnUnAssignLossLob").style.display = "inline";
					//document.getElementById("btnSave").style.display = "inline";
				}
				else
				{
					document.getElementById("cmbUnAssignLossLob").style.display = "none";
					document.getElementById("cmbAssignLossLob").style.display = "none";
					document.getElementById("capUnassignLossLob").style.display = "none";
					document.getElementById("capAssignedLossLob").style.display = "none";
					document.getElementById("btnAssignLossCodes").style.display = "none";
					document.getElementById("btnUnAssignLossLob").style.display = "none";
					document.getElementById("btnUnAssignLossLob").style.display = "none";
					//To avoid a null check.
					if(document.getElementById("btnSave")!=null)
					document.getElementById("btnSave").style.display = "none";
					document.getElementById("chkMoveAll").style.display = "none";					
					document.getElementById("lblMoveAll").style.display = "none";					
				}
					
			}
			function cmbLOB_ID_Changed()
			{
				if(document.getElementById("cmbSTATE_ID"))
				{
					if(document.getElementById("cmbSTATE_ID").selectedIndex<1)
						ShowControls(false);
					else
						ShowControls(true);
				}
			}
			function AssignLossCodes()
			{					
				var coll = document.MNT_AGENCY_STATE_LOB_ASSOC.cmbUnAssignLossLob;
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
						document.MNT_AGENCY_STATE_LOB_ASSOC.cmbAssignLossLob.options[document.MNT_AGENCY_STATE_LOB_ASSOC.cmbAssignLossLob.length] = new Option(innerText,szSelectedDept)
						coll.remove(i);	
						//Added By raghav
						if(document.getElementById("btnSave")!=null)									
						document.getElementById("btnSave").style.display="inline";			
						document.getElementById("chkMoveAll").style.display="inline";									
						document.getElementById("lblMoveAll").style.display="inline";									
						//document.getElementById("btnReset").style.display="inline";			
					}										
				}	
				len = coll.options.length;			
				
				if(	num < len )
				{
					document.MNT_AGENCY_STATE_LOB_ASSOC.cmbUnAssignLossLob.options(num).selected = true;
				}	
				else
				{
					if(num>0)
						document.MNT_AGENCY_STATE_LOB_ASSOC.cmbUnAssignLossLob.options(num - 1).selected = true;
				}					
			}
			function UnAssignLossCodes()
			{					
				var UnassignableString = ""
				var Unassignable = UnassignableString.split(",")
				var gszAssignedString = ""				
				var Assigned = gszAssignedString.split(",")
				var coll = document.MNT_AGENCY_STATE_LOB_ASSOC.cmbAssignLossLob;
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
								document.MNT_AGENCY_STATE_LOB_ASSOC.cmbUnAssignLossLob.options[document.MNT_AGENCY_STATE_LOB_ASSOC.cmbUnAssignLossLob.length] = new Option(innerText,szSelectedDept)					
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
					document.MNT_AGENCY_STATE_LOB_ASSOC.cmbAssignLossLob.options(num).selected = true;
				}	
				else
				{
					document.MNT_AGENCY_STATE_LOB_ASSOC.cmbAssignLossLob.options(num - 1).selected = true;
				}				
			}
				
			//Function added to fill the previous saved LOB's in cmbAssignLossLob which will turn go to 'Modified from' field in Transaction Log) 
			function OldAssignLossCodes()
			{
				document.getElementById("hidOldLOBCODE").value = "";
				var coll = document.MNT_AGENCY_STATE_LOB_ASSOC.cmbAssignLossLob;
				var len = coll.options.length;
				for( k = 0;k < len ; k++)
				{
					var szSelectedDept = coll.options(k).text;
					if (document.getElementById("hidOldLOBCODE").value == "")
					{
						document.getElementById("hidOldLOBCODE").value =  szSelectedDept;
					}
					else
					{
						document.MNT_AGENCY_STATE_LOB_ASSOC.hidOldLOBCODE.value = document.MNT_AGENCY_STATE_LOB_ASSOC.hidOldLOBCODE.value + ", " + szSelectedDept;//Done for Itrack Issue 5502 on 28 April 2009
					}
				}							
			}
			
			function CountAssignLossCodes()
			{
				document.getElementById("hidLOSS_CODE_TYPE").value = "";
				var coll = document.MNT_AGENCY_STATE_LOB_ASSOC.cmbAssignLossLob;
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
						document.MNT_AGENCY_STATE_LOB_ASSOC.hidLOSS_CODE_TYPE.value = document.MNT_AGENCY_STATE_LOB_ASSOC.hidLOSS_CODE_TYPE.value + "," + szSelectedDept;
					}
				}						
	//			document.MNT_AGENCY_STATE_LOB_ASSOC.TextBox1.style.display = 'none';		
			}
				
			function ResetTheForm()
			{
				//ChangeColor();
				DisableValidators();	
				document.getElementById("hidRESET").value="1";				
				document.location.href = document.location.href;
				return false;
			}
			function AssignAllLossCodes()
			{					
				var coll = document.MNT_AGENCY_STATE_LOB_ASSOC.cmbUnAssignLossLob;
				var selIndex = coll.options.selectedIndex;				
				var len = coll.options.length;
				var num=0;				
				for (i = len- 1; i > -1 ; i--)
				{
					//if((coll.options(i).selected == true) && (coll.options(i).value > 0))
					if((coll.options(i).value > 0))
					{
						num = i;
						var szSelectedDept = coll.options(i).value;
						var innerText = coll.options(i).text;
						document.MNT_AGENCY_STATE_LOB_ASSOC.cmbAssignLossLob.options[document.MNT_AGENCY_STATE_LOB_ASSOC.cmbAssignLossLob.length] = new Option(innerText,szSelectedDept)
						coll.remove(i);	
						//Added By Raghav to avoid null check
						if(document.getElementById("btnSave")!=null)								
						document.getElementById("btnSave").style.display="inline";			
						document.getElementById("chkMoveAll").style.display="inline";			
						document.getElementById("lblMoveAll").style.display="inline";			
					}										
				}	
				len = coll.options.length;			
				
				if(	num < len )
				{
					document.MNT_AGENCY_STATE_LOB_ASSOC.cmbUnAssignLossLob.options(num).selected = true;
				}	
				else
				{
					if(num>0)
						document.MNT_AGENCY_STATE_LOB_ASSOC.cmbUnAssignLossLob.options(num - 1).selected = true;
				}					
				
			}
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="ApplyColor();cmbLOB_ID_Changed();OldAssignLossCodes();">
		<FORM id="MNT_AGENCY_STATE_LOB_ASSOC" method="post" runat="server">
			<TABLE cellSpacing='0' cellPadding='0'  border='0' width="100%">
				<tr>
					<TD class="pageHeader" width="100%" colSpan="4"><asp:Label ID="lblpleaseclick" runat="server"
                            Text="Label"></asp:Label>
                    </TD>
				</tr>
				<tr>
					<td class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" runat="server" cssclass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<tr id="trLOB_ID" runat="server">
					<td class="midcolorc" colSpan="1"><br />
                    </td>
                    <td class="midcolorc"  colSpan="1"><br />
                   <asp:Label ID="capSTATE_ID" Runat="server"></asp:Label> <asp:DropDownList ID="cmbSTATE_ID" Runat="server" OnFocus="SelectComboIndex('cmbSTATE_ID')" AutoPostBack="True"></asp:DropDownList>
                    </td>
                    <td class="midcolorc" colSpan="2"><br />
						<asp:RequiredFieldValidator ID="rfvSTATE_ID" Runat="server" Display="Dynamic" ControlToValidate="cmbSTATE_ID" ErrorMessage="Please select State"></asp:RequiredFieldValidator></td>
				</tr>
				<tr id="trLossCodes" runat="server">
					<td class="midcolorc" align="center" width="37%"><asp:label id="capUnassignLossLob" runat="server">Available Products</asp:label></td>
					<td class="midcolorc" vAlign="middle" align="center" width="33%" rowSpan="7"><br>
						<br>
						<input class="clsButton" runat="server" onclick="javascript:AssignLossCodes();" type="button"
							value=">>" name="btnAssignLossCodes" id="btnAssignLossCodes"><br>
						<br>
						<input class="clsButton" runat="server" onclick="javascript:UnAssignLossCodes();" type="button"
							value="<<" name="btnUnAssignLossLob" id="btnUnAssignLossLob">						
					</td>
					<td class="midcolorc" align="center" width="33%"><asp:label id="capAssignedLossLob" runat="server">Approved Products</asp:label></td>
				</tr>
				<tr>
					<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="cmbUnAssignLossLob" Runat="server" Width="300px" Height="150px" SelectionMode="Multiple"></asp:listbox>
						<br><br>
						<asp:Label ID="lblMoveAll" Runat="server"></asp:Label>
						<asp:CheckBox ID="chkMoveAll" Runat="server"></asp:CheckBox>
					</td>
					<td class="midcolorc" align="center" width="33%" rowSpan="6"><asp:listbox id="cmbAssignLossLob" Runat="server" Width="300px"  Height="150px" SelectionMode="Multiple"></asp:listbox></td>
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
			<INPUT id="hidMNT_AGENCY_STATE_LOB_ASSOC_ID" type="hidden" value="0" name="hidMNT_AGENCY_STATE_LOB_ASSOC_ID" runat="server">
			<INPUT id="hidLOSS_CODE_TYPE" type="hidden" value="0" name="hidLOSS_CODE_TYPE" runat="server">
			<INPUT id="hidOldLOBCODE" type="hidden" value="0" name="hidOldLOSS_CODE_TYPE" runat="server">
			<INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server">
			<INPUT id="hidAGENCY_ID" type="hidden" value="0" name="hidAGENCY_ID" runat="server">
		</FORM>
		<script>
			//RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidMNT_AGENCY_STATE_LOB_ASSOC_ID').value,true);			
		</script>
	</BODY>
</HTML>

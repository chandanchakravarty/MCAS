<%@ Page language="c#" Codebehind="InstallParams.aspx.cs" AutoEventWireup="false" Inherits="Cms.Accounting.InstallParams" ValidateRequest = "false" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>InstallParams</title>
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
		var coll = document.ACT_INSTALL_PARAMS.lbUnAssignUsers;
		var selIndex = coll.options.selectedIndex;
		var len = coll.options.length;
		var num = -1;
		for (i = len- 1; i > -1 ; i--)
		{
			if((coll.options(i).selected == true) && (coll.options(i).value > 0))
			{
				num = i;
				var szSelectedDept = coll.options(i).value;
				var innerText = coll.options(i).text;
				document.ACT_INSTALL_PARAMS.lbAssignUsers.options[document.ACT_INSTALL_PARAMS.lbAssignUsers.length] = new Option(innerText,szSelectedDept)
				coll.remove(i);													
			}										
		}
		if (num != -1)
		{
			len = coll.options.length;
			if(	num < len-1 )
			{
				document.ACT_INSTALL_PARAMS.lbUnAssignUsers.options(num).selected = true;
			}	
			else
			{
				document.ACT_INSTALL_PARAMS.lbUnAssignUsers.options(num - 1).selected = true;
			}						
		}
		if (document.ACT_INSTALL_PARAMS.lbUnAssignUsers.options.length == 2)
		{
			document.ACT_INSTALL_PARAMS.lbUnAssignUsers.options(1).selected = true;
		}
	}
	function UnAssignDepts()
	{	var num = -1;
		var UnassignableString = ""
		var Unassignable = UnassignableString.split(",")
		var gszAssignedString = ""
		var Assigned = gszAssignedString.split(",")
		var coll = document.ACT_INSTALL_PARAMS.lbAssignUsers;
		var selIndex = coll.options.selectedIndex;
		var len = coll.options.length;
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
						document.ACT_INSTALL_PARAMS.lbUnAssignUsers.options[document.ACT_INSTALL_PARAMS.lbUnAssignUsers.length] = new Option(innerText,szSelectedDept)					
						coll.remove(i);
													
					}
					else
					{
						alert("Cannot unassign Department "+innerText+" because some divisions are associasted with it");
					}
			}			
		}
		var len = coll.options.length;
		if (num != -1)
		{
			if(	num < len -1 )
			{
				document.ACT_INSTALL_PARAMS.lbAssignUsers.options(num).selected = true;
			}	
			else
			{
				document.ACT_INSTALL_PARAMS.lbAssignUsers.options(num - 1).selected = true;
			}					
		}
		if (document.ACT_INSTALL_PARAMS.lbUnAssignUsers.options.length == 2)
		{
			document.ACT_INSTALL_PARAMS.lbUnAssignUsers.options(1).selected = true;
		}
	}
	
	function CountAssignDepts()
	{
		document.getElementById("hidINSTALL_NOTIFY_OTHER_USERS").value = "";
		var coll = document.ACT_INSTALL_PARAMS.lbAssignUsers;
		var len = coll.options.length;
		for( k = 0;k < len ; k++)
		{
			var szSelectedDept = coll.options(k).value;
			if (document.getElementById("hidINSTALL_NOTIFY_OTHER_USERS").value == "")
			{
				document.getElementById("hidINSTALL_NOTIFY_OTHER_USERS").value =  szSelectedDept;
			}
			else
			{
				document.ACT_INSTALL_PARAMS.hidINSTALL_NOTIFY_OTHER_USERS.value = document.ACT_INSTALL_PARAMS.hidINSTALL_NOTIFY_OTHER_USERS.value + "," + szSelectedDept;
			}
		}
	}	
	
	function formReset()
		{
			document.ACT_INSTALL_PARAMS.reset();
			DisableValidators();
			ChangeColor();
			return false;
		}			
	
	</script>
	</HEAD>
	<BODY class="bodyBackGround" onresize="SmallScroll();" leftMargin="0" topMargin="0" onload="top.topframe.main1.mousein = false;findMouseIn();showScroll();">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<div class="pageContent" id="bodyHeight">
			<FORM id="ACT_INSTALL_PARAMS" method="post" runat="server">
				<TABLE class="tableWidth" border="0">
					<tr>
						<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
					</tr>
					<tr>
						<td class="headereffectCenter" colSpan="2">Billing Parameters</td>
					</tr>
					<tr>
						<TD class="pageHeader" width="100%" colSpan="2">Please note that all fields marked with * are mandatory.</TD>
					</tr>
					<tr>
						<td class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" cssclass="errmsg"></asp:label></td>
					</tr>
					<tr>
						<TD class="midcolora" width="50%"><asp:label id="capINSTALL_DAYS_IN_ADVANCE" runat="server">Number of Days In Advance to Release Installment Invoices</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="50%"><asp:textbox id="txtINSTALL_DAYS_IN_ADVANCE" runat="server" maxlength="2" size="2"></asp:textbox><BR>
							<asp:requiredfieldvalidator id="rfvINSTALL_DAYS_IN_ADVANCE" runat="server" Display="Dynamic" ErrorMessage="INSTALL_DAYS_IN_ADVANCE can't be blank."
								ControlToValidate="txtINSTALL_DAYS_IN_ADVANCE"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revINSTALL_DAYS_IN_ADVANCE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
								ControlToValidate="txtINSTALL_DAYS_IN_ADVANCE"></asp:regularexpressionvalidator></TD>
					</tr>
					<tr>
						<TD class="midcolora" width="50%">
							<asp:label id="capINSTALL_NOTIFY_ACCOUNTEXE" runat="server">Notify following people indicated on policy when installments are released.</asp:label>
						</TD>
						<TD class="midcolora" width="50%">
							<asp:checkbox id="chkINSTALL_NOTIFY_ACCOUNTEXE" Text="Account Executive" runat="server"></asp:checkbox>
							<asp:checkbox id="chkINSTALL_NOTIFY_UNDERWRITER" Text="Underwriter" runat="server"></asp:checkbox>
						</TD>
					</tr>
					<tr>
						<TD class="midcolora" colSpan="2"><asp:label id="capINSTALL_NOTIFY_OTHER_USERS" runat="server">Notify the following additional people</asp:label></TD>
					</tr>
					<tr>
						<td colspan="4">
							<table width="100%">
								<tr>
									<td class="midcolorc" align="center">Unassigned Users</td>
									<td class="midcolora"></td>
									<td class="midcolorc" align="center">Assigned Users</td>
								</tr>
								<tr>
									<td class="midcolorc" align="center" width="33%" rowSpan="6">
										<asp:listbox id="lbUnAssignUsers" Runat="server" Height="88px" SelectionMode="Multiple" ondblclick="javascript:AssignDepts()"></asp:listbox>
									</td>
									<td class="midcolorc" align="center" width="34%" rowSpan="6">
										<br>
										<br>
										<input class="clsButton" onclick="javascript:AssignDepts();" type="button" value=">>" name="AssignDivisions">
										<br>
										<br>
										<input class="clsButton" onclick="javascript:UnAssignDepts();" type="button" value="<<"
											name="UnAssignDivisions">
									</td>
									<td class="midcolorc" align="center" width="33%" rowSpan="6">
										<asp:listbox id="lbAssignUsers" Runat="server" Height="88px" SelectionMode="Multiple" ondblclick="javascript:UnAssignDepts()"></asp:listbox>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="midcolora" >
							<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
						</td>
						<td class="midcolorr" >
							<cmsb:cmsbutton class="clsButton" id="btnSave" runat="Server" text="Save"></cmsb:cmsbutton>
						</td>
					</tr>
				</TABLE>
				<table class="tableWidth" width="100%" border="0" cellpadding="0">
					<tr>
						<td class="midcolorr" vAlign="middle" align="right" colSpan="3"></td>
					</tr>
					<!-- Added to show scroll bar on the page-->
					<tr>
						<td class="iframsHeightMedium"></td>
					</tr>
					<!--End here-->
				</table>
				<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
				<INPUT id="hidOldData" type="hidden" value="" name="hidOldData" runat="server">
				<INPUT id="hidINSTALL_NOTIFY_OTHER_USERS" type="hidden" name="hidINSTALL_NOTIFY_OTHER_USERS"
					runat="server">
			</FORM>
		</div>
	</BODY>
</HTML>

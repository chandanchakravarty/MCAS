<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PolicyLocationPopup.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.aspx.HomeOwners.PolicyLocationPopup" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS - Copy Dwelling Info</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../../cmsweb/scripts/common.js"></script>
		<script src="../../../cmsweb/scripts/form.js"></script>
		<script language="javascript">
			function Initialize()
			{
				if ( document.getElementById("cmbLOCATION_ID") )
				{
					if ( document.Form1.hidFormSaved.value == "0" )
					{
						document.getElementById("cmbLOCATION_ID").options.selectedIndex = -1;
						ApplyColor();
						ChangeColor();
						
					}
				}
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="Initialize();">
		<form id="Form1" method="post" runat="server">
			<table width="100%">
				<tr>
					<td class="midcolorc" colspan="2">
						<asp:Label id="lblMessage" runat="server" Visible="False" CssClass="errmsg" EnableViewState="False"></asp:Label></td>
				</tr>
				</TD></TR>
				<asp:Panel ID="pnl" Visible="True" Runat="server">
					<TBODY>
						<TR>
							<TD class="headereffectCenter" colSpan="2"><SPAN id="spnHeader">Copy Dwelling Info</SPAN>
							</TD>
						</TR>
						<TR>
							<TD class="midcolora" width="30%">
								<asp:Label id="Label1" runat="server">Please select the location for this dwelling</asp:Label></TD>
							<TD class="midcolora" width="70%">
								<asp:DropDownList id="cmbLOCATION_ID" onfocus="SelectComboIndex('cmbLOCATION_ID')" runat="server"
									Width="300px"></asp:DropDownList><BR>
								<asp:RequiredFieldValidator id="rfv_LOCATION_ID" runat="server" Display="Dynamic" ControlToValidate="cmbLOCATION_ID"></asp:RequiredFieldValidator><INPUT id="hidFormSaved" type="hidden" value="0" name="Hidden1" runat="server"></TD>
						</TR>
				</asp:Panel>
				<tr>
					<td class="midcolora"><cmsb:cmsbutton class="clsButton" id="btnClose" runat="server" Text="Close" CausesValidation="False"></cmsb:cmsbutton></td>
					<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSubmit" runat="server" Text="Submit"></cmsb:cmsbutton></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

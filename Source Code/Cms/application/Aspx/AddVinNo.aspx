<%@ Page language="c#" Codebehind="AddVinNo.aspx.cs" AutoEventWireup="false" Inherits="Cms.Application.Aspx.AddVinNo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddVinNo</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js">
		</script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/webcommon.js"></script>
		<script language="javascript">
			function GoEdit()
			{
				var isRefresh='<%=isRefresh%>'
				if(isRefresh==1)
				{
					document.location.href="AddVINMasterPopup.aspx?year=" + document.getElementById("txtVEHICLE_YEAR").value + "&make=" + document.getElementById("txtMAKE").value + "&Model=" + document.getElementById("txtMODEL").value + "&addvin=" + document.getElementById("txtVIN").value + "&bodytype=" + document.getElementById("txtBODY_TYPE").value + "&CalledFrom=MOT"  ; 
				}	
			
			}
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" leftMargin='0' topMargin='0' onload='ApplyColor();ChangeColor();GoEdit();'>
		<form id='VINMasterPopup' method='post' runat='server'>
			<table cellSpacing='1' cellPadding='1' width='100%' border='0'>
				<tr>
					<td class='midcolora' colspan="2">
						<asp:label id="lblMessage" CssClass="errmsg" Runat="server" Visible="False"></asp:label>
					</td>
				</tr>
				<tr>
					<TD class="headerEffectSystemParams" colSpan="4" align="center">Add Vehicle 
						Information</TD>
				</tr>
				<tr>
					<TD class='midcolora' width='18%'>
						<asp:Label id="capVEHICLE_YEAR" runat="server">Year</asp:Label><span class="mandatory">*</span></TD>
					<TD class='midcolora' width='32%'>
						<asp:TextBox id='txtVEHICLE_YEAR' MaxLength="4" runat='server'></asp:TextBox><BR>
						<asp:requiredfieldvalidator id="rfvVEHICLE_YEAR" runat="server" ControlToValidate="txtVEHICLE_YEAR" Display="Dynamic"></asp:requiredfieldvalidator>
						<asp:RangeValidator MinimumValue="1981" Type=Integer ID="rngVEHICLE_YEAR" Runat="server" ControlToValidate="txtVEHICLE_YEAR"
							Display="Dynamic"></asp:RangeValidator>
					</TD>
				</tr>
				<tr>
					<TD class='midcolora' width='18%'>
						<asp:Label id="capMAKE" runat="server">Make of Vehicle</asp:Label><span class="mandatory">*</span></TD>
					<TD class='midcolora' width='32%'>
						<asp:TextBox id='txtMAKE' MaxLength="28" size="32" runat='server'></asp:TextBox><BR>
						<asp:requiredfieldvalidator id="rfvMAKE" runat="server" ControlToValidate="txtMAKE" Display="Dynamic"></asp:requiredfieldvalidator>
					</TD>
				</tr>
				<tr>
					<TD class='midcolora' width='18%'>
						<asp:Label id="capMODEL" runat="server">Model of Vehicle</asp:Label><span class="mandatory">*</span></TD>
					<TD class='midcolora' width='32%'>
						<asp:TextBox id='txtMODEL' MaxLength="25" runat='server'></asp:TextBox><br>
						<asp:requiredfieldvalidator id="rfvModel" runat="server" ControlToValidate="txtMODEL" Display="Dynamic"></asp:requiredfieldvalidator>
					</TD>
				</tr>
				<tr>
					<TD class='midcolora' width='18%'>
						<asp:Label id="capBODY_TYPE" runat="server">Body Type</asp:Label></TD>
					<TD class='midcolora' width='32%'>
						<asp:TextBox id='txtBODY_TYPE' runat='server' MaxLength="2"></asp:TextBox>
					</TD>
				</tr>
				<tr>
					<TD class='midcolora' width='18%'>
						<asp:Label id="lblVIN" runat="server">VIN</asp:Label><span class="mandatory">*</span></TD>
					<TD class='midcolora' width='32%'>
						<asp:TextBox id="txtVIN" runat='server' MaxLength="17"></asp:TextBox><br>
						<asp:requiredfieldvalidator id="rfvVIN" runat="server" ControlToValidate="txtVIN" Display="Dynamic"></asp:requiredfieldvalidator>
					</TD>
				</tr>
				<tr>
					<td class='midcolorr' colspan="2">
						<asp:Button class="clsButton" id='btnSubmit' runat="server" Text='Submit'></asp:Button>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

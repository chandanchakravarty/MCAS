<%@ Page language="c#" Codebehind="AddVinNoMotorcycle.aspx.cs" AutoEventWireup="false" Inherits="Cms.Application.Aspx.AddVinNoMotorcycle" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddMotorCycleVinNo</title>
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
					//document.location.href="AddVINMotorCyclePopup.aspx?id=" + document.getElementById("txtID").value + "&manufacturer=" + document.getElementById("txtManufacturer").value + "&Model=" + document.getElementById("txtModel").value + "&year=" + document.getElementById("txtModel_Year").value + "&cc=" + document.getElementById("txtModel_CC").value; 					
					//window.opener.document.location.href="AddVINMotorCyclePopup.aspx?id=" + document.getElementById("txtID").value + "&manufacturer=" + document.getElementById("txtManufacturer").value + "&Model=" + document.getElementById("txtModel").value + "&year=" + document.getElementById("txtModel_Year").value + "&cc=" + document.getElementById("txtModel_CC").value; 										
					//window.opener.document.location.reload();					
					window.close();
					
				}	
			
			}
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" leftMargin='0' topMargin='0' onload='ApplyColor();ChangeColor();GoEdit();'>
		<form id='VINMotorCycleMasterPopup' method='post' runat='server'>
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
						<asp:Label id="capModel_Year" runat="server">Year</asp:Label><span class="mandatory">*</span></TD>
					<TD class='midcolora' width='32%'>
						<asp:TextBox id='txtModel_Year' MaxLength="4" runat='server'></asp:TextBox><BR>
						<asp:requiredfieldvalidator id="rfvModel_Year" runat="server" ControlToValidate="txtModel_Year" Display="Dynamic"></asp:requiredfieldvalidator>
						<asp:RangeValidator MinimumValue="1981" Type="Integer" ID="rngModel_Year" Runat="server" ControlToValidate="txtModel_Year" Enabled="True"
							Display="Dynamic"></asp:RangeValidator>
					</TD>
				</tr>
				<tr>
					<TD class='midcolora' width='18%'>
						<asp:Label id="capManufacturer" runat="server">Make of Vehicle</asp:Label><span class="mandatory">*</span></TD>
					<TD class='midcolora' width='32%'>
						<asp:TextBox id='txtManufacturer' MaxLength="4" runat='server'></asp:TextBox><BR>
						<asp:requiredfieldvalidator id="rfvManufacturer" runat="server" ControlToValidate="txtManufacturer" Display="Dynamic"></asp:requiredfieldvalidator>
					</TD>
				</tr>
				<tr>
					<TD class='midcolora' width='18%'>
						<asp:Label id="capModel" runat="server">Model of Vehicle</asp:Label><span class="mandatory">*</span></TD>
					<TD class='midcolora' width='32%'>
						<asp:TextBox id='txtModel' MaxLength="25" runat='server'></asp:TextBox><br>
						<asp:requiredfieldvalidator id="rfvModel" runat="server" ControlToValidate="txtModel" Display="Dynamic"></asp:requiredfieldvalidator>
					</TD>
				</tr>
				<tr>
					<TD class='midcolora' width='18%'>
						<asp:Label id="capModel_CC" runat="server">Vehicle CC</asp:Label></TD>
					<TD class='midcolora' width='32%'>
						<asp:TextBox id='txtModel_CC' runat='server' MaxLength="3"></asp:TextBox>
					</TD>
				</tr>
				<tr>
					<TD class='midcolora' width='18%'>
						<asp:Label id="lblID" runat="server">VIN</asp:Label><span class="mandatory">*</span></TD>
					<TD class='midcolora' width='32%'>
						<asp:TextBox id="txtID" runat='server' MaxLength="17"></asp:TextBox><br>
						<asp:requiredfieldvalidator id="rfvID" runat="server" ControlToValidate="txtID" Display="Dynamic"></asp:requiredfieldvalidator>
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

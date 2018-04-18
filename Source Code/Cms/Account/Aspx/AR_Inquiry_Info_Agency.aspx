<%@ Page language="c#" Codebehind="AR_Inquiry_Info_Agency.aspx.cs" AutoEventWireup="false" Inherits="Account.Aspx.AR_Inquiry_Info_Agency" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>EBIX ADVANTAGE - Account Inquiry Agency</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css4.css" type="text/css" rel="stylesheet">
		<LINK href="/cms/cmsweb/css/menu4.css" type="text/css" rel="stylesheet">
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script src="../../cmsweb/scripts/AJAXcommon.js"></script>
		<script language="javascript">
		
			function ShowAssoc(obj)
			{	
				document.getElementById('txtPolicyNo').value=obj;
				__doPostBack('btnSave',null);
				return false;
			}
				    
			function policyfocus()
			{
				document.getElementById('txtPolicyNo').focus();
				//document.getElementById('btnClose').style.display = "none";				
			}			
				
			</script>
		<!--style="OVERFLOW: scroll" -->
	</HEAD>
	<body onload="ApplyColor();ChangeColor();policyfocus();" MS_POSITIONING="GridLayout">
		<div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 100%">
			<form id="AR_INQUIRY" method="post" runat="server">
				<table class="tableWidth" cellSpacing="0" cellPadding="0" align="center" border="0">
					<tr>
						<td class="ImageBackColor" width="17%"><IMG src="/cms/cmsweb/images4/bricklogo.gif"></td>
						<td class="ImageBackColor" width="33%"><IMG src="/cms/cmsweb/images4/wolverine.gif"></td>
						<td class="ImageBackColor" width="17%"></td>
						<td class="ImageBackColor" width="33%"></td>
					</tr>
					<tr>
						<td class="headereffectCenter" colSpan="4">Account Inquiry Report
						</td>
					</tr>
					<tr>
						<td class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" Runat="server" CssClass="errmsg"></asp:label></td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">Policy Number<span class="mandatory">*</span></td>
						<td class="midcolora" width="32%"><asp:textbox id="txtPolicyNo" Runat="server" MaxLength="8" size="10"></asp:textbox><span id="spnPOLICY_NO" runat="server"></span><br>
							<asp:requiredfieldvalidator id="rfvPolicyNo" Runat="server" Display="Dynamic" ControlToValidate="txtPolicyNo"></asp:requiredfieldvalidator></td>
						<td class="midcolora" colSpan="2">Combined Agency Code #&nbsp;&nbsp;<span class="mandatory">*</span>
							<asp:TextBox ID="txtAgencyCode" Runat="server" MaxLength="10" size="10"></asp:TextBox><br>
							<asp:requiredfieldvalidator id="rfvAgencyCode" ErrorMessage="Please enter Agency Combined Code." Runat="server" Display="Dynamic" ControlToValidate="txtAgencyCode"></asp:requiredfieldvalidator></td>
					</tr>
					<tr>
						<td class="midcolorr" align="right" width="100%" colSpan="4">
							<asp:Button CssClass="clsButton" ID="btnSave" runat="server" Text="Show Report"></asp:Button>
						</td>
					</tr>
				</table>
				<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0" id="tbArReport">
					<tr>
						<td id="tdArReport" width="100%" colSpan="5" runat="server"></td>
					</tr>
					<tr>
						<td><asp:Button ID="btnClose" Runat="server" Text="Close" CssClass="clsButton"></asp:Button></td>
					</tr>
					<tr>
						<td></td>
					</tr>
				</table>
				<input id="hidPOLICYINFO" type="hidden" name="hidPOLICYINFO" runat="server">
			</form>
		</div>
	</body>
</HTML>

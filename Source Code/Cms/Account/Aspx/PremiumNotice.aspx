<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PremiumNotice.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.PremiumNotice" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AR_Inquiry_Info</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script language="javascript">
		
			function OpenPolicyLookup()
				{
					var url='<%=URL%>';
					var idField,valueField,lookUpTagName,lookUpTitle;					
					OpenLookupWithFunction(url,'POL_INFORM','POLICY_NUMBER','hidPOLICYINFO','txtPolicyNo','PREM_NOTICE','Policy','','splitPolicy()');
				//	OpenLookup(url,'POL_INFORM','POLICY_NUMBER','hidPOLICYINFO','txtPolicyNo','NFS','NFS','');
				    
				
				}
				function splitPolicy()
				{
				}
				//Disable Menu Items when called from TopFrame : Praveen kasana
				function MenuRequired()
				{
				//alert();
					var val = '<%=calledfrom%>';
					if(val != 'TopFrame')
					{
						top.topframe.main1.mousein = false;
						findMouseIn();
						document.getElementById('btnClose').style.display = "none";
					}
					document.getElementById('txtPolicyNo').focus();
				}
				/*function ShowReport()
				{
				alert();
					if (window.event.keyCode == 13)
					{
					
						__doPostBack('btnSave', 'btnSave_Click()');
					}
				}*/
				 
		</script>
		<!--style="OVERFLOW: scroll" -->
	</HEAD>
	<body oncontextmenu = "return true;" onload="showScroll();setfirstTime();ApplyColor();ChangeColor();MenuRequired();" MS_POSITIONING="GridLayout">
		<webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 100%">
			<form id="AR_Inquiry_Info" method="post" runat="server">
				<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
					<tr>
						<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
					</tr>
					<tr>
						<td class="headereffectCenter" colSpan="4">Premium Notice
						</td>
					</tr>
					<tr>
						<td class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessageDB" Runat="server" CssClass="errmsg"></asp:label></td>
					</tr>
					<tr>
						<td class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" Runat="server" CssClass="errmsg"></asp:label></td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">Policy Number<span class="mandatory">*</span></td>
						<td class="midcolora" width="32%"><asp:textbox id="txtPolicyNo" Runat="server" size="10"></asp:textbox><span id="spnPOLICY_NO" runat="server"><IMG id="imgPOLICY_NO" style="CURSOR: hand" onclick="OpenPolicyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
									runat="server"></span><br>
							<asp:requiredfieldvalidator id="rfvPolicyNo" Runat="server" Display="Dynamic" ControlToValidate="txtPolicyNo"></asp:requiredfieldvalidator></td>
					</tr>
					<tr>
						<td class="midcolorr" align="right" width="100%" colSpan="4">
						<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Generate Premium Notice"></cmsb:cmsbutton></td>
					</tr>
				</table>
				<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
					<tr>
						<td id="tdArReport" width="100%" colSpan="5" runat="server"></td>
					</tr>
					<tr>
						<td><asp:Button ID="btnClose" Runat="server" Text="Close" CssClass="clsButton"></asp:Button></td>
					</tr>
					<tr>
						<td><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></td>
					</tr>
					
				</table>
				<input id="hidPOLICYINFO" type="hidden" name="hidPOLICYINFO" runat="server">
			</form>
		</div>
	</body>
</HTML>

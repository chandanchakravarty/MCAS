<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="ClearData.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.ClearData" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Clear Data</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script language="vbscript">
		Function getUserConfirmationForSymbol
				getUserConfirmationForSymbol= msgbox("All policies of this customer will be deleted, are you sure?",4,"CMS")
		End function
		
		Function getUserConfirmationForNull
				getUserConfirmationForNull= msgbox("Please select customer",16,"CMS")
		End function
		
		</script>
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
			function CheckPolicy()
			{
			
				var returnVal = 6;//Yes	
				if(document.getElementById('cmbPolClearCustomerID').selectedIndex==0)
				{
					//alert('Please select customer');			
					returnVal=getUserConfirmationForNull();
					//returnVal=0;
				}
				else if(document.getElementById('cmbPolClearCustomerID').selectedIndex >0) // not null
				{
					returnVal = getUserConfirmationForSymbol();					
				}
				//alert(returnVal);
				if(returnVal == 6)
					return true;
				else
					return false;
			}			
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" class="bodyBackGround" topMargin="0" onload="ApplyColor();ChangeColor(); top.topframe.main1.mousein = false;findMouseIn();showScroll();"
		MS_POSITIONING="GridLayout">
		<webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<form id="frmDeleteData" method="post" runat="server">
			<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
				<tr>
					<td class="headereffectcenter" colSpan="2">Clear Accounting Data
					</td>
				</tr>
				<tr>
					<TD class="pageHeader" colSpan="2">Select the customer, else all the customer's 
						accounting data will be deleted
					</TD>
				</tr>
				<tr>
					<td class="midcolorc" align="right" colSpan="2"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR>
					<TD class="midcolora" ><asp:dropdownlist id="cmbCustomerID" runat="server">
							<asp:ListItem Value="0">0</asp:ListItem>
						</asp:dropdownlist>
					</td>
					<td class="midcolorr">
					<asp:button id="btnNext" CssClass="clsButton" CommandName="Next" OnCommand="Navigation_Click"
							Runat="server" Text="Clear Accounting Data"></asp:button></TD>
				</TR>
				<TR>
					<td class="midcolora" colspan="2"></td>
				</TR>
				<tr>
					<td style="WIDTH: 482px" colSpan="2"></td>
				</tr>
				<tr>
					<td class="headereffectcenter" colSpan="2">Clear Customer Policy Data
					</td>
				</tr>
				<tr>
					<TD class="pageHeader" colSpan="2">Select customer, else all policies will be 
						deleted
					</TD>
				</tr>
				<!--<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="Label1" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>-->
				<TR>
					<TD class="midcolora"><asp:label id="capCUST_NAME" runat="server">Customer Name</asp:label>
						<asp:dropdownlist id="cmbPolClearCustomerID" runat="server" >
							<asp:ListItem Value="0"></asp:ListItem>
						</asp:dropdownlist>
					</td>
					<!--<TD class="midcolora"><asp:label id="capPolicy_ID" runat="server" Visible=False>Policy Number</asp:label>
					<asp:dropdownlist id="cmbPolID" runat="server" Visible=False>
							<asp:ListItem Value=""></asp:ListItem>
						</asp:dropdownlist><!--</TD>-->
				
					<TD class="midcolorr" ><asp:button id="btnCustPol" CssClass="clsButton" CommandName="Next" Runat="server" Text="Clear Customer Policy Data"></asp:button></TD>
				</TR>				
			</table>
		</form>
	</body>
</HTML>

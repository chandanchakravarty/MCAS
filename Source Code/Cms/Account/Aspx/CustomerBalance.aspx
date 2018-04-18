<%@ Page language="c#" Codebehind="CustomerBalance.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.CustomerBalance" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>CustomerBalance</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/AJAXcommon.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		function showPrint()
		{
			spn_Button.style.display = "none"
			bgcolor = buttons.style.background
			buttons.style.background = "white"
			window.print()
			spn_Button.style.display = "inline"
		}
		function OnClick()
		{			
				return false;
		}
		function CheckPageNum(objSource, objArgs)
		{	
		
			if(document.getElementById('txtGOTO').value != "" && !isNaN(document.getElementById('txtGOTO').value))
			{
				if(eval(document.getElementById('txtGOTO').value) < 0 || eval(document.getElementById('txtGOTO').value) == 0)
				{
					objArgs.IsValid = false;
					return false;
				}
				var currentPage =<%=CurrentPageIndex%>;
				var totalPages = <%=intTotalPages%>;
				
				if(document.getElementById('txtGOTO').value > 0 && eval(document.getElementById('txtGOTO').value) > totalPages)
				{
					objArgs.IsValid = false;
				}
				else
				{
					objArgs.IsValid = true;
				}
			}
			else
				objArgs.IsValid = false;
		}
		
		
		function ValidatePolicyNum(PolNum,CustID)
		{
			//document.getElementById('lblCurrentPage').innerHTML ='';
			var ParamArray = new Array();
			obj1=new Parameter('POLICY_NUMBER',PolNum);
			obj2=new Parameter('CUSTOMER_ID',CustID);
			ParamArray.push(obj1);
			ParamArray.push(obj2);
			var objRequest = _CreateXMLHTTPObject();
			var Action = 'CUST_POL_CHECK';
			_SendAJAXRequest(objRequest,Action,ParamArray,CallbackFun);
			var currentPage =<%=CurrentPageIndex%>;
			var totalPages = <%=intTotalPages%>;
			//document.getElementById('lblCurrentPage').innerHTML= "Page " + currentPage + " of " + totalPages;
			document.getElementById('hidpolCount').value = 0;
		}
		function CallbackFun(AJAXREsponse)
		{	
			if(document.getElementById('txtSEARCH_POLICYNUMBER').value != '' && AJAXREsponse == 0)
			{
				alert('Invalid policy number. Please enter a valid Policy Number for this customer.');
				document.getElementById('txtSEARCH_POLICYNUMBER').value = '';
				if(document.getElementById('lblMessage'))
				document.getElementById('lblMessage').style.display = 'none';
				return false; 
			}
		}
		
		</script>
</HEAD>
	<body oncontextmenu="return false;" class="bodyBackGround" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<div class="pageContent" id="bodyHeight">
			<form id="CustomerBalance" method="post" runat="server">
				<!--  <div id="myid" style="OVERFLOW: auto;height=500;width=100% "  --><webcontrol:gridspacer id="Gridspacer2" runat="server"></webcontrol:gridspacer>
				<table cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					<tr>
						<td class="headereffectCenter" colSpan="2">Customer Balance Information
						</td>
					</tr>
					<tr>
						<td class="midcolorr" width="50%">Today's Date : &nbsp;
						</td>
						<td class="midcolora" width="50%">
							<%=DateTime.Now.ToString("dd/MM/yyyy")%>
						</td>
					</tr>
					<tr>
						<td class="midcolorr" width="50%">Time : &nbsp;
						</td>
						<td class="midcolora" width="50%">
							<%=DateTime.Now.ToString("hh:mm:ss tt")%>
						</td>
					</tr>
					<tr>
						<td class="midcolora" colspan="2"><asp:Label id="lblSEARCH_POLICYNUMBER" runat="server">Search Policy Number : </asp:Label>
						<asp:TextBox ID="txtSEARCH_POLICYNUMBER" Runat="server" MaxLength="75" size="30"></asp:TextBox>
						<cmsb:cmsbutton class="clsButton" id="btnSEARCH_POLICYNUMBER" runat="server" Text="Go"></cmsb:cmsbutton>
						</td>
					</tr>
					<tr>
						<td class="errmsg"><asp:label id="lblMessage" Runat="server" Visible="false"></asp:label></td>
					</tr>
					<tr>
						<td id="tdAgencyStatementTD" colSpan="2" runat="server"></td>
					</tr>
					<TR class="AlternateDataRow">
						<TD vAlign="middle" align="center" colSpan="4">
							<TABLE width="100%">
								<tr align="center">
									<td><asp:label id="capGOTO" Runat="server">Go To Page :</asp:label>
									<asp:textbox id="txtGOTO" Runat="server" size="3"></asp:textbox>
									<cmsb:cmsbutton class="clsButton" id="btnGOTO" runat="server" Text="Go"></cmsb:cmsbutton><br>
									<asp:CustomValidator ID="csvGOTO" ErrorMessage="aaaaa" Runat="server" ControlToValidate="txtGOTO" ClientValidationFunction="CheckPageNum"
									 Display="Dynamic"></asp:CustomValidator>
									</td>
									
								</tr>
								<TR>
									<TD align="center">
										<asp:imagebutton id="btnPrevious" Runat="server" ImageUrl="../images/prevoff.gif"></asp:imagebutton>
										<asp:label id="lblCurrentPage" Runat="server"></asp:label>
										<asp:imagebutton id="btnNext" Runat="server" ImageUrl="../images/next.gif"></asp:imagebutton>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</table>
				<INPUT id="hidpolCount" type="hidden" value="0" name="hidpolCount" runat="server">
				<!-- </div>  --></form>
		</div>
	</body>
</HTML>

<%@ Page language="c#" Codebehind="LockGLPostingDates.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.LockGLPostingDates" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Brics-Check Items To Be Paid</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>
	
		<script type="text/javascript" language="javascript">

		    var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		function CheckDateInRange(objSource , objArgs)
		{
			if(document.getElementById('txtLOCK_DATE').value.length>0) {
			  

			    var beginDate = document.getElementById('hidBeginDate').value;
			    var endDate = document.getElementById('hidEndDate').value;
			    var lockDate = document.getElementById('txtLOCK_DATE').value;
			    
				if (DateComparer(lockDate, beginDate, jsaAppDtFormat) && DateComparer(endDate, lockDate, jsaAppDtFormat))
				{
					objArgs.IsValid = true;
					return true;		
				}
				else 
				{
					objArgs.IsValid =false;
					return false;
				}
			}
		}
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" MS_POSITIONING="GridLayout" onload="ApplyColor();">
		<form id="frmLockDates" method="post" runat="server">
			<TABLE class="tablewidthHeader" border="0" align="center">
				<tr>
					<td class="headereffectCenter" colSpan="6">
						<asp:Label ID="capGENERAL_LEDGER" runat="server"></asp:Label>
					</td><%--Lock General Ledger Posting Date--%>
				</tr>
				<tr>
					<td class="midcolorc" align="right" colSpan="6"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<tr>
					<td class="headRow" align="right" colSpan="6"><asp:Label ID="capHEADMSG" runat="server"></asp:Label></td><%--Enter the day you desire as the cut 
						off day for posting to occur for the month selected.--%>
				</tr>
				<tr>
					<td class="midcolora" width="18%">
						<asp:Label ID="capFISCAL_PERIOD" runat="server"></asp:Label>
					</td><%--Fiscal Period:--%>
					<td class="midcolora" colSpan="5">
						<b>
							<asp:Label id="lblFiscalPeriod" runat="server"></asp:Label>
						</b>
					</td>
				</tr>
				<tr>
					<td class="midcolora" width="18%"><asp:Label ID="capLOCK_DATE" runat="server"></asp:Label><span class="mandatory">*</span></td><%--Lock Date--%>
					<td class="midcolora" colSpan="5">
						<asp:TextBox ID="txtLOCK_DATE" Runat="server" Width="80px" MaxLength="10"></asp:TextBox>
						<asp:hyperlink id="hlkLOCK_DATE" runat="server" CssClass="HotSpot">
							<asp:image id="imgLOCK_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
						</asp:hyperlink><BR>
						<asp:requiredfieldvalidator id="rfvLOCK_DATE" runat="server" Display="Dynamic" ErrorMessage=""
							ControlToValidate="txtLOCK_DATE"></asp:requiredfieldvalidator><%--LOCK_DATE can't be blank.--%>
						<asp:CustomValidator ID="cstLOCK_DATE" Runat="server" ControlToValidate="txtLOCK_DATE" Display="Dynamic"
							ClientValidationFunction="CheckDateInRange"></asp:CustomValidator>
					</td>
				</tr>
				<tr>
					<td class="midcolorr" colSpan="6"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
				</tr>
				<tr>
					<td>
						<input id="hidBeginDate" type="hidden" name="hidBeginDate" runat="server"> <input id="hidEndDate" type="hidden" name="hidEndDate" runat="server">
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>

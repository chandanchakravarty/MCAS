<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="TransferDiaryEntry.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.diary.TransferDiaryEntry" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title><%=ts%></title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script language="javascript" src="../Scripts/Calendar.js"></script>
		<script language="javascript">
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";

			function refreshParent()
			{
				
				if(document.getElementById("hidFormSaved").value=="1")
				{
					//window.opener.top.frames[1].HideTab();					
					//window.opener.top.frames[1].RefreshWebgrid(1,document.getElementById('hidRowId').value,false);
					
					// set this to make date bold
					window.opener.parent.gAppDatesGlobal = '<%=gStrAppDates%>';
					//set this to make counts of pending tasks
					window.opener.parent.gStrEEC	= '<%=gStrEEC%>';
					window.opener.parent.gStrQPAC	= '<%=gStrQPAC%>';
					window.opener.parent.gStrQPBC	= '<%=gStrQPBC%>';
					window.opener.parent.gStrRRC	= '<%=gStrRRC%>';
					window.opener.parent.gStrBRC	= '<%=gStrBRC%>';
					window.opener.parent.gStrERC	= '<%=gStrERC%>';
					window.opener.parent.gStrAAC	= '<%=gStrAAC%>';
					window.opener.parent.cStrCRE	= '<%=cStrCRE%>';
					window.opener.parent.cStrANF	= '<%=cStrANF%>';
					window.opener.parent.cStrCF		= '<%=cStrCF%>';
					window.opener.parent.listtypeid1 = '<%=listtypeid1%>';
					window.opener.parent.listtypeid2 = '<%=listtypeid2%>';
					window.opener.parent.listtypeid3 = '<%=listtypeid3%>';
					window.opener.parent.listtypeid4 = '<%=listtypeid4%>';
					window.opener.parent.listtypeid5 = '<%=listtypeid5%>';
					window.opener.parent.listtypeid6 = '<%=listtypeid6%>';
					window.opener.parent.listtypeid7 = '<%=listtypeid7%>';
					window.opener.parent.listtypeid8 = '<%=listtypeid8%>';
					window.opener.parent.listtypeid9 = '<%=listtypeid9%>';
					window.opener.parent.listtypeid10 = '<%=listtypeid10%>';
					//call this function to make date bold in calendar
					window.opener.parent.fPopCalendar(window.opener.parent.dc,window.opener.parent.dc);
					//call this to set counts of pending task
					window.opener.parent.writePendingTask();
					//window.opener.parent.HideTab();
					window.opener.parent.RefreshWebgrid(1,document.getElementById('hidRowId').value,true);
				}
			}

	
			
			function checkToUserID()
			{
				//alert(document.getElementById('cmbToUserId').selectedIndex)
				if(document.getElementById('cmbToUserId').selectedIndex==-1)
				{
				    alert('<%=Alert%>');
					return false;
				}
				
				if(document.getElementById('cmbToUserId').options[document.getElementById('cmbToUserId').selectedIndex].value==-1 )
				{
					alert('<%=Alert%>');
					return false;
				}
				return true;
			}
		</script>
</HEAD>
	<body MS_POSITIONING="GridLayout" class="bodyBackGround" leftMargin="0" topMargin="0"
		onload="ApplyColor();refreshParent();">
		<form id="Form1" method="post" runat="server">
			<TABLE width="100%" align="center" border="0">
				<tr>
					<td class="headereffectCenter" colspan="4"><asp:Label ID="capHeader" runat="server" Text="Transfer Diary Entry"></asp:Label></td>
				</tr>
				<tr>
					<td class="pageHeader" colSpan="4"><asp:Label ID="capMandatory" runat="server" Text=""></asp:Label></td>
				</tr>
				<tr>
					<td class="midcolorc" align="center" colSpan="4">
						<asp:label id="lblMessage" Visible="False" Runat="server" CssClass="errmsg"></asp:label>
					</td>
				</tr>
				<tr>
					<td class="midcolora"><asp:Label ID="capTo" runat="server">To</asp:Label><span class="mandatory">*</span></td>
					<td class="midcolora">
						<asp:ListBox id="cmbToUserId" Runat="server" style="width:200"></asp:ListBox>
					</td>
					<td class="midcolora"><asp:Label ID="capFollow" runat="server">Follow up Date</asp:Label><span class="mandatory">*</span></td>
					<td class="midcolora">
						<asp:textbox id="txtFollowUpDate" Runat="server" MaxLength="10"></asp:textbox>
						<asp:hyperlink id="hlkCalandarDate" runat="server" CssClass="HotSpot">
							<asp:image id="imgCalenderExp" runat="server" ImageUrl="../Images/CalendarPicker.gif"></asp:image>
						</asp:hyperlink><br>
						<asp:requiredfieldvalidator id="rfvFollowUpDate" Runat="server" Display="Dynamic" ControlToValidate="txtFollowUpDate"></asp:requiredfieldvalidator>
						<asp:RegularExpressionValidator ID="revFollowUpDate" Runat="server" Display="Dynamic" ControlToValidate="txtFollowUpDate"></asp:RegularExpressionValidator>
					</td>
				</tr>
				<tr>
					<td class="midcolora"><asp:Label ID="capNote" runat="server">Note</asp:Label></td>
					<TD class="midcolora" colSpan="3"><asp:textbox id="txtNote" runat="server" MaxLength="2000" Height="128px" Width="480px" TextMode="MultiLine"></asp:textbox></TD>
				</tr>
				<tr>
					<td class="midcolora" colSpan="2">
						<cmsb:cmsbutton id="btnClose" runat="server" CssClass="clsButton" Text="Close"></cmsb:cmsbutton>
					</td>
					<td class="midcolorr" colSpan="2">
						<cmsb:cmsbutton id="btnSave" runat="server" CssClass="clsButton" Text="Save"></cmsb:cmsbutton>
					</td>
				</tr>
			</TABLE>
			<input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<input id="hidRowId" type="hidden" value="0" name="hidRowId" runat="server"> <input type="hidden" id="hidOldData" name="hidOldData" runat="server">
		</form>
	</body>
</HTML>

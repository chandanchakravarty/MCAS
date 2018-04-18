<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="EditList.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.UserDefined.EditList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>EditList</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		<!--
			function Winclose()
			{
				var qid;
				var screenID=document.EditList.hidScreenID.value 
				var groupID=document.EditList.hidGroupID.value 
				var calledFrom=document.EditList.hidCalledFrom.value
				var tabID=document.EditList.hidTabID.value
				qid = "<%=gstrQID%>";				
				var urlStr="";
				
					 urlStr="GridQuestion.aspx?qid="+qid+"&screenID="+screenID+"&groupID="+groupID+"&tabID="+tabID+"&calledFrom="+calledFrom+"&transferdata=";
							
				if(qid>0)
				{
					if(calledFrom!="G")
						window.opener.location = "GridQuestion.aspx?qid="+qid+"&screenID="+screenID+"&groupID="+groupID+"&tabID="+tabID+"&calledFrom="+calledFrom+"&transferdata=";							
					else
						window.opener.location = "PostGridQuestion.aspx?qid="+qid+"&screenID="+screenID+"&groupID="+groupID+"&tabID="+tabID+"&calledFrom="+calledFrom+"&transferdata=";							
						
				}
				else
				{
					if(calledFrom!="G")
						window.opener.location = "GridQuestion.aspx?qid=-1&screenID="+screenID+"&groupID="+groupID+"&tabID="+tabID+"&calledFrom="+calledFrom+"&transferdata=";						
					else
						window.opener.location = "PostGridQuestion.aspx?qid="+qid+"&screenID="+screenID+"&groupID="+groupID+"&tabID="+tabID+"&calledFrom="+calledFrom+"&transferdata=";							
						
				}
				
				window.close();								
			}
			
		//-->
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="0" leftmargin="0" rightmargin="0">
		<form id="EditList" method="post" runat="server">
			<table align="center" cellpadding="1" cellspacing="1" width="100%" border="0">
				<tr>
					<td height="20" colspan="2">&nbsp;
					</td>
				</tr>
				<tr>
											<td colspan="2" class="headereffectCenter">Edit User Defined List</td>
				</tr>
				<tr>
					<td class="midcolora" width="33%">
						<asp:Label ID="lblListtxt" Runat="server"></asp:Label>
					</td>
					<td class="midcolora" width="67%">
						<asp:Label ID="lblListName" Runat="server"></asp:Label>
					</td>
				</tr>
				<asp:Panel ID="pnlDdlDesc" Runat="server" Visible="True">
					<TR>
						<TD class="midcolora">
							<asp:Label id="lblCodeToDisp" Runat="server"></asp:Label></TD>
						<TD class="midcolora">
							<asp:DropDownList id="selDescp" Runat="server" AutoPostBack="True" EnableViewState="True"></asp:DropDownList></TD>
					</TR>
				</asp:Panel>
				<tr>
					<td class="midcolora">
						<asp:Label ID="lblDesctxt" Runat="server"></asp:Label>
					</td>
					<td class="midcolora"><asp:TextBox ID="txtDescp" Runat="server" MaxLength="200"></asp:TextBox>
						<asp:requiredfieldvalidator ForeColor="" id="reqListName" CssClass="errmsg" runat="server" display="dynamic"
							controltovalidate="txtDescp" Enabled="True"></asp:requiredfieldvalidator>
						<cmsb:cmsbutton class="clsButton" id="btnAddNew" runat="server" CausesValidation="false" Text='Add New'></cmsb:cmsbutton>
					</td>
				</tr>
				<tr>
					<td class="midcolorc" colspan="1" align="left">
						<cmsb:cmsbutton class="clsButton" id="btnClose" runat="server" CausesValidation="false" Text='Close'></cmsb:cmsbutton>
					</td>
					<td class="midcolorc" colspan="1" align="right">
						<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
					</td>
				</tr>
				<asp:Label ID="lblListID" Runat="server" Visible="False"></asp:Label>
				<asp:Label ID="lblInsertMode" Runat="server" Visible="False"></asp:Label>
				<asp:Label ID="lblQID" Runat="server" Visible="False"></asp:Label>
				<input type="hidden" name="hidListID" id="hidListID" runat="server"> <input type="hidden" name="hidQidID" id="hidQidID" runat="server">
				<input type="hidden" name="hidScreenID" id="hidScreenID" runat="server"> <input type="hidden" name="hidTabID" id="hidTabID" runat="server">
				<input type="hidden" name="hidGroupID" id="hidGroupID" runat="server"> <input type="hidden" name="hidCalledFrom" id="hidCalledFrom" runat="server">
			</table>
		</form>
	</body>
</HTML>

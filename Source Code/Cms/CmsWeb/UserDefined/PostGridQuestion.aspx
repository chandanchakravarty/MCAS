<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PostGridQuestion.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.UserDefined.PostGridQuestion" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GridQuation</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<Script Language="JavaScript">
		<% if (gIntInsertUpdateFlag==1)
		{
		 %>		 
		 parent.refreshGrid(<%=gIntReturn%>);
		<%
		}
		%>
		
		function ResetScreenForm()
		{
			document.SubmitQuestion.reset();  
			 
			DisableValidators();
		
		}
			function fnList()
			{
				//alert(document.SubmitQuestion.ddlList.value); 
				var lStrListID=document.SubmitQuestion.ddlList.value;
				var lStrViewMsg="<%=gStrViewMsg%>";
				
				if(document.SubmitQuestion.ddlList.value=="")
				{
					alert(lStrViewMsg);
				}
				else
				{
					window.open("EditList.aspx?ListID="+lStrListID,'','width=450,height=300,menubars=NO,top=250,left=300,statusbar=NO,toolbars=NO');					
				}
			}
			function fnAddList(strType)
			{
				//document.SubmitQuestion.hidQid.value   
				var qid =document.SubmitQuestion.hidQid.value
				var scID=document.SubmitQuestion.hidScreenID.value;   				
				var tabID=document.getElementById("hidTabID").value ;
				var grpID=document.getElementById("hidGroupID").value ;
				var clFrom='G' ;
				
				window.open("AddList.aspx?ListID="+strType+"&QID="+qid+"&screenID="+scID+"&tabID="+tabID+"&grpID="+grpID+"&calledFrom="+clFrom,'','width=450,height=300,menubars=NO,top=250,left=300,statusbar=NO,toolbars=NO');
				
			}
			function GoBack()
			{			
				var lStrBackID = "<%=lStrScreenID%>";
				var lStrTemplateID="<%=lStrTabID%>";		
				var lStrGroupID="<%=lStrGroupID%>";
				var CalledFrom="<%=gStrCalledFrom%>";	
				alert(CalledFrom);
				parent.location.href="SubmitQuestion.aspx?CalledFrom="+CalledFrom+"&TabID="+ lStrTemplateID +"&ScreenID="+lStrBackID+"&GroupID="+lStrGroupID;
			}
		</Script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" topmargin="0" leftmargin="0" rightmargin="0">
		<form id="SubmitQuestion" method="post" runat="server">
			<table cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td class="pageHeader" colspan="2" align="left">
						Please note that all fields marked with <SPAN class="Mandatory">*</SPAN> are 
						mandatory.
					</td>
				</tr>
				<asp:Panel ID="pnlMessage" Runat="server" Visible="False">
					<TR>
						<TD class="midcolorc" align="center" colSpan="2">
							<asp:label id="lblError" runat="server" Visible="False" cssclass="errmsg"></asp:label></TD>
					</TR>
				</asp:Panel>
				<TR>
					<td class="midcolora" vAlign="top" width="20%">
						<asp:Label Runat="server" ID="lblQuestion"></asp:Label><span class='mandatory'>*</span>
					</td>
					<td class="midcolora" width="80%">
						<asp:textbox id="txtQuestion" cssclass="TEXTAREA" Runat="server" Textmode="multiline" Rows="500"
							Columns="50" Height="50" Width="450"></asp:textbox><br>
						<asp:requiredfieldvalidator ForeColor="" id="reqtxtQuestion" cssclass="errmsg" runat="server" display="dynamic"
							controltovalidate="txtQuestion"></asp:requiredfieldvalidator><br>
						<asp:customvalidator id="CVtxtQuestionDesc" ForeColor="" runat="server" cssclass="errmsg" Display="Dynamic"
							clientvalidationfunction="txtAreaChk" ControlToValidate="txtQuestion"></asp:customvalidator>
						<script>
						function txtAreaChk(source, arguments)
						{
							var txtAreaVal = arguments.Value;
							if(txtAreaVal.length > 200) {
								arguments.IsValid = false;
								return;   
							}
						}
						</script>
					</td>
				</TR>
				<asp:Panel ID="pnlLayout" Runat="server" Visible="False">
					<TR>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblLayout" Runat="server"></asp:Label><span class='mandatory'>*</span></TD>
						<TD class="midcolora">
							<asp:DropDownList id="ddllayout" Runat="server" cssclass="SELECT" AutoPostBack="True">
								<asp:ListItem Value="H">Horizontal</asp:ListItem>
								<asp:ListItem Value="V">Vertical</asp:ListItem>
							</asp:DropDownList><BR>
							<asp:requiredfieldvalidator id="reqddlLayout" runat="server" cssclass="errmsg" controltovalidate="ddllayout"
								display="dynamic" ForeColor=""></asp:requiredfieldvalidator></TD>
					</TR>
				</asp:Panel>
				<asp:Panel ID="pnlQuesType" Runat="server" Visible="true">
					<TR>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblQuestionType" Runat="server"></asp:Label><span class='mandatory'>*</span></TD>
						<TD class="midcolora">
							<asp:DropDownList id="ddlQuesType" Runat="server" cssclass="SELECT" AutoPostBack="true" EnableViewState="True"></asp:DropDownList><BR>
							<asp:requiredfieldvalidator id="reqddlQuesType" runat="server" cssclass="errmsg" controltovalidate="ddlQuesType"
								display="dynamic" ForeColor=""></asp:requiredfieldvalidator></TD>
					</TR>
				</asp:Panel>
				<asp:Panel ID="pnlAnsType" Runat="server" Visible="False">
					<TR>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblAnsType" Runat="server" visible="false"></asp:Label><span class='mandatory'>*</span></TD>
						<TD class="midcolora">
							<asp:DropDownList id="ddlAnswerType" Runat="server" cssclass="SELECT" AutoPostBack="true" visible="false"></asp:DropDownList><BR>
							<asp:requiredfieldvalidator id="reqddlAnswerType" runat="server" cssclass="errmsg" controltovalidate="ddlAnswerType"
								ForeColor="" Display="Dynamic"></asp:requiredfieldvalidator></TD>
					</TR>
				</asp:Panel>
				<asp:Panel ID="pnlDdlList" Runat="server" Visible="False">
					<TR>
						<TD class="midcolora" vAlign="top">
							<asp:Label id="lblList" Runat="server" visible="false"></asp:Label><span class='mandatory'>*</span></TD>
						<TD class="midcolora">
							<asp:DropDownList id="ddlList" Runat="server" cssclass="SELECT" visible="false"></asp:DropDownList>
							<% if(gStrType!="S" && gStrType!="")
							{
							%>
							<%=gStrAddLink1%>
							<A class=Dark 
      href="Javascript:fnAddList('<%=gStrType%>');">
								<%=gStrAddLink2%>
							</A>
							<%=gStrAddLink3%>
							<BR>
							<%= gStrViewLink1 %>
							<A class="Dark" href="JavaScript:fnList();">
								<%= gStrViewLink2 %>
							</A>
							<%= gStrViewLink3 %>
							<%
							}
							%>
							<BR>
							<asp:requiredfieldvalidator id="reqddlListType" runat="server" cssclass="errmsg" controltovalidate="ddlList"
								display="dynamic" ForeColor=""></asp:requiredfieldvalidator></TD>
					</TR>
				</asp:Panel>
				<tr>
					<td valign="top" class="midcolora">
						<asp:Label Runat="server" ID="lblIsMandatory"></asp:Label>
					</td>
					<td class="midcolora">
						<asp:DropDownList ID="ddlMandatory" cssclass="SELECT" Runat="server"></asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td align="left" valign="top">
						<!--	<input type="button" ID="bnCancel" onmouseover="f_ChangeClass(this, 'HoverButton');" onmouseout="f_ChangeClass(this, 'clsButton');" class="clsButton" onclick="javascript:GoBack();" Runat="server" NAME="btnCancel">-->
						<input type="reset" ID="btnReset" class="clsButton" Runat="server" NAME="btnReset" value="Reset">
					</td>
					<td align="right" valign="top">
						<asp:Button ID="btnSave" cssclass="clsButton" Runat="server" OnClick="btnSave_Click"></asp:Button>
					</td>
				</tr>
				<asp:Panel Visible="False" Runat="server" ID="Panel1" NAME="Panel1">
					<TR>
						<TD>
							<asp:label id="lblupdate" runat="server" Visible="False" cssclass="errmsg"></asp:label>
							<asp:label id="lblerrmsg" runat="server" Visible="False" cssclass="errmsg"></asp:label>
							<asp:label id="lblinsert" runat="server" Visible="False" cssclass="errmsg"></asp:label>
							<asp:label id="lblGridType" runat="server" Visible="False" cssclass="errmsg"></asp:label>
							<asp:Label id="lblTemplateID" Visible="False" Runat="server"></asp:Label>
							<asp:Label id="lblTabID" Visible="False" Runat="server"></asp:Label>
							<asp:Label id="lblgroup" Visible="False" Runat="server"></asp:Label>
							<asp:Label id="lblQID" Visible="False" Runat="server"></asp:Label>
							<asp:Label id="lblGridID" Visible="False" Runat="server"></asp:Label>
							<asp:Label id="lblBackID" Visible="False" Runat="server"></asp:Label>
							<asp:label id="lblGroupID" Visible="False" Runat="server"></asp:label></TD>
					</TR>
				</asp:Panel>
			</table>
			<input type="hidden" name="hidScreenID" id="hidScreenID" runat="server"> <input type="hidden" name="hidTabID" id="hidTabID" runat="server">
			<input type="hidden" name="hidGroupID" id="hidGroupID" runat="server"> <input type="hidden" name="hidCalledFrom" id="hidCalledFrom" runat="server">
			<input type="hidden" name="hidQid" id="hidQid" runat="server">
		</form>
	</body>
</HTML>

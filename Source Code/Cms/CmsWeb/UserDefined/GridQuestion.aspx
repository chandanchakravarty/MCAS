<%@ Page language="c#" Codebehind="GridQuestion.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.UserDefined.GridQuestion" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GridQuation</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
			
			<% if (gIntInsertUpdateFlag==1)
			{
			 %>	
			 var retID="<%=gIntReturn%>";			 
			 var gpID="<%=grpID%>";			 
			 var tbID="<%=lStrTabID%>"; 			 
			 var scID="<%=lStrScreenID%>";			
			 var val=retID + "^" + gpID + "^" + scID + "^" + tbID;		
			 RefreshWebGrid(1,val,false);
			<%
			}
			%>

		
			<% if (gIntGridQuestion>0)
			{
			%>
				var lStrQID = "<%=gIntReturn%>";
				var lStrBackID = "<%=lStrScreenID%>";
				var lStrTabID="<%=lStrTabID%>";
				var lStrGroup="<%=lStrGroup%>";
				var lStrGroupID="<%=grpID%>";
				var CalledFrom ="<%=gStrCalledFrom%>";
				
				parent.location.href="SubmitGridQuestion.aspx?CalledFrom="+CalledFrom+"&QID="+lStrQID+"&TabID="+lStrTabID+"&ScreenID="+lStrBackID+"&GroupID="+lStrGroupID;
			<%
			}
			%>
		
			function fnList()
			{
				var lStrViewMsg="<%=gStrViewMsg%>";
				var qid =document.SubmitQuestion.hidQid.value
		
				var scID=document.SubmitQuestion.hidScreenID.value;   				
				var tabID=document.getElementById("hidTabID").value ;
				var grpID=document.getElementById("hidGroupID").value ;
				var clFrom=document.getElementById("hidCalledFrom").value ;
				if(qid=="")
				{
					qid="-1";
				}
				var strListID=document.SubmitQuestion.ddlList.value;
		
				if(document.SubmitQuestion.ddlList.value=="")
				{
					alert("Please select an item from the List");
				}
				else
				{
					window.open("EditList.aspx?ListID="+strListID+"&QID="+qid+"&screenID="+scID+"&tabID="+tabID+"&grpID="+grpID+"&calledFrom="+clFrom ,'','width=450,height=300,menubars=NO,top=250,left=300,statusbar=NO,toolbars=NO');
				}
			}
			
			function fnAddList(strType)
			{	
				var qid =document.SubmitQuestion.hidQid.value
				var scID=document.SubmitQuestion.hidScreenID.value;   				
				var tabID=document.getElementById("hidTabID").value ;
				var grpID=document.getElementById("hidGroupID").value ;
				var clFrom=document.getElementById("hidCalledFrom").value ;
				
				window.open("AddList.aspx?ListID="+strType+"&QID="+qid+"&screenID="+scID+"&tabID="+tabID+"&grpID="+grpID+"&calledFrom="+clFrom,'','width=450,height=300,menubars=NO,top=250,left=300,statusbar=NO,toolbars=NO');				
			}
			
			function GoBack()
			{
				var lStrGroup;
				var lStrBack;
				var lStrTab;
				var CalledFrom;
				CalledFrom="<%=gStrCalledFrom%>";
				lStrBack="<%=lStrScreenID%>";				
				lStrGroupID="<%=lStrGroupID%>";				
				lStrTab="<%=lStrTabID%>"				
				if(lStrGroupID!="")
				{					
					parent.location.href="SubmitGroups.aspx?CalledFrom="+CalledFrom+"&TabID="+lStrTab+"&ScreenID="+lStrBack;
				}
				else
				{
					parent.location.href="SubmitTab.aspx?CalledFrom="+CalledFrom+"&TemplateID="+lStrBack;
				}
				 
			}
			
			function ResetScreenForm()
			{
				document.SubmitQuestion.reset();  
				 
				DisableValidators();
			
			}
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="SubmitQuestion" method="post" runat="server">
			<TABLE cellSpacing="1" cellPadding="1" width="100%" border="0">
				<asp:panel id="pnlMessage" Visible="False" Runat="server">
					<TBODY>
						<TR>
							<TD class="midcolorc" align="center" colSpan="4">
								<asp:label id="lblError" runat="server" Visible="False" cssclass="errmsg"></asp:label></TD>
						</TR>
				</asp:panel>
				<TR>
					<td class="midcolora" vAlign="top" width="20%"><asp:label id="lblQuestion" Runat="server"></asp:label><span class="mandatory" id="spnQuestion">*</span>
					</td>
					<td class="midcolora" width="80%" colSpan="3"><asp:textbox id="txtQuestion" Runat="server" cssclass="TEXTAREA" Textmode="multiline" Width="450"
							Height="50" Columns="50" Rows="500"></asp:textbox><asp:requiredfieldvalidator id="reqtxtQuestion" runat="server" cssclass="errmsg" display="dynamic" controltovalidate="txtQuestion"
							ForeColor=""></asp:requiredfieldvalidator><br>
						<asp:customvalidator id="CVtxtQuestionDesc" runat="server" cssclass="errmsg" ForeColor="" ControlToValidate="txtQuestion"
							clientvalidationfunction="txtAreaChk" Display="Dynamic"></asp:customvalidator>
						<script>
						function txtAreaChk(source, arguments)
						{
							var txtAreaVal = arguments.Value;
							if(txtAreaVal.length > 500) {
								arguments.IsValid = false;
								return;   
							}
						}
						</script>
					</td>
				</TR>
				<tr>
					<td class="midcolora" vAlign="top"><asp:label id="lblQuestionType" Runat="server"></asp:label><span class="mandatory" id="spnQuestiontype">*</span>
					</td>
					<td class="midcolora" colSpan="3"><asp:dropdownlist id="ddlQuesType" Runat="server" cssclass="SELECT" EnableViewState="True" AutoPostBack="true"></asp:dropdownlist><br>
						<asp:requiredfieldvalidator id="reqddlQuesType" runat="server" cssclass="errmsg" display="dynamic" controltovalidate="ddlQuesType"
							ForeColor=""></asp:requiredfieldvalidator></td>
				</tr>
				<asp:panel id="pntTotalField" Visible="False" Runat="server">
					<TR>
						<TD class="midcolora" vAlign="top">
							<asp:label id="lblTotalField" Runat="server"></asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:CheckBox id="chkTotal" Runat="server"></asp:CheckBox></TD>
					</TR>
				</asp:panel><asp:panel id="pnlDdlAnsType" Visible="False" Runat="server">
					<TR>
						<TD class="midcolora" vAlign="top">
							<asp:label id="lblAnsType" Runat="server" visible="false"></asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:dropdownlist id="ddlAnswerType" Runat="server" cssclass="SELECT" AutoPostBack="true" visible="false"></asp:dropdownlist>
							<asp:requiredfieldvalidator id="reqddlAnswerType" runat="server" cssclass="errmsg" ForeColor="" controltovalidate="ddlAnswerType"
								Display="Dynamic"></asp:requiredfieldvalidator></TD>
					</TR>
				</asp:panel><asp:panel id="pnlDdlList" Visible="False" Runat="server">
					<TR>
						<TD class="midcolora" vAlign="top">
							<asp:label id="lblList" Runat="server" visible="false"></asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:dropdownlist id="ddlList" Runat="server" cssclass="SELECT" AutoPostBack="True" visible="false"></asp:dropdownlist><% if(gstrType!="S" && gstrType!="")
								{
							%><%=lStrAddLink1%><A class=Dark 
      href="Javascript:fnAddList('<%=gstrType%>');"><%=lStrAddLink2%></A><%=lStrAddLink3%><BR>
							<%= lStrViewLink1 %>
							<A class="Dark" href="JavaScript:fnList();">
								<%= lStrViewLink2 %>
							</A>
							<%= lStrViewLink3 %>
							<%	}	%>
							<asp:requiredfieldvalidator id="reqddlListType" runat="server" cssclass="errmsg" ForeColor="" controltovalidate="ddlList"
								display="dynamic"></asp:requiredfieldvalidator></TD>
					</TR>
				</asp:panel><asp:panel id="pnlChkDescription" Visible="False" Runat="server">
					<TR>
						<TD class="midcolora" vAlign="top">
							<asp:label id="lblDescRequired" Runat="server" Visible="False"></asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:checkbox id="chkDesc" Runat="server" Visible="False" AutoPostBack="True"></asp:checkbox></TD>
					</TR>
				</asp:panel><asp:panel id="pnlDdlDescription" Visible="False" Runat="server">
					<TR>
						<TD class="midcolora" vAlign="top">
							<asp:label id="lblSpecify" Runat="server" Visible="False"></asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<TABLE cellSpacing="1" cellPadding="0" width="90%" border="0">
								<TR class="midcolora">
									<TD rowSpan="2">
										<asp:ListBox id="lstOptionList" Runat="server" cssclass="SELECT" Height="50px" visible="false"
											SelectionMode="Multiple"></asp:ListBox><BR>
										<asp:requiredfieldvalidator id="reqlstOptionList" runat="server" cssclass="errmsg" ForeColor="" controltovalidate="lstOptionList"
											display="dynamic" Enabled="false"></asp:requiredfieldvalidator></TD>
									<TD>Description Text</TD>
									<TD>
										<asp:TextBox id="txtDepQuesDescription" Runat="server" Width="200px"></asp:TextBox></TD>
									<TD>Description Type</TD>
									<TD>
										<asp:dropdownlist id="ddlDepQuesType" Runat="server" cssclass="SELECT" AutoPostBack="true" EnableViewState="True">
											<asp:ListItem value=""></asp:ListItem>
											<asp:ListItem value="1">Single Select List</asp:ListItem>
											<asp:ListItem value="2">Multi Select List</asp:ListItem>
											<asp:ListItem value="4">Number</asp:ListItem>
											<asp:ListItem value="5">Date</asp:ListItem>
											<asp:ListItem value="6">Text</asp:ListItem>
											<asp:ListItem value="7">Text Area</asp:ListItem>
											<asp:ListItem value="13">Radio Button List</asp:ListItem>
											<asp:ListItem value="14">Check Box List</asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvDepQuesTYpe" runat="server" cssclass="errmsg" ForeColor="" controltovalidate="ddlDepQuesType"
											display="dynamic"></asp:requiredfieldvalidator></TD>
								</TR>
								<%if ( ddlDepQuesType.SelectedItem.Value == "1" || ddlDepQuesType.SelectedItem.Value == "2" || ddlDepQuesType.SelectedItem.Value == "13" || ddlDepQuesType.SelectedItem.Value == "14")
								{%>
								<TR class="midcolora">
									<TD>Answer Type</TD>
									<TD>
										<asp:dropdownlist id="ddlDepAnswerType" Runat="server" cssclass="SELECT" AutoPostBack="true" EnableViewState="True"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="reqDepAnswerType" runat="server" cssclass="errmsg" ForeColor="" controltovalidate="ddlDepAnswerType"
											display="dynamic" ErrorMessage="Select an item from list."></asp:requiredfieldvalidator></TD>
									<TD>
										<asp:Label id="lblDepList" Runat="server">List</asp:Label></TD>
									<TD>
										<asp:dropdownlist id="ddlDepList" Runat="server" cssclass="SELECT" EnableViewState="True"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="reqDepList" runat="server" cssclass="errmsg" ForeColor="" controltovalidate="ddlDepList"
											display="dynamic" ErrorMessage="Select an item from list."></asp:requiredfieldvalidator></TD>
								</TR>
								<%}
								else
								{%>
								<TR class="midcolora">
									<TD colSpan="4"></TD>
								</TR>
								<%}%>
							</TABLE>
						</TD>
					</TR>
				</asp:panel><asp:panel id="pnlCommonQuestion" Visible="True" Runat="server">
					<TR>
						<TD class="midcolora" vAlign="top" width="20%">
							<asp:label id="lblIsMandatory" Runat="server"></asp:label></TD>
						<TD class="midcolora" width="20%">
							<asp:dropdownlist id="ddlMandatory" Runat="server" cssclass="SELECT"></asp:dropdownlist></TD>
						<TD class="midcolora" vAlign="top" width="15%">
							<asp:label id="lblColumnSpan" Runat="server"></asp:label></TD>
						<TD class="midcolora">
							<asp:TextBox id="txtRepatableColumns" runat="server" Width="54px">1</asp:TextBox>
							<asp:RequiredFieldValidator id="rfvRepeatCol" runat="server" Display="Dynamic" ControlToValidate="txtRepatableColumns"
								CssClass="errmsg"></asp:RequiredFieldValidator>
							<asp:CompareValidator id="cvRepCols" runat="server" Display="Dynamic" ControlToValidate="txtRepatableColumns"
								CssClass="errmsg" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
							<asp:RangeValidator id="rvRepeatColumns" runat="server" Display="Dynamic" ControlToValidate="txtRepatableColumns"
								CssClass="errmsg" Type="Integer" MinimumValue="1" MaximumValue="5"></asp:RangeValidator></TD>
					</TR>
					<TR class="midcolora" id="trHeightStyle" runat="server">
						<TD>
							<asp:label id="lblHeight" Runat="server"></asp:label></TD>
						<TD>
							<asp:TextBox id="txtHeight" Runat="server" Width="54px"></asp:TextBox>
							<asp:DropDownList id="ddlHeightMsrment" Runat="server">
								<asp:ListItem Value="%">%</asp:ListItem>
								<asp:ListItem Value="Px">Px</asp:ListItem>
								<asp:ListItem Value="Pt">Pt</asp:ListItem>
							</asp:DropDownList>
							<asp:CompareValidator id="cvHeight" runat="server" Display="Dynamic" ControlToValidate="txtHeight" CssClass="errmsg"
								Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></TD>
						<TD>
							<asp:label id="lblWidth" Runat="server"></asp:label></TD>
						<TD>
							<asp:TextBox id="txtWidth" Runat="server" Width="54px"></asp:TextBox>
							<asp:DropDownList id="ddlWidthMsrment" Runat="server">
								<asp:ListItem Value="%">%</asp:ListItem>
								<asp:ListItem Value="Px">Px</asp:ListItem>
								<asp:ListItem Value="Pt">Pt</asp:ListItem>
							</asp:DropDownList>
							<asp:CompareValidator id="cvWidth" runat="server" Display="Dynamic" ControlToValidate="txtWidth" CssClass="errmsg"
								Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></TD>
					</TR>
					<TR class="midcolora" id="trCharacterStyle" runat="server">
						<TD>
							<asp:label id="lblMaxCharacters" Runat="server"></asp:label></TD>
						<TD>
							<asp:TextBox id="txtMaxLength" Runat="server" Width="54px"></asp:TextBox>
							<asp:CompareValidator id="cvMaxChars" runat="server" Display="Dynamic" ControlToValidate="txtMaxLength"
								CssClass="errmsg" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></TD>
						<TD>
							<asp:label id="lblValidationType" Runat="server"></asp:label></TD>
						<TD>
							<asp:DropDownList id="ddlValidation" Runat="server">
								<asp:ListItem Value=""></asp:ListItem>
								<asp:ListItem value="^\+?\d{0,3}(\s|-|\s)?(\d{0,4}|\(\d{1,4}\))(\s|-|\s)?\d{1,4}(\s|-|\s)?\d{1,4}$">Phone</asp:ListItem>
								<asp:ListItem value="^\d{0,4}$">Extn</asp:ListItem>
								<asp:ListItem value="^\+?\d{0,3}(\s|-|\s)?(\d{0,4}|\(\d{0,4}\))(\s|-|\s)?\d{1,4}(\s|-|\s)?\d{1,4}$">Fax</asp:ListItem>
								<asp:ListItem value="^\+?\d{0,3}(\s|-|\s)?(\d{0,4}|\(\d{0,4}\))(\s|-|\s)?\d{1,5}(\s|-|\s)?\d{1,5}$">Mobile</asp:ListItem>
								<asp:ListItem value="^([a-zA-Z0-9]+(\s|\-)*)*[a-zA-Z][a-zA-Z0-9\s]*$">AlphaNum</asp:ListItem>
								<asp:ListItem value="^[a-zA-Z][a-zA-Z\s\-]*[a-zA-Z\s]$">Alpha</asp:ListItem>
								<asp:ListItem value="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$">Email</asp:ListItem>
								<asp:ListItem value="^\d+$">Number</asp:ListItem>
								<asp:ListItem value="^\d+(\.\d+)?$">Decimal</asp:ListItem>
								<asp:ListItem value="^\d+((\.\d{2})|(\.\d{4}))$">Currency</asp:ListItem>
								<asp:ListItem value="^\d{0,3}(\.\d{1,2})?$">Rate</asp:ListItem>
							</asp:DropDownList></TD>
					</TR>
					<TR>
						<TD class="midcolora" vAlign="top">
							<asp:label id="lblSuffix" Runat="server"></asp:label></TD>
						<TD class="midcolora">
							<asp:textbox id="txtsuffix" Runat="server" cssclass="INPUT" MaxLength="100"></asp:textbox>
							<asp:Label id="lblsfxex" Runat="server"></asp:Label></TD>
						<TD class="midcolora" vAlign="top">
							<asp:label id="lblPrefix" Runat="server"></asp:label></TD>
						<TD class="midcolora">
							<asp:textbox id="txtprefix" Runat="server" cssclass="INPUT" MaxLength="100"></asp:textbox>
							<asp:Label id="lblprefixex" Runat="server"></asp:Label></TD>
					</TR>
					<TR>
						<TD class="midcolora" vAlign="top">
							<asp:label id="lblQuestionNotes" Runat="server"></asp:label></TD>
						<TD class="midcolora" colSpan="3">
							<asp:textbox id="txtQuestionNotes" Runat="server" cssclass="Textarea" Columns="50" Height="50"
								Width="450" Textmode="multiline"></asp:textbox><BR>
							<asp:customvalidator id="CvQuesNotes" runat="server" cssclass="errmsg" ForeColor="" Display="Dynamic"
								clientvalidationfunction="txtAreaChk" ControlToValidate="txtQuestionNotes"></asp:customvalidator>
							<SCRIPT>
						function txtAreaChk(source, arguments)
						{
							var txtAreaVal = arguments.Value;
							if(txtAreaVal.length > 500) {
								arguments.IsValid = false;
								return;   
							}
						}
							</SCRIPT>
						</TD>
					</TR>
				</asp:panel>
				<tr>
					<td class="midcolora" vAlign="top" noWrap align="left" colSpan="2"><input class="clsButton" id="btnReset" type="button" value="Reset" name="btnReset" runat="server" CausesValidation="false" onserverclick="btnReset_ServerClick"><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"></cmsb:cmsbutton>
					</td>
					<td class="midcolorr" align="right" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
					<!--<td vAlign="top" align="right"><input class="clsButton" id="btnCancel" onmouseover="f_ChangeClass(this, 'HoverButton');" onmouseout="f_ChangeClass(this, 'clsButton');" onclick="javascript:GoBack();" type="button" name="btnCancel" Runat="server">--></tr>
				<tr>
					<td><asp:textbox id="txtDeactivateVal" Visible="False" Runat="server"></asp:textbox><asp:label id="lblTemplateID" Visible="False" Runat="server"></asp:label><asp:label id="lblBackID" Visible="False" Runat="server"></asp:label><asp:label id="lblTabID" Visible="False" Runat="server"></asp:label><asp:label id="lblGroupID" Visible="False" Runat="server"></asp:label><asp:label id="lblGroupStatus" Visible="False" Runat="server"></asp:label><asp:label id="lblordexist" runat="server" Visible="False" cssclass="errmsg"></asp:label><asp:label id="lblupdate" runat="server" Visible="False" cssclass="errmsg"></asp:label><asp:label id="lblerrmsg" runat="server" Visible="False" cssclass="errmsg"></asp:label><asp:label id="lblinsert" runat="server" Visible="False" cssclass="errmsg"></asp:label><asp:label id="lblGridBtn" runat="server" Visible="False"></asp:label><asp:label id="lblSaveBtn" runat="server" Visible="False"></asp:label><input id="hidScreenID" type="hidden" name="hidScreenID" runat="server">
						<input id="hidTabID" type="hidden" name="hidTabID" runat="server"> <input id="hidGroupID" type="hidden" name="hidGroupID" runat="server">
						<input id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server"> <input id="hidQid" type="hidden" name="hidQid" runat="server">
					</td>
				</tr>
				</TBODY></TABLE>
		</form>
	</body>
</HTML>

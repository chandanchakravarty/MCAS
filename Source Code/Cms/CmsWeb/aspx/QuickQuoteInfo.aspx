<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="QuickQuoteInfo.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.aspx.QuickQuoteInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>QuickQuote Info</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		function DoBack()
		{
			this.parent.document.location.href = "/Cms/Client/aspx/CustomerManagerSearch.aspx";
			return false;
		}
		function DoBack_Parent()
		{
			this.parent.parent.document.location.href = "/Cms/Client/aspx/CustomerManagerSearch.aspx";
			return false;
		}
		function DoBackToAssistant()
		{
			
			this.parent.document.location.href = "/Cms/Client/aspx/CustomerManagerIndex.aspx";
			return false;
		}
		function DoBackToAssistant_Parent()
		{
			
			this.parent.parent.document.location.href = "/Cms/Client/aspx/CustomerManagerIndex.aspx";
			return false;
		}
		
		function SetState()
		{
		  //Added By shafi check for Delete
		  
		  /*if(document.getElementById('hidChkDelete') && document.getElementById('hidChkDelete').value!="1")
			{
					if (document.QuickQuoteInfo.cmbQQ_TYPE(document.QuickQuoteInfo.cmbQQ_TYPE.selectedIndex).value=='BOAT' && document.QuickQuoteInfo.cmbQQ_State.options.length == 2)
					{
						document.QuickQuoteInfo.cmbQQ_State.options[2]=new Option('Wisconsin','WISCONSIN');
						if (document.QuickQuoteInfo.hidState.value=="Wisconsin")
						  document.QuickQuoteInfo.cmbQQ_State.options.selectedIndex=2;
					}
					else if (document.QuickQuoteInfo.cmbQQ_TYPE(document.QuickQuoteInfo.cmbQQ_TYPE.selectedIndex).value!='BOAT' && document.QuickQuoteInfo.cmbQQ_State.options.length == 3)
					{
						document.QuickQuoteInfo.cmbQQ_State.remove(2);
					}
			}
		  */	
		}
		
		// confirms application .
		function confirmApplication()
		{
			//var confirmAction = confirm("Do you really want to commit this deposit?");
			var confirmAction = confirm("This action will convert selected quote into application, do you want to continue?");
			if(confirmAction)
				return true;
			else
				return false;
        }

        function GoToPersonalDetail() {
            parent.document.location.href = "/cms/cmsweb/aspx/PersonalDetailTab.aspx?CalledFromMenu=Y";
            return false;
        }
        		
		
		function Focus()
		{
			try
			{
				//ADDED IF CONDITION TO SET THE DEFAULT FOCUS ON THE STATE DROPDOWN AS PER ISSUE 5184:PRAVEEN KUMAR(27-02-2009)
				if(document.getElementById('cmbQQ_State')== null || document.getElementById('cmbQQ_State').disabled == true)
				{
					if(document.getElementById('btnQuickQuote').value != "" )
					{
						if(document.getElementById('btnQuickQuote').style.visibility != "" || document.getElementById('btnQuickQuote').style.visibility != 'hidden')
								document.getElementById('btnQuickQuote').focus();
					}
				}
				
				else if(document.getElementById('cmbQQ_TYPE')== null || document.getElementById('cmbQQ_TYPE').disabled == true)
				{
					if(document.getElementById('btnQuickQuote').value != "" )
					{
						if(document.getElementById('btnQuickQuote').style.visibility != "" || document.getElementById('btnQuickQuote').style.visibility != 'hidden')
								document.getElementById('btnQuickQuote').focus();
					}
				}
				else
				{
					if (document.getElementById('cmbQQ_State').disabled == false)
					{
							if(document.getElementById('cmbQQ_State').style.visibility != "" || document.getElementById('cmbQQ_State').style.visibility != 'hidden')
								document.getElementById('cmbQQ_State').focus();
					}
					else if (document.getElementById('cmbQQ_TYPE').disabled == false)
					{
							if(document.getElementById('cmbQQ_TYPE').style.visibility != "" || document.getElementById('cmbQQ_TYPE').style.visibility != 'hidden')
								document.getElementById('cmbQQ_TYPE').focus();
					}
				}
				
			
			}
			catch(e)
			{
			}	
		}
		</script>
</HEAD>
	<body oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="ApplyColor();ChangeColor();setTimeout('Focus()',500);">
		<form id="QuickQuoteInfo" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				
				<TR>
					<TD>
					
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
									mandatory</TD>
							</tr>
							<tr>
								<td class="midcolorc" style="HEIGHT: 9px" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							<tbody id="tbBody" runat="server">
							<%if (DeleteFlag) {%>
							<tr>
								<TD class="midcolora" style="HEIGHT: 19px"><asp:label id="capQQ_NUMBER" runat="server">Quote Number</asp:label><span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" style="HEIGHT: 19px" colSpan="3"><asp:label id="lblQuickQuoteNumber" runat="server"></asp:label><BR>
								</TD>
							</tr>
							
							<tr id="trState" runat="server" visible="false">
								<TD class="midcolora" style="HEIGHT: 18px">
									<asp:Label id="lblStateName" runat="server">State</asp:Label><span class="mandatory">*</span></TD>
								<TD class="midcolora" style="HEIGHT: 18px">
									<asp:DropDownList id="cmbQQ_State" runat="server" onfocus="SelectComboIndex('cmbQQ_State')" AutoPostBack="True"></asp:DropDownList>
									<asp:RequiredFieldValidator id="rfvQQ_State" runat="server" ControlToValidate="cmbQQ_State" ErrorMessage="State can't be blank"
										Display="Dynamic"></asp:RequiredFieldValidator></TD>
								<TD class="midcolora" colSpan="2" style="HEIGHT: 18px"></TD>
							</tr>
							
							<tr>
								<TD class="midcolora"><asp:label id="capQQ_TYPE" runat="server">MainClass</asp:label><span class="mandatory">*</span>
								</TD>
								<TD class="midcolora"><asp:dropdownlist id="cmbQQ_TYPE" onfocus="SelectComboIndex('cmbQQ_TYPE')" runat="server" onchange="javascript:SetState();"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvQQ_TYPE" runat="server" Display="Dynamic" ErrorMessage=""
										ControlToValidate="cmbQQ_TYPE"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" colSpan="2"></TD>
							</tr>
							
							<tr>
								<td class="midcolora" align="left" colSpan="2">
									<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Deactivate"></cmsb:cmsbutton></td>
								<td class="midcolorr" align="right" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnAddToApplication" runat="server" Text="Make Application"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnQuickQuote" runat="server" Text="Run Quick Quote"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
							<%}%>
							<tr>
								<td class="midcolora" align="left" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnCustomerAssistant" runat="server" Text="Back To Customer Assistant"></cmsb:cmsbutton>
								</td>
								<td class="midcolorr" align="right" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Text="Back To Search"></cmsb:cmsbutton></td>
							</tr>
							<INPUT id="hidCustomerId" type="hidden" value="0" name="hidCustomerId" runat="server">
							<INPUT id="hidQuoteId" type="hidden" value="0" name="hidQuoteId" runat="server">
							<INPUT id="hidState" type="hidden" value="0" name="hidState" runat="server"> <INPUT id="hidCalledFromMenu" type="hidden" value="N" name="hidCalledFromMenu" runat="server">
							<INPUT id="hidChkDelete" type=hidden value="0" name="hidChkDelete" runat=server>
						</TABLE>
					</TD>
				</TR>
				</tbody>
			</TABLE>
		
			<%if (UpdateGrid) {%>
			<script language="javascript">
				if (document.getElementById('hidCalledFromMenu').value == "N")
			 	{
			 	//alert('<%=primaryKeyValues%>')
			 		RefreshWebGrid('1',"<%=primaryKeyValues%>",false);
				}
			</script>
			<%}%>
			<script language="javascript">
				SetState();
			</script>
		</form>
	</body>
</HTML>

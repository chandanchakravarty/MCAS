<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddCatastropheEvent.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Claims.AddCatastropheEvent" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>CLM_CATASTROPHE_EVENT</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language='javascript'>
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		function AddData()
		{
			ChangeColor();
			DisableValidators();
			document.getElementById('hidCATASTROPHE_EVENT_ID').value	=	'New';
			//document.getElementById('cmbCATASTROPHE_EVENT_TYPE').focus();
			document.getElementById('cmbCATASTROPHE_EVENT_TYPE').options.selectedIndex = -1;
			document.getElementById('txtDATE_FROM').value  = '';
			document.getElementById('txtDATE_TO').value  = '';
			document.getElementById('txtDESCRIPTION').value  = '';
			document.getElementById('txtCAT_CODE').value  = '';			
		}
		function CompareDateFromWithDateTo(objSource , objArgs)
		{
			var dob=document.CLM_CATASTROPHE_EVENT.txtDATE_FROM.value;
			var expdate=document.CLM_CATASTROPHE_EVENT.txtDATE_TO.value;
			if (dob != "")
			{
				objArgs.IsValid = CompareTwoDate(expdate,dob,jsaAppDtFormat);
			}
		}			
		
		function CompareTwoDate(DateFirst, DateSec, FormatOfComparision)
		{
			var saperator = '/';
			var firstDate, secDate;
			var strDateFirst = DateFirst.split("/");
			var strDateSec = DateSec.split("/");
			if(FormatOfComparision.toLowerCase() == "dd/mm/yyyy")
			{			
				firstDate = (strDateFirst[1].length != 2 ? '0' + strDateFirst[1] : strDateFirst[1]) + '/' + (strDateFirst[0].length != 2 ? '0' + strDateFirst[0] : strDateFirst[0])  + '/' + (strDateFirst[2].length != 4 ? '0' + strDateFirst[2] : strDateFirst[2]);
				secDate = (strDateSec[1].length != 2 ? '0' + strDateSec[1] : strDateSec[1]) + '/' + (strDateSec[0].length != 2 ? '0' + strDateSec[0] : strDateSec[0])  + '/' + (strDateSec[2].length != 4 ? '0' + strDateSec[2] : strDateSec[2]);
			}
			if(FormatOfComparision.toLowerCase() == "mm/dd/yyyy")
			{				
				firstDate = DateFirst
				secDate = DateSec;
			}
			firstDate = new Date(firstDate);
			secDate = new Date(secDate);
			firstSpan = Date.parse(firstDate);
			secSpan = Date.parse(secDate);
			if(firstSpan >= secSpan) 
				return true;	// first is greater
			else 
				return false;	// secound is greater
		}	
		function populateXML() {
		    
			if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
				{
					var tempXML;	 
					if(document.getElementById('hidOldData')!=null)
					{
						tempXML=document.getElementById('hidOldData').value;									 							
						if(tempXML!="" && tempXML!=0)
						{
						    populateFormData(tempXML, CLM_CATASTROPHE_EVENT);
						    document.getElementById('txtDATE_FROM').value = document.getElementById('hidDATE_FROM_TEXT').value;
						    document.getElementById('txtDATE_TO').value = document.getElementById('hidDATE_TO_TEXT').value;	
						}
						else
						{
								AddData();
						}

						
																										
					}
				}
		}
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();'>
		<FORM id='CLM_CATASTROPHE_EVENT' method='post' runat='server'>
			<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
				<TR>
					<TD>
						<TABLE width='100%' border='0' align='center'>
							<tr id="trbody" runat="server">
								<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								
											<TD class='midcolora' width='18%'>
												<asp:Label id="capCATASTROPHE_EVENT_TYPE" runat="server">Catastrophe Event Type</asp:Label><span class="mandatory">*</span></TD>
											<TD class='midcolora' width='32%'>
												<asp:DropDownList id='cmbCATASTROPHE_EVENT_TYPE' OnFocus="SelectComboIndex('cmbCATASTROPHE_EVENT_TYPE')"
													runat='server'></asp:DropDownList><br>
												<asp:requiredfieldvalidator id="rfvCATASTROPHE_EVENT_TYPE" runat="server" ControlToValidate="cmbCATASTROPHE_EVENT_TYPE"
													Display="Dynamic"></asp:requiredfieldvalidator>
											</TD>
											<TD class='midcolora' width='18%'>
												<asp:Label id="capCAT_CODE" runat="server">CAT Code</asp:Label></TD>
											<TD class='midcolora' width='32%'>
												<asp:textbox id='txtCAT_CODE' runat='server' size='30' maxlength='20'></asp:textbox><BR>
											</TD>
										</tr>
										<tr>
											<TD class='midcolora' width='18%'>
												<asp:Label id="capDATE_FROM" runat="server">From</asp:Label><span class="mandatory">*</span></TD>
											<TD class='midcolora' width='32%'>
												<asp:textbox id='txtDATE_FROM' runat='server' size='12' maxlength='10'></asp:textbox>
												<asp:hyperlink id="hlkDATE_FROM" runat="server" CssClass="HotSpot"><ASP:IMAGE id="imgDATE_FROM" runat="server" ImageUrl="../../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE></asp:hyperlink>
												<br>
												<asp:regularexpressionvalidator id="revDATE_FROM" runat="server" ControlToValidate="txtDATE_FROM" Display="Dynamic"></asp:regularexpressionvalidator><asp:requiredfieldvalidator id="rfvDATE_FROM" runat="server" ControlToValidate="txtDATE_FROM" Display="Dynamic"></asp:requiredfieldvalidator>
											</TD>
											<TD class='midcolora' width='18%'>
												<asp:Label id="capDATE_TO" runat="server">To</asp:Label><span class="mandatory">*</span></TD>
											<TD class='midcolora' width='32%'>
												<asp:textbox id='txtDATE_TO' runat='server' size='12' maxlength='10'></asp:textbox>
												<asp:hyperlink id="hlkDATE_TO" runat="server" CssClass="HotSpot"><ASP:IMAGE id="imgDATE_TO" runat="server" ImageUrl="../../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE></asp:hyperlink>
												<br>
												<asp:regularexpressionvalidator id="revDATE_TO" runat="server" ControlToValidate="txtDATE_TO" Display="Dynamic"></asp:regularexpressionvalidator><asp:requiredfieldvalidator id="rfvDATE_TO" runat="server" ControlToValidate="txtDATE_TO" Display="Dynamic"></asp:requiredfieldvalidator>
												<asp:customvalidator id="csvDATE_TO" ControlToValidate="txtDATE_TO" Display="Dynamic" ClientValidationFunction="CompareDateFromWithDateTo"
										Runat="server"></asp:customvalidator>
											</TD>
										</tr>
										<tr>
											<TD class='midcolora' width='18%'>
												<asp:Label id="capDESCRIPTION" runat="server">Description</asp:Label><span class="mandatory">*</span></TD>
											<TD class='midcolora' ColSpan='3'>
												<asp:textbox id='txtDESCRIPTION' runat='server' size='70' maxlength='0'></asp:textbox><br>
												<asp:requiredfieldvalidator id="rfvDESCRIPTION" runat="server" ControlToValidate="txtDESCRIPTION" Display="Dynamic"></asp:requiredfieldvalidator>
											</TD>
										</tr>
										<tr>
											<td class='midcolora' colspan='2'>
												<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
												<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" ></cmsb:cmsbutton>
											</td>
											<td class='midcolorr' colspan="2">												
												<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
											</td>
										
							</tr>
							
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidOldData" type="hidden" value="" name="hidOldData" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidCATASTROPHE_EVENT_ID" type="hidden" value="0" name="hidCATASTROPHE_EVENT_ID" runat="server">
			<INPUT id="hidDATE_FROM_TEXT" type="hidden" name="hidDATE_FROM" runat="server">
			<INPUT id="hidDATE_TO_TEXT" type="hidden" name="hidDATE_TO" runat="server">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidCATASTROPHE_EVENT_ID').value, true);	
		</script>
	</BODY>
</HTML>

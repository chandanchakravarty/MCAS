<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddMCCAAttachment.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Claims.AddMCCAAttachment" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Add MCCA Attachment</title>
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
		/*	ChangeColor();
			DisableValidators();
			document.getElementById('hidCATASTROPHE_EVENT_ID').value	=	'New';
			document.getElementById('cmbCATASTROPHE_EVENT_TYPE').focus();
			document.getElementById('cmbCATASTROPHE_EVENT_TYPE').options.selectedIndex = -1;
			document.getElementById('txtDATE_FROM').value  = '';
			document.getElementById('txtDATE_TO').value  = '';
			document.getElementById('txtDESCRIPTION').value  = '';
			document.getElementById('txtCAT_CODE').value  = '';			
			*/
		}
		function CompareLossDateFromWithDateTo(objSource , objArgs)
		{
			var dob=document.CLM_MCCA_ATTACHMENT.txtLOSS_PERIOD_DATE_FROM.value;
			var expdate=document.CLM_MCCA_ATTACHMENT.txtLOSS_PERIOD_DATE_TO.value;
			if (dob != "")
			{
				objArgs.IsValid = CompareTwoDate(expdate,dob,jsaAppDtFormat);
			}
		}			
		
		function CompareDateFromWithDateTo(objSource , objArgs)
		{
			var dob=document.CLM_MCCA_ATTACHMENT.txtPOLICY_PERIOD_DATE_FROM.value;
			var expdate=document.CLM_MCCA_ATTACHMENT.txtPOLICY_PERIOD_DATE_TO.value;
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
		function formReset()
		{
			DisableValidators();
			document.forms[0].reset();			
			if(document.getElementById("hidMCCA_ATTACHMENT_ID").value>0)
				document.getElementById("txtMCCA_ATTACHMENT_POINT").focus();
			else
				document.getElementById("txtPOLICY_PERIOD_DATE_FROM").focus();
			Init();
			return false;
		}					
		function Init()
		{
			document.getElementById("txtMCCA_ATTACHMENT_POINT").value = formatCurrency(document.getElementById("txtMCCA_ATTACHMENT_POINT").value);
			ApplyColor();
			ChangeColor();
		}
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin='0' topMargin='0' onload='Init();'>
		<FORM id='CLM_MCCA_ATTACHMENT' method='post' runat='server'>
			<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
				<TR>
					<TD>
						<TABLE width='100%' border='0' align='center'>
							<tr id="trbody" runat="server">
								<TD class="pageHeader" colSpan="4"><asp:label ID="capMessages" runat="server" Text= "Please note that all fields marked with * are mandatory"></asp:label></TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capPOLICY_PERIOD_DATE_FROM" runat="server"></asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtPOLICY_PERIOD_DATE_FROM" runat='server' size='12' maxlength='10'></asp:textbox>
									<asp:hyperlink id="hlkPOLICY_PERIOD_DATE_FROM" runat="server" CssClass="HotSpot">
										<ASP:IMAGE id="imgPOLICY_PERIOD_DATE_FROM" runat="server" ImageUrl="../../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
									</asp:hyperlink>
									<br>
									<asp:regularexpressionvalidator id="revPOLICY_PERIOD_DATE_FROM" runat="server" ControlToValidate="txtPOLICY_PERIOD_DATE_FROM"
										Display="Dynamic"></asp:regularexpressionvalidator>
									<asp:requiredfieldvalidator id="rfvPOLICY_PERIOD_DATE_FROM" runat="server" ControlToValidate="txtPOLICY_PERIOD_DATE_FROM"
										Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capPOLICY_PERIOD_DATE_TO" runat="server"></asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtPOLICY_PERIOD_DATE_TO" runat='server' size='12' maxlength='10'></asp:textbox>
									<asp:hyperlink id="hlkPOLICY_PERIOD_DATE_TO" runat="server" CssClass="HotSpot">
										<ASP:IMAGE id="imgPOLICY_PERIOD_DATE_TO" runat="server" ImageUrl="../../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
									</asp:hyperlink>
									<br>
									<asp:regularexpressionvalidator id="revPOLICY_PERIOD_DATE_TO" runat="server" ControlToValidate="txtPOLICY_PERIOD_DATE_TO"
										Display="Dynamic"></asp:regularexpressionvalidator>
									<asp:requiredfieldvalidator id="rfvPOLICY_PERIOD_DATE_TO" runat="server" ControlToValidate="txtPOLICY_PERIOD_DATE_TO"
										Display="Dynamic"></asp:requiredfieldvalidator>
									<asp:customvalidator id="csvPOLICY_PERIOD_DATE_TO" ControlToValidate="txtPOLICY_PERIOD_DATE_TO" Display="Dynamic"
										ClientValidationFunction="CompareDateFromWithDateTo" Runat="server"></asp:customvalidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capLOSS_PERIOD_DATE_FROM" runat="server"></asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtLOSS_PERIOD_DATE_FROM" runat='server' size='12' maxlength='10'></asp:textbox>
									<asp:hyperlink id="hlkLOSS_PERIOD_DATE_FROM" runat="server" CssClass="HotSpot">
										<ASP:IMAGE id="imgLOSS_PERIOD_DATE_FROM" runat="server" ImageUrl="../../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
									</asp:hyperlink>
									<br>
									<asp:regularexpressionvalidator id="revLOSS_PERIOD_DATE_FROM" runat="server" ControlToValidate="txtLOSS_PERIOD_DATE_FROM"
										Display="Dynamic"></asp:regularexpressionvalidator>
									<asp:requiredfieldvalidator id="rfvLOSS_PERIOD_DATE_FROM" runat="server" ControlToValidate="txtLOSS_PERIOD_DATE_FROM"
										Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capLOSS_PERIOD_DATE_TO" runat="server"></asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtLOSS_PERIOD_DATE_TO" runat='server' size='12' maxlength='10'></asp:textbox>
									<asp:hyperlink id="hlkLOSS_PERIOD_DATE_TO" runat="server" CssClass="HotSpot">
										<ASP:IMAGE id="Image2" runat="server" ImageUrl="../../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
									</asp:hyperlink>
									<br>
									<asp:regularexpressionvalidator id="revLOSS_PERIOD_DATE_TO" runat="server" ControlToValidate="txtLOSS_PERIOD_DATE_TO"
										Display="Dynamic"></asp:regularexpressionvalidator>
									<asp:requiredfieldvalidator id="rfvLOSS_PERIOD_DATE_TO" runat="server" ControlToValidate="txtLOSS_PERIOD_DATE_TO"
										Display="Dynamic"></asp:requiredfieldvalidator>
									<asp:customvalidator id="csvLOSS_PERIOD_DATE_TO" ControlToValidate="txtLOSS_PERIOD_DATE_TO" Display="Dynamic"
										ClientValidationFunction="CompareLossDateFromWithDateTo" Runat="server"></asp:customvalidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capMCCA_ATTACHMENT_POINT" runat="server"></asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id="txtMCCA_ATTACHMENT_POINT" onBlur="this.value=formatCurrency(this.value);" CssClass="INPUTCURRENCY"  runat='server' size='20' maxlength='8'></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvMCCA_ATTACHMENT_POINT" runat="server" ControlToValidate="txtMCCA_ATTACHMENT_POINT"
										Display="Dynamic"></asp:requiredfieldvalidator>
									<asp:RangeValidator ID="rngMCCA_ATTACHMENT_POINT" MinimumValue="1" MaximumValue="99999999" Type="Currency" Runat="server" Display="Dynamic" ControlToValidate="txtMCCA_ATTACHMENT_POINT"></asp:RangeValidator>
								</TD>
								<TD class='midcolora' width='50%' colspan="2"></TD>
							</tr>
							<tr>
								<td class='midcolora' colspan='2'>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>&nbsp;
									<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text='Activate/ Deactivate'></cmsb:cmsbutton>
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
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> 
			<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidMCCA_ATTACHMENT_ID" type="hidden" value="0" name="hidMCCA_ATTACHMENT_ID" runat="server">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidMCCA_ATTACHMENT_ID').value, true);	
		</script>
	</BODY>
</HTML>

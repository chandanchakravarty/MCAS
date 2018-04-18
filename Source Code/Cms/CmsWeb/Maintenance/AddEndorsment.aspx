<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" validateRequest=false Codebehind="AddEndorsment.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AddEndorsment" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>MNT_ENDORSMENT_DETAILS</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
	
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		
		function setTab()
		{
		/*	if (document.getElementById('hidOldData').value	!= '')
			{	
				Url="EndorsementAttachmentIndex.aspx?"; 
				DrawTab(2,top.frames[1],'Endorsement Attachment',Url);										
			}
			else
			{							
				RemoveTab(2,top.frames[1]);			
			}*/
		}
		

		function ResetTheForm()
		{
			var url;
			if(document.getElementById('hidENDORSMENT_ID').value=='New' || document.getElementById('hidENDORSMENT_ID').value=='NEW')
				url="AddEndorsment.aspx?&transferdata=";
			else
				url="AddEndorsment.aspx?ENDORSMENT_ID=" + document.getElementById('hidENDORSMENT_ID').value + "&transferdata=";		
			window.location.href=url;
			Check();
			return false;	
		}

		function Check()
		{
			if(document.getElementById('cmbENDORS_ASSOC_COVERAGE').options[document.getElementById('cmbENDORS_ASSOC_COVERAGE').selectedIndex].value=='N')
			{
				
				
				document.getElementById('cmbSELECT_COVERAGE').style.display= 'none';
				document.getElementById('capSELECT_COVERAGE').style.display= 'none';
				document.getElementById("spnMandatory").style.display= 'none';
				document.getElementById("rfvSELECT_COVERAGE").setAttribute('isValid',false);
				document.getElementById("rfvSELECT_COVERAGE").style.display='none';
				document.getElementById("rfvSELECT_COVERAGE").setAttribute('enabled',false);
				
			}
			else
			{
				document.getElementById('capSELECT_COVERAGE').style.display= 'inline';
				document.getElementById('cmbSELECT_COVERAGE').style.display= 'inline';
				document.getElementById("rfvSELECT_COVERAGE").setAttribute('enabled',true);
				document.getElementById("rfvSELECT_COVERAGE").setAttribute('isValid',true);	
				document.getElementById("spnMandatory").style.display= "inline";
			}
			
		}
		function ChkTextAreaLength(source, arguments)
		{
			var txtArea = arguments.Value;
			if(txtArea.length > 500 ) 
			{
				arguments.IsValid = false;
				return;   // invalid userName
			}
		}
	
		function CheckEndDate(objSource , objArgs)
		{
			var startdate=document.getElementById('txtEFFECTIVE_FROM_DATE').value;
			var enddate=document.getElementById('txtEFFECTIVE_TO_DATE').value;
			objArgs.IsValid = CompareTwoDate(startdate,enddate,jsaAppDtFormat);
		}	
		
		function CheckDisabledDate(objSource , objArgs)
		{
			var disableddate=document.getElementById('txtDISABLED_DATE').value;
			var enddate=document.getElementById('txtEFFECTIVE_TO_DATE').value;
			objArgs.IsValid = CompareTwoDate(enddate,disableddate,jsaAppDtFormat);
			
		}	
		
		function CompareTwoDate(DateFirst, DateSec, FormatOfComparision)
		{
			if(DateFirst == "" ||DateFirst ==null )
				return false;
			if(DateSec == "" || DateSec==null)
				return true;
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
			if(firstSpan <= secSpan) 
				return true;	// first is less than or equal
			else 
				return false;	// First date is greater
		}
		
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="setTab();Check(); ApplyColor();ChangeColor();">
		<FORM id="MNT_ENDORSMENT_DETAILS" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4"><asp:Label ID=capMessages runat="server"></asp:Label></TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capSTATE_ID" runat="server">State</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSTATE_ID" onfocus="SelectComboIndex('cmbSTATE_ID')" runat="server" Enabled="False">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvSTATE_ID" runat="server" Display="Dynamic" ErrorMessage="STATE_ID can't be blank."
											ControlToValidate="cmbSTATE_ID"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capLOB_ID" runat="server">LOB</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOB_ID" onfocus="SelectComboIndex('cmbLOB_ID')" runat="server" Enabled="False">
											<asp:ListItem Value="0">0</asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvLOB_ID" runat="server" Display="Dynamic" ErrorMessage="LOB_ID can't be blank."
											ControlToValidate="cmbLOB_ID"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPURPOSE" runat="server">Purpose</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPURPOSE" onfocus="SelectComboIndex('cmbPURPOSE')" runat="server">
											<asp:ListItem Value='N'>New Business Request</asp:ListItem>
											<asp:ListItem Value='R'>Renewal Request</asp:ListItem>
											<asp:ListItem Value='B' Selected="True">Both</asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvPURPOSE" runat="server" Display="Dynamic" ErrorMessage="PURPOSE can't be blank."
											ControlToValidate="cmbPURPOSE"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capTYPE" runat="server">Type</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbTYPE" onfocus="SelectComboIndex('cmbTYPE')" runat="server">
											<asp:ListItem Value='M'>Mandatory</asp:ListItem>
											<asp:ListItem Value='O' Selected="True">Optional</asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvTYPE" runat="server" Display="Dynamic" ErrorMessage="TYPE can't be blank."
											ControlToValidate="cmbTYPE"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDESCRIPTION" runat="server">Description</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESCRIPTION" runat="server" maxlength="50" size="52"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvDESCRIPTION" runat="server" Display="Dynamic" ErrorMessage="DESCRIPTION can't be blank."
											ControlToValidate="txtDESCRIPTION"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capENDORSEMENT_CODE" runat="server">Endorsement Code</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtENDORSEMENT_CODE" runat="server" maxlength="50" size="35"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvENDORSEMENT_CODE" runat="server" Display="Dynamic" ErrorMessage="Endorsement Code can't be blank."
											ControlToValidate="txtENDORSEMENT_CODE"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capENDORS_ASSOC_COVERAGE" runat="server">Is this Endorsement associated with coverage</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbENDORS_ASSOC_COVERAGE" runat="server" Enabled="False" onchange="Check()">
											<asp:ListItem Value='N'>No</asp:ListItem>
											<asp:ListItem Value='Y' Selected="True">Yes</asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvENDORS_ASSOC_COVERAGE" runat="server" Display="Dynamic" ErrorMessage="ENDORS_ASSOC_COVERAGE can't be blank."
											ControlToValidate="cmbENDORS_ASSOC_COVERAGE"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capSELECT_COVERAGE" runat="server">Select Coverage</asp:label><SPAN class="mandatory" id="spnMandatory">*</SPAN></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSELECT_COVERAGE" onfocus="SelectComboIndex('cmbSELECT_COVERAGE')" runat="server"
											Enabled="False" Width="275px">
											<asp:ListItem Value=""></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvSELECT_COVERAGE" runat="server" Display="Dynamic" ErrorMessage="SELECT_COVERAGE can't be blank."
											ControlToValidate="cmbSELECT_COVERAGE"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capENDORS_PRINT" runat="server">Would you like to print endorsement</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbENDORS_PRINT" runat="server">
											<asp:ListItem Value='N' Selected="True">No</asp:ListItem>
											<asp:ListItem Value='Y'>Yes</asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvENDORS_PRINT" runat="server" Display="Dynamic" ErrorMessage="ENDORS_PRINT can't be blank."
											ControlToValidate="cmbENDORS_PRINT"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capINCREASED_LIMIT" runat="server">Increased Limit</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbINCREASED_LIMIT" onfocus="SelectComboIndex('cmbINCREASED_LIMIT')" runat="server"
											Enabled="True" Width="50px">
											<asp:ListItem Value=""></asp:ListItem>
										</asp:dropdownlist>
									</TD>
								</tr>
								<%--<TD class="midcolora" width="18%"><asp:Label ID="capEFFECTIVE_FROM_DATE" Runat="server" Visible =False ></asp:Label>
									</TD>
									<td class="midcolora" width="32%">
										<asp:textbox id="txtEFFECTIVE_FROM_DATE" Runat="server" maxlength="10" size="12" Visible=False></asp:textbox>
										<asp:hyperlink id="hlkEFFECTIVE_FROM_DATE" runat="server" CssClass="HotSpot" Visible=False>
											<asp:Image runat="server" ID="imgEFFECTIVE_FROM_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif" Visible=False></asp:Image>
										</asp:hyperlink><br>
										<asp:requiredfieldvalidator id="rfvEFFECTIVE_FROM_DATE" Runat="server" ControlToValidate="txtEFFECTIVE_FROM_DATE"
											Display="Dynamic" Visible=False></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revEFFECTIVE_FROM_DATE" Runat="server" ControlToValidate="txtEFFECTIVE_FROM_DATE"
											Display="Dynamic" Visible=False></asp:regularexpressionvalidator>
									</td>--%>
								<%--<tr>
									<td class="midcolora" width="18%"><asp:label id="capEFFECTIVE_TO_DATE" Runat="server"></asp:label></td>
									<td class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_TO_DATE" Runat="server" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkEFFECTIVE_TO_DATE" runat="server" CssClass="HotSpot">
											<asp:Image runat="server" ID="imgEFFECTIVE_TO_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></asp:Image>
										</asp:hyperlink><br>
										<asp:regularexpressionvalidator id="revEFFECTIVE_TO_DATE" Runat="server" ControlToValidate="txtEFFECTIVE_TO_DATE"
											Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvEFFECTIVE_TO_DATE" Runat="server" ControlToValidate="txtEFFECTIVE_TO_DATE"
											Display="Dynamic" ClientValidationFunction="CheckEndDate"></asp:customvalidator></td>
									<td class="midcolora" width="18%"><asp:label id="capDISABLED_DATE" Runat="server"></asp:label></td>
									<td class="midcolora" width="32%"><asp:textbox id="txtDISABLED_DATE" Runat="server" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkDISABLED_DATE" runat="server" CssClass="HotSpot">
											<asp:Image runat="server" ID="imgDISABLED_DATE" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></asp:Image>
										</asp:hyperlink><br>
										<asp:regularexpressionvalidator id="revDISABLED_DATE" Runat="server" ControlToValidate="txtDISABLED_DATE" Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvDISABLED_DATE" Runat="server" ControlToValidate="txtDISABLED_DATE" Display="Dynamic"
											ClientValidationFunction="CheckDisabledDate"></asp:customvalidator></td>
								</tr>--%>
								<tr style="DISPLAY:none">
									<td class="midcolora" width="18%"><asp:label id="capFORM_NUMBER" visible="False" Runat="server"></asp:label></td>
									<td class="midcolora" width="32%"><asp:textbox id="txtFORM_NUMBER" visible="False" Runat="server" size="35" MaxLength="20"></asp:textbox></td>
									<td class="midcolora" width="18%"><asp:label id="capEDITION_DATE" Visible="False" Runat="server"></asp:label></td>
									<td class="midcolora" width="32%"><asp:textbox id="txtEDITION_DATE" visible="False" Runat="server" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkEDITION_DATE" runat="server" CssClass="HotSpot">
											<asp:Image runat="server" ID="imgEDITION_DATE" visible="False" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></asp:Image>
										</asp:hyperlink><br>
										<asp:regularexpressionvalidator id="revEDITION_DATE" visible="False" Runat="server" ControlToValidate="txtEDITION_DATE"
											Display="Dynamic"></asp:regularexpressionvalidator></td>
								</tr>
					</TD>
				</TR>
				<tr>
					<TD class="midcolora" width="18%">
						<asp:label id="capTEXT" runat="server">Text(Max 500 characters)</asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox onkeypress="MaxLength(this,500);" id="txtTEXT" runat="server" maxlength="500" size="30"
							Height="50" Width="250" TextMode="MultiLine"></asp:textbox><BR>
						<asp:customvalidator id="csvTEXT" Display="Dynamic" ControlToValidate="txtTEXT" Runat="server" ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator></TD>
					<td class="midcolora" width="18%"><asp:label id="capPRINT_ORDER" Runat="server"></asp:label></td>
					<td class="midcolora" width="32%"><asp:textbox id="txtPRINT_ORDER" Runat="server" size="10" MaxLength="5"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revPRINT_ORDER" Runat="server" ControlToValidate="txtPRINT_ORDER"
							ErrorMessage="Please enter integer only." Display="Dynamic"></asp:regularexpressionvalidator></td>
				</tr>
				<tr>
					<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="ActivateDeactivate"></cmsb:cmsbutton></td>
					<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
				</tr>
				<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
				<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
				<INPUT id="hidRootPath" type="hidden" name="hidRootPath" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
				<INPUT id="hidENDORSMENT_ID" type="hidden" name="hidENDORSMENT_ID" runat="server">
			</TABLE>
			</TD></TR></TBODY></TABLE>
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidENDORSMENT_ID').value,true);
			
		</script>
	</BODY>
</HTML>

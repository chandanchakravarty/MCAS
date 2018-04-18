<%@ Page validateRequest=false language="c#" Codebehind="AddExpiryDates.aspx.cs" AutoEventWireup="false" Inherits="Cms.Application.PriorLoss.AddExpiryDates" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//Dtd HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>APP_EXPIRY_DATES</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language='javascript'>
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
			
			function AddData()
			{
			ChangeColor();
			DisableValidators();
			document.getElementById('hidEXPDT_ID').value	=	'New';
			document.getElementById('txtPOLICY_NUMBER').focus();
			document.getElementById('cmbEXPDT_LOB').options.selectedIndex = -1;
			document.getElementById('txtEXPDT_CARR').value  = '';
			document.getElementById('txtEXPDT_DATE').value  = '';
			document.getElementById('txtEXPDT_PREM').value  = '';
			document.getElementById('txtEXPDT_CONT_DATE').value  = '';
			document.getElementById('cmbEXPDT_CSR').options.selectedIndex = -1;
			document.getElementById('cmbEXPDT_PROD').options.selectedIndex = -1;
			document.getElementById('txtPOLICY_NUMBER').value  = '';
			document.getElementById('txtEXPDT_NOTES').value =''; 

			if(document.getElementById('btnActivateDeactivate'))
			document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			}
			
			function populateXML()
			{
				var tempXML;
				tempXML=document.getElementById('hidOldData').value;
				
				if(document.getElementById('hidFormSaved').value == '0')
				{
					if(document.getElementById('hidOldData').value!="")
					{
						//alert(tempXML)
						populateFormData(tempXML,APP_EXPIRY_DATES);
						document.getElementById('txtPOLICY_NUMBER').focus();
					}
					else
					{
						AddData();
					}
				}
				return false;
			}
			
			function ChkTextAreaLength(source, arguments)
			{
				var txtArea = arguments.Value;
				if(txtArea.length > 200 ) 
				{
					arguments.IsValid = false;
					return;   // invalid userName
				}
			}
			function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbEXPDT_LOB":
						lookupMessage	=	"LOBCD.";
						break;
					default:
						lookupMessage	=	"Look up code not found";
						break;
				}
				showLookupLayer(controlId,lookupMessage);							
			}
	
		</script>
	</HEAD>
	<body class="bodyBackGround" leftMargin="0" topMargin="0" onload='populateXML();ApplyColor();'>
		<form id='APP_EXPIRY_DATES' method='post' runat='server'>
			<table cellSpacing='0' cellPadding='0' width='100%' border='0'>
				<tr>
					<td>
						<table width='100%' border='0' align='center'>
							<TBODY>
								<tr>
									<td class="pageHeader" colSpan="4">Please note that all fields marked with * are 
										mandatory</td>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr>
									<td class='midcolora' width="18%">
										<asp:Label id="capPOLICY_NUMBER" runat="server">Policy Number</asp:Label></td>
									<td class='midcolora' width="32%">
										<asp:textbox id='txtPOLICY_NUMBER' runat='server' size='70' maxlength='75'></asp:textbox>
									</td>
									<td class='midcolora' width="18%">
										<asp:Label id="capEXPDT_LOB" runat="server">Line of Business</asp:Label><span class="mandatory">*</span>
									</td>
									<td class='midcolora' width="32%">
										<asp:DropDownList id='cmbEXPDT_LOB' OnFocus="SelectComboIndex('cmbEXPDT_LOB')" runat='server'>
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:DropDownList><a class="calcolora" href="javascript:showPageLookupLayer('cmbEXPDT_LOB')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0" /></a>
										<BR>
										<asp:requiredfieldvalidator id="rfvEXPDT_LOB" runat="server" ControlToValidate="cmbEXPDT_LOB" Display="Dynamic"></asp:requiredfieldvalidator>
									</td>
								</tr>
								<tr>
									<td class='midcolora' width="18%">
										<asp:Label id="capEXPDT_CARR" runat="server">Current Carrier</asp:Label></td>
									<td class='midcolora' width="32%">
										<asp:textbox id='txtEXPDT_CARR' runat='server' width='80' maxlength='10'></asp:textbox>
									</td>
									<td class='midcolora' width="18%">
										<asp:Label id="capEXPDT_DATE" runat="server">Expiration Date</asp:Label><span class="mandatory">*</span></td>
									<td class='midcolora' width="32%">
										<asp:textbox id='txtEXPDT_DATE' runat='server' width='70' maxlength='10'></asp:textbox>
										<asp:hyperlink id="hlkEXPDT_DATE" runat="server" CssClass="HotSpot">
											<asp:image id="imgEXPDT_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
										</asp:hyperlink><br>
										<asp:requiredfieldvalidator id="rfvEXPDT_DATE" Runat="server" ControlToValidate="txtEXPDT_DATE" Display="Dynamic"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revEXPDT_DATE" Runat="server" ControlToValidate="txtEXPDT_DATE" Display="Dynamic"></asp:regularexpressionvalidator>
									</td>
								</tr>
								<tr>
									<td class='midcolora' width="18%">
										<asp:Label id="capEXPDT_PREM" runat="server">Premium</asp:Label></td>
									<td class='midcolora' width="32%">
										<asp:textbox id='txtEXPDT_PREM' CssClass="INPUTCURRENCY" runat='server' width='85' maxlength='8'></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revEXPDT_PREM" Runat="server" Display="Dynamic" Enabled="true" ControlToValidate="txtEXPDT_PREM"></asp:regularexpressionvalidator></td>
					</td>
					<td class='midcolora' width="18%">
						<asp:Label id="capEXPDT_CONT_DATE" runat="server">Date</asp:Label></td>
					<td class='midcolora' width="32%">
						<asp:textbox id='txtEXPDT_CONT_DATE' runat='server' width='70' maxlength='10'></asp:textbox>
						<asp:hyperlink id="hlkEXPDT_CONT_DATE" runat="server" CssClass="HotSpot">
							<asp:image id="Image1" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
						</asp:hyperlink><br>
						<asp:regularexpressionvalidator id="revEXPDT_CONT_DATE" Runat="server" ControlToValidate="txtEXPDT_CONT_DATE" Display="Dynamic"></asp:regularexpressionvalidator>
					</td>
				</tr>
				<tr>
					<td class='midcolora' width="18%">
						<asp:Label id="capEXPDT_PROD" runat="server">Producer Assigned</asp:Label></td>
					<td class='midcolora' width="32%">
						<asp:DropDownList id='cmbEXPDT_PROD' OnFocus="SelectComboIndex('cmbEXPDT_PROD')" runat='server'>
							<asp:ListItem Value='0'></asp:ListItem>
						</asp:DropDownList><BR>
					</td>
					<td class='midcolora' width="18%">
						<asp:Label id="capEXPDT_CSR" runat="server">CSR</asp:Label></td>
					<td class='midcolora' width="32%">
						<asp:DropDownList id='cmbEXPDT_CSR' OnFocus="SelectComboIndex('cmbEXPDT_CSR')" runat='server'>
							<asp:ListItem Value='0'></asp:ListItem>
						</asp:DropDownList><BR>
					</td>
				</tr>
				<tr>
					<td class='midcolora' width="18%">
						<asp:Label id="capEXPDT_NOTES" runat="server">Notes</asp:Label></td>
					<td class='midcolora' ColSpan='3' width="32%">
						<asp:textbox id='txtEXPDT_NOTES' runat='server' Columns="70" Rows="7" maxlength='255' TextMode='MultiLine'></asp:textbox><BR>
						<asp:CustomValidator ID="csvEXPDT_NOTES" Runat="server" ControlToValidate="txtEXPDT_NOTES" Display="Dynamic"
							ClientValidationFunction="ChkTextAreaLength"></asp:CustomValidator>
					</td>
				</tr>
				<tr>
					<td class='midcolora' colspan='2'>
						<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
						&nbsp;
						<cmsb:cmsbutton class="clsButton" id='btnActivateDeactivate' runat="server" Text='Activate/Deactivate'></cmsb:cmsbutton>
					</td>
					<td class='midcolorr' colspan="2">
						<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
					</td>
				</tr>
			</table>
			</TD></TR></TBODY></TABLE> <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<input id="hidOldData" type="hidden" name="hidOldData" runat="server"> <input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			<input id="hidEXPDT_ID" type="hidden" value="0" name="hidEXPDT_ID" runat="server">
		</form>
		</TD></TR></TBODY></TABLE></FORM> 
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none;Z-INDEX: 101;POSITION: absolute">
			<iframe id="lookupLayerIFrame" style="DISPLAY: none;Z-INDEX: 1001;FILTER: alpha(opacity=0);BACKGROUND-COLOR: #000000"
				width="0" height="0" top="0px;" left="0px"></iframe>
		</div>
		<DIV id="lookupLayer" style="Z-INDEX: 101;VISIBILITY: hidden;POSITION: absolute" onmouseover="javascript:refreshLookupLayer();">
			<table class="TabRow" cellspacing="0" cellpadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b>Add LookUp</b></td>
					<td><p align="right"><a href="javascript:void(0)" onclick="javascript:hideLookupLayer();"><img src="/cms/cmsweb/images/cross.gif" border="0" alt="Close" WIDTH="16" HEIGHT="14"></a></p>
					</td>
				</tr>
				<tr class="SubTabRow">
					<td colspan="2">
						<span id="LookUpMsg"></span>
					</td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,"<%=primaryKeyValues%>",true);											
		</script>
	</body>
</HTML>

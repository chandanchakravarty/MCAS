<%@ Page language="c#" Codebehind="PolicyAddWatercraftEngine.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Watercrafts.PolicyAddWatercraftEngine" ValidateRequest="false" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>BRICS-Policy Watercraft Engine Information</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language='javascript'>
function AddData()
{
	if(document.getElementById('hidFormSaved').value != "5")
	document.getElementById('txtENGINE_NO').focus();
	document.getElementById('txtYEAR').value  = '';
	document.getElementById('txtMAKE').value  = '';
	document.getElementById('txtMODEL').value  = '';
	document.getElementById('txtSERIAL_NO').value  = '';
	document.getElementById('txtHORSEPOWER').value  = '';
	document.getElementById('txtINSURING_VALUE').value  = '';
	document.getElementById('cmbFUEL_TYPE').options.selectedIndex = -1;
	document.getElementById('txtOTHER').value  = '';
	
	if(document.getElementById('btnDelete')!=null)
	    document.getElementById('btnDelete').setAttribute('disabled', true);
	if (document.getElementById('btnActivateDeactivate'))
	    document.getElementById('btnActivateDeactivate').setAttribute('disabled', true);


	DisableValidators(); 
	ChangeColor();
}

//Added by Asfa (09-June-2008) - iTrack issue #4071
function InsuringValueMsg()
{
	document.getElementById('txtINSURING_VALUE').value=formatCurrency(document.getElementById('txtINSURING_VALUE').value);	
	if(document.getElementById('txtINSURING_VALUE').value != "")
		alert("Field is for Reference Only. Value should be included on the Watercraft Rating Information Tab, Insuring Value (incl. motor) field.");
}
 
function populateXML()
{		
	if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
	{
		var tempXML;
		tempXML=document.getElementById('hidOldData').value;
		
		if(tempXML!="" && tempXML!=0)
		{			 
			populateFormData(tempXML,APP_WATERCRAFT_ENGINE_INFO);
			document.getElementById('txtINSURING_VALUE').value=formatCurrency(document.getElementById('txtINSURING_VALUE').value);
		}		 
		else
		{
			AddData();
		}		 
	}
	else if(document.getElementById('hidFormSaved')!=null && document.getElementById('hidFormSaved').value == '2')
	{
		//do nothing for updation failed in case of duplication engine no.
	}
	else
	{
		AddData(); 
	}
	return false;
}

function ChkTextAreaLength(source, arguments)
	{
		var txtArea = arguments.Value;
		if(txtArea.length > 100 ) 
		{
			arguments.IsValid = false;
			return;   // invalid userName
		}
	}
		</script>
</HEAD>
	<BODY leftMargin='0' topMargin='0' onload='populateXML();ChangeColor();ApplyColor();'>
		<FORM id='APP_WATERCRAFT_ENGINE_INFO' method='post' runat='server'>
			<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
			
				<tr>
					<td class="midcolorc" align="right" colSpan="4">
					<asp:label id="lblDelete" runat="server" CssClass="errmsg"></asp:label>						
					</td>
				</tr>
				
				<TR>
					<TD id="tdForm" runat="server">
						<TABLE width='100%' border='0' align='center'>
						<tr>
						<TD colSpan="4">
						<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
						</TD>
						</tr>
							<tr>							
								
								<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
									mandatory</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capENGINE_NO" runat="server">Engine Number</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtENGINE_NO' runat='server' size='10' maxlength='20'></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvENGINE_NO" runat="server" ControlToValidate="txtENGINE_NO" ErrorMessage=""
										Display="Dynamic"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revENGINE_NO" runat="server" Display="Dynamic" ErrorMessage="revENGINE_NO"
											ControlToValidate="txtENGINE_NO"></asp:regularexpressionvalidator>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capYEAR" runat="server">Year</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtYEAR' runat='server' size='6' maxlength='4'></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvYEAR" runat="server" ControlToValidate="txtYEAR" ErrorMessage="" Display="Dynamic"></asp:requiredfieldvalidator>
									<asp:RangeValidator ID="rngYEAR" Runat="server" Type="Integer" ControlToValidate="txtYEAR" Display="Dynamic"
										MinimumValue="1950"></asp:RangeValidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capMAKE" runat="server">Make</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtMAKE' runat='server' size='20' maxlength='75'></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvMAKE" runat="server" ControlToValidate="txtMAKE" ErrorMessage="MAKE can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capMODEL" runat="server">Model</asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtMODEL' runat='server' size='20' maxlength='75'></asp:textbox><BR>
									
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capSERIAL_NO" runat="server">Serial Number</asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtSERIAL_NO' runat='server' size='20' maxlength='75'></asp:textbox><br>
									<asp:regularexpressionvalidator id="revSERIAL_NO" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtSERIAL_NO"></asp:regularexpressionvalidator>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capHORSEPOWER" runat="server">Horsepower</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtHORSEPOWER' runat='server' size='7' maxlength='5'></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvHORSEPOWER" runat="server" ControlToValidate="txtHORSEPOWER" ErrorMessage="HORSEPOWER can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revHORSEPOWER" runat="server" ControlToValidate="txtHORSEPOWER" ErrorMessage="RegularExpressionValidator"
										Display="Dynamic"></asp:regularexpressionvalidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capINSURING_VALUE" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtINSURING_VALUE' runat='server' size='18' maxlength='7' CssClass="InputCurrency"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revINSURING_VALUE" runat="server" ControlToValidate="txtINSURING_VALUE" ErrorMessage="RegularExpressionValidator"
										Display="Dynamic"></asp:regularexpressionvalidator>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capASSOCIATED_BOAT" runat="server">Associated Boat</asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id='cmbASSOCIATED_BOAT' OnFocus="SelectComboIndex('cmbASSOCIATED_BOAT')" runat='server'></asp:DropDownList><br>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capFUEL_TYPE" runat="server">Fuel type</asp:Label></TD>
								<TD class='midcolora' width="32%" colspan="3">
									<asp:DropDownList ID="cmbFUEL_TYPE" Runat="server" onfocus="SelectComboIndex('cmbFUEL_TYPE')"></asp:DropDownList>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capOTHER" runat="server">Others</asp:Label></TD>
								<TD class='midcolora' ColSpan='3'>
									<asp:textbox id='txtOTHER' runat='server' TextMode="MultiLine" size='40' Columns="35" Rows="5"></asp:textbox><br>
									<asp:CustomValidator ID="csvOTHER" ControlToValidate="txtOTHER" Runat="server" ClientValidationFunction="ChkTextAreaLength"
										Display="Dynamic"></asp:CustomValidator>
								</TD>
							</tr>
							<tr>
								<td class='midcolora' colspan='2'>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
										&nbsp;
									<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate" CausesValidation="false"></cmsb:cmsbutton>
									
								
								</td>
								<td class='midcolorr' colspan='2'>
									<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" causesValidation="false"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
								</td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
							<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
							<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
							<INPUT id="hidAppID" type="hidden" value="0" name="hidAppID" runat="server"> <INPUT id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
							<INPUT id="hidAppVersionID" type="hidden" value="0" name="hidAppVersionID" runat="server">
							<INPUT id="hidENGINE_ID" type="hidden" value="0" name="hidENGINE_ID" runat="server">
							<INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
							<INPUT id="hidBOATID" type="hidden" value="0" name="hidBOATID" runat="server">
							<INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
							<INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
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
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidENGINE_ID').value);
			//Delete case
			<%if (strDelete == "Y")
			{%>
				document.getElementById('tdForm').style.display='none';
				RefreshWebGrid("5","1",true,true); 
			<%}%>
		</script>
	</BODY>
</HTML>

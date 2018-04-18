<%@ Page language="c#" Codebehind="PolicyAddChimneyStove.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.aspx.HomeOwners.PolicyAddChimneyStove" ValidateRequest="false"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
	<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
function AddData()
{
ChangeColor();
DisableValidators();
document.getElementById('cmbIS_STOVE_VENTED').focus();
document.getElementById('cmbIS_STOVE_VENTED').options.selectedIndex = -1;
document.getElementById('txtOTHER_DEVICES_ATTACHED').value  = '';
document.getElementById('cmbCHIMNEY_CONSTRUCTION').options.selectedIndex = -1;
document.getElementById('txtCONSTRUCT_OTHER_DESC').value  = '';
document.getElementById('cmbIS_TILE_FLUE_LINING').options.selectedIndex = -1;
document.getElementById('cmbIS_CHIMNEY_GROUND_UP').options.selectedIndex = -1;
document.getElementById('cmbCHIMNEY_INST_AFTER_HOUSE_BLT').options.selectedIndex = -1;
document.getElementById('cmbIS_CHIMNEY_COVERED').options.selectedIndex = -1;
document.getElementById('txtTHIMBLE_OR_MATERIAL').value  = '';
document.getElementById('cmbSTOVE_PIPE_IS').options.selectedIndex = -1;
document.getElementById('cmbDOES_SMOKE_PIPE_FIT').options.selectedIndex = -1;
document.getElementById('cmbSMOKE_PIPE_WASTE_HEAT').options.selectedIndex = -1;
document.getElementById('cmbSTOVE_CONN_SECURE').options.selectedIndex = -1;
document.getElementById('cmbSMOKE_PIPE_PASS').options.selectedIndex = -1;
document.getElementById('cmbSELECT_PASS').options.selectedIndex = -1;
document.getElementById('txtPASS_INCHES').value  = '';
document.getElementById('txtDIST_FROM_SMOKE_PIPE').value  = '';
}

function populateXML()
{
	
	if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
	{
		var tempXML;
		if(document.getElementById('hidOldData').value != "")
		{			

			tempXML = document.getElementById('hidOldData').value;
			populateFormData(tempXML,APP_HOME_OWNER_CHIMNEY_STOVE);			
		}
		else
		{
			AddData();
		}
	}

	return false;
}


</script>
<body leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();'>
	<FORM id='APP_HOME_OWNER_CHIMNEY_STOVE' method='post' runat='server'>
		<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
			<tr>
				<TD>
					<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
				</TD>
			</tr>
			<TR>
				<TD>
					<TABLE width='100%' border='0' align='center'>
						<tr>
							<TD class="pageHeader" colspan='4'>Please note that all fields marked with * are mandatory</TD>
						</tr>
						<tr>
							<td class="midcolorc" colspan='4' align="right"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
						</tr>
						<tr>
							<TD class='midcolora' width='32%'>
								<asp:Label id="capIS_STOVE_VENTED" runat="server">Is the stove vented into the same chimney fuel (double vented) with a heating device using a different type of fuel?</asp:Label></TD>
							<TD class='midcolora' width='18%'>
								<asp:DropDownList id='cmbIS_STOVE_VENTED' OnFocus="SelectComboIndex('cmbIS_STOVE_VENTED')" runat='server'>
								 
								</asp:DropDownList>
							</TD>
							<TD class='midcolora' width='32%'>
								<asp:Label id="capOTHER_DEVICES_ATTACHED" runat="server">If yes, list other devices and where each is attached to the chimney</asp:Label></TD>
							<TD class='midcolora' width='18%'>
								<asp:textbox id='txtOTHER_DEVICES_ATTACHED' runat='server' size='30' maxlength='100'></asp:textbox>
							</TD>
						</tr>
						<tr>
							<TD class='midcolora' width='32%'>
								<asp:Label id="capCHIMNEY_CONSTRUCTION" runat="server">Chimney construction </asp:Label></TD>
							<TD class='midcolora' width='18%'>
								<asp:DropDownList id='cmbCHIMNEY_CONSTRUCTION' OnFocus="SelectComboIndex('cmbCHIMNEY_CONSTRUCTION')"
									runat='server'>
								 
								</asp:DropDownList>
							</TD>
							<TD class='midcolora' width='32%'>
								<asp:Label id="capCONSTRUCT_OTHER_DESC" runat="server">Other construction description </asp:Label></TD>
							<TD class='midcolora' width='18%'>
								<asp:textbox id='txtCONSTRUCT_OTHER_DESC' runat='server' size='30' maxlength='100'></asp:textbox>
							</TD>
						</tr>
						<tr>
							<TD class='midcolora' width='32%'>
								<asp:Label id="capIS_TILE_FLUE_LINING" runat="server">If masonry: does tile flue lining extend from below the stovepipe entry point to the top of the chimney? </asp:Label></TD>
							<TD class='midcolora' width='18%'>
								<asp:DropDownList id='cmbIS_TILE_FLUE_LINING' OnFocus="SelectComboIndex('cmbIS_TILE_FLUE_LINING')"
									runat='server'>
									 
								</asp:DropDownList>
							</TD>
							<TD class='midcolora' width='32%'>
								<asp:Label id="capIS_CHIMNEY_GROUND_UP" runat="server">If masonry: is the chimney built from the ground up? </asp:Label></TD>
							<TD class='midcolora' width='18%'>
								<asp:DropDownList id='cmbIS_CHIMNEY_GROUND_UP' OnFocus="SelectComboIndex('cmbIS_CHIMNEY_GROUND_UP')"
									runat='server'>
									 
								</asp:DropDownList>
							</TD>
						</tr>
						<tr>
							<TD class='midcolora' width='32%'>
								<asp:Label id="capCHIMNEY_INST_AFTER_HOUSE_BLT" runat="server">Was the chimney installed after the house was built and for this solid fuel heating device?</asp:Label></TD>
							<TD class='midcolora' width='18%'>
								<asp:DropDownList id='cmbCHIMNEY_INST_AFTER_HOUSE_BLT' OnFocus="SelectComboIndex('cmbCHIMNEY_INST_AFTER_HOUSE_BLT')"
									runat='server'>
									 
								</asp:DropDownList>
							</TD>
							<TD class='midcolora' width='32%'>
								<asp:Label id="capIS_CHIMNEY_COVERED" runat="server">Is chimney "covered with" or "hidden behind" a combustible wall?</asp:Label></TD>
							<TD class='midcolora' width='18%'>
								<asp:DropDownList id='cmbIS_CHIMNEY_COVERED' OnFocus="SelectComboIndex('cmbIS_CHIMNEY_COVERED')" runat='server'>
								 
								</asp:DropDownList>
							</TD>
						</tr>
						<tr>
							<TD class='midcolora' width='32%'>
								<asp:Label id="capDIST_FROM_SMOKE_PIPE" runat="server">Give distance from smoke pipe to edges of opening in that wall or cover</asp:Label></TD>
							<TD class='midcolora' width='18%'>
								<asp:textbox id='txtDIST_FROM_SMOKE_PIPE' runat='server' size='2' maxlength='2'></asp:textbox><br>
								<asp:regularexpressionvalidator id="revDIST_FROM_SMOKE_PIPE" runat="server" ControlToValidate="txtDIST_FROM_SMOKE_PIPE" Display="Dynamic"></asp:regularexpressionvalidator>
							</TD>
							<TD class='midcolora' width='32%'>
								<asp:Label id="capTHIMBLE_OR_MATERIAL" runat="server">Describe any thimble or material placed to protect the edges of that opening</asp:Label></TD>
							<TD class='midcolora' width='18%'>
								<asp:textbox id='txtTHIMBLE_OR_MATERIAL' runat='server' size='30' maxlength='100'></asp:textbox>
							</TD>
						</tr>
						<tr>
							<TD class='midcolora' width='32%'>
								<asp:Label id="capSTOVE_PIPE_IS" runat="server">Stove pipe is</asp:Label></TD>
							<TD class='midcolora' width='18%'>
								<asp:DropDownList id='cmbSTOVE_PIPE_IS' OnFocus="SelectComboIndex('cmbSTOVE_PIPE_IS')" runat='server'>
									 
								</asp:DropDownList>
							</TD>
							<TD class='midcolora' width='32%'>
								<asp:Label id="capDOES_SMOKE_PIPE_FIT" runat="server">Does the smoke pipe fit snugly into the chimney opening?</asp:Label></TD>
							<TD class='midcolora' width='18%'>
								<asp:DropDownList id='cmbDOES_SMOKE_PIPE_FIT' OnFocus="SelectComboIndex('cmbDOES_SMOKE_PIPE_FIT')"
									runat='server'>									 
								</asp:DropDownList>
							</TD>
						</tr>
						<tr>
							<TD class='midcolora' width='32%'>
								<asp:Label id="capSMOKE_PIPE_WASTE_HEAT" runat="server">Does the smoke pipe have a "waste heat collector/circulator", "heat reclaimer", "catalytic converter", "heat extractor" or "circulating fan"?</asp:Label></TD>
							<TD class='midcolora' width='18%'>
								<asp:DropDownList id='cmbSMOKE_PIPE_WASTE_HEAT' OnFocus="SelectComboIndex('cmbSMOKE_PIPE_WASTE_HEAT')"
									runat='server'>								 
								</asp:DropDownList>
							</TD>
							<TD class='midcolora' width='32%'>
								<asp:Label id="capSTOVE_CONN_SECURE" runat="server">Are stovepipe connections securely fastened to each other with screws at each connection?</asp:Label></TD>
							<TD class='midcolora' width='18%'>
								<asp:DropDownList id='cmbSTOVE_CONN_SECURE' OnFocus="SelectComboIndex('cmbSTOVE_CONN_SECURE')" runat='server'>
									
								</asp:DropDownList>
							</TD>
						</tr>
						<tr>
							<TD class='midcolora' width='32%'>
								<asp:Label id="capSMOKE_PIPE_PASS" runat="server">Does the smoke pipe pass through any interior combustible wall, ceiling, closet or concealed are?</asp:Label></TD>
							<TD class='midcolora' width='18%'>
								<asp:DropDownList id='cmbSMOKE_PIPE_PASS' OnFocus="SelectComboIndex('cmbSMOKE_PIPE_PASS')" runat='server'>
								 
								</asp:DropDownList>
							</TD>
							<TD class='midcolora' width='32%'>
								<asp:Label id="capSELECT_PASS" runat="server">Select one of the following </asp:Label></TD>
							<TD class='midcolora' width='18%'>
								<asp:DropDownList id='cmbSELECT_PASS' OnFocus="SelectComboIndex('cmbSELECT_PASS')" runat='server'>
								 
								</asp:DropDownList>
							</TD>
						</tr>
						<tr>
							<TD class='midcolora' width='32%'>
								<asp:Label id="capPASS_INCHES" runat="server">Inches Details</asp:Label>
							</TD>
							<TD class='midcolora' width='18%'>
								<asp:textbox id='txtPASS_INCHES' runat='server' size='10' maxlength='4'></asp:textbox><br>
								<asp:regularexpressionvalidator id="revPASS_INCHES" runat="server" ControlToValidate="txtPASS_INCHES" Display="Dynamic"></asp:regularexpressionvalidator>
							</TD>
							<TD class='midcolora' width='18%' colspan="2">
							 
						</tr>
						<tr>
							<td class='midcolora' colspan='2'  width='50%'>
								<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>							
							</td>
							<td class='midcolorr'  colspan='2' width='50%'>
								<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
							</td>
						</tr>
						<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
						<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
						<INPUT id="hidOldData" type="hidden"  name="hidOldData" runat="server">
						<INPUT id="hidCUSTOMER_ID" type="hidden" name="hidFUEL_ID" runat="server">
						<INPUT id="hidAPP_ID" type="hidden" name="hidFUEL_ID" runat="server">
						<INPUT id="hidAPP_VERSION_ID" type="hidden" name="hidFUEL_ID" runat="server">
						<INPUT id="hidPOLICY_ID" type="hidden" name="hidPOLICY_ID" runat="server">
						<INPUT id="hidPOLICY_VERSION_ID" type="hidden" name="hidPOLICY_VERSION_ID" runat="server">
						<INPUT id="hidFUEL_ID" type="hidden" value="0" name="hidFUEL_ID" runat="server">
					</TABLE>
				</TD>
			</TR>
		</TABLE>
	</FORM>
	<script>
//RefreshWindowsGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidFUEL_ID').value);
	</script>
</BODY>

<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PolicyAddGeneralLocation.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.GeneralLiability.PolicyAddGeneralLocation" validateRequest="false" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>POL_LOCATIONS</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language='javascript'>
		//var varLOB;
		function TerritoryRule()
		{
			if(document.getElementById('cmbLOC_STATE').selectedIndex<0) return;
			
			/*Lookup values for Territory
			UnqID	Text
			11673	01
			11674	02
			11675	03
			11676	04
			11677	05
			11678	06
			Territory values state wise
			14-Indiana = 01, 02, 04, 06
			22-Michigan = 01, 03, 04 05 
			*/
			document.getElementById('cmbLOC_TERRITORY').length=0;			
			if(document.getElementById('cmbLOC_STATE').options[document.getElementById('cmbLOC_STATE').selectedIndex].value=="14")
			{
				AddComboOption("cmbLOC_TERRITORY",'11673','01');
				AddComboOption("cmbLOC_TERRITORY",'11674','02');
				AddComboOption("cmbLOC_TERRITORY",'11676','04');
				AddComboOption("cmbLOC_TERRITORY",'11678','06');
			}
			else if(document.getElementById('cmbLOC_STATE').options[document.getElementById('cmbLOC_STATE').selectedIndex].value=="22")
			{
				AddComboOption("cmbLOC_TERRITORY",'11673','01');
				AddComboOption("cmbLOC_TERRITORY",'11675','03');
				AddComboOption("cmbLOC_TERRITORY",'11676','04');
				AddComboOption("cmbLOC_TERRITORY",'11677','05');
			}
			else
			{
				AddComboOption("cmbLOC_TERRITORY",'11673','01');
				AddComboOption("cmbLOC_TERRITORY",'11674','02');								
				AddComboOption("cmbLOC_TERRITORY",'11675','03');
				AddComboOption("cmbLOC_TERRITORY",'11676','04');
				AddComboOption("cmbLOC_TERRITORY",'11677','05');
				AddComboOption("cmbLOC_TERRITORY",'11678','06');
			}
			
			if(document.getElementById('hidLOC_TERRITORY').value!="")
			{
				for(i=0;i<document.getElementById('cmbLOC_TERRITORY').options.length;i++)
				{
					if(document.getElementById('cmbLOC_TERRITORY').options[i].value==document.getElementById('hidLOC_TERRITORY').value)
					{
						document.getElementById('cmbLOC_TERRITORY').options[i].selected = true;
						//document.getElementById('hidLOC_TERRITORY').value="";
						return false;
					}
				}
			}
			else
				document.getElementById('cmbLOC_TERRITORY').selectedIndex=-1;
			
			return false;
		}
		
		function GetCounty()
		{
			document.getElementById('hidSubmitZip').value = document.getElementById('txtLOC_ZIP').value;
			
			if ( document.getElementById('hidSubmitZip').value == '' ) 
			{
				return false;
			}
			
			var regEx = document.all["revLOC_ZIP"].validationexpression;
				
			if ( document.getElementById('hidSubmitZip').value.search(regEx) == -1 )
			{
				return false;
			}
				
			//DisableValidators();
			//document.POL_LOCATIONS.submit();
			//__doPostBack('hidSubmitZip','');
		}
		function AddData()
		{		
			DisableValidators();
			//document.getElementById('hidLOCATION_ID').value	=	'New';
			document.getElementById('txtLOC_NUM').focus();
			//document.getElementById('txtLOC_NUM').value  = '';
			document.getElementById('txtLOC_ADD1').value  = '';
			document.getElementById('txtLOC_ADD2').value  = '';
			document.getElementById('txtLOC_CITY').value  = '';
			//document.getElementById('txtLOC_COUNTY').value  = '';
			document.getElementById('cmbLOC_TERRITORY').options.selectedIndex = -1;
			document.getElementById('cmbLOC_STATE').options.selectedIndex = -1;
			document.getElementById('txtLOC_ZIP').value  = '';
			document.getElementById('cmbLOC_COUNTRY').options.selectedIndex = 0;
			//document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			ChangeColor();
		}
		function populateXML()
		{
			
			if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
			{				
				//if ( document.getElementById('hidSubmitZip').value == '' )
				//{
				if(document.getElementById('hidOldData').value != "")
				{
					//alert(document.getElementById('hidOldData').value);
					populateFormData(document.getElementById('hidOldData').value, POL_LOCATIONS);
					//varLOB = document.getElementById('hidPOL_LOB').value;
					
				}
				else
				{	
					AddData();
					//varLOB = document.getElementById('hidPOL_LOB').value;
				}
			//}
			
			return false;
			}
		}
		</script>
</HEAD>
	<BODY leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();TerritoryRule();'>
		<FORM id='POL_LOCATIONS' method='post' runat='server'>
			<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
				<TR>
					<TD>
						<TABLE width='100%' border='0' align='center'>
							<tr>
								<TD class="pageHeader" colSpan="4">
									<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
									Please note that all fields marked with * are mandatory
								</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capLOC_NUM" runat="server">Num</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtLOC_NUM' runat='server' size='6' maxlength='4'></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvLOC_NUM" runat="server" ControlToValidate="txtLOC_NUM" ErrorMessage="LOC_NUM can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revLOC_NUM" Enabled="False" runat="server" ControlToValidate="txtLOC_NUM" ErrorMessage="RegularExpressionValidator"
										Display="Dynamic"></asp:regularexpressionvalidator>
									<asp:rangevalidator id="rngLOC_NUM" runat="server" Display="Dynamic" ControlToValidate="txtLOC_NUM"
										Type="Integer" MinimumValue="1" MaximumValue="9999"></asp:rangevalidator>
								</TD>
								<TD class='midcolora' ColSpan='2'></TD>
							</tr>
							<tr>
								<td class="midcolora">
									<asp:label id="lblPull" runat="server">Would you like to pull customer address</asp:label>
								</td>
								<td class="midcolora" colSpan="3">
									<cmsb:cmsbutton class="clsButton" id="btnPullCustomerAddress" runat="server" Text="Pull Customer Address"></cmsb:cmsbutton>
								</td>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capLOC_ADD1" runat="server">Add1</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtLOC_ADD1' runat='server' size='35' maxlength='75'></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvLOC_ADD1" runat="server" ControlToValidate="txtLOC_ADD1" ErrorMessage="LOC_ADD1 can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capLOC_ADD2" runat="server">Add2</asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtLOC_ADD2' runat='server' size='35' maxlength='75'></asp:textbox><BR>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capLOC_CITY" runat="server">City</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtLOC_CITY' runat='server' size='30' maxlength='75'></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvLOC_CITY" runat="server" ControlToValidate="txtLOC_CITY" ErrorMessage="LOC_CITY can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
									<asp:RegularExpressionValidator id="revLOC_CITY" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtLOC_CITY"></asp:RegularExpressionValidator>
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="capLOC_COUNTRY" Runat="server">Country</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOC_COUNTRY" onfocus="SelectComboIndex('cmbLOC_COUNTRY')" runat="server">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvLOC_COUNTRY" runat="server" ControlToValidate="cmbLOC_COUNTRY" Display="Dynamic"
										ErrorMessage="LOC_COUNTRY can't be blank."></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capLOC_STATE" runat="server">State</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id='cmbLOC_STATE' OnFocus="SelectComboIndex('cmbLOC_STATE')" runat='server'>
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvLOC_STATE" runat="server" ControlToValidate="cmbLOC_STATE" ErrorMessage="LOC_STATE can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>							
								<TD class='midcolora' width='18%'>
									<asp:Label id="capLOC_ZIP" runat="server">Zip</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtLOC_ZIP' runat='server' size='13' maxlength='10'></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvLOC_ZIP" runat="server" ControlToValidate="txtLOC_ZIP" ErrorMessage="LOC_ZIP can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
									<asp:RegularExpressionValidator id="revLOC_ZIP" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtLOC_ZIP"></asp:RegularExpressionValidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capTERRITORY" runat="server">Territory</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id="cmbLOC_TERRITORY" OnFocus="SelectComboIndex('cmbLOC_TERRITORY')" runat='server'></asp:DropDownList><br>
									<asp:requiredfieldvalidator id="rfvLOC_TERRITORY" runat="server" ControlToValidate="cmbLOC_TERRITORY" ErrorMessage="Territory can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' colspan='2'></TD>
							</tr>
							<tr>
								<td class='midcolora' colspan='1'>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
								</td>
								<td class='midcolorr' colspan="3">
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
								</td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
							<INPUT id="hid" type="hidden" value="0" name="hid" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
							<!--<INPUT id="hidLOCATION_ID" type="hidden" name="hidLOCATION_ID" runat="server">--> <INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
							<input id="hidSubmitZip" type="hidden" runat="server" NAME="hidSubmitZip"> <!--<INPUT id="hidPOL_LOB" type="hidden" value="0" name="hidPOL_LOB" runat="server">-->
							<input id="hidLOC_TERRITORY" type="hidden" runat="server" NAME="hidLOC_TERRITORY">
							<input id="hidCustomerID" type="hidden" runat="server" NAME="hidCustomerID"> <input id="hidPolicyID" type="hidden" runat="server" NAME="hidPolicyID">
							<input id="hidPolicyVersionID" type="hidden" runat="server" NAME="hidPolicyVersionID">
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
		<script>
//RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidLOCATION_ID').value,true);
		</script>
	</BODY>
</HTML>

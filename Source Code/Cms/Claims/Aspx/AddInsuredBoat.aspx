<%@ Page language="c#" Codebehind="AddInsuredBoat.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddInsuredBoat" validateRequest=false  %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>MNT_AGENCY_LIST</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript">
		
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
			
			function FetchPolicyBoat()
			{
				combo = document.getElementById("cmbPOLICY_BOAT");				
				//Structure of the encoded string is as follows : 
				/* BOAT_ID^YEAR^MAKE^MODEL^LENGTH^WATERCRAFT_HORSE_POWER^SERIAL_NUMBER^STATE^BODY_TYPE */  
				//We will split the user_id field to obtain the relavent information
				if(combo.selectedIndex==-1 || combo.selectedIndex==0) return;
				encoded_string = new String(combo.options[combo.selectedIndex].value);
				
				if(encoded_string.length<1) return;
				array = encoded_string.split('^');
				//Traverse through the array and put the values in relavent fields			
//				
				//document.getElementById("hidLOCATION").value = array[0];
				//Add the boat id to policy boat id field
				document.getElementById("hidPOLICY_BOAT_ID").value = array[0];
				document.getElementById("txtYEAR").value = array[1];
				document.getElementById("txtMAKE").value = array[2];
				document.getElementById("txtMODEL").value = array[3];
				document.getElementById("txtLENGTH").value = array[4];
				document.getElementById("txtHORSE_POWER").value = array[5];
				document.getElementById("txtSERIAL_NUMBER").value = array[6];
				SelectComboOption('cmbSTATE',array[7]);
				SelectComboOption('cmbBODY_TYPE',array[8]);
				
				
				ChangeColor();
			}		
			
			function ResetTheForm()
			{
				DisableValidators();
				document.CLM_INSURED_BOAT.reset();
				//FetchPolicyBoat();
				Init();
				return false;
			}
			function Init()
			{
				ChangeColor();
				ApplyColor();
				if(document.getElementById("hidBOAT_ID").value!="" && document.getElementById("hidBOAT_ID").value!="0")
					document.getElementById("cmbPOLICY_BOAT").disabled = true;
				//if(document.getElementById('txtSERIAL_NUMBER')!=null)
				//	document.getElementById('txtSERIAL_NUMBER').focus();
			}
			
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="CLM_INSURED_BOAT" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
										mandatory</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr id="trPOLICY_BOAT" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capPOLICY_BOAT" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPOLICY_BOAT" onfocus="SelectComboIndex('cmbPOLICY_BOAT')" runat="server" AutoPostBack="True"></asp:dropdownlist>
									</TD>			
									<td colspan="2" class="midcolora"></td>						
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capSERIAL_NUMBER" runat="server"></asp:label><!--<span class="mandatory">*</span>--></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtSERIAL_NUMBER" runat="server" size="40" maxlength="50"></asp:textbox><BR>
										<%--<asp:requiredfieldvalidator id="rfvSERIAL_NUMBER" runat="server" ControlToValidate="txtSERIAL_NUMBER" Display="Dynamic"></asp:requiredfieldvalidator>--%></TD>
									<TD class="midcolora" width="18%"><asp:label id="capYEAR" runat="server"></asp:label><!--<span class="mandatory">*</span>--></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtYEAR" runat="server" size="6" maxlength="4"></asp:textbox><BR>
										<asp:rangevalidator id="rngYEAR" Display="Dynamic" ControlToValidate="txtYEAR" MinimumValue="1900" Runat="server"
											Type="Integer"></asp:rangevalidator>
										<%--<asp:requiredfieldvalidator id="rfvYEAR" runat="server" ControlToValidate="txtYEAR" Display="Dynamic"></asp:requiredfieldvalidator>--%></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMAKE" runat="server"></asp:label><!--<span class="mandatory">*</span>--></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMAKE" runat="server" size="40" maxlength="50"></asp:textbox><br>
										<%--<asp:requiredfieldvalidator id="rfvMAKE" runat="server" ControlToValidate="txtMAKE" Display="Dynamic" ErrorMessage="Please select txtMAKE."></asp:requiredfieldvalidator>--%>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capMODEL" runat="server"></asp:label><!--<span class="mandatory">*</span>--></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMODEL" runat="server" size="40" maxlength="50"></asp:textbox><br>
										<%--<asp:requiredfieldvalidator id="rfvMODEL" runat="server" ControlToValidate="txtMODEL" Display="Dynamic" ErrorMessage="Please select txtMAKE."></asp:requiredfieldvalidator>--%>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capBODY_TYPE" runat="server"></asp:label><!--<span class="mandatory">*</span>--></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbBODY_TYPE" onfocus="SelectComboIndex('cmbBODY_TYPE')" runat="server"></asp:dropdownlist><br>
										<%--<asp:requiredfieldvalidator id="rfvBODY_TYPE" runat="server" Display="Dynamic" ControlToValidate="cmbBODY_TYPE"></asp:requiredfieldvalidator>--%>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capLENGTH" runat="server"></asp:label><!--<span class="mandatory">*</span>--></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtLENGTH" runat="server" maxlength="10" size="15"></asp:textbox><BR>
										<%--<asp:requiredfieldvalidator id="rfvLENGTH" runat="server" Display="Dynamic" ControlToValidate="txtLENGTH"></asp:requiredfieldvalidator>--%></TD>
								</tr>
								<TR>
									<TD class="midcolora" width="18%"><asp:label id="capWEIGHT" runat="server"></asp:label><!--<span class="mandatory">*</span>--></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtWEIGHT" runat="server" maxlength="10" size="15"></asp:textbox><br>
										<%--<asp:requiredfieldvalidator id="rfvWEIGHT" runat="server" ControlToValidate="txtWEIGHT" Display="Dynamic" ErrorMessage="Please select txtWEIGHT."></asp:requiredfieldvalidator>--%>
										<asp:rangevalidator id="rngWEIGHT" Display="Dynamic" ControlToValidate="txtWEIGHT" MinimumValue="1" MaximumValue="9999999999" Runat="server"
											Type="Double" ></asp:rangevalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capHORSE_POWER" runat="server"></asp:label><!--<span class="mandatory">*</span>--></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtHORSE_POWER" runat="server" maxlength="5" size="7"></asp:textbox><br>
										<asp:RegularExpressionValidator ID="revHORSE_POWER" ControlToValidate="txtHORSE_POWER" Runat="server" Display="Dynamic"></asp:RegularExpressionValidator>
										<asp:rangevalidator id="rngHORSE_POWER" Display="Dynamic" ControlToValidate="txtHORSE_POWER" MinimumValue="0" Enabled="False"
											MaximumValue="9999" Runat="server" Type="Integer"></asp:rangevalidator>
										<%--<asp:requiredfieldvalidator id="rfvHORSE_POWER" runat="server" ControlToValidate="txtHORSE_POWER" Display="Dynamic"
											ErrorMessage="Please select txtWEIGHT."></asp:requiredfieldvalidator>--%>
									</TD>
								</TR>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capOTHER_HULL_TYPE" runat="server"></asp:label><!--<span class="mandatory">*</span>--></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtOTHER_HULL_TYPE" runat="server" maxlength="50" size="40"></asp:textbox><br>
										<%--<asp:requiredfieldvalidator id="rfvOTHER_HULL_TYPE" runat="server" ControlToValidate="txtOTHER_HULL_TYPE" Display="Dynamic"
											ErrorMessage="Please select txtOTHER_HULL_TYPE."></asp:requiredfieldvalidator>--%>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capPLATE_NUMBER" runat="server"></asp:label><!--<span class="mandatory">*</span>--></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPLATE_NUMBER" runat="server" maxlength="10" size="15"></asp:textbox><br>
										<%--<asp:requiredfieldvalidator id="rfvPLATE_NUMBER" runat="server" ControlToValidate="txtPLATE_NUMBER" Display="Dynamic"
											ErrorMessage="Please select txtPLATE_NUMBER."></asp:requiredfieldvalidator>--%>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capSTATE" runat="server"></asp:label><!--<span class="mandatory">*</span>-->  </TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSTATE" onfocus="SelectComboIndex('cmbSTATE')" runat="server"></asp:dropdownlist><br>
										<%--<asp:requiredfieldvalidator id="rfvSTATE" runat="server" ControlToValidate="cmbSTATE" Display="Dynamic" ErrorMessage="Please select STATE."></asp:requiredfieldvalidator>--%></TD>
									<TD class="midcolora" width="18%"><asp:label id="capWHERE_BOAT_SEEN" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtWHERE_BOAT_SEEN" runat="server" size="40" maxlength="50"></asp:textbox></TD>
									
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capIncludeTrailer" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbIncludeTrailer" runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvIncludeTrailer" runat="server" ControlToValidate="cmbIncludeTrailer" Display="Dynamic" ErrorMessage="Please select Include Trailer."></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"></TD>
									<TD class="midcolora" width="32%"></TD>
									
								</tr>
								<tr>
									<td class='midcolora' colspan="2"><%--Done for Itrack Issue 5833 on 20 July 2009--%>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text='ActivateDeactivate'></cmsb:cmsbutton>
								</td>
								<td class='midcolorr' colspan="2">
									<cmsb:cmsbutton class="clsButton" id='btnDelete' runat="server" Text='Delete'></cmsb:cmsbutton><%--Added for Itrack Issue 5833 on 20 July 2009--%>
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>				
								</td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			<INPUT id="hidLOB_ID" type="hidden" value="" name="hidLOB_ID" runat="server">
			<INPUT id="hidPOLICY_BOAT_ID" type="hidden" value="0" name="hidPOLICY_BOAT_ID" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidBOAT_ID" type="hidden" value="0" name="hidBOAT_ID" runat="server">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidBOAT_ID').value,true);			
		</script>
	</BODY>
</HTML>

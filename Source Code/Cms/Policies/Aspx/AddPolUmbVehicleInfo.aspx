<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddPolUmbVehicleInfo.aspx.cs"  validateRequest="false" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Umbrella.AddPolUmbVehicleInfo" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>POL_UMBRELLA_VEHICLE_INFO</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">		
		function AddData()
		{				
			DisableValidators();
			document.getElementById('txtVEHICLE_YEAR').value  = '';
			document.getElementById('txtMAKE').value  = '';
			document.getElementById('txtMODEL').value  = '';
			document.getElementById('txtREGN_PLATE_NUMBER').value  = '';
			//document.getElementById('cmbMOTORCYCLE_TYPE').selectedIndex = -1;
			document.getElementById('cmbOTHER_POLICY').options.selectedIndex = -1;
			document.getElementById('cmbUSE_VEHICLE').selectedIndex = -1;
			document.getElementById('cmbIS_EXCLUDED').selectedIndex = -1;
			//if(document.getElementById('btnDelete'))
			//	document.getElementById('btnDelete').setAttribute('disabled',true); 
			if(document.getElementById('btnActivateDeactivate'))	
				document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			document.getElementById('cmbMOTORCYCLE_TYPE').focus();
		}
		
						
		function populateXML()
		{	
			if(document.getElementById('hidFormSaved').value == '0')
			{
				var tempXML;
				if(document.getElementById("hidOldData").value!="")
				{
					tempXML=document.getElementById("hidOldData").value;
					if(document.getElementById('btnActivateDeactivate')!=null)				
						document.getElementById('btnActivateDeactivate').setAttribute('disabled',true); 
					populateFormData(tempXML,POL_UMBRELLA_VEHICLE_INFO);						
				}
				else
				{
					AddData();
					ChangeColor();
				}			
			}		
			return false;
		}

		function Init()
		{
			populateXML();
			cmbMOTORCYCLE_TYPE_Change();
			ApplyColor();
			ChangeColor()
		}
		function cmbMOTORCYCLE_TYPE_Change()
		{
			combo = document.getElementById("cmbMOTORCYCLE_TYPE");
			
			if(combo!=null && combo.selectedIndex!=-1 && combo.options[combo.selectedIndex].value==document.getElementById("hidUMB_VEHICLE_TYPE_OTHER").value) //Other Business
			{
				document.getElementById("txtREGN_PLATE_NUMBER").style.display = "inline";
				document.getElementById("spnREGN_PLATE_NUMBER").style.display = "inline";
				document.getElementById("capREGN_PLATE_NUMBER").style.display = "inline";
				EnableValidator("rfvREGN_PLATE_NUMBER",true);
			}
			else
			{
				document.getElementById("txtREGN_PLATE_NUMBER").style.display = "none";
				document.getElementById("spnREGN_PLATE_NUMBER").style.display = "none";
				document.getElementById("capREGN_PLATE_NUMBER").style.display = "none";
				EnableValidator("rfvREGN_PLATE_NUMBER",false);
			}
			ChangeColor();
		}
		
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="POL_UMBRELLA_VEHICLE_INFO" method="post" runat="server">
			<DIV id="myTSMain1" style="BEHAVIOR: url(/cms/cmsweb/htc/webservice.htc)"></DIV>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow>Please 
									note that all fields marked with * are mandatory
								</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capVEHICLE_NUMBER" runat="server"> Vehicle #.</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtVEHICLE_NUMBER" runat="server" size="30" maxlength="10"></asp:textbox><BR>
								</TD>
								<td colspan="2" class="midcolora"></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capMOTORCYCLE_TYPE" runat="server">Motorcycle Type</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbMOTORCYCLE_TYPE" onfocus="SelectComboIndex('cmbMOTORCYCLE_TYPE')" runat="server"
										Visible="true" AutoPostBack =True></asp:dropdownlist>
									<BR>
									<asp:requiredfieldvalidator id="rfvMOTORCYCLE_TYPE" runat="server" Display="Dynamic" ErrorMessage="Select Motorcycle type."
										ControlToValidate="cmbMOTORCYCLE_TYPE"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capREGN_PLATE_NUMBER" runat="server"></asp:label><span id="spnREGN_PLATE_NUMBER" class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtREGN_PLATE_NUMBER" runat="server" maxlength="50" size="40"></asp:textbox><br>
								<asp:requiredfieldvalidator id="rfvREGN_PLATE_NUMBER" runat="server" Display="Dynamic" 
										ControlToValidate="txtREGN_PLATE_NUMBER"></asp:requiredfieldvalidator>
								</td>		
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capUSE_VEHICLE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbUSE_VEHICLE" runat="server" onfocus="SelectComboIndex('cmbUSE_VEHICLE')"></asp:dropdownlist><br>
									</TD>
								<td colspan="2" class="midcolora"></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capVEHICLE_YEAR" runat="server">Year</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtVEHICLE_YEAR" runat="server" maxlength="4" size="6"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvVEHICLE_YEAR" runat="server" Display="Dynamic" ErrorMessage="VEHICLE_YEAR can't be blank."
										ControlToValidate="txtVEHICLE_YEAR"></asp:requiredfieldvalidator><asp:rangevalidator id="rngVEHICLE_YEAR" Display="Dynamic" ControlToValidate="txtVEHICLE_YEAR" MinimumValue="1950"
										Runat="server" Type="Integer"></asp:rangevalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capMAKE" runat="server">Make of Vehicle</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtMAKE" runat="server"
										maxlength="28" size="20"></asp:textbox>
									<BR>
									<asp:requiredfieldvalidator id="rfvMAKE" runat="server" Display="Dynamic" ErrorMessage="MAKE can't be blank."
										ControlToValidate="txtMAKE"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capMODEL" runat="server">Model of Vehicle</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtMODEL" runat="server" maxlength="28" size="20"></asp:textbox>
									<BR>
									<asp:requiredfieldvalidator id="rfvMODEL" runat="server" Display="Dynamic" ErrorMessage="MODEL can't be blank."
										ControlToValidate="txtMODEL"></asp:requiredfieldvalidator></TD>
								<td class="midcolora" width="18%"><asp:label id="capIS_EXCLUDED" Runat="server"></asp:label></td>
								<td class="midcolora" width="32%"><asp:dropdownlist id="cmbIS_EXCLUDED" onfocus="SelectComboIndex('cmbIS_EXCLUDED')" Runat="server"></asp:dropdownlist></td>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capOTHER_POLICY" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%" colspan ="3"><asp:dropdownlist id="cmbOTHER_POLICY" onfocus="SelectComboIndex('cmbOTHER_POLICY')" runat="server">										
									</asp:dropdownlist></td>
							</tr>
							<tr>
							
								<td class="midcolora" align="left" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" causesvalidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnCopy" runat="server" Text="Copy" causesvalidation="false"
										visible="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"
										causesValidation="false"></cmsb:cmsbutton></td>
								<td class="midcolorr" colSpan="2"><%--<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton>&nbsp;--%>
									<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			<INPUT id="hidVEHICLE_ID" type="hidden" value="0" name="hidVEHICLE_ID" runat="server">
			<INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server"> <INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">			
			<INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
			<INPUT id="hidCustomInfo" type="hidden" value="0" name="hidCustomInfo" runat="server">
			<INPUT id="hidUMB_VEHICLE_TYPE_OTHER" type="hidden" value="" name="hidUMB_VEHICLE_TYPE_OTHER" runat="server">
		</FORM>
		<!-- For lookup layer ends here-->
		<script>
			if (document.getElementById("hidFormSaved").value == "3")
			 {
				/*Record deleted*/
				/*Refreshing the grid and coverting the form into add mode*/
				/*Using the javascript*/
				RefreshWebGrid("1","1",false,"1");
				RemoveTab(4,top.frames[1]);	
				RemoveTab(3,top.frames[1]);	
				RemoveTab(2,top.frames[1]);
				//RefreshWebGrid("1","1"); 
				document.getElementById('hidVEHICLE_ID').value = "NEW";				
		    }
		    else if (document.getElementById("hidFormSaved").value == "1")
			{
				this.parent.strSelectedRecordXML = "-1";
				RefreshWebGrid(document.getElementById('hidFormSaved').value,TrimTheString(document.getElementById('hidVEHICLE_ID').value),true);
				
			}
		</script>
		</SCRIPT>
	</BODY>
</HTML>

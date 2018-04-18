<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" Codebehind="AddPolicyVehicle.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Aviation.AddPolicyVehicle" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>VEHICLES</title>
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
			var GlobalError=false;
			//webcontrol:workflow id="myWorkFlow" runat="server"/webcontrol:workflow
		
		function AddData()			
		{	
			document.getElementById('hidVehicleID').value	=	'NEW';
			//document.getElementById('txtVIN').focus();
			document.getElementById('txtVEHICLE_YEAR').value  = '';
			//document.getElementById('txtMAKE').value  = '';
			//document.getElementById('cmbANTI_LOCK_BRAKES').options.selectedIndex = -1;
			//if(document.getElementById('cmbUNDERINS_MOTOR_INJURY_COVE')!=null)
			ChangeColor();
			//DisableValidators();
		}
		function populateXML()
		{	
			//varLOB = document.getElementById('hidAPP_LOB').value;
			var tempXML;
			
			if(document.getElementById('btnDelete'))	
				document.getElementById('btnDelete').setAttribute('disabled',true); 
			
			if(document.getElementById('btnActivateDeactivate')!=null)				
				document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
					  	
				if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
				{	 
					if(document.getElementById('hidOldData')!=null)
					{
						tempXML=document.getElementById('hidOldData').value;
						//alert(tempXML);
						if(tempXML!="" && tempXML!=0)
						{	
							populateFormData(tempXML,POL_AVIATION_VEHICLES);								
						}
						else
						{
								AddData();
						}
					}
						
					else
					{
							
						varLOB = document.getElementById('hidAPP_LOB').value;
					}
				}
				//populateInfo();
				setTab();
				return false;
		}
		function EnableDisableButtons()
		{
			if (document.getElementById('hidOldData').value != "" && document.getElementById('hidOldData').value != "0")
			{
				if(document.getElementById('btnDelete') != null)	
					document.getElementById('btnDelete').setAttribute('disabled',false); 
					if(document.getElementById('btnActivateDeactivate') != null)	
					document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
				//SetActivateDeactivateButton(document.getElementById('hidOldData').value);
			}
			
		}
		function setTab()
		{
		   //alert(document.getElementById('hidOldData').value);
		   if(document.getElementById('hidOldData').value	!= '')
		   {
 				if (document.getElementById('hidVehicleID')!=null && document.getElementById('hidVehicleID').value!="NEW")
				{
					var CalledFrom ='';
					if (document.getElementById('hidCalledFrom')!=null)
						CalledFrom=document.getElementById('hidCalledFrom').value;
					var CustomerID='';
					if (document.getElementById('hidCustomerID')!=null)
						CustomerID=document.getElementById('hidCustomerID').value;
					var AppID='';
					if (document.getElementById('hidAPPID')!=null)
						AppID=document.getElementById('hidAPPID').value;
					var AppVersionID='';
					if (document.getElementById('hidAppVersionID')!=null)
						AppVersionID=document.getElementById('hidAppVersionID').value;
					var VehicleID='';
					if (document.getElementById('hidVehicleID')!=null)
						VehicleID=document.getElementById('hidVehicleID').value;
					//if(document.getElementById('hidAPP_LOB').value!=null)
					LOB_ID="8"; //document.getElementById('hidAPP_LOB').value;
					 
						Url="../../../Policies/aspx/aviation/PolicyVehicleCoverageDetails.aspx?CalledFrom="+CalledFrom+"&pageTitle=Aviation Vehicle Coverages" + "&VEHICLEID=" + VehicleID  + "&LOB_ID="+LOB_ID+"&" ; 
						DrawTab(2,top.frames[1],'Cover Type Details',Url); 
						/*
						Url="../../application/aspx/Endorsements.aspx?CalledFrom="+CalledFrom+"&pageTitle=Vehicle Coverages" + "&VEHICLEID=" + VehicleID  + "&LOB_ID="+LOB_ID+"&" ; 
						DrawTab(3,top.frames[1],'Endorsements',Url); 					
						
						Url="../Aspx/AdditionalInterestIndex.aspx?CalledFrom="+CalledFrom+"&CUSTOMER_ID="+CustomerID+"&APP_ID="+AppID+"&APP_VERSION_ID="+AppVersionID +"&VEHICLE_ID="+VehicleID+"&"; 
						DrawTab(4,top.frames[1],'Additional Interest',Url); 
						*/
				}
				else
				{
					
					//RemoveTab(4,top.frames[1]);	
					//RemoveTab(3,top.frames[1]);	
					RemoveTab(2,top.frames[1]);	
				}
			}
			else
			{				
				//RemoveTab(4,top.frames[1]);	
				//RemoveTab(3,top.frames[1]);	
				RemoveTab(2,top.frames[1]);	
				this.parent.strSelectedRecordXML='';
			}
			return false; 
		}
			function Init()
			{
			populateXML();
			EnableDisableButtons();
			ApplyColor();
			ChangeColor();
			}		
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="POL_AVIATION_VEHICLES" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD colSpan="4">
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4">
										Please note that all fields marked with * are mandatory
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capUSE_VEHICLE" runat="server">Vehicle Use</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbUSE_VEHICLE" runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvUSE_VEHICLE" runat="server" ControlToValidate="cmbUSE_VEHICLE" ErrorMessage="Please select vehicle use."
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" colSpan="2"></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCOVG_PERIMETER" runat="server">Coverage Perimeter</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCOVG_PERIMETER" Runat="server" Width="250"></asp:dropdownlist></TD>
									<TD class="midcolora" colSpan="2"></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capREG_NUMBER" runat="server">Registration Number</asp:label><span class="mandatory" id="spnREG_NUMBER">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtREG_NUMBER" runat="server" size="15" maxlength="8"></asp:textbox><asp:requiredfieldvalidator id="rfvREG_NUMBER" runat="server" ControlToValidate="txtREG_NUMBER" ErrorMessage="Please enter Registration Number"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capSERIAL_NUMBER" runat="server">Serial Number</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtSERIAL_NUMBER" runat="server" size="20" maxlength="15"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capVEHICLE_YEAR" runat="server">Year</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtVEHICLE_YEAR" runat="server" size="4" maxlength="4"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvVEHICLE_YEAR" runat="server" ControlToValidate="txtVEHICLE_YEAR" ErrorMessage="VEHICLE_YEAR can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator><asp:rangevalidator id="rngVEHICLE_YEAR" ControlToValidate="txtVEHICLE_YEAR" Display="Dynamic" Runat="server"
											Type="Integer" MinimumValue="1950" MaximumValue="2010" ErrorMessage="Please Enter valid year"></asp:rangevalidator></TD>
									<TD class="midcolora" colSpan="2"></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMAKE" runat="server">Make of Vehicle</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbMAKE" runat="server" Width="250"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvMAKE" runat="server" ControlToValidate="cmbMAKE" ErrorMessage="MAKE can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capMAKE_OTHER" runat="server">Other Make</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMAKE_OTHER" runat="server" size="25" maxlength="20"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMODEL" runat="server">Model of Vehicle</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbMODEL" runat="server" Width="250"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvMODEL" runat="server" ControlToValidate="cmbMODEL" ErrorMessage="MODEL can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capMODEL_OTHER" runat="server">Other Model</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMODEL_OTHER" runat="server" size="25" maxlength="15"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCERTIFICATION" runat="server">Aircraft Certification</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCERTIFICATION" runat="server" maxlength="10" size="25"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvCERTIFICATION" runat="server" Display="Dynamic" ErrorMessage="Aircraft Certification can't be blank."
											ControlToValidate="txtCERTIFICATION"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capREGISTER" runat="server">Aerodome Register</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtREGISTER" runat="server" maxlength="10" size="25"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capENGINE_TYPE" runat="server">Engine Type</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbENGINE_TYPE" runat="server" Width="250"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvENGINE_TYPE" runat="server" Display="Dynamic" ErrorMessage="Engine type can't be blank."
											ControlToValidate="cmbENGINE_TYPE"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capWING_TYPE" runat="server">Wing Type</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbWING_TYPE" runat="server" Width="250"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvWING_TYPE" runat="server" Display="Dynamic" ErrorMessage="wing type can't be blank."
											ControlToValidate="cmbWING_TYPE"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCREW" runat="server">Crew</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCREW" runat="server" maxlength="10" size="10"></asp:textbox></TD>
									<TD class="midcolora" width="18%"><asp:label id="capPAX" runat="server">Pax</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPAX" runat="server" maxlength="10" size="10"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capREMARKS" runat="server">Remarks</asp:label></TD>
									<TD class="midcolora" colspan="3"><asp:textbox id="txtREMARKS" runat="server" Columns="65" Rows="5" TextMode="MultiLine" maxlength="500"></asp:textbox></TD>
								</tr>
								<tr>
								</tr>
								<tr>
									<td class="midcolora" align="left" colSpan="2">
										<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" CausesValidation="false"
											Text="Activate/Deactivate"></cmsb:cmsbutton>
									</td>
									<td class="midcolorr" colSpan="2">
										<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" causesvalidation="false"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
									</td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
			<INPUT id="hidVehicleID" type="hidden" value="0" name="hidVehicleID" runat="server">
			<INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			<INPUT id="hidPolVersionID" type="hidden" value="0" name="hidPolVersionID" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			<INPUT id="hidAPP_EFFECTIVE_YEAR" type="hidden" name="hidAPP_EFFECTIVE_YEAR" runat="server">
			<INPUT id="hidAPP_LOB" type="hidden" name="hidAPP_LOB" runat="server">
		</FORM>
		<script>
		if (document.getElementById("hidFormSaved").value == "5")
		{
				
			RefreshWebGrid("1","1",false,"1"); 
			/*Record deleted*/
			/*Refreshing the grid and coverting the form into add mode*/
			/*Using the javascript*/
			RemoveTab(2,top.frames[1]);	
			document.getElementById('hidVehicleID').value = "NEW";
		  }
		  else if(document.getElementById("hidFormSaved").value == "1")
		  {
			this.parent.strSelectedRecordXML = "-1";
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidVehicleID').value,true);	
		  }
		</script>
	</BODY>
</HTML>

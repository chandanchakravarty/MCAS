<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page validateRequest="false" CodeBehind="PolicyAddRecrVehInfo.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Umbrella.PolicyAddRecrVehInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddUmrellaRecrVehIndex</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		
			function EnableValidator(ValidatorID,flag)
			{
				if(flag) //Enable the validator
				{
					document.getElementById(ValidatorID).setAttribute('enabled',true);
					document.getElementById(ValidatorID).setAttribute('isValid',true);
				}
				else
				{
					document.getElementById(ValidatorID).setAttribute('enabled',false);
					document.getElementById(ValidatorID).style.display = "none";
				}
			}
			function cmbVEH_LIC_ROAD_Change(CALLED_FROM_SAVE)
			{
				combo = document.getElementById("cmbVEH_LIC_ROAD");
				if(CALLED_FROM_SAVE)
					Page_ClientValidate();
					
				if(combo==null || combo.selectedIndex==-1)
					return true;			
				
				if(combo.options[combo.selectedIndex].value=='1')
				{	
					alert(document.getElementById("hidMessage").value);
					Page_IsValid = false;
					return false;
				}
				else				
					return Page_IsValid;					
			}
			function cmbREC_VEH_TYPE_Change()
			{
				combo = document.getElementById("cmbREC_VEH_TYPE");
				
				if(combo!=null && combo.selectedIndex!=-1 && combo.options[combo.selectedIndex].text=='Other')
				{
					document.getElementById('txtREC_VEH_TYPE_DESC').style.display="inline";
					document.getElementById('capREC_VEH_TYPE_DESC').style.display="inline";
					document.getElementById('spnREC_VEH_TYPE_DESC').style.display="inline";
					EnableValidator("rfvREC_VEH_TYPE_DESC",true);
				}
				else
				{
					document.getElementById('txtREC_VEH_TYPE_DESC').style.display="none";
					document.getElementById('capREC_VEH_TYPE_DESC').style.display="none";
					document.getElementById('spnREC_VEH_TYPE_DESC').style.display="none";
					EnableValidator("rfvREC_VEH_TYPE_DESC",false);
				}
				return false;
			}		
			
		
			function cmbVEHICLE_MODIFIED_Change()
			{ 
				combo = document.getElementById("cmbVEHICLE_MODIFIED");
				if(combo==null)
					return;
	
				//if(combo.selectedIndex!=-1 && (combo.options[combo.selectedIndex].value=='Y' || combo.options[combo.selectedIndex].value=='y'))
				if(combo.selectedIndex!=-1 && combo.selectedIndex==2)
				{
					document.getElementById("txtVEHICLE_MODIFIED_DETAILS").style.display="inline";
					document.getElementById("capVEHICLE_MODIFIED_DETAILS").style.display="inline";
					document.getElementById("spnVEHICLE_MODIFIED_DETAILS").style.display="inline";					
					EnableValidator("rfvVEHICLE_MODIFIED_DETAILS",true);
				}
				else
				{	
				
				
					document.getElementById("txtVEHICLE_MODIFIED_DETAILS").style.display="none";
					document.getElementById("capVEHICLE_MODIFIED_DETAILS").style.display="none";
					document.getElementById("spnVEHICLE_MODIFIED_DETAILS").style.display="none";					
					EnableValidator("rfvVEHICLE_MODIFIED_DETAILS",false);
				}
				ChangeColor();
			}
			
			function Refresh(formSaved,rowID)
			{
				//alert('refresh');
				RefreshWebGrid(formSaved,rowID);
				
				//document.getElementById("lblMessage").style.display = "inline";
				//document.getElementById("lblMessage").innerHTML = "Information copied successfully.";
			}
			
			function OpenPopupWindow(Url)
			{
				var myUrl = Url;
				window.open(myUrl,'','toolbar=no, directories=no, location=no, status=yes, menubar=no, resizable=yes, scrollbars=no,  width=500, height=400'); 
				return false;		
			}
			
			function OpenLookupWindow(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Arg1,Arg2,Arg3,Arg4)
			{
				//window.showModalDialog(Url);
				
				//alert(Url);
				//alert(DataTextFieldID);
				//alert(DataValueFieldID);
				//alert(Url + '?DataTextFieldID=' + DataTextFieldID + '&DataValueFieldID=' + DataValueFieldID, 'review','height=500, width=500,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no');
				
				var win = window.open(Url + '?DataValueField=' + DataValueField + '&DataTextField=' + 
									DataTextField + '&DataValueFieldID=' + DataValueFieldID + 
									'&DataTextFieldID=' + DataTextFieldID + '&LookupCode=' + LookupCode + 
									'&Arg1=' + Arg1 + '&Arg2=' + Arg2 + '&Arg3=' + Arg3 + '&Arg4=' + Arg4,
									'review','height=500, width=500,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no' );
				
				//win.document.title = Title;
				
			}
			
			function ResetForm()
			{  
				//alert(document.POL_UMBRELLA_RECREATIONAL_VEHICLES.hidOldData.value);
				DisableValidators();
				if ( document.POL_UMBRELLA_RECREATIONAL_VEHICLES.hidOldData.value == '' )
				{
					AddData();
				}
				else				
					populateFormData(document.POL_UMBRELLA_RECREATIONAL_VEHICLES.hidOldData.value,POL_UMBRELLA_RECREATIONAL_VEHICLES);
					
				cmbVEHICLE_MODIFIED_Change();
				cmbUSED_IN_RACE_SPEED_Change();
				cmbREC_VEH_TYPE_Change();
				ApplyColor();
				ChangeColor();
				
				return false;
			}
			
			function checkFutureYear(source, arguments)
			{
				var dateEntered = arguments.Value;
				var curDate=new Date();
			
				if(!isNaN(dateEntered))
				{
					if(parseInt(dateEntered)>parseInt(curDate.getFullYear()))
					{
						arguments.IsValid = false;
						return false;
					}
				}				
			}
			
			function AddData()
			{
			
				DisableValidators();
				document.getElementById('hidREC_VEH_ID').value	=	'0';
				document.getElementById('txtCOMPANY_ID_NUMBER').focus();
				//document.getElementById('txtCOMPANY_ID_NUMBER').value  = '';
				document.getElementById('txtYEAR').value  = '';
				document.getElementById('txtMAKE').value  = '';
				document.getElementById('txtMODEL').value  = '';
				document.getElementById('txtSERIAL').value  = '';
				document.getElementById('cmbSTATE_REGISTERED').options.selectedIndex = -1;
				document.getElementById('txtVEHICLE_TYPE_NAME').value  = '';
				document.getElementById('hidLOOKUP_UNIQUE_ID').value  = '';
				document.getElementById('txtMANUFACTURER_DESC').value  = '';
				document.getElementById('txtHORSE_POWER').value  = '';
				//document.getElementById('txtDISPLACEMENT').value  = '';
				document.getElementById('txtREMARKS').value  = '';
				document.getElementById('cmbUSED_IN_RACE_SPEED').options.selectedIndex = -1;
				//document.getElementById('cmbPRIOR_LOSSES').options.selectedIndex = -1;
				//document.getElementById('cmbIS_UNIT_REG_IN_OTHER_STATE').options.selectedIndex = -1;
				//document.getElementById('cmbRISK_DECL_BY_OTHER_COMP').options.selectedIndex = -1;
				//document.getElementById('txtDESC_RISK_DECL_BY_OTHER_COMP').value  = '';
				document.getElementById('cmbVEHICLE_MODIFIED').options.selectedIndex = -1;
				//document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
				document.getElementById('cmbUSED_IN_RACE_SPEED').options.selectedIndex = -1;
				document.getElementById('cmbVEH_LIC_ROAD').options.selectedIndex = -1;
				document.getElementById('cmbREC_VEH_TYPE').options.selectedIndex = -1;
				document.getElementById('txtREC_VEH_TYPE_DESC').value  = '';
				document.getElementById('txtUSED_IN_RACE_SPEED_CONTEST').value  = '';
				document.getElementById('cmbIS_BOAT_EXCLUDED').options.selectedIndex = -1;
				
				ApplyColor();
				ChangeColor();
			}
			
		/*	function SetTab()
			{
				if ( document.POL_UMBRELLA_RECREATIONAL_VEHICLES.hidOldData.value != '' )
				{
					
					//Url="CoveragesInfo.aspx?CalledFrom=UREC&PageTitle=Recreational Vehicle Coverages&VEHICLEID=" + document.POL_UMBRELLA_RECREATIONAL_VEHICLES.hidREC_VEH_ID.value + "&";
					//Sumit Chhabra: 22-12-2005:Put the page under construction as it may change
					Url="../../../cms/cmsweb/construction.html";//CoveragesInfo.aspx?CalledFrom=UREC&PageTitle=Recreational Vehicle Coverages&VEHICLEID=" + document.POL_UMBRELLA_RECREATIONAL_VEHICLES.hidREC_VEH_ID.value + "&";
					DrawTab(2,top.frames[1],'Coverages',Url);				
					
					Url="UmbrellaRecrVehRemarks.aspx?RECVEHID=" + document.POL_UMBRELLA_RECREATIONAL_VEHICLES.hidREC_VEH_ID.value + "&";
					DrawTab(3,top.frames[1],'Remarks',Url);				
				}
				else
				{
					RemoveTab(3,top.frames[1]);	
					RemoveTab(2,top.frames[1]);
					
				}
				
			}*/
			
			function ValidateMaxLength(objSource, objArgs)
			{
				if (document.getElementById(objSource.controltovalidate).value.length > 100)
				{
					objArgs.IsValid = false;
				}
				else
				{
					objArgs.IsValid = true;
				}
			}
			function cmbUSED_IN_RACE_SPEED_Change()
			{
				combo = document.getElementById("cmbUSED_IN_RACE_SPEED");
				if(combo==null)
					return;
				
				if(combo.selectedIndex!=-1 && combo.options[combo.selectedIndex].value=='1')
				{
					document.getElementById("txtUSED_IN_RACE_SPEED_CONTEST").style.display="inline";
					document.getElementById("capUSED_IN_RACE_SPEED_CONTEST").style.display="inline";
					document.getElementById("spnUSED_IN_RACE_SPEED_CONTEST").style.display="inline";					
					EnableValidator("rfvUSED_IN_RACE_SPEED_CONTEST",true);
				}
				else
				{
					document.getElementById("txtUSED_IN_RACE_SPEED_CONTEST").style.display="none";
					document.getElementById("capUSED_IN_RACE_SPEED_CONTEST").style.display="none";
					document.getElementById("spnUSED_IN_RACE_SPEED_CONTEST").style.display="none";					
					EnableValidator("rfvUSED_IN_RACE_SPEED_CONTEST",false);
				}
				ChangeColor();
			}			
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="ResetForm();">
		<FORM id="POL_UMBRELLA_RECREATIONAL_VEHICLES" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr id="trMessage" runat="server">
					<td class="midcolorc"><asp:label id="lblInfo" CssClass="errmsg" Runat="server"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
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
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_ID_NUMBER" runat="server"> RV #.</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOMPANY_ID_NUMBER" runat="server" size="30" maxlength="10"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvCOMPANY_ID_NUMBER" runat="server" ControlToValidate="txtCOMPANY_ID_NUMBER"
										ErrorMessage="COMPANY_ID # can't be blank." Display="Dynamic"></asp:requiredfieldvalidator><asp:rangevalidator id="rngCOMPANY_ID_NUMBER" runat="server" ControlToValidate="txtCOMPANY_ID_NUMBER"
										ErrorMessage="Value should be between 1 and 10000" Display="Dynamic" MinimumValue="1" MaximumValue="2147483647" Type="Integer"></asp:rangevalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capYEAR" runat="server">Year</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtYEAR" runat="server" size="5" maxlength="4"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvYEAR" runat="server" ControlToValidate="txtYEAR" ErrorMessage="YEAR can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator><asp:rangevalidator id="rngYEAR" runat="server" ControlToValidate="txtYEAR" Display="Dynamic" MinimumValue="1900"
										Type="Integer"></asp:rangevalidator><br>
									<asp:RegularExpressionValidator id="revYEAR" Enabled="False" runat="server" Display="Dynamic" ControlToValidate="txtYEAR"></asp:RegularExpressionValidator>
									<asp:CustomValidator id="csvYEAR" runat="server" Enabled="False" Display="Dynamic" ControlToValidate="txtYEAR"
										ClientValidationFunction="checkFutureYear"></asp:CustomValidator>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capMAKE" runat="server">Make</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtMAKE" runat="server" size="30" maxlength="30"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvMAKE" runat="server" ControlToValidate="txtMAKE" ErrorMessage="MAKE can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capMODEL" runat="server">Model</asp:label>
									<span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtMODEL" runat="server" size="30" maxlength="30"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvMODEL" runat="server" ControlToValidate="txtMODEL" ErrorMessage="Please enter the type"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSERIAL" runat="server">Serial #</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSERIAL" runat="server" size="30" maxlength="30"></asp:textbox></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSTATE_REGISTERED" runat="server">Registered State</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSTATE_REGISTERED" onfocus="SelectComboIndex('cmbSTATE_REGISTERED')" runat="server"></asp:dropdownlist></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%" style="DISPLAY:none"><asp:label id="capVEHICLE_TYPE" runat="server">Type</asp:label>
									<span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%" style="DISPLAY:none"><asp:textbox id="txtVEHICLE_TYPE_NAME" runat="server" size="40" maxlength="150" ReadOnly="True"></asp:textbox><a href="#"><IMG id="imgSelect" style="CURSOR: hand" alt="" src="../../../cmsweb/images/selecticon.gif"
											runat="server" border="0"></a><br>
									<asp:requiredfieldvalidator id="rfvVEHICLE_TYPE_NAME" runat="server" ControlToValidate="txtVEHICLE_TYPE_NAME"
										ErrorMessage="Please enter the type" Display="Dynamic" Enabled="False"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="19%"><asp:label id="capMANUFACTURER_DESC" runat="server">Description</asp:label>
									<%--<span class="mandatory">*</span>--%>
								</TD>
								<TD class="midcolora" width="32%" colspan="3"><asp:textbox id="txtMANUFACTURER_DESC" runat="server" size="30" maxlength="200"></asp:textbox><br>
									<%--<asp:requiredfieldvalidator id="rfvMANUFACTURER_DESC" runat="server" ControlToValidate="txtMANUFACTURER_DESC"
										ErrorMessage="Please enter the type" Display="Dynamic"></asp:requiredfieldvalidator>--%>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capHORSE_POWER" runat="server">Power</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtHORSE_POWER" runat="server" size="12" maxlength="10"></asp:textbox></TD>
								<%--<TD class="midcolora" width="18%"><asp:label id="capDISPLACEMENT" runat="server">Displacement</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtDISPLACEMENT" runat="server" size="12" maxlength="10"></asp:textbox></TD>
							</tr>
							<tr>--%>
								<TD class="midcolora" width="18%"><asp:label id="capREMARKS" runat="server">Remarks</asp:label></TD>
								<TD class="midcolora" width="32%">
									<asp:textbox id="txtREMARKS" runat="server" size="30" maxlength="100" OnKeyPress="javascript:MaxLength(this,100);"
										Columns="34" Rows="4" TextMode="MultiLine"></asp:textbox>
									<br>
									<asp:CustomValidator id="csvREMARKS" Runat="server" ControlToValidate="txtREMARKS" ClientValidationFunction="ValidateMaxLength"
										Display="Dynamic" ErrorMessage="Please enter only 100 chars."></asp:CustomValidator>
								</TD>
								<%--<td class="midcolora" colSpan="2"></td>--%>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capREC_VEH_TYPE" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREC_VEH_TYPE" onfocus="SelectComboIndex('cmbREC_VEH_TYPE')" runat="server"></asp:dropdownlist>
									<asp:requiredfieldvalidator id="rfvREC_VEH_TYPE" runat="server" ControlToValidate="cmbREC_VEH_TYPE" ErrorMessage="Please select Type of Recreational Vehicle"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capREC_VEH_TYPE_DESC" runat="server"></asp:label><span id="spnREC_VEH_TYPE_DESC" class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtREC_VEH_TYPE_DESC" runat="server" size="30" maxlength="25"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvREC_VEH_TYPE_DESC" runat="server" ControlToValidate="txtREC_VEH_TYPE_DESC"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capVEH_LIC_ROAD" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbVEH_LIC_ROAD" onfocus="SelectComboIndex('cmbVEH_LIC_ROAD')" runat="server"></asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvVEH_LIC_ROAD" runat="server" ControlToValidate="cmbVEH_LIC_ROAD" Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="capIS_BOAT_EXCLUDED" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbIS_BOAT_EXCLUDED" onfocus="SelectComboIndex('cmbIS_BOAT_EXCLUDED')" runat="server"></asp:dropdownlist></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capOTHER_POLICY" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbOTHER_POLICY" onfocus="SelectComboIndex('cmbOTHER_POLICY')" runat="server"></asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"><asp:label id="capC44" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbC44" onfocus="SelectComboIndex('cmbC44')" runat="server"></asp:dropdownlist></TD>
							</tr>
							<tr>
								<TD class="headerEffectSystemParams" colSpan="4" style="HEIGHT: 7px">Underwriting 
									Info</TD>
							</tr>
							<%--<tr>
								<TD class="midcolora" width="18%"><asp:label id="capPRIOR_LOSSES" runat="server">Have there been any prior losses either with the vehicles or any other Recreational vehicle?</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPRIOR_LOSSES" onfocus="SelectComboIndex('cmbPRIOR_LOSSES')" runat="server">
										<asp:ListItem></asp:ListItem>
										<asp:ListItem Value="Y">Yes</asp:ListItem>
										<asp:ListItem Value="N">No</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"><asp:label id="capIS_UNIT_REG_IN_OTHER_STATE" runat="server">Is any unit registered in a state other than Michigan or Indiana?</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbIS_UNIT_REG_IN_OTHER_STATE" onfocus="SelectComboIndex('cmbIS_UNIT_REG_IN_OTHER_STATE')"
										runat="server">
										<asp:ListItem></asp:ListItem>
										<asp:ListItem Value="Y">Yes</asp:ListItem>
										<asp:ListItem Value="N">No</asp:ListItem>
									</asp:dropdownlist></TD>
							</tr>--%>
							<%--<tr>
								<TD class="midcolora" width="18%"><asp:label id="capRISK_DECL_BY_OTHER_COMP" runat="server">Has this risk been cancelled, declined or non-renewed by another company?</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbRISK_DECL_BY_OTHER_COMP" onfocus="SelectComboIndex('cmbRISK_DECL_BY_OTHER_COMP')"
										runat="server">
										<asp:ListItem></asp:ListItem>
										<asp:ListItem Value="Y">Yes</asp:ListItem>
										<asp:ListItem Value="N">No</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"><asp:label id="capDESC_RISK_DECL_BY_OTHER_COMP" runat="server">If yes, please explain</asp:label></TD>
								<TD class="midcolora" width="32%">
									<asp:textbox id="txtDESC_RISK_DECL_BY_OTHER_COMP" runat="server" size="30" maxlength="100" Rows="3"
										Columns="40" TextMode="MultiLine"></asp:textbox>
									<br>
									<asp:CustomValidator id="csvDESC_RISK_DECL_BY_OTHER_COMP" Runat="server" ControlToValidate="txtDESC_RISK_DECL_BY_OTHER_COMP"
										ClientValidationFunction="ValidateMaxLength" Display="Dynamic" ErrorMessage="Please enter only 100 chars."></asp:CustomValidator>
								</TD>
							</tr>--%>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capVEHICLE_MODIFIED" runat="server">Has this vehicle been modified?</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbVEHICLE_MODIFIED" onfocus="SelectComboIndex('cmbVEHICLE_MODIFIED')" runat="server"></asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"><asp:label id="capVEHICLE_MODIFIED_DETAILS" runat="server"></asp:label><span id="spnVEHICLE_MODIFIED_DETAILS" class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtVEHICLE_MODIFIED_DETAILS" runat="server" size="30" maxlength="100"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvVEHICLE_MODIFIED_DETAILS" runat="server" ControlToValidate="txtVEHICLE_MODIFIED_DETAILS"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capUSED_IN_RACE_SPEED" runat="server">Used to participate in any race or speed contest?</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbUSED_IN_RACE_SPEED" onfocus="SelectComboIndex('cmbUSED_IN_RACE_SPEED')" runat="server"></asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"><asp:label id="capUSED_IN_RACE_SPEED_CONTEST" runat="server"></asp:label><span id="spnUSED_IN_RACE_SPEED_CONTEST" class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtUSED_IN_RACE_SPEED_CONTEST" runat="server" size="30" maxlength="250"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvUSED_IN_RACE_SPEED_CONTEST" runat="server" ControlToValidate="txtUSED_IN_RACE_SPEED_CONTEST"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" CausesValidation="False" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnCopy" runat="server" CausesValidation="False" Text="Copy"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" CausesValidation="False" Text="Delete"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" CausesValidation="False"
										Text="Activate/Deactivate"></cmsb:cmsbutton></td>
								<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
							<tr>
								<td colSpan="4"><INPUT id="hidFormSaved" type="hidden" name="hidFormSaved" runat="server">
									<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
									<INPUT id="hidREC_VEH_ID" type="hidden" value="0" name="hidREC_VEH_ID" runat="server">
									<INPUT id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server"> <INPUT id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server">
									<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"> <INPUT id="hidLOOKUP_UNIQUE_ID" type="hidden" runat="server" NAME="hidLOOKUP_UNIQUE_ID">
									<INPUT id="hidMessage" type="hidden" runat="server" NAME="hidMessage">
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</BODY>
</HTML>






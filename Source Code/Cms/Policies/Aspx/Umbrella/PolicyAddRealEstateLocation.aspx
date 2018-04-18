<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page validateRequest="false" CodeBehind="PolicyAddRealEstateLocation.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Umbrella.PolicyAddRealEstateLocation" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		function ResetForm(frmID)
		{
		 DisableValidators();
		 ChangeColor();
		 document.POL_UMB_REAL_ESTATE_LOCATION.reset();
		 
		 return false;
		 }
		
		function AddData()
			{
			
			document.getElementById('hidLOCATION_ID').value	=	'New';
			//document.getElementById('txtCLIENT_LOCATION_NUMBER').focus();
			document.getElementById('txtCLIENT_LOCATION_NUMBER').value  = '';
			//document.getElementById('txtLOCATION_NUMBER').value  = '';
			document.getElementById('txtADDRESS_1').value  = '';
			document.getElementById('txtADDRESS_2').value  = '';
			document.getElementById('txtCITY').value  = '';
			document.getElementById('txtCOUNTY').value  = '';
			document.getElementById('cmbSTATE').options.selectedIndex = -1;
			document.getElementById('txtZIPCODE').value  = '';
			//document.getElementById('txtPHONE_NUMBER').value  = '';
			//document.getElementById('txtFAX_NUMBER').value  = '';
			document.getElementById('cmbOCCUPIED_BY').options.selectedIndex = -1;
			document.getElementById('txtNUM_FAMILIES').value  = '';
			document.getElementById('cmbBUSS_FARM_PURSUITS').options.selectedIndex = -1;
			document.getElementById('txtBUSS_FARM_PURSUITS_DESC').value  = '';
			document.getElementById('cmbPERS_INJ_COV_82').options.selectedIndex = -1;
			document.getElementById('cmbLOC_EXCLUDED').options.selectedIndex = 0;
			document.getElementById('cmbOTHER_POLICY').options.selectedIndex = -1;

			if(document.getElementById('btnActivateDeactivate'))
			document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			if(document.getElementById('btnDelete'))
			document.getElementById('btnDelete').setAttribute('disabled',true);
		    ChangeColor();
			DisableValidators();
			}
			
		function populateXML()
			{
				
				if(document.getElementById('hidFormSaved').value == '0')
					{
		
						if(document.getElementById('hidOldData').value!="")
						{
							populateFormData(document.getElementById('hidOldData').value,POL_UMB_REAL_ESTATE_LOCATION);
						}    
			       
						else
						{
							AddData();
						}
					}
					//setTab();
					return false;
			
				}
			
			function GetTerritory()
			{
				if(document.getElementById('txtZIPCODE').value!="")
				{
					if(isNaN(document.getElementById('txtZIPCODE').value))
						return;
					var strZip = document.getElementById('txtZIPCODE').value;				
					PolicyAddRealEstateLocation.AjaxGetCountyForZip(strZip,PutTerritory);
					//myTSMain1.GetCountyForZip.callService(PutTerritory, "GetCountyForZip",strZip);
				}			
			}
			function PutTerritory(Result)
			{
				if(Result.error)
				{
					var xfaultcode   = Result.errorDetail.code;
					var xfaultstring = Result.errorDetail.string;
					var xfaultsoap   = Result.errorDetail.raw;        				
				}
				else		
					document.getElementById("txtCOUNTY").value= Result.value;
			}
				
			function OpenPopupWindow(Url)
			{
				var myUrl = Url;
				window.open(myUrl,'','toolbar=no, directories=no, location=no, status=yes, menubar=no, resizable=yes, scrollbars=no,  width=500, height=500'); 
				return false;		
			}
			
			function OpenLookupWindow(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Arg1,Arg2,Arg3,Arg4)
			{
					
				var win = window.open(Url + '?DataValueField=' + DataValueField + '&DataTextField=' + 
									DataTextField + '&DataValueFieldID=' + DataValueFieldID + 
									'&DataTextFieldID=' + DataTextFieldID + '&LookupCode=' + LookupCode + 
									'&Arg1=' + Arg1 + '&Arg2=' + Arg2 + '&Arg3=' + Arg3 + '&Arg4=' + Arg4,
									'review','height=500, width=500,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no' );
				
			}
			function cmbBUSS_FARM_PURSUITS_Change()
			{
				combo = document.getElementById("cmbBUSS_FARM_PURSUITS");
				
				if(combo!=null && combo.selectedIndex!=-1 && combo.options[combo.selectedIndex].value=="<%=BUSS_PURSUIT_OTHER_BUSINESS.ToString()%>") //Other Business
				{
					document.getElementById("txtBUSS_FARM_PURSUITS_DESC").style.display = "inline";
					document.getElementById("spnBUSS_FARM_PURSUITS_DESC").style.display = "inline";
					document.getElementById("capBUSS_FARM_PURSUITS_DESC").style.display = "inline";
					EnableValidator("rfvBUSS_FARM_PURSUITS_DESC",true);
				}
				else
				{
					document.getElementById("txtBUSS_FARM_PURSUITS_DESC").style.display = "none";
					document.getElementById("spnBUSS_FARM_PURSUITS_DESC").style.display = "none";
					document.getElementById("capBUSS_FARM_PURSUITS_DESC").style.display = "none";
					EnableValidator("rfvBUSS_FARM_PURSUITS_DESC",false);
				}
				ChangeColor();
			}
			// Added by Swarup For checking zip code for LOB: Start
			function GetZipForState_OLD()
			{		    
				GlobalError=true;
				if(document.getElementById('cmbSTATE').value==14 ||document.getElementById('cmbSTATE').value==22||document.getElementById('cmbSTATE').value==49)
				{ 
					if(document.getElementById('txtZIPCODE').value!="")
					{
						var intStateID = document.getElementById('cmbSTATE').options[document.getElementById('cmbSTATE').options.selectedIndex].value;
						var strZipID = document.getElementById('txtZIPCODE').value;	
						var co=myTSMain1.createCallOptions();
						co.funcName = "FetchZipForState";
						co.async = false;
						co.SOAPHeader= new Object();
						var oResult = myTSMain1.FetchZip.callService(co,intStateID,strZipID);
						handleResult(oResult);	
						if(GlobalError)
						{
							return false;
						}
						else
						{
							return true;
						}
					}	
					return false;
				}
				else 
					return true;		
			}
			
			
				////////////////////////////////////AJAX CALLS FOR ZIP///////////////////////////
		function GetZipForState()
		{		
			GlobalError=true;
			if(document.getElementById('cmbSTATE').value==14 ||document.getElementById('cmbSTATE').value==22||document.getElementById('cmbSTATE').value==49)
			{ 
				if(document.getElementById('txtZIPCODE').value!="")
				{
					var intStateID = document.getElementById('cmbSTATE').options[document.getElementById('cmbSTATE').options.selectedIndex].value;
					var strZipID = document.getElementById('txtZIPCODE').value;	
					var result = PolicyAddRealEstateLocation.AjaxFetchZipForState(intStateID,strZipID);
					return AjaxCallFunction_CallBack(result);
									
				}
				return false;
			}
			else 
				return true;
				
		}
		
		function AjaxCallFunction_CallBack(response)
		{		
			if(document.getElementById('cmbSTATE').value==14 ||document.getElementById('cmbSTATE').value==22||document.getElementById('cmbSTATE').value==49)
			{ 
				if(document.getElementById('txtZIPCODE').value!="")
				{
					handleResult(response);
					if(GlobalError)
					{
						return false;
					}
					else
					{
						return true;
					}
				}	
				return false;
			}
			else 
				return true;		
		}
		/////EMP ZIP AJAX////////////////
			function ChkResult(objSource , objArgs)
			{
				objArgs.IsValid = true;
				if(objArgs.IsValid == true)
				{
					objArgs.IsValid = GetZipForState();
					if(objArgs.IsValid == false)
						document.getElementById('csvZIPCODE').innerHTML = "The zip code does not belong to the state";				
				}
				return;
				if(GlobalError==true)
				{
					Page_IsValid = false;
					objArgs.IsValid = false;
				}
				else
				{
					objArgs.IsValid = true;				
				}
				document.getElementById("btnSave").click();
			}
				
			function handleResult(res) 
			{
			if(!res.error)
				{
				if (res.value!="" && res.value!=null ) 
					{
						GlobalError=false;
					}
					else
					{
						GlobalError=true;
					}
				}
				else
				{
					GlobalError=true;		
				}
			}		
		
	// Added by Swarup For checking zip code for LOB: End
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="populateXML();cmbBUSS_FARM_PURSUITS_Change();ApplyColor();">
		<FORM id="POL_UMB_REAL_ESTATE_LOCATION" method="post" runat="server">
			
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4">
						<asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label>
					</td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow>Please 
									note that all fields marked with * are mandatory</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capLOCATION_NUMBER" runat="server">LOCATION NUMBER</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%" colspan ="3"><asp:textbox id="txtLOCATION_NUMBER" runat="server" size="6" MaxLength="4"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvLOCATION_NUMBER" runat="server" ControlToValidate="txtLOCATION_NUMBER" ErrorMessage="LOCATION_NUMBER can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revLOCATION_NUMBER" runat="server" ControlToValidate="txtLOCATION_NUMBER" Display="Dynamic"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%" style = "display:none" ><asp:label id="capCLIENT_LOCATION_NUMBER" runat="server">Customer Location Number</asp:label></TD>
								<TD class="midcolora" width="32%" style = "display:none"><asp:textbox id="txtCLIENT_LOCATION_NUMBER" runat="server" size="23" maxlength="20"></asp:textbox></TD>
							</tr>
							<tr style = "display:none">
								<td class="midcolora" ><asp:label id="capPullCustomerAddress" runat="server">Would you like to pull customer address</asp:label></td>
								<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnPullCustomerAddress" runat="server" Text="Pull Customer Address"></cmsb:cmsbutton></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capADDRESS_1" runat="server">Address1</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtADDRESS_1" runat="server" size="30" maxlength="75"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvADDRESS1" ControlToValidate="txtADDRESS_1" Display="Dynamic" Runat="server"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capADDRESS_2" runat="server">Address2</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtADDRESS_2" runat="server" size="30" maxlength="75"></asp:textbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCITY" runat="server">City</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCITY" runat="server" size="30" maxlength="75"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvCITY" ControlToValidate="txtCITY" Display="Dynamic" Runat="server"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSTATE" runat="server">State</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSTATE" onfocus="SelectComboIndex('cmbSTATE')" runat="server">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfv_STATE" ControlToValidate="cmbSTATE" Display="Dynamic" Runat="server"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capZIPCODE" runat="server">Zip</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtZIPCODE" runat="server" size="13" maxlength="10" onBlur="GetZipForState();"></asp:textbox><BR>
									<asp:customvalidator id="csvZIPCODE" Runat="server" ClientValidationFunction="ChkResult" ErrorMessage=" "
												Display="Dynamic" ControlToValidate="txtZIPCODE"></asp:customvalidator>
									<asp:requiredfieldvalidator id="rfvZIPCODE" ControlToValidate="txtZIPCODE" Display="Dynamic" Runat="server"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revZIPCODE" runat="server" ControlToValidate="txtZIPCODE" Display="Dynamic"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capCOUNTY" runat="server">County</asp:label><span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOUNTY" runat="server" size="30" maxlength="75" ReadOnly="True"></asp:textbox><IMG id="imgSelect" style="CURSOR: hand" alt="" src="/cms/cmsweb/images/selecticon.gif"
										runat="server"><BR>
									<asp:requiredfieldvalidator id="rfvCOUNTY" ControlToValidate="txtCOUNTY" Display="Dynamic" Runat="server"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr style="display:none">
								<TD class="midcolora" width="18%"><asp:label id="capPHONE_NUMBER" runat="server">Phone</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtPHONE_NUMBER" runat="server" size="16" maxlength="13"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revPHONE_NUMBER" runat="server" ControlToValidate="txtPHONE_NUMBER" Display="Dynamic"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capFAX_NUMBER" runat="server">Fax</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtFAX_NUMBER" runat="server" size="16" maxlength="13"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revFAX_NUMBER" runat="server" ControlToValidate="txtFAX_NUMBER" Display="Dynamic"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capOCCUPIED_BY" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbOCCUPIED_BY" onfocus="SelectComboIndex('cmbOCCUPIED_BY')" runat="server">										
									</asp:dropdownlist></td>
								<TD class="midcolora" width="18%"><asp:label id="capNUM_FAMILIES" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtNUM_FAMILIES" runat="server" maxlength="2" size="3"></asp:textbox><br>
								<asp:rangevalidator id="rngNUM_FAMILIES" runat="server" Display="Dynamic" ControlToValidate="txtNUM_FAMILIES"
										MaximumValue="99" MinimumValue="1" Type="Integer"></asp:rangevalidator></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capBUSS_FARM_PURSUITS" runat="server"></asp:label><span id="spnBUSS_FARM_PURSUITS" class="mandatory">*</span> </TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbBUSS_FARM_PURSUITS" onfocus="SelectComboIndex('cmbBUSS_FARM_PURSUITS')" runat="server">										
									</asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvBUSS_FARM_PURSUITS" runat="server" Display="Dynamic" 
										ControlToValidate="cmbBUSS_FARM_PURSUITS"></asp:requiredfieldvalidator>
								</td>
								<TD class="midcolora" width="18%"><asp:label id="capBUSS_FARM_PURSUITS_DESC" runat="server"></asp:label><span id="spnBUSS_FARM_PURSUITS_DESC" class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtBUSS_FARM_PURSUITS_DESC" runat="server" maxlength="50" size="40"></asp:textbox><br>
								<asp:requiredfieldvalidator id="rfvBUSS_FARM_PURSUITS_DESC" runat="server" Display="Dynamic" 
										ControlToValidate="txtBUSS_FARM_PURSUITS_DESC"></asp:requiredfieldvalidator>
								</td>								
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capPERS_INJ_COV_82" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPERS_INJ_COV_82" onfocus="SelectComboIndex('cmbPERS_INJ_COV_82')" runat="server">										
									</asp:dropdownlist>
								</td>
								<TD class="midcolora" width="18%"><asp:label id="capLOC_EXCLUDED" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOC_EXCLUDED" onfocus="SelectComboIndex('cmbLOC_EXCLUDED')" runat="server">										
									</asp:dropdownlist>
								</td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capOTHER_POLICY" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%" colspan ="3"><asp:dropdownlist id="cmbOTHER_POLICY" onfocus="SelectComboIndex('cmbOTHER_POLICY')" runat="server">										
									</asp:dropdownlist></td>
							</tr>
							<tr>
								<td class="midcolora" colSpan="2">
									<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate" CausesValidation="False"></cmsb:cmsbutton>
								</td>
								<td class="midcolorr" colSpan="2">
									<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" CausesValidation="False"></cmsb:cmsbutton>&nbsp;
									<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidAPP_VERSION_ID" runat="server">
			<INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidAPP_ID" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			<INPUT id="hidLOCATION_ID" type="hidden" name="hidLOCATION_ID" runat="server"> <INPUT id="hidCheckZipSubmit" type="hidden" name="hidCheckZipSubmit" runat="server">
			<input id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <input id="hidAPP_LOB" type="hidden" name="hidAPP_LOB" runat="server">
			<INPUT id="hidDWELLING_ID" type="hidden" value="0" name="hidDWELLING_ID" runat="server">
		</FORM>
		<script>
			if (document.getElementById("hidFormSaved").value == "5")
			 {
				/*Record deleted*/
				/*Refreshing the grid and coverting the form into add mode*/
				/*Using the javascript*/
				RemoveTab(5,this.parent);	
				RemoveTab(4,this.parent);	
				RemoveTab(3,this.parent);	
				RemoveTab(2,this.parent);
				RefreshWebGrid("5","1",true,true); 
				document.getElementById('hidLOCATION_ID').value = "NEW";				
			}
			else if (document.getElementById("hidFormSaved").value == "1")
			{
				this.parent.strSelectedRecordXML = "-1";
				RefreshWebGrid(document.getElementById('hidFormSaved').value,TrimTheString(document.getElementById('hidLOCATION_ID').value), true);
				
			}
		</script>
		</SCRIPT>
	</BODY>
</HTML>

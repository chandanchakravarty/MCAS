<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page validateRequest=false language="c#" Codebehind="AddInsuredVehicle.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddInsuredVehicle" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Insured Vehicle</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language='javascript'>
		
	
		function ConvertToUpper(e,r)
		{
		if (e.keyCode > 96 && e.keyCode < 123)
		r.value = r.value.toUpperCase();
		}
		function CheckOwnedVehicleSelection()
		{
			var NonOwnedVehicle = document.getElementById('cmbNON_OWNED_VEHICLE').value=="Y";
			if(NonOwnedVehicle)
			{
				document.getElementById("rfvVehicle").setAttribute("isValid",false);
				document.getElementById("rfvVehicle").setAttribute("enabled",false);
				document.getElementById("rfvVehicle").style.display="none";				
				document.getElementById("cmbVehicle").disabled = true;
				document.getElementById("cmbVehicle").style.display="none";
				document.getElementById("capVehicle").style.display="none";
				document.getElementById("spanVehicle").style.display="none";
				document.getElementById("hidPolicy_Vehicle_ID").value = "";
				
				document.getElementById("txtVIN").disabled = false;
				document.getElementById("txtVEHICLE_YEAR").disabled = false;
				document.getElementById("txtMAKE").disabled = false;
				document.getElementById("cmbPURPOSE_OF_USE")
					document.getElementById("cmbPURPOSE_OF_USE").setAttribute("disabled",false);				
				document.getElementById("txtMODEL").disabled = false;
				document.getElementById("spanVIN").style.display="none";			
				document.getElementById("rfvVIN").style.display="none";			
				document.getElementById("rfvVIN").setAttribute("isValid",false);
				document.getElementById("rfvVIN").setAttribute("enabled",false);					
				document.getElementById('txtVIN').className = "";
				document.getElementById("imgLookUpWindow").style.display="inline";
			}
			else
			{
				document.getElementById("imgLookUpWindow").style.display="none";
				//document.getElementById("cmbPURPOSE_OF_USE").setAttribute("disabled",true);				
				document.getElementById("rfvVehicle").setAttribute("isValid",true);
				document.getElementById("rfvVehicle").setAttribute("enabled",true);								
				document.getElementById("cmbVehicle").disabled = false;
				document.getElementById("spanVehicle").style.display="inline";
				document.getElementById("cmbVehicle").style.display="inline";
				document.getElementById("capVehicle").style.display="inline";
				document.getElementById("spanVIN").style.display="inline";							
				document.getElementById("rfvVIN").setAttribute("isValid",true);
				document.getElementById("rfvVIN").setAttribute("enabled",true);	
				document.getElementById("txtVIN").setAttribute("enabled",true);	
				document.getElementById('txtVIN').className = "MandatoryControl";
			}		
		
		}
		function AddData()
		{
		    ChangeColor();
			DisableValidators();
			document.getElementById('hidINSURED_VEHICLE_ID').value	=	'New';
			document.getElementById('cmbNON_OWNED_VEHICLE').style.display = "inline";
			//document.getElementById('cmbNON_OWNED_VEHICLE').focus();
			//Manoj Rathore on 8th May 2009 Itrack # 5639			
			
			if(document.getElementById('cmbNON_OWNED_VEHICLE')) 
			   try
		 	{	
			document.getElementById('cmbNON_OWNED_VEHICLE').focus();
			}
		    catch(e)
			{
			  return false;
			}
			//Done for Itrack Issue 6327 on 16 Sept 09
			document.getElementById('txtMAKE').setAttribute('disabled',false);
			document.getElementById('txtMODEL').setAttribute('disabled',false);
			document.getElementById('txtVIN').setAttribute('disabled',false);
			document.getElementById('txtBODY_TYPE').setAttribute('disabled',false);
			document.getElementById('txtVEHICLE_YEAR').setAttribute('disabled',false);
			document.getElementById('cmbSTATE').setAttribute('disabled',false);
			document.getElementById('cmbPURPOSE_OF_USE').setAttribute('disabled',false);
			document.getElementById('cmbVehicle').value = "";
			document.getElementById('txtVEHICLE_YEAR').value  = '';
			document.getElementById('txtMAKE').value  = '';
			document.getElementById('txtMODEL').value  = '';
			document.getElementById('txtVIN').value  = '';
			document.getElementById('txtBODY_TYPE').value  = '';
			//document.getElementById('txtPLATE_NUMBER').value  = '';
			document.getElementById('cmbSTATE').options.selectedIndex = -1;
			//document.getElementById('cmbOWNER').options.selectedIndex = -1;
			//document.getElementById('cmbDRIVER').options.selectedIndex = -1;
			document.getElementById('txtWHERE_VEHICLE_SEEN').value  = '';
			document.getElementById('txtWHEN_VEHICLE_SEEN').value  = '';//Done for Itrack Issue 6327 on 16 Sept 09	
			document.getElementById('cmbPURPOSE_OF_USE').options.selectedIndex = 1;
			document.getElementById('cmbUSED_WITH_PERMISSION').options.selectedIndex = 1;
			document.getElementById('txtOTHER_VEHICLE_INSURANCE').value  = '';
			document.getElementById('txtDESCRIBE_DAMAGE').value  = '';			
			document.getElementById('txtESTIMATE_AMOUNT').value  = '';			
			document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
		}
		function populateXML()
		{
			var tempXML;
			if(document.getElementById("hidFormSaved").value=="" || document.getElementById("hidFormSaved").value=="0" || document.getElementById("hidFormSaved").value=="1")
			{
				if(document.getElementById('hidOldData').value!="")
				{
					tempXML=document.getElementById('hidOldData').value					
					document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
					document.getElementById('hidOldData').value		=	 tempXML;
					document.getElementById("cmbVehicle").disabled = true;
					populateFormData(tempXML,CLM_INSURED_VEHICLE);	
					document.getElementById("txtESTIMATE_AMOUNT").value = formatCurrencyWithCents(document.getElementById("txtESTIMATE_AMOUNT").value);
					//Added for Itrack Issue 5833 on 20 May 2009
					if(document.getElementById('cmbNON_OWNED_VEHICLE').options.selectedIndex == 0)
					{
						document.getElementById("txtVIN").readOnly = true;
						document.getElementById("txtVEHICLE_YEAR").readOnly = true;
						document.getElementById("txtMAKE").readOnly = true;
						document.getElementById('cmbPURPOSE_OF_USE').disabled= true;
						document.getElementById("cmbPURPOSE_OF_USE").value= document.getElementById("hidPURPOSE_OF_USE").value;
						document.getElementById("txtMODEL").readOnly = true;
						document.getElementById('cmbSTATE').disabled= true;
						document.getElementById("cmbSTATE").value= document.getElementById("hidSTATE").value;
						document.getElementById('txtBODY_TYPE').readOnly =true;
					}
				}
				else
				{
					AddData();			
				}
			}
			return false;
		}		
		function GetAgeOfVehicle()
		{
		}
		/*function ShowLookUpWindow()
		{
			if(document.getElementById('txtVIN')!=null)
			{			
				var vin = document.getElementById('txtVIN').value;					
				vin = SearchAndReplace(vin,"&","^");
				
				ShowPopup('/cms/application/Aspx/AddVINMasterPopup.aspx?CalledFrom=' + document.getElementById('hidCalledFrom').value + '&VIN='+vin,'VehicleInformation', 400, 200);
				return;
			}
		}*/
		function ShowLookUpWindow()
		{
			if(document.getElementById('txtVIN')!=null)
			{	
				var vin = document.getElementById('txtVIN').value;
				AddInsuredVehicle.AjaxGetDetailsFromVIN(SearchAndReplace(vin,"&","REPLACE_CHAR"),CallBackFunction);												
			}					
			
		}
		
		function CallBackFunction(Result)
		{
			if(!(Result.error))
			{
				if(Result.value != "0" && Result.value!="undefined")
				{
					var objXmlHandler = new XMLHandler();
					var tree = (objXmlHandler.quickParseXML(Result.value).getElementsByTagName('NewDataSet')[0]);
					
					if(tree)
					{
						//-1 means VIN does not exist in DB
						if (tree.childNodes[0].childNodes[0].childNodes[0].text == "-1")
						{
							var vin = document.getElementById('txtVIN').value;
							//Replace & with ^..it will be reversed when it is fetched from QueryString
							vin = SearchAndReplace(vin,"&","^");				
							ShowPopup('/cms/application/Aspx/AddVINMasterPopup.aspx?CalledFrom=' + document.getElementById('hidCalledFrom').value + '&VIN='+vin,'VehicleInformation', 400, 200);
							return;
						}
						//POPULATE TEXT BOX FROM VALUES FETCHED FROM DB
						else
						{
							var lStrAntiLock='',lStrAirBag='';
							if(tree.childNodes[0].childNodes[1].childNodes[0]!=undefined && tree.childNodes[0].childNodes[1].childNodes[0].text!='')
								document.getElementById("txtVEHICLE_YEAR").value = tree.childNodes[0].childNodes[1].childNodes[0].text;
							if(tree.childNodes[0].childNodes[2].childNodes[0] !=undefined && tree.childNodes[0].childNodes[2].childNodes[0].text!='')
								document.getElementById("txtMAKE").value = tree.childNodes[0].childNodes[2].childNodes[0].text;
							if(tree.childNodes[0].childNodes[3].childNodes[0] !=undefined && tree.childNodes[0].childNodes[3].childNodes[0].text!='')
								document.getElementById("txtMODEL").value = tree.childNodes[0].childNodes[3].childNodes[0].text;
							if(tree.childNodes[0].childNodes[4].childNodes[0] !=undefined && tree.childNodes[0].childNodes[4].childNodes[0].text!='')
								document.getElementById("txtBODY_TYPE").value = tree.childNodes[0].childNodes[4].childNodes[0].text;						
							if(tree.childNodes[0].childNodes[5].childNodes[0] !=undefined && tree.childNodes[0].childNodes[5].childNodes[0].text!='')
								lStrAntiLock = tree.childNodes[0].childNodes[5].childNodes[0].text;				
							if(tree.childNodes[0].childNodes[6].childNodes[0] !=undefined && tree.childNodes[0].childNodes[6].childNodes[0].text!='')
								lStrAirBag = tree.childNodes[0].childNodes[6].childNodes[0].text;				
							if(tree.childNodes[0].childNodes[7].childNodes[0] !=undefined && tree.childNodes[0].childNodes[7].childNodes[0].text!='')
								document.getElementById("txtSYMBOL").value = tree.childNodes[0].childNodes[7].childNodes[0].text;				
						
							/*document.getElementById("cmbANTI_LOCK_BRAKES").selectedIndex = -1;
							
							if(lStrAntiLock=="N" || lStrAntiLock=="n")							
								SelectComboOptionByText("cmbANTI_LOCK_BRAKES","No");
							else if(lStrAntiLock!='')
								SelectComboOptionByText("cmbANTI_LOCK_BRAKES","Yes");
							
							document.getElementById("cmbAIR_BAG").selectedIndex = -1;							
							if(lStrAirBag!='')
								SelectComboOption("cmbAIR_BAG",lStrAirBag);								
							*/
								//ApplyColor();
								ChangeColor();
								
						}
					}//if tree
				}//if result value <>0
			}//result <> error
		}//function

		function InitializePage()
		{
			populateXML();
			ApplyColor();
			ChangeColor();
		}
		
		function ShowHideVehicleCombo()
		{
			if (parseInt(document.getElementById("hidINSURED_VEHICLE_ID").value) > 0 || document.getElementById("cmbVehicle").options.length==0) 
			{ 
				document.getElementById("rfvVehicle").setAttribute("isValid",false);
				document.getElementById("rfvVehicle").setAttribute("enabled",false);
				document.getElementById("rfvVehicle").style.display="none";				
				document.getElementById("cmbVehicle").disabled = true;
				document.getElementById("cmbVehicle").style.display="none";
				document.getElementById("capVehicle").style.display="none";
				document.getElementById("spanVehicle").style.display="none";
				document.getElementById("capNON_OWNED_VEHICLE").style.display="none";
				document.getElementById("spanNON_OWNED_VEHICLE").style.display="none";
				document.getElementById("cmbNON_OWNED_VEHICLE").style.display="none"
				document.getElementById("rfvNON_OWNED_VEHICLE").setAttribute("isValid",false);
				document.getElementById("rfvNON_OWNED_VEHICLE").setAttribute("enabled",false);;
				document.getElementById("rfvNON_OWNED_VEHICLE").style.display="none";
				document.getElementById("trVehicle").style.display="none";
				document.getElementById("spanVIN").style.display="none";			
				document.getElementById("rfvVIN").style.display="none";			
				document.getElementById("rfvVIN").setAttribute("isValid",false);
				document.getElementById("rfvVIN").setAttribute("enabled",false);					
				document.getElementById('txtVIN').className = "";
			}
		}
		
		//Done for Itrack Issue 6327 on 16 Sept 09
		function ResetTheForm()
			{
				//Done for Itrack Issue 6327 on 16 Sept 09
				if(document.getElementById('hidFormSaved').value==0)
				{
						AddData();
						populateXML();
						onResetDisableValidators();
				}
				else if(document.getElementById('hidFormSaved').value == 1)
				{
						document.CLM_INSURED_VEHICLE.reset();
						onResetDisableValidators();
				}
				else if(document.getElementById('hidFormSaved').value == 2)
				{
						AddData();
						document.getElementById('hidFormSaved').value = "0";
						onResetDisableValidators();
				}
				else if(document.getElementById('hidFormSaved').value == 3)
				{
						AddData();
						onResetDisableValidators();
				}
				ChangeColor();
				return false;
			}
			
			function onResetDisableValidators()
			{
				
				document.getElementById("rfvVIN").style.display="none";			
				document.getElementById("rfvVIN").setAttribute("isValid",false);
				document.getElementById("rfvVIN").setAttribute("enabled",false);
				document.getElementById("rfvVehicle").setAttribute("isValid",false);
				document.getElementById("rfvVehicle").setAttribute("enabled",false);
				document.getElementById("rfvVehicle").style.display="none";
				document.getElementById("rfvVEHICLE_YEAR").setAttribute("isValid",false);
				document.getElementById("rfvVEHICLE_YEAR").setAttribute("enabled",false);
				document.getElementById("rfvVEHICLE_YEAR").style.display="none";
				document.getElementById("rfvMODEL").setAttribute("isValid",false);
				document.getElementById("rfvMODEL").setAttribute("enabled",false);
				document.getElementById("rfvMODEL").style.display="none";
				document.getElementById("rfvMAKE").setAttribute("isValid",false);
				document.getElementById("rfvMAKE").setAttribute("enabled",false);
				document.getElementById("rfvMAKE").style.display="none";
				document.getElementById("rfvVEHICLE_YEAR").setAttribute("isValid",false);
				document.getElementById("rfvVEHICLE_YEAR").setAttribute("enabled",false);
				document.getElementById("rfvVEHICLE_YEAR").style.display="none";
			}
		//Added till here
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin='0' topMargin='0' onload='InitializePage();ShowHideVehicleCombo();'>
		<FORM id='CLM_INSURED_VEHICLE' method='post' runat='server'>			
			<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
				  <tr><%--Added for Itrack Issue 5833 on 20 May 2009--%>
						<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				  </tr>
				<TR id=trBody runat="server">	 
					<TD>
						<TABLE width='100%' border='0' align='center'>
							<tr>
								<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
									mandatory</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							
							<tr id="trVehicle">
								<TD class='midcolora' width='18%'>
									<asp:Label id="capNON_OWNED_VEHICLE" runat="server"></asp:Label><span class="mandatory" id="spanNON_OWNED_VEHICLE">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id='cmbNON_OWNED_VEHICLE' OnChange="CheckOwnedVehicleSelection();" OnFocus="SelectComboIndex('cmbNON_OWNED_VEHICLE')"
										runat='server'>
										<asp:ListItem Value='N'>No</asp:ListItem>
										<asp:ListItem Value='Y'>Yes</asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvNON_OWNED_VEHICLE" ControlToValidate="cmbNON_OWNED_VEHICLE" Display="Dynamic"
										Runat="server"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='18%'>
									<Asp:Label ID="capVehicle" RunAt="Server"></Asp:Label><Span Class="mandatory" id="spanVehicle">*</Span></TD>
								<TD class='midcolora' width='32%'>
									<Asp:DropDownList id='cmbVehicle' RunAt="Server" AutoPostBack="True"></Asp:DropDownList><BR>
									<Asp:RequiredFieldValidator ID="rfvVehicle" ControlToValidate="cmbVehicle" Display="Dynamic" Runat="Server"></Asp:RequiredFieldValidator>
								</TD>
							</tr>

							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capVIN" runat="server"></asp:label><span class="mandatory" id="spanVIN">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtVIN" runat="server" size="25" maxlength="17"></asp:textbox><IMG id=imgLookUpWindow style="CURSOR: hand;display:none;" onclick=ShowLookUpWindow(); src="/cms/cmsweb/images<%=GetColorScheme()%>/calender.gif" ><br>
									<asp:requiredfieldvalidator id="rfvVIN" ControlToValidate="txtVIN" Display="Dynamic" Runat="server"></asp:requiredfieldvalidator></TD>
								<td class="midcolora" colspan="2"></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capVEHICLE_YEAR" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtVEHICLE_YEAR" runat="server" size="4" maxlength="4"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvVEHICLE_YEAR" runat="server" ControlToValidate="txtVEHICLE_YEAR" Display="Dynamic"></asp:requiredfieldvalidator><asp:rangevalidator id="rngVEHICLE_YEAR" ControlToValidate="txtVEHICLE_YEAR" Display="Dynamic" Runat="server"
										Type="Integer" MinimumValue="1950"></asp:rangevalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capMAKE" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtMAKE" runat="server" size="32" maxlength="28"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvMAKE" runat="server" ControlToValidate="txtMAKE" Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capMODEL" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtMODEL" runat="server" size="30" maxlength="28"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvMODEL" runat="server" ControlToValidate="txtMODEL" Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capBODY_TYPE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtBODY_TYPE" runat="server" size="20" maxlength="28"></asp:textbox></TD>
							</tr>
							<tr>
								<%--<TD class='midcolora' width='18%'>
									<asp:Label id="capPLATE_NUMBER" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:textbox id='txtPLATE_NUMBER' runat='server' size='30' maxlength='10'></asp:textbox>
								</TD>--%>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capSTATE" runat="server"></asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:dropdownlist id='cmbSTATE' runat='server'></asp:dropdownlist>
								</TD>
								<td colspan="2" class="midcolora"></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capWHERE_VEHICLE_SEEN" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtWHERE_VEHICLE_SEEN" runat="server" size="40" maxlength="50"></asp:textbox>
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="capWHEN_VEHICLE_SEEN" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtWHEN_VEHICLE_SEEN" runat="server" size="40" maxlength="25"></asp:textbox>
								</TD>
							</tr>
							<tr id="trPURPOSE_OF_USE" runat="server">
								<TD class="midcolora" width="18%"><asp:label id="capPURPOSE_OF_USE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPURPOSE_OF_USE" onfocus="SelectComboIndex('cmbPURPOSE_OF_USE')" runat="server"></asp:dropdownlist></TD>
								<TD class="midcolora" colspan="2"></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capUSED_WITH_PERMISSION" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbUSED_WITH_PERMISSION" onfocus="SelectComboIndex('cmbUSED_WITH_PERMISSION')"
										runat="server"></asp:dropdownlist></TD>							
								<TD class="midcolora" width="18%"><asp:label id="capDESCRIBE_DAMAGE" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtDESCRIBE_DAMAGE" runat="server" maxlength="100" size="40"></asp:textbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capESTIMATE_AMOUNT" runat="server"></asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtESTIMATE_AMOUNT" runat="server" maxlength="10" size="15" CssClass="INPUTCURRENCY"></asp:textbox><br>
									<asp:rangevalidator id="rngESTIMATE_AMOUNT" ControlToValidate="txtESTIMATE_AMOUNT" Display="Dynamic"
										Runat="server" Type="Currency" MinimumValue="1" MaximumValue="9999999999"></asp:rangevalidator>
								</TD>														
								<TD class="midcolora" width="18%"><asp:label id="capOTHER_VEHICLE_INSURANCE" runat="server"></asp:label></TD>							
								<TD class="midcolora" width="32%"><asp:textbox id="txtOTHER_VEHICLE_INSURANCE" runat="server" maxlength="100" size="40"></asp:textbox></TD>								
							</tr>
							<tr>
								<td class='midcolora' colspan="2"><%--Done for Itrack Issue 5833 on 18 June 2009--%>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text='ActivateDeactivate'></cmsb:cmsbutton>
								</td>
								<td class='midcolora'><%--Done for Itrack Issue 5833 on 18 June 2009--%>
									<%--<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text='ActivateDeactivate'></cmsb:cmsbutton>--Done for Itrack Issue 5833 on 18 June 2009--%>
								</td>
								<td class='midcolorr' colspan="2">
									<cmsb:cmsbutton class="clsButton" id='btnDelete' runat="server" Text='Delete'></cmsb:cmsbutton><%--Added for Itrack Issue 5833 on 20 May 2009--%>
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>				
								</td>
							</tr>
						</TABLE>
						<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
						<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
						<INPUT id="hidLOB_ID" type="hidden" value="" name="hidLOB_ID" runat="server">
						<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
						<!--Added for Itrack Issue 5833 on 20 May 2009-->
						<INPUT id="hidSTATE" type="hidden" value="0" name="hidSTATE" runat="server">
						<INPUT id="hidPURPOSE_OF_USE" type="hidden" value="0" name="hidPURPOSE_OF_USE" runat="server">
						<INPUT id="hidINSURED_VEHICLE_ID" type="hidden" value="0" name="hidINSURED_VEHICLE_ID"
							runat="server"> <INPUT id="hidCalledFrom" type="hidden" value="PPA" name="hidCalledFrom" runat="server">
						<INPUT id="txtSYMBOL" type="hidden"> <INPUT id="hidMakeCode" type="hidden"> <INPUT id="txtVEHICLE_AGE" type="hidden">
						<%--<Select id="cmbANTI_LOCK_BRAKES" Style="DISPLAY:none">
						</Select>
						<Select id="cmbAIR_BAG" Style="DISPLAY:none">
						</Select>--%>
						<INPUT id="hidVehicleID" type="hidden" Name="hidVehicleID"> <INPUT id="hidPolicy_Vehicle_ID" type="hidden" Name="hidPolicy_Vehicle_ID" runat="server">
					</TD>
				</TR>
			</TABLE>
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidINSURED_VEHICLE_ID').value,false);
		</script>
	</BODY>
</HTML>

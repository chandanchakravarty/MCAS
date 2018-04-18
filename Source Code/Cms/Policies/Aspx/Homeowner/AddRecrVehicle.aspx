<%@ Page language="c#" Codebehind="AddRecrVehicle.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Homeowner.AddRecrVehicle" validateRequest="false" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddRecrVehicle</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../../cmsweb/scripts/common.js"></script>
		<script src="../../../cmsweb/scripts/form.js"></script>
		<script language="javascript">
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
			function txtDESC_RISK_DECL_BY_OTHER_COMP_Validate(objSource,objArgs)
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
			function CheckDeductible(objSource,objArgs)
			{
				combo = document.getElementById("cmbDEDUCTIBLE");
				if(combo==null || combo.selectedIndex==-1)
					objArgs.IsValid = true;
				
				var effDate=document.getElementById("hidAPP_INCEPTION_DATE").value;								
				if(effDate=='')
				{
					objArgs.IsValid = true;
					return;
				}
				var Result = DateComparer(effDate,"<%=Cms.BusinessLayer.BlApplication.ClsHomeRecrVehicles.NewBusinessDate.ToShortDateString()%>",jsaAppDtFormat);												
				if(Result==true)//date is within the limits
				{
					var SelectedValue = combo.options[combo.selectedIndex].value;					
					if(SelectedValue < <%=Cms.BusinessLayer.BlApplication.ClsHomeRecrVehicles.MinDeductibleAmt.ToString()%>)
						objArgs.IsValid = false;
					else
						objSource.IsValid = true;					
				}
				else
					objArgs.IsValid = true;				
				
			}
			// Function added , Charles (10-Dec-09), Itrack 6841 -->	
			function Trailer_HP_Mandatory()
			{		
				if(document.getElementById('cmbVEHICLE_TYPE_NAME').options[document.getElementById('cmbVEHICLE_TYPE_NAME').selectedIndex].value == '11811')
					{
						document.getElementById("spnHORSE_POWER").style.display="none";
						document.getElementById("rfvHORSE_POWER").style.display="none";
						document.getElementById("rfvHORSE_POWER").setAttribute('enabled',false);
					}
				else
					{
						document.getElementById("spnHORSE_POWER").style.display="inline";
						document.getElementById("rfvHORSE_POWER").setAttribute('enabled',true);
					}
			}
			
			function txtDESC_RISK_DECL_BY_OTHER_COMP_Validate_255(objSource,objArgs)
			{
				if (document.getElementById(objSource.controltovalidate).value.length > 255)
				{
					objArgs.IsValid = false;
				}
				else
				{
					objArgs.IsValid = true;
				}
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
			
		// Modified by Swastika	on 16th Mar'06 for Pol Iss #156	
		function ResetForm()
			{
			//alert(document.APP_HOME_OWNER_RECREATIONAL_VEHICLES.hidOldData.value);
				
				DisableValidators();
				if ( document.APP_HOME_OWNER_RECREATIONAL_VEHICLES.hidOldData.value == '' )
				{   
					
					AddData();
					ApplyColor();
					ChangeColor();
					return false;
									
				}
				else
				{
					return true;
				}
			 
			}
			
	  function AddData()
        {
                document.getElementById('txtCOMPANY_ID_NUMBER').value=document.getElementById('hidCompany').value;
				document.getElementById('txtMAKE').value='';
				document.getElementById('txtMODEL').value='';
				document.getElementById('txtSERIAL').value='';
				document.getElementById('txtYEAR').value='';
				document.getElementById('txtHORSE_POWER').value='';
				document.getElementById('txtREMARKS').value='';
				document.getElementById('cmbSTATE_REGISTERED').options.selectedIndex = -1;
				document.getElementById('cmbPRIOR_LOSSES').options.selectedIndex = -1;				
				document.getElementById('cmbIS_UNIT_REG_IN_OTHER_STATE').options.selectedIndex = -1;
				document.getElementById('cmbRISK_DECL_BY_OTHER_COMP').options.selectedIndex = -1;
				document.getElementById('cmbUSED_IN_RACE_SPEED').options.selectedIndex = -1;
				document.getElementById('cmbVEHICLE_MODIFIED').options.selectedIndex = -1;
				document.getElementById('txtDESC_RISK_DECL_BY_OTHER_COMP').value='';
				document.getElementById('txtINSURING_VALUE').value='';
				document.getElementById('cmbDEDUCTIBLE').options.length = 0;
				document.getElementById('cmbVEHICLE_TYPE_NAME').options.selectedIndex = -1;
				//Added by Manoj Rathore on 29th Mar 2007
				document.getElementById('cmbUNIT_RENTED').options.selectedIndex = -1;
				document.getElementById('cmbUNIT_OWNED_DEALERS').options.selectedIndex = -1;
				document.getElementById('cmbYOUTHFUL_OPERATOR_UNDER_25').options.selectedIndex = -1;
				//********************
				
		}		
			
	function Check()
	{
	//Nov,07,2005:Changes made by Mohit	
	 if (document.getElementById('cmbVEHICLE_TYPE_NAME').selectedIndex != '-1')		
	 {
	
	 if (document.getElementById('cmbVEHICLE_TYPE_NAME').options[document.getElementById('cmbVEHICLE_TYPE_NAME').selectedIndex].value == '11434')
	  {
	   document.getElementById('txtMANUFACTURER_DESC').style.display='inline';
	   document.getElementById('lblMANUFACTURER_DESC').style.display='none';
	   document.getElementById("rfvMANUFACTURER_DESC").setAttribute('enabled',true);
	   document.getElementById("rfvMANUFACTURER_DESC").setAttribute('isValid',true);
	   if (document.getElementById("spnMANUFACTURER_DESC") != null)
		{
		document.getElementById("spnMANUFACTURER_DESC").style.display = "inline";
		}
	  }
	  else
	  {
		
		document.getElementById('txtMANUFACTURER_DESC').style.display='none';
		document.getElementById('lblMANUFACTURER_DESC').style.display='inline';
	    document.getElementById('lblMANUFACTURER_DESC').innerHTML='NA';
	    document.getElementById("rfvMANUFACTURER_DESC").setAttribute('isValid',false);
		document.getElementById("rfvMANUFACTURER_DESC").style.display='none';
		document.getElementById("rfvMANUFACTURER_DESC").setAttribute('enabled',false);
		if (document.getElementById("spnMANUFACTURER_DESC") != null)
			{
				document.getElementById("spnMANUFACTURER_DESC").style.display = "none";
			}
	  
	  }
	  }
	  else
	  {
		document.getElementById('txtMANUFACTURER_DESC').style.display='none';
		document.getElementById('lblMANUFACTURER_DESC').style.display='inline';
	    document.getElementById('lblMANUFACTURER_DESC').innerHTML='-N.A.-';
	    document.getElementById("rfvMANUFACTURER_DESC").setAttribute('isValid',false);
		document.getElementById("rfvMANUFACTURER_DESC").style.display='none';
		document.getElementById("rfvMANUFACTURER_DESC").setAttribute('enabled',false);
		if (document.getElementById("spnMANUFACTURER_DESC") != null)
			{
				document.getElementById("spnMANUFACTURER_DESC").style.display = "none";
			}
	  }	
	
	}	
	
	function VehicleTypeChange()
		{
			//if(document.getElementById('cmbVEHICLE_TYPE_NAME').options[document.getElementById('cmbVEHICLE_TYPE_NAME').selectedIndex].value == '11434')		
			//{
			//	document.getElementById('txtMANUFACTURER_DESC').focus();
			//}
			//else
			//{
			//	document.getElementById('txtHORSE_POWER').focus();	
			//}		
			document.getElementById('hidVEHICLE_TYPE_NAME').value='1';
		}
		
	function VehicleTypeIndexChange()
		{
			document.getElementById('cmbVEHICLE_TYPE_NAME').focus();
			document.getElementById('hidVEHICLE_TYPE_NAME').value='2';
		}
		
        //Added For Itrack issue #6710   
	    function Libality()
		{		
		  if(document.getElementById('chkLIABILITY').checked == true)
		  {
			document.getElementById("txtLIABILITY_LIMIT").style.display="inline"
			document.getElementById("txtMEDICAL_PAYMENTS_LIMIT").style.display="inline"
			document.getElementById('chkMEDICAL_PAYMENTS').checked = true;	
		  }
		  else
		  {
			 document.getElementById("txtLIABILITY_LIMIT").style.display="none"
			 document.getElementById("txtMEDICAL_PAYMENTS_LIMIT").style.display="none"
			 document.getElementById('chkMEDICAL_PAYMENTS').checked = false;
		  }
		 }
		
		function MedicalPayment()
		{ 
		   if(document.getElementById('chkMEDICAL_PAYMENTS').checked == false)
		   {					      
				document.getElementById("txtMEDICAL_PAYMENTS_LIMIT").style.display="none" 
				document.getElementById("txtLIABILITY_LIMIT").style.display="none"
				document.getElementById('chkLIABILITY').checked = false;
			} 		  
			else
			{
				document.getElementById("txtMEDICAL_PAYMENTS_LIMIT").style.display="inline"
				document.getElementById("txtLIABILITY_LIMIT").style.display="inline"
				document.getElementById('chkLIABILITY').checked = true;
			}
		 }
		 
		function Physical_Damage()
		{		
		 
		  if(document.getElementById('chkPHYSICAL_DAMAGE').checked == true)
			 {				
			   document.getElementById("txtINSURING_VALUE").style.display="inline"				
			   document.getElementById("rfvINSURING_VALUE").setAttribute('enabled',true);
			   document.getElementById("rfvINSURING_VALUE").setAttribute('isValid',true);
			   
			   if (document.getElementById("spnINSURING_VALUE") != null)
				 {
					document.getElementById("spnINSURING_VALUE").style.display = "inline";
				 }
				 document.getElementById("rfvDEDUCTIBLE").setAttribute('enabled',true);
			     document.getElementById("rfvDEDUCTIBLE").setAttribute('isValid',true);
			     document.getElementById("spnDEDUCTIBLE").style.display = "inline";
			     document.getElementById("rngPHYSICAL_DAMAGE").setAttribute('enabled',true);
			     document.getElementById("rngPHYSICAL_DAMAGE").setAttribute('isValid',true);
			     document.getElementById("rngPHYSICAL_DAMAGE").style.display = "inline";			    
			     document.getElementById("revINSURING_VALUE").setAttribute('enabled',true);
				 document.getElementById("revINSURING_VALUE").setAttribute('isValid',true);
				
			 }
		 else
			 {
				document.getElementById("txtINSURING_VALUE").style.display="none"
				document.getElementById("txtINSURING_VALUE").value="";
				document.getElementById("rfvINSURING_VALUE").setAttribute('enabled',false);
				document.getElementById("rfvINSURING_VALUE").setAttribute('isValid',false);
				document.getElementById("rfvINSURING_VALUE").style.display='none';
				if (document.getElementById("spnINSURING_VALUE") != null)
				 {
					document.getElementById("spnINSURING_VALUE").style.display = "none";
				 }
				 document.getElementById("rfvDEDUCTIBLE").setAttribute('enabled',false);
			     document.getElementById("rfvDEDUCTIBLE").setAttribute('isValid',false);
			     document.getElementById("rfvDEDUCTIBLE").style.display='none';
			     document.getElementById("spnDEDUCTIBLE").style.display = "none";
			     document.getElementById("rngPHYSICAL_DAMAGE").style.display = "none";
			     document.getElementById("rngPHYSICAL_DAMAGE").setAttribute('enabled',false);
			     document.getElementById("rngPHYSICAL_DAMAGE").setAttribute('isValid',false);
			     document.getElementById("revINSURING_VALUE").setAttribute('enabled',false);
				 document.getElementById("revINSURING_VALUE").setAttribute('isValid',false);
			     document.getElementById("revINSURING_VALUE").style.display = "none";
			     
			 }
		 }
		//Add Till here
			
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Check();ApplyColor();ChangeColor();Physical_Damage();Trailer_HP_Mandatory();"> <!-- Added Trailer_HP_Mandatory, Charles (10-Dec-09), Itrack 6841 -->
		<FORM id="APP_HOME_OWNER_RECREATIONAL_VEHICLES" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr id="trMessage" runat="server">
					<td class="midcolorc"><asp:label id="lblInfo" Runat="server" CssClass="errmsg"></asp:label></td>
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
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Text="" Visible="True"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCOMPANY_ID_NUMBER" runat="server">Company ID number</asp:label><span class="mandatory"></span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtCOMPANY_ID_NUMBER" runat="server" maxlength="4" size="4"></asp:textbox><BR>
									<asp:rangevalidator id="rngCOMPANY_ID_NUMBER" runat="server" Type="Integer" MaximumValue="2147483647"
										MinimumValue="1" Display="Dynamic" ErrorMessage="Value should be between 1 and 10000" ControlToValidate="txtCOMPANY_ID_NUMBER"></asp:rangevalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capYEAR" runat="server">Year</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtYEAR" runat="server" maxlength="4" size="5"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvYEAR" runat="server" Display="Dynamic" ErrorMessage="YEAR can't be blank."
										ControlToValidate="txtYEAR"></asp:requiredfieldvalidator>
									<!--Oct24,2005:Sumit Chhabra
										The following rangevalidator has been disabled as the restriction on 1950 year needs to be removed.--><asp:rangevalidator id="rngYEAR" runat="server" Type="Integer" MinimumValue="1900" Display="Dynamic"
										ControlToValidate="txtYEAR" Enabled="True"></asp:rangevalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capMAKE" runat="server">Make</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtMAKE" runat="server" maxlength="30" size="30"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvMAKE" runat="server" Display="Dynamic" ErrorMessage="MAKE can't be blank."
										ControlToValidate="txtMAKE"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capMODEL" runat="server">Model</asp:label><span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtMODEL" runat="server" maxlength="30" size="30"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvMODEL" runat="server" Display="Dynamic" ErrorMessage="Please enter the type"
										ControlToValidate="txtMODEL"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSERIAL" runat="server">Serial #</asp:label><span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtSERIAL" runat="server" maxlength="30" size="30"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvSERIAL" runat="server" Display="Dynamic" ControlToValidate="txtSERIAL"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capSTATE_REGISTERED" runat="server">Registered State</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSTATE_REGISTERED" onfocus="SelectComboIndex('cmbPRIOR_LOSSES')" runat="server">
										<asp:ListItem></asp:ListItem>
										<asp:ListItem Value="Y">Yes</asp:ListItem>
										<asp:ListItem Value="N">No</asp:ListItem>
									</asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvSTATE_REGISTERED" runat="server" Display="Dynamic" ControlToValidate="cmbSTATE_REGISTERED"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capVEHICLE_TYPE" runat="server">Type</asp:label><span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbVEHICLE_TYPE_NAME" onfocus="SelectComboIndex('cmbVEHICLE_TYPE_NAME')" runat="server" onBlur="VehicleTypeChange();" onChange="VehicleTypeIndexChange(); Trailer_HP_Mandatory();"
										AutoPostBack="True"></asp:dropdownlist><br> <!-- Added Trailer_HP_Mandatory, Charles (10-Dec-09), Itrack 6841 -->
									<asp:requiredfieldvalidator id="rfvVEHICLE_TYPE_NAME" runat="server" Display="Dynamic" ControlToValidate="cmbVEHICLE_TYPE_NAME"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capMANUFACTURER_DESC" runat="server">Description</asp:label><span id="spnMANUFACTURER_DESC" class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%">
									<asp:textbox id="txtMANUFACTURER_DESC" runat="server" maxlength="200" size="30"></asp:textbox>
									<asp:Label ID="lblMANUFACTURER_DESC" Runat="server" CssClass="LabelFont">-N.A.-</asp:Label>
									<br>
									<asp:requiredfieldvalidator id="rfvMANUFACTURER_DESC" runat="server" Display="Dynamic" ErrorMessage="Please enter the type"
										ControlToValidate="txtMANUFACTURER_DESC"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capHORSE_POWER" runat="server">H.P./CC's</asp:label><span class="mandatory" id="spnHORSE_POWER">*</span> <!-- Added id spnHORSE_POWER, Charles (10-Dec-09), Itrack 6841 -->
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtHORSE_POWER" runat="server" maxlength="10" size="12"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvHORSE_POWER" runat="server" Display="Dynamic" ControlToValidate="txtHORSE_POWER"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revHORSE_POWER" runat="server" Display="Dynamic" ControlToValidate="txtHORSE_POWER"></asp:regularexpressionvalidator>
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="capREMARKS" runat="server">Remarks</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtREMARKS" runat="server" maxlength="100" size="30" TextMode="MultiLine" Columns="40"
										Rows="3"></asp:textbox><br>
									<asp:customvalidator id="csvREMARKS" Runat="server" ErrorMessage="Please enter only 100 chars." ControlToValidate="txtREMARKS"
										display="Dynamic" ClientValidationFunction="txtDESC_RISK_DECL_BY_OTHER_COMP_Validate"></asp:customvalidator></TD>
							</tr>
							<tr>
								<td class="headerEffectSystemParams" colSpan="4">Coverages</td>
							</tr>
							<tr>
								<td class="midcolora" width="19%"><asp:Label ID="capINSURING_VALUE" Runat="server"></asp:Label><span id ="spnINSURING_VALUE" class="mandatory">*</span>
								<!--check box and range validator added for itrack issue #6710-->
									<asp:CheckBox id="chkPHYSICAL_DAMAGE" checked="false" onclick="Physical_Damage();" runat="server"></asp:CheckBox></td>
								
								<td class="midcolora" width="32%"><asp:TextBox ID="txtINSURING_VALUE" Runat="server" maxlength="30" size="18" CssClass="INPUTCURRENCY"></asp:TextBox>								
								 <asp:rangevalidator id="rngPHYSICAL_DAMAGE" runat="server" Type="Currency" MaximumValue="999999999"
										MinimumValue="1" Display="Dynamic" ControlToValidate="txtINSURING_VALUE"></asp:rangevalidator>
									<br>
									<asp:RequiredFieldValidator ID="rfvINSURING_VALUE" Runat="server" Display="Dynamic" ControlToValidate="txtINSURING_VALUE"></asp:RequiredFieldValidator><br>
									<asp:RegularExpressionValidator id="revINSURING_VALUE" Runat="server" Display="Dynamic" ControlToValidate="txtINSURING_VALUE"></asp:RegularExpressionValidator>
								</td>
								<td class="midcolora" width="18%"><asp:Label ID="capDEDUCTIBLE" Runat="server"></asp:Label><span id="spnDEDUCTIBLE" class="mandatory">*</span></td>
								<td class="midcolora" width="32%"><asp:DropDownList ID="cmbDEDUCTIBLE" Runat="server"></asp:DropDownList>
									<br>
									<asp:RequiredFieldValidator ID="rfvDEDUCTIBLE" Runat="server" Display="Dynamic" ControlToValidate="cmbDEDUCTIBLE"></asp:RequiredFieldValidator>
									<asp:customvalidator id="csvDEDUCTIBLE" Runat="server" ControlToValidate="cmbDEDUCTIBLE" display="Dynamic"
										ClientValidationFunction="CheckDeductible"></asp:customvalidator>
								</td>
							</tr>
							<!--libality and medical payments added For Itrack issue #6710-->
							 	<tr>
							   <td class="midcolora" width="18%"><asp:Label ID="capLIABILITY" Runat="server"></asp:Label>
							         <asp:CheckBox id="chkLIABILITY" checked="true" runat="server" onclick="Libality();"> </asp:CheckBox></td>							         
							   <td class="midcolora" width="32%"><asp:TextBox ID="txtLIABILITY_LIMIT" Runat="server" ReadOnly="True" maxlength="10" size="22" ></asp:TextBox></td>
							   <td class="midcolora" width="18%"><asp:Label ID="capMEDICAL_PAYMENTS" Runat="server"></asp:Label>
							         <asp:CheckBox id="chkMEDICAL_PAYMENTS" checked="true" runat="server" onclick="MedicalPayment();"></asp:CheckBox></td>							         
							   <td class="midcolora" width="32%"><asp:TextBox ID="txtMEDICAL_PAYMENTS_LIMIT" ReadOnly="True" Runat="server" maxlength="10" size="22" ></asp:TextBox></td>						   							   
							</tr>							
							<tr>
							<tr>
								<TD class="headerEffectSystemParams" colSpan="4">Underwriting Info</TD>
							</tr>
							<tr>
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
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capRISK_DECL_BY_OTHER_COMP" runat="server">Has this risk been cancelled, declined or non-renewed by another company?</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbRISK_DECL_BY_OTHER_COMP" onfocus="SelectComboIndex('cmbRISK_DECL_BY_OTHER_COMP')"
										runat="server">
										<asp:ListItem></asp:ListItem>
										<asp:ListItem Value="Y">Yes</asp:ListItem>
										<asp:ListItem Value="N">No</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"><asp:label id="capUSED_IN_RACE_SPEED" runat="server">Used to participate in any race or speed contest?</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbUSED_IN_RACE_SPEED" onfocus="SelectComboIndex('cmbUSED_IN_RACE_SPEED')" runat="server">
										<asp:ListItem></asp:ListItem>
										<asp:ListItem Value="Y">Yes</asp:ListItem>
										<asp:ListItem Value="N">No</asp:ListItem>
									</asp:dropdownlist></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capVEHICLE_MODIFIED" runat="server">Has this vehicle been modified?</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbVEHICLE_MODIFIED" onfocus="SelectComboIndex('cmbVEHICLE_MODIFIED')" runat="server">
										<asp:ListItem></asp:ListItem>
										<asp:ListItem Value="Y">Yes</asp:ListItem>
										<asp:ListItem Value="N">No</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD class="midcolora" width="18%"><asp:label id="capDESC_RISK_DECL_BY_OTHER_COMP" runat="server">If yes, please explain</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtDESC_RISK_DECL_BY_OTHER_COMP" runat="server" maxlength="255" size="30" TextMode="MultiLine"
										Columns="40" Rows="3"></asp:textbox><br>
									<asp:customvalidator id="csvDESC_RISK_DECL_BY_OTHER_COMP" Runat="server" ErrorMessage="Please enter only 255 chars."
										ControlToValidate="txtDESC_RISK_DECL_BY_OTHER_COMP" display="Dynamic" ClientValidationFunction="txtDESC_RISK_DECL_BY_OTHER_COMP_Validate_255"></asp:customvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capUNIT_RENTED" runat="server">Is any Unit rented to others?</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbUNIT_RENTED" onfocus="SelectComboIndex('cmbUNIT_RENTED')" runat="server">
										<asp:ListItem></asp:ListItem>
										<asp:ListItem Value="Y">Yes</asp:ListItem>
										<asp:ListItem Value="N">No</asp:ListItem>
									</asp:dropdownlist></TD>
									
									<TD class="midcolora" width="18%"><asp:label id="capUNIT_OWNED_DEALERS" runat="server">Is any Unit owned by dealers?</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbUNIT_OWNED_DEALERS" onfocus="SelectComboIndex('cmbUNIT_OWNED_DEALERS')" runat="server">
										<asp:ListItem></asp:ListItem>
										<asp:ListItem Value="Y">Yes</asp:ListItem>
										<asp:ListItem Value="N">No</asp:ListItem>
									</asp:dropdownlist></TD>									
							</tr>
							<tr>
									<TD class="midcolora" width="18%"><asp:label id="capYOUTHFUL_OPERATOR_UNDER_25" runat="server">Any youthful operator(s) under age 25?</asp:label></TD>
									<TD class="midcolora" width="32%" colspan=3><asp:dropdownlist id="cmbYOUTHFUL_OPERATOR_UNDER_25" onfocus="SelectComboIndex('cmbYOUTHFUL_OPERATOR_UNDER_25')" runat="server">
										<asp:ListItem></asp:ListItem>
										<asp:ListItem Value="Y">Yes</asp:ListItem>
										<asp:ListItem Value="N">No</asp:ListItem>
									</asp:dropdownlist></TD>	
							</tr>
							<tr>
								<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" CausesValidation="False"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnCopy" runat="server" Text="Copy" CausesValidation="False"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" CausesValidation="False"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"
										CausesValidation="False"></cmsb:cmsbutton></td>
								<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
							<tr>
								<td colSpan="4">
									<INPUT id="hidFormSaved" type="hidden" name="hidFormSaved" runat="server"> <INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server">
									<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidREC_VEH_ID" type="hidden" value="0" name="hidREC_VEH_ID" runat="server">
									<INPUT id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server"> <INPUT id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server">
									<INPUT id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server"> <INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server">
									<INPUT id="hidLOOKUP_UNIQUE_ID" type="hidden" runat="server" NAME="hidLOOKUP_UNIQUE_ID">
									<INPUT id="hidDeductibleXml" type="hidden" runat="server" NAME="hidDeductibleXml">
									<INPUT id="hidVEHICLE_TYPE_NAME" type="hidden" runat="server" NAME="hidVEHICLE_TYPE_NAME" value="0">
									<INPUT id="hidCompany" type="hidden" runat="server" NAME="hidCompany"> <INPUT id="hidAPP_INCEPTION_DATE" type="hidden" runat="server" NAME="hidAPP_INCEPTION_DATE">
									<INPUT id="hidliability_limit" type="hidden" runat="server" NAME="hidliability_limit" value="">
									<INPUT id="hidMedical_limit" type="hidden" runat="server" NAME="hidMedical_limit" value="">
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
		<script>
		
			//Commented by Charles on 28-Jul-09 for Itrack 6176 (Note# 1)
			//RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidREC_VEH_ID').value, true);
			
			if(document.getElementById('hidVEHICLE_TYPE_NAME').value=='1')
			{
				document.getElementById('txtHORSE_POWER').focus();
				document.getElementById('hidVEHICLE_TYPE_NAME').value='0';
			}
			else if(document.getElementById('hidVEHICLE_TYPE_NAME').value=='2')
			{
				document.getElementById('cmbVEHICLE_TYPE_NAME').focus();
			}
			
			//Added by Charles on 28-Jul-09 for Itrack 6176 (Note# 1)
			if(document.getElementById('hidFormSaved').value=='5')
			{
				RemoveTab(2,this.parent);										
				RefreshWebGrid("5","1",true,true); 
		    }
		    else if (document.getElementById("hidFormSaved").value == "1")
			{
				RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidREC_VEH_ID').value);				
			}
			//Added till here
			
		</script>
	</BODY>
</HTML>



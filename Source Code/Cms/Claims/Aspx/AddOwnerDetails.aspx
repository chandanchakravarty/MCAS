<%@ Page language="c#" Codebehind="AddOwnerDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddOwnerDetails" validateRequest=false  %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>CLM_OWNER_INFORMATION</title>
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
			
			function EnableDisableDesc(cmbCombo,txtDesc,capDesc)
			{	
				if(!(cmbCombo)) return;
				if (cmbCombo.selectedIndex > -1)
				{	
					
					//Checking value only if item is selected
					if (cmbCombo.options[cmbCombo.selectedIndex].text == "Other")
					{
						//Disabling the description field, if No is selected
						txtDesc.style.display = "inline";
						capDesc.style.display = "inline";
						
						//Enabling the validators
						if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null)
						{
							document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled",true);
							
							if (document.getElementById("rfv" + txtDesc.id.substring(3)).isvalid == false)
								document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "inline";
						}
						
						//making the * sign visible					
						if (document.getElementById("spn" + txtDesc.id.substring(3)) != null)
						{
							document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "inline";
						}
						ChangeColor();
											
					}
					else
					{
					
						//Enabling the description field, if yes is selected
						txtDesc.style.display = "none";
						txtDesc.value = "";
						capDesc.style.display = "none";
						//lblNA.innerHTML="NA";
						
						//Disabling the validators					
						if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null)
						{
							document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled",false);
							document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "none";
						}
						
						//making the * sign invisible					
						if (document.getElementById("spn" + txtDesc.id.substring(3)) != null)
						{
							document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "none";
						}
					}
				}
				else
				{
					//Disabling the description field, if No is selected
					txtDesc.style.display = "none";
					capDesc.style.display = "none";
					//lblNA.innerHTML="NA";
					
					//Disabling the validators					
					if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null)
					{
						document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled",false);
						document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "none";
					}
					
					//making the * sign invisible					
					if (document.getElementById("spn" + txtDesc.id.substring(3)) != null)
					{
						document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "none";
					}
				}
			}
			
			function ResetTheForm()
			{
				
				ChangeColor();		
				DisableValidators();	
				if(document.getElementById("hidTYPE_OF_OWNER").value==<%=((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString()%>)
				{
					//When the page is in edit mode, we can reset the page without postback
					if(document.getElementById("hidOWNER_ID").value!="" && document.getElementById("hidOWNER_ID").value!="0" && document.getElementById("hidOWNER_ID").value!="NEW")
					{
						document.CLM_OWNER_INFORMATION.reset();
						//LoadInsuredName();
						/*if (document.getElementById("txtADDRESS1") != null)
							setTimeOut(document.getElementById("txtADDRESS1").focus(),2000);*/
						return false;					
					}
					else
					{
						document.location.href = document.location.href;				
						document.getElementById("hidRESET").value="1";				
					}
				}
				else
				{
					document.CLM_OWNER_INFORMATION.reset();						
					EnableDisableDesc(document.getElementById('cmbPRODUCTS_INSURED_IS'),document.getElementById('txtOTHER_DESCRIPTION'),document.getElementById('capOTHER_DESCRIPTION'))	
					/*if (document.getElementById("txtADDRESS1") != null)
						setTimeOut(document.getElementById("txtADDRESS1").focus(),2000);		*/
					return false;
				}
				
				
			}
			function LoadInsuredName()
			{
				
				combo = document.getElementById("cmbNAME");				
				if(combo==null || combo.selectedIndex==-1) return;
				//When the user chooses Yes, fetch data and put it in appropriate fields
				if(combo.options[combo.selectedIndex].value!='-1')
				{
					//We obtain the encoded string as follows
					//NAMED_INSURED_ID^NAMED_INSURED^ADDRESS1^ADDRESS2^CITY^STATE^COUNTRY^ZIP_CODE^PHONE
					encoded_string = new String(combo.options[combo.selectedIndex].value);										
					array = encoded_string.split('^');							
					//Traverse through the array and put the values in relavent fields										
					document.getElementById("hidNAME").value = array[1];										
					//document.getElementById("txtCONTACT_NAME").value = array[1];					
					document.getElementById("txtADDRESS1").value = array[2];
					document.getElementById("txtADDRESS2").value = array[3];
					document.getElementById("txtCITY").value = array[4];
					SelectComboOption("cmbSTATE",array[5]);						
					//SelectComboOption("cmbCONTACT_COUNTRY",array[6]);
					document.getElementById("txtZIP").value = array[6];
					document.getElementById("txtHOME_PHONE").value = array[7];									
					
				}
				else
				{
					document.getElementById("hidNAME").value = "";
					//document.getElementById("txtCONTACT_NAME").value = "";					
					document.getElementById("txtADDRESS1").value = "";
					document.getElementById("txtADDRESS2").value = "";
					document.getElementById("txtCITY").value = "";
					document.getElementById("cmbSTATE").selectedIndex = -1;
					//document.getElementById("cmbCONTACT_COUNTRY").selectedIndex = -1;					
					document.getElementById("txtZIP").value = "";
					document.getElementById("txtHOME_PHONE").value = "";
					
				}
				return false;
				
			}	

			function AddData()
			{
				
				if(document.getElementById("hidOWNER_ID").value=="" || document.getElementById("hidOWNER_ID").value=="0")
				{
					if(document.getElementById("txtNAME"))
						document.getElementById("txtNAME").value	=	"";
					document.getElementById("txtADDRESS1").value	=	"";
					document.getElementById("txtADDRESS2").value	=	"";
					document.getElementById("txtCITY").value	=	"";
					document.getElementById("cmbSTATE").selectedIndex = -1;
					document.getElementById("txtZIP").value	=	"";
					document.getElementById("txtHOME_PHONE").value	=	"";
					document.getElementById("txtWORK_PHONE").value	=	"";
					document.getElementById("txtEXTENSION").value	=	"";
					document.getElementById("txtMOBILE_PHONE").value	=	"";
					
					//if(document.getElementById("cmbDEFAULT_PHONE_TO_NOTICE"))
					//	document.getElementById("cmbDEFAULT_PHONE_TO_NOTICE").selectedIndex = 1;
						
					if(document.getElementById("cmbName"))
						document.getElementById("cmbName").selectedIndex = -1;
						
					if(document.getElementById("cmbPRODUCTS_INSURED_IS"))
						document.getElementById("cmbPRODUCTS_INSURED_IS").selectedIndex = -1;
						
					if(document.getElementById("txtOTHER_DESCRIPTION"))
						document.getElementById("txtOTHER_DESCRIPTION").value	=	"";
						
					if(document.getElementById("txtTYPE_OF_PRODUCT"))
						document.getElementById("txtTYPE_OF_PRODUCT").value	=	"";
						
					if(document.getElementById("txtWHERE_PRODUCT_SEEN"))
						document.getElementById("txtWHERE_PRODUCT_SEEN").value	=	"";
						
					if(document.getElementById("txtOTHER_LIABILITY"))
						document.getElementById("txtOTHER_LIABILITY").value	=	"";
						
					//if(document.getElementById("cmbVEHICLE_OWNER"))
						//document.getElementById("cmbVEHICLE_OWNER").selectedIndex = 3;
						
					document.getElementById("hidOWNER_ID").value	=	"NEW";					
				}
				
				if(document.getElementById("hidOWNER_ID").value!="NEW" && document.getElementById("hidOWNER_ID").value>0)				
				{
					document.getElementById("cmbVEHICLE_OWNER").disabled = true;
					document.getElementById("cmbNAME").disabled = true;
					document.getElementById("txtNAME").readOnly = true;
					document.getElementById("cmbDRIVER_TYPE").disabled = true;
					document.getElementById("txtNAME").readOnly = true;
				}
				if(document.getElementById("cmbVEHICLE_OWNER"))
				{
					if(document.getElementById("cmbVEHICLE_OWNER").disabled)		
					{
						/*if (document.getElementById("txtADDRESS1") != null)
							setTimeOut(document.getElementById("txtADDRESS1").focus(),2000);*/
					}
					else
					{	/*if (document.getElementById("cmbVEHICLE_OWNER") != null)
							setTimeOut(document.getElementById("cmbVEHICLE_OWNER").focus(),2000);*/
					}
				}
				else
				{	
					/*if (document.getElementById("txtNAME") != null)
						setTimeOut(document.getElementById("txtNAME").focus(),2000);*/
				}
				EnableDisableDesc(document.getElementById('cmbPRODUCTS_INSURED_IS'),document.getElementById('txtOTHER_DESCRIPTION'),document.getElementById('capOTHER_DESCRIPTION'))
				HideShowControls();
			}
			function HideShowControls()
			{
				
				document.getElementById("trINURED_VEHICLE").style.display = "none";				
				document.getElementById("trLIABILITY_OWNER1").style.display = "none";
				document.getElementById("trLIABILITY_OWNER2").style.display = "none";
				document.getElementById("trLIABILITY_OWNER_MANF").style.display = "none";
				document.getElementById("trLIABILITY_MANF").style.display = "none";				
				
				if(document.getElementById("hidTYPE_OF_OWNER").value == <%=(((int)enumTYPE_OF_OWNER.LIABILITY_TYPE_OWNER).ToString())%>)
				{
					document.getElementById("trLIABILITY_OWNER1").style.display = "inline";
					document.getElementById("trLIABILITY_OWNER2").style.display = "inline";
					document.getElementById("trLIABILITY_OWNER_MANF").style.display = "inline";
					MaketxtNameMandatory(false);
					EnableCmbName(false);
				}
				else if(document.getElementById("hidTYPE_OF_OWNER").value == <%=(((int)enumTYPE_OF_OWNER.LIABILITY_TYPE_MANUFACTURER).ToString())%>)
				{
					document.getElementById("trLIABILITY_MANF").style.display = "inline";					
					MaketxtNameMandatory(false);
					EnableCmbName(false);
				}
				else if(document.getElementById("hidTYPE_OF_OWNER").value == <%=(((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString())%>)
				{
					document.getElementById("trINURED_VEHICLE").style.display = "inline";	
					MaketxtNameMandatory(true);
					EnableCmbName(true);
				}				
				else
				{
					EnableCmbName(false);
					MaketxtNameMandatory(false);
				}
				
				
			}
			function EnableCmbName(flag)
			{		
				var ComboFlag = false;
				combo = document.getElementById("cmbVEHICLE_OWNER");
				/*NAMED_INSURED = 11752,      
				INSURED = 11753,      
				NOT_ON_POLICY = 11754  */
				if(combo!=null && combo.selectedIndex!=-1 && (combo.options[combo.selectedIndex].value=="11752" || combo.options[combo.selectedIndex].value=="11753" || combo.options[combo.selectedIndex].value=="14151"))
					ComboFlag = true;
				
				if(flag && ComboFlag)
				{					
					document.getElementById("rfvNAMES").setAttribute("enabled",true);					
					document.getElementById("rfvNAMES").setAttribute("isValid",true);
					document.getElementById("spnNAME").style.display = "inline";
					document.getElementById("cmbNAME").style.display = "inline";
					document.getElementById("txtNAME").style.display = "none";
					document.getElementById("rfvNAME").setAttribute("enabled",false);					
					document.getElementById("rfvNAME").setAttribute("isValid",false);
					document.getElementById("rfvNAME").style.display = "none";	
					if(combo.options[combo.selectedIndex].value=="11752" || combo.options[combo.selectedIndex].value=="14151")
						document.getElementById("trDRIVER_TYPE").style.display = "inline";
					else
						document.getElementById("trDRIVER_TYPE").style.display = "none";

				}
				else
				{
					document.getElementById("rfvNAMES").setAttribute("enabled",false);					
					document.getElementById("rfvNAMES").setAttribute("isValid",false);					
					document.getElementById("rfvNAMES").style.display = "none";
					//document.getElementById("spnNAME").style.display = "none";
					document.getElementById("cmbNAME").style.display = "none";
					document.getElementById("trDRIVER_TYPE").style.display = "none";
					
				}					
			}
			function MaketxtNameMandatory(flag)
			{
				if(flag)
				{
					
					document.getElementById("rfvNAME").setAttribute("enabled",true);					
					document.getElementById("rfvNAME").setAttribute("isValid",true);					
					document.getElementById("spnNAME").style.display = "inline";
				}
				else
				{
					document.getElementById("rfvNAME").setAttribute("enabled",false);					
					document.getElementById("rfvNAME").setAttribute("isValid",false);					
					document.getElementById("rfvNAME").style.display = "none";
					document.getElementById("spnNAME").style.display = "none";
				}
						
			}
			function Redirect()
			{
				//document.location.href = "InsuredVehicleIndex.aspx?&CLAIM_ID=" + document.getElementById("hidCLAIM_ID").value + "&";
				parent.parent.changeTab(0,2);
			}
			function Init()
			{
				AddData();
				if(document.getElementById("cmbVEHICLE_ID").options.length<1)
					document.getElementById("rfvVEHICLE_ID").innerHTML="";
				ChangeColor();
				ApplyColor();
			}
			//Done for Itrack Issue 6313 on 1 Oct 2009
			function vehicleCheck()
			{
			   var comboVEHICLE_ID = document.getElementById("cmbVEHICLE_ID");
			   var comboVEHICLE_OWNER= document.getElementById("cmbVEHICLE_OWNER");
			   if((comboVEHICLE_ID == null || comboVEHICLE_ID.selectedIndex == "-1"))
			   {
				alert('Please select Vehicle first.');
			   }
			}
				
		</script>
</HEAD>
	<BODY oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="CLM_OWNER_INFORMATION" method="post" runat="server">
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
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4">Assigned Vehicle</TD>
								</tr>								
								<tr>
									<TD class="midcolora"><asp:label id="capVEHICLE_ID" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" colspan="3">
										<asp:label id="lblVEHICLE_ID" runat="server"></asp:label>
										<asp:dropdownlist id='cmbVEHICLE_ID' runat='server'></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvVEHICLE_ID" ControlToValidate="cmbVEHICLE_ID" Display="Dynamic" Runat="server"></asp:requiredfieldvalidator></TD>									
								</tr>
								<tr>
									<td colspan="4" height="10"></td>
								</tr>
								<tr id="trINURED_VEHICLE" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capVEHICLE_OWNER" runat="server"></asp:label></TD>
									<%--Done for Itrack Issue 6313 on 1 Oct 2009--%>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbVEHICLE_OWNER" onfocus="SelectComboIndex('cmbVEHICLE_OWNER')" onchange ="vehicleCheck();" AutoPostBack="True"
											runat="server"></asp:dropdownlist>
									</TD>
									<td colspan="2" class="midcolora">&nbsp;</td>
								</tr>	
								<tr id="trDRIVER_TYPE">
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_TYPE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_TYPE" onfocus="SelectComboIndex('cmbDRIVER_TYPE')" AutoPostBack="True"
											runat="server"></asp:dropdownlist></td>
									<td class="midcolora" colspan="2"></td>
								</tr>							
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capNAME" runat="server"></asp:label><span class="mandatory" id="spnName" runat="server">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtNAME" runat="server" size="40" maxlength="50"></asp:textbox>
															<asp:dropdownlist id="cmbNAME" AutoPostBack="True" onfocus="SelectComboIndex('cmbNAME')" runat="server"></asp:dropdownlist>
									<BR>
										<asp:requiredfieldvalidator id="rfvNAME" runat="server" ControlToValidate="txtNAME" Display="Dynamic"></asp:requiredfieldvalidator>
										<asp:requiredfieldvalidator id="rfvNAMES" runat="server" ControlToValidate="cmbNAME" Display="Dynamic"></asp:requiredfieldvalidator>
										</TD>										
									<TD class="midcolora" colSpan="2"></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capADDRESS1" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtADDRESS1" runat="server" size="40" maxlength="50"></asp:textbox></TD>
									<TD class="midcolora" width="18%"><asp:label id="capADDRESS2" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtADDRESS2" runat="server" size="40" maxlength="50"></asp:textbox>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCITY" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCITY" runat="server" size="40" maxlength="50"></asp:textbox></TD>
									<TD class="midcolora" width="18%"><asp:label id="capSTATE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSTATE" onfocus="SelectComboIndex('cmbSTATE')" runat="server"></asp:dropdownlist></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capZIP" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtZIP" runat="server" maxlength="10" size="20"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revZIP" runat="server" ControlToValidate="txtZIP" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capCOUNTRY" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCOUNTRY" onfocus="SelectComboIndex('cmbCOUNTRY')" runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvCOUNTRY" runat="server" ControlToValidate="cmbCOUNTRY" Display="Dynamic"
											ErrorMessage="Please select COUNTRY."></asp:requiredfieldvalidator></TD>									
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capHOME_PHONE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtHOME_PHONE" runat="server" maxlength="15" size="20"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revHOME_PHONE" runat="server" ControlToValidate="txtHOME_PHONE" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator>
									</TD>
									<td class="midcolora" colspan="2"></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capWORK_PHONE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtWORK_PHONE" runat="server" maxlength="15" size="20"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revWORK_PHONE" runat="server" ControlToValidate="txtWORK_PHONE" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capEXTENSION" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEXTENSION" runat="server" maxlength="5" size="10"></asp:textbox><br>
									<asp:rangevalidator id="rngEXTENSION" ControlToValidate="txtEXTENSION" Display="Dynamic" Runat="server"
											Type="Integer" MinimumValue="1" MaximumValue="99999"></asp:rangevalidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMOBILE_PHONE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMOBILE_PHONE" runat="server" maxlength="15" size="20"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revMOBILE_PHONE" runat="server" ControlToValidate="txtMOBILE_PHONE" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator>
									</TD>
									<TD class="midcolora" colspan="2"></TD>
								</tr>
								<tr id="trLIABILITY_OWNER_MANF" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capDEFAULT_PHONE_TO_NOTICE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDEFAULT_PHONE_TO_NOTICE" onfocus="SelectComboIndex('cmbDEFAULT_PHONE_TO_NOTICE')"
											runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" colspan="2"></TD>
								</tr>
								<tr id="trLIABILITY_OWNER1" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capPRODUCTS_INSURED_IS" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPRODUCTS_INSURED_IS" onfocus="SelectComboIndex('cmbPRODUCTS_INSURED_IS')" onChange="javascript:EnableDisableDesc(this,document.getElementById('txtOTHER_DESCRIPTION'),document.getElementById('capOTHER_DESCRIPTION'))"
											runat="server"></asp:dropdownlist>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capOTHER_DESCRIPTION" runat="server"></asp:label><span class="mandatory" id="spnOTHER_DESCRIPTION">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtOTHER_DESCRIPTION" runat="server" maxlength="25" size="40"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvOTHER_DESCRIPTION" runat="server" ControlToValidate="txtOTHER_DESCRIPTION" Display="Dynamic"></asp:requiredfieldvalidator>
									</TD>
								</tr>
								<tr id="trLIABILITY_OWNER2" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capTYPE_OF_PRODUCT" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtTYPE_OF_PRODUCT" runat="server" maxlength="256" size="40"></asp:textbox>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capWHERE_PRODUCT_SEEN" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtWHERE_PRODUCT_SEEN" runat="server" maxlength="256" size="40"></asp:textbox>
									</TD>
								</tr>
								<tr id="trLIABILITY_MANF" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capOTHER_LIABILITY" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtOTHER_LIABILITY" runat="server" maxlength="256" size="40"></asp:textbox></TD>
									<TD class="midcolora" colspan="2"></TD>
								</tr>
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" causesvalidation="false"  Visible="False" ></cmsb:cmsbutton>
									</td>
									<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidLOB_ID" type="hidden" value="" name="hidLOB_ID" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidTYPE_OF_HOME" type="hidden" value="0" name="hidTYPE_OF_HOME" runat="server">			
			<INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			<INPUT id="hidRESET" type="hidden" value="0" name="hidRESET" runat="server">
			<input type="hidden" name="hidCUSTOMER_ID" id="hidCUSTOMER_ID" runat="server">
						<input type="hidden" name="hidPOLICY_ID" id="hidPOLICY_ID" runat="server">
						<input type="hidden" name="hidPOLICY_VERSION_ID" id="hidPOLICY_VERSION_ID" runat="server">
			<INPUT id="hidTYPE_OF_OWNER" type="hidden" value="0" name="hidTYPE_OF_OWNER" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidOWNER_ID" type="hidden" value="0" name="hidOWNER_ID" runat="server">
			<input type="hidden" name="hidNAME" id="hidNAME" runat="server">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidOWNER_ID').value,true);			
		</script>
	</BODY>
</HTML>

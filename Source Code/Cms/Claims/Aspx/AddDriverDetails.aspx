<%@ Page language="c#" Codebehind="AddDriverDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddDriverDetails" validateRequest=false  %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>CLM_DRIVER_INFORMATION</title>
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
			
			function ResetTheForm()
			{
				//Done for Itrack Issue 6327 on 16 Sept 09
				if(document.getElementById("hidDRIVER_ID").value!="NEW")
				{
					ChangeColor();		
					DisableValidators();
					document.getElementById("hidRESET").value="1";				
					document.location.href = document.location.href;
					return true;//Done for Itrack Issue 6327 on 16 Oct 09
				}
				else
				{
					ChangeColor();		
					DisableValidators();					
					document.CLM_DRIVER_INFORMATION.reset();
					document.getElementById("hidRESET").value="1";				
					document.location.href = document.location.href;
					return false;
				}
			}
			
			function ChkDATE_OF_BIRTH(objSource , objArgs)
			{
				var expdate=document.CLM_DRIVER_INFORMATION.txtDATE_OF_BIRTH.value;
				objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);				
			}			

			function AddData()
			{
				//alert(document.getElementById("hidDRIVER_ID").value);
				if(document.getElementById("hidDRIVER_ID").value=="" || document.getElementById("hidDRIVER_ID").value=="0")
				{
					document.getElementById("txtADDRESS1").value	=	"";
					document.getElementById("txtADDRESS2").value	=	"";
					document.getElementById("txtCITY").value	=	"";
					document.getElementById("cmbSTATE").selectedIndex = -1;
					document.getElementById("txtZIP").value	=	"";
					document.getElementById("txtHOME_PHONE").value	=	"";
					document.getElementById("txtWORK_PHONE").value	=	"";
					document.getElementById("txtEXTENSION").value	=	"";
					document.getElementById("txtMOBILE_PHONE").value	=	"";
					document.getElementById("txtRELATION_INSURED").value	=	"";
					//document.getElementById("txtOTHER_VEHICLE_INSURANCE").value	=	"";					
					document.getElementById("hidDRIVER_ID").value	=	"NEW";		
					if(document.getElementById("cmbName"))
						document.getElementById("cmbName").selectedIndex = -1;	
					//if(document.getElementById("cmbVEHICLE_OWNER"))
					//	document.getElementById("cmbVEHICLE_OWNER").selectedIndex = 3;		
					document.getElementById("btnDelete").setAttribute("disabled",true);
				}	
				else if(document.getElementById("hidDRIVER_ID").value=="NEW")
				{
					/*if(document.getElementById("cmbSAME_AS_OWNER")!=null && document.getElementById("cmbSAME_AS_OWNER").selectedIndex!=-1 && document.getElementById("cmbSAME_AS_OWNER").selectedIndex!=0)
					{
						//EnableDisableCombo(document.getElementById("cmbNAMED_INSURED"));
						EnableDisableCombo(document.getElementById("cmbDRIVERS"));
					}
					if(document.getElementById("cmbNAMED_INSURED")!=null && document.getElementById("cmbNAMED_INSURED").selectedIndex!=-1 && document.getElementById("cmbNAMED_INSURED").selectedIndex!=0)
					{
						EnableDisableCombo(document.getElementById("cmbSAME_AS_OWNER"));
						EnableDisableCombo(document.getElementById("cmbDRIVERS"));
					}
					if(document.getElementById("cmbDRIVERS")!=null && document.getElementById("cmbDRIVERS").selectedIndex!=-1 && document.getElementById("cmbDRIVERS").selectedIndex!=0)
					{
						EnableDisableCombo(document.getElementById("cmbNAMED_INSURED"));
						EnableDisableCombo(document.getElementById("cmbSAME_AS_OWNER"));
					}*/					
				}
				else //When in edit mode, Disable all combos
				{
					/*Commented by Asfa(24-July-2008) - iTrack #4538
					document.getElementById("cmbVEHICLE_OWNER").disabled = true;
					document.getElementById("cmbNAME").disabled = true;
					document.getElementById("cmbDRIVER_TYPE").disabled = true;
					document.getElementById("txtNAME").readOnly = true;
					*/
						//EnableDisableCombo(document.getElementById("cmbNAMED_INSURED"));
						//EnableDisableCombo(document.getElementById("cmbSAME_AS_OWNER"));
						//Commented as new Claim Enhancements
						//if(document.getElementById("trSAME_AS_OWNER"))
						//	document.getElementById("trSAME_AS_OWNER").style.display="none";
						//EnableDisableCombo(document.getElementById("cmbDRIVERS"));						
				}
				
				
				/*if(document.getElementById("cmbSAME_AS_OWNER") && !document.getElementById("cmbSAME_AS_OWNER").disabled )
					document.getElementById("cmbSAME_AS_OWNER").focus();
				else				
					document.getElementById("txtNAME").focus();*/
					
				HideShowControls();
			}
			function HideShowControls()
			{
				document.getElementById("trINURED_VEHICLE").style.display = "none";																								
				
				if(document.getElementById("hidTYPE_OF_OWNER").value == <%=(((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString())%>)
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
			function EnableDisableCombo(combo)
			{
				if(combo==null)	return;
				combo.selectedIndex = -1;
				combo.disabled = true;
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
				//EnableDisableNameAndAddress();
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
					alert(combo.options[combo.selectedIndex].value);
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
			
			//Done for Itrack Issue 6313 on 27 Aug 2009
			function vehicleCheck()
			{
			   var comboVEHICLE_ID = document.getElementById("cmbVEHICLE_ID");
			   var comboVEHICLE_OWNER= document.getElementById("cmbVEHICLE_OWNER");
			   //Done for Itrack Issue 6313 on 15 Aug 2009
			   //if((combo == null || combo.options[combo.selectedIndex].value == 0 || combo.options[combo.selectedIndex].value == "") && document.getElementById("cmbVEHICLE_OWNER").options[document.getElementById("cmbVEHICLE_OWNER").selectedIndex].value == "14151")
			  if((comboVEHICLE_ID == null || comboVEHICLE_ID.selectedIndex == "0" || comboVEHICLE_ID.selectedIndex == "-1") && comboVEHICLE_OWNER.selectedIndex == "3")
			  {
				alert('Please select Vehicle first.');
			  }
			}
			
			/*function EnableDisableNameAndAddress()
			{
				var combo = document.getElementById("cmbSAME_AS_OWNER");
				if (combo != null && combo.options[combo.selectedIndex] != -1)
				{
					if (combo.selectedIndex == 1)     //if is equal to "No"
					{
						document.getElementById("txtNAME").disabled = true;
						document.getElementById("txtADDRESS1").disabled = true;
						document.getElementById("txtADDRESS2").disabled = true;
						document.getElementById("txtCITY").disabled = true;
						document.getElementById("cmbSTATE").disabled = true;
						document.getElementById("cmbCOUNTRY").disabled = true;						
						document.getElementById("txtZIP").disabled = true;
						document.getElementById("txtHOME_PHONE").disabled = true;
					}
					else
					{
						document.getElementById("txtNAME").disabled = false;
						document.getElementById("txtADDRESS1").disabled = false;
						document.getElementById("txtADDRESS2").disabled = false;
						document.getElementById("txtCITY").disabled = false;
						document.getElementById("cmbSTATE").disabled = false;
						document.getElementById("cmbCOUNTRY").disabled = false;
						document.getElementById("txtZIP").disabled = false;
						document.getElementById("txtHOME_PHONE").disabled = false;
					}
				}
			}*/		
				
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="CLM_DRIVER_INFORMATION" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="99%" border="0">
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
									<TD class="midcolora"><asp:label id="capVEHICLE_ID" runat="server"></asp:label><span class="mandatory" id="spanVEHICLE_ID">*</span></TD>
									<TD class="midcolora" colspan="3">
										<asp:label id="lblVEHICLE_ID" runat="server"></asp:label>
										<asp:dropdownlist id='cmbVEHICLE_ID' runat='server'></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvVEHICLE_ID" ControlToValidate="cmbVEHICLE_ID" Display="Dynamic" Runat="server"></asp:requiredfieldvalidator></TD>									
								</tr>
								<tr>
									<td colspan="4" height="10"></td>
								</tr>
								<%--<tr id="trSAME_AS_OWNER" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capSAME_AS_OWNER" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSAME_AS_OWNER" onfocus="SelectComboIndex('cmbSAME_AS_OWNER')" AutoPostBack="True"
											runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" colSpan="2"></TD>
								</tr>--%>
								<%--<tr>
									<TD class="midcolora" width="18%"><asp:label id="capNAMED_INSURED" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbNAMED_INSURED" onfocus="SelectComboIndex('cmbNAMED_INSURED')" AutoPostBack="True"
											runat="server"></asp:dropdownlist>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVERS" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVERS" onfocus="SelectComboIndex('cmbDRIVERS')" AutoPostBack="True" runat="server"></asp:dropdownlist>
									</TD>
								</tr>--%>								
								<%--<tr>
									<TD class="midcolora" width="18%"><asp:label id="capNAME" runat="server"></asp:label><span class="mandatory" id="spnName" runat="server">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtNAME" runat="server" size="40" maxlength="50"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvNAME" runat="server" ControlToValidate="txtNAME" Display="Dynamic"></asp:requiredfieldvalidator>
									</TD>
									<TD class="midcolora" colSpan="2"></TD>
								</tr>--%>
								<tr id="trINURED_VEHICLE" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capVEHICLE_OWNER" runat="server"></asp:label></TD>
									<%--Done for Itrack Issue 6313 on 27 Aug 2009--%>
									<%--TD class="midcolora" width="32%"><asp:dropdownlist id="cmbVEHICLE_OWNER" onfocus="SelectComboIndex('cmbVEHICLE_OWNER')" AutoPostBack="True"
											runat="server"></asp:dropdownlist>--%>
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
									<TD class="midcolora" width="18%"><asp:label id="capNAME" runat="server"></asp:label><span class="mandatory" id="spnName" runat="server">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtNAME" runat="server" size="40" maxlength="50"></asp:textbox>
															<asp:dropdownlist id="cmbNAME" AutoPostBack="True" onfocus="SelectComboIndex('cmbNAME')" runat="server"></asp:dropdownlist>
									<BR>
										<asp:requiredfieldvalidator id="rfvNAME" runat="server" ControlToValidate="txtNAME" Display="Dynamic"></asp:requiredfieldvalidator>
										<asp:requiredfieldvalidator id="rfvNAMES" runat="server" ControlToValidate="cmbNAME" Display="Dynamic"></asp:requiredfieldvalidator>
										</TD>							
									<td class="midcolora" colspan="2"></td>												
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
									<TD class="midcolora" width="18%"><asp:label id="capCOUNTRY" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCOUNTRY" onfocus="SelectComboIndex('cmbCOUNTRY')" runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvCOUNTRY" runat="server" ControlToValidate="cmbCOUNTRY" Display="Dynamic"
											ErrorMessage="Please select COUNTRY."></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capZIP" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtZIP" runat="server" maxlength="10" size="12"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revZIP" runat="server" ControlToValidate="txtZIP" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator>
									</TD>
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
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capRELATION_INSURED" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtRELATION_INSURED" runat="server" maxlength="50" size="20"></asp:textbox>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capDATE_OF_BIRTH" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDATE_OF_BIRTH" runat="server" maxlength="10" size="12"></asp:textbox>
										<asp:hyperlink id="hlkDATE_OF_BIRTH" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgDATE_OF_BIRTH" runat="server" ImageUrl="../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
										</asp:hyperlink><br>
										<asp:regularexpressionvalidator id="revDATE_OF_BIRTH" runat="server" ControlToValidate="txtDATE_OF_BIRTH" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator>
										<asp:customvalidator id="csvDATE_OF_BIRTH" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic" ClientValidationFunction="ChkDATE_OF_BIRTH"
											Runat="server"></asp:customvalidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capLICENSE_NUMBER" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtLICENSE_NUMBER" runat="server" maxlength="15" size="40"></asp:textbox></TD>
									<TD class="midcolora" width="18%"><asp:label id="capLICENSE_STATE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLICENSE_STATE" onfocus="SelectComboIndex('cmbLICENSE_STATE')" runat="server"></asp:dropdownlist></TD>
								</tr>
								<tr>
									<TD class='midcolora' width='18%'>
									<asp:Label id="capDRIVERS_INJURY" runat="server"></asp:Label></TD>
									<TD class='midcolora' width='32%'>
									<asp:textbox id="txtDRIVERS_INJURY" runat='server' size='30' maxlength='10' Rows="5" Columns="36" TextMode="MultiLine" onkeypress="MaxLength(this,256);"></asp:textbox><br>
									</td>
									<td class="midcolora" colspan="2"></td>
								</tr>
								<%--<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPURPOSE_OF_USE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPURPOSE_OF_USE" onfocus="SelectComboIndex('cmbPURPOSE_OF_USE')" runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capUSED_WITH_PERMISSION" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbUSED_WITH_PERMISSION" onfocus="SelectComboIndex('cmbUSED_WITH_PERMISSION')"
											runat="server"></asp:dropdownlist></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDESCRIBE_DAMAGE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDESCRIBE_DAMAGE" runat="server" maxlength="100" size="40"></asp:textbox></TD>
									<TD class="midcolora" width="18%"><asp:label id="capESTIMATE_AMOUNT" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtESTIMATE_AMOUNT" runat="server" maxlength="10" size="15"></asp:textbox><br>
										<asp:rangevalidator id="rngESTIMATE_AMOUNT" ControlToValidate="txtESTIMATE_AMOUNT" Display="Dynamic"
											Runat="server" Type="Currency" MinimumValue="1" MaximumValue="9999999999"></asp:rangevalidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capOTHER_VEHICLE_INSURANCE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtOTHER_VEHICLE_INSURANCE" runat="server" maxlength="100" size="40"></asp:textbox></TD>
									<TD class="midcolora" colspan="2"></TD>
								</tr>--%>
								<tr>
									<td class="midcolora" colspan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
										<%--<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" causesvalidation="false"
											Visible="False"></cmsb:cmsbutton>--%>
										<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text='ActivateDeactivate'></cmsb:cmsbutton><%--Added for Itrack Issue 6053 on 8 Sept 2009--%>
									</td>
									
									<td class="midcolorr" colspan="2">
									 <cmsb:cmsbutton class="clsButton" id='btnDelete' runat="server" Text='Delete'></cmsb:cmsbutton><%--Added for Itrack Issue 6053 on 8 Sept 2009--%>
									 <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
									</td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidLOB_ID" type="hidden" value="" name="hidLOB_ID" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			<INPUT id="hidTYPE_OF_OWNER" type="hidden" value="0" name="hidTYPE_OF_OWNER" runat="server">
			<input type="hidden" name="hidNAME" id="hidNAME" runat="server">
			<INPUT id="hidRESET" type="hidden" value="0" name="hidRESET" runat="server"> <input type="hidden" name="hidCUSTOMER_ID" id="hidCUSTOMER_ID" runat="server">
			<input type="hidden" name="hidPOLICY_ID" id="hidPOLICY_ID" runat="server"> <input type="hidden" name="hidPOLICY_VERSION_ID" id="hidPOLICY_VERSION_ID" runat="server">
			<INPUT id="hidTYPE_OF_DRIVER" type="hidden" value="0" name="hidTYPE_OF_DRIVER" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidDRIVER_ID" type="hidden" value="0" name="hidDRIVER_ID" runat="server">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidDRIVER_ID').value,true);			
		</script>
	</BODY>
</HTML>

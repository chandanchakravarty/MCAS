<%@ Page language="c#" Codebehind="AddPropertyDamaged.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddPropertyDamaged" validateRequest=false  %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>CLM_PROPERTY_DAMAGED</title>
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
			
//			function ShowLookUpWindow()
//			{
//				if(document.getElementById('txtVIN')!=null)
//				{			
//					var vin = document.getElementById('txtVIN').value;					
//					vin = SearchAndReplace(vin,"&","^");
//					ShowPopup('/cms/application/Aspx/AddVINMasterPopup.aspx?CalledFrom=PPA&VIN='+vin,'VehicleInformation', 400, 200);
//					return;
//				}
//			}
			
			function Init()
			{
				
				setTab();					
								
				if(document.getElementById("hidOldData").value=="" || document.getElementById("hidOldData").value=="0")
					AddData();
				
				
				ShowHideFields();				
				cmbOTHER_INSURANCE_Change();			
				//document.getElementById("txtESTIMATE_AMOUNT").value = formatCurrencyWithCents(document.getElementById("txtESTIMATE_AMOUNT").value);	
				ApplyColor();
				ChangeColor();

					
				
				
			}

			function setTab() {
			    var tab = document.getElementById('hidtext').value;
				if(document.getElementById('hidPROPERTY_DAMAGED_ID').value != "0")
				{
					Url="PartyIndex.aspx?";
					DrawTab(2,this.parent,tab,Url);
				}
				
				//Added by Sibin for Itrack Issue 5055 on 19 Nov 08
				else
				 {
					Url="PartyIndex.aspx?";
					RemoveTab(2,this.parent,tab,Url);
				 }
			}
			
			function ConvertToUpper(e,r)
			{
				if (e.keyCode > 96 && e.keyCode < 123)
				r.value = r.value.toUpperCase();
			}
			
			
			function ValidateLength(objSource , objArgs)
			{
				if(document.getElementById('txtDESCRIPTION').value.length>256)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}
			function ValidateDescLength(objSource , objArgs)
			{
				if(document.getElementById('txtPARTY_TYPE_DESC').value.length>300)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}
			
			
			function ShowHideFields()
			{
				combo = document.getElementById("cmbDAMAGED_ANOTHER_VEHICLE");
				if (combo.selectedIndex == -1 || combo.selectedIndex == 0) ///if nothing is chosen or No is selected, hide the fields
				{					
					//document.getElementById("tbHome").style.display="none";					
					document.getElementById("tbVehicle").style.display="none";					
					document.getElementById("tbOther").style.display="none";										
					document.getElementById("capPROP_DAMAGED_TYPE").style.display="none";					
					document.getElementById("cmbPROP_DAMAGED_TYPE").style.display="none";
					document.getElementById("spnPROP_DAMAGED_TYPE").style.display = "none";
					document.getElementById("tddesc").style.display = "none";
					document.getElementById("tr1").style.display = "none";
					document.getElementById("tr2").style.display = "none";
														
					EnableValidator("rfvPROP_DAMAGED_TYPE",false);	
					EnableValidator("rngVEHICLE_YEAR",false);
					//EnableValidator("rfvADDRESS1",false);
					//EnableValidator("rfvCITY",false);
					//EnableValidator("rfvCOUNTRY",false);
					//EnableValidator("rfvSTATE",false);
					//EnableValidator("rfvZIP",false);
					EnableValidator("revZIP",false);
					EnableValidator("rfvDESCRIPTION",false);
					EnableValidator("csvDESCRIPTION",false);
					cmbPROP_DAMAGED_TYPE_Change(false);				
				}
				else
				{
					document.getElementById("capPROP_DAMAGED_TYPE").style.display="inline";					
					document.getElementById("cmbPROP_DAMAGED_TYPE").style.display="inline";
					document.getElementById("spnPROP_DAMAGED_TYPE").style.display = "inline";
												
					EnableValidator("rfvPROP_DAMAGED_TYPE",true);											
					cmbPROP_DAMAGED_TYPE_Change(true);
				}						
				ChangeColor();		
				return false;
			}
			function ResetTheForm()
			{
				DisableValidators();
				document.CLM_PROPERTY_DAMAGED.reset();
				Init();
				return false;				
			}

			function AddData()
			{
				document.getElementById("cmbDAMAGED_ANOTHER_VEHICLE").selectedIndex = 1;
				document.getElementById("cmbOTHER_INSURANCE").selectedIndex = -1;
				document.getElementById('txtESTIMATE_AMOUNT').value  = '';				
				document.getElementById('txtPARTY_TYPE_DESC').value  = '';
			}				
			function cmbPROP_DAMAGED_TYPE_Change(flag)
			{
				document.getElementById("tbAll").style.display="none";
				document.getElementById("tbVehicle").style.display="none";
				//document.getElementById("tbHome").style.display="none";
				//document.getElementById("tbOther").style.display="none";
				EnableValidator("rngVEHICLE_YEAR",false);
				//EnableValidator("rfvADDRESS1",false);
				//EnableValidator("rfvCITY",false);
				//EnableValidator("rfvCOUNTRY",false);
				//EnableValidator("rfvSTATE",false);
				//EnableValidator("rfvZIP",false);
				EnableValidator("revZIP",false);
				EnableValidator("rfvDESCRIPTION",false);
				EnableValidator("csvDESCRIPTION",false);				
				combo = document.getElementById("cmbPROP_DAMAGED_TYPE");	
				if((combo==null && combo.selectedIndex==-1) || flag==false)
					return true ;
	            document.getElementById("tddesc").style.display = "none";
	            document.getElementById("tr1").style.display = "none";
	            document.getElementById("tr2").style.display = "none";
	       
				combValue = combo.options[combo.selectedIndex].value;
				switch(combValue)
				{
					case "<%=Cms.BusinessLayer.BLClaims.ClsPropertyDamaged.PROP_DAMAGED_TYPE_VEHICLE.ToString()%>":
						document.getElementById("tbVehicle").style.display="inline";
						EnableValidator("rngVEHICLE_YEAR",true);
						document.getElementById("tddesc").style.display = "inline";
						document.getElementById("tr1").style.display = "none";
						document.getElementById("tr2").style.display = "none";
						
						document.getElementById("tbOther").style.display="inline";
						EnableValidator("rfvDESCRIPTION",true);
						EnableValidator("csvDESCRIPTION", true);
						
						break;
					case "<%=Cms.BusinessLayer.BLClaims.ClsPropertyDamaged.PROP_DAMAGED_TYPE_HOME.ToString()%>":
						//document.getElementById("tbHome").style.display="inline";
					    document.getElementById("tddesc").style.display = "inline";
					    document.getElementById("tr1").style.display = "inline";
					    document.getElementById("tr2").style.display = "inline";
					  
						document.getElementById("tbOther").style.display="inline";
						EnableValidator("rfvDESCRIPTION",true);
						EnableValidator("csvDESCRIPTION",true);
						/*EnableValidator("rfvADDRESS1",true);
						EnableValidator("rfvCITY",true);
						EnableValidator("rfvCOUNTRY",true);
						EnableValidator("rfvSTATE",true);
						EnableValidator("rfvZIP",true);*/
						EnableValidator("revZIP",true);
						break;
					case "<%=Cms.BusinessLayer.BLClaims.ClsPropertyDamaged.PROP_DAMAGED_TYPE_OTHER.ToString()%>":
					    document.getElementById("tbOther").style.display = "inline";
					    document.getElementById("tddesc").style.display = "inline";
					    document.getElementById("tr1").style.display = "none";
					    document.getElementById("tr2").style.display = "none";
					   
					    
						EnableValidator("rfvDESCRIPTION",true);
						EnableValidator("csvDESCRIPTION",true);
						break;
					default:
						break;
				}				
				ChangeColor();
				return false;
			}
			function cmbOTHER_INSURANCE_Change()
			{
				combo = document.getElementById("cmbOTHER_INSURANCE");				
				if(combo.selectedIndex==-1 || combo.selectedIndex==0) //hide the policy and agency row				
					document.getElementById("trAgency").style.display="none";
				else
					document.getElementById("trAgency").style.display="inline";
			}

		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Init(); ApplyColor();ChangeColor();">
		<FORM id="CLM_PROPERTY_DAMAGED" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR>
					<TD colspan="4">
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4"><asp:Label ID="capHeader" runat="server"></asp:Label>
								</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label>
								</td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capDAMAGED_ANOTHER_VEHICLE" runat="server"></asp:label><span clas*</span><br />
                                    <span class="mandatory"><asp:dropdownlist id="cmbDAMAGED_ANOTHER_VEHICLE" onfocus="SelectComboIndex('cmbDAMAGED_ANOTHER_VEHICLE')" runat="server">
									</asp:dropdownlist></TD>
								<TD class="midcolora" width="32%"><asp:label id="capPROP_DAMAGED_TYPE" runat="server"></asp:label><span class="mandatory" id="spnPROP_DAMAGED_TYPE">*<br />
                                    <asp:dropdownlist id="cmbPROP_DAMAGED_TYPE" onfocus="SelectComboIndex('cmbPROP_DAMAGED_TYPE')" runat="server"></asp:dropdownlist>
                                    </span>
                                    <br />
									<asp:requiredfieldvalidator id="rfvPROP_DAMAGED_TYPE" runat="server" Display="Dynamic" ControlToValidate="cmbPROP_DAMAGED_TYPE"></asp:requiredfieldvalidator>
                                   
								</TD>
								<TD class="midcolora" width="18%">
								    </td>
							</tr>
							<tr id="trPARTY_TYPE" runat="server" style ="DISPLAY:none">
								<TD class="midcolora" width="18%"><asp:label id="capPARTY_TYPE" runat="server"></asp:label>
                                    <br />
                                    <asp:dropdownlist id="cmbPARTY_TYPE" onfocus="SelectComboIndex('cmbPARTY_TYPE')" runat="server"></asp:dropdownlist></td>
								<TD class="midcolora" width="32%">								
                                    <br />
                                    <br />
								</TD>
								<TD class='midcolora' width='18%'>
                                    <br />
                                    <br />
                                </TD>
							</tr>
					<tbody id="tbAll" style="display:none">
						<tbody id="tbVehicle"  style="display:none">
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capVIN" runat="server"></asp:label>
                                    <br />
                                    <asp:textbox id="txtVIN" runat="server" size="25" maxlength="17"></asp:textbox></TD>
								<TD class="midcolora" width="32%">
								    <asp:label id="capMAKE" runat="server"></asp:label>
                                    <br />
                                    <asp:textbox id="txtMAKE" runat="server" size="32" maxlength="30"></asp:textbox>
								</TD>
					
					<td class="midcolora"><asp:label id="capBODY_TYPE" runat="server"></asp:label>
                        <br />
                        <asp:textbox id="txtBODY_TYPE" runat="server" size="32" maxlength="30"></asp:textbox>
				                </td>
				</TR>
				<tr>
					<TD class='midcolora' width='18%'>
						<asp:Label id="capPLATE_NUMBER" runat="server"></asp:Label>
                        
						<br />
                        
						<asp:textbox id='txtPLATE_NUMBER' runat='server' size='15' maxlength='10'></asp:textbox>
					</TD>
					<TD class='midcolora' width='32%'>
								    <asp:label id="capVEHICLE_YEAR" runat="server"  ></asp:label>
                                    <br />
                                    <asp:textbox id="txtVEHICLE_YEAR" runat="server"  size="4" maxlength="4"></asp:textbox>
                                    <br />
						<asp:rangevalidator id="rngVEHICLE_YEAR" ControlToValidate="txtVEHICLE_YEAR" Display="Dynamic" Runat="server"  Type="Integer"></asp:rangevalidator>
                        
					</TD>
					<TD class="midcolora" width="18%">
									<asp:label id="capMODEL" runat="server"></asp:label>
                                    <br />
                                    <asp:textbox id="txtMODEL" runat="server" size="32" maxlength="30"></asp:textbox>
                        </TD>
					   
                
				</tr>
				</tbody>
				<%--<tbody id="tbHome" style="display:none">--%>
				<tr id="tr1">
					<TD class="midcolora" width="18%">
						<asp:label id="capADDRESS1" runat="server">Address1</asp:label>
                        <br />
                        <asp:textbox id="txtADDRESS1" runat="server" maxlength="50" size="35"></asp:textbox>
						</TD>
					<TD class="midcolora" width="32%">
						<asp:label id="capADDRESS2" runat="server">Address2</asp:label>
                        <br />
                        <asp:textbox id="txtADDRESS2" runat="server" maxlength="50" size="35"></asp:textbox>
					</TD>
					<TD class="midcolora" width="18%"><asp:label id="capCITY" runat="server">City</asp:label>
                        <br />
                        <asp:textbox id="txtCITY" runat="server" maxlength="50" size="35"></asp:textbox>
						</TD>
				</tr>
				<tr id="tr2">
					<TD class="midcolora" width="18%">
					<asp:label id="capCOUNTRY" runat="server">Country</asp:label>
                        <br />
                        <asp:dropdownlist id="cmbCOUNTRY" onfocus="SelectComboIndex('cmbCOUNTRY')" runat="server"></asp:dropdownlist>
						</TD>
					<TD class="midcolora" width="32%">
						<%--<asp:RequiredFieldValidator ID="rfvCITY" Runat="server" Display="Dynamic" ControlToValidate="txtCITY"></asp:RequiredFieldValidator>--%>
					<asp:label id="capSTATE" runat="server" Enabled="false">State</asp:label>
                        <br />
                        <asp:dropdownlist id="cmbSTATE" onfocus="SelectComboIndex('cmbSTATE')" runat="server" Enabled="false"></asp:dropdownlist>
					</TD>
					<TD class="midcolora" width="18%">
						
						
                                   <asp:label id="capZIP" runat="server">Zip</asp:label>
                        <br />
						<asp:textbox id="txtZIP" runat="server" maxlength="10" size="12"></asp:textbox>
						<br />
						<asp:regularexpressionvalidator id="revZIP" runat="server" Display="Dynamic" ControlToValidate="txtZIP"></asp:regularexpressionvalidator>
						
						
						
                       
						</TD>
				</tr>
				<tr>
					<TD class="midcolora" id="tddesc"  style="width: 36%">
						
						
                      <asp:Label id="capDESCRIPTION" runat="server"></asp:Label>
                        <span class="mandatory" id="SpnDESCRIPTION">*</span><br />
                        
						<asp:textbox id="txtDESCRIPTION" runat='server' size='30' maxlength='10' Rows="5" Columns="36" TextMode="MultiLine" onkeypress="MaxLength(this,256);"></asp:textbox>
                        <br />
                        
						<asp:requiredfieldvalidator id="rfvDESCRIPTION" runat="server" ControlToValidate="txtDESCRIPTION" Display="Dynamic" ErrorMessage="Please select txtDESCRIPTION."></asp:requiredfieldvalidator>
						
						<asp:customvalidator Runat="server" id="csvDESCRIPTION" ErrorMessage="Error" Display="Dynamic" ControlToValidate="txtDESCRIPTION" ClientValidationFunction="ValidateLength"></asp:customvalidator>
						
						
                                   
						
						
								</TD>
								<TD class="midcolora" width="18%">
								<TD class="midcolora" width="18%">
				</tr>
				<%--</tbody>--%>
				<tbody id="Tbody1" style="display:none">
				<tr>
					<TD class='midcolora' width='18%'>
						<span class="mandatory" id="spnDESCRIPTION">
                        
						<asp:Label id="capPARTY_TYPE_DESC" runat="server"></asp:Label>
                                 
									<asp:textbox id="txtPARTY_TYPE_DESC" runat='server' size='30' maxlength='10' Rows="5" Columns="36" TextMode="MultiLine" onkeypress="MaxLength(this,300);"></asp:textbox>
                                    <br />
									<asp:customvalidator Runat="server" id="csvPARTY_TYPE_DESC"  Display="Dynamic" ControlToValidate="txtPARTY_TYPE_DESC" ClientValidationFunction="ValidateDescLength"></asp:customvalidator>
                        </TD>
					<TD class='midcolora' width='32%'>
						
						</TD>
				</tr>
				</tbody>
				<tbody id="tbOther" style="display:none">
				</tbody>
				<tr>
					<td class="midcolora" width="18%"><asp:label id="capOTHER_INSURANCE" runat="server"></asp:label>
                        <br />
                        <asp:dropdownlist id="cmbOTHER_INSURANCE" onfocus="SelectComboIndex('cmbOTHER_INSURANCE')" runat="server">
						</asp:dropdownlist>
					</td>
					<TD class="midcolora" width="32%">
                                    <asp:label id="capESTIMATE_AMOUNT" runat="server"></asp:label>
                        <br />
                        <asp:textbox id="txtESTIMATE_AMOUNT" CssClass="INPUTCURRENCY" runat="server" maxlength="10" size="15"></asp:textbox>
                        <br />
						<asp:RegularExpressionValidator ID="revESTIMATE_AMOUNT" ControlToValidate="txtESTIMATE_AMOUNT"  Runat="server" ></asp:RegularExpressionValidator>
						
						
                    </TD>
					<td class="midcolora"></td>
				</tr>
				<tr id="trAgency">
					<TD class='midcolora' width='18%'>
                       
						
						
						<asp:Label id="capAGENCY_NAME" runat="server"></asp:Label>
                                    <br />
						
						
						<asp:textbox id="txtAGENCY_NAME" runat='server' size='32' maxlength='30'></asp:textbox>
                                    
                       
					</TD>
					<TD class='midcolora' width='32%'>
						<asp:Label id="capPOLICY_NUMBER" runat="server"></asp:Label>
                        <br />
						<asp:textbox id="txtPOLICY_NUMBER" runat='server' size='12' maxlength='8'></asp:textbox>
                        
					</TD>				
					<TD class='midcolora' width='18%'>
                        
					</TD>
					</tbody>
				<tr>
					<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
					</td>
					<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
				</tr>
			</TABLE>
			</TD>
			</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidtext" type="hidden"  runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			<input type="hidden" id="hidCUSTOMER_ID" runat="server" name="hidCUSTOMER_ID">
			<input type="hidden" id="hidLOB_ID" runat="server" name="hidLOB_ID" value="">
			<input type="hidden" id="hidPOLICY_ID" runat="server" name="hidPOLICY_ID"> 
			<input type="hidden" id="hidPOLICY_VERSION_ID" runat="server" name="hidPOLICY_VERSION_ID">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> 
			<INPUT id="hidPROPERTY_DAMAGED_ID" type="hidden" value="0" name="hidPROPERTY_DAMAGED_ID" runat="server">
		</FORM>
		<script>
		
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidPROPERTY_DAMAGED_ID').value,true);			
			//		RemoveTab(1,top.frames[1]);
			//		var Url="AddPropertyDamaged.aspx?";
			//		DrawTab(1,top.frames[1],'Property Damaged',Url);
		</script>
	</BODY>
</HTML>

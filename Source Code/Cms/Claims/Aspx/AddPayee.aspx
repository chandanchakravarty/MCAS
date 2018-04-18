<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddPayee.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddPayee" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>Add Payee Details</title>
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
			function cmbPAYEE_ADDRESS_Change()
			{
				
				combo = document.getElementById('cmbPAYEE_ADDRESS');
				document.getElementById('hidPAYEE_ADDRESS').value ="";
				if(combo==null || combo.selectedIndex==-1)
					return;
				document.getElementById('hidPAYEE_ADDRESS').value = combo.options[combo.selectedIndex].value;								
				if(document.getElementById('hidPAYEE_ADDRESS').value!="-1")
					__doPostBack('cmbPAYEE_ADDRESS_Change',1);				
				else
				{
				
				    
					document.getElementById("txtADDRESS1").disabled = false;
					document.getElementById("txtADDRESS1").value='';
					document.getElementById("txtADDRESS2").disabled = false;
					document.getElementById("txtADDRESS2").value=''
					document.getElementById("txtCITY").disabled = false;
					document.getElementById("txtCITY").value='';
					document.getElementById("cmbSTATE").disabled = false;
					document.getElementById("cmbSTATE").value='';
					document.getElementById("cmbCOUNTRY").disabled = false;
					document.getElementById("txtZIP").disabled = false;
					document.getElementById("txtZIP").value='';
					document.getElementById("trNAME").style.display="inline";					
					/*document.getElementById("rfvFIRST_NAME").setAttribute("isValid","true");
					document.getElementById("rfvFIRST_NAME").setAttribute("enabled","true");*/
					EnableValidator("rfvFIRST_NAME",true);
					ChangeColor();
					ApplyColor();
				}
			}
			function cmbPAYEE_Change()
			{
			   
				combo = document.getElementById('cmbPAYEE');				
				
				if(combo==null || combo.selectedIndex==-1)
					return;
				
				document.getElementById("cmbPAYEE_ADDRESS").length=0;
				var ComboLength = combo.options.length;
				AddComboOption("cmbPAYEE_ADDRESS",0,"");
				for(var iCounter=0;iCounter<ComboLength;iCounter++)
				{
					//if(combo.options[iCounter].selected)
						AddComboOption("cmbPAYEE_ADDRESS",combo.options[iCounter].value,combo.options[iCounter].text);
				}
				AddComboOption("cmbPAYEE_ADDRESS","-1","Other");
				if(document.getElementById('hidPAYEE_ADDRESS').value!="")
					SelectComboOption("cmbPAYEE_ADDRESS",document.getElementById('hidPAYEE_ADDRESS').value);					
				return false;
			}
			function ValidateOrderDescLength(objSource , objArgs)
			{
				if(document.getElementById('txtTO_ORDER_DESC').value.length>250)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}
			function ValidateLength(objSource , objArgs)
			{
				if(document.getElementById('txtNARRATIVE').value.length>300)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}
			
			
			function ValidatePaymentMethod(objSource , objArgs)
			{
		
			   var PAYMENT_METHOD_TYPE= document.getElementById('cmbPAYMENT_METHOD').options[document.getElementById('cmbPAYMENT_METHOD').selectedIndex].value;
			   var PAYEE_HAS_BANK_INFO= document.getElementById('hidPAYEE_HAS_BANK_INFO').value;
			   
			   // IF PAYMENT TYPE IS BANK TRANFER AND SELECTED PAYEE IS NOT PROVIDED THE BANK INFO IN PARTY SCREEN THEN PROMP ERROR
			   
			   if(PAYMENT_METHOD_TYPE==14707 && PAYEE_HAS_BANK_INFO!="Y")			    
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}
			
			function formReset()
			{
				document.CLM_PAYEE.reset();
				//ShowHideAddressDetails();
				//cmbPAYEE_Change();
				DisableValidators();
				ChangeColor();
				return true;
			}
			
		
			
			
			function trim(str) {
			 return str;//.replace(/^\s+|\s+$/g,'');
			 } 
			
			function CheckPartiesForSPH()
			{		
			     
			      Page_ClientValidate();
					for(var j=0; j<document.getElementById('cmbPAYEE').options.length; j++)
					{

						if(document.getElementById("cmbPAYEE").options[j].selected == true && document.getElementById("cmbPayee").options[j].value != "")
						{
				  				var obj = document.getElementById("cmbPAYEE").options[j].value;
								val = obj.split("^");
								if(val[1].trim()=="10963") 				
								{
									//Do not Allow to make Checks of this Entity Type									
									alert('Party cannot be added as REQUIRED SPECIAL HANDLING is set to Yes for selected party.');
									return false;					
								}
						}
					
					}
					return true;
				
			}	
								
				
			function Init()
			{
				//If any payee has been added and we are adding new payee, no need to execute any functions
				if(<%=AnyPayeeAdded%> > 0 )
					return;
				//ShowHideAddressDetails();
				
				//cmbPAYEE_Change();
				//ShowHideSecondaryPayee();
				ChangeColor();
				ApplyColor();
				
			}
			
			
			
		</script>
        </HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="CLM_PAYEE" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="center" colSpan="4"><asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="3" align="center"><asp:Label ID="capHeader" runat="server"></asp:Label></TD>
								</tr>
								<tr>
									<td class="midcolorc" align="center" colSpan="3"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
									<tr id="TrRecoveryType" runat="server">
									<TD class="midcolora" colspan="3">
                                        <asp:label id="capRECOVERY_TYPE" runat="server"></asp:label>									
									    <br />
                                        
									
                                        
										<asp:DropdownList id="cmbRECOVERY_TYPE" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="cmbRECOVERY_TYPE_SelectedIndexChanged"></asp:DropdownList>
									    </TD>
								</tr>
								<tr>
									<TD class="midcolora"><asp:label id="capPAYEE" runat="server"></asp:label><span class="mandatory">*</span><br />
                                        <asp:label id="lblPAYEE" runat="server"></asp:label>									
									<SELECT id="cmbPAYEE"  runat="server" Multiple="True" name="D1" ></SELECT><br>
									<asp:requiredfieldvalidator id="rfvPAYEE" runat="server" ControlToValidate="cmbPAYEE" Display="Dynamic"></asp:requiredfieldvalidator>
									    </TD>
									<TD class="midcolora" width="26%" ><asp:label id="capPAYEE_ADDRESS" runat="server">Address of Payee</asp:label><span class="mandatory">*</span><br />
                                        
									
                                        
										<asp:DropdownList id="cmbPAYEE_ADDRESS" runat="server"></asp:DropdownList><br>
                                        <asp:RequiredFieldValidator ID="rfvPAYEE_ADDRESS" runat="server" 
                                            ControlToValidate="cmbPAYEE_ADDRESS" Display="Dynamic"></asp:RequiredFieldValidator>
									</TD>
									<TD class="midcolora" width="32%" >
                                        <asp:Label ID="capPAYMENT_METHOD" runat="server" 
                                            AssociatedControlID="rfvPAYMENT_METHOD"></asp:Label>
                                        <span class="mandatory">*</span><br />
                                        <asp:DropDownList ID="cmbPAYMENT_METHOD" runat="server" 
                                            onfocus="SelectComboIndex('cmbPAYMENT_METHOD')">
                                        </asp:DropDownList>
                                        <br />
                                        <asp:RequiredFieldValidator ID="rfvPAYMENT_METHOD" runat="server" 
                                            ControlToValidate="cmbPAYMENT_METHOD" Display="Dynamic"></asp:RequiredFieldValidator>
										<asp:customvalidator id="csvPAYMENT_METHOD" 
                                            ControlToValidate="cmbPAYMENT_METHOD" Display="Dynamic" ClientValidationFunction="ValidatePaymentMethod"
											Runat="server" ErrorMessage="TEST FAIL" ></asp:customvalidator>
									    </span></TD>
								</tr>
							
								<tr>
									<TD class="midcolora">
                                        <asp:label id="capAMOUNT" runat="server"></asp:label><span class="mandatory">*</span><br />
									   
										<asp:textbox id="txtAMOUNT" runat="server" CssClass="INPUTCURRENCY"   maxlength="14" size="20"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvAMOUNT" runat="server" ControlToValidate="txtAMOUNT" Display="Dynamic"></asp:requiredfieldvalidator>
										<asp:RegularExpressionValidator ID="revAMOUNT" Runat="server" ControlToValidate="txtAMOUNT" Display="Dynamic"></asp:RegularExpressionValidator>
									
									   
                                        </TD>
									<TD class="midcolora" width="32%" >
                                        <asp:label id="capINVOICE_NUMBER" 
                                            runat="server"></asp:label>
                                        <br />
									   
										<asp:TextBox ID="txtINVOICE_NUMBER" Runat="server" size="40" 
                                            maxlength="8" Width="113px" style="margin-top: 0px"></asp:TextBox>
									   
										<asp:RequiredFieldValidator ID="rfvINVOICE_NUMBER" Display="Dynamic" 
                                            ControlToValidate="txtINVOICE_NUMBER" Runat="server" Enabled="true"></asp:RequiredFieldValidator>
									   
									</TD>
										<TD class="midcolora" width="32%" >
                                        <asp:label id="capINVOICE_SERIAL_NUMBER" 
                                            runat="server"></asp:label>
                                            <br />
									   
										<asp:TextBox ID="txtINVOICE_SERIAL_NUMBER" Runat="server" 
                                            maxlength="8" Width="125px" Height="20px"></asp:TextBox>
									   
										<asp:RequiredFieldValidator ID="rfvINVOICE_SERIAL_NUMBER" Display="Dynamic" 
                                            ControlToValidate="txtINVOICE_SERIAL_NUMBER" Runat="server" Enabled="true"></asp:RequiredFieldValidator>
									   
                                            <br />
									   
                                    <asp:RegularExpressionValidator ID="revINVOICE_SERIAL_NUMBER" runat="server" Display="Dynamic"
                                                ControlToValidate="txtINVOICE_SERIAL_NUMBER"></asp:RegularExpressionValidator> 
									   
                                    </TD>
								</TR>
								<tr  runat="server">
									<TD class="midcolora" valign="top">
                                        <asp:label id="capINVOICE_DATE" 
                                            runat="server"></asp:label>
                                        <br />
									<asp:textbox id="txtINVOICE_DATE" 
                                            runat="server"   maxlength="10" size="12"></asp:textbox>
                                              <asp:HyperLink ID="hlkINVOICE_DATE" runat="server" CssClass="HotSpot"> <asp:Image ID="imgINVOICE_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                        <br />
                                    <asp:RegularExpressionValidator ID="revINVOICE_DATE" runat="server" Display="Dynamic"
                                                ControlToValidate="txtINVOICE_DATE"></asp:RegularExpressionValidator>
										<asp:RequiredFieldValidator ID="rfvINVOICE_DATE" Display="Dynamic" 
                                            ControlToValidate="txtINVOICE_DATE" Runat="server" Enabled="true"></asp:RequiredFieldValidator>
									</TD>
									<TD class="midcolora" width="32%" valign="top">
                                        
									    <asp:label id="capINVOICE_DUE_DATE" 
                                            runat="server"></asp:label>
                                        <br />
									    <asp:textbox id="txtINVOICE_DUE_DATE" 
                                            runat="server"   maxlength="10" size="12"></asp:textbox>
                                            
                                            <asp:HyperLink ID="hlkINVOICE_DUE_DATE" runat="server" CssClass="HotSpot"> <asp:Image ID="imgINVOICE_DUE_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                        <br />
                                    <asp:RegularExpressionValidator ID="revINVOICE_DUE_DATE" runat="server" Display="Dynamic"
                                                ControlToValidate="txtINVOICE_DUE_DATE"></asp:RegularExpressionValidator> 
									</TD>
									<TD class="midcolora" width="18%" valign="top">
                                        <asp:label id="capCHECK_NUMBER" runat="server">Check #</asp:label>
                                        <br />
                                        <asp:label id="lblCHECK_NUMBER" runat="server"></asp:label>
                                        <br />
                                    </TD>
								</tr>
								<tr>
									<TD class="midcolora" valign="top">
										<asp:label id="capADDRESS1" runat="server"></asp:label><span class="mandatory">*</span><br />
                                        <asp:textbox id="txtADDRESS1" runat="server" size="40" maxlength="50"></asp:textbox>
                                        <br />
										<asp:requiredfieldvalidator id="rfvADDRESS1" runat="server" ControlToValidate="txtADDRESS1" Display="Dynamic"></asp:requiredfieldvalidator></span>
										
									   
                                       
									</TD>
									<TD class="midcolora" width="32%" valign="top">
                                   
										<asp:label id="capADDRESS2" runat="server"></asp:label>
                                        <br />
                                        <asp:textbox id="txtADDRESS2" runat="server" size="40" maxlength="50"></asp:textbox>
                                        
                                        
									</TD>
									<TD class="midcolora" width="18%" valign="top">
                                   
										<asp:label id="capCITY" runat="server"></asp:label>
									    <br />
                                        <asp:textbox id="txtCITY" runat="server" size="20" maxlength="25"></asp:textbox>
									    <br />
                                        
										</TD>
								</tr>
								<tr>
									<TD class="midcolora">
                                       
                                        <asp:label id="capCOUNTRY" runat="server">Country</asp:label><span class="mandatory">*</span><br />
                                        <asp:dropdownlist id="cmbCOUNTRY" onfocus="SelectComboIndex('cmbCOUNTRY')" runat="server"></asp:dropdownlist>
                                        <br />
										<asp:requiredfieldvalidator id="rfvCOUNTRY" runat="server" Display="Dynamic" ControlToValidate="cmbCOUNTRY"></asp:requiredfieldvalidator>
									    
									   
									</TD>
									<TD class="midcolora"><asp:label id="capSTATE" runat="server"></asp:label><span class="mandatory">*</span><br />
                                        <asp:dropdownlist id="cmbSTATE" onfocus="SelectComboIndex('cmbSTATE')" runat="server"></asp:dropdownlist>
                                        <br />
										<asp:requiredfieldvalidator id="rfvSTATE" runat="server" Display="Dynamic" ControlToValidate="cmbSTATE"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capZIP" runat="server"></asp:label><span class="mandatory">*</span><br />
                                        <asp:textbox id="txtZIP" runat="server" 
                                            maxlength="16" size="12" Width="179px"></asp:textbox>
                                        <br />
										<asp:requiredfieldvalidator id="rfvZIP" runat="server" Display="Dynamic" ControlToValidate="txtZIP"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revZIP" runat="server" ControlToValidate="txtZIP" Display="Dynamic"></asp:regularexpressionvalidator>
									    </span></TD>
											
								</tr>
								<tr>
									<TD class="midcolora">
                                        <asp:label id="capPAYEE_TYPE" 
                                            runat="server"></asp:label>
                                        <br />
                                        <asp:label id="lblPAYEE_TYPE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%">										
										<asp:label id="capBANK_NUMBER" runat="server"></asp:label>
                                        <br />
                                        <asp:label id="lblBANK_NUMBER" runat="server"></asp:label>
										
									    </TD>
											
									<TD class="midcolora" width="18%">
										<asp:Label id="capBANK_BRANCH" runat="server"></asp:Label>
										<br>
                                        <asp:label id="lblBANK_BRANCH" runat="server"></asp:label>
									</TD>
									
								</tr>
								<tr  runat="server">
									<td class="midcolora">
										<asp:label id="capACCOUNT_NUMBER" runat="server"></asp:label>
                                        <br />
                                        <asp:label id="lblACCOUNT_NUMBER" runat="server"></asp:label>
										
									</td>
									<td class="midcolora" width="32%"> 
										<asp:label id="capACCOUNT_NAME" runat="server"></asp:label>
                                        <br />
                                        <asp:label id="lblACCOUNT_NAME" runat="server"></asp:label>
                                        
									</td>
									<td class="midcolora" width="32%">
                                        <asp:label id="capCHECK_STATUS" runat="server"></asp:label>
                                        <br />
                                        <asp:label id="lblCHECK_STATUS" runat="server"></asp:label>
									</td>
								</tr>
								
								<tr id="tr1">
									<TD class="midcolora">
                                        
                                        <asp:label id="capTO_ORDER_DESC" runat="server"></asp:label>
                                        <br />
                                        <asp:textbox  id="txtTO_ORDER_DESC" runat="server" TextMode="MultiLine"
											 size="40" Columns="50" Height="93px" ></asp:textbox>
                                        <br />
                                        
                                    </TD>
									<TD class="midcolora" width="32%">
									    
									   
										<asp:label id="capNARRATIVE" runat="server"></asp:label>
                                        <br />
                                        <asp:textbox onkeypress="MaxLength(this,300);" id="txtNARRATIVE" runat="server" TextMode="MultiLine"
											maxlength="300" size="40" Columns="50" Rows="5"></asp:textbox>
                                        <br />
										<asp:customvalidator id="csvNARRATIVE" ControlToValidate="txtNARRATIVE" Display="Dynamic" ClientValidationFunction="ValidateLength"
											Runat="server"></asp:customvalidator>
									   
                                            
										</TD>
									<TD class="midcolora" width="18%">
                                        
                                    </TD>
								</tr>
								
								<tr id="tr1">
									<TD class="midcolora">
                                       
									<asp:label id="capREIN_RECOVERY_NUMBER" 
                                            runat="server"></asp:label>
                                        <br />
                                        <asp:textbox id="txtREIN_RECOVERY_NUMBER" runat="server" 
                                            maxlength="256"  ></asp:textbox>
                                            
                                            
										
                                    </TD>
									<TD class="midcolora" width="32%">
                                        
                                   
										<asp:Label ID="capFIRST_NAME"  Runat="server" visible="false"></asp:Label>
                                        <span class="mandatory" id="spnFIRST_NAME" visible="false"></span><br />
                                        
                                        
                                      	<asp:TextBox ID="txtFIRST_NAME" Runat="server" size="40" maxlength="30" visible="false"></asp:TextBox>
                                        
                                        
                                        <br />
                                        
										<asp:RequiredFieldValidator ID="rfvFIRST_NAME" Display="Dynamic" ControlToValidate="txtFIRST_NAME" Runat="server" visible="false"></asp:RequiredFieldValidator>
									   
                                            
                                            
										</TD>
									
                                   <td class="midcolora" width="18%">
										<asp:Label ID="capLAST_NAME"  Runat="server" visible="false"></asp:Label>

                                        	<br />

                                        	<asp:TextBox ID="txtLAST_NAME" Runat="server"   size="40" maxlength="30" Visible ="false" ></asp:TextBox>
                                        
                                        
                                        
									</td>
								</tr>  
                                        
										


									
									
								
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" causesvalidation="false" Text="Reset"></cmsb:cmsbutton></td>
									<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
						</TABLE></TD></TR></TBODY></TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> 
			<INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			<INPUT id="hidPAYEE_ID" type="hidden" value="0" name="hidPAYEE_ID" runat="server">
			<INPUT id="hidDefaultValues" type="hidden" value="0" name="hidDefaultValues" runat="server">
			<INPUT id="hidACTIVITY_ID" type="hidden" value="0" name="hidACTIVITY_ID" runat="server">
			<INPUT id="hidACTIVITY_REASON" type="hidden" value="0" name="hidACTIVITY_REASON" runat="server">
			<INPUT id="hidEXPENSE_ID" type="hidden" value="0" name="hidEXPENSE_ID" runat="server">
			<INPUT id="hidPARTY_ID" type="hidden" value="0" name="hidPARTY_ID" runat="server">
			<INPUT id="hidPAYEE_ADDRESS" type="hidden" value="" name="hidPAYEE_ADDRESS" runat="server">
			<INPUT id="hidIS_PAYMENT_EXPENSE" type="hidden" value="" name="hidIS_PAYMENT_EXPENSE" runat="server">
			<INPUT id="hidPAYEE_HAS_BANK_INFO" type="hidden" value="" name="hidPAYEE_HAS_BANK_INFO" runat="server">
			
			
			
		</FORM>
		
	</BODY>
</HTML>

<%@ Page language="c#" Codebehind="AddExpensePayee.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.AddExpensePayee" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
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
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
			function ValidateNarrativeLength(objSource , objArgs)
			{
				if(document.getElementById('txtNARRATIVE').value.length>300)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}
			function ValidateOrderDescLength(objSource , objArgs)
			{
				if(document.getElementById('txtTO_ORDER_DESC').value.length>250)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}			
			function formReset()
			{
				document.CLM_PAYEE.reset();
				//ShowHideAddressDetails();
				DisableValidators();
				ChangeColor();
				return false;
			}
			//Added FOR Itrack Issue #5339
			function FillAddress()
			{
				
				combo = document.getElementById('cmbPARTY_ID');
				if(combo==null || combo.selectedIndex==-1)
					return;							
				if(combo.options[combo.selectedIndex].value!="-1")
					__doPostBack('NAME',1);				
				else
				{
				   ChangeColor();
					ApplyColor();
				}
				
		 }		
			
			function ValidateServiceLength(objSource , objArgs)
			{
				if(document.getElementById('txtSERVICE_DESCRIPTION').value.length>300)
					objArgs.IsValid = false;
				else
					objArgs.IsValid = true;
			}			
			
			function ChkINVOICE_DATE(objSource , objArgs)
			{				
				var expdate=document.getElementById("txtINVOICE_DATE").value;
				objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);
				
			}
			
			//function ShowHideAddressDetails()
			//{
				/*Lookup values for Payment Method
				11787>Check
				11788>EFT*/
			
				/*document.getElementById("tr4").style.display = "none";
				document.getElementById("tr5").style.display = "none";*/
				/*combo = document.getElementById("cmbPAYMENT_METHOD");
				if(combo.options[combo.selectedIndex].value=="11787") 
				{
					document.getElementById("tr1").style.display = "inline";
					document.getElementById("tr2").style.display = "inline";
					document.getElementById("tr3").style.display = "inline";					
					//Enable the validators
					
					document.getElementById("rfvADDRESS1").setAttribute('isValid',true); 				
					document.getElementById("rfvADDRESS1").setAttribute('enabled',true); 
					
					document.getElementById("rfvSTATE").setAttribute('isValid',true); 				
					document.getElementById("rfvSTATE").setAttribute('enabled',true); 
					
					document.getElementById("rfvZIP").setAttribute('isValid',true); 				
					document.getElementById("rfvZIP").setAttribute('enabled',true); 
					ChangeColor();*/
					
					/*
					if (document.getElementById("hidDefaultValues").value != 'undefined' && document.getElementById("hidDefaultValues").value != '0' )
					{
						strEncoded = hidDefaultValues.value;
						objArr = strEncoded.split('^');							
						document.getElementById("txtADDRESS1").value = objArr['ADDRESS1'];
						document.getElementById("txtADDRESS2").value = objArr['ADDRESS2'];
						document.getElementById("txtCITY").value = objArr['CITY'];
						document.getElementById("txtZIP").value = objArr['ZIP'];
						SelectComboOption("cmbSTATE",objArr['STATE']);						
						SelectComboOption("cmbCOUNTRY",objArr['COUNTRY']);
					}*/
					
				/*}
				else //Hide the controls,set their values to blank and disable their validators
				{
					document.getElementById("tr1").style.display = "none";
					document.getElementById("tr2").style.display = "none";
					document.getElementById("tr3").style.display = "none";
					//Show the bank and a/c fields only at payment screen*/
					/*if(document.getElementById("hidCALLED_FROM").value=='<%=CALLED_FROM_PAYMENT%>')
					{
						document.getElementById("tr4").style.display = "inline";
						document.getElementById("tr5").style.display = "inline";
					}*/
					
					/*document.getElementById("rfvADDRESS1").setAttribute('isValid',false); 
					document.getElementById("rfvADDRESS1").style.display='none'; 
					document.getElementById("rfvADDRESS1").setAttribute('enabled',false); 
					
					document.getElementById("rfvSTATE").setAttribute('isValid',false); 
					document.getElementById("rfvSTATE").style.display='none'; 
					document.getElementById("rfvSTATE").setAttribute('enabled',false); 
					
					document.getElementById("rfvZIP").setAttribute('isValid',false); 
					document.getElementById("rfvZIP").style.display='none'; 
					document.getElementById("rfvZIP").setAttribute('enabled',false); 

					//Set the values to blank*/
					/*document.getElementById("txtADDRESS1").value="";
					document.getElementById("txtADDRESS2").value="";
					document.getElementById("txtCITY").value="";
					document.getElementById("cmbCOUNTRY").selectedIndex = -1;
					document.getElementById("cmbSTATE").selectedIndex = -1;
					document.getElementById("txtZIP").value="";
					
					document.getElementById("txtADDRESS1").value = '';
					document.getElementById("txtADDRESS2").value = '';
					document.getElementById("txtCITY").value = '';
					document.getElementById("txtZIP").value = '';
					SelectComboOption("cmbSTATE",'');						
					SelectComboOption("cmbCOUNTRY",'');*/
				/*}
			
			}*/
			/*function ShowHidePayee()
			{
				if(document.getElementById("hidCALLED_FROM").value=='EXPENSE')
				{
					document.getElementById("lblPAYEE").style.display = "inline"
					document.getElementById("cmbPAYEE").style.display = "none"
				}
				else
				{
					document.getElementById("lblPAYEE").style.display = "none"
					document.getElementById("cmbPAYEE").style.display = "inline"					
				}
			}*/
			function CheckPartiesForSPH()
			{
				Page_ClientValidate();//Added by Sibin for Itrack Issue 5179 on 23 Dec 08
				var combo = document.getElementById("cmbPARTY_ID").options[document.getElementById("cmbPARTY_ID").selectedIndex].value;
				//Split
				val = combo.split("^");
								
				if(val[1]=="10963") 
				{
					//Do not Allow to make Checks of this Entity Type
					alert('Party cannot be added as REQUIRED SPECIAL HANDLING is set to Yes for selected party.');
					return false;					
				}
				else
				 return true;					
							
			}
			function Init()
			{
				//ShowHideAddressDetails();
				if(<%=AnyPayeeAdded%> > 0 )
					return;
				ChangeColor();
				ApplyColor();
			}
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Init();">
		<FORM id="CLM_PAYEE" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
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
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPARTY_ID" runat="server"></asp:label><%--<span class="mandatory">*</span>--%></TD>
									<TD class="midcolora" width="32%">									
									<SELECT id="cmbPARTY_ID" onfocus="SelectComboIndex('cmbPARTY_ID')" AutoPostBack="True"  runat="server" ></SELECT><br/>
									<asp:requiredfieldvalidator id="rfvPARTY_ID" runat="server" ControlToValidate="cmbPARTY_ID" Display="Dynamic"></asp:requiredfieldvalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capINVOICE_NUMBER" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtINVOICE_NUMBER" runat="server" TextMode="SingleLine" maxlength="50" size="40"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvINVOICE_NUMBER" runat="server" ControlToValidate="txtINVOICE_NUMBER" Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capINVOICE_DATE" runat="server"></asp:label><%--<span class="mandatory">*</span>--%></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtINVOICE_DATE" runat="server" TextMode="SingleLine" size="12" MaxLength="10"></asp:textbox><asp:hyperlink id="hlkINVOICE_DATE" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgINVOICE_DATE" runat="server" ImageUrl="../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
										</asp:hyperlink><br>
										<%--<asp:requiredfieldvalidator id="rfvINVOICE_DATE" runat="server" ControlToValidate="txtINVOICE_DATE" Display="Dynamic"></asp:requiredfieldvalidator>--%><asp:regularexpressionvalidator id="revINVOICE_DATE" runat="server" ControlToValidate="txtINVOICE_DATE" Display="Dynamic"></asp:regularexpressionvalidator>
										<asp:customvalidator id="csvINVOICE_DATE" ControlToValidate="txtINVOICE_DATE" Display="Dynamic" ClientValidationFunction="ChkINVOICE_DATE"
											Runat="server"></asp:customvalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capSERVICE_TYPE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSERVICE_TYPE" onfocus="SelectComboIndex('cmbSERVICE_TYPE')" runat="server"></asp:dropdownlist><br>
										<%--<asp:requiredfieldvalidator id="rfvSERVICE_TYPE" runat="server" ControlToValidate="cmbSERVICE_TYPE" Display="Dynamic"></asp:requiredfieldvalidator>--%></TD>
								</tr>
								<%--<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPAYEE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblPAYEE" runat="server"></asp:label><%--<asp:dropdownlist id="cmbPAYEE" onfocus="SelectComboIndex('cmbPAYEE')" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capREFERENCE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblREFERENCE" runat="server"></asp:label></TD>
								</tr>--%>
								<%--<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPAYMENT_METHOD" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPAYMENT_METHOD" onfocus="SelectComboIndex('cmbPAYMENT_METHOD')" runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvPAYMENT_METHOD" runat="server" ControlToValidate="cmbPAYMENT_METHOD" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="50%" colspan="2"></TD>
								</tr>--%>
								<tr id="tr1">
									<TD class="midcolora" width="18%"><asp:label id="capADDRESS1" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtADDRESS1" runat="server" size="40" maxlength="50"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvADDRESS1" runat="server" ControlToValidate="txtADDRESS1" Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capADDRESS2" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtADDRESS2" runat="server" size="40" maxlength="50"></asp:textbox>
									</TD>
								</tr>
								<tr id="tr2">
									<TD class="midcolora" width="18%"><asp:label id="capCITY" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCITY" runat="server" size="20" maxlength="25"></asp:textbox></TD>
									<TD class="midcolora" width="18%"><asp:label id="capCOUNTRY" runat="server">Country</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCOUNTRY" onfocus="SelectComboIndex('cmbCOUNTRY')" runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvCOUNTRY" runat="server" Display="Dynamic" ControlToValidate="cmbCOUNTRY"></asp:requiredfieldvalidator>
									</TD>
								</tr>
								<tr id="tr3">
									<TD class="midcolora" width="18%"><asp:label id="capSTATE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSTATE" onfocus="SelectComboIndex('cmbSTATE')" runat="server"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvSTATE" runat="server" Display="Dynamic" ControlToValidate="cmbSTATE"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capZIP" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtZIP" runat="server" maxlength="10" size="12"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvZIP" runat="server" Display="Dynamic" ControlToValidate="txtZIP"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revZIP" runat="server" ControlToValidate="txtZIP" Display="Dynamic"></asp:regularexpressionvalidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capNARRATIVE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox onkeypress="MaxLength(this,300);" id="txtNARRATIVE" runat="server" TextMode="MultiLine"
											maxlength="300" size="40" Columns="50" Rows="5"></asp:textbox><br>
										<%--<asp:requiredfieldvalidator id="rfvNARRATIVE" runat="server" ControlToValidate="txtNARRATIVE" Display="Dynamic"></asp:requiredfieldvalidator>--%>
										<asp:customvalidator id="csvNARRATIVE" ControlToValidate="txtNARRATIVE" Display="Dynamic" ClientValidationFunction="ValidateNarrativeLength"
											Runat="server"></asp:customvalidator></TD>
											<%--<td class="midcolora"> &nbsp; </td>
											<td class="midcolora"> &nbsp; </td>--%>
									<TD class="midcolora" width="18%"><asp:label id="capTO_ORDER_DESC" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox onkeypress="MaxLength(this,250);" id="txtTO_ORDER_DESC" runat="server" TextMode="MultiLine"
											maxlength="300" size="40" Columns="50" Rows="5"></asp:textbox><br>										
										<asp:customvalidator id="csvTO_ORDER_DESC" ControlToValidate="txtTO_ORDER_DESC" Display="Dynamic" ClientValidationFunction="ValidateOrderDescLength"
											Runat="server"></asp:customvalidator></TD>
								</tr>
								<tr>									
									<TD class="midcolora" width="18%"><asp:label id="capAMOUNT" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtAMOUNT" CssClass="INPUTCURRENCY" runat="server" MaxLength="14" size="20"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvAMOUNT" runat="server" ControlToValidate="txtAMOUNT" Display="Dynamic"></asp:requiredfieldvalidator>
										<asp:RegularExpressionValidator ID="revAMOUNT" Runat="server" ControlToValidate="txtAMOUNT" Display="Dynamic"></asp:RegularExpressionValidator>
										<asp:RangeValidator ID="rngAMOUNT" MinimumValue="1" MaximumValue="99999999999999" Type="Currency" Runat="server" Enabled="False"
											Display="Dynamic" ControlToValidate="txtAMOUNT"></asp:RangeValidator>
									</TD>
									<td class="midcolora" colspan="2">&nbsp;</td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCHECK_NUMBER" runat="server">Check #</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblCHECK_NUMBER" runat="server"></asp:label></TD>
									<TD class="midcolora" width="18%"><asp:label id="capCHECK_STATUS" runat="server">Status</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblCHECK_STATUS" runat="server"></asp:label></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capSERVICE_DESCRIPTION" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox onkeypress="MaxLength(this,300);" id="txtSERVICE_DESCRIPTION" runat="server" TextMode="MultiLine"
											maxlength="300" size="40" Columns="50" Rows="5"></asp:textbox><br>
										<%--<asp:requiredfieldvalidator id="rfvSERVICE_DESCRIPTION" Enabled="False" runat="server" ControlToValidate="txtSERVICE_DESCRIPTION"
											Display="Dynamic"></asp:requiredfieldvalidator>--%>
										<asp:customvalidator id="csvSERVICE_DESCRIPTION" ControlToValidate="txtSERVICE_DESCRIPTION" Display="Dynamic"
											ClientValidationFunction="ValidateServiceLength" Runat="server"></asp:customvalidator></TD>
									<td class="midcolora" colspan="2">&nbsp;</td>
								</tr>
								<%--<tr id="tr4">
									<TD class="midcolora" width="18%"><asp:label id="capBANK_NAME" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblBANK_NAME" runat="server"></asp:label></TD>
									<TD class="midcolora" width="50%" colspan="2"></TD>
								</tr>
								<tr id="tr5">
									<TD class="midcolora" width="18%"><asp:label id="capACCOUNT_NUMBER" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblACCOUNT_NUMBER" runat="server"></asp:label></TD>
									<TD class="midcolora" width="18%"><asp:label id="capACCOUNT_NAME" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblACCOUNT_NAME" runat="server"></asp:label></TD>
								</tr>--%>
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" causesvalidation="false" Text="Reset"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			<INPUT id="hidPAYEE_ID" type="hidden" value="0" name="hidPAYEE_ID" runat="server">
			<INPUT id="hidDefaultValues" type="hidden" value="0" name="hidDefaultValues" runat="server">
			<INPUT id="hidACTIVITY_ID" type="hidden" value="0" name="hidACTIVITY_ID" runat="server">
			<INPUT id="hidEXPENSE_ID" type="hidden" value="0" name="hidEXPENSE_ID" runat="server">
			<INPUT id="hidPARTY_ID" type="hidden" value="0" name="hidPARTY_ID" runat="server">
			<INPUT id="hidCALLED_FROM" type="hidden" name="hidCALLED_FROM" runat="server">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidPAYEE_ID').value,true);			
		</script>
	</BODY>
</HTML>

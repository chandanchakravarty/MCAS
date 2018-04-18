<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Page language="c#" AutoEventWireup="false" CodeBehind="AddForm1099.aspx.cs" Inherits="Cms.Account.Aspx.AddForm1099" validateRequest="false" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>FORM 1099</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		function AddData()
		{
			ChangeColor();
			DisableValidators();
			document.getElementById('hidFORM_1099_ID').value	=	'New';
			
			if(document.getElementById('btnDelete'))
			document.getElementById('btnDelete').setAttribute('disabled',true);
			
			if(document.getElementById('btnGenrateForm1099'))
			document.getElementById('btnGenrateForm1099').setAttribute('disabled',true);
			
			if(document.getElementById('btnViewDetails'))
			document.getElementById('btnViewDetails').setAttribute('disabled',true);
			
			if(document.getElementById('btnFreeze1099'))
			document.getElementById('btnFreeze1099').setAttribute('disabled',true);
			//Added by Sibin on 19 Dec 08 to Encrypt/Decrypt Recipient Identification 
			RECIPIENT_IDENTIFICATION_change();
		}
		
		function populateXML()
		{
			if(document.getElementById('hidFormSaved').value == '0')
			{
				var tempXML;
				
				if(document.getElementById('hidOldData').value !="")
				{
					
					//document.getElementById('btnReset').style.display='none';
					tempXML = document.getElementById('hidOldData').value;
					//Enabling the activate deactivate button
					if(document.getElementById('btnDelete'))
					document.getElementById('btnDelete').setAttribute('disabled',false); 
					
					//Populating the controls with values from xml
					populateFormData(tempXML,FORM_1099);
					//Added by Sibin on 19 Dec 08 to Encrypt/Decrypt Recipient Identification 
					RECIPIENT_IDENTIFICATION_hide();
					
				}
				else
				{
					AddData();
				}
			}	
			//Added by Sibin on 19 Dec 08 to Encrypt/Decrypt Recipient Identification 
			else
			{
				if(document.getElementById('hidRECIPIENT_IDENTIFICATION').value != '')
					RECIPIENT_IDENTIFICATION_hide();
				else
					RECIPIENT_IDENTIFICATION_change();					
			
			}
			//Added till here
			
			return false;
		}
		function ShowForm1099()
		{
			var strCustomerId=document.getElementById('hidFORM_1099_ID').value;
			window.open("/Cms/Application/Aspx/DecPage.aspx?CALLEDFOR=FORM1099&CALLEDFROM=ACCOUNT&CUSTOMER_ID="+strCustomerId+"&CALLEDFORPRINT=CHK");	
			return false;
		}
		
	
		
		function Show1099CheckDetails()
		{
			var formId=document.getElementById('hidFORM_1099_ID').value;
			window.open("Check_Details_1099.aspx?FORM_1099_ID="+formId+"",'BRICS','resizable=yes,scrollbars=yes,left=150,top=50,width=800,height=500');
			return false;
		}
		
		//To reset Form Values
		/*function ResetForm()
	    {
			DisableValidators();
			document.getElementById('txtRECIPIENT_IDENTIFICATION').innerText = '';
			document.getElementById('txtRECIPIENT_NAME').innerText = '';
			document.getElementById('txtRECIPIENT_STREET_ADDRESS1').innerText = '';
			document.getElementById('cmbRECIPIENT_STATE').selectedIndex=-1;
			document.getElementById('txtRECIPIENT_STREET_ADDRESS2').innerText = '';
			document.getElementById('txtRECIPIENT_CITY').innerText = '';
			document.getElementById('txtRECIPIENT_ZIP').innerText = '';
			document.getElementById('txtRENTS').innerText = '';
			document.getElementById('txtROYALATIES').innerText = '';
			document.getElementById('txtOTHERINCOME').innerText = '';
			document.getElementById('txtFISHING_BOAT_PROCEEDS').innerText = '';
			document.getElementById('txtFEDERAL_INCOME_TAXWITHHELD').innerText = '';
			document.getElementById('txtPAYER_MADE_DIRECT_SALES').innerText = '';
			document.getElementById('txtCROP_INSURANCE_PROCEED').innerText = '';
			document.getElementById('txtEXCESS_GOLDEN_PARACHUTE_PAYMENTS').innerText = '';
			document.getElementById('txtGROSS_PROCEEDS_PAID_TO_AN_ATTORNEY').innerText = '';
			document.getElementById('txtSTATE_TAX_WITHHELD').innerText = '';
			document.getElementById('txtSTATE_PAYER_STATE_NO').innerText = ''; 
			document.getElementById('txtSUBSTITUTE_PAYMENTS').innerText = ''; 
			document.getElementById('txtNON_EMPLOYEMENT_COMPENSATION').innerText = ''; 
			document.getElementById('txtSTATE_INCOME').innerText = ''; 
			document.getElementById('txtMEDICAL_AND_HEALTH_CARE_PRODUCTS').innerText = ''; 
			return false;
	   }*/
	   //Formats the amount and convert 111 into 1.11
		function FormatAmount(txtAmount)
		{
			if (txtAmount.value != "")
			{
				amt = txtAmount.value;
				
				amt = ReplaceAll(amt,".","");
				
				if (amt.length == 1)
					amt = amt + "0";
				
				if ( ! isNaN(amt))
				{
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					txtAmount.value = InsertDecimal(amt);
				}
			}
		}
		function confirmCommit()
		{
			var confirmAction = confirm("Do you really want to Freeze this Form 1099?");
			if(confirmAction)
				return true;
			else
				return false;
		}	
		
		//Added by Sibin on 19 Dec 08 to Encrypt/Decrypt Recipient Identification 
		function RECIPIENT_IDENTIFICATION_change()
			{
				document.getElementById('txtRECIPIENT_IDENTIFICATION').value = '';
				document.getElementById('txtRECIPIENT_IDENTIFICATION').style.display = 'inline';
				document.getElementById("rfvRECIPIENT_IDENTIFICATION").setAttribute('enabled',true);
				document.getElementById("revRECIPIENT_IDENTIFICATION").setAttribute('enabled',true);
				if(document.getElementById("btnRECIPIENT_IDENTIFICATION").value == 'Edit')
					document.getElementById("btnRECIPIENT_IDENTIFICATION").value = 'Cancel';
				else if(document.getElementById("btnRECIPIENT_IDENTIFICATION").value == 'Cancel')
					RECIPIENT_IDENTIFICATION_hide();
				else
					document.getElementById("btnRECIPIENT_IDENTIFICATION").style.display = 'none';
					
			}
			
			function RECIPIENT_IDENTIFICATION_hide()
			{
				document.getElementById("btnRECIPIENT_IDENTIFICATION").style.display = 'inline';
				document.getElementById('txtRECIPIENT_IDENTIFICATION').value = '';
				document.getElementById('txtRECIPIENT_IDENTIFICATION').style.display = 'none';
				document.getElementById("rfvRECIPIENT_IDENTIFICATION").style.display='none';
				document.getElementById("rfvRECIPIENT_IDENTIFICATION").setAttribute('enabled',false);
				document.getElementById("revRECIPIENT_IDENTIFICATION").style.display='none';
				document.getElementById("revRECIPIENT_IDENTIFICATION").setAttribute('enabled',false);
				document.getElementById("btnRECIPIENT_IDENTIFICATION").value = 'Edit';
			}
			
		//Added till here
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();">
		<FORM id="FORM_1099" method="post" runat="server">
		 <P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td class="midcolorc" align="right" colSpan="4">
							<asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label>
						</td>
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
										<td class="midcolorc" align="right" colSpan="4">
											<asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label>
										</td>
									</tr>
									<TR>
										<TD class="midcolora" width="18%"><asp:label id="capRECIPIENT_IDENTIFICATION" runat="server"></asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%">
											<%--Added by Sibin on 19 Dec 08 to Encrypt/Decrypt Recipient Identification --%>
											<asp:label id="capRECIPIENT_IDENTIFICATION_HID" runat="server" size="15" maxlength="9"></asp:label>
											<input class="clsButton" id="btnRECIPIENT_IDENTIFICATION" text="Edit" onclick="RECIPIENT_IDENTIFICATION_change();" type="button"></input>
											<%--Added till here--%>
											<asp:textbox id="txtRECIPIENT_IDENTIFICATION" runat="server" maxlength="9" size="11" autocomplete = "Off"></asp:textbox><br>
											<asp:RequiredFieldValidator ID="rfvRECIPIENT_IDENTIFICATION" Runat="server" ControlToValidate="txtRECIPIENT_IDENTIFICATION"
												Display="Dynamic" ErrorMessage="Please enter Recipient Identification."></asp:RequiredFieldValidator>
												<asp:regularexpressionvalidator id="revRECIPIENT_IDENTIFICATION" runat="server" ControlToValidate="txtRECIPIENT_IDENTIFICATION" Display="Dynamic"
												ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator>
										</TD>
										<TD class="midcolora" width="18%"><asp:label id="capACCOUNT_NO" runat="server"></asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtACCOUNT_NO" runat="server" maxlength="30" size="20"></asp:textbox><BR>
										</TD>
									</TR>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capRECIPIENT_NAME" runat="server"></asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtRECIPIENT_NAME" runat="server" maxlength="100" size="35"></asp:textbox><br>
											<asp:RequiredFieldValidator ID="rfvRECIPIENT_NAME" Runat="server" ControlToValidate="txtRECIPIENT_NAME" Display="Dynamic" ErrorMessage="Please enter Recipient Name."></asp:RequiredFieldValidator></TD>
										<!--Added By Raghav For Itrack Issue #4797-->
										<TD class="midcolora" width="18%"><asp:label id="capPROCESSING_OPTION" runat="server"></asp:label></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbFED_SSN_1099"  runat="server" width="34%">																								
												<asp:ListItem></asp:ListItem>
												<asp:ListItem Value='F'>Federal - ID</asp:ListItem>
												<asp:ListItem Value='S'>SSN</asp:ListItem>	
											</asp:dropdownlist>
										</TD>
										<!--<TD class="midcolora" width="18%"><span class="mandatory"></span></TD>-->
										<!--<TD class="midcolora" width="32%"></TD>-->
										
									</tr>
									<TR>
										<TD class="midcolora" width="18%"><asp:label id="capRECIPIENT_STREET_ADDRESS1" runat="server"></asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtRECIPIENT_STREET_ADDRESS1" runat="server" maxlength="70" size="50"></asp:textbox><BR>
										</TD>
										<TD class="midcolora" width="18%"><asp:label id="capRECIPIENT_STREET_ADDRESS2" runat="server"></asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtRECIPIENT_STREET_ADDRESS2" runat="server" maxlength="70" size="50"></asp:textbox><BR>
										</TD>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capRECIPIENT_CITY" runat="server"></asp:label><span class="mandatory" id="spnRECIPIENT_CITY">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtRECIPIENT_CITY" runat="server" maxlength="40" size="30"></asp:textbox><BR>
											<asp:requiredfieldvalidator id="rfvRECIPIENT_CITY" runat="server" Display="Dynamic" ErrorMessage="Please enter Recipient City."
												ControlToValidate="txtRECIPIENT_CITY"></asp:requiredfieldvalidator></TD>
										
										
										<TD class="midcolora" width="18%"><asp:label id="capRECIPIENT_STATE" runat="server"></asp:label><span class="mandatory" id="spnRECIPIENT_STATE">*</span></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbRECIPIENT_STATE" onfocus="SelectComboIndex('cmbRECIPIENT_STATE')" runat="server">
												<asp:ListItem Value='0'></asp:ListItem>
											</asp:dropdownlist><BR>
											<asp:requiredfieldvalidator id="rfvRECIPIENT_STATE" runat="server" Display="Dynamic" ErrorMessage="Recipient state can't be blank"
												ControlToValidate="cmbRECIPIENT_STATE"></asp:requiredfieldvalidator></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capRECIPIENT_ZIP" runat="server"></asp:label><span class="mandatory" id="spnRECIPIENT_ZIP">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtRECIPIENT_ZIP" runat="server" maxlength="10" size="13"></asp:textbox><asp:hyperlink id="hlkRECIPIENT_ZIP" runat="server" CssClass="HotSpot">
												<asp:image id="imgRECIPIENT_ZIP" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
											</asp:hyperlink><BR>
											<asp:requiredfieldvalidator id="rfvRECIPIENT_ZIP" runat="server" Display="Dynamic" ErrorMessage="Please enter Recipient Zip."
												ControlToValidate="txtRECIPIENT_ZIP"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revRECIPIENT_ZIP" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
												ControlToValidate="txtRECIPIENT_ZIP"></asp:regularexpressionvalidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capRENTS" runat="server"></asp:label></TD>
										<td class="midcolora" width="32%"><asp:textbox class="inputCurrency" id="txtRENTS" runat="server" maxlength="10" size="12"  onblur="FormatAmount(this);"></asp:textbox>
											<asp:RegularExpressionValidator id="revRENTS" Runat="server" Display="Dynamic" ControlToValidate="txtRENTS"></asp:RegularExpressionValidator></td>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capROYALATIES" runat="server"></asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox class="inputCurrency" id="txtROYALATIES" runat="server" maxlength="10" size="12"  onblur="FormatAmount(this);"></asp:textbox>
											<asp:RegularExpressionValidator id="revROYALATIES" Runat="server" Display="Dynamic" ControlToValidate="txtROYALATIES"></asp:RegularExpressionValidator>
										</TD>
										<TD class="midcolora" width="18%"><asp:label id="capOTHERINCOME" runat="server"></asp:label></TD>
										<td class="midcolora" width="32%"><asp:textbox class="inputCurrency" id="txtOTHERINCOME" runat="server"  maxlength="10" size="12" onblur="FormatAmount(this);"></asp:textbox>
											<asp:RegularExpressionValidator id="revOTHERINCOME" Runat="server" Display="Dynamic" ControlToValidate="txtOTHERINCOME"></asp:RegularExpressionValidator></td>
									</tr>
									<!--New-->
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capFISHING_BOAT_PROCEEDS" runat="server"></asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox class="inputCurrency" id="txtFISHING_BOAT_PROCEEDS" runat="server" maxlength="10" size="12"  onblur="FormatAmount(this);"></asp:textbox>
										<asp:RegularExpressionValidator id="revFISHING_BOAT_PROCEEDS" Runat="server" Display="Dynamic" ControlToValidate="txtFISHING_BOAT_PROCEEDS"></asp:RegularExpressionValidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capFEDERAL_INCOME_TAXWITHHELD" runat="server"></asp:label></TD>
										<td class="midcolora" width="32%"><asp:textbox class="inputCurrency" id="txtFEDERAL_INCOME_TAXWITHHELD" runat="server" maxlength="10" size="12"  onblur="FormatAmount(this);"></asp:textbox>
											<asp:RegularExpressionValidator id="revFEDERAL_INCOME_TAXWITHHELD" Runat="server" Display="Dynamic" ControlToValidate="txtFEDERAL_INCOME_TAXWITHHELD"></asp:RegularExpressionValidator></td>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capMEDICAL_AND_HEALTH_CARE_PRODUCTS" runat="server"></asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox class="inputCurrency" id="txtMEDICAL_AND_HEALTH_CARE_PRODUCTS" runat="server" maxlength="10" size="12"  onblur="FormatAmount(this);"></asp:textbox>
											<asp:RegularExpressionValidator id="revMEDICAL_AND_HEALTH_CARE_PRODUCTS" Runat="server" Display="Dynamic" ControlToValidate="txtMEDICAL_AND_HEALTH_CARE_PRODUCTS"></asp:RegularExpressionValidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capNON_EMPLOYEMENT_COMPENSATION" runat="server"></asp:label></TD>
										<td class="midcolora" width="32%"><asp:textbox class="inputCurrency" id="txtNON_EMPLOYEMENT_COMPENSATION" runat="server" maxlength="10" size="12"  onblur="FormatAmount(this);"></asp:textbox>
											<asp:RegularExpressionValidator id="revNON_EMPLOYEMENT_COMPENSATION" Runat="server" Display="Dynamic" ControlToValidate="txtNON_EMPLOYEMENT_COMPENSATION"></asp:RegularExpressionValidator></td>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capSUBSTITUTE_PAYMENTS" runat="server"></asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox class="inputCurrency" id="txtSUBSTITUTE_PAYMENTS" runat="server" maxlength="10" size="12"  onblur="FormatAmount(this);"></asp:textbox>
											<asp:RegularExpressionValidator id="revSUBSTITUTE_PAYMENTS" Runat="server" Display="Dynamic" ControlToValidate="txtSUBSTITUTE_PAYMENTS"></asp:RegularExpressionValidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capPAYER_MADE_DIRECT_SALES" runat="server"></asp:label></TD>
										<td class="midcolora" width="32%"><asp:textbox class="inputCurrency" id="txtPAYER_MADE_DIRECT_SALES" runat="server" maxlength="10" size="12"  onblur="FormatAmount(this);"></asp:textbox>
											<asp:RegularExpressionValidator id="revPAYER_MADE_DIRECT_SALES" Runat="server" Display="Dynamic" ControlToValidate="txtPAYER_MADE_DIRECT_SALES"></asp:RegularExpressionValidator></td>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capCROP_INSURANCE_PROCEED" runat="server"></asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox class="inputCurrency" id="txtCROP_INSURANCE_PROCEED" runat="server" maxlength="10" size="12"  onblur="FormatAmount(this);"></asp:textbox>
											<asp:RegularExpressionValidator id="revCROP_INSURANCE_PROCEED" Runat="server" Display="Dynamic" ControlToValidate="txtCROP_INSURANCE_PROCEED"></asp:RegularExpressionValidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capEXCESS_GOLDEN_PARACHUTE_PAYMENTS" runat="server"></asp:label></TD>
										<td class="midcolora" width="32%"><asp:textbox class="inputCurrency" id="txtEXCESS_GOLDEN_PARACHUTE_PAYMENTS" runat="server" maxlength="10" size="12" onblur="FormatAmount(this);"></asp:textbox>
											<asp:RegularExpressionValidator id="revEXCESS_GOLDEN_PARACHUTE_PAYMENTS" Runat="server" Display="Dynamic" ControlToValidate="txtEXCESS_GOLDEN_PARACHUTE_PAYMENTS"></asp:RegularExpressionValidator></td>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capGROSS_PROCEEDS_PAID_TO_AN_ATTORNEY" runat="server"></asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox class="inputCurrency" id="txtGROSS_PROCEEDS_PAID_TO_AN_ATTORNEY" runat="server" maxlength="10" size="12"  onblur="FormatAmount(this);"></asp:textbox>
											<asp:RegularExpressionValidator id="revGROSS_PROCEEDS_PAID_TO_AN_ATTORNEY" Runat="server" Display="Dynamic" ControlToValidate="txtGROSS_PROCEEDS_PAID_TO_AN_ATTORNEY"></asp:RegularExpressionValidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capSTATE_TAX_WITHHELD" runat="server"></asp:label></TD>
										<td class="midcolora" width="32%"><asp:textbox class="inputCurrency" id="txtSTATE_TAX_WITHHELD" runat="server" maxlength="10" size="12"  onblur="FormatAmount(this);"></asp:textbox>
											<asp:RegularExpressionValidator id="revSTATE_TAX_WITHHELD" Runat="server" Display="Dynamic" ControlToValidate="txtSTATE_TAX_WITHHELD"></asp:RegularExpressionValidator></td>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capSTATE_PAYER_STATE_NO" runat="server"></asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtSTATE_PAYER_STATE_NO" runat="server" maxlength="10" size="12"></asp:textbox>
											<asp:RegularExpressionValidator id="revSTATE_PAYER_STATE_NO" Runat="server" Display="Dynamic" ControlToValidate="txtSTATE_PAYER_STATE_NO"></asp:RegularExpressionValidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capSTATE_INCOME" runat="server"></asp:label></TD>
										<td class="midcolora" width="32%"><asp:textbox class="inputCurrency" id="txtSTATE_INCOME" runat="server" maxlength="10" size="12"  onblur="FormatAmount(this);"></asp:textbox>
											<asp:RegularExpressionValidator id="revSTATE_INCOME" Runat="server" Display="Dynamic" ControlToValidate="txtSTATE_INCOME"></asp:RegularExpressionValidator></td>
									</tr>
									<tr>
										<td class="midcolora" width="50%" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnFreeze1099" runat="server" Text="Freeze 1099"></cmsb:cmsbutton></td>
											<td class="midcolorr" width="50%" colSpan="2">
											<cmsb:cmsbutton class="clsButton" id="btnViewDetails" runat="server" Text="View Details"></cmsb:cmsbutton>
											<cmsb:cmsbutton class="clsButton" id="btnGenrateForm1099"  runat="server" Text="Generate Form 1099"></cmsb:cmsbutton>
											<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
										</td>
									</tr>
								</TBODY>
							</TABLE>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> 
			<INPUT id="hidFORM_1099_ID" type="hidden" value="0" name="hidAPP_ID" runat="server">
			<INPUT id="hidRECIPIENT_IDENTIFICATION" type="hidden" name="hidRECIPIENT_IDENTIFICATION" runat="server"><%--Added by Sibin on 19 Dec 08 to Encrypt/Decrypt Recipient Identification --%>
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none;Z-INDEX: 101;POSITION: absolute">
			<iframe id="lookupLayerIFrame" style="DISPLAY: none;Z-INDEX: 1001;FILTER: alpha(opacity=0);BACKGROUND-COLOR: #000000"
				width="0" height="0" top="0px;" left="0px"></iframe>
		</div>
		<DIV id="lookupLayer" style="Z-INDEX: 101;VISIBILITY: hidden;POSITION: absolute" onmouseover="javascript:refreshLookupLayer();">
			<table class="TabRow" cellspacing="0" cellpadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b>Add LookUp</b></td>
					<td><p align="right"><a href="javascript:void(0)" onclick="javascript:hideLookupLayer();"><img src="/cms/cmsweb/images/cross.gif" border="0" alt="Close" WIDTH="16" HEIGHT="14"></a></p>
					</td>
				</tr>
				<tr class="SubTabRow">
					<td colspan="2">
						<span id="LookUpMsg"></span>
					</td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->
		<script>
		if (document.getElementById('hidFormSaved').value == "5")
		{
			document.getElementById('hidFormSaved').value = 1;
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidFORM_1099_ID').value,false);
			document.getElementById('hidFormSaved').value = 0;
		}
		else
		{
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidFORM_1099_ID').value,false);
		}
		</script>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</BODY>
</HTML>

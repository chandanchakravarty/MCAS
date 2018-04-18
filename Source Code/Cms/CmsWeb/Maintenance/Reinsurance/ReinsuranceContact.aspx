<%@ Page language="c#" Codebehind="ReinsuranceContact.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Reinsurance.ReinsuranceContact" ValidateRequest="false" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Reinsurance Contact</title>
		<meta content="False" name="vs_showGrid">
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
			function setURL()
			{
				window.parent.location.href = "../addreinsurer.aspx?REIN_COMAPANY_ID=" + document.getElementById("hidCompany_ID").value;
				//return false;
			}
			
			function CopyPhysicalAddress()
			{					
				if(document.getElementById('txtREIN_CONTACT_ADDRESS_1').value=="")
				document.getElementById('txtREIN_CONTACT_ADDRESS_1').value =document.getElementById('hidAddress1').value ;
				
				if(document.getElementById('txtREIN_CONTACT_ADDRESS_2').value=="")
				document.getElementById('txtREIN_CONTACT_ADDRESS_2').value =document.getElementById('hidAddress2').value ;
				
				if(document.getElementById('txtREIN_CONTACT_CITY').value=="")
				document.getElementById('txtREIN_CONTACT_CITY').value =document.getElementById('hidCity').value	;
				
				/*if(document.getElementById('cmbREIN_CONTACT_COUNTRY').value=="")
				document.getElementById('cmbREIN_CONTACT_COUNTRY').value =document.getElementById('hidCountry').value ;*/
				
				if(document.getElementById('txtREIN_CONTACT_COUNTRY').value=="")
				document.getElementById('txtREIN_CONTACT_COUNTRY').value =document.getElementById('hidCountry').value ;
				
				if(document.getElementById('txtREIN_CONTACT_STATE').value=="")
				document.getElementById('txtREIN_CONTACT_STATE').value =document.getElementById('hidState').value ;
				
				if(document.getElementById('txtREIN_CONTACT_ZIP').value=="")
				document.getElementById('txtREIN_CONTACT_ZIP').value =document.getElementById('hidZip').value	;
				
				if(document.getElementById('txtREIN_CONTACT_PHONE_1').value=="")
				document.getElementById('txtREIN_CONTACT_PHONE_1').value =document.getElementById('hidPhone1').value ;
				
				if(document.getElementById('txtREIN_CONTACT_PHONE_2').value=="")
				document.getElementById('txtREIN_CONTACT_PHONE_2').value =document.getElementById('hidPhone2').value ;
				
				if(document.getElementById('txtREIN_CONTACT_FAX').value=="")
				document.getElementById('txtREIN_CONTACT_FAX').value =document.getElementById('hidFax').value	;
			
				EnableValidator('rfvREIN_CONTACT_ADDRESS_1',false);
				ChangeColor();

				return false;
			}
			
			function CopyMailingAddress()
			{	 
				
				if(document.getElementById('txtM_REIN_CONTACT_ADDRESS_1').value=="")
				document.getElementById('txtM_REIN_CONTACT_ADDRESS_1').value =document.getElementById('hidM_Address1').value ;
				
				if(document.getElementById('txtM_REIN_CONTACT_ADDRESS_2').value=="")
				document.getElementById('txtM_REIN_CONTACT_ADDRESS_2').value =document.getElementById('hidM_Address2').value ;
				
				if(document.getElementById('txtM_REIN_CONTACT_CITY').value=="")
				document.getElementById('txtM_REIN_CONTACT_CITY').value =document.getElementById('hidM_City').value	;
				
				/*if(document.getElementById('cmbM_REIN_CONTACT_COUNTRY').value=="")
				document.getElementById('cmbM_REIN_CONTACT_COUNTRY').value =document.getElementById('hidM_Country').value ;*/
				
				if(document.getElementById('txtM_REIN_CONTACT_COUNTRY').value=="")
				document.getElementById('txtM_REIN_CONTACT_COUNTRY').value =document.getElementById('hidM_Country').value ;
								
				if(document.getElementById('txtM_REIN_CONTACT_STATE').value=="")
				document.getElementById('txtM_REIN_CONTACT_STATE').value =document.getElementById('hidM_State').value ;
				
				if(document.getElementById('txtM_REIN_CONTACT_ZIP').value=="")
				document.getElementById('txtM_REIN_CONTACT_ZIP').value =document.getElementById('hidM_Zip').value	;
			
				if(document.getElementById('txtM_REIN_CONTACT_PHONE_1').value=="")
				document.getElementById('txtM_REIN_CONTACT_PHONE_1').value =document.getElementById('hidM_Phone1').value	;
				
				if(document.getElementById('txtM_REIN_CONTACT_PHONE_2').value=="")
				document.getElementById('txtM_REIN_CONTACT_PHONE_2').value =document.getElementById('hidM_Phone2').value	;
			
			
				if(document.getElementById('txtREIN_CONTACT_FAX').value=="")
				document.getElementById('txtREIN_CONTACT_FAX').value =document.getElementById('hidM_Fax').value	;
			
				EnableValidator('rfvM_REIN_CONTACT_ADDRESS_1',false);

				ChangeColor();

				return false;
			}			
					
			function AddData()
			{
			ChangeColor();
			DisableValidators();
			//return;
			document.getElementById('hidREIN_CONTACT_ID').value	=	'New';
			document.getElementById('txtREIN_CONTACT_CODE').value  = '';
			document.getElementById('txtREIN_CONTACT_NAME').value = '';
			document.getElementById('txtREIN_CONTACT_POSITION').value  = '';
			document.getElementById('txtREIN_CONTACT_SALUTATION').value  = '';
			document.getElementById('txtREIN_CONTACT_ADDRESS_1').value  = '';
			document.getElementById('txtREIN_CONTACT_ADDRESS_2').value  = '';
			document.getElementById('txtREIN_CONTACT_CITY').value  = '';
			//document.getElementById('cmbREIN_CONTACT_COUNTRY').options.selectedIndex = -1;
			//document.getElementById('cmbREIN_CONTACT_STATE').options.selectedIndex = -1;
			document.getElementById('txtREIN_CONTACT_STATE').value  = '';
			document.getElementById('txtREIN_CONTACT_ZIP').value  = '';
			document.getElementById('txtREIN_CONTACT_PHONE_1').value  = '';
			document.getElementById('txtREIN_CONTACT_PHONE_2').value  = '';
			document.getElementById('txtREIN_CONTACT_EXT_1').value  = '';
			document.getElementById('txtREIN_CONTACT_EXT_2').value  = '';
			//document.getElementById('txtREIN_CONTACT_MOBILE').value  = '';
			//document.getElementById('txtREIN_CONTACT_FAX').value  = '';
			//document.getElementById('txtREIN_CONTACT_SPEED_DIAL').value  = '';
			
			document.getElementById('txtM_REIN_CONTACT_ADDRESS_1').value  = '';
			document.getElementById('txtM_REIN_CONTACT_ADDRESS_2').value  = '';
			document.getElementById('txtM_REIN_CONTACT_CITY').value  = '';
			//document.getElementById('cmbM_REIN_CONTACT_COUNTRY').options.selectedIndex = 0;
			//document.getElementById('cmbM_REIN_CONTACT_STATE').options.selectedIndex = 0;
			document.getElementById('txtM_REIN_CONTACT_STATE').value  = '';
			document.getElementById('txtM_REIN_CONTACT_ZIP').value  = '';
			document.getElementById('txtM_REIN_CONTACT_PHONE_1').value  = '';
			document.getElementById('txtM_REIN_CONTACT_PHONE_2').value  = '';
			document.getElementById('txtM_REIN_CONTACT_EXT_1').value  = '';
			document.getElementById('txtM_REIN_CONTACT_EXT_2').value  = '';
			
			document.getElementById('txtREIN_CONTACT_MOBILE').value  = '';
			document.getElementById('txtREIN_CONTACT_FAX').value  = '';
			document.getElementById('txtREIN_CONTACT_SPEED_DIAL').value  = '';
			document.getElementById('txtREIN_CONTACT_EMAIL_ADDRESS').value  = '';
			document.getElementById('txtREIN_CONTACT_CONTRACT_DESC').value  = '';
			document.getElementById('txtCOMMENTS').value  = '';
			
			//document.getElementById('txtREIN_CONTACT_WEBSITE').value  = '';
			//document.getElementById('cmbREIN_CONTACT_IS_BROKER').options.selectedIndex = 0;
			//document.getElementById('txtPRINCIPAL_CONTACT').value  = '';
			//document.getElementById('txtOTHER_CONTACT').value  = '';
			//document.getElementById('txtFEDERAL_ID_NUMBER').value  = '';
			//document.getElementById('txtROUTING_NUMBER').value  = '';
			//document.getElementById('txtTERMINATION_DATE').value  = '';
			//document.getElementById('txtEFFECTIVE_DATE').value  = '';
			//document.getElementById('txtDOMICILED_STATE').value  = '';
			//document.getElementById('txtTERMINATION_DATE').value  = '';
			//document.getElementById('cmbNAIC_CODE').options.selectedIndex = 0;
			//document.getElementById('txtTERMINATION_REASON').value  = '';
			if(document.getElementById('btnActivateDeactivate'))
				document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			
			}


			function populateXML()
			{
				//EnableValidator('revM_REIN_CONTACT_PHONE_1',true);
				var tempXML;
				tempXML=document.getElementById("hidOldData").value;
				//alert(document.getElementById("hidOldData").value);
				if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
				{				
					if(tempXML!="" && tempXML!="0")
					{
		
						populateFormData(tempXML,"MNT_REIN_CONTACT");
						//ChkCountry_physical();ChkCountry_mailing();						
					}
					else
					{
						AddData();
					}
				}
				//SetTab();
				
				return false;
			}
			
			function SetTab()
			{
				if(document.getElementById('hidOldData').value != "")
				{				
					Url="Reinsurance/ReinsuranceContactIndex.aspx?calledfrom=reinsurer&EntityType=Reinsurer&EntityId="+document.getElementById('hidREIN_CONTACT_ID').value + "&";
					DrawTab(2,top.frames[1],'Contacts',Url);
					
					Url="AttachmentIndex.aspx?calledfrom=reinsurer&EntityType=Reinsurer&EntityId="+document.getElementById('hidREIN_CONTACT_ID').value + "&";
					DrawTab(3,top.frames[1],'Attachment',Url);
					
					Url="Reinsurance/ReinsuranceBankingDetails.aspx?";
					DrawTab(4,top.frames[1],'Reinsurance Banking Details',Url);			
				}
				else
				{							
					RemoveTab(4,top.frames[1]);
					RemoveTab(3,top.frames[1]);
					RemoveTab(2,top.frames[1]);					
				}	
			}
			
			function ResetAfterActivateDeactivate()
			{
				if (document.getElementById('hidReset').value == "1")
				{				
					document.MNT_REIN_CONTACT.reset();			
				}
			}		
																							
			function Reset()
			{
				DisableValidators();
				document.MNT_REIN_CONTACT.reset();
				return false;
			}
																			  
	//function ChkDate(objSource , objArgs)
	//{
	//	var effdate=document.MNT_REIN_CONTACT.txtORIGINAL_CONTRACT_DATE.value;
	//	objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",effdate,jsaAppDtFormat);
	//}
	
		function ChkCountry_physical()
		{
			if(document.getElementById("cmbREIN_CONTACT_COUNTRY").options[document.getElementById("cmbREIN_CONTACT_COUNTRY").selectedIndex].value != "1")
			{
				EnableValidator('revREIN_CONTACT_ZIP',false);
				EnableValidator('rfvREIN_CONTACT_STATE',false);
				EnableValidator('rfvREIN_CONTACT_ZIP',false);
				document.getElementById("spnREIN_CONTACT_STATE").style.display = "none";
				document.getElementById("spnREIN_CONTACT_ZIP").style.display = "none";
				document.getElementById("cmbREIN_CONTACT_STATE").style.backgroundColor="white";
				document.getElementById("txtREIN_CONTACT_ZIP").style.backgroundColor="white";
			}
			else 
			{
				EnableValidator('revREIN_CONTACT_ZIP',true);
				EnableValidator('rfvREIN_CONTACT_STATE',true);
				EnableValidator('rfvREIN_CONTACT_ZIP',true);
				document.getElementById("spnREIN_CONTACT_STATE").style.display = "inline";
				document.getElementById("spnREIN_CONTACT_ZIP").style.display = "inline";
				document.getElementById("cmbREIN_CONTACT_STATE").style.backgroundColor="#FFFFD1";
				document.getElementById("txtREIN_CONTACT_ZIP").style.backgroundColor="#FFFFD1";
			}
			if(document.getElementById("cmbREIN_CONTACT_COUNTRY").options[document.getElementById("cmbREIN_CONTACT_COUNTRY").selectedIndex].value != "1" && document.getElementById("cmbREIN_CONTACT_COUNTRY").options[document.getElementById("cmbREIN_CONTACT_COUNTRY").selectedIndex].value != "2")
			{
				//EnableValidator('revREIN_CONTACT_PHONE_1',false);
				//EnableValidator('revREIN_CONTACT_PHONE_2',false);
				EnableValidator('revREIN_CONTACT_PHONE_1',true);
				EnableValidator('revREIN_CONTACT_PHONE_2',true);
			}
			else
			{
				//EnableValidator('revREIN_CONTACT_PHONE_1',true);
				//EnableValidator('revREIN_CONTACT_PHONE_2',true);
				EnableValidator('revREIN_CONTACT_PHONE_1',false);
				EnableValidator('revREIN_CONTACT_PHONE_2',false);
				
			}
			if((document.getElementById("cmbREIN_CONTACT_COUNTRY").options[document.getElementById("cmbREIN_CONTACT_COUNTRY").selectedIndex].value != "1" && document.getElementById("cmbREIN_CONTACT_COUNTRY").options[document.getElementById("cmbREIN_CONTACT_COUNTRY").selectedIndex].value != "2")
			||( document.getElementById("cmbM_REIN_CONTACT_COUNTRY").options[document.getElementById("cmbM_REIN_CONTACT_COUNTRY").selectedIndex].value !="1" && document.getElementById("cmbM_REIN_CONTACT_COUNTRY").options[document.getElementById("cmbM_REIN_CONTACT_COUNTRY").selectedIndex].value !="2"))
			{
				EnableValidator('revREIN_CONTACT_MOBILE',false);
				EnableValidator('revREIN_CONTACT_FAX',false);
			}
			else
			{
				EnableValidator('revREIN_CONTACT_MOBILE',true);
				EnableValidator('revREIN_CONTACT_FAX',true);
			}
			
		}
		function ChkCountry_mailing()
		{
			if(document.getElementById("cmbM_REIN_CONTACT_COUNTRY").options[document.getElementById("cmbM_REIN_CONTACT_COUNTRY").selectedIndex].value !="1")
				{
					EnableValidator('revM_REIN_CONTACT_ZIP',false);
					//EnableValidator('rfvM_REIN_CONTACT_STATE',false);
					//EnableValidator('rfvM_REIN_CONTACT_ZIP',false);
					//document.getElementById("spnM_REIN_CONTACT_STATE").style.display = "none";
					//document.getElementById("spnM_REIN_CONTACT_ZIP").style.display = "none";
					document.getElementById("cmbM_REIN_CONTACT_STATE").style.backgroundColor="white";
					document.getElementById("txtM_REIN_CONTACT_ZIP").style.backgroundColor="white";
				}
			else 
			{
				EnableValidator('revM_REIN_CONTACT_ZIP',true);
				//EnableValidator('rfvM_REIN_CONTACT_STATE',true);
				//EnableValidator('rfvM_REIN_CONTACT_ZIP',true);
				//document.getElementById("spnM_REIN_CONTACT_STATE").style.display = "inline";
				//document.getElementById("spnM_REIN_CONTACT_ZIP").style.display = "inline";
				document.getElementById("cmbM_REIN_CONTACT_STATE").style.backgroundColor="#FFFFD1";
				document.getElementById("txtM_REIN_CONTACT_ZIP").style.backgroundColor="#FFFFD1";
			}
			
			if(document.getElementById("cmbM_REIN_CONTACT_COUNTRY").options[document.getElementById("cmbM_REIN_CONTACT_COUNTRY").selectedIndex].value !="1" && document.getElementById("cmbM_REIN_CONTACT_COUNTRY").options[document.getElementById("cmbM_REIN_CONTACT_COUNTRY").selectedIndex].value !="2")
			{
				//EnableValidator('revM_REIN_CONTACT_PHONE_1',false);
				EnableValidator('revM_REIN_CONTACT_PHONE_1',true);
				//EnableValidator('revM_REIN_CONTACT_PHONE_2',false);
				EnableValidator('revM_REIN_CONTACT_PHONE_2',true);
			}
			else
			{
				//EnableValidator('revM_REIN_CONTACT_PHONE_1',true);
				EnableValidator('revM_REIN_CONTACT_PHONE_1',false);
				//EnableValidator('revM_REIN_CONTACT_PHONE_2',true);
				EnableValidator('revM_REIN_CONTACT_PHONE_2',false);
			}
			if((document.getElementById("cmbREIN_CONTACT_COUNTRY").options[document.getElementById("cmbREIN_CONTACT_COUNTRY").selectedIndex].value != "1" && document.getElementById("cmbREIN_CONTACT_COUNTRY").options[document.getElementById("cmbREIN_CONTACT_COUNTRY").selectedIndex].value != "2")
			||( document.getElementById("cmbM_REIN_CONTACT_COUNTRY").options[document.getElementById("cmbM_REIN_CONTACT_COUNTRY").selectedIndex].value !="1" && document.getElementById("cmbM_REIN_CONTACT_COUNTRY").options[document.getElementById("cmbM_REIN_CONTACT_COUNTRY").selectedIndex].value !="2"))
			{
				//EnableValidator('revREIN_CONTACT_MOBILE',false);
				//EnableValidator('revREIN_CONTACT_FAX',false);
				EnableValidator('revREIN_CONTACT_MOBILE',true);
				EnableValidator('revREIN_CONTACT_FAX',true);

			}
			else
			{
				//EnableValidator('revREIN_CONTACT_MOBILE',true);
				//EnableValidator('revREIN_CONTACT_FAX',true);
				EnableValidator('revREIN_CONTACT_MOBILE',false);
				EnableValidator('revREIN_CONTACT_FAX',false);
			}
			
		}
		
		function GenerateCustomerCode(Ctrl)
		{
			 if (Ctrl == "txtREIN_CONTACT_NAME")
					{
						if (document.getElementById("txtREIN_CONTACT_NAME").value != "")
						{
							if (document.getElementById("txtREIN_CONTACT_CODE").value == "")
							{
								document.getElementById('txtREIN_CONTACT_CODE').value=(GenerateRandomCode(document.getElementById('txtREIN_CONTACT_NAME').value,''));
								/*document.getElementById("txtCUSTOMER_CODE").value = document.getElementById("txtCUSTOMER_FIRST_NAME").value.substring(0,4) +
																									"000001";*/
								EnableValidator('rfvREIN_CONTACT_CODE',false);
							}
						}
					}
		}


		/*function chkphone()
		{
			if(document.getElementById("cmbM_REIN_CONTACT_COUNTRY").options[document.getElementById("cmbM_REIN_CONTACT_COUNTRY").selectedIndex].value !="1" && document.getElementById("cmbM_REIN_CONTACT_COUNTRY").options[document.getElementById("cmbM_REIN_CONTACT_COUNTRY").selectedIndex].value !="2")
			{
				EnableValidator('revM_REIN_CONTACT_PHONE_1_CHECK',false);
				alert(document.getElementById("revM_REIN_CONTACT_PHONE_1").isValid );
				if(document.getElementById("revM_REIN_CONTACT_PHONE_1").isValid == false)
				{
				alert('1');
					//EnableValidator('revM_REIN_CONTACT_PHONE_1',false);
					EnableValidator('revM_REIN_CONTACT_PHONE_1_CHECK',true);
				}
				else
				{
				alert('2');
					EnableValidator('revM_REIN_CONTACT_PHONE_1_CHECK',false);
				}
				alert('3');
				EnableValidator('revM_REIN_CONTACT_PHONE_1',false);
			}
			else
			{
					EnableValidator('revM_REIN_CONTACT_PHONE_1_CHECK',false);
					EnableValidator('revM_REIN_CONTACT_PHONE_1',true);
			}
		
		
		}*/
		
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();">
		<FORM id="MNT_REIN_CONTACT" method="post" runat="server">
			<P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4"><asp:Label ID="capMAND" runat="server"></asp:Label></TD><%--Please note that all fields marked with * are 
										mandatory--%>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr>
									<!--START April 09 2007 Harmanjeet-->
									<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_NAME" runat="server"></asp:label><span id="spnREIN_CONTACT_NAME" runat="server" class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_CONTACT_NAME" runat="server" size="30" MaxLength="75" ></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvREIN_CONTACT_NAME" runat="server" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_NAME"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revREIN_CONTACT_NAME" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_NAME"
											Runat="server"></asp:regularexpressionvalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_CODE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_CONTACT_CODE"  runat="server" size="12" maxlength="10"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvREIN_CONTACT_CODE" runat="server" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_CODE"></asp:requiredfieldvalidator></TD>
									<!--END Harmanjeet--></tr>
								<!-- START April 10 2007 Harmanjeet-->
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_POSITION" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_CONTACT_POSITION" MaxLength="30"  runat="server"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revREIN_CONTACT_POSITION" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_POSITION"
											Runat="server"></asp:regularexpressionvalidator>
									</TD>
									<!--<td class="midcolora" colSpan="2"></td>-->
									<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_SALUTATION" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_CONTACT_SALUTATION"  runat="server" size="12" maxlength="50"></asp:textbox>
                                    <asp:DropDownList ID="cmbREIN_CONTACT_SALUTATION" runat="server"></asp:DropDownList>
										<asp:regularexpressionvalidator id="revREIN_CONTACT_SALUTATION" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_SALUTATION"
											Runat="server"></asp:regularexpressionvalidator>
									</TD>
								</tr>
								<!--END Harmanjeet-->
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4"><asp:Label ID="capPHYSICAL" runat="server"></asp:Label></TD><%--Physical Address--%>
								</tr>
								<tr>
									<td class="midcolora" width="18%"><asp:label id="Label1" runat="server"></asp:label></td>
									<td class="midcolora" width="32%"><cmsb:cmsbutton class="clsButton" id="btnPullPhysicalAddress" runat="server" Text="Pull Physical Address"
											CausesValidation="False" ></cmsb:cmsbutton></td>
					</TD>
					<td class="midcolora" colSpan="2"></td>
				</TR>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_ADDRESS_1" runat="server"></asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_CONTACT_ADDRESS_1"  runat="server" maxlength="50"></asp:textbox><BR>
						<asp:requiredfieldvalidator id="rfvREIN_CONTACT_ADDRESS_1" runat="server" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_ADDRESS_1"></asp:requiredfieldvalidator>
						<asp:regularexpressionvalidator id="revREIN_CONTACT_ADDRESS_1" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_ADDRESS_1"
							Runat="server"></asp:regularexpressionvalidator>
					</TD>
					<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_ADDRESS_2" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_CONTACT_ADDRESS_2"  runat="server" maxlength="50"></asp:textbox>
						<asp:regularexpressionvalidator id="revREIN_CONTACT_ADDRESS_2" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_ADDRESS_2"
							Runat="server"></asp:regularexpressionvalidator>
					</TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_CITY" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_CONTACT_CITY"  runat="server" maxlength="50"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revREIN_CONTACT_CITY" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_CITY"
							Runat="server"></asp:regularexpressionvalidator>
					</TD>
					<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_COUNTRY" runat="server"></asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%">
					<%--<asp:dropdownlist id="cmbREIN_CONTACT_COUNTRY" onfocus="SelectComboIndex('cmbREIN_CONTACT_COUNTRY')"
					onChange = "ChkCountry_physical();" runat="server"></asp:dropdownlist>--%>
					<asp:textbox id="txtREIN_CONTACT_COUNTRY"  runat="server" maxlength="50"></asp:textbox><BR>
					<asp:requiredfieldvalidator id="rfvREIN_CONTACT_COUNTRY" runat="server" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_COUNTRY"></asp:requiredfieldvalidator>
					
					</TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_STATE" runat="server"></asp:label><span id ="spnREIN_CONTACT_STATE" runat="server"  class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_CONTACT_STATE" 
							runat="server"></asp:textbox><br>
					<asp:requiredfieldvalidator id="rfvREIN_CONTACT_STATE" runat="server" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_STATE"></asp:requiredfieldvalidator>
					</TD>
					<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_ZIP" runat="server"></asp:label><span id="spnREIN_CONTACT_ZIP" class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_CONTACT_ZIP"  runat="server" size="13" onkeypress="MaxLength(this,8)" onpaste="MaxLength(this,8)"></asp:textbox>
						<%-- Added by Swarup on 30-mar-2007 --%>
						<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
							<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
						</asp:hyperlink><br>
						<asp:requiredfieldvalidator id="rfvREIN_CONTACT_ZIP" runat="server" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_ZIP"></asp:requiredfieldvalidator>
						<asp:regularexpressionvalidator id="revREIN_CONTACT_ZIP" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_ZIP"
							Runat="server"></asp:regularexpressionvalidator>
					</TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_PHONE_1" runat="server"></asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_CONTACT_PHONE_1"  runat="server" size="17" onkeypress="MaxLength(this,15)" onpaste="MaxLength(this,15)" onblur="FormatBrazilPhone()"></asp:textbox><br />
						<%--<asp:regularexpressionvalidator id="revREIN_CONTACT_PHONE_1" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_PHONE_1"
							Runat="server"></asp:regularexpressionvalidator>--%>
							<asp:requiredfieldvalidator id="rfvREIN_CONTACT_PHONE_1" runat="server" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_PHONE_1"></asp:requiredfieldvalidator>
							<asp:regularexpressionvalidator id="revREIN_CONTACT_PHONE_1" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_PHONE_1"
							Runat="server"></asp:regularexpressionvalidator>
					</TD>
					<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_EXT_1" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_CONTACT_EXT_1"  runat="server" size="6" MaxLength="5"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revREIN_CONTACT_EXT_1" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_EXT_1"
							Runat="server"></asp:regularexpressionvalidator>
						
					</TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_PHONE_2" runat="server"></asp:label><span id="spnREIN_CONTACT_PHONE_2" runat="server" class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_CONTACT_PHONE_2"  runat="server" size="17" onkeypress="MaxLength(this,15)" onpaste="MaxLength(this,15)" onblur="FormatBrazilPhone()"></asp:textbox><br>
						<%--<asp:regularexpressionvalidator id="revREIN_CONTACT_PHONE_2" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_PHONE_2"
							Runat="server"></asp:regularexpressionvalidator>--%>
							<asp:requiredfieldvalidator id="rfvREIN_CONTACT_PHONE_2" runat="server" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_PHONE_2"></asp:requiredfieldvalidator>
							<asp:regularexpressionvalidator id="revREIN_CONTACT_PHONE_2" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_PHONE_2"
							Runat="server"></asp:regularexpressionvalidator>
					</TD>
					<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_EXT_2" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_CONTACT_EXT_2"  runat="server" size="6" MaxLength="5"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revREIN_CONTACT_EXT_2" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_EXT_2"
							Runat="server"></asp:regularexpressionvalidator>
					</TD>
				</tr>
				<tr>
					<TD class="headerEffectSystemParams" style="HEIGHT: 21px" colSpan="4"><asp:Label ID="capMAIL" runat="server"></asp:Label></TD><%--Mailing 
						Address--%>
				</tr>
				<tr>
					<td class="midcolora" width="18%"><asp:label id="lblCopy_Address" runat="server"></asp:label></td>
					<td class="midcolora" width="32%"><cmsb:cmsbutton class="clsButton" id="btnPullMailingAddress" runat="server" Text="Pull Mailing Address"
							CausesValidation="False" ></cmsb:cmsbutton></td>
					</TD>
					<td class="midcolora" colSpan="2"></td>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capM_REIN_CONTACT_ADDRESS_1" runat="server"></asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtM_REIN_CONTACT_ADDRESS_1"  runat="server" maxlength="50"></asp:textbox><BR>
						<asp:requiredfieldvalidator id="rfvM_REIN_CONTACT_ADDRESS_1" runat="server" Display="Dynamic" ControlToValidate="txtM_REIN_CONTACT_ADDRESS_1"></asp:requiredfieldvalidator>
						<asp:regularexpressionvalidator id="revM_REIN_CONTACT_ADDRESS_1" Display="Dynamic" ControlToValidate="txtM_REIN_CONTACT_ADDRESS_1"
							Runat="server"></asp:regularexpressionvalidator>
					</TD>
					<TD class="midcolora" width="18%"><asp:label id="capM_REIN_CONTACT_ADDRESS_2" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtM_REIN_CONTACT_ADDRESS_2"  runat="server" maxlength="50"></asp:textbox>
						<asp:regularexpressionvalidator id="revM_REIN_CONTACT_ADDRESS_2" Display="Dynamic" ControlToValidate="txtM_REIN_CONTACT_ADDRESS_2"
							Runat="server"></asp:regularexpressionvalidator>
					</TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capM_REIN_CONTACT_CITY" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtM_REIN_CONTACT_CITY" runat="server" maxlength="50" ></asp:textbox><br>
						<asp:regularexpressionvalidator id="revM_REIN_CONTACT_CITY" Display="Dynamic" ControlToValidate="txtM_REIN_CONTACT_CITY"
							Runat="server"></asp:regularexpressionvalidator>
					</TD>
					<TD class="midcolora" width="18%"><asp:label id="capM_REIN_CONTACT_COUNTRY" runat="server"></asp:label><!--<span class="mandatory">*</span>--></TD>
					<TD class="midcolora" width="32%">
					<%--<asp:dropdownlist id="cmbM_REIN_CONTACT_COUNTRY" onfocus="SelectComboIndex('cmbM_REIN_CONTACT_COUNTRY')"
						onChange ="ChkCountry_mailing();" runat="server" ></asp:dropdownlist>--%>
						<asp:textbox id="txtM_REIN_CONTACT_COUNTRY" runat="server" maxlength="50" ></asp:textbox><br>
						<%--<asp:requiredfieldvalidator id="rfvM_REIN_CONTACT_COUNTRY" runat="server" Display="Dynamic" ControlToValidate="txtM_REIN_CONTACT_COUNTRY"></asp:requiredfieldvalidator>--%>
					</TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capM_REIN_CONTACT_STATE" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtM_REIN_CONTACT_STATE" 
							runat="server" ></asp:textbox><br>
							<%--<asp:requiredfieldvalidator id="rfvM_REIN_CONTACT_STATE" runat="server" Display="Dynamic" ControlToValidate="cmbM_REIN_CONTACT_STATE"></asp:requiredfieldvalidator>--%>
							</TD>
					<TD class="midcolora" width="18%"><asp:label id="capM_REIN_CONTACT_ZIP" runat="server"></asp:label><span id="spnM_REIN_CONTACT_ZIP" runat="server" class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtM_REIN_CONTACT_ZIP" runat="server" size="13" onkeypress="MaxLength(this,8)" onpaste="MaxLength(this,8)" ></asp:textbox>
						
						<asp:hyperlink id="hlkMZipLookup" runat="server" CssClass="HotSpot">
							<asp:image id="imgMZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
						</asp:hyperlink><br>
					<%--<asp:requiredfieldvalidator id="rfvM_REIN_CONTACT_ZIP" runat="server" Display="Dynamic" ControlToValidate="txtM_REIN_CONTACT_ZIP"></asp:requiredfieldvalidator>--%>
						<asp:regularexpressionvalidator id="revM_REIN_CONTACT_ZIP" Display="Dynamic" ControlToValidate="txtM_REIN_CONTACT_ZIP"
							Runat="server"></asp:regularexpressionvalidator></TD>
							
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capM_REIN_CONTACT_PHONE_1" runat="server"></asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtM_REIN_CONTACT_PHONE_1" runat="server" size="17" onkeypress="MaxLength(this,15)" onpaste="MaxLength(this,15)" onblur="FormatBrazilPhone()"></asp:textbox><br>
						<%--<asp:regularexpressionvalidator id="revM_REIN_CONTACT_PHONE_1" Display="Dynamic" ControlToValidate="txtM_REIN_CONTACT_PHONE_1"
							Runat="server"></asp:regularexpressionvalidator>--%>
							<asp:requiredfieldvalidator id="rfvM_REIN_CONTACT_PHONE_1" runat="server" Display="Dynamic" ControlToValidate="txtM_REIN_CONTACT_PHONE_1"></asp:requiredfieldvalidator>
							<asp:regularexpressionvalidator id="revM_REIN_CONTACT_PHONE_1" Display="Dynamic" ControlToValidate="txtM_REIN_CONTACT_PHONE_1"
							Runat="server"></asp:regularexpressionvalidator></TD>
					<TD class="midcolora" width="18%"><asp:label id="capM_REIN_CONTACT_EXT_1" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtM_REIN_CONTACT_EXT_1"  runat="server" size="6" MaxLength="5"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revM_REIN_CONTACT_EXT_1" Display="Dynamic" ControlToValidate="txtM_REIN_CONTACT_EXT_1"
							Runat="server"></asp:regularexpressionvalidator></TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capM_REIN_CONTACT_PHONE_2" runat="server"></asp:label><span id="spnM_REIN_CONTACT_PHONE_2" runat="server" class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtM_REIN_CONTACT_PHONE_2" runat="server" size="17" onkeypress="MaxLength(this,15)" onpaste="MaxLength(this,15)" onblur="FormatBrazilPhone()"></asp:textbox><br>
						<%--<asp:regularexpressionvalidator id="revM_REIN_CONTACT_PHONE_2" Display="Dynamic" ControlToValidate="txtM_REIN_CONTACT_PHONE_2"
							Runat="server"></asp:regularexpressionvalidator>--%>
							<asp:requiredfieldvalidator id="rfvM_REIN_CONTACT_PHONE_2" runat="server" Display="Dynamic" ControlToValidate="txtM_REIN_CONTACT_PHONE_2"></asp:requiredfieldvalidator>
							<asp:regularexpressionvalidator id="revM_REIN_CONTACT_PHONE_2" Display="Dynamic" ControlToValidate="txtM_REIN_CONTACT_PHONE_2"
							Runat="server"></asp:regularexpressionvalidator></TD>
					<TD class="midcolora" width="18%"><asp:label id="capM_REIN_CONTACT_EXT_2" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtM_REIN_CONTACT_EXT_2"  runat="server" size="6" MaxLength="5"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revM_REIN_CONTACT_EXT_2" Display="Dynamic" ControlToValidate="txtM_REIN_CONTACT_EXT_2"
							Runat="server"></asp:regularexpressionvalidator></TD>
				</tr>
				<tr>
					<TD class="headerEffectSystemParams" style="HEIGHT: 21px" colSpan="4"><asp:Label ID="capOTHER" runat="server"></asp:Label> </TD><%--Other 
						Information--%>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_MOBILE" runat="server"></asp:label><span id="spnREIN_CONTACT_MOBILE" runat="server" class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_CONTACT_MOBILE" runat="server" size="15" MaxLength="13" ></asp:textbox><br>
						<%--<asp:regularexpressionvalidator id="revREIN_CONTACT_MOBILE" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_MOBILE"
							Runat="server"></asp:regularexpressionvalidator>--%>
							<asp:requiredfieldvalidator id="rfvREIN_CONTACT_MOBILE" runat="server" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_MOBILE"></asp:requiredfieldvalidator><BR>
							<asp:regularexpressionvalidator id="revREIN_CONTACT_MOBILE" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_MOBILE"
							Runat="server"></asp:regularexpressionvalidator></TD>
					<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_FAX" runat="server"></asp:label><span id="spnREIN_CONTACT_FAX" runat="server" class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_CONTACT_FAX" runat="server" size="17" onkeypress="MaxLength(this,15)" onpaste="MaxLength(this,15)" onblur="FormatBrazilPhone()"></asp:textbox><br>
						<%--<asp:regularexpressionvalidator id="revREIN_CONTACT_FAX" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_FAX"
							Runat="server"></asp:regularexpressionvalidator>--%>
							<asp:requiredfieldvalidator id="rfvREIN_CONTACT_FAX" runat="server" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_FAX"></asp:requiredfieldvalidator>
							<asp:regularexpressionvalidator id="revREIN_CONTACT_FAX" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_FAX"
							Runat="server"></asp:regularexpressionvalidator></TD>
				</tr>
				<tr>
					<!--START April 09 2007 Harmanjeet-->
					<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_SPEED_DIAL" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_CONTACT_SPEED_DIAL" runat="server" size="6" MaxLength="4" ></asp:textbox><br>
						<asp:regularexpressionvalidator id="revREIN_CONTACT_SPEED_DIAL" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_SPEED_DIAL"
							Runat="server"></asp:regularexpressionvalidator>
					</TD>
					<!--END Harmanjeet-->
					<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_EMAIL_ADDRESS" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_CONTACT_EMAIL_ADDRESS" runat="server" maxlength="50" ></asp:textbox><br>
						<asp:regularexpressionvalidator id="revREIN_CONTACT_EMAIL_ADDRESS" Display="Dynamic" ControlToValidate="txtREIN_CONTACT_EMAIL_ADDRESS"
							Runat="server"></asp:regularexpressionvalidator></TD>
				</tr>
				<!--START April 09 2007 Harmanjeet-->
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capREIN_CONTACT_DESC" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_CONTACT_CONTRACT_DESC" runat="server" maxlength="30" ></asp:textbox><BR>
					</TD>
					<TD class="midcolora" width="18%"><asp:label id="capCOMMENTS" runat="server"></asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtCOMMENTS" runat="server" onkeypress="MaxLength(this,255);"  TextMode="MultiLine" ></asp:textbox><BR>
					</TD>
				</tr>
				<!--End Harmanjeet-->
				<tr>
					<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" ></cmsb:cmsbutton>
						<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate" CausesValidation="false"
							visible="True" ></cmsb:cmsbutton>&nbsp;<cmsb:cmsbutton class="clsButton" id="btnBackToReinsurer" runat="server" Text="Back To Carrier Details"
							CausesValidation="false" visible="True" ></cmsb:cmsbutton></td>
					<td class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" ></cmsb:cmsbutton></td>
				</tr>
			</TABLE>
			</TD></TR></TBODY></TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidREIN_CONTACT_ID" type="hidden" value="0" name="hidREIN_CONTACT_ID" runat="server">
			<INPUT id="hidReset" type="hidden" value="0" name="hidReset" runat="server">
			<INPUT id="hidCompany_ID" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidAddress1" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidAddress2" type="hidden" name="hidIS_ACTIVE" runat="server"> 
			<INPUT id="hidCity" type="hidden" value="0" name="hidUSER_TYPE_ID" runat="server">
			<INPUT id="hidState" type="hidden" name="hidAGENCY_CODE" runat="server">
			<INPUT id="hidCountry" type="hidden" value="0" name="hidUSER_ID" runat="server">
			<INPUT id="hidZip" type="hidden" name="hidAGENCY_ADD2" runat="server"> 
			<INPUT id="hidPhone1" type="hidden" value="0" name="hidUSER_ID" runat="server">
			<INPUT id="hidPhone2" type="hidden" value="0" name="hidPhone2" runat="server">
			<INPUT id="hidFax" type="hidden" name="hidAGENCY_ADD2" runat="server">
			<INPUT id="hidM_Address1" type="hidden" name="hidAGENCY_CITY" runat="server">
			<INPUT id="hidM_Address2" type="hidden" name="hidAGENCY_STATE" runat="server"> 
			<INPUT id="hidM_City" type="hidden" name="hidAGENCY_COUNTRY" runat="server">
			<INPUT id="hidM_State" type="hidden" name="hidAGENCY_PHONE" runat="server">
			<INPUT id="hidM_Country" type="hidden" name="hidAGENCY_ZIP" runat="server">
			<INPUT id="hidM_Zip" type="hidden" name="hidAGENCY_FAX" runat="server">
			<INPUT id="hidM_Phone1" type="hidden" value="0" name="hidUSER_ID" runat="server">
			<INPUT id="hidM_Phone2" type="hidden" value="0" name="hidM_Phone2" runat="server">
			<INPUT id="hidM_Fax" type="hidden" name="hidAGENCY_ADD2" runat="server">
            <input id="hidREINCONTACTSALUTATION" type="hidden" runat="server" />
			<INPUT id="hidREIN_COMAPANY_ID" type="hidden" name="hidREIN_COMAPANY_ID" runat="server">&nbsp;
		</FORM>
		<script>
		RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidREIN_CONTACT_ID').value);
		</script>
	</BODY>
</HTML>

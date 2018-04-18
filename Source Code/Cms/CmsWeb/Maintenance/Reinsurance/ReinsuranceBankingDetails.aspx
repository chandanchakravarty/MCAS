<%@ Page language="c#" Codebehind="ReinsuranceBankingDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Reinsurance.ReinsuranceBankingDetails" ValidateRequest="false" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Reinsurance Banking Details</title>
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
				
				//window.parent.changeTab(0,0);
				//window.location.href = "addreinsurer.aspx?REIN_COMAPANY_ID=" + document.getElementById("hidCompany_ID").value;
				//alert(window.location.href);
				//RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidCompany_ID').value,true);
				return false;
			}
			
			function AddData()
			{
			ChangeColor();
			DisableValidators();
			//return;
			document.getElementById('hidREIN_BANK_DETAIL_ID').value	=	'New';
			//document.getElementById('txtREIN_CONTACT_CODE').value  = '';
			//document.getElementById('txtREIN_CONTACT_NAME').focus();
			//document.getElementById('txtREIN_CONTACT_POSITION').value  = '';
			//document.getElementById('txtREIN_CONTACT_SALUTATION').value  = '';
			document.getElementById('txtREIN_BANK_DETAIL_ADDRESS_1').value  = '';
			document.getElementById('txtREIN_BANK_DETAIL_ADDRESS_2').value  = '';
			document.getElementById('txtREIN_BANK_DETAIL_CITY').value  = '';
			document.getElementById('cmbREIN_BANK_DETAIL_COUNTRY').options.selectedIndex = 0;
			document.getElementById('cmbREIN_BANK_DETAIL_STATE').options.selectedIndex = -1;
			document.getElementById('txtREIN_BANK_DETAIL_ZIP').value  = '';
			//document.getElementById('txtREIN_CONTACT_PHONE_1').value  = '';
			//document.getElementById('txtREIN_CONTACT_PHONE_2').value  = '';
			//document.getElementById('txtREIN_CONTACT_EXT_1').value  = '';
			//document.getElementById('txtREIN_CONTACT_EXT_2').value  = '';
			//document.getElementById('txtREIN_CONTACT_MOBILE').value  = '';
			//document.getElementById('txtREIN_CONTACT_FAX').value  = '';
			//document.getElementById('txtREIN_CONTACT_SPEED_DIAL').value  = '';
			
			document.getElementById('txtM_REIN_BANK_DETAIL_ADDRESS_1').value  = '';
			document.getElementById('txtM_REIN_BANK_DETAIL_ADDRESS_2').value  = '';
			document.getElementById('txtM_REIN_BANK_DETAIL_CITY').value  = '';
			document.getElementById('cmbM_REIN_BANK_DETAIL_COUNTRY').options.selectedIndex = 0;
			document.getElementById('cmbM_REIN_BANK_DETAIL_STATE').options.selectedIndex = 0;
			document.getElementById('txtM_REIN_BANK_DETAIL_ZIP').value  = '';
			//document.getElementById('txtM_REIN_CONTACT_PHONE_1').value  = '';
			//document.getElementById('txtM_REIN_CONTACT_PHONE_2').value  = '';
			//document.getElementById('txtM_REIN_CONTACT_EXT_1').value  = '';
			//document.getElementById('txtM_REIN_CONTACT_EXT_2').value  = '';
			
			document.getElementById('cmbREIN_PAYMENT_BASIS').options.selectedIndex = 0;
			document.getElementById('txtREIN_BANK_NAME').value  = '';
			document.getElementById('txtREIN_TRANSIT_ROUTING').value  = '';
			document.getElementById('txtREIN_BANK_ACCOUNT').value  = '';
			
			
			//document.getElementById('txtREIN_CONTACT_DESC').value  = '';
			//document.getElementById('txtCOMMENTS').value  = '';
			
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
			
			document.getElementById('btnPullPhysicalAddress').focus();
			
			}


			function populateXML()
			{
			//ResetAfterActivateDeactivate();
			if(document.getElementById('hidCheckAdd').value == "1")
				return;
				var tempXML;				
				tempXML=document.getElementById("hidOldData").value;
				if(document.getElementById('txtREIN_BANK_DETAIL_ADDRESS_1'))
					document.getElementById('txtREIN_BANK_DETAIL_ADDRESS_1').focus();
				if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
				{				
					if(tempXML!="" && tempXML!="0")
					{
		
						populateFormData(tempXML,"MNT_REIN_BANKING_DETAILS");						
					}
					else
					{
						AddData();
					}
				}
				else
				 AddData();
				
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
					document.MNT_REIN_BANKING_DETAILS.reset();			
				}
			}
												  
									  
			function CopyPhysicalAddress()
			{	 
				if(document.getElementById('txtREIN_BANK_DETAIL_ADDRESS_1').value=="")
				document.getElementById('txtREIN_BANK_DETAIL_ADDRESS_1').value =document.getElementById('hidAddress1').value ;
				
				if(document.getElementById('txtREIN_BANK_DETAIL_ADDRESS_2').value=="")
				document.getElementById('txtREIN_BANK_DETAIL_ADDRESS_2').value =document.getElementById('hidAddress2').value ;
				
				if(document.getElementById('txtREIN_BANK_DETAIL_CITY').value=="")
				document.getElementById('txtREIN_BANK_DETAIL_CITY').value =document.getElementById('hidCity').value	;
				
				//if(document.getElementById('cmbREIN_BANK_DETAIL_COUNTRY').value=="")
//document.getElementById('cmbREIN_BANK_DETAIL_COUNTRY').value =document.getElementById('hidCountry').value ;

//				if(document.getElementById('cmbREIN_BANK_DETAIL_COUNTRY').value=="")
//				{
				for (i=1 ;i<=document.getElementById('cmbREIN_BANK_DETAIL_COUNTRY').options.length ;i++)
					{
					    if (document.getElementById('cmbREIN_BANK_DETAIL_COUNTRY').options[i] != null) {
					       
					        
						var optionText=document.getElementById('cmbREIN_BANK_DETAIL_COUNTRY').options[i].text;
						optionText = optionText.toUpperCase();
						
						if(optionText==document.getElementById('hidCountry').value.toUpperCase())	
							{
							    document.getElementById('cmbREIN_BANK_DETAIL_COUNTRY').options[i].selected = true;
						
							break;
							}
						}
						
					}
//				}
				
				if(document.getElementById('cmbREIN_BANK_DETAIL_STATE').value=="")
				{
				for (i=1 ;i<=document.getElementById('cmbREIN_BANK_DETAIL_STATE').options.length ;i++)
					{				
						
						if (document.getElementById('cmbREIN_BANK_DETAIL_STATE').options[i]!=null)
						{
						var optionText=document.getElementById('cmbREIN_BANK_DETAIL_STATE').options[i].text;
						optionText=optionText.toUpperCase();
						if(optionText==document.getElementById('hidState').value.toUpperCase())	
							{
							document.getElementById('cmbREIN_BANK_DETAIL_STATE').options[i].selected=true;
							break;
							}
						}
						
					}
				}
				
				if(document.getElementById('txtREIN_BANK_DETAIL_ZIP').value=="")
				document.getElementById('txtREIN_BANK_DETAIL_ZIP').value =document.getElementById('hidZip').value	;
				

				ChangeColor();

				return false;
			}
			
			function CopyMailingAddress()
			{	 
				
				if(document.getElementById('txtM_REIN_BANK_DETAIL_ADDRESS_1').value=="")
				document.getElementById('txtM_REIN_BANK_DETAIL_ADDRESS_1').value =document.getElementById('hidM_Address1').value ;
				
				if(document.getElementById('txtM_REIN_BANK_DETAIL_ADDRESS_2').value=="")
				document.getElementById('txtM_REIN_BANK_DETAIL_ADDRESS_2').value =document.getElementById('hidM_Address2').value ;
				
				if(document.getElementById('txtM_REIN_BANK_DETAIL_CITY').value=="")
				document.getElementById('txtM_REIN_BANK_DETAIL_CITY').value =document.getElementById('hidM_City').value	;
				
				//if(document.getElementById('cmbM_REIN_BANK_DETAIL_COUNTRY').value=="")
				//document.getElementById('cmbM_REIN_BANK_DETAIL_COUNTRY').value =document.getElementById('hidM_Country').value ;
//				if(document.getElementById('cmbM_REIN_BANK_DETAIL_COUNTRY').value=="")
//				{
				for (i=1 ;i<=document.getElementById('cmbM_REIN_BANK_DETAIL_COUNTRY').options.length ;i++)
					{						
						if (document.getElementById('cmbM_REIN_BANK_DETAIL_COUNTRY').options[i]!=null)
						{
						var optionText=document.getElementById('cmbM_REIN_BANK_DETAIL_COUNTRY').options[i].text;
						optionText=optionText.toUpperCase();
						if(optionText==document.getElementById('hidM_Country').value.toUpperCase())	
							{
							document.getElementById('cmbM_REIN_BANK_DETAIL_COUNTRY').options[i].selected=true;
							break;
							}
						}
						
					}
//				}
				
				//if(document.getElementById('cmbM_REIN_BANK_DETAIL_STATE').value=="")
				//document.getElementById('cmbM_REIN_BANK_DETAIL_STATE').value =document.getElementById('hidM_State').value ;
				if(document.getElementById('cmbM_REIN_BANK_DETAIL_STATE').value=="")
				{
				for (i=1 ;i<=document.getElementById('cmbM_REIN_BANK_DETAIL_STATE').options.length ;i++)
					{					
						if (document.getElementById('cmbM_REIN_BANK_DETAIL_STATE').options[i]!=null)
						{
						var optionText=document.getElementById('cmbM_REIN_BANK_DETAIL_STATE').options[i].text;
						optionText=optionText.toUpperCase(); 
						if(optionText==document.getElementById('hidM_State').value.toUpperCase())	
							{
							document.getElementById('cmbM_REIN_BANK_DETAIL_STATE').options[i].selected=true;
							break;
							}
						}
						
					}
				}
				
				if(document.getElementById('txtM_REIN_BANK_DETAIL_ZIP').value=="")
				document.getElementById('txtM_REIN_BANK_DETAIL_ZIP').value =document.getElementById('hidM_Zip').value	;
				

				ChangeColor();

				return false;
			}	
			
			function ShowEFTDetail()
			{	
				if  (document.getElementById("cmbREIN_PAYMENT_BASIS").value == "EFT")
				{
					trBankName.style.display = '';
					trBankAccount.style.display = ''
					document.getElementById("rfvREIN_BANK_ACCOUNT").enabled = true;
					document.getElementById("revREIN_BANK_ACCOUNT").enabled = true;
					document.getElementById("rfvREIN_TRANSIT_ROUTING").enabled = true;
					document.getElementById("revREIN_TRANSIT_ROUTING").enabled = true;
					document.getElementById("rfvREIN_BANK_NAME").enabled = true;					
					
					
					/*
					document.getElementById('imgFOLLOW_UP_DATE').style.display='inline'; 
					if (document.getElementById('rfvREIN_BANK_NAME') != null)
						{
							document.getElementById('rfvREIN_BANK_NAME').setAttribute('enabled',true);
							if (document.getElementById('rfvREIN_BANK_NAME').isvalid == false)
								document.getElementById('rfvREIN_BANK_NAME').style.display = 'inline';
						} 	
			         
					if (document.getElementById('spnFOLLOW_UP_DATE') != null)
						{
							document.getElementById('spnFOLLOW_UP_DATE').style.display = "inline";
						}
					*/
				}  
				else
				{
					  	
					/*
					document.getElementById('txtFOLLOW_UP_DATE').style.display='none';
					document.getElementById('capFOLLOW_UP_DATE').style.display='none';
					document.getElementById('imgFOLLOW_UP_DATE').style.display='none'; 
					if (document.getElementById('rfvFOLLOW_UP_DATE') != null)
						{
							document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("enabled",false);
							document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("isvalid",true);
							document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'none';
							
						}
					if (document.getElementById('spnFOLLOW_UP_DATE') != null)
						{
							document.getElementById('spnFOLLOW_UP_DATE').style.display = "none";
						}	 
					*/
					trBankName.style.display = 'none';
					trBankAccount.style.display = 'none';
					document.getElementById("rfvREIN_BANK_ACCOUNT").enabled = false;
					document.getElementById("revREIN_BANK_ACCOUNT").enabled = false;
					document.getElementById("rfvREIN_TRANSIT_ROUTING").enabled = false;
					document.getElementById("revREIN_TRANSIT_ROUTING").enabled = false;
					document.getElementById("rfvREIN_BANK_NAME").enabled = false;				
					
				}     
			}			
					
																							
																				  
	//function ChkDate(objSource , objArgs)
	//{
	//	var effdate=document.MNT_REIN_CONTACT.txtORIGINAL_CONTRACT_DATE.value;
	//	objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",effdate,jsaAppDtFormat);
	//}
	

	//	function GenerateCustomerCode(Ctrl)
	//	{
	//		 if (Ctrl == "txtREIN_CONTACT_NAME")
	//				{
	//					if (document.getElementById("txtREIN_CONTACT_NAME").value != "")
	//					{
	//						if (document.getElementById("txtREIN_CONTACT_CODE").value == "")
	//						{
	//							document.getElementById('txtREIN_CONTACT_CODE').value=(GenerateRandomCode(document.getElementById('txtREIN_CONTACT_NAME').value,''));
								/*document.getElementById("txtCUSTOMER_CODE").value = document.getElementById("txtCUSTOMER_FIRST_NAME").value.substring(0,4) +
	//																								"000001";*/
	//						}
	//					}
	//				}
	//	}
	
	////////////////////////	
		function ValidateTranNo(objSource, objArgs)
		{
	
			var tranNum = document.getElementById('txtREIN_TRANSIT_ROUTING').value;
			var firstDigit = tranNum.slice(0,1);
			if(firstDigit == "5")
				objArgs.IsValid = false;
		}
		function VerifyTranNo(objSource, objArgs)
		{
		
			var boolval = ValidateTransitNumber(document.getElementById('txtREIN_TRANSIT_ROUTING'));
		
			if(boolval == false)
			{
				objArgs.IsValid = false;
			}
		}
		function ValidateTranNoLength(objSource, objArgs)
		{
		
			var boolval = ValidateTransitNumberLen(document.getElementById('txtREIN_TRANSIT_ROUTING'));
			
			if(boolval == false)
			{
				objArgs.IsValid = false;
			}
			
		}
	//////////////	
	function chkAdd()
	{
		if((document.getElementById('txtREIN_BANK_DETAIL_ADDRESS_1').value!=""
			&& document.getElementById('txtREIN_BANK_DETAIL_CITY').value!=""
			&& document.getElementById('cmbREIN_BANK_DETAIL_STATE').value!=""
			&& document.getElementById('txtREIN_BANK_DETAIL_ZIP').value!="")
			||
			(document.getElementById('txtM_REIN_BANK_DETAIL_ADDRESS_1').value!=""
			&& document.getElementById('txtM_REIN_BANK_DETAIL_CITY').value!=""
			&& document.getElementById('cmbM_REIN_BANK_DETAIL_STATE').value!=""
			&& document.getElementById('txtM_REIN_BANK_DETAIL_ZIP').value!=""))
			{
				document.getElementById('hidCheckAdd').value = "0";
			}
		else
		{
			alert('Please Fill atleast one complete address');
			document.getElementById('hidCheckAdd').value = "1";
			return;
		}
	}
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();ShowEFTDetail();">
		<FORM id="MNT_REIN_BANKING_DETAILS" method="post" runat="server">
			<P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
					</tr>
					<TR id="trBody" runat="server">
						<TD>
							<TABLE width="100%" align="center" border="0">
								<TBODY>
									<tr>
										<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
									</tr>
									<tr>
										<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
									</tr>
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
						<TD class="midcolora" width="18%"><asp:label id="capREIN_BANK_DETAIL_ADDRESS_1" runat="server"></asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_BANK_DETAIL_ADDRESS_1" runat="server" maxlength="50" ></asp:textbox><BR>
							<%--<asp:requiredfieldvalidator id="rfvREIN_BANK_DETAIL_ADDRESS_1" runat="server" Display="Dynamic" ControlToValidate="txtREIN_BANK_DETAIL_ADDRESS_1"></asp:requiredfieldvalidator>--%>
							<asp:regularexpressionvalidator id="revREIN_BANK_DETAIL_ADDRESS_1" runat="server" Display="Dynamic" ControlToValidate="txtREIN_BANK_DETAIL_ADDRESS_1"
								ErrorMessage="" ValidationExpression="^[\w\W]{0,50}$"></asp:regularexpressionvalidator></TD> <%--Please enter valid  Address--%>
						<TD class="midcolora" width="18%"><asp:label id="capREIN_BANK_DETAIL_ADDRESS_2" runat="server"></asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_BANK_DETAIL_ADDRESS_2"  runat="server" maxlength="50"></asp:textbox><asp:regularexpressionvalidator id="revREIN_BANK_DETAIL_ADDRESS_2" runat="server" Display="Dynamic" ControlToValidate="txtREIN_BANK_DETAIL_ADDRESS_2"
								ErrorMessage="" ValidationExpression="^^[\w\W]{0,50}$"></asp:regularexpressionvalidator></TD> <%--Please enter valid address--%>
					</tr>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="capREIN_BANK_DETAIL_CITY" runat="server"></asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_BANK_DETAIL_CITY"  runat="server" maxlength="50"></asp:textbox><br>
							<asp:regularexpressionvalidator id="revREIN_BANK_DETAIL_CITY" runat="server" Display="Dynamic" ControlToValidate="txtREIN_BANK_DETAIL_CITY"
								ErrorMessage="" ValidationExpression="^^[\w\W]{0,50}$"></asp:regularexpressionvalidator>
						<TD class="midcolora" width="18%"><asp:label id="capREIN_BANK_DETAIL_COUNTRY" runat="server"></asp:label></TD><%--Please enter valid City--%>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREIN_BANK_DETAIL_COUNTRY" onfocus="SelectComboIndex('cmbREIN_BANK_DETAIL_COUNTRY')"
								 runat="server"></asp:dropdownlist></TD>
					</tr>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="capREIN_BANK_DETAIL_STATE" runat="server"></asp:label></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREIN_BANK_DETAIL_STATE" onfocus="SelectComboIndex('cmbREIN_BANK_DETAIL_STATE')"
								 runat="server"></asp:dropdownlist></TD>
						<TD class="midcolora" width="18%"><asp:label id="capREIN_BANK_DETAIL_ZIP" runat="server"></asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_BANK_DETAIL_ZIP"  runat="server" onkeypress="MaxLength(this,8)" onpaste="MaxLength(this,8)" size="13"></asp:textbox>
							<%-- Added by Swarup on 30-mar-2007 --%>
							<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
								<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
							</asp:hyperlink><br>
							<asp:regularexpressionvalidator id="revREIN_BANK_DETAIL_ZIP" runat="server" Display="Dynamic" ControlToValidate="txtREIN_BANK_DETAIL_ZIP"
								ErrorMessage=""></asp:regularexpressionvalidator></TD><%--Please enter vaild Zip Code--%>
					</tr>
					<tr>
						<TD class="headerEffectSystemParams" colSpan="4"><asp:Label ID="capMAIL" runat="server"></asp:Label></TD><%--Mailing Address--%>
					</tr>
					<tr>
						<td class="midcolora" width="18%"><asp:label id="lblCopy_Address" runat="server"></asp:label></td>
						<td class="midcolora" width="32%"><cmsb:cmsbutton class="clsButton" id="btnPullMailingAddress" runat="server" Text="Pull Mailing Address"
								CausesValidation="False" ></cmsb:cmsbutton></td>
						<td class="midcolora" colSpan="2"></td>
					</tr>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="capM_REIN_BANK_DETAIL_ADDRESS_1" runat="server"></asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtM_REIN_BANK_DETAIL_ADDRESS_1"  runat="server" maxlength="50"></asp:textbox><BR>
							<%--<asp:requiredfieldvalidator id="rfvM_REIN_BANK_DETAIL_ADDRESS_1" runat="server" Display="Dynamic" ControlToValidate="txtM_REIN_BANK_DETAIL_ADDRESS_1"></asp:requiredfieldvalidator>--%>
							<asp:regularexpressionvalidator id="revM_REIN_BANK_DETAIL_ADDRESS_1" runat="server" Display="Dynamic" ControlToValidate="txtM_REIN_BANK_DETAIL_ADDRESS_1"
								ErrorMessage="" ValidationExpression="^[\w\W]{0,50}$"></asp:regularexpressionvalidator></TD><%--Please enter valid Address--%>
						<TD class="midcolora" width="18%"><asp:label id="capM_REIN_BANK_DETAIL_ADDRESS_2" runat="server"></asp:label></TD>
						<TD class="midcolora" width="32%">
							<asp:textbox id="txtM_REIN_BANK_DETAIL_ADDRESS_2"  runat="server" maxlength="50"></asp:textbox>
							<asp:regularexpressionvalidator id="revM_REIN_BANK_DETAIL_ADDRESS_2" runat="server" Display="Dynamic" ControlToValidate="txtM_REIN_BANK_DETAIL_ADDRESS_2"
								ErrorMessage="" ValidationExpression="^[\w\W]{0,50}$"></asp:regularexpressionvalidator></TD> <%--Please enter valid address--%>
						</TD>
					</tr>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="capM_REIN_BANK_DETAIL_CITY" runat="server"></asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtM_REIN_BANK_DETAIL_CITY" runat="server" maxlength="50" ></asp:textbox>
							<br>
							<asp:regularexpressionvalidator id="revM_REIN_BANK_DETAIL_CITY" runat="server" Display="Dynamic" ControlToValidate="txtM_REIN_BANK_DETAIL_CITY"
								ErrorMessage="" ValidationExpression="^[\w\W]{0,50}$"></asp:regularexpressionvalidator></TD> <%--Please enter valid City--%>
						<TD class="midcolora" width="18%"><asp:label id="capM_REIN_BANK_DETAIL_COUNTRY" runat="server"></asp:label></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbM_REIN_BANK_DETAIL_COUNTRY" onfocus="SelectComboIndex('cmbM_REIN_BANK_DETAIL_COUNTRY')"
								runat="server" ></asp:dropdownlist>
						</TD>
					</tr>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="capM_REIN_BANK_DETAIL_STATE" runat="server"></asp:label></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbM_REIN_BANK_DETAIL_STATE" onfocus="SelectComboIndex('cmbM_REIN_BANK_DETAIL_STATE')"
								runat="server" ></asp:dropdownlist>
							<br>
						</TD>
						<TD class="midcolora" width="18%"><asp:label id="capM_REIN_BANK_DETAIL_ZIP" runat="server"></asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtM_REIN_BANK_DETAIL_ZIP" runat="server" onkeypress="MaxLength(this,8)" onpaste="MaxLength(this,8)" size="13" ></asp:textbox>
							<%-- Added by Swarup on 30-mar-2007 --%>
							<asp:hyperlink id="hlkMZipLookup" runat="server" CssClass="HotSpot">
								<asp:image id="imgMZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
							</asp:hyperlink><br>
							<asp:regularexpressionvalidator id="revM_REIN_BANK_DETAIL_ZIP" runat="server" Display="Dynamic" ControlToValidate="txtM_REIN_BANK_DETAIL_ZIP"
								ErrorMessage="" Height="21"></asp:regularexpressionvalidator></TD><%--Please enter vaild Zip Code--%>
					</tr>
					<tr>
						<TD class="midcolora" width="18%"><asp:label id="capREIN_PAYMENT_BASIS" runat="server"></asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREIN_PAYMENT_BASIS" onfocus="SelectComboIndex('cmbREIN_PAYMENT_BASIS')" 
								runat="server"></asp:dropdownlist>
							<br>
							<asp:requiredfieldvalidator id="rfvREIN_PAYMENT_BASIS" runat="server" Display="Dynamic" ControlToValidate="cmbREIN_PAYMENT_BASIS"></asp:requiredfieldvalidator>
						</TD>
						<td class="midcolora" colSpan="2"></td>
					</tr>
					<tr id="trBankName" style="DISPLAY: none">
						<!--START April 09 2007 Harmanjeet-->
						<TD class="midcolora" width="18%"><asp:label id="capREIN_BANK_NAME" runat="server"></asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_BANK_NAME" runat="server" MaxLength="75" ></asp:textbox><br>
							<asp:requiredfieldvalidator id="rfvREIN_BANK_NAME" runat="server" ControlToValidate="txtREIN_BANK_NAME" ErrorMessage = "" Display="Dynamic"></asp:requiredfieldvalidator></TD><%--Please enter Bank Name--%>
						<!--END Harmanjeet-->
						<TD class="midcolora" width="18%"><asp:label id="capREIN_TRANSIT_ROUTING" runat="server"></asp:label><span class="mandatory">*</span></TD>
						
								
								<TD class="midcolora" width="32%">
										<asp:textbox id="txtREIN_TRANSIT_ROUTING" Runat="server" MaxLength="9"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvREIN_TRANSIT_ROUTING" Runat="server" Display="Dynamic" ErrorMessage=""
											ControlToValidate="txtREIN_TRANSIT_ROUTING"></asp:requiredfieldvalidator><%--Please Enter Transit/Routing number.--%>
										<asp:customvalidator id="csvREIN_TRANSIT_ROUTING" Runat="server" ClientValidationFunction="ValidateTranNo"
											ErrorMessage="" Display="Dynamic" ControlToValidate="txtREIN_TRANSIT_ROUTING"></asp:customvalidator><%--Number starting with 5 is invalid.--%>
										<asp:customvalidator id="csvVERIFY_ROUTING_NUMBER" Runat="server" ClientValidationFunction="VerifyTranNo"
											ErrorMessage="" Display="Dynamic" ControlToValidate="txtREIN_TRANSIT_ROUTING"></asp:customvalidator><%--Please Verify the 9th Digit (Check digit).--%>
										<asp:customvalidator id="csvVERIFY_ROUTING_NUMBER_LENGHT" Runat="server" ClientValidationFunction="ValidateTranNoLength"
											ErrorMessage="" Display="Dynamic" ControlToValidate="txtREIN_TRANSIT_ROUTING"></asp:customvalidator><%--Length has to be exactly 8/9 digits.--%>
										<asp:regularexpressionvalidator id="revREIN_TRANSIT_ROUTING" Runat="server" ValidationExpression="^[012346789]{1}[0-9]{7,8}" ErrorMessage=""
											Display="Dynamic" ControlToValidate="txtREIN_TRANSIT_ROUTING"></asp:regularexpressionvalidator></TD><%--Please Enter Valid Transit/Routing Number.--%>
					</tr>
					<!--START April 09 2007 Harmanjeet-->
					<tr id="trBankAccount" style="DISPLAY: none">
						<TD class="midcolora" width="18%"><asp:label id="capREIN_BANK_ACCOUNT" runat="server"></asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_BANK_ACCOUNT" runat="server" maxlength="17" ></asp:textbox><BR>
							<asp:requiredfieldvalidator id="rfvREIN_BANK_ACCOUNT" runat="server" ControlToValidate="txtREIN_BANK_ACCOUNT" ErrorMessage = ""
								Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revREIN_BANK_ACCOUNT" runat="server" Display="Dynamic" ControlToValidate="txtREIN_BANK_ACCOUNT"
								ValidationExpression="^[0-9]{1}[0-9\-]{0,15}[0-9]{0,1}" ErrorMessage=""></asp:regularexpressionvalidator></TD><%--Please enter Bank Account #--%><%--Enter Numeric and (-) in between--%>
						<td class="midcolora" colSpan="2"></td>
					</tr>
					<!--End Harmanjeet-->
					<tr>
						<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" ></cmsb:cmsbutton>&nbsp;
							<cmsb:cmsbutton class="clsButton" id="btnBackToReinsurer" runat="server" Text="Back To Carrier Details"
								CausesValidation="false" visible="True" ></cmsb:cmsbutton></td>
						<td class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" ></cmsb:cmsbutton></td>
					</tr>
				</TBODY>
			</TABLE>
			</TD></TR></TBODY></TABLE><INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidREIN_BANK_DETAIL_ID" type="hidden" value="0" name="hidREIN_BANK_DETAIL_ID"
				runat="server"> <INPUT id="hidReset" type="hidden" value="0" name="hidReset" runat="server">
			<INPUT id="hidCompany_ID" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidAddress1" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidAddress2" type="hidden" name="hidIS_ACTIVE" runat="server"> <INPUT id="hidCity" type="hidden" value="0" name="hidUSER_TYPE_ID" runat="server">
			<INPUT id="hidState" type="hidden" name="hidAGENCY_CODE" runat="server"> <INPUT id="hidCountry" type="hidden" value="0" name="hidUSER_ID" runat="server">
			<INPUT id="hidZip" type="hidden" name="hidAGENCY_ADD2" runat="server"> <INPUT id="hidM_Address1" type="hidden" name="hidAGENCY_CITY" runat="server">
			<INPUT id="hidM_Address2" type="hidden" name="hidAGENCY_STATE" runat="server"> <INPUT id="hidM_City" type="hidden" name="hidAGENCY_COUNTRY" runat="server">
			<INPUT id="hidM_State" type="hidden" name="hidAGENCY_PHONE" runat="server"> <INPUT id="hidM_Country" type="hidden" name="hidAGENCY_ZIP" runat="server">
			<INPUT id="hidM_Zip" type="hidden" name="hidAGENCY_FAX" runat="server">
			<INPUT id="hidREIN_COMAPANY_ID" type="hidden" name="hidREIN_COMAPANY_ID" runat="server">
			<INPUT id="hidCarriername" type="hidden" name="hidCarriername" runat="server">
			<INPUT id="hidcarriercode" type="hidden" name="hidcarriercode" runat="server">
			<INPUT id="hidcarrierType" type="hidden" name="hidcarrierType" runat="server">
			<INPUT id="hidCheckAdd" type="hidden" name="hidCheckAdd" value="0" runat="server">
		</FORM>
		<script>
		//RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidREIN_BANKING_DETAIL_ID').value,true);
		
			
		</script>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM></TR></TBODY></TABLE></TR></TBODY></TABLE></FORM></TR></TBODY></TABLE></TR></TBODY></TABLE></FORM></TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</BODY>
</HTML>

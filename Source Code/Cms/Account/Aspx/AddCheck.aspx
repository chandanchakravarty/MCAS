<%@ Page validateRequest=false language="c#" Codebehind="AddCheck.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AddCheck" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>ACT_CHECK_INFORMATION</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/webcommon.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		
		function AddData(DisableButtons)
		{
			ChangeColor();
			DisableValidators();
			document.getElementById('txtPAYEE_ADD1').value  = '';
			document.getElementById('txtPAYEE_ADD2').value  = '';
			document.getElementById('txtPAYEE_CITY').value  = '';
			document.getElementById('cmbPAYEE_STATE').options.selectedIndex = -1;
			document.getElementById('txtPAYEE_ZIP').value  = '';
			document.getElementById('hidCHECK_ID').value	=	'New';
			document.getElementById('cmbACCOUNT_ID');
			document.getElementById('cmbACCOUNT_ID').options.selectedIndex = -1;
			document.getElementById('txtCHECK_NUMBER').value  = '';
			document.getElementById('txtCHECK_DATE').value  = '<%=DateTime.Now.ToString("MM/dd/yyyy")%>';
			document.getElementById('txtCHECK_AMOUNT').value  = '';
			document.getElementById('txtCHECK_NOTE').value  = '';
			document.getElementById('txtPAYEE_ENTITY_ID').value  = '';
			document.getElementById('txtCUSTOMER_ID').value  = '';
			document.getElementById('txtPOLICY_ID').value  = '';
			document.getElementById('cmbCHECKSIGN_1').options.selectedIndex = -1;
			document.getElementById('cmbCHECKSIGN_2').options.selectedIndex = -1;
			document.getElementById('txtCHECK_MEMO').value  = '';
			if(DisableButtons)
			{
				if(document.getElementById('btnDelete')!=null)
					document.getElementById('btnDelete').setAttribute('disabled',true);
				if(document.getElementById('btnDistribute')!=null)
					document.getElementById('btnDistribute').setAttribute('disabled',true);
				if(document.getElementById('btnCommit')!=null)
					document.getElementById('btnCommit').setAttribute('disabled',true);
			}
			ChangeColor();
		}
		
		function populateXML()
		{
			if('<%=Request.QueryString["CalledFrom"]%>' != 'Register')
				document.getElementById('btnPrint').style.display = 'none';			
			else
				document.getElementById('btnPrint').style.display = 'inline';			
			
			document.getElementById('CHECK_MEMO').style.display = 'none'
			DisableValidators();
			var tempXML = document.getElementById('hidOldData').value;
			if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1' ) //|| document.getElementById('hidFormSaved').value == '3')
			{
				if(tempXML!="")
				{
					populateFormData(tempXML,ACT_CHECK_INFORMATION);
					
					var CheckTypeID = document.getElementById('cmbCHECK_TYPE').options[document.getElementById('cmbCHECK_TYPE').selectedIndex].value;
					document.getElementById('txtCHECK_AMOUNT').value = formatCurrency(document.getElementById('txtCHECK_AMOUNT').value);
					SetCheckType();
				}
				else
				{
					AddData(true);
				}
			}
			SetPayeeLookup(null);
			ChangeColor();
			
		}
		// set the check type 
		function SetCheckType()
		{
			if(document.getElementById('cmbCHECK_TYPE')!=null)
				document.getElementById('cmbCHECK_TYPE').style.display="none";
			if(document.getElementById('lblCHECK_TYPE')!=null)
				document.getElementById('lblCHECK_TYPE').style.display="inline";
				//Changes for #5615 by Shikha 
				//document.getElementById('lblCHECK_TYPE').innerText	=	document.getElementById('cmbCHECK_TYPE').options[document.getElementById('cmbCHECK_TYPE').selectedIndex].text;			
				// Reference Policy Lookup Icon to be Visible Conditionally
				var chkType =document.getElementById('cmbCHECK_TYPE').options[document.getElementById('cmbCHECK_TYPE').selectedIndex].value;
				if(chkType == '9935' || chkType == '9936' || chkType == '2474') // All 'Return Premium Checks' Types
				{
					//alert(chkType);
					document.getElementById('lblCHECK_TYPE').innerText	=	"Premium Refund Check";
					document.getElementById('Img3').style.display="none";	
					document.getElementById('trRefCustAndRefPol').style.display='inline';
				}
				else
				{
					document.getElementById('trRefCustAndRefPol').style.display='none';
					document.getElementById('lblCHECK_TYPE').innerText	=	document.getElementById('cmbCHECK_TYPE').options[document.getElementById('cmbCHECK_TYPE').selectedIndex].text;			
				}
		}
		var payeeLookupType="";
		function SetPayeeLookup(ComboCtrl)
		{
		
			var CheckTypeID = document.getElementById('cmbCHECK_TYPE').options[document.getElementById('cmbCHECK_TYPE').selectedIndex].value;
			if(CheckTypeID==9940 || CheckTypeID==9941 || CheckTypeID==9942 || CheckTypeID==9943 || CheckTypeID==9937)
			{
				payeeLookupType="TEXT_FIELD";
				document.getElementById('txtPAYEE_ENTITY_ID').setAttribute('disabled',false);
				if(ComboCtrl!=null)
					document.getElementById('txtPAYEE_ENTITY_ID').value ="";
				document.getElementById('imgPayee').style.display="none";
				
			}
			else
			{
			
				document.getElementById('txtPAYEE_ENTITY_ID').setAttribute('disabled',false);
				if(ComboCtrl!=null)
					document.getElementById('txtPAYEE_ENTITY_ID').value ="";
				document.getElementById('imgPayee').style.display="inline";
			}
			if(CheckTypeID==2472)
				payeeLookupType="AGENCY";
			if(CheckTypeID==2474 || CheckTypeID==9935 || CheckTypeID==9936)
				payeeLookupType="CUSTOMER";
			if(CheckTypeID==9938)
				payeeLookupType="VENDOR";
			if(CheckTypeID==9939)
				payeeLookupType="TAX";
				
			HideReinsurancePayee();
		}
		function OpenPayeeLookup()
		{
			var ID;
			switch(payeeLookupType)
			{
			case "AGENCY":
				ID='AGENCY_ID';
				Text='Name1';
				XMLTag='AgencyLookup';
				Heading = 'Agency Names';
				break;
			case "CUSTOMER":
				ID='CUSTOMER_ID';
				Text='Name1';
				XMLTag='CustLookupForm';
				Heading = 'Customer Names';
				break;
			case "VENDOR":
				ID='VENDOR_ID';
				Text='Name1';
				XMLTag='VendorLookup';
				Heading = 'Vendor Names';
				break;
			case "TAX":
				ID='TAX_ID';
				Text='Name1';
				XMLTag='TaxLookup';
				Heading = 'Tax Entity Names';
				break;
			}
			if(ID!=null && ID.length>0)

				OpenLookupWithFunction('<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>',ID,Text,'hidPAYEE_ENTITY_ID_HID','txtPAYEE_ENTITY_ID',XMLTag,Heading,'','SetPayeeInfo()');
			else
				alert("Please select a check type first.");
		}
		function SetPayeeInfo()
		{

			var info = document.getElementById('txtPAYEE_ENTITY_ID').value.split("^");
			if(typeof(info[0])!='undefined')
				document.getElementById('txtPAYEE_ENTITY_ID').value = info[0];
			if(typeof(info[1])!='undefined')
				document.getElementById('txtPAYEE_ADD1').value = info[1];
			if(typeof(info[2])!='undefined')
				document.getElementById('txtPAYEE_ADD2').value = info[2];
			if(typeof(info[3])!='undefined')
				document.getElementById('txtPAYEE_CITY').value = info[3];
			if(typeof(info[4])!='undefined')
				document.getElementById('cmbPAYEE_STATE').value = info[4];
			if(typeof(info[5])!='undefined')
				document.getElementById('txtPAYEE_ZIP').value = info[5];
		}

		function splitCustomer()
		{
			document.getElementById('hidPOLICY_ID_HID').value		=	'0' ;
			document.getElementById('hidPOLICY_VERSION_ID').value	=	'0';
			document.getElementById('txtPOLICY_ID').value = '';
		}
		function splitPolicy()
		{
			var PolicyAppNumber = document.getElementById('hidPOLICY_APP_NUMBER').value.split('~');
			document.getElementById('hidPOLICY_ID_HID').value		=	PolicyAppNumber[0] ;
			document.getElementById('hidPOLICY_VERSION_ID').value	=	PolicyAppNumber[1];
			document.getElementById('txtPOLICY_ID').value = PolicyAppNumber[2];
			
			var CustomerIdName	=	document.getElementById('hidCUSTOMER_ID_NAME').value.split('~');
			document.getElementById('hidCUSTOMER_ID_HID').value = CustomerIdName[0];
			document.getElementById('txtCUSTOMER_ID').value = CustomerIdName[1];
			
		}
		function OpenPolicyLookup(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction)
		{
			var custID = document.getElementById('hidCUSTOMER_ID_HID').value;
			
			if(custID == null || custID == '' || custID == '0')
			{
				OpenLookupWithFunction('<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>','POLICY_APP_NUMBER','CUSTOMER_ID_NAME','hidPOLICY_APP_NUMBER','hidCUSTOMER_ID_NAME','Policy','Policy','','splitPolicy()');
			}
			else
			{
				OpenLookupWithFunction('<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>','POLICY_APP_NUMBER','CUSTOMER_ID_NAME','hidPOLICY_APP_NUMBER','hidCUSTOMER_ID_NAME','PolicyCustomer','Policy','@customerID1='+custID,'splitPolicy()');
			}
		}
		function OpenCustomerLookup()
		{
			
			OpenLookupWithFunction('<%=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL()%>','CUSTOMER_ID','Name','hidCustomer_ID','txtCUSTOMER_ID','CustLookupForm','Customer','','splitCustomer()');
		}

		function OpenDistributeCheck()
		{
				populateXML();
				var calledFrom = '<%=Request.QueryString["CalledFrom"]%>';
				var url="DistributeCashReceipt.aspx?GROUP_ID="+document.getElementById('hidCHECK_ID').value+"&GROUP_TYPE=CHQ&DISTRIBUTION_AMOUNT="+document.getElementById('txtCHECK_AMOUNT').value+"&CalledFrom="+calledFrom;	
				ShowPopup(url,'DistributeCheck',960,500);	
				return false;
		}
		function SaveClicked()
		{
			Page_ClientValidate();
			amount= document.getElementById('txtCHECK_AMOUNT').value;
			whole=amount.substring(0,amount.indexOf('.'));
			while(whole.indexOf(",")>-1)
					whole = whole.replace(",","");
			if(whole.length>9)
			{
				document.getElementById('revCHECK_AMOUNT').style.display = "inline";
				return false;
			}
				
			document.getElementById('IsSaveClicked').value='True';
			document.getElementById('txtPAYEE_ENTITY_ID').setAttribute('disabled',false);
		}
		function AccountIDIndexChanged()
		{
			if(this.selectedIndex==0)
				return false;
			else
				document.getElementById('txtPAYEE_ENTITY_ID').setAttribute('disabled',false);
		}
		function ChkTextAreaLength(source, arguments)
		{
			var txtArea = arguments.Value;
			if(txtArea.length > 100 ) 
			{
				arguments.IsValid = false;
				return;   // invalid userName
			}
		}	

		// fxn to show check info fields in read-only mode if page called from check register
		function ShwDisabledCheckInfo()
		{
			
			document.getElementById('cmbACCOUNT_ID').setAttribute('disabled',true);
			document.getElementById('txtCHECK_AMOUNT').setAttribute('disabled',true);
			document.getElementById('txtPAYEE_ADD1').setAttribute('disabled',true);
			document.getElementById('txtPAYEE_ADD2').setAttribute('disabled',true);
			document.getElementById('txtPAYEE_CITY').setAttribute('disabled',true);
			document.getElementById('cmbPAYEE_STATE').setAttribute('disabled',true);
			document.getElementById('txtPAYEE_ZIP').setAttribute('disabled',true);
			document.getElementById('txtPAYEE_ENTITY_ID').setAttribute('disabled',true);
			document.getElementById('txtPAYEE_ENTITY_ID').setAttribute('readOnly',true);
			document.getElementById('imgPayee').setAttribute('disabled',true);
			document.getElementById('cmbCHECKSIGN_1').setAttribute('disabled',true);
			document.getElementById('cmbCHECKSIGN_2').setAttribute('disabled',true);
			document.getElementById('txtCHECK_NOTE').setAttribute('disabled',true);	
			document.getElementById('txtCUSTOMER_ID').setAttribute('disabled',true);	
			document.getElementById('Img2').setAttribute('disabled',true);
			
			document.getElementById('btnReset').style.display = "none";
			document.getElementById('btnSave').style.display = "none";
			document.getElementById('btnDistribute').style.display = "none";
			document.getElementById('btnDelete').style.display = "none";
			document.getElementById('btnCommit').style.display = "none";


		}
		//Formats the amount and convert 111 into 1.11
		function InsertDecimalAmt(AmtValues)
		{
			AmtValues = ReplaceAll(AmtValues,".","");
			DollarPart = AmtValues.substring(0, AmtValues.length - 2);
			CentPart = AmtValues.substring(AmtValues.length - 2);
			//tmp = formatCurrency(DollarPart) + "." + CentPart;
			tmp = DollarPart + "." + CentPart;
			return tmp;
		}
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
					
					txtAmount.value = InsertDecimalAmt(amt);
				}
			}
		}
		
		function Reset()
		{
			DisableValidators();
			document.ACT_CHECK_INFORMATION.reset();
			populateXML();
			return false;
		}
		function ReadOnlyCheckAmount()
		{
			var CheckTypeID = document.getElementById('cmbCHECK_TYPE').options[document.getElementById('cmbCHECK_TYPE').selectedIndex].value;
			var CheckAmtCtrl = document.getElementById('txtCHECK_AMOUNT');
			if(CheckTypeID == 9940 || CheckTypeID == 9945) // Misc & Re-Ins Checks // Editable
			{
				CheckAmtCtrl.readOnly = false;
				document.getElementById('btnDistribute').style.display = 'inline';
			}
			else
			{
				CheckAmtCtrl.readOnly = true;
				document.getElementById('btnDistribute').style.display = 'none';
			}
  
		}
		function HideReinsurancePayee()
		{
			var CheckTypeID = document.getElementById('cmbCHECK_TYPE').options[document.getElementById('cmbCHECK_TYPE').selectedIndex].value;
			
			if(CheckTypeID = 9945)
			{
				document.getElementById('imgPayee').style.display="none";
			}
			//Check for Checks for Cancellation and Change Premium Payment (Ravinder Mail)
			if(CheckTypeID = 2474)
			{
				document.getElementById('Img2').style.display="none";
			}
			
		}
		
		</script>
	</HEAD>
	<BODY oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();ReadOnlyCheckAmount();HideReinsurancePayee();">
		<FORM id="ACT_CHECK_INFORMATION" method="post" runat="server">
			<P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
						mandatory
					</TD>
				</tr>
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR>
					<TD>
						<TABLE id="tblBody" width="100%" align="center" border="0" runat="server">
							<TBODY>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capACCOUNT_ID" runat="server">Bank Accounts</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbACCOUNT_ID" onfocus="SelectComboIndex('cmbACCOUNT_ID')" runat="server" onchange="AccountIDIndexChanged();"
											AutoPostBack="True">
											<asp:ListItem Value="0">0</asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvACCOUNT_ID" runat="server" Display="Dynamic" ErrorMessage="ACCOUNT_ID can't be blank."
											ControlToValidate="cmbACCOUNT_ID"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capCHECK_TYPE" runat="server">Check Type</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCHECK_TYPE" onfocus="SelectComboIndex('cmbCHECK_TYPE')" runat="server" onchange="SetPayeeLookup(this);"></asp:dropdownlist><asp:label id="lblCHECK_TYPE" CssClass="labelfont" Runat="server"></asp:label><BR>
										<asp:requiredfieldvalidator id="rfvCHECK_TYPE" runat="server" Display="Dynamic" ErrorMessage="CHECK_TYPE can't be blank."
											ControlToValidate="cmbCHECK_TYPE"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCHECK_NUMBER" runat="server">Check Number</asp:label></TD>
									<TD class="midcolora" width="32%">
										<!--Lookup code--><asp:label id="lblCheckNumber" runat="server">To be assigned at printing</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:label id="capCHECK_DATE" runat="server">Check Date</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblChkDate" runat="server"></asp:label></TD>
								</tr>
								<tr>
									<TD class="midcolora" style="HEIGHT: 18px" width="18%"><asp:label id="capCHECK_AMOUNT" runat="server">Check Amount</asp:label><SPAN class="mandatory">*</SPAN></TD>
									<TD class="midcolora" style="HEIGHT: 18px" width="32%"><asp:textbox class="inputCurrency" id="txtCHECK_AMOUNT" runat="server" maxlength="12" size="30"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvCHECK_AMOUNT" runat="server" Display="Dynamic" ErrorMessage="CHECK_AMOUNT can't be blank."
											ControlToValidate="txtCHECK_AMOUNT"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revCHECK_AMOUNT" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtCHECK_AMOUNT"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" style="HEIGHT: 18px" width="18%"></TD>
									<TD class="midcolora" style="HEIGHT: 18px" width="32%"></TD>
								</tr>
								<tr id="CHECK_MEMO">
									<TD class="midcolora" width="18%"><asp:label id="capCHECK_MEMO" runat="server">Memo to Print On Check</asp:label></TD>
									<TD class="midcolora" colSpan="2"><asp:textbox id="txtCHECK_MEMO" runat="server" maxlength="70" width="100%"></asp:textbox></TD>
									<TD class="midcolora" colSpan="1"></TD>
								</tr>
								<tr>
									<td class="headrow" colSpan="4">Payee Information
									</td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPAYEE_ENTITY_ID" runat="server">Payee</asp:label><SPAN class="mandatory">*</SPAN></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPAYEE_ENTITY_ID" runat="server" maxlength="30" size="30"></asp:textbox><IMG id="imgPayee" style="CURSOR: hand" onclick="OpenPayeeLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
											runat="server">
										<asp:requiredfieldvalidator id="rfvPAYEE_ENTITY_ID" runat="server" Display="Dynamic" ErrorMessage="PAYEE_ENTITY_ID can't be blank."
											ControlToValidate="txtPAYEE_ENTITY_ID"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" colSpan="2"></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPAYEE_ADD1" runat="server">Payee Address1</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPAYEE_ADD1" runat="server" maxlength="140" size="30"></asp:textbox>
										<%--		<BR>	<asp:requiredfieldvalidator id="rfvPAYEE_ADD1" runat="server" ControlToValidate="txtPAYEE_ADD1" ErrorMessage="PAYEE_ADD1 can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator>--%>
									</TD>
									<TD class="midcolora" noWrap width="18%"><asp:label id="capPAYEE_ADD2" runat="server">Payee Address2</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPAYEE_ADD2" runat="server" maxlength="140" size="30"></asp:textbox><BR>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPAYEE_CITY" runat="server">Payee City</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPAYEE_CITY" runat="server" maxlength="80" size="30"></asp:textbox>
										<%--<BR><asp:requiredfieldvalidator id="rfvPAYEE_CITY" runat="server" ControlToValidate="txtPAYEE_CITY" ErrorMessage="PAYEE_CITY can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator>--%>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capPAYEE_STATE" runat="server">Payee State</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPAYEE_STATE" onfocus="SelectComboIndex('cmbPAYEE_STATE')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist>
										<%--<BR><asp:requiredfieldvalidator id="rfvPAYEE_STATE" runat="server" ControlToValidate="cmbPAYEE_STATE" ErrorMessage="PAYEE_STATE can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator>--%>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPAYEE_ZIP" runat="server">Payee Zip</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPAYEE_ZIP" runat="server" maxlength="24" size="30"></asp:textbox>
										<%-- Added by Swarup on 30-mar-2007 --%>
										<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
											<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
										</asp:hyperlink><BR>
										<%--	<asp:requiredfieldvalidator id="rfvPAYEE_ZIP" runat="server" ControlToValidate="txtPAYEE_ZIP" ErrorMessage="PAYEE_ZIP can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator>--%>
										<asp:regularexpressionvalidator id="revPAYEE_ZIP" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtPAYEE_ZIP"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" colSpan="2"></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCHECK_NOTE" runat="server">Notes</asp:label></TD>
									<TD class="midcolora" colSpan="2"><asp:textbox id="txtCHECK_NOTE" runat="server" maxlength="500" width="248px" Height="56px" TextMode="MultiLine"></asp:textbox><br>
										<asp:customvalidator id="csvCHECK_NOTE" Display="Dynamic" ControlToValidate="txtCHECK_NOTE" Runat="server"
											ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator></TD>
									<TD class="midcolora" colSpan="1"></TD>
								</tr>
								<tr id="trRefCustAndRefPol">
									<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_ID" runat="server">Reference Customer</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCUSTOMER_ID" runat="server" maxlength="4" size="30" ReadOnly="True"></asp:textbox><IMG id="Img2" style="CURSOR: hand" onclick="OpenCustomerLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
											runat="server"></TD>
									<TD class="midcolora" width="18%"><asp:label id="capPOLICY_ID" runat="server">Reference Policy</asp:label></TD>
									<TD class="midcolora" width="32%">
										<!--Lookup code--> &nbsp;<asp:textbox id="txtPOLICY_ID" runat="server" maxlength="2" size="12" ReadOnly="True"></asp:textbox><IMG id="Img3" style="CURSOR: hand" onclick="OpenPolicyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
											runat="server"></TD>
								</tr>
								<tr>
									<TD class="midcolora" style="HEIGHT: 1px" width="18%"><asp:label id="capCHECKSIGN_1" runat="server">Signature File 1</asp:label></TD>
									<TD class="midcolora" style="HEIGHT: 1px" width="32%">
										<!--Lookup code--> &nbsp;<asp:dropdownlist id="cmbCHECKSIGN_1" onfocus="SelectComboIndex('cmbCHECKSIGN_1')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist></TD>
									<TD class="midcolora" style="HEIGHT: 1px" width="18%"><asp:label id="capCHECKSIGN_2" runat="server">Signature File 2</asp:label></TD>
									<TD class="midcolora" style="HEIGHT: 1px" width="32%"><asp:dropdownlist id="cmbCHECKSIGN_2" onfocus="SelectComboIndex('cmbCHECKSIGN_2')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist></TD>
										
								</tr>
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" CausesValidation="false"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnDistribute" runat="server" Text="Distribute"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnCommit" runat="server" Text="Commit Check"></cmsb:cmsbutton>
									</td>
									<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
								<tr><td class="midcolorr" colSpan="4"><input type="button" id="btnPrint" class="clsButton" onclick="window.print();" value="Print"></input></td></tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCHECK_ID" type="hidden" name="hidCHECK_ID" runat="server">
			<INPUT id="hidPOLICY_ID_HID" type="hidden" name="hidPOLICY_ID_HID" runat="server">
			<INPUT id="hidCUSTOMER_ID_HID" type="hidden" name="hidCUSTOMER_ID_HID" runat="server">
			<INPUT id="hidPAYEE_ENTITY_ID_HID" type="hidden" name="hidPAYEE_ENTITY_ID_HID" runat="server">
			<INPUT id="hidPOLICY_APP_NUMBER" type="hidden" name="hidPOLICY_APP_NUMBER" runat="server">
			<INPUT id="hidCUSTOMER_ID_NAME" type="hidden" name="hidCUSTOMER_ID_NAME" runat="server">
			<INPUT id="hidPOLICY_VERSION_ID" type="hidden" name="hidPOLICY_VERSION_ID" runat="server">
			<INPUT id="IsSaveClicked" type="hidden" value="False" name="IsSaveClicked">
		</FORM>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidCHECK_ID').value);
		</script>
	</BODY>
</HTML>

<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Page language="c#" Codebehind="Email.aspx.cs" validateRequest=false  AutoEventWireup="false" Inherits="Cms.Client.Aspx.Email" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Cms.BusinessLayer.BlCommon" Assembly="blcommon" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Email</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript">
		function ClearText(e)
		{
			event.returnValue=false;
			return false;			
		}	
		function OpenAppLookup(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction)
		{
		    var strApplication = document.getElementById('hidApplication').value;
			var custID = document.getElementById('hidCUSTOMER_ID').value;
			if(custID == null || custID == '' || custID == '0')
			    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'APP_NUMBER_ID', 'CUSTOMER_ID_NAME', 'hidAPP_NUMBER_ID', 'hidCUSTOMER_ID_NAME', 'Application', strApplication, '', 'splitApplication()');
			else
			    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'APP_NUMBER_ID', 'CUSTOMER_ID_NAME', 'hidAPP_NUMBER_ID', 'hidCUSTOMER_ID_NAME', 'ApplicationCustomer', strApplication, '@CUSTID=' + custID, 'splitApplication()');
		}
		function OpenPolicyLookup(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction)
		{
			var custID = document.getElementById('hidCUSTOMER_ID').value;
			var appID = document.getElementById('hidAPP_ID').value;
			var appVersionID = document.getElementById('hidAPP_VERSION_ID').value;
			var strPolicy = document.getElementById('hidPolicy').value;
			
			if(custID == null || custID == '' || custID == '0') {
			   
			    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'POLICY_APP_NUMBER', 'CUSTOMER_ID_NAME', 'hidPOLICY_APP_NUMBER', 'hidCUSTOMER_ID_NAME', 'Policy', strPolicy, '', 'splitPolicy()');
			}
			else
			{
				if(appID == null || appID == '' || appID == '0')
				    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'POLICY_APP_CUSTOMER_ID_NAME', 'CUSTOMER_ID_NAME', 'hidPOLICY_APP_NUMBER', 'hidCUSTOMER_ID_NAME', 'PolicyCustomer', strPolicy, '@customerID1=' + custID, 'splitPolicy()');
				else
				    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'POLICY_APP_CUSTOMER_ID_NAME', 'CUSTOMER_ID_NAME', 'hidPOLICY_APP_NUMBER', 'hidCUSTOMER_ID_NAME', 'PolicyCustomerApplication', strPolicy, '@customerID2=' + custID + ';@APPID=' + appID + ';@APPVERSION=' + appVersionID, 'splitPolicy()');
			}
		}
		
		function OpenClaimLookup(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction)
		{
			var custID = document.getElementById('hidCUSTOMER_ID').value;
			//var policyID = document.getElementById('hidPOLICY_ID').value;
			//var policyVersionID = document.getElementById('hidPOLICY_VERSION_ID').value;
			//alert("custID  " + custID);
			var strClaim = document.getElementById('hidClaim').value;
			if(custID == null || custID == '' || custID == '0')
			{
				alert('Lookup Not Available for Unmatched Policy Claims');
			}
			else
			{
			    OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'CLAIM_POLICY_NUMBER', 'CUSTOMER_ID_NAME', 'hidCLAIM_POLICY_NUMBER', 'hidCUSTOMER_ID_NAME', 'ClaimPolicyCustomer', strClaim, '@customerID2=' + custID, 'splitClaim()');
			}
		}
		
			function splitClaim()
			{
				var ClaimPolicyNumber = document.getElementById('hidCLAIM_POLICY_NUMBER').value.split('~');
				document.getElementById('hidCLAIM_ID').value		=	ClaimPolicyNumber[0];
				document.getElementById('hidCLAIM_NUMBER').value	=	ClaimPolicyNumber[1];
				document.getElementById('txtCLAIM_NUMBER').value	=	ClaimPolicyNumber[1];
				
				if(ClaimPolicyNumber[2])
				{
					document.getElementById('hidPOLICY_ID').value	=	ClaimPolicyNumber[2];
					document.getElementById('hidPOLICY_VERSION_ID').value=	ClaimPolicyNumber[4];
					document.getElementById('txtPOLICY_NUMBER').value=	ClaimPolicyNumber[3];
				}
				
				var CustomerIdName	=	document.getElementById('hidCUSTOMER_ID_NAME').value.split('~');
				document.getElementById('hidCUSTOMER_ID').value = CustomerIdName[0];
				//document.getElementById('txtEMAIL_SUBJECT').value = CustomerIdName[1] + '/' + document.getElementById('txtAPP_NUMBER').value + '/' + document.getElementById('txtPOLICY_NUMBER').value;
				document.getElementById('txtEMAIL_SUBJECT').value = CustomerIdName[1] + '/' + document.getElementById('txtPOLICY_NUMBER').value;
			}
		
		   function splitPolicy()
			{
				var PolicyAppNumber = document.getElementById('hidPOLICY_APP_NUMBER').value.split('~');
				document.getElementById('hidPOLICY_ID').value		=	PolicyAppNumber[0] ;
				document.getElementById('hidPOLICY_VERSION_ID').value	=	PolicyAppNumber[1];
				document.getElementById('txtPOLICY_NUMBER').value = PolicyAppNumber[2];
				
				if(PolicyAppNumber[5])
				{
					document.getElementById('hidAPP_ID').value		=	PolicyAppNumber[3] ;
					document.getElementById('hidAPP_VERSION_ID').value	=	PolicyAppNumber[4];
					document.getElementById('txtAPP_NUMBER').value = PolicyAppNumber[5];
				}
				
				var CustomerIdName	=	document.getElementById('hidCUSTOMER_ID_NAME').value.split('~');
				document.getElementById('hidCUSTOMER_ID').value = CustomerIdName[0];
				//document.getElementById('txtEMAIL_SUBJECT').value = CustomerIdName[1] + '/' + document.getElementById('txtAPP_NUMBER').value + '/' + document.getElementById('txtPOLICY_NUMBER').value;
				document.getElementById('txtEMAIL_SUBJECT').value = CustomerIdName[1] + '/' + document.getElementById('txtPOLICY_NUMBER').value;
			}
			function splitApplication()
			{
				var AppNumberId		=	document.getElementById('hidAPP_NUMBER_ID').value.split('~');
				document.getElementById('hidAPP_ID').value		=	AppNumberId[0] ;
				document.getElementById('hidAPP_VERSION_ID').value	=	AppNumberId[1];
				document.getElementById('txtAPP_NUMBER').value = AppNumberId[2];
				
				var CustomerIdName	=	document.getElementById('hidCUSTOMER_ID_NAME').value.split('~');
				document.getElementById('hidCUSTOMER_ID').value = CustomerIdName[0];
				document.getElementById('txtEMAIL_SUBJECT').value = CustomerIdName[1] + '/' + document.getElementById('txtAPP_NUMBER').value;
				
			}
			function OpenQuoteLookup(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction)
			{
				var custID = document.getElementById('hidCUSTOMER_ID').value;
				var appID = document.getElementById('hidAPP_ID').value;
				var appVersionID = document.getElementById('hidAPP_VERSION_ID').value;
				var strQuote = document.getElementById('hidQuote').value;
				OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>', 'QQ_NUMBER', 'POLICY_APP_CUSTOMER_ID_NAME', 'hidQQ_ID', 'hidPOLICY_APP_CUSTOMER_ID_NAME', 'QuoteCust', strQuote, '@CUSTID=' + custID, 'splitQuote()');
				
				
		}	
		function splitQuote()
			{
				//alert(document.getElementById('hidPOLICY_APP_CUSTOMER_ID_NAME').value);
				var QuoteNumber		=	document.getElementById('hidPOLICY_APP_CUSTOMER_ID_NAME').value.split('~');
				
					
				document.getElementById('txtQuote').value = QuoteNumber[1];
				document.getElementById('hidQQ_ID').value = QuoteNumber[0];	// Quote ID
				
				if(QuoteNumber[2]!= '');
				{
					
					document.getElementById('hidPOLICY_ID').value = QuoteNumber[2] ;
					document.getElementById('hidPOLICY_VERSION_ID').value	= QuoteNumber[3] ;
					document.getElementById('txtPOLICY_NUMBER').value		=	QuoteNumber[4] ;
				}
				
				if(QuoteNumber[5]!= '');
				{
					document.getElementById('hidAPP_ID').value		=	QuoteNumber[5] ;
					document.getElementById('hidAPP_VERSION_ID').value	=	QuoteNumber[6];
					document.getElementById('txtAPP_NUMBER').value = QuoteNumber[7];
					//document.getElementById('hidAPP_ID').value = QuoteNumber[5];
				}
						
				//document.getElementById('txtCUSTOMER_NAME').value = QuoteNumber[9];	
				document.getElementById('hidCUSTOMER_ID').value = QuoteNumber[8];
				
				//document.getElementById('hidAPP_VERSION_ID').value = QuoteNumber[6];	
				//document.getElementById('hidPOLICY_VERSION_ID').value = QuoteNumber[3];	
				/*var CustomerIdName	=	document.getElementById('hidPOLICY_APP_CUSTOMER_ID_NAME').value.split('~');
				document.getElementById('hidCUSTOMER_ID').value = CustomerIdName[8];*/
				document.getElementById('txtEMAIL_SUBJECT').value = QuoteNumber[9] + '/' + document.getElementById('txtQuote').value;					
								
			}	
		function ShowFOLLOW_UP_DATE()
		{

			
			if  (document.getElementById('cmbDIARY_ITEM_REQ').options[document.getElementById('cmbDIARY_ITEM_REQ').options.selectedIndex].value == 'Y')
			{
				document.getElementById('txtFOLLOW_UP_DATE').style.display='inline';	
				document.getElementById('capFOLLOW_UP_DATE').style.display='inline';
				document.getElementById('imgFOLLOW_UP_DATE').style.display='inline';
				document.getElementById('trDIARY_ITEM_TO').style.display='inline'; 
				if (document.getElementById('rfvFOLLOW_UP_DATE') != null)
					{
						document.getElementById('rfvFOLLOW_UP_DATE').setAttribute('enabled',true);
						if (document.getElementById('rfvFOLLOW_UP_DATE').isvalid == false)
							document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'inline';
					} 	
		         
				if (document.getElementById('spnFOLLOW_UP_DATE') != null)
					{
						document.getElementById('spnFOLLOW_UP_DATE').style.display = "inline";
					}
				
			}  
			else
		    {
				  	
		          document.getElementById('txtFOLLOW_UP_DATE').style.display='none';
		          document.getElementById('capFOLLOW_UP_DATE').style.display='none';
		          document.getElementById('imgFOLLOW_UP_DATE').style.display='none'; 
      			document.getElementById('trDIARY_ITEM_TO').style.display='none'; 
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
		    
		     }     
		}	
		function ChkDate(objSource , objArgs)
		{
			var expdate=document.getElementById('txtFOLLOW_UP_DATE').value;
			//objArgs.IsValid = DateComparer(expdate,"<%=System.DateTime.Now.AddDays(-1)%>",'mm/dd/yyyy');   //jsaAppDtFormat
			objArgs.IsValid=true;
			
		}
	
		
		function selectRecipients()
		{
				
			for (var i=document.getElementById('cmbCONTACTDETAILS').options.length-1;i>=0;i--)
			{
			
					if (document.getElementById('cmbCONTACTDETAILS').options[i].selected == true)
					{
						if(document.getElementById('cmbCONTACTDETAILS').options[i].value == "")
						{
						//do nothing					
						}
						else
						{
						document.getElementById('cmbRECIPIENTS').options.length=document.getElementById('cmbRECIPIENTS').length+1;
						document.getElementById('cmbRECIPIENTS').options[document.getElementById('cmbRECIPIENTS').length-1].value=document.getElementById('cmbCONTACTDETAILS').options[i].value;
						document.getElementById('cmbRECIPIENTS').options[document.getElementById('cmbRECIPIENTS').length-1].text=document.getElementById('cmbCONTACTDETAILS').options[i].text;
						document.getElementById('cmbCONTACTDETAILS').options[i] = null;
						}
					}
		  	}
		  	
			if (document.getElementById('txtADDITIONAL').value != '' && document.getElementById('revADDITIONAL').getAttribute('IsValid'))
			{
				document.getElementById('cmbRECIPIENTS').options.length=document.getElementById('cmbRECIPIENTS').length+1;
				document.getElementById('cmbRECIPIENTS').options[document.getElementById('cmbRECIPIENTS').length-1].value=document.getElementById('txtADDITIONAL').value;
				document.getElementById('cmbRECIPIENTS').options[document.getElementById('cmbRECIPIENTS').length-1].text=document.getElementById('txtADDITIONAL').value;
				document.getElementById('txtADDITIONAL').value='';
			} 		
			
			return false;
		  	
		}
	
		
		
		function deselectRecipients()
		{
		
		  for (var i=document.getElementById('cmbRECIPIENTS').options.length-1;i>=0;i--)
			{
			
				if (document.getElementById('cmbRECIPIENTS').options[i].selected == true)
				{
					document.getElementById('cmbCONTACTDETAILS').options.length=document.getElementById('cmbCONTACTDETAILS').length+1;
					document.getElementById('cmbCONTACTDETAILS').options[document.getElementById('cmbCONTACTDETAILS').length-1].value=document.getElementById('cmbRECIPIENTS').options[i].value;
					document.getElementById('cmbCONTACTDETAILS').options[document.getElementById('cmbCONTACTDETAILS').length-1].text=document.getElementById('cmbRECIPIENTS').options[i].text;
					document.getElementById('cmbRECIPIENTS').options[i] = null;
				}
				
		  	}	
		  	return false;			
		
		}	
		function setRecipients()
		{
			
			document.Email.hidRECIPIENTS.value=''
			for (var i=0;i< document.getElementById('cmbRECIPIENTS').options.length;i++)
			{document.Email.hidRECIPIENTS.value = document.Email.hidRECIPIENTS.value + document.getElementById('cmbRECIPIENTS').options[i].text + ',';}							
			Page_ClientValidate();
			var returnVal = funcValidateRecipients();
			return Page_IsValid && returnVal;
		}
		
		function addRecipients()
		{
			var Recipients = document.getElementById("hidRECIPIENTS").value;
			//alert(Recipients)
			var Recipient = Recipients.split(",");
			for(j = document.getElementById('cmbRECIPIENTS').length-1; j >=0;j--)
			{
				document.getElementById('cmbRECIPIENTS').options[j]=null;
			}	
			//Changed length
			for(j = 0; j < Recipient.length ;j++)
			{
				//alert(Recipient[j]);
				if(Recipient[j]!='')
				{
					document.getElementById('cmbRECIPIENTS').options.length=document.getElementById('cmbRECIPIENTS').length+1;
					document.getElementById('cmbRECIPIENTS').options[document.getElementById('cmbRECIPIENTS').length-1].value=Recipient[j];
					document.getElementById('cmbRECIPIENTS').options[document.getElementById('cmbRECIPIENTS').length-1].text=Recipient[j];
				}	
			}
		}
				
		function populateXML()
		{
			var tempXML = document.getElementById('hidOldData').value;	
			//Added on 1 OCt 2009 to Retrieve recipients after Invalid Email(s).
			if(document.getElementById('hidInvalid_RECIPIENTS').value == '1')
			{
				addRecipients();
			}
			if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
			{
				if(document.getElementById('hidFormSaved').value == '1')
				{
					document.getElementById('trBody').style.display="inline";	
				}
				//Enabling the activate deactivate button
				if(tempXML!="" && tempXML!="0")
				{
					//document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 					
					populateFormData(tempXML,Email);
					document.getElementById("hidRECIPIENTS").value=document.getElementById("txtRECIPIENTS").value;
					addRecipients();
					if (document.getElementById('cmbDIARY_ITEM_REQ').options[document.getElementById('cmbDIARY_ITEM_REQ').options.selectedIndex].value == 'Y')
					{
						document.getElementById('txtFOLLOW_UP_DATE').style.display='inline';	
						document.getElementById('capFOLLOW_UP_DATE').style.display='inline';
						document.getElementById('imgFOLLOW_UP_DATE').style.display='inline'; 
						document.getElementById('trDIARY_ITEM_TO').style.display='inline'; 
						if (document.getElementById('rfvFOLLOW_UP_DATE') != null)
						{
							document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("enabled",true);
							document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'none';
						}
						if (document.getElementById('spnFOLLOW_UP_DATE') != null)
						{
							document.getElementById('spnFOLLOW_UP_DATE').style.display = "inline";
						}	
					}
					else
					{
						if (document.getElementById('rfvFOLLOW_UP_DATE') != null)
						{
							document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("enabled",false);
							document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'none';
						}
						if (document.getElementById('spnFOLLOW_UP_DATE') != null)
						{
							document.getElementById('spnFOLLOW_UP_DATE').style.display = "none";
						}	
					}
					document.getElementById('trBody').style.display="none";
					var objXmlHandler = new XMLHandler();
					
					var tree = (objXmlHandler.quickParseXML(document.getElementById('hidOldData').value).getElementsByTagName('Table')[0]);
					
					var i=0;
					for(i=0;i<tree.childNodes.length;i++)
					{
						if(!tree.childNodes[i].firstChild) 
							continue;
						
						var nodeName = tree.childNodes[i].nodeName;
						var nodeValue = tree.childNodes[i].firstChild.text;
					//	alert(nodeValue);
						var fileName;						
						switch(nodeName)
						{
							case "EMAIL_ATTACH_PATH":															
								//document.getElementById("lblATTACHMENT").innerHTML = nodeValue ;
								document.getElementById("lblATTACHMENT").innerHTML = "<a href='" + document.Email.hidRootPath.value + "\\" + nodeValue + "' target='blank'>" + nodeValue + "</a>";
								document.getElementById("txtATTACHMENT").style.display = "none";
								break;
							default:
								document.getElementById("txtATTACHMENT").style.display = "none";
								break;
						}
					}
				}
				else
				{
					var dt =new Date();
					document.getElementById('txtFOLLOW_UP_DATE').value = dt.getMonth()+1 +  '/' + dt.getDate() + '/' + dt.getFullYear(); 
					document.getElementById('spnFOLLOW_UP_DATE').style.display = "none";
					document.getElementById('trDIARY_ITEM_TO').style.display='none'; 
				}				
			}
		/*	else if(document.getElementById("hidFormSaved").value=="2")
			{
						addRecipients();

			}*/
			 if (document.getElementById('rfvFOLLOW_UP_DATE') != null)
					{
					//alert('else + ' + document.getElementById('rfvFOLLOW_UP_DATE').style.display);
					document.getElementById('rfvFOLLOW_UP_DATE').setAttribute("enabled",false);
					document.getElementById('rfvFOLLOW_UP_DATE').style.display = 'none';
					}						
			return false;
		}
		function funcValidateRecipients()
		{
			if(document.getElementById('cmbRECIPIENTS').options.length == 0)
			{
				document.getElementById('cmbRECIPIENTS').className = "MandatoryControl";
				document.getElementById("spnRECIPIENTS").style.display="inline";
				return false;
			}
			else
			{
				document.getElementById('cmbRECIPIENTS').className = "none";
				document.getElementById("spnRECIPIENTS").style.display="none";
				return true;
			}
		}		
	
	
		</script>
	</HEAD>
	<body oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="populateXML();ChangeColor();ApplyColor();">
		<form id="Email" method="post" runat="server">
			<TABLE width="100%" align="center" border="0">
				<TR>
					<TD class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage1" runat="server" CssClass="errmsg" Visible="False"></asp:label></TD>
				</TR>
				<TR>
					<TD class="midcolora" width="18%"><asp:label id="capEMAIL_FROM_NAME" runat="server">From Name</asp:label><SPAN class="mandatory">*</SPAN></TD>
					<TD class="midcolora" width="32%" colSpan="3"><asp:textbox id="txtEMAIL_FROM_NAME" runat="server" maxlength="50" size="30"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvFROM_NAME" runat="server" Display="Dynamic" ControlToValidate="txtEMAIL_FROM_NAME"></asp:requiredfieldvalidator></TD>
				</TR>
				<TR>
					<TD class="midcolora" width="18%"><asp:label id="capEMAIL_FROM" runat="server">From Email</asp:label><SPAN class="mandatory">*</SPAN></TD>
					<TD class="midcolora" width="32%" colSpan="3"><asp:textbox id="txtEMAIL_FROM" runat="server" maxlength="50" size="30"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvFROM_EMAIL" runat="server" Display="Dynamic" ControlToValidate="txtEMAIL_FROM"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revFROM_EMAIL" Display="Dynamic" ControlToValidate="txtEMAIL_FROM" Runat="server"></asp:regularexpressionvalidator></TD>
				</TR>
				<tr>
					<TD class="midcolora"><asp:label id="capTO" runat="server">To</asp:label><SPAN class="mandatory">*</SPAN></TD>
					<td class="midcolora" align="center" width="18%"><asp:label id="capCONTACTDETAILS" Runat="server">Contact Details</asp:label><br>
						<asp:listbox id="cmbCONTACTDETAILS" Runat="server" Height="79px" AutoPostBack="False" SelectionMode="Multiple">
							<ASP:LISTITEM Value="0"><--------Select--------></ASP:LISTITEM>
						</asp:listbox><br>
						<asp:label id="capADDITIONAL" Runat="server">Add Additional Recipient</asp:label><br>
						<asp:textbox id="txtADDITIONAL" maxlength="50" size="30" Runat="server"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revADDITIONAL" Display="Dynamic" ControlToValidate="txtADDITIONAL" Runat="server"></asp:regularexpressionvalidator></td>
					<td class="midcolorc" align="center" width="18%"><br>
						<asp:button id="btnSELECT" Runat="server" Text=">>" CausesValidation="True"></asp:button><br>
						<br>
						<br>
						<br>
						<asp:button id="btnDESELECT" Runat="server" Text="<<" CausesValidation="False"></asp:button></td>
					<td class="midcolora" align="center"><asp:label id="capRECIPIENTS1" Runat="server">Recipients</asp:label><br>
						<asp:listbox id="cmbRECIPIENTS" onblur="funcValidateRecipients" Runat="server" Height="79px"
							AutoPostBack="False" SelectionMode="Multiple" onChange="funcValidateRecipients"></asp:listbox><br>
						<asp:customvalidator id="csvRECIPIENTS" Display="Dynamic" ControlToValidate="cmbRECIPIENTS" Runat="server"
							ClientValidationFunction="funcValidateRecipients" Enabled="False"></asp:customvalidator><span id="spnRECIPIENTS" style="DISPLAY: none; COLOR: red">Please 
							select Recipients.</span>
					</td>
				</tr>
				<TR id="trRECIEPIENTS" style="DISPLAY: none">
					<TD class="midcolora"><asp:label id="capRECIPIENTS" runat="server">Recipients</asp:label></TD>
					<TD class="midcolora"><asp:textbox id="txtRECIPIENTS" runat="server" maxlength="200" size="30" TextMode="MultiLine"></asp:textbox><BR>
					</TD>
				</TR>
				<tr>
					<td class="midcolora" width="18%"><asp:label id="capCLAIM_NUMBER" Runat="server">Claim</asp:label></td>
					<td class="midcolora" colSpan="3"><asp:textbox id="txtCLAIM_NUMBER" size="15" runat="server" ReadOnly="true" MaxLength="50"></asp:textbox><IMG id="imgClaim" style="CURSOR: hand" onclick="OpenClaimLookup()" src="/cms/cmsweb/images/selecticon.gif"></td>
				</tr>
				<tr>
					<td class="midcolora" width="18%"><asp:label id="capPOLICY_NUMBER" Runat="server">Policy</asp:label></td>
					<td class="midcolora" colSpan="3"><asp:textbox id="txtPOLICY_NUMBER" size="15" runat="server" ReadOnly="true" MaxLength="50"></asp:textbox><IMG id="imgPolicy" style="CURSOR: hand" onclick="OpenPolicyLookup()" src="/cms/cmsweb/images/selecticon.gif"></td>
				</tr>
				<tr>
					<td class="midcolora" width="18%"><asp:label id="capApplication" Runat="server">Application</asp:label></td>
					<td class="midcolora" colSpan="3"><asp:textbox id="txtAPP_NUMBER" size="15" Runat="server" ReadOnly="True" MaxLength="50"></asp:textbox><IMG id="imgApplication" style="CURSOR: hand" onclick="OpenAppLookup()" src="/cms/cmsweb/images/selecticon.gif"></td>
				</tr>
				<tr>
					<td class="midcolora" width="18%"><asp:label id="capQuote" Runat="server">Quote</asp:label></td>
					<td class="midcolora" colSpan="3"><asp:textbox id="txtQuote" size="15" Runat="server" ReadOnly="True" MaxLength="50"></asp:textbox>
						<IMG id="imgQuote" style="CURSOR: hand" onclick="OpenQuoteLookup()" src="/cms/cmsweb/images/selecticon.gif"></td>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capEMAIL_SUBJECT" runat="server">Subject</asp:label><SPAN class="mandatory">*</SPAN></TD>
					<TD class="midcolora" colSpan="3"><asp:textbox id="txtEMAIL_SUBJECT" runat="server" maxlength="50" size="50"></asp:textbox><BR>
						<asp:requiredfieldvalidator id="rfvSUBJECT" runat="server" Display="Dynamic" ControlToValidate="txtEMAIL_SUBJECT"></asp:requiredfieldvalidator></TD>
				</tr>
				<TR>
					<TD class="midcolora" width="18%"><asp:label id="capEMAIL_MESSAGE" runat="server">Message</asp:label><SPAN class="mandatory">*</SPAN></TD>
					<TD class="midcolora" colSpan="3"><asp:textbox onkeypress="MaxLength(txtEMAIL_MESSAGE,500)" id="txtEMAIL_MESSAGE" runat="server" Height="96px"
							TextMode="MultiLine" width="265px"></asp:textbox><BR>
						<asp:requiredfieldvalidator id="rfvMESSAGE" runat="server" Display="Dynamic" ControlToValidate="txtEMAIL_MESSAGE"></asp:requiredfieldvalidator></TD>
				</TR>
				<tr>
					<TD class="midcolora"><asp:label id="capATTACHMENT" runat="server">Attachment</asp:label></TD>
					<TD class="midcolora" colSpan="3"><input onkeypress="ClearText(event)" id="txtATTACHMENT" type="file" size="42" name="txtATTACHMENT"
							runat="server">
						<asp:label id="lblATTACHMENT" Runat="server"></asp:label></TD>
				</tr>
				<tr>
					<TD class="midcolora"><asp:label id="capDIARY_ITEM_REQ" runat="server">Diary Item Required</asp:label></TD>
					<TD class="midcolora"><asp:dropdownlist id="cmbDIARY_ITEM_REQ" onfocus="SelectComboIndex('cmbDIARY_ITEM_REQ')" runat="server">
							<asp:ListItem Value='N'>No</asp:ListItem>
							<asp:ListItem Value='Y'>Yes</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD class="midcolora" id="tdFollowupCap"><asp:label id="capFOLLOW_UP_DATE" style="DISPLAY: none" runat="server">Follow up date</asp:label><span class="mandatory" id="spnFOLLOW_UP_DATE" style="DISPLAY: none">*</span></TD>
					<TD class="midcolora" id="tdFollowuptxt"><asp:textbox id="txtFOLLOW_UP_DATE" style="DISPLAY: none" runat="server" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkFOLLOW_UP_DATE" runat="server" CssClass="HotSpot">
							<ASP:IMAGE id="imgFOLLOW_UP_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"
								style="display:none"></ASP:IMAGE>
						</asp:hyperlink><br>
						<asp:requiredfieldvalidator id="rfvFOLLOW_UP_DATE" Display="Dynamic" ControlToValidate="txtFOLLOW_UP_DATE" Runat="server"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revFOLLOW_UP_DATE" runat="server" Display="Dynamic" ControlToValidate="txtFOLLOW_UP_DATE"
							ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator><asp:customvalidator id="csvFOLLOW_UP_DATE" Display="Dynamic" ControlToValidate="txtFOLLOW_UP_DATE" Runat="server"
							ClientValidationFunction="ChkDate"></asp:customvalidator></TD>
				</tr>
				<tr id ="trDIARY_ITEM_TO" runat="server" style="DISPLAY:none">
					<TD class="midcolora" colSpan="1" style="HEIGHT: 38px"><asp:label id="capDIARY_ITEM_TO" runat="server">To</asp:label></TD>
					<TD class="midcolora" colSpan="1" style="HEIGHT: 38px"><asp:dropdownlist id="cmbDIARY_ITEM_TO" onfocus="SelectComboIndex('cmbDIARY_ITEM_TO')" runat="server"></asp:dropdownlist></TD>
					<TD class="midcolora" colSpan="2" style="HEIGHT: 38px"></TD>
				</tr>

				<tr id="trBody" runat="server">
					<td class="midcolora"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
					<td class="midcolorr" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnSend" runat="server" Text="Send"></cmsb:cmsbutton></td>
				</tr>
				<tr>
					<td class="iframsHeightMedium"></td>
				</tr>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
			<INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			<INPUT id="hidRowId" type="hidden" value="0" name="hidRowId" runat="server"> 
			<INPUT id="hidRECIPIENTS" type="hidden" value="0" name="hidRECIPIENTS" runat="server">
			<input id="hidAPP_ID" type="hidden" value="0" name="hidAPP_ID" runat="server"> 
			<input id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			<input id="hidCLAIM_ID" type="hidden" value="0" name="hidCLAIM_ID" runat="server">
			<input id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
			<input id="hidAPP_VERSION_ID" type="hidden" value="0" name="hidAPP_VERSION_ID" runat="server">
			<input id="hidAPP_NUMBER_ID" type="hidden" value="0" name="hidAPP_NUMBER_ID" runat="server">
			<input id="hidCUSTOMER_ID_NAME" type="hidden" name="hidCUSTOMER_ID_NAME" runat="server">
			<input id="hidPOLICY_APP_NUMBER" type="hidden" value="0" name="hidPOLICY_APP_NUMBER" runat="server">
			<input id="hidCLAIM_POLICY_NUMBER" type="hidden" value="0" name="hidCLAIM_POLICY_NUMBER" runat="server">
			<input id="hidCLAIM_NUMBER" type="hidden" value="0" name="hidCLAIM_NUMBER" runat="server">
			<input id="hidQQ_ID" type="hidden" value="0" name="hidQuote_number" runat="server">
			<input id="hidPOLICY_APP_CUSTOMER_ID_NAME" type="hidden" name="hidPOLICY_APP_CUSTOMER_ID_NAME" runat="server">
			<input id="hidMergeId" type="hidden" name="hidMergeId" runat="server"> 
			<INPUT id="hidRootPath" type="hidden" name="hidRootPath" runat="server">
			<INPUT id="hidInvalid_RECIPIENTS" type="hidden" name="hidInvalid_RECIPIENTS" runat="server">
			<input id="hidClaim" type="hidden" runat="server" />
			<input id="hidPolicy" type="hidden" runat="server" />
			<input id="hidApplication" type="hidden" runat="server" />
			<input id="hidQuote" type="hidden" runat="server" />
		</form>
		<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidRowId').value,true);
			
		</script>
	</body>
</HTML>

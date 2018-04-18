<%@ Register TagPrefix="cc1" Namespace="Cms.WebControls" Assembly="AjaxLookupTextbox" %>
<%@ Register TagPrefix="cc11" Namespace="Cms.BusinessLayer.BlCommon" Assembly="blcommon" %>
<%@ Page language="c#" Codebehind="AddJournalEntryDetail.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AddJournalEntryDetail" validateRequest=false  %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>ACT_JOURNAL_LINE_ITEMS</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta http-equiv="CACHE-CONTROL" content = "No-cache">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/AJAXcommon.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		
		//This function resets the form values
		function ResetForm()
		{
			if(document.getElementById("hidFormSaved").value == "3" || document.getElementById("hidFormSaved").value=="8")
			{
				//Form is posted back, hence data may not be present in combo box or other controls
				//hence, calling the changetab method of tab controls
				//which will load the form again							
				//parent.changeTab(0,0);
				//return false;
				document.location.href = "AddJournalEntryDetail.aspx?JOURNAL_ID=" + document.getElementById("hidJOURNAL_ID").value
											+ "&JE_LINE_ITEM_ID=" + document.getElementById("hidJE_LINE_ITEM_ID").value + "&";
			}				
			if (document.getElementById("hidOldData").value == "")
			{
			  //Old xml not present , hence form is in add mode
				document.forms[0].reset();
				AddData();
			}
			else
			{
				document.location.href = "AddJournalEntryDetail.aspx?JOURNAL_ID=" + document.getElementById("hidJOURNAL_ID").value
											+ "&JE_LINE_ITEM_ID=" + document.getElementById("hidJE_LINE_ITEM_ID").value + "&";
			}
			ChangeColor();
			return false;	
			
		/*	document.ACT_JOURNAL_LINE_ITEMS.reset();
			DisableValidators();
			populateXML();
			ChangeColor();			
			return false; */
		}
		
		
		//This function activates the first tab
		function OnBackClick()
		{
			this.parent.parent.changeTab(0,0);
			return false;
		}
		
		function OnDeleteClick()
		{
			var retVal = confirm("Are you sure you want to delete the selected record ?");
			return retVal;
		}
	
		function AddData()
		{
			// Value 8 : To persist add time data on postback of Tran Code Combo.
			if(document.getElementById('hidFormSaved').value != 8)
			{
				document.forms[0].reset();
				if(document.getElementById('txtAMOUNT') && document.getElementById('hidFormSaved').value != 5)
					document.getElementById('txtAMOUNT').focus();
				document.getElementById('hidJE_LINE_ITEM_ID').value	=	'New';
				
				if (document.getElementById('cmbDEPT_ID').options.length > 0)
				{
					document.getElementById('cmbDEPT_ID').options.selectedIndex = 0;
					FillProfitCenter();
				}
				
				if(document.getElementById('cmbPC_ID')!=null)
				{				
					if (document.getElementById('cmbPC_ID').options.length > 0)
						document.getElementById('cmbPC_ID').options.selectedIndex = 0;
				}
				if(document.getElementById('cmbTRAN_CODE')!=null)	
				{
					if (document.getElementById('cmbTRAN_CODE').options.length > 0)	
						document.getElementById('cmbTRAN_CODE').options.selectedIndex = 0;	
				}
				if (document.getElementById('cmbPOLICY_ID').options.length > 0)		
					document.getElementById('cmbPOLICY_ID').options.selectedIndex = -1;
				document.getElementById('txtAMOUNT').value  = '';
				document.getElementById('txtREGARDING_NAME').value = "";
				document.getElementById('hidREGARDING').value = "";
				document.getElementById('hidREF_CUSTOMER').value = ""
				document.getElementById('txtREF_CUSTOMER_NAME').value = ""
				document.getElementById('cmbACCOUNT_ID').options.selectedIndex = -1;
				document.getElementById('txtNOTE').value  = '';
			//	ChangeColor();
				DisableValidators();
				EnableValidator('rfvPOLICY_ID',false);
				//document.getElementById(rfvPOLICY_ID).setAttribute('enabled',false);
				//document.getElementById(rfvPOLICY_ID).style.display = "none";
			}

		}
		
		function commonCodeForTypeChangeCase()
		{
			document.getElementById("cmbACCOUNT_ID").style.display = "none";
			document.getElementById("txtPOLICY_NUMBER").style.display = "none"
			document.getElementById("lblACC_DESC").style.display = "inline";
			document.getElementById("txtOTHER_REGARDING").style.display = "none";
			document.getElementById("txtREGARDING_NAME").style.display = "inline";
			document.getElementById("imgREGARDING").style.display = "inline";
			document.getElementById("rfvACCOUNT_ID").setAttribute("enabled",false);
			document.getElementById("rfvREGARDING").setAttribute ("enabled", true);
			document.getElementById("rfvOTHER_REGARDING").setAttribute ("enabled", false);
			document.getElementById("rfvOTHER_REGARDING").style.display = "none";
			document.getElementById("txtAccount_ID").style.display = "none";			
		}
		
		//This function make the accounting id drop down
		//visible or invisible depending upon the selected value of type
		function OnTypeChange()
		{		  
			cmbType = document.getElementById("cmbTYPE");			
			if (cmbType.selectedIndex == -1)
			{
				//document.getElementById("cmbACCOUNT_ID").style.display = "none";
				document.getElementById("txtPOLICY_NUMBER").style.display = "none"
				document.getElementById("lblACC_DESC").style.display = "none";
				return;
			}
			
			
			switch(cmbType.options[cmbType.selectedIndex].value)
			{
				
				case "AGN":
					commonCodeForTypeChangeCase()
					document.getElementById("capREGARDING").innerText= "Agency Name";
					document.getElementById("capREF_CUSTOMER").innerText= "Insured Name";
					document.getElementById("rfvREGARDING").innerText= "Please select Agency Name";
					document.getElementById("capREF_CUSTOMER").style.display = "inline";					
					break;
				case "CUS":
					commonCodeForTypeChangeCase()
					document.getElementById("capREGARDING").innerText= "Insured Name";
					//document.getElementById("capREF_CUSTOMER").innerText= "Insured Name";
					document.getElementById("rfvREGARDING").innerText= "Please select Insured Name";
					//document.getElementById("capREF_CUSTOMER").style.display = "none";
					//added by uday
					//document.getElementById("txtPOLICY_NUMBER").innerText = "";	
				   	//end
					break;
				case "VEN":
					commonCodeForTypeChangeCase()
					document.getElementById("capREGARDING").innerText= "Vendor Name";
					//document.getElementById("capREF_CUSTOMER").innerText= "Insured Name";
					document.getElementById("rfvREGARDING").innerText= "Please select Vendor Name";
					//document.getElementById("capREF_CUSTOMER").style.display = "none";
					break;
				case "MOR":
					document.getElementById("cmbACCOUNT_ID").style.display = "none";
					document.getElementById("txtPOLICY_NUMBER").style.display = "none"
					document.getElementById("lblACC_DESC").style.display = "inline";
					
					document.getElementById("txtOTHER_REGARDING").style.display = "none";
					document.getElementById("txtREGARDING_NAME").style.display = "inline";
					document.getElementById("imgREGARDING").style.display = "inline";
					
					document.getElementById("rfvACCOUNT_ID").setAttribute("enabled",false);
					document.getElementById("rfvREGARDING").setAttribute ("enabled", true);
					document.getElementById("rfvOTHER_REGARDING").setAttribute ("enabled", false);
					document.getElementById("rfvOTHER_REGARDING").style.display = "none";
					break;	
				case "TAX":	
					//document.getElementById("cmbACCOUNT_ID").style.display = "inline";
					document.getElementById("txtPOLICY_NUMBER").style.display = "inline"
					document.getElementById("lblACC_DESC").style.display = "none";
					document.getElementById("txtOTHER_REGARDING").style.display = "none";
					document.getElementById("txtREGARDING_NAME").style.display = "inline";
					document.getElementById("imgREGARDING").style.display = "inline";
					document.getElementById("rfvACCOUNT_ID").setAttribute("enabled",true);
					document.getElementById("rfvREGARDING").setAttribute ("enabled", true);
					document.getElementById("rfvOTHER_REGARDING").setAttribute ("enabled", false);
					document.getElementById("rfvREGARDING").style.display = "none";
					document.getElementById("rfvOTHER_REGARDING").style.display = "none";
					break;				
				case "OTH":
					document.getElementById("txtPOLICY_NUMBER").style.display = "inline"
					document.getElementById("lblACC_DESC").style.display = "none";					
					document.getElementById("txtOTHER_REGARDING").style.display = "inline";															
					document.getElementById("txtREGARDING_NAME").style.display = "none";					
					document.getElementById("imgREGARDING").style.display = "none";
					document.getElementById("rfvACCOUNT_ID").setAttribute("enabled",true);
					document.getElementById("rfvREGARDING").setAttribute ("enabled", false);
					document.getElementById("rfvOTHER_REGARDING").setAttribute ("enabled", false);
					document.getElementById("rfvOTHER_REGARDING").style.display = "none";
					document.getElementById("rfvREGARDING").style.display = "none";
					//document.getElementById("capREF_CUSTOMER").innerText= "Insured Name";
					document.getElementById("spnREGARDING").style.display = "none";
					//document.getElementById("capREF_CUSTOMER").style.display = "none";
					document.getElementById("txtAccount_ID").style.display = "inline";
					break;				
			}			
				//document.getElementById("txtREGARDING_NAME_123").style.display = "none";
				//document.getElementById("imgREGARDING").style.display = "none";
		}
		
		//Thi function shows or hide the delete button
		function ShowDeleteButton()
		{
			if (document.getElementById('btnDelete') != null)
			{
				if(document.getElementById('hidOldData').value == "")
				{
					document.getElementById('btnDelete').style.display = "none";
				}
				else
				{
					document.getElementById('btnDelete').style.display = "inline";
				}
			}
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
		
		function populateXML()
		{
			//alert(document.getElementById('hidOldData').value)
			EnableValidator('rfvPOLICY_ID',false);
			if(document.getElementById('hidFormSaved').value == '0')
			{
					
				if(document.getElementById('hidOldData').value != "")
				{
					populateFormData(document.getElementById('hidOldData').value, ACT_JOURNAL_LINE_ITEMS);	
					var strCmbText = new String();
					strCmbText = document.getElementById('cmbACCOUNT_ID').options[document.getElementById('cmbACCOUNT_ID').selectedIndex].text;
					var cmbOptionAccTxt = strCmbText.substr(strCmbText.indexOf(': ') + 2,8);
					document.getElementById('txtAccount_ID').value = cmbOptionAccTxt;
				}
				else
				{
					AddData();
				}
			}
			
		
			ShowDeleteButton();
			var PC_ID = document.getElementById("hidPC_ID").value;
	
			FillProfitCenter();
			SelectComboOption("cmbPC_ID", PC_ID);
			
			document.getElementById("hidPC_ID").value = PC_ID;
			
			OnTypeChange();
			cmbPOLICY_ID_Change();
			FetchPolicies();	
			return false;
			//document.getElementById('txtAccount_ID').value = '';
		}
		
		//This function populates the profit center of selected department
		//selection of department
		function FillProfitCenter()
		{
			var cmbPC = document.getElementById('cmbPC_ID');
			var cmbDEPT = document.getElementById('cmbDEPT_ID');
			var pcXml = document.getElementById('hidDeptXML').value ;
			
			/*Clearing the old values in combo box*/
			cmbPC.innerHTML = '';	
			//alert('dept' +cmbDEPT.options[cmbDEPT.selectedIndex].value+ 'xml=' + document.getElementById('hidDeptXML').value );
			//If dept not selected then returning
			if(cmbDEPT.selectedIndex == -1)
			{
				return false;
			}
			
			var Deptid = cmbDEPT.options[cmbDEPT.selectedIndex].value;
			
			//Parsing the XML
			var objXmlHandler = new XMLHandler();
			var tree = (objXmlHandler.quickParseXML(pcXml).getElementsByTagName('profitxml')[0]);
			
			var oOption;
			var i=0;
			var nodeName;
			var checkDept;
								
			for(i = 0;i < tree.childNodes.length;i++)
			{
				if(!tree.childNodes[i].firstChild) continue;
				
				nodeName = tree.childNodes[i];
				checkDept = tree.childNodes[i].getAttribute("idd");
				
				for(j=0;j<nodeName.childNodes.length;j++)
				{
					if(Deptid == checkDept)
					{
						oOption = document.createElement("option");
						oOption.value = nodeName.childNodes[j].getAttribute("idd");
						oOption.text = nodeName.childNodes[j].getAttribute("pcname");
						cmbPC.add(oOption);
					}
				}
			}
			SetPCId();
		}

		//Validates the amount
		function txtAMOUNT_Validate(objSource,objArgs)
		{
			value = document.getElementById("txtAMOUNT").value;
			value = ReplaceString(value, ",","");
			if (value != "")
			{		
				
				if (isNaN(value))
				{
					objArgs.IsValid = false;
					return;
				}
				else
				{
					if(document.getElementById("hidCalledFrom").value=="TMPLT" && parseFloat(value)>=0)
					{
						objArgs.IsValid = true;						
						return;
					}					
				// Commented -- Amount(value) can be negative
				//	if(parseFloat(value) <= 0)
				//	{
				//		objArgs.IsValid = false;
				//		return;
				//	}					
					
				}
			}
			objArgs.IsValid = true;
			
		}		
		
		//This function sets hidPC_Id hidden control
		//from cmbPC drop down list
		function SetPCId()
		{
			if (document.getElementById("cmbPC_ID").selectedIndex > -1)
				document.getElementById("hidPC_ID").value = document.getElementById("cmbPC_ID").options[document.getElementById("cmbPC_ID").selectedIndex].value;
		}
		
		function cmbPOLICY_ID_Change()
		{
			ctrlType = document.getElementById("cmbTYPE");
			
			type = ctrlType.options[ctrlType.selectedIndex].value;
			if (type == "CUS" || type == "AGN" || type == "MOR")
			{
				//document.getElementById("cmbPOLICY_ID").style.display = "inline";				
				document.getElementById("txtPOLICY_NUMBER").style.display = "inline"				
				document.getElementById("spnPOLICY_ID").style.display = "none";
				//document.getElementById("rfvPOLICY_ID").setAttribute("enabled",true);
				document.getElementById("rfvPOLICY_NUMBER").setAttribute("enabled",true);
				document.getElementById("imgPolicy").style.display = "inline";
			}
			else
			{
				//document.getElementById("cmbPOLICY_ID").style.display = "none";
				document.getElementById("txtPOLICY_NUMBER").style.display = "none"
				document.getElementById("spnPOLICY_ID").style.display = "none";
				document.getElementById("rfvPOLICY_ID").setAttribute("enabled",false);
				document.getElementById("rfvPOLICY_ID").style.display = "none";
				document.getElementById("rfvPOLICY_NUMBER").setAttribute("enabled",false);
				document.getElementById("rfvPOLICY_NUMBER").style.display = "none";
				document.getElementById("imgPolicy").style.display = "none";
				document.getElementById("spnMandPOLICY_ID").style.display = "none";
				document.getElementById("capPOLICY_ID").style.display = "none";
			}
			if(type == "AGN")
			{
				document.getElementById("txtREF_CUSTOMER_NAME").style.display = "inline"
//				document.getElementById("spnREF_CUSTOMER_NAME").style.display = "none";
			}
			else
			{
				document.getElementById("txtREF_CUSTOMER_NAME").style.display = "none"
//				document.getElementById("spnREF_CUSTOMER_NAME").style.display = "inline";
			}
			
			//ChangeColor();
		}
		
		function FetchPolicies()
		{	
			if (document.getElementById("cmbTYPE").options[document.getElementById("cmbTYPE").selectedIndex].value != 'CUS')
			{
				if( document.getElementById("hidREF_CUSTOMER").value == "")

				{	
					return;
				}
			}
			
			var Agency = "0";
			var Customer = "0";
			
			if (document.getElementById("cmbTYPE").selectedIndex > -1)
			{
				if (document.getElementById("cmbTYPE").options[document.getElementById("cmbTYPE").selectedIndex].value == 'AGN')
				{
					Agency = document.getElementById("hidREGARDING").value;
					Customer = document.getElementById("hidREF_CUSTOMER").value
				}

				else if (document.getElementById("cmbTYPE").options[document.getElementById("cmbTYPE").selectedIndex].value == 'CUS')
				{
					Customer = document.getElementById("hidREGARDING").value;
					Agency = "0";
				}
				else if (document.getElementById("cmbTYPE").options[document.getElementById("cmbTYPE").selectedIndex].value == 'MOR')
				{
					Customer = document.getElementById("hidREF_CUSTOMER").value;
					Agency = "0";
				}

			}
			
			AddJournalEntryDetail.AjaxGetCustomerPolicies(Customer, Agency,FillPolicies);
				
			
						
		}
		
		function FillPolicies(Result)
		{
			
			if(Result.value!=null)
			{
			var strXML;
			if(Result.error)
			{        
				var xfaultcode   = Result.errorDetail.code;
				var xfaultstring = Result.errorDetail.string;
				var xfaultsoap   = Result.errorDetail.raw;        				
			}
			else	
			{	
				strXML= Result.value;
					
				
				//////////
				var xmlDoc = new ActiveXObject("Microsoft.XMLDOM") ;
				xmlDoc.async=false;
				xmlDoc.loadXML(strXML);
				xmlTableNodes = xmlDoc.selectNodes('/NewDataSet/Table');
				
				
				document.getElementById('cmbPOLICY_ID').length=0;
				objViolation = document.getElementById('cmbPOLICY_ID');
				
				for(var i = 0; i < xmlTableNodes.length; i++ )
				{ 
					//change by uday on 7 Nov
					var text = 	xmlTableNodes[i].selectSingleNode('POLICY_NUMBER').text + " " + xmlTableNodes[i].selectSingleNode('POLICY_DISP_VERSION').text;
					//var text = 	xmlTableNodes[i].selectSingleNode('POLICY_NUMBER').text;
					//end
					var value = 	xmlTableNodes[i].selectSingleNode('POLICY_ID').text + ";" + xmlTableNodes[i].selectSingleNode('POLICY_VERSION_ID').text;
					document.getElementById('cmbPOLICY_ID').options[document.getElementById('cmbPOLICY_ID').length]= new Option(text,value);
				}		
				document.getElementById('cmbPOLICY_ID').selectedIndex = -1;
				if (document.getElementById("hidPOLICY_ID").values == "")
				{
					document.getElementById('cmbPOLICY_ID').selectedIndex = -1;
				}
				else
				{		
					SelectComboOption('cmbPOLICY_ID',document.getElementById("hidPOLICY_ID").value + ";" +document.getElementById("hidPOLICY_VERSION_ID").value);
					if (document.getElementById('cmbPOLICY_ID').selectedIndex !=-1)
					 document.getElementById('txtPOLICY_NUMBER').value=document.getElementById('cmbPOLICY_ID').options[document.getElementById('cmbPOLICY_ID').selectedIndex].text;
					 fetchPolicyInfo(document.getElementById('txtPOLICY_NUMBER'));
				}
			}
			ChangeColor();	
			}
		}
		
		function SetPolicyId()
		{
			
			if (document.getElementById('cmbPOLICY_ID').selectedIndex == -1)
			{
				document.getElementById("hidPOLICY_ID").value = "";
				document.getElementById("hidPOLICY_VERSION_ID").value = "";			
			}
			
			var strPolicy = document.getElementById('cmbPOLICY_ID').options[document.getElementById('cmbPOLICY_ID').selectedIndex].value;
			
			PolicyInfo = strPolicy.split(';');
			
			if (PolicyInfo.length > 0 )
			{
				document.getElementById("hidPOLICY_ID").value = PolicyInfo[0];
				document.getElementById("hidPOLICY_VERSION_ID").value = PolicyInfo[1];
			}		
		}
		
		function OpenPolicyLookup(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction)
		{
			var custID = document.getElementById('hidCUSTOMER_ID').value;
			var appID = document.getElementById('hidAPP_ID').value;
			var appVersionID = document.getElementById('hidAPP_VERSION_ID').value;
			var whereClause = document.getElementById('hidREGARDING').value;
						
			ctrlType = document.getElementById("cmbTYPE");
			type = ctrlType.options[ctrlType.selectedIndex].value;
			
			if(type == "AGN")
			{
				if(whereClause == '')
				{	
					whereClause = ' @AGENCY_ID  = \'' + '%[^~]%' + '\'';
				}
				else
				{
					whereClause = ' @AGENCY_ID  = ' + whereClause ;
				}
				whereClause = whereClause + ' ; @CUSTOMER_ID =  \'' + '%[^~]%' + '\'';
			}
			else
			{
				if(whereClause == '')
				{	
					whereClause = ' @CUSTOMER_ID  =  \'' + '%[^~]%' + '\'';
				}
				else
				{
					whereClause = ' @CUSTOMER_ID  = ' + whereClause ;
				}
				whereClause = whereClause + ' ; @AGENCY_ID =  \'' + '%[^~]%' + '\'';
			}
		   if(type=='CUS')
           OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','POLICY_APP_NUMBER','CUSTOMER_ID_NAME','txtPOLICY_NUMBER','hidCUSTOMER_ID_NAME','PolicyDBCustomer','Policy',whereClause,'splitPolicy()');
           else
           OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','POLICY_APP_NUMBER','CUSTOMER_ID_NAME','txtPOLICY_NUMBER','hidCUSTOMER_ID_NAME','PolicyCustomerAgency','Policy',whereClause,'splitPolicy()');
			//OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','POLICY_APP_NUMBER','CUSTOMER_ID_NAME','hidPOLICY_APP_NUMBER','hidCUSTOMER_ID_NAME','PolicyCustomerAgency','Policy',whereClause,'splitPolicy()');
		//	OpenLookupWithFunction('<%=ClsCommon.GetLookupURL()%>','POLICY_APP_NUMBER','CUSTOMER_ID_NAME','txtPOLICY_NUMBER','hidCUSTOMER_ID_NAME','PolicyCustomerAgency','Policy',whereClause,'splitPolicy()');        
		}
		function splitPolicy()
		{

			var PolicyAppNumber = document.getElementById('txtPOLICY_NUMBER').value.split('~');
			document.getElementById('hidPOLICY_APP_NUMBER').value = PolicyAppNumber;
			document.getElementById('hidPOLICY_ID').value		=	PolicyAppNumber[0] ;
			document.getElementById('hidPOLICY_VERSION_ID').value	=	PolicyAppNumber[1];			
			//document.getElementById('txtPOLICY_NUMBER').value = PolicyAppNumber[2];
			document.getElementById('cmbPOLICY_ID').length=0;
			//change by uday on 7 Nov
			var text = PolicyAppNumber[2] + " " + PolicyAppNumber[9];
			//var text = PolicyAppNumber[2];
			//end
			var value = 	PolicyAppNumber[0] + ";" + PolicyAppNumber[1];						
			document.getElementById('cmbPOLICY_ID').options[document.getElementById('cmbPOLICY_ID').length]= new Option(text,value);
			document.getElementById('txtPOLICY_NUMBER').value=text;
			
			if(PolicyAppNumber[5])
			{
				document.getElementById('hidAPP_ID').value		=	PolicyAppNumber[3] ;
				document.getElementById('hidAPP_VERSION_ID').value	=	PolicyAppNumber[4];
				//document.getElementById('txtAPP_NUMBER').value = PolicyAppNumber[5];
			}
			
			var CustomerIdName	=	document.getElementById('hidCUSTOMER_ID_NAME').value.split('~');
			document.getElementById('hidCUSTOMER_ID').value = CustomerIdName[0];
			document.getElementById("hidREF_CUSTOMER").value =CustomerIdName[0];
			document.getElementById('txtREF_CUSTOMER_NAME').value = CustomerIdName[1];
			ctrlType = document.getElementById("cmbTYPE");
			type = ctrlType.options[ctrlType.selectedIndex].value;

			if (type == "AGN")
			{
				if(PolicyAppNumber[8])
				{
				//document.getElementById('hidAPP_ID').value		=	PolicyAppNumber[3];
				//document.getElementById('hidAPP_VERSION_ID').value	=	PolicyAppNumber[4];
				document.getElementById('txtREGARDING_NAME').value = PolicyAppNumber[8];
				//document.getElementById("hidREF_CUSTOMER").value =PolicyAppNumber[6];
				document.getElementById('hidREGARDING').value= PolicyAppNumber[6];
				}
			}  
			else if (type == "CUS")
			{
			document.getElementById('txtREGARDING_NAME').value=CustomerIdName[1];
			document.getElementById("hidREF_CUSTOMER").value =CustomerIdName[0];
			document.getElementById('hidREGARDING').value= CustomerIdName[0];
			}
			FetchPolicies();
			
		}
		
		

		//Formats the amount and convert 111 into 1.11
		function FormatAmount(txtReceiptAmount)
		{
						
			if (txtReceiptAmount.value != "")
			{
				amt = txtReceiptAmount.value;
				
				amt = ReplaceAll(amt,".","");
				
				if (amt.length == 1)
					amt = amt + "0";
				
				if ( ! isNaN(amt))
				{
					
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					
					txtReceiptAmount.value = InsertDecimal(amt);
				}
			}
		}
		
		
		// set Account in combo on basis of account no entered in AccountTextbox
		function searchAccount(txtAccountIDValue,cmbACCOUNT_ID)// Acc text, Acc ComboID
		{
			var cmbtext;
			var cmpCmbText;
			var searchresult=0;
			var accText = txtAccountIDValue;
			if(accText != "")
			{
				// loop through all the options of corresponding combo
				for(var j=1; j<cmbACCOUNT_ID.options.length; j++)
				{
					cmbtext= cmbACCOUNT_ID.options[j].text; // Combo text
					var strCmbText = new String();
					strCmbText = cmbtext;
					var cmbAccNo = strCmbText.substr(strCmbText.indexOf(': ') + 2,8);
					searchresult=cmbAccNo.indexOf(accText, 0) // search for Acc text in Combo Text
					if(searchresult != -1) // if true, set that combo text as selected
					{
						cmbACCOUNT_ID.options.selectedIndex = j;
						//document.getElementById(txtAccountIDValue).value = cmbAccNo;
						break;
					}
				}
			}
		}
		
		// fill Account Textbox on change of Account combo
		function FillSearchAccTxtBx(cmbACCOUNT_ID,txtACCOUNT_ID)
		{
			var cmbText = cmbACCOUNT_ID.options[cmbACCOUNT_ID.selectedIndex].text;
			var strCmbText = new String();
			strCmbText = cmbText;
			var cmbAccNo = strCmbText.substr(strCmbText.indexOf(': ') + 2,8);
			document.getElementById('txtACCOUNT_ID').value = cmbAccNo; // set combo acc no in account textbox
		}
		
		function fetchPolicyInfo(PolNum)
		{
			var PolNum = PolNum.value.split(' ');
			FetchXML(PolNum[0]);
		}
		
		
		function FetchXML(PolNum)
		{
			var ParamArray = new Array();
			obj1=new Parameter('POLICY_NUMBER',PolNum);
			ParamArray.push(obj1);
			var objRequest = _CreateXMLHTTPObject();
			var Action = 'AI_INFO';
			_SendAJAXRequest(objRequest,'AI_INFO',ParamArray,CallbackFun);
			
		}
		
		function CallbackFun(AJAXREsponse)
		{	
			//alert(AJAXREsponse) 17^435^A1000087^2^Agency Four^sadfsafsafasdfa  
			var cmbSelType = document.getElementById("cmbTYPE").options[document.getElementById("cmbTYPE").selectedIndex].value;
			var tmp = AJAXREsponse.split("^");	
			
			if(tmp != 0)				
			{
				if(cmbSelType == "AGN")
				{
					document.getElementById('txtREF_CUSTOMER_NAME').value = tmp[5];
					document.getElementById('txtREGARDING_NAME').value = tmp[4];
					document.getElementById("hidREF_CUSTOMER").value = tmp[1];
					document.getElementById('hidREGARDING').value = tmp[6];
					document.getElementById('hidCUSTOMER_ID').value = tmp[1];
					document.getElementById('hidPOLICY_ID').value = tmp[0];
					document.getElementById('hidPOLICY_VERSION_ID').value = tmp[3];					
				}
				
				else if(cmbSelType == "CUS")
				{
					document.getElementById('txtREGARDING_NAME').value = tmp[5];
					document.getElementById('hidREGARDING').value = tmp[1];
					document.getElementById('hidCUSTOMER_ID').value = tmp[1];
					document.getElementById('hidPOLICY_ID').value = tmp[0];
					document.getElementById('hidPOLICY_VERSION_ID').value = tmp[3];
					
					if(tmp[7] == 'AB')
					{
					alert('Policy is of AB type. Only DB policies are allowed for Customer Type.');
					document.getElementById('txtPOLICY_NUMBER').value = '';
					document.getElementById('txtREF_CUSTOMER_NAME').value = '';
					document.getElementById('txtREGARDING_NAME').value = '';
					
					}
				}
				
			}
			else
			{
				if(document.getElementById('txtPOLICY_NUMBER').value!="")
				{
					alert('Invalid policy number. Please enter a valid Policy Number.');
					document.getElementById('txtPOLICY_NUMBER').value = '';
					document.getElementById('txtREF_CUSTOMER_NAME').value = '';
					document.getElementById('txtREGARDING_NAME').value = '';
				}
			}
		}
		function fnSetMaxlength()
		{
			var amt = new String();
			amt = document.getElementById('txtAMOUNT').value;
			if(amt.indexOf('-') == "0")
			{
				document.getElementById('txtAMOUNT').maxLength = 16;}
			else
				document.getElementById('txtAMOUNT').maxLength = 15;
				
		}
		</script>
</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();ChangeColor();">
		<FORM id="ACT_JOURNAL_LINE_ITEMS" method="post" runat="server">			
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr id="trPageHeader" runat="server">
									<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
										mandatory</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tbody id="tbBody">
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capAMOUNT" runat="server">Amount</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%">
											<asp:textbox  id="txtAMOUNT" runat="server" CssClass="InputCurrency" size="30" style="TEXT-ALIGN:right" onkeypress="fnSetMaxlength();"></asp:textbox>
											<BR>
											<asp:requiredfieldvalidator id="rfvAMOUNT" runat="server" Display="Dynamic" ErrorMessage="AMOUNT can't be blank."
												ControlToValidate="txtAMOUNT"></asp:requiredfieldvalidator>
											<asp:CustomValidator id="csvAMOUNT" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
												ControlToValidate="txtAMOUNT" ClientValidationFunction="txtAMOUNT_Validate"></asp:CustomValidator>
										</TD>
										<TD class="midcolora" width="18%"><asp:label id="capTYPE" runat="server">Type</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbTYPE" onfocus="SelectComboIndex('cmbTYPE')" runat="server" AutoPostBack="True">
												<ASP:LISTITEM Value="AGN">Agency</ASP:LISTITEM>
												<ASP:LISTITEM Value="CUS">Customer</ASP:LISTITEM>
												<%--asp:ListItem Value='TAX' >Tax Entity</asp:ListItem--%>
												<ASP:LISTITEM Value="VEN">Vendor</ASP:LISTITEM>
												<ASP:LISTITEM Value="OTH">Other</ASP:LISTITEM>
												<%--asp:ListItem Value='MOR'>Mortgage</asp:ListItem--%>
											</asp:dropdownlist><BR>
											<asp:requiredfieldvalidator id="rfvTYPE" runat="server" Display="Dynamic" ErrorMessage="TYPE can't be blank."
												ControlToValidate="cmbTYPE"></asp:requiredfieldvalidator></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capPOLICY_ID" runat="server">Policy</asp:label><span class="mandatory" id="spnMandPOLICY_ID">*</span></TD>
										<TD class="midcolora" width="32%">
											<asp:TextBox ID="txtPOLICY_NUMBER" Runat="server" MaxLength="10" size="15" onblur="javascript:fetchPolicyInfo(this)"></asp:TextBox>
											<asp:dropdownlist id="cmbPOLICY_ID" onchange="javascript:cmbPOLICY_ID_Change();SetPolicyId();" runat="server"
												style="DISPLAY:none"></asp:dropdownlist>
											<span class="labelfont" id="spnPOLICY_ID">N.A.</span><IMG id="imgPolicy" style="CURSOR: hand" onclick="OpenPolicyLookup()" src="/cms/cmsweb/images/selecticon.gif">
											<BR>
											<asp:requiredfieldvalidator id="rfvPOLICY_ID" runat="server" Display="Dynamic" ErrorMessage="POLICY_ID can't be blank."
												ControlToValidate="cmbPOLICY_ID"></asp:requiredfieldvalidator>
											<asp:requiredfieldvalidator id="rfvPOLICY_NUMBER" runat="server" Display="Dynamic" ErrorMessage="Please select Policy Number."
												ControlToValidate="txtPOLICY_NUMBER"></asp:requiredfieldvalidator>
										</TD>
										<TD class="midcolora" width="18%"><asp:label id="capREGARDING" runat="server">Regarding</asp:label><span class="mandatory" id="spnREGARDING">*</span></TD>
										<TD class="midcolora" width="32%">
											<asp:TextBox ID="txtOTHER_REGARDING" Runat="server" MaxLength="30"></asp:TextBox>
											<asp:textbox id="txtREGARDING_NAME" runat="server" size="40" ReadOnly="True"></asp:textbox>
											<IMG id="imgREGARDING" style="CURSOR: hand" alt="" src="../../cmsweb/images/selecticon.gif"
												runat="server"> 
											<!-- Ajax control-->
											<%--<cc1:ajaxlookup id="txtREGARDING_NAME" runat="server" size="40" DataValueFieldID="hidREGARDING_NAME"
										CallBackFunction="AccountBase.GetSearch" DataTextField="NAME" DataValueField="CUSTOMER_ID" ScriptFile="../../cmsweb/scripts/lookup.js"
										LookupNodeName="CustomerName"></cc1:ajaxlookup></CC1:AJAXLOOKUP> --%>
											<BR>
											<asp:requiredfieldvalidator id="rfvREGARDING" runat="server" Display="Dynamic" ErrorMessage="REGARDING can't be blank."
												ControlToValidate="txtREGARDING_NAME"></asp:requiredfieldvalidator>
											<asp:requiredfieldvalidator id="rfvOTHER_REGARDING" runat="server" Display="Dynamic" ErrorMessage="REGARDING can't be blank."
												ControlToValidate="txtOTHER_REGARDING"></asp:requiredfieldvalidator>
										</TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capREF_CUSTOMER" runat="server">Reference Customer</asp:label><span id="spnReferenceCustomerMan" runat="server" class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%">
											<asp:textbox id="txtREF_CUSTOMER_NAME_123" style="DISPLAY: none" runat="server" size="40" ReadOnly="True"></asp:textbox><IMG id="imgSelect" style="DISPLAY: none" alt="" src="../../cmsweb/images/selecticon.gif"
												runat="server"> 
											<!-- Ajax control-->
											<cc1:ajaxlookup id="txtREF_CUSTOMER_NAME" runat="server" size="40" DataValueFieldID="hidRECEIPT_FROM_ID"
												CallBackFunction="AccountBase.GetSearch" DataTextField="NAME" DataValueField="CUSTOMER_ID" ScriptFile="../../cmsweb/scripts/lookup.js"
												LookupNodeName="CustomerName"></cc1:ajaxlookup>
											<BR>
											<asp:requiredfieldvalidator id="rfvREF_CUSTOMER" runat="server" Display="Dynamic" ErrorMessage="REF_CUSTOMER can't be blank."
												ControlToValidate="txtREF_CUSTOMER_NAME"></asp:requiredfieldvalidator>
										</TD>
										<TD class="midcolora" width="18%">
											<asp:label id="capACCOUNT_ID" runat="server">G/L Accounts</asp:label><span class="mandatory">*</span>
										</TD>
										<TD class="midcolora" width="32%">
											<asp:TextBox ID="txtAccount_ID" size="9" Runat="server" ReadOnly="False" MaxLength="8" onblur="searchAccount(this.value,document.getElementById('cmbACCOUNT_ID'))"></asp:TextBox>
											<asp:dropdownlist id="cmbACCOUNT_ID" onfocus="SelectComboIndex('cmbACCOUNT_ID')" runat="server" onchange="FillSearchAccTxtBx(this,document.getElementById('txtAccount_ID'))">
												<ASP:LISTITEM Value=""></ASP:LISTITEM>
											</asp:dropdownlist><asp:label id="lblACC_DESC" CssClass="labelfont" Runat="server"></asp:label><BR>
											<asp:requiredfieldvalidator id="rfvACCOUNT_ID" runat="server" Display="Dynamic" ErrorMessage="ACCOUNT_ID can't be blank."
												ControlToValidate="cmbACCOUNT_ID"></asp:requiredfieldvalidator></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capDEPT_ID" runat="server">Department</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDEPT_ID" onfocus="SelectComboIndex('cmbDEPT_ID')" runat="server" onchange="FillProfitCenter()">
												<ASP:LISTITEM Value="0"></ASP:LISTITEM>
											</asp:dropdownlist><BR>
											<asp:requiredfieldvalidator id="rfvDEPT_ID" runat="server" Display="Dynamic" ErrorMessage="DEPT_ID can't be blank."
												ControlToValidate="cmbDEPT_ID"></asp:requiredfieldvalidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capPC_ID" runat="server">Profit Center</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPC_ID" onfocus="SelectComboIndex('cmbPC_ID')" runat="server" onchange="SetPCId()">
												<ASP:LISTITEM Value="0"></ASP:LISTITEM>
											</asp:dropdownlist><BR>
											<asp:requiredfieldvalidator id="rfvPC_ID" runat="server" Display="Dynamic" ErrorMessage="PC_ID can't be blank."
												ControlToValidate="cmbPC_ID"></asp:requiredfieldvalidator></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capNOTE" runat="server">Note</asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtNOTE" runat="server" TextMode="MultiLine" Height="48px" Width="220px"></asp:textbox>
										<BR><asp:CustomValidator ClientValidationFunction="ChkTextAreaLength" ControlToValidate="txtNOTE"
										Runat="server" Display="Dynamic" ID="csvDESCRIPTION"></asp:CustomValidator></TD>
										<TD class="midcolora" width="18%"><asp:label id="capTRAN_CODE" runat="server">Transaction Codes</asp:label><span id="spnTRAN_CODE" class="mandatory" runat="server">*</span></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbTRAN_CODE" onfocus="SelectComboIndex('cmbTRAN_CODE')" runat="server" AutoPostBack="True"></asp:dropdownlist>
										<br><asp:RequiredFieldValidator Display="Dynamic" ID="rfvTRAN_CODE" Runat="server" ControlToValidate="cmbTRAN_CODE"></asp:RequiredFieldValidator> 
										</TD>
									<tr>
										<td class="midcolora" colSpan="2">
											<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
											<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton>
										</td>
										<td class="midcolorr" colSpan="2">
											<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
										</td>
									</tr>
								</tbody>
							</TBODY>	
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidJE_LINE_ITEM_ID" type="hidden" name="hidJE_LINE_ITEM_ID" runat="server">
			<input id="hidJOURNAL_ID" type="hidden" name="hidJOURNAL_ID" runat="server"> <input id="hidDeptXML" type="hidden" name="hidDeptXML" runat="server">
			<input id="hidPC_ID" type="hidden" name="hidPC_ID" runat="server"> <input type="hidden" runat="server" id="hidGLAccountXML" name="hidGLAccountXML">
			<input type="hidden" runat="server" id="hidREF_CUSTOMER" name="hidREF_CUSTOMER">
			<input type="hidden" runat="server" id="hidJournalInfoXML" name="hidJournalInfoXML">
			<input type="hidden" runat="server" id="hidREGARDING" value="0" name="hidREGARDING">
			<input type="hidden" id="hidPOLICY_ID" runat="server"> <input type="hidden" id="hidPOLICY_VERSION_ID" runat="server">
			<input id="hidCUSTOMER_ID" type="hidden" name="hidCUSTOMER_ID" runat="server" value="0">
			<input id="hidAPP_ID" type="hidden" name="hidAPP_ID" runat="server" value="0"> <input id="hidAPP_VERSION_ID" type="hidden" name="hidAPP_VERSION_ID" runat="server" value="0">
			<input id="hidAPP_NUMBER_ID" type="hidden" name="hidAPP_NUMBER_ID" runat="server">
			<input id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server"> <input id="hidCUSTOMER_ID_NAME" type="hidden" name="hidCUSTOMER_ID_NAME" runat="server">
			<input id="hidPOLICY_APP_NUMBER" type="hidden" name="hidPOLICY_APP_NUMBER" runat="server">
			<input id="hidMergeId" type="hidden" name="hidMergeId" runat="server">
		</FORM>
		<script>
			if (document.getElementById('hidFormSaved').value == "5")
			{
				document.getElementById('tbBody').style.display = 'none';
				document.getElementById('hidFormSaved').value = 1;
				RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidJOURNAL_ID').value, false);
				document.getElementById('hidFormSaved').value = 0;
				
				//Refreshing the parent header band
				this.parent.document.getElementById("hidJournalInfoXML").value = document.getElementById("hidJournalInfoXML").value;
				this.parent.ShowJournalInfo();
			}
			else
			{
				RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidJOURNAL_ID').value, false);
				if (document.getElementById('hidFormSaved').value == "1")
				{
					//Refreshing the parent header band
					this.parent.document.getElementById("hidJournalInfoXML").value = document.getElementById("hidJournalInfoXML").value;
					this.parent.ShowJournalInfo();
				}
			}
			//setffff();
		</script>
	</BODY>
</HTML>

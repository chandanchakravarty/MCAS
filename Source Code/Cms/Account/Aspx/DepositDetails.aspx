<%@ Page language="c#" Codebehind="DepositDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.DepositDetails" validateRequest="false" smartNavigation="false"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>RecrVehCoverages</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script src="../../cmsweb/scripts/AJAXcommon.js"></script>
		<script language="javascript">
		var strRowNum="";
		var blnIsValid = true;
			
		//Formats the amount and convert 111 into 1.11
		function FormatAmount(txtReceiptAmount)
		{
			
			if (txtReceiptAmount.value != "")
			{
				amt = txtReceiptAmount.value;
				var strAmt = new String();
				strAmt = amt;	
				
				if(strAmt.charAt(0) == "." && strAmt.length == 3)
				{
					strAmt = strAmt.substring(1);
					txtReceiptAmount.value = InsertDecimal(strAmt);
				}	
				else
				{	
					amt = ReplaceAll(strAmt,".","");
					if (amt.length == 1)
						amt = amt + "0";
					
					if ( ! isNaN(amt))
					{
						DollarPart = amt.substring(0, amt.length - 2);
						CentPart = amt.substring(amt.length - 2);
						
						txtReceiptAmount.value = InsertDecimal(amt);
						
						//Validating the amount, againt reguler exp
						//ValidatorOnChange();
					}
				}
			}
		}


		function OnTypeChange(cmbCombo) {
		    if (cmbCombo == null)
		        return false;

		    if (cmbCombo.selectedIndex == -1)
		        return false;

		    var rowNo = cmbCombo.getAttribute("RowNo");
		    if (parseInt(rowNo) < 10) {
		        rowNo = '0' + rowNo;
		    }

		    hlkCtrl = document.getElementById("dgDepositDetails_ctl" + rowNo + "_hlkACCOUNT_ID");
		    lblCtrl = document.getElementById("dgDepositDetails_ctl" + rowNo + "_lblACCOUNT_ID");
		    hidCtrl = document.getElementById("dgDepositDetails_ctl" + rowNo + "_hidCD_LINE_ITEM_ID");
		    txtAmountCtrl = document.getElementById("dgDepositDetails_ctl" + rowNo + "_txtRECEIPT_AMOUNT");
		    imgSelect = document.getElementById("dgDepositDetails_ctl" + rowNo + "_imgSelect");
		    txtReceiptFrom = document.getElementById("dgDepositDetails_ctl" + rowNo + "_txtRECEIPT_FROM_ID");
		    hidReceiptFrom = document.getElementById("dgDepositDetails_ctl" + rowNo + "_hidRECEIPT_FROM_ID");

		    if (hidCtrl.value == "") {
		        document.getElementById("dgDepositDetails_ctl" + rowNo + "_hlkACCOUNT_ID").setAttribute("disabled", true);
		        document.getElementById("dgDepositDetails_ctl" + rowNo + "_hrfOpenWindow").setAttribute("disabled", true);
		        document.getElementById("dgDepositDetails_ctl" + rowNo + "_hrfApplyItems").setAttribute("disabled", true);
		        document.getElementById("dgDepositDetails_ctl" + rowNo + "_hrfOpenWindow").href = "javascript:OpenEditWindow(null)";
		        document.getElementById("dgDepositDetails_ctl" + rowNo + "_hlkACCOUNT_ID").href = "javascript:OpenDistributionWindow(null,null)";
		        document.getElementById("dgDepositDetails_ctl" + rowNo + "_hrfApplyItems").href = "javascript:OpenApplyOpenItemsWindow(null,null)";
		    }
		    else {
		        document.getElementById("dgDepositDetails_ctl" + rowNo + "_hlkACCOUNT_ID").setAttribute("disabled", false);
		        document.getElementById("dgDepositDetails_ctl" + rowNo + "_hrfOpenWindow").setAttribute("disabled", false);
		        document.getElementById("dgDepositDetails_ctl" + rowNo + "_hrfApplyItems").setAttribute("disabled", false);
		        document.getElementById("dgDepositDetails_ctl" + rowNo + "_hrfOpenWindow").href = "javascript:OpenEditWindow('" + hidCtrl.value + "')";
		        document.getElementById("dgDepositDetails_ctl" + rowNo + "_hlkACCOUNT_ID").href = "javascript:OpenDistributionWindow('" + hidCtrl.value + "','" + txtAmountCtrl.value + "')";
		        document.getElementById("dgDepositDetails_ctl" + rowNo + "_hrfApplyItems").href = "javascript:OpenApplyOpenItemsWindow('" + hidCtrl.value + "','" + hidReceiptFrom.value + "')";
		    }

		    if (hlkCtrl == null || lblCtrl == null)
		        return false;

		    switch (cmbCombo.options[cmbCombo.selectedIndex].value) {
		        case "AGN":
		            hlkCtrl.style.display = "none";
		            lblCtrl.style.display = "inline";
		            lblCtrl.innerHTML = GetLedgerAccount("AGENCY_ACCOUNT");
		            imgSelect.style.display = "inline";
		            txtReceiptFrom.readOnly = true;

		            //Policy lookup not required
		            document.getElementById("dgDepositDetails_ctl" + rowNo + "_txtPolicyNo").setAttribute("disabled", true);
		            break;
		        case "CUS":
		            hlkCtrl.style.display = "none";
		            lblCtrl.style.display = "inline";
		            lblCtrl.innerHTML = GetLedgerAccount("CUSTOMER_ACCOUNT");
		            imgSelect.style.display = "inline";
		            txtReceiptFrom.readOnly = true;

		            //Policy lookup required
		            document.getElementById("dgDepositDetails_ctl" + rowNo + "_txtPolicyNo").setAttribute("disabled", false);
		            break;
		        case "VEN":
		            hlkCtrl.style.display = "none";
		            lblCtrl.style.display = "inline";
		            lblCtrl.innerHTML = GetLedgerAccount("VENDOR_ACCOUNT");
		            imgSelect.style.display = "inline";
		            txtReceiptFrom.readOnly = true;

		            //Policy lookup not required
		            document.getElementById("dgDepositDetails_ctl" + rowNo + "_txtPolicyNo").setAttribute("disabled", true);
		            break;
		        case "MOR":
		            hlkCtrl.style.display = "none";
		            lblCtrl.style.display = "inline";
		            lblCtrl.innerHTML = GetLedgerAccount("MORTGAGE_ACCOUNT");
		            imgSelect.style.display = "inline";
		            txtReceiptFrom.readOnly = true;

		            //Policy lookup not required
		            document.getElementById("dgDepositDetails_ctl" + rowNo + "_txtPolicyNo").setAttribute("disabled", true);
		            break;
		        case "OTH":
		            hlkCtrl.style.display = "inline";
		            lblCtrl.style.display = "none";
		            imgSelect.style.display = "none";
		            txtReceiptFrom.readOnly = false;

		            //Policy lookup not required
		            document.getElementById("dgDepositDetails_ctl" + rowNo + "_txtPolicyNo").setAttribute("disabled", true);
		            break;
		        default:
		            hlkCtrl.style.display = "none";
		            lblCtrl.style.display = "none";
		            imgSelect.style.display = "inline";
		            txtReceiptFrom.readOnly = true;

		            //Policy lookup not required
		            document.getElementById("dgDepositDetails_ctl" + rowNo + "_txtPolicyNo").setAttribute("disabled", true);
		            break;
		    }
		    return false;
		}
		
		// Checking whether any record selected or not
		function DeleteRows()
		{
			for (ctr=1; ctr<=18; ctr++)
			{
			    flag = 0;
			    if (parseInt(ctr) < 10) {
			        if (document.getElementById("dgDepositDetails_ctl0" + (ctr + 1) + "_chkDelete") != null) {
			            if (document.getElementById("dgDepositDetails_ctl0" + (ctr + 1) + "_chkDelete").checked == true) {
			                flag = 1;
			                break;
			            }
			        }
			    }
			    else {
			        if (document.getElementById("dgDepositDetails_ctl" + (ctr + 1) + "_chkDelete") != null) {
			            if (document.getElementById("dgDepositDetails_ctl" + (ctr + 1) + "_chkDelete").checked == true) {
			                flag = 1;
			                break;
			            }
			        }
			    }
			}
			
			if (flag == 0)
			{	
				// Not a single row is selected , hence returning false
				alert("Please select the row, which you want to delete.");
				return false;
			}
		}
		
		function SetPayorType()
		{
			window.ACT_CURRENT_DEPOSIT_LINE_ITEMS.scrollIntoView(0);
			for (ctr=1; ctr<=10; ctr++) {
			    if (parseInt(ctr) < 10)
                    OnTypeChange(document.getElementById("dgDepositDetails_ctl0" + (ctr + 1) + "_cmbPAYOR_TYPE"))
				else
				    OnTypeChange(document.getElementById("dgDepositDetails_ctl" + (ctr + 1) + "_cmbPAYOR_TYPE"))
			}
		}
		
		function GetLedgerAccount(nodeName)
		{
			if (document.getElementById("hidGLAccountXML").value != "")
			{
				//Parsing the XML
				var objXmlHandler = new XMLHandler();
				var tree = (objXmlHandler.quickParseXML(document.getElementById("hidGLAccountXML").value).getElementsByTagName('Table')[0]);
				
				nod = tree.getElementsByTagName(nodeName);
				if (nod != null)
				{
					if (nod.length > 0)
					{
						if (nod[0].childNodes.length >0)
							return nod[0].childNodes[0].text;
					}
				}
			}
			return "";
		}
		
		//Resets the form to its original state
		function Reset()
		{
			DisableValidators();
			document.ACT_CURRENT_DEPOSIT_LINE_ITEMS.reset();
			SetPayorType();
			CalculateTotalAmount();
			return false;
		}
		
		function CheckPayorType(objSource, objArgs)
		{
			if (objSource.controltovalidate != null )
			{
				var ctrl = document.getElementById(objSource.controltovalidate);
				if (ctrl.selectedIndex == -1)
				{
					objArgs.IsValid = false;
				}
				else
				{
					if (ctrl.options[ctrl.selectedIndex].text.trim() == "")
					{
						objArgs.IsValid = false;
					}
					else
					{
						objArgs.IsValid = true;
					}
				}
			}
			
		}
		
		function CheckPolicyNumber(objSource, objArgs)
		{
			if (objSource.controltovalidate != null )
			{
				var ctrl = document.getElementById(objSource.controltovalidate);
				var rowNo = ctrl.getAttribute("RowNo");
				
				if(parseInt(rowNo)<10)
				    rowNo = document.getElementById("dgDepositDetails_ctl0" + rowNo + "_txtRECEIPT_AMOUNT");
				else
				    rowNo = document.getElementById("dgDepositDetails_ctl" + rowNo + "_txtRECEIPT_AMOUNT");

				if (rowNo!=null && rowNo.value != "")
				{
					if (ctrl.selectedIndex == -1)
					{
						objArgs.IsValid = false;
					}
					else
					{
						objArgs.IsValid = true;
					}	
				}
				else
				{
					objArgs.IsValid = true;
				}
			}
		}
		
		function CheckAccountId(objSource, objArgs)
		{
			if (objSource.controltovalidate != null )
			{
				var ctrl = document.getElementById(objSource.controltovalidate);
				var rowNo = objSource.getAttribute("RowNo");
				
				if (parseInt(rowNo) < 10)
				    rowNo = document.getElementById("dgDepositDetails_ctl0" + rowNo + "_txtRECEIPT_AMOUNT");
				else
				    rowNo = document.getElementById("dgDepositDetails_ctl" + rowNo + "_txtRECEIPT_AMOUNT");
				
				if (rowNo.value != "")
				{
					if (ctrl.selectedIndex == -1)
					{
						objArgs.IsValid = false;
					}
					else
					{
						objArgs.IsValid = true;
					}	
				}
				else
				{
					objArgs.IsValid = true;
				}
			}
		}
		
		//This function will open the Disttribution window
		function OpenDistributionWindow(LineItemId, Amount)
		{
			if (LineItemId == null)
				return;
			window.open("DistributeCashReceipt.aspx?GROUP_ID=" + LineItemId
				+ "&GROUP_TYPE=DEP&DISTRIBUTION_AMOUNT=" + Amount 
				,"DistributeDeposits","height=500, width=800,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no,position=center" );
			
		}
		
		//This function open the edit line item window
		function OpenEditWindow(LineItemId)
		{
			if (LineItemId == null)
				return;
			window.open("EditDeposit.aspx?DEPOSIT_ID=" + document.getElementById("hidDEPOSIT_ID").value
				+ "&CD_LINE_ITEM_ID=" + LineItemId + "&","EditDeposit","height=500, width=700,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no" );
			//}
		}
		
		//This function will open the apply open items window
		function OpenApplyOpenItemsWindow(LineItemId,ReceiptFrom)
		{
			if (LineItemId == null)
				return;
				
			window.open("ReconDetail.aspx?DEPOSIT_ID=" + document.getElementById("hidDEPOSIT_ID").value
				+ "&CD_LINE_ITEM_ID=" + LineItemId 
				+ "&ENTITY_ID=" + ReceiptFrom
				+ "&","ApplyOpenItems","height=500, width=700,status= no, resizable= yes, scrollbars=no, toolbar=no,location=no,menubar=no" );
		}
		
		//This function open the policy lookup window
		function OpenPolicyLookup(RowNo)
		{
			
			var url='<%=URL%>';
			var idField,valueField,lookUpTagName,lookUpTitle;

			if (parseInt(RowNo) < 10) {
			    var txtCtrl = "dgDepositDetails_ctl0" + RowNo + "_txtPolicyNo";
			    var txtCtrl2 = "dgDepositDetails_ctl0" + RowNo + "_txtCheckPolicyNo";
			} else {
			    var txtCtrl = "dgDepositDetails_ctl" + RowNo + "_txtPolicyNo";
			    var txtCtrl2 = "dgDepositDetails_ctl" + RowNo + "_txtCheckPolicyNo";
			}					
			OpenLookupWithFunction(url,'POLICY_APP_NUMBER','CUSTOMER_ID_NAME','hidPOLICYINFO',txtCtrl,'DBPolicy','Policy','','splitPolicy(' + RowNo + ')');
			
		}
				
		//This function splits the policyid and policy version id and put it in different controls
		function splitPolicy(RowNo) {
		    if (parseInt(RowNo) < 10) {
		        var ctrlPolicyId = "dgDepositDetails_ctl0" + RowNo + "_hidPOLICY_ID"
		        var ctrlPolicyVerId = "dgDepositDetails_ctl0" + RowNo + "_hidPOLICY_VERSION_ID"
		        var ctrlPolicyNo = "dgDepositDetails_ctl0" + RowNo + "_txtPolicyNo"
		        var txtCtrl = "dgDepositDetails_ctl0" + RowNo + "_txtPolicyNo";
		     } else {
		        var ctrlPolicyId = "dgDepositDetails_ctl" + RowNo + "_hidPOLICY_ID"
		        var ctrlPolicyVerId = "dgDepositDetails_ctl" + RowNo + "_hidPOLICY_VERSION_ID"
		        var ctrlPolicyNo = "dgDepositDetails_ctl" + RowNo + "_txtPolicyNo"
		        var txtCtrl = "dgDepositDetails_ctl" + RowNo + "_txtPolicyNo";
		    }
			if (document.getElementById("hidPOLICYINFO").value.length > 0)
			{
				var arr = document.getElementById("hidPOLICYINFO").value.split("~");
				document.getElementById(ctrlPolicyId).value = arr[0];
				document.getElementById(ctrlPolicyVerId).value = arr[1];
				document.getElementById(ctrlPolicyNo).value = arr[2];
				document.getElementById(txtCtrl).value = arr[2];
			}
		}
		
		//Opens the look up window
		function OpenNewLookup(RowNo)
		{
			
			var url='<%=URL%>';
			var idField,valueField,lookUpTagName,lookUpTitle;
			
			cmbPayor = document.getElementById("dgDepositDetails_ctl" + RowNo + "_cmbPAYOR_TYPE");
			var payerType = cmbPayor.options[cmbPayor.selectedIndex].value;
			switch(payerType)
			{
			case 'AGN':
				idField			=	'AGENCY_ID';
				valueField		=	'Name';
				lookUpTagName	=	'Agency';
				lookUpTitle		=	"Agency Names";
				break;
			case 'CUS':
				idField			=	'CUSTOMER_ID';
				valueField		=	'Name';
				lookUpTagName	=	'CustLookupForm';
				lookUpTitle		=	'Customer Names';
				break;
			case 'VEN':
				idField			=	'VENDOR_ID';
				valueField		=	'Name';
				lookUpTagName	=	'VendorLookup';
				lookUpTitle		=	'Vendor Names';
				break;
			case 'MOR':
				idField			=	'HOLDER_ID';
				valueField		=	'Name';
				lookUpTagName	=	'Holder';
				lookUpTitle		=	'Mortgage Names';
				break;
			default:
				return false;
			}
			//signature:Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction
			var hid = "dgDepositDetails_ctl" + RowNo + "_hidRECEIPT_FROM_ID";
			var txtRECEIPT_FROM_ID = "dgDepositDetails_ctl" + RowNo + "_txtRECEIPT_FROM_ID";
			
			OpenLookup( url,idField,valueField,hid,txtRECEIPT_FROM_ID,lookUpTagName,lookUpTitle,'');
		}
		
		//Validate the receipt from id fields(whether can be empty or not)
		function ValidateRECEIPT_FROM_ID(objSource, objArgs)
		{
			var ctrlAmt = objSource.id.replace("csvRECEIPT_FROM_ID", "txtRECEIPT_AMOUNT");
			ctrlAmt = document.getElementById(ctrlAmt);
			
			if (ctrlAmt.value == "")
			{
				//Validating only if amount id entered else declare as valid field
				objArgs.IsValid = true;
			}
			else
			{
				if (document.getElementById(objSource.controltovalidate).value == "")
				{
					//For empty field is invalid
					objArgs.IsValid = false;
				}
				else
				{
					//For empty field is valid
					objArgs.IsValid = true;
				}
			}
		}
		
		//Validate the deposit type fields against empty
		function ValidateDEPOSIT_TYPE(objSource, objArgs)
		{
			var ctrlAmt = objSource.id.replace("csvRECEIPT_FROM_ID", "txtRECEIPT_AMOUNT");
			ctrlAmt = document.getElementById(ctrlAmt);
			
			if (ctrlAmt.value == "")
			{
				//Validating only if amount id entered else declare as valid field
				objArgs.IsValid = true;
			}
			else
			{
				if (document.getElementById(objSource.controltovalidate).value == "")
				{
					//For empty field is invalid
					objArgs.IsValid = false;
				}
				else
				{
					//For empty field is valid
					objArgs.IsValid = true;
				}
			}
		}
		
			
		//This function checks the validation of controls
		function DoValidationCheck()
		{
			var rowno = 0;
		
			for (ctr=1; ctr<=18; ctr++)
			{
			    rowno = ctr + 1;
			    var prefix = ""
			    if(parseInt(rowno)<10)
			        prefix = "dgDepositDetails_ctl0" + rowno;
			    else
			        prefix = "dgDepositDetails_ctl" + rowno;
				var amtCtrl = document.getElementById(prefix + "_txtRECEIPT_AMOUNT");
				var ErrStatus = document.getElementById(prefix + "_lblStatus");
				if(ErrStatus.innerHTML.trim() != "")
					{
						blnIsValid = false;
						return false;
					}
					
				
				if (amtCtrl != null)
				{
					if (amtCtrl.value != "")
					{
						//Checking whether the other controls r properly filled or not
						
						//Checking for policy number
						var policyNo = document.getElementById(prefix + "_txtPolicyNo");
						var ctrlcheckPolicyNo = document.getElementById(prefix + "_txtCheckPolicyNo");
						var ErrStatus = document.getElementById(prefix + "_lblStatus");
						if (policyNo.value.trim() == "")
						{
							document.getElementById(prefix + "_csvPolicyNo").style.display = "inline";
							blnIsValid = false;
						}
						
						//Checking for seconf policy number
						if (ctrlcheckPolicyNo.value.trim() == "")
						{
							document.getElementById(prefix + "_csvCheckPolicyNo").style.display = "inline";
							blnIsValid = false;
						}
						
						if (checkPolicyNo(rowno) == false)
						{
							blnIsValid = false;
						}
						//checkPolicyStatus(rowno);
					}
					else
					{
						//Checking whether all other fields are empty or not
						var policyNo = document.getElementById(prefix + "_txtPolicyNo");
						var ctrlcheckPolicyNo = document.getElementById(prefix + "_txtCheckPolicyNo");
						var ErrStatus = document.getElementById(prefix + "_lblStatus");
						if (policyNo.value.trim() != "")
						{
						blnIsValid = true;
							//blnIsValid = false;
							//document.getElementById(prefix + "_csvRECEIPT_AMOUNT").style.display = "inline";
							
						}
						
						//Checking for seconf policy number
						if (ctrlcheckPolicyNo.value.trim() != "")
						{
						blnIsValid = true;
							//blnIsValid = false;
							//document.getElementById(prefix + "_csvRECEIPT_AMOUNT").style.display = "inline";
						}
					}
				}
			}
			if (CompareTotal() == false)
			{
				return false;
			}
			
			if (blnIsValid == false)
			{
				return false;
			}	
		
			return (Page_IsValid && blnIsValid)
				
		}
		
		
		
		//
		//This function checks the validation of controls
	/*	function DoValidationCheckOnSave()
		{
			var rowno = 0;
			var blnIsValid = true;
			for (ctr=1; ctr<=20; ctr++)
			{
				rowno = ctr + 1;
				var prefix = "dgDepositDetails__ctl" + rowno;
				
				var amtCtrl = document.getElementById(prefix + "_txtRECEIPT_AMOUNT");
				
				
				if (amtCtrl != null)
				{
					if (amtCtrl.value != "")
					{
						//Checking whether the other controls r properly filled or not
						
						//Checking for policy number
						var policyNo = document.getElementById(prefix + "_txtPolicyNo");
						var ctrlcheckPolicyNo = document.getElementById(prefix + "_txtCheckPolicyNo");
						
						if (policyNo.value.trim() == "")
						{
							document.getElementById(prefix + "_csvPolicyNo").style.display = "inline";
							blnIsValid = false;
						}
						
						//Checking for seconf policy number
						if (ctrlcheckPolicyNo.value.trim() == "")
						{
							document.getElementById(prefix + "_csvCheckPolicyNo").style.display = "inline";
							blnIsValid = false;
						}
						if (checkPolicyNo(rowno) == false)
						{
							blnIsValid = false;
						}
						
					}
					else
					{
						//Checking whether all other fields are empty or not
						var policyNo = document.getElementById(prefix + "_txtPolicyNo");
						var ctrlcheckPolicyNo = document.getElementById(prefix + "_txtCheckPolicyNo");
						
						if (policyNo.value.trim() != "")
						{
							blnIsValid = false;
							document.getElementById(prefix + "_csvRECEIPT_AMOUNT").style.display = "inline";
						}
						
						//Checking for seconf policy number
						if (ctrlcheckPolicyNo.value.trim() != "")
						{
							blnIsValid = false;
							document.getElementById(prefix + "_csvRECEIPT_AMOUNT").style.display = "inline";
						}
					}
				}
			}			
			
			
			if (blnIsValid == false)
			{
				return false;
			}		
			
			return (Page_IsValid && blnIsValid)
		}
	*/	
		function CompareTotal()
		{
			//Calculating the total amount
			CalculateTotalAmount();
			//alert('compare');
			if (parseFloat(ReplaceString(document.getElementById("lblTotalAmount").innerHTML,",","")) !=  parseFloat(ReplaceString(document.getElementById("txtTapeTotal").value,",","")))
			{
				alert("Tape total is not equal to total amount.");
				document.getElementById("txtTapeTotal").focus();
				return false;
			}
			return true;
		}
		
		///AJAX Fxn to check pol status:
		function checkPolicyStatus(rowNo) {
		  
		    var prefix = '';
		    if (parseInt(rowNo) < 10)
		        prefix = "dgDepositDetails_ctl0" + rowNo;
		    else
		        prefix = "dgDepositDetails_ctl" + rowNo;
		        
			var txtPolicyNo = document.getElementById(prefix + "_txtPolicyNo");
			var txtPolicyNo2 = document.getElementById(prefix + "_txtCheckPolicyNo");
			FetchPolStatusXML(txtPolicyNo.value,rowNo);
		}
		function FetchPolStatusXML(PolNum,RowNo) {
		   
			var ParamArray = new Array();
			obj1=new Parameter('POLICY_NUMBER',PolNum);
			ParamArray.push(obj1);
			var objRequest = _CreateXMLHTTPObject();
			var Action = 'DEP_VALIDATE_POLNUM';
			strRowNum = RowNo;
			_SendAJAXRequest(objRequest,Action,ParamArray,CallbackFunPolStatus);
			strRowNum = RowNo;
			
			
			
		}
		function CallbackFunPolStatus(AJAXREsponse) {
		    debugger;
		    var prefix = ''; 
		    if (parseInt(strRowNum) < 10)
		         prefix = "dgDepositDetails_ctl0" + strRowNum;
		    else
		        prefix = "dgDepositDetails_ctl" + strRowNum;
		         
			var lblPolStatus = document.getElementById(prefix + "_lblPolStatus");
			var lblStatus = document.getElementById(prefix + "_lblStatus");
			var PolNumCtrl = new String();
			PolNumCtrl = lblPolStatus.id;
			var txtChkPolNum = PolNumCtrl.replace("lblPolStatus","txtCheckPolicyNo");
			var txtPolNum = PolNumCtrl.replace("lblPolStatus","txtPolicyNo");
			// If both PolicyNum controls are blank then don't shw validation msg.
			if(document.getElementById(txtChkPolNum).value != "" && document.getElementById(txtPolNum).value != "")
			{
				//alert(AJAXREsponse)
				if(AJAXREsponse == "5")
				{
					if(lblPolStatus !=null)
						lblPolStatus.innerHTML = "Cancellation in progress.";
				}
				else if(AJAXREsponse == "6")
				{
					if(lblPolStatus !=null)
						lblPolStatus.innerHTML = "Marked for Cancellation.";					
					
				}
				else if(AJAXREsponse == "7")
				{
					if(lblPolStatus !=null)
						lblPolStatus.innerHTML = "Cancelled.";
					
				}
				else if(AJAXREsponse == "8")
				{
					if(lblPolStatus !=null)
						lblPolStatus.innerHTML = "Cancelled.";						
				}
				else if(AJAXREsponse == "9")
				{
					if(lblPolStatus !=null)
						lblPolStatus.innerHTML = "Rescind in progress.";						
				}
				
				else
				{
					if(lblPolStatus !=null)
						lblPolStatus.innerHTML = "	";
					
				}
			}
		}
		
		///END
		
		//Checking the policy number whther the policy number 1st and 3rd row are same or not
		function checkPolicyNo(rowNo) {
		    var prefix = ''
		    if (parseInt(rowNo) < 10)
		        prefix = "dgDepositDetails_ctl0" + rowNo;
		    else
		        prefix = "dgDepositDetails_ctl" + rowNo;
			var txtPolicyNo = document.getElementById(prefix + "_txtPolicyNo");
			var txtPolicyNo2 = document.getElementById(prefix + "_txtCheckPolicyNo");
			var lblStatus = document.getElementById(prefix + "_lblStatus");

			if ((txtPolicyNo!=null && txtPolicyNo.value != "" && txtPolicyNo2!=null && txtPolicyNo2.value != "") && (txtPolicyNo.value.toUpperCase() != txtPolicyNo2.value.toUpperCase()))
			{
				lblStatus.innerHTML = "Policy numbers are not the same.";
				return false;
			}			
			else
			{   if(lblStatus!=null)
				    lblStatus.innerHTML = "	";
}
            if(txtPolicyNo!=null)
			    FetchXML(txtPolicyNo.value,rowNo);
		}
		
		
		//Checking for Invalid policy number 
	
		function CallbackFun(AJAXREsponse) {
		    var prefix = '';
		    if (parseInt(strRowNum) < 10)
		        prefix = "dgDepositDetails_ctl0" + strRowNum;
            else
                prefix = "dgDepositDetails_ctl" + strRowNum;
			    
			var lblStatus = document.getElementById(prefix + "_lblStatus");
			var lblPolStatus = document.getElementById(prefix + "_lblPolStatus");
			var PolNumCtrl = new String();
			PolNumCtrl = lblStatus.id;
			var txtChkPolNum = PolNumCtrl.replace("lblStatus","txtCheckPolicyNo");
			var txtPolNum = PolNumCtrl.replace("lblStatus","txtPolicyNo");
			// If both PolicyNum controls are blank then don't shw validation msg.
			if(document.getElementById(txtChkPolNum).value != "" && document.getElementById(txtPolNum).value != "")
			{
			//alert(AJAXREsponse)
				if(AJAXREsponse == "-2")
				{
					if(lblStatus !=null)
						lblStatus.innerHTML = "Policy is of AB type. Only DB policies are allowed.";
					Page_IsValid = false;
					blnIsValid = false;
				}
				else if(AJAXREsponse == "-4")
				{
					if(lblStatus !=null)
						lblStatus.innerHTML = "Policy Number entered is Invalid.";
					Page_IsValid = false;
					blnIsValid = false;
				}
				else
				{
					if(lblStatus !=null)
						lblStatus.innerHTML = "	";
					Page_IsValid = true;
					blnIsValid = true;
				}
			}
		}
		
		function FetchXML(PolNum,RowNo)
		{
			var ParamArray = new Array();
			obj1=new Parameter('POLICY_NUMBER',PolNum);
			ParamArray.push(obj1);
			var objRequest = _CreateXMLHTTPObject();
			var Action = 'DEP_VALIDATE_POLNUM';
			strRowNum = RowNo;
			_SendAJAXRequest(objRequest,Action,ParamArray,CallbackFun);
			strRowNum = RowNo;
			
		}
		
		//Calculates the total of receipts amount
		function CalculateTotalAmount()
		{
			var total = 0;
			var prefix;
			
			for (ctr=1; ctr<=20; ctr++)
			{
			    rowNo = ctr + 1;
			    
				if(parseFloat(rowNo)<10)
				    prefix = "dgDepositDetails_ctl0" + rowNo;
				else
				    prefix = "dgDepositDetails_ctl" + rowNo;
				
				ctrl = document.getElementById(prefix + "_txtRECEIPT_AMOUNT");
				
				if (ctrl != null)
				{
					amt = ReplaceAll(ctrl.value,",","");
					
					if (amt != "" && !isNaN(amt))
					{
						total = total + parseFloat(amt);
					}
				}
			}	
			//alert(InsertDecimal(total.toFixed(2)) + "Total_Amount");	
			document.getElementById("lblTotalAmount").innerHTML = InsertDecimal(total.toFixed(2));
			//alert(document.getElementById("lblTotalAmount").innerHTML);
		}
		
		//This function activates the first tab
		function BackClick()
		{
			this.parent.changeTab(0,0);
			return false;
		}
		function SetParentElement()
		{
		
			var Deopsit_Id = document.getElementById('hidDEPOSIT_ID').value;
			//alert(window.parent.self.document.forms[0].hidSELECTED_DEP_ID.value);
			window.parent.self.document.forms[0].hidSELECTED_DEP_ID.value = Deopsit_Id ;
			//window.parent.self.document.forms[0].hidlocQueryStr.value=Deopsit_Id  + "&DEPOSIT_TYPE";
			//alert(window.parent.self.document.forms[0].hidSELECTED_DEP_ID.value);

		}
		</script>
</HEAD>
	<body leftMargin="0" topMargin="0" onload="SetPayorType();SetParentElement();" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="ACT_CURRENT_DEPOSIT_LINE_ITEMS" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td align="center"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False" EnableViewState="False"></asp:label></td>
				</tr>
				<tr>
					<td class="midcolora"><b><asp:Label ID="lblDepositNum" Runat="server">Deposit Number :</asp:Label></b>
					<b><asp:Label ID="lblDEPOSIT_NUM" Runat="server"></asp:Label></b></td>
				</tr>
				<tr>
					<td class="midcolora"><asp:datagrid id="dgDepositDetails" runat="server" DataKeyField="CD_LINE_ITEM_ID" AutoGenerateColumns="False"
							Width="100%" ItemStyle-Height="5">
							<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
							<ItemStyle CssClass="midcolora"></ItemStyle>
							<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
							<ItemStyle Height="4"></ItemStyle>
							<Columns>
								<%--<asp:TemplateColumn >
									<ItemTemplate>
										<input type="hidden" id="hidCD_LINE_ITEM_ID" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "CD_LINE_ITEM_ID")%>' />
									</ItemTemplate>
								</asp:TemplateColumn>--%>
								<asp:TemplateColumn  ItemStyle-Width="2%">
									<ItemTemplate>
									    <input type="hidden" id="hidCD_LINE_ITEM_ID" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "CD_LINE_ITEM_ID")%>' />
										<asp:CheckBox id="chkDelete" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							<%-- Added By Pradeep Kushwaha on 21-oct-2010 --%>
								<asp:TemplateColumn HeaderText="Our No/Boleto #" ItemStyle-Width="11%">
									<ItemTemplate>
										<asp:TextBox name="tt" ID="txtBOLETO_NO" MaxLength="10" size="15" Runat="server" ></asp:TextBox><span id="spnBOLETO_NO" runat="server"><IMG id="imgBOLETO_NO" style="CURSOR: hand" onclick="OpenPolicyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
												runat="server"> </span>
										<asp:CustomValidator id="csvBOLETO_NO" runat="server" ControlToValidate="txtBOLETO_NO" ErrorMessage="Please select boleto number." ClientValidationFunction="CheckPolicyNumber" Display="Dynamic"></asp:CustomValidator>
										<input type="hidden" id="hidBOLETO_ID" runat="server"/> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Policy #" ItemStyle-Width="11%">
									<ItemTemplate>
										<asp:TextBox name="tt" ID="txtPolicyNo" MaxLength="21" size="20" Runat="server" TextMode="Password"></asp:TextBox><span id="spnPOLICY_NO" runat="server"><IMG id="imgPOLICY_NO" style="CURSOR: hand" onclick="OpenPolicyLookup();" alt="" src="../../cmsweb/images/selecticon.gif" runat="server"> </span>
										<asp:CustomValidator id="csvPolicyNo" runat="server" ControlToValidate="txtPolicyNo" ErrorMessage="Please select policy number." ClientValidationFunction="CheckPolicyNumber" Display="Dynamic"></asp:CustomValidator>
										<input type="hidden" id="hidPOLICY_ID" runat="server"/> <input type="hidden" id="hidPOLICY_VERSION_ID" runat="server"/>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Risk Pre" ItemStyle-Width="5%">
									<ItemTemplate>
										<asp:TextBox name="tt" ID="txtRISK_PREMIUM" MaxLength="10" CssClass="InputCurrency"  size="10" Runat="server" ></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Fees" ItemStyle-Width="5%">
									<ItemTemplate>
										<asp:TextBox name="tt" ID="txtFEE" MaxLength="10" size="10" CssClass="InputCurrency" Runat="server" ></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Taxes" ItemStyle-Width="3%">
									<ItemTemplate>
										<asp:TextBox name="tt" ID="txtTAX" MaxLength="10" CssClass="InputCurrency" size="6" Runat="server" ></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Interest" ItemStyle-Width="5%">
									<ItemTemplate>
										<asp:TextBox name="tt" ID="txtINTEREST" MaxLength="10" CssClass="InputCurrency" size="8" Runat="server" ></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="Late Fees" ItemStyle-Width="8%">
									<ItemTemplate>
										<asp:TextBox name="tt" ID="txtLATE_FEE" MaxLength="10" CssClass="InputCurrency"  size="12" Runat="server" ></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
							
								<%-- Added end till here  --%>
								<asp:TemplateColumn HeaderText="Receipt Amt" ItemStyle-Width="8%">
									<ItemTemplate>
										<asp:TextBox ID="txtRECEIPT_AMOUNT" MaxLength="11" CssClass="InputCurrency" Runat="server" size="12" Text='<%# FormatMoney(DataBinder.Eval(Container.DataItem, "RECEIPT_AMOUNT"))%>'>
										</asp:TextBox>
										<asp:RegularExpressionValidator ID="revRECEIPT_AMOUNT" Runat="server" Display="Dynamic" ControlToValidate="txtRECEIPT_AMOUNT"></asp:RegularExpressionValidator>
										
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Policy #" ItemStyle-Width="8%">
									<ItemTemplate>
										<asp:TextBox ID="txtCheckPolicyNo" Runat="server" MaxLength="21" size="30"></asp:TextBox>
										<asp:CustomValidator id="csvCheckPolicyNo" runat="server" ControlToValidate="txtCheckPolicyNo" ErrorMessage="Please select policy number." ClientValidationFunction="CheckPolicyNumber" Display="Dynamic"></asp:CustomValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Status" ItemStyle-Width="44%">
									<ItemTemplate>
										<asp:Label ID="lblStatus" CssClass="errmsg" Runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Policy Status" ItemStyle-Width="74%">
									<ItemTemplate>
										<asp:Label ID="lblPolStatus" CssClass="errmsg" Runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td class="midcolorc"><asp:label id="lblPagingLeft" runat="server"></asp:label>
						<asp:label id="lblPageCurr" runat="server"></asp:label><asp:label id="lblPagingRight" runat="server"></asp:label><asp:label id="lblPageLast" runat="server"></asp:label>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%">
							<tr>
								<td class="midcolorr" width="15%"><asp:label id="lblMsg" CssClass="labelfont" Runat="server">Total Amount&nbsp;</asp:label></td>
								<td class="midcolorr" width="12%"><asp:label id="lblTotalAmount" CssClass="LabelFont" Runat="server"></asp:label></td>
								<td class="midcolorr" width="25%"><asp:label id="lblTapeTotal" CssClass="labelfont" Runat="server">Tape Total&nbsp;</asp:label></td>
								<td class="midcolora" width="50%"><asp:textbox id="txtTapeTotal" CssClass="INPUTCURRENCY" Runat="server"></asp:textbox>
								<br>
									<asp:regularexpressionvalidator id="rfvTapeTotal" Runat="server" ErrorMessage="Please enter a ValidationExpression amount"
										Display="Dynamic" ControlToValidate="txtTapeTotal"></asp:regularexpressionvalidator>
								
								</td>
							</tr>
							<tr>
								<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton> 
								<cmsb:cmsbutton class="clsButton" id="btnCompareTotal" runat="server" Text="Compare Total" visible="false">	</cmsb:cmsbutton> 
								<cmsb:cmsbutton class="clsButton" id="btnPrevious" runat="server" Text="Previous">	</cmsb:cmsbutton> 
								<cmsb:cmsbutton class="clsButton" id="btnNext" runat="server" Text="Next"></cmsb:cmsbutton> 
								<cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Text="Back"></cmsb:cmsbutton> 
							</td>
								<td class="midcolorr" align="right"><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton>
								<cmsb:cmsbutton class="clsButton" id="btnConfirm" style="DISPLAY: none" runat="server" Text="Confirm"></cmsb:cmsbutton>
								<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save &amp; Confirm"></cmsb:cmsbutton>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> 
					<INPUT id="hidDEPOSIT_ID" type="hidden" value="0" name="hidDEPOSIT_ID" runat="server">
					<INPUT id="hidDEPOSIT_TYPE" type="hidden" name="hidDEPOSIT_TYPE" runat="server">
					</td>
				</tr>
			</table>
			<input id="hidGLAccountXML" type="hidden" runat="server"> <input id="hidPOLICYINFO" type="hidden" runat="server">
		</form>
	</body>
</HTML>

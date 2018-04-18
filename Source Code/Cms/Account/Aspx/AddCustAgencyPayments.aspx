<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddCustAgencyPayments.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AddCustAgencyPayments" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddCustAgencyPayments</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Page-Enter" content="revealtrans(duration=0.0)">
		<meta http-equiv="Page-Exit" content="revealtrans(duration=0.0)">
		<LINK href="../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script src="../../cmsweb/scripts/AJAXcommon.js"></script>
		<SCRIPT language=VBScript>
		function PolicyStatusConfirmation(status)
			if (status = "CANCEL" or status = "SCANCEL" or status = "INACTIVE" or status ="NONRENEWED" or status="MNONRENEWED")	then		
				if msgbox ("The status of this policy is Cancelled or Non-Renewed do you want to proceed ?",vbyesno + vbDefaultButton2,"POLICY STATUS : " + status) = vbYes then
						
				else
					ClearValues() 'JS Call
				end if
			end if			
		end function
		</SCRIPT>
		<script language="javascript">
		function ClearValues()
		{
			  document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_txtPolicyNo').value = '';
			  document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_lblTOTAL_DUE').innerHTML = '';
			  document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_lblMIN_DUE').innerHTML = '';
			  document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_lblCUSTOMER_NAME').innerHTML = '';
			  document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_txtPolicyNo').focus();
			  document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_txtAMOUNT').value = "";
			 
			  
		}
		var strRowNum="";
		
		function CallbackFun(AJAXREsponse)
		{			
			if(AJAXREsponse == "0") // Invalid Policy Number or Agency does not allow EFT
			 {
			  alert('Entered Policy Number does not exist. Please try again.');
			  document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_txtPolicyNo').value = '';
			  document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_lblTOTAL_DUE').innerHTML = '';
			  document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_lblMIN_DUE').innerHTML = '';
			  document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_lblCUSTOMER_NAME').innerHTML = '';
			  document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_txtPolicyNo').focus();
			  document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_txtAMOUNT').value = "";
			  return false;
			 }
			 if(AJAXREsponse == "1") 
			 {
				 alert('Policy does not belong to this agency.');
				 document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_txtPolicyNo').value = '';
				 document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_txtPolicyNo').focus();
				 return false;
			 }
			if(AJAXREsponse == "-1") 
			{
				alert('Entered Policy Number is of AB type. Please enter only DB type Policy Number.');
				document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_txtPolicyNo').value = '';
				document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_txtPolicyNo').focus();
				return false;					
			}	
			 
			var strResponse = AJAXREsponse.split('~'); // AJAXREsponse contains Total Due & Min Due separated by '~'
			//alert(strResponse)
			var TotalDue = document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_lblTOTAL_DUE');	
			if(TotalDue !=null)
				TotalDue.innerText = formatAmount(strResponse[0]);
			var hidTotalDue = document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_hidTOTAL_DUE');	
			if(hidTotalDue !=null)
				hidTotalDue.value = strResponse[0];
			var MinDue = document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_lblMIN_DUE');
			if(MinDue !=null)
				MinDue.innerText = formatAmount(strResponse[1]);
			var hidMinDue = document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_hidMIN_DUE');	
			if(hidMinDue !=null)
				hidMinDue.value = strResponse[1];
			//Added For Itrack #5243
			var policyNo = document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_txtPolicyNo').value;		
            document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_txtPolicyNo').value = policyNo.toUpperCase();
			var CustId = document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_hidCUSTOMER_ID');	
		
			var PolId = document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_hidPOLICY_ID');	
			
			var PolVerId = document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_hidPOLICY_VERSION_ID');	
			
			//Added for AgencyID ITRACK : #3933
			var AgencyId = document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_hidAGENCY_ID');
			
			//Statement Commented For Itrack Issue #6051 & #6172. 
			//if(AgencyId.value == "" && typeof(AgencyId) != 'undefined')
			if(typeof(AgencyId) != 'undefined')
			{
				AgencyId.value = strResponse[2];
			}
			//END			
			//Blank Check remove For Itrack Issue #6051. 
			if(typeof(CustId) != 'undefined')
			{
				
				CustId.value = strResponse[6];
			       	
			}
				
			
			if(typeof(PolId) != 'undefined')	
			{
				PolId.value = strResponse[7];
				//alert(PolId.value + ': POLICY ID')
			}
			
				
			if(typeof(PolVerId) != 'undefined')	
			{
				PolVerId.value = strResponse[8];
				//alert(PolVerId.value+ ':POL VERSIONID')
			}
			
			if(strResponse[2]) // Comes only in case Policy Num has been entered directly in Textbox
			{
				//document.getElementById('hidAGENCY_ID').value = strResponse[2];
				//Testing Agency ID (to be removed)
				//var agency = document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_hidAGENCY_ID');
				//alert(agency);
				//var lbl = "dgCustAgencyPay__ctl" + strRowNum + "_lblAgencyID";
					//document.getElementById(lbl).innerHTML = strResponse[2];	
				//End Agency ID
				
				var lblCustName = "dgCustAgencyPay__ctl" + strRowNum + "_lblCUSTOMER_NAME";	
				if(document.getElementById(lblCustName) != null)	
					document.getElementById(lblCustName).innerHTML = strResponse[3];
				var AllowEFT = document.getElementById('dgCustAgencyPay__ctl' + strRowNum + '_hidALLOW_EFT');
				if(AllowEFT != null)
					AllowEFT.value = strResponse[4];
			}
			
				//Get Status
			var polStatus = strResponse[9];
				PolicyStatusConfirmation(polStatus); //VB Function
			
		}
		function FetchXML(PolVerID,PolID,CustId,PolNum,CalledFrom,RowNo)
		{
			var ParamArray = new Array();
			obj1=new Parameter('POLICY_VERSION_ID',PolVerID);
			obj2=new Parameter('POLICY_ID',PolID);
			obj3=new Parameter('CUSTOMER_ID',CustId);
			obj4=new Parameter('POLICY_NUMBER',PolNum);
			obj5=new Parameter('CALLED_FROM',CalledFrom);
			ParamArray.push(obj1);
			ParamArray.push(obj2);
			ParamArray.push(obj3);
			ParamArray.push(obj4);
			ParamArray.push(obj5);
			var objRequest = _CreateXMLHTTPObject();
			var Action = 'BAL';
			strRowNum = RowNo;
			_SendAJAXRequest(objRequest,'BAL',ParamArray,CallbackFun);
			strRowNum = RowNo;
			//alert()
			
		}
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
		//This function open the policy lookup window
		function OpenPolicyLookup(RowNo)
		{
			var url='<%=URL%>';
			var txtCtrl	 = "dgCustAgencyPay__ctl" + RowNo + "_txtPolicyNo";
			var AgenCode ="'<%=AgenCode%>'";
			var CarrierCode = "'<%=CarrierCode%>'";
			var strCode = "'%[^~]%'";
			if(AgenCode == CarrierCode) // Logged User is Wolverine
				OpenLookupWithFunction(url,'POLICY_APP_NUMBER','CUSTOMER_ID_NAME','hidPOLICY_APP_NUMBER',txtCtrl,'PolicyAgency','Policy','@AGENCY_CODE=' +strCode,'splitPolicy(' + RowNo + ')');
			else
				OpenLookupWithFunction(url,'POLICY_APP_NUMBER','CUSTOMER_ID_NAME','hidPOLICY_APP_NUMBER',txtCtrl,'PolicyAgency','Policy','@AGENCY_CODE=' +AgenCode,'splitPolicy(' + RowNo + ')');
		
		}
		
		//This function splits the policyid and policy version id and put it in different controls
		function splitPolicy(RowNo)
		{
			var txtCtrl = "dgCustAgencyPay__ctl" + RowNo + "_txtPolicyNo";			
			var hidPolID = "dgCustAgencyPay__ctl" + RowNo + "_hidPOLICY_ID";	
			var hidPolVerID = "dgCustAgencyPay__ctl" + RowNo + "_hidPOLICY_VERSION_ID";	
			var hidCustID = "dgCustAgencyPay__ctl" + RowNo + "_hidCUSTOMER_ID";	
			var hidAllowEFT = "dgCustAgencyPay__ctl" + RowNo + "_hidALLOW_EFT";
			var rfvPolNo  = "dgCustAgencyPay__ctl" + RowNo + "_csvPolicyNo";	
			
			//Added on ITRACK #3933
			var hidAgencyID  = "dgCustAgencyPay__ctl" + RowNo + "_hidAGENCY_ID";	
			//
		
			if (document.getElementById("hidPOLICY_APP_NUMBER").value.length > 0)
			{
			    /*
				86~5~W7000007~360~1~W1002558APP~626~219~10964~Abe  
			    */
				var arr = document.getElementById("hidPOLICY_APP_NUMBER").value.split("~");
				document.getElementById(txtCtrl).value = arr[2]; // Policy Number
				var CustId = arr[6]; // Customer ID
				var PolID = arr[0];
				var PolVerID = arr[1];
				document.getElementById(hidPolID).value = arr[0];			// Policy ID
				document.getElementById(hidPolVerID).value = arr[1];		// Policy Version ID
				document.getElementById(hidAgencyID).value = arr[7];		// Agency ID 
				
					
				document.getElementById(hidCustID).value = arr[6];			// Customer ID
				document.getElementById(hidAllowEFT).value = arr[8];
				
				/*
				alert('POL ID = '+document.getElementById(hidPolID).value)
				alert('CUST ID = '+document.getElementById(hidCustID).value)
				alert('POL VER = '+document.getElementById(hidPolVerID).value)      // EFT value
				*/
				FetchXML(PolVerID,PolID,CustId,'','L',RowNo)
				if(rfvPolNo !=null)
					document.getElementById(rfvPolNo).style.display = 'none';
				var lblCustName = "dgCustAgencyPay__ctl" + RowNo + "_lblCUSTOMER_NAME";			
				if(lblCustName !=null)
					document.getElementById(lblCustName).innerHTML = arr[9];
					
					//Added on Dec 27 2007
				var polStatus = arr[10];
				
				/*Commented by Asfa(20-June-2008) - iTrack #4368
				if(polStatus!="")
					PolicyStatusConfirmation(polStatus); //VB Function
				*/
					
				
			}
		}
		
		// Checking whether any record selected or not
		function DeleteRows()
		{
			for (ctr=1; ctr<=20; ctr++)
			{
				flag = 0;
				if(document.getElementById("dgCustAgencyPay__ctl" + (ctr + 1)+ "_chkSelect") != null)
				{
					if (document.getElementById("dgCustAgencyPay__ctl" + (ctr + 1)+ "_chkSelect").checked == true)
					{
						flag = 1;
						break;
					}
				}
			}
			
			if (flag == 0)
			{	
				// Not a single row is selected , hence returning false
				alert("You have not selected checkboxes for rows.");
				HideShowCommitInProgress();
				return false;
			}
			else
				return true;
		}
		//Added For Itrack Issue #5315.		
		function CheckedRow()
		{
		   for (ctr=1; ctr<=20; ctr++)
			{			 
			 if(document.getElementById("dgCustAgencyPay__ctl" + (ctr + 1)+ "_txtPolicyNo")!= null)	
			 {
			  if(document.getElementById("dgCustAgencyPay__ctl" +(ctr + 1)+ "_txtPolicyNo").value!='')			  
			     {
			        if (document.getElementById("dgCustAgencyPay__ctl" +(ctr + 1)+ "_chkSelect").checked == false)
			          {
			            
			              alert("You have not selected checkboxes for rows.");
			              HideShowCommitInProgress();			              
			                  return false;    
			          }
			       
			        
			     }			  
			}
			}	
			return true;	      
		}
		//End 
		
		//This function checks the validation of controls
		function DoValidationCheckOnSave()
		{			    
		     //Page_IsValid Added For Itrack Issue #5979.
		    //alert('hi'); 
		  if(Page_IsValid == true)
	      { 
		   
			if(!DeleteRows()) // Checkbox has not been checked
				return false;
		//Added For Itrack Issue #5315.	
				if(!CheckedRow())	         
                return false; 
			var rowno = 0;
			var blnIsValid = true;
			for (ctr=1; ctr<=18; ctr++)
			{
				rowno = ctr + 1;
				var prefix = "dgCustAgencyPay__ctl" + rowno;
				var PolNoCtrl = document.getElementById(prefix + "_txtPolicyNo");
				if (document.getElementById("dgCustAgencyPay__ctl" + rowno+ "_chkSelect").checked == true)
				{	
					if (PolNoCtrl != null)
					{ 
						if (PolNoCtrl.value != "")
						{
							//Checking whether the other controls are properly filled or not
							//Checking for policy number
						//alert('');
							var policyNo = document.getElementById(prefix + "_txtPolicyNo");
							//alert(policyNo);
							var amt		 = document.getElementById(prefix + "_txtAMOUNT");
							//alert(amt);
							var mode	 = document.getElementById(prefix + "_cmbMODE");
							if (policyNo.value.trim() == "")
							{
								document.getElementById(prefix + "_csvPolicyNo").style.display = "inline";
								blnIsValid = false;
							}
							
							//Checking for amount
							if (amt.value.trim() == "")
							{
								document.getElementById(prefix + "_csvAMOUNT").style.display = "inline";
								blnIsValid = false;
							}
							//Checking for mode
							if (mode.selectedIndex == 0)
							{
								document.getElementById(prefix + "_csvMode").style.display = "inline";
								blnIsValid = false;
							}
						
						
						}
						else
						{
							//Checking whether all other fields are empty or not
							var policyNo = document.getElementById(prefix + "_txtPolicyNo");
							var amt		 = document.getElementById(prefix + "_txtAMOUNT");
							var mode	 = document.getElementById(prefix + "_cmbMODE");
						
							if (policyNo.value.trim() == "")
							{
								blnIsValid = false;
								document.getElementById(prefix + "_csvPolicyNo").style.display = "inline";
							}
							if (amt.value.trim() == "")
							{
								blnIsValid = false;
								document.getElementById(prefix + "_csvAMOUNT").style.display = "inline";
							}
							if (mode.selectedIndex == 0)
							{
								blnIsValid = false;
								document.getElementById(prefix + "_csvMode").style.display = "inline";
							}
						}
					}
				}
			}			
			

			if (blnIsValid == false)
			{				
				return false;
			}		
			else 
			{					
				return true;
			}
		  }
		    else
		   {
		        return false ;   
		   }		
		  
		}
		
		function CheckPolicyNumber(objSource, objArgs)
		{	
			if (objSource.controltovalidate != null )
			{
				var ctrl = document.getElementById(objSource.controltovalidate);
				var rowNo = ctrl.getAttribute("RowNo");
			
				rowNo = document.getElementById("dgCustAgencyPay__ctl" + rowNo + "_txtPolicyNo");
				if(rowNo != null)
				{
					if (rowNo.value != "")
					{
						if (ctrl.selectedIndex == 0)
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
		}
	
		//Validating the receipt amount(whther entered or not)
		function ValidateAMOUNT(objSource, objArgs)
		{
			var ctrlAmt = objSource.id.replace("csvAMOUNT","txtAMOUNT");
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
		
		//Validating the MODE(whether entered or not)
		function ValidateMODE(objSource, objArgs)
		{
			var ctrlMode = objSource.id.replace("csvPayMode","cmbMODE");
			ctrlMode = document.getElementById(ctrlMode);
			
			if (ctrlMode.value == "")
			{
				objArgs.IsValid = true;
			}
			else
			{
				if (document.getElementById(objSource.controltovalidate).value == "")
				{
					objArgs.IsValid = false;
				}
				else
				{
					objArgs.IsValid = true;
				}
			}
		}
		
		// enable/disable validators on check change
		function chkSelDeSel(objChk)
		{ 
		
			if(!objChk.checked)
			{
				//dgCustAgencyPay__ctl3_chkSelect
				var strChkID		= new String(objChk.id);
				
				// Below check has been incorporated to clear only those fields on uncheck
				// who have not been saved yet.
				var strID	= strChkID.replace("_chkSelect","_hidIDEN_ROW_ID");
				if(document.getElementById(strID).value == "" || document.getElementById(strID).value == null)
				{
					var strTxtPolicyNo	= strChkID.replace("_chkSelect","_txtPolicyNo");
					document.getElementById(strTxtPolicyNo).value = "";
					
					var strTxtAmtID		= strChkID.replace("_chkSelect","_txtAMOUNT");
					document.getElementById(strTxtAmtID).value = "";
					
					var strlblMIN_DUE	= strChkID.replace("_chkSelect","_lblMIN_DUE");
					document.getElementById(strlblMIN_DUE).innerHTML = "";
					
					var strlblTOTAL_DUE	= strChkID.replace("_chkSelect","_lblTOTAL_DUE");
					document.getElementById(strlblTOTAL_DUE).innerHTML = "";
					
					var strcmbMode	= strChkID.replace("_chkSelect","_cmbMode");
					document.getElementById(strcmbMode).selectedIndex = 0;
					
					var strlblCUSTOMER_NAME	= strChkID.replace("_chkSelect","_lblCUSTOMER_NAME");
					document.getElementById(strlblCUSTOMER_NAME).innerHTML = "";
				}
			}
			
		}
		// Fetch Total Due, Min Due if Policy Number has been entered directly into textbox
		function FillPolDetails(RowNo)
		{
			var PolNum = "dgCustAgencyPay__ctl" + RowNo + "_txtPolicyNo";
			PolNum = document.getElementById(PolNum).value;
			if(PolNum != "")
				FetchXML('','','',PolNum,'T',RowNo);
			
		}
	
		// Disables the fired validators on checkbox ' UNCHECK'
		function HideErrMsgsOnCheckChange(CtrlID)
		{
			var chkCtrl  = document.getElementById(CtrlID);
			var strCtrl  = new String();
			strCtrl		 = CtrlID;
			var PolCtrl  = document.getElementById(strCtrl.replace("chkSelect","csvPolicyNo"));
			var AmtCtrl  = document.getElementById(strCtrl.replace("chkSelect","csvAMOUNT"));
			var ModeCtrl = document.getElementById(strCtrl.replace("chkSelect","csvMODE"));
			if(!chkCtrl.checked)
			{
				PolCtrl.style.display = 'none';
				AmtCtrl.style.display = 'none';
				ModeCtrl.style.display = 'none';
				
				PolCtrl.IsValid = true;
				AmtCtrl.IsValid = true;
				ModeCtrl.IsValid = true;
			}
		}
		
		function chkSelDeSelAll(chkAllctrl)
		{
			var strCtrl  = new String();
			if (chkAllctrl.checked)
			{
				aspCheckBoxID = 'chkSelect';	//for seond checkbox section
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.ACT_CUSTOMER_PAYMENTS_FROM_AGENCY.elements.length; i++)
				{
					chkAllctrl = document.ACT_CUSTOMER_PAYMENTS_FROM_AGENCY.elements[i]
					if (chkAllctrl.type == 'checkbox')
					{
						strCtrl	= chkAllctrl.name;
						var AmtCtrl = document.getElementById(strCtrl.replace("chkSelect","txtPolicyNo"));
						if(AmtCtrl != null && AmtCtrl.value != "")
						{//alert(chkAllctrl.name)
							if (re.test(chkAllctrl.name)) 
							chkAllctrl.checked = true;
						}
					}
				}
			}
			else
			{
				aspCheckBoxID = 'chkSelect';	//for seond checkbox section
				re = new RegExp(':' + aspCheckBoxID + '$')  
				for(i = 0; i < document.ACT_CUSTOMER_PAYMENTS_FROM_AGENCY.elements.length; i++)
				 {
					chkAllctrl = document.ACT_CUSTOMER_PAYMENTS_FROM_AGENCY.elements[i]
					if (chkAllctrl.type == 'checkbox')
					{
						if (re.test(chkAllctrl.name)) 
						chkAllctrl.checked = false;
					}
				}
			}
		}
		//Added by Shikha for #5534 on 12/03/09.
		function HideCommit()
		{
			if(document.getElementById('btnConfirming')!=null)
				{
					document.getElementById('btnConfirming').style.display="none";
				}
		}
		
		function HideShowCommit()
		{
			if(document.getElementById('btnConfirm')!=null)
				document.getElementById('btnConfirm').style.display="none";
				
			if(document.getElementById('btnConfirming')!=null)
				{
					document.getElementById('btnConfirming').style.display="inline";
					document.getElementById('btnConfirming').disabled="true";
				}
		}
		
		//Function Added For Itrack Issue #6730
		function HideShowCommitIn(btn)
		{
		    //document.getElementById('btnSave').disabled="true";
		    var check = DoValidationCheckOnSave()
		    if(check == false)
		    {
		      return false 
		     }
		   
		   if(Page_ClientValidate() && event.keyCode!=13 && check )
		   {
		   // alert('dopost');
		    document.getElementById('btnConfirm').disabled="true";
		    document.getElementById('btnSave').disabled="true";
		    document.getElementById('btnConfirmPrint').disabled="true";
		    document.getElementById('btnDelete').disabled="true";
		    __doPostBack(btn.id,btn.id); 
		    }
		    
		}
		
		//Added - Itrack 6048
		function HideShowCommitInProgress()
		{			
			if(document.getElementById('btnConfirm')!=null)
			{
				document.getElementById('btnConfirm').style.display="inline";
				if(document.getElementById('btnConfirming')!=null)
					document.getElementById('btnConfirming').style.display="none";
				
			}
			
			
		}
		
		
		//End of Addition.
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" class="bodyBackGround" topMargin="0" onload="HideCommit();ApplyColor();ChangeColor();top.topframe.main1.mousein = false;findMouseIn();showScroll();">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<form id="ACT_CUSTOMER_PAYMENTS_FROM_AGENCY" method="post" runat="server">
				<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0">
					<tr>
						<td><webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer></td>
					</tr>
					<tr>
						<td class="headereffectcenter" colSpan="3">Customer Payments from Agency</td>
					</tr>
					<tr>
						<td align="center"><asp:label id="lblMessage" runat="server" EnableViewState="False" Visible="False" CssClass="errmsg"></asp:label></td>
					</tr>
					<tr class="headereffectWebGrid">
						<td colspan="3">
							&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkSelectAll" Runat="server" onclick="chkSelDeSelAll(this);"></asp:CheckBox>&nbsp;Select 
							All
						</td>
					</tr>
					<tr>
						<td class="midcolora"><asp:datagrid id="dgCustAgencyPay" runat="server" ItemStyle-Height="5" Width="100%" DataKeyField="IDEN_ROW_ID"
								AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="midcolora"></AlternatingItemStyle>
								<ItemStyle CssClass="midcolora"></ItemStyle>
								<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
								<ItemStyle Height="4"></ItemStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Select" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<asp:CheckBox id="chkSelect" name="chkSelect" runat="server" onClick="chkSelDeSel(this);"></asp:CheckBox>
											<input type="hidden" id="hidIDEN_ROW_ID" runat="server" name="hidIDEN_ROW_ID" value ='<%# DataBinder.Eval(Container.DataItem,"IDEN_ROW_ID")%>'>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Policy #" ItemStyle-Width="6%" HeaderStyle-HorizontalAlign="Center"
										ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<asp:TextBox ID="txtPolicyNo" MaxLength="8" size="10" Runat="server" text='<%# DataBinder.Eval(Container.DataItem, "POLICY_NUMBER")%>'>
											</asp:TextBox>
											<IMG id="imgPOLICY_NO" style="CURSOR: hand" onclick="OpenPolicyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
												runat="server">
											<br>											
											<asp:CustomValidator id="csvPolicyNo" runat="server" ControlToValidate="txtPolicyNo" ErrorMessage="Please select policy number."
												ClientValidationFunction="CheckPolicyNumber" Display="Dynamic"></asp:CustomValidator>												
											<input type="hidden" id="hidPOLICY_ID" runat="server" NAME="hidPOLICY_ID" value='<%# DataBinder.Eval(Container.DataItem, "POLICY_ID")%>'>
											<input type="hidden" id="hidPOLICY_VERSION_ID" runat="server" NAME="hidPOLICY_VERSION_ID" value='<%# DataBinder.Eval(Container.DataItem, "POLICY_VERSION_ID")%>'>
											<input type="hidden" id="hidCUSTOMER_ID" runat="server" NAME="hidCUSTOMER_ID" value='<%# DataBinder.Eval(Container.DataItem, "CUSTOMER_ID")%>'>
										</ItemTemplate>
									</asp:TemplateColumn>
									
									<asp:TemplateColumn HeaderText="Customer Name" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<asp:Label ID="lblCUSTOMER_NAME" Runat="server" CssClass="clsLabel" text='<%# DataBinder.Eval(Container.DataItem, "CUSTOMER_NAME")%>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Total Due" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<asp:Label ID="lblTOTAL_DUE" Runat="server" CssClass="clsLabel" text='<%# DataBinder.Eval(Container.DataItem, "TOTAL_DUE")%>'>
											</asp:Label>
											<input type="hidden" id="hidTOTAL_DUE" name="hidTOTAL_DUE" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "TOTAL_DUE")%>'>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Minimum Due" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
										ItemStyle-HorizontalAlign="Right">
										<ItemTemplate>
											<asp:Label ID="lblMIN_DUE" Runat="server" CssClass="clsLabel" text='<%# DataBinder.Eval(Container.DataItem, "MIN_DUE")%>'>
											</asp:Label>
											<input type="hidden" id="hidMIN_DUE" name="hidMIN_DUE" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "MIN_DUE")%>'>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Amount" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
										<ItemTemplate>
											<asp:TextBox ID="txtAMOUNT" MaxLength="11" CssClass="InputCurrency" Runat="server" size="12" onBlur="FormatAmount(this)" text='<%# DataBinder.Eval(Container.DataItem, "AMOUNT")%>' >
											</asp:TextBox>
											<br>
											<asp:RegularExpressionValidator ID="revAMOUNT" Runat="server" Display="Dynamic" ControlToValidate="txtAMOUNT" ErrorMessage="Please enter amount in numeric."></asp:RegularExpressionValidator>
											<asp:CustomValidator id="csvAMOUNT" runat="server" ControlToValidate="txtAMOUNT" ErrorMessage="Please enter amount."
												ClientValidationFunction="ValidateAMOUNT" Display="Dynamic"></asp:CustomValidator>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Mode" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
										<ItemTemplate>
											<asp:DropDownList ID="cmbMODE" Runat="server"></asp:DropDownList>
											<input type="hidden" id="hidALLOW_EFT" runat="server" name="hidALLOW_EFT" value='<%# DataBinder.Eval(Container.DataItem, "ALLOWS_EFT")%>'>
											<input type="hidden" id="hidAGENCY_ID" name="hidAGENCY_ID" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "AGENCY_ID")%>'>
											<br>
											<asp:CustomValidator id="csvMode" runat="server" ControlToValidate="cmbMODE" ErrorMessage="Please select Mode."
												ClientValidationFunction="ValidateMODE" Display="Dynamic"></asp:CustomValidator>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ALLOWS_EFT" Visible="False"></asp:BoundColumn>
								</Columns>
							</asp:datagrid></td>
					</tr>
					<tr>
						<td>
							<table width="100%">
								<tr>
									<td class="midcolorr" align="right">
									<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnConfirm" runat="server" Text="Confirm"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnConfirming" runat="server" Text="Confirming"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnConfirmPrint" runat="server" Text="Confirm &amp; Print"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnSaveConfirm" runat="server" Text="Save &amp; Confirm"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td><INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidPOLICY_APP_NUMBER" type="hidden" name="hidPOLICY_APP_NUMBER" runat="server">
							<INPUT id="hidCUSTOMER_ID_NAME" type="hidden" name="hidCUSTOMER_ID_NAME" runat="server">
							<INPUT id="hidAllowEFT" type="hidden" runat="server" NAME="hidAllowEFT">
						</td>
					</tr>
				</table>
			</form>
		</div>
	</body>
</HTML>

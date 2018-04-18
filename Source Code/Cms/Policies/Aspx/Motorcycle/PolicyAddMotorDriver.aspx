<%@ Page language="c#" Codebehind="PolicyAddMotorDriver.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Motorcycle.PolicyAddMotorDriver" ValidateRequest="false" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>Policy Driver Details</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		var varPolEffDate="<%=PolEffDate  %>";
		
		/* Commented by Charles on 2-Jul-09 for Itrack issue 6012
		// Added by Charles on 5-Jun-2009 for Itrack issue 5744
		//var varYearsWithWolverine = "<%-- intYearsWithWolverine --%>";
		//var varYearsContInsured = "<%-- intYearsContInsured --%>";
		//Added Till Here
		*/
		
		function SaveClientSide()
			{
				var intTotalRows = parseInt(document.getElementById("tblAssignedVeh").getAttribute("TotalRows"));
				var SelectedData="",driverType="";				
				if(document.getElementById('cmbDRIVER_DRIV_TYPE').options.selectedIndex!=-1)
					driverType =document.getElementById('cmbDRIVER_DRIV_TYPE').options[document.getElementById('cmbDRIVER_DRIV_TYPE').options.selectedIndex].value;
				
				if(driverType=="11941")//Fill data only when the driver type is Operates Cycle
				{
					if(document.getElementById("tbAssVehSec").style.display=="none")
					{
						document.getElementById("hidSeletedData").value="";
						//return;
					}				
					for(var i=1 ; i<=intTotalRows ; i++)
					{
						if (SelectedData == "")
							SelectedData = document.getElementById('ID_' + i).getAttribute("RowVehID") + "~" + document.getElementById('ID_' + i).getElementsByTagName("td")[3].childNodes[0].value;
						else
				
							SelectedData = SelectedData + "|" + document.getElementById('ID_' + i).getAttribute("RowVehID") + "~" + document.getElementById('ID_' + i).getElementsByTagName("td")[3].childNodes[0].value;
					}				
					document.getElementById("hidSeletedData").value=SelectedData;
				 }	
				 			 			 
				 CompareExpDateWithDOB();
				 //Itrack # 5881 Manoj Rathore					 
				 //setDriv_Lic_Rfv();
				 Page_ClientValidate();			
				 return Page_IsValid;
			}
			
		function SetDateValidator()
		{
			if(document.getElementById("cmbMVR_ORDERED").value == 10963) //Yes
			{
			//red star visible
				document.getElementById("spnDATE_ORDERED").style.visibility="visible";
			//req fld validator enable
				EnableValidator('rfvDATE_ORDERED',true);
			
			//text len>0 txt box back color =white else color = yellow
			if (document.getElementById("txtDATE_ORDERED").value=="")
				document.getElementById("txtDATE_ORDERED").style.backgroundColor="#FFFFD1";
			else
				document.getElementById("txtDATE_ORDERED").style.backgroundColor="white";
			
			}
			else
			{
			//red star visible=false
				document.getElementById("spnDATE_ORDERED").style.visibility="hidden";
			//req fld validator disable
				EnableValidator('rfvDATE_ORDERED',false);
			//text box back color =white
				document.getElementById("txtDATE_ORDERED").style.backgroundColor="white";
			
			}
			
		}
		
		function EnableDisableRiskDiscount()
		{
			var dtCurrentDate=new Date();	
			
			//Set the two dates
			//var millennium =new Date(2000, 0, 1) //Month is 0-11 in JavaScript
			today=new Date()
			//Get 1 day in milliseconds
			var one_day=1000*60*60*24

			var dtDOB=new Date(FormatDateForGrid(document.getElementById('txtDATE_LICENSED'),'##/##/####'));
			
			//Calculate difference btw the two dates, and convert to days
			var diff=Math.ceil((today.getTime()-dtDOB.getTime())/(one_day));
			diff = diff/366;					

			//var dtDOB=new Date(FormatDateForGrid(document.getElementById('txtDATE_LICENSED'),'##/##/####'));
			if(diff > '<%=strLicenceLimit%>' && document.getElementById('hidMVRPoints').value <= 2)
				document.getElementById('trRisk').style.display="inline";
			else
				document.getElementById('trRisk').style.display="none";
			HideShowDiscountHeader();
		}
		
		function ValidateDiscountType(source, arguments)
			{
				var cnt=0;
				if(document.getElementById('chkMATURE_DRIVER').checked || document.getElementById('chkPREFERRED_RISK').checked || document.getElementById('chkTRANSFEREXPERIENCE_RENEWALCREDIT').checked)
				{
					cnt=1;
				}
			
				if(cnt!=1)
				{
					arguments.IsValid = false;
					return;   
				}				
			
			}

		function SetTab()
		{
			if((document.getElementById('hidFormSaved').value == '1') || (document.getElementById('hidOldData').value != ""))
			{
				
				Url="PolicyAutoMVRIndex.aspx?CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value 
					+ "&POLICY_ID=" + document.getElementById("hidPolicyId").value 
					+ "&POLICY_VERSION_ID=" + document.getElementById("hidPolicyVersionId").value
					+ "&CALLED_FROM=" + document.getElementById("hidCalledFrom").value + "&";
					//+ "&DRIVER_ID=" + document.getElementById("hidDRIVER_ID").value + "&";
				
				DrawTab(2,top.frames[1],'MVR Information',Url);
			}
			else
			{	
				RemoveTab(3,top.frames[1]);						
				RemoveTab(2,top.frames[1]);
			}			
		}
		
		function CollegeStudent()
		{
			var Age = GetDriverAge();
			combo = document.getElementById("cmbDRIVER_DRIV_TYPE");
			if(Age < 25 && combo!=null && combo.selectedIndex !="-1" && combo.options[combo.selectedIndex].value=="<%=MOTOR_DRIVER_OPERATES_CYCLE.ToString()%>")
				ShowCollegeStudent(true);
			else
				ShowCollegeStudent(false);
			cmbCOLL_STUD_AWAY_HOME_Change();
		}
		function ShowCollegeStudent(flag)
		{
			if(flag)
			{
				document.getElementById("cmbCOLL_STUD_AWAY_HOME").style.display = "inline";
				document.getElementById("capCOLL_STUD_AWAY_HOME").style.display = "inline";
				document.getElementById("spnCOLL_STUD_AWAY_HOME").style.display = "inline";
				EnableValidator("rfvCOLL_STUD_AWAY_HOME",true);
			}
			else
			{
				document.getElementById("cmbCOLL_STUD_AWAY_HOME").style.display = "none";
				document.getElementById("capCOLL_STUD_AWAY_HOME").style.display = "none";
				document.getElementById("spnCOLL_STUD_AWAY_HOME").style.display = "none";
				document.getElementById("cmbCOLL_STUD_AWAY_HOME").selectedIndex = -1;
				EnableValidator("rfvCOLL_STUD_AWAY_HOME",false);
			}
		}
		function EnableValidator(ValidatorID,flag)
		{
			if(flag) //Enable the validator
			{
				document.getElementById(ValidatorID).setAttribute('enabled',true);
				document.getElementById(ValidatorID).setAttribute('isValid',true);
			}
			else
			{
				document.getElementById(ValidatorID).setAttribute('enabled',false);
				document.getElementById(ValidatorID).style.display = "none";
			}
		}
		function GetDriverAge()
		{
			if(document.getElementById('txtDRIVER_DOB')=='')
				return -1;
			var dtCurrentDate=new Date();	
			//itrack 5744			
			//var today=new Date()
			var today=varPolEffDate;
			//Get 1 day in milliseconds
			//var one_day=1000*60*60*24
			var dtDOB=new Date(FormatDateForGrid(document.getElementById('txtDRIVER_DOB'),'##/##/####'));
			//var diff=Math.ceil((today.getTime()-dtDOB.getTime())/(one_day));
			//diff = diff/366;			
			var diff = DateDiffernce(dtDOB,today);
			return diff;
		}
		function cmbCOLL_STUD_AWAY_HOME_Change()
		{
			combo = document.getElementById("cmbCOLL_STUD_AWAY_HOME");			
			if(combo!=null && combo.style.display=="inline" && combo.selectedIndex!="-1" && combo.options[combo.selectedIndex].value=="1")
			{
				document.getElementById("cmbCYCL_WITH_YOU").style.display = "inline";
				document.getElementById("capCYCL_WITH_YOU").style.display = "inline";
				document.getElementById("spnCYCL_WITH_YOU").style.display = "inline";
				EnableValidator("rfvCYCL_WITH_YOU",true);
			}
			else
			{
				document.getElementById("cmbCYCL_WITH_YOU").style.display = "none";
				document.getElementById("capCYCL_WITH_YOU").style.display = "none";
				document.getElementById("spnCYCL_WITH_YOU").style.display = "none";
				document.getElementById("cmbCYCL_WITH_YOU").selectedIndex = -1;
				EnableValidator("rfvCYCL_WITH_YOU",false);
			}
			if(combo.style.display=="none" && document.getElementById("cmbCYCL_WITH_YOU").style.display == "none")
				document.getElementById("trCollegetStudent").style.display = "none";
			else
				document.getElementById("trCollegetStudent").style.display = "inline";
		}
			
		function AddData()
		{
			document.getElementById('txtDRIVER_FNAME').value  = '';
			//document.getElementById('txtDRIVER_MNAME').value  = '';
			//document.getElementById('txtDRIVER_LNAME').value  = '';
			document.getElementById('txtDRIVER_CODE').value  = '';
			document.getElementById('txtDRIVER_SUFFIX').value  = '';
			document.getElementById('txtDRIVER_ADD1').value  = '';
			document.getElementById('txtDRIVER_ADD2').value  = '';
			document.getElementById('txtDRIVER_CITY').value  = '';
			document.getElementById('cmbDRIVER_STATE').options.selectedIndex = -1;
			document.getElementById('txtDRIVER_ZIP').value  = '';
			document.getElementById('cmbDRIVER_DRIV_TYPE').options.selectedIndex = -1;
			document.getElementById('cmbCYCL_WITH_YOU').options.selectedIndex = -1;
			document.getElementById('cmbCOLL_STUD_AWAY_HOME').options.selectedIndex = -1;
			var varSysID = "<%=GetSystemId()%>";
			//alert(varSysID);
			if (varSysID == 'S001' || varSysID == 'SUAT')	
            {	     
			    document.getElementById('cmbDRIVER_COUNTRY').options.selectedIndex = 2;
            }
            else
            {
                document.getElementById('cmbDRIVER_COUNTRY').options.selectedIndex = 0;
            }
			document.getElementById('txtDRIVER_HOME_PHONE').value  = '';
			document.getElementById('txtDRIVER_BUSINESS_PHONE').value  = '';
			document.getElementById('txtDRIVER_EXT').value  = '';
			//document.getElementById('txtDRIVER_FAX').value  = '';
			document.getElementById('txtDRIVER_MOBILE').value  = '';
			document.getElementById('txtDRIVER_DOB').value  = '';
			document.getElementById('txtDRIVER_SSN').value  = '';
			document.getElementById('cmbDRIVER_MART_STAT').options.selectedIndex = -1;
			document.getElementById('cmbDRIVER_SEX').options.selectedIndex = -1;
			document.getElementById('txtDRIVER_DRIV_LIC').value  = '';
			document.getElementById('cmbDRIVER_LIC_STATE').options.selectedIndex = -1;
			//document.getElementById('txtDRIVER_LIC_CLASS').value  = '';
		    document.getElementById('txtDATE_LICENSED').value  = '';
			document.getElementById('cmbRELATIONSHIP').options.selectedIndex = -1;
			//document.getElementById('cmbDRIVER_OCC_CODE').options.selectedIndex = -1;
			document.getElementById('cmbDRIVER_OCC_CLASS').options.selectedIndex = -1;			
			if(document.getElementById('btnActivateDeactivate'))
			document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			document.getElementById('hidDRIVER_ID').value	=	'New';
			document.getElementById('cmbVEHICLE_ID').options.selectedIndex = -1;	
			if(document.getElementById('cmbNO_CYCLE_ENDMT'))
				document.getElementById('cmbNO_CYCLE_ENDMT').options.selectedIndex = 2;				
			document.getElementById('cmbDRIVER_DRINK_VIOLATION').options.selectedIndex = 1;	
			document.getElementById('cmbVIOLATIONS').options.selectedIndex = -1;	
			document.getElementById('cmbMVR_ORDERED').options.selectedIndex = -1;	
			document.getElementById('txtDATE_ORDERED').value  = '';
			ChangeColor();
			DisableValidators();
			SSN_change();
			document.getElementById('txtDRIVER_FNAME').focus();
		}
		
		function populateXML()
		{
			
//			if(document.getElementById('hidDRIVER_DRIV_LIC_SELINDEX').value != '0')	
//			{		
		//			   document.getElementById('cmbDRIVER_LIC_STATE').selectedIndex = document.getElementById('hidDRIVER_DRIV_LIC_SELINDEX').value;
//				document.getElementById('cmbDRIVER_LIC_STATE').focus();
//				document.getElementById('hidDRIVER_DRIV_LIC_SELINDEX').value = '0';
//				return;
//			}
			setDriv_Lic_regexp(1);
			if(document.getElementById('hidFormSaved').value == '0')
			{
				if(document.getElementById('hidOldData').value != "")
				{
					
					//Enabling the activate deactivate button
					if(document.getElementById('btnActivateDeactivate'))
						document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
					
					//Populating the controls values
					populateFormData(document.getElementById('hidOldData').value,APP_DRIVER_DETAILS);
					populateInfo();
					SSN_hide();
				}
				else
				{	
					if(document.getElementById('btnDelete')!=null)
						document.getElementById('btnDelete').setAttribute('disabled',true); 
					AddData();
				}		
			}
			
			//Added by Sibin on 19 Dec 08 to Encrypt/Decrypt Recipient Identification 
			else
			{
				if(document.getElementById('hidSSN_NO').value != '')
					SSN_hide();
				else
					SSN_change();					
			
			}
			//Added till here
			///
			/* commented by pravesh
					if (document.getElementById('cmbMATURE_DRIVER').options[document.getElementById('cmbMATURE_DRIVER').selectedIndex].value == "1")
						document.getElementById('capMatureDriver').style.display="inline";
					else
						document.getElementById('capMatureDriver').style.display="none";
					
					/*if (document.getElementById('cmbPREFERRED_RISK').options[document.getElementById('cmbPREFERRED_RISK').selectedIndex].value == "1")
						document.getElementById('capPreferedRisk').style.display="inline"
					else
						document.getElementById('capPreferedRisk').style.display="none"
				*/	
				
				/*Commented by Charles on 2-Jul-09 for Itrack issue 6012
					if (document.getElementById('cmbTRANSFEREXPERIENCE_RENEWALCREDIT').options[document.getElementById('cmbTRANSFEREXPERIENCE_RENEWALCREDIT').selectedIndex].value == "1")
						document.getElementById('capTransferExperienceRenewalCredit').style.display="inline"
					else
						document.getElementById('capTransferExperienceRenewalCredit').style.display="none"
				*/
			
			//end here  
			//Making tabs
			//SetTab();
			CheckForDriverType(1);
			setDriv_Lic_regexp(1);
			return false;
		}
		//added by pravesh
		function EnableDisableMatureDiscount()
		{
			/*var dtCurrentDate=new Date();				
			today=new Date()
			//Get 1 day in milliseconds
			var one_day=1000*60*60*24
			var dtDOB=new Date(FormatDateForGrid(document.getElementById('txtDRIVER_DOB'),'##/##/####'));
			
			var diff=Math.ceil((today.getTime()-dtDOB.getTime())/(one_day));
			diff = diff/366;			*/
			var Age = GetDriverAge();
			
			if(Age >= 45)												
				{document.getElementById('trMatureDriver').style.display="inline";				
				}
			else
				{ document.getElementById('trMatureDriver').style.display="none";				
				}
			
			HideShowDiscountHeader();
			
			
			/*	if (document.getElementById('cmbPREFERRED_RISK').options[document.getElementById('cmbPREFERRED_RISK').selectedIndex].value == "1")
						document.getElementById('capPreferedRisk').style.display="inline"
				else
						document.getElementById('capPreferedRisk').style.display="none"*/			
		}
		
		/* Commented by Charles on 2-Jul-09 for Itrack issue 6012
		//Method added by Charles on 5-Jun-2009 for Itrack issue 5744
		function EnableDisableTransferExperienceRenewalCredit()
		{
			if(document.getElementById('cmbDRIVER_LIC_STATE').value=='14')// Indiana
			{
				var dtLicensed=new Date(FormatDateForGrid(document.getElementById('txtDATE_LICENSED'),'##/##/####'));
				var diff = DateDiffernce(dtLicensed,varPolEffDate);
			
				if((varYearsContInsured>=1 || varYearsWithWolverine>=1) && diff>=1)						
				 document.getElementById('trTRANSFEREXPERIENCE_RENEWALCREDIT').style.display="inline";
				 else				 			
				  document.getElementById('trTRANSFEREXPERIENCE_RENEWALCREDIT').style.display="none";			
			}
			else if(document.getElementById('cmbDRIVER_LIC_STATE').value=='22')//Michigan
			{
				if(varYearsContInsured>=1 || varYearsWithWolverine>=1)
				 document.getElementById('trTRANSFEREXPERIENCE_RENEWALCREDIT').style.display="inline";
				else				 			
				  document.getElementById('trTRANSFEREXPERIENCE_RENEWALCREDIT').style.display="none";
			}
			else
				document.getElementById('trTRANSFEREXPERIENCE_RENEWALCREDIT').style.display="none";
				
				//Added by Charles on 24-Jun-2009 for Itrack 6003
				ShowDiscountPercentage();
				HideShowDiscountHeader();
		}
		*/
		
		function HideShowDiscountHeader()
		{
		//trTRANSFEREXPERIENCE_RENEWALCREDIT check removed by Charles on 2-Jul-09 for Itrack issue 6012
			if(document.getElementById('trRisk').style.display=="none" && document.getElementById('trMatureDriver').style.display=="none" )//&& document.getElementById('trTRANSFEREXPERIENCE_RENEWALCREDIT').style.display=="none" )
				document.getElementById('trDriverDiscount').style.display="none";
			else
				document.getElementById('trDriverDiscount').style.display="inline";
		}		
		function FillDriverState()
		{			
		}
		function SetRegisteredState()
		{
			//SelectComboOption("cmbDRIVER_LIC_STATE", document.getElementById("cmbDRIVER_STATE").value);
			ChangeColor();
			DisableValidators();	
			return false;

		}
		function ChkCurrDate()
		{
			
		}		
		function ChkDOB(objSource , objArgs)
		{
			var expdate=document.APP_DRIVER_DETAILS.txtDRIVER_DOB.value;
			var dt=new Date();
			dt='<%=System.DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy")%>';
			//dt='<%=System.DateTime.Now.AddDays(-1).ToString("dd/mm/yyyy")%>';
			objArgs.IsValid = DateComparer(dt,expdate,jsaAppDtFormat);			
		}			
		function ChkEXPDate(objSource , objArgs)
		{
			var expdate=document.APP_DRIVER_DETAILS.txtDATE_LICENSED.value;
			objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);			
		}
				
		function GenerateDriverCode(Ctrl)
		{						
			//if (document.getElementById('hidDRIVER_ID').value == "New")
			//{				
				  document.getElementById('txtDRIVER_CODE').value=(GenerateRandomCode(document.getElementById('txtDRIVER_FNAME').value,''));
			  //}
		}
		function FillCustomerName()
		{	
			var objXmlHandler = new XMLHandler();
			var tree = (objXmlHandler.quickParseXML(document.getElementById('hidCUSTOMER_INFO').value).getElementsByTagName('Table')[0]);
			var i=0;
			for(i=0;i<tree.childNodes.length;i++)
			{						
				var nodeName = tree.childNodes[i].nodeName;
				var nodeValue ;
				if ( tree.childNodes[i].firstChild == 'undefined' || tree.childNodes[i].firstChild == null )
				{
					nodeValue = '';
				}
				else
				{		
					//Added by Sibin for Itrack Issue 5329 on 12 Feb 09- TO MOVE TO LOCAL VSS
					tree.childNodes[i].firstChild.text = tree.childNodes[i].firstChild.text.replace("&amp;","&");			
					nodeValue = tree.childNodes[i].firstChild.text;
				}				
				switch(nodeName)
				{
				case "CUSTOMER_FIRST_NAME":
					document.getElementById('txtDRIVER_FNAME').value = nodeValue;
					GenerateDriverCode('txtDRIVER_FNAME');	
					break;
				case "CUSTOMER_MIDDLE_NAME":
					 document.getElementById('txtDRIVER_MNAME').value = nodeValue;
					 break;
				case "CUSTOMER_LAST_NAME":	
					 document.getElementById('txtDRIVER_LNAME').value = nodeValue;
					 GenerateDriverCode('txtDRIVER_LNAME');					
					 break;	
				case "CUSTOMER_ADDRESS1":
					 document.getElementById('txtDRIVER_ADD1').value = nodeValue;
					 break;
				case "CUSTOMER_ADDRESS2":
					  document.getElementById('txtDRIVER_ADD2').value = nodeValue;
					 break;		
				case "CUSTOMER_CITY":
					  document.getElementById('txtDRIVER_CITY').value = nodeValue;
					  break;							
				case "CUSTOMER_COUNTRY":
					  SelectComboOption("cmbDRIVER_COUNTRY",nodeValue)
					  break;
				case "CUSTOMER_STATE":
					  SelectComboOption("cmbDRIVER_STATE",nodeValue)
					  SetRegisteredState();
					  break;
				case "CUSTOMER_ZIP":
					   document.getElementById('txtDRIVER_ZIP').value = nodeValue;
					   break;
				case "CUSTOMER_EXT":
						document.getElementById('txtDRIVER_EXT').value = nodeValue;
					    break;				
				case "CUSTOMER_HOME_PHONE":
					   document.getElementById('txtDRIVER_HOME_PHONE').value = nodeValue;
					   break;						
				case "CUSTOMER_MOBILE":
					  document.getElementById('txtDRIVER_MOBILE').value = nodeValue;
					  break;
				case "DATE_OF_BIRTH":
					  document.getElementById('txtDRIVER_DOB').value = nodeValue;
					  break;	
			
				case "MARITAL_STATUS":
					  document.getElementById('cmbDRIVER_MART_STAT').value = nodeValue;
					  break;
					  	
				case "VIOLATIONS":
					  SelectComboOption("cmbVIOLATIONS",nodeValue)
					  break;
				case "MVR_ORDERED":
					  SelectComboOption("cmbMVR_ORDERED",nodeValue)
					  break;
				case "DATE_ORDERED":
					  document.getElementById('txtDATE_ORDERED').value = nodeValue;
					  break;
				 case "SSN_NO":
					  document.getElementById('hidSSN_NO').value = nodeValue;
					  break;				
				case "DECRYPT_SSN_NO":
					  document.getElementById('capSSN_NO_HID').innerText = nodeValue;
					  //Added for Itrack Issue 6165 on 27 July 2009
					  document.getElementById("txtDRIVER_SSN").style.display = 'none';
					  document.getElementById("btnSSN_NO").style.display = 'inline';
					  document.getElementById("btnSSN_NO").value = 'Edit';
					  break;	
					  
				//--------------------Added for Itrack Issue 6165 on 27 July 2009---------.
				case "GENDER":
					 SelectComboOption("cmbDRIVER_SEX",nodeValue)
					 break;
				case "CUSTOMER_SUFFIX":
					  document.getElementById('txtDRIVER_SUFFIX').value = nodeValue;
					  break;
				case "OCCUPATION":
					  SelectComboOption("cmbDRIVER_OCC_CLASS",nodeValue)
					  break;
				//-------------------------------End--------------------------------------.
				 }	
			}		
				ChangeColor();
				DisableValidators();	
				GenerateDriverCode('txtDRIVER_LNAME');
				return false;
							
			}	
		function CoypApplicationDrivers()
		{			
			var customerid = '';
			var appid='';
			var appversionid='';	 fdate_lic
			
			if (document.getElementById('hidCUSTOMER_ID')!=null)
			{				
				customerid = document.getElementById('hidCUSTOMER_ID').value;
			}
			if (document.getElementById('hidAppId')!=null)
			{
				appid = document.getElementById('hidAppId').value;
			}
			if (document.getElementById('hidAppVersionId')!=null)
			{
				appversionid = document.getElementById('hidAppVersionId').value;
			}
			
			var clFrom='<%=calledFrom%>'
			window.open('AddCurrentAppExistingDriver.aspx?calledFrom='+ clFrom +'&CUSTOMER_ID='+customerid+'&APP_ID='+appid+'&APP_VERSION_ID='+appversionid,'DriverDetails',600,300,'Yes','Yes','No','No','No');	
			return false;
		}
		function CompareExpDateWithDOB()
		{
			document.getElementById("txtDATE_LICENSED").value = FormatDateForGrid(document.getElementById("txtDATE_LICENSED"),'');
			document.getElementById("txtDRIVER_DOB").value = FormatDateForGrid(document.getElementById("txtDRIVER_DOB"),'');
			var dob=document.getElementById("txtDRIVER_DOB").value;
			var expdate=document.getElementById("txtDATE_LICENSED").value;
			var Result = true;
			if (dob != "")
			{
				if(expdate=="")
					Result=true;
				else
					Result = CompareTwoDate(expdate,dob,jsaAppDtFormat);
			}
			else
			{
				Result = true;
			}			
			if(Result)
				document.getElementById("spnDATE_EXP_DOB").style.display="none";
			else
				document.getElementById("spnDATE_EXP_DOB").style.display="inline";
						
			Page_IsValid = (Page_IsValid && Result);			
		}	
		/*function CompareExpDateWithDOB(objSource , objArgs)
		{
		var dob=document.APP_DRIVER_DETAILS.txtDRIVER_DOB.value;
		var expdate=document.APP_DRIVER_DETAILS.txtDATE_LICENSED.value;
		if (dob != "")
		{
			objArgs.IsValid = CompareTwoDate(expdate,dob,jsaAppDtFormat);
		}	
		
		}*/	
		
		function CompareTwoDate(DateFirst, DateSec, FormatOfComparision)
		{
			var saperator = '/';
			var firstDate, secDate;
			var strDateFirst = DateFirst.split("/");
			var strDateSec = DateSec.split("/");
			if(FormatOfComparision.toLowerCase() == "dd/mm/yyyy")
			{			
				firstDate = (strDateFirst[1].length != 2 ? '0' + strDateFirst[1] : strDateFirst[1]) + '/' + (strDateFirst[0].length != 2 ? '0' + strDateFirst[0] : strDateFirst[0])  + '/' + (strDateFirst[2].length != 4 ? '0' + strDateFirst[2] : strDateFirst[2]);
				secDate = (strDateSec[1].length != 2 ? '0' + strDateSec[1] : strDateSec[1]) + '/' + (strDateSec[0].length != 2 ? '0' + strDateSec[0] : strDateSec[0])  + '/' + (strDateSec[2].length != 4 ? '0' + strDateSec[2] : strDateSec[2]);
			}
			if(FormatOfComparision.toLowerCase() == "mm/dd/yyyy")
			{				
				firstDate = DateFirst
				secDate = DateSec;
			}
			firstDate = new Date(firstDate);
			secDate = new Date(secDate);
			firstSpan = Date.parse(firstDate);
			secSpan = Date.parse(secDate);
			if(firstSpan > secSpan) 
				return true;	// first is greater
			else 
				return false;	// secound is greater
		}
		function DefaultMaritalStatus()
		{
			if(document.getElementById('hidFormSaved').value == '0' && document.getElementById('hidOldData').value == "")
			{	
				//Changed Selected Index to Single on 19-Jun-2009 for Itrack 5993 --Charles
				document.getElementById('cmbDRIVER_MART_STAT').options.selectedIndex = 4;
			}
			
		}			
		function CheckIfPhoneEmpty()
		{				
			if(document.getElementById('txtDRIVER_BUSINESS_PHONE').value == "")
			{
				document.getElementById('txtDRIVER_EXT').value = ""
				return false;
			}
			else
				return true;
		}	
		function ClearText(e)
		{
			event.returnValue=false;
			return false;			
		}
		function showPageLookupLayer(controlId)
			{				
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbRELATIONSHIP":
						lookupMessage	=	"DRACD.";
						break;											
					//case "cmbDRIVER_OCC_CODE":
					//	lookupMessage	=	"%OCC.";
					//	break;							
					case "cmbDRIVER_OCC_CLASS":
						lookupMessage	=	"OCCCL.";
						break;													
					case "cmbDRIVER_MART_STAT":
						lookupMessage	=	"MARST.";
						break;							
					default:
						lookupMessage	=	"Look up code not found";
						break;
						
				}
				showLookupLayer(controlId,lookupMessage);							
			}	
			
		function Redirect()
		{
			top.frames[1].location='../PolicyVehicleIndex.aspx?CalledFrom=MOT&';
		}
		
		function ShowPercentage(objCheckbox,objLabel)
		{
			 if(objCheckbox.checked)	
				objLabel.style.display="inline";
			else
				objLabel.style.display="none";		
			
		}
		function ResetForm1(obj)
		{
			ResetForm(obj);
			ShowDiscountPercentage();
			CheckForDriverType(1);
			return false;
		}
		function ChkDateOfOrder(objSource , objArgs)
		{
			var expdate=document.APP_DRIVER_DETAILS.txtDATE_ORDERED.value;
			objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);
		}	
		function ShowDiscountPercentage()
		{
		//alert('hi');
		// As per ITrack 5891
			
			/* Commented by Charles on 2-Jul-09 for Itrack issue 6012
			//Uncommented if-else condition on 23-Jun-2009 for Itrack 6003 --Charles
			if (document.getElementById("cmbTRANSFEREXPERIENCE_RENEWALCREDIT").options[document.getElementById("cmbTRANSFEREXPERIENCE_RENEWALCREDIT").selectedIndex].value == "1")
				document.getElementById("capTransferExperienceRenewalCredit").style.display="inline";
			else
				document.getElementById("capTransferExperienceRenewalCredit").style.display="none";	
			*/
				
			//if (document.getElementById("cmbMATURE_DRIVER").options[document.getElementById("cmbMATURE_DRIVER").selectedIndex].value == "1")
				document.getElementById("capMatureDriver").style.display="inline";
			//else
			//	document.getElementById("capMatureDriver").style.display="none";	
		}
		/*function ShowDiscountPercentage(objDropDownListID, objLabelID)
		{		
			if (document.getElementById(objDropDownListID).options[document.getElementById(objDropDownListID).selectedIndex].value == "1")
				document.getElementById(objLabelID).style.display="inline";
			else
				document.getElementById(objLabelID).style.display="none";	
		}*/
		function populateAdditionalInfo(NodeName)
		{
			var tempXML=parent.strSelectedRecordXML;		
			var objXmlHandler = new XMLHandler();
			var unqID;
			if(tempXML==null || tempXML=="")
				return;
			var tree = (objXmlHandler.quickParseXML(tempXML).getElementsByTagName('Table')[0]);					
			
			if(tree)
			{				
				for(i=0;i<tree.childNodes.length;i++)
				{
					if(tree.childNodes[i].nodeName==NodeName)
					{
						if(tree.childNodes[i].firstChild)
							unqID=tree.childNodes[i].firstChild.text;
					}					
				}
			}
			return unqID;
		}
		function populateInfo()
		{
			if (this.parent.strSelectedRecordXML == "-1")
			{
				setTimeout('populateInfo();',100);
				return;
			}
			document.getElementById('hidDataValue1').value =populateAdditionalInfo("DRIVER");
			document.getElementById('hidDataValue2').value =populateAdditionalInfo("DRIVER_CODE");					
			if(document.getElementById('hidDataValue1').value=='undefined')
				document.getElementById('hidDataValue1').value="";
			if(document.getElementById('hidDataValue2').value=='undefined')
				document.getElementById('hidDataValue2').value="";
			document.getElementById('hidCustomInfo').value=";Driver Name = " + document.getElementById('hidDataValue1').value + ";Driver Code = " + document.getElementById('hidDataValue2').value;						
			
		}
		function ChkTextAreaLength(source, arguments)
		{
			var txtArea = arguments.Value;
			if(txtArea.length > 250 ) 
			{
				arguments.IsValid = false;
				return;   // invalid userName
			}
		}

		function setDriv_Lic_regexp(calledFrom)
		{
			document.getElementById('hidDRIVER_DRIV_LIC').value = document.getElementById('cmbDRIVER_LIC_STATE').value;
			document.getElementById('hidDRIVER_DRIV_LIC_SELINDEX').value = document.getElementById('cmbDRIVER_LIC_STATE').selectedIndex;
			switch(document.getElementById('cmbDRIVER_LIC_STATE').value)
			{
				case '14': document.getElementById('revDRIVER_DRIV_LIC').validationexpression = "^[0-9]{10}$";
							document.getElementById('revDRIVER_DRIV_LIC').innerHTML = "Please enter 10 digits all numeric.";
							document.getElementById('revDRIVER_DRIV_LIC').enabled = true;
							break;
				case '22': document.getElementById('revDRIVER_DRIV_LIC').validationexpression = "^[a-zA-Z]{1}[0-9]{12}$";
							document.getElementById('revDRIVER_DRIV_LIC').innerHTML = "Please enter 13 digits (first one is Alpha and the last 12 are numeric).";
							document.getElementById('revDRIVER_DRIV_LIC').enabled = true;
							break;
				case '49': document.getElementById('revDRIVER_DRIV_LIC').validationexpression = "^[a-zA-Z]{1}[0-9]{13}$";
							document.getElementById('revDRIVER_DRIV_LIC').innerHTML = "Please enter 14 digits (first one is Alpha and the last 13 are numeric).";
							document.getElementById('revDRIVER_DRIV_LIC').enabled = true;
							break;
				default: document.getElementById('revDRIVER_DRIV_LIC').validationexpression = "^[0-9]{10}$";
							document.getElementById('revDRIVER_DRIV_LIC').innerHTML = "Please enter 10 digits all numeric.";
							document.getElementById('revDRIVER_DRIV_LIC').enabled = false;
							break;
			}
			if(calledFrom == '1')
				document.getElementById('revDRIVER_DRIV_LIC').style.display = 'none';
			//else
			//	document.getElementById('revDRIVER_DRIV_LIC').style.display = 'inline';
			//alert(document.getElementById('revDRIVER_DRIV_LIC').validationexpression);
		}
		 function SSN_change()
			{
				document.getElementById('txtDRIVER_SSN').value = '';
				document.getElementById('txtDRIVER_SSN').style.display = 'inline';
				document.getElementById("revDRIVER_SSN").setAttribute('enabled',true);
				if(document.getElementById("btnSSN_NO").value == 'Edit')
					document.getElementById("btnSSN_NO").value = 'Cancel';

				else if(document.getElementById("btnSSN_NO").value == 'Cancel')
					SSN_hide();
					
				else
					document.getElementById("btnSSN_NO").style.display = 'none';
			}
			
			function SSN_hide()
			{
				document.getElementById("btnSSN_NO").style.display = 'inline';
				//document.getElementById('txtSSN_NO').value = document.getElementById('txtSSN_NO_HID').value;
				document.getElementById('txtDRIVER_SSN').value = '';
				document.getElementById('txtDRIVER_SSN').style.display = 'none';
				document.getElementById("revDRIVER_SSN").style.display='none';
				document.getElementById("revDRIVER_SSN").setAttribute('enabled',false);
				document.getElementById("btnSSN_NO").value = 'Edit';
			}
			
           
		   function setDriv_Lic_Rfv()
			{

              var systemID = "<%=GetSystemId()%>";

			  if(document.getElementById('cmbDRIVER_DRIV_TYPE').options.selectedIndex!='-1')
			  {
			      if (document.getElementById('cmbDRIVER_DRIV_TYPE').options[document.getElementById('cmbDRIVER_DRIV_TYPE').selectedIndex].value == '11942')//Does not Operate
			      {
			          EnableValidator("rfvDRIVER_DRIV_LIC", false);
			          document.getElementById('rfvDRIVER_DRIV_LIC').style.display = "none";
			          document.getElementById('spnDRIVER_DRIV_LIC').style.display = "none";
			      }

			      else if (document.getElementById('cmbDRIVER_DRIV_TYPE').options[document.getElementById('cmbDRIVER_DRIV_TYPE').selectedIndex].value == '11941')//Operates cycle
			      {
			          if (document.getElementById('txtDRIVER_DRIV_LIC').value != "") {
			              EnableValidator("rfvDRIVER_DRIV_LIC", false);
			              document.getElementById('rfvDRIVER_DRIV_LIC').style.display = "none";
			              document.getElementById('spnDRIVER_DRIV_LIC').style.display = "inline";
			          }

			          else {
			              EnableValidator("rfvDRIVER_DRIV_LIC", true);
			              document.getElementById('rfvDRIVER_DRIV_LIC').style.display = "inline";
			              document.getElementById('spnDRIVER_DRIV_LIC').style.display = "inline";
			          }
			      }
			      else 
                  {
			          //Blank option
			          if (document.getElementById('txtDRIVER_DRIV_LIC').value != "") {
			              EnableValidator("rfvDRIVER_DRIV_LIC", false);
			              document.getElementById('rfvDRIVER_DRIV_LIC').style.display = "none";
			              document.getElementById('spnDRIVER_DRIV_LIC').style.display = "inline";
			          }

			          else {
			              EnableValidator("rfvDRIVER_DRIV_LIC", true);
			              document.getElementById('rfvDRIVER_DRIV_LIC').style.display = "inline";
			              document.getElementById('spnDRIVER_DRIV_LIC').style.display = "inline";

			              if (systemID.toUpperCase() == 'S001' || systemID.toUpperCase() == 'SUAT')
			              {
			                  EnableValidator("rfvDRIVER_DRIV_LIC", false);
			                  document.getElementById('rfvDRIVER_DRIV_LIC').style.display = "none";
			                  document.getElementById('spnDRIVER_DRIV_LIC').style.display = "none";
			              }
			          }

			      }
			  }
			 else
			 {
				EnableValidator("rfvDRIVER_DRIV_LIC",true);
				document.getElementById('rfvDRIVER_DRIV_LIC').style.display = "inline";
				document.getElementById('spnDRIVER_DRIV_LIC').style.display = "inline";

				if (systemID.toUpperCase() == 'S001' || systemID.toUpperCase() == 'SUAT')
                {
                    EnableValidator("rfvDRIVER_DRIV_LIC",false);
				    document.getElementById('rfvDRIVER_DRIV_LIC').style.display = "none";
				    document.getElementById('spnDRIVER_DRIV_LIC').style.display = "none";
                }
			 }
		 }
		 
		function CheckForDriverType(calledfor)
		{ 
			CollegeStudent();
			var SelectedValue;		 
			if (document.getElementById('cmbDRIVER_DRIV_TYPE').selectedIndex != '-1')	
			{
				SelectedValue=document.getElementById('cmbDRIVER_DRIV_TYPE').options[document.getElementById('cmbDRIVER_DRIV_TYPE').selectedIndex].value;		        
 
				if(SelectedValue == '11942' )  // Does not operate cycle
				{
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'),document.getElementById('rfvDRIVER_LIC_STATE'),document.getElementById('spnDRIVER_LIC_STATE'),false);			
					EnableDisableDescByDriver(document.getElementById('txtDRIVER_DRIV_LIC'),document.getElementById('rfvDRIVER_DRIV_LIC'),document.getElementById('spnDRIVER_DRIV_LIC'),false);			
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_DRINK_VIOLATION'),document.getElementById('rfvDRIVER_DRINK_VIOLATION'),document.getElementById('spnDRIVER_DRINK_VIOLATION'),false);			
					EnableDisableDescByDriver(document.getElementById('cmbVEHICLE_ID'),document.getElementById('rfvVEHICLE_ID'),document.getElementById('spnVEHICLE_ID'),false);			
					EnableDisableDescByDriver(document.getElementById('cmbVIOLATIONS'),document.getElementById('rfvVIOLATIONS'),document.getElementById('spnVIOLATIONS'),false);			
					EnableDisableDescByDriver(document.getElementById('cmbNO_CYCLE_ENDMT'),document.getElementById('rfvNO_CYCLE_ENDMT'),document.getElementById('spnNO_CYCLE_ENDMT'),false);			
					document.getElementById('tbAssVehSec').style.display="none";					
					EnableDisableDescByDriver(document.getElementById('txtDATE_LICENSED'),document.getElementById('rfvDATE_LICENSED'),document.getElementById('spnDATE_LICENSED'),false);
					
				}				
				else if( SelectedValue == '11941') {
				    var varSysID = "<%=GetSystemId()%>";
				    //alert(varSysID);
				    if (varSysID != 'S001' && varSysID != 'SUAT') {
				        EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'), document.getElementById('rfvDRIVER_LIC_STATE'), document.getElementById('spnDRIVER_LIC_STATE'), true);
				        EnableDisableDescByDriver(document.getElementById('cmbNO_CYCLE_ENDMT'), document.getElementById('rfvNO_CYCLE_ENDMT'), document.getElementById('spnNO_CYCLE_ENDMT'), true);
				    }
				    else {
				        EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'), document.getElementById('rfvDRIVER_LIC_STATE'), document.getElementById('spnDRIVER_LIC_STATE'), false);
				        EnableDisableDescByDriver(document.getElementById('cmbNO_CYCLE_ENDMT'), document.getElementById('rfvNO_CYCLE_ENDMT'), document.getElementById('spnNO_CYCLE_ENDMT'), false);
				        
				    }
				    EnableDisableDescByDriver(document.getElementById('txtDRIVER_DRIV_LIC'), document.getElementById('rfvDRIVER_DRIV_LIC'), document.getElementById('spnDRIVER_DRIV_LIC'), true);			
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_DRINK_VIOLATION'),document.getElementById('rfvDRIVER_DRINK_VIOLATION'),document.getElementById('spnDRIVER_DRINK_VIOLATION'),true);						
					document.getElementById('tbAssVehSec').style.display="inline";					
					EnableDisableDescByDriver(document.getElementById('txtDATE_LICENSED'),document.getElementById('rfvDATE_LICENSED'),document.getElementById('spnDATE_LICENSED'),true);
					EnableDisableDescByDriver(document.getElementById('cmbVIOLATIONS'),document.getElementById('rfvVIOLATIONS'),document.getElementById('spnVIOLATIONS'),true);			
					
				}
				else
				{
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'),document.getElementById('rfvDRIVER_LIC_STATE'),document.getElementById('spnDRIVER_LIC_STATE'),false);			
					EnableDisableDescByDriver(document.getElementById('txtDRIVER_DRIV_LIC'),document.getElementById('rfvDRIVER_DRIV_LIC'),document.getElementById('spnDRIVER_DRIV_LIC'),false);			
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_DRINK_VIOLATION'),document.getElementById('rfvDRIVER_DRINK_VIOLATION'),document.getElementById('spnDRIVER_DRINK_VIOLATION'),false);			
					document.getElementById('tbAssVehSec').style.display="none"	;				
					EnableDisableDescByDriver(document.getElementById('txtDATE_LICENSED'),document.getElementById('rfvDATE_LICENSED'),document.getElementById('spnDATE_LICENSED'),false);					
					EnableDisableDescByDriver(document.getElementById('cmbVIOLATIONS'),document.getElementById('rfvVIOLATIONS'),document.getElementById('spnVIOLATIONS'),false);			
					EnableDisableDescByDriver(document.getElementById('cmbNO_CYCLE_ENDMT'),document.getElementById('rfvNO_CYCLE_ENDMT'),document.getElementById('spnNO_CYCLE_ENDMT'),false);
				}
			}		 
			else
			{
				EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'),document.getElementById('rfvDRIVER_LIC_STATE'),document.getElementById('spnDRIVER_LIC_STATE'),false);			
				EnableDisableDescByDriver(document.getElementById('txtDRIVER_DRIV_LIC'),document.getElementById('rfvDRIVER_DRIV_LIC'),document.getElementById('spnDRIVER_DRIV_LIC'),false);			
				EnableDisableDescByDriver(document.getElementById('cmbDRIVER_DRINK_VIOLATION'),document.getElementById('rfvDRIVER_DRINK_VIOLATION'),document.getElementById('spnDRIVER_DRINK_VIOLATION'),false);			
				document.getElementById('tbAssVehSec').style.display="none";		
				EnableDisableDescByDriver(document.getElementById('txtDATE_LICENSED'),document.getElementById('rfvDATE_LICENSED'),document.getElementById('spnDATE_LICENSED'),false);
				EnableDisableDescByDriver(document.getElementById('cmbVIOLATIONS'),document.getElementById('rfvVIOLATIONS'),document.getElementById('spnVIOLATIONS'),false);			
				EnableDisableDescByDriver(document.getElementById('cmbNO_CYCLE_ENDMT'),document.getElementById('rfvNO_CYCLE_ENDMT'),document.getElementById('spnNO_CYCLE_ENDMT'),false);
			}
			HideShowAssignedDriverSection();
			ApplyColor();
			ChangeColor();
			return false;
		} 			
		function HideShowAssignedDriverSection()
		{
			
			if(document.getElementById('cmbDRIVER_DRIV_TYPE').selectedIndex==-1)
				return;			
			
			if(document.getElementById('cmbDRIVER_DRIV_TYPE').options[document.getElementById('cmbDRIVER_DRIV_TYPE').selectedIndex].value!='11941')
			{
			
				document.getElementById("tbAssVehSec").style.display="none";	
											
				return;
			}			
			else
				document.getElementById("tbAssVehSec").style.display="inline";	
							
		}
		function EnableDisableDescByDriver(txtDesc,rfvDesc,spnDesc,flag)
		{		
				
				if (flag==false)
				{			
					
					if(rfvDesc!=null)
					{
						rfvDesc.setAttribute('enabled',false);					
						rfvDesc.setAttribute('isValid',false);
						rfvDesc.style.display="none";
						spnDesc.style.display = "none";
						//ValidatorEnable(rfvDesc,false);
						txtDesc.className = "";
					}					
					//txtDesc.style.display = "none";
					//lblNA.style.display = "none";					
				}
				else
				{	
					if(rfvDesc!=null)
					{
						rfvDesc.setAttribute('enabled',true);					
						rfvDesc.setAttribute('isValid',true);
						//rfvDesc.style.display="inline";
						spnDesc.style.display = "inline";
						//ValidatorEnable(rfvDesc,true);							
						txtDesc.className = "MandatoryControl";
						//document.getElementById('capGoodStudent').style.display="inline"									
					}
					//txtDesc.style.display = "inline";
					//lblNA.style.display = "inline";										
				}	
				
		}	
		
		</script>
</HEAD>
<!--EnableDisableTransferExperienceRenewalCredit(); removed by Charles on 2-Jul-2009 for Itrack issue 6012 -->
	<BODY leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();ChangeColor();DefaultMaritalStatus();EnableDisableMatureDiscount();EnableDisableRiskDiscount();CollegeStudent();ShowDiscountPercentage();">
		<FORM id="APP_DRIVER_DETAILS" method="post" runat="server">
			<P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4">
						<asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label>
					</td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4">
										<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
										Please note that all fields marked with * are mandatory
									</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg"></asp:label></td>
								</tr>
								<%--<tr>
									<td colSpan="4">
										<table cellSpacing="0" cellPadding="0" width="100%">
											<tr>
												<TD class="midcolora"><asp:label id="capDRIVER_FNAME" runat="server">First NAme</asp:label><span class="mandatory">*</span>
												</TD>
												<TD class="midcolora"><asp:textbox id="txtDRIVER_FNAME" runat="server" maxlength="70" size="30"></asp:textbox><BR>
													<asp:requiredfieldvalidator id="rfvDRIVER_FNAME" runat="server" Display="Dynamic" ErrorMessage="DRIVER_FNAME can't be blank."
														ControlToValidate="txtDRIVER_FNAME"></asp:requiredfieldvalidator></TD>
												<TD class="midcolora"><asp:label id="capDRIVER_MNAME" runat="server">Middle Name</asp:label></TD>
												<TD class="midcolora"><asp:textbox id="txtDRIVER_MNAME" runat="server" maxlength="25" size="30"></asp:textbox></TD>
												<TD class="midcolora"><asp:label id="capDRIVER_LNAME" runat="server">Last Name</asp:label><span class="mandatory">*</span>
												</TD>
												<TD class="midcolora"><asp:textbox id="txtDRIVER_LNAME" runat="server" maxlength="70" size="30"></asp:textbox><BR>
													<asp:requiredfieldvalidator id="rfvDRIVER_LNAME" runat="server" Display="Dynamic" ErrorMessage="DRIVER_LNAME can't be blank."
														ControlToValidate="txtDRIVER_LNAME"></asp:requiredfieldvalidator></TD>
											</tr>
										</table>
									</td>
								</tr>--%>
                                <tr>									
									<TD class="midcolora" width="18%">
										<asp:label id="capDRIVER_FNAME" runat="server">Name</asp:label><span class="mandatory">*</span>
									</TD>
									<td class="midcolora" colspan="3" width="82%">
										<asp:textbox id="txtDRIVER_FNAME" runat="server" maxlength="70" size="58"></asp:textbox>
                                        <asp:label id="capDRIVER_MNAME" runat="server" Visible="false">Middle Name</asp:label>
                                        <asp:textbox id="txtDRIVER_MNAME" runat="server" Visible="false" maxlength="25" size="5"></asp:textbox>
                                        <asp:label id="capDRIVER_LNAME" runat="server"  Visible="false">Last Name</asp:label>
                                        <asp:textbox id="txtDRIVER_LNAME" runat="server"  Visible="false" maxlength="70" size="5"></asp:textbox>
													<asp:requiredfieldvalidator id="rfvDRIVER_LNAME" Enabled="false"  Visible="false" runat="server" Display="Dynamic" ErrorMessage="DRIVER_LNAME can't be blank."
														ControlToValidate="txtDRIVER_LNAME"></asp:requiredfieldvalidator>


                                        <BR>
													<asp:requiredfieldvalidator id="rfvDRIVER_FNAME" runat="server" Display="Dynamic" ErrorMessage="DRIVER_FNAME can't be blank."
														ControlToValidate="txtDRIVER_FNAME"></asp:requiredfieldvalidator>
									</td>
									
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_CODE" runat="server">Code</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_CODE" runat="server" maxlength="20" size="28"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_CODE" runat="server" Display="Dynamic" ErrorMessage="DRIVER_CODE can't be blank."
											ControlToValidate="txtDRIVER_CODE"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revDRIVER_CODE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtDRIVER_CODE"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_SUFFIX" runat="server">Suffix</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_SUFFIX" runat="server" maxlength="10" size="15"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_SEX" runat="server">Gender</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_SEX" onfocus="SelectComboIndex('cmbDRIVER_SEX')" runat="server">
											<asp:ListItem Value='M'>Male</asp:ListItem>
											<asp:ListItem Value='F'>Female</asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_SEX" runat="server" Display="Dynamic" ErrorMessage="DRIVER_SEX can't be blank."
											ControlToValidate="cmbDRIVER_SEX"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" colSpan="2"></td>
								</tr>
								<tr>
									<td class="midcolora"><asp:label id="Label1" runat="server">Would you like to pull customer address</asp:label></td>
									<td class="midcolora" colSpan="1">
										<cmsb:cmsbutton class="clsButton" id="btnPullCustomerAddress" runat="server" Text="Pull Customer Address"></cmsb:cmsbutton></td>
									<td class="midcolora" colSpan="2">
                                    <%--Button Disabled by Kuldeep because its not working properly for demo purpose on 12_jan_2012--%>
										<cmsb:cmsbutton class="clsButton" id="btnCopyDefaultCustomer" runat="server" Text="Copy Default Customer" style="display:none"></cmsb:cmsbutton>
									</td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_ADD1" runat="server">Address1</asp:label><%--<span class="mandatory">*</span>--%></TD> <%-- Span commented by Sibin on 28 Nov 08 for Itrack Issue 5061--%>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_ADD1" runat="server" size="30" maxlength="70"></asp:textbox><BR>
										<%-- Commented by Sibin for Itrack Issue 5061 on 28 Nov 08-->
										<!--<asp:requiredfieldvalidator id="rfvDRIVER_ADD1" runat="server" ControlToValidate="txtDRIVER_ADD1" ErrorMessage="Address can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator>--%></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_ADD2" runat="server">Address2</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_ADD2" runat="server" maxlength="70" size="30"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_CITY" runat="server">City</asp:label><%--<span class="mandatory">*</span>--%></TD> <%-- Span commented by Sibin on 28 Nov 08 for Itrack Issue 5061--%>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_CITY" runat="server" size="20" maxlength="35"></asp:textbox><br>
										<%-- Commented by Sibin for Itrack Issue 5061 on 28 Nov 08
										<!--<asp:requiredfieldvalidator id="rfvDRIVER_CITY" runat="server" ControlToValidate="txtDRIVER_CITY" ErrorMessage="City can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator>--%>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_COUNTRY" runat="server">Country</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_COUNTRY" onfocus="SelectComboIndex('cmbDRIVER_COUNTRY')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_STATE" runat="server">State</asp:label><span id="spnDRIVER_STATE" runat="server" class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_STATE" onfocus="SelectComboIndex('cmbDRIVER_STATE')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_STATE" runat="server" Display="Dynamic" ErrorMessage="DRIVER_STATE can't be blank."
											ControlToValidate="cmbDRIVER_STATE"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_ZIP" runat="server">Zip</asp:label><span id="spnDRIVER_ZIP" runat="server" class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_ZIP" runat="server" maxlength="10" size="12"></asp:textbox>
										<%-- Added by Swarup on 05-apr-2007 --%>
										<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
											<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
										</asp:hyperlink><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_ZIP" runat="server" Display="Dynamic" ErrorMessage="ZIP can't be blank."
											ControlToValidate="txtDRIVER_ZIP"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revDRIVER_ZIP" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtDRIVER_ZIP"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_HOME_PHONE" runat="server">Home Phone</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_HOME_PHONE" runat="server" maxlength="15" size="17"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revDRIVER_HOME_PHONE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtDRIVER_HOME_PHONE"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_MOBILE" runat="server">Mobile Phone</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_MOBILE" runat="server" maxlength="15" size="17"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revDRIVER_MOBILE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtDRIVER_MOBILE"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_BUSINESS_PHONE" runat="server">Business Phone</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_BUSINESS_PHONE" runat="server" maxlength="15" size="17"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revDRIVER_BUSINESS_PHONE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtDRIVER_BUSINESS_PHONE"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_EXT" runat="server">Ext</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_EXT" runat="server" maxlength="4" size="6" onblur="CheckIfPhoneEmpty();"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revDRIVER_EXT" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtDRIVER_EXT"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<%--<TD class="midcolora" width="18%"><asp:label id="capDRIVER_FAX" runat="server">Fax</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_FAX" runat="server" maxlength="15" size="17"></asp:textbox>
									<BR>
									<asp:regularexpressionvalidator id="revDRIVER_FAX" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtDRIVER_FAX"></asp:regularexpressionvalidator></TD>--%>
									<TD class="midcolora" width="18%">
										<asp:label id="capDRIVER_DOB" runat="server">Date of Birth</asp:label><span class="mandatory">*</span>
									</TD>
									<TD class="midcolora" width="32%">
											<%--Removed onblur event for txtDriver_DOB on 23-April-2009 for Itrack issue 5744 --%>
										<asp:textbox id="txtDRIVER_DOB" runat="server" maxlength="10" size="12"></asp:textbox>
										<asp:hyperlink id="hlkDRIVER_DOB" runat="server" CssClass="HotSpot">
											<asp:image id="imgDRIVER_DOB" runat="server" ImageUrl="../../../cmsweb/Images/CalendarPicker.gif"></asp:image>
										</asp:hyperlink>
										<br>
										<asp:regularexpressionvalidator id="revDRIVER_DOB" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtDRIVER_DOB"></asp:regularexpressionvalidator>
										<asp:requiredfieldvalidator id="rfvDRIVER_DOB" runat="server" Display="Dynamic" ErrorMessage="Date of birth can't be blank."
											ControlToValidate="txtDRIVER_DOB"></asp:requiredfieldvalidator>
										<asp:customvalidator id="csvDRIVER_DOB" Display="Dynamic" ControlToValidate="txtDRIVER_DOB" Runat="server"
											ClientValidationFunction="ChkDOB"></asp:customvalidator>
									</TD>
									<td colspan="2" class="midcolora"></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_SSN" runat="server">Social Security Number</asp:label></TD>
									<TD class="midcolora" width="32%">
									<asp:label id="capSSN_NO_HID" runat="server" size="14" maxlength="11"></asp:label>
										<input class="clsButton" id="btnSSN_NO" text="Edit" onclick="SSN_change();" type="button"></input>
									<asp:textbox id="txtDRIVER_SSN" runat="server" maxlength="11" size="13"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revDRIVER_SSN" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtDRIVER_SSN"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_MART_STAT" runat="server">Marital Status</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_MART_STAT" onfocus="SelectComboIndex('cmbDRIVER_MART_STAT')" runat="server"></asp:dropdownlist>
										<a class="calcolora" href="javascript:showPageLookupLayer('cmbDRIVER_MART_STAT')"></a>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_DRIV_LIC" runat="server">License Number</asp:label><span id="spnDRIVER_DRIV_LIC" runat="server" class="mandatory">*</span></TD> <%-- Span added by Sibin on 28 Nov 08 for Itrack Issue 5061--%>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_DRIV_LIC" runat="server" size="30" maxlength="30"></asp:textbox><br>
										<%--Added rfvDRIVER_DRIV_LIC by Sibin for Itrack Issue 5061 on 28 Nov 08--%>
										<asp:requiredfieldvalidator id="rfvDRIVER_DRIV_LIC" runat="server" ControlToValidate="txtDRIVER_DRIV_LIC" Display="Dynamic"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revDRIVER_DRIV_LIC" runat="server" ControlToValidate="txtDRIVER_DRIV_LIC" ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_LIC_STATE" runat="server">License State</asp:label><span class="mandatory" id="spnDRIVER_LIC_STATE">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_LIC_STATE" onfocus="SelectComboIndex('cmbDRIVER_LIC_STATE')" onChange="setDriv_Lic_regexp(2);" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_LIC_STATE" runat="server" Display="Dynamic" ErrorMessage="DRIVER_LIC_STATE can't be blank."
											ControlToValidate="cmbDRIVER_LIC_STATE"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<%--<TD class="midcolora" width="18%"><asp:label id="capDRIVER_LIC_CLASS" runat="server">License Class</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_LIC_CLASS" runat="server" maxlength="5" size="7"></asp:textbox></TD>--%>
									<TD class="midcolora" width="18%"><asp:label id="capDATE_LICENSED" runat="server">Date Experience Started </asp:label><span class="mandatory" id="spnDATE_LICENSED">*</span></TD>
									<TD class="midcolora" width="32%">
										<asp:textbox id="txtDATE_LICENSED" runat="server" maxlength="10" size="12"></asp:textbox>
										<asp:hyperlink id="hlkDATE_LICENSED" runat="server" CssClass="HotSpot">
											<asp:Image runat="server" ID="imgDATE_EXP_START" ImageUrl="../../../cmsweb/Images/CalendarPicker.gif"></asp:Image>
										</asp:hyperlink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:regularexpressionvalidator id="revDATE_LICENSED" runat="server" Display="Dynamic" ControlToValidate="txtDATE_LICENSED"></asp:regularexpressionvalidator>
										<asp:customvalidator id="csvDATE_LICENSED" Display="Dynamic" ControlToValidate="txtDATE_LICENSED" Runat="server"
											ClientValidationFunction="ChkEXPDate"></asp:customvalidator>
										<%--<asp:customvalidator id="csvDATE_EXP_DOB" Display="Dynamic" ControlToValidate="txtDATE_LICENSED" ClientValidationFunction="CompareExpDateWithDOB"
										Runat="server"></asp:customvalidator>--%>
										<asp:RequiredFieldValidator ID="rfvDATE_LICENSED" Runat="server" Display="Dynamic" ErrorMessage="DATE_LICENSED can't be blank."
											ControlToValidate="txtDATE_LICENSED"></asp:RequiredFieldValidator>
										<span id="spnDATE_EXP_DOB" style="DISPLAY: none; COLOR: red" runat="server"></span>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_OCC_CLASS" runat="server">Occupation Class</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_OCC_CLASS" onfocus="SelectComboIndex('cmbDRIVER_OCC_CLASS')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist>
										<a id="ancDRIVER_OCC_CLASS" runat="server" class="calcolora" href="javascript:showPageLookupLayer('cmbDRIVER_OCC_CLASS')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
										<BR>
									</TD>
								</tr>
								<%--<tr>
								<TD class="midcolora" width="18%"><asp:label id="capDRIVER_OCC_CODE" runat="server">Occupation Code  </asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_OCC_CODE" onfocus="SelectComboIndex('cmbDRIVER_OCC_CODE')" runat="server">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist>
									<a class="calcolora" href="javascript:showPageLookupLayer('cmbDRIVER_OCC_CODE')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
									<BR>
								</TD>								
								<td colspan="2" class="midcolora"></td>
							</tr>--%>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capRELATIONSHIP" runat="server">Relation</asp:label><%--<span class="mandatory">*</span>--%></TD> <%-- Span commented by Sibin on 28 Nov 08 for Itrack Issue 5061--%>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbRELATIONSHIP" onfocus="SelectComboIndex('cmbRELATIONSHIP')" runat="server">
											<asp:ListItem Value='0'></asp:ListItem>
										</asp:dropdownlist><A id="ancRELATIONSHIP" runat="server" class="calcolora" href="javascript:showPageLookupLayer('cmbRELATIONSHIP')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
										<BR>
										<%-- Commented by Sibin for Itrack Issue 5061 on 28 Nov 08
										<!--<asp:requiredfieldvalidator id="rfvRELATIONSHIP" runat="server" ControlToValidate="cmbRELATIONSHIP" ErrorMessage="RELATIONSHIP can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator>--%>
									</TD>
									<%--<TD class="midcolora" width="18%"><asp:label id="capDRIVER_BROADEND_NOFAULT" runat="server">Broadend No-Fault</asp:label></TD>
								<TD class="midcolora" width="32%">
									<asp:DropDownList id="cmbDRIVER_BROADEND_NOFAULT" runat="server"></asp:DropDownList></TD>--%>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_US_CITIZEN" runat="server">U.S. Citizen</asp:label><span id="spnDRIVER_US_CITIZEN" runat="server" class="mandatory">*</span></TD> <%-- Span added by Sibin on 28 Nov 08 for Itrack Issue 5061--%>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_US_CITIZEN" runat="server"></asp:dropdownlist><BR>
									  <%-- Added by Sibin for Itrack Issue 5061 on 28 Nov 08--%>
										<asp:requiredfieldvalidator id="rfvDRIVER_US_CITIZEN" runat="server" ControlToValidate="cmbDRIVER_US_CITIZEN" Display="Dynamic"></asp:requiredfieldvalidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_DRINK_VIOLATION" runat="server">Drinking or Drug Related violation in last 5 years?</asp:label><span class="mandatory" id="spnDRIVER_DRINK_VIOLATION">*</span></TD> <%-- Span added by Sibin on 28 Nov 08 for Itrack Issue 5061--%>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_DRINK_VIOLATION" runat="server"></asp:dropdownlist><BR>
									 <%-- Added by Sibin for Itrack Issue 5061 on 28 Nov 08--%>
										<asp:requiredfieldvalidator id="rfvDRIVER_DRINK_VIOLATION" runat="server" ControlToValidate="cmbDRIVER_DRINK_VIOLATION" Display="Dynamic"></asp:requiredfieldvalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capNO_CYCLE_ENDMT" runat="server"></asp:label><span class="mandatory" id="spnNO_CYCLE_ENDMT">*</span></TD> <%-- Span added by Sibin on 28 Nov 08 for Itrack Issue 5061--%>
									<td class="midcolora" width="32%"><asp:dropdownlist id="cmbNO_CYCLE_ENDMT" onfocus="SelectComboIndex('cmbNO_CYCLE_ENDMT')" runat="server"></asp:dropdownlist><BR>
									<%-- Added by Sibin for Itrack Issue 5061 on 28 Nov 08--%>
										<asp:requiredfieldvalidator id="rfvNO_CYCLE_ENDMT" runat="server" ControlToValidate="cmbNO_CYCLE_ENDMT" Display="Dynamic"></asp:requiredfieldvalidator>
									</td>
								</tr>
								<tr>
									<%--<TD class="midcolora" width="18%"><asp:label id="capDRIVER_LIC_SUSPENDED" runat="server">License suspended, restricted, or revoked in the last 5 years</asp:label></TD>
								<TD class="midcolora" width="32%">
									<asp:DropDownList id="cmbDRIVER_LIC_SUSPENDED" runat="server"></asp:DropDownList></TD>--%>
									<TD class="midcolora" width="18%">
										<asp:label id="capDRIVER_DRIV_TYPE" runat="server"></asp:label><span class="mandatory" id="spnDRIVER_DRIV_TYPE" runat="server">*</span>
									</TD>
									<td class="midcolora" width="32%">
										<asp:dropdownlist id="cmbDRIVER_DRIV_TYPE" runat="server" OnFocus="SelectComboIndex('cmbDRIVER_DRIV_TYPE')" onchange="CheckForDriverType(0);">
										<ASP:LISTITEM Value="0"></ASP:LISTITEM>
										</asp:dropdownlist>
										<br>
										<%-- Added by Manoj Rathore for Itrack Issue 5881 on 25 May 09--%>
										<asp:requiredfieldvalidator id="rfvDRIVER_DRIV_TYPE" runat="server" ControlToValidate="cmbDRIVER_DRIV_TYPE" Display="Dynamic"></asp:requiredfieldvalidator>
									</td>
									<td colspan="2" class="midcolora"></td>
								</tr>
								<tr id="trCollegetStudent">
									<TD class="midcolora" width="18%">
										<asp:label id="capCOLL_STUD_AWAY_HOME" runat="server"></asp:label><span id="spnCOLL_STUD_AWAY_HOME" class="mandatory">*</span>
									</TD>
									<td class="midcolora" width="32%">
										<asp:dropdownlist id="cmbCOLL_STUD_AWAY_HOME" runat="server" OnFocus="SelectComboIndex('cmbCOLL_STUD_AWAY_HOME')"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvCOLL_STUD_AWAY_HOME" runat="server" Display="Dynamic" ControlToValidate="cmbCOLL_STUD_AWAY_HOME"></asp:requiredfieldvalidator>
									</td>
									<TD class="midcolora" width="18%">
										<asp:label id="capCYCL_WITH_YOU" runat="server"></asp:label><span id="spnCYCL_WITH_YOU" class="mandatory">*</span>
									</TD>
									<td class="midcolora" width="32%">
										<asp:dropdownlist id="cmbCYCL_WITH_YOU" runat="server" OnFocus="SelectComboIndex('cmbCYCL_WITH_YOU')"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvCYCL_WITH_YOU" runat="server" Display="Dynamic" ControlToValidate="cmbCYCL_WITH_YOU"></asp:requiredfieldvalidator>
									</td>
								</tr>
								<tr id="trDriverDiscount">
									<TD class="headerEffectSystemParams" colSpan="4">Driver Discount
									</TD>
								</tr>
								<tr id="trMatureDriver">
									<TD class="midcolora" width="18%">
										<asp:label id="capMATURE_DRIVER" runat="server"></asp:label>
									</TD>
									<TD class="midcolora" width="18%">
									<%-- As per Itrack 5891  --%>
										<%--asp:dropdownlist id="cmbMATURE_DRIVER" runat="server" OnFocus="SelectComboIndex('cmbMATURE_DRIVER')"></asp:dropdownlist--%>
										<asp:Label ID="capMatureDriver" Runat="server"></asp:Label>
									</TD>
									<TD class="midcolora" colspan="2"></TD>
									<%--<TD class="midcolora" width="18%">
									<asp:label id="capPREFERRED_RISK" runat="server"></asp:label>
								</TD>
								<TD class="midcolora" width="32%">
									<asp:dropdownlist id="cmbPREFERRED_RISK" runat="server" OnFocus="SelectComboIndex('cmbPREFERRED_RISK')"></asp:dropdownlist>
									<asp:Label ID="capPreferedRisk" Runat="server"></asp:Label>
								</TD>--%>
								</tr>
								<tr id="trRisk">
									<TD class="midcolora" width="18%">
									<%--Removed Inline Text of capPREFERRED_RISK for Itrack issue 5744 on 23-April-09--%>
										<asp:label id="capPREFERRED_RISK" runat="server"></asp:label>
									</TD>
									<TD class="midcolora" width="32%">
										<%--<asp:dropdownlist id="cmbPREFERRED_RISK" runat="server" OnFocus="SelectComboIndex('cmbPREFERRED_RISK')"></asp:dropdownlist>--%>
										<asp:Label ID="capPreferedRisk" Runat="server"></asp:Label>
									</TD>
									<TD class="midcolora" colspan="2"></TD>
								</tr>
								<%-- Commented by Charles on 2-Jul-09 for Itrack issue 6012
								<!-- Added tr id for Itrack issue 5744 on 23-April-09 -->
								<tr id="trTRANSFEREXPERIENCE_RENEWALCREDIT">
									<TD class="midcolora" width="18%">
										<asp:label id="capTRANSFEREXPERIENCE_RENEWALCREDIT" runat="server"></asp:label>
									</TD>
									<td class="midcolora" colspan="3">
										<!-- Uncommented by Charles on 23-Jun-2009 for Itrack 6003  -->
										<asp:dropdownlist id="cmbTRANSFEREXPERIENCE_RENEWALCREDIT" runat="server" OnFocus="SelectComboIndex('cmbTRANSFEREXPERIENCE_RENEWALCREDIT')"></asp:dropdownlist>
										<asp:Label ID="capTransferExperienceRenewalCredit" Runat="server"></asp:Label>
									</td>
								</tr>
								--%>
								<tr>
									<td class="midcolora" colspan="4">
										<asp:CustomValidator ID="csvDISC_TYPE" Runat="server" ClientValidationFunction="ValidateDiscountType"
											Display="Dynamic" Enabled="False"></asp:CustomValidator>
									</td>
								</tr>
								<!--START-->
								<tr id="trViolationMVR" runat="server" visible="true">
									<TD class="headerEffectSystemParams" colSpan="4">Violations &amp; MVR Info</TD>
									<TD class="headerEffectSystemParams"></TD>
								</tr>
								<tr id="trViolations" runat="server" visible="true">
									<TD class="midcolora" width="18%"><asp:label id="capVIOLATIONS" runat="server">Violations</asp:label><span class="mandatory" id="spnVIOLATIONS">*</span></TD>
									<TD class="midcolora" width="18%" colSpan="3"><asp:dropdownlist id="cmbVIOLATIONS" onfocus="SelectComboIndex('cmbVIOLATIONS')" runat="server"></asp:dropdownlist>
										<br>
										<asp:RequiredFieldValidator ID="rfvVIOLATIONS" Runat="server" Display="Dynamic" ErrorMessage="Please select Violations."
											ControlToValidate="cmbVIOLATIONS"></asp:RequiredFieldValidator>
									</TD>
									<TD class="midcolora" width="18%"></TD>
								</tr>
								<tr id="trMVROrder" runat="server" visible="true">
									<TD class="midcolora" width="18%"><asp:label id="capMVR_ORDERED" runat="server">MVR Ordered</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbMVR_ORDERED" onfocus="SelectComboIndex('cmbMVR_ORDERED')" onchange="SetDateValidator()"
											runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDATE_ORDERED" runat="server">Date MVR Order</asp:label><span id="spnDATE_ORDERED" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%"><asp:textbox id="txtDATE_ORDERED" runat="server" size="11" maxlength="10"></asp:textbox>
										<asp:hyperlink id="hlkDATE_ORDERED" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgDATE_ORDERED" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
										</asp:hyperlink><br>
										<asp:requiredfieldvalidator id="rfvDATE_ORDERED" runat="server" ControlToValidate="txtDATE_ORDERED" ErrorMessage="Please select MVR Date ordered."
											Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revDATE_ORDERED" runat="server" ControlToValidate="txtDATE_ORDERED" ErrorMessage="Please check format of date."
											Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvDATE_ORDERED" ControlToValidate="txtDATE_ORDERED" Display="Dynamic" Runat="server"
											ClientValidationFunction="ChkDateOfOrder" ErrorMessage="Please check date of order can't be greater than today."></asp:customvalidator>
									</TD>
								</tr>
								<tr id="trMVRClass" runat="server" visible="true">
									<TD class="midcolora" width="18%"><asp:label id="capMVR_CLASS" runat="server">MVR Class</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:textbox id="txtMVR_CLASS" runat="server" Width="296px" MaxLength="50"></asp:textbox></TD>
									<TD class="midcolora" width="18%"><asp:label id="capMVR_LIC_CLASS" runat="server">MVR License Class</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:textbox id="txtMVR_LIC_CLASS" runat="server" Width="296px" MaxLength="50"></asp:textbox></TD>
								</tr>
								<tr id="trMVRLicense" runat="server" visible="true">
									<TD class="midcolora" width="18%"><asp:label id="capMVR_LIC_RESTR" runat="server">MVR License Restriction</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:textbox id="txtMVR_LIC_RESTR" runat="server" Width="296px" MaxLength="50"></asp:textbox></TD>
									<TD class="midcolora" width="18%"><asp:label id="capMVR_DRIV_LIC_APPL" runat="server">MVR Driver License Application</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:textbox id="txtMVR_DRIV_LIC_APPL" runat="server" Width="296px" MaxLength="50"></asp:textbox></TD>
								</tr>
								<tr id="trMVRRemark" runat="server" visible="true">
									<TD class="midcolora" width="18%"><asp:label id="capMVR_REMARKS" runat="server">MVR Remarks</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:textbox id="txtMVR_REMARKS" runat="server" Height="64px" MaxLength="250" Width="296px" TextMode="MultiLine"></asp:textbox><br>
									<asp:customvalidator id="csvMVR_REMARKS" Runat="server" Display="Dynamic" ControlToValidate="txtMVR_REMARKS"
										ErrorMessage="Maximum length of MVR Remarks is 250." ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capMVR_STATUS" runat="server">MVR Status</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbMVR_STATUS" onfocus="SelectComboIndex('cmbMVR_STATUS')" runat="server" Height="26px"></asp:dropdownlist></TD>
								</tr>
								<tr id="trLossOrder" runat="server" visible="true">
									<TD class="midcolora" width="18%"><asp:label id="capLOSSREPORT_ORDER" runat="server">Property Loss Report Ordered</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOSSREPORT_ORDER" onfocus="SelectComboIndex('cmbLOSSREPORT_ORDER')" runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capLOSSREPORT_DATETIME" runat="server">Date Ordered</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtLOSSREPORT_DATETIME" runat="server" size="11" maxlength="10"></asp:textbox><asp:hyperlink id="hlkLOSSREPORT_DATETIME" runat="server" CssClass="HotSpot">
									<ASP:IMAGE id="imgLOSSREPORT_DATETIME" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
									</asp:hyperlink><br>
									<asp:regularexpressionvalidator id="revLOSSREPORT_DATETIME" runat="server" ControlToValidate="txtLOSSREPORT_DATETIME" ErrorMessage="Please check format of date."
									Display="Dynamic"></asp:regularexpressionvalidator></TD>
								</tr>
								<!--END-->
								
								<%--Added Violation Points section by Sibin for Itrack Issue 5061 on 28 Nov 08--%>
								<tr id="trViolationSec" runat="server" visible="true">
										<TD class="headerEffectSystemParams" colSpan="4">Violation Points</TD>
									</tr>
									<tr id="trViolationMsg" style="DISPLAY: none" runat="server">
										<td class="midcolora" colSpan="4"><asp:label id="lblViolationMsg" Runat="server"></asp:label></td>
									</tr>
									<tr id="trViolationField" runat="server">
										<TD class="midcolora" width="18%"><asp:label id="capViolationPoints" Runat="server">Total Violation Points (Minor 2 years/ Major 5 years)</asp:label></TD>
										<TD class="midcolora" width="32%"><asp:label id="lblViolationPoints" Runat="server"></asp:label></TD>
										<TD class="midcolora" width="18%"><asp:label id="capAccidentPoints" Runat="server">Total Accident Points (during the last 3 years)</asp:label></TD>
										<TD class="midcolora" width="32%"><asp:label id="lblAccidentPoints" Runat="server"></asp:label></TD>
								</tr>								
								<tbody id="tbAssVehSec">
									<tr id="assignVehSec">
										<TD class="headerEffectSystemParams" colSpan="4">Assigned Vehicle</TD>
									</tr>
									<tr id="trVehMsg" style="DISPLAY: none" runat="server">
										<td class="midcolora" colSpan="4"><asp:label id="lblVehicleMsg" Runat="server"></asp:label></td>
									</tr>
									<tr id="trVehField" runat="server" style="DISPLAY: none">
										<td colSpan="4" class="midcolora">
											<table cellSpacing="0" cellPadding="0" width="47%" border="0">
												<tr align="left">
													<td class="midcolora" align="left">
														<asp:label id="capVEHICLE_ID" runat="server">Drive</asp:label><span class="mandatory" id="spnVEHICLE_ID">*</span>
													</td>
													<td class="midcolora" align="left" width="35%">
														&nbsp;<asp:dropdownlist id="cmbVEHICLE_ID" onfocus="SelectComboIndex('cmbVEHICLE_ID')" runat="server"></asp:dropdownlist>&nbsp;as&nbsp;<br>
														&nbsp;<asp:requiredfieldvalidator id="rfvVEHICLE_ID" runat="server" ControlToValidate="cmbVEHICLE_ID" Display="Dynamic"
															Enabled="False"></asp:requiredfieldvalidator>
													</td>
													<td class="midcolora" align="left">
														<asp:dropdownlist id="cmbAPP_VEHICLE_PRIN_OCC_ID" onfocus="SelectComboIndex('cmbAPP_VEHICLE_PRIN_OCC_ID')"
															runat="server"></asp:dropdownlist>&nbsp;<asp:label id="capAPP_VEHICLE_PRIN_OCC_ID" runat="server">Driver</asp:label><br>
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
									<td colSpan="4"><asp:Table id="tblAssignedVeh" runat="server" width="100%" Border="0"></asp:Table></td>
									</tr>
								</tbody>								
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" CausesValidation="false"
											Text="Activate/Deactivate"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" causesvalidation="false"></cmsb:cmsbutton>
									</td>
									<td class="midcolorr" colSpan="2">
										<cmsb:cmsbutton class="clsButton" id="btnCopyAppDrivers" runat="server" Text="Copy Application Drivers"
											Visible="false"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidDRIVER_ID" type="hidden" name="hidDRIVER_ID" runat="server"> <INPUT id="hidCUSTOMER_ID" type="hidden" name="hidAPP_ID" runat="server">
			<INPUT id="hidCUSTOMER_INFO" type="hidden" name="hidCUSTOMER_INFO" runat="server">
			<INPUT id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server"> <INPUT id="hidAppId" type="hidden" name="hidAppId" runat="server">
			<INPUT id="hidAppVersionId" type="hidden" name="hidAppVersionId" runat="server">
			<INPUT id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server"> <INPUT id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server">
			<INPUT id="hidStateID" type="hidden" name="hidStateID" runat="server"> <INPUT id="hidAPP_EFFECTIVE_DATE" type="hidden" name="hidAPP_EFFECTIVE_DATE" runat="server">
			<INPUT id="hidMVRPoints" type="hidden" name="hidMVRPoints" runat="server" value="0">
			<input id="hidDataValue1" type="hidden" name="hidDataValue1" runat="server"> <input id="hidDataValue2" type="hidden" name="hidDataValue2" runat="server">
			<input id="hidCustomInfo" type="hidden" name="hidCustomInfo" runat="server"> <input id="hidSeletedData" type="hidden" name="hidSeletedData" runat="server">
			<input id="hidDRIVER_DRIV_LIC" type="hidden" value="0" name="hidDRIVER_DRIV_LIC" runat="server">
			<input id="hidDRIVER_DRIV_LIC_SELINDEX" type="hidden" value="0" name="hidDRIVER_DRIV_LIC_SELINDEX" runat="server">
			<INPUT id="hidSSN_NO" type="hidden" name="hidSSN_NO" runat="server">
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
			//RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidDRIVER_ID').value, true);
			if (document.getElementById("hidFormSaved").value == "5")
			 {
				/*Record deleted*/
				/*Refreshing the grid and coverting the form into add mode*/
				/*Using the javascript*/
				RemoveTab(5,this.parent);	
				RemoveTab(4,this.parent);	
				RemoveTab(3,this.parent);	
				RemoveTab(2,this.parent);
				RefreshWebGrid("5","1",true,true); 
				document.getElementById('hidDRIVER_ID').value = "NEW";				
			}
			else if (document.getElementById("hidFormSaved").value == "1")
			{
				this.parent.strSelectedRecordXML = "-1";
				RefreshWebGrid(document.getElementById('hidFormSaved').value,TrimTheString(document.getElementById('hidDRIVER_ID').value),true);				
			}
				SetDateValidator();
		</script>
	</BODY>
</HTML>

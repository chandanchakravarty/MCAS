<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PolicyAddWatercraftInformation.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Watercrafts.PolicyAddWatercraftInformation" ValidateRequest="false"%>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>Policy Watercraft Information</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="vbscript">
			' Added By Swastika on 7th Mar'06 for Pol Iss #59
			Function getUserConfirmationForDeactivate
				getUserConfirmationForDeactivate= msgbox("Deactivating the current Watercraft will unassign the boat from the operator." & vbcrlf & " Do you want to continue?",35,"CMS")
			End function
			Function getUserConfirmationForDelete
				getUserConfirmationForDelete= msgbox("Deleting the current Watercraft will unassign the boat from the operator." & vbcrlf & " Do you want to continue?",35,"CMS")
			End function
		</script>
		<script language="javascript">
		
		//Added by Asfa (26-June-2008) - iTrack #4200
		function Validate()
		{
			var result = GetZipForState(true);
			Page_ClientValidate();
			Page_IsValid = Page_IsValid && result;
			return Page_IsValid;
		}
		function ChkResult(objSource , objArgs)
		{
			if(GlobalError==true)
			{
				Page_IsValid = false;
				objArgs.IsValid = false;
			}
			else
			{
				objArgs.IsValid = true;				
			}
		}		
		function handleResult(res) 
		{
			if(!res.error)
			{
				if (res.value!="") 
					GlobalError=false;
				else
					GlobalError=true;
			}
			else
			{
				GlobalError=true;		
			}
		}

		

		
		//End by Asfa (26-June-2008) - iTrack #4200
		
		////////////////////////////////////AJAX CALLS FOR ZIP///////////////////////////
		function GetZipForState(flag)
		{		    
			GlobalError=true;			
			
			if(document.getElementById('txtLOCATION_ZIP').value!="")
			{
				//if(isNaN(document.getElementById('txtLOC_ZIP').value))
				//	return;
				var intStateID = document.getElementById('cmbLOCATION_STATE').options[document.getElementById('cmbLOCATION_STATE').options.selectedIndex].value;
				var strZipID = document.getElementById('txtLOCATION_ZIP').value;	
				var result = PolicyAddWatercraftInformation.AjaxFetchZipForState(intStateID,strZipID);
				AjaxCallFunction_CallBack(result);
				if(GlobalError)
				{
					document.getElementById('csvLOCATION_ZIP').setAttribute('enabled',true);
					document.getElementById('csvLOCATION_ZIP').setAttribute('isValid',true);
					document.getElementById('csvLOCATION_ZIP').style.display = 'inline';			
					return false;
				}
				else
				{
					document.getElementById('csvLOCATION_ZIP').setAttribute('enabled',false); 
			   		document.getElementById('csvLOCATION_ZIP').setAttribute('isValid',false);
					document.getElementById('csvLOCATION_ZIP').style.display = 'none';
			
					//if(window.event.srcElement.id == "btnSave")
					//	document.forms[0].submit();
					return true;
				}
			
			}	
			return false;
		}
		function AjaxCallFunction_CallBack(response)
		{	
			if(document.getElementById('txtLOCATION_ZIP').value!="")
			{
					handleResult(response);
					if(GlobalError)
					{
						return false;
					}
					else
					{
						return true;
					}
					
				return false;			
			}
			else 
				return true;		
		      		
		}
			
		/////EMP ZIP AJAX////////////////
		
		
		//As per new requirements, Address will be populated only if textbox is blank.
		//If text box had data in it, do not pull from DB
		function PullCustomerAddress()
		{
			if (document.getElementById("txtLOCATION_ADDRESS").value == "")
				document.getElementById("txtLOCATION_ADDRESS").value = document.getElementById("hidCustomerAddress").value; 
				
			if (document.getElementById("txtLOCATION_CITY").value == "")	
				document.getElementById("txtLOCATION_CITY").value = document.getElementById("hidCustomerCity").value;
			
			if (document.getElementById("txtLOCATION_ZIP").value == "")	
				document.getElementById("txtLOCATION_ZIP").value = document.getElementById("hidCustomerZip").value;
			
			if (document.getElementById("cmbLOCATION_STATE").value == "")	
				document.getElementById("cmbLOCATION_STATE").value = document.getElementById("hidCustomerState").value;
			
			ChangeColor();
			DisableValidators();				
			return false;
		}
		
		
		//While saving check that sail boat is seelcted or not. 
		//If it is not selected then set include sail boat dropdown to 'NO'
		//This is done to prevent the problem that was coming in Discount/Surcharge Page.
		//If yes was saved then sail boat disocunt was visible on Discount/Surcharge Page.
		function HandleSaleBoat()
		{
			Validate();
			if (document.getElementById('cmbTYPE_OF_WATERCRAFT').selectedIndex != -1)
			{
				var CurrBoatType = document.getElementById('cmbTYPE_OF_WATERCRAFT').options[document.getElementById('cmbTYPE_OF_WATERCRAFT').selectedIndex].value;
			
			
				if (CurrBoatType != '11372' && CurrBoatType != '11672')
				{
					document.getElementById('cmbREMOVE_SAILBOAT').selectedIndex = 1;					
					return;
				}
			}
			
		CheckForHullMandatory();
		}
		
		function ismaxlength(obj)
		{
			var mlength=obj.getAttribute? parseInt(obj.getAttribute("maxlength")) : ""
			if (obj.getAttribute && obj.value.length>mlength)
				obj.value=obj.value.substring(0,mlength)
		}
		
		function ShowDatesToMonth()
		{
			var NumOfDays = new Array (31,29,31,30,31,30,31,31,30,31,30,31);
			//Array NumOfDaysLeapYear = new Array (31,29,31,30,31,30,31,31,30,31,30,31);
			
			var MaxDays = NumOfDays[(document.getElementById('cmbLAY_UP_PERIOD_TO_MONTH').options.selectedIndex) - 1];
			document.getElementById('cmbLAY_UP_PERIOD_TO_DAY').innerText = "";
			
			var anOption = document.createElement("OPTION") 				
			document.getElementById('cmbLAY_UP_PERIOD_TO_DAY').options.add(anOption)				
			anOption.innerText = "";
			anOption.Value = 0;
			
			for (var i = 1; i <= MaxDays; i++)
			{
				//alert(i);
				var anOption = document.createElement("OPTION") 				
				document.getElementById('cmbLAY_UP_PERIOD_TO_DAY').options.add(anOption)				
				anOption.innerText = i;
				anOption.Value = i;
			}		
		}
		
		function ShowDatesFromMonth()
		{
			var NumOfDays = new Array (31,29,31,30,31,30,31,31,30,31,30,31);
			//Array NumOfDaysLeapYear = new Array (31,29,31,30,31,30,31,31,30,31,30,31);
			
			var MaxDays = NumOfDays[(document.getElementById('cmbLAY_UP_PERIOD_FROM_MONTH').options.selectedIndex) - 1];
			document.getElementById('cmbLAY_UP_PERIOD_FROM_DAY').innerText = "";
			
			var anOption = document.createElement("OPTION") 				
			document.getElementById('cmbLAY_UP_PERIOD_FROM_DAY').options.add(anOption)				
			anOption.innerText = "";
			anOption.Value = 0;
			
			for (var i = 1; i <= MaxDays; i++)
			{
				//alert(i);
				var anOption = document.createElement("OPTION") 				
				document.getElementById('cmbLAY_UP_PERIOD_FROM_DAY').options.add(anOption)				
				anOption.innerText = i;
				anOption.Value = i;
			}		
		}
		
		function DieselEngineDiscount(LoadType)
		{
			//Sumit Chhabra:12-04-2006
			//When the LoadType is OLD, ie page has loaded after saving, don't do anything, return 
			//else when the page is loaded for the first time, continue functioning
			//It is done to prevent the Saved Value at Diesel Discount from being overwritten when saved page loads
			if(typeof(LoadType)=='undefined' || LoadType=='OLD')
				return;
		/*	if(document.getElementById('cmbFUEL_TYPE').selectedIndex<1)
			{				
				document.getElementById('cmbDIESEL_ENGINE').selectedIndex = 1;
				return;
			}			
			/*Diesel Engine Combo box Values
			Diesel - 3725
			Gas - 3726 
			None - 3724*/
		/*	if(document.getElementById('cmbFUEL_TYPE').options[document.getElementById('cmbFUEL_TYPE').selectedIndex].value==3725)
				document.getElementById('cmbDIESEL_ENGINE').selectedIndex = 2;	
			else
				document.getElementById('cmbDIESEL_ENGINE').selectedIndex = 1;
			*/		
		}		
		
		function CheckDateValidity(objSource , objArgs)
		{ 
			//If Reg Exp validator for text box is fired, do not check custom validator
			if (document.getElementById("revDATE_PURCHASED").getAttribute("IsValid") == false)
			{
				objArgs.IsValid = true;
				return;
			}
					
			var sDate = DateAdd();		
			var isValid = false;				 
			var jsaAppDtFormat = "<%=aAppDtFormat%>";			
			objArgs.IsValid = DateComparer(sDate,document.APP_WATERCRAFT_INFO.txtDATE_PURCHASED.value,jsaAppDtFormat);
		}
		
		function CheckMarineDateValidity(objSource , objArgs)
		{
			//If Reg Exp validator for text box is fired, do not check custom validator
			if (document.getElementById("revDATE_MARINE_SURVEY").getAttribute("IsValid") == false)
			{
				objArgs.IsValid = true;
				return;
			}
			
			var sDate = DateAdd();
			var isValid = false;
			var jsaAppdtFormat = "<%=aAppDtFormat %>";
			objArgs.IsValid = DateComparer(sDate,document.APP_WATERCRAFT_INFO.txtDATE_MARINE_SURVEY.value,jsaAppdtFormat);
		}
		
function AddData()
{
		ChangeColor();
		DisableValidators();

		//document.getElementById('txtMAX_SPEED').focus();		
		document.getElementById('hidBOAT_ID').value	=	'New';
		document.getElementById('txtYEAR').value  = '';
		document.getElementById('txtMAKE').value  = '';
		document.getElementById('txtMODEL').value  = '';
		document.getElementById('cmbSTATE_REG').options.selectedIndex = -1;
		
		document.getElementById('cmbHULL_MATERIAL').options.selectedIndex = -1;
		document.getElementById('cmbFUEL_TYPE').options.selectedIndex = -1;

		document.getElementById('cmbTYPE_OF_WATERCRAFT').options.selectedIndex = -1;
		document.getElementById('cmbTERRITORY').options.selectedIndex = -1;

		document.getElementById('txtDATE_PURCHASED').value  = '';
		document.getElementById('txtLENGTH').value  = '';
		document.getElementById('txtINCHES').value  = '';
		document.getElementById('txtSERIAL_NO').value  = '';
		
		document.getElementById('txtWATERCRAFT_HORSE_POWER').value  = '';
		document.getElementById('txtINSURING_VALUE').value  = '';

		document.getElementById('txtMAX_SPEED').value  = '';
		document.getElementById('cmbTWIN_SINGLE').options.selectedIndex = -1;
		document.getElementById('cmbWATERS_NAVIGATED').options.selectedIndex = -1;

		document.getElementById('cmbLORAN_NAV_SYSTEM').options.selectedIndex = 1;
		document.getElementById('cmbREMOVE_SAILBOAT').options.selectedIndex = 1;
		document.getElementById('cmbDUAL_OWNERSHIP').options.selectedIndex = 1;
		document.getElementById('cmbSHORE_STATION').options.selectedIndex = 1;
		document.getElementById('cmbHALON_FIRE_EXT_SYSTEM').options.selectedIndex = 1;
		document.getElementById('cmbCOV_TYPE_BASIS').options.selectedIndex = -1;
		document.getElementById('cmbPHOTO_ATTACHED').options.selectedIndex = 1;
		document.getElementById('cmbMARINE_SURVEY').options.selectedIndex = 1;
		document.getElementById('txtDATE_MARINE_SURVEY').value  = '';
		
		document.getElementById('cmbLAY_UP_PERIOD_FROM_MONTH').options.selectedIndex = -1;
		document.getElementById('cmbLAY_UP_PERIOD_FROM_DAY').options.selectedIndex = -1

		document.getElementById('cmbLAY_UP_PERIOD_TO_MONTH').options.selectedIndex = -1
		document.getElementById('cmbLAY_UP_PERIOD_TO_DAY').options.selectedIndex = -1

		document.getElementById('txtLOCATION_ADDRESS').value  = '';
		document.getElementById('txtLOCATION_CITY').value  = '';
		document.getElementById('cmbLOCATION_STATE').options.selectedIndex = -1
		document.getElementById('txtLOCATION_ZIP').value  = '';

		if(document.getElementById('btnDelete'))
		    document.getElementById('btnDelete').setAttribute('disabled', true);
		if (document.getElementById('btnActivateDeactivate'))
		    document.getElementById('btnActivateDeactivate').setAttribute('disabled', true);
			
		
}

function ShowMarineDate()
	{
		if(document.getElementById('cmbMARINE_SURVEY').selectedIndex == 2) //YES
		{
			
			document.getElementById('txtDATE_MARINE_SURVEY').style.display='inline';
			document.getElementById('capDATE_MARINE_SURVEY').style.display='inline';
			document.getElementById('imgMARINE_SURVEY').style.display='inline';
		}
		else
		{
			document.getElementById('txtDATE_MARINE_SURVEY').value = '';
			document.getElementById('txtDATE_MARINE_SURVEY').style.display='none';
			document.getElementById('capDATE_MARINE_SURVEY').style.display='none';
			document.getElementById('imgMARINE_SURVEY').style.display='none';
			
			
		}
	} 


function setTab()
{  
	var str="";
	if(document.getElementById('hidBOAT_ID')!=null)
		str=new String(document.getElementById('hidBOAT_ID').value);		
			
    if (document.getElementById('hidBOAT_ID')!=null && str.toUpperCase()!="NEW")
	{ 
		var CalledFrom ='';
		if (document.getElementById('hidCalledFrom')!=null)
		{
			CalledFrom=document.getElementById('hidCalledFrom').value;
		}	
		var CustomerID='';
		if (document.getElementById('hidCustomerID')!=null)
		{
			CustomerID=document.getElementById('hidCustomerID').value;
		}
		var AppID='';
		if (document.getElementById('hidAppID')!=null)
		{
			AppID=document.getElementById('hidAppID').value;
		}
		var AppVersionID='';
		if (document.getElementById('hidAppVersionID')!=null)
		{
			AppVersionID=document.getElementById('hidAppVersionID').value;
		}
		var BoatID='';
		if (document.getElementById('hidBOAT_ID')!=null)
		{
			BoatID=document.getElementById('hidBOAT_ID').value;
		} 		
		
		var args;
		
		if ( CalledFrom == "UMB")
		{
			args = "UWAT";
		}
		
		if ( CalledFrom == "WAT")
		{
			args = "WAT";
		}
		
		if ( CalledFrom == "HOME")
		{
			args = "HOME";
		}
		
		if ( CalledFrom == "RENT")
		{
			args = "RENT";
		}
		var lobStr='<%=lob%>'
		
		if(document.getElementById('cmbTYPE_OF_WATERCRAFT').value=="11369")
		{
		
			Url = "WatercraftEngineIndex.aspx?CalledFrom=WAT&CUSTOMER_ID="+CustomerID+"&APP_ID="+AppID+"&APP_VERSION_ID="+AppVersionID+"&BOAT_ID="+BoatID+"&"; 
			DrawTab(2,top.frames[1],'Outboard Engine Information',Url); 
	
			Url = "../PolicyCoverages.aspx?PageFrom=" + lobStr +"&CalledFrom=WAT&VehicleID=" + document.APP_WATERCRAFT_INFO.hidBOAT_ID.value + "&PageTitle=Limits/Endorsement Information" + "&"; 
			DrawTab(3,top.frames[1],'Limits/Endorsement Information',Url); 
			
			Url = "../PolicyEndorsement.aspx.aspx?PageFrom=" + lobStr +"&CalledFrom=WAT&VehicleID=" + document.APP_WATERCRAFT_INFO.hidBOAT_ID.value + "&PageTitle=Limits/Endorsement Information" + "&"; 
			DrawTab(4,top.frames[1],'Endorsements',Url); 
			
			if (CalledFrom !="UMB")
			{
				Url="../AdditionalInterestIndex.aspx?PageFrom=" + lobStr +"&CalledFrom=WAT&CUSTOMER_ID="+CustomerID+"&APP_ID="+AppID+"&APP_VERSION_ID="+AppVersionID+"&BOAT_ID1="+BoatID+"&"; 
				DrawTab(5,top.frames[1],'Additional Interest',Url); 
			}
		}
		else
		{		
			Url = "../PolicyCoverages.aspx?PageFrom=" + lobStr +"&CalledFrom=WAT&VehicleID=" + document.APP_WATERCRAFT_INFO.hidBOAT_ID.value + "&PageTitle=Limits/Endorsement Information" + "&"; 
			DrawTab(2,top.frames[1],'Limits/Endorsement Information',Url); 
			
			Url = "../PolicyEndorsement.aspx?PageFrom=" + lobStr +"&CalledFrom=WAT&VehicleID=" + document.APP_WATERCRAFT_INFO.hidBOAT_ID.value + "&PageTitle=Limits/Endorsement Information" + "&"; 
			DrawTab(3,top.frames[1],'Endorsements',Url); 
			
			if (CalledFrom !="UMB")
			{
				Url="../AdditionalInterestIndex.aspx?PageFrom=" + lobStr +"&CalledFrom=WAT&CUSTOMER_ID="+CustomerID+"&APP_ID="+AppID+"&APP_VERSION_ID="+AppVersionID+"&BOAT_ID1="+BoatID+"&"; 
				DrawTab(4,top.frames[1],'Additional Interest',Url); 
			}
			
			RemoveTab(5,top.frames[1]);			
		}		
	}
	else
	{	
		if(document.getElementById('cmbTYPE_OF_WATERCRAFT').value=="11369")
		{			
			RemoveTab(4,top.frames[1]);	
			RemoveTab(3,top.frames[1]);	
			RemoveTab(2,top.frames[1]);	
			
		}
		else
		{			
			RemoveTab(5,top.frames[1]);	
			RemoveTab(4,top.frames[1]);	
			RemoveTab(3,top.frames[1]);	
			RemoveTab(2,top.frames[1]);	
		}
		
	}
	return false; 
}



function populateXML()
{
	if (document.getElementById('trBody') && document.getElementById('trBody').style.display != "none")	
	{
		document.getElementById('txtBOAT_NO').focus();
	}
	
	if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
	{
		var tempXML;			
		if(document.getElementById('hidOldData').value != '')
		{			
			tempXML=document.getElementById('hidOldData').value;							 
			if(tempXML!="" && tempXML!=0)
			{
				populateFormData(tempXML,APP_WATERCRAFT_INFO);
				//ShowDatesToMonth();
				//ShowDatesFromMonth();
				document.getElementById('txtINSURING_VALUE').value = formatCurrency(document.getElementById('txtINSURING_VALUE').value);
				if(document.getElementById('btnDelete'))	
					document.getElementById('btnDelete').setAttribute('disabled',false); 				
				ShowHideDiscounts('OLD');
				//added by pravesh on 11 dec 2006
				if(document.getElementById("hidCalledFrom").value!="<%=CALLED_FROM_UMBRELLA%>")
				{
					if (document.getElementById('cmbCOV_TYPE_BASIS').selectedIndex == "3")
					{
						document.getElementById('capINSURING_VALUE').style.display = 'none';
						document.getElementById('txtINSURING_VALUE').style.display = 'none';
						document.getElementById('revINSURING_VALUE').setAttribute("enabled",false);
						document.getElementById('revINSURING_VALUE').style.display = 'none';
						document.getElementById('rfvINSURING_VALUE').setAttribute("enabled",false);
						document.getElementById('rfvINSURING_VALUE').style.display = 'none';
						document.getElementById('spnINSURANCE_VALUE').style.display = 'none';
					}
					else
					{
						document.getElementById('capINSURING_VALUE').style.display = 'inline';
						document.getElementById('txtINSURING_VALUE').style.display = 'inline';
						document.getElementById('revINSURING_VALUE').setAttribute("enabled",true);
						document.getElementById('rfvINSURING_VALUE').setAttribute("enabled",true);
						document.getElementById('spnINSURANCE_VALUE').style.display = 'inline';
					}
				}
				//end 
			}			 
			else
			{			
				AddData();
			}			
			RemoveSailboat();
		}
		else // hidOldData != ''
		{
			AddData();
		}
	} 
					
	
	Check();
	return false;
}

	function RemoveSailboat()
	{	
		/**********************************
		cases
		-----
		Removal of Sailboat racing exclusion" should only be displayed 
			if Type of Watercraft is Sailboat and
			length of Sailboat is <= 25 (accept UPTO 25" 11')
			
			If policy effective date is prior to 01/03/2006 
			then the Surcharge Removal of Sailbaot racing exclusion should be displayed 
			even if Sailboat length is exceeding 25 feet.
		***********************************/
		
		var CurrBoatType; 
		
		if (document.getElementById('cmbTYPE_OF_WATERCRAFT').selectedIndex != -1)
		{
			CurrBoatType = document.getElementById('cmbTYPE_OF_WATERCRAFT').options[document.getElementById('cmbTYPE_OF_WATERCRAFT').selectedIndex].value;
		
		
			if (CurrBoatType != '11372' && CurrBoatType != '11672')
			{
				document.getElementById('cmbREMOVE_SAILBOAT').style.display = 'none';
				document.getElementById('capREMOVE_SAILBOAT').style.display = 'none';
				return;
			}
			
			if (DateComparer(document.getElementById('hidPolEffDate').value,'1/3/2006',"<%=aAppDtFormat%>")== false)
			{
				document.getElementById('cmbREMOVE_SAILBOAT').style.display = 'inline';
				document.getElementById('capREMOVE_SAILBOAT').style.display = 'inline';
			}
			else
			{
				if (parseInt(document.getElementById('txtLENGTH').value) > 25)
				{ 
					//document.getElementById('cmbREMOVE_SAILBOAT').selectedIndex = -1;
					/*Commented by Asfa (06-June-2008) - iTrack #4273
					document.getElementById('cmbREMOVE_SAILBOAT').style.display = 'none';
					document.getElementById('capREMOVE_SAILBOAT').style.display = 'none';
					*/
					document.getElementById('cmbREMOVE_SAILBOAT').style.display = 'inline';
					document.getElementById('capREMOVE_SAILBOAT').style.display = 'inline';
				}
				//else if (parseInt(document.getElementById('txtLENGTH').value) == 26 && document.getElementById('txtINCHES').value != "")
				//{
				//	//document.getElementById('cmbREMOVE_SAILBOAT').selectedIndex = -1;
				//	document.getElementById('cmbREMOVE_SAILBOAT').style.display = 'none';
				//	document.getElementById('capREMOVE_SAILBOAT').style.display = 'none';
				//}
				else
				{
					document.getElementById('cmbREMOVE_SAILBOAT').style.display = 'inline';
					document.getElementById('capREMOVE_SAILBOAT').style.display = 'inline';
				}
			}
		}
	}
   function WatercraftAgeOnClick()
   {
	if (WatercraftAge() == false) 
	  { 
		alert("<%=strErrMessageAgreedValueMessage%>");		
		return false;
	  }	
	  else
	  	return true;
   
   }
	function WatercraftAge()
		{	
			if(document.getElementById("hidCalledFrom").value=="<%=CALLED_FROM_UMBRELLA%>")
				return true;
		//added by pravesh on 11 dec 2006
			if (document.getElementById('cmbCOV_TYPE_BASIS').selectedIndex == "3")
			{
				document.getElementById('capINSURING_VALUE').style.display = 'none';
				document.getElementById('txtINSURING_VALUE').style.display = 'none';
				document.getElementById('revINSURING_VALUE').setAttribute("enabled",false);
				document.getElementById('revINSURING_VALUE').style.display = 'none';
				document.getElementById('rfvINSURING_VALUE').setAttribute("enabled",false);
				document.getElementById('rfvINSURING_VALUE').style.display = 'none';
				document.getElementById('spnINSURANCE_VALUE').style.display = 'none';
				document.getElementById('capINSURING_VALUE').value = "";
				document.getElementById('txtINSURING_VALUE').value = "";
				//ChangeColor();
			}
			else
			{	
				document.getElementById('capINSURING_VALUE').style.display = 'inline';
				document.getElementById('txtINSURING_VALUE').style.display = 'inline';		
				document.getElementById('revINSURING_VALUE').setAttribute("enabled",true);
				document.getElementById('rfvINSURING_VALUE').setAttribute("enabled",true);
				document.getElementById('spnINSURANCE_VALUE').style.display = 'inline';
				//ChangeColor();
			}
			//end 
		if (document.getElementById('cmbCOV_TYPE_BASIS').selectedIndex == "2")
		{	
			var ShowErrorAlert = false;
			if((document.getElementById('revINSURING_VALUE').getAttribute("IsValid") == true) && (document.getElementById('rngINSURING_VALUE').getAttribute("IsValid") == true))
			{
				if (parseInt(ReplaceAll(document.getElementById('txtINSURING_VALUE').value,",","")) > 75000)
				{				
					document.getElementById('spnErrMessageAgreedValueInsuredVal').style.display="";
					ShowErrorAlert = true;
				}			
				else
				{
					document.getElementById('spnErrMessageAgreedValueInsuredVal').style.display="none";
				}
			}
			
			if((document.getElementById('rfvLENGTH').getAttribute("IsValid") == true) && (document.getElementById('revLENGTH').getAttribute("IsValid") == true) && (document.getElementById('rngLENGTH').getAttribute("IsValid") == true) && (document.getElementById('rngINCHES').getAttribute("IsValid") == true))			
			{
				if (parseInt(ReplaceAll(document.getElementById('txtLENGTH').value,",","")) > 26)
				{
					document.getElementById('spnErrMessageAgreedValueLength').style.display="";
					ShowErrorAlert = true;
				}
				else
				{
					if ((parseInt(document.getElementById('txtLENGTH').value) == 26) && (document.getElementById('txtINCHES').value != ""))
					{
						document.getElementById('spnErrMessageAgreedValueLength').style.display="";
						ShowErrorAlert = true;
					}
					else
					{
						document.getElementById('spnErrMessageAgreedValueLength').style.display="none";
					}
				}
			}			
			
			if ((document.getElementById('rfvYEAR').getAttribute("IsValid") == true) && (document.getElementById('rngYEAR').getAttribute("IsValid") == true))
			{			
				if ((parseInt(document.getElementById('hidBoatAge').value) - parseInt(document.getElementById('txtYEAR').value) > 20))
				{
					document.getElementById('spnErrMessageAgreedValueAge').style.display="";
					ShowErrorAlert = true;
				}
				else
				{
					document.getElementById('spnErrMessageAgreedValueAge').style.display="none";
				}
			}
			 
			if (ShowErrorAlert == true)


			{	
				//alert message is shown only if called from Save Button
				if (window.event.srcElement.name == "btnSave")



				{			
					//alert("<%=strErrMessageAgreedValueMessage%>");								
				}
				return false;
			}
			else
			{
				document.getElementById('spnErrMessageAgreedValueAge').style.display="none";
				document.getElementById('spnErrMessageAgreedValueLength').style.display="none";
				document.getElementById('spnErrMessageAgreedValueInsuredVal').style.display="none";
				return true;
			} 				
		}
		else
		{
			document.getElementById('spnErrMessageAgreedValueAge').style.display="none";
			document.getElementById('spnErrMessageAgreedValueLength').style.display="none";
			document.getElementById('spnErrMessageAgreedValueInsuredVal').style.display="none";
			return true;
		}
	}	


function RightAlign(obj)
{
	if(obj)
		document.getElementById(obj.id).style.textAlign="right";
}
function CountWaterNavigate()
		{
			document.getElementById("hidWaterNavigateID").value = "";
			var coll = document.APP_WATERCRAFT_INFO.cmbWATERS_NAVIGATED;
			var len = coll.options.length;
			var k;
			var szSelectedDept;

			for( k = 0;k < len ; k++)
			{											
				if(document.getElementById("cmbWATERS_NAVIGATED").options[k].selected == true)
				{
					szSelectedDept = coll.options(k).value;
					if (document.getElementById("hidWaterNavigateID").value == "")
					{
						document.getElementById("hidWaterNavigateID").value =  szSelectedDept;
					}
					else
					{
						document.APP_WATERCRAFT_INFO.hidWaterNavigateID.value = document.APP_WATERCRAFT_INFO.hidWaterNavigateID.value + "," + szSelectedDept;
						
					}

				}	
			
			}

		}	
		function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbFUEL_TYPE":
						lookupMessage	=	"FTYCD.";
						break;
					case "cmbHULL_MATERIAL":
						lookupMessage	=	"%HULL.";
						break;
					case "cmbWATERS_NAVIGATED":
						lookupMessage	=	"WNVC.";
						break;	
					case "cmbTYPE_OF_WATERCRAFT":
						lookupMessage	=	"WCTCD.";
						break;	
					case "cmbTWIN_SINGLE":
						lookupMessage	=	"TWSGL.";
						break;											
					default:
						lookupMessage	=	"Look up code not found";
						break;
						
				}
				showLookupLayer(controlId,lookupMessage);							
			}
			
	function Check()
	{
		if (document.getElementById('cmbTYPE_OF_WATERCRAFT').selectedIndex != '-1')		
		{
			if (document.getElementById('cmbTYPE_OF_WATERCRAFT').options[document.getElementById('cmbTYPE_OF_WATERCRAFT').selectedIndex].value == '11439')
			{			
				document.getElementById('txtDESC_OTHER_WATERCRAFT').style.display='inline';
			}		
		}
	
		CheckForMax();
		CheckForWaterNavigated();
		CheckForHullMandatory();
	}	
	
	
	
	
	
	function CheckForHullMandatory()
	{
		//Personal Watercraft -> Jet Ski,Mini Jet Boat,Wave runner
		//For Personal types of Watercraft Hull material field should be non-mandatory.
		//11387 -> Jetski (w/Lift Bar)
		//11495 -> Wave runner with no lift bar
		//11373 -> Mini Jet Boat
		//11390 -> Jet Ski
		//11386 -> Waverunner
		
		var CurrBoatType; 
		
		
		if(document.getElementById("hidCalledFrom").value!="<%=CALLED_FROM_UMBRELLA%>")
		{
			if (document.getElementById('cmbTYPE_OF_WATERCRAFT').selectedIndex != -1)
			{
				CurrBoatType = document.getElementById('cmbTYPE_OF_WATERCRAFT').options[document.getElementById('cmbTYPE_OF_WATERCRAFT').selectedIndex].value;
			
				if (CurrBoatType == '11387' || CurrBoatType == '11495'  || CurrBoatType == '11373' || CurrBoatType == '11390' || CurrBoatType == '11386')
				{
					EnableDisableDesc(document.getElementById('cmbHULL_MATERIAL'),document.getElementById('rfvHULL_MATERIAL'),document.getElementById('spnHULL_MATERIAL'),false);
			
				}
				else
				{	
					EnableDisableDesc(document.getElementById('cmbHULL_MATERIAL'),document.getElementById('rfvHULL_MATERIAL'),document.getElementById('spnHULL_MATERIAL'),true);
			
				}						
			}
		}//if selIndex
	//	if(document.getElementById('hidFormSaved').value!=0)
		//	{Page_ClientValidate();}
	}//function
	
	function CheckForWaterNavigated()
	 {
		if (document.getElementById('cmbTYPE_OF_WATERCRAFT').selectedIndex != '-1')		
		{
			var waterCraft=document.getElementById('cmbTYPE_OF_WATERCRAFT').options[document.getElementById('cmbTYPE_OF_WATERCRAFT').selectedIndex].value;
			if (waterCraft == '11387' || waterCraft == '11495' || waterCraft == '11373' || waterCraft == '11390' || waterCraft == '11386'  )
			{	
				document.getElementById('cmbWATERS_NAVIGATED').options.selectedIndex = 1;
				document.getElementById('cmbWATERS_NAVIGATED').disabled=true;
			}
			else
			{
				document.getElementById('cmbWATERS_NAVIGATED').disabled=false;
			}		
		}
	 }
	
	
	function EnableDisableDesc(txtDesc,rfvDesc,spnDesc,flag)
			{		
				
				if (flag==false)
				{			
					
					if(rfvDesc!=null)
					{
						rfvDesc.setAttribute('enabled',false);					
						rfvDesc.setAttribute('isValid',false);
						rfvDesc.style.display="none";
						spnDesc.style.display = "none";
		
						txtDesc.className = "";
					}					
				}
				else
				{	
					if(rfvDesc!=null)
					{
						rfvDesc.setAttribute('enabled',true);					
						rfvDesc.setAttribute('isValid',true);
						
						spnDesc.style.display = "inline";
												
						txtDesc.className = "MandatoryControl";
			
					}
				}			
			}
			
			
			
		function ShowHideDiscounts(LoadType)
		{
		
			if(document.getElementById('cmbTYPE_OF_WATERCRAFT').selectedIndex==-1)	return;
			BoatType = document.getElementById('cmbTYPE_OF_WATERCRAFT').options[document.getElementById('cmbTYPE_OF_WATERCRAFT').selectedIndex].value;
			DieselEngineDiscount(LoadType);
			document.getElementById("trDiscountsAnsSurcharges").style.display="inline";
			document.getElementById("capLORAN_NAV_SYSTEM").style.display="inline";
			document.getElementById("cmbLORAN_NAV_SYSTEM").style.display="inline";
			document.getElementById("capSHORE_STATION").style.display="inline";
			document.getElementById("cmbSHORE_STATION").style.display="inline";
			document.getElementById("capHALON_FIRE_EXT_SYSTEM").style.display="inline";
			document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").style.display="inline";
			document.getElementById("capDUAL_OWNERSHIP").style.display="inline";
			document.getElementById("cmbDUAL_OWNERSHIP").style.display="inline";
			//If the index has not been set, set it to 1 (N0)
			if(document.getElementById("cmbLORAN_NAV_SYSTEM").selectedIndex==0)
				document.getElementById("cmbLORAN_NAV_SYSTEM").selectedIndex=1;
			
						
			if(document.getElementById("cmbSHORE_STATION").selectedIndex==0)
				document.getElementById("cmbSHORE_STATION").selectedIndex=1;
				
			//RPSingh - commented because saved value was not displayed
			//if(document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").selectedIndex==0)
			//document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").selectedIndex=1;
				
			if(document.getElementById("cmbDUAL_OWNERSHIP").selectedIndex==0)
				document.getElementById("cmbDUAL_OWNERSHIP").selectedIndex=1;
				
			//RPSingh - commented because saved value was not displayed
			//if(document.getElementById("cmbREMOVE_SAILBOAT").selectedIndex==0)			
			//	document.getElementById("cmbREMOVE_SAILBOAT").selectedIndex=1;
			
			
			
			switch(BoatType)
			{
				case "11371"://Inboard
				case "11375"://Pontoon INBOARD/ OUTBOARD
				case "11370": //Inboard / Outboard Boat					
					//document.getElementById("cmbREMOVE_SAILBOAT").selectedIndex=0;
					document.getElementById("capREMOVE_SAILBOAT").style.display="none";
					document.getElementById("cmbREMOVE_SAILBOAT").style.display="none";					
					break;
				/*case "11390": //Jet Ski					
					document.getElementById("capHALON_FIRE_EXT_SYSTEM").style.display="none";
					document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").style.display="none";
					document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").selectedIndex=0;
					document.getElementById("capDUAL_OWNERSHIP").style.display="none";
					document.getElementById("cmbDUAL_OWNERSHIP").style.display="none";
					document.getElementById("cmbDUAL_OWNERSHIP").selectedIndex=0;
					document.getElementById("capREMOVE_SAILBOAT").style.display="none";
					document.getElementById("cmbREMOVE_SAILBOAT").style.display="none"
					document.getElementById("cmbREMOVE_SAILBOAT").selectedIndex=0;
					
					break;*/
				case "11373": //Mini Jet Boat
				    document.getElementById("capLORAN_NAV_SYSTEM").style.display="none";
					document.getElementById("cmbLORAN_NAV_SYSTEM").style.display="none";
					document.getElementById("cmbLORAN_NAV_SYSTEM").selectedIndex=0;
					document.getElementById("capSHORE_STATION").style.display="none";
					document.getElementById("cmbSHORE_STATION").style.display="none";
					document.getElementById("cmbSHORE_STATION").selectedIndex=0;
					document.getElementById("capHALON_FIRE_EXT_SYSTEM").style.display="none";
					document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").style.display="none";
					//document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").selectedIndex=0;
					document.getElementById("capREMOVE_SAILBOAT").style.display="none";
					document.getElementById("cmbREMOVE_SAILBOAT").style.display="none";
					//document.getElementById("cmbREMOVE_SAILBOAT").selectedIndex=0;
					document.getElementById("capDIESEL_ENGINE").style.display="none";
					break;
				case "11386": //Waverunne				
			    case "11387": //Jet Ski
			    case "11390": //Jetski (w/Lift Bar)
			    //Modifed 18 sep 2007 : Dual ownership for Personal types:
				//case "11390": //Jet Ski					
					document.getElementById("capLORAN_NAV_SYSTEM").style.display="none";
					document.getElementById("cmbLORAN_NAV_SYSTEM").style.display="none";
					document.getElementById("cmbLORAN_NAV_SYSTEM").selectedIndex=0;
					document.getElementById("capSHORE_STATION").style.display="none";
					document.getElementById("cmbSHORE_STATION").style.display="none";
					document.getElementById("cmbSHORE_STATION").selectedIndex=0;
					document.getElementById("capHALON_FIRE_EXT_SYSTEM").style.display="none";
					document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").style.display="none";
					//document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").selectedIndex=0;
					document.getElementById("capDUAL_OWNERSHIP").style.display="inline";
					document.getElementById("cmbDUAL_OWNERSHIP").style.display="inline";
					//document.getElementById("cmbDUAL_OWNERSHIP").selectedIndex=0;					
					document.getElementById("capREMOVE_SAILBOAT").style.display="none";
					document.getElementById("cmbREMOVE_SAILBOAT").style.display="none";
					//document.getElementById("cmbREMOVE_SAILBOAT").selectedIndex=0;
					document.getElementById("trDiscountsAnsSurcharges").style.display="inline";
				
					/*document.getElementById("capLORAN_NAV_SYSTEM").style.display="none";
					document.getElementById("cmbLORAN_NAV_SYSTEM").style.display="none";
					document.getElementById("cmbLORAN_NAV_SYSTEM").selectedIndex=0;
					document.getElementById("capDIESEL_ENGINE").style.display="none";
					document.getElementById("cmbDIESEL_ENGINE").style.display="none";
					document.getElementById("cmbDIESEL_ENGINE").selectedIndex=0;
					document.getElementById("capSHORE_STATION").style.display="none";
					document.getElementById("cmbSHORE_STATION").style.display="none";
					document.getElementById("cmbSHORE_STATION").selectedIndex=0;
					document.getElementById("capHALON_FIRE_EXT_SYSTEM").style.display="none";
					document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").style.display="none";					
					document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").selectedIndex=0;
					document.getElementById("capREMOVE_SAILBOAT").style.display="none";
					document.getElementById("cmbREMOVE_SAILBOAT").style.display="none";
					document.getElementById("cmbREMOVE_SAILBOAT").selectedIndex=0;*/					
					break;
				case "11374": //Pontoon (w/Motor)</option>
					document.getElementById("capHALON_FIRE_EXT_SYSTEM").style.display="none";
					document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").style.display="none";
					//document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").selectedIndex=0;
					document.getElementById("capREMOVE_SAILBOAT").style.display="none";
					document.getElementById("cmbREMOVE_SAILBOAT").style.display="none";
					//document.getElementById("cmbREMOVE_SAILBOAT").selectedIndex=0;
					break;
				case "11372": //Sailboat
				case "11672": //Sailboat w/outboard //Modified on 18 sep 2007
					document.getElementById("capHALON_FIRE_EXT_SYSTEM").style.display="inline";
					document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").style.display="inline";
					//document.getElementById("capHALON_FIRE_EXT_SYSTEM").style.display="none";
					//document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").style.display="none";
					//document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").selectedIndex=1;
				 	//RPSingh - commented because saved value was not displayed
					//document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").selectedIndex=1;
			     	document.getElementById("capREMOVE_SAILBOAT").style.display="inline";
					document.getElementById("cmbREMOVE_SAILBOAT").style.display="inline";
					//RPSingh - commented because saved value was not displayed
					//document.getElementById("cmbREMOVE_SAILBOAT").selectedIndex=1;
					break;
			case "11488"://Ski Boat
			case "11489"://Bass Boat
			        document.getElementById("capHALON_FIRE_EXT_SYSTEM").style.display="none";
					//document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").selectedIndex=0;
					document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").style.display="none";
					document.getElementById("capREMOVE_SAILBOAT").style.display="none";
					document.getElementById("cmbREMOVE_SAILBOAT").style.display="none";
					//document.getElementById("cmbREMOVE_SAILBOAT").selectedIndex=0;
					
					break;
				case "11376": // Canoe
				case "11377": // Rowboat
				case "11756": // Paddleboat
				case "11755": // Pontoon Outboard
				case "11757": // Jet Drive outboards
				case "11369": //Outboard Boat
				case "11487": //Outboard Boat(W/Motor)
					document.getElementById("capHALON_FIRE_EXT_SYSTEM").style.display="none";
					document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").style.display="none";					
					//document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").selectedIndex=0;
					document.getElementById("capREMOVE_SAILBOAT").style.display="none";
					document.getElementById("cmbREMOVE_SAILBOAT").style.display="none";
					//document.getElementById("cmbREMOVE_SAILBOAT").selectedIndex=0;
					break;
				case "11445": //Jetski Trailer
				case "11446": //WaveRunner Trailer				
					document.getElementById("capLORAN_NAV_SYSTEM").style.display="inline";
					document.getElementById("cmbLORAN_NAV_SYSTEM").style.display="inline";
					document.getElementById("cmbLORAN_NAV_SYSTEM").selectedIndex=1;
					document.getElementById("capSHORE_STATION").style.display="inline";
					document.getElementById("cmbSHORE_STATION").style.display="inline";
					document.getElementById("cmbSHORE_STATION").selectedIndex=1;
					document.getElementById("capHALON_FIRE_EXT_SYSTEM").style.display="inline";
					document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").style.display="inline";
					//RPSingh - commented because saved value was not displayed
					//document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").selectedIndex=1;
					document.getElementById("capDUAL_OWNERSHIP").style.display="inline";
					document.getElementById("cmbDUAL_OWNERSHIP").style.display="inline";
					document.getElementById("cmbDUAL_OWNERSHIP").selectedIndex=1;
					break;		
			
				/*case "11387":        //Jetski (w/Lift Bar)
				    document.getElementById("capLORAN_NAV_SYSTEM").style.display="inline";
					document.getElementById("cmbLORAN_NAV_SYSTEM").style.display="inline";
					document.getElementById("cmbLORAN_NAV_SYSTEM").selectedIndex=1;
					document.getElementById("capDIESEL_ENGINE").style.display="inline";
					document.getElementById("cmbDIESEL_ENGINE").style.display="inline";
					document.getElementById("cmbDIESEL_ENGINE").selectedIndex=1;
					document.getElementById("capSHORE_STATION").style.display="inline";
					document.getElementById("cmbSHORE_STATION").style.display="inline";
					document.getElementById("cmbSHORE_STATION").selectedIndex=1;
					document.getElementById("capHALON_FIRE_EXT_SYSTEM").style.display="inline";
					document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").style.display="inline";
					document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").selectedIndex=1;
					document.getElementById("capHALON_FIRE_EXT_SYSTEM").style.display="none";
					document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").style.display="none";					
					document.getElementById("cmbHALON_FIRE_EXT_SYSTEM").selectedIndex=0;
					document.getElementById("capREMOVE_SAILBOAT").style.display="none";
					document.getElementById("cmbREMOVE_SAILBOAT").style.display="none";
					document.getElementById("cmbREMOVE_SAILBOAT").selectedIndex=0;
					document.getElementById("capDUAL_OWNERSHIP").style.display="none";
					document.getElementById("cmbDUAL_OWNERSHIP").style.display="none";
					document.getElementById("cmbDUAL_OWNERSHIP").selectedIndex=0;
					break;*/
					
			}
			RemoveSailboat();
		}
			
	function CheckForMax()
	{
         var SelectedValue=-1;	
                    
			if(document.getElementById('hidCalledFrom').value=="Wat" || document.getElementById('hidCalledFrom').value =="WAT")	
     		{
	  			if (document.getElementById('cmbTYPE_OF_WATERCRAFT').selectedIndex != '-1')		
				{  
					SelectedValue=document.getElementById('cmbTYPE_OF_WATERCRAFT').options[document.getElementById('cmbTYPE_OF_WATERCRAFT').selectedIndex].value;
				  
				  	if(SelectedValue == '11490' || SelectedValue == '11497' || SelectedValue == '11496' || SelectedValue == '11369')
					{
					    
						EnableDisableDesc(document.getElementById('txtMAX_SPEED'),document.getElementById('rfvMAX_SPEED'),document.getElementById('spnMAX_SPEED'),false);			
										
					}
					else
					{
					
						EnableDisableDesc(document.getElementById('txtMAX_SPEED'),document.getElementById('rfvMAX_SPEED'),document.getElementById('spnMAX_SPEED'),true);			
						
					
					}		 
				}
              }

	}
	function ShowHideFieldsForUmb()
	{
		if(document.getElementById("hidCalledFrom").value=="<%=CALLED_FROM_UMBRELLA%>")
		{
			document.getElementById("trSerial").style.display="none";
			EnableValidator("rfvSTATE_REG",false);
			EnableValidator("revSERIAL_NO",false);
			
			document.getElementById("tbFuel").style.display="none";
			EnableValidator("rfvTERRITORY",false);
			EnableValidator("rfvHULL_MATERIAL",false);
			EnableValidator("revDATE_PURCHASED",false);
			//EnableValidator("rfvWATERS_NAVIGATED",false);
			EnableValidator("csvDATE_PURCHASED",false);
			document.getElementById("capCOV_TYPE_BASIS").style.display="none";
			document.getElementById("cmbCOV_TYPE_BASIS").style.display="none";
			document.getElementById("spnCOV_TYPE_BASIS").style.display="none";
			EnableValidator("rfvCOV_TYPE_BASIS",false);
			
			document.getElementById("tbLocationUsedStored").style.display="none";
			EnableValidator("rfvLOCATION_ADDRESS",false);
			EnableValidator("rfvLOCATION_CITY",false);
			EnableValidator("rfvLOCATION_STATE",false);
			EnableValidator("rfvLOCATION_ZIP",false);
			EnableValidator("revLOCATION_ZIP",false);
			
			document.getElementById("tbLay").style.display="none";
			EnableValidator("csvDATE_MARINE_SURVEY",false);
			EnableValidator("revDATE_MARINE_SURVEY",false);
			
			document.getElementById("tbDiscountsAnsSurcharges").style.display="none";
			
			document.getElementById("trCOV_TYPE_BASIS").style.display="none";
			document.getElementById("trWATERS_NAVIGATED").style.display="none";			
			EnableValidator("rfvINSURING_VALUE",false);
			EnableValidator("revINSURING_VALUE",false);
			EnableValidator("rngINSURING_VALUE",false);
			EnableValidator("rfvWATERS_NAVIGATED",false);
		}
		else
		{
			document.getElementById("trUmbRow").style.display="none";
			document.getElementById("trUSED_PARTICIPATE").style.display="none";
			EnableValidator("rfvUSED_PARTICIPATE",false);
			EnableValidator("rfvWATERCRAFT_CONTEST",false);	
		}
	}	
	function USED_PARTICIPATE_Change()
	{
		combo = document.getElementById("cmbUSED_PARTICIPATE");
		if(combo!=null && combo.selectedIndex!=-1 && combo.options[combo.selectedIndex].value=="<%=((int)enumYESNO_LOOKUP_UNIQUE_ID.YES).ToString()%>") //Yes
		{
			document.getElementById("txtWATERCRAFT_CONTEST").style.display="inline";
			document.getElementById("capWATERCRAFT_CONTEST").style.display="inline";
			document.getElementById("spnWATERCRAFT_CONTEST").style.display="inline";
			EnableValidator("rfvWATERCRAFT_CONTEST",true);
		}
		else
		{
			document.getElementById("txtWATERCRAFT_CONTEST").style.display="none";
			document.getElementById("capWATERCRAFT_CONTEST").style.display="none";
			document.getElementById("spnWATERCRAFT_CONTEST").style.display="none";
			EnableValidator("rfvWATERCRAFT_CONTEST",false);
		}
	}
	function Init()
	{
		populateXML();		
		ShowMarineDate();
		ShowHideFieldsForUmb();
		USED_PARTICIPATE_Change();
		ApplyColor();
		ChangeColor();		
	}
					
		</script>
</HEAD>
	<BODY leftMargin='0' topMargin='0' onload="Init();">
		<FORM id='APP_WATERCRAFT_INFO' method='post' runat='server'>
		
			<TABLE cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow></TD>
								</tr>
								<tr>
									<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
										mandatory</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capBOAT_NO" runat="server">Boat Number</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtBOAT_NO" runat="server" size="6" maxlength="4" readonly="false"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvBOAT_NO" runat="server" ControlToValidate="txtBOAT_NO" ErrorMessage="BOAT_NO can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revBOAT_NO" runat="server" ControlToValidate="txtBOAT_NO" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capMAX_SPEED" runat="server">Maximum Speed</asp:label><span class="mandatory" id="spnMAX_SPEED">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMAX_SPEED" runat="server" size="5" maxlength="7"></asp:textbox><BR>
										<asp:RequiredFieldValidator ID="rfvMAX_SPEED" ControlToValidate="txtMAX_SPEED" Runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
										<asp:regularexpressionvalidator id="revMAX_SPEED" runat="server" ControlToValidate="txtMAX_SPEED" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" style="HEIGHT: 24px" width="18%"><asp:label id="capTYPE_OF_WATERCRAFT" runat="server">Type of Watercraft</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" style="HEIGHT: 24px" width="32%"><asp:dropdownlist id="cmbTYPE_OF_WATERCRAFT" onfocus="SelectComboIndex('cmbTYPE_OF_WATERCRAFT')" runat="server"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbTYPE_OF_WATERCRAFT')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
										<br>
										<asp:requiredfieldvalidator id="rfvTYPE_OF_WATERCRAFT" runat="server" ControlToValidate="cmbTYPE_OF_WATERCRAFT"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capYEAR" runat="server">Year</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtYEAR" runat="server" size="4" maxlength="4" onblur="WatercraftAge();"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvYEAR" runat="server" ControlToValidate="txtYEAR" ErrorMessage="YEAR can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator>
										<asp:rangevalidator id="rngYEAR" ControlToValidate="txtYEAR" Display="Dynamic" Type="Integer" Runat="server"
											MinimumValue="1900"></asp:rangevalidator>
											<span id="spnErrMessageAgreedValueAge" style="DISPLAY:none;COLOR:red"> <%=strErrMessageAgreedValueAge %> </span>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMAKE" runat="server">Make</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMAKE" runat="server" size="20" maxlength="70"></asp:textbox><BR>
									<asp:RequiredFieldValidator ID="rfvMAKE" Runat="server" Display="Dynamic" ControlToValidate="txtMAKE"></asp:RequiredFieldValidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capMODEL" runat="server">Model</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtMODEL" runat="server" size="20" maxlength="70"></asp:textbox><BR>
									</TD>
								</tr>
								<tr id="trSerial">
									<TD class="midcolora" width="18%"><asp:label id="capSERIAL_NO" runat="server">Serial No.</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtSERIAL_NO" runat="server" size="20" maxlength="75"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revSERIAL_NO" runat="server" ControlToValidate="txtSERIAL_NO" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capSTATE_REG" runat="server">State Registered</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSTATE_REG" onfocus="SelectComboIndex('cmbSTATE_REG')" runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvSTATE_REG" runat="server" ControlToValidate="cmbSTATE_REG" Display="Dynamic"></asp:requiredfieldvalidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capLENGTH" runat="server">Length</asp:label><span class="mandatory" id="spnLength">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtLENGTH" runat="server" maxlength="4" size="7" onblur="RemoveSailboat();WatercraftAge();"></asp:textbox>
										<asp:label id="capFEET" runat="server">Feet</asp:label>
										<asp:textbox id="txtINCHES" runat="server"  maxlength="2" size="3"  onblur="RemoveSailboat();WatercraftAge();"></asp:textbox>
										<asp:label id="capINCHES" runat="server">Inches</asp:label>
										<br>
										<asp:requiredfieldvalidator id="rfvLENGTH" runat="server" Display="Dynamic" ErrorMessage="LENGTH can't be blank." ControlToValidate="txtLENGTH"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revLENGTH" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtLENGTH"></asp:regularexpressionvalidator>
										<asp:RangeValidator ID="rngLENGTH" Runat="server" ControlToValidate="txtLENGTH" Display="Dynamic" MaximumValue="26" Enabled="False" MinimumValue="0" Type="Integer"></asp:RangeValidator>
										<asp:regularexpressionvalidator id="revINCHES" runat="server" Display="Dynamic" ControlToValidate="txtINCHES"></asp:regularexpressionvalidator>
										<asp:rangevalidator id="rngINCHES" runat="server" Display="Dynamic" ControlToValidate="txtINCHES" MinimumValue="1" Type="Integer" MaximumValue="11"></asp:rangevalidator>
										<span id="spnErrMessageAgreedValueLength" style="DISPLAY:none;COLOR:red"> <%=strErrMessageAgreedValueLength %> </span>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capWATERCRAFT_HORSE_POWER" runat="server">Horsepower/CC</asp:label><span class="mandatory" id="spnWATERCRAFT_HORSE_POWER">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtWATERCRAFT_HORSE_POWER" runat="server" maxlength="5" size="7"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvWATERCRAFT_HORSE_POWER" runat="server" Display="Dynamic" ErrorMessage="Please enter Horsepower."
											ControlToValidate="txtWATERCRAFT_HORSE_POWER"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revWATERCRAFT_HORSE_POWER" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtWATERCRAFT_HORSE_POWER"></asp:regularexpressionvalidator>
									</TD>
								</tr>
							<tbody id="tbFuel">
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capTWIN_SINGLE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbTWIN_SINGLE" onfocus="SelectComboIndex('cmbTWIN_SINGLE')" runat="server"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbTWIN_SINGLE')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capHULL_MATERIAL" runat="server">Hull Material</asp:label>
									<span id="spnHULL_MATERIAL" class="mandatory">*</span>
									</TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbHULL_MATERIAL" onfocus="SelectComboIndex('cmbHULL_MATERIAL')" runat="server"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbHULL_MATERIAL')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
										<BR>
										<asp:RequiredFieldValidator ID="rfvHULL_MATERIAL" Runat="server" ControlToValidate="cmbHULL_MATERIAL" Display="Dynamic"></asp:RequiredFieldValidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" style="HEIGHT: 24px" width="18%"><asp:label id="capFUEL_TYPE" runat="server">Fuel Type</asp:label></TD>
									<TD class="midcolora" style="HEIGHT: 24px" width="32%"><asp:dropdownlist id="cmbFUEL_TYPE" onfocus="SelectComboIndex('cmbFUEL_TYPE')" runat="server"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbFUEL_TYPE')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capTERRITORY" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbTERRITORY" onfocus="SelectComboIndex('cmbTERRITORY')" runat="server"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvTERRITORY" runat="server" ControlToValidate="cmbTERRITORY" Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDATE_PURCHASED" runat="server">Date Purchased</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDATE_PURCHASED" runat="server" size="11" maxlength="10"></asp:textbox><asp:hyperlink id="hlkPURCHASE_DATE" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgPURCHASE_DATE" runat="server" ImageUrl="../../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
										</asp:hyperlink><BR>
										<asp:regularexpressionvalidator id="revDATE_PURCHASED" runat="server" ControlToValidate="txtDATE_PURCHASED" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvDATE_PURCHASED" ControlToValidate="txtDATE_PURCHASED" Display="Dynamic" Runat="server"
											ClientValidationFunction="CheckDateValidity"></asp:customvalidator></TD>
									<td class="midcolora" colspan="2"></td>
								</tr>
							</tbody>
								<tr id="trWATERS_NAVIGATED">
									<TD class="midcolora" width="18%"><asp:label id="capWATERS_NAVIGATED" runat="server">Waters Navigated</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbWATERS_NAVIGATED" runat="server" onfocus="SelectComboIndex('cmbWATERS_NAVIGATED')"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbWATERS_NAVIGATED')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
										<BR>
										<asp:requiredfieldvalidator id="rfvWATERS_NAVIGATED" runat="server" ControlToValidate="cmbWATERS_NAVIGATED"
											ErrorMessage="WATERS_NAVIGATED can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>							
								
									<TD class="midcolora" width="18%"><asp:label id="capINSURING_VALUE" runat="server">Insuring Value</asp:label><span class="mandatory" id="spnINSURANCE_VALUE">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox class="INPUTCURRENCY" id="txtINSURING_VALUE" onfocus="RightAlign(this);"   onblur="WatercraftAge();this.value=formatCurrency(this.value);" runat="server"
											size="10" maxlength="6"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revINSURING_VALUE" runat="server" ControlToValidate="txtINSURING_VALUE" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator><asp:requiredfieldvalidator id="rfvINSURING_VALUE" runat="server" ControlToValidate="txtINSURING_VALUE" Display="Dynamic"></asp:requiredfieldvalidator>
										<asp:RangeValidator ID="rngINSURING_VALUE" Runat="server" ControlToValidate="txtINSURING_VALUE" MinimumValue="0"
											MaximumValue="75000" Display="Dynamic" Type="Currency" Enabled=False></asp:RangeValidator>
											<span id="spnErrMessageAgreedValueInsuredVal" style="DISPLAY:none;COLOR:red"> <%=strErrMessageAgreedValueInsuredVal %> </span>
									</TD>
								</tr>
								<tr id="trCOV_TYPE_BASIS">
									<td class="midcolora" width="18%">
										<asp:label id="capCOV_TYPE_BASIS" runat="server">Coverage Type Basis</asp:label><span class="mandatory" id="spnCOV_TYPE_BASIS">*</span></td>
									<td class="midcolora" width="32%">
										<asp:dropdownlist id="cmbCOV_TYPE_BASIS" onfocus="SelectComboIndex('cmbCOV_TYPE_BASIS')" runat="server" OnChange="WatercraftAge();" OnClick="WatercraftAge();"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvCOV_TYPE_BASIS" runat="server" Display="Dynamic" ControlToValidate="cmbCOV_TYPE_BASIS"
											ErrorMessage="Custom Error"></asp:requiredfieldvalidator>
									</td>
									<td colspan="2" class="midcolora"></td>
								</tr>
								<tr id="trUmbRow">
									<TD class="midcolora" width="18%"><asp:label id="capOTHER_POLICY" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbOTHER_POLICY" onfocus="SelectComboIndex('cmbOTHER_POLICY')" runat="server">										
										</asp:dropdownlist></td>									
									<TD class="midcolora" width="18%"><asp:label id="capIS_BOAT_EXCLUDED" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbIS_BOAT_EXCLUDED" onfocus="SelectComboIndex('cmbIS_BOAT_EXCLUDED')" runat="server">										
										</asp:dropdownlist></td>
									
										
								</tr>
								<!--start-->
							<tbody id="tbLay">
								<tr>
									<TD class="midcolora" width="18%">
										<asp:label id="capLAY_UP_PERIOD_FROM" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%">
										<asp:dropdownlist id="cmbLAY_UP_PERIOD_FROM_MONTH" runat="server" onchange="ShowDatesFromMonth()"
											Width="45"></asp:dropdownlist>
										&nbsp;
										<asp:dropdownlist id="cmbLAY_UP_PERIOD_FROM_DAY" runat="server" Width="40"></asp:dropdownlist>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capLAY_UP_PERIOD_TO" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%">
										<asp:dropdownlist id="cmbLAY_UP_PERIOD_TO_MONTH" runat="server" onchange="ShowDatesToMonth()" Width="45"></asp:dropdownlist>
										&nbsp;
										<asp:dropdownlist id="cmbLAY_UP_PERIOD_TO_DAY" runat="server" Width="40"></asp:dropdownlist>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMARINE_SURVEY" runat="server">MARINE SURVEY</asp:label></TD>
									<td class="midcolora" width="32%"><asp:DropDownList ID="cmbMARINE_SURVEY" Runat="server" onChange="ShowMarineDate();"></asp:DropDownList></td>
									<td class="midcolora" width="18%"><asp:Label ID="capDATE_MARINE_SURVEY" Runat="server">DATE_MARINE_SURVEY</asp:Label></td>
									<TD class="midcolora" width="32%">
										<asp:textbox id="txtDATE_MARINE_SURVEY" runat="server" maxlength="10" size="11"></asp:textbox><asp:hyperlink id="hlkDATE_MARINE_SURVEY" runat="server" CssClass="HotSpot">
											<asp:image id="imgMARINE_SURVEY" runat="server" ImageUrl="../../../CmsWeb/Images/CalendarPicker.gif"></asp:image>
										</asp:hyperlink><BR>
										<asp:regularexpressionvalidator id="revDATE_MARINE_SURVEY" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtDATE_MARINE_SURVEY"></asp:regularexpressionvalidator><asp:customvalidator id="csvDATE_MARINE_SURVEY" Display="Dynamic" ControlToValidate="txtDATE_MARINE_SURVEY"
											Runat="server" ClientValidationFunction="CheckMarineDateValidity"></asp:customvalidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capPHOTO_ATTACHED" runat="server">Photo Attached</asp:label></TD>
									<TD class="midcolora" width="32%" colspan="3"><asp:dropdownlist id="cmbPHOTO_ATTACHED" runat="server"></asp:dropdownlist></TD>
								</tr>
							</tbody>
								<tr id="trUSED_PARTICIPATE">
									<TD class="midcolora" width="18%"><asp:label id="capUSED_PARTICIPATE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbUSED_PARTICIPATE" onfocus="SelectComboIndex('cmbUSED_PARTICIPATE')" runat="server"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvUSED_PARTICIPATE" runat="server" ControlToValidate="cmbUSED_PARTICIPATE"
											ErrorMessage="USED_PARTICIPATE_VEH can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="18%"><asp:label id="capWATERCRAFT_CONTEST" runat="server"></asp:label><span class="mandatory" id="spnWATERCRAFT_CONTEST">*</span></td>
									<td class="midcolora" width="32%"><asp:textbox id="txtWATERCRAFT_CONTEST" runat="server" size="40" MaxLength="250"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvWATERCRAFT_CONTEST" runat="server" ControlToValidate="txtWATERCRAFT_CONTEST"
											Display="Dynamic"></asp:requiredfieldvalidator></td>
								</tr>
							<tbody id="tbLocationUsedStored">
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4" id="trLocationUsedStored">
										Location Used / Stored
									</TD>
								</tr>
								<tr>
									<td class="midcolora" width="18%">
										<asp:label id="capPullCustomerAddress" runat="server">
												Would you like to pull customer address
											</asp:label>
									</td>
									<td class="midcolora" width="32%"><cmsb:cmsbutton class="clsButton" id="btnPullCustomerAddress" runat="server" Text="Pull Customer Address"></cmsb:cmsbutton></td>
									<TD class="midcolora" colspan="2">&nbsp;</TD>
								</tr>							
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capLOCATION_ADDRESS" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtLOCATION_ADDRESS" runat="server" maxlength="150" Width="225px"></asp:textbox>
										<br>
										<asp:requiredfieldvalidator id="rfvLOCATION_ADDRESS" Runat="server" Display="Dynamic" ControlToValidate="txtLOCATION_ADDRESS"></asp:requiredfieldvalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capLOCATION_CITY" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtLOCATION_CITY" runat="server" maxlength="35"></asp:textbox>
										<br>
										<asp:requiredfieldvalidator id="rfvLOCATION_CITY" Runat="server" Display="Dynamic" ControlToValidate="txtLOCATION_CITY"></asp:requiredfieldvalidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capLOCATION_STATE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOCATION_STATE" runat="server"></asp:dropdownlist>
										<br>
										<asp:requiredfieldvalidator id="rfvLOCATION_STATE" Runat="server" Display="Dynamic" ControlToValidate="cmbLOCATION_STATE"></asp:requiredfieldvalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capLOCATION_ZIP" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtLOCATION_ZIP" runat="server" maxlength="10"></asp:textbox>
										<br>
										<asp:requiredfieldvalidator id="rfvLOCATION_ZIP" Runat="server" Display="Dynamic" ControlToValidate="txtLOCATION_ZIP"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revLOCATION_ZIP" Runat="server" Display="Dynamic" ControlToValidate="txtLOCATION_ZIP"></asp:regularexpressionvalidator>
										<asp:CustomValidator Enabled=False ID="csvLOCATION_ZIP" ClientValidationFunction="ChkResult" Runat="server" ControlToValidate="txtLOCATION_ZIP" Display="Dynamic" ErrorMessage="Zip does not belong to the specifed state."></asp:CustomValidator>
									</TD>
								</tr>
							</tbody>
							<tbody id="tbDiscountsAnsSurcharges">
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4" id="trDiscountsAnsSurcharges">Discounts 
										and Surcharges</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capSHORE_STATION" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSHORE_STATION" runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capHALON_FIRE_EXT_SYSTEM" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbHALON_FIRE_EXT_SYSTEM" runat="server"></asp:dropdownlist></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDUAL_OWNERSHIP" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDUAL_OWNERSHIP" runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capREMOVE_SAILBOAT" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREMOVE_SAILBOAT" runat="server"></asp:dropdownlist></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%" colspan="3"><asp:label id="capLORAN_NAV_SYSTEM" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLORAN_NAV_SYSTEM" runat="server"></asp:dropdownlist>
										<div id="1" style="DISPLAY:none"><asp:label id="capDIESEL_ENGINE" runat="server"></asp:label><asp:dropdownlist id="cmbDIESEL_ENGINE" runat="server"></asp:dropdownlist></div>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capLOSSREPORT_ORDER" runat="server">Property Loss Report Ordered</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOSSREPORT_ORDER" onfocus="SelectComboIndex('cmbLOSSREPORT_ORDER')" runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capLOSSREPORT_DATETIME" runat="server">Date Ordered</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtLOSSREPORT_DATETIME" runat="server" size="11" maxlength="10"></asp:textbox><asp:hyperlink id="hlkCalandarDate2" runat="server" CssClass="HotSpot">
									<ASP:IMAGE id="imgCalenderExp2" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
									</asp:hyperlink><br>
									<asp:regularexpressionvalidator id="revLOSSREPORT_DATETIME" runat="server" ControlToValidate="txtLOSSREPORT_DATETIME" ErrorMessage="Please check format of date."
									Display="Dynamic"></asp:regularexpressionvalidator></TD>
								</tr>
							</tbody>
								<tr>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" Causesvalidation="false"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" causesvalidation="false"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text=""
											causesValidation="false"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save" ></cmsb:cmsbutton>
									
									
									</td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
			<INPUT id="hidAppID" type="hidden" value="0" name="hidAppID" runat="server"> <INPUT id="hidAppVersionID" type="hidden" value="0" name="hidAppVersionID" runat="server">
			<INPUT id="hidBOAT_ID" type="hidden" value="0" name="hidBOAT_ID" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
			<INPUT id="hidWaterNavigateID" type="hidden" name="hidWaterNavigateID" runat="server">
			<INPUT id="hidAPP_LOB" type="hidden" name="hidAPP_LOB" runat="server"> <INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
			<INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
			<INPUT id="hidAlert" type="hidden" name="hidAlert" runat="server" value="0"> <INPUT id="hidBoatAge" type="hidden" name="hidBoatAge" runat="server">
			<INPUT id="hidCustomerAddress" type="hidden" name="hidCustomerAddress" runat="server">
			<INPUT id="hidCustomerCity" type="hidden" name="hidCustomerCity" runat="server">
			<INPUT id="hidCustomerState" type="hidden" name="hidCustomerState" runat="server">
			<INPUT id="hidCustomerZip" type="hidden" name="hidCustomerZip" runat="server">
			<INPUT id="hidPolEffDate" type="hidden" name="hidPolEffDate" runat="server"> 
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none; Z-INDEX: 101; POSITION: absolute"><iframe id="lookupLayerIFrame" style="DISPLAY: none; Z-INDEX: 1001; FILTER: alpha(opacity=0); BACKGROUND-COLOR: #000000"
				width="0" height="0" top="0px;" left="0px"></iframe>
		</div>
		<DIV id="lookupLayer" onmouseover="javascript:refreshLookupLayer();" style="Z-INDEX: 101; VISIBILITY: hidden; POSITION: absolute">
			<table class="TabRow" cellSpacing="0" cellPadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b>Add LookUp</b></td>
					<td><p align="right"><A onclick="javascript:hideLookupLayer();" href="javascript:void(0)"><img src="/cms/cmsweb/images/cross.gif" border="0" alt="Close" WIDTH="16" HEIGHT="14"></A></p>
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
				document.getElementById('hidBOAT_ID').value = "NEW";				
			}
			else
			{			
				this.parent.strSelectedRecordXML = "-1";
				RefreshWebGrid(document.getElementById('hidFormSaved').value,TrimTheString(document.getElementById('hidBOAT_ID').value));
				
			}
		</script>
	</BODY>
</HTML>

<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" Codebehind="UmbDriverDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.UmbDriverDetails" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>APP_DRIVER_DETAILS</title>
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
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
			
			
			function SaveClientSide()
			{
				/*var intTotalRows = parseInt(document.getElementById("tblAssignedVeh").getAttribute("TotalRows"));
				var SelectedData="";				
				if(document.getElementById("tbAssignVehSec").style.display=="none")
				{
					 document.getElementById("hidSeletedData").value="";
					 return;
				}
				for(var i=1 ; i<=intTotalRows ; i++)
				{
					if (SelectedData == "")
						SelectedData = document.getElementById('ID_' + i).getAttribute("RowVehID") + "~" + document.getElementById('ID_' + i).getElementsByTagName("td")[3].childNodes[0].value;
					else
						SelectedData = SelectedData + "|" + document.getElementById('ID_' + i).getAttribute("RowVehID") + "~" + document.getElementById('ID_' + i).getElementsByTagName("td")[3].childNodes[0].value;
				}
				
				 document.getElementById("hidSeletedData").value=SelectedData;	*/
				 Page_ClientValidate();				 
				 CompareExpDateWithDOB();				 
				 return Page_IsValid;
			}
			
			
		//Function for drawing the tabs
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
			if(document.getElementById('hidDataValue1').value=='undefined')
				document.getElementById('hidDataValue1').value="";
			if(document.getElementById('hidDataValue2').value=='undefined')
				document.getElementById('hidDataValue2').value="";
			document.getElementById('hidCustomInfo').value=";Driver Name = " + document.getElementById('hidDataValue1').value + ";Driver Code = " + document.getElementById('hidDataValue2').value;						
			
		}
		
		function SetTab()
		{
			if((document.getElementById('hidFormSaved').value == '1') || (document.getElementById('hidOldData').value != ""))
			{
				Url="AddMvrInformationIndex.aspx?CUSTOMER_ID=" + document.getElementById("hidCUSTOMER_ID").value 
					+ "&APP_ID=" + document.getElementById("hidPolicyId").value 
					+ "&APP_VERSION_ID=" + document.getElementById("hidPolicyVersionId").value
					+ "&CALLEDFROM=" + document.getElementById("hidCalledFrom").value + "&";
				
				DrawTab(2,top.frames[1],'MVR Information',Url);
				var CalledFrom ='';
				if (document.getElementById('hidCalledFrom')!=null)
				{
					CalledFrom=document.getElementById('hidCalledFrom').value;
				}
			}
			else
			{	
				RemoveTab(3,top.frames[1]);						
				RemoveTab(2,top.frames[1]);
			}			
		}
		
		function AddData()
		{
			document.getElementById('txtDRIVER_FNAME').value  = '';
			document.getElementById('txtDRIVER_MNAME').value  = '';
			document.getElementById('txtDRIVER_LNAME').value  = '';
			document.getElementById('txtDRIVER_DOB').value  = '';
			document.getElementById('txtDRIVER_SSN').value  = '';
			document.getElementById('cmbDRIVER_MART_STAT').options.selectedIndex = -1;
			document.getElementById('cmbDRIVER_SEX').options.selectedIndex = -1;
			document.getElementById('txtDRIVER_DRIV_LIC').value  = '';
			document.getElementById('cmbDRIVER_LIC_STATE').options.selectedIndex = -1;
			document.getElementById('txtDATE_LICENSED').value  = '';
			document.getElementById('cmbDRIVER_DRIV_TYPE').options.selectedIndex = -1;
			if(document.getElementById('btnActivateDeactivate'))
				document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			document.getElementById('cmbVEHICLE_ID').options.selectedIndex = -1;
			document.getElementById('cmbFORM_F95').options.selectedIndex = -1;	
			document.getElementById('hidDRIVER_ID').value	=	'New';
			ChangeColor();
			DisableValidators();
			document.getElementById('txtDRIVER_FNAME').focus();
		}
		
		function populateXML()
		{

			if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1' )
			{ 
				if(document.getElementById('hidOldData').value != "")
				{
					if(document.getElementById('btnActivateDeactivate'))
					document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
				
					if(document.getElementById('btnDelete')!=null)
						document.getElementById('btnDelete').setAttribute('disabled',false); 					
					populateFormData(document.getElementById('hidOldData').value,APP_DRIVER_DETAILS);
				}
				else
				{
					AddData();
					
				}
					populateInfo();
			}
	
			CheckForDriverType();			
			return false;
		}
		
		function ChkDOB(objSource , objArgs)
		{
			var expdate=document.APP_DRIVER_DETAILS.txtDRIVER_DOB.value;
			objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);
		}	
				
		function ChkEXPDate(objSource , objArgs)
		{
			var expdate=document.APP_DRIVER_DETAILS.txtDATE_LICENSED.value;
			objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);
		}
		
		function ChkDate(objSource , objArgs)
		{
			var expdate=document.APP_DRIVER_DETAILS.txtDISC_DATE.value;
			objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);
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
					nodeValue = tree.childNodes[i].firstChild.text;
				}				
				switch(nodeName)
				{
				case "CUSTOMER_FIRST_NAME":
					document.getElementById('txtDRIVER_FNAME').value = nodeValue;
					break;
				case "CUSTOMER_MIDDLE_NAME":
					 document.getElementById('txtDRIVER_MNAME').value = nodeValue;
					 break;
				case "CUSTOMER_LAST_NAME":	
					 document.getElementById('txtDRIVER_LNAME').value = nodeValue;
					 break;	
				case "DATE_OF_BIRTH":
					  document.getElementById('txtDRIVER_DOB').value = nodeValue;
					  break;	
				case "MARITAL_STATUS":
					  document.getElementById('cmbDRIVER_MART_STAT').value = nodeValue;
					  break;	
				 case "SSN_NO":
					  document.getElementById('txtDRIVER_SSN').value = nodeValue;
					  break;	
				 }	
			}		
				ChangeColor();
				DisableValidators();
				EnableDisableControls();	
				return false;
							
		}	

		function CoypApplicationDrivers()
		{			
			var customerid = '';
			var appid='';
			var appversionid='';
			var calledfrom='';
			if (document.getElementById('hidCUSTOMER_ID')!=null)
			{				
				customerid = document.getElementById('hidCUSTOMER_ID').value;
			}
			if (document.getElementById('hidPolicyId')!=null)
			{
				appid = document.getElementById('hidPolicyId').value;
			}
			if (document.getElementById('hidPolicyVersionId')!=null)
			{
				appversionid = document.getElementById('hidPolicyVersionId').value;
			}
			if (document.getElementById('hidCalledFrom')!=null)
			{
				calledfrom = document.getElementById('hidCalledFrom').value;
			}
			var clFrom='<%=strCalledFrom%>';
			window.open('AddCurrentAppExistingDriver.aspx?CalledFrom='+ clFrom +'&CUSTOMER_ID='+customerid+'&APP_ID='+appid+'&APP_VERSION_ID='+appversionid ,'DriverDetails',600,300,'Yes','Yes','No','No','No');	
			return false;
		}
		function AddLookUpValue(lookupname)
		{	
			window.open('/cms/cmsweb/maintenance/AddLookup.aspx?LookUpName=' +lookupname+ '&CalledForEnter=true','DriverDetails',600,300,'Yes','Yes','No','No','No');	
			return false;
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
		function CompareExpDateWithDOB()
		{
			document.getElementById("txtDATE_LICENSED").value = FormatDateForGrid(document.getElementById("txtDATE_LICENSED"),'');
			document.getElementById("txtDRIVER_DOB").value = FormatDateForGrid(document.getElementById("txtDRIVER_DOB"),'');
			var dob=document.APP_DRIVER_DETAILS.txtDRIVER_DOB.value;
			var expdate=document.APP_DRIVER_DETAILS.txtDATE_LICENSED.value;
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

	function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbDRIVER_DRIV_TYPE":
						lookupMessage	=	"DRTCD.";
						break;
					
					case "cmbDRIVER_MART_STAT":
						lookupMessage	=	"Marst.";
						break;
					default:
						lookupMessage	=	"Look up code not found";
						break;
						
				}
				showLookupLayer(controlId,lookupMessage);							
			}
			
			
		function Redirect()
		{
			 top.frames[1].location='PolicyVehicleIndex.aspx?CalledFrom=UMB&'; 
		}		
		function RedirectToBoat()
		{	
			var calledFrom = new String(document.getElementById('hidCalledFrom').value);						
			top.frames[1].location='Watercraft/PolicyWatercraftIndex.aspx?CalledFrom=' + calledFrom.toUpperCase() +  '&'; 
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
				EnableDisableControls();
				CheckForDriverType();
				return false;	
		}
		
		function EnableDisableControls()
		{	
			if(document.getElementById('hidCalledFrom').value=='PPA' || document.getElementById('hidCalledFrom').value=='ppa')
			{	
				
				var dtCurrentDate=new Date();				
				var dtDOB=new Date(FormatDateForGrid(document.getElementById('txtDRIVER_DOB'),'##/##/####'));
				var diff = DateDiffernce(dtDOB,dtCurrentDate);
					
					
			}	
		}
		function EnableDisableAssignVehicleValidators(AssignSection,flag)
		{
			switch(AssignSection)
			{
				case "AUTO":
					EnableDisableDescByDriver(document.getElementById('cmbVEHICLE_ID'),document.getElementById('rfvVEHICLE_ID'),document.getElementById('spnVEHICLE_ID'),flag);			
					EnableDisableDescByDriver(document.getElementById('cmbAPP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvVEHICLE_DRIVER'),document.getElementById('spnVEHICLE_ID'),flag);			
					break;
				case "CYCL":			
					EnableDisableDescByDriver(document.getElementById('cmbMOT_VEHICLE_ID'),document.getElementById('rfvMOT_VEHICLE_ID'),document.getElementById('spnMOT_VEHICLE_ID'),flag);			
					EnableDisableDescByDriver(document.getElementById('cmbMOT_APP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvMOT_APP_VEHICLE_PRIN_OCC_ID'),document.getElementById('spnMOT_VEHICLE_ID'),flag);			
					break;
				case "BOAT":
					EnableDisableDescByDriver(document.getElementById('cmbOP_VEHICLE_ID'),document.getElementById('rfvOP_VEHICLE_ID'),document.getElementById('spnOP_VEHICLE_ID'),flag);			
					EnableDisableDescByDriver(document.getElementById('cmbOP_APP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvOP_APP_VEHICLE_PRIN_OCC_ID'),document.getElementById('spnOP_VEHICLE_ID'),flag);			
					break;
				case "ALL":
					EnableDisableDescByDriver(document.getElementById('cmbVEHICLE_ID'),document.getElementById('rfvVEHICLE_ID'),document.getElementById('spnVEHICLE_ID'),flag);			
					EnableDisableDescByDriver(document.getElementById('cmbAPP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvVEHICLE_DRIVER'),document.getElementById('spnVEHICLE_ID'),flag);			
					EnableDisableDescByDriver(document.getElementById('cmbMOT_VEHICLE_ID'),document.getElementById('rfvMOT_VEHICLE_ID'),document.getElementById('spnMOT_VEHICLE_ID'),flag);			
					EnableDisableDescByDriver(document.getElementById('cmbMOT_APP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvMOT_APP_VEHICLE_PRIN_OCC_ID'),document.getElementById('spnMOT_VEHICLE_ID'),flag);			
					EnableDisableDescByDriver(document.getElementById('cmbOP_VEHICLE_ID'),document.getElementById('rfvOP_VEHICLE_ID'),document.getElementById('spnOP_VEHICLE_ID'),flag);			
					EnableDisableDescByDriver(document.getElementById('cmbOP_APP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvOP_APP_VEHICLE_PRIN_OCC_ID'),document.getElementById('spnOP_VEHICLE_ID'),flag);			
					break;
				default:
					break;
			}
		}
		function HideAndDisableAssignSection(flag)
		{
			if(flag)
			{
				document.getElementById('tbAssignVehSec').style.display="inline";
				document.getElementById('tbAssignMotorSec').style.display="inline";
				document.getElementById('tbAssignBoatSec').style.display="inline";
			}
			else
			{				
				document.getElementById('tbAssignVehSec').style.display="none";
				document.getElementById('tbAssignMotorSec').style.display="none";
				document.getElementById('tbAssignBoatSec').style.display="none";
			}
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
		
		function CheckForDriverType()
		{ 
			var SelectedValue
		
         if (document.getElementById('cmbDRIVER_DRIV_TYPE').selectedIndex != '-1')	
		    {
		        SelectedValue=document.getElementById('cmbDRIVER_DRIV_TYPE').options[document.getElementById('cmbDRIVER_DRIV_TYPE').selectedIndex].value;
				if(SelectedValue == '3478' ) //Not Licenced Driver Type
				{
				EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'),document.getElementById('rfvDRIVER_LIC_STATE'),document.getElementById('spnDRIVER_LIC_STATE'),false);			
				EnableDisableDescByDriver(document.getElementById('txtDRIVER_DRIV_LIC'),document.getElementById('rfvDRIVER_DRIV_LIC'),document.getElementById('spnDRIVER_DRIV_LIC'),false);			
				EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'),document.getElementById('rfvDRIVER_LIC_STATE'),document.getElementById('spnDRIVER_LIC_STATE'),false);			
				EnableDisableDescByDriver(document.getElementById('cmbDRIVER_DRINK_VIOLATION'),document.getElementById('rfvDRIVER_DRINK_VIOLATION'),document.getElementById('spnDRIVER_DRINK_VIOLATION'),false);			
				//EnableDisableDescByDriver(document.getElementById('cmbVEHICLE_ID'),document.getElementById('rfvVEHICLE_ID'),document.getElementById('spnVEHICLE_ID'),false);			
				//EnableDisableDescByDriver(document.getElementById('cmbAPP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvVEHICLE_DRIVER'),document.getElementById('spnVEHICLE_ID'),false);			
				EnableDisableDescByDriver(document.getElementById('txtDATE_LICENSED'),document.getElementById('rfvDATE_LICENSED'),document.getElementById('spnDATE_LICENSED'),false);			
				//document.getElementById('assignVehSec').style.display="none"
				//document.getElementById('tbAssignVehSec').style.display="none"
				//document.getElementById('trVehField').style.display="none"	
				HideAndDisableAssignSection(false);	
				EnableDisableAssignVehicleValidators("ALL",false);
				document.getElementById('capFORM_F95').style.display="none"
				document.getElementById('cmbFORM_F95').style.display="none"		
				document.getElementById('cmbFORM_F95').options.selectedIndex = -1;	
					
				}
				else if( SelectedValue == '3477')//Excluded Driver Type
				{
				    EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'),document.getElementById('rfvDRIVER_LIC_STATE'),document.getElementById('spnDRIVER_LIC_STATE'),false);			
				    EnableDisableDescByDriver(document.getElementById('txtDRIVER_DRIV_LIC'),document.getElementById('rfvDRIVER_DRIV_LIC'),document.getElementById('spnDRIVER_DRIV_LIC'),false);			
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_DRINK_VIOLATION'),document.getElementById('rfvDRIVER_DRINK_VIOLATION'),document.getElementById('spnDRIVER_DRINK_VIOLATION'),false);			
					//EnableDisableDescByDriver(document.getElementById('cmbVEHICLE_ID'),document.getElementById('rfvVEHICLE_ID'),document.getElementById('spnVEHICLE_ID'),false);			
					//EnableDisableDescByDriver(document.getElementById('cmbAPP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvVEHICLE_DRIVER'),document.getElementById('spnVEHICLE_ID'),false);			
					EnableDisableDescByDriver(document.getElementById('txtDATE_LICENSED'),document.getElementById('rfvDATE_LICENSED'),document.getElementById('spnDATE_LICENSED'),false);			
					//document.getElementById('assignVehSec').style.display="none"
					//document.getElementById('tbAssignVehSec').style.display="none"
					//document.getElementById('trVehField').style.display="none"
					HideAndDisableAssignSection(false);				
					EnableDisableAssignVehicleValidators("ALL",false);
					document.getElementById('capFORM_F95').style.display="inline"
					document.getElementById('cmbFORM_F95').style.display="inline"
				}			
				else if( SelectedValue == '11603')// && document.getElementById('cmbVEHICLE_ID').options.length>0) //Driver is licenced and there are boats in the combo-box
				{				
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'),document.getElementById('rfvDRIVER_LIC_STATE'),document.getElementById('spnDRIVER_LIC_STATE'),true);			
					EnableDisableDescByDriver(document.getElementById('txtDRIVER_DRIV_LIC'),document.getElementById('rfvDRIVER_DRIV_LIC'),document.getElementById('spnDRIVER_DRIV_LIC'),true);			
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'),document.getElementById('rfvDRIVER_LIC_STATE'),document.getElementById('spnDRIVER_LIC_STATE'),true);			
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_DRINK_VIOLATION'),document.getElementById('rfvDRIVER_DRINK_VIOLATION'),document.getElementById('spnDRIVER_DRINK_VIOLATION'),true);			
					//EnableDisableDescByDriver(document.getElementById('cmbVEHICLE_ID'),document.getElementById('rfvVEHICLE_ID'),document.getElementById('spnVEHICLE_ID'),true);			
					//EnableDisableDescByDriver(document.getElementById('cmbAPP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvVEHICLE_DRIVER'),document.getElementById('spnVEHICLE_ID'),true);	
					//allow driver data to be saved even when there are no vehicles added and driver type is licensed
					/*if(document.getElementById('hidCalledFrom').value == 'PPA' && document.getElementById("cmbVEHICLE_ID").length>0)
					{
						EnableDisableDescByDriver(document.getElementById('cmbVEHICLE_ID'),document.getElementById('rfvVEHICLE_ID'),document.getElementById('spnVEHICLE_ID'),true);			
						EnableDisableDescByDriver(document.getElementById('cmbAPP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvVEHICLE_DRIVER'),document.getElementById('spnVEHICLE_ID'),true);			
					} 
					else
					{
						EnableDisableDescByDriver(document.getElementById('cmbVEHICLE_ID'),document.getElementById('rfvVEHICLE_ID'),document.getElementById('spnVEHICLE_ID'),false);			
						EnableDisableDescByDriver(document.getElementById('cmbAPP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvVEHICLE_DRIVER'),document.getElementById('spnVEHICLE_ID'),false);			
					 
					}*/ 
					EnableDisableDescByDriver(document.getElementById('txtDATE_LICENSED'),document.getElementById('rfvDATE_LICENSED'),document.getElementById('spnDATE_LICENSED'),true);					
					//document.getElementById('assignVehSec').style.display="inline"
					//document.getElementById('tbAssignVehSec').style.display="inline"
					HideAndDisableAssignSection(true);				
					if(document.getElementById('cmbVEHICLE_ID').options.length>0) //Driver is licenced and there are boats in the combo-box
					{
						document.getElementById('trVehField').style.display="inline"
						document.getElementById('trVehMsg').style.display="none"
						EnableDisableAssignVehicleValidators("AUTO",true);
					}
					else
					{
						document.getElementById('trVehField').style.display="none"
						document.getElementById('trVehMsg').style.display="inline"
						EnableDisableAssignVehicleValidators("AUTO",false);
					}
					
					if(document.getElementById('cmbOP_VEHICLE_ID').options.length>0) //Driver is licenced and there are boats in the combo-box
					{
						document.getElementById('trBoatField').style.display="inline"
						document.getElementById('trBoatMsg').style.display="none"
						EnableDisableAssignVehicleValidators("BOAT",true);
					}
					else
					{
						document.getElementById('trBoatField').style.display="none"
						document.getElementById('trBoatMsg').style.display="inline"
						EnableDisableAssignVehicleValidators("BOAT",false);
					}
					
					if(document.getElementById('cmbMOT_VEHICLE_ID').options.length>0) //Driver is licenced and there are boats in the combo-box
					{
						document.getElementById('trMotorField').style.display="inline"
						document.getElementById('trMotorMsg').style.display="none"
						EnableDisableAssignVehicleValidators("CYCL",true);
					}
					else
					{
						document.getElementById('trMotorField').style.display="none"
						document.getElementById('trMotorMsg').style.display="inline"
						EnableDisableAssignVehicleValidators("CYCL",false);
					}
					document.getElementById('capFORM_F95').style.display="none"
					document.getElementById('cmbFORM_F95').style.display="none"
					document.getElementById('cmbFORM_F95').options.selectedIndex = -1;
				}
				else 
				{
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'),document.getElementById('rfvDRIVER_LIC_STATE'),document.getElementById('spnDRIVER_LIC_STATE'),false);			
					EnableDisableDescByDriver(document.getElementById('txtDRIVER_DRIV_LIC'),document.getElementById('rfvDRIVER_DRIV_LIC'),document.getElementById('spnDRIVER_DRIV_LIC'),false);			
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'),document.getElementById('rfvDRIVER_LIC_STATE'),document.getElementById('spnDRIVER_LIC_STATE'),false);			
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_DRINK_VIOLATION'),document.getElementById('rfvDRIVER_DRINK_VIOLATION'),document.getElementById('spnDRIVER_DRINK_VIOLATION'),false);			
					//EnableDisableDescByDriver(document.getElementById('cmbVEHICLE_ID'),document.getElementById('rfvVEHICLE_ID'),document.getElementById('spnVEHICLE_ID'),false);			
					//EnableDisableDescByDriver(document.getElementById('cmbAPP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvVEHICLE_DRIVER'),document.getElementById('spnVEHICLE_ID'),false);			
					EnableDisableDescByDriver(document.getElementById('txtDATE_LICENSED'),document.getElementById('rfvDATE_LICENSED'),document.getElementById('spnDATE_LICENSED'),false);			
					//document.getElementById('assignVehSec').style.display="none"
					//document.getElementById('tbAssignVehSec').style.display="none"
					HideAndDisableAssignSection(false);		
					EnableDisableAssignVehicleValidators("ALL",false);		
					//document.getElementById('trVehField').style.display="none"
					document.getElementById('capFORM_F95').style.display="none"
					document.getElementById('cmbFORM_F95').style.display="none"
					document.getElementById('cmbFORM_F95').options.selectedIndex = -1;
				}	 
		    }	
		    else
		    {
		        EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'),document.getElementById('rfvDRIVER_LIC_STATE'),document.getElementById('spnDRIVER_LIC_STATE'),false);			
				EnableDisableDescByDriver(document.getElementById('txtDRIVER_DRIV_LIC'),document.getElementById('rfvDRIVER_DRIV_LIC'),document.getElementById('spnDRIVER_DRIV_LIC'),false);			
				EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'),document.getElementById('rfvDRIVER_LIC_STATE'),document.getElementById('spnDRIVER_LIC_STATE'),false);			
				EnableDisableDescByDriver(document.getElementById('cmbDRIVER_DRINK_VIOLATION'),document.getElementById('rfvDRIVER_DRINK_VIOLATION'),document.getElementById('spnDRIVER_DRINK_VIOLATION'),false);			
				//EnableDisableDescByDriver(document.getElementById('cmbVEHICLE_ID'),document.getElementById('rfvVEHICLE_ID'),document.getElementById('spnVEHICLE_ID'),false);			
				//EnableDisableDescByDriver(document.getElementById('cmbAPP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvVEHICLE_DRIVER'),document.getElementById('spnVEHICLE_ID'),false);			
				EnableDisableDescByDriver(document.getElementById('txtDATE_LICENSED'),document.getElementById('rfvDATE_LICENSED'),document.getElementById('spnDATE_LICENSED'),false);			
				//document.getElementById('assignVehSec').style.display="none"
				//document.getElementById('tbAssignVehSec').style.display="none"
				HideAndDisableAssignSection(false);				
				document.getElementById('capFORM_F95').style.display="none"
				document.getElementById('cmbFORM_F95').style.display="none"
				document.getElementById('cmbFORM_F95').options.selectedIndex = -1;
				//document.getElementById('trVehField').style.display="none"
		    }	 
		
		//	HideShowAssignedVehicleSection();
		return false;
						
		}

		
		
		function EnableDisableDesc(txtDesc,lblNA,rfvDesc,flag)
		{		
				
				if (flag==false)
				{			
					
					if(rfvDesc!=null)
					{
						rfvDesc.setAttribute('enabled',false);					
						rfvDesc.setAttribute('isValid',false);
						document.getElementById('capGoodStudent').style.display="none"
					}					
					txtDesc.style.display = "none";
					lblNA.style.display = "none";					
				}
				else
				{	
					if(rfvDesc!=null)
					{
						rfvDesc.setAttribute('enabled',true);					
						rfvDesc.setAttribute('isValid',true);
					}
					txtDesc.style.display = "inline";
					lblNA.style.display = "inline";										
				}			
		}
		
	function MakeNonMandatory()
		{
			if((document.getElementById('cmbVEHICLE_ID').options.selectedIndex != 0) && (document.getElementById('cmbAPP_VEHICLE_PRIN_OCC_ID').options.selectedIndex != 0))
			{
				document.getElementById('spnMOT_VEHICLE_ID').style.display = "none";
				rfvMOT_VEHICLE_ID.setAttribute('enabled',false);					
				rfvMOT_VEHICLE_ID.setAttribute('isValid',false);
				document.getElementById('rfvMOT_VEHICLE_ID').style.display = "none";
				rfvMOT_APP_VEHICLE_PRIN_OCC_ID.setAttribute('enabled',false);					
				rfvMOT_APP_VEHICLE_PRIN_OCC_ID.setAttribute('isValid',false);
				document.getElementById('rfvMOT_APP_VEHICLE_PRIN_OCC_ID').style.display = "none";
				document.getElementById('spnOP_VEHICLE_ID').style.display = "none";
				rfvOP_VEHICLE_ID.setAttribute('enabled',false);					
				rfvOP_VEHICLE_ID.setAttribute('isValid',false);
				document.getElementById('rfvOP_VEHICLE_ID').style.display = "none";
				rfvOP_APP_VEHICLE_PRIN_OCC_ID.setAttribute('enabled',false);					
				rfvOP_APP_VEHICLE_PRIN_OCC_ID.setAttribute('isValid',false);
				document.getElementById('rfvOP_APP_VEHICLE_PRIN_OCC_ID').style.display = "none";
				

				ChangeColor();
				DisableValidators();

			}
			else if ((document.getElementById('cmbMOT_VEHICLE_ID').options.selectedIndex != 0) && (document.getElementById('cmbMOT_APP_VEHICLE_PRIN_OCC_ID').options.selectedIndex != 0))
			{
				document.getElementById('spnVEHICLE_ID').style.display = "none";
				rfvVEHICLE_ID.setAttribute('enabled',false);					
				rfvVEHICLE_ID.setAttribute('isValid',false);
				document.getElementById('rfvVEHICLE_ID').style.display = "none";
				rfvVEHICLE_DRIVER.setAttribute('enabled',false);					
				rfvVEHICLE_DRIVER.setAttribute('isValid',false);
				document.getElementById('rfvVEHICLE_DRIVER').style.display = "none";
				document.getElementById('spnOP_VEHICLE_ID').style.display = "none";
				rfvOP_VEHICLE_ID.setAttribute('enabled',false);					
				rfvOP_VEHICLE_ID.setAttribute('isValid',false);
				document.getElementById('rfvOP_VEHICLE_ID').style.display = "none";
				rfvOP_APP_VEHICLE_PRIN_OCC_ID.setAttribute('enabled',false);					
				rfvOP_APP_VEHICLE_PRIN_OCC_ID.setAttribute('isValid',false);
				document.getElementById('rfvOP_APP_VEHICLE_PRIN_OCC_ID').style.display = "none";
				ChangeColor();
				DisableValidators();
			}
			
			else if ((document.getElementById('cmbOP_VEHICLE_ID').options.selectedIndex != 0) && (document.getElementById('cmbOP_APP_VEHICLE_PRIN_OCC_ID').options.selectedIndex != 0))
			{
				document.getElementById('spnVEHICLE_ID').style.display = "none";
				document.getElementById('spnMOT_VEHICLE_ID').style.display = "none";
				rfvVEHICLE_ID.setAttribute('enabled',false);					
				rfvVEHICLE_ID.setAttribute('isValid',false);
				document.getElementById('rfvVEHICLE_ID').style.display = "none";
				rfvVEHICLE_DRIVER.setAttribute('enabled',false);					
				rfvVEHICLE_DRIVER.setAttribute('isValid',false);
				document.getElementById('rfvVEHICLE_DRIVER').style.display = "none";
				rfvMOT_VEHICLE_ID.setAttribute('enabled',false);					
				rfvMOT_VEHICLE_ID.setAttribute('isValid',true);
				document.getElementById('rfvMOT_VEHICLE_ID').style.display = "none";				
				rfvMOT_APP_VEHICLE_PRIN_OCC_ID.setAttribute('enabled',false);					
				rfvMOT_APP_VEHICLE_PRIN_OCC_ID.setAttribute('isValid',false);
				document.getElementById('rfvMOT_APP_VEHICLE_PRIN_OCC_ID').style.display = "none";
				ChangeColor();
				DisableValidators();
			}
			ChangeColor();
		}
		
	/*function HideShowAssignedVehicleSection()
		{
			
			if(document.getElementById('cmbDRIVER_DRIV_TYPE').options[document.getElementById('cmbDRIVER_DRIV_TYPE').selectedIndex].value!='11603')
			{
				document.getElementById("tbAssignVehSec").style.display="none";				
				return;
			}			
			else
				document.getElementById("tbAssignVehSec").style.display="inline";
		
		}*/

		</script>
</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="populateXML();EnableDisableControls();MakeNonMandatory();ApplyColor();ChangeColor();">
		<FORM id="APP_DRIVER_DETAILS" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow>Please 
										note that all fields marked with * are mandatory</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<td colSpan="4">
										<table cellSpacing="0" cellPadding="0" width="100%">
											<tr>
												<TD class="midcolora"><asp:label id="capDRIVER_FNAME" runat="server">First NAme</asp:label><span class="mandatory">*</span>
												</TD>
												<TD class="midcolora"><asp:textbox id="txtDRIVER_FNAME" runat="server" size="35" maxlength="70"></asp:textbox><BR>
													<asp:requiredfieldvalidator id="rfvDRIVER_FNAME" runat="server" ControlToValidate="txtDRIVER_FNAME" ErrorMessage="DRIVER_FNAME can't be blank."
														Display="Dynamic"></asp:requiredfieldvalidator></TD>
												<TD class="midcolora"><asp:label id="capDRIVER_MNAME" runat="server">Middle Name</asp:label></TD>
												<TD class="midcolora"><asp:textbox id="txtDRIVER_MNAME" runat="server" size="35" maxlength="25"></asp:textbox></TD>
												<TD class="midcolora"><asp:label id="capDRIVER_LNAME" runat="server">Last Name</asp:label><span class="mandatory">*</span>
												</TD>
												<TD class="midcolora"><asp:textbox id="txtDRIVER_LNAME" runat="server" size="35" maxlength="70"></asp:textbox><BR>
													<asp:requiredfieldvalidator id="rfvDRIVER_LNAME" runat="server" ControlToValidate="txtDRIVER_LNAME" ErrorMessage="DRIVER_LNAME can't be blank."
														Display="Dynamic"></asp:requiredfieldvalidator></TD>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_DOB" runat="server">Date of Birth</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_DOB" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkOCCURENCE_DATE" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgOCCURENCE_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
										</asp:hyperlink><asp:requiredfieldvalidator id="rfvDRIVER_DOB" ControlToValidate="txtDRIVER_DOB" Display="Dynamic" Runat="server"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revDRIVER_DOB" runat="server" ControlToValidate="txtDRIVER_DOB" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvDRIVER_DOB" ControlToValidate="txtDRIVER_DOB" Display="Dynamic" Runat="server"
											ClientValidationFunction="ChkDOB"></asp:customvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_SSN" runat="server">Social Security Number</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_SSN" runat="server" size="13" maxlength="11"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revDRIVER_SSN" runat="server" ControlToValidate="txtDRIVER_SSN" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_MART_STAT" runat="server">Marital Status</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_MART_STAT" onfocus="SelectComboIndex('cmbDRIVER_MART_STAT')" runat="server">
											<ASP:LISTITEM></ASP:LISTITEM>
										</asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbDRIVER_MART_STAT')"></A></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_SEX" runat="server">Gender</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_SEX" onfocus="SelectComboIndex('cmbDRIVER_SEX')" runat="server">
											<ASP:LISTITEM Value=""></ASP:LISTITEM>
											<ASP:LISTITEM Value="M">Male</ASP:LISTITEM>
											<ASP:LISTITEM Value="F">Female</ASP:LISTITEM>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_SEX" runat="server" ControlToValidate="cmbDRIVER_SEX" ErrorMessage="DRIVER_SEX can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_DRIV_TYPE" runat="server">Driver Type </asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_DRIV_TYPE" onfocus="SelectComboIndex('cmbDRIVER_DRIV_TYPE')" runat="server"
											onchange="CheckForDriverType();">
											<ASP:LISTITEM Value="0"></ASP:LISTITEM>
										</asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbDRIVER_DRIV_TYPE')"></A><br>
										<asp:requiredfieldvalidator id="rfvDRIVER_DRIV_TYPE" runat="server" ControlToValidate="cmbDRIVER_DRIV_TYPE"
											ErrorMessage="DRIVER_DRIV_TYPE can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
									<td class="midcolora" width="18%"><asp:label id="capFORM_F95" runat="server">Form F-95 Signed</asp:label></td>
									<td class="midcolora" width="32%"><asp:dropdownlist id="cmbFORM_F95" onfocus="SelectComboIndex('cmbFORM_F95')" runat="server"></asp:dropdownlist></td>
					</TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capDRIVER_LIC_STATE" runat="server">License State</asp:label><span class="mandatory" id="spnDRIVER_LIC_STATE">*</span></TD>
					<TD class="midcolora" width="32%" colspan="3"><asp:dropdownlist id="cmbDRIVER_LIC_STATE" onfocus="SelectComboIndex('cmbDRIVER_LIC_STATE')" runat="server">
							<ASP:LISTITEM Value="0"></ASP:LISTITEM>
						</asp:dropdownlist><BR>
						<asp:requiredfieldvalidator id="rfvDRIVER_LIC_STATE" runat="server" ControlToValidate="cmbDRIVER_LIC_STATE"
							ErrorMessage="DRIVER_LIC_STATE can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>
					
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capDATE_LICENSED" runat="server">Date Experience Started </asp:label><span class="mandatory" id="spnDATE_LICENSED">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtDATE_LICENSED" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkDATE_LICENSED" runat="server" CssClass="HotSpot">
							<ASP:IMAGE id="imgDATE_LICENSED" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
						</asp:hyperlink><asp:regularexpressionvalidator id="revDATE_LICENSED" runat="server" ControlToValidate="txtDATE_LICENSED" ErrorMessage="RegularExpressionValidator"
							Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvDATE_LICENSED" ControlToValidate="txtDATE_LICENSED" Display="Dynamic" Runat="server"
							ClientValidationFunction="ChkEXPDate"></asp:customvalidator><%--<asp:customvalidator id="csvDATE_EXP_DOB" ControlToValidate="txtDATE_LICENSED" Display="Dynamic" Runat="server"
							ClientValidationFunction="CompareExpDateWithDOB"></asp:customvalidator>--%>
							<span id="spnDATE_EXP_DOB" style="DISPLAY: none; COLOR: red" runat="server"></span><br>
						<asp:requiredfieldvalidator id="rfvDATE_LICENSED" ControlToValidate="txtDATE_LICENSED" ErrorMessage="DATE_LICENSED can't be blank."
							Display="Dynamic" Runat="server"></asp:requiredfieldvalidator></TD>
					<TD class="midcolora" width="18%"><asp:label id="capDRIVER_DRIV_LIC" runat="server">License Number</asp:label><span class="mandatory" id="spnDRIVER_DRIV_LIC">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_DRIV_LIC" runat="server" size="30" maxlength="30"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvDRIVER_DRIV_LIC" ControlToValidate="txtDRIVER_DRIV_LIC" Display="Dynamic"
							Runat="server"></asp:requiredfieldvalidator></TD>
				</tr>

				<tbody id="tbAssignVehSec">
					<tr id="assignVehSec">
						<TD class="headerEffectSystemParams" colSpan="4">Assigned Vehicle</TD>
					</tr>
					<tr id="trVehMsg" style="DISPLAY: none" runat="server">
						<td class="midcolora" colSpan="4"><asp:label id="lblVehicleMsg" Runat="server">No vehicle added until now. Please click <a href='#' onclick='Redirect();'>here</a> to add vehicles.</asp:label></td>
					</tr>
					<tr id="trVehField" style="DISPLAY: none" runat="server">
						<td class="midcolora" colSpan="4">
							<table cellSpacing="0" cellPadding="0" width="65%" border="0">
								<tr align="left">
									<td class="midcolora" align="left"><asp:label id="capVEHICLE_ID" runat="server"></asp:label><span class="mandatory" id="spnVEHICLE_ID" runat="server">*</span>
									</td>
									<td class="midcolora" align="left" width="25%">&nbsp;<asp:dropdownlist id="cmbVEHICLE_ID" onfocus="SelectComboIndex('cmbVEHICLE_ID')" runat="server"></asp:dropdownlist>&nbsp;as&nbsp;<br>
										<asp:requiredfieldvalidator id="rfvVEHICLE_ID" runat="server" ControlToValidate="cmbVEHICLE_ID" Display="Dynamic"></asp:requiredfieldvalidator></td>
									<td class="midcolora" align="left"><asp:dropdownlist id="cmbAPP_VEHICLE_PRIN_OCC_ID" onfocus="SelectComboIndex('cmbAPP_VEHICLE_PRIN_OCC_ID')" onclick ="MakeNonMandatory();"
											runat="server"></asp:dropdownlist>&nbsp;<asp:label id="capAPP_VEHICLE_PRIN_OCC_ID" runat="server">Driver</asp:label><br>
										<asp:requiredfieldvalidator id="rfvVEHICLE_DRIVER" runat="server" ControlToValidate="cmbAPP_VEHICLE_PRIN_OCC_ID"
											Display="Dynamic"></asp:requiredfieldvalidator></td>
								</tr>
							</table>
						</td>
					</tr>
					<%--<tr>
						<td colSpan="4"><asp:table id="tblAssignedVeh" runat="server" Border="0" width="100%"></asp:table></td>
					</tr>--%>
				</tbody>
				<tbody id="tbAssignMotorSec">
					<tr id="assignMotorSec">
						<TD class="headerEffectSystemParams" colSpan="4">Assigned Motorcycle</TD>
					</tr>
					<tr id="trMotorMsg" style="DISPLAY: none" runat="server">
						<td class="midcolora" colSpan="4"><asp:label id="lblMotorMsg" Runat="server">No motorcycle added until now. Please click <a href='#' onclick='Redirect();'>here</a> to add motorcycles.</asp:label></td>
					</tr>
					<tr id="trMotorField" style="DISPLAY: none" runat="server">
						<td class="midcolora" colSpan="4">
							<table cellSpacing="0" cellPadding="0" width="65%" border="0">
								<tr align="left">
									<td class="midcolora" align="left"><asp:label id="capMOT_VEHICLE_ID" runat="server"></asp:label><span class="mandatory" id="spnMOT_VEHICLE_ID" runat="server">*</span>
									</td>
									<td class="midcolora" align="left" width="25%">&nbsp;<asp:dropdownlist id="cmbMOT_VEHICLE_ID" onfocus="SelectComboIndex('cmbMOT_VEHICLE_ID')" runat="server"></asp:dropdownlist>&nbsp;as&nbsp;<br>
										<asp:requiredfieldvalidator id="rfvMOT_VEHICLE_ID" runat="server" ControlToValidate="cmbMOT_VEHICLE_ID" Display="Dynamic"></asp:requiredfieldvalidator></td>
									<td class="midcolora" align="left"><asp:dropdownlist id="cmbMOT_APP_VEHICLE_PRIN_OCC_ID" onfocus="SelectComboIndex('cmbMOT_APP_VEHICLE_PRIN_OCC_ID')" onclick ="MakeNonMandatory();"
											runat="server"></asp:dropdownlist>&nbsp;<asp:label id="capMOT_APP_VEHICLE_PRIN_OCC_ID" runat="server"></asp:label><br>
										<asp:requiredfieldvalidator id="rfvMOT_APP_VEHICLE_PRIN_OCC_ID" runat="server" ControlToValidate="cmbMOT_APP_VEHICLE_PRIN_OCC_ID"
											Display="Dynamic"></asp:requiredfieldvalidator></td>
								</tr>
							</table>
						</td>
					</tr>
				</tbody>
				<tbody id="tbAssignBoatSec">
					<tr id="assignBoatSec">
						<TD class="headerEffectSystemParams" colSpan="4">Assigned Boat</TD>
					</tr>
					<tr id="trBoatMsg" style="DISPLAY: none" runat="server">
						<td class="midcolora" colSpan="4"><asp:label id="lblBoatMsg" Runat="server">No boat added until now. Please click <a href='#' onclick='RedirectToBoat();'>here</a> to add boats.</asp:label></td>
					</tr>
					<tr id="trBoatField" style="DISPLAY: none" runat="server">
						<td class="midcolora" colSpan="4">
							<table cellSpacing="0" cellPadding="0" width="65%" border="0">
								<tr align="left">
									<td class="midcolora" align="left"><asp:label id="capOP_VEHICLE_ID" runat="server">Drive</asp:label><span class="mandatory" id="spnOP_VEHICLE_ID" runat="server">*</span>
									</td>
									<td class="midcolora" align="left" width="25%">&nbsp;<asp:dropdownlist id="cmbOP_VEHICLE_ID" onfocus="SelectComboIndex('cmbOP_VEHICLE_ID')" runat="server"></asp:dropdownlist>&nbsp;as&nbsp;<br>
										<asp:requiredfieldvalidator id="rfvOP_VEHICLE_ID" runat="server" ControlToValidate="cmbOP_VEHICLE_ID" Display="Dynamic"></asp:requiredfieldvalidator></td>
									<td class="midcolora" align="left"><asp:dropdownlist id="cmbOP_APP_VEHICLE_PRIN_OCC_ID" onfocus="SelectComboIndex('cmbOP_APP_VEHICLE_PRIN_OCC_ID')" onclick ="MakeNonMandatory();"
											runat="server"></asp:dropdownlist>&nbsp;<asp:label id="capOP_APP_VEHICLE_PRIN_OCC_ID" runat="server"></asp:label><br>
										<asp:requiredfieldvalidator id="rfvOP_APP_VEHICLE_PRIN_OCC_ID" runat="server" Display="Dynamic" ControlToValidate="cmbOP_APP_VEHICLE_PRIN_OCC_ID"></asp:requiredfieldvalidator></td>
								</tr>
							</table>
						</td>
					</tr>
				</tbody>
				<TR>
					<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"
							CausesValidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" causesvalidation="false"></cmsb:cmsbutton></td>
					<td class="midcolorr" colSpan="2"><%--<cmsb:cmsbutton class="clsButton" id="btnCopyAppDrivers" runat="server" Text="Copy Application Drivers"></cmsb:cmsbutton>--%><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
				</TR>
			</TABLE>
			</TD></TR></TABLE><INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidDRIVER_ID" type="hidden" name="hidDRIVER_ID" runat="server"> <INPUT id="hidCUSTOMER_ID" type="hidden" name="hidAPP_ID" runat="server">
			<INPUT id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server">
			<INPUT id="hidPolicyId" type="hidden" name="hidPolicyId" runat="server">
			<INPUT id="hidPolicyVersionId" type="hidden" name="hidPolicyVersionId" runat="server">
			<INPUT id="hidCUSTOMER_INFO" type="hidden" name="hidCUSTOMER_INFO" runat="server">
			<INPUT id="hidDiscTypeLen" type="hidden" name="hidDiscTypeLen" runat="server"> <input id="hidDataValue1" type="hidden" name="hidDataValue1" runat="server">
			<input id="hidDataValue2" type="hidden" name="hidDataValue2" runat="server"> <input id="hidCustomInfo" type="hidden" name="hidCustomInfo" runat="server">
			<input id="hidState_id" type="hidden" value="0" name="hidState_id" runat="server">
			<input id="hidSeletedData" type="hidden" name="hidSeletedData" runat="server">
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none; Z-INDEX: 101; POSITION: absolute"><iframe id="lookupLayerIFrame" style="DISPLAY: none; Z-INDEX: 1001; FILTER: alpha(opacity=0); BACKGROUND-COLOR: #000000"
				width="0" height="0" top="0px;" left="0px"></iframe>
		</div>
		<DIV id="lookupLayer" onmouseover="javascript:refreshLookupLayer();" style="Z-INDEX: 101; VISIBILITY: hidden; POSITION: absolute">
			<table class="TabRow" cellSpacing="0" cellPadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b>Add LookUp</b></td>
					<td>
						<p align="right"><A onclick="javascript:hideLookupLayer();" href="javascript:void(0)"><IMG height="14" alt="Close" src="/cms/cmsweb/images/cross.gif" width="16" border="0"></A></p>
					</td>
				</tr>
				<tr class="SubTabRow">
					<td colSpan="2"><span id="LookUpMsg"></span></td>
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
				RefreshWebGrid(document.getElementById('hidFormSaved').value,TrimTheString(document.getElementById('hidDRIVER_ID').value));				
			}
						
			
		</script>
	</BODY>
</HTML>












<%@ Page language="c#" Codebehind="AddPolicyDriver.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.AddPolicyDriver" ValidateRequest="false"%>
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
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
			var varPolEffDate="<%=PolEffDate  %>";
			var varYearsWithWolverine = "<%=intYearsWithWolverine %>";//Added by Sibin for Itrack Issue 5428 on 18 Feb 09
			
		function FetchAjaxResponse()
		{
			if(document.getElementById('txtDRIVER_DOB').value!="") // if-else condition modified by Sibin for Itrack Issue 5207 on 12 Jan 09
			{
				//var dtCurrentDate=new Date();		
				var dtDOB=new Date(FormatDateForGrid(document.getElementById('txtDRIVER_DOB'),'##/##/####'));
				var diff = DateDiffernce(dtDOB,varPolEffDate);
				document.getElementById("hidDateDiff").value = diff;
				AddPolicyDriver.AjaxCallFunction(diff,AjaxCallFunction_CallBack);
			}
		}
		
		//ADDED BY PRAVEEN KUMAR:ITRACK 5612
		function change_ddl()
		{
			if(event.keyCode==13)
			{
				CompareExpDateWithDOB();
				FetchAjaxResponse();
				EnableDisableControls();
				return SaveClientSide();
				
			}
		}
		//END PRAVEEN KUMAR
		
			function AjaxCallFunction_CallBack(response)
			{
			    var ds = response.value;
				var Xml = response.value;
                if(Xml!=null)
                {
				    var intTotalRows = parseInt(document.getElementById("tblAssignedVeh").getAttribute("TotalRows"));
					    for(var i=1 ; i<=intTotalRows ; i++)
					    {
						    var ddl = document.getElementById('ID_' + i).getElementsByTagName("td")[3].childNodes[0];
						    ddl.options.length=0;
						    var objXmlHandler = new XMLHandler();
   						    var tree = objXmlHandler.quickParseXML(Xml).childNodes[0];
    							
    			 
						    for(j=0; j<tree.childNodes.length; j++)
						    {
							    nodValue = tree.childNodes[j].getElementsByTagName('LOOKUP_UNIQUE_ID');
							    nodText = tree.childNodes[j].getElementsByTagName('LOOKUP_VALUE_DESC');
							    oOption = document.createElement("option");
							    oOption.value = nodValue[0].firstChild.text;
							    oOption.text = nodText[0].firstChild.text;
							    ddl.add(oOption);
					       }
				        }	
				}   
			}
			
			
			function SaveClientSide()
			{
				var intTotalRows = parseInt(document.getElementById("tblAssignedVeh").getAttribute("TotalRows"));
				var SelectedData="",driverType="";				
				if(document.getElementById('cmbDRIVER_DRIV_TYPE').options.selectedIndex!=-1)
					driverType = document.getElementById('cmbDRIVER_DRIV_TYPE').options[document.getElementById('cmbDRIVER_DRIV_TYPE').options.selectedIndex].value;
				if(driverType=="11603")//Fill data only when the driver type is licensed
				{
					if(document.getElementById("tbAssignVehSec").style.display=="none")
					{
						document.getElementById("hidSeletedData").value="";
						//return;
					}								
					for(var i=1 ; i<=intTotalRows ; i++)
					{
						//alert("VECH ID = " + document.getElementById('ID_' + i).getAttribute("RowVehID"))					
						//alert("DRP TEXT = " + document.getElementById('ID_' + i).getElementsByTagName("td")[3].childNodes[0].value)
						
						if (SelectedData == "")
							SelectedData = document.getElementById('ID_' + i).getAttribute("RowVehID") + "~" + document.getElementById('ID_' + i).getElementsByTagName("td")[3].childNodes[0].value;
						else
							SelectedData = SelectedData + "|" + document.getElementById('ID_' + i).getAttribute("RowVehID") + "~" + document.getElementById('ID_' + i).getElementsByTagName("td")[3].childNodes[0].value;
					}
					
					document.getElementById("hidSeletedData").value=SelectedData;
				}				
				Page_ClientValidate();
				CompareExpDateWithDOB();						 				 
				return Page_IsValid;
			}
		//Function for drawing the tabs
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
				RemoveTab(2,top.frames[1]);
			}			
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
		function EnableDisabletrLicMed()
		{
		   var CalledFrom = document.getElementById("hidCalledFrom").value;
		 
		   if(CalledFrom == 'PPA')
		   {	 	
				document.getElementById("trLicMed").style.display="None";
		   }
		}
		function HideShowAssignedVehicleSection()
		{
			combo = document.getElementById("cmbSTATIONED_IN_US_TERR");
			if(combo==null || combo.selectedIndex==-1 || document.getElementById('cmbDRIVER_DRIV_TYPE').selectedIndex==-1)
				return;			
			
			if(document.getElementById('cmbDRIVER_DRIV_TYPE').options[document.getElementById('cmbDRIVER_DRIV_TYPE').selectedIndex].value!='11603')
			{
				document.getElementById("tbAssignVehSec").style.display="none";				
				return;
			}			
			else
				document.getElementById("tbAssignVehSec").style.display="inline";
		
			if(combo.style.display!="none")
			{
				if(combo.options[combo.selectedIndex].value!="<%=((int)enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()%>")					
					document.getElementById("tbAssignVehSec").style.display="inline";					
				else
					document.getElementById("tbAssignVehSec").style.display="none";
			}					
		}
		function fxnDisplayFields(flag)
		{	
			if(document.getElementById('cmbIN_MILITARY'))
			{
				if(flag && document.getElementById('cmbIN_MILITARY').selectedIndex == 2) //Yes
				{
					//ADDED PRAVEEN KUMAR (22-01-2009):ITRACK 5330
					
					document.getElementById("rfvSTATIONED_IN_US_TERR").setAttribute("enabled",true);
					//document.getElementById("rfvSTATIONED_IN_US_TERR").setAttribute("isValid",false);
					// END PRAVEEN KUMAR
					document.getElementById('cmbSTATIONED_IN_US_TERR').style.display = "inline";
					document.getElementById('capSTATIONED_IN_US_TERR').style.display = "inline";
					document.getElementById('spnSTATIONED_IN_US_TERR').style.display = "inline";	
					
				}
				else
				{
					//ADDED PRAVEEN KUMAR(22-01-2009):ITRACK 5330
					document.getElementById('rfvSTATIONED_IN_US_TERR').style.display = 'none';
					document.getElementById("rfvSTATIONED_IN_US_TERR").setAttribute("enabled",false);
					document.getElementById("rfvSTATIONED_IN_US_TERR").setAttribute("isValid",false);
					// END PRAVEEN KUMAR
					document.getElementById('spnSTATIONED_IN_US_TERR').style.display = "none";
					document.getElementById('cmbSTATIONED_IN_US_TERR').style.display = "none";
					document.getElementById('capSTATIONED_IN_US_TERR').style.display = "none";					
					HideShowAssignedVehicleSection();
				}	
			}
			if((document.getElementById("rowIN_MILITARY").style.display=="inline" && document.getElementById('cmbIN_MILITARY').selectedIndex == 2) || (document.getElementById("cmbDRIVER_STUD_DIST_OVER_HUNDRED").style.display=="inline" && document.getElementById('cmbDRIVER_STUD_DIST_OVER_HUNDRED').selectedIndex == 2 ))
					ShowHaveCar(true);
				else
					ShowHaveCar(false);
		}	
		function ShowHaveCar(flag)
		{	//Added condition document.getElementById("hidDateDiff").value < 25 for PATCH 7
			if(flag && document.getElementById("hidDateDiff").value < 25)
			{
				document.getElementById('rfvHAVE_CAR').setAttribute('IsValid',true);
				document.getElementById('rfvHAVE_CAR').setAttribute('enabled',true);
				document.getElementById('rowDependentFields').style.display = "inline";
				//document.getElementById('capHAVE_CAR').style.display = "inline";
				//document.getElementById('spnHAVE_CAR').style.display = "inline";
			}
			else
			{
				document.getElementById('rfvHAVE_CAR').setAttribute('IsValid',false);
				document.getElementById('rfvHAVE_CAR').setAttribute('enabled',false);
				document.getElementById('rfvHAVE_CAR').style.display = "none";
				document.getElementById('rowDependentFields').style.display = "none";
				//document.getElementById('capHAVE_CAR').style.display = "none";
				//document.getElementById('spnHAVE_CAR').style.display = "none";
			}
		}
		function ShowParentsInsurance(flag)
		{
//			if(flag && (document.getElementById("cmbDRIVER_STUD_DIST_OVER_HUNDRED").style.display=="inline" && document.getElementById('cmbDRIVER_STUD_DIST_OVER_HUNDRED').selectedIndex == 2 ))
			if(flag)
			{
				document.getElementById("trParentsInsurance").style.display = "inline";
				document.getElementById("cmbPARENTS_INSURANCE").style.display = "inline";
				document.getElementById("capPARENTS_INSURANCE").style.display = "inline";
				//Added By Raghav on 24-07-2008 Itrack #Issue4537
				document.getElementById('rfvPARENTS_INSURANCE').setAttribute('IsValid',true);								
				document.getElementById('rfvPARENTS_INSURANCE').setAttribute('enabled',true);
				document.getElementById('spnPARENTS_INSURANCE').style.display="inline";
			}
			else
			{
				document.getElementById("trParentsInsurance").style.display = "none";
				document.getElementById("cmbPARENTS_INSURANCE").style.display = "none";
				document.getElementById("capPARENTS_INSURANCE").style.display = "none";
				
				//Added By Raghav on 24-07-2008 Itrack #Issue4537
				
				document.getElementById('rfvPARENTS_INSURANCE').setAttribute('IsValid',false);								
				document.getElementById('rfvPARENTS_INSURANCE').setAttribute('enabled',false);
				document.getElementById('spnPARENTS_INSURANCE').style.display="none";	
			}
		}
		
		function fxnShwHAVE_CAR()
		{
			if(document.getElementById('cmbDRIVER_STUD_DIST_OVER_HUNDRED'))
			{
				if((document.getElementById("rowIN_MILITARY").style.display=="inline" && document.getElementById('cmbIN_MILITARY').selectedIndex == 2) || (document.getElementById("cmbDRIVER_STUD_DIST_OVER_HUNDRED").style.display=="inline" && document.getElementById('cmbDRIVER_STUD_DIST_OVER_HUNDRED').selectedIndex == 2 ))
				{
					ShowHaveCar(true);
				}
				else
				{
					ShowHaveCar(false);
				}
			//	ShowParentsInsurance(true);
			}
			CheckforCollegeStud();
		}
			
		function AddData()
		{
			document.getElementById('txtDRIVER_FNAME').value  = '';
			document.getElementById('txtDRIVER_MNAME').value  = '';
			document.getElementById('txtDRIVER_LNAME').value  = '';
			document.getElementById('txtDRIVER_CODE').value  = '';
			document.getElementById('txtDRIVER_SUFFIX').value  = '';
			document.getElementById('txtDRIVER_ADD1').value  = '';
			document.getElementById('txtDRIVER_ADD2').value  = '';
			document.getElementById('txtDRIVER_CITY').value  = '';
			document.getElementById('cmbDRIVER_STATE').options.selectedIndex = -1;
			document.getElementById('txtDRIVER_ZIP').value  = '';
			document.getElementById('cmbDRIVER_COUNTRY').options.selectedIndex = 0;
			document.getElementById('txtDRIVER_HOME_PHONE').value  = '';
			document.getElementById('txtDRIVER_BUSINESS_PHONE').value  = '';
			document.getElementById('txtDRIVER_EXT').value  = '';
			document.getElementById('txtDRIVER_MOBILE').value  = '';
			document.getElementById('txtDRIVER_DOB').value  = '';
			document.getElementById('txtDRIVER_SSN').value  = '';
			document.getElementById('cmbDRIVER_MART_STAT').options.selectedIndex = -1;
			document.getElementById('cmbDRIVER_SEX').options.selectedIndex = -1;
			document.getElementById('txtDRIVER_DRIV_LIC').value  = '';
			document.getElementById('cmbNO_DEPENDENTS').options.selectedIndex = -1;
			document.getElementById('cmbDRIVER_LIC_STATE').options.selectedIndex = -1;

			document.getElementById('txtDATE_LICENSED').value  = '';
			document.getElementById('cmbDRIVER_DRIV_TYPE').options.selectedIndex = -1;
		
			document.getElementById('cmbDRIVER_OCC_CLASS').options.selectedIndex = -1;
			//document.getElementById('txtDRIVER_DRIVERLOYER_NAME').value  = ''; - Commented by Sibin on 28 Nov 08 for Itrack Issue 5060
			//document.getElementById('txtDRIVER_DRIVERLOYER_ADD').value  = ''; - Commented by Sibin on 28 Nov 08 for Itrack Issue 5060
			document.getElementById('cmbDRIVER_INCOME').options.selectedIndex = -1;
			document.getElementById('cmbDRIVER_PHYS_MED_IMPAIRE').options.selectedIndex = -1;
			if(document.getElementById('cmbPARENTS_INSURANCE'))
			 { document.getElementById('cmbPARENTS_INSURANCE').options.selectedIndex = -1;}
	
			if(document.getElementById('btnActivateDeactivate'))
				document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
	
			document.getElementById('cmbDRIVER_DRINK_VIOLATION').options.selectedIndex = -1;
			document.getElementById('cmbDRIVER_STUD_DIST_OVER_HUNDRED').options.selectedIndex = -1;
			document.getElementById('cmbDRIVER_LIC_SUSPENDED').options.selectedIndex = -1;
			document.getElementById('cmbDRIVER_VOLUNTEER_POLICE_FIRE').options.selectedIndex = -1;
			document.getElementById('cmbDRIVER_US_CITIZEN').options.selectedIndex = 1;		
			document.getElementById('cmbRELATIONSHIP').options.selectedIndex = -1;
			
			// added by Swarup on 11-Dec-2006
			document.getElementById('cmbVIOLATIONS').options.selectedIndex = -1;	
			document.getElementById('cmbMVR_ORDERED').options.selectedIndex = -1;	
			document.getElementById('txtDATE_ORDERED').value  = '';
				
			document.getElementById('cmbVEHICLE_ID').options.selectedIndex = -1;	
			document.getElementById('cmbWAIVER_WORK_LOSS_BENEFITS').options.selectedIndex = 1;
			if(document.getElementById('cmbFORM_F95'))
			 { document.getElementById('cmbFORM_F95').options.selectedIndex = -1;}	
			 
			if(document.getElementById('cmbFULL_TIME_STUDENT'))
				document.getElementById('cmbFULL_TIME_STUDENT').options.selectedIndex = -1;
			if(document.getElementById('cmbSUPPORT_DOCUMENT'))
				document.getElementById('cmbSUPPORT_DOCUMENT').options.selectedIndex = -1;
			if(document.getElementById('cmbSIGNED_WAIVER_BENEFITS_FORM'))
				document.getElementById('cmbSIGNED_WAIVER_BENEFITS_FORM').options.selectedIndex = -1;
			
			document.getElementById('cmbEXT_NON_OWN_COVG_INDIVI').options.selectedIndex = 1;
		
			if(document.getElementById('cmbOP_DRIVER_COST_GAURAD_AUX'))
			{
				document.getElementById('cmbOP_DRIVER_COST_GAURAD_AUX').options.selectedIndex = 0;			
			}
			document.getElementById('hidDRIVER_ID').value	=	'New';
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
//			 }
		//Added By Shafi
		//Check For Michigan And Automobile
		setDriv_Lic_regexp(1);
		if(document.getElementById('hidCalledFrom').value=="PPA" && document.getElementById('hidState_id').value=="22")
		 {
		      
		 }
		 else
		 {
		    
		       document.getElementById('spnDRIVER_INCOME').style.display="none";
		       document.getElementById('spnNO_DEPENDENTS').style.display="none";
		  }	
		  if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1' )
			{
				if(document.getElementById('hidOldData').value != "")
				{
					//Enabling the activate deactivate button
					if(document.getElementById('btnActivateDeactivate'))
					document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
					if(document.getElementById('btnDelete'))
					document.getElementById('btnDelete').setAttribute('disabled',false); 
										
					//Populating the controls values
					populateFormData(document.getElementById('hidOldData').value,APP_DRIVER_DETAILS);
					populateInfo();
					SSN_hide();
					driverDiscount(1);
				}
				else
				{
					AddData();
					driverDiscount(0);
				}
				
				ShowHideValues();
				if(document.getElementById('cmbOP_DRIVER_COST_GAURAD_AUX'))
				{
					if (document.getElementById('cmbOP_DRIVER_COST_GAURAD_AUX').options[document.getElementById('cmbOP_DRIVER_COST_GAURAD_AUX').selectedIndex].value == "1")
						document.getElementById('capCoastGuard').style.display="inline";
					else
						document.getElementById('capCoastGuard').style.display="none";
				}
				//return false;

			}
			
			//Added by Sibin on 12 Jan 09
			else
			{
				if(document.getElementById('hidSSN_NO').value != '')
					SSN_hide();
				else
					SSN_change();					
			
			}
			//Added till here
			
			//Making tabs
			//SetTab();	
			CheckForDriverType();
			CheckforCollegeStud();		
			fxnDisableDependents();
			setDriv_Lic_regexp(1);
			//driverDiscount(0);//Added by Sibin for Itrack Issue 5428 on 18 Feb 09
			//SSN_hide();
			return false;
			
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
		
		function CheckForDriverType()
		{
		//debugger;
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
				EnableDisableDescByDriver(document.getElementById('cmbVEHICLE_ID'),document.getElementById('rfvVEHICLE_ID'),document.getElementById('spnVEHICLE_ID'),false);			
				EnableDisableDescByDriver(document.getElementById('cmbAPP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvVEHICLE_DRIVER'),document.getElementById('spnVEHICLE_ID'),false);			
				EnableDisableDescByDriver(document.getElementById('txtDATE_LICENSED'),document.getElementById('rfvDATE_LICENSED'),document.getElementById('spnDATE_LICENSED'),false);			
				
				EnableDisableDescByDriver(document.getElementById('cmbDRIVER_VOLUNTEER_POLICE_FIRE'),document.getElementById('rfvDRIVER_VOLUNTEER_POLICE_FIRE'),document.getElementById('spnDRIVER_VOLUNTEER_POLICE_FIRE'),true);			
                EnableDisableDescByDriver(document.getElementById('cmbIN_MILITARY'),document.getElementById('rfvIN_MILITARY'),document.getElementById('spnIN_MILITARY'),true);	
                EnableDisableDescByDriver(document.getElementById('cmbDRIVER_STUD_DIST_OVER_HUNDRED'),document.getElementById('rfvDRIVER_STUD_DIST_OVER_HUNDRED'),document.getElementById('spnDRIVER_STUD_DIST_OVER_HUNDRED'),true);
				document.getElementById('vilation').style.display="none"
				document.getElementById('assignVehSec').style.display="none"
				document.getElementById('tbAssignVehSec').style.display="none"
				//document.getElementById('trVehField').style.display="none"	
				document.getElementById('capFORM_F95').style.display="none"
				document.getElementById('cmbFORM_F95').style.display="none"		
				document.getElementById('cmbFORM_F95').options.selectedIndex = -1;
				document.getElementById('spnFORM_F95').style.display="none"
				EnableValidator('rfvFORM_F95',false); 
				//Added for Itrack Issue 5444	
				document.getElementById("trDriverDis1").style.display = "none";
				document.getElementById("trDriverDis2").style.display = "none";
				document.getElementById("trFULL_TIME_STUDENT").style.display = "none";
				document.getElementById("spnFULL_TIME_STUDENT").style.display = "none";									   			
	   			document.getElementById("rfvFULL_TIME_STUDENT").setAttribute("enabled",false);
	   			document.getElementById("rfvFULL_TIME_STUDENT").setAttribute("isValid",false);
				document.getElementById("trDriverDis3").style.display = "none";	
				}
				else if( SelectedValue == '3477')//Excluded Driver Type
				{
				    EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'),document.getElementById('rfvDRIVER_LIC_STATE'),document.getElementById('spnDRIVER_LIC_STATE'),false);			
				    EnableDisableDescByDriver(document.getElementById('txtDRIVER_DRIV_LIC'),document.getElementById('rfvDRIVER_DRIV_LIC'),document.getElementById('spnDRIVER_DRIV_LIC'),false);			
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_DRINK_VIOLATION'),document.getElementById('rfvDRIVER_DRINK_VIOLATION'),document.getElementById('spnDRIVER_DRINK_VIOLATION'),false);			
					EnableDisableDescByDriver(document.getElementById('cmbVEHICLE_ID'),document.getElementById('rfvVEHICLE_ID'),document.getElementById('spnVEHICLE_ID'),false);			
					EnableDisableDescByDriver(document.getElementById('cmbAPP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvVEHICLE_DRIVER'),document.getElementById('spnVEHICLE_ID'),false);			
					EnableDisableDescByDriver(document.getElementById('txtDATE_LICENSED'),document.getElementById('rfvDATE_LICENSED'),document.getElementById('spnDATE_LICENSED'),false);			
				    //Added by Sibin for Itrack Issue 5424 on 9 Feb 09
				    EnableDisableDescByDriver(document.getElementById('cmbDRIVER_VOLUNTEER_POLICE_FIRE'),document.getElementById('rfvDRIVER_VOLUNTEER_POLICE_FIRE'),document.getElementById('spnDRIVER_VOLUNTEER_POLICE_FIRE'),false);			
                    EnableDisableDescByDriver(document.getElementById('cmbIN_MILITARY'),document.getElementById('rfvIN_MILITARY'),document.getElementById('spnIN_MILITARY'),false);	
                    EnableDisableDescByDriver(document.getElementById('cmbDRIVER_STUD_DIST_OVER_HUNDRED'),document.getElementById('rfvDRIVER_STUD_DIST_OVER_HUNDRED'),document.getElementById('spnDRIVER_STUD_DIST_OVER_HUNDRED'),false);
					document.getElementById('vilation').style.display="none"
					//document.getElementById('assignVehSec').style.display="none"
					document.getElementById('tbAssignVehSec').style.display="none"
					//document.getElementById('trVehField').style.display="none"
					document.getElementById('capFORM_F95').style.display="inline"
					document.getElementById('cmbFORM_F95').style.display="inline"
					document.getElementById('spnFORM_F95').style.display="inline"
					EnableValidator('rfvFORM_F95',true); 
					//Added for Itrack Issue 5444
					document.getElementById("trDriverDis1").style.display = "none";
					document.getElementById("trDriverDis2").style.display = "none";	
					document.getElementById("trFULL_TIME_STUDENT").style.display = "none";
					document.getElementById("spnFULL_TIME_STUDENT").style.display = "none";									   			
	   				document.getElementById("rfvFULL_TIME_STUDENT").setAttribute("enabled",false);
	   				document.getElementById("rfvFULL_TIME_STUDENT").setAttribute("isValid",false);
					document.getElementById("trDriverDis3").style.display = "none";							   			
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
					if(document.getElementById('hidCalledFrom').value == 'PPA' && document.getElementById("cmbVEHICLE_ID").length>0)
					{
						EnableDisableDescByDriver(document.getElementById('cmbVEHICLE_ID'),document.getElementById('rfvVEHICLE_ID'),document.getElementById('spnVEHICLE_ID'),true);			
						EnableDisableDescByDriver(document.getElementById('cmbAPP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvVEHICLE_DRIVER'),document.getElementById('spnVEHICLE_ID'),true);			
					} 
					else
					{
						EnableDisableDescByDriver(document.getElementById('cmbVEHICLE_ID'),document.getElementById('rfvVEHICLE_ID'),document.getElementById('spnVEHICLE_ID'),false);			
						EnableDisableDescByDriver(document.getElementById('cmbAPP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvVEHICLE_DRIVER'),document.getElementById('spnVEHICLE_ID'),false);			
					 
					} 
					EnableDisableDescByDriver(document.getElementById('txtDATE_LICENSED'),document.getElementById('rfvDATE_LICENSED'),document.getElementById('spnDATE_LICENSED'),true);					
					//Added by Sibin for Itrack Issue 5424 on 9 Feb 09
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_VOLUNTEER_POLICE_FIRE'),document.getElementById('rfvDRIVER_VOLUNTEER_POLICE_FIRE'),document.getElementById('spnDRIVER_VOLUNTEER_POLICE_FIRE'),true);			
                    EnableDisableDescByDriver(document.getElementById('cmbIN_MILITARY'),document.getElementById('rfvIN_MILITARY'),document.getElementById('spnIN_MILITARY'),true);	
                    EnableDisableDescByDriver(document.getElementById('cmbDRIVER_STUD_DIST_OVER_HUNDRED'),document.getElementById('rfvDRIVER_STUD_DIST_OVER_HUNDRED'),document.getElementById('spnDRIVER_STUD_DIST_OVER_HUNDRED'),true);
					document.getElementById('cmbDRIVER_STUD_DIST_OVER_HUNDRED').style.display="inline"
					document.getElementById('capDRIVER_STUD_DIST_OVER_HUNDRED').style.display="inline"
					document.getElementById('vilation').style.display="inline"
					//document.getElementById('assignVehSec').style.display="inline"
					document.getElementById('tbAssignVehSec').style.display="inline"
					if(document.getElementById('cmbVEHICLE_ID').options.length>0) //Driver is licenced and there are boats in the combo-box
						document.getElementById('trVehField').style.display="inline"
					document.getElementById('capFORM_F95').style.display="none"
					document.getElementById('cmbFORM_F95').style.display="none"
					document.getElementById('cmbFORM_F95').options.selectedIndex = -1;
					document.getElementById('spnFORM_F95').style.display="none"
					EnableValidator('rfvFORM_F95',false); 
					//Added for Itrack Issue 5444
					document.getElementById("trDriverDis1").style.display = "inline";
					document.getElementById("trDriverDis2").style.display = "inline";
					document.getElementById("trFULL_TIME_STUDENT").style.display = "inline";
					document.getElementById("spnFULL_TIME_STUDENT").style.display = "inline";									   			
	   				document.getElementById("rfvFULL_TIME_STUDENT").setAttribute("enabled",true);
	   				document.getElementById("rfvFULL_TIME_STUDENT").setAttribute("isValid",true);
					document.getElementById("trDriverDis3").style.display = "inline";
				}
				else 
				{
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'),document.getElementById('rfvDRIVER_LIC_STATE'),document.getElementById('spnDRIVER_LIC_STATE'),false);			
					EnableDisableDescByDriver(document.getElementById('txtDRIVER_DRIV_LIC'),document.getElementById('rfvDRIVER_DRIV_LIC'),document.getElementById('spnDRIVER_DRIV_LIC'),false);			
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'),document.getElementById('rfvDRIVER_LIC_STATE'),document.getElementById('spnDRIVER_LIC_STATE'),false);			
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_DRINK_VIOLATION'),document.getElementById('rfvDRIVER_DRINK_VIOLATION'),document.getElementById('spnDRIVER_DRINK_VIOLATION'),false);			
					EnableDisableDescByDriver(document.getElementById('cmbVEHICLE_ID'),document.getElementById('rfvVEHICLE_ID'),document.getElementById('spnVEHICLE_ID'),false);			
					EnableDisableDescByDriver(document.getElementById('cmbAPP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvVEHICLE_DRIVER'),document.getElementById('spnVEHICLE_ID'),false);			
					EnableDisableDescByDriver(document.getElementById('txtDATE_LICENSED'),document.getElementById('rfvDATE_LICENSED'),document.getElementById('spnDATE_LICENSED'),false);			
					//Added by Sibin for Itrack Issue 5424 on 9 Feb 09
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_VOLUNTEER_POLICE_FIRE'),document.getElementById('rfvDRIVER_VOLUNTEER_POLICE_FIRE'),document.getElementById('spnDRIVER_VOLUNTEER_POLICE_FIRE'),true);			
                    EnableDisableDescByDriver(document.getElementById('cmbIN_MILITARY'),document.getElementById('rfvIN_MILITARY'),document.getElementById('spnIN_MILITARY'),true);	
                    EnableDisableDescByDriver(document.getElementById('cmbDRIVER_STUD_DIST_OVER_HUNDRED'),document.getElementById('rfvDRIVER_STUD_DIST_OVER_HUNDRED'),document.getElementById('spnDRIVER_STUD_DIST_OVER_HUNDRED'),true);
					document.getElementById('vilation').style.display="none"
					//document.getElementById('assignVehSec').style.display="none"
					document.getElementById('tbAssignVehSec').style.display="none"
					//document.getElementById('trVehField').style.display="none"
					document.getElementById('capFORM_F95').style.display="none"
					document.getElementById('cmbFORM_F95').style.display="none"
					document.getElementById('cmbFORM_F95').options.selectedIndex = -1;
					document.getElementById('spnFORM_F95').style.display="none"
					EnableValidator('rfvFORM_F95',false); 
				}	 
		    }	
		    else
		    {
		        EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'),document.getElementById('rfvDRIVER_LIC_STATE'),document.getElementById('spnDRIVER_LIC_STATE'),false);			
				EnableDisableDescByDriver(document.getElementById('txtDRIVER_DRIV_LIC'),document.getElementById('rfvDRIVER_DRIV_LIC'),document.getElementById('spnDRIVER_DRIV_LIC'),false);			
				EnableDisableDescByDriver(document.getElementById('cmbDRIVER_LIC_STATE'),document.getElementById('rfvDRIVER_LIC_STATE'),document.getElementById('spnDRIVER_LIC_STATE'),false);			
				EnableDisableDescByDriver(document.getElementById('cmbDRIVER_DRINK_VIOLATION'),document.getElementById('rfvDRIVER_DRINK_VIOLATION'),document.getElementById('spnDRIVER_DRINK_VIOLATION'),false);			
				EnableDisableDescByDriver(document.getElementById('cmbVEHICLE_ID'),document.getElementById('rfvVEHICLE_ID'),document.getElementById('spnVEHICLE_ID'),false);			
				EnableDisableDescByDriver(document.getElementById('cmbAPP_VEHICLE_PRIN_OCC_ID'),document.getElementById('rfvVEHICLE_DRIVER'),document.getElementById('spnVEHICLE_ID'),false);			
				EnableDisableDescByDriver(document.getElementById('txtDATE_LICENSED'),document.getElementById('rfvDATE_LICENSED'),document.getElementById('spnDATE_LICENSED'),false);			
				document.getElementById('vilation').style.display="none"
				//document.getElementById('assignVehSec').style.display="none"
				document.getElementById('tbAssignVehSec').style.display="none"
				document.getElementById('capFORM_F95').style.display="none"
				document.getElementById('cmbFORM_F95').style.display="none"
				document.getElementById('cmbFORM_F95').options.selectedIndex = -1;
				document.getElementById('spnFORM_F95').style.display="none"
				EnableValidator('rfvFORM_F95',false); 
				//document.getElementById('trVehField').style.display="none"
		    }	 
			ChangeColor();
		
			HideShowAssignedVehicleSection();
		return false;
						
		}
		function CheckforCollegeStud()
		{
			var selectedvalue;
			if (document.getElementById('cmbDRIVER_STUD_DIST_OVER_HUNDRED').selectedIndex != '-1')	
			{
				SelectedValue=document.getElementById('cmbDRIVER_STUD_DIST_OVER_HUNDRED').selectedIndex;
				if(SelectedValue == '2')
				{
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_STUD_DIST_OVER_HUNDRED'),document.getElementById('rfvPARENTS_INSURANCE'),document.getElementById('spnPARENTS_INSURANCE'),true); 
				}
				else
				{	//Commented on 25 AUgust 2008 Itrack 4537
					//EnableDisableDescByDriver(document.getElementById('cmbDRIVER_STUD_DIST_OVER_HUNDRED'),document.getElementById('rfvPARENTS_INSURANCE'),document.getElementById('spnPARENTS_INSURANCE'),false); 
				}
			}
			else
			{
					EnableDisableDescByDriver(document.getElementById('cmbDRIVER_STUD_DIST_OVER_HUNDRED'),document.getElementById('rfvPARENTS_INSURANCE'),document.getElementById('spnPARENTS_INSURANCE'),false); 
			}
		}
		function ShowHideValues()
		{
			if (document.getElementById('cmbSAFE_DRIVER_RENEWAL_DISCOUNT').options[document.getElementById('cmbSAFE_DRIVER_RENEWAL_DISCOUNT').selectedIndex].value == "1")
				document.getElementById('capSAFE_DRIVER_RENEWAL_DISCOUNT').style.display="inline";
			else
				document.getElementById('capSAFE_DRIVER_RENEWAL_DISCOUNT').style.display="none";
					
			if (document.getElementById('cmbDRIVER_GOOD_STUDENT').options[document.getElementById('cmbDRIVER_GOOD_STUDENT').selectedIndex].value == "1")
				document.getElementById('capGoodStudent').style.display="inline"
			else
				document.getElementById('capGoodStudent').style.display="none"
			
			if (document.getElementById('cmbDRIVER_PREF_RISK').options[document.getElementById('cmbDRIVER_PREF_RISK').selectedIndex].value == "1")
				document.getElementById('capPremierDriver').style.display="inline"
			else
				document.getElementById('capPremierDriver').style.display="none"
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
		
		
		function ChkDateOfOrder(objSource , objArgs)
		{
			var expdate=document.APP_DRIVER_DETAILS.txtDATE_ORDERED.value;
			objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);
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
		


		
		/*
		
		
		function GenerateDriverCode(Ctrl)
		{
			if (document.getElementById('hidDRIVER_ID').value == "New")
			{
				if (Ctrl == "txtDRIVER_LNAME")
					{
					if (document.getElementById("txtDRIVER_FNAME").value + document.getElementById("txtDRIVER_LNAME").value != "")
						{
							document.getElementById("txtDRIVER_CODE").value = document.getElementById("txtDRIVER_FNAME").value.substring(0,2) +
																	document.getElementById("txtDRIVER_LNAME").value.substring(0,2)+
																	"000001";
						}
					}
					else
					{
					if (Ctrl == "txtDRIVER_FNAME")
					{
					 if (document.getElementById("txtDRIVER_FNAME").value != "")
						{
							document.getElementById("txtDRIVER_CODE").value = document.getElementById("txtDRIVER_FNAME").value.substring(0,4) +
																		"000001";
						}
					}
					
					}
			  }
		}
		*/
		//Generate Driver Code
		function GenerateDriverCode(Ctrl)
		{			
			document.getElementById('txtDRIVER_CODE').value=(GenerateRandomCode(document.getElementById('txtDRIVER_FNAME').value,document.getElementById('txtDRIVER_LNAME').value));
		}
		
		
		function FillCustomerName()
		{	
				//debugger;	
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
						//cmbDRIVER_STATE
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
					  
					  //--------------------Added by Swarup on 15/12/2006-------------.				
				case "VIOLATIONS":
					  SelectComboOption("cmbVIOLATIONS",nodeValue)
					  break;
				case "MVR_ORDERED":
					  SelectComboOption("cmbMVR_ORDERED",nodeValue)
					  break;
				case "DATE_ORDERED":
					  document.getElementById('txtDATE_ORDERED').value = nodeValue;
					  break;
					  	
				case "DATE_OF_BIRTH":
					  document.getElementById('txtDRIVER_DOB').value = nodeValue;
					  break;	
				case "MARITAL_STATUS":
					  document.getElementById('cmbDRIVER_MART_STAT').value = nodeValue;
					  break;	
				 case "SSN_NO":
					  //document.getElementById('txtDRIVER_SSN').value = nodeValue;
					  document.getElementById('hidSSN_NO').value = nodeValue;
					  break;	
			
				 case "DECRYPT_SSN_NO":
					  //document.getElementById('txtDRIVER_SSN').value = nodeValue;
					  document.getElementById('capSSN_NO_HID').innerText = nodeValue;
					  break;	
					  
				   //Commented By Sibin on 26 Nov 08 for Itrack Issue 5060
				   
		    	  /*case "EMPLOYER_NAME":
					  document.getElementById('txtDRIVER_DRIVERLOYER_NAME').value = nodeValue;
					  break;	
				   case "EMPLOYER_ADDRESS":
					  document.getElementById('txtDRIVER_DRIVERLOYER_ADD').value = nodeValue;
					  break;	*/
				 case "GENDER":	
						SelectComboOption("cmbDRIVER_SEX",nodeValue);
						break;
					  
					 }	
				}		
				ChangeColor();
				DisableValidators();	
				EnableDisableControls();
				FetchAjaxResponse(); //While Fetching Cust Data	and Set DropDowns
				return false;
							
			}	
		
		function CoypApplicationDrivers()
		{			
			/*var customerid = '';
			var appid='';
			var appversionid='';
			var calledfrom='';
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
			if (document.getElementById('hidCalledFrom')!=null)
			{
				calledfrom = document.getElementById('hidCalledFrom').value;
			}
			var clFrom='<%=strCalledFrom%>';
			window.open('AddCurrentAppExistingDriver.aspx?CalledFrom='+ clFrom +'&CUSTOMER_ID='+customerid+'&APP_ID='+appid+'&APP_VERSION_ID='+appversionid ,'DriverDetails',600,300,'Yes','Yes','No','No','No');	
			return false;*/
		}
		function AddLookUpValue(lookupname)
		{	
			window.open('/cms/cmsweb/maintenance/AddLookup.aspx?LookUpName=' +lookupname+ '&CalledForEnter=true','DriverDetails',600,300,'Yes','Yes','No','No','No');	
			return false;
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

	function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbRELATIONSHIP":
						lookupMessage	=	"DRACD.";
						break;
					case "cmbDRIVER_DRIV_TYPE":
						lookupMessage	=	"DRTCD.";
						break;
					case "cmbDRIVER_OCC_CLASS":
						lookupMessage	=	"OCCCL.";
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
			
			function ValidateDiscountType(source, arguments)
			{
				var cnt=0;
				if(document.getElementById('chkSAFE_DRIVER_RENEWAL_DISCOUNT').checked || document.getElementById('chkDRIVER_GOOD_STUDENT').checked || document.getElementById('chkDRIVER_PREF_RISK').checked)
				{
					cnt=1;
				}
			
				if(cnt!=1)
				{
					arguments.IsValid = false;
					return;   
				}				
			
			}

		function Redirect()
		{			
			var calledFrom = new String(document.getElementById('hidCalledFrom').value);			
			top.frames[1].location='../PolicyVehicleIndex.aspx?CalledFrom=' + calledFrom.toUpperCase() + '&';
		}
		function RedirectToBoat()
		{			
			var calledFrom = new String(document.getElementById('hidCalledFrom').value);			
			top.frames[1].location='../Watercraft/PolicyWatercraftIndex.aspx?CalledFrom=' + calledFrom.toUpperCase() + '&';
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
				
				
				if(document.getElementById('cmbSAFE_DRIVER_RENEWAL_DISCOUNT').options[document.getElementById('cmbSAFE_DRIVER_RENEWAL_DISCOUNT').selectedIndex].value=="1")				
					document.getElementById('capSAFE_DRIVER_RENEWAL_DISCOUNT').style.display="inline";									
				else				
					document.getElementById('capSAFE_DRIVER_RENEWAL_DISCOUNT').style.display="none";						
						
					if(document.getElementById('cmbDRIVER_GOOD_STUDENT').options[document.getElementById('cmbDRIVER_GOOD_STUDENT').selectedIndex].value==1)
						document.getElementById('capGoodStudent').style.display="inline";
					else					
						document.getElementById('capGoodStudent').style.display="none";
						
					
					if(document.getElementById('cmbDRIVER_PREF_RISK')[document.getElementById('cmbDRIVER_PREF_RISK').selectedIndex].value=="1")									
						document.getElementById('capPremierDriver').style.display="inline";
					else
						document.getElementById('capPremierDriver').style.display="none";
					EnableDisableControls();
					CheckForDriverType();
					CheckforCollegeStud();
					return false;	
				
		}
		
		
		function ShowDiscountPercentage(objDropDownListID, objLabelID)
		{
		
			if (document.getElementById(objDropDownListID).options[document.getElementById(objDropDownListID).selectedIndex].value == "1")
				document.getElementById(objLabelID).style.display="inline";
			else
				document.getElementById(objLabelID).style.display="none";	
			
			if(objDropDownListID=="cmbDRIVER_GOOD_STUDENT")
			{
				ShowHideFullTimeStudentRow(true);
			}
		}
		function ShowHideFullTimeStudentRow(flag)
		{
			
			if(document.getElementById('hidCalledFrom').value=='PPA' && flag && document.getElementById("cmbDRIVER_GOOD_STUDENT").options[document.getElementById("cmbDRIVER_GOOD_STUDENT").selectedIndex].value=="1")				
			{
				document.getElementById("trFULL_TIME_STUDENT").style.display = "inline";
				document.getElementById("spnFULL_TIME_STUDENT").style.display = "inline";									   			
	   			document.getElementById("rfvFULL_TIME_STUDENT").setAttribute("enabled",true);
	   			document.getElementById("rfvFULL_TIME_STUDENT").setAttribute("isValid",true);				
				cmbFULL_TIME_STUDENT_Change(true);
			}
			else
			{
				document.getElementById("trFULL_TIME_STUDENT").style.display = "none";
				document.getElementById("spnFULL_TIME_STUDENT").style.display = "none";									   			
	   			document.getElementById("rfvFULL_TIME_STUDENT").setAttribute("enabled",false);
	   			document.getElementById("rfvFULL_TIME_STUDENT").setAttribute("isValid",false);			
				cmbFULL_TIME_STUDENT_Change(false);
			}
		}
		function EnableDisableControls()
		{	
			ShowHideValues();
			if(document.getElementById('hidCalledFrom').value=='PPA' || document.getElementById('hidCalledFrom').value=='ppa')
			{	
				
				//var dtCurrentDate=new Date();				
				//Set the two dates
				//var millennium =new Date(2000, 0, 1) //Month is 0-11 in JavaScript
				//today=new Date()
				//Get 1 day in milliseconds
				//var one_day=1000*60*60*24

				var dtDOB=new Date(FormatDateForGrid(document.getElementById('txtDRIVER_DOB'),'##/##/####'));
				
				//Calculate difference btw the two dates, and convert to days
				//var diff=Math.ceil((today.getTime()-dtDOB.getTime())/(one_day));
				//diff = diff/366;		
				//var dtCurrentDate=new Date();				
				
				//strDOB=FormatDateForGrid(document.getElementById('txtDRIVER_DOB'),'##/##/####');				
				//alert(document.getElementById('txtDRIVER_DOB'));
				//alert(FormatDateForGrid(document.getElementById('txtDRIVER_DOB'),'##/##/####'));
				//return;
				//var dtDOB=new Date(FormatDateForGrid(document.getElementById('txtDRIVER_DOB'),'##/##/####'));
				
				//if((dtCurrentDate.getFullYear()-dtDOB.getFullYear())>25)												
				var diff = DateDiffernce(dtDOB,varPolEffDate);
				
				if(diff >= 25 )
				{
					EnableDisableDesc(document.getElementById('cmbDRIVER_STUD_DIST_OVER_HUNDRED'),document.getElementById('capDRIVER_STUD_DIST_OVER_HUNDRED'),document.getElementById('rfvDRIVER_STUD_DIST_OVER_HUNDRED'),false);
					EnableDisableDesc(document.getElementById('cmbDRIVER_GOOD_STUDENT'),document.getElementById('capDRIVER_GOOD_STUDENT'),null,false);
					ShowHideFullTimeStudentRow(false);						
					DisplayMilitary(false);
					ShowParentsInsurance(false);
				}
				else
				{					
					EnableDisableDesc(document.getElementById('cmbDRIVER_STUD_DIST_OVER_HUNDRED'),document.getElementById('capDRIVER_STUD_DIST_OVER_HUNDRED'),document.getElementById('rfvDRIVER_STUD_DIST_OVER_HUNDRED'),true);
					EnableDisableDesc(document.getElementById('cmbDRIVER_GOOD_STUDENT'),document.getElementById('capDRIVER_GOOD_STUDENT'),null,true);
					ShowHideFullTimeStudentRow(true);						
					DisplayMilitary(true);
					ShowParentsInsurance(true);
					//Added by Sibin for Itrack Issue 5424 on 9 Feb 09
					CheckForDriverType();
				}	
				
				if (diff>19)
				{
					document.getElementById('cmbDRIVER_PREF_RISK').style.display='inline';
					document.getElementById('capDRIVER_PREF_RISK').style.display='inline';
					if (document.getElementById('cmbDRIVER_PREF_RISK').options[document.getElementById('cmbDRIVER_PREF_RISK').selectedIndex].value == "1")
						document.getElementById('capPremierDriver').style.display="inline"
					else
						document.getElementById('capPremierDriver').style.display="none"
				}
				else if(diff>0 && diff<19)
				{
					document.getElementById('cmbDRIVER_PREF_RISK').style.display= 'none';
					document.getElementById('capDRIVER_PREF_RISK').style.display='none';
					document.getElementById('capPremierDriver').style.display="none"					
				}	
				
				else
				{
					document.getElementById('cmbDRIVER_PREF_RISK').style.display= 'inline';
					document.getElementById('capDRIVER_PREF_RISK').style.display='inline';
					document.getElementById('capPremierDriver').style.display="inline"					
				}				
				//Put the date difference in hidden variable
				document.getElementById("hidDateDiff").value = diff;
				if (document.getElementById('cmbDRIVER_STUD_DIST_OVER_HUNDRED').selectedIndex<1)			
					document.getElementById('rfvDRIVER_STUD_DIST_OVER_HUNDRED').style.display="none";
					
				//if((dtCurrentDate.getFullYear()-dtDOB.getFullYear()) < <%=strWaiverBenefitsLimit%>)					
				if((diff >= <%=strWaiverBenefitsLimit%>) && document.getElementById("hidState_id").value=="22")
				{
					EnableDisableDesc(document.getElementById('cmbWAIVER_WORK_LOSS_BENEFITS'),document.getElementById('capWAIVER_WORK_LOSS_BENEFITS'),null,true);				
					cmbWAIVER_WORK_LOSS_BENEFITS_Change(true);
				}
				else				
				{
					EnableDisableDesc(document.getElementById('cmbWAIVER_WORK_LOSS_BENEFITS'),document.getElementById('capWAIVER_WORK_LOSS_BENEFITS'),null,false);				
					document.getElementById('cmbWAIVER_WORK_LOSS_BENEFITS').options.selectedIndex = 1;		
					cmbWAIVER_WORK_LOSS_BENEFITS_Change(false);
				}
			}	
			else
			{//Don't display for Umbrella and others			
				EnableDisableDesc(document.getElementById('cmbWAIVER_WORK_LOSS_BENEFITS'),document.getElementById('capWAIVER_WORK_LOSS_BENEFITS'),null,false);				
				ShowHideFullTimeStudentRow(false);
				cmbWAIVER_WORK_LOSS_BENEFITS_Change(false);
				DisplayMilitary(false);
				ShowParentsInsurance(false);
			}		
		}
		function DisplayMilitary(flag)
		{
			if(flag)
			{
				document.getElementById("rowIN_MILITARY").style.display="inline";				
				document.getElementById("rfvIN_MILITARY").setAttribute("enabled",true);
				document.getElementById("rfvIN_MILITARY").setAttribute("isValid",true);				
			}
			else
			{
				document.getElementById("rowIN_MILITARY").style.display="none";
				document.getElementById("rfvIN_MILITARY").style.display="none";
				document.getElementById("rfvIN_MILITARY").setAttribute("enabled",false);
				document.getElementById("rfvIN_MILITARY").setAttribute("isValid",false);
			}
			fxnDisplayFields(flag);
		}
		function EnableDisableDesc(txtDesc,lblNA,rfvDesc,flag)
		{		
				
				if (flag==false)
				{			
					
					if(rfvDesc!=null)
					{
						rfvDesc.setAttribute('enabled',false);					
						rfvDesc.setAttribute('isValid',false);
						//rfvDesc.style.display = "none";
						document.getElementById('spnDRIVER_STUD_DIST_OVER_HUNDRED').style.display="none";
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
						//rfvDesc.style.display = "inline";							
						//document.getElementById('capGoodStudent').style.display="inline"			
						document.getElementById('spnDRIVER_STUD_DIST_OVER_HUNDRED').style.display="inline";
					}
					txtDesc.style.display = "inline";
					lblNA.style.display = "inline";										
				}			
		}
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
		
		function fxnDisableDependents()
		{
			if(document.getElementById('cmbDRIVER_MART_STAT').options.selectedIndex != "-1")
			{ 
				if(document.getElementById("hidCalledFrom").value == "PPA" && document.getElementById("hidState_id").value=="22")
				{
					if(document.getElementById('cmbNO_DEPENDENTS'))
					{
						var cmbValue = document.getElementById('cmbDRIVER_MART_STAT').selectedIndex;

						if(cmbValue == "2" || cmbValue == "3")
						{
							document.getElementById('cmbNO_DEPENDENTS').options.selectedIndex = 1;
							document.getElementById('cmbNO_DEPENDENTS').setAttribute("disabled",true);
							if(document.getElementById('cmbNO_DEPENDENTS').selectedIndex!="-1")
							{
								var noOfDependents= document.getElementById('cmbNO_DEPENDENTS').options[document.getElementById('cmbNO_DEPENDENTS').selectedIndex].value;
								document.getElementById('hidNO_DEPENDENTS').value = noOfDependents;
							}


						}
						else
						{

							document.getElementById('cmbNO_DEPENDENTS').setAttribute("disabled",false);
							if(document.getElementById('cmbNO_DEPENDENTS').selectedIndex!="-1")
							{
								var noOfDependents= document.getElementById('cmbNO_DEPENDENTS').options[document.getElementById('cmbNO_DEPENDENTS').selectedIndex].value;
								document.getElementById('hidNO_DEPENDENTS').value = noOfDependents;
							}


						}
					}	
					if(document.getElementById('cmbNO_DEPENDENTS').selectedIndex!="-1")
						document.getElementById('hidNO_DEPENDENTS').value = document.getElementById('cmbNO_DEPENDENTS').options[document.getElementById('cmbNO_DEPENDENTS').selectedIndex].value;
				}
			} 
			if(document.getElementById('cmbNO_DEPENDENTS').selectedIndex!="-1")
						document.getElementById('hidNO_DEPENDENTS').value = document.getElementById('cmbNO_DEPENDENTS').options[document.getElementById('cmbNO_DEPENDENTS').selectedIndex].value;
			
		}
	   	function cmbFULL_TIME_STUDENT_Change(flag)
	   	{
	   		
	   		if(document.getElementById("hidCalledFrom").value == "PPA" && flag && document.getElementById("cmbFULL_TIME_STUDENT").selectedIndex !="-1" && document.getElementById("cmbFULL_TIME_STUDENT").options[document.getElementById("cmbFULL_TIME_STUDENT").selectedIndex].value=="1")				
	   		{
	   			document.getElementById("cmbSUPPORT_DOCUMENT").style.display = "inline";								
	   			document.getElementById("capSUPPORT_DOCUMENT").style.display = "inline";									   			
	   			document.getElementById("spnSUPPORT_DOCUMENT").style.display = "inline";									   			
	   			document.getElementById("rfvSUPPORT_DOCUMENT").setAttribute("enabled",true);
	   			document.getElementById("rfvSUPPORT_DOCUMENT").setAttribute("isValid",true);
	   		}
			else
			{
				document.getElementById("cmbSUPPORT_DOCUMENT").style.display = "none";
				document.getElementById("capSUPPORT_DOCUMENT").style.display = "none";
				document.getElementById("spnSUPPORT_DOCUMENT").style.display = "none";
				document.getElementById("rfvSUPPORT_DOCUMENT").setAttribute("enabled",false);
	   			document.getElementById("rfvSUPPORT_DOCUMENT").setAttribute("isValid",false);
	   			document.getElementById("rfvSUPPORT_DOCUMENT").style.display = "none";
			}
		}
		function cmbWAIVER_WORK_LOSS_BENEFITS_Change(flag)
		{
			if(document.getElementById("hidCalledFrom").value == "PPA" && flag && document.getElementById("cmbWAIVER_WORK_LOSS_BENEFITS").options[document.getElementById("cmbWAIVER_WORK_LOSS_BENEFITS").selectedIndex].value=="1")
			{
				document.getElementById("cmbSIGNED_WAIVER_BENEFITS_FORM").style.display = "inline";
				document.getElementById("capSIGNED_WAIVER_BENEFITS_FORM").style.display = "inline";
				//Added by Asfa(18-July-2008) - iTrack #4526
				document.getElementById("spnSIGNED_WAIVER_BENEFITS_FORM").style.display = "inline";
			}
			else
			{
				document.getElementById("cmbSIGNED_WAIVER_BENEFITS_FORM").style.display = "none";
				document.getElementById("capSIGNED_WAIVER_BENEFITS_FORM").style.display = "none";
				//Added by Asfa(18-July-2008) - iTrack #4526
				document.getElementById("spnSIGNED_WAIVER_BENEFITS_FORM").style.display = "none";
			}
		}
		// Added by Swarup For checking zip code for LOB: Start
	function GetZipForState_OLD()
		{		    
			GlobalError=true;
			if(document.getElementById('cmbDRIVER_STATE').value==14 ||document.getElementById('cmbDRIVER_STATE').value==22||document.getElementById('cmbDRIVER_STATE').value==49)
			{ 
				if(document.getElementById('txtDRIVER_ZIP').value!="")
				{
					var intStateID = document.getElementById('cmbDRIVER_STATE').options[document.getElementById('cmbDRIVER_STATE').options.selectedIndex].value;
					var strZipID = document.getElementById('txtDRIVER_ZIP').value;	
					var co=myTSMain1.createCallOptions();
					co.funcName = "FetchZipForState";
					co.async = false;
					co.SOAPHeader= new Object();
					var oResult = myTSMain1.FetchZip.callService(co,intStateID,strZipID);
					handleResult(oResult);	
					if(GlobalError)
					{
						return false;
					}
					else
					{
						return true;
					}
				}	
				return false;
			}
			else 
				return true;		
		}
		/////ZIP AJAX CALL///
		function GetZipForState()
		{		    
			GlobalError=true;
			if(document.getElementById('cmbDRIVER_STATE').value==14 ||document.getElementById('cmbDRIVER_STATE').value==22||document.getElementById('cmbDRIVER_STATE').value==49)
			{ 
				if(document.getElementById('txtDRIVER_ZIP').value!="")
				{
					var intStateID = document.getElementById('cmbDRIVER_STATE').options[document.getElementById('cmbDRIVER_STATE').options.selectedIndex].value;
					var strZipID = document.getElementById('txtDRIVER_ZIP').value;	
					var result = AddPolicyDriver.AjaxFetchZipForState(intStateID,strZipID);
					return AjaxCallFunction_CallBack_Zip(result);
					
				}	
				return false;
			}
			else 
				return true;
				
		}
		function AjaxCallFunction_CallBack_Zip(response)
		{		
		  if(document.getElementById('cmbDRIVER_STATE').value==14 ||document.getElementById('cmbDRIVER_STATE').value==22||document.getElementById('cmbDRIVER_STATE').value==49)
			{ 
				if(document.getElementById('txtDRIVER_ZIP').value!="")
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
				}	
				return false;
			}
			else 
				return true;		
		}
		
		//////AJAX END/////
			
		function ChkResult(objSource , objArgs)
		{
			objArgs.IsValid = true;
			if(objArgs.IsValid == true)
			{
				objArgs.IsValid = GetZipForState();
				//objArgs.IsValid = true;
				//alert(objArgs.IsValid);
				if(objArgs.IsValid == false)
					document.getElementById('csvDRIVER_ZIP').innerHTML = "The zip code does not belong to the state";				
			}
			return;
			if(GlobalError==true)
			{
				Page_IsValid = false;
				objArgs.IsValid = false;
			}
			else
			{
				objArgs.IsValid = true;				
			}
			document.getElementById("btnSave").click();
		}
				
		function handleResult(res) 
		{
		if(!res.error)
			{
			if (res.value!="" && res.value!=null ) 
				{
					GlobalError=false;
				}
				else
				{
					GlobalError=true;
				}
			}
			else
			{
				GlobalError=true;		
			}
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
		
	// Added by Swarup For checking zip code for LOB: End 
		function focusGender()
		{
			document.getElementById('cmbDRIVER_SEX').focus();
		}
		function focusType()
		{
			if (document.getElementById('capFORM_F95').style.display == 'inline')
				document.getElementById('cmbFORM_F95').focus();
			else
				document.getElementById('cmbDRIVER_LIC_STATE').focus();
		}
		 function SSN_change()
			{
				document.getElementById("capSSN_NO_HID").style.display = 'inline';
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
			function setEncryptFields()
			{
				if(document.getElementById('txtDRIVER_SSN').value != '')
				{
					var txtval = document.getElementById('txtDRIVER_SSN').value;
					var txtvaln = 'xxx-xx-';
					txtvaln += txtval.substring(txtvaln.length, txtval.length);
					document.getElementById('txtSSN_NO_HID').value = txtvaln;

					document.getElementById('txtSSN_NO_HID').style.display= "inline" ;
					document.getElementById('txtDRIVER_SSN').style.display= "none" ;
				}
				else
				{
					document.getElementById('txtSSN_NO_HID').style.display= "none" ;
					document.getElementById('txtSSN_NO').style.display= "inline" ;
				}
			}
		
		//Added by Sibin for Itrack Issue 5428 on 18 Feb 09		
   function driverDiscount(flag)
	{
	  if(flag==0)
	  { 
		if(varYearsWithWolverine == 1)
		{
			document.getElementById('cmbSAFE_DRIVER_RENEWAL_DISCOUNT').options.selectedIndex=1;
			document.getElementById('cmbDRIVER_PREF_RISK').options.selectedIndex=0;
			document.getElementById("cmbSAFE_DRIVER_RENEWAL_DISCOUNT").setAttribute('disabled',false);
			document.getElementById("cmbDRIVER_PREF_RISK").setAttribute('disabled',true);
			document.getElementById("cmbDRIVER_PREF_RISK").setAttribute('visible',true);
			document.getElementById('capPremierDriver').style.display="none";
		}
		else
		{
				document.getElementById('cmbSAFE_DRIVER_RENEWAL_DISCOUNT').options.selectedIndex=0;
				document.getElementById('cmbDRIVER_PREF_RISK').options.selectedIndex=1;
				document.getElementById("cmbSAFE_DRIVER_RENEWAL_DISCOUNT").setAttribute('disabled',true);
				document.getElementById("cmbDRIVER_PREF_RISK").setAttribute('disabled',false);
				document.getElementById("cmbDRIVER_PREF_RISK").setAttribute('visible',true);
				document.getElementById('capSAFE_DRIVER_RENEWAL_DISCOUNT').style.display="none";
		}
	  }
	  
	  else
	  {
		 if(varYearsWithWolverine == 1)
		 {
			document.getElementById('cmbSAFE_DRIVER_RENEWAL_DISCOUNT').options.selectedIndex=document.getElementById('cmbSAFE_DRIVER_RENEWAL_DISCOUNT').options[document.getElementById('cmbSAFE_DRIVER_RENEWAL_DISCOUNT').options.selectedIndex].value;
			document.getElementById('cmbDRIVER_PREF_RISK').options.selectedIndex=0;
			document.getElementById("cmbSAFE_DRIVER_RENEWAL_DISCOUNT").setAttribute('disabled',false);
			document.getElementById("cmbDRIVER_PREF_RISK").setAttribute('disabled',true);
			document.getElementById("cmbDRIVER_PREF_RISK").setAttribute('visible',true);
			document.getElementById('capPremierDriver').style.display="none";
		 }
		else
		{
			document.getElementById('cmbSAFE_DRIVER_RENEWAL_DISCOUNT').options.selectedIndex=0;
			document.getElementById('cmbDRIVER_PREF_RISK').options.selectedIndex=document.getElementById('cmbDRIVER_PREF_RISK').options[document.getElementById('cmbDRIVER_PREF_RISK').options.selectedIndex].value;
			document.getElementById("cmbSAFE_DRIVER_RENEWAL_DISCOUNT").setAttribute('disabled',true);
			document.getElementById("cmbDRIVER_PREF_RISK").setAttribute('disabled',false);
			document.getElementById("cmbDRIVER_PREF_RISK").setAttribute('visible',true);
			document.getElementById('capSAFE_DRIVER_RENEWAL_DISCOUNT').style.display="none";
		}
	   
	  }
	  
	}
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();ChangeColor();EnableDisabletrLicMed();EnableDisableControls();fxnDisableDependents();fxnShwHAVE_CAR();driverDiscount(1);">
		<FORM id="APP_DRIVER_DETAILS" method="post" runat="server">		
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
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_CODE" runat="server">Code</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_CODE" runat="server" size="28" maxlength="20"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_CODE" runat="server" ControlToValidate="txtDRIVER_CODE" ErrorMessage="DRIVER_CODE can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revDRIVER_CODE" runat="server" ControlToValidate="txtDRIVER_CODE" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_SUFFIX" runat="server">Suffix</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_SUFFIX" runat="server" size="13" maxlength="10"></asp:textbox></TD>
								</tr>
								<tr>
									<td class="midcolora"><asp:label id="Label1" runat="server">Would you like to pull customer address</asp:label></td>
									<td class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnPullCustomerAddress" runat="server" Text="Pull Customer Address"></cmsb:cmsbutton></td>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnCopyDefaultCustomer" runat="server" Text="Copy Default Customer"></cmsb:cmsbutton></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_ADD1" runat="server">Address1</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_ADD1" runat="server" size="35" maxlength="70"></asp:textbox></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_ADD2" runat="server">Address2</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_ADD2" runat="server" size="35" maxlength="70"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_CITY" runat="server">City</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_CITY" runat="server" size="30" maxlength="40"></asp:textbox></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_COUNTRY" runat="server">Country</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_COUNTRY" onfocus="SelectComboIndex('cmbDRIVER_COUNTRY')" runat="server">
											<ASP:LISTITEM Value="0"></ASP:LISTITEM>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_COUNTRY" runat="server" ControlToValidate="cmbDRIVER_COUNTRY" ErrorMessage="DRIVER_COUNTRY can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_STATE" runat="server">State</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_STATE" onfocus="SelectComboIndex('cmbDRIVER_STATE')" runat="server">
											<ASP:LISTITEM Value="0"></ASP:LISTITEM>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_STATE" runat="server" ControlToValidate="cmbDRIVER_STATE" ErrorMessage="DRIVER_STATE can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_ZIP" runat="server">Zip</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_ZIP" runat="server" size="13" maxlength="10" onBlur="GetZipForState();"></asp:textbox>
									<%-- Added by Swarup on 30-mar-2007 --%>
										<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
										<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
										</asp:hyperlink>
										<BR>
										<asp:customvalidator id="csvDRIVER_ZIP" Runat="server" ClientValidationFunction="ChkResult" ErrorMessage=" "
												Display="Dynamic" ControlToValidate="txtDRIVER_ZIP"></asp:customvalidator><br>
										<asp:regularexpressionvalidator id="revDRIVER_ZIP" runat="server" ControlToValidate="txtDRIVER_ZIP" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator><asp:requiredfieldvalidator id="rfvDRIVER_ZIP" runat="server" ControlToValidate="txtDRIVER_ZIP" Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_HOME_PHONE" runat="server">Home Phone</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_HOME_PHONE" runat="server" size="15" maxlength="13"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revDRIVER_HOME_PHONE" runat="server" ControlToValidate="txtDRIVER_HOME_PHONE"
											ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_MOBILE" runat="server">Mobile Phone</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_MOBILE" runat="server" size="15" maxlength="13"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revDRIVER_MOBILE" runat="server" ControlToValidate="txtDRIVER_MOBILE" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_BUSINESS_PHONE" runat="server">Business Phone</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_BUSINESS_PHONE" runat="server" size="15" maxlength="13"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revDRIVER_BUSINESS_PHONE" runat="server" ControlToValidate="txtDRIVER_BUSINESS_PHONE"
											ErrorMessage="RegularExpressionValidator" Display="Dynamic"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_EXT" runat="server">Ext</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_EXT" onblur="CheckIfPhoneEmpty();" runat="server" size="4" maxlength="4"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revDRIVER_EXT" runat="server" ControlToValidate="txtDRIVER_EXT" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_DOB" runat="server">Date of Birth</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_DOB" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkOCCURENCE_DATE" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgOCCURENCE_DATE" runat="server" ImageUrl="../../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
										</asp:hyperlink>
										<asp:RequiredFieldValidator ID="rfvDRIVER_DOB" ControlToValidate="txtDRIVER_DOB" Runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
										<asp:regularexpressionvalidator id="revDRIVER_DOB" runat="server" ControlToValidate="txtDRIVER_DOB" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvDRIVER_DOB" ControlToValidate="txtDRIVER_DOB" Display="Dynamic" ClientValidationFunction="ChkDOB"
											Runat="server"></asp:customvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_SSN" runat="server">Social Security Number</asp:label></TD>
									<TD class="midcolora" width="32%">
										<asp:label id="capSSN_NO_HID" runat="server" size="14" maxlength="11"></asp:label>
										<input class="clsButton" id="btnSSN_NO" text="Edit" onclick="SSN_change();" type="button"></input>
									<asp:textbox id="txtDRIVER_SSN" runat="server" size="13" maxlength="11"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revDRIVER_SSN" runat="server" ControlToValidate="txtDRIVER_SSN" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_MART_STAT" runat="server">Marital Status</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_MART_STAT" onfocus="SelectComboIndex('cmbDRIVER_MART_STAT')" runat="server"
											onchange="fxnDisableDependents()">
											<ASP:LISTITEM></ASP:LISTITEM>
										</asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbDRIVER_MART_STAT')" onfocus ="focusGender();"></A></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_SEX" runat="server">Gender</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_SEX" onfocus="SelectComboIndex('cmbDRIVER_SEX')" runat="server">
											<ASP:LISTITEM Value="M">Male</ASP:LISTITEM>
											<ASP:LISTITEM Value="F">Female</ASP:LISTITEM>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_SEX" runat="server" ControlToValidate="cmbDRIVER_SEX" ErrorMessage="DRIVER_SEX can't be blank."
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_DRIV_TYPE" runat="server">Driver Type </asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_DRIV_TYPE" onfocus="SelectComboIndex('cmbDRIVER_DRIV_TYPE')" onchange="CheckForDriverType();EnableDisableControls();"
											runat="server">
											<ASP:LISTITEM Value="0"></ASP:LISTITEM>
										</asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbDRIVER_DRIV_TYPE')" onfocus = "focusType();"></A><BR>
										<asp:RequiredFieldValidator ID="rfvDRIVER_DRIV_TYPE" Runat="server" ControlToValidate="cmbDRIVER_DRIV_TYPE"
											ErrorMessage="DRIVER_DRIV_TYPE can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
									</TD>
									<td class="midcolora" width="18%" id="tdcapFORM_F95"><asp:label id="capFORM_F95" runat="server">Form F-95 Signed</asp:label><span id="spnFORM_F95" class="mandatory">*</span></td>
									<td class="midcolora" width="32%" id="tdcmbFORM_F95"><asp:dropdownlist id="cmbFORM_F95" onfocus="SelectComboIndex('cmbFORM_F95')" runat="server"></asp:dropdownlist>
									<asp:requiredfieldvalidator id="rfvFORM_F95" runat="server" Display="Dynamic" ErrorMessage="Please Select Form F-95 Signed."
											ControlToValidate="cmbFORM_F95"></asp:requiredfieldvalidator>
									</td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_LIC_STATE" runat="server">License State</asp:label><span class="mandatory" id="spnDRIVER_LIC_STATE">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_LIC_STATE" onfocus="SelectComboIndex('cmbDRIVER_LIC_STATE')" onChange="setDriv_Lic_regexp(2);" runat="server">
											<ASP:LISTITEM Value="0"></ASP:LISTITEM>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_LIC_STATE" runat="server" ControlToValidate="cmbDRIVER_LIC_STATE"
											ErrorMessage="DRIVER_LIC_STATE can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capEXT_NON_OWN_COVG_INDIVI" runat="server">Extended Non-Owned Coverage for Named Individual Required</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbEXT_NON_OWN_COVG_INDIVI" onfocus="SelectComboIndex('cmbDRIVER_LIC_STATE')"
											runat="server"></asp:dropdownlist></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDATE_LICENSED" runat="server">Date Experience Started </asp:label><span class="mandatory" id="spnDATE_LICENSED">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDATE_LICENSED" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkDATE_LICENSED" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgDATE_LICENSED" runat="server" ImageUrl="../../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
										</asp:hyperlink><asp:regularexpressionvalidator id="revDATE_LICENSED" runat="server" ControlToValidate="txtDATE_LICENSED" ErrorMessage="RegularExpressionValidator"
											Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvDATE_LICENSED" ControlToValidate="txtDATE_LICENSED" Display="Dynamic" ClientValidationFunction="ChkEXPDate"
											Runat="server"></asp:customvalidator><span id="spnDATE_EXP_DOB" style="DISPLAY: none; COLOR: red" runat="server"></span>
										<asp:RequiredFieldValidator ID="rfvDATE_LICENSED" Runat="server" Display="Dynamic" ErrorMessage="DATE_LICENSED can't be blank"
											ControlToValidate="txtDATE_LICENSED"></asp:RequiredFieldValidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_DRIV_LIC" runat="server">License Number</asp:label><span class="mandatory" id="spnDRIVER_DRIV_LIC">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_DRIV_LIC" runat="server" size="30" maxlength="30"></asp:textbox>
										<br>
										<asp:RequiredFieldValidator ID="rfvDRIVER_DRIV_LIC" ControlToValidate="txtDRIVER_DRIV_LIC" Runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
										<asp:regularexpressionvalidator id="revDRIVER_DRIV_LIC" runat="server" ControlToValidate="txtDRIVER_DRIV_LIC" ErrorMessage="RegularExpressionValidator"
							Display="Dynamic"></asp:regularexpressionvalidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_OCC_CLASS" runat="server">Occupation Class</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_OCC_CLASS" onfocus="SelectComboIndex('cmbDRIVER_OCC_CLASS')" runat="server">
											<ASP:LISTITEM Value="0"></ASP:LISTITEM>
										</asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbDRIVER_OCC_CLASS')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
									</TD>
									<TD class="midcolora" width="18%"><%--//Commented By Sibin on 26 Nov 08 for Itrack Issue 5060--<asp:label id="capDRIVER_DRIVERLOYER_NAME" runat="server">Employer Name</asp:label>--%></TD>
									<TD class="midcolora" width="32%"><%--//Commented By Sibin on 26 Nov 08 for Itrack Issue 5060--<asp:textbox id="txtDRIVER_DRIVERLOYER_NAME" runat="server" maxlength="70" size="30"></asp:textbox>--%></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><%--//Commented By Sibin on 26 Nov 08 for Itrack Issue 5060--<asp:label id="capDRIVER_DRIVERLOYER_ADD" runat="server">Employer Address</asp:label>--%></TD>
									<TD class="midcolora" width="32%"><%--//Commented By Sibin on 26 Nov 08 for Itrack Issue 5060--<asp:textbox id="txtDRIVER_DRIVERLOYER_ADD" runat="server" maxlength="70" size="30"></asp:textbox>--%></TD>
									<%--<TD class="midcolora" width="18%"></TD>
									<TD class="midcolora" width="32%"></TD>--%>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_INCOME" runat="server">Income</asp:label><span class="mandatory" id="spnDRIVER_INCOME">*</span></TD>
									<TD class="midcolora" width="32%">
										<asp:DropDownList id="cmbDRIVER_INCOME" runat="server"></asp:DropDownList><BR>
										<asp:RequiredFieldValidator ID="rfvDRIVER_INCOME" Runat="server" ControlToValidate="cmbDRIVER_INCOME" Display="Dynamic"></asp:RequiredFieldValidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capNO_DEPENDENTS" runat="server"></asp:label><span class="mandatory" id="spnNO_DEPENDENTS">*</span></TD>
									<TD class="midcolora" width="32%">
										<asp:DropDownList id="cmbNO_DEPENDENTS" runat="server"></asp:DropDownList><BR>
										<asp:RequiredFieldValidator ID="rfvNO_DEPENDENTS" Runat="server" ControlToValidate="cmbNO_DEPENDENTS" Display="Dynamic"></asp:RequiredFieldValidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capRELATIONSHIP" runat="server">Relation</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbRELATIONSHIP" onfocus="SelectComboIndex('cmbRELATIONSHIP')" runat="server">
											<ASP:LISTITEM Value="0"></ASP:LISTITEM>
										</asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbRELATIONSHIP')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
										<BR>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_VOLUNTEER_POLICE_FIRE" runat="server">Volunteer fireman or policeman</asp:label><span id="spnDRIVER_VOLUNTEER_POLICE_FIRE" class="mandatory">*</span><%--Added by Sibin for Itrack Issue 5424 on 9 Feb 09--%></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_VOLUNTEER_POLICE_FIRE" runat="server"></asp:dropdownlist>
										<br>
										<asp:RequiredFieldValidator ID="rfvDRIVER_VOLUNTEER_POLICE_FIRE" ControlToValidate="cmbDRIVER_VOLUNTEER_POLICE_FIRE"
											Runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_US_CITIZEN" runat="server">U.S. Citizen</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_US_CITIZEN" runat="server"></asp:dropdownlist>
										<br>
										<asp:RequiredFieldValidator ID="rfvDRIVER_US_CITIZEN" ControlToValidate="cmbDRIVER_US_CITIZEN" Runat="server"
											Display="Dynamic"></asp:RequiredFieldValidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_STUD_DIST_OVER_HUNDRED" runat="server">Distant Student</asp:label><span class="mandatory" id="spnDRIVER_STUD_DIST_OVER_HUNDRED">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_STUD_DIST_OVER_HUNDRED" runat="server" onchange="fxnShwHAVE_CAR();EnableDisableControls();"></asp:dropdownlist>
										<br>
										<asp:RequiredFieldValidator ID="rfvDRIVER_STUD_DIST_OVER_HUNDRED" ControlToValidate="cmbDRIVER_STUD_DIST_OVER_HUNDRED"
											Runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
									</TD>
								</tr>
								<tr id="rowIN_MILITARY"> <!-- IN MILITARY -->
									<TD class="midcolora" width="18%"><asp:label id="capIN_MILITARY" runat="server">Are you in the Military?</asp:label><span class="mandatory" id="spnIN_MILITARY">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbIN_MILITARY" runat="server" onchange="fxnDisplayFields(true);CheckForDriverType();"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvIN_MILITARY" Display="Dynamic" ControlToValidate="cmbIN_MILITARY" Runat="server"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capSTATIONED_IN_US_TERR" runat="server">Are you stationed in US, Canada or Puerto Rico or  other US Territories?</asp:label><span class="mandatory" id="spnSTATIONED_IN_US_TERR">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSTATIONED_IN_US_TERR" runat="server" onchange="fxnDisplayFields(true)"></asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvSTATIONED_IN_US_TERR" Display="Dynamic" ControlToValidate="cmbSTATIONED_IN_US_TERR" Runat="server"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr id="rowDependentFields"> <!-- STATIONED IN US-TERR, HAVE CAR -->
									<TD class="midcolora" width="18%"><asp:label id="capHAVE_CAR" runat="server">Do you have the car with you?</asp:label><span class="mandatory" id="spnHAVE_CAR">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbHAVE_CAR" runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvHAVE_CAR" Display="Dynamic" ControlToValidate="cmbHAVE_CAR" Runat="server"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" colspan="2"></TD>
								</tr>
								<tr id="trParentsInsurance">
									<TD class="midcolora" width="18%"><asp:label id="capPARENTS_INSURANCE" runat="server">Parents Insurance</asp:label><span class="mandatory" id="spnPARENTS_INSURANCE">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPARENTS_INSURANCE" runat="server"></asp:dropdownlist>
									<asp:requiredfieldvalidator id="rfvPARENTS_INSURANCE" runat="server" Display="Dynamic" ErrorMessage="Parents Insurance can't be blank."
											ControlToValidate="cmbPARENTS_INSURANCE"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" colspan="2"></td>
								</tr>
								<tr id="trLicMed">
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_LIC_SUSPENDED" runat="server">License suspended, restricted, or revoked in the last 5 years</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_LIC_SUSPENDED" runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_PHYS_MED_IMPAIRE" runat="server">Physical / Medical  Impairment</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_PHYS_MED_IMPAIRE" runat="server" DESIGNTIMEDRAGDROP="1660"></asp:dropdownlist></TD>
								<tr id="vilation">
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_DRINK_VIOLATION" runat="server">Drinking or Drug Related violation in last 5 years?</asp:label><span class="mandatory" id="spnDRIVER_DRINK_VIOLATION">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_DRINK_VIOLATION" runat="server"></asp:dropdownlist>
										<br>
										<asp:RequiredFieldValidator ID="rfvDRIVER_DRINK_VIOLATION" ControlToValidate="cmbDRIVER_DRINK_VIOLATION" Runat="server"
											Display="Dynamic"></asp:RequiredFieldValidator>
									</TD>
									<TD class="midcolora" width="18%"></TD>
									<TD class="midcolora" width="32%"></TD>
								</tr>
								<tr id="trDriverDis1"><%--Added id by Sibin--%>
									<TD class="headerEffectSystemParams" style="HEIGHT: 21px" colSpan="4">Driver 
										Discounts</TD>
								</tr>
								<tr id="trDriverDis2"><%--Added id by Sibin--%>
									<TD class="midcolora" width="18%"><asp:label id="capSAFE_DRIVER" runat="server"></asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbSAFE_DRIVER_RENEWAL_DISCOUNT" runat="server" OnFocus="SelectComboIndex('cmbSAFE_DRIVER_RENEWAL_DISCOUNT')"></asp:dropdownlist><asp:label id="capSAFE_DRIVER_RENEWAL_DISCOUNT" Runat="server"></asp:label></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_GOOD_STUDENT" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_GOOD_STUDENT" runat="server" OnFocus="SelectComboIndex('cmbDRIVER_GOOD_STUDENT')"></asp:dropdownlist><asp:label id="capGoodStudent" Runat="server"></asp:label></TD>
								</tr>
								<tr id="trFULL_TIME_STUDENT">
									<TD class="midcolora" width="18%"><asp:label id="capFULL_TIME_STUDENT" runat="server"></asp:label><span class="mandatory" id="spnFULL_TIME_STUDENT">*</span></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbFULL_TIME_STUDENT" runat="server" OnFocus="SelectComboIndex('cmbFULL_TIME_STUDENT')"></asp:dropdownlist><asp:label id="Label3" Runat="server"></asp:label></TD>
									<br><asp:RequiredFieldValidator ID="rfvFULL_TIME_STUDENT" Runat="server" ControlToValidate="cmbFULL_TIME_STUDENT"
										Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
									<TD class="midcolora" width="18%"><asp:label id="capSUPPORT_DOCUMENT" runat="server"></asp:label><span class="mandatory" id="spnSUPPORT_DOCUMENT">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSUPPORT_DOCUMENT" runat="server" OnFocus="SelectComboIndex('cmbSUPPORT_DOCUMENT')"></asp:dropdownlist><asp:label id="Label5" Runat="server"></asp:label><br>
										<asp:RequiredFieldValidator ID="rfvSUPPORT_DOCUMENT" Runat="server" ControlToValidate="cmbSUPPORT_DOCUMENT"
											Display="Dynamic"></asp:RequiredFieldValidator>
									</TD>
								</tr>
								<tr id="trDriverDis3">
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_PREF_RISK" runat="server"></asp:label></TD>
									<td class="midcolora" colSpan="3"><asp:dropdownlist id="cmbDRIVER_PREF_RISK" runat="server" OnFocus="SelectComboIndex('cmbDRIVER_PREF_RISK')"></asp:dropdownlist><asp:label id="capPremierDriver" Runat="server"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capWAIVER_WORK_LOSS_BENEFITS" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbWAIVER_WORK_LOSS_BENEFITS" runat="server" OnFocus="SelectComboIndex('cmbWAIVER_WORK_LOSS_BENEFITS')"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capSIGNED_WAIVER_BENEFITS_FORM" runat="server"></asp:label><span id="spnSIGNED_WAIVER_BENEFITS_FORM" class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSIGNED_WAIVER_BENEFITS_FORM" runat="server" OnFocus="SelectComboIndex('cmbWAIVER_WORK_LOSS_BENEFITS')"></asp:dropdownlist></TD>
								</tr>
								<!-- added By Swarup on 11-Dec-2006-->
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4">Violations &amp; MVR Info</TD>
									<TD class="headerEffectSystemParams"></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capVIOLATIONS" runat="server">Violations</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%" colSpan="3"><asp:dropdownlist id="cmbVIOLATIONS" onfocus="SelectComboIndex('cmbVIOLATIONS')" runat="server"></asp:dropdownlist>
										<br>
										<asp:RequiredFieldValidator ID="rfvVIOLATIONS" Display="Dynamic" Runat="server" ErrorMessage="Please select Violations."
											ControlToValidate="cmbVIOLATIONS"></asp:RequiredFieldValidator>
									</TD>
									<TD class="midcolora" width="18%"></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMVR_ORDERED" runat="server">MVR Ordered</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbMVR_ORDERED" onfocus="SelectComboIndex('cmbMVR_ORDERED')" onchange="SetDateValidator()"
											runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDATE_ORDERED" runat="server">Date MVR Order</asp:label><span id="spnDATE_ORDERED" class="mandatory">*</span></TD>
									<TD class="midcolora" width="18%" colSpan="3"><asp:textbox id="txtDATE_ORDERED" runat="server" size="11" maxlength="10"></asp:textbox><asp:hyperlink id="hlkCalandarDate2" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgCalenderExp2" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
										</asp:hyperlink><br>
										<asp:requiredfieldvalidator id="rfvDATE_ORDERED" runat="server" ControlToValidate="txtDATE_ORDERED" ErrorMessage="Please select MVR Date ordered."
											Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revDATE_ORDERED" runat="server" ControlToValidate="txtDATE_ORDERED" ErrorMessage="Please check format of date."
											Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvDATE_ORDERED" ControlToValidate="txtDATE_ORDERED" Display="Dynamic" Runat="server"
											ClientValidationFunction="ChkDateOfOrder" ErrorMessage="Please check date of order can't be greater than today."></asp:customvalidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMVR_CLASS" runat="server">MVR Class</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:textbox id="txtMVR_CLASS" runat="server" Width="296px" MaxLength="50"></asp:textbox></TD>
									<TD class="midcolora" width="18%"><asp:label id="capMVR_LIC_CLASS" runat="server">MVR License Class</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:textbox id="txtMVR_LIC_CLASS" runat="server" Width="296px" MaxLength="50"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMVR_LIC_RESTR" runat="server">MVR License Restriction</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:textbox id="txtMVR_LIC_RESTR" runat="server" Width="296px" MaxLength="50"></asp:textbox></TD>
									<TD class="midcolora" width="18%"><asp:label id="capMVR_DRIV_LIC_APPL" runat="server">MVR Driver License Application</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:textbox id="txtMVR_DRIV_LIC_APPL" runat="server" Width="296px" MaxLength="50"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capMVR_REMARKS" runat="server">MVR Remarks</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:textbox id="txtMVR_REMARKS" runat="server" Height="64px" MaxLength="250" Width="296px" TextMode="MultiLine"></asp:textbox><br>
									<asp:customvalidator id="csvMVR_REMARKS" Runat="server" Display="Dynamic" ControlToValidate="txtMVR_REMARKS"
										ErrorMessage="Maximum length of MVR Remarks is 250." ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capMVR_STATUS" runat="server">MVR Status</asp:label></TD>
									<TD class="midcolora" width="18%"><asp:dropdownlist id="cmbMVR_STATUS" onfocus="SelectComboIndex('cmbMVR_STATUS')" runat="server" Height="26px"></asp:dropdownlist></TD>
								</tr>
								<tr>
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
								<tr id="trViolationSec" runat="server">
									<TD class="headerEffectSystemParams" colSpan="4">Violation Points</TD>
								</tr>
								<tr id="trViolationMsg" style="DISPLAY: none" runat="server">
									<td class="midcolora" colSpan="4"><asp:label id="lblViolationMsg" Runat="server"></asp:label></td>
								</tr>
								<tr id="trViolationField" runat="server">
									<TD class="midcolora" width="18%">
										<asp:Label ID="capViolationPoints" Runat="server">Total Violation Points (Minor 2 years/ Major 5 years)</asp:Label>
									</TD>
									<TD class="midcolora" width="32%">
										<asp:Label ID="lblViolationPoints" Runat="server"></asp:Label>
									</TD>
									<TD class="midcolora" width="18%">
										<asp:Label ID="capAccidentPoints" Runat="server">Total Accident Points (during the last 3 years)</asp:Label>
									</TD>
									<TD class="midcolora" width="32%">
										<asp:Label ID="lblAccidentPoints" Runat="server"></asp:Label>
									</TD>
								</tr>
								<tbody id="tbAssignVehSec">
									<tr id="assignVehSec">
										<TD class="headerEffectSystemParams" colSpan="4">Assigned Vehicle</TD>
									</tr>
									<tr id="trVehMsg" style="DISPLAY: none" runat="server">
										<td class="midcolora" colSpan="4"><asp:label id="lblVehicleMsg" Runat="server"></asp:label></td>
									</tr>
									<tr id="trVehField" style="DISPLAY: none" runat="server">
										<td colSpan="4" class="midcolora">
											<table cellSpacing="0" cellPadding="0" width="65%" border="0">
												<tr align="left">
													<td class="midcolora" align="left">
														<asp:label id="capVEHICLE_ID" runat="server">Drive</asp:label><span class="mandatory" id="spnVEHICLE_ID" runat="server">*</span>
													</td>
													<td class="midcolora" align="left" width="50%">
														&nbsp;<asp:dropdownlist id="cmbVEHICLE_ID" onfocus="SelectComboIndex('cmbVEHICLE_ID')" runat="server"></asp:dropdownlist>&nbsp;as&nbsp;<br>
														<asp:requiredfieldvalidator id="rfvVEHICLE_ID" runat="server" ControlToValidate="cmbVEHICLE_ID" Display="Dynamic"></asp:requiredfieldvalidator>
													</td>
													<td class="midcolora" align="left">
														<asp:dropdownlist id="cmbAPP_VEHICLE_PRIN_OCC_ID" onfocus="SelectComboIndex('cmbAPP_VEHICLE_PRIN_OCC_ID')"
															runat="server"></asp:dropdownlist>&nbsp; Driver<br>
														<asp:requiredfieldvalidator id="rfvVEHICLE_DRIVER" runat="server" ControlToValidate="cmbAPP_VEHICLE_PRIN_OCC_ID"
															Display="Dynamic"></asp:requiredfieldvalidator>
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td class="midcolora" colSpan="4">
											<asp:Table id="tblAssignedVeh" runat="server" width="100%" Border="0"></asp:Table>
										</td>
									</tr>
								</tbody>
								<%-- Added For Operator Screen --%>
								<tr id="trOperator" runat="server">
									<td colspan="4">
										<table cellpadding="0" cellspacing="0" border="0">
											<tr>
												<TD class="headerEffectSystemParams" colSpan="4">Operator Discount &amp; Surcharge</TD>
											</tr>
											<tr>
												<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capDRIVER_COST_GAURAD_AUX" runat="server"></asp:label></TD>
												<TD class="midcolora" width="18%" colSpan="2"><asp:dropdownlist id="cmbOP_DRIVER_COST_GAURAD_AUX" onfocus="SelectComboIndex('cmbOP_DRIVER_COST_GAURAD_AUX')"
														runat="server"></asp:dropdownlist><asp:label id="capCoastGuard" Runat="server"></asp:label></TD>
											</tr>
											<tr id="trDRIVER_DIESEL_DISCOUNT" style="DISPLAY: none" runat="server">
												<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capDRIVER_DIESEL_DISCOUNT" runat="server">Diesel Engine Credit</asp:label></TD>
												<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capDriverDiscount" Runat="server">10%</asp:label></TD>
											</tr>
											<tr id="trHALON_FIRE_DISCOUNT" style="DISPLAY: none" runat="server">
												<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capHALON_FIRE_DISCOUNT" runat="server">Halon Fire Extinguisher Discount</asp:label></TD>
												<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capHalonFireDiscount" Runat="server">5%</asp:label></TD>
											</tr>
											<tr id="trNAVIGATION_DISCOUNT" style="DISPLAY: none" runat="server">
												<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capNAVIGATION_DISCOUNT" runat="server">Navigation System Discount</asp:label></TD>
												<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capNavigationDiscount" Runat="server">3%</asp:label></TD>
											</tr>
											<tr id="trSHORE_STATION_CREDIT" style="DISPLAY: none" runat="server">
												<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capSHORE_STATION_CREDIT" runat="server">Shore Station Credit</asp:label></TD>
												<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capShoreStationCredit" Runat="server">4%</asp:label></TD>
											</tr>
											<tr id="trMULTIPLE_DISCOUNT" style="DISPLAY: none" runat="server">
												<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capMULTIPLE_DISCOUNT" runat="server">Multi Boat Discount</asp:label></TD>
												<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capMultipleDiscount" Runat="server">5%</asp:label></TD>
											</tr>
											<tr>
												<TD class="headerEffectSystemParams" colSpan="4">Assigned Boats</TD>
											</tr>
											<tr id="trOpVehMsg" style="DISPLAY: none" runat="server">
												<td class="midcolora" colSpan="4"><asp:label id="lblOpVehMsg" Runat="server"></asp:label></td>
											</tr>
											<tr id="trOpVehField" style="DISPLAY: inline" runat="server">
												<td class="midcolora" colSpan="4">
													<table cellSpacing="0" cellPadding="0" width="45%" border="0">
														<tr align="left">
															<td class="midcolora" align="left"><asp:label id="capOP_VEHICLE_ID" runat="server"></asp:label>
															</td>
															<td class="midcolora" align="left" width="30%"><asp:dropdownlist id="cmbOP_VEHICLE_ID" onfocus="SelectComboIndex('cmbOP_VEHICLE_ID')" runat="server"></asp:dropdownlist>&nbsp;as&nbsp;<br>
															</td>
															<td class="midcolora" align="left"><asp:dropdownlist id="cmbOP_APP_VEHICLE_PRIN_OCC_ID" onfocus="SelectComboIndex('cmbOP_APP_VEHICLE_PRIN_OCC_ID')"
																	runat="server"></asp:dropdownlist>&nbsp;<asp:label id="capOP_APP_VEHICLE_PRIN_OCC_ID" runat="server"></asp:label><br>
															</td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<%-- ************* --%>
								<TR>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"
											CausesValidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" causesvalidation="false"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="2"><%--<cmsb:cmsbutton class="clsButton" id="btnCopyAppDrivers" runat="server" Text="Copy Application Drivers" Visible="false"></cmsb:cmsbutton>--%><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidDRIVER_ID" type="hidden" name="hidDRIVER_ID" runat="server"> <INPUT id="hidCUSTOMER_ID" type="hidden" name="hidAPP_ID" runat="server">
			<INPUT id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server"> <INPUT id="hidAppId" type="hidden" name="hidAppId" runat="server">
			<INPUT id="hidAppVersionId" type="hidden" name="hidAppVersionId" runat="server">
			<INPUT id="hidCUSTOMER_INFO" type="hidden" name="hidCUSTOMER_INFO" runat="server">
			<INPUT id="hidDiscTypeLen" type="hidden" name="hidDiscTypeLen" runat="server"> <INPUT id="hidPolicyId" type="hidden" name="hidPolicyId" runat="server">
			<INPUT id="hidPolicyVersionId" type="hidden" name="hidPolicyVersionId" runat="server">
			<input id="hidState_id" type="hidden" name="hidState_id" runat="server" value="0">
			<input id="hidNO_DEPENDENTS" type="hidden" name="hidNO_DEPENDENTS" runat="server">
			<input id="hidSeletedData" type="hidden" name="hidSeletedData" runat="server">
			<input id="hidDataValue1" type="hidden" name="hidDataValue1" runat="server">
			<input id="hidDataValue2" type="hidden" name="hidDataValue2" runat="server"> 
			<input id="hidCustomInfo" type="hidden" name="hidCustomInfo" runat="server">	
			<input id="hidDateDiff" type="hidden" name="hidDateDiff" runat="server" value="0">		
			<input id="hidDRIVER_DRIV_LIC" type="hidden" value="0" name="hidDRIVER_DRIV_LIC" runat="server">
			<input id="hidDRIVER_DRIV_LIC_SELINDEX" type="hidden" value="0" name="hidDRIVER_DRIV_LIC_SELINDEX" runat="server">
			<INPUT id="hidSSN_NO" type="hidden" name="hidSSN_NO" runat="server">	
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none; Z-INDEX: 101; POSITION: absolute"><iframe id="lookupLayerIFrame" style="DISPLAY: none; Z-INDEX: 1001; FILTER: alpha(opacity=0); BACKGROUND-COLOR: #000000"
				width="0" height="0" left="0px" top="0px;"></iframe>
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
					<td colSpan="2">
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
				RefreshWebGrid(document.getElementById('hidFormSaved').value,TrimTheString(document.getElementById('hidDRIVER_ID').value));
				
			}
			
			SetDateValidator();
		</script>
	</BODY>
</HTML>

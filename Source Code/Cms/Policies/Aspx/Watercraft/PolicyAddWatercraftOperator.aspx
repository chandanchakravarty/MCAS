<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Page language="c#" Codebehind="PolicyAddWatercraftOperator.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Watercrafts.PolicyAddWatercraftOperator" ValidateRequest="false"%>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
  <HEAD>
		<title>BRICS-Policy Watercraft Operator Information</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript" src="/cms/cmsweb/Scripts/Calendar.js"></script>
		<script language="javascript">
	
				var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
				var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
				var jsaAppDtFormat = "<%=aAppDtFormat  %>";
				
				//13 sep 2007
			function SaveClientSide()
			{
				var intTotalRows = parseInt(document.getElementById("tblAssignedVeh").getAttribute("TotalRows"));
				var SelectedData="",driverType="";				
			
					if(document.getElementById("trVehField").style.display=="none")
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
					
					EnableValidator('rfvVEHICLE_ID',false);
					EnableValidator('rfvAPP_VEHICLE_PRIN_OCC_ID',false);		 
				
				 Page_ClientValidate();
				 return Page_IsValid;
			}
			//Added for Itrack Issue 6737 on 18 Nov 09
			function RecVehicle_SaveClientSide()
			{
				var intTotalRecRows = parseInt(document.getElementById("tblAssignedRecVeh").getAttribute("TotalRecRows"));
				var RecSelectedData="",RecdriverType="";				
				
				if(document.getElementById("trVehRecField_Home").style.display=="none")
				{
					document.getElementById("hidRecSeletedData").value="";
					//return;
				}
				for(var i=1 ; i<=intTotalRecRows ; i++)
				{
					if (RecSelectedData == "")
						RecSelectedData = document.getElementById('IDRec_' + i).getAttribute("RowRecVehID") + "~" + document.getElementById('IDRec_' + i).getElementsByTagName("td")[3].childNodes[0].value;
					else
			
						RecSelectedData = RecSelectedData + "|" + document.getElementById('IDRec_' + i).getAttribute("RowRecVehID") + "~" + document.getElementById('IDRec_' + i).getElementsByTagName("td")[3].childNodes[0].value;
				}
				
				document.getElementById("hidRecSeletedData").value=RecSelectedData;		
				
				//EnableValidator('rfvVEHICLE_ID',false);
				//EnableValidator('rfvAPP_VEHICLE_PRIN_OCC_ID',false);		 
				
				 Page_ClientValidate();
				 return Page_IsValid;
			}
			
	   function Assigned_Veh()
		{
			var intTotalRows = parseInt(document.getElementById("tblAssignedVeh").getAttribute("TotalRows"));
			var SelectedData="",driverType="";				
			
			if(document.getElementById("trVehField").style.display=="none")
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
		
	    //Added for Itrack Issue 6737 on 18 Nov 09
		function AssignedRec_Veh()
		{
			var intTotalRecRows = parseInt(document.getElementById("tblAssignedRecVeh").getAttribute("TotalRecRows"));
			var RecSelectedData="",RecdriverType="";				
			
			if(document.getElementById("trVehRecField_Home").style.display=="none")
			{
				document.getElementById("hidRecSeletedData").value="";
				//return;
			}
			for(var i=1 ; i<=intTotalRecRows ; i++)
			{
				if (RecSelectedData == "")
					RecSelectedData = document.getElementById('IDRec_' + i).getAttribute("RowRecVehID") + "~" + document.getElementById('IDRec_' + i).getElementsByTagName("td")[3].childNodes[0].value;
				else
				
					RecSelectedData = RecSelectedData + "|" + document.getElementById('IDRec_' + i).getAttribute("RowRecVehID") + "~" + document.getElementById('IDRec_' + i).getElementsByTagName("td")[3].childNodes[0].value;
			}
			
			document.getElementById("hidRecSeletedData").value=RecSelectedData;		
		}		
		
function ChkDateOfBirth(objSource , objArgs)
{
	
	var expdate=document.APP_WATERCRAFT_DRIVER_DETAILS.txtDRIVER_DOB.value;
	objArgs.IsValid = DateComparer("<%=curDate%>",expdate,jsaAppDtFormat);
	
}	


function ChkDateOfExp(objSource , objArgs)
{
	
	//var expdate=document.APP_WATERCRAFT_DRIVER_DETAILS.txtDATE_EXP_START.value;
	//objArgs.IsValid = DateComparer("<%=curDate%>",expdate,jsaAppDtFormat);
	var expdate=document.APP_WATERCRAFT_DRIVER_DETAILS.txtDRIVER_COST_GAURAD_AUX.value;
	if(isNaN(expdate))//Added by Charles on 6-Nov-09 for Itrack 6721
	{
		objArgs.IsValid=false;
		return;
	}
	var dob=document.APP_WATERCRAFT_DRIVER_DETAILS.txtDRIVER_DOB.value;
	var currentdate = new Date();
	if (dob!='')
	{
		DOBDate = new Date(dob);
		DOBYear=DOBDate.getFullYear();
		if (expdate<DOBYear || expdate>currentdate.getFullYear())
		{
			objArgs.IsValid=false;
		}
		else
			objArgs.IsValid=true;
	}		
}


function populateXML()
{ 
	
//	if(document.getElementById('hidDRIVER_DRIV_LIC_SELINDEX').value != '0')	
//	{		
//			   document.getElementById('cmbDRIVER_LIC_STATE').selectedIndex = document.getElementById('hidDRIVER_DRIV_LIC_SELINDEX').value;
//		document.getElementById('cmbDRIVER_LIC_STATE').focus();
//		document.getElementById('hidDRIVER_DRIV_LIC_SELINDEX').value = '0';
//		return;
//	}
	setDriv_Lic_regexp(1);
	if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
	{
		var tempXML;
		if(document.getElementById('hidOldData')!=null)
		{
			tempXML=document.getElementById('hidOldData').value;				
				
			if(tempXML!="" && tempXML!=0)
			{	
				populateFormData(tempXML,APP_WATERCRAFT_DRIVER_DETAILS);
			}		
		}
		CheckForDriverType();
		setDriv_Lic_regexp(1);
		SSN_hide();
		return false;
	}
	
}


function SetRegisteredState()
{		
	if(document.getElementById("cmbDRIVER_LIC_STATE").value==null || document.getElementById("cmbDRIVER_LIC_STATE").value=='')
	{
		//SelectComboOption("cmbDRIVER_LIC_STATE", document.getElementById("cmbDRIVER_STATE").value);
		ChangeColor();
	}	
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
			case "MARITAL_STATUS":
					SelectComboOption("cmbMARITAL_STATUS",nodeValue)
					break;
			case "CUSTOMER_ZIP":
					document.getElementById('txtDRIVER_ZIP').value = nodeValue;
					break;
			case "DATE_OF_BIRTH":
					document.getElementById('txtDRIVER_DOB').value = nodeValue;
					break;	
			case "SSN_NO":
				//	document.getElementById('txtDRIVER_SSN').value = nodeValue;
			        document.getElementById('hidSSN_NO').value = nodeValue;
					  break;				
			case "DECRYPT_SSN_NO":
					  document.getElementById('capSSN_NO_HID').innerText = nodeValue;
					  break;		  			
			case "GENDER":
					if (window.parent.document.all.cltClientTop_lblSCustomerType && window.parent.document.all.cltClientTop_lblSCustomerType.innerHTML != "Commercial")
					document.getElementById('cmbDRIVER_Sex').value = nodeValue;
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
		}	
	}		
	
	ChangeColor();
	DisableValidators();	
	return false;
	
}

function GenerateDriverCode(Ctrl)
{
	
	if (document.getElementById('hidDRIVER_ID').value == "NEW")
	{
		document.getElementById('txtDRIVER_CODE').value=(GenerateRandomCode(document.getElementById('txtDRIVER_FNAME').value,document.getElementById('txtDRIVER_LNAME').value));
	}
	
}	

		
function CompareExpDateWithDOB(objSource , objArgs)
{
	
	var dob=document.APP_WATERCRAFT_DRIVER_DETAILS.txtDRIVER_DOB.value;
	var expdate=document.APP_WATERCRAFT_DRIVER_DETAILS.txtDATE_EXP_START.value;
	objArgs.IsValid = CompareTwoDate(expdate,dob,jsaAppDtFormat);
	
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
		
function RemoveOtherTab()
{
	
	var deleteStr='<%=delStr%>'
	if(deleteStr=="1")
	{
		RemoveTab(3,top.frames[1]);						
		RemoveTab(2,top.frames[1]);
	}
	
}

function showPageLookupLayer(controlId)
{
	
	var lookupMessage;						
	switch(controlId)
	{
		case "cmbDRIVER_SEX":
			lookupMessage	=	"SEXCD.";
			break;
		default:
			lookupMessage	=	"Look up code not found";
			break;
			
	}
	showLookupLayer(controlId,lookupMessage);							
	
	
}
function RedirectToRecVeh()
{
	top.frames[1].location='../Homeowner/RecrVehiclesIndex.aspx?CalledFrom=HOME&';		
}
function Redirect()
{
	
	if(document.getElementById('hidCalledFrom').value=="UMB")
		top.frames[1].location='PolicyWatercraftIndex.aspx?CalledFrom=UMB&';		
	else
		top.frames[1].location='PolicyWatercraftIndex.aspx?CalledFrom=WAT&';		
		
	
	
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
	
	ResetForm(obj)
	return false;	
	
}
	
function ShowDiscountPercentage(objDropDownListID, objLabelID)
{
	
	if (document.getElementById(objDropDownListID).options[document.getElementById(objDropDownListID).selectedIndex].value == "1")
		document.getElementById(objLabelID).style.display="inline";
	else
		document.getElementById(objLabelID).style.display="none";	
		
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

		function ChkDateOfOrder(objSource , objArgs)
		{
		//var currentDate="<%=DateTime.Now.Date.ToString().Substring(0,DateTime.Now.Date.ToString().IndexOf(' '))%>";
		var expdate=document.APP_WATERCRAFT_DRIVER_DETAILS.txtDATE_ORDERED.value;
		objArgs.IsValid = DateComparer("<%=curDate%>",expdate,jsaAppDtFormat);

		//alert(DateComparer(expdate,currentDate,jsaAppDtFormat));
		}	
		
		function Init()
		{
			if(document.getElementById('trBody').getAttribute("style").display!="none")
			{
				document.getElementById('txtDRIVER_FNAME').focus();
			}
			
			EnableValidator('rfvVEHICLE_ID',false);
			EnableValidator('rfvAPP_VEHICLE_PRIN_OCC_ID',false);
			SSN_change();
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
function CheckForDriverType()
		{ 
			var SelectedValue;		 
			if (document.getElementById('cmbDRIVER_DRIV_TYPE').selectedIndex != '-1')	
			{
				SelectedValue=document.getElementById('cmbDRIVER_DRIV_TYPE').options[document.getElementById('cmbDRIVER_DRIV_TYPE').selectedIndex].value;		        
				if(SelectedValue == '3478' )  // NOT LICENSED
				{
					EnableDisableDesc(spnDRIVER_DRIV_LIC,rfvDRIVER_DRIV_LIC,false);
					EnableDisableDesc(spnDRIVER_LIC_STATE,rfvDRIVER_LIC_STATE,false);
					document.getElementById("txtDRIVER_DRIV_LIC").style.backgroundColor="white";
					document.getElementById("cmbDRIVER_LIC_STATE").style.backgroundColor="white";
					
				}
				else if( SelectedValue == '3477')  //EXCLUDED
				{
					EnableDisableDesc(spnDRIVER_DRIV_LIC,rfvDRIVER_DRIV_LIC,false);
					EnableDisableDesc(spnDRIVER_LIC_STATE,rfvDRIVER_LIC_STATE,false);
					document.getElementById("txtDRIVER_DRIV_LIC").style.backgroundColor="white";
					document.getElementById("cmbDRIVER_LIC_STATE").style.backgroundColor="white";
				}
				else if( SelectedValue == '11603')  //LICENSED
				{
					EnableDisableDesc(spnDRIVER_DRIV_LIC,rfvDRIVER_DRIV_LIC,true);
					EnableDisableDesc(spnDRIVER_LIC_STATE,rfvDRIVER_LIC_STATE,true);
					//document.getElementById("txtDRIVER_DRIV_LIC").style.backgroundColor="#FFFFD1";
					//document.getElementById("cmbDRIVER_LIC_STATE").style.backgroundColor="#FFFFD1";
					DriverColor();
				}
				else
				{
					EnableDisableDesc(spnDRIVER_DRIV_LIC,rfvDRIVER_DRIV_LIC,false);
					EnableDisableDesc(spnDRIVER_LIC_STATE,rfvDRIVER_LIC_STATE,false);
					document.getElementById("txtDRIVER_DRIV_LIC").style.backgroundColor="white";
					document.getElementById("cmbDRIVER_LIC_STATE").style.backgroundColor="white";
				}
			}		 
			else
			{
					EnableDisableDesc(spnDRIVER_DRIV_LIC,rfvDRIVER_DRIV_LIC,false);
					EnableDisableDesc(spnDRIVER_LIC_STATE,rfvDRIVER_LIC_STATE,false);
					document.getElementById("txtDRIVER_DRIV_LIC").style.backgroundColor="white";
					document.getElementById("cmbDRIVER_LIC_STATE").style.backgroundColor="white";
			}
			ApplyColor();
			ChangeColor();
			return false;
		}
		
		
		function EnableDisableDesc(spnDesc,rfvDesc,flag)
		{		
				
				if (flag==false)
				{			
					
					if(rfvDesc!=null)
					{
						rfvDesc.setAttribute('enabled',false);					
						rfvDesc.setAttribute('isValid',false);
						spnDesc.style.display = "none";
						rfvDesc.style.display = "none";
					}					
				}
				else
				{	
					if(rfvDesc!=null)
					{
						rfvDesc.setAttribute('enabled',true);					
						rfvDesc.setAttribute('isValid',true);
						spnDesc.style.display = "inline";
					}
				}			
		}
		
		function DriverColor()
		{
			var SelectedDrvValue;		 
			if (document.getElementById('cmbDRIVER_DRIV_TYPE').selectedIndex != '-1')	
			{
				SelectedDrvValue=document.getElementById('cmbDRIVER_DRIV_TYPE').options[document.getElementById('cmbDRIVER_DRIV_TYPE').selectedIndex].value;		        
			
				if(SelectedDrvValue== '11603')
				{
						if(document.getElementById("txtDRIVER_DRIV_LIC").value !="")
						{
							document.getElementById("txtDRIVER_DRIV_LIC").style.backgroundColor="white";
						}
						else
						{
							document.getElementById("txtDRIVER_DRIV_LIC").style.backgroundColor="#FFFFD1";
						}
						
						if(document.getElementById('cmbDRIVER_LIC_STATE').selectedIndex != '0')
						{
							document.getElementById("cmbDRIVER_LIC_STATE").style.backgroundColor="white";
						}
						else
						{						
							document.getElementById("cmbDRIVER_LIC_STATE").style.backgroundColor="#FFFFD1";
						}
				}
			}	
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
				document.getElementById('txtDRIVER_SSN').value = '';
				document.getElementById('txtDRIVER_SSN').style.display = 'none';
				document.getElementById("revDRIVER_SSN").style.display='none';
				document.getElementById("revDRIVER_SSN").setAttribute('enabled',false);
				document.getElementById("btnSSN_NO").value = 'Edit';
			}	
		</script>
		<!--<BODY leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();SetTab();ChangeColor();RemoveOtherTab();">
		alert(document.getElementById('hidIS_ACTIVE').value);alert(document.getElementById('hidOldData').value);alert(document.getElementById('hidFormSaved').value);
		-->
  </HEAD>
	<BODY leftMargin="0" topMargin="0" onload="Init();populateXML();ApplyColor();ChangeColor();">
		<FORM id="APP_WATERCRAFT_DRIVER_DETAILS" method="post" runat="server">
		<P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
			<TABLE cellSpacing="2" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4">
						<asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label>
					</td>
				</tr>
				<TR>
					<TD class="pageHeader" colSpan="4">
						<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
					</TD>
				</TR>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
										mandatory</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<td colSpan="4">
										<table cellSpacing="0" cellPadding="0" width="100%">
											<TR>
												<TD class="midcolora"><asp:label id="capDRIVER_FNAME" runat="server">First 
                 Name</asp:label><span class="mandatory">*</span></TD>
												<TD class="midcolora"><asp:textbox id="txtDRIVER_FNAME" runat="server" maxlength="75" size="30"></asp:textbox><BR>
													<asp:requiredfieldvalidator id="rfvDRIVER_FNAME" runat="server" Display="Dynamic" ErrorMessage="DRIVER_FNAME can't be blank."
														ControlToValidate="txtDRIVER_FNAME"></asp:requiredfieldvalidator></TD>
												<TD class="midcolora"><asp:label id="capDRIVER_MNAME" runat="server">Middle 
                 Name</asp:label></TD>
												<TD class="midcolora"><asp:textbox id="txtDRIVER_MNAME" runat="server" maxlength="50" size="30"></asp:textbox></TD>
												<TD class="midcolora"><asp:label id="capDRIVER_LNAME" runat="server">Last 
                 Name</asp:label><span class="mandatory">*</span></TD>
												<TD class="midcolora"><asp:textbox id="txtDRIVER_LNAME" onblur="GenerateDriverCode();" runat="server" maxlength="75"
														size="30"></asp:textbox><BR>
													<asp:requiredfieldvalidator id="rfvDRIVER_LNAME" runat="server" Display="Dynamic" ErrorMessage="DRIVER_LNAME can't be blank."
														ControlToValidate="txtDRIVER_LNAME"></asp:requiredfieldvalidator></TD>
											</TR>
										</table>
									</td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_CODE" runat="server">Code</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_CODE" runat="server" maxlength="20" size="23"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_CODE" runat="server" Display="Dynamic" ErrorMessage="DRIVER_CODE can't be blank."
											ControlToValidate="txtDRIVER_CODE"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_SUFFIX" runat="server">Suffix</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_SUFFIX" runat="server" maxlength="10" size="11"></asp:textbox></TD>
								</tr>
								<tr>
									<td class="midcolora"><asp:label id="capPullCustomerAddress" runat="server">Would 
           you like to pull customer address</asp:label></td>
									<td class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnPullCustomerAddress" runat="server" Text="Pull Customer Address"></cmsb:cmsbutton></td>
									<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnCopyDefaultCustomer" runat="server" Text="Copy Default Customer"></cmsb:cmsbutton></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_ADD1" runat="server">Address 
           1</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_ADD1" runat="server" maxlength="70" size="40"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_ADD1" runat="server" Display="Dynamic" ErrorMessage="DRIVER_ADD1 can't be blank."
											ControlToValidate="txtDRIVER_ADD1"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_ADD2" runat="server">Address 
           2</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_ADD2" runat="server" maxlength="70" size="40"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_CITY" runat="server">City</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_CITY" runat="server" maxlength="40" size="20"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_CITY" runat="server" Display="Dynamic" ErrorMessage="DRIVER_CITY can't be blank."
											ControlToValidate="txtDRIVER_CITY"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_COUNTRY" runat="server">Country</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_COUNTRY" onfocus="SelectComboIndex('cmbDRIVER_COUNTRY')" runat="server"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_COUNTRY" runat="server" Display="Dynamic" ErrorMessage="DRIVER_COUNTRY can't be blank."
											ControlToValidate="cmbDRIVER_COUNTRY"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_STATE" runat="server">State</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_STATE" onfocus="SelectComboIndex('cmbDRIVER_STATE')" runat="server"
											onchange="SetRegisteredState();"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_STATE" runat="server" Display="Dynamic" ErrorMessage="DRIVER_STATE can't be blank."
											ControlToValidate="cmbDRIVER_STATE"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_ZIP" runat="server">Zip</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_ZIP" runat="server" maxlength="10" size="12"></asp:textbox>
									<%-- Added by Swarup on 30-mar-2007 --%>
										<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
										<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
										</asp:hyperlink><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_ZIP" runat="server" Display="Dynamic" ErrorMessage="DRIVER_ZIP can't be blank."
											ControlToValidate="txtDRIVER_ZIP"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revDRIVER_ZIP" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtDRIVER_ZIP"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_DOB" runat="server">Date of 
							           Birth</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_DOB" runat="server" maxlength="10" size="11"></asp:textbox><asp:hyperlink id="hlkCalandarDate1" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgCalenderExp1" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
										</asp:hyperlink><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_DOB" runat="server" Display="Dynamic" ErrorMessage="DRIVER_DOB can't be blank."
											ControlToValidate="txtDRIVER_DOB"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revDRIVER_DOB" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtDRIVER_DOB"></asp:regularexpressionvalidator><asp:customvalidator id="csvDRIVER_DOB" Display="Dynamic" ControlToValidate="txtDRIVER_DOB" ClientValidationFunction="ChkDateOfBirth"
											Runat="server"></asp:customvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDRIVER_SSN" runat="server">Social Security #</asp:label></TD>
									<TD class="midcolora" width="32%">
									<asp:label id="capSSN_NO_HID" runat="server" size="14" maxlength="11"></asp:label>
										<input class="clsButton" id="btnSSN_NO" text="Edit" onclick="SSN_change();" type="button"></input>
									<asp:textbox id="txtDRIVER_SSN" runat="server" maxlength="11" size="13"></asp:textbox><BR>
										<asp:regularexpressionvalidator id="revDRIVER_SSN" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtDRIVER_SSN"></asp:regularexpressionvalidator></TD>
					</TD>
				</TR>
				<tr>
					<TD class="midcolora" width="18%">
						<asp:label id="capDRIVER_SEX" runat="server">Gender</asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%">
						<asp:dropdownlist id="cmbDRIVER_SEX" onfocus="SelectComboIndex('cmbDRIVER_SEX')" runat="server">
							<ASP:LISTITEM Value=""></ASP:LISTITEM>
							<ASP:LISTITEM Value="M">Male</ASP:LISTITEM>
							<ASP:LISTITEM Value="F">Female</ASP:LISTITEM>
						</asp:dropdownlist>
						<BR>
						<asp:requiredfieldvalidator id="rfvDRIVER_SEX" runat="server" Display="Dynamic" ControlToValidate="cmbDRIVER_SEX"></asp:requiredfieldvalidator>
					</TD>
					<TD class="midcolora" width="18%"><asp:label id="capDRIVER_DRIV_TYPE" runat="server">Driver Type </asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDRIVER_DRIV_TYPE" onfocus="SelectComboIndex('cmbDRIVER_DRIV_TYPE')" runat="server" onchange="CheckForDriverType();">
											<ASP:LISTITEM Value="0"></ASP:LISTITEM>
										</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvDRIVER_DRIV_TYPE" runat="server" Display="Dynamic" ErrorMessage="DRIVER_DRIV_TYPE can't be blank."
											ControlToValidate="cmbDRIVER_DRIV_TYPE"></asp:requiredfieldvalidator>
					</tr>
					<tr>
					<TD class="midcolora" width="18%"><asp:label id="capDRIVER_DRIV_LIC" runat="server">License #</asp:label><span id ="spnDRIVER_DRIV_LIC" class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%" colspan = "3"><asp:textbox id="txtDRIVER_DRIV_LIC" runat="server" maxlength="30" size="30" onchange="DriverColor();"></asp:textbox><BR>
						<asp:requiredfieldvalidator id="rfvDRIVER_DRIV_LIC" runat="server" Display="Dynamic" ControlToValidate="txtDRIVER_DRIV_LIC"></asp:requiredfieldvalidator>
						<asp:regularexpressionvalidator id="revDRIVER_DRIV_LIC" runat="server" ControlToValidate="txtDRIVER_DRIV_LIC" ErrorMessage="RegularExpressionValidator"
							Display="Dynamic"></asp:regularexpressionvalidator>
					</TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capDRIVER_LIC_STATE" runat="server">License 
           State</asp:label><span id="spnDRIVER_LIC_STATE" class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%" ><asp:dropdownlist id="cmbDRIVER_LIC_STATE" onfocus="SelectComboIndex('cmbDRIVER_LIC_STATE')" onChange="setDriv_Lic_regexp(2);DriverColor();" runat="server"></asp:dropdownlist><BR>
						<asp:requiredfieldvalidator id="rfvDRIVER_LIC_STATE" runat="server" Display="Dynamic" ControlToValidate="cmbDRIVER_LIC_STATE"></asp:requiredfieldvalidator>
					</TD>
					<TD class="midcolora" width="18%"><asp:label id="capMARITAL_STATUS" runat="server">Marital Status</asp:label></TD>
					<TD class="midcolora" width="32%">
						<asp:dropdownlist id="cmbMARITAL_STATUS" onfocus="SelectComboIndex('cmbMARITAL_STATUS')" runat="server"
							Height="26px"></asp:dropdownlist>
					</TD>
				</tr>
				<tr>
					<TD class="headerEffectSystemParams" colSpan="4">Operator Discount &amp; Surcharge</TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capDRIVER_COST_GAURAD_AUX" runat="server">Boating Experience Since</asp:label><span class="mandatory" id="spnDRIVER_COST_GAURAD_AUX" runat="server">*</span></TD> <!-- Added id & runat, Charles 6-Nov-09, Itrack 6721 -->
					<TD class="midcolora" width="18%" colSpan="3"><asp:textbox id="txtDRIVER_COST_GAURAD_AUX" size="4" Runat="server" MaxLength="4"></asp:textbox>
						<br>
						<%-- <asp:RangeValidator ID="rngDRIVER_COST_GAURAD_AUX" Runat="server" ControlToValidate="txtDRIVER_COST_GAURAD_AUX"
							Type="Integer" Display="Dynamic"></asp:RangeValidator> --%> <!-- Commented by Charles on 6-Nov-09 for Itrack 6721 -->
						<asp:RequiredFieldValidator ID="rfvDRIVER_COST_GAURAD_AUX" Runat="server" ControlToValidate="txtDRIVER_COST_GAURAD_AUX"
							Display="Dynamic"></asp:RequiredFieldValidator>
							<asp:customvalidator id="csvDRIVER_COST_GAURAD_AUX" Display="Dynamic" ControlToValidate="txtDRIVER_COST_GAURAD_AUX" 
							ClientValidationFunction="ChkDateOfExp"	Runat="server"></asp:customvalidator>
					</TD>
				</tr>
				<!--START-->
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capWAT_SAFETY_COURSE" runat="server">WAT_SAFETY_COURSE</asp:label></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbWAT_SAFETY_COURSE" onfocus="SelectComboIndex('cmbWAT_SAFETY_COURSE')" runat="server"></asp:dropdownlist></TD>
					<TD class="midcolora" width="18%"><asp:label id="capCERT_COAST_GUARD" runat="server">CERT_COAST_GUARD</asp:label></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCERT_COAST_GUARD" onfocus="SelectComboIndex('cmbCERT_COAST_GUARD')" runat="server"></asp:dropdownlist></TD>
				</tr>
				<tr>
					<TD class="headerEffectSystemParams" colSpan="4">Violations &amp; MVR Info</TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capVIOLATIONS" runat="server">Violations</asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbVIOLATIONS" onfocus="SelectComboIndex('cmbVIOLATIONS')" runat="server"></asp:dropdownlist>
						<br>
						<asp:RequiredFieldValidator ID="rfvVIOLATIONS" Display="Dynamic" Runat="server" ErrorMessage="Please select Violations."
							ControlToValidate="cmbVIOLATIONS"></asp:RequiredFieldValidator>
					</TD>
					<TD class="midcolora" colspan="2"></TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capMVR_ORDERED" runat="server">MVR Ordered</asp:label></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbMVR_ORDERED" onchange="SetDateValidator()" onfocus="SelectComboIndex('cmbMVR_ORDERED')"
							runat="server"></asp:dropdownlist></TD>
					<TD class="midcolora" width="18%"><asp:label id="capDATE_ORDERED" runat="server">Date MVR Order</asp:label><span id="spnDATE_ORDERED" class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtDATE_ORDERED" runat="server" size="11" maxlength="10"></asp:textbox><asp:hyperlink id="hlkDATE_ORDERED" runat="server" CssClass="HotSpot">
							<ASP:IMAGE id="imgDATE_ORDERED" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
						</asp:hyperlink>
						<br>
						<asp:requiredfieldvalidator id="rfvDATE_ORDERED" Display="Dynamic" runat="server" ControlToValidate="txtDATE_ORDERED"
							ErrorMessage="Please select MVR Date."></asp:requiredfieldvalidator>
						<asp:regularexpressionvalidator id="revDATE_ORDERED" Display="Dynamic" runat="server" ControlToValidate="txtDATE_ORDERED"
							ErrorMessage="Please check format of date."></asp:regularexpressionvalidator>
						<asp:customvalidator id="csvDATE_ORDERED" ControlToValidate="txtDATE_ORDERED" Display="Dynamic" Runat="server"
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
				<!--END-->
				<tr id="trDRIVER_DIESEL_DISCOUNT" runat="server">
					<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capDRIVER_DIESEL_DISCOUNT" runat="server">Diesel Engine Credit</asp:label></TD>
					<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capDriverDiscount" Runat="server">10%</asp:label></TD>
				</tr>
				<tr id="trHALON_FIRE_DISCOUNT" runat="server">
					<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capHALON_FIRE_DISCOUNT" runat="server">Halon Fire Extinguisher Discount</asp:label></TD>
					<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capHalonFireDiscount" Runat="server">5%</asp:label></TD>
				</tr>
				<tr id="trNAVIGATION_DISCOUNT" runat="server">
					<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capNAVIGATION_DISCOUNT" runat="server">Navigation System Discount</asp:label></TD>
					<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capNavigationDiscount" Runat="server">3%</asp:label></TD>
				</tr>
				<tr id="trSHORE_STATION_CREDIT" runat="server">
					<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capSHORE_STATION_CREDIT" runat="server">Shore Station Credit</asp:label></TD>
					<TD class="midcolora" width="18%" colSpan="2"><asp:label id="capShoreStationCredit" Runat="server">4%</asp:label></TD>
				</tr>
				<tr id="trMULTIPLE_DISCOUNT" runat="server">
					<TD class="midcolora" width="18%" colSpan="1"><asp:label id="capMULTIPLE_DISCOUNT" runat="server">Multi Boat Discount</asp:label></TD>
					<TD class="midcolora" width="18%" colSpan="3"><asp:label id="capMultipleDiscount" Runat="server">5%</asp:label></TD>
				</tr>
				
				<TR ID="trViolationSec" RUNAT="SERVER">
						<TD class="headerEffectSystemParams" colSpan="4">Violation Points</TD>
					</TR>
					<TR ID="trViolationMsg" style="DISPLAY: none" runat="server">
						<TD class="midcolora" colSpan="4"><ASP:LABEL id="lblViolationMsg" Runat="server"></ASP:LABEL></TD>
					</TR>
					<TR id="trViolationField" runat="server">
						<TD class="midcolora" width="18%"><asp:label id="capViolationPoints" Runat="server">Total Violation Points </asp:label></TD>
						<TD class="midcolora" width="32%"><asp:label id="lblViolationPoints" Runat="server"></asp:label></TD>
						<TD class="midcolora" colSpan="2"></TD>						
				</TR>	
					
				<tr>
					<TD class="headerEffectSystemParams" colSpan="4">Assigned Boats</TD>
				</tr>
				<tr id="trVehMsg" style="DISPLAY:none" runat="server">
					<td colspan="4" class="midcolora">
						<asp:Label ID="lblVehicleMsg" Runat="server"></asp:Label>
					</td>
				</tr>
				<tr id="trVehField_old" runat="server" style="DISPLAY:none">
					<td colSpan="4" class="midcolora">
						<table cellSpacing="0" cellPadding="0" width="45%" border="0">
							<tr align="left">
								<td class="midcolora" align="left">
									<asp:label id="capVEHICLE_ID" runat="server"></asp:label><span class="mandatory">*</span>
								</td>
								<td class="midcolora" align="left" width="30%">
									<asp:dropdownlist id="cmbVEHICLE_ID" onfocus="SelectComboIndex('cmbVEHICLE_ID')" runat="server"></asp:dropdownlist>&nbsp;as&nbsp;<br>
									<asp:requiredfieldvalidator id="rfvVEHICLE_ID" runat="server" Display="Dynamic" ControlToValidate="cmbVEHICLE_ID"></asp:requiredfieldvalidator>
								</td>
								<td class="midcolora" align="left">
									<asp:dropdownlist id="cmbAPP_VEHICLE_PRIN_OCC_ID" onfocus="SelectComboIndex('cmbAPP_VEHICLE_PRIN_OCC_ID')"
										runat="server"></asp:dropdownlist>&nbsp;<asp:label id="capAPP_VEHICLE_PRIN_OCC_ID" runat="server">Operator</asp:label><br>
									<asp:requiredfieldvalidator id="rfvAPP_VEHICLE_PRIN_OCC_ID" runat="server" Display="Dynamic" ControlToValidate="cmbAPP_VEHICLE_PRIN_OCC_ID"></asp:requiredfieldvalidator>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr id="trVehField" runat="server">
						<td colSpan="4"><asp:table id="tblAssignedVeh" runat="server" Border="0" width="100%"></asp:table></td>
					</tr>
				<tr id="trRechVehHeader" runat="server" style="DISPLAY: none">
					<TD class="headerEffectSystemParams" colSpan="4">Assigned Recreational Vehicles</TD>
				</tr>
				<tr id="trRecVehMsg" style="DISPLAY: none" runat="server">
					<td class="midcolora" colSpan="4"><asp:label id="lblRecVehicleMsg" Runat="server"></asp:label></td>
				</tr>
				<tr id="trRecVehField" style="DISPLAY: none" runat="server">
					<td class="midcolora" colSpan="4">
						<table cellSpacing="0" cellPadding="0" width="45%" border="0">
							<tr align="left">
								<td class="midcolora" align="left"><asp:label id="capREC_VEH_ID" runat="server"></asp:label>
								</td>
								<td class="midcolora" align="left" width="30%"><asp:dropdownlist id="cmbREC_VEH_ID" onfocus="SelectComboIndex('cmbREC_VEH_ID')" runat="server"></asp:dropdownlist>&nbsp;as&nbsp;<br>
								</td>
								<td class="midcolora" align="left"><asp:dropdownlist id="cmbAPP_REC_VEHICLE_PRIN_OCC_ID" onfocus="SelectComboIndex('cmbAPP_REC_VEHICLE_PRIN_OCC_ID')"
										runat="server"></asp:dropdownlist>&nbsp;<asp:label id="capAPP_REC_VEHICLE_PRIN_OCC_ID" runat="server"></asp:label><br>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<%--Added for Itrack Issue 6737 on 18 Nov 09--%>
				<tr id="trVehRecField_Home" runat="server">
						<td colSpan="4"><asp:table id="tblAssignedRecVeh" runat="server" Border="0" width="100%"></asp:table></td>
					</tr>
				<tr>
					<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
						&nbsp;<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"
							causesValidation="false"></cmsb:cmsbutton></td>
					<td class="midcolorr" colSpan="2">
						<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" causesValidation="false"></cmsb:cmsbutton>
						&nbsp;<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
				</tr>
			</TABLE></TD></TR></TBODY></TABLE> <INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
			<INPUT id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
			<INPUT id="hidAppID" type="hidden" value="0" name="hidAppID" runat="server"> <INPUT id="hidAppVersionID" type="hidden" value="0" name="hidAppVersionID" runat="server">
			<INPUT id="hidDRIVER_ID" type="hidden" value="0" name="hidDRIVER_ID" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
			<INPUT id="hidCalledFor" type="hidden" value="0" name="hidCalledFor" runat="server">
			<INPUT id="hidPolicyID" type="hidden" value="0" name="hidPolicyID" runat="server">
			<INPUT id="hidLOB_ID" type="hidden" value="0" name="hidLOB_ID" runat="server"> 
			<INPUT id="hidPolicyVersionID" type="hidden" value="0" name="hidPolicyVersionID" runat="server">
			<input id="hidDRIVER_DRIV_LIC" type="hidden" value="0" name="hidDRIVER_DRIV_LIC" runat="server">
			<INPUT id="hidSeletedData" type="hidden" value="0" name="hidSeletedData" runat="server">
			<input id="hidDRIVER_DRIV_LIC_SELINDEX" type="hidden" value="0" name="hidDRIVER_DRIV_LIC_SELINDEX" runat="server">
			<INPUT language="javascript" id="hidCUSTOMER_INFO" onbeforeeditfocus="return hidCUSTOMER_INFO_onbeforeeditfocus()"
				type="hidden" name="hidCUSTOMER_INFO" runat="server">
			<INPUT id="hidCustomInfo" type="hidden" value="0" name="hidCustomInfo" runat="server">
			<INPUT id="hidSSN_NO" type="hidden" name="hidSSN_NO" runat="server">
			<INPUT id="hidRecSeletedData" type="hidden" value="0" name="hidRecSeletedData" runat="server"><%--Added for Itrack Issue 6737 on 18 Nov 09--%>
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
		
		/*if (document.getElementById('hidFormSaved').value == "5")
		{
			document.getElementById('hidFormSaved').value = 1;			
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidDRIVER_ID').value,false);
			document.getElementById('hidFormSaved').value = 0;
		}
		else if (document.getElementById('hidFormSaved').value == "1")
		{
			this.parent.strSelectedRecordXML = "-1";
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidDRIVER_ID').value,true);
		}*/
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

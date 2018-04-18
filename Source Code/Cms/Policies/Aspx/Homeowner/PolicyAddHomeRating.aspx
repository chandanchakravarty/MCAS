<%@ Page language="c#" Codebehind="PolicyAddHomeRating.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.aspx.HomeOwners.PolicyAddHomeRating" validateRequest="false"%>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddHomeRating_New</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
			var jsaAppEffDate = "<%=App_Effective_Date  %>";
			function ResetForm()
			{
				document.POL_HOME_RATING_INFO.reset();
				Initialize();
				
				return false;			
			}
			
/*******************************************************************
This is the javascript function that is invoked when the checkboxlist
is clicked.

Function:    disableListItems.
Inputs:        checkBoxListId - The id of the checkbox list.

            checkBoxIndex - The index of the checkbox to verify.
            i.e If the 4th checkbox is clicked and 
            you want the other checkboxes to be
            disabled the index would be 3.
            
            numOfItems - The number of checkboxes in the list.
Purpose:  Disables all the checkboxes when the checkbox to verify is
            checked.  The checkbox to verify is never disabled.
********************************************************************/
function disableListItems(checkBoxListId, checkBoxIndex, numOfItems)
{
    // Get the checkboxlist object.
    objCtrl = document.getElementById(checkBoxListId);
    
    // Does the checkboxlist not exist?
    if(objCtrl == null)
    {
        return;
    }

    var i = 0;
    var objItem = null;
    // Get the checkbox to verify.
    var objItemChecked = 
       document.getElementById(checkBoxListId + '_' + checkBoxIndex);

    // Does the individual checkbox exist?
    if(objItemChecked == null)
    {
        return;
    }

    // Is the checkbox to verify checked?
    var isChecked = objItemChecked.checked;
    
    // Loop through the checkboxes in the list.
    for(i = 0; i < numOfItems; i++)
    {
        objItem = document.getElementById(checkBoxListId + '_' + i);

        if(objItem == null)
        {
            continue;
        }

        // If i does not equal the checkbox that is never to be disabled.
        if(i != checkBoxIndex)
        {
            // Disable/Enable the checkbox.
            objItem.disabled = isChecked;
            // Should the checkbox be disabled?
            if(isChecked)
            {
                // Uncheck the checkbox.
                objItem.checked = false;
            }
        }
    }
}

/////////////////////////////////////////////////////////////////
function disableChkLocal(checkBoxListId)
{
	// Get the checkboxlist object.
    objCtrl = document.getElementById(checkBoxListId);
    
    // Does the checkboxlist not exist?
    if(objCtrl == null)
    {
        return;
    }

    var i = 0;
    var objItem = null;
    // Get the checkbox to verify.
    
    var objItemChecked0 = document.getElementById(checkBoxListId + '_' + '0');
    var objItemChecked1 = document.getElementById(checkBoxListId + '_' + '1');

    // Does the individual checkbox exist?
    if(objItemChecked0 == null || objItemChecked1 == null)        
        return;
    

    // Is the checkbox to verify checked?
    var isChecked0 = objItemChecked0.checked;
    var isChecked1 = objItemChecked1.checked;	
        
		
    // Disable/Enable the checkbox.
   		if(isChecked0)
		{
			// Uncheck and Disable the checkbox.
			objItemChecked1.checked = false;
			objItemChecked1.disabled = true;
			return;
		}
		else
		{
			objItemChecked1.disabled = false;			
		}
		if(isChecked1)
		{
			// Uncheck and Disable the checkbox.		
			objItemChecked0.checked = false;
			objItemChecked0.disabled = true;
			return;
		}		
		else
		{
			objItemChecked0.disabled = false;			
		}
  

}

function ChkOccurenceDate(objSource , objArgs)
	{
		var effdateObj=eval('document.POL_HOME_RATING_INFO.' + objSource.getAttribute('ControlTovalidate'));
		var effdate=effdateObj.value;
		var date='<%=DateTime.Now.Year%>';
		if(effdate.length<4)
		{
		objArgs.IsValid = false;
		}
		else
		{
		if(effdate > date)
		objArgs.IsValid = false;
		else
		objArgs.IsValid = true;
		}		
	}
			
function AddData()
{
					
	//Home rating
	document.getElementById('hidRowId').value	=	'New';
   
	//document.getElementById('txtHYDRANT_DIST').focus();
	//document.getElementById('txtHYDRANT_DIST').value  = '';
   
  	document.getElementById('txtFIRE_STATION_DIST').value  = '';
      
	//document.getElementById('cmbIS_UNDER_CONSTRUCTION').options.selectedIndex = -1;
   	
if ( document.getElementById('hidPolicyType').value == '11458' || document.getElementById('hidPolicyType').value == '11480' || document.getElementById('hidPolicyType').value == '11482' || document.getElementById('hidPolicyType').value == '11290'|| document.getElementById('hidPolicyType').value == '11292' )
	{
		document.getElementById('cmbIS_UNDER_CONSTRUCTION').options.selectedIndex = 1;
	}
	else
	{
		document.getElementById('cmbIS_UNDER_CONSTRUCTION').options.selectedIndex = -1;
	}
				
	///document.getElementById('cmbIS_AUTO_POL_WITH_CARRIER').options.selectedIndex = -1;
    		
	//document.getElementById('cmbPROT_CLASS').options.selectedIndex = -1;
	document.getElementById('txtDWELLING_CONST_DATE').value  = ''; 
    	
	//Construction
	//document.getElementById('txtNO_OF_FAMILIES').focus();
	document.getElementById('txtNO_OF_FAMILIES').value  = '';
	document.getElementById('cmbCONSTRUCTION_CODE').options.selectedIndex = -1;
	document.getElementById('cmbEXTERIOR_CONSTRUCTION').options.selectedIndex = -1;
	document.getElementById('cmbFOUNDATION').value  = '';
	document.getElementById('txtPRIMARY_HEAT_OTHER_DESC').value  = '';
	document.getElementById('cmbROOF_TYPE').options.selectedIndex = -1;
	document.getElementById('txtROOF_OTHER_DESC').value  = '';
	document.getElementById('cmbPRIMARY_HEAT_TYPE').options.selectedIndex = -1;
	document.getElementById('cmbSECONDARY_HEAT_TYPE').options.selectedIndex = -1;

	cmbEXTERIOR_CONSTRUCTION_OnChange();
	combo_OnChange('cmbROOF_TYPE','txtROOF_OTHER_DESC','lblROOF_OTHER_DESC');
	combo_OnChange('cmbPRIMARY_HEAT_TYPE','txtPRIMARY_HEAT_OTHER_DESC','lblPR_HEAT_OTHER_DESC');
	combo_OnChange('cmbSECONDARY_HEAT_TYPE','txtSECONDARY_HEAT_OTHER_DESC','lblSC_HEAT_OTHER_DESC');
		
	document.getElementById('cmbWIRING_RENOVATION').options.selectedIndex = 2;
	document.getElementById('txtWIRING_UPDATE_YEAR').value  = '';
	document.getElementById('cmbPLUMBING_RENOVATION').options.selectedIndex = 2;
	document.getElementById('txtPLUMBING_UPDATE_YEAR').value  = '';
	document.getElementById('cmbHEATING_RENOVATION').options.selectedIndex = 2;
	document.getElementById('txtHEATING_UPDATE_YEAR').value  = '';
	document.getElementById('cmbROOFING_RENOVATION').options.selectedIndex = 2;
	document.getElementById('txtROOFING_UPDATE_YEAR').value  = '';
	//document.getElementById('txtNO_OF_AMPS').value  = '';
	document.getElementById('cmbCIRCUIT_BREAKERS').options.selectedIndex = -1;
	document.getElementById('cmbALARM_CERT_ATTACHED').options.selectedIndex = -1;
	document.getElementById('cmbSPRINKER').options.selectedIndex = -1;			
	
	document.getElementById('txtNEED_OF_UNITS').value  = '';
	
	ApplyColor();
	ChangeColor();
	DisableValidators();
	
}
		function DisplaySuburbanDiscount()
		{
			if(DateDiffernce('11/01/2008',jsaAppEffDate)>=0 && document.getElementById('hidPolicyType').value != "11195" && document.getElementById('hidPolicyType').value != "11196" && document.getElementById('hidPolicyType').value != "11405" && document.getElementById('hidPolicyType').value != "11406" && document.getElementById('hidPolicyType').value != "11403"  && document.getElementById('hidPolicyType').value != "11404"  && document.getElementById('hidPolicyType').value != "11193"  && document.getElementById('hidPolicyType').value != "11194")
			{
				document.getElementById('trSuburbanClass').style.display="inline";
				document.getElementById('trSuburbanClassDiscount').style.display="inline";
			}
			else
			{
				document.getElementById('trSuburbanClass').style.display="none";
				document.getElementById('trSuburbanClassDiscount').style.display="none";
			}
		}	
		function DisplaySuburbanHomeLocation(flag)
			{
			
			    //alert(document.getElementById('cmbLOCATED_IN_SUBDIVISION').options.selectedIndex);
				if(document.getElementById('cbSUBURBAN_CLASS').checked==true)
				{
					document.getElementById('CapLOCATED_IN_SUBDIVISION').style.display="inline";
					document.getElementById('cmbLOCATED_IN_SUBDIVISION').style.display="inline";
					document.getElementById('spnLOCATED_IN_SUBDIVISION').style.display="inline";
					if(document.getElementById('cmbLOCATED_IN_SUBDIVISION').options.selectedIndex==0 || document.getElementById('cmbLOCATED_IN_SUBDIVISION').options.selectedIndex==-1)
					{
				
						if(flag == true)							
						{
							document.getElementById('rfvLOCATED_IN_SUBDIVISION').style.display="inline";
							document.getElementById("rfvLOCATED_IN_SUBDIVISION").setAttribute('isValid',true);
							document.getElementById("rfvLOCATED_IN_SUBDIVISION").setAttribute('enabled',true);						
						
						}
						else
						{
							document.getElementById('rfvLOCATED_IN_SUBDIVISION').style.display="none";
							document.getElementById("rfvLOCATED_IN_SUBDIVISION").setAttribute('isValid',false);
							document.getElementById("rfvLOCATED_IN_SUBDIVISION").setAttribute('enabled',false);
						}	
					}
					else
					{
						document.getElementById('rfvLOCATED_IN_SUBDIVISION').style.display="none";
						document.getElementById("rfvLOCATED_IN_SUBDIVISION").setAttribute('isValid',false);
						document.getElementById("rfvLOCATED_IN_SUBDIVISION").setAttribute('enabled',false);
					}
				}
				else
				{				
					document.getElementById('CapLOCATED_IN_SUBDIVISION').style.display="none";
					document.getElementById('cmbLOCATED_IN_SUBDIVISION').style.display="none";
					document.getElementById('spnLOCATED_IN_SUBDIVISION').style.display="none";
					
					document.getElementById('rfvLOCATED_IN_SUBDIVISION').style.display="none";
					document.getElementById("rfvLOCATED_IN_SUBDIVISION").setAttribute('isValid',false);
					document.getElementById("rfvLOCATED_IN_SUBDIVISION").setAttribute('enabled',false);					
					
				}
			}
			
function Initialize()
{
				getProtectionClassFromWS();
               //Check For Year to disable validators
                if(document.getElementById('hiddyear').value!="0")
			    {
			        document.getElementById('spnWIRING_RENOVATION').style.display="inline";
			        document.getElementById('spnPLUMBING_RENOVATION').style.display="inline";
			        document.getElementById('spnHEATING_RENOVATION').style.display="inline";
			        document.getElementById('spnROOFING_RENOVATION').style.display="inline";
			    }
			    else
			    {
			        document.getElementById('spnWIRING_RENOVATION').style.display="none";
			        document.getElementById('spnPLUMBING_RENOVATION').style.display="none";
			        document.getElementById('spnHEATING_RENOVATION').style.display="none";
			        document.getElementById('spnROOFING_RENOVATION').style.display="none";
			    }
			   
	if ( document.POL_HOME_RATING_INFO.hidOldData.value == '')
	{
		AddData();
		
	}
	else
	{					
		if (document.getElementById('cmbIS_UNDER_CONSTRUCTION').selectedIndex != '2')
		{
			document.getElementById('txtDWELLING_CONST_DATE').value  = ''; 
		}
	}
	
	//Footage
	setDropdowns();
	DisplayDate();
	
	//Home construction
	cmbEXTERIOR_CONSTRUCTION_OnChange();
	combo_OnChange('cmbROOF_TYPE','txtROOF_OTHER_DESC','lblROOF_OTHER_DESC');
	combo_OnChange('cmbPRIMARY_HEAT_TYPE','txtPRIMARY_HEAT_OTHER_DESC','lblPR_HEAT_OTHER_DESC');
	combo_OnChange('cmbSECONDARY_HEAT_TYPE','txtSECONDARY_HEAT_OTHER_DESC','lblSC_HEAT_OTHER_DESC');
	
	disableChkLocal('cblLOCAL');
	disableListItems('cblBurgFire', '0', '3');
	disableListItems('cblDIRECT', '0', '3');
	MAND_ALARM_CERT_ATTACHED();	//Added by Charles on 20-Oct-09 for Itrack 6586
	ApplyColor();
	ChangeColor();
	document.getElementById('cmbPROT_CLASS').focus();
	DisplaySuburbanDiscount();
	DisplaySuburbanHomeLocation(true);
}
			
		    
function SetControls()
{
	switch (document.getElementById('hidFormSaved').value)
	{
	case "0" : // not save
								
					document.getElementById('lblTEMPERATURE').style.display="none";
					document.getElementById('lblSMOKE').style.display="none";
					document.getElementById('lblBURGLAR').style.display="none";
					document.getElementById('lblSWIMMING_POOL_TYPE').style.display="none";						
					document.getElementById('cmbTEMPERATURE').style.display="inline";
					document.getElementById('cmbSMOKE').style.display="inline";
					document.getElementById('cmbBURGLAR').style.display="inline";		
					document.getElementById('cblSWIMMING_POOL_TYPE').style.display="inline";					
					break;
				
	case "1" : // save successfully
				
			if (document.getElementById('cmbPROTECTIVE_DEVICES').options.selectedIndex == 1)
				{
					document.getElementById('lblTEMPERATURE').style.display="none";
					document.getElementById('lblSMOKE').style.display="none";
					document.getElementById('lblBURGLAR').style.display="none";
					document.getElementById('cmbTEMPERATURE').style.display="inline";
					document.getElementById('cmbSMOKE').style.display="inline";
					document.getElementById('cmbBURGLAR').style.display="inline";								
					//break;
				}
				else
				{
				
					document.getElementById('lblTEMPERATURE').style.display="inline";
					document.getElementById('lblSMOKE').style.display="inline";
					document.getElementById('lblBURGLAR').style.display="inline";
					document.getElementById('cmbTEMPERATURE').style.display="none";
					document.getElementById('cmbSMOKE').style.display="none";
					document.getElementById('cmbBURGLAR').style.display="none";					
					//break;
				}
				if ( document.getElementById('cmbSWIMMING_POOL').options.selectedIndex == 1)
				{
					
					document.getElementById('cblSWIMMING_POOL_TYPE').style.display="inline";
					document.getElementById('lblSWIMMING_POOL_TYPE').style.display="none";
					//break;					
				}
				else
				{
			
					document.getElementById('cblSWIMMING_POOL_TYPE').style.display="none";
					document.getElementById('lblSWIMMING_POOL_TYPE').style.display="inline";
					//break;					
				}
		
			case "2" : // error
					//do nothing
					break;	
		}
	}	
	
function showPageLookupLayer(controlId)
{
	var lookupMessage;						
	switch(controlId)
	{
		case "cmbFOUNDATION":
			lookupMessage	=	"FNDCD.";
			break;
		case "cmbEXTERIOR_CONSTRUCTION":
			lookupMessage	=	"CONTYP.";
			break;
		case "cmbPRIMARY_HEAT_TYPE":
			lookupMessage	=	"PHEAT.";
			break;
		case "cmbSECONDARY_HEAT_TYPE":
			lookupMessage	=	"PHEAT.";
			break;
		case "cmbROOF_TYPE":
			lookupMessage	=	"RFTYP.";
			break;
		case "cmbTEMPERATURE":
			lookupMessage	=	"PVDCD.";
			break;
		case "cmbSMOKE":
			lookupMessage	=	"PDSCD.";
			break;
		case "cmbBURGLAR":
			lookupMessage	=	"%BURG.";
			break;
		case "cmbSWIMMING_POOL_TYPE":
			lookupMessage	=	"SPLCD.";
			break;
		default:
			lookupMessage	=	"Look up code not found";
			break;
	}
	showLookupLayer(controlId,lookupMessage);							
}
			
function cmbEXTERIOR_CONSTRUCTION_OnChange()
{
var combo = document.getElementById('cmbEXTERIOR_CONSTRUCTION');
var index = combo.selectedIndex;
var selectedText;
var comboValue = document.getElementById('cmbEXTERIOR_CONSTRUCTION').value;


if ( index > -1)
{
	selectedText = combo.options[index].text;
}

var tmpXML = document.getElementById('hidFRAME').value
				
				if(tmpXML==null || tmpXML=="")
					{return};
				
				var objXmlHandler = new XMLHandler();
				var tree = (objXmlHandler.quickParseXML(tmpXML).getElementsByTagName('NewDataSet')[0]);
				
				if(tree)
				{				
					for(i=0;i<tree.childNodes.length;i++)
					{	
						var row_item_id = tree.childNodes[i].childNodes[0].childNodes[0].text; 	
						var row_ded_val = tree.childNodes[i].childNodes[1].childNodes[0].text;
						if (row_item_id == comboValue)
						{
							document.getElementById('lblRATED_CLASS').innerHTML = row_ded_val;
							break;
						}
					}
				}

}
				
function setColor(comboBox,txtDesc)
{
	if(document.getElementById(txtDesc).style.display=="none")
		return;
		
	document.getElementById(txtDesc).focus();
	document.getElementById(comboBox).focus();
}
																		                                
function combo_OnChange(comboBox,txtDesc,lblNA)
{
	
	var combo = document.getElementById(comboBox);
	var index = combo.selectedIndex;
	var selectedText;
	
	if ( index > -1)
	{
		selectedText = combo.options[index].text;
	}
	
	if ( selectedText == 'undefined' || selectedText == "Other")
	{
	
		document.getElementById(lblNA).style.display = "none";
		document.getElementById(txtDesc).style.display = "inline";
		
			if (document.getElementById("rfv" + txtDesc.substring(3)) != null)
			{
			
				document.getElementById("rfv" + txtDesc.substring(3)).setAttribute("enabled",true);
				
				if (document.getElementById("rfv" + txtDesc.substring(3)).isvalid == false)
					document.getElementById("rfv" + txtDesc.substring(3)).style.display = "inline";
			}
			
			if (document.getElementById("spn" + txtDesc.substring(3)) != null)
			{
				document.getElementById("spn" + txtDesc.substring(3)).style.display = "inline";
			}
						
	}
	else
	{
	
		document.getElementById(lblNA).style.display = "inline";
		document.getElementById(txtDesc).style.display = "none";
		if (document.getElementById("rfv" + txtDesc.substring(3)) != null)
		{
			document.getElementById("rfv" + txtDesc.substring(3)).setAttribute("enabled",false);
			document.getElementById("rfv" + txtDesc.substring(3)).style.display = "none";
		}
	
	//making the * sign invisible					
	if (document.getElementById("spn" + txtDesc.substring(3)) != null)
	{
		document.getElementById("spn" + txtDesc.substring(3)).style.display = "none";
	}
	}	  
	
}


function setDropdowns()
{			
	if(document.getElementById('cmbWIRING_RENOVATION').selectedIndex == 0 || document.getElementById('cmbWIRING_RENOVATION').selectedIndex == 2)
	{
		document.getElementById('txtWIRING_UPDATE_YEAR').style.display="none";
		document.getElementById('txtWIRING_UPDATE_YEAR').value="";
		document.getElementById('spnWIRING_UPDATE_YEAR').style.display="none";
		document.getElementById('spnWIRING_UPDATE_YEARNA').style.display="inline";
		document.getElementById('rfvWIRING_UPDATE_YEAR').setAttribute("enabled",false);
		document.getElementById('rfvWIRING_UPDATE_YEAR').style.display="none";
	}
	else
	{
		document.getElementById('txtWIRING_UPDATE_YEAR').style.display="inline";
		document.getElementById('spnWIRING_UPDATE_YEAR').style.display="inline";
		document.getElementById('spnWIRING_UPDATE_YEARNA').style.display="none";
		document.getElementById('rfvWIRING_UPDATE_YEAR').setAttribute("enabled",true);				
		
	}
	
	if(document.getElementById('cmbPLUMBING_RENOVATION').selectedIndex ==0 || document.getElementById('cmbPLUMBING_RENOVATION').selectedIndex == 2)
	{
		document.getElementById('txtPLUMBING_UPDATE_YEAR').style.display="none";
		document.getElementById('txtPLUMBING_UPDATE_YEAR').value="";
		document.getElementById('spnPLUMBING_UPDATE_YEAR').style.display="none";
		document.getElementById('spnPLUMBING_UPDATE_YEARNA').style.display="inline";
		document.getElementById('rfvPLUMBING_UPDATE_YEAR').setAttribute("enabled",false);
		document.getElementById('rfvPLUMBING_UPDATE_YEAR').style.display="none";
	}
	else
	{
		document.getElementById('txtPLUMBING_UPDATE_YEAR').style.display="inline";
		document.getElementById('spnPLUMBING_UPDATE_YEAR').style.display="inline";
		document.getElementById('spnPLUMBING_UPDATE_YEARNA').style.display="none";
		document.getElementById('rfvPLUMBING_UPDATE_YEAR').setAttribute("enabled",true);					
		
	}				
	if(document.getElementById('cmbHEATING_RENOVATION').selectedIndex == 0 || document.getElementById('cmbHEATING_RENOVATION').selectedIndex == 2)
	{					
		document.getElementById('txtHEATING_UPDATE_YEAR').style.display="none";
		document.getElementById('spnHEATING_UPDATE_YEAR').style.display="none";
		document.getElementById('spnHEATING_UPDATE_YEARNA').style.display="inline";
		document.getElementById('rfvHEATING_UPDATE_YEAR').setAttribute("enabled",false);
		document.getElementById('rfvHEATING_UPDATE_YEAR').style.display="none";
	}
	else
	{					
		document.getElementById('txtHEATING_UPDATE_YEAR').style.display="inline";
		document.getElementById('spnHEATING_UPDATE_YEAR').style.display="inline";
		document.getElementById('spnHEATING_UPDATE_YEARNA').style.display="none";
		document.getElementById('rfvHEATING_UPDATE_YEAR').setAttribute("enabled",true);					
		
	}				
	if(document.getElementById('cmbROOFING_RENOVATION').selectedIndex  == 0 || document.getElementById('cmbROOFING_RENOVATION').selectedIndex==2 )
	{
		document.getElementById('txtROOFING_UPDATE_YEAR').style.display="none";
		document.getElementById('txtROOFING_UPDATE_YEAR').value="";
		document.getElementById('spnROOFING_UPDATE_YEAR').style.display="none";
		document.getElementById('spnROOFING_UPDATE_YEARNA').style.display="inline";
		document.getElementById('rfvROOFING_UPDATE_YEAR').setAttribute("enabled",false);
		document.getElementById('rfvROOFING_UPDATE_YEAR').style.display="none";
	}
	else
	{
		document.getElementById('txtROOFING_UPDATE_YEAR').style.display="inline";
		document.getElementById('spnROOFING_UPDATE_YEAR').style.display="inline";
		document.getElementById('spnROOFING_UPDATE_YEARNA').style.display="none";
		document.getElementById('rfvROOFING_UPDATE_YEAR').setAttribute("enabled",true);				
		
	}
}
				
function ChkDwellingStartDate(objSource , objArgs)
{
if (document.getElementById("revDWELLING_CONST_DATE").isvalid == true)
	{		
		var effdate=document.POL_HOME_RATING_INFO.txtDWELLING_CONST_DATE.value;
		objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
	}
	else
	{
		objArgs.IsValid = true;	
		
	}
}
		
function DisplayDate()
{ 
	if(document.getElementById('cmbIS_UNDER_CONSTRUCTION').selectedIndex=='2')  
	{
		document.getElementById('lblDWELLING_CONST_DATE').style.display ='none';
		document.getElementById('txtDWELLING_CONST_DATE').style.display ='inline'; 
		document.getElementById("revDWELLING_CONST_DATE").setAttribute('enabled',true); 
		document.getElementById("revDWELLING_CONST_DATE").setAttribute('isValid',true); 
		document.getElementById("csvDWELLING_CONST_DATE").setAttribute('enabled',true); 
		document.getElementById("csvDWELLING_CONST_DATE").setAttribute('isValid',true);
		document.getElementById('hlkDWELLING_CONST_DATE').style.display='inline';
		document.getElementById('trIS_SUPERVISED').style.display='inline';
		document.getElementById("rfvDWELLING_CONST_DATE").setAttribute('enabled',true);
		document.getElementById("spnDWELLING_CONST_DATE").style.display ='inline';
		//document.getElementById("rfvIS_SUPERVISED").style.display ='inline';
		document.getElementById("rfvIS_SUPERVISED").setAttribute('enabled',true);
	} 
else
	{		
		document.getElementById('lblDWELLING_CONST_DATE').style.display ='inline';	
		document.getElementById('txtDWELLING_CONST_DATE').style.display ='none';
		document.getElementById('hlkDWELLING_CONST_DATE').style.display='none';			
		//document.getElementById("revDWELLING_CONST_DATE").setAttribute('isValid',false); 
		//document.getElementById("revDWELLING_CONST_DATE").style.display='none'; 
		//document.getElementById("revDWELLING_CONST_DATE").setAttribute('enabled',false); 
		document.getElementById("csvDWELLING_CONST_DATE").setAttribute('isValid',false); 
		document.getElementById("csvDWELLING_CONST_DATE").style.display='none'; 
		document.getElementById("csvDWELLING_CONST_DATE").setAttribute('enabled',false); 
		document.getElementById('trIS_SUPERVISED').style.display='none';
		document.getElementById("rfvDWELLING_CONST_DATE").setAttribute('enabled',false);
		document.getElementById("rfvDWELLING_CONST_DATE").style.display ='none';
		document.getElementById("spnDWELLING_CONST_DATE").style.display ='none';
		document.getElementById("rfvIS_SUPERVISED").style.display ='none';
		document.getElementById("rfvIS_SUPERVISED").setAttribute('enabled',false);
		EnableValidator('revDWELLING_CONST_DATE',false);
	} 
 }
		// Function to get RATED_CLASS
		function getProtectionClassFromWS()
		{
			if(document.getElementById('cmbPROT_CLASS').selectedIndex != -1)
			{
				var protectionClass = document.getElementById('cmbPROT_CLASS').options[document.getElementById('cmbPROT_CLASS').selectedIndex].text;
				var milesToDwelling = document.getElementById('txtFIRE_STATION_DIST').value;
				var feetToHydrant	= document.getElementById('cmbHYDRANT_DIST').options[document.getElementById('cmbHYDRANT_DIST').selectedIndex].text;
				var lineOfBusiness	= '<%=strCalledFrom%>';
								
				//myTSMain1.useService(path.toString(), "GetProtectionClass");
				//myTSMain1.GetProtectionClass.callService(CallBackFunction, "GetProtectionClass",protectionClass,milesToDwelling,feetToHydrant,lineOfBusiness);		
				PolicyAddHomeRating.AjaxGetProtectionClass(protectionClass,milesToDwelling,feetToHydrant,lineOfBusiness,CallBackFunction);
			}
		}	
			
		function CallBackFunction(Result)
		{	
			if(Result.value!=null)
			{
				if(Result.error)
				{
					var xfaultcode   = Result.errorDetail.code;
					var xfaultstring = Result.errorDetail.string;
					var xfaultsoap   = Result.errorDetail.raw;        								
				}
				else	
				{
					//document.getElementById('txtRATED_CLASS').value = Result.value.text			
					document.getElementById('txtRATED_CLASS').value = Result.value;			
				}	
			}	
				
		}
		function setdefaultPPC()
		{
			document.getElementById('cmbPROT_CLASS').selectedIndex = document.getElementById('hidPPC').value;
		}
		
		//Done for Itrack Issue 6492 on 5 Oct 09
		function chkNo_FAMILIES_FOR_HO_4_HO_6()
		{
			//11195 - HO-4 , 11196 - HO-6
			if(document.getElementById('hidPolicyType').value == "11195" || document.getElementById('hidPolicyType').value == "11196" || document.getElementById('hidPolicyType').value == "11405" || document.getElementById('hidPolicyType').value == "11406")//Done for Itrack Issue 6492 on 16 Oct 09
			{
				document.getElementById("rfvNO_OF_FAMILIES").setAttribute('isValid',false);
				document.getElementById("rfvNO_OF_FAMILIES").setAttribute('enabled',false);
				document.getElementById("rfvNO_OF_FAMILIES").style.display ='none';
				document.getElementById('spnNO_OF_FAMILIES').style.display="none";
			}
		}
		function MAND_ALARM_CERT_ATTACHED()//Function added by Charles on 20-Oct-09 for Itrack 6586
		{			
			var atleastOneBurgFire = false, atleastOneDIRECT = false, atleastOneLOCAL = false;				
			
			if(document.getElementById('cblBurgFire'))
			{
				for(var i=0; i<3;i++)
				{
					var objItem = document.getElementById('cblBurgFire_' + i);
					if(objItem == null) continue;
					if(objItem.checked)
					{
						atleastOneBurgFire = true;
						break;
					}
				}
			}
					
			if(document.getElementById('cblDIRECT'))
			{
				for(var i=0; i<3;i++)
				{
					var objItem = document.getElementById('cblDIRECT_' + i);
					if(objItem == null) continue;
					if(objItem.checked)
					{
						atleastOneDIRECT = true;
						break;
					}
				}
			}
					
			if(document.getElementById('cblLOCAL'))
			{
				for(var i=0; i<2;i++)
				{
					var objItem = document.getElementById('cblLOCAL_' + i);
					if(objItem == null) continue;
					if(objItem.checked)
					{
						atleastOneLOCAL = true;
						break;
					}
				}
			}
			
			if(atleastOneBurgFire==false && atleastOneDIRECT==false && atleastOneLOCAL==true)
			{
				document.getElementById('spnALARM_CERT_ATTACHED').style.display="none";
				document.getElementById("rfvALARM_CERT_ATTACHED").setAttribute('isValid',true);
				document.getElementById("rfvALARM_CERT_ATTACHED").setAttribute('enabled',false);
				document.getElementById("rfvALARM_CERT_ATTACHED").style.display ='none';
			}
			else if(atleastOneBurgFire==true || atleastOneDIRECT==true)
			{
				document.getElementById('spnALARM_CERT_ATTACHED').style.display="inline";
				document.getElementById("rfvALARM_CERT_ATTACHED").setAttribute('enabled',true);
			}
			else
			{
				document.getElementById('spnALARM_CERT_ATTACHED').style.display="none";
				document.getElementById("rfvALARM_CERT_ATTACHED").setAttribute('isValid',true);
				document.getElementById("rfvALARM_CERT_ATTACHED").setAttribute('enabled',false);
				document.getElementById("rfvALARM_CERT_ATTACHED").style.display ='none';
			}
		}
		</script>
	</HEAD>
	<body oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="Initialize();chkNo_FAMILIES_FOR_HO_4_HO_6();ApplyColor();ChangeColor();" MS_POSITIONING="GridLayout"><%--Done for Itrack Issue 6492 on 5 Oct 09--%>		
		<form id="POL_HOME_RATING_INFO" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow>Please 
									note that all fields marked with * are mandatory
								</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capPROT_CLASS" runat="server">Protection class</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPROT_CLASS" onfocus="SelectComboIndex('cmbPROT_CLASS')" runat="server" onchange="getProtectionClassFromWS();"></asp:dropdownlist>
									<asp:requiredfieldvalidator id="rfvPROT_CLASS" Display="Dynamic" ControlToValidate="cmbPROT_CLASS" Runat="server"></asp:requiredfieldvalidator>
									<%--Done for Itrack Issue 6526 on 7 Oct 09- href="#" removed--%>
									<A class="calcolora"><img src="/cms/cmsweb/images/selecticon.gif" id="imgSelect"
									style="CURSOR: hand" border="0" autopostback="false" onclick="setdefaultPPC();"/>
									 </A>
								</TD>
								<TD class="midcolora" width="18%"><asp:label id="capFIRE_STATION_DIST" runat="server">Distance to the fire station</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtFIRE_STATION_DIST" runat="server" size="4" maxlength="4" onBlur="getProtectionClassFromWS();"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvFIRE_STATION_DIST" Runat="server" ControlToValidate="txtFIRE_STATION_DIST" Display="Dynamic"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator Enabled="False" id="revFIRE_STATION_DIST" Runat="server" ControlToValidate="txtFIRE_STATION_DIST" Display="Dynamic"></asp:regularexpressionvalidator>
									<asp:rangevalidator id="rngFIRE_STATION_DIST" runat="server" Display="Dynamic" ControlToValidate="txtFIRE_STATION_DIST" MaximumValue="9999" MinimumValue="1" Type="Integer"></asp:rangevalidator>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capHYDRANT_DIST" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" colspan="3"><asp:dropdownlist id="cmbHYDRANT_DIST" onfocus="SelectComboIndex('cmbHYDRANT_DIST')" runat="server"
										onChange="getProtectionClassFromWS();"></asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvHYDRANT_DIST" Runat="server" ControlToValidate="cmbHYDRANT_DIST" Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capIS_UNDER_CONSTRUCTION" runat="server">Is the dwelling under construction?</asp:label><span class="mandatory" id="spnIS_UNDER_CONSTRUCTION" runat="server">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbIS_UNDER_CONSTRUCTION" onfocus="SelectComboIndex('cmbIS_UNDER_CONSTRUCTION')"
										runat="server">
										<asp:ListItem Value=''></asp:ListItem>
										<asp:ListItem Value='0'>No</asp:ListItem>
										<asp:ListItem Value='1'>Yes</asp:ListItem>
									</asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvIS_UNDER_CONSTRUCTION" runat="server" ControlToValidate="cmbIS_UNDER_CONSTRUCTION"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capDWELLING_CONST_DATE" runat="server">Dwellind start date</asp:label><span id="spnDWELLING_CONST_DATE" class="mandatory" runat="server">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtDWELLING_CONST_DATE" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkDWELLING_CONST_DATE" runat="server" CssClass="HotSpot">
										<ASP:IMAGE id="imgDWELLING_CONST_DATE" runat="server" ImageUrl="../../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
									</asp:hyperlink><asp:label id="lblDWELLING_CONST_DATE" runat="server" CssClass="LabelFont">-N.A.-</asp:label><BR>
									<asp:requiredfieldvalidator id="rfvDWELLING_CONST_DATE" Display="Dynamic" ControlToValidate="txtDWELLING_CONST_DATE"
										Runat="server"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revDWELLING_CONST_DATE" runat="server" ControlToValidate="txtDWELLING_CONST_DATE"
										Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvDWELLING_CONST_DATE" Runat="server" ControlToValidate="txtDWELLING_CONST_DATE"
										Display="Dynamic" ClientValidationFunction="ChkDwellingStartDate"></asp:customvalidator></TD>
							</tr>
							<tr id="trIS_SUPERVISED">
								<td class="midcolora" width="18%"><asp:Label ID="capIS_SUPERVISED" Runat="server"></asp:Label><span id="spnIS_SUPERVISED" class="mandatory" runat="server">*</span></td>
								<td class="midcolora" width="32%"><asp:DropDownList ID="cmbIS_SUPERVISED" Runat="server">
										<asp:ListItem Value=''></asp:ListItem>
										<asp:ListItem Value='0'>No</asp:ListItem>
										<asp:ListItem Value='1'>Yes</asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvIS_SUPERVISED" runat="server" Display="Dynamic" ControlToValidate="cmbIS_SUPERVISED"></asp:requiredfieldvalidator>
									</td>
								<TD class="midcolora" colspan="2"></TD>
							</tr>
						<%--<tr>
								<TD class="midcolora" width="18%"><asp:label id="capIS_AUTO_POL_WITH_CARRIER" runat="server">Does Wolverine Mutual write your Auto policy? </asp:label></TD>
								<TD class="midcolora" width="32%" colspan="3"><asp:dropdownlist id="cmbIS_AUTO_POL_WITH_CARRIER" onfocus="SelectComboIndex('cmbIS_AUTO_POL_WITH_CARRIER')"
										runat="server">
										<asp:ListItem Value=''></asp:ListItem>
										<asp:ListItem Value='0'>No</asp:ListItem>
										<asp:ListItem Value='1'>Yes</asp:ListItem>
									</asp:dropdownlist></TD>
							</tr> --%> <!-- Commented by Charles on 6-Nov-09 for Itrack 6722 -->
							<tr>
								<TD class="headerEffectSystemParams" width="18%" colSpan="4">Property Update Information</TD>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capWIRING_RENOVATION" runat="server">Wiring renovation</asp:label><span class="mandatory" id="spnWIRING_RENOVATION">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbWIRING_RENOVATION" onfocus="SelectComboIndex('cmbWIRING_RENOVATION')" runat="server"
										onchange="JavaScript:setDropdowns();"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbWIRING_RENOVATION')"></A><BR>
									<asp:requiredfieldvalidator id="rfvWIRING_RENOVATION" Runat="server" ControlToValidate="cmbWIRING_RENOVATION"
										Display="Dynamic" ErrorMessage="Please Select the wiring renovation"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" style="WIDTH: 123px" width="123"><asp:label id="capWIRING_UPDATE_YEAR" runat="server">Wiring update year</asp:label><span class="mandatory" id="spnWIRING_UPDATE_YEAR">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtWIRING_UPDATE_YEAR" runat="server" size="4" maxlength="4"></asp:textbox><span class="labelfont" id="spnWIRING_UPDATE_YEARNA">N.A.</span>
									<BR>
									<asp:requiredfieldvalidator id="rfvWIRING_UPDATE_YEAR" runat="server" ControlToValidate="txtWIRING_UPDATE_YEAR"
										Display="Dynamic"></asp:requiredfieldvalidator><asp:customvalidator id="csvpWIRING_UPDATE_YEAR" runat="server" ControlToValidate="txtWIRING_UPDATE_YEAR"
										Display="Dynamic" ClientValidationFunction="ChkOccurenceDate" ErrorMessage="Update Year  should be less or equal to current year."></asp:customvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capPLUMBING_RENOVATION" runat="server">Plumbing renovation</asp:label><span class="mandatory" id="spnPLUMBING_RENOVATION">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPLUMBING_RENOVATION" onfocus="SelectComboIndex('cmbPLUMBING_RENOVATION')"
										runat="server" onchange="JavaScript:setDropdowns();"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvPLUMBING_RENOVATION" Runat="server" ControlToValidate="cmbPLUMBING_RENOVATION"
										Display="Dynamic" ErrorMessage="Please select Plumbing renovation"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" style="WIDTH: 123px" width="123"><asp:label id="capPLUMBING_UPDATE_YEAR" runat="server">Plumbing update year</asp:label><span class="mandatory" id="spnPLUMBING_UPDATE_YEAR">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtPLUMBING_UPDATE_YEAR" runat="server" size="4" maxlength="4"></asp:textbox><span class="labelfont" id="spnPLUMBING_UPDATE_YEARNA">N.A.</span><br>
									<asp:customvalidator id="csvPLUMBING_UPDATE_YEAR" runat="server" ControlToValidate="txtPLUMBING_UPDATE_YEAR"
										Display="Dynamic" ClientValidationFunction="ChkOccurenceDate" ErrorMessage="Update Year  should be less or equal to current year."></asp:customvalidator><BR>
									<asp:requiredfieldvalidator id="rfvPLUMBING_UPDATE_YEAR" runat="server" ControlToValidate="txtPLUMBING_UPDATE_YEAR"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capHEATING_RENOVATION" runat="server">Heating renovation</asp:label><span class="mandatory" id="spnHEATING_RENOVATION">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbHEATING_RENOVATION" onfocus="SelectComboIndex('cmbHEATING_RENOVATION')" runat="server"
										onchange="JavaScript:setDropdowns();" cmbHEATING_RENOVATION></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvHEATING_RENOVATION" Runat="server" ControlToValidate="cmbHEATING_RENOVATION"
										Display="Dynamic" ErrorMessage="Please select Heating renovation "></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" style="WIDTH: 123px" width="123"><asp:label id="capHEATING_UPDATE_YEAR" runat="server">Heating update year</asp:label><span class="mandatory" id="spnHEATING_UPDATE_YEAR">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtHEATING_UPDATE_YEAR" runat="server" size="4" maxlength="4"></asp:textbox><span class="labelfont" id="spnHEATING_UPDATE_YEARNA">N.A.</span><br>
									<asp:customvalidator id="csvHEATING_UPDATE_YEAR" runat="server" ControlToValidate="txtHEATING_UPDATE_YEAR"
										Display="Dynamic" ClientValidationFunction="ChkOccurenceDate" ErrorMessage="Update Year  should be less or equal to current year."></asp:customvalidator><BR>
									<asp:requiredfieldvalidator id="rfvHEATING_UPDATE_YEAR" runat="server" ControlToValidate="txtHEATING_UPDATE_YEAR"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capROOFING_RENOVATION" runat="server">Roofing renovation</asp:label><span class="mandatory" id="spnROOFING_RENOVATION">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbROOFING_RENOVATION" onfocus="SelectComboIndex('cmbROOFING_RENOVATION')" runat="server"
										onchange="JavaScript:setDropdowns();"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvROOFING_RENOVATION" Runat="server" ControlToValidate="cmbROOFING_RENOVATION"
										Display="Dynamic" ErrorMessage="Please select Roofing renovation"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" style="WIDTH: 123px" width="123"><asp:label id="capROOFING_UPDATE_YEAR" runat="server">Roofing update year</asp:label><span class="mandatory" id="spnROOFING_UPDATE_YEAR">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtROOFING_UPDATE_YEAR" runat="server" size="4" maxlength="4"></asp:textbox><span class="labelfont" id="spnROOFING_UPDATE_YEARNA">N.A.</span><br>
									<asp:customvalidator id="csvROOFING_UPDATE_YEAR" runat="server" ControlToValidate="txtROOFING_UPDATE_YEAR"
										Display="Dynamic" ClientValidationFunction="ChkOccurenceDate" ErrorMessage="Update Year  should be less or equal to current year."
										EnableViewState="False"></asp:customvalidator><BR>
									<asp:requiredfieldvalidator id="rfvROOFING_UPDATE_YEAR" runat="server" ControlToValidate="txtROOFING_UPDATE_YEAR"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capNO_OF_AMPS" runat="server">Number of Amps (Elec Sys)</asp:label><span class="mandatory">*</span>
								</TD>
								<%--<TD class="midcolora" width="32%"><asp:textbox id="txtNO_OF_AMPS" runat="server" size="4" maxlength="4"></asp:textbox><BR>--%>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbNO_OF_AMPS" onfocus="SelectComboIndex('cmbNO_OF_AMPS')" runat="server"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvNO_OF_AMPS" Runat="server" ControlToValidate="cmbNO_OF_AMPS" Display="Dynamic"></asp:requiredfieldvalidator>
									<%--<asp:rangevalidator id="rngNO_OF_AMPS" Runat="server" ControlToValidate="cmbNO_OF_AMPS" Display="Dynamic"
										Type="Integer" MinimumValue="1" MaximumValue="9999"></asp:rangevalidator>--%></TD>
								<TD class="midcolora" style="WIDTH: 123px" width="123"><asp:label id="capCIRCUIT_BREAKERS" runat="server">Circuit Breakers</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCIRCUIT_BREAKERS" onfocus="SelectComboIndex('cmbCIRCUIT_BREAKERS')" runat="server"></asp:dropdownlist><br>
									<asp:requiredfieldvalidator id="rfvCIRCUIT_BREAKERS" runat="server" ControlToValidate="cmbCIRCUIT_BREAKERS"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="headerEffectSystemParams" width="18%" colSpan="4">Construction 
									Information</TD>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capNO_OF_FAMILIES" runat="server">Number of Families</asp:label><span id="spnNO_OF_FAMILIES" class="mandatory">*</span></TD><%--Done for Itrack Issue 6492 on 5 Oct 09--%>
								<TD class="midcolora" width="32%"><asp:textbox id="txtNO_OF_FAMILIES" runat="server" size="3" maxlength="3"></asp:textbox><BR>
								<asp:requiredfieldvalidator id="rfvNO_OF_FAMILIES" Display="Dynamic" ControlToValidate="txtNO_OF_FAMILIES"
										Runat="server"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revNO_OF_FAMILIES" runat="server" ControlToValidate="txtNO_OF_FAMILIES" Display="Dynamic"
										Enabled="False"></asp:regularexpressionvalidator><asp:rangevalidator id="rngNO_OF_FAMILIES" runat="server" ControlToValidate="txtNO_OF_FAMILIES" Display="Dynamic"
										Type="Integer" MinimumValue="0" MaximumValue="2"></asp:rangevalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capNEED_OF_UNITS" runat="server">Need # of families/units </asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtNEED_OF_UNITS" runat="server" maxlength="3" size="3"></asp:textbox><br>
									<asp:rangevalidator id="rngNEED_OF_UNITS" runat="server" Display="Dynamic" ControlToValidate="txtNEED_OF_UNITS"
										MaximumValue="999" MinimumValue="1" Type="Integer"></asp:rangevalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCONSTRUCTION_CODE" runat="server">Construction Code</asp:label></TD>
								<TD class="midcolora" colSpan="3"><asp:dropdownlist id="cmbCONSTRUCTION_CODE" runat="server"></asp:dropdownlist></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capEXTERIOR_CONSTRUCTION" runat="server">Exterior construction</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbEXTERIOR_CONSTRUCTION" onfocus="SelectComboIndex('cmbEXTERIOR_CONSTRUCTION')"
										runat="server" OnChange="javascript:cmbEXTERIOR_CONSTRUCTION_OnChange();"></asp:dropdownlist><!--<A class="calcolora" href="javascript:showPageLookupLayer('cmbEXTERIOR_CONSTRUCTION')"></A>--><br>
									<asp:requiredfieldvalidator id="rfvEXTERIOR_CONSTRUCTION" Runat="server" ControlToValidate="cmbEXTERIOR_CONSTRUCTION"
										Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%">Rated Class</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtRATED_CLASS" runat="server" size="2" maxlength="2" ReadOnly="True"></asp:textbox>
									<asp:Label ID="lblRATED_CLASS" class="midcolora" Runat="server" align="middle"></asp:Label><br>
								</TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capFOUNDATION" runat="server">Foundation</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbFOUNDATION" onfocus="SelectComboIndex('cmbFOUNDATION')" runat="server">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><!--<A class="calcolora" href="javascript:showPageLookupLayer('cmbFOUNDATION')"></A>--></TD>
								<TD class="midcolora" width="18%"></TD>
								<TD class="midcolora" width="32%"></TD>
							</tr>
							<tr>
								<td class="midcolora" width="18%"><asp:label id="capROOF_TYPE" runat="server">Roof Type</asp:label><span class="mandatory">*</span></td>
								<td class="midcolora" width="32%"><asp:dropdownlist id="cmbROOF_TYPE" onfocus="SelectComboIndex('cmbROOF_TYPE')" runat="server" OnChange="javascript:combo_OnChange('cmbROOF_TYPE','txtROOF_OTHER_DESC','lblROOF_OTHER_DESC');setColor('cmbROOF_TYPE','txtROOF_OTHER_DESC');">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbROOF_TYPE')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A><br>
									<asp:requiredfieldvalidator id="rfvROOF_TYPE" runat="server" Display="Dynamic" ControlToValidate="cmbROOF_TYPE"></asp:requiredfieldvalidator>
								</td>
								<td class="midcolora" width="18%"><asp:label id="capROOF_TYPE_OTHER_DESC" runat="server">Other Description</asp:label><span class="mandatory" id="spnROOF_OTHER_DESC">*</span></td>
								<td class="midcolora" width="32%"><asp:textbox id="txtROOF_OTHER_DESC" runat="server" size="30" maxlength="50"></asp:textbox><asp:label id="lblROOF_OTHER_DESC" runat="server" CssClass="LabelFont">N.A</asp:label><br>
									<asp:requiredfieldvalidator id="rfvROOF_OTHER_DESC" runat="server" ControlToValidate="txtROOF_OTHER_DESC" Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<td class="midcolora" width="18%"><asp:label id="capPRIMARY_HEAT_TYPE" runat="server">Primary Heat Type</asp:label></td>
								<td class="midcolora" width="32%"><asp:dropdownlist id="cmbPRIMARY_HEAT_TYPE" onfocus="SelectComboIndex('cmbPRIMARY_HEAT_TYPE')" runat="server"
										OnChange="javascript:combo_OnChange('cmbPRIMARY_HEAT_TYPE','txtPRIMARY_HEAT_OTHER_DESC','lblPR_HEAT_OTHER_DESC'); setColor('cmbPRIMARY_HEAT_TYPE','txtPRIMARY_HEAT_OTHER_DESC');">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbPRIMARY_HEAT_TYPE')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
								</td>
								<td class="midcolora" width="18%"><asp:label id="capPRIMARY_HEAT_OTHER_DESC" runat="server">Other Description</asp:label><span class="mandatory" id="spnPRIMARY_HEAT_OTHER_DESC">*</span></td>
								<td class="midcolora" width="32%"><asp:textbox id="txtPRIMARY_HEAT_OTHER_DESC" runat="server" size="30" maxlength="30"></asp:textbox><asp:label id="lblPR_HEAT_OTHER_DESC" runat="server" CssClass="LabelFont">N.A</asp:label><br>
									<asp:requiredfieldvalidator id="rfvPRIMARY_HEAT_OTHER_DESC" runat="server" ControlToValidate="txtPRIMARY_HEAT_OTHER_DESC"
										Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSECONDARY_HEAT_TYPE" runat="server">Secondary Heat</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSECONDARY_HEAT_TYPE" onfocus="SelectComboIndex('cmbSECONDARY_HEAT_TYPE')"
										runat="server" OnChange="javascript:combo_OnChange('cmbSECONDARY_HEAT_TYPE','txtSECONDARY_HEAT_OTHER_DESC','lblSC_HEAT_OTHER_DESC'); setColor('cmbSECONDARY_HEAT_TYPE','txtSECONDARY_HEAT_OTHER_DESC');">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbSECONDARY_HEAT_TYPE')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
								</TD>
								<td class="midcolora" width="18%"><asp:label id="capSECONDARY_HEAT_OTHER_DESC" runat="server">Other Description</asp:label><span class="mandatory" id="spnSECONDARY_HEAT_OTHER_DESC">*</span></td>
								<td class="midcolora" width="32%"><asp:textbox id="txtSECONDARY_HEAT_OTHER_DESC" runat="server" size="30" maxlength="30"></asp:textbox><asp:label id="lblSC_HEAT_OTHER_DESC" runat="server" CssClass="LabelFont">N.A</asp:label><br>
									<asp:requiredfieldvalidator id="rfvSECONDARY_HEAT_OTHER_DESC" runat="server" ControlToValidate="txtSECONDARY_HEAT_OTHER_DESC"
										Display="Dynamic"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capSPRINKER" runat="server">Sprinker</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbSPRINKER" onfocus="SelectComboIndex('cmbSPRINKER')" runat="server"></asp:dropdownlist></TD>
								<TD class="midcolora" colspan="2"></TD>
							</tr>
							<tr>
								<TD class="headerEffectSystemParams" width="18%" colSpan="4">Protective Devices 
									Information</TD>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE width="100%" align="center" border="0">
				<TR id="trProtectiveDevices" runat="server">
					<td class="midcolora" width="18%">Protective devices</td>
					<TD class="midcolora" width="32%"><asp:checkboxlist id="cblBurgFire" runat="server" CssClass="midcolora"></asp:checkboxlist></TD>
					<TD class="midcolora" width="18%"><asp:checkboxlist id="cblDIRECT" runat="server" CssClass="midcolora"></asp:checkboxlist></TD>
					<td class="midcolora" width="32%"><asp:checkboxlist id="cblLOCAL" runat="server" CssClass="midcolora"></asp:checkboxlist></td>
				</TR>
				<tr id="trArmsSupplies" runat="server">
					<td class="midcolora" width="18%"><asp:label id="lblNUM_LOC_ALARMS_APPLIES" runat="server"></asp:label></td>
					<td class="midcolora" width="32%"><asp:textbox id="txtNUM_LOC_ALARMS_APPLIES" runat="server" size="5" maxlength="4"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revNUM_LOC_ALARMS_APPLIES" Runat="server" ControlToValidate="txtNUM_LOC_ALARMS_APPLIES"
							Display="Dynamic" Enabled="False"></asp:regularexpressionvalidator><asp:rangevalidator id="rngNUM_LOC_ALARMS_APPLIES" Runat="server" ControlToValidate="txtNUM_LOC_ALARMS_APPLIES"
							Display="Dynamic" Type="Integer" MinimumValue="1" MaximumValue="9999"></asp:rangevalidator></td>
					<td class="midcolora" colSpan="2">&nbsp;</td>
				</tr>
				<tr>
					<td class="midcolora" width="18%"><asp:Label ID="capALARM_CERT_ATTACHED" Runat="server"></asp:Label><span id="spnALARM_CERT_ATTACHED" class="mandatory" runat="server" style="display:none">*</span></td> <!--Span added by Charles on 20-Oct-09 for Itrack 6586 -->
					<td class="midcolora" width="32%"><asp:DropDownList ID="cmbALARM_CERT_ATTACHED" Runat="server" onfocus="SelectComboIndex('cmbALARM_CERT_ATTACHED')"></asp:DropDownList>
					<br><asp:requiredfieldvalidator id="rfvALARM_CERT_ATTACHED" runat="server" Display="Dynamic" ControlToValidate="cmbALARM_CERT_ATTACHED" ErrorMessage="Please select Alarm Certificate Attached." Enabled="false"></asp:requiredfieldvalidator></td> <!--Validator added by Charles on 20-Oct-09 for Itrack 6586 -->
					<td class="midcolora" colspan="2"></td>
				</tr>
				<!-- place to put suburban class discount -->
				<tr id="trSuburbanClass" runat="server">
					<TD class="headerEffectSystemParams" width="18%" colSpan="4">Suburban Class Information</TD>
				</tr>
				</TABLE>
				<TABLE width="100%" align="center" border="0">
				<tr id="trSuburbanClassDiscount" runat="server">
					<td class="midcolora" width="18%" colspan="1"><asp:label id="CapSUBURBAN_CLASS" Runat="server"  CssClass="midcolora">Suburban Class Discount  </asp:Label><asp:checkbox id="cbSUBURBAN_CLASS" runat="server" onChange=DisplaySuburbanHomeLocation(true); ></asp:checkbox></td>
					<td class="midcolora" width="18%" colspan="2"><asp:label id="CapLOCATED_IN_SUBDIVISION" Runat="server"  CssClass="midcolora"></asp:Label><span class="mandatory" id="spnLOCATED_IN_SUBDIVISION">*</span></td>
					<td class="midcolora" width="32%"> <asp:dropdownlist id="cmbLOCATED_IN_SUBDIVISION" runat="server"  CssClass="MandatoryControl"></asp:dropdownlist>
					</br>
						<asp:requiredfieldvalidator id="rfvLOCATED_IN_SUBDIVISION" runat="server" Display="Dynamic" ControlToValidate="cmbLOCATED_IN_SUBDIVISION" Enabled="true"></asp:requiredfieldvalidator>
					</td>
				</tr>
				<TR>
					<TD class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></TD>
					<TD class="midcolorr" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></TD>
				</TR>
				<tr>
					<td><INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
						<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
						<INPUT id="hidPOL_ID" type="hidden" value="0" name="hidPOL_ID" runat="server"> <INPUT id="hidPOL_VERSION_ID" type="hidden" value="0" name="hidPOL_VERSION_ID" runat="server">
						<INPUT id="hidDWELLING_ID" type="hidden" value="0" name="hidDWELLING_ID" runat="server">
						<INPUT id="hidRowId" type="hidden" value="0" name="hidRowId" runat="server"> <INPUT id="hidDefaultTerr" type="hidden" value="0" name="hidDefaultTerr" runat="server">
						<INPUT id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server">
						<INPUT id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server">
						<INPUT id="hiddyear" type="hidden" value="0" name="dyear" runat="server"> <INPUT id="hidStateID" type="hidden" value="0" name="hidStateID" runat="server">
						<INPUT id="hidFRAME" type="hidden" name="hidFRAME" runat="server"> <INPUT id="hidPolicyType" type="hidden" name="hidPolicyType" runat="server">
						<INPUT id="hidPPC" type="hidden" value="0" name="hidPPC" runat="server">
					</td>
				</tr>
			</TABLE>
		</form>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none; Z-INDEX: 101; POSITION: absolute"><iframe id="lookupLayerIFrame" style="DISPLAY: none; Z-INDEX: 1001; FILTER: alpha(opacity=0); BACKGROUND-COLOR: #000000"
				width="0" height="0" left="0px" top="0px;"></iframe>
		</div>
		<DIV id="lookupLayer" onmouseover="javascript:refreshLookupLayer();" style="Z-INDEX: 102; VISIBILITY: hidden; POSITION: absolute">
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
	
		</script>
	</body>
</HTML>

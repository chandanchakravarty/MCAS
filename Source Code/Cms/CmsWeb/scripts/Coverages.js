var prefix = "dgCoverages_ctl";
var policyPrefix = "dgPolicyCoverages_ctl";

//Disables the cells in the current row
function EnableDisableRow(tr, action)
{
	for( var i = 1; i < tr.cells.length; i++)
	{
		
		tr.cells[i].disabled = action;
	}
}

//Sets the current value of the passed in check box into a hidden field
function SetHiddenField(checkBoxID)
{
	
	var lastIndex = checkBoxID.lastIndexOf('_');
	
	var hid = checkBoxID.substring(0,lastIndex) + '_hidcbDelete';
	
	var hidField = document.getElementById(hid);
	
	if ( hidField != null)
	{
		hidField.value = document.getElementById(checkBoxID).checked;
		hidField.disabled = false;
		//alert(hidField.id + ' ' + hidField.value);
	}
	
	//return hidField;
	
}
	
/****************** SelectDropdownOption ****************
Description:  Is used to select an option of a combobox.
Parameters :  comobobox, selectedValue value to be selected in combo box.
Added By   :  Shafi
Date       :  11/05/06
*/	
function SelectDropdownOptionByText(combo,selectedValue)
{

       	for(var j=0; j<combo.options.length; j++)
		{
		if(selectedValue == combo.options[j].text)
			{
			  	combo.options.selectedIndex = j;
				break;
			}
		}
}					

//Sets the current value of the passed in check box into and accecc its Limit Control into hidden field
function SetHiddenFieldControls(checkBoxID)
{
	
	var lastIndex = checkBoxID.lastIndexOf('_');
	
	var hid = checkBoxID.substring(0,lastIndex) + '_hidLIMIT';
	
	var hidField = document.getElementById(hid);
	
	if ( hidField != null)
	{
		hidField.value = document.getElementById(checkBoxID).innerText;
		//alert(hidField.id + ' ' + hidField.value);
	}
	
	//return hidField;
	
}
//Gets the dropdown with the passed in code
//suffix should be like : _ddlLimit
function GetControlInGridFromCode(covCode, suffix)
{
    
	var xmlDoc = new ActiveXObject("Microsoft.XMLDOM") ; 
	xmlDoc.async=false;
	var strXML = document.getElementById('hidControlXML').value;
	xmlDoc.loadXML(strXML);
	covNode = xmlDoc.selectSingleNode('/Root/COV_CODE[@ID="' + covCode + '"]');
	
	if ( covNode == null ) return null;
	
	newPrefix = covNode.text;
	
	ctrlName = 	newPrefix + suffix;
	ctrl = document.getElementById(ctrlName);
	return ctrl;
	
}

//Gets the dropdown with the passed in code
//suffix should be like : _ddlLimit
function GetPolicyControlFromCode(covCode, suffix)
{
	var rowCount = 50;
	
	if ( document.forms[0].hidPOLICY_ROW_COUNT != null )
	{
		rowCount = document.forms[0].hidPOLICY_ROW_COUNT.value;
	}
	
	//alert(rowCount);
	
	for (ctr = 2; ctr < rowCount + 2; ctr++)
	{
		ctrl = document.getElementById(policyPrefix + ctr + suffix);
		
		if ( ctrl != null )
		{
				//If type of control is checkbox, get coverage code from parent span tag
				//else get it form control attribute
				var type = ctrl.type;
				var coverageCode = '';
				//alert(type);
				
				if ( type == 'checkbox')
				{
						var span = ctrl.parentElement;
									
						if ( span != null )
						{
							coverageCode = span.getAttribute("COV_CODE");
						}
				}
				else
				{
					coverageCode = ctrl.getAttribute("COV_CODE");
				}
				///////////////
				
				if ( trim(covCode) == trim(coverageCode) )
				{
					//alert(covCode + ' ' + coverageCode );
					return ctrl;
				}
			
		}
	}
	
	return null;
			
}
						
//Gets the checkbox with the passed in code
function GetControlFromCode(covCode, suffix)
{
	var rowCount = 50;
	
	if ( document.forms[0].hidROW_COUNT != null )
	{
		rowCount = document.forms[0].hidROW_COUNT.value;
	}
			
	for (ctr = 2; ctr < rowCount + 2; ctr++)
	{
		var ctrl = document.getElementById(prefix + ctr + suffix);
			
			
			if ( ctrl != null )
			{
				//If type of control is checkbox, get coverage code from parent span tag
				//else get it form control attribute
				var type = ctrl.type;
				var coverageCode = '';
				
				if ( type == 'checkbox')
				{
					var span = ctrl.parentElement;
									
						if ( span != null )
						{
							coverageCode = span.getAttribute("COV_CODE");
						}
				}
				else
				{
					coverageCode = ctrl.getAttribute("COV_CODE");
				}
				///////////////
				
				if ( trim(covCode) == trim(coverageCode) )
				{
					return ctrl;
				}
			}
		
	}
	
	return null;
			
}

//Gets the checkbox with the passed in code
function GetAdditionalDDLFromCode(covCode)
{
	var rowCount = 50;
	
	if ( document.forms[0].hidROW_COUNT != null )
	{
		rowCount = document.forms[0].hidROW_COUNT.value;
	}
			
	for (ctr = 2; ctr < rowCount + 2; ctr++)
	{
		chk = document.getElementById(prefix + ctr + "_cbDelete");
		
		if ( chk != null )
		{
			var span = chk.parentElement;
						
			if ( span != null )
			{
				var coverageCode = span.getAttribute("COV_CODE");
		
				if ( trim(covCode) == trim(coverageCode) )
				{
					ddl = document.getElementById(prefix + ctr + "_ddlDEDUCTIBLE");
					//alert(covCode + ' ' + coverageCode );
					return ddl;
				}
			}
		}
	}
	
	return null;
			
}


function GetDeductibleNoCoverageFromCode(covCode)
{
	//lblNoCoverage
	var rowCount = 50;
	
	if ( document.forms[0].hidROW_COUNT != null )
	{
		rowCount = document.forms[0].hidROW_COUNT.value;
	}
			
	for (ctr = 2; ctr < rowCount + 2; ctr++)
	{
		chk = document.getElementById(prefix + ctr + "_cbDelete");
		var lbl = document.getElementById(prefix + ctr + "_lblNoCoverage");
		
		if ( chk != null )
		{
			var span = chk.parentElement;
						
			if ( span != null )
			{
				var coverageCode = span.getAttribute("COV_CODE");
		
				if ( trim(covCode) == trim(coverageCode) )
				{
					//alert(covCode + ' ' + coverageCode );
					
					return lbl;
				}
			}
		}
	}
	
	return null;
}
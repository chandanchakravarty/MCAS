function ShowOtherStructuresDetail(object)
{	
	if (object.selectedIndex==2)
	{
		document.all.trOtherStructuresDetail.style.display='inline';
		document.getElementById('rfvPREMISES_LOCATION').setAttribute('enabled',true);
	}
	else
	{
		document.all.trOtherStructuresDetail.style.display='none';
		document.getElementById('rfvPREMISES_LOCATION').setAttribute('enabled',false);
	}
}

function ShowPremises(object) {
	//var ComboValue="";	
	//On prem		
	if(object.selectedIndex==-1)		
		return;
	
	var	ComboValue = object.options[object.selectedIndex].value;
	
	//if (object.selectedIndex==2)
	//On premises
	if (ComboValue != "") {
	    if (ComboValue == "11841") {
	        if (document.getElementById('CalledFROM') && document.getElementById('CalledFROM').value == "Rental") {
	            document.getElementById('lblRentedDwellingPolicies').style.display = 'none';
	            //Added by Charles on 9-Dec-09 for Itrack 6405
	            document.getElementById('spnPICTURE_ATTACHED').style.display = 'none';
	            document.getElementById('spnCOVERAGE_BASIS').style.display = 'none';
	            document.getElementById('spnPREMISES_DESCRIPTION').style.display = 'none';

	            document.getElementById('rfvPICTURE_ATTACHED').style.display = 'none';
	            document.getElementById("rfvPICTURE_ATTACHED").setAttribute('enabled', false);
	            document.getElementById('rfvCOVERAGE_BASIS').style.display = 'none';
	            document.getElementById("rfvCOVERAGE_BASIS").setAttribute('enabled', false);
	            document.getElementById('rfvPREMISES_DESCRIPTION').style.display = 'none';
	            document.getElementById("rfvPREMISES_DESCRIPTION").setAttribute('enabled', false); //Added till here
	        }
	        document.getElementById("lblOFF_EXCL_COV_BASIS").style.display = "none";  //Added by Charles on 3-Dec-09 for Itrack 6405
	        //document.all.tdCoverageBasis.style.display='inline'; //Commented by Charles on 15-Sep-09 for Itrack 6405
	        document.all.trAddressCity.style.display = 'none';
	        document.all.trStateZip.style.display = 'none';
	        document.all.trAddressHeader.style.display = 'none';
	        document.all.trInsuringValueOffPremises.style.display = 'none';
	        document.all.txtCOVERAGE_AMOUNT.style.display = "none";
	        document.all.capCOVERAGE_AMOUNT.style.display = "none";
	        document.getElementById("revCOVERAGE_AMOUNT").style.display = "none";
	        document.getElementById("revCOVERAGE_AMOUNT").setAttribute('enabled', false);
	        //Added Mohit Agarwal 31-Oct 08 ITrack 4972
	        document.getElementById('revINSURING_VALUE_OFF_PREMISES').setAttribute('enabled', false);
	        document.getElementById('revINSURING_VALUE_OFF_PREMISES').style.display = 'none';
	        //Added by Charles on 15-Sep-09 for Itrack 6405
	        document.all.trInsuringValue.style.display = "none";
	        document.all.spnINSURING_VALUE.style.display = "none";
	        document.getElementById("rfvINSURING_VALUE").style.display = "none";
	        document.getElementById("rfvINSURING_VALUE").setAttribute('enabled', false);
	        document.getElementById("revINSURING_VALUE").style.display = "none";
	        document.getElementById("revINSURING_VALUE").setAttribute('enabled', false);

	        document.all.spnLOCATION_ADDRESS.style.display = 'none';
	        document.all.spnLOCATION_CITY.style.display = 'none';
	        document.all.spnINSURING_VALUE_OFF_PREMISES.style.display = 'none';
	        document.all.spnLIABILITY_EXTENDED.style.display = 'none';

	        document.getElementById('rfvLOCATION_ADDRESS').style.display = "none";
	        document.getElementById('rfvLOCATION_CITY').style.display = "none";
	        document.getElementById('rfvINSURING_VALUE_OFF_PREMISES').style.display = "none";
	        document.getElementById('rfvLIABILITY_EXTENDED').style.display = "none";
	        document.getElementById('rfvLOCATION_ADDRESS').setAttribute('enabled', false);
	        document.getElementById('rfvLOCATION_CITY').setAttribute('enabled', false);
	        document.getElementById('rfvINSURING_VALUE_OFF_PREMISES').setAttribute('enabled', false);
	        document.getElementById('rfvLIABILITY_EXTENDED').setAttribute('enabled', false);
	        //Added till here
	    } //Off Premises
	    else if (ComboValue == "11840")//else if (object.selectedIndex==1)
	    {
	        if (document.getElementById('CalledFROM') && document.getElementById('CalledFROM').value == "Rental") {
	            document.getElementById('lblRentedDwellingPolicies').style.display = 'inline';
	            object.selectedIndex = "-1";
	            ShowPremises(document.getElementById('cmbPREMISES_LOCATION'))
	            return;
	        }
	        document.getElementById("lblOFF_EXCL_COV_BASIS").style.display = "none";  //Added by Charles on 3-Dec-09 for Itrack 6405
	        //document.all.tdCoverageBasis.style.display='none'; //Commented by Charles on 15-Sep-09 for Itrack 6405		
	        document.all.trAddressCity.style.display = 'inline';
	        document.all.trStateZip.style.display = 'inline';
	        document.all.trAddressHeader.style.display = 'inline';
	        document.all.trInsuringValueOffPremises.style.display = 'inline';
	        //document.getElementById('cmbCOVERAGE_BASIS').selectedIndex=0;	//Commented by Charles on 15-Sep-09 for Itrack 6405			
	        document.all.txtCOVERAGE_AMOUNT.style.display = "none";
	        document.all.capCOVERAGE_AMOUNT.style.display = "none";
	        document.getElementById("revCOVERAGE_AMOUNT").style.display = "none";
	        document.getElementById("revCOVERAGE_AMOUNT").setAttribute('enabled', false);
	        document.getElementById('revINSURING_VALUE_OFF_PREMISES').setAttribute('enabled', true);
	        //Added by Charles on 15-Sep-09 for Itrack 6405
	        document.all.spnLOCATION_ADDRESS.style.display = 'inline';
	        document.all.spnLOCATION_CITY.style.display = 'inline';
	        document.all.spnINSURING_VALUE_OFF_PREMISES.style.display = 'inline';
	        document.all.spnLIABILITY_EXTENDED.style.display = 'inline';

	        document.getElementById('rfvLOCATION_ADDRESS').setAttribute('enabled', true);
	        document.getElementById('rfvLOCATION_CITY').setAttribute('enabled', true);
	        document.getElementById('rfvINSURING_VALUE_OFF_PREMISES').setAttribute('enabled', true);
	        document.getElementById('rfvLIABILITY_EXTENDED').setAttribute('enabled', true);

	        document.all.trInsuringValue.style.display = "none";
	        document.all.spnINSURING_VALUE.style.display = "none";
	        document.getElementById("rfvINSURING_VALUE").style.display = "none";
	        document.getElementById("rfvINSURING_VALUE").setAttribute('enabled', false);
	        document.getElementById("revINSURING_VALUE").style.display = "none";
	        document.getElementById("revINSURING_VALUE").setAttribute('enabled', false);
	        //Added till here
	    } //On Premises/Rented to Others/ Other
	    else {
	        if (document.getElementById('CalledFROM') && document.getElementById('CalledFROM').value == "Rental") {
	            document.getElementById('lblRentedDwellingPolicies').style.display = 'inline';
	            object.selectedIndex = "-1";
	            ShowPremises(document.getElementById('cmbPREMISES_LOCATION'))
	            return;
	        }
	        document.getElementById("lblOFF_EXCL_COV_BASIS").style.display = "none";  //Added by Charles on 3-Dec-09 for Itrack 6405	
	        //document.all.tdCoverageBasis.style.display='none'; //Commented by Charles on 15-Sep-09 for Itrack 6405		
	        //Added by Charles on 15-Sep-09 for Itrack 6405
	        document.all.trInsuringValue.style.display = "none";
	        document.all.spnINSURING_VALUE.style.display = "inline";
	        document.getElementById("rfvINSURING_VALUE").setAttribute('enabled', true);
	        document.getElementById("revINSURING_VALUE").setAttribute('enabled', true);
	        document.getElementById("cmpINSURING_VALUE").setAttribute('enabled', false);
	        document.getElementById("cmpINSURING_VALUE").style.display = "none";

	        document.all.spnLOCATION_ADDRESS.style.display = 'none';
	        document.all.spnLOCATION_CITY.style.display = 'none';
	        document.all.spnINSURING_VALUE_OFF_PREMISES.style.display = 'none';
	        document.all.spnLIABILITY_EXTENDED.style.display = 'none';

	        document.getElementById('rfvLOCATION_ADDRESS').style.display = "none";
	        document.getElementById('rfvLOCATION_CITY').style.display = "none";
	        document.getElementById('rfvINSURING_VALUE_OFF_PREMISES').style.display = "none";
	        document.getElementById('rfvLIABILITY_EXTENDED').style.display = "none";
	        document.getElementById('rfvLOCATION_ADDRESS').setAttribute('enabled', false);
	        document.getElementById('rfvLOCATION_CITY').setAttribute('enabled', false);
	        document.getElementById('rfvINSURING_VALUE_OFF_PREMISES').setAttribute('enabled', false);
	        document.getElementById('rfvLIABILITY_EXTENDED').setAttribute('enabled', false);
	        //Added till here		
	        document.all.trAddressCity.style.display = 'none';
	        document.all.trStateZip.style.display = 'none';
	        document.all.trAddressHeader.style.display = 'none';
	        document.all.trInsuringValueOffPremises.style.display = 'none';
	        //document.getElementById('cmbCOVERAGE_BASIS').selectedIndex=0; //Commented by Charles on 15-Sep-09 for Itrack 6405		
	        document.all.txtCOVERAGE_AMOUNT.style.display = "inline";
	        document.all.capCOVERAGE_AMOUNT.style.display = "inline";
	        document.getElementById("revCOVERAGE_AMOUNT").setAttribute('enabled', true);
	        //EnableValidator('revINSURING_VALUE_OFF_PREMISES',false);
	        //Added Mohit Agarwal 31-Oct 08 ITrack 4972
	        document.getElementById('revINSURING_VALUE_OFF_PREMISES').setAttribute('enabled', false);
	        document.getElementById('revINSURING_VALUE_OFF_PREMISES').style.display = 'none';
	    }
	    if (document.getElementById('cmbCOVERAGE_BASIS'))
	        ShowInsuringValue(document.getElementById('cmbCOVERAGE_BASIS'))
	}
}
		
function EnableDisableMandPic()//Function added by Charles on 14-Oct-09 for Itrack 6405
{	
	if (document.getElementById('CalledFROM') && document.getElementById('CalledFROM').value=="Rental")//Added by Charles on 9-Dec-09 for Itrack 6405
	{
		return; 
	}	
	object = document.getElementById('cmbSATELLITE_EQUIPMENT'); 
	if(object.value == "10963")
	{
		document.all.spnPICTURE_ATTACHED.style.display="none";
		document.getElementById("rfvPICTURE_ATTACHED").setAttribute('enabled',false);
		document.getElementById("rfvPICTURE_ATTACHED").style.display="none";	
		
		//Added by Charles on 27-Nov-09 for Itrack 6681
		if(document.getElementById("trSOLID_FUEL_DEVICE"))
		{
			document.all.spnSOLID_FUEL_DEVICE.style.display="none";
			document.getElementById("rfvSOLID_FUEL_DEVICE").setAttribute('enabled',false);
			document.getElementById("rfvSOLID_FUEL_DEVICE").style.display="none";
		}
		//Added till here	 
	}
	else
	{
		document.all.spnPICTURE_ATTACHED.style.display="inline";
		document.getElementById("rfvPICTURE_ATTACHED").setAttribute('enabled',true);
		
		//Added by Charles on 27-Nov-09 for Itrack 6681
		if(document.getElementById("trSOLID_FUEL_DEVICE"))
		{
			document.all.spnSOLID_FUEL_DEVICE.style.display="inline";
			document.getElementById("rfvSOLID_FUEL_DEVICE").setAttribute('enabled',true);
		}
		//Added till here		
	}
}

function ShowInsuringValue(object)
{		
	if(object.value == "11846")
	//if (object.selectedIndex==1) //repair  11846
	{	document.all.trAdditionalAmountofInsuranceDesired.style.display='none';	
		//Added by Charles on 3-Dec-09 for Itrack 6405
		document.getElementById("lblOFF_EXCL_COV_BASIS").style.display="none"; 
		if(document.getElementById("btnSave")) 
		document.getElementById("btnSave").disabled = false;
		
		document.getElementById("trAPPLY_ENDS").style.display="none";		 
		//Added till here
	
		if(document.getElementById("cmbPREMISES_LOCATION").value!='11968')//If added by Charles on 15-Sep-09 for Itrack 6405
		{	document.all.trInsuringValue.style.display='inline';	
			
			//On premises	
			if(document.getElementById("cmbPREMISES_LOCATION").value=='11841') //Added by Charles on 12-Oct-09 for Itrack 6405
			{								
				document.getElementById("revINSURING_VALUE").setAttribute('enabled',true);
				document.getElementById("cmpINSURING_VALUE").setAttribute('enabled',true);
				
				if (document.getElementById('CalledFROM') && document.getElementById('CalledFROM').value!="Rental") //If Check added by Charles on 9-Dec-09 for Itrack 6405
				{
					document.all.spnINSURING_VALUE.style.display="inline";	
					document.getElementById("rfvINSURING_VALUE").setAttribute('enabled',true);
					document.all.trAdditionalAmountofInsuranceDesired.style.display='inline';
					//Added by Charles on 2-Dec-09 for Itrack 6405
					document.getElementById("capADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").style.display="none";				
					document.getElementById("txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").style.display="none";				
					document.getElementById("revADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").style.display="none";
					document.getElementById("revADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").setAttribute('enabled',false);
					
					document.getElementById("capSATELLITE_EQUIPMENT").style.display="inline";				 			
					document.getElementById("cmbSATELLITE_EQUIPMENT").style.display="inline";
					//Added till here
					document.all.spnADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED.style.display="none";				
					document.all.spnSATELLITE_EQUIPMENT.style.display="inline";
					document.getElementById("rfvADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").style.display="none";
					document.getElementById("rfvADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").setAttribute('enabled',false);				
					document.getElementById("rfvSATELLITE_EQUIPMENT").setAttribute('enabled',true);	
				}
			}			
			else
			{				
				document.all.spnINSURING_VALUE.style.display="none";	
				document.getElementById("rfvINSURING_VALUE").style.display="none";
				document.getElementById("rfvINSURING_VALUE").setAttribute('enabled',false);
				document.getElementById("revINSURING_VALUE").style.display="none";
				document.getElementById("revINSURING_VALUE").setAttribute('enabled',false);
				document.getElementById("cmpINSURING_VALUE").style.display="none";
				document.getElementById("cmpINSURING_VALUE").setAttribute('enabled',false);	
				document.all.spnSATELLITE_EQUIPMENT.style.display="none";	
				document.getElementById("rfvSATELLITE_EQUIPMENT").style.display="none";
				document.getElementById("rfvSATELLITE_EQUIPMENT").setAttribute('enabled',false);	
				//Added by Charles on 3-Dec-09 for Itrack 6405
				document.getElementById("rfvADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").style.display="none";
				document.getElementById("rfvADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").setAttribute('enabled',false);
				document.getElementById("revADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").style.display="none";
				document.getElementById("revADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").setAttribute('enabled',false);
				
				if(document.getElementById("cmbPREMISES_LOCATION").value=='11840') //Off Premises
				{
					document.all.trInsuringValue.style.display='none';	
				} //Added till here
						
			}//Added till here
		}
		//On Premises/Rented to Others
		else //Added by Charles on 2-Dec-09 for Itrack 6405
		{ 
			document.all.txtCOVERAGE_AMOUNT.style.display="none";
			document.all.capCOVERAGE_AMOUNT.style.display="none";
			document.getElementById("revCOVERAGE_AMOUNT").style.display="none";
			document.getElementById("revCOVERAGE_AMOUNT").setAttribute('enabled',false);
			
			document.all.spnINSURING_VALUE.style.display="none";	
			document.getElementById("rfvINSURING_VALUE").style.display="none";	
			document.getElementById("rfvINSURING_VALUE").setAttribute('enabled',false);
			document.getElementById("revINSURING_VALUE").style.display="none";	
			document.getElementById("revINSURING_VALUE").setAttribute('enabled',false);		
			document.getElementById("cmpINSURING_VALUE").style.display="none";	
			document.getElementById("cmpINSURING_VALUE").setAttribute('enabled',false);			
			document.all.trInsuringValue.style.display="none";
			
			document.all.trAdditionalAmountofInsuranceDesired.style.display='inline';			
			document.getElementById("capADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").style.display="inline";
			document.getElementById("spnADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").style.display="inline";
			document.getElementById("txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").style.display="inline";
			document.getElementById("rfvADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").setAttribute('enabled',true);
			document.getElementById("revADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").setAttribute('enabled',true);
			
			document.getElementById("capSATELLITE_EQUIPMENT").style.display="none";
			document.getElementById('cmbSATELLITE_EQUIPMENT').options.selectedIndex = -1; 			
			document.getElementById("cmbSATELLITE_EQUIPMENT").style.display="none";
			document.getElementById("spnSATELLITE_EQUIPMENT").style.display="none";
			document.getElementById("rfvSATELLITE_EQUIPMENT").style.display="none";
			document.getElementById("rfvSATELLITE_EQUIPMENT").setAttribute('enabled',false);
		}//Added till here			
	}
	//else if (object.selectedIndex==2) //replacement 11847
	else if(object.value == "11847")
	{	//Added by Charles on 3-Dec-09 for Itrack 6405 
		document.getElementById("lblOFF_EXCL_COV_BASIS").style.display="none";  
		if(document.getElementById("btnSave"))
		document.getElementById("btnSave").disabled = false;
		
		document.getElementById("trAPPLY_ENDS").style.display="none";		
		//Added till here
		
		if(document.getElementById("cmbPREMISES_LOCATION").value!='11968')//If added by Charles on 15-Sep-09 for Itrack 6405
		{
			document.getElementById("rfvINSURING_VALUE").style.display="none";	//Added by Charles on 12-Oct-09 for Itrack 6405
			document.getElementById("rfvINSURING_VALUE").setAttribute('enabled',false);
			document.getElementById("revINSURING_VALUE").style.display="none";
			document.getElementById("revINSURING_VALUE").setAttribute('enabled',false);
			document.getElementById("cmpINSURING_VALUE").style.display="none";
			document.getElementById("cmpINSURING_VALUE").setAttribute('enabled',false);//Added till here
			document.all.trInsuringValue.style.display='none';
		}
		document.all.trAdditionalAmountofInsuranceDesired.style.display='inline';
		//Added by Charles on 2-Dec-09 for Itrack 6405
		document.getElementById("capADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").style.display="inline";
		document.getElementById("txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").style.display="inline";
		document.getElementById("revADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").setAttribute('enabled',true);	
		
		document.getElementById("capSATELLITE_EQUIPMENT").style.display="inline";			
		document.getElementById("cmbSATELLITE_EQUIPMENT").style.display="inline"; //Added till here
		
		//Added by Charles on 12-Oct-09 for Itrack 6405
		if(document.getElementById("cmbPREMISES_LOCATION").value=='11841')//On Premises 
		{
			if (document.getElementById('CalledFROM') && document.getElementById('CalledFROM').value!="Rental") //If Check added by Charles on 9-Dec-09 for Itrack 6405
			{			
				document.all.spnADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED.style.display="inline";	
				document.all.spnSATELLITE_EQUIPMENT.style.display="inline";
				document.getElementById("rfvADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").setAttribute('enabled',true);			
				document.getElementById("rfvSATELLITE_EQUIPMENT").setAttribute('enabled',true);	
			}
		}
		else
		{
			document.all.spnADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED.style.display="none";			
			document.getElementById("rfvADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").style.display="none";
			document.getElementById("rfvADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").setAttribute('enabled',false);
			
			//Added by Charles on 2-Dec-09 for Itrack 6405
			if(document.getElementById("cmbPREMISES_LOCATION").value=='11968') //On Premises/Rented to Others
			{ 
				document.all.txtCOVERAGE_AMOUNT.style.display="none";
				document.all.capCOVERAGE_AMOUNT.style.display="none";
				document.getElementById("revCOVERAGE_AMOUNT").style.display="none";
				document.getElementById("revCOVERAGE_AMOUNT").setAttribute('enabled',false);
				
				document.all.spnINSURING_VALUE.style.display="none";	
				document.getElementById("rfvINSURING_VALUE").style.display="none";	
				document.getElementById("rfvINSURING_VALUE").setAttribute('enabled',false);
				document.getElementById("revINSURING_VALUE").style.display="none";	
				document.getElementById("revINSURING_VALUE").setAttribute('enabled',false);		
				document.getElementById("cmpINSURING_VALUE").style.display="none";	
				document.getElementById("cmpINSURING_VALUE").setAttribute('enabled',false);			
				document.all.trInsuringValue.style.display="none";
				
				document.all.spnADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED.style.display="inline";			
				document.getElementById("rfvADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").setAttribute('enabled',true);	
			}//Added till here				
			
			document.all.spnSATELLITE_EQUIPMENT.style.display="none";			
			document.getElementById("rfvSATELLITE_EQUIPMENT").style.display="none";
			document.getElementById("rfvSATELLITE_EQUIPMENT").setAttribute('enabled',false);
			//Added by Charles on 3-Dec-09 for Itrack 6405
			if(document.getElementById("cmbPREMISES_LOCATION").value=='11840') //Off Premises
			{
				document.getElementById("capADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").style.display="none";
				document.getElementById("txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").style.display="none";
				document.getElementById("revADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").style.display="none";
				document.getElementById("revADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").setAttribute('enabled',false);
			} //Added till here
			
		}//Added till here			
	}
	else
	{	//Added by Charles on 3-Dec-09 for Itrack 6405 
		document.getElementById("lblOFF_EXCL_COV_BASIS").style.display="none";
		if(document.getElementById("btnSave"))
		document.getElementById("btnSave").disabled = false;	
		
		if(document.getElementById("cmbPREMISES_LOCATION").value=='11840') //Off Premises
		{			
			if(object.value == "114213")//Excluded
			{
				document.getElementById("lblOFF_EXCL_COV_BASIS").style.display="inline";
				if(document.getElementById("btnSave"))
				document.getElementById("btnSave").disabled = true;			
			}			
		}
		
		if (document.getElementById('CalledFROM') && document.getElementById('CalledFROM').value!="Rental") //If Check added by Charles on 9-Dec-09 for Itrack 6405
		{
			if(document.getElementById("cmbPREMISES_LOCATION").value=='11841' || document.getElementById("cmbPREMISES_LOCATION").value=='11968')
			{
				if(object.value == "114213")//Excluded
				{
					//document.getElementById("trAPPLY_ENDS").style.display="inline"; // Point #7, #8 not covered in Itrack 6405, covered in diff. Itrack as Enhancement.	
				}
			}		//Added till here
		}
		
		if(document.getElementById("cmbPREMISES_LOCATION").value!='11968')//If added by Charles on 15-Sep-09 for Itrack 6405
		{
			document.getElementById("rfvINSURING_VALUE").style.display="none";	//Added by Charles on 12-Oct-09 for Itrack 6405
			document.getElementById("rfvINSURING_VALUE").setAttribute('enabled',false);
			document.getElementById("revINSURING_VALUE").style.display="none";
			document.getElementById("revINSURING_VALUE").setAttribute('enabled',false);
			document.getElementById("cmpINSURING_VALUE").style.display="none";
			document.getElementById("cmpINSURING_VALUE").setAttribute('enabled',false);//Added till here
			document.all.trInsuringValue.style.display='none';				
		}
				
		//Added by Charles on 2-Dec-09 for Itrack 6405
		if(document.getElementById("cmbPREMISES_LOCATION").value=='11968')
		{ 
			document.all.txtCOVERAGE_AMOUNT.style.display="inline";
			document.all.capCOVERAGE_AMOUNT.style.display="inline";
			document.getElementById("revCOVERAGE_AMOUNT").setAttribute('enabled',true);
			
			document.all.spnINSURING_VALUE.style.display="none";	
			document.getElementById("rfvINSURING_VALUE").style.display="none";	
			document.getElementById("rfvINSURING_VALUE").setAttribute('enabled',false);
			document.getElementById("revINSURING_VALUE").style.display="none";	
			document.getElementById("revINSURING_VALUE").setAttribute('enabled',false);		
			document.getElementById("cmpINSURING_VALUE").style.display="none";	
			document.getElementById("cmpINSURING_VALUE").setAttribute('enabled',false);			
			document.all.trInsuringValue.style.display="none";
		}//Added till here
		
		document.getElementById("rfvADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").style.display="none"; //Added by Charles on 12-Oct-09 for Itrack 6405
		document.getElementById("rfvADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED").setAttribute('enabled',false);	
		document.getElementById("rfvSATELLITE_EQUIPMENT").style.display="none";
		document.getElementById("rfvSATELLITE_EQUIPMENT").setAttribute('enabled',false); //Added till here
		document.getElementById('cmbSATELLITE_EQUIPMENT').options.selectedIndex = -1; //Added by Charles on 2-Dec-09 for Itrack 6405
		document.all.trAdditionalAmountofInsuranceDesired.style.display='none';			
	}
	
	if (document.getElementById('cmbSATELLITE_EQUIPMENT'))//Added by Charles on 14-Oct-09 for Itrack 6405
		EnableDisableMandPic();
}

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

function RightAlign(obj)
{
	if(obj)
		document.getElementById(obj.id).style.textAlign="right";
}

function ClientSideSave()
{		
 		if (document.getElementById('cmbPREMISES_LOCATION').value == "11840")//OFF
		{			
	 	    document.getElementById('txtINSURING_VALUE').value = '';
	 	    document.getElementById('txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED').value='';			
	 	    
			if (document.getElementById('cmbCOVERAGE_BASIS').value  != "11847")//Added by Charles on 14-Oct-09 for Itrack 6405
			{	 			
	 			document.getElementById('cmbSATELLITE_EQUIPMENT').options.selectedIndex = 0;		
	 		}
		}
		else 
		{
			document.getElementById('txtLOCATION_ADDRESS').value = '';
			document.getElementById('txtLOCATION_CITY').value = '';
			document.getElementById('cmbLOCATION_STATE').options.selectedIndex = 0;
			document.getElementById('txtLOCATION_ZIP').value = '';
			document.getElementById('txtINSURING_VALUE_OFF_PREMISES').value = '';
			document.getElementById('cmbLIABILITY_EXTENDED').options.selectedIndex = 0; //Added by Charles on 4-Dec-09 for Itrack 6405
			
			if (document.getElementById('cmbPREMISES_LOCATION').value == "11841")//ON
			{
				if (document.getElementById('cmbCOVERAGE_BASIS').value  == "11847")//Replacement
				{				
					document.getElementById('txtINSURING_VALUE').value = '';
				}
				else if (document.getElementById('cmbCOVERAGE_BASIS').value  == "11846")//Repair
				{									
					document.getElementById('txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED').value='';
				}
				else //Added by Charles on 4-Dec-09 for Itrack 6405
				{
					document.getElementById('txtINSURING_VALUE').value = '';
					document.getElementById('txtADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED').value='';
					document.getElementById('cmbSATELLITE_EQUIPMENT').options.selectedIndex = 0;
				}
			}
			else if (document.getElementById('cmbPREMISES_LOCATION').value == "11968") //On Premises/Rented to Others
			{
				document.getElementById('txtINSURING_VALUE').value = '';				
				
				if (document.getElementById('cmbCOVERAGE_BASIS').value  != "114213") //Excluded
				{
					document.getElementById('txtCOVERAGE_AMOUNT').value = '';
					
					if (document.getElementById('cmbCOVERAGE_BASIS').value == "11846") //Repair
					{
						document.getElementById('cmbSATELLITE_EQUIPMENT').options.selectedIndex = 0;
					}
				}
				
			}//Added till here	
		}
}
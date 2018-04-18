<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" Codebehind="PolicyCoverages.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.PolicyCoverages" validateRequest="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Coverages</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script src="../../cmsweb/scripts/Coverages.js"></script>
		<script language="javascript">
			var prefix = "dgCoverages_ctl";
			var policyPrefix = "dgPolicyCoverages_ctl";
			
			var ShowSaveMsgAlways = true;
			var firstTime = false;
			var flagA9=false;
			var flag17=false;
			var flagminvalue=false;
			var flagUNSCL=false;
			var flagUMPB=false;
			var flagUMPD=false;
			var flag17UMPD=false;
			var flagUNDSP=false;			
			var flagUNCSLM16=false;
			//Displays the controls in a row according to coverage and deductible
			//type	
			function DisableControls(strcbDelete)
			{
				var lastIndex = strcbDelete.lastIndexOf('_');
				var prefix = strcbDelete.substring(0,lastIndex);
							
				strddlLimit = prefix + '_ddlLIMIT';
				strddlDed = prefix + '_ddlDEDUCTIBLE';
				strlblLimit = prefix + '_lblLIMIT';
				strlblDed = prefix + '_lblDEDUCTIBLE';
				strtxtLimit = prefix + '_txtLIMIT';
				strtxtDed = prefix + '_txtDEDUCTIBLE';
				strCovTypeName = prefix + '_lblLIMIT_TYPE';
				strDedTypeName = prefix + '_lblDEDUCTIBLE_TYPE';
				strLimitApplyName = prefix + '_lblIS_LIMIT_APPLICABLE';
				strDedApplyName = prefix + '_lblIS_DEDUCT_APPLICABLE';
				strSigObtName = prefix + '_ddlSignatureObtained';
				strlblSigObt= prefix + '_lblSigObt';
				strhiddSigObt= prefix + '_hiddSigObt';
				//strSignatureObtained= prefix + '_ddlSignatureObtained';	
				strCovType = document.getElementById(strCovTypeName).innerText;
				strDedType = document.getElementById(strDedTypeName).innerText;
				strLimitApply = document.getElementById(strLimitApplyName).innerText;
				strDedApply = document.getElementById(strDedApplyName).innerText;
				
				
				//alert(strCovType);
				//alert(strDedType);
				//alert(strLimitApply);
				//alert(strDedApply);
				
				ddlLimit = document.getElementById(strddlLimit);
				ddlDed = document.getElementById(strddlDed);
				lblLimit = document.getElementById(strlblLimit);
				lblDed = document.getElementById(strlblDed);	
				cbDelete = document.getElementById(strcbDelete);	
				txtLimit = document.getElementById(strtxtLimit);	
				txtDed = document.getElementById(strtxtDed);
				ddlSignatureObtained = document.getElementById(strSigObtName);
				lblSigObt = document.getElementById(strlblSigObt);
				hiddSigObt = document.getElementById(strhiddSigObt);
								
				strrevLimit = prefix + '_revLIMIT';
				strrevDed = prefix + '_revDEDUCTIBLE';
				
				//alert(strrevLimit);	
				revLimit = document.getElementById(strrevLimit);
				revDeductible = document.getElementById(strrevDed);	
				
				//alert(prefix + 'revLIMIT');	
				//alert('dsf' + revLimit);	
				//alert("aa" + strLimitApply);
				
				/*if ( strLimitApply == '0')
				{
					DisableLimits(prefix);
				}
				
				if ( strDedApply == '0' )
				{
					alert(prefix + '   not applied' + strDedApply );
					DisableDeds(prefix);
					//return;
				}*/
				//alert(cbDelete.parentElement.id);
				if ( cbDelete.checked == true )
				{
					/*
					if ( ddlSignatureObtained != null )
					{
						 ddlSignatureObtained.style.display = "inline";
					} 
					
					if ( lblSigObt != null )
					{
						lblSigObt.style.display = "inline";
					}*/
			
					
					//alert(strCovType);
					switch(strCovType)
					{
						case '0':
							//Show "No Coverages.
							//DisableLimits(prefix);
							SetEmptyLimits(prefix);
							break;
						case '1':
							//Flat
							//Show Dropdown
							
						case '2':
							//Split
							//Show Dropdown
								if ( ddlLimit.options.length == 0 )
								{
									lblLimit.style.display = "inline";
									lblLimit.innerText = 'N.A';
									if(ddlLimit != null)
										ddlLimit.style.display = "none";
									
									
								}
								else
								{
									lblLimit.style.display = "none";
									lblLimit.innerText = 'N.A';
									if(ddlLimit != null)
										ddlLimit.style.display = "inline";
									
								}
								if ( txtLimit != null )
								{
									txtLimit.style.display = "none";
								}
								break;	
							case '3':
								//Open
								//Show Textbox
								lblLimit.style.display = "none";
								if(ddlLimit!=null)
									ddlLimit.style.display = "none";
								if(txtLimit!=null)
									txtLimit.style.display = "inline";
								var span1 = cbDelete.parentElement;
								if ( span1 != null ) 
									var covCode1 = span1.getAttribute("COV_CODE"); 
								if ( revLimit != null )
								{
									if(covCode1=='UNDSP'||covCode1=='UNCSL')
										ValidatorEnable(revLimit,false);
									else
										ValidatorEnable(revLimit,true);
								}
								break;		
								
					}
					
					switch(strDedType)
					{
						case '0':
							//Show "No Coverages.
							//alert(strDedType)
							//DisableDeds(prefix);
							SetEmptyDeds(prefix);
							break;
						case '1':
							//Flat
							//Show Dropdown
						case '2':
							//Split
							//Show Dropdown
							if ( ddlDed.options.length == 0 )
							{
								lblDed.style.display = "inline";
								lblDed.innerText = 'N.A';
								ddlDed.style.display = "none";
							}
							else
							{
								lblDed.style.display = "none";
								lblDed.innerText = 'N.A';
								ddlDed.style.display = "inline";
								
							}
							if(txtDed != null)
								txtDed.style.display = "none";
							break;
						case '3':
							//Open
							//Show Textbox
							
								lblDed.style.display = "none";
								if(ddlDed!=null)
									ddlDed.style.display = "none";
								txtDed.style.display = "inline";
								ValidatorEnable(revDeductible,true);
							break;
					}
						
						    //Show "No Coverages.
							//alert(strDedType)
							//DisableDeds(prefix);
							if(lblSigObt != null)
							{
							   lblSigObt.style.display = "inline";
							   //alert('this in first function in label' +  hiddSigObt.value);
							   if (hiddSigObt != null)
							   {
							       hiddSigObt.value="0";
							   }   
							}
							if(ddlSignatureObtained != null)
							  {
							     ddlSignatureObtained.style.display = "inline";
							     lblSigObt.style.display = "none";
							    // alert('this in first function in ddl' + hiddSigObt.value );
							     if (hiddSigObt != null)
							   {
							   
							       hiddSigObt.value="1";
							      // alert('this in first function in ddl after' + hiddSigObt.value );
							   }   
							  }  
							  var span = cbDelete.parentElement;
							var covID = 0;
							if ( span == null ) return;
							var covCode = span.getAttribute("COV_CODE"); 
					        ExtraDependcies(covCode,ddlSignatureObtained,lblSigObt);	
							if(covCode=='UMPD' ||covCode=='MEDPM1'||covCode=='UNDSP'||covCode=='UNCSL'|| covCode=='PUMSP')
								OnDDLChange(ddlLimit);	   
							
				}
				else if ( cbDelete.checked == false )	//checked == false
				{
					
					DisableLimits(prefix);
					DisableDeds(prefix);
					//DisableSignatureObtained(prefix);
				
					if ( ddlSignatureObtained != null )
					{
						ddlSignatureObtained.style.display = "none";
						
						if (hiddSigObt != null)
						{
							hiddSigObt.value="0";
						}	
					}
					
					if ( lblSigObt != null )
					{
						lblSigObt.style.display = "inline";
						if (hiddSigObt != null)
						{
							hiddSigObt.value="0";
						}	
					}
				}
			}
			
			function ExtraDependcies( covCode, ddlSignatureObtained, lblSigObt)
			{
			   var lobState = document.getElementById('hidLOBState').value; 
			   if ( lobState == 'MOTMichigan' || lobState == 'MOTIndiana' || lobState == 'PPAIndiana')
				{
				    
					if (covCode== "PUNCS" || covCode == "PUMSP"  || (covCode == "UNCSL" && lobState != 'MOTIndiana'))
					{
					  
					    	if ( ddlSignatureObtained != null )
							{
								ddlSignatureObtained.style.display = "none";
								if (hiddSigObt != null)
							       hiddSigObt.value="0";

							}
							
							if ( lblSigObt != null )
							{
								lblSigObt.style.display = "inline";
								if (hiddSigObt != null)
							       hiddSigObt.value="0";
							}
					   
					}
				}
			}
			
			//**********************************************************
			
			//Sets the visibility fo Limits dropdowns
			function DisableLimits(prefix)
			{
					lblLimit.style.display = "inline";
					lblLimit.innerText = 'No Coverages';
					if(ddlLimit != null)
						ddlLimit.style.display = "none";
					if(txtLimit != null)
						txtLimit.style.display = "none";		
					if ( revLimit != null )
					{
						ValidatorEnable(revLimit,false);
					}
					
			}
			
			/********* Function to hide Limits/Deductibles ans No Coverage if there is no Limit/Ded applicable
			    for selected coverage ********************/
			///////////////////////////////////
			function SetEmptyLimits(prefix)
			{
					lblLimit.style.display = "inline";
					lblLimit.innerText="Included"
					if(ddlLimit!=null)
						ddlLimit.style.display = "none";
					if(txtLimit != null)
						txtLimit.style.display = "none";
					if ( revLimit != null )
					{
						ValidatorEnable(revLimit,false);
					}
			}
			function SetEmptyDeds(prefix)
			{
					lblDed.style.display = "none";
					if(ddlDed!=null)
						ddlDed.style.display = "none";
					if(txtDed!=null)
						txtDed.style.display = "none";
					if ( revDeductible != null )
					{
						ValidatorEnable(revDeductible,false);
					}
			}
			
			
			function SetRejRedUmCoverageMotor()
			{
			   var cbRRUM = GetControlInGridFromCode('RRUMM','_cbDelete');
			   if(cbRRUM == null) return;
			   
			   if(flagA9==true || flagUMPD==true || flagUNCSLM16==true)
			   {
			        var lblRRUM   =  GetControlInGridFromCode('RRUMM','_lblLIMIT');
			     	cbRRUM.checked = true;
					DisableControls(cbRRUM.id);
					SetHiddenField(cbRRUM.id);
					lblRRUM.innerText = 'Included'
					lblRRUM.style.display='inline'
			   }
			   else
			   {
			     	cbRRUM.checked = false;
					DisableControls(cbRRUM.id);
					SetHiddenField(cbRRUM.id);
				   
			   }
			}
			
			function SetRejRedUmCoverageMotor17()
			{
			  var cb17 = GetControlInGridFromCode('E17','_cbDelete');
			  if(cb17 == null) return;
			   //return;
			   if(flag17==true || flag17UMPD == true)
			   {
					cb17.checked = true;
					DisableControls(cb17.id);
					SetHiddenField(cb17.id);

					
			   }
			   else
			   {
				    cb17.checked = false;
					DisableControls(cb17.id);
					SetHiddenField(cb17.id);
			   }
			  //Added By Pravesh op n 12 Jan 09 Itrack 4647 
			  var UMPDeuctibleDDL = GetControlInGridFromCode('UMPD','_ddlLIMIT');
			  var chkUMPD = GetControlInGridFromCode('UMPD','_cbDelete');
			  if (UMPDeuctibleDDL!=null)
			    currentAmt = ReplaceAll(UMPDeuctibleDDL.options[UMPDeuctibleDDL.selectedIndex].text,',','');
				if ( trim(currentAmt) == 'Reject' &&  (document.getElementById('hidLOBState').value=="MOTIndiana" || document.getElementById('hidLOBState').value=="PPAIndiana")&& chkUMPD.checked==true)
				{
				    cb17.checked = false;
					DisableControls(cb17.id);
					SetHiddenField(cb17.id);
					cb17.disabled = true;
				}
				else
				cb17.disabled = false;
				//End here Itracak 4647			   
			}
		
			
			//Rejection or Reductions of UM Coverage
			
			function SetRejRedUmCoverage()
			{
			   var cbRRUM = GetControlInGridFromCode('RRUM','_cbDelete');
			   if(cbRRUM == null) return;
			   if(firstTime != false) return;
			   
			   if(flagA9 ==true || flagUMPB  == true || (flagUNSCL ==true && flagminvalue == true) || flagUNDSP==true )
			   {
			        var lblRRUM   =  GetControlInGridFromCode('RRUM','_lblLIMIT');
			     	cbRRUM.checked = true;
					DisableControls(cbRRUM.id);
					SetHiddenField(cbRRUM.id);
					lblRRUM.innerText = 'Included'
					lblRRUM.style.display='inline'
			   }
			   else
			   {
			     	cbRRUM.checked = false;
					DisableControls(cbRRUM.id);
					SetHiddenField(cbRRUM.id);
			   }
			}
			
			
			/*
			function CheckMinmumValue(objSource, objArgs)
			{
			     var dedCoverageD  = document.getElementById(objSource.controltovalidate);
		    	 var strCheck      = ReplaceAll(dedCoverageD.id,'_ddlDEDUCTIBLE','_cbDelete');
		    	 var cbCheck       = document.getElementById(strCheck);
		    	 if(cbCheck.checked == false) return;
		    	 
		    	 var ddlAmount =dedCoverageD.options[dedCoverageD.selectedIndex].text;
		    	 var Claim= document.getElementById('hidClaims').value;
		    	 ddlAmount=ReplaceAll(ddlAmount,',','');
		    	 if(parseInt(Claim) ==2 && parseInt(ddlAmount) < 150)
		    	 {
		    	    objArgs.IsValid = false;
		    	    return;
		    	 }
		        else if(parseInt(Claim) ==3 && parseInt(ddlAmount) < 250)
		    	 {
		    	    objArgs.IsValid = false;
		    	    return;
		    	 }
		    	 else if(parseInt(Claim) >= 4 && parseInt(ddlAmount) < 500)
		    	 {
		    	    objArgs.IsValid = false;
		    	    return;
		    	 }
		    	 else
		    	 {
		    	     objArgs.IsValid = true;
		    	 }
			
			}
			//if the Make of the vehicle is "Corvette"
            //if they select the coverage Collision or Comprehensive the minimum deductible 
            //for both coverages is $500
			function MinCollisionComp(objSource, objArgs)
			{
		    	 var dedCoverageD  = document.getElementById(objSource.controltovalidate);
		    	 var strCheck      = ReplaceAll(dedCoverageD.id,'_ddlDEDUCTIBLE','_cbDelete');
		    	 var cbCheck       = document.getElementById(strCheck);
		    	 if(cbCheck.checked == false) return;
		    	 
		    	 var ddlAmount =dedCoverageD.options[dedCoverageD.selectedIndex].text;
		    	 var ddlVehAmount= document.getElementById('hidVehicleAmount').value;
		    	 var hidVehicleType= document.getElementById('hidVehicleType').value;
		    	 ddlAmount=ReplaceAll(ddlAmount,',','');
		    	 ddlVehAmount=ReplaceAll(ddlVehAmount,',','');
		    	 objSource.innerHTML=" Minimum deductible for the Type of Vehicle/ Value  is 500"
		    	 if(hidVehicleType=="11336")
		    	 {
		    	    if(parseInt(ddlVehAmount)>= 50000)
		    		{
		    			if(parseInt(ddlAmount) < 2000)
		    			{
		    			    objSource.innerHTML=" Minimum deductible for the Type of Vehicle/ Value  is 2,000"
		    				objArgs.IsValid = false;
		    				return;
		    			}
		    		}
		    	 }
		    	if(parseInt(ddlAmount)>= 500)
		    	{
		    		objArgs.IsValid = true;
		    		return;
		    	}
		    	objArgs.IsValid = false;
		    	return;
			}
			*/
			////Sets the visibility of Deductibles dropdowns
			function DisableDeds(prefix)
			{
					lblDed.style.display = "inline";
					lblDed.innerText = 'No Coverages';
					if(ddlDed != null)
						ddlDed.style.display = "none";
					if(txtDed != null)
						txtDed.style.display = "none";
					if ( revDeductible != null )
					{
						ValidatorEnable(revDeductible,false);
					}
			}

			function Refresh()
			{
				//document.Form1.submit();
				document.location.href = document.location.href;
				
				document.getElementById('lblMessage').style.display = "inline";
				document.getElementById('lblMessage').innerText = "Coverages copied successfully.";
			}
		
			function OpenPopupWindow(Url)
			{
				var ret = confirm('Are you sure you want to copy coverages and lose the existing ones?');
				
				if ( ret == false ) return false;
				
				var myUrl = Url;
				window.open(myUrl,'','toolbar=no, directories=no, location=no, status=yes, menubar=no, resizable=yes, scrollbars=no,  width=500, height=500'); 
				return false;		
			}
			
			
		
			//Gets the checkbox with the passed in code
			function GetPolicyLimitTextBoxFromCode(covCode)
			{
				var rowCount = 20;
				
				if ( document.Form1.hidPOLICY_ROW_COUNT != null )
				{
					rowCount = document.Form1.hidPOLICY_ROW_COUNT.value;
				}
				
				//alert(rowCount);
				
				for (ctr = 2; ctr < rowCount + 2; ctr++)
				{

				    if (ctr < 10) {
				        chk = document.getElementById(policyPrefix + '0' + ctr + "_cbDelete");
				        var tb = document.getElementById(policyPrefix + '0' + ctr + "_txtLIMIT");
				    }
				    else {
				        chk = document.getElementById(policyPrefix + ctr + "_cbDelete");
				        var tb = document.getElementById(policyPrefix + ctr + "_txtLIMIT");
				    }
				
									
					if ( chk != null )
					{
						var span = chk.parentElement;
									
						if ( span != null )
						{
							var coverageCode = span.getAttribute("COV_CODE");
					
							if ( trim(covCode) == trim(coverageCode) )
							{
								//alert(covCode + ' ' + coverageCode );
								return tb;
							}
						}
					}
				}
				
				return null;
						
			}
			
			function EbableMisExtraEquiA15()
			{
			  
			    var cbEECOMP = GetControlInGridFromCode('EECOMP','_cbDelete');
				if(cbEECOMP == null) return;
				var span = cbEECOMP.parentElement;
				var covID = 0;
				if ( span == null ) return;
				var covID = span.getAttribute("COV_ID");
				
			    var cbCOMP = GetControlInGridFromCode('COMP','_cbDelete');
			    if(cbCOMP == null)
			    {
			      cbCOMP = GetControlInGridFromCode('OTC','_cbDelete');
			    } 
			    var cbCOLL = GetControlInGridFromCode('COLL','_cbDelete');
			    var cbEECOLL = GetControlInGridFromCode('EECOLL','_cbDelete');
				/*if(cbCOMP != null && cbCOLL != null)  commented by Pravesh on 7 july 2008 itrack# 4432 
				{
					if(cbCOLL.checked == true && cbCOMP.checked == true)
					{
					    EnableDisableRow(cbEECOMP.parentElement.parentElement.parentElement, false);
					   	cbEECOMP.disabled=false;
					}
				}*/
				cbEECOMP.disabled=true;
				cbEECOLL.disabled=true;
				//added  by Pravesh on 7 july 2008 itrack# 4432 
				if(cbCOMP != null)
				{
					if(document.getElementById("hidMIS_COUNT").value!="0" && cbCOMP.checked == true)
					{
					    cbEECOMP.checked = true;
					    EnableDisableRow(cbEECOMP.parentElement.parentElement.parentElement, false);
					    //onButtonClick(cbEECOMP,0);
					   	cbEECOMP.disabled=true;
						DisableControls(cbEECOMP.id);
						var DeductibleEECOMP  = GetControlInGridFromCode('EECOMP','_ddlDEDUCTIBLE');
						SetHiddenField(cbEECOMP.id);
						//DeductibleEECOMP.disabled=true;

					}
				}
				if(cbCOLL != null)
				{
					if(document.getElementById("hidMIS_COUNT").value!="0" && cbCOLL.checked == true)
					{
					    cbEECOLL.checked = true;
					    EnableDisableRow(cbEECOLL.parentElement.parentElement.parentElement, false);
					    onButtonClick(cbEECOLL,0);
					   	cbEECOLL.disabled=true;
						DisableControls(cbEECOLL.id);
						var DeductibleEECOLL  = GetControlInGridFromCode('EECOLL','_ddlDEDUCTIBLE');
						SetHiddenField(cbEECOLL.id);
						//DeductibleEECOLL.disabled=true;

					}
				}
				// added by pravesh end here
			}
			
				//
			function EnableDisbaleSignDDlUMPD()
			{
			        var cbUMPD  = GetControlInGridFromCode('UMPD','_cbDelete');
			        if(cbUMPD == null) return;
			        if(cbUMPD.checked == false) return;
			        
			        var ddl  = GetControlInGridFromCode('UMPD','_ddlLIMIT');
			        if(ddl == null) return;
			        var sigDDL = GetSigDDL(ddl);
					var lblSigObt = GetSigObtLabel(ddl);
					//var hiddSigObt = GetControlInGridFromCode('UMPD','_hiddSigObt'); 
					var hiddSigObt = GethiddSigObtLabel(ddl);
					var currentAmt = ReplaceAll(ddl.options[ddl.selectedIndex].text,',','');
			
					//Get PD Amount
					//var ddlPD = GetPolicyLimitDDLCode('PD');
					var ddlPD = GetControlInGridFromCode('PD','_ddlLimit');
					var cbPD = GetControlInGridFromCode('PD','_cbDelete');
					if ( ddlPD == null) return;
					var PDAmt = ReplaceAll(ddlPD.options[ddlPD.selectedIndex].text,',','');
					PDAmt = ReplaceAll(PDAmt,',','');


					if ( sigDDL != null)
					{
						sigDDL.style.display = "none";
							if (hiddSigObt != null)
							       hiddSigObt.value="0";
					

					}
								
				    if ( lblSigObt != null)
					{
						lblSigObt.style.display = "inline";
						if (hiddSigObt != null)
							       hiddSigObt.value="0";
					
					}


					//Rejection case///////////////////////////////////////////
					if ( trim(currentAmt) == 'Reject' )
					{
						
						if ( sigDDL != null )
						{
							sigDDL.style.display = "inline";
							if (hiddSigObt != null)
							       hiddSigObt.value="1";
					
						}
						
						if ( lblSigObt != null )
						{
							lblSigObt.style.display = "none";
							if (hiddSigObt != null)
							       hiddSigObt.value="1";
					

						}
						flag17UMPD =false;
						SetRejRedUmCoverageMotor17();
						return;
					}
                        flag17UMPD=true;	
						SetRejRedUmCoverageMotor17();
					////////////////////////////////////////////////////////////////
					//Ravindra(04-26-2006) Rule of lesser amount than PD not applicable for Indiana
					var lobState = document.getElementById('hidLOBState').value;	
					if(cbPD.checked == true)
					{
						if (parseInt(currentAmt) < parseInt(PDAmt) && lobState != 'MOTIndiana')
							{
														
								if ( sigDDL != null)
								{
									sigDDL.style.display = "inline";
									if (hiddSigObt != null)
								       hiddSigObt.value="1";
								}
								
								if ( lblSigObt != null)
								{

									lblSigObt.style.display = "none";
									if (hiddSigObt != null)
								       hiddSigObt.value="1";
								}
							
							}
						else
							{
					
								if ( sigDDL != null)
								{
									sigDDL.style.display = "none";
								if (hiddSigObt != null)
								       hiddSigObt.value="0";
								}
								
								if ( lblSigObt != null)
								{
									lblSigObt.style.display = "inline";
									if (hiddSigObt != null)
								       hiddSigObt.value="0";





								}
							}
						}
			}
			//Called on OnClick of check box
			function onButtonClick(chk,rowCount)
			{
			/***********************************/
			  
				var span = chk.parentElement;
				var covID = 0;
				if ( span == null ) return;
				
				SetHiddenField(chk.id);
				var covID = span.getAttribute("COV_ID");
				var covCode = span.getAttribute("COV_CODE");
				//alert('Button CLick --  ' + covCode);
				var lastIndex1 = chk.id.lastIndexOf('_');
				var dgPrefix = chk.id.substring(0,lastIndex1);
				var lobState = document.getElementById('hidLOBState').value;	
				
				if ( chk.checked == true )
				{		
					if(covCode =='COMP' || covCode == 'OTC' || covCode == 'COLL' )
					{
					   EbableMisExtraEquiA15()
					}
					var toDisable = GetControlInGridFromCode(covCode,'_hidCHECKDDISABLE');
					//alert(toDisable.value);
					if(toDisable==null)return;
					
					var lastIndex = toDisable.id.lastIndexOf('_');
					var prefix = toDisable.id.substring(0,lastIndex);
					var toEnable =  document.getElementById(prefix + '_hidCHECKDENABLE');
					var toUncheck = document.getElementById(prefix + '_hidCHECKDDSELECT');
					var toCheck =document.getElementById(prefix +'_hidCHECKDSELECT');
				}
				else if ( chk.checked == false )
				{
					
					var toDisable = GetControlInGridFromCode(covCode,'_hidUNCHECKDDISABLE');
					if(toDisable==null)return;
					
					var lastIndex = toDisable.id.lastIndexOf('_');
					var prefix = toDisable.id.substring(0,lastIndex);
					
					var toEnable =  document.getElementById(prefix + '_hidUNCHECKDENABLE');
					var toUncheck = document.getElementById(prefix + '_hidUNCHECKDDSELECT');
					var toCheck =document.getElementById(prefix +'_hidUNCHECKDSELECT');
				}
				
				
				if(trim(toDisable.value) != "")
				{
					var toDisableArray =toDisable.value.split(",");
					for(i=0;i < toDisableArray.length ;i++)
					{
						var cbCTRL = GetControlInGridFromCode(toDisableArray[i],'_cbDelete');
						 //alert(toDisableArray[i]);
						if (toDisableArray[i]=='UMPD')
						{
							var cbPUNCS = GetControlInGridFromCode('PUNCS','_cbDelete');
							var cbPUMSP = GetControlInGridFromCode('PUMSP','_cbDelete');
							var cbRLCSL = GetControlInGridFromCode('RLCSL','_cbDelete');
							if((cbPUNCS!=null && cbPUNCS.checked==true) || (cbPUMSP!=null && cbPUMSP.checked==true))
								continue;
							if(cbRLCSL!=null && cbRLCSL.checked==true && lobState=='MOTIndiana')	//Itrack 4647
								continue;
						}
						if ( cbCTRL != null )
						{
							//cbCTRL.parentElement.parentElement.parentElement.disabled = true;
							if (document.getElementById('hidIsunder25').value=='1' && document.getElementById('hidattachUmb').value=='1' && toDisableArray[i]=='RLCSL' && lobState=='MOTMichigan') continue; //itrack 5354
							EnableDisableRow(cbCTRL.parentElement.parentElement.parentElement, true);
							cbCTRL.disabled = true;
						}
					}
				}
				if(trim(toEnable.value) != "")
				{
					var toEnableArray = toEnable.value.split(",");
					for(i=0;i < toEnableArray.length ;i++)
					{
						var cbCTRL = GetControlInGridFromCode(toEnableArray[i],'_cbDelete');
						if ( cbCTRL != null )
						{
							//cbCTRL.parentElement.parentElement.parentElement.disabled = false;
							EnableDisableRow(cbCTRL.parentElement.parentElement.parentElement, false);
							cbCTRL.disabled = false;
						}
					}
				}
				if(trim(toUncheck.value) != "")
				{
				  	var toUnCheckArray =toUncheck.value.split(",");
					for(i=0;i < toUnCheckArray.length ;i++)
					{
						var cbCTRL = GetControlInGridFromCode(toUnCheckArray[i],'_cbDelete');
						if (toUnCheckArray[i]=='UMPD')
						{
							var cbPUNCS = GetControlInGridFromCode('PUNCS','_cbDelete');
							var cbPUMSP = GetControlInGridFromCode('PUMSP','_cbDelete');
							var cbRLCSL = GetControlInGridFromCode('RLCSL','_cbDelete');
							if((cbPUNCS!=null && cbPUNCS.checked==true) || (cbPUMSP!=null && cbPUMSP.checked==true))
								continue;
							if(cbRLCSL!=null && cbRLCSL.checked==true && lobState=='MOTIndiana')	//Itrack 4647
								continue;
						}
						if (toUnCheckArray[i]=='UNDSP') flagUNDSP=false;
						if (toUnCheckArray[i]=='UNCSL') flagUNCSLM16=false;
						if (toUnCheckArray[i]=='UMPD' && lobState=='MOTIndiana') flagUMPD=false;
						if ( cbCTRL != null )
						{
							cbCTRL.checked = false;
							DisableControls(cbCTRL.id);
							SetHiddenField(cbCTRL.id);
						}
					}
					if(covCode == "PUMSP")
					{
						SetRejRedUmCoverageMotor();
					}
				}
				if(trim(toCheck.value) != "")
				{
					var toCheckArray =toCheck.value.split(",");
					for(i=0;i < toCheckArray.length ;i++)
					{
						var cbCTRL = GetControlInGridFromCode(toCheckArray[i],'_cbDelete');
						var ddlCTRL = GetControlInGridFromCode(toCheckArray[i],'_ddlLIMIT');
						if ( cbCTRL != null )
						{
							if(firstTime==false)
								cbCTRL.checked = true;
							DisableControls(cbCTRL.id);
							if(firstTime==false)
								OnDDLChange(ddlCTRL);
							SetHiddenField(cbCTRL.id);
						}
					}
				}
				
				
				
				//UMPD Uninsured Motorists (PD) (A-21) /////
				// To override rule in Coverage Rule XML 
				/*
				if (covCode == 'SLL' || covCode == 'RLCSL' || covCode == 'BISPL' || covCode == 'PD')
				{
					var cbUMPD = GetControlInGridFromCode('UMPD','_cbDelete');
					if ( chk.checked == true )
					{			
						if ( cbUMPD != null )
						{
							//cbUMPD.checked = false;	
							cbUMPD.disabled = false;
							cbUMPD.parentElement.parentElement.parentElement.disabled = false;
							DisableControls(cbUMPD.id);
							SetHiddenField(cbUMPD.id);
						}
					}
					else	//if CSL checked == false
					{
						if ( cbUMPD != null )
						{
						 	if ( firstTime == false )
							{
								cbUMPD.checked = false;	
							}
							cbUMPD.disabled = true;
							cbUMPD.parentElement.parentElement.parentElement.disabled = true;
							DisableControls(cbUMPD.id);
							SetHiddenField(cbUMPD.id);
						}
					 }
				}
				if ( covCode == 'PUNCS' ||  covCode == 'PUMSP' )
				{
					var cbUMPD = GetControlInGridFromCode('UMPD','_cbDelete');
					
					if ( GetControlInGridFromCode('PUNCS', '_cbDelete').checked==true || 
						 GetControlInGridFromCode('PUMSP', '_cbDelete').checked==true)
					{
						//alert(covCode + ' *** ' + cbUMPD.id);
						//Bodily Injury Liability ( Split Limit) 
						if ( cbUMPD != null )
						{
							cbUMPD.parentElement.parentElement.parentElement.disabled = false;
							cbUMPD.disabled = false;
							DisableControls(cbUMPD.id);
							SetHiddenField(cbUMPD.id);
						}
					}
					else
					{					
						//alert(covCode + ' *** ' + cbUMPD.id);
						//Bodily Injury Liability ( Split Limit) 
						if ( cbUMPD != null )
						{
							cbUMPD.parentElement.parentElement.parentElement.disabled = true;
							cbUMPD.disabled = true;
							cbUMPD.checked = false;
							DisableControls(cbUMPD.id);
							SetHiddenField(cbUMPD.id);
						}
					}
				}
				
				*/
				//////////////End of UMPD//////////////////////////
				
				//Get the current ddl
				var lastIndexOfUnderscore = chk.id.lastIndexOf('_');
				var prefixCtrl = chk.id.substring(0,lastIndexOfUnderscore);
				
				var ddlName = prefixCtrl + '_ddlLimit';
				var ddl = document.getElementById(ddlName);
			    
			    //If user checks this coverage, default "Yes" in sig obtained dropdown.
			    // If the user select "No" in drop down, uncheck this coverage
			    if(covCode=="PIP" || covCode== "ENO")
			    {
			      EnableDisableAddInfor()
			    }
			 
			    if(covCode == "EP95")
			    {
			      
			      var lblLimitCAB91       = GetControlInGridFromCode('EP95','_lblLIMIT');
			      if ( chk.checked == true )
					{
					  var dedDeductibleEP95  = GetControlInGridFromCode('EP95','_ddlSignatureObtained');
					  lblLimitCAB91.innerText ='Included';
					  lblLimitCAB91.style.display="inline";
					  dedDeductibleEP95.options.selectedIndex=1;
					  DisableControls(chk.id); 
					}
					else
					{
					   lblLimitCAB91.innerText="No Coverages";
					   lblLimitCAB91.style.display="none";
					}
			    
			    }
			    if(covCode == "MHT22")
			    {
			      
			      var lblLimitCAB91       = GetControlInGridFromCode('MHT22','_lblLIMIT');
			      if ( chk.checked == true )
					{
					  
					  lblLimitCAB91.innerText ='Included';
					  lblLimitCAB91.style.display="inline";
					}
					else
					{
					   lblLimitCAB91.innerText="No Coverages";
					   lblLimitCAB91.style.display="none";
					}
			    
			    }
				
				//Reject cases
				//Uninsured Motorists (CSL)/////////
				//Uninsured Motorists (PD) (A-21) 
				//Uninsured Motorists (BI Split Limit)
				
				//For PIP Diductibe has to be changed
				if ( covCode == 'PUNCS' ||  covCode == 'UMPD' || covCode == 'PUMSP' || covCode == 'PIP' || covCode == 'MEDPM1' || (covCode == 'UNDSP' && lobState=='PPAIndiana') || (covCode == 'UNCSL' && lobState=='PPAIndiana'))
				{	
					if ( chk.checked == true )
					{
					   if(covCode == 'MEDPM1' && firstTime == false)
					  	{
					  	 var MedicallimitDdl = GetControlInGridFromCode('MEDPM1','_ddlLIMIT');
					  	 //SelectDropdownOptionByText(MedicallimitDdl,"5,000 1st Party Medical-Excess")
					  	 var intRet=SelectDropdownOptionByValueWithReturn(MedicallimitDdl,853)
					  	}
						//alert(covCode);
						OnDDLChange(ddl);	
					}
				}
				
				
				if ( covCode == 'LPD')
					{
						//Call Set Hidden field for Helmet, getting checked from XML file
						var cbLPD  = GetControlInGridFromCode('cbLPD','_cbDelete');
					    var txtDeductibleCAB91  = GetControlInGridFromCode('LPD','_txtLimit');
					    var lblDeductibleCAB91  = GetControlInGridFromCode('LPD','_lblLIMIT');
					    if(chk.checked == true)
						{
						       txtDeductibleCAB91.value="500";
						       txtDeductibleCAB91.style.display = "inline";
						       lblDeductibleCAB91.style.display="none";
     					}
     					else
     					{
     					        txtDeductibleCAB91.value="";
						        txtDeductibleCAB91.style.display = "none";
						        lblDeductibleCAB91.style.display="inline";
     					}
					}
					
				if(firstTime == false)
				{	
					if(covCode =='COLL')
					{
					
						if(chk.checked==true)
						{
						  if(document.getElementById('hidUseVehicle').value=="11332")
						  {
								var DeductibleCOLL  = GetControlInGridFromCode('COLL','_ddlDEDUCTIBLE');
								var vehAmount=parseInt(document.getElementById('hidVehicleAmount').value);
								if (document.getElementById('hidVehicleType').value=='11336' && vehAmount <= 50000)
								{
								var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,608)
								if(intRet == 2)
								var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,73)
								}
								else
								{
								var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,607)
								if(intRet == 2)
								var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,72)
								}
						  }
						  else
						  {
						        var DeductibleCOLL  = GetControlInGridFromCode('COLL','_ddlDEDUCTIBLE');
								var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,606)
								if(intRet == 2)
								var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,71)
						  }
						}
					}
				}
				
				
				
				//Single Limit Liability CSL 
				// Default-500 
				 
				if(firstTime == false)
				{	
				   if(covCode =='SLL')
					{
					
						if(chk.checked==true)
						{
						
								var DeductibleCOLL  = GetControlInGridFromCode('SLL','_ddlDEDUCTIBLE');
								if(DeductibleCOLL)
								{
								var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,1)
								if(intRet == 2)
								var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,131)
								}
						}
					}
				}
				
				//Uninsured Motorist PD  
				// Default-300 
				 
				if(firstTime == false)
				{	
				   if(covCode =='UMPD')
					{
					
						if(chk.checked==true)
						{
						
								var DeductibleCOLL  = GetControlInGridFromCode('UMPD','_ddlDEDUCTIBLE');
								SelectDropdownOptionByText(DeductibleCOLL,300)
						}
					}
				}
				//Medical Payments  PD  
				// Default-500 
				 
				if(firstTime == false)
				{	
				   if(covCode =='MP')
					{
					
						if(chk.checked==true)
						{
								var DeductibleCOLL  = GetControlInGridFromCode('MP','_ddlLIMIT');
								var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,15)
						}
					}
				}
				//Uninsured Motorist PD
				if(firstTime == false)
				{	
				   if(covCode =='UMPD')
					{
					
						if(chk.checked==true)
						{
								var DeductibleCOLL  = GetControlInGridFromCode('UMPD','_ddlLIMIT');
								var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,64)
						}
					}
					if(covCode =='EECOMP')
					{
						if(chk.checked==true)
						{
								var DeductibleCOLL  = GetControlInGridFromCode('EECOMP','_ddlDEDUCTIBLE');
								var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,660)
						}
					}
					if(covCode =='EECOLL')
					{
						if(chk.checked==true)
						{
								var DeductibleCOLL  = GetControlInGridFromCode('EECOLL','_ddlDEDUCTIBLE');
								var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,672)
								var  txtDeductibleEECOMP= GetControlInGridFromCode('EECOMP','_txtLIMIT');
								onLimitChange(txtDeductibleEECOMP,'EECOMP');
								
						}
					}
				}
				//Other Than Collision (Comprehensive) 
				//FOR INDIAN DEFAULT PERSONAL-250 
				//FOR INDIAN DEFAULT COMMERCIAL-100 
				if(firstTime == false)
				{	
					if(covCode =='COMP')
					{
					
						if(chk.checked==true)
						{
						  if(document.getElementById('hidUseVehicle').value=="11332")
						  {
								var DeductibleCOLL  = GetControlInGridFromCode('COMP','_ddlDEDUCTIBLE');
								var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,165)

						  }
						  else
						  {
						        var DeductibleCOLL  = GetControlInGridFromCode('COMP','_ddlDEDUCTIBLE');
								//var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,162) 
								var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,165)//iTRACK 4893
								
						  }
						}
					}
				}
				//Other Than Collision (Comprehensive) 
				//FOR INDIAN DEFAULT PERSONAL-250 
				//FOR INDIAN DEFAULT COMMERCIAL-100 
				if(firstTime == false)
				{	
					if(covCode =='OTC')
					{
						if(chk.checked==true)
						{
						  if(document.getElementById('hidUseVehicle').value=="11332")
						  {
								var DeductibleCOLL  = GetControlInGridFromCode('OTC','_ddlDEDUCTIBLE');
								var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,82)

						  }
						  else
						  {
						        var DeductibleCOLL  = GetControlInGridFromCode('OTC','_ddlDEDUCTIBLE');
								var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,79)
								
						  }
						}
					}
				}

				//Other Than Collision (Comprehensive) 
				//Medical Payments 
				
				if(firstTime == false)
				{	
					if(covCode =='MP')
					{
					
						if(chk.checked==true)
						{
						  if(document.getElementById('hidUseVehicle').value=="11332")
						  {
								var DeductibleCOLL  = GetControlInGridFromCode('MP','_ddlLIMIT');
								var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,15)
						  }		

						}
					}
				}
					
			if ( covCode == 'OTC' ||  covCode == 'COMP' || covCode == 'COLL')
				{	
					if ( chk.checked == true )
					{
					    ddl = GetControlInGridFromCode(covCode,'_ddlDEDUCTIBLE');
						OnDDLChange(ddl);	
					}
				}	
					//*********************Auto*****************************
				if ( lobState == 'PPAMichigan' || lobState == 'PPAIndiana')
				{
					if(firstTime ==false)
					{
						if(covCode == 'PUNCS')
						{
							if(chk.checked==true)
							{
							   flagminvalue=false;
								var DeductibleCOLL  = GetControlInGridFromCode('SLL','_ddlLIMIT');
								if(DeductibleCOLL != null)
           						{
									OnDDLChange(DeductibleCOLL)
									flagA9=false;
									flag17=true;
									
								}
							  SetRejRedUmCoverage();	
							}
						}
						
						if(covCode == 'UMPD')
						{
							if(chk.checked==true)
							{
							   flagminvalue=false;
								var DeductibleCOLL  = GetControlInGridFromCode('PD','_ddlLIMIT');
								if(DeductibleCOLL != null)
           						{
									OnDDLChange(DeductibleCOLL)
									flagUMPB=false;
								}
							  SetRejRedUmCoverage();	
							}
						}
						//Start 'UNCSL'
						if(covCode == 'UNCSL')
						{
							if(chk.checked==true)
							{
								var DeductibleSign  = GetControlInGridFromCode('UNCSL','_ddlSignatureObtained');
								if(DeductibleSign != null)
								{
									if ( DeductibleSign.options[DeductibleSign.selectedIndex].text == 'No' )
									{
										flagUNSCL=false;
									}
									else
									{
										flagUNSCL=true;
									} 
								}
								var DeductibleCOLL  = GetControlInGridFromCode('PUNCS','_ddlLIMIT'); // added on 4 sep 08 Itrack 4496
								if(DeductibleCOLL != null)
									OnDDLChange(DeductibleCOLL);
								SetRejRedUmCoverage();
							}
							else
							{
							 flagUNSCL =false;
							 SetRejRedUmCoverage();
							}
						}
						if(covCode == 'PUMSP')
						{
							if(chk.checked==true)
							{
              				   flagminvalue=false;
								var DeductibleCOLL  = GetControlInGridFromCode('BISPL','_ddlLIMIT');
								if(DeductibleCOLL != null)
           						{
									OnDDLChange(DeductibleCOLL)
									//flagA9=false;
									flagUNSCL =false;
								}
							  SetRejRedUmCoverage();	
							}
							else
							{
							flagA9=false;
							flagUNSCL =false;
							flagUNDSP =false;
							SetRejRedUmCoverage();					
							}		
						}
						if(covCode == 'UNDSP')
						{
							if(chk.checked==true)
							{
								var DeductibleSign  = GetControlInGridFromCode('UNDSP','_ddlSignatureObtained');
								if(DeductibleSign != null)
								{
									DeductibleSign.selectedIndex=0;
									if ( DeductibleSign.options[DeductibleSign.selectedIndex].text == 'No' )
									{
										flagUNSCL=false;
										 flagUNDSP =false
									}
									else
									{
										flagUNSCL=true;
										flagUNDSP =true
									} 
								}
								var DeductibleCOLL  = GetControlInGridFromCode('PUMSP','_ddlLIMIT'); // added on 4 sep 08 Itrack 4496
								if(DeductibleCOLL != null)
									OnDDLChange(DeductibleCOLL);
								SetRejRedUmCoverage();
							}
							else
							{
							 flagUNSCL =false;
							 flagUNDSP =false;
							 SetRejRedUmCoverage();
							}
						}
						
						
						//Start 'UNCSL'
						EnableDisbaleSignDDlUMPD();
					}		//End of first
				}		//End Of PPa	
				//////////////////////////
				
					//*********************MOTORCYCLE*****************************
				if ( lobState == 'MOTMichigan' || lobState == 'MOTIndiana')
				{
					
					if(lobState == 'MOTIndiana' && covCode == 'UNCSL')
					{
						var DeductibleSign  = GetControlInGridFromCode('UNCSL','_ddlSignatureObtained');
						if(chk.checked==true)
							{
								if(DeductibleSign != null)
								{
									if ( DeductibleSign.options[DeductibleSign.selectedIndex].text == 'No' )
									{
										flagUNCSLM16=false;
									}
									else
									{
										flagUNCSLM16=true;
									} 
								}
							 var ddl  = GetControlInGridFromCode('UNCSL','_ddlLIMIT'); 
						     if(ddl != null)
							   OnDDLChange(ddl);	
							 SetRejRedUmCoverageMotor();
							}
							else
							{
							if(DeductibleSign != null )
									DeductibleSign.selectedIndex=0;
							 flagUNCSLM16 =false;
							 SetRejRedUmCoverageMotor();
							}
					}
					//Part E - Other Than Collision (Comprehensive) 
					if ( covCode == 'COLL')
					{
						//var cbRoadService = GetCheckBoxFromCode('ROAD');
						var cbRoadService = GetControlInGridFromCode('ROAD','_cbDelete');
						
						if ( chk.checked == true )
						{
						 
							if ( cbRoadService != null )
							{
								//If Motorcycle type is Gold Wing or Tour Bike, make Road Service mandatory
								if ( document.getElementById('hidMotorcycleType').value == '11423' ||  document.getElementById('hidMotorcycleType').value == '11425' )
								{
									//EnableDisableRow(cbRoadService.parentElement.parentElement.parentElement, true);
									cbRoadService.checked = true;
									
									
									cbRoadService.parentElement.parentElement.parentElement.disabled =  false;
									cbRoadService.disabled = true;
									DisableControls(cbRoadService.id);
									SetHiddenField(cbRoadService.id);
									EnableDisableRow(cbRoadService.parentElement.parentElement.parentElement, true);
								}
								else
								{
									
									//cbRoadService.checked = false;
									//EnableDisableRow(cbRoadService.parentElement.parentElement.parentElement, false);
									cbRoadService.parentElement.parentElement.parentElement.disabled =  false;
									cbRoadService.disabled = false;
									DisableControls(cbRoadService.id);
									SetHiddenField(cbRoadService.id);
								}
								
							}
							
						}
						else	//checked == false
						{
							if ( cbRoadService != null )
							{
								cbRoadService.disabled = true;
								cbRoadService.checked = false;
								
								cbRoadService.parentElement.parentElement.parentElement.disabled =  true;
								DisableControls(cbRoadService.id);
								SetHiddenField(cbRoadService.id);
							}
								
						}
					
				}//End Of Part E - Other Than Collision (Comprehensive)
				
				if(firstTime == false)
				{
				    if(covCode =='MEDPM')
						 {
							if(chk.checked==true)
							{
									var DeductibleCOLL  = GetControlInGridFromCode('MEDPM','_ddlLIMIT');
									var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,882)
									
							}
						}
						
					if(document.getElementById('hidattachUmb').value=='1')
					{
					   //start Single Limits Liability (CSL) 
					   if(covCode =="RLCSL")
					   {
							if(chk.checked==true)
							{
									var DeductibleCOLL  = GetControlInGridFromCode('RLCSL','_ddlLIMIT');
									var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,379)
									if(intRet==2)
									{
									intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,1077)
									}
							}
							else
							{
									ctrl  = GetControlInGridFromCode('BISPL','_cbDelete');
									if(ctrl && ctrl.checked==true)
									{
										var DeductibleCOLL  = GetControlInGridFromCode('BISPL','_ddlLIMIT');
										var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,384)
										if(intRet==2)
										{
											intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,1381)
										}
									}
							}
						    
						 }   
					  //end Single Limits Liability (CSL) 
								  
					  //start Bodily Injury Liability (Split Limit) 
                      	
                    if(covCode =="BISPL")
					{					  
						
						if(chk.checked==true)
						{
							var DeductibleCOLL  = GetControlInGridFromCode('BISPL','_ddlLIMIT');
							var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,384)
							if(intRet==2)
							{
							intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,1381)
							}
						}
						else
						{
							ctrl  = GetControlInGridFromCode('RLCSL','_cbDelete');
							if(ctrl && ctrl.checked==true)
							{
								var DeductibleCOLL  = GetControlInGridFromCode('RLCSL','_ddlLIMIT');
								var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,379)
								if(intRet==2)
								{
									intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,1077)
								}
						    }
						    flagUNDSP==false;	
				        }	
					 }  //end Bodily Injury Liability (Split Limit) 
					}
					else
					{
					  //start Single Limits Liability (CSL) 
					   if(covCode =="RLCSL")
					   {
							if(chk.checked==true)
							{
									var DeductibleCOLL  = GetControlInGridFromCode('RLCSL','_ddlLIMIT');
									var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,377)
									if(intRet==2)
									{
										intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,171)
									}
							}
							else
							{
									ctrl  = GetControlInGridFromCode('BISPL','_cbDelete');
									if(ctrl && ctrl.checked==true)
									{
										var DeductibleCOLL  = GetControlInGridFromCode('BISPL','_ddlLIMIT');
										var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,381)
										if(intRet==2)
										{
											intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,177)
										}
									}
							}
						}	
					    //end Single Limits Liability (CSL) 	
					     //start Bodily Injury Liability (Split Limit) 
					   if(covCode =="BISPL")
					   { 
					     	if(chk.checked==true)
							{
									var DeductibleCOLL  = GetControlInGridFromCode('BISPL','_ddlLIMIT');
									var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,381)
									if(intRet==2)
									{
										intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,177)
									}
							}
							else
							{
							        ctrl  = GetControlInGridFromCode('BISPL','_cbDelete');
									if(ctrl && ctrl.checked==true)
									{
										var DeductibleCOLL  = GetControlInGridFromCode('RLCSL','_ddlLIMIT');
										var intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,377)
										if(intRet==2)
										{
											intRet=SelectDropdownOptionByValueWithReturn(DeductibleCOLL,171)
										}
									}
								flag17UMPD=false;
							}
						}	
					  //end Bodily Injury Liability (Split Limit) 
					
					}
				}
				
				//Update Uninsured Motorists (CSL) Based On Single Limits Liability (CSL) 
				if(firstTime ==false)
				{
					if(covCode == 'PUNCS')
					{
						if(chk.checked==true)
						{
						   
							var DeductibleCOLL  = GetControlInGridFromCode('RLCSL','_ddlLIMIT');
							if(DeductibleCOLL != null)
           					{
								OnDDLChange(DeductibleCOLL)

								flagA9=false;
								flag17=true;
								SetRejRedUmCoverageMotor17();
							}
						}
						else
						{
								flagA9=false;
								SetRejRedUmCoverageMotor();
						}
					}	
					
					//Uninsured Motorists (PD)
					
					if(covCode == 'UMPD')
					{
						if(chk.checked==true)
						{
							    var DeductibleSign  = GetControlInGridFromCode('UMPD','_ddlSignatureObtained');
							    var ddlLIMIT  = GetControlInGridFromCode('UMPD','_ddlLIMIT');
								if(DeductibleSign != null)
								{
									if ( DeductibleSign.options[DeductibleSign.selectedIndex].text == 'No' )
									{
										flagUMPD=false;
										if ( ddlLIMIT.options[ddlLIMIT.selectedIndex].text == 'Reject' )
										{
											flag17UMPD=false;
										}
										else
										{
										   flag17UMPD=true;
										}
									}
									else
									{
										flagUMPD=true;
										flag17UMPD=false;
									} 
								}
						}
						else
						{
						    flagUMPD=false;
						    flag17UMPD=false;
						}
						SetRejRedUmCoverageMotor();
						SetRejRedUmCoverageMotor17();
					}	
					
					//Uninsured Motorists (BI Split Limit)
					if(covCode == 'PUMSP')
					{
						if(chk.checked==true)
						{
						   
							var DeductibleCOLL  = GetControlInGridFromCode('BISPL','_ddlLIMIT');
							if(DeductibleCOLL != null)
           					{
								OnDDLChange(DeductibleCOLL)

								flagA9=false;
								SetRejRedUmCoverageMotor();
								flag17=true;
								SetRejRedUmCoverageMotor17();
							}
						}
						else
						{
							flagA9=false;
							SetRejRedUmCoverageMotor();
							flag17=false;
							SetRejRedUmCoverageMotor17();
						}
					}			
			     }
			     
			     if(covCode =="EBM49")
			     {
			        
					var DLLOTC  = GetControlInGridFromCode('OTC','_ddlDEDUCTIBLE');
					if(DLLOTC != null)
           			{
						OnDDLChange(DLLOTC)
					}
			     }
			     if(covCode =="CEBM49")
			     {
					var DLLOTC  = GetControlInGridFromCode('COLL','_ddlDEDUCTIBLE');
					
					if(DLLOTC != null)
           			{
						OnDDLChange(DLLOTC)
					}
			     }

			     
				//Other Than Collision (Comprehensive) 
					if ( covCode == 'OTC')
					{
						//Call Set Hidden field for Helmet, getting checked from XML file
						var cbEBM15  = GetControlInGridFromCode('EBM15','_cbDelete');
						
						SetHiddenField(cbEBM15.id);
						
					}
					
				
					
			}	//End of Mot state check
			//***************
			return;
			}
			//***********************************************************************
			
			
			
			//Return the Signature obtained Label for the coverage
			function GetSigObtLabel(ddl)
			{	
				var lastIndex = ddl.id.lastIndexOf('_');
				var prefix = ddl.id.substring(0,lastIndex);
				
				var strSigDDL = prefix + '_lblSigObt';
				 
				 
				var lblSigDDL = document.getElementById(strSigDDL);				
				
				return lblSigDDL;
			}
			//Returnd the Signature obtained DDL for the coverage
			function GethiddSigObtLabel(ddl)
			{	
				var lastIndex = ddl.id.lastIndexOf('_');
				var prefix = ddl.id.substring(0,lastIndex);
				
				var strSigDDL = prefix + '_hiddSigObt';
				var hiddSigDDL = document.getElementById(strSigDDL);				
				return hiddSigDDL;
			}
			//Returnd the Signature obtained DDL for the coverage
			function GetSigDDL(ddl)
			{
			
				var lastIndex = ddl.id.lastIndexOf('_');
				var prefix = ddl.id.substring(0,lastIndex);
				
				var strSigDDL = prefix + '_ddlSignatureObtained';
				
				//alert( strSigDDL);
				 
				var SigDDL = document.getElementById(strSigDDL);				
				//alert( SigDDL);
				return SigDDL;
			}
			
			/////////////Runs When Option in signature DDL changed
			function OnSigDDLChange(ddl)
			{
				var COV_CODE = '';
				if ( ddl == null ) return;
				if ( ddl.getAttribute('COV_CODE') == null ) return;
				COV_CODE = ddl.getAttribute('COV_CODE');
				//Medical Payments
				var lobState = document.getElementById('hidLOBState').value;
				if ( COV_CODE == 'MEDPM1' )
				{
				 	var cbCTRL = GetControlInGridFromCode('MEDPM2','_cbDelete');
					var limitDDL = GetControlInGridFromCode('MEDPM1','_ddlLIMIT');;
					//alert(limitDDL);	 
					
					if ( ddl.options[ddl.selectedIndex].text == 'Yes' )
					{
							if ( cbCTRL != null )
							{
								if(limitDDL.options[limitDDL.selectedIndex].text == 'Reject')
								{
									//cbCTRL.checked=true;
									EnableDisableRow(cbCTRL.parentElement.parentElement.parentElement, false);
									cbCTRL.disabled=false;
									DisableControls(cbCTRL.id);
									SetHiddenField(cbCTRL.id);
								}
							}
							
					}
					else
					{
							if ( cbCTRL != null )
							{	
								cbCTRL.checked=false;
								EnableDisableRow(cbCTRL.parentElement.parentElement.parentElement, true);
								cbCTRL.disabled=true;
								DisableControls(cbCTRL.id);
								SetHiddenField(cbCTRL.id);
							}
							
					}
				}
				//Excluded Person(s) Endorsement A-96 if User Opt No In Signiture Obtained
				// Then Uncheck the Coverage
				if ( COV_CODE == 'EP95' )
				{
				    var cbCTRL = GetControlInGridFromCode('EP95','_cbDelete');
				    var lblLimitCAB91       = GetControlInGridFromCode('EP95','_lblLIMIT');
			        if ( ddl.options[ddl.selectedIndex].text == 'No' )
					{
   					
   					      if ( cbCTRL != null )
							{	
								cbCTRL.checked=false;
								DisableControls(cbCTRL.id);
								SetHiddenField(cbCTRL.id);
								//lblLimitCAB91.innerText='No Coverages'
							}
					}
				}	
				if (lobState == 'MOTIndiana')
				{
				  if(COV_CODE =='UNCSL' )
				  {
				     if ( ddl.options[ddl.selectedIndex].text == 'No' )
					 {
					    //flagA9=false;
					    flagUNCSLM16=false;
					 }
					else
					 {
					   // flagA9=true;
					    flagUNCSLM16=true;
					 } 
					 SetRejRedUmCoverageMotor();
					 return;
				  }
				}
				//*********************MOTORCYCLE*****************************
				if ( lobState == 'MOTMichigan' || lobState == 'MOTIndiana' || lobState == 'PPAIndiana')
				{
				  if(COV_CODE =='PUNCS' || COV_CODE == "PUMSP" )
				  {
				     if ( ddl.options[ddl.selectedIndex].text == 'No' )
					 {
					    flagA9=false;
					    flagUNSCL=false;
					 }
					else
					 {
					    flagA9=true;
					    if (COV_CODE =='PUNCS') flagUNSCL=true;
					 } 
					 SetRejRedUmCoverageMotor();
					 SetRejRedUmCoverage();
					 
					 flag17=false;
				  }
				  if(COV_CODE =='UNCSL' )
				  {
				    
				     if ( ddl.options[ddl.selectedIndex].text == 'No' )
					 {
					   
					    flagUNSCL=false;
					 }
					else
					 {
					   
					    flagUNSCL=true;
					 } 
					 SetRejRedUmCoverage();
				  }
				 
				 if(COV_CODE =='UNDSP' )
				  {
				    
				     if ( ddl.options[ddl.selectedIndex].text == 'No' )
					 {
					    flagUNSCL=false;
					    flagUNDSP=false;
					     //flagA9=false;
					 }
					else
					 {
					   
					    //flagUNSCL=true;
					    flagUNDSP=true; 
					 } 
					 SetRejRedUmCoverage();
				  }
				  if(COV_CODE =='UMPD' )
				  {
				    
				     if ( ddl.options[ddl.selectedIndex].text == 'No' )
					 {
					    flagUMPB=false;
					    flagUMPD=false
					      var ddlLIMIT  = GetControlInGridFromCode('UMPD','_ddlLIMIT');
						  if(ddlLIMIT != null)
						   {
								if ( ddlLIMIT.options[ddlLIMIT.selectedIndex].text == 'Reject' )
								{
									flag17UMPD=false;
								}
								else
								{
									flag17UMPD=true;
								}
						  }
					 }
					else
					 {
					    flagUMPB=true;
					    flagUMPD=true;
					    flag17UMPD=false;
					 } 
					 SetRejRedUmCoverage();
					 SetRejRedUmCoverageMotor();
					 SetRejRedUmCoverageMotor17();
				  }
				}
				
								
				
			}
			/*
			Either "PIP" is selected as  
			Excess Medical or Excess Medical/Wage 
			Excess Medical & Full Wage Loss 
			Excess Medical & Excess Wage Loss &/Or Work Comp 
			OR /AND then display "Health Care Primary Carrier "
			If "Extended Non-Owned Coverage for Named Individual (A-35) " is selected ,
			display  "# of Persons"
			*/
			function EnableDisableAddInfor()
			{
				var flag=false;
				var flagpip=false;
				
				
				
				var lblDeductibleCAB91  = GetControlInGridFromCode('CAB91','_lblDEDUCTIBLE');
				var cbDeletePIP     = GetControlInGridFromCode('PIP','_cbDelete');
				var ddl     = GetControlInGridFromCode('PIP','_ddlLimit');
				
				if(cbDeletePIP != null)
				{
					if(cbDeletePIP.checked == true)
					{	
					    
						if(trim(ddl.options[ddl.selectedIndex].value) != '1372' && trim(ddl.options[ddl.selectedIndex].value) != '686') //ITRACK 4524
						{
						  flagpip =true       //DisableControls(cbDeleteCAB91.id);
    					}
    				}
    			}	
    			
    			if(document.getElementById('hidUseVehicle').value =='11333')
    			{
    				if (ddl!=null && trim(ddl.options[ddl.selectedIndex].value) != '1374' && trim(ddl.options[ddl.selectedIndex].value) != '1375') //ITRACK 4524
    				{
    				flag =false   
    				flagpip =false  
    				}
    			}
    			
    			document.getElementById('hidPipValue').value=flagpip
				
				if( flagpip==true)
				{
					document.getElementById('trAddInformation').style.display ="inline"
					document.getElementById('trHealthCare').style.display ="inline"
					document.getElementById('rfvHealthCare').setAttribute("enabled",true);
					flag =true
				}
				else
				{
					document.getElementById('rfvHealthCare').setAttribute("enabled",false);
					document.getElementById('trHealthCare').style.display ="none"
				}
				
				var cbDeleteENO     = GetControlInGridFromCode('ENO','_cbDelete');
				if(cbDeleteENO != null)
				{
					if(cbDeleteENO.checked == true)
					{
						ApplyColor();
						document.getElementById('trAddInformation').style.display ="inline"
						document.getElementById('trNoPersons').style.display ="inline"
						document.getElementById('rfvNoPersons').setAttribute("enabled",true);
						flag =true
						ChangeColor();
					}
					else
					{
						document.getElementById('rfvNoPersons').setAttribute("enabled",false);
						document.getElementById('trNoPersons').style.display ="none"
					}
					cbDeleteENO.disabled=true;
			    }
			    else
			    {
						document.getElementById('rfvNoPersons').setAttribute("enabled",false);
						document.getElementById('trNoPersons').style.display ="none"
			    }
			    
			    if(document.getElementById('hidUseVehicle').value =='11333')
    			{
    				if(ddl!=null && trim(ddl.options[ddl.selectedIndex].value) != '1374' && trim(ddl.options[ddl.selectedIndex].value) != '1375') //ITRACK 4524
    				{
    				flag =false   
    				flagpip =false  
    				}
    			}
			    if(flag == false)
			    {
			     document.getElementById('trAddInformation').style.display ="none"
			      
			    }
			}
			
			
			/////////////////////////////////
			//Runs when the Limit options change********************************
			function OnDDLChange(ddl)
			{
			   // debugger;
				var COV_CODE = '';
			
				if ( ddl == null ) return;
				
				if ( ddl.getAttribute('COV_CODE') == null ) return;
				
				COV_CODE = ddl.getAttribute('COV_CODE');
				
			
				var lobState = document.getElementById('hidLOBState').value;
				//added by Pravesh itrack# 4432 as Mis Equip deductible same as Comprehensive /collision
				if(COV_CODE == 'EECOLL')
				{
				 var DeductibleCOLL  = GetControlInGridFromCode('COLL','_ddlDEDUCTIBLE');
        		 if(DeductibleCOLL!=null)
					 SelectDropdownOptionByText(ddl,DeductibleCOLL.options[DeductibleCOLL.selectedIndex].text);
				}
				if(COV_CODE == 'EECOMP')
				{
				 var DeductibleCOMP  = GetControlInGridFromCode('COMP','_ddlDEDUCTIBLE');
				 if (DeductibleCOMP==null)
				  DeductibleCOMP = GetControlInGridFromCode('OTC','_ddlDEDUCTIBLE');
        		 if(DeductibleCOMP!=null)
					 SelectDropdownOptionByText(ddl,DeductibleCOMP.options[DeductibleCOMP.selectedIndex].text);
				}
				// end here		
				//In case of PIP  to Make Deductible 300 if Limit is other than "Primary"
				if(COV_CODE == 'PIP')
				{
					//alert(ddl.options[ddl.selectedIndex].value);
					//Set Deductible to 300 if Limit is other than primary
					
					var txtDeductible = GetControlInGridFromCode('PIP','_txtDEDUCTIBLE');
					var lblDeductible= GetControlInGridFromCode('PIP','_lblDEDUCTIBLE');
					
					var lblLimitCAB91       = GetControlInGridFromCode('CAB91','_lblLIMIT');
					var txtDeductibleCAB91  = GetControlInGridFromCode('CAB91','_txtDEDUCTIBLE');
					var lblDeductibleCAB91  = GetControlInGridFromCode('CAB91','_lblDEDUCTIBLE');
					var cbDeleteCAB91       = GetControlInGridFromCode('CAB91','_cbDelete');
					EnableDisableAddInfor();

					if(trim(ddl.options[ddl.selectedIndex].value) == '1372') 
					{
						txtDeductible.value="";
						txtDeductible.style.display = "none";
						lblDeductible.style.display="inline";
						if(cbDeleteCAB91!= null)
						{
						        cbDeleteCAB91.checked=false;
						        lblLimitCAB91.style.display = "inline";
						        lblLimitCAB91.innerText='No Coverages'
								SetHiddenField(cbDeleteCAB91.id);
								txtDeductibleCAB91.value="";
						        txtDeductibleCAB91.style.display = "none";
						        lblDeductibleCAB91.style.display="inline";
						       
						        //DisableControls(cbDeleteCAB91.id);
    					}
					} 
					else
					{  
						txtDeductible.value="300";
						txtDeductible.style.display = "inline";						
						lblDeductible.style.display="none";
						EnableDisableAddInfor('PIP',true);
						if(cbDeleteCAB91!= null)
						{
						        cbDeleteCAB91.checked=true;
								SetHiddenField(cbDeleteCAB91.id);
								//DisableControls(cbDeleteCAB91.id);
								txtDeductibleCAB91.value="300";
						        txtDeductibleCAB91.style.display = "inline";
						        lblDeductibleCAB91.style.display="none";
						        lblLimitCAB91.style.display = "inline";
						        lblLimitCAB91.innerText='Included'
						}
						
						
					}
				}
				
				//Part A - Residual Liability (CSL) 
				if ( COV_CODE == 'RLCSL' || COV_CODE == 'SLL')
				{
					//Part D - Underinsured Motorists (CSL) 
					//var targetDDL = GetPolicyLimitDDLCode('PUNCS');
					var targetDDL = GetControlInGridFromCode('PUNCS','_ddlLimit');
					//ReplaceAll(inclAmount,',','');
					
					//alert(targetDDL);
					if ( targetDDL == null ) return;
										
					SelectComboOptionByText(targetDDL.id,ddl.options[ddl.selectedIndex].text);
					OnDDLChange(targetDDL);
					//added by pravesh on 9sep08 Itrack 4313 sheet and for PPA Itrack 4776
					if (lobState=='MOTIndiana' || lobState=='PPAIndiana')
					{
					var UNCSLddl = GetControlInGridFromCode('UNCSL','_ddlLimit');
					if (UNCSLddl!=null && firstTime==false)
					  SelectComboOptionByText(UNCSLddl.id,ddl.options[ddl.selectedIndex].text);
					}
					//var tb = GetPolicyLimitTextBoxFromCode('UNCSL');
					var tb = GetControlInGridFromCode('UNCSL','_txtLIMIT');
					
					if ( tb != null )
					{
						//var amount = ReplaceAll(ddl.options[targetDDL.selectedIndex].value,',','');
						//alert(targetDDL.options[targetDDL.selectedIndex].text);
						tb.value = targetDDL.options[targetDDL.selectedIndex].text;	 
					}
					
					//Make invisible the Sig Obt DDL
					var sigDDL = GetSigDDL(targetDDL);
					//var hiddSignObt= GetControlInGridFromCode('UNCSL','_hiddSignObt');
					var hiddSignObt= GethiddSigObtLabel(targetDDL); 
					if ( sigDDL != null)
					{
						sigDDL.style.display = "none";
						if (hiddSignObt != null) 
                          {
                          hiddSignObt.value ="0";  
                          } 
						flagA9=false;
						flagminvalue =false;
						flag17=true;
						SetRejRedUmCoverageMotor17();
						SetRejRedUmCoverageMotor();
						SetRejRedUmCoverage();
					}
					
				}
				
				
				//Part C - Uninsured Motorists (CSL) 
				if ( COV_CODE == 'PUNCS')
				{
				   
				   	flagA9=false;
				   	flag17=false;
				   	flagminvalue =false;
					//Part D - Underinsured Motorists (CSL) 
					//var targetDDL = GetPolicyLimitDDLCode('UNCSL');
					var targetDDL = GetControlInGridFromCode('UNCSL','_ddlLimit');
					
					//alert(targetDDL);
					//if ( targetDDL == null ) return;
					
					if ( targetDDL != null  && firstTime==false)
					{				
						SelectComboOptionByText(targetDDL.id,ddl.options[ddl.selectedIndex].text);
						OnDDLChange(targetDDL);
					}
					//******  Reduced limits code  *********  Added By Praveen ********** 
					var SigDDL = GetSigDDL(ddl);
					var lblSigDDL = GetSigObtLabel(ddl);
					//var hiddSignObt=GetControlInGridFromCode('UNCSL','_hiddSignObt'); 
					var hiddSignObt= GethiddSigObtLabel(ddl);
					//var sllDDL = GetPolicyLimitDDLCode('SLL');
					var sllDDL = GetControlInGridFromCode('SLL','_ddlLimit');
					//var rlcslDDL =  GetPolicyLimitDDLCode('RLCSL');
					var rlcslDDL = GetControlInGridFromCode('RLCSL','_ddlLimit');
					var sllAmt;
					var formattedsllAmt;	 
					//Get the currently selected amount in Bodily Injury Liability ( Split Limit) 
					if ( sllDDL != null ) 
					{
						sllAmt = ReplaceAll(sllDDL.options[sllDDL.selectedIndex].text,',','');
						formattedsllAmt = sllDDL.options[sllDDL.selectedIndex].text;
					}
					
					if ( rlcslDDL != null ) 
					{
						sllAmt = ReplaceAll(rlcslDDL.options[rlcslDDL.selectedIndex].text,',','');
						formattedsllAmt = rlcslDDL.options[rlcslDDL.selectedIndex].text;
					}
					
					 
					var currentAmt = ReplaceAll(ddl.options[ddl.selectedIndex].text,',','');
					
					//alert(currentAmt);
					//Rejection/////////////
					
					if ( trim(currentAmt) == 'Reject')
					{
							//Itrack 4776 added on 22 sep 08
							if(document.getElementById('hidLOBState').value=='PPAIndiana')
							{
								var cbUNCSL = GetControlInGridFromCode('UNCSL','_cbDelete');
								var cbUMPD = GetControlInGridFromCode('UMPD','_cbDelete');
								if (cbUNCSL!=null  )
								{
									cbUNCSL.checked=false;
									cbUNCSL.disabled=true;
									DisableControls(cbUNCSL.id);
									SetHiddenField(cbUNCSL.id)
									flagUNSCL =false;
					    		}
					    		if (cbUMPD!=null )
								{
									cbUMPD.checked=false;
									cbUMPD.disabled=true;
									DisableControls(cbUMPD.id);
									SetHiddenField(cbUMPD.id)
									flagUMPB=false;
					    		}
					    	}
							//end here Itrack 4776
							//alert(currentAmt);
							flagA9=false;
							if ( SigDDL != null )
							{
								SigDDL.style.display = "inline";
								if ( hiddSignObt != null ) 
								{
								    hiddSignObt.value="1";
								 }   

								flagA9=true;
							}
							if(SigDDL !=null)
							{
								if ( SigDDL.options[SigDDL.selectedIndex].text == 'No' )
								{
									flagA9=false;
									
								}
								else
								{
									flagA9=true;
									
								} 
							}
							
							
							if ( lblSigDDL != null )
							{
								lblSigDDL.style.display = "none";
								if ( hiddSignObt != null ) 
								       hiddSignObt.value="1";
							}
							
							var tb = GetPolicyLimitTextBoxFromCode('UNCSL');
					
							if ( tb != null )
							{ 
								tb.value = ddl.options[ddl.selectedIndex].text;	 
							}	
							flag17=false;
						    flagminvalue =true;
							SetRejRedUmCoverageMotor17();
							SetRejRedUmCoverageMotor();
							
							SetRejRedUmCoverage();
							return;	
					}
					
					//End of rejection////////////////////
					
					if(document.getElementById('hidLOBState').value=='PPAMichigan')
					{
					   /*Commented as per Itrack 4886
					    if ( parseInt(currentAmt) != parseInt(sllAmt))
					    {
					        alert('Amount cannot be greater or less than than CSL');
					      
							//Select amount in CSL here
							SelectComboOptionByText(ddl.id,formattedsllAmt);
							
							if ( SigDDL != null )
							{
						  		SigDDL.style.display = "none";
						  		if ( hiddSignObt != null ) 
										hiddSignObt.value="0";
						  	}
						  	
						  	if ( lblSigDDL != null )
							{
								lblSigDDL.style.display = "inline";
								if ( hiddSignObt != null ) 
										hiddSignObt.value="0";
							}
					      
					    } */
					     // added by Pravesh on 15 oct 08 Itrack 4886
					    if ( parseInt(currentAmt) > parseInt(sllAmt))
						 {
							alert('Amount cannot be greater than CSL');
							//Select amount in CSL here
							SelectComboOptionByText(ddl.id,formattedsllAmt);
							if ( SigDDL != null )
							{
						  		SigDDL.style.display = "none";
						  		if ( hiddSignObt != null ) 
										hiddSignObt.value="0";
						  	}
						  	if ( lblSigDDL != null )
							{
								lblSigDDL.style.display = "inline";
								if ( hiddSignObt != null ) 
										hiddSignObt.value="0";
							}
						 }
						 else if (parseInt(currentAmt) == parseInt(sllAmt))
						 {
							if ( SigDDL != null )
							{
						  		SigDDL.style.display = "none";
						  		if ( hiddSignObt != null ) 
										hiddSignObt.value="0";
						  	}
						  	if ( lblSigDDL != null )
							{
								lblSigDDL.style.display = "inline";
								if ( hiddSignObt != null ) 
										hiddSignObt.value="0";
							}
						 }  
						//End here					
					}
					else
					{
						//Itrack 4776 added on 22 sep 08
						 if(document.getElementById('hidLOBState').value=='PPAIndiana')
							{
								var cbUNCSL = GetControlInGridFromCode('UNCSL','_cbDelete');
								if (cbUNCSL!=null  && firstTime==false)
								{
									cbUNCSL.checked=true;
									cbUNCSL.disabled=false;
									EnableDisableRow(cbUNCSL.parentElement.parentElement.parentElement, false);
									DisableControls(cbUNCSL.id);
									SetHiddenField(cbUNCSL.id)
					    		}
					    		var cbUMPD = GetControlInGridFromCode('UMPD','_cbDelete');
					    		if (cbUMPD!=null  && firstTime==false)
								{
									cbUMPD.checked=true;
									cbUMPD.disabled=false;
									EnableDisableRow(cbUMPD.parentElement.parentElement.parentElement, false);
									DisableControls(cbUMPD.id);
									SetHiddenField(cbUMPD.id)
					    		}
					    		
					    	}
							//end here Itrack 4776
						//Check for lesser amount here		
						if ( parseInt(currentAmt) < parseInt(sllAmt))
						 {
							if ( SigDDL != null )
							{
								SigDDL.style.display = "inline";
								if ( hiddSignObt != null ) 
										hiddSignObt.value="1";
								if(SigDDL !=null)
						    	{
									if ( SigDDL.options[SigDDL.selectedIndex].text == 'No' )
									{
										flagA9=false;
										
									}
									else
									{
										flagA9=true;
										flagminvalue =true;
									}
								}	 
								flag17=false;
								//flagminvalue =true;
							}
							
							if ( lblSigDDL != null )
							{
								lblSigDDL.style.display = "none";
								if ( hiddSignObt != null ) 
										hiddSignObt.value="1";
							}
						 }
						 else if ( parseInt(currentAmt) > parseInt(sllAmt))
						 {
							alert('Amount cannot be greater than CSL');
							flag17=true;
							flagA9 =false;
							flagminvalue =false;
							//Select amount in CSL here
							SelectComboOptionByText(ddl.id,formattedsllAmt);
							var targetDDL = GetControlInGridFromCode('UNCSL','_ddlLimit');
							if ( targetDDL != null )
							{				
								SelectComboOptionByText(targetDDL.id,ddl.options[ddl.selectedIndex].text);
							}
							if ( SigDDL != null )
							{
						  		SigDDL.style.display = "none";
						  		if ( hiddSignObt != null ) 
										hiddSignObt.value="0";
						  	}
						  	
						  	if ( lblSigDDL != null )
							{
								lblSigDDL.style.display = "inline";
								if ( hiddSignObt != null ) 
										hiddSignObt.value="0";
							}
						 }
						 else if (parseInt(currentAmt) == parseInt(sllAmt))
						 {
						    flag17 =true;
						    flagA9 =false;
						    flagminvalue =false;
						    
							if ( SigDDL != null )
							{
						  		SigDDL.style.display = "none";
						  		if ( hiddSignObt != null ) 
										hiddSignObt.value="0";
						  	}
						  	
						  	if ( lblSigDDL != null )
							{
								lblSigDDL.style.display = "inline";
								if ( hiddSignObt != null ) 
										hiddSignObt.value="0";
							}
						 } 
					}	 
					
					//Set the same value for Underinsured	 
					var tb = GetPolicyLimitTextBoxFromCode('UNCSL');
					
					//Update A-9 Coverage
					
					SetRejRedUmCoverageMotor17();
					SetRejRedUmCoverageMotor();
				    SetRejRedUmCoverage();

				   currentAmt = ddl.options[ddl.selectedIndex].text;
				   currentAmt = ReplaceAll(currentAmt,',','');
				   currentAmt=parseInt(currentAmt);
					
					if ( tb != null )
					{ 
						/*if ( lobState == 'MOTIndiana' && currentAmt== 25000)
						{
						   tb.value = '50,000';	 
						}
						else
						{
						  tb.value = ddl.options[ddl.selectedIndex].text;	 
						} */ 
						tb.value = ddl.options[ddl.selectedIndex].text;	 
					}
				 
						 
					//**************************************
					
				}
				if (COV_CODE == 'COLL')	
					setCoverageA68(ddl);
				//Medical Payments
				if ( COV_CODE == 'MEDPM1' )
				{
				 	var strRejected = ddl.options[ddl.selectedIndex].text;
				 	var strLimitID = ddl.options[ddl.selectedIndex].value;
				 	var cbCTRL = GetControlInGridFromCode('MEDPM2','_cbDelete');
					var sigDDL = GetSigDDL(ddl);
					var sigLabel = GetSigObtLabel(ddl);
					//var hiddSignObt=GetControlInGridFromCode('MEDPM2','_hiddSignObt');
					var hiddSignObt=GethiddSigObtLabel(ddl); 
					//for deductible of medical first party	
					var ddlDeducMEDPM1 = GetControlInGridFromCode('MEDPM1','_ddlDEDUCTIBLE');
					//alert(strLimitID);
					if (ddlDeducMEDPM1!=null)
					{
						//applicable deduct Id 
						if (strLimitID=="862" || strLimitID=="863" || strLimitID=="864" ||  strLimitID=="865" || strLimitID=="866" || strLimitID=="867" || strLimitID=="868" || strLimitID=="869" || strLimitID=="870" )
						{
							ddlDeducMEDPM1.selectedIndex=1;
							ddlDeducMEDPM1.style.display="inline";
						}
						else
						{
							ddlDeducMEDPM1.selectedIndex=0
							ddlDeducMEDPM1.style.display="none";
						}
					}
					//end here 
					
					if ( strRejected.trim() == 'Reject' )
					{
							if ( cbCTRL != null )
							{
								if(sigDDL.options[sigDDL.selectedIndex].text == 'Yes')
								{
									//cbCTRL.checked=true;
									EnableDisableRow(cbCTRL.parentElement.parentElement.parentElement, false);
									cbCTRL.disabled=false;
									DisableControls(cbCTRL.id);
									SetHiddenField(cbCTRL.id);
								}
								else
								 OnSigDDLChange(sigDDL);
							}
							if ( sigDDL != null )
							{
								sigDDL.style.display = "inline";
								if ( hiddSignObt != null )
  									hiddSignObt.value="1";	
							}
							
							if ( sigLabel != null )
							{
								sigLabel.style.display = "none";
								if ( hiddSignObt != null )
  									hiddSignObt.value="1";
							}
					}
					else
					{
							if ( cbCTRL != null )
							{	
								cbCTRL.checked=false;
								EnableDisableRow(cbCTRL.parentElement.parentElement.parentElement, true);
								cbCTRL.disabled=true;
								DisableControls(cbCTRL.id);
								SetHiddenField(cbCTRL.id);
							}
							if ( sigDDL != null )
							{
								sigDDL.style.display = "none";
  								if ( hiddSignObt != null ) 
									hiddSignObt.value="0";

							}
							
							if ( sigLabel != null )
							{
								sigLabel.style.display = "inline";
   								if ( hiddSignObt != null ) 
									hiddSignObt.value="0";

							}
					}
				}
								
				
				//Part C - Uninsured Motorists (BI Split Limit) 
				if ( COV_CODE == 'PUMSP')
				{
					//Part C - Underinsured Motorists (BI Split Limit) 
					//var targetDDL = GetPolicyLimitDDLCode('UNDSP');
					flagA9=false;
					flagminvalue =false;
					flag17=false;
					var targetDDL = GetControlInGridFromCode('UNDSP','_ddlLimit');
					
					//if ( targetDDL == null ) return;
					if (ddl.selectedIndex==-1) return;
					var currentSplitAmt = ddl.options[ddl.selectedIndex].text;
					var currentAmt = currentSplitAmt.split('/');
					var lblSigObt = GetSigObtLabel(ddl);		
					var SigDDL = GetSigDDL(ddl);
					//var hiddSignObt = GetControlInGridFromCode('UNDSP','_hiddSignObt');
					var hiddSignObt = GethiddSigObtLabel(ddl);
					if ( currentAmt.length == 2)
					{
						currentAmt[0] = ReplaceAll(currentAmt[0],',','');
						currentAmt[1] = ReplaceAll(currentAmt[1],',','');
					}
					if (firstTime==false && targetDDL!=null)
					{
						if (lobState == 'PPAIndiana' && currentAmt[0] == 25 && currentAmt[1] == 50000)
							SelectComboOptionByText(targetDDL.id,'50 /50,000'); //Itrack 
						else
							SelectComboOptionByText(targetDDL.id,ddl.options[ddl.selectedIndex].text); // added on 4 sep 08 itrack 4496
					}
					var tb = GetPolicyLimitTextBoxFromCode('UNDSP');
					
					var lobState = document.getElementById('hidLOBState').value;
					
					//Rejection////////////////////
					if ( trim(currentAmt[0]) == 'Reject' && trim(currentAmt[0]) == 'Reject' )
					{
							//Itrack 4776 added on 22 sep 08
							if(lobState=='PPAIndiana')
							{
								var cbUNDSP = GetControlInGridFromCode('UNDSP','_cbDelete');
								if (cbUNDSP!=null )
								{
									cbUNDSP.checked=false;
									cbUNDSP.disabled=true;
									DisableControls(cbUNDSP.id);
									SetHiddenField(cbUNDSP.id)
									flagUNDSP=false;
					    		}
					    		var cbUMPD = GetControlInGridFromCode('UMPD','_cbDelete');
								if (cbUMPD!=null )
								{
									cbUMPD.checked=false;
									cbUMPD.disabled=true;
									DisableControls(cbUMPD.id);
									SetHiddenField(cbUMPD.id)
									flagUMPB=false;
					    		}
					    	}
							//end here Itrack 4776
							if ( SigDDL != null )
							{
								SigDDL.style.display = "inline";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="1";
							}
							if ( SigDDL.options[SigDDL.selectedIndex].text == 'No' )
							{
								flagA9=false;
							}
							else
							{
								flagA9=true;
							} 
							if ( lblSigObt != null )
							{
								lblSigObt.style.display = "none";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="1";
							}
							
							if ( tb != null )
							{
								tb.value = ddl.options[ddl.selectedIndex].text;
							}
							flagminvalue=true;
							flag17=false;
							SetRejRedUmCoverage();
							SetRejRedUmCoverageMotor();
							SetRejRedUmCoverageMotor17()
							return;
					}
					else
					{
							//Itrack 4776 added on 22 sep 08
							if(lobState=='PPAIndiana')
							{
							 var cbUNDSP = GetControlInGridFromCode('UNDSP','_cbDelete');
							 var ddlPD = GetControlInGridFromCode('PD','_ddlLIMIT');
								if (cbUNDSP!=null  && firstTime==false)
								{
									cbUNDSP.checked=true;
									cbUNDSP.disabled=false;
									DisableControls(cbUNDSP.id);
									SetHiddenField(cbUNDSP.id)
					    		}
					    		var cbUMPD = GetControlInGridFromCode('UMPD','_cbDelete');
								if (cbUMPD!=null  && firstTime==false)
								{
									cbUMPD.checked=true;
									cbUMPD.disabled=false;
									OnDDLChange(ddlPD);
									DisableControls(cbUMPD.id);
									SetHiddenField(cbUMPD.id)
					    		}					    	 
					    	}
							//end here Itrack 4776
					}
					//////////////////////////////////
					
					
					
					if ( tb != null )
					{
						//For Auto Indiana only: If 25/50,000 then 50/500000/////
						if ( lobState == 'PPAIndiana' || lobState == 'MOTIndiana')
						{
							
							if ( currentAmt[0] == 25 && currentAmt[1] == 50000 )
							{
								tb.value = '50 /50,000';
								flagminvalue=false;
								if(targetDDL!=null)
								SelectComboOptionByText(targetDDL.id,tb.value); //Itrack 
							}
							else
							{
								//Put currently selected amount in UnderInsured Split
								tb.value = ddl.options[ddl.selectedIndex].text;
							}
						}
						else
						{
							tb.value = ddl.options[ddl.selectedIndex].text;
						}
					}
					
				
					
					//var bisplDDL = GetPolicyLimitDDLCode('BISPL');
					var bisplDDL = GetControlInGridFromCode('BISPL','_ddlLimit');
					 
					 //alert(SigDDL);
					 
					//Get the currently selected amount in Bodily Injury Liability ( Split Limit) 
					if ( bisplDDL == null ) return;
					
					//array
					
					var BISPLSplitAmt = bisplDDL.options[bisplDDL.selectedIndex].text;
					var BISPLAmt = BISPLSplitAmt.split('/');
					
					if ( currentAmt.length == 2 && BISPLAmt.length == 2 )
					{
						currentAmt[0] = ReplaceAll(currentAmt[0],',','');
						currentAmt[1] = ReplaceAll(currentAmt[1],',','');
						
						BISPLAmt[0] = ReplaceAll(BISPLAmt[0],',','');
						BISPLAmt[1] = ReplaceAll(BISPLAmt[1],',','');
			
						if ( parseInt(currentAmt[0]) < parseInt(BISPLAmt[0]) 
						   )
						 
						 {
							
							
							if ( SigDDL != null )
							{
								SigDDL.style.display = "inline";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="1";
							}
							if(SigDDL != null)
							{
								if ( SigDDL.options[SigDDL.selectedIndex].text == 'No' )
								{
									flagA9=false;
								}
								else
								{
									flagA9=true;
								}
							}	 
							
							if ( lblSigObt != null )
							{
								lblSigObt.style.display = "none";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="1";
							}
							flagminvalue=true;
							flag17=false;
							SetRejRedUmCoverage();
							SetRejRedUmCoverageMotor();
							SetRejRedUmCoverageMotor17();
							return;
						 }
						 if (
									parseInt(currentAmt[0]) > parseInt(BISPLAmt[0]) ||
									parseInt(currentAmt[1]) > parseInt(BISPLAmt[1])
							)
						 {
							alert('Amount cannot be greater than Split Limit.');
							//Select amount in CSL here
							flagA9=false;
							SelectComboOptionByText(ddl.id,BISPLSplitAmt);
							if(targetDDL!=null)
							{
							   //SelectComboOptionByText(targetDDL.id,ddl.options[ddl.selectedIndex].text); //added on 4 sep 2008 Itrack 4496
							   if (lobState == 'PPAIndiana' && parseInt(BISPLAmt[0]) == 25 && parseInt(BISPLAmt[1]) == 50000)
								SelectComboOptionByText(targetDDL.id,'50 /50,000'); //Itrack 
							   else
								SelectComboOptionByText(targetDDL.id,ddl.options[ddl.selectedIndex].text); //added on 4 sep 2008 Itrack 4496
							}
							if ( tb != null )
							{
								//tb.value = ddl.options[ddl.selectedIndex].text
									var NewcurrentSplitAmt = ddl.options[ddl.selectedIndex].text;
									var NewcurrentAmt = NewcurrentSplitAmt.split('/');
									if ( NewcurrentAmt.length == 2)
									{
										NewcurrentAmt[0] = ReplaceAll(NewcurrentAmt[0],',','');
										NewcurrentAmt[1] = ReplaceAll(NewcurrentAmt[1],',','');
									}
								//For Auto Indiana only: If 25/50,000 then 50/500000/////
									if ( lobState == 'PPAIndiana' || lobState == 'MOTIndiana')
									{
										if ( NewcurrentAmt[0] == 25 && NewcurrentAmt[1] == 50000 )
										{
											tb.value = '50 /50,000';
											flagminvalue=false;
										}
										else
										{
											//Put currently selected amount in UnderInsured Split
											tb.value = ddl.options[ddl.selectedIndex].text;
										}
									}
									else
									{
										tb.value = ddl.options[ddl.selectedIndex].text;
									}
							}
							
							if ( SigDDL != null )
							{
								SigDDL.style.display = "none";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="0";
							}
							
							if ( lblSigObt != null )
							{
								lblSigObt.style.display = "inline";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="0";
							}
							flagminvalue=false;
							flag17=true;
							SetRejRedUmCoverage();
							SetRejRedUmCoverageMotor();
							SetRejRedUmCoverageMotor17();
							return;
						 }
						 
						  if (
									parseInt(currentAmt[0]) == parseInt(BISPLAmt[0]) ||
									parseInt(currentAmt[1]) == parseInt(BISPLAmt[1])
								)
						 {
							
							//Select amount in CSL here
							//SelectComboOption(ddl.id,BISPLSplitAmt);
							
							flagA9 =false;
							flag17=true;
							if ( SigDDL != null )
							{
								SigDDL.style.display = "none";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="0";
							}
							
							if ( lblSigObt != null )
							{
								lblSigObt.style.display = "inline";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="0";
							}
							flagminvalue=false;
							SetRejRedUmCoverage();
							SetRejRedUmCoverageMotor();
							SetRejRedUmCoverageMotor17();
						 }
						 
					}						
				}//End of Part C - Uninsured Motorists (BI Split Limit) 
				
				/////Added on 9 sep 08 Itrack 4313 and Itrack 4776
				if ( COV_CODE == 'UNCSL')
				{
					var lobState = document.getElementById('hidLOBState').value;
					if (lobState == 'MOTIndiana' || lobState == 'PPAIndiana') 
					{
					//flagA9=false;
					if (ddl.selectedIndex==-1) return;
					var currentSplitAmt = ddl.options[ddl.selectedIndex].text;
					var currentAmt = currentSplitAmt.split('/');
					var lblSigObt = GetSigObtLabel(ddl);		
					var SigDDL = GetSigDDL(ddl);
					var hiddSignObt = GethiddSigObtLabel(ddl);
					var cbUNCSL = GetControlInGridFromCode('UNCSL','_cbDelete'); 
					if ( currentAmt.length == 2)
					{
						currentAmt[0] = ReplaceAll(currentAmt[0],',','');
						currentAmt[1] = ReplaceAll(currentAmt[1],',','');
					}
					else
					{
						currentAmt[0] = ReplaceAll(currentAmt[0],',','');
					}
					//alert(currentAmt[0]);
					//Rejection////////////////////
					if ( trim(currentAmt[0]) == 'Reject' && trim(currentAmt[0]) == 'Reject' )
					{
							if ( SigDDL != null && cbUNCSL.checked==true)
							{
								SigDDL.style.display = "inline";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="1";

							}
							if ( SigDDL.options[SigDDL.selectedIndex].text == 'No' )
							{
								flagUNCSLM16=false;
							}
							else
							{
								flagUNCSLM16=true;
							} 
							if ( lblSigObt != null )
							{
								lblSigObt.style.display = "none";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="1";
							}
							SetRejRedUmCoverageMotor();
							flagminvalue=true;
							SetRejRedUmCoverage();
							return;
					}
					var PUMSPDDL = GetControlInGridFromCode('PUNCS','_ddlLimit');
					//Get the currently selected amount in PUNCS
					if ( PUMSPDDL == null ) return;
					var PUMSPSplitAmt = PUMSPDDL.options[PUMSPDDL.selectedIndex].text;
					var PUMSPAmt = PUMSPSplitAmt.split('/');
					if ( currentAmt.length == 1 && PUMSPAmt.length ==1 )
					{
						currentAmt[0] = ReplaceAll(currentAmt[0],',','');
						PUMSPAmt[0] = ReplaceAll(PUMSPAmt[0],',','');
						if ( parseInt(currentAmt[0]) < parseInt(PUMSPAmt[0]) )
						 {
							flagminvalue=true;
							if ( SigDDL != null )
							{
								SigDDL.style.display = "inline";
								if (firstTime==false) SigDDL.selectedIndex=0;
								if ( hiddSignObt != null ) 
									hiddSignObt.value="1";
							}
							if(SigDDL != null)
							{
								if ( SigDDL.options[SigDDL.selectedIndex].text == 'No' )
								{
									flagUNCSLM16=false;
									
								}
								else
								{
									flagUNCSLM16=true;
								}
							}	 
							
							if ( lblSigObt != null )
							{
								lblSigObt.style.display = "none";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="1";
							}
							SetRejRedUmCoverageMotor();
							return;
						 }
						 if (parseInt(currentAmt[0]) > parseInt(PUMSPAmt[0]) )
						 {
							alert('Amount cannot be greater than Uninsured Motorists.');
							//Select amount 
							flagUNCSLM16=false;
							//flagUNDSP=false;
							SelectComboOptionByText(ddl.id,PUMSPSplitAmt);
							if ( SigDDL != null )
							{
								SigDDL.style.display = "none";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="0";
							}
							
							if ( lblSigObt != null )
							{
								lblSigObt.style.display = "inline";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="0";
							}
							flagminvalue=false;
							SetRejRedUmCoverageMotor();
							SetRejRedUmCoverage();
							return;
						 }
						 
						  if (parseInt(currentAmt[0]) == parseInt(PUMSPAmt[0]))
						 {
							flagminvalue=false;
							flagUNCSLM16 =false;
							if ( SigDDL != null )
							{
								SigDDL.selectedIndex=0;
								SigDDL.style.display = "none";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="0";
							}
							
							if ( lblSigObt != null )
							{
								lblSigObt.style.display = "inline";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="0";
							}
							SetRejRedUmCoverageMotor();
							SetRejRedUmCoverage();
						 }
						 
					}						
				 }	
				}
			//////////end here added on 9 sep 08			
		/////Added on 4 sep 08 Itrack 4496
				if ( COV_CODE == 'UNDSP')
				{
					var lobState = document.getElementById('hidLOBState').value;
					if (lobState != 'PPAIndiana') return;
					//flagA9=false;
					flagUNSCL=false;
					flagUNDSP=false;
					if (ddl.selectedIndex==-1) return;
					var currentSplitAmt = ddl.options[ddl.selectedIndex].text;
					var currentAmt = currentSplitAmt.split('/');
					var lblSigObt = GetSigObtLabel(ddl);		
					var SigDDL = GetSigDDL(ddl);
					var hiddSignObt = GethiddSigObtLabel(ddl); 
					if ( currentAmt.length == 2)
					{
						currentAmt[0] = ReplaceAll(currentAmt[0],',','');
						currentAmt[1] = ReplaceAll(currentAmt[1],',','');
					}
					//Rejection////////////////////
					if ( trim(currentAmt[0]) == 'Reject' && trim(currentAmt[0]) == 'Reject' )
					{
							if ( SigDDL != null )
							{
								SigDDL.style.display = "inline";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="1";

							}
							if ( SigDDL.options[SigDDL.selectedIndex].text == 'No' )
							{
								//flagA9=false;
								flagUNDSP=false;
							}
							else
							{
								//flagA9=true;
								flagUNDSP=true;
							} 
							if ( lblSigObt != null )
							{
								lblSigObt.style.display = "none";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="1";
							}
							SetRejRedUmCoverage();
							return;
					}
					var PUMSPDDL = GetControlInGridFromCode('PUMSP','_ddlLimit');
					//Get the currently selected amount in PUMSP
					if ( PUMSPDDL == null ) return;
					var PUMSPSplitAmt = PUMSPDDL.options[PUMSPDDL.selectedIndex].text;
					var PUMSPAmt = PUMSPSplitAmt.split('/');
					if ( currentAmt.length == 2 && PUMSPAmt.length == 2 )
					{
						currentAmt[0] = ReplaceAll(currentAmt[0],',','');
						currentAmt[1] = ReplaceAll(currentAmt[1],',','');
						
						PUMSPAmt[0] = ReplaceAll(PUMSPAmt[0],',','');
						PUMSPAmt[1] = ReplaceAll(PUMSPAmt[1],',','');
			
						if ( parseInt(currentAmt[0]) < parseInt(PUMSPAmt[0]) 
							 || parseInt(currentAmt[1]) < parseInt(PUMSPAmt[1]) )
						 {
							if ( SigDDL != null )
							{
								SigDDL.style.display = "inline";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="1";
							}
							if(SigDDL != null)
							{
								if ( SigDDL.options[SigDDL.selectedIndex].text == 'No' )
								{
									//flagA9=false;
									flagUNDSP=false;
								}
								else
								{
									//	flagA9=true;
									flagUNDSP=true;
								}
							}	 
							
							if ( lblSigObt != null )
							{
								lblSigObt.style.display = "none";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="1";
							}
							//flagminvalue=true;
							SetRejRedUmCoverage();
							return;
						 }
						 if (
								parseInt(PUMSPAmt[0])==25  && parseInt(PUMSPAmt[1])==50000 && 
								parseInt(currentAmt[0])==50 &&  parseInt(currentAmt[1])==50000
							)	
							{
							//Allow 50/50000 if 25/50000 in uninsured
							}
						 else if (
								parseInt(currentAmt[0]) > parseInt(PUMSPAmt[0]) ||
								parseInt(currentAmt[1]) > parseInt(PUMSPAmt[1])
							)
						 {
							alert('Amount cannot be greater than Uninsured Motorists.');
							//Select amount 
							//flagA9=false;
							flagUNDSP=false;
							if (parseInt(PUMSPAmt[0])==25  && parseInt(PUMSPAmt[1])==50000)
							 SelectComboOptionByText(ddl.id,'50 /50,000');
							else
							 SelectComboOptionByText(ddl.id,PUMSPSplitAmt);
							if ( SigDDL != null )
							{
								SigDDL.style.display = "none";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="0";
							}
							
							if ( lblSigObt != null )
							{
								lblSigObt.style.display = "inline";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="0";
							}
							//flagminvalue=false;
							SetRejRedUmCoverage();
							return;
						 }
    					  if (
									parseInt(currentAmt[0]) == parseInt(PUMSPAmt[0]) ||
									parseInt(currentAmt[1]) == parseInt(PUMSPAmt[1])
								)
						 {
							
							//flagA9 =false;
							flagUNDSP=false;
							if ( SigDDL != null )
							{
								SigDDL.style.display = "none";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="0";
							}
							
							if ( lblSigObt != null )
							{
								lblSigObt.style.display = "inline";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="0";
							}
							//flagminvalue=false;
							SetRejRedUmCoverage();
						 }
						 
					}						
				}
			//////////end here added on 4 sep 08
							
				//Bodily Injury Liability ( Split Limit) 
				if ( COV_CODE == 'BISPL')
				{
					//Part C - Uninsured Motorists (BI Split Limit) 
					//var targetDDL = GetPolicyLimitDDLCode('PUMSP');
					var targetDDL = GetControlInGridFromCode('PUMSP','_ddlLimit');
					//var sigDDL = GetSigDDLFromCode('PUMSP');
					var sigDDL = GetControlInGridFromCode('PUMSP','_ddlSignatureObtained');
					var sigLabel = GetSigLabelFromCode('PUMSP');
					//var hiddSignObt = GetControlInGridFromCode('PUMSP','_hiddSignObt')
					var hiddSignObt = GethiddSigObtLabel(targetDDL);
					
					//alert(targetDDL);
					if ( targetDDL == null ) return;
				 	
					SelectComboOptionByText(targetDDL.id,ddl.options[ddl.selectedIndex].text);
					
					if ( sigDDL != null )
					{
						sigDDL.style.display = "none";
						flagA9=false;
						flagUNSCL=false;
						if ( hiddSignObt != null ) 
								hiddSignObt.value="0";
					}
					
					if ( sigLabel != null )
					{
						sigLabel.style.display = "inline";
						if ( hiddSignObt != null ) 
									hiddSignObt.value="0";
					}
					if (targetDDL.selectedIndex==-1) return;
					var currentSplitAmt = targetDDL.options[targetDDL.selectedIndex].text;
					var currentAmt = currentSplitAmt.split('/');
					
					if ( currentAmt.length == 2)
					{
						currentAmt[0] = ReplaceAll(currentAmt[0],',','');
						currentAmt[1] = ReplaceAll(currentAmt[1],',','');
					}
					
					//Underinsured Motorists (BI Split Limit) 
					var targetDDLUNDSP = GetControlInGridFromCode('UNDSP','_ddlLimit');
					if (targetDDLUNDSP!=null)
					{
					SelectComboOptionByText(targetDDLUNDSP.id,ddl.options[ddl.selectedIndex].text); // added on 4 sep Itrack 4496
					OnDDLChange(targetDDLUNDSP);
					}
					var tb = GetPolicyLimitTextBoxFromCode('UNDSP');
					if ( tb != null )
					{
						var lobState = document.getElementById('hidLOBState').value;
						
						//For Auto Indiana only: If 25/50,000 then 50/500000/////
						if ( lobState == 'PPAIndiana' || lobState == "MOTIndiana")
						{
							if ( currentAmt[0] == 25 && currentAmt[1] == 50000 )
							{
								tb.value = '50 /50,000';
							}
							else
							{
								//Put currently selected amount in UnderInsured Split
								tb.value = targetDDL.options[targetDDL.selectedIndex].text;
							}
						}
						else
						{
							tb.value = targetDDL.options[targetDDL.selectedIndex].text;
						}
							
					}
					
					//Make invisible the Sig Obt DDL
					var sigDDL = GetSigDDL(targetDDL);
					//var hiddSignObt = GetControlInGridFromCode('PUMSP','_hiddSignObt')
					var hiddSignObt=GethiddSigObtLabel(targetDDL); 
					/*
					if ( sigDDL != null)
					{
						sigDDL.style.display = "none";
					}*/
					SetRejRedUmCoverage();
					SetRejRedUmCoverageMotor();
				}
				
				//Part A - Property Damage Liability 
				if ( COV_CODE == 'PD' )
				{
					//Part C - Unisured Motorists (PD) (A -21)
					//var targetDDL=GetPolicyLimitDDLCode('UMPD')
					var targetDDL = GetControlInGridFromCode('UMPD','_ddlLimit');
						if (targetDDL == null ) return ;
						
						
							SelectComboOptionByText(targetDDL.id,ddl.options[ddl.selectedIndex].text);
							
							//Make invisible the Sig Obt DDL
							var sigDDL = GetSigDDL(targetDDL);
							//var hiddSignObt=GetControlInGridFromCode('UMPD','_hiddSignObt');
							var hiddSignObt=GethiddSigObtLabel(targetDDL);
									
							if ( sigDDL != null)
							{
								sigDDL.style.display = "none";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="0";
							}
								
				}
			
				//Part C - Unisured Motorists (PD) (A - 21) 
				if ( COV_CODE == 'UMPD' )
				{
					var sigDDL = GetSigDDL(ddl);
					var lblSigObt = GetSigObtLabel(ddl);		
					//var hiddSignObt = GetControlInGridFromCode('UMPD','_hiddSigObt');
					var hiddSignObt=GethiddSigObtLabel(ddl);
					var currentAmt = ReplaceAll(ddl.options[ddl.selectedIndex].text,',','');
					
					//Get PD Amount
					//var ddlPD = GetPolicyLimitDDLCode('PD');
					var ddlPD = GetControlInGridFromCode('PD','_ddlLimit');
					var cbPD = GetControlInGridFromCode('PD','_cbDelete');
					if ( ddlPD == null) return;
					var PDAmt = ReplaceAll(ddlPD.options[ddlPD.selectedIndex].text,',','');
					PDAmt = ReplaceAll(PDAmt,',','');
					if ( sigDDL != null)
						{
									sigDDL.style.display = "none";
									if ( hiddSignObt != null ) 
									hiddSignObt.value="0";
						}
								
				    if ( lblSigObt != null)
					{
									lblSigObt.style.display = "inline";
									if ( hiddSignObt != null ) 
									hiddSignObt.value="0";
					}
					var UMPDeuctibleDDL = GetControlInGridFromCode('UMPD','_ddlDEDUCTIBLE');
					//Rejection case///////////////////////////////////////////
					if ( trim(currentAmt) == 'Reject' )
					{
						if(UMPDeuctibleDDL)
						{
						UMPDeuctibleDDL.selectedIndex=0;
						UMPDeuctibleDDL.disabled=true;
						}
						if ( sigDDL != null )
						{
							sigDDL.style.display = "inline";
							if (firstTime==false) sigDDL.selectedIndex=0;
							OnSigDDLChange(sigDDL);
							if ( hiddSignObt != null ) 
									hiddSignObt.value="1";
						}
						
						if ( lblSigObt != null )
						{
							lblSigObt.style.display = "none";
							if ( hiddSignObt != null ) 
									hiddSignObt.value="1";
						}
						flag17UMPD =false;
						SetRejRedUmCoverageMotor17();
						return;
					}
					else
					{
						if(UMPDeuctibleDDL)
						{
							if (firstTime==false) UMPDeuctibleDDL.selectedIndex=1;
							UMPDeuctibleDDL.disabled=false;
						}
						if ( sigDDL != null )
						{
							if (firstTime==false) sigDDL.selectedIndex=0;
							OnSigDDLChange(sigDDL);
						}						
					}
                        flag17UMPD=true;	
						SetRejRedUmCoverageMotor17();
					////////////////////////////////////////////////////////////////
					//Ravindra(04-26-2006) Rule of lesser amount than PD not applicable for Indiana
					var lobState = document.getElementById('hidLOBState').value;	
				if(cbPD.checked == true)
				 {
					if (parseInt(currentAmt) < parseInt(PDAmt) && lobState != 'MOTIndiana')
						{
													
							if ( sigDDL != null)
							{
								sigDDL.style.display = "inline";
								if (firstTime==false) sigDDL.selectedIndex=0;
								OnSigDDLChange(sigDDL);
								if ( hiddSignObt != null ) 
									hiddSignObt.value="1";
							}
							
							if ( lblSigObt != null)
							{
								lblSigObt.style.display = "none";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="1";
							}
						
						}
					else
						{
				
							if ( sigDDL != null)
							{
								sigDDL.style.display = "none";
								if (firstTime==false)  sigDDL.selectedIndex=0;
								OnSigDDLChange(sigDDL);
								if ( hiddSignObt != null ) 
									hiddSignObt.value="0";
							}
							
							if ( lblSigObt != null)
							{
								lblSigObt.style.display = "inline";
								if ( hiddSignObt != null ) 
									hiddSignObt.value="0";
							}
						}
					}	

								
				}//End of UMPD
				if(COV_CODE =='COMP')
				{
				  var compAmount
				  var vehAmount=parseInt(document.getElementById('hidVehicleAmount').value);
				  var noClaim= parseInt(document.getElementById('hidClaims').value);
				  if(document.getElementById('hidVehicleType').value=='11336' && vehAmount <= 50000)
				  {
				     compAmount=100;
				  }
			
				  if(noClaim==2)
				  {
				     compAmount=150;
				  }
				  else if(noClaim==3)
				  {
				     compAmount=250;
				  }
				  else if(noClaim>=4)
				  {
				     compAmount=500;
				  }
    			  if(document.getElementById('hidVehicleMake').value=='true')
    			  {
    			     compAmount=500;
    			  }
    			  if(document.getElementById('hidVehicleType').value=='11336' && vehAmount > 50000)
				  {
				     compAmount=2000;
				  }
				  
				  var targetDDL = GetControlInGridFromCode('COMP','_ddlDEDUCTIBLE');
				  var compAmt = ReplaceAll(targetDDL.options[targetDDL.selectedIndex].text,',','');
				  compAmt =parseInt(compAmt);
				  if(compAmount > compAmt )
				  {
				        compAmount =formatCurrency(compAmount);
				  		SelectDropdownOptionByText(targetDDL,compAmount);
				  }
				  else if(document.getElementById('hidUseVehicle').value=="11333" && document.getElementById('hidVehicleType').value=='11341' && compAmt<250)
				  {
					SelectDropdownOptionByValueWithReturn(targetDDL,165)
				  }		
				 //added by pravesh on 8 july 08 as itrack #4432
				 compAmt = ReplaceAll(targetDDL.options[targetDDL.selectedIndex].text,',','');
				 compAmt =parseInt(compAmt);
        		 var DeductibleEECOMP  = GetControlInGridFromCode('EECOMP','_ddlDEDUCTIBLE');
				 compAmount =formatCurrency(compAmt);
				 if(DeductibleEECOMP!=null)
				 SelectDropdownOptionByText(DeductibleEECOMP,compAmount);
				}
		     if(lobState == 'PPAIndiana' || lobState =='PPAMichigan')
		     {
		     if(COV_CODE =='OTC')
				{
				  var compAmount
				  var vehAmount=parseInt(document.getElementById('hidVehicleAmount').value);
				  var noClaim= parseInt(document.getElementById('hidClaims').value);
				  if(document.getElementById('hidVehicleType').value=='11336' && vehAmount <= 50000)
				  {
				     compAmount=100;
				  }
				
				  if(noClaim==2)
				  {
				     compAmount=150;
				  }
				  else if(noClaim==3)
				  {
				     compAmount=250;
				  }
				  else if(noClaim>=4)
				  {
				     compAmount=500;
				  }
    			  if(document.getElementById('hidVehicleMake').value=='true')
    			  {
    			     compAmount=500;
    			  }
    			  if(document.getElementById('hidVehicleType').value=='11336' && vehAmount > 50000)
				  {
				     compAmount=2000;
				  }
				  
				  var targetDDL = GetControlInGridFromCode('OTC','_ddlDEDUCTIBLE');
				  var compAmt = ReplaceAll(targetDDL.options[targetDDL.selectedIndex].text,',','');
				  compAmt =parseInt(compAmt);
				  if(compAmount > compAmt )
				  {
				        compAmount =formatCurrency(compAmount);
				  		SelectDropdownOptionByText(targetDDL,compAmount);
				  }
				  else if(document.getElementById('hidUseVehicle').value=="11333" && document.getElementById('hidVehicleType').value=='11341')
				  {
					SelectDropdownOptionByValueWithReturn(targetDDL,82)
				  }				  
				 //added by pravesh on 8 july 08 as itrack #4432
				  compAmt = ReplaceAll(targetDDL.options[targetDDL.selectedIndex].text,',','');
				  compAmt =parseInt(compAmt);
        		 var DeductibleEECOMP  = GetControlInGridFromCode('EECOMP','_ddlDEDUCTIBLE');
				 compAmount =formatCurrency(compAmt);
				 if(DeductibleEECOMP!=null)
				 SelectDropdownOptionByText(DeductibleEECOMP,compAmount);
				}
	          }

	     
	        if(lobState == 'MOTIndiana' || lobState =='MOTMichigan')
		     {
				if(COV_CODE =='OTC')
				{
					
					var compAmount
					var noSymbol= parseInt(document.getElementById('hidSymbol').value);
					  
					if(noSymbol >=1 && noSymbol < 3)
					{
						compAmount=100;
					}
					else if(noSymbol >=3 && noSymbol < 9)
					{
						compAmount=250;
					}
					else if(noSymbol >=9)
					{
						compAmount=1000;
					}
					if(document.getElementById('hidMotorcycleType').value=="11424")
					{
							if(noSymbol >=1 && noSymbol < 9)
							{
								compAmount=500;
							}
					}
					var targetDDL = GetControlInGridFromCode('OTC','_ddlDEDUCTIBLE');
					var compAmt = ReplaceAll(targetDDL.options[targetDDL.selectedIndex].text,',','');
					compAmt =parseInt(compAmt);
					if(compAmount > compAmt )
					{
							compAmount =formatCurrency(compAmount);
				  			SelectDropdownOptionByText(targetDDL,compAmount);
					}
					else
					{
					  compAmount = compAmt
					}
					
					var cbEBM49  = GetControlInGridFromCode('EBM49','_cbDelete');
					var txtEBM49 = GetControlInGridFromCode('EBM49','_txtDeductible');
					if(cbEBM49!=null)
					if(cbEBM49.checked==true)
					{
					  txtEBM49.value=compAmount;
					
					}
					
				 }
				if(COV_CODE =='COLL')
				{
			
					var compAmount
					var noSymbol= parseInt(document.getElementById('hidSymbol').value);
					if(noSymbol >=1 && noSymbol < 3)
					{
						compAmount=100;
					}
					else if(noSymbol >=3 && noSymbol < 6)
					{
						compAmount=250;
					}
					else if(noSymbol >=6 && noSymbol < 9)
					{
						compAmount=500;
					}
					else if(noSymbol >=9)
					{
						compAmount=1000;
					}

					if(document.getElementById('hidMotorcycleType').value=="11424")
					{
							if(noSymbol >=1 && noSymbol < 9)
							{
								compAmount=500;
							}
							if(noSymbol >= 9)
							{
								compAmount=1000;
							}
					}
					var targetDDL = GetControlInGridFromCode('COLL','_ddlDEDUCTIBLE');
					var compAmt = ReplaceAll(targetDDL.options[targetDDL.selectedIndex].text,',','');
					compAmt =parseInt(compAmt);
					if(compAmount > compAmt )
					{
							compAmount =formatCurrency(compAmount);
				  			SelectDropdownOptionByText(targetDDL,compAmount);
				  			setCoverageA68(ddl);
					}
					else
					{
					  compAmount = compAmt
					}
					var cbEBM49  = GetControlInGridFromCode('CEBM49','_cbDelete');
					var txtEBM49 = GetControlInGridFromCode('CEBM49','_txtDeductible');
					if(cbEBM49!=null)
					{
						if(cbEBM49.checked==true)
						{
							txtEBM49.value=compAmount;
						}
					}
					
				 }
	          }
	         if(COV_CODE =='COLL')
				{
				
				  var compAmount
				  var vehAmount=parseInt(document.getElementById('hidVehicleAmount').value);
				  var noClaim= parseInt(document.getElementById('hidClaims').value);
				  if(document.getElementById('hidVehicleType').value=='11336' && vehAmount <= 50000)
				  {
				     compAmount=500;
				  }
    			  if(document.getElementById('hidVehicleMake').value=='true')
    			  {
    			     compAmount=500;
    			  }
    			  if(document.getElementById('hidVehicleType').value=='11336' && vehAmount > 50000)
				  {
				     compAmount=2000;
				  }
				  
				  var targetDDL = GetControlInGridFromCode('COLL','_ddlDEDUCTIBLE');
				  var compAmt = ReplaceAll(targetDDL.options[targetDDL.selectedIndex].text,',','');
				  compAmt =parseInt(compAmt);
				  if(document.getElementById('hidVehicleMake').value=='true' && compAmt==0) //added on 15 Jan 09 as per Margot Mail
				  {
				  //allow 0 if Make is Corvette 
				  //Yes – 0 limited is to be offered 
				  //Confirmed with WM  Margot 
				  }
				  else if(document.getElementById('hidVehicleType').value=='11336' && vehAmount <= 50000 && (compAmt==0 || ReplaceAll(targetDDL.options[targetDDL.selectedIndex].text,',','')=='400 Regular')) // added by pravesh as allow 0 limited also in case of motor home Itrack 4546/ also allow 400 Regular Itrack 5154
				  {
				  //allow 0
				  }				  
				  else if(compAmount > compAmt )
				  {   
				        compAmount =formatCurrency(compAmount);
				        if (lobState == 'PPAIndiana')
				          SelectDropdownOptionByText(targetDDL,compAmount);
				        else  
				          SelectDropdownOptionByText(targetDDL,compAmount + ' Broad');
				         setCoverageA68(ddl); 
				  }
				  else if(document.getElementById('hidUseVehicle').value=="11333" && document.getElementById('hidVehicleType').value=='11341' && compAmt<250) //itrack 4529
				  {
					 if (lobState == 'PPAIndiana')
						SelectDropdownOptionByValueWithReturn(targetDDL,72)
					else	
						SelectDropdownOptionByValueWithReturn(targetDDL,607)
					setCoverageA68(ddl);	
				  }	
				//added by pravesh on 8 july 08 as itrack #4432
        		 var DeductibleEECOLL  = GetControlInGridFromCode('EECOLL','_ddlDEDUCTIBLE');
        		 if(DeductibleEECOLL!=null)
				 SelectDropdownOptionByText(DeductibleEECOLL,targetDDL.options[targetDDL.selectedIndex].text);
				}
			}//End of onChange
			//********************End of DDL Selection chaged code********
			function setCoverageA68(ddl)
			{
				//var cbRoadService = GetCheckBoxFromCode('ROAD');
				var cbRCC68  = GetControlInGridFromCode('RCC68','_cbDelete');
				var strRejected = ddl.options[ddl.selectedIndex].value
				if ( cbRCC68 != null )
				{
					if(strRejected==612 || strRejected==613 )
					{
						cbRCC68.checked = true;
						cbRCC68.disabled = true;
						DisableControls(cbRCC68.id);
						SetHiddenField(cbRCC68.id);
					}
					else
					{
						cbRCC68.checked = false;
						cbRCC68.disabled = false;
						DisableControls(cbRCC68.id);
						SetHiddenField(cbRCC68.id);
					
					}
					}
			}		
			//OnBlur of textbox
			function onLimitChange(currentTextBox, covCode)
			{
				//alert('palp');
				if (Page_IsValid == false) return;
				
				if ( covCode == 'EECOMP' )
				{
					var tb = GetControlInGridFromCode('EECOLL','_txtLIMIT');
					//tb.value = formatCurrency(currentTextBox.value);
					tb.value = currentTextBox.value;
				}
			}
			
			function GetSigLabelFromCode(covCode)
			{
				var rowCount = 20;
				
				if ( document.Form1.hidPOLICY_ROW_COUNT != null )
				{
					rowCount = document.Form1.hidPOLICY_ROW_COUNT.value;
				}
				
				//alert(rowCount);
				
				for (ctr = 2; ctr < rowCount + 2; ctr++)
				{
				
					chk = document.getElementById(policyPrefix + ctr + "_cbDelete");
				
					var sigLabel = document.getElementById(policyPrefix + ctr + "_lblSigObt");
					
					
					if ( chk != null )
					{
						var span = chk.parentElement;
									
						if ( span != null )
						{
							var coverageCode = span.getAttribute("COV_CODE");
					
							if ( trim(covCode) == trim(coverageCode) )
							{
								//alert(covCode + ' ' + coverageCode );
								return sigLabel;
							}
						}
					}
				}
				
				return null;
			}
			//Over ride Default Rule In Case Of India When Comprehensive Only Is "YES"
			function OverrideRule()
			{
				var cbRRUM = GetControlInGridFromCode('RRUM','_cbDelete');
				
				if(cbRRUM != null) 
				{
					var A9 = document.getElementById('hidA9').value; 
					if(A9 == "1")
					{
						cbRRUM.checked = true;
						DisableControls(cbRRUM.id);
						SetHiddenField(cbRRUM.id);
					}
				}
				
			   var lobState = document.getElementById('hidLOBState').value; 
			   if (lobState == 'MOTIndiana' && document.getElementById('hidComp').value=="10963")
			   { 
					var cbBISPL = GetControlInGridFromCode('BISPL','_cbDelete');
					var cbPD = GetControlInGridFromCode('PD','_cbDelete');
					if(cbBISPL != null) 
					{
						cbBISPL.checked = false;
						DisableControls(cbBISPL .id);
						SetHiddenField(cbBISPL .id);
					}
					if(cbPD  != null) 
					{
						cbPD.checked = false;
						DisableControls(cbPD.id);
						SetHiddenField(cbPD.id);
					}
			   }
			   ////added by pravesh
			   if(document.getElementById('hidVehicleType').value !='11335') 
			    {
						var cbEBCE= GetControlInGridFromCode('EBCE','_cbDelete');
						if(cbEBCE != null) 
						{
							//EnableDisableRow(cbEBCE.parentElement.parentElement.parentElement, true);
							cbEBCE.parentElement.disabled=true;
							cbEBCE.disabled = true;
						}
				}		
			   //////end
			   if (lobState == 'MOTMichigan')
			   { 
			        if(parseInt(document.getElementById('hidCC').value) < 51)
			        {
						var cbMEDPM2 = GetControlInGridFromCode('MEDPM2','_cbDelete');
						
						if(cbMEDPM2 != null) 
						{
							EnableDisableRow(cbMEDPM2.parentElement.parentElement.parentElement, false);
							cbMEDPM2.parentElement.disabled=false;
							cbMEDPM2.disabled = false;
						}
				    }
				    var strMEDPM2 = document.getElementById('hidMEDPM2').value; 
				    if(strMEDPM2 == "1")
			        {
			            var cbMEDPM2 = GetControlInGridFromCode('MEDPM2','_cbDelete');
			            if(cbMEDPM2 != null)
			            {
							cbMEDPM2.checked = true;
							DisableControls(cbMEDPM2.id);
							SetHiddenField(cbMEDPM2.id);
						}
				    }
			   }
		}
		function AssignTextToHid(hidCntrl) {

		    var rowCount = document.getElementById(hidCntrl).value;
		    var Txt;
		    var Hid;
		    for (ctr = 2; ctr < rowCount + 2; ctr++) {

		        if (ctr < 10) {
		            Hid = document.getElementById(policyPrefix + '0' + ctr + "_hidLIMIT");
		            Txt = document.getElementById(policyPrefix + '0' + ctr + "_txtLIMIT");
		        }
		        else {
		            Hid = document.getElementById(policyPrefix + ctr + "_hidLIMIT");
		            Txt = document.getElementById(policyPrefix + ctr + "_txtLIMIT");
		        }

		        if (Txt != null && Hid != null) {
		            Hid.value = Txt.value;
		          
		        } 
		    } 
		}
		    
		 function Init() {
		     ApplyColor();
		     ChangeColor();
		     EnableDisableAddInfor();
		     OverrideRule();
		     AssignTextToHid('hidROW_COUNT');
		     AssignTextToHid('hidPOLICY_ROW_COUNT');
		     
		     
		  }
		</script>
	</HEAD>
	<body  oncontextmenu="return true;" leftMargin="0" rightMargin="0" onload="Init();"
		MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="headereffectCenter"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow><asp:label id="lblTitle" runat="server">Coverages</asp:label></td>
				</tr>
				<tr>
					<td align="center"><asp:label id="lblMessage" runat="server" EnableViewState="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<tr>
					<td class="headerEffectSystemParams">
						<asp:Label ID="lblPolicyCaption" Runat="server">Policy Level Coverages</asp:Label>
					</td>
				</tr>
				<tr id="trPOLICY_LEVEL_GRID" runat="server">
					<td class="midcolora"><asp:datagrid id="dgPolicyCoverages" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyField="COVERAGE_ID">
							<AlternatingItemStyle></AlternatingItemStyle>
							<ItemStyle></ItemStyle>
							<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Required /Optional">
									<ItemTemplate>
										<asp:Label ID="lblCOV_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
										</asp:Label>
										<asp:CheckBox ID="cbDelete" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' COV_ID='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
										</asp:CheckBox>
										<input type="hidden" id="hidcbDelete" name="hidcbDelete" runat="server">
										<asp:Label ID="lblLIMIT_TYPE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT_TYPE") %>'>
										</asp:Label>
										<asp:Label ID="lblDEDUCTIBLE_TYPE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_TYPE") %>'>
										</asp:Label>
										<asp:Label ID="lblIS_LIMIT_APPLICABLE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IS_LIMIT_APPLICABLE") %>'>
										</asp:Label>
										<asp:Label ID="lblIS_DEDUCT_APPLICABLE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IS_DEDUCT_APPLICABLE") %>'>
										</asp:Label>
										<asp:Label ID="lblAdd_IS_DEDUCT_APPLICABLE" style="display:none" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ISADDDEDUCTIBLE_APP") %>'>
										</asp:Label>
										<input type="hidden" id="hidCHECKDDISABLE" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidCHECKDDISABLE">
										<input type="hidden" id="hidCHECKDENABLE" runat="server"  COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidCHECKDENABLE">
										<input type="hidden" id="hidCHECKDSELECT" runat="server"  COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidCHECKDSELECT">
										<input type="hidden" id="hidCHECKDDSELECT" runat="server"  COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidCHECKDDSELECT">
										<input type="hidden" id="hidUNCHECKDDISABLE" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidUNCHECKDDISABLE">
										<input type="hidden" id="hidUNCHECKDENABLE" runat="server"  COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidUNCHECKDENABLE">
										<input type="hidden" id="hidUNCHECKDSELECT" runat="server"  COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidUNCHECKDSELECT">
										<input type="hidden" id="hidUNCHECKDDSELECT" runat="server"  COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidUNCHECKDDSELECT">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="36%" HeaderText="Coverage">
									<ItemTemplate>
										<asp:Label runat="server" ID="lblCOV_DESC" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Limit" ItemStyle-Width="26%">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<select  id="ddlLIMIT" Visible="True" Runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' onchange="javascript:OnDDLChange(this);" NAME="ddlLIMIT">
										</select>
										<asp:label id="lblLIMIT" CssClass="labelfont" Runat="server">
											<%# DataBinder.Eval(Container, "DataItem.INCLUDED","{0:,#,###}") %>
										</asp:label>
										<asp:TextBox ID="txtLIMIT" Runat="server" CssClass="INPUTCURRENCY" MaxLength="7"></asp:TextBox>
										<input type="hidden" runat="server" id="hidLIMIT"  name="hidLIMIT"/>
										<BR>
										<asp:RegularExpressionValidator ID="revLIMIT" Enabled="False" Runat="server" ControlToValidate="txtLIMIT" Display="Dynamic"></asp:RegularExpressionValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Deductible" ItemStyle-Width="18%">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<select id="ddlDEDUCTIBLE" Visible="True" Runat="server" NAME="ddlDEDUCTIBLE">
										</select>
										<asp:Label id="lblDEDUCTIBLE" CssClass="labelfont" Runat="server">N.A.</asp:Label>
										<asp:TextBox ID="txtDEDUCTIBLE" Runat="server" CssClass="INPUTCURRENCY" MaxLength="10"></asp:TextBox>
										<BR>
										<asp:RegularExpressionValidator ID="revDEDUCTIBLE" Enabled="False" Runat="server" ControlToValidate="txtDEDUCTIBLE"
											Display="Dynamic"></asp:RegularExpressionValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Sig. Obt." ItemStyle-Width="10%">
									<ItemTemplate>
										<asp:DropDownList ID="ddlSignatureObtained" Visible="False" Runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' onchange="javascript:OnSigDDLChange(this);">
											<asp:ListItem Value='N'>No</asp:ListItem>
											<asp:ListItem Value='Y'>Yes</asp:ListItem>
										</asp:DropDownList>
										<asp:label id="lblSigObt" CssClass="labelfont" Runat="server" Visible="True">N.A.</asp:label>
										<input type=hidden id="hiddSigObt" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ISADDDEDUCTIBLE_APP") %>' NAME="hiddSigObt">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="True">
									<ItemTemplate>
										<asp:Label ID="lblCOV_CODE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False">
									<ItemTemplate>
										<asp:Label ID="lblCOV_TYPE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COVERAGE_TYPE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td class="headerEffectSystemParams">Vehicle Level Coverages</td>
				</tr>
				<tr>
					<td class="midcolora"><asp:datagrid id="dgCoverages" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyField="COVERAGE_ID">
							<AlternatingItemStyle></AlternatingItemStyle>
							<ItemStyle></ItemStyle>
							<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Opt/Reject" ItemStyle-Width="10%">
									<ItemTemplate>
										<asp:Label ID="lblCOV_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
										</asp:Label>
										<asp:CheckBox ID="cbDelete" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' COV_ID='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
										</asp:CheckBox>
										<input type="hidden" id="hidcbDelete" name="hidcbDelete" runat="server">
										<asp:Label ID="lblLIMIT_TYPE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT_TYPE") %>'>
										</asp:Label>
										<asp:Label ID="lblDEDUCTIBLE_TYPE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_TYPE") %>'>
										</asp:Label>
										<asp:Label ID="lblIS_LIMIT_APPLICABLE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IS_LIMIT_APPLICABLE") %>'>
										</asp:Label>
										<asp:Label ID="lblIS_DEDUCT_APPLICABLE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IS_DEDUCT_APPLICABLE") %>'>
										</asp:Label>
										<asp:Label ID="lblAdd_IS_DEDUCT_APPLICABLE" style="display:none" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ISADDDEDUCTIBLE_APP") %>'>
										</asp:Label>
										<input type="hidden" id="hidCHECKDDISABLE" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidCHECKDDISABLE">
										<input type="hidden" id="hidCHECKDENABLE" runat="server"  COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidCHECKDENABLE">
										<input type="hidden" id="hidCHECKDSELECT" runat="server"  COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidCHECKDSELECT">
										<input type="hidden" id="hidCHECKDDSELECT" runat="server"  COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidCHECKDDSELECT">
										<input type="hidden" id="hidUNCHECKDDISABLE" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidUNCHECKDDISABLE">
										<input type="hidden" id="hidUNCHECKDENABLE" runat="server"  COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidUNCHECKDENABLE">
										<input type="hidden" id="hidUNCHECKDSELECT" runat="server"  COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidUNCHECKDSELECT">
										<input type="hidden" id="hidUNCHECKDDSELECT" runat="server"  COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidUNCHECKDDSELECT">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Coverage" ItemStyle-Width="40%">
									<ItemTemplate>
										<asp:Label runat="server" ID="lblCOV_DESC" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Limit" ItemStyle-Width="20%">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<select id="ddlLIMIT" Visible="True" Runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' onchange="javascript:OnDDLChange(this);" NAME="ddlLIMIT">
										</select>
										<asp:label id="lblLIMIT" CssClass="labelfont" Runat="server">
											<%# DataBinder.Eval(Container, "DataItem.INCLUDED","{0:,#,###}") %>
										</asp:label>
										<asp:TextBox ID="txtLIMIT" Runat="server" CssClass="INPUTCURRENCY" MaxLength="7"></asp:TextBox>
										<input type="hidden" runat="server" id="hidLIMIT"  name="hidLIMIT"/>
										<BR>
										<asp:RegularExpressionValidator ID="revLIMIT" Enabled="False" Runat="server" ControlToValidate="txtLIMIT" Display="Dynamic"></asp:RegularExpressionValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Deductible" ItemStyle-Width="20%">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<select id="ddlDEDUCTIBLE" Visible="True" Runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' onchange="javascript:OnDDLChange(this);" NAME="ddlDEDUCTIBLE">
										</select>
										<asp:Label id="lblDEDUCTIBLE" CssClass="labelfont" Runat="server">N.A.</asp:Label>
										<asp:TextBox ID="txtDEDUCTIBLE" Runat="server" CssClass="INPUTCURRENCY" MaxLength="10"></asp:TextBox>
										<BR>
										<asp:RegularExpressionValidator ID="revDEDUCTIBLE" Enabled="False" Runat="server" ControlToValidate="txtDEDUCTIBLE"
											Display="Dynamic"></asp:RegularExpressionValidator>
										<asp:CustomValidator ID="csvddlDEDUCTIBLE" Runat="server" Display="Dynamic" ErrorMessage="This type of vehicle is subject to Minimum $500 deductible."
											ControlToValidate="ddlDEDUCTIBLE" Enabled="False" Visible="False"></asp:CustomValidator>
										<asp:CustomValidator ID="csvddlDEDUCTIBLE1" Runat="server" Display="Dynamic" ErrorMessage="This type of vehicle is subject to Minimum $500 deductible."
											ControlToValidate="ddlDEDUCTIBLE" Enabled="False"></asp:CustomValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Sig. Obt." ItemStyle-Width="10%">
									<ItemTemplate>
										<asp:DropDownList ID="ddlSignatureObtained" Visible="True" Runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' onchange="javascript:OnSigDDLChange(this);">
											<asp:ListItem Value='N'>No</asp:ListItem>
											<asp:ListItem Value='Y'>Yes</asp:ListItem>
										</asp:DropDownList>
										<asp:label id="lblSigObt" CssClass="labelfont" Runat="server" Visible="TRUE">N.A.</asp:label>
										<input type=hidden id="hiddSigObt" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ISADDDEDUCTIBLE_APP") %>' NAME="hiddSigObt">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="True">
									<ItemTemplate>
										<asp:Label ID="lblCOV_CODE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False">
									<ItemTemplate>
										<asp:Label ID="lblCOV_TYPE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COVERAGE_TYPE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr id="trAddInformation" style="DISPLAY: none" runat="server">
					<td>
						<table width="100%">
							<tr>
								<td class="headerEffectSystemParams">Additional Information</td>
							</tr>
							<tr id="trHealthCare" runat="server">
								<td class="midcolora" align="left" width="25%"><asp:label id="lblHealthCare" Runat="server">Health Care Primary Carrier</asp:label><span class="mandatory" id="spnHealthCare">*</span></td>
								<td class="midcolora" width="32%"><asp:textbox id="txtHealthCare" size='30' Runat="server" MaxLength="30"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvHealthCare" Runat="server" ErrorMessage="Please enter Health Care Primary Carrier"
										Enabled="False" ControlToValidate="txtHealthCare" Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revHealthCare" Runat="server" ErrorMessage="Please enter alpha numeric" ControlToValidate="txtNoPersons"
										Display="Dynamic"></asp:regularexpressionvalidator></td>
								<td class="midcolora" colSpan="2"></td>
							</tr>
							<tr id="trNoPersons" runat="server">
								<td class="midcolora" align="right" width="25%"><asp:label id="lbltrNoPersons" Runat="server">No.of Persons -Extended Non Owned Coverage</asp:label><span class="mandatory" id="spnNoPersons">*</span></td>
								<td class="midcolora" align="right"><asp:textbox id="txtNoPersons" Runat="server" MaxLength="4"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvNoPersons" Runat="server" ErrorMessage="Please enter # of Persons" Enabled="False"
										ControlToValidate="txtNoPersons" Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revNoPersons" Runat="server" ErrorMessage="Please enter numeric" ControlToValidate="txtNoPersons"
										Display="Dynamic"></asp:regularexpressionvalidator></td>
								<td class="midcolora" colSpan="2"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width="100%">
							<tr>
								<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnCopy" runat="server" CausesValidation="False" Text="Copy Coverages"></cmsb:cmsbutton></td>
								<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidREC_VEH_ID" type="hidden" value="0" name="hidREC_VEH_ID" runat="server">
						<INPUT id="hidPolID" type="hidden" name="hidPolID" runat="server"> <INPUT id="hidPolVersionID" type="hidden" name="hidPolVersionID" runat="server">
						<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"> <INPUT id="hidROW_COUNT" type="hidden" value="0" name="hidROW_COUNT" runat="server">
						<INPUT id="hidPOLICY_ROW_COUNT" type="hidden" value="0" name="hidPOLICY_ROW_COUNT" runat="server">
						<INPUT id="hidCoverageXML" type="hidden" runat="server" NAME="hidCoverageXML"> <INPUT id="hidLOBState" type="hidden" name="hidLOBState" runat="server">
						<INPUT id="hidMotorcycleType" type="hidden" name="hidMotorcycleType" runat="server">
						<INPUT id="hidTYPE_OF_WATERCRAFT" type="hidden" name="hidTYPE_OF_WATERCRAFT" runat="server">
						<INPUT id="hidVehicleMake" type="hidden" name="hidVehicleMake" runat="server"> <INPUT id="hidRejectUMPD" type="hidden" name="hidRejectUMPD" runat="server">
						<INPUT id="hidCustomInfo" type="hidden" name="hidCustomInfo" runat="server"> <INPUT id="hidDataValue1" type="hidden" name="hidDataValue1" runat="server">
						<INPUT id="hidDataValue2" type="hidden" name="hidDataValue2" runat="server"> <INPUT id="hidDataValue3" type="hidden" name="hidDataValue3" runat="server">
						<INPUT id="hidControlXML" type="hidden" name="hidControlXML" runat="server"> <INPUT id="hidUseVehicle" type="hidden" name="hidUseVehicle" runat="server">
						<INPUT id="hidVehicleAmount" type="hidden" name="hidVehicleAmount" runat="server">
						<INPUT id="hidPipValue" type="hidden" name="hidPipValue" runat="server"> <INPUT id="hidVehicleType" type="hidden" name="hidVehicleType" runat="server">
						<INPUT id="hidClaims" type="hidden" name="hidClaims" runat="server"> <input id="hidattachUmb" type="hidden" runat="server" NAME="hidattachUmb">
						<input id="hidComp" type="hidden" runat="server" NAME="hidComp"> <input id="hidSymbol" type="hidden" runat="server" NAME="hidSymbol">
						<input id="hidMotorType" type="hidden" runat="server" NAME="hidMotorType"> <input id="hidCC" type="hidden" runat="server" NAME="hidCC">
						<input id="hidA9" type="hidden" runat="server" NAME="hidA9"> <input id="hidMEDPM2" type="hidden" runat="server" NAME="hidMEDPM2">
						<input id="hidMIS_COUNT" type="hidden" runat="server" value="0" NAME="hidMIS_COUNT">
						<input id="hidIsunder25" type="hidden" runat="server" value="0" NAME="hidIsunder25">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

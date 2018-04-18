<%@ Page language="c#" validateRequest=false  Codebehind="PolicyCoverages_Section1.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.aspx.HomeOwners.PolicyCoverages_Section1" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Coverages_Section1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../../cmsweb/scripts/common.js"></script>
		<script src="../../../cmsweb/scripts/form.js"></script>
		<script src="../../../cmsweb/scripts/Coverages.js"></script>
		<script language="javascript">
			var prefix = "dgCoverages__ctl";
			
			//This variable will be used by tab control, for checking ,whether save msg should be shown to user
			//while changing the tabs
			//True means Want to save msg should always be shown to user
			var ShowSaveMsgAlways = true;
			var firstTime = false;
			
			// Additional Amount  for Mine Subsidence is not greater than  Coverage A 
			function UpDateBuliding()
			{
			    
				var cbBUMC = GetControlInGridFromCode("BUMC","_cbDelete");
				if( cbBUMC == null) return;
				if(cbBUMC.checked ==false) return;
				var amount;
				var inAmountA=0;
				var inAmountB=0;
				var cbDWELL = GetControlInGridFromCode("DWELL","_cbDelete");
				if( cbDWELL != null)
				{
				    if(cbDWELL.checked ==true)
				    {
						var txtDWELL=GetControlInGridFromCode("DWELL","_txtLIMIT");
						if (txtDWELL != null)
						{
							if(txtDWELL.value != '')
							 //inAmountA=parseInt(ReplaceAll(txtDWELL.value,',',''));
							 inAmountA=parseInt(ReplaceAll(ReplaceAll(txtDWELL.value,',',''),'$',''));
						}
						var txtDWELL=GetControlInGridFromCode("DWELL","_txtDEDUCTIBLE_1_TYPE");
						if (txtDWELL != null)
						{
						    if(txtDWELL.value != '')
							 //inAmountA=parseInt(ReplaceAll(txtDWELL.value,',','')) + inAmountA;
							 inAmountA=parseInt(ReplaceAll(ReplaceAll(txtDWELL.value,',',''),'$','')) + inAmountA;
						}
					}
				   
				}
				var cbOS = GetControlInGridFromCode("OS","_cbDelete");
				if( cbOS != null)
				{
				    if(cbOS.checked ==true)
				    {
						var txtOS=GetControlInGridFromCode("OS","_txtLIMIT");
						if (txtOS != null)
						{
						    if(txtOS.value != '')
							  //inAmountB=parseInt(ReplaceAll(txtOS.value,',',''));
								inAmountB=parseInt(ReplaceAll(ReplaceAll(txtOS.value,',',''),'$',''));
						}
						txtOS=GetControlInGridFromCode("OS","_txtDEDUCTIBLE_1_TYPE");
						if (txtOS != null)
						{
							if(txtOS.value != '')
							 //inAmountB=parseInt(ReplaceAll(txtOS.value,',','')) + inAmountB;
							 inAmountB=parseInt(ReplaceAll(ReplaceAll(txtOS.value,',',''),'$','')) + inAmountB;
						}
					}
				   
				}
				amount=inAmountA + inAmountB;
				amount=amount * .1;
				var lblBUMC=GetControlInGridFromCode("BUMC","_lblLIMIT");
				lblBUMC.innerText=amount;
				SetHiddenFieldControls(lblBUMC.id);
			    lblBUMC.innerText=formatCurrency(amount);
				
			}
			function CoverageMinValidation(objSource, objArgs)
			{
			  
				//Get The Coverage A 
				var txtDwell= GetControlInGridFromCode("DWELL","_txtLIMIT");
				
				if( txtDwell == null)
				    return;
				
				//Get The Mine Subsidence (HO-287) Value
				var txtMin = document.getElementById(objSource.controltovalidate);
				//var sourceVal     = parseInt(ReplaceAll(txtMin.value,',',''));
				var sourceVal     = parseInt(ReplaceAll(ReplaceAll(txtMin.value,',',''),'$',''));
				//var DwellValue = parseInt(ReplaceAll(txtDwell.value,',',''));
				var DwellValue = parseInt(ReplaceAll(ReplaceAll(txtDwell.value,',',''),'$',''));
			
				//Additional of Mine Subsidence coverage should be coverage A or 200000 
				// whichever is lesser
				if(DwellValue  > 200000)
					DwellValue = 200000;
					
				if( sourceVal > DwellValue)
				{
					objArgs.IsValid = false;
       				return;
				}
				
				objArgs.IsValid = true;
			}
			//Rounds to lower 100
			function CoverageFireArm(objSource, objArgs)
			{
				
				var txtCoverageD = document.getElementById(objSource.controltovalidate);
				var strCoverageD = ReplaceAll(objSource.id,'csv','rev');
				//Get The Regular Expression Validator
				var revCoverageD =document.getElementById(strCoverageD);
				
				if(revCoverageD.isvalid==false)
				   return;
				if ( txtCoverageD.value == '' && txtCoverageD.value == 0)
				{
					objArgs.IsValid = true;
					return;
				}
				var inAmount=ReplaceAll(txtCoverageD.value,',','');
					inAmount=ReplaceAll(inAmount,'$','');
				var factor   =  parseInt(inAmount/100);
				if(factor==0)
				{
				   //txtCoverageD.value='';
				   if (inAmount>0)
					txtCoverageD.value='100';
				   else
				    txtCoverageD.value='';	
				}
				else
				{
				   txtCoverageD.value=factor*100;
				}
				
				if(parseFloat(txtCoverageD.value) > 6000)
				{
				  objArgs.IsValid = false;
				  return
				}
				
				objArgs.IsValid = true;
				
			}
			//FOR MINE SUBSIDENCE
			// for mine Susidence
			function SetMinSubsidence()
			{
			        //Get The Mine Subsidence Coverage (DP-480)
			        var cb= GetControlInGridFromCode('MIN##','_cbDelete');
			        if (cb == null) return;
			        if(cb.checked == false) return;
					var txtMin = GetControlInGridFromCode("MIN##","_txtDEDUCTIBLE_1_TYPE");
					var sourceVal     = ReplaceAll(txtMin.value,',','');
						sourceVal     = ReplaceAll(sourceVal,'$','');
					var lblMin=GetControlInGridFromCode("MIN##","_lblDEDUCTIBLE_AMOUNT");
					var hid=GetControlInGridFromCode('MIN##','_hidlbl_DEDUCTIBLE_AMOUNT')
					var lblLimitMIN= GetControlInGridFromCode("MIN##","_lblLIMIT");
					if(sourceVal== '')
					{
					  lblMin.innerText='';
					  lblLimitMIN.innerText="Not Applicable";//Added by Charles on 4-Sep-09 for Itrack 6296
					  hid.value=''
					  return
					   
					}
					var Min=parseInt(sourceVal) * .02;
					Min=Math.round(Min,0);
					if(parseInt(Min) <= 250)
					{
					  Min=250;
					}
					else if(parseInt(Min) >= 500)
					{
					  Min =500;
					} 
					lblMin.innerText= '2%';//'2% - ' + Min //Changed by Charles on 4-Sep-09 for Itrack 6296
					lblLimitMIN.innerText="Not Applicable";
					hid.value=Min + ' ' ;
					txtMin.value=formatCurrency(sourceVal);					
			}
			//if there is a limit in the Additional Coverages Column then check off 
            //Personal Property Coverage C Increased Limits Away from Premises (HO-50)
			function UpdateHo50()
			{
			  return;
	          /* commented by Pravesh on 1 oct 2007 as pere Itrack Issue 2635 no relation b/w 2
	          var txtEBUSPP = GetControlInGridFromCode("EBUSPP","_txtDEDUCTIBLE_1_TYPE");		
	          if(txtEBUSPP == null)
	               return;
			  var revEBUSPP = GetControlInGridFromCode("EBUSPP","_revLIMIT_DEDUC_AMOUNT");
		      var rngEBUSPP = GetControlInGridFromCode("EBUSPP","_rngDEDUCTIBLE");
              var cbEBPPOP = GetControlInGridFromCode("EBPPOP","_cbDelete");
			  if( cbEBPPOP == null)
			    	return;
			//by pravesh--> In case og HO-6 and HO-4 Ho-50 will be grabted by Default as per Issue no 1025 og Covg.
			 if(document.getElementById('hidPolcyType').value == "HO-4^TENANT" || document.getElementById('hidPolcyType').value == "HO-6^UNIT")
				return;
			//end here	
		      if(revEBUSPP.getAttribute("isValid") == false || rngEBUSPP.getAttribute("isValid")== false )
		        {
		          cbEBPPOP.checked=false; 
		          SetHiddenField(cbEBPPOP.id)
		          DisableItems(cbEBPPOP.id)
		          return;
		        }
		        
		       
		        if(txtEBUSPP.value == '')
		        {
					cbEBPPOP.checked=false;  
					cbEBPPOP.disabled=false;
					DisableItems(cbEBPPOP.id)
					SetHiddenField(cbEBPPOP.id)
		        }
		        else
		        {
					cbEBPPOP.checked=true;   
					cbEBPPOP.disabled=true;
					DisableItems(cbEBPPOP.id)
					SetHiddenField(cbEBPPOP.id)
		        } 
		        */
			}
			//added by pravesh to set addtional value for Ho 50
			//Rounding of HO-50 Coverages additional 
			//Rounds to lower 1000
			function CoverageHO50Validations(objSource, objArgs)
			{
				
				var txtCoverageHO50 = document.getElementById(objSource.controltovalidate);
				var strCoverageHO50 = ReplaceAll(objSource.id,'csv','rev');
				//Get The Regular Expression Validator
				var revCoverageHO50 =document.getElementById(strCoverageHO50);			
				
				if(revCoverageHO50.isvalid==false)
				{
				   return;
				}
				if ( txtCoverageHO50.value == '' && txtCoverageHO50.value == 0)
				{
					objArgs.IsValid = true;
					return;
				}
				var inAmount=ReplaceAll(txtCoverageHO50.value,',','');
					inAmount=ReplaceAll(inAmount,'$','');
					
				var factor   =  parseInt(inAmount/1000);
				if(factor==0)
				{
				   txtCoverageHO50.value=1000;
				 
				}
				else
				{
				   txtCoverageHO50.value=factor*1000;
				}
				
				objArgs.IsValid = true;
				
			}
			//Only available on Policy Type HO-4 Tenants
            //Included Amount=10% of Coverage C 
			function UpdateHo51()
			{
			    var percent="10";
			   
				if(document.getElementById('hidPolcyType').value == "HO-4^TENANT")
				{
				   var txtLimitEBUSPP=GetControlInGridFromCode('EBUSPP',"_txtLIMIT");
				   if( txtLimitEBUSPP == null)
				     {
				      return;
				     }
				   
				   var chkLimitEBBAA=GetControlInGridFromCode('EBBAA',"_cbDelete");
				   
				   if(chkLimitEBBAA.checked == false)  
				     return;
				    //var valueEBUSPP=parseInt(ReplaceAll(txtLimitEBUSPP.value,',',''))
				    var valueEBUSPP=parseInt(ReplaceAll(ReplaceAll(txtLimitEBUSPP.value,',',''),'$',''))
				    var valueEBBAA=valueEBUSPP *parseInt(percent);
				    valueEBBAA=valueEBBAA/100;
				   
				    var lblLimitEBBAA=GetControlInGridFromCode('EBBAA',"_lblLIMIT");
				    lblLimitEBBAA.innerText=valueEBBAA;
				    SetHiddenFieldControls(lblLimitEBBAA.id);
				    lblLimitEBBAA.innerText=formatCurrency(valueEBBAA);
				}
				
			}
			
			function SetHO11()
			{
				var txtLimitDWELL=GetControlInGridFromCode('DWELL',"_txtLIMIT");
				if( txtLimitDWELL == null)
				{
					return;
				}

				//var sourceVal=parseInt(ReplaceAll(txtLimitDWELL.value,',',''))
				var sourceVal=parseInt(ReplaceAll(ReplaceAll(txtLimitDWELL.value,',',''),'$',''))
				var repCost = parseInt(document.getElementById('hidREP_COST').value);
					    
				var cbEBEP11 = GetControlInGridFromCode('EBEP11','_cbDELETE');  
				if(cbEBEP11 == null)
				{
					return;
				}
				
				//If Equal to RepCost 
				if(sourceVal == repCost)
				{	
					cbEBEP11.disabled=false;
					//Added by Charles on 22-Dec-09 for Itrack 6604				
					if(document.getElementById('hidPolcyType').value == "HO-5^REPLACE" || document.getElementById('hidPolcyType').value == "HO-5^PREMIER")
					{
						cbEBEP11.checked=true;
						SetHiddenField(cbEBEP11.id);						
					}//Added till here
				}
				else
				{
					cbEBEP11.disabled=true;
					cbEBEP11.checked=false;
					SetHiddenField(cbEBEP11.id)//Added by Charles on 22-Oct-09 for Itrack 6604
				}
			}
			
			
			//HO-6 Policy Only Add Condominium Deluxe Coverage  Endorsement (HO-66) If H0-66 is checked off they grey out HO-32
			
			function UpdateHO32()
			{
				
				if(document.getElementById('hidPolcyType').value == "HO-6^UNIT")
					{
				
					var cbEBCDC = GetControlInGridFromCode('EBCDC','_cbDELETE'); 
					if(cbEBCDC == null)
					{
						return;
					}
					var cbEBCASP = GetControlInGridFromCode('EBCASP','_cbDELETE'); 
					if(cbEBCASP == null)
					{
						return;
					}
					
					if(cbEBCDC.checked == true)
					{
    					cbEBCASP.disabled=true;
						cbEBCASP.checked=false;
						DisableItems(cbEBCASP.id);
						SetHiddenField(cbEBCASP.id)
					}
					
					else
					{
					  
					   //cbEBCASP.disabled = false;
					   //cbEBCASP.checked=true;
					   cbEBCASP.disabled=false;
					   DisableItems(cbEBCASP.id);
					   SetHiddenField(cbEBCASP.id)
					  //cbEBCASP.parentElement.parentElement.parentElement.disabled = false;
					 // cbEBCASP.disabled = false;
					
					}
				}
			
			
			
			}
		
		
		function UpdateCoverageB()
			{
			
				if(document.getElementById('hidPolcyType').value == 'HO-4^TENANT' || document.getElementById('hidPolcyType').value == 'HO-6^UNIT')
				{
					var txtOS=GetControlInGridFromCode('OS',"_txtLIMIT");
					  
					if(txtOS == null) return
				    
					var txtEBUSPP=GetControlInGridFromCode('EBUSPP',"_txtLIMIT");
					if(txtEBUSPP != null)
					{
						var amount=ReplaceAll(txtEBUSPP.value,',','');
							amount=ReplaceAll(amount,'$','');
						txtOS.value= parseInt(amount) * .1;
						txtOS.value=formatCurrency(txtOS.value);   
					}
				}
			}
			function SetCondominiumDelux()
			{
		
		
				if(document.getElementById('hidPolcyType').value == "HO-6^UNIT")
					{
						var txtLimitEBUSPP=GetControlInGridFromCode('EBUSPP',"_txtLIMIT");
						if( txtLimitEBUSPP == null)
							{
							return;
							}
						   
					    
						//var sourceVal=parseInt(ReplaceAll(txtLimitEBUSPP.value,',',''))
						var sourceVal=parseInt(ReplaceAll(ReplaceAll(txtLimitEBUSPP.value,',',''),'$',''))					    
						var cbEBCDC = GetControlInGridFromCode('EBCDC','_cbDELETE');  
						if(cbEBCDC == null)
						{
						return;
						}
						if(parseInt(sourceVal) >=25000)
						{	
							cbEBCDC.disabled=false;
							
						}
						else
						{
							cbEBCDC.disabled=true;
							cbEBCDC.checked=false;
							SetHiddenField(cbEBCDC.id)
							var cbEBCASP = GetControlInGridFromCode('EBCASP','_cbDELETE'); 
							if(cbEBCASP != null)
							{
								cbEBCASP.disabled=false;
							}
						}
						document.getElementById(txtLimitEBUSPP.id).setAttribute("readOnly",true);
				}
			
			}
			
			
			
			
			/*HO-3 Repalcement, Indiana:
            Premier V.I.P.(HO-24)" is displayed as disbaled even 
            if Covg. A >=75,000. The coverage should be enabled if covg. A >=75,000.
            */
			function PremierVIP24()
			{
			
			 if(document.Form1.hidProduct.value != "11148") return;
			   //Get The Coverage A 
    			var txtDwell= GetControlInGridFromCode("DWELL","_txtLIMIT");
				
				if( txtDwell == null)
				    return;
				var cbEBP24 = GetControlInGridFromCode("EBP24","_cbDelete");   
				
				if (cbEBP24 != null)
				{
				  var amount = txtDwell.value
				  var sourceVal     = ReplaceAll(amount,',','');
					  sourceVal     = ReplaceAll(sourceVal,'$','');
				  if(parseInt(sourceVal) >= 75000)
				   {
				     cbEBP24.disabled=false;
				     cbEBP24.parentElement.disabled=false;
				   }
				   else
				   {
				    cbEBP24.disabled=true;
				    cbEBP24.checked=false;
				    SetHiddenField(cbEBP24.id)
				   }
				
				}
			}


			function SetRentalDelux() { 
				var sourceCov='EBUSPP' ;
				var source = GetControlInGridFromCode(sourceCov,'_txtLIMIT');  
				var out = ',';
				var add = ''; 
				var temp = '' + source.value;
				while 
				(
					temp.indexOf(out)>-1){ pos= temp.indexOf(out);
					temp = '' + (temp.substring(0, pos)+ add + temp.substring((pos + out.length), temp.length));
				}
				var sourceVal=temp;
				var cbRental = GetControlInGridFromCode('EBRDC','_cbDELETE');  
				var hidRental =GetControlInGridFromCode('EBRDC','_hidCbDelete');
				if(cbRental == null) return;
				if(parseInt(sourceVal) >=25000)
				{	
					cbRental.disabled=false;
					
				}
				else
				{
					cbRental.disabled=true;
					cbRental.checked=false;
					DisableItems(cbRental.id);
					SetHiddenField(cbRental.id);
				}
				if(document.getElementById('hidPolcyType').value == "HO-4^TENANT")
				{
				document.getElementById(source.id).setAttribute("readOnly",true);
				}
				
			}
			

			
			function DisableItems(strcbDelete)
			{
				
				//alert('hi');
				var lastIndex = strcbDelete.lastIndexOf('_');
				var prefix = strcbDelete.substring(0,lastIndex);
				
				//alert(strcbDelete);
				strlblNoLimit = prefix + '_lblNoCoverageLimit';
				lblNoLimit = document.getElementById(strlblNoLimit);
				
				//May not be relevant in some cases
				strddlDed = prefix + '_ddlDEDUCTIBLE';
				ddlDed = document.getElementById(strddlDed);
				
				strlblLimit = prefix + '_lblLimit';
				lblLimit = document.getElementById(strlblLimit);
				
				strlblNoDeductible = prefix + '_lblNoCoverage';
				lblNoDeductible = document.getElementById(strlblNoDeductible);	
				
				
				cbDelete = document.getElementById(strcbDelete);
				
				strtxtbox = prefix + '_txtDEDUCTIBLE_1_TYPE';
				txtDed = document.getElementById(strtxtbox);
				
				strtxtLIMIT = prefix + '_txtLIMIT';		
				txtLIMIT = document.getElementById(strtxtLIMIT);
				//alert(txtLIMIT);
				
				strDedApplyName = prefix + '_lblIS_DEDUCT_APPLICABLE';
				strDedType = prefix + '_lblDEDUCTIBLE_TYPE';
				strLimitType = prefix + '_lblLIMIT_TYPE';
				strddlLIMIT = prefix + '_ddlLIMIT';
				ddlLimit = document.getElementById(strddlLIMIT);
				strlblNoCoverageLimit = prefix + '_lblNoCoverageLimit';
				
				var strhidDED = prefix + '_hidDEDUCTIBLE'
				var hidDEDUCTIBLE= document.getElementById(strhidDED); 
				hidDEDUCTIBLE.value='';
				
				strlblDEDUCTIBLE = prefix + '_lblDEDUCTIBLE';
				lblDEDUCTIBLE = document.getElementById(strlblDEDUCTIBLE); 
				
				
				
				DedType = document.getElementById(strDedType).innerText;
				LimitType = document.getElementById(strLimitType).innerText;
				lblNoCoverageLimit = document.getElementById(strlblNoCoverageLimit);
				
				strNoaddDEDUCTIBLE=prefix + '_lblNoaddDEDUCTIBLE';
				strddlAddDed=prefix + '_ddladdDEDUCTIBLE'
				strlblDEDUCTIBLE_AMOUNT = prefix + '_lblDEDUCTIBLE_AMOUNT';
				strspnDEDUCTIBLE_AMOUNT_TEXT = prefix + '_spnDEDUCTIBLE_AMOUNT_TEXT';
				strtxtaddDEDUCTIBLE          = prefix  + '_txtaddDEDUCTIBLE'
				strhidlbl_DEDUCTIBLE_AMOUNT  = prefix  + '_hidlbl_DEDUCTIBLE_AMOUNT'
				
				lblNoAddded=document.getElementById(strNoaddDEDUCTIBLE);
				lblDEDUCTIBLE_AMOUNT=document.getElementById(strlblDEDUCTIBLE_AMOUNT);
				ddlAddDed=document.getElementById(strddlAddDed);
				txtAddDed=document.getElementById(strtxtaddDEDUCTIBLE);
				strAdddedType= prefix + '_lblAddDEDUCTIBLE_TYPE';
				
				hidlbl_DEDUCTIBLE_AMOUNT =  document.getElementById(strhidlbl_DEDUCTIBLE_AMOUNT);
				spnDEDUCTIBLE_AMOUNT_TEXT =  document.getElementById(strspnDEDUCTIBLE_AMOUNT_TEXT);
				AddDedType=document.getElementById(strAdddedType).innerText 
			
				
				var span = cbDelete.parentElement;
				var covCode = '';
				var product = document.Form1.hidProduct.value;
									
				if ( span != null )
				{
					covCode = span.getAttribute("COV_CODE");
				}
				
				if(covCode== "EBUSPP")
				{
					if(firstTime==true)
					{
					 SetCondominiumDelux();
					 SetHO11();
					}
				}
				
				//EBCCSL, EBCCSM, ESCCSS, EBCCSI
				//alert(DedType);
				
				if ( cbDelete.checked == true )
				{
					lblNoLimit.style.display = "none";
					
					
					if ( lblLimit != null )
					{
						lblLimit.style.display = "inline";
					}
					
					lblNoDeductible.style.display = "none";
					//lblNoDeductible.innerText = 'No Coverages';
					
					if(ddlDed != null)
					{
						ddlDed.style.display = "none";
					}
					
					
					//---------Deductible type
					switch(DedType)
					{
						case '0':
							
							lblNoDeductible.style.display = "none";
							
						   
						   
							
							if(ddlDed != null)
							{
								ddlDed.style.display = "none";
							}
							
							txtDed.style.display = "none";
							//DisableDeds(prefix);
							break;
						case '1':
							//Flat
								
								//alert(covCode);
								//alert(product);
								//Ho-3 both states, HO-3 Premier
								if(ddlDed != null)
								  ddlDed.style.display = "inline";
								
									//Depends om other coverages
									
									if ( covCode == "EBCCSL" || covCode == "EBCCSM" || covCode == "EBCCSI")
									{	
										var cbEBP25 = GetControlInGridFromCode('EBP25','_cbDelete');
										var cbEBP24 = GetControlInGridFromCode('EBP24','_cbDelete');
										var cbEBP23 = GetControlInGridFromCode('EBP23','_cbDelete');
										
										if ( cbEBP25 != null )
										{
											UpdateSpecialLimits(cbEBP25.checked);
										}
										
										if ( cbEBP24 != null )
										{
											UpdateSpecialLimits(cbEBP24.checked);
										}
										
										if ( cbEBP23 != null )
										{
											UpdateSpecialLimits(cbEBP23.checked);
										}
											
									}
									else
									{
										lblNoDeductible.style.display = "none";
										lblNoDeductible.innerText = 'N.A';
										
										
										if(ddlDed != null)
										{		
											ddlDed.style.display = "inline";		
										}
									}	
							
								txtDed.style.display = "none";
								break;
						case '2':
						
							//Split
							if ( ddlDed.options.length == 0 )
							{
								lblNoDeductible.style.display = "inline";
								lblNoDeductible.innerText = 'N.A';
								if(ddlDed != null)
								{	
									ddlDed.style.display = "none";
								}
							}
							else
							{
								lblNoDeductible.style.display = "none";
								lblNoDeductible.innerText = 'N.A';
								ddlDed.style.display = "inline";
								
							}
							txtDed.style.display = "none";
							break;
						case '3':
							//alert(txtDed.style.display);
							//Open
								lblNoDeductible.style.display = "none";
								if ( covCode == "ECOB" || covCode == "ECOC") 
								{
									
								}
								else
								{
									txtDed.style.display = "inline";
									lblNoDeductible.style.display = "none";
								}
								
								if(ddlDed != null)
								{
									ddlDed.style.display = "none";
								}
								
								txtDed.style.display = "inline";
								//ValidatorEnable(revDeductible,true);
							break;
					}
					//----------------------------------------------------------------
					
					
					
					//Limit /////////////////////////////
					switch(LimitType)
					{
						
						case '0':
							//lblNoLimit.style.display = "inline";
							//lblNoLimit.innerText = 'No Coverages';
							//alert(LimitType);
							lblNoCoverageLimit.style.display = "none";
							
							if ( lblLimit != null )
							{
								lblLimit.style.display = "inline";
							}
							
							if ( ddlLimit != null )
							{
								ddlLimit.style.display = "none";
							}
							
							//DisableDeds(prefix);
							break;
						case '1':
							//Flat
							lblNoLimit.style.display = "none";
							ddlLimit.style.display = "inline";
							
							if ( lblLimit != null )
							{
								lblLimit.style.display = "none";	
							}
							
							lblNoCoverageLimit.style.display = "none";
							
							break;
						case '2':
							//Split
							if ( ddlDed.options.length == 0 )
							{
								lblNoDeductible.style.display = "inline";
								lblNoDeductible.innerText = 'N.A';
								
								if(ddlDed != null)
								{
									ddlDed.style.display = "none";
								}
							}
							else
							{
								lblNoDeductible.style.display = "none";
								lblNoDeductible.innerText = 'N.A';
								if(ddlDed != null)
								{
									ddlDed.style.display = "inline";
								}
								txtDed.style.display = "none";
							}
							
							break;
						case '3':
								//Open
								//lblNoDeductible.style.display = "none";
								lblNoLimit.style.display = "none";
								
								if ( txtLIMIT != null )
								{
									txtLIMIT.style.display = "inline";
								}
								//ddlLimit.style.display = "none";
									
								//ddlDed.style.display = "none";
								//txtDed.style.display = "inline";
								//ValidatorEnable(revDeductible,true);
							break;
					}
					switch(AddDedType)
					{
						case '0':
							lblNoAddded.style.display = "inline";
							lblNoAddded.innerText = 'No Coverages';
							if(ddlAddDed != null)
							{
							 ddlAddDed.style.display = "none";
							}
							
							//DisableDeds(prefix);
							break;
						case '1':
							//Flat
							lblNoAddded.style.display = "none"; 
							ddlAddDed.style.display = "inline";
							break;
						case '4':
						 if ( covCode == "ECOB" || covCode =="ECOC") 
							{
							 
								lblDEDUCTIBLE_AMOUNT.innerText="5%-250";
								lblDEDUCTIBLE_AMOUNT.style.display="inline";
								hidlbl_DEDUCTIBLE_AMOUNT.value="5 %-250";
							}
							if ( covCode == "EROK" && document.getElementById('hidStateId').value == "22") //Added by Charles on 9-Dec-09 for Itrack 6647
							{							 
								lblDEDUCTIBLE_AMOUNT.innerText="10% - 250";
								lblDEDUCTIBLE_AMOUNT.style.display="inline";
								hidlbl_DEDUCTIBLE_AMOUNT.value="10 %-250";
							}//Added till here
						     lblNoAddded.style.display = "none";
						     if(ddlAddDed != null)
							{
							 ddlAddDed.style.display = "none";
							}
						     break;
						case '2':
							//Split
							if ( ddlAddDed.options.length == 0 )
							{
								lblNoAddded.style.display = "inline";
								lblNoAddded.innerText = 'N.A';
								ddlAddDed.style.display = "none";
							}
							else
							{
								lblNoAddded.style.display = "none";
								lblNoAddded.innerText = 'N.A';
								ddlAddDed.style.display = "inline";
								
							}
						
							break;
						case '3':
							//Open
								lblNoAddded.style.display = "none";
								txtAddDed.style.display = "inline";
							if(ddlAddDed != null)
							{
							 ddlAddDed.style.display = "none";
							}
								//ValidatorEnable(revDeductible,true);
							break;
					}
					//----------------------------------------------------------------		
								
				}
				else
				{
					lblNoLimit.style.display = "inline";
					lblNoLimit.innerText = 'No Coverages';
				  	if ( lblDEDUCTIBLE != null) 
					{
					    lblDEDUCTIBLE.style.display="none";
					}
					if ( lblLimit != null )
					{
						lblLimit.style.display = "none";
					}
					
					lblNoDeductible.style.display = "inline";
					lblNoDeductible.innerText = 'No Coverages';
					
					if ( lblLimit != null )
					{
						lblLimit.style.display = "none";
					}
					
					txtDed.style.display = "none";
					
					if(ddlDed != null)
					{
						ddlDed.style.display="none";
					}
					
					if ( ddlLimit != null )
					{
						ddlLimit.style.display = "none";
					}
					
					if ( txtLIMIT != null )
					{
						txtLIMIT.style.display = "none";
					}
					lblNoAddded.style.display = "inline";
					lblNoAddded.innerText = 'No Coverages';
					if(lblDEDUCTIBLE_AMOUNT != null)
					{
					  lblDEDUCTIBLE_AMOUNT.style.display = "none";
					}
					if(txtAddDed != null)
					{
					  txtAddDed.style.display = "none";
					}
					if(ddlAddDed != null)
					{
					   ddlAddDed.style.display="none";
					}   
					
							
				}
				//End of deductible
								
			}//End of function
			
			
			function Initialize()
			{
				document.getElementById("btnDelete").disabled = true;
			}
			
		/*	function Reset()
			{
			ChangeColor();
			DisableValidators();
				document.Form1.reset();
				return false;
			} */
			var noOfRowsSelected = 0;
			
			
			
			
			
			/*
			function GetCheckBoxID(intCovId)
			{
				var ctr = 0;
				
				var rowCount = document.Form1.hidROW_COUNT.value;
				
				for (ctr = 2; ctr < rowCount; ctr++)
				{
					var label = document.getElementById(prefix + ctr + "_lblCOV_ID");
					
					if ( label != null )
					{
						if ( label.innerText == intCovId )
						{
							
							var cb = document.getElementById(prefix + ctr + "_cbDelete");
							
							if( cb != null )
							{
								return cb.id;
							}
						
						}
					}
				}
			}
			
		*/
			
			
		
			
			
			
			
			function InitInstance()
			{
				var ctr = 0;
				for (ctr = 2; ctr<10; ctr++)
				{
					chk = document.getElementById(prefix + ctr + "_cbDelete");
					if (chk != null)
					{
						OnClickCheck(ctr);
					}
					else
					{
						break;
					}
				}
			}
			function LocationPolicy() {
				//alert(self.parent.location )
            self.parent.location = '/cms/policies/aspx/policytab.aspx?customer_id=' + document.Form1.hidCustomerID.value + '&policy_id=' + document.Form1.hidPOLID.value + '&policy_version_id=' + document.Form1.hidPOLVersionID.value + '&app_version_id=' + document.Form1.hidAppVersionID.value + '&app_id=' + document.Form1.hidPOLID.value + '&'; 
			}
			function LocationLimit()
			{
				self.location ='cms/policies/aspx/homeowner/PolicyAddDwellingDetails.aspx?DWELLING_ID=' + document.Form1.hidREC_VEH_ID.value + '&CalledFrom= Home&';
				changetab(4,0);
			}
			
			//Gets the checkbox with the passed in code
			//Gets the checkbox with the passed in code
			/*
			function GetDeductibleNoCoverageFromCode(covCode)
			{
				//lblNoCoverage
				
						
				 var chk = GetControlInGridFromCode(covCode,"_cbDelete");	
				 var lbl = GetControlInGridFromCode(covCode,"_lblNoCoverage");		
				
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
				
				return null;
			}
			function GetLabelFromCode(covCode)
			{
			     
				 var chk = GetControlInGridFromCode(covCode,"_cbDelete");	
				 var lbl = GetControlInGridFromCode(covCode,"_lblLIMIT");		
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
			
				return null;
						
			}
			
			//Gets the checkbox with the passed in code
			function GetIncludedDDLFromCode(covCode)
			{
				
						
			
				 var chk = GetControlInGridFromCode(covCode,"_cbDelete");	
				 var ddl = GetControlInGridFromCode(covCode,"_ddlLIMIT");
					
					if ( chk != null )
					{
						var span = chk.parentElement;
									
						if ( span != null )
						{
							var coverageCode = span.getAttribute("COV_CODE");
					
							if ( trim(covCode) == trim(coverageCode) )
							{
								return ddl;
							}
						}
					}
				
				return null;
						
			}
			*/
			
			
			//Upadtes the Special Limits for Jewelry, Furs, Money etc etc on the basis of HO-20,HO-21 , etc. etc. 
			function UpdateSpecialLimitsFromHO22(checked)
			{
					//if ( firstTime == true ) return;
					
					//Get the labels for Money, Jewelry, Firearms and Silverware////////////////
					//Get the labels for Money, Jewelry, Firearms and Silverware////////////////
					var lblJewelry     = GetControlInGridFromCode('EBCCSL',"_lblLIMIT");	
					var lblMoney       = GetControlInGridFromCode('EBCCSM',"_lblLIMIT");	
					var lblFirearms    = GetControlInGridFromCode('EBCCSF',"_lblLIMIT");	
					var lblSilver      = GetControlInGridFromCode('EBCCSI',"_lblLIMIT");	
					/*
					var ddlJewelry     = GetControlInGridFromCode('EBCCSL',"_ddlDEDUCTIBLE");
					var ddlMoney       = GetControlInGridFromCode('EBCCSM',"_ddlDEDUCTIBLE");
					var ddlFirearms    = GetControlInGridFromCode('EBCCSM',"_ddlDEDUCTIBLE");
					var ddlSilver      = GetControlInGridFromCode('EBCCSI',"_ddlDEDUCTIBLE");
					*/
					var lblNoJewelry   = GetControlInGridFromCode('EBCCSL',"_lblNoCoverage");
					var lblNoMoney     = GetControlInGridFromCode('EBCCSM',"_lblNoCoverage");
					var lblNoFirearms  = GetControlInGridFromCode('EBCCSF',"_lblNoCoverage");
					var lblNoSilver    = GetControlInGridFromCode('EBCCSI',"_lblNoCoverage");
					////////////////////////////
					
					if ( checked == true )
					{
						if ( lblJewelry != null )
						{
							lblJewelry.innerText = '2,500';
							SetHiddenFieldControls(lblJewelry.id);
						}
						
					/*	if ( ddlJewelry != null)
						{
							ddlJewelry.style.display = "none";
							
						}
					*/	
						if ( lblNoJewelry != null)
						{
							lblNoJewelry.style.display = "none";
						}
						////////////////////////////////
						
						if ( lblMoney != null )
						{
							lblMoney.innerText = '500';
							SetHiddenFieldControls(lblMoney.id);
						}
						
					/*	if ( ddlMoney != null)
						{
							ddlMoney.style.display = "none";
							ddlMoney.options.selectedIndex = 0;
						}
					*/	
						
						if ( lblNoMoney != null)
						{
							lblNoMoney.style.display = "none";
						}
						////////////////////
						
						if ( lblFirearms != null )
						{
							lblFirearms.innerText = '2,500';
							SetHiddenFieldControls(lblFirearms.id);
						}
						
					
					}
					else
					{
						if ( lblJewelry != null )
						{
							lblJewelry.innerText = '1,000';
							SetHiddenFieldControls(lblJewelry.id);
						}
						
					/*	if ( ddlJewelry != null)
						{
							ddlJewelry.style.display = "inline";
						}
					*/	
						if ( lblNoJewelry != null)
						{
							lblNoJewelry.style.display = "none";
						}
						
						//////////
							
						if ( lblMoney != null )
						{
							lblMoney.innerText = '200';
							SetHiddenFieldControls(lblMoney.id);
						}
						
					/*	if ( ddlMoney != null)
						{
							ddlMoney.style.display = "inline";
						}
					*/	
						if ( lblNoMoney != null)
						{
							lblNoMoney.style.display = "none";
						}
						
						////////////////
						
						if ( lblFirearms != null )
						{
							lblFirearms.innerText = '2,000';
							SetHiddenFieldControls(lblFirearms.id);
						}
						
					
						
						if ( lblSilver != null )
						{
							lblSilver.innerText = '2,500';
							SetHiddenFieldControls(lblSilver.id);
						}
						
					/*	if ( ddlSilver != null)
						{
							ddlSilver.style.display = "inline";
						}
					*/	
						if ( lblNoSilver != null)
						{
							lblNoSilver.style.display = "none";
						}
						
						//////////
					}
					
			}//End of UpdateSpecialLimitsFromHO22
			//Gets the checkbox with the passed in code
			
			
			//Upadtes the Special Limits for Jewelry, Furs, Money etc etc on the basis of HO-20,HO-21 , etc. etc. 
			function UpdateSpecialLimits(checked)
			{
					//if ( firstTime == true ) return;
					
					//Get the labels for Money, Jewelry, Firearms and Silverware////////////////
					
					var lblJewelry  = GetControlInGridFromCode('EBCCSL',"_lblLIMIT");	
					var lblMoney    = GetControlInGridFromCode('EBCCSM',"_lblLIMIT");	
					var lblFirearms = GetControlInGridFromCode('EBCCSF',"_lblLIMIT");	
					var lblSilver   = GetControlInGridFromCode('EBCCSI',"_lblLIMIT");
					var lblEBP25       = GetControlInGridFromCode('EBP25',"_lblLIMIT");		
					/*
					var ddlJewelry  = GetControlInGridFromCode('EBCCSL',"_ddlDEDUCTIBLE");
					var ddlMoney    = GetControlInGridFromCode('EBCCSM',"_ddlDEDUCTIBLE");
					var ddlFirearms = GetControlInGridFromCode('EBCCSM',"_ddlDEDUCTIBLE");
					var ddlSilver   = GetControlInGridFromCode('EBCCSI',"_ddlDEDUCTIBLE");
					*/
					var lblNoJewelry   = GetControlInGridFromCode('EBCCSL',"_lblNoCoverage");
					var lblNoMoney     = GetControlInGridFromCode('EBCCSM',"_lblNoCoverage");
					var lblNoFirearms  = GetControlInGridFromCode('EBCCSF',"_lblNoCoverage");
					var lblNoSilver    = GetControlInGridFromCode('EBCCSI',"_lblNoCoverage");
					////////////////////////////
					
					if ( checked == true )
					{
					    
						/*if ( lblJewelry != null )
						{
							lblJewelry.innerText = '10,000';
							SetHiddenFieldControls(lblJewelry.id);
						}*/
						
					/*	if ( ddlJewelry != null)
						{
							ddlJewelry.style.display = "none";
							
						}*/
						if ( lblNoJewelry != null)
						{
							lblNoJewelry.style.display = "none";
						}
						////////////////////////////////
						
						if ( lblMoney != null )
						{
							lblMoney.innerText = '1,000';
							SetHiddenFieldControls(lblMoney.id);
						}
						
					/*	if ( ddlMoney != null)
						{
							ddlMoney.style.display = "none";
							ddlMoney.options.selectedIndex = 0;
						}
					*/	
						if ( lblNoMoney != null)
						{
							lblNoMoney.style.display = "none";
						}
						////////////////////
						
						if ( lblFirearms != null )
						{
							lblFirearms.innerText = '2,500';
							SetHiddenFieldControls(lblFirearms.id);
						}
						
						
						//////////////////
						/*
						if ( lblSilver != null )
						{
							lblSilver.innerText = '10,000';
							SetHiddenFieldControls(lblSilver.id);
						}*/
						
					/*	if ( ddlSilver != null)
						{
							ddlSilver.style.display = "none";
							ddlSilver.options.selectedIndex = 0;
						}
					*/	
						if ( lblNoSilver != null)
						{
							lblNoSilver.style.display = "none";
						}
						
					}
					else
					{
						if ( lblJewelry != null )
						{
							lblJewelry.innerText = '1,000';
							SetHiddenFieldControls(lblJewelry.id);
						}
						
					/*	if ( ddlJewelry != null)
						{
							ddlJewelry.style.display = "inline";
						}
					*/	
						if ( lblNoJewelry != null)
						{
							lblNoJewelry.style.display = "none";
						}
						
						//////////
							
						if ( lblMoney != null )
						{
							lblMoney.innerText = '200';
							SetHiddenFieldControls(lblMoney.id);
						}
						
					/*	if ( ddlMoney != null)
						{
							ddlMoney.style.display = "inline";
						}
					*/	
						if ( lblNoMoney != null)
						{
							lblNoMoney.style.display = "none";
						}
						
						////////////////
						
						if ( lblFirearms != null )
						{
							lblFirearms.innerText = '2,000';
							SetHiddenFieldControls(lblFirearms.id);
						}
						
					
						
						if ( lblSilver != null )
						{
							lblSilver.innerText = '2,500';
							SetHiddenFieldControls(lblSilver.id);
						}
						
					/*	if ( ddlSilver != null)
						{
							ddlSilver.style.display = "inline";
						}
					*/	
						if ( lblNoSilver != null)
						{
							lblNoSilver.style.display = "none";
						}
						
						//////////
					}
					
			}//End of UpdateSpecialLimits***********************************************
			
			
			//Executes on the check and uncheck of the check boxes
			function onCheck(CheckBoxID) {	
     			var cb = document.getElementById(CheckBoxID);
				var span = cb.parentElement;
				
				if ( span == null ) return;
				
				var covID = span.getAttribute("COV_ID");
				var covCode = span.getAttribute("COV_CODE");				
				var product = document.Form1.hidProduct.value;
				
				if (covCode == "ECOB")//Added by Charles on 9-Sep-09 for Itrack 6328
				{
				  
				  var cbECOC=GetControlInGridFromCode('ECOC',"_cbDelete");
					if(cbECOC != null)
					 {
						if(cb.checked == true)
						{							
							cbECOC.disabled=false;
						}
						else
						{
							cbECOC.checked=false;
							cbECOC.disabled=true;
							SetHiddenField(cbECOC.id); 
							DisableItems(cbECOC.id);
						}	
						
					}
				}//Added till here
				
				if (covCode == "EBBAA")
				{
				  UpdateHo51();
				}  
				if(covCode == "EBCDC")
				{
				  UpdateHO32();
				}
				//Ravindra(03-27-2007) Coverage B will be available in HO-4 and HO-6 
				//with Limit not applicable
				/*
				if(covCode == "OS")
				{
				  UpdateCoverageB();
				}*/
				
				
				/*The Reduction in Limit - Coverage C  should be displayed as disabled only if HO-34 or HO-42 
				coverage is selected otherwise the coverage should be displayed as enabled
				
				*/
				SetHiddenField(cb.id)
				
				  if(cb.checked == true)
				  {  
					      if(covCode== "IOPSS" || covCode == "IOPSI" || covCode == "EBRCPP")
								{
									var cbREDUC=GetControlInGridFromCode('REDUC',"_cbDelete");
									if(cbREDUC != null)
									{
										cbREDUC.checked=false;
										SetHiddenField(cbREDUC.id)
										cbREDUC.disabled=true;
										DisableItems(cbREDUC.id)
								     
									}
								}
				  }
				  else
				  {
						var cbIOPSS = GetControlInGridFromCode("IOPSS","_cbDelete");
						var flagREDUC="0";
						if(cbIOPSS != null)
							{
							if (cbIOPSS.checked==true)
							{
								flagREDUC="1";
							}
						    
							}  
						    
						var cbIOPSI = GetControlInGridFromCode("IOPSI","_cbDelete");
						if(cbIOPSI != null)
							{
							if (cbIOPSI.checked==true)
							{
								flagREDUC="1";
							}
						    
							}  
						var cbEBRCPP= GetControlInGridFromCode("EBRCPP","_cbDelete");
						if(cbEBRCPP != null)
							{
							if (cbEBRCPP.checked==true)
							{
								flagREDUC="1";
							}
						    
							}  
						var cbREDUC=GetControlInGridFromCode('REDUC',"_cbDelete");
						   
						if(cbREDUC != null)
						{
							if(flagREDUC == "1")
							{   
								cbREDUC.checked=false;
								SetHiddenField(cbREDUC.id)
								cbREDUC.disabled=true;
							}
							else
							{
								cbREDUC.disabled=false;
								DisableItems(cbREDUC.id)
							}   
						}
				  
				  }
				if (covCode == "IBUSPOA")
				{
			
				 if ( cb.checked == true )
					{
						var cbIBUSPA = GetControlInGridFromCode("IBUSPA","_cbDelete");
						
						if ( cbIBUSPA != null)
						  {
							if ( cbIBUSPA.checked == true )
							{
							    var ddlIBUSPA = GetControlInGridFromCode("IBUSPA","_ddlDEDUCTIBLE");
							    if (ddlIBUSPA != null)
							      OnDDLChange(ddlIBUSPA)   
							}
							else
							{
								var txtIBUSPOA = GetControlInGridFromCode("IBUSPOA","_txtDEDUCTIBLE_1_TYPE");
								txtIBUSPOA.value=''; 
							} 
							
						}
					}
					
				
				}
				
				if(covCode == "IBUSPA")
				 {
				   if (cb.checked == false)
				   {
				    var txtIBUSPOA = GetControlInGridFromCode("IBUSPOA","_txtDEDUCTIBLE_1_TYPE");
					txtIBUSPOA.value=''; 
				   }
				   else
					{
						 var ddlIBUSPA = GetControlInGridFromCode("IBUSPA","_ddlDEDUCTIBLE");
							    if (ddlIBUSPA != null)
							      OnDDLChange(ddlIBUSPA)   
					}
				 }
				
				
				
				
				/*
				//HO-65 or (HO-211 for HO-5) Coverage C Increased Special Limits - Unscheduled Jewelry & Furs 
				if ( covCode == "EBCCSL" || covCode == "EBCCSM" || covCode == "ESCCSS" || covCode == "EBCCSI")
				{
					alert(product);
					if ( product == '11400' || product == '11409' || product == '11148' )
					{
						//Get whether the relevant coverage is checked
						//If checked , make dropdown invisible for max limits
						//Premier V.I.P.(HO-24) HO-3 EBP24
						//Premier V.I.P. (HO-25) Ho-3 Premier EBP25
						var cbHO23 = GetControlInGridFromCode('EBP24');
						var cbHO25 = GetControlInGridFromCode('EBP24');
						
						//Get the relevant additinal DDL
						if ( cbHO23 != null)
						{
							//UpdateSpecialLimits(cbHO23.checked);
						}
						
						if ( cbHO25 != null)
						{
							//UpdateSpecialLimits(cbHO25.checked);
						}
						
					}
				}
				*/
				
				//Preferred Plus V.I.P.(HO-22) 
				if ( covCode == "EBP21")
				{
					//Get the others
					//var cb21 = GetControlInGridFromCode("EBP20");
					var cb20 = GetControlInGridFromCode("EBP20",'_cbDelete');
					var cb23 = GetControlInGridFromCode("EBP23",'_cbDelete');
					var cb24 = GetControlInGridFromCode("EBP24",'_cbDelete');
					var cb25 = GetControlInGridFromCode("EBP25",'_cbDelete');
						
					if ( cb.checked == true )
					{
						UpdateSpecialLimitsFromHO22(true);	//Added by Charles on 4-Sep-09 for Itrack 6357
						//alert('in 21');
						if ( cb20 != null )
						{	
							
							cb20.checked = false;
							cb20.parentElement.parentElement.parentElement.disabled = true;
							cb20.disabled = true;
							//alert(cb20.disabled);
							DisableItems(cb20.id);		
						}
									
					}
					else
					{
						//if ( firstTime == false )
						//{
							UpdateSpecialLimitsFromHO22(false);
						//}
						
						if ( cb20 != null )
						{	
							//cb21.checked = false;
							cb20.parentElement.parentElement.parentElement.disabled = false;
							cb20.disabled = false;
							DisableItems(cb20.id);		
						}
						
					}
					
				}//End of HO-21
				//MINE SUBSIDENCE
				if (covCode == 'MIN##')
				{
				  SetMinSubsidence();
				
				  if(cb.checked == false)
					{  
						var validat =GetControlInGridFromCode(covCode,'_revLIMIT_DEDUC_AMOUNT');
						validat.setAttribute('enabled',false); 
						validat.style.display='none'; 
						var validat =GetControlInGridFromCode(covCode,'_csvLIMIT_DEDUC_AMOUNT');
						validat.setAttribute('enabled',false); 
						validat.style.display='none'; 
					}
					else
					{
						var validat =GetControlInGridFromCode(covCode,'_revLIMIT_DEDUC_AMOUNT');
						validat.setAttribute('enabled',true); 
						var validat =GetControlInGridFromCode(covCode,'_csvLIMIT_DEDUC_AMOUNT');
						validat.setAttribute('enabled',true); 
					
						//Added by Charles on 4-Sep-09 for Itrack 6296						
						if ( firstTime == false )
						{
							 var txtMin = GetControlInGridFromCode("MIN##","_txtDEDUCTIBLE_1_TYPE");
							 var txtDwell= GetControlInGridFromCode("DWELL","_txtLIMIT");
							 var sourceVal = parseInt(ReplaceAll(ReplaceAll(txtMin.value,'$',''),',',''));
							 var DwellValue = parseInt(ReplaceAll(ReplaceAll(txtDwell.value,'$',''),',',''));
							 var lblMin=GetControlInGridFromCode("MIN##","_lblDEDUCTIBLE_AMOUNT");
							 var hid=GetControlInGridFromCode('MIN##','_hidlbl_DEDUCTIBLE_AMOUNT');
							 
							 if(DwellValue  > 200000)
								sourceVal = 200000;									
							 else 
								sourceVal = DwellValue;
								
							var Min=parseInt(sourceVal) * .02;
							Min=Math.round(Min,0);
							if(parseInt(Min) <= 250)
							{
							Min=250;
							}
							else if(parseInt(Min) >= 500)
							{
							Min =500;
							} 
							lblMin.innerText= '2%';	
							lblMin.style.display='inline';						
							hid.value=Min + ' ' ;							
							txtMin.value=formatCurrency(sourceVal);
						}
						//Added till here
					}
				}
				//Preferred Plus V.I.P.(HO-22) 
				if ( covCode == "EBP22")
				{
					//Get the others
					//var cb21 = GetControlInGridFromCode("EBP20");
					var cb20 = GetControlInGridFromCode("EBP20",'_cbDelete');
					var cb23 = GetControlInGridFromCode("EBP23",'_cbDelete');
					var cb24 = GetControlInGridFromCode("EBP24",'_cbDelete');
					var cb25 = GetControlInGridFromCode("EBP25",'_cbDelete');
					var ddlEBCCSL  = GetControlInGridFromCode("EBCCSL","_ddlDEDUCTIBLE");
					var ddlEBCCSM  = GetControlInGridFromCode("EBCCSM","_ddlDEDUCTIBLE");
						
					if ( cb.checked == true )
					{
						//if ( firstTime == false )
					//	{
							UpdateSpecialLimitsFromHO22(true);
							if ( ddlEBCCSL != null)
							{
							  OnDDLChange(ddlEBCCSL)   
							}
							if ( ddlEBCCSM != null)
							{
							  OnDDLChange(ddlEBCCSM)   
							}
					//	}
						//alert('in 21');
						if ( cb20 != null )
						{	
							
							cb20.checked = false;
							cb20.parentElement.parentElement.parentElement.disabled = true;
							cb20.disabled = true;
							//alert(cb20.disabled);
							DisableItems(cb20.id);		
						}
									
					}
					else
					{
					//	if ( firstTime == false )
					//	{
							UpdateSpecialLimitsFromHO22(false);
					//	}
						
						if ( cb20 != null )
						{	
							//cb21.checked = false;
							cb20.parentElement.parentElement.parentElement.disabled = false;
							cb20.disabled = false;
							DisableItems(cb20.id);		
						}
						
					}
					
				}//End of HO-21
				
				//Preferred Plus Coverage (HO-23)
				if ( covCode == "EBP23")
				{
					//Get the others
					//var cb21 = GetControlInGridFromCode("EBP20");
					var cb20 = GetControlInGridFromCode("EBP20",'_cbDelete');
					var cb21 = GetControlInGridFromCode("EBP21",'_cbDelete');
					var cb24 = GetControlInGridFromCode("EBP24",'_cbDelete');
					var cb25 = GetControlInGridFromCode("EBP25",'_cbDelete');
						
					if ( cb.checked == true )
					{
						
							UpdateSpecialLimits(true);
						
						if ( cb20 != null )
						{	
							cb20.checked = false;
							cb20.parentElement.parentElement.parentElement.disabled = true;
							cb20.disabled = true;
							DisableItems(cb20.id);		
						}
						
						if ( cb21 != null )
						{	
							cb21.checked = false;
							cb21.parentElement.parentElement.parentElement.disabled = true;
							cb21.disabled = true;
							DisableItems(cb21.id);		
						}
						
						if ( cb24 != null )
						{	
							cb24.checked = false;
							cb24.parentElement.parentElement.parentElement.disabled = true;
							cb24.disabled = true;
							DisableItems(cb24.id);		
						}
						
						if ( cb25 != null )
						{	
							cb25.checked = false;
							cb25.parentElement.parentElement.parentElement.disabled = true;
							cb25.disabled = true;
							DisableItems(cb25.id);		
						}
							
					}
					else
					{
						
							UpdateSpecialLimits(false);
						
						
						if ( cb20 != null && cb21.checked == false )//cb21.checked added by Charles on 29-Jul-09 for Itrack 6153
						{	
							//cb21.checked = false;
							cb20.parentElement.parentElement.parentElement.disabled = false;
							cb20.disabled = false;
							DisableItems(cb20.id);		
						}
						
						if ( cb21 != null )
						{	
							//cb23.checked = false;
							cb21.parentElement.parentElement.parentElement.disabled = false;
							cb21.disabled = false;
							DisableItems(cb21.id);		
						}
						
						if ( cb24 != null )
						{	
							cb24.checked = false;
							cb24.parentElement.parentElement.parentElement.disabled = false;
							cb24.disabled = false;
							DisableItems(cb24.id);		
						}
						
						if ( cb25 != null )
						{	
							cb25.checked = false;
							cb25.parentElement.parentElement.parentElement.disabled = false;
							cb25.disabled = false;
							DisableItems(cb25.id);		
						}
					}
					
				}//End of HO-23
				
				//Preferred Plus Coverage (HO-24)
				if ( covCode == "EBP24")
				{
					//Get the others
					//var cb21 = GetControlInGridFromCode("EBP20");
					var cb20 = GetControlInGridFromCode("EBP20",'_cbDelete');
					var cb22 = GetControlInGridFromCode("EBP22",'_cbDelete');
					var cb23 = GetControlInGridFromCode("EBP23",'_cbDelete');
					var cb25 = GetControlInGridFromCode("EBP25",'_cbDelete');
					
					//Water backup
					var cbWater = GetControlInGridFromCode("WBSPO",'_cbDelete');
					
					if ( cb.checked == true )
					{
						if ( firstTime == false )
						{
							UpdateSpecialLimits(true);
						}
						
						//Water Backup and Sump Pump Overflow (HO-327) 
						if ( cbWater != null )
						{
							if ( firstTime == false )
							{
								cbWater.checked = true;
								SetHiddenField(cbWater.id)
						        cbWater.disabled = true;
								DisableItems(cbWater.id);	
								//Get Water DDL
								//var ddlWater = GetIncludedDDLFromCode("WBSPO");	
								var ddlWater = GetControlInGridFromCode('WBSPO',"_ddlLimit");
							
								//Set Default value of 1000
								if ( ddlWater != null )
								{
									
										ddlWater.options.selectedIndex = 1;
									
								}
							}
						}
							
						if ( cb20 != null )
						{	
							cb20.checked = false;
							cb20.parentElement.parentElement.parentElement.disabled = true;
							cb20.disabled = true;
							DisableItems(cb20.id);		
						}
						
						if ( cb22 != null )
						{	
							cb22.checked = false;
							cb22.parentElement.parentElement.parentElement.disabled = true;
							cb22.disabled = true;
							DisableItems(cb22.id);		
						}
						
						if ( cb23 != null )
						{	
							cb23.checked = false;
							cb23.parentElement.parentElement.parentElement.disabled = true;
							cb23.disabled = true;
							DisableItems(cb23.id);		
						}
						
						if ( cb25 != null )
						{	
							cb25.checked = false;
							cb25.parentElement.parentElement.parentElement.disabled = true;
							cb25.disabled = true;
							DisableItems(cb25.id);		
						}
							
					}
					else
					{
						if ( firstTime == false )
						{
							UpdateSpecialLimits(false);
						}
									
						//Preferred Plus V.I.P.(HO-22)
						if ( cb22 != null )
						{	
							//cb23.checked = false;
							cb22.parentElement.parentElement.parentElement.disabled = false;
							cb22.disabled = false;
							DisableItems(cb22.id);		
						}
						
						//Preferred Plus (HO-20)	
						if ( cb20 != null )
						{
							//alert('here');
							if ( cb21 != null )
							{
								if ( cb20.disabled == true && cb21.checked == false)
								{	
									//cb21.checked = false;
									cb20.parentElement.parentElement.parentElement.disabled = false;
									cb20.disabled = false;
									DisableItems(cb20.id);		
								}
							}
						}
						
						
						if ( cb23 != null )
						{	
							cb23.checked = false;
							cb23.parentElement.parentElement.parentElement.disabled = false;
							cb23.disabled = false;
							DisableItems(cb23.id);		
						}
						
						if ( cb25 != null )
						{	
							cb25.checked = false;
							cb25.parentElement.parentElement.parentElement.disabled = false;
							cb25.disabled = false;
							DisableItems(cb25.id);		
						}
						if ( cbWater != null )
						{
						 
						  var ddlWater = GetControlInGridFromCode("WBSPO","_ddlLimit");
						  var lblWater = GetControlInGridFromCode("WBSPO","_lblLIMIT");
						  lblWater.innerText='';
						  SetHiddenFieldControls(lblWater.id);
						  lblWater.innerText = 'Sum Insured';
					      SetHiddenField(cbWater.id)
						  cbWater.disabled = false;
						  
						
						}
					}
					
				}//End of HO-24
				
				//Preferred Plus Coverage (HO-25)
				if ( covCode == "EBP25")
				{
					//Get the others
					//var cb21 = GetControlInGridFromCode("EBP20");
					var cb20 = GetControlInGridFromCode("EBP20",'_cbDelete');
					var cb21 = GetControlInGridFromCode("EBP21",'_cbDelete');
					var cb23 = GetControlInGridFromCode("EBP23",'_cbDelete');
					var cb24 = GetControlInGridFromCode("EBP24",'_cbDelete');
					var cbWater = GetControlInGridFromCode("WBSPO",'_cbDelete');
					var lblWBSPO       = GetControlInGridFromCode('WBSPO',"_lblLIMIT");	
					
					var product = document.getElementById('hidProduct').value;
						
					if ( cb.checked == true )
					{
						
						//HO-3 Premier
						if ( product == 11409 )
						{		
							UpdateSpecialLimits(true);
						}
						
						if ( cb20 != null )
						{	
							if ( cb20.disabled == false )
							{
								cb20.checked = false;
								cb20.parentElement.parentElement.parentElement.disabled = true;
								cb20.disabled = true;
								DisableItems(cb20.id);		
							}
						}
						
						if ( cb21 != null )
						{	
							cb21.checked = false;
							cb21.parentElement.parentElement.parentElement.disabled = true;
							cb21.disabled = true;
							DisableItems(cb21.id);		
						}
						
						if ( cb23 != null )
						{	
							cb23.checked = false;
							cb23.parentElement.parentElement.parentElement.disabled = true;
							cb23.disabled = true;
							DisableItems(cb23.id);		
						}
						
						if ( cb24 != null )
						{	
							cb24.checked = false;
							cb24.parentElement.parentElement.parentElement.disabled = true;
							cb24.disabled = true;
							DisableItems(cb24.id);		
						}
						
						//Water Backup and Sump Pump Overflow (HO-327) 
						if ( cbWater != null )
						{
						
						  if ( firstTime == false )
							{
								//Commented by Charles on 11-Dec-09 for Itrack 6845
								/*cbWater.checked = true;
								cbWater.disabled = true;
								lblWBSPO.innerText = '1,000';
					            SetHiddenFieldControls(lblWBSPO.id);
								SetHiddenField(cbWater.id)
								DisableItems(cbWater.id);*/										
								
								//Get Water DDL
								if ( document.getElementById('hidWBSPOEXIST').value  != 'EXIST')
							  {
								//var ddlWater = GetIncludedDDLFromCode("WBSPO");	
								var ddlWater = GetControlInGridFromCode('WBSPO',"_ddlLimit");
							
								//Set Default value of 1000
								if ( ddlWater != null )
								{
									ddlWater.options.selectedIndex = 1;	
								}
							 }
							}
						}//End of water back up
						
							
					}
					else
					{
						//HO-3 Premier
						
						if ( product == 11409 )
						{
							UpdateSpecialLimits(false);
						}
						
						if ( cb20 != null )
						{	
							//cb21.checked = false;
							cb20.parentElement.parentElement.parentElement.disabled = false;
							cb20.disabled = false;
							DisableItems(cb20.id);		
						}
						
						if ( cb21 != null )
						{	
							//cb23.checked = false;
							cb21.parentElement.parentElement.parentElement.disabled = false;
							cb21.disabled = false;
							DisableItems(cb21.id);		
						}
						
						if ( cb23 != null )
						{	
							cb23.checked = false;
							cb23.parentElement.parentElement.parentElement.disabled = false;
							cb23.disabled = false;
							DisableItems(cb23.id);		
						}
						
						if ( cb24 != null )
						{	
							cb24.checked = false;
							cb24.parentElement.parentElement.parentElement.disabled = false;
							cb24.disabled = false;
							DisableItems(cb24.id);		
						}
						if ( cbWater != null )
						{
						  var ddlWater = GetControlInGridFromCode("WBSPO","_ddlLimit");
						  var lblWater = GetControlInGridFromCode("WBSPO","_lblLIMIT");
						  cbWater.disabled = false;
						  lblWater.innerText='';
						  SetHiddenFieldControls(lblWater.id);
						  lblWater.innerText = 'Sum Insured';
						 	
						}
					}
					
				}//End of HO-25
				
				//Water Backup and Sump Pump Overflow (HO-327) 
				if ( covCode == "WBSPO")
				{
					if ( cb.checked == true )
					{
						if ( firstTime == false )
						{
							//var ddlWBSPO = GetIncludedDDLFromCode("WBSPO");
							var ddlWBSPO = GetControlInGridFromCode('WBSPO',"_ddlLimit");
							
							if ( ddlWBSPO != null )
							{
								//OnDDLChange(ddlWBSPO);
								ddlWBSPO.selectedIndex = 1;
							}
						}
					}
				}
				
			}
			//End of OnCheck***********************************************************
			
						
			//Custom validator function for Reduction Limit C
			//Rounds to lower 100
			function ReductionLimitValidations(objSource, objArgs)
			{
							
				var amount = ReplaceAll(objArgs.Value,',','');
					 //Add:Itrack # 6093 -13 July 2009 -Manoj Rathore 
					amount = ReplaceAll(amount,'$','');
				
				var txtDwell= GetControlInGridFromCode("DWELL","_txtLIMIT");
				var DwellValue = ReplaceAll(txtDwell.value,',','');
					DwellValue = ReplaceAll(DwellValue,'$','');
				if ( amount == '' || DwellValue == '')

				{
					objArgs.IsValid = true;
					
				}
				DwellValue=parseInt(DwellValue) * .1;
				if ( parseInt(amount) > DwellValue )
				{
					
					objArgs.IsValid = false;
					
				}
				
				return;
			}
			//Custom validator function for Coverage D
			//Rounds to lower 1000
			function CoverageDValidations(objSource, objArgs)
			{
				
				var txtCoverageD = document.getElementById(objSource.controltovalidate);
				var strCoverageD = ReplaceAll(objSource.id,'csv','rev');
				//Get The Regular Expression Validator
				var revCoverageD =document.getElementById(strCoverageD);
				
				if(revCoverageD.isvalid==false)
				   return;
				if ( txtCoverageD.value == '' && txtCoverageD.value == 0)
				{
					objArgs.IsValid = true;
					return;
				}
				var inAmount=ReplaceAll(txtCoverageD.value,',','');
					inAmount=ReplaceAll(inAmount,'$','');
				var factor   =  parseInt(inAmount/1000);
				if(factor==0)
				{
				   txtCoverageD.value=1000;
				 
				}
				else
				{
				   txtCoverageD.value=factor*1000;
				}
				//if(txtLossAsses.value=='NaN')
				//{
				 // txtLossAsses.value='';
			//	}
				objArgs.IsValid = true;
			/*
				var txtFirearm = document.getElementById(objSource.controltovalidate);	
				
				var amount = ReplaceAll(objArgs.Value,',','');
				
				if ( amount == '' )
				{
					objArgs.IsValid = true;
					return;
				}
				
				if ( amount.length < 4 )
				{
					txtFirearm.value = '1000';
				}
				else
				{	
					var newAmount = amount.replace(amount,amount.substring(amount.length - 2,amount.length),'000') ;	
					alert(newAmount);
				}
				*/
				
			}
			//Custom validator function for Loss Assessment Coverage (HO-35)
			//value should increment/round off in 500.
            //For eg: if we enter 200 - 500 should be displayed, 1400 -1000, 1600 - 1500.
			function LossAssessment(objSource, objArgs)
			{
			
				var txtLossAsses = document.getElementById(objSource.controltovalidate);
				var strLossAsses = ReplaceAll(objSource.id,'csv','rev');
				//Get The Regular Expression Validator
				var revLossAsses =document.getElementById(strLossAsses);
				//by Pravesh
				var out = ',';
				var add = '';
				var temp = '' + txtLossAsses.value;
				while (temp.indexOf(out)>-1)
				{ 
				pos= temp.indexOf(out);
				temp = '' + (temp.substring(0, pos)+ add + temp.substring((pos + out.length), temp.length));
				}
				var sourceVal=temp;
				if(sourceVal==0)
				{
				 txtLossAsses.value='';
				 return;
				}
				if( parseInt(sourceVal) > 24000)
				{
					objArgs.IsValid = false;
					return;
				}
				//end here
				if(revLossAsses.isvalid==false)
				   return;
				if ( txtLossAsses.value == '' && txtLossAsses.value == 0)
				{
					objArgs.IsValid = true;
					return;
				}
				var inAmount=ReplaceAll(txtLossAsses.value,',','');
					inAmount=ReplaceAll(inAmount,'$','');
				var factor   =  parseInt(inAmount/1000);
				if(factor==0)
				{
				   txtLossAsses.value=1000;
				 
				}
				else
				{
				   txtLossAsses.value=factor*1000;
				}
				//if(txtLossAsses.value=='NaN')
				//{
				 // txtLossAsses.value='';
			//	}
				objArgs.IsValid = true;
		
			}
			//Custom validator function for Firearms
			//Rounds to lower 100
			function FirearmsValidations(objSource, objArgs)
			{
				var txtFirearm = document.getElementById(objSource.controltovalidate);	
				
				//var prefix = "dgCoverages__ctl";
				
				var id =txtFirearm.id 
				
				
				var lastIndex = id.indexOf('txtDEDUCTIBLE_1_TYPE');
				var prefix = id.substring(0,lastIndex);
				var varLimitAmt = prefix + 'lblLIMIT';
				
				
				
				var inclAmount = document.getElementById(varLimitAmt).innerText;
					inclAmount = ReplaceAll(ReplaceAll(inclAmount,',',''),'$','');
				//Additional amount
				var amount = ReplaceAll(objArgs.Value,',','');
					amount = ReplaceAll(amount,'$','');
				//If empty , then return
				if ( amount == '' && inclAmount == '')
				{
					objArgs.IsValid = true;
					return;
				}
				
				if ( inclAmount == '' )
				{
					inclAmount = 0;
				}
					
				if ( amount == '' )
				{
					amount = 0;
				}
				
				//alert(inclAmount);
				//alert(amount);
				
				
				//Calculate sum of included and additional
				var sum = parseInt(inclAmount) + parseInt(amount);
				
				//alert(sum);
				
				//If sum > 6000, then show error message
				if ( sum > 6000 )
				{
					//Validation false
					objArgs.IsValid = false;
					return;	
				}
				
				if ( amount < 100 )
				{
					txtFirearm.value = 	'100';	
				}
				
				if ( amount > 100 && amount < 1000 )
				{
					var newAmount = amount.substring(0,1) + '00';	
					txtFirearm.value = 	newAmount;	
				}
				
				if ( amount > 1000 && amount < 6000 )
				{
					var newAmount = amount.substring(0,2) + '00';	
					txtFirearm.value = 	newAmount;			
				}
					
				objArgs.IsValid = true;
				
			}
			
			function OnDDLChange(ddl)
			{
				
				var COV_CODE = '';
				
				if ( ddl.getAttribute('COV_CODE') == null ) return;
				
				COV_CODE = ddl.getAttribute('COV_CODE');
			   if (COV_CODE  == 'IBUSPA')
				{
				    
				    var cbIBUSPOA = GetControlInGridFromCode("IBUSPOA","_cbDelete");
				    if (cbIBUSPOA != null)
				    {
				      if ( cbIBUSPOA.checked == true )
						  {
							var amount = ReplaceAll(ddl.options[ddl.selectedIndex].text,',','');
							var lblIBUSP = GetControlInGridFromCode("IBUSP","_lblLIMIT");
							var lblIBUSPO = GetControlInGridFromCode("IBUSPO","_lblLIMIT");
							var amountIBUSP=0;
							var amountIBUSPO=0;
     						if (lblIBUSP != null)
							{
							   amountIBUSP=ReplaceAll(lblIBUSP.innerText,',','');
							   
							}
							if (lblIBUSPO != null)
							{
							   amountIBUSPO=ReplaceAll(lblIBUSPO.innerText,',','');
							   
							}
							   
							amount=((parseInt(amount) + parseInt(amountIBUSP) ) * .1) - 250;
							var txtIBUSPOA = GetControlInGridFromCode("IBUSPOA","_txtDEDUCTIBLE_1_TYPE");
							if (txtIBUSPOA != null)
							{
							   txtIBUSPOA.value=amount;
							   txtIBUSPOA.value=formatCurrency(txtIBUSPOA.value);  
							    if(txtIBUSPOA.value==0)  
							     txtIBUSPOA.value=''; 
							}   
				          }
				         
				    }
				}
				/* Commented by Charles on 8-Oct-09 for Itrack 6148
				if ( COV_CODE == 'WBSPO')
				{
					//Get HO-24
					var cb24 = GetControlInGridFromCode("EBP24",'_cbDelete');
					var cb25 = GetControlInGridFromCode("EBP25",'_cbDelete');
					
					if ( cb24 != null)
					{
						if ( cb24.checked == true )
						{
							var amount = ReplaceAll(ddl.options[ddl.selectedIndex].text,',','');
							
							if ( parseInt(amount) < parseInt(1000) )
							{
								alert('With HO-24 selected, amount cannot be less than 1000.');
								ddl.selectedIndex = 1;
							}
						}
					}
					
					//If HO-25 is checked, water backup cannot be less than 1000
					if ( cb25 != null)
					{
						if ( cb25.checked == true )
						{
							var amount = ReplaceAll(ddl.options[ddl.selectedIndex].text,',','');
							
							if ( parseInt(amount) < parseInt(1000) )
							{
								alert('With HO-25 selected, amount cannot be less than 1000.');
								ddl.selectedIndex = 1;
							}
						}
					}
				}*/
				if(COV_CODE == 'EBCCSL')
				{
					  var cb22 = GetControlInGridFromCode("EBP22","_cbDelete");
					  if ( cb22 != null)
					   {
						if(cb22.checked == true )
							{ 	
								var intddl = ddl.options[ddl.selectedIndex].text
									intddl=ReplaceAll(intddl,',','');									
								if(parseInt(intddl) > 2500)
								{
									SelectDropdownOptionByText(ddl,"2,500")
								}
													
							 }
					   }	
				}
					if(COV_CODE == 'EBCCSM')
					{
					  var cb22 = GetControlInGridFromCode("EBP22","_cbDelete");
					  if ( cb22 != null)
					  {
						if(cb22.checked == true )
							{ 	
								var intddl = ddl.options[ddl.selectedIndex].text
									intddl=ReplaceAll(intddl,',','');
								if(parseInt(intddl) > 500)
								{
									SelectDropdownOptionByText(ddl,"500")
								}
								
							}
					  }		
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
				var calledFrom = new String('<%=strCalledFrom%>');									
				switch(calledFrom.toUpperCase())
				{
					case "HOME":
					{
						document.getElementById('hidDataValue1').value =populateAdditionalInfo("DWELLING_NUMBER");
					document.getElementById('hidDataValue2').value =populateAdditionalInfo("Address");					
					if(document.getElementById('hidDataValue1').value=='undefined')
						document.getElementById('hidDataValue1').value="";
					if(document.getElementById('hidDataValue2').value=='undefined')
						document.getElementById('hidDataValue2').value="";
					document.getElementById('hidCustomInfo').value=";Dwelling # = " + document.getElementById('hidDataValue1').value + ";Address = " + document.getElementById('hidDataValue2').value;										
					
					break;				
					}
					case "RENTAL":
					{
						//document.getElementById('hidDataName1').value="DWELLING_NUMBER";
						//document.getElementById('hidDataName2').value="Address";
						document.getElementById('hidDataValue1').value=populateAdditionalInfo("DWELLING_NUMBER");
						document.getElementById('hidDataValue2').value=populateAdditionalInfo("Address");					
						if(document.getElementById('hidDataValue1').value=='undefined')
							document.getElementById('hidDataValue1').value="";
						if(document.getElementById('hidDataValue2').value=='undefined')
							document.getElementById('hidDataValue2').value="";
						document.getElementById('hidCustomInfo').value=";Dwelling # = " + document.getElementById('hidDataValue1').value + ";Address = " + document.getElementById('hidDataValue2').value;						
						break;
					}
					/*case "MOT":					
					{
						//document.getElementById('hidDataName1').value="MAKE";
						//document.getElementById('hidDataName2').value="MODEL";
						document.getElementById('hidDataValue1').value=populateAdditionalInfo("MAKE");
						document.getElementById('hidDataValue2').value=populateAdditionalInfo("MODEL");										
						document.getElementById('hidDataValue3').value=populateAdditionalInfo("INSURED_VEH_NUMBER");										
						if(document.getElementById('hidDataValue1').value=='undefined')
							document.getElementById('hidDataValue1').value="";
						if(document.getElementById('hidDataValue2').value=='undefined')
							document.getElementById('hidDataValue2').value="";
						document.getElementById('hidCustomInfo').value=";Motorcycle # = " + document.getElementById('hidDataValue3').value + ";Make = " + document.getElementById('hidDataValue1').value + ";Model = " + document.getElementById('hidDataValue2').value;					
						break;
					}*/
				}
			
			
		}
		</script>
	</HEAD>
	<body onload="populateInfo();UpdateHo50();PremierVIP24();UpDateBuliding();" MS_POSITIONING="GridLayout"> <!--UpDateBuliding() added by Charles on 7-Oct-09 for Itrack 6525 -->
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr id="trError" runat="server">
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblError" runat="server" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<table cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td class="headereffectCenter"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow><asp:label id="lblTitle" runat="server">SECTION 1 COVERAGES</asp:label></td>
							</tr>
							<tr>
								<td align="center"><asp:label id="lblMessage" runat="server" CssClass="errmsg" EnableViewState="False" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<td class="midcolora"><asp:datagrid id="dgCoverages" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyField="COVERAGE_ID">
										<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText=" Required /Optional" ItemStyle-Width="10%">
												<ItemTemplate>
													<asp:CheckBox id="cbDelete" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' COV_ID='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
													</asp:CheckBox>
													<input type="hidden" id="hidcbDelete" value="" name="hidcbDelete" runat="server">
													<asp:Label ID="lblLIMIT_TYPE" style="display:none" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT_TYPE") %>'>
													</asp:Label>
													<asp:Label ID="lblDEDUCTIBLE_TYPE" style="display:none" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_TYPE") %>'>
													</asp:Label>
													<asp:Label ID="lblAddDEDUCTIBLE_TYPE" style="display:none" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ADDDEDUCTIBLE_TYPE") %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Coverage">
												<ItemTemplate>
													<asp:Label runat="server" ID="lblCOV_DESC" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Sum Insured" ItemStyle-Width="15%">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox ID="txtLIMIT" CssClass="INPUTCURRENCY" MaxLength="10" Visible="False" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INCLUDED","{0:,#,###}") %>'> CssClass="INPUTCURRENCY" MaxLength="10" onBlur="this.value=formatCurrency(this.value);"></asp:TextBox>
													<select  id="ddlLIMIT" Visible="false" Runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' onchange="javascript:OnDDLChange(this);">
													</select>
													<asp:label id="lblNoCoverageLimit" CssClass="labelfont" Runat="server">No Coverages</asp:label>
													<asp:label id="lblLIMIT" CssClass="LabelFont" Visible="true" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INCLUDED_TEXT","{0:,#,###}") %>'>></asp:label>
													<input type="hidden" id="hidLIMIT" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.INCLUDED","{0:,#,###}") %>' NAME="hidLIMIT">
													<BR>
													<asp:RequiredFieldValidator ID="rfvLIMIT" Runat="server" Display="Dynamic" ControlToValidate="txtLIMIT" Enabled="False"
														ErrorMessage="A"></asp:RequiredFieldValidator>
													<asp:rangevalidator id="rngDWELLING_LIMIT" runat="server" ControlToValidate="txtLIMIT" Display="Dynamic"
														Type="Currency" MaximumValue="0" MinimumValue="0" ErrorMessage="Coverage A" Enabled="False"></asp:rangevalidator>
													<asp:RegularExpressionValidator ID="Regularexpressionvalidator1" Runat="server" ControlToValidate="txtLIMIT" Display="Dynamic"
														Enabled="False" ErrorMessage="B"></asp:RegularExpressionValidator>
													<asp:RegularExpressionValidator ID="revLIMIT" Runat="server" ControlToValidate="txtLIMIT" Display="Dynamic" ErrorMessage="C"></asp:RegularExpressionValidator>
													<asp:CustomValidator ID="csvLIMIT" Runat="server" Display="Dynamic" ErrorMessage="Limit already at max. allowed ($6000)."
														ControlToValidate="txtLIMIT" Enabled="False"></asp:CustomValidator>
													<BR>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn Visible="False" HeaderText="Limit 2">
												<ItemTemplate>
													<select ID="ddlLIMIT_2_TYPE" Runat="server">
													</select>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Additional" ItemStyle-Width="15%">
												<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<select  id="ddlDEDUCTIBLE" Visible="true" Runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_1","{0:,#,###}") %>' onchange="javascript:OnDDLChange(this);">
													</select>
													<asp:TextBox CssClass="INPUTCURRENCY" MaxLength="8" id="txtDEDUCTIBLE_1_TYPE" onChange="this.value=formatCurrency(this.value);" Visible="true" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_1","{0:,#,###}") %>'>
													</asp:TextBox>
													<asp:Label id="lblDEDUCTIBLE" CssClass="labelfont" Runat="server" visible="false"></asp:Label>
													<input id="hidDEDUCTIBLE" type="hidden" value='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_1_DISPLAY","{0:,#,###}") %>' name="hidDEDUCTIBLE" runat="server">
													<asp:label id="lblNoCoverage" CssClass="labelfont" Runat="server">No Coverages</asp:label>
													<BR>
													<asp:RegularExpressionValidator ID="revLIMIT_DEDUC_AMOUNT" Runat="server" ControlToValidate="txtDEDUCTIBLE_1_TYPE"
														Display="Dynamic"></asp:RegularExpressionValidator>
													<asp:RangeValidator ID="rngDEDUCTIBLE" Runat="server" ControlToValidate="txtDEDUCTIBLE_1_TYPE" Display="Dynamic"
														Type="Currency" MaximumValue="200000" MinimumValue="1" Enabled="False"></asp:RangeValidator>
													<asp:CustomValidator ID="csvLIMIT_DEDUC_AMOUNT" Runat="server" Display="Dynamic" ErrorMessage="The maximum combined limit canot exceed $6000."
														ControlToValidate="txtDEDUCTIBLE_1_TYPE" Enabled="False"></asp:CustomValidator>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn Visible="True" HeaderText="Deductible" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
												<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<!--<span id="spnDEDUCTIBLE_AMOUNT_TEXT" class="labelfont" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
														<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_TEXT") %>
													</span>--><INPUT id="hidlbl_DEDUCTIBLE_AMOUNT"  type="hidden" value='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE") %> ' name="hidDEDUCTIBLE_AMOUNT" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
													<asp:Label ID="lblDEDUCTIBLE_AMOUNT" CssClass="labelfont" Visible=False runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_TEXT") %>' COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
													</asp:Label>
													<asp:label id="lblNoaddDEDUCTIBLE" CssClass="labelfont" Runat="server">No Coverages</asp:label>
													<select id="ddladdDEDUCTIBLE" Runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' onchange="javascript:OnDDLChange(this);" NAME="ddladdDEDUCTIBLE">
													</select>
													<asp:TextBox CssClass="INPUTCURRENCY" MaxLength="6" id="txtaddDEDUCTIBLE" Visible="false" Runat="server" onBlur="this.value=formatCurrency(this.value);" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE","{0:,#,###}") %>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn Visible="False" HeaderText="COV_ID">
												<ItemTemplate>
													<asp:Label ID="lblCOV_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="COV_CODE" Visible="True"></asp:BoundColumn>
										</Columns>
									</asp:datagrid></td>
							</tr>
							<tr>
								<td>
									<table width="100%">
										<tr>
											<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Visible="False" Text="Delete" Enabled="False"></cmsb:cmsbutton></td>
											<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td><INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidREC_VEH_ID" type="hidden" value="0" name="hidREC_VEH_ID" runat="server">
									<INPUT id="hidAppID" type="hidden" name="hidAppID" runat="server"> <INPUT id="hidAppVersionID" type="hidden" name="hidAppVersionID" runat="server">
									<INPUT id="hidPOLID" type="hidden" name="hidPOLID" runat="server"> <INPUT id="hidPOLVersionID" type="hidden" name="hidPOLVersionID" runat="server">
									<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"> <INPUT id="hidPOL_LOB" type="hidden" value="0" name="hidAPP_LOB" runat="server">
									<INPUT id="hidOldXml" type="hidden" name="hidOldXml" runat="server"> <INPUT id="hidPolcyType" type="hidden" name="hidPolcyType" runat="server">
									<INPUT id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server"> <INPUT id="hidROW_COUNT" type="hidden" value="0" name="hidAPP_LOB" runat="server">
									<INPUT id="hidCoverageA" type="hidden" name="hidCoverageA" runat="server"> <INPUT id="hidProduct" type="hidden" name="hidProduct" runat="server">
									<INPUT id="hidDataValue1" type="hidden" name="hidDataValue1" runat="server"> <INPUT id="hidDataValue2" type="hidden" name="hidDataValue2" runat="server">
									<INPUT id="hidDataValue3" type="hidden" name="hidDataValue3" runat="server"> <INPUT id="hidCustomInfo" type="hidden" name="hidCustomInfo" runat="server">
									<input id="hidcbWBSPO" type="hidden" name="hidcbWBSPO" runat="server"> <input id="hidControlXML" type="hidden" name="hidControlXML" runat="server">
									<input id="hidWBSPOEXIST" type="hidden" value="NOTEXIST" name="hidWBSPOEXIST" runat="server">
									<INPUT id="hidREP_COST" type="hidden" name="hidREP_COST" value="0" runat="server">
									<input id="hidStateId" type="hidden" name="hidStateId" runat="server"> <!--Added by Charles on 9-Dec-09 for Itrack 6647-->
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>

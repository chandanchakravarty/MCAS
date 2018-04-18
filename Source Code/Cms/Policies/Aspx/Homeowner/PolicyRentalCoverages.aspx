<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" Codebehind="PolicyRentalCoverages.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.aspx.HomeOwners.PolicyRentalCoverages" validateRequest="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RentalCoverages</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
			function SetMinSubsidenceLimit()
			{
			
			        var cb= GetControlInGridFromCode('MSC480','_cbDelete');
			        if (cb == null) return;
			        if(cb.checked == false) return;
			        var txtMin = GetControlInGridFromCode("MSC480","_txtDEDUCTIBLE_1_TYPE");
					var txtDwell= GetControlInGridFromCode("DWELL","_txtLIMIT");
					var DwellValue = ReplaceAll(txtDwell.value,',','');
					var valComp;
					if( parseInt(DwellValue) > 200000)
					{
						valComp = 200000
					}
					else
					{
						valComp = parseInt(DwellValue)
					}
				    txtMin.value=formatCurrency(valComp);
				    SetMinSubsidence();
			}
			function GetControlFromCode(covCode, suffix)
			{
				//lblNoCoverage
				var rowCount = 50;
				
				if ( document.Form1.hidROW_COUNT != null )
				{
					rowCount = document.Form1.hidROW_COUNT.value;
				}
						
				for (ctr = 2; ctr < rowCount + 2; ctr++)
				{
					//chk = document.getElementById(prefix + ctr + "_cbDelete");
					var ctl = document.getElementById(prefix + ctr + "_" + suffix);
					
					if ( ctl != null )
					{
						var coverageCode = ctl.getAttribute("COV_CODE");
				
						if ( trim(covCode) == trim(coverageCode) )
						{
							//alert(covCode + ' ' + coverageCode );
							
							return ctl;
						}	
					}
				}
				
				return null;
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
			//This variable will be used by tab control, for checking ,whether save msg should be shown to user
			//while changing the tabs
			//True means Want to save msg should always be shown to user
			function SetMinSubsidence()
			{
			       //Get The Mine Subsidence Coverage (DP-480)
			        var cb= GetControlInGridFromCode('MSC480','_cbDelete');
			        if (cb == null) return;
			        if(cb.checked == false) return;
					var txtMin = GetControlInGridFromCode("MSC480","_txtDEDUCTIBLE_1_TYPE");
					var sourceVal     = ReplaceAll(txtMin.value,',','');
					var lblMin=GetControlInGridFromCode("MSC480","_lblDEDUCTIBLE_AMOUNT");
					var hid=GetControlInGridFromCode('MSC480','_hidlbl_DEDUCTIBLE_AMOUNT');
					var lblLimitMSC480= GetControlInGridFromCode("MSC480","_lblLIMIT");
					if(sourceVal== '')
					{
					  lblMin.innerText='';
					  hid.value=''
					  return
					   
					}
					var Min=parseInt(sourceVal) * .02;
					Min=Math.round(Min,0);
					if(parseInt(Min) < 250)
					{
					  Min=250;
					}
					else if(parseInt(Min) > 500)
					{
					  Min =500;
					} 
					lblMin.innerText= '2% - ' + Min 
					lblLimitMSC480.innerText="Not Applicable";
					hid.value=Min + ' ' ;
					txtMin.value=formatCurrency(sourceVal);
			
			}
			function UpdateTreesShrubs()
			{
			  var cb= GetControlInGridFromCode('TSPL','_cbDelete');
			  if(cb == null) return;
			  onCheck(cb.id);
			}
			// Additional Amount  for Mine Subsidence is not greater than  Coverage A 
			
			function CoverageMinValidation(objSource, objArgs)
			{
			  
				var txtDwell= GetControlInGridFromCode("DWELL","_txtLIMIT");
				
				if( txtDwell == null)
				    return;
				
				//Get The Mine Subsidence Coverage (DP-480)
				var txtMin = document.getElementById(objSource.controltovalidate);
				var sourceVal     = ReplaceAll(txtMin.value,',','');
				var DwellValue = ReplaceAll(txtDwell.value,',','');
				var valComp;
				if( parseInt(DwellValue) > 200000)
				{
					valComp = 200000
				}
				else
				{
				    valComp = parseInt(DwellValue)
				}
				
				if( parseInt(sourceVal) > valComp)
				{
					objArgs.IsValid = false;
					//Page_IsValid=false;
       				return;
				}
				objArgs.IsValid = true;
				
			}
			
		function OnDDLChange(ddl)
			{
				//alert('lolo');
				var COV_CODE = '';
				var cb;
				if ( ddl.getAttribute('COV_CODE') == null ) return;
				
				
				COV_CODE = ddl.getAttribute('COV_CODE');
				
				 /*if(COV_CODE == 'APDI')
				 { 
				
				    var ddlAmount =ddl.options[ddl.selectedIndex].text;
				    var lblBR1143;
			       //Builders Risk (DP-1143) start
			        cb=GetControlInGridFromCode('BR1143','_cbDelete');
			        if(cb != null)
			        {
					 if(cb.checked == true)
						{
							lblBR1143 =GetControlInGridFromCode('BR1143','_lblDEDUCTIBLE_AMOUNT');
							if(lblBR1143 != null)
								{
											
											lblBR1143.innerText=ddlAmount
											var hid=GetControlInGridFromCode('BR1143','_hidlbl_DEDUCTIBLE_AMOUNT')
										    hid.value=ddlAmount + ' ';
											
								}
								
						}
					}
					//Builders Risk (DP-1143) end
					
					//Awnings, Canopies or Signs start
					cb=GetControlInGridFromCode('ACS','_cbDelete');
					if(cb != null)
			        {
						if(cb.checked == true)
							{
								lblBR1143 =GetControlInGridFromCode('ACS','_lblDEDUCTIBLE_AMOUNT');
								if(lblBR1143 != null)
									{
												
												lblBR1143.innerText=ddlAmount
												var hid=GetControlInGridFromCode('ACS','_hidlbl_DEDUCTIBLE_AMOUNT')
										        hid.value=ddlAmount+ ' ' ;
									}
									
							}
					}		
				   //Awnings, Canopies or Signs end	
				   
				   	//Building Improvements, Alterations & Additions  start
					cb=GetControlInGridFromCode('BIAA','_cbDelete');
					if(cb != null)
			        {
						if(cb.checked == true)
							{
								lblBR1143 =GetControlInGridFromCode('BIAA','_lblDEDUCTIBLE_AMOUNT');
								if(lblBR1143 != null)
									{
												
												lblBR1143.innerText=ddlAmount
												var hid=GetControlInGridFromCode('BIAA','_hidlbl_DEDUCTIBLE_AMOUNT')
										        hid.value=ddlAmount+ ' ' ;
									}
									
							}
					}		
				   //Building Improvements, Alterations & Additions  end	
				   
				   	//Radio & Television Equipment   start
					cb=GetControlInGridFromCode('RTE','_cbDelete');
					if(cb != null)
			        {
						if(cb.checked == true)
							{
								lblBR1143 =GetControlInGridFromCode('RTE','_lblDEDUCTIBLE_AMOUNT');
								if(lblBR1143 != null)
									{
												
												lblBR1143.innerText=ddlAmount
												var hid=GetControlInGridFromCode('RTE','_hidlbl_DEDUCTIBLE_AMOUNT')
										        hid.value=ddlAmount+ ' ' ;
									}
									
							}
					}		
				   //Radio & Television Equipment   end		
				   
				   //Trees, Shrubs, Plants & Lawns   start
					cb=GetControlInGridFromCode('TSPL','_cbDelete');
					if(cb != null)
			        {
						if(cb.checked == true)
							{
								lblBR1143 =GetControlInGridFromCode('TSPL','_lblDEDUCTIBLE_AMOUNT');
								if(lblBR1143 != null)
									{
												
												lblBR1143.innerText=ddlAmount
												var hid=GetControlInGridFromCode('TSPL','_hidlbl_DEDUCTIBLE_AMOUNT')
										        hid.value=ddlAmount+ ' ' ;
									}
									
							}
					}		
				   //Trees, Shrubs, Plants & Lawns   end		
				   
			  }
		*/		
		    }		
		function populateInfo()
		{
			if (this.parent.strSelectedRecordXML == "-1")
			{
				setTimeout('populateInfo();',100);
				return;
			}
			//document.getElementById('hidDataName1').value="DWELLING_NUMBER";
			//document.getElementById('hidDataName2').value="Address";
			document.getElementById('hidDataValue1').value=populateAdditionalInfo("DWELLING_NUMBER");
			document.getElementById('hidDataValue2').value=populateAdditionalInfo("Address");					
			if(document.getElementById('hidDataValue1').value=='undefined')
				document.getElementById('hidDataValue1').value="";
			if(document.getElementById('hidDataValue2').value=='undefined')
				document.getElementById('hidDataValue2').value="";
			document.getElementById('hidCustomInfo').value=";Dwelling # = " + document.getElementById('hidDataValue1').value + ";Address = " + document.getElementById('hidDataValue2').value;								
			
			
		}
		
		function DisableItemsForSection2(strcbDelete, strCOV_CODE, controlPrefix)
			{
				
				cbDelete = document.getElementById(strcbDelete);
				strlblNoLimit = controlPrefix + '_lblNoLimit';
				strlblLIMIT_AMOUNT = controlPrefix + '_lblLIMIT_AMOUNT';
				strlblNoDeductible =controlPrefix + '_lblNoDeductible';
				strspnDEDUCTIBLE_AMOUNT_TEXT=controlPrefix + '_spnDEDUCTIBLE_AMOUNT_TEXT';
				
				//DedType = document.getElementById(strDedType).innerText;
				
				//alert(strcbDelete);
				lblNoLimit = document.getElementById(strlblNoLimit);
				lblLIMIT_AMOUNT = document.getElementById(strlblLIMIT_AMOUNT);
				lblNoDeductible=document.getElementById(strlblNoDeductible);
				spnDEDUCTIBLE_AMOUNT_TEXT=document.getElementById(strspnDEDUCTIBLE_AMOUNT_TEXT);
				SetHiddenField(cbDelete.id)
				
				
				//if ( strCOV_CODE == 'IOO' || strCOV_CODE == 'PIOSS')
				if ( strCOV_CODE != 'CSL' || strCOV_CODE != 'MEDPM')
				{
						
					if ( cbDelete.checked == true )
					{
						
						
						if ( lblNoLimit != null )
						{
							lblNoLimit.style.display = "none";
							
						}
						
						
						if ( lblLIMIT_AMOUNT != null )
						{
							lblLIMIT_AMOUNT.style.display = "inline";
						}
						if ( lblNoDeductible != null )
						{
							lblNoDeductible.style.display = "none";
							
						}
						
						
						if ( spnDEDUCTIBLE_AMOUNT_TEXT != null )
						{
							spnDEDUCTIBLE_AMOUNT_TEXT.style.display = "inline";
						}

							
					}
					else
					{
						
						if ( lblNoLimit != null )
						{
							lblNoLimit.style.display = "inline";
							
						}
						
						if ( lblLIMIT_AMOUNT != null )
						{
							lblLIMIT_AMOUNT.style.display = "none";
						}
						if ( lblNoDeductible != null )
						{
							lblNoDeductible.style.display = "inline";
							
						}
						
						
						if ( spnDEDUCTIBLE_AMOUNT_TEXT != null )
						{
							spnDEDUCTIBLE_AMOUNT_TEXT.style.display = "none";
						}
					}
				}
				
				
			}
			
			//DP-382 - Lead Liability Exclusion Automatically included if LP-124 is checked of
				function GetControl(cb,suffix)
		{
				var lastIndex = cb.lastIndexOf('_');
				var hid = cb.substring(0,lastIndex) + suffix;
				return hid;
		}
		function GetControls(cbDelete)
		{
		 	
			var lblNoLimit=GetControl(cbDelete,'_lblNoCoverageLimit');
			var lblNoaddDEDUCTIBLE=GetControl(cbDelete,'_lblNoaddDEDUCTIBLE');
			var lblDEDUCTIBLE_AMOUNT=GetControl(cbDelete,'_lblDEDUCTIBLE_AMOUNT');
			var ddlAddDed=GetControl(cbDelete,'_ddladdDEDUCTIBLE');
			var txtaddDEDUCTIBLE=GetControl(cbDelete,'_txtaddDEDUCTIBLE');
			var lblNoDeductible=GetControl(cbDelete,'_lblNoCoverage');
			var txtaddDEDUCTIBLE=GetControl(cbDelete,'_txtaddDEDUCTIBLE');
			var lblLimit=GetControl(cbDelete,'_lblLimit');
			var ddlDed = GetControl(cbDelete,'_ddlDEDUCTIBLE');
			var txtbox = GetControl(cbDelete,'_txtDEDUCTIBLE_1_TYPE');
			var prefix=GetControl(cbDelete,'');
			 
			DisableItems(cbDelete,lblNoLimit,lblNoaddDEDUCTIBLE,lblDEDUCTIBLE_AMOUNT,ddlAddDed,txtaddDEDUCTIBLE,lblNoDeductible,
						lblLimit,ddlDed,txtbox,prefix);
		
		
		}
		
        //DP-382 - Lead Liability Exclusion Automatically included if LP-124 is checked of
		function ChkNocoverage()
	     {
	        var ddlliability=GetControlInGridFromCode("CSL","_ddlLIMIT");
	        var ddlmedpay=GetControlInGridFromCode("MEDPM","_ddlLIMIT");
			
			var liability =ddlliability.selectedIndex
			var medpay =ddlmedpay.selectedIndex
			var cbDP382=GetControlInGridFromCode("DP382","_cbDelete");
			var cbDP392=GetControlInGridFromCode("DP392","_cbDelete");
			var cbLP417=GetControlInGridFromCode("LP417","_cbDelete");
			var cbIOO=GetControlInGridFromCode("IOO","_cbDelete2");
			var cbPIOSS=GetControlInGridFromCode("PIOSS","_cbDelete2");
			
			
			
				if(liability == "0" && medpay != "0")
				{
					ddlmedpay.selectedIndex=0;
						
				}
				if(liability != "0" && medpay == "0")
				{
					ddlmedpay.selectedIndex=1;
				}
		
		
		     liability =ddlliability.selectedIndex
			 medpay =ddlmedpay.selectedIndex
			 //added by pravesh on 24 jan 08 for itrack 3462
			 var cbLP124 = GetControlInGridFromCode("LP124","_cbDelete2");
			 if(liability != "0" )
			 {
			 	if (cbLP124!=null)
			    	{
					   cbLP124.checked=true;
					   SetHiddenField(cbLP124.id);
					   DisableItemsForSection2(cbLP124.id, 'LP124', GetControl(cbLP124.id,''))
					}
				   //aaded by pravesh on march 28 -08 as per itack 3976
				  if (cbIOO!=null)
				  {
				    cbIOO.disabled=false;
				  } 
				  if (cbPIOSS!=null)
				  {
				     cbPIOSS.disabled=false;
				  } 
				  if (cbDP392!=null)
				  {
				     cbDP392.disabled=false;
				  } 
				  
			 }
			 else
			 {
			 	 if (cbLP124!=null)
				  {
				    cbLP124.checked=false;
				    SetHiddenField(cbLP124.id);
				    DisableItemsForSection2(cbLP124.id, 'LP124', GetControl(cbLP124.id,''))
				   }
				   //aaded by pravesh on march 28 -08 as per itack 3976
				  if (cbIOO!=null)
				  {
				    cbIOO.checked=false;
				    SetHiddenField(cbIOO.id);
				    DisableItemsForSection2(cbIOO.id, 'IOO', GetControl(cbIOO.id,''));
				    cbIOO.disabled=true;
				  } 
				  if (cbPIOSS!=null)
				  {
				    cbPIOSS.checked=false; 
				    SetHiddenField(cbPIOSS.id);
				    DisableItemsForSection2(cbPIOSS.id, 'PIOSS', GetControl(cbPIOSS.id,''));
				     cbPIOSS.disabled=true;
				  } 
				  if (cbDP392!=null)
				  {
				    cbDP392.checked=false; 
				    SetHiddenField(cbDP392.id);
				    cbDP392.disabled=true;
				  } 
				  
			 }
			 // end here
			
			if(liability != "0" && medpay != "0")
				{
				  
					if(cbDP382 != null)
					{
						  //added by pravesh on 6 dec 2007 itrack 3157
						  var cbDP392=GetControlInGridFromCode("DP392","_cbDelete"); 
						  if(cbDP392 != null)
						  {
							if (cbDP392.checked!=true)
							{
							cbDP382.checked=true;
							GetControls(cbDP382.id);
							SetHiddenField(cbDP382.id)
							}
					      }
					      //P kasana #3847 ,DP392 is NA in INDIANA ,
					      if(document.getElementById('hidStateId').value == "14")
					      {
							cbDP382.checked=true;
							GetControls(cbDP382.id);
							SetHiddenField(cbDP382.id)					      
							
					      }
					      
						 cbLP417.checked=true;
						 GetControls(cbLP417.id);
						 SetHiddenField(cbLP417.id)
										
					}
				
				}
				else
				{
					if(cbDP382 != null)
						{
						   cbDP382.checked=false;
						   GetControls(cbDP382.id);
						   SetHiddenField(cbDP382.id)
						   cbLP417.checked=false;
						   GetControls(cbLP417.id);
						   SetHiddenField(cbLP417.id)
						}
				}
		}		
		
				//Executes on the check and uncheck of the check boxes
				
			function onCheck(CheckBoxID)
			{
			
			    
			    var cb = document.getElementById(CheckBoxID);
				var span = cb.parentElement;
				var lblBR1143;
				
				if ( span == null ) return;
				
				var covID = span.getAttribute("COV_ID");
				var covCode = span.getAttribute("COV_CODE");
				
				SetHiddenField(cb.id)
				
				var ddlAPD =GetControlInGridFromCode('APDI','_ddladdDEDUCTIBLE'); 
				
				if (covCode == 'TSPL')
				{
					if(cb.checked == true)
					{
						var lblTSPL =GetControlInGridFromCode(covCode,'_lblLIMIT');
						var hidTSPL =GetControlInGridFromCode(covCode,'_hidLIMIT');
						var txtDwell = GetControlInGridFromCode('DWELL','_txtLIMIT');
						
						var amount;
						if(txtDwell != null)
						{
						  amount=ReplaceAll(txtDwell.value,',','');
						  amount=parseInt(amount) * .05;
						}
						if(lblTSPL != null)
						{
								lblTSPL.innerText=formatCurrency(amount);
								hidTSPL.value=amount;
						} 
/*						lblBR1143 =GetControlInGridFromCode(covCode,'_lblDEDUCTIBLE_AMOUNT');
						if( ddlAPD != null || lblBR1143 != null)
						{
							var ddlAmount =ddlAPD.options[ddlAPD.selectedIndex].text;
							lblBR1143.innerText=ddlAmount
							var hid=GetControlInGridFromCode('TSPL','_hidlbl_DEDUCTIBLE_AMOUNT')
							hid.value=ddlAmount + ' ' ;
						}
*/
					}
				}
				if (covCode == 'DP392')
				{
					if(cb.checked == true)
					{
						var lblDP392 =GetControlInGridFromCode(covCode,'_lblLIMIT');
						var hidDP392 =GetControlInGridFromCode(covCode,'_hidLIMIT');
						if(lblDP392 != null)
							{
								lblDP392.innerText='10,000';
								hidDP392.value=10000;
								
							}
						// added by Pravesh on 6 dec 2007 (itrack 3157)
						var cbDP382=GetControlInGridFromCode("DP382","_cbDelete");
						if(cbDP382 != null)
						{
						   cbDP382.checked=false;
						   GetControls(cbDP382.id);
						   SetHiddenField(cbDP382.id)
						}
						//	
					}
					else
						ChkNocoverage();
				}
				if (covCode == 'MSC480')
				{
				  SetMinSubsidence();
				  SetMinSubsidenceLimit();
				}
				/*
				//Builders Risk (DP-1143) START
				if (covCode == 'BR1143')
				{
					if(cb.checked == true)
					{
						lblBR1143 =GetControlInGridFromCode(covCode,'_lblDEDUCTIBLE_AMOUNT');
						if(lblBR1143 != null)
							{
								
								if( ddlAPD != null)
								{
										var ddlAmount =ddlAPD.options[ddlAPD.selectedIndex].text;
										lblBR1143.innerText=ddlAmount
										var hid=GetControlInGridFromCode('BR1143','_hidlbl_DEDUCTIBLE_AMOUNT')
										hid.value=ddlAmount + ' ' ;
								}
							}
						 
					}
				}
				//Builders Risk (DP-1143) END
				
				//Awnings, Canopies or Signs START
				if (covCode == 'ACS')
				{
					if(cb.checked == true)
					{
						lblBR1143 =GetControlInGridFromCode(covCode,'_lblDEDUCTIBLE_AMOUNT');
						if(lblBR1143 != null)
							{
								
								if( ddlAPD != null)
								{
										var ddlAmount =ddlAPD.options[ddlAPD.selectedIndex].text;
										lblBR1143.innerText=ddlAmount
										var hid=GetControlInGridFromCode('ACS','_hidlbl_DEDUCTIBLE_AMOUNT')
										hid.value=ddlAmount + ' ';
								}
							}
						 
					}
				}
				//Awnings, Canopies or Signs END
			   
			   //Building Improvements, Alterations & Additions  START
				if (covCode == 'BIAA')
				{
					if(cb.checked == true)
					{
						lblBR1143 =GetControlInGridFromCode(covCode,'_lblDEDUCTIBLE_AMOUNT');
						if(lblBR1143 != null)
							{
								
								if( ddlAPD != null)
								{
										var ddlAmount =ddlAPD.options[ddlAPD.selectedIndex].text;
										lblBR1143.innerText=ddlAmount
										var hid=GetControlInGridFromCode('BIAA','_hidlbl_DEDUCTIBLE_AMOUNT')
										hid.value=ddlAmount + ' ';
								}
							}
						 
					}
				}
				//Building Improvements, Alterations & Additions  END
				
				 //Radio & Television Equipment  START
				if (covCode == 'RTE')
				{
					if(cb.checked == true)
					{
						lblBR1143 =GetControlInGridFromCode(covCode,'_lblDEDUCTIBLE_AMOUNT');
						if(lblBR1143 != null)
							{
								
								if( ddlAPD != null)
								{
										var ddlAmount =ddlAPD.options[ddlAPD.selectedIndex].text;
										lblBR1143.innerText=ddlAmount
										var hid=GetControlInGridFromCode('RTE','_hidlbl_DEDUCTIBLE_AMOUNT')
										hid.value=ddlAmount + ' ';
								}
							}
						 
					}
				}
				//Radio & Television Equipment  END
				*/
			  
			}

			function DisableItems(strcbDelete, strlblNoLimit,strNoaddDEDUCTIBLE,strlblDEDUCTIBLE_AMOUNT,strddlAddDed,strtxtaddDEDUCTIBLE, strlblNoDeductible, strlblLimit, strddlDed, strtxtbox, prefix)
			{
				
				//alert(strcbDelete);
				lblNoLimit = document.getElementById(strlblNoLimit);
				ddlDed = document.getElementById(strddlDed);
				lblLimit = document.getElementById(strlblLimit);
				lblNoDeductible = document.getElementById(strlblNoDeductible);	
				cbDelete = document.getElementById(strcbDelete);
				txtDed = document.getElementById(strtxtbox);		
				strDedApplyName = prefix + '_lblIS_DEDUCT_APPLICABLE';
				strDedType = prefix + '_lblDEDUCTIBLE_TYPE';
				strDeductible = prefix + '_lblDEDUCTIBLE_AMOUNT';
				strDeductibleText = prefix + '_spnDEDUCTIBLE_TEXT';
				strspnDEDUCTIBLE_AMOUNT_TEXT = prefix + '_spnDEDUCTIBLE_AMOUNT_TEXT';
				
				lblNoAddded=document.getElementById(strNoaddDEDUCTIBLE);
				lblDEDUCTIBLE_AMOUNT=document.getElementById(strlblDEDUCTIBLE_AMOUNT);
				ddlAddDed=document.getElementById(strddlAddDed);
				txtAddDed=document.getElementById(strtxtaddDEDUCTIBLE);
				strAdddedType= prefix + '_lblAddDEDUCTIBLE_TYPE';
				

				lblDeductible = document.getElementById(strDeductible);
				//spnDEDUCTIBLE_TEXT = document.getElementById(strDeductibleText);
				spnDEDUCTIBLE_AMOUNT_TEXT =  document.getElementById(strspnDEDUCTIBLE_AMOUNT_TEXT);
				strhidlbl_DEDUCTIBLE_AMOUNT  = prefix  + '_hidlbl_DEDUCTIBLE_AMOUNT'
				
				hidlbl_DEDUCTIBLE_AMOUNT=document.getElementById(strhidlbl_DEDUCTIBLE_AMOUNT);
				
				
				DedType = document.getElementById(strDedType).innerText;
				AddDedType=document.getElementById(strAdddedType).innerText 
				var span = cbDelete.parentElement;
				var covCode = '';
												
				if ( span != null )
				{
					covCode = span.getAttribute("COV_CODE");
				}
				var lblDEDUCTIBLE=GetControlInGridFromCode(covCode,'_lblDEDUCTIBLE')
				
				
				//alert(DedType);
				if ( cbDelete.checked == true )
				{
					
				
					lblNoLimit.style.display = "none";
					lblNoAddded.style.display ="none";
					if(lblLimit != null)
					{
						lblLimit.style.display = "inline";
					}
					
					lblNoDeductible.style.display = "none";
					//lblNoDeductible.innerText = 'No Coverages';
					//if(ddlDed!=null)
					ddlDed.style.display = "none";
					
					if ( lblDeductible != null)
					{
						lblDeductible.style.display = "inline";
					}
					
					if ( spnDEDUCTIBLE_AMOUNT_TEXT != null )
					{
						spnDEDUCTIBLE_AMOUNT_TEXT.style.display = "inline";
					}
					//--------------------------------------------------------------------------
					switch(DedType)
					{
						case '0':
							lblNoDeductible.style.display = "none";
							if(lblDEDUCTIBLE != null)
							{
							 lblDEDUCTIBLE.style.display="inline";
							}
							ddlDed.style.display = "none";
							txtDed.style.display = "none";
							//DisableDeds(prefix);
							break;
						case '1':
							//Flat
							
						case '2':
							//Split
							if ( ddlDed.options.length == 0 )
							{
								lblNoDeductible.style.display = "inline";
								lblNoDeductible.innerText = 'No Coverages';
								ddlDed.style.display = "none";
							}
							else
							{
								lblNoDeductible.style.display = "none";
								lblNoDeductible.innerText = 'No Coverages';
								ddlDed.style.display = "inline";
								
							}
							txtDed.style.display = "none";
							break;
						case '3':
							//Open
								lblNoDeductible.style.display = "none";
								ddlDed.style.display = "none";
								txtDed.style.display = "inline";
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
						     lblNoAddded.style.display = "none";
						     lblDEDUCTIBLE_AMOUNT.style.display = "inline";
						    if ( covCode == "EDP469" && document.getElementById('hidStateId').value == "14") 
							{
							 
								lblDEDUCTIBLE_AMOUNT.innerText="10% - 250";
								lblDEDUCTIBLE_AMOUNT.style.display="inline";
								hidlbl_DEDUCTIBLE_AMOUNT.value="10 %-250";
							}
							if ( covCode == "EDP469" && document.getElementById('hidStateId').value == "22") 
							{
							 
								lblDEDUCTIBLE_AMOUNT.innerText="5% - 250";
								lblDEDUCTIBLE_AMOUNT.style.display="inline";
								hidlbl_DEDUCTIBLE_AMOUNT.value="5 %-250";
							}
							if ( covCode == "IF184" || covCode == "IFNSE") 
							{
							 
								lblDEDUCTIBLE_AMOUNT.innerText="500";
								lblDEDUCTIBLE_AMOUNT.style.display="inline";
								hidlbl_DEDUCTIBLE_AMOUNT.value="500";
							}
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
								lblNoAddded.innerText = 'No Coverages';
								ddlAddDed.style.display = "none";
							}
							else
							{
								lblNoAddded.style.display = "none";
								lblNoAddded.innerText = 'No Coverages';
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
					
					if (covCode == "MSC480" )
					{
						SetMinSubsidence();
					}
				}
				else
				{
					lblNoLimit.style.display = "inline";
					lblNoLimit.innerText = 'No Coverages';
					lblLimit.style.display = "none";
					
					lblNoDeductible.style.display = "inline";
					lblNoDeductible.innerText = 'No Coverages';
					lblLimit.style.display = "none";
					txtDed.style.display = "none";
					ddlDed.style.display="none";
					
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
					
					if ( lblDeductible != null)
					{
						lblDeductible.style.display = "none";
					}		
					
					if ( spnDEDUCTIBLE_AMOUNT_TEXT != null )
					{
						spnDEDUCTIBLE_AMOUNT_TEXT.style.display = "none";
					}
				}
				
			}
				
			
				//Runs when the text in additional column changes
			/*
			function OnAdditionalChange(strCovCode, txtBox)
			{
			
				if ( strCovCode == 'DWELL' || strCovCode == 'OSTR' || strCovCode == 'OSTR' || strCovCode == 'LPP' || strCovCode == 'RV' )
				{
					return;
				}
				
				if ( Page_IsValid == false ) return;
				
				lblDeductible = GetControlFromCode(strCovCode,'lblDEDUCTIBLE_AMOUNT');
				hidlbl_DEDUCTIBLE_AMOUNT = GetControlFromCode(strCovCode,'hidlbl_DEDUCTIBLE_AMOUNT');
				spnDEDUCTIBLE_AMOUNT_TEXT = GetControlFromCode(strCovCode,'spnDEDUCTIBLE_AMOUNT_TEXT');
				
				//Earthquake
				if ( strCovCode == 'EDP469')
				{
					if ( spnDEDUCTIBLE_AMOUNT_TEXT != null )
					{
						spnDEDUCTIBLE_AMOUNT_TEXT.innerText = '5%-';
						hidlbl_DEDUCTIBLE_AMOUNT.value = '500';
						lblDeductible.innerText = '500';
					}
					
					return;
				}
						
				if ( txtBox.value == '' )
				{
					if ( lblDeductible != null)
					{
						lblDeductible.innerText = '';
						lblDeductible.style.display = "none";
						
					}
					
					if ( hidlbl_DEDUCTIBLE_AMOUNT != null )
					{
						hidlbl_DEDUCTIBLE_AMOUNT.value = '';
					}
				}
				else
				{
					if ( lblDeductible != null )
					{
						lblDeductible.innerText = document.getElementById('hidALL_PERILL_DEDUCTIBLE_AMT').value;
						lblDeductible.style.display = "inline";	
					}
					
					if ( hidlbl_DEDUCTIBLE_AMOUNT != null )
					{
						//Installation Floater – Building Materials (IF-184) IF184
						//Installation Floater – Non-Structural Equipment (IF-184) IFNSE
						if ( strCovCode == 'IF184' || strCovCode == 'IFNSE')
						{
							hidlbl_DEDUCTIBLE_AMOUNT.value = '500';
							lblDeductible.innerText = '500';
							return;
						}
						
						var perill = document.getElementById('hidALL_PERILL_DEDUCTIBLE_AMT').value;
						
						//alert( isNaN(perill) );
						
						
						hidlbl_DEDUCTIBLE_AMOUNT.value = perill;
						
						
					}
					
				}
			}	*/	
		</script>
	</HEAD>
	<body oncontextmenu="return false;" MS_POSITIONING="GridLayout" onload="populateInfo();ChkNocoverage();">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr id="trError" runat="server">
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblError" runat="server" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<table cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td class="headereffectCenter">
									<webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow><asp:label id="lblTitle" runat="server">SECTION 1 COVERAGES</asp:label></td>
							</tr>
							<tr>
								<td align="center"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False" EnableViewState="False"></asp:label></td>
							</tr>
							<tr>
								<td class="midcolora"><asp:datagrid id="dgCoverages" runat="server" DataKeyField="COVERAGE_ID" AutoGenerateColumns="False"
										Width="100%">
										
										<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="Required /Optional" ItemStyle-Width="10%">
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
											<%--<asp:BoundColumn DataField="COV_DESC" HeaderText="Coverage" ItemStyle-Width="50%"></asp:BoundColumn>--%>
											<asp:TemplateColumn HeaderText="Coverage">
												<ItemTemplate>
													<asp:Label runat="server" ID="lblCOV_DESC" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Included" ItemStyle-Width="17%" ItemStyle-HorizontalAlign="Center">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<asp:TextBox ID="txtLIMIT" CssClass="INPUTCURRENCY" MaxLength="8" Visible="False" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INCLUDED","{0:,#,###}") %>'> CssClass="INPUTCURRENCY" MaxLength="6" onBlur="this.value=formatCurrency(this.value);"></asp:TextBox>
													</span><input type="hidden" id="hidLIMIT" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.INCLUDED","{0:,#,###}") %>' NAME="hidLIMIT">
													<select id="Dropdownlist1" Visible="false" Runat="server" NAME="Dropdownlist1"></select>
													<asp:label id="lblNoCoverageLimit" CssClass="labelfont" Runat="server">No Coverages</asp:label>
													<span id="lblLIMIT" class="LabelFont" Runat="server">
														<%# DataBinder.Eval(Container, "DataItem.INCLUDED_TEXT","{0:,#,###}") %>
													</span>
													<BR>
													<asp:RequiredFieldValidator ID="rfvLIMIT" Runat="server" Display="Dynamic" ControlToValidate="txtLIMIT" Enabled="False"
														ErrorMessage="This"></asp:RequiredFieldValidator>
													<asp:rangevalidator id="rngDWELLING_LIMIT" runat="server" ControlToValidate="txtLIMIT" Display="Dynamic"
														Type="Currency" MaximumValue="0" MinimumValue="0" ErrorMessage="Coverage A" Enabled="False"></asp:rangevalidator>
													<asp:RegularExpressionValidator ID="revLIMIT" Runat="server" ControlToValidate="txtLIMIT" Display="Dynamic"></asp:RegularExpressionValidator>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn Visible="False" HeaderText="Limit 2">
												<ItemTemplate>
													<asp:DropDownList ID="ddlLIMIT_2_TYPE" Runat="server"></asp:DropDownList>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Additional" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
												<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<select id="ddlDEDUCTIBLE" Visible="true" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_1") %>'>
													</select>
													<asp:TextBox CssClass="INPUTCURRENCY" MaxLength="6" id="txtDEDUCTIBLE_1_TYPE" Visible="true" Runat="server" onBlur="this.value=formatCurrency(this.value);" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_1","{0:,#,###}") %>'>
													</asp:TextBox>
													<asp:Label id="lblDEDUCTIBLE" CssClass="labelfont" Runat="server" visible="false"></asp:Label>
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
											<asp:TemplateColumn Visible="True" HeaderText="Deductible" ItemStyle-Width="17%" ItemStyle-HorizontalAlign="Center">
												<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<INPUT id="hidlbl_DEDUCTIBLE_AMOUNT"  type="hidden" value='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_TEXT") %> ' name="hidDEDUCTIBLE_AMOUNT" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
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
									</asp:datagrid><table cellSpacing="0" cellPadding="0" width="100%">
										<tr>
											<td class="headereffectCenter">
												<asp:label id="Label1" runat="server">SECTION 2 - LIABILITY COVERAGES (LP-124)</asp:label>
											</td>
										</tr>
										<tr>
											<td class="midcolora">
												<asp:datagrid id="dgSection2" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyField="COVERAGE_ID">
													
													<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
													<Columns>
														<asp:TemplateColumn ItemStyle-Width="10%">
															<ItemTemplate>
																<asp:CheckBox id="cbDelete2" runat="server" Checked="False" Enabled="True"></asp:CheckBox>
																<input type="hidden" id="hidcbDelete" value="" name="hidcbDelete" runat="server">
																<asp:Label ID="lblLIMIT_TYPE" style="display:none" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT_TYPE") %>'>
																</asp:Label>
																<asp:Label ID="lblDEDUCTIBLE_TYPE" style="display:none" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_TYPE") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<%--<asp:BoundColumn DataField="COV_DESC" HeaderText="Coverage" ItemStyle-Width="50%"></asp:BoundColumn>--%>
														<asp:TemplateColumn Visible="False" HeaderText="COV_ID">
															<ItemTemplate>
																<asp:Label ID="lblCov_Id2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Coverage">
															<ItemTemplate>
																<asp:Label runat="server" ID="lblCov_Des" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Included" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
															<ItemTemplate>
																<select id="ddlLIMIT" Visible="false" Runat="server" NAME="Select1" Onchange="ChkNocoverage();">
																</select>
																<asp:label id="lblLIMIT_AMOUNT" CssClass="labelfont" Visible="false" Runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INCLUDED_TEXT","{0:,#,###}") %>'></asp:label>
																<asp:label id="lblNoLimit" CssClass="labelfont" Visible="False" Runat="server">No Coverages</asp:label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Additional" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
															<ItemTemplate>
																<asp:label id="lblNoDeductible" CssClass="labelfont" Visible="True" Runat="server">No Coverages</asp:label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn Visible="True" HeaderText="Deductible">
															<ItemTemplate>
																<span id="spnDEDUCTIBLE_AMOUNT_TEXT" class="labelfont" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
																	<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_TEXT") %>
																</span><INPUT id="hidlbl_DEDUCTIBLE_AMOUNT"  type="hidden" value='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE") %>' name="hidDEDUCTIBLE_AMOUNT" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
																<asp:Label ID="lblDEDUCTIBLE_AMOUNT" CssClass="labelfont" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE") %>' COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn Visible="True" HeaderText="Code">
															<ItemTemplate>
																<asp:Label ID="lblCOV_CODE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
												</asp:datagrid>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td>
									<table width="100%">
										<tr>
											<td class="midcolora" colSpan="3">
												<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Visible="False" Text="Delete" Enabled="False"></cmsb:cmsbutton></td>
											<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td><INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidREC_VEH_ID" type="hidden" value="0" name="hidREC_VEH_ID" runat="server">
									<INPUT id="hidPolID" type="hidden" name="hidPolID" runat="server"> <INPUT id="hidPolVersionID" type="hidden" name="hidPolVersionID" runat="server">
									<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"> <INPUT id="hidPol_LOB" type="hidden" value="0" name="hidPol_LOB" runat="server">
									<INPUT id="hidOldXml" type="hidden" name="hidOldXml" runat="server"> <INPUT id="hidPolcyType" type="hidden" name="hidPolcyType" runat="server">
									<INPUT id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server"> <INPUT id="hidROW_COUNT" type="hidden" value="0" name="hidPol_LOB" runat="server">
									<INPUT id="hidALL_PERILL_DEDUCTIBLE_AMT" type="hidden" name="hidALL_PERILL_DEDUCTIBLE_AMT" runat="server">
									<INPUT id="hidDataValue1" type="hidden" name="hidDataValue1" runat="server">
									<INPUT id="hidDataValue2" type="hidden" name="hidDataValue2" runat="server">
									<INPUT id="hidCustomInfo" type="hidden" name="hidCustomInfo" runat="server">									
									<input id="hidControlXML" type="hidden" name="hidControlXML" runat="server">
									<input id="hidStateId" type="hidden" name="hidControlXML" runat="server">
									
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>

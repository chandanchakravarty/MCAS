<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" Codebehind="PolicyWatercraftCoverages.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Watercrafts.PolicyWatercraftCoverages" ValidateRequest = "false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Coverages</title>
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
			var prefix = "dgCoverages_ctl";
			
			var ShowSaveMsgAlways = true;
			var xmlDoc = new ActiveXObject("Microsoft.XMLDOM") ; 
			var firstTime = false;
							
			function DisableCheckBox(checkBox,checkState, disableState, parentRowState)
			{
				if ( checkBox != null )
				{
					checkBox.checked = checkState;
					checkBox.disabled = disableState;
					checkBox.parentElement.parentElement.parentElement.disabled = parentRowState;
					DisableControls(checkBox.id);
					SetHiddenField(checkBox.id);
				}
			}
			
		/*function populateAdditionalInfo(NodeName)
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
			
			document.getElementById('hidDataValue1').value =populateAdditionalInfo("MAKE");
			document.getElementById('hidDataValue2').value =populateAdditionalInfo("MODEL");					
			if(document.getElementById('hidDataValue1').value=='undefined')
				document.getElementById('hidDataValue1').value="";
			if(document.getElementById('hidDataValue2').value=='undefined')
				document.getElementById('hidDataValue2').value="";
			document.getElementById('hidCustomInfo').value=";Boat # = " + populateAdditionalInfo("BOAT_ID") + ";Boat Make = " + document.getElementById('hidDataValue1').value + ";Boat Model = " + document.getElementById('hidDataValue2').value;
			//alert(document.getElementById('hidCustomInfo').value);
			
		}
			*/
			
				
			//Displays the controls in a row according to coverage and deductible type	
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
				strlblLIMIT_AMOUNT = prefix + '_lblLIMIT_AMOUNT';
				strlblDEDUCTIBLE_AMOUNT = prefix + '_lblDEDUCTIBLE_AMOUNT';
				//strSignatureObtained= prefix + '_ddlSignatureObtained';
				//strlblSigObt= prefix + '_lblSigObt';
				
				
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
				lblLIMIT_AMOUNT = document.getElementById(strlblLIMIT_AMOUNT);
				lblDEDUCTIBLE_AMOUNT = document.getElementById(strlblDEDUCTIBLE_AMOUNT);
				
								
				strrevLimit = prefix + '_revLIMIT';
				strrevDed = prefix + '_revDEDUCTIBLE';
				
				//alert(strrevLimit);	
				revLimit = document.getElementById(strrevLimit);
				revDeductible = document.getElementById(strrevDed);	
				
				
				//alert(prefix + 'revLIMIT');	
				//alert('dsf' + revLimit);	
				//alert("aa" + strLimitApply);
				
				if ( strLimitApply == '0')
				{
					DisableLimits(prefix);
				}
				
				if ( strDedApply == '0' )
				{
					DisableDeds(prefix);
					//return;
				}
				
				if ( cbDelete.checked == true )
				{
				
					//alert('po' + lblLimit);
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
									if ( lblLimit != null )
									{
										lblLimit.style.display = "inline";
										lblLimit.innerText = 'N.A';
									}
									
									if ( ddlLimit != null )
									{
										ddlLimit.style.display = "none";
									}
									
								}
								else
								{
									if ( lblLimit != null )
									{
										lblLimit.style.display = "none";
										lblLimit.innerText = 'N.A';
									}
									
									if ( ddlLimit != null )
									{
										ddlLimit.style.display = "inline";
									}
								}
								
								if ( txtLimit != null )
								{
									txtLimit.style.display = "none";
								}
								
								break;	
							case '3':
								//Open
								//Show Textbox or label
								
								if ( lblLIMIT_AMOUNT != null )
								{
									lblLIMIT_AMOUNT.style.display = "inline";
								}
								
								if ( lblLimit != null )
								{
									lblLimit.style.display = "none";
								}
								
								if ( ddlLimit != null )
								{
									ddlLimit.style.display = "none";
								}
								
								if ( txtLimit != null )
								{
									txtLimit.style.display = "inline";
									ValidatorEnable(revLimit,true);
								}
								
								break;		
								
					}
					
					switch(strDedType)
					{
						case '0':
							//Show "No Coverages.
							//DisableDeds(prefix);
							SetEmptyDeds(prefix);
							break;
						case '1':
							//Flat
							//Show Dropdown
						case '2':
							//Split
							//Show Dropdown
						
							if ( ddlDed != null )
							{
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
							}
							
							if ( ddlDed == null )
							{
								lblDed.style.display = "none";
								lblDed.innerText = 'N.A';
								//ddlDed.style.display = "inline";
										
							}
							
							if ( txtDed != null )
							{
								txtDed.style.display = "none";
							}
							
							if ( lblDEDUCTIBLE_AMOUNT != null )
							{
								lblDEDUCTIBLE_AMOUNT.style.display = "inline";
							}
							break;
						case '3':
							//Open
							//Show Textbox or label
							
								if ( lblDEDUCTIBLE_AMOUNT != null )
								{
									lblDEDUCTIBLE_AMOUNT.style.display = "inline";
								}
								
								if ( lblDed != null)
								{
									lblDed.style.display = "none";
								}
								
								if ( ddlDed != null )
								{
									ddlDed.style.display = "none";
								}
								
								if ( txtDed != null )
								{
									txtDed.style.display = "inline";
									ValidatorEnable(revDeductible,true);
								}
							break;
					}
									
				}
				else	//IF CHECKED == FALSE
				{
					
					DisableLimits(prefix);
					DisableDeds(prefix);
					
							
				}
						
			}
			//**********************************************************
			
			//Sets the visibility fo Limits dropdowns
			function DisableLimits(prefix)
			{
					if ( lblLimit != null )
					{
						lblLimit.style.display = "inline";
						lblLimit.innerText = 'No Coverages';
					}
					
				
					
					if ( lblLIMIT_AMOUNT != null )
					{
						lblLIMIT_AMOUNT.style.display = "none";
					}
								
					if ( ddlLimit != null )
					{
						ddlLimit.style.display = "none";
					}
					
					if ( txtLimit != null )
					{
						txtLimit.style.display = "none";
					}
					
				
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
					lblLimit.style.display = "none";
					ddlLimit.style.display = "none";
					txtLimit.style.display = "none";
					if ( revLimit != null )
					{
						ValidatorEnable(revLimit,false);
					}
			}
			function SetEmptyDeds(prefix)
			{
					lblDed.style.display = "none";
					ddlDed.style.display = "none";
					txtDed.style.display = "none";
					if ( revDeductible != null )
					{
						ValidatorEnable(revDeductible,false);
					}
			}
			/////////////////////////////////////////////////
			////Sets the visibility of Deductibles dropdowns
			function DisableDeds(prefix)
			{
					
					
					if ( lblDed != null )
					{
						lblDed.style.display = "inline";
						lblDed.innerText = 'No Coverages';
					}
					
					if ( lblDEDUCTIBLE_AMOUNT != null )
					{
						lblDEDUCTIBLE_AMOUNT.style.display = "none";
					}
					
					if ( ddlDed != null)
					{
						ddlDed.style.display = "none";
					}
					
					if ( txtDed != null )
					{
						txtDed.style.display = "none";
					}
				
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
				window.open(myUrl,'','toolbar=no, directories=no, location=no, status=yes, menubar=no, resizable=yes, scrollbars=no,  width=800, height=500'); 
				return false;		
			}
			
			function GetCheckBoxID(intCovId,status)
			{
				var ctr = 0;
				
				var rowCount = document.Form1.hidROW_COUNT.value;
				
				//alert(rowCount);
				for (ctr = 0; ctr < rowCount; ctr++)
				{
					var label = document.getElementById(prefix + ctr + "_lblCOV_ID");
					
					if ( label != null )
					{
						//alert(label.innerText + '=' + intCovId);
						if ( label.innerText == intCovId )
						{
							
							//Dropdown list
							var ddl = document.getElementById(prefix + ctr + "_ddlLIMIT");
							
							//Check box
							var cb = document.getElementById(prefix + ctr + "_cbDelete");
							
							
							if( ddl != null )
							{
								if ( status == true )
								{
									ddl.disabled = false;
								}
								else
								{
									ddl.disabled = true;
								}
							}
							
							
							
							if( cb != null )
							{	
								/*
								if ( status == true )
								{
									cb.checked = false;
								}
								else
								{
									cb.checked = true;
								}
								*/
								return cb.id;
							}
							
							
						
						}
					}
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
					//alert(hidField.value);
				}
				
				//return hidField;
				
			}
			
			
				
				
			function EnableDisableRow(tr, action)
			{
				for( var i = 1; i < tr.cells.length; i++)
				{
					
					tr.cells[i].disabled = action;
				}
			}
			
			
			//Back up for Coverage rule.xml***************************
			function onButtonClick(chk)
			{
				
				if ( chk == null ) return;
				
				SetHiddenField(chk.id);
				
				if ( chk.disabled == true && chk.checked == false) return;
				
				var lobState = document.getElementById('hidLOBState').value;
				var span = chk.parentElement;
				var covID = 0;
				
				if ( span == null ) return;
				
				var covID = span.getAttribute("COV_ID");
				var covCode = span.getAttribute("COV_CODE");
				
				
				
				
				
				if ( lobState == 'WATIndiana' || lobState == 'WATMichigan' || lobState == 'WATWisconsin' || lobState == 'UMBIndiana' || lobState == 'UMBMichigan' || lobState == 'UMBWisconsin' || lobState == 'HOMEMichigan' || lobState == 'HOMEIndiana')
				{
					 //Check For Insure value and Default Accordingly
				    if ( covCode == 'EBPPDACV')
				    {
				      if ( chk.checked == true )
					  {
							ddl = GetControlInGridFromCode('EBPPDACV','_ddlDEDUCTIBLE');
							//OnDDLChange(ddl);
				      }
				    }
				    if ( covCode == 'EBPPDAV')
				    {
				      if ( chk.checked == true )
					  {
							ddl = GetControlInGridFromCode('EBPPDAV','_ddlDEDUCTIBLE');
							//OnDDLChange(ddl);
				      }
				    }
					if ( covCode == 'EBSMT')
					{
						if ( chk.checked == true )
						{		
							//Get Actual or Agreed
							chkActual = GetControlInGridFromCode('EBPPDACV','_cbDelete');
							chkAgreed = GetControlInGridFromCode('EBPPDAV','_cbDelete');
							chkJet    = GetControlInGridFromCode('EBPPDJ','_cbDelete');
							/*
							if ( chkActual != null )
							{
								if ( chkActual.checked == true )
								{
									//alert('1');
									ddl = GetControlInGridFromCode('EBPPDACV','_ddlDEDUCTIBLE');
								}
							}
							
							if ( chkAgreed != null )
							{
								if ( chkAgreed.checked == true )
								{
									//alert('2');
									ddl = GetControlInGridFromCode('EBPPDAV','_ddlDEDUCTIBLE');
								}
							}
							
							if ( chkJet != null )
							{
								if ( chkJet.checked == true )
								{
									
									ddl = GetControlInGridFromCode('EBPPDJ','_ddlDEDUCTIBLE');
								}
							} */
							
						
							//Get the label for this ddl
							var label = GetControlInGridFromCode('EBSMT','_lblDEDUCTIBLE_AMOUNT');
							
							if ( label != null )
							{
								//alert(label.innerText);
								label.style.display = "inline";
								
								if ( firstTime == false )
								{
									if ( ddl != null )
									{
										OnDDLChange(ddl);
									}
								}
							}
						}
					}
					
					//start
					//Get the Trailer - Jet Ski checkbox
					if ( covCode == 'EBSMETJ')
					{
						if ( chk.checked == true )
						{
													
							chkJet    = GetControlInGridFromCode('EBPPDJ','_cbDelete');
							
							if ( chkJet != null )
							{
								if ( chkJet.checked == true )
								{
									
									//ddl = GetControlInGridFromCode('EBPPDJ','_ddlDEDUCTIBLE');
								}
							}
							
						
							//Get the label for this ddl
							var label = GetControlInGridFromCode('EBSMETJ','_lblDEDUCTIBLE_AMOUNT');
							
							if ( label != null )
							{
								//alert(label.innerText);
								label.style.display = "inline";
								
								if ( firstTime == false )
								{
									if ( ddl != null )
									{
										OnDDLChange(ddl);
									}
								}
							}
						}
					}
					
					
					  
			
					
					
					
					
					//end
					 var ddl
					 if ( covCode == 'BRCC')
					   {
					    if ( chk.checked == true )
						 {	
							var	chkDav    = GetControlInGridFromCode('EBPPDAV','_cbDelete');
							var	chkDacv    = GetControlInGridFromCode('EBPPDACV','_cbDelete');
							var	chkPdj    = GetControlInGridFromCode('EBPPDJ','_cbDelete');
							/*
							if ( chkDav != null )
								{
									if ( chkDav.checked == true )
									{
										
										ddl = GetControlInGridFromCode('EBPPDAV','_ddlDEDUCTIBLE');
									}
								}
								
								
								
								if ( chkDacv != null )
								{
									if ( chkDacv.checked == true )
									{
										
										ddl = GetControlInGridFromCode('EBPPDACV','_ddlDEDUCTIBLE');
									}
								}
								
								
								
								if ( chkPdj != null )
								{
									if ( chkPdj.checked == true )
									{
										
										ddl = GetControlInGridFromCode('EBPPDJ','_ddlDEDUCTIBLE');
									}
								} */
							  
								//Get the label for this ddl
								
								var label = GetControlInGridFromCode('BRCC','_lblDEDUCTIBLE_AMOUNT');
								
								if ( label != null )
								{
									//alert(label.innerText);
									label.style.display = "inline";
								 
									//if ( firstTime == false )
									//{
										if ( ddl != null )
										{
										    
											OnDDLChange(ddl);
										}
								//	}
								}
							}
					}
					 
			
					
					
					//alert(covCode);
					//Section I - Covered Property, Physical Damage - Actual cash value 
					
					
					
					
					if ( covCode == 'EBPPDACV')
					{
				
						var cbEBPPDAV = GetControlInGridFromCode('EBPPDAV','_cbDelete');	
						
						if ( chk.checked == true )
						{
							chk.disabled = false;
							EnableDisableRow(chk.parentElement.parentElement.parentElement,false);
							
							if ( cbEBPPDAV != null )
							{
								cbEBPPDAV.checked = false;
								//EnableDisableRow(cbEBPPDAV.parentElement.parentElement.parentElement,false);
								cbEBPPDAV.disabled = true;
								//cbEBPPDAV.parentElement.parentElement.parentElement.disabled = true;
								
								DisableControls(cbEBPPDAV.id);
								SetHiddenField(cbEBPPDAV.id);
							}
						}
						else
						{
							
							chk.disabled = true;
							EnableDisableRow(chk.parentElement.parentElement.parentElement,true);
							SetHiddenField(chk.id);
							
							if ( cbEBPPDAV != null )
							{
								//cbEBPPDAV.checked = true;
								EnableDisableRow(cbEBPPDAV.parentElement.parentElement.parentElement,false);
								cbEBPPDAV.disabled = false;
								//cbEBPPDAV.parentElement.parentElement.parentElement.disabled = false;
								
								DisableControls(cbEBPPDAV.id);
								SetHiddenField(cbEBPPDAV.id);
							}
						}
						
					}//End of Section I - Covered Property, Physical Damage - Actual cash value 
					
					//Section I - Covered Property, Physical Damage - Agreed Value
					if ( covCode == 'EBPPDAV')
					{
						
						var cbEBPPDACV = GetControlInGridFromCode('EBPPDACV','_cbDelete');	
						
						if ( chk.checked == true )
						{
							chk.disabled = false;
							EnableDisableRow(chk.parentElement.parentElement.parentElement,false);
							
							if ( cbEBPPDACV != null )
							{
								cbEBPPDACV.checked = false;
								//EnableDisableRow(cbEBPPDACV.parentElement.parentElement.parentElement,true);
								cbEBPPDACV.disabled = true;
								//cbEBPPDACV.parentElement.parentElement.parentElement.disabled = true;
								
								DisableControls(cbEBPPDACV.id);
								SetHiddenField(cbEBPPDACV.id);
							}
						}
						else
						{
							chk.disabled = true;
							EnableDisableRow(chk.parentElement.parentElement.parentElement,true);
							SetHiddenField(chk.id);
							
							if ( cbEBPPDACV != null )
							{
								//cbEBPPDACV.checked = true;
								EnableDisableRow(cbEBPPDACV.parentElement.parentElement.parentElement,false);
								cbEBPPDACV.disabled = false;
								//cbEBPPDACV.parentElement.parentElement.parentElement.disabled = false;
								
								DisableControls(cbEBPPDACV.id);
								SetHiddenField(cbEBPPDACV.id);
							}
						}
							
					}//End of Section I - Covered Property, Physical Damage - Agreed Value
					
					//Section II - Liability (CSL) 
					if ( covCode == 'LCCSL')
					{
						var ddlLCCSL= GetControlInGridFromCode('LCCSL','_ddlLIMIT')
						OnDDLChange(ddlLCCSL);
						
						var cbMCPAY = GetControlInGridFromCode('MCPAY','_cbDelete');	
						var cbUMBCS = GetControlInGridFromCode('UMBCS','_cbDelete');	
						if ( chk.checked == true )
						{
							
							if ( cbMCPAY != null )
							{
								//Section II - Medical 
								cbMCPAY.checked = true;
								EnableDisableRow(cbMCPAY.parentElement.parentElement.parentElement,false);
								//cbMEDEXC.parentElement.parentElement.parentElement.disabled = true;
								cbMCPAY.disabled = true;
								DisableControls(cbMCPAY.id);
								SetHiddenField(cbMCPAY.id);
							}
							
							
							//Uninsured Boaters 
							if ( cbUMBCS != null )
							{
								cbUMBCS.checked = true;
								EnableDisableRow(cbUMBCS.parentElement.parentElement.parentElement,false);
								//cbMEDEXC.parentElement.parentElement.parentElement.disabled = true;
								cbUMBCS.disabled = true;
								DisableControls(cbUMBCS.id);
								SetHiddenField(cbUMBCS.id);
							}
							
						}
						else
						{
							//Section II - Medical 
							if ( cbMCPAY != null )
							{
								cbMCPAY.checked = false;
								EnableDisableRow(cbMCPAY.parentElement.parentElement.parentElement,true);
								//cbMEDEXC.parentElement.parentElement.parentElement.disabled = true;
								cbMCPAY.disabled = true;
								DisableControls(cbMCPAY.id);
								SetHiddenField(cbMCPAY.id);
							}
							
							//Section II - Uninsured Watercraft Liability (CSL)
							if ( cbUMBCS != null )
							{
								cbUMBCS.checked = false;
								EnableDisableRow(cbUMBCS.parentElement.parentElement.parentElement,true);
								//cbMEDEXC.parentElement.parentElement.parentElement.disabled = true;
								cbUMBCS.disabled = true;
								DisableControls(cbUMBCS.id);
								SetHiddenField(cbUMBCS.id);
							}
							
						}
					}//End of Section II - Liability (CSL)
					
					//Boat Towing and Emergency Service Coverage
					if ( covCode == 'BTESC')
					{
						chk.disabled = true;
						
					//alert(chk.parentElement.parentElement.parentElement);
						//EnableDisableRow(chk.parentElement.parentElement.parentElement,true);
					}
					//End of Boat towing///////////////////////
					
					// Added by Praveen for disabling the chkbox
					if ( covCode == 'EBIUE')
					{
						//chk.checked=true;
						chk.disabled = true;
					}
					
					
					
				}			
			}
				//End of Watercraft rules*****************************************************
			
			//Runs when the Deductible options change********************************
			function OnDDLChange(ddl)
			{
			
				var COV_CODE = '';
				if (ddl==null) return;
				if ( ddl.getAttribute('COV_CODE') == null ) return;
				var IS_JETSKI;
				if(GetControlInGridFromCode('EBPPDJ','_cbDelete')== null)
					IS_JETSKI = false;
				else
					IS_JETSKI = true;
					
				COV_CODE = ddl.getAttribute('COV_CODE');
				//to set medical limit to "Extended from HO" if Liability limit is selected to "Extended from HO"
				if(COV_CODE	=="LCCSL")
				{
					var limitID=ddl.options[ddl.selectedIndex].value;
					var MedicalDDl = GetControlInGridFromCode('MCPAY','_ddlLIMIT')
					var hidLIMIT_SEL_INDEX = GetControlInGridFromCode('MCPAY','_hidLIMIT_SEL_INDEX')
					var hidLIMIT_SEL_INDEX_LCCSL = GetControlInGridFromCode('LCCSL','_hidLIMIT_SEL_INDEX')
					hidLIMIT_SEL_INDEX_LCCSL.value=ddl.selectedIndex;
					if (limitID=="1412" || limitID=="1413" || limitID=="1414")
					{
						if (MedicalDDl!=null)
						{
							for ( i=0;i<MedicalDDl.options.length;i++)
							{
								if (MedicalDDl.options[i].value=="1415" || MedicalDDl.options[i].value=="1416"  ||MedicalDDl.options[i].value=="1417")
								{
									MedicalDDl.options.selectedIndex=i;
									hidLIMIT_SEL_INDEX.value=i;
									MedicalDDl.disabled=true;
									break;
								}
							}
						}
					}
					else
					{
					    if(MedicalDDl.options[MedicalDDl.options.selectedIndex].value=="1415" || MedicalDDl.options[MedicalDDl.options.selectedIndex].value=="1416" || MedicalDDl.options[MedicalDDl.options.selectedIndex].value=="1417")
					    {
						MedicalDDl.options.selectedIndex=0;
						hidLIMIT_SEL_INDEX.value=0;
						MedicalDDl.disabled=false;
						}
						else
						hidLIMIT_SEL_INDEX.value=MedicalDDl.options.selectedIndex;
					}
				return;	
				}
				//if Liability limit is not selected to "Extended from HO" then medical not alowed to limit "Extended from HO" 
				if(COV_CODE	=="MCPAY")
				{
					var limitID=ddl.options[ddl.selectedIndex].value;
					var liabilityDDl = GetControlInGridFromCode('LCCSL','_ddlLIMIT')
					var hidLIMIT_SEL_INDEX = GetControlInGridFromCode('MCPAY','_hidLIMIT_SEL_INDEX')
					var liabilityID=liabilityDDl.options[liabilityDDl.selectedIndex].value;
					if((limitID=="1415" || limitID=="1416" || limitID=="1417") && !(liabilityID=="1412" ||liabilityID=="1413" || liabilityID=="1414"  ))
					  {
					     ddl.selectedIndex=0;
					     hidLIMIT_SEL_INDEX.value=0;
					  }
					 else
					   hidLIMIT_SEL_INDEX.value= ddl.selectedIndex;
				return;	
				}
				
				//Section 1 - Covered Property Damage - Actual Cash Value
				//Section 1 - Covered Property Damage - Agreed Cash Value
			 if(COV_CODE=="BDEDUC") 
			 {
					
				if ( IS_JETSKI == false)
				{
					//If insuring Value is over $10,000 the $100 ded is not available *
					var ddlAmount = ddl.options[ddl.selectedIndex].text;
					ddlAmount = ReplaceAll(ddlAmount,',','');
					// If Grandfather is applicable for Actual Cash Value/Agreed value then ignore the rule
						//If insuring Value is over $10,000 the $100 ded is not available *
					if(document.getElementById('hidIsGrandFather').value == "1")
					{
						if(parseInt(document.getElementById('hidInsureValue').value) >= 10000 && parseInt(document.getElementById('hidInsureValue').value) < 25000 && parseInt(ddlAmount) <= 100)
						{
       						SelectDropdownOptionByText(ddl,"250")
						}
						//If insuring Values is over $25,000 ded is $500 or greater *
						if(parseInt(document.getElementById('hidInsureValue').value) >= 25000 && parseInt(ddlAmount) < 500)
						{
	     					SelectDropdownOptionByText(ddl,"500")
						}
					}	
					//Get the trailer Checkbox
					
					var cb = GetControlInGridFromCode('EBSMT','_cbDelete');
					
					if ( cb != null )
					{
						if ( cb.checked == true )
						{
							//Get the Trailer Label
							var trailerLabel = GetControlInGridFromCode('EBSMT','_lblDEDUCTIBLE_AMOUNT');
							if ( trailerLabel != null )
							{
								trailerLabel.style.display = "inline";
								trailerLabel.innerText = ddl.options[ddl.selectedIndex].text;
								trailerLabel.innerHTML = ddl.options[ddl.selectedIndex].text;
								
								
								document.getElementById('hidTrailer').value = ddl.options[ddl.selectedIndex].text;
							}
						}
					}
					
				 //Get Boat replacement Checkbox
					var cbBRCC = GetControlInGridFromCode('BRCC','_cbDelete');
					
					
					if ( cbBRCC != null )
					{
						if ( cbBRCC.checked == true )
						{
							//Get the Trailer Label
							var brccLabel = GetControlInGridFromCode('BRCC','_lblDEDUCTIBLE_AMOUNT');
							
							
							
							if ( brccLabel != null )
							{
								brccLabel.style.display = "inline";
								brccLabel.innerText = ddl.options[ddl.selectedIndex].text;
								brccLabel.innerHTML = ddl.options[ddl.selectedIndex].text;
								//alert(trailerLabel.innerText);
								
								document.getElementById('hidTrailer').value = ddl.options[ddl.selectedIndex].text;
							}
						}
					}
				 
				}
				
				//Section 1 - Covered Property Damage Jet Ski
				
				if ( IS_JETSKI == true)
				{
					
					//Get the Trailer - Jet Ski checkbox
					var cb = GetControlInGridFromCode('EBSMETJ','_cbDelete');
					
					if ( cb != null )
					{
						if ( cb.checked == true )
						{
							//Get the Trailer Label
							var trailerLabel = GetControlInGridFromCode('EBSMETJ','_lblDEDUCTIBLE_AMOUNT');
								
							if ( trailerLabel != null )
							{
								trailerLabel.style.display = "inline";
								trailerLabel.innerText = ddl.options[ddl.selectedIndex].text;
								trailerLabel.innerHTML = ddl.options[ddl.selectedIndex].text;
								document.getElementById('hidTrailerJet').value = ddl.options[ddl.selectedIndex].text;
							}
						}
					}
					
				}
			}	
				
		}
				
		</script>
</HEAD>
	<body leftMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td class="headereffectCenter"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow><asp:label id="lblTitle" runat="server">Coverages</asp:label></td>
				</tr>
				<tr>
					<td align="center"><asp:label id="lblMessage" runat="server" EnableViewState="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<tr>
					<td class="headerEffectSystemParams">Boat Level Coverages</td>
				</tr>
				<tr>
					<td class="midcolora"><asp:datagrid id="dgCoverages" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyField="COVERAGE_ID">
							
							<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Required /Optional" ItemStyle-Width="10%">
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
										<select id="ddlLIMIT" Visible="True" Runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' onchange="javascript:OnDDLChange(this);" >
										</select>
										<input type="hidden" id="hidLIMIT_SEL_INDEX" name="hidLIMIT_SEL_INDEX" runat="server">
										<asp:label id="lblLIMIT_AMOUNT" CssClass="labelfont" Runat="server" Visible="False"></asp:label>
										<asp:label id="lblLIMIT" CssClass="labelfont" Runat="server">N.A.</asp:label>
										<asp:TextBox ID="txtLIMIT" Runat="server" CssClass="INPUTCURRENCY" MaxLength="10"></asp:TextBox>
										<BR>
										<asp:RegularExpressionValidator ID="revLIMIT" Enabled="False" Runat="server" ControlToValidate="txtLIMIT" Display="Dynamic"></asp:RegularExpressionValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Deductible" ItemStyle-Width="20%">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<select id="ddlDEDUCTIBLE" Visible="True" Runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' onchange="javascript:OnDDLChange(this);">
										</select>
										<asp:Label id="lblDEDUCTIBLE" CssClass="labelfont" Runat="server">N.A.</asp:Label>
										<asp:Label id="lblDEDUCTIBLE_AMOUNT" CssClass="labelfont" Runat="server" Visible="False" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
										</asp:Label>
										<asp:TextBox ID="txtDEDUCTIBLE" Runat="server" CssClass="INPUTCURRENCY" MaxLength="10"></asp:TextBox>
										<BR>
										<asp:RegularExpressionValidator ID="revDEDUCTIBLE" Enabled="False" Runat="server" ControlToValidate="txtDEDUCTIBLE" 
 Display="Dynamic"></asp:RegularExpressionValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Sig. Obt." ItemStyle-Width="10%">
									<ItemTemplate>
										<asp:DropDownList ID="ddlSignatureObtained" Visible="True" Runat="server">
											<asp:ListItem Value='N'>No</asp:ListItem>
											<asp:ListItem Value='Y'>Yes</asp:ListItem>
										</asp:DropDownList>
										<asp:label id="lblSigObt" CssClass="labelfont" Runat="server" Visible="False">N.A.</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:Label ID="lblCOV_CODE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td class="midcolora">##Any changes to this coverage will be reflected for all other boats as well
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
						<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"> <INPUT id="hidROW_COUNT" type="hidden" value="0" name="hidAPP_LOB" runat="server">
						<INPUT id="hidCoverageXML" type="hidden" name="hidCoverageXML" runat="server"> <INPUT id="hidLOBState" type="hidden" name="hidLOBState" runat="server">
						<INPUT id="hidMotorcycleType" type="hidden" name="hidMotorcycleType" runat="server">
						<INPUT id="hidTYPE_OF_WATERCRAFT" type="hidden" name="hidTYPE_OF_WATERCRAFT" runat="server">
						<INPUT id="hidCustomInfo" type="hidden" name="hidCustomInfo" runat="server"> <INPUT id="hidTrailer" type="hidden" name="hidTrailer" runat="server">
						<INPUT id="hidTrailerJet" type="hidden" name="hidTrailerJet" runat="server"> <INPUT id="hidPOLICY_ROW_COUNT" type="hidden" value="0" name="hidPOLICY_ROW_COUNT" runat="server">
						<input id="hidType" type="hidden" name="hidType" runat="server"> <input id="hidControlXML" type="hidden" name="hidControlXML" runat="server">
						<input id="hidInsureValue" type="hidden" name="hidInsureValue" runat="server">
						<input id="hidIsGrandFather" type="hidden" name="hidIsGrandFather" value="0" runat="server">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

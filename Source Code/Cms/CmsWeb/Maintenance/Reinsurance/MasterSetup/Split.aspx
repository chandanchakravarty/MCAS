	<%@ Page language="c#" Codebehind="Split.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup.Split" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Split</title>
		<meta content="False" name="vs_showGrid">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>
	 
		<script language="javascript" type="text/javascript">

			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";

			function clear(objSelect)
			{
				for (var i = (objSelect.options.length-1); i >= 0; i--){
				 objSelect.options[i]=null;
				}
			}
			
			function AddData()
			{
			ChangeColor();
			DisableValidators();
			//return;
			
				if(document.getElementById('hidStateChange').value!='1')
				{
					document.getElementById('txtREIN_EFFECTIVE_DATE').value='';
							
					document.getElementById('hidREIN_SPLIT_DEDUCTION_ID').value	=	'New';
					
					document.getElementById('cmbREIN_LINE_OF_BUSINESS').options.selectedIndex = 0;
					document.getElementById('cmbREIN_STATE').options.selectedIndex = 0;
					clear(document.getElementById('cmbFROMREIN_COVERAGE'));
					clear(document.getElementById('cmbREIN_COVERAGE'));
					clear(document.getElementById('cmbREIN_IST_SPLIT_COVERAGE'));
					clear(document.getElementById('cmb2ndRECIPIENTS'));
					clear(document.getElementById('cmbFROMREIN_1ST_SPLIT_COVERAGE'));
					clear(document.getElementById('cmbFROMREIN_2ND_SPLIT_COVERAGE'));
					
					document.getElementById('txtREIN_IST_SPLIT').value  = '';
					document.getElementById('txtREIN_2ND_SPLIT').value  = '';
					
					ChangeColor();
				}	
					
			if(document.getElementById('btnActivateDeactivate'))
				document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);	
			}


			function populateXML()
			{
			//ResetAfterActivateDeactivate();	
			//if(document.getElementById("cmbREIN_LINE_OF_BUSINESS").style.display =="inline")		
			//	document.getElementById("cmbREIN_LINE_OF_BUSINESS").focus();
			    
				if(document.getElementById('hidLOBChange').value=='1')
				{
					document.getElementById('hidLOBChange').value='0';
					return;
				}
				if(document.getElementById('hidStateChng').value=='1')
				{
					document.getElementById('hidStateChng').value='0';
					document.getElementById('cmbREIN_STATE').focus();
					return;
				}
				var tempXML;
				tempXML=document.getElementById("hidOldData").value;
				if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
				{
				    
					if(tempXML!="" && tempXML!="0") {
					    var REIN_EFFECTIVE_DATE = $("#txtREIN_EFFECTIVE_DATE").val();

					    populateFormData(tempXML, "Split");
//					    $("#txtREIN_EFFECTIVE_DATE").val(REIN_EFFECTIVE_DATE!=null?REIN_EFFECTIVE_DATE:'');
					}
					else
					{
				
						AddData();
					}
				}
				//SetTab();
				
				setTimeout('Focus()',500);
				
					
				return false;
			}
			function showhidepoltype()
			{
				
				if(document.getElementById('hidShowHide').value == "0")
					{
						document.getElementById('trPolicy_type').style.display = "none";
						document.getElementById('trCapPolicy').style.display = "inline";
					
						document.getElementById('capPolicyType').style.display='none';
						//document.getElementById('rfvPolicyType').setAttribute('enabled',false);
						EnableValidator('rfvPolicyType',false);
						//document.getElementById('rfvPolicyType').style.display='none';
						document.getElementById('spnPolicyType').style.display='none';
					}
					else if(document.getElementById('hidShowHide').value == "1")
					{
						document.getElementById('trCapPolicy').style.display = "none";
						document.getElementById('trPolicy_type').style.display = "inline";
						
						document.getElementById('capPolicyType').style.display='inline';
						//document.getElementById('rfvPolicyType').setAttribute('enabled',true);
						//document.getElementById('rfvPolicyType').style.display='inline';
						document.getElementById('spnPolicyType').style.display='inline';
						EnableValidator('rfvPolicyType',true);
					}
			}
			function SetTab()
			{
				if(document.getElementById('hidOldData').value != "")
				{				
					Url="Reinsurance/ReinsuranceContactIndex.aspx?calledfrom=reinsurer&EntityType=Reinsurer&EntityId="+document.getElementById('hidREIN_CONTACT_ID').value + "&";
					DrawTab(2,top.frames[1],'Contact',Url);
					
					Url="AttachmentIndex.aspx?calledfrom=reinsurer&EntityType=Reinsurer&EntityId="+document.getElementById('hidREIN_CONTACT_ID').value + "&";
					DrawTab(3,top.frames[1],'Attachment',Url);
					
					Url="Reinsurance/ReinsuranceBankingDetails.aspx?";
					DrawTab(4,top.frames[1],'Reinsurance Banking Details',Url);			
				}
				else
				{							
					RemoveTab(4,top.frames[1]);
					RemoveTab(3,top.frames[1]);
					RemoveTab(2,top.frames[1]);					
				}	
			}
			
			function ResetAfterActivateDeactivate()
			{
				if (document.getElementById('hidReset').value == "1")
				{				
					document.Split.reset();			
				}
			}
			
			function resetmultiselect()
			{
				for(j = document.getElementById('cmbREIN_COVERAGE').length-1; j >=0;j--)
				{
					if(document.getElementById('cmbREIN_COVERAGE').options[j].text.trim()!="" && document.getElementById('cmbREIN_COVERAGE').options[j].text!=null)
					{
						addOption(document.getElementById('cmbFROMREIN_COVERAGE'), document.getElementById('cmbREIN_COVERAGE').options[j].text, document.getElementById('cmbREIN_COVERAGE').options[j].value);
					}
					document.getElementById('cmbREIN_COVERAGE').options[j]= null;
				}
				
				for(j = document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').length-1; j >=0;j--)
				{
					if(document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').options[j].text.trim()!="" && document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').options[j].text!=null)
					{
						addOption(document.getElementById('cmbFROMREIN_1ST_SPLIT_COVERAGE'), document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').options[j].text, document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').options[j].value);
					}
					document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').options[j]= null;
				}
				for(j = document.getElementById('cmb2ndRECIPIENTS').length-1; j >=0;j--)
				{
					if(document.getElementById('cmb2ndRECIPIENTS').options[j].text.trim()!="" && document.getElementById('cmb2ndRECIPIENTS').options[j].text!=null)
					{
						addOption(document.getElementById('cmbFROMREIN_2ND_SPLIT_COVERAGE'), document.getElementById('cmb2ndRECIPIENTS').options[j].text, document.getElementById('cmb2ndRECIPIENTS').options[j].value);
					}
					document.getElementById('cmb2ndRECIPIENTS').options[j]= null;
				}
			}
																		  
			function Reset()
			{
				DisableValidators();
				//document.Split.reset();
				ChangeColor();
				document.getElementById('hidFormSaved').value = '0';
				document.getElementById('hidStateChange').value='0';	
				populateXML();
				resetmultiselect();

				//selectCoverages();
				addCoverages();	
				addCoverages1stsplit();
				addCoverages2ndsplit();	
				setTimeout('Focus()',500);
				return false;
			}																		
																				  
			function ChkDate(objSource , objArgs)
			{
				var effdate=document.Split.txtREIN_EFFECTIVE_DATE.value;
				objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",effdate,jsaAppDtFormat);
			}
			
  
			function showHide()
			{
				if(document.getElementById('RowId').value!='New')
				{
					if(document.getElementById('btnActivateDeactivate'))
					document.getElementById('btnActivateDeactivate').setAttribute('disabled',false);  
					
				}	
			}	
			function LOBChange()
			{
				document.getElementById('hidLOBChange').value='1';
				/*alert(document.getElementById('cmbREIN_LINE_OF_BUSINESS').options[document.getElementById('cmbREIN_LINE_OF_BUSINESS').selectedIndex].value);
				var LOB = document.getElementById('cmbREIN_LINE_OF_BUSINESS').options[document.getElementById('cmbREIN_LINE_OF_BUSINESS').selectedIndex].value;
				if(document.getElementById('hidShowHide').value == "0")
				{
					if(LOB==1 || LOB==6)
					{
						document.getElementById('capPolicyType').style.display='inline';
						document.getElementById('rfvPolicyType').setAttribute('enabled',true);
						document.getElementById('rfvPolicyType').style.display='inline';
						document.getElementById('spnPolicyType').style.display='inline';	
					}
					else
					{alert('n');
						document.getElementById('trPolicy_type').style.display='none';
						document.getElementById('capPolicyType').style.display='none';
						document.getElementById('rfvPolicyType').setAttribute('enabled',true);
						document.getElementById('spnPolicyType').style.display='none';
						document.getElementById('rfvPolicyType').style.display='none';	
					}
				}*/					
			}
			function StateChange()
			{
				document.getElementById('hidStateChng').value='1';
				//document.getElementById('cmbREIN_STATE').focus();
		}
		function ActivateDeactivate() {
//		    alert(document.getElementById("hidIS_ACTIVE").value);
//			if(document.getElementById("hidIS_ACTIVE").value != 'Y')
//				document.getElementById("btnActivateDeactivate").value = 'Deactivate';
//			else
//				document.getElementById("btnActivateDeactivate").value = 'Activate';
		}
		function StateIndexChange()
		{
			//document.getElementById('cmbREIN_STATE').focus();
			
		}
		
		//functions for multiselect box : start
		function setCoverages() {
		   
			document.Split.hidCOVERAGE.value = '';
			for (var i=0;i< document.getElementById('cmbREIN_COVERAGE').options.length;i++)
			{
				document.Split.hidCOVERAGE.value = document.Split.hidCOVERAGE.value + document.getElementById('cmbREIN_COVERAGE').options[i].value + ',';
			}	
			Page_ClientValidate();						
			//var returnVal = funcValidateCatagories();
			//return Page_IsValid && returnVal;
		}
		//functions 1st split coverage for multiselect box : start
		function setCoverages1st()
		{
		    var ctr = document.getElementById('cmbREIN_IST_SPLIT_COVERAGE')
		    
			document.Split.hid1stCOVERAGE.value = '';
			for (var i=0;i< ctr.options.length;i++)
			{
				document.Split.hid1stCOVERAGE.value = document.Split.hid1stCOVERAGE.value + document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').options[i].value + ',';
			}	
			
			funcValidateSplit();
			Page_ClientValidate();						
			//var returnVal = funcValidateCatagories();
			//return Page_IsValid && returnVal;
		}
		//functions for multiselect box : start
		function setCoverages2nd()
		{
			document.Split.hid2ndCOVERAGE.value = '';
			var ctr = document.getElementById('cmb2ndRECIPIENTS')
			
			for (var i=0;i< ctr.options.length;i++)
			{
				document.Split.hid2ndCOVERAGE.value = document.Split.hid2ndCOVERAGE.value + document.getElementById('cmb2ndRECIPIENTS').options[i].value + ',';
			}	
			funcValidateSplit2nd();
			Page_ClientValidate();						
			//var returnVal = funcValidateCatagories();
			//return Page_IsValid && returnVal;
		}
		function setCoverage(covvalue)
		{
			for(s = document.getElementById('cmbFROMREIN_COVERAGE').length-1; s >=0;s--)
			{
				if(document.getElementById('cmbFROMREIN_COVERAGE').options[s].value == covvalue)
				{	
					document.getElementById('cmbREIN_COVERAGE').options[document.getElementById('cmbREIN_COVERAGE').length-1].text = document.getElementById('cmbFROMREIN_COVERAGE').options[s].text;
					document.getElementById('cmbFROMREIN_COVERAGE').options[s]=null;
					break;
				}
			}
				
		}
		function setCoverage1st(covvalue)
		{
			for(s1 = document.getElementById('cmbFROMREIN_1ST_SPLIT_COVERAGE').length-1; s1 >=0;s1--)
			{
				if(document.getElementById('cmbFROMREIN_1ST_SPLIT_COVERAGE').options[s1].value == covvalue)
				{	
					document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').options[document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').length-1].text = document.getElementById('cmbFROMREIN_1ST_SPLIT_COVERAGE').options[s1].text;
					document.getElementById('cmbFROMREIN_1ST_SPLIT_COVERAGE').options[s1]=null;
					break;
				}
			}
				
		}
		function setCoverage2nd(covvalue)
		{
			for(s2 = document.getElementById('cmbFROMREIN_2ND_SPLIT_COVERAGE').length-1; s2 >=0;s2--)
			{
				if(document.getElementById('cmbFROMREIN_2ND_SPLIT_COVERAGE').options[s2].value == covvalue)
				{	
				     document.getElementById('cmb2ndRECIPIENTS').options[document.getElementById('cmb2ndRECIPIENTS').length-1].text = document.getElementById('cmbFROMREIN_2ND_SPLIT_COVERAGE').options[s2].text;
					document.getElementById('cmbFROMREIN_2ND_SPLIT_COVERAGE').options[s2]=null;
					break;
				}
			}
				
		}
		function addCoverages()
		{
			var Coverages = document.getElementById("hidCOVERAGE").value;
			var Coverage = Coverages.split(",");
			for(j = document.getElementById('cmbREIN_COVERAGE').length-1; j >=0;j--)
			{
				document.getElementById('cmbREIN_COVERAGE').options[j]= null;
			}
			if(document.getElementById('hidStateChng').value!='1')
			{
		    	for(j = 0; j < Coverage.length-1 ;j++)
				{
					document.getElementById('cmbREIN_COVERAGE').options.length=document.getElementById('cmbREIN_COVERAGE').length+1;
					document.getElementById('cmbREIN_COVERAGE').options[document.getElementById('cmbREIN_COVERAGE').length-1].value=Coverage[j];
					setCoverage(Coverage[j]);
				}
			}
		}
		function addCoverages1stsplit()
		{
			var Coverages1st = document.getElementById("hid1stCOVERAGE").value;
			var Coverage1st = Coverages1st.split(",");
			for(j1 = document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').length-1; j1 >=0;j1--)
			{
				document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').options[j1]= null;
			}
			
			if(document.getElementById('hidStateChng').value!='1')
			{
				for(j1 = 0; j1 < Coverage1st.length-1 ;j1++)
				{
					document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').options.length=document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').length+1;
					document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').options[document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').length-1].value=Coverage1st[j1];
					setCoverage1st(Coverage1st[j1]);
				}
			}
		}
		function addCoverages2ndsplit()
		{
			var Coverages2nd = document.getElementById("hid2ndCOVERAGE").value;
			var Coverage2nd = Coverages2nd.split(",");
			for(j2 = document.getElementById('cmb2ndRECIPIENTS').length-1; j2 >=0;j2--)
			{
				document.getElementById('cmb2ndRECIPIENTS').options[j2]= null;
			}
			
			if(document.getElementById('hidStateChng').value!='1')
			{
				for(j2 = 0; j2 < Coverage2nd.length-1 ;j2++)
				{
					document.getElementById('cmb2ndRECIPIENTS').options.length=document.getElementById('cmb2ndRECIPIENTS').length+1;
					document.getElementById('cmb2ndRECIPIENTS').options[document.getElementById('cmb2ndRECIPIENTS').length-1].value=Coverage2nd[j2];
					setCoverage2nd(Coverage2nd[j2]);
				}
			}
		}
		function addOption(selectElement, text, value) 
		{ 
			var foundIndex = selectElement.options.length; 
			
			for (i = 0; i < selectElement.options.length; i++) 
			{ 
				if (selectElement.options[i].innerText.toLowerCase() > text.toLowerCase()) 
				{ 
					foundIndex = i; 
					break; 
				} 
			} 

			var oOption = new Option(text,value); 
			selectElement.options.add(oOption, foundIndex); 
		} 
		function selectCoverages()
		{
			for (var i=document.getElementById('cmbFROMREIN_COVERAGE').options.length-1;i>=0;i--)
			{
					if (document.getElementById('cmbFROMREIN_COVERAGE').options[i].selected == true)
					{
						addOption(document.getElementById('cmbREIN_COVERAGE'), document.getElementById('cmbFROMREIN_COVERAGE').options[i].text, document.getElementById('cmbFROMREIN_COVERAGE').options[i].value);
						document.getElementById('cmbFROMREIN_COVERAGE').options[i] = null;
					}
					
		  	}
			
			return false;
		  	
		}
		function select1stCoverages()
		{
			if(document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').options.length > 0)
			{
				alert('1st Split Coverage can contain only one coverage, Please deselect coverage to add new coverage');
			}
			else
			{
				for (var i=document.getElementById('cmbFROMREIN_1ST_SPLIT_COVERAGE').options.length-1;i>=0;i--)
				{
						if (document.getElementById('cmbFROMREIN_1ST_SPLIT_COVERAGE').options[i].selected == true)
						{
							addOption(document.getElementById('cmbREIN_IST_SPLIT_COVERAGE'), document.getElementById('cmbFROMREIN_1ST_SPLIT_COVERAGE').options[i].text, document.getElementById('cmbFROMREIN_1ST_SPLIT_COVERAGE').options[i].value);
							document.getElementById('cmbFROMREIN_1ST_SPLIT_COVERAGE').options[i] = null;
						}
						
		  		}
				
				return false;
			} 
			funcValidateSplit();	
		}
		function select2ndCoverages()
		{
		    if (document.getElementById('cmb2ndRECIPIENTS').options.length > 0)
			{
				alert('2nd Split Coverage can contain only one coverage, Please deselect coverage to add new coverage');
			}
			//else
			{
				for (var i=document.getElementById('cmbFROMREIN_2ND_SPLIT_COVERAGE').options.length-1;i>=0;i--)
				{
						if (document.getElementById('cmbFROMREIN_2ND_SPLIT_COVERAGE').options[i].selected == true)
						{
						    addOption(document.getElementById('cmb2ndRECIPIENTS'), document.getElementById('cmbFROMREIN_2ND_SPLIT_COVERAGE').options[i].text, document.getElementById('cmbFROMREIN_2ND_SPLIT_COVERAGE').options[i].value);
							document.getElementById('cmbFROMREIN_2ND_SPLIT_COVERAGE').options[i] = null;
						}
						
		  		}
				funcValidateSplit2nd();
				return false;
		  	}
		}
		function deselectCoverages()
		{
		  for (var i=document.getElementById('cmbREIN_COVERAGE').options.length-1;i>=0;i--)
			{
			
				if (document.getElementById('cmbREIN_COVERAGE').options[i].selected == true)
				{
					addOption(document.getElementById('cmbFROMREIN_COVERAGE'), document.getElementById('cmbREIN_COVERAGE').options[i].text, document.getElementById('cmbREIN_COVERAGE').options[i].value);
					document.getElementById('cmbREIN_COVERAGE').options[i] = null;
				}
				
		  	}	
		  	return false;			
		
		}
		function deselect1stCoverages()
		{
		  for (var i=document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').options.length-1;i>=0;i--)
			{
			
				if (document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').options[i].selected == true)
				{
					addOption(document.getElementById('cmbFROMREIN_1ST_SPLIT_COVERAGE'), document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').options[i].text, document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').options[i].value);
					document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').options[i] = null;
				}
				
		  	}	
		  	funcValidateSplit();
		  	return false;			
		
		}
		function deselect2ndCoverages()
		{
		  for (var i=document.getElementById('cmb2ndRECIPIENTS').options.length-1;i>=0;i--)
			{
			
				if (document.getElementById('cmb2ndRECIPIENTS').options[i].selected == true)
				{
					addOption(document.getElementById('cmbFROMREIN_2ND_SPLIT_COVERAGE'), document.getElementById('cmb2ndRECIPIENTS').options[i].text, document.getElementById('cmb2ndRECIPIENTS').options[i].value);
					document.getElementById('cmb2ndRECIPIENTS').options[i] = null;
				}
				
		  	}	
		  	funcValidateSplit2nd();
		  	return false;			
		
		}
		
		function funcValidateSplit()
		{
			
			if (document.getElementById('cmbREIN_IST_SPLIT_COVERAGE').options.length==0)
			{
			document.getElementById('rfvREIN_1ST_SPLIT_COVERAGE').setAttribute("enabled",true);
			//document.getElementById('rfvREIN_1ST_SPLIT_COVERAGE').setAttribute("isValid",false);
			document.getElementById("rfvREIN_1ST_SPLIT_COVERAGE").style.display="inline";
			}
			else
			{
			//EnableValidator('rfvRSKAssignLossCodes',true);
			document.getElementById('rfvREIN_1ST_SPLIT_COVERAGE').setAttribute("enabled",false);
			//document.getElementById('rfvREIN_1ST_SPLIT_COVERAGE').setAttribute("isValid",true);
			document.getElementById("rfvREIN_1ST_SPLIT_COVERAGE").style.display="none";
			
			//return true;
			}
			return;
		}	
		function funcValidateSplit2nd()
		{
			
			if (document.getElementById('cmb2ndRECIPIENTS').options.length==0)
			{
			document.getElementById('rfvREIN_2ND_SPLIT_COVERAGE').setAttribute("enabled",true);
			//document.getElementById('rfvREIN_2ND_SPLIT_COVERAGE').setAttribute("isValid",true);
			document.getElementById("rfvREIN_2ND_SPLIT_COVERAGE").style.display="inline";
			}
			else
			{
			//EnableValidator('rfvRSKAssignLossCodes',true);
			document.getElementById('rfvREIN_2ND_SPLIT_COVERAGE').setAttribute("enabled",false);
			//document.getElementById('rfvREIN_2ND_SPLIT_COVERAGE').setAttribute("isValid",false);
			document.getElementById("rfvREIN_2ND_SPLIT_COVERAGE").style.display="none";
			
			//return true;
			}
			return;
		}
		
		function Focus()
		{
			document.getElementById("txtREIN_EFFECTIVE_DATE").focus();
		}
		
		//Functions for policy type multiselect box : Start
		function setPolicies()
		{
			document.Split.hidPOLICY.value = '';
			for (var i=0;i< document.getElementById('cmbPOLICY_TYPE').options.length;i++)
			{
				document.Split.hidPOLICY.value = document.Split.hidPOLICY.value + document.getElementById('cmbPOLICY_TYPE').options[i].value + ',';
			}	
			//addCatagories();
			funcPolicyType();	
			Page_ClientValidate();						
			//var returnVal = funcValidatePolicies();
			//return Page_IsValid && returnVal;
		}
		
		function setPolicy(polvalue)
		{
			for(p = document.getElementById('cmbFROMPOLICY_TYPE').length-1; p >=0;p--)
			{
				if(document.getElementById('cmbFROMPOLICY_TYPE').options[p].value == polvalue)
				{	
					document.getElementById('cmbPOLICY_TYPE').options[document.getElementById('cmbPOLICY_TYPE').length-1].text = document.getElementById('cmbFROMPOLICY_TYPE').options[p].text;
					document.getElementById('cmbFROMPOLICY_TYPE').options[p]=null;
					break;
				}
			}
				
		}
		function addPolicies()
		{
			var Policies = document.getElementById("hidPOLICY").value;
			var Policy = Policies.split(",");
			for(j3 = document.getElementById('cmbPOLICY_TYPE').length-1; j3 >=0;j3--)
			{
				document.getElementById('cmbPOLICY_TYPE').options[j3].value= null;
			}
			if(document.getElementById('hidStateChng').value!='1')
			{
				for(j3 = 0; j3 < Policy.length-1 ;j3++)
				{
					document.getElementById('cmbPOLICY_TYPE').options.length=document.getElementById('cmbPOLICY_TYPE').length+1;
					document.getElementById('cmbPOLICY_TYPE').options[document.getElementById('cmbPOLICY_TYPE').length-1].value=Policy[j3];
					setPolicy(Policy[j3]);
				}
			}
			
		}
		
		
		function selectPolicies()
		{
			for (var i3=document.getElementById('cmbFROMPOLICY_TYPE').options.length-1;i3>=0;i3--)
			{
					if (document.getElementById('cmbFROMPOLICY_TYPE').options[i3].selected == true)
					{
						addOption(document.getElementById('cmbPOLICY_TYPE'), document.getElementById('cmbFROMPOLICY_TYPE').options[i3].text, document.getElementById('cmbFROMPOLICY_TYPE').options[i3].value);
						document.getElementById('cmbFROMPOLICY_TYPE').options[i3] = null;
					}
					
		  	}
			//funcPolicyType();
			return false;
		  	
		}
		
		function deselectPolicies()
		{
		  for (var i=document.getElementById('cmbPOLICY_TYPE').options.length-1;i>=0;i--)
			{
			
				if (document.getElementById('cmbPOLICY_TYPE').options[i].selected == true)
				{
					addOption(document.getElementById('cmbFROMPOLICY_TYPE'), document.getElementById('cmbPOLICY_TYPE').options[i].text, document.getElementById('cmbPOLICY_TYPE').options[i].value);
					document.getElementById('cmbPOLICY_TYPE').options[i] = null;
				}
				
		  	}
		  funcPolicyType();	
		  	return false;			
		
		}
		
		function funcValidatePolicies()
		{
			/*if(document.getElementById('cmbPOLICY_TYPE').options.length == 0)
			{
				document.getElementById('cmbPOLICY_TYPE').className = "MandatoryControl";
				//document.getElementById("cmbPOLICY_TYPE").style.display="inline";
				document.getElementById("csvPOLICY_TYPE").style.display="inline";
				document.getElementById("csvPOLICY_TYPE").innerText = "Please select Category";
				return false;
			}
			else
			{
				document.getElementById('cmbPOLICY_TYPE').className = "none";
				return true;
			}*/
		}
		function resetPolicies()
		{
			for(j = document.getElementById('cmbPOLICY_TYPE').length-1; j >=0;j--)
			{
				if(document.getElementById('cmbPOLICY_TYPE').options[j].text.trim()!="" && document.getElementById('cmbPOLICY_TYPE').options[j].text!=null)
				{
					addOption(document.getElementById('cmbFROMPOLICY_TYPE'), document.getElementById('cmbPOLICY_TYPE').options[j].text, document.getElementById('cmbPOLICY_TYPE').options[j].value);
				}
				document.getElementById('cmbPOLICY_TYPE').options[j]= null;
			}
		}
		function funcPolicyType()
		{
		var LOB = document.getElementById('cmbREIN_LINE_OF_BUSINESS').options[document.getElementById('cmbREIN_LINE_OF_BUSINESS').selectedIndex].value;
		
		if(LOB==1 || LOB==6)
		{
	
			if (document.getElementById('cmbPOLICY_TYPE').options.length==0)
			{
			//document.getElementById('rfvPolicyType').setAttribute("enabled",true);
			//document.getElementById("rfvPolicyType").style.display="inline";
			EnableValidator('rfvPolicyType',true);
			}
			else
			{
			//document.getElementById('rfvPolicyType').setAttribute("enabled",false);
			//document.getElementById("rfvPolicyType").style.display="none";
			EnableValidator('rfvPolicyType',false);
			}
			
		}
			return;
		}	
		//Functions for policy type multiselect box : End	
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();">
	<div style="OVERFLOW-X: scroll; OVERFLOW-Y: hidden; WIDTH: 100%">
	<FORM id="Split" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
								</tr>
								<tr>
									<!--START April 09 2007 Harmanjeet-->
									<TD class="midcolora" width="18%"><asp:label id="capREIN_EFFECTIVE_DATE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtREIN_EFFECTIVE_DATE" runat="server" MaxLength="10" size="12"></asp:textbox>
									<asp:hyperlink id="hlkEFFECTIVE_DATE" runat="server" CssClass="HotSpot">
											<asp:image id="imgEFFECTIVE_DATE" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif"></asp:image>
										</asp:hyperlink><br>
										<br>
									
									<asp:regularexpressionvalidator id="revREIN_EFFECTIVE_DATE" runat="server" Display="Dynamic" ControlToValidate="txtREIN_EFFECTIVE_DATE"></asp:regularexpressionvalidator></TD>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capREIN_LINE_OF_BUSINESS" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREIN_LINE_OF_BUSINESS" onchange="LOBChange();" onfocus="SelectComboIndex('cmbREIN_LINE_OF_BUSINESS')"
											 runat="server"></asp:dropdownlist><br>
											 <asp:requiredfieldvalidator id="rfvREIN_LINE_OF_BUSINESS" Runat="server" Display="Dynamic" ControlToValidate="cmbREIN_LINE_OF_BUSINESS"
											ErrorMessage=""></asp:requiredfieldvalidator></TD><%--Please Select Line of Business.--%>
								</tr>
								<tr>
									<TD class="midcolora" width="18%" style="HEIGHT: 19px"><asp:label id="capREIN_STATE" runat="server"></asp:label><span id="spnREIN_STATE" runat="server" class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%" style="HEIGHT: 19px" colSpan="3"><asp:dropdownlist id="cmbREIN_STATE" onchange="StateChange();"  onfocus="SelectComboIndex('cmbREIN_STATE')"  runat="server"
											AutoPostBack="True"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvREIN_STATE" Runat="server" Display="Dynamic" ControlToValidate="cmbREIN_STATE"
											ErrorMessage=""></asp:requiredfieldvalidator><%--Please Select State.--%>
									</TD>
									
									<%--<td class="midcolora" colSpan="2"></td>--%>
								</tr>
								<%-- Policy Type Multiselect box:Start --%>
								<tr id = "trPolicy_type" runat="server">
										<TD class="midcolora"><asp:label id="capPOLICY_TYPE" runat="server">Policy Type</asp:label></TD>
										<td class="midcolora" align="center" width="18%">
											<%--<asp:label id="capCATEGORYDETAILS" Runat="server">Catagory Details</asp:label><br>--%>
											<asp:listbox id="cmbFROMPOLICY_TYPE" Runat="server" Height="79px" AutoPostBack="False" SelectionMode="Multiple" >
												<ASP:LISTITEM Value="0"><--------Select--------></ASP:LISTITEM>
											</asp:listbox><br>
										</td>
										<td class="midcolorc" align="center" width="18%"><br>
											<asp:button class="clsButton" id="btnPolSELECT" Runat="server" Text=">>" CausesValidation="True" ></asp:button><br>
											<br>
											<br>
											<br>
											<asp:button class="clsButton" id="btnPolDESELECT" Runat="server" Text="<<" CausesValidation="False" ></asp:button></td>
										<td class="midcolora" align="center">
											<asp:label id="capPolicyType" runat="server">Policy Type</asp:label><span class="mandatory" ID="spnPolicyType">*</span><br>
											<asp:label id="capPolRECIPIENTS" Runat="server" style="DISPLAY: none">Recipients</asp:label>
											<asp:listbox id="cmbPOLICY_TYPE" onblur="" Runat="server" Height="79px"  AutoPostBack="False"
												SelectionMode="Multiple" ></asp:listbox><br>
										<asp:requiredfieldvalidator id="rfvPolicyType" runat="server" ErrorMessage=""
										ControlToValidate="cmbPOLICY_TYPE"></asp:requiredfieldvalidator><%--Please select Policy Type.--%>
										<asp:customvalidator id="csvPOLICY" Display="Dynamic" ControlToValidate="cmbPOLICY_TYPE" Runat="server"
												ClientValidationFunction="funcValidatePolicies" ErrorMessage =""></asp:customvalidator><span id="spnPOLICY" style="DISPLAY: none; COLOR: red">Please 
												select Policy Type.</span><%--Please select Policy Type.--%>
										</td>
								</tr>
								<tr id ="trCapPolicy" runat="server">
									<TD class="midcolora" colSpan="2"><asp:label id="capPol_type" Runat="server" >Policy Type</asp:label></td>
									<TD class="midcolora" colSpan="2"><asp:label id="capPol_tpye" Runat="server" >All</asp:label></td>

								</tr>
								<%-- Policy Type Multiselect box:End --%>
								<!-- Multiselect box:Start-->
								<tr>
										<TD class="midcolora"><asp:label id="capREIN_COVERAGE" runat="server">Coverages</asp:label></TD>
										<td class="midcolora" align="center" width="18%">
											<%--<asp:label id="capCATEGORYDETAILS" Runat="server">Catagory Details</asp:label><br>--%>
											<asp:listbox id="cmbFROMREIN_COVERAGE" Runat="server" Height="79px" AutoPostBack="False" SelectionMode="Multiple" >
												<ASP:LISTITEM Value="0"><--------Select--------></ASP:LISTITEM>
											</asp:listbox><br>
										</td>
										<td class="midcolorc" align="center" width="18%"><br>
											<asp:button class="clsButton" id="btnSELECT" Runat="server" Text=">>" CausesValidation="True" ></asp:button><br>
											<br>
											<br>
											<br>
											<asp:button class="clsButton" id="btnDESELECT" Runat="server" Text="<<" CausesValidation="False" ></asp:button></td>
										<td class="midcolora" align="center">
											<asp:label id="capRECIPIENTS" Runat="server" style="DISPLAY: none">Recipients</asp:label>
											<asp:listbox id="cmbREIN_COVERAGE" onblur="" Runat="server" Height="79px"  AutoPostBack="False"
												SelectionMode="Multiple" ></asp:listbox><br>
										</td>
								</tr>
								<!-- Multiselect box:End-->
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capREIN_IST_SPLIT" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%" colspan=3><asp:textbox style="TEXT-ALIGN:right" id="txtREIN_IST_SPLIT" runat="server" class="INPUTCURRENCY" MaxLength="3" size="3"></asp:textbox><br>
										<asp:requiredfieldvalidator id="rfvREIN_IST_SPLIT" Runat="server" Display="Dynamic" ControlToValidate="txtREIN_IST_SPLIT"
											ErrorMessage=""></asp:requiredfieldvalidator><%--Please Enter 1st Split%.--%>
										<asp:RangeValidator id="rngREIN_IST_SPLIT" runat="server" ErrorMessage=""
											MinimumValue="0" MaximumValue="100" ControlToValidate="txtREIN_IST_SPLIT" Type="Integer"></asp:RangeValidator></TD><%--Please enter between 0-100--%>
									<!--<td class="midcolora" colSpan="2"></td>-->
									<%--TD class="midcolora" width="18%"><asp:label id="capREIN_IST_SPLIT_COVERAGE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREIN_IST_SPLIT_COVERAGE" onfocus="SelectComboIndex('cmbREIN_IST_SPLIT_COVERAGE')"
											 runat="server">
											<asp:ListItem Value="Any Value" Selected="True">No Value</asp:ListItem>
										</asp:dropdownlist><br><asp:requiredfieldvalidator id="rfvREIN_IST_SPLIT_COVERAGE" Runat="server" Display="Dynamic" ControlToValidate="cmbREIN_IST_SPLIT_COVERAGE"
											ErrorMessage="Please Select 1st Split Coverage."></asp:requiredfieldvalidator></TD--%>
								</tr>
								<!-- Multiselect box For 1st split coverage:Start-->
								<tr>
										<TD class="midcolora"><asp:label id="capREIN_IST_SPLIT_COVERAGE" runat="server">1st Split Coverage</asp:label></TD>
										<td class="midcolora" align="center" width="18%">
											<%--<asp:label id="capCATEGORYDETAILS" Runat="server">Catagory Details</asp:label><br>--%>
											<asp:listbox id="cmbFROMREIN_1ST_SPLIT_COVERAGE" Runat="server" Height="79px" AutoPostBack="False" >
												<ASP:LISTITEM Value="0"><--------Select--------></ASP:LISTITEM>
											</asp:listbox><br>
										</td>
										<td class="midcolorc" align="center" width="18%"><br>
											<asp:button class="clsButton" id="btn1stSELECT" Runat="server" Text=">>" CausesValidation="True" ></asp:button><br>
											<br>
											<br>
											<br>
											<asp:button class="clsButton" id="btn1stDESELECT" Runat="server" Text="<<" CausesValidation="False" ></asp:button></td>
										<td class="midcolora" align="center">
											<asp:label id="capREIN_1ST_SPLIT_COVERAGE" runat="server">1st Split Coverage</asp:label><span class="mandatory">*</span><br>
											<asp:label id="cap1stRECIPIENTS" Runat="server" style="DISPLAY: none">Recipients</asp:label>
											<asp:listbox id="cmbREIN_IST_SPLIT_COVERAGE" onblur="" Runat="server" Height="79px"  AutoPostBack="False"
												SelectionMode="Multiple" ></asp:listbox>
												<br>
										<asp:requiredfieldvalidator id="rfvREIN_1ST_SPLIT_COVERAGE" runat="server" ErrorMessage=""
										ControlToValidate="cmbREIN_IST_SPLIT_COVERAGE"></asp:requiredfieldvalidator><%--Please select 1st Split Coverage.--%>
										</td>
								</tr>
								<!-- Multiselect box:End-->
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capREIN_2ND_SPLIT" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%" colspan=3><asp:textbox style="TEXT-ALIGN:right" id="txtREIN_2ND_SPLIT" class="INPUTCURRENCY" runat="server" MaxLength="3" size="3"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvREIN_2ND_SPLIT" Runat="server" Display="Dynamic" ControlToValidate="txtREIN_2ND_SPLIT"
											ErrorMessage=""></asp:requiredfieldvalidator><asp:rangevalidator id="rngREIN_2ND_SPLIT" runat="server" ErrorMessage="Please enter between 0-100"
											MinimumValue="0" MaximumValue="100" ControlToValidate="txtREIN_2ND_SPLIT" Type="Integer"></asp:rangevalidator></TD><%--Please Enter 2nd Split%.--%>
									<%--TD class="midcolora" width="18%"><asp:label id="capREIN_2ND_SPLIT_COVERAGE" runat="server"></asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmb2ndRECIPIENTS" onfocus="SelectComboIndex('cmb2ndRECIPIENTS')"
											 runat="server">
											<asp:ListItem Value="Any Value" Selected="True">No Value</asp:ListItem>
										</asp:dropdownlist><br><asp:requiredfieldvalidator id="rfvREIN_2ND_SPLIT_COVERAGE" Runat="server" Display="Dynamic" ControlToValidate="cmb2ndRECIPIENTS"
											ErrorMessage="Please Select 2nd Split Coverage."></asp:requiredfieldvalidator></TD--%>
								</tr>
								<!-- Multiselect box For 2ND split coverage:Start-->
								<tr>
										<TD class="midcolora"><asp:label id="capREIN_2ND_SPLIT_COVERAGE" runat="server">2nd Split Coverage</asp:label></TD>
										<td class="midcolora" align="center" width="18%" >
											<%--<asp:label id="capCATEGORYDETAILS" Runat="server">Catagory Details</asp:label><br>--%>
											<asp:listbox id="cmbFROMREIN_2ND_SPLIT_COVERAGE" Runat="server" Height="79px" AutoPostBack="False">
												<ASP:LISTITEM Value="0"><--------Select--------></ASP:LISTITEM>
											</asp:listbox><br>
										</td>
										<td class="midcolorc" align="center" width="18%"><br>
											<asp:button class="clsButton" id="btn2ndSELECT" Runat="server" Text=">>" CausesValidation="True" ></asp:button><br>
											<br>
											<br>
											<br>
											<asp:button class="clsButton" id="btn2ndDESELECT" Runat="server" Text="<<" CausesValidation="False" ></asp:button></td>
										<td class="midcolora" align="center">
											<asp:label id="cap2ndRECIPIENTS" Runat="server" >2nd Split Coverage</asp:label><span class="mandatory">*</span><br>
											<asp:listbox id="cmb2ndRECIPIENTS" onblur="" Runat="server" Height="79px"  AutoPostBack="False"
												SelectionMode="Multiple" ></asp:listbox><br>
										<asp:requiredfieldvalidator id="rfvREIN_2ND_SPLIT_COVERAGE" runat="server" ErrorMessage=""
										ControlToValidate="cmb2ndRECIPIENTS"></asp:requiredfieldvalidator><%--Please select 2nd Split Coverage.--%>
										</td>
								</tr>
								<!-- Multiselect box:End-->
								<!--END Harmanjeet-->
								<tr>
									<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"  causesValidation="false"></cmsb:cmsbutton>&nbsp;
										<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidREIN_SPLIT_DEDUCTION_ID" type="hidden" value="0" name="hidREIN_SPLIT_DEDUCTION_ID"
				runat="server"> <INPUT id="hidReset" type="hidden" value="0" name="hidReset" runat="server">
			<INPUT id="hidStateChange" type="hidden" value="0" name="hidStateChange" runat="server">
			<INPUT id="hidState" type="hidden" value="0" name="hidState" runat="server">
			<INPUT id="hidLOB" type="hidden" value="0" name="hidLOB" runat="server">
			<INPUT id="hidLOBChange" type="hidden" value="0" name="hidLOBChange" runat="server">
			<INPUT id="hidStateChng" type="hidden" value="0" name="hidStateChng" runat="server">
			<INPUT id="hidCOVERAGE" type="hidden" value="0" name="hidCOVERAGE" runat="server">
			<INPUT id="hid1stCOVERAGE" type="hidden" value="0" name="hid1stCOVERAGE" runat="server">
			<INPUT id="hid2ndCOVERAGE" type="hidden" value="0" name="hid2ndCOVERAGE" runat="server">
			<INPUT id="hidPOLICY" type="hidden" value="0" name="hidPOLICY" runat="server">
			<INPUT id="hidShowHide" type="hidden" value="0" name="hidShowHide" runat="server">
			
			
			       
		</FORM>
		</DIV>
		<script>
		    if (document.getElementById('hidFormSaved').value == "1") {
		        RefreshWebGrid(1, document.getElementById('hidREIN_SPLIT_DEDUCTION_ID').value, true);
		    }

		            addCoverages();
		            addCoverages1stsplit();
		            addCoverages2ndsplit();
		            ActivateDeactivate();
		            funcValidateSplit();
		            funcValidateSplit2nd();
		            addPolicies();
		            funcValidatePolicies();
		            funcPolicyType();
		   
		  
			
			
		</script>
	</BODY>
</HTML>

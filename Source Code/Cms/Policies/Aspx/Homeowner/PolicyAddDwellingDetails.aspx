<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" Codebehind="PolicyAddDwellingDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.aspx.HomeOwners.PolicyAddDwellingDetails" validateRequest=false %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddDwellingDetails</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../../cmsweb/scripts/common.js"></script>
		<script src="../../../cmsweb/scripts/form.js"></script>
		<script language="javascript">
		
		function ChkTextAreaLength(source, arguments)
		{
			var txtArea = arguments.Value;
			if(txtArea.length > 250 ) 
			{
				arguments.IsValid = false;
				return;   
			}
		}
		
		function YearBuilt_OnBlur()
		{	
			//IF MKT VAL IS PROVIDED. NO NEED OF VALIDATIONS
			var mkt_val= document.getElementById('txtMARKET_VALUE').value;
			if (mkt_val != '')
			{
				return;
			}
				
			var policyType = document.getElementById('hidPolicyType').value;
			var msgToShow  = "Market Value is Mandatory for this type of policy.\nPage will be saved without this information.\nYou are Requested to provide this information at later stage."
			switch(policyType)
			{
				case '11403': //HO2 Repair Mis
				case '11193': //HO2 Repair Ind
				case '11404': //HO3 Repair Mis
				case '11194': //HO3 Repair Ind					
					alert(msgToShow)					
					break;
									
				case '11405': //HO4 Mis
				case '11195': //HO4 Ind				
				case '11406': //HO6 Mis
				case '11196': //HO6 Ind
					//document.getElementById("lblMessage").innerText = ""					
					break;
					
				default :
					var year = document.getElementById('txtYEAR_BUILT').value;
					if (parseInt (year) < 1940 )
					{
						alert("Year built is prior to 1940.\n" + msgToShow);					
					}
					break;
			}						
		}
		
		//For HO-6, If number entered is greater than 6 
		//Message to appear " Must be owner occupied for a least 6 months
		function handleMonthsRentedCase()
		{
			var PolicyType		= document.getElementById('hidPolicyType').value;
			var MonthsRented	= document.getElementById('txtMONTHS_RENTED').value;
			
			//'11406': HO6 MIS AND '11196': HO6 IND
			if ((PolicyType == "11406" || PolicyType == "11196" ) && parseInt(MonthsRented) >6)		
				return false;
			else
				return true;
				
		}
		
		function CheckMonthsRented(objSource , objArgs)
		{
			objArgs.IsValid = handleMonthsRentedCase();
		}
		
		function CheckMarketCost(objSource , objArgs)
		{
			//var currentDate="<%=DateTime.Now.Date.ToString().Substring(0,DateTime.Now.Date.ToString().IndexOf(' '))%>";
			//var currentDate=new Date("<%=DateTime.Now.Date.ToString().Substring(0,DateTime.Now.Date.ToString().IndexOf(' '))%>");
			//alert(currentDate);
			var MarketValue=ReplaceAll( document.getElementById('txtMARKET_VALUE').value,',','');
			if (MarketValue == '' ) 
			{
				objArgs.IsValid = true;
				return;
			}
			var ReplacementCost=ReplaceAll( document.getElementById('txtREPLACEMENT_COST').value,',','');
			
			//Itrack # 6146 - 27 July 2009 Manoj Rathore
			var policyType = document.getElementById('hidPolicyType').value;
			if(policyType == "11409" || policyType == "11410" || policyType =="11149" || policyType== "11401")
			{
				var txtValue	 =  getFixedSimple(ReplacementCost ,2,1);
					MarketValue  =  getFixedSimple(MarketValue ,2,1);				
				if ( parseFloat(MarketValue) ==  parseFloat(txtValue))
				{
					objArgs.IsValid = true;							
				}
				else
				{
					objArgs.IsValid   = false;
				}				
			}
			else if(policyType == "11402" || policyType == "11148" || policyType == "11192" || policyType =="11400")
			{	
					//ITrack # 6146 - 17 Aug. 2009 Manoj Rathore
					//In HO-2 Replacement , IN HO-3 Replacemnt 50000 
					//if repalcement cost is equel to 35000 than Market Value should be 100% of the replacement cost,
					//if replacement cost greater than 35000 than Market Value cannot be less than 80% of replacement cost..			
					
					var txtValue	 =  getFixedSimple(ReplacementCost ,2,1);					
						
					var varValue	 = "";
					var varValue1	 = "";
					
					if (policyType== "11148" || policyType== "11400" || policyType== "11192" )
					{
						varValue= "50000";
						varValue1="$50,000";
					}
					else if(policyType== "11402" )
					{
						varValue= "35000";
						varValue1="$35,000";
					}				
					
					if(parseFloat(txtValue) > parseFloat(varValue))
					{	
						txtValue	 =  getFixedSimple(ReplacementCost *.80 - parseInt(1) ,2,1);	
						if (parseFloat(MarketValue) <= parseFloat(ReplacementCost) && parseFloat(MarketValue) > parseFloat(txtValue) && parseFloat(MarketValue) >= parseFloat(varValue))
						{
							objArgs.IsValid = true;
						}
						else if(parseFloat(ReplacementCost *.80) < parseFloat(varValue))
						{
							objArgs.IsValid   =false;
							if(parseFloat(MarketValue) > parseFloat(varValue))
							{
							document.getElementById('csvMARKET_VALUE').innerText ="Market Value can not be greater than Replacement Cost.";
							}
							else
							{
							document.getElementById('csvMARKET_VALUE').innerText ="Market Value can not be less than " + varValue1 +".";
							}
						}						
						else
						{
						objArgs.IsValid   =false;
						document.getElementById('csvMARKET_VALUE').innerText ="Market Value must be between 80% and 100% of Replacement Cost.";
						}
					
					}
					else
					{
						var txtValue1 = getFixedSimple(ReplacementCost ,2,1)
						
						if ( parseFloat(MarketValue) ==  parseFloat(txtValue1) || parseFloat(MarketValue) == parseFloat(varValue) || (parseFloat(txtValue1) > parseFloat(varValue) && parseFloat(MarketValue) > parseFloat(varValue)) )
						{
							objArgs.IsValid = true;							
						}
						else
						{
							objArgs.IsValid   = false;
							if(parseFloat(MarketValue) > parseFloat(varValue))
							{
							document.getElementById('csvMARKET_VALUE').innerText ="Market Value can not be greater than " + varValue1 +".";
							}
							else
							{
							document.getElementById('csvMARKET_VALUE').innerText ="Market Value can not be less than " + varValue1 +".";
							}							
						}						
					}			
			}
			else
			{	
				//Reduced 1 to ReplacementCost for Gen Issue 3158
				var txtValue =  getFixedSimple(ReplacementCost *.80 - parseInt(1),2,1);
				
				//if ( parseFloat(MarketValue) >  parseFloat(txtValue))
				//ITrack # 6146 - 27 July 2009 Manoj Rathore
				if ( parseFloat(MarketValue) > parseFloat(txtValue) &&  parseFloat(MarketValue) <= parseFloat(ReplacementCost))
				{
						objArgs.IsValid = true;	
				}
				else
				{
					objArgs.IsValid   =false;
				}
			}	
			
			
		}		
		function SetMinMarketValue()
		{
		  
	      if( !(document.getElementById('hidPolicyType').value == "11148" || document.getElementById('hidPolicyType').value == "11192" || document.getElementById('hidPolicyType').value == "11193" ||  document.getElementById('hidPolicyType').value == "11194")) 
	      {
	        return false;
	      }
	      
	      if(document.getElementById('cmbLOCATION_ID').options.selectedIndex == -1)
	      {
	        return false;
	      }
	      
	      var strValue = new String(document.getElementById('cmbLOCATION_ID').options[document.getElementById('cmbLOCATION_ID').selectedIndex].value);
                   
          var strlocation=strValue.split('^');
		   var EffDate = document.getElementById('hidEffDate').value;
          if(EffDate !='' && EffDate != null )
          {	
				if(strlocation[1] == "11812")
				{
						if(document.getElementById('cmpREPLACEMENT_COST') != null)
						{
						document.getElementById('cmpREPLACEMENT_COST').setAttribute("valuetocompare","50000"); 
						document.getElementById('cmpREPLACEMENT_COST').innerHTML="Replacement cost  must be numeric, non-zero and non-negative.  It must be greater than or equal to 50,000";
						}  
						if(document.getElementById('cmpMARKET_VALUE') != null)
						{
						document.getElementById('cmpMARKET_VALUE').setAttribute("valuetocompare","50000"); 
						document.getElementById('cmpMARKET_VALUE').innerHTML="Market/Repair cost  must be numeric, non-zero and non-negative.  It must be greater than or equal to 50,000";
						}  
		                         
				}
				else 
				{
						if(document.getElementById('cmpREPLACEMENT_COST') != null)
						{
						document.getElementById('cmpREPLACEMENT_COST').setAttribute("valuetocompare","40000"); 
						document.getElementById('cmpREPLACEMENT_COST').innerHTML="Replacement cost  must be numeric, non-zero and non-negative. It must be greater than or equal to 40,000";
						}
						if(document.getElementById('cmpMARKET_VALUE') != null)
						{
						document.getElementById('cmpMARKET_VALUE').setAttribute("valuetocompare","40000"); 
						document.getElementById('cmpMARKET_VALUE').innerHTML="Market/Repair  must be numeric, non-zero and non-negative. It must be greater than or equal to 40,000";
						Page_ClientValidate();
						}  
				}
		 }
		else
		{
			if(document.getElementById('cmpREPLACEMENT_COST') != null)
				{
				document.getElementById('cmpREPLACEMENT_COST').setAttribute("valuetocompare","1"); 
				document.getElementById('cmpREPLACEMENT_COST').innerHTML="Replacement cost  must be numeric, non-zero and non-negative.";						
				}
			if(document.getElementById('cmpMARKET_VALUE') != null)
				{
				document.getElementById('cmpMARKET_VALUE').setAttribute("valuetocompare","1"); 
				document.getElementById('cmpMARKET_VALUE').innerHTML="Market/Repair  must be numeric, non-zero and non-negative.";
				}		
		}				
         //DisableValidators();
		}	
		function getFixedSimple(val, places, isRounded)
		{
			var factor;
			var i;
			factor = 1;
			for (i=0; i<places; i++)
			{	factor *= 10; }
			val = Math.floor((val * factor) + (isRounded ? 0.50000000001 : 0.00000000001));
			val = val / factor;
			return val;
		}
		
		
		//Defaults repair cost on the Market value
		function DefaultRepairCost()
		{
			if ( Page_IsValid == false ) return;
			
			document.getElementById('txtMARKET_VALUE').value = formatCurrency(document.getElementById('txtMARKET_VALUE').value)			
			
			var policyType = document.getElementById('hidPolicyType').value;
			
			if (policyType == "11403" ||policyType == "11404" || policyType == "11193" || policyType == "11194" || policyType == "11290" || policyType == "11292" || policyType == "11480" || policyType == "11482")
			{
				if (document.getElementById('txtREPLACEMENT_COST') && document.getElementById('txtMARKET_VALUE').value)
					document.getElementById('txtREPLACEMENT_COST').value = formatCurrency(document.getElementById('txtMARKET_VALUE').value);			 
			}
						
		}
		
		
		function EnableDisableDesc(cmbCombo, txtDesc,lblNA)
		{	

			if (cmbCombo.selectedIndex > -1)
			{	
				
				//Checking value only if item is selected
				if (cmbCombo.options[cmbCombo.selectedIndex].text == "Yes")
				{
					//Disabling the description field, if No is selected
					txtDesc.style.display = "inline";
					
					lblNA.style.display = "none";
				}
				else
				{
					//Enabling the description field, if yes is selected
					txtDesc.style.display = "none";
				
					lblNA.style.display = "inline";
					lblNA.innerHTML="NA";
				}
			}
			else
			{
				//Disabling the description field, if No is selected
				txtDesc.style.display = "none";
				lblNA.style.display = "inline";
				lblNA.innerHTML="NA";
			}
		}

			
			function Refresh(formSaved,rowID)
			{
				//alert('refresh');
				RefreshWebGrid(formSaved,rowID);
			}
			
			function OpenPopupWindow(Url)
			{
				var myUrl = Url;
				window.open(myUrl,'','toolbar=no, directories=no, location=no, status=yes, menubar=no, resizable=yes, scrollbars=no,  width=500, height=200'); 
				return false;		
			}
			
			function Location()
			{
			 top.frames[1].location ='/cms/Policies/aspx/PolicyLocationIndex.aspx?CalledFrom=<%=strCalledFrom%>';
			}
			
			function PopulateFormData(xml,objForm)
			{		
					if(xml!="")
					{
					
						var objXmlHandler = new XMLHandler();
						var tree = (objXmlHandler.quickParseXML(xml).getElementsByTagName('Table')[0]);
						
						var i=0;
						for(i=0;i<tree.childNodes.length;i++)
						{
							var nodeName = tree.childNodes[i].nodeName;
							var nodeValue ;
							
							//tree.childNodes[i].firstChild.text;
							
							if ( tree.childNodes[i].firstChild == 'undefined' || tree.childNodes[i].firstChild == null )
							{
								nodeValue = '';
							}
							else
							{
								nodeValue = tree.childNodes[i].firstChild.text;
							}
						
							if ( nodeName == 'LOC_SUBLOC')
							{
								if(document.getElementById("cmbLOCATION_ID"))
								{
									SelectComboOption("cmbLOCATION_ID",nodeValue)
								}
							}
								
							//**** Setting Textfield value *********
							if(document.getElementById("txt"+nodeName))
							{
								document.getElementById("txt"+nodeName).value =nodeValue;
								continue;
							}
							
							//**** Setting List box value *********
							if(document.getElementById("lst"+nodeName))
							{
								SelectComboOption("lst"+nodeName,nodeValue)
								continue;
							}
							//**** Setting hidden field value *********
							if(document.getElementById("hid"+nodeName))
							{
								document.getElementById("hid"+nodeName).value = nodeValue;
								//alert(document.getElementById("hid"+nodeName).value);
								continue;
							}
							//**** Setting drop down list value *********
							if(document.getElementById("cmb"+nodeName))
							{
								
								SelectComboOption("cmb"+nodeName,nodeValue)
								continue;
							}
							
							//**** Setting label value *********
							if(document.getElementById("lbl"+nodeName))
							{
								document.getElementById("lbl"+nodeName).innerText = nodeValue;
								continue;
							}
							//**** Setting checkbox value *********
							if(document.getElementById("chk"+nodeName))
							{
								//alert(document.getElementById("chk"+nodeName).checked);
								if(nodeValue=="Y" || nodeValue=="1" || nodeValue==true)
									document.getElementById("chk"+nodeName).checked = true;
								else 
									document.getElementById("chk"+nodeName).checked = false;
									continue;
							}
							//**** Setting radio button value *********
							if(document.getElementById("rdb"+nodeName+nodeValue))
							{
								var nodeValue = nodeValue;
								document.getElementById("rdb"+nodeName+nodeValue).checked = true;
								continue;
							}
							
							
							
						}
						
						//**** Setting Activate/Deacttivate caption *********
						
						SetActivateDeactivateButton(xml);
					
					}
					//EnableDisableDesc(document.getElementById('cmbIS_DWELLING_OWNED_BY_OTHER'),document.getElementById('txtDwellingOwned'),document.getElementById('lblDwellingOwned'));
					
			}
			
			function Initialize()
			{
			
			
			                  
                /*if(document.getElementById("hidCalledFrom").value=='Rental' || document.getElementById("hidCalledFrom").value=='RENTAL')
                {
                
                   //document.getElementById("cmbREPLACEMENT_COSTren").style.display = "inline";
                   //document.getElementById("rfvREPLACEMENT_COSTren").setAttribute("enabled",true);
                   //document.getElementById("rfvREPLACEMENT_COST").setAttribute("enabled",false);
                   //document.getElementById("rfvREPLACEMENT_COST").style.display = "none";
                   //document.getElementById("csvREPLACEMENT_COST").setAttribute("enabled",false);
                   //document.getElementById("spnREPLACEMENT_COST").style.display = "none";                   
                   //document.getElementById("csvREPLACEMENT_COST").style.display = "none";
                   document.getElementById("revREPAIR_COST").setAttribute("enabled",true);
                   document.getElementById("txtREPAIR_COST").style.display ="inline"
                   document.getElementById("txtREPLACEMENT_COST").style.display = "inline"; 
                   document.getElementById("spnREPLACEMENT_COST").style.display = "inline";                   
                }
                else
                {
					//document.getElementById("spnREPLACEMENT_COST").style.display = "none";                   
					//document.getElementById("rfvREPLACEMENT_COST").style.display = "none";
					//document.getElementById("csvREPLACEMENT_COST").style.display = "none";
                   //document.getElementById("csvREPLACEMENT_COST").setAttribute("enabled",false);
                   //document.getElementById("rfvREPLACEMENT_COST").setAttribute("enabled",false);
                   //document.getElementById("rfvREPLACEMENT_COSTren").setAttribute("enabled",false);
                   //document.getElementById("rfvREPLACEMENT_COSTren").style.display = "none";
                   //document.getElementById("cmbREPLACEMENT_COSTren").style.display = "none";
                   document.getElementById("txtREPLACEMENT_COST").style.display = "inline"; 
                   document.getElementById("txtREPAIR_COST").style.display ="none"
                   document.getElementById("revREPAIR_COST").setAttribute("enabled",false);
                   

                
                }*/
                //Visibility of fields to be set as implemented at application
                var policyType = document.getElementById('hidPolicyType').value;
             	if ( document.POL_DWELLINGS_INFO.hidOldData.value == '' && document.POL_DWELLINGS_INFO.hidFormSaved.value == "0")
				{	
					document.getElementById('cmbLOCATION_ID').options.selectedIndex = -1;
				}
				
				
			}
			function ResetForm()
			{
				//alert(document.POL_DWELLINGS_INFO.hidOldData.value);
				DisableValidators();
				if ( document.POL_DWELLINGS_INFO.hidOldData.value == '' )
				{
					AddData();
					
				}
				else
				{
					//alert(document.POL_DWELLINGS_INFO.hidOldData.value);
					PopulateFormData(document.POL_DWELLINGS_INFO.hidOldData.value,POL_DWELLINGS_INFO);
					
					if(document.getElementById('txtPURCHASE_PRICE'))
						document.getElementById('txtPURCHASE_PRICE').value=formatCurrency(document.getElementById('txtPURCHASE_PRICE').value);
						
					if (document.getElementById('txtMARKET_VALUE'))
						document.getElementById('txtMARKET_VALUE').value=formatCurrency(document.getElementById('txtMARKET_VALUE').value);
						
					if(document.getElementById('txtREPLACEMENT_COST'))
					   document.getElementById('txtREPLACEMENT_COST').value=formatCurrency(document.getElementById('txtREPLACEMENT_COST').value);
					   
                   //document.getElementById('txtREPAIR_COST').value=formatCurrency(document.getElementById('txtREPAIR_COST').value);  
				}
				//EnableDisableDesc(document.getElementById('cmbIS_DWELLING_OWNED_BY_OTHER'),document.getElementById('txtDwellingOwned'),document.getElementById('lblDwellingOwned'));
				YearBuilt_OnBlur();
				return false;
			}
			
			function AddData()
			{
				//DisableValidators();

				document.getElementById('hidDWELLING_ID').value	=	'0';
				document.getElementById('txtDWELLING_NUMBER').focus();
				document.getElementById('txtDWELLING_NUMBER').value  = '';
				
				
				document.getElementById('cmbLOCATION_ID').selectedIndex = -1;
			
				//document.getElementById('cmbSUB_LOC_ID').selectedIndex  = -1;
				document.getElementById('txtYEAR_BUILT').value  = '';
				document.getElementById('txtPURCHASE_YEAR').value  = '';
				document.getElementById('txtPURCHASE_PRICE').value  = '';
				document.getElementById('txtMARKET_VALUE').value  = '';
				if(document.getElementById('txtREPLACEMENT_COST'))
				{
					document.getElementById('txtREPLACEMENT_COST').value  = '';
				}
				//document.getElementById('txtREPAIR_COST').value  = '';
				document.getElementById('cmbBUILDING_TYPE').selectedIndex = -1;
				document.getElementById('cmbOCCUPANCY').selectedIndex = -1;
				//document.getElementById('txtNEED_OF_UNITS').value  = '';
				//document.getElementById('txtUSAGE').value  = '';
				document.getElementById('cmbNEIGHBOURS_VISIBLE').selectedIndex = -1;
				//document.getElementById('cmbIS_VACENT_OCCUPY').selectedIndex = -1;
				//document.getElementById('cmbIS_RENTED_IN_PART').selectedIndex = -1;
				document.getElementById('cmbOCCUPIED_DAILY').selectedIndex = -1;
				document.getElementById('txtNO_WEEKS_RENTED').value = '';
				//document.getElementById('cmbIS_DWELLING_OWNED_BY_OTHER').selectedIndex = -1;
				
				document.getElementById('cmbLOCATION_ID').selectedIndex = -1;
				//document.getElementById('cmbSUB_LOC_ID').selectedIndex = -1;
			
				ApplyColor();	
				ChangeColor();
				
			}
			
			/*function setTab()
			{
				if ( document.POL_DWELLINGS_INFO.hidDWELLING_ID.value == "0" )
				{
					RemoveTab(7,top.frames[1]);
					RemoveTab(6,top.frames[1]);
					RemoveTab(5,top.frames[1]);
					RemoveTab(4,top.frames[1]);
					RemoveTab(3,top.frames[1]);
					RemoveTab(2,top.frames[1]);
					//RemoveTab(4,top.frames[1]);
					//RemoveTab(3,top.frames[1]);
					//RemoveTab(2,top.frames[1]);					}
				}
				else
				{
					var dwellingID = document.POL_DWELLINGS_INFO.hidDWELLING_ID.value;
					
					//Url="AddSquareFootage.aspx?CustomerID=" + document.POL_DWELLINGS_INFO.hidCustomerID.value + "&POLID=" + document.POL_DWELLINGS_INFO.hidPOLID.value + "&POLVersionID=" + document.POL_DWELLINGS_INFO.hidPOLVersionID.value + "&DWELLINGID=" + dwellingID + "&CalledFrom=" + document.POL_DWELLINGS_INFO.hidCalledFrom.value + "&";
					//DrawTab(2,top.frames[1],'Square Footage',Url);															
					
					Url="AddHomeRating.aspx?CustomerID=" + document.POL_DWELLINGS_INFO.hidCustomerID.value + "&POLID=" + document.POL_DWELLINGS_INFO.hidPOLID.value + "&POLVersionID=" + document.POL_DWELLINGS_INFO.hidPOLVersionID.value + "&DWELLINGID=" + dwellingID  + "&CalledFrom=" + document.POL_DWELLINGS_INFO.hidCalledFrom.value + "&";
					DrawTab(2,top.frames[1],'Rating Info',Url);
					
					//Url = "AddHomeConstruction.aspx?CustomerID=" + document.POL_DWELLINGS_INFO.hidCustomerID.value + "&POLID=" + document.POL_DWELLINGS_INFO.hidPOLID.value + "&POLVersionID=" + document.POL_DWELLINGS_INFO.hidPOLVersionID.value + "&DWELLINGID=" + dwellingID   + "&CalledFrom=" + document.POL_DWELLINGS_INFO.hidCalledFrom.value + "&";
					//DrawTab(4,top.frames[1],'Construction',Url);
					
					Url="../AdditionalInterestIndex.aspx?Customer_ID=" + document.POL_DWELLINGS_INFO.hidCustomerID.value + "&POL_ID=" + document.POL_DWELLINGS_INFO.hidPOLID.value + "&POL_Version_ID=" + document.POL_DWELLINGS_INFO.hidPOLVersionID.value + "&DWELLINGID=" + dwellingID  + "&CalledFrom=" + document.POL_DWELLINGS_INFO.hidCalledFrom.value + "&";
					DrawTab(3,top.frames[1],'Additional Interest',Url);
				
					Url="AddDwellingCoverageLimit.aspx?CustomerID=" + document.POL_DWELLINGS_INFO.hidCustomerID.value + "&POLID=" + document.POL_DWELLINGS_INFO.hidPOLID.value + "&POLVersionID=" + document.POL_DWELLINGS_INFO.hidPOLVersionID.value + "&DWELLINGID=" + dwellingID  + "&CalledFrom=" + document.POL_DWELLINGS_INFO.hidCalledFrom.value + "&";
					DrawTab(4,top.frames[1],'Coverage/Limits',Url);
					
					Url="Coverages_Section1.aspx?CustomerID=" + document.POL_DWELLINGS_INFO.hidCustomerID.value + "&POLID=" + document.POL_DWELLINGS_INFO.hidPOLID.value + "&POLVersionID=" + document.POL_DWELLINGS_INFO.hidPOLVersionID.value + "&DWELLINGID=" + dwellingID  + "&CalledFrom=" + document.POL_DWELLINGS_INFO.hidCalledFrom.value + "&";
					DrawTab(5,top.frames[1],'Section 1',Url);
					
					Url="Coverages_Section2.aspx?CustomerID=" + document.POL_DWELLINGS_INFO.hidCustomerID.value + "&POLID=" + document.POL_DWELLINGS_INFO.hidPOLID.value + "&POLVersionID=" + document.POL_DWELLINGS_INFO.hidPOLVersionID.value + "&DWELLINGID=" + dwellingID  + "&CalledFrom=" + document.POL_DWELLINGS_INFO.hidCalledFrom.value + "&";
					DrawTab(6,top.frames[1],'Section 2',Url);
					
					Url="HomeOwnersEndorsements.aspx?CustomerID=" + document.POL_DWELLINGS_INFO.hidCustomerID.value + "&POLID=" + document.POL_DWELLINGS_INFO.hidPOLID.value + "&POLVersionID=" + document.POL_DWELLINGS_INFO.hidPOLVersionID.value + "&DWELLINGID=" + dwellingID  + "&CalledFrom=" + document.POL_DWELLINGS_INFO.hidCalledFrom.value + "&";
					DrawTab(7,top.frames[1],'Endorsement',Url);
					
					//Url="AddProtectDevices.aspx?CustomerID=" + document.POL_DWELLINGS_INFO.hidCustomerID.value + "&POLID=" + document.POL_DWELLINGS_INFO.hidPOLID.value + "&POLVersionID=" + document.POL_DWELLINGS_INFO.hidPOLVersionID.value + "&DWELLINGID=" + dwellingID  + "&CalledFrom=" + document.POL_DWELLINGS_INFO.hidCalledFrom.value + "&";
					//DrawTab(7,top.frames[1],'Protective Devices',Url);
				}
								
			}*/
				
				function ShowCopy()
				{
					if(document.POL_DWELLINGS_INFO.btnCopy)
					if(document.POL_DWELLINGS_INFO.txtDWELLING_NUMBER.value>=2147483647) 
						document.POL_DWELLINGS_INFO.btnCopy.disabled=true; 
					else 
						document.POL_DWELLINGS_INFO.btnCopy.disabled=false;
				
				}
			function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbOCCUPANCY":
						lookupMessage	=	"OCCUPA.";
						break;
					case "cmbBUILDING_TYPE":
						lookupMessage	=	"HBLDTY.";
						break;
					default:
						lookupMessage	=	"Look up code not found";
						break;
						
				}
				showLookupLayer(controlId,lookupMessage);							
			}
			
			function CallEnableDisable()
			{
					//EnableDisableDesc(document.getElementById('cmbIS_DWELLING_OWNED_BY_OTHER'),document.getElementById('txtDwellingOwned'),document.getElementById('lblDwellingOwned'));		
			}	
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
			
	/*function ChkOccurenceDate(objSource , objArgs)
	{
		if(document.getElementById("revYEAR_BUILT").isvalid==true)
		//document.getElementById("revYEAR_BUILT").isvalid
		{
			var effdate=document.POL_DWELLINGS_INFO.txtYEAR_BUILT.value;

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
		 else
			objArgs.IsValid = true;
	}*/
	function ChkOccurenceDate(objSource , objArgs)
	{
		var effdate=document.POL_DWELLINGS_INFO.txtYEAR_BUILT.value;




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

	
	/*function ChkPurchaseDate(objSource , objArgs)
	{
	    if(document.getElementById("revPURCHASE_YEAR").isvalid==true )
	    {
			var effdate = parseInt(document.POL_DWELLINGS_INFO.txtPURCHASE_YEAR.value);

			var date=parseInt('<%=DateTime.Now.Year%>');
			if(effdate.length < 4)
			{
				objArgs.IsValid = false;
			}
			else
			{
				if(parseInt(effdate) <= parseInt(date) && parseInt(effdate) >= parseInt(document.getElementById("txtYEAR_BUILT").value) )
				{
					objArgs.IsValid = true;
				}
				else
				{
					objArgs.IsValid = false;
				}
			}
		}
		else
			objArgs.IsValid = true;
				
	}*/
	function ChkPurchaseDate(objSource , objArgs)
	{
		var effdate=document.POL_DWELLINGS_INFO.txtPURCHASE_YEAR.value;

		var date='<%=DateTime.Now.Year%>';
		
		//Check for date entered to be numeric and of 4 characters
		if(isNaN(effdate) || effdate.length<4 || effdate<1)
		{
			objArgs.IsValid = false;
			document.getElementById('csvPurchase_YEAR').innerText = "Year must be numeric and in #### format.";
			return;
		}
		//Check for purchase year to be greater than built year
		var builtYear = document.getElementById("txtYEAR_BUILT").value;		
		if(builtYear!="")
		{
			if(parseInt(effdate) < parseInt(builtYear))			
			{				
				objArgs.IsValid = false;
				document.getElementById('csvPurchase_YEAR').innerText = "Purchase Year can't be less than built year";
				return;
			}			
		}		
		//Check for date to be not a future date
		if(effdate > date)
		{				
			objArgs.IsValid = false;
			document.getElementById('csvPurchase_YEAR').innerText = "Purchase year cannot be future year";
			return;			
		}
		//If all validations do not turn false, return true and set validation message to blank		
		objArgs.IsValid = true;
				
	}
	
	
	
	function CheckReplacementCost(objSource , objArgs)
	{
		value = document.getElementById("txtREPLACEMENT_COST").value;
			value = ReplaceString(value, ",","");
			if (value != "")
			{
				if (isNaN(value))
				{
					objArgs.IsValid = false;
					return;
				}
				else
				{
					if (parseFloat(value) == 0)
					{
						objArgs.IsValid = false;
						return;
					}
				}
			}
			objArgs.IsValid = true;
	}
	
	
	//added by vj on 19-10-2005
	function SetRemoveTabValue()
	{
		if (document.getElementById('hidCheckDelete').value == "1")
		{
			this.parent.document.getElementById('hidRemoveTab').value = "1";
		}
		else
		{
			this.parent.document.getElementById('hidRemoveTab').value = "0";		
		}
	}
	
	function SetActivateDeactivateButton()
	{
	
	    
		if (! document.getElementById("btnActivateDeactivate"))
			return;
			
		if (document.getElementById('hidIS_ACTIVE').value =="0")
		{
			document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
		}
		else
		{
	
			if ( document.getElementById('hidIS_ACTIVE').value == "Y")
			{
				document.getElementById("btnActivateDeactivate").value = "Deactivate";
			}
			else
			{
				document.getElementById("btnActivateDeactivate").value = "Activate";
				/*Commented : ITrack # 6254 - 12 Aug 09 -Manoj
				RemoveTab(8,this.parent);
				RemoveTab(7,this.parent);	
				RemoveTab(6,this.parent);	
				RemoveTab(5,this.parent);	
				RemoveTab(4,this.parent);	
				RemoveTab(3,this.parent);	
				RemoveTab(2,this.parent);*/
				
			}
		}
	}
	function ValidateDwelling(objSource , objArgs)
	{
		var DwellingNum = document.getElementById("txtDWELLING_NUMBER").value;		
		
		
		if(isNaN(DwellingNum))		
		{
			document.getElementById("csvDWELLING_NUMBER").innerText = "Please enter a numeric value";
			objArgs.IsValid = false;
		}
		else if(isNaN(parseInt(DwellingNum)) || parseInt(DwellingNum)<1)
		{			
			document.getElementById("csvDWELLING_NUMBER").innerText = "Please enter a positive and non-zero value";
			objArgs.IsValid = false;			
		}
		else if(parseInt(DwellingNum)>2147483647)
		{
			document.getElementById("csvDWELLING_NUMBER").innerText = "Customer Dwelling number must be between 1 and 2147483647";
			objArgs.IsValid = false;			
		}
		else
			objArgs.IsValid = true;	
		
			
	}


		
		</script>
	</HEAD>
	<BODY oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="Initialize();ApplyColor();ChangeColor();ShowCopy();CallEnableDisable();SetRemoveTabValue();SetActivateDeactivateButton();SetMinMarketValue();">
		<FORM id="POL_DWELLINGS_INFO" method="post" runat="server">
			<asp:repeater id="Repeater1" runat="server"></asp:repeater>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr id="trError" runat="server">
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblError" runat="server" CssClass="errmsg"></asp:label></td>
				</tr>
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg"></asp:label></td>
				</tr>
				
				<TR id="trBody" runat="server">
					<TD>
						<table>
							<TBODY>
			
								<tr>
									<TD class="pageHeader" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow>Please 
										note that all fields marked with * are mandatory
									</TD>
								</tr>
								
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capDWELLING_NUMBER" runat="server">Client Dwelling Number</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDWELLING_NUMBER" onblur="javascript:ShowCopy();" runat="server" maxlength="10"
											size="15"></asp:textbox><BR>
										<%--<asp:requiredfieldvalidator id="rfvDWELLING_NUMBER" runat="server" Display="Dynamic" ErrorMessage="Please enter the Client dwelling number"
										ControlToValidate="txtDWELLING_NUMBER"></asp:requiredfieldvalidator><asp:rangevalidator id="rngDWELLING_NUMBER" Display="Dynamic" ControlToValidate="txtDWELLING_NUMBER"
										Runat="server" MaximumValue="2147483647" MinimumValue="0" Type="Integer"></asp:rangevalidator></TD>--%>
										<asp:requiredfieldvalidator id="rfvDWELLING_NUMBER" runat="server" Display="Dynamic" ErrorMessage="Please enter the Client dwelling number"
											ControlToValidate="txtDWELLING_NUMBER"></asp:requiredfieldvalidator><asp:rangevalidator id="rngDWELLING_NUMBER" Display="Dynamic" ControlToValidate="txtDWELLING_NUMBER"
											Runat="server" MaximumValue="2147483647" MinimumValue="0" Type="Integer" Enabled="False"></asp:rangevalidator>
										<asp:customvalidator id="csvDWELLING_NUMBER" Runat="server" ControlToValidate="txtDWELLING_NUMBER" Display="Dynamic"
											ClientValidationFunction="ValidateDwelling" ErrorMessage="Error"></asp:customvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capLOCATION_ID" runat="server">Location</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLOCATION_ID" onfocus="SelectComboIndex('cmbLOCATION_ID')" runat="server" onchange="SetMinMarketValue();"
											Width="265px"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvLOCATION_ID" runat="server" Display="Dynamic" ControlToValidate="cmbLOCATION_ID"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capYEAR_BUILT" runat="server">Year built</asp:label><span class="mandatory">*</span>
									</TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtYEAR_BUILT" runat="server" maxlength="4" size="5" ></asp:textbox><br>
										<%--<asp:requiredfieldvalidator id="rfvYEAR_BUILT" Display="Dynamic" ControlToValidate="txtYEAR_BUILT" Runat="server"></asp:requiredfieldvalidator>--%>
										<asp:regularexpressionvalidator id="revYEAR_BUILT" Enabled="False" runat="server" Display="Dynamic" ControlToValidate="txtYEAR_BUILT"></asp:regularexpressionvalidator><%--<asp:customvalidator id="csvYEAR_BUILT" Display="Dynamic" ErrorMessage="Year built should be less or equal to current year."
										Enabled="False" ControlToValidate="txtYEAR_BUILT" Runat="server" ClientValidationFunction="ChkOccurenceDate"></asp:customvalidator>
									<asp:rangevalidator id="rngYEAR_BUILT" ControlToValidate="txtYEAR_BUILT" Display="Dynamic" Type="Integer"
										Runat="server" MinimumValue="1900"></asp:rangevalidator>--%>
										<asp:requiredfieldvalidator id="rfvYEAR_BUILT" Display="Dynamic" ControlToValidate="txtYEAR_BUILT" Runat="server"></asp:requiredfieldvalidator><asp:customvalidator id="csvYEAR_BUILT" Runat="server" ControlToValidate="txtYEAR_BUILT" Display="Dynamic"
											ClientValidationFunction="ChkOccurenceDate"></asp:customvalidator>
									</TD>
									<TD class="midcolora" width="18%"><asp:label id="capPURCHASE_YEAR" runat="server">Purchase Year</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtPURCHASE_YEAR" runat="server" maxlength="4" size="5"></asp:textbox><br>
										<%--<asp:regularexpressionvalidator id="revPURCHASE_YEAR" runat="server" Display="Dynamic" ControlToValidate="txtPURCHASE_YEAR"></asp:regularexpressionvalidator><asp:customvalidator id="csvPurchase_YEAR" runat="server" Display="Dynamic" ErrorMessage="Purchase year should be between year built and current year."
										ControlToValidate="txtPURCHASE_YEAR" ClientValidationFunction="ChkPurchaseDate" Enabled="True"></asp:customvalidator></TD>--%>
										<asp:comparevalidator id="cmpPURCHASE_YEAR" runat="server" Display="Dynamic" ControlToValidate="txtPURCHASE_YEAR"
											Type="Integer" Enabled="False" ControlToCompare="txtYEAR_BUILT" Operator="GreaterThanEqual"></asp:comparevalidator>
										<asp:CustomValidator Enabled="True" id="csvPurchase_YEAR" runat="server" ControlToValidate="txtPURCHASE_YEAR"
											Display="Dynamic" ClientValidationFunction="ChkPurchaseDate"></asp:CustomValidator></TD>
								</tr>
				</TR>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capPURCHASE_PRICE" runat="server">Purchase price</asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtPURCHASE_PRICE" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="20"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revPURCHASE_PRICE" runat="server" Display="Dynamic" ControlToValidate="txtPURCHASE_PRICE"></asp:regularexpressionvalidator></TD>
					<TD class="midcolora" width="18%"><asp:label id="capMARKET_VALUE" runat="server">Market Value</asp:label>					
					</TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtMARKET_VALUE" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="20"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvMARKET_VALUE" runat="server" Display="Dynamic" ErrorMessage="Please enter Market/Repair Cost" ControlToValidate="txtMARKET_VALUE"></asp:requiredfieldvalidator>
						<asp:customvalidator id="csvMARKET_VALUE" Runat="server" Display="Dynamic" ControlToValidate="txtREPLACEMENT_COST"
										ClientValidationFunction="CheckMarketCost"></asp:customvalidator>					
						<asp:regularexpressionvalidator id="revMARKET_VALUE" runat="server" Display="Dynamic" ControlToValidate="txtMARKET_VALUE"></asp:regularexpressionvalidator>
						<asp:comparevalidator id="cmpMARKET_VALUE" Display="Dynamic" ControlToValidate="txtMARKET_VALUE" Runat="server"
							Type="Currency" Operator="GreaterThanEqual" ValueToCompare="0" Enabled="False"></asp:comparevalidator>
					</TD>
					
				</tr>
				<tr id="trReplace" runat=server visible ="false">
					<TD class="midcolora" width="18%"><asp:label id="capREPLACEMENT_COST" runat="server">Replacement Cost</asp:label><span class="mandatory" id="spnREPLACEMENT_COST">*</span></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtREPLACEMENT_COST" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="20"></asp:textbox><br>
						<asp:comparevalidator id="cmpREPLACEMENT_COST" Display="Dynamic" ControlToValidate="txtREPLACEMENT_COST"
							Runat="server" Type="Currency" Operator="GreaterThanEqual" ValueToCompare="0"></asp:comparevalidator>
						<asp:requiredfieldvalidator id="rfvREPLACEMENT_COST" Display="Dynamic" ControlToValidate="txtREPLACEMENT_COST"
							Runat="server"></asp:requiredfieldvalidator>
						<asp:CustomValidator id="csvREPLACEMENT_COST" runat="server" Enabled="False" Display="Dynamic" ControlToValidate="txtREPLACEMENT_COST"
							ClientValidationFunction="CheckReplacementCost"></asp:CustomValidator>
					</TD>
					<td class="midcolora" width="18%"><asp:label id="capREPLACEMENTCOST_COVA" runat="server"></asp:label></td>
					<td class="midcolora" width="32%"><asp:CheckBox id="cbREPLACEMENTCOST_COVA" runat="server" Checked="True"></asp:CheckBox></td>
				</tr>
					
					<tr runat ="server" visible ="false">
					
						<TD class="midcolora" width="18%">
									# of Months Rented
								</TD>
								<TD class="midcolora" width="32%">
									<asp:textbox id="txtMONTHS_RENTED" runat="server" size="3" MaxLength="2"></asp:textbox><br>
									<asp:comparevalidator id="cmpMONTHS_RENTED" Display="Dynamic" ControlToValidate="txtMONTHS_RENTED" Runat="server"
										Type="Integer" Operator="GreaterThanEqual" ValueToCompare="0" ErrorMessage="Please enter numeric value"></asp:comparevalidator>
									<asp:customvalidator id="csvMONTHS_RENTED" Runat="server" Display="Dynamic" ControlToValidate="txtMONTHS_RENTED"
										ClientValidationFunction="CheckMonthsRented" ErrorMessage="Dwelling not to be rented for more than 6 months"></asp:customvalidator>
								</TD>
								<td class="midcolora" colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capBUILDING_TYPE" runat="server">Building type</asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbBUILDING_TYPE" onfocus="SelectComboIndex('cmbBUILDING_TYPE')" runat="server"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbBUILDING_TYPE')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A><br>
					<asp:requiredfieldvalidator id="rfvBUILDING_TYPE" runat="server" Display="Dynamic" ControlToValidate="cmbBUILDING_TYPE"></asp:requiredfieldvalidator>
					</TD>
					<TD class="midcolora" width="18%"><asp:label id="capOCCUPANCY" runat="server">Occupancy</asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbOCCUPANCY" onfocus="SelectComboIndex('cmbOCCUPANCY')" runat="server"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbOCCUPANCY')"></A><BR>
					<asp:requiredfieldvalidator id="rfvOCCUPANCY" Display="Dynamic" ControlToValidate="cmbOCCUPANCY" Runat="server" ErrorMessage=" "></asp:requiredfieldvalidator>
					</TD>
				</tr>
				<tr runat ="server" visible ="false">
					<TD class="midcolora" width="18%"><asp:label id="capNEIGHBOURS_VISIBLE" runat="server">Visible to neighbors</asp:label></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbNEIGHBOURS_VISIBLE" onfocus="SelectComboIndex('cmbNEIGHBOURS_VISIBLE')" runat="server">
							<asp:ListItem></asp:ListItem>
							<asp:ListItem Value="N">No</asp:ListItem>
							<asp:ListItem Value="Y">Yes</asp:ListItem>							
						</asp:dropdownlist></TD>
						<TD class="midcolora" colspan="2">&nbsp;</TD> 
				</tr>
				<tr runat ="server" visible ="false">
					<TD class="midcolora" width="18%"><asp:label id="capOCCUPIED_DAILY" runat="server">Occupied daily</asp:label></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbOCCUPIED_DAILY" onfocus="SelectComboIndex('cmbOCCUPIED_DAILY')" runat="server">
							<asp:ListItem></asp:ListItem>
							<asp:ListItem Value="N">No</asp:ListItem>
							<asp:ListItem Value="Y">Yes</asp:ListItem>							
						</asp:dropdownlist></TD>
					<TD class="midcolora" width="18%"><asp:label id="capNO_WEEKS_RENTED" runat="server"># of weeks rented</asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtNO_WEEKS_RENTED" runat="server" maxlength="3" size="3"></asp:textbox><br>
						<asp:rangevalidator id="rngNO_WEEKS_RENTED" runat="server" Display="Dynamic" ControlToValidate="txtNO_WEEKS_RENTED"
							MaximumValue="100" MinimumValue="0" Type="Integer"></asp:rangevalidator></TD>
				</tr>
				<tr>
					<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" CausesValidation="False"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnCopy" runat="server" Text="Copy" CausesValidation="False"
							name="btnCopy"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" CausesValidation="False"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" Runat="server" CausesValidation="False"
							text="Activate/Deactivate"></cmsb:cmsbutton></td>
					<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
				</tr>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidDWELLING_ID" type="hidden" value="0" name="hidDWELLING_ID" runat="server">
			<INPUT id="hidPOLID" type="hidden" name="hidPOLID" runat="server"> <INPUT id="hidPOLVersionID" type="hidden" name="hidPOLVersionID" runat="server">
			<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"> <INPUT id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server">
			<INPUT id="hidCheckDelete" type="hidden" name="hidCheckDelete" runat="server"> <INPUT id="hidPolicyType" type="hidden" name="hidPolicyType" runat="server">
			<INPUT id="hidStateId" type="hidden" name="hidStateId" runat="server"> <INPUT id="hidPercent" type="hidden" name="hidPercent" runat="server">
			<INPUT id="hidCustomInfo" type="hidden" name="hidCustomInfo" runat="server"> 
			<INPUT id="hidEffDate" type="hidden" name="hidEffDate" runat="server">	
			<INPUT id="hidAppEffDate" type="hidden" name="hidAppEffDate" runat="server">	<!-- Added by Charles on 18-Dec-09 for Itrack 6681 -->	
			</TD></TR></TBODY></TABLE>
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none; Z-INDEX: 101; POSITION: absolute"><iframe id="lookupLayerIFrame" style="DISPLAY: none; Z-INDEX: 1001; FILTER: alpha(opacity=0); BACKGROUND-COLOR: #000000"
				width="0" height="0" top="0px;" left="0px"></iframe>
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
					<td colSpan="2"><span id="LookUpMsg"></span></td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->
		<script>
		
				
			if (document.getElementById("hidFormSaved").value == "5")
			 {
				/*Record deleted*/
				/*Refreshing the grid and coverting the form into add mode*/
				/*Using the javascript*/
				RemoveTab(8,this.parent);
				RemoveTab(7,this.parent);	
				RemoveTab(6,this.parent);	
				RemoveTab(5,this.parent);	
				RemoveTab(4,this.parent);	
				RemoveTab(3,this.parent);	
				RemoveTab(2,this.parent);
				RefreshWebGrid("5","1",true,true); 
				document.getElementById('hidDWELLING_ID').value = "NEW";				
			}
			else if (document.getElementById("hidFormSaved").value == "1")
			{
				this.parent.strSelectedRecordXML = "-1";
				RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidDWELLING_ID').value,true);
				
			}
		</script>
	</BODY>
</HTML>

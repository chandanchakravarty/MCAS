<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddReinsurancePremiumBuilder.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Reinsurance.AddReinsurancePremiumBuilder" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddReinsurancePremiumBuilder</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<LINK href="Common.css" type="text/css" rel="stylesheet">
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
		var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		
		var txtamount_alarm =0.0000;
		var txtamount_home =0.0000;
		var txtamount =0.0000;
		function AddData()
		{
			document.getElementById('hidPREMIUM_BUILDER_ID').value	=	'New';
			if(document.getElementById('btnActivateDeactivate'))
				document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			if(document.getElementById('btnDelete'))
				document.getElementById('btnDelete').style.display = "none";	
			
		}
		
		function populateXML()
		{
			if(document.getElementById('hidFormSaved').value == '0')
			{
				var tempXML = document.getElementById('hidOldData').value;
				if(tempXML != "" && tempXML!="0")
				{
					populateFormData(tempXML,REIN_PREMIUM_BUILDER);
					//addCatagories();
				}
				else
				{
					AddData();	
				}
			}
			if(document.getElementById('txtLAYER'))
			{
			//Added by Raghav get focus on hidden text.
				try
				{
					document.getElementById('txtLAYER').focus();	
				}
				catch(e)
                {
					return false;
				}
			}	
			EnableValidator('revLAYER',false);	
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
		
		// For multiselect box Coverage category : Start
		function setCatagories()
		{
			document.REIN_PREMIUM_BUILDER.hidCATAGORY.value = '';
			for (var i=0;i< document.getElementById('cmbCOVERAGE_CATEGORY').options.length;i++)
			{
				document.REIN_PREMIUM_BUILDER.hidCATAGORY.value = document.REIN_PREMIUM_BUILDER.hidCATAGORY.value + document.getElementById('cmbCOVERAGE_CATEGORY').options[i].value + ',';
			}	
			
			//Page_ClientValidate();
			var retVal = setProtections();	
			var returnVal = funcValidateCatagories();
			
			return Page_IsValid && returnVal && retVal ;
		}
		
		function setCategory(catvalue)
		{
			for(s = document.getElementById('cmbFROMCATEGORY').length-1; s >=0;s--)
			{
				if(document.getElementById('cmbFROMCATEGORY').options[s].value == catvalue)
				{	
					document.getElementById('cmbCOVERAGE_CATEGORY').options[document.getElementById('cmbCOVERAGE_CATEGORY').length-1].text = document.getElementById('cmbFROMCATEGORY').options[s].text;
					document.getElementById('cmbFROMCATEGORY').options[s]=null;
					break;
				}
			}	
		}
		function addCatagories()
		{
		
			var Catagories = document.getElementById("hidCATAGORY").value;
			var Catagorie = Catagories.split(",");
			for(j = document.getElementById('cmbCOVERAGE_CATEGORY').length-1; j >=0;j--)
			{
				document.getElementById('cmbCOVERAGE_CATEGORY').options[j].value= null;
			}
			for(j = 0; j < Catagorie.length-1 ;j++)
			{
				document.getElementById('cmbCOVERAGE_CATEGORY').options.length=document.getElementById('cmbCOVERAGE_CATEGORY').length+1;
				document.getElementById('cmbCOVERAGE_CATEGORY').options[document.getElementById('cmbCOVERAGE_CATEGORY').length-1].value=Catagorie[j];
				setCategory(Catagorie[j]);
			}
		}
		
		function selectCatagories()
		{
			for (var i=document.getElementById('cmbFROMCATEGORY').options.length-1;i>=0;i--)
			{
					if (document.getElementById('cmbFROMCATEGORY').options[i].selected == true)
					{
						addOption(document.getElementById('cmbCOVERAGE_CATEGORY'), document.getElementById('cmbFROMCATEGORY').options[i].text, document.getElementById('cmbFROMCATEGORY').options[i].value);
						//document.getElementById('cmbCOVERAGE_CATEGORY').options.length=document.getElementById('cmbCOVERAGE_CATEGORY').length+1;
						//document.getElementById('cmbCOVERAGE_CATEGORY').options[document.getElementById('cmbCOVERAGE_CATEGORY').length-1].value=document.getElementById('cmbFROMCATEGORY').options[i].value;
						//document.getElementById('cmbCOVERAGE_CATEGORY').options[document.getElementById('cmbCOVERAGE_CATEGORY').length-1].text=document.getElementById('cmbFROMCATEGORY').options[i].text;
						document.getElementById('cmbFROMCATEGORY').options[i] = null;
					}
		  	}
		  	
			
			return false;
		  	
		}
		
		function deselectCatagories()
		{
		  for (var i=document.getElementById('cmbCOVERAGE_CATEGORY').options.length-1;i>=0;i--)
			{
			
				if (document.getElementById('cmbCOVERAGE_CATEGORY').options[i].selected == true)
				{
					addOption(document.getElementById('cmbFROMCATEGORY'), document.getElementById('cmbCOVERAGE_CATEGORY').options[i].text, document.getElementById('cmbCOVERAGE_CATEGORY').options[i].value);
					//document.getElementById('cmbFROMCATEGORY').options.length=document.getElementById('cmbFROMCATEGORY').length+1;
					//document.getElementById('cmbFROMCATEGORY').options[document.getElementById('cmbFROMCATEGORY').length-1].value=document.getElementById('cmbCOVERAGE_CATEGORY').options[i].value;
					//document.getElementById('cmbFROMCATEGORY').options[document.getElementById('cmbFROMCATEGORY').length-1].text=document.getElementById('cmbCOVERAGE_CATEGORY').options[i].text;
					document.getElementById('cmbCOVERAGE_CATEGORY').options[i] = null;
				}
				
		  	}	
		  	return false;			
		
		}
		
		function funcValidateCatagories()
		{
			if(document.getElementById('cmbCOVERAGE_CATEGORY').options.length == 0)
			{
				document.getElementById('cmbCOVERAGE_CATEGORY').className = "MandatoryControl";
				document.getElementById("cmbCOVERAGE_CATEGORY").style.display="inline";
				document.getElementById("csvCOVERAGE_CATEGORY").style.display="inline";
				document.getElementById("csvCOVERAGE_CATEGORY").innerText = "Please select Category";
				return false;
			}
			else
			{
				document.getElementById('cmbCOVERAGE_CATEGORY').className = "none";
				//document.getElementById("cmbCOVERAGE_CATEGORY").style.display="none";
				return true;
			}
		}
		// For multiselect box Coverage category : End
		
		// For multiselect box Protection : Start
		
		function setProtections()
		{
			document.REIN_PREMIUM_BUILDER.hidPROTECTION.value = '';
			for (var i=0;i< document.getElementById('cmbPROTECTION').options.length;i++)
			{
				document.REIN_PREMIUM_BUILDER.hidPROTECTION.value = document.REIN_PREMIUM_BUILDER.hidPROTECTION.value + document.getElementById('cmbPROTECTION').options[i].value + ',';
			}
			//addCatagories();	
			Page_ClientValidate();						
			var returnVal = funcValidateProtection();
			return Page_IsValid && returnVal;
		}
		
		function setProtection(provalue)
		{
			for(s = document.getElementById('cmbFROMPROTECTION').length-1; s >=0;s--)
			{
				if(document.getElementById('cmbFROMPROTECTION').options[s].value == provalue)
				{	
					document.getElementById('cmbPROTECTION').options[document.getElementById('cmbPROTECTION').length-1].text = document.getElementById('cmbFROMPROTECTION').options[s].text;
					document.getElementById('cmbFROMPROTECTION').options[s]=null;
					break;
				}
			}	
		}
		function addProtection()
		{
			var Protections = document.getElementById("hidPROTECTION").value;
			var Protection = Protections.split(",");
			for(j = document.getElementById('cmbPROTECTION').length-1; j >=0;j--)
			{
				document.getElementById('cmbPROTECTION').options[j].value= null;
			}
			for(j = 0; j < Protection.length-1 ;j++)
			{
				document.getElementById('cmbPROTECTION').options.length=document.getElementById('cmbPROTECTION').length+1;
				document.getElementById('cmbPROTECTION').options[document.getElementById('cmbPROTECTION').length-1].value=Protection[j];
				setProtection(Protection[j]);
			}
		}
		
		function selectProtection()
		{
			for (var i=document.getElementById('cmbFROMPROTECTION').options.length-1;i>=0;i--)
			{
					if (document.getElementById('cmbFROMPROTECTION').options[i].selected == true)
					{
						addOption(document.getElementById('cmbPROTECTION'), document.getElementById('cmbFROMPROTECTION').options[i].text, document.getElementById('cmbFROMPROTECTION').options[i].value);
						//document.getElementById('cmbPROTECTION').options.length=document.getElementById('cmbPROTECTION').length+1;
						//document.getElementById('cmbPROTECTION').options[document.getElementById('cmbPROTECTION').length-1].value=document.getElementById('cmbFROMPROTECTION').options[i].value;
						//document.getElementById('cmbPROTECTION').options[document.getElementById('cmbPROTECTION').length-1].text=document.getElementById('cmbFROMPROTECTION').options[i].text;
						document.getElementById('cmbFROMPROTECTION').options[i] = null;
					}
		  	}
		  	
			chkCheckSelect();
			return false;
		  	
		}
		
		function deselectProtection()
		{
		  for (var i=document.getElementById('cmbPROTECTION').options.length-1;i>=0;i--)
			{
			
				if (document.getElementById('cmbPROTECTION').options[i].selected == true)
				{
					addOption(document.getElementById('cmbFROMPROTECTION'), document.getElementById('cmbPROTECTION').options[i].text, document.getElementById('cmbPROTECTION').options[i].value);
					//document.getElementById('cmbFROMPROTECTION').options.length=document.getElementById('cmbFROMPROTECTION').length+1;
					//document.getElementById('cmbFROMPROTECTION').options[document.getElementById('cmbFROMPROTECTION').length-1].value=document.getElementById('cmbPROTECTION').options[i].value;
					//document.getElementById('cmbFROMPROTECTION').options[document.getElementById('cmbFROMPROTECTION').length-1].text=document.getElementById('cmbPROTECTION').options[i].text;
					document.getElementById('cmbPROTECTION').options[i] = null;
				}
				
		  	}
		  	chkCheckDeSelect();	
		  	return false;			
		
		}
		
		function funcValidateProtection()
		{
			if(document.getElementById('cmbPROTECTION').options.length == 0)
			{
				document.getElementById('cmbPROTECTION').className = "MandatoryControl";
				document.getElementById("cmbPROTECTION").style.display="inline";
				document.getElementById("csvPROTECTION").style.display="inline";
				document.getElementById("csvPROTECTION").innerText = "Please select Protection";
				return false;
			}
			else
			{
				document.getElementById('cmbPROTECTION').className = "none";
				document.getElementById("csvPROTECTION").style.display="none";
				document.getElementById("csvPROTECTION").setAttribute("enabled",false);					
				document.getElementById("csvPROTECTION").setAttribute("isValid",false);
				//document.getElementById("cmbPROTECTION").style.display="none";
				return true;
			}
		}
		// For multiselect box Protection : End
		
		function showhide_alarm()
		{
			if(document.getElementById("cmbALARM_CREDIT").options[document.getElementById("cmbALARM_CREDIT").selectedIndex].value=="10963")
			{
				document.getElementById("capALARM_PERCENTAGE").style.display="inline";
				document.getElementById("txtALARM_PERCENTAGE").style.display="inline";
				document.getElementById("spnALARM_PERCENTAGE").style.display="inline";
				//document.getElementById("rfvALARM_PERCENTAGE").style.display="inline";
				//document.getElementById("revALARM_PERCENTAGE").style.display="inline";
				document.getElementById("rfvALARM_PERCENTAGE").setAttribute("enabled",true);					
				document.getElementById("rfvALARM_PERCENTAGE").setAttribute("isValid",true);	
				document.getElementById("revALARM_PERCENTAGE").setAttribute("enabled",true);					
				document.getElementById("revALARM_PERCENTAGE").setAttribute("isValid",true);
				
			}
			else
			{
				document.getElementById("capALARM_PERCENTAGE").style.display="none";
				document.getElementById("txtALARM_PERCENTAGE").style.display="none";
				document.getElementById("txtALARM_PERCENTAGE").value = '';
				document.getElementById("rfvALARM_PERCENTAGE").style.display="none";
				document.getElementById("revALARM_PERCENTAGE").style.display="none";
				document.getElementById("spnALARM_PERCENTAGE").style.display="none";
				document.getElementById("rfvALARM_PERCENTAGE").setAttribute("enabled",false);					
				document.getElementById("rfvALARM_PERCENTAGE").setAttribute("isValid",false);
				document.getElementById("revALARM_PERCENTAGE").setAttribute("enabled",false);					
				document.getElementById("revALARM_PERCENTAGE").setAttribute("isValid",false);	
				
			}
			
		}
		function showhide_homeage()
		{
			if(document.getElementById("cmbHOME_CREDIT").options[document.getElementById("cmbHOME_CREDIT").selectedIndex].value=="10963")
			{
				document.getElementById("capHOME_AGE").style.display="inline";
				document.getElementById("txtHOME_AGE").style.display="inline";
				document.getElementById("spnHOME_AGE").style.display="inline";
				//document.getElementById("rfvHOME_AGE").style.display="inline";
				//document.getElementById("revHOME_AGE").style.display="inline";
				document.getElementById("rfvHOME_AGE").setAttribute("enabled",true);					
				document.getElementById("rfvHOME_AGE").setAttribute("isValid",true);	
				document.getElementById("revHOME_AGE").setAttribute("enabled",true);					
				document.getElementById("revHOME_AGE").setAttribute("isValid",true);
				
				document.getElementById("capHOME_PERCENTAGE").style.display="inline";
				document.getElementById("txtHOME_PERCENTAGE").style.display="inline";
				document.getElementById("spnHOME_PERCENTAGE").style.display="inline";
				document.getElementById("rfvHOME_PERCENTAGE").setAttribute("enabled",true);					
				document.getElementById("rfvHOME_PERCENTAGE").setAttribute("isValid",true);	
				document.getElementById("revHOME_PERCENTAGE").setAttribute("enabled",true);					
				document.getElementById("revHOME_PERCENTAGE").setAttribute("isValid",true);
				
			}
			else
			{
				document.getElementById("capHOME_AGE").style.display="none";
				document.getElementById("txtHOME_AGE").style.display="none";
				document.getElementById("txtHOME_AGE").value = '';
				document.getElementById("rfvHOME_AGE").style.display="none";
				document.getElementById("revHOME_AGE").style.display="none";
				document.getElementById("spnHOME_AGE").style.display="none";
				document.getElementById("rfvHOME_AGE").setAttribute("enabled",false);					
				document.getElementById("rfvHOME_AGE").setAttribute("isValid",false);
				document.getElementById("revHOME_AGE").setAttribute("enabled",false);					
				document.getElementById("revHOME_AGE").setAttribute("isValid",false);
				
				document.getElementById("capHOME_PERCENTAGE").style.display="none";
				document.getElementById("txtHOME_PERCENTAGE").style.display="none";
				document.getElementById("txtHOME_PERCENTAGE").value = '';
				document.getElementById("rfvHOME_PERCENTAGE").style.display="none";
				document.getElementById("revHOME_PERCENTAGE").style.display="none";
				document.getElementById("spnHOME_PERCENTAGE").style.display="none";
				document.getElementById("rfvHOME_PERCENTAGE").setAttribute("enabled",false);					
				document.getElementById("rfvHOME_PERCENTAGE").setAttribute("isValid",false);
				document.getElementById("revHOME_PERCENTAGE").setAttribute("enabled",false);					
				document.getElementById("revHOME_PERCENTAGE").setAttribute("isValid",false);
			}
			
		}
		function showhide_deductAmount()
		{
			if(document.getElementById("cmbCALCULATION_BASE").options[document.getElementById("cmbCALCULATION_BASE").selectedIndex].value=="14204")
			{
				document.getElementById("capINSURANCE_VALUE").style.display="inline";
				document.getElementById("txtINSURANCE_VALUE").style.display="inline";
				document.getElementById("spnINSURANCE_VALUE").style.display="inline";
				//document.getElementById("rfvHOME_AGE").style.display="inline";
				//document.getElementById("revHOME_AGE").style.display="inline";
				document.getElementById("rfvINSURANCE_VALUE").setAttribute("enabled",true);					
				document.getElementById("rfvINSURANCE_VALUE").setAttribute("isValid",true);	
				document.getElementById("revINSURANCE_VALUE").setAttribute("enabled",true);					
				document.getElementById("revINSURANCE_VALUE").setAttribute("isValid",true);
				
			}
			else
			{
				document.getElementById("capINSURANCE_VALUE").style.display="none";
				document.getElementById("txtINSURANCE_VALUE").style.display="none";
				document.getElementById("txtINSURANCE_VALUE").value = '';
				document.getElementById("rfvINSURANCE_VALUE").style.display="none";
				document.getElementById("revINSURANCE_VALUE").style.display="none";
				document.getElementById("spnINSURANCE_VALUE").style.display="none";
				document.getElementById("rfvINSURANCE_VALUE").setAttribute("enabled",false);					
				document.getElementById("rfvINSURANCE_VALUE").setAttribute("isValid",false);
				document.getElementById("revINSURANCE_VALUE").setAttribute("enabled",false);					
				document.getElementById("revINSURANCE_VALUE").setAttribute("isValid",false);
			}
			
		}
		function resetmultiselect()
		{
			for(j = document.getElementById('cmbCOVERAGE_CATEGORY').length-1; j >=0;j--)
			{
				if(document.getElementById('cmbCOVERAGE_CATEGORY').options[j].text.trim()!="" && document.getElementById('cmbCOVERAGE_CATEGORY').options[j].text!=null)
				{
					addOption(document.getElementById('cmbFROMCATEGORY'), document.getElementById('cmbCOVERAGE_CATEGORY').options[j].text, document.getElementById('cmbCOVERAGE_CATEGORY').options[j].value);
				}
				document.getElementById('cmbCOVERAGE_CATEGORY').options[j]= null;
			}
		}
		function Reset()
			{
				DisableValidators();
				//document.REIN_PREMIUM_BUILDER.reset();
				ChangeColor();
				document.getElementById('hidFormSaved').value = '0';
				populateXML();
				resetmultiselect();
				addCatagories();	
				return false;
			}	
		function ChkDate(objSource , objArgs)
		{
			var fromDate=document.getElementById('txtEFFECTIVE_DATE').value;				
			var toDate=document.getElementById('txtEXPIRY_DATE').value;
			objArgs.IsValid = DateComparer(toDate,fromDate, jsaAppDtFormat);
		}	
		
		//Formats the amount and convert 11111 into 1.1111
		function InsertDecimalAmt(AmtValues)
		{
			AmtValues = ReplaceAll(AmtValues,".","");
			DollarPart = AmtValues.substring(0, AmtValues.length - 4);
			CentPart = AmtValues.substring(AmtValues.length - 4);
			tmp = DollarPart + "." + CentPart;
			return tmp;
		}
		
		function FormatAmount(txtAmount)
		{
			if (txtAmount.value != "")
			{
				amt_1 = txtAmount.value;
				
				amt = ReplaceAll(amt_1,".","");
				
				//alert(amt+"..."+amt_1);
				if(amt == amt_1)
				{			
					/*if (amt.length == 1)
						amt = amt + "0";
					if ( ! isNaN(amt))
					{
						DollarPart = amt.substring(0, amt.length - 4);
						CentPart = amt.substring(amt.length - 4);
						txtAmount.value = InsertDecimalAmt(amt);
						txtamount = txtAmount.value;
					}*/
					txtAmount.value = amt + ".0000"
					txtamount = txtAmount.value;
				}
				else
				{
					/*var amt2 = amt_1.split(".");
					var amt2_1 = amt2[0];
					var amt2_2 =  amt2[1];
					alert('amt_2 '+ amt2_2);
					var tempa = parseFloat(parseFloat(amt_1)*10000).toFixed(4);
					if(amt2_2 > 0)
						//amt2_2 = tempa - parseFloat(parseFloat(amt2_2)*10000);
						amt2_2 = parseFloat((parseFloat(0.1 * amt2_2).toFixed(4))*10000).toFixed(4);
						alert(amt2_2);
					alert(parseFloat(amt_1).toFixed(4));
					//txtAmount.value = amt2_1 + "." + amt2_2;*/
					txtAmount.value = parseFloat(amt_1).toFixed(4);
					txtamount = txtAmount.value;
				}
				return txtamount;
			}
		}
		
		function formatDeductCurrency(num) {

		if(num==undefined)
		{
		 return num;
		}
		
		if ( trim(num) == ',' || trim(num) == '$')
		{
			return num;
		}
		
		num = num.toString().replace(/\$|\,/g,'');
		
		if(num==null || num=='')
		{
		 return num;
		}
		
		num = getActualValue(num);
		
		
		if(isNaN(num))
		{
		num=trim(num);
		return num;
		}
		else
		{
		
		sign = (num == (num = Math.abs(num)));
		num = Math.floor(num*100+0.50000000001);
		
		cents = num%100;
		num = Math.floor(num/100).toString();
		if(cents<10)
			cents = "0" + cents;
		for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
		num = num.substring(0,num.length-(4*i+3))+','+
		num.substring(num.length-(4*i+3));
			num=trim(num);
			cents=trim(cents);
		return (('-')+ num);
		//return (((sign)?'':'-') +  num);
		}
		
}

		function chkper_alarm(objSource , objArgs)
		{
			objArgs.IsValid = true;
			txtamount_alarm = FormatAmount(document.getElementById('txtALARM_PERCENTAGE'))
			if(txtamount_alarm >100.0000)
				{
					    document.getElementById('csvALARM_PERCENTAGE').innerHTML = "Alarm % must be between 0.0000 to 100.0000";				
					    Page_IsValid = false;
						objArgs.IsValid = false;
				}
				else
				{
					objArgs.IsValid = true;
				}
		}
		function chkper_home(objSource , objArgs)
		{
			objArgs.IsValid = true;
			txtamount_home = FormatAmount(document.getElementById('txtHOME_PERCENTAGE'))
			if(txtamount_home >100.0000)
				{
					    document.getElementById('csvHOME_PERCENTAGE').innerHTML = "Home % must be between 0.0000 to 100.0000";				
					    Page_IsValid = false;
						objArgs.IsValid = false;
				}
				else
				{
					objArgs.IsValid = true;
				}
		}
		function chkrate(objSource , objArgs)
		{
			objArgs.IsValid = true;
			txtamount = FormatAmount(document.getElementById('txtRATE_APPLIED'))
			if(txtamount >100.0000)
				{
					    document.getElementById('csvRATE_APPLIED').innerHTML = "Rate must be between 0.0000 to 100.0000";				
					    Page_IsValid = false;
						objArgs.IsValid = false;
				}
				else
				{
					objArgs.IsValid = true;
				}
		}
		function validatelayer(objSource , objArgs)
		{
			objArgs.IsValid = true;
			txtlayer = formatCurrency(document.getElementById('txtLAYER').value)
			document.getElementById('txtLAYER').value = txtlayer;
			if(txtlayer == 0)
			{
				document.getElementById('csvLAYER').innerHTML = "Layer should be greater than zero";				
				Page_IsValid = false;
				objArgs.IsValid = false;
			}
			else
			{
				objArgs.IsValid = true;
			}
			EnableValidator('revLAYER',true);
		}
		function validatecurrency(controlId)
		{
				controlId.value=formatCurrency(controlId.value);
				//Page_ClientValidate();	
				//Page_IsValid = true;
				return controlId.value;		
				//return Page_IsValid;
		}
		function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbCALCULATION_BASE":
						lookupMessage	=	"RCAL.";
						break;
					case "cmbCONSTRUCTION":
						lookupMessage	=	"RCONSC.";
						break;
					case "cmbFROMCATEGORY":
						lookupMessage	=	"RRB.";
						break;
					default:
						lookupMessage	=	"Look up code not found";
						break;
						
				}
				showLookupLayer(controlId,lookupMessage);							
			}
			function selectAll()
			{
				if(document.getElementById('chkSelectAll').checked == true)
				{
					for (var i=document.getElementById('cmbFROMPROTECTION').options.length-1;i>=0;i--)
					{
							document.getElementById('cmbFROMPROTECTION').options[i].selected = true;
					
					
					}
					//document.getElementById('chkSelectAll').checked = false;
					//selectProtection();
				}
			}
			function deselectAll()
			{
				if(document.getElementById('chkDeSelectAll').checked == true)
				{
					for (var i=document.getElementById('cmbPROTECTION').options.length-1;i>=0;i--)
					{
							document.getElementById('cmbPROTECTION').options[i].selected = true;
					
					
					}
					//deselectProtection();
				}
			
			}
			function chkCheckSelect()
			{
				if (document.getElementById('chkSelectAll').checked == true)
				{
					document.getElementById('chkSelectAll').checked = false;
				}
			}
			function chkCheckDeSelect()
			{
				if (document.getElementById('chkDeSelectAll').checked == true)
				{
					document.getElementById('chkDeSelectAll').checked = false;
				}
			}
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();showhide_alarm();showhide_homeage();showhide_deductAmount();ApplyColor();ChangeColor();">
		<form id="REIN_PREMIUM_BUILDER" method="post" runat="server">
			<table width="100%" align="center" border="0">
				<TBODY>
					<tr>
						<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
					</tr>
					<TR id="trBody" runat="server">
						<td>
							<table id="tblBody" width="100%" align="center" border="0" runat="server">
								<TBODY>
									<tr>
										<TD class="pageHeader" width="100%" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capCONTRACT" runat="server">Contract #</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtCONTRACT" runat="server" size="12" maxlength="10" ReadOnly="True"></asp:textbox><br>
											<asp:requiredfieldvalidator id="rfvCONTRACT" runat="server" ControlToValidate="txtCONTRACT" ErrorMessage="Contract # can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator>
											<%--<asp:regularexpressionvalidator id="revCONTRACT" runat="server" Display="Dynamic" ControlToValidate="txtCONTRACT"
												></asp:regularexpressionvalidator>--%>
										</TD>
										<TD class="midcolora" width="18%" colspan="2"></TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capEFFECTIVE_DATE" runat="server">Effective Date</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtEFFECTIVE_DATE" runat="server" size="12" maxlength="10" ReadOnly = "True"></asp:textbox>
										<asp:hyperlink id="hlkEFFECTIVE_DATE" runat="server" CssClass="HotSpot">
												<ASP:IMAGE id="imgEFFECTIVE_DATE" runat="server" ImageUrl="../../Images/CalendarPicker.gif"></ASP:IMAGE>
											</asp:hyperlink><br>
											<asp:requiredfieldvalidator id="rfvEFFECTIVE_DATE" runat="server" ControlToValidate="txtEFFECTIVE_DATE" ErrorMessage="Effective Date can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revEFFECTIVE_DATE" runat="server" Display="Dynamic" ControlToValidate="txtEFFECTIVE_DATE"
												ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator>
										</TD>
										<TD class="midcolora" width="18%"><asp:label id="capEXPIRY_DATE" runat="server">Expiry Date</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtEXPIRY_DATE" runat="server" size="12" maxlength="10" ReadOnly = "True"></asp:textbox><asp:hyperlink id="hlkEXPIRY_DATE" runat="server" CssClass="HotSpot">
												<ASP:IMAGE id="Image1" runat="server" ImageUrl="../../Images/CalendarPicker.gif"></ASP:IMAGE>
											</asp:hyperlink><br>
											<asp:requiredfieldvalidator id="rfvEXPIRY_DATE" runat="server" ControlToValidate="txtEXPIRY_DATE" ErrorMessage="Expiry Date can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revEXPIRY_DATE" runat="server" Display="Dynamic" ControlToValidate="txtEXPIRY_DATE"
												></asp:regularexpressionvalidator>
											<asp:customvalidator id="csvCHECK_DATE" Display="Dynamic" ControlToValidate="txtEXPIRY_DATE" ClientValidationFunction="ChkDate"
											Runat="server"></asp:customvalidator>
										</TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capLAYER" runat="server">Layer</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox style="TEXT-ALIGN:right" cssclass="INPUTCURRENCY" id="txtLAYER" runat="server" size="12" maxlength="2"></asp:textbox><br>
											<asp:requiredfieldvalidator id="rfvLAYER" runat="server" ControlToValidate="txtLAYER" ErrorMessage="Please Enter Layer."
												Display="Dynamic"></asp:requiredfieldvalidator>
												<asp:customvalidator id="csvLAYER" Display="Dynamic" ControlToValidate="txtLAYER" ClientValidationFunction="validatelayer"
											Runat="server"></asp:customvalidator>
											<asp:regularexpressionvalidator id="revLAYER" runat="server" Display="Dynamic" ControlToValidate="txtLAYER"
												ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator>
										</TD>
										<TD class="midcolora" width="18%" colspan="2"></TD>
									</tr>
									<tr>
										<TD class="midcolora"><asp:label id="capCOVERAGE_CATEGORY" runat="server">Coverage Category Coverage Code Description</asp:label><SPAN class="mandatory">*</SPAN></TD>
										<td class="midcolora" align="center" width="18%">
											<%--<asp:label id="capCATEGORYDETAILS" Runat="server">Category Details</asp:label><br>--%>
											<asp:listbox id="cmbFROMCATEGORY" Runat="server" Height="79px" AutoPostBack="False" SelectionMode="Multiple">
												<ASP:LISTITEM Value="0"><--------Select--------></ASP:LISTITEM>
											</asp:listbox><a class="calcolora" href="javascript:showPageLookupLayer('cmbFROMCATEGORY')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
											<br>
										</td>
										<td class="midcolorc" align="center" width="18%"><br>
											<asp:button class="clsButton" id="btnSELECT" Runat="server" Text=">>" CausesValidation="True"></asp:button><br>
											<br>
											<br>
											<br>
											<asp:button class="clsButton" id="btnDESELECT" Runat="server" Text="<<" CausesValidation="False"></asp:button></td>
										<td class="midcolora" align="center">
											<asp:label id="capRECIPIENTS" Runat="server" style="DISPLAY: none">Recipients</asp:label>
											<asp:listbox id="cmbCOVERAGE_CATEGORY" onblur="" Runat="server" Height="79px" Width="200px" AutoPostBack="False"
												SelectionMode="Multiple" onChange=""></asp:listbox><br>
											<asp:customvalidator id="csvCOVERAGE_CATEGORY" Display="Dynamic" ControlToValidate="cmbCOVERAGE_CATEGORY" Runat="server"
												ClientValidationFunction="funcValidateCatagories" ErrorMessage="Please select Category."></asp:customvalidator>
												<span id="spnCATEGORY" style="DISPLAY: none; COLOR: red">Please select Coverage Category.</span>
										</td>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capCALCULATION_BASE" runat="server">Calculation Based On</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%">
										<asp:dropdownlist id="cmbCALCULATION_BASE" onfocus="SelectComboIndex('cmbCALCULATION_BASE')"
											 runat="server" onChange = "showhide_deductAmount();"></asp:dropdownlist><a class="calcolora" href="javascript:showPageLookupLayer('cmbCALCULATION_BASE')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
											 <br>
											<asp:requiredfieldvalidator id="rfvCALCULATION_BASE" ControlToValidate="cmbCALCULATION_BASE" ErrorMessage="Please Select Calculation Based On" Display="Dynamic"
												Runat="server"></asp:requiredfieldvalidator>
										</TD>
										<TD class="midcolora" width="18%"><asp:label id="capINSURANCE_VALUE" runat="server">Deduct Amount from total insurance Value</asp:label><span id ="spnINSURANCE_VALUE" class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox style="TEXT-ALIGN:right" cssclass="INPUTCURRENCY" id="txtINSURANCE_VALUE" runat="server" size="15" maxlength="10"></asp:textbox><br>
											<asp:requiredfieldvalidator id="rfvINSURANCE_VALUE" runat="server" ControlToValidate="txtINSURANCE_VALUE" ErrorMessage="Please Enter Deduct Amount from total insurance Value."
												Display="Dynamic"></asp:requiredfieldvalidator>
												<asp:regularexpressionvalidator id="revINSURANCE_VALUE" runat="server" Display="Dynamic" ControlToValidate="txtINSURANCE_VALUE"
												ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator>
												
										</TD>
									</tr><%-- --%>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capTOTAL_INSURANCE_FROM" runat="server">Total Insurable Value From</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox style="TEXT-ALIGN:right" cssclass="INPUTCURRENCY" id="txtTOTAL_INSURANCE_FROM" runat="server" size="15" maxlength="10"></asp:textbox><br>
											<asp:requiredfieldvalidator id="rfvTOTAL_INSURANCE_FROM" runat="server" ControlToValidate="txtTOTAL_INSURANCE_FROM" ErrorMessage="Please Enter Total Insurable Value From."
												Display="Dynamic"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revTOTAL_INSURANCE_FROM" runat="server" Display="Dynamic" ControlToValidate="txtTOTAL_INSURANCE_FROM"
												ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator>
										</TD>
										<TD class="midcolora" width="18%"><asp:label id="capTOTAL_INSURANCE_TO" runat="server">Total Insurance Value To</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox style="TEXT-ALIGN:right" cssclass="INPUTCURRENCY" id="txtTOTAL_INSURANCE_TO" runat="server" size="15" maxlength="10"></asp:textbox><br>
											<asp:requiredfieldvalidator id="rfvTOTAL_INSURANCE_TO" runat="server" ControlToValidate="txtTOTAL_INSURANCE_TO" ErrorMessage="Please Enter Total Insurance Value To."
												Display="Dynamic"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revTOTAL_INSURANCE_TO" runat="server" Display="Dynamic" ControlToValidate="txtTOTAL_INSURANCE_TO"
												ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator>
										</TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capOTHER_INST" runat="server">Other Instructions</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%">
										<asp:dropdownlist id="cmbOTHER_INST" onfocus="SelectComboIndex('cmbOTHER_INST')" runat="server">
										<%--<asp:ListItem Value="" Selected="True"></asp:ListItem>
										<asp:ListItem Value="1">Reduce Premium by 50%</asp:ListItem>
										<asp:ListItem Value="2"> 9183A Premium all layers -based on Reinsurance Premium</asp:ListItem>--%>
										</asp:dropdownlist>
								<br>
											<asp:requiredfieldvalidator id="rfvOTHER_INST" ControlToValidate="cmbOTHER_INST" ErrorMessage="Please Select Other Instructions" Display="Dynamic"
												Runat="server"></asp:requiredfieldvalidator>
										</TD>
										<TD class="midcolora" width="18%"><asp:label id="capRATE_APPLIED" runat="server">Rate Applied </asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox style="TEXT-ALIGN:right" cssclass="INPUTCURRENCY" id="txtRATE_APPLIED" runat="server" size="12" maxlength="10"></asp:textbox><br>
											<asp:requiredfieldvalidator id="rfvRATE_APPLIED" runat="server" ControlToValidate="txtRATE_APPLIED" ErrorMessage="Please Enter Rate Applied."
												Display="Dynamic"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revRATE_APPLIED" runat="server" Display="Dynamic" ControlToValidate="txtRATE_APPLIED"
												ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator>
											<asp:customvalidator id="csvRATE_APPLIED" Display="Dynamic" ControlToValidate="txtRATE_APPLIED" Runat="server"
												ClientValidationFunction="chkrate" ErrorMessage="RegularExpressionValidator"></asp:customvalidator>	
										</TD>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capCONSTRUCTION" runat="server">Construction</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%">
										<asp:dropdownlist id="cmbCONSTRUCTION" onfocus="SelectComboIndex('cmbCONSTRUCTION')"
											 runat="server"></asp:dropdownlist><a class="calcolora" href="javascript:showPageLookupLayer('cmbCONSTRUCTION')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
											 <br>
											<asp:requiredfieldvalidator id="rfvCONSTRUCTION" ControlToValidate="cmbCONSTRUCTION" ErrorMessage="Please Select Construction" Display="Dynamic"
												Runat="server"></asp:requiredfieldvalidator>
										</TD>
										<TD class="midcolora" width="18%" colspan="2"></TD>
									</tr>
									<tr>
										<TD class="midcolora"><asp:label id="capPROTECTION" runat="server">Protection</asp:label><SPAN class="mandatory">*</SPAN></TD>
										<td class="midcolora" align="center" width="18%">
											<%--<asp:label id="capCATEGORYDETAILS" Runat="server">Category Details</asp:label><br>--%>
											<asp:listbox id="cmbFROMPROTECTION" Runat="server" Height="79px" width = "80px" AutoPostBack="False" SelectionMode="Multiple">
												<ASP:LISTITEM Value="0"><--------Select--------></ASP:LISTITEM>
											</asp:listbox><br>
											<asp:checkbox id="chkSelectAll" runat="server" onClick="selectAll();"></asp:checkbox>Select All
										</td>
										<td class="midcolorc" align="center" width="18%"><br>
											<asp:button class="clsButton" id="btnSELECT_PROTECTION" Runat="server" Text=">>" CausesValidation="True"></asp:button><br>
											<br>
											<br>
											<br>
											<asp:button class="clsButton" id="btnDESELECT_PROTECTION" Runat="server" Text="<<" CausesValidation="False"></asp:button></td>
										<td class="midcolora" align="center">
											<asp:label id="capRECIPIENT" Runat="server" style="DISPLAY: none">Recipient</asp:label>
											<asp:listbox id="cmbPROTECTION" onblur="" Runat="server" Height="79px" Width="200px" AutoPostBack="False"
												SelectionMode="Multiple" onChange=""></asp:listbox><br>
												<asp:checkbox id="chkDeSelectAll" runat="server" onClick="deselectAll();" Text="Select All"></asp:checkbox>
												<%--<asp:requiredfieldvalidator id="rfvPROTECTION" ControlToValidate="cmbPROTECTION" ErrorMessage="Please select Category." Display="Dynamic"
												Runat="server"></asp:requiredfieldvalidator>--%>
											<asp:customvalidator id="csvPROTECTION" Display="Dynamic" ControlToValidate="cmbPROTECTION" Runat="server"
												ClientValidationFunction="funcValidateProtection" ErrorMessage="Please select Protection."></asp:customvalidator>
												<span id="spnPROTECTION" style="DISPLAY: none; COLOR: red">Please select Protection.</span>
										</td>
									</tr>
									<tr>
										<TD class="midcolora" width="18%"><asp:label id="capALARM_CREDIT" runat="server">Central Alarm Credit Applies</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%">
										<asp:dropdownlist id="cmbALARM_CREDIT" onfocus="SelectComboIndex('cmbALARM_CREDIT')"  runat="server" onChange = "showhide_alarm();"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvALARM_CREDIT" ControlToValidate="cmbALARM_CREDIT" ErrorMessage="Please Select Central Alarm Credit Applies" Display="Dynamic"
												Runat="server"></asp:requiredfieldvalidator>
										</TD>
										<TD class="midcolora" width="18%"><asp:label id="capALARM_PERCENTAGE" runat="server">Alarm %</asp:label><span id = "spnALARM_PERCENTAGE"class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox style="TEXT-ALIGN:right" cssclass="INPUTCURRENCY" id="txtALARM_PERCENTAGE" runat="server" size="12" maxlength="8"></asp:textbox><br>
											<asp:requiredfieldvalidator id="rfvALARM_PERCENTAGE" runat="server" ControlToValidate="txtALARM_PERCENTAGE" ErrorMessage="Please Enter Alarm %."
												Display="Dynamic"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revALARM_PERCENTAGE" runat="server" Display="Dynamic" ControlToValidate="txtALARM_PERCENTAGE"
												ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator>
											<asp:customvalidator id="csvALARM_PERCENTAGE" Display="Dynamic" ControlToValidate="txtALARM_PERCENTAGE" Runat="server"
												ClientValidationFunction="chkper_alarm" ErrorMessage="RegularExpressionValidator"></asp:customvalidator>	
										</TD>
									</tr>
									<tr>
										<TD class="midcolora" rowspan="2" style="VERTICAL-ALIGN:middle"><asp:label id="capHOME_CREDIT" runat="server">New Home Credit Applies</asp:label><span class="mandatory">*</span></TD>
										<TD class="midcolora" rowspan="2" style="VERTICAL-ALIGN:middle">
										<asp:dropdownlist id="cmbHOME_CREDIT" onfocus="SelectComboIndex('cmbHOME_CREDIT')"	 runat="server" onChange = "showhide_homeage();"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvHOME_CREDIT" ControlToValidate="cmbHOME_CREDIT" ErrorMessage="Please Select New Home Credit Applies" Display="Dynamic"
												Runat="server"></asp:requiredfieldvalidator>
										</TD>
										<TD class="midcolora" width="18%"><asp:label id="capHOME_AGE" runat="server">Age for New Home</asp:label><span id = "spnHOME_AGE" class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox style="TEXT-ALIGN:right" cssclass="INPUTCURRENCY" id="txtHOME_AGE" runat="server" size="12" maxlength="2"></asp:textbox><br>
											<asp:requiredfieldvalidator id="rfvHOME_AGE" runat="server" ControlToValidate="txtHOME_AGE" ErrorMessage="Please Enter Age for New Home."
												Display="Dynamic"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revHOME_AGE" runat="server" Display="Dynamic" ControlToValidate="txtHOME_AGE"
												ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator>
										</TD>
									</tr>
									<tr>
									<TD class="midcolora" width="18%"><asp:label id="capHOME_PERCENTAGE" runat="server">Home %</asp:label><span id = "spnHOME_PERCENTAGE"class="mandatory">*</span></TD>
										<TD class="midcolora" width="32%"><asp:textbox style="TEXT-ALIGN:right" cssclass="INPUTCURRENCY" id="txtHOME_PERCENTAGE" runat="server" size="12" maxlength="8"></asp:textbox><br>
											<asp:requiredfieldvalidator id="rfvHOME_PERCENTAGE" runat="server" ControlToValidate="txtHOME_PERCENTAGE" ErrorMessage="Please Enter Home %."
												Display="Dynamic"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revHOME_PERCENTAGE" runat="server" Display="Dynamic" ControlToValidate="txtHOME_PERCENTAGE"
												ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator>
											<asp:customvalidator id="csvHOME_PERCENTAGE" Display="Dynamic" ControlToValidate="txtHOME_PERCENTAGE" Runat="server"
												ClientValidationFunction="chkper_home" ErrorMessage="RegularExpressionValidator"></asp:customvalidator>	
										</TD>
									</tr>
									<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCOMMENTS" runat="server">Comments</asp:label></TD>
										<TD class="midcolora" width="32%"><asp:textbox onkeypress="MaxLength(this,255);" id="txtCOMMENTS" runat="server" size="30" maxlength="255" Rows="5"
										Columns="40" TextMode="MultiLine"></asp:textbox><br>
										<%--	<asp:requiredfieldvalidator id="rfvCOMMENTS" runat="server" ControlToValidate="txtCOMMENTS" ErrorMessage="Comments can't be blank."
												Display="Dynamic"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revCOMMENTS" runat="server" Display="Dynamic" ControlToValidate="txtCOMMENTS"
												ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator>--%>
										</TD>
										<TD class="midcolora" width="18%" colspan="2"></TD>
									</tr>
									<tr>
										<td class="midcolora" colSpan="2">
											<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" causesValidation="false"></cmsb:cmsbutton>
											<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton>
											<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"
												CausesValidation="false"></cmsb:cmsbutton>
										</td>
										<td class="midcolorr" colSpan="2">
											<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
										</td>
									</tr>
								</TBODY>
							</table>
						</td>
					</TR>
					</TR> <INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
					<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
					<INPUT id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server"> 
					<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
					<INPUT id="hidOldTempData" type="hidden" name="hidOldData" runat="server">
					<INPUT id="hidPREMIUM_BUILDER_ID" type="hidden" value="0" name="hidPREMIUM_BUILDER_ID" runat="server"> 
					<INPUT id="hidCONTRACT_ID" type="hidden" name="hidCONTRACT_ID" runat="server">
					<INPUT id="hidCATAGORY" type="hidden" name="hidCATAGORY" runat="server">
					<INPUT id="hidPROTECTION" type="hidden" name="hidPROTECTION" runat="server">

						
				</TBODY>
			</table>
		</form>
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
				RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidPREMIUM_BUILDER_ID').value, false);
				addCatagories();
				addProtection();				
		</script>
	</body>
</HTML>
